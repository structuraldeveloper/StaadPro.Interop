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
using Microsoft.CSharp.RuntimeBinder;
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
    public class OSPropertyMainPortTests
    {
        private static OpenStaadWrapper Wrap(PropertyPortFixture.Native native) => new OpenStaadWrapper(new PropertyPortFixture.Root { Property = native });
        private static HashSet<Beam> Beams() => new HashSet<Beam> { new Beam { Id = 17 }, new Beam { Id = 41 }, new Beam { Id = 99 } };
        private static HashSet<Plate> Plates() => new HashSet<Plate> { new Plate { Id = 17 }, new Plate { Id = 99 } };
        private static readonly HashSet<string> NewNames = new HashSet<string>
        {
            "GetUnusedSectionsCount", "GetUnusedSectionsIds", "GetBeamProperty", "GetPlateMaterialName", "GetSectionPropertyList",
            "GetSectionPropertyName", "GetSectionPropertyValues", "GetSectionPropertyAssignedBeams", "GetThicknessPropertyList",
            "GetThicknessPropertyAssignedPlates", "GetAveragePlateThickness", "GetBeamsWithMissingSectionProperty", "GetPlatesWithMissingThicknessProperty",
            "GetMemberSpecification", "AssignBeamProperty", "AssignMaterialToMember", "SetMaterialName", "AssignMemberEndOffsets", "AssignMemberEndOffset"
        };
        private static IEnumerable<MethodInfo> NewMethods => typeof(IOSProperty).GetMethods().Where(m => NewNames.Contains(m.Name));
        public static IEnumerable<TestCaseData> Ports => NewMethods.Select(m => new TestCaseData(m).SetName("{m}_" + m));
        private static object[] Args(MethodInfo method) => method.GetParameters().Select(p =>
            p.ParameterType == typeof(int[]) ? (object)new[] { 17, 41 } :
            p.ParameterType == typeof(HashSet<Beam>) ? Beams() : p.ParameterType == typeof(HashSet<Plate>) ? (object)Plates() :
            p.ParameterType == typeof(IEnumerable<int>) ? new[] { method.Name.Contains("Thickness") ? 45 : 37 } :
            p.ParameterType == typeof(string) ? " STEEL Ω " : p.ParameterType == typeof(OffsetLocation) ? (object)OffsetLocation.End :
            p.ParameterType == typeof(double) ? (object)(-0.15) : p.Name == "location" ? 1 :
            p.Name == "propertyId" || p.Name == "propId" ? (method.Name.Contains("Thickness") ? 45 : 37) : 17).ToArray();
        private static object Invoke(IOSProperty property, MethodInfo method, object[] args = null)
        {
            try { return method.Invoke(property, args ?? Args(method)); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
        }
        private static void Response(PropertyPortFixture.Native native, string op, params object[] values) => native.Responses[op] = new Queue<object>(values);

        [Test]
        public void ExactlyTwentyFiveNewOverloadsAreAvailableOnBothSurfaces()
        {
            Assert.That(NewMethods.Count(), Is.EqualTo(25));
            Assert.That(typeof(IOSProperty).GetMethods().Count(m => !m.IsSpecialName), Is.EqualTo(100));
            foreach (var method in NewMethods)
            {
                var concrete = typeof(OSPropertyAdapter).GetMethod(method.Name, method.GetParameters().Select(p => p.ParameterType).ToArray());
                Assert.That(concrete, Is.Not.Null);
                Assert.That(concrete.ReturnType, Is.EqualTo(method.ReturnType));
                Assert.That(concrete.GetParameters().Select(p => p.Name), Is.EqualTo(method.GetParameters().Select(p => p.Name)));
                Assert.That(method.GetParameters().All(p => !p.IsOut && !p.ParameterType.IsByRef && !p.IsOptional), Is.True);
            }
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryNewPortRunsWithManagedNativeEvidence(MethodInfo method)
        {
            var native = new PropertyPortFixture.Native();
            using (var wrapper = Wrap(native))
            {
                object value = Invoke(wrapper.Property, method);
                if (method.ReturnType == typeof(bool)) Assert.That(value, Is.True);
                else if (method.ReturnType != typeof(void)) Assert.That(value, Is.Not.Null);
                Assert.That(native.Calls, Is.Not.Empty);
                Assert.That(native.Calls.Any(n => n.StartsWith("Save") || n.StartsWith("Analyze")), Is.False);
            }
        }
        [TestCaseSource(nameof(Ports))]
        public void EveryNewPortRejectsDisposedCapturedBinding(MethodInfo method)
        {
            var native = new PropertyPortFixture.Native(); var wrapper = Wrap(native); var property = wrapper.Property;
            wrapper.Dispose(); Assert.Throws<InvalidOperationException>(() => Invoke(property, method)); Assert.That(native.Calls, Is.Empty);
        }
        [TestCaseSource(nameof(Ports))]
        public void InterfaceAndAdapterShipMatchingCompleteXml(MethodInfo method)
        {
            var xml = XDocument.Load(Path.ChangeExtension(typeof(IOSProperty).Assembly.Location, ".xml"));
            var entries = xml.Descendants("member").Where(m => ((string)m.Attribute("name")).StartsWith("M:StaadPro.Interop.Adapters.")).ToArray();
            var name = method.Name; int overloadCount = NewMethods.Count(m => m.Name == name);
            var contract = entries.Where(m => ((string)m.Attribute("name")).StartsWith("M:StaadPro.Interop.Adapters.Interfaces.IOSProperty." + name + "(") || (string)m.Attribute("name") == "M:StaadPro.Interop.Adapters.Interfaces.IOSProperty." + name).ToArray();
            Assert.That(contract.Length, Is.EqualTo(overloadCount));
            foreach (var entry in contract)
            {
                string concreteId = ((string)entry.Attribute("name")).Replace("Interfaces.IOSProperty", "Models.OSPropertyAdapter");
                var concrete = entries.Single(m => (string)m.Attribute("name") == concreteId);
                Assert.That(entry.Elements().Select(e => e.ToString()), Is.EqualTo(concrete.Elements().Select(e => e.ToString())));
                foreach (string tag in new[] { "summary", "remarks", "example" }) Assert.That(entry.Element(tag)?.Value, Is.Not.Null.And.Not.Empty);
                Assert.That(entry.Element("remarks").Value, Does.Contain("When to use:"));
                Assert.That(entry.Descendants("include").Concat(entry.Descendants("inheritdoc")), Is.Empty);
                Assert.That(entry.Descendants().Attributes("cref").Any(a => a.Value.StartsWith("!:")), Is.False);
                if (method.ReturnType != typeof(void)) Assert.That(entry.Element("returns")?.Value, Is.Not.Null.And.Not.Empty);
            }
        }
        [Test]
        public void InventoryMembershipAndMissingChecksPreserveSourceSemanticsAndObjectIdentity()
        {
            var native = new PropertyPortFixture.Native(); var beams = Beams(); var plates = Plates();
            using (var wrapper = Wrap(native))
            {
                var p = wrapper.Property;
                Assert.That(p.GetUnusedSectionsIds(), Is.EquivalentTo(new[] { 38 })); Assert.That(p.GetUnusedSectionsCount(), Is.EqualTo(1));
                Assert.That(p.GetBeamProperty(17), Is.EqualTo(37)); Assert.That(p.GetBeamProperty(99), Is.Zero);
                Assert.That(p.GetSectionPropertyAssignedBeams(beams, 37).All(b => beams.Any(original => ReferenceEquals(original, b))), Is.True);
                Assert.That(p.GetThicknessPropertyAssignedPlates(plates, 45).Single(), Is.SameAs(plates.Single(b => b.Id == 17)));
                Assert.That(p.GetBeamsWithMissingSectionProperty(beams, new[] { 37 }).Select(b => b.Id), Is.EqualTo(new[] { 99 }));
                Assert.That(p.GetPlatesWithMissingThicknessProperty(plates, new[] { 45 }).Select(b => b.Id), Is.EqualTo(new[] { 99 }));
                Assert.That(p.GetBeamsWithMissingSectionProperty(beams, new int[0]), Is.EquivalentTo(beams));
                Assert.That(beams.Count, Is.EqualTo(3)); Assert.That(plates.Count, Is.EqualTo(2));
                Assert.That(p.GetSectionPropertyName(37), Is.EqualTo(" Section Ω "));
                Assert.That(p.GetPlateMaterialName(17), Is.EqualTo(" CONCRETE Ω "));
                Assert.That(p.GetAveragePlateThickness(17), Is.EqualTo(0.25));
                Assert.That(p.GetMemberSpecification(17), Is.EqualTo(MemberSpecificationType.CompressionOnly));
            }
        }
        [TestCase("GetSectionPropertyCount")][TestCase("GetThicknessPropertyCount")]
        [TestCase("GetSectionPropertyAssignedBeamCount")][TestCase("GetThicknessPropertyAssignedPlateCount")]
        public void ZeroCountsSkipListRetrieval(string op)
        {
            var native = new PropertyPortFixture.Native(); Response(native, op, 0);
            using (var wrapper = Wrap(native))
            {
                switch (op)
                {
                    case "GetSectionPropertyCount": Assert.That(wrapper.Property.GetSectionPropertyList(), Is.Empty); break;
                    case "GetThicknessPropertyCount": Assert.That(wrapper.Property.GetThicknessPropertyList(), Is.Empty); break;
                    case "GetSectionPropertyAssignedBeamCount": Assert.That(wrapper.Property.GetSectionPropertyAssignedBeams(Beams(), 37), Is.Empty); break;
                    default: Assert.That(wrapper.Property.GetThicknessPropertyAssignedPlates(Plates(), 45), Is.Empty); break;
                }
                Assert.That(native.Calls, Is.EqualTo(new[] { op }));
            }
        }
        public static IEnumerable<TestCaseData> CountFailures
        {
            get
            {
                foreach (string op in new[] { "GetSectionPropertyCount", "GetThicknessPropertyCount", "GetSectionPropertyAssignedBeamCount", "GetThicknessPropertyAssignedPlateCount", "GetCountofSectionPropertyValuesEx" })
                    foreach (object value in new object[] { -1, 100001, 1.0, "2", true, null }) yield return new TestCaseData(op, value);
            }
        }
        private static void QueryCount(IOSProperty p, string op)
        {
            switch (op)
            {
                case "GetSectionPropertyCount": p.GetSectionPropertyList(); break;
                case "GetThicknessPropertyCount": p.GetThicknessPropertyList(); break;
                case "GetSectionPropertyAssignedBeamCount": p.GetSectionPropertyAssignedBeams(Beams(), 37); break;
                case "GetThicknessPropertyAssignedPlateCount": p.GetThicknessPropertyAssignedPlates(Plates(), 45); break;
                default: p.GetSectionPropertyValues(37); break;
            }
        }
        [TestCaseSource(nameof(CountFailures))]
        public void CountsRejectErrorsCoercionAndUnboundedAllocation(string op, object value)
        { var native = new PropertyPortFixture.Native(); Response(native, op, value); using (var wrapper = Wrap(native)) Assert.Throws<InvalidOperationException>(() => QueryCount(wrapper.Property, op)); }
        [TestCase("GetSectionPropertyCount")][TestCase("GetThicknessPropertyCount")]
        [TestCase("GetSectionPropertyAssignedBeamCount")][TestCase("GetThicknessPropertyAssignedPlateCount")][TestCase("GetCountofSectionPropertyValuesEx")]
        public void CountsAreRecheckedAndObservedChangesFail(string op)
        { var native = new PropertyPortFixture.Native(); Response(native, op, op.Contains("Values") ? 12 : 2, 3); using (var wrapper = Wrap(native)) Assert.Throws<InvalidOperationException>(() => QueryCount(wrapper.Property, op)); }
        [Test]
        public void NegativeAssignedCountsNeverBecomeUnusedSections()
        { var native = new PropertyPortFixture.Native(); Response(native, "GetSectionPropertyAssignedBeamCount", -6022); using (var wrapper = Wrap(native)) Assert.Throws<InvalidOperationException>(() => wrapper.Property.GetUnusedSectionsIds()); }
        public static IEnumerable<TestCaseData> BadBuffers
        {
            get
            {
                foreach (string op in new[] { "GetSectionPropertyList", "GetThicknessPropertyList", "GetSectionPropertyAssignedBeamList", "GetThicknessPropertyAssignedPlateList", "GetSectionPropertyValuesEx", "GetPlateThickness" })
                    foreach (string shape in new[] { "null", "type", "rank", "short", "long", "bad-value" }) yield return new TestCaseData(op, shape);
            }
        }
        private static void QueryOutput(IOSProperty p, string op)
        {
            if (op == "GetPlateThickness") p.GetAveragePlateThickness(17);
            else if (op == "GetSectionPropertyValuesEx") p.GetSectionPropertyValues(37);
            else QueryCount(p, op.Replace("List", "Count"));
        }
        [TestCaseSource(nameof(BadBuffers))]
        public void BufferShapesAndInvalidElementsAreRejected(string op, string shape)
        {
            var native = new PropertyPortFixture.Native(); native.Output = (name, value) =>
            {
                if (name != op) return value;
                var a = (Array)value; var element = a.GetType().GetElementType();
                switch (shape)
                {
                    case "null": return null;
                    case "type": return new object[a.Length];
                    case "rank": return Array.CreateInstance(element, a.Length, 1);
                    case "short": return Array.CreateInstance(element, a.Length - 1);
                    case "long": return Array.CreateInstance(element, a.Length + 1);
                    default: a.SetValue(element == typeof(int) ? (object)0 : double.NaN, 0); return a;
                }
            };
            using (var wrapper = Wrap(native)) Assert.Throws<InvalidOperationException>(() => QueryOutput(wrapper.Property, op));
        }
        [Test]
        public void OwnedArraysSupportNonzeroLowerBoundsAndRawSectionPaddingWithoutParsing()
        {
            var native = new PropertyPortFixture.Native(); native.Sections = new[] { 38, 37, 37 };
            native.Output = (op, value) =>
            {
                if (!(value is Array input)) return value;
                var a = Array.CreateInstance(input.GetType().GetElementType(), new[] { input.Length }, new[] { 5 });
                for (int i = 0; i < input.Length; i++) a.SetValue(input.GetValue(i), i + 5); return a;
            };
            using (var wrapper = Wrap(native))
            {
                int[] ids = wrapper.Property.GetSectionPropertyList(); Assert.That(ids, Is.EqualTo(new[] { 38, 37, 37 }));
                ((Array)native.Buffers["GetSectionPropertyList"]).SetValue(99, 5); Assert.That(ids[0], Is.EqualTo(38));
                double[] values = wrapper.Property.GetSectionPropertyValues(37); Assert.That(values.Length, Is.EqualTo(12));
                Assert.That(values[0], Is.EqualTo(1)); Assert.That(values[10], Is.EqualTo(11)); Assert.That(values[11], Is.Zero);
                ((Array)native.Buffers["GetSectionPropertyValuesEx"]).SetValue(99.0, 5); Assert.That(values[0], Is.EqualTo(1));
            }
        }
        [TestCase(-1)][TestCase(0)][TestCase(1)][TestCase(2)][TestCase(3)][TestCase(4)]
        public void EveryDefinedMemberSpecificationIsRetained(int code)
        { var native = new PropertyPortFixture.Native(); native.Output = (op, value) => op == "GetMemberSpecCode" ? code : value; using (var wrapper = Wrap(native)) Assert.That((int)wrapper.Property.GetMemberSpecification(17), Is.EqualTo(code)); }
        [TestCase("GetMemberSpecCode")][TestCase("sectionType")][TestCase("GetSectionPropertyName")]
        public void UnknownOrUnwrittenScalarOutputsFail(string op)
        {
            var native = new PropertyPortFixture.Native(); native.Output = (name, value) => name == op ? (object)null : value;
            using (var wrapper = Wrap(native)) Assert.Throws<InvalidOperationException>(() => { if (op == "GetMemberSpecCode") wrapper.Property.GetMemberSpecification(17); else if (op == "sectionType") wrapper.Property.GetSectionPropertyValues(37); else wrapper.Property.GetSectionPropertyName(37); });
        }
        [Test]
        public void MaterialAndArrayNoOpsMatchMainBehaviorEvenAfterDisposal()
        {
            var native = new PropertyPortFixture.Native(); var wrapper = Wrap(native); var p = wrapper.Property; wrapper.Dispose();
            p.AssignBeamProperty((int[])null, -1); p.AssignBeamProperty(new int[0], -1);
            p.AssignMaterialToMember(null, -1); p.AssignMaterialToMember(" ", new[] { -1 }); p.AssignMaterialToMember("STEEL", (int[])null);
            p.SetMaterialName(null); p.SetMaterialName(" ");
            Assert.That(p.AssignMemberEndOffsets((int[])null, double.NaN, double.NaN), Is.False);
            Assert.That(p.AssignMemberEndOffset(new int[0], 8, double.NaN), Is.False); Assert.That(native.Calls, Is.Empty);
        }
        [TestCaseSource(nameof(Ports))]
        public void InvalidIdsAndOffsetsFailBeforeAnyNativeWork(MethodInfo method)
        {
            var native = new PropertyPortFixture.Native();
            using (var wrapper = Wrap(native))
            {
                var parameters = method.GetParameters();
                for (int i = 0; i < parameters.Length; i++)
                {
                    var args = Args(method); var type = parameters[i].ParameterType;
                    if (type == typeof(int)) args[i] = 0 == (int)args[i] ? -1 : -2;
                    else if (type == typeof(double)) args[i] = double.NaN;
                    else if (type == typeof(OffsetLocation)) args[i] = (OffsetLocation)9;
                    else if (type == typeof(int[])) args[i] = new[] { 17, 0 };
                    else if (type == typeof(IEnumerable<int>)) args[i] = new[] { 37, 0 };
                    else if (type == typeof(HashSet<Beam>)) args[i] = new HashSet<Beam> { null };
                    else if (type == typeof(HashSet<Plate>)) args[i] = new HashSet<Plate> { new Plate { Id = 0 } };
                    else if (type == typeof(string)) args[i] = "STEEL\0";
                    Assert.That(() => Invoke(wrapper.Property, method, args), Throws.InstanceOf<ArgumentException>(), method + " " + parameters[i].Name);
                    Assert.That(native.Calls, Is.Empty);
                }
            }
        }
        [Test]
        public void ScalarAndBatchAssignmentsPreserveArgumentOrderAndCopyInputIds()
        {
            var native = new PropertyPortFixture.Native(); int[] ids = { 41, 17, 41 };
            using (var wrapper = Wrap(native))
            {
                wrapper.Property.AssignBeamProperty(ids, 37); Assert.That(native.Arguments.Last()[0], Is.EqualTo(ids).And.Not.SameAs(ids));
                wrapper.Property.AssignMaterialToMember(" STEEL Ω ", ids); Assert.That(native.Arguments.Last()[0], Is.EqualTo(" STEEL Ω ")); Assert.That(native.Arguments.Last()[1], Is.EqualTo(ids).And.Not.SameAs(ids));
                wrapper.Property.SetMaterialName(" CONCRETE Ω "); Assert.That(native.Arguments.Last()[0], Is.EqualTo(" CONCRETE Ω "));
                wrapper.Property.AssignMemberEndOffsets(ids, .15, -.20);
                Assert.That(native.Calls.Skip(3), Is.EqualTo(new[] { "CreateMemberOffsetSpec", "CreateMemberOffsetSpec", "AssignMemberSpecToBeam", "AssignMemberSpecToBeam" }));
                Assert.That(native.Arguments[3], Is.EqualTo(new object[] { 0, 0, 0.0, .15, 0.0 }));
                Assert.That(native.Arguments[4], Is.EqualTo(new object[] { 1, 0, 0.0, -.20, 0.0 }));
                Assert.That(native.Arguments[5][0], Is.EqualTo(ids).And.Not.SameAs(ids)); Assert.That(native.Arguments[5][1], Is.EqualTo(71)); Assert.That(native.Arguments[6][1], Is.EqualTo(72));
                Assert.That(ids, Is.EqualTo(new[] { 41, 17, 41 }));
            }
        }
        [TestCase("GetSectionPropertyList", false)][TestCase("GetThicknessPropertyList", false)]
        [TestCase("GetSectionPropertyAssignedBeamList", -6004)][TestCase("GetThicknessPropertyAssignedPlateList", false)]
        [TestCase("GetSectionPropertyName", -6025)][TestCase("GetSectionPropertyValuesEx", 0)]
        [TestCase("GetPlateThickness", -6022)][TestCase("GetMemberSpecCode", false)]
        [TestCase("AssignBeamProperty", -6001)][TestCase("AssignMaterialToMember", false)][TestCase("SetMaterialName", 0)]
        public void NativeFailureStatusesCannotExposeSuccessfulDefaults(string op, object status)
        {
            var native = new PropertyPortFixture.Native(); Response(native, op, status);
            using (var wrapper = Wrap(native)) Assert.Throws<InvalidOperationException>(() =>
            {
                if (op == "GetSectionPropertyName") wrapper.Property.GetSectionPropertyName(37);
                else if (op == "GetMemberSpecCode") wrapper.Property.GetMemberSpecification(17);
                else if (op == "AssignBeamProperty") wrapper.Property.AssignBeamProperty(17, 37);
                else if (op == "AssignMaterialToMember") wrapper.Property.AssignMaterialToMember("STEEL", 17);
                else if (op == "SetMaterialName") wrapper.Property.SetMaterialName("STEEL");
                else QueryOutput(wrapper.Property, op);
            });
        }
        [TestCase(0)][TestCase(1)][TestCase(2)][TestCase(3)]
        public void OffsetFailureStopsRemainingWorkAndDoesNotPretendToRollBack(int position)
        {
            var native = new PropertyPortFixture.Native();
            if (position < 2) Response(native, "CreateMemberOffsetSpec", position == 0 ? new object[] { -6016 } : new object[] { 71, -6016 });
            else Response(native, "AssignMemberSpecToBeam", position == 2 ? new object[] { -6017 } : new object[] { 0, -6017 });
            using (var wrapper = Wrap(native)) Assert.That(wrapper.Property.AssignMemberEndOffsets(17, .1, .2), Is.False);
            Assert.That(native.Calls.Count, Is.EqualTo(position + 1)); Assert.That(native.Calls.Any(c => c.StartsWith("Delete") || c.StartsWith("Save")), Is.False);
        }
        [TestCase(true)][TestCase(false)]
        public void OffsetTransportAndMissingMemberFailuresReturnFalse(bool transport)
        {
            var native = new PropertyPortFixture.Native(); native.Errors["CreateMemberOffsetSpec"] = transport ? (Exception)new COMException("native", unchecked((int)0x80004005)) : new RuntimeBinderException("missing");
            using (var wrapper = Wrap(native)) Assert.That(wrapper.Property.AssignMemberEndOffset(17, OffsetLocation.End, .2), Is.False);
        }
        [Test]
        public void PostCallBindingChecksDetectDisposalAfterNativeMutation()
        {
            var native = new PropertyPortFixture.Native(); var wrapper = Wrap(native); var p = wrapper.Property;
            native.OnCall = name => { if (name == "AssignBeamProperty") wrapper.Dispose(); };
            Assert.Throws<InvalidOperationException>(() => p.AssignBeamProperty(17, 37)); Assert.That(native.Calls, Is.EqualTo(new[] { "AssignBeamProperty" }));
        }
        [Test]
        public void CompatibilityAndTypedQueriesShareOneSerializationGate()
        {
            var native = new PropertyPortFixture.Native();
            using (var entered = new ManualResetEventSlim()) using (var release = new ManualResetEventSlim()) using (var attempted = new ManualResetEventSlim()) using (var wrapper = Wrap(native))
            {
                native.OnCall = name => { if (name == "GetBetaAngle") { entered.Set(); if (!release.Wait(5000)) throw new TimeoutException(); } };
                var first = Task.Run(() => wrapper.Property.GetBetaAngle(17));
                Task<int[]> second = null;
                try
                {
                    Assert.That(entered.Wait(5000), Is.True);
                    second = Task.Run(() => { attempted.Set(); return wrapper.Property.GetThicknessPropertyList(); });
                    Assert.That(attempted.Wait(5000), Is.True); Assert.That(second.Wait(150), Is.False);
                    Assert.That(native.Calls, Is.EqualTo(new[] { "GetBetaAngle" }));
                }
                finally { release.Set(); first.GetAwaiter().GetResult(); second?.GetAwaiter().GetResult(); }
            }
        }
    }
}
