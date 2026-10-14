using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Services;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSRootRewriteTests
    {
        private const string Target = @"C:\Models\Frame.std";
        internal sealed class Clock : IRootPollingClock
        {
            public double Now;
            public List<double> Sleeps = new List<double>();
            public double ElapsedSeconds => Now;
            public void Sleep(double seconds) { Sleeps.Add(seconds); Now += seconds; }
        }
        public class NativeRoot
        {
            public string Path = Target;
            public List<string> Events = new List<string>();
            public string FailAt;
            public Exception Failure = new COMException("Native close/reopen failed");
            public bool RemainNamed;
            public Action OnClose;
            private void Step(string name) { Events.Add(name); if (FailAt == name) throw Failure; }
            public void GetSTAADFile(ref object path, bool full) { Assert.That(full, Is.True); Step("read"); path = Path; }
            public object SetSilentMode(int flag) { Assert.That(flag, Is.EqualTo(1)); Step("silent"); return 0; }
            public void CloseSTAADFile() { Step("close"); if (!RemainNamed) Path = string.Empty; OnClose?.Invoke(); }
            public void OpenSTAADFile(string path) { Step("open"); Path = path; }
        }
        internal sealed class Files : IRootModelFiles
        {
            private readonly NativeRoot root;
            public Files(NativeRoot root) { this.root = root; }
            public bool Locked;
            public string FailAt;
            public Exception Failure = new IOException("Filesystem request failed");
            public string[] Written;
            public Action OnWrite, OnProbe;
            private void Step(string name) { root.Events.Add(name); if (FailAt == name) throw Failure; }
            public void ValidateExistingFile(string path) { Assert.That(path, Is.EqualTo(Target)); Step("validate"); }
            public bool IsLocked(string path) { Step("probe"); OnProbe?.Invoke(); return Locked; }
            public void WriteAtomically(string path, string[] lines) { Step("write"); Written = lines; OnWrite?.Invoke(); }
        }

        [TestCase("")] [TestCase(" \t ")]
        public void CloseCurrentDoesNothingForExplicitEmptyOrWhitespaceFilename(string current)
        {
            var native = new NativeRoot { Path = current };
            new OSRootAdapter(native).CloseCurrentStaadModel();
            Assert.That(native.Events, Is.EqualTo(new[] { "read" }));
        }
        [Test]
        public void CloseCurrentReadsOnceThenSetsSilentAndClosesInOrder()
        {
            var native = new NativeRoot(); new OSRootAdapter(native).CloseCurrentStaadModel();
            Assert.That(native.Events, Is.EqualTo(new[] { "read", "silent", "close" }));
        }
        [TestCase("read")] [TestCase("silent")] [TestCase("close")]
        public void CloseCurrentPropagatesNativeFailureAndDoesNotContinue(string stage)
        {
            var native = new NativeRoot { FailAt = stage };
            Assert.That(Assert.Throws<COMException>(() => new OSRootAdapter(native).CloseCurrentStaadModel()), Is.SameAs(native.Failure));
            Assert.That(native.Events.Last(), Is.EqualTo(stage));
            Assert.That(native.Events.Count, Is.EqualTo(stage == "read" ? 1 : stage == "silent" ? 2 : 3));
        }

        [Test]
        public void RewriteSnapshotsLinesThenClosesWritesAndReopensInOrder()
        {
            var native = new NativeRoot(); var files = new Files(native); var clock = new Clock();
            var lines = new List<string> { "first", null, "雪" };
            native.OnClose = () => lines.Add("too late");
            Assert.That(new OSRootAdapter(native, () => clock, files).CloseWriteReopenStaadFile(Target, lines), Is.True);
            Assert.That(files.Written, Is.EqualTo(new[] { "first", null, "雪" }));
            Assert.That(native.Events, Is.EqualTo(new[] { "read", "validate", "silent", "close", "read", "probe", "read", "write", "read", "open" }));
            Assert.That(clock.Sleeps, Is.Empty);
        }
        [TestCase("")] [TestCase(@"D:\Models\Frame.std")]
        public void NonmatchingModelReturnsFalseBeforeFilesystemOrNativeMutation(string current)
        {
            var native = new NativeRoot { Path = current }; var files = new Files(native);
            Assert.That(new OSRootAdapter(native, () => new Clock(), files).CloseWriteReopenStaadFile(Target, new List<string>()), Is.False);
            Assert.That(native.Events, Is.EqualTo(new[] { "read" }));
        }
        [TestCase(true)] [TestCase(false)]
        public void UnreleasedModelOrFileTimesOutWithoutWriting(bool modelStillNamed)
        {
            var native = new NativeRoot { RemainNamed = modelStillNamed }; var files = new Files(native) { Locked = !modelStillNamed }; var clock = new Clock();
            Assert.That(new OSRootAdapter(native, () => clock, files).CloseWriteReopenStaadFile(Target, new List<string>(), 100, 250), Is.False);
            Assert.That(files.Written, Is.Null); Assert.That(native.Events, Does.Not.Contain("open"));
            Assert.That(clock.Now, Is.EqualTo(.25).Within(1e-10));
            Assert.That(clock.Sleeps.Last(), Is.EqualTo(.05).Within(1e-10));
        }
        [TestCase(true)] [TestCase(false)]
        public void ZeroBudgetPerformsOnlyAnImmediateAvailabilityCheck(bool locked)
        {
            var native = new NativeRoot(); var files = new Files(native) { Locked = locked }; var clock = new Clock();
            Assert.That(new OSRootAdapter(native, () => clock, files).CloseWriteReopenStaadFile(Target, new List<string>(), 100, 0), Is.EqualTo(!locked));
            Assert.That(clock.Sleeps, Is.Empty);
        }
        [TestCase(0, 1, "sleepTimeMilliseconds")] [TestCase(-1, 1, "sleepTimeMilliseconds")]
        [TestCase(1, -1, "maxTimeMilliseconds")]
        public void InvalidDurationsAreRejectedBeforeNativeAccess(int sleep, int limit, string name)
        {
            var native = new NativeRoot(); var files = new Files(native);
            Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => new OSRootAdapter(native, () => new Clock(), files).CloseWriteReopenStaadFile(Target, new List<string>(), sleep, limit)).ParamName, Is.EqualTo(name));
            Assert.That(native.Events, Is.Empty);
        }
        [Test]
        public void NullContentsIsRejectedBeforeNativeAccess()
        {
            var native = new NativeRoot();
            Assert.That(Assert.Throws<ArgumentNullException>(() => new OSRootAdapter(native).CloseWriteReopenStaadFile(Target, null)).ParamName, Is.EqualTo("fileContents"));
            Assert.That(native.Events, Is.Empty);
        }
        [TestCase("validate")] [TestCase("probe")] [TestCase("write")]
        public void FilesystemFailuresPropagateAndPreventSubsequentSteps(string stage)
        {
            var native = new NativeRoot(); var files = new Files(native) { FailAt = stage };
            Assert.That(Assert.Throws<IOException>(() => new OSRootAdapter(native, () => new Clock(), files).CloseWriteReopenStaadFile(Target, new List<string>())), Is.SameAs(files.Failure));
            Assert.That(native.Events.Last(), Is.EqualTo(stage)); Assert.That(native.Events, Does.Not.Contain("open"));
        }
        [TestCase("silent")] [TestCase("close")] [TestCase("open")]
        public void RewritePropagatesNativeFailuresIncludingAfterReplacement(string stage)
        {
            var native = new NativeRoot { FailAt = stage }; var files = new Files(native);
            Assert.That(Assert.Throws<COMException>(() => new OSRootAdapter(native, () => new Clock(), files).CloseWriteReopenStaadFile(Target, new List<string>())), Is.SameAs(native.Failure));
            Assert.That(files.Written != null, Is.EqualTo(stage == "open"));
        }
        [TestCase(true)] [TestCase(false)]
        public void InterveningModelSwitchPreventsOverwriteOrReopen(bool beforeWrite)
        {
            var native = new NativeRoot(); var files = new Files(native);
            if (beforeWrite) files.OnProbe = () => native.Path = @"D:\Other.std";
            else files.OnWrite = () => native.Path = @"D:\Other.std";
            Assert.That(new OSRootAdapter(native, () => new Clock(), files).CloseWriteReopenStaadFile(Target, new List<string>()), Is.False);
            Assert.That(files.Written != null, Is.EqualTo(!beforeWrite)); Assert.That(native.Events, Does.Not.Contain("open"));
        }
    }
}
