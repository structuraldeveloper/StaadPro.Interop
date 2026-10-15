using System;

namespace StaadPro.Interop.Services
{
    /// <summary>Supplies the model-service operation required by the wrapper's window-handle refresh.</summary>
    /// <remarks>Only the source service contract used by OpenStaadWrapper is ported here.
    /// File creation, model coordination and editing services remain separate work.</remarks>
    public interface IStaadModelServices
    {
        /// <summary>Obtains the window handle for the host's explicitly selected STAAD instance.</summary>
        /// <returns>A non-owning window handle, or zero when unavailable.</returns>
        /// <remarks>An injected implementation must select the same instance as the wrapper.
        /// The built-in wrapper implementation reads its captured OpenSTAAD root directly.</remarks>
        IntPtr GetStaadApplicationWindowHandle();
    }
}
