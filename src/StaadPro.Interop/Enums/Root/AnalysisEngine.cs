namespace StaadPro.Interop.Enums.Root
{
    /// <summary>Selects the documented engine for the native AnalyzeModel request.</summary>
    /// <remarks>Only native code -1 is documented in the reviewed 2024–2026 contracts.
    /// Undefined values are rejected before dispatch; this enum does not add engine availability or licensing guarantees.</remarks>
    public enum AnalysisEngine
    {
        /// <summary>Native code -1: request the STAAD.Pro analysis engine.</summary>
        StaadPro = -1
    }
}
