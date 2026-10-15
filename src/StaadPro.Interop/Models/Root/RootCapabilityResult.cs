using System;
using StaadPro.Interop.Enums.Root;

namespace StaadPro.Interop.Models.Root
{
    /// <summary>Contains the typed result of acquiring one optional Root feature.</summary>
    /// <typeparam name="TCapability">The separately exposed operation interface.</typeparam>
    /// <remarks>A grant verifies the configured build policy and member metadata, not model readiness,
    /// licensing, operation success or runtime compatibility. Unavailable results never contain an adapter.
    /// Public interface declarations remain visible in assembly metadata; acquisition is a typed access boundary.</remarks>
    public sealed class RootCapabilityResult<TCapability> where TCapability : class
    {
        private readonly Version nativeVersion;
        internal RootCapabilityResult(RootCapabilityAvailability availability, TCapability capability, Version nativeVersion, string reason)
        {
            if ((availability == RootCapabilityAvailability.Available) != (capability != null))
                throw new ArgumentException("Only available results may contain a capability.", nameof(capability));
            Availability = availability; Capability = capability; this.nativeVersion = nativeVersion; Reason = reason;
        }
        /// <summary>Gets the acquisition outcome.</summary>
        /// <value>A precise version-policy, member or grant state.</value>
        public RootCapabilityAvailability Availability { get; }
        /// <summary>Gets whether a typed adapter was granted.</summary>
        /// <value>True exactly when Availability is Available and Capability is non-null.</value>
        public bool IsAvailable => Availability == RootCapabilityAvailability.Available;
        /// <summary>Gets the acquired operation interface.</summary>
        /// <value>Non-null only on an available result; null for every unavailable outcome.</value>
        public TCapability Capability { get; }
        /// <summary>Gets an independent copy of the observed native STAAD version.</summary>
        /// <value>A native version snapshot, or null when unavailable; never the host application version.</value>
        public ApplicationVersion NativeVersion => nativeVersion == null ? null : ApplicationVersion.Parse(nativeVersion.ToString());
        /// <summary>Gets the acquisition explanation.</summary>
        /// <value>A nonempty policy or metadata explanation, without a guessed native operation failure.</value>
        public string Reason { get; }
    }
}
