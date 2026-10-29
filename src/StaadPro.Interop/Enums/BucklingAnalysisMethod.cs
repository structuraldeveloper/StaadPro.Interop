using System.ComponentModel;

namespace StaadPro.Interop.Enums
{
    /// <summary>Preserves the reviewed native BucklingAnalysisMethod codes used by analysis-command configuration.</summary>
    public enum BucklingAnalysisMethod
    {
        /// <summary>Iterative buckling analysis.</summary>
        [Description("Iterative buckling analysis")]
        Iterative = 0,
        /// <summary>Eigenvalue buckling analysis.</summary>
        [Description("Eigenvalue buckling analysis")]
        Eigen = 1
    }
}
