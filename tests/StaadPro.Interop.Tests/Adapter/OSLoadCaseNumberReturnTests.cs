using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSLoadCaseNumberReturnTests
    {
        // Exposes only read APIs. Boolean primary/combination returns match the observed native build.
        public sealed class Native
        {
            public object Count = 2, NumberResult = true, CountAfter;
            public object Ids = new[] { 41, 7 };
            public bool InPlace;
            public int CountCalls;
            public readonly List<string> Calls = new List<string>();
            private object ReadCount(string family)
            {
                Calls.Add(family + ":count"); CountCalls++;
                return CountCalls > 1 && CountAfter != null ? CountAfter : Count;
            }
            private object Numbers(string family, ref object ids)
            {
                Calls.Add(family + ":numbers");
                if (InPlace) Array.Copy((Array)Ids, (Array)ids, ((Array)Ids).Length);
                else ids = Ids;
                return NumberResult;
            }
            public object GetPrimaryLoadCaseCount() => ReadCount("primary");
            public object GetReferenceLoadCaseCount() => ReadCount("reference");
            public object GetLoadCombinationCaseCount() => ReadCount("combination");
            public object GetPrimaryLoadCaseNumbers(ref object ids) => Numbers("primary", ref ids);
            public object GetReferenceLoadCaseNumbers(ref object ids) => Numbers("reference", ref ids);
            public object GetLoadCombinationCaseNumbers(ref object ids) => Numbers("combination", ref ids);
            public object GetLoadCaseTitle(int id) { Calls.Add("title:" + id); return "native " + id; }
            public object GetReferenceLoadCaseTitle(int id) { Calls.Add("reference-title:" + id); return "reference " + id; }
            public object GetLoadType(int id) { Calls.Add("type:" + id); return 0; }
            public object GetReferenceLoadType(int id) { Calls.Add("reference-type:" + id); return 0; }
        }
        private static OpenStaadWrapper Wrap(Native native) => new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native));
        private static IEnumerable<ILoadCase> Read(IOSLoad load, LoadCaseType family) => load.GetAllLoadCasesOfType(family);

        [TestCase(false)] [TestCase(true)]
        public void BooleanPrimarySuccessWithSeventyFourCasesSupportsTheReportedFilter(bool inPlace)
        {
            var raw = new Native { Count = 74, Ids = Enumerable.Range(1, 74).Reverse().ToArray(), InPlace = inPlace };
            using (var wrapper = Wrap(raw))
            {
                var cases = wrapper.Load.GetLoadCasesFromIds(new[] { 1, 2, 3 }, LoadCaseType.PrimaryLoad).ToList();
                Assert.That(cases.Select(c => c.Id), Is.EquivalentTo(new[] { 1, 2, 3 }));
                Assert.That(cases.All(c => c.Title == "native " + c.Id && c.CaseType == LoadCaseType.PrimaryLoad), Is.True);
                Assert.That(raw.Calls.Take(3), Is.EqualTo(new[] { "primary:count", "primary:numbers", "primary:count" }));
                Assert.That(raw.Calls.Count(c => c.StartsWith("title:")), Is.EqualTo(74));
                Assert.That(wrapper.Load.GetPrimaryLoadCasesFromIds(new[] { 1, 2, 3 }).Select(c => c.Id), Is.EqualTo(new[] { 1, 2, 3 }));
            }
        }
        [TestCase(false)] [TestCase(true)]
        public void BooleanCombinationSuccessWithTwoHundredThirtyCasesSupportsAllAndLookupQueries(bool inPlace)
        {
            var raw = new Native { Count = 230, Ids = Enumerable.Range(100, 230).ToArray(), InPlace = inPlace };
            using (var wrapper = Wrap(raw))
            {
                Assert.That(wrapper.Load.GetAllLoadCombinationCases().Count, Is.EqualTo(230));
                Assert.That(wrapper.Load.GetLoadCasesFromIds(new[] { 100, 201 }, LoadCaseType.LoadCombination).Select(c => c.Id), Is.EquivalentTo(new[] { 100, 201 }));
                Assert.That(wrapper.Load.GetLoadCombinationCaseFromId(201).Id, Is.EqualTo(201));
                Assert.That(wrapper.Load.GetLoadCombinationCasesFromIds(new[] { 201, 301 }).Select(c => c.Id), Is.EqualTo(new[] { 201, 301 }));
                Assert.That(raw.Calls.Any(c => c.StartsWith("type:")), Is.False);
            }
        }
        [TestCase(LoadCaseType.PrimaryLoad)] [TestCase(LoadCaseType.LoadCombination)]
        public void GenericFamilyAndRangeQueriesUseTheCorrectBooleanContract(LoadCaseType family)
        {
            using (var wrapper = Wrap(new Native()))
            {
                Assert.That(wrapper.Load.GetLoadCases(1, 100, family).Select(c => c.Id), Is.EquivalentTo(new[] { 41, 7 }));
                Assert.That(wrapper.Load.GetLoadCasesOfType(family).Select(c => c.Id), Is.EquivalentTo(new[] { 41, 7 }));
                Assert.That(Read(wrapper.Load, family).Select(c => c.Id), Is.EquivalentTo(new[] { 41, 7 }));
            }
        }
        [TestCase(LoadCaseType.PrimaryLoad)] [TestCase(LoadCaseType.LoadCombination)]
        public void BooleanFalseFailsBeforeMetadataDespiteAnOtherwiseValidArray(LoadCaseType family)
        {
            var raw = new Native { NumberResult = false };
            using (var wrapper = Wrap(raw)) Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, family).ToList());
            Assert.That(raw.Calls.Count, Is.EqualTo(2));
        }
        [TestCase(true)] [TestCase(false)]
        public void ReferenceEnumerationStillRejectsBooleanInsteadOfItsIntegerCount(bool flag)
        {
            var raw = new Native { NumberResult = flag };
            using (var wrapper = Wrap(raw)) Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, LoadCaseType.ReferenceLoad).ToList());
            Assert.That(raw.Calls.Count, Is.EqualTo(2));
        }
        public static IEnumerable<TestCaseData> NumericCounts
        {
            get
            {
                foreach (var family in new[] { LoadCaseType.PrimaryLoad, LoadCaseType.ReferenceLoad, LoadCaseType.LoadCombination })
                    foreach (object count in new object[] { (short)2, 2, 2L }) yield return new TestCaseData(family, count);
            }
        }
        [TestCaseSource(nameof(NumericCounts))]
        public void DocumentedMatchingIntegralCountFormsRemainSupported(LoadCaseType family, object count)
        {
            var raw = new Native { NumberResult = count };
            using (var wrapper = Wrap(raw)) Assert.That(Read(wrapper.Load, family).Select(c => c.Id), Is.EquivalentTo(new[] { 41, 7 }));
            Assert.That(raw.CountCalls, Is.EqualTo(1));
        }
        public static IEnumerable<TestCaseData> MalformedArrays
        {
            get
            {
                foreach (var family in new[] { LoadCaseType.PrimaryLoad, LoadCaseType.LoadCombination })
                    foreach (object ids in new object[] { null, new int[0], new[] { 41 }, new[] { 41, 7, 9 }, new long[] { 41, 7 }, new int[1, 2], new[] { 41, 0 }, new[] { 41, -7 }, new[] { 41, 41 }, new int[2] })
                        yield return new TestCaseData(family, ids);
            }
        }
        [TestCaseSource(nameof(MalformedArrays))]
        public void BooleanSuccessDoesNotBypassArrayAndIdValidation(LoadCaseType family, object ids)
        {
            var raw = new Native { Ids = ids };
            using (var wrapper = Wrap(raw)) Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, family).ToList());
            Assert.That(raw.Calls.Any(c => c.Contains("title:")), Is.False);
        }
        [TestCase(LoadCaseType.PrimaryLoad)] [TestCase(LoadCaseType.LoadCombination)]
        public void BooleanSuccessRequiresTheSeparateCountToRemainConsistent(LoadCaseType family)
        {
            var raw = new Native { CountAfter = 3 };
            using (var wrapper = Wrap(raw)) Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, family).ToList());
            Assert.That(raw.CountCalls, Is.EqualTo(2));
            Assert.That(raw.Calls.Any(c => c.Contains("title:")), Is.False);
        }
        [TestCase(LoadCaseType.PrimaryLoad)] [TestCase(LoadCaseType.LoadCombination)]
        public void BooleanSuccessRechecksTheCountWithoutCoercingItsType(LoadCaseType family)
        {
            var raw = new Native { CountAfter = true };
            using (var wrapper = Wrap(raw)) Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, family).ToList());
        }
    }
}
