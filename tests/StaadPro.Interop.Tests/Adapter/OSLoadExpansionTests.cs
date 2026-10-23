using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Loads;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Loads.Contracts;
using StaadPro.Interop.Models.Loads.Queries;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSLoadExpansionTests
    {
        public sealed class GeometryNative { public object ZUp = false; public object IsZUp() => ZUp; }
        public sealed class Root
        {
            public object Load { get; set; }
            public int NativeMajor = 26;
            public object Geometry { get; set; } = new GeometryNative();
            public object GetApplicationVersion(ref object major, ref object minor, ref object revision, ref object build)
            { major = NativeMajor; minor = 0; revision = 0; build = 340; return NativeMajor + ".00.00.340"; }
        }
        public sealed class Native
        {
            public readonly List<string> Calls = new List<string>();
            public readonly List<object[]> Arguments = new List<object[]>();
            public readonly List<int> Threads = new List<int>();
            public readonly Dictionary<string, Queue<object>> Responses = new Dictionary<string, Queue<object>>();
            public readonly Dictionary<string, Exception> Errors = new Dictionary<string, Exception>();
            public Action<string> OnCall;
            public Func<string, object, object> ReplaceBuffer;
            public int Active = 17, Kind = 3110, Count = 2, Items = 3, ReferenceIndex;
            public object ActualStart = 101, Generated = 2, CommandDirection = 9, PlateDirection = 0;
            private object R(string name, object fallback, params object[] args)
            {
                Calls.Add(name); Arguments.Add(args); Threads.Add(Thread.CurrentThread.ManagedThreadId); OnCall?.Invoke(name);
                if (Errors.TryGetValue(name, out var error)) throw error;
                return Responses.TryGetValue(name, out var values) && values.Count > 0 ? values.Dequeue() : fallback;
            }
            private void Fill(string role, ref object value, bool ids = false, bool direction = false)
            {
                if (ReplaceBuffer != null) { value = ReplaceBuffer(role, value); return; }
                var a = (Array)value;
                if (a.GetType().GetElementType() == typeof(double)) Assert.That(a.Cast<double>().All(double.IsNaN), Is.True, role);
                for (int i = 0; i < a.Length; i++) a.SetValue(a.GetType().GetElementType() == typeof(double) ? (object)(i - 1.25) : direction ? (i % 9) + 1 : ids ? (i == 0 ? 41 : 17) : 0, i);
            }
            public object SetLoadActive(int id) { Active = id; return R(nameof(SetLoadActive), true, id); }
            public object SetReferenceLoadActive(int id) { Active = id; return R(nameof(SetReferenceLoadActive), id, id); }
            public object GetActiveLoad() => R(nameof(GetActiveLoad), Active);
            public object GetLoadItemsCount(int id) => R(nameof(GetLoadItemsCount), Items, id);
            public object GetLoadItemType(int id, int index) => R(nameof(GetLoadItemType), Kind, id, index);
            public object GetLoadTypeCount(int kind) => R(nameof(GetLoadTypeCount), 2, kind);
            public object GetListSizeForLoadType(int kind, int index) => R(nameof(GetListSizeForLoadType), Count, kind, index);
            public object GetAssignmentListForLoadType(int kind, int index, ref object ids)
            { var code = R(nameof(GetAssignmentListForLoadType), Count, kind, index); Fill("ids", ref ids, true); return code; }
            public object GetNoOfSetsInReferenceLoad(int index) { ReferenceIndex = index; return R(nameof(GetNoOfSetsInReferenceLoad), Count, index); }
            public object GetReferenceLoadByIndex(int index, ref object ids, ref object factors)
            { var code = R(nameof(GetReferenceLoadByIndex), 41, index); Fill("ids", ref ids, true); Fill("factor", ref factors); return code; }
            public object AddLoadCasesToEnvelop(int id, int[] ids) => R(nameof(AddLoadCasesToEnvelop), 1, id, ids);
            public object RemoveLoadCasesFromEnvelop(int id, int[] ids) => R(nameof(RemoveLoadCasesFromEnvelop), 1, id, ids);
            public object DeleteLoadEnvelop(int id) => R(nameof(DeleteLoadEnvelop), 1, id);
            public object SetLoadType(int[] ids, int kind) => R(nameof(SetLoadType), 0, ids, kind);
            public object DeleteLoadList(int index) => R(nameof(DeleteLoadList), 0, index);
            public object SetASDLoadAttribute(int id, int strength, int increase) => R(nameof(SetASDLoadAttribute), 0, id, strength, increase);
            public object SetLSDLoadAttribute(int id) => R(nameof(SetLSDLoadAttribute), 0, id);
            public object RemoveAttribute(int id) => R(nameof(RemoveAttribute), 0, id);
            public object GetAttribute(int id) => R(nameof(GetAttribute), 0, id);
            public object DeletePrimaryLoadCases(int[] ids, bool reference) => R(nameof(DeletePrimaryLoadCases), 1, ids, reference);
            public object DeleteReferenceLoadCases(int[] ids) => R(nameof(DeleteReferenceLoadCases), 1, (object)ids);
            public object AddAutoLoadCombinations(string code, string category, object ids, ref object start)
            { var result = R(nameof(AddAutoLoadCombinations), 0, code, category, ids, start); start = ActualStart; return result; }
            public object AddAutoCombinationRepeat(string code, string category, object ids, ref object start, ref object count, bool reference, bool notional,
                double factor, bool gb, int floors, bool x, bool nx, bool z, bool nz)
            { var result = R(nameof(AddAutoCombinationRepeat), 0, code, category, ids, start, count, reference, notional, factor, gb, floors, x, nx, z, nz); start = ActualStart; count = Generated; return result; }
            public object AddSelfWeightInXYZ(int direction, double factor) => R(nameof(AddSelfWeightInXYZ), true, direction, factor);
            public object AddMemberAreaLoad(int[] ids, double pressure) => R(nameof(AddMemberAreaLoad), 0, ids, pressure);
            public object AddMemberFixedEnd(int[] ids, double[] start, double[] end) => R(nameof(AddMemberFixedEnd), 0, ids, start, end);
            public object AddElementHydrostaticPressure(int[] ids, int direction, int interpolation, double min, double max) => R(nameof(AddElementHydrostaticPressure), true, ids, direction, interpolation, min, max);
            public object AddElementTrapPressureEx(int[] ids, int direction, int variation, double first, double second, double third, double fourth) => R(nameof(AddElementTrapPressureEx), true, ids, direction, variation, first, second, third, fourth);
            public object AddMemberFloorLoadEx(int range, int direction, double pressure, string mode, double ymin, double ymax, double zmin, double zmax, double xmin, double xmax) => R(nameof(AddMemberFloorLoadEx), 1, range, direction, pressure, mode, ymin, ymax, zmin, zmax, xmin, xmax);
            public object AddStrainLoad(int[] ids, double elongation) => R(nameof(AddStrainLoad), 0, ids, elongation);
            public object AddSeismicLoad(int axis, double factor) => R(nameof(AddSeismicLoad), 0, axis, factor);
            public object AddWindLoad(int id, int axis, double factor, bool open, double ymin, double ymax, double zmin, double zmax, double xmin, double xmax) => R(nameof(AddWindLoad), 0, id, axis, factor, open, ymin, ymax, zmin, zmax, xmin, xmax);
            public object AddNotionalLoad(int[] primary, double[] pf, int[] pd, int[] reference, double[] rf, int[] rd) => R(nameof(AddNotionalLoad), 0, primary, pf, pd, reference, rf, rd);
            public object GetNodalLoadInfo(int index, ref object force)
            { var code = R(nameof(GetNodalLoadInfo), true, index); Fill("force", ref force); return code; }
            public object GetMemberLoadInfo(int index, ref object direction, ref object force, ref object distance)
            { var code = R(nameof(GetMemberLoadInfo), true, index); direction = CommandDirection; Fill("force", ref force); Fill("distance", ref distance); return code; }
            public object GetElementLoadInfo(int index, ref object direction, ref object force, ref object distance)
            { var code = R(nameof(GetElementLoadInfo), 1, index); direction = PlateDirection; Fill("force", ref force); Fill("distance", ref distance); return code; }
            public object GetElementPressureLoadCount(int id) => R(nameof(GetElementPressureLoadCount), Count, id);
            public object GetElementConcLoadCount(int id) => R(nameof(GetElementConcLoadCount), Count, id);
            public object GetElementPressureLoads(int id, ref object direction, ref object w, ref object x1, ref object y1, ref object x2, ref object y2)
            { var code = R(nameof(GetElementPressureLoads), 0, id); Fill("direction", ref direction, direction: true); Fill("pressure", ref w); Fill("x1", ref x1); Fill("y1", ref y1); Fill("x2", ref x2); Fill("y2", ref y2); return code; }
            public object GetElementConcLoads(int id, ref object direction, ref object w, ref object x, ref object y)
            { var code = R(nameof(GetElementConcLoads), 0, id); Fill("direction", ref direction, direction: true); Fill("magnitude", ref w); Fill("x1", ref x); Fill("y1", ref y); return code; }
            public object GetBeamCountAtFloor(float xmin, float xmax, float ymin, float ymax, float zmin, float zmax, int axis) => R(nameof(GetBeamCountAtFloor), Count, xmin, xmax, ymin, ymax, zmin, zmax, axis);
            public object GetInfluenceArea(float xmin, float xmax, float ymin, float ymax, float zmin, float zmax, int axis, ref object ids, ref object areas)
            { var code = R(nameof(GetInfluenceArea), true, xmin, xmax, ymin, ymax, zmin, zmax, axis); Fill("ids", ref ids, true); Fill("area", ref areas); if (ReplaceBuffer == null) { var a = (double[])areas; for (int i=0;i<a.Length;i++) a[i] = i + 0.5; } return code; }
            public object SplitLoadsOnBeam(int oldId, int newId) => R(nameof(SplitLoadsOnBeam), true, oldId, newId);
            public void BeginLoadMerging() => R(nameof(BeginLoadMerging), null);
            public object MergeLoadsOnBeam(int keep, int source) => R(nameof(MergeLoadsOnBeam), 17, keep, source);
            public void EndLoadMerging() => R(nameof(EndLoadMerging), null);
        }
        private static JObject Index => JObject.Parse(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "ContractReview", "LoadExpansionContractIndex.json")));
        public static IEnumerable<string> Names => Index["pendingSignatures"].Select(x => (string)x["name"]);
        private static MethodInfo Method(string name) => typeof(IOSLoad).GetMethod(name);
        private static OpenStaadWrapper Wrap(Native native) => new OpenStaadWrapper(new Root { Load = native });
        private static FloorRegion Region() => new FloorRegion(new GlobalLoadRegion(1, 2, 3, 4, 5, 6), GlobalAxis.Y);
        private static AutomaticCombinationRequest Generation() => new AutomaticCombinationRequest(" code ", " category ", LoadCaseSelection.SpecifiedCases(new[] { 41, 17, 41 }), 100);
        private static object[] Args(string name) => Method(name).GetParameters().Select(p =>
        {
            if (p.ParameterType == typeof(ILoadCase)) return (object)new LoadCase(17);
            if (p.ParameterType == typeof(IEnumerable<int>)) return new[] { 41, 17, 41 };
            if (p.ParameterType == typeof(AutomaticCombinationRequest)) return Generation();
            if (p.ParameterType == typeof(AutomaticRepeatLoadRequest)) return new AutomaticRepeatLoadRequest(Generation());
            if (p.ParameterType == typeof(FixedEndLoadDefinition)) return new FixedEndLoadDefinition(new Forces(1,2,3,4,5,6), new Forces(7,8,9,10,11,12));
            if (p.ParameterType == typeof(HydrostaticPressureDefinition)) return new HydrostaticPressureDefinition(LoadDirection.GlobalY, GlobalAxis.Z, -2, -5);
            if (p.ParameterType == typeof(TrapezoidalPressureDefinition)) return TrapezoidalPressureDefinition.AtCorners(LoadDirection.LocalZ, 1, 2, 3, 4);
            if (p.ParameterType == typeof(FloorLoadDefinition)) return FloorLoadDefinition.ForRange(-2, GlobalAxis.Z, Region());
            if (p.ParameterType == typeof(FloorRegion)) return Region();
            if (p.ParameterType == typeof(WindLoadDefinition)) return new WindLoadDefinition(3, WindLoadDirection.NegativeZ, -2, true, Region().Bounds);
            if (p.ParameterType == typeof(IEnumerable<NotionalLoadContribution>)) return new[] { new NotionalLoadContribution(41, LoadCaseType.ReferenceLoad, -2, NotionalLoadDirection.GlobalZ), new NotionalLoadContribution(17, LoadCaseType.PrimaryLoad, 0, NotionalLoadDirection.GlobalY) };
            if (p.ParameterType == typeof(IEnumerable<BeamLoadMergeRequest>)) return new[] { new BeamLoadMergeRequest(17, 41), new BeamLoadMergeRequest(17, 42) };
            if (p.ParameterType == typeof(LoadCommandKind)) return LoadCommandKind.Nodal;
            if (p.ParameterType == typeof(LoadCaseType)) return LoadCaseType.PrimaryLoad;
            if (p.ParameterType == typeof(LoadType)) return LoadType.Wind;
            if (p.ParameterType == typeof(LoadStrengthType)) return LoadStrengthType.StrengthWithPDelta;
            if (p.ParameterType == typeof(SelfWeightDirection)) return SelfWeightDirection.GlobalZ;
            if (p.ParameterType == typeof(GlobalAxis)) return GlobalAxis.Z;
            if (p.ParameterType == typeof(double)) return -2.5;
            if (p.ParameterType == typeof(bool)) return true;
            return p.Name == "maximumResults" ? 100000 : p.Name.EndsWith("Index") ? 0 : p.Name == "newBeamId" ? 41 : 17;
        }).ToArray();
        private static object Invoke(IOSLoad load, string name, object[] args = null)
        {
            try { return Method(name).Invoke(load, args ?? Args(name)); }
            catch (TargetInvocationException error) { ExceptionDispatchInfo.Capture(error.InnerException).Throw(); throw; }
        }
        private static object Prop(object value, string name) => value.GetType().GetProperty(name).GetValue(value);
        private static Native For(string name)
        { return new Native { Kind = name == "GetMemberLoadCommand" ? 3260 : name == "GetPlateLoadCommand" ? 3310 : 3110 }; }
        [TestCaseSource(nameof(Names))]
        public void EveryApprovedMethodDispatchesOnTheCallingThreadAndHasAMatchingConcreteSignature(string name)
        {
            var native = For(name);
            using (var wrapper = Wrap(native))
            {
                var args = Args(name); if (name == "DeleteLoadList") args[0] = 1;
                var result = Invoke(wrapper.Load, name, args);
                Assert.That(Prop(result, "IsSuccessful"), Is.EqualTo(name != "MergeBeamLoads"));
                var entry = Index["pendingSignatures"].Single(e => (string)e["name"] == name);
                foreach (string target in entry["nativeNames"].Values<string>())
                    if (!(name == "DeleteLoadCases" && target == "DeleteReferenceLoadCases") && target != "AddMemberFloorLoad") Assert.That(native.Calls, Does.Contain(target), name);
                Assert.That(native.Threads.All(t => t == Thread.CurrentThread.ManagedThreadId), Is.True);
                var method = Method(name); var concrete = typeof(OSLoadAdapter).GetMethod(name, method.GetParameters().Select(p => p.ParameterType).ToArray());
                Assert.That(concrete.ReturnType, Is.EqualTo(method.ReturnType));
                Assert.That(concrete.GetParameters().Select(p => p.Name), Is.EqualTo(method.GetParameters().Select(p => p.Name)));
            }
        }
        [TestCaseSource(nameof(Names))]
        public void EveryApprovedMethodRejectsStaleBindingsWithoutNativeDispatch(string name)
        {
            var native = For(name); var wrapper = Wrap(native); var load = wrapper.Load; wrapper.Dispose(); native.Calls.Clear();
            var args = Args(name); if (name == "DeleteLoadList") args[0] = 1;
            Assert.Throws<InvalidOperationException>(() => Invoke(load, name, args)); Assert.That(native.Calls, Is.Empty);
        }
        [TestCaseSource(nameof(Names))]
        public void EveryApprovedMethodPropagatesTransportFailure(string name)
        {
            var native = For(name); var error = new COMException("transport", unchecked((int)0x80004005));
            using (var wrapper = Wrap(native))
            {
                native.OnCall = op => { if (op != "EndLoadMerging") throw error; };
                var args = Args(name); if (name == "DeleteLoadList") args[0] = 1;
                Assert.That(Assert.Throws<COMException>(() => Invoke(wrapper.Load, name, args)), Is.SameAs(error));
                if (name == "MergeBeamLoads") Assert.That(native.Calls.Last(), Is.EqualTo("EndLoadMerging"));
            }
        }
        [TestCaseSource(nameof(Names))]
        public void EveryApprovedMethodHasIdenticalExpandedComparativeXmlOnInterfaceAndAdapter(string name)
        {
            var xml = XDocument.Load(Path.ChangeExtension(typeof(IOSLoad).Assembly.Location, ".xml"));
            var docs = xml.Descendants("member").Where(m => ((string)m.Attribute("name")).StartsWith("M:StaadPro.Interop.Adapters.") &&
                ((string)m.Attribute("name")).Contains("." + name + "(")).ToArray();
            Assert.That(docs.Length, Is.EqualTo(2), name);
            Assert.That(docs[0].Elements().Select(e=>e.ToString()), Is.EqualTo(docs[1].Elements().Select(e=>e.ToString())));
            foreach (string tag in new[] { "summary", "returns", "remarks", "example" }) Assert.That(docs[0].Element(tag)?.Value.Trim(), Is.Not.Null.And.Not.Empty);
            Assert.That(docs[0].Element("remarks").Value, Does.Contain("When to use:").And.Contain("No implicit save"));
            Assert.That(docs[0].Elements("param").Select(p=>(string)p.Attribute("name")), Is.EqualTo(Method(name).GetParameters().Select(p=>p.Name)));
            Assert.That(docs[0].Element("example").Value, Does.Contain("if (submitLoadChanges)"));
            Assert.That(docs[0].Value, Does.Not.Contain("Review state:").And.Not.Contain("WITHHELD:"));
        }

        public static IEnumerable<TestCaseData> InvalidInputs
        {
            get
            {
                foreach (string name in Names)
                {
                    var parameters = Method(name).GetParameters();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var type = parameters[i].ParameterType;
                        if (type == typeof(int) || type == typeof(double) || type.IsEnum || !type.IsValueType)
                            yield return new TestCaseData(name, i, "invalid").SetName(name + "_RejectedInvalid_" + parameters[i].Name);
                        if (type == typeof(IEnumerable<int>) || type == typeof(IEnumerable<BeamLoadMergeRequest>) || type == typeof(IEnumerable<NotionalLoadContribution>))
                            yield return new TestCaseData(name, i, "empty").SetName(name + "_RejectedEmpty_" + parameters[i].Name);
                    }
                }
            }
        }
        [TestCaseSource(nameof(InvalidInputs))]
        public void InvalidInputsAreRejectedBeforeAnyNativeDispatch(string name, int index, string mode)
        {
            var native = For(name);
            using (var wrapper = Wrap(native))
            {
                var args = Args(name); if (name == "DeleteLoadList") args[0] = 1;
                var type = Method(name).GetParameters()[index].ParameterType;
                args[index] = mode == "empty" ? Array.CreateInstance(type.GetGenericArguments()[0], 0) : type == typeof(int) ? (object)(-1) : type == typeof(double) ? double.NaN : type.IsEnum ? Enum.ToObject(type, 999) : null;
                Assert.That(() => Invoke(wrapper.Load, name, args), Throws.InstanceOf<ArgumentException>());
                Assert.That(native.Calls, Is.Empty);
            }
        }
        public static IEnumerable<string> Mutations => Names.Where(n => Method(n).ReturnType == typeof(LoadOperationResult) || n.StartsWith("CreateAutomatic"));
        [TestCaseSource(nameof(Mutations))]
        public void MutationFailureIsNotConfusedWithAnotherOperationsSuccessConvention(string name)
        {
            if (name == "MergeBeamLoads") return; // Uninterpreted response; separately tested below.
            var native = For(name);
            using (var wrapper = Wrap(native))
            {
                string op = name == "AddFloorLoad" ? "AddMemberFloorLoadEx" : (string)Index["pendingSignatures"].Single(e=>(string)e["name"]==name)["nativeNames"][0];
                bool boolean = new[] { "SplitLoadsOnBeam", "AddSelfWeight", "AddHydrostaticPressure", "AddTrapezoidalPressure" }.Contains(name);
                bool one = new[] { "SplitLoadsOnBeam", "AddCasesToEnvelope", "RemoveCasesFromEnvelope", "DeleteEnvelope", "DeleteLoadCases", "AddSelfWeight", "AddHydrostaticPressure", "AddTrapezoidalPressure", "AddFloorLoad" }.Contains(name);
                native.Responses[op] = new Queue<object>(new[] { boolean ? (object)false : one ? 0 : -1 });
                var args = Args(name); if (name == "DeleteLoadList") args[0] = 1;
                var result = (LoadOperationResult)Invoke(wrapper.Load, name, args);
                Assert.That(result.Status, Is.EqualTo(LoadOperationStatus.NativeFailure)); Assert.That(result.IsSuccessful, Is.False);
                Assert.That(result.MayHaveChangedModel, Is.True);
                var step = result.Steps.Single(s=>s.NativeFunction == op);
                Assert.That(step.NativeBooleanReturn, Is.EqualTo(boolean ? (bool?)false : null));
                Assert.That(step.NativeReturnCode, Is.EqualTo(boolean ? (int?)null : one ? 0 : -1));
                if (name.StartsWith("CreateAutomatic")) Assert.Throws<InvalidOperationException>(() => { var data = ((LoadOperationResult<AutomaticLoadGenerationData>)result).Data; });
            }
        }
        public static IEnumerable<string> Targeted => Names.Where(n=>Method(n).GetParameters().Any(p=>p.ParameterType==typeof(ILoadCase)));
        [TestCaseSource(nameof(Targeted))]
        public void ExplicitReferenceFamilyActivatesTheReferenceMethodOnly(string name)
        {
            var native = For(name);
            using (var wrapper = Wrap(native))
            {
                var args = Args(name); args[0] = new LoadCase(17, "reference", LoadCaseType.ReferenceLoad);
                var result = Invoke(wrapper.Load, name, args);
                Assert.That(Prop(result,"IsSuccessful"), Is.True);
                Assert.That(native.Calls.First(), Is.EqualTo("SetReferenceLoadActive")); Assert.That(native.Calls, Does.Not.Contain("SetLoadActive"));
            }
        }
        [TestCaseSource(nameof(Targeted))]
        public void ActivationFailureStopsTargetedDispatch(string name)
        {
            var native = For(name); native.Responses["SetLoadActive"] = new Queue<object>(new object[] { false });
            using (var wrapper = Wrap(native))
            {
                var result = Invoke(wrapper.Load,name); Assert.That(Prop(result,"IsSuccessful"), Is.False);
                Assert.That(Prop(result,"Status").ToString(), Is.EqualTo("ActivationFailed"));
                Assert.That(native.Calls, Is.EqualTo(new[] { "SetLoadActive" }));
                if (result is LoadOperationResult mutation) Assert.That(mutation.MayHaveChangedModel, Is.False);
            }
        }
        [TestCaseSource(nameof(Targeted))]
        public void ObservedCaseSwitchDiscardsReadbackOrReportsPossibleMutation(string name)
        {
            var native = For(name);
            string op = name == "AddFloorLoad" ? "AddMemberFloorLoadEx" : (string)Index["pendingSignatures"].Single(e=>(string)e["name"]==name)["nativeNames"][0];
            native.OnCall = call => { if (call == op) native.Active = 99; };
            using (var wrapper = Wrap(native))
            {
                var result = Invoke(wrapper.Load,name); Assert.That(Prop(result,"IsSuccessful"), Is.False);
                Assert.That(Prop(result,"Status").ToString(), Is.EqualTo(result is LoadOperationResult ? "PartialCompletion" : "StateChanged"));
                if (result is LoadOperationResult mutation) Assert.That(mutation.MayHaveChangedModel, Is.True);
            }
        }
        [Test]
        public void CaseAndEnvelopeMutationsRetainOrderDuplicatesAndExactFamilyRoutes()
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                int enumerations = 0; IEnumerable<int> Ids() { enumerations++; yield return 41; yield return 17; yield return 41; }
                wrapper.Load.AddCasesToEnvelope(5,Ids()); Assert.That(enumerations,Is.EqualTo(1));
                Assert.That(native.Arguments.Single()[1],Is.EqualTo(new[] {41,17,41}));
                wrapper.Load.DeleteLoadCases(LoadCaseType.ReferenceLoad,new[]{17});
                Assert.That(native.Calls.Last(),Is.EqualTo("DeleteReferenceLoadCases"));
                wrapper.Load.DeleteLoadCases(LoadCaseType.PrimaryLoad,new[]{17});
                Assert.That(native.Calls.Last(),Is.EqualTo("DeletePrimaryLoadCases")); Assert.That(native.Arguments.Last()[1],Is.EqualTo(false));
                Assert.That(native.Calls.Any(n=>n.StartsWith("RemoveLoadCase")),Is.False);
                Assert.Throws<ArgumentOutOfRangeException>(()=>wrapper.Load.DeleteLoadCases(LoadCaseType.LoadCombination,new[]{17}));
            }
        }
        [Test]
        public void AttributeGetIsStatusOnlyAndAsdUsesIntegerIncreaseAsRequiredByClrMetadata()
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                wrapper.Load.SetAllowableStressAttribute(17,LoadStrengthType.StrengthWithPDelta,true);
                Assert.That(native.Arguments.Single(),Is.EqualTo(new object[]{17,4,1}));
                var result=wrapper.Load.GetLoadAttribute(17); Assert.That(result.Data.NativeValue,Is.Zero);
                Assert.That(result.Diagnostic,Does.Contain("no attribute payload"));
                native.Responses["GetAttribute"]=new Queue<object>(new object[]{-1,4});
                Assert.That(wrapper.Load.GetLoadAttribute(17).Status,Is.EqualTo(LoadQueryStatus.NativeFailure));
                Assert.Throws<InvalidOperationException>(()=>wrapper.Load.GetLoadAttribute(17));
            }
        }
        [Test]
        public void AutomaticGenerationRetainsActualOutputsAndExplicitAllSelection()
        {
            var native=new Native();
            using(var wrapper=Wrap(native))
            {
                var result=wrapper.Load.CreateAutomaticCombinations(Generation());
                Assert.That(result.Data.PreferredStartCaseId,Is.EqualTo(100)); Assert.That(result.Data.ActualStartCaseId,Is.EqualTo(101)); Assert.That(result.Data.GeneratedCount,Is.Null);
                Assert.That(native.Arguments.Last()[2],Is.EqualTo(new[]{41,17,41}));
                var all=new AutomaticCombinationRequest("code","category",LoadCaseSelection.AllCases(),100);
                var repeat=wrapper.Load.CreateAutomaticRepeatLoads(new AutomaticRepeatLoadRequest(all,true,new AutomaticNotionalOptions(AutomaticNotionalDirections.PositiveX|AutomaticNotionalDirections.NegativeZ,-0.002,true,4)));
                Assert.That(repeat.Data.GeneratedCount,Is.EqualTo(2));
                var args=native.Arguments.Last(); Assert.That(args[2],Is.Empty); Assert.That(args.Skip(5),Is.EqualTo(new object[]{true,true,-0.002,true,4,true,false,false,true}));
                wrapper.Load.CreateAutomaticRepeatLoads(new AutomaticRepeatLoadRequest(all));
                Assert.That(native.Arguments.Last().Skip(5),Is.EqualTo(new object[]{false,false,0.0,false,0,false,false,false,false}));
            }
        }
        [TestCase(null)] [TestCase(0)] [TestCase("101")] [TestCase(101.0)] [TestCase(long.MaxValue)]
        public void AutomaticInvalidActualStartIsRejectedAfterNativeInvocation(object output)
        {
            var native=new Native{ActualStart=output}; using(var wrapper=Wrap(native)) Assert.Throws<InvalidOperationException>(()=>wrapper.Load.CreateAutomaticCombinations(Generation()));
        }
        [TestCase(-1)] [TestCase(null)] [TestCase(2.0)]
        public void AutomaticRepeatRejectsUnwrittenOrMalformedCount(object count)
        {
            var native=new Native{Generated=count}; using(var wrapper=Wrap(native)) Assert.Throws<InvalidOperationException>(()=>wrapper.Load.CreateAutomaticRepeatLoads(new AutomaticRepeatLoadRequest(Generation())));
        }
        [Test]
        public void DirectionsUnitsBoundsAndSixComponentEndPacketsMapExactly()
        {
            var native=new Native();using(var wrapper=Wrap(native))
            {
                wrapper.Load.AddSelfWeight(new LoadCase(17),SelfWeightDirection.GlobalZ,-2);
                Assert.That(native.Arguments[native.Calls.IndexOf("AddSelfWeightInXYZ")],Is.EqualTo(new object[]{3,-2.0}));
                wrapper.Load.AddSeismicLoad(new LoadCase(17),GlobalAxis.Z,-2);
                Assert.That(native.Arguments[native.Calls.IndexOf("AddSeismicLoad")],Is.EqualTo(new object[]{2,-2.0}));
                wrapper.Load.AddHydrostaticPressure(new LoadCase(17),new[]{4},new HydrostaticPressureDefinition(LoadDirection.GlobalY,GlobalAxis.Z,-2,-5));
                Assert.That(native.Arguments[native.Calls.IndexOf("AddElementHydrostaticPressure")].Skip(1),Is.EqualTo(new object[]{5,3,-2.0,-5.0}));
                wrapper.Load.AddMemberFixedEndLoad(new LoadCase(17),new[]{4},new FixedEndLoadDefinition(new Forces(1,2,3,4,5,6),new Forces(7,8,9,10,11,12)));
                var args=native.Arguments[native.Calls.IndexOf("AddMemberFixedEnd")]; Assert.That(args[1],Is.EqualTo(new[]{1d,2,3,4,5,6})); Assert.That(args[2],Is.EqualTo(new[]{7d,8,9,10,11,12}));
                wrapper.Load.AddStrainLoad(new LoadCase(17),new[]{4},-0.001);
                Assert.That(native.Arguments[native.Calls.IndexOf("AddStrainLoad")][1],Is.EqualTo(-0.001));
            }
        }
        [Test]
        public void FloorModesAndWindBoundsRetainTheirIndependentAxesAndMarkers()
        {
            var native=new Native();using(var wrapper=Wrap(native))
            {
                foreach(bool oneWay in new[]{false,true})
                {
                    wrapper.Load.AddFloorLoad(new LoadCase(17),FloorLoadDefinition.ForRange(-2,GlobalAxis.Z,Region(),oneWay));
                    var args=native.Arguments[native.Calls.LastIndexOf("AddMemberFloorLoadEx")];
                    Assert.That(args,Is.EqualTo(new object[]{1,2,-2.0,oneWay?"0":" ",3.0,4.0,5.0,6.0,1.0,2.0}));
                }
                wrapper.Load.AddFloorLoad(new LoadCase(17),FloorLoadDefinition.ForGroup(-2,GlobalAxis.Y,"FLOOR_A"));
                Assert.That(native.Arguments[native.Calls.LastIndexOf("AddMemberFloorLoadEx")],Is.EqualTo(new object[]{3,1,-2.0,"FLOOR_A",0.0,0.0,0.0,0.0,0.0,0.0}));
                wrapper.Load.AddWindLoad(new LoadCase(17),new WindLoadDefinition(3,WindLoadDirection.NegativeZ,-2,true,Region().Bounds));
                Assert.That(native.Arguments[native.Calls.IndexOf("AddWindLoad")],Is.EqualTo(new object[]{3,6,-2.0,true,3.0,4.0,5.0,6.0,1.0,2.0}));
            }
        }
        [Test]
        public void WindRejectsZUpWithoutActivationOrAssignment()
        {
            var native=new Native(); using(var wrapper=new OpenStaadWrapper(new Root{Load=native,Geometry=new GeometryNative{ZUp=true}}))
            {
                var result=wrapper.Load.AddWindLoad(new LoadCase(17),new WindLoadDefinition(3,WindLoadDirection.PositiveX,1,false,Region().Bounds));
                Assert.That(result.Status,Is.EqualTo(LoadOperationStatus.Unsupported));Assert.That(result.MayHaveChangedModel,Is.False);Assert.That(native.Calls,Is.Empty);
            }
        }
        [Test]
        public void NotionalBuffersUsePositiveIdsInBothFamiliesAndPreserveDuplicates()
        {
            var native=new Native();using(var wrapper=Wrap(native))
            {
                int enumerations=0; IEnumerable<NotionalLoadContribution> Values()
                {enumerations++;yield return new NotionalLoadContribution(41,LoadCaseType.ReferenceLoad,-2,NotionalLoadDirection.GlobalZ);yield return new NotionalLoadContribution(17,LoadCaseType.PrimaryLoad,0,NotionalLoadDirection.GlobalX);yield return new NotionalLoadContribution(41,LoadCaseType.ReferenceLoad,3,NotionalLoadDirection.GlobalY);}
                wrapper.Load.AddNotionalLoad(new LoadCase(17),Values());Assert.That(enumerations,Is.EqualTo(1));
                var args=native.Arguments[native.Calls.IndexOf("AddNotionalLoad")];
                Assert.That(args[0],Is.EqualTo(new[]{17}));Assert.That(args[1],Is.EqualTo(new[]{0d}));Assert.That(args[2],Is.EqualTo(new[]{1}));
                Assert.That(args[3],Is.EqualTo(new[]{41,41}));Assert.That(args[4],Is.EqualTo(new[]{-2d,3}));Assert.That(args[5],Is.EqualTo(new[]{3,2}));
            }
        }
        public static readonly string[] Collections={"GetAssignedEntityIds","GetPlatePressureLoads","GetPlateConcentratedLoads","GetFloorInfluenceAreas","GetReferenceContributions"};
        [TestCaseSource(nameof(Collections))]
        public void CollectionLimitsStopBeforeFetchingAndDataRemainsGuarded(string name)
        {
            var native=For(name);using(var wrapper=Wrap(native))
            {
                var args=Args(name);args[args.Length-1]=1;
                var result=Invoke(wrapper.Load,name,args);Assert.That(Prop(result,"Status"),Is.EqualTo(LoadQueryStatus.LimitExceeded));
                string op=(string)Index["pendingSignatures"].Single(e=>(string)e["name"]==name)["nativeNames"][0];Assert.That(native.Calls,Does.Not.Contain(op));
                Assert.That(()=>Prop(result,"Data"),Throws.TypeOf<TargetInvocationException>().With.InnerException.TypeOf<InvalidOperationException>());
            }
        }
        [TestCaseSource(nameof(Collections))]
        public void CollectionsRejectCountChangesDuringRetrieval(string name)
        {
            var native=For(name);string countOp=name=="GetAssignedEntityIds"?"GetListSizeForLoadType":name=="GetPlatePressureLoads"?"GetElementPressureLoadCount":name=="GetPlateConcentratedLoads"?"GetElementConcLoadCount":name=="GetFloorInfluenceAreas"?"GetBeamCountAtFloor":"GetNoOfSetsInReferenceLoad";
            native.Responses[countOp]=new Queue<object>(new object[]{2,3});
            using(var wrapper=Wrap(native)){var result=Invoke(wrapper.Load,name);Assert.That(Prop(result,"Status"),Is.EqualTo(LoadQueryStatus.StateChanged));}
        }
        [TestCase("GetPlatePressureLoads")] [TestCase("GetPlateConcentratedLoads")] [TestCase("GetFloorInfluenceAreas")] [TestCase("GetReferenceContributions")]
        public void EmptyCollectionsAreOwnedAndDoNotFetch(string name)
        {
            var native=For(name);native.Count=0;using(var wrapper=Wrap(native))
            {var result=Invoke(wrapper.Load,name);Assert.That(Prop(result,"IsSuccessful"),Is.True);Assert.That((IEnumerable)Prop(result,"Data"),Is.Empty);string op=(string)Index["pendingSignatures"].Single(e=>(string)e["name"]==name)["nativeNames"][0];Assert.That(native.Calls,Does.Not.Contain(op));}
        }
        [Test]
        public void ReferenceScalarIsNotAssumedToBeACountAndNativeIndexIsPassedUnchanged()
        {
            var native=new Native();using(var wrapper=Wrap(native))
            {var result=wrapper.Load.GetReferenceContributions(17,0);Assert.That(result.NativeReturnCode,Is.EqualTo(41));Assert.That(result.Data.Select(x=>x.LoadCaseId),Is.EqualTo(new[]{41,17}));Assert.That(native.ReferenceIndex,Is.Zero);Assert.That(result.Data.Select(x=>x.Factor),Is.EqualTo(new[]{-1.25,-0.25}));}
        }
        [Test]
        public void CommandKindPreflightPreventsUnsupportedReaderDispatchAndPlateDirectionsAreRebased()
        {
            var native=new Native{Kind=3310};using(var wrapper=Wrap(native))
            {
                Assert.That(wrapper.Load.GetNodalLoadCommand(new LoadCase(17),0).Status,Is.EqualTo(LoadQueryStatus.Unsupported));Assert.That(native.Calls,Does.Not.Contain("GetNodalLoadInfo"));
                var pressure=wrapper.Load.GetPlateLoadCommand(new LoadCase(17),0);Assert.That(pressure.Data.Direction,Is.EqualTo(LoadDirection.LocalX));
                native.Responses["GetElementLoadInfo"]=new Queue<object>(new object[]{-1});Assert.That(wrapper.Load.GetPlateLoadCommand(new LoadCase(17),0).Status,Is.EqualTo(LoadQueryStatus.Unsupported));
                native.Kind=3260;Assert.That(wrapper.Load.GetMemberLoadCommand(new LoadCase(17),0).Data.Direction,Is.EqualTo(LoadDirection.ProjectedZ));
            }
        }
        [TestCase(-1)] [TestCase(0)] [TestCase(1)] [TestCase(true)] [TestCase(false)] [TestCase(null)]
        public void MergeRetainsUninterpretedBodyEvidenceAndNeverCertifiesSuccess(object response)
        {
            var native=new Native();native.Responses["MergeLoadsOnBeam"]=new Queue<object>(new[]{response});using(var wrapper=Wrap(native))
            {
                var result=wrapper.Load.MergeBeamLoads(new[]{new BeamLoadMergeRequest(17,41)});Assert.That(result.Status,Is.EqualTo(LoadOperationStatus.InvocationCompleted));Assert.That(result.IsSuccessful,Is.False);Assert.That(result.MayHaveChangedModel,Is.True);
                Assert.That(native.Calls,Is.EqualTo(new[]{"BeginLoadMerging","MergeLoadsOnBeam","EndLoadMerging"}));Assert.That(result.Steps.Count(),Is.EqualTo(3));
            }
        }
        [TestCase("BeginLoadMerging")] [TestCase("MergeLoadsOnBeam")]
        public void MergeAttemptsCleanupOnBeginOrBodyFailureAndPreservesBothFailures(string failing)
        {
            var native=new Native();var body=new COMException("body");var cleanup=new COMException("cleanup");native.Errors[failing]=body;native.Errors["EndLoadMerging"]=cleanup;
            using(var wrapper=Wrap(native))
            {
                var error=Assert.Throws<AggregateException>(()=>wrapper.Load.MergeBeamLoads(new[]{new BeamLoadMergeRequest(17,41)}));Assert.That(error.InnerExceptions,Is.EqualTo(new[]{body,cleanup}));Assert.That(native.Calls.Last(),Is.EqualTo("EndLoadMerging"));Assert.That(error.Data["MayHaveChangedModel"],Is.True);
            }
        }
        [Test]
        public void FloorSizingAndRetrievalUseIdenticalSingleBoundsAndDirection()
        {
            var native=new Native();using(var wrapper=Wrap(native))
            {
                var result=wrapper.Load.GetFloorInfluenceAreas(Region());Assert.That(result.Data.Select(x=>x.Area),Is.EqualTo(new[]{0.5,1.5}));Assert.That(native.Arguments[0],Is.EqualTo(native.Arguments[1]));Assert.That(native.Arguments[0],Is.EqualTo(new object[]{1f,2f,3f,4f,5f,6f,2}));
                var tooLarge=new FloorRegion(new GlobalLoadRegion(0,double.MaxValue,0,0,0,0),GlobalAxis.X);native.Calls.Clear();Assert.Throws<ArgumentOutOfRangeException>(()=>wrapper.Load.GetFloorBeamCount(tooLarge));Assert.That(native.Calls,Is.Empty);
            }
        }

        public static IEnumerable<TestCaseData> MalformedBuffers
        {
            get
            {
                foreach (string name in Collections.Concat(new[]{"GetNodalLoadCommand","GetMemberLoadCommand","GetPlateLoadCommand"}))
                    foreach (string shape in new[]{"null","wrong-type","short","long","rank","unwritten","nonfinite"})
                        yield return new TestCaseData(name,shape);
            }
        }
        private static object ValidBuffer(string role, object value)
        {
            var a=(Array)value; var copy=Array.CreateInstance(a.GetType().GetElementType(),new[]{a.Length},new[]{5});
            for(int i=0;i<a.Length;i++)copy.SetValue(a.GetType().GetElementType()==typeof(double)?(object)(i+0.5):role=="direction"?i%9+1:i+17,i+5);
            return copy;
        }
        [TestCaseSource(nameof(MalformedBuffers))]
        public void EveryReaderRejectsMalformedReplacedOrUnwrittenBuffers(string name,string shape)
        {
            var native=For(name); bool replaced=false;
            native.ReplaceBuffer=(role,value)=>
            {
                if(replaced)return ValidBuffer(role,value);replaced=true;var a=(Array)value;var element=a.GetType().GetElementType();
                if(shape=="null")return null;
                if(shape=="wrong-type")return new object[a.Length];
                if(shape=="short")return Array.CreateInstance(element,a.Length-1);
                if(shape=="long")return Array.CreateInstance(element,a.Length+1);
                if(shape=="rank")return Array.CreateInstance(element,a.Length,1);
                if(shape=="unwritten")return value;
                var valid=(Array)ValidBuffer(role,value);
                if(element==typeof(double))valid.SetValue(double.PositiveInfinity,valid.GetLowerBound(0));
                else valid.SetValue(-1,valid.GetLowerBound(0));
                return valid;
            };
            using(var wrapper=Wrap(native))Assert.Throws<InvalidOperationException>(()=>Invoke(wrapper.Load,name));
        }
        [TestCaseSource(nameof(Collections))]
        public void NonzeroLowerBoundBuffersAreCopiedIntoOwnedReadOnlyResults(string name)
        {
            var native=For(name);var buffers=new List<Array>();
            native.ReplaceBuffer=(role,value)=>{var a=(Array)ValidBuffer(role,value);buffers.Add(a);return a;};
            using(var wrapper=Wrap(native))
            {
                var result=Invoke(wrapper.Load,name);Assert.That(Prop(result,"IsSuccessful"),Is.True);
                var data=(IList)Prop(result,"Data");Assert.That(data.IsReadOnly,Is.True);Assert.That(data.Count,Is.EqualTo(2));
                if(name=="GetAssignedEntityIds"){var first=data[0];buffers[0].SetValue(99,5);Assert.That(data[0],Is.EqualTo(first));}
            }
        }
        [TestCase("GetNodalLoadCommand",false)] [TestCase("GetMemberLoadCommand",false)] [TestCase("GetPlateLoadCommand",0)]
        [TestCase("GetPlatePressureLoads",-1)] [TestCase("GetPlateConcentratedLoads",-1)] [TestCase("GetFloorInfluenceAreas",false)]
        [TestCase("GetReferenceContributions",-1)] [TestCase("GetAssignedEntityIds",0)]
        public void FailedQueriesNeverExposeBufferData(string name,object failure)
        {
            var native=For(name);string op=(string)Index["pendingSignatures"].Single(e=>(string)e["name"]==name)["nativeNames"][0];native.Responses[op]=new Queue<object>(new[]{failure});
            using(var wrapper=Wrap(native))
            {var result=Invoke(wrapper.Load,name);Assert.That(Prop(result,"Status"),Is.EqualTo(LoadQueryStatus.NativeFailure));Assert.That(()=>Prop(result,"Data"),Throws.TypeOf<TargetInvocationException>().With.InnerException.TypeOf<InvalidOperationException>());}
        }
        [TestCase("GetNodalLoadCommand")] [TestCase("GetMemberLoadCommand")] [TestCase("GetPlateLoadCommand")]
        public void CommandChangesInCountOrKindDiscardThePacket(string name)
        {
            var native=For(name);native.Responses["GetLoadItemsCount"]=new Queue<object>(new object[]{3,4});
            using(var wrapper=Wrap(native))Assert.That(Prop(Invoke(wrapper.Load,name),"Status"),Is.EqualTo(LoadQueryStatus.StateChanged));
            native=For(name);native.Responses["GetLoadItemType"]=new Queue<object>(new object[]{native.Kind,9999});
            using(var wrapper=Wrap(native))Assert.That(Prop(Invoke(wrapper.Load,name),"Status"),Is.EqualTo(LoadQueryStatus.StateChanged));
        }
        [TestCase(-1)] [TestCase(-8002)]
        public void ReferenceActivationFailurePreservesNativeEvidenceAndDoesNotAssign(int code)
        {
            var native=new Native();native.Responses["SetReferenceLoadActive"]=new Queue<object>(new object[]{code});
            using(var wrapper=Wrap(native))
            {var result=wrapper.Load.AddSelfWeight(new LoadCase(17,"reference",LoadCaseType.ReferenceLoad),SelfWeightDirection.GlobalY,-1);Assert.That(result.Status,Is.EqualTo(LoadOperationStatus.ActivationFailed));Assert.That(result.Steps.Single().NativeReturnCode,Is.EqualTo(code));Assert.That(native.Calls,Is.EqualTo(new[]{"SetReferenceLoadActive"}));}
        }
        [TestCase("GetMemberLoadCommand")] [TestCase("GetPlateLoadCommand")]
        public void InvalidOrUnwrittenScalarDirectionsAreRejected(string name)
        {
            var native=For(name);native.CommandDirection=int.MinValue;native.PlateDirection=int.MinValue;
            using(var wrapper=Wrap(native))Assert.Throws<InvalidOperationException>(()=>Invoke(wrapper.Load,name));
        }
        [Test]
        public void NodalPacketUsesAllSixComponentsAndReturnedForcesAreDefensiveCopies()
        {
            var native=For("GetNodalLoadCommand");using(var wrapper=Wrap(native))
            {var data=wrapper.Load.GetNodalLoadCommand(new LoadCase(17),0).Data;Assert.That(data.Forces.Fx,Is.EqualTo(-1.25));Assert.That(data.Forces.Mz,Is.EqualTo(3.75));var copy=data.Forces;copy.Fx=99;Assert.That(data.Forces.Fx,Is.EqualTo(-1.25));}
        }
        [TestCase(24, "Unclassified STAAD 2024")]
        [TestCase(26, "code name")]
        [TestCase(99, "unreviewed version")]
        public void NativeVersionSelectsAutomaticErrorMeaningWithoutUsingHostVersion(int major,string expected)
        {
            var native=new Native();native.Responses["AddAutoLoadCombinations"]=new Queue<object>(new object[]{-8040});
            using(var wrapper=new OpenStaadWrapper(new Root{Load=native,NativeMajor=major}))
            {
                var result=wrapper.Load.CreateAutomaticCombinations(Generation());
                Assert.That(result.Diagnostic,Does.Contain(expected));Assert.That(result.Steps.Single().NativeReturnCode,Is.EqualTo(-8040));
            }
        }
    }
}
