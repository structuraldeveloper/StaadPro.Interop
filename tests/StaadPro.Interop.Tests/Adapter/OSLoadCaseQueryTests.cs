using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSLoadCaseQueryTests
    {
        // Public for the production assembly's dynamic binder. Deliberately exposes no activation API.
        public class CaseLoad
        {
            public object Count { get; set; } = 2;
            public object NumberCount { get; set; } = 2;
            public object Ids { get; set; } = new[] { 41, 7 };
            public bool InPlace { get; set; }
            public bool OverrideTitle { get; set; }
            public object Title { get; set; }
            public bool OverrideType { get; set; }
            public object Type { get; set; }
            public string ThrowAt { get; set; }
            public Exception Failure { get; set; } = new COMException("native metadata failure");
            public List<string> Calls { get; } = new List<string>();

            private void Record(string family, string stage, int id = 0)
            {
                Calls.Add(family + ":" + stage + (id == 0 ? "" : ":" + id));
                if (ThrowAt == stage) throw Failure;
            }
            private object Numbers(string family, ref object ids)
            {
                Record(family, "numbers");
                if (InPlace)
                {
                    Assert.That(ids, Is.TypeOf<int[]>());
                    Assert.That(((int[])ids).Length, Is.EqualTo(Convert.ToInt32(Count)));
                    Array.Copy((int[])Ids, (int[])ids, ((int[])Ids).Length);
                }
                else ids = Ids;
                return NumberCount;
            }
            private object ReadTitle(string family, int id)
            {
                Record(family, "title", id);
                return OverrideTitle ? Title : "  " + family + " 雪 " + id + "  ";
            }
            private object ReadType(string family, int id)
            {
                Record(family, "type", id);
                return OverrideType ? Type : family == "primary" ? (int)LoadType.Dead : (int)LoadType.Mass;
            }
            public object GetPrimaryLoadCaseCount() { Record("primary", "count"); return Count; }
            public object GetReferenceLoadCaseCount() { Record("reference", "count"); return Count; }
            public object GetPrimaryLoadCaseNumbers(ref object ids) => Numbers("primary", ref ids);
            public object GetReferenceLoadCaseNumbers(ref object ids) => Numbers("reference", ref ids);
            public object GetLoadCaseTitle(int id) => ReadTitle("primary", id);
            public object GetReferenceLoadCaseTitle(int id) => ReadTitle("reference", id);
            public object GetLoadType(int id) => ReadType("primary", id);
            public object GetReferenceLoadType(int id) => ReadType("reference", id);
        }

        private static HashSet<ILoadCase> Read(IOSLoad load, bool reference) => reference
            ? load.GetAllReferenceLoadCases() : load.GetAllPrimaryLoadCases();
        private static string Family(bool reference) => reference ? "reference" : "primary";

        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        public void Enumeration_PreservesSparseIdsAndMetadataWithoutActivation(bool reference, bool inPlace)
        {
            var raw = new CaseLoad { InPlace = inPlace };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var result = Read(wrapper.Load, reference);
                Assert.That(result.Select(c => c.Id), Is.EquivalentTo(new[] { 41, 7 }));
                foreach (var item in result)
                {
                    Assert.That(item, Is.TypeOf<LoadCase>());
                    Assert.That(item.Title, Is.EqualTo("  " + Family(reference) + " 雪 " + item.Id + "  "));
                    Assert.That(item.Type, Is.EqualTo(reference ? LoadType.Mass : LoadType.Dead));
                    Assert.That(item.CaseType, Is.EqualTo(reference ? LoadCaseType.ReferenceLoad : LoadCaseType.PrimaryLoad));
                }
                Assert.That(raw.Calls, Is.EqualTo(new[] { Family(reference) + ":count", Family(reference) + ":numbers", Family(reference) + ":title:41", Family(reference) + ":type:41", Family(reference) + ":title:7", Family(reference) + ":type:7" }));
                var again = Read(wrapper.Load, reference);
                Assert.That(again, Is.Not.SameAs(result));
                Assert.That(again, Is.EquivalentTo(result));
                foreach (var item in again) Assert.That(item, Is.Not.SameAs(result.Single(c => c.Id == item.Id)));
                ((LoadCase)result.Single(c => c.Id == 41)).Title = "edited";
                Assert.That(again.Single(c => c.Id == 41).Title, Does.Contain("雪"));
                ((int[])raw.Ids)[0] = 99;
                Assert.That(result.Select(c => c.Id), Is.EquivalentTo(new[] { 41, 7 }));
            }
        }

        [Test]
        public void Enumeration_UsesReferenceMetadataWhenPrimaryIdsOverlap()
        {
            var raw = new CaseLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var primary = wrapper.Load.GetAllPrimaryLoadCases();
                var reference = wrapper.Load.GetAllReferenceLoadCases();
                Assert.That(primary.Select(c => c.Id), Is.EquivalentTo(reference.Select(c => c.Id)));
                Assert.That(reference.All(c => c.Type == LoadType.Mass && c.Title.Contains("reference")), Is.True);
                Assert.That(primary.Union(reference).Count(), Is.EqualTo(4));
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void ZeroCount_ReturnsFreshEmptySetAndSkipsFurtherNativeCalls(bool reference)
        {
            var raw = new CaseLoad { Count = 0, ThrowAt = "numbers" };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var first = Read(wrapper.Load, reference);
                var second = Read(wrapper.Load, reference);
                Assert.That(first, Is.Empty);
                Assert.That(second, Is.Empty.And.Not.SameAs(first));
                Assert.That(raw.Calls, Is.EqualTo(new[] { Family(reference) + ":count", Family(reference) + ":count" }));
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void InvalidCounts_StopBeforeNumberRetrieval(bool reference)
        {
            foreach (object count in new object[] { -1, -106, -114, null, true, "2", 2.0, (long)int.MaxValue + 1 })
            {
                var raw = new CaseLoad { Count = count };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, reference));
                Assert.That(raw.Calls, Is.EqualTo(new[] { Family(reference) + ":count" }));
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void NumberRetrieval_RequiresMatchingCountInsteadOfZeroSuccess(bool reference)
        {
            foreach (object status in new object[] { 0, 1, 3, -1, -106, -114, null, true, "2", 2.0, long.MaxValue })
            {
                var raw = new CaseLoad { NumberCount = status };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, reference));
                Assert.That(raw.Calls.Count, Is.EqualTo(2));
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void MalformedOrUnwrittenIdArrays_StopBeforeReadingMetadata(bool reference)
        {
            foreach (object ids in new object[] { null, new int[0], new[] { 1 }, new[] { 1, 2, 3 }, new long[] { 1, 2 }, new object[] { 1, 2 }, new int[1, 2], new[] { 0, 2 }, new[] { 1, -2 }, new[] { 7, 7 }, new int[2] })
            {
                var raw = new CaseLoad { Ids = ids };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, reference));
                Assert.That(raw.Calls.Count, Is.EqualTo(2));
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void Enumeration_AcceptsNonzeroArrayBoundsAndAllDocumentedLoadTypes(bool reference)
        {
            var ids = Array.CreateInstance(typeof(int), new[] { 2 }, new[] { -3 });
            ids.SetValue(41, -3); ids.SetValue(7, -2);
            foreach (LoadType type in Enum.GetValues(typeof(LoadType)))
            {
                var raw = new CaseLoad { Count = (short)2, NumberCount = 2L, Ids = ids, OverrideType = true, Type = (short)type };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                {
                    var result = Read(wrapper.Load, reference);
                    Assert.That(result.Select(c => c.Id), Is.EquivalentTo(new[] { 41, 7 }));
                    Assert.That(result.All(c => c.Type == type), Is.True);
                }
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void Titles_ArePreservedAndNonStringsAreRejected(bool reference)
        {
            foreach (object title in new object[] { "", "  ", "雪\r\nLoad", "NONE", null, 1, false })
            {
                var raw = new CaseLoad { OverrideTitle = true, Title = title };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                {
                    if (title is string value) Assert.That(Read(wrapper.Load, reference).All(c => c.Title == value), Is.True);
                    else
                    {
                        Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, reference));
                        Assert.That(raw.Calls.Count, Is.EqualTo(3));
                    }
                }
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void InvalidTypeResults_ThrowInsteadOfDefaultingToNone(bool reference)
        {
            foreach (object type in new object[] { -1, 24, 100, null, true, "0", 0.0, long.MaxValue })
            {
                var raw = new CaseLoad { OverrideType = true, Type = type };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, reference));
                Assert.That(raw.Calls.Count, Is.EqualTo(4));
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void NativeFailures_PropagateUnchangedAndStopAtEachStage(bool reference)
        {
            var stages = new[] { "count", "numbers", "title", "type" };
            for (int index = 0; index < stages.Length; index++)
            {
                var raw = new CaseLoad { ThrowAt = stages[index] };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.That(Assert.Throws<COMException>(() => Read(wrapper.Load, reference)), Is.SameAs(raw.Failure));
                Assert.That(raw.Calls.Count, Is.EqualTo(index + 1));
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void MissingNativeInterface_PropagatesBindingFailure(bool reference)
        {
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(new object())))
                Assert.Throws<RuntimeBinderException>(() => Read(wrapper.Load, reference));
        }
    }
}
