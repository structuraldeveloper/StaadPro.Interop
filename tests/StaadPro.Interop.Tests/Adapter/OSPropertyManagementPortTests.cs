using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using NUnit.Framework;
using PackageVerification;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSPropertyManagementPortTests
    {
        private static readonly string[] Names = {
            "DeleteMaterial", "CreateBeamPropertyFromTable", "CreateCompositeBeamPropertyFromTable", "CreateBuiltUpBeamProperty",
            "AssignBetaAnglesToBeams", "PopulateBetaAngles", "GetIsotropicMaterialAssignedBeams", "GetIsotropicMaterialAssignedPlates",
            "CreatePlateElementThickness", "CreatePlateElementThicknesses", "AssignPlateElementThickness", "AssignPlateElementThicknesses",
            "GetPlateThicknessByPlateId", "GetPlateThicknessByThicknessPropertyId", "GetAllPlateElementThicknesses",
            "GetAllPlateElementThicknessesWithPopulatedAssignments", "GetBeamOffsetSpecification", "PopulateMemberOffsets",
            "CreateOffsetSpecification", "CreateOffsetSpecifications", "AssignOffsetSpecifications"
        };
        private static IEnumerable<MethodInfo> Methods => typeof(IOSProperty).GetMethods().Where(m => Names.Contains(m.Name) && (m.Name != "AssignOffsetSpecifications" || m.GetParameters().Length == 1));
        public static IEnumerable<TestCaseData> Ports => Methods.Select(m => new TestCaseData(m).SetName("{m}_" + m));
        private static OpenStaadWrapper Wrap(PropertyPortFixture.Native native) => new OpenStaadWrapper(new PropertyPortFixture.Root { Property = native });
        private static HashSet<Beam> Beams() => new HashSet<Beam> { new Beam { Id = 17, BetaAngle = 25.0 }, new Beam { Id = 41, BetaAngle = -15.0 } };
        private static HashSet<Plate> Plates() => new HashSet<Plate> { new Plate { Id = 17 }, new Plate { Id = 99 } };
        private static PlateElementThickness Thickness() { var pet = new PlateElementThickness(0.2) { Id = 45 }; pet.AssignedPlates.UnionWith(Plates()); return pet; }
        private static OffsetSpecification Offset() { var spec = OffsetSpecification.LocalYEnd(-0.15); spec.Id = 72; spec.Beams.UnionWith(Beams()); return spec; }
        private static object[] Args(MethodInfo method, OpenStaadWrapper wrapper) => method.GetParameters().Select(p =>
            p.ParameterType == typeof(string) ? (object)" STEEL Ω " : p.ParameterType == typeof(Country) ? Country.Indian :
            p.ParameterType == typeof(TypeSpecification) ? (method.Name.Contains("Composite") ? TypeSpecification.BC : TypeSpecification.ST) :
            p.ParameterType == typeof(double[]) ? (method.Name.Contains("Composite") ? new[] { .2, .01 } : new[] { .6, .01, .8, .25, .02, .3, .03 }) :
            p.ParameterType == typeof(double) ? (object)0.0 : p.ParameterType == typeof(int) ? (p.Name == "nThreads" ? 2 : p.Name == "petId" ? 45 : 17) :
            p.ParameterType == typeof(IOSGeometry) ? wrapper.Geometry : p.ParameterType == typeof(HashSet<Beam>) || p.ParameterType == typeof(IEnumerable<Beam>) ? Beams() :
            p.ParameterType == typeof(IEnumerable<Plate>) ? (object)Plates() : p.ParameterType == typeof(PlateElementThickness) ? Thickness() :
            p.ParameterType == typeof(HashSet<PlateElementThickness>) ? new HashSet<PlateElementThickness> { Thickness() } :
            p.ParameterType == typeof(OffsetSpecification) ? (object)Offset() : p.ParameterType == typeof(HashSet<OffsetSpecification>) ? new HashSet<OffsetSpecification> { Offset() } :
            p.ParameterType == typeof(OffsetLocation) ? (object)OffsetLocation.End : OffsetAxis.Local).ToArray();
        private static object Invoke(IOSProperty property, MethodInfo method, object[] args)
        {
            try { return method.Invoke(property, args); }
            catch (TargetInvocationException e) { ExceptionDispatchInfo.Capture(e.InnerException).Throw(); throw; }
        }
        private static void Respond(PropertyPortFixture.Native native, string name, params object[] values) => native.Responses[name] = new Queue<object>(values);

        [Test]
        public void AddsExactlyTwentyFiveMethodsWithoutChangingExistingObservationReturns()
        {
            Assert.That(Methods.Count(), Is.EqualTo(25));
            Assert.That(typeof(IOSProperty).GetMethods().Count(m => !m.IsSpecialName), Is.EqualTo(225));
            foreach (var method in Methods)
            {
                var concrete = typeof(OSPropertyAdapter).GetMethod(method.Name, method.GetParameters().Select(p => p.ParameterType).ToArray());
                Assert.That(concrete, Is.Not.Null); Assert.That(concrete.ReturnType, Is.EqualTo(method.ReturnType));
                Assert.That(concrete.GetParameters().Select(p => p.Name), Is.EqualTo(method.GetParameters().Select(p => p.Name)));
                Assert.That(concrete.GetParameters().Select(p => p.DefaultValue), Is.EqualTo(method.GetParameters().Select(p => p.DefaultValue)));
            }
            var optional = typeof(IOSProperty).GetMethod("PopulateMemberOffsets").GetParameters()[1];
            Assert.That(optional.IsOptional, Is.True); Assert.That(optional.DefaultValue, Is.EqualTo(OffsetAxis.Global));
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryPortUsesManagedNativeEvidenceAndPreservesFluentIdentity(MethodInfo method)
        {
            var native = new PropertyPortFixture.Native();
            using (var wrapper = Wrap(native))
            {
                var value = Invoke(wrapper.Property, method, Args(method, wrapper));
                Assert.That(value, method.ReturnType == typeof(IOSProperty) ? Is.SameAs(wrapper.Property) : Is.Not.Null);
                Assert.That(native.Calls, Is.Not.Empty);
                Assert.That(native.Calls.Any(n => n.StartsWith("Save") || n.StartsWith("Analyze")), Is.False);
            }
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryPortRejectsDisposedBindingBeforeDispatch(MethodInfo method)
        {
            var native = new PropertyPortFixture.Native(); var wrapper = Wrap(native); var property = wrapper.Property;
            var args = Args(method, wrapper); wrapper.Dispose();
            Assert.Throws<InvalidOperationException>(() => Invoke(property, method, args)); Assert.That(native.Calls, Is.Empty);
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryPortPropagatesNativeTransportErrors(MethodInfo method)
        {
            var native = new PropertyPortFixture.Native(); var expected = new COMException("Native failure", unchecked((int)0x80004005));
            native.OnCall = op => throw expected;
            using (var wrapper = Wrap(native))
                Assert.That(Assert.Throws<COMException>(() => Invoke(wrapper.Property, method, Args(method, wrapper))), Is.SameAs(expected));
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryPortHasCompleteMatchingCompiledIntellisense(MethodInfo method)
        {
            var xml = XDocument.Load(Path.ChangeExtension(typeof(IOSProperty).Assembly.Location, ".xml"));
            string prefix = "M:StaadPro.Interop.Adapters.Interfaces.IOSProperty." + method.Name;
            var entries = xml.Descendants("member").Where(e => (string)e.Attribute("name") == prefix || ((string)e.Attribute("name")).StartsWith(prefix + "(")).ToArray();
            Assert.That(entries.Length, Is.EqualTo(typeof(IOSProperty).GetMethods().Count(m => m.Name == method.Name)));
            foreach (var entry in entries)
            {
                var id = ((string)entry.Attribute("name")).Replace("Interfaces.IOSProperty", "Models.OSPropertyAdapter");
                var concrete = xml.Descendants("member").Single(e => (string)e.Attribute("name") == id);
                Assert.That(concrete.Elements().Select(e => e.ToString()), Is.EqualTo(entry.Elements().Select(e => e.ToString())));
                foreach (string tag in new[] { "summary", "returns", "remarks", "example" }) Assert.That(entry.Element(tag)?.Value, Is.Not.Null.And.Not.Empty);
                Assert.That(entry.Element("remarks").Value, Does.Contain("When to use:"));
                Assert.That(entry.Descendants("include").Concat(entry.Descendants("inheritdoc")), Is.Empty);
                Assert.That(entry.Descendants().Attributes("cref").Any(a => a.Value.StartsWith("!:")), Is.False);
            }
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryPortDetectsBindingReplacementDuringTheNativeOperation(MethodInfo method)
        {
            var native = new PropertyPortFixture.Native(); var root = new PropertyPortFixture.Root { Property = native };
            using (var wrapper = new OpenStaadWrapper(root))
            {
                var property = wrapper.Property;
                native.OnCall = op => { native.OnCall = null; root.Property = new PropertyPortFixture.Native(); wrapper.RefreshComObjects(); };
                Assert.Throws<InvalidOperationException>(() => Invoke(property, method, Args(method, wrapper)));
            }
        }
        [Test]
        public void NativeSectionCreationPreservesRawFailureCodesAndCoverPlateCodeAndCopiesDimensions()
        {
            var native = new PropertyPortFixture.Native();
            using (var wrapper = Wrap(native))
            {
                var values = new[] { .2, .01 }; wrapper.Property.CreateCompositeBeamPropertyFromTable(Country.Indian, "ISMB300", TypeSpecification.BC, values);
                Assert.That(native.Arguments.Last()[2], Is.EqualTo(8)); Assert.That(native.Arguments.Last()[3], Is.EqualTo(values).And.Not.SameAs(values));
                var seven = new[] { .6, .01, .8, .25, .02, .3, .03 };
                wrapper.Property.CreateBuiltUpBeamProperty(null, seven); Assert.That(native.Arguments.Last()[0], Is.EqualTo(seven).And.Not.SameAs(seven));
                Respond(native, "CreateBeamPropertyFromTable", 0); Respond(native, "CreateBeamPropertyFromTableWithCoverPlates", -6004); Respond(native, "CreateTaperedIProperty", -108);
                Assert.That(wrapper.Property.CreateBeamPropertyFromTable(Country.Indian, "ISMB300", TypeSpecification.ST, 0, 0), Is.Zero);
                Assert.That(wrapper.Property.CreateCompositeBeamPropertyFromTable(Country.Indian, "ISMB300", TypeSpecification.TC, values), Is.EqualTo(-6004));
                Assert.That(wrapper.Property.CreateBuiltUpBeamProperty("ignored", seven), Is.EqualTo(-108));
            }
        }
        [TestCase(false)][TestCase(0)][TestCase(true)][TestCase(1)]
        public void DeleteMaterialRetainsSuccessOrRefusal(object raw)
        {
            var native = new PropertyPortFixture.Native(); Respond(native, "DeleteMaterial", raw);
            using (var wrapper = Wrap(native)) Assert.That(wrapper.Property.DeleteMaterial(" STEEL Ω "), Is.EqualTo(raw.Equals(true) || raw.Equals(1)));
            Assert.That(native.Arguments.Single()[0], Is.EqualTo(" STEEL Ω "));
        }
        [Test]
        public void BetaAssignmentsStayOnCallerApartmentAndStopOnFailure()
        {
            var native = new PropertyPortFixture.Native(); int caller = Thread.CurrentThread.ManagedThreadId; var threads = new List<int>();
            native.OnCall = op => threads.Add(Thread.CurrentThread.ManagedThreadId); Respond(native, "AssignBetaAngle", 1, 0);
            using (var wrapper = Wrap(native)) Assert.Throws<InvalidOperationException>(() => wrapper.Property.AssignBetaAnglesToBeams(Beams(), 4));
            Assert.That(threads, Is.EqualTo(new[] { caller, caller })); Assert.That(native.Calls, Is.EqualTo(new[] { "AssignBetaAngle", "AssignBetaAngle" }));
        }
        [Test]
        public void PopulationStagesAllReadsBeforeChangingCallerBeams()
        {
            var native = new PropertyPortFixture.Native(); var beams = Beams();
            using (var wrapper = Wrap(native))
            {
                Respond(native, "GetBetaAngle", 10.0, double.NaN);
                Assert.Throws<InvalidOperationException>(() => wrapper.Property.PopulateBetaAngles(beams)); Assert.That(beams.Select(b => b.BetaAngle), Is.EqualTo(new[] { 25.0, -15.0 }));
                Respond(native, "GetMemberGlobalOffSet", 0, -3001);
                Assert.Throws<InvalidOperationException>(() => wrapper.Property.PopulateMemberOffsets(beams));
                Assert.That(beams.All(b => b.StartOffset == null && b.EndOffset == null), Is.True);
                wrapper.Property.PopulateMemberOffsets(beams, OffsetAxis.Local);
                Assert.That(beams.All(b => b.EndOffset.Axis == OffsetAxis.Local && b.EndOffset.Location == OffsetLocation.End && b.EndOffset.X == -.2), Is.True);
            }
        }
        [Test]
        public void MaterialSubsetsReuseCandidateIdentityAndGeometryOverloadsHydrate()
        {
            var native = new PropertyPortFixture.Native(); var beams = Beams(); var plates = Plates();
            using (var wrapper = Wrap(native))
            {
                Assert.That(wrapper.Property.GetIsotropicMaterialAssignedBeams("STEEL", beams).All(b => beams.Any(candidate => ReferenceEquals(candidate, b))), Is.True);
                Assert.That(wrapper.Property.GetIsotropicMaterialAssignedPlates("STEEL", plates).Single(), Is.SameAs(plates.First()));
                var hydrated = wrapper.Property.GetIsotropicMaterialAssignedBeams("STEEL", wrapper.Geometry);
                Assert.That(hydrated.Select(b => b.Id), Is.EquivalentTo(new[] { 17, 41 })); Assert.That(hydrated.All(b => b.StartNode.Id == 1 && b.EndNode.Id == 2), Is.True);
                Assert.That(wrapper.Property.GetIsotropicMaterialAssignedPlates("STEEL", wrapper.Geometry).Single().C.Id, Is.EqualTo(3));
            }
        }
        [TestCase(true)][TestCase(false)]
        public void MaterialZeroCountsSkipFetchAndChangedCountsFail(bool beams)
        {
            var native = new PropertyPortFixture.Native(); string op = "GetIsotropicMaterialAssigned" + (beams ? "Beam" : "Plate") + "Count";
            using (var wrapper = Wrap(native))
            {
                Respond(native, op, 0);
                if (beams) Assert.That(wrapper.Property.GetIsotropicMaterialAssignedBeams("STEEL", Beams()), Is.Empty);
                else Assert.That(wrapper.Property.GetIsotropicMaterialAssignedPlates("STEEL", Plates()), Is.Empty);
                Assert.That(native.Calls, Is.EqualTo(new[] { op })); native.Calls.Clear();
                Respond(native, op, beams ? 2 : 1, 3);
                Assert.Throws<InvalidOperationException>(() => { if (beams) wrapper.Property.GetIsotropicMaterialAssignedBeams("STEEL", Beams()); else wrapper.Property.GetIsotropicMaterialAssignedPlates("STEEL", Plates()); });
            }
        }
        public static IEnumerable<TestCaseData> InvalidCounts
        {
            get { foreach (bool beams in new[] { true, false }) foreach (object raw in new object[] { -1, 100001, 1.0, "2", true, null }) yield return new TestCaseData(beams, raw); }
        }
        [TestCaseSource(nameof(InvalidCounts))]
        public void MaterialCountsRejectErrorsAndCoercionBeforeFetching(bool beams, object value)
        {
            var native = new PropertyPortFixture.Native(); var op = "GetIsotropicMaterialAssigned" + (beams ? "Beam" : "Plate") + "Count"; Respond(native, op, value);
            using (var wrapper = Wrap(native)) Assert.Throws<InvalidOperationException>(() => { if (beams) wrapper.Property.GetIsotropicMaterialAssignedBeams("STEEL", Beams()); else wrapper.Property.GetIsotropicMaterialAssignedPlates("STEEL", Plates()); });
            Assert.That(native.Calls, Is.EqualTo(new[] { op }));
        }
        public static IEnumerable<TestCaseData> InvalidOutputs
        {
            get { foreach (string op in new[] { "GetIsotropicMaterialAssignedBeamList", "GetIsotropicMaterialAssignedPlateList", "GetThicknessPropertyValues", "GetPlateThickness" }) foreach (string shape in new[] { "null", "type", "rank", "short", "long", "unwritten" }) yield return new TestCaseData(op, shape); }
        }
        [TestCaseSource(nameof(InvalidOutputs))]
        public void MalformedOrUnwrittenBuffersNeverBecomeSuccessfulPackets(string op, string shape)
        {
            var native = new PropertyPortFixture.Native(); native.Output = (name, value) =>
            {
                if (name != op) return value;
                var a = (Array)value; var element = a.GetType().GetElementType();
                switch (shape)
                {
                    case "null": return null; case "type": return new object[a.Length]; case "rank": return Array.CreateInstance(element, a.Length, 1);
                    case "short": return Array.CreateInstance(element, a.Length - 1); case "long": return Array.CreateInstance(element, a.Length + 1);
                    default: a.SetValue(element == typeof(int) ? (object)0 : double.NaN, 0); return a;
                }
            };
            using (var wrapper = Wrap(native)) Assert.Throws<InvalidOperationException>(() => Query(wrapper.Property, op));
        }
        private static void Query(IOSProperty p, string op)
        {
            switch (op)
            {
                case "GetThicknessPropertyValues": p.GetPlateThicknessByThicknessPropertyId(45); break;
                case "GetPlateThickness": p.GetPlateThicknessByPlateId(17); break;
                case "GetIsotropicMaterialAssignedBeamList": p.GetIsotropicMaterialAssignedBeams("STEEL", Beams()); break;
                default: p.GetIsotropicMaterialAssignedPlates("STEEL", Plates()); break;
            }
        }
        [Test]
        public void ThicknessPacketsRetainFourSlotsIdsOwnershipAndCandidateAssignments()
        {
            var native = new PropertyPortFixture.Native();
            native.Output = (op, value) =>
            {
                if (op != "GetThicknessPropertyValues") return value;
                var a = (Array)value; var result = Array.CreateInstance(typeof(double), new[] { 4 }, new[] { 5 });
                for (int i = 0; i < 4; i++) result.SetValue(a.GetValue(i), i + 5); return result;
            };
            using (var wrapper = Wrap(native))
            {
                var pet = wrapper.Property.GetPlateThicknessByThicknessPropertyId(45);
                Assert.That(pet.Id, Is.EqualTo(45)); Assert.That(pet.ToArray(), Is.EqualTo(new[] { .1, .2, .3, 0.0 })); Assert.That(pet.AssignedPlates, Is.Empty);
                ((Array)native.Buffers["GetThicknessPropertyValues"]).SetValue(99.0, 5); Assert.That(pet.A, Is.EqualTo(.1));
                Assert.That(wrapper.Property.GetPlateThicknessByPlateId(17).Id, Is.Zero);
                var candidates = Plates(); var populated = wrapper.Property.GetAllPlateElementThicknessesWithPopulatedAssignments(candidates);
                Assert.That(populated.Select(v => v.Id), Is.EquivalentTo(new[] { 45, 46 }));
                Assert.That(populated.Single(v => v.Id == 45).AssignedPlates.Single(), Is.SameAs(candidates.First()));
                Assert.That(populated.Single(v => v.Id == 46).AssignedPlates, Is.Empty);
            }
        }
        [Test]
        public void ReplacedThicknessInventoryIsRejectedAfterReadingValues()
        {
            var native = new PropertyPortFixture.Native(); int reads = 0;
            native.OnCall = op => { if (op == "GetThicknessPropertyValues" && ++reads == 2) native.Thicknesses = new[] { 46, 45 }; };
            using (var wrapper = Wrap(native)) Assert.Throws<InvalidOperationException>(() => wrapper.Property.GetAllPlateElementThicknesses());
        }
        [Test]
        public void ZeroAndAbsentOffsetPacketsAreIndependentAndKeepRequestedSelectors()
        {
            var native = new PropertyPortFixture.Native(); native.Output = (op, value) => op.StartsWith("offset") ? 0.0 : value;
            using (var wrapper = Wrap(native))
            {
                var first = wrapper.Property.GetBeamOffsetSpecification(17, OffsetLocation.End, OffsetAxis.Local);
                Respond(native, "GetMemberGlobalOffSet", -6027);
                var absent = wrapper.Property.GetBeamOffsetSpecification(17, OffsetLocation.End, OffsetAxis.Global);
                Assert.That(first.IsZero && absent.IsZero, Is.True); Assert.That(first, Is.Not.SameAs(OffsetSpecification.None));
                Assert.That(first.Location, Is.EqualTo(OffsetLocation.End)); Assert.That(first.Axis, Is.EqualTo(OffsetAxis.Local));
                first.Y = 10; Assert.That(absent.Y, Is.Zero); Assert.That(absent.Location, Is.EqualTo(OffsetLocation.End)); Assert.That(absent.Id, Is.Zero);
            }
        }
        [Test]
        public void BulkCreationPrevalidatesAndKeepsOnlySuccessfulNativeIdsOnPartialFailure()
        {
            var native = new PropertyPortFixture.Native(); var first = new PlateElementThickness(.2) { Id = 8 }; var second = new PlateElementThickness(.3) { Id = 9 };
            using (var wrapper = Wrap(native))
            {
                second.D = double.NaN;
                Assert.Throws<ArgumentException>(() => wrapper.Property.CreatePlateElementThicknesses(new HashSet<PlateElementThickness> { first, second })); Assert.That(native.Calls, Is.Empty);
                second.D = .3; Respond(native, "CreatePlateThicknessProperty", 94, -6003);
                Assert.Throws<InvalidOperationException>(() => wrapper.Property.CreatePlateElementThicknesses(new HashSet<PlateElementThickness> { first, second }));
                Assert.That(first.Id, Is.EqualTo(94)); Assert.That(second.Id, Is.EqualTo(9));
                var a = OffsetSpecification.GlobalYStart(.1); a.Id = 3; var b = OffsetSpecification.LocalYEnd(.2); b.Id = 4;
                Respond(native, "CreateMemberOffsetSpec", 81, -6016);
                Assert.Throws<InvalidOperationException>(() => wrapper.Property.CreateOffsetSpecifications(new HashSet<OffsetSpecification> { a, b }));
                Assert.That(a.Id, Is.EqualTo(81)); Assert.That(b.Id, Is.EqualTo(4));
            }
        }
        [Test]
        public void AssignmentAndZeroOffsetNoOpsRetainBindingValidationWithoutNativeAccess()
        {
            var native = new PropertyPortFixture.Native();
            using (var wrapper = Wrap(native))
            {
                var zero = new OffsetSpecification { Id = 99 };
                wrapper.Property.CreateOffsetSpecification(zero); wrapper.Property.CreateOffsetSpecifications(new HashSet<OffsetSpecification> { zero });
                wrapper.Property.AssignOffsetSpecifications(null); wrapper.Property.AssignOffsetSpecifications(new HashSet<OffsetSpecification> { zero });
                wrapper.Property.AssignPlateElementThickness(null); wrapper.Property.AssignPlateElementThicknesses(null);
                wrapper.Property.AssignPlateElementThickness(new PlateElementThickness()); wrapper.Property.AssignBetaAnglesToBeams(new HashSet<Beam>());
                Assert.That(zero.Id, Is.EqualTo(99)); Assert.That(native.Calls, Is.Empty);
            }
        }
        [TestCase("GetThicknessPropertyValues", 0)][TestCase("GetThicknessPropertyValues", true)][TestCase("GetPlateThickness", 1)]
        [TestCase("GetIsotropicMaterialAssignedBeamList", true)][TestCase("GetIsotropicMaterialAssignedPlateList", false)]
        [TestCase("AssignBetaAngle", 0)][TestCase("AssignPlateThickness", -4008)][TestCase("AssignMemberSpecToBeam", -6017)]
        [TestCase("CreatePlateThicknessProperty", 0)][TestCase("CreateMemberOffsetSpec", -6016)]
        public void NativeOperationSpecificFailuresDoNotUseAUniversalSuccessRule(string op, object raw)
        {
            var native = new PropertyPortFixture.Native(); Respond(native, op, raw);
            using (var wrapper = Wrap(native)) Assert.Throws<InvalidOperationException>(() =>
            {
                switch (op)
                {
                    case "AssignBetaAngle": wrapper.Property.AssignBetaAnglesToBeams(Beams()); break;
                    case "AssignPlateThickness": wrapper.Property.AssignPlateElementThickness(Thickness()); break;
                    case "AssignMemberSpecToBeam": wrapper.Property.AssignOffsetSpecifications(new HashSet<OffsetSpecification> { Offset() }); break;
                    case "CreatePlateThicknessProperty": wrapper.Property.CreatePlateElementThickness(Thickness()); break;
                    case "CreateMemberOffsetSpec": wrapper.Property.CreateOffsetSpecification(Offset()); break;
                    default: Query(wrapper.Property, op); break;
                }
            });
        }
        [Test]
        public void InvalidArgumentsAreRejectedBeforeAnyNativeOperation()
        {
            var native = new PropertyPortFixture.Native();
            using (var wrapper = Wrap(native))
            {
                var p = wrapper.Property;
                Assert.Throws<ArgumentException>(() => p.DeleteMaterial("STEEL\0"));
                Assert.Throws<ArgumentOutOfRangeException>(() => p.AssignBetaAnglesToBeams(Beams(), 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => p.AssignBetaAnglesToBeams(Beams(), 513));
                Assert.Throws<ArgumentException>(() => p.CreateBuiltUpBeamProperty("I", new double[7]));
                Assert.Throws<ArgumentOutOfRangeException>(() => p.CreateCompositeBeamPropertyFromTable(Country.Indian, "I", TypeSpecification.ST, new[] { .2, .01 }));
                Assert.Throws<ArgumentException>(() => p.CreateCompositeBeamPropertyFromTable(Country.Indian, "I", TypeSpecification.TB, new[] { .2, .01 }));
                Assert.Throws<ArgumentOutOfRangeException>(() => p.GetPlateThicknessByThicknessPropertyId(0));
                Assert.Throws<InvalidOperationException>(() => p.CreatePlateElementThickness(null));
                Assert.Throws<InvalidOperationException>(() => p.CreateOffsetSpecifications(null));
                Assert.Throws<InvalidOperationException>(() => p.CreateOffsetSpecifications(new HashSet<OffsetSpecification>()));
                Assert.Throws<ArgumentOutOfRangeException>(() => p.GetBeamOffsetSpecification(17, OffsetLocation.End, (OffsetAxis)5));
                Assert.Throws<ArgumentNullException>(() => p.GetIsotropicMaterialAssignedBeams("STEEL", (IOSGeometry)null));
                Assert.That(native.Calls, Is.Empty);
            }
        }
        [Test]
        public void NewWritesAndOldQueriesShareOneSerializationGate()
        {
            var native = new PropertyPortFixture.Native();
            using (var wrapper = Wrap(native))
            using (var entered = new ManualResetEventSlim())
            using (var release = new ManualResetEventSlim())
            using (var secondStarted = new ManualResetEventSlim())
            {
                native.OnCall = op => { if (op == "AssignBetaAngle") { entered.Set(); if (!release.Wait(5000)) throw new TimeoutException(); } };
                var write = Task.Run(() => wrapper.Property.AssignBetaAnglesToBeams(new HashSet<Beam> { Beams().First() }));
                Assert.That(entered.Wait(5000), Is.True);
                var read = Task.Run(() => { secondStarted.Set(); return wrapper.Property.GetBetaAngle(17); });
                Assert.That(secondStarted.Wait(5000), Is.True);
                try { Assert.That(read.Wait(50), Is.False); Assert.That(native.Calls, Is.EqualTo(new[] { "AssignBetaAngle" })); }
                finally { release.Set(); }
                Assert.That(Task.WaitAll(new Task[] { write, read }, 5000), Is.True);
            }
        }
    }
}
