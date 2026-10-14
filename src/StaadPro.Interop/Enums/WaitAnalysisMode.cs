namespace StaadPro.Interop.Enums
{
    /// <summary>Controls whether the native analysis request waits for engine completion.</summary>
    public enum WaitAnalysisMode
    {
        /// <summary>Requests returning without waiting for completion using native code 0; analysis may still be running.</summary>
        Return = 0,
        /// <summary>Requests waiting for native analysis completion using code 1; the COM call can block without a managed timeout.</summary>
        Wait = 1
    }
}
