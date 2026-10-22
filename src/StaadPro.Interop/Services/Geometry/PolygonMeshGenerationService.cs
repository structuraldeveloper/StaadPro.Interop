using StaadPro.Interop.Entities;
using StaadPro.Interop.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace StaadPro.Interop.Services
{
    /// <summary>Builds a bounded managed triangulation of a simple planar polygon without calling OpenSTAAD.</summary>
    /// <remarks>Preserves original boundary objects; refinement may add nodes along boundary segments. Quality is reported, not guaranteed.</remarks>
    public static class PolygonMeshGenerationService
    {
        private const int MaximumRefinementIterations = 256;
        private const int MaximumBoundaryNodes = 512;


        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='GeometryCompletion.GeneratePolygonMesh']/*" />
        public static PolygonMeshResult GenerateMesh(
            IReadOnlyList<Node> boundaryNodes, int startingNodeId, int startingPlateId,
            double minimumAngleDegrees = 20.0, double maximumEdgeLength = 0.0)
        {
            if (boundaryNodes == null) throw new ArgumentNullException(nameof(boundaryNodes));
            var boundary = boundaryNodes.ToList();
            if (boundary.Count < 3 || boundary.Count > MaximumBoundaryNodes)
                throw new ArgumentException("Supply between 3 and 512 boundary nodes.", nameof(boundaryNodes));
            if (startingNodeId <= 0) throw new ArgumentOutOfRangeException(nameof(startingNodeId));
            if (startingPlateId <= 0) throw new ArgumentOutOfRangeException(nameof(startingPlateId));
            if (!Finite(minimumAngleDegrees) || minimumAngleDegrees <= 0 || minimumAngleDegrees >= 60)
                throw new ArgumentOutOfRangeException(nameof(minimumAngleDegrees));
            if (!Finite(maximumEdgeLength) || maximumEdgeLength < 0)
                throw new ArgumentOutOfRangeException(nameof(maximumEdgeLength));
            if (boundary.Any(node => node == null || node.Id <= 0 || !Finite(node.X) || !Finite(node.Y) || !Finite(node.Z)))
                throw new ArgumentException("Boundary nodes require positive IDs and finite coordinates.", nameof(boundaryNodes));
            if (boundary.Select(node => node.Id).Distinct().Count() != boundary.Count)
                throw new ArgumentException("Boundary IDs must be unique; omit a repeated closing node.", nameof(boundaryNodes));

            PlanarFrame frame = PlanarFrame.FromBoundary(boundary);
            var originalLoop = boundary.Select(frame.ToPlanar).ToList();
            ValidateSimplePolygon(originalLoop);
            var loop = new List<PlanarPoint>(originalLoop);
            if (SignedArea(loop) < 0) loop.Reverse();
            var triangles = EarClipTriangulate(loop);
            var constraints = BuildBoundarySegments(loop);
            DelaunayizeByFlips(triangles, constraints);
            var outcome = Refine(triangles, constraints, loop, minimumAngleDegrees,
                maximumEdgeLength == 0 ? 0 : maximumEdgeLength / frame.Scale);
            return BuildResult(boundary, frame, originalLoop, triangles, outcome, startingNodeId, startingPlateId);
        }

        private static bool Finite(double value) => !double.IsNaN(value) && !double.IsInfinity(value);

        private static void ValidateSimplePolygon(List<PlanarPoint> loop)
        {
            for (int i = 0; i < loop.Count; i++)
            {
                for (int j = i + 1; j < loop.Count; j++)
                    if (loop[i].DistanceTo(loop[j]) <= 1e-10)
                        throw new ArgumentException("Boundary coordinates must be distinct.", "boundaryNodes");
                var previous = loop[(i + loop.Count - 1) % loop.Count];
                var current = loop[i];
                var next = loop[(i + 1) % loop.Count];
                if (Math.Abs(Cross(current - previous, next - current)) <= 1e-12 &&
                    LiesOnSegment(next, previous, current))
                    throw new ArgumentException("Adjacent edges overlap.", "boundaryNodes");
                for (int j = i + 1; j < loop.Count; j++)
                {
                    if (j == i + 1 || (i == 0 && j == loop.Count - 1)) continue;
                    var a = loop[i]; var b = loop[(i + 1) % loop.Count];
                    var c = loop[j]; var d = loop[(j + 1) % loop.Count];
                    double abC = Cross(b - a, c - a), abD = Cross(b - a, d - a);
                    double cdA = Cross(d - c, a - c), cdB = Cross(d - c, b - c);
                    if ((abC * abD < 0 && cdA * cdB < 0) ||
                        LiesOnSegment(a, c, d) || LiesOnSegment(b, c, d) ||
                        LiesOnSegment(c, a, b) || LiesOnSegment(d, a, b))
                        throw new ArgumentException("Boundary edges must not cross or touch nonadjacent edges.", "boundaryNodes");
                }
            }
            if (Math.Abs(SignedArea(loop)) <= 1e-12)
                throw new ArgumentException("The polygon has negligible area at its coordinate scale.", "boundaryNodes");
        }

        #region Result Assembly


        private static PolygonMeshResult BuildResult(
            List<Node> boundary, PlanarFrame frame, List<PlanarPoint> originalLoop,
            List<MeshTriangle> triangles, RefinementOutcome outcome, int startingNodeId, int startingPlateId)
        {
            var result = new PolygonMeshResult { BoundaryNodes = boundary, ReachedTargetQuality = outcome.ReachedTargetQuality };
            var nodes = new Dictionary<PlanarPoint, Node>();
            for (int i = 0; i < boundary.Count; i++) nodes.Add(originalLoop[i], boundary[i]);
            var boundaryIds = new HashSet<int>(boundary.Select(node => node.Id));
            for (int i = 0; i < outcome.NewInteriorPoints.Count; i++)
            {
                int id = checked(startingNodeId + i);
                if (boundaryIds.Contains(id)) throw new ArgumentException("Generated node IDs overlap boundary IDs.", nameof(startingNodeId));
                var point = outcome.NewInteriorPoints[i];
                var node = frame.ToNode(point, id);
                nodes.Add(point, node);
                result.InteriorNodes.Add(node);
            }
            for (int i = 0; i < triangles.Count; i++)
            {
                MeshTriangle triangle = triangles[i];
                result.Plates.Add(new Plate(checked(startingPlateId + i), nodes[triangle.A], nodes[triangle.B], nodes[triangle.C]));
            }
            result.AchievedMinimumAngleDegrees = triangles.Min(SmallestAngleDegrees);
            return result;
        }

        #endregion

        #region Ear-Clipping Triangulation

        private static List<MeshTriangle> EarClipTriangulate(List<PlanarPoint> boundaryLoop)
        {
            List<PlanarPoint> remaining = new List<PlanarPoint>(boundaryLoop);
            List<MeshTriangle> triangles = new List<MeshTriangle>();

            int guardIterations = remaining.Count * remaining.Count + 8;
            while (remaining.Count > 3 && guardIterations-- > 0)
            {
                bool clipped = false;
                for (int index = 0; index < remaining.Count; index++)
                {
                    PlanarPoint previous = remaining[(index - 1 + remaining.Count) % remaining.Count];
                    PlanarPoint current = remaining[index];
                    PlanarPoint next = remaining[(index + 1) % remaining.Count];

                    if (IsConvexVertex(previous, current, next) && IsEar(remaining, index))
                    {
                        triangles.Add(new MeshTriangle(previous, current, next));
                        remaining.RemoveAt(index);
                        clipped = true;
                        break;
                    }
                }

                if (!clipped)
                {
                    throw new ArgumentException("The polygon cannot be triangulated reliably at its coordinate scale.", "boundaryNodes");
                }
            }

            if (remaining.Count == 3)
            {
                triangles.Add(new MeshTriangle(remaining[0], remaining[1], remaining[2]));
            }

            return triangles;
        }

        private static bool IsConvexVertex(PlanarPoint previous, PlanarPoint current, PlanarPoint next)
        {
            return Cross(current - previous, next - current) > 0.0;
        }

        private static bool IsEar(List<PlanarPoint> polygon, int vertexIndex)
        {
            PlanarPoint previous = polygon[(vertexIndex - 1 + polygon.Count) % polygon.Count];
            PlanarPoint current = polygon[vertexIndex];
            PlanarPoint next = polygon[(vertexIndex + 1) % polygon.Count];

            for (int index = 0; index < polygon.Count; index++)
            {
                if (index == vertexIndex ||
                    index == (vertexIndex - 1 + polygon.Count) % polygon.Count ||
                    index == (vertexIndex + 1) % polygon.Count)
                {
                    continue;
                }

                if (PointInOrOnTriangle(polygon[index], previous, current, next))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Delaunay-ization By Edge Flips


        private static void DelaunayizeByFlips(List<MeshTriangle> triangles, List<Segment> constraints)
        {
            var constrained = new HashSet<Segment>(constraints);
            // Rebuild local adjacency after each sweep; do not scan every pair of triangles.
            // Bounded sweeps are a quality improvement, not a certified Delaunay predicate.
            for (int sweep = 0; sweep < 16; sweep++)
            {
                var adjacency = new Dictionary<Segment, List<int>>();
                for (int i = 0; i < triangles.Count; i++)
                {
                    var t = triangles[i];
                    foreach (var edge in new[] { new Segment(t.A, t.B), new Segment(t.B, t.C), new Segment(t.C, t.A) })
                    {
                        if (!adjacency.TryGetValue(edge, out List<int> incident))
                            adjacency.Add(edge, incident = new List<int>());
                        incident.Add(i);
                    }
                }
                bool changed = false;
                var touched = new HashSet<int>();
                foreach (var edge in adjacency)
                {
                    if (edge.Value.Count != 2 || constrained.Contains(edge.Key)) continue;
                    int first = edge.Value[0], second = edge.Value[1];
                    if (touched.Contains(first) || touched.Contains(second)) continue;
                    if (TryFlipSharedEdge(triangles, first, second, constrained))
                    {
                        touched.Add(first); touched.Add(second);
                        changed = true;
                    }
                }
                if (!changed) return;
            }
        }

        private static bool TryFlipSharedEdge(List<MeshTriangle> triangles, int firstIndex, int secondIndex, HashSet<Segment> constrainedEdges)
        {
            MeshTriangle first = triangles[firstIndex];
            MeshTriangle second = triangles[secondIndex];

            if (!TryGetSharedEdgeAndOpposites(first, second, out PlanarPoint shared1, out PlanarPoint shared2, out PlanarPoint opposite1, out PlanarPoint opposite2))
            {
                return false;
            }

            if (constrainedEdges.Contains(new Segment(shared1, shared2)))
            {
                return false;
            }

            if (!IsConvexQuadrilateral(shared1, opposite1, shared2, opposite2))
            {
                return false;
            }

            if (!IsInCircumcircle(first, opposite2))
            {
                return false;
            }

            triangles[firstIndex] = new MeshTriangle(shared1, opposite1, opposite2);
            triangles[secondIndex] = new MeshTriangle(opposite1, shared2, opposite2);
            return true;
        }

        private static bool TryGetSharedEdgeAndOpposites(
            MeshTriangle first,
            MeshTriangle second,
            out PlanarPoint shared1,
            out PlanarPoint shared2,
            out PlanarPoint opposite1,
            out PlanarPoint opposite2)
        {
            shared1 = default;
            shared2 = default;
            opposite1 = default;
            opposite2 = default;

            List<PlanarPoint> firstPoints = first.Points;
            List<PlanarPoint> secondPoints = second.Points;
            List<PlanarPoint> shared = firstPoints.Where(point => secondPoints.Contains(point)).ToList();
            if (shared.Count != 2)
            {
                return false;
            }

            PlanarPoint sharedFirst = shared[0];
            PlanarPoint sharedSecond = shared[1];
            shared1 = sharedFirst;
            shared2 = sharedSecond;
            opposite1 = firstPoints.Single(point => point != sharedFirst && point != sharedSecond);
            opposite2 = secondPoints.Single(point => point != sharedFirst && point != sharedSecond);
            return true;
        }

        private static bool IsConvexQuadrilateral(PlanarPoint shared1, PlanarPoint opposite1, PlanarPoint shared2, PlanarPoint opposite2)
        {
            double firstSide = Cross(shared2 - shared1, opposite1 - shared1);
            double secondSide = Cross(shared2 - shared1, opposite2 - shared1);
            double thirdSide = Cross(opposite2 - opposite1, shared1 - opposite1);
            double fourthSide = Cross(opposite2 - opposite1, shared2 - opposite1);
            return firstSide * secondSide < -1e-24 && thirdSide * fourthSide < -1e-24;
        }

        #endregion

        #region Ruppert Refinement

        private sealed class RefinementOutcome
        {
            public List<PlanarPoint> NewInteriorPoints { get; } = new List<PlanarPoint>();
            public bool ReachedTargetQuality { get; set; }
        }


        private static RefinementOutcome Refine(
            List<MeshTriangle> triangles, List<Segment> constraints, List<PlanarPoint> boundaryLoop,
            double minimumAngleDegrees, double maximumEdgeLength)
        {
            var outcome = new RefinementOutcome();
            var points = new List<PlanarPoint>(boundaryLoop);
            for (int iteration = 0; iteration < MaximumRefinementIterations; iteration++)
            {
                var poor = triangles.FirstOrDefault(triangle => !MeetsQuality(triangle, minimumAngleDegrees, maximumEdgeLength));
                if (poor == null) break;
                // Interior insertion cannot shorten a constrained edge. Split its midpoint when
                // encroached; original boundary objects remain unchanged and generated split nodes
                // are explicitly returned in the source-compatible InteriorNodes collection.
                var point = Circumcenter(poor);
                if (!Finite(point.X) || !Finite(point.Z) ||
                    !triangles.Any(t => PointInOrOnTriangle(point, t.A, t.B, t.C)))
                    point = new PlanarPoint((poor.A.X + poor.B.X + poor.C.X) / 3, (poor.A.Z + poor.B.Z + poor.C.Z) / 3);
                var segment = constraints.FirstOrDefault(edge => edge.Encroaches(point));
                if (segment != null) point = segment.Midpoint;
                if (points.Any(existing => existing.DistanceTo(point) <= 1e-10)) break;
                RetriangulateAfterInsertion(triangles, point);
                if (segment != null)
                {
                    constraints.Remove(segment);
                    constraints.Add(new Segment(segment.Start, point));
                    constraints.Add(new Segment(point, segment.End));
                }
                points.Add(point);
                outcome.NewInteriorPoints.Add(point);
                DelaunayizeByFlips(triangles, constraints);
            }
            outcome.ReachedTargetQuality = triangles.All(t => MeetsQuality(t, minimumAngleDegrees, maximumEdgeLength));
            return outcome;
        }

        private static bool MeetsQuality(MeshTriangle triangle, double angle, double maximumEdgeLength) =>
            SmallestAngleDegrees(triangle) + 1e-8 >= angle &&
            (maximumEdgeLength == 0 || triangle.Sides.All(side => side <= maximumEdgeLength * (1 + 1e-10)));

        private static void RetriangulateAfterInsertion(List<MeshTriangle> triangles, PlanarPoint point)
        {
            // Edge insertion must split every adjacent triangle, avoiding a degenerate triangle
            // and a hanging node when the insertion lies on a shared internal edge.
            var touched = triangles.Where(t => LiesOnSegment(point, t.A, t.B) ||
                LiesOnSegment(point, t.B, t.C) || LiesOnSegment(point, t.C, t.A)).ToList();
            if (touched.Count > 0)
            {
                foreach (var triangle in touched)
                {
                    PlanarPoint a, b, opposite;
                    if (LiesOnSegment(point, triangle.A, triangle.B)) { a = triangle.A; b = triangle.B; opposite = triangle.C; }
                    else if (LiesOnSegment(point, triangle.B, triangle.C)) { a = triangle.B; b = triangle.C; opposite = triangle.A; }
                    else { a = triangle.C; b = triangle.A; opposite = triangle.B; }
                    triangles.Remove(triangle);
                    triangles.Add(new MeshTriangle(a, point, opposite));
                    triangles.Add(new MeshTriangle(point, b, opposite));
                }
                return;
            }
            var containing = triangles.FirstOrDefault(t => PointInOrOnTriangle(point, t.A, t.B, t.C));
            if (containing == null) throw new InvalidOperationException("A refinement point lies outside the triangulation.");
            triangles.Remove(containing);
            triangles.Add(new MeshTriangle(containing.A, containing.B, point));
            triangles.Add(new MeshTriangle(containing.B, containing.C, point));
            triangles.Add(new MeshTriangle(containing.C, containing.A, point));
        }

        #endregion

        #region Boundary And Constraint Setup

        private static List<Segment> BuildBoundarySegments(List<PlanarPoint> boundaryLoop)
        {
            List<Segment> segments = new List<Segment>();
            for (int index = 0; index < boundaryLoop.Count; index++)
            {
                segments.Add(new Segment(boundaryLoop[index], boundaryLoop[(index + 1) % boundaryLoop.Count]));
            }

            return segments;
        }

        private static double SignedArea(List<PlanarPoint> loop)
        {
            double area = 0.0;
            for (int index = 0; index < loop.Count; index++)
            {
                PlanarPoint current = loop[index];
                PlanarPoint next = loop[(index + 1) % loop.Count];
                area += (current.X * next.Z) - (next.X * current.Z);
            }

            return area / 2.0;
        }

        #endregion

        #region Planar Geometry Primitives

        private static double Cross(PlanarVector left, PlanarVector right)
        {
            return (left.X * right.Z) - (left.Z * right.X);
        }

        private static bool PointInOrOnTriangle(PlanarPoint point, PlanarPoint a, PlanarPoint b, PlanarPoint c)
        {
            double d1 = Cross(b - a, point - a);
            double d2 = Cross(c - b, point - b);
            double d3 = Cross(a - c, point - c);

            bool hasNegative = d1 < 0.0 || d2 < 0.0 || d3 < 0.0;
            bool hasPositive = d1 > 0.0 || d2 > 0.0 || d3 > 0.0;
            return !(hasNegative && hasPositive);
        }

        private static bool LiesOnSegment(PlanarPoint point, PlanarPoint segmentStart, PlanarPoint segmentEnd)
        {
            double cross = Cross(segmentEnd - segmentStart, point - segmentStart);
            if (Math.Abs(cross) > 1e-9)
            {
                return false;
            }

            double dot = ((point - segmentStart).X * (segmentEnd - segmentStart).X) + ((point - segmentStart).Z * (segmentEnd - segmentStart).Z);
            double lengthSquared = ((segmentEnd - segmentStart).X * (segmentEnd - segmentStart).X) + ((segmentEnd - segmentStart).Z * (segmentEnd - segmentStart).Z);
            return dot >= -1e-9 && dot <= lengthSquared + 1e-9;
        }

        private static PlanarPoint Circumcenter(MeshTriangle triangle)
        {
            double ax = triangle.A.X, az = triangle.A.Z;
            double bx = triangle.B.X, bz = triangle.B.Z;
            double cx = triangle.C.X, cz = triangle.C.Z;

            double d = 2.0 * ((ax * (bz - cz)) + (bx * (cz - az)) + (cx * (az - bz)));
            double ux = (((ax * ax) + (az * az)) * (bz - cz) + ((bx * bx) + (bz * bz)) * (cz - az) + ((cx * cx) + (cz * cz)) * (az - bz)) / d;
            double uz = (((ax * ax) + (az * az)) * (cx - bx) + ((bx * bx) + (bz * bz)) * (ax - cx) + ((cx * cx) + (cz * cz)) * (bx - ax)) / d;

            return new PlanarPoint(ux, uz);
        }

        private static bool IsInCircumcircle(MeshTriangle triangle, PlanarPoint point)
        {
            PlanarPoint centre = Circumcenter(triangle);
            double radiusSquared = ((triangle.A.X - centre.X) * (triangle.A.X - centre.X)) + ((triangle.A.Z - centre.Z) * (triangle.A.Z - centre.Z));
            double distanceSquared = ((point.X - centre.X) * (point.X - centre.X)) + ((point.Z - centre.Z) * (point.Z - centre.Z));
            return distanceSquared < radiusSquared - 1e-9;
        }

        private static double SmallestAngleDegrees(MeshTriangle triangle)
        {
            double sideAB = triangle.A.DistanceTo(triangle.B);
            double sideBC = triangle.B.DistanceTo(triangle.C);
            double sideCA = triangle.C.DistanceTo(triangle.A);

            double angleAtA = AngleFromSides(sideCA, sideAB, sideBC);
            double angleAtB = AngleFromSides(sideAB, sideBC, sideCA);
            double angleAtC = AngleFromSides(sideBC, sideCA, sideAB);

            return Math.Min(angleAtA, Math.Min(angleAtB, angleAtC));
        }

        private static double AngleFromSides(double adjacent1, double adjacent2, double opposite)
        {
            double cosine = ((adjacent1 * adjacent1) + (adjacent2 * adjacent2) - (opposite * opposite)) / (2.0 * adjacent1 * adjacent2);
            cosine = Math.Max(-1.0, Math.Min(1.0, cosine));
            return Math.Acos(cosine) * (180.0 / Math.PI);
        }

        #endregion

        #region Supporting Types

        /// <summary>
        /// A 2D point in the polygon's own working plane, used internally so the triangulation math
        /// (which is inherently planar) is independent of how the polygon happens to be oriented in
        /// the model's global 3D coordinate system.
        /// </summary>
        private struct PlanarPoint : IEquatable<PlanarPoint>
        {
            public PlanarPoint(double x, double z)
            {
                X = x;
                Z = z;
            }

            public double X { get; }
            public double Z { get; }

            public double DistanceTo(PlanarPoint other)
            {
                double dx = X - other.X;
                double dz = Z - other.Z;
                return Math.Sqrt((dx * dx) + (dz * dz));
            }

            public bool Equals(PlanarPoint other) => X.Equals(other.X) && Z.Equals(other.Z);
            public override bool Equals(object obj) => obj is PlanarPoint other && Equals(other);
            public override int GetHashCode() => X.GetHashCode() ^ Z.GetHashCode();

            public static PlanarVector operator -(PlanarPoint left, PlanarPoint right) => new PlanarVector(left.X - right.X, left.Z - right.Z);
            public static bool operator ==(PlanarPoint left, PlanarPoint right) => left.Equals(right);
            public static bool operator !=(PlanarPoint left, PlanarPoint right) => !left.Equals(right);
        }

        private struct PlanarVector
        {
            public PlanarVector(double x, double z)
            {
                X = x;
                Z = z;
            }

            public double X { get; }
            public double Z { get; }
        }

        private sealed class MeshTriangle
        {
            public MeshTriangle(PlanarPoint a, PlanarPoint b, PlanarPoint c)
            {
                double area = Cross(b - a, c - a);
                if (!Finite(area) || Math.Abs(area) <= 1e-15)
                    throw new InvalidOperationException("A numerically degenerate mesh triangle was encountered.");
                A = a;
                B = area > 0 ? b : c;
                C = area > 0 ? c : b;
            }

            public PlanarPoint A { get; }
            public PlanarPoint B { get; }
            public PlanarPoint C { get; }

            public List<PlanarPoint> Points => new List<PlanarPoint> { A, B, C };
            public List<double> Sides => new List<double> { A.DistanceTo(B), B.DistanceTo(C), C.DistanceTo(A) };
        }

        /// <summary>
        /// A constrained boundary edge. Used both to prevent Delaunay edge flips from removing a
        /// boundary edge, and to detect Ruppert-style encroachment during refinement.
        /// </summary>
        private sealed class Segment : IEquatable<Segment>
        {
            public Segment(PlanarPoint start, PlanarPoint end)
            {
                Start = start;
                End = end;
            }

            public PlanarPoint Start { get; }
            public PlanarPoint End { get; }
            public PlanarPoint Midpoint => new PlanarPoint((Start.X + End.X) / 2.0, (Start.Z + End.Z) / 2.0);

            /// <summary>
            /// A point encroaches this segment if it lies within the segment's diametral circle
            /// (the circle whose diameter is the segment itself) — the standard Ruppert encroachment test.
            /// </summary>
            public bool Encroaches(PlanarPoint point)
            {
                PlanarVector toStart = Start - point;
                PlanarVector toEnd = End - point;
                double dot = (toStart.X * toEnd.X) + (toStart.Z * toEnd.Z);
                return dot < 0.0;
            }

            public bool Equals(Segment other)
            {
                if (other is null) return false;
                return (Start.Equals(other.Start) && End.Equals(other.End)) || (Start.Equals(other.End) && End.Equals(other.Start));
            }

            public override bool Equals(object obj) => obj is Segment other && Equals(other);
            public override int GetHashCode() => Start.GetHashCode() ^ End.GetHashCode();
        }


        private sealed class PlanarFrame
        {
            private readonly Node origin;
            private readonly System.Windows.Media.Media3D.Vector3D localX, localZ;
            public double Scale { get; }

            private PlanarFrame(Node origin, System.Windows.Media.Media3D.Vector3D x,
                System.Windows.Media.Media3D.Vector3D z, double scale)
            { this.origin = origin; localX = x; localZ = z; Scale = scale; }

            public static PlanarFrame FromBoundary(List<Node> nodes)
            {
                var origin = nodes[0];
                double scale = nodes.Max(node => Math.Max(Math.Abs(node.X - origin.X),
                    Math.Max(Math.Abs(node.Y - origin.Y), Math.Abs(node.Z - origin.Z))));
                if (!Finite(scale) || scale <= 0) throw new ArgumentException("The coordinate extent must be finite and nonzero.", "boundaryNodes");
                var offsets = nodes.Select(node => new System.Windows.Media.Media3D.Vector3D(
                    (node.X - origin.X) / scale, (node.Y - origin.Y) / scale, (node.Z - origin.Z) / scale)).ToList();
                var x = offsets[1];
                if (x.Length <= 1e-10) throw new ArgumentException("Consecutive boundary coordinates are coincident at the polygon scale.", "boundaryNodes");
                x.Normalize();
                var normal = offsets.Select(offset => System.Windows.Media.Media3D.Vector3D.CrossProduct(x, offset))
                    .OrderByDescending(cross => cross.LengthSquared).First();
                if (normal.Length <= 1e-10) throw new ArgumentException("The boundary is collinear at the polygon scale.", "boundaryNodes");
                normal.Normalize();
                if (offsets.Any(offset => Math.Abs(System.Windows.Media.Media3D.Vector3D.DotProduct(offset, normal)) > 1e-9))
                    throw new ArgumentException("The boundary must be coplanar within 1e-9 of its extent.", "boundaryNodes");
                var z = System.Windows.Media.Media3D.Vector3D.CrossProduct(normal, x);
                z.Normalize();
                return new PlanarFrame(origin, x, z, scale);
            }

            public PlanarPoint ToPlanar(Node node)
            {
                var offset = new System.Windows.Media.Media3D.Vector3D((node.X - origin.X) / Scale,
                    (node.Y - origin.Y) / Scale, (node.Z - origin.Z) / Scale);
                return new PlanarPoint(System.Windows.Media.Media3D.Vector3D.DotProduct(offset, localX),
                    System.Windows.Media.Media3D.Vector3D.DotProduct(offset, localZ));
            }

            public Node ToNode(PlanarPoint point, int id)
            {
                var offset = (point.X * localX + point.Z * localZ) * Scale;
                double x = origin.X + offset.X, y = origin.Y + offset.Y, z = origin.Z + offset.Z;
                if (!Finite(x) || !Finite(y) || !Finite(z)) throw new InvalidOperationException("Generated coordinates overflowed.");
                return new Node(id, x, y, z);
            }
        }

        #endregion
    }
}
