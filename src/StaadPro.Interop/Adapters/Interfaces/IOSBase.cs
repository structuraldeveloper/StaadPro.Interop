using StaadPro.Interop.Models;

namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>
    /// Base contract for OpenSTAAD interop adapters bound to an OpenStaadWrapper.
    /// </summary>
    public interface IOSBase
    {
        /// <summary>
        /// Gets the parent OpenStaadWrapper instance.
        /// </summary>
        OpenStaadWrapper Wrapper { get; }
    }
}
