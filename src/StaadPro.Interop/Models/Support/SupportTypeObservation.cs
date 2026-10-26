using System;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Support;
namespace StaadPro.Interop.Models.Support
{
    /// <summary>Identifies a node’s native support type without guessing unknown codes.</summary>
    public sealed class SupportTypeObservation
    {
        internal SupportTypeObservation(int value0, int value1, SupportType? value2)
        { NodeId=value0; NativeTypeCode=value1; KnownType=value2; }
        /// <summary>Gets the requested positive analytical node ID.</summary>
        public int NodeId { get; }
        /// <summary>Gets the exact native type code; zero means no support and -1 is not published as successful data.</summary>
        public int NativeTypeCode { get; }
        /// <summary>Gets the known reviewed SupportType, or null for an unrecognized nonnegative native code.</summary>
        public SupportType? KnownType { get; }
    }
}
