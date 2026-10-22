using StaadPro.Interop.Services.Load;
using System;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Models.Loads.Wind
{
    /// <summary>
    /// Encapsulates parameters for computing the wall wind pressure profile for ASCE 7-2016 in OpenSTAAD.
    /// </summary>
    public class ASCE72016WallWindPressureProfileParameters
    {
        /// <summary>
        /// Gets or sets the basic wind speed (default 85 mph).
        /// </summary>
        public double WindSpeed { get; set; } = 85.0;

        /// <summary>
        /// Gets or sets the ground height above sea level (used for Ke ground elevation factor; default 0.0).
        /// </summary>
        public double HeightAboveSeaLevel { get; set; }

        /// <summary>
        /// Gets or sets the building classification / risk category.
        /// </summary>
        public ASCE7BuildingClassification BuildingClassification { get; set; } = ASCE7BuildingClassification.CategoryII;

        /// <summary>
        /// Gets or sets the structure type (Building, Chimney, Sign, Tower).
        /// </summary>
        /// <summary>Gets the native ASCE structure classification.</summary>
        public ASCE7StructureType StructureType => StructureData?.StructureType ?? ASCE7StructureType.Building;

        /// <summary>
        /// Gets or sets the terrain exposure category.
        /// </summary>
        public ASCE7ExposureCategory ExposureCategory { get; set; } = ASCE7ExposureCategory.ExposureB;

        /// <summary>
        /// Gets or sets whether topographic speed-up over hills or escarpments is considered.
        /// </summary>
        public bool ConsiderEscarpment { get; set; }

        /// <summary>
        /// Gets or sets the units mapping configuration.
        /// </summary>
        public WindUnitsConfiguration Units { get; set; } = WindUnitsConfiguration.CreateImperial();

        /// <summary>
        /// Gets or sets the topographic hill / escarpment data.
        /// </summary>
        public WindEscarpmentData EscarpmentData { get; set; } = new WindEscarpmentData();

        /// <summary>
        /// Gets or sets the structure-specific geometry and dynamic parameters.
        /// </summary>
        public IASCE7StructureData StructureData { get; set; } = new ASCE7BuildingData();

        /// <summary>
        /// Gets or sets the building wall orientation (Windward, Leeward, SideWall).
        /// </summary>
        public ASCE7WallType WallType { get; set; } = ASCE7WallType.Windward;

        /// <summary>
        /// Validates that all parameters are physically valid.
        /// </summary>
        /// <remarks>Rejects nonfinite numeric values and undefined classifications before native use. Does not certify engineering suitability.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">A scalar or classification is invalid.</exception>
        public void Validate()
        {
            LoadArguments.Finite(WindSpeed, nameof(WindSpeed));
            LoadArguments.Finite(HeightAboveSeaLevel, nameof(HeightAboveSeaLevel));
            LoadArguments.Defined(BuildingClassification, nameof(BuildingClassification));
            LoadArguments.Defined(ExposureCategory, nameof(ExposureCategory));
            LoadArguments.Defined(WallType, nameof(WallType));
            if (WindSpeed <= 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(WindSpeed), "Wind speed must be strictly positive.");
            }

            if (HeightAboveSeaLevel < 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(HeightAboveSeaLevel), "Height above sea level cannot be negative.");
            }

            if (Units == null)
            {
                throw new ArgumentNullException(nameof(Units), "Units cannot be null.");
            }

            if (EscarpmentData == null)
            {
                throw new ArgumentNullException(nameof(EscarpmentData), "EscarpmentData cannot be null.");
            }

            if (StructureData == null)
            {
                throw new ArgumentNullException(nameof(StructureData), "StructureData cannot be null.");
            }

            if (ConsiderEscarpment)
            {
                EscarpmentData.Validate();
            }

            Units.ToIntArray();
            EscarpmentData.Validate();
            StructureData.Validate();
        }
    }
}
