using System;

namespace StaadPro.Interop.Services.Interop
{
    internal static class NativeValues
    {
        internal static int ReadInt32(object value, string name)
        {
            long integer = ReadInt64(value, name);
            if (integer < int.MinValue || integer > int.MaxValue)
                throw new InvalidOperationException("OpenSTAAD returned " + name + " outside Int32 range.");
            return (int)integer;
        }

        internal static long ReadInt64(object value, string name)
        {
            switch (value)
            {
                case sbyte n: return n;
                case byte n: return n;
                case short n: return n;
                case ushort n: return n;
                case int n: return n;
                case uint n: return n;
                case long n: return n;
                case ulong n when n <= long.MaxValue: return (long)n;
                default: throw new InvalidOperationException("OpenSTAAD returned an unsupported " + name + " type.");
            }
        }

        internal static uint ReadProcessId(object value)
        {
            long integer = ReadInt64(value, "process ID");
            if (integer <= 0 || integer > uint.MaxValue)
                throw new InvalidOperationException("OpenSTAAD supplied an invalid process ID.");
            return (uint)integer;
        }

        internal static bool ReadBoolean(object value, string name)
        {
            if (value is bool flag) return flag;
            int code = ReadInt32(value, name);
            if (code != 0 && code != 1)
                throw new InvalidOperationException("OpenSTAAD returned a " + name + " other than zero or one.");
            return code == 1;
        }

        internal static string ReadText(object value, string name)
        {
            if (!(value is string text))
                throw new InvalidOperationException("OpenSTAAD returned a non-string " + name + ".");
            return text;
        }
    }
}
