using System;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Adapters.Models
{
    /// <summary>Binds the wrapper’s native Output object for future domain-function ports.</summary>
    /// <remarks>This is a binding placeholder. No typed Output domain operations are implemented by this class.</remarks>
    public sealed class OSOutputAdapter : OSBaseAdapter, IOSOutput
    {
        /// <summary>Captures the supplied wrapper’s Output interface without calling its native functions.</summary>
        /// <param name="wrapper">The connected wrapper whose RawOutput must be non-null.</param>
        /// <exception cref="ArgumentNullException">The wrapper or its RawOutput binding is null.</exception>
        public OSOutputAdapter(OpenStaadWrapper wrapper) : base(wrapper)
        {
            ComObject = wrapper.RawOutput ?? throw new ArgumentNullException(nameof(wrapper.RawOutput));
        }

        /// <summary>Gets the captured native Output automation object.</summary>
        /// <value>The same object that RawOutput exposed at construction; refresh the wrapper after reopening a model.</value>
        public dynamic ComObject { get; }
    }
}
