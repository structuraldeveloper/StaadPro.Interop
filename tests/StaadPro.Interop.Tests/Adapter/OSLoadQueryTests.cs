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
    public class OSLoadQueryTests
    {
        public class QueryRoot
        {
            public QueryRoot(object load) { Load = load; }
            public object Load { get; }
            public object Geometry => null;
        }

        // Public because the production assembly's dynamic binder must access this test double.
        public class QueryLoad
        {
            public object Count { get; set; } = 2;
            public object PrimaryStatus { get; set; } = true;
            public object ReferenceStatus { get; set; }
            public bool OverrideReferenceStatus { get; set; }
            public object FetchStatus { get; set; } = 0;
            public object[] Columns { get; set; }
            public bool WriteInPlace { get; set; }
            public string ThrowAt { get; set; }
            public Exception Failure { get; set; } = new COMException("test COM failure", unchecked((int)0x80004005));
            public List<string> Calls { get; } = new List<string>();

            private void Record(string stage, int id)
            {
                Calls.Add(stage + ":" + id);
                if (ThrowAt == stage) throw Failure;
            }

            public object SetLoadActive(int id) { Record("primary", id); return PrimaryStatus; }
            public object SetReferenceLoadActive(int id) { Record("reference", id); return OverrideReferenceStatus ? ReferenceStatus : id; }
            public object GetNodalLoadCount(int id) { Record("nodal-count", id); return Count; }
            public object GetUDLLoadCount(int id) { Record("udl-count", id); return Count; }
            public object GetConcForceCount(int id) { Record("concentrated-count", id); return Count; }
            public object GetConcMomentCount(int id) { Record("moment-count", id); return Count; }
            public object GetLinearVaryingLoadCount(int id) { Record("linear-count", id); return Count; }
            public object GetTrapLoadCount(int id) { Record("trapezoidal-count", id); return Count; }

            private void Write(ref object buffer, int index)
            {
                if (WriteInPlace)
                {
                    var source = (Array)Columns[index];
                    var target = (Array)buffer;
                    Assert.That(target.Length, Is.EqualTo((int)Count), "Input buffer must match count.");
                    Assert.That(target.GetType(), Is.EqualTo(source.GetType()), "Input SAFEARRAY element type.");
                    Array.Copy(source, target, source.Length);
                }
                else buffer = Columns[index];
            }

            public object GetNodalLoads(int id, ref object fx, ref object fy, ref object fz, ref object mx, ref object my, ref object mz)
            {
                Record("nodal-fetch", id);
                if (Columns != null) { Write(ref fx, 0); Write(ref fy, 1); Write(ref fz, 2); Write(ref mx, 3); Write(ref my, 4); Write(ref mz, 5); }
                return FetchStatus;
            }
            public object GetUDLLoads(int id, ref object direction, ref object force, ref object start, ref object end, ref object offset)
            {
                Record("udl-fetch", id);
                if (Columns != null) { Write(ref direction, 0); Write(ref force, 1); Write(ref start, 2); Write(ref end, 3); Write(ref offset, 4); }
                return FetchStatus;
            }
            public object GetConcForces(int id, ref object direction, ref object force, ref object position, ref object offset)
            {
                Record("concentrated-fetch", id);
                if (Columns != null) { Write(ref direction, 0); Write(ref force, 1); Write(ref position, 2); Write(ref offset, 3); }
                return FetchStatus;
            }
            public object GetConcMoments(int id, ref object direction, ref object moment, ref object position, ref object offset)
            {
                Record("moment-fetch", id);
                if (Columns != null) { Write(ref direction, 0); Write(ref moment, 1); Write(ref position, 2); Write(ref offset, 3); }
                return FetchStatus;
            }
            public object GetLinearVaryingLoads(int id, ref object direction, ref object start, ref object end, ref object middle)
            {
                Record("linear-fetch", id);
                if (Columns != null) { Write(ref direction, 0); Write(ref start, 1); Write(ref end, 2); Write(ref middle, 3); }
                return FetchStatus;
            }
            public object GetTrapLoads(int id, ref object direction, ref object startForce, ref object endForce, ref object startPosition, ref object endPosition)
            {
                Record("trapezoidal-fetch", id);
                if (Columns != null) { Write(ref direction, 0); Write(ref startForce, 1); Write(ref endForce, 2); Write(ref startPosition, 3); Write(ref endPosition, 4); }
                return FetchStatus;
            }
        }

        private static int MaximumDirection(string query) => query == "linear" ? 3 : query == "udl" || query == "trapezoidal" ? 9 : 6;

        private static QueryLoad MakeLoad(string query)
        {
            var columns = new List<object>();
            if (query != "nodal") columns.Add(new[] { 1, MaximumDirection(query) });
            int numericColumns = query == "nodal" ? 6 : query == "udl" || query == "trapezoidal" ? 4 : 3;
            for (int i = 0; i < numericColumns; i++) columns.Add(new[] { i + 0.25, -i - 0.5 });
            return new QueryLoad { Columns = columns.ToArray() };
        }

        private static System.Collections.IList ReadList(IOSLoad load, string query, ILoadCase lc, int id = 42)
        {
            switch (query)
            {
                case "nodal": return load.GetNodalLoads(lc, id);
                case "udl": return load.GetMemberUniformlyDistributedLoads(lc, id);
                case "concentrated": return load.GetMemberConcentratedLoads(lc, id);
                case "moment": return load.GetMemberConcentratedMoments(lc, id);
                case "linear": return load.GetMemberLinearVaryingLoads(lc, id);
                case "trapezoidal": return load.GetMemberTrapezoidalLoads(lc, id);
                default: throw new ArgumentException("Unknown query", nameof(query));
            }
        }

        private static IList<LoadItem> Read(IOSLoad load, string query, ILoadCase lc, int id = 42) => ReadList(load, query, lc, id).Cast<LoadItem>().ToList();

        private static double[] Values(LoadItem item)
        {
            if (item is NodalLoad node)
                return new[] { node.Forces.Fx, node.Forces.Fy, node.Forces.Fz, node.Forces.Mx, node.Forces.My, node.Forces.Mz };
            if (item is MemberUniformlyDistributedLoad udl)
                return new[] { udl.Magnitude, udl.SPosition, udl.EPosition, udl.Eccentricity };
            if (item is MemberConcentratedMoment moment)
                return new[] { moment.Magnitude, moment.Position, moment.Eccentricity };
            if (item is MemberLinearVaryingLoad linear)
                return new[] { linear.WStart, linear.WEnd, linear.WMiddle }; // Native output order.
            if (item is MemberTrapezoidalLoad trapezoidal)
                return new[] { trapezoidal.WStart, trapezoidal.WEnd, trapezoidal.SPosition, trapezoidal.EPosition };
            var point = (MemberConcentratedLoad)item;
            return new[] { point.Magnitude, point.Position, point.Eccentricity };
        }

        [TestCase("nodal", LoadCaseType.PrimaryLoad, false)]
        [TestCase("nodal", LoadCaseType.ReferenceLoad, true)]
        [TestCase("udl", LoadCaseType.PrimaryLoad, true)]
        [TestCase("udl", LoadCaseType.ReferenceLoad, false)]
        [TestCase("concentrated", LoadCaseType.PrimaryLoad, false)]
        [TestCase("concentrated", LoadCaseType.ReferenceLoad, true)]
        [TestCase("moment", LoadCaseType.PrimaryLoad, true)]
        [TestCase("moment", LoadCaseType.ReferenceLoad, false)]
        [TestCase("linear", LoadCaseType.PrimaryLoad, false)]
        [TestCase("linear", LoadCaseType.ReferenceLoad, true)]
        [TestCase("trapezoidal", LoadCaseType.PrimaryLoad, true)]
        [TestCase("trapezoidal", LoadCaseType.ReferenceLoad, false)]
        public void Read_MapsEveryColumnAndPreservesOrderSignsCaseAndOwnership(string query, LoadCaseType caseType, bool inPlace)
        {
            var com = MakeLoad(query);
            com.WriteInPlace = inPlace;
            var lc = new LoadCase(17, "case", caseType);
            using (var wrapper = new OpenStaadWrapper(new QueryRoot(com)))
            {
                var loads = Read(wrapper.Load, query, lc);
                Assert.That(com.Calls, Is.EqualTo(new[] { (caseType == LoadCaseType.PrimaryLoad ? "primary" : "reference") + ":17", query + "-count:42", query + "-fetch:42" }));
                Assert.That(loads.Count, Is.EqualTo(2));
                int offset = query == "nodal" ? 0 : 1;
                for (int row = 0; row < 2; row++)
                {
                    var expected = com.Columns.Skip(offset).Select(c => ((double[])c)[row]);
                    Assert.That(Values(loads[row]), Is.EqualTo(expected));
                    Assert.That(loads[row].LoadCase, Is.SameAs(lc));
                    if (loads[row] is MemberLoad member)
                    {
                        Assert.That(member.Direction, Is.EqualTo((LoadDirection)((int[])com.Columns[0])[row]));
                        Assert.That(member.Members, Is.Empty);
                    }
                }
                Assert.That(loads[0], Is.Not.SameAs(loads[1]));
                if (loads[0] is NodalLoad first) Assert.That(first.Forces, Is.Not.SameAs(((NodalLoad)loads[1]).Forces));
                var secondRead = Read(wrapper.Load, query, lc);
                Assert.That(secondRead[0], Is.Not.SameAs(loads[0]));
                ((double[])com.Columns[offset])[0] = 999;
                Assert.That(Values(loads[0])[0], Is.EqualTo(0.25), "Returned loads must not alias COM arrays.");
            }
        }

        [TestCase("nodal")]
        [TestCase("udl")]
        [TestCase("concentrated")]
        [TestCase("moment")]
        [TestCase("linear")]
        [TestCase("trapezoidal")]
        public void Read_ZeroCountSkipsFetchAndReturnsFreshEmptyList(string query)
        {
            var com = MakeLoad(query); com.Count = 0;
            using (var wrapper = new OpenStaadWrapper(new QueryRoot(com)))
            {
                Assert.That(Read(wrapper.Load, query, new LoadCase(1)), Is.Empty);
                Assert.That(com.Calls, Is.EqualTo(new[] { "primary:1", query + "-count:42" }));
                Assert.That(ReadList(wrapper.Load, query, new LoadCase(1)), Is.Not.SameAs(ReadList(wrapper.Load, query, new LoadCase(1))));
            }
        }

        [TestCase("nodal")]
        [TestCase("udl")]
        [TestCase("concentrated")]
        [TestCase("moment")]
        [TestCase("linear")]
        [TestCase("trapezoidal")]
        public void Read_RejectsInvalidArgumentsBeforeAnyComCall(string query)
        {
            var com = MakeLoad(query);
            using (var wrapper = new OpenStaadWrapper(new QueryRoot(com)))
            {
                Assert.That(Assert.Throws<ArgumentNullException>(() => Read(wrapper.Load, query, null)).ParamName, Is.EqualTo("lc"));
                foreach (int id in new[] { 0, -1 })
                {
                    Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => Read(wrapper.Load, query, new LoadCase(id))).ParamName, Is.EqualTo("lc"));
                    Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => Read(wrapper.Load, query, new LoadCase(1), id)).ParamName, Is.EqualTo(query == "nodal" ? "nId" : "mId"));
                }
                foreach (var caseType in new[] { LoadCaseType.LoadCombination, LoadCaseType.RepeatLoad, (LoadCaseType)0, (LoadCaseType)999 })
                    Assert.Throws<NotSupportedException>(() => Read(wrapper.Load, query, new LoadCase(1, "case", caseType)));
                Assert.That(com.Calls, Is.Empty);
            }
        }

        [TestCase("nodal")]
        [TestCase("udl")]
        [TestCase("concentrated")]
        [TestCase("moment")]
        [TestCase("linear")]
        [TestCase("trapezoidal")]
        public void Read_RejectsFailedOrMalformedActivationWithoutCounting(string query)
        {
            foreach (object status in new object[] { false, 0, 2, null, "true", 1.0 })
            {
                var com = MakeLoad(query); com.PrimaryStatus = status;
                using (var wrapper = new OpenStaadWrapper(new QueryRoot(com)))
                    Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, query, new LoadCase(17)));
                Assert.That(com.Calls, Is.EqualTo(new[] { "primary:17" }));
            }
            foreach (object status in new object[] { -1, -8002, 0, 18, null, true, "17" })
            {
                var com = MakeLoad(query); com.OverrideReferenceStatus = true; com.ReferenceStatus = status;
                using (var wrapper = new OpenStaadWrapper(new QueryRoot(com)))
                    Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, query, new LoadCase(17, "ref", LoadCaseType.ReferenceLoad)));
                Assert.That(com.Calls, Is.EqualTo(new[] { "reference:17" }));
            }
        }

        [TestCase("nodal")]
        [TestCase("udl")]
        [TestCase("concentrated")]
        [TestCase("moment")]
        [TestCase("linear")]
        [TestCase("trapezoidal")]
        public void Read_RejectsInvalidCountsAndDoesNotFetch(string query)
        {
            foreach (object count in new object[] { -1, -8002, null, 1.5, "2", true, (long)int.MaxValue + 1 })
            {
                var com = MakeLoad(query); com.Count = count;
                using (var wrapper = new OpenStaadWrapper(new QueryRoot(com)))
                    Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, query, new LoadCase(1)));
                Assert.That(com.Calls.Count, Is.EqualTo(2));
            }
        }

        [TestCase("nodal")]
        [TestCase("udl")]
        [TestCase("concentrated")]
        [TestCase("moment")]
        [TestCase("linear")]
        [TestCase("trapezoidal")]
        public void Read_RejectsFailedOrMalformedFetchStatuses(string query)
        {
            foreach (object status in new object[] { -1, -8002, 1, null, true, "0", 0.0 })
            {
                var com = MakeLoad(query); com.FetchStatus = status;
                using (var wrapper = new OpenStaadWrapper(new QueryRoot(com)))
                    Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, query, new LoadCase(1)));
            }
        }

        [TestCase("nodal")]
        [TestCase("udl")]
        [TestCase("concentrated")]
        [TestCase("moment")]
        [TestCase("linear")]
        [TestCase("trapezoidal")]
        public void Read_RejectsEveryMalformedColumn(string query)
        {
            int columnCount = MakeLoad(query).Columns.Length;
            for (int column = 0; column < columnCount; column++)
            {
                bool direction = query != "nodal" && column == 0;
                var invalidArrays = direction
                    ? new object[] { null, new int[0], new int[1], new int[3], new int[1, 2], new double[2], new object[] { 1, 2 }, new[] { 0, 1 }, new[] { 1, MaximumDirection(query) + 1 }, new[] { -1, 1 } }
                    : new object[] { null, new double[0], new double[1], new double[3], new double[1, 2], new int[2], new object[] { 1.0, 2.0 }, new[] { double.NaN, 0 }, new[] { 0, double.PositiveInfinity }, new[] { double.NegativeInfinity, 0 } };
                foreach (object bad in invalidArrays)
                {
                    var com = MakeLoad(query); com.Columns[column] = bad;
                    using (var wrapper = new OpenStaadWrapper(new QueryRoot(com)))
                        Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, query, new LoadCase(1)), "Column " + column);
                }
            }
        }

        [TestCase("nodal")]
        [TestCase("udl")]
        [TestCase("concentrated")]
        [TestCase("moment")]
        [TestCase("linear")]
        [TestCase("trapezoidal")]
        public void Read_DetectsUnwrittenBuffersDespiteSuccessStatus(string query)
        {
            var com = MakeLoad(query); com.Columns = null;
            using (var wrapper = new OpenStaadWrapper(new QueryRoot(com)))
                Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, query, new LoadCase(1)));
        }

        [TestCase("nodal")]
        [TestCase("udl")]
        [TestCase("concentrated")]
        [TestCase("moment")]
        [TestCase("linear")]
        [TestCase("trapezoidal")]
        public void Read_AcceptsNonZeroSafeArrayBoundsAndDoesNotNormalizeValues(string query)
        {
            var com = MakeLoad(query);
            for (int column = 0; column < com.Columns.Length; column++)
            {
                var source = (Array)com.Columns[column];
                var array = Array.CreateInstance(source.GetType().GetElementType(), new[] { 2 }, new[] { column % 2 == 0 ? -3 : 5 });
                for (int i = 0; i < 2; i++) array.SetValue(source.GetValue(i), array.GetLowerBound(0) + i);
                com.Columns[column] = array;
            }
            com.Count = (short)2; com.PrimaryStatus = -1; com.FetchStatus = (long)0;
            using (var wrapper = new OpenStaadWrapper(new QueryRoot(com)))
            {
                var loads = Read(wrapper.Load, query, new LoadCase(1));
                Assert.That(Values(loads[0])[0], Is.EqualTo(0.25));
                Assert.That(Values(loads[1])[0], Is.EqualTo(-0.5));
            }
        }

        [TestCase("udl", 9)]
        [TestCase("concentrated", 6)]
        [TestCase("moment", 6)]
        [TestCase("linear", 3)]
        [TestCase("trapezoidal", 9)]
        public void Read_PreservesAllSupportedDirectionsAndZeroPositionSentinels(string query, int maximum)
        {
            var com = MakeLoad(query); com.Count = maximum;
            com.Columns[0] = Enumerable.Range(1, maximum).ToArray();
            for (int i = 1; i < com.Columns.Length; i++) com.Columns[i] = new double[maximum];
            using (var wrapper = new OpenStaadWrapper(new QueryRoot(com)))
            {
                var loads = Read(wrapper.Load, query, new LoadCase(1)).Cast<MemberLoad>().ToList();
                Assert.That(loads.Select(x => (int)x.Direction), Is.EqualTo(Enumerable.Range(1, maximum)));
                Assert.That(loads.SelectMany(Values), Is.All.Zero);
            }
        }

        [Test]
        public void Read_LinearVaryingLoadsMapsDistinctNativeStartEndAndMiddleIntensities()
        {
            var com = new QueryLoad { Columns = new object[] { new[] { 1, 3 }, new[] { 11.0, -12.0 }, new[] { 21.0, -22.0 }, new[] { 31.0, -32.0 } } };
            using (var wrapper = new OpenStaadWrapper(new QueryRoot(com)))
            {
                var loads = wrapper.Load.GetMemberLinearVaryingLoads(new LoadCase(1), 42);
                Assert.That(loads.Select(x => x.WStart), Is.EqualTo(new[] { 11.0, -12.0 }));
                Assert.That(loads.Select(x => x.WEnd), Is.EqualTo(new[] { 21.0, -22.0 }));
                Assert.That(loads.Select(x => x.WMiddle), Is.EqualTo(new[] { 31.0, -32.0 }));
            }
        }

        [TestCase("nodal")]
        [TestCase("udl")]
        [TestCase("concentrated")]
        [TestCase("moment")]
        [TestCase("linear")]
        [TestCase("trapezoidal")]
        public void Read_PropagatesComFailuresAndStopsImmediately(string query)
        {
            foreach (string stage in new[] { "primary", "reference", query + "-count", query + "-fetch" })
            {
                var com = MakeLoad(query); com.ThrowAt = stage;
                using (var wrapper = new OpenStaadWrapper(new QueryRoot(com)))
                {
                    var lc = new LoadCase(1, "case", stage == "reference" ? LoadCaseType.ReferenceLoad : LoadCaseType.PrimaryLoad);
                    Assert.That(Assert.Throws<COMException>(() => Read(wrapper.Load, query, lc)), Is.SameAs(com.Failure));
                    Assert.That(com.Calls.Last(), Is.EqualTo(stage + ":" + (stage.EndsWith("count") || stage.EndsWith("fetch") ? 42 : 1)));
                }
            }
        }

        [TestCase("nodal")]
        [TestCase("udl")]
        [TestCase("concentrated")]
        [TestCase("moment")]
        [TestCase("linear")]
        [TestCase("trapezoidal")]
        public void Read_PropagatesMissingComSignature(string query)
        {
            using (var wrapper = new OpenStaadWrapper(new QueryRoot(new object())))
                Assert.Throws<RuntimeBinderException>(() => Read(wrapper.Load, query, new LoadCase(1)));
        }
    }
}
