using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StaadPro.Interop.Enums
{
    /// <summary>Preserves the main-project whole-packet release classification; inspect per-DOF native evidence for mixed modes.</summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ReleaseType
    {
        /// <summary>No Boolean release, spring or partial-moment mode is selected.</summary>
        [Description("No Release")] NoRelease = 0,
        /// <summary>One or more full releases are selected.</summary>
        Release = 1,
        /// <summary>One or more spring stiffnesses or native spring modes are present.</summary>
        [Description("Release with Springs")] ReleaseWithSprings = 2,
        /// <summary>A native common partial-moment mode is present.</summary>
        [Description("MP Only")] MpOnly = 3,
        /// <summary>A native directional partial-moment mode is present.</summary>
        [Description("MPx MPy MPz")] MpAll = 4
    }
}
