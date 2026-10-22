using System;
using System.Collections.Generic;
using StaadPro.Interop.Services.Load;

namespace StaadPro.Interop.Models.Loads.Seismic
{
    /// <summary>
    /// Provides strongly-typed parameter definitions for Indian Standard IS 1893-2016 seismic definitions.
    /// </summary>
    public class IS1893_2016SeismicParameters
    {
        /// <summary>Seismic Zone Factor Z (0.10 for Zone II, 0.16 for Zone III, 0.24 for Zone IV, 0.36 for Zone V).</summary>
        public double Zone { get; set; } = 0.16;

        /// <summary>Response Reduction Factor R (e.g. 3.0 for OMRF, 5.0 for SMRF).</summary>
        public double ResponseReductionFactor { get; set; } = 5.0;

        /// <summary>Importance Factor I (e.g. 1.0, 1.2, or 1.5).</summary>
        public double ImportanceFactor { get; set; } = 1.0;

        /// <summary>Soil Type SS (1 = Rock / Hard Soil, 2 = Medium Soil, 3 = Soft Soil).</summary>
        public int SoilType { get; set; } = 2;

        /// <summary>Structure Type ST (1 = RC Frame, 2 = Steel Frame, 3 = Other).</summary>
        public int StructureType { get; set; } = 1;

        /// <summary>Damping Ratio DM (fraction, e.g. 0.05 for 5% damping).</summary>
        public double Damping { get; set; } = 0.05;

        /// <summary>Fundamental natural period in X direction PX (seconds, 0 for auto-calculate).</summary>
        public double PeriodX { get; set; } = 0.0;

        /// <summary>Fundamental natural period in Z direction PZ (seconds, 0 for auto-calculate).</summary>
        public double PeriodZ { get; set; } = 0.0;

        /// <summary>Depth of foundation DT below ground level (length units, 0 if at GL).</summary>
        public double FoundationDepth { get; set; } = 0.0;

        /// <summary>Ground Level elevation GL in model coordinates.</summary>
        public double GroundLevel { get; set; } = 0.0;

        /// <summary>Spectral Acceleration SA (if overridden directly).</summary>
        public double? SpectralAcceleration { get; set; }

        /// <summary>Ductility Factor DF.</summary>
        public double? DuctilityFactor { get; set; }

        /// <summary>Height of structure HT.</summary>
        public double? Height { get; set; }

        /// <summary>Base dimension in X direction DX.</summary>
        public double? BaseDimensionX { get; set; }

        /// <summary>Base dimension in Z direction DZ.</summary>
        public double? BaseDimensionZ { get; set; }

        /// <summary>
        /// Converts the parameters to a dictionary of STAAD keyword-value pairs for <c>ModifySeismicDefinitionParams</c>.
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
            if (SpectralAcceleration.HasValue) LoadArguments.Finite(SpectralAcceleration.Value, nameof(SpectralAcceleration));
            if (DuctilityFactor.HasValue) LoadArguments.Finite(DuctilityFactor.Value, nameof(DuctilityFactor));
            if (Height.HasValue) LoadArguments.Finite(Height.Value, nameof(Height));
            if (BaseDimensionX.HasValue) LoadArguments.Finite(BaseDimensionX.Value, nameof(BaseDimensionX));
            if (BaseDimensionZ.HasValue) LoadArguments.Finite(BaseDimensionZ.Value, nameof(BaseDimensionZ));
            var dict = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
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

            if (SpectralAcceleration.HasValue) dict["SA"] = SpectralAcceleration.Value;
            if (DuctilityFactor.HasValue) dict["DF"] = DuctilityFactor.Value;
            if (Height.HasValue) dict["HT"] = Height.Value;
            if (BaseDimensionX.HasValue) dict["DX"] = BaseDimensionX.Value;
            if (BaseDimensionZ.HasValue) dict["DZ"] = BaseDimensionZ.Value;

            return dict;
        }
    }
}
