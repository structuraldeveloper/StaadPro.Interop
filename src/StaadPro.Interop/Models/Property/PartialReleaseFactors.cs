using System;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains six explicit immutable dimensionless member partial-release factors.</summary>
    /// <remarks>All six values are forwarded in native FX/FY/FZ/MX/MY/MZ order, including entries for unselected flags. This packet is not the MP/MPX/MPY/MPZ read-back representation and carries no created specification ID.</remarks>
    public sealed class PartialReleaseFactors
    {
        /// <summary>Validates and copies the complete explicit input packet.</summary>
        /// <param name="xTranslation">Finite factor from zero through one for native FX translation; no release-mode inference is performed.</param>
        /// <param name="yTranslation">Finite factor from zero through one for native FY translation; no release-mode inference is performed.</param>
        /// <param name="zTranslation">Finite factor from zero through one for native FZ translation; no release-mode inference is performed.</param>
        /// <param name="xRotation">Finite factor from zero through one for native MX rotation; no release-mode inference is performed.</param>
        /// <param name="yRotation">Finite factor from zero through one for native MY rotation; no release-mode inference is performed.</param>
        /// <param name="zRotation">Finite factor from zero through one for native MZ rotation; no release-mode inference is performed.</param>
        /// <exception cref="ArgumentOutOfRangeException">A numeric input violates the documented managed preconditions.</exception>
        public PartialReleaseFactors(double xTranslation, double yTranslation, double zTranslation, double xRotation, double yRotation, double zRotation)
        {
            if (double.IsNaN(xTranslation) || xTranslation < 0 || xTranslation > 1) throw new ArgumentOutOfRangeException(nameof(xTranslation));
            if (double.IsNaN(yTranslation) || yTranslation < 0 || yTranslation > 1) throw new ArgumentOutOfRangeException(nameof(yTranslation));
            if (double.IsNaN(zTranslation) || zTranslation < 0 || zTranslation > 1) throw new ArgumentOutOfRangeException(nameof(zTranslation));
            if (double.IsNaN(xRotation) || xRotation < 0 || xRotation > 1) throw new ArgumentOutOfRangeException(nameof(xRotation));
            if (double.IsNaN(yRotation) || yRotation < 0 || yRotation > 1) throw new ArgumentOutOfRangeException(nameof(yRotation));
            if (double.IsNaN(zRotation) || zRotation < 0 || zRotation > 1) throw new ArgumentOutOfRangeException(nameof(zRotation));
            Fx = xTranslation;
            Fy = yTranslation;
            Fz = zTranslation;
            Mx = xRotation;
            My = yRotation;
            Mz = zRotation;
        }
        /// <summary>Gets the explicit dimensionless factor for native FX translation.</summary>
        public double Fx { get; }
        /// <summary>Gets the explicit dimensionless factor for native FY translation.</summary>
        public double Fy { get; }
        /// <summary>Gets the explicit dimensionless factor for native FZ translation.</summary>
        public double Fz { get; }
        /// <summary>Gets the explicit dimensionless factor for native MX rotation.</summary>
        public double Mx { get; }
        /// <summary>Gets the explicit dimensionless factor for native MY rotation.</summary>
        public double My { get; }
        /// <summary>Gets the explicit dimensionless factor for native MZ rotation.</summary>
        public double Mz { get; }
    }
}
