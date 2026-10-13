using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using NUnit.Framework;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class GroupedLoadPortTests
    {
        private OpenStaadWrapper _wrapper;
        private NativeLoad _native;
        private static Node N(int id) => new Node(id, 0, 0, 0);
        private static Beam B(int id) => new Beam(id, N(1), new Node(2, 5, 0, 0));
        private static MemberLoad Point(double force = -7.25) => new MemberConcentratedLoad(LoadDirection.GlobalY, force, 2.5, -.125);

        [SetUp]
        public void SetUp()
        {
            _native = new NativeLoad();
            _wrapper = new OpenStaadWrapper(new NativeRoot(_native));
        }

        [TearDown]
        public void TearDown() => _wrapper.Dispose();

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(16)]
        public void ThermalOverloadUsesScalarIdsInInputOrderOnTheCallingThread(int nThreads)
        {
            int caller = Thread.CurrentThread.ManagedThreadId;
            var input = new Once<int>(new[] { 103, 101, 103 });
            var result = _wrapper.Load.AddAxialThermalGradientTo(17, input, -15.125, nThreads);
            Assert.That(result, Is.SameAs(_wrapper.Load));
            Assert.That(input.Enumerations, Is.EqualTo(1));
            Assert.That(_native.Calls, Is.EqualTo(new[] { "primary:17", "thermal:103", "thermal:101", "thermal:103" }));
            Assert.That(_native.Records.SelectMany(r => r.Ids), Is.EqualTo(new[] { 103, 101, 103 }));
            Assert.That(_native.Records.All(r => r.ThreadId == caller && r.CaseId == 17 && !r.Reference), Is.True);
            Assert.That(_native.Records.All(r => r.Values.SequenceEqual(new[] { -15.125, 0.0, 0.0 })), Is.True);
            Assert.That(_native.ArrayTemperatureCalls, Is.Zero);
        }

        [Test]
        public void EmptyThermalInputDoesNotActivateAndZeroThermalValueIsAssigned()
        {
            Assert.That(_wrapper.Load.AddAxialThermalGradientTo(17, new int[0], 0, 4), Is.SameAs(_wrapper.Load));
            Assert.That(_native.Calls, Is.Empty);
            _wrapper.Load.AddAxialThermalGradientTo(17, new[] { 101 }, 0, 4);
            Assert.That(_native.Records.Single().Values, Is.EqualTo(new[] { 0.0, 0.0, 0.0 }));
        }

        [TestCase("threads-zero")]
        [TestCase("threads-negative")]
        [TestCase("case-zero")]
        [TestCase("null-sequence")]
        [TestCase("invalid-later-id")]
        [TestCase("nan")]
        [TestCase("positive-infinity")]
        [TestCase("negative-infinity")]
        public void InvalidThermalInputIsRejectedBeforeAnyNativeCall(string kind)
        {
            int nThreads = kind == "threads-zero" ? 0 : kind == "threads-negative" ? -1 : 3;
            int caseId = kind == "case-zero" ? 0 : 17;
            int[] ids = kind == "null-sequence" ? null : kind == "invalid-later-id" ? new[] { 101, 0 } : new[] { 101 };
            double value = kind == "nan" ? double.NaN : kind == "positive-infinity" ? double.PositiveInfinity : kind == "negative-infinity" ? double.NegativeInfinity : 12.5;
            Assert.Catch<ArgumentException>(() => _wrapper.Load.AddAxialThermalGradientTo(caseId, ids, value, nThreads));
            Assert.That(_native.Calls, Is.Empty);
        }

        [Test]
        public void ThermalActivationFailurePreventsEveryAssignment()
        {
            _native.ActivationSucceeds = false;
            Assert.Throws<InvalidOperationException>(() => _wrapper.Load.AddAxialThermalGradientTo(17, new[] { 101, 102 }, 1, 2));
            Assert.That(_native.Calls, Is.EqualTo(new[] { "primary:17" }));
            Assert.That(_native.Records, Is.Empty);
        }

        [Test]
        public void ThermalNativeFailureStopsProcessingAndKeepsEarlierAssignmentAndActivation()
        {
            _native.FailThermalId = 102;
            var exception = Assert.Throws<COMException>(() => _wrapper.Load.AddAxialThermalGradientTo(17, new[] { 101, 102, 103 }, 1, 2));
            Assert.That(exception, Is.SameAs(_native.Failure));
            Assert.That(_native.Calls, Is.EqualTo(new[] { "primary:17", "thermal:101", "thermal:102" }));
            Assert.That(_native.Records.Single().Ids, Is.EqualTo(new[] { 101 }));
            Assert.That(_native.ActiveId, Is.EqualTo(17));
        }

        [Test]
        public void MemberGroupsRoutePrimaryAndReferenceCasesAndDispatchConcreteLoadTypes()
        {
            var primary = new LoadCase(17);
            var reference = new LoadCase(41, "REF", LoadCaseType.ReferenceLoad);
            MemberLoad point = Point();
            point.LoadCase = new LoadCase(999);
            var beam = B(101);
            var groups = new Dictionary<ILoadCase, List<(MemberLoad Load, List<Beam> Beams)>>
            {
                { primary, new List<(MemberLoad Load, List<Beam> Beams)>
                    { (point, new List<Beam> { B(103), null, beam, B(103) }) } },
                { reference, new List<(MemberLoad Load, List<Beam> Beams)>
                    { (new MemberUniformMoment(LoadDirection.LocalZ, -3.5, 1.25, 0, -.25), new List<Beam> { beam }) } }
            };
            Assert.That(_wrapper.Load.AddMemberLoadsFromGroupsByLoadCase(groups), Is.SameAs(_wrapper.Load));
            var first = _native.Records.Single(r => !r.Reference);
            var second = _native.Records.Single(r => r.Reference);
            Assert.That(first.CaseId, Is.EqualTo(17));
            Assert.That(first.Ids, Is.EqualTo(new[] { 103, 101, 103 }));
            Assert.That(first.Direction, Is.EqualTo((int)LoadDirection.GlobalY));
            Assert.That(first.Values, Is.EqualTo(new[] { -7.25, 2.5, -.125 }));
            Assert.That(second.CaseId, Is.EqualTo(41));
            Assert.That(second.Kind, Is.EqualTo("uniform-moment"));
            Assert.That(second.Values, Is.EqualTo(new[] { -3.5, 1.25, 0.0, -.25 }));
            Assert.That(point.LoadCase.Id, Is.EqualTo(999));
            Assert.That(point.Members, Is.Empty);
            Assert.That(beam.MemberLoadItems, Is.Empty);
            Assert.That(groups[primary][0].Beams.Count, Is.EqualTo(4));
            Assert.That(groups[primary][0].Load, Is.SameAs(point));
        }

        [Test]
        public void MemberGroupsPreserveTupleOrderAndSkipNullLoadNullListAndEmptyList()
        {
            var groups = new Dictionary<ILoadCase, List<(MemberLoad Load, List<Beam> Beams)>>
            {
                { new LoadCase(17), new List<(MemberLoad Load, List<Beam> Beams)>
                    {
                        (Point(-1), new List<Beam> { B(101) }),
                        (null, new List<Beam> { B(102) }),
                        (Point(-2), null),
                        (Point(-3), new List<Beam>()),
                        (Point(-4), new List<Beam> { B(104) })
                    }
                }
            };
            _wrapper.Load.AddMemberLoadsFromGroupsByLoadCase(groups);
            Assert.That(_native.Calls, Is.EqualTo(new[] { "primary:17", "point", "primary:17", "point" }));
            Assert.That(_native.Records.Select(r => r.Values[0]), Is.EqualTo(new[] { -1.0, -4.0 }));
        }

        [Test]
        public void MemberAllNullBeamListStillActivatesButDoesNotAssign()
        {
            _wrapper.Load.AddMemberLoadsFromGroupsByLoadCase(new Dictionary<ILoadCase, List<(MemberLoad Load, List<Beam> Beams)>>
            { { new LoadCase(17), new List<(MemberLoad Load, List<Beam> Beams)> { (Point(), new List<Beam> { null }) } } });
            Assert.That(_native.Calls, Is.EqualTo(new[] { "primary:17" }));
            Assert.That(_native.Records, Is.Empty);
        }

        [Test]
        public void NodalGroupsMapAllSixComponentsAndUseKeyInsteadOfLoadMetadata()
        {
            var primary = new LoadCase(17);
            var reference = new LoadCase(41, "REF", LoadCaseType.ReferenceLoad);
            var node = N(10);
            var load = new NodalLoad(1.125, -2.25, 3.5, -4.75, 5.875, -6.125) { LoadCase = new LoadCase(999) };
            var groups = new Dictionary<ILoadCase, List<(NodalLoad Load, List<Node> Nodes)>>
            {
                { primary, new List<(NodalLoad Load, List<Node> Nodes)> { (load, new List<Node> { N(12), null, node, N(12) }) } },
                { reference, new List<(NodalLoad Load, List<Node> Nodes)> { (new NodalLoad(-1, 2, -3, 4, -5, 6), new List<Node> { node }) } }
            };
            Assert.That(_wrapper.Load.AddNodalLoadsFromGroupsByLoadCase(groups), Is.SameAs(_wrapper.Load));
            var first = _native.Records.Single(r => !r.Reference);
            var second = _native.Records.Single(r => r.Reference);
            Assert.That(first.CaseId, Is.EqualTo(17));
            Assert.That(first.Ids, Is.EqualTo(new[] { 12, 10, 12 }));
            Assert.That(first.Values, Is.EqualTo(new[] { 1.125, -2.25, 3.5, -4.75, 5.875, -6.125 }));
            Assert.That(second.CaseId, Is.EqualTo(41));
            Assert.That(second.Values, Is.EqualTo(new[] { -1.0, 2.0, -3.0, 4.0, -5.0, 6.0 }));
            Assert.That(load.LoadCase.Id, Is.EqualTo(999));
            Assert.That(node.NodalLoadItems, Is.Empty);
            Assert.That(groups[primary][0].Nodes.Count, Is.EqualTo(4));
            Assert.That(groups[primary][0].Load, Is.SameAs(load));
        }

        [Test]
        public void NodalEmptyOrAllNullNodesAndNullForcesRetainHelperActivationBehavior()
        {
            var groups = new Dictionary<ILoadCase, List<(NodalLoad Load, List<Node> Nodes)>>
            {
                { new LoadCase(17), new List<(NodalLoad Load, List<Node> Nodes)>
                    {
                        (new NodalLoad(), new List<Node>()),
                        (new NodalLoad(), new List<Node> { null }),
                        (new NodalLoad { Forces = null }, new List<Node> { N(10) })
                    }
                }
            };
            _wrapper.Load.AddNodalLoadsFromGroupsByLoadCase(groups);
            Assert.That(_native.Calls, Is.EqualTo(new[] { "primary:17", "primary:17", "primary:17" }));
            Assert.That(_native.Records, Is.Empty);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void NullAndEmptyGroupedInputsHaveExplicitContracts(bool members)
        {
            if (members)
            {
                Assert.Throws<ArgumentNullException>(() => _wrapper.Load.AddMemberLoadsFromGroupsByLoadCase(null));
                Assert.That(_wrapper.Load.AddMemberLoadsFromGroupsByLoadCase(new Dictionary<ILoadCase, List<(MemberLoad Load, List<Beam> Beams)>>()), Is.SameAs(_wrapper.Load));
                _wrapper.Load.AddMemberLoadsFromGroupsByLoadCase(new Dictionary<ILoadCase, List<(MemberLoad Load, List<Beam> Beams)>> { { new LoadCase(17), new List<(MemberLoad Load, List<Beam> Beams)>() } });
                Assert.Throws<ArgumentException>(() => _wrapper.Load.AddMemberLoadsFromGroupsByLoadCase(new Dictionary<ILoadCase, List<(MemberLoad Load, List<Beam> Beams)>> { { new LoadCase(17), null } }));
            }
            else
            {
                Assert.Throws<ArgumentNullException>(() => _wrapper.Load.AddNodalLoadsFromGroupsByLoadCase(null));
                Assert.That(_wrapper.Load.AddNodalLoadsFromGroupsByLoadCase(new Dictionary<ILoadCase, List<(NodalLoad Load, List<Node> Nodes)>>()), Is.SameAs(_wrapper.Load));
                _wrapper.Load.AddNodalLoadsFromGroupsByLoadCase(new Dictionary<ILoadCase, List<(NodalLoad Load, List<Node> Nodes)>> { { new LoadCase(17), new List<(NodalLoad Load, List<Node> Nodes)>() } });
                Assert.Throws<ArgumentException>(() => _wrapper.Load.AddNodalLoadsFromGroupsByLoadCase(new Dictionary<ILoadCase, List<(NodalLoad Load, List<Node> Nodes)>> { { new LoadCase(17), null } }));
            }
            Assert.That(_native.Calls, Is.Empty);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void NodalInvalidLaterTupleThrowsAfterEarlierAssignmentWithoutAnotherActivation(bool nullLoad)
        {
            var groups = new Dictionary<ILoadCase, List<(NodalLoad Load, List<Node> Nodes)>>
            {
                { new LoadCase(17), new List<(NodalLoad Load, List<Node> Nodes)>
                    {
                        (new NodalLoad(1, 2, 3, 4, 5, 6), new List<Node> { N(10) }),
                        (nullLoad ? null : new NodalLoad(), nullLoad ? new List<Node> { N(11) } : null),
                        (new NodalLoad(), new List<Node> { N(12) })
                    }
                }
            };
            Assert.Throws<ArgumentNullException>(() => _wrapper.Load.AddNodalLoadsFromGroupsByLoadCase(groups));
            Assert.That(_native.Calls, Is.EqualTo(new[] { "primary:17", "nodal" }));
            Assert.That(_native.Records.Single().Ids, Is.EqualTo(new[] { 10 }));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void GroupedNativeFailureStopsLaterTuplesAndLeavesPriorAssignment(bool members)
        {
            _native.FailAssignmentNumber = 2;
            var failure = Assert.Throws<COMException>(() =>
            {
                if (members)
                    _wrapper.Load.AddMemberLoadsFromGroupsByLoadCase(new Dictionary<ILoadCase, List<(MemberLoad Load, List<Beam> Beams)>>
                    { { new LoadCase(17), new List<(MemberLoad Load, List<Beam> Beams)> { (Point(), new List<Beam> { B(101) }), (Point(), new List<Beam> { B(102) }), (Point(), new List<Beam> { B(103) }) } } });
                else
                    _wrapper.Load.AddNodalLoadsFromGroupsByLoadCase(new Dictionary<ILoadCase, List<(NodalLoad Load, List<Node> Nodes)>>
                    { { new LoadCase(17), new List<(NodalLoad Load, List<Node> Nodes)> { (new NodalLoad(), new List<Node> { N(10) }), (new NodalLoad(), new List<Node> { N(11) }), (new NodalLoad(), new List<Node> { N(12) }) } } });
            });
            Assert.That(failure, Is.SameAs(_native.Failure));
            Assert.That(_native.AssignmentAttempts, Is.EqualTo(2));
            Assert.That(_native.Records.Count, Is.EqualTo(1));
            Assert.That(_native.ActiveId, Is.EqualTo(17));
        }

        [Test]
        public void GroupedAssignmentsRetainExistingHelperActivationStatusSemantics()
        {
            _native.ActivationSucceeds = false;
            _wrapper.Load.AddMemberLoadsFromGroupsByLoadCase(new Dictionary<ILoadCase, List<(MemberLoad Load, List<Beam> Beams)>>
            { { new LoadCase(17), new List<(MemberLoad Load, List<Beam> Beams)> { (Point(), new List<Beam> { B(101) }) } } });
            _wrapper.Load.AddNodalLoadsFromGroupsByLoadCase(new Dictionary<ILoadCase, List<(NodalLoad Load, List<Node> Nodes)>>
            { { new LoadCase(17), new List<(NodalLoad Load, List<Node> Nodes)> { (new NodalLoad(), new List<Node> { N(10) }) } } });
            Assert.That(_native.AssignmentAttempts, Is.EqualTo(2));
            Assert.That(_native.Records.All(r => r.CaseId == 0), Is.True);
        }

        public class NativeRoot
        {
            public NativeRoot(NativeLoad load) { Load = load; }
            public object Geometry { get; } = new object();
            public NativeLoad Load { get; }
        }

        public class Record
        {
            public int CaseId;
            public bool Reference;
            public string Kind;
            public int[] Ids;
            public double[] Values;
            public int Direction;
            public int ThreadId;
        }

        public class NativeLoad
        {
            public List<string> Calls { get; } = new List<string>();
            public List<Record> Records { get; } = new List<Record>();
            public int ActiveId { get; private set; }
            public bool Reference { get; private set; }
            public bool ActivationSucceeds { get; set; } = true;
            public int FailThermalId { get; set; }
            public int FailAssignmentNumber { get; set; }
            public int AssignmentAttempts { get; private set; }
            public int ArrayTemperatureCalls { get; private set; }
            public COMException Failure { get; } = new COMException("Native assignment failure");
            public bool SetLoadActive(int id)
            {
                Calls.Add("primary:" + id);
                if (ActivationSucceeds) { ActiveId = id; Reference = false; }
                return ActivationSucceeds;
            }
            public int SetReferenceLoadActive(int id) { Calls.Add("reference:" + id); ActiveId = id; Reference = true; return id; }
            public void AddTemperatureLoad(int id, double axial, double y, double z)
            {
                Calls.Add("thermal:" + id);
                if (id == FailThermalId) throw Failure;
                Capture("thermal", new[] { id }, new[] { axial, y, z });
            }
            public void AddTemperatureLoad(int[] ids, double axial, double y, double z) { ArrayTemperatureCalls++; Capture("thermal-array", ids, new[] { axial, y, z }); }
            public void AddMemberConcForce(int[] ids, int direction, double force, double position, double offset)
            { Calls.Add("point"); Capture("point", ids, new[] { force, position, offset }, direction); }
            public void AddMemberUniformMoment(int[] ids, int direction, double moment, double start, double end, double offset)
            { Calls.Add("uniform-moment"); Capture("uniform-moment", ids, new[] { moment, start, end, offset }, direction); }
            public void AddNodalLoad(int[] ids, double fx, double fy, double fz, double mx, double my, double mz)
            { Calls.Add("nodal"); Capture("nodal", ids, new[] { fx, fy, fz, mx, my, mz }); }
            private void Capture(string kind, int[] ids, double[] values, int direction = 0)
            {
                if (++AssignmentAttempts == FailAssignmentNumber) throw Failure;
                Records.Add(new Record { CaseId = ActiveId, Reference = Reference, Kind = kind, Ids = ids,
                    Values = values, Direction = direction, ThreadId = Thread.CurrentThread.ManagedThreadId });
            }
        }

        private sealed class Once<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> _items;
            public int Enumerations { get; private set; }
            public Once(IEnumerable<T> items) { _items = items; }
            public IEnumerator<T> GetEnumerator()
            {
                if (++Enumerations != 1) throw new InvalidOperationException("Input enumerated twice");
                return _items.GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
