using StaadPro.Interop.Enums.Root;

namespace StaadPro.Interop.Models.Root
{
    /// <summary>Contains an immutable snapshot of one model's native analysis-status query.</summary>
    /// <remarks>Successful retrieval and successful analysis are different outcomes. A terminated, running,
    /// completed-with-errors or not-performed state can be retrieved successfully. Inspect Status before using analysis results.
    /// Counts and CPU time are optional for noncompleted states; native query failures and unknown statuses expose no output data.
    /// This snapshot does not track subsequent model edits or prove that stored results match the current model.</remarks>
    public sealed class AnalysisStatusResult
    {
        internal AnalysisStatusResult(string modelFilePath, int nativeStatus, int? warningCount, int? errorCount, double? cpuTimeSeconds)
        {
            ModelFilePath = modelFilePath;
            NativeStatus = nativeStatus;
            Status = nativeStatus >= -2 && nativeStatus <= 5 ? (AnalysisStatus?)nativeStatus : null;
            WarningCount = warningCount;
            ErrorCount = errorCount;
            CpuTimeSeconds = cpuTimeSeconds;
        }

        /// <summary>Gets the absolute model path sent to this query.</summary>
        /// <value>The normalized requested path; no filesystem existence, file identity or result freshness is certified.</value>
        public string ModelFilePath { get; }

        /// <summary>Gets the exact signed 32-bit status returned by OpenSTAAD.</summary>
        /// <value>The native code, including documented failures and unknown future values.</value>
        public int NativeStatus { get; }

        /// <summary>Gets the recognized native query response or analysis state.</summary>
        /// <value>A documented AnalysisStatus for native codes -2 through 5; null for an unknown code.</value>
        public AnalysisStatus? Status { get; }

        /// <summary>Gets whether the query retrieved a recognized analysis state.</summary>
        /// <value>True for -1 and 1 through 5; false for invalid path (-2), general error (0) and unknown codes.</value>
        /// <remarks>True does not mean analysis completed or succeeded. In particular, Terminated, InProgress,
        /// CompletedWithErrors and NotPerformed all indicate a retrieved state. Inspect Status for the analysis outcome.</remarks>
        public bool IsSuccessful => Status == AnalysisStatus.Terminated ||
            (NativeStatus >= 1 && NativeStatus <= 5);

        /// <summary>Gets whether the reported state represents a completed analysis.</summary>
        /// <value>True for native codes 2, 3 and 4, including completion with errors; false otherwise.</value>
        public bool IsCompleted => NativeStatus >= 2 && NativeStatus <= 4;

        /// <summary>Gets the warning count written by the native query, when available.</summary>
        /// <value>A nonnegative count, including a valid zero; null for an unwritten noncompleted-state output, a failed query or an unknown status.</value>
        public int? WarningCount { get; }

        /// <summary>Gets the error count written by the native query, when available.</summary>
        /// <value>A nonnegative count, including a valid zero; null for an unwritten noncompleted-state output, a failed query or an unknown status.</value>
        public int? ErrorCount { get; }

        /// <summary>Gets the native analysis CPU time in seconds, when available.</summary>
        /// <value>A finite nonnegative duration expressed in seconds, preserving native Double precision; null when unavailable or the query failed or returned an unknown status.</value>
        /// <remarks>This is CPU time reported by STAAD, not wall-clock time measured by this library.
        /// A NaN sentinel is considered unavailable for noncompleted states; completion requires a finite written value.</remarks>
        public double? CpuTimeSeconds { get; }
    }
}
