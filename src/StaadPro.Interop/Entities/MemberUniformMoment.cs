using StaadPro.Interop.Attributes;
using StaadPro.Interop.Common.Units;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Entities
{
    /// <summary>Represents a uniformly distributed member moment over a full or partial span.</summary>
    /// <remarks>Direction codes 1 through 9 select local, global or projected axes.
    /// Magnitude is moment per unit length, not a concentrated moment. Positions and eccentricity
    /// are lengths. Query results retain raw native units and zero span sentinels.</remarks>
    public sealed class MemberUniformMoment : MemberLoad
    {
        /// <summary>Creates a zero-valued definition with the inherited GlobalY direction.</summary>
        public MemberUniformMoment() { }

        /// <summary>Creates a uniform moment definition from its intensity, span and offset.</summary>
        /// <param name="direction">Local/global/projected moment direction (codes 1 through 9).</param>
        /// <param name="magnitude">Signed moment-per-length intensity, for example kN-m/m.</param>
        /// <param name="sPosition">Distance from member start to loading start; zero is preserved.</param>
        /// <param name="ePosition">Distance from member start to loading end; zero denotes the member end.</param>
        /// <param name="eccentricity">Signed perpendicular offset from the member shear center.</param>
        public MemberUniformMoment(LoadDirection direction, double magnitude, double sPosition, double ePosition, double eccentricity)
        {
            Direction = direction;
            Magnitude = magnitude;
            SPosition = sPosition;
            EPosition = ePosition;
            Eccentricity = eccentricity;
        }

        /// <summary>Gets or sets the signed distributed moment intensity in moment-per-length units.</summary>
        [LoadDefiningProperty]
        public double Magnitude { get; set; }

        /// <summary>Gets or sets the distance from member start to loading start, in length units.</summary>
        [LoadDefiningProperty]
        public double SPosition { get; set; }

        /// <summary>Gets or sets the distance from member start to loading end, in length units; zero denotes the member end.</summary>
        [LoadDefiningProperty]
        public double EPosition { get; set; }

        /// <summary>Gets or sets the signed perpendicular offset from the shear center, in length units.</summary>
        [LoadDefiningProperty]
        public double Eccentricity { get; set; }

        /// <summary>Copies this load definition into a new independent uniform moment object.</summary>
        /// <returns>A new definition with identical values and a shared LoadCase reference;
        /// its Members collection is empty.</returns>
        public override MemberLoad DeepCopy() => new MemberUniformMoment(Direction, Magnitude, SPosition, EPosition, Eccentricity) { LoadCase = LoadCase };

        /// <summary>Converts the distributed moment intensity, span distances and offset in place.</summary>
        /// <param name="uc">Converter for the actual source/target units; null leaves the definition unchanged.</param>
        /// <remarks>Intensity uses MomentConversionFactor divided by LengthConversionFactor
        /// (equivalent to ForceConversionFactor). All three distances use LengthConversionFactor.
        /// Confirm the native read-back units before converting queried definitions for assignment.
        /// Direction, LoadCase and member associations are unchanged.</remarks>
        public override void ConvertUsing(UnitsConverter uc)
        {
            if (uc == null) return;
            Magnitude *= uc.MomentConversionFactor / uc.LengthConversionFactor;
            SPosition *= uc.LengthConversionFactor;
            EPosition *= uc.LengthConversionFactor;
            Eccentricity *= uc.LengthConversionFactor;
        }

        /// <summary>Submits this definition to OpenSTAAD's AddMemberUniformMoment method.</summary>
        /// <param name="load">Raw OpenSTAAD Load interface; null causes no operation.</param>
        /// <param name="beamIds">Target member numbers; null or empty causes no operation.</param>
        /// <remarks>The caller must activate the case and use the assignment API's input units.
        /// No conversion is performed; native return statuses are not exposed, and COM/binding exceptions propagate.</remarks>
        public override void ApplyTo(dynamic load, int[] beamIds)
        {
            if (load == null || beamIds == null || beamIds.Length == 0) return;
            load.AddMemberUniformMoment(beamIds, (int)Direction, Magnitude, SPosition, EPosition, Eccentricity);
        }
    }
}
