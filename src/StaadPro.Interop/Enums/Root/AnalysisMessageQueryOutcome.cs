namespace StaadPro.Interop.Enums.Root
{
    /// <summary>Distinguishes managed preflight outcomes from native message retrieval responses.</summary>
    public enum AnalysisMessageQueryOutcome
    {
        /// <summary>The native retrieval returned messages successfully. Value 0.</summary>
        Retrieved = 0,

        /// <summary>The native retrieval reported successful absence. Value 1.</summary>
        NoMessages = 1,

        /// <summary>Retrieval returned a failure or unknown native code. Value 2.</summary>
        NativeFailure = 2,

        /// <summary>The status query supplied no usable count; no further retrieval was attempted. Value 3.</summary>
        AnalysisCountUnavailable = 3,

        /// <summary>The required count exceeds the bounded allocation policy. Value 4.</summary>
        MessageLimitExceeded = 4
    }
}
