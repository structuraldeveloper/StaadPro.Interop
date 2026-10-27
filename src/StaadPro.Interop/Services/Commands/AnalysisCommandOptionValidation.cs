using System;

namespace StaadPro.Interop.Services.Commands
{
    internal static class AnalysisCommandOptionValidation
    {
        internal static void Positive(int value, string parameterName)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(parameterName, "Value must be greater than zero.");
        }

        internal static void Positive(double value, string parameterName)
        {
            if (double.IsNaN(value) || double.IsInfinity(value) || value <= 0.0)
                throw new ArgumentOutOfRangeException(parameterName, "Value must be finite and greater than zero.");
        }

        internal static void Range(double value, double minimum, double maximum, string parameterName)
        {
            if (double.IsNaN(value) || double.IsInfinity(value) || value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(parameterName, $"Value must be from {minimum} through {maximum}.");
        }

        internal static void Finite(double value, string parameterName)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentOutOfRangeException(parameterName, "Value must be finite.");
        }

        internal static void EnumValue<T>(T value, string parameterName)
            where T : struct
        {
            if (!Enum.IsDefined(typeof(T), value))
                throw new ArgumentOutOfRangeException(parameterName);
        }
    }
}
