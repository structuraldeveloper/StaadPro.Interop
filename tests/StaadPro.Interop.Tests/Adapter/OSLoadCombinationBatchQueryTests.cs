using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSLoadCombinationBatchQueryTests
    {
        public class MutatingLoad : OSLoadCombinationQueryTests.CombinationLoad
        {
            public Action OnCount { get; set; }
            public new object GetLoadCombinationCaseCount() { OnCount?.Invoke(); return base.GetLoadCombinationCaseCount(); }
        }
        private sealed class SinglePassIds : IEnumerable<int>
        {
            private readonly IEnumerable<int> source;
            public int Enumerations { get; private set; }
            public SinglePassIds(IEnumerable<int> source) { this.source = source; }
            public IEnumerator<int> GetEnumerator()
            {
                if (++Enumerations != 1) throw new InvalidOperationException("Input enumerated twice.");
                return source.GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Test]
        public void Batch_ReturnsDistinctFreshMetadataAndPerformsOneNumberFetch()
        {
            var raw = new OSLoadCombinationQueryTests.CombinationLoad();
            var input = new[] { 201, 41, 201, 41 };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var result = wrapper.Load.GetLoadCombinationCasesFromIds(input);
                Assert.That(result, Is.InstanceOf<HashSet<ILoadCase>>());
                Assert.That(raw.Calls.Take(2), Is.EqualTo(new[] { "count", "numbers" }));
                Assert.That(raw.Calls.Skip(2), Is.EquivalentTo(new[] { "title:201", "title:41" }));
                var records = result.ToList();
                Assert.That(records.Select(c => c.Id), Is.EquivalentTo(new[] { 201, 41 }));
                Assert.That(records.All(c => c.CaseType == LoadCaseType.LoadCombination && c.Type == LoadType.None && c.Title == (string)raw.Title), Is.True);
                result.ToList();
                Assert.That(raw.Calls.Count, Is.EqualTo(4), "Repeated enumeration must not read native data.");
                input[0] = 99;
                var again = wrapper.Load.GetLoadCombinationCasesFromIds(new[] { 41, 201 });
                Assert.That(again, Is.Not.SameAs(result));
                foreach (var item in again) Assert.That(item, Is.Not.SameAs(records.Single(c => c.Id == item.Id)));
                ((LoadCase)records[0]).Title = "edited";
                Assert.That(again.All(c => c.Title == (string)raw.Title), Is.True);
                ((int[])raw.Ids)[0] = 99;
                Assert.That(records.Select(c => c.Id), Is.EquivalentTo(new[] { 41, 201 }));
            }
        }

        [Test]
        public void Batch_EmptyInputReturnsFreshEmptySetsWithoutCom()
        {
            var raw = new OSLoadCombinationQueryTests.CombinationLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var first = wrapper.Load.GetLoadCombinationCasesFromIds(new int[0]);
                var second = wrapper.Load.GetLoadCombinationCasesFromIds(Enumerable.Empty<int>());
                Assert.That(first, Is.Empty);
                Assert.That(second, Is.Empty.And.Not.SameAs(first));
            }
            Assert.That(raw.Calls, Is.Empty);
        }

        [Test]
        public void Batch_RejectsNullAndAnyNonpositiveIdBeforeCom()
        {
            var raw = new OSLoadCombinationQueryTests.CombinationLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                Assert.That(Assert.Throws<ArgumentNullException>(() => wrapper.Load.GetLoadCombinationCasesFromIds(null)).ParamName, Is.EqualTo("lcIds"));
                foreach (int id in new[] { 0, -1, int.MinValue })
                    Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.GetLoadCombinationCasesFromIds(new[] { 41, 201, id })).ParamName, Is.EqualTo("lcIds"));
            }
            Assert.That(raw.Calls, Is.Empty);
        }

        [Test]
        public void Batch_ValidatesEveryMembershipBeforeAnyTitleRead()
        {
            var raw = new OSLoadCombinationQueryTests.CombinationLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.GetLoadCombinationCasesFromIds(new[] { 41, 201, 17 }));
            Assert.That(raw.Calls, Is.EqualTo(new[] { "count", "numbers" }));
        }

        [Test]
        public void Batch_NonemptyRequestInEmptyModelThrowsWithoutNumberOrTitleReads()
        {
            var raw = new OSLoadCombinationQueryTests.CombinationLoad { Count = 0 };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.GetLoadCombinationCasesFromIds(new[] { 201 }));
            Assert.That(raw.Calls, Is.EqualTo(new[] { "count" }));
        }

        [Test]
        public void Batch_EnumeratesAndSnapshotsInputOnceBeforeCom()
        {
            var backing = new List<int> { 201, 41, 201 };
            var input = new SinglePassIds(backing);
            var raw = new MutatingLoad { OnCount = () => backing.Clear() };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.That(wrapper.Load.GetLoadCombinationCasesFromIds(input).Select(c => c.Id), Is.EquivalentTo(new[] { 201, 41 }));
            Assert.That(input.Enumerations, Is.EqualTo(1));
        }

        private static IEnumerable<int> FailingIds(Exception failure, Action disposed)
        {
            try { yield return 41; throw failure; }
            finally { disposed(); }
        }

        [Test]
        public void Batch_PropagatesEnumeratorFailureBeforeComAndDisposesEnumerator()
        {
            var raw = new OSLoadCombinationQueryTests.CombinationLoad();
            var failure = new ApplicationException("input failure");
            bool disposed = false;
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.That(Assert.Throws<ApplicationException>(() => wrapper.Load.GetLoadCombinationCasesFromIds(FailingIds(failure, () => disposed = true))), Is.SameAs(failure));
            Assert.That(disposed, Is.True);
            Assert.That(raw.Calls, Is.Empty);
        }

        [TestCase("count")]
        [TestCase("numbers")]
        [TestCase("array")]
        [TestCase("title")]
        public void Batch_RejectsMalformedNativeDataWithoutReturningPartialSet(string stage)
        {
            var raw = new OSLoadCombinationQueryTests.CombinationLoad();
            if (stage == "count") raw.Count = -1;
            if (stage == "numbers") raw.NumberCount = 0;
            if (stage == "array") raw.Ids = new[] { 201, 0 };
            if (stage == "title") raw.Title = null;
            IEnumerable<ILoadCase> result = null;
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.Throws<InvalidOperationException>(() => result = wrapper.Load.GetLoadCombinationCasesFromIds(new[] { 201 }));
            Assert.That(result, Is.Null);
            if (stage != "title") Assert.That(raw.Calls.Any(c => c.StartsWith("title:")), Is.False);
        }

        [TestCase("count")]
        [TestCase("numbers")]
        [TestCase("title")]
        public void Batch_PropagatesNativeFailuresWithoutPartialResults(string stage)
        {
            var raw = new OSLoadCombinationQueryTests.CombinationLoad { ThrowAt = stage };
            IEnumerable<ILoadCase> result = null;
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.That(Assert.Throws<COMException>(() => result = wrapper.Load.GetLoadCombinationCasesFromIds(new[] { 41, 201 })), Is.SameAs(raw.Failure));
            Assert.That(result, Is.Null);
            Assert.That(raw.Calls.Last(), Is.EqualTo(stage == "title" ? "title:201" : stage));
        }

        [Test]
        public void Batch_PropagatesMissingNativeSignaturesAtEveryStage()
        {
            foreach (object raw in new object[] { new object(), new OSLoadCombinationQueryTests.CountOnlyLoad(), new OSLoadCombinationQueryTests.NumbersOnlyLoad() })
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.Throws<RuntimeBinderException>(() => wrapper.Load.GetLoadCombinationCasesFromIds(new[] { 201 }));
        }

        [Test]
        public void Batch_SupportsNonzeroArrayBoundsAndIntegerCountRepresentations()
        {
            var ids = Array.CreateInstance(typeof(int), new[] { 2 }, new[] { -3 });
            ids.SetValue(201, -3); ids.SetValue(41, -2);
            var raw = new OSLoadCombinationQueryTests.CombinationLoad { Count = (short)2, NumberCount = 2L, Ids = ids };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.That(wrapper.Load.GetLoadCombinationCasesFromIds(new[] { 41, 201, 41 }).Select(c => c.Id), Is.EquivalentTo(new[] { 201, 41 }));
        }

        [Test]
        public void ConcreteAdapter_ExposesCombinationBatchLookup()
        {
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(new OSLoadCombinationQueryTests.CombinationLoad())))
                Assert.That(new OSLoadAdapter(wrapper).GetLoadCombinationCasesFromIds(new[] { 201, 201 }).Count(), Is.EqualTo(1));
        }
    }
}
