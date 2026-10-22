using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StaadPro.Interop.Enums
{
    /// <summary>
    /// Specifies the building enclosure classification under ASCE 7 for internal pressure coefficient (GCpi) determination.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BuildingEnclosureClassification
    {
        /// <summary>
        /// Open building (each wall is at least 80% open).
        /// </summary>
        [Description("Open Building")]
        OpenBuilding = 0,

        /// <summary>
        /// Partially open building (introduced in ASCE 7-2016).
        /// </summary>
        [Description("Partially Open")]
        PartiallyOpen = 1,

        /// <summary>
        /// Partially enclosed building.
        /// </summary>
        [Description("Partially Enclosed")]
        PartiallyEnclosed = 2,

        /// <summary>
        /// Fully enclosed building.
        /// </summary>
        [Description("Enclosed Building")]
        EnclosedBuilding = 3
    }
}
