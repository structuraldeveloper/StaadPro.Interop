using System;
using System.Globalization;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Services.Wrapper
{
    // Unit parsing ported from the main wrapper; native invocation remains in the wrapper.
    internal static class WrapperUnitParser
    {
        internal static bool TryParseForceInputUnit(object value, out ForceInputUnit unit)
        {
            unit = ForceInputUnit.KiloNewton;
            if (value == null || value == DBNull.Value || value is bool)
            {
                return false;
            }

            if (TryConvertToInt32(value, out int intVal))
            {
                if (intVal >= 0 && Enum.IsDefined(typeof(ForceInputUnit), intVal))
                {
                    unit = (ForceInputUnit)intVal;
                    return true;
                }
            }

            string text = value.ToString().Trim();
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            string clean = text.ToLowerInvariant().Replace(" ", "").Replace("-", "").Replace("_", "");
            if (clean.Contains("kilonewton") || clean == "kn" || clean == "kns")
            {
                unit = ForceInputUnit.KiloNewton;
                return true;
            }
            if (clean.Contains("kilopound") || clean == "kip" || clean == "kips")
            {
                unit = ForceInputUnit.KiloPound;
                return true;
            }
            if (clean.Contains("pound") || clean == "lb" || clean == "lbs" || clean == "lbf")
            {
                unit = ForceInputUnit.Pound;
                return true;
            }
            if (clean.Contains("kilogram") || clean == "kg" || clean == "kgf")
            {
                unit = ForceInputUnit.KiloGram;
                return true;
            }
            if (clean.Contains("metricton") || clean == "mt" || clean == "mton")
            {
                unit = ForceInputUnit.MetricTon;
                return true;
            }
            if (clean.Contains("meganewton") || clean == "mn")
            {
                unit = ForceInputUnit.MegaNewton;
                return true;
            }
            if (clean.Contains("decanewton") || clean == "dan" || clean == "dn")
            {
                unit = ForceInputUnit.DecaNewton;
                return true;
            }
            if (clean.Contains("newton") || clean == "n")
            {
                unit = ForceInputUnit.Newton;
                return true;
            }

            return false;
        }

        internal static bool TryParseLengthInputUnit(object value, out LengthInputUnit unit)
        {
            unit = LengthInputUnit.Meter;
            if (value == null || value == DBNull.Value || value is bool)
            {
                return false;
            }

            if (TryConvertToInt32(value, out int intVal))
            {
                if (intVal == 1) { unit = LengthInputUnit.Feet; return true; }
                if (intVal >= 0 && Enum.IsDefined(typeof(LengthInputUnit), intVal))
                {
                    unit = (LengthInputUnit)intVal;
                    return true;
                }
            }

            string text = value.ToString().Trim();
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            string clean = text.ToLowerInvariant().Replace(" ", "").Replace("-", "").Replace("_", "");
            if (clean.Contains("millimeter") || clean.Contains("millimetre") || clean == "mm")
            {
                unit = LengthInputUnit.MilliMeter;
                return true;
            }
            if (clean.Contains("centimeter") || clean.Contains("centimetre") || clean == "cm")
            {
                unit = LengthInputUnit.CentiMeter;
                return true;
            }
            if (clean.Contains("decimeter") || clean.Contains("decimetre") || clean == "dm")
            {
                unit = LengthInputUnit.DeciMeter;
                return true;
            }
            if (clean.Contains("kilometer") || clean.Contains("kilometre") || clean == "km")
            {
                unit = LengthInputUnit.KiloMeter;
                return true;
            }
            if (clean.Contains("meter") || clean.Contains("metre") || clean == "m")
            {
                unit = LengthInputUnit.Meter;
                return true;
            }
            if (clean.Contains("inch") || clean == "in")
            {
                unit = LengthInputUnit.Inch;
                return true;
            }
            if (clean.Contains("feet") || clean.Contains("foot") || clean == "ft")
            {
                unit = LengthInputUnit.Feet;
                return true;
            }

            return false;
        }

        private static bool TryConvertToInt32(object value, out int result)
        {
            result = default;

            if (value == null || value == DBNull.Value || value is bool)
            {
                return false;
            }

            if (value is string text)
                return int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
            long integer;
            switch (value)
            {
                case sbyte n: integer = n; break;
                case byte n: integer = n; break;
                case short n: integer = n; break;
                case ushort n: integer = n; break;
                case int n: integer = n; break;
                case uint n: integer = n; break;
                case long n: integer = n; break;
                case ulong n when n <= int.MaxValue: integer = (long)n; break;
                default: return false;
            }
            if (integer < int.MinValue || integer > int.MaxValue) return false;
            result = (int)integer;
            return true;
        }

    }
}
