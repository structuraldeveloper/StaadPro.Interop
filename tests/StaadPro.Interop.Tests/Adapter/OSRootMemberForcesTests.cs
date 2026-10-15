using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSRootMemberForcesTests
    {
        public delegate object ForceResponder(int beam, int end, int loadCase, ref object buffer);
        public class NativeRoot
        {
            public readonly List<string> Calls = new List<string>();
            public NativeOutput OutputObject = new NativeOutput();
            public NativeLoad LoadObject = new NativeLoad();
            public object Output { get { Calls.Add("output"); return OutputObject; } }
            public object Load { get { Calls.Add("load"); return LoadObject; } }
        }
        public class NativeOutput
        {
            public object Available = true;
            public int AvailabilityCalls, ForceCalls;
            public readonly List<string> Queries = new List<string>();
            public ForceResponder Response;
            public object AreResultsAvailable() { AvailabilityCalls++; return Available; }
            public object GetMemberEndForces(object beam, object end, object loadCase, ref object buffer, object coordinates)
            {
                ForceCalls++;
                Assert.That(beam, Is.TypeOf<int>()); Assert.That(end, Is.TypeOf<int>());
                Assert.That(loadCase, Is.TypeOf<int>()); Assert.That(coordinates, Is.TypeOf<int>().And.EqualTo(0));
                Assert.That(buffer, Is.TypeOf<double[]>());
                Assert.That(((double[])buffer).All(double.IsNaN), Is.True);
                Queries.Add(beam + ":" + end + ":" + loadCase);
                if (Response != null) return Response((int)beam, (int)end, (int)loadCase, ref buffer);
                buffer = new[] { 1d, 2d, 3d, 4d, 5d, 6d }; return true;
            }
        }
        public class NativeLoad
        {
            public readonly List<int> Queries = new List<int>();
            public object Title = "Native title";
            public object GetLoadCaseTitle(int id) { Queries.Add(id); return Title; }
        }
        private class Once : IEnumerable<int>
        {
            private readonly IEnumerable<int> values;
            internal int Reads;
            internal Once(IEnumerable<int> values) { this.values = values; }
            public IEnumerator<int> GetEnumerator() { if (++Reads > 1) throw new InvalidOperationException("Repeated enumeration"); return values.GetEnumerator(); }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Test]
        public void SourceHelperUsesBoundRawObjectsAndStableDistinctInputSnapshots()
        {
            var native = new NativeRoot();
            var beams = new Once(new[] { 2, 1, 2 }); var cases = new Once(new[] { 3, 5, 3 });
            var result = new OSRootAdapter(native).RetrieveMemberForces(beams, cases);
            Assert.That(result.IsSuccessful, Is.True); Assert.That(result.AreResultsAvailable, Is.True);
            Assert.That(beams.Reads, Is.EqualTo(1)); Assert.That(cases.Reads, Is.EqualTo(1));
            Assert.That(native.Calls, Is.EqualTo(new[] { "output", "load" }));
            Assert.That(native.OutputObject.AvailabilityCalls, Is.EqualTo(1));
            Assert.That(native.LoadObject.Queries, Is.EqualTo(new[] { 3, 5 }));
            Assert.That(native.OutputObject.Queries, Is.EqualTo(new[] { "2:0:3", "2:1:3", "1:0:3", "1:1:3", "2:0:5", "2:1:5", "1:0:5", "1:1:5" }));
            Assert.That(result.Queries, Has.Count.EqualTo(8));
            var first = result.Forces[0];
            Assert.That(new[] { first.Fx, first.Fy, first.Fz, first.Mx, first.My, first.Mz }, Is.EqualTo(new[] { 1d,2d,3d,4d,5d,6d }));
            Assert.That(first.BeamId, Is.EqualTo(2)); Assert.That(first.End, Is.EqualTo(MemberEnd.Start));
            Assert.That(first.LoadCaseId, Is.EqualTo(3)); Assert.That(first.LoadCaseTitle, Is.EqualTo("Native title"));
            Assert.Throws<NotSupportedException>(() => ((IList<MemberForces>)result.Forces).Clear());
        }
        [Test]
        public void EqualComponentsDoNotCollapseDifferentPhysicalRecords()
        {
            var result = new OSRootAdapter(new NativeRoot()).RetrieveMemberForces(new[] { 1,2 }, new[] { 1 });
            Assert.That(result.Forces, Has.Count.EqualTo(4));
            Assert.That(new HashSet<MemberForces>(result.Forces), Has.Count.EqualTo(1), "Inherited source equality compares force components.");
            Assert.That(result.Forces[0], Is.Not.SameAs(result.Forces[1]));
        }
        [TestCase(true)] [TestCase(false)]
        public void EmptyRequestsDoNotProbeNativeObjectsOrInventAvailability(bool emptyBeams)
        {
            var result = new OSRootAdapter(new object()).RetrieveMemberForces(emptyBeams ? new int[0] : new[] { 1 }, emptyBeams ? new[] { 1 } : new int[0]);
            Assert.That(result.AreResultsAvailable, Is.Null);
            Assert.That(result.IsSuccessful, Is.True);
            Assert.That(result.Queries, Is.Empty);
        }
        [TestCase(false)] [TestCase(0)]
        public void NativeUnavailableResultsDoNotReadLoadOrForceData(object availability)
        {
            var native = new NativeRoot(); native.OutputObject.Available = availability;
            var result = new OSRootAdapter(native).RetrieveMemberForces(new[] { 1 }, new[] { 1 });
            Assert.That(result.IsSuccessful, Is.False); Assert.That(result.AreResultsAvailable, Is.False);
            Assert.That(result.Queries, Is.Empty); Assert.That(result.Forces, Is.Empty);
            Assert.That(native.Calls, Is.EqualTo(new[] { "output" }));
        }
        [TestCase(null)] [TestCase("1")] [TestCase(2)] [TestCase(1d)]
        public void MalformedAvailabilityDoesNotCertifyResults(object availability)
        {
            var native = new NativeRoot(); native.OutputObject.Available = availability;
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).RetrieveMemberForces(new[] { 1 }, new[] { 1 }));
            Assert.That(native.OutputObject.ForceCalls, Is.Zero);
        }
        [TestCase(true)] [TestCase(false)]
        public void NullInputsAreRejectedBeforeNativeAccess(bool beams)
        {
            var native = new NativeRoot();
            var error = Assert.Throws<ArgumentNullException>(() => new OSRootAdapter(native).RetrieveMemberForces(beams ? null : new[] { 1 }, beams ? new[] { 1 } : null));
            Assert.That(error.ParamName, Is.EqualTo(beams ? "beamIds" : "loadCaseIds"));
            Assert.That(native.Calls, Is.Empty);
        }
        [TestCase(0,true)] [TestCase(-1,true)] [TestCase(0,false)] [TestCase(-1,false)]
        public void InvalidIdentifiersPreventAllNativeAccess(int invalid, bool beams)
        {
            var native = new NativeRoot();
            Assert.Catch<ArgumentException>(() => new OSRootAdapter(native).RetrieveMemberForces(beams ? new[] { 1,invalid } : new[] { 1 }, beams ? new[] { 1 } : new[] { 1,invalid }));
            Assert.That(native.Calls, Is.Empty);
        }
        [Test]
        public void QueryBudgetIsCheckedBeforeNativeObjects()
        {
            var native = new NativeRoot();
            Assert.Throws<ArgumentException>(() => new OSRootAdapter(native).RetrieveMemberForces(Enumerable.Range(1,1000), Enumerable.Range(1,51)));
            Assert.That(native.Calls, Is.Empty);
        }
        [Test]
        public void NativeFalsePreservesIdentityAndNoPartialBufferDataWhileOtherQueriesContinue()
        {
            var native = new NativeRoot();
            native.OutputObject.Response = (int beam, int end, int loadCase, ref object buffer) =>
            {
                if (end == 0) { buffer = "malformed partial data"; return false; }
                buffer = new[] { 1d,2d,3d,4d,5d,6d }; return 1;
            };
            var result = new OSRootAdapter(native).RetrieveMemberForces(new[] { 7 }, new[] { 9 });
            Assert.That(result.IsSuccessful, Is.False); Assert.That(result.AreResultsAvailable, Is.True);
            Assert.That(result.Queries, Has.Count.EqualTo(2)); Assert.That(result.Forces, Has.Count.EqualTo(1));
            Assert.That(result.Queries[0].Forces, Is.Null); Assert.That(result.Queries[0].End, Is.EqualTo(MemberEnd.Start));
            Assert.That(result.Queries[0].BeamId, Is.EqualTo(7)); Assert.That(result.Queries[0].LoadCaseId, Is.EqualTo(9));
        }
        [TestCase(0)] [TestCase(1)] [TestCase(2)] [TestCase(3)] [TestCase(4)] [TestCase(5)]
        public void MalformedSuccessfulForceBuffersRemainExceptions(int shape)
        {
            var native = new NativeRoot();
            native.OutputObject.Response = (int b, int e, int lc, ref object buffer) =>
            {
                if (shape == 0) buffer = null;
                if (shape == 1) buffer = new object[6];
                if (shape == 2) buffer = new double[5];
                if (shape == 3) buffer = new double[2,3];
                if (shape == 4) buffer = new[] { double.PositiveInfinity,0d,0d,0d,0d,0d };
                return true; // shape 5 keeps the NaN sentinels.
            };
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).RetrieveMemberForces(new[] { 1 }, new[] { 1 }));
            Assert.That(native.OutputObject.ForceCalls, Is.EqualTo(1));
        }
        [Test]
        public void AReplacedDoubleArrayWithNonzeroLowerBoundIsMappedInNativeComponentOrder()
        {
            var native = new NativeRoot();
            native.OutputObject.Response = (int b, int e, int lc, ref object buffer) =>
            {
                Array array = Array.CreateInstance(typeof(double), new[] { 6 }, new[] { 1 });
                for (int i = 1; i <= 6; i++) array.SetValue((double)i, i);
                buffer = array; return true;
            };
            Assert.That(new OSRootAdapter(native).RetrieveMemberForces(new[] { 1 }, new[] { 1 }).Forces[0].Mz, Is.EqualTo(6));
        }
        [Test]
        public void NativeReadExceptionPropagatesByIdentityWithoutRetryOrPartialBatch()
        {
            var failure = new COMException("Output disconnected");
            var native = new NativeRoot();
            native.OutputObject.Response = (int b, int e, int lc, ref object buffer) => throw failure;
            Assert.That(Assert.Throws<COMException>(() => new OSRootAdapter(native).RetrieveMemberForces(new[] { 1 }, new[] { 1 })), Is.SameAs(failure));
            Assert.That(native.OutputObject.ForceCalls, Is.EqualTo(1));
        }
        [TestCase(null)] [TestCase(42)]
        public void InvalidLoadTitleDoesNotBecomeInventedMetadata(object title)
        {
            var native = new NativeRoot(); native.LoadObject.Title = title;
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).RetrieveMemberForces(new[] { 1 }, new[] { 1 }));
            Assert.That(native.OutputObject.ForceCalls, Is.Zero);
        }
        [Test]
        public void PortedModelRetainsSourceDescriptionAndEditableManagedValues()
        {
            var model = new MemberForces { BeamId = 7, End = MemberEnd.End, LoadCaseId = 9, LoadCaseTitle = "Wind", Fx = 1, Mz = 6 };
            Assert.That(model.ToString(), Does.Contain("Member 7, End End, Load Case 9 (Wind): Fx=1"));
            model.LoadCaseTitle = " ";
            Assert.That(model.ToString(), Does.Not.Contain("( )"));
        }
    }
}
