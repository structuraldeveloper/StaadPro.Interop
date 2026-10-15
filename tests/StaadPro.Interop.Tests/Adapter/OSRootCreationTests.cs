using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSRootCreationTests
    {
        public class NativeRoot
        {
            public readonly List<string> Calls = new List<string>();
            public object Filename = "";
            public bool WriteFilename = true;
            public string CreatedPath;
            public int LengthCode, ForceCode;
            public Exception QueryFailure, CreationFailure;
            public Action DuringRead, DuringCreate;

            public void GetSTAADFile(ref object filename, bool fullPath)
            {
                Calls.Add("filename");
                Assert.That(fullPath, Is.True);
                Assert.That(filename, Is.TypeOf<string>());
                if (QueryFailure != null) throw QueryFailure;
                if (WriteFilename) filename = Filename;
                DuringRead?.Invoke();
            }

            public void NewSTAADFile(object filename, object length, object force)
            {
                Calls.Add("create");
                Assert.That(filename, Is.TypeOf<string>());
                Assert.That(length, Is.TypeOf<int>());
                Assert.That(force, Is.TypeOf<int>());
                CreatedPath = (string)filename; LengthCode = (int)length; ForceCode = (int)force;
                DuringCreate?.Invoke();
                if (CreationFailure != null) throw CreationFailure;
            }
        }

        public class MissingCreation
        {
            public void GetSTAADFile(ref object filename, bool fullPath) { filename = ""; }
        }

        private string directory;
        private string Target => Path.Combine(directory, "New model.std");

        [SetUp]
        public void SetUp()
        {
            directory = Path.Combine(Path.GetTempPath(), "StaadRootCreation-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(directory);
        }

        [TearDown]
        public void TearDown()
        {
            // Only files created directly in this unique fixture directory are removed.
            foreach (string filename in Directory.GetFiles(directory))
            {
                File.SetAttributes(filename, FileAttributes.Normal);
                File.Delete(filename);
            }
            foreach (string child in Directory.GetDirectories(directory)) Directory.Delete(child);
            Directory.Delete(directory);
        }

        [TestCase(0)] [TestCase(1)] [TestCase(2)] [TestCase(3)]
        [TestCase(4)] [TestCase(5)] [TestCase(6)] [TestCase(7)]
        public void EveryLengthCodeIncludingFeetAliasIsSentUnchanged(int code)
        {
            var native = new NativeRoot();
            new OSRootAdapter(native).NewSTAADFile(Target, (LengthInputUnit)code, ForceInputUnit.Newton);
            Assert.That(native.LengthCode, Is.EqualTo(code));
            Assert.That(native.ForceCode, Is.EqualTo(4));
            Assert.That(native.CreatedPath, Is.EqualTo(Target));
            Assert.That(native.Calls, Is.EqualTo(new[] { "filename", "create" }));
            Assert.That(File.Exists(Target), Is.False, "The library does not fabricate native model contents.");
        }

        [TestCase(0)] [TestCase(1)] [TestCase(2)] [TestCase(3)]
        [TestCase(4)] [TestCase(5)] [TestCase(6)] [TestCase(7)]
        public void EveryDefinedForceCodeIsSentUnchanged(int code)
        {
            var native = new NativeRoot();
            new OSRootAdapter(native).NewSTAADFile(Target, LengthInputUnit.Meter, (ForceInputUnit)code);
            Assert.That(native.ForceCode, Is.EqualTo(code));
            Assert.That(native.LengthCode, Is.EqualTo(4));
        }

        [TestCase(null)] [TestCase("")] [TestCase(" \t")]
        [TestCase("bad\0path.std")] [TestCase("no-extension")] [TestCase("wrong.txt")]
        [TestCase("CON.std")] [TestCase("con.extra.std")] [TestCase("PRN.std")]
        [TestCase("NUL.std")] [TestCase("AUX.std")] [TestCase("COM1.std")]
        [TestCase("LPT9.std")] [TestCase("COM\u00b9.std")] [TestCase("bad?.std")]
        public void InvalidPathsAreRejectedBeforeAnyNativeRead(string filename)
        {
            var native = new NativeRoot();
            var error = Assert.Catch<ArgumentException>(() =>
            {
                string target = filename == null ? null : filename.Length == 0 || string.IsNullOrWhiteSpace(filename)
                    ? filename : Path.Combine(directory, filename);
                Create(native, target);
            });
            if (filename == null) Assert.That(error, Is.TypeOf<ArgumentNullException>());
            Assert.That(native.Calls, Is.Empty);
        }

        [TestCase(-1, 0, "lengthUnit")] [TestCase(8, 0, "lengthUnit")]
        [TestCase(int.MaxValue, 0, "lengthUnit")]
        [TestCase(4, -1, "forceUnit")] [TestCase(4, 8, "forceUnit")]
        [TestCase(4, int.MinValue, "forceUnit")]
        public void InvalidUnitsAreRejectedBeforeFilesystemAndNativeAccess(int length, int force, string parameter)
        {
            var native = new NativeRoot();
            string unavailableParent = Path.Combine(directory, "missing", "target.std");
            var error = Assert.Throws<ArgumentOutOfRangeException>(() =>
                new OSRootAdapter(native).NewSTAADFile(unavailableParent, (LengthInputUnit)length, (ForceInputUnit)force));
            Assert.That(error.ParamName, Is.EqualTo(parameter));
            Assert.That(native.Calls, Is.Empty);
        }

        [Test]
        public void PathIsNormalizedAndUnicodeNameIsPreserved()
        {
            string supplied = Path.Combine(directory, "unused", "..", "\u96ea New.STD");
            var native = new NativeRoot();
            Create(native, supplied);
            Assert.That(native.CreatedPath, Is.EqualTo(Path.GetFullPath(supplied)));
            Assert.That(native.Calls, Is.EqualTo(new[] { "filename", "create" }));
        }

        [Test]
        public void RelativePathIsResolvedAgainstCurrentProcessDirectory()
        {
            // A temporary directory can be on a different drive from the checkout.
            // Use an absent relative filename in the existing current directory;
            // the native stand-in records the request without creating a file.
            string filename = "StaadRootRelative-" + Guid.NewGuid().ToString("N") + " New model.std";
            string supplied = "." + Path.DirectorySeparatorChar + filename;
            string expectedPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            Assert.That(Path.IsPathRooted(supplied), Is.False);
            Assert.That(File.Exists(expectedPath), Is.False);
            var native = new NativeRoot();
            Create(native, supplied);
            Assert.That(native.CreatedPath, Is.EqualTo(expectedPath));
            Assert.That(native.Calls, Is.EqualTo(new[] { "filename", "create" }));
            Assert.That(File.Exists(expectedPath), Is.False);
        }

        [TestCase(false)] [TestCase(true)]
        public void ExistingTargetsArePreservedAndRejectedBeforeNativeAccess(bool readOnly)
        {
            File.WriteAllText(Target, "existing contents");
            if (readOnly) File.SetAttributes(Target, FileAttributes.ReadOnly);
            var native = new NativeRoot();
            Assert.Throws<IOException>(() => Create(native, Target));
            Assert.That(File.ReadAllText(Target), Is.EqualTo("existing contents"));
            Assert.That(native.Calls, Is.Empty);
        }

        [Test]
        public void ExistingDirectoryAtTargetIsNotAFileCreationDestination()
        {
            Directory.CreateDirectory(Target);
            var native = new NativeRoot();
            Assert.Throws<IOException>(() => Create(native, Target));
            Assert.That(native.Calls, Is.Empty);
        }

        [Test]
        public void MissingParentIsNotCreatedOrSilentlyTreatedAsAnAbsentFile()
        {
            string target = Path.Combine(directory, "missing", "target.std");
            var native = new NativeRoot();
            Assert.Catch<IOException>(() => Create(native, target));
            Assert.That(Directory.Exists(Path.GetDirectoryName(target)), Is.False);
            Assert.That(native.Calls, Is.Empty);
        }

        [Test]
        public void AFileCannotBeUsedAsTheParentDirectory()
        {
            string parentFile = Path.Combine(directory, "parent");
            File.WriteAllText(parentFile, "file");
            var native = new NativeRoot();
            Assert.Throws<IOException>(() => Create(native, Path.Combine(parentFile, "target.std")));
            Assert.That(native.Calls, Is.Empty);
        }

        [TestCase("C:\\Models\\Existing.std")] [TestCase("unsaved-name.std")]
        public void NamedSessionIsNotClosedSavedOrReplaced(string filename)
        {
            var native = new NativeRoot { Filename = filename };
            Assert.Throws<InvalidOperationException>(() => Create(native, Target));
            Assert.That(native.Calls, Is.EqualTo(new[] { "filename" }));
            Assert.That(File.Exists(Target), Is.False);
        }

        [TestCase("")] [TestCase(" \t")]
        public void UnnamedFilenamePolicyAllowsEmptyOrWhitespaceText(string filename)
        {
            var native = new NativeRoot { Filename = filename };
            Create(native, Target);
            Assert.That(native.Calls, Is.EqualTo(new[] { "filename", "create" }));
        }

        [TestCase(null)] [TestCase(42)] [TestCase(false)]
        public void MalformedFilenameCannotAuthorizeCreation(object filename)
        {
            var native = new NativeRoot { Filename = filename };
            Assert.Throws<InvalidOperationException>(() => Create(native, Target));
            Assert.That(native.Calls, Is.EqualTo(new[] { "filename" }));
        }

        [Test]
        public void UnwrittenFilenameCannotAuthorizeCreation()
        {
            var native = new NativeRoot { WriteFilename = false };
            Assert.Throws<InvalidOperationException>(() => Create(native, Target));
            Assert.That(native.Calls, Is.EqualTo(new[] { "filename" }));
        }

        [Test]
        public void TargetCreatedDuringNativeFilenameReadIsPreservedByFinalRecheck()
        {
            var native = new NativeRoot { DuringRead = () => File.WriteAllText(Target, "other writer") };
            Assert.Throws<IOException>(() => Create(native, Target));
            Assert.That(native.Calls, Is.EqualTo(new[] { "filename" }));
            Assert.That(File.ReadAllText(Target), Is.EqualTo("other writer"));
        }

        [TestCase(true)] [TestCase(false)]
        public void NativeFailuresPropagateWithoutRetryRollbackOrFileCleanup(bool duringQuery)
        {
            var error = new COMException("Disconnected", unchecked((int)0x80010108));
            var native = new NativeRoot();
            if (duringQuery) native.QueryFailure = error;
            else
            {
                native.CreationFailure = error;
                native.DuringCreate = () => File.WriteAllText(Target, "native partial effects");
            }
            Assert.That(Assert.Throws<COMException>(() => Create(native, Target)), Is.SameAs(error));
            Assert.That(native.Calls, Is.EqualTo(duringQuery ? new[] { "filename" } : new[] { "filename", "create" }));
            if (!duringQuery) Assert.That(File.ReadAllText(Target), Is.EqualTo("native partial effects"));
        }

        [Test]
        public void MissingCreationMemberDoesNotTriggerFallbackFileWriting()
        {
            Assert.Throws<RuntimeBinderException>(() => new OSRootAdapter(new MissingCreation())
                .NewSTAADFile(Target, LengthInputUnit.Meter, ForceInputUnit.KiloNewton));
            Assert.That(File.Exists(Target), Is.False);
        }

        [Test]
        public void WrapperCreationKeepsCachesUntilExplicitRefresh()
        {
            var native = new NativeRoot();
            using (var wrapper = new OpenStaadWrapper(native))
            {
                var originalLength = wrapper.InputLengthUnit;
                var originalForce = wrapper.InputForceUnit;
                native.Calls.Clear();
                wrapper.Root.NewSTAADFile(Target, LengthInputUnit.Inch, ForceInputUnit.Pound);
                Assert.That(wrapper.InputLengthUnit, Is.EqualTo(originalLength));
                Assert.That(wrapper.InputForceUnit, Is.EqualTo(originalForce));
                Assert.That(native.Calls, Is.EqualTo(new[] { "filename", "create" }));
            }
        }

        private static void Create(NativeRoot native, string target) =>
            new OSRootAdapter(native).NewSTAADFile(target, LengthInputUnit.Meter, ForceInputUnit.KiloNewton);
    }
}
