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
using PropertyAttribute = StaadPro.Interop.Models.Property.PropertyAttribute;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSPropertyAttributeTests
    {
        private static readonly string[] Names = { "GetInactiveMemberCount", "GetFireProofedBeamCount", "GetFireProofedBeamList", "GetFireProofDataForBeam", "GetFireProofingSpecCount", "GetFireProofingSpecDetails",
            "GetFireProofingSpecAssignedBeamCount", "GetFireProofingSpecAssignedBeamList", "CreateMemberAttribute", "CreateElementAttribute", "DeleteMemberAttribute", "DeleteElementAttribute",
            "GetMemberCountByAttribute", "GetMemberListByAttribute", "GetMemberAttributeCount", "GetMemberAttributeList", "GetElementCountByAttribute", "GetElementListByAttribute",
            "GetAssignedAttributeCount", "RemoveAttribute", "AssignMemberAttribute", "AssignElementAttribute", "CreateUPTTable", "AddUPTPropertyWIDEFLANGE", "AddUPTPropertyWIDEFLANGEUNEQUAL" };
        public static IEnumerable<TestCaseData> Ports => Names.Select(n => new TestCaseData(n));
        private static MethodInfo Method(string name) => typeof(IOSProperty).GetMethods().Single(m => m.Name == name);
        private static PropertyAttribute Attribute() => new PropertyAttribute(" Purpose ", " VALUE ");
        private static UptWideFlangeDefinition Wide() => new UptWideFlangeDefinition(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        private static OpenStaadWrapper Wrap(PropertyAttributeFixture.Native n) => new OpenStaadWrapper(new PropertyAttributeFixture.Root { Property = n });
        private static object[] Args(string name) => Method(name).GetParameters().Select(p =>
            p.ParameterType == typeof(PropertyAttribute) ? (object)Attribute() : p.ParameterType == typeof(UptWideFlangeDefinition) ? Wide() :
            p.ParameterType == typeof(UptUnequalWideFlangeDefinition) ? (object)new UptUnequalWideFlangeDefinition(Wide(), 11, 12) : p.ParameterType == typeof(UserProvidedTableType) ? UserProvidedTableType.WideFlange :
            p.ParameterType == typeof(string) ? (object)" EXACT_SECTION " : p.ParameterType == typeof(IEnumerable<int>) ? new[] { 41, 17, 41 } :
            (object)(p.Name == "maximumResults" ? 100000 : p.Name == "specificationIndex" ? 1 : p.Name == "tableId" ? 7 : 17)).ToArray();
        private static object Invoke(IOSProperty p, string name, object[] args = null)
        { try { return Method(name).Invoke(p, args ?? Args(name)); } catch (TargetInvocationException e) { ExceptionDispatchInfo.Capture(e.InnerException).Throw(); throw; } }
        private static object Value(object result, string name) => result.GetType().GetProperty(name).GetValue(result);
        private static void Respond(PropertyAttributeFixture.Native n, string op, params object[] values) => n.Responses[op] = new Queue<object>(values);
        [Test]
        public void AddsExactlyTwentyFiveTypedContractsAndLeavesUnverifiedFunctionsPending()
        {
            Assert.That(Names.Distinct().Count(), Is.EqualTo(25)); Assert.That(typeof(IOSProperty).GetMethods().Count(m => !m.IsSpecialName), Is.EqualTo(200));
            foreach (string name in Names)
            {
                var m = Method(name); var impl = typeof(OSPropertyAdapter).GetMethod(name, m.GetParameters().Select(p => p.ParameterType).ToArray());
                Assert.That(impl.ReturnType, Is.EqualTo(m.ReturnType)); Assert.That(impl.GetParameters().Select(p => p.Name), Is.EqualTo(m.GetParameters().Select(p => p.Name)));
                Assert.That(impl.GetParameters().Select(p => p.DefaultValue), Is.EqualTo(m.GetParameters().Select(p => p.DefaultValue)));
                Assert.That(m.GetParameters().Any(p => p.IsOut || p.ParameterType.IsByRef || p.ParameterType.IsArray || p.ParameterType == typeof(object)), Is.False);
            }
            foreach (string name in new[] { "GetInactiveMemberList", "GetAlphaAngleForSection", "GetCentroidLocationForSection", "GetAssignedAttributeByIndex" }) Assert.That(typeof(IOSProperty).GetMethods().Any(m => m.Name == name), Is.False);
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryRouteRunsWithQualifiedEvidenceOnTheCallerThread(string name)
        {
            var n = new PropertyAttributeFixture.Native(); using (var w = Wrap(n))
            { Assert.That(Value(Invoke(w.Property, name), "IsSuccessful"), Is.True); Assert.That(n.Calls, Is.Not.Empty); Assert.That(n.Threads.All(id => id == Thread.CurrentThread.ManagedThreadId), Is.True); Assert.That(n.Calls.Any(c => c.StartsWith("Save") || c.StartsWith("Analyze")), Is.False); }
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryRouteRejectsDisposedBindingsBeforeDispatch(string name)
        { var n = new PropertyAttributeFixture.Native(); var w = Wrap(n); var p = w.Property; w.Dispose(); Assert.Throws<InvalidOperationException>(() => Invoke(p, name)); Assert.That(n.Calls, Is.Empty); }
        [TestCaseSource(nameof(Ports))]
        public void EveryRouteStopsAfterOneCallWhenTheCapturedBindingChanges(string name)
        {
            var n = new PropertyAttributeFixture.Native(); var root = new PropertyAttributeFixture.Root { Property = n }; using (var w = new OpenStaadWrapper(root))
            { var p = w.Property; n.OnCall = op => { n.OnCall = null; root.Property = new PropertyAttributeFixture.Native(); w.RefreshComObjects(); }; Assert.Throws<InvalidOperationException>(() => Invoke(p, name)); Assert.That(n.Calls.Count, Is.EqualTo(1)); }
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryRoutePropagatesTheOriginalTransportErrorWithoutRetry(string name)
        {
            var n = new PropertyAttributeFixture.Native(); var error = new COMException("Transport", unchecked((int)0x80004005)); n.OnCall = op => throw error;
            using (var w = Wrap(n)) { Assert.That(Assert.Throws<COMException>(() => Invoke(w.Property, name)), Is.SameAs(error)); Assert.That(n.Calls.Count, Is.EqualTo(1)); }
        }
        [TestCaseSource(nameof(Ports))]
        public void BothPublicSurfacesShipIdenticalExpandedXmlAndGuardedMutationExamples(string name)
        {
            var xml = XDocument.Load(Path.ChangeExtension(typeof(IOSProperty).Assembly.Location, ".xml")); string prefix = "M:StaadPro.Interop.Adapters.Interfaces.IOSProperty." + name;
            var entry = xml.Descendants("member").Single(e => (string)e.Attribute("name") == prefix || ((string)e.Attribute("name")).StartsWith(prefix + "("));
            var impl = xml.Descendants("member").Single(e => (string)e.Attribute("name") == ((string)entry.Attribute("name")).Replace("Interfaces.IOSProperty", "Models.OSPropertyAdapter"));
            Assert.That(impl.Elements().Select(e => e.ToString()), Is.EqualTo(entry.Elements().Select(e => e.ToString())));
            foreach (string tag in new[] { "summary", "returns", "remarks", "example" }) Assert.That(entry.Element(tag)?.Value, Is.Not.Null.And.Not.Empty);
            Assert.That(entry.Elements("param").Select(e => (string)e.Attribute("name")), Is.EqualTo(Method(name).GetParameters().Select(p => p.Name)));
            Assert.That(entry.Element("remarks").Value, Does.Contain("When to use:")); Assert.That(entry.Descendants("include").Concat(entry.Descendants("inheritdoc")), Is.Empty);
            Assert.That(entry.Element("example").Value, Method(name).ReturnType == typeof(PropertyMutationResult) ? Does.Contain("bool applyChanges = false") : Does.Contain("IsSuccessful"));
        }
        [Test]
        public void SharedInstalledConsumerFixtureExercisesEveryNewRoute()
        { var n = new PropertyAttributeFixture.Native(); using (var w = Wrap(n)) { PropertyAttributeFixture.VerifyCalls(w.Property, n); Assert.That(n.Calls.Distinct(), Is.EquivalentTo(Names)); } }
        [TestCase("GetFireProofedBeamList", "GetFireProofedBeamCount")]
        [TestCase("GetFireProofingSpecAssignedBeamList", "GetFireProofingSpecAssignedBeamCount")]
        public void FireProofingListsAreOwnedBoundedAndCountChecked(string name, string countOp)
        {
            var n = new PropertyAttributeFixture.Native(); using (var w = Wrap(n))
            {
                var r = (PropertyQueryResult<IReadOnlyList<int>>)Invoke(w.Property, name); Assert.That(r.Data, Is.EqualTo(new[] { 41, 17, 41 })); ((int[])n.Buffers["FireIds"])[0] = 999; Assert.That(r.Data[0], Is.EqualTo(41)); Assert.Throws<NotSupportedException>(() => ((IList<int>)r.Data)[0] = 7);
                n.Calls.Clear(); n.EntityCount = 0; Assert.That(((PropertyQueryResult<IReadOnlyList<int>>)Invoke(w.Property, name)).Data, Is.Empty); Assert.That(n.Calls, Is.EqualTo(new[] { countOp, countOp }));
                n.Calls.Clear(); n.EntityCount = 100001; Assert.That(Value(Invoke(w.Property, name), "Status"), Is.EqualTo(PropertyQueryStatus.LimitExceeded)); Assert.That(n.Calls.Count, Is.EqualTo(1));
                n.EntityCount = 3; Respond(n, countOp, 3, 4); Assert.That(Value(Invoke(w.Property, name), "Status"), Is.EqualTo(PropertyQueryStatus.StateChanged));
                Respond(n, name, false); var failure = (PropertyQueryResult<IReadOnlyList<int>>)Invoke(w.Property, name); Assert.That(failure.Status, Is.EqualTo(PropertyQueryStatus.NativeFailure)); Assert.Throws<InvalidOperationException>(() => { var ignored = failure.Data; });
            }
        }
        [TestCase("GetMemberListByAttribute", "GetMemberCountByAttribute", PropertyAttributeScope.Member)]
        [TestCase("GetElementListByAttribute", "GetElementCountByAttribute", PropertyAttributeScope.Element)]
        public void AttributeListsRetainExactPairDomainOrderAndOwnedIds(string name, string countOp, PropertyAttributeScope scope)
        {
            var n = new PropertyAttributeFixture.Native(); using (var w = Wrap(n))
            {
                var r = (PropertyQueryResult<PropertyAttributeEntityIds>)Invoke(w.Property, name); Assert.That(r.Data.Attribute.Name, Is.EqualTo(" Purpose ")); Assert.That(r.Data.Attribute.Value, Is.EqualTo(" VALUE ")); Assert.That(r.Data.Scope, Is.EqualTo(scope));
                Assert.That(r.Data.EntityIds, Is.EqualTo(scope == PropertyAttributeScope.Member ? new[] { 41, 17, 41 } : new[] { 31, 47, 31 })); ((int[])n.Buffers["AttributeIds"])[0] = 999; Assert.That(r.Data.EntityIds[0], Is.Not.EqualTo(999));
                Assert.Throws<NotSupportedException>(() => ((IList<int>)r.Data.EntityIds)[0] = 7); Assert.That(n.Calls, Is.EqualTo(new[] { countOp, name, countOp }));
                Respond(n, countOp, 3, 4); Assert.That(Value(Invoke(w.Property, name), "Status"), Is.EqualTo(PropertyQueryStatus.StateChanged));
                Respond(n, name, -1); Assert.That(Value(Invoke(w.Property, name), "Status"), Is.EqualTo(PropertyQueryStatus.NativeFailure));
                Respond(n, countOp, -1); Assert.That(Value(Invoke(w.Property, name), "Status"), Is.EqualTo(PropertyQueryStatus.NativeFailure));
                n.Calls.Clear(); n.EntityCount = 0; Assert.That(((PropertyQueryResult<PropertyAttributeEntityIds>)Invoke(w.Property, name)).Data.EntityIds, Is.Empty); Assert.That(n.Calls, Is.EqualTo(new[] { countOp, countOp }));
                n.Calls.Clear(); n.EntityCount = 100001; Assert.That(Value(Invoke(w.Property, name), "Status"), Is.EqualTo(PropertyQueryStatus.LimitExceeded)); Assert.That(n.Calls.Count, Is.EqualTo(1));
            }
        }
        [TestCase("GetMemberCountByAttribute", "AttributeInput")][TestCase("GetElementCountByAttribute", "AttributeInput")]
        [TestCase("GetMemberListByAttribute", "AttributeInput")][TestCase("GetElementListByAttribute", "AttributeInput")]
        [TestCase("GetMemberListByAttribute", "ListAttributeInput")][TestCase("GetElementListByAttribute", "ListAttributeInput")]
        public void RewrittenByReferenceInputNeverChangesCallerIdentityOrSuppliesQualifiedData(string name, string key)
        {
            var n = new PropertyAttributeFixture.Native { Output = (field, value) => field == key ? "REWRITTEN" : value }; var pair = Attribute(); using (var w = Wrap(n))
            { var args = Args(name); args[0] = pair; var r = Invoke(w.Property, name, args); Assert.That(Value(r, "Status"), Is.EqualTo(PropertyQueryStatus.StateChanged)); Assert.That(pair.Value, Is.EqualTo(" VALUE ")); Assert.That(Assert.Throws<TargetInvocationException>(() => Value(r, "Data")).InnerException, Is.TypeOf<InvalidOperationException>()); }
        }
        public static IEnumerable<TestCaseData> BadIdBuffers
        {
            get { foreach (object value in new object[] { null, new int[2], new int[4], new long[3], new object[3], new int[1, 3], new[] { 41, 0, 17 }, new[] { 41, -1, 17 } }) yield return new TestCaseData(new object[] { value }); }
        }
        [TestCaseSource(nameof(BadIdBuffers))]
        public void AllIdSnapshotsRequireExactWrittenPositiveInt32Buffers(object bad)
        {
            foreach (string name in new[] { "GetFireProofedBeamList", "GetFireProofingSpecAssignedBeamList", "GetMemberListByAttribute", "GetElementListByAttribute" })
            { var n = new PropertyAttributeFixture.Native { Output = (key, value) => key == "FireIds" || key == "AttributeIds" ? bad : value }; using (var w = Wrap(n)) Assert.Throws<InvalidOperationException>(() => Invoke(w.Property, name)); }
        }
        [Test]
        public void MemberAttributeInventoryRequiresCountReturnAndOwnedPairedWrittenStringArrays()
        {
            var n = new PropertyAttributeFixture.Native(); using (var w = Wrap(n))
            {
                var r = w.Property.GetMemberAttributeList(); Assert.That(r.Data.Select(a => a.Name), Is.EqualTo(new[] { "Purpose", " Purpose " })); Assert.That(r.Data.Select(a => a.Value), Is.EqualTo(new[] { " REVIEWED ", "" }));
                ((string[])n.Buffers["AttributeNames"])[0] = "EDITED"; Assert.That(r.Data[0].Name, Is.EqualTo("Purpose")); Assert.Throws<NotSupportedException>(() => ((IList<PropertyAttribute>)r.Data)[0] = Attribute());
                Respond(n, "GetMemberAttributeList", 0); Assert.That(w.Property.GetMemberAttributeList().Status, Is.EqualTo(PropertyQueryStatus.StateChanged));
                Respond(n, "GetMemberAttributeList", true); Assert.Throws<InvalidOperationException>(() => w.Property.GetMemberAttributeList());
                Respond(n, "GetMemberAttributeCount", 2, 3); Assert.That(w.Property.GetMemberAttributeList().Status, Is.EqualTo(PropertyQueryStatus.StateChanged));
                n.Calls.Clear(); n.AttributeCount = 0; Assert.That(w.Property.GetMemberAttributeList().Data, Is.Empty); Assert.That(n.Calls, Is.EqualTo(new[] { "GetMemberAttributeCount", "GetMemberAttributeCount" }));
                n.Calls.Clear(); n.AttributeCount = 100001; Assert.That(w.Property.GetMemberAttributeList().Status, Is.EqualTo(PropertyQueryStatus.LimitExceeded)); Assert.That(n.Calls.Count, Is.EqualTo(1));
            }
        }
        public static IEnumerable<TestCaseData> BadStringBuffers
        { get { foreach (object value in new object[] { null, new string[2], new[] { "A" }, new[] { "A", "B", "C" }, new object[] { "A", "B" }, new string[1, 2], new[] { "A", "B\0C" } }) yield return new TestCaseData(new object[] { value }); } }
        [TestCaseSource(nameof(BadStringBuffers))]
        public void AttributeNamesAndValuesRejectMalformedOrUnwrittenStringBuffers(object bad)
        {
            foreach (string field in new[] { "AttributeNames", "AttributeValues" })
            { var n = new PropertyAttributeFixture.Native { Output = (key, value) => key == field ? bad : value }; using (var w = Wrap(n)) Assert.Throws<InvalidOperationException>(() => w.Property.GetMemberAttributeList()); }
        }
        [Test]
        public void ValidNonzeroLowerBoundsAndDuplicateAttributePairsRetainCorrespondence()
        {
            var n = new PropertyAttributeFixture.Native(); n.Output = (key, value) =>
            {
                if (key == "FireIds") { var a = Array.CreateInstance(typeof(int), new[] { 3 }, new[] { -2 }); a.SetValue(41, -2); a.SetValue(17, -1); a.SetValue(41, 0); return a; }
                if (key == "AttributeNames" || key == "AttributeValues") { var a = Array.CreateInstance(typeof(string), new[] { 2 }, new[] { 5 }); a.SetValue(key == "AttributeNames" ? "" : " VALUE ", 5); a.SetValue(key == "AttributeNames" ? "" : " VALUE ", 6); return a; }
                return value;
            };
            using (var w = Wrap(n)) { Assert.That(w.Property.GetFireProofedBeamList().Data, Is.EqualTo(new[] { 41, 17, 41 })); var pairs = w.Property.GetMemberAttributeList().Data; Assert.That(pairs.Count, Is.EqualTo(2)); Assert.That(pairs.Select(p => p.Name), Is.EqualTo(new[] { "", "" })); Assert.That(pairs.Select(p => p.Value), Is.EqualTo(new[] { " VALUE ", " VALUE " })); }
        }
        [Test]
        public void FireproofingDataPreservesMemberVersusPositiveGetterIndexAndScalarAssignmentCount()
        {
            var n = new PropertyAttributeFixture.Native(); using (var w = Wrap(n))
            {
                var member = w.Property.GetFireProofDataForBeam(17).Data; Assert.That(member.MemberId, Is.EqualTo(17)); Assert.That(member.SpecificationIndex, Is.Null); Assert.That(member.AssignedMemberCount, Is.Null);
                var spec = w.Property.GetFireProofingSpecDetails(1).Data; Assert.That(spec.SpecificationIndex, Is.EqualTo(1)); Assert.That(spec.MemberId, Is.Null); Assert.That(spec.AssignedMemberCount, Is.EqualTo(3)); Assert.That(spec.Type, Is.EqualTo(FireProofingType.Contour));
                n.Output = (key, value) => key == "AssignedCount" ? new[] { 3 } : value; Assert.Throws<InvalidOperationException>(() => w.Property.GetFireProofingSpecDetails(1));
                n.Output = (key, value) => key == "Density" || key == "Thickness" ? 0.0 : value; Assert.That(w.Property.GetFireProofDataForBeam(17).Data.Density, Is.Zero);
                Respond(n, "GetFireProofingSpecDetails", false); Assert.That(w.Property.GetFireProofingSpecDetails(1).Status, Is.EqualTo(PropertyQueryStatus.NativeFailure));
            }
        }
        [TestCase("FireType", 0)][TestCase("FireType", 3)][TestCase("FireType", true)]
        [TestCase("Thickness", double.NaN)][TestCase("Thickness", -1.0)][TestCase("Thickness", 1)]
        [TestCase("Density", double.PositiveInfinity)][TestCase("Density", -1.0)][TestCase("AssignedCount", -1)]
        public void FireproofingRejectsUnwrittenUnknownOrMalformedEngineeringFields(string key, object bad)
        { var n = new PropertyAttributeFixture.Native { Output = (field, value) => field == key ? bad : value }; using (var w = Wrap(n)) Assert.Throws<InvalidOperationException>(() => w.Property.GetFireProofingSpecDetails(1)); }
        [TestCase("CreateMemberAttribute")][TestCase("CreateElementAttribute")][TestCase("DeleteMemberAttribute")][TestCase("DeleteElementAttribute")]
        [TestCase("RemoveAttribute")][TestCase("AssignMemberAttribute")][TestCase("AssignElementAttribute")]
        public void AttributeMutationsUseZeroSuccessMinusOneFailureAndRetainPairWithoutFabricatedIds(string name)
        {
            var n = new PropertyAttributeFixture.Native(); Respond(n, name, 0, -1); using (var w = Wrap(n))
            { var r = (PropertyMutationResult)Invoke(w.Property, name); Assert.That(r.Status, Is.EqualTo(PropertyMutationStatus.Succeeded)); Assert.That(r.Attribute.Name, Is.EqualTo(" Purpose ")); Assert.That(r.Attribute.Value, Is.EqualTo(" VALUE ")); Assert.That(r.NativeReturnCode, Is.Zero); Assert.That(r.CreatedPropertyId, Is.Null); Assert.That(r.CreatedSpecificationId, Is.Null);
                Assert.That(((PropertyMutationResult)Invoke(w.Property, name)).Status, Is.EqualTo(PropertyMutationStatus.NativeFailure)); }
        }
        [Test]
        public void AssignedCountRetainsAmbiguousRequestedIdentityWithoutIndexCallsOrGeometry()
        { var n = new PropertyAttributeFixture.Native(); using (var w = Wrap(n)) { var r = w.Property.GetAssignedAttributeCount(17); Assert.That(r.Data.MemberOrPlateId, Is.EqualTo(17)); Assert.That(r.Data.Count, Is.EqualTo(2)); Assert.That(n.Calls, Is.EqualTo(new[] { "GetAssignedAttributeCount" })); } }
        [TestCase("RemoveAttribute", PropertyAttributeScope.MemberOrPlate, PropertyMutationTargetKind.MemberOrPlate)]
        [TestCase("AssignMemberAttribute", PropertyAttributeScope.Member, PropertyMutationTargetKind.Member)]
        [TestCase("AssignElementAttribute", PropertyAttributeScope.Element, PropertyMutationTargetKind.Element)]
        public void AttributeAssignmentsAreBoundedSinglePassOwnedAndScopeExplicit(string name, PropertyAttributeScope scope, PropertyMutationTargetKind target)
        {
            var n = new PropertyAttributeFixture.Native(); using (var w = Wrap(n))
            {
                var ids = new SinglePass(); var r = (PropertyMutationResult)Invoke(w.Property, name, new object[] { Attribute(), ids }); Assert.That(ids.Enumerations, Is.EqualTo(1)); Assert.That(r.TargetIds, Is.EqualTo(new[] { 41, 17, 41 })); Assert.That(r.AttributeScope, Is.EqualTo(scope)); Assert.That(r.TargetKind, Is.EqualTo(target));
                ((int[])n.Arguments.Last()[2])[0] = 999; Assert.That(r.TargetIds[0], Is.EqualTo(41)); Assert.Throws<NotSupportedException>(() => ((IList<int>)r.TargetIds)[0] = 7); n.Calls.Clear();
                foreach (var invalid in new[] { new int[0], new[] { 0 }, new[] { -1 }, Enumerable.Repeat(1, 100001).ToArray() }) Assert.Throws<ArgumentException>(() => Invoke(w.Property, name, new object[] { Attribute(), invalid }));
                Assert.Throws<ArgumentNullException>(() => Invoke(w.Property, name, new object[] { Attribute(), null })); Assert.That(n.Calls, Is.Empty);
            }
        }
        private sealed class SinglePass : IEnumerable<int>
        { public int Enumerations; public IEnumerator<int> GetEnumerator() { if (++Enumerations != 1) throw new InvalidOperationException(); return ((IEnumerable<int>)new[] { 41, 17, 41 }).GetEnumerator(); } IEnumerator IEnumerable.GetEnumerator() => GetEnumerator(); }
        [Test]
        public void UptTableIdentityAndDistinctScalarVersusArrayStatusContractsArePreserved()
        {
            var n = new PropertyAttributeFixture.Native(); using (var w = Wrap(n))
            {
                Respond(n, "CreateUPTTable", 7, -6031, 0); var table = w.Property.CreateUPTTable(UserProvidedTableType.WideFlange); Assert.That(table.CreatedTableId, Is.EqualTo(7)); Assert.That(table.TableType, Is.EqualTo(UserProvidedTableType.WideFlange)); Assert.That(table.CreatedPropertyId, Is.Null); Assert.That(table.CreatedSpecificationId, Is.Null);
                Assert.That(w.Property.CreateUPTTable(UserProvidedTableType.WideFlange).Status, Is.EqualTo(PropertyMutationStatus.InvalidInput)); Assert.Throws<InvalidOperationException>(() => w.Property.CreateUPTTable(UserProvidedTableType.WideFlange));
                Respond(n, "AddUPTPropertyWIDEFLANGE", 0, -6045, -6032); var r = w.Property.AddUPTPropertyWIDEFLANGE(7, " EXACT ", Wide()); Assert.That(r.Status, Is.EqualTo(PropertyMutationStatus.Succeeded)); Assert.That(r.TableId, Is.EqualTo(7)); Assert.That(r.SectionName, Is.EqualTo(" EXACT ")); Assert.That(r.CreatedTableId, Is.Null); Assert.That(r.CreatedPropertyId, Is.Null);
                Assert.That(n.Arguments.Last().Skip(2), Is.EqualTo(Enumerable.Range(1, 10).Select(i => (double)i))); Assert.That(w.Property.AddUPTPropertyWIDEFLANGE(7, "X", Wide()).Status, Is.EqualTo(PropertyMutationStatus.AlreadyExists)); Assert.That(w.Property.AddUPTPropertyWIDEFLANGE(7, "X", Wide()).Status, Is.EqualTo(PropertyMutationStatus.NativeFailure));
                var d = new UptUnequalWideFlangeDefinition(Wide(), 11, 12); Respond(n, "AddUPTPropertyWIDEFLANGEUNEQUAL", true, false, 0, 1);
                r = w.Property.AddUPTPropertyWIDEFLANGEUNEQUAL(7, "X", d); Assert.That(r.NativeBooleanReturn, Is.True); Assert.That(r.NativeReturnCode, Is.Null); Assert.That(n.Arguments.Last()[2], Is.EqualTo(Enumerable.Range(1, 12).Select(i => (double)i)));
                ((double[])n.Arguments.Last()[2])[0] = 999; Assert.That(d.PrimaryValues.AxialArea, Is.EqualTo(1)); Assert.That(w.Property.AddUPTPropertyWIDEFLANGEUNEQUAL(7, "X", d).IsSuccessful, Is.False); Assert.That(w.Property.AddUPTPropertyWIDEFLANGEUNEQUAL(7, "X", d).IsSuccessful, Is.False); Assert.That(w.Property.AddUPTPropertyWIDEFLANGEUNEQUAL(7, "X", d).NativeReturnCode, Is.EqualTo(1));
            }
        }
        [TestCase("GetInactiveMemberCount")][TestCase("GetFireProofedBeamCount")][TestCase("GetFireProofingSpecCount")][TestCase("GetMemberAttributeCount")]
        [TestCase("GetFireProofingSpecAssignedBeamCount")][TestCase("GetAssignedAttributeCount")][TestCase("GetMemberCountByAttribute")][TestCase("GetElementCountByAttribute")]
        [TestCase("CreateMemberAttribute")][TestCase("CreateElementAttribute")][TestCase("DeleteMemberAttribute")][TestCase("DeleteElementAttribute")]
        [TestCase("RemoveAttribute")][TestCase("AssignMemberAttribute")][TestCase("AssignElementAttribute")][TestCase("CreateUPTTable")][TestCase("AddUPTPropertyWIDEFLANGE")]
        public void IntegralContractsRejectBooleanTextNullFractionalAndUndocumentedReturns(string name)
        {
            foreach (object bad in new object[] { true, "1", null, .5, -9999 })
            { var n = new PropertyAttributeFixture.Native(); Respond(n, name, new[] { bad }); using (var w = Wrap(n)) Assert.Throws<InvalidOperationException>(() => Invoke(w.Property, name)); }
        }
        [Test]
        public void InvalidSelectorsInputsAndLimitsFailBeforeNativeWrites()
        {
            var n = new PropertyAttributeFixture.Native(); using (var w = Wrap(n))
            {
                Assert.Throws<ArgumentNullException>(() => w.Property.CreateMemberAttribute(null)); Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.GetFireProofingSpecDetails(0)); Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.GetAssignedAttributeCount(0));
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.GetFireProofedBeamList(100001)); Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.GetMemberAttributeList(0)); Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.GetMemberListByAttribute(Attribute(), 100001));
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.CreateUPTTable((UserProvidedTableType)0)); Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.CreateUPTTable((UserProvidedTableType)11));
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.AddUPTPropertyWIDEFLANGE(0, "X", Wide())); Assert.Throws<ArgumentNullException>(() => w.Property.AddUPTPropertyWIDEFLANGE(7, "X", null));
                Assert.Throws<ArgumentException>(() => w.Property.AddUPTPropertyWIDEFLANGEUNEQUAL(7, "a\0b", new UptUnequalWideFlangeDefinition(Wide(), 11, 12))); Assert.That(n.Calls, Is.Empty);
            }
            Assert.Throws<ArgumentNullException>(() => new PropertyAttribute(null, "X")); Assert.Throws<ArgumentException>(() => new PropertyAttribute("X", "a\0b")); Assert.That(new PropertyAttribute("", " ").Value, Is.EqualTo(" "));
            Assert.Throws<ArgumentOutOfRangeException>(() => new UptWideFlangeDefinition(0, 2, 3, 4, 5, 6, 7, 8, 9, 10)); Assert.Throws<ArgumentOutOfRangeException>(() => new UptUnequalWideFlangeDefinition(Wide(), double.NaN, 12)); Assert.Throws<ArgumentNullException>(() => new UptUnequalWideFlangeDefinition(null, 11, 12));
        }
        [Test]
        public void NewCompoundQueriesAndMutationsShareTheExistingSerializationGate()
        {
            var n = new PropertyAttributeFixture.Native(); using (var w = Wrap(n))
            {
                int active = 0, maximum = 0; n.OnCall = op => { int current = Interlocked.Increment(ref active); maximum = Math.Max(maximum, current); Thread.Sleep(15); Interlocked.Decrement(ref active); };
                Task.WaitAll(Task.Run(() => w.Property.GetMemberListByAttribute(Attribute())), Task.Run(() => w.Property.GetIsotropicMaterialCount()), Task.Run(() => w.Property.CreateMemberAttribute(Attribute())));
                Assert.That(maximum, Is.EqualTo(1)); int start = n.Calls.IndexOf("GetMemberListByAttribute") - 1; Assert.That(n.Calls.Skip(start).Take(3), Is.EqualTo(new[] { "GetMemberCountByAttribute", "GetMemberListByAttribute", "GetMemberCountByAttribute" }));
            }
        }
    }
}
