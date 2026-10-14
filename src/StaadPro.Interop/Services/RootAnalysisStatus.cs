using System;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Services
{
    internal static class RootAnalysisStatus
    {
        internal static AnalysisResult Read(object value)
        {
            long code;
            switch (value)
            {
                case sbyte n: code = n; break;
                case byte n: code = n; break;
                case short n: code = n; break;
                case ushort n: code = n; break;
                case int n: code = n; break;
                case uint n: code = n; break;
                case long n: code = n; break;
                case ulong n when n <= int.MaxValue: code = (long)n; break;
                default: throw new InvalidOperationException("OpenSTAAD returned an unsupported analysis status type.");
            }
            if (code < int.MinValue || code > int.MaxValue) throw new InvalidOperationException("OpenSTAAD returned an analysis status outside Int32 range.");
            return (AnalysisResult)(int)code;
        }
    }
}
