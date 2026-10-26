using System;
using System.Windows.Media.Media3D;
namespace StaadPro.Interop.Models.Output
{
    /// <summary>Owns a right-handed orthonormal member basis computed from supplied geometry and beta angle.</summary>
    /// <remarks>This is a managed geometry convention port, not native orientation read-back. Caller geometry, beta degrees and the global-Y reference must match the intended model. Native Output extraction should request native axes directly when possible.</remarks>
    public sealed class MemberCoordinateSystem
    {
        internal MemberCoordinateSystem(int id, Point3D origin, Vector3D x, Vector3D y, Vector3D z, double beta)
        {
            MemberId = id;
            Origin = origin;
            XAxis = x;
            YAxis = y;
            ZAxis = z;
            BetaAngleDegrees = beta;
        }
        /// <summary>Gets the supplied positive analytical member ID.</summary>
        public int MemberId
        {
            get;
        }
        /// <summary>Gets the captured start-node point in native length units.</summary>
        public Point3D Origin
        {
            get;
        }
        /// <summary>Gets unit local X from start to end.</summary>
        public Vector3D XAxis
        {
            get;
        }
        /// <summary>Gets unit local Y after the supplied beta rotation.</summary>
        public Vector3D YAxis
        {
            get;
        }
        /// <summary>Gets unit local Z completing the right-handed basis after beta rotation.</summary>
        public Vector3D ZAxis
        {
            get;
        }
        /// <summary>Gets the supplied finite beta angle in degrees.</summary>
        public double BetaAngleDegrees
        {
            get;
        }
        /// <summary>Projects a finite global vector into this managed orthonormal basis.</summary>
        /// <param name="globalVector">Finite vector with no change of units or force/moment sign convention.</param>
        /// <returns>Dot products against local X/Y/Z.</returns>
        /// <exception cref="ArgumentException">An input or computed component is nonfinite; the transform cannot be represented as finite doubles.</exception>
        public Vector3D ToLocal(Vector3D globalVector)
        {
            Validate(globalVector);
            var result = new Vector3D(Vector3D.DotProduct(globalVector, XAxis), Vector3D.DotProduct(globalVector, YAxis), Vector3D.DotProduct(globalVector, ZAxis));
            Validate(result);
            return result;
        }
        /// <summary>Expands finite local components into the corresponding global vector.</summary>
        /// <param name="localVector">Finite local components with no unit or sign conversion.</param>
        /// <returns>The same vector expressed in global XYZ.</returns>
        /// <exception cref="ArgumentException">An input or computed component is nonfinite; the transform cannot be represented as finite doubles.</exception>
        public Vector3D ToGlobal(Vector3D localVector)
        {
            Validate(localVector);
            var result = localVector.X * XAxis + localVector.Y * YAxis + localVector.Z * ZAxis;
            Validate(result);
            return result;
        }
        private static void Validate(Vector3D v)
        {
            if (double.IsNaN(v.X) || double.IsInfinity(v.X) || double.IsNaN(v.Y) || double.IsInfinity(v.Y) || double.IsNaN(v.Z) || double.IsInfinity(v.Z))
                throw new ArgumentException("Vector must be finite.");
        }
    }
}
