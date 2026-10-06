using StaadPro.Interop.Attributes;
using StaadPro.Interop.Common.Units;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Entities
{
    /// <summary>Represents a full-member linearly varying force distribution, including a triangular middle intensity.</summary>
    /// <remarks>OpenSTAAD supports only LocalX, LocalY and LocalZ for this load family.
    /// Intensities have force-per-length units. The constructor accepts start/middle/end;
    /// the native getter and assignment method use start/end/middle. No interpolation is performed.</remarks>
    public sealed class MemberLinearVaryingLoad : MemberLoad
    {
        /// <summary>Creates a zero-valued definition using the supported LocalY direction.</summary>
        public MemberLinearVaryingLoad() { Direction = LoadDirection.LocalY; }

        /// <summary>Creates a varying load definition from its start, middle and end intensities.</summary>
        /// <param name="direction">Local force direction (LocalX, LocalY or LocalZ).</param>
        /// <param name="wStart">Signed force-per-length intensity at the member start.</param>
        /// <param name="wMiddle">Signed force-per-length intensity at the member middle, used for triangular loading.</param>
        /// <param name="wEnd">Signed force-per-length intensity at the member end.</param>
        public MemberLinearVaryingLoad(LoadDirection direction, double wStart, double wMiddle, double wEnd) : this()
        {
            Direction = direction;
            WStart = wStart;
            WMiddle = wMiddle;
            WEnd = wEnd;
        }

        /// <summary>Gets or sets the signed force-per-length intensity at the member start.</summary>
        [LoadDefiningProperty]
        public double WStart { get; set; }

        /// <summary>Gets or sets the signed middle intensity for a triangular member load.</summary>
        /// <remarks>This is preserved as supplied/retrieved; it is not computed from the endpoints.</remarks>
        [LoadDefiningProperty]
        public double WMiddle { get; set; }

        /// <summary>Gets or sets the signed force-per-length intensity at the member end.</summary>
        [LoadDefiningProperty]
        public double WEnd { get; set; }

        /// <summary>Copies this load definition into a new independent varying load object.</summary>
        /// <returns>A new definition with identical intensities/direction and a shared LoadCase reference;
        /// its Members collection is empty.</returns>
        public override MemberLoad DeepCopy() => new MemberLinearVaryingLoad(Direction, WStart, WMiddle, WEnd) { LoadCase = LoadCase };

        /// <summary>Converts all three intensities in place using the force-per-length factor.</summary>
        /// <param name="uc">Converter for the actual source/target units; null leaves the definition unchanged.</param>
        /// <remarks>WStart, WMiddle and WEnd use LinearLoadConversionFactor. Direction,
        /// LoadCase and member associations are unchanged.</remarks>
        public override void ConvertUsing(UnitsConverter uc)
        {
            if (uc == null) return;
            WStart *= uc.LinearLoadConversionFactor;
            WMiddle *= uc.LinearLoadConversionFactor;
            WEnd *= uc.LinearLoadConversionFactor;
        }

        /// <summary>Submits this definition to OpenSTAAD's AddMemberLinearVari method.</summary>
        /// <param name="load">Raw OpenSTAAD Load interface; null causes no operation.</param>
        /// <param name="beamIds">Target member numbers; null or empty causes no operation.</param>
        /// <remarks>Native intensity arguments are WStart, WEnd, WMiddle, in that order.
        /// The caller must activate the case and use the assignment API's input units. No conversion is
        /// performed; native return statuses are not exposed, and COM/binding exceptions propagate.</remarks>
        public override void ApplyTo(dynamic load, int[] beamIds)
        {
            if (load == null || beamIds == null || beamIds.Length == 0) return;
            load.AddMemberLinearVari(beamIds, (int)Direction, WStart, WEnd, WMiddle);
        }
    }
}
