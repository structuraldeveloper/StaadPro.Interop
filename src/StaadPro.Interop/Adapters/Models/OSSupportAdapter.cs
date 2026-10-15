using System;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Adapters.Models
{
    /// <summary>Binds the wrapper’s native Support object for future domain-function ports.</summary>
    /// <remarks>This is a binding placeholder. No typed Support domain operations are implemented by this class.</remarks>
    public sealed class OSSupportAdapter : OSBaseAdapter, IOSSupport
    {
        /// <summary>Captures the supplied wrapper’s Support interface without calling its native functions.</summary>
        /// <param name="wrapper">The connected wrapper whose RawSupport must be non-null.</param>
        /// <exception cref="ArgumentNullException">The wrapper or its RawSupport binding is null.</exception>
        public OSSupportAdapter(OpenStaadWrapper wrapper) : base(wrapper)
        {
            ComObject = wrapper.RawSupport ?? throw new ArgumentNullException(nameof(wrapper.RawSupport));
        }

        /// <summary>Gets the captured native Support automation object.</summary>
        /// <value>The same object that RawSupport exposed at construction; refresh the wrapper after reopening a model.</value>
        public dynamic ComObject { get; }
    }
}
