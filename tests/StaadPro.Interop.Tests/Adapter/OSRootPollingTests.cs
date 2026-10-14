using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Services;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSRootPollingTests
    {
        internal sealed class Clock : IRootPollingClock
        {
            public double Now;
            public List<double> Sleeps = new List<double>();
            public double ElapsedSeconds => Now;
            public void Sleep(double seconds) { Sleeps.Add(seconds); Now += seconds; }
        }

        public class NativeRoot
        {
            public string Path = @"C:\Models\Frame.std";
            public string Opened;
            public int Reads, Opens;
            public Exception Failure;
            public Action OnRead, OnOpen;
            public void GetSTAADFile(ref object path, bool full)
            {
                Reads++; Assert.That(full, Is.True);
                if (Failure != null) throw Failure;
                OnRead?.Invoke(); path = Path;
            }
            public void OpenSTAADFile(string path)
            {
                Opens++; Opened = path;
                if (Failure != null) throw Failure;
                OnOpen?.Invoke();
            }
        }

        [Test]
        public void WaitChecksImmediatelyUsingFullNormalizedCaseInsensitiveIdentity()
        {
            var native = new NativeRoot(); var clock = new Clock();
            var root = new OSRootAdapter(native, () => clock);
            Assert.That(root.AwaitModelHandle(@"c:\Models\sub\..\FRAME.std"), Is.True);
            Assert.That(native.Reads, Is.EqualTo(1)); Assert.That(native.Opens, Is.Zero);
            Assert.That(clock.Sleeps, Is.Empty);
        }

        [TestCase("")] [TestCase(@"D:\Models\Frame.std")]
        public void WaitTimesOutWithoutOpeningAndCapsLastSleep(string current)
        {
            var native = new NativeRoot { Path = current }; var clock = new Clock();
            Assert.That(new OSRootAdapter(native, () => clock).AwaitModelHandle(@"C:\Models\Frame.std", .25, .1), Is.False);
            Assert.That(clock.Sleeps.Count, Is.EqualTo(3));
            Assert.That(clock.Sleeps[2], Is.EqualTo(.05).Within(1e-10));
            Assert.That(clock.Now, Is.EqualTo(.25).Within(1e-10));
            Assert.That(native.Reads, Is.EqualTo(3)); Assert.That(native.Opens, Is.Zero);
        }

        [Test]
        public void WaitObservesLaterModelChangeWithOneReadPerPoll()
        {
            var native = new NativeRoot { Path = string.Empty }; var clock = new Clock();
            native.OnRead = () => { if (native.Reads == 3) native.Path = @"C:\Models\Frame.std"; };
            Assert.That(new OSRootAdapter(native, () => clock).AwaitModelHandle(@"C:\Models\Frame.std", 1, .1), Is.True);
            Assert.That(native.Reads, Is.EqualTo(3)); Assert.That(clock.Sleeps.Count, Is.EqualTo(2));
        }

        [TestCase(true)] [TestCase(false)]
        public void CallsCompletingAfterBudgetAreNotSuccessful(bool open)
        {
            var native = new NativeRoot(); var clock = new Clock();
            if (open) native.OnOpen = () => clock.Now = 2;
            else native.OnRead = () => clock.Now = 2;
            var root = new OSRootAdapter(native, () => clock);
            Assert.That(open ? root.OpenStaadModel(native.Path, 10, 1) : root.AwaitModelHandle(native.Path, 1, .1), Is.False);
            Assert.That(native.Reads, Is.EqualTo(open ? 0 : 1)); Assert.That(clock.Sleeps, Is.Empty);
        }

        [TestCase(false, 0)] [TestCase(false, -1)] [TestCase(false, double.NaN)]
        [TestCase(false, double.PositiveInfinity)] [TestCase(false, double.NegativeInfinity)] [TestCase(false, double.MaxValue)]
        [TestCase(true, 0)] [TestCase(true, -1)] [TestCase(true, double.NaN)]
        [TestCase(true, double.PositiveInfinity)] [TestCase(true, double.NegativeInfinity)] [TestCase(true, double.MaxValue)]
        public void WaitRejectsInvalidDurationsBeforeNativeAccess(bool interval, double value)
        {
            var native = new NativeRoot(); var root = new OSRootAdapter(native, () => new Clock());
            var error = Assert.Throws<ArgumentOutOfRangeException>(() => root.AwaitModelHandle(native.Path, interval ? 1 : value, interval ? value : .1));
            Assert.That(error.ParamName, Is.EqualTo(interval ? "sleepIntervalInSeconds" : "timeoutDurationInSeconds"));
            Assert.That(native.Reads + native.Opens, Is.Zero);
        }

        [TestCase(0, 1, "sleepInterval")] [TestCase(-1, 1, "sleepInterval")]
        [TestCase(1, 0, "maxWaitTime")] [TestCase(1, -1, "maxWaitTime")]
        public void OpenRejectsInvalidDurationsBeforeDispatch(int interval, int limit, string parameter)
        {
            var native = new NativeRoot(); var root = new OSRootAdapter(native, () => new Clock());
            Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => root.OpenStaadModel(native.Path, interval, limit)).ParamName, Is.EqualTo(parameter));
            Assert.That(native.Opens + native.Reads, Is.Zero);
        }

        [TestCase(true, null)] [TestCase(true, " \t ")] [TestCase(true, "bad\0path")]
        [TestCase(false, null)] [TestCase(false, " \t ")] [TestCase(false, "bad\0path")]
        public void PathsAreValidatedBeforePollingOrOpening(bool open, string target)
        {
            var native = new NativeRoot(); var root = new OSRootAdapter(native, () => new Clock());
            TestDelegate call = () => { if (open) root.OpenStaadModel(target, 10, 1); else root.AwaitModelHandle(target, 1, .1); };
            if (target == null) Assert.That(Assert.Throws<ArgumentNullException>(call).ParamName, Is.EqualTo(open ? "staadFileFullPath" : "fileFullPath"));
            else Assert.Throws<ArgumentException>(call);
            Assert.That(native.Opens + native.Reads, Is.Zero);
        }

        [Test]
        public void OpenWaitUsesElapsedSecondsAndNeverAcceptsBasenameSubstring()
        {
            var native = new NativeRoot { Path = @"D:\Models\Frame_extra.std" }; var clock = new Clock();
            Assert.That(new OSRootAdapter(native, () => clock).OpenStaadModel(@"C:\Models\Frame.std", 400, 1), Is.False);
            Assert.That(native.Opens, Is.EqualTo(1)); Assert.That(native.Opened, Is.EqualTo(@"C:\Models\Frame.std"));
            Assert.That(clock.Now, Is.EqualTo(1)); Assert.That(clock.Sleeps.Count, Is.EqualTo(3));
        }

        [Test]
        public void OpenChecksImmediatelyAfterOneRequest()
        {
            var native = new NativeRoot(); var clock = new Clock();
            Assert.That(new OSRootAdapter(native, () => clock).OpenStaadModel(@"C:\Models\sub\..\Frame.std", 100, 1), Is.True);
            Assert.That(native.Opens, Is.EqualTo(1)); Assert.That(native.Reads, Is.EqualTo(1));
            Assert.That(clock.Sleeps, Is.Empty);
        }

        [TestCase(true)] [TestCase(false)]
        public void NativeFailuresPropagateUnchanged(bool open)
        {
            var native = new NativeRoot { Failure = new COMException("Native polling failure") };
            var root = new OSRootAdapter(native, () => new Clock());
            Assert.That(Assert.Throws<COMException>(() => { if (open) root.OpenStaadModel(native.Path, 1, 1); else root.AwaitModelHandle(native.Path, 1, .1); }), Is.SameAs(native.Failure));
        }
    }
}
