namespace StaadPro.Interop.Models.Output
{
    /// <summary>Contains the five plate-center stress components from GetAllPlateCenterForces.</summary>
    /// <remarks>The native function name says forces, but these five values are stresses, not
    /// concentrated forces. Components use the plate-local axes and native stress units.</remarks>
    public sealed class PlateCenterForces
    {
        internal PlateCenterForces(double[] v) { Sqx = v[0]; Sqy = v[1]; Sx = v[2]; Sy = v[3]; Sxy = v[4]; }
        /// <summary>Gets shear stress on the local X face in local Z.</summary>
        public double Sqx { get; }
        /// <summary>Gets shear stress on the local Y face in local Z.</summary>
        public double Sqy { get; }
        /// <summary>Gets axial stress in the local X direction.</summary>
        public double Sx { get; }
        /// <summary>Gets axial stress in the local Y direction.</summary>
        public double Sy { get; }
        /// <summary>Gets shear stress in the local X-Y plane.</summary>
        public double Sxy { get; }
    }

    /// <summary>Contains plate-center moments per unit width in plate-local axes.</summary>
    public sealed class PlateCenterMoments
    {
        internal PlateCenterMoments(double[] v) { Mx = v[0]; My = v[1]; Mxy = v[2]; }
        /// <summary>Gets moment per unit width about the local X face, in native units.</summary>
        public double Mx { get; }
        /// <summary>Gets moment per unit width about the local Y face, in native units.</summary>
        public double My { get; }
        /// <summary>Gets torsional moment per unit width in the local X-Y plane, in native units.</summary>
        public double Mxy { get; }
    }

    /// <summary>Contains the eight native plate-center stresses and moments as named groups.</summary>
    public sealed class PlateCenterStressesAndMoments
    {
        internal PlateCenterStressesAndMoments(double[] v)
        {
            Forces = new PlateCenterForces(new[] { v[0], v[1], v[5], v[6], v[7] });
            Moments = new PlateCenterMoments(new[] { v[2], v[3], v[4] });
        }
        /// <summary>Gets Sqx/Sqy/Sx/Sy/Sxy stresses, preserving their distinct stress dimension.</summary>
        public PlateCenterForces Forces { get; }
        /// <summary>Gets Mx/My/Mxy moments per unit width.</summary>
        public PlateCenterMoments Moments { get; }
    }

    /// <summary>Contains top and bottom in-plane normal principal stresses in native stress units.</summary>
    public sealed class PlatePrincipalStresses
    {
        internal PlatePrincipalStresses(double[] v) { MaximumTop = v[0]; MinimumTop = v[1]; MaximumBottom = v[2]; MinimumBottom = v[3]; }
        /// <summary>Gets the maximum principal stress on the plate's top surface.</summary>
        public double MaximumTop { get; }
        /// <summary>Gets the minimum principal stress on the plate's top surface.</summary>
        public double MinimumTop { get; }
        /// <summary>Gets the maximum principal stress on the plate's bottom surface.</summary>
        public double MaximumBottom { get; }
        /// <summary>Gets the minimum principal stress on the plate's bottom surface.</summary>
        public double MinimumBottom { get; }
    }

    /// <summary>Contains six top/bottom principal/shear stresses and two native principal angles.</summary>
    /// <remarks>Stress fields use native stress units. Angles describe maximum-principal directions
    /// relative to local X; their native angular units are preserved without inferred conversion.</remarks>
    public sealed class PlatePrincipalStressesAndAngles
    {
        internal PlatePrincipalStressesAndAngles(double[] v)
        {
            MaximumTop = v[0]; MinimumTop = v[1]; MaximumShearTop = v[2];
            MaximumBottom = v[3]; MinimumBottom = v[4]; MaximumShearBottom = v[5];
            AngleTop = v[6]; AngleBottom = v[7];
        }
        /// <summary>Gets top-surface maximum in-plane principal stress.</summary>
        public double MaximumTop { get; }
        /// <summary>Gets top-surface minimum in-plane principal stress.</summary>
        public double MinimumTop { get; }
        /// <summary>Gets top-surface maximum in-plane shear stress.</summary>
        public double MaximumShearTop { get; }
        /// <summary>Gets bottom-surface maximum in-plane principal stress.</summary>
        public double MaximumBottom { get; }
        /// <summary>Gets bottom-surface minimum in-plane principal stress.</summary>
        public double MinimumBottom { get; }
        /// <summary>Gets bottom-surface maximum in-plane shear stress.</summary>
        public double MaximumShearBottom { get; }
        /// <summary>Gets the top maximum-principal direction relative to local X, in native angular units.</summary>
        public double AngleTop { get; }
        /// <summary>Gets the bottom maximum-principal direction relative to local X, in native angular units.</summary>
        public double AngleBottom { get; }
    }

    /// <summary>Contains plate-center top and bottom Von Mises stresses in native stress units.</summary>
    public sealed class PlateCenterVonMises
    {
        internal PlateCenterVonMises(double top, double bottom) { Top = top; Bottom = bottom; }
        /// <summary>Gets the Von Mises stress on the plate's top surface.</summary>
        public double Top { get; }
        /// <summary>Gets the Von Mises stress on the plate's bottom surface.</summary>
        public double Bottom { get; }
    }
}
