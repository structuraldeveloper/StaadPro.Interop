using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StaadPro.Interop.Enums
{
    /// <summary>
    /// Specifies the ASCE 7 publication / edition used for wind load definitions in STAAD.Pro.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ASCE7Publication
    {
        /// <summary>
        /// ASCE 7-1995 wind load specification.
        /// </summary>
        [Description("ASCE 7-1995")]
        ASCE7_95 = 0,

        /// <summary>
        /// ASCE 7-2002 wind load specification.
        /// </summary>
        [Description("ASCE 7-2002")]
        ASCE7_02 = 1,

        /// <summary>
        /// ASCE 7-2005 / ASCE 7-2010 wind load specification.
        /// </summary>
        [Description("ASCE 7-2005 / 2010")]
        ASCE7_05_10 = 2,

        /// <summary>
        /// ASCE 7-2016 wind load specification.
        /// </summary>
        [Description("ASCE 7-2016")]
        ASCE7_16 = 3
    }
}
