using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;
using Newtonsoft.Json;
using StaadPro.Interop.Common;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Extensions;

namespace StaadPro.Interop.Entities
{
    /// <summary>
    /// Represents an analytical beam element connected between two nodes in STAAD.Pro.
    /// </summary>
    public class Beam : ValidatablePropertyStore, IEntity
    {
        #region Constructors

        public Beam()
        {
            MemberLoadItems = new List<MemberLoad>();
        }

        public Beam(Node startNode, Node endNode) : this()
        {
            StartNode = startNode;
            EndNode = endNode;
        }

        public Beam(int number, Node startNode, Node endNode) : this(startNode, endNode)
        {
            Id = number;
        }

        #endregion

        #region Public Properties

        [JsonProperty(Order = 1)]
        public int Id { get => Get<int>(); set => Set(value); }

        [JsonProperty(Order = 2)]
        public Node StartNode
        {
            get => Get<Node>();
            private set
            {
                Set(value);
                StartNode?.ConnectedBeams.Add(this);
                GenerateLocalAxes();
            }
        }

        [JsonProperty(Order = 3)]
        public Node EndNode
        {
            get => Get<Node>();
            private set
            {
                Set(value);
                EndNode?.ConnectedBeams.Add(this);
                GenerateLocalAxes();
            }
        }

        [JsonProperty(Order = 4)]
        public double BetaAngle
        {
            get => Get<double>();
            set
            {
                Set(value);
                GenerateLocalAxes();
            }
        }

        [JsonIgnore]
        public List<MemberLoad> MemberLoadItems { get => Get<List<MemberLoad>>(); private set => Set(value); }

        #endregion

        #region Public Read-only Properties

        [JsonIgnore]
        public Vector3D ToVector => StartNode != null && EndNode != null ? Node.GetVector3D(StartNode, EndNode) : default;

        [JsonIgnore]
        public double Length => StartNode != null && EndNode != null
            ? Math.Sqrt((EndNode.X - StartNode.X).Square() + (EndNode.Y - StartNode.Y).Square() + (EndNode.Z - StartNode.Z).Square())
            : 0.0;

        [JsonIgnore]
        public double DeltaX => StartNode != null && EndNode != null ? Math.Abs(EndNode.X - StartNode.X) : 0.0;
        [JsonIgnore]
        public double DeltaY => StartNode != null && EndNode != null ? Math.Abs(EndNode.Y - StartNode.Y) : 0.0;
        [JsonIgnore]
        public double DeltaZ => StartNode != null && EndNode != null ? Math.Abs(EndNode.Z - StartNode.Z) : 0.0;

        [JsonIgnore]
        public bool IsParallelToX => DeltaY + DeltaZ <= 0.01;
        [JsonIgnore]
        public bool IsParallelToY => DeltaX + DeltaZ <= 0.01;
        [JsonIgnore]
        public bool IsParallelToZ => DeltaX + DeltaY <= 0.01;

        [JsonIgnore]
        public Vector3D LocalXAxis { get => Get<Vector3D>(); private set => Set(value); }
        [JsonIgnore]
        public Vector3D LocalYAxis { get => Get<Vector3D>(); private set => Set(value); }
        [JsonIgnore]
        public Vector3D LocalZAxis { get => Get<Vector3D>(); private set => Set(value); }

        #endregion

        #region Public Functions

        public BeamRelation DetermineBeamRelationship(Beam targetBeam)
        {
            if (targetBeam == null || StartNode == null || EndNode == null || targetBeam.StartNode == null || targetBeam.EndNode == null)
                return BeamRelation.Undefined;

            Vector3D v1 = ToVector;
            Vector3D v2 = targetBeam.ToVector;
            v1.Normalize();
            v2.Normalize();

            double dot = Vector3D.DotProduct(v1, v2);

            if (Math.Abs(dot - 1.0) < 0.001) return BeamRelation.Parallel;
            if (Math.Abs(dot + 1.0) < 0.001) return BeamRelation.ParallelOppositeDirection;
            if (Math.Abs(dot) < 0.001) return BeamRelation.Orthogonal;

            return BeamRelation.Other;
        }

        #endregion

        #region Private Methods

        private void GenerateLocalAxes()
        {
            if (StartNode == null || EndNode == null) return;

            Vector3D x = Node.GetVector3D(StartNode, EndNode);
            if (x.Length <= 1e-6) return;
            x.Normalize();

            Vector3D refY = Math.Abs(x.Y - 1.0) < 0.001 || Math.Abs(x.Y + 1.0) < 0.001
                ? new Vector3D(0, 0, 1)
                : new Vector3D(0, 1, 0);

            Vector3D z = Vector3D.CrossProduct(x, refY);
            z.Normalize();
            Vector3D y = Vector3D.CrossProduct(z, x);
            y.Normalize();

            LocalXAxis = x;
            LocalYAxis = y;
            LocalZAxis = z;
        }

        #endregion

        #region Equality

        public override int GetHashCode() => Id.GetHashCode();

        public bool Equals(Beam other)
        {
            if (other is null) return false;
            return Id == other.Id && StartNode == other.StartNode && EndNode == other.EndNode;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Beam other)) return false;
            return Equals(other);
        }

        public static bool operator ==(Beam left, Beam right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }

        public static bool operator !=(Beam left, Beam right) => !(left == right);

        #endregion
    }
}
