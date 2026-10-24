using System;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains an explicit immutable native wide-flange table selection and its applicable four-slot specification values.</summary>
    /// <remarks>Factories name the applicable spacing, concrete or cover-plate inputs. Unused native buffer
    /// slots are zero padding, not substituted engineering data. No section database lookup, unit conversion,
    /// implicit current-material selection or engineering adequacy check occurs.</remarks>
    public sealed class WideFlangeTableDefinition
    {
        private readonly double[] values;
        private WideFlangeTableDefinition(Country country, string sectionName, TypeSpecification specification, params double[] values)
        {
            if ((int)country < 1 || (int)country > 16) throw new ArgumentOutOfRangeException(nameof(country), "This function documents country codes 1..16.");
            if (sectionName == null) throw new ArgumentNullException(nameof(sectionName));
            if (string.IsNullOrWhiteSpace(sectionName) || sectionName.IndexOf('\0') >= 0)
                throw new ArgumentException("Provide an exact nonblank native table name without NUL characters.", nameof(sectionName));
            Country = country; SectionName = sectionName; Specification = specification; this.values = (double[])values.Clone();
        }
        internal double[] NativeValues() => (double[])values.Clone();
        private static void Positive(double value, string name) => IsotropicMaterialDefinition.Finite(value, name, positive: true);
        /// <summary>Gets the requested documented country selector, codes 1..16.</summary>
        public Country Country { get; }
        /// <summary>Gets the exact nonblank native table name without trimming, NUL characters or catalog normalization.</summary>
        public string SectionName { get; }
        /// <summary>Gets the verified ST/D/T/CM/TC/BC/TB native selector.</summary>
        public TypeSpecification Specification { get; }
        /// <summary>Gets explicit double-section spacing in native length units, or null for other specifications.</summary>
        public double? Spacing => Specification == TypeSpecification.D ? values[0] : (double?)null;
        /// <summary>Gets explicit composite concrete thickness in native length units, or null.</summary>
        public double? ConcreteThickness => Specification == TypeSpecification.CM ? values[0] : (double?)null;
        /// <summary>Gets explicit composite concrete grade in native stress units, or null.</summary>
        public double? ConcreteStrength => Specification == TypeSpecification.CM ? values[1] : (double?)null;
        /// <summary>Gets explicit composite concrete width in native length units, or null.</summary>
        public double? ConcreteWidth => Specification == TypeSpecification.CM ? values[2] : (double?)null;
        /// <summary>Gets explicit composite concrete density in the native density convention, or null.</summary>
        public double? ConcreteDensity => Specification == TypeSpecification.CM ? values[3] : (double?)null;
        /// <summary>Gets explicit top cover-plate width in native length units for TC/TB, or null.</summary>
        public double? TopPlateWidth => Specification == TypeSpecification.TC || Specification == TypeSpecification.TB ? values[0] : (double?)null;
        /// <summary>Gets explicit top cover-plate thickness in native length units for TC/TB, or null.</summary>
        public double? TopPlateThickness => Specification == TypeSpecification.TC || Specification == TypeSpecification.TB ? values[1] : (double?)null;
        /// <summary>Gets explicit bottom cover-plate width in native length units for BC/TB, or null; BC uses the native first slot rather than a top-plate field.</summary>
        public double? BottomPlateWidth => Specification == TypeSpecification.BC ? values[0] : Specification == TypeSpecification.TB ? values[2] : (double?)null;
        /// <summary>Gets explicit bottom cover-plate thickness in native length units for BC/TB, or null.</summary>
        public double? BottomPlateThickness => Specification == TypeSpecification.BC ? values[1] : Specification == TypeSpecification.TB ? values[3] : (double?)null;

        /// <summary>Defines an ST selection with four unused zero buffer slots.</summary>
        /// <param name="country">Documented native country code 1..16.</param>
        /// <param name="sectionName">Exact nonblank native table name without NUL characters.</param>
        /// <returns>A new immutable standard selection; no native call is made.</returns>
        /// <exception cref="ArgumentException">The country or name is invalid.</exception>
        public static WideFlangeTableDefinition Standard(Country country, string sectionName) => new WideFlangeTableDefinition(country, sectionName, TypeSpecification.ST, 0, 0, 0, 0);
        /// <summary>Defines a D selection with explicit spacing in native length units.</summary>
        /// <param name="country">Documented native country code 1..16.</param>
        /// <param name="sectionName">Exact nonblank native table name without NUL characters.</param>
        /// <param name="spacing">Finite nonnegative native spacing; zero is explicit, not inferred.</param>
        /// <returns>A new immutable double-section selection; no native call is made.</returns>
        /// <exception cref="ArgumentException">An input violates its stated preconditions.</exception>
        public static WideFlangeTableDefinition Double(Country country, string sectionName, double spacing)
        { IsotropicMaterialDefinition.Finite(spacing, nameof(spacing)); return new WideFlangeTableDefinition(country, sectionName, TypeSpecification.D, spacing, 0, 0, 0); }
        /// <summary>Defines a T selection with four unused zero buffer slots.</summary>
        /// <param name="country">Documented native country code 1..16.</param>
        /// <param name="sectionName">Exact nonblank native table name without NUL characters.</param>
        /// <returns>A new immutable tee selection; no native call is made.</returns>
        /// <exception cref="ArgumentException">The country or name is invalid.</exception>
        public static WideFlangeTableDefinition Tee(Country country, string sectionName) => new WideFlangeTableDefinition(country, sectionName, TypeSpecification.T, 0, 0, 0, 0);
        /// <summary>Defines CM with explicit native CT/FC/CW/CD inputs in that order.</summary>
        /// <param name="country">Documented native country code 1..16.</param>
        /// <param name="sectionName">Exact nonblank native table name without NUL characters.</param>
        /// <param name="concreteThickness">Finite positive CT in native length units.</param>
        /// <param name="concreteStrength">Finite positive FC in native stress units.</param>
        /// <param name="concreteWidth">Finite positive CW in native length units.</param>
        /// <param name="concreteDensity">Finite nonnegative CD in the acquired native density convention.</param>
        /// <returns>A new immutable composite selection; no native call or unit conversion is made.</returns>
        /// <exception cref="ArgumentException">An input violates its stated preconditions.</exception>
        public static WideFlangeTableDefinition Composite(Country country, string sectionName, double concreteThickness, double concreteStrength, double concreteWidth, double concreteDensity)
        {
            Positive(concreteThickness, nameof(concreteThickness)); Positive(concreteStrength, nameof(concreteStrength)); Positive(concreteWidth, nameof(concreteWidth)); IsotropicMaterialDefinition.Finite(concreteDensity, nameof(concreteDensity));
            return new WideFlangeTableDefinition(country, sectionName, TypeSpecification.CM, concreteThickness, concreteStrength, concreteWidth, concreteDensity);
        }
        /// <summary>Defines TC with explicit top plate dimensions.</summary>
        /// <param name="country">Documented native country code 1..16.</param>
        /// <param name="sectionName">Exact nonblank native table name without NUL characters.</param>
        /// <param name="width">Finite positive top width in native length units.</param>
        /// <param name="thickness">Finite positive top thickness in native length units.</param>
        /// <returns>A new immutable top-cover selection.</returns>
        /// <exception cref="ArgumentException">An input violates its stated preconditions.</exception>
        public static WideFlangeTableDefinition WithTopPlate(Country country, string sectionName, double width, double thickness)
        { Positive(width, nameof(width)); Positive(thickness, nameof(thickness)); return new WideFlangeTableDefinition(country, sectionName, TypeSpecification.TC, width, thickness, 0, 0); }
        /// <summary>Defines BC using bottom dimensions in native slots zero/one with the actual BC selector eight.</summary>
        /// <param name="country">Documented native country code 1..16.</param>
        /// <param name="sectionName">Exact nonblank native table name without NUL characters.</param>
        /// <param name="width">Finite positive bottom width in native length units.</param>
        /// <param name="thickness">Finite positive bottom thickness in native length units.</param>
        /// <returns>A new immutable bottom-cover selection.</returns>
        /// <exception cref="ArgumentException">An input violates its stated preconditions.</exception>
        public static WideFlangeTableDefinition WithBottomPlate(Country country, string sectionName, double width, double thickness)
        { Positive(width, nameof(width)); Positive(thickness, nameof(thickness)); return new WideFlangeTableDefinition(country, sectionName, TypeSpecification.BC, width, thickness, 0, 0); }
        /// <summary>Defines TB using top width/thickness then bottom width/thickness.</summary>
        /// <param name="country">Documented native country code 1..16.</param>
        /// <param name="sectionName">Exact nonblank native table name without NUL characters.</param>
        /// <param name="topWidth">Finite positive top width in native length units.</param>
        /// <param name="topThickness">Finite positive top thickness in native length units.</param>
        /// <param name="bottomWidth">Finite positive bottom width in native length units.</param>
        /// <param name="bottomThickness">Finite positive bottom thickness in native length units.</param>
        /// <returns>A new immutable top-and-bottom-cover selection.</returns>
        /// <exception cref="ArgumentException">An input violates its stated preconditions.</exception>
        public static WideFlangeTableDefinition WithTopAndBottomPlates(Country country, string sectionName, double topWidth, double topThickness, double bottomWidth, double bottomThickness)
        {
            Positive(topWidth, nameof(topWidth)); Positive(topThickness, nameof(topThickness)); Positive(bottomWidth, nameof(bottomWidth)); Positive(bottomThickness, nameof(bottomThickness));
            return new WideFlangeTableDefinition(country, sectionName, TypeSpecification.TB, topWidth, topThickness, bottomWidth, bottomThickness);
        }
    }
}
