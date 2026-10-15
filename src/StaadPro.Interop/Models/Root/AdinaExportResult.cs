using System;
using StaadPro.Interop.Enums.Root;

namespace StaadPro.Interop.Models.Root
{
    /// <summary>Contains the native export status and requested ADINA conversion options.</summary>
    /// <remarks>Success is the native response, not independent file verification. No output path is
    /// inferred from an API that does not return one. Unknown status codes remain inspectable.</remarks>
    public sealed class AdinaExportResult
    {
        internal AdinaExportResult(int nativeStatus, AdinaAxisSystem axisSystem, AdinaLoadCaseHandling loadCaseHandling)
        {
            NativeStatus = nativeStatus; AxisSystem = axisSystem; LoadCaseHandling = loadCaseHandling;
            Status = Enum.IsDefined(typeof(AdinaExportStatus), nativeStatus) ? (AdinaExportStatus?)nativeStatus : null;
        }
        /// <summary>Gets the exact native signed status code.</summary>
        /// <value>The documented or unknown Int32 response.</value>
        public int NativeStatus { get; }
        /// <summary>Gets a recognized native export response.</summary>
        /// <value>A documented status, or null for an unknown code.</value>
        public AdinaExportStatus? Status { get; }
        /// <summary>Gets whether the server reported export success.</summary>
        /// <value>True only for zero or -14501, including warning success.</value>
        public bool IsSuccessful => NativeStatus == 0 || NativeStatus == -14501;
        /// <summary>Gets whether the server reported successful export with warnings.</summary>
        /// <value>True only for -14501; inspect the native export log separately.</value>
        public bool HasWarnings => NativeStatus == -14501;
        /// <summary>Gets the requested axis convention.</summary>
        /// <value>The validated option sent to native export.</value>
        public AdinaAxisSystem AxisSystem { get; }
        /// <summary>Gets the requested load-case conversion.</summary>
        /// <value>The validated option sent to native export.</value>
        public AdinaLoadCaseHandling LoadCaseHandling { get; }
    }
}
