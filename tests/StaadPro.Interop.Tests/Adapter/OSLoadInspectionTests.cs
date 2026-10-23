using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Loads;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Loads.Queries;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSLoadInspectionTests
    {
        public sealed class Root
        {
            public object Load { get; set; }
            public object Geometry => null;
        }
        public sealed class Native
        {
            public object Active = 17, Activation = true, Lists = 2, ListSize = 3, ListStatus = true;
            public object Commands = 2, Entities = 3, Items = 2, Kind = 3110, Dynamic = 1;
            public object RepeatSize = 3, RepeatReturned = 3, NotionalSize = 3, NotionalReturned = 3;
            public object NotionalItems = 1, ReferenceItems = 1;
            public int[] ListIds = { 41, 17, 41 }, RepeatIds = { 41, 17, 41 }, NotionalIds = { 17, -41, 17 }, Directions = { 1, 2, 3 };
            public double[] Factors = { -1.25, 0.0, 2.5 };
            public readonly List<string> Calls = new List<string>();
            public readonly List<object[]> Arguments = new List<object[]>();
            public readonly List<int> Threads = new List<int>();
            public readonly Dictionary<string, Queue<object>> Responses = new Dictionary<string, Queue<object>>();
            public Func<string, object, object> ReplaceBuffer;
            public Action<string> OnCall;
            public Exception Failure;
            private object Read(string name, object value, params object[] args)
            {
                Calls.Add(name); Arguments.Add(args); Threads.Add(Thread.CurrentThread.ManagedThreadId);
                OnCall?.Invoke(name);
                if (Failure != null) throw Failure;
                return Responses.TryGetValue(name, out var queue) && queue.Count > 0 ? queue.Dequeue() : value;
            }
            public object SetLoadActive(int id) { Active = id; return Read(nameof(SetLoadActive), Activation, id); }
            public object GetActiveLoad() => Read(nameof(GetActiveLoad), Active);
            public object GetLoadListCount() => Read(nameof(GetLoadListCount), Lists);
            public object GetLoadCountInLoadList(int index) => Read(nameof(GetLoadCountInLoadList), ListSize, index);
            private void Fill(string role, ref object output, Array values)
            {
                Array buffer = (Array)output;
                Assert.That(buffer.Rank, Is.EqualTo(1));
                Assert.That(buffer.GetLowerBound(0), Is.Zero);
                if (buffer.GetType().GetElementType() == typeof(double))
                    Assert.That(buffer.Cast<double>().All(double.IsNaN), Is.True);
                else Assert.That(buffer.Cast<int>().All(n => n == 0), Is.True);
                if (ReplaceBuffer != null) { output = ReplaceBuffer(role, output); return; }
                for (int i = 0; i < Math.Min(buffer.Length, values.Length); i++) buffer.SetValue(values.GetValue(i), i);
            }
            public object GetLoadsInLoadList(int index, ref object ids)
            { object status = Read(nameof(GetLoadsInLoadList), ListStatus, index); Fill("list", ref ids, ListIds); return status; }
            public object GetLoadTypeCount(int kind) => Read(nameof(GetLoadTypeCount), Commands, kind);
            public object GetListSizeForLoadType(int kind, int index) => Read(nameof(GetListSizeForLoadType), Entities, kind, index);
            public object GetLoadItemsCount(int id) => Read(nameof(GetLoadItemsCount), Items, id);
            public object GetLoadItemType(int id, int index) => Read(nameof(GetLoadItemType), Kind, id, index);
            public object IsDynamicLoadIncluded(int id) => Read(nameof(IsDynamicLoadIncluded), Dynamic, id);
            public object GetNoLoadFactorInRepeatLoad(int index) => Read(nameof(GetNoLoadFactorInRepeatLoad), RepeatSize, index);
            public object GetRepeatLoadByIndex(int index, ref object ids, ref object factors)
            { object count = Read(nameof(GetRepeatLoadByIndex), RepeatReturned, index); Fill("ids", ref ids, RepeatIds); Fill("factors", ref factors, Factors); return count; }
            public object GetNotionalLoadCount() => Read(nameof(GetNotionalLoadCount), NotionalItems);
            public object GetNoLoadFactorDirectionInNotionalLoad(int index) => Read(nameof(GetNoLoadFactorDirectionInNotionalLoad), NotionalSize, index);
            public object GetNotionalLoadByIndex(int index, ref object ids, ref object factors, ref object directions)
            { object count = Read(nameof(GetNotionalLoadByIndex), NotionalReturned, index); Fill("ids", ref ids, NotionalIds); Fill("factors", ref factors, Factors); Fill("directions", ref directions, Directions); return count; }
            public object GetReferenceLoadCount() => Read(nameof(GetReferenceLoadCount), ReferenceItems);
        }
        public static readonly string[] Names = { "GetActiveLoadCaseNumber", "GetLoadListCount", "GetLoadListCaseCount", "GetLoadListCases",
            "GetLoadCommandCount", "GetAssignedEntityCount", "GetLoadItemCount", "GetLoadItemKind", "ContainsDynamicLoad",
            "GetRepeatContributionCount", "GetRepeatContributions", "GetNotionalItemCount", "GetNotionalContributionCount", "GetNotionalContributions", "GetReferenceItemCount" };
        private static readonly string[] NativeNames = { "GetActiveLoad", "GetLoadListCount", "GetLoadCountInLoadList", "GetLoadsInLoadList",
            "GetLoadTypeCount", "GetListSizeForLoadType", "GetLoadItemsCount", "GetLoadItemType", "IsDynamicLoadIncluded",
            "GetNoLoadFactorInRepeatLoad", "GetRepeatLoadByIndex", "GetNotionalLoadCount", "GetNoLoadFactorDirectionInNotionalLoad", "GetNotionalLoadByIndex", "GetReferenceLoadCount" };
        private static OpenStaadWrapper Wrap(Native native) => new OpenStaadWrapper(new Root { Load = native });
        private static MethodInfo Method(string name) => typeof(IOSLoad).GetMethod(name);
        public static object[] Args(string name) => Method(name).GetParameters().Select(p => p.Name == "commandKind" ? (object)LoadCommandKind.Nodal :
            p.Name == "maximumResults" ? 100000 : p.Name.EndsWith("Index") ? (p.Name == "commandIndex" || p.Name == "loadItemIndex" ? 0 : 1) : 17).ToArray();
        private static object Invoke(IOSLoad load, string name, object[] args = null)
        {
            try { return Method(name).Invoke(load, args ?? Args(name)); }
            catch (TargetInvocationException ex) { ExceptionDispatchInfo.Capture(ex.InnerException).Throw(); throw; }
        }
        private static object Property(object result, string name) => result.GetType().GetProperty(name).GetValue(result);
        private static void Outcome(object result, LoadQueryStatus status)
        {
            Assert.That(Property(result, "Status"), Is.EqualTo(status));
            Assert.That(Property(result, "IsSuccessful"), Is.EqualTo(status == LoadQueryStatus.Success));
            if (status != LoadQueryStatus.Success)
            {
                var error = Assert.Throws<TargetInvocationException>(() => Property(result, "Data"));
                Assert.That(error.InnerException, Is.TypeOf<InvalidOperationException>());
            }
        }
        [TestCaseSource(nameof(Names))]
        public void EveryQueryDispatchesItsExactNativeTargetOnTheCallerThread(string name)
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                var result = Invoke(wrapper.Load, name);
                Outcome(result, LoadQueryStatus.Success);
                Assert.That(Property(result, "RequestedNativeFunction"), Is.EqualTo(NativeNames[Array.IndexOf(Names, name)]));
                Assert.That(native.Calls, Does.Contain(NativeNames[Array.IndexOf(Names, name)]));
                Assert.That(native.Threads.All(t => t == Thread.CurrentThread.ManagedThreadId), Is.True);
                bool active = Method(name).GetParameters().Any(p => p.Name == "primaryLoadCaseId");
                Assert.That(native.Calls.Contains("SetLoadActive"), Is.EqualTo(active));
                if (active) Assert.That(native.Calls.First(), Is.EqualTo("SetLoadActive"));
                Assert.That(Method(name).GetParameters().Any(p => p.ParameterType.IsByRef), Is.False);
            }
        }
        private static IEnumerable<TestCaseData> BadInputs
        {
            get
            {
                foreach (string name in Names)
                    foreach (var p in Method(name).GetParameters())
                    {
                        int[] values = p.ParameterType == typeof(LoadCommandKind) ? new[] { -1, 0, 99999 } :
                            p.Name == "commandIndex" || p.Name == "loadItemIndex" ? new[] { -1 } : new[] { -1, 0 };
                        foreach (int value in values) yield return new TestCaseData(name, p.Position, value);
                    }
            }
        }
        [TestCaseSource(nameof(Names))]
        public void ExactNativeArgumentsPreserveIdsKindAndIndexBases(string name)
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                Invoke(wrapper.Load, name);
                string target = NativeNames[Array.IndexOf(Names, name)];
                int call = native.Calls.IndexOf(target);
                object[] expected = target == "GetLoadTypeCount" ? new object[] { 3110 } :
                    target == "GetListSizeForLoadType" ? new object[] { 3110, 0 } :
                    target == "GetLoadItemType" ? new object[] { 17, 0 } :
                    target == "GetLoadItemsCount" || target == "IsDynamicLoadIncluded" ? new object[] { 17 } :
                    target == "GetLoadCountInLoadList" || target == "GetLoadsInLoadList" || target == "GetNoLoadFactorInRepeatLoad" ||
                    target == "GetRepeatLoadByIndex" || target == "GetNoLoadFactorDirectionInNotionalLoad" || target == "GetNotionalLoadByIndex"
                        ? new object[] { 1 } : new object[0];
                Assert.That(native.Arguments[call], Is.EqualTo(expected));
                if (native.Calls.Contains("SetLoadActive")) Assert.That(native.Arguments[0], Is.EqualTo(new object[] { 17 }));
            }
        }
        [TestCase("GetLoadListCount")]
        [TestCase("GetLoadListCaseCount")]
        [TestCase("GetLoadItemCount")]
        [TestCase("GetAssignedEntityCount")]
        [TestCase("GetRepeatContributionCount")]
        [TestCase("GetNotionalItemCount")]
        [TestCase("GetNotionalContributionCount")]
        [TestCase("GetReferenceItemCount")]
        public void DocumentedNonnegativeZeroCountsRemainUsableData(string name)
        {
            var native = new Native { Lists = name == "GetLoadListCount" ? 0 : 2, ListSize = 0, Items = 0, Entities = 0,
                RepeatSize = 0, NotionalSize = 0, NotionalItems = 0, ReferenceItems = 0 };
            using (var wrapper = Wrap(native))
            { var result = Invoke(wrapper.Load, name); Outcome(result, LoadQueryStatus.Success); Assert.That(Property(result, "Data"), Is.EqualTo(0)); }
        }
        [TestCase(false)] [TestCase(0)]
        public void FailedListFetchDoesNotInspectMalformedOutput(object flag)
        {
            var native = new Native { ListStatus = flag, ReplaceBuffer = (role, buffer) => null };
            using (var wrapper = Wrap(native)) Outcome(wrapper.Load.GetLoadListCases(1), LoadQueryStatus.NativeFailure);
        }
        [TestCase(1)] [TestCase(-1)] [TestCase(true)]
        public void SuccessfulListFlagsAcceptDocumentedBooleanRepresentations(object flag)
        {
            using (var wrapper = Wrap(new Native { ListStatus = flag })) Outcome(wrapper.Load.GetLoadListCases(1), LoadQueryStatus.Success);
        }
        [TestCase(2)] [TestCase(-2)] [TestCase("1")] [TestCase(1.0)]
        public void UnsupportedFlagsAndPredicatesAreNotCoerced(object flag)
        {
            using (var wrapper = Wrap(new Native { ListStatus = flag, Dynamic = flag, Activation = flag }))
            {
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.GetLoadListCases(1));
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.ContainsDynamicLoad(17));
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.GetReferenceItemCount(17));
            }
        }
        [TestCaseSource(nameof(Names))]
        public void ReplacementDuringANativeCallCannotReturnACapturedSuccess(string name)
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                var adapter = wrapper.Load;
                native.OnCall = stage => { native.OnCall = null; ((Root)wrapper.RawOpenStaad).Load = new Native(); wrapper.RefreshComObjects(); };
                Assert.Throws<InvalidOperationException>(() => Invoke(adapter, name));
            }
        }
        [TestCaseSource(nameof(BadInputs))]
        public void InvalidRequestsAreRejectedBeforeAnyNativeAccess(string name, int position, int value)
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                var args = Args(name); args[position] = Method(name).GetParameters()[position].ParameterType.IsEnum ? (object)(LoadCommandKind)value : value;
                Assert.Throws<ArgumentOutOfRangeException>(() => Invoke(wrapper.Load, name, args));
                Assert.That(native.Calls, Is.Empty);
            }
        }
        [TestCase("GetRepeatContributions")]
        [TestCase("GetNotionalContributions")]
        [TestCase("GetAssignedEntityCount")]
        public void ActivationFailureStopsBeforeReadingAndPreservesEvidence(string name)
        {
            foreach (object value in new object[] { false, 0, (short)0, 0L })
            {
                var native = new Native { Activation = value };
                using (var wrapper = Wrap(native))
                {
                    var result = Invoke(wrapper.Load, name); Outcome(result, LoadQueryStatus.ActivationFailed);
                    Assert.That(Property(result, "NativeFunction"), Is.EqualTo("SetLoadActive"));
                    Assert.That(Property(result, "NativeReturnCode"), Is.EqualTo(value is bool ? (object)null : 0));
                    Assert.That(native.Calls, Is.EqualTo(new[] { "SetLoadActive" }));
                }
            }
        }
        [TestCase(true)] [TestCase(1)] [TestCase(-1)]
        public void ActivationAcceptsBooleanAndVariantTrueRepresentations(object activation)
        {
            using (var wrapper = Wrap(new Native { Activation = activation }))
                Outcome(wrapper.Load.GetReferenceItemCount(17), LoadQueryStatus.Success);
        }
        [TestCase("GetRepeatContributions")]
        [TestCase("GetNotionalContributions")]
        public void ActivationMismatchBeforeOrAfterReadSuppressesPayload(string name)
        {
            foreach (var responses in new[] { new object[] { 19 }, new object[] { 17, 19 }, new object[] { 17, -1 } })
            {
                var native = new Native(); native.Responses["GetActiveLoad"] = new Queue<object>(responses);
                using (var wrapper = Wrap(native))
                {
                    var result = Invoke(wrapper.Load, name);
                    Outcome(result, responses.Last().Equals(-1) ? LoadQueryStatus.NativeFailure : LoadQueryStatus.StateChanged);
                    Assert.That(Property(result, "NativeFunction"), Is.EqualTo("GetActiveLoad"));
                    if (responses.Length == 1) Assert.That(native.Calls.Count, Is.EqualTo(2));
                }
            }
        }
        [TestCase("GetLoadListCases")]
        [TestCase("GetRepeatContributions")]
        [TestCase("GetNotionalContributions")]
        public void SnapshotsPreserveOrderDuplicatesFactorsFamiliesAndOwnership(string name)
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                object result = Invoke(wrapper.Load, name); Outcome(result, LoadQueryStatus.Success);
                var data = (IList)Property(result, "Data");
                Assert.That(data.IsReadOnly, Is.True);
                if (name == "GetLoadListCases") Assert.That(data.Cast<int>(), Is.EqualTo(new[] { 41, 17, 41 }));
                else if (name == "GetRepeatContributions")
                {
                    Assert.That(data.Cast<LoadFactorContribution>().Select(c => c.LoadCaseId), Is.EqualTo(new[] { 41, 17, 41 }));
                    Assert.That(data.Cast<LoadFactorContribution>().Select(c => c.Factor), Is.EqualTo(new[] { -1.25, 0.0, 2.5 }));
                }
                else
                {
                    var contributions = data.Cast<NotionalLoadContribution>().ToArray();
                    Assert.That(contributions.Select(c => c.LoadCaseId), Is.EqualTo(new[] { 17, 41, 17 }));
                    Assert.That(contributions.Select(c => c.NativeLoadCaseId), Is.EqualTo(new[] { 17, -41, 17 }));
                    Assert.That(contributions.Select(c => c.CaseType), Is.EqualTo(new[] { LoadCaseType.PrimaryLoad, LoadCaseType.ReferenceLoad, LoadCaseType.PrimaryLoad }));
                    Assert.That(contributions.Select(c => c.Direction), Is.EqualTo(new[] { NotionalLoadDirection.GlobalX, NotionalLoadDirection.GlobalY, NotionalLoadDirection.GlobalZ }));
                }
                int calls = native.Calls.Count; data.Cast<object>().ToArray(); data.Cast<object>().ToArray();
                Assert.That(native.Calls.Count, Is.EqualTo(calls));
                native.ListIds[0] = 999; native.RepeatIds[0] = 999; native.NotionalIds[0] = 999; native.Factors[0] = 999;
                Assert.That(name == "GetLoadListCases" ? data[0] : data[0].GetType().GetProperty("LoadCaseId").GetValue(data[0]), Is.EqualTo(name == "GetNotionalContributions" ? 17 : 41));
            }
        }
        [TestCase("GetLoadListCases")]
        [TestCase("GetRepeatContributions")]
        [TestCase("GetNotionalContributions")]
        public void BoundedRetrievalAndZeroCountsAvoidFetching(string name)
        {
            foreach (bool empty in new[] { false, true })
            {
                var native = new Native();
                if (empty) native.ListSize = native.RepeatSize = native.NotionalSize = 0;
                using (var wrapper = Wrap(native))
                {
                    var args = Args(name); args[args.Length - 1] = 2;
                    var result = Invoke(wrapper.Load, name, args);
                    Outcome(result, empty ? LoadQueryStatus.Success : LoadQueryStatus.LimitExceeded);
                    Assert.That(native.Calls, Does.Not.Contain(NativeNames[Array.IndexOf(Names, name)]));
                    if (empty) Assert.That(((IEnumerable)Property(result, "Data")).Cast<object>(), Is.Empty);
                }
            }
        }
        [TestCase("GetLoadListCases", "GetLoadCountInLoadList")]
        [TestCase("GetRepeatContributions", "GetNoLoadFactorInRepeatLoad")]
        [TestCase("GetNotionalContributions", "GetNoLoadFactorDirectionInNotionalLoad")]
        [TestCase("GetAssignedEntityCount", "GetLoadTypeCount")]
        public void ChangedSizingCountsDiscardSuccessfulPayloads(string name, string counter)
        {
            var native = new Native();
            native.Responses[counter] = new Queue<object>(counter == "GetLoadTypeCount" ? new object[] { 2, 3 } : new object[] { 3, 4 });
            using (var wrapper = Wrap(native)) Outcome(Invoke(wrapper.Load, name), LoadQueryStatus.StateChanged);
        }
        [TestCase("GetLoadListCases")]
        [TestCase("GetLoadListCaseCount")]
        public void ListRemovalOrOutOfRangeIndexIsDistinguished(string name)
        {
            using (var wrapper = Wrap(new Native { Lists = 0 })) Outcome(Invoke(wrapper.Load, name), LoadQueryStatus.NotFound);
            var native = new Native(); native.Responses["GetLoadListCount"] = new Queue<object>(new object[] { 2, 1 });
            using (var wrapper = Wrap(native)) Outcome(Invoke(wrapper.Load, name), LoadQueryStatus.StateChanged);
        }
        [TestCase("GetRepeatContributions")]
        [TestCase("GetNotionalContributions")]
        public void ReturnedSizesMustMatchPreflightAndFailureOutputsAreNotDecoded(string name)
        {
            foreach (int value in new[] { 2, 4, -1 })
            {
                var native = new Native { RepeatReturned = value, NotionalReturned = value, ReplaceBuffer = (role, buffer) => null };
                using (var wrapper = Wrap(native)) Outcome(Invoke(wrapper.Load, name), value == -1 ?
                    name == "GetRepeatContributions" ? LoadQueryStatus.NotFound : LoadQueryStatus.NativeFailure : LoadQueryStatus.StateChanged);
            }
        }
        private static IEnumerable<TestCaseData> MalformedArrays
        {
            get
            {
                foreach (string name in new[] { "GetLoadListCases", "GetRepeatContributions", "GetNotionalContributions" })
                    foreach (string shape in new[] { "null", "rank", "length", "type", "unwritten" })
                        yield return new TestCaseData(name, shape);
            }
        }
        [TestCaseSource(nameof(MalformedArrays))]
        public void MalformedAndUnwrittenSuccessfulArraysAreRejected(string name, string shape)
        {
            var native = new Native { ReplaceBuffer = (role, buffer) => shape == "null" ? null : shape == "rank" ? new int[1, 3] :
                shape == "length" ? new int[2] : shape == "type" ? (object)new object[3] : buffer };
            using (var wrapper = Wrap(native)) Assert.Throws<InvalidOperationException>(() => Invoke(wrapper.Load, name));
        }
        [TestCase("GetLoadListCases")]
        [TestCase("GetRepeatContributions")]
        [TestCase("GetNotionalContributions")]
        public void NonzeroArrayLowerBoundsAreSupportedAndCopied(string name)
        {
            var native = new Native();
            native.ReplaceBuffer = (role, buffer) =>
            {
                Type type = ((Array)buffer).GetType().GetElementType();
                var result = Array.CreateInstance(type, new[] { 3 }, new[] { -2 });
                for (int i = 0; i < 3; i++) result.SetValue(type == typeof(double) ? (object)(i - 1.25) : role == "directions" ? i + 1 : i + 17, i - 2);
                return result;
            };
            using (var wrapper = Wrap(native)) Outcome(Invoke(wrapper.Load, name), LoadQueryStatus.Success);
        }
        [TestCase(0)] [TestCase(int.MinValue)]
        public void InvalidNotionalIdsAreRejectedWithoutOverflow(int id)
        {
            var native = new Native { NotionalIds = new[] { id, -41, 17 } };
            using (var wrapper = Wrap(native)) Assert.Throws<InvalidOperationException>(() => wrapper.Load.GetNotionalContributions(17, 1));
        }
        [TestCase(0)] [TestCase(4)] [TestCase(-1)]
        public void NotionalAxesAreNotInterpretedAsLocalLoadDirections(int axis)
        {
            var native = new Native { Directions = new[] { axis, 2, 3 } };
            using (var wrapper = Wrap(native)) Assert.Throws<InvalidOperationException>(() => wrapper.Load.GetNotionalContributions(17, 1));
        }
        [TestCase(double.NaN)] [TestCase(double.PositiveInfinity)] [TestCase(double.NegativeInfinity)]
        public void NonfiniteFactorsAreRejected(double factor)
        {
            var native = new Native { Factors = new[] { factor, 0, 1 } };
            using (var wrapper = Wrap(native)) Assert.Throws<InvalidOperationException>(() => wrapper.Load.GetRepeatContributions(17, 1));
        }
        [TestCase(0, false)] [TestCase(1, true)] [TestCase(false, false)] [TestCase(true, true)]
        public void DynamicFalseIsSuccessfulData(object value, bool expected)
        {
            using (var wrapper = Wrap(new Native { Dynamic = value }))
            { var result = wrapper.Load.ContainsDynamicLoad(17); Outcome(result, LoadQueryStatus.Success); Assert.That(result.Data, Is.EqualTo(expected)); }
        }
        [Test]
        public void OperationSpecificFailureCodesArePreserved()
        {
            using (var wrapper = Wrap(new Native { Active = -1, Lists = -1, Items = -1, Kind = 0, Dynamic = -1, RepeatSize = -1, NotionalSize = -1, NotionalItems = -1, ReferenceItems = -1 }))
            {
                Outcome(wrapper.Load.GetActiveLoadCaseNumber(), LoadQueryStatus.NativeFailure);
                Outcome(wrapper.Load.GetLoadListCount(), LoadQueryStatus.NativeFailure);
                Outcome(wrapper.Load.GetLoadItemCount(17), LoadQueryStatus.NativeFailure);
                Outcome(wrapper.Load.GetLoadItemKind(17, 0), LoadQueryStatus.NotFound);
                Outcome(wrapper.Load.ContainsDynamicLoad(17), LoadQueryStatus.NativeFailure);
                Outcome(wrapper.Load.GetRepeatContributionCount(17, 1), LoadQueryStatus.NotFound);
                Outcome(wrapper.Load.GetRepeatContributions(17, 1), LoadQueryStatus.NotFound);
                Outcome(wrapper.Load.GetNotionalContributionCount(17, 1), LoadQueryStatus.NativeFailure);
                Outcome(wrapper.Load.GetNotionalContributions(17, 1), LoadQueryStatus.NativeFailure);
                Outcome(wrapper.Load.GetNotionalItemCount(17), LoadQueryStatus.NativeFailure);
                Outcome(wrapper.Load.GetReferenceItemCount(17), LoadQueryStatus.NativeFailure);
            }
        }
        [Test]
        public void AmbiguousCommandZerosDoNotBecomeProvenEmptyInventories()
        {
            var native = new Native { Commands = 0 };
            using (var wrapper = Wrap(native))
            {
                Outcome(wrapper.Load.GetLoadCommandCount(17, LoadCommandKind.Nodal), LoadQueryStatus.EmptyOrUnavailable);
                Outcome(wrapper.Load.GetAssignedEntityCount(17, LoadCommandKind.Nodal, 0), LoadQueryStatus.EmptyOrUnavailable);
                Assert.That(native.Calls, Does.Not.Contain("GetListSizeForLoadType"));
            }
        }
        [TestCase(3312)] [TestCase(998877)]
        public void AmbiguousAndUnknownPositiveItemCodesAreRetained(int code)
        {
            using (var wrapper = Wrap(new Native { Kind = code }))
            {
                var result = wrapper.Load.GetLoadItemKind(17, 0);
                Assert.That((int)result.Data, Is.EqualTo(code));
                Assert.That(result.NativeReturnCode, Is.EqualTo(code));
                Assert.That(Enum.IsDefined(typeof(LoadItemKind), result.Data), Is.EqualTo(code == 3312));
            }
        }
        [TestCase("text")] [TestCase(1.25)] [TestCase(null)] [TestCase(true)] [TestCase(2147483648L)] [TestCase(-2)]
        public void MalformedNativeCountsAreNotCoerced(object count)
        {
            using (var wrapper = Wrap(new Native { Lists = count })) Assert.Throws<InvalidOperationException>(() => wrapper.Load.GetLoadListCount());
        }
        [TestCaseSource(nameof(Names))]
        public void StaleDisposedAndTransportFailuresPropagate(string name)
        {
            var native = new Native(); var wrapper = Wrap(native); var adapter = wrapper.Load;
            ((Root)wrapper.RawOpenStaad).Load = new Native(); wrapper.RefreshComObjects();
            Assert.Throws<InvalidOperationException>(() => Invoke(adapter, name));
            adapter = wrapper.Load; wrapper.Dispose();
            Assert.Throws<InvalidOperationException>(() => Invoke(adapter, name));
            var failure = new COMException("Original native failure");
            using (var connected = Wrap(new Native { Failure = failure })) Assert.That(Assert.Throws<COMException>(() => Invoke(connected.Load, name)), Is.SameAs(failure));
        }
    }
}
