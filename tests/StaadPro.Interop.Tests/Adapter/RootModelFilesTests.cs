using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using StaadPro.Interop.Services;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class RootModelFilesTests
    {
        private string directory, target;
        [SetUp]
        public void CreateOwnedFile()
        {
            directory = Path.Combine(Path.GetTempPath(), "StaadRootFiles-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(directory); target = Path.Combine(directory, "Target.std");
            File.WriteAllText(target, "original", new UTF8Encoding(false));
        }
        [TearDown]
        public void RemoveOwnedImmediateFiles()
        {
            foreach (string file in Directory.GetFiles(directory)) { File.SetAttributes(file, FileAttributes.Normal); File.Delete(file); }
            Directory.Delete(directory, false);
        }
        [Test]
        public void AtomicReplacementWritesCompleteUtf8WithoutBomAndPreservesNullLineMeaning()
        {
            var files = new RootModelFiles(); files.ValidateExistingFile(target);
            files.WriteAtomically(target, new[] { "first", null, "雪" });
            var expected = new UTF8Encoding(false).GetBytes("first" + Environment.NewLine + Environment.NewLine + "雪" + Environment.NewLine);
            Assert.That(File.ReadAllBytes(target), Is.EqualTo(expected));
            Assert.That(Directory.GetFiles(directory), Is.EqualTo(new[] { target }));
        }
        [Test]
        public void ExclusiveSharingViolationIsRetriedButUnlockedFileIsAvailable()
        {
            var files = new RootModelFiles();
            using (new FileStream(target, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                Assert.That(files.IsLocked(target), Is.True);
            Assert.That(files.IsLocked(target), Is.False);
        }
        [Test]
        public void MissingFileAndDirectoryAreErrorsRatherThanLockTimeouts()
        {
            var files = new RootModelFiles();
            Assert.Throws<FileNotFoundException>(() => files.ValidateExistingFile(target + ".missing"));
            Assert.Throws<IOException>(() => files.ValidateExistingFile(directory));
            Assert.Throws<FileNotFoundException>(() => files.IsLocked(target + ".missing"));
        }
        [Test]
        public void ReadOnlyFileIsRejectedBeforeMutation()
        {
            File.SetAttributes(target, FileAttributes.ReadOnly);
            Assert.Throws<UnauthorizedAccessException>(() => new RootModelFiles().ValidateExistingFile(target));
            Assert.That(File.ReadAllText(target), Is.EqualTo("original"));
        }
        [Test]
        public void FailedReplacementDoesNotTruncateTargetAndCleansOwnedTemporary()
        {
            using (new FileStream(target, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                Assert.Catch<IOException>(() => new RootModelFiles().WriteAtomically(target, new[] { "replacement" }));
            Assert.That(File.ReadAllText(target), Is.EqualTo("original"));
            Assert.That(Directory.GetFiles(directory), Is.EqualTo(new[] { target }));
        }
        [Test]
        public void DisappearingTargetDoesNotBecomeAnImplicitNewFile()
        {
            File.Delete(target);
            Assert.Catch<IOException>(() => new RootModelFiles().WriteAtomically(target, new[] { "replacement" }));
            Assert.That(Directory.GetFiles(directory), Is.Empty);
        }
    }
}
