using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
using Newtonsoft.Json;
using StaadPro.Interop.Common;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Helpers;

namespace StaadPro.Interop.Entities
{
    /// <summary>
    /// Represents a 3-node or 4-node analytical plate/shell element in STAAD.Pro.
    /// </summary>
    public class Plate : ValidatablePropertyStore, IEntity, IEquatable<Plate>
    {
        #region Constructors

        public Plate()
        {
        }

        public Plate(Node a, Node b, Node c, Node d = null) : this()
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }

        public Plate(int id, Node a, Node b, Node c, Node d = null) : this(a, b, c, d)
        {
            Id = id;
        }

        #endregion

        #region Public Properties

        [JsonProperty(Order = 1)]
        public int Id { get => Get<int>(); set => Set(value); }

        [JsonProperty(Order = 2)]
        public Node A { get => Get<Node>(); set => Set(value); }

        [JsonProperty(Order = 3)]
        public Node B { get => Get<Node>(); set => Set(value); }

        [JsonProperty(Order = 4)]
        public Node C { get => Get<Node>(); set => Set(value); }

        [JsonProperty(Order = 5)]
        public Node D { get => Get<Node>(); set => Set(value); }

        #endregion

        #region Public Read-only Properties

        [JsonIgnore]
        public bool IsTriangle => D == null;

        [JsonIgnore]
        public double Area
        {
            get
            {
                if (A == null || B == null || C == null) return 0.0;
                Vector3D ab = Node.GetVector3D(A, B);
                Vector3D ac = Node.GetVector3D(A, C);
                double area1 = Vector3D.CrossProduct(ab, ac).Length * 0.5;

                if (D == null) return area1;

                Vector3D ad = Node.GetVector3D(A, D);
                double area2 = Vector3D.CrossProduct(ac, ad).Length * 0.5;
                return area1 + area2;
            }
        }

        [JsonIgnore]
        public PlateOrientation Orientation
        {
            get
            {
                if (!TryGetLocalCoordinateSystemBasis(out _, out _, out Vector3D localZ))
                {
                    return PlateOrientation.Inclined;
                }

                if (Math.Abs(Math.Abs(localZ.Y) - 1.0) <= Constants.Tolerance)
                {
                    return PlateOrientation.Horizontal;
                }

                if (Math.Abs(localZ.Y) <= Constants.Tolerance)
                {
                    return PlateOrientation.Vertical;
                }

                return PlateOrientation.Inclined;
            }
        }

        #endregion

        #region Public Functions

        public bool TryGetLocalCoordinateSystemBasis(out Vector3D localX, out Vector3D localY, out Vector3D localZ)
        {
            localX = default;
            localY = default;
            localZ = default;

            if (A == null || B == null || C == null) return false;

            Vector3D vAB = Node.GetVector3D(A, B);
            Vector3D vAC = Node.GetVector3D(A, C);

            if (vAB.Length <= Constants.Tolerance || vAC.Length <= Constants.Tolerance) return false;

            Vector3D normal = Vector3D.CrossProduct(vAB, vAC);
            if (normal.Length <= Constants.Tolerance) return false;

            normal.Normalize();
            vAB.Normalize();

            Vector3D inPlaneY = Vector3D.CrossProduct(normal, vAB);
            inPlaneY.Normalize();

            localX = vAB;
            localY = inPlaneY;
            localZ = normal;
            return true;
        }

        #endregion

        #region Equality

        public override int GetHashCode() => Id.GetHashCode();

        public bool Equals(Plate other)
        {
            if (other is null) return false;
            return Id == other.Id && A == other.A && B == other.B && C == other.C && D == other.D;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Plate other)) return false;
            return Equals(other);
        }

        public static bool operator ==(Plate left, Plate right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }

        public static bool operator !=(Plate left, Plate right) => !(left == right);

        #endregion
    }
}
