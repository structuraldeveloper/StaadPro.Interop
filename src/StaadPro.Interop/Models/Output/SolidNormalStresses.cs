using System.Windows.Media.Media3D;

namespace StaadPro.Interop.Models.Output
{
    /// <summary>Contains the three named native solid normal stress components.</summary>
    /// <remarks>The requested native corner selector is passed unchanged. No corner renumbering, principal rotation or stress-unit conversion occurs.</remarks>
    public sealed class SolidNormalStresses
    {
        internal SolidNormalStresses(double sxx, double syy, double szz) { Sxx = sxx; Syy = syy; Szz = szz; }
        /// <summary>Gets native normal stress SXX in native stress units.</summary>
        public double Sxx { get; }
        /// <summary>Gets native normal stress SYY in native stress units.</summary>
        public double Syy { get; }
        /// <summary>Gets native normal stress SZZ in native stress units.</summary>
        public double Szz { get; }
    }
}
