using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class ExtendedAdapterPortTests
    {
        private OpenStaadWrapper _wrapper;
        private NativeRoot _native;

        [SetUp]
        public void SetUp()
        {
            _native = new NativeRoot();
            _wrapper = new OpenStaadWrapper(_native);
        }

        [TearDown]
        public void TearDown() => _wrapper.Dispose();

        private static Node N(int id) => new Node(id, id + .125, -id - .25, id + .5);
        private static Beam B(int id, int start = 10, int end = 20) => new Beam(id, N(start), N(end));

        [Test]
        public void RootCapturesOriginalObjectAndWrapperClearsItsBindingOnDispose()
        {
            Assert.That((object)_wrapper.Root.ComObject, Is.SameAs(_native));
            Assert.Throws<ArgumentNullException>(() => new OSRootAdapter(null));
            _wrapper.Dispose();
            Assert.That(_wrapper.Root, Is.Null);
            Assert.That(_wrapper.IsConnected, Is.False);
        }

        [Test]
        public void GeometryMutationsUseSuppliedRootAndReturnReceivingAdapter()
        {
            var other = new NativeRoot();
            var root = new OSRootAdapter(other);
            IOSGeometry geometry = _wrapper.Geometry;
            Assert.That(geometry.CreateMultipleNodesExtended(root, new[] { N(3), N(8) }), Is.SameAs(geometry));
            Assert.That(geometry.CreateMultipleBeamsExtended(root, new[] { B(101, 3, 8) }), Is.SameAs(geometry));
            Assert.That(geometry.MergeNodesExtended(root, N(3), new[] { N(8), N(3) }), Is.SameAs(geometry));
            Assert.That(other.Geometry.Calls, Is.EqualTo(new[] { "nodes", "beam:101", "merge" }));
            Assert.That(_native.Geometry.Calls, Is.Empty);
        }

        [Test]
        public void NodesUseOneEnumerationWithIntegerIdsAndXyzRowsWithoutConversion()
        {
            var input = new Once<Node>(new[] { N(9), N(2) });
            _wrapper.Geometry.CreateMultipleNodesExtended(_wrapper.Root, input);
            Assert.That(input.Enumerations, Is.EqualTo(1));
            Assert.That(_native.Geometry.Ids, Is.EqualTo(new[] { 9, 2 }));
            var coords = _native.Geometry.Coordinates;
            Assert.That(coords.GetLength(0), Is.EqualTo(2));
            Assert.That(coords.GetLength(1), Is.EqualTo(3));
            Assert.That(new[] { coords[0, 0], coords[0, 1], coords[0, 2] }, Is.EqualTo(new[] { 9.125, -9.25, 9.5 }));
            Assert.That(new[] { coords[1, 0], coords[1, 1], coords[1, 2] }, Is.EqualTo(new[] { 2.125, -2.25, 2.5 }));
        }

        [Test]
        public void BeamsCreateInInputOrderWithExplicitIntegerIncidences()
        {
            var input = new Once<Beam>(new[] { B(9, 21, 31), B(2, 31, 41) });
            _wrapper.Geometry.CreateMultipleBeamsExtended(_wrapper.Root, input);
            Assert.That(input.Enumerations, Is.EqualTo(1));
            Assert.That(_native.Geometry.Beams, Is.EqualTo(new[] { (9, 21, 31), (2, 31, 41) }));
        }

        [Test]
        public void BeamValidationCompletesBeforeFirstNativeMutation()
        {
            Assert.Throws<ArgumentException>(() => _wrapper.Geometry.CreateMultipleBeamsExtended(_wrapper.Root,
                new[] { B(1), B(2, 10, 10) }));
            Assert.That(_native.Geometry.Calls, Is.Empty);
        }

        [Test]
        public void BeamCreationStopsAtNativeFailureWithoutRollingBackEarlierCreation()
        {
            _native.Geometry.FailBeamId = 2;
            var failure = Assert.Throws<COMException>(() => _wrapper.Geometry.CreateMultipleBeamsExtended(_wrapper.Root,
                new[] { B(1), B(2), B(3) }));
            Assert.That(failure, Is.SameAs(_native.Geometry.Failure));
            Assert.That(_native.Geometry.Calls, Is.EqualTo(new[] { "beam:1", "beam:2" }));
            Assert.That(_native.Geometry.Beams, Is.EqualTo(new[] { (1, 10, 20) }));
        }

        [Test]
        public void MergeIncludesRetainedIdInInputOrderAndMatchesById()
        {
            Node first = N(8), retainedCopy = N(3);
            var input = new Once<Node>(new[] { first, retainedCopy });
            _wrapper.Geometry.MergeNodesExtended(_wrapper.Root, N(3), input);
            Assert.That(_native.Geometry.RetainedId, Is.EqualTo(3));
            Assert.That(_native.Geometry.Ids, Is.EqualTo(new[] { 8, 3 }));
            Assert.That(input.Enumerations, Is.EqualTo(1));
            Assert.That(first.Id, Is.EqualTo(8));
            Assert.That(retainedCopy.Id, Is.EqualTo(3));
        }

        [Test]
        public void EmptyCreationAndRetainOnlyMergeDoNotAccessNativeRoot()
        {
            var unavailable = new UnavailableRoot();
            Assert.That(_wrapper.Geometry.CreateMultipleNodesExtended(unavailable, new Node[0]), Is.SameAs(_wrapper.Geometry));
            Assert.That(_wrapper.Geometry.CreateMultipleBeamsExtended(unavailable, new Beam[0]), Is.SameAs(_wrapper.Geometry));
            Assert.That(_wrapper.Geometry.MergeNodesExtended(unavailable, N(1), new[] { N(1) }), Is.SameAs(_wrapper.Geometry));
            Assert.That(unavailable.Reads, Is.Zero);
        }

        [TestCase("null-root")]
        [TestCase("null-sequence")]
        [TestCase("null-entry")]
        [TestCase("nonpositive")]
        [TestCase("duplicate")]
        [TestCase("nan")]
        [TestCase("infinity")]
        public void InvalidNodesFailBeforeNativeCalls(string kind)
        {
            var nodes = new[] { N(1), N(2) };
            IOSRoot root = _wrapper.Root;
            if (kind == "null-root") root = null;
            if (kind == "null-sequence") nodes = null;
            if (kind == "null-entry") nodes[1] = null;
            if (kind == "nonpositive") nodes[1].Id = 0;
            if (kind == "duplicate") nodes[1].Id = 1;
            if (kind == "nan") nodes[1].Y = double.NaN;
            if (kind == "infinity") nodes[1].Z = double.PositiveInfinity;
            Assert.Catch<ArgumentException>(() => _wrapper.Geometry.CreateMultipleNodesExtended(root, nodes));
            Assert.That(_native.Geometry.Calls, Is.Empty);
        }

        [TestCase("null-root")]
        [TestCase("null-sequence")]
        [TestCase("null-entry")]
        [TestCase("null-start")]
        [TestCase("null-end")]
        [TestCase("nonpositive")]
        [TestCase("nonpositive-endpoint")]
        [TestCase("duplicate")]
        [TestCase("same-endpoints")]
        public void InvalidBeamsFailBeforeNativeCalls(string kind)
        {
            var beams = new[] { B(1), B(2) };
            IOSRoot root = _wrapper.Root;
            if (kind == "null-root") root = null;
            if (kind == "null-sequence") beams = null;
            if (kind == "null-entry") beams[1] = null;
            if (kind == "null-start") beams[1] = new Beam(2, null, N(20));
            if (kind == "null-end") beams[1] = new Beam(2, N(10), null);
            if (kind == "nonpositive") beams[1].Id = -1;
            if (kind == "nonpositive-endpoint") beams[1].EndNode.Id = 0;
            if (kind == "duplicate") beams[1].Id = 1;
            if (kind == "same-endpoints") beams[1].EndNode.Id = 10;
            Assert.Catch<ArgumentException>(() => _wrapper.Geometry.CreateMultipleBeamsExtended(root, beams));
            Assert.That(_native.Geometry.Calls, Is.Empty);
        }

        [TestCase("null-root")]
        [TestCase("null-retain")]
        [TestCase("null-sequence")]
        [TestCase("null-entry")]
        [TestCase("nonpositive-retain")]
        [TestCase("nonpositive-entry")]
        [TestCase("duplicate")]
        [TestCase("empty")]
        [TestCase("missing-retain")]
        public void InvalidMergeFailsBeforeNativeCalls(string kind)
        {
            Node retain = N(1);
            var nodes = new[] { N(1), N(2) };
            IOSRoot root = _wrapper.Root;
            if (kind == "null-root") root = null;
            if (kind == "null-retain") retain = null;
            if (kind == "null-sequence") nodes = null;
            if (kind == "null-entry") nodes[1] = null;
            if (kind == "nonpositive-retain") retain.Id = 0;
            if (kind == "nonpositive-entry") nodes[1].Id = -1;
            if (kind == "duplicate") nodes[1].Id = 1;
            if (kind == "empty") nodes = new Node[0];
            if (kind == "missing-retain") nodes = new[] { N(2), N(3) };
            Assert.Catch<ArgumentException>(() => _wrapper.Geometry.MergeNodesExtended(root, retain, nodes));
            Assert.That(_native.Geometry.Calls, Is.Empty);
        }

        [Test]
        public void MissingRootGeometryFailsExplicitly()
        {
            Assert.Throws<InvalidOperationException>(() => _wrapper.Geometry.CreateMultipleNodesExtended(new UnavailableRoot(), new[] { N(1) }));
            Assert.Throws<InvalidOperationException>(() => _wrapper.Geometry.CreateMultipleBeamsExtended(new OSRootAdapter(new NativeRoot { Geometry = null }), new[] { B(1) }));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void GeometryBulkAndMergeNativeExceptionsPropagate(bool merge)
        {
            _native.Geometry.FailOther = true;
            var failure = Assert.Throws<COMException>(() =>
            {
                if (merge) _wrapper.Geometry.MergeNodesExtended(_wrapper.Root, N(1), new[] { N(1), N(2) });
                else _wrapper.Geometry.CreateMultipleNodesExtended(_wrapper.Root, new[] { N(1) });
            });
            Assert.That(failure, Is.SameAs(_native.Geometry.Failure));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void FactoredLoadsPreservePairsAndPrecisionAfterPrimaryActivation(bool repeat)
        {
            var pairs = new Dictionary<int, double> { { 41, -1.123456789123 }, { 7, 0 }, { 99, 1.25 } };
            IOSLoad result = repeat ? _wrapper.Load.AddRepeatLoadsTo(17, pairs) : _wrapper.Load.AddReferenceLoadsTo(17, pairs);
            Assert.That(result, Is.SameAs(_wrapper.Load));
            Assert.That(_native.Load.Calls, Is.EqualTo(new[] { "active:17", repeat ? "repeat" : "reference" }));
            Assert.That(_native.Load.Ids, Is.EqualTo(pairs.Select(p => p.Key)));
            Assert.That(_native.Load.Values, Is.EqualTo(pairs.Select(p => p.Value)));
        }

        [Test]
        public void RepeatLoadsUseOnePairEnumerationWithoutSeparateKeysAndValues()
        {
            var pairs = new EnumerationOnlyDictionary { { 41, 1.2 }, { 8, -2.3 } };
            _wrapper.Load.AddRepeatLoadsTo(17, pairs);
            Assert.That(pairs.Enumerations, Is.EqualTo(1));
            Assert.That(_native.Load.Ids, Is.EqualTo(new[] { 41, 8 }));
            Assert.That(_native.Load.Values, Is.EqualTo(new[] { 1.2, -2.3 }));
        }

        [TestCase(-15.125)]
        [TestCase(0.0)]
        [TestCase(22.25)]
        public void ThermalInputIsUnconvertedFirstArgumentWithZeroTransverseValues(double thermal)
        {
            var input = new Once<int>(new[] { 103, 101, 103 });
            Assert.That(_wrapper.Load.AddAxialThermalGradientTo(17, input, thermal), Is.SameAs(_wrapper.Load));
            Assert.That(input.Enumerations, Is.EqualTo(1));
            Assert.That(_native.Load.Calls, Is.EqualTo(new[] { "active:17", "temperature" }));
            Assert.That(_native.Load.Ids, Is.EqualTo(new[] { 103, 101, 103 }));
            Assert.That(_native.Load.Values, Is.EqualTo(new[] { thermal, 0.0, 0.0 }));
        }

        [Test]
        public void EmptyAssignmentsDoNotActivateACase()
        {
            Assert.That(_wrapper.Load.AddReferenceLoadsTo(17, new Dictionary<int, double>()), Is.SameAs(_wrapper.Load));
            Assert.That(_wrapper.Load.AddRepeatLoadsTo(17, new Dictionary<int, double>()), Is.SameAs(_wrapper.Load));
            Assert.That(_wrapper.Load.AddAxialThermalGradientTo(17, new int[0], 1), Is.SameAs(_wrapper.Load));
            Assert.That(_native.Load.Calls, Is.Empty);
        }

        [TestCase("reference")]
        [TestCase("repeat")]
        [TestCase("temperature")]
        public void FailedActivationPreventsAssignment(string kind)
        {
            _native.Load.ActivationSucceeds = false;
            Assert.Throws<InvalidOperationException>(() => Assign(kind));
            Assert.That(_native.Load.Calls, Is.EqualTo(new[] { "active:17" }));
        }

        [TestCase("reference")]
        [TestCase("repeat")]
        [TestCase("temperature")]
        public void AssignmentFailurePropagatesAndLeavesTargetActive(string kind)
        {
            _native.Load.FailAssignment = true;
            var failure = Assert.Throws<COMException>(() => Assign(kind));
            Assert.That(failure, Is.SameAs(_native.Load.Failure));
            Assert.That(_native.Load.ActiveId, Is.EqualTo(17));
            Assert.That(_native.Load.Calls, Is.EqualTo(new[] { "active:17", kind }));
        }

        private void Assign(string kind)
        {
            var pairs = new Dictionary<int, double> { { 41, 1.2 } };
            if (kind == "reference") _wrapper.Load.AddReferenceLoadsTo(17, pairs);
            else if (kind == "repeat") _wrapper.Load.AddRepeatLoadsTo(17, pairs);
            else _wrapper.Load.AddAxialThermalGradientTo(17, new[] { 101 }, 15.5);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void InvalidFactorsAndCaseIdsFailBeforeActivation(bool repeat)
        {
            Action<int, Dictionary<int, double>> assign = (id, pairs) =>
            {
                if (repeat) _wrapper.Load.AddRepeatLoadsTo(id, pairs);
                else _wrapper.Load.AddReferenceLoadsTo(id, pairs);
            };
            Assert.Throws<ArgumentNullException>(() => assign(17, null));
            Assert.Throws<ArgumentOutOfRangeException>(() => assign(0, new Dictionary<int, double>()));
            Assert.Throws<ArgumentOutOfRangeException>(() => assign(17, new Dictionary<int, double> { { 41, 1 }, { 0, 1 } }));
            foreach (double invalid in new[] { double.NaN, double.PositiveInfinity, double.NegativeInfinity })
                Assert.Throws<ArgumentException>(() => assign(17, new Dictionary<int, double> { { 41, 1 }, { 7, invalid } }));
            Assert.That(_native.Load.Calls, Is.Empty);
        }

        [Test]
        public void InvalidThermalInputsFailBeforeActivation()
        {
            Assert.Throws<ArgumentNullException>(() => _wrapper.Load.AddAxialThermalGradientTo(17, null, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => _wrapper.Load.AddAxialThermalGradientTo(-1, new int[0], 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => _wrapper.Load.AddAxialThermalGradientTo(17, new[] { 101, 0 }, 1));
            foreach (double invalid in new[] { double.NaN, double.PositiveInfinity, double.NegativeInfinity })
                Assert.Throws<ArgumentOutOfRangeException>(() => _wrapper.Load.AddAxialThermalGradientTo(17, new[] { 101 }, invalid));
            Assert.That(_native.Load.Calls, Is.Empty);
        }

        public class NativeRoot
        {
            public NativeGeometry Geometry { get; set; } = new NativeGeometry();
            public NativeLoad Load { get; } = new NativeLoad();
        }

        public class NativeGeometry
        {
            public List<string> Calls { get; } = new List<string>();
            public List<(int, int, int)> Beams { get; } = new List<(int, int, int)>();
            public int[] Ids { get; private set; }
            public double[,] Coordinates { get; private set; }
            public int RetainedId { get; private set; }
            public int FailBeamId { get; set; }
            public bool FailOther { get; set; }
            public COMException Failure { get; } = new COMException("Native geometry failure");
            public void CreateMultipleNodes(int[] ids, double[,] coordinates)
            {
                Calls.Add("nodes");
                if (FailOther) throw Failure;
                Ids = ids;
                Coordinates = coordinates;
            }
            public void CreateBeam(int id, int start, int end)
            {
                Calls.Add("beam:" + id);
                if (id == FailBeamId) throw Failure;
                Beams.Add((id, start, end));
            }
            public void MergeNodes(int retain, int[] ids)
            {
                Calls.Add("merge");
                if (FailOther) throw Failure;
                RetainedId = retain;
                Ids = ids;
            }
        }

        public class NativeLoad
        {
            public List<string> Calls { get; } = new List<string>();
            public int ActiveId { get; private set; }
            public bool ActivationSucceeds { get; set; } = true;
            public bool FailAssignment { get; set; }
            public int[] Ids { get; private set; }
            public double[] Values { get; private set; }
            public COMException Failure { get; } = new COMException("Native load failure");
            public bool SetLoadActive(int id) { Calls.Add("active:" + id); ActiveId = id; return ActivationSucceeds; }
            public void AddReferenceLoad(int[] ids, double[] factors) => Assign("reference", ids, factors);
            public void AddRepeatLoad(int[] ids, double[] factors) => Assign("repeat", ids, factors);
            public void AddTemperatureLoad(int[] ids, double axial, double y, double z) => Assign("temperature", ids, new[] { axial, y, z });
            private void Assign(string kind, int[] ids, double[] values)
            {
                Calls.Add(kind);
                if (FailAssignment) throw Failure;
                Ids = ids;
                Values = values;
            }
        }

        public class UnavailableRoot : IOSRoot
        {
            public int Reads { get; private set; }
            public dynamic ComObject { get { Reads++; return null; } }
            public string GetStaadFileFullPath() => throw new NotSupportedException();
            public string GetStaadFileNameOnly() => throw new NotSupportedException();
            public string GetAnlFileFullPath() => throw new NotSupportedException();
            public string GetOpenedStaadFilename() => throw new NotSupportedException();
            public bool IsTarget(string staadFileFullPath) => throw new NotSupportedException();
            public bool ModelIsOpen(string fileFullPath) => throw new NotSupportedException();
            public StaadPro.Interop.Models.ShortJobInfo GetShortJobInfo() => throw new NotSupportedException();
            public StaadPro.Interop.Models.FullJobInfo GetFullJobInfo() => throw new NotSupportedException();
            public IOSRoot SetInputUnitsEx(StaadPro.Interop.Enums.LengthInputUnit lengthUnit, StaadPro.Interop.Enums.ForceInputUnit forceUnit) => throw new NotSupportedException();
            public bool SetSilentMode(StaadPro.Interop.Enums.SilentMode silentMode) => throw new NotSupportedException();
            public IOSRoot SaveModel(StaadPro.Interop.Enums.SilentMode silentMode) => throw new NotSupportedException();
            public IOSRoot CloseSTAADFile() => throw new NotSupportedException();
            public IOSRoot OpenSTAADFile(string filePath) => throw new NotSupportedException();
            public IOSRoot ShowApplication() => throw new NotSupportedException();
        }

        private sealed class Once<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> _items;
            public int Enumerations { get; private set; }
            public Once(IEnumerable<T> items) { _items = items; }
            public IEnumerator<T> GetEnumerator()
            {
                if (++Enumerations != 1) throw new InvalidOperationException("Enumerated twice");
                return _items.GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private sealed class EnumerationOnlyDictionary : Dictionary<int, double>, IDictionary<int, double>
        {
            public int Enumerations { get; private set; }
            ICollection<int> IDictionary<int, double>.Keys => throw new InvalidOperationException("Separate Keys access");
            ICollection<double> IDictionary<int, double>.Values => throw new InvalidOperationException("Separate Values access");
            void ICollection<KeyValuePair<int, double>>.CopyTo(KeyValuePair<int, double>[] array, int index)
            {
                foreach (var pair in (IEnumerable<KeyValuePair<int, double>>)this) array[index++] = pair;
            }
            IEnumerator<KeyValuePair<int, double>> IEnumerable<KeyValuePair<int, double>>.GetEnumerator()
            {
                if (++Enumerations != 1) throw new InvalidOperationException("Enumerated twice");
                return base.GetEnumerator();
            }
        }
    }
}
