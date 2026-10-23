using System;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Services.Load
{
    internal static class LoadContractArguments
    {
        internal static int Positive(int value, string name)
        { if (value <= 0) throw new ArgumentOutOfRangeException(name); return value; }
        internal static double Finite(double value, string name)
        { if (double.IsNaN(value) || double.IsInfinity(value)) throw new ArgumentOutOfRangeException(name); return value; }
        internal static string Text(string value, string name)
        { if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("A nonempty value is required.", name); return value; }
        internal static T Defined<T>(T value, string name) where T : struct
        { if (!Enum.IsDefined(typeof(T), value)) throw new ArgumentOutOfRangeException(name); return value; }
        internal static Forces Copy(Forces value, string name)
        {
            if (value == null) throw new ArgumentNullException(name);
            return new Forces(Finite(value.Fx, name), Finite(value.Fy, name), Finite(value.Fz, name),
                Finite(value.Mx, name), Finite(value.My, name), Finite(value.Mz, name));
        }
    }
}
