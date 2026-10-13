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
    public class RepeatPopulationTests
    {
        public class RepeatNative : CombinationGroupingReadTests.Native
        {
            public bool FailActivation;
            public object RepeatCount;
            public int RepeatReads;
            public List<int> Scanned = new List<int>();
            public new bool SetLoadActive(int id) { Scanned.Add(id); Active = id; return !FailActivation; }
            public object GetRepeatLoadCount() { RepeatReads++; return RepeatCount ?? (Active == 41 ? 2 : 0); }
        }
        [Test]
        public void RepeatDiscoveryClassifiesPrimaryMetadataAndDoesNotReturnFactors()
        {
            var native = new RepeatNative(); using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
            {
                var result = wrapper.Load.GetRepeatLoadCases();
                var item = result.Single();
                Assert.That(item.Id, Is.EqualTo(41)); Assert.That(item.Title, Is.EqualTo("TITLE 41"));
                Assert.That(item.CaseType, Is.EqualTo(LoadCaseType.RepeatLoad)); Assert.That(item.Type, Is.EqualTo(LoadType.Dead));
                Assert.That(native.Scanned, Is.EquivalentTo(new[] { 17, 41 })); Assert.That(native.RepeatReads, Is.EqualTo(2));
                Assert.That(native.Active, Is.EqualTo(native.Scanned.Last()));
                var again = wrapper.Load.GetRepeatLoadCases();
                Assert.That(again, Is.Not.SameAs(result)); Assert.That(again.Single(), Is.Not.SameAs(item));
            }
        }
        [TestCase(-1)]
        [TestCase(1.25)]
        [TestCase("invalid")]
        public void RepeatMalformedCountsThrow(object count)
        {
            var native = new RepeatNative { RepeatCount = count }; using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.GetRepeatLoadCases());
        }
        [Test]
        public void RepeatActivationFailureStopsScanAndZeroReportsNoCases()
        {
            var native = new RepeatNative { FailActivation = true }; using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
            {
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.GetRepeatLoadCases()); Assert.That(native.RepeatReads, Is.Zero);
                native.FailActivation = false; native.RepeatCount = 0;
                Assert.That(wrapper.Load.GetRepeatLoadCases(), Is.Empty);
            }
        }
        [TestCase(false)]
        [TestCase(true)]
        public void ConcentratedPopulationAppendsPreciseSubtypeRecordsAndKeepsCaseIdentity(bool moment)
        {
            var native = new OSLoadQueryTests.QueryLoad
            {
                Columns = new object[] { new[] { 6, 6 }, new[] { -9.75, -9.75 }, new[] { 2.25, 2.25 }, new[] { -.125, -.125 } }
            };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
            {
                var beam = new Beam(101, new Node(1, 0, 0, 0), new Node(2, 5, 0, 0));
                var old = new MemberConcentratedLoad(); beam.MemberLoadItems.Add(old);
                var lc = new LoadCase(41, "REF", LoadCaseType.ReferenceLoad);
                var beams = new Once<Beam>(new[] { beam }); var cases = new Once<ILoadCase>(new[] { lc });
                if (moment) wrapper.Load.PopulateMemberConcentratedMoments(beams, cases);
                else wrapper.Load.PopulateMemberConcentratedLoads(beams, cases);
                Assert.That(beams.Count, Is.EqualTo(1)); Assert.That(cases.Count, Is.EqualTo(1));
                Assert.That(beam.MemberLoadItems.Count, Is.EqualTo(3)); Assert.That(beam.MemberLoadItems[0], Is.SameAs(old));
                Assert.That(beam.MemberLoadItems.Skip(1).All(l => ReferenceEquals(l.LoadCase, lc) && l.Members.Count == 0), Is.True);
                Assert.That(beam.MemberLoadItems[1], moment ? Is.TypeOf<MemberConcentratedMoment>() : Is.TypeOf<MemberConcentratedLoad>());
                if (moment) Assert.That(((MemberConcentratedMoment)beam.MemberLoadItems[1]).Magnitude, Is.EqualTo(-9.75));
                else Assert.That(((MemberConcentratedLoad)beam.MemberLoadItems[1]).Magnitude, Is.EqualTo(-9.75));
                if (moment) wrapper.Load.PopulateMemberConcentratedMoments(new[] { beam }, new[] { lc });
                else wrapper.Load.PopulateMemberConcentratedLoads(new[] { beam }, new[] { lc });
                Assert.That(beam.MemberLoadItems.Count, Is.EqualTo(5));
            }
        }
        [Test]
        public void PopulationValidatesAllInputBeforeAppendAndEmptyInputPreservesExistingItems()
        {
            var native = new OSLoadQueryTests.QueryLoad(); using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
            {
                var beam = new Beam { Id = 101 }; var old = new MemberConcentratedLoad(); beam.MemberLoadItems.Add(old);
                Assert.Throws<NotSupportedException>(() => wrapper.Load.PopulateMemberConcentratedLoads(new[] { beam }, new[] { new LoadCase(17), new LoadCase(18, "BAD", LoadCaseType.LoadCombination) }));
                Assert.Throws<ArgumentException>(() => wrapper.Load.PopulateMemberConcentratedMoments(new[] { beam, null }, new[] { new LoadCase(17) }));
                wrapper.Load.PopulateMemberConcentratedLoads(new[] { beam }, new ILoadCase[0]);
                Assert.That(beam.MemberLoadItems.Single(), Is.SameAs(old)); Assert.That(native.Calls, Is.Empty);
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
