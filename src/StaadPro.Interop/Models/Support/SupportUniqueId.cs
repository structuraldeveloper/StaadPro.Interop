using System;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Support;
namespace StaadPro.Interop.Models.Support
{
    /// <summary>Owns a parsed support GUID together with its exact native text.</summary>
    public sealed class SupportUniqueId
    {
        internal SupportUniqueId(int value0, Guid value1, string value2)
        { SupportId=value0; Value=value1; NativeText=value2; }
        /// <summary>Gets the requested positive support item ID; not a node or index.</summary>
        public int SupportId { get; }
        /// <summary>Gets the parsed native GUID; Guid.Empty remains an observation rather than a new generated identity.</summary>
        public Guid Value { get; }
        /// <summary>Gets the exact returned GUID text without case or format normalization.</summary>
        public string NativeText { get; }
    }
}
