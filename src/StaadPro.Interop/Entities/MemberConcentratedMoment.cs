using StaadPro.Interop.Attributes;
using StaadPro.Interop.Common.Units;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Entities
{
    /// <summary>Represents an assigned concentrated moment at a position along a member.</summary>
    /// <remarks>Direction identifies the local or global axis about which the moment acts
    /// (codes 1 through 6). Values retrieved by load queries retain OpenSTAAD's raw units.
    /// Before reapplying a queried definition, convert it to the assignment API's input units.</remarks>
    public sealed class MemberConcentratedMoment : MemberLoad
    {
        /// <summary>Creates a zero-valued moment definition with the inherited GlobalY direction.</summary>
        public MemberConcentratedMoment() { }

        /// <summary>Creates a concentrated moment definition from its direction, magnitude and location.</summary>
        /// <param name="direction">Local/global moment axis; OpenSTAAD supports codes 1 through 6.</param>
        /// <param name="magnitude">Signed moment magnitude (force times length).</param>
        /// <param name="position">Distance from the member start node to the moment.</param>
        /// <param name="eccentricity">Signed perpendicular offset from the shear center to the loading plane.</param>
        public MemberConcentratedMoment(LoadDirection direction, double magnitude, double position, double eccentricity = 0.0)
        {
            Direction = direction;
            Magnitude = magnitude;
            Position = position;
            Eccentricity = eccentricity;
        }

        /// <summary>Gets or sets the signed concentrated moment magnitude in force-times-length units.</summary>
        [LoadDefiningProperty]
        public double Magnitude { get; set; }

        /// <summary>Gets or sets the distance from the member start node to the moment, in length units.</summary>
        [LoadDefiningProperty]
        public double Position { get; set; }

        /// <summary>Gets or sets the signed perpendicular shear-center offset to the loading plane, in length units.</summary>
        [LoadDefiningProperty]
        public double Eccentricity { get; set; }

        /// <summary>Copies this load definition into a new independent moment object.</summary>
        /// <returns>A new definition with the same values and shared LoadCase reference;
        /// its Members collection is empty, consistent with the other member load definition copies.</returns>
        public override MemberLoad DeepCopy() => new MemberConcentratedMoment(Direction, Magnitude, Position, Eccentricity) { LoadCase = LoadCase };

        /// <summary>Converts this definition's dimensional values in place.</summary>
        /// <param name="uc">Converter for the definition's actual source/target units; null leaves it unchanged.</param>
        /// <remarks>Magnitude uses MomentConversionFactor. Position and Eccentricity use
        /// LengthConversionFactor. Direction, LoadCase and member associations are unchanged.</remarks>
        public override void ConvertUsing(UnitsConverter uc)
        {
            if (uc == null) return;
            Magnitude *= uc.MomentConversionFactor;
            Position *= uc.LengthConversionFactor;
            Eccentricity *= uc.LengthConversionFactor;
        }

        /// <summary>Submits this moment definition to OpenSTAAD's AddMemberConcMoment method.</summary>
        /// <param name="load">Raw OpenSTAAD Load interface; null causes no operation.</param>
        /// <param name="beamIds">Target STAAD member numbers; null or empty causes no operation.</param>
        /// <remarks>The caller must activate the target case and supply values in the assignment API's
        /// input units. This follows the existing MemberLoad assignment contract: no conversion is performed,
        /// native return statuses are not exposed, and COM/binding exceptions propagate.</remarks>
        public override void ApplyTo(dynamic load, int[] beamIds)
        {
            if (load == null || beamIds == null || beamIds.Length == 0) return;
            load.AddMemberConcMoment(beamIds, (int)Direction, Magnitude, Position, Eccentricity);
        }
    }
}
