using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StaadPro.Interop.Enums
{
    /// <summary>
    /// Specifies the building classification / risk category under ASCE 7 for wind loading in STAAD.Pro.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ASCE7BuildingClassification
    {
        /// <summary>
        /// Category I / Type I structure (low hazard to human life in event of failure).
        /// </summary>
        [Description("Category I")]
        CategoryI = 0,

        /// <summary>
        /// Category II / Type II structure (standard occupancy).
        /// </summary>
        [Description("Category II")]
        CategoryII = 1,

        /// <summary>
        /// Category III / Type III structure (substantial hazard to human life).
        /// </summary>
        [Description("Category III")]
        CategoryIII = 2,

        /// <summary>
        /// Category IV / Type IV structure (essential facilities).
        /// </summary>
        [Description("Category IV")]
        CategoryIV = 3
    }
}
