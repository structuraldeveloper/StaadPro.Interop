using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StaadPro.Interop.Enums
{
    /// <summary>
    /// Specifies the ground surface roughness and terrain exposure category under ASCE 7 for wind loading in STAAD.Pro.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ASCE7ExposureCategory
    {
        /// <summary>
        /// Exposure A (large city centers with at least 50% of buildings having height greater than 70 ft).
        /// </summary>
        [Description("Exposure A")]
        ExposureA = 0,

        /// <summary>
        /// Exposure B (urban and suburban areas, wooded areas, or other terrain with numerous closely spaced obstructions).
        /// </summary>
        [Description("Exposure B")]
        ExposureB = 1,

        /// <summary>
        /// Exposure C (open terrain with scattered obstructions, flat open country and grasslands).
        /// </summary>
        [Description("Exposure C")]
        ExposureC = 2,

        /// <summary>
        /// Exposure D (flat, unobstructed areas and water surfaces, including smooth mud flats, salt flats, and unbroken ice).
        /// </summary>
        [Description("Exposure D")]
        ExposureD = 3
    }
}
