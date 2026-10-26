using System.Windows.Media.Media3D;

namespace StaadPro.Interop.Models.Output
{
    /// <summary>Contains only the three critical-section actions actually returned by the steel-design API.</summary>
    /// <remarks>Reuses the main-project FX/MY/MZ meanings. FY/FZ/MX are unavailable and are not fabricated as zero.</remarks>
    public sealed class SteelDesignForces
    {
        internal SteelDesignForces(double fx, double my, double mz) { Fx = fx; My = my; Mz = mz; }
        /// <summary>Gets the signed critical-section axial action in native base force units.</summary>
        public double Fx { get; }
        /// <summary>Gets the signed critical-section MY bending moment in native base force-times-length units.</summary>
        public double My { get; }
        /// <summary>Gets the signed critical-section MZ bending moment in native base force-times-length units.</summary>
        public double Mz { get; }
    }
}
