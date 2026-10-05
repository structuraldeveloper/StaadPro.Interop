using StaadPro.Interop.Attributes;
using StaadPro.Interop.Common.Units;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Entities
{
    /// <summary>
    /// Represents a concentrated point load applied at a specific location along a member.
    /// </summary>
    public sealed class MemberConcentratedLoad : MemberLoad
    {
        #region Constructors

        public MemberConcentratedLoad() : base()
        {
        }

        public MemberConcentratedLoad(LoadDirection direction, double magnitude, double position, double eccentricity = 0.0) : this()
        {
            Direction = direction;
            Magnitude = magnitude;
            Position = position;
            Eccentricity = eccentricity;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Force magnitude of the concentrated load (e.g. kN).
        /// </summary>
        [LoadDefiningProperty]
        public double Magnitude { get; set; }

        /// <summary>
        /// Distance from the member start node along the member length.
        /// </summary>
        [LoadDefiningProperty]
        public double Position { get; set; }

        /// <summary>
        /// Perpendicular offset from the member's shear center.
        /// </summary>
        [LoadDefiningProperty]
        public double Eccentricity { get; set; }

        #endregion

        #region Method Overrides

        public override MemberLoad DeepCopy()
        {
            return new MemberConcentratedLoad(Direction, Magnitude, Position, Eccentricity)
            {
                LoadCase = this.LoadCase
            };
        }

        public override void ConvertUsing(UnitsConverter uc)
        {
            if (uc == null) return;
            Magnitude *= uc.ForceConversionFactor;
            Position *= uc.LengthConversionFactor;
            Eccentricity *= uc.LengthConversionFactor;
        }

        public override void ApplyTo(dynamic load, int[] beamIds)
        {
            if (load == null || beamIds == null || beamIds.Length == 0) return;
            load.AddMemberConcForce(beamIds, (int)Direction, Magnitude, Position, Eccentricity);
        }

        #endregion
    }
}
