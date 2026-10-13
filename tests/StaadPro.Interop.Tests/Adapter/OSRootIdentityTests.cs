using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSRootIdentityTests
    {
        private static bool Matches(IOSRoot root, bool modelIsOpen, string path) => modelIsOpen ? root.ModelIsOpen(path) : root.IsTarget(path);

        [TestCase(@"C:\Models\sub\..\Frame.v2.STD")]
        [TestCase(@"\\server\share\folder\..\雪.STD")]
        [TestCase(@"relative\..\frame.std")]
        public void OpenedFilenameNormalizesOneReadWithoutCheckingExistence(string nativePath)
        {
            var native = new OSRootPathTests.NativeRoot { Path = nativePath };
            var root = new OSRootAdapter(native);
            Assert.That(root.GetOpenedStaadFilename(), Is.EqualTo(Path.GetFullPath(nativePath)));
            Assert.That(native.Calls, Is.EqualTo(1));
        }

        [TestCase(false)] [TestCase(true)]
        public void IdentityMatchesCompleteNormalizedPathsAndIgnoresCase(bool modelIsOpen)
        {
            var native = new OSRootPathTests.NativeRoot { Path = @"C:\Models\sub\..\雪 Frame.STD" };
            var root = new OSRootAdapter(native);
            Assert.That(Matches(root, modelIsOpen, @"c:/models/雪 frame.std"), Is.True);
            Assert.That(native.Calls, Is.EqualTo(1));
            Assert.That(Matches(root, modelIsOpen, @"D:\Models\雪 frame.std"), Is.False, "Identical basenames in different directories are different models.");
            Assert.That(Matches(root, modelIsOpen, @"C:\Models\雪 Frame.STD.bak"), Is.False, "A substring is not identity.");
            Assert.That(native.Calls, Is.EqualTo(3));
        }

        [TestCase(false)] [TestCase(true)]
        public void RelativeRequestedAndNativePathsUseTheSameWorkingDirectory(bool modelIsOpen)
        {
            var native = new OSRootPathTests.NativeRoot { Path = @"sub\..\frame.std" };
            var root = new OSRootAdapter(native);
            Assert.That(Matches(root, modelIsOpen, Path.GetFullPath("frame.std")), Is.True);
            Assert.That(Matches(root, modelIsOpen, "frame.std"), Is.True);
            Assert.That(native.Calls, Is.EqualTo(2));
        }

        [Test]
        public void BlankNativeFilenameIsPreservedAndNeverMatchesANamedModel()
        {
            var native = new OSRootPathTests.NativeRoot { Path = string.Empty };
            var root = new OSRootAdapter(native);
            Assert.That(root.GetOpenedStaadFilename(), Is.Empty);
            Assert.That(root.IsTarget(@"C:\Models\Frame.STD"), Is.False);
            Assert.That(root.ModelIsOpen(@"C:\Models\Frame.STD"), Is.False);
            Assert.That(native.Calls, Is.EqualTo(3));
        }

        [TestCase(false)] [TestCase(true)]
        public void InvalidRequestedPathsFailBeforeNativeAccessWithPublicParameterNames(bool modelIsOpen)
        {
            var native = new OSRootPathTests.NativeRoot();
            var root = new OSRootAdapter(native);
            string parameterName = modelIsOpen ? "fileFullPath" : "staadFileFullPath";
            Assert.That(Assert.Throws<ArgumentNullException>(() => Matches(root, modelIsOpen, null)).ParamName, Is.EqualTo(parameterName));
            foreach (string path in new[] { "", " ", "\t\r\n" })
                Assert.That(Assert.Throws<ArgumentException>(() => Matches(root, modelIsOpen, path)).ParamName, Is.EqualTo(parameterName));
            Assert.Throws<ArgumentException>(() => Matches(root, modelIsOpen, "bad\0path.std"));
            Assert.That(native.Calls, Is.Zero);
        }

        [TestCase("filename")] [TestCase("target")] [TestCase("open")]
        public void NewQueriesRejectUnwrittenNonStringAndInvalidNativePaths(string operation)
        {
            foreach (object value in new object[] { null, true, 17, new object() })
            {
                var native = new OSRootPathTests.NativeRoot { Path = value };
                Assert.Throws<InvalidOperationException>(() => Read(new OSRootAdapter(native), operation));
                Assert.That(native.Calls, Is.EqualTo(1));
            }
            var unwritten = new OSRootPathTests.NativeRoot { WriteOutput = false };
            Assert.Throws<InvalidOperationException>(() => Read(new OSRootAdapter(unwritten), operation));
            var invalid = new OSRootPathTests.NativeRoot { Path = "native\0model.std" };
            Assert.Throws<ArgumentException>(() => Read(new OSRootAdapter(invalid), operation));
            Assert.That(unwritten.Calls, Is.EqualTo(1));
            Assert.That(invalid.Calls, Is.EqualTo(1));
        }

        [TestCase("filename")] [TestCase("target")] [TestCase("open")]
        public void NewQueriesPropagateNativeAndBindingFailuresInsteadOfReturningFalse(string operation)
        {
            var native = new OSRootPathTests.NativeRoot { Failure = new COMException("Root unavailable") };
            Assert.That(Assert.Throws<COMException>(() => Read(new OSRootAdapter(native), operation)), Is.SameAs(native.Failure));
            Assert.That(native.Calls, Is.EqualTo(1));
            Assert.Throws<RuntimeBinderException>(() => Read(new OSRootAdapter(new object()), operation));
        }

        private static object Read(IOSRoot root, string operation)
        {
            if (operation == "filename") return root.GetOpenedStaadFilename();
            return Matches(root, operation == "open", @"C:\Models\Frame.STD");
        }

        public class BstrOnlyRoot
        {
            public string Filename;
            public int Calls;
            public void GetSTAADFile(ref object output, bool fullPath)
            {
                Calls++;
                if (!(output is string)) throw new COMException("Bad variable type", unchecked((int)0x80020008));
                Assert.That(fullPath, Is.True);
                output = Filename;
            }
        }

        [TestCase("")]
        [TestCase(@"C:\Models\Frame.STD")]
        public void SharedNativeFilenameReaderUsesBstrForBlankAndNamedModels(string filename)
        {
            var native = new BstrOnlyRoot { Filename = filename };
            var root = new OSRootAdapter(native);
            Assert.That(root.GetStaadFileFullPath(), Is.EqualTo(filename));
            Assert.That(root.GetOpenedStaadFilename(), Is.EqualTo(filename));
            Assert.That(native.Calls, Is.EqualTo(2));
        }
    }
}
