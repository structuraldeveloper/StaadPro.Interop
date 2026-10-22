using System;
using System.Collections.Generic;
using StaadPro.Interop.Services.Load;

namespace StaadPro.Interop.Models.Loads.Seismic
{
    /// <summary>
    /// Provides strongly-typed parameter definitions for Indian Standard IS 1893-2002/2005 seismic definitions.
    /// </summary>
    public class IS1893_2002SeismicParameters
    {
        /// <summary>Seismic Zone Factor Z (0.10 for Zone II, 0.16 for Zone III, 0.24 for Zone IV, 0.36 for Zone V).</summary>
        public double Zone { get; set; } = 0.16;

        /// <summary>Response Reduction Factor R (RF).</summary>
        public double ResponseReductionFactor { get; set; } = 5.0;

        /// <summary>Importance Factor I.</summary>
        public double ImportanceFactor { get; set; } = 1.0;

        /// <summary>Soil Type SS (1 = Hard, 2 = Medium, 3 = Soft).</summary>
        public int SoilType { get; set; } = 2;

        /// <summary>Structure Type ST (1 = RC Frame, 2 = Steel Frame, 3 = Other).</summary>
        public int StructureType { get; set; } = 1;

        /// <summary>Damping Ratio DM (e.g. 0.05).</summary>
        public double Damping { get; set; } = 0.05;

        /// <summary>Period X direction (seconds).</summary>
        public double PeriodX { get; set; } = 0.0;

        /// <summary>Period Z direction (seconds).</summary>
        public double PeriodZ { get; set; } = 0.0;

        /// <summary>Depth of foundation DT.</summary>
        public double FoundationDepth { get; set; } = 0.0;

        /// <summary>Ground Level GL.</summary>
        public double GroundLevel { get; set; } = 0.0;

        /// <summary>
        /// Converts the parameters to a dictionary of STAAD keyword-value pairs.
        /// </summary>
        /// <returns>A new case-insensitive dictionary of native keywords and finite values; no native calls are made.</returns>
        /// <remarks>Only explicitly supplied nullable values are included. This mapping does not select or verify the active seismic definition. Values use the native code-specific units.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">A numeric value is not finite.</exception>
        public Dictionary<string, double> ToParameterDictionary()
        {
            LoadArguments.Finite(Zone, nameof(Zone));
            LoadArguments.Finite(ResponseReductionFactor, nameof(ResponseReductionFactor));
            LoadArguments.Finite(ImportanceFactor, nameof(ImportanceFactor));
            LoadArguments.Finite(Damping, nameof(Damping));
            LoadArguments.Finite(PeriodX, nameof(PeriodX));
            LoadArguments.Finite(PeriodZ, nameof(PeriodZ));
            LoadArguments.Finite(FoundationDepth, nameof(FoundationDepth));
            LoadArguments.Finite(GroundLevel, nameof(GroundLevel));
            return new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
            {
                ["ZONE"] = Zone,
                ["RF"] = ResponseReductionFactor,
                ["I"] = ImportanceFactor,
                ["SS"] = SoilType,
                ["ST"] = StructureType,
                ["DM"] = Damping,
                ["PX"] = PeriodX,
                ["PZ"] = PeriodZ,
                ["DT"] = FoundationDepth,
                ["GL"] = GroundLevel
            };
        }
    }
}
