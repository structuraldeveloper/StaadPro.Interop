using StaadPro.Interop.Models;

namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>Reserves the wrapper's IDEA StatiCa integration binding for later implementation.</summary>
    /// <remarks>No force transformation, verification or export operations have been ported.
    /// Presence means the required Geometry, Load and Output bindings exist, not that integration functions are available.</remarks>
    public interface IOpenStaadIdeaStatiCaAdapter
    {
        /// <summary>Gets the wrapper captured by this placeholder binding.</summary>
        OpenStaadWrapper Wrapper { get; }
    }
}
