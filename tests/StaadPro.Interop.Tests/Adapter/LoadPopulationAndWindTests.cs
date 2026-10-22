
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Loads.Wind;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class LoadPopulationAndWindTests
    {
        public class WindNative
        {
            public readonly List<string> Calls = new List<string>();
            public object Status = 0;
            public Exception Failure;
            public int TypeId;
            public object First, Second;
            public int CallingThread;
            private object Record(string operation, int id, object first = null, object second = null)
            {
                Calls.Add(operation); TypeId = id; First = first; Second = second;
                CallingThread = System.Threading.Thread.CurrentThread.ManagedThreadId;
                if (Failure != null) throw Failure;
                return Status;
            }
            public object AddWindDefinition(int id, string name) => Record("definition", id, name);
            public object AddWindExposure(int id, double factor, int[] nodes) => Record("exposure", id, factor, nodes);
            public object AddWindIntensity(int id, object intensity, object height) => Record("intensity", id, intensity, height);
            public object DeleteWindDefinition(int id) => Record("delete", id);
        }

        public class PopulationNative
        {
            public readonly List<string> Calls = new List<string>();
            public int Active, FailEntity;
            public bool FailActivation, BadFetch, BadCount, BadValues;
            public int Count = 1;
            public object SetLoadActive(int id)
            {
                Calls.Add("primary:" + id); Active = id;
                return !FailActivation;
            }
            public object SetReferenceLoadActive(int id)
            {
                Calls.Add("reference:" + id); Active = id;
                return FailActivation ? 0 : id;
            }
            private object CountRead(int id, string operation)
            {
                Calls.Add(operation + ":" + id);
                if (id == FailEntity) throw new COMException("Native entity read failed");
                return BadCount ? -1 : Count;
            }
            public object GetUDLLoadCount(int id) => CountRead(id, "udl-count");
            public object GetUNIMomentCount(int id) => CountRead(id, "moment-count");
            public object GetNodalLoadCount(int id) => CountRead(id, "nodal-count");
            private object MemberRead(int id, int directionCode, string operation,
                ref object direction, ref object magnitude, ref object start, ref object end, ref object offset)
            {
                Calls.Add(operation + ":" + id);
                direction = new[] { directionCode };
                magnitude = new[] { BadValues ? double.NaN : -7.123456789123 };
                start = new[] { 1.25 }; end = new[] { 4.75 }; offset = new[] { -.125 };
                return BadFetch ? -1 : 0;
            }
            public object GetUDLLoads(int id, ref object direction, ref object magnitude, ref object start, ref object end, ref object offset)
                => MemberRead(id, 5, "udl", ref direction, ref magnitude, ref start, ref end, ref offset);
            public object GetUNIMoments(int id, ref object direction, ref object magnitude, ref object start, ref object end, ref object offset)
                => MemberRead(id, 9, "moment", ref direction, ref magnitude, ref start, ref end, ref offset);
            public object GetNodalLoads(int id, ref object fx, ref object fy, ref object fz, ref object mx, ref object my, ref object mz)
            {
                Calls.Add("nodal:" + id);
                fx = new[] { BadValues ? double.NaN : 1.25 }; fy = new[] { -2.5 }; fz = new[] { 3.75 };
                mx = new[] { -4.125 }; my = new[] { 5.25 }; mz = new[] { -6.5 };
                return BadFetch ? -1 : 0;
            }
        }

        private static OpenStaadWrapper Wrap(object native) => new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native));
        private static Beam Beam(int id = 101) => new Beam(id, new Node(1, 0, 0, 0), new Node(2, 5, 0, 0));
        private static void Populate(IOSLoad load, string kind, IEnumerable<Beam> beams, IEnumerable<Node> nodes, IEnumerable<ILoadCase> cases)
        {
            if (kind == "udl") load.PopulateMemberUniformlyDistributedLoads(beams, cases);
            else if (kind == "moment") load.PopulateMemberUniformMoments(beams, cases);
            else load.PopulateNodalLoads(nodes, cases);
        }

        [TestCase("udl")]
        [TestCase("moment")]
        public void UniformPopulationAppendsAndPreservesPreciseFieldsCaseIdentityAndOriginalData(string kind)
        {
            var native = new PopulationNative();
            using (var wrapper = Wrap(native))
            {
                var beam = Beam(); var previous = new MemberConcentratedLoad(); beam.MemberLoadItems.Add(previous);
                var loadCase = new LoadCase(41, "REFERENCE", LoadCaseType.ReferenceLoad);
                var beams = new Once<Beam>(new[] { beam }); var cases = new Once<ILoadCase>(new[] { loadCase });
                Populate(wrapper.Load, kind, beams, null, cases);
                Assert.That(beams.Enumerations, Is.EqualTo(1)); Assert.That(cases.Enumerations, Is.EqualTo(1));
                Assert.That(beam.MemberLoadItems.Count, Is.EqualTo(2));
                Assert.That(beam.MemberLoadItems[0], Is.SameAs(previous));
                var item = beam.MemberLoadItems[1];
                Assert.That(item.LoadCase, Is.SameAs(loadCase));
                Assert.That(item.Members, Is.Empty);
                if (kind == "udl")
                {
                    var uniform = (MemberUniformlyDistributedLoad)item;
                    Assert.That(uniform.Magnitude, Is.EqualTo(-7.123456789123));
                    Assert.That(new[] { uniform.SPosition, uniform.EPosition, uniform.Eccentricity }, Is.EqualTo(new[] { 1.25, 4.75, -.125 }));
                    Assert.That(uniform.Direction, Is.EqualTo(LoadDirection.GlobalY));
                }
                else
                {
                    var uniform = (MemberUniformMoment)item;
                    Assert.That(uniform.Magnitude, Is.EqualTo(-7.123456789123));
                    Assert.That(new[] { uniform.SPosition, uniform.EPosition, uniform.Eccentricity }, Is.EqualTo(new[] { 1.25, 4.75, -.125 }));
                    Assert.That(uniform.Direction, Is.EqualTo(LoadDirection.ProjectedZ));
                }
                Assert.That(native.Calls, Is.EqualTo(new[] { "reference:41", kind + "-count:101", kind + ":101" }));
                Populate(wrapper.Load, kind, new[] { beam }, null, new[] { loadCase });
                Assert.That(beam.MemberLoadItems.Count, Is.EqualTo(3));
            }
        }

        [Test]
        public void NodalPopulationEagerlyReplacesAllPriorLoadsPreservesComponentsAndSnapshotsOnce()
        {
            var native = new PopulationNative();
            using (var wrapper = Wrap(native))
            {
                var node = new Node(101, 0, 0, 0);
                var old = new NodalLoad(); node.NodalLoadItems.Add(old);
                var primary = new LoadCase(17); var reference = new LoadCase(41, "REF", LoadCaseType.ReferenceLoad);
                var nodes = new Once<Node>(new[] { node }); var cases = new Once<ILoadCase>(new[] { primary, reference });
                var result = wrapper.Load.PopulateNodalLoads(nodes, cases);
                Assert.That(result, Is.Not.SameAs(nodes));
                Assert.That(result.Single(), Is.SameAs(node));
                Assert.That(nodes.Enumerations, Is.EqualTo(1)); Assert.That(cases.Enumerations, Is.EqualTo(1));
                Assert.That(node.NodalLoadItems.Count, Is.EqualTo(2));
                Assert.That(node.NodalLoadItems, Does.Not.Contain(old));
                Assert.That(node.NodalLoadItems.Select(item => item.LoadCase), Is.EqualTo(new ILoadCase[] { primary, reference }));
                var force = node.NodalLoadItems[0].Forces;
                Assert.That(new[] { force.Fx, force.Fy, force.Fz, force.Mx, force.My, force.Mz }, Is.EqualTo(new[] { 1.25, -2.5, 3.75, -4.125, 5.25, -6.5 }));
                Assert.That(native.Calls, Is.EqualTo(new[] { "primary:17", "nodal-count:101", "nodal:101", "reference:41", "nodal-count:101", "nodal:101" }));
                Assert.That(native.Active, Is.EqualTo(41));
            }
        }

        [TestCase("udl")]
        [TestCase("moment")]
        [TestCase("nodal")]
        public void PopulationPrevalidatesAllInputsBeforeClearOrNativeAccess(string kind)
        {
            var native = new PopulationNative();
            using (var wrapper = Wrap(native))
            {
                var beam = Beam(); var node = new Node(101, 0, 0, 0);
                beam.MemberLoadItems.Add(new MemberConcentratedLoad()); node.NodalLoadItems.Add(new NodalLoad());
                Assert.Throws<ArgumentNullException>(() => Populate(wrapper.Load, kind, null, null, new[] { new LoadCase(17) }));
                Assert.Throws<ArgumentNullException>(() => Populate(wrapper.Load, kind, new[] { beam }, new[] { node }, null));
                Assert.Throws<ArgumentException>(() => Populate(wrapper.Load, kind, new[] { beam, null }, new[] { node, null }, new[] { new LoadCase(17) }));
                Assert.Throws<ArgumentOutOfRangeException>(() => Populate(wrapper.Load, kind, new[] { beam }, new[] { node }, new[] { new LoadCase(0) }));
                Assert.Throws<NotSupportedException>(() => Populate(wrapper.Load, kind, new[] { beam }, new[] { node }, new[] { new LoadCase(17, "BAD", LoadCaseType.LoadCombination) }));
                Assert.That(beam.MemberLoadItems.Count, Is.EqualTo(1)); Assert.That(node.NodalLoadItems.Count, Is.EqualTo(1));
                Assert.That(native.Calls, Is.Empty);
            }
        }

        [TestCase("udl")]
        [TestCase("moment")]
        [TestCase("nodal")]
        public void EmptyCasesAndEmptyEntitiesHaveDocumentedManagedEffectsWithoutNativeReads(string kind)
        {
            var native = new PopulationNative();
            using (var wrapper = Wrap(native))
            {
                var beam = Beam(); var node = new Node(101, 0, 0, 0);
                beam.MemberLoadItems.Add(new MemberConcentratedLoad()); node.NodalLoadItems.Add(new NodalLoad());
                Populate(wrapper.Load, kind, new[] { beam }, new[] { node }, new ILoadCase[0]);
                Assert.That(beam.MemberLoadItems.Count, Is.EqualTo(1));
                Assert.That(node.NodalLoadItems.Count, Is.EqualTo(kind == "nodal" ? 0 : 1));
                Populate(wrapper.Load, kind, new Beam[0], new Node[0], new[] { new LoadCase(17) });
                Assert.That(native.Calls, Is.Empty);
            }
        }

        [TestCase("udl")]
        [TestCase("moment")]
        [TestCase("nodal")]
        public void PopulationPreservesDuplicateEntitiesAndCaseMajorOrder(string kind)
        {
            var native = new PopulationNative();
            using (var wrapper = Wrap(native))
            {
                var beam = Beam(); var node = new Node(101, 0, 0, 0);
                Populate(wrapper.Load, kind, new[] { beam, beam }, new[] { node, node }, new[] { new LoadCase(17), new LoadCase(41) });
                Assert.That(kind == "nodal" ? node.NodalLoadItems.Count : beam.MemberLoadItems.Count, Is.EqualTo(4));
                Assert.That(native.Calls.Where(call => call.StartsWith("primary:")), Is.EqualTo(new[] { "primary:17", "primary:17", "primary:41", "primary:41" }));
            }
        }

        [TestCase("udl")]
        [TestCase("moment")]
        [TestCase("nodal")]
        public void LatePopulationFailureKeepsEarlierResultsAndStopsFurtherProcessing(string kind)
        {
            var native = new PopulationNative { FailEntity = 102 };
            using (var wrapper = Wrap(native))
            {
                var beam = Beam(); var laterBeam = Beam(102); var node = new Node(101, 0, 0, 0); var laterNode = new Node(102, 0, 0, 0);
                node.NodalLoadItems.Add(new NodalLoad()); laterNode.NodalLoadItems.Add(new NodalLoad());
                Assert.Throws<COMException>(() => Populate(wrapper.Load, kind, new[] { beam, laterBeam }, new[] { node, laterNode }, new[] { new LoadCase(17) }));
                Assert.That(kind == "nodal" ? node.NodalLoadItems.Count : beam.MemberLoadItems.Count, Is.EqualTo(1));
                Assert.That(kind == "nodal" ? laterNode.NodalLoadItems.Count : laterBeam.MemberLoadItems.Count, Is.EqualTo(0));
                Assert.That(native.Calls.Last(), Is.EqualTo(kind + "-count:102"));
            }
        }

        [TestCase("udl", "activation")]
        [TestCase("udl", "count")]
        [TestCase("udl", "fetch")]
        [TestCase("udl", "values")]
        [TestCase("moment", "activation")]
        [TestCase("moment", "count")]
        [TestCase("moment", "fetch")]
        [TestCase("moment", "values")]
        [TestCase("nodal", "activation")]
        [TestCase("nodal", "count")]
        [TestCase("nodal", "fetch")]
        [TestCase("nodal", "values")]
        public void PopulationUsesStrictExistingQueryReaders(string kind, string failure)
        {
            var native = new PopulationNative { FailActivation = failure == "activation", BadCount = failure == "count", BadFetch = failure == "fetch", BadValues = failure == "values" };
            using (var wrapper = Wrap(native))
            {
                var beam = Beam(); var node = new Node(101, 0, 0, 0);
                Assert.Throws<InvalidOperationException>(() => Populate(wrapper.Load, kind, new[] { beam }, new[] { node }, new[] { new LoadCase(17) }));
                Assert.That(beam.MemberLoadItems, Is.Empty); Assert.That(node.NodalLoadItems, Is.Empty);
            }
        }


        [TestCase("udl")]
        [TestCase("moment")]
        [TestCase("nodal")]
        public void ZeroNativeCountAddsNothingAndNodalRefreshStillClearsPreviousLoads(string kind)
        {
            var native = new PopulationNative { Count = 0 };
            using (var wrapper = Wrap(native))
            {
                var beam = Beam(); var node = new Node(101, 0, 0, 0);
                var oldMember = new MemberConcentratedLoad(); var oldNodal = new NodalLoad();
                beam.MemberLoadItems.Add(oldMember); node.NodalLoadItems.Add(oldNodal);
                Populate(wrapper.Load, kind, new[] { beam }, new[] { node }, new[] { new LoadCase(17) });
                Assert.That(beam.MemberLoadItems.Single(), Is.SameAs(oldMember));
                Assert.That(node.NodalLoadItems.Count, Is.EqualTo(kind == "nodal" ? 0 : 1));
                Assert.That(native.Calls, Is.EqualTo(new[] { "primary:17", kind + "-count:101" }));
            }
        }

        [TestCase("udl")]
        [TestCase("moment")]
        [TestCase("nodal")]
        public void PopulationEnumerationFailureDoesNotClearExistingManagedLoads(string kind)
        {
            var native = new PopulationNative();
            using (var wrapper = Wrap(native))
            {
                var beam = Beam(); var node = new Node(101, 0, 0, 0);
                var oldMember = new MemberConcentratedLoad(); var oldNodal = new NodalLoad();
                beam.MemberLoadItems.Add(oldMember); node.NodalLoadItems.Add(oldNodal);
                Assert.Throws<InvalidOperationException>(() => Populate(wrapper.Load, kind, new[] { beam }, new[] { node }, FailingCases()));
                Assert.That(beam.MemberLoadItems.Single(), Is.SameAs(oldMember));
                Assert.That(node.NodalLoadItems.Single(), Is.SameAs(oldNodal));
                Assert.That(native.Calls, Is.Empty);
            }
        }

        private static IEnumerable<ILoadCase> FailingCases()
        {
            yield return new LoadCase(17);
            throw new InvalidOperationException("Failed during input snapshot");
        }

        private static int Request(IOSLoad load, string operation)
        {
            switch (operation)
            {
                case "definition": return load.AddWindDefinition(7, "  North 雪 wind  ");
                case "exposure": return load.AddWindExposure(7, 1.25, new[] { 9, 2, 9 });
                case "scalar": return load.AddWindIntensity(7, -1.123456789123, 0);
                case "array": return load.AddWindIntensity(7, new[] { -1.123456789123, 0.0 }, new[] { 15.0, 10.0 });
                default: return load.DeleteWindDefinition(7);
            }
        }

        [TestCase("definition")]
        [TestCase("exposure")]
        [TestCase("scalar")]
        [TestCase("array")]
        [TestCase("delete")]
        public void NativeWindRequestsPreservePreciseArgumentShapesAndDispatchOnceOnCallerThread(string operation)
        {
            var native = new WindNative { Status = -79 };
            using (var wrapper = Wrap(native))
            {
                Assert.That(Request(wrapper.Load, operation), Is.EqualTo(-79));
                Assert.That(native.Calls.Count, Is.EqualTo(1));
                Assert.That(native.TypeId, Is.EqualTo(7));
                Assert.That(native.CallingThread, Is.EqualTo(System.Threading.Thread.CurrentThread.ManagedThreadId));
                if (operation == "definition") Assert.That(native.First, Is.EqualTo("  North 雪 wind  "));
                if (operation == "exposure") { Assert.That(native.First, Is.EqualTo(1.25)); Assert.That(native.Second, Is.TypeOf<int[]>()); Assert.That(native.Second, Is.EqualTo(new[] { 9, 2, 9 })); }
                if (operation == "scalar") { Assert.That(native.First, Is.TypeOf<double>()); Assert.That(native.First, Is.EqualTo(-1.123456789123)); Assert.That(native.Second, Is.EqualTo(0.0)); }
                if (operation == "array") { Assert.That(native.First, Is.TypeOf<double[]>()); Assert.That(native.First, Is.EqualTo(new[] { -1.123456789123, 0.0 })); Assert.That(native.Second, Is.EqualTo(new[] { 15.0, 10.0 })); }
            }
        }

        [Test]
        public void WindCollectionOverloadsSnapshotOncePreserveOrderDuplicatesAndPairing()
        {
            var native = new WindNative();
            using (var wrapper = Wrap(native))
            {
                var ids = new Once<int>(new[] { 5, 3, 5 });
                wrapper.Load.AddWindExposure(7, 0, ids);
                Assert.That(ids.Enumerations, Is.EqualTo(1)); Assert.That(native.Second, Is.EqualTo(new[] { 5, 3, 5 }));
                var first = new Node(5, double.NaN, 0, 0);
                var nodes = new Once<Node>(new[] { first, new Node(3, 0, 0, 0), first });
                wrapper.Load.AddWindExposure(7, 1, nodes);
                Assert.That(nodes.Enumerations, Is.EqualTo(1)); Assert.That(native.Second, Is.EqualTo(new[] { 5, 3, 5 }));
                wrapper.Load.AddWindExposure(7, 1, 99);
                Assert.That(native.Second, Is.EqualTo(new[] { 99 }));
                var pressure = new Once<double>(new[] { -2.0, 3.0, 3.0 }); var heights = new Once<double>(new[] { 10.0, 5.0, 5.0 });
                wrapper.Load.AddWindIntensity(7, pressure, heights);
                Assert.That(pressure.Enumerations, Is.EqualTo(1)); Assert.That(heights.Enumerations, Is.EqualTo(1));
                Assert.That(native.First, Is.EqualTo(new[] { -2.0, 3.0, 3.0 })); Assert.That(native.Second, Is.EqualTo(new[] { 10.0, 5.0, 5.0 }));
                var pairs = new Once<WindIntensityPair>(new[] { new WindIntensityPair(-2, 10), default(WindIntensityPair), new WindIntensityPair(3, 5) });
                wrapper.Load.AddWindIntensity(7, pairs);
                Assert.That(pairs.Enumerations, Is.EqualTo(1));
                Assert.That(native.First, Is.EqualTo(new[] { -2.0, 0.0, 3.0 })); Assert.That(native.Second, Is.EqualTo(new[] { 10.0, 0.0, 5.0 }));
                Assert.That(native.Calls.Count, Is.EqualTo(5));
            }
        }

        [Test]
        public void DeletionDefaultsAndExplicitAllBothSendZeroAndPreserveStatus()
        {
            var native = new WindNative { Status = -8039 };
            using (var wrapper = Wrap(native))
            {
                Assert.That(wrapper.Load.DeleteWindDefinition(), Is.EqualTo(-8039)); Assert.That(native.TypeId, Is.Zero);
                Assert.That(wrapper.Load.DeleteAllWindDefinitions(), Is.EqualTo(-8039)); Assert.That(native.TypeId, Is.Zero);
                Assert.That(native.Calls, Is.EqualTo(new[] { "delete", "delete" }));
            }
        }

        public static IEnumerable<TestCaseData> StatusCases()
        {
            object[] statuses = { (sbyte)-1, (byte)0, (short)-8039, (ushort)7, int.MinValue, int.MaxValue, (uint)42, (long)-19, (ulong)19 };
            foreach (string operation in new[] { "definition", "exposure", "scalar", "array", "delete" })
                foreach (object status in statuses)
                    yield return new TestCaseData(operation, status).SetName("WindStatus_" + operation + "_" + status.GetType().Name + "_" + status);
        }

        [TestCaseSource(nameof(StatusCases))]
        public void WindIntegralStatusesArePreservedWithoutAssumedSuccess(string operation, object status)
        {
            var native = new WindNative { Status = status };
            using (var wrapper = Wrap(native))
                Assert.That(Request(wrapper.Load, operation), Is.EqualTo(Convert.ToInt32(status)));
            Assert.That(native.Calls.Count, Is.EqualTo(1));
        }

        public static IEnumerable<TestCaseData> MalformedStatusCases()
        {
            object[] statuses = { null, true, false, 0.0, 0m, "0", (long)int.MaxValue + 1, ulong.MaxValue, (uint)int.MaxValue + 1, LoadCaseType.PrimaryLoad };
            int index = 0;
            foreach (string operation in new[] { "definition", "exposure", "scalar", "array", "delete" })
                foreach (object status in statuses)
                    yield return new TestCaseData(operation, status).SetName("MalformedWindStatus_" + operation + "_" + index++);
        }

        [TestCaseSource(nameof(MalformedStatusCases))]
        public void MalformedWindStatusesFailAfterOneDispatchWithoutRetry(string operation, object status)
        {
            var native = new WindNative { Status = status };
            using (var wrapper = Wrap(native))
                Assert.Throws<InvalidOperationException>(() => Request(wrapper.Load, operation));
            Assert.That(native.Calls.Count, Is.EqualTo(1));
        }

        [TestCase("definition")]
        [TestCase("exposure")]
        [TestCase("scalar")]
        [TestCase("array")]
        [TestCase("delete")]
        public void WindComFailuresPropagateWithoutRetryOrFallback(string operation)
        {
            var error = new COMException("Disconnected", unchecked((int)0x80010108));
            var native = new WindNative { Failure = error };
            using (var wrapper = Wrap(native))
                Assert.That(Assert.Throws<COMException>(() => Request(wrapper.Load, operation)), Is.SameAs(error));
            Assert.That(native.Calls.Count, Is.EqualTo(1));
        }

        [Test]
        public void InvalidWindInputsAreRejectedBeforeAnyNativeDispatch()
        {
            Action<IOSLoad>[] invalid =
            {
                load => load.AddWindDefinition(0, "Wind"),
                load => load.AddWindDefinition(1, null),
                load => load.AddWindDefinition(1, " \t"),
                load => load.AddWindDefinition(1, "bad\0name"),
                load => load.AddWindExposure(0, 1, new[] { 1 }),
                load => load.AddWindExposure(1, -1, new[] { 1 }),
                load => load.AddWindExposure(1, double.NaN, new[] { 1 }),
                load => load.AddWindExposure(1, double.PositiveInfinity, new[] { 1 }),
                load => load.AddWindExposure(1, 1, (IEnumerable<int>)null),
                load => load.AddWindExposure(1, 1, new int[0]),
                load => load.AddWindExposure(1, 1, new[] { 1, 0 }),
                load => load.AddWindExposure(1, 1, (IEnumerable<Node>)null),
                load => load.AddWindExposure(1, 1, new Node[0]),
                load => load.AddWindExposure(1, 1, new[] { new Node(1, 0, 0, 0), null }),
                load => load.AddWindExposure(1, 1, new[] { new Node(0, 0, 0, 0) }),
                load => load.AddWindExposure(1, 1, 0),
                load => load.AddWindIntensity(0, 1, 1),
                load => load.AddWindIntensity(1, double.NaN, 1),
                load => load.AddWindIntensity(1, double.NegativeInfinity, 1),
                load => load.AddWindIntensity(1, 1, -1),
                load => load.AddWindIntensity(1, 1, double.NaN),
                load => load.AddWindIntensity(1, 1, double.PositiveInfinity),
                load => load.AddWindIntensity(0, new[] { 1.0 }, new[] { 1.0 }),
                load => load.AddWindIntensity(1, (IEnumerable<double>)null, new[] { 1.0 }),
                load => load.AddWindIntensity(1, new[] { 1.0 }, null),
                load => load.AddWindIntensity(1, new double[0], new double[0]),
                load => load.AddWindIntensity(1, new[] { 1.0, 2.0 }, new[] { 1.0 }),
                load => load.AddWindIntensity(1, new[] { 1.0, double.NaN }, new[] { 1.0, 2.0 }),
                load => load.AddWindIntensity(1, new[] { 1.0, 2.0 }, new[] { 1.0, -1.0 }),
                load => load.AddWindIntensity(1, new[] { 1.0 }, new[] { double.PositiveInfinity }),
                load => load.AddWindIntensity(0, new[] { default(WindIntensityPair) }),
                load => load.AddWindIntensity(1, (IEnumerable<WindIntensityPair>)null),
                load => load.AddWindIntensity(1, new WindIntensityPair[0]),
                load => load.DeleteWindDefinition(-1)
            };
            var native = new WindNative();
            using (var wrapper = Wrap(native))
                for (int i = 0; i < invalid.Length; i++)
                {
                    int index = i;
                    Assert.Catch<ArgumentException>(() => invalid[index](wrapper.Load), "Invalid request " + index);
                    Assert.That(native.Calls, Is.Empty, "Invalid request " + index);
                }
        }

        [Test]
        public void EnumerationFailuresCannotReachWindNativeCalls()
        {
            var native = new WindNative();
            using (var wrapper = Wrap(native))
            {
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.AddWindExposure(1, 1, Failing<int>()));
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.AddWindExposure(1, 1, Failing<Node>()));
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.AddWindIntensity(1, Failing<double>(), new[] { 1.0 }));
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.AddWindIntensity(1, new[] { 1.0 }, Failing<double>()));
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.AddWindIntensity(1, Failing<WindIntensityPair>()));
                Assert.That(native.Calls, Is.Empty);
            }
        }

        [Test]
        public void MissingWindMembersPropagateBinderFailure()
        {
            using (var wrapper = Wrap(new object()))
            {
                Assert.Throws<RuntimeBinderException>(() => wrapper.Load.AddWindDefinition(1, "Wind"));
                Assert.Throws<RuntimeBinderException>(() => wrapper.Load.AddWindExposure(1, 1, 1));
                Assert.Throws<RuntimeBinderException>(() => wrapper.Load.AddWindIntensity(1, 1, 1));
                Assert.Throws<RuntimeBinderException>(() => wrapper.Load.DeleteAllWindDefinitions());
            }
        }

        [Test]
        public void WindPairHasImmutableValidatedValueSemanticsAndInvariantText()
        {
            var pair = new WindIntensityPair(-1.25, 10.5); var same = new WindIntensityPair(-1.25, 10.5);
            Assert.That(pair == same, Is.True); Assert.That(pair != same, Is.False);
            Assert.That(pair.Equals((object)same), Is.True); Assert.That(pair.Equals(null), Is.False);
            Assert.That(pair.Equals("not a pair"), Is.False);
            Assert.That(pair.GetHashCode(), Is.EqualTo(same.GetHashCode()));
            Assert.That(pair.ToString(), Is.EqualTo("[Intensity: -1.25, Height: 10.5]"));
            Assert.That(new HashSet<WindIntensityPair> { pair, same }.Count, Is.EqualTo(1));
            Assert.That(default(WindIntensityPair), Is.EqualTo(new WindIntensityPair(0, 0)));
            Assert.Throws<ArgumentOutOfRangeException>(() => new WindIntensityPair(double.NaN, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => new WindIntensityPair(double.PositiveInfinity, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => new WindIntensityPair(1, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new WindIntensityPair(1, double.NaN));
            Assert.Throws<ArgumentOutOfRangeException>(() => new WindIntensityPair(1, double.PositiveInfinity));
        }

        private static IEnumerable<T> Failing<T>()
        {
            yield return default(T);
            throw new InvalidOperationException("Input enumeration failed");
        }

        private sealed class Once<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> source;
            public int Enumerations;
            public Once(IEnumerable<T> source) { this.source = source; }
            public IEnumerator<T> GetEnumerator()
            {
                if (++Enumerations != 1) throw new InvalidOperationException("Input enumerated twice");
                return source.GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
