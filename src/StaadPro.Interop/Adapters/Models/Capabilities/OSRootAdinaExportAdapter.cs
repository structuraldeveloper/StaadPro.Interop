using System;
using StaadPro.Interop.Adapters.Interfaces.Capabilities;
using StaadPro.Interop.Enums.Root;
using StaadPro.Interop.Models.Root;
using StaadPro.Interop.Services.Root.Analysis;
using StaadPro.Interop.Services.Root.Common;
using StaadPro.Interop.Services.Root.Capabilities;
namespace StaadPro.Interop.Adapters.Models.Capabilities
{
    internal sealed class OSRootAdinaExportAdapter : IOSRootAdinaExport
    {
        private readonly RootCapabilityBinding binding;
        internal OSRootAdinaExportAdapter(RootCapabilityBinding binding) { this.binding = binding; }
        /// <summary>Requests exporting the bound model to native-selected ADINA input and log files.</summary>
        /// <param name="axisSystem">Defined axis convention: Preserve=0, YUp=1 or ZUp=2.</param>
        /// <param name="loadCaseHandling">Defined conversion: MultipleLoadCases=0 or TimeHistory=1.</param>
        /// <returns>An immutable native status result, including warning success and unknown codes.</returns>
        /// <remarks><para>Available only through root.Capabilities.GetAdinaExport after exact build and signature checks.
        /// Calls ExportModelToADINA once with two Int32 options, after validating both and confirming the native-version snapshot.
        /// Native codes are zero success, -14501 warning success, -14502 failure and -101 no open model.</para>
        /// <para>The native server chooses the output location and overwrite behavior; it creates an ADINA .in file and an
        /// .ADINA.log companion. This API supplies no output path, atomic overwrite guarantee, filesystem preflight or rollback.
        /// Use a controlled model/destination and verify outputs separately. No SaveModel, analysis, login, alternate signature
        /// or retry is added. Calls may block or show UI. Use the acquired COM thread and coordinate shared session access.
        /// Availability proves metadata compatibility under the configured policy, not live runtime compatibility or licensing.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">Either option is undefined; rejected before native access.</exception>
        /// <exception cref="InvalidOperationException">The version snapshot changed or a native response has an invalid shape.</exception>
        /// <exception cref="ObjectDisposedException">The owning wrapper was disposed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">A native request fails; the original failure propagates without mutation retries.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired object lacks a compatible operation or dependency.</exception>
        /// <example><code>
        /// var exportGrant = root.Capabilities.GetAdinaExport();
        /// bool exportRequested = false; // enable only for an intended native export with controlled destinations
        /// if (exportGrant.IsAvailable &amp;&amp; exportRequested)
        /// {
        ///     var result = exportGrant.Capability.ExportModelToAdina(
        ///         StaadPro.Interop.Enums.Root.AdinaAxisSystem.Preserve,
        ///         StaadPro.Interop.Enums.Root.AdinaLoadCaseHandling.MultipleLoadCases);
        ///     Console.WriteLine(result.NativeStatus);
        /// }
        /// </code></example>
        public AdinaExportResult ExportModelToAdina(AdinaAxisSystem axisSystem, AdinaLoadCaseHandling loadCaseHandling)
        {
            RootAdinaExportService.Validate(axisSystem, loadCaseHandling);
            binding.EnsureCurrent();
            return RootAdinaExportService.Export(binding.Root, axisSystem, loadCaseHandling);
        }
    }
}
