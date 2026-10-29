namespace StaadPro.Interop.Enums.Table
{
    /// <summary>Specifies the native report-cell horizontal alignment; adapted from the main project's alignment vocabulary.</summary>
    public enum TableHorizontalAlignment
    {
        /// <summary>Aligns text to the left; native code 0.</summary>
        Left = 0,
        /// <summary>Centers text horizontally; native code 1.</summary>
        Center = 1,
        /// <summary>Aligns text to the right; native code 2.</summary>
        Right = 2
    }

    /// <summary>Specifies the native report-cell vertical alignment; these codes differ from WPF VerticalAlignment.</summary>
    public enum TableVerticalAlignment
    {
        /// <summary>Aligns text at the top; native code 0.</summary>
        Top = 0,
        /// <summary>Centers text vertically; native code 4.</summary>
        Middle = 4,
        /// <summary>Aligns text at the bottom; native code 8.</summary>
        Bottom = 8
    }
}
