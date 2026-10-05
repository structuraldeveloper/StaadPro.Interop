using StaadPro.Interop.Attributes;
using StaadPro.Interop.Common.Units;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Entities
{
    /// <summary>
    /// Represents a uniformly distributed load (UDL) applied along the length of a member.
    /// </summary>
    public sealed class MemberUniformlyDistributedLoad : MemberLoad
    {
        #region Constructors

        public MemberUniformlyDistributedLoad() : base()
        {
        }

        public MemberUniformlyDistributedLoad(LoadDirection direction, double magnitude, double sPosition = 0.0, double ePosition = 0.0, double eccentricity = 0.0) : this()
        {
            Direction = direction;
            Magnitude = magnitude;
            SPosition = sPosition;
            EPosition = ePosition;
            Eccentricity = eccentricity;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The magnitude of the uniformly distributed load (force per unit length, e.g. kN/m).
        /// </summary>
        [LoadDefiningProperty]
        public double Magnitude { get; set; }

        /// <summary>
        /// Start position along the member length from the start node (0.0 for full member).
        /// </summary>
        [LoadDefiningProperty]
        public double SPosition { get; set; }

        /// <summary>
        /// End position along the member length from the start node (0.0 for full member).
        /// </summary>
        [LoadDefiningProperty]
        public double EPosition { get; set; }

        /// <summary>
        /// Perpendicular offset from the member's shear center.
        /// </summary>
        [LoadDefiningProperty]
        public double Eccentricity { get; set; }

        #endregion

        #region Method Overrides

        public override MemberLoad DeepCopy()
        {
            return new MemberUniformlyDistributedLoad(Direction, Magnitude, SPosition, EPosition, Eccentricity)
            {
                LoadCase = this.LoadCase
            };
        }

        public override void ConvertUsing(UnitsConverter uc)
        {
            if (uc == null) return;
            Magnitude *= uc.LinearLoadConversionFactor;
            SPosition *= uc.LengthConversionFactor;
            EPosition *= uc.LengthConversionFactor;
            Eccentricity *= uc.LengthConversionFactor;
        }

        public override void ApplyTo(dynamic load, int[] beamIds)
        {
            if (load == null || beamIds == null || beamIds.Length == 0) return;
            load.AddMemberUniformForce(beamIds, (int)Direction, Magnitude, SPosition, EPosition, Eccentricity);
        }

        #endregion
    }
}
