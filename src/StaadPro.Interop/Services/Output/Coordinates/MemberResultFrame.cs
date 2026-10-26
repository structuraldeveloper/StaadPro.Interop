using System;
using System.Windows.Media.Media3D;
namespace StaadPro.Interop.Services.Output.Coordinates
{
    // Shared pure basis convention; no identity, COM call, native-orientation claim or unit conversion.
    internal sealed class MemberResultFrame
    {
        internal Vector3D X
        {
            get; private set;
        }
        internal Vector3D Y
        {
            get; private set;
        }
        internal Vector3D Z
        {
            get; private set;
        }
        internal double Beta
        {
            get; private set;
        }
        internal static void Finite(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("A coordinate or derived component is nonfinite.");
        }
        internal static void Finite(Vector3D v)
        {
            Finite(v.X);
            Finite(v.Y);
            Finite(v.Z);
        }
        internal static void Finite(Point3D p)
        {
            Finite(p.X);
            Finite(p.Y);
            Finite(p.Z);
        }
        internal static MemberResultFrame Create(Point3D start, Point3D end, double beta)
        {
            Finite(start);
            Finite(end);
            Finite(beta);
            var x = end - start;
            Finite(x);
            if (double.IsInfinity(x.Length) || x.Length <= 1e-9)
                throw new ArgumentException("A finite noncoincident member direction is required.");
            x.Normalize();
            Vector3D y, z;
            if (Math.Abs(x.Y) >= 1 - 1e-9)
            {
                var gz = new Vector3D(0, 0, 1);
                z = gz - Vector3D.DotProduct(gz, x) * x;
                z.Normalize();
                y = Vector3D.CrossProduct(z, x);
                y.Normalize();
            }
            else
            {
                var gy = new Vector3D(0, 1, 0);
                y = gy - Vector3D.DotProduct(gy, x) * x;
                y.Normalize();
                z = Vector3D.CrossProduct(x, y);
                z.Normalize();
            }
            var rotation = Matrix3D.Identity;
            rotation.Rotate(new Quaternion(x, beta % 360));
            y = rotation.Transform(y);
            z = rotation.Transform(z);
            Finite(y);
            Finite(z);
            return new MemberResultFrame { X = x, Y = y, Z = z, Beta = beta };
        }
        internal Vector3D ToLocal(Vector3D vector)
        {
            Finite(vector);
            var result = new Vector3D(Vector3D.DotProduct(vector, X), Vector3D.DotProduct(vector, Y), Vector3D.DotProduct(vector, Z));
            Finite(result);
            return result;
        }
    }
}
