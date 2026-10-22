using System;
using StaadPro.Interop.Models;
using StaadPro.Interop.Services.Interop;

namespace StaadPro.Interop.Services.Load
{
    internal static class LoadArguments
    {
        internal static void Finite(double value, string name)
        {
            if (double.IsNaN(value) || double.IsInfinity(value)) throw new ArgumentOutOfRangeException(name, "A finite value is required.");
        }

        internal static void Defined<T>(T value, string name) where T : struct
        {
            if (!Enum.IsDefined(typeof(T), value)) throw new ArgumentOutOfRangeException(name, "A defined enumeration value is required.");
        }

        internal static void Keyword(string value, string name)
        {
            if (value == null) throw new ArgumentNullException(name);
            if (value.Length == 0 || value.IndexOf('\0') >= 0)
                throw new ArgumentException("A nonempty STAAD keyword is required.", name);
            if (value[0] < 'A' || value[0] > 'Z') throw new ArgumentException("A keyword must start with an uppercase ASCII letter.", name);
            foreach (char character in value)
                if (!((character >= 'A' && character <= 'Z') || (character >= '0' && character <= '9')))
                    throw new ArgumentException("Use an uppercase ASCII STAAD parameter keyword without whitespace.", name);
        }

        internal static bool ReadBoolean(object value, string operation)
        {
            if (value is bool flag) return flag;
            int code = NativeValues.ReadInt32(value, operation + " result");
            if (code != 0 && code != 1 && code != -1)
                throw new InvalidOperationException("OpenSTAAD " + operation + " returned an invalid Boolean result.");
            return code != 0;
        }

        internal static void CoveredSeismicProfile(ApplicationVersion version)
        {
            if (version == null || !((version.Major == 24 && version.Update == 0 && version.Revision == 2 && version.Build == 354) ||
                (version.Major == 25 && version.Update == 0 && version.Revision == 1 && version.Build == 424) ||
                (version.Major == 26 && version.Update == 0 && version.Revision == 0 && version.Build == 340)))
                throw new NotSupportedException("This version-sensitive seismic request requires a reviewed native STAAD build (24.00.02.354, 25.00.01.424 or 26.00.00.340).");
        }

        internal static void SeismicMemberCode(int seismicCode, ApplicationVersion version)
        {
            if (seismicCode < 0) throw new ArgumentOutOfRangeException(nameof(seismicCode));
            if (seismicCode == 0) return; // Common native auto-detection; no numeric translation.
            CoveredSeismicProfile(version);
            int maximum = version.Major == 24 ? 25 : 26;
            if (seismicCode > maximum) throw new ArgumentOutOfRangeException(nameof(seismicCode));
        }
    }
}
