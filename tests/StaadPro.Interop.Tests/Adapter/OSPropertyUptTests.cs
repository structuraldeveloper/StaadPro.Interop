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
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Property;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSPropertyUptTests
    {
        private static readonly string[] Names = { "AddUPTPropertyWIDEFLANGECOMPOSITE", "AddUPTPropertyCHANNEL", "AddUPTPropertyANGLE", "AddUPTPropertyDOUBLEANGLE", "AddUPTPropertyTEE",
            "AddUPTPropertyPIPE", "AddUPTPropertyTUBE", "AddUPTPropertyGENERAL", "AddUPTPropertyISECTION", "AddUPTPropertyPRISMATIC", "CreatePropertyFromUPTTable",
            "CreateUPTTableEx", "RemoveUPTTable", "RemovePropertyFromUPTTable", "CreatePropertyFromUserTable", "GetUserProvidedTableCount", "GetUserProvidedTableList",
            "GetUserProvidedTableSectionCount", "GetUserProvidedTableSectionType", "GetUserProvidedTableSectionPropertyCount", "GetUserProvidedTableSectionProperties",
            "GetUserProvidedTableNo", "GetShapeCode", "SetStandardProfileDBFolder", "GetStandardProfileDBFolder" };
        private static readonly string[] Scalars = { "AddUPTPropertyCHANNEL", "AddUPTPropertyANGLE", "AddUPTPropertyDOUBLEANGLE", "AddUPTPropertyTEE", "AddUPTPropertyPIPE", "AddUPTPropertyTUBE", "AddUPTPropertyGENERAL", "AddUPTPropertyISECTION", "AddUPTPropertyPRISMATIC" };
        public static IEnumerable<TestCaseData> Ports => Names.Select(n => new TestCaseData(n));
        private static MethodInfo Method(string name) => typeof(IOSProperty).GetMethods().Single(m => m.Name == name);
        private static OpenStaadWrapper Wrap(PropertyUptFixture.Native n, Version version = null) => new OpenStaadWrapper(new PropertyUptFixture.Root { Property = n, NativeVersion = version ?? new Version(26, 0, 0, 340) });
        private static object Definition(Type type)
        {
            if (type == typeof(UptCompositeWideFlangeDefinition)) return PropertyUptFixture.Composite();
            if (type == typeof(UptChannelDefinition)) return new UptChannelDefinition(PropertyUptFixture.Primary(), 11);
            if (type == typeof(UptTeeDefinition)) return new UptTeeDefinition(PropertyUptFixture.Primary(), 11);
            if (type == typeof(UptPipeDefinition)) return new UptPipeDefinition(2, 1, 3, 4);
            var ctor = type.GetConstructors().Single(); return ctor.Invoke(Enumerable.Range(1, ctor.GetParameters().Length).Select(i => (object)(double)i).ToArray());
        }
        private static object[] Args(string name) => Method(name).GetParameters().Select(p =>
            p.Name == "definition" ? Definition(p.ParameterType) : p.ParameterType == typeof(Country) ? (object)Country.Indian : p.ParameterType == typeof(UserProvidedTableType) ? UserProvidedTableType.WideFlange :
            p.ParameterType == typeof(string) ? (object)(p.Name == "folderPath" ? @"C:\STAAD\Profiles" : " EXACT_SECTION ") : p.ParameterType == typeof(int) ? (object)(p.Name == "maximumResults" ? 100000 : p.Name == "nativeTableIndex" ? 1 : 7) : null).ToArray();
        private static object Invoke(IOSProperty p, string name, object[] args = null)
        { try { return Method(name).Invoke(p, args ?? Args(name)); } catch (TargetInvocationException e) { ExceptionDispatchInfo.Capture(e.InnerException).Throw(); throw; } }
        private static object Value(object result, string name) => result.GetType().GetProperty(name).GetValue(result);
        private static void Respond(PropertyUptFixture.Native n, string op, params object[] values) => n.Responses[op] = new Queue<object>(values);
        [Test]
        public void AddsExactlyTwentyFiveTypedContractsAndKeepsUnverifiedOptionalTransportsOutOfTheCommonAdapter()
        {
            Assert.That(Names.Distinct().Count(), Is.EqualTo(25)); Assert.That(typeof(IOSProperty).GetMethods().Count(m => !m.IsSpecialName), Is.EqualTo(225));
            foreach (string name in Names)
            {
                var m = Method(name); var impl = typeof(OSPropertyAdapter).GetMethod(name, m.GetParameters().Select(p => p.ParameterType).ToArray()); Assert.That(impl.ReturnType, Is.EqualTo(m.ReturnType));
                Assert.That(impl.GetParameters().Select(p => p.Name), Is.EqualTo(m.GetParameters().Select(p => p.Name))); Assert.That(impl.GetParameters().Select(p => p.DefaultValue), Is.EqualTo(m.GetParameters().Select(p => p.DefaultValue)));
                Assert.That(m.GetParameters().Any(p => p.IsOut || p.ParameterType.IsByRef || p.ParameterType.IsArray || p.ParameterType == typeof(object)), Is.False);
            }
            foreach (string name in new[] { "GetUptGeneralProfilePointsCount", "GetUptGeneralProfileBoundaryPoints", "GetUptGeneralStressLocationPoints", "FindUPTTable", "GetUserProvidedTableSectionList" })
                Assert.That(typeof(OSPropertyAdapter).GetMethods().Any(m => m.Name == name), Is.False);
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryRouteRunsWithQualifiedEvidenceOnTheCallingThread(string name)
        { var n = new PropertyUptFixture.Native(); using (var w = Wrap(n)) { Assert.That(Value(Invoke(w.Property, name), "IsSuccessful"), Is.True); Assert.That(n.Calls, Is.Not.Empty); Assert.That(n.Threads.All(t => t == Thread.CurrentThread.ManagedThreadId), Is.True); Assert.That(n.Calls.Any(c => c.StartsWith("Save") || c.StartsWith("Analyze")), Is.False); } }
        [TestCaseSource(nameof(Ports))]
        public void EveryRouteRejectsDisposedBindingBeforeNativeDispatch(string name)
        { var n = new PropertyUptFixture.Native(); var w = Wrap(n); var p = w.Property; w.Dispose(); Assert.Throws<InvalidOperationException>(() => Invoke(p, name)); Assert.That(n.Calls, Is.Empty); }
        [TestCaseSource(nameof(Ports))]
        public void EveryRouteStopsAfterTheFirstDispatchWhenTheCapturedReceiverChanges(string name)
        {
            var n = new PropertyUptFixture.Native(); var root = new PropertyUptFixture.Root { Property = n }; using (var w = new OpenStaadWrapper(root))
            { var p = w.Property; n.OnCall = op => { n.OnCall = null; root.Property = new PropertyUptFixture.Native(); w.RefreshComObjects(); }; Assert.Throws<InvalidOperationException>(() => Invoke(p, name)); Assert.That(n.Calls.Count, Is.EqualTo(1)); }
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryRoutePropagatesOriginalTransportFailuresWithoutRetry(string name)
        {
            var n = new PropertyUptFixture.Native(); var error = new COMException("Transport", unchecked((int)0x80004005)); n.OnCall = op => throw error;
            using (var w = Wrap(n)) { Assert.That(Assert.Throws<COMException>(() => Invoke(w.Property, name)), Is.SameAs(error)); Assert.That(n.Calls.Count, Is.EqualTo(1)); }
        }
        [TestCaseSource(nameof(Ports))]
        public void BothPublicSurfacesShipMatchingExpandedXmlAndGuardedMutations(string name)
        {
            var xml = XDocument.Load(Path.ChangeExtension(typeof(IOSProperty).Assembly.Location, ".xml")); string prefix = "M:StaadPro.Interop.Adapters.Interfaces.IOSProperty." + name;
            var entry = xml.Descendants("member").Single(e => (string)e.Attribute("name") == prefix || ((string)e.Attribute("name")).StartsWith(prefix + "("));
            var impl = xml.Descendants("member").Single(e => (string)e.Attribute("name") == ((string)entry.Attribute("name")).Replace("Interfaces.IOSProperty", "Models.OSPropertyAdapter"));
            Assert.That(impl.Elements().Select(e => e.ToString()), Is.EqualTo(entry.Elements().Select(e => e.ToString())));
            foreach (string tag in new[] { "summary", "returns", "remarks", "example" }) Assert.That(entry.Element(tag)?.Value, Is.Not.Null.And.Not.Empty);
            Assert.That(entry.Elements("param").Select(e => (string)e.Attribute("name")), Is.EqualTo(Method(name).GetParameters().Select(p => p.Name))); Assert.That(entry.Element("remarks").Value, Does.Contain("When to use:"));
            Assert.That(entry.Descendants("include").Concat(entry.Descendants("inheritdoc")), Is.Empty); Assert.That(entry.Element("example").Value, Method(name).ReturnType == typeof(PropertyMutationResult) ? Does.Contain("bool applyChanges = false") : Does.Contain("IsSuccessful"));
        }
        [Test]
        public void InstalledConsumerFixtureExecutesAllTwentyFiveNewRoutes()
        { var n = new PropertyUptFixture.Native(); using (var w = Wrap(n)) { PropertyUptFixture.VerifyCalls(w.Property, n); Assert.That(n.Calls.Where(Names.Contains).Distinct(), Is.EquivalentTo(Names)); } }
        [TestCaseSource(nameof(Scalars))]
        public void ScalarInputsFollowNativeOrderAndKeepZeroSuccessDuplicateAndFailureDistinct(string name)
        {
            var n = new PropertyUptFixture.Native(); Respond(n, name, 0, -6045, -6032); using (var w = Wrap(n))
            {
                var r = (PropertyMutationResult)Invoke(w.Property, name); var actual = n.Arguments.Last().Skip(2).Cast<double>().ToArray();
                double[] expected = name.EndsWith("CHANNEL") ? new[] { 1.0, 2, 3, 4, 5, 6, 7, 8, 11, 9, 10 } : name.EndsWith("TEE") ? new[] { 1.0, 2, 4, 5, 3, 6, 7, 8, 11, 9, 10 } : name.EndsWith("PIPE") ? new[] { 2.0, 1, 3, 4 } : Enumerable.Range(1, actual.Length).Select(i => (double)i).ToArray();
                Assert.That(actual, Is.EqualTo(expected)); Assert.That(r.Status, Is.EqualTo(PropertyMutationStatus.Succeeded)); Assert.That(r.NativeReturnCode, Is.Zero); Assert.That(r.TableId, Is.EqualTo(7)); Assert.That(r.SectionName, Is.EqualTo(" EXACT_SECTION ")); Assert.That(r.CreatedPropertyId, Is.Null); Assert.That(r.CreatedTableId, Is.Null);
                Assert.That(((PropertyMutationResult)Invoke(w.Property, name)).Status, Is.EqualTo(PropertyMutationStatus.AlreadyExists)); Assert.That(((PropertyMutationResult)Invoke(w.Property, name)).Status, Is.EqualTo(PropertyMutationStatus.NativeFailure));
            }
        }
        [TestCase(12)][TestCase(16)][TestCase(19)]
        public void CompositePacketsHaveOnlyReviewedLengthsAndOwnedExactNativeOrder(int length)
        {
            var d = PropertyUptFixture.Composite(length); var n = new PropertyUptFixture.Native(); using (var w = Wrap(n))
            {
                var r = w.Property.AddUPTPropertyWIDEFLANGECOMPOSITE(7, "COMPOSITE", d); Assert.That(d.NativeValueCount, Is.EqualTo(length)); Assert.That(n.Arguments.Last()[2], Is.EqualTo(Enumerable.Range(1, length).Select(i => (double)i))); Assert.That(r.NativeBooleanReturn, Is.True); Assert.That(r.NativeReturnCode, Is.Null);
                ((double[])n.Arguments.Last()[2])[0] = 999; Assert.That(d.SteelValues.PrimaryValues.AxialArea, Is.EqualTo(1)); Respond(n, "AddUPTPropertyWIDEFLANGECOMPOSITE", false); Assert.That(w.Property.AddUPTPropertyWIDEFLANGECOMPOSITE(7, "COMPOSITE", d).IsSuccessful, Is.False);
            }
        }
        [Test]
        public void CompositeBottomPlateRequiresSlabAndNeverFillsOmittedEngineeringInputs()
        {
            var steel = new UptUnequalWideFlangeDefinition(PropertyUptFixture.Primary(), 11, 12); var plate = new UptCompositeBottomPlate(17, 18, 19);
            Assert.Throws<ArgumentException>(() => new UptCompositeWideFlangeDefinition(steel, null, plate)); Assert.Throws<ArgumentNullException>(() => new UptCompositeWideFlangeDefinition(null));
            Assert.That(new UptCompositeWideFlangeDefinition(steel).Slab, Is.Null); Assert.That(new UptCompositeWideFlangeDefinition(steel).BottomPlate, Is.Null);
        }
        [Test]
        public void PropertyAndTableCreationRetainRequestedAndReturnedReferenceDomainsAndArgumentOrder()
        {
            var n = new PropertyUptFixture.Native(); using (var w = Wrap(n))
            {
                var a = w.Property.CreatePropertyFromUPTTable(7, " EXACT "); Assert.That(a.CreatedPropertyId, Is.EqualTo(801)); Assert.That(a.CreatedTableId, Is.Null); Assert.That(n.Arguments.Last(), Is.EqualTo(new object[] { 7, " EXACT " }));
                var b = w.Property.CreatePropertyFromUserTable(" EXACT ", 7); Assert.That(b.CreatedPropertyId, Is.EqualTo(802)); Assert.That(n.Arguments.Last(), Is.EqualTo(new object[] { " EXACT ", 7 }));
                var t = w.Property.CreateUPTTableEx(7, UserProvidedTableType.Channel); Assert.That(t.TableId, Is.EqualTo(7)); Assert.That(t.CreatedTableId, Is.EqualTo(13)); Assert.That(t.CreatedPropertyId, Is.Null); Assert.That(n.Arguments.Last(), Is.EqualTo(new object[] { 7, 2 }));
                Respond(n, "CreatePropertyFromUPTTable", -1); Assert.That(w.Property.CreatePropertyFromUPTTable(7, "X").Status, Is.EqualTo(PropertyMutationStatus.NativeFailure));
                Respond(n, "CreatePropertyFromUserTable", -1); Assert.Throws<InvalidOperationException>(() => w.Property.CreatePropertyFromUserTable("X", 7));
                Respond(n, "CreateUPTTableEx", 0); Assert.That(w.Property.CreateUPTTableEx(7, UserProvidedTableType.Channel).Status, Is.EqualTo(PropertyMutationStatus.NativeFailure));
            }
        }
        [Test]
        public void RemovalUsesItsOwnReturnConventionsAndNeverPerformsHiddenCascadeOrFallback()
        {
            var n = new PropertyUptFixture.Native(); using (var w = Wrap(n))
            {
                Respond(n, "RemoveUPTTable", true, false); Assert.That(w.Property.RemoveUPTTable(7).NativeBooleanReturn, Is.True); Assert.That(w.Property.RemoveUPTTable(7).IsSuccessful, Is.False);
                Respond(n, "RemovePropertyFromUPTTable", 1, 0, -100); Assert.That(w.Property.RemovePropertyFromUPTTable(7, " X ").Status, Is.EqualTo(PropertyMutationStatus.Succeeded)); Assert.That(w.Property.RemovePropertyFromUPTTable(7, " X ").Status, Is.EqualTo(PropertyMutationStatus.NativeFailure)); Assert.That(w.Property.RemovePropertyFromUPTTable(7, " X ").Status, Is.EqualTo(PropertyMutationStatus.InvalidInput));
                Assert.That(n.Calls, Is.EqualTo(new[] { "RemoveUPTTable", "RemoveUPTTable", "RemovePropertyFromUPTTable", "RemovePropertyFromUPTTable", "RemovePropertyFromUPTTable" }));
            }
        }
        [Test]
        public void TableInventoryIsBoundedOwnedAndRecheckedWithEmptyFastPath()
        {
            var n = new PropertyUptFixture.Native(); using (var w = Wrap(n))
            {
                var r = w.Property.GetUserProvidedTableList(); Assert.That(r.Data, Is.EqualTo(new[] { 7, 9, 7 })); ((int[])n.Buffers["TableIds"])[0] = 999; Assert.That(r.Data[0], Is.EqualTo(7)); Assert.Throws<NotSupportedException>(() => ((IList<int>)r.Data)[0] = 7);
                Respond(n, "GetUserProvidedTableCount", 3, 4); Assert.That(w.Property.GetUserProvidedTableList().Status, Is.EqualTo(PropertyQueryStatus.StateChanged));
                Respond(n, "GetUserProvidedTableList", false); Assert.That(w.Property.GetUserProvidedTableList().Status, Is.EqualTo(PropertyQueryStatus.NativeFailure));
                n.Calls.Clear(); n.TableCount = 0; Assert.That(w.Property.GetUserProvidedTableList().Data, Is.Empty); Assert.That(n.Calls, Is.EqualTo(new[] { "GetUserProvidedTableCount", "GetUserProvidedTableCount" }));
                n.Calls.Clear(); n.TableCount = 100001; Assert.That(w.Property.GetUserProvidedTableList().Status, Is.EqualTo(PropertyQueryStatus.LimitExceeded)); Assert.That(n.Calls.Count, Is.EqualTo(1));
            }
        }
        [Test]
        public void TableTypeIsASingularWrittenValueAndUsesItsCountReturnWithoutInventingArrayTransport()
        {
            var n = new PropertyUptFixture.Native(); using (var w = Wrap(n))
            {
                var r = w.Property.GetUserProvidedTableSectionType(7); Assert.That(r.Data.TableId, Is.EqualTo(7)); Assert.That(r.Data.NativeTypeCode, Is.EqualTo(412)); Assert.That(r.Data.SectionCount, Is.EqualTo(2)); Assert.That(r.NativeReturnCode, Is.EqualTo(2));
                Respond(n, "GetUserProvidedTableSectionType", 1); Assert.That(w.Property.GetUserProvidedTableSectionType(7).Status, Is.EqualTo(PropertyQueryStatus.StateChanged));
                Respond(n, "GetUserProvidedTableSectionCount", 2, 3); Assert.That(w.Property.GetUserProvidedTableSectionType(7).Status, Is.EqualTo(PropertyQueryStatus.StateChanged));
                n.SectionCount = 0; n.TypeCode = int.MinValue; Assert.That(w.Property.GetUserProvidedTableSectionType(7).Status, Is.EqualTo(PropertyQueryStatus.EmptyOrUnavailable));
                n.SectionCount = 2; n.TypeCode = 12345; Assert.That(w.Property.GetUserProvidedTableSectionType(7).Data.NativeTypeCode, Is.EqualTo(12345));
            }
        }
        public static IEnumerable<TestCaseData> LayoutCases
        {
            get
            {
                var layouts = new Dictionary<int, string[]> { [502] = "Ax Iz Iy Ix Ay Az YD ZD".Split(' '), [482] = "Ax D Td B Tb Iz Iy Ix Sz Sy Ay Az Pz Py Hss Dee".Split(' '), [412] = "Ax D Tw Wf Tf Iz Iy Ix Ay Az Wf1 Tf1".Split(' '),
                    [422] = "Ax D Tw Wf Tf Iz Iy Ix Cz Ay Az".Split(' '), [432] = "D Wf Tf R Ay Az".Split(' '), [442] = "D Wf Tf SP Iz Iy Ix Cy Ay Az".Split(' '), [452] = "Ax D Wf Tf Tw Iz Iy Ix Cy Ay Az".Split(' '), [462] = "OD ID Ay Az".Split(' '), [472] = "Ax D Wf Tf Iz Iy Ix Ay Az".Split(' '), [492] = "Dww Tww Dww1 Bff Tff Bff1 Tff1 Ayf Azf Xif".Split(' ') };
                foreach (string version in new[] { "24.0.2.354", "25.0.1.424", "26.0.0.340" }) foreach (var pair in layouts) yield return new TestCaseData(version, pair.Key, pair.Value);
            }
        }
        [TestCaseSource(nameof(LayoutCases))]
        public void AllTenLayoutsUseTheExactNativeProfileAndOwnedApplicableSlots(string version, int type, string[] fields)
        {
            var n = new PropertyUptFixture.Native { TypeCode = type, WrittenFields = fields.Length }; using (var w = Wrap(n, Version.Parse(version)))
            {
                var d = w.Property.GetUserProvidedTableSectionProperties(7, " EXACT ").Data; Assert.That(d.TableId, Is.EqualTo(7)); Assert.That(d.SectionName, Is.EqualTo(" EXACT ")); Assert.That(d.NativeContractVersion, Is.EqualTo(Version.Parse(version))); Assert.That(d.NativeTypeCode, Is.EqualTo(type)); Assert.That(d.NativeBufferCapacity, Is.EqualTo(24)); Assert.That(d.FieldNames, Is.EqualTo(fields)); Assert.That(d.Values, Is.EqualTo(Enumerable.Range(0, fields.Length).Select(i => i + .5)));
                ((double[])n.Buffers["PropertyValues"])[0] = 999; Assert.That(d.Values[0], Is.EqualTo(.5)); Assert.Throws<NotSupportedException>(() => ((IList<double>)d.Values)[0] = 7); Assert.Throws<NotSupportedException>(() => ((IList<string>)d.FieldNames)[0] = "EDITED");
                n.WrittenFields = fields.Length - 1; Assert.Throws<InvalidOperationException>(() => w.Property.GetUserProvidedTableSectionProperties(7, " EXACT "));
            }
        }
        [Test]
        public void LayoutCapacityComesFromTheDedicatedCapacityApiAndChangesInvalidateData()
        {
            var n = new PropertyUptFixture.Native(); using (var w = Wrap(n))
            {
                Respond(n, "GetUserProvidedTableSectionPropertyCount", 99999); Assert.That(w.Property.GetUserProvidedTableSectionPropertyCount(7, "X").Data.NativeCount, Is.EqualTo(99999)); n.Calls.Clear();
                Assert.That(w.Property.GetUserProvidedTableSectionProperties(7, "X").Data.Values.Count, Is.EqualTo(12)); Assert.That(n.Calls, Does.Not.Contain("GetUserProvidedTableSectionPropertyCount"));
                Respond(n, "GetCountofSectionPropertyValuesEx", 24, 25); Assert.That(w.Property.GetUserProvidedTableSectionProperties(7, "X").Status, Is.EqualTo(PropertyQueryStatus.StateChanged));
                n.TypeCode = 12345; Assert.Throws<NotSupportedException>(() => w.Property.GetUserProvidedTableSectionProperties(7, "X"));
            }
        }
        [TestCase("23.0.0.1")][TestCase("24.0.0.1")][TestCase("25.0.0.539")][TestCase("26.0.0.341")][TestCase("27.0.0.1")]
        public void UnreviewedProfilesCannotSelectLayoutsOrFolderMutationPolicies(string version)
        {
            var n = new PropertyUptFixture.Native(); using (var w = Wrap(n, Version.Parse(version)))
            { Assert.Throws<NotSupportedException>(() => w.Property.GetUserProvidedTableSectionProperties(7, "X")); Assert.Throws<NotSupportedException>(() => w.Property.SetStandardProfileDBFolder(@"C:\Profiles")); Assert.That(n.Calls, Is.Empty); }
        }
        [TestCase("24.0.2.354", 1, 0)][TestCase("25.0.1.424", 0, -1)][TestCase("26.0.0.340", 0, -1)]
        public void FolderSetterSelectsExactAcquiredNativeReturnPolicyAndRetainsEvidence(string version, int success, int failure)
        {
            var n = new PropertyUptFixture.Native(); Respond(n, "SetStandardProfileDBFolder", success, failure); using (var w = Wrap(n, Version.Parse(version)))
            {
                w.ApplicationVersion.Major = 999; var r = w.Property.SetStandardProfileDBFolder(@"C:\ EXACT \Profiles"); Assert.That(r.IsSuccessful, Is.True); Assert.That(r.NativeReturnCode, Is.EqualTo(success)); Assert.That(r.FolderPath, Is.EqualTo(@"C:\ EXACT \Profiles")); Assert.That(r.NativeContractVersion, Is.EqualTo(Version.Parse(version))); Assert.That(r.TargetKind, Is.EqualTo(PropertyMutationTargetKind.ProfileDatabaseFolder));
                Assert.That(w.Property.SetStandardProfileDBFolder("").Status, Is.EqualTo(PropertyMutationStatus.NativeFailure)); Assert.That(n.Arguments.Last()[0], Is.EqualTo(""));
            }
        }
        [Test]
        public void NativeVersionCopiesCannotChangeTheAcquiredContractPolicy()
        {
            var n = new PropertyUptFixture.Native(); using (var w = Wrap(n))
            {
                n.OnCall = op => { w.StaadVersion.Build = 341; };
                var read = w.Property.GetUserProvidedTableSectionProperties(7, "X");
                Assert.That(read.Status, Is.EqualTo(PropertyQueryStatus.Success));
                Assert.That(read.Data.NativeContractVersion, Is.EqualTo(new Version(26, 0, 0, 340)));
                var mutation = w.Property.SetStandardProfileDBFolder(@"C:\Profiles");
                Assert.That(mutation.IsSuccessful, Is.True);
                Assert.That(mutation.NativeContractVersion, Is.EqualTo(new Version(26, 0, 0, 340)));
            }
        }
        public static IEnumerable<TestCaseData> BadBuffers
        { get { foreach (object value in new object[] { null, new double[23], new double[25], new int[24], new object[24], new double[2, 12], Enumerable.Repeat(double.NaN, 24).ToArray() }) yield return new TestCaseData(new object[] { value }); } }
        [TestCaseSource(nameof(BadBuffers))]
        public void MalformedOrUnwrittenPropertyBuffersNeverBecomeEngineeringData(object bad)
        { var n = new PropertyUptFixture.Native { Output = (key, value) => key == "PropertyValues" ? bad : value }; using (var w = Wrap(n)) Assert.Throws<InvalidOperationException>(() => w.Property.GetUserProvidedTableSectionProperties(7, "X")); }
        [Test]
        public void IndexAndShapeObservationsRetainTheirReferenceDomainsAndUnknownRawCodes()
        {
            var n = new PropertyUptFixture.Native(); using (var w = Wrap(n))
            {
                var r = w.Property.GetUserProvidedTableNo(0); Assert.That(r.Data.NativeIndex, Is.Zero); Assert.That(r.Data.TableId, Is.EqualTo(7)); Assert.That(n.Arguments.Last()[0], Is.Zero); Respond(n, "GetUserProvidedTableNo", -1); Assert.That(w.Property.GetUserProvidedTableNo(1).Status, Is.EqualTo(PropertyQueryStatus.NativeFailure));
                Respond(n, "GetShapeCode", 0, 12345, -1); var shape = w.Property.GetShapeCode(Country.Indian, " EXACT "); Assert.That(shape.Data.Country, Is.EqualTo(Country.Indian)); Assert.That(shape.Data.SectionName, Is.EqualTo(" EXACT ")); Assert.That(shape.Data.NativeCode, Is.Zero); Assert.That(w.Property.GetShapeCode(Country.Indian, "X").Data.NativeCode, Is.EqualTo(12345)); Assert.That(w.Property.GetShapeCode(Country.Indian, "X").Status, Is.EqualTo(PropertyQueryStatus.NativeFailure));
                Respond(n, "GetStandardProfileDBFolder", null, DBNull.Value, "", " EXACT ", 7); for (int i = 0; i < 3; i++) Assert.That(w.Property.GetStandardProfileDBFolder().Status, Is.EqualTo(PropertyQueryStatus.EmptyOrUnavailable)); Assert.That(w.Property.GetStandardProfileDBFolder().Data, Is.EqualTo(" EXACT ")); Assert.Throws<InvalidOperationException>(() => w.Property.GetStandardProfileDBFolder());
            }
        }
        [TestCaseSource(nameof(Scalars))]
        [TestCase("CreatePropertyFromUPTTable")][TestCase("CreatePropertyFromUserTable")][TestCase("CreateUPTTableEx")][TestCase("RemovePropertyFromUPTTable")]
        [TestCase("GetUserProvidedTableCount")][TestCase("GetUserProvidedTableSectionCount")][TestCase("GetUserProvidedTableSectionPropertyCount")][TestCase("GetUserProvidedTableNo")][TestCase("GetShapeCode")][TestCase("SetStandardProfileDBFolder")]
        public void IntegralContractsRejectBooleanTextNullFractionalAndUndocumentedReturns(string name)
        {
            foreach (object bad in new object[] { true, "1", null, .5, -9999 })
            { var n = new PropertyUptFixture.Native(); Respond(n, name, new[] { bad }); using (var w = Wrap(n)) Assert.Throws<InvalidOperationException>(() => Invoke(w.Property, name)); }
        }
        [Test]
        public void InvalidInputsAndUnsupportedCompositeStructuresFailBeforeNativeWrites()
        {
            var n = new PropertyUptFixture.Native(); using (var w = Wrap(n))
            {
                Assert.Throws<ArgumentNullException>(() => w.Property.AddUPTPropertyCHANNEL(7, "X", null)); Assert.Throws<ArgumentNullException>(() => w.Property.AddUPTPropertyWIDEFLANGECOMPOSITE(7, "X", null));
                Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.CreateUPTTableEx(7, (UserProvidedTableType)11)); Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.RemoveUPTTable(0)); Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.GetUserProvidedTableList(100001));
                Assert.Throws<ArgumentException>(() => w.Property.CreatePropertyFromUserTable("A\0B", 7)); Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.GetUserProvidedTableNo(-1)); Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.GetShapeCode(Country.Chilean, "X")); Assert.Throws<ArgumentException>(() => w.Property.SetStandardProfileDBFolder("A\0B")); Assert.That(n.Calls, Is.Empty);
            }
            Assert.Throws<ArgumentException>(() => new UptPipeDefinition(1, 2, 3, 4)); Assert.Throws<ArgumentOutOfRangeException>(() => new UptAngleDefinition(double.NaN, 2, 3, 4, 5, 6)); Assert.Throws<ArgumentOutOfRangeException>(() => new UptCompositeSlab(1, 2, 3, double.PositiveInfinity));
            Assert.Throws<ArgumentNullException>(() => new UptChannelDefinition(null, 1)); Assert.Throws<ArgumentNullException>(() => new UptTeeDefinition(null, 1));
        }
        [Test]
        public void CompoundUptObservationsAndExistingQueriesShareSerialization()
        {
            var n = new PropertyUptFixture.Native(); using (var w = Wrap(n))
            {
                int active = 0, maximum = 0; n.OnCall = op => { int current = Interlocked.Increment(ref active); maximum = Math.Max(maximum, current); Thread.Sleep(15); Interlocked.Decrement(ref active); };
                Task.WaitAll(Task.Run(() => w.Property.GetUserProvidedTableList()), Task.Run(() => w.Property.GetIsotropicMaterialCount()), Task.Run(() => w.Property.AddUPTPropertyANGLE(7, "X", new UptAngleDefinition(1, 2, 3, 4, 5, 6))));
                Assert.That(maximum, Is.EqualTo(1)); int start = n.Calls.IndexOf("GetUserProvidedTableList") - 1; Assert.That(n.Calls.Skip(start).Take(3), Is.EqualTo(new[] { "GetUserProvidedTableCount", "GetUserProvidedTableList", "GetUserProvidedTableCount" }));
            }
        }
    }
}
