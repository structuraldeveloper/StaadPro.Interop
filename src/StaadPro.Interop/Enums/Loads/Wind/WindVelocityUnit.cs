using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StaadPro.Interop.Enums
{
    /// <summary>
    /// Specifies velocity units for wind speed inputs in OpenSTAAD wind load definitions.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WindVelocityUnit
    {
        /// <summary>
        /// Miles per hour (mph).
        /// </summary>
        [Description("mph")]
        Mph = 0,

        /// <summary>
        /// Meters per second (m/s).
        /// </summary>
        [Description("m/s")]
        MetersPerSecond = 1,

        /// <summary>
        /// Centimeters per second (cm/s).
        /// </summary>
        [Description("cm/s")]
        CentimetersPerSecond = 2,

        /// <summary>
        /// Millimeters per second (mm/s).
        /// </summary>
        [Description("mm/s")]
        MillimetersPerSecond = 3,

        /// <summary>
        /// Kilometers per hour (km/h).
        /// </summary>
        [Description("km/h")]
        KilometersPerHour = 4,

        /// <summary>
        /// Inches per second (in/s).
        /// </summary>
        [Description("in/s")]
        InchesPerSecond = 5,

        /// <summary>
        /// Feet per second (ft/s).
        /// </summary>
        [Description("ft/s")]
        FeetPerSecond = 6,

        /// <summary>
        /// Yards per second (yd/s).
        /// </summary>
        [Description("yd/s")]
        YardsPerSecond = 7
    }
}
