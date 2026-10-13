#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using StaadPro.Interop.Services.Acquisition;

namespace StaadPro.Interop.Helpers
{
    /// <summary>Provides the existing ROT helper API on modern Windows .NET through native OLE lookup.</summary>
    public static class RotHelpers
    {
        /// <summary>Retrieves existing filename, class and active-object OpenSTAAD candidates.</summary>
        /// <returns>A non-null list of obtained roots; inaccessible registrations are skipped.</returns>
        /// <remarks>Does not launch STAAD. Candidate count is not application count. Requires appropriate COM apartment usage.</remarks>
        public static IList<object> GetRunningOpenStaadInstances() => OpenStaadNativeDiscovery.Discover().Roots;
        internal static bool IsOpenStaadMoniker(string name, Guid classId) => OpenStaadNativeDiscovery.IsCandidate(name, classId);
    }
}
#endif
