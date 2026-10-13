using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSRootPathTests
    {
        public class NativeRoot
        {
            public object Path = @"C:\Models\雪 frame.v2.STD";
            public bool WriteOutput = true;
            public Exception Failure;
            public int Calls;
            public Queue<string> Paths;
            public object Geometry { get; } = new object();
            public object Load { get; } = new object();
            public void GetSTAADFile(ref object name, bool fullPath)
            {
                Calls++;
                Assert.That(fullPath, Is.True);
                Assert.That(name, Is.Null, "Unwritten output must not look like an empty native filename.");
                if (Failure != null) throw Failure;
                if (WriteOutput) name = Paths != null ? Paths.Dequeue() : Path;
            }
        }

        private static string Read(IOSRoot root, string method)
        {
            if (method == "full") return root.GetStaadFileFullPath();
            if (method == "name") return root.GetStaadFileNameOnly();
            return root.GetAnlFileFullPath();
        }

        [TestCase(@"C:\Models\雪 frame.v2.STD", "雪 frame.v2.STD", @"C:\Models\雪 frame.v2.ANL")]
        [TestCase(@"\\server\share\folder\A.B.std", "A.B.std", @"\\server\share\folder\A.B.ANL")]
        [TestCase("relative.std", "relative.std", "relative.ANL")]
        [TestCase(@"sub\model", "model", @"sub\model.ANL")]
        [TestCase("", "", "")]
        public void PathQueriesMapNativeFilenameWithOneReadEach(string path, string name, string anl)
        {
            var native = new NativeRoot { Path = path };
            IOSRoot root = new OSRootAdapter(native);
            Assert.That(root.GetStaadFileFullPath(), Is.EqualTo(path));
            Assert.That(root.GetStaadFileNameOnly(), Is.EqualTo(name));
            Assert.That(root.GetAnlFileFullPath(), Is.EqualTo(anl));
            Assert.That(native.Calls, Is.EqualTo(3));
            Assert.That((object)root.ComObject, Is.SameAs(native));
        }

        [Test]
        public void AnalysisPathDoesNotMixTwoDifferentNativeModelReads()
        {
            var native = new NativeRoot { Paths = new Queue<string>(new[] { @"C:\A\first.std", @"D:\B\second.std" }) };
            var root = new OSRootAdapter(native);
            Assert.That(root.GetAnlFileFullPath(), Is.EqualTo(@"C:\A\first.ANL"));
            Assert.That(native.Calls, Is.EqualTo(1));
            Assert.That(root.GetAnlFileFullPath(), Is.EqualTo(@"D:\B\second.ANL"));
            Assert.That(native.Calls, Is.EqualTo(2));
        }

        [Test]
        public void FullPathPreservesWhitespaceAndDoesNotCheckFilesystemExistence()
        {
            var native = new NativeRoot { Path = "  X:\\unlikely-directory\\missing-雪.STD  " };
            Assert.That(new OSRootAdapter(native).GetStaadFileFullPath(), Is.EqualTo(native.Path));
            Assert.That(native.Calls, Is.EqualTo(1));
        }

        [TestCase("full")] [TestCase("name")] [TestCase("anl")]
        public void QueriesRejectUnwrittenAndNonStringNativeOutputs(string operation)
        {
            foreach (object value in new object[] { null, 0, true, new object() })
            {
                var native = new NativeRoot { Path = value };
                Assert.Throws<InvalidOperationException>(() => Read(new OSRootAdapter(native), operation));
                Assert.That(native.Calls, Is.EqualTo(1));
            }
            var unwritten = new NativeRoot { WriteOutput = false };
            Assert.Throws<InvalidOperationException>(() => Read(new OSRootAdapter(unwritten), operation));
            Assert.That(unwritten.Calls, Is.EqualTo(1));
        }

        [TestCase("full")] [TestCase("name")] [TestCase("anl")]
        public void QueriesPropagateComAndMissingSignatureFailures(string operation)
        {
            var native = new NativeRoot { Failure = new COMException("Root read failed") };
            Assert.That(Assert.Throws<COMException>(() => Read(new OSRootAdapter(native), operation)), Is.SameAs(native.Failure));
            Assert.That(native.Calls, Is.EqualTo(1));
            Assert.Throws<RuntimeBinderException>(() => Read(new OSRootAdapter(new object()), operation));
        }

        [TestCase("name")] [TestCase("anl")]
        public void DerivedPathsPropagateInvalidWindowsPathSyntax(string operation)
        {
            var native = new NativeRoot { Path = "invalid\0path.std" };
            Assert.Throws<ArgumentException>(() => Read(new OSRootAdapter(native), operation));
            Assert.That(native.Calls, Is.EqualTo(1));
        }

        [Test]
        public void WrapperRootUsesItsOwnCapturedSessionAndConstructorRejectsNull()
        {
            var first = new NativeRoot { Path = @"C:\first.std" };
            var second = new NativeRoot { Path = @"D:\second.std" };
            using (var a = new OpenStaadWrapper(first))
            using (var b = new OpenStaadWrapper(second))
            {
                Assert.That(a.Root.GetStaadFileFullPath(), Is.EqualTo(first.Path));
                Assert.That(b.Root.GetStaadFileFullPath(), Is.EqualTo(second.Path));
            }
            Assert.That(first.Calls, Is.EqualTo(1));
            Assert.That(second.Calls, Is.EqualTo(1));
            Assert.That(Assert.Throws<ArgumentNullException>(() => new OSRootAdapter(null)).ParamName, Is.EqualTo("openStaad"));
        }
    }
}
