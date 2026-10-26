using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Media.Media3D;
using NUnit.Framework;
using PackageVerification;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Output;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Output;
namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSOutputPlateCornerTests
    {
        private static Plate Plate(int id = 17) => new Plate(id, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
        private static IEnumerable<TestCaseData> Workflows => Enumerable.Range(0, 25).Select(i => new TestCaseData(i));
        private static IEnumerable<TestCaseData> Versions => new[] { 24, 25, 26 }.SelectMany(v => Enumerable.Range(0, 25).Select(i => new TestCaseData(v, i)));
        private static bool Pure(int i) => i == 15 || i == 16 || i == 22;
        private static object Value(object result, string name) => result.GetType().GetProperty(name).GetValue(result);
        private static object Call(int index, IOSOutput output, OpenStaadWrapper wrapper, PlateCornerForces source = null, IOSGeometry acquiredGeometry = null, IOSLoad acquiredLoad = null)
        {
            ILoadCase loadCase = new LoadCase(1, "Dead");
            var plate = Plate();
            var cornerNode = plate.A;
            var otherMemberNode = plate.B;
            var member = new Beam(18, plate.A, plate.B) { BetaAngle = 30 };
            var geometry = acquiredGeometry ?? wrapper.Geometry;
            var load = acquiredLoad ?? wrapper.Load;
            if (Pure(index) && source == null)
                source = output.GetPlateCornerForces(loadCase, plate, cornerNode).Data;
            switch (index)
            {
                case 0:
                    return output.GetPlateCornerForces(loadCase, plate, cornerNode);
                case 1:
                    return output.GetPlateCornerForces(1, 17, 1);
                case 2:
                    return output.GetPlateCornerForces(1, 17, 1, geometry, load);
                case 3:
                    return output.GetPlateCornerForces(1, 17, 1, LoadCaseType.PrimaryLoad, geometry, load);
                case 4:
                    return output.GetPlateCornerForces(new[] { loadCase }, plate, cornerNode);
                case 5:
                    return output.GetPlateCornerForces(loadCase, plate);
                case 6:
                    return output.GetPlateCornerForces(new[] { loadCase }, plate);
                case 7:
                    return output.GetPlateCornerForces(loadCase, new[] { plate });
                case 8:
                    return output.GetPlateCornerForces(new[] { loadCase }, new[] { plate });
                case 9:
                    return output.GetPlateCornerForces(new[] { new PlateCornerForcesRequest(loadCase, plate, cornerNode) });
                case 10:
                    return output.GetPlateCornerForces(new[] { 1 }, 17, 1, geometry, load);
                case 11:
                    return output.GetPlateCornerForcesInGlobalCoordinates(loadCase, plate, cornerNode);
                case 12:
                    return output.GetPlateCornerForcesGlobal(plate, cornerNode, 1);
                case 13:
                    return output.GetPlateCornerForces(loadCase, plate, cornerNode, otherMemberNode);
                case 14:
                    return output.GetPlateCornerForces(loadCase, plate, cornerNode, member);
                case 15:
                    return output.TransformPlateCornerForcesToMemberLocal(source, otherMemberNode);
                case 16:
                    return output.TransformPlateCornerForcesToMemberLocal(source, member);
                case 17:
                    return output.GetPlateCornerForcesInMemberLocalCoordinates(loadCase, plate, cornerNode, otherMemberNode);
                case 18:
                    return output.GetPlateCornerForces(new PlateCornerForcesRequest(loadCase, plate, cornerNode));
                case 19:
                    return output.GetPlateCornerForces(new[] { 1 }, 17, 1, LoadCaseType.PrimaryLoad, geometry, load);
                case 20:
                    return output.GetPlateCornerForcesInMemberLocalCoordinates(loadCase, plate, cornerNode, member);
                case 21:
                    return output.GetPlateCornerForcesInMemberLocalCoordinates(new[] { new PlateCornerForcesRequest(loadCase, plate, cornerNode) }, member);
                case 22:
                    return output.TransformPlateCornerForcesToMemberLocal(new[] { source }, member);
                case 23:
                    return output.GetPlateCornerForces(new[] { new PlateCornerForcesKey(1, 17, 1) }, geometry, load);
                case 24:
                    return output.GetPlateCornerForces(new[] { new PlateCornerForcesKey(1, 17, 1) }, LoadCaseType.PrimaryLoad, geometry, load);
                default:
                    throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
        private static object[] Arguments(int index, OpenStaadWrapper wrapper, PlateCornerForces source)
        {
            ILoadCase loadCase = new LoadCase(1, "Dead");
            var plate = Plate();
            var cornerNode = plate.A;
            var otherMemberNode = plate.B;
            var member = new Beam(18, plate.A, plate.B) { BetaAngle = 30 };
            switch (index)
            {
                case 0:
                    return new object[] { loadCase, plate, cornerNode };
                case 1:
                    return new object[] { 1, 17, 1 };
                case 2:
                    return new object[] { 1, 17, 1, wrapper.Geometry, wrapper.Load };
                case 3:
                    return new object[] { 1, 17, 1, LoadCaseType.PrimaryLoad, wrapper.Geometry, wrapper.Load };
                case 4:
                    return new object[] { new[] { loadCase }, plate, cornerNode };
                case 5:
                    return new object[] { loadCase, plate };
                case 6:
                    return new object[] { new[] { loadCase }, plate };
                case 7:
                    return new object[] { loadCase, new[] { plate } };
                case 8:
                    return new object[] { new[] { loadCase }, new[] { plate } };
                case 9:
                    return new object[] { new[] { new PlateCornerForcesRequest(loadCase, plate, cornerNode) } };
                case 10:
                    return new object[] { new[] { 1 }, 17, 1, wrapper.Geometry, wrapper.Load };
                case 11:
                    return new object[] { loadCase, plate, cornerNode };
                case 12:
                    return new object[] { plate, cornerNode, 1 };
                case 13:
                    return new object[] { loadCase, plate, cornerNode, otherMemberNode };
                case 14:
                    return new object[] { loadCase, plate, cornerNode, member };
                case 15:
                    return new object[] { source, otherMemberNode };
                case 16:
                    return new object[] { source, member };
                case 17:
                    return new object[] { loadCase, plate, cornerNode, otherMemberNode };
                case 18:
                    return new object[] { new PlateCornerForcesRequest(loadCase, plate, cornerNode) };
                case 19:
                    return new object[] { new[] { 1 }, 17, 1, LoadCaseType.PrimaryLoad, wrapper.Geometry, wrapper.Load };
                case 20:
                    return new object[] { loadCase, plate, cornerNode, member };
                case 21:
                    return new object[] { new[] { new PlateCornerForcesRequest(loadCase, plate, cornerNode) }, member };
                case 22:
                    return new object[] { new[] { source }, member };
                case 23:
                    return new object[] { new[] { new PlateCornerForcesKey(1, 17, 1) }, wrapper.Geometry, wrapper.Load };
                case 24:
                    return new object[] { new[] { new PlateCornerForcesKey(1, 17, 1) }, LoadCaseType.PrimaryLoad, wrapper.Geometry, wrapper.Load };
                default:
                    throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
        private static readonly string[] Names = { "GetPlateCornerForces", "GetPlateCornerForces", "GetPlateCornerForces", "GetPlateCornerForces", "GetPlateCornerForces", "GetPlateCornerForces", "GetPlateCornerForces", "GetPlateCornerForces", "GetPlateCornerForces", "GetPlateCornerForces", "GetPlateCornerForces", "GetPlateCornerForcesInGlobalCoordinates", "GetPlateCornerForcesGlobal", "GetPlateCornerForces", "GetPlateCornerForces", "TransformPlateCornerForcesToMemberLocal", "TransformPlateCornerForcesToMemberLocal", "GetPlateCornerForcesInMemberLocalCoordinates", "GetPlateCornerForces", "GetPlateCornerForces", "GetPlateCornerForcesInMemberLocalCoordinates", "GetPlateCornerForcesInMemberLocalCoordinates", "TransformPlateCornerForcesToMemberLocal", "GetPlateCornerForces", "GetPlateCornerForces" };
        private static readonly Type[][] Types ={
            new[]{typeof(ILoadCase),typeof(Plate),typeof(Node)},
            new[]{typeof(int),typeof(int),typeof(int)},
            new[]{typeof(int),typeof(int),typeof(int),typeof(IOSGeometry),typeof(IOSLoad)},
            new[]{typeof(int),typeof(int),typeof(int),typeof(LoadCaseType),typeof(IOSGeometry),typeof(IOSLoad)},
            new[]{typeof(IEnumerable<ILoadCase>),typeof(Plate),typeof(Node)},
            new[]{typeof(ILoadCase),typeof(Plate)},
            new[]{typeof(IEnumerable<ILoadCase>),typeof(Plate)},
            new[]{typeof(ILoadCase),typeof(IEnumerable<Plate>)},
            new[]{typeof(IEnumerable<ILoadCase>),typeof(IEnumerable<Plate>)},
            new[]{typeof(IEnumerable<PlateCornerForcesRequest>)},
            new[]{typeof(IEnumerable<int>),typeof(int),typeof(int),typeof(IOSGeometry),typeof(IOSLoad)},
            new[]{typeof(ILoadCase),typeof(Plate),typeof(Node)},
            new[]{typeof(Plate),typeof(Node),typeof(int)},
            new[]{typeof(ILoadCase),typeof(Plate),typeof(Node),typeof(Node)},
            new[]{typeof(ILoadCase),typeof(Plate),typeof(Node),typeof(Beam)},
            new[]{typeof(PlateCornerForces),typeof(Node)},
            new[]{typeof(PlateCornerForces),typeof(Beam)},
            new[]{typeof(ILoadCase),typeof(Plate),typeof(Node),typeof(Node)},
            new[]{typeof(PlateCornerForcesRequest)},
            new[]{typeof(IEnumerable<int>),typeof(int),typeof(int),typeof(LoadCaseType),typeof(IOSGeometry),typeof(IOSLoad)},
            new[]{typeof(ILoadCase),typeof(Plate),typeof(Node),typeof(Beam)},
            new[]{typeof(IEnumerable<PlateCornerForcesRequest>),typeof(Beam)},
            new[]{typeof(IEnumerable<PlateCornerForces>),typeof(Beam)},
            new[]{typeof(IEnumerable<PlateCornerForcesKey>),typeof(IOSGeometry),typeof(IOSLoad)},
            new[]{typeof(IEnumerable<PlateCornerForcesKey>),typeof(LoadCaseType),typeof(IOSGeometry),typeof(IOSLoad)}
        };
        [TestCaseSource(nameof(Versions))]
        public void EveryNewContractRetainsOwnedResultsAndCallerThread(int version, int index)
        {
            var root = new OutputPlateCornerFixture.Root { Major = version };
            var n = (OutputPlateCornerFixture.Native)root.Output;
            using (var w = new OpenStaadWrapper(root))
            {
                var r = Call(index, w.Output, w);
                Assert.That(Value(r, "IsSuccessful"), Is.True);
                Assert.That(n.Threads.All(t => t == Thread.CurrentThread.ManagedThreadId), Is.True);
                Assert.That(n.Calls.Count, Is.EqualTo(index >= 5 && index <= 8 ? 4 : 1));
            }
        }
        [TestCaseSource(nameof(Workflows))]
        public void DisposalRejectsAllNewOperationsBeforeFurtherNativeDispatch(int index)
        {
            var root = new OutputPlateCornerFixture.Root();
            var n = (OutputPlateCornerFixture.Native)root.Output;
            var w = new OpenStaadWrapper(root);
            var output = w.Output;
            var source = output.GetPlateCornerForces(new LoadCase(1), Plate(), Plate().A).Data;
            var g = w.Geometry;
            var l = w.Load;
            n.Calls.Clear();
            w.Dispose();
            Assert.Throws<InvalidOperationException>(() => Call(index, output, w, source, g, l));
            Assert.That(n.Calls, Is.Empty);
        }
        [TestCaseSource(nameof(Workflows))]
        public void RefreshRejectsCapturedAdapterEvenForPureTransforms(int index)
        {
            var root = new OutputPlateCornerFixture.Root();
            using (var w = new OpenStaadWrapper(root))
            {
                var old = w.Output;
                var source = old.GetPlateCornerForces(new LoadCase(1), Plate(), Plate().A).Data;
                root.Output = new OutputPlateCornerFixture.Native();
                w.RefreshComObjects();
                Assert.Throws<InvalidOperationException>(() => Call(index, old, w, source));
                Assert.That(((OutputPlateCornerFixture.Native)root.Output).Calls, Is.Empty);
            }
        }
        [TestCaseSource(nameof(Workflows))]
        public void NullOrInvalidParametersFailBeforeNativeOutputCalls(int index)
        {
            var root = new OutputPlateCornerFixture.Root();
            var n = (OutputPlateCornerFixture.Native)root.Output;
            using (var w = new OpenStaadWrapper(root))
            {
                var source = w.Output.GetPlateCornerForces(new LoadCase(1), Plate(), Plate().A).Data;
                n.Calls.Clear();
                var method = typeof(IOSOutput).GetMethod(Names[index], Types[index]);
                for (int p = 0; p < Types[index].Length; p++)
                {
                    var args = Arguments(index, w, source);
                    Type type = Types[index][p];
                    args[p] = type == typeof(int) ? (object)0 : type.IsEnum ? Enum.ToObject(type, 999) : null;
                    var ex = Assert.Throws<TargetInvocationException>(() => method.Invoke(w.Output, args));
                    Assert.That(ex.InnerException, Is.InstanceOf<ArgumentException>(), index + ":" + p);
                    Assert.That(n.Calls, Is.Empty);
                }
            }
        }
        [TestCaseSource(nameof(Workflows))]
        public void NativeFalseRemainsAnIdentifiedFailureWithoutBufferData(int index)
        {
            if (Pure(index))
                return;
            var root = new OutputPlateCornerFixture.Root();
            var n = (OutputPlateCornerFixture.Native)root.Output;
            n.Status = false;
            n.ReplaceArray = a => new object[] { "invalid" };
            using (var w = new OpenStaadWrapper(root))
            {
                var result = Call(index, w.Output, w);
                Assert.That(Value(result, "IsSuccessful"), Is.False);
                if (result.GetType().GetProperty("Data") != null)
                    Assert.That(Assert.Throws<TargetInvocationException>(() => Value(result, "Data")).InnerException, Is.TypeOf<InvalidOperationException>());
                if (result is PlateCornerForcesResultSet set)
                    Assert.That(set.Items.All(r => r.CornerNodeId > 0 && r.LoadCaseId > 0 && r.EntityId > 0), Is.True);
            }
        }
        [TestCaseSource(nameof(Workflows))]
        public void TransportFailureIsPropagatedExactlyAndNotRetried(int index)
        {
            if (Pure(index))
                return;
            var root = new OutputPlateCornerFixture.Root();
            var n = (OutputPlateCornerFixture.Native)root.Output;
            var error = new COMException("transport", unchecked((int)0x80010108));
            n.OnCall = () => throw error;
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.That(Assert.Throws<COMException>(() => Call(index, w.Output, w)), Is.SameAs(error));
                Assert.That(n.Calls, Has.Count.EqualTo(1));
            }
        }
        [Test]
        public void SixGlobalComponentsAreNotRotatedThroughTheSuppliedPlateBasis()
        {
            var root = new OutputPlateCornerFixture.Root();
            var p = Plate();
            p.B = new Node(2, 0, 2, 0);
            p.C = new Node(3, 0, 2, 2);
            p.D = new Node(4, 0, 0, 2);
            using (var w = new OpenStaadWrapper(root))
            {
                var r = w.Output.GetPlateCornerForces(new LoadCase(1, " Ω "), p, p.A);
                Assert.That(r.Data.Components.ForceVector, Is.EqualTo(new Vector3D(1.25, 2.25, 3.25)));
                Assert.That(r.Data.Components.MomentVector, Is.EqualTo(new Vector3D(4.25, 5.25, 6.25)));
                Assert.That(r.Data.CoordinateSystem, Is.EqualTo(ResultCoordinateSystem.Global));
                Assert.That(r.CornerNodeId, Is.EqualTo(1));
                Assert.That(r.LoadCaseType, Is.EqualTo(LoadCaseType.PrimaryLoad));
                Assert.That(r.NativeBooleanReturn, Is.True);
                Assert.That(r.Observations.Single().NativeFunction, Is.EqualTo("GetPlateCornerForces"));
            }
        }
        [Test]
        public void SnapshotsStayStableAfterCallerGeometryMetadataAndNativeBuffersChange()
        {
            var root = new OutputPlateCornerFixture.Root();
            var n = (OutputPlateCornerFixture.Native)root.Output;
            var p = Plate();
            var c = new LoadCase(1, "Original");
            Array buffer = null;
            n.ReplaceArray = a => { buffer = a; for (int i = 0; i < 6; i++) a.SetValue(i + 0.25, i); return a; };
            using (var w = new OpenStaadWrapper(root))
            {
                var r = w.Output.GetPlateCornerForces(c, p, p.A);
                p.Id = 99;
                p.A.X = 100;
                c.Id = 99;
                c.Title = "Changed";
                buffer.SetValue(99.0, 0);
                Assert.That(r.Data.Key.PlateId, Is.EqualTo(17));
                Assert.That(r.Data.LoadCase.Title, Is.EqualTo("Original"));
                Assert.That(r.Data.CornerPosition, Is.EqualTo(new Point3D(0, 0, 0)));
                Assert.That(r.Data.Components.Fx, Is.EqualTo(0.25));
                var copy = r.Data.ToForces();
                copy.Fx = 100;
                Assert.That(r.Data.Components.Fx, Is.EqualTo(0.25));
            }
        }
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void MalformedSuccessfulBuffersNeverEscape(int kind)
        {
            var root = new OutputPlateCornerFixture.Root();
            var n = (OutputPlateCornerFixture.Native)root.Output;
            n.ReplaceArray = a => kind == 0 ? (object)new object[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 } : kind == 1 ? new double[5] : kind == 2 ? new double[1, 6] : kind == 3 ? new[] { 1.0, 2, 3, 4, 5, double.NaN } : kind == 4 ? new[] { 1.0, 2, 3, 4, 5, double.PositiveInfinity } : new float[6];
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.Throws<InvalidOperationException>(() => w.Output.GetPlateCornerForces(new LoadCase(1), Plate(), Plate().A));
            }
        }
        [Test]
        public void NonzeroArrayLowerBoundIsDecodedAndCopiedInNativeOrder()
        {
            var root = new OutputPlateCornerFixture.Root();
            ((OutputPlateCornerFixture.Native)root.Output).ReplaceArray = a => { var b = Array.CreateInstance(typeof(double), new[] { 6 }, new[] { 4 }); for (int i = 0; i < 6; i++) b.SetValue(i + 0.5, i + 4); return b; };
            using (var w = new OpenStaadWrapper(root))
            {
                var r = w.Output.GetPlateCornerForces(new LoadCase(1), Plate(), Plate().A);
                Assert.That(r.Data.Components.Fx, Is.EqualTo(0.5));
                Assert.That(r.Data.Components.Mz, Is.EqualTo(5.5));
            }
        }
        [TestCase(null)]
        [TestCase("true")]
        [TestCase(2)]
        [TestCase(-4001)]
        [TestCase(0.5)]
        public void UndocumentedNativeSuccessRepresentationsAreRejected(object status)
        {
            var root = new OutputPlateCornerFixture.Root();
            ((OutputPlateCornerFixture.Native)root.Output).Status = status;
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.Throws<InvalidOperationException>(() => w.Output.GetPlateCornerForces(new LoadCase(1), Plate(), Plate().A));
            }
        }
        [TestCase(0, false)]
        [TestCase(1, true)]
        [TestCase(-1, true)]
        public void DocumentedBooleanAndVariantTrueStatusAreRetainedAsEvidence(int status, bool success)
        {
            var root = new OutputPlateCornerFixture.Root();
            ((OutputPlateCornerFixture.Native)root.Output).Status = status;
            using (var w = new OpenStaadWrapper(root))
            {
                var r = w.Output.GetPlateCornerForces(new LoadCase(1), Plate(), Plate().A);
                Assert.That(r.IsSuccessful, Is.EqualTo(success));
                Assert.That(r.NativeReturnCode, Is.EqualTo(status));
                Assert.That(r.NativeBooleanReturn, Is.Null);
            }
        }
        [TestCase(0)]
        [TestCase(3)]
        public void RepeatedOrZeroFourthNativeCornerIsTreatedAsTriangle(int fourth)
        {
            var root = new OutputPlateCornerFixture.Root();
            ((OutputPlateCornerFixture.Geometry)root.Geometry).FourthNode = fourth;
            using (var w = new OpenStaadWrapper(root))
            {
                var r = w.Output.GetPlateCornerForces(1, 17, 3);
                Assert.That(r.IsSuccessful, Is.True);
                Assert.That(((OutputPlateCornerFixture.Geometry)root.Geometry).CoordinateCalls, Is.EqualTo(3));
            }
        }
        [TestCase(-4001, OutputQueryStatus.NotFound)]
        [TestCase(-1, OutputQueryStatus.NativeFailure)]
        public void FailedIncidenceStopsBeforeCoordinateOrOutputQueries(int code, OutputQueryStatus status)
        {
            var root = new OutputPlateCornerFixture.Root();
            var g = (OutputPlateCornerFixture.Geometry)root.Geometry;
            g.IncidenceStatus = code;
            using (var w = new OpenStaadWrapper(root))
            {
                var r = w.Output.GetPlateCornerForces(1, 17, 1);
                Assert.That(r.Status, Is.EqualTo(status));
                Assert.That(r.NativeFunction, Is.EqualTo("GetPlateIncidence"));
                Assert.That(r.NativeReturnCode, Is.EqualTo(code));
                Assert.That(g.CoordinateCalls, Is.Zero);
                Assert.That(((OutputPlateCornerFixture.Native)root.Output).Calls, Is.Empty);
            }
        }
        [TestCase(true)]
        [TestCase(false)]
        public void UnwrittenIncidenceOrCoordinatesAreNotHydratedAsZeroGeometry(bool incidence)
        {
            var root = new OutputPlateCornerFixture.Root();
            var g = (OutputPlateCornerFixture.Geometry)root.Geometry;
            if (incidence)
                g.WriteIncidence = false;
            else
                g.WriteCoordinates = false;
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.Throws<InvalidOperationException>(() => w.Output.GetPlateCornerForces(1, 17, 1));
                Assert.That(((OutputPlateCornerFixture.Native)root.Output).Calls, Is.Empty);
            }
        }
        [Test]
        public void AutomaticCaseFamilyAmbiguityRemainsUnresolvedInsteadOfChoosingPrimary()
        {
            var root = new OutputPlateCornerFixture.Root();
            ((OutputPlateCornerFixture.Load)root.Load).ReferenceIds = new[] { 1 };
            using (var w = new OpenStaadWrapper(root))
            {
                var r = w.Output.GetPlateCornerForces(1, 17, 1);
                Assert.That(r.Status, Is.EqualTo(OutputQueryStatus.MetadataUnavailable));
                Assert.That(r.LoadCaseType, Is.Null);
                Assert.That(r.CornerNodeId, Is.EqualTo(1));
                Assert.That(((OutputPlateCornerFixture.Native)root.Output).Calls, Is.Empty);
                var chosen = w.Output.GetPlateCornerForces(1, 17, 1, LoadCaseType.ReferenceLoad, w.Geometry, w.Load);
                Assert.That(chosen.IsSuccessful, Is.True);
                Assert.That(chosen.Data.Key.LoadCaseType, Is.EqualTo(LoadCaseType.ReferenceLoad));
            }
        }
        [Test]
        public void MissingMetadataRemainsAnIndexedFailedObservation()
        {
            using (var w = new OpenStaadWrapper(new OutputPlateCornerFixture.Root()))
            {
                var set = w.Output.GetPlateCornerForces(new[] { 1, 2 }, 17, 1, w.Geometry, w.Load);
                Assert.That(set.Count, Is.EqualTo(2));
                Assert.That(set.FailedCount, Is.EqualTo(1));
                Assert.That(set[new PlateCornerForcesKey(2, 17, 1)].Status, Is.EqualTo(OutputQueryStatus.MetadataUnavailable));
                Assert.That(set.Keys[1].LoadCaseType, Is.Null);
            }
        }
        [Test]
        public void ExplicitFamiliesRemainDistinctAndFamilyFreeIndexLookupRejectsAmbiguity()
        {
            var root = new OutputPlateCornerFixture.Root();
            var p = Plate();
            using (var w = new OpenStaadWrapper(root))
            {
                var set = w.Output.GetPlateCornerForces(new[] { new PlateCornerForcesRequest(new LoadCase(1, "Primary"), p, p.A), new PlateCornerForcesRequest(new LoadCase(1, "Ref", LoadCaseType.ReferenceLoad), p, p.A) });
                Assert.That(set.Count, Is.EqualTo(2));
                Assert.That(set.Keys[0], Is.Not.EqualTo(set.Keys[1]));
                Assert.Throws<InvalidOperationException>(() => set.Find(new PlateCornerForcesKey(1, 17, 1)));
                Assert.That(set.Find(set.Keys[1]), Is.SameAs(set.Items[1]));
                Assert.That(set.GetForLoadCase(1), Has.Count.EqualTo(2));
                Assert.That(set.Find(new PlateCornerForcesKey(99, 17, 1)), Is.Null);
            }
        }
        [Test]
        public void SparseSelectionsDeduplicateBeforeNativeCallsAndContinueOrdinaryFailures()
        {
            var root = new OutputPlateCornerFixture.Root();
            var n = (OutputPlateCornerFixture.Native)root.Output;
            n.Responses.Enqueue(false);
            n.Responses.Enqueue(true);
            var p = Plate();
            var a = new PlateCornerForcesRequest(new LoadCase(1, "D"), p, p.B);
            var b = new PlateCornerForcesRequest(new LoadCase(1, "D"), p, p.A);
            var once = new Once<PlateCornerForcesRequest>(new[] { a, a, b });
            using (var w = new OpenStaadWrapper(root))
            {
                var r = w.Output.GetPlateCornerForces(once);
                Assert.That(once.Count, Is.EqualTo(1));
                Assert.That(r.Keys.Select(k => k.CornerNodeId), Is.EqualTo(new[] { 2, 1 }));
                Assert.That(r.FailedCount, Is.EqualTo(1));
                Assert.That(r.SucceededCount, Is.EqualTo(1));
                Assert.That(n.Calls, Has.Count.EqualTo(2));
                Assert.That(r.GetForPlate(17), Has.Count.EqualTo(2));
                Assert.That(r.GetForCornerNode(1), Has.Count.EqualTo(1));
                Assert.Throws<NotSupportedException>(() => ((IList<OutputQueryResult<PlateCornerForces>>)r.Items).Clear());
                Assert.That(r.ToArray(), Is.EqualTo(r.Items));
                Assert.That(n.Calls, Has.Count.EqualTo(2));
            }
        }
        [Test]
        public void CartesianTriangleSelectionKeepsCasePlateIncidenceOrderAndCoalescesDuplicates()
        {
            var root = new OutputPlateCornerFixture.Root();
            var p = Plate();
            p.D = p.C;
            var cases = new Once<ILoadCase>(new ILoadCase[] { new LoadCase(2, "B"), new LoadCase(1, "A") });
            var plates = new Once<Plate>(new[] { p, p });
            using (var w = new OpenStaadWrapper(root))
            {
                var r = w.Output.GetPlateCornerForces(cases, plates);
                Assert.That(r.Keys.Select(k => k.LoadCaseId), Is.EqualTo(new[] { 2, 2, 2, 1, 1, 1 }));
                Assert.That(r.Keys.Select(k => k.CornerNodeId), Is.EqualTo(new[] { 1, 2, 3, 1, 2, 3 }));
                Assert.That(cases.Count, Is.EqualTo(1));
                Assert.That(plates.Count, Is.EqualTo(1));
            }
        }
        [Test]
        public void ConflictingMetadataOrGeometryStopsBeforeOutputDispatch()
        {
            var root = new OutputPlateCornerFixture.Root();
            var p = Plate();
            var other = Plate();
            other.A.X = 1;
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.Throws<ArgumentException>(() => w.Output.GetPlateCornerForces(new[] { new LoadCase(1, "A"), new LoadCase(1, "B") }, p, p.A));
                Assert.Throws<ArgumentException>(() => w.Output.GetPlateCornerForces(new[] { new PlateCornerForcesRequest(new LoadCase(1, "A"), p, p.A), new PlateCornerForcesRequest(new LoadCase(2, "B"), other, other.A) }));
                Assert.That(((OutputPlateCornerFixture.Native)root.Output).Calls, Is.Empty);
            }
        }
        [Test]
        public void KeySelectionNormalizesAutomaticAndExplicitIdentitiesBeforeExtraction()
        {
            var root = new OutputPlateCornerFixture.Root();
            var n = (OutputPlateCornerFixture.Native)root.Output;
            using (var w = new OpenStaadWrapper(root))
            {
                var set = w.Output.GetPlateCornerForces(new[] { new PlateCornerForcesKey(1, 17, 1), new PlateCornerForcesKey(1, 17, 1, LoadCaseType.PrimaryLoad) }, w.Geometry, w.Load);
                Assert.That(set.Count, Is.EqualTo(1));
                Assert.That(n.Calls, Has.Count.EqualTo(1));
                Assert.That(set[new PlateCornerForcesKey(1, 17, 1)].IsSuccessful, Is.True);
                Assert.Throws<ArgumentException>(() => w.Output.GetPlateCornerForces(new[] { new PlateCornerForcesKey(1, 17, 1, LoadCaseType.ReferenceLoad) }, LoadCaseType.PrimaryLoad, w.Geometry, w.Load));
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Output.GetPlateCornerForces(new[] { default(PlateCornerForcesKey) }, w.Geometry, w.Load));
            }
        }
        [Test]
        public void GeometryIsHydratedOncePerPlateAndMetadataIsDiscoveredOncePerFamily()
        {
            var root = new OutputPlateCornerFixture.Root();
            var l = (OutputPlateCornerFixture.Load)root.Load;
            l.PrimaryIds = new[] { 1, 2 };
            using (var w = new OpenStaadWrapper(root))
            {
                var set = w.Output.GetPlateCornerForces(new[] { 1, 2 }, 17, 1, w.Geometry, w.Load);
                Assert.That(set.IsSuccessful, Is.True);
                Assert.That(((OutputPlateCornerFixture.Geometry)root.Geometry).IncidenceCalls, Is.EqualTo(1));
                Assert.That(l.Calls.Count(c => c == "GetPrimaryLoadCaseNumbers"), Is.EqualTo(1));
            }
        }
        [Test]
        public void AdapterOwnershipAndRebindingProtectHydration()
        {
            var root = new OutputPlateCornerFixture.Root();
            using (var w = new OpenStaadWrapper(root))
            using (var other = new OpenStaadWrapper(new OutputPlateCornerFixture.Root()))
            {
                Assert.Throws<ArgumentException>(() => w.Output.GetPlateCornerForces(1, 17, 1, other.Geometry, w.Load));
                var old = w.Geometry;
                root.Geometry = new OutputPlateCornerFixture.Geometry();
                w.RefreshComObjects();
                Assert.Throws<ArgumentException>(() => w.Output.GetPlateCornerForces(1, 17, 1, old, w.Load));
                ((OutputPlateCornerFixture.Geometry)root.Geometry).OnCall = () => { root.Geometry = new OutputPlateCornerFixture.Geometry(); w.RefreshComObjects(); };
                Assert.Throws<InvalidOperationException>(() => w.Output.GetPlateCornerForces(1, 17, 1));
                Assert.That(((OutputPlateCornerFixture.Native)root.Output).Calls, Is.Empty);
            }
        }
        [Test]
        public void RebindingOutputDuringRetrievalDoesNotReturnMixedData()
        {
            var root = new OutputPlateCornerFixture.Root();
            var n = (OutputPlateCornerFixture.Native)root.Output;
            using (var w = new OpenStaadWrapper(root))
            {
                n.OnCall = () => { root.Output = new OutputPlateCornerFixture.Native(); w.RefreshComObjects(); };
                Assert.Throws<InvalidOperationException>(() => w.Output.GetPlateCornerForces(new LoadCase(1), Plate(), Plate().A));
                Assert.That(n.Calls, Has.Count.EqualTo(1));
            }
        }
        [Test]
        public void EmptySelectionsNeedNoNativeMetadataOrExtractionButStillCheckLifetime()
        {
            var root = new OutputPlateCornerFixture.Root();
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.That(w.Output.GetPlateCornerForces(new PlateCornerForcesRequest[0]).IsSuccessful, Is.True);
                Assert.That(w.Output.GetPlateCornerForces(new PlateCornerForcesKey[0], w.Geometry, w.Load).Count, Is.Zero);
                Assert.That(((OutputPlateCornerFixture.Load)root.Load).Calls, Is.Empty);
                Assert.That(((OutputPlateCornerFixture.Native)root.Output).Calls, Is.Empty);
            }
        }
        [Test]
        public void InfiniteAndOversizedSelectionsAreBoundedBeforeExtraction()
        {
            var root = new OutputPlateCornerFixture.Root();
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.Throws<ArgumentException>(() => w.Output.GetPlateCornerForces(Forever(), 17, 1, w.Geometry, w.Load));
                Assert.Throws<ArgumentException>(() => w.Output.GetPlateCornerForces(Enumerable.Repeat<ILoadCase>(new LoadCase(1), 251), Enumerable.Repeat(Plate(), 100)));
                Assert.That(((OutputPlateCornerFixture.Native)root.Output).Calls, Is.Empty);
            }
        }
        [TestCase(0)]
        [TestCase(90)]
        [TestCase(-30)]
        public void RealMemberTransformUsesBetaAndDoesNotReverseForceOrMomentSigns(double beta)
        {
            var root = new OutputPlateCornerFixture.Root();
            var p = Plate();
            var member = new Beam(18, p.A, p.B) { BetaAngle = beta };
            using (var w = new OpenStaadWrapper(root))
            {
                var raw = w.Output.GetPlateCornerForces(new LoadCase(1), p, p.A);
                var n = (OutputPlateCornerFixture.Native)root.Output;
                n.Calls.Clear();
                var r = w.Output.TransformPlateCornerForcesToMemberLocal(raw.Data, member);
                var basis = w.Output.GetMemberLocalCoordinateSystem(member);
                Assert.That(r.Data.MemberId, Is.EqualTo(18));
                Assert.That(r.Data.MemberEnd, Is.EqualTo(MemberEnd.Start));
                Assert.That((r.Data.Components.ForceVector - basis.ToLocal(raw.Data.Components.ForceVector)).Length, Is.LessThan(1e-12));
                Assert.That((r.Data.Components.MomentVector - basis.ToLocal(raw.Data.Components.MomentVector)).Length, Is.LessThan(1e-12));
                Assert.That(r.Data.Source, Is.SameAs(raw.Data));
                Assert.That(n.Calls, Is.Empty);
                Assert.That(r.Observations, Is.Empty);
            }
        }
        [Test]
        public void EndCornerUsesOriginalMemberIncidenceAndConceptualDirectionHasNoFakeMemberId()
        {
            var p = Plate();
            using (var w = new OpenStaadWrapper(new OutputPlateCornerFixture.Root()))
            {
                var member = new Beam(18, p.A, p.B);
                var atEnd = w.Output.GetPlateCornerForces(new LoadCase(1), p, p.B, member);
                Assert.That(atEnd.Data.MemberEnd, Is.EqualTo(MemberEnd.End));
                Assert.That(atEnd.Data.Components.Fx, Is.EqualTo(1.25));
                var conceptual = w.Output.GetPlateCornerForces(new LoadCase(1), p, p.B, p.A);
                Assert.That(conceptual.Data.MemberId, Is.Null);
                Assert.That(conceptual.Data.MemberEnd, Is.Null);
                Assert.That(conceptual.Data.Components.Fx, Is.EqualTo(-1.25));
            }
        }
        [Test]
        public void NonMemberAndStaleReferencePointsAreRejectedWithoutImplicitMomentTransfer()
        {
            var root = new OutputPlateCornerFixture.Root();
            var p = Plate();
            var bad = new Beam(18, p.C, p.D);
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.Throws<ArgumentException>(() => w.Output.GetPlateCornerForces(new LoadCase(1), p, p.A, bad));
                Assert.That(((OutputPlateCornerFixture.Native)root.Output).Calls, Is.Empty);
                var raw = w.Output.GetPlateCornerForces(new LoadCase(1), p, p.A).Data;
                var stale = new Beam(18, new Node(1, 0, 1, 0), p.B);
                Assert.Throws<ArgumentException>(() => w.Output.TransformPlateCornerForcesToMemberLocal(raw, stale));
            }
        }
        [Test]
        public void TransformCollectionsRetainDuplicateObservationsAndUseNoNativeExtraction()
        {
            var root = new OutputPlateCornerFixture.Root();
            var p = Plate();
            using (var w = new OpenStaadWrapper(root))
            {
                var raw = w.Output.GetPlateCornerForces(new LoadCase(1), p, p.A).Data;
                var n = (OutputPlateCornerFixture.Native)root.Output;
                n.Calls.Clear();
                var r = w.Output.TransformPlateCornerForcesToMemberLocal(new[] { raw, raw }, new Beam(18, p.A, p.B));
                Assert.That(r.Items, Has.Count.EqualTo(2));
                Assert.That(r.Items.All(x => x.IsSuccessful), Is.True);
                Assert.That(n.Calls, Is.Empty);
            }
        }

        [Test]
        public void InvalidDirectionOrReferencePointStopsBeforeNativeOutputExtraction()
        {
            var root = new OutputPlateCornerFixture.Root();
            var p = Plate();
            var n = (OutputPlateCornerFixture.Native)root.Output;
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.Throws<ArgumentException>(() => w.Output.GetPlateCornerForces(new LoadCase(1), p, p.A, new Node(99, 0, 0, 0)));
                Assert.Throws<ArgumentException>(() => w.Output.GetPlateCornerForces(new LoadCase(1), p, p.A, new Beam(18, new Node(1, 0, 1, 0), p.B)));
                Assert.That(n.Calls, Is.Empty);
            }
        }
        [Test]
        public void LoadReplacementDuringDiscoveryInvalidatesWorkflowBeforeOutputCall()
        {
            var root = new OutputPlateCornerFixture.Root();
            var load = (OutputPlateCornerFixture.Load)root.Load;
            using (var w = new OpenStaadWrapper(root))
            {
                load.OnCall = () => { root.Load = new OutputPlateCornerFixture.Load(); w.RefreshComObjects(); };
                Assert.Throws<InvalidOperationException>(() => w.Output.GetPlateCornerForces(1, 17, 1));
                Assert.That(((OutputPlateCornerFixture.Native)root.Output).Calls, Is.Empty);
            }
        }
        [Test]
        public void ExcessiveMetadataDiscoveryStopsBeforeAllocatingOrReadingCaseNumbers()
        {
            var root = new OutputPlateCornerFixture.Root();
            var load = (OutputPlateCornerFixture.Load)root.Load;
            load.PrimaryIds = new int[100001];
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.Throws<InvalidOperationException>(() => w.Output.GetPlateCornerForces(1, 17, 1));
                Assert.That(load.Calls, Is.EqualTo(new[] { "GetPrimaryLoadCaseCount" }));
                Assert.That(((OutputPlateCornerFixture.Native)root.Output).Calls, Is.Empty);
            }
        }
        [Test]
        public void SuccessfulStatusWithPartiallyWrittenForceArrayIsRejected()
        {
            var root = new OutputPlateCornerFixture.Root();
            ((OutputPlateCornerFixture.Native)root.Output).WriteCount = 5;
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.Throws<InvalidOperationException>(() => w.Output.GetPlateCornerForces(new LoadCase(1), Plate(), Plate().A));
            }
        }
        [Test]
        public void BooleanIncidenceStatusDoesNotMasqueradeAsDocumentedZeroSuccess()
        {
            var root = new OutputPlateCornerFixture.Root();
            ((OutputPlateCornerFixture.Geometry)root.Geometry).IncidenceStatus = true;
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.Throws<InvalidOperationException>(() => w.Output.GetPlateCornerForces(1, 17, 1));
            }
        }
        [Test]
        public void ResolvedNonCornerReturnsNotFoundWithoutQueryingNativeOutput()
        {
            var root = new OutputPlateCornerFixture.Root();
            using (var w = new OpenStaadWrapper(root))
            {
                var r = w.Output.GetPlateCornerForces(1, 17, 99);
                Assert.That(r.Status, Is.EqualTo(OutputQueryStatus.NotFound));
                Assert.That(r.CornerNodeId, Is.EqualTo(99));
                Assert.That(r.NativeReturnCode, Is.Null);
                Assert.That(((OutputPlateCornerFixture.Native)root.Output).Calls, Is.Empty);
            }
        }
        [Test]
        public void KeysHaveValueSemanticsAndCaseFamiliesDoNotCollide()
        {
            var a = new PlateCornerForcesKey(1, 17, 1, LoadCaseType.PrimaryLoad);
            var b = new PlateCornerForcesKey(1, 17, 1, LoadCaseType.PrimaryLoad);
            var c = new PlateCornerForcesKey(1, 17, 1, LoadCaseType.ReferenceLoad);
            Assert.That(a == b, Is.True);
            Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
            Assert.That(a != c, Is.True);
            Assert.That(new PlateCornerForcesKey(1, 17, 1).LoadCaseType, Is.Null);
            Assert.Throws<ArgumentOutOfRangeException>(() => new PlateCornerForcesKey(1, 17, 1, LoadCaseType.RepeatLoad));
        }
        private static IEnumerable<int> Forever()
        {
            while (true)
                yield return 1;
        }
        private sealed class Once<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> source; internal int Count; internal Once(IEnumerable<T> source)
            {
                this.source = source;
            }
            public IEnumerator<T> GetEnumerator()
            {
                if (++Count > 1)
                    throw new InvalidOperationException("Reenumerated");
                return source.GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
