using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StaadPro.Interop.Enums
{
    /// <summary>
    /// Specifies the building wall orientation / surface type for wind pressure calculations in STAAD.Pro.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ASCE7WallType
    {
        /// <summary>
        /// Windward wall surface (facing directly into the oncoming wind).
        /// </summary>
        [Description("Windward Wall")]
        Windward = 0,

        /// <summary>
        /// Leeward wall surface (facing away from the oncoming wind).
        /// </summary>
        [Description("Leeward Wall")]
        Leeward = 1,

        /// <summary>
        /// Sidewall surface (parallel to the oncoming wind direction).
        /// </summary>
        [Description("Side Wall")]
        SideWall = 2
    }
}
