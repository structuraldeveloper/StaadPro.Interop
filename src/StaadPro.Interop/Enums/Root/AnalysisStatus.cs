namespace StaadPro.Interop.Enums.Root
{
    /// <summary>Identifies a documented native GetAnalysisStatus response.</summary>
    /// <remarks>Describes both query failures and analysis states. It is separate from the AnalyzeEx request result.
    /// Unknown future integer codes remain available through AnalysisStatusResult.NativeStatus and have no recognized enum value.</remarks>
    public enum AnalysisStatus
    {
        /// <summary>Native code -2: the supplied model path is invalid to the native query.</summary>
        InvalidModelPath = -2,
        /// <summary>Native code -1: the analysis was terminated; its state was retrieved.</summary>
        Terminated = -1,
        /// <summary>Native code 0: the native query reported a general error.</summary>
        GeneralError = 0,
        /// <summary>Native code 1: analysis is running.</summary>
        InProgress = 1,
        /// <summary>Native code 2: analysis completed without reported errors or warnings.</summary>
        CompletedSuccessfully = 2,
        /// <summary>Native code 3: analysis completed with warnings and without reported errors.</summary>
        CompletedWithWarnings = 3,
        /// <summary>Native code 4: analysis completed with reported errors.</summary>
        CompletedWithErrors = 4,
        /// <summary>Native code 5: analysis has not been performed.</summary>
        NotPerformed = 5
    }
}
