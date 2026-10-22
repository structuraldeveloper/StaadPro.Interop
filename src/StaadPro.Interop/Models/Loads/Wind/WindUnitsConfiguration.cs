using StaadPro.Interop.Services.Load;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Models.Loads.Wind
{
    /// <summary>
    /// Encapsulates the complete units mapping for wind velocity and geometric dimensions passed to OpenSTAAD.
    /// </summary>
    public class WindUnitsConfiguration
    {
        /// <summary>
        /// Gets or sets the velocity unit for wind speed (Index 0).
        /// </summary>
        public WindVelocityUnit WindSpeedUnit { get; set; } = WindVelocityUnit.Mph;

        /// <summary>
        /// Gets or sets the length unit for ground height above sea level (Index 1).
        /// </summary>
        public WindLengthUnit HeightAboveSeaLevelUnit { get; set; } = WindLengthUnit.Feet;

        /// <summary>
        /// Gets or sets the length unit for escarpment / hill height H (Index 2).
        /// </summary>
        public WindLengthUnit EscarpmentHeightUnit { get; set; } = WindLengthUnit.Feet;

        /// <summary>
        /// Gets or sets the length unit for escarpment upwind distance Lh (Index 3).
        /// </summary>
        public WindLengthUnit EscarpmentLhUnit { get; set; } = WindLengthUnit.Feet;

        /// <summary>
        /// Gets or sets the length unit for escarpment crest-to-structure distance x (Index 4).
        /// </summary>
        public WindLengthUnit EscarpmentXUnit { get; set; } = WindLengthUnit.Feet;

        /// <summary>
        /// Gets or sets the length unit for the structure height (Index 5).
        /// </summary>
        public WindLengthUnit StructureHeightUnit { get; set; } = WindLengthUnit.Feet;

        /// <summary>
        /// Gets or sets the length unit for the primary structure plan dimension (Length L, Width W, Dim M) (Index 6).
        /// </summary>
        public WindLengthUnit StructureDimension1Unit { get; set; } = WindLengthUnit.Feet;

        /// <summary>
        /// Gets or sets the length unit for the secondary structure plan dimension (Width B, Depth D', Dim N, Member Diameter) (Index 7).
        /// </summary>
        public WindLengthUnit StructureDimension2Unit { get; set; } = WindLengthUnit.Feet;

        /// <summary>
        /// Creates a unit configuration with standard Imperial units (mph for speed, feet for all lengths).
        /// </summary>
        /// <returns>A new mutable configuration using mph and feet.</returns>
        public static WindUnitsConfiguration CreateImperial()
        {
            return new WindUnitsConfiguration
            {
                WindSpeedUnit = WindVelocityUnit.Mph,
                HeightAboveSeaLevelUnit = WindLengthUnit.Feet,
                EscarpmentHeightUnit = WindLengthUnit.Feet,
                EscarpmentLhUnit = WindLengthUnit.Feet,
                EscarpmentXUnit = WindLengthUnit.Feet,
                StructureHeightUnit = WindLengthUnit.Feet,
                StructureDimension1Unit = WindLengthUnit.Feet,
                StructureDimension2Unit = WindLengthUnit.Feet
            };
        }

        /// <summary>
        /// Creates a unit configuration with standard Metric units (m/s for speed, meters for all lengths).
        /// </summary>
        /// <returns>A new mutable configuration using metres per second and metres.</returns>
        public static WindUnitsConfiguration CreateMetric()
        {
            return new WindUnitsConfiguration
            {
                WindSpeedUnit = WindVelocityUnit.MetersPerSecond,
                HeightAboveSeaLevelUnit = WindLengthUnit.Meters,
                EscarpmentHeightUnit = WindLengthUnit.Meters,
                EscarpmentLhUnit = WindLengthUnit.Meters,
                EscarpmentXUnit = WindLengthUnit.Meters,
                StructureHeightUnit = WindLengthUnit.Meters,
                StructureDimension1Unit = WindLengthUnit.Meters,
                StructureDimension2Unit = WindLengthUnit.Meters
            };
        }

        /// <summary>
        /// Marshals the units configuration into the exact 8-element integer array expected by OpenSTAAD COM.
        /// </summary>
        /// <returns>An integer array of size 8.</returns>
        public int[] ToIntArray()
        {
            LoadArguments.Defined(WindSpeedUnit, nameof(WindSpeedUnit));
            LoadArguments.Defined(HeightAboveSeaLevelUnit, nameof(HeightAboveSeaLevelUnit));
            LoadArguments.Defined(EscarpmentHeightUnit, nameof(EscarpmentHeightUnit));
            LoadArguments.Defined(EscarpmentLhUnit, nameof(EscarpmentLhUnit));
            LoadArguments.Defined(EscarpmentXUnit, nameof(EscarpmentXUnit));
            LoadArguments.Defined(StructureHeightUnit, nameof(StructureHeightUnit));
            LoadArguments.Defined(StructureDimension1Unit, nameof(StructureDimension1Unit));
            LoadArguments.Defined(StructureDimension2Unit, nameof(StructureDimension2Unit));
            return new int[8]
            {
                (int)WindSpeedUnit,
                (int)HeightAboveSeaLevelUnit,
                (int)EscarpmentHeightUnit,
                (int)EscarpmentLhUnit,
                (int)EscarpmentXUnit,
                (int)StructureHeightUnit,
                (int)StructureDimension1Unit,
                (int)StructureDimension2Unit
            };
        }
    }
}
