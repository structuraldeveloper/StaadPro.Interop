namespace StaadPro.Interop.Enums
{
    /// <summary>Identifies a global model axis independently of an operation's native numeric encoding.</summary>
    /// <remarks>Ported from the read-only main project's GlobalAxis. Internal callers must translate to
    /// each native domain: floor/seismic directions use 0/1/2; interpolation/range queries use 1/2/3.</remarks>
    public enum GlobalAxis
    {
        /// <summary>The global X axis.</summary>
        X = 0,
        /// <summary>The global Y axis.</summary>
        Y = 1,
        /// <summary>The global Z axis.</summary>
        Z = 2
    }
}
