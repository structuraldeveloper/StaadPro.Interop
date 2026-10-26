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
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Property;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSPropertyCreationTests
    {
        private static readonly string[] Names = { "CreateWideFlangePropertyFromTable", "CreateBeamPropertyFromTableEx", "CreatePrismaticRectangleProperty", "CreatePrismaticCircleProperty",
            "CreatePrismaticTeeProperty", "CreatePrismaticTrapezoidalProperty", "CreatePrismaticGeneralProperty", "CreateTaperedTubeProperty", "CreateMemberPartialReleaseSpec",
            "CreateMemberTrussSpec", "CreateMemberInactiveSpec", "CreateMemberTensionSpec", "CreateMemberCompressionSpec", "CreateMemberIgnoreStiffSpec", "CreateMemberCableSpec",
            "CreateMemberCableSpecEx", "CreateElementNodeReleaseSpec", "CreateElementPlaneStressSpec", "CreateElementIgnoreInplaneRotnSpec", "AddControlDependentRelation",
            "CreateElementOffsetSpec", "CreateElementLocalZOffsetSpec", "CreateMemberFireProofingSpec", "GetPropertyUniqueID", "SetPropertyUniqueID" };
        public static IEnumerable<TestCaseData> Ports => Names.Select(n => new TestCaseData(n));
        private static MethodInfo Method(string name) => typeof(IOSProperty).GetMethods().Single(m => m.Name == name);
        private static DegreeOfFreedomSelection Dofs() => new DegreeOfFreedomSelection(true, false, true, false, true, false);
        private static PartialReleaseFactors Factors() => new PartialReleaseFactors(.1, .2, .3, .4, .5, .6);
        private static GeneralPrismaticSectionDefinition General() => new GeneralPrismaticSectionDefinition(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        private static OpenStaadWrapper Wrap(PropertyCreationFixture.Native n) => new OpenStaadWrapper(new PropertyCreationFixture.Root { Property = n });
        private static object[] Args(string name) => Method(name).GetParameters().Select(p =>
            p.ParameterType == typeof(WideFlangeTableDefinition) ? (object)WideFlangeTableDefinition.Standard(Country.Indian, "ISMB300") :
            p.ParameterType == typeof(GeneralPrismaticSectionDefinition) ? General() : p.ParameterType == typeof(DegreeOfFreedomSelection) ? (object)Dofs() :
            p.ParameterType == typeof(PartialReleaseFactors) ? Factors() : p.ParameterType == typeof(Country) ? (object)Country.Indian :
            p.ParameterType == typeof(TableSolidShape) ? TableSolidShape.SolidRectangle : p.ParameterType == typeof(TaperedTubeShape) ? (object)TaperedTubeShape.Round :
            p.ParameterType == typeof(MemberEnd) ? MemberEnd.End : p.ParameterType == typeof(CableDefinitionKind) ? (object)CableDefinitionKind.InitialTension :
            p.ParameterType == typeof(CableTensionEnd) ? CableTensionEnd.Start : p.ParameterType == typeof(ControlDependencyMode) ? (object)ControlDependencyMode.SpecifiedDegreesOfFreedom :
            p.ParameterType == typeof(ElementNodeOffsetAxis) ? ElementNodeOffsetAxis.Local : p.ParameterType == typeof(FireProofingType) ? (object)FireProofingType.Block :
            p.ParameterType == typeof(IEnumerable<int>) ? new[] { 13, 12, 13 } : p.ParameterType == typeof(int) ? (object)(p.Name == "plateNodeIndex" ? 2 : p.Name == "controlNodeId" ? 11 : 37) :
            p.ParameterType == typeof(string) ? " exact ":
            (p.Name == "stemDepth" ? (object).45 : p.Name == "stemWidth" ? .02 : p.Name == "thickness" ? .01 : p.Name == "totalDepth" ? .5 : p.Name == "flangeWidth" ? .3 : .2)).ToArray();
        private static object Invoke(IOSProperty p, string name, object[] args = null)
        { try { return Method(name).Invoke(p, args ?? Args(name)); } catch (TargetInvocationException e) { ExceptionDispatchInfo.Capture(e.InnerException).Throw(); throw; } }
        private static void Respond(PropertyCreationFixture.Native n, string op, params object[] values) => n.Responses[op] = new Queue<object>(values);
        [Test]
        public void ExactlyTwentyFiveAdditionsPreserveMatchingPublicContractsWithoutRefOutOrRawPackets()
        {
            Assert.That(Names.Distinct().Count(), Is.EqualTo(25)); Assert.That(typeof(IOSProperty).GetMethods().Count(m => !m.IsSpecialName), Is.EqualTo(249));
            foreach (string name in Names)
            {
                var m = Method(name); var impl = typeof(OSPropertyAdapter).GetMethod(name, m.GetParameters().Select(p => p.ParameterType).ToArray());
                Assert.That(impl.ReturnType, Is.EqualTo(m.ReturnType)); Assert.That(impl.GetParameters().Select(p => p.Name), Is.EqualTo(m.GetParameters().Select(p => p.Name)));
                Assert.That(impl.GetParameters().Select(p => p.DefaultValue), Is.EqualTo(m.GetParameters().Select(p => p.DefaultValue)));
                Assert.That(m.GetParameters().Any(p => p.ParameterType.IsByRef || p.IsOut || p.ParameterType.IsArray || p.ParameterType == typeof(object)), Is.False);
            }
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryRouteRunsOnTheCallerThreadWithQualifiedEvidence(string name)
        {
            var n = new PropertyCreationFixture.Native(); using (var w = Wrap(n))
            {
                var r = Invoke(w.Property, name); var mutation = r as PropertyMutationResult;
                if (mutation != null) Assert.That(mutation.Status, name == "SetPropertyUniqueID" ? Is.EqualTo(PropertyMutationStatus.InvokedWithoutAcknowledgement) : Is.EqualTo(PropertyMutationStatus.Created).Or.EqualTo(PropertyMutationStatus.Succeeded));
                else Assert.That(((PropertyQueryResult<PropertyUniqueIdentifier>)r).IsSuccessful, Is.True);
                Assert.That(n.Calls, Is.EqualTo(new[] { name })); Assert.That(n.Threads, Is.EqualTo(new[] { Thread.CurrentThread.ManagedThreadId }));
            }
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryRouteRejectsDisposedBindingsBeforeNativeAccess(string name)
        { var n = new PropertyCreationFixture.Native(); var w = Wrap(n); var p = w.Property; w.Dispose(); Assert.Throws<InvalidOperationException>(() => Invoke(p, name)); Assert.That(n.Calls, Is.Empty); }
        [TestCaseSource(nameof(Ports))]
        public void EveryRouteDetectsReplacedBindingAfterOneDispatchWithoutRetry(string name)
        {
            var n = new PropertyCreationFixture.Native(); var root = new PropertyCreationFixture.Root { Property = n }; using (var w = new OpenStaadWrapper(root))
            { var p = w.Property; n.OnCall = op => { n.OnCall = null; root.Property = new PropertyCreationFixture.Native(); w.RefreshComObjects(); }; Assert.Throws<InvalidOperationException>(() => Invoke(p, name)); Assert.That(n.Calls.Count, Is.EqualTo(1)); }
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryRoutePropagatesOriginalTransportFailures(string name)
        {
            var n = new PropertyCreationFixture.Native(); var error = new COMException("Transport", unchecked((int)0x80004005)); n.OnCall = op => throw error;
            using (var w = Wrap(n)) { Assert.That(Assert.Throws<COMException>(() => Invoke(w.Property, name)), Is.SameAs(error)); Assert.That(n.Calls.Count, Is.EqualTo(1)); }
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryRouteShipsMatchingExpandedXmlWithGuardedMutationExamples(string name)
        {
            var xml = XDocument.Load(Path.ChangeExtension(typeof(IOSProperty).Assembly.Location, ".xml"));
            string prefix = "M:StaadPro.Interop.Adapters.Interfaces.IOSProperty." + name;
            var entry = xml.Descendants("member").Single(e => (string)e.Attribute("name") == prefix || ((string)e.Attribute("name")).StartsWith(prefix + "("));
            var impl = xml.Descendants("member").Single(e => (string)e.Attribute("name") == ((string)entry.Attribute("name")).Replace("Interfaces.IOSProperty", "Models.OSPropertyAdapter"));
            Assert.That(impl.Elements().Select(e => e.ToString()), Is.EqualTo(entry.Elements().Select(e => e.ToString())));
            foreach (string tag in new[] { "summary", "returns", "remarks", "example" }) Assert.That(entry.Element(tag)?.Value, Is.Not.Null.And.Not.Empty);
            Assert.That(entry.Elements("param").Select(e => (string)e.Attribute("name")), Is.EqualTo(Method(name).GetParameters().Select(p => p.Name)));
            Assert.That(entry.Element("remarks").Value, Does.Contain("When to use:")); Assert.That(entry.Descendants("include").Concat(entry.Descendants("inheritdoc")), Is.Empty);
            Assert.That(entry.Element("example").Value, name == "GetPropertyUniqueID" ? Does.Contain("IsSuccessful") : Does.Contain("bool applyChanges = false"));
        }
        [Test]
        public void InstalledFixtureCallsAllNewMethodsIncludingTwentyFourMutations()
        { var n = new PropertyCreationFixture.Native(); using (var w = Wrap(n)) { PropertyCreationFixture.VerifyCalls(w.Property, n); Assert.That(n.Calls.Count, Is.EqualTo(25)); Assert.That(n.Calls.Distinct(), Is.EquivalentTo(Names)); } }
        public static IEnumerable<TestCaseData> KnownFailures
        {
            get
            {
                yield return new TestCaseData("CreateWideFlangePropertyFromTable", -1, PropertyMutationStatus.NativeFailure);
                foreach (string name in Names.Skip(1).Take(7)) yield return new TestCaseData(name, 0, PropertyMutationStatus.NativeFailure);
                foreach (int code in new[] { -106, -108 }) yield return new TestCaseData("CreatePrismaticGeneralProperty", code, PropertyMutationStatus.InvalidInput);
                foreach (string name in new[] { "CreateMemberPartialReleaseSpec", "CreateElementNodeReleaseSpec" })
                { foreach (int code in new[] { -106, -108 }) yield return new TestCaseData(name, code, PropertyMutationStatus.InvalidInput); yield return new TestCaseData(name, name == "CreateMemberPartialReleaseSpec" ? -6020 : -6021, PropertyMutationStatus.NativeFailure); }
                string[] ids = { "CreateMemberTrussSpec", "CreateMemberInactiveSpec", "CreateMemberTensionSpec", "CreateMemberCompressionSpec", "CreateMemberIgnoreStiffSpec", "CreateMemberCableSpec", "CreateMemberCableSpecEx", "CreateElementPlaneStressSpec", "CreateElementIgnoreInplaneRotnSpec", "CreateElementOffsetSpec", "CreateElementLocalZOffsetSpec" };
                int[] errors = { -6010, -6011, -6012, -6013, -6014, -6015, -6015, -6018, -6019, -6016, -6016 };
                for (int i = 0; i < ids.Length; i++) yield return new TestCaseData(ids[i], errors[i], PropertyMutationStatus.NativeFailure);
                yield return new TestCaseData("AddControlDependentRelation", -106, PropertyMutationStatus.InvalidInput); yield return new TestCaseData("AddControlDependentRelation", -6029, PropertyMutationStatus.NativeFailure);
                yield return new TestCaseData("CreateMemberFireProofingSpec", -1, PropertyMutationStatus.NativeFailure);
            }
        }
        [TestCaseSource(nameof(KnownFailures))]
        public void NativeFailuresRetainActualCodeWithoutAnyCreatedIdentity(string name, int code, PropertyMutationStatus status)
        {
            var n = new PropertyCreationFixture.Native(); Respond(n, name, code); using (var w = Wrap(n))
            { var r = (PropertyMutationResult)Invoke(w.Property, name); Assert.That(r.Status, Is.EqualTo(status)); Assert.That(r.IsSuccessful, Is.False); Assert.That(r.NativeReturnCode, Is.EqualTo(code)); Assert.That(r.NativeBooleanReturn, Is.Null);
                Assert.That(r.CreatedPropertyId, Is.Null); Assert.That(r.CreatedSpecificationId, Is.Null); Assert.That(r.CreatedSpecificationIndex, Is.Null); }
        }
        [TestCaseSource(nameof(Ports))]
        public void IntegralAndTextContractsRejectMalformedReturns(string name)
        {
            if (name == "SetPropertyUniqueID") return; // Native void has no return value to validate.
            foreach (object bad in new object[] { true, .5, "9", null, -9999 })
            {
                if (name == "GetPropertyUniqueID" && (bad == null || bad is string)) continue;
                var n = new PropertyCreationFixture.Native(); Respond(n, name, new[] { bad }); using (var w = Wrap(n)) Assert.Throws<InvalidOperationException>(() => Invoke(w.Property, name));
            }
        }
        [TestCase("CreateMemberPartialReleaseSpec")][TestCase("CreateElementNodeReleaseSpec")][TestCase("AddControlDependentRelation")]
        public void StatusOnlyCreatorsNeverTreatPositiveReturnsAsIds(string name)
        { var n = new PropertyCreationFixture.Native(); Respond(n, name, 0, 901); using (var w = Wrap(n)) { var r = (PropertyMutationResult)Invoke(w.Property, name); Assert.That(r.IsSuccessful, Is.True); Assert.That(r.CreatedSpecificationId, Is.Null); Assert.Throws<InvalidOperationException>(() => Invoke(w.Property, name)); } }
        [TestCase("CreateMemberTrussSpec")][TestCase("CreateMemberInactiveSpec")][TestCase("CreateMemberTensionSpec")][TestCase("CreateMemberCompressionSpec")]
        [TestCase("CreateMemberIgnoreStiffSpec")][TestCase("CreateMemberCableSpec")][TestCase("CreateMemberCableSpecEx")][TestCase("CreateElementPlaneStressSpec")]
        [TestCase("CreateElementIgnoreInplaneRotnSpec")][TestCase("CreateElementOffsetSpec")][TestCase("CreateElementLocalZOffsetSpec")]
        public void SpecificationIdsArePositiveAndDistinctFromSectionIdsAndIndices(string name)
        { var n = new PropertyCreationFixture.Native(); Respond(n, name, 901, 0); using (var w = Wrap(n)) { var r = (PropertyMutationResult)Invoke(w.Property, name); Assert.That(r.CreatedSpecificationId, Is.EqualTo(901)); Assert.That(r.CreatedPropertyId, Is.Null); Assert.That(r.CreatedSpecificationIndex, Is.Null); Assert.Throws<InvalidOperationException>(() => Invoke(w.Property, name)); } }
        [TestCase(0)][TestCase(1)][TestCase(101)]
        public void FireProofingReturnsAnUnmodifiedZeroBasedIndex(int index)
        { var n = new PropertyCreationFixture.Native(); Respond(n, "CreateMemberFireProofingSpec", index); using (var w = Wrap(n)) { var r = w.Property.CreateMemberFireProofingSpec(FireProofingType.Contour, .02, 0); Assert.That(r.CreatedSpecificationIndex, Is.EqualTo(index)); Assert.That(r.CreatedSpecificationId, Is.Null); Assert.That(r.NativeReturnCode, Is.EqualTo(index)); Assert.That(n.Arguments.Single(), Is.EqualTo(new object[] { 2, .02, 0.0 })); } }
        [Test]
        public void VoidUniqueIdSetterKeepsExactRequestedIdentityWithoutAcknowledgementOrHiddenReadback()
        {
            var n = new PropertyCreationFixture.Native(); using (var w = Wrap(n))
            {
                foreach (string value in new[] { "", "  ", " not-a-guid " })
                { n.Calls.Clear(); var r = w.Property.SetPropertyUniqueID(37, value); Assert.That(r.Status, Is.EqualTo(PropertyMutationStatus.InvokedWithoutAcknowledgement)); Assert.That(r.IsSuccessful, Is.False);
                    Assert.That(r.PropertyId, Is.EqualTo(37)); Assert.That(r.UniqueId, Is.EqualTo(value)); Assert.That(r.NativeReturnCode, Is.Null); Assert.That(r.NativeBooleanReturn, Is.Null); Assert.That(n.Calls, Is.EqualTo(new[] { "SetPropertyUniqueID" })); }
                Assert.Throws<ArgumentNullException>(() => w.Property.SetPropertyUniqueID(37, null)); Assert.Throws<ArgumentException>(() => w.Property.SetPropertyUniqueID(37, "a\0b"));
                var read = w.Property.GetPropertyUniqueID(37); Assert.That(read.Data.PropertyId, Is.EqualTo(37)); Assert.That(read.Data.Value, Is.EqualTo(" not-a-guid "));
                Respond(n, "GetPropertyUniqueID", null, DBNull.Value, ""); for (int i = 0; i < 3; i++) Assert.That(w.Property.GetPropertyUniqueID(37).Status, Is.EqualTo(PropertyQueryStatus.EmptyOrUnavailable));
            }
        }
        [Test]
        public void AllWideFlangeFactoriesKeepApplicableSlotsAndActualBottomPlateSelector()
        {
            var defs = new[] { WideFlangeTableDefinition.Standard(Country.Indian, " N "), WideFlangeTableDefinition.Double(Country.Indian, " N ", .1), WideFlangeTableDefinition.Tee(Country.Indian, " N "),
                WideFlangeTableDefinition.Composite(Country.Indian, " N ", .2, 30000, .4, 25), WideFlangeTableDefinition.WithTopPlate(Country.Indian, " N ", .2, .01), WideFlangeTableDefinition.WithBottomPlate(Country.Indian, " N ", .3, .02), WideFlangeTableDefinition.WithTopAndBottomPlates(Country.Indian, " N ", .2, .01, .3, .02) };
            int[] modifiers = { 0, 2, 5, 6, 7, 8, 9 }; double[][] values = { new[] { 0.0, 0, 0, 0 }, new[] { .1, 0, 0, 0 }, new[] { 0.0, 0, 0, 0 }, new[] { .2, 30000, .4, 25 }, new[] { .2, .01, 0, 0 }, new[] { .3, .02, 0, 0 }, new[] { .2, .01, .3, .02 } };
            var n = new PropertyCreationFixture.Native(); using (var w = Wrap(n))
            { for (int i = 0; i < defs.Length; i++) { var d = defs[i]; w.Property.CreateWideFlangePropertyFromTable(d); var args = n.Arguments.Last(); Assert.That(args.Take(3), Is.EqualTo(new object[] { 10, " N ", modifiers[i] })); Assert.That(args[3], Is.EqualTo(values[i])); ((double[])args[3])[0] = 999; }
                w.Property.CreateWideFlangePropertyFromTable(defs[5]); Assert.That(n.Arguments.Last()[3], Is.EqualTo(values[5])); Assert.That(defs[5].TopPlateWidth, Is.Null); Assert.That(defs[5].BottomPlateWidth, Is.EqualTo(.3)); Assert.That(defs[3].ConcreteDensity, Is.EqualTo(25)); }
        }
        [Test]
        public void NativeArrayLayoutsAndCoordinateSelectorsAreExplicitAndOwned()
        {
            var n = new PropertyCreationFixture.Native(); using (var w = Wrap(n))
            {
                var d = General(); w.Property.CreatePrismaticGeneralProperty(d); Assert.That(n.Arguments.Last()[0], Is.EqualTo(Enumerable.Range(1, 10).Select(i => (double)i))); ((double[])n.Arguments.Last()[0])[0] = 999; Assert.That(d.AxialArea, Is.EqualTo(1));
                var f = Dofs(); var factors = Factors(); w.Property.CreateMemberPartialReleaseSpec(MemberEnd.Start, f, factors); Assert.That(n.Arguments.Last()[0], Is.EqualTo(0)); Assert.That(n.Arguments.Last()[1], Is.EqualTo(new[] { 1, 0, 1, 0, 1, 0 })); Assert.That(n.Arguments.Last()[2], Is.EqualTo(new[] { .1, .2, .3, .4, .5, .6 }));
                ((int[])n.Arguments.Last()[1])[0] = 0; Assert.That(f.Fx, Is.True);
                var offset = w.Property.CreateElementOffsetSpec(ElementNodeOffsetAxis.Global, 4, -.1, .2, -.3); Assert.That(offset.PlateNodeIndex, Is.EqualTo(4)); Assert.That(n.Arguments.Last(), Is.EqualTo(new object[] { 1, 4, -.1, .2, -.3 }));
                w.Property.CreateElementLocalZOffsetSpec(.1, -.2, .3, -.4); Assert.That(n.Arguments.Last(), Is.EqualTo(new object[] { .1, -.2, .3, -.4 }));
                w.Property.CreateMemberCableSpecEx(CableDefinitionKind.UnstressedLength, 5, CableTensionEnd.Unspecified, -1, .5, 0); Assert.That(n.Arguments.Last(), Is.EqualTo(new object[] { 1, 5.0, 0, -1.0, .5, 0.0 }));
            }
        }
        [Test]
        public void DependentNodeInputIsBoundedSinglePassOwnedAndKeepsRequestedOrderDuplicates()
        {
            var n = new PropertyCreationFixture.Native(); using (var w = Wrap(n))
            {
                var ids = new SinglePass(); var r = w.Property.AddControlDependentRelation(11, ControlDependencyMode.SpecifiedDegreesOfFreedom, Dofs(), ids);
                Assert.That(ids.Enumerations, Is.EqualTo(1)); Assert.That(r.TargetIds, Is.EqualTo(new[] { 13, 12, 13 })); Assert.That(r.ControlNodeId, Is.EqualTo(11));
                Assert.That(n.Arguments.Last().Take(8), Is.EqualTo(new object[] { 11, 4, 1, 0, 1, 0, 1, 0 })); ((int[])n.Arguments.Last()[8])[0] = 999; Assert.That(r.TargetIds[0], Is.EqualTo(13));
                Assert.Throws<NotSupportedException>(() => ((IList<int>)r.TargetIds)[0] = 7); n.Calls.Clear();
                foreach (var invalid in new[] { new int[0], new[] { 0 }, new[] { -1 }, new[] { 11 }, Enumerable.Repeat(12, 100001).ToArray() }) Assert.Throws<ArgumentException>(() => w.Property.AddControlDependentRelation(11, ControlDependencyMode.AllDirections, Dofs(), invalid));
                Assert.Throws<ArgumentNullException>(() => w.Property.AddControlDependentRelation(11, ControlDependencyMode.AllDirections, Dofs(), null)); Assert.That(n.Calls, Is.Empty);
            }
        }
        private sealed class SinglePass : IEnumerable<int>
        { public int Enumerations; public IEnumerator<int> GetEnumerator() { if (++Enumerations != 1) throw new InvalidOperationException(); return ((IEnumerable<int>)new[] { 13, 12, 13 }).GetEnumerator(); } IEnumerator IEnumerable.GetEnumerator() => GetEnumerator(); }
        [Test]
        public void AllInvalidDefinitionSelectorsAndNumericsFailBeforeNativeWrites()
        {
            var n = new PropertyCreationFixture.Native(); using (var w = Wrap(n))
            {
                Assert.Throws<ArgumentNullException>(() => w.Property.CreateWideFlangePropertyFromTable(null)); Assert.Throws<ArgumentNullException>(() => w.Property.CreatePrismaticGeneralProperty(null));
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.CreateBeamPropertyFromTableEx(Country.Chilean, "X", TableSolidShape.Cable));
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.CreateBeamPropertyFromTableEx(Country.Indian, "X", (TableSolidShape)0));
                Assert.That(Assert.Throws<ArgumentNullException>(() => w.Property.CreateBeamPropertyFromTableEx(Country.Indian, null, TableSolidShape.Cable)).ParamName, Is.EqualTo("sectionName"));
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.CreateTaperedTubeProperty((TaperedTubeShape)6, .4, .2, .01));
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.CreateTaperedTubeProperty(TaperedTubeShape.Round, .4, .2, .1));
                Assert.Throws<ArgumentException>(() => w.Property.CreatePrismaticTeeProperty(.4, .2, .5, .01));
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.CreateMemberPartialReleaseSpec((MemberEnd)2, Dofs(), Factors()));
                Assert.Throws<ArgumentNullException>(() => w.Property.CreateMemberPartialReleaseSpec(MemberEnd.Start, null, Factors())); Assert.Throws<ArgumentNullException>(() => w.Property.CreateMemberPartialReleaseSpec(MemberEnd.Start, Dofs(), null));
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.CreateElementOffsetSpec(ElementNodeOffsetAxis.Z, 1, 0, 0, 0)); Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.CreateElementOffsetSpec(ElementNodeOffsetAxis.Local, 0, 0, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.CreateMemberCableSpec((CableDefinitionKind)2, 5)); Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.CreateMemberCableSpec(CableDefinitionKind.UnstressedLength, 0));
                Assert.Throws<ArgumentException>(() => w.Property.CreateMemberCableSpecEx(CableDefinitionKind.UnstressedLength, 5, CableTensionEnd.Start, 0, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.CreateMemberFireProofingSpec((FireProofingType)0, .01, 10));
                foreach (double bad in new[] { double.NaN, double.PositiveInfinity, -1.0, 0 })
                { Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.CreatePrismaticCircleProperty(bad)); Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.CreatePrismaticRectangleProperty(.4, bad)); }
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.GetPropertyUniqueID(0)); Assert.That(n.Calls, Is.Empty);
            }
            Assert.Throws<ArgumentOutOfRangeException>(() => new PartialReleaseFactors(.1, .2, .3, .4, .5, 1.01)); Assert.Throws<ArgumentOutOfRangeException>(() => new PartialReleaseFactors(double.NaN, 0, 0, 0, 0, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => new GeneralPrismaticSectionDefinition(0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => WideFlangeTableDefinition.Standard(Country.Aluminium, "X"));
            Assert.That(Assert.Throws<ArgumentException>(() => WideFlangeTableDefinition.Standard(Country.Indian, "a\0b")).ParamName, Is.EqualTo("sectionName"));
        }
        [Test]
        public void CreationSharesSerializationWithExistingQueries()
        {
            var n = new PropertyCreationFixture.Native(); using (var w = Wrap(n))
            {
                int active = 0, maximum = 0; n.OnCall = op => { int count = Interlocked.Increment(ref active); maximum = Math.Max(maximum, count); Thread.Sleep(20); Interlocked.Decrement(ref active); };
                Task.WaitAll(Task.Run(() => w.Property.CreatePrismaticCircleProperty(.3)), Task.Run(() => w.Property.GetIsotropicMaterialCount()), Task.Run(() => w.Property.CreateMemberTrussSpec())); Assert.That(maximum, Is.EqualTo(1));
            }
        }
    }
}
