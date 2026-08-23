using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;
using StaadPro.Interop.Common;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Extensions;

namespace StaadPro.Interop.Entities
{
    /// <summary>
    /// Represents a physical structural member formed by one or more analytical beam segments.
    /// </summary>
    public sealed class Member : ValidatablePropertyStore, IEntity
    {
        #region Constructors

        public Member()
        {
            Nodes = new HashSet<Node>();
            Beams = new HashSet<Beam>();
        }

        public Member(Node sNode, Node eNode) : this()
        {
            StartNode = sNode;
            EndNode = eNode;
            Nodes.Add(sNode);
            Nodes.Add(eNode);
        }

        public Member(int id, Node sNode, Node eNode) : this(sNode, eNode)
        {
            Id = id;
        }

        #endregion

        #region Public Properties

        public int Id { get => Get<int>(); private set => Set(value); }

        public Node StartNode { get => Get<Node>(); private set => Set(value); }

        public Node EndNode { get => Get<Node>(); private set => Set(value); }

        public HashSet<Node> Nodes { get => Get<HashSet<Node>>(); set => Set(value); }

        public HashSet<Beam> Beams { get => Get<HashSet<Beam>>(); set => Set(value); }

        public MemberType Type { get => Get<MemberType>(); set => Set(value); }

        #endregion

        #region Public Read-only Properties

        public Vector3D ToVector => StartNode != null && EndNode != null ? Node.GetVector3D(StartNode, EndNode) : default;

        public double Length => Beams != null ? Beams.Sum(b => b.Length) : 0.0;

        public double LengthAB => StartNode != null && EndNode != null
            ? Math.Sqrt(DeltaX.Square() + DeltaY.Square() + DeltaZ.Square())
            : 0.0;

        public double DeltaX => StartNode != null && EndNode != null ? Math.Sqrt((EndNode.X - StartNode.X).Square()) : 0.0;
        public double DeltaY => StartNode != null && EndNode != null ? Math.Sqrt((EndNode.Y - StartNode.Y).Square()) : 0.0;
        public double DeltaZ => StartNode != null && EndNode != null ? Math.Sqrt((EndNode.Z - StartNode.Z).Square()) : 0.0;

        public bool IsParallelToX => DeltaY + DeltaZ <= 0.01;
        public bool IsParallelToY => DeltaX + DeltaZ <= 0.01;
        public bool IsParallelToZ => DeltaX + DeltaY <= 0.01;

        #endregion

        #region Public Functions

        public MemberRelation DetermineMemberRelation(Member targetMember)
        {
            if (Beams == null || !Beams.Any() || targetMember?.Beams == null || !targetMember.Beams.Any())
                return MemberRelation.Other;

            switch (Beams.First().DetermineBeamRelationship(targetMember.Beams.First()))
            {
                case BeamRelation.Parallel:
                    return MemberRelation.Parallel;
                case BeamRelation.Orthogonal:
                    return MemberRelation.Orthogonal;
                default:
                    return MemberRelation.Other;
            }
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is Member m && Id == m.Id;
        }

        public bool Equals(Member member)
        {
            return !(member is null) && Id == member.Id;
        }

        public static bool operator ==(Member a, Member b)
        {
            return ReferenceEquals(a, b) || (!(a is null) && !(b is null) && a.Id == b.Id);
        }

        public static bool operator !=(Member a, Member b) => !(a == b);

        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}
