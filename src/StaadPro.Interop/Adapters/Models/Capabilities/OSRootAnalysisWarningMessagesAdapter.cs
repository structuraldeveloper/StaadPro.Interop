using System;
using StaadPro.Interop.Adapters.Interfaces.Capabilities;
using StaadPro.Interop.Enums.Root;
using StaadPro.Interop.Models.Root;
using StaadPro.Interop.Services.Root.Analysis;
using StaadPro.Interop.Services.Root.Common;
using StaadPro.Interop.Services.Root.Capabilities;
namespace StaadPro.Interop.Adapters.Models.Capabilities
{
    internal sealed class OSRootAnalysisWarningMessagesAdapter : IOSRootAnalysisWarningMessages
    {
        private readonly RootCapabilityBinding binding;
        internal OSRootAnalysisWarningMessagesAdapter(RootCapabilityBinding binding) { this.binding = binding; }
        /// <summary>Reads analysis warning messages with bounded internal BSTR buffers.</summary>
        /// <param name="modelFilePath">Non-null, non-whitespace model path, normalized to an absolute Windows path before native access. No file existence or extension check is performed.</param>
        /// <returns>An immutable outcome containing qualified text, the last native retrieval code when invoked, analysis-count evidence and zero to two attempts.</returns>
        /// <remarks><para>Acquired independently through root.Capabilities.GetAnalysisWarningMessages. Reads GetAnalysisStatus
        /// for the warning count, allocates a BSTR string array internally and calls native GetAnalysisWarningMessages using ref object.
        /// No reference parameters or caller buffers are exposed. No unavailable count is treated as zero. If count is unavailable
        /// or exceeds 100,000, returns the corresponding managed outcome with no further retrieval; NativeStatus is null if
        /// never invoked. A count of zero still invokes the native retrieval, so successful absence is not fabricated.</para>
        /// <para>Native responses: 1 retrieved, 0 successful absence, -1 general error, -2 invalid path, -107 not an array,
        /// -119 wrong array type, -108 insufficient capacity, -1001 missing source and -10004 analysis running. Unknown
        /// Int32 codes are preserved as native failures. Only -108 allows one fresh count and one more retrieval attempt;
        /// exceptions and other failures are not retried. A failed retry preflight retains the previous -108 response.</para>
        /// <para>Success requires a one-dimensional BSTR array, at most 100,000 elements, with all returned slots written
        /// as strings. Nonzero array lower bounds and native array replacement are accepted. Unwritten/null/non-string
        /// successful outputs throw; zero/failure responses expose no partial messages. Text order, empty strings, whitespace,
        /// Unicode and line breaks are preserved in an independent read-only collection. Success describes retrieval, not
        /// analysis success or current result freshness. The native source is the ANL file.</para>
        /// <para>Confirms the captured native-version snapshot before work. No analysis, save, file parsing, source deletion,
        /// global acquisition, thread migration or rollback is added. Counts/retrieval are sequential and may race native changes.
        /// Use the bound COM thread, coordinate session access and expect native calls to block. A grant is not runtime certification.</para></remarks>
        /// <exception cref="ArgumentNullException">modelFilePath is null; rejected before native access.</exception>
        /// <exception cref="ArgumentException">The path is blank or has invalid Windows syntax.</exception>
        /// <exception cref="System.IO.PathTooLongException">The runtime rejects the path length.</exception>
        /// <exception cref="NotSupportedException">The path format is unsupported.</exception>
        /// <exception cref="InvalidOperationException">The version snapshot changed or a native response has an invalid shape.</exception>
        /// <exception cref="ObjectDisposedException">The owning wrapper was disposed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">A native request fails; the original failure propagates without mutation retries.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired object lacks a compatible operation or dependency.</exception>
        /// <example><code>
        /// var messageGrant = root.Capabilities.GetAnalysisWarningMessages();
        /// if (messageGrant.IsAvailable)
        /// {
        ///     var result = messageGrant.Capability.GetAnalysisWarningMessages(@"C:\Models\Frame.std");
        ///     Console.WriteLine(result.Outcome);
        ///     foreach (string message in result.Messages) Console.WriteLine(message);
        /// }
        /// </code></example>
        public AnalysisMessagesResult GetAnalysisWarningMessages(string modelFilePath)
        {
            string normalizedPath = RootModelPaths.Normalize(modelFilePath, nameof(modelFilePath));
            binding.EnsureCurrent();
            return RootAnalysisMessagesService.Read(binding.Root, normalizedPath, false);
        }
    }
}
