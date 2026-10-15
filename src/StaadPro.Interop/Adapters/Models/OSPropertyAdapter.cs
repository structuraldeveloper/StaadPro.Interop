using System;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Adapters.Models
{
    /// <summary>Binds the wrapper’s native Property object for future domain-function ports.</summary>
    /// <remarks>This is a binding placeholder. No typed Property domain operations are implemented by this class.</remarks>
    public sealed class OSPropertyAdapter : OSBaseAdapter, IOSProperty
    {
        /// <summary>Captures the supplied wrapper’s Property interface without calling its native functions.</summary>
        /// <param name="wrapper">The connected wrapper whose RawProperty must be non-null.</param>
        /// <exception cref="ArgumentNullException">The wrapper or its RawProperty binding is null.</exception>
        public OSPropertyAdapter(OpenStaadWrapper wrapper) : base(wrapper)
        {
            ComObject = wrapper.RawProperty ?? throw new ArgumentNullException(nameof(wrapper.RawProperty));
        }

        /// <summary>Gets the captured native Property automation object.</summary>
        /// <value>The same object that RawProperty exposed at construction; refresh the wrapper after reopening a model.</value>
        public dynamic ComObject { get; }
    }
}
