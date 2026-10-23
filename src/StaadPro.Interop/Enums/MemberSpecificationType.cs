using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StaadPro.Interop.Enums
{
    /// <summary>Describes the native axial member specification returned by GetMemberSpecCode.</summary>
    /// <remarks>These codes identify member behavior, not a section property, material, release, offset or analysis result.</remarks>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MemberSpecificationType
    {
        /// <summary>A member not classified by the named axial specifications, native code minus one.</summary>
        Other = -1,
        /// <summary>A truss member, native code zero.</summary>
        Truss = 0,
        /// <summary>A tension-only member, native code one.</summary>
        [Description("Tension Only")]
        TensionOnly = 1,
        /// <summary>A compression-only member, native code two.</summary>
        [Description("Compression Only")]
        CompressionOnly = 2,
        /// <summary>A cable-only member, native code three.</summary>
        [Description("Cable Only")]
        CableOnly = 3,
        /// <summary>A joist member, native code four.</summary>
        Joist = 4
    }
}
