using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSLoadCaseFilterTests
    {
        private static int[] Ids(LoadCaseType type) => type == LoadCaseType.PrimaryLoad ? new[] { 41, 17 } : type == LoadCaseType.ReferenceLoad ? new[] { 41, 101 } : new[] { 201, 301 };
        private static IEnumerable<ILoadCase> Read(IOSLoad load, string query, LoadCaseType type)
        {
            if (query == "range") return load.GetLoadCases(1, int.MaxValue, type);
            if (query == "ids") return load.GetLoadCasesFromIds(Ids(type), type);
            return load.GetLoadCasesOfType(type);
        }

        [TestCase("range", LoadCaseType.PrimaryLoad)]
        [TestCase("range", LoadCaseType.ReferenceLoad)]
        [TestCase("range", LoadCaseType.LoadCombination)]
        [TestCase("ids", LoadCaseType.PrimaryLoad)]
        [TestCase("ids", LoadCaseType.ReferenceLoad)]
        [TestCase("ids", LoadCaseType.LoadCombination)]
        [TestCase("list", LoadCaseType.PrimaryLoad)]
        [TestCase("list", LoadCaseType.ReferenceLoad)]
        [TestCase("list", LoadCaseType.LoadCombination)]
        public void Queries_AreEagerFreshListsWithIndependentMetadata(string query, LoadCaseType type)
        {
            var raw = new OSLoadCaseSelectionTests.SelectionLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var result = Read(wrapper.Load, query, type);
                Assert.That(result, Is.TypeOf<List<ILoadCase>>());
                int nativeCalls = raw.Calls.Count;
                Assert.That(nativeCalls, Is.EqualTo(type == LoadCaseType.LoadCombination ? 4 : 6));
                Assert.That(result.Select(c => c.Id), Is.EquivalentTo(Ids(type)));
                Assert.That(result.All(c => c.CaseType == type), Is.True);
                result.ToList();
                Assert.That(raw.Calls.Count, Is.EqualTo(nativeCalls));
                var again = Read(wrapper.Load, query, type).ToList();
                Assert.That(again, Is.Not.SameAs(result));
                foreach (var item in again) Assert.That(item, Is.Not.SameAs(result.Single(c => c.Id == item.Id)));
                ((LoadCase)result.First()).Title = "edited";
                Assert.That(again.Any(c => c.Title == "edited"), Is.False);
                ((List<ILoadCase>)result).Clear();
                Assert.That(again.Count, Is.EqualTo(2));
            }
        }

        [TestCase("range")]
        [TestCase("ids")]
        [TestCase("list")]
        public void Queries_RejectUnsupportedFamiliesBeforeCom(string query)
        {
            var raw = new OSLoadCaseSelectionTests.SelectionLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                foreach (LoadCaseType type in new[] { LoadCaseType.RepeatLoad, (LoadCaseType)0, (LoadCaseType)(-1), (LoadCaseType)int.MaxValue })
                    Assert.Throws<NotSupportedException>(() => Read(wrapper.Load, query, type));
            Assert.That(raw.Calls, Is.Empty);
        }

        [TestCase("range", LoadCaseType.PrimaryLoad)]
        [TestCase("range", LoadCaseType.ReferenceLoad)]
        [TestCase("range", LoadCaseType.LoadCombination)]
        [TestCase("ids", LoadCaseType.PrimaryLoad)]
        [TestCase("ids", LoadCaseType.ReferenceLoad)]
        [TestCase("ids", LoadCaseType.LoadCombination)]
        [TestCase("list", LoadCaseType.PrimaryLoad)]
        [TestCase("list", LoadCaseType.ReferenceLoad)]
        [TestCase("list", LoadCaseType.LoadCombination)]
        public void Queries_ZeroNativeCountReturnsFreshEmptyLists(string query, LoadCaseType type)
        {
            var raw = new OSLoadCaseSelectionTests.SelectionLoad { Count = 0 };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var first = Read(wrapper.Load, query, type);
                var second = Read(wrapper.Load, query, type);
                Assert.That(first, Is.Empty);
                Assert.That(second, Is.Empty.And.Not.SameAs(first));
            }
            Assert.That(raw.Calls.Count, Is.EqualTo(2));
            Assert.That(raw.Calls.All(c => c.EndsWith(":count")), Is.True);
        }

        [TestCase(LoadCaseType.PrimaryLoad)]
        [TestCase(LoadCaseType.ReferenceLoad)]
        [TestCase(LoadCaseType.LoadCombination)]
        public void Range_IsInclusiveAndEqualBoundsSelectOneExistingId(LoadCaseType type)
        {
            int low = Ids(type).Min(), high = Ids(type).Max();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(new OSLoadCaseSelectionTests.SelectionLoad())))
            {
                Assert.That(wrapper.Load.GetLoadCases(low, high, type).Select(c => c.Id), Is.EquivalentTo(Ids(type)));
                Assert.That(wrapper.Load.GetLoadCases(low, low, type).Select(c => c.Id), Is.EqualTo(new[] { low }));
                Assert.That(wrapper.Load.GetLoadCases(low + 1, high, type).Select(c => c.Id), Is.EqualTo(new[] { high }));
                Assert.That(wrapper.Load.GetLoadCases(high + 1, int.MaxValue, type), Is.Empty);
            }
        }

        [Test]
        public void Range_HandlesInt32LimitsWithoutEnumeratingEveryInteger()
        {
            var raw = new OSLoadCaseSelectionTests.SelectionLoad { PrimaryIds = new[] { 1, int.MaxValue } };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                Assert.That(wrapper.Load.GetLoadCases(1, int.MaxValue, LoadCaseType.PrimaryLoad).Select(c => c.Id), Is.EquivalentTo(new[] { 1, int.MaxValue }));
                Assert.That(wrapper.Load.GetLoadCases(int.MaxValue, int.MaxValue, LoadCaseType.PrimaryLoad).Single().Id, Is.EqualTo(int.MaxValue));
            }
        }

        [Test]
        public void Range_RejectsNonpositiveAndReversedBoundsBeforeCom()
        {
            var raw = new OSLoadCaseSelectionTests.SelectionLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                foreach (int id in new[] { 0, -1, int.MinValue })
                {
                    Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.GetLoadCases(id, 41, LoadCaseType.PrimaryLoad)).ParamName, Is.EqualTo("fromId"));
                    Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.GetLoadCases(17, id, LoadCaseType.PrimaryLoad)).ParamName, Is.EqualTo("toId"));
                }
                Assert.That(Assert.Throws<ArgumentException>(() => wrapper.Load.GetLoadCases(41, 17, LoadCaseType.PrimaryLoad)).ParamName, Is.EqualTo("toId"));
            }
            Assert.That(raw.Calls, Is.Empty);
        }

        [TestCase(LoadCaseType.PrimaryLoad)]
        [TestCase(LoadCaseType.ReferenceLoad)]
        [TestCase(LoadCaseType.LoadCombination)]
        public void IdFilter_DeduplicatesRequestsOmitsMissingAndKeepsSelectedFamily(LoadCaseType type)
        {
            int present = Ids(type)[1];
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(new OSLoadCaseSelectionTests.SelectionLoad())))
            {
                var result = wrapper.Load.GetLoadCasesFromIds(new[] { int.MaxValue, present, present }, type);
                Assert.That(result.Select(c => c.Id), Is.EqualTo(new[] { present }));
                Assert.That(result.Single().CaseType, Is.EqualTo(type));
                Assert.That(wrapper.Load.GetLoadCasesFromIds(new[] { int.MaxValue }, type), Is.Empty);
            }
        }

        [Test]
        public void IdFilter_EmptyInputSkipsComButStillRejectsUnsupportedFamilies()
        {
            var raw = new OSLoadCaseSelectionTests.SelectionLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var first = wrapper.Load.GetLoadCasesFromIds(new int[0], LoadCaseType.PrimaryLoad);
                var second = wrapper.Load.GetLoadCasesFromIds(Enumerable.Empty<int>(), LoadCaseType.PrimaryLoad);
                Assert.That(first, Is.Empty);
                Assert.That(second, Is.Empty.And.Not.SameAs(first));
                Assert.Throws<NotSupportedException>(() => wrapper.Load.GetLoadCasesFromIds(new int[0], LoadCaseType.RepeatLoad));
                Assert.Throws<NotSupportedException>(() => wrapper.Load.GetLoadCasesFromIds(new int[0], (LoadCaseType)0));
            }
            Assert.That(raw.Calls, Is.Empty);
        }

        [Test]
        public void IdFilter_RejectsNullAndEveryInvalidIdBeforeCom()
        {
            var raw = new OSLoadCaseSelectionTests.SelectionLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                Assert.That(Assert.Throws<ArgumentNullException>(() => wrapper.Load.GetLoadCasesFromIds(null, LoadCaseType.PrimaryLoad)).ParamName, Is.EqualTo("loadCasesIds"));
                foreach (int id in new[] { 0, -1, int.MinValue })
                    Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.GetLoadCasesFromIds(new[] { 41, id }, LoadCaseType.PrimaryLoad)).ParamName, Is.EqualTo("loadCasesIds"));
            }
            Assert.That(raw.Calls, Is.Empty);
        }

        private sealed class SinglePassIds : IEnumerable<int>
        {
            private readonly IEnumerable<int> source;
            public int Enumerations { get; private set; }
            public SinglePassIds(IEnumerable<int> source) { this.source = source; }
            public IEnumerator<int> GetEnumerator()
            {
                if (++Enumerations != 1) throw new InvalidOperationException("Enumerated twice.");
                return source.GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        private static IEnumerable<int> FailingIds(Exception failure, Action disposed)
        {
            try { yield return 41; throw failure; }
            finally { disposed(); }
        }

        [Test]
        public void IdFilter_SnapshotsInputOnceAndPropagatesDisposedEnumeratorFailureBeforeCom()
        {
            var raw = new OSLoadCaseSelectionTests.SelectionLoad();
            var backing = new List<int> { 41, 17, 41 };
            var ids = new SinglePassIds(backing);
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var result = wrapper.Load.GetLoadCasesFromIds(ids, LoadCaseType.PrimaryLoad);
                backing.Clear();
                Assert.That(result.Select(c => c.Id), Is.EquivalentTo(new[] { 41, 17 }));
                Assert.That(ids.Enumerations, Is.EqualTo(1));
                raw.Calls.Clear();
                var failure = new ApplicationException("input failed");
                bool disposed = false;
                Assert.That(Assert.Throws<ApplicationException>(() => wrapper.Load.GetLoadCasesFromIds(FailingIds(failure, () => disposed = true), LoadCaseType.PrimaryLoad)), Is.SameAs(failure));
                Assert.That(disposed, Is.True);
                Assert.That(raw.Calls, Is.Empty);
            }
        }

        [TestCase("range")]
        [TestCase("ids")]
        [TestCase("list")]
        public void Queries_PropagateMalformedNativeAndMissingSignatureFailures(string query)
        {
            foreach (var raw in new[] { new OSLoadCaseSelectionTests.SelectionLoad { Count = -1 }, new OSLoadCaseSelectionTests.SelectionLoad { NumberCount = 0 }, new OSLoadCaseSelectionTests.SelectionLoad { InvalidIds = true }, new OSLoadCaseSelectionTests.SelectionLoad { InvalidTitle = true }, new OSLoadCaseSelectionTests.SelectionLoad { InvalidType = true } })
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, query, LoadCaseType.PrimaryLoad));
            var failing = new OSLoadCaseSelectionTests.SelectionLoad { ThrowAt = "count" };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(failing)))
                Assert.That(Assert.Throws<COMException>(() => Read(wrapper.Load, query, LoadCaseType.PrimaryLoad)), Is.SameAs(failing.Failure));
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(new object())))
                Assert.Throws<RuntimeBinderException>(() => Read(wrapper.Load, query, LoadCaseType.PrimaryLoad));
        }

        [TestCase("range")]
        [TestCase("ids")]
        public void Filters_StillReadAndValidateUnmatchedMetadata(string query)
        {
            var raw = new OSLoadCaseSelectionTests.SelectionLoad { ThrowAt = "title", FailureId = 41 };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                if (query == "range") Assert.That(Assert.Throws<COMException>(() => wrapper.Load.GetLoadCases(17, 17, LoadCaseType.PrimaryLoad)), Is.SameAs(raw.Failure));
                else Assert.That(Assert.Throws<COMException>(() => wrapper.Load.GetLoadCasesFromIds(new[] { 17 }, LoadCaseType.PrimaryLoad)), Is.SameAs(raw.Failure));
        }

        [Test]
        public void ConcreteAdapter_ExposesAllThreeFilters()
        {
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(new OSLoadCaseSelectionTests.SelectionLoad())))
            {
                var adapter = new OSLoadAdapter(wrapper);
                Assert.That(adapter.GetLoadCases(41, 41, LoadCaseType.PrimaryLoad).Single().Id, Is.EqualTo(41));
                Assert.That(adapter.GetLoadCasesFromIds(new[] { 101 }, LoadCaseType.ReferenceLoad).Single().Id, Is.EqualTo(101));
                Assert.That(adapter.GetLoadCasesOfType(LoadCaseType.LoadCombination).Count, Is.EqualTo(2));
            }
        }
    }
}
