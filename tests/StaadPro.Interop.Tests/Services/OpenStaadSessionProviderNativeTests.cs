using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using StaadPro.Interop.Models;
using StaadPro.Interop.Services;
using StaadPro.Interop.Services.Acquisition;

namespace StaadPro.Interop.Tests.Services
{
    [TestFixture, Apartment(ApartmentState.STA), Category("LiveIntegration")]
    public class OpenStaadSessionProviderNativeTests
    {
        [Test, Explicit("Launches one temporary named model in a new licensed STAAD application, then closes only that verified new process.")]
        public void NativeNewNamedTargetHasVerifiedDedicatedOwnership()
        {
            const string executable = @"C:\Program Files\Bentley\Engineering\STAAD.Pro 2026\STAAD\Bentley.Staad.exe";
            if (!File.Exists(executable)) Assert.Ignore("The test installation is unavailable.");
            string directory = Path.Combine(Path.GetTempPath(), "StaadProviderNative-" + Guid.NewGuid().ToString("N"));
            string target = Path.Combine(directory, "provider-test.std");
            var environment = new TrackingEnvironment();
            var before = environment.Inventory().Select(p => p.Key).ToArray();
            try
            {
                using (var provider = new OpenStaadSessionProvider(new OpenStaadAcquisitionOptions
                { StaadExecutablePath = executable, Timeout = TimeSpan.FromSeconds(45) }, environment))
                {
                    var result = provider.Acquire(target);
                    TestContext.WriteLine("Outcome=" + result.Outcome + "; PID=" + result.ProcessId + "; complete=" + result.DiscoveryComplete);
                    Assert.That(result.Succeeded, Is.True, string.Join(Environment.NewLine, result.Diagnostics));
                    using (result.Wrapper)
                    {
                        Assert.That(result.Outcome, Is.EqualTo(OpenStaadAcquisitionOutcome.OpenedTarget));
                        Assert.That(result.ActualPath, Is.EqualTo(target).IgnoreCase);
                        Assert.That(result.Wrapper.IsDedicated, Is.True);
                        Assert.That(result.ProcessId, Is.EqualTo(environment.Launched.Id));
                        Assert.That(before, Does.Not.Contain(environment.Launched.Key));
                        var attached = provider.Acquire(target);
                        using (attached.Wrapper)
                        {
                            Assert.That(attached.Outcome, Is.EqualTo(OpenStaadAcquisitionOutcome.ExistingTarget));
                            Assert.That(attached.ProcessId, Is.EqualTo(result.ProcessId));
                            Assert.That(attached.Wrapper.IsDedicated, Is.False);
                        }
                        // Only this newly created empty test model is closed; no pre-existing root is modified.
                        ((dynamic)result.Wrapper.RawOpenStaad).CloseSTAADFile();
                        int nativeReferenceCount = Convert.ToInt32(((dynamic)result.Wrapper.RawLoad).GetReferenceLoadCaseCount());
                        TestContext.WriteLine("Closed-model reference count=" + nativeReferenceCount);
                        var blank = provider.Acquire(Path.Combine(directory, "not-opened.std"));
                        TestContext.WriteLine("After closing owned empty model: outcome=" + blank.Outcome + "; PID=" + blank.ProcessId);
                        using (blank.Wrapper)
                        {
                            if (nativeReferenceCount < 0)
                            {
                                // A negative native count is an unavailable/error state, not proven empty content.
                                Assert.That(blank.Outcome, Is.EqualTo(OpenStaadAcquisitionOutcome.OpenedTarget));
                                Assert.That(blank.ProcessId, Is.Not.EqualTo(result.ProcessId));
                                Assert.That(blank.Diagnostics.Any(d => d.Contains("reference load count")), Is.True);
                            }
                            else
                            {
                                Assert.That(blank.Outcome, Is.EqualTo(OpenStaadAcquisitionOutcome.ExistingBlank), string.Join(Environment.NewLine, blank.Diagnostics));
                                Assert.That(blank.ProcessId, Is.EqualTo(result.ProcessId));
                                Assert.That(File.Exists(Path.Combine(directory,"not-opened.std")), Is.False);
                            }
                        }
                    }
                }
            }
            finally
            {
                // Never close any process inventoried before the test, or a reused PID.
                foreach (var owned in environment.LaunchedProcesses.Where(p => !before.Contains(p.Key)))
                {
                    try
                    {
                        using (var process = Process.GetProcessById(owned.Id))
                            if (process.StartTime.ToUniversalTime() == owned.StartedUtc)
                            { process.CloseMainWindow(); Assert.That(process.WaitForExit(15000), Is.True, "The owned test application did not close normally."); }
                    }
                    catch (ArgumentException) { /* The owned process already exited. */ }
                }
                environment.Dispose();
                if (Directory.Exists(directory))
                {
                    string resolved = Path.GetFullPath(directory);
                    Assert.That(Path.GetDirectoryName(resolved), Is.EqualTo(Path.GetTempPath().TrimEnd(Path.DirectorySeparatorChar)).IgnoreCase);
                    Assert.That(Path.GetFileName(resolved), Does.StartWith("StaadProviderNative-"));
                    // No recursive deletion: remove only the test directory's immediate files.
                    foreach (var file in Directory.GetFiles(resolved)) File.Delete(file);
                    if (!Directory.EnumerateFileSystemEntries(resolved).Any()) Directory.Delete(resolved, false);
                }
            }
        }
        private sealed class TrackingEnvironment : IOpenStaadAcquisitionEnvironment
        {
            private readonly WindowsOpenStaadEnvironment inner = new WindowsOpenStaadEnvironment();
            internal StaadProcessIdentity Launched;
            internal readonly List<StaadProcessIdentity> LaunchedProcesses = new List<StaadProcessIdentity>();
            public TimeSpan Now => inner.Now;
            public IList<StaadProcessIdentity> Inventory() => inner.Inventory();
            public OpenStaadRootDiscovery Discover() => inner.Discover();
            public void Wait(TimeSpan duration, CancellationToken cancellation) => inner.Wait(duration, cancellation);
            public IDisposable TryCoordinate(string target) => inner.TryCoordinate(target);
            public void ValidateLaunch(string executable) => inner.ValidateLaunch(executable);
            public void PrepareTarget(string target, bool create, Action checkpoint) => inner.PrepareTarget(target, create, checkpoint);
            public StaadProcessIdentity Launch(string executable, string target)
            { Launched = inner.Launch(executable,target); LaunchedProcesses.Add(Launched); return Launched; }
            public void Dispose() => inner.Dispose();
        }
    }
}
