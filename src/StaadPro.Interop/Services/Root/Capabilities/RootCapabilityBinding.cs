using System;
using StaadPro.Interop.Models;
using StaadPro.Interop.Services.Root.Metadata;
namespace StaadPro.Interop.Services.Root.Capabilities
{
    internal sealed class RootCapabilityBinding
    {
        private readonly IRootMetadataAccess metadata;
        private readonly Version version;
        internal RootCapabilityBinding(object root, IRootMetadataAccess metadata, Version version)
        { Root = root; this.metadata = metadata; this.version = version; }
        internal object Root { get; }
        internal void EnsureCurrent()
        {
            if (!Equals(version, Freeze(metadata.GetApplicationVersion())))
                throw new InvalidOperationException("The native version snapshot changed. Acquire this capability again.");
        }
        internal static Version Freeze(ApplicationVersion value)
        {
            if (value == null || value.Major < 0 || value.Update < 0 || value.Revision < 0 || value.Build < 0) return null;
            return new Version(value.Major, value.Update, value.Revision, value.Build);
        }
    }

    internal static class RootCapabilityProfiles
    {
        internal static bool IsCandidateYear(Version version, bool export) =>
            export ? version.Major == 25 || version.Major == 26 : version.Major == 26;

        internal static bool IsCoveredBuild(Version version) =>
            version.Equals(new Version(25, 0, 1, 424)) || version.Equals(new Version(26, 0, 0, 340));
    }
}
