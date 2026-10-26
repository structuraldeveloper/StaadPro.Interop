using System.Windows.Media.Media3D;

namespace StaadPro.Interop.Models.Output
{
    /// <summary>Contains the three named native solid shear stress components.</summary>
    /// <remarks>Preserves SXY/SYZ/SZX slot ordering and signs; no coordinate rotation or stress-unit conversion is applied.</remarks>
    public sealed class SolidShearStresses
    {
        internal SolidShearStresses(double sxy, double syz, double szx) { Sxy = sxy; Syz = syz; Szx = szx; }
        /// <summary>Gets native shear stress SXY in native stress units.</summary>
        public double Sxy { get; }
        /// <summary>Gets native shear stress SYZ in native stress units.</summary>
        public double Syz { get; }
        /// <summary>Gets native shear stress SZX in native stress units.</summary>
        public double Szx { get; }
    }
}
