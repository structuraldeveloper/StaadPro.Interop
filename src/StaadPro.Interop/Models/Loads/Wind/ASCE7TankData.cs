using StaadPro.Interop.Services.Load;
using System;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Models.Loads.Wind
{
    /// <summary>
    /// Encapsulates chimney, tank, and silo structure parameters for ASCE 7 wind loading in OpenSTAAD.
    /// </summary>
    public class ASCE7TankData : IASCE7StructureData
    {
        /// <summary>Gets the native ASCE structure classification.</summary>
        public ASCE7StructureType StructureType => ASCE7StructureType.ChimneyOrTank;

        /// <summary>
        /// Gets or sets the horizontal cross-section type of the tank or chimney.
        /// </summary>
        public TankCrossSectionType CrossSectionType { get; set; } = TankCrossSectionType.RoundAxisymmetric;

        /// <summary>
        /// Gets or sets the total height of the tank or chimney (H).
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets the least horizontal dimension or diameter of the tank/chimney (W).
        /// </summary>
        public double LeastHorizontalDimension { get; set; }

        /// <summary>
        /// Gets or sets the depth of protruding elements such as spoilers, ribs, and ladders (D').
        /// </summary>
        public double DepthOfProtrudingElements { get; set; }

        /// <summary>
        /// Gets or sets the fundamental natural frequency of the structure in Hz.
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
            LoadArguments.Finite(LeastHorizontalDimension, nameof(LeastHorizontalDimension));
            LoadArguments.Finite(DepthOfProtrudingElements, nameof(DepthOfProtrudingElements));
            LoadArguments.Finite(NaturalFrequency, nameof(NaturalFrequency));
            LoadArguments.Finite(DampingRatio, nameof(DampingRatio));
            if (Height <= 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(Height), "Tank height must be strictly positive.");
            }

            if (LeastHorizontalDimension <= 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(LeastHorizontalDimension), "Tank least horizontal dimension (W) must be strictly positive.");
            }

            if (DepthOfProtrudingElements < 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(DepthOfProtrudingElements), "Depth of protruding elements (D') cannot be negative.");
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

            double typeVal;
            if (publication == ASCE7Publication.ASCE7_16)
            {
                typeVal = (double)(int)CrossSectionType;
            }
            else
            {
                switch (CrossSectionType)
                {
                    case TankCrossSectionType.Square:
                        typeVal = 0.0;
                        break;
                    case TankCrossSectionType.SquareDiagonal:
                        typeVal = 1.0;
                        break;
                    case TankCrossSectionType.Hexagonal:
                    case TankCrossSectionType.OctagonalNonAxisymmetric:
                    case TankCrossSectionType.OctagonalAxisymmetric:
                        typeVal = 2.0; // Hexagonal or Octagonal in pre-2016
                        break;
                    case TankCrossSectionType.RoundNonAxisymmetric:
                    case TankCrossSectionType.RoundAxisymmetric:
                    default:
                        typeVal = 3.0; // Round in pre-2016
                        break;
                }
            }

            return new double[7]
            {
                typeVal,
                Height,
                LeastHorizontalDimension,
                DepthOfProtrudingElements,
                NaturalFrequency,
                DampingRatio,
                0.0
            };
        }
    }
}
