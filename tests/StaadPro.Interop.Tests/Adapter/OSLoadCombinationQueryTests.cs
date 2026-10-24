using System;
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
    public class OSLoadCombinationQueryTests
    {
        // Deliberately omits activation and GetLoadType: combination metadata needs neither.
        public class CombinationLoad
        {
            public object Count { get; set; } = 2;
            public object NumberCount { get; set; } = 2;
            public object Ids { get; set; } = new[] { 41, 201 };
            public bool InPlace { get; set; }
            public object Title { get; set; } = "  雪 COMB  ";
            public string ThrowAt { get; set; }
            public int FailureId { get; set; } = 201;
            public Exception Failure { get; set; } = new COMException("combination read failed");
            public List<string> Calls { get; } = new List<string>();
            private void Record(string stage, int id = 0)
            {
                Calls.Add(stage + (id == 0 ? "" : ":" + id));
                if (ThrowAt == stage && (stage != "title" || id == FailureId)) throw Failure;
            }
            public object GetLoadCombinationCaseCount() { Record("count"); return Count; }
            public object GetLoadCombinationCaseNumbers(ref object ids)
            {
                Record("numbers");
                if (InPlace)
                {
                    Assert.That(ids, Is.TypeOf<int[]>());
                    Assert.That(((int[])ids).Length, Is.EqualTo(Convert.ToInt32(Count)));
                    Array.Copy((int[])Ids, (int[])ids, ((int[])Ids).Length);
                }
                else ids = Ids;
                return NumberCount;
            }
            public object GetLoadCaseTitle(int id) { Record("title", id); return Title; }
        }

        public class CountOnlyLoad
        {
            public int GetLoadCombinationCaseCount() => 2;
        }
        public class NumbersOnlyLoad
        {
            public int GetLoadCombinationCaseCount() => 1;
            public int GetLoadCombinationCaseNumbers(ref object ids) { ids = new[] { 201 }; return 1; }
        }
        private static IEnumerable<ILoadCase> Read(IOSLoad load, bool single) => single
            ? new[] { load.GetLoadCombinationCaseFromId(201) } : load.GetAllLoadCombinationCases();

        [TestCase(false)]
        [TestCase(true)]
        public void Enumeration_MapsMetadataAndReturnsIndependentObjects(bool inPlace)
        {
            var raw = new CombinationLoad { InPlace = inPlace };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var result = wrapper.Load.GetAllLoadCombinationCases();
                Assert.That(result.Select(c => c.Id), Is.EquivalentTo(new[] { 41, 201 }));
                Assert.That(result.All(c => c is LoadCase && c.CaseType == LoadCaseType.LoadCombination && c.Type == LoadType.None && c.Title == (string)raw.Title), Is.True);
                Assert.That(raw.Calls, Is.EqualTo(new[] { "count", "numbers", "title:41", "title:201" }));
                var again = wrapper.Load.GetAllLoadCombinationCases();
                Assert.That(again, Is.EqualTo(result).And.Not.SameAs(result));
                foreach (var item in again) Assert.That(item, Is.Not.SameAs(result.Single(c => c.Id == item.Id)));
                ((LoadCase)result.Single(c => c.Id == 41)).Title = "edited";
                Assert.That(again.Single(c => c.Id == 41).Title, Is.EqualTo(raw.Title));
                ((int[])raw.Ids)[0] = 99;
                Assert.That(result.Select(c => c.Id), Is.EquivalentTo(new[] { 41, 201 }));
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void Lookup_ValidatesMembershipAndReadsOnlyRequestedTitle(bool inPlace)
        {
            var raw = new CombinationLoad { InPlace = inPlace };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var result = wrapper.Load.GetLoadCombinationCaseFromId(201);
                Assert.That(result.Id, Is.EqualTo(201));
                Assert.That(result.CaseType, Is.EqualTo(LoadCaseType.LoadCombination));
                Assert.That(result.Type, Is.EqualTo(LoadType.None));
                Assert.That(result.Title, Is.EqualTo(raw.Title));
                Assert.That(raw.Calls, Is.EqualTo(new[] { "count", "numbers", "title:201" }));
                var again = wrapper.Load.GetLoadCombinationCaseFromId(201);
                Assert.That(again, Is.EqualTo(result).And.Not.SameAs(result));
                ((LoadCase)result).Title = "edited";
                Assert.That(again.Title, Is.EqualTo(raw.Title));
            }
        }

        [Test]
        public void Lookup_RejectsInvalidIdsBeforeCom()
        {
            var raw = new CombinationLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                foreach (int id in new[] { 0, -1, int.MinValue })
                    Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.GetLoadCombinationCaseFromId(id)).ParamName, Is.EqualTo("lcId"));
            Assert.That(raw.Calls, Is.Empty);
        }

        [Test]
        public void Lookup_RejectsAbsentOrPrimaryOnlyIdWithoutReadingTitle()
        {
            var raw = new CombinationLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.GetLoadCombinationCaseFromId(17));
            Assert.That(raw.Calls, Is.EqualTo(new[] { "count", "numbers" }));
        }

        [Test]
        public void Enumeration_ZeroCountReturnsFreshEmptySetsWithoutFetching()
        {
            var raw = new CombinationLoad { Count = 0 };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var first = wrapper.Load.GetAllLoadCombinationCases();
                var second = wrapper.Load.GetAllLoadCombinationCases();
                Assert.That(first, Is.Empty);
                Assert.That(second, Is.Empty.And.Not.SameAs(first));
            }
            Assert.That(raw.Calls, Is.EqualTo(new[] { "count", "count" }));
        }

        [Test]
        public void Lookup_ZeroCountThrowsWithoutNumberOrTitleAccess()
        {
            var raw = new CombinationLoad { Count = 0 };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.GetLoadCombinationCaseFromId(201));
            Assert.That(raw.Calls, Is.EqualTo(new[] { "count" }));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void Queries_RejectMalformedCountsBeforeFetching(bool single)
        {
            foreach (object count in new object[] { -1, -106, -114, null, true, "2", 2.0, (long)int.MaxValue + 1 })
            {
                var raw = new CombinationLoad { Count = count };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, single));
                Assert.That(raw.Calls, Is.EqualTo(new[] { "count" }));
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void Queries_RejectFailureOrMismatchingIntegralCount(bool single)
        {
            foreach (object count in new object[] { 0, 1, 3, -1, -106, -114, null, false, "2", 2.0, long.MaxValue })
            {
                var raw = new CombinationLoad { NumberCount = count };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, single));
                Assert.That(raw.Calls, Is.EqualTo(new[] { "count", "numbers" }));
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void Queries_RejectMalformedDuplicateOrUnwrittenIdArraysBeforeTitles(bool single)
        {
            foreach (object ids in new object[] { null, new int[0], new[] { 201 }, new[] { 201, 41, 7 }, new long[] { 201, 41 }, new object[] { 201, 41 }, new int[1, 2], new[] { 201, 0 }, new[] { 201, -1 }, new[] { 201, 201 }, new int[2] })
            {
                var raw = new CombinationLoad { Ids = ids };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, single));
                Assert.That(raw.Calls, Is.EqualTo(new[] { "count", "numbers" }));
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void Queries_AcceptNonzeroArrayBoundsAndIntegerCountRepresentations(bool single)
        {
            var ids = Array.CreateInstance(typeof(int), new[] { 2 }, new[] { -3 });
            ids.SetValue(41, -3); ids.SetValue(201, -2);
            var raw = new CombinationLoad { Count = (short)2, NumberCount = 2L, Ids = ids };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.That(Read(wrapper.Load, single).Select(c => c.Id), Is.EquivalentTo(single ? new[] { 201 } : new[] { 41, 201 }));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void Queries_PreserveStringTitlesAndRejectOtherTitleValues(bool single)
        {
            foreach (object title in new object[] { "", "  ", "NONE", "雪\r\nComb", null, 1, false })
            {
                var raw = new CombinationLoad { Title = title };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                {
                    if (title is string value) Assert.That(Read(wrapper.Load, single).All(c => c.Title == value), Is.True);
                    else Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, single));
                }
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void Queries_PropagateNativeFailuresAndStopAtFailingStage(bool single)
        {
            foreach (string stage in new[] { "count", "numbers", "title" })
            {
                var raw = new CombinationLoad { ThrowAt = stage, FailureId = single ? 201 : 41 };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.That(Assert.Throws<COMException>(() => Read(wrapper.Load, single)), Is.SameAs(raw.Failure));
                Assert.That(raw.Calls.Count, Is.EqualTo(stage == "count" ? 1 : stage == "numbers" ? 2 : 3));
            }
        }

        [Test]
        public void Enumeration_LaterTitleFailureDoesNotReturnPartialSet()
        {
            var raw = new CombinationLoad { ThrowAt = "title" };
            HashSet<ILoadCase> result = null;
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.That(Assert.Throws<COMException>(() => result = wrapper.Load.GetAllLoadCombinationCases()), Is.SameAs(raw.Failure));
            Assert.That(result, Is.Null);
            Assert.That(raw.Calls, Is.EqualTo(new[] { "count", "numbers", "title:41", "title:201" }));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void Queries_PropagateMissingSignaturesAtEveryStage(bool single)
        {
            foreach (object raw in new object[] { new object(), new CountOnlyLoad(), new NumbersOnlyLoad() })
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.Throws<RuntimeBinderException>(() => Read(wrapper.Load, single));
        }

        [Test]
        public void ConcreteAdapter_ExposesBothCombinationMethods()
        {
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(new CombinationLoad())))
            {
                var adapter = new OSLoadAdapter(wrapper);
                Assert.That(adapter.GetAllLoadCombinationCases().Count, Is.EqualTo(2));
                Assert.That(adapter.GetLoadCombinationCaseFromId(201).CaseType, Is.EqualTo(LoadCaseType.LoadCombination));
            }
        }
    }
}
