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
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Property;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSPropertyRoadmapTests
    {
        private static readonly string[] Names = { "GetOrthotropic3DMaterialCount", "GetOrthotropic3DMaterialProperties", "RemoveMaterialFromBeam", "GetSolidMaterialName",
            "GetIsotropicMaterialAssignedSolidCount", "GetIsotropicMaterialAssignedSolidIds", "SetTypeToIsotropicMaterial", "GetTypeForIsotropicMaterial",
            "GetSectionPropertiesForProfile", "GetBeamSectionPropertyRefNo", "GetBeamSectionPropertyValuesEx", "GetSectionTableNo", "GetBeamSectionPropertyTypeNo",
            "GetBeamSectionName", "GetBeamSectionDisplayName", "GetCountryTableNo", "CreateAssignProfileProperty", "AssignElementSpecToPlate", "UpdatePropertiesToDesignSection",
            "CreateIsotropicMaterialPropertiesEx", "CreateIsotropicMaterialSteel", "CreateIsotropicMaterialConcrete", "CreateIsotropicMaterialAluminum", "CreateIsotropicMaterialTimber", "AssignMaterialToSolid" };
        private static readonly string[] Creators = { "CreateIsotropicMaterialPropertiesEx", "CreateIsotropicMaterialSteel", "CreateIsotropicMaterialConcrete", "CreateIsotropicMaterialAluminum", "CreateIsotropicMaterialTimber" };
        public static IEnumerable<TestCaseData> Ports => Names.Select(n => new TestCaseData(n));
        private static IsotropicMaterialDefinition Definition() => new IsotropicMaterialDefinition(" EXPLICIT ", 101, .2, 37, 71, -.001, .02);
        private static OpenStaadWrapper Wrap(PropertyRoadmapFixture.Native n, int major = 26) => new OpenStaadWrapper(new PropertyRoadmapFixture.Root { Property = n, NativeMajor = major });
        private static MethodInfo Method(string name) => typeof(IOSProperty).GetMethods().Single(m => m.Name == name);
        private static object[] Args(string name) => Method(name).GetParameters().Select(p =>
            p.ParameterType == typeof(string) ? (object)" EXPLICIT " : p.ParameterType == typeof(int) ? (p.Name == "nativeMaterialNumber" ? 0 : p.Name == "maximumResults" ? 100000 : 17) :
            p.ParameterType == typeof(IsotropicMaterialType) ? (object)IsotropicMaterialType.Steel : p.ParameterType == typeof(AssignedSectionProfile) ? AssignedSectionProfile.Beam :
            p.ParameterType == typeof(IsotropicMaterialDefinition) ? (object)Definition() : p.ParameterType == typeof(IsotropicMaterialStrengths) ? new IsotropicMaterialStrengths(11, 22, 1.1, 1.2, 33) :
            p.ParameterType == typeof(SteelMaterialStrengths) ? (object)new SteelMaterialStrengths(22, 11, 1.2, 1.1) : p.ParameterType == typeof(bool) ? true : p.ParameterType == typeof(double) ? (object)33.0 : new[] { 89, 71, 89 }).ToArray();
        private static object Invoke(IOSProperty p, string name, object[] args = null)
        {
            try { return Method(name).Invoke(p, args ?? Args(name)); }
            catch (TargetInvocationException e) { ExceptionDispatchInfo.Capture(e.InnerException).Throw(); throw; }
        }
        private static object Value(object result, string name) => result.GetType().GetProperty(name).GetValue(result);
        private static void Respond(PropertyRoadmapFixture.Native n, string op, params object[] values) => n.Responses[op] = new Queue<object>(values);
        [Test]
        public void AddsExactlyTwentyFiveImmutableNamedContractsWithoutChangingCompatibilitySignatures()
        {
            Assert.That(Names.Distinct().Count(), Is.EqualTo(25)); Assert.That(typeof(IOSProperty).GetMethods().Count(m => !m.IsSpecialName), Is.EqualTo(175));
            foreach (string name in Names)
            {
                var m = Method(name); var impl = typeof(OSPropertyAdapter).GetMethod(name, m.GetParameters().Select(p => p.ParameterType).ToArray());
                Assert.That(impl.ReturnType, Is.EqualTo(m.ReturnType)); Assert.That(impl.GetParameters().Select(p => p.Name), Is.EqualTo(m.GetParameters().Select(p => p.Name)));
                Assert.That(impl.GetParameters().Select(p => p.DefaultValue), Is.EqualTo(m.GetParameters().Select(p => p.DefaultValue)));
                Assert.That(m.GetParameters().Any(p => p.IsOut || p.ParameterType.IsByRef || p.ParameterType == typeof(object)), Is.False);
                Assert.That(m.ReturnType.IsArray, Is.False);
            }
            Assert.That(typeof(IOSProperty).GetMethod("GetSectionPropertyValues", new[] { typeof(int) }).ReturnType, Is.EqualTo(typeof(double[])));
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryRouteRunsWithQualifiedManagedEvidence(string name)
        {
            var n = new PropertyRoadmapFixture.Native(); using (var w = Wrap(n))
            { var result = Invoke(w.Property, name); Assert.That(Value(result, "IsSuccessful"), Is.True); Assert.That(n.Calls, Is.Not.Empty); Assert.That(n.Calls.Any(c => c.StartsWith("Save") || c.StartsWith("Analyze")), Is.False); }
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryRouteRejectsDisposedBindingsBeforeNativeAccess(string name)
        {
            var n = new PropertyRoadmapFixture.Native(); var w = Wrap(n); var p = w.Property; w.Dispose();
            Assert.Throws<InvalidOperationException>(() => Invoke(p, name)); Assert.That(n.Calls, Is.Empty);
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryRouteDetectsBindingReplacementAndStopsAfterFirstDispatch(string name)
        {
            var n = new PropertyRoadmapFixture.Native(); var root = new PropertyRoadmapFixture.Root { Property = n };
            using (var w = new OpenStaadWrapper(root))
            { var p = w.Property; n.OnCall = op => { n.OnCall = null; root.Property = new PropertyRoadmapFixture.Native(); w.RefreshComObjects(); };
                Assert.Throws<InvalidOperationException>(() => Invoke(p, name)); Assert.That(n.Calls.Count, Is.EqualTo(1)); }
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryRoutePropagatesOriginalTransportErrorWithoutRetry(string name)
        {
            var n = new PropertyRoadmapFixture.Native(); var error = new COMException("Transport", unchecked((int)0x80004005)); n.OnCall = op => throw error;
            using (var w = Wrap(n)) { Assert.That(Assert.Throws<COMException>(() => Invoke(w.Property, name)), Is.SameAs(error)); Assert.That(n.Calls.Count, Is.EqualTo(1)); }
        }
        [TestCaseSource(nameof(Ports))]
        public void BothSurfacesShipIdenticalExpandedIntellisenseAndGuardMutationExamples(string name)
        {
            var xml = XDocument.Load(Path.ChangeExtension(typeof(IOSProperty).Assembly.Location, ".xml"));
            var entry = xml.Descendants("member").Single(e => ((string)e.Attribute("name")).StartsWith("M:StaadPro.Interop.Adapters.Interfaces.IOSProperty." + name + "(") || (string)e.Attribute("name") == "M:StaadPro.Interop.Adapters.Interfaces.IOSProperty." + name);
            var impl = xml.Descendants("member").Single(e => (string)e.Attribute("name") == ((string)entry.Attribute("name")).Replace("Interfaces.IOSProperty", "Models.OSPropertyAdapter"));
            Assert.That(impl.Elements().Select(e => e.ToString()), Is.EqualTo(entry.Elements().Select(e => e.ToString())));
            foreach (string tag in new[] { "summary", "returns", "remarks", "example/code" }) Assert.That(entry.XPathSelect(tag), Is.Not.Null.And.Not.Empty);
            Assert.That(entry.Element("remarks").Value, Does.Contain("When to use:")); Assert.That(entry.Descendants("include").Concat(entry.Descendants("inheritdoc")), Is.Empty);
            Assert.That(entry.Elements("param").Select(e => (string)e.Attribute("name")), Is.EqualTo(Method(name).GetParameters().Select(p => p.Name)));
            Assert.That(entry.Element("example").Value, Method(name).ReturnType == typeof(PropertyMutationResult) ? Does.Contain("bool applyChanges = false") : Does.Contain("IsSuccessful"));
        }
        [Test]
        public void NativeStrengthOrderCommonConstantsPhysicalFlagAndCreationIdentityArePreserved()
        {
            var n = new PropertyRoadmapFixture.Native(); using (var w = Wrap(n))
            {
                PropertyRoadmapFixture.VerifyMutationCalls(w.Property, n);
                n.Calls.Clear(); n.Arguments.Clear();
                foreach (string creator in Creators)
                {
                    Respond(n, creator, 0, 1, -1); var created = (PropertyMutationResult)Invoke(w.Property, creator); var updated = (PropertyMutationResult)Invoke(w.Property, creator); var failed = (PropertyMutationResult)Invoke(w.Property, creator);
                    Assert.That(created.Status, Is.EqualTo(PropertyMutationStatus.Created)); Assert.That(created.NativeReturnCode, Is.Zero); Assert.That(created.CreatedPropertyId, Is.Null);
                    Assert.That(updated.Status, Is.EqualTo(PropertyMutationStatus.Updated)); Assert.That(updated.NativeReturnCode, Is.EqualTo(1)); Assert.That(failed.Status, Is.EqualTo(PropertyMutationStatus.NativeFailure));
                    Assert.That(created.MaterialName, Is.EqualTo(" EXPLICIT ")); Assert.That(n.Arguments.Last().Take(7), Is.EqualTo(new object[] { " EXPLICIT ", 101.0, .2, 37.0, 71.0, -.001, .02 }));
                }
                var ex = n.Arguments[n.Calls.IndexOf("CreateIsotropicMaterialPropertiesEx")]; Assert.That(ex.Skip(7), Is.EqualTo(new object[] { 11.0, 22.0, 1.1, 1.2, 33.0 }));
                var steel = n.Arguments[n.Calls.IndexOf("CreateIsotropicMaterialSteel")]; Assert.That(steel.Skip(7), Is.EqualTo(new object[] { 22.0, 11.0, 1.2, 1.1, true }));
            }
        }
        [TestCase("GetSectionTableNo", -3001, PropertyQueryStatus.NotFound)]
        [TestCase("GetSectionTableNo", -6004, PropertyQueryStatus.InvalidReference)]
        [TestCase("GetSectionTableNo", -6022, PropertyQueryStatus.NotAssigned)]
        [TestCase("GetCountryTableNo", -3001, PropertyQueryStatus.NotFound)]
        [TestCase("GetCountryTableNo", -6022, PropertyQueryStatus.NotAssigned)]
        [TestCase("GetBeamSectionPropertyRefNo", 0, PropertyQueryStatus.EmptyOrUnavailable)]
        [TestCase("GetBeamSectionPropertyTypeNo", 0, PropertyQueryStatus.NativeFailure)]
        [TestCase("GetSectionPropertiesForProfile", -3001, PropertyQueryStatus.NotFound)]
        [TestCase("GetSectionPropertiesForProfile", -6022, PropertyQueryStatus.NotAssigned)]
        public void MemberQueriesKeepOperationSpecificFailuresWithoutDefaultData(string name, int code, PropertyQueryStatus status)
        {
            var n = new PropertyRoadmapFixture.Native(); Respond(n, name == "GetSectionPropertiesForProfile" ? "GetSectionPropertyValues" : name, code);
            using (var w = Wrap(n)) { var r = Invoke(w.Property, name); Assert.That(Value(r, "Status"), Is.EqualTo(status)); Assert.That(Value(r, "NativeReturnCode"), Is.EqualTo(code));
                Assert.That(Assert.Throws<TargetInvocationException>(() => Value(r, "Data")).InnerException, Is.TypeOf<InvalidOperationException>()); }
        }
        [TestCase("GetSectionTableNo")][TestCase("GetCountryTableNo")]
        public void SuccessfulZeroAndUnknownPositiveCodesRemainData(string name)
        {
            var n = new PropertyRoadmapFixture.Native(); Respond(n, name, 0, 12345);
            using (var w = Wrap(n)) { Assert.That(Value(Invoke(w.Property, name), "Data"), Is.EqualTo(0)); Assert.That(Value(Invoke(w.Property, name), "Data"), Is.EqualTo(12345)); }
        }
        [TestCase("GetSolidMaterialName")][TestCase("GetBeamSectionName")][TestCase("GetBeamSectionDisplayName")]
        public void NativeNamesPreserveAmbiguousAbsenceAndExactStrings(string name)
        {
            var n = new PropertyRoadmapFixture.Native(); Respond(n, name, null, DBNull.Value, "", " Exact native name ", 17);
            using (var w = Wrap(n))
            { for (int i = 0; i < 3; i++) Assert.That(Value(Invoke(w.Property, name), "Status"), Is.EqualTo(PropertyQueryStatus.EmptyOrUnavailable));
                Assert.That(Value(Invoke(w.Property, name), "Data"), Is.EqualTo(" Exact native name ")); Assert.Throws<InvalidOperationException>(() => Invoke(w.Property, name)); }
        }
        [TestCase(0)][TestCase(1)][TestCase(2)][TestCase(3)][TestCase(4)]
        public void MaterialClassificationPreservesNotSpecifiedAndRejectsFalseAndUnknownOutputs(int type)
        {
            var n = new PropertyRoadmapFixture.Native { MaterialType = type }; using (var w = Wrap(n))
            {
                Assert.That(w.Property.GetTypeForIsotropicMaterial("X").Data, Is.EqualTo((IsotropicMaterialType)type));
                Respond(n, "GetTypeForIsotropicMaterial", false); Assert.That(w.Property.GetTypeForIsotropicMaterial("X").Status, Is.EqualTo(PropertyQueryStatus.NotFound));
                n.MaterialType = 5; Assert.Throws<InvalidOperationException>(() => w.Property.GetTypeForIsotropicMaterial("X"));
            }
        }
        [Test]
        public void SolidInventoryIsBoundedOwnedOrderedAndCheckedForStateChanges()
        {
            var n = new PropertyRoadmapFixture.Native(); using (var w = Wrap(n))
            {
                var ids = w.Property.GetIsotropicMaterialAssignedSolidIds("X").Data; Assert.That(ids, Is.EqualTo(new[] { 89, 71, 89 }));
                ((int[])n.Buffers["SolidIds"])[0] = 999; Assert.That(ids[0], Is.EqualTo(89)); Assert.Throws<NotSupportedException>(() => ((IList<int>)ids)[0] = 3);
                n.Calls.Clear(); n.SolidCount = 0; Assert.That(w.Property.GetIsotropicMaterialAssignedSolidIds("X").Data, Is.Empty); Assert.That(n.Calls, Is.EqualTo(new[] { "GetIsotropicMaterialAssignedSolidCount", "GetIsotropicMaterialAssignedSolidCount" }));
                n.Calls.Clear(); n.SolidCount = 4; Assert.That(w.Property.GetIsotropicMaterialAssignedSolidIds("X", 3).Status, Is.EqualTo(PropertyQueryStatus.LimitExceeded)); Assert.That(n.Calls.Count, Is.EqualTo(1));
                n.SolidCount = 3; Respond(n, "GetIsotropicMaterialAssignedSolidCount", 3, 4); Assert.That(w.Property.GetIsotropicMaterialAssignedSolidIds("X").Status, Is.EqualTo(PropertyQueryStatus.StateChanged));
                Respond(n, "GetIsotropicMaterialAssignedSolidList", false); Assert.That(w.Property.GetIsotropicMaterialAssignedSolidIds("X").Status, Is.EqualTo(PropertyQueryStatus.NativeFailure));
            }
        }
        public static IEnumerable<TestCaseData> BadArrays
        {
            get { foreach (object value in new object[] { null, new double[2], new double[4], new int[3], new object[3], new double[1, 3], new[] { 1.0, double.NaN, 3.0 }, new[] { 1.0, double.PositiveInfinity, 3.0 } }) yield return new TestCaseData(new object[] { value }); }
        }
        [TestCaseSource(nameof(BadArrays))]
        public void OrthotropicRequiresAllSixExactThreeValueFloatingBuffers(object bad)
        {
            foreach (string field in new[] { "E", "Poisson", "G", "Density", "Alpha", "Damping" })
            {
                var n = new PropertyRoadmapFixture.Native { Output = (key, value) => key == field ? bad : value };
                using (var w = Wrap(n)) Assert.Throws<InvalidOperationException>(() => w.Property.GetOrthotropic3DMaterialProperties(0));
            }
        }
        [Test]
        public void OrthotropicRetainsAllEighteenValuesAndOwnsNativeBuffersIncludingNonzeroLowerBounds()
        {
            var n = new PropertyRoadmapFixture.Native(); n.Output = (key, value) => { var source = (double[])value; var a = Array.CreateInstance(typeof(double), new[] { 3 }, new[] { -2 }); for (int i = 0; i < 3; i++) a.SetValue(source[i], i - 2); return a; };
            using (var w = Wrap(n))
            {
                var m = w.Property.GetOrthotropic3DMaterialProperties(0).Data;
                Assert.That(new[] { m.ElasticModulusFirst, m.ElasticModulusSecond, m.ElasticModulusThird, m.PoissonRatioFirst, m.PoissonRatioSecond, m.PoissonRatioThird, m.ShearModulusFirst, m.ShearModulusSecond, m.ShearModulusThird,
                    m.WeightDensityFirst, m.WeightDensitySecond, m.WeightDensityThird, m.ThermalExpansionFirst, m.ThermalExpansionSecond, m.ThermalExpansionThird, m.DampingRatioFirst, m.DampingRatioSecond, m.DampingRatioThird },
                    Is.EqualTo(new[] { 101, 102, 103, .1, .2, .3, 31, 32, 33, 71, 72, 73, .001, .002, .003, .01, .02, .03 }));
                ((Array)n.Buffers["E"]).SetValue(999.0, -2); Assert.That(m.ElasticModulusFirst, Is.EqualTo(101));
            }
        }
        [TestCase(24, 666, 7)][TestCase(25, 666, 6)][TestCase(26, 666, 6)]
        [TestCase(24, 612, 11)][TestCase(25, 612, 12)][TestCase(26, 612, 12)]
        public void MemberValuesUseAcquiredVersionFixedCapacityApplicableFieldsAndOwnedValues(int major, int type, int slots)
        {
            var n = new PropertyRoadmapFixture.Native { SectionType = type, WrittenSectionSlots = slots };
            using (var w = Wrap(n, major))
            {
                var s = w.Property.GetBeamSectionPropertyValuesEx(17).Data; Assert.That(s.MemberId, Is.EqualTo(17)); Assert.That(s.NativeMajorVersion, Is.EqualTo(major));
                Assert.That(s.NativePropertyType, Is.EqualTo(type)); Assert.That(s.FieldNames.Count, Is.EqualTo(slots)); Assert.That(s.Values.Count, Is.EqualTo(slots)); Assert.That(s.NativeBufferCapacity, Is.EqualTo(24));
                Assert.That(((Array)n.Buffers["SectionValues"]).Length, Is.EqualTo(24)); ((double[])n.Buffers["SectionValues"])[0] = 999; Assert.That(s.Values[0], Is.EqualTo(.5));
                Assert.Throws<NotSupportedException>(() => ((IList<double>)s.Values)[0] = 7); n.WrittenSectionSlots = slots - 1; Assert.Throws<InvalidOperationException>(() => w.Property.GetBeamSectionPropertyValuesEx(17));
            }
        }
        [TestCase(23, 610)][TestCase(27, 610)][TestCase(26, 12345)]
        public void UnknownMemberLayoutsFailExplicitly(int major, int type)
        { var n = new PropertyRoadmapFixture.Native { SectionType = type }; using (var w = Wrap(n, major)) Assert.Throws<NotSupportedException>(() => w.Property.GetBeamSectionPropertyValuesEx(17)); }
        [Test]
        public void AssignmentsSnapshotOncePreserveOrderDuplicatesAndRejectEmptyUnboundedOrInvalidSequences()
        {
            var n = new PropertyRoadmapFixture.Native(); using (var w = Wrap(n))
            {
                var ids = new SinglePass(); var r = w.Property.AssignElementSpecToPlate(ids, 9); Assert.That(ids.Enumerations, Is.EqualTo(1)); Assert.That(r.TargetIds, Is.EqualTo(new[] { 89, 71, 89 })); Assert.That(r.SpecificationId, Is.EqualTo(9));
                ((int[])n.Arguments.Last()[0])[0] = 999; Assert.That(r.TargetIds[0], Is.EqualTo(89)); Assert.Throws<NotSupportedException>(() => ((IList<int>)r.TargetIds)[0] = 7);
                n.Calls.Clear(); foreach (var invalid in new[] { new int[0], new[] { 0 }, new[] { -1 }, Enumerable.Repeat(1, 100001).ToArray() })
                { Assert.Throws<ArgumentException>(() => w.Property.AssignMaterialToSolid("X", invalid)); Assert.Throws<ArgumentException>(() => w.Property.AssignElementSpecToPlate(invalid, 9)); }
                Assert.Throws<ArgumentNullException>(() => w.Property.AssignMaterialToSolid("X", null)); Assert.That(n.Calls, Is.Empty);
            }
        }
        private sealed class SinglePass : IEnumerable<int>
        {
            public int Enumerations;
            public IEnumerator<int> GetEnumerator() { if (++Enumerations != 1) throw new InvalidOperationException(); return ((IEnumerable<int>)new[] { 89, 71, 89 }).GetEnumerator(); }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        [TestCase("RemoveMaterialFromBeam", -1, PropertyMutationStatus.NativeFailure)]
        [TestCase("AssignElementSpecToPlate", -106, PropertyMutationStatus.InvalidInput)]
        [TestCase("AssignElementSpecToPlate", -6017, PropertyMutationStatus.NativeFailure)]
        [TestCase("CreateAssignProfileProperty", 0, PropertyMutationStatus.NativeFailure)]
        [TestCase("CreateAssignProfileProperty", -6008, PropertyMutationStatus.InvalidInput)]
        [TestCase("AssignMaterialToSolid", 0, PropertyMutationStatus.NativeFailure)]
        [TestCase("SetTypeToIsotropicMaterial", 0, PropertyMutationStatus.NativeFailure)]
        [TestCase("UpdatePropertiesToDesignSection", 0, PropertyMutationStatus.NativeFailure)]
        public void MutationFailuresPreserveCodesAndRequestedIdentityWithoutFabricatedCreationIds(string name, int code, PropertyMutationStatus status)
        { var n = new PropertyRoadmapFixture.Native(); Respond(n, name, code); using (var w = Wrap(n)) { var r = (PropertyMutationResult)Invoke(w.Property, name); Assert.That(r.Status, Is.EqualTo(status)); Assert.That(r.IsSuccessful, Is.False); Assert.That(r.NativeReturnCode, Is.EqualTo(code)); Assert.That(r.NativeBooleanReturn, Is.Null); Assert.That(r.CreatedPropertyId, Is.Null); } }
        [TestCase("AssignMaterialToSolid")][TestCase("SetTypeToIsotropicMaterial")][TestCase("UpdatePropertiesToDesignSection")]
        public void BooleanMutationEvidenceRemainsBoolean(string name)
        { var n = new PropertyRoadmapFixture.Native(); Respond(n, name, true, false); using (var w = Wrap(n)) { var r = (PropertyMutationResult)Invoke(w.Property, name); Assert.That(r.NativeBooleanReturn, Is.True); Assert.That(r.NativeReturnCode, Is.Null); Assert.That(((PropertyMutationResult)Invoke(w.Property, name)).IsSuccessful, Is.False); } }
        [TestCase("RemoveMaterialFromBeam")][TestCase("AssignElementSpecToPlate")][TestCase("CreateAssignProfileProperty")]
        [TestCase("CreateIsotropicMaterialPropertiesEx")][TestCase("CreateIsotropicMaterialSteel")][TestCase("CreateIsotropicMaterialConcrete")]
        [TestCase("CreateIsotropicMaterialAluminum")][TestCase("CreateIsotropicMaterialTimber")]
        [TestCase("GetOrthotropic3DMaterialCount")][TestCase("GetIsotropicMaterialAssignedSolidCount")][TestCase("GetBeamSectionPropertyRefNo")]
        public void IntegralContractsRejectBooleanFractionalTextAndUndocumentedNegativeReturns(string name)
        {
            foreach (object bad in new object[] { true, .5, "1", null, -9999 })
            { var n = new PropertyRoadmapFixture.Native(); Respond(n, name, new[] { bad }); using (var w = Wrap(n)) Assert.Throws<InvalidOperationException>(() => Invoke(w.Property, name)); }
        }
        [Test]
        public void InvalidInputsFailBeforeAnyNativeWriteAndNoMaterialDefaultsAreAvailable()
        {
            var n = new PropertyRoadmapFixture.Native(); using (var w = Wrap(n))
            {
                Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.SetTypeToIsotropicMaterial("X", (IsotropicMaterialType)5)).ParamName, Is.EqualTo("materialType"));
                Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.CreateAssignProfileProperty((AssignedSectionProfile)(-1))).ParamName, Is.EqualTo("profileType"));
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.GetSectionPropertiesForProfile((AssignedSectionProfile)37));
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.RemoveMaterialFromBeam(0)); Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.GetOrthotropic3DMaterialProperties(-1));
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.GetIsotropicMaterialAssignedSolidIds("X", 100001));
                Assert.Throws<ArgumentException>(() => w.Property.AssignMaterialToSolid("X\0Y", new[] { 1 }));
                Assert.Throws<ArgumentNullException>(() => w.Property.CreateIsotropicMaterialSteel(null, new SteelMaterialStrengths(2, 1, 1, 1), false));
                Assert.Throws<ArgumentNullException>(() => w.Property.CreateIsotropicMaterialPropertiesEx(Definition(), null));
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.CreateIsotropicMaterialConcrete(Definition(), double.NaN, false)); Assert.That(n.Calls, Is.Empty);
            }
            Assert.That(typeof(IsotropicMaterialDefinition).GetConstructor(Type.EmptyTypes), Is.Null);
            foreach (double bad in new[] { double.NaN, double.PositiveInfinity, -1.0 })
            { Assert.Throws<ArgumentOutOfRangeException>(() => new IsotropicMaterialDefinition("X", bad, .2, 37, 71, .001, .02));
                Assert.Throws<ArgumentOutOfRangeException>(() => new IsotropicMaterialStrengths(1, 2, 1, 1, bad)); Assert.Throws<ArgumentOutOfRangeException>(() => new SteelMaterialStrengths(2, 1, bad, 1)); }
        }
        [Test]
        public void NewAndExistingQueriesShareSerializationAcrossConcurrentRequests()
        {
            var n = new PropertyRoadmapFixture.Native(); using (var w = Wrap(n))
            {
                int active = 0, maximum = 0; n.OnCall = op => { int current = Interlocked.Increment(ref active); maximum = Math.Max(maximum, current); Thread.Sleep(15); Interlocked.Decrement(ref active); };
                Task.WaitAll(Task.Run(() => w.Property.GetIsotropicMaterialAssignedSolidIds("X")), Task.Run(() => w.Property.GetOrthotropic3DMaterialCount()), Task.Run(() => w.Property.GetIsotropicMaterialCount()));
                Assert.That(maximum, Is.EqualTo(1)); int start = n.Calls.IndexOf("GetIsotropicMaterialAssignedSolidList") - 1;
                Assert.That(n.Calls.Skip(start).Take(3), Is.EqualTo(new[] { "GetIsotropicMaterialAssignedSolidCount", "GetIsotropicMaterialAssignedSolidList", "GetIsotropicMaterialAssignedSolidCount" }));
            }
        }
    }
    internal static class PropertyXmlTestExtensions
    {
        internal static string XPathSelect(this XElement e, string path) => path.Split('/').Aggregate(e, (node, name) => node?.Element(name))?.Value;
    }
}
