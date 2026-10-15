using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Interfaces.Capabilities;
using StaadPro.Interop.Enums.Root;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Root;
using StaadPro.Interop.Services.Root.Capabilities;
using static StaadPro.Interop.Tests.Adapter.RootOptionalTestDoubles;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSRootCapabilityTests
    {
        [TestCase("24.0.2.354", RootCapabilityAvailability.DisabledByVersionPolicy, RootCapabilityAvailability.DisabledByVersionPolicy)]
        [TestCase("25.0.1.424", RootCapabilityAvailability.Available, RootCapabilityAvailability.DisabledByVersionPolicy)]
        [TestCase("26.0.0.340", RootCapabilityAvailability.Available, RootCapabilityAvailability.Available)]
        [TestCase("25.0.1.425", RootCapabilityAvailability.ContractUnverified, RootCapabilityAvailability.DisabledByVersionPolicy)]
        [TestCase("26.0.0.341", RootCapabilityAvailability.ContractUnverified, RootCapabilityAvailability.ContractUnverified)]
        [TestCase("27.0.0.1", RootCapabilityAvailability.DisabledByVersionPolicy, RootCapabilityAvailability.DisabledByVersionPolicy)]
        public void ExactNativeProfilesAndIndependentFeaturePoliciesAreRequired(string version, RootCapabilityAvailability export, RootCapabilityAvailability messages)
        {
            var native = new NativeRoot();
            var metadata = new Metadata { Version = ApplicationVersion.Parse(version) };
            var root = new OSRootAdapter(native, metadata);
            var gateway = root.Capabilities;
            Assert.That(metadata.Reads, Is.Zero, "Construction/property access are capture-only.");
            Assert.That(gateway.GetAdinaExport().Availability, Is.EqualTo(export));
            Assert.That(gateway.GetAnalysisErrorMessages().Availability, Is.EqualTo(messages));
            Assert.That(gateway.GetAnalysisWarningMessages().Availability, Is.EqualTo(messages));
            Assert.That(native.Calls, Is.Empty, "Acquisition must not execute any optional operation or count query.");
        }

        [TestCase(false)] [TestCase(true)]
        public void MissingOrIncompleteVersionGrantsNoInterfaceOrMemberInspection(bool incomplete)
        {
            var metadata = new Metadata { Version = incomplete ? new ApplicationVersion { Major = 26, Build = -1 } : null };
            var inspector = new Inspector();
            var result = new OSRootAdapter(new NativeRoot(), metadata, inspector).Capabilities.GetAdinaExport();
            Assert.That(result.Availability, Is.EqualTo(RootCapabilityAvailability.UnknownVersion));
            Assert.That(result.Capability, Is.Null);
            Assert.That(result.IsAvailable, Is.False);
            Assert.That(result.NativeVersion, Is.Null);
            Assert.That(inspector.Calls, Is.Empty);
        }

        [TestCase(0)] [TestCase(1)] [TestCase(2)]
        public void CandidateYearAloneNeverGrantsOrProbesAnUncoveredBuild(int feature)
        {
            var inspector = new Inspector();
            var root = new OSRootAdapter(new NativeRoot(), new Metadata { Version = ApplicationVersion.Parse("26.1.0.1") }, inspector);
            if (feature == 0) Assert.That(root.Capabilities.GetAdinaExport().Capability, Is.Null);
            if (feature == 1) Assert.That(root.Capabilities.GetAnalysisErrorMessages().Capability, Is.Null);
            if (feature == 2) Assert.That(root.Capabilities.GetAnalysisWarningMessages().Capability, Is.Null);
            Assert.That(inspector.Calls, Is.Empty);
        }

        [TestCase((int)RootMemberEvidence.Absent, RootCapabilityAvailability.NativeMemberAbsent)]
        [TestCase((int)RootMemberEvidence.SignatureUnverified, RootCapabilityAvailability.ContractUnverified)]
        public void IncompleteMetadataNeverReturnsAnAdapter(int evidenceCode, RootCapabilityAvailability expected)
        {
            var inspector = new Inspector();
            inspector.Results["GetAnalysisErrorMessages"] = (RootMemberEvidence)evidenceCode;
            var gateway = new OSRootAdapter(new NativeRoot(), new Metadata(), inspector).Capabilities;
            var errors = gateway.GetAnalysisErrorMessages();
            Assert.That(errors.Availability, Is.EqualTo(expected));
            Assert.That(errors.Capability, Is.Null);
            Assert.That(errors.Reason, Does.Contain("GetAnalysisErrorMessages"));
            Assert.That(gateway.GetAnalysisWarningMessages().IsAvailable, Is.True);
            Assert.That(gateway.GetAdinaExport().IsAvailable, Is.True);
        }

        [Test]
        public void MessageGrantAlsoRequiresTheCountQueryContract()
        {
            var inspector = new Inspector();
            inspector.Results["GetAnalysisStatus"] = RootMemberEvidence.Absent;
            var gateway = new OSRootAdapter(new NativeRoot(), new Metadata(), inspector).Capabilities;
            Assert.That(gateway.GetAnalysisErrorMessages().IsAvailable, Is.False);
            Assert.That(gateway.GetAnalysisWarningMessages().Reason, Does.Contain("GetAnalysisStatus"));
            Assert.That(gateway.GetAdinaExport().IsAvailable, Is.True);
        }

        [Test]
        public void BusyDisconnectedAndLocaleFailuresAreNotCachedAsUnsupported()
        {
            foreach (int hresult in new[] { unchecked((int)0x80010108), unchecked((int)0x80010001), unchecked((int)0x8002000C) })
            {
                var error = new COMException("Metadata failure", hresult);
                var inspector = new Inspector { Failure = error };
                var gateway = new OSRootAdapter(new NativeRoot(), new Metadata(), inspector).Capabilities;
                Assert.That(Assert.Throws<COMException>(() => gateway.GetAdinaExport()), Is.SameAs(error));
                inspector.Failure = null;
                Assert.That(gateway.GetAdinaExport().IsAvailable, Is.True);
                Assert.That(inspector.Calls, Has.Count.EqualTo(2));
            }
        }

        [Test]
        public void NativeVersionFailuresRemainExceptionsWithoutOperationOrInspection()
        {
            var failure = new COMException("Version failure");
            var inspector = new Inspector();
            var native = new NativeRoot();
            var gateway = new OSRootAdapter(native, new Metadata { Failure = failure }, inspector).Capabilities;
            Assert.That(Assert.Throws<COMException>(() => gateway.GetAnalysisWarningMessages()), Is.SameAs(failure));
            Assert.That(inspector.Calls, Is.Empty);
            Assert.That(native.Calls, Is.Empty);
        }

        [Test]
        public void ResultVersionCannotBeChangedByMutatingSourceOrReturnedCopies()
        {
            var metadata = new Metadata();
            var result = new OSRootAdapter(new NativeRoot(), metadata).Capabilities.GetAdinaExport();
            metadata.Version.Build = 999;
            result.NativeVersion.Build = 123;
            Assert.That(result.NativeVersion.Build, Is.EqualTo(340));
            Assert.That(result.IsAvailable, Is.True, "This is a past acquisition snapshot, not live availability.");
            Assert.Throws<InvalidOperationException>(() => result.Capability.ExportModelToAdina(AdinaAxisSystem.Preserve, AdinaLoadCaseHandling.MultipleLoadCases));
        }

        [Test]
        public void CapabilitiesCannotBeObtainedByCastingTheCommonAdapter()
        {
            foreach (var type in new[] { typeof(IOSRootAdinaExport), typeof(IOSRootAnalysisErrorMessages), typeof(IOSRootAnalysisWarningMessages) })
            {
                Assert.That(type.IsAssignableFrom(typeof(OSRootAdapter)), Is.False);
                var implementations = typeof(OSRootAdapter).Assembly.GetTypes().Where(t => t.IsClass && type.IsAssignableFrom(t)).ToArray();
                Assert.That(implementations, Has.Length.EqualTo(1));
                Assert.That(implementations[0].IsVisible, Is.False);
            }
            Assert.That(typeof(IOSRoot).GetMethods().Any(m => m.Name == "GetAnalysisErrorMessages" || m.Name == "ExportModelToAdina"), Is.False);
        }

        [Test]
        public void WrapperUsesNativeSnapshotAndDisposalInvalidatesCapturedGatewayAndGrants()
        {
            var native = new NativeRoot();
            IOSRootCapabilities gateway;
            IOSRootAdinaExport export;
            using (var wrapper = new OpenStaadWrapper(native))
            {
                int reads = native.VersionReads;
                gateway = wrapper.Root.Capabilities;
                export = gateway.GetAdinaExport().Capability;
                Assert.That(native.VersionReads, Is.EqualTo(reads));
                Assert.That(export.ExportModelToAdina(AdinaAxisSystem.YUp, AdinaLoadCaseHandling.TimeHistory).IsSuccessful, Is.True);
                Assert.That(native.VersionReads, Is.EqualTo(reads));
                native.VersionText = "26.0.0.341";
                wrapper.RefreshComObjects();
                Assert.Throws<InvalidOperationException>(() => export.ExportModelToAdina(AdinaAxisSystem.Preserve, AdinaLoadCaseHandling.MultipleLoadCases));
            }
            Assert.Throws<ObjectDisposedException>(() => gateway.GetAdinaExport());
            Assert.Throws<ObjectDisposedException>(() => export.ExportModelToAdina(AdinaAxisSystem.Preserve, AdinaLoadCaseHandling.MultipleLoadCases));
        }

        [Test]
        public void StandaloneChecksItsOwnVersionAndDoesNotRetargetAnotherRoot()
        {
            var first = new NativeRoot();
            var second = new NativeRoot { VersionText = "25.0.1.424" };
            var firstApi = new OSRootAdapter(first).Capabilities.GetAnalysisErrorMessages().Capability;
            var secondApi = new OSRootAdapter(second).Capabilities.GetAdinaExport().Capability;
            secondApi.ExportModelToAdina(AdinaAxisSystem.ZUp, AdinaLoadCaseHandling.TimeHistory);
            firstApi.GetAnalysisErrorMessages("relative.std");
            Assert.That(first.Calls.Any(c => c == "export"), Is.False);
            Assert.That(second.Calls, Is.EqualTo(new[] { "export" }));
            first.VersionText = "26.0.0.341";
            Assert.Throws<InvalidOperationException>(() => firstApi.GetAnalysisErrorMessages("relative.std"));
        }

        public class WrongExport { public long ExportModelToADINA(int axis, int loads) => throw new Exception("Must not invoke"); }
        public class WrongArguments { public int ExportModelToADINA(object axis, object loads) => throw new Exception("Must not invoke"); }
        public class OptionalArguments { public int ExportModelToADINA(int axis, int loads = 0) => throw new Exception("Must not invoke"); }
        public class WrongArray { public int GetAnalysisErrorMessages(string path, ref string[] buffer) => throw new Exception("Must not invoke"); }
        private class InaccessibleExport { public int ExportModelToADINA(int axis, int loads) => throw new Exception("Must not invoke"); }

        [Test]
        public void PublicMethodsOnAnInaccessibleManagedTypeDoNotEstablishCallableMetadata()
        {
            Assert.That(new RootDispatchInspector().Inspect(new InaccessibleExport(), RootNativeContract.Export),
                Is.EqualTo(RootMemberEvidence.SignatureUnverified));
        }

        [TestCase(typeof(WrongExport))] [TestCase(typeof(WrongArguments))] [TestCase(typeof(OptionalArguments))]
        public void ManagedInspectionRequiresExactCompatibleShapeWithoutInvoking(Type type)
        {
            Assert.That(new RootDispatchInspector().Inspect(Activator.CreateInstance(type), RootNativeContract.Export),
                Is.EqualTo(RootMemberEvidence.SignatureUnverified));
        }

        [Test]
        public void ManagedInspectionDistinguishesAbsentMemberAndIncorrectReferenceShape()
        {
            var inspector = new RootDispatchInspector();
            Assert.That(inspector.Inspect(new object(), RootNativeContract.Export), Is.EqualTo(RootMemberEvidence.Absent));
            Assert.That(inspector.Inspect(new WrongArray(), RootNativeContract.Errors), Is.EqualTo(RootMemberEvidence.SignatureUnverified));
            Assert.That(inspector.Inspect(new NativeRoot(), RootNativeContract.Errors), Is.EqualTo(RootMemberEvidence.Compatible));
        }
    }
}
