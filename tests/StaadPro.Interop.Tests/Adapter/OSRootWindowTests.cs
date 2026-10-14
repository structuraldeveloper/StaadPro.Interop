using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Services;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSRootWindowTests
    {
        public class NativeRoot
        {
            public object Handle = 123;
            public object Process = 42;
            public string FailAt;
            public Exception Failure = new COMException("Window identity lookup failed");
            public List<string> Calls = new List<string>();
            public object GetMainWindowHandle() { Calls.Add("handle"); if (FailAt == "handle") throw Failure; return Handle; }
            public object GetProcessId() { Calls.Add("process"); if (FailAt == "process") throw Failure; return Process; }
        }
        internal sealed class Windows : IRootWindowApi
        {
            public bool Valid = true, Accepted = true;
            public uint Process = 42;
            public List<string> Calls = new List<string>();
            public IntPtr Handle;
            public int Command;
            public bool IsWindow(IntPtr handle) { Calls.Add("validate"); Handle = handle; return Valid; }
            public uint GetProcessId(IntPtr handle) { Calls.Add("owner"); return Process; }
            public bool PostShowState(IntPtr handle, int command) { Calls.Add("post"); Command = command; return Accepted; }
        }
        [TestCase(true, 6)] [TestCase(false, 3)]
        public void ValidNativeIdentityPostsExactlyOneCorrectCommand(bool minimize, int command)
        {
            var native = new NativeRoot(); var windows = new Windows();
            Invoke(new OSRootAdapter(native, windows), minimize);
            Assert.That(native.Calls, Is.EqualTo(new[] { "handle", "process" }));
            Assert.That(windows.Calls, Is.EqualTo(new[] { "validate", "owner", "post" }));
            Assert.That(windows.Handle, Is.EqualTo(new IntPtr(123))); Assert.That(windows.Command, Is.EqualTo(command));
        }
        [Test]
        public void SupportedIntegerAndPointerHandlesRetainTheirValue()
        {
            foreach (object value in new object[] { (byte)123, (short)123, 123, 123U, 123L, 123UL, new IntPtr(123) })
            {
                var windows = new Windows();
                new OSRootAdapter(new NativeRoot { Handle = value }, windows).MinimizeStaadMainWindow();
                Assert.That(windows.Handle, Is.EqualTo(new IntPtr(123)));
            }
        }
        [TestCase(null)] [TestCase(0)] [TestCase(-1)] [TestCase("123")]
        [TestCase(true)] [TestCase(123d)] [TestCase(double.NaN)]
        public void InvalidHandlesAreRejectedBeforeAnyWindowApiCall(object handle)
        {
            var windows = new Windows();
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(new NativeRoot { Handle = handle }, windows).MaximizeStaadMainWindow());
            Assert.That(windows.Calls, Is.Empty);
        }
        [TestCase(null)] [TestCase(0)] [TestCase(-1)] [TestCase("42")]
        [TestCase(true)] [TestCase(42d)] [TestCase(long.MaxValue)]
        public void InvalidProcessIdsAreRejectedBeforeAnyWindowApiCall(object process)
        {
            var windows = new Windows();
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(new NativeRoot { Process = process }, windows).MinimizeStaadMainWindow());
            Assert.That(windows.Calls, Is.Empty);
        }
        [TestCase(false, 42U)] [TestCase(true, 0U)] [TestCase(true, 77U)]
        public void AbsentOrForeignWindowsAreNeverPosted(bool valid, uint owner)
        {
            var windows = new Windows { Valid = valid, Process = owner };
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(new NativeRoot(), windows).MaximizeStaadMainWindow());
            Assert.That(windows.Calls, Does.Not.Contain("post"));
        }
        [Test]
        public void RejectedPostThrowsWithoutInventingAWin32LastErrorOrRetrying()
        {
            var windows = new Windows { Accepted = false };
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(new NativeRoot(), windows).MinimizeStaadMainWindow());
            Assert.That(windows.Calls, Is.EqualTo(new[] { "validate", "owner", "post" }));
        }
        [TestCase("handle")] [TestCase("process")]
        public void NativeLookupFailuresPropagateBeforeWindowMutation(string stage)
        {
            var native = new NativeRoot { FailAt = stage }; var windows = new Windows();
            Assert.That(Assert.Throws<COMException>(() => new OSRootAdapter(native, windows).MaximizeStaadMainWindow()), Is.SameAs(native.Failure));
            Assert.That(windows.Calls, Is.Empty);
        }
        [TestCase(true)] [TestCase(false)]
        public void MissingNativeSignaturesPropagateWithoutWindowMutation(bool minimize)
        {
            var windows = new Windows();
            Assert.Throws<RuntimeBinderException>(() => Invoke(new OSRootAdapter(new object(), windows), minimize));
            Assert.That(windows.Calls, Is.Empty);
        }
        private static void Invoke(OSRootAdapter root, bool minimize)
        {
            if (minimize) root.MinimizeStaadMainWindow(); else root.MaximizeStaadMainWindow();
        }
    }
}
