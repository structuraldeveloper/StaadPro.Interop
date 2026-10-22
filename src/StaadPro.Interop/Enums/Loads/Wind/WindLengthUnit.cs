using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StaadPro.Interop.Enums
{
    /// <summary>
    /// Specifies length / dimension units for geometric inputs in OpenSTAAD wind load definitions.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WindLengthUnit
    {
        /// <summary>
        /// Inches (in).
        /// </summary>
        [Description("in")]
        Inches = 0,

        /// <summary>
        /// Feet (ft).
        /// </summary>
        [Description("ft")]
        Feet = 1,

        /// <summary>
        /// Foot (foot).
        /// </summary>
        [Description("foot")]
        Foot = 2,

        /// <summary>
        /// Centimeters (cm).
        /// </summary>
        [Description("cm")]
        Centimeters = 3,

        /// <summary>
        /// Meters (m).
        /// </summary>
        [Description("m")]
        Meters = 4,

        /// <summary>
        /// Millimeters (mm).
        /// </summary>
        [Description("mm")]
        Millimeters = 5,

        /// <summary>
        /// Decimeters (dm).
        /// </summary>
        [Description("dm")]
        Decimeters = 6,

        /// <summary>
        /// Kilometers (km).
        /// </summary>
        [Description("km")]
        Kilometers = 7,

        /// <summary>
        /// Yards (yd).
        /// </summary>
        [Description("yd")]
        Yards = 8,

        /// <summary>
        /// Miles (mi).
        /// </summary>
        [Description("mile")]
        Miles = 9
    }
}
