using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSLoadSelectionMutationTests
    {
        public class NativeLoad
        {
            public object PrimaryStatus = true;
            public object ReferenceStatus = 17;
            public object MutationStatus = true;
            public object ListStatus = 0;
            public int[] Ids;
            public int Type;
            public int EnvelopeId;
            public double Factor;
            public int ActiveId;
            public bool Reference;
            public string ThrowAt;
            public Exception Failure = new COMException("Native mutation failed");
            public Action OnActivation;
            public int CallThread;
            public List<string> Calls { get; } = new List<string>();
            private void Record(string stage)
            {
                Calls.Add(stage);
                CallThread = Thread.CurrentThread.ManagedThreadId;
                if (ThrowAt == stage) throw Failure;
            }
            public object SetLoadActive(int id)
            {
                Record("primary"); OnActivation?.Invoke();
                if (PrimaryStatus is bool success && success) { ActiveId = id; Reference = false; }
                return PrimaryStatus;
            }
            public object SetReferenceLoadActive(int id)
            {
                Record("reference"); OnActivation?.Invoke();
                if (ReferenceStatus is int number && number == id) { ActiveId = id; Reference = true; }
                return ReferenceStatus;
            }
            public object AddSelfWeightInXYZToGeometry(int[] ids, int direction, double factor)
            {
                Record("selfweight"); Ids = ids; Type = direction; Factor = factor; return MutationStatus;
            }
            public object CreateLoadEnvelop(int id, int type, int[] ids)
            {
                Record("envelope"); EnvelopeId = id; Type = type; Ids = ids; return MutationStatus;
            }
            public object CreateLoadList(int type, int[] ids)
            {
                Record("list"); Type = type; Ids = ids; return ListStatus;
            }
        }

        private static object Invoke(IOSLoad load, string kind, IEnumerable<int> ids)
        {
            if (kind == "selfweight") return load.AddSelfweightInXYZTo(17, LoadCaseType.PrimaryLoad, ids);
            if (kind == "envelope") return load.CreateLoadEnvelopExt(7, LoadEnvelopType.Strength, ids);
            return (object)load.CreateLoadList(LoadListType.LoadList, ids);
        }

        [TestCase(LoadCaseType.PrimaryLoad, SelfWeightDirection.GlobalX, -1.123456789123)]
        [TestCase(LoadCaseType.ReferenceLoad, SelfWeightDirection.GlobalY, 0.0)]
        [TestCase(LoadCaseType.PrimaryLoad, SelfWeightDirection.GlobalZ, 2.5)]
        public void SelfWeightMapsFamilyGeometryDirectionPrecisionAndFluentReturn(LoadCaseType family, SelfWeightDirection direction, double factor)
        {
            var native = new NativeLoad();
            var source = new[] { 101, 102, 101 };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
            {
                Assert.That(wrapper.Load.AddSelfweightInXYZTo(17, family, source, direction, factor), Is.SameAs(wrapper.Load));
                Assert.That(native.Calls, Is.EqualTo(new[] { family == LoadCaseType.PrimaryLoad ? "primary" : "reference", "selfweight" }));
                Assert.That(native.Ids, Is.EqualTo(source).And.Not.SameAs(source));
                Assert.That(native.Type, Is.EqualTo((int)direction));
                Assert.That(native.Factor, Is.EqualTo(factor));
                Assert.That(native.ActiveId, Is.EqualTo(17));
                Assert.That(native.Reference, Is.EqualTo(family == LoadCaseType.ReferenceLoad));
                Assert.That(native.CallThread, Is.EqualTo(Thread.CurrentThread.ManagedThreadId));
                native.Ids[0] = 999;
                Assert.That(source, Is.EqualTo(new[] { 101, 102, 101 }));
            }
        }

        [Test]
        public void SelfWeightSnapshotPrecedesActivationAndEmptyInputSkipsNativeCalls()
        {
            var source = new List<int> { 101, 102 };
            var native = new NativeLoad { OnActivation = () => source.Clear() };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
            {
                wrapper.Load.AddSelfweightInXYZTo(17, LoadCaseType.PrimaryLoad, source);
                Assert.That(native.Ids, Is.EqualTo(new[] { 101, 102 }));
                native.Calls.Clear();
                Assert.That(wrapper.Load.AddSelfweightInXYZTo(17, LoadCaseType.ReferenceLoad, new int[0]), Is.SameAs(wrapper.Load));
                Assert.That(native.Calls, Is.Empty);
            }
        }

        [TestCase(LoadEnvelopType.None)] [TestCase(LoadEnvelopType.Stress)]
        [TestCase(LoadEnvelopType.Serviceability)] [TestCase(LoadEnvelopType.Column)]
        [TestCase(LoadEnvelopType.Connection)] [TestCase(LoadEnvelopType.Strength)] [TestCase(LoadEnvelopType.Temporary)]
        public void EnvelopeMapsEveryClassificationWithIndependentOrderedIds(LoadEnvelopType type)
        {
            var native = new NativeLoad();
            var source = new[] { 41, 17, 41 };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
                Assert.That(wrapper.Load.CreateLoadEnvelopExt(int.MaxValue, type, source), Is.SameAs(wrapper.Load));
            Assert.That(native.EnvelopeId, Is.EqualTo(int.MaxValue));
            Assert.That(native.Type, Is.EqualTo((int)type));
            Assert.That(native.Ids, Is.EqualTo(source).And.Not.SameAs(source));
            Assert.That(native.Calls, Is.EqualTo(new[] { "envelope" }));
            Assert.That(native.ActiveId, Is.Zero);
        }

        [TestCase(LoadListType.LoadList)] [TestCase(LoadListType.EnvelopList)]
        public void LoadListMapsTypeAndIdsAndReturnsUnchangedIntegerZero(LoadListType type)
        {
            foreach (object status in new object[] { (short)0, 0, 0L })
            {
                var native = new NativeLoad { ListStatus = status };
                var source = new[] { 41, 17, 41 };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
                    Assert.That((object)wrapper.Load.CreateLoadList(type, source), Is.SameAs(status));
                Assert.That(native.Type, Is.EqualTo((int)type));
                Assert.That(native.Ids, Is.EqualTo(source).And.Not.SameAs(source));
                Assert.That(native.Calls, Is.EqualTo(new[] { "list" }));
            }
        }

        [TestCase("envelope")] [TestCase("list")]
        public void EmptyEnvelopeAndListInputsAreForwardedWithoutActivation(string kind)
        {
            var native = new NativeLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
                Invoke(wrapper.Load, kind, new int[0]);
            Assert.That(native.Ids, Is.Empty);
            Assert.That(native.Calls, Is.EqualTo(new[] { kind }));
        }

        [TestCase("selfweight")] [TestCase("envelope")] [TestCase("list")]
        public void NullInvalidAndThrowingInputsHaveNoNativeSideEffects(string kind)
        {
            var native = new NativeLoad();
            string parameter = kind == "selfweight" ? "entities" : kind == "envelope" ? "loadCaseList" : "loadCaseIds";
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
            {
                Assert.That(Assert.Throws<ArgumentNullException>(() => Invoke(wrapper.Load, kind, null)).ParamName, Is.EqualTo(parameter));
                foreach (int invalid in new[] { 0, -1, int.MinValue })
                    Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => Invoke(wrapper.Load, kind, new[] { 17, invalid })).ParamName, Is.EqualTo(parameter));
                var error = new InvalidOperationException("Input enumeration failed");
                Assert.That(Assert.Throws<InvalidOperationException>(() => Invoke(wrapper.Load, kind, ThrowingIds(error))), Is.SameAs(error));
            }
            Assert.That(native.Calls, Is.Empty);
        }

        private static IEnumerable<int> ThrowingIds(Exception error) { yield return 17; throw error; }

        [Test]
        public void SelfWeightValidatesCaseFamilyDirectionAndFiniteFactorBeforeCom()
        {
            var native = new NativeLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
            {
                foreach (int id in new[] { 0, -1, int.MinValue })
                    Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.AddSelfweightInXYZTo(id, LoadCaseType.PrimaryLoad, new int[0])).ParamName, Is.EqualTo("loadCaseId"));
                foreach (LoadCaseType type in new[] { LoadCaseType.RepeatLoad, LoadCaseType.LoadCombination, (LoadCaseType)999 })
                    Assert.Throws<NotSupportedException>(() => wrapper.Load.AddSelfweightInXYZTo(17, type, new int[0]));
                foreach (SelfWeightDirection direction in new[] { (SelfWeightDirection)0, (SelfWeightDirection)4, (SelfWeightDirection)(-1) })
                    Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.AddSelfweightInXYZTo(17, LoadCaseType.PrimaryLoad, new int[0], direction)).ParamName, Is.EqualTo("loadDirection"));
                foreach (double factor in new[] { double.NaN, double.PositiveInfinity, double.NegativeInfinity })
                    Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.AddSelfweightInXYZTo(17, LoadCaseType.PrimaryLoad, new int[0], factor: factor)).ParamName, Is.EqualTo("factor"));
            }
            Assert.That(native.Calls, Is.Empty);
        }

        [Test]
        public void EnvelopeAndListRejectInvalidTypesAndEnvelopeIdsBeforeCom()
        {
            var native = new NativeLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
            {
                foreach (int id in new[] { 0, -1 })
                    Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.CreateLoadEnvelopExt(id, LoadEnvelopType.None, new int[0])).ParamName, Is.EqualTo("envelopId"));
                foreach (int type in new[] { -1, 7, int.MaxValue })
                    Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.CreateLoadEnvelopExt(7, (LoadEnvelopType)type, new int[0])).ParamName, Is.EqualTo("envelopType"));
                foreach (int type in new[] { -1, 2, int.MaxValue })
                    Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.CreateLoadList((LoadListType)type, new int[0])).ParamName, Is.EqualTo("loadListType"));
            }
            Assert.That(native.Calls, Is.Empty);
        }

        [TestCase("selfweight")] [TestCase("envelope")] [TestCase("list")]
        public void InputsEnumerateOnceAndDisposeEnumerator(string kind)
        {
            var native = new NativeLoad();
            var ids = new OnceIds();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
                Invoke(wrapper.Load, kind, ids);
            Assert.That(ids.Enumerations, Is.EqualTo(1));
            Assert.That(ids.Disposed, Is.True);
            Assert.That(native.Ids, Is.EqualTo(new[] { 17, 41, 17 }));
        }

        private sealed class OnceIds : IEnumerable<int>
        {
            public int Enumerations;
            public bool Disposed;
            public IEnumerator<int> GetEnumerator()
            {
                if (++Enumerations != 1) throw new InvalidOperationException("Input repeated");
                return Items().GetEnumerator();
            }
            private IEnumerable<int> Items() { try { yield return 17; yield return 41; yield return 17; } finally { Disposed = true; } }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [TestCase("selfweight")] [TestCase("envelope")]
        public void BooleanMutationSuccessAndFailureRepresentationsAreChecked(string kind)
        {
            foreach (object status in new object[] { true, 1, (short)1, -1, -1L })
            {
                var native = new NativeLoad { MutationStatus = status };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
                    Assert.That(Invoke(wrapper.Load, kind, new[] { 17 }), Is.SameAs(wrapper.Load));
            }
            foreach (object status in new object[] { false, 0, 2, -106, null, "true", 1.0, long.MaxValue })
            {
                var native = new NativeLoad { MutationStatus = status };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
                    Assert.Throws<InvalidOperationException>(() => Invoke(wrapper.Load, kind, new[] { 17 }));
                Assert.That(native.Calls.Last(), Is.EqualTo(kind));
                if (kind == "selfweight") Assert.That(native.ActiveId, Is.EqualTo(17), "Assignment failure does not restore activation.");
            }
        }

        [Test]
        public void LoadListRequiresIntegerZeroRatherThanBooleanOrOneSuccess()
        {
            foreach (object status in new object[] { true, false, 1, -1, -106, null, "0", 0.0, long.MaxValue })
            {
                var native = new NativeLoad { ListStatus = status };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
                    Assert.Throws<InvalidOperationException>(() => Invoke(wrapper.Load, "list", new[] { 17 }));
                Assert.That(native.Calls, Is.EqualTo(new[] { "list" }));
            }
        }

        [TestCase(LoadCaseType.PrimaryLoad)] [TestCase(LoadCaseType.ReferenceLoad)]
        public void ActivationFailurePreventsSelfWeightAssignment(LoadCaseType family)
        {
            foreach (object status in new object[] { false, 0, 2, null, "17", 17.0 })
            {
                var native = new NativeLoad { PrimaryStatus = status, ReferenceStatus = status };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
                    Assert.Throws<InvalidOperationException>(() => wrapper.Load.AddSelfweightInXYZTo(17, family, new[] { 101 }));
                Assert.That(native.Calls, Is.EqualTo(new[] { family == LoadCaseType.PrimaryLoad ? "primary" : "reference" }));
            }
        }

        [TestCase("primary")] [TestCase("reference")] [TestCase("selfweight")]
        [TestCase("envelope")] [TestCase("list")]
        public void NativeExceptionsPropagateUnchangedAndStopFurtherCalls(string stage)
        {
            var native = new NativeLoad { ThrowAt = stage };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native)))
            {
                Exception actual = stage == "reference"
                    ? Assert.Throws<COMException>(() => wrapper.Load.AddSelfweightInXYZTo(17, LoadCaseType.ReferenceLoad, new[] { 101 }))
                    : Assert.Throws<COMException>(() => Invoke(wrapper.Load, stage == "primary" ? "selfweight" : stage, new[] { 17 }));
                Assert.That(actual, Is.SameAs(native.Failure));
            }
            Assert.That(native.Calls.Last(), Is.EqualTo(stage));
        }

        public class ActivationOnly { public bool SetLoadActive(int id) => true; }

        [TestCase("selfweight")] [TestCase("envelope")] [TestCase("list")]
        public void MissingNativeMethodsPropagateBindingFailures(string kind)
        {
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(kind == "selfweight" ? (object)new ActivationOnly() : new object())))
                Assert.Throws<RuntimeBinderException>(() => Invoke(wrapper.Load, kind, new[] { 17 }));
        }
    }
}
