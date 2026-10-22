using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StaadPro.Interop.Enums
{
    /// <summary>
    /// Specifies the horizontal cross-section geometry of a chimney, tank, or silo for ASCE 7 force coefficient calculations in STAAD.Pro.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TankCrossSectionType
    {
        /// <summary>
        /// Square cross-section with wind normal to face.
        /// </summary>
        [Description("Square (Wind Normal to Face)")]
        Square = 0,

        /// <summary>
        /// Square cross-section with wind along diagonal.
        /// </summary>
        [Description("Square (Wind along Diagonal)")]
        SquareDiagonal = 1,

        /// <summary>
        /// Hexagonal cross-section.
        /// </summary>
        [Description("Hexagonal")]
        Hexagonal = 2,

        /// <summary>
        /// Octagonal non-axisymmetric cross-section (or round in pre-2016 editions).
        /// </summary>
        [Description("Octagonal Non-Axisymmetric")]
        OctagonalNonAxisymmetric = 3,

        /// <summary>
        /// Octagonal axisymmetric cross-section (ASCE 7-2016).
        /// </summary>
        [Description("Octagonal Axisymmetric")]
        OctagonalAxisymmetric = 4,

        /// <summary>
        /// Round non-axisymmetric cross-section (ASCE 7-2016).
        /// </summary>
        [Description("Round Non-Axisymmetric")]
        RoundNonAxisymmetric = 5,

        /// <summary>
        /// Round axisymmetric cross-section (ASCE 7-2016).
        /// </summary>
        [Description("Round Axisymmetric")]
        RoundAxisymmetric = 6
    }
}
