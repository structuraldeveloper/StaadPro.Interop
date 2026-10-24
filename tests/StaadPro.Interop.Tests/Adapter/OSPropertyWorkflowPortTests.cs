using System;
using System.Collections;
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
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSPropertyWorkflowPortTests
    {
        private static readonly string[] Names = {
            "AssignMaterialProperties", "AssignMaterialProperty", "CreateMaterialProperties", "CreateMaterialPropertiesNoConversion", "CreateMaterialProperty", "CreateMaterialPropertyNoConversion",
            "GetAllExtendedIsotropicMaterialProperties", "GetAllIsotropicMaterialProperties", "GetAllExtendedIsotropicMaterialPropertiesWithPopulatedAssignments",
            "GetAllIsotropicMaterialPropertiesWithPopulatedAssignments", "GetAllExtendedMaterialPropertiesWithPopulatedAssignments", "GetAllMaterialPropertiesWithPopulatedAssignments",
            "CreatePlateThicknessProperties", "AssignThicknessProperties", "AssignThicknessProperty", "AssignOffsetSpecification", "AssignOffsetSpecifications",
            "CreateAndAssignOffsetSpecifications", "PopulateMemberReleases", "CreateAndAssignMemberEndReleases"
        };
        private static IEnumerable<MethodInfo> Methods => typeof(IOSProperty).GetMethods().Where(m => Names.Contains(m.Name) && (m.Name != "AssignOffsetSpecifications" || m.GetParameters().Length == 2));
        public static IEnumerable<TestCaseData> Ports => Methods.Select(m => new TestCaseData(m).SetName("{m}_" + m));
        private static OpenStaadWrapper Wrap(PropertyPortFixture.Native p) => new OpenStaadWrapper(new PropertyPortFixture.Root { Property = p });
        private static MemberReleases Release() { var r = new MemberReleases(); r.Releases.My = true; r.Releases.Mz = true; return r; }
        private static HashSet<Beam> Beams() => new HashSet<Beam> {
            new Beam { Id = 17, StartOffset = OffsetSpecification.GlobalYStart(.1), EndOffset = OffsetSpecification.LocalYEnd(.2), StartRelease = Release(), EndRelease = Release() }, new Beam { Id = 41 }
        };
        private static HashSet<Plate> Plates() => new HashSet<Plate> { new Plate { Id = 17 }, new Plate { Id = 99 } };
        private static MaterialProperty Material()
        { var m = new MaterialProperty(" STEEL Ω ") { Id = 73, E = 200000, Poisson = .3, Density = 77, Alpha = 12e-6, Damping = .02 }; m.Beams.UnionWith(Beams()); m.Plates.UnionWith(Plates()); return m; }
        private static PlateThickness Thickness() { var t = new PlateThickness { Id = 45, Thickness = .2 }; t.Plates.UnionWith(Plates()); return t; }
        private static OffsetSpecification Offset() { var s = OffsetSpecification.LocalYEnd(.15); s.Id = 72; s.Beams.UnionWith(Beams()); return s; }
        private static object[] Args(MethodInfo method) => method.GetParameters().Select(p =>
            p.ParameterType == typeof(int) ? (object)2 : p.ParameterType == typeof(MaterialProperty) ? Material() :
            p.ParameterType == typeof(HashSet<MaterialProperty>) ? new HashSet<MaterialProperty> { Material() } :
            p.ParameterType == typeof(HashSet<Beam>) || p.ParameterType == typeof(IEnumerable<Beam>) ? (object)Beams() :
            p.ParameterType == typeof(IEnumerable<Plate>) ? Plates() : p.ParameterType == typeof(PlateThickness) ? (object)Thickness() :
            p.ParameterType == typeof(HashSet<PlateThickness>) ? new HashSet<PlateThickness> { Thickness() } :
            p.ParameterType == typeof(OffsetSpecification) ? (object)Offset() : p.ParameterType == typeof(HashSet<OffsetSpecification>) ? new HashSet<OffsetSpecification> { Offset() } :
            (object)(Func<string, MaterialProperty>)(name => new MaterialProperty(name))).ToArray();
        private static object Invoke(IOSProperty p, MethodInfo method, object[] args = null)
        {
            try { return method.Invoke(p, args ?? Args(method)); }
            catch (TargetInvocationException e) { ExceptionDispatchInfo.Capture(e.InnerException).Throw(); throw; }
        }
        private static void Respond(PropertyPortFixture.Native p, string op, params object[] responses) => p.Responses[op] = new Queue<object>(responses);
        [Test]
        public void AddsExactlyTwentyFiveSourceSignaturesOnBothSurfaces()
        {
            Assert.That(Methods.Count(), Is.EqualTo(25)); Assert.That(typeof(IOSProperty).GetMethods().Count(m => !m.IsSpecialName), Is.EqualTo(100));
            foreach (var method in Methods)
            {
                var implementation = typeof(OSPropertyAdapter).GetMethod(method.Name, method.GetParameters().Select(p => p.ParameterType).ToArray());
                Assert.That(implementation, Is.Not.Null); Assert.That(implementation.ReturnType, Is.EqualTo(method.ReturnType));
                Assert.That(implementation.GetParameters().Select(p => p.Name), Is.EqualTo(method.GetParameters().Select(p => p.Name)));
                Assert.That(implementation.GetParameters().Select(p => p.DefaultValue), Is.EqualTo(method.GetParameters().Select(p => p.DefaultValue)));
                Assert.That(method.GetParameters().All(p => !p.IsOut && !p.ParameterType.IsByRef), Is.True);
            }
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryPortExecutesWithCheckedNativeEvidenceAndFluentIdentity(MethodInfo method)
        {
            var native = new PropertyPortFixture.Native();
            using (var w = Wrap(native))
            {
                var result = Invoke(w.Property, method);
                Assert.That(result, method.ReturnType == typeof(IOSProperty) ? Is.SameAs(w.Property) : Is.Not.Null);
                Assert.That(native.Calls, Is.Not.Empty);
                Assert.That(native.Calls.Any(c => c.StartsWith("Save") || c.StartsWith("Analyze")), Is.False);
            }
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryPortRejectsDisposedBindingsBeforeDispatch(MethodInfo method)
        {
            var native = new PropertyPortFixture.Native(); var w = Wrap(native); var p = w.Property; w.Dispose();
            Assert.Throws<InvalidOperationException>(() => Invoke(p, method)); Assert.That(native.Calls, Is.Empty);
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryPortStopsWhenItsNativeBindingChanges(MethodInfo method)
        {
            var native = new PropertyPortFixture.Native(); var root = new PropertyPortFixture.Root { Property = native };
            using (var w = new OpenStaadWrapper(root))
            {
                var p = w.Property;
                native.OnCall = op => { native.OnCall = null; root.Property = new PropertyPortFixture.Native(); w.RefreshComObjects(); };
                Assert.Throws<InvalidOperationException>(() => Invoke(p, method)); Assert.That(native.Calls.Count, Is.EqualTo(1));
            }
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryPortPropagatesTransportFailures(MethodInfo method)
        {
            var native = new PropertyPortFixture.Native(); var error = new COMException("Transport failure", unchecked((int)0x80004005)); native.OnCall = op => throw error;
            using (var w = Wrap(native)) Assert.That(Assert.Throws<COMException>(() => Invoke(w.Property, method)), Is.SameAs(error));
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryPortShipsMatchingCompleteExpandedXml(MethodInfo method)
        {
            var xml = XDocument.Load(Path.ChangeExtension(typeof(IOSProperty).Assembly.Location, ".xml"));
            string prefix = "M:StaadPro.Interop.Adapters.Interfaces.IOSProperty." + method.Name;
            var entries = xml.Descendants("member").Where(e => (string)e.Attribute("name") == prefix || ((string)e.Attribute("name")).StartsWith(prefix + "(")).ToArray();
            Assert.That(entries.Length, Is.EqualTo(typeof(IOSProperty).GetMethods().Count(m => m.Name == method.Name)));
            foreach (var entry in entries)
            {
                string id = ((string)entry.Attribute("name")).Replace("Interfaces.IOSProperty", "Models.OSPropertyAdapter");
                var concrete = xml.Descendants("member").Single(e => (string)e.Attribute("name") == id);
                Assert.That(entry.Elements().Select(e => e.ToString()), Is.EqualTo(concrete.Elements().Select(e => e.ToString())));
                foreach (string tag in new[] { "summary", "returns", "remarks", "example" }) Assert.That(entry.Element(tag)?.Value, Is.Not.Null.And.Not.Empty);
                Assert.That(entry.Element("remarks").Value, Does.Contain("When to use:"));
                Assert.That(entry.Descendants("include").Concat(entry.Descendants("inheritdoc")), Is.Empty);
                Assert.That(entry.Descendants().Attributes("cref").Any(a => a.Value.StartsWith("!:")), Is.False);
            }
        }
        [TestCase(false, 0)][TestCase(false, 1)][TestCase(true, 0)][TestCase(true, 1)]
        public void MaterialCreationKeepsNativeOutcomeSeparateFromIdAndUsesDocumentedScale(bool legacy, int code)
        {
            var native = new PropertyPortFixture.Native(); var m = Material(); var before = new[] { m.E, m.G, m.Poisson, m.Density, m.Alpha, m.Damping };
            Respond(native, "CreateIsotropicMaterialProperties", code);
            using (var w = Wrap(native)) { if (legacy) w.Property.CreateMaterialProperty(m); else w.Property.CreateMaterialPropertyNoConversion(m); }
            Assert.That(m.Id, Is.EqualTo(73)); Assert.That(m.LastCreationStatus, Is.EqualTo((MaterialCreationStatus)code));
            Assert.That(native.Arguments.Single(), Is.EqualTo(new object[] { m.Name, m.E * (legacy ? 1000 : 1), m.Poisson, m.G * (legacy ? 1000 : 1), m.Density, m.Alpha, m.Damping }));
            Assert.That(new[] { m.E, m.G, m.Poisson, m.Density, m.Alpha, m.Damping }, Is.EqualTo(before));
        }
        [TestCase(-1)][TestCase(2)][TestCase(true)][TestCase(0.0)][TestCase("0")][TestCase(null)]
        public void MaterialCreationRejectsErrorsAndCoercionWithoutChangingPriorOutcomeOrId(object raw)
        {
            var native = new PropertyPortFixture.Native(); var m = Material();
            using (var w = Wrap(native))
            {
                Respond(native, "CreateIsotropicMaterialProperties", 1, raw); w.Property.CreateMaterialPropertyNoConversion(m);
                Assert.Throws<InvalidOperationException>(() => w.Property.CreateMaterialPropertyNoConversion(m));
                Assert.That(m.Id, Is.EqualTo(73)); Assert.That(m.LastCreationStatus, Is.EqualTo(MaterialCreationStatus.Updated));
            }
        }
        [Test]
        public void MaterialBatchPrevalidatesSnapshotsInputsAndRetainsPartialSuccess()
        {
            var native = new PropertyPortFixture.Native(); var first = Material(); var second = new MaterialProperty("SECOND");
            using (var w = Wrap(native))
            {
                second.Poisson = .5;
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.CreateMaterialPropertiesNoConversion(new HashSet<MaterialProperty> { first, second })); Assert.That(native.Calls, Is.Empty);
                second.Poisson = .3;
                native.OnCall = op => { if (op == "CreateIsotropicMaterialProperties") second.E = double.NaN; };
                Respond(native, "CreateIsotropicMaterialProperties", 0, -1);
                Assert.Throws<InvalidOperationException>(() => w.Property.CreateMaterialPropertiesNoConversion(new HashSet<MaterialProperty> { first, second }));
                Assert.That(first.LastCreationStatus, Is.EqualTo(MaterialCreationStatus.Created)); Assert.That(second.LastCreationStatus, Is.Null);
                Assert.That(native.Arguments[1][1], Is.EqualTo(200000.0)); Assert.That(first.Id, Is.EqualTo(73));
            }
        }
        [Test]
        public void IndexedMaterialsRetainActualNamesIndexesAndExtendedStrengthsWithoutReplacingNativeGQueries()
        {
            var native = new PropertyPortFixture.Native();
            using (var w = Wrap(native))
            {
                var basic = w.Property.GetAllIsotropicMaterialProperties().OrderBy(m => m.Id).ToArray();
                Assert.That(basic.Select(m => m.Name), Is.EqualTo(native.MaterialNames)); Assert.That(basic.Select(m => m.Id), Is.EqualTo(new[] { 0, 1 }));
                Assert.That(basic.All(m => m.Fy == 0 && m.Beams.Count == 0 && m.Plates.Count == 0), Is.True);
                Assert.That(basic[0].G, Is.EqualTo(200000.0 / 2.6)); Assert.That(w.Property.GetIsotropicMaterialProperty(0).Data.Constants.ShearModulus, Is.EqualTo(80000.0));
                Assert.That(w.Property.GetAllExtendedIsotropicMaterialProperties().All(m => m.Fy == 355 && m.Fcu == 40), Is.True);
            }
        }
        [Test]
        public void MaterialNameEqualityUsesConsistentCaseInsensitiveHashing()
        {
            var first = new MaterialProperty("Steel"); var second = new MaterialProperty("STEEL");
            Assert.That(first.Equals(second), Is.True); Assert.That(first.GetHashCode(), Is.EqualTo(second.GetHashCode()));
            Assert.That(new HashSet<MaterialProperty> { first, second }.Count, Is.EqualTo(1));
            var native = new PropertyPortFixture.Native { MaterialNames = new[] { "Steel", "STEEL" } };
            using (var w = Wrap(native)) Assert.That(w.Property.GetAllIsotropicMaterialProperties().Single().Id, Is.Zero);
        }
        [TestCase(-1)][TestCase(100001)][TestCase(true)][TestCase(2.0)][TestCase("2")][TestCase(null)]
        public void InventoryBoundsAndTypesAreCheckedBeforeIndexReads(object raw)
        {
            var native = new PropertyPortFixture.Native(); Respond(native, "GetIsotropicMaterialCount", raw);
            using (var w = Wrap(native)) Assert.Throws<InvalidOperationException>(() => w.Property.GetAllIsotropicMaterialProperties());
            Assert.That(native.Calls, Is.EqualTo(new[] { "GetIsotropicMaterialCount" }));
        }
        [Test]
        public void EmptyInventorySkipsIndexCallsAndChangedOrMalformedIndexedDataFails()
        {
            var native = new PropertyPortFixture.Native { MaterialNames = new string[0] };
            using (var w = Wrap(native)) Assert.That(w.Property.GetAllIsotropicMaterialProperties(), Is.Empty);
            Assert.That(native.Calls.All(c => c == "GetIsotropicMaterialCount"), Is.True);
            native = new PropertyPortFixture.Native(); Respond(native, "GetIsotropicMaterialCount", 2, 2, 3);
            using (var w = Wrap(native)) Assert.Throws<InvalidOperationException>(() => w.Property.GetAllExtendedIsotropicMaterialProperties());
            native = new PropertyPortFixture.Native(); native.Output = (op, v) => op == "materialE" ? double.NaN : v;
            using (var w = Wrap(native)) Assert.Throws<InvalidOperationException>(() => w.Property.GetAllIsotropicMaterialProperties());
        }
        private sealed class SingleUse<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> items; private bool used;
            internal SingleUse(IEnumerable<T> items) { this.items = items; }
            public IEnumerator<T> GetEnumerator() { if (used) throw new InvalidOperationException("Enumerated twice"); used = true; return items.GetEnumerator(); }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        [Test]
        public void ResolverRunsOncePerExactNameAndReusesPacketsAndCandidateReferences()
        {
            var native = new PropertyPortFixture.Native(); var beams = Beams(); var plates = Plates(); var names = new List<string>(); var packets = new List<MaterialProperty>();
            using (var w = Wrap(native))
            {
                var result = w.Property.GetAllMaterialPropertiesWithPopulatedAssignments(new SingleUse<Beam>(beams), new SingleUse<Plate>(plates), name =>
                { names.Add(name); var p = new MaterialProperty(name); packets.Add(p); return p; });
                Assert.That(names, Is.EqualTo(new[] { " STEEL Ω ", " CONCRETE Ω " })); Assert.That(result.All(p => packets.Any(original => ReferenceEquals(original, p))), Is.True);
                Assert.That(result.All(p => p.Beams.All(b => beams.Any(original => ReferenceEquals(original, b)))), Is.True);
                Assert.That(result.All(p => p.Plates.Single() == plates.First()), Is.True);
            }
        }
        [Test]
        public void AssignmentPopulationStagesReadsAndPreservesSkippedCandidateSides()
        {
            var native = new PropertyPortFixture.Native(); var a = new MaterialProperty("A"); var b = new MaterialProperty("B"); var sentinel = new Beam { Id = 99 }; var oldPlate = new Plate { Id = 88 };
            a.Beams.Add(sentinel); b.Beams.Add(sentinel); a.Plates.Add(oldPlate); b.Plates.Add(oldPlate);
            using (var w = Wrap(native))
            {
                Respond(native, "GetIsotropicMaterialAssignedBeamList", 0, -6023);
                Assert.Throws<InvalidOperationException>(() => w.Property.GetAllMaterialPropertiesWithPopulatedAssignments(Beams(), Plates(), name => name.Contains("STEEL") ? a : b));
                Assert.That(a.Beams.Single(), Is.SameAs(sentinel)); Assert.That(b.Beams.Single(), Is.SameAs(sentinel)); Assert.That(a.Plates.Single(), Is.SameAs(oldPlate));
                w.Property.GetAllMaterialPropertiesWithPopulatedAssignments(Beams(), null, name => a);
                Assert.That(a.Beams.Select(e => e.Id), Is.EquivalentTo(new[] { 17, 41 })); Assert.That(a.Plates.Single(), Is.SameAs(oldPlate));
            }
        }
        [Test]
        public void ResolverFailuresAndMissingNamesNeverProduceDefaultMaterialPackets()
        {
            var native = new PropertyPortFixture.Native();
            using (var w = Wrap(native))
            {
                Assert.Throws<InvalidOperationException>(() => w.Property.GetAllMaterialPropertiesWithPopulatedAssignments(Beams(), null, name => null));
                var failure = new ApplicationException("Resolver failure");
                Assert.That(Assert.Throws<ApplicationException>(() => w.Property.GetAllMaterialPropertiesWithPopulatedAssignments(Beams(), null, name => throw failure)), Is.SameAs(failure));
                Respond(native, "GetBeamMaterialName", "");
                Assert.Throws<InvalidOperationException>(() => w.Property.GetAllMaterialPropertiesWithPopulatedAssignments(Beams(), null));
            }
        }
        [Test]
        public void MaterialAssignmentUsesDifferentMemberAndPlateReturnContractsAndSnapshotsTargets()
        {
            var native = new PropertyPortFixture.Native(); var m = Material();
            native.OnCall = op => { if (op == "AssignMaterialToMember") { m.Name = "CHANGED"; m.Beams.Clear(); m.Plates.Clear(); } };
            using (var w = Wrap(native)) w.Property.AssignMaterialProperty(m, 4);
            Assert.That(native.Calls, Is.EqualTo(new[] { "AssignMaterialToMember", "AssignMaterialToMember", "AssignMaterialToPlate", "AssignMaterialToPlate" }));
            Assert.That(native.Arguments.All(a => (string)a[0] == " STEEL Ω "), Is.True);
            foreach (string op in new[] { "AssignMaterialToMember", "AssignMaterialToPlate" })
            {
                native = new PropertyPortFixture.Native(); Respond(native, op, op.EndsWith("Member") ? (object)false : -4008);
                using (var w = Wrap(native)) Assert.Throws<InvalidOperationException>(() => w.Property.AssignMaterialProperties(new HashSet<MaterialProperty> { Material() }));
            }
        }
        [Test]
        public void UniformThicknessUsesScalarCreationAndCorrectBatchOrScalarAssignmentShapes()
        {
            var native = new PropertyPortFixture.Native(); var t = Thickness();
            using (var w = Wrap(native))
            {
                w.Property.CreatePlateThicknessProperties(new HashSet<PlateThickness> { t }); Assert.That(native.Arguments[0], Is.EqualTo(new object[] { .2 })); Assert.That(t.Id, Is.EqualTo(94));
                w.Property.AssignThicknessProperties(new HashSet<PlateThickness> { t }); Assert.That(native.Arguments[1][0], Is.TypeOf<int[]>());
                w.Property.AssignThicknessProperties(new HashSet<PlateThickness> { t }, 4); Assert.That(native.Arguments.Skip(2).All(a => a[0] is int), Is.True);
                Respond(native, "CreatePlateThicknessProperty", -6003); Assert.Throws<InvalidOperationException>(() => w.Property.CreatePlateThicknessProperties(new HashSet<PlateThickness> { t })); Assert.That(t.Id, Is.EqualTo(94));
                Respond(native, "AssignPlateThickness", -4008); Assert.Throws<InvalidOperationException>(() => w.Property.AssignThicknessProperty(t, 2));
            }
        }
        [TestCase(false)][TestCase(true)]
        public void OffsetPipelineGroupsEqualDefinitionsPropagatesIdsAndSkipsZeros(bool degree)
        {
            var native = new PropertyPortFixture.Native(); var first = OffsetSpecification.LocalYEnd(.2); var equal = OffsetSpecification.LocalYEnd(.2);
            first.Beams.Add(new Beam { Id = 99 }); var beams = new HashSet<Beam> { new Beam { Id = 17, EndOffset = first }, new Beam { Id = 41, EndOffset = equal, StartOffset = new OffsetSpecification() } };
            using (var w = Wrap(native)) { if (degree) w.Property.CreateAndAssignOffsetSpecifications(beams, 3); else w.Property.CreateAndAssignOffsetSpecifications(beams); }
            Assert.That(native.Calls.Count(c => c == "CreateMemberOffsetSpec"), Is.EqualTo(1)); Assert.That(first.Id, Is.EqualTo(72)); Assert.That(equal.Id, Is.EqualTo(72));
            Assert.That(first.Beams.Select(b => b.Id), Is.EquivalentTo(new[] { 99, 17, 41 }));
            Assert.That(native.Arguments.Skip(1).All(a => (int)a[1] == 72), Is.True);
            Assert.That(native.Arguments.Skip(1).All(a => degree ? a[0] is int : a[0] is int[]), Is.True);
        }
        [Test]
        public void OffsetPipelineSnapshotsTargetsAndCompletesCreationBeforeAssignment()
        {
            var native = new PropertyPortFixture.Native(); var beams = Beams(); var original = beams.First();
            native.OnCall = op => { if (op == "CreateMemberOffsetSpec") original.Id = 0; };
            using (var w = Wrap(native)) w.Property.CreateAndAssignOffsetSpecifications(beams);
            Assert.That(native.Calls, Is.EqualTo(new[] { "CreateMemberOffsetSpec", "CreateMemberOffsetSpec", "AssignMemberSpecToBeam", "AssignMemberSpecToBeam" }));
            Assert.That(native.Arguments.Skip(2).All(a => ((int[])a[0]).SequenceEqual(new[] { 17 })), Is.True);
        }
        [Test]
        public void DegreeOffsetAssignmentUsesExistingIdEvenForZeroComponents()
        {
            var native = new PropertyPortFixture.Native(); var s = new OffsetSpecification { Id = 72 }; s.Beams.Add(new Beam { Id = 17 });
            using (var w = Wrap(native)) w.Property.AssignOffsetSpecification(s, 2);
            Assert.That(native.Calls.Single(), Is.EqualTo("AssignMemberSpecToBeam")); Assert.That(native.Arguments.Single(), Is.EqualTo(new object[] { 17, 72 }));
        }
        [Test]
        public void ReleasePopulationRetainsMixedModesAndStagesBothEnds()
        {
            var native = new PropertyPortFixture.Native { ReleaseModes = new[] { 0, -1, 1, -2, -3, 0 }, ReleaseValues = new[] { double.NaN, 50.0, double.NaN, .25, double.NaN, double.NaN } };
            var beams = Beams(); var originalStart = beams.First().StartRelease;
            using (var w = Wrap(native))
            {
                Respond(native, "GetMemberReleaseSpec", 1, 0);
                Assert.Throws<InvalidOperationException>(() => w.Property.PopulateMemberReleases(beams)); Assert.That(beams.First().StartRelease, Is.SameAs(originalStart));
                w.Property.PopulateMemberReleases(beams);
                var r = beams.First().EndRelease;
                Assert.That(r.NativeSpecification.RotationX.Mode, Is.EqualTo(MemberDofReleaseMode.DirectionalPartialMoment));
                Assert.That(r.NativeSpecification.RotationY.Mode, Is.EqualTo(MemberDofReleaseMode.CommonPartialMoment));
                Assert.That(r.NativeSpecification.CommonPartialMomentFactor, Is.Null); Assert.That(r.Springs.KFy, Is.EqualTo(50)); Assert.That(r.Springs.KFx, Is.Null);
                Assert.That(r.ReleaseType, Is.EqualTo(ReleaseType.MpAll));
                Assert.Throws<NotSupportedException>(() => w.Property.CreateAndAssignMemberEndReleases(beams));
            }
        }
        [TestCase(false)][TestCase(true)]
        public void ReleaseCreationPreservesEndFlagSpringOrderAndAssignsOnlyPositiveReferences(bool degree)
        {
            var native = new PropertyPortFixture.Native(); var b = Beams().First(); b.StartRelease.Springs.KFx = 25.0;
            using (var w = Wrap(native)) { if (degree) w.Property.CreateAndAssignMemberEndReleases(new HashSet<Beam> { b }, 3); else w.Property.CreateAndAssignMemberEndReleases(new HashSet<Beam> { b }); }
            Assert.That(native.Calls, Is.EqualTo(new[] { "CreateMemberReleaseSpec", "AssignMemberSpecToBeam", "CreateMemberReleaseSpec", "AssignMemberSpecToBeam" }));
            Assert.That(native.Arguments[0][0], Is.EqualTo(0)); Assert.That(native.Arguments[0][1], Is.EqualTo(new[] { 0, 0, 0, 0, 1, 1 }));
            Assert.That(native.Arguments[0][2], Is.EqualTo(new[] { 25.0, 0, 0, 0, 0, 0 }));
            Assert.That(native.Arguments[2][0], Is.EqualTo(1)); Assert.That(b.StartRelease.Id, Is.EqualTo(95)); Assert.That(b.EndRelease.Id, Is.EqualTo(96));
        }
        [TestCase(0)][TestCase(-6020)][TestCase(true)]
        public void FailedReleaseCreationNeverOverwritesIdOrAssignsIt(object raw)
        {
            var native = new PropertyPortFixture.Native(); var b = new Beam { Id = 17, EndRelease = Release() }; b.EndRelease.Id = 42; Respond(native, "CreateMemberReleaseSpec", raw);
            using (var w = Wrap(native)) Assert.Throws<InvalidOperationException>(() => w.Property.CreateAndAssignMemberEndReleases(new HashSet<Beam> { b }));
            Assert.That(b.EndRelease.Id, Is.EqualTo(42)); Assert.That(native.Calls, Is.EqualTo(new[] { "CreateMemberReleaseSpec" }));
        }
        [Test]
        public void ReleaseAssignmentFailureRetainsTheAlreadyCreatedReference()
        {
            var native = new PropertyPortFixture.Native(); var b = new Beam { Id = 17, EndRelease = Release() }; Respond(native, "AssignMemberSpecToBeam", -6017);
            using (var w = Wrap(native)) Assert.Throws<InvalidOperationException>(() => w.Property.CreateAndAssignMemberEndReleases(new HashSet<Beam> { b }));
            Assert.That(b.EndRelease.Id, Is.EqualTo(96)); Assert.That(native.Calls.Count, Is.EqualTo(2));
        }
        [Test]
        public void ReleaseParserRetainsExtendedModesAndInvalidatesObservationWhenProjectionChanges()
        {
            var r = new MemberReleases(); r.Parse(new[] { 0, 0, 0, -2, -3, 1 }, new[] { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN }, .4, new[] { .2, double.NaN, double.NaN });
            Assert.That(r.NativeSpecification.DirectionalPartialMomentFactors.MomentX, Is.EqualTo(.2)); Assert.That(r.MpFactor, Is.EqualTo(.4));
            r.Releases.Fx = true; Assert.That(r.NativeSpecification, Is.Null); Assert.That(r.ReleaseType, Is.EqualTo(ReleaseType.MpAll));
            var modes = new[] { 0, 0, 0, 0, 1, 1 }; r.Parse(modes, new double[6]); Assert.That(r.ReleaseType, Is.EqualTo(ReleaseType.Release));
            r.Springs.KFx = 25; Assert.That(r.ReleaseType, Is.EqualTo(ReleaseType.ReleaseWithSprings));
            r.Releases.My = r.Releases.Mz = false; Assert.That(r.ReleaseType, Is.EqualTo(ReleaseType.ReleaseWithSprings));
            Assert.Throws<InvalidOperationException>(() => r.Parse(new[] { 0, 0, 0, 0, 8, 1 }, new double[6]));
        }
        [Test]
        public void NullAndEmptyAssignmentInputsAreNoOpsButCreationStillRequiresDefinitions()
        {
            var native = new PropertyPortFixture.Native();
            using (var w = Wrap(native))
            {
                var p = w.Property; p.AssignMaterialProperties(null); p.AssignMaterialProperties(null, 2); p.AssignMaterialProperty(null, 2);
                p.AssignThicknessProperties(null); p.AssignThicknessProperties(null, 2); p.AssignThicknessProperty(null, 2);
                p.AssignOffsetSpecification(null, 2); p.AssignOffsetSpecifications(null, 2); p.CreateAndAssignOffsetSpecifications(new HashSet<Beam>()); p.CreateAndAssignMemberEndReleases(new HashSet<Beam>());
                Assert.Throws<InvalidOperationException>(() => p.CreateMaterialProperties(null)); Assert.Throws<InvalidOperationException>(() => p.CreateMaterialPropertiesNoConversion(new HashSet<MaterialProperty>()));
                Assert.Throws<InvalidOperationException>(() => p.CreatePlateThicknessProperties(null)); Assert.That(native.Calls, Is.Empty);
            }
        }
        [Test]
        public void AllBatchInputsArePrevalidatedBeforeAnyNativeWrite()
        {
            var native = new PropertyPortFixture.Native();
            using (var w = Wrap(native))
            {
                var p = w.Property; var bad = Material(); bad.Name = "INVALID"; bad.Plates.Add(new Plate());
                Assert.Throws<ArgumentException>(() => p.AssignMaterialProperties(new HashSet<MaterialProperty> { Material(), bad }));
                Assert.Throws<ArgumentOutOfRangeException>(() => p.AssignMaterialProperty(Material(), 513));
                Assert.Throws<ArgumentOutOfRangeException>(() => p.CreatePlateThicknessProperties(new HashSet<PlateThickness> { Thickness(), new PlateThickness { Thickness = double.NaN } }));
                var release = new Beam { Id = 17, StartRelease = Release(), EndRelease = Release() }; release.EndRelease.Springs.KMx = -1;
                Assert.Throws<ArgumentException>(() => p.CreateAndAssignMemberEndReleases(new HashSet<Beam> { release }));
                var shared = new Beam { Id = 17, StartRelease = Release() }; shared.EndRelease = shared.StartRelease;
                Assert.Throws<ArgumentException>(() => p.CreateAndAssignMemberEndReleases(new HashSet<Beam> { shared }));
                Assert.Throws<ArgumentNullException>(() => p.GetAllMaterialPropertiesWithPopulatedAssignments(null, null, null)); Assert.That(native.Calls, Is.Empty);
            }
        }
        [Test]
        public void DegreeWorkflowsRemainOnCallerThreadAndShareTheExistingQueryLock()
        {
            var native = new PropertyPortFixture.Native(); int thread = Thread.CurrentThread.ManagedThreadId; var seen = new List<int>(); native.OnCall = op => seen.Add(Thread.CurrentThread.ManagedThreadId);
            using (var w = Wrap(native)) { w.Property.AssignMaterialProperty(Material(), 4); w.Property.AssignThicknessProperty(Thickness(), 4); w.Property.CreateAndAssignMemberEndReleases(Beams(), 4); }
            Assert.That(seen.All(t => t == thread), Is.True);
            native = new PropertyPortFixture.Native();
            using (var w = Wrap(native))
            using (var entered = new ManualResetEventSlim())
            using (var release = new ManualResetEventSlim())
            using (var started = new ManualResetEventSlim())
            {
                native.OnCall = op => { if (op == "CreateIsotropicMaterialProperties") { entered.Set(); if (!release.Wait(5000)) throw new TimeoutException(); } };
                var first = Task.Run(() => w.Property.CreateMaterialPropertyNoConversion(Material())); Assert.That(entered.Wait(5000), Is.True);
                var second = Task.Run(() => { started.Set(); return w.Property.GetBetaAngle(17); }); Assert.That(started.Wait(5000), Is.True);
                try { Assert.That(second.Wait(50), Is.False); Assert.That(native.Calls, Is.EqualTo(new[] { "CreateIsotropicMaterialProperties" })); }
                finally { release.Set(); }
                Assert.That(Task.WaitAll(new Task[] { first, second }, 5000), Is.True);
            }
        }

        [Test]
        public void EditingAProjectionCannotSilentlyDiscardAZeroValuedNativePartialMode()
        {
            var packet = new MemberReleases(); packet.Parse(new[] { 0, 0, 0, 0, -3, 0 }, new double[6]);
            Assert.That(packet.MpFactor, Is.Zero); packet.Releases.Fx = true;
            Assert.That(packet.NativeSpecification, Is.Null); Assert.That(packet.ContainsPartialMomentModes, Is.True);
            Assert.That(packet.ReleaseType, Is.EqualTo(ReleaseType.MpOnly));
            var native = new PropertyPortFixture.Native();
            using (var w = Wrap(native)) Assert.Throws<NotSupportedException>(() => w.Property.CreateAndAssignMemberEndReleases(new HashSet<Beam> { new Beam { Id = 17, EndRelease = packet } }));
            Assert.That(native.Calls, Is.Empty);
            packet.Parse(new[] { 0, 0, 0, 0, 1, 0 }, new double[6]); Assert.That(packet.ContainsPartialMomentModes, Is.False);
        }

        [Test]
        public void ReleaseClassificationNotifiesBindingsOnceForEachProjectionEdit()
        {
            var packet = new MemberReleases(); var names = new List<string>(); packet.PropertyChanged += (s, e) => names.Add(e.PropertyName);
            packet.Releases.My = true;
            Assert.That(names.Count(n => n == nameof(MemberReleases.ReleaseType)), Is.EqualTo(1)); Assert.That(packet.ReleaseType, Is.EqualTo(ReleaseType.Release));
            names.Clear(); packet.Springs.KFx = 25;
            Assert.That(names.Count(n => n == nameof(MemberReleases.ReleaseType)), Is.EqualTo(1)); Assert.That(packet.ReleaseType, Is.EqualTo(ReleaseType.ReleaseWithSprings));
        }
    }
}
