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
    public class MemberPopulationRefreshTests
    {
        public class Native : CombinationGroupingReadTests.Native
        {
            public bool FailMoments;
            public int Reads;
            public new int GetConcForceCount(int id) => 1;
            public new int GetConcMomentCount(int id) => FailMoments ? -1 : 1;
            public new int GetUDLLoadCount(int id) => 1;
            public new int GetUNIMomentCount(int id) => 1;
            public new int GetLinearVaryingLoadCount(int id) => 1;
            public new int GetTrapLoadCount(int id) => 1;
            public new int GetConcForces(int id, ref object direction, ref object force, ref object position, ref object offset)
            { Reads++; direction = new[] { 5 }; force = new[] { -7.25 }; position = new[] { 1.5 }; offset = new[] { -.125 }; return 0; }
            public int GetConcMoments(int id, ref object direction, ref object moment, ref object position, ref object offset)
            { Reads++; direction = new[] { 6 }; moment = new[] { -9.75 }; position = new[] { 2.25 }; offset = new[] { -.125 }; return 0; }
            public int GetUDLLoads(int id, ref object direction, ref object force, ref object start, ref object end, ref object offset)
            { Reads++; direction = new[] { 5 }; force = new[] { -11.5 }; start = new[] { 1.0 }; end = new[] { 3.0 }; offset = new[] { -.25 }; return 0; }
            public int GetUNIMoments(int id, ref object direction, ref object moment, ref object start, ref object end, ref object offset)
            { Reads++; direction = new[] { 9 }; moment = new[] { -13.5 }; start = new[] { 1.25 }; end = new[] { 0.0 }; offset = new[] { -.75 }; return 0; }
            public int GetLinearVaryingLoads(int id, ref object direction, ref object start, ref object end, ref object middle)
            { Reads++; direction = new[] { 2 }; start = new[] { -11.0 }; end = new[] { -22.0 }; middle = new[] { -33.0 }; return 0; }
            public int GetTrapLoads(int id, ref object direction, ref object start, ref object end, ref object startPosition, ref object endPosition)
            { Reads++; direction = new[] { 7 }; start = new[] { -4.0 }; end = new[] { -12.0 }; startPosition = new[] { 1.5 }; endPosition = new[] { 3.25 }; return 0; }
        }
        private static Beam B() => new Beam(101, new Node(1, 0, 0, 0), new Node(2, 5, 0, 0));
        [TestCase(false)]
        [TestCase(true)]
        public void VaryingAndTrapezoidalPopulationAppendExactFieldsAndKeepExistingDefinitions(bool trapezoidal)
        {
            var native = new Native(); using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
            {
                var beam = B(); var old = new MemberConcentratedLoad(); beam.MemberLoadItems.Add(old);
                var lc = new LoadCase(41, "REF", LoadCaseType.ReferenceLoad);
                var beams = new Once<Beam>(new[] { beam }); var cases = new Once<ILoadCase>(new[] { lc });
                if (trapezoidal) wrapper.Load.PopulateMemberTrapezoidalLoads(beams, cases);
                else wrapper.Load.PopulateMemberLinearVaryingLoads(beams, cases);
                Assert.That(beams.Count, Is.EqualTo(1)); Assert.That(cases.Count, Is.EqualTo(1));
                Assert.That(beam.MemberLoadItems.Count, Is.EqualTo(2)); Assert.That(beam.MemberLoadItems[0], Is.SameAs(old));
                Assert.That(beam.MemberLoadItems[1].LoadCase, Is.SameAs(lc));
                if (trapezoidal)
                {
                    var item = (MemberTrapezoidalLoad)beam.MemberLoadItems[1];
                    Assert.That(new[] { item.WStart, item.WEnd, item.SPosition, item.EPosition }, Is.EqualTo(new[] { -4.0, -12.0, 1.5, 3.25 }));
                    Assert.That(item.Direction, Is.EqualTo(LoadDirection.ProjectedX));
                }
                else
                {
                    var item = (MemberLinearVaryingLoad)beam.MemberLoadItems[1];
                    Assert.That(new[] { item.WStart, item.WEnd, item.WMiddle }, Is.EqualTo(new[] { -11.0, -22.0, -33.0 }));
                    Assert.That(item.Direction, Is.EqualTo(LoadDirection.LocalY));
                }
                Assert.That(native.Reads, Is.EqualTo(1));
            }
        }
        [Test]
        public void FullPopulationIsEagerRefreshesAllSixFamiliesAndReturnsOriginalBeamObjects()
        {
            var native = new Native(); using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
            {
                var beam = B(); var old = new MemberConcentratedLoad(); beam.MemberLoadItems.Add(old);
                var lc = new LoadCase(17); var beams = new Once<Beam>(new[] { beam }); var cases = new Once<ILoadCase>(new[] { lc });
                var result = wrapper.Load.PopulateMemberLoads(beams, cases);
                Assert.That(beams.Count, Is.EqualTo(1)); Assert.That(cases.Count, Is.EqualTo(1));
                Assert.That(result, Is.Not.SameAs(beams)); Assert.That(result.Single(), Is.SameAs(beam));
                Assert.That(result.Single(), Is.SameAs(beam)); Assert.That(native.Reads, Is.EqualTo(6));
                Assert.That(beam.MemberLoadItems, Does.Not.Contain(old));
                Assert.That(beam.MemberLoadItems.Select(l => l.GetType()), Is.EqualTo(new[] { typeof(MemberConcentratedLoad), typeof(MemberConcentratedMoment), typeof(MemberUniformlyDistributedLoad), typeof(MemberUniformMoment), typeof(MemberLinearVaryingLoad), typeof(MemberTrapezoidalLoad) }));
                Assert.That(beam.MemberLoadItems.All(l => ReferenceEquals(l.LoadCase, lc) && l.Members.Count == 0), Is.True);
                Assert.That(native.Active, Is.EqualTo(17));
            }
        }
        [Test]
        public void FullPopulationValidatesBeforeClearAndEmptyCasesClearWithoutReads()
        {
            var native = new Native(); using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
            {
                var beam = B(); var old = new MemberConcentratedLoad(); beam.MemberLoadItems.Add(old);
                Assert.Throws<ArgumentException>(() => wrapper.Load.PopulateMemberLoads(new[] { beam, null }, new[] { new LoadCase(17) }));
                Assert.Throws<NotSupportedException>(() => wrapper.Load.PopulateMemberLoads(new[] { beam }, new[] { new LoadCase(17, "BAD", LoadCaseType.LoadCombination) }));
                Assert.That(beam.MemberLoadItems.Single(), Is.SameAs(old));
                Assert.That(wrapper.Load.PopulateMemberLoads(new[] { beam }, new ILoadCase[0]).Single(), Is.SameAs(beam));
                Assert.That(beam.MemberLoadItems, Is.Empty); Assert.That(native.Reads, Is.Zero);
            }
        }
        [Test]
        public void FullPopulationFailureLeavesEarlierFamiliesAndStopsFurtherReads()
        {
            var native = new Native { FailMoments = true }; using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
            {
                var beam = B(); beam.MemberLoadItems.Add(new MemberTrapezoidalLoad());
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.PopulateMemberLoads(new[] { beam }, new[] { new LoadCase(17) }));
                Assert.That(beam.MemberLoadItems.Single(), Is.TypeOf<MemberConcentratedLoad>()); Assert.That(native.Reads, Is.EqualTo(1));
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
