using System;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Common.Units
{
    /// <summary>
    /// Provides linear and derived engineering unit conversions between STAAD model unit systems.
    /// </summary>
    public class UnitsConverter
    {
        #region Constructors

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='UnitsConverter..ctor(LengthInputUnit,ForceInputUnit,LengthInputUnit,ForceInputUnit)']/*" />
        public UnitsConverter(LengthInputUnit fromLength = LengthInputUnit.Meter,
                              ForceInputUnit fromForce = ForceInputUnit.KiloNewton,
                              LengthInputUnit toLength = LengthInputUnit.Meter,
                              ForceInputUnit toForce = ForceInputUnit.KiloNewton)
        {
            FromLength = fromLength;
            FromForce = fromForce;
            ToLength = toLength;
            ToForce = toForce;
        }

        #endregion

        #region Properties

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='UnitsConverter.P:FromLength']/*" />
        public LengthInputUnit FromLength { get; set; }
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='UnitsConverter.P:FromForce']/*" />
        public ForceInputUnit FromForce { get; set; }
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='UnitsConverter.P:ToLength']/*" />
        public LengthInputUnit ToLength { get; set; }
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='UnitsConverter.P:ToForce']/*" />
        public ForceInputUnit ToForce { get; set; }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='UnitsConverter.P:LengthConversionFactor']/*" />
        public double LengthConversionFactor => GetLengthToMeter(FromLength) / GetLengthToMeter(ToLength);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='UnitsConverter.P:ForceConversionFactor']/*" />
        public double ForceConversionFactor => GetForceToKiloNewton(FromForce) / GetForceToKiloNewton(ToForce);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='UnitsConverter.P:AreaConversionFactor']/*" />
        public double AreaConversionFactor => LengthConversionFactor * LengthConversionFactor;

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='UnitsConverter.P:VolumeConversionFactor']/*" />
        public double VolumeConversionFactor => AreaConversionFactor * LengthConversionFactor;

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='UnitsConverter.P:MomentConversionFactor']/*" />
        public double MomentConversionFactor => ForceConversionFactor * LengthConversionFactor;

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='UnitsConverter.P:LinearLoadConversionFactor']/*" />
        public double LinearLoadConversionFactor => ForceConversionFactor / LengthConversionFactor;

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='UnitsConverter.P:StressConversionFactor']/*" />
        public double StressConversionFactor => ForceConversionFactor / AreaConversionFactor;

        /// <summary>Gets the elastic-modulus factor, equal to force divided by length squared.</summary>
        /// <remarks>This is an alias of StressConversionFactor. It does not establish the native unit convention or convert temperature-dependent fields.</remarks>
        public double ElasticModulusConversionFactor => StressConversionFactor;

        /// <summary>Gets the shear-modulus factor, equal to force divided by length squared.</summary>
        /// <remarks>Uses the same dimensional factor as elastic modulus without deriving a native G value.</remarks>
        public double ShearModulusConversionFactor => StressConversionFactor;

        /// <summary>Gets the weight-density factor, equal to force divided by length cubed.</summary>
        /// <remarks>This is force/volume, not a mass-density conversion. Uses this repository's unit constants.</remarks>
        public double DensityConversionFactor => ForceConversionFactor / VolumeConversionFactor;

        #endregion

        #region Helpers

        private static double GetLengthToMeter(LengthInputUnit unit)
        {
            switch (unit)
            {
                case LengthInputUnit.MilliMeter: return 0.001;
                case LengthInputUnit.CentiMeter: return 0.01;
                case LengthInputUnit.DeciMeter: return 0.1;
                case LengthInputUnit.Meter: return 1.0;
                case LengthInputUnit.KiloMeter: return 1000.0;
                case LengthInputUnit.Inch: return 0.0254;
                case LengthInputUnit.Feet: return 0.3048;
                default: return 1.0;
            }
        }

        private static double GetForceToKiloNewton(ForceInputUnit unit)
        {
            switch (unit)
            {
                case ForceInputUnit.Newton: return 0.001;
                case ForceInputUnit.DecaNewton: return 0.01;
                case ForceInputUnit.KiloNewton: return 1.0;
                case ForceInputUnit.MegaNewton: return 1000.0;
                case ForceInputUnit.Pound: return 0.0044482216152605;
                case ForceInputUnit.KiloPound: return 4.4482216152605;
                case ForceInputUnit.KiloGram: return 0.00980665;
                case ForceInputUnit.MetricTon: return 9.80665;
                default: return 1.0;
            }
        }

        #endregion
    }
}
