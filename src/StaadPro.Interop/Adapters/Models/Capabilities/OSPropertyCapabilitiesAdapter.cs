using System;
using StaadPro.Interop.Models.Property;
using StaadPro.Interop.Adapters.Interfaces.Capabilities;
using StaadPro.Interop.Services.Property.Capabilities;
namespace StaadPro.Interop.Adapters.Models.Capabilities
{
    internal sealed class OSPropertyCapabilitiesAdapter : IOSPropertyCapabilities
    {
        private readonly PropertyCatalogAccess access;
        internal OSPropertyCapabilitiesAdapter(PropertyCatalogAccess access) { this.access = access; }
        /// <summary>Qualifies and acquires the five newer standard-section catalog observations without invoking domain operations.</summary>
        /// <returns>An immutable available grant only for acquired native 25.00.01.424 or 26.00.00.340 with all five compatible signatures. Otherwise returns a precise unavailable state and null Capability. Disposal/replacement or unexpected metadata transport failures throw.</returns>
        /// <remarks>When to use: acquire the catalog group independently from common Property functions. Uses the acquired native version, never host ApplicationVersion. Passive IDispatch name/type metadata or exact visible managed signatures must establish object returns and the documented zero/one by-value object arguments. One absent or incompatible member withholds the whole group. Metadata qualification is not live runtime certification. Every acquired call rechecks the captured version and receiver under the shared Property gate. No lookup, path probing, model mutation, worker thread or retry occurs.</remarks>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// var grant = property.Capabilities.GetStandardCatalog();
        /// System.Console.WriteLine(grant.Availability);
        /// if (grant.IsAvailable) System.Console.WriteLine(grant.NativeVersion);
        /// </code></example>
        public PropertyCapabilityResult<IOSPropertyStandardCatalog> GetStandardCatalog() => access.Acquire();
    }
}
