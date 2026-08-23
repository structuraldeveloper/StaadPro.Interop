using System;

namespace StaadPro.Interop.Extensions
{
    public static class MathExtensions
    {
        public static double Radians(this double degrees) => degrees * (Math.PI / 180.0);
        public static double Degrees(this double radians) => radians * (180.0 / Math.PI);
        public static double Square(this double value) => value * value;
        public static double Ceiling(this double value, double roundTo)
        {
            if (roundTo <= 0) return Math.Ceiling(value);
            return Math.Ceiling(value / roundTo) * roundTo;
        }
    }
}
