using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using NUnit.Framework;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Adapter
{
    public class SelfWeightCombinationTests
    {
        public class Root { public object Geometry { get; } = new object(); public Native Load { get; } = new Native(); }
        public class Native
        {
            public List<string> Calls = new List<string>();
            public int[] Ids;
            public int Direction;
            public double Factor;
            public int Thread;
            public bool Activate = true;
            public List<(int Combination, int Case, double Factor)> Pairs = new List<(int, int, double)>();
            public List<(int Id, string Title)> Combinations = new List<(int, string)>();
            public int FailCase;
            public int EnvelopeId;
            public int[] EnvelopeMembers;
            public bool CreateLoadEnvelop(int id, int type, int[] ids) { EnvelopeId = id; EnvelopeMembers = ids; Calls.Add("envelope"); return Activate; }
            public bool SetLoadActive(int id) { Calls.Add("primary:" + id); return Activate; }
            public int SetReferenceLoadActive(int id) { Calls.Add("reference:" + id); return Activate ? id : 0; }
            public bool AddSelfWeightInXYZToGeometry(int[] ids, int direction, double factor)
            { Calls.Add("selfweight"); Ids = ids; Direction = direction; Factor = factor; Thread = System.Threading.Thread.CurrentThread.ManagedThreadId; return true; }
            public void CreateNewLoadCombination(string title, int id) { Calls.Add("combination:" + id); Combinations.Add((id, title)); }
            public void AddLoadAndFactorToCombination(int combinationId, int caseId, double factor)
            { Calls.Add("factor:" + caseId); if (caseId == FailCase) throw new COMException("native factor failure"); Pairs.Add((combinationId, caseId, factor)); }
        }
        [TestCase(LoadCaseType.PrimaryLoad)]
        [TestCase(LoadCaseType.ReferenceLoad)]
        public void EntitySelfWeightChecksActivationAndPreservesNativeArguments(LoadCaseType type)
        {
            var root = new Root();
            using (var wrapper = new OpenStaadWrapper(root))
            {
                var input = new Once<int>(new[] { 103, 101, 103 });
                Assert.That(wrapper.Load.AddSelfweightInXYZTo(17, type, input, SelfWeightDirection.GlobalZ, -.125), Is.SameAs(wrapper.Load));
                Assert.That(root.Load.Calls, Is.EqualTo(new[] { (type == LoadCaseType.PrimaryLoad ? "primary:" : "reference:") + "17", "selfweight" }));
                Assert.That(root.Load.Ids, Is.EqualTo(new[] { 103, 101, 103 }));
                Assert.That(root.Load.Direction, Is.EqualTo(3));
                Assert.That(root.Load.Factor, Is.EqualTo(-.125));
                Assert.That(input.Count, Is.EqualTo(1));
                root.Load.Calls.Clear(); root.Load.Activate = false;
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.AddSelfweightInXYZTo(17, type, new[] { 101 }));
                Assert.That(root.Load.Calls.Count, Is.EqualTo(1));
            }
        }
        [Test]
        public void ThreadSignatureScopesEntitiesAndUsesDefaultDirectionAndFactorOnCallerThread()
        {
            var root = new Root(); using (var wrapper = new OpenStaadWrapper(root))
            {
                wrapper.Load.AddSelfweightInXYZTo(17, new[] { 101, 102 }, 4);
                Assert.That(root.Load.Ids, Is.EqualTo(new[] { 101, 102 }));
                Assert.That(root.Load.Direction, Is.EqualTo(2)); Assert.That(root.Load.Factor, Is.EqualTo(-1));
                Assert.That(root.Load.Thread, Is.EqualTo(Thread.CurrentThread.ManagedThreadId));
                Assert.That(root.Load.Calls.Count(c => c == "selfweight"), Is.EqualTo(1));
                wrapper.Load.AddSelfweightInXYZTo(17, LoadCaseType.PrimaryLoad, new[] { 101 }, factor: 0);
                Assert.That(root.Load.Factor, Is.Zero);
            }
        }
        [Test]
        public void SelfWeightNoOpsPreserveReferenceNullEmptyAndUnsupportedCaseBehavior()
        {
            var root = new Root(); using (var wrapper = new OpenStaadWrapper(root))
            {
                wrapper.Load.AddSelfweightInXYZTo(17, null, 2);
                wrapper.Load.AddSelfweightInXYZTo(17, LoadCaseType.ReferenceLoad, new int[0]);
                Assert.Throws<NotSupportedException>(() => wrapper.Load.AddSelfweightInXYZTo(17, LoadCaseType.LoadCombination, new[] { 101 }));
                Assert.That(root.Load.Calls, Is.Empty);
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.AddSelfweightInXYZTo(17, new[] { 101 }, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.AddSelfweightInXYZTo(0, LoadCaseType.PrimaryLoad, new[] { 101 }));
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.AddSelfweightInXYZTo(17, LoadCaseType.PrimaryLoad, new[] { 101, 0 }));
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.AddSelfweightInXYZTo(17, LoadCaseType.PrimaryLoad, new[] { 101 }, (SelfWeightDirection)4));
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.AddSelfweightInXYZTo(17, LoadCaseType.PrimaryLoad, new[] { 101 }, factor: double.NaN));
                Assert.That(root.Load.Calls, Is.Empty);
            }
        }
        [Test]
        public void CombinationCreationPreservesFactorsTitlesAndOrderAndSkipsReferenceInvalidDefinitions()
        {
            var root = new Root(); using (var wrapper = new OpenStaadWrapper(root))
            {
                var first = new EditorLoadCombination(201, "TITLE", "17 1.123456789123 41 -0.75");
                var input = new Once<EditorLoadCombination>(new[] { null, new EditorLoadCombination(202, "SKIP", "17 0"), first });
                Assert.That(wrapper.Load.CreateLoadCombinations(input), Is.SameAs(wrapper.Load));
                Assert.That(input.Count, Is.EqualTo(1));
                Assert.That(root.Load.Calls, Is.EqualTo(new[] { "combination:201", "factor:17", "factor:41" }));
                Assert.That(root.Load.Combinations, Is.EqualTo(new[] { (201, "TITLE") }));
                Assert.That(root.Load.Pairs, Is.EqualTo(new[] { (201, 17, 1.123456789123), (201, 41, -.75) }));
                Assert.That(first.CombinationText, Is.EqualTo("17 1.123456789123 41 -0.75"));
            }
        }
        [TestCase("17")]
        [TestCase("17 invalid")]
        [TestCase("17 1 17 2")]
        [TestCase("-17 1")]
        [TestCase("17 NaN")]
        public void InvalidLaterCombinationIsDetectedBeforeAnyMutation(string text)
        {
            var root = new Root(); using (var wrapper = new OpenStaadWrapper(root))
            {
                Assert.Catch(() => wrapper.Load.CreateLoadCombinations(new[] { new EditorLoadCombination(201, "VALID", "17 1"), new EditorLoadCombination(202, "INVALID", text) }));
                Assert.That(root.Load.Calls, Is.Empty);
            }
        }
        [Test]
        public void NativeFactorFailureLeavesCombinationAndEarlierFactorsWithoutProcessingLaterDefinitions()
        {
            var root = new Root(); root.Load.FailCase = 41;
            using (var wrapper = new OpenStaadWrapper(root))
            {
                Assert.Throws<COMException>(() => wrapper.Load.CreateLoadCombinations(new[] { new EditorLoadCombination(201, "FIRST", "17 1 41 -2"), new EditorLoadCombination(202, "SECOND", "17 1") }));
                Assert.That(root.Load.Combinations.Count, Is.EqualTo(1)); Assert.That(root.Load.Pairs.Count, Is.EqualTo(1));
            }
        }
        [Test]
        public void EditorModelRetainsReferenceParsingTitleEqualityAndFreshFactorDictionaries()
        {
            var item = new EditorLoadCombination(201, "LOAD COMB 201 D + L", "17 1.25\t41 -0.75");
            Assert.That(item.GetRepeatLoadTitle(true), Is.EqualTo("LOAD 201 LOADTYPE None D+L"));
            Assert.That(item.SplitCombinationTitle(), Is.EqualTo(new[] { "LOAD COMB", "201", "D + L" }));
            Assert.That(item.Factors, Is.Not.SameAs(item.Factors));
            Assert.That(item.IsValid(), Is.True); Assert.That(EditorLoadCombination.IsValid(null), Is.False);
            Assert.That(item == new EditorLoadCombination(201, "OTHER", item.CombinationText), Is.True);
            Assert.That(EditorLoadCombination.SplitCombinationTitle("OTHER"), Is.Null);
        }
        [Test]
        public void EditorEnvelopeFiltersInvalidDefinitionsAndPreservesValidIdsAndDuplicates()
        {
            var root = new Root(); using (var wrapper = new OpenStaadWrapper(root))
            {
                var input = new Once<EditorLoadCombination>(new[] { null, new EditorLoadCombination(201, "A", "17 1"), new EditorLoadCombination(202, "SKIP", "17 0"), new EditorLoadCombination(201, "A", "17 1") });
                Assert.That(wrapper.Load.CreateLoadEnvelopExt(7, LoadEnvelopType.Strength, input), Is.SameAs(wrapper.Load));
                Assert.That(root.Load.EnvelopeId, Is.EqualTo(7));
                Assert.That(root.Load.EnvelopeMembers, Is.EqualTo(new[] { 201, 201 }));
                Assert.That(input.Count, Is.EqualTo(1));
                Assert.That(root.Load.Calls, Is.EqualTo(new[] { "envelope" }));
            }
        }
        [Test]
        public void EditorEnvelopeRejectsInvalidArgumentsAndNativeFailure()
        {
            var root = new Root(); using (var wrapper = new OpenStaadWrapper(root))
            {
                Assert.Throws<ArgumentNullException>(() => wrapper.Load.CreateLoadEnvelopExt(7, LoadEnvelopType.Strength, (IEnumerable<EditorLoadCombination>)null));
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.CreateLoadEnvelopExt(7, LoadEnvelopType.Strength, new[] { new EditorLoadCombination(-1, "BAD", "17 1") }));
                Assert.That(root.Load.Calls, Is.Empty);
                root.Load.Activate = false;
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.CreateLoadEnvelopExt(7, LoadEnvelopType.Strength, new EditorLoadCombination[0]));
                Assert.That(root.Load.Calls, Is.EqualTo(new[] { "envelope" }));
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
