using System.Windows.Media.Media3D;

namespace StaadPro.Interop.Models.Output
{
    /// <summary>Contains the six unconverted nodal translations/rotations in native X/Y/Z order.</summary>
    /// <remarks>Retains the main-source X/Y/Z/Rx/Ry/Rz vocabulary as an immutable snapshot.
    /// Values use the acquired result's native length/angular units; input-unit caches are not proof
    /// of those units. No local-basis transformation, degree/radian conversion or scaling is performed.</remarks>
    public sealed class NodeDisplacements
    {
        internal NodeDisplacements(double[] values)
        { X = values[0]; Y = values[1]; Z = values[2]; Rx = values[3]; Ry = values[4]; Rz = values[5]; }
        /// <summary>Gets translation along native X in native result length units.</summary>
        public double X { get; }
        /// <summary>Gets translation along native Y in native result length units.</summary>
        public double Y { get; }
        /// <summary>Gets translation along native Z in native result length units.</summary>
        public double Z { get; }
        /// <summary>Gets rotation about native X in native result angular units.</summary>
        public double Rx { get; }
        /// <summary>Gets rotation about native Y in native result angular units.</summary>
        public double Ry { get; }
        /// <summary>Gets rotation about native Z in native result angular units.</summary>
        public double Rz { get; }
        /// <summary>Gets X/Y/Z translations as an independent value vector.</summary>
        public Vector3D TranslationVector => new Vector3D(X, Y, Z);
        /// <summary>Gets Rx/Ry/Rz rotations as an independent value vector.</summary>
        public Vector3D RotationVector => new Vector3D(Rx, Ry, Rz);
    }
}
