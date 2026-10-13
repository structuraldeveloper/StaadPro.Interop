using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Adapter
{
    public class CombinationGroupingReadTests
    {
        public class Root { public object Geometry => null; public Native Load { get; } = new Native(); }
        public class Native
        {
            public object PairCount = 2;
            public object SourceIds = new[] { 41, 17 };
            public object Factors = new[] { -.75, 1.123456789123 };
            public object PairStatus = true;
            public int Active;
            public int PairFetches;
            public int AssignmentCount = 2;
            public bool Activation = true;
            public int GetNoOfLoadAndFactorPairsForCombination(int id) => Convert.ToInt32(PairCount);
            public bool SetLoadActive(int id) { Active = id; return Activation; }
            public int SetReferenceLoadActive(int id) { Active = id; return Activation ? id : 0; }
            public string GetLoadCaseTitle(int id) => "TITLE " + id;
            public int GetLoadType(int id) => (int)LoadType.Dead;
            public int GetPrimaryLoadCaseCount() => 2;
            public int GetPrimaryLoadCaseNumbers(ref object ids) { ids = new[] { 17, 41 }; return 2; }
            public object GetLoadAndFactorForCombination(int id, ref object ids, ref object factors)
            { PairFetches++; ids = SourceIds; factors = Factors; return PairStatus; }
            public int GetNodalLoadCount(int id) => AssignmentCount;
            public int GetConcForceCount(int id) => AssignmentCount;
            public int GetConcMomentCount(int id) => 0;
            public int GetUDLLoadCount(int id) => 0;
            public int GetUNIMomentCount(int id) => 0;
            public int GetLinearVaryingLoadCount(int id) => 0;
            public int GetTrapLoadCount(int id) => 0;
            public int GetNodalLoads(int id, ref object fx, ref object fy, ref object fz, ref object mx, ref object my, ref object mz)
            { fx = new[] { 1.0, 1.0 }; fy = new[] { -2.0, -2.0 }; fz = new[] { 3.0, 3.0 }; mx = new[] { -4.0, -4.0 }; my = new[] { 5.0, 5.0 }; mz = new[] { -6.0, -6.0 }; return 0; }
            public int GetConcForces(int id, ref object direction, ref object force, ref object position, ref object offset)
            { direction = new[] { 5, 5 }; force = new[] { -7.25, -7.25 }; position = new[] { 1.5, 1.5 }; offset = new[] { -.125, -.125 }; return 0; }
        }
        [Test]
        public void CombinationSnapshotPairsSourcesWithExactFactorsWithoutActivation()
        {
            var root = new Root(); using (var wrapper = new OpenStaadWrapper(root))
            {
                var result = wrapper.Load.GetLoadCombination(201);
                Assert.That(result.Id, Is.EqualTo(201)); Assert.That(result.Title, Is.EqualTo("TITLE 201"));
                Assert.That(result.Factors.Single(p => p.Key.Id == 41).Value, Is.EqualTo(-.75));
                Assert.That(result.Factors.Single(p => p.Key.Id == 17).Value, Is.EqualTo(1.123456789123));
                Assert.That(result.Factors.Keys.All(c => c.CaseType == LoadCaseType.PrimaryLoad && c.Type == LoadType.Dead), Is.True);
                Assert.That(root.Load.Active, Is.Zero);
                var other = wrapper.Load.GetLoadCombination(201);
                Assert.That(other.Factors, Is.Not.SameAs(result.Factors));
                Assert.That(other.Factors.Keys.Single(c => c.Id == 17), Is.Not.SameAs(result.Factors.Keys.Single(c => c.Id == 17)));
            }
        }
        [TestCase("false-status")]
        [TestCase("short-array")]
        [TestCase("duplicate-id")]
        [TestCase("missing-id")]
        [TestCase("nan")]
        [TestCase("infinity")]
        [TestCase("negative-count")]
        public void CombinationMalformedReadsThrowInsteadOfReturningPartialData(string kind)
        {
            var root = new Root(); var n = root.Load;
            if (kind == "false-status") n.PairStatus = false;
            if (kind == "short-array") n.Factors = new[] { 1.0 };
            if (kind == "duplicate-id") n.SourceIds = new[] { 17, 17 };
            if (kind == "missing-id") n.SourceIds = new[] { 17, 99 };
            if (kind == "nan") n.Factors = new[] { 1.0, double.NaN };
            if (kind == "infinity") n.Factors = new[] { 1.0, double.PositiveInfinity };
            if (kind == "negative-count") n.PairCount = -1;
            using (var wrapper = new OpenStaadWrapper(root)) Assert.Throws<InvalidOperationException>(() => wrapper.Load.GetLoadCombination(201));
        }
        [Test]
        public void CombinationZeroPairsReturnsNullAndSrssOverallFactorIsRejected()
        {
            var root = new Root(); using (var wrapper = new OpenStaadWrapper(root))
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.GetLoadCombination(0));
                root.Load.PairCount = 0; Assert.That(wrapper.Load.GetLoadCombination(201), Is.Null); Assert.That(root.Load.PairFetches, Is.Zero);
                root.Load.PairCount = 2; root.Load.Factors = new[] { 1.0, -2.0, 1.5 };
                Assert.Throws<NotSupportedException>(() => wrapper.Load.GetLoadCombination(201));
                root.Load.Factors = new[] { 1.0, -2.0, double.NaN };
                Assert.That(wrapper.Load.GetLoadCombination(201).Factors.Count, Is.EqualTo(2));
            }
        }
        [Test]
        public void NodalGroupingRefreshesListsAndPreservesMultiplicityWithIndependentClones()
        {
            var root = new Root(); using (var wrapper = new OpenStaadWrapper(root))
            {
                var a = new Node(10, 0, 0, 0); var b = new Node(20, 1, 0, 0); var lc = new LoadCase(17);
                a.NodalLoadItems.Add(new NodalLoad(99, 0, 0, 0, 0, 0));
                var nodes = new Once<Node>(new[] { a, b }); var cases = new Once<ILoadCase>(new[] { lc });
                var result = wrapper.Load.GetNodalLoadsGroupedByLoadCase(nodes, cases);
                Assert.That(nodes.Count, Is.EqualTo(1)); Assert.That(cases.Count, Is.EqualTo(1));
                Assert.That(a.NodalLoadItems.Count, Is.EqualTo(2)); Assert.That(a.NodalLoadItems[0].Forces.Fx, Is.EqualTo(1));
                Assert.That(result[lc].Count, Is.EqualTo(2));
                Assert.That(result[lc].All(g => g.Nodes.Select(n => n.Id).SequenceEqual(new[] { 10, 20 })), Is.True);
                Assert.That(result[lc][0].Load, Is.Not.SameAs(result[lc][1].Load));
                Assert.That(result[lc][0].Load.Forces, Is.Not.SameAs(result[lc][1].Load.Forces));
                Assert.That(result[lc].All(g => ReferenceEquals(g.Load.LoadCase, lc)), Is.True);
                Assert.That(root.Load.Active, Is.EqualTo(17));
            }
        }
        [Test]
        public void MemberGroupingKeepsRuntimeDefinitionsAndSplitsDuplicateBeamAssignments()
        {
            var root = new Root(); using (var wrapper = new OpenStaadWrapper(root))
            {
                var a = new Beam(101, new Node(1, 0, 0, 0), new Node(2, 1, 0, 0));
                var b = new Beam(102, new Node(3, 0, 0, 0), new Node(4, 1, 0, 0));
                var lc = new LoadCase(41, "REF", LoadCaseType.ReferenceLoad);
                a.MemberLoadItems.Add(new MemberConcentratedLoad(LoadDirection.GlobalX, 99, 1));
                var result = wrapper.Load.GetMemberLoadsGroupedByLoadCase(new Once<Beam>(new[] { a, b }), new Once<ILoadCase>(new[] { lc }));
                Assert.That(a.MemberLoadItems.Count, Is.EqualTo(2)); Assert.That(result[lc].Count, Is.EqualTo(2));
                Assert.That(result[lc].All(g => g.Load is MemberConcentratedLoad && g.Beams.Select(e => e.Id).SequenceEqual(new[] { 101, 102 })), Is.True);
                Assert.That(result[lc][0].Load, Is.Not.SameAs(result[lc][1].Load));
                Assert.That(result[lc].All(g => ReferenceEquals(g.Load.LoadCase, lc) && g.Load.Members.Count == 0), Is.True);
            }
        }
        [Test]
        public void GroupingValidatesBeforeClearingAndEmptyCasesClearWithoutNativeCalls()
        {
            var root = new Root(); using (var wrapper = new OpenStaadWrapper(root))
            {
                var node = new Node(10, 0, 0, 0); var old = new NodalLoad(); node.NodalLoadItems.Add(old);
                Assert.Throws<NotSupportedException>(() => wrapper.Load.GetNodalLoadsGroupedByLoadCase(new[] { node }, new[] { new LoadCase(17, "BAD", LoadCaseType.LoadCombination) }));
                Assert.That(node.NodalLoadItems.Single(), Is.SameAs(old));
                Assert.Throws<ArgumentException>(() => wrapper.Load.GetNodalLoadsGroupedByLoadCase(new[] { node, null }, new[] { new LoadCase(17) }));
                Assert.That(node.NodalLoadItems.Single(), Is.SameAs(old));
                Assert.That(wrapper.Load.GetNodalLoadsGroupedByLoadCase(new[] { node }, new ILoadCase[0]), Is.Empty);
                Assert.That(node.NodalLoadItems, Is.Empty); Assert.That(root.Load.Active, Is.Zero);
            }
        }
        private class Once<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> _items; public int Count;
            public Once(IEnumerable<T> items) { _items = items; }
            public IEnumerator<T> GetEnumerator() { if (++Count > 1) throw new InvalidOperationException(); return _items.GetEnumerator(); }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
