namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>Exposes the root OpenSTAAD object used by model-specific geometry operations.</summary>
    /// <remarks>This is the initial root binding. Additional IOSRoot operations are ported separately.
    /// Native access uses runtime dispatch and requires an undisposed connection on the correct COM thread.</remarks>
    public interface IOSRoot
    {
        /// <summary>Gets the root OpenSTAAD automation object associated with this binding.</summary>
        /// <value>The raw root object; its Geometry property identifies the model modified by extended operations.</value>
        dynamic ComObject { get; }
    }
}
