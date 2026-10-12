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
    public class OSLoadCaseSelectionTests
    {
        // No activation API: generic queries must only read their selected family's metadata.
        public class SelectionLoad
        {
            public object Count { get; set; } = 2;
            public object NumberCount { get; set; } = 2;
            public int[] PrimaryIds { get; set; } = new[] { 41, 17 };
            public int[] ReferenceIds { get; set; } = new[] { 41, 101 };
            public int[] CombinationIds { get; set; } = new[] { 201, 301 };
            public bool InvalidIds { get; set; }
            public bool InvalidTitle { get; set; }
            public bool InvalidType { get; set; }
            public string ThrowAt { get; set; }
            public int FailureId { get; set; }
            public Exception Failure { get; set; } = new COMException("selection failed");
            public List<string> Calls { get; } = new List<string>();
            private void Record(string family, string stage, int id = 0)
            {
                Calls.Add(family + ":" + stage + (id == 0 ? "" : ":" + id));
                if (ThrowAt == stage && (FailureId == 0 || FailureId == id)) throw Failure;
            }
            private object Numbers(string family, ref object ids, int[] values)
            {
                Record(family, "numbers");
                ids = InvalidIds ? new[] { 0, 0 } : values;
                return NumberCount;
            }
            public object GetPrimaryLoadCaseCount() { Record("primary", "count"); return Count; }
            public object GetReferenceLoadCaseCount() { Record("reference", "count"); return Count; }
            public object GetLoadCombinationCaseCount() { Record("combination", "count"); return Count; }
            public object GetPrimaryLoadCaseNumbers(ref object ids) => Numbers("primary", ref ids, PrimaryIds);
            public object GetReferenceLoadCaseNumbers(ref object ids) => Numbers("reference", ref ids, ReferenceIds);
            public object GetLoadCombinationCaseNumbers(ref object ids) => Numbers("combination", ref ids, CombinationIds);
            public object GetLoadCaseTitle(int id)
            {
                string family = CombinationIds.Contains(id) ? "combination" : "primary";
                Record(family, "title", id);
                return InvalidTitle ? null : "  " + family + " 雪 " + id + "  ";
            }
            public object GetReferenceLoadCaseTitle(int id)
            {
                Record("reference", "title", id);
                return InvalidTitle ? null : "  reference 雪 " + id + "  ";
            }
            public object GetLoadType(int id) { Record("primary", "type", id); return InvalidType ? -1 : (int)LoadType.Dead; }
            public object GetReferenceLoadType(int id) { Record("reference", "type", id); return InvalidType ? -1 : (int)LoadType.Mass; }
        }

        private static string Family(LoadCaseType type) => type == LoadCaseType.PrimaryLoad ? "primary" : type == LoadCaseType.ReferenceLoad ? "reference" : "combination";
        private static int[] Ids(LoadCaseType type) => type == LoadCaseType.PrimaryLoad ? new[] { 41, 17 } : type == LoadCaseType.ReferenceLoad ? new[] { 41, 101 } : new[] { 201, 301 };
        private static LoadType EngineeringType(LoadCaseType type) => type == LoadCaseType.PrimaryLoad ? LoadType.Dead : type == LoadCaseType.ReferenceLoad ? LoadType.Mass : LoadType.None;

        [TestCase(LoadCaseType.PrimaryLoad)]
        [TestCase(LoadCaseType.ReferenceLoad)]
        [TestCase(LoadCaseType.LoadCombination)]
        public void FamilyQuery_DispatchesOnlySelectedFamilyAndIsEager(LoadCaseType type)
        {
            var raw = new SelectionLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var result = wrapper.Load.GetAllLoadCasesOfType(type);
                Assert.That(result, Is.InstanceOf<HashSet<ILoadCase>>());
                int nativeCalls = raw.Calls.Count;
                Assert.That(nativeCalls, Is.EqualTo(type == LoadCaseType.LoadCombination ? 4 : 6), "All reads finish before return.");
                var first = result.ToList();
                Assert.That(first.Select(c => c.Id), Is.EquivalentTo(Ids(type)));
                foreach (var item in first)
                {
                    Assert.That(item, Is.TypeOf<LoadCase>());
                    Assert.That(item.CaseType, Is.EqualTo(type));
                    Assert.That(item.Type, Is.EqualTo(EngineeringType(type)));
                    Assert.That(item.Title, Is.EqualTo("  " + Family(type) + " 雪 " + item.Id + "  "));
                }
                result.ToList();
                Assert.That(raw.Calls.Count, Is.EqualTo(nativeCalls), "Re-enumeration must not contact COM.");
                Assert.That(raw.Calls.All(c => c.StartsWith(Family(type) + ":")), Is.True);
                var again = wrapper.Load.GetAllLoadCasesOfType(type);
                Assert.That(again, Is.Not.SameAs(result));
                foreach (var item in again) Assert.That(item, Is.Not.SameAs(first.Single(c => c.Id == item.Id)));
            }
        }

        [Test]
        public void FamilyQuery_DefaultIsPrimaryThroughInterfaceAndConcreteAdapter()
        {
            var raw = new SelectionLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                Assert.That(wrapper.Load.GetAllLoadCasesOfType().Select(c => c.CaseType), Is.All.EqualTo(LoadCaseType.PrimaryLoad));
                Assert.That(new OSLoadAdapter(wrapper).GetAllLoadCasesOfType().Select(c => c.CaseType), Is.All.EqualTo(LoadCaseType.PrimaryLoad));
            }
            Assert.That(raw.Calls.All(c => c.StartsWith("primary:")), Is.True);
        }

        [TestCase(LoadCaseType.PrimaryLoad)]
        [TestCase(LoadCaseType.ReferenceLoad)]
        [TestCase(LoadCaseType.LoadCombination)]
        public void FamilyQuery_ZeroCountReturnsFreshEmptySets(LoadCaseType type)
        {
            var raw = new SelectionLoad { Count = 0 };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var first = wrapper.Load.GetAllLoadCasesOfType(type);
                var second = wrapper.Load.GetAllLoadCasesOfType(type);
                Assert.That(first, Is.Empty);
                Assert.That(second, Is.Empty.And.Not.SameAs(first));
            }
            Assert.That(raw.Calls, Is.EqualTo(new[] { Family(type) + ":count", Family(type) + ":count" }));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void GenericQueries_RejectRepeatAndUnknownFamiliesBeforeCom(bool lookup)
        {
            var raw = new SelectionLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                foreach (LoadCaseType type in new[] { LoadCaseType.RepeatLoad, (LoadCaseType)0, (LoadCaseType)(-1), (LoadCaseType)int.MaxValue })
                    if (lookup) Assert.Throws<NotSupportedException>(() => wrapper.Load.GetLoadCase(41, type));
                    else Assert.Throws<NotSupportedException>(() => wrapper.Load.GetAllLoadCasesOfType(type));
            Assert.That(raw.Calls, Is.Empty);
        }

        [TestCase(LoadCaseType.PrimaryLoad)]
        [TestCase(LoadCaseType.ReferenceLoad)]
        [TestCase(LoadCaseType.LoadCombination)]
        public void GenericLookup_FindsOnlySelectedFamilyAndReturnsFreshMetadata(LoadCaseType type)
        {
            var raw = new SelectionLoad();
            int id = Ids(type)[0];
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var result = wrapper.Load.GetLoadCase(id, type);
                Assert.That(result.Id, Is.EqualTo(id));
                Assert.That(result.CaseType, Is.EqualTo(type));
                Assert.That(result.Type, Is.EqualTo(EngineeringType(type)));
                Assert.That(result.Title, Does.Contain(Family(type)).And.Contain("雪"));
                Assert.That(raw.Calls.Count, Is.EqualTo(type == LoadCaseType.LoadCombination ? 4 : 6), "Reads the whole family, as in the main project.");
                var again = wrapper.Load.GetLoadCase(id, type);
                Assert.That(again, Is.EqualTo(result).And.Not.SameAs(result));
                ((LoadCase)result).Title = "edited";
                Assert.That(again.Title, Does.Contain(Family(type)));
                Assert.That(raw.Calls.All(c => c.StartsWith(Family(type) + ":")), Is.True);
            }
        }

        [TestCase(LoadCaseType.PrimaryLoad)]
        [TestCase(LoadCaseType.ReferenceLoad)]
        [TestCase(LoadCaseType.LoadCombination)]
        public void GenericLookup_AbsentIdReturnsNullAfterSuccessfulFamilyRead(LoadCaseType type)
        {
            var raw = new SelectionLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.That(wrapper.Load.GetLoadCase(int.MaxValue, type), Is.Null);
            Assert.That(raw.Calls.Count, Is.EqualTo(type == LoadCaseType.LoadCombination ? 4 : 6));
        }

        [Test]
        public void GenericLookup_DoesNotMatchNumbersInOtherFamilies()
        {
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(new SelectionLoad())))
            {
                Assert.That(wrapper.Load.GetLoadCase(101, LoadCaseType.PrimaryLoad), Is.Null);
                Assert.That(wrapper.Load.GetLoadCase(17, LoadCaseType.ReferenceLoad), Is.Null);
                Assert.That(wrapper.Load.GetLoadCase(41, LoadCaseType.LoadCombination), Is.Null);
                Assert.That(wrapper.Load.GetLoadCase(41, LoadCaseType.PrimaryLoad).Type, Is.EqualTo(LoadType.Dead));
                Assert.That(wrapper.Load.GetLoadCase(41, LoadCaseType.ReferenceLoad).Type, Is.EqualTo(LoadType.Mass));
            }
        }

        [TestCase(LoadCaseType.PrimaryLoad)]
        [TestCase(LoadCaseType.ReferenceLoad)]
        [TestCase(LoadCaseType.LoadCombination)]
        public void GenericLookup_EmptyFamilyReturnsNullWithoutFetchingNumbers(LoadCaseType type)
        {
            var raw = new SelectionLoad { Count = 0 };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.That(wrapper.Load.GetLoadCase(41, type), Is.Null);
            Assert.That(raw.Calls, Is.EqualTo(new[] { Family(type) + ":count" }));
        }

        [Test]
        public void GenericLookup_InvalidIdsThrowBeforeCom()
        {
            var raw = new SelectionLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                foreach (int id in new[] { 0, -1, int.MinValue })
                    foreach (LoadCaseType type in new[] { LoadCaseType.PrimaryLoad, LoadCaseType.ReferenceLoad, LoadCaseType.LoadCombination, LoadCaseType.RepeatLoad })
                        Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.GetLoadCase(id, type)).ParamName, Is.EqualTo("lcId"));
            Assert.That(raw.Calls, Is.Empty);
        }

        [TestCase(LoadCaseType.PrimaryLoad, false)]
        [TestCase(LoadCaseType.PrimaryLoad, true)]
        [TestCase(LoadCaseType.ReferenceLoad, false)]
        [TestCase(LoadCaseType.ReferenceLoad, true)]
        [TestCase(LoadCaseType.LoadCombination, false)]
        [TestCase(LoadCaseType.LoadCombination, true)]
        public void GenericQueries_PropagateMalformedFamilyDataInsteadOfEmptyOrNull(LoadCaseType type, bool lookup)
        {
            foreach (var raw in new[] { new SelectionLoad { Count = -1 }, new SelectionLoad { NumberCount = 0 }, new SelectionLoad { InvalidIds = true }, new SelectionLoad { InvalidTitle = true } })
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    if (lookup) Assert.Throws<InvalidOperationException>(() => wrapper.Load.GetLoadCase(int.MaxValue, type));
                    else Assert.Throws<InvalidOperationException>(() => wrapper.Load.GetAllLoadCasesOfType(type));
            if (type != LoadCaseType.LoadCombination)
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(new SelectionLoad { InvalidType = true })))
                    if (lookup) Assert.Throws<InvalidOperationException>(() => wrapper.Load.GetLoadCase(41, type));
                    else Assert.Throws<InvalidOperationException>(() => wrapper.Load.GetAllLoadCasesOfType(type));
        }

        [TestCase(LoadCaseType.PrimaryLoad)]
        [TestCase(LoadCaseType.ReferenceLoad)]
        [TestCase(LoadCaseType.LoadCombination)]
        public void GenericLookup_UnrelatedMetadataFailureStillThrows(LoadCaseType type)
        {
            var raw = new SelectionLoad { ThrowAt = "title", FailureId = Ids(type)[1] };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.That(Assert.Throws<COMException>(() => wrapper.Load.GetLoadCase(Ids(type)[0], type)), Is.SameAs(raw.Failure));
            Assert.That(raw.Calls.Last(), Is.EqualTo(Family(type) + ":title:" + Ids(type)[1]));
        }

        [TestCase(LoadCaseType.PrimaryLoad)]
        [TestCase(LoadCaseType.ReferenceLoad)]
        [TestCase(LoadCaseType.LoadCombination)]
        public void GenericQueries_PropagateNativeAndMissingSignatureFailures(LoadCaseType type)
        {
            var raw = new SelectionLoad { ThrowAt = "count" };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                Assert.That(Assert.Throws<COMException>(() => wrapper.Load.GetAllLoadCasesOfType(type)), Is.SameAs(raw.Failure));
                Assert.That(Assert.Throws<COMException>(() => wrapper.Load.GetLoadCase(41, type)), Is.SameAs(raw.Failure));
            }
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(new object())))
            {
                Assert.Throws<RuntimeBinderException>(() => wrapper.Load.GetAllLoadCasesOfType(type));
                Assert.Throws<RuntimeBinderException>(() => wrapper.Load.GetLoadCase(41, type));
            }
        }

        [Test]
        public void ConcreteAdapter_GenericLookupUsesFamilyDispatch()
        {
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(new SelectionLoad())))
                Assert.That(new OSLoadAdapter(wrapper).GetLoadCase(41, LoadCaseType.ReferenceLoad).Type, Is.EqualTo(LoadType.Mass));
        }
    }
}
