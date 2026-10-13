namespace StaadPro.Interop.Enums
{
    /// <summary>Selects whether native load-list IDs refer to load cases or load envelopes.</summary>
    public enum LoadListType
    {
        /// <summary>Select a load-case list (native code 0).</summary>
        LoadList = 0,
        /// <summary>Select a load-envelope list (native code 1); IDs identify envelopes, not cases.</summary>
        EnvelopList = 1
    }
}
