namespace StaadPro.Interop.Enums.Design
{
    /// <summary>Distinguishes successful native evidence, ordinary failure, absence and an unattempted batch item.</summary>
    public enum DesignOperationStatus
    {
        /// <summary>The documented native success contract and any output validation passed.</summary>
        Success,
        /// <summary>The native function returned its documented -1 failure.</summary>
        NativeFailure,
        /// <summary>GetDesignBriefCode returned zero: the brief was not found.</summary>
        NotFound,
        /// <summary>The item was not dispatched because brief creation or an earlier mutation failed.</summary>
        NotAttempted
    }
}
