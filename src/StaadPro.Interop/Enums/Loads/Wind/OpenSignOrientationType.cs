using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StaadPro.Interop.Enums
{
    /// <summary>
    /// Specifies the member profile shape for open signs and lattice frameworks in ASCE 7 wind load definitions.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OpenSignOrientationType
    {
        /// <summary>
        /// Flat-sided structural members (angles, channels, tees, etc.).
        /// </summary>
        [Description("Flat Members")]
        Flat = 0,

        /// <summary>
        /// Rounded structural members (pipes, tubes, rods).
        /// </summary>
        [Description("Rounded Members")]
        Rounded = 1
    }
}
