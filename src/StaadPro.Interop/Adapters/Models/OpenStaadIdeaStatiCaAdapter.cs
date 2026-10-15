using System;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Adapters.Models
{
    /// <summary>Captures the wrapper for the separately planned IDEA StatiCa integration.</summary>
    /// <remarks>This placeholder performs no extraction, conversion, verification or export.</remarks>
    public sealed class OpenStaadIdeaStatiCaAdapter : OSBaseAdapter, IOpenStaadIdeaStatiCaAdapter
    {
        /// <summary>Creates an integration placeholder with the required native domain bindings.</summary>
        /// <param name="wrapper">A wrapper exposing Geometry, Load and Output adapters.</param>
        /// <exception cref="ArgumentNullException">The wrapper is null.</exception>
        /// <exception cref="ArgumentException">A required adapter is unavailable.</exception>
        public OpenStaadIdeaStatiCaAdapter(OpenStaadWrapper wrapper) : base(wrapper)
        {
            if (wrapper.Geometry == null || wrapper.Load == null || wrapper.Output == null)
                throw new ArgumentException("Geometry, Load and Output bindings are required.", nameof(wrapper));
        }
    }
}
