using System;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Property;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Adapters.Interfaces.Capabilities;
using StaadPro.Interop.Adapters.Models.Capabilities;
namespace StaadPro.Interop.Services.Property.Capabilities
{
    internal sealed class PropertyCatalogAccess
    {
        private readonly OpenStaadWrapper wrapper;
        private readonly PropertyQueryReader queries;
        internal PropertyCatalogAccess(OpenStaadWrapper wrapper, PropertyQueryReader queries) { this.wrapper = wrapper; this.queries = queries; }
        private Version Snapshot() { var v = wrapper.StaadVersion; return v == null || v.Major < 0 || v.Update < 0 || v.Revision < 0 || v.Build < 0 ? null : new Version(v.Major, v.Update, v.Revision, v.Build); }
        internal T Run<T>(Version acquired, Func<dynamic,T> body) => queries.WithBinding(p => {
            if (!Equals(acquired, Snapshot())) throw new InvalidOperationException("The acquired catalog native version changed; reacquire the capability.");
            var result = body(p);
            if (!Equals(acquired, Snapshot())) throw new InvalidOperationException("The acquired catalog native version changed during observation.");
            return result;
        });
        private static PropertyCapabilityResult<IOSPropertyStandardCatalog> Unavailable(PropertyCapabilityAvailability status, Version version, string reason) => new PropertyCapabilityResult<IOSPropertyStandardCatalog>(status, null, version, reason);
        internal PropertyCapabilityResult<IOSPropertyStandardCatalog> Acquire() => queries.WithBinding(p => {
            var version = Snapshot();
            if (version == null) return Unavailable(PropertyCapabilityAvailability.UnknownVersion, null, "The acquired native version is unavailable.");
            if (version.Major != 25 && version.Major != 26) return Unavailable(PropertyCapabilityAvailability.DisabledByVersionPolicy, version, "Catalog APIs require a reviewed 2025/2026 native family.");
            if (!version.Equals(new Version(25, 0, 1, 424)) && !version.Equals(new Version(26, 0, 0, 340))) return Unavailable(PropertyCapabilityAvailability.ContractUnverified, version, "This servicing build has no reviewed exact catalog profile.");
            var inspector = new PropertyDispatchInspector();
            foreach (var contract in PropertyCatalogNativeContract.All) {
                var evidence = inspector.Inspect(p, contract);
                if (evidence != PropertyMemberEvidence.Compatible) return Unavailable(evidence == PropertyMemberEvidence.Absent ? PropertyCapabilityAvailability.NativeMemberAbsent : PropertyCapabilityAvailability.ContractUnverified, version, contract.Name + " is absent or has unqualified signature metadata.");
            }
            if (!Equals(version, Snapshot())) throw new InvalidOperationException("The native version changed during catalog acquisition.");
            return new PropertyCapabilityResult<IOSPropertyStandardCatalog>(PropertyCapabilityAvailability.Available, new OSPropertyStandardCatalogAdapter(this, version), version, "Exact candidate build and compatible catalog metadata permit acquisition; native runtime behavior remains unverified.");
        });
    }
}
