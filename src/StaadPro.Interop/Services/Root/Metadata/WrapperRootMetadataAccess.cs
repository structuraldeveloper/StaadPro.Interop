using System;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Services.Root.Metadata
{
    // Reuses the owning wrapper's snapshots, including its configured window service.
    internal sealed class WrapperRootMetadataAccess : IRootMetadataAccess
    {
        private readonly OpenStaadWrapper wrapper;

        internal WrapperRootMetadataAccess(OpenStaadWrapper wrapper)
        {
            this.wrapper = wrapper ?? throw new ArgumentNullException(nameof(wrapper));
        }

        public ApplicationVersion GetApplicationVersion() { EnsureBound(); return wrapper.StaadVersion; }
        public BaseUnitSystem GetBaseUnit() { EnsureBound(); return wrapper.BaseUnitSystem; }
        public ForceInputUnit GetInputUnitForForce() { EnsureBound(); return wrapper.InputForceUnit; }
        public LengthInputUnit GetInputUnitForLength() { EnsureBound(); return wrapper.InputLengthUnit; }
        public IntPtr GetMainWindowHandle() { EnsureBound(); return wrapper.StaadApplicationWindowHandle; }

        private void EnsureBound()
        {
            if (wrapper.RawOpenStaad == null) throw new ObjectDisposedException(nameof(OpenStaadWrapper));
        }
    }
}
