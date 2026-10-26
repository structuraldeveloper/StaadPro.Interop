using System.Windows.Media.Media3D;

namespace StaadPro.Interop.Models.Output
{
    /// <summary>An owned, immutable six-component force/moment snapshot in the query's documented axes.</summary>
    /// <remarks>Components preserve the enclosing query's documented sign, axes and units. Member-end actions default to local or use explicitly selected native axes; support
    /// reactions are global and GetSupportForces explicitly negates all six reaction components. Preserve their different action conventions. Use ToForces for the existing
    /// mutable vector algebra and unit-conversion model without changing this snapshot.</remarks>
    public sealed class ForceComponents
    {
        internal ForceComponents(double[] values)
        { Fx = values[0]; Fy = values[1]; Fz = values[2]; Mx = values[3]; My = values[4]; Mz = values[5]; }
        /// <summary>Gets force along the documented X axis, in native force units.</summary>
        public double Fx { get; }
        /// <summary>Gets force along the documented Y axis, in native force units.</summary>
        public double Fy { get; }
        /// <summary>Gets force along the documented Z axis, in native force units.</summary>
        public double Fz { get; }
        /// <summary>Gets moment about the documented X axis, in native force-times-length units.</summary>
        public double Mx { get; }
        /// <summary>Gets moment about the documented Y axis, in native force-times-length units.</summary>
        public double My { get; }
        /// <summary>Gets moment about the documented Z axis, in native force-times-length units.</summary>
        public double Mz { get; }
        /// <summary>Gets the three force components as an independent value vector.</summary>
        public Vector3D ForceVector => new Vector3D(Fx, Fy, Fz);
        /// <summary>Gets the three moment components as an independent value vector.</summary>
        public Vector3D MomentVector => new Vector3D(Mx, My, Mz);
        /// <summary>Creates an editable copy using the existing Forces model.</summary>
        /// <returns>A fresh Forces instance; changes and conversions cannot alter this snapshot or STAAD.</returns>
        public Forces ToForces() => new Forces(Fx, Fy, Fz, Mx, My, Mz);
    }
}
