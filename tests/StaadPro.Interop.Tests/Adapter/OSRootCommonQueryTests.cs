using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSRootCommonQueryTests
    {
        public class NativeRoot
        {
            public object Folder = @"C:\Models\雪\", Physical = 0, Message = "Native error\r\nDetails";
            public bool WriteFolder = true;
            public Exception Failure;
            public readonly List<string> Calls = new List<string>();

            public void GetSTAADFileFolder(ref object folder)
            {
                Calls.Add("folder");
                Assert.That(folder, Is.TypeOf<string>());
                Assert.That((string)folder, Does.Contain("UnwrittenFolder"));
                if (Failure != null) throw Failure;
                if (WriteFolder) folder = Folder;
            }

            public object IsPhysicalModel()
            {
                Calls.Add("physical");
                if (Failure != null) throw Failure;
                return Physical;
            }

            public object GetErrorMessage()
            {
                Calls.Add("message");
                if (Failure != null) throw Failure;
                return Message;
            }
        }

        [TestCase(@"C:\Models\雪\")]
        [TestCase(@"\\server\share\模型")]
        [TestCase(@"relative\folder/")]
        [TestCase("  unnormalized folder  ")]
        [TestCase("")]
        public void FolderReadPreservesTheNativeTextWithoutFilenameOrFilesystemQueries(string folder)
        {
            var native = new NativeRoot { Folder = folder };
            Assert.That(new OSRootAdapter(native).GetStaadFileFolder(), Is.EqualTo(folder));
            Assert.That(native.Calls, Is.EqualTo(new[] { "folder" }));
        }

        [TestCase(null)] [TestCase(42)] [TestCase(true)]
        public void FolderReadRejectsMalformedOutputs(object value)
        {
            var native = new NativeRoot { Folder = value };
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).GetStaadFileFolder());
            Assert.That(native.Calls, Is.EqualTo(new[] { "folder" }));
        }

        [Test]
        public void FolderReadRejectsUnwrittenAndDatabaseNullOutputs()
        {
            var native = new NativeRoot { WriteFolder = false };
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).GetStaadFileFolder());
            Assert.That(native.Calls, Is.EqualTo(new[] { "folder" }));
            native = new NativeRoot { Folder = DBNull.Value };
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).GetStaadFileFolder());
        }

        [TestCase(false, false)] [TestCase(true, true)]
        [TestCase(0, false)] [TestCase(1, true)]
        public void PhysicalPredicateAcceptsBooleanAndIntegralFlags(object value, bool expected)
        {
            var native = new NativeRoot { Physical = value };
            Assert.That(new OSRootAdapter(native).IsPhysicalModel(), Is.EqualTo(expected));
            Assert.That(native.Calls, Is.EqualTo(new[] { "physical" }));
        }

        [Test]
        public void PhysicalPredicateAcceptsIntegralRepresentations()
        {
            foreach (object value in new object[] { (sbyte)1, (byte)1, (short)1, (ushort)1, 1, 1U, 1L, 1UL })
                Assert.That(new OSRootAdapter(new NativeRoot { Physical = value }).IsPhysicalModel(), Is.True);
        }

        [TestCase(null)] [TestCase(-1)] [TestCase(2)] [TestCase("1")]
        [TestCase(1d)] [TestCase(0.5d)] [TestCase(double.NaN)]
        [TestCase(long.MaxValue)] [TestCase(ulong.MaxValue)]
        public void PhysicalPredicateRejectsMalformedAndUnknownFlags(object value)
        {
            var native = new NativeRoot { Physical = value };
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).IsPhysicalModel());
            Assert.That(native.Calls, Is.EqualTo(new[] { "physical" }));
        }

        [Test]
        public void PhysicalPredicateRejectsDecimalAndDatabaseNull()
        {
            foreach (object value in new object[] { 1m, DBNull.Value })
                Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(new NativeRoot { Physical = value }).IsPhysicalModel());
        }

        [TestCase("License unavailable\r\n雪 details")]
        [TestCase("  exact text  ")]
        [TestCase("")]
        public void ErrorReadPreservesTextIncludingWhitespaceAndEmptyMessages(string message)
        {
            var native = new NativeRoot { Message = message };
            Assert.That(new OSRootAdapter(native).GetErrorMessage(), Is.EqualTo(message));
            Assert.That(native.Calls, Is.EqualTo(new[] { "message" }));
        }

        [TestCase(null)] [TestCase(0)] [TestCase(true)]
        public void ErrorReadRejectsNonStringValuesInsteadOfInventingText(object value)
        {
            var native = new NativeRoot { Message = value };
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).GetErrorMessage());
            Assert.That(native.Calls, Is.EqualTo(new[] { "message" }));
        }

        [Test]
        public void ErrorReadRejectsDatabaseNull()
        {
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(new NativeRoot { Message = DBNull.Value }).GetErrorMessage());
        }

        [TestCase("folder")] [TestCase("physical")] [TestCase("message")]
        public void NativeExceptionsPropagateUnchangedWithoutRetryOrAdditionalDiagnostics(string operation)
        {
            var failure = new COMException("Disconnected", unchecked((int)0x80010108));
            var native = new NativeRoot { Failure = failure };
            Assert.That(Assert.Throws<COMException>(() => Read(new OSRootAdapter(native), operation)), Is.SameAs(failure));
            Assert.That(native.Calls, Is.EqualTo(new[] { operation }));
        }

        [TestCase("folder")] [TestCase("physical")] [TestCase("message")]
        public void MissingSignaturesRemainDispatchFailures(string operation)
        {
            Assert.Throws<RuntimeBinderException>(() => Read(new OSRootAdapter(new object()), operation));
        }

        [Test]
        public void ConstructorIsCaptureOnlyAndSubsequentReadsRemainLiveAndIsolated()
        {
            var first = new NativeRoot { Folder = "first", Physical = 1, Message = "first error" };
            var second = new NativeRoot { Folder = "second", Physical = 0, Message = "second error" };
            var left = new OSRootAdapter(first); var right = new OSRootAdapter(second);
            Assert.That(first.Calls.Concat(second.Calls), Is.Empty);
            Assert.That(left.GetStaadFileFolder(), Is.EqualTo("first"));
            Assert.That(right.GetStaadFileFolder(), Is.EqualTo("second"));
            Assert.That(left.IsPhysicalModel(), Is.True);
            Assert.That(right.IsPhysicalModel(), Is.False);
            Assert.That(left.GetErrorMessage(), Is.EqualTo("first error"));
            Assert.That(right.GetErrorMessage(), Is.EqualTo("second error"));
            first.Folder = "changed"; first.Physical = 0; first.Message = "changed error";
            Assert.That(left.GetStaadFileFolder(), Is.EqualTo("changed"));
            Assert.That(left.IsPhysicalModel(), Is.False);
            Assert.That(left.GetErrorMessage(), Is.EqualTo("changed error"));
            Assert.That(second.Calls, Is.EqualTo(new[] { "folder", "physical", "message" }));
        }

        [Test]
        public void WrapperOwnedQueriesUseTheBoundRootWithoutMetadataRefresh()
        {
            var native = new NativeRoot();
            using (var wrapper = new OpenStaadWrapper(native))
            {
                native.Calls.Clear();
                Assert.That(wrapper.Root.GetStaadFileFolder(), Is.EqualTo(native.Folder));
                Assert.That(wrapper.Root.IsPhysicalModel(), Is.False);
                Assert.That(wrapper.Root.GetErrorMessage(), Is.EqualTo(native.Message));
                Assert.That(native.Calls, Is.EqualTo(new[] { "folder", "physical", "message" }));
            }
        }

        [Test]
        public void PublicContractsHaveNoReferenceParametersAndHelpersRemainInternal()
        {
            var assembly = typeof(IOSRoot).Assembly;
            foreach (string name in new[] { "GetStaadFileFolder", "IsPhysicalModel", "GetErrorMessage" })
                Assert.That(typeof(IOSRoot).GetMethod(name).GetParameters(), Is.Empty);
            Assert.That(assembly.GetExportedTypes().Any(t => t.Namespace == "StaadPro.Interop.Services.Interop" ||
                t.Namespace == "StaadPro.Interop.Services.Root.Common"), Is.False);
        }

        private static void Read(IOSRoot root, string operation)
        {
            switch (operation)
            {
                case "folder": root.GetStaadFileFolder(); break;
                case "physical": root.IsPhysicalModel(); break;
                default: root.GetErrorMessage(); break;
            }
        }
    }
}
