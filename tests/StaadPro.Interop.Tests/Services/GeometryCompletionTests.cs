
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Media3D;
using System.Xml.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Models;
using StaadPro.Interop.Services;

namespace StaadPro.Interop.Tests.Services
{
    [TestFixture]
    public class GeometryCompletionTests
    {
        private static OSGeometryAdapter Adapter() => new OSGeometryAdapter(new OpenStaadWrapper(new object()));
        private static Node N(int id, double x, double z = 0, double y = 0) => new Node(id, x, y, z);
        private static Node[] Square(double scale = 1) => new[] { N(1, 0), N(2, 4 * scale), N(3, 4 * scale, 4 * scale), N(4, 0, 4 * scale) };

        [Test]
        public void ReconstructsUnorderedReversedChainAndSetsSourceBackReferences()
        {
            var a = N(1, 0); var b = N(2, 2); var c = N(3, 4); var d = N(4, 6);
            var beams = new[] { new Beam(12, c, b), new Beam(13, c, d), new Beam(11, a, b) };
            var input = new Once<Beam>(beams);
            var result = Adapter().IdentifyPhysicalMembers(input);
            Assert.That(input.Enumerations, Is.EqualTo(1));
            Assert.That(result.Members.Count, Is.EqualTo(1));
            Assert.That(result.DiscardedBeams, Is.Empty);
            var member = result.Members.Single();
            Assert.That(member.Id, Is.EqualTo(1));
            Assert.That(new[] { member.StartNode.Id, member.EndNode.Id }, Is.EquivalentTo(new[] { 1, 4 }));
            Assert.That(member.Beams, Is.EquivalentTo(beams));
            Assert.That(member.Nodes.Select(n => n.Id), Is.EquivalentTo(new[] { 1, 2, 3, 4 }));
            Assert.That(beams.All(beam => ReferenceEquals(beam.Member, member)), Is.True);
            Assert.That(JsonConvert.SerializeObject(beams[0]), Does.Not.Contain("\"Member\":"));
        }

        [Test]
        public void BendsSplitRunsAndDisconnectedSinglesReceiveSequentialSyntheticIds()
        {
            var a = N(1, 0); var b = N(2, 1); var c = N(3, 1, 2);
            var beams = new[] { new Beam(1, a, b), new Beam(2, b, c), new Beam(3, N(4, 10), N(5, 11)) };
            var result = Adapter().IdentifyPhysicalMembers(beams);
            Assert.That(result.Members.Count, Is.EqualTo(3));
            Assert.That(result.Members.Select(m => m.Id), Is.EquivalentTo(new[] { 1, 2, 3 }));
            Assert.That(result.Members.SelectMany(m => m.Beams), Is.EquivalentTo(beams));
        }

        [TestCase(0.001, 1)]
        [TestCase(0.1, 2)]
        public void ConfigurableAngleToleranceControlsPairwiseRunSplitting(double angle, int expected)
        {
            var a = N(1, 0); var b = N(2, 1);
            var c = N(3, 1 + Math.Cos(angle * Math.PI / 180), Math.Sin(angle * Math.PI / 180));
            var result = Adapter().IdentifyPhysicalMembers(new[] { new Beam(1, a, b), new Beam(2, b, c) }, 0.005);
            Assert.That(result.Members.Count, Is.EqualTo(expected));
        }

        [Test]
        public void BranchedAndCyclicComponentsAreDiscardedWithoutChangingPriorAssignments()
        {
            var a = N(1, 0); var b = N(2, 1); var c = N(3, 2); var d = N(4, 1, 1);
            var branch = new[] { new Beam(1, a, b), new Beam(2, b, c), new Beam(3, b, d) };
            var old = new Member(50, a, c);
            branch[0].Member = old;
            var result = Adapter().IdentifyPhysicalMembers(branch);
            Assert.That(result.Members, Is.Empty);
            Assert.That(result.DiscardedBeams, Is.EquivalentTo(branch));
            Assert.That(branch[0].Member, Is.SameAs(old));
            var cycle = new[] { new Beam(4, a, b), new Beam(5, b, d), new Beam(6, d, a) };
            result = Adapter().IdentifyPhysicalMembers(cycle);
            Assert.That(result.Members, Is.Empty);
            Assert.That(result.DiscardedBeams, Is.EquivalentTo(cycle));
        }

        [Test]
        public void DuplicateBeamIdsKeepFirstInstanceAndNullsAndEmptyInputsAreHandled()
        {
            var a = N(1, 0); var b = N(2, 1);
            var first = new Beam(1, a, b); var duplicate = new Beam(1, a, b);
            var result = Adapter().IdentifyPhysicalMembers(new[] { null, first, duplicate });
            Assert.That(result.Members.Single().Beams.Single(), Is.SameAs(first));
            Assert.That(duplicate.Member, Is.Null);
            Assert.That(Adapter().IdentifyPhysicalMembers(new Beam[0]).Members, Is.Empty);
            Assert.That(Adapter().GroupPhysicalMembersByPlanAngle(new Member[0]), Is.Empty);
            Assert.Throws<ArgumentNullException>(() => Adapter().IdentifyPhysicalMembers(null));
            Assert.Throws<ArgumentNullException>(() => Adapter().GroupPhysicalMembersByPlanAngle(null));
        }

        [TestCase(-1)]
        [TestCase(90)]
        [TestCase(double.NaN)]
        [TestCase(double.PositiveInfinity)]
        public void InvalidAngularTolerancesAreRejectedEvenForEmptyInputs(double tolerance)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Adapter().IdentifyPhysicalMembers(new Beam[0], tolerance));
            Assert.Throws<ArgumentOutOfRangeException>(() => Adapter().GroupPhysicalMembersByPlanAngle(new Member[0], tolerance));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void InvalidBeamGeometryIsRejectedBeforeAnyBackReferenceChanges(int invalid)
        {
            var a = N(1, 0); var b = N(2, 1); var valid = new Beam(10, a, b);
            var old = new Member(90, a, b); valid.Member = old;
            Beam bad;
            switch (invalid)
            {
                case 0: bad = new Beam(11, null, b); break;
                case 1: bad = new Beam(0, a, b); break;
                case 2: bad = new Beam(11, a, N(3, 0)); break;
                case 3: bad = new Beam(11, N(3, double.NaN), b); break;
                case 4: bad = new Beam(11, N(1, 99), N(3, 100)); break;
                default: bad = new Beam(10, a, N(3, 3)); break;
            }
            Assert.Throws<ArgumentException>(() => Adapter().IdentifyPhysicalMembers(new[] { valid, bad }));
            Assert.That(valid.Member, Is.SameAs(old));
        }

        [Test]
        public void PlanGroupingIsUndirectedWrapsNear180AndUsesXZForVerticalMembers()
        {
            Func<int, double, Member> at = (id, angle) => new Member(id, N(id * 2, 0),
                N(id * 2 + 1, Math.Cos(angle * Math.PI / 180), Math.Sin(angle * Math.PI / 180)));
            var zero = at(1, 0); var reversed = at(2, 180); var nearWrap = at(3, 179.9995);
            var diagonal = at(4, 45); var vertical = new Member(5, N(20, 0), N(21, 0, 0, 4));
            var input = new Once<Member>(new[] { diagonal, zero, reversed, nearWrap, vertical, null });
            var groups = Adapter().GroupPhysicalMembersByPlanAngle(input).ToList();
            Assert.That(input.Enumerations, Is.EqualTo(1));
            Assert.That(groups.Select(g => g.PlanAngleInDegrees), Is.EqualTo(new[] { 0.0, 45.0 }).Within(1e-10));
            Assert.That(groups[0].Members, Is.EquivalentTo(new[] { zero, reversed, nearWrap, vertical }));
            Assert.That(groups[1].Members.Single(), Is.SameAs(diagonal));
        }

        [Test]
        public void PlanGroupingUsesFirstRepresentativeRatherThanTransitiveClustering()
        {
            Func<int, double, Member> at = (id, angle) => new Member(id, N(id * 2, 0),
                N(id * 2 + 1, Math.Cos(angle * Math.PI / 180), Math.Sin(angle * Math.PI / 180)));
            var first = at(1, 0); var middle = at(2, 0.75); var last = at(3, 1.5);
            var groups = Adapter().GroupPhysicalMembersByPlanAngle(new[] { first, middle, last, first }, 1).ToList();
            Assert.That(groups.Count, Is.EqualTo(2));
            Assert.That(groups[0].Members, Is.EqualTo(new[] { first, middle, first }));
            Assert.Throws<ArgumentException>(() => Adapter().GroupPhysicalMembersByPlanAngle(new[] { new Member() }));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void MeshPreservesBoundaryReferencesAndCoversConcavePolygonInEitherWinding(bool reverse)
        {
            var outline = new[] { N(1, 0), N(2, 3), N(3, 3, 1), N(4, 1, 1), N(5, 1, 3), N(6, 0, 3) };
            if (reverse) Array.Reverse(outline);
            var mesh = Adapter().GeneratePolygonMesh(outline, 100, 200, 1);
            CheckMesh(mesh, outline, 5);
            Assert.That(mesh.Plates.Count, Is.EqualTo(4));
            Assert.That(mesh.Plates[0].Id, Is.EqualTo(200));
            Assert.That(mesh.InteriorNodes, Is.Empty);
            Assert.That(mesh.ReachedTargetQuality, Is.True);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void MeshSupportsHorizontalVerticalAndTiltedPlanes(int plane)
        {
            var outline = Square();
            if (plane == 1) outline = outline.Select(n => new Node(n.Id, 5, n.X, n.Z)).ToArray();
            if (plane == 2) outline = outline.Select(n => new Node(n.Id, n.X + 100, n.X + 200, n.Z + 300)).ToArray();
            var mesh = Adapter().GeneratePolygonMesh(outline, 100, 200);
            CheckMesh(mesh, outline, plane == 2 ? 16 * Math.Sqrt(2) : 16);
            Assert.That(mesh.AchievedMinimumAngleDegrees, Is.GreaterThanOrEqualTo(20));
        }

        [TestCase(1e-6)]
        [TestCase(1)]
        [TestCase(1e6)]
        public void MeshUsesScaleRelativePredicates(double scale)
        {
            var outline = Square(scale);
            var mesh = Adapter().GeneratePolygonMesh(outline, 100, 200);
            CheckMesh(mesh, outline, 16 * scale * scale);
            Assert.That(mesh.ReachedTargetQuality, Is.True);
        }

        [Test]
        public void EdgeRefinementReturnsExplicitUniqueIdsConformingTopologyAndHonestQuality()
        {
            var outline = Square();
            var mesh = Adapter().GeneratePolygonMesh(outline, 100, 200, 20, 2.5);
            CheckMesh(mesh, outline, 16);
            Assert.That(mesh.InteriorNodes.Count, Is.GreaterThan(0));
            Assert.That(mesh.InteriorNodes.Select(n => n.Id), Is.EqualTo(Enumerable.Range(100, mesh.InteriorNodes.Count)));
            Assert.That(mesh.Plates.Select(p => p.Id), Is.EqualTo(Enumerable.Range(200, mesh.Plates.Count)));
            Assert.That(mesh.ReachedTargetQuality, Is.True);
            Assert.That(mesh.Plates.SelectMany(Edges).All(edge => Distance(edge[0], edge[1]) <= 2.5 + 1e-8), Is.True);
            Assert.That(mesh.InteriorNodes.Any(n => n.X == 0 || n.X == 4 || n.Z == 0 || n.Z == 4), Is.True,
                "Source-compatible InteriorNodes includes boundary-segment split nodes.");
        }

        [Test, Timeout(30000)]
        public void UnattainableAngleReturnsBoundedValidBestEffortMesh()
        {
            var outline = Square();
            var mesh = Adapter().GeneratePolygonMesh(outline, 100, 1000, 59);
            CheckMesh(mesh, outline, 16);
            Assert.That(mesh.InteriorNodes.Count, Is.LessThanOrEqualTo(256));
            Assert.That(mesh.ReachedTargetQuality, Is.False);
            Assert.That(mesh.AchievedMinimumAngleDegrees, Is.LessThan(59));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        public void MeshRejectsInvalidBoundaries(int invalid)
        {
            var boundary = Square();
            switch (invalid)
            {
                case 0: boundary[1] = null; break;
                case 1: boundary[1] = N(1, 4); break;
                case 2: boundary[1] = N(2, 0); break;
                case 3: boundary[2] = N(3, 4, 4, 1); break;
                case 4: boundary[2] = N(3, double.NaN, 4); break;
                case 5: boundary = new[] { N(1, 0), N(2, 4, 4), N(3, 0, 4), N(4, 4, 0) }; break;
                case 6: boundary = new[] { N(1, 0), N(2, 1), N(3, 2) }; break;
                default: boundary = new[] { N(1, 0), N(2, 4), N(3, 2), N(4, 2, 2), N(5, 0, 2) }; break;
            }
            Assert.Throws<ArgumentException>(() => Adapter().GeneratePolygonMesh(boundary, 100, 200));
        }

        [Test]
        public void MeshRejectsNullSizeAndRepeatedClosingNode()
        {
            Assert.Throws<ArgumentNullException>(() => Adapter().GeneratePolygonMesh(null, 100, 200));
            Assert.Throws<ArgumentException>(() => Adapter().GeneratePolygonMesh(new Node[2], 100, 200));
            Assert.Throws<ArgumentException>(() => Adapter().GeneratePolygonMesh(new Node[513], 100, 200));
            var square = Square();
            Assert.Throws<ArgumentException>(() => Adapter().GeneratePolygonMesh(square.Concat(new[] { square[0] }).ToArray(), 100, 200));
        }

        [TestCase(0)]
        [TestCase(60)]
        [TestCase(double.NaN)]
        [TestCase(double.PositiveInfinity)]
        public void MeshRejectsInvalidAngles(double angle) =>
            Assert.Throws<ArgumentOutOfRangeException>(() => Adapter().GeneratePolygonMesh(Square(), 100, 200, angle));

        [TestCase(-1)]
        [TestCase(double.NaN)]
        [TestCase(double.PositiveInfinity)]
        public void MeshRejectsInvalidEdgeLimits(double length) =>
            Assert.Throws<ArgumentOutOfRangeException>(() => Adapter().GeneratePolygonMesh(Square(), 100, 200, 20, length));

        [TestCase(0, 100)]
        [TestCase(100, 0)]
        [TestCase(-1, 100)]
        public void MeshRejectsNonpositiveStartingIds(int nodeId, int plateId) =>
            Assert.Throws<ArgumentOutOfRangeException>(() => Adapter().GeneratePolygonMesh(Square(), nodeId, plateId));

        [Test]
        public void MeshChecksGeneratedIdCollisionAndOverflowWithoutChangingInput()
        {
            var outline = Square();
            Assert.Throws<ArgumentException>(() => Adapter().GeneratePolygonMesh(outline, 1, 200, 20, 2.5));
            Assert.Throws<OverflowException>(() => Adapter().GeneratePolygonMesh(outline, int.MaxValue, 200, 20, 2.5));
            Assert.Throws<OverflowException>(() => Adapter().GeneratePolygonMesh(outline, 100, int.MaxValue));
            Assert.That(outline.Select(n => n.Id), Is.EqualTo(new[] { 1, 2, 3, 4 }));
        }

        [Test]
        public void MeshAcceptsCollinearBoundarySubdivisionWithoutDroppingOriginalVertex()
        {
            var boundary = new[] { N(1, 0), N(2, 2), N(3, 4), N(4, 4, 4), N(5, 0, 4) };
            var mesh = Adapter().GeneratePolygonMesh(boundary, 100, 200, 1);
            CheckMesh(mesh, boundary, 16);
            Assert.That(mesh.Plates.SelectMany(p => new[] { p.A, p.B, p.C }).Select(n => n.Id).Distinct(),
                Is.SupersetOf(boundary.Select(n => n.Id)));
        }

        [TestCase("IdentifyPhysicalMembers")]
        [TestCase("GroupPhysicalMembersByPlanAngle")]
        [TestCase("GeneratePolygonMesh")]
        public void FinalGeometryFunctionsHaveIdenticalExpandedInterfaceAndAdapterDocumentation(string name)
        {
            var contract = typeof(IOSGeometry).GetMethod(name);
            var adapter = typeof(OSGeometryAdapter).GetMethod(name);
            Assert.That(adapter.ReturnType, Is.EqualTo(contract.ReturnType));
            Assert.That(adapter.GetParameters().Select(p => p.Name), Is.EqualTo(contract.GetParameters().Select(p => p.Name)));
            Assert.That(adapter.GetParameters().Select(p => p.DefaultValue), Is.EqualTo(contract.GetParameters().Select(p => p.DefaultValue)));
            var xml = XDocument.Load(Path.ChangeExtension(typeof(IOSGeometry).Assembly.Location, ".xml"));
            Func<Type, XElement> entry = type => xml.Descendants("member").Single(e =>
                ((string)e.Attribute("name")).StartsWith("M:" + type.FullName + "." + name + "("));
            var first = entry(typeof(IOSGeometry)); var second = entry(typeof(OSGeometryAdapter));
            Assert.That(second.Elements().Select(e => e.ToString()), Is.EqualTo(first.Elements().Select(e => e.ToString())));
            foreach (string tag in new[] { "summary", "returns", "remarks", "example" })
                Assert.That(first.Element(tag)?.Value, Is.Not.Null.And.Not.Empty);
            Assert.That(first.Elements("param").Select(e => (string)e.Attribute("name")),
                Is.EqualTo(contract.GetParameters().Select(p => p.Name)));
            Assert.That(first.Elements("exception").Count(), Is.GreaterThanOrEqualTo(3));
        }


        [Test]
        public void WrapperExposesCompletedGeometryAndRetainsAllBindingsAfterRefresh()
        {
            var native = new StaadPro.Interop.Tests.Models.OpenStaadWrapperParityTests.NativeRoot();
            using (var wrapper = new OpenStaadWrapper(native))
            {
                var firstGeometry = wrapper.Geometry;
                native.Queries.Clear();
                var outline = Square();
                var beam = new Beam(99, outline[0], outline[1]);
                Assert.That(wrapper.Geometry.IdentifyPhysicalMembers(new[] { beam }).Members.Count, Is.EqualTo(1));
                Assert.That(wrapper.Geometry.GroupPhysicalMembersByPlanAngle(new[] { beam.Member }).Count(), Is.EqualTo(1));
                CheckMesh(wrapper.Geometry.GeneratePolygonMesh(outline, 100, 200), outline, 16);
                Assert.That(native.Queries, Is.Empty, "Managed Geometry work must not query native metadata.");
                wrapper.RefreshComObjects();
                Assert.That(wrapper.Geometry, Is.Not.SameAs(firstGeometry));
                Assert.That((object)wrapper.Geometry.ComObject, Is.SameAs(native.Geometry));
                Assert.That(wrapper.Output, Is.Not.Null);
                Assert.That(wrapper.Property, Is.Not.Null);
                Assert.That(wrapper.Support, Is.Not.Null);
                Assert.That(wrapper.Design, Is.Not.Null);
                Assert.That(wrapper.Commands, Is.Not.Null);
                Assert.That(wrapper.Table, Is.Not.Null);
                Assert.That(wrapper.View, Is.Not.Null);
                Assert.That(wrapper.IdeaStatiCa, Is.Not.Null);
            }
        }

        [TestCase(5)]
        [TestCase(8)]
        [TestCase(13)]
        [TestCase(21)]
        public void MeshMaintainsTopologyForDeterministicIrregularPolygons(int count)
        {
            var random = new Random(312 + count);
            var outline = Enumerable.Range(0, count).Select(i =>
            {
                double radius = 2 + random.NextDouble();
                double angle = i * 2 * Math.PI / count;
                return N(i + 1, radius * Math.Cos(angle), radius * Math.Sin(angle));
            }).ToArray();
            double area = Math.Abs(Enumerable.Range(0, count).Sum(i => outline[i].X * outline[(i + 1) % count].Z -
                outline[(i + 1) % count].X * outline[i].Z)) / 2;
            CheckMesh(Adapter().GeneratePolygonMesh(outline, 100, 1000, 1), outline, area);
            Array.Reverse(outline);
            CheckMesh(Adapter().GeneratePolygonMesh(outline, 100, 1000, 1), outline, area);
        }

        private static IEnumerable<Node[]> Edges(Plate plate)
        {
            yield return new[] { plate.A, plate.B };
            yield return new[] { plate.B, plate.C };
            yield return new[] { plate.C, plate.A };
        }

        private static double Distance(Node a, Node b) => (Node.GetVector3D(a, b)).Length;

        private static void CheckMesh(PolygonMeshResult mesh, Node[] outline, double expectedArea)
        {
            Assert.That(mesh.BoundaryNodes.Count, Is.EqualTo(outline.Length));
            for (int i = 0; i < outline.Length; i++) Assert.That(mesh.BoundaryNodes[i], Is.SameAs(outline[i]));
            var all = mesh.BoundaryNodes.Concat(mesh.InteriorNodes).ToList();
            Assert.That(all.Select(n => n.Id).Distinct().Count(), Is.EqualTo(all.Count));
            Assert.That(mesh.Plates, Is.Not.Empty);
            Assert.That(mesh.Plates.All(p => p.IsTriangle && p.Area > 0 && !double.IsNaN(p.Area)), Is.True);
            Assert.That(mesh.Plates.Sum(p => p.Area), Is.EqualTo(expectedArea).Within(Math.Abs(expectedArea) * 1e-8));
            Assert.That(mesh.Plates.SelectMany(p => new[] { p.A, p.B, p.C }).All(n => all.Any(a => ReferenceEquals(a, n))), Is.True);
            var edges = mesh.Plates.SelectMany(Edges).ToList();
            var incidence = edges.GroupBy(edge => Math.Min(edge[0].Id, edge[1].Id) + ":" + Math.Max(edge[0].Id, edge[1].Id));
            foreach (var group in incidence)
            {
                Assert.That(group.Count(), Is.InRange(1, 2));
                var edge = group.First();
                if (group.Count() == 1)
                {
                    Assert.That(Enumerable.Range(0, outline.Length).Any(i =>
                        OnSegment(edge[0], outline[i], outline[(i + 1) % outline.Length]) &&
                        OnSegment(edge[1], outline[i], outline[(i + 1) % outline.Length])), Is.True,
                        "Every exterior edge must lie on an original boundary segment.");
                }
                foreach (Node node in all.Where(n => n.Id != edge[0].Id && n.Id != edge[1].Id))
                    Assert.That(OnSegment(node, edge[0], edge[1]), Is.False, "No hanging node may lie inside an unsplit edge.");
            }
        }

        private static bool OnSegment(Node point, Node a, Node b)
        {
            double length = Distance(a, b);
            return Math.Abs(Distance(a, point) + Distance(point, b) - length) <= length * 1e-9;
        }

        private sealed class Once<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> source;
            public int Enumerations;
            public Once(IEnumerable<T> source) { this.source = source; }
            public IEnumerator<T> GetEnumerator()
            {
                if (++Enumerations != 1) throw new InvalidOperationException("Sequence enumerated twice.");
                return source.GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
