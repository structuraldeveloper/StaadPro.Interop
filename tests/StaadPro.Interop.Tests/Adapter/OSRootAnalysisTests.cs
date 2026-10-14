using System;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSRootAnalysisTests
    {
        public class NativeRoot
        {
            public int Calls;
            public int[] Flags;
            public object Status = 2;
            public Exception Failure;
            // No save, filename or silent-mode API: extra calls would fail binding.
            public object AnalyzeEx(int silent, int hidden, int wait)
            {
                Calls++; Flags = new[] { silent, hidden, wait };
                if (Failure != null) throw Failure;
                return Status;
            }
        }

        [TestCase(0,0,0)] [TestCase(0,0,1)] [TestCase(0,1,0)] [TestCase(0,1,1)]
        [TestCase(1,0,0)] [TestCase(1,0,1)] [TestCase(1,1,0)] [TestCase(1,1,1)]
        public void AllDefinedFlagCombinationsDispatchExactlyOnceInNativeOrder(int silent, int hidden, int wait)
        {
            var native = new NativeRoot();
            Assert.That(new OSRootAdapter(native).AnalyzeEx((SilentMode)silent, (HiddenMode)hidden, (WaitAnalysisMode)wait), Is.EqualTo(AnalysisResult.CompletedSuccessfully));
            Assert.That(native.Flags, Is.EqualTo(new[] { silent, hidden, wait }));
            Assert.That(native.Calls, Is.EqualTo(1));
        }
        [TestCase(-1)] [TestCase(0)] [TestCase(1)] [TestCase(2)] [TestCase(3)] [TestCase(4)] [TestCase(5)]
        [TestCase(-99)] [TestCase(999)]
        public void NativeStatusCodesIncludingErrorsAndUnknownCodesArePreserved(int code)
        {
            var native = new NativeRoot { Status = code };
            Assert.That((int)new OSRootAdapter(native).AnalyzeEx(SilentMode.Enable, HiddenMode.Enable, WaitAnalysisMode.Wait), Is.EqualTo(code));
            Assert.That(native.Calls, Is.EqualTo(1));
        }
        [Test]
        public void IntegralStatusRepresentationsAreAcceptedWithoutNumericCoercion()
        {
            foreach (object status in new object[] { (sbyte)2, (byte)2, (short)2, (ushort)2, 2, 2U, 2L, 2UL })
                Assert.That(new OSRootAdapter(new NativeRoot { Status = status }).AnalyzeEx(SilentMode.Disable, HiddenMode.Disable, WaitAnalysisMode.Return), Is.EqualTo(AnalysisResult.CompletedSuccessfully));
        }
        [TestCase(null)] [TestCase(true)] [TestCase(false)] [TestCase("2")]
        [TestCase(2d)] [TestCase(2.5d)] [TestCase(double.NaN)]
        [TestCase(long.MaxValue)] [TestCase(long.MinValue)] [TestCase(uint.MaxValue)] [TestCase(ulong.MaxValue)]
        public void MalformedOrOutOfRangeStatusThrowsAfterOneRequest(object status)
        {
            var native = new NativeRoot { Status = status };
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).AnalyzeEx(SilentMode.Enable, HiddenMode.Enable, WaitAnalysisMode.Return));
            Assert.That(native.Calls, Is.EqualTo(1), "Failure does not retry or undo the analysis request.");
        }
        [TestCase("silentMode", -1)] [TestCase("silentMode", 2)]
        [TestCase("hiddenMode", -1)] [TestCase("hiddenMode", 2)]
        [TestCase("waitMode", -1)] [TestCase("waitMode", 2)]
        public void InvalidFlagsAreRejectedBeforeNativeAnalysis(string parameter, int value)
        {
            var native = new NativeRoot();
            var error = Assert.Throws<ArgumentOutOfRangeException>(() => new OSRootAdapter(native).AnalyzeEx(
                parameter == "silentMode" ? (SilentMode)value : SilentMode.Enable,
                parameter == "hiddenMode" ? (HiddenMode)value : HiddenMode.Enable,
                parameter == "waitMode" ? (WaitAnalysisMode)value : WaitAnalysisMode.Wait));
            Assert.That(error.ParamName, Is.EqualTo(parameter)); Assert.That(native.Calls, Is.Zero);
        }
        [Test]
        public void DecimalStatusIsNotCoercedIntoSuccess()
        {
            var native = new NativeRoot { Status = 2m };
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).AnalyzeEx(SilentMode.Enable, HiddenMode.Enable, WaitAnalysisMode.Wait));
            Assert.That(native.Calls, Is.EqualTo(1));
        }
        [Test]
        public void NativeFailurePropagatesUnchangedWithoutFallback()
        {
            var native = new NativeRoot { Failure = new COMException("Analysis dispatch failed") };
            Assert.That(Assert.Throws<COMException>(() => new OSRootAdapter(native).AnalyzeEx(SilentMode.Enable, HiddenMode.Enable, WaitAnalysisMode.Wait)), Is.SameAs(native.Failure));
            Assert.That(native.Calls, Is.EqualTo(1));
        }
        [Test]
        public void MissingNativeSignaturePropagates()
        {
            Assert.Throws<RuntimeBinderException>(() => new OSRootAdapter(new object()).AnalyzeEx(SilentMode.Enable, HiddenMode.Enable, WaitAnalysisMode.Wait));
        }
    }
}
