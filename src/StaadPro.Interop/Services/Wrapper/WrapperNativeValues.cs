using System;

namespace StaadPro.Interop.Services.Wrapper
{
    internal static class WrapperNativeValues
    {
        internal static int ReadInt32(object value, string name)
        {
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
                default: throw new InvalidOperationException("OpenSTAAD returned an unsupported " + name + " type.");
            }
            if (integer < int.MinValue || integer > int.MaxValue)
                throw new InvalidOperationException("OpenSTAAD returned " + name + " outside Int32 range.");
            return (int)integer;
        }
    }
}
