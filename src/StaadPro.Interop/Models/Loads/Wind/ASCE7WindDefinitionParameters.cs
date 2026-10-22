using StaadPro.Interop.Services.Load;
using System;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Models.Loads.Wind
{
    /// <summary>
    /// Encapsulates the complete set of parameters required to generate an ASCE 7 wind definition via OpenSTAAD.
    /// </summary>
    public class ASCE7WindDefinitionParameters
    {
        /// <summary>
        /// Gets or sets the wind definition reference type number ID (1, 2, ...).
        /// </summary>
        public int TypeNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the ASCE 7 publication code (ASCE 7-95, 7-02, 7-05/10, 7-16).
        /// </summary>
        public ASCE7Publication Publication { get; set; } = ASCE7Publication.ASCE7_16;

        /// <summary>
        /// Gets or sets the basic wind speed (V).
        /// </summary>
        public double WindSpeed { get; set; } = 85.0;

        /// <summary>
        /// Gets or sets the ground height above sea level (used for ASCE 7-2016 Ke factor; set 0.0 for earlier editions).
        /// </summary>
        public double HeightAboveSeaLevel { get; set; }

        /// <summary>
        /// Gets or sets the building classification / risk category.
        /// </summary>
        public ASCE7BuildingClassification BuildingClassification { get; set; } = ASCE7BuildingClassification.CategoryII;

        /// <summary>
        /// Gets or sets the exposure category (Exposure A, B, C, or D).
        /// </summary>
        public ASCE7ExposureCategory ExposureCategory { get; set; } = ASCE7ExposureCategory.ExposureB;

        /// <summary>
        /// Gets or sets whether topographic speed-up effects over hills or escarpments are considered.
        /// </summary>
        public bool ConsiderEscarpment { get; set; }

        /// <summary>
        /// Gets or sets the building wall orientation to generate wind load on (Windward, Leeward, or SideWall).
        /// </summary>
        public ASCE7WallType WallType { get; set; } = ASCE7WallType.Windward;

        /// <summary>
        /// Gets or sets whether the structure is dynamically flexible (true) or rigid (false).
        /// </summary>
        public bool IsFlexible { get; set; }

        /// <summary>
        /// Gets or sets the topographic escarpment / hill parameters.
        /// </summary>
        public WindEscarpmentData EscarpmentData { get; set; } = new WindEscarpmentData();

        /// <summary>
        /// Gets or sets the structure-specific geometry and dynamic properties (Building, Tank, Sign, Tower).
        /// </summary>
        public IASCE7StructureData StructureData { get; set; } = new ASCE7BuildingData();

        /// <summary>
        /// Gets or sets the units mapping for speed and dimensions.
        /// </summary>
        public WindUnitsConfiguration Units { get; set; } = WindUnitsConfiguration.CreateImperial();

        /// <summary>
        /// Gets or sets the factor calculation flags and overrides (Kz, Kzt, I, Kd, Ke, G, Cp, Gcpi).
        /// </summary>
        public WindFactorsConfiguration Factors { get; set; } = new WindFactorsConfiguration();

        /// <summary>
        /// Validates that all parameters, embedded geometry, and units are valid.
        /// </summary>
        /// <remarks>Rejects nonfinite numeric values and undefined classifications before native use. Does not certify engineering suitability.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">A scalar or classification is invalid.</exception>
        public void Validate()
        {
            LoadArguments.Defined(Publication, nameof(Publication));
            LoadArguments.Finite(WindSpeed, nameof(WindSpeed));
            LoadArguments.Finite(HeightAboveSeaLevel, nameof(HeightAboveSeaLevel));
            LoadArguments.Defined(BuildingClassification, nameof(BuildingClassification));
            LoadArguments.Defined(ExposureCategory, nameof(ExposureCategory));
            LoadArguments.Defined(WallType, nameof(WallType));
            if (TypeNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(TypeNumber), "Wind definition TypeNumber must be greater than zero.");
            }

            if (WindSpeed <= 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(WindSpeed), "Wind speed must be strictly positive.");
            }

            if (HeightAboveSeaLevel < 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(HeightAboveSeaLevel), "Height above sea level cannot be negative.");
            }

            if (EscarpmentData == null)
            {
                throw new ArgumentNullException(nameof(EscarpmentData), "EscarpmentData cannot be null.");
            }

            if (StructureData == null)
            {
                throw new ArgumentNullException(nameof(StructureData), "StructureData cannot be null.");
            }

            if (Units == null)
            {
                throw new ArgumentNullException(nameof(Units), "Units cannot be null.");
            }

            if (Factors == null)
            {
                throw new ArgumentNullException(nameof(Factors), "Factors cannot be null.");
            }

            if (ConsiderEscarpment)
            {
                EscarpmentData.Validate();
            }

            Units.ToIntArray();
            Factors.ToFactorValuesArray();
            EscarpmentData.Validate();
            StructureData.Validate();
        }
    }
}
