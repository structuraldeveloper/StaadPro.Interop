using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Interfaces;
using StaadPro.Interop.Models;
using StaadPro.Interop.Services.Acquisition;

namespace StaadPro.Interop.Services
{
    /// <summary>Independently acquires a verified target, reusable unnamed session, or newly opened target.</summary>
    /// <remarks>
    /// <para>Create, use and dispose this provider on one Windows STA thread. It retains previously obtained roots
    /// and validates process ID/start time on subsequent requests. The existing provider and wrapper are not modified.</para>
    /// <para>Best-effort discovery permits fallback when applications are unmapped; it cannot guarantee that an
    /// inaccessible application does not already contain the target. Strict discovery can instead fail.
    /// Synchronous COM calls cannot be forcibly interrupted by the logical timeout or cancellation token.</para>
    /// </remarks>
    public sealed class PreferExistingOpenStaadWrapperProvider : IOpenStaadWrapperResolver, IDisposable
    {
        private readonly IOpenStaadAcquisitionEnvironment environment;
        private readonly OpenStaadAcquisitionOptions options;
        private readonly int ownerThread = Thread.CurrentThread.ManagedThreadId;
        private readonly Dictionary<string, object> retained = new Dictionary<string, object>();
        private bool disposed;
        [ThreadStatic] private static int acquisitionDepth;

        /// <summary>Creates a provider on the calling Windows STA thread.</summary>
        /// <param name="options">Optional configuration copied into the provider; null uses attachment-capable defaults with launching disabled.</param>
        /// <remarks>A configured executable is needed only for create/open fallback. Construction initializes COM but does not start STAAD.</remarks>
        /// <exception cref="PlatformNotSupportedException">The operating system is not Windows.</exception>
        /// <exception cref="InvalidOperationException">The caller is not an STA thread.</exception>
        /// <exception cref="ArgumentException">An executable path or discovery policy is invalid.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Timeout, polling or grace values are invalid.</exception>
        public PreferExistingOpenStaadWrapperProvider(OpenStaadAcquisitionOptions options = null)
        {
            this.options = CopyOptions(options);
            CheckThread();
            environment = new WindowsOpenStaadEnvironment();
        }

        internal PreferExistingOpenStaadWrapperProvider(OpenStaadAcquisitionOptions options, IOpenStaadAcquisitionEnvironment environment)
        {
            this.options = CopyOptions(options);
            CheckThread();
            this.environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        /// <summary>Acquires a verified wrapper, returning null on an operational acquisition failure.</summary>
        /// <param name="fileFullPath">Absolute .std target path, or null for attachment only without creation or launch.</param>
        /// <returns>A target, blank or running-session wrapper; null if acquisition fails. Use Acquire to inspect the outcome and diagnostics.</returns>
        /// <remarks>A returned ExistingBlank does not open the requested path. Run on the provider's owning STA thread.
        /// Caller-input, apartment, disposal and reentrancy errors throw. Get does not replace the existing static provider's resolver.</remarks>
        /// <exception cref="ArgumentException">The supplied target is not a valid absolute .std path.</exception>
        /// <exception cref="InvalidOperationException">The caller violates STA ownership or reenters acquisition.</exception>
        /// <exception cref="ObjectDisposedException">The provider has been disposed.</exception>
        public OpenStaadWrapper Get(string fileFullPath = null) => Acquire(fileFullPath).Wrapper;

        /// <summary>Acquires a wrapper with immutable process, outcome and discovery diagnostics.</summary>
        /// <param name="fileFullPath">Absolute .std target path, or null for attachment only.</param>
        /// <param name="cancellationToken">Cancellation observed before side effects and between synchronous native operations.</param>
        /// <returns>A non-null result; Wrapper is null on operational failure. Successful blank outcomes have an empty ActualPath.</returns>
        /// <remarks>
        /// <para>Prefers a matching target, waits for a known busy target, then tries a blank verified by zero nodes,
        /// members, plates, solids, primary loads, reference loads and combination cases. These checks do not certify
        /// absence of every native definition. The provider never opens a file into an existing blank or named model.</para>
        /// <para>Missing files are prepared through atomic, non-overwriting publication. Cooperating callers serialize
        /// fallback for a normalized target. Startup is attempted once and a successful new target must belong to the
        /// actual newly launched process. Native calls may exceed the logical timeout. UI changes after the last
        /// validation remain possible; coordinate all subsequent COM use on the same STA thread.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">The target path is invalid.</exception>
        /// <exception cref="InvalidOperationException">The caller violates STA ownership or reenters acquisition.</exception>
        /// <exception cref="ObjectDisposedException">The provider has been disposed.</exception>
        /// <example><code>
        /// // On a Windows STA thread:
        /// using (var provider = new PreferExistingOpenStaadWrapperProvider())
        /// {
        ///     var result = provider.Acquire(); // attachment only; never launches
        ///     if (result.Succeeded) result.Wrapper.Dispose();
        /// }
        /// </code></example>
        public OpenStaadAcquisitionResult Acquire(string fileFullPath = null, CancellationToken cancellationToken = default)
        {
            CheckThread();
            if (acquisitionDepth != 0) throw new InvalidOperationException("OpenSTAAD acquisition cannot be reentered on the owning STA thread.");
            string target = fileFullPath == null ? null : NormalizePath(fileFullPath, true);
            var budget = new RequestBudget(environment, options, cancellationToken);
            var diagnostics = new List<string>();
            Snapshot snapshot = null;
            IDisposable coordination = null;
            StaadProcessIdentity launched = null;
            bool waitingForKnownTarget = false;
            OpenStaadAcquisitionFailure stage = OpenStaadAcquisitionFailure.DiscoveryFailed;
            acquisitionDepth++;
            try
            {
                while (true)
                {
                    budget.Check();
                    stage = OpenStaadAcquisitionFailure.DiscoveryFailed;
                    snapshot = Observe(diagnostics, budget);
                    var matches = snapshot.Observations.Where(o => o.Readable && o.Path.Length > 0 &&
                        (target == null || string.Equals(o.Path, target, StringComparison.OrdinalIgnoreCase))).ToList();
                    waitingForKnownTarget = target != null && matches.Count > 0;

                    if (launched != null)
                    {
                        foreach (var match in matches.Where(o => o.Process.Key == launched.Key))
                        {
                            var result = TryReturn(match, false, true, snapshot, target, diagnostics, budget);
                            if (result != null) return result;
                        }
                        if (matches.Any(o => o.Process.Key != launched.Key))
                            return Fail(OpenStaadAcquisitionFailure.LaunchRedirected, "The opened target belongs to another process; new-process ownership was not established.");
                        if (!snapshot.Processes.Any(p => p.Key == launched.Key))
                            return Fail(OpenStaadAcquisitionFailure.LaunchedProcessExited, "The launched STAAD process exited before the requested model was ready.");
                        budget.Pause(); continue;
                    }

                    foreach (var match in matches)
                    {
                        var result = TryReturn(match, false, false, snapshot, target, diagnostics, budget);
                        if (result != null) return result;
                    }
                    if (target != null && matches.Count > 0)
                    {
                        Note(diagnostics, "The target is identified but not ready; no blank or duplicate launch will replace it.");
                        budget.Pause(); continue;
                    }
                    if (snapshot.Observations.Any(o => !o.Readable))
                    {
                        Note(diagnostics, "A mapped root's native model identity is unreadable; waiting before fallback.");
                        budget.Pause(); continue;
                    }
                    if (!snapshot.Complete && (options.DiscoveryPolicy == OpenStaadDiscoveryPolicy.RequireCompleteDiscovery ||
                        budget.Elapsed < options.DiscoveryGracePeriod))
                    { budget.Pause(); continue; }

                    foreach (var blank in snapshot.Observations.Where(o => o.Readable && o.Path.Length == 0))
                    {
                        var result = TryReturn(blank, true, false, snapshot, target, diagnostics, budget);
                        if (result != null) return result;
                    }
                    if (!snapshot.Complete && options.DiscoveryPolicy == OpenStaadDiscoveryPolicy.RequireCompleteDiscovery)
                    { budget.Pause(); continue; }
                    if (target == null && snapshot.Processes.Count == 0)
                        return Fail(OpenStaadAcquisitionFailure.NoRunningSession, "Attachment-only acquisition found no running supported STAAD application.");
                    if (target == null || (snapshot.Observations.Any(o => o.Readable && o.Path.Length == 0) && budget.Elapsed < options.DiscoveryGracePeriod))
                    { budget.Pause(); continue; }

                    if (!snapshot.Complete)
                        Note(diagnostics, "Best-effort fallback: one or more running applications are unmapped or discovery was incomplete; a hidden target cannot be ruled out.");
                    if (coordination == null)
                    {
                        coordination = environment.TryCoordinate(target);
                        if (coordination == null) budget.Pause();
                        // Always recheck discovery after acquiring coordination, before any file write.
                        continue;
                    }
                    budget.Check();
                    stage = OpenStaadAcquisitionFailure.LaunchFailed;
                    if (options.StaadExecutablePath == null)
                        return Fail(OpenStaadAcquisitionFailure.LaunchFailed, "Launching is disabled: configure a STAAD executable. No target was prepared.");
                    environment.ValidateLaunch(options.StaadExecutablePath);
                    budget.Check();
                    stage = OpenStaadAcquisitionFailure.FilePreparationFailed;
                    environment.PrepareTarget(target, options.CreateMissingTarget, budget.Check);
                    budget.Check();
                    stage = OpenStaadAcquisitionFailure.LaunchFailed;
                    launched = environment.Launch(options.StaadExecutablePath, target);
                    if (launched == null) return Fail(OpenStaadAcquisitionFailure.LaunchFailed, "Startup returned no process identity.");
                    if (snapshot.Processes.Any(p => p.Key == launched.Key))
                        return Fail(OpenStaadAcquisitionFailure.LaunchRedirected, "Startup returned a pre-existing process; dedicated ownership is not established.");
                    budget.Check();
                }
            }
            catch (OperationCanceledException) { return Fail(OpenStaadAcquisitionFailure.Cancelled, "Acquisition was cancelled. Existing files and applications were preserved."); }
            catch (TimeoutException)
            {
                var failure = waitingForKnownTarget ? OpenStaadAcquisitionFailure.Timeout : snapshot != null && snapshot.Observations.Any(o => !o.Readable)
                    ? OpenStaadAcquisitionFailure.IdentityUnavailable
                    : snapshot != null && !snapshot.Complete && options.DiscoveryPolicy == OpenStaadDiscoveryPolicy.RequireCompleteDiscovery
                        ? OpenStaadAcquisitionFailure.IncompleteDiscovery : OpenStaadAcquisitionFailure.Timeout;
                return Fail(failure, "The logical acquisition budget expired. It does not interrupt an in-progress native call.");
            }
            catch (Exception ex) when (!(ex is OutOfMemoryException)) { return Fail(stage, ex.Message + " (HRESULT 0x" + ex.HResult.ToString("X8") + ")"); }
            finally { try { coordination?.Dispose(); } finally { acquisitionDepth--; } }

            OpenStaadAcquisitionResult Fail(OpenStaadAcquisitionFailure failure, string message)
            {
                if (diagnostics.Count >= 128 && !diagnostics.Contains(message)) diagnostics.RemoveAt(diagnostics.Count - 1);
                Note(diagnostics, message);
                return new OpenStaadAcquisitionResult(null, OpenStaadAcquisitionOutcome.Failure, failure,
                    target, null, null, null, snapshot?.Complete ?? false, diagnostics, launched?.Id, launched?.StartedUtc);
            }
        }

        /// <summary>Retains a caller-obtained root after verifying its native PID, live process start time and readable model identity.</summary>
        /// <param name="openStaad">An existing root actually obtained by the caller; registration does not bind an inaccessible process.</param>
        /// <remarks>Run on the owning STA thread outside acquisition. Retained roots are revalidated on every request;
        /// registration does not transfer application ownership or certify readiness.</remarks>
        /// <exception cref="ArgumentNullException">The supplied root is null.</exception>
        /// <exception cref="InvalidOperationException">The root cannot be verified or the caller violates thread/reentrancy requirements.</exception>
        /// <exception cref="ObjectDisposedException">The provider has been disposed.</exception>
        public void RegisterRoot(object openStaad)
        {
            CheckIdle();
            if (openStaad == null) throw new ArgumentNullException(nameof(openStaad));
            int pid = ReadInteger(((dynamic)openStaad).GetProcessId(), "process ID", true);
            var identity = environment.Inventory().SingleOrDefault(p => p.Id == pid);
            if (identity == null) throw new InvalidOperationException("The root does not belong to a live supported application in this session.");
            ReadPath(openStaad);
            retained[identity.Key] = openStaad;
        }

        /// <summary>Clears retained root references without closing STAAD or disposing returned wrappers.</summary>
        /// <remarks>Run on the owning STA thread outside acquisition. Shared COM roots are not forcibly released.</remarks>
        /// <exception cref="InvalidOperationException">The caller violates thread or reentrancy requirements.</exception>
        /// <exception cref="ObjectDisposedException">The provider has been disposed.</exception>
        public void ClearRetainedRoots() { CheckIdle(); retained.Clear(); }

        /// <summary>Clears retained references and balances this provider's COM initialization on its owning thread.</summary>
        /// <remarks>Does not close applications, delete targets, final-release shared roots or dispose returned wrappers. Repeated disposal on the owning thread is harmless.</remarks>
        /// <exception cref="InvalidOperationException">Called from another thread or during acquisition.</exception>
        public void Dispose()
        {
            if (Thread.CurrentThread.ManagedThreadId != ownerThread || acquisitionDepth != 0)
                throw new InvalidOperationException("Dispose the provider on its owning STA thread outside acquisition.");
            if (disposed) return;
            retained.Clear(); environment.Dispose(); disposed = true;
        }

        private Snapshot Observe(List<string> diagnostics, RequestBudget budget)
        {
            var processes = environment.Inventory();
            budget.Check();
            var discovery = environment.Discover();
            foreach (string diagnostic in discovery.Diagnostics) Note(diagnostics, diagnostic);
            foreach (var key in retained.Keys.Where(key => !processes.Any(p => p.Key == key)).ToArray()) retained.Remove(key);
            var roots = new List<object>(retained.Values);
            foreach (object root in discovery.Roots) if (root != null && !roots.Any(r => ReferenceEquals(root, r))) roots.Add(root);
            var observations = new List<Observation>();
            bool complete = discovery.Complete;
            foreach (object root in roots)
            {
                budget.Check();
                StaadProcessIdentity process;
                try
                {
                    int pid = ReadInteger(((dynamic)root).GetProcessId(), "process ID", true);
                    process = processes.SingleOrDefault(p => p.Id == pid);
                    if (process == null) { complete = false; Note(diagnostics, "An obtained root did not map to a live supported application."); continue; }
                }
                catch (Exception ex) { complete = false; Note(diagnostics, "An obtained candidate could not provide a valid process ID: " + ex.Message); continue; }
                var observation = new Observation { Root = root, Process = process };
                try { observation.Path = ReadPath(root); observation.Readable = true; }
                catch (Exception ex) { Note(diagnostics, "PID " + process.Id + " model identity: " + ex.Message); }
                observations.Add(observation);
            }
            // Prefer the freshest readable alias over an obsolete retained proxy of the same process.
            var selected = observations.GroupBy(o => o.Process.Key).Select(group =>
            {
                var readable = group.Where(o => o.Readable).ToList();
                if (readable.Select(o => o.Path).Distinct(StringComparer.OrdinalIgnoreCase).Count() > 1)
                    return new Observation { Root = group.Last().Root, Process = group.Last().Process };
                return readable.LastOrDefault() ?? group.Last();
            }).OrderBy(o => o.Process.StartedUtc).ThenBy(o => o.Process.Id).ToList();
            foreach (var observation in selected.Where(o => o.Readable)) retained[observation.Process.Key] = observation.Root;
            foreach (var process in processes)
                if (!selected.Any(o => o.Process.Key == process.Key && o.Readable))
                { complete = false; Note(diagnostics, "PID " + process.Id + " is unmapped or has unreadable model identity."); }
            return new Snapshot { Processes = processes, Observations = selected, Complete = complete };
        }

        private OpenStaadAcquisitionResult TryReturn(Observation observation, bool blank, bool dedicated,
            Snapshot snapshot, string target, List<string> diagnostics, RequestBudget budget)
        {
            OpenStaadWrapper wrapper = null;
            bool succeeded = false;
            try
            {
                budget.Check();
                dynamic root = observation.Root;
                CheckReady((object)root.Geometry, (object)root.Load, blank, budget.Check);
                budget.Check();
                wrapper = new OpenStaadWrapper(observation.Root, dedicated);
                budget.Check();
                CheckReady((object)wrapper.RawGeometry, (object)wrapper.RawLoad, blank, budget.Check);
                budget.Check();
                string path = ReadPath(observation.Root);
                budget.Check();
                int pid = ReadInteger(root.GetProcessId(), "process ID", true);
                budget.Check();
                var current = environment.Inventory();
                budget.Check();
                if (pid != observation.Process.Id || !current.Any(p => p.Key == observation.Process.Key) ||
                    !string.Equals(path, observation.Path, StringComparison.OrdinalIgnoreCase) || (blank && path.Length != 0))
                { Note(diagnostics, "Session identity changed during final wrapper validation; rescanning."); return null; }
                bool complete = snapshot.Complete && current.All(p => snapshot.Observations.Any(o => o.Readable && o.Process.Key == p.Key));
                snapshot.Complete = complete;
                if (blank && !complete && options.DiscoveryPolicy == OpenStaadDiscoveryPolicy.RequireCompleteDiscovery)
                { Note(diagnostics, "Discovery coverage changed during blank validation; strict policy requires rescanning."); return null; }
                if (!complete) Note(diagnostics, "The wrapper identity is verified, but complete application discovery is not established.");
                var outcome = dedicated ? OpenStaadAcquisitionOutcome.OpenedTarget : blank ? OpenStaadAcquisitionOutcome.ExistingBlank
                    : target != null ? OpenStaadAcquisitionOutcome.ExistingTarget : OpenStaadAcquisitionOutcome.ExistingSession;
                var result = new OpenStaadAcquisitionResult(wrapper, outcome, OpenStaadAcquisitionFailure.None,
                    target, path, pid, observation.Process.StartedUtc, complete, diagnostics,
                    dedicated ? (int?)pid : null, dedicated ? (DateTime?)observation.Process.StartedUtc : null);
                succeeded = true; return result;
            }
            catch (OperationCanceledException) { throw; }
            catch (TimeoutException) { throw; }
            catch (Exception ex) { Note(diagnostics, "PID " + observation.Process.Id + " readiness: " + ex.Message); return null; }
            finally { if (!succeeded) wrapper?.Dispose(); }
        }

        internal static void CheckReady(object geometry, object load, bool blank, Action checkpoint)
        {
            if (geometry == null || load == null) throw new InvalidOperationException("Both Geometry and Load must be available.");
            dynamic g = geometry, l = load;
            checkpoint(); int nodes = ReadInteger(g.GetNodeCount(), "node count");
            checkpoint(); int primary = ReadInteger(l.GetPrimaryLoadCaseCount(), "primary load count");
            if (!blank) return;
            if (nodes != 0 || primary != 0) throw new InvalidOperationException("The unnamed session contains geometry or primary loads.");
            checkpoint(); int members = ReadInteger(g.GetMemberCount(), "member count");
            checkpoint(); int plates = ReadInteger(g.GetPlateCount(), "plate count");
            checkpoint(); int solids = ReadInteger(g.GetSolidCount(), "solid count");
            checkpoint(); int reference = ReadInteger(l.GetReferenceLoadCaseCount(), "reference load count");
            checkpoint(); int combinations = ReadInteger(l.GetLoadCombinationCaseCount(), "combination count");
            if (members != 0 || plates != 0 || solids != 0 || reference != 0 || combinations != 0)
                throw new InvalidOperationException("The unnamed session is not empty under the provider's content contract.");
        }
        internal static int ReadInteger(object value, string name, bool positive = false)
        {
            if (!(value is byte || value is sbyte || value is short || value is ushort || value is int || value is uint || value is long || value is ulong))
                throw new InvalidOperationException("OpenSTAAD returned a non-integral " + name + ".");
            decimal integer = Convert.ToDecimal(value, System.Globalization.CultureInfo.InvariantCulture);
            if (integer < (positive ? 1 : 0) || integer > int.MaxValue)
                throw new InvalidOperationException("OpenSTAAD returned an out-of-range " + name + ".");
            return (int)integer;
        }
        private static string ReadPath(object root)
        {
            string path = new OSRootAdapter(root).GetStaadFileFullPath();
            return path.Length == 0 ? string.Empty : NormalizePath(path, true);
        }
        internal static string NormalizePath(string path, bool std)
        {
            bool drive = path != null && path.Length >= 3 && char.IsLetter(path[0]) && path[1] == ':' && (path[2] == '\\' || path[2] == '/');
            bool unc = path != null && path.StartsWith(@"\\", StringComparison.Ordinal) &&
                !path.StartsWith(@"\\?\", StringComparison.Ordinal) && !path.StartsWith(@"\\.\", StringComparison.Ordinal);
            if (string.IsNullOrWhiteSpace(path) || (!drive && !unc) || path.IndexOfAny(new[] { '"', '\0', '*', '?', '<', '>', '|' }) >= 0 ||
                path.IndexOf(':', drive ? 2 : 0) >= 0)
                throw new ArgumentException("Supply a conventional absolute Windows file path.", nameof(path));
            foreach (string rawSegment in path.Replace('/', '\\').Substring(drive ? 3 : 2).Split('\\'))
                if (rawSegment != "." && rawSegment != ".." &&
                    (rawSegment.EndsWith(" ", StringComparison.Ordinal) || rawSegment.EndsWith(".", StringComparison.Ordinal)))
                    throw new ArgumentException("The file path contains a component with an ambiguous trailing space or period.", nameof(path));
            string full = Path.GetFullPath(path);
            foreach (string segment in full.Substring(drive ? 3 : 2).Split('\\'))
            {
                string stem = segment.Split('.')[0].ToUpperInvariant();
                if (segment.EndsWith(" ", StringComparison.Ordinal) || segment.EndsWith(".", StringComparison.Ordinal) ||
                    stem == "CON" || stem == "PRN" || stem == "AUX" || stem == "NUL" ||
                    (stem.Length == 4 && (stem.StartsWith("COM", StringComparison.Ordinal) || stem.StartsWith("LPT", StringComparison.Ordinal)) && stem[3] >= '1' && stem[3] <= '9'))
                    throw new ArgumentException("The file path contains an ambiguous or reserved Windows component.", nameof(path));
            }
            if (string.IsNullOrEmpty(Path.GetFileName(full))) throw new ArgumentException("Supply a filename, not a directory root.", nameof(path));
            if (std && !string.Equals(Path.GetExtension(full), ".std", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("The target must have a .std extension.", nameof(path));
            return full;
        }
        private static OpenStaadAcquisitionOptions CopyOptions(OpenStaadAcquisitionOptions input)
        {
            input = input ?? new OpenStaadAcquisitionOptions();
            if (input.Timeout <= TimeSpan.Zero || input.Timeout > TimeSpan.FromDays(1)) throw new ArgumentOutOfRangeException(nameof(input.Timeout));
            if (input.PollInterval <= TimeSpan.Zero || input.PollInterval > input.Timeout) throw new ArgumentOutOfRangeException(nameof(input.PollInterval));
            if (input.DiscoveryGracePeriod < TimeSpan.Zero || input.DiscoveryGracePeriod > input.Timeout) throw new ArgumentOutOfRangeException(nameof(input.DiscoveryGracePeriod));
            if (!Enum.IsDefined(typeof(OpenStaadDiscoveryPolicy), input.DiscoveryPolicy)) throw new ArgumentException("Unknown discovery policy.");
            return new OpenStaadAcquisitionOptions { Timeout = input.Timeout, PollInterval = input.PollInterval,
                DiscoveryGracePeriod = input.DiscoveryGracePeriod, DiscoveryPolicy = input.DiscoveryPolicy,
                CreateMissingTarget = input.CreateMissingTarget,
                StaadExecutablePath = input.StaadExecutablePath == null ? null : NormalizePath(input.StaadExecutablePath, false) };
        }
        private void CheckThread()
        {
            if (Environment.OSVersion.Platform != PlatformID.Win32NT) throw new PlatformNotSupportedException("OpenSTAAD requires Windows.");
            if (Thread.CurrentThread.ManagedThreadId != ownerThread || Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
                throw new InvalidOperationException("Use this provider exclusively on its owning Windows STA thread.");
            if (disposed) throw new ObjectDisposedException(nameof(PreferExistingOpenStaadWrapperProvider));
        }
        private void CheckIdle() { CheckThread(); if (acquisitionDepth != 0) throw new InvalidOperationException("Registry and lifecycle operations cannot reenter acquisition."); }
        private static void Note(List<string> diagnostics, string message) { if (!diagnostics.Contains(message) && diagnostics.Count < 128) diagnostics.Add(message); }
        private sealed class Observation { internal object Root; internal StaadProcessIdentity Process; internal bool Readable; internal string Path; }
        private sealed class Snapshot { internal IList<StaadProcessIdentity> Processes; internal List<Observation> Observations; internal bool Complete; }
        private sealed class RequestBudget
        {
            private readonly IOpenStaadAcquisitionEnvironment environment;
            private readonly OpenStaadAcquisitionOptions options;
            private readonly CancellationToken cancellation;
            private readonly TimeSpan start;
            internal RequestBudget(IOpenStaadAcquisitionEnvironment environment, OpenStaadAcquisitionOptions options, CancellationToken cancellation)
            { this.environment = environment; this.options = options; this.cancellation = cancellation; start = environment.Now; }
            internal TimeSpan Elapsed => environment.Now - start;
            internal void Check() { cancellation.ThrowIfCancellationRequested(); if (Elapsed >= options.Timeout) throw new TimeoutException(); }
            internal void Pause()
            {
                Check(); TimeSpan remaining = options.Timeout - Elapsed;
                environment.Wait(remaining < options.PollInterval ? remaining : options.PollInterval, cancellation); Check();
            }
        }
    }
}
