namespace StaadPro.Interop.Enums.Output
{
    /// <summary>Describes whether a native Output observation supplied usable data.</summary>
    public enum OutputQueryStatus
    {
        /// <summary>The native observation completed and its data passed managed validation.</summary>
        Success = 0,
        /// <summary>The native retrieval returned its documented failure flag; no data is exposed.</summary>
        NativeFailure = 1,
        /// <summary>The native section-name query returned an empty string.</summary>
        NotFound = 2,
        /// <summary>The steel-ratio output reported the documented -1 sentinel for analysis not performed.</summary>
        AnalysisNotPerformed = 3,
        /// <summary>The steel-ratio output reported the documented -999 sentinel for a member not designed.</summary>
        MemberNotDesigned = 4
    }
}
