using System;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using StaadPro.Interop.Services.Acquisition;

namespace StaadPro.Interop.Tests.Services
{
    [TestFixture]
    public class OpenStaadTargetFilesTests
    {
        private string directory;
        private string Target => Path.Combine(directory,"target.std");
        [SetUp] public void SetUp() { directory=Path.Combine(Path.GetTempPath(),"StaadProviderTests-"+Guid.NewGuid().ToString("N"));Directory.CreateDirectory(directory); }
        [TearDown] public void TearDown()
        {
            // Only individually remove files belonging to this test's newly created directory.
            if(!Directory.Exists(directory))return;
            foreach(var file in Directory.EnumerateFiles(directory))File.Delete(file);
            Directory.Delete(directory,false);
        }
        [Test] public void ExistingBytesArePreserved()
        {
            var bytes=new byte[]{0,255,10,23};File.WriteAllBytes(Target,bytes);OpenStaadTargetFiles.Prepare(Target,true,()=>{});Assert.That(File.ReadAllBytes(Target),Is.EqualTo(bytes));Assert.That(Directory.GetFiles(directory).Length,Is.EqualTo(1));
        }
        [Test] public void NewModelIsCompleteBeforeAtomicPublication()
        {
            int checks=0;OpenStaadTargetFiles.Prepare(Target,true,()=>{checks++;if(checks==3){Assert.That(File.Exists(Target),Is.False);var temporary=Directory.GetFiles(directory,"*.tmp").Single();Assert.That(File.ReadAllText(temporary),Does.EndWith("FINISH"+Environment.NewLine));}});
            Assert.That(File.ReadAllText(Target),Does.StartWith("STAAD SPACE"));Assert.That(File.ReadAllBytes(Target)[0],Is.EqualTo((byte)'S'));Assert.That(Directory.GetFiles(directory,"*.tmp"),Is.Empty);
        }
        [Test] public void CompetingCreatorWinsWithoutBeingOverwritten()
        {
            int checks=0;OpenStaadTargetFiles.Prepare(Target,true,()=>{if(++checks==3)File.WriteAllText(Target,"existing customer content");});Assert.That(File.ReadAllText(Target),Is.EqualTo("existing customer content"));Assert.That(Directory.GetFiles(directory,"*.tmp"),Is.Empty);
        }
        [Test] public void CancellationBeforePublishRemovesOnlyOwnedTemporaryFile()
        {
            int checks=0;Assert.Throws<OperationCanceledException>(()=>OpenStaadTargetFiles.Prepare(Target,true,()=>{if(++checks==3)throw new OperationCanceledException();}));Assert.That(File.Exists(Target),Is.False);Assert.That(Directory.GetFiles(directory),Is.Empty);
        }
        [Test] public void DisabledCreationHasNoTargetSideEffects()
        {
            Assert.Throws<FileNotFoundException>(()=>OpenStaadTargetFiles.Prepare(Target,false,()=>{}));Assert.That(Directory.GetFiles(directory),Is.Empty);
        }
        [Test] public void CancellationBeforeWorkDoesNotCreateAnything()
        {
            Assert.Throws<OperationCanceledException>(()=>OpenStaadTargetFiles.Prepare(Target,true,()=>{throw new OperationCanceledException();}));Assert.That(Directory.GetFiles(directory),Is.Empty);
        }
    }
}
