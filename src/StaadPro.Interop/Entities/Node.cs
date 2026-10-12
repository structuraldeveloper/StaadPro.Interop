using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;
using Newtonsoft.Json;
using StaadPro.Interop.Common;
using StaadPro.Interop.Extensions;
using StaadPro.Interop.Helpers;

namespace StaadPro.Interop.Entities
{
    /// <summary>
    /// Represents a 3D structural node with spatial coordinates and connectivity in STAAD.Pro.
    /// </summary>
    public class Node : ValidatablePropertyStore, IEntity, IEquatable<Node>
    {
        #region Constructors

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node..ctor()']/*" />
        public Node()
        {
            ConnectedBeams = new HashSet<Beam>();
            NodalLoadItems = new List<NodalLoad>();
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node..ctor(double,double,double)']/*" />
        public Node(double x, double y, double z) : this()
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node..ctor(int,double,double,double)']/*" />
        public Node(int number, double x, double y, double z) : this(x, y, z)
        {
            Id = number;
        }

        #endregion

        #region Public Properties

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.P:Id']/*" />
        [JsonProperty(Order = 1)]
        public int Id { get => Get<int>(); set => Set(value); }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.P:X']/*" />
        [JsonProperty(Order = 2)]
        public double X { get => Get<double>(); set => Set(value); }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.P:Y']/*" />
        [JsonProperty(Order = 3)]
        public double Y { get => Get<double>(); set => Set(value); }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.P:Z']/*" />
        [JsonProperty(Order = 4)]
        public double Z { get => Get<double>(); set => Set(value); }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.P:ConnectedBeams']/*" />
        [JsonIgnore]
        public HashSet<Beam> ConnectedBeams { get => Get<HashSet<Beam>>(); set => Set(value); }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.P:NodalLoadItems']/*" />
        [JsonIgnore]
        public List<NodalLoad> NodalLoadItems { get => Get<List<NodalLoad>>(); private set => Set(value); }

        #endregion

        #region Public Read-only Properties

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.P:ToPoint3D']/*" />
        [JsonIgnore]
        public Point3D ToPoint3D => new Point3D(X, Y, Z);

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.P:ToVector3D']/*" />
        [JsonIgnore]
        public Vector3D ToVector3D => new Vector3D(X, Y, Z);

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.P:RadiusFromOrigin']/*" />
        [JsonIgnore]
        public double RadiusFromOrigin => DistanceBetweenNodes(this, new Node(0.0, 0.0, 0.0));

        #endregion

        #region Public Functions

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.Move`0(double,double,double)']/*" />
        public Point3D Move(double dx, double dy, double dz)
        {
            X += dx;
            Y += dy;
            Z += dz;
            return ToPoint3D;
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.Move`0(Vector3D)']/*" />
        public Point3D Move(Vector3D translationVector) => Move(translationVector.X, translationVector.Y, translationVector.Z);

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.RadiusFromNode`0(Node)']/*" />
        public double RadiusFromNode(Node center) => DistanceBetweenNodes(this, center);

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.AngularDistanceOf`0(Node,double)']/*" />
        public double AngularDistanceOf(Node madeAtNode, double relativeToAngularLocation) =>
            AngularDistanceOf(this, madeAtNode, relativeToAngularLocation);

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.AngularDistanceBetween`0(Node,Node)']/*" />
        public double AngularDistanceBetween(Node targetNode, Node madeAtNode) =>
            AngularDistanceBetween(this, targetNode, madeAtNode);

        #endregion

        #region Public Static Functions

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.GetVector3D`0(Node,Node)']/*" />
        public static Vector3D GetVector3D(Node fromNode, Node toNode) =>
            new Vector3D(toNode.X - fromNode.X, toNode.Y - fromNode.Y, toNode.Z - fromNode.Z);

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.CreateNewNodeByDistance`0(Node,Node,double)']/*" />
        public static Node CreateNewNodeByDistance(Node sNode, Node eNode, double distance)
        {
            Vector3D unitVector = GetVector3D(sNode, eNode);
            unitVector.Normalize();

            return new Node(
                sNode.X + (unitVector.X * distance),
                sNode.Y + (unitVector.Y * distance),
                sNode.Z + (unitVector.Z * distance));
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.CreateNewNodeByFraction`0(Node,Node,double)']/*" />
        public static Node CreateNewNodeByFraction(Node sNode, Node eNode, double fraction)
        {
            double distance = DistanceBetweenNodes(sNode, eNode) * fraction;
            return CreateNewNodeByDistance(sNode, eNode, distance);
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.DistanceBetweenNodes`0(Node,Node)']/*" />
        public static double DistanceBetweenNodes(Node a, Node b)
        {
            if (a == null || b == null) return 0.0;
            return Math.Sqrt((b.X - a.X).Square() + (b.Y - a.Y).Square() + (b.Z - a.Z).Square());
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.LeastDistanceBetweenNodes`0(IList&lt;Node&gt;)']/*" />
        public static double LeastDistanceBetweenNodes(IList<Node> nodes)
        {
            if (nodes == null || nodes.Count <= 1) return 0;
            double least = DistanceBetweenNodes(nodes[1], nodes[0]);
            for (int i = 1; i < nodes.Count - 1; i++)
            {
                least = Math.Min(least, DistanceBetweenNodes(nodes[i + 1], nodes[i]));
            }
            return least;
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.MaxDistanceBetweenNodes`0(IList&lt;Node&gt;)']/*" />
        public static double MaxDistanceBetweenNodes(IList<Node> nodes)
        {
            if (nodes == null || nodes.Count <= 1) return 0;
            double max = DistanceBetweenNodes(nodes[1], nodes[0]);
            for (int i = 1; i < nodes.Count - 1; i++)
            {
                max = Math.Max(max, DistanceBetweenNodes(nodes[i + 1], nodes[i]));
            }
            return max;
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.PlanAngleIn360DegreesOf`0(Node,Node)']/*" />
        public static double PlanAngleIn360DegreesOf(Node node, Node madeAtNode)
        {
            double angleInDegrees = Math.Atan2(node.Z - madeAtNode.Z, node.X - madeAtNode.X).Degrees() % 360.0;
            if (angleInDegrees < 0) angleInDegrees += 360.0;
            return angleInDegrees;
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.AngularDistanceBetween`0(Node,Node,Node)']/*" />
        public static double AngularDistanceBetween(Node a, Node b, Node madeAtNode) =>
            AngularDistanceOf(b, madeAtNode, PlanAngleIn360DegreesOf(a, madeAtNode));

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.AngularDistanceOf`0(Node,Node,double)']/*" />
        public static double AngularDistanceOf(Node node, Node madeAtNode, double relativeToAngularLocation)
        {
            if (madeAtNode == null) madeAtNode = new Node(0.0, 0.0, 0.0);
            relativeToAngularLocation = AngleHelpers.PositiveComplimentOf(relativeToAngularLocation);

            double targetNodeAngle = PlanAngleIn360DegreesOf(node, madeAtNode);
            double angularDistance = Math.Abs(relativeToAngularLocation - targetNodeAngle);

            if (angularDistance > Constants.WholeCircleAngleInDegrees / 2)
            {
                angularDistance = Constants.WholeCircleAngleInDegrees - angularDistance;
            }

            return angularDistance;
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.GenerateIntermediateNodes`0(Node,Node,double,int)']/*" />
        public static List<Node> GenerateIntermediateNodes(Node sNode, Node eNode, double spacing, int currentNodeId = 0)
        {
            List<Node> nodes = new List<Node>();
            double startNodeToEndNodeDistance = DistanceBetweenNodes(sNode, eNode);

            if (spacing != 0 && spacing < startNodeToEndNodeDistance)
            {
                double divisions = (startNodeToEndNodeDistance / spacing).Ceiling(1);
                double adjustedSpacing = startNodeToEndNodeDistance / divisions;

                for (int i = 1; i < divisions; i++)
                {
                    currentNodeId += 1;
                    Node node = CreateNewNodeByDistance(sNode, eNode, i * adjustedSpacing);
                    node.Id = currentNodeId;
                    nodes.Add(node);
                }
            }

            return nodes;
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.GenerateNodesOnCircularPath`0(Node,double,int,double,int)']/*" />
        public static HashSet<Node> GenerateNodesOnCircularPath(Node center, double radius, int divisions, double dy = 0.0, int lastUsedId = 0)
        {
            return GenerateNodesOnCircularPath(center, radius, 0, Constants.WholeCircleAngleInDegrees, divisions, dy, lastUsedId);
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.GenerateNodesOnCircularPath`0(Node,double,double,double,int,double,int)']/*" />
        public static HashSet<Node> GenerateNodesOnCircularPath(Node center, double radius, double sAngle, double eAngle, int divisions, double dy = 0.0, int lastUsedId = 0)
        {
            int id = lastUsedId + 1;
            double incAngle = (eAngle - sAngle) / divisions;
            HashSet<Node> nodes = new HashSet<Node>();

            for (int i = 0; i < divisions; i++)
            {
                double angle = sAngle + (i * incAngle);
                double dx = radius * Math.Cos(angle.Radians());
                double dz = radius * Math.Sin(angle.Radians());

                Node node = new Node(id, center.X + dx, center.Y + dy, center.Z + dz);
                nodes.Add(node);
                id += 1;
            }

            return nodes;
        }

        #endregion

        #region Equality

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.GetHashCode`0()']/*" />
        public override int GetHashCode() => Id.GetHashCode();

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.Equals`0(Node)']/*" />
        public bool Equals(Node node)
        {
            if (node == null) return false;
            return Id == node.Id && DistanceBetweenNodes(this, node) <= Constants.Tolerance;
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='Node.Equals`0(object)']/*" />
        public override bool Equals(object obj)
        {
            if (!(obj is Node node)) return false;
            return Id == node.Id && DistanceBetweenNodes(this, node) <= Constants.Tolerance;
        }

        public static bool operator ==(Node left, Node right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return left.Id == right.Id && DistanceBetweenNodes(left, right) <= Constants.Tolerance;
        }

        public static bool operator !=(Node left, Node right) => !(left == right);

        #endregion
    }
}
