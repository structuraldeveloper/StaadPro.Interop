using StaadPro.Interop.Services.Load;
using System;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Models.Loads.Wind
{
    /// <summary>
    /// Encapsulates building structure parameters for ASCE 7 wind loading in OpenSTAAD.
    /// </summary>
    public class ASCE7BuildingData : IASCE7StructureData
    {
        /// <summary>Gets the native ASCE structure classification.</summary>
        public ASCE7StructureType StructureType => ASCE7StructureType.Building;

        /// <summary>
        /// Gets or sets the enclosure classification of the building.
        /// </summary>
        public BuildingEnclosureClassification EnclosureClassification { get; set; } = BuildingEnclosureClassification.EnclosedBuilding;

        /// <summary>
        /// Gets or sets the total height of the building.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets the building length parallel to the wind direction (L).
        /// </summary>
        public double LengthAlongWindDirection { get; set; }

        /// <summary>
        /// Gets or sets the building width / length normal to the wind direction (B).
        /// </summary>
        public double WidthNormalToWindDirection { get; set; }

        /// <summary>
        /// Gets or sets the fundamental natural frequency of the building in Hz.
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
            LoadArguments.Defined(EnclosureClassification, nameof(EnclosureClassification));
            LoadArguments.Finite(Height, nameof(Height));
            LoadArguments.Finite(LengthAlongWindDirection, nameof(LengthAlongWindDirection));
            LoadArguments.Finite(WidthNormalToWindDirection, nameof(WidthNormalToWindDirection));
            LoadArguments.Finite(NaturalFrequency, nameof(NaturalFrequency));
            LoadArguments.Finite(DampingRatio, nameof(DampingRatio));
            if (Height <= 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(Height), "Building height must be strictly positive.");
            }

            if (LengthAlongWindDirection <= 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(LengthAlongWindDirection), "Building length along wind direction (L) must be strictly positive.");
            }

            if (WidthNormalToWindDirection <= 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(WidthNormalToWindDirection), "Building width normal to wind direction (B) must be strictly positive.");
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
        /// <exception cref="NotSupportedException">PartiallyOpen is requested for a legacy packet without that classification.</exception>
        public double[] ToDoubleArray(ASCE7Publication publication)
        {
            LoadArguments.Defined(publication, nameof(publication));
            Validate();

            // Enclosure code mapping:
            // 2016: Open(0), PartiallyOpen(1), PartiallyEnclosed(2), Enclosed(3)
            // Pre-2016: Open(0), PartiallyEnclosed(1), Enclosed(2)
            double enclosureVal;
            if (publication == ASCE7Publication.ASCE7_16)
            {
                enclosureVal = (double)(int)EnclosureClassification;
            }
            else
            {
                switch (EnclosureClassification)
                {
                    case BuildingEnclosureClassification.OpenBuilding:
                        enclosureVal = 0.0;
                        break;
                    case BuildingEnclosureClassification.PartiallyOpen:
                        throw new NotSupportedException("PartiallyOpen has no distinct classification in the reviewed legacy wind packet.");
                    case BuildingEnclosureClassification.PartiallyEnclosed:
                        enclosureVal = 1.0;
                        break;
                    case BuildingEnclosureClassification.EnclosedBuilding:
                    default:
                        enclosureVal = 2.0;
                        break;
                }
            }

            return new double[7]
            {
                enclosureVal,
                Height,
                LengthAlongWindDirection,
                WidthNormalToWindDirection,
                NaturalFrequency,
                DampingRatio,
                0.0
            };
        }
    }
}
