using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums.Root;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSRootRemainingViewTests
    {
        public class NativeRoot
        {
            public readonly List<string> Calls = new List<string>();
            public string Name;
            public int[] Ids;
            public int Qualifier, Mode;
            public Exception Failure;
            public Action DuringCall;
            public void RemoveNamedView(object name, object error)
            {
                Calls.Add("remove");
                Assert.That(name, Is.TypeOf<string>());
                Assert.That(error, Is.TypeOf<int>());
                Assert.That(error, Is.EqualTo(0));
                Name = (string)name;
                if (Failure != null) throw Failure;
            }
            public void ModifyNamedView(object name, object ids, object qualifier, object mode)
            {
                Calls.Add("modify");
                Assert.That(name, Is.TypeOf<string>());
                Assert.That(ids, Is.TypeOf<int[]>());
                Assert.That(qualifier, Is.TypeOf<int>());
                Assert.That(mode, Is.TypeOf<int>());
                Name = (string)name; Ids = (int[])ids; Qualifier = (int)qualifier; Mode = (int)mode;
                DuringCall?.Invoke();
                if (Failure != null) throw Failure;
            }
        }
        public class OnceIds : IEnumerable<int>
        {
            private readonly IEnumerable<int> values;
            public int Enumerations;
            public OnceIds(IEnumerable<int> values) { this.values = values; }
            public IEnumerator<int> GetEnumerator()
            {
                Enumerations++;
                if (Enumerations > 1) throw new InvalidOperationException("Enumerated twice.");
                return values.GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        public class HelpSignature
        {
            public void RemoveNamedView(object name, ref object error) => Assert.Fail("Must not call alternative signature.");
            public void ModifyNamedView(object name, object count, object ids, object kind, object mode, ref object error) => Assert.Fail("Must not call alternative signature.");
        }

        [Test]
        public void RemoveUsesVerifiedByValueZeroSlotAndPreservesLiteralName()
        {
            var native = new NativeRoot();
            new OSRootAdapter(native).RemoveNamedView("  雪 view  ");
            Assert.That(native.Name, Is.EqualTo("  雪 view  "));
            Assert.That(native.Calls, Is.EqualTo(new[] { "remove" }));
        }
        [TestCase(0)] [TestCase(1)] [TestCase(2)] [TestCase(3)] [TestCase(4)]
        public void ModifySendsFourNativeInputsWithEveryDocumentedQualifier(int qualifier)
        {
            var native = new NativeRoot();
            var ids = new OnceIds(new[] { 17, 4, 17 });
            new OSRootAdapter(native).ModifyNamedView("  雪 view  ", ids, (NamedViewEntityType)qualifier, -123);
            Assert.That(native.Name, Is.EqualTo("  雪 view  "));
            Assert.That(native.Ids, Is.EqualTo(new[] { 17, 4, 17 }));
            Assert.That(native.Qualifier, Is.EqualTo(qualifier));
            Assert.That(native.Mode, Is.EqualTo(-123));
            Assert.That(ids.Enumerations, Is.EqualTo(1));
            Assert.That(native.Calls, Is.EqualTo(new[] { "modify" }));
        }
        [TestCase(int.MinValue)] [TestCase(0)] [TestCase(1)] [TestCase(int.MaxValue)]
        public void RawModificationModesAreNotReinterpreted(int mode)
        {
            var native = new NativeRoot();
            new OSRootAdapter(native).ModifyNamedView("View", new[] { 1 }, NamedViewEntityType.Beam, mode);
            Assert.That(native.Mode, Is.EqualTo(mode));
        }
        [Test]
        public void EmptyArrayIsAnExplicitNativeRequestRatherThanAnInventedNoOp()
        {
            var native = new NativeRoot();
            new OSRootAdapter(native).ModifyNamedView("View", new int[0], NamedViewEntityType.Node, 0);
            Assert.That(native.Ids, Is.Empty);
            Assert.That(native.Calls, Is.EqualTo(new[] { "modify" }));
        }
        [Test]
        public void ArrayStorageIsIndependentFromCallerAndNativeMutations()
        {
            int[] source = { 1, 2 };
            var native = new NativeRoot();
            native.DuringCall = () => native.Ids[0] = 999;
            new OSRootAdapter(native).ModifyNamedView("View", source, NamedViewEntityType.Plate, 1);
            Assert.That(source, Is.EqualTo(new[] { 1, 2 }));
            source[1] = 333;
            Assert.That(native.Ids, Is.EqualTo(new[] { 999, 2 }));
        }
        [TestCase(null, true)] [TestCase("", true)] [TestCase(" \t", true)]
        [TestCase(null, false)] [TestCase("", false)] [TestCase(" \t", false)]
        public void InvalidNamesNeverDispatch(string name, bool modify)
        {
            var native = new NativeRoot();
            var error = Assert.Catch<ArgumentException>(() =>
            {
                var root = new OSRootAdapter(native);
                if (modify) root.ModifyNamedView(name, new[] { 1 }, NamedViewEntityType.Beam, 0);
                else root.RemoveNamedView(name);
            });
            Assert.That(error.ParamName, Is.EqualTo("viewName"));
            Assert.That(native.Calls, Is.Empty);
        }
        [Test]
        public void NullSequenceIsRejectedBeforeNativeAccess()
        {
            var native = new NativeRoot();
            Assert.That(Assert.Throws<ArgumentNullException>(() => new OSRootAdapter(native)
                .ModifyNamedView("View", null, NamedViewEntityType.Node, 0)).ParamName, Is.EqualTo("entityIds"));
            Assert.That(native.Calls, Is.Empty);
        }
        [TestCase(-1)] [TestCase(5)] [TestCase(int.MaxValue)]
        public void InvalidQualifierDoesNotEnumerateOrDispatch(int qualifier)
        {
            var native = new NativeRoot(); var ids = new OnceIds(new[] { 1 });
            Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => new OSRootAdapter(native)
                .ModifyNamedView("View", ids, (NamedViewEntityType)qualifier, 0)).ParamName, Is.EqualTo("entityType"));
            Assert.That(ids.Enumerations, Is.Zero);
            Assert.That(native.Calls, Is.Empty);
        }
        [TestCase(0)] [TestCase(-1)] [TestCase(int.MinValue)]
        public void EveryIdIsValidatedBeforeMutation(int invalid)
        {
            var native = new NativeRoot();
            Assert.That(Assert.Throws<ArgumentException>(() => new OSRootAdapter(native)
                .ModifyNamedView("View", new[] { 1, invalid }, NamedViewEntityType.Beam, 0)).ParamName, Is.EqualTo("entityIds"));
            Assert.That(native.Calls, Is.Empty);
        }
        private static IEnumerable<int> BrokenIds() { yield return 1; throw new InvalidOperationException("Enumeration failed."); }
        [Test]
        public void EnumerationFailurePreventsMutation()
        {
            var native = new NativeRoot();
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).ModifyNamedView("View", BrokenIds(), NamedViewEntityType.Node, 0));
            Assert.That(native.Calls, Is.Empty);
        }
        [TestCase(true)] [TestCase(false)]
        public void NativeFailuresRemainOriginalAndAreNotRetried(bool modify)
        {
            var failure = new COMException("Native view failed");
            var native = new NativeRoot { Failure = failure };
            Assert.That(Assert.Throws<COMException>(() => Invoke(new OSRootAdapter(native), modify)), Is.SameAs(failure));
            Assert.That(native.Calls, Is.EqualTo(new[] { modify ? "modify" : "remove" }));
        }
        [TestCase(true)] [TestCase(false)]
        public void IncompatibleHelpSignaturesAreNotTriedAsFallback(bool modify)
        {
            Assert.Throws<RuntimeBinderException>(() => Invoke(new OSRootAdapter(new HelpSignature()), modify));
        }
        private static void Invoke(OSRootAdapter root, bool modify)
        {
            if (modify) root.ModifyNamedView("View", new[] { 1 }, NamedViewEntityType.Beam, 0);
            else root.RemoveNamedView("View");
        }
    }
}
