using System;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Adapters.Models
{
    /// <summary>Binds the wrapper’s native Table object for future domain-function ports.</summary>
    /// <remarks>This is a binding placeholder. No typed Table domain operations are implemented by this class.</remarks>
    public sealed class OSTableAdapter : OSBaseAdapter, IOSTable
    {
        /// <summary>Captures the supplied wrapper’s Table interface without calling its native functions.</summary>
        /// <param name="wrapper">The connected wrapper whose RawTable must be non-null.</param>
        /// <exception cref="ArgumentNullException">The wrapper or its RawTable binding is null.</exception>
        public OSTableAdapter(OpenStaadWrapper wrapper) : base(wrapper)
        {
            ComObject = wrapper.RawTable ?? throw new ArgumentNullException(nameof(wrapper.RawTable));
        }

        /// <summary>Gets the captured native Table automation object.</summary>
        /// <value>The same object that RawTable exposed at construction; refresh the wrapper after reopening a model.</value>
        public dynamic ComObject { get; }
    }
}
