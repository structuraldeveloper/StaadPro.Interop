using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Media.Media3D;
using NUnit.Framework;
using PackageVerification;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Output;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Output;
namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSOutputWorkflowTests
    {
        private static Beam Beam(int id = 17) => new Beam(id, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
        private static object Property(object value, string name) => value.GetType().GetProperty(name).GetValue(value);
        private static IEnumerable<TestCaseData> Workflows => Enumerable.Range(0, 25).Select(i => new TestCaseData(i));
        private static IEnumerable<TestCaseData> VersionWorkflows => new[] { 24, 25, 26 }.SelectMany(v => Enumerable.Range(0, 25).Select(i => new TestCaseData(v, i)));
        private static object Call(int index, IOSOutput output, OpenStaadWrapper wrapper, IOSLoad acquiredLoad = null)
        {
            var load = acquiredLoad ?? wrapper.Load;
            var beam = Beam();
            var loadCases = new ILoadCase[] { new LoadCase(1, "Dead") };
            ILoadCase loadCase = loadCases[0];
            var plate = new Plate { Id = 17 };
            switch (index)
            {
                case 0:
                    return output.GetSteelDesignResultsForBeam(beam, loadCases);
                case 1:
                    return output.GetSteelDesignResultsForBeam(load, beam);
                case 2:
                    return output.GetSteelDesignResultsForBeams(new[] { beam }, loadCases);
                case 3:
                    return output.GetSteelDesignResultsForBeams(load, new[] { beam });
                case 4:
                    return output.GetMemberSteelUtilizationRatios(new HashSet<Beam> { beam });
                case 5:
                    return output.PopulateMemberSteelUtilizationRatios(new HashSet<Beam> { beam });
                case 6:
                    return output.GetMemberEndForces("Dead", 17, 1, MemberEnd.Start);
                case 7:
                    return output.GetMemberEndForces("Dead", 17, 1, MemberEnd.Start, ResultCoordinateSystem.Global);
                case 8:
                    return output.GetMemberEndForcesConsideringOffsets("Dead", beam, 1, MemberEnd.Start);
                case 9:
                    return output.GetMemberEndForcesAtPhysicalJoint("Dead", beam, 1, MemberEnd.Start);
                case 10:
                    return output.GetMemberEndForcesConsideringOffsets("Dead", 17, 1, MemberEnd.Start);
                case 11:
                    return output.GetMemberEndForcesAtPhysicalJoint("Dead", 17, 1, MemberEnd.Start);
                case 12:
                    return output.GetMemberLocalCoordinateSystem(beam);
                case 13:
                    return output.GetPlateCenterForces(loadCase, plate);
                case 14:
                    return output.GetPlateCenterMoments(loadCase, plate);
                case 15:
                    return output.GetPlateCenterVonMises(loadCase, plate);
                case 16:
                    return output.GetPlatePrincipalStresses(loadCase, plate);
                case 17:
                    return output.GetPlateCenterResults(loadCase, plate);
                case 18:
                    return output.GetPlateCenterResults(new[] { loadCase }, new[] { plate });
                case 19:
                    return output.GetPlateCenterResults(new[] { loadCase }, new[] { plate }, 2);
                case 20:
                    return output.GetStaticCheckResults(new[] { 1, 2 });
                case 21:
                    return output.GetStaticCheckResults(new[] { 1, 2 }, 2);
                case 22:
                    return output.GetSupportForces(17, 1);
                case 23:
                    return output.GetMemberEndForces(17, MemberEnd.Start, 1, ResultCoordinateSystem.Global);
                case 24:
                    return output.GetMemberSteelUtilizationRatio(beam);
                default:
                    throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
        [TestCaseSource(nameof(VersionWorkflows))]
        public void EveryWorkflowRunsOnCallerApartmentWithQualifiedTypedResults(int year, int index)
        {
            var root = new OutputWorkflowFixture.Root { Major = year };
            using (var w = new OpenStaadWrapper(root))
            {
                object result = Call(index, w.Output, w);
                var n = (OutputWorkflowFixture.Native)root.Output;
                if (result is MemberCoordinateSystem frame)
                {
                    Assert.That(frame.XAxis, Is.EqualTo(new Vector3D(1, 0, 0)));
                    Assert.That(n.Calls, Is.Empty);
                }
                else
                    Assert.That(Property(result, "IsSuccessful"), Is.True);
                Assert.That(n.Threads.All(t => t == Thread.CurrentThread.ManagedThreadId), Is.True);
                Assert.That(n.Calls.Count, Is.EqualTo(index == 12 ? 0 : index == 17 || index == 18 || index == 19 ? 4 : index == 20 || index == 21 ? 2 : 1));
            }
        }
        [TestCaseSource(nameof(Workflows))]
        public void DisposedOutputCannotDispatchAnyWorkflow(int index)
        {
            var root = new OutputWorkflowFixture.Root();
            var w = new OpenStaadWrapper(root);
            var output = w.Output;
            var load = w.Load;
            w.Dispose();
            Assert.Throws<InvalidOperationException>(() => Call(index, output, w, load));
            Assert.That(((OutputWorkflowFixture.Native)root.Output).Calls, Is.Empty);
        }
        [TestCaseSource(nameof(Workflows))]
        public void ReboundOutputCannotDispatchUsingOldAdapter(int index)
        {
            var root = new OutputWorkflowFixture.Root();
            using (var w = new OpenStaadWrapper(root))
            {
                var old = w.Output;
                root.Output = new OutputWorkflowFixture.Native();
                w.RefreshComObjects();
                Assert.Throws<InvalidOperationException>(() => Call(index, old, w));
                Assert.That(((OutputWorkflowFixture.Native)root.Output).Calls, Is.Empty);
            }
        }
        [TestCaseSource(nameof(Workflows))]
        public void TransportExceptionsKeepIdentityAndAbortWithoutRetries(int index)
        {
            if (index == 12)
                return; // Pure managed basis has no native transport.
            var root = new OutputWorkflowFixture.Root();
            var n = (OutputWorkflowFixture.Native)root.Output;
            var original = new COMException("transport", unchecked((int)0x80010108));
            n.OnCall = _ => throw original;
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.That(Assert.Throws<COMException>(() => Call(index, w.Output, w)), Is.SameAs(original));
                Assert.That(n.Calls, Has.Count.EqualTo(1));
            }
        }
        [TestCaseSource(nameof(Workflows))]
        public void OutputReplacementDuringQueryInvalidatesWholeWorkflow(int index)
        {
            if (index == 12)
                return;
            var root = new OutputWorkflowFixture.Root();
            var n = (OutputWorkflowFixture.Native)root.Output;
            using (var w = new OpenStaadWrapper(root))
            {
                n.OnCall = _ => { n.OnCall = null; root.Output = new OutputWorkflowFixture.Native(); w.RefreshComObjects(); };
                Assert.Throws<InvalidOperationException>(() => Call(index, w.Output, w));
                Assert.That(n.Calls, Has.Count.EqualTo(1));
            }
        }
        [Test]
        public void GoverningCaseIsOwnedAndUnknownDesignDecisionIsNotConvertedToFailure()
        {
            var root = new OutputWorkflowFixture.Root();
            var n = (OutputWorkflowFixture.Native)root.Output;
            n.Scalars["designStatus"] = 7;
            var b = Beam();
            var c = new LoadCase(1, "  Dead  ");
            using (var w = new OpenStaadWrapper(root))
            {
                var r = w.Output.GetSteelDesignResultsForBeam(b, new[] { c, c });
                Assert.That(r.IsSuccessful, Is.True);
                Assert.That(r.Data.Beam, Is.SameAs(b));
                Assert.That(r.Data.NativeResults.DesignStatus, Is.EqualTo(SteelDesignStatus.Unknown));
                Assert.That(r.Data.NativeResults.NativeStatusNumber, Is.EqualTo(7));
                c.Title = "changed";
                Assert.That(r.Data.CriticalLoadCase.Title, Is.EqualTo("  Dead  "));
                Assert.That(r.Observations.Single().NativeFunction, Is.EqualTo("GetMemberSteelDesignResults"));
                b.Id = 90;
                Assert.That(r.Data.MemberId, Is.EqualTo(17));
            }
        }
        [Test]
        public void UnresolvedGoverningMetadataDoesNotPublishPartialDesignObject()
        {
            using (var w = new OpenStaadWrapper(new OutputWorkflowFixture.Root()))
            {
                var r = w.Output.GetSteelDesignResultsForBeam(Beam(), new[] { new LoadCase(2, "Other") });
                Assert.That(r.Status, Is.EqualTo(OutputQueryStatus.MetadataUnavailable));
                Assert.Throws<InvalidOperationException>(() => { var data = r.Data; });
                Assert.That(r.Observations.Single().IsSuccessful, Is.True);
            }
        }
        [Test]
        public void ConflictingMetadataAndDuplicateMemberIdsFailBeforeDispatch()
        {
            var root = new OutputWorkflowFixture.Root();
            var n = (OutputWorkflowFixture.Native)root.Output;
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.Throws<ArgumentException>(() => w.Output.GetSteelDesignResultsForBeam(Beam(), new[] { new LoadCase(1, "A"), new LoadCase(1, "B") }));
                Assert.Throws<ArgumentException>(() => w.Output.GetSteelDesignResultsForBeam(Beam(), new[] { new LoadCase(1, "A"), new LoadCase(1, "A", LoadCaseType.ReferenceLoad) }));
                Assert.Throws<ArgumentException>(() => w.Output.GetSteelDesignResultsForBeams(new[] { Beam(), Beam() }, new[] { new LoadCase(1, "A") }));
                Assert.That(n.Calls, Is.Empty);
            }
        }
        [Test]
        public void DiscoveredCasesRequireCurrentLoadAdapterOfSameWrapper()
        {
            var root = new OutputWorkflowFixture.Root();
            using (var w = new OpenStaadWrapper(root))
            using (var other = new OpenStaadWrapper(new OutputWorkflowFixture.Root()))
            {
                Assert.Throws<ArgumentException>(() => w.Output.GetSteelDesignResultsForBeam(other.Load, Beam()));
                var old = w.Load;
                root.Load = new OutputWorkflowFixture.Load();
                w.RefreshComObjects();
                Assert.Throws<ArgumentException>(() => w.Output.GetSteelDesignResultsForBeam(old, Beam()));
                Assert.That(w.Output.GetSteelDesignResultsForBeams(w.Load, new[] { Beam(), Beam(18) }).SucceededCount, Is.EqualTo(2));
                Assert.That(((OutputWorkflowFixture.Load)root.Load).Calls, Is.EqualTo(6));
            }
        }
        [Test]
        public void RatioPopulationStagesAllReadsAndLeavesAllCachesUntouchedOnNativeFailure()
        {
            var root = new OutputWorkflowFixture.Root();
            var n = (OutputWorkflowFixture.Native)root.Output;
            n.Responses["GetMemberSteelDesignRatio"] = new Queue<object>(new object[] { true, false });
            var a = Beam();
            var b = Beam(18);
            a.UtilizationRatio = 0.1;
            b.UtilizationRatio = 0.2;
            using (var w = new OpenStaadWrapper(root))
            {
                var r = w.Output.PopulateMemberSteelUtilizationRatios(new HashSet<Beam> { a, b });
                Assert.That(r.IsSuccessful, Is.False);
                Assert.That(r.WasApplied, Is.False);
                Assert.That(r.AppliedCount, Is.Zero);
                Assert.That(r.Observations.FailedCount, Is.EqualTo(1));
                Assert.That(a.UtilizationRatio, Is.EqualTo(0.1));
                Assert.That(b.UtilizationRatio, Is.EqualTo(0.2));
                Assert.That(n.Calls, Has.Count.EqualTo(2));
            }
        }
        [Test]
        public void SuccessfulPopulationTouchesOnlyManagedCachesAndReadingDoesNotPopulate()
        {
            var root = new OutputWorkflowFixture.Root();
            var a = Beam();
            var b = Beam(18);
            Assert.That(a.UtilizationRatio, Is.Null);
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.That(w.Output.GetMemberSteelUtilizationRatio(a).Data, Is.EqualTo(0.75));
                Assert.That(a.UtilizationRatio, Is.Null);
                var r = w.Output.PopulateMemberSteelUtilizationRatios(new HashSet<Beam> { a, b });
                Assert.That(r.WasApplied, Is.True);
                Assert.That(r.AppliedCount, Is.EqualTo(2));
                Assert.That(a.UtilizationRatio, Is.EqualTo(0.75));
                Assert.That(b.UtilizationRatio, Is.EqualTo(0.75));
                Assert.That(((OutputWorkflowFixture.Native)root.Output).Calls.All(c => c == "GetMemberSteelDesignRatio"), Is.True);
            }
        }
        [Test]
        public void ManagedNotificationExceptionIsNotHiddenAsNativeFailure()
        {
            var root = new OutputWorkflowFixture.Root();
            var a = Beam();
            var error = new InvalidOperationException("observer");
            a.PropertyChanged += (sender, args) => { if (args.PropertyName == "UtilizationRatio") throw error; };
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.That(Assert.Throws<InvalidOperationException>(() => w.Output.PopulateMemberSteelUtilizationRatios(new HashSet<Beam> { a })), Is.SameAs(error));
            }
        }
        [TestCase(MemberEnd.Start)]
        [TestCase(MemberEnd.End)]
        public void OffsetsUseNativeGlobalActionsAndTransferCrossProductWithCorrectSign(MemberEnd end)
        {
            var root = new OutputWorkflowFixture.Root();
            var n = (OutputWorkflowFixture.Native)root.Output;
            using (var w = new OpenStaadWrapper(root))
            {
                var r = w.Output.GetMemberEndForcesConsideringOffsets("  Case Ω  ", Beam(), 1, end);
                Assert.That(r.IsSuccessful, Is.True);
                Assert.That(r.Data.MemberFaceForces.CoordinateSystem, Is.EqualTo(ResultCoordinateSystem.Global));
                Assert.That(n.Arguments.Single().Last(), Is.EqualTo(1));
                Assert.That(r.Data.PhysicalJointForces.Components.ForceVector, Is.EqualTo(new Vector3D(1.25, 2.25, 3.25)));
                Assert.That(r.Data.PhysicalJointForces.Components.MomentVector, Is.EqualTo(new Vector3D(10.75, 5.25, 3.75)));
                Assert.That(r.Data.MemberFace - r.Data.PhysicalJoint, Is.EqualTo(new Vector3D(0, 2, 0)));
                Assert.That(r.Data.PhysicalJointForces.LoadCaseTitle, Is.EqualTo("  Case Ω  "));
                Assert.That(r.Observations.Select(x => x.NativeFunction), Is.EqualTo(new[] { "GetMemberGlobalOffSet", "GetMemberEndForces" }));
                var copy = r.Data.PhysicalJointForces.ToMemberForces();
                copy.Fx = 99;
                Assert.That(r.Data.PhysicalJointForces.Components.Fx, Is.EqualTo(1.25));
            }
        }
        [TestCase(-6027, OutputQueryStatus.Success)]
        [TestCase(-3001, OutputQueryStatus.NotFound)]
        [TestCase(-6026, OutputQueryStatus.NativeFailure)]
        public void MissingOffsetHasExplicitProvenanceAndOtherErrorsNeverBecomeZero(int code, OutputQueryStatus expected)
        {
            var root = new OutputWorkflowFixture.Root();
            ((OutputWorkflowFixture.Property)root.Property).Status = code;
            using (var w = new OpenStaadWrapper(root))
            {
                var r = w.Output.GetMemberEndForcesConsideringOffsets("Dead", 17, 1, MemberEnd.End);
                Assert.That(r.Status, Is.EqualTo(expected));
                if (r.IsSuccessful)
                {
                    Assert.That(r.Data.HasOffset, Is.False);
                    Assert.That(r.Data.OffsetStatus, Is.EqualTo(PropertyQueryStatus.NoSpecification));
                }
                else
                {
                    Assert.That(r.NativeReturnCode, Is.EqualTo(code));
                    Assert.That(((OutputWorkflowFixture.Native)root.Output).Calls, Is.Empty);
                }
            }
        }
        [TestCase(-3001, OutputQueryStatus.NotFound)]
        [TestCase(-1, OutputQueryStatus.NativeFailure)]
        public void FailedNativeIncidenceStopsBeforeCoordinatesOrForces(int status, OutputQueryStatus expected)
        {
            var root = new OutputWorkflowFixture.Root();
            var g = (OutputWorkflowFixture.Geometry)root.Geometry;
            g.Status = status;
            using (var w = new OpenStaadWrapper(root))
            {
                var r = w.Output.GetMemberEndForcesAtPhysicalJoint("Dead", 17, 1, MemberEnd.Start);
                Assert.That(r.Status, Is.EqualTo(expected));
                Assert.That(r.NativeReturnCode, Is.EqualTo(status));
                Assert.That(g.Calls, Is.EqualTo(1));
                Assert.That(((OutputWorkflowFixture.Native)root.Output).Calls, Is.Empty);
            }
        }
        [TestCase(true)]
        [TestCase(false)]
        public void UnwrittenOrBooleanIncidenceCannotCreateZeroGeometry(bool booleanStatus)
        {
            var root = new OutputWorkflowFixture.Root();
            var g = (OutputWorkflowFixture.Geometry)root.Geometry;
            if (booleanStatus)
                g.Status = true;
            else
                g.WriteOutputs = false;
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.Throws<InvalidOperationException>(() => w.Output.GetMemberEndForcesAtPhysicalJoint("Dead", 17, 1, MemberEnd.Start));
                Assert.That(((OutputWorkflowFixture.Native)root.Output).Calls, Is.Empty);
            }
        }
        [Test]
        public void VoidCoordinateReadRetainsWrittenOutputEvidenceWithoutFabricatingBooleanSuccess()
        {
            using (var w = new OpenStaadWrapper(new OutputWorkflowFixture.Root()))
            {
                var r = w.Output.GetMemberEndForcesConsideringOffsets("Dead", 17, 1, MemberEnd.Start);
                Assert.That(r.IsSuccessful, Is.True);
                var coordinates = r.Observations.Where(x => x.NativeFunction == "GetNodeCoordinates").ToArray();
                Assert.That(coordinates, Has.Length.EqualTo(2));
                Assert.That(coordinates.All(x => x.NativeReturnCode == null && x.NativeBooleanReturn == null), Is.True);
            }
        }
        [Test]
        public void PropertyReplacementDuringOffsetQueryRejectsMixedBinding()
        {
            var root = new OutputWorkflowFixture.Root();
            var p = (OutputWorkflowFixture.Property)root.Property;
            using (var w = new OpenStaadWrapper(root))
            {
                p.OnCall = () => { root.Property = new OutputWorkflowFixture.Property(); w.RefreshComObjects(); };
                Assert.Throws<InvalidOperationException>(() => w.Output.GetMemberEndForcesConsideringOffsets("Dead", Beam(), 1, MemberEnd.Start));
                Assert.That(((OutputWorkflowFixture.Native)root.Output).Calls, Is.Empty);
            }
        }
        [Test]
        public void CallerGeometryMutationDuringQueryRejectsMixedResult()
        {
            var root = new OutputWorkflowFixture.Root();
            var b = Beam();
            ((OutputWorkflowFixture.Property)root.Property).OnCall = () => b.StartNode.X = 99;
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.Throws<InvalidOperationException>(() => w.Output.GetMemberEndForcesConsideringOffsets("Dead", b, 1, MemberEnd.Start));
            }
        }
        [TestCase(2, 0, 0, 0)]
        [TestCase(2, 0, 0, 90)]
        [TestCase(0, 2, 0, 0)]
        [TestCase(0, -2, 0, 0)]
        [TestCase(0.000001, 2, 0.000001, 37)]
        [TestCase(1, 2, 3, 1e300)]
        public void ManagedFrameIsOrthonormalRightHandedAndRoundTrips(double x, double y, double z, double beta)
        {
            var b = new Beam(17, new Node(1, 0, 0, 0), new Node(2, x, y, z)) { BetaAngle = beta };
            using (var w = new OpenStaadWrapper(new OutputWorkflowFixture.Root()))
            {
                var r = w.Output.GetMemberLocalCoordinateSystem(b);
                Assert.That(r.XAxis.Length, Is.EqualTo(1).Within(1e-12));
                Assert.That(r.YAxis.Length, Is.EqualTo(1).Within(1e-12));
                Assert.That(r.ZAxis.Length, Is.EqualTo(1).Within(1e-12));
                Assert.That(Vector3D.DotProduct(r.XAxis, r.YAxis), Is.EqualTo(0).Within(1e-12));
                Assert.That((Vector3D.CrossProduct(r.XAxis, r.YAxis) - r.ZAxis).Length, Is.LessThan(1e-12));
                var v = new Vector3D(2, 3, 4);
                Assert.That((r.ToGlobal(r.ToLocal(v)) - v).Length, Is.LessThan(1e-12));
            }
        }
        [Test]
        public void FrameBetaConventionAndDegenerateGeometryAreExplicit()
        {
            var b = Beam();
            b.BetaAngle = 90;
            using (var w = new OpenStaadWrapper(new OutputWorkflowFixture.Root()))
            {
                var r = w.Output.GetMemberLocalCoordinateSystem(b);
                Assert.That((r.YAxis - new Vector3D(0, 0, 1)).Length, Is.LessThan(1e-12));
                b.EndNode.X = 0;
                Assert.Throws<ArgumentException>(() => w.Output.GetMemberLocalCoordinateSystem(b));
                b.EndNode.X = 2;
                b.BetaAngle = double.NaN;
                Assert.Throws<ArgumentException>(() => w.Output.GetMemberLocalCoordinateSystem(b));
                Assert.Throws<ArgumentException>(() => r.ToLocal(new Vector3D(double.PositiveInfinity, 0, 0)));
            }
        }
        [Test]
        public void PlateCompositeRetainsAllFamilyFailuresAndChoosesFirstFailureWithoutData()
        {
            var root = new OutputWorkflowFixture.Root();
            var n = (OutputWorkflowFixture.Native)root.Output;
            n.Responses["GetAllPlateCenterMoments"] = new Queue<object>(new object[] { false });
            using (var w = new OpenStaadWrapper(root))
            {
                var r = w.Output.GetPlateCenterResults(new LoadCase(1, "Dead"), new Plate { Id = 17 });
                Assert.That(r.Status, Is.EqualTo(OutputQueryStatus.NativeFailure));
                Assert.That(r.NativeFunction, Is.EqualTo("GetAllPlateCenterMoments"));
                Assert.That(r.Observations, Has.Count.EqualTo(4));
                Assert.That(n.Calls, Has.Count.EqualTo(4));
                Assert.Throws<InvalidOperationException>(() => { var d = r.Data; });
            }
        }
        [Test]
        public void CartesianBatchSnapshotsOnceKeepsOrderDuplicatesAndPerPairIdentities()
        {
            var root = new OutputWorkflowFixture.Root();
            var cases = new Once<ILoadCase>(new ILoadCase[] { new LoadCase(2, "B"), new LoadCase(1, "A"), new LoadCase(1, "A") });
            var plates = new Once<Plate>(new[] { new Plate { Id = 18 }, new Plate { Id = 17 } });
            using (var w = new OpenStaadWrapper(root))
            {
                var r = w.Output.GetPlateCenterResults(cases, plates, 4);
                Assert.That(r.Items.Select(i => i.LoadCaseId), Is.EqualTo(new[] { 2, 2, 1, 1, 1, 1 }));
                Assert.That(r.Items.Select(i => i.EntityId), Is.EqualTo(new[] { 18, 17, 18, 17, 18, 17 }));
                Assert.That(cases.Count, Is.EqualTo(1));
                Assert.That(plates.Count, Is.EqualTo(1));
                Assert.Throws<NotSupportedException>(() => ((IList<OutputQueryResult<PlateCenterResults>>)r.Items).Clear());
            }
        }
        [Test]
        public void StaticBatchKeepsDuplicateEqualObservationsAndContinuesOrdinaryFailures()
        {
            var root = new OutputWorkflowFixture.Root();
            var n = (OutputWorkflowFixture.Native)root.Output;
            n.Responses["GetStaticCheckResult"] = new Queue<object>(new object[] { false, true, true });
            using (var w = new OpenStaadWrapper(root))
            {
                var r = w.Output.GetStaticCheckResults(new[] { 1, 2, 2 }, 3);
                Assert.That(r.Items.Select(x => x.LoadCaseId), Is.EqualTo(new[] { 1, 2, 2 }));
                Assert.That(r.FailedCount, Is.EqualTo(1));
                Assert.That(r.SucceededCount, Is.EqualTo(2));
                Assert.That(n.Calls, Has.Count.EqualTo(3));
            }
        }
        [TestCase(0)]
        [TestCase(65)]
        public void InvalidSchedulingHintFailsBeforeDispatch(int degree)
        {
            var root = new OutputWorkflowFixture.Root();
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Output.GetStaticCheckResults(new[] { 1 }, degree));
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Output.GetPlateCenterResults(new[] { new LoadCase(1, "A") }, new[] { new Plate { Id = 17 } }, degree));
                Assert.That(((OutputWorkflowFixture.Native)root.Output).Calls, Is.Empty);
            }
        }
        [Test]
        public void OversizedCartesianProductAndInfiniteSelectionsAreBoundedBeforeDispatch()
        {
            var root = new OutputWorkflowFixture.Root();
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.Throws<ArgumentException>(() => w.Output.GetPlateCenterResults(Enumerable.Repeat<ILoadCase>(new LoadCase(1, "A"), 317), Enumerable.Repeat(new Plate { Id = 17 }, 317)));
                Assert.Throws<ArgumentException>(() => w.Output.GetStaticCheckResults(Forever()));
                Assert.That(((OutputWorkflowFixture.Native)root.Output).Calls, Is.Empty);
            }
        }
        [Test]
        public void EmptySelectionsAreSuccessfulAndPerformNoNativeDiscoveryOrWrites()
        {
            var root = new OutputWorkflowFixture.Root();
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.That(w.Output.GetSteelDesignResultsForBeams(w.Load, new Beam[0]).IsSuccessful, Is.True);
                var r = w.Output.PopulateMemberSteelUtilizationRatios(new HashSet<Beam>());
                Assert.That(r.IsSuccessful, Is.True);
                Assert.That(r.WasApplied, Is.False);
                Assert.That(w.Output.GetPlateCenterResults(new ILoadCase[0], new[] { new Plate { Id = 17 } }).Items, Is.Empty);
                Assert.That(w.Output.GetStaticCheckResults(new int[0]).IsSuccessful, Is.True);
                Assert.That(((OutputWorkflowFixture.Load)root.Load).Calls, Is.Zero);
                Assert.That(((OutputWorkflowFixture.Native)root.Output).Calls, Is.Empty);
            }
        }
        [Test]
        public void SupportActionNegatesEveryGlobalReactionComponentIncludingMoments()
        {
            using (var w = new OpenStaadWrapper(new OutputWorkflowFixture.Root()))
            {
                var r = w.Output.GetSupportForces(17, 1);
                Assert.That(r.Data.ForceVector, Is.EqualTo(new Vector3D(-1.25, -2.25, -3.25)));
                Assert.That(r.Data.MomentVector, Is.EqualTo(new Vector3D(-4.25, -5.25, -6.25)));
                Assert.That(r.ManagedOperation, Is.EqualTo("GetSupportForces"));
                Assert.That(r.NativeFunction, Is.EqualTo("GetSupportReactions"));
            }
        }
        [TestCase(ResultCoordinateSystem.Local, 0)]
        [TestCase(ResultCoordinateSystem.Global, 1)]
        public void ExplicitAxesDispatchDoesNotReconstructBetaOrChangeNativeSigns(ResultCoordinateSystem axes, int code)
        {
            var root = new OutputWorkflowFixture.Root();
            using (var w = new OpenStaadWrapper(root))
            {
                var r = w.Output.GetMemberEndForces(17, MemberEnd.End, 1, axes);
                Assert.That(r.Data.Fx, Is.EqualTo(1.25));
                Assert.That(r.NativeBooleanReturn, Is.True);
                Assert.That(((OutputWorkflowFixture.Native)root.Output).Arguments.Single(), Is.EqualTo(new object[] { 17, 1, 1, code }));
            }
        }
        [TestCase(false)]
        [TestCase(true)]
        public void InvalidNativeBuffersDoNotEscapeAndNativeFailureDoesNotReadThem(bool failure)
        {
            var root = new OutputWorkflowFixture.Root();
            var n = (OutputWorkflowFixture.Native)root.Output;
            n.ReplaceArray = (role, a) => new object[] { "bad" };
            if (failure)
                n.Responses["GetMemberEndForces"] = new Queue<object>(new object[] { false });
            using (var w = new OpenStaadWrapper(root))
            {
                if (failure)
                    Assert.That(w.Output.GetMemberEndForces(17, MemberEnd.Start, 1, ResultCoordinateSystem.Global).IsSuccessful, Is.False);
                else
                    Assert.Throws<InvalidOperationException>(() => w.Output.GetMemberEndForces(17, MemberEnd.Start, 1, ResultCoordinateSystem.Global));
            }
        }

        private static object[] ValidArguments(int index, OpenStaadWrapper wrapper)
        {
            var beam = Beam();
            var loadCases = new ILoadCase[] { new LoadCase(1, "Dead") };
            ILoadCase loadCase = loadCases[0];
            var plate = new Plate { Id = 17 };
            switch (index)
            {
                case 0:
                    return new object[] { beam, loadCases };
                case 1:
                    return new object[] { wrapper.Load, beam };
                case 2:
                    return new object[] { new[] { beam }, loadCases };
                case 3:
                    return new object[] { wrapper.Load, new[] { beam } };
                case 4:
                    return new object[] { new HashSet<Beam> { beam } };
                case 5:
                    return new object[] { new HashSet<Beam> { beam } };
                case 6:
                    return new object[] { "Dead", 17, 1, MemberEnd.Start };
                case 7:
                    return new object[] { "Dead", 17, 1, MemberEnd.Start, ResultCoordinateSystem.Global };
                case 8:
                    return new object[] { "Dead", beam, 1, MemberEnd.Start };
                case 9:
                    return new object[] { "Dead", beam, 1, MemberEnd.Start };
                case 10:
                    return new object[] { "Dead", 17, 1, MemberEnd.Start };
                case 11:
                    return new object[] { "Dead", 17, 1, MemberEnd.Start };
                case 12:
                    return new object[] { beam };
                case 13:
                    return new object[] { loadCase, plate };
                case 14:
                    return new object[] { loadCase, plate };
                case 15:
                    return new object[] { loadCase, plate };
                case 16:
                    return new object[] { loadCase, plate };
                case 17:
                    return new object[] { loadCase, plate };
                case 18:
                    return new object[] { new[] { loadCase }, new[] { plate } };
                case 19:
                    return new object[] { new[] { loadCase }, new[] { plate }, 2 };
                case 20:
                    return new object[] { new[] { 1, 2 } };
                case 21:
                    return new object[] { new[] { 1, 2 }, 2 };
                case 22:
                    return new object[] { 17, 1 };
                case 23:
                    return new object[] { 17, MemberEnd.Start, 1, ResultCoordinateSystem.Global };
                case 24:
                    return new object[] { beam };
                default:
                    throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
        private static readonly string[] WorkflowNames = { "GetSteelDesignResultsForBeam", "GetSteelDesignResultsForBeam", "GetSteelDesignResultsForBeams", "GetSteelDesignResultsForBeams", "GetMemberSteelUtilizationRatios", "PopulateMemberSteelUtilizationRatios", "GetMemberEndForces", "GetMemberEndForces", "GetMemberEndForcesConsideringOffsets", "GetMemberEndForcesAtPhysicalJoint", "GetMemberEndForcesConsideringOffsets", "GetMemberEndForcesAtPhysicalJoint", "GetMemberLocalCoordinateSystem", "GetPlateCenterForces", "GetPlateCenterMoments", "GetPlateCenterVonMises", "GetPlatePrincipalStresses", "GetPlateCenterResults", "GetPlateCenterResults", "GetPlateCenterResults", "GetStaticCheckResults", "GetStaticCheckResults", "GetSupportForces", "GetMemberEndForces", "GetMemberSteelUtilizationRatio" };
        private static readonly Type[][] WorkflowParameterTypes ={
            new[]{typeof(Beam),typeof(IEnumerable<ILoadCase>)},
            new[]{typeof(IOSLoad),typeof(Beam)},
            new[]{typeof(IEnumerable<Beam>),typeof(IEnumerable<ILoadCase>)},
            new[]{typeof(IOSLoad),typeof(IEnumerable<Beam>)},
            new[]{typeof(HashSet<Beam>)},
            new[]{typeof(HashSet<Beam>)},
            new[]{typeof(string),typeof(int),typeof(int),typeof(MemberEnd)},
            new[]{typeof(string),typeof(int),typeof(int),typeof(MemberEnd),typeof(ResultCoordinateSystem)},
            new[]{typeof(string),typeof(Beam),typeof(int),typeof(MemberEnd)},
            new[]{typeof(string),typeof(Beam),typeof(int),typeof(MemberEnd)},
            new[]{typeof(string),typeof(int),typeof(int),typeof(MemberEnd)},
            new[]{typeof(string),typeof(int),typeof(int),typeof(MemberEnd)},
            new[]{typeof(Beam)},
            new[]{typeof(ILoadCase),typeof(Plate)},
            new[]{typeof(ILoadCase),typeof(Plate)},
            new[]{typeof(ILoadCase),typeof(Plate)},
            new[]{typeof(ILoadCase),typeof(Plate)},
            new[]{typeof(ILoadCase),typeof(Plate)},
            new[]{typeof(IEnumerable<ILoadCase>),typeof(IEnumerable<Plate>)},
            new[]{typeof(IEnumerable<ILoadCase>),typeof(IEnumerable<Plate>),typeof(int)},
            new[]{typeof(IEnumerable<int>)},
            new[]{typeof(IEnumerable<int>),typeof(int)},
            new[]{typeof(int),typeof(int)},
            new[]{typeof(int),typeof(MemberEnd),typeof(int),typeof(ResultCoordinateSystem)},
            new[]{typeof(Beam)}
        };
        [TestCaseSource(nameof(Workflows))]
        public void InvalidInputsFailBeforeNativeDispatch(int index)
        {
            var root = new OutputWorkflowFixture.Root();
            var n = (OutputWorkflowFixture.Native)root.Output;
            using (var w = new OpenStaadWrapper(root))
            {
                var method = typeof(IOSOutput).GetMethod(WorkflowNames[index], WorkflowParameterTypes[index]);
                for (int p = 0; p < WorkflowParameterTypes[index].Length; p++)
                {
                    var args = ValidArguments(index, w);
                    Type type = WorkflowParameterTypes[index][p];
                    args[p] = type.IsEnum ? Enum.ToObject(type, 999) : type == typeof(int) ? (object)0 : null;
                    var ex = Assert.Throws<System.Reflection.TargetInvocationException>(() => method.Invoke(w.Output, args));
                    Assert.That(ex.InnerException, Is.InstanceOf<ArgumentException>(), index + ":" + p);
                    Assert.That(n.Calls, Is.Empty);
                }
            }
        }
        [TestCaseSource(nameof(Workflows))]
        public void OrdinaryNativeFailuresRemainTypedWithoutDataOrCacheApplication(int index)
        {
            if (index == 12)
                return;
            var root = new OutputWorkflowFixture.Root();
            var n = (OutputWorkflowFixture.Native)root.Output;
            foreach (string name in new[] { "GetMemberSteelDesignResults", "GetMemberSteelDesignRatio", "GetMemberEndForces", "GetAllPlateCenterForces", "GetAllPlateCenterMoments", "GetPlateCenterVonMisesStresses", "GetPlateCenterNormalPrincipalStresses", "GetStaticCheckResult", "GetSupportReactions" })
                n.Responses[name] = new Queue<object>(Enumerable.Repeat<object>(false, 10));
            using (var w = new OpenStaadWrapper(root))
            {
                object result = Call(index, w.Output, w);
                Assert.That(Property(result, "IsSuccessful"), Is.False);
                if (result is OutputPopulationResult population)
                    Assert.That(population.WasApplied, Is.False);
                else if (result.GetType().GetProperty("Data") != null)
                    Assert.That(Assert.Throws<System.Reflection.TargetInvocationException>(() => Property(result, "Data")).InnerException, Is.TypeOf<InvalidOperationException>());
            }
        }
        [Test]
        public void VoidCoordinatesMustBeWrittenEvenAfterSuccessfulIncidence()
        {
            var root = new OutputWorkflowFixture.Root();
            ((OutputWorkflowFixture.Geometry)root.Geometry).WriteCoordinates = false;
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.Throws<InvalidOperationException>(() => w.Output.GetMemberEndForcesConsideringOffsets("Dead", 17, 1, MemberEnd.Start));
                Assert.That(((OutputWorkflowFixture.Native)root.Output).Calls, Is.Empty);
            }
        }


        [Test]
        public void FiniteInputsWhoseProjectedComponentsOverflowAreRejected()
        {
            var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 1, 1, 0));
            using (var wrapper = new OpenStaadWrapper(new OutputWorkflowFixture.Root()))
            {
                var basis = wrapper.Output.GetMemberLocalCoordinateSystem(beam);
                Assert.Throws<ArgumentException>(() => basis.ToLocal(new Vector3D(double.MaxValue, double.MaxValue, 0)));
                Assert.Throws<ArgumentException>(() => basis.ToGlobal(new Vector3D(double.MaxValue, double.MaxValue, 0)));
            }
        }
        private static IEnumerable<int> Forever()
        {
            while (true)
                yield return 1;
        }
        private sealed class Once<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> items; public int Count;
            public Once(IEnumerable<T> items)
            {
                this.items = items;
            }
            public IEnumerator<T> GetEnumerator()
            {
                if (++Count != 1)
                    throw new InvalidOperationException("Reenumerated");
                return items.GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
