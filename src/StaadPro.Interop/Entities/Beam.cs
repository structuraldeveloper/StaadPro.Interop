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

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam..ctor()']/*" />
        public Beam()
        {
            MemberLoadItems = new List<MemberLoad>();
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam..ctor(Node,Node)']/*" />
        public Beam(Node startNode, Node endNode) : this()
        {
            StartNode = startNode;
            EndNode = endNode;
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam..ctor(int,Node,Node)']/*" />
        public Beam(int number, Node startNode, Node endNode) : this(startNode, endNode)
        {
            Id = number;
        }

        #endregion

        #region Public Properties

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam.P:Id']/*" />
        [JsonProperty(Order = 1)]
        public int Id { get => Get<int>(); set => Set(value); }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam.P:StartNode']/*" />
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

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam.P:EndNode']/*" />
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

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam.P:BetaAngle']/*" />
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

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam.P:MemberLoadItems']/*" />
        [JsonIgnore]
        public List<MemberLoad> MemberLoadItems { get => Get<List<MemberLoad>>(); private set => Set(value); }

        #endregion

        #region Public Read-only Properties

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam.P:ToVector']/*" />
        [JsonIgnore]
        public Vector3D ToVector => StartNode != null && EndNode != null ? Node.GetVector3D(StartNode, EndNode) : default;

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam.P:Length']/*" />
        [JsonIgnore]
        public double Length => StartNode != null && EndNode != null
            ? Math.Sqrt((EndNode.X - StartNode.X).Square() + (EndNode.Y - StartNode.Y).Square() + (EndNode.Z - StartNode.Z).Square())
            : 0.0;

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam.P:DeltaX']/*" />
        [JsonIgnore]
        public double DeltaX => StartNode != null && EndNode != null ? Math.Abs(EndNode.X - StartNode.X) : 0.0;
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam.P:DeltaY']/*" />
        [JsonIgnore]
        public double DeltaY => StartNode != null && EndNode != null ? Math.Abs(EndNode.Y - StartNode.Y) : 0.0;
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam.P:DeltaZ']/*" />
        [JsonIgnore]
        public double DeltaZ => StartNode != null && EndNode != null ? Math.Abs(EndNode.Z - StartNode.Z) : 0.0;

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam.P:IsParallelToX']/*" />
        [JsonIgnore]
        public bool IsParallelToX => DeltaY + DeltaZ <= 0.01;
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam.P:IsParallelToY']/*" />
        [JsonIgnore]
        public bool IsParallelToY => DeltaX + DeltaZ <= 0.01;
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam.P:IsParallelToZ']/*" />
        [JsonIgnore]
        public bool IsParallelToZ => DeltaX + DeltaY <= 0.01;

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam.P:LocalXAxis']/*" />
        [JsonIgnore]
        public Vector3D LocalXAxis { get => Get<Vector3D>(); private set => Set(value); }
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam.P:LocalYAxis']/*" />
        [JsonIgnore]
        public Vector3D LocalYAxis { get => Get<Vector3D>(); private set => Set(value); }
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam.P:LocalZAxis']/*" />
        [JsonIgnore]
        public Vector3D LocalZAxis { get => Get<Vector3D>(); private set => Set(value); }

        #endregion

        #region Public Functions

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam.DetermineBeamRelationship`0(Beam)']/*" />
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

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam.GetHashCode`0()']/*" />
        public override int GetHashCode() => Id.GetHashCode();

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam.Equals`0(Beam)']/*" />
        public bool Equals(Beam other)
        {
            if (other is null) return false;
            return Id == other.Id && StartNode == other.StartNode && EndNode == other.EndNode;
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Beam.Equals`0(object)']/*" />
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
