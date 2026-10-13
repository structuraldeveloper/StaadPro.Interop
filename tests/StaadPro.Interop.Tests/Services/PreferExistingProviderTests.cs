using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using StaadPro.Interop.Models;
using StaadPro.Interop.Services;
using StaadPro.Interop.Services.Acquisition;

namespace StaadPro.Interop.Tests.Services
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class PreferExistingProviderTests
    {
        private const string Target = @"C:\Models\Target.std";
        private const string Other = @"C:\Other\Target.std";
        public class GeometryCom
        {
            public object Nodes = 0, Members = 0, Plates = 0, Solids = 0;
            public object GetNodeCount() => Nodes;
            public object GetMemberCount() => Members;
            public object GetPlateCount() => Plates;
            public object GetSolidCount() => Solids;
        }
        public class LoadCom
        {
            public object Primary = 0, Reference = 0, Combinations = 0;
            public object GetPrimaryLoadCaseCount() => Primary;
            public object GetReferenceLoadCaseCount() => Reference;
            public object GetLoadCombinationCaseCount() => Combinations;
        }
        public class RootCom
        {
            public RootCom(int pid, string path) { Pid = pid; Path = path; }
            public object Pid;
            public string Path;
            public bool WriteFilename = true, BrokenGeometry;
            public int GeometryReads, FilenameReads, BreakGeometryAfter = int.MaxValue;
            public Action<RootCom> OnFilename;
            public GeometryCom G = new GeometryCom();
            public LoadCom L = new LoadCom();
            public GeometryCom Geometry { get { GeometryReads++; if (BrokenGeometry || GeometryReads > BreakGeometryAfter) throw new InvalidOperationException("busy"); return G; } }
            public LoadCom Load => L;
            public object GetProcessId() => Pid;
            public void GetSTAADFile(ref object filename, bool full) { FilenameReads++; OnFilename?.Invoke(this); if (WriteFilename) filename = Path; }
        }
        internal sealed class FakeEnvironment : IOpenStaadAcquisitionEnvironment
        {
            public TimeSpan Now { get; private set; }
            internal readonly List<StaadProcessIdentity> Processes = new List<StaadProcessIdentity>();
            internal readonly OpenStaadRootDiscovery Discovery = new OpenStaadRootDiscovery();
            internal int Launches, Prepares, Discoveries, Inventories, Waits, LeasesReleased;
            internal bool Locked, Disposed;
            internal Exception InventoryError, PrepareError, LaunchError;
            internal Exception LaunchValidationError;
            internal Action OnWait, OnPrepare;
            internal Action<int> OnInventory;
            internal Func<string, StaadProcessIdentity> OnLaunch;
            internal readonly List<string> Events = new List<string>();
            internal StaadProcessIdentity Add(RootCom root)
            {
                var process = new StaadProcessIdentity((int)root.Pid, new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((int)root.Pid));
                Processes.Add(process); Discovery.Roots.Add(root); return process;
            }
            public IList<StaadProcessIdentity> Inventory() { Inventories++; OnInventory?.Invoke(Inventories); if (InventoryError != null) throw InventoryError; return Processes.ToList(); }
            public OpenStaadRootDiscovery Discover() { Discoveries++; Events.Add("discover"); return Discovery; }
            public void Wait(TimeSpan duration, CancellationToken cancellation) { Waits++; Now += duration; OnWait?.Invoke(); cancellation.ThrowIfCancellationRequested(); }
            public IDisposable TryCoordinate(string path) => Locked ? null : new Lease(this);
            public void ValidateLaunch(string executable) { if (LaunchValidationError != null) throw LaunchValidationError; }
            public void PrepareTarget(string path, bool create, Action checkpoint) { Prepares++; Events.Add("prepare"); checkpoint(); OnPrepare?.Invoke(); if (PrepareError != null) throw PrepareError; }
            public StaadProcessIdentity Launch(string executable, string path)
            {
                Launches++; Events.Add("launch"); if (LaunchError != null) throw LaunchError;
                return OnLaunch != null ? OnLaunch(path) : Add(new RootCom(900, path));
            }
            public void Dispose() { Disposed = true; }
            private sealed class Lease : IDisposable
            {
                private readonly FakeEnvironment owner;
                internal Lease(FakeEnvironment owner) { this.owner = owner; }
                public void Dispose() { owner.LeasesReleased++; }
            }
        }
        private static OpenStaadAcquisitionOptions Options(OpenStaadDiscoveryPolicy policy = OpenStaadDiscoveryPolicy.BestEffort) =>
            new OpenStaadAcquisitionOptions { Timeout = TimeSpan.FromMilliseconds(100), PollInterval = TimeSpan.FromMilliseconds(10),
                DiscoveryGracePeriod = TimeSpan.FromMilliseconds(20), DiscoveryPolicy = policy, StaadExecutablePath = @"C:\STAAD\Bentley.Staad.exe" };

        [Test]
        public void MatchingTargetWinsOverEarlierBlankAndRetainsExistingOwnership()
        {
            var env = new FakeEnvironment(); env.Add(new RootCom(1, "")); var root = new RootCom(2, Target); env.Add(root);
            var original = OpenStaadWrapperProvider.Resolver;
            using (var provider = new PreferExistingOpenStaadWrapperProvider(Options(), env))
            using (var wrapper = provider.Get(Target.ToUpperInvariant()))
            { Assert.That(wrapper.RawOpenStaad, Is.SameAs(root)); Assert.That(wrapper.IsDedicated, Is.False); }
            Assert.That(env.Launches, Is.Zero); Assert.That(OpenStaadWrapperProvider.Resolver, Is.SameAs(original));
        }
        [TestCase(1), TestCase(3)]
        public void UsableBlankIsReturnedWithoutOpeningRequestedTarget(int count)
        {
            var env = new FakeEnvironment(); for (int id=1; id<=count; id++) env.Add(new RootCom(id, ""));
            using (var provider = new PreferExistingOpenStaadWrapperProvider(Options(), env))
            {
                var result = provider.Acquire(Target);
                Assert.That(result.Outcome, Is.EqualTo(OpenStaadAcquisitionOutcome.ExistingBlank));
                Assert.That(result.ActualPath, Is.Empty); Assert.That(result.RequestedPath, Is.EqualTo(Target));
                Assert.That(result.ProcessId, Is.EqualTo(1)); Assert.That(result.Wrapper.IsDedicated, Is.False);
                result.Wrapper.Dispose();
            }
            Assert.That(env.Prepares, Is.Zero); Assert.That(env.Launches, Is.Zero);
        }
        [Test]
        public void NoProcessesCreatesAndLaunchesOnceAfterCoordinationRecheck()
        {
            var env = new FakeEnvironment();
            using (var provider = new PreferExistingOpenStaadWrapperProvider(Options(), env))
            {
                var result = provider.Acquire(Target);
                Assert.That(result.Outcome, Is.EqualTo(OpenStaadAcquisitionOutcome.OpenedTarget));
                Assert.That(result.ProcessId, Is.EqualTo(900)); Assert.That(result.Wrapper.IsDedicated, Is.True); result.Wrapper.Dispose();
            }
            Assert.That(env.Launches, Is.EqualTo(1)); Assert.That(env.Prepares, Is.EqualTo(1));
            Assert.That(env.Events.Take(3), Is.EqualTo(new[] { "discover", "discover", "prepare" })); Assert.That(env.LeasesReleased, Is.EqualTo(1));
        }
        [Test]
        public void DifferentDirectorySameFilenameDoesNotMatch()
        {
            var env = new FakeEnvironment(); var other = new RootCom(1, Other); env.Add(other);
            using (var provider = new PreferExistingOpenStaadWrapperProvider(Options(), env))
            {
                var result = provider.Acquire(Target); Assert.That(result.Outcome, Is.EqualTo(OpenStaadAcquisitionOutcome.OpenedTarget));
                Assert.That(result.Wrapper.RawOpenStaad, Is.Not.SameAs(other)); result.Wrapper.Dispose();
            }
            Assert.That(other.Path, Is.EqualTo(Other));
        }
        [TestCase(OpenStaadDiscoveryPolicy.BestEffort), TestCase(OpenStaadDiscoveryPolicy.RequireCompleteDiscovery)]
        public void KnownBusyTargetNeverFallsThroughToBlankOrLaunch(OpenStaadDiscoveryPolicy policy)
        {
            var env = new FakeEnvironment(); env.Add(new RootCom(1, "")); env.Add(new RootCom(2, Target) { BrokenGeometry=true });
            using (var provider = new PreferExistingOpenStaadWrapperProvider(Options(policy), env))
            { var result=provider.Acquire(Target); Assert.That(result.Failure, Is.EqualTo(OpenStaadAcquisitionFailure.Timeout)); Assert.That(result.Wrapper, Is.Null); }
            Assert.That(env.Launches, Is.Zero); Assert.That(env.Prepares, Is.Zero);
        }
        [Test]
        public void BusyTargetCanBecomeReadyWithoutReopening()
        {
            var env = new FakeEnvironment(); var root = new RootCom(1, Target) { BrokenGeometry=true }; env.Add(root); env.OnWait=()=>root.BrokenGeometry=false;
            using (var provider = new PreferExistingOpenStaadWrapperProvider(Options(), env))
            using (var wrapper = provider.Get(Target)) Assert.That(wrapper.RawOpenStaad, Is.SameAs(root));
            Assert.That(env.Launches, Is.Zero);
        }
        [TestCase(OpenStaadDiscoveryPolicy.BestEffort), TestCase(OpenStaadDiscoveryPolicy.RequireCompleteDiscovery)]
        public void UnmappedApplicationsFollowExplicitPolicy(OpenStaadDiscoveryPolicy policy)
        {
            var env=new FakeEnvironment(); env.Processes.Add(new StaadProcessIdentity(10, DateTime.UtcNow));
            using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(policy),env))
            {
                var result=provider.Acquire(Target);
                if(policy==OpenStaadDiscoveryPolicy.BestEffort)
                { Assert.That(result.Succeeded,Is.True); Assert.That(result.DiscoveryComplete,Is.False); result.Wrapper.Dispose(); }
                else { Assert.That(result.Failure,Is.EqualTo(OpenStaadAcquisitionFailure.IncompleteDiscovery)); Assert.That(env.Launches,Is.Zero); }
                Assert.That(result.Diagnostics.Any(d=>d.Contains("unmapped")),Is.True);
            }
            Assert.That(env.Waits,Is.GreaterThanOrEqualTo(2));
        }
        [TestCase(OpenStaadDiscoveryPolicy.BestEffort), TestCase(OpenStaadDiscoveryPolicy.RequireCompleteDiscovery)]
        public void MappedUnwrittenFilenameBlocksFallback(OpenStaadDiscoveryPolicy policy)
        {
            var env=new FakeEnvironment(); env.Add(new RootCom(1,"") { WriteFilename=false });
            using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(policy),env))
                Assert.That(provider.Acquire(Target).Failure,Is.EqualTo(OpenStaadAcquisitionFailure.IdentityUnavailable));
            Assert.That(env.Prepares,Is.Zero); Assert.That(env.Launches,Is.Zero);
        }
        [TestCase(0), TestCase(1), TestCase(2), TestCase(3), TestCase(4), TestCase(5), TestCase(6)]
        public void NonemptyUnnamedSessionIsNeverReused(int component)
        {
            var env=new FakeEnvironment(); var root=new RootCom(1,"");
            if(component==0)root.G.Nodes=1;if(component==1)root.G.Members=1;if(component==2)root.G.Plates=1;if(component==3)root.G.Solids=1;
            if(component==4)root.L.Primary=1;if(component==5)root.L.Reference=1;if(component==6)root.L.Combinations=1;
            env.Add(root);
            using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))
            {var result=provider.Acquire(Target);Assert.That(result.Outcome,Is.EqualTo(OpenStaadAcquisitionOutcome.OpenedTarget));Assert.That(result.Wrapper.RawOpenStaad,Is.Not.SameAs(root));result.Wrapper.Dispose();}
            Assert.That(root.Path,Is.Empty);
        }
        [TestCase(null), TestCase(true), TestCase("0"), TestCase(0.0), TestCase(0.25), TestCase(-1), TestCase(long.MaxValue)]
        public void MalformedNamedReadinessCannotReturnAWrapper(object count)
        {
            var env=new FakeEnvironment();var root=new RootCom(1,Target);root.G.Nodes=count;env.Add(root);
            using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))Assert.That(provider.Acquire(Target).Succeeded,Is.False);
            Assert.That(env.Launches,Is.Zero);
        }
        [Test]
        public void CapturedWrapperInterfaceMustPassReadinessAgain()
        {
            var env=new FakeEnvironment();env.Add(new RootCom(1,Target){BreakGeometryAfter=1});
            using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))Assert.That(provider.Acquire(Target).Succeeded,Is.False);
            Assert.That(env.Launches,Is.Zero);
        }
        [Test]
        public void NegativeReferenceCountDoesNotProveAnUnnamedSessionEmpty()
        {
            var env=new FakeEnvironment();var root=new RootCom(1,"");root.L.Reference=-1;env.Add(root);
            using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))
            {var result=provider.Acquire(Target);Assert.That(result.Outcome,Is.EqualTo(OpenStaadAcquisitionOutcome.OpenedTarget));Assert.That(result.Diagnostics.Any(d=>d.Contains("reference load count")),Is.True);result.Wrapper.Dispose();}
        }
        [Test]
        public void ModelChangeDuringConstructionCannotReturnWrongTarget()
        {
            var env=new FakeEnvironment();var root=new RootCom(1,Target);root.OnFilename=r=>{if(r.FilenameReads==2)r.Path=Other;};env.Add(root);
            using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))
            {var result=provider.Acquire(Target);Assert.That(result.Outcome,Is.EqualTo(OpenStaadAcquisitionOutcome.OpenedTarget));Assert.That(result.Wrapper.RawOpenStaad,Is.Not.SameAs(root));result.Wrapper.Dispose();}
        }
        [Test]
        public void RetainedNamedRootCanBeRevalidatedAfterBecomingBlank()
        {
            var env=new FakeEnvironment();var root=new RootCom(1,Target);env.Add(root);
            using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))
            {using(var first=provider.Get(Target))Assert.That(first.RawOpenStaad,Is.SameAs(root));root.Path="";env.Discovery.Roots.Clear();
                var second=provider.Acquire(Other);Assert.That(second.Outcome,Is.EqualTo(OpenStaadAcquisitionOutcome.ExistingBlank));Assert.That(second.Wrapper.RawOpenStaad,Is.SameAs(root));second.Wrapper.Dispose();}
            Assert.That(env.Launches,Is.Zero);
        }
        [Test]
        public void RegisteredRootWorksWhenItsGlobalRegistrationIsNotVisible()
        {
            var env=new FakeEnvironment();var root=new RootCom(1,"");env.Add(root);env.Discovery.Roots.Clear();
            using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))
            {provider.RegisterRoot(root);var result=provider.Acquire(Target);Assert.That(result.Outcome,Is.EqualTo(OpenStaadAcquisitionOutcome.ExistingBlank));result.Wrapper.Dispose();provider.ClearRetainedRoots();}
        }
        [Test]
        public void PidReuseInvalidatesRetainedRoot()
        {
            var env=new FakeEnvironment();var old=new RootCom(1,Target);var identity=env.Add(old);
            using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))
            {using(var first=provider.Get(Target)){}env.Discovery.Roots.Clear();env.Processes.Clear();env.Processes.Add(new StaadProcessIdentity(1,identity.StartedUtc.AddHours(1)));
                var result=provider.Acquire(Target);Assert.That(result.Outcome,Is.EqualTo(OpenStaadAcquisitionOutcome.OpenedTarget));Assert.That(result.Wrapper.RawOpenStaad,Is.Not.SameAs(old));result.Wrapper.Dispose();}
        }
        [Test]
        public void DuplicateRegistrationsAreOneProcessAndDoNotCreateExtraWrappers()
        {
            var env=new FakeEnvironment();var root=new RootCom(1,Target);env.Add(root);env.Discovery.Roots.Add(root);env.Discovery.Roots.Add(new RootCom(1,Target));
            using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env)){var result=provider.Acquire(Target);Assert.That(result.DiscoveryComplete,Is.True);Assert.That(result.ProcessId,Is.EqualTo(1));result.Wrapper.Dispose();}
        }
        [Test]
        public void FreshReadableAliasSupersedesBrokenRetainedProxy()
        {
            var env=new FakeEnvironment();var root=new RootCom(1,Target);env.Add(root);
            using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))
            {using(var first=provider.Get(Target)){}root.WriteFilename=false;env.Discovery.Roots.Clear();var fresh=new RootCom(1,Target);env.Discovery.Roots.Add(fresh);using(var second=provider.Get(Target))Assert.That(second.RawOpenStaad,Is.SameAs(fresh));}
        }
        [Test]
        public void ConflictingPathsForSameProcessRequireRescan()
        {
            var env=new FakeEnvironment();env.Add(new RootCom(1,Target));env.Discovery.Roots.Add(new RootCom(1,Other));
            using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))Assert.That(provider.Acquire(Target).Failure,Is.EqualTo(OpenStaadAcquisitionFailure.IdentityUnavailable));
            Assert.That(env.Launches,Is.Zero);
        }
        [Test]
        public void CancellationBeforeDiscoveryHasNoSideEffects()
        {
            var env=new FakeEnvironment();using(var source=new CancellationTokenSource())using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))
            {source.Cancel();Assert.That(provider.Acquire(Target,source.Token).Failure,Is.EqualTo(OpenStaadAcquisitionFailure.Cancelled));}
            Assert.That(env.Inventories,Is.Zero);Assert.That(env.Prepares,Is.Zero);Assert.That(env.Launches,Is.Zero);
        }
        [Test]
        public void CancellationAfterPreparationPreventsLateLaunch()
        {
            var env=new FakeEnvironment();using(var source=new CancellationTokenSource())using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))
            {env.OnPrepare=source.Cancel;Assert.That(provider.Acquire(Target,source.Token).Failure,Is.EqualTo(OpenStaadAcquisitionFailure.Cancelled));}
            Assert.That(env.Prepares,Is.EqualTo(1));Assert.That(env.Launches,Is.Zero);Assert.That(env.LeasesReleased,Is.EqualTo(1));
        }
        [Test]
        public void CancellationAfterLaunchPreservesStartupIdentityForDiagnostics()
        {
            var env=new FakeEnvironment();using(var source=new CancellationTokenSource())using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))
            {env.OnLaunch=path=>{var launched=env.Add(new RootCom(900,path));source.Cancel();return launched;};var result=provider.Acquire(Target,source.Token);
                Assert.That(result.Failure,Is.EqualTo(OpenStaadAcquisitionFailure.Cancelled));Assert.That(result.Wrapper,Is.Null);Assert.That(result.LaunchedProcessId,Is.EqualTo(900));Assert.That(result.LaunchedProcessStartTimeUtc,Is.Not.Null);}
            Assert.That(env.Launches,Is.EqualTo(1));
        }
        [Test]
        public void CoordinationTimeoutDoesNotCreateOrLaunch()
        {
            var env=new FakeEnvironment(){Locked=true};using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))Assert.That(provider.Acquire(Target).Failure,Is.EqualTo(OpenStaadAcquisitionFailure.Timeout));
            Assert.That(env.Prepares,Is.Zero);Assert.That(env.Launches,Is.Zero);
        }
        [TestCase(false), TestCase(true)]
        public void MissingOrInvalidLauncherFailsBeforeTargetPreparation(bool invalid)
        {
            var env=new FakeEnvironment();var options=Options();if(invalid)env.LaunchValidationError=new FileNotFoundException("not installed");else options.StaadExecutablePath=null;
            using(var provider=new PreferExistingOpenStaadWrapperProvider(options,env))Assert.That(provider.Acquire(Target).Failure,Is.EqualTo(OpenStaadAcquisitionFailure.LaunchFailed));
            Assert.That(env.Prepares,Is.Zero);Assert.That(env.Launches,Is.Zero);Assert.That(env.LeasesReleased,Is.EqualTo(1));
        }
        [TestCase("inventory"),TestCase("prepare"),TestCase("launch")]
        public void OperationalErrorsAreReportedAndNeverProduceAWrapper(string stage)
        {
            var env=new FakeEnvironment();if(stage=="inventory")env.InventoryError=new UnauthorizedAccessException("inventory denied");if(stage=="prepare")env.PrepareError=new IOException("disk failure");if(stage=="launch")env.LaunchError=new IOException("startup failure");
            using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env)){var result=provider.Acquire(Target);Assert.That(result.Succeeded,Is.False);Assert.That(result.Failure,Is.EqualTo(stage=="inventory"?OpenStaadAcquisitionFailure.DiscoveryFailed:stage=="prepare"?OpenStaadAcquisitionFailure.FilePreparationFailed:OpenStaadAcquisitionFailure.LaunchFailed));Assert.That(result.Diagnostics.Count,Is.GreaterThan(0));}
            Assert.That(env.Launches,Is.LessThanOrEqualTo(1));
        }
        [Test]
        public void PreExistingLaunchIdentityIsRejected()
        {
            var env=new FakeEnvironment();var identity=env.Add(new RootCom(1,Other));env.OnLaunch=path=>identity;
            using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))Assert.That(provider.Acquire(Target).Failure,Is.EqualTo(OpenStaadAcquisitionFailure.LaunchRedirected));Assert.That(env.Launches,Is.EqualTo(1));
        }
        [Test]
        public void TargetOpenedInDifferentProcessIsRejected()
        {
            var env=new FakeEnvironment();env.OnLaunch=path=>{env.Add(new RootCom(901,path));var process=new StaadProcessIdentity(900,DateTime.UtcNow);env.Processes.Add(process);return process;};
            using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))Assert.That(provider.Acquire(Target).Failure,Is.EqualTo(OpenStaadAcquisitionFailure.LaunchRedirected));Assert.That(env.Launches,Is.EqualTo(1));
        }
        [Test]
        public void LaunchedProcessExitDoesNotCauseAnotherLaunch()
        {
            var env=new FakeEnvironment();env.OnLaunch=path=>new StaadProcessIdentity(900,DateTime.UtcNow);
            using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))Assert.That(provider.Acquire(Target).Failure,Is.EqualTo(OpenStaadAcquisitionFailure.LaunchedProcessExited));Assert.That(env.Launches,Is.EqualTo(1));
        }
        [Test]
        public void CoverageChangeDuringBlankValidationBlocksStrictReuse()
        {
            var env=new FakeEnvironment();env.Add(new RootCom(1,""));env.OnInventory=count=>{if(count==2)env.Processes.Add(new StaadProcessIdentity(2,DateTime.UtcNow));};
            using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(OpenStaadDiscoveryPolicy.RequireCompleteDiscovery),env))Assert.That(provider.Acquire(Target).Failure,Is.EqualTo(OpenStaadAcquisitionFailure.IncompleteDiscovery));Assert.That(env.Launches,Is.Zero);
        }
        [Test]
        public void AttachmentOnlyNeverLaunchesAndReportsNoSession()
        {
            var env=new FakeEnvironment();using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))Assert.That(provider.Acquire().Failure,Is.EqualTo(OpenStaadAcquisitionFailure.NoRunningSession));Assert.That(env.Launches,Is.Zero);
        }
        [Test]
        public void DisposingProviderDoesNotDisposeReturnedWrapper()
        {
            var env=new FakeEnvironment();env.Add(new RootCom(1,Target));var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env);var wrapper=provider.Get(Target);provider.Dispose();provider.Dispose();Assert.That(wrapper.IsConnected,Is.True);Assert.That(env.Disposed,Is.True);Assert.Throws<ObjectDisposedException>(()=>provider.Get(Target));wrapper.Dispose();
        }
        [Test]
        public void WrongThreadAndReentrancyAreRejected()
        {
            var env=new FakeEnvironment();env.Add(new RootCom(1,Target){BrokenGeometry=true});using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))
            {Exception error=null;var thread=new Thread(()=>{try{provider.Get(Target);}catch(Exception ex){error=ex;}});thread.Start();thread.Join();Assert.That(error,Is.TypeOf<InvalidOperationException>());
                env.OnWait=()=>{Assert.Throws<InvalidOperationException>(()=>provider.Get(Target));Assert.Throws<InvalidOperationException>(()=>provider.ClearRetainedRoots());Assert.Throws<InvalidOperationException>(()=>provider.Dispose());};Assert.That(provider.Acquire(Target).Succeeded,Is.False);}
        }
        [TestCase("relative.std"),TestCase("C:relative.std"),TestCase(@"\Models\file.std"),TestCase("/Models/file.std"),TestCase(""),TestCase(" "),TestCase(@"C:\Models\file.txt"),TestCase(@"C:\Models\NUL.std"),TestCase(@"C:\Models\file:stream.std"),TestCase(@"C:\Models\wild*.std"),TestCase(@"C:\Models\bad .std ")]
        public void InvalidTargetIsRejectedBeforeDiscovery(string target)
        {
            var env=new FakeEnvironment();using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))Assert.Throws<ArgumentException>(()=>provider.Acquire(target));Assert.That(env.Inventories,Is.Zero);Assert.That(env.Prepares,Is.Zero);
        }
        [Test]
        public void OptionsAreCopiedAndDiagnosticsImmutable()
        {
            var env=new FakeEnvironment();env.Add(new RootCom(1,Target));var options=Options();using(var provider=new PreferExistingOpenStaadWrapperProvider(options,env))
            {options.Timeout=TimeSpan.Zero;var result=provider.Acquire(Target);Assert.That(result.Succeeded,Is.True);Assert.Throws<NotSupportedException>(()=>((IList<string>)result.Diagnostics).Add("mutate"));result.Wrapper.Dispose();}
        }
        [Test]
        public void BoundedDiagnosticsAlwaysRetainFinalFailureReason()
        {
            var env=new FakeEnvironment();for(int i=0;i<150;i++)env.Discovery.Diagnostics.Add("discovery note "+i);
            using(var provider=new PreferExistingOpenStaadWrapperProvider(Options(),env))
            {var result=provider.Acquire();Assert.That(result.Failure,Is.EqualTo(OpenStaadAcquisitionFailure.NoRunningSession));Assert.That(result.Diagnostics.Count,Is.EqualTo(128));Assert.That(result.Diagnostics.Last(),Does.Contain("Attachment-only"));}
        }
        [Test]
        public void InvalidTimingOptionsAreRejected()
        {
            var options=Options();options.PollInterval=TimeSpan.Zero;Assert.Throws<ArgumentOutOfRangeException>(()=>new PreferExistingOpenStaadWrapperProvider(options,new FakeEnvironment()));options=Options();options.DiscoveryGracePeriod=TimeSpan.FromDays(2);Assert.Throws<ArgumentOutOfRangeException>(()=>new PreferExistingOpenStaadWrapperProvider(options,new FakeEnvironment()));
        }
    }
}
