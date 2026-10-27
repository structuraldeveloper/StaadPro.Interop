using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Services.Design
{
    internal static class DesignInputs
    {
        internal static void Nonnegative(int value, string name)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(name);
        }

        internal static void Limit(int value, string name)
        {
            if (value < 1 || value > 100000)
                throw new ArgumentOutOfRangeException(name);
        }

        internal static void Code(StaadDesignCode code)
        {
            if (code == StaadDesignCode.Unknown || !Enum.IsDefined(typeof(StaadDesignCode), code))
                throw new ArgumentOutOfRangeException("designCode");
        }

        internal static void Keyword(string value, string name)
        {
            if (value == null)
                throw new ArgumentNullException(name);
            if (!Regex.IsMatch(value, @"\A[A-Za-z][A-Za-z0-9_]*\z"))
                throw new ArgumentException("Specify one STAAD parameter keyword.", name);
        }

        internal static int[] Ids(IEnumerable<int> values, int maximum, string name, bool allowEmpty = true, bool allowZero = false)
        {
            if (values == null)
                throw new ArgumentNullException(name);
            Limit(maximum, "maximumResults");
            var ids = values.Take(maximum + 1).ToArray();
            if (ids.Length > maximum || (!allowEmpty && ids.Length == 0))
                throw new ArgumentException("The selection is empty or exceeds its configured limit.", name);
            if (ids.Any(i => i < 0 || (!allowZero && i == 0)))
                throw new ArgumentOutOfRangeException(name);
            return ids;
        }

        internal static string Value(object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (value.GetType().IsEnum)
            {
                try
                {
                    return Convert.ToInt32(value, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
                }
                catch (OverflowException)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Enum must fit Int32.");
                }
            }

            switch (value)
            {
                case string text:
                    if (string.IsNullOrWhiteSpace(text) || text.Any(char.IsControl))
                        throw new ArgumentException("Specify a nonblank value without control characters.", nameof(value));
                    return text;
                case bool flag:
                    return flag ? "1" : "0";
                case double number:
                    if (double.IsNaN(number) || double.IsInfinity(number))
                        throw new ArgumentOutOfRangeException(nameof(value));
                    return number.ToString("R", CultureInfo.InvariantCulture);
                case float number:
                    if (float.IsNaN(number) || float.IsInfinity(number))
                        throw new ArgumentOutOfRangeException(nameof(value));
                    return number.ToString("R", CultureInfo.InvariantCulture);
                case decimal number:
                    return number.ToString(CultureInfo.InvariantCulture);
                case sbyte _:
                case byte _:
                case short _:
                case ushort _:
                case int _:
                case uint _:
                case long _:
                case ulong _:
                    return ((IFormattable)value).ToString(null, CultureInfo.InvariantCulture);
                default:
                    throw new ArgumentException("Only strings, finite numeric scalars, Booleans and Int32-range enums are supported.", nameof(value));
            }
        }
    }
}
