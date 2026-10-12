using System;
using StaadPro.Interop.Adapters.Interfaces;

namespace StaadPro.Interop.Adapters.Models
{
    /// <summary>Binds a raw OpenSTAAD root object without a build-time Bentley dependency.</summary>
    public sealed class OSRootAdapter : IOSRoot
    {
        /// <summary>Initializes a root binding for the supplied OpenSTAAD automation object.</summary>
        /// <param name="openStaad">The non-null raw root object from the model session.</param>
        /// <remarks>The constructor captures the object and does not launch STAAD or validate model readiness.</remarks>
        /// <exception cref="ArgumentNullException"><paramref name="openStaad"/> is null.</exception>
        public OSRootAdapter(object openStaad)
        {
            ComObject = openStaad ?? throw new ArgumentNullException(nameof(openStaad));
        }

        /// <summary>Gets the captured root OpenSTAAD automation object.</summary>
        /// <value>The original object supplied to the constructor, used for late-bound native access.</value>
        public dynamic ComObject { get; }
    }
}
