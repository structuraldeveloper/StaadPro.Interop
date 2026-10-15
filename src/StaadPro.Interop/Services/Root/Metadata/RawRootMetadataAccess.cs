using System;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;
using StaadPro.Interop.Services.Wrapper;

namespace StaadPro.Interop.Services.Root.Metadata
{
    // No wrapper construction, global acquisition, process selection or eager native reads.
    internal sealed class RawRootMetadataAccess : IRootMetadataAccess
    {
        private readonly object root;

        internal RawRootMetadataAccess(object root)
        {
            this.root = root ?? throw new ArgumentNullException(nameof(root));
        }

        public ApplicationVersion GetApplicationVersion() => WrapperMetadataReader.ReadApplicationVersion(root);
        public BaseUnitSystem GetBaseUnit() => WrapperMetadataReader.ReadBaseUnit(root);

        public ForceInputUnit GetInputUnitForForce()
        {
            ApplicationVersion version = GetApplicationVersion();
            return WrapperMetadataReader.ReadInputForceUnit(root, GetBaseUnit(), version);
        }

        public LengthInputUnit GetInputUnitForLength()
        {
            ApplicationVersion version = GetApplicationVersion();
            return WrapperMetadataReader.ReadInputLengthUnit(root, GetBaseUnit(), version);
        }

        public IntPtr GetMainWindowHandle()
        {
            ApplicationVersion version = GetApplicationVersion();
            return version != null && version.Major > 20
                ? new BoundStaadModelServices(root).GetStaadApplicationWindowHandle() : IntPtr.Zero;
        }
    }
}
