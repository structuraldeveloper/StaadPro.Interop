namespace StaadPro.Interop.Enums
{
    /// <summary>Represents the native AnalyzeEx status for a STAAD analysis request.</summary>
    /// <remarks>Preserves native integer codes. Future or unrecognized integer statuses can be returned as undefined enum values; callers must not treat them as successful analysis.</remarks>
    public enum AnalysisResult
    {
        /// <summary>Native code -1: analysis was terminated.</summary>
        Terminated = -1,
        /// <summary>Native code 0: a general analysis error occurred.</summary>
        GeneralError = 0,
        /// <summary>Native code 1: analysis is still in progress.</summary>
        InProgress = 1,
        /// <summary>Native code 2: analysis completed without reported warnings or errors.</summary>
        CompletedSuccessfully = 2,
        /// <summary>Native code 3: analysis completed with warnings and no reported errors.</summary>
        CompletedWithWarnings = 3,
        /// <summary>Native code 4: analysis completed with reported errors.</summary>
        CompletedWithErrors = 4,
        /// <summary>Native code 5: analysis was not performed.</summary>
        NotPerformed = 5
    }
}
