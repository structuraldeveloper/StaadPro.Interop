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

        public LengthInputUnit FromLength { get; set; }
        public ForceInputUnit FromForce { get; set; }
        public LengthInputUnit ToLength { get; set; }
        public ForceInputUnit ToForce { get; set; }

        public double LengthConversionFactor => GetLengthToMeter(FromLength) / GetLengthToMeter(ToLength);

        public double ForceConversionFactor => GetForceToKiloNewton(FromForce) / GetForceToKiloNewton(ToForce);

        public double AreaConversionFactor => LengthConversionFactor * LengthConversionFactor;

        public double VolumeConversionFactor => AreaConversionFactor * LengthConversionFactor;

        public double MomentConversionFactor => ForceConversionFactor * LengthConversionFactor;

        public double LinearLoadConversionFactor => ForceConversionFactor / LengthConversionFactor;

        public double StressConversionFactor => ForceConversionFactor / AreaConversionFactor;

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
