using System;
using StaadPro.Interop.Enums.Property;
namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains an immutable optional Property capability acquisition outcome.</summary>
    /// <typeparam name="TCapability">The separately exposed operation interface.</typeparam>
    /// <remarks>A grant checks version policy and member metadata, not native operation success, licensing, model readiness or engineering validity.</remarks>
    public sealed class PropertyCapabilityResult<TCapability> where TCapability : class
    {
        internal PropertyCapabilityResult(PropertyCapabilityAvailability availability, TCapability capability, Version version, string reason)
        { if ((availability == PropertyCapabilityAvailability.Available) != (capability != null)) throw new ArgumentException("Only available results contain a capability."); Availability = availability; Capability = capability; NativeVersion = version; Reason = reason; }
        /// <summary>Gets the version-policy/member acquisition outcome.</summary>
        public PropertyCapabilityAvailability Availability { get; }
        /// <summary>Gets whether a non-null typed catalog adapter was granted.</summary>
        public bool IsAvailable => Availability == PropertyCapabilityAvailability.Available;
        /// <summary>Gets the granted interface, or null for every unavailable outcome.</summary>
        public TCapability Capability { get; }
        /// <summary>Gets the immutable acquired native version, or null when unknown. Never the host assembly version.</summary>
        public Version NativeVersion { get; }
        /// <summary>Gets the acquisition explanation without fabricating a native domain failure.</summary>
        public string Reason { get; }
    }
    /// <summary>Contains a qualified standard-database origin observation with its requested section reference.</summary>
    public sealed class StandardSectionSource
    {
        internal StandardSectionSource(int propertyId, bool standard) { PropertyId = propertyId; IsStandardDatabase = standard; }
        /// <summary>Gets the requested positive section property reference, not an analytical member ID.</summary>
        public int PropertyId { get; }
        /// <summary>Gets the native origin observation. False is valid nonstandard-source data and does not prove the reference exists.</summary>
        public bool IsStandardDatabase { get; }
    }
    /// <summary>Contains exact native standard-section catalog text with its requested section reference.</summary>
    public sealed class StandardSectionText
    {
        internal StandardSectionText(int propertyId, string text) { PropertyId = propertyId; Text = text; }
        /// <summary>Gets the requested positive native section reference, independently of entity or table IDs.</summary>
        public int PropertyId { get; }
        /// <summary>Gets exact nonempty native text, preserving whitespace/case and without catalog-name normalization.</summary>
        public string Text { get; }
    }
}
