namespace StaadPro.Interop.Enums
{
    /// <summary>Controls whether OpenSTAAD displays the analysis-engine dialog.</summary>
    /// <remarks>Uses native integer codes 0 and 1; hiding this dialog does not guarantee suppression of application or error dialogs.</remarks>
    public enum HiddenMode
    {
        /// <summary>Displays the analysis-engine dialog using native code 0.</summary>
        Disable = 0,
        /// <summary>Requests hiding the analysis-engine dialog using native code 1.</summary>
        Enable = 1
    }
}
