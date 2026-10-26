using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Linq;
using NUnit.Framework;
using PackageVerification;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Common.Units;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Interfaces;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Sections;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSPropertySectionPortTests
    {
        private static readonly string[] Names = { "AssignSectionProperties", "AssignSectionProperty", "CreateAndAssignSectionProperties", "CreateMaterialProperties", "CreateMaterialProperty",
            "CreatePlateThicknessProperty", "CreateSectionProperties", "CreateSectionProperty", "GetAllSections", "GetAllSectionsWithAssignedBeamsList", "GetSectionalArea", "GetSectionBy",
            "GetSectionByPropertyIdWithAssignedBeamsList", "GetSectionPropertyCount", "GetThicknessPropertyCount", "GetCountofSectionPropertyValuesEx", "GetSectionPropertyType", "GetSectionPropertyCountry", "GetRecordForSection" };
        private static IEnumerable<MethodInfo> Methods => typeof(IOSProperty).GetMethods().Where(m => Names.Contains(m.Name) && (!m.Name.StartsWith("CreateMaterialPropert") || m.GetParameters().Length == 2));
        public static IEnumerable<TestCaseData> Ports => Methods.Select(m => new TestCaseData(m).SetName("{m}_" + m));
        private static OpenStaadWrapper Wrap(PropertySectionFixture.Native p, int major = 26) => new OpenStaadWrapper(new PropertySectionFixture.Root { Property = p, NativeMajor = major });
        private static HashSet<Beam> Beams() => new HashSet<Beam> { new Beam { Id = 17 }, new Beam { Id = 41 } };
        private static SectionProperty Section(RolledSection selected = null, TypeSpecification modifier = TypeSpecification.ST)
        {
            selected = selected ?? new RolledSectionHShape(); selected.Country = Country.Indian; selected.StaadName = "ISMB300"; selected.Designation = "ISMB300";
            var d = new SectionDefinition { TypeSpecification = modifier }; d.SectionProfile.SelectedSection = selected;
            if (d.AdditionalSpecifications is SpacedDoubleSectionsSpecifications spacing) spacing.Spacing = .1;
            if (d.AdditionalSpecifications is TopAndBottomCoverPlateSpecifications cover) { cover.Top.Width = .2; cover.Top.Thickness = .01; cover.Bottom.Width = .3; cover.Bottom.Thickness = .02; }
            if (d.AdditionalSpecifications is ConcreteCompositeSpecifications concrete) { concrete.Thickness = .15; concrete.CharacteristicStrength = 30000; concrete.Width = 1.2; concrete.Density = 25; }
            if (modifier == TypeSpecification.UserDefined) d.AdditionalSpecifications = selected is RolledSectionOShape ? (IAdditionalSpecifications)new PipeDefineSpecifications { OuterDiameter = .2, InnerDiameter = .18 } : new TubeDefineSpecifications { Thickness = .01, Width = .2, Depth = .3 };
            var section = new SectionProperty { Id = 37, SectionDefinition = d }; section.Beams.UnionWith(Beams()); return section;
        }
        private sealed class CandidateSection : IGenericSection
        {
            public int Id { get; set; } = 37;
            public int SectionRecNo => 42;
            public string StaadName => "ISMB300";
            public string PropertyName => StaadName;
            public Country Country => Country.Indian;
            public double Ax => .01;
            public SectionPropertyType SectionPropertyType => SectionPropertyType.BeamST;
            public double[] PropertyValues => new double[8];
            public HashSet<Beam> AssignedBeams { get; } = Beams();
        }
        private static object[] Args(MethodInfo m, OpenStaadWrapper w, PropertySectionFixture.Native native) => m.GetParameters().Select(p =>
            p.ParameterType == typeof(int) ? (object)(p.Name == "nThreads" ? 2 : p.Name == "Country" ? 10 : 37) : p.ParameterType == typeof(string) ? "ISMB300" :
            p.ParameterType == typeof(SectionProperty) ? (object)Section() : p.ParameterType == typeof(HashSet<SectionProperty>) ? new HashSet<SectionProperty> { Section() } :
            p.ParameterType == typeof(HashSet<IGenericSection>) ? (object)new HashSet<IGenericSection> { new CandidateSection() } : p.ParameterType == typeof(HashSet<Beam>) ? new HashSet<Beam> { new Beam { Id = 17, SectionProperty = Section() } } :
            p.ParameterType == typeof(IEnumerable<Beam>) ? (object)Beams() : p.ParameterType == typeof(IOSGeometry) ? w.Geometry : p.ParameterType == typeof(UnitsConverter) ? (object)new UnitsConverter(LengthInputUnit.Meter, ForceInputUnit.KiloNewton, LengthInputUnit.MilliMeter, ForceInputUnit.Newton) :
            p.ParameterType == typeof(MaterialProperty) ? new MaterialProperty("STEEL") : p.ParameterType == typeof(HashSet<MaterialProperty>) ? (object)new HashSet<MaterialProperty> { new MaterialProperty("STEEL") } :
            p.ParameterType == typeof(PlateThickness) ? new PlateThickness { Thickness = .2 } : (object)native).ToArray();
        private static object Invoke(IOSProperty p, MethodInfo method, object[] args)
        {
            try { return method.Invoke(p, args); } catch (TargetInvocationException e) { ExceptionDispatchInfo.Capture(e.InnerException).Throw(); throw; }
        }
        private static void Respond(PropertySectionFixture.Native p, string op, params object[] values) => p.Responses[op] = new Queue<object>(values);
        [Test]
        public void AddsExactlyNineteenMainAndSixNativeSignaturesOnBothSurfaces()
        {
            Assert.That(Methods.Count(), Is.EqualTo(25)); Assert.That(typeof(IOSProperty).GetMethods().Count(m => !m.IsSpecialName), Is.EqualTo(249));
            foreach (var m in Methods)
            {
                var impl = typeof(OSPropertyAdapter).GetMethod(m.Name, m.GetParameters().Select(p => p.ParameterType).ToArray());
                Assert.That(impl, Is.Not.Null); Assert.That(impl.ReturnType, Is.EqualTo(m.ReturnType));
                Assert.That(impl.GetParameters().Select(p => p.Name), Is.EqualTo(m.GetParameters().Select(p => p.Name)));
                Assert.That(impl.GetParameters().Select(p => p.DefaultValue), Is.EqualTo(m.GetParameters().Select(p => p.DefaultValue)));
                Assert.That(m.GetParameters().Any(p => p.IsOut || p.ParameterType.IsByRef), Is.False);
            }
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryPortRunsWithManagedEvidenceAndPreservesFluentIdentity(MethodInfo m)
        {
            var n = new PropertySectionFixture.Native(); using (var w = Wrap(n))
            { var result = Invoke(w.Property, m, Args(m, w, n)); Assert.That(result, m.ReturnType == typeof(IOSProperty) ? Is.SameAs(w.Property) : Is.Not.Null); Assert.That(n.Calls, Is.Not.Empty); Assert.That(n.Calls.Any(c => c.StartsWith("Save") || c.StartsWith("Analyze")), Is.False); }
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryPortRejectsDisposedBindingBeforeNativeAccess(MethodInfo m)
        {
            var n = new PropertySectionFixture.Native(); var w = Wrap(n); var p = w.Property; var args = Args(m, w, n); w.Dispose();
            Assert.Throws<InvalidOperationException>(() => Invoke(p, m, args)); Assert.That(n.Calls, Is.Empty);
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryPortStopsWhenTheCapturedBindingChanges(MethodInfo m)
        {
            var n = new PropertySectionFixture.Native(); var root = new PropertySectionFixture.Root { Property = n };
            using (var w = new OpenStaadWrapper(root))
            { var p = w.Property; var args = Args(m, w, n); n.OnCall = op => { n.OnCall = null; root.Property = new PropertySectionFixture.Native(); w.RefreshComObjects(); }; Assert.Throws<InvalidOperationException>(() => Invoke(p, m, args)); Assert.That(n.Calls.Count, Is.EqualTo(1)); }
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryPortPropagatesTransportFailures(MethodInfo m)
        {
            var n = new PropertySectionFixture.Native(); var error = new COMException("Transport", unchecked((int)0x80004005));
            using (var w = Wrap(n)) { var args = Args(m, w, n); n.OnCall = op => throw error; Assert.That(Assert.Throws<COMException>(() => Invoke(w.Property, m, args)), Is.SameAs(error)); }
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryPortShipsMatchingExpandedIntellisense(MethodInfo m)
        {
            var xml = XDocument.Load(Path.ChangeExtension(typeof(IOSProperty).Assembly.Location, ".xml")); string prefix = "M:StaadPro.Interop.Adapters.Interfaces.IOSProperty." + m.Name;
            var entries = xml.Descendants("member").Where(e => (string)e.Attribute("name") == prefix || ((string)e.Attribute("name")).StartsWith(prefix + "(")).ToArray();
            Assert.That(entries.Length, Is.EqualTo(typeof(IOSProperty).GetMethods().Count(x => x.Name == m.Name)));
            foreach (var entry in entries)
            {
                var id = ((string)entry.Attribute("name")).Replace("Interfaces.IOSProperty", "Models.OSPropertyAdapter");
                var concrete = xml.Descendants("member").Single(e => (string)e.Attribute("name") == id);
                Assert.That(concrete.Elements().Select(e => e.ToString()), Is.EqualTo(entry.Elements().Select(e => e.ToString())));
                foreach (string tag in new[] { "summary", "returns", "remarks", "example" }) Assert.That(entry.Element(tag)?.Value, Is.Not.Null.And.Not.Empty);
                Assert.That(entry.Element("remarks").Value, Does.Contain("When to use:")); Assert.That(entry.Descendants("include").Concat(entry.Descendants("inheritdoc")), Is.Empty);
                Assert.That(entry.Descendants().Attributes("cref").Any(a => a.Value.StartsWith("!:")), Is.False);
            }
        }
        [TestCase(24, 666, 7, "T")][TestCase(25, 666, 6, "Iz")][TestCase(26, 666, 6, "Iz")]
        [TestCase(24, 634, 10, "Bf")][TestCase(25, 634, 10, "D")][TestCase(26, 635, 11, "D")]
        [TestCase(24, 612, 11, "D")][TestCase(25, 612, 12, "D")][TestCase(26, 612, 12, "D")]
        [TestCase(24, 668, 6, "OD")][TestCase(25, 668, 7, "OD")]
        public void AcquiredNativeVersionSelectsTheReviewedLayoutAndRequiredSlots(int major, int type, int fields, string expected)
        {
            var n = new PropertySectionFixture.Native(); n.Definitions[37] = new PropertySectionFixture.Definition { Type = type, Country = 0, Name = "CUSTOM", Values = Enumerable.Repeat(1.0, fields).ToArray() };
            using (var w = Wrap(n, major))
            {
                var s = (GenericSection)w.Property.GetSectionBy(37); Assert.That(s.NativeMajorVersion, Is.EqualTo(major)); Assert.That(s.PropertyValueNames.Length, Is.EqualTo(fields));
                Assert.That(s.PropertyValueNames[type == 666 ? 3 : 1], Is.EqualTo(expected)); Assert.That(s.Ax, Is.EqualTo(1)); Assert.That(s.SectionRecNo, Is.EqualTo(-1));
                Assert.That(s.PropertyValues.Skip(fields).All(v => v == 0), Is.True);
            }
        }
        [TestCase(23)][TestCase(27)]
        public void UnreviewedNativeVersionsNeverGuessALayout(int major)
        { var n = new PropertySectionFixture.Native(); using (var w = Wrap(n, major)) Assert.Throws<NotSupportedException>(() => w.Property.GetSectionBy(37)); Assert.That(n.Calls, Does.Not.Contain("GetSectionPropertyValuesEx")); }
        [Test]
        public void MissingApplicableSlotsCannotBeMistakenForUnusedPadding()
        {
            var n = new PropertySectionFixture.Native(); n.Definitions[37] = new PropertySectionFixture.Definition { Type = 612, Country = 0, Name = "CM", Values = Enumerable.Repeat(1.0, 11).ToArray() };
            using (var w = Wrap(n, 24)) Assert.That(w.Property.GetSectionBy(37), Is.Not.Null);
            using (var w = Wrap(n, 25)) Assert.Throws<InvalidOperationException>(() => w.Property.GetSectionBy(37));
        }
        [TestCase(616, 8)][TestCase(698, 10)]
        public void LayoutsWithoutAxReturnNullableAbsenceWithoutFabricatedArea(int type, int fields)
        {
            var n = new PropertySectionFixture.Native(); n.Definitions[37] = new PropertySectionFixture.Definition { Type = type, Country = 0, Name = "NO AX", Values = Enumerable.Repeat(1.0, fields).ToArray() };
            using (var w = Wrap(n)) { Assert.That(w.Property.GetSectionalArea(37), Is.Null); var s = (GenericSection)w.Property.GetSectionBy(37); Assert.That(s.HasSectionalArea, Is.False); Assert.Throws<NotSupportedException>(() => { var unused = s.Ax; }); }
        }
        [Test]
        public void TaperedAreaUsesTheSourceMeanOfStartAndEndPlateAreas()
        {
            var n = new PropertySectionFixture.Native(); n.Definitions[37] = new PropertySectionFixture.Definition { Type = 680, Country = 0, Name = "TAPER", Values = new[] { .6, .01, .8, .25, .02, .3, .03 } };
            double expected = (.25 * .02 + .3 * .03) + ((.6 + .8) / 2 - .02 - .03) * .01;
            using (var w = Wrap(n)) Assert.That(w.Property.GetSectionalArea(37), Is.EqualTo(expected).Within(1e-12));
        }
        [Test]
        public void PacketsOwnTheirBuffersAndActualAssignmentsPreserveCandidateIdentity()
        {
            var n = new PropertySectionFixture.Native(); var beams = Beams();
            using (var w = Wrap(n))
            {
                var s = (GenericSection)w.Property.GetSectionByPropertyIdWithAssignedBeamsList(37, beams);
                Assert.That(s.AssignedBeams.Select(b => b.Id), Is.EquivalentTo(new[] { 17, 41 })); Assert.That(s.AssignedBeams.All(b => beams.Any(original => ReferenceEquals(original, b))), Is.True);
                s.PropertyValues[0] = 99; ((Array)n.Buffers["GetSectionPropertyValuesEx"]).SetValue(88.0, 0); Assert.That(s.Ax, Is.EqualTo(.01)); Assert.That(s.PropertyValues[0], Is.EqualTo(.01));
                Assert.That(w.Property.GetSectionByPropertyIdWithAssignedBeamsList(37, w.Geometry).AssignedBeams.Count, Is.EqualTo(2));
                var all = w.Property.GetAllSectionsWithAssignedBeamsList(beams); Assert.That(all.Count, Is.EqualTo(2)); Assert.That(all.Single(x => x.Id == 38).AssignedBeams, Is.Empty);
            }
        }
        public static IEnumerable<TestCaseData> BadBuffers
        { get { foreach (string kind in new[] { "null", "type", "rank", "short", "long", "unwritten", "infinity", "bad-padding" }) yield return new TestCaseData(kind); } }
        [TestCaseSource(nameof(BadBuffers))]
        public void MalformedSectionBuffersFailInsteadOfExposingDefaults(string kind)
        {
            var n = new PropertySectionFixture.Native(); n.Output = (op, value) =>
            {
                if (op != "GetSectionPropertyValuesEx") return value; var a = (Array)value;
                switch (kind)
                { case "null": return null; case "type": return new object[a.Length]; case "rank": return new double[a.Length, 1]; case "short": return new double[a.Length - 1]; case "long": return new double[a.Length + 1]; case "unwritten": a.SetValue(double.NaN, 0); break; case "infinity": a.SetValue(double.PositiveInfinity, 0); break; default: a.SetValue(double.PositiveInfinity, a.Length - 1); break; } return a;
            };
            using (var w = Wrap(n)) Assert.Throws<InvalidOperationException>(() => w.Property.GetSectionBy(37));
        }
        [TestCase(0)][TestCase(-1)][TestCase(100001)][TestCase(true)][TestCase(24.0)][TestCase("24")][TestCase(null)]
        public void InvalidCapacityNeverReachesBufferRetrieval(object value)
        { var n = new PropertySectionFixture.Native(); Respond(n, "GetCountofSectionPropertyValuesEx", value); using (var w = Wrap(n)) Assert.Throws<InvalidOperationException>(() => w.Property.GetSectionBy(37)); Assert.That(n.Calls, Does.Not.Contain("GetSectionPropertyValuesEx")); }
        [TestCase("GetSectionPropertyType", 610, 672)][TestCase("GetSectionPropertyCountry", 10, 11)][TestCase("GetCountofSectionPropertyValuesEx", 24, 25)]
        public void ChangedMetadataOrCapacityIsRejected(string op, object before, object after)
        { var n = new PropertySectionFixture.Native(); Respond(n, op, before, after); using (var w = Wrap(n)) Assert.Throws<InvalidOperationException>(() => w.Property.GetSectionBy(37)); }
        [Test]
        public void NativeRecordZeroAndMinusOneArePreservedWithoutIndexGuessing()
        {
            var n = new PropertySectionFixture.Native(); Respond(n, "GetRecordForSection", 0, -1, -1);
            using (var w = Wrap(n)) { Assert.That(w.Property.GetRecordForSection(10, "ISMB300"), Is.Zero); Assert.That(w.Property.GetRecordForSection(10, "MISSING"), Is.EqualTo(-1)); Assert.That(w.Property.GetSectionBy(37).SectionRecNo, Is.EqualTo(-1)); }
        }
        public static IEnumerable<TestCaseData> ScalarErrors
        { get { foreach (string op in new[] { "GetSectionPropertyCount", "GetThicknessPropertyCount", "GetCountofSectionPropertyValuesEx", "GetSectionPropertyType", "GetSectionPropertyCountry", "GetRecordForSection" }) foreach (object value in new object[] { true, 1.0, "1", null, -6025 }) yield return new TestCaseData(op, value); } }
        [TestCaseSource(nameof(ScalarErrors))]
        public void ScalarNativeObservationsRejectErrorsAndCoercion(string op, object raw)
        {
            var n = new PropertySectionFixture.Native(); Respond(n, op, raw); using (var w = Wrap(n)) { var m = Methods.Single(x => x.Name == op); Assert.Throws<InvalidOperationException>(() => Invoke(w.Property, m, Args(m, w, n))); }
        }
        public static IEnumerable<TestCaseData> CreationRoutes
        {
            get
            {
                foreach (var pair in new[] { Tuple.Create("H", TypeSpecification.ST, "CreateBeamPropertyFromTable"), Tuple.Create("H", TypeSpecification.T, "CreateTeePropertyFromTable"), Tuple.Create("H", TypeSpecification.D, "CreateBeamPropertyFromTable"), Tuple.Create("H", TypeSpecification.TC, "CreateBeamPropertyFromTableWithCoverPlates"), Tuple.Create("H", TypeSpecification.BC, "CreateBeamPropertyFromTableWithCoverPlates"), Tuple.Create("H", TypeSpecification.TB, "CreateBeamPropertyFromTableWithCoverPlates"), Tuple.Create("H", TypeSpecification.CM, "CreateBeamPropertyFromTableComposite"), Tuple.Create("C", TypeSpecification.ST, "CreateChannelPropertyFromTable"), Tuple.Create("C", TypeSpecification.D, "CreateChannelPropertyFromTable"), Tuple.Create("C", TypeSpecification.FR, "CreateChannelPropertyFromTable"), Tuple.Create("C", TypeSpecification.BA, "CreateChannelPropertyFromTable"), Tuple.Create("L", TypeSpecification.ST, "CreateAnglePropertyFromTable"), Tuple.Create("L", TypeSpecification.RA, "CreateAnglePropertyFromTable"), Tuple.Create("L", TypeSpecification.SA, "CreateAnglePropertyFromTable"), Tuple.Create("L", TypeSpecification.LD, "CreateAnglePropertyFromTable"), Tuple.Create("L", TypeSpecification.SD, "CreateAnglePropertyFromTable"), Tuple.Create("O", TypeSpecification.ST, "CreatePipePropertyFromTable"), Tuple.Create("O", TypeSpecification.UserDefined, "CreatePipePropertyFromTable"), Tuple.Create("B", TypeSpecification.ST, "CreateTubePropertyFromTable"), Tuple.Create("B", TypeSpecification.UserDefined, "CreateTubePropertyFromTable") }) yield return new TestCaseData(pair.Item1, pair.Item2, pair.Item3);
            }
        }
        private static RolledSection Shape(string family) => family == "H" ? (RolledSection)new RolledSectionHShape() : family == "C" ? new RolledSectionCShape() : family == "L" ? (RolledSection)new RolledSectionLShape() : family == "O" ? new RolledSectionOShape() : (RolledSection)new RolledSectionBoxShape();
        [TestCaseSource(nameof(CreationRoutes))]
        public void SourceProfileFamiliesUseTheCorrectNativeRoutesAndKeepPositiveIds(string family, TypeSpecification modifier, string op)
        {
            var n = new PropertySectionFixture.Native(); var s = Section(Shape(family), modifier);
            using (var w = Wrap(n)) w.Property.CreateSectionProperty(s);
            Assert.That(n.Calls, Is.EqualTo(new[] { op })); Assert.That(n.Arguments[0].Take(3), Is.EqualTo(new object[] { 10, "ISMB300", (int)modifier })); Assert.That(s.Id, Is.GreaterThan(0));
            if (modifier == TypeSpecification.BC) Assert.That(n.Arguments[0][3], Is.EqualTo(new[] { .3, .02 }));
            if (modifier == TypeSpecification.CM) Assert.That(n.Arguments[0][3], Is.EqualTo(new[] { .15, 30000, 1.2, 25 }));
            if (family == "O" && modifier == TypeSpecification.UserDefined) Assert.That(n.Arguments[0].Skip(3), Is.EqualTo(new object[] { .2, .18 }));
            if (family == "B" && modifier == TypeSpecification.UserDefined) Assert.That(n.Arguments[0].Skip(3), Is.EqualTo(new object[] { .01, .2, .3 }));
        }
        [TestCase(0)][TestCase(-6004)][TestCase(true)]
        public void FailedCreationNeverOverwritesAnExistingSectionId(object raw)
        { var n = new PropertySectionFixture.Native(); var s = Section(); Respond(n, "CreateBeamPropertyFromTable", raw); using (var w = Wrap(n)) Assert.Throws<InvalidOperationException>(() => w.Property.CreateSectionProperty(s)); Assert.That(s.Id, Is.EqualTo(37)); }
        [TestCase(false)][TestCase(true)]
        public void SectionPipelineGroupsDefinitionsAndPropagatesIdsBeforeAssigning(bool degree)
        {
            var n = new PropertySectionFixture.Native(); var first = Section(); var equal = Section(); first.Beams.Clear(); equal.Beams.Clear(); first.Beams.Add(new Beam { Id = 99 });
            var beams = new HashSet<Beam> { new Beam { Id = 17, SectionProperty = first }, new Beam { Id = 41, SectionProperty = equal } };
            using (var w = Wrap(n)) { if (degree) w.Property.CreateAndAssignSectionProperties(beams, 4); else w.Property.CreateAndAssignSectionProperties(beams); }
            Assert.That(n.Calls.Count(c => c == "CreateBeamPropertyFromTable"), Is.EqualTo(1)); Assert.That(first.Id, Is.EqualTo(91)); Assert.That(equal.Id, Is.EqualTo(91));
            Assert.That(first.Beams.Select(b => b.Id), Is.EquivalentTo(new[] { 99, 17, 41 })); Assert.That(n.Arguments.Skip(1).All(a => degree ? a[0] is int : a[0] is int[]), Is.True);
        }
        [Test]
        public void CreationBatchesPrevalidateAndSnapshotAllValuesBeforeTheFirstWrite()
        {
            var n = new PropertySectionFixture.Native(); var first = Section(); var second = Section(new RolledSectionOShape(), TypeSpecification.UserDefined);
            using (var w = Wrap(n))
            {
                var dimensions = (PipeDefineSpecifications)second.SectionDefinition.AdditionalSpecifications; dimensions.InnerDiameter = .3;
                Assert.Throws<ArgumentException>(() => w.Property.CreateSectionProperties(new HashSet<SectionProperty> { first, second })); Assert.That(n.Calls, Is.Empty);
                dimensions.InnerDiameter = .18; n.OnCall = op => { if (op == "CreateBeamPropertyFromTable") dimensions.OuterDiameter = double.NaN; };
                w.Property.CreateSectionProperties(new HashSet<SectionProperty> { first, second }); Assert.That(n.Arguments[1][3], Is.EqualTo(.2));
            }
        }
        [Test]
        public void AssignmentsUseArrayAndScalarRoutesAndDoNotReadCreationGraphs()
        {
            var n = new PropertySectionFixture.Native(); var s = new SectionProperty { Id = 37 }; s.Beams.UnionWith(Beams());
            using (var w = Wrap(n)) { w.Property.AssignSectionProperty(s); Assert.That(n.Arguments[0][0], Is.TypeOf<int[]>()); w.Property.AssignSectionProperty(s, 2); Assert.That(n.Arguments.Skip(1).All(a => a[0] is int), Is.True); Respond(n, "AssignBeamProperty", -6001); Assert.Throws<InvalidOperationException>(() => w.Property.AssignSectionProperty(s)); }
        }
        [Test]
        public void ConverterCreationScalesOnlyModuliAndWeightDensityWithoutChangingThePacket()
        {
            var n = new PropertySectionFixture.Native(); var m = new MaterialProperty("STEEL") { Id = 73, E = 200000000, Poisson = .3, Density = 77, Alpha = 12e-6, Damping = .02 };
            var uc = new UnitsConverter(LengthInputUnit.Meter, ForceInputUnit.KiloNewton, LengthInputUnit.MilliMeter, ForceInputUnit.Newton);
            using (var w = Wrap(n)) w.Property.CreateMaterialProperty(m, uc);
            Assert.That(n.Arguments.Single(), Is.EqualTo(new object[] { m.Name, m.E * .001, m.Poisson, m.G * .001, m.Density * .000001, m.Alpha, m.Damping }).Within(1e-9));
            Assert.That(m.Id, Is.EqualTo(73)); Assert.That(m.E, Is.EqualTo(200000000)); Assert.That(m.Density, Is.EqualTo(77)); Assert.That(m.LastCreationStatus, Is.EqualTo(MaterialCreationStatus.Created));
            Assert.That(uc.ElasticModulusConversionFactor, Is.EqualTo(uc.StressConversionFactor)); Assert.That(uc.DensityConversionFactor, Is.EqualTo(.000001));
        }
        [Test]
        public void ExplicitReceiverMustMatchTheCapturedBindingAndKeepsTheScalarThicknessRoute()
        {
            var n = new PropertySectionFixture.Native(); var t = new PlateThickness { Thickness = .2, Id = 45 };
            using (var w = Wrap(n)) { Assert.Throws<ArgumentException>(() => w.Property.CreatePlateThicknessProperty(new PropertySectionFixture.Native(), t)); Assert.That(n.Calls, Is.Empty); w.Property.CreatePlateThicknessProperty(w.Property.ComObject, t); Assert.That(t.Id, Is.EqualTo(94)); Assert.That(n.Arguments.Single()[0], Is.EqualTo(.2)); }
        }
        [Test]
        public void InvalidInputsAndNoOpAssignmentsDoNotReachNativeCode()
        {
            var n = new PropertySectionFixture.Native(); using (var w = Wrap(n))
            {
                var p = w.Property; p.AssignSectionProperties((HashSet<SectionProperty>)null); p.AssignSectionProperties((HashSet<IGenericSection>)null); p.AssignSectionProperty(null); p.CreateAndAssignSectionProperties(new HashSet<Beam>());
                Assert.Throws<InvalidOperationException>(() => p.CreateSectionProperties(null)); Assert.Throws<ArgumentException>(() => p.CreateSectionProperty(new SectionProperty()));
                Assert.Throws<ArgumentOutOfRangeException>(() => p.GetSectionBy(0)); Assert.Throws<ArgumentOutOfRangeException>(() => p.AssignSectionProperty(Section(), 513));
                Assert.Throws<ArgumentException>(() => p.CreateMaterialProperty(new MaterialProperty("STEEL"), new UnitsConverter((LengthInputUnit)999)));
                Assert.Throws<ArgumentNullException>(() => p.CreateMaterialProperty(new MaterialProperty("STEEL"), (UnitsConverter)null)); Assert.That(n.Calls, Is.Empty);
            }
        }
        [Test]
        public void DegreePipelinesStayOnTheEstablishedCallerThread()
        { var n = new PropertySectionFixture.Native(); int caller = Thread.CurrentThread.ManagedThreadId; var seen = new List<int>(); n.OnCall = op => seen.Add(Thread.CurrentThread.ManagedThreadId); using (var w = Wrap(n)) w.Property.CreateAndAssignSectionProperties(new HashSet<Beam> { new Beam { Id = 17, SectionProperty = Section() } }, 4); Assert.That(seen.All(id => id == caller), Is.True); }

        [Test]
        public void NativeVersionMetadataIsReadAfterInitializationAndCheckedAgainBeforeReturning()
        {
            var n = new PropertySectionFixture.Native(); var root = new PropertySectionFixture.Root { Property = n, NativeMajor = 26 };
            using (var w = new OpenStaadWrapper(root))
            {
                Assert.That(((GenericSection)w.Property.GetSectionBy(37)).NativeMajorVersion, Is.EqualTo(26));
                var p = w.Property; n.OnCall = op => { if (op == "GetSectionPropertyName") { n.OnCall = null; root.NativeMajor = 24; w.RefreshComObjects(); } };
                Assert.Throws<InvalidOperationException>(() => p.GetSectionBy(37));
            }
        }
    }
}
