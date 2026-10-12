namespace StaadPro.Interop.Enums
{
    /// <summary>Specifies the candidate plate corner criterion used for connectivity grouping.</summary>
    public enum PlateConnectivityScope
    {
        /// <summary>Requires consecutive candidate corners present in the compared plate; corner-only contact does not qualify.</summary>
        SharedEdge = 0,
        /// <summary>Requires at least one common distinct, non-null corner node.</summary>
        AnyCorner = 1
    }
}
