using StaadPro.Interop.Attributes;
using StaadPro.Interop.Common.Units;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Entities
{
    /// <summary>Represents a trapezoidal force distribution with endpoint intensities and loaded-span positions.</summary>
    /// <remarks>OpenSTAAD supports direction codes 1 through 9, including projected axes.
    /// Intensities have force-per-length units; positions have length units. Query results retain raw units
    /// and zero position sentinels. The definition does not interpolate or expand full-span endpoints.</remarks>
    public sealed class MemberTrapezoidalLoad : MemberLoad
    {
        /// <summary>Creates a zero-valued definition with the inherited GlobalY direction.</summary>
        public MemberTrapezoidalLoad() { }

        /// <summary>Creates a trapezoidal definition from endpoint intensities and positions.</summary>
        /// <param name="direction">Local/global/projected force direction (codes 1 through 9).</param>
        /// <param name="wStart">Signed force-per-length intensity at the loading start.</param>
        /// <param name="wEnd">Signed force-per-length intensity at the loading end.</param>
        /// <param name="sPosition">Distance from member start to loading start; zero is preserved.</param>
        /// <param name="ePosition">Distance from member start to loading end; zero is preserved.</param>
        public MemberTrapezoidalLoad(LoadDirection direction, double wStart, double wEnd, double sPosition, double ePosition)
        {
            Direction = direction;
            WStart = wStart;
            WEnd = wEnd;
            SPosition = sPosition;
            EPosition = ePosition;
        }

        /// <summary>Gets or sets the signed intensity at the loading start, in force-per-length units.</summary>
        [LoadDefiningProperty]
        public double WStart { get; set; }

        /// <summary>Gets or sets the signed intensity at the loading end, in force-per-length units.</summary>
        [LoadDefiningProperty]
        public double WEnd { get; set; }

        /// <summary>Gets or sets the distance from the member start to the loading start, in length units.</summary>
        [LoadDefiningProperty]
        public double SPosition { get; set; }

        /// <summary>Gets or sets the distance from the member start to the loading end, in length units.</summary>
        [LoadDefiningProperty]
        public double EPosition { get; set; }

        /// <summary>Copies this load definition into a new independent trapezoidal load object.</summary>
        /// <returns>A new definition with identical values and a shared LoadCase reference;
        /// its Members collection is empty.</returns>
        public override MemberLoad DeepCopy() => new MemberTrapezoidalLoad(Direction, WStart, WEnd, SPosition, EPosition) { LoadCase = LoadCase };

        /// <summary>Converts intensities and loaded-span distances in place.</summary>
        /// <param name="uc">Converter for the actual source/target units; null leaves the definition unchanged.</param>
        /// <remarks>Intensities use LinearLoadConversionFactor; positions use LengthConversionFactor.
        /// Direction, LoadCase and member associations are unchanged.</remarks>
        public override void ConvertUsing(UnitsConverter uc)
        {
            if (uc == null) return;
            WStart *= uc.LinearLoadConversionFactor;
            WEnd *= uc.LinearLoadConversionFactor;
            SPosition *= uc.LengthConversionFactor;
            EPosition *= uc.LengthConversionFactor;
        }

        /// <summary>Submits this definition to OpenSTAAD's AddMemberTrapezoidal method.</summary>
        /// <param name="load">Raw OpenSTAAD Load interface; null causes no operation.</param>
        /// <param name="beamIds">Target member numbers; null or empty causes no operation.</param>
        /// <remarks>The caller must activate the case and use the assignment API's input units.
        /// No conversion is performed; native return statuses are not exposed, and COM/binding exceptions propagate.</remarks>
        public override void ApplyTo(dynamic load, int[] beamIds)
        {
            if (load == null || beamIds == null || beamIds.Length == 0) return;
            load.AddMemberTrapezoidal(beamIds, (int)Direction, WStart, WEnd, SPosition, EPosition);
        }
    }
}
