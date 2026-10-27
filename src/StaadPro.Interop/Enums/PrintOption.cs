using System.ComponentModel;

namespace StaadPro.Interop.Enums
{
    /// <summary>Preserves the reviewed native PrintOption codes used by analysis-command configuration.</summary>
    public enum PrintOption
    {
        /// <summary>No print output.</summary>
        [Description("No print output")]
        NoPrint = 0,
        /// <summary>Print load data.</summary>
        [Description("Print load data")]
        PrintLoadData = 1,
        /// <summary>Print statics check.</summary>
        [Description("Print statics check")]
        PrintStaticsCheck = 2,
        /// <summary>Print statics and load data.</summary>
        [Description("Print statics and load data")]
        PrintStaticsLoad = 3,
        /// <summary>Print mode shapes.</summary>
        [Description("Print mode shapes")]
        PrintModeShapes = 4,
        /// <summary>Print load data and statics check.</summary>
        [Description("Print load data and statics check")]
        PrintBoth = 5,
        /// <summary>Print all available analysis output.</summary>
        [Description("Print all available analysis output")]
        PrintAll = 6
    }
}
