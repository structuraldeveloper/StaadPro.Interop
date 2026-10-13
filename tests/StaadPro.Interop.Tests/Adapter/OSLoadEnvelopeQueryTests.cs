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
    public class OSLoadEnvelopeQueryTests
    {
        // No activation, active-load-list, case-title or result API is available to these queries.
        public class EnvelopeLoad
        {
            public object Count = 2;
            public object EnvelopeIds = new[] { 7, 19 };
            public object EnvelopeStatus = 1;
            public object DetailStatus = 1;
            public object MemberStatus = 1;
            public int? Type = 5;
            public int? MembersCount = 2;
            public object MemberIds = new[] { 17, 41 };
            public bool InPlace;
            public bool LeaveLastIdUnwritten;
            public int BadDetailId;
            public string ThrowAt;
            public int FailureId;
            public Exception Failure = new COMException("envelope read failed");
            public List<string> Calls { get; } = new List<string>();
            private void Record(string stage, int id = 0)
            {
                Calls.Add(stage + (id == 0 ? "" : ":" + id));
                if (ThrowAt == stage && (FailureId == 0 || FailureId == id)) throw Failure;
            }
            public object GetEnvelopeCount() { Record("count"); return Count; }
            public object GetEnvelopeIDs(ref object ids)
            {
                Record("ids");
                Fill(ref ids, EnvelopeIds, Convert.ToInt32(Count));
                return EnvelopeStatus;
            }
            public object GetLoadEnvelopeDetails(int id, ref int type, ref int count)
            {
                Record("detail", id);
                Assert.That(type, Is.EqualTo(-1), "Undefined output must not look like valid None.");
                Assert.That(count, Is.EqualTo(-1), "Undefined output must not look like valid zero members.");
                if (Type.HasValue) type = Type.Value;
                if (MembersCount.HasValue) count = MembersCount.Value;
                return id == BadDetailId ? -1 : DetailStatus;
            }
            public object GetLoadListfromLoadEnvelope(int id, ref object ids)
            {
                Record("members", id);
                Fill(ref ids, MemberIds, MembersCount.Value);
                return MemberStatus;
            }
            private void Fill(ref object destination, object source, int count)
            {
                Assert.That(destination, Is.TypeOf<int[]>());
                Assert.That((int[])destination, Has.Length.EqualTo(count));
                Assert.That(((int[])destination).All(id => id == 0), Is.True);
                if (InPlace) Array.Copy((int[])source, (int[])destination, ((int[])source).Length - (LeaveLastIdUnwritten ? 1 : 0));
                else destination = source;
            }
        }

        private static void Read(IOSLoad load, string operation)
        {
            if (operation == "ids") load.GetEnvelopeIds();
            else if (operation == "detail") load.GetLoadEnvelopeDetail(7);
            else load.GetAllLoadEnvelopeDetails();
        }

        [TestCase(false)]
        [TestCase(true)]
        public void Ids_ReadOnceAndReturnIndependentManagedSets(bool inPlace)
        {
            var raw = new EnvelopeLoad { InPlace = inPlace };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var first = wrapper.Load.GetEnvelopeIds();
                var second = wrapper.Load.GetEnvelopeIds();
                Assert.That(first, Is.EquivalentTo(new[] { 7, 19 }));
                Assert.That(second, Is.EquivalentTo(first).And.Not.SameAs(first));
                Assert.That(raw.Calls, Is.EqualTo(new[] { "count", "ids", "count", "ids" }));
                ((int[])raw.EnvelopeIds)[0] = 99;
                first.Clear();
                Assert.That(second, Is.EquivalentTo(new[] { 7, 19 }));
            }
        }

        [TestCase(0, false)] [TestCase(1, false)] [TestCase(2, false)] [TestCase(3, false)]
        [TestCase(4, false)] [TestCase(5, false)] [TestCase(6, false)] [TestCase(5, true)]
        public void Detail_MapsEveryClassificationAndBothArrayWriteModes(int type, bool inPlace)
        {
            var raw = new EnvelopeLoad { Type = type, InPlace = inPlace };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var first = wrapper.Load.GetLoadEnvelopeDetail(7);
                var second = wrapper.Load.GetLoadEnvelopeDetail(7);
                Assert.That(first, Is.TypeOf<LoadEnvelopeDetail>());
                Assert.That(first.Id, Is.EqualTo(7));
                Assert.That(first.EnvelopType, Is.EqualTo((LoadEnvelopType)type));
                Assert.That(first.LoadCasesCount, Is.EqualTo(2));
                Assert.That(first.LoadCaseIds, Is.EquivalentTo(new[] { 17, 41 }));
                Assert.That(second, Is.Not.SameAs(first));
                Assert.That(second.LoadCaseIds, Is.Not.SameAs(first.LoadCaseIds));
                Assert.That(raw.Calls, Is.EqualTo(new[] { "detail:7", "members:7", "detail:7", "members:7" }));
                ((int[])raw.MemberIds)[0] = 99;
                first.LoadCaseIds.Clear();
                ((LoadEnvelopeDetail)first).Id = 999;
                first.EnvelopType = LoadEnvelopType.None;
                Assert.That(second.Id, Is.EqualTo(7));
                Assert.That(second.EnvelopType, Is.EqualTo((LoadEnvelopType)type));
                Assert.That(second.LoadCaseIds, Is.EquivalentTo(new[] { 17, 41 }));
            }
        }

        [TestCase(false)] [TestCase(true)]
        public void All_ReadsIdsOnceThenEveryEnvelopeEagerlyWithSeparateOwnership(bool inPlace)
        {
            var raw = new EnvelopeLoad { InPlace = inPlace };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var first = wrapper.Load.GetAllLoadEnvelopeDetails();
                Assert.That(raw.Calls, Is.EqualTo(new[] { "count", "ids", "detail:7", "members:7", "detail:19", "members:19" }));
                Assert.That(first.Select(e => e.Id), Is.EquivalentTo(new[] { 7, 19 }));
                Assert.That(first.All(e => e.LoadCasesCount == 2 && e.EnvelopType == LoadEnvelopType.Strength), Is.True);
                var second = wrapper.Load.GetAllLoadEnvelopeDetails();
                Assert.That(second, Is.Not.SameAs(first));
                var seven = first.Single(e => e.Id == 7);
                seven.LoadCaseIds.Clear();
                Assert.That(first.Single(e => e.Id == 19).LoadCaseIds, Is.EquivalentTo(new[] { 17, 41 }));
                Assert.That(second.Single(e => e.Id == 7).LoadCaseIds, Is.EquivalentTo(new[] { 17, 41 }));
                Assert.That(second.Single(e => e.Id == 7), Is.Not.SameAs(seven));
            }
        }

        [TestCase("ids")] [TestCase("all")]
        public void NoEnvelopes_ReturnsFreshEmptySetsAndSkipsFetchAndDetails(string operation)
        {
            var raw = new EnvelopeLoad { Count = 0 };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                if (operation == "ids")
                {
                    var first = wrapper.Load.GetEnvelopeIds();
                    Assert.That(first, Is.Empty);
                    Assert.That(wrapper.Load.GetEnvelopeIds(), Is.Empty.And.Not.SameAs(first));
                }
                else
                {
                    var first = wrapper.Load.GetAllLoadEnvelopeDetails();
                    Assert.That(first, Is.Empty);
                    Assert.That(wrapper.Load.GetAllLoadEnvelopeDetails(), Is.Empty.And.Not.SameAs(first));
                }
            }
            Assert.That(raw.Calls, Is.EqualTo(new[] { "count", "count" }));
        }

        [TestCase("detail")] [TestCase("all")]
        public void ZeroMembers_IsValidEmptyMetadataAndSkipsMembershipFetch(string operation)
        {
            var raw = new EnvelopeLoad { MembersCount = 0, Type = 0 };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var result = operation == "detail" ? new[] { wrapper.Load.GetLoadEnvelopeDetail(7) }
                    : wrapper.Load.GetAllLoadEnvelopeDetails().ToArray();
                Assert.That(result.Length, Is.EqualTo(operation == "detail" ? 1 : 2));
                Assert.That(result.All(e => e.LoadCasesCount == 0 && e.LoadCaseIds != null && e.LoadCaseIds.Count == 0 && e.EnvelopType == LoadEnvelopType.None), Is.True);
                Assert.That(raw.Calls.Any(c => c.StartsWith("members")), Is.False);
            }
        }

        [TestCase(0)] [TestCase(-1)] [TestCase(int.MinValue)]
        public void Detail_InvalidInputThrowsBeforeNativeAccess(int id)
        {
            var raw = new EnvelopeLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.GetLoadEnvelopeDetail(id)).ParamName, Is.EqualTo("envId"));
            Assert.That(raw.Calls, Is.Empty);
        }

        [Test]
        public void Detail_AcceptsLargestPositiveIdAndDelegatesAbsenceToNativeReader()
        {
            var raw = new EnvelopeLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.That(wrapper.Load.GetLoadEnvelopeDetail(int.MaxValue).Id, Is.EqualTo(int.MaxValue));
            Assert.That(raw.Calls, Is.EqualTo(new[] { "detail:" + int.MaxValue, "members:" + int.MaxValue }));
            raw.DetailStatus = -1;
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.GetLoadEnvelopeDetail(1001));
            Assert.That(raw.Calls.Last(), Is.EqualTo("detail:1001"));
        }

        [TestCase("ids")] [TestCase("all")]
        public void InvalidEnvelopeCountsFailBeforeFetching(string operation)
        {
            foreach (object count in new object[] { -1, int.MinValue, -106, null, true, "2", 2.0, (long)int.MaxValue + 1 })
            {
                var raw = new EnvelopeLoad { Count = count };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, operation));
                Assert.That(raw.Calls, Is.EqualTo(new[] { "count" }));
            }
        }

        [TestCase("ids")] [TestCase("detail")] [TestCase("members")]
        public void FetchRequiresIntegerOne_NotArrayLengthZeroOrVariantTrue(string stage)
        {
            foreach (object status in new object[] { -1, 0, 2, -106, null, true, "1", 1.0, long.MaxValue })
            {
                var raw = new EnvelopeLoad();
                if (stage == "ids") raw.EnvelopeStatus = status;
                else if (stage == "detail") raw.DetailStatus = status;
                else raw.MemberStatus = status;
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, stage == "ids" ? "all" : "detail"));
                Assert.That(raw.Calls.Last(), Is.EqualTo(stage == "ids" ? "ids" : stage + ":7"));
            }
        }

        [TestCase("detail")] [TestCase("all")]
        public void DetailRejectsUnwrittenOrInvalidClassificationAndCounts(string operation)
        {
            foreach (int? type in new int?[] { null, -1, 7, int.MaxValue, int.MinValue })
            {
                var raw = new EnvelopeLoad { Type = type };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, operation));
                Assert.That(raw.Calls.Last(), Is.EqualTo("detail:7"));
            }
            foreach (int? count in new int?[] { null, -1, int.MinValue })
            {
                var raw = new EnvelopeLoad { MembersCount = count };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, operation));
                Assert.That(raw.Calls.Last(), Is.EqualTo("detail:7"));
            }
        }

        [TestCase("ids")] [TestCase("all")]
        public void InvalidEnvelopeArraysFailBeforeAnyDetails(string operation)
        {
            foreach (object ids in InvalidArrays())
            {
                var raw = new EnvelopeLoad { EnvelopeIds = ids };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, operation));
                Assert.That(raw.Calls, Is.EqualTo(new[] { "count", "ids" }));
            }
        }

        [TestCase("detail")] [TestCase("all")]
        public void InvalidMembershipArraysFailWithoutReturningPartialData(string operation)
        {
            foreach (object ids in InvalidArrays())
            {
                var raw = new EnvelopeLoad { MemberIds = ids };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, operation));
                Assert.That(raw.Calls.Last(), Is.EqualTo("members:7"));
                Assert.That(raw.Calls.Any(c => c == "detail:19"), Is.False);
            }
        }

        private static IEnumerable<object> InvalidArrays()
        {
            return new object[] { null, new int[0], new[] { 7 }, new[] { 7, 19, 21 }, new long[] { 7, 19 }, new short[] { 7, 19 }, new object[] { 7, 19 }, new int[1, 2], new[] { 7, 0 }, new[] { 7, -1 }, new[] { 7, 7 }, new int[2] };
        }

        [TestCase("ids")] [TestCase("members")]
        public void PartlyUnwrittenInPlaceBuffersAreRejected(string stage)
        {
            var raw = new EnvelopeLoad { InPlace = true, LeaveLastIdUnwritten = true };
            // Direct lookup bypasses envelope discovery, isolating the membership output buffer.
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, stage == "ids" ? "ids" : "detail"));
            Assert.That(raw.Calls.Last(), Is.EqualTo(stage == "ids" ? "ids" : "members:7"));
        }

        [Test]
        public void LaterEnvelopeFailureStatusStopsAnOtherwiseSuccessfulEnumeration()
        {
            var raw = new EnvelopeLoad { BadDetailId = 19 };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.GetAllLoadEnvelopeDetails());
            Assert.That(raw.Calls, Is.EqualTo(new[] { "count", "ids", "detail:7", "members:7", "detail:19" }));
        }

        [TestCase(-3)] [TestCase(4)]
        public void NonzeroSafeArrayBoundsAndIntegerStatusRepresentationsAreSupported(int lowerBound)
        {
            var ids = Array.CreateInstance(typeof(int), new[] { 2 }, new[] { lowerBound });
            ids.SetValue(7, lowerBound); ids.SetValue(int.MaxValue, lowerBound + 1);
            var members = Array.CreateInstance(typeof(int), new[] { 2 }, new[] { lowerBound });
            members.SetValue(17, lowerBound); members.SetValue(int.MaxValue, lowerBound + 1);
            var raw = new EnvelopeLoad { Count = (short)2, EnvelopeStatus = 1L, DetailStatus = (short)1, MemberStatus = 1L, EnvelopeIds = ids, MemberIds = members };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var result = wrapper.Load.GetAllLoadEnvelopeDetails();
                Assert.That(result.Select(e => e.Id), Is.EquivalentTo(new[] { 7, int.MaxValue }));
                Assert.That(result.All(e => e.LoadCaseIds.SetEquals(new[] { 17, int.MaxValue })), Is.True);
            }
        }

        [TestCase("count", 0)] [TestCase("ids", 0)] [TestCase("detail", 7)]
        [TestCase("members", 7)] [TestCase("detail", 19)] [TestCase("members", 19)]
        public void NativeExceptionsPropagateUnchangedAtEveryStageAndStopEnumeration(string stage, int id)
        {
            var raw = new EnvelopeLoad { ThrowAt = stage, FailureId = id };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.That(Assert.Throws<COMException>(() => wrapper.Load.GetAllLoadEnvelopeDetails()), Is.SameAs(raw.Failure));
            Assert.That(raw.Calls.Last(), Is.EqualTo(stage + (id == 0 ? "" : ":" + id)));
        }

        public class CountOnly { public int GetEnvelopeCount() => 1; }
        public class IdsOnly : CountOnly { public int GetEnvelopeIDs(ref object ids) { ids = new[] { 7 }; return 1; } }
        public class DetailsOnly
        {
            public int GetLoadEnvelopeDetails(int id, ref int type, ref int count) { type = 0; count = 1; return 1; }
        }

        [Test]
        public void MissingNativeSignaturesPropagateBindingFailures()
        {
            foreach (object raw in new object[] { new object(), new CountOnly(), new IdsOnly() })
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.Throws<RuntimeBinderException>(() => wrapper.Load.GetAllLoadEnvelopeDetails());
            foreach (object raw in new object[] { new object(), new DetailsOnly() })
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.Throws<RuntimeBinderException>(() => wrapper.Load.GetLoadEnvelopeDetail(7));
        }
    }
}
