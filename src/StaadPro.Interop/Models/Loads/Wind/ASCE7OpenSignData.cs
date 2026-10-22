using StaadPro.Interop.Services.Load;
using System;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Models.Loads.Wind
{
    /// <summary>
    /// Encapsulates open sign and lattice framework structure parameters for ASCE 7 wind loading in OpenSTAAD.
    /// </summary>
    public class ASCE7OpenSignData : IASCE7StructureData
    {
        private ASCE7StructureType _structureType = ASCE7StructureType.OpenSign;

        /// <summary>
        /// Gets or sets whether this represents an Open Sign or a Lattice Framework.
        /// </summary>
        public ASCE7StructureType StructureType
        {
            get => _structureType;
            set
            {
                if (value != ASCE7StructureType.OpenSign && value != ASCE7StructureType.LatticeFramework)
                {
                    throw new ArgumentException("StructureType must be OpenSign or LatticeFramework.", nameof(value));
                }
                _structureType = value;
            }
        }

        /// <summary>
        /// Gets or sets the member profile orientation type (Flat or Rounded).
        /// </summary>
        public OpenSignOrientationType OrientationType { get; set; } = OpenSignOrientationType.Flat;

        /// <summary>
        /// Gets or sets the total height of the open sign or lattice framework (H).
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets the width of the open sign or lattice framework.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the diameter of typical rounded members (used when OrientationType is Rounded).
        /// </summary>
        public double MemberDiameter { get; set; }

        /// <summary>
        /// Gets or sets the solidity ratio: ratio of solid area to gross area (between 0.0 and 1.0).
        /// </summary>
        public double SolidAreaToGrossAreaRatio { get; set; } = 0.3;

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
            LoadArguments.Defined(OrientationType, nameof(OrientationType));
            LoadArguments.Finite(Height, nameof(Height));
            LoadArguments.Finite(Width, nameof(Width));
            LoadArguments.Finite(MemberDiameter, nameof(MemberDiameter));
            LoadArguments.Finite(SolidAreaToGrossAreaRatio, nameof(SolidAreaToGrossAreaRatio));
            LoadArguments.Finite(NaturalFrequency, nameof(NaturalFrequency));
            LoadArguments.Finite(DampingRatio, nameof(DampingRatio));
            if (Height <= 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(Height), "Height must be strictly positive.");
            }

            if (Width <= 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(Width), "Width must be strictly positive.");
            }

            if (MemberDiameter < 0) throw new ArgumentOutOfRangeException(nameof(MemberDiameter));
            if (OrientationType == OpenSignOrientationType.Rounded && MemberDiameter <= 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(MemberDiameter), "Member diameter must be strictly positive for rounded members.");
            }

            if (SolidAreaToGrossAreaRatio < 0.0 || SolidAreaToGrossAreaRatio > 1.0)
            {
                throw new ArgumentOutOfRangeException(nameof(SolidAreaToGrossAreaRatio), "Solid area to gross area ratio must be between 0.0 and 1.0.");
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
                (double)(int)OrientationType,
                Height,
                Width,
                MemberDiameter,
                SolidAreaToGrossAreaRatio,
                NaturalFrequency,
                DampingRatio
            };
        }
    }
}
