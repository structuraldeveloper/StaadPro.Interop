namespace StaadPro.Interop.Enums.Output
{
    /// <summary>Describes whether a native Output observation supplied usable data.</summary>
    public enum OutputQueryStatus
    {
        /// <summary>The native observation completed and its data passed managed validation.</summary>
        Success = 0,
        /// <summary>The native retrieval returned its documented failure flag; no data is exposed.</summary>
        NativeFailure = 1,
        /// <summary>The native observation reported a documented not-found outcome, including an empty section name or nonlinear case -8002.</summary>
        NotFound = 2,
        /// <summary>The steel-ratio output reported the documented -1 sentinel for analysis not performed.</summary>
        AnalysisNotPerformed = 3,
        /// <summary>The steel-ratio output reported the documented -999 sentinel for a member not designed.</summary>
        MemberNotDesigned = 4,
        /// <summary>The native query or integration preflight explicitly reported no applicable result set.</summary>
        ResultsUnavailable = 5,
        /// <summary>The native sample count exceeded the caller’s managed allocation limit; no result buffer was allocated.</summary>
        LimitExceeded = 6,
        /// <summary>Native input correspondence or before/after integration metadata changed; no mixed payload is exposed.</summary>
        StateChanged = 7
    }
}
