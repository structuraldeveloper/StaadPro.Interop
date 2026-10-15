using System;
using StaadPro.Interop.Adapters.Interfaces.Capabilities;
using StaadPro.Interop.Enums.Root;
using StaadPro.Interop.Models.Root;
using StaadPro.Interop.Services.Root.Capabilities;
using StaadPro.Interop.Services.Root.Metadata;
namespace StaadPro.Interop.Adapters.Models.Capabilities
{
    internal sealed class OSRootCapabilitiesAdapter : IOSRootCapabilities
    {
        private readonly object root;
        private readonly IRootMetadataAccess metadata;
        private readonly IRootMemberInspector inspector;
        internal OSRootCapabilitiesAdapter(object root, IRootMetadataAccess metadata, IRootMemberInspector inspector)
        { this.root = root; this.metadata = metadata; this.inspector = inspector; }

        public RootCapabilityResult<IOSRootAdinaExport> GetAdinaExport() =>
            Acquire<IOSRootAdinaExport>(RootNativeContract.Export, true, b => new OSRootAdinaExportAdapter(b));
        public RootCapabilityResult<IOSRootAnalysisErrorMessages> GetAnalysisErrorMessages() =>
            Acquire<IOSRootAnalysisErrorMessages>(RootNativeContract.Errors, false, b => new OSRootAnalysisErrorMessagesAdapter(b));
        public RootCapabilityResult<IOSRootAnalysisWarningMessages> GetAnalysisWarningMessages() =>
            Acquire<IOSRootAnalysisWarningMessages>(RootNativeContract.Warnings, false, b => new OSRootAnalysisWarningMessagesAdapter(b));

        private RootCapabilityResult<T> Acquire<T>(RootNativeContract contract, bool export, Func<RootCapabilityBinding, T> factory)
            where T : class
        {
            var observed = RootCapabilityBinding.Freeze(metadata.GetApplicationVersion());
            if (observed == null) return Unavailable<T>(RootCapabilityAvailability.UnknownVersion, null, "The acquired native version is unavailable or incomplete.");
            if (!RootCapabilityProfiles.IsCandidateYear(observed, export))
                return Unavailable<T>(RootCapabilityAvailability.DisabledByVersionPolicy, observed, "This product family is outside the reviewed feature policy.");
            if (!RootCapabilityProfiles.IsCoveredBuild(observed))
                return Unavailable<T>(RootCapabilityAvailability.ContractUnverified, observed, "This servicing build has no exact candidate contract profile.");
            var evidence = inspector.Inspect(root, contract);
            if (evidence != RootMemberEvidence.Compatible) return Missing<T>(evidence, observed, contract.Name);
            if (!export)
            {
                evidence = inspector.Inspect(root, RootNativeContract.AnalysisStatus);
                if (evidence != RootMemberEvidence.Compatible) return Missing<T>(evidence, observed, RootNativeContract.AnalysisStatus.Name);
            }
            var capability = factory(new RootCapabilityBinding(root, metadata, observed));
            return new RootCapabilityResult<T>(RootCapabilityAvailability.Available, capability, observed,
                "The exact candidate build and compatible member metadata permit acquisition; native runtime behavior remains unverified.");
        }

        private static RootCapabilityResult<T> Missing<T>(RootMemberEvidence evidence, Version version, string member) where T : class =>
            Unavailable<T>(evidence == RootMemberEvidence.Absent ? RootCapabilityAvailability.NativeMemberAbsent :
                RootCapabilityAvailability.ContractUnverified, version, member + (evidence == RootMemberEvidence.Absent ?
                " is absent." : " has unavailable or incompatible signature metadata."));
        private static RootCapabilityResult<T> Unavailable<T>(RootCapabilityAvailability status, Version version, string reason) where T : class =>
            new RootCapabilityResult<T>(status, null, version, reason);
    }
}
