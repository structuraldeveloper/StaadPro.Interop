namespace StaadPro.Interop.Enums
{
    /// <summary>Classifies the STAAD application family using its observed major version.</summary>
    /// <remarks>This is a family classification, not a feature-availability guarantee.</remarks>
    public enum ApplicationEdition
    {
        /// <summary>No native version is available.</summary>
        None = 0,
        /// <summary>The native major version is 20 or earlier, matching the source wrapper's rule.</summary>
        V8i = 1,
        /// <summary>The native major version is greater than 20, matching the source wrapper's rule.</summary>
        Connect = 2
    }
}
