namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>Provides the native Design binding reserved for separately ported domain operations.</summary>
    /// <remarks>This placeholder contains no ported domain functions. It exposes only the captured native object.</remarks>
    public interface IOSDesign : IOSBase
    {
        /// <summary>Gets the native Design automation object captured by this adapter.</summary>
        /// <value>The connected raw interface; native calls use the session’s COM-thread and lifetime rules.</value>
        dynamic ComObject { get; }
    }
}
