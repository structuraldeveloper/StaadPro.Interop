using StaadPro.Interop.Models;

namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>
    /// Base contract for OpenSTAAD interop adapters bound to a geometry session.
    /// </summary>
    public interface IOSBase
    {
        /// <summary>
        /// Gets the parent STAAD session object.
        /// </summary>
        StaadGeometrySession Session { get; }
    }
}
