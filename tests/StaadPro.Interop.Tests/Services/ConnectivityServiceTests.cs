using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;
using StaadPro.Interop.Services;
using StaadPro.Interop.Tests.Models;

namespace StaadPro.Interop.Tests.Services
{
    [TestFixture]
    public class ConnectivityServiceTests
    {
        private static Node N(int id) => new Node(id, id, 0, 0);
        private static List<List<T>> Groups<T>(IEnumerable<IEnumerable<T>> source) => source.Select(g => g.ToList()).ToList();

        [Test]
        public void Beams_SeparateComponentsAndOrderAnUnorderedChain()
        {
            var ab = new Beam(1, N(1), N(2));
            var bc = new Beam(2, N(2), N(3));
            var cd = new Beam(3, N(3), N(4));
            var other = new Beam(4, N(8), N(9));
            var groups = Groups(ConnectivityService.SegregateBeamsByConnectivity(new[] { bc, other, cd, ab }));
            Assert.That(groups.Count, Is.EqualTo(2));
            var chain = groups.Single(g => g.Count == 3);
            Assert.That(chain.Select(b => b.Id).ToArray(), Is.EqualTo(new[] { 1, 2, 3 }).Or.EqualTo(new[] { 3, 2, 1 }));
            Assert.That(groups.Single(g => g.Count == 1).Single(), Is.SameAs(other));
        }

        [Test]
        public void Beams_BranchesAndLoopsKeepEveryBeamInOneComponent()
        {
            var branch = new[] { new Beam(1, N(1), N(2)), new Beam(2, N(2), N(3)), new Beam(3, N(2), N(4)) };
            var loop = new[] { new Beam(4, N(5), N(6)), new Beam(5, N(6), N(7)), new Beam(6, N(7), N(5)) };
            var groups = Groups(ConnectivityService.SegregateBeamsByConnectivity(branch.Concat(loop)));
            Assert.That(groups.Count, Is.EqualTo(2));
            Assert.That(groups.SelectMany(g => g).Select(b => b.Id), Is.EquivalentTo(Enumerable.Range(1, 6)));
            Assert.That(groups.All(g => g.Count == 3), Is.True);
        }

        [Test]
        public void Beams_UseNodeIdsRatherThanCoordinatesOrNodeConnectedBeams()
        {
            var first = new Beam(1, new Node(1, 0, 0, 0), new Node(2, 1, 0, 0));
            var second = new Beam(2, new Node(2, 99, 0, 0), new Node(3, 2, 0, 0));
            var touchingCoordinates = new Beam(3, new Node(4, 1, 0, 0), new Node(5, 2, 0, 0));
            var groups = Groups(ConnectivityService.SegregateBeamsByConnectivity(new[] { first, second, touchingCoordinates }));
            Assert.That(groups.Count, Is.EqualTo(2));
            Assert.That(groups.Single(g => g.Count == 2), Is.EquivalentTo(new[] { first, second }));
        }

        [Test]
        public void Beams_NullEntriesAndDuplicateIdsAreIgnoredWithoutReenumeratingInput()
        {
            var original = new Beam(1, N(1), N(2));
            var sequence = new SingleUseSequence<Beam>(new[] { original, null, new Beam(1, N(8), N(9)) });
            var groups = Groups(ConnectivityService.SegregateBeamsByConnectivity(sequence));
            Assert.That(groups.Single().Single(), Is.SameAs(original));
            Assert.That(sequence.Enumerations, Is.EqualTo(1));
        }

        [Test]
        public void Members_DefaultEndpointScopeDoesNotJoinAnInteriorTJunction()
        {
            var main = new Member(1, N(1), N(3));
            main.Nodes.Add(N(2));
            var branch = new Member(2, N(2), N(4));
            Assert.That(Groups(ConnectivityService.SegregateMembersByConnectivity(new[] { main, branch })).Count, Is.EqualTo(2));
            Assert.That(Groups(ConnectivityService.SegregateMembersByConnectivity(new[] { main, branch }, MemberConnectivityScope.AnyNode)).Count, Is.EqualTo(1));
        }

        [Test]
        public void Members_GroupTransitivelyAndDeduplicateById()
        {
            var members = new[] { new Member(1, N(1), N(2)), new Member(2, N(2), N(3)), new Member(3, N(3), N(4)), null, new Member(1, N(8), N(9)) };
            var groups = Groups(ConnectivityService.SegregateMembersByConnectivity(members));
            Assert.That(groups.Single().Select(m => m.Id), Is.EquivalentTo(new[] { 1, 2, 3 }));
            Assert.That(groups.Single()[0], Is.SameAs(members[0]));
        }

        [Test]
        public void Members_AnyNodeUsesNodesCollectionWithoutRebuildingItFromBeams()
        {
            var first = new Member(1, N(1), N(2));
            first.Nodes.Clear();
            var second = new Member(2, N(2), N(3));
            Assert.That(Groups(ConnectivityService.SegregateMembersByConnectivity(new[] { first, second })).Count, Is.EqualTo(1));
            Assert.That(Groups(ConnectivityService.SegregateMembersByConnectivity(new[] { first, second }, MemberConnectivityScope.AnyNode)).Count, Is.EqualTo(2));
        }

        [Test]
        public void Plates_DefaultEdgeScopeExcludesCornerOnlyContacts()
        {
            var a = new Plate(1, N(1), N(2), N(3));
            var edge = new Plate(2, N(3), N(2), N(4));
            var corner = new Plate(3, N(4), N(5), N(6));
            Assert.That(Groups(ConnectivityService.SegregatePlatesByConnectivity(new[] { a, edge, corner })).Count, Is.EqualTo(2));
            Assert.That(Groups(ConnectivityService.SegregatePlatesByConnectivity(new[] { a, edge, corner }, PlateConnectivityScope.AnyCorner)).Count, Is.EqualTo(1));
        }

        [Test]
        public void Plates_WrappingEdgesAndRepeatedTriangularCornerAreSupported()
        {
            var a = N(1); var b = N(2); var c = N(3);
            var repeatedTriangle = new Plate(1, a, b, c, a);
            var acrossClosingEdge = new Plate(2, c, a, N(4));
            var groups = Groups(ConnectivityService.SegregatePlatesByConnectivity(new[] { repeatedTriangle, acrossClosingEdge, null, repeatedTriangle }));
            Assert.That(groups.Single(), Is.EquivalentTo(new[] { repeatedTriangle, acrossClosingEdge }));
        }

        [Test]
        public void Plates_PreserveSourceCandidateEdgePredicateForNonmatchingTargetWinding()
        {
            var target = new Plate(1, N(1), N(2), N(3), N(4));
            var candidate = new Plate(2, N(1), N(3), N(5));
            Assert.That(Groups(ConnectivityService.SegregatePlatesByConnectivity(new[] { target, candidate })).Count, Is.EqualTo(1));
        }

        [Test]
        public void Collections_NullThrowsAndEmptyResultsAreIndependent()
        {
            Assert.Throws<ArgumentNullException>(() => ConnectivityService.SegregateBeamsByConnectivity(null));
            Assert.Throws<ArgumentNullException>(() => ConnectivityService.SegregateMembersByConnectivity(null));
            Assert.Throws<ArgumentNullException>(() => ConnectivityService.SegregatePlatesByConnectivity(null));
            var first = ConnectivityService.SegregateBeamsByConnectivity(Array.Empty<Beam>());
            var second = ConnectivityService.SegregateBeamsByConnectivity(Array.Empty<Beam>());
            Assert.That(first, Is.Empty.And.Not.SameAs(second));
            Assert.That(ConnectivityService.SegregateMembersByConnectivity(new Member[] { null }), Is.Empty);
            Assert.That(ConnectivityService.SegregatePlatesByConnectivity(new Plate[] { null }), Is.Empty);
        }

        [Test]
        public void UndefinedScopesRetainSourceFallbacks()
        {
            var members = new[] { new Member(1, N(1), N(2)), new Member(2, N(2), N(3)) };
            var plates = new[] { new Plate(1, N(1), N(2), N(3)), new Plate(2, N(2), N(3), N(4)) };
            Assert.That(Groups(ConnectivityService.SegregateMembersByConnectivity(members, (MemberConnectivityScope)99)).Count, Is.EqualTo(1));
            Assert.That(Groups(ConnectivityService.SegregatePlatesByConnectivity(plates, (PlateConnectivityScope)99)).Count, Is.EqualTo(1));
        }

        [Test]
        public void GeometryAdapter_ExposesThreeExactContractsWithoutNativeCalls()
        {
            using (var wrapper = new OpenStaadWrapper(new OpenStaadWrapperTests.MockStaadRootCom()))
            {
                IOSGeometry geometry = new OSGeometryAdapter(wrapper);
                Assert.That(Groups(geometry.SegregateBeamsByConnectivity(new[] { new Beam(1, N(1), N(2)) })).Count, Is.EqualTo(1));
                Assert.That(Groups(geometry.SegregateMembersByConnectivity(new[] { new Member(1, N(1), N(2)) })).Count, Is.EqualTo(1));
                Assert.That(Groups(geometry.SegregatePlatesByConnectivity(new[] { new Plate(1, N(1), N(2), N(3)) })).Count, Is.EqualTo(1));
            }
            var method = typeof(IOSGeometry).GetMethod("SegregateMembersByConnectivity");
            Assert.That(method.GetParameters().Select(p => p.Name), Is.EqualTo(new[] { "members", "scope" }));
            Assert.That(method.GetParameters()[1].DefaultValue, Is.EqualTo(MemberConnectivityScope.EndpointsOnly));
            method = typeof(IOSGeometry).GetMethod("SegregatePlatesByConnectivity");
            Assert.That(method.GetParameters().Select(p => p.Name), Is.EqualTo(new[] { "plates", "scope" }));
            Assert.That(method.GetParameters()[1].DefaultValue, Is.EqualTo(PlateConnectivityScope.SharedEdge));
        }

        private sealed class SingleUseSequence<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> _items;
            public int Enumerations { get; private set; }
            public SingleUseSequence(IEnumerable<T> items) { _items = items; }
            public IEnumerator<T> GetEnumerator()
            {
                if (++Enumerations > 1) throw new InvalidOperationException("Sequence enumerated twice.");
                return _items.GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
