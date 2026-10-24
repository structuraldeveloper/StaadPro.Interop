namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains a requested native property reference and its exact nonempty unique-ID metadata string.</summary>
    /// <remarks>This metadata is not an analytical entity ID or a created specification ID. No GUID parsing,
    /// global uniqueness check, whitespace normalization or persistence guarantee is implied.</remarks>
    public sealed class PropertyUniqueIdentifier
    {
        internal PropertyUniqueIdentifier(int propertyId, string value) { PropertyId = propertyId; Value = value; }
        /// <summary>Gets the requested positive native property reference number.</summary>
        public int PropertyId { get; }
        /// <summary>Gets the exact nonempty native string, preserving case and whitespace without assuming GUID syntax.</summary>
        public string Value { get; }
    }
}
