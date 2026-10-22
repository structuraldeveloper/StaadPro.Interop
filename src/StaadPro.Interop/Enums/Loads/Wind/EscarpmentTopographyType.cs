using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StaadPro.Interop.Enums
{
    /// <summary>
    /// Specifies the topographic hill or escarpment feature geometry for wind speed-up calculations (Kzt) in STAAD.Pro.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EscarpmentTopographyType
    {
        /// <summary>
        /// 2D Ridge topography.
        /// </summary>
        [Description("2D Ridge")]
        TwoDimensionalRidge = 0,

        /// <summary>
        /// 2D Escarpment topography.
        /// </summary>
        [Description("2D Escarpment")]
        TwoDimensionalEscarpment = 1,

        /// <summary>
        /// 3D Axisymmetrical Hill / Escarpment topography.
        /// </summary>
        [Description("3D Escarpment / Hill")]
        ThreeDimensionalEscarpment = 2
    }
}
