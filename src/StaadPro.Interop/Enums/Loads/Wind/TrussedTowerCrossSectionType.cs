using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StaadPro.Interop.Enums
{
    /// <summary>
    /// Specifies the cross-sectional shape of a trussed tower for ASCE 7 wind load definitions in STAAD.Pro.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TrussedTowerCrossSectionType
    {
        /// <summary>
        /// Triangular cross-section trussed tower.
        /// </summary>
        [Description("Triangular Cross-Section")]
        Triangle = 0,

        /// <summary>
        /// Square / rectangular cross-section trussed tower.
        /// </summary>
        [Description("Square Cross-Section")]
        Square = 1
    }
}
