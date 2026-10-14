using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSRootLifecycleTests
    {
        public class NativeRoot
        {
            public List<string> Calls = new List<string>();
            public int SilentFlag;
            public int SaveFlag = -1;
            public bool ModelClosed;
            public object PreviousValue;
            public Exception Failure;

            // Strict integer signatures verify that enum values are converted to native codes.
            public object SetSilentMode(int flag)
            {
                Calls.Add("silent");
                SilentFlag = flag;
                if (Failure != null) throw Failure;
                return PreviousValue;
            }
            public void SaveModel(int flag)
            {
                Calls.Add("save");
                SaveFlag = flag;
                if (Failure != null) throw Failure;
            }
            public void CloseSTAADFile()
            {
                Calls.Add("close");
                ModelClosed = true;
                if (Failure != null) throw Failure;
            }
        }

        [TestCase(SilentMode.Disable, 0)]
        [TestCase(SilentMode.Enable, 1)]
        public void SilentModeDispatchesOneIntegerCodeAndIgnoresPreviousState(SilentMode mode, int code)
        {
            foreach (object previous in new object[] { 0, 1, false, true, null })
            {
                var native = new NativeRoot { PreviousValue = previous };
                IOSRoot root = new OSRootAdapter(native);
                Assert.That(root.SetSilentMode(mode), Is.True);
                Assert.That(native.SilentFlag, Is.EqualTo(code));
                Assert.That(native.Calls, Is.EqualTo(new[] { "silent" }));
                Assert.That(native.SaveFlag, Is.EqualTo(-1));
                Assert.That(native.ModelClosed, Is.False);
            }
        }

        [TestCase(SilentMode.Disable, 0)]
        [TestCase(SilentMode.Enable, 1)]
        public void SaveDispatchesOneIntegerCodeAndReturnsOriginalAdapter(SilentMode mode, int code)
        {
            var native = new NativeRoot { SilentFlag = 17 };
            IOSRoot root = new OSRootAdapter(native);
            Assert.That(root.SaveModel(mode), Is.SameAs(root));
            Assert.That(native.SaveFlag, Is.EqualTo(code));
            Assert.That(native.SilentFlag, Is.EqualTo(17), "No separate silent-mode mutation.");
            Assert.That(native.ModelClosed, Is.False);
            Assert.That(native.Calls, Is.EqualTo(new[] { "save" }));
        }

        [TestCase("silent", -1)] [TestCase("silent", 2)]
        [TestCase("silent", int.MinValue)] [TestCase("silent", int.MaxValue)]
        [TestCase("save", -1)] [TestCase("save", 2)]
        [TestCase("save", int.MinValue)] [TestCase("save", int.MaxValue)]
        public void UndefinedModesThrowBeforeAnyNativeMutation(string operation, int code)
        {
            var native = new NativeRoot();
            var root = new OSRootAdapter(native);
            var error = Assert.Throws<ArgumentOutOfRangeException>(() => Invoke(root, operation, (SilentMode)code));
            Assert.That(error.ParamName, Is.EqualTo("silentMode"));
            Assert.That(native.Calls, Is.Empty);
        }

        [TestCase(false)] [TestCase(true)]
        public void SilentModeReturnsFalseForNativeFailuresWithoutRollingBack(bool comFailure)
        {
            var native = new NativeRoot { Failure = Error(comFailure) };
            Assert.That(new OSRootAdapter(native).SetSilentMode(SilentMode.Enable), Is.False);
            Assert.That(native.Calls, Is.EqualTo(new[] { "silent" }));
            Assert.That(native.SilentFlag, Is.EqualTo(1), "False does not guarantee absence of native side effects.");
        }

        [Test]
        public void SilentModeReturnsFalseForMissingSignature()
        {
            Assert.That(new OSRootAdapter(new object()).SetSilentMode(SilentMode.Enable), Is.False);
        }

        [TestCase("save", false)] [TestCase("save", true)]
        [TestCase("close", false)] [TestCase("close", true)]
        public void SaveAndClosePropagateOriginalFailuresWithoutRetriesOrRollback(string operation, bool comFailure)
        {
            var native = new NativeRoot { Failure = Error(comFailure) };
            var actual = Assert.Throws(native.Failure.GetType(), () => Invoke(new OSRootAdapter(native), operation));
            Assert.That(actual, Is.SameAs(native.Failure));
            Assert.That(native.Calls, Is.EqualTo(new[] { operation }));
        }

        [TestCase("save")] [TestCase("close")]
        public void SaveAndClosePropagateMissingSignatureErrors(string operation)
        {
            Assert.Throws<RuntimeBinderException>(() => Invoke(new OSRootAdapter(new object()), operation));
        }

        [Test]
        public void CloseDispatchesOnceWithoutSavingChangingSilentModeOrDisposingBinding()
        {
            var native = new NativeRoot { SilentFlag = 17 };
            IOSRoot root = new OSRootAdapter(native);
            Assert.That(root.CloseSTAADFile(), Is.SameAs(root));
            Assert.That(native.Calls, Is.EqualTo(new[] { "close" }));
            Assert.That(native.ModelClosed, Is.True);
            Assert.That(native.SaveFlag, Is.EqualTo(-1));
            Assert.That(native.SilentFlag, Is.EqualTo(17));
            Assert.That((object)root.ComObject, Is.SameAs(native));
        }

        [Test]
        public void FluentSaveThenClosePreservesExplicitOrderAndSessionIsolation()
        {
            var native = new NativeRoot();
            var unrelated = new NativeRoot();
            IOSRoot root = new OSRootAdapter(native);
            var other = new OSRootAdapter(unrelated);
            Assert.That(root.SaveModel(SilentMode.Enable).CloseSTAADFile(), Is.SameAs(root));
            Assert.That(native.Calls, Is.EqualTo(new[] { "save", "close" }));
            Assert.That(unrelated.Calls, Is.Empty);
            Assert.That((object)other.ComObject, Is.SameAs(unrelated));
        }

        private static Exception Error(bool comFailure) => comFailure
            ? (Exception)new COMException("Native lifecycle failure", unchecked((int)0x80004005))
            : new InvalidOperationException("Native lifecycle failure");

        private static void Invoke(OSRootAdapter root, string operation, SilentMode mode = SilentMode.Enable)
        {
            if (operation == "silent") root.SetSilentMode(mode);
            else if (operation == "save") root.SaveModel(mode);
            else root.CloseSTAADFile();
        }
    }
}
