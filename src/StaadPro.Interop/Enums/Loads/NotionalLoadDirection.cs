namespace StaadPro.Interop.Enums.Loads
{
    /// <summary>Identifies the global direction of a notional contribution.</summary>
    /// <remarks>Native codes 1/2/3 are global X/Y/Z, not the local-axis meanings of LoadDirection.</remarks>
    public enum NotionalLoadDirection
    {
        /// <summary>Global X (native code 1).</summary>
        GlobalX = 1,
        /// <summary>Global Y (native code 2).</summary>
        GlobalY = 2,
        /// <summary>Global Z (native code 3).</summary>
        GlobalZ = 3,
    }
}
