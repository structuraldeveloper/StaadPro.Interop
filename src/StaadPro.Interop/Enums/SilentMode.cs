namespace StaadPro.Interop.Enums
{
    /// <summary>Specifies whether OpenSTAAD suppresses supported warning dialogs during analysis or saving.</summary>
    /// <remarks>Uses native integer codes 0 and 1. Silent mode does not suppress every dialog: licence and inaccessible-folder errors may still require user interaction.</remarks>
    public enum SilentMode
    {
        /// <summary>Disables warning suppression using native code 0; supported warning dialogs can require user attention.</summary>
        Disable = 0,

        /// <summary>Enables suppression of supported warnings using native code 1; errors and other dialogs can still be displayed.</summary>
        Enable = 1
    }
}
