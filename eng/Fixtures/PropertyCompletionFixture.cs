namespace PackageVerification
{
    // Managed stand-in for installed-package verification; never creates or modifies a native STAAD model.
    public static class PropertyCompletionFixture
    {
        public sealed class Root
        {
            public object Property { get; set; }
            public object Geometry => null;
            public object GetApplicationVersion(ref object major, ref object update, ref object revision, ref object build)
            { major = 26; update = 0; revision = 0; build = 340; return "26.0.0.340"; }
        }
        public class Native : PropertyCleanupFixture.Native
        {
            public object RemoveAllElementNodeReleaseSpec() => R(nameof(RemoveAllElementNodeReleaseSpec), true);
            public object RemoveMemberFireProofingSpecFromBeam(int memberId) => R(nameof(RemoveMemberFireProofingSpecFromBeam), true, memberId);
            public object GetPublishedProfileName(string name, int country) => R(nameof(GetPublishedProfileName), " EXACT PUBLISHED ", name, country);
            public object GetSTAADProfileName(string name, int country) => R(nameof(GetSTAADProfileName), " EXACT STAAD ", name, country);
        }
    }
}
