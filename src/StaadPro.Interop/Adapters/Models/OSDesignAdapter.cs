using System;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Adapters.Models
{
    /// <summary>Binds the wrapper’s native Design object for future domain-function ports.</summary>
    /// <remarks>This is a binding placeholder. No typed Design domain operations are implemented by this class.</remarks>
    public sealed class OSDesignAdapter : OSBaseAdapter, IOSDesign
    {
        /// <summary>Captures the supplied wrapper’s Design interface without calling its native functions.</summary>
        /// <param name="wrapper">The connected wrapper whose RawDesign must be non-null.</param>
        /// <exception cref="ArgumentNullException">The wrapper or its RawDesign binding is null.</exception>
        public OSDesignAdapter(OpenStaadWrapper wrapper) : base(wrapper)
        {
            ComObject = wrapper.RawDesign ?? throw new ArgumentNullException(nameof(wrapper.RawDesign));
        }

        /// <summary>Gets the captured native Design automation object.</summary>
        /// <value>The same object that RawDesign exposed at construction; refresh the wrapper after reopening a model.</value>
        public dynamic ComObject { get; }
    }
}
