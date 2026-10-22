using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StaadPro.Interop.Enums
{
    /// <summary>
    /// Specifies the ASCE 7 loading code version used for pre-2016 wall wind pressure profile calculation in STAAD.Pro.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ASCE7LoadingCode
    {
        /// <summary>
        /// ASCE 7-1995 loading code.
        /// </summary>
        [Description("ASCE 7-1995")]
        ASCE7_1995 = 0,

        /// <summary>
        /// ASCE 7-2002 loading code.
        /// </summary>
        [Description("ASCE 7-2002")]
        ASCE7_2002 = 1,

        /// <summary>
        /// ASCE 7-2005 / ASCE 7-2010 loading code.
        /// </summary>
        [Description("ASCE 7-2005 / 2010")]
        ASCE7_2005_2010 = 2
    }
}
