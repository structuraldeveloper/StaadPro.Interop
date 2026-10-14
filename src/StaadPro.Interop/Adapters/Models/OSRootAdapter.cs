using System;
using System.Collections.Generic;
using System.IO;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Services;

namespace StaadPro.Interop.Adapters.Models
{
    /// <summary>Binds a raw OpenSTAAD root object without a build-time Bentley dependency.</summary>
    public sealed class OSRootAdapter : IOSRoot
    {
        private const string UnwrittenFileName = "<StaadPro.Interop.UnwrittenFilename>";
        private readonly Func<IRootPollingClock> pollingClockFactory;
        private readonly IRootModelFiles modelFiles;
        private readonly IRootWindowApi windowApi;
        /// <summary>Initializes a root binding for the supplied OpenSTAAD automation object.</summary>
        /// <param name="openStaad">The non-null raw root object from the model session.</param>
        /// <remarks>The constructor captures the object and does not launch STAAD or validate model readiness.</remarks>
        /// <exception cref="ArgumentNullException"><paramref name="openStaad"/> is null.</exception>
        public OSRootAdapter(object openStaad)
        {
            ComObject = openStaad ?? throw new ArgumentNullException(nameof(openStaad));
            pollingClockFactory = () => new RootPollingClock();
            modelFiles = new RootModelFiles();
            windowApi = new RootWindowApi();
        }

        internal OSRootAdapter(object openStaad, Func<IRootPollingClock> clockFactory) : this(openStaad)
        {
            pollingClockFactory = clockFactory ?? throw new ArgumentNullException(nameof(clockFactory));
        }

        internal OSRootAdapter(object openStaad, Func<IRootPollingClock> clockFactory, IRootModelFiles files) : this(openStaad, clockFactory)
        {
            modelFiles = files ?? throw new ArgumentNullException(nameof(files));
        }

        internal OSRootAdapter(object openStaad, IRootWindowApi windows) : this(openStaad)
        {
            windowApi = windows ?? throw new ArgumentNullException(nameof(windows));
        }

        /// <summary>Gets the captured root OpenSTAAD automation object.</summary>
        /// <value>The original object supplied to the constructor, used for late-bound native access.</value>
        public dynamic ComObject { get; }

        /// <summary>Reads the current model's native STAAD file path from this root session.</summary>
        /// <returns>The exact string reported by OpenSTAAD, including original case, whitespace and Unicode; an empty string when the native API reports no open filename.</returns>
        /// <remarks>
        /// <para>Calls the void GetSTAADFile API once with a VARIANT output and Boolean true
        /// to request the full path. No numeric success status is expected. The BSTR output buffer starts with a string sentinel to detect unwritten results. The output must be
        /// explicitly written as a string; null, non-string and unwritten output throw.</para>
        /// <para>Performs no filesystem lookup, path normalization, model opening/saving, analysis or
        /// STAAD launch. A relative native path is returned unchanged. Use the captured root on the
        /// appropriate COM thread; this object does not certify model readiness or session thread safety.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">GetSTAADFile leaves the output unwritten or returns a non-string value.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native read fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root object lacks a compatible GetSTAADFile signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root:
        /// <code>
        /// Console.WriteLine(root.GetStaadFileFullPath());
        /// </code>
        /// </example>
        public string GetStaadFileFullPath()
        {
            // Native GetSTAADFile expects an output VARIANT already typed as BSTR.
            object fileName = UnwrittenFileName;
            ComObject.GetSTAADFile(ref fileName, true);
            if (!(fileName is string path) || path == UnwrittenFileName)
                throw new InvalidOperationException("OpenSTAAD GetSTAADFile returned an unwritten or non-string path.");
            return path;
        }

        /// <summary>Reads the current STAAD model's filename, including its existing extension.</summary>
        /// <returns>The filename extracted from one native path read, or an empty string for an empty native path.</returns>
        /// <remarks>
        /// <para>Calls GetStaadFileFullPath once, then applies System.IO.Path.GetFileName using
        /// Windows path semantics. The name retains case, Unicode, spaces and multiple periods.
        /// A relative filename is accepted. Native data is not normalized or checked for file existence.</para>
        /// <para>No native write, model activation, file opening or analysis is performed.
        /// Invalid path syntax is delegated to the .NET Framework Path API.
        /// Use the captured root on the correct session COM thread.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">The native string contains invalid path characters for the Windows Path API.</exception>
        /// <exception cref="InvalidOperationException">GetSTAADFile leaves the output unwritten or returns a non-string value.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native read fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root object lacks a compatible GetSTAADFile signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root:
        /// <code>
        /// Console.WriteLine(root.GetStaadFileNameOnly());
        /// </code>
        /// </example>
        public string GetStaadFileNameOnly()
        {
            return Path.GetFileName(GetStaadFileFullPath());
        }

        /// <summary>Derives the expected .ANL analysis-file path from the current model's path.</summary>
        /// <returns>The model directory and filename stem with uppercase .ANL, or an empty string for an empty native model path.</returns>
        /// <remarks>
        /// <para>Reads GetStaadFileFullPath exactly once, removes only the final filename extension,
        /// then combines that stem plus .ANL with the same directory using the Windows Path API.
        /// Relative filenames yield relative analysis-file names. A model-path read does not prove
        /// that the returned analysis file exists or that analysis results are current.</para>
        /// <para>This improves on the reference's two path reads, which could mix model names if
        /// the session changed. No file is created, opened or checked, and no analysis is started.
        /// Native path changes after the read remain possible. Coordinate root access on the session's COM thread.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">The native string contains invalid path characters for the Windows Path API.</exception>
        /// <exception cref="InvalidOperationException">GetSTAADFile leaves the output unwritten or returns a non-string value.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native read fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root object lacks a compatible GetSTAADFile signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root:
        /// <code>
        /// Console.WriteLine(root.GetAnlFileFullPath());
        /// </code>
        /// </example>
        public string GetAnlFileFullPath()
        {
            string path = GetStaadFileFullPath();
            if (path.Length == 0) return string.Empty;
            return Path.Combine(Path.GetDirectoryName(path) ?? string.Empty, Path.GetFileNameWithoutExtension(path) + ".ANL");
        }

        /// <summary>Reads and normalizes the filename of the model currently associated with this root session.</summary>
        /// <returns>The absolute Windows path resolved from one native full-path read, or an empty string when OpenSTAAD explicitly reports no filename.</returns>
        /// <remarks>
        /// <para>Calls GetStaadFileFullPath once, then Path.GetFullPath for nonempty text.
        /// Relative native paths are resolved against the process working directory.
        /// Unlike GetStaadFileFullPath, this method normalizes separators and relative segments.
        /// It preserves path identity without checking file existence, resolving filesystem links,
        /// querying model readiness, or opening/saving a model.</para>
        /// <para>A blank/unnamed native session yields an empty string rather than the reference's
        /// exception from normalizing empty text. Native/path errors still throw and are not treated
        /// as a blank session. COM calls run on the calling thread; coordinate shared-session access.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">A native or requested path contains invalid Windows path syntax; requested paths must contain non-whitespace text.</exception>
        /// <exception cref="NotSupportedException">A path uses a format unsupported by the Windows Path API.</exception>
        /// <exception cref="System.IO.PathTooLongException">A path exceeds the limits enforced by the runtime.</exception>
        /// <exception cref="InvalidOperationException">GetSTAADFile leaves its string output unwritten or returns a non-string value.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native path read fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible GetSTAADFile signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root:
        /// <code>
        /// Console.WriteLine(root.GetOpenedStaadFilename());
        /// </code>
        /// </example>
        public string GetOpenedStaadFilename()
        {
            string path = GetStaadFileFullPath();
            return path.Length == 0 ? string.Empty : Path.GetFullPath(path);
        }

        /// <summary>Checks whether this root session currently identifies the requested model path.</summary>
        /// <param name="staadFileFullPath">Non-null, non-whitespace Windows model path. Relative input is accepted and resolved against the process working directory before native access.</param>
        /// <returns>True when normalized requested and native paths match with ordinal case-insensitive comparison; false for a different path or an explicitly unnamed native session.</returns>
        /// <remarks>
        /// <para>Validates and normalizes the requested path first, then reads GetOpenedStaadFilename
        /// once. Compares complete paths, not just basenames or substring matches. Case and relative
        /// dot segments are normalized by the Windows Path API; filesystem links are not resolved.</para>
        /// <para>This read-only identity predicate does not check Geometry/Load readiness or file
        /// existence, open or switch models, save files, or change active cases/load lists.
        /// A true result describes the session at the read; later native model changes are possible.
        /// Native failures propagate. Call on the appropriate COM thread and serialize session access.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">staadFileFullPath is null; rejected before native access.</exception>
        /// <exception cref="ArgumentException">A native or requested path contains invalid Windows path syntax; requested paths must contain non-whitespace text.</exception>
        /// <exception cref="NotSupportedException">A path uses a format unsupported by the Windows Path API.</exception>
        /// <exception cref="System.IO.PathTooLongException">A path exceeds the limits enforced by the runtime.</exception>
        /// <exception cref="InvalidOperationException">GetSTAADFile leaves its string output unwritten or returns a non-string value.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native path read fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible GetSTAADFile signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root:
        /// <code>
        /// bool matches = root.IsTarget(@"C:\Models\Frame.v2.STD");
        /// Console.WriteLine(matches);
        /// </code>
        /// </example>
        public bool IsTarget(string staadFileFullPath)
        {
            return MatchesOpenedModel(staadFileFullPath, nameof(staadFileFullPath));
        }

        /// <summary>Checks whether the specified model path is the model identified by this root session.</summary>
        /// <param name="fileFullPath">Non-null, non-whitespace Windows model path; relative input is resolved before native access.</param>
        /// <returns>True for a normalized, ordinal case-insensitive full-path match; false for an explicitly unnamed session or a different model, even when basenames are equal.</returns>
        /// <remarks>
        /// <para>Uses the same complete-path identity check as IsTarget and performs exactly one
        /// native filename read after validating input. The reference incorrectly passes the path
        /// as GetSTAADFile's Boolean full-path flag and compares basenames; this port requests
        /// Boolean true and compares normalized complete paths, preventing cross-directory matches.</para>
        /// <para>This describes model identity rather than Geometry/Load readiness or analysis state.
        /// It does not open, close, launch or save STAAD, test file existence, or alter native state.
        /// Relative paths use the process working directory; filesystem links are not resolved.
        /// Native/path failures throw rather than returning false. Coordinate session COM access.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">fileFullPath is null; rejected before native access.</exception>
        /// <exception cref="ArgumentException">A native or requested path contains invalid Windows path syntax; requested paths must contain non-whitespace text.</exception>
        /// <exception cref="NotSupportedException">A path uses a format unsupported by the Windows Path API.</exception>
        /// <exception cref="System.IO.PathTooLongException">A path exceeds the limits enforced by the runtime.</exception>
        /// <exception cref="InvalidOperationException">GetSTAADFile leaves its string output unwritten or returns a non-string value.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native path read fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible GetSTAADFile signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root:
        /// <code>
        /// Console.WriteLine(root.ModelIsOpen(@"C:\Models\Frame.v2.STD"));
        /// </code>
        /// </example>
        public bool ModelIsOpen(string fileFullPath)
        {
            return MatchesOpenedModel(fileFullPath, nameof(fileFullPath));
        }

        /// <summary>Reads the current model's job name, client and originating engineer.</summary>
        /// <returns>A fresh ShortJobInfo snapshot whose three properties contain exact native strings, including empty text, whitespace and Unicode.</returns>
        /// <remarks>
        /// <para>Calls the void GetShortJobInfo API once with three BSTR-initialized reference outputs
        /// in name/client/originator order. Every output must be explicitly written as a string.
        /// Unwritten or non-string responses throw; a failed read never returns a partial record.</para>
        /// <para>Edits to the returned property-notifying model affect managed data only.
        /// No model opening, saving, unit changes or analysis occurs. This is job metadata rather
        /// than readiness or result data. Coordinate the calling thread's COM session access.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">A native output is unwritten or is not a string.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native call fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed root lacks the required native signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root:
        /// <code>
        /// var job = root.GetShortJobInfo();
        /// Console.WriteLine(job.Name + ": " + job.Client + " / " + job.Originator);
        /// </code>
        /// </example>
        public ShortJobInfo GetShortJobInfo()
        {
            string sentinel = CreateJobSentinel();
            object name = sentinel, client = sentinel, originator = sentinel;
            ComObject.GetShortJobInfo(ref name, ref client, ref originator);
            return ShortJobInfo.From(ReadJobText(name, "name", sentinel), ReadJobText(client, "client", sentinel), ReadJobText(originator, "originator", sentinel));
        }

        /// <summary>Reads all thirteen job-information fields and converts the three date fields.</summary>
        /// <returns>A fresh FullJobInfo snapshot. Unspecified native dates map to DateTime.MinValue; text fields preserve their native contents.</returns>
        /// <remarks>
        /// <para>Calls void GetFullJobInfo once with BSTR-initialized reference outputs in native order:
        /// name, client, engineer, engineer date, number, revision, part, reference, checker,
        /// checked date, approver, approved date and comments. All thirteen must be written strings
        /// before mapping into FullJobInfo; invalid output or parsing failure returns no partial model.</para>
        /// <para>Empty/whitespace dates mean unspecified. Nonempty dates follow DateTime.Parse with
        /// the calling thread's CurrentCulture and DateTimeStyles.None, preserving reference culture
        /// behavior; ambiguous dates are not guessed using alternative cultures. Original date text
        /// is not retained by the DateTime properties.</para>
        /// <para>Results own their managed property storage, raise change notifications and perform no
        /// native writes when edited. No file lookup, saving, activation, unit conversion or analysis
        /// occurs. Use the correct COM thread; a metadata snapshot does not certify model readiness.</para>
        /// </remarks>
        /// <exception cref="FormatException">A nonempty native date cannot be parsed using the current culture.</exception>
        /// <exception cref="InvalidOperationException">A native output is unwritten or is not a string.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native call fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed root lacks the required native signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root:
        /// <code>
        /// var job = root.GetFullJobInfo();
        /// Console.WriteLine(job.Number + ": " + job.Checker + " / " + job.Comments);
        /// </code>
        /// </example>
        public FullJobInfo GetFullJobInfo()
        {
            string sentinel = CreateJobSentinel();
            object name = sentinel, client = sentinel, originator = sentinel, originated = sentinel;
            object number = sentinel, revision = sentinel, part = sentinel, reference = sentinel;
            object checker = sentinel, dateChecked = sentinel, approver = sentinel, approved = sentinel, comments = sentinel;
            ComObject.GetFullJobInfo(ref name, ref client, ref originator, ref originated, ref number, ref revision, ref part,
                ref reference, ref checker, ref dateChecked, ref approver, ref approved, ref comments);
            // Validate every native string before any date parsing or model construction.
            string[] fields = { ReadJobText(name, "name", sentinel), ReadJobText(client, "client", sentinel), ReadJobText(originator, "originator", sentinel),
                ReadJobText(originated, "originated", sentinel), ReadJobText(number, "number", sentinel), ReadJobText(revision, "revision", sentinel),
                ReadJobText(part, "part", sentinel), ReadJobText(reference, "reference", sentinel), ReadJobText(checker, "checker", sentinel),
                ReadJobText(dateChecked, "dateChecked", sentinel), ReadJobText(approver, "approver", sentinel), ReadJobText(approved, "approved", sentinel), ReadJobText(comments, "comments", sentinel) };
            return FullJobInfo.From(fields[0], fields[1], fields[2], fields[3], fields[8], fields[9], fields[10], fields[11],
                fields[4], fields[5], fields[6], fields[7], fields[12]);
        }

        /// <summary>Sets the current model's input length and force units using existing enum codes.</summary>
        /// <param name="lengthUnit">Defined LengthInputUnit value: Inch=0, Feet=2, CentiMeter=3, Meter=4, MilliMeter=5, DeciMeter=6 or KiloMeter=7.</param>
        /// <param name="forceUnit">Defined ForceInputUnit value 0 through 7: KiloPound, Pound, KiloGram, MetricTon, Newton, KiloNewton, MegaNewton or DecaNewton.</param>
        /// <returns>This root adapter after the native void call returns without an exception.</returns>
        /// <remarks>
        /// <para>Validates both enums before one SetInputUnits call, sending integer length then
        /// force codes unchanged. The native API is void; no Boolean/numeric status is interpreted
        /// and no read-back is performed. Undefined enum values, including the unnamed length code 1,
        /// are rejected even though the installed vendor documentation lists 1 as another Feet code.</para>
        /// <para>This changes native input-unit state used by subsequent input operations. It does
        /// not rescale existing managed values, convert already-stored results, change the base
        /// unit system, save the model or restore previous units. A native failure may have partial
        /// effects; no rollback is supplied. Coordinate session access on the appropriate COM thread.</para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Either unit value is not defined by its public enum; rejected before native access.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native unit change fails; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed root lacks a compatible SetInputUnits signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root:
        /// <code>
        /// root.SetInputUnitsEx(LengthInputUnit.Meter, ForceInputUnit.KiloNewton);
        /// </code>
        /// </example>
        public IOSRoot SetInputUnitsEx(LengthInputUnit lengthUnit, ForceInputUnit forceUnit)
        {
            if (!Enum.IsDefined(typeof(LengthInputUnit), lengthUnit)) throw new ArgumentOutOfRangeException(nameof(lengthUnit));
            if (!Enum.IsDefined(typeof(ForceInputUnit), forceUnit)) throw new ArgumentOutOfRangeException(nameof(forceUnit));
            ComObject.SetInputUnits((int)lengthUnit, (int)forceUnit);
            return this;
        }

        /// <summary>Sets the STAAD application's silent-mode flag and reports whether the native call completed without throwing.</summary>
        /// <param name="silentMode">Defined SilentMode value: Disable=0 or Enable=1. Invalid values are rejected before native access.</param>
        /// <returns>True if the native call returns without an exception; false if native dispatch throws. This is not the previous flag, proof of the resulting setting, or a general readiness check.</returns>
        /// <remarks>
        /// <para>Validates the enum, then calls SetSilentMode once with its integer code. OpenSTAAD's
        /// native VARIANT return is the previous silent-mode value; this method deliberately ignores
        /// that value and preserves the main project's Boolean call-completion contract. A native
        /// previous value of zero is not a failure. No read-back or diagnostic exception is returned.</para>
        /// <para>This changes application-level warning suppression, which can affect later analysis
        /// and saves. Licence, inaccessible-folder and other error dialogs may still appear. The flag
        /// is not automatically restored. No model is saved or closed and no analysis is started.
        /// Dispatch exceptions, including missing signatures, are caught and return false; a failure
        /// can still have native side effects. Serialize access on the session's appropriate COM thread.</para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">silentMode is not a defined enum value; no native call is made.</exception>
        /// <example>
        /// With a connected IOSRoot named root:
        /// <code>
        /// bool completed = root.SetSilentMode(SilentMode.Enable);
        /// Console.WriteLine(completed);
        /// </code>
        /// </example>
        public bool SetSilentMode(SilentMode silentMode)
        {
            if (!Enum.IsDefined(typeof(SilentMode), silentMode)) throw new ArgumentOutOfRangeException(nameof(silentMode));
            try
            {
                ComObject.SetSilentMode((int)silentMode);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>Requests a native save of the current STAAD model using the specified warning-suppression option.</summary>
        /// <param name="silentMode">Defined SilentMode value: Disable=0 or Enable=1, passed as the native integer save option.</param>
        /// <returns>This root adapter after the native void call returns without throwing. This does not independently verify file persistence or that a user completed a native prompt.</returns>
        /// <remarks>
        /// <para>Validates the enum before one SaveModel call. The native API is void, so no Boolean
        /// or numeric status is interpreted. It saves the current structure through STAAD rather
        /// than writing managed file contents. No separate SetSilentMode call, path query or
        /// automatic analysis is performed; this method does not restore application settings.</para>
        /// <para>The native call can write the model file or display dialogs, particularly for an
        /// unnamed model or an error. Silent mode is not a guarantee of unattended operation.
        /// Exceptions propagate unchanged; a failed call may have partial native effects and no
        /// rollback is provided. Call on the correct COM thread and coordinate shared-session access.</para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">silentMode is not a defined enum value; rejected before native access.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native save fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible SaveModel signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root; this writes through STAAD:
        /// <code>
        /// root.SaveModel(SilentMode.Enable);
        /// </code>
        /// </example>
        public IOSRoot SaveModel(SilentMode silentMode)
        {
            if (!Enum.IsDefined(typeof(SilentMode), silentMode)) throw new ArgumentOutOfRangeException(nameof(silentMode));
            ComObject.SaveModel((int)silentMode);
            return this;
        }

        /// <summary>Requests that STAAD close the model currently associated with this root session.</summary>
        /// <returns>This root adapter after the native void call returns without throwing. The return does not certify that the model is closed or that the root remains ready for further operations.</returns>
        /// <remarks>
        /// <para>Calls CloseSTAADFile once without arguments. No Boolean/numeric status is interpreted,
        /// and no filename/readiness check is performed. The request concerns the current .STD file;
        /// it does not terminate the STAAD application, dispose this adapter or create another model.</para>
        /// <para>This method does not explicitly save unsaved changes or set/restore silent mode.
        /// Native dialogs or user decisions may affect closing and may block the call. Save first
        /// when persistence is required. Previously captured model interfaces can become unusable
        /// after closing; the unchanged adapter reference does not prove their validity. Exceptions
        /// propagate unchanged and no rollback occurs. Serialize calls on the session's COM thread.</para>
        /// </remarks>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native close fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible CloseSTAADFile signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root and any required saving already completed:
        /// <code>
        /// root.CloseSTAADFile();
        /// </code>
        /// </example>
        public IOSRoot CloseSTAADFile()
        {
            ComObject.CloseSTAADFile();
            return this;
        }

        /// <summary>Requests that the captured STAAD application open the specified model file.</summary>
        /// <param name="filePath">Non-null, non-whitespace Windows model path, resolved to an absolute path before dispatch. Relative paths use the process working directory.</param>
        /// <returns>This adapter after the native void OpenSTAADFile call returns without throwing; the return does not verify model identity or readiness.</returns>
        /// <remarks>
        /// <para>Validates and normalizes the path, then sends that full path as a string to exactly
        /// one native call. Does not check file existence, create files, start another STAAD process,
        /// query Geometry/Load, or wait for the requested model to become available.</para>
        /// <para>The call can switch the current model and display native dialogs concerning unsaved
        /// work or inaccessible files. No automatic save or rollback is supplied. Captured interfaces
        /// can change validity after a model switch. Serialize access on the session's COM thread;
        /// native calls can block on dialogs. Use a separate identity/readiness check when required.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">filePath is null; no native call occurs.</exception>
        /// <exception cref="ArgumentException">filePath is empty, whitespace or invalid Windows path syntax.</exception>
        /// <exception cref="NotSupportedException">The path uses an unsupported Windows format.</exception>
        /// <exception cref="System.IO.PathTooLongException">The runtime rejects the path length.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native open fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible OpenSTAADFile signature.</exception>
        /// <example><code>
        /// root.OpenSTAADFile(@"C:\Models\Frame.v2.STD"); // native model-switch request
        /// </code></example>
        public IOSRoot OpenSTAADFile(string filePath)
        {
            string normalized = NormalizeRequestedModelPath(filePath, nameof(filePath));
            ComObject.OpenSTAADFile(normalized);
            return this;
        }

        /// <summary>Requests that the captured STAAD application become visible.</summary>
        /// <returns>This root adapter after the native ShowApplication call returns without throwing; visibility and foreground focus are not independently verified.</returns>
        /// <remarks>
        /// <para>Calls ShowApplication once with no arguments and ignores any native return.
        /// Does not launch another application, open/save/close a model, start analysis, or
        /// explicitly maximize, restore or activate a window through Windows APIs.</para>
        /// <para>Call on the session's appropriate COM thread and coordinate shared access.
        /// Native failures propagate unchanged; no retry, timeout or rollback is supplied.
        /// A successful dispatch does not certify that a model is ready for subsequent operations.</para>
        /// </remarks>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native visibility request fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible ShowApplication signature.</exception>
        /// <example><code>
        /// root.ShowApplication();
        /// </code></example>
        public IOSRoot ShowApplication()
        {
            ComObject.ShowApplication();
            return this;
        }

        /// <summary>Polls this root until its reported full model path matches the requested path or the elapsed-time limit expires.</summary>
        /// <param name="fileFullPath">Non-null, non-whitespace Windows model path, normalized before polling; relative input uses the process working directory.</param>
        /// <param name="timeoutDurationInSeconds">Finite positive timeout in seconds within TimeSpan limits; default 60. Counts native polling time.</param>
        /// <param name="sleepIntervalInSeconds">Finite positive polling interval in seconds within TimeSpan limits; default 0.1. Sleeps are capped by the remaining budget.</param>
        /// <returns>True when a matching path is observed within the logical budget; false on timeout. This does not verify Geometry/Load readiness or analysis results.</returns>
        /// <remarks>
        /// <para>Checks immediately, then reads one native filename per poll with ordinal
        /// case-insensitive full-path comparison. An explicitly blank filename is a nonmatch.
        /// No open/save/close or application launch is performed. Native/read/path errors throw
        /// instead of being treated as a blank session or timeout.</para>
        /// <para>Uses a monotonic elapsed-time budget and synchronous waits on the caller's COM
        /// thread. It does not pump UI messages, offer cancellation or prevent native model changes
        /// after a match. A synchronous native call can exceed the timeout; the budget cannot abort it.
        /// Thread.Sleep resolution can exceed the last remaining interval. Serialize session access.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">fileFullPath is null.</exception>
        /// <exception cref="ArgumentException">The requested path is empty/whitespace, or requested/native text has invalid Windows syntax.</exception>
        /// <exception cref="ArgumentOutOfRangeException">A duration is nonpositive, NaN, infinite or outside supported limits; rejected before native access.</exception>
        /// <exception cref="NotSupportedException">A path format is unsupported.</exception>
        /// <exception cref="System.IO.PathTooLongException">A path exceeds runtime limits.</exception>
        /// <exception cref="InvalidOperationException">Native filename output is unwritten or non-string.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native polling fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible filename API.</exception>
        /// <example><code>
        /// bool identified = root.AwaitModelHandle(@"C:\Models\Frame.v2.STD", 1, 0.01);
        /// Console.WriteLine(identified);
        /// </code></example>
        public bool AwaitModelHandle(string fileFullPath, double timeoutDurationInSeconds = 60, double sleepIntervalInSeconds = 0.1)
        {
            string target = NormalizeRequestedModelPath(fileFullPath, nameof(fileFullPath));
            RootModelPolling.ValidateSeconds(timeoutDurationInSeconds, nameof(timeoutDurationInSeconds));
            RootModelPolling.ValidateSeconds(sleepIntervalInSeconds, nameof(sleepIntervalInSeconds));
            return RootModelPolling.Wait(() => string.Equals(GetOpenedStaadFilename(), target, StringComparison.OrdinalIgnoreCase),
                timeoutDurationInSeconds, sleepIntervalInSeconds, pollingClockFactory());
        }

        /// <summary>Requests opening a model once, then waits for this root to report that exact normalized model path.</summary>
        /// <param name="staadFileFullPath">Non-null, non-whitespace Windows model path, normalized before the native request.</param>
        /// <param name="sleepInterval">Positive polling interval in milliseconds, capped by the remaining budget.</param>
        /// <param name="maxWaitTime">Positive total logical limit in seconds, including the native open call; default 600.</param>
        /// <returns>True when the exact normalized path is observed within the budget; false on timeout. A false result does not undo the open request or prove that the model stayed closed.</returns>
        /// <remarks>
        /// <para>Validates all input before one OpenSTAADFile request, then checks immediately and
        /// polls native full paths with ordinal case-insensitive comparison. Corrects the reference's
        /// basename substring comparison and iteration-count timeout. A similarly named model in
        /// another folder is not a match. It requests opening even if the target was already open.</para>
        /// <para>Model switching can affect unsaved work and captured interfaces; native dialogs may
        /// block. No auto-save, rollback, new process creation or Geometry/Load readiness check occurs.
        /// Uses synchronous caller-thread sleeps without UI pumping or cancellation. A monotonic
        /// budget cannot interrupt a blocked COM call. Native and path failures propagate unchanged.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">staadFileFullPath is null.</exception>
        /// <exception cref="ArgumentException">The requested path is empty/whitespace, or requested/native text has invalid Windows syntax.</exception>
        /// <exception cref="ArgumentOutOfRangeException">sleepInterval or maxWaitTime is nonpositive; no native call occurs.</exception>
        /// <exception cref="NotSupportedException">A path format is unsupported.</exception>
        /// <exception cref="System.IO.PathTooLongException">A path exceeds runtime limits.</exception>
        /// <exception cref="InvalidOperationException">Native filename output is unwritten or non-string.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native open or polling fails; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible open or filename API.</exception>
        /// <example><code>
        /// bool identified = root.OpenStaadModel(@"C:\Models\Frame.v2.STD", 10, 1);
        /// Console.WriteLine(identified);
        /// </code></example>
        public bool OpenStaadModel(string staadFileFullPath, int sleepInterval, int maxWaitTime = 600)
        {
            string target = NormalizeRequestedModelPath(staadFileFullPath, nameof(staadFileFullPath));
            if (sleepInterval <= 0) throw new ArgumentOutOfRangeException(nameof(sleepInterval));
            if (maxWaitTime <= 0) throw new ArgumentOutOfRangeException(nameof(maxWaitTime));
            var clock = pollingClockFactory();
            ComObject.OpenSTAADFile(target);
            return RootModelPolling.Wait(() => string.Equals(GetOpenedStaadFilename(), target, StringComparison.OrdinalIgnoreCase),
                maxWaitTime, sleepInterval / 1000d, clock);
        }

        /// <summary>Requests a silent close only when this root reports a non-whitespace current model filename.</summary>
        /// <remarks>
        /// <para>Reads one full native filename. Explicit empty/whitespace text causes no mutation.
        /// Otherwise calls native SetSilentMode with integer 1, then CloseSTAADFile once. Corrects
        /// the reference's null full-path flag. The previous silent setting is ignored and not restored.</para>
        /// <para>Does not explicitly save unsaved work, verify closing, terminate STAAD or dispose
        /// the wrapper. Native dialogs can block despite silent mode, and captured model interfaces
        /// can become unusable after closing. A filename can change after the read; callers must
        /// coordinate UI/session access on the correct COM thread. Native failures propagate,
        /// and a failure after setting silent mode can leave that setting changed.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">Native filename output is unwritten or non-string.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Filename, silent-mode or close dispatch fails; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible filename, silent-mode or close API.</exception>
        /// <example><code>
        /// root.CloseCurrentStaadModel(); // explicitly requests closing; does not auto-save
        /// </code></example>
        public void CloseCurrentStaadModel()
        {
            if (string.IsNullOrWhiteSpace(GetStaadFileFullPath())) return;
            ComObject.SetSilentMode((int)SilentMode.Enable);
            ComObject.CloseSTAADFile();
        }

        /// <summary>Closes the matching current model, replaces its existing file contents and requests reopening after bounded close/lock-release waiting.</summary>
        /// <param name="filePath">Non-null, non-whitespace Windows path to the existing current model; normalized before dispatch.</param>
        /// <param name="fileContents">Non-null list snapshotted before mutation. Lines are written as UTF-8 without BOM using platform newlines; null entries become empty lines.</param>
        /// <param name="sleepTimeMilliseconds">Positive retry interval in milliseconds; capped by the remaining close/lock-release budget. Default 200.</param>
        /// <param name="maxTimeMilliseconds">Nonnegative close/lock-release wait limit in milliseconds after the close request. Default 5000; zero permits only an immediate check.</param>
        /// <returns>True when replacement completed and the target was identified or its reopen request returned; false for a nonmatching model, wait expiry or an intervening model switch. False can follow native side effects or a completed file replacement.</returns>
        /// <remarks>
        /// <para>Validates input, snapshots lines and requires the current normalized path to match
        /// the target before closing. Rejects missing, directory or read-only targets before mutation.
        /// Sets native silent mode to 1 without restoring it, requests closing, then waits for an
        /// explicitly empty filename and exclusive read/write availability. Only Windows sharing/
        /// lock violations are retried; other filesystem errors propagate.</para>
        /// <para>Publishes a complete exclusively created sibling temporary file using File.Replace,
        /// without truncation fallback. Only owned temporary cleanup is attempted. Rechecks blank
        /// identity before writing and avoids reopening over a different intervening model. The
        /// exclusive probe is a snapshot, not a transaction with external writers or the STAAD UI.
        /// Native and filesystem races remain possible; replacement does not preserve original encoding.</para>
        /// <para>The budget does not bound close/write/open native calls or force-abort COM. No
        /// explicit save, backup, rollback, directory creation, process launch or reopen-readiness
        /// verification occurs. Errors propagate; a failure can leave silent mode enabled, the model
        /// closed or the replacement published. Serialize access on the correct session COM thread.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">filePath or fileContents is null.</exception>
        /// <exception cref="ArgumentException">A requested path is empty/whitespace, or requested/native path syntax is invalid.</exception>
        /// <exception cref="ArgumentOutOfRangeException">sleepTimeMilliseconds is nonpositive or maxTimeMilliseconds is negative.</exception>
        /// <exception cref="NotSupportedException">A path or filesystem operation is unsupported.</exception>
        /// <exception cref="System.IO.IOException">The target is missing/a directory or a non-lock filesystem operation fails; original failures propagate.</exception>
        /// <exception cref="UnauthorizedAccessException">The target is read-only or permissions prevent required access.</exception>
        /// <exception cref="InvalidOperationException">Native filename output is unwritten or non-string.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native identity, silent-mode, close or open dispatch fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a required native signature.</exception>
        /// <example><code>
        /// var editedLines = new System.Collections.Generic.List&lt;string&gt; { "STAAD SPACE", "UNIT METER KN", "FINISH" };
        /// bool requested = root.CloseWriteReopenStaadFile(@"C:\Models\Frame.v2.STD", editedLines);
        /// Console.WriteLine(requested); // explicit file replacement; no reopen-readiness guarantee
        /// </code></example>
        public bool CloseWriteReopenStaadFile(string filePath, List<string> fileContents, int sleepTimeMilliseconds = 200, int maxTimeMilliseconds = 5000)
        {
            string target = NormalizeRequestedModelPath(filePath, nameof(filePath));
            if (fileContents == null) throw new ArgumentNullException(nameof(fileContents));
            if (sleepTimeMilliseconds <= 0) throw new ArgumentOutOfRangeException(nameof(sleepTimeMilliseconds));
            if (maxTimeMilliseconds < 0) throw new ArgumentOutOfRangeException(nameof(maxTimeMilliseconds));
            string[] lines = fileContents.ToArray();
            if (!string.Equals(GetOpenedStaadFilename(), target, StringComparison.OrdinalIgnoreCase)) return false;
            modelFiles.ValidateExistingFile(target);
            ComObject.SetSilentMode((int)SilentMode.Enable);
            ComObject.CloseSTAADFile();
            var clock = pollingClockFactory();
            while (true)
            {
                if (maxTimeMilliseconds > 0 && clock.ElapsedSeconds >= maxTimeMilliseconds / 1000d) return false;
                string current = GetOpenedStaadFilename();
                if (current.Length > 0 && !string.Equals(current, target, StringComparison.OrdinalIgnoreCase)) return false;
                if (current.Length == 0 && !modelFiles.IsLocked(target))
                {
                    if (maxTimeMilliseconds > 0 && clock.ElapsedSeconds > maxTimeMilliseconds / 1000d) return false;
                    break;
                }
                double remaining = maxTimeMilliseconds / 1000d - clock.ElapsedSeconds;
                if (remaining <= 0) return false;
                clock.Sleep(Math.Min(remaining, sleepTimeMilliseconds / 1000d));
            }
            if (GetOpenedStaadFilename().Length > 0) return false;
            modelFiles.WriteAtomically(target, lines);
            string afterWrite = GetOpenedStaadFilename();
            if (afterWrite.Length == 0) ComObject.OpenSTAADFile(target);
            else if (!string.Equals(afterWrite, target, StringComparison.OrdinalIgnoreCase)) return false;
            return true;
        }

        /// <summary>Requests minimizing the main window reported by this OpenSTAAD root after validating its process association.</summary>
        /// <remarks>
        /// <para>Reads GetMainWindowHandle and GetProcessId once each, accepts supported integral
        /// native values, checks a nonzero pointer-sized handle, existing window and matching process,
        /// then posts Windows ShowWindowAsync command 6. Rejects malformed, unavailable or foreign
        /// windows before posting. No model opening/saving/closing or application launch occurs.</para>
        /// <para>The void return means the state request was accepted, not that the final window
        /// state, visibility or focus was observed. Uses asynchronous posting rather than blocking
        /// ShowWindow. HWND/process identity checks are snapshots: windows can be destroyed/recycled
        /// after validation. Native COM reads can still block. Use the correct COM thread and
        /// coordinate UI/session access; no retries, rollback or completion timeout are provided.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">Native handle/process output is malformed, the window is absent/foreign or Windows does not accept the request.</exception>
        /// <exception cref="PlatformNotSupportedException">The default window API is invoked outside Windows.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native handle or process lookup fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible handle/process API.</exception>
        /// <example><code>
        /// bool changeWindowState = false; // set true when an actual STAAD window should be changed
        /// if (changeWindowState) root.MinimizeStaadMainWindow();
        /// </code></example>
        public void MinimizeStaadMainWindow()
        {
            RootWindowControl.Request(ComObject.GetMainWindowHandle(), ComObject.GetProcessId(), 6, windowApi);
        }

        /// <summary>Requests maximizing the main window reported by this OpenSTAAD root after validating its process association.</summary>
        /// <remarks>
        /// <para>Reads GetMainWindowHandle and GetProcessId once each, accepts supported integral
        /// native values, checks a nonzero pointer-sized handle, existing window and matching process,
        /// then posts Windows ShowWindowAsync command 3. Rejects malformed, unavailable or foreign
        /// windows before posting. No model opening/saving/closing or application launch occurs.</para>
        /// <para>The void return means the state request was accepted, not that the final window
        /// state, visibility or focus was observed. Uses asynchronous posting rather than blocking
        /// ShowWindow. HWND/process identity checks are snapshots: windows can be destroyed/recycled
        /// after validation. Native COM reads can still block. Use the correct COM thread and
        /// coordinate UI/session access; no retries, rollback or completion timeout are provided.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">Native handle/process output is malformed, the window is absent/foreign or Windows does not accept the request.</exception>
        /// <exception cref="PlatformNotSupportedException">The default window API is invoked outside Windows.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native handle or process lookup fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible handle/process API.</exception>
        /// <example><code>
        /// bool changeWindowState = false; // set true when an actual STAAD window should be changed
        /// if (changeWindowState) root.MaximizeStaadMainWindow();
        /// </code></example>
        public void MaximizeStaadMainWindow()
        {
            RootWindowControl.Request(ComObject.GetMainWindowHandle(), ComObject.GetProcessId(), 3, windowApi);
        }

        /// <summary>Requests restoring the main window reported by this root after validating its process association.</summary>
        /// <remarks>
        /// <para>Reads native GetMainWindowHandle and GetProcessId once each, validates supported
        /// integral/pointer values and matching window ownership, then posts ShowWindowAsync command
        /// 9. An absent, malformed or foreign window is rejected before posting. No model is changed.</para>
        /// <para>The void return means the restore request was accepted, not that normal window state
        /// or focus was observed. HWND/PID checks are snapshots and handles can be destroyed/recycled
        /// after checking. Native identity reads may block. Coordinate UI/session access on the correct
        /// COM thread; no launch, retry, rollback or completion timeout is provided.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">Handle/process output is malformed, the window is absent/foreign or Windows rejects the request.</exception>
        /// <exception cref="PlatformNotSupportedException">The default window API is invoked outside Windows.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native window/process lookup fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible handle/process API.</exception>
        /// <example><code>
        /// bool changeWindowState = false; // set true when an actual STAAD window should be changed
        /// if (changeWindowState) root.RestoreStaadMainWindow();
        /// </code></example>
        public void RestoreStaadMainWindow()
        {
            RootWindowControl.Request(ComObject.GetMainWindowHandle(), ComObject.GetProcessId(), 9, windowApi);
        }

        /// <summary>Requests analysis of the currently opened STAAD model with explicit engine-dialog and waiting options.</summary>
        /// <param name="silentMode">Defined SilentMode code 0/1 controlling supported engine warnings; no separate application SetSilentMode call is made.</param>
        /// <param name="hiddenMode">Defined HiddenMode code 0/1 controlling the analysis-engine dialog.</param>
        /// <param name="waitMode">Defined WaitAnalysisMode: Return=0 requests immediate return; Wait=1 requests native completion waiting.</param>
        /// <returns>The native integer analysis status as AnalysisResult: -1 terminated, 0 general error, 1 in progress, 2 completed successfully, 3 warnings, 4 errors or 5 not performed. Unrecognized Int32 codes are preserved as undefined enum values.</returns>
        /// <remarks>
        /// <para>Validates all flags before one AnalyzeEx call with integer silent/hidden/wait codes
        /// in that order. Accepts only supported CLR integer status values within Int32 range; null,
        /// Boolean, floating-point, decimal and string responses throw rather than being coerced
        /// into success. Defined native failure/in-progress codes are returned, not converted to exceptions.</para>
        /// <para>This starts native analysis and may generate/change analysis output files and display
        /// dialogs. No managed pre-save, target/readiness check, extra SetSilentMode call, result-file
        /// retrieval, retry, cancellation, timeout or rollback is supplied. Return mode does not mean
        /// completion; wait mode can block. Silent/hidden flags do not guarantee unattended execution.
        /// Serialize calls on the correct COM thread and explicitly inspect the returned status.</para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Any flag is undefined; rejected before analysis dispatch.</exception>
        /// <exception cref="InvalidOperationException">Native status is not a supported integer or cannot fit Int32.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native analysis dispatch fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible AnalyzeEx signature.</exception>
        /// <example><code>
        /// AnalysisResult status = root.AnalyzeEx(SilentMode.Enable, HiddenMode.Enable, WaitAnalysisMode.Wait);
        /// Console.WriteLine(status); // inspect errors/warnings; this requests real analysis
        /// </code></example>
        public AnalysisResult AnalyzeEx(SilentMode silentMode, HiddenMode hiddenMode, WaitAnalysisMode waitMode)
        {
            if (!Enum.IsDefined(typeof(SilentMode), silentMode)) throw new ArgumentOutOfRangeException(nameof(silentMode));
            if (!Enum.IsDefined(typeof(HiddenMode), hiddenMode)) throw new ArgumentOutOfRangeException(nameof(hiddenMode));
            if (!Enum.IsDefined(typeof(WaitAnalysisMode), waitMode)) throw new ArgumentOutOfRangeException(nameof(waitMode));
            object status = ComObject.AnalyzeEx((int)silentMode, (int)hiddenMode, (int)waitMode);
            return RootAnalysisStatus.Read(status);
        }

        private static string NormalizeRequestedModelPath(string path, string parameterName)
        {
            if (path == null) throw new ArgumentNullException(parameterName);
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("The model path must contain text.", parameterName);
            return Path.GetFullPath(path);
        }

        private static string CreateJobSentinel() => "<StaadPro.Interop.UnwrittenJobInfo." + Guid.NewGuid().ToString("N") + ">";

        private static string ReadJobText(object value, string field, string sentinel)
        {
            if (!(value is string text) || text == sentinel)
                throw new InvalidOperationException("OpenSTAAD returned unwritten or non-string job information: " + field + ".");
            return text;
        }

        private bool MatchesOpenedModel(string requestedPath, string parameterName)
        {
            if (requestedPath == null) throw new ArgumentNullException(parameterName);
            if (string.IsNullOrWhiteSpace(requestedPath)) throw new ArgumentException("The model path must contain text.", parameterName);
            string normalized = Path.GetFullPath(requestedPath);
            string opened = GetOpenedStaadFilename();
            return opened.Length != 0 && string.Equals(normalized, opened, StringComparison.OrdinalIgnoreCase);
        }
    }
}
