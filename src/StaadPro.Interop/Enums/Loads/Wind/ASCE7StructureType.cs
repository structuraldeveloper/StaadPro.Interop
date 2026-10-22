using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StaadPro.Interop.Enums
{
    /// <summary>
    /// Specifies the structure type under ASCE 7 for wind load definition generation in STAAD.Pro.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ASCE7StructureType
    {
        /// <summary>
        /// Regular building structures.
        /// </summary>
        [Description("Building Structures")]
        Building = 0,

        /// <summary>
        /// Chimneys, tanks, silos, and similar cylindrical / polygonal structures.
        /// </summary>
        [Description("Chimney, Tank and Similar Structures")]
        ChimneyOrTank = 1,

        /// <summary>
        /// Solid signs.
        /// </summary>
        [Description("Solid Signs")]
        SolidSign = 2,

        /// <summary>
        /// Open signs.
        /// </summary>
        [Description("Open Signs")]
        OpenSign = 3,

        /// <summary>
        /// Lattice frameworks.
        /// </summary>
        [Description("Lattice Framework")]
        LatticeFramework = 4,

        /// <summary>
        /// Trussed towers.
        /// </summary>
        [Description("Trussed Tower")]
        TrussedTower = 5
    }
}
