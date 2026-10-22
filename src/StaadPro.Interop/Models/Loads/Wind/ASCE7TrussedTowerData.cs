using StaadPro.Interop.Services.Load;
using System;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Models.Loads.Wind
{
    /// <summary>
    /// Encapsulates trussed tower structure parameters for ASCE 7 wind loading in OpenSTAAD.
    /// </summary>
    public class ASCE7TrussedTowerData : IASCE7StructureData
    {
        /// <summary>Gets the native ASCE structure classification.</summary>
        public ASCE7StructureType StructureType => ASCE7StructureType.TrussedTower;

        /// <summary>
        /// Gets or sets the horizontal cross-section geometry of the trussed tower (Triangle or Square).
        /// </summary>
        public TrussedTowerCrossSectionType CrossSectionType { get; set; } = TrussedTowerCrossSectionType.Square;

        /// <summary>
        /// Gets or sets the total height of the trussed tower (H).
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets the width of the tower face.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the ratio of solid area to gross area expressed as a percentage (e.g. 20.0 for 20%).
        /// </summary>
        public double SolidAreaPercentage { get; set; } = 20.0;

        /// <summary>
        /// Gets or sets the fundamental natural frequency of the tower in Hz.
        /// </summary>
        public double NaturalFrequency { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets the structural damping ratio.
        /// </summary>
        public double DampingRatio { get; set; } = 0.01;

        /// <summary>Validates this structure’s scalar values and classifications.</summary>
        /// <remarks>Rejects nonfinite numeric values and undefined classifications before native use. Does not certify engineering suitability.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">A scalar or classification is invalid.</exception>
        public void Validate()
        {
            LoadArguments.Defined(CrossSectionType, nameof(CrossSectionType));
            LoadArguments.Finite(Height, nameof(Height));
            LoadArguments.Finite(Width, nameof(Width));
            LoadArguments.Finite(SolidAreaPercentage, nameof(SolidAreaPercentage));
            LoadArguments.Finite(NaturalFrequency, nameof(NaturalFrequency));
            LoadArguments.Finite(DampingRatio, nameof(DampingRatio));
            if (Height <= 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(Height), "Tower height must be strictly positive.");
            }

            if (Width <= 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(Width), "Tower width must be strictly positive.");
            }

            if (SolidAreaPercentage <= 0.0 || SolidAreaPercentage > 100.0)
            {
                throw new ArgumentOutOfRangeException(nameof(SolidAreaPercentage), "Solid area percentage must be between 0.0 and 100.0%.");
            }

            if (NaturalFrequency <= 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(NaturalFrequency), "Natural frequency must be strictly positive.");
            }

            if (DampingRatio < 0.0 || DampingRatio > 1.0)
            {
                throw new ArgumentOutOfRangeException(nameof(DampingRatio), "Damping ratio must be between 0.0 and 1.0.");
            }
        }

        /// <summary>Builds an independent, validated seven-value wind definition packet.</summary>
        /// <remarks>Returns an independent seven-slot definition packet. Dimensions use the associated explicit wind units; frequency uses Hz and damping is a fraction. Older publications map enclosure and cross-section classifications to their older numeric domain.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">A value or publication is invalid.</exception>
        /// <param name="publication">Defined ASCE publication controlling edition-specific classification mapping.</param>
        /// <returns>A new seven-element Double array; mutating it does not change this model.</returns>
        public double[] ToDoubleArray(ASCE7Publication publication)
        {
            LoadArguments.Defined(publication, nameof(publication));
            Validate();

            return new double[7]
            {
                (double)(int)CrossSectionType,
                Height,
                Width,
                SolidAreaPercentage,
                NaturalFrequency,
                DampingRatio,
                0.0
            };
        }
    }
}
