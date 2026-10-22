using StaadPro.Interop.Services.Load;
using System;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Models.Loads.Wind
{
    /// <summary>
    /// Encapsulates solid sign structure parameters for ASCE 7 wind loading in OpenSTAAD.
    /// </summary>
    public class ASCE7SolidSignData : IASCE7StructureData
    {
        /// <summary>Gets the native ASCE structure classification.</summary>
        public ASCE7StructureType StructureType => ASCE7StructureType.SolidSign;

        /// <summary>
        /// Gets or sets the total height of the solid sign above ground level (H).
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets the vertical dimension of the solid sign board (M).
        /// </summary>
        public double DimensionM { get; set; }

        /// <summary>
        /// Gets or sets the horizontal dimension / width of the solid sign board (N).
        /// </summary>
        public double DimensionN { get; set; }

        /// <summary>
        /// Gets or sets the fundamental natural frequency of the sign structure in Hz.
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
            LoadArguments.Finite(Height, nameof(Height));
            LoadArguments.Finite(DimensionM, nameof(DimensionM));
            LoadArguments.Finite(DimensionN, nameof(DimensionN));
            LoadArguments.Finite(NaturalFrequency, nameof(NaturalFrequency));
            LoadArguments.Finite(DampingRatio, nameof(DampingRatio));
            if (Height <= 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(Height), "Sign height (H) must be strictly positive.");
            }

            if (DimensionM <= 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(DimensionM), "Sign M dimension must be strictly positive.");
            }

            if (DimensionN <= 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(DimensionN), "Sign N dimension must be strictly positive.");
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
                Height,
                DimensionM,
                DimensionN,
                NaturalFrequency,
                DampingRatio,
                0.0,
                0.0
            };
        }
    }
}
