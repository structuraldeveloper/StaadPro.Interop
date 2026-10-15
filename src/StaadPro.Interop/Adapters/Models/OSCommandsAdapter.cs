using System;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Adapters.Models
{
    /// <summary>Binds the wrapper’s native Commands object for future domain-function ports.</summary>
    /// <remarks>This is a binding placeholder. No typed Commands domain operations are implemented by this class.</remarks>
    public sealed class OSCommandsAdapter : OSBaseAdapter, IOSCommands
    {
        /// <summary>Captures the supplied wrapper’s Commands interface without calling its native functions.</summary>
        /// <param name="wrapper">The connected wrapper whose RawCommands must be non-null.</param>
        /// <exception cref="ArgumentNullException">The wrapper or its RawCommands binding is null.</exception>
        public OSCommandsAdapter(OpenStaadWrapper wrapper) : base(wrapper)
        {
            ComObject = wrapper.RawCommands ?? throw new ArgumentNullException(nameof(wrapper.RawCommands));
        }

        /// <summary>Gets the captured native Commands automation object.</summary>
        /// <value>The same object that RawCommands exposed at construction; refresh the wrapper after reopening a model.</value>
        public dynamic ComObject { get; }
    }
}
