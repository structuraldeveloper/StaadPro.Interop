using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Root;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSRootViewHandleAndQuitTests
    {
        public class NativeRoot
        {
            public readonly List<string> Calls = new List<string>();
            public object Handle = 123;
            public Exception Failure;
            public string ViewName;
            public int? CreationFlag;
            public void CreateNamedView(object viewName, object creationFlag)
            {
                Calls.Add("create-view");
                Assert.That(viewName, Is.TypeOf<string>());
                Assert.That(creationFlag, Is.TypeOf<int>());
                ViewName = (string)viewName; CreationFlag = (int)creationFlag;
                if (Failure != null) throw Failure;
            }
            public void SaveNamedView(object viewName)
            {
                Calls.Add("save-view");
                Assert.That(viewName, Is.TypeOf<string>());
                ViewName = (string)viewName;
                if (Failure != null) throw Failure;
            }
            public object GetProcessHandle()
            {
                Calls.Add("handle");
                if (Failure != null) throw Failure;
                return Handle;
            }
            public void Quit()
            {
                Calls.Add("quit");
                if (Failure != null) throw Failure;
            }
        }

        public class HelpOnlySignatures
        {
            public void CreateNamedView(object name, object flag, ref object error) => Assert.Fail("Wrong signature");
            public void SaveNamedView(object name, ref object error) => Assert.Fail("Wrong signature");
        }

        private static IEnumerable<object[]> HandleValues()
        {
            yield return new object[] { (sbyte)-1, ulong.MaxValue };
            yield return new object[] { (byte)1, 1UL };
            yield return new object[] { (short)-2, unchecked((ulong)-2L) };
            yield return new object[] { (ushort)3, 3UL };
            yield return new object[] { 0, 0UL };
            yield return new object[] { int.MinValue, unchecked((ulong)(long)int.MinValue) };
            yield return new object[] { uint.MaxValue, (ulong)uint.MaxValue };
            yield return new object[] { long.MinValue, unchecked((ulong)long.MinValue) };
            yield return new object[] { long.MaxValue, (ulong)long.MaxValue };
            yield return new object[] { ulong.MaxValue, ulong.MaxValue };
            yield return new object[] { new IntPtr(-1), ulong.MaxValue };
            yield return new object[] { new UIntPtr(27U), 27UL };
            if (IntPtr.Size == 8)
                yield return new object[] { new UIntPtr(ulong.MaxValue), ulong.MaxValue };
        }

        [TestCaseSource(nameof(HandleValues))]
        public void ProcessHandlePreservesNumericBitPatternsWithoutPointerOwnership(object nativeValue, ulong expected)
        {
            var native = new NativeRoot { Handle = nativeValue };
            var value = new OSRootAdapter(native).GetProcessHandle();
            Assert.That(value.RawValue, Is.EqualTo(expected));
            Assert.That(native.Calls, Is.EqualTo(new[] { "handle" }));
        }

        [TestCase(null)] [TestCase(true)] [TestCase(false)] [TestCase("123")]
        [TestCase(123d)] [TestCase(double.NaN)] [TestCase('a')]
        public void NonIntegralHandlesAreNotCoercedOrReportedAsValid(object value)
        {
            var native = new NativeRoot { Handle = value };
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).GetProcessHandle());
            Assert.That(native.Calls, Is.EqualTo(new[] { "handle" }));
        }

        [Test]
        public void UnsupportedBoxedHandleShapesAreRejected()
        {
            foreach (object value in new object[] { 1m, DBNull.Value, DayOfWeek.Monday, new object() })
                Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(new NativeRoot { Handle = value }).GetProcessHandle());
        }

        [Test]
        public void OpaqueHandleIsImmutableIndependentDataWithNoOwnershipApi()
        {
            var native = new NativeRoot(); var root = new OSRootAdapter(native);
            var first = root.GetProcessHandle();
            native.Handle = 456;
            var second = root.GetProcessHandle();
            Assert.That(first.RawValue, Is.EqualTo(123UL));
            Assert.That(second.RawValue, Is.EqualTo(456UL));
            Assert.That(first, Is.Not.SameAs(second));
            var type = typeof(NativeProcessHandleValue);
            Assert.That(type.IsSealed, Is.True);
            Assert.That(type.GetConstructors(), Is.Empty);
            Assert.That(type.GetProperties().Any(p => p.CanWrite), Is.False);
            Assert.That(typeof(IDisposable).IsAssignableFrom(type), Is.False);
            Assert.That(type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .Where(m => !m.IsSpecialName), Is.Empty);
            Assert.That(type.GetMethods().Any(m => m.Name == "op_Implicit" || m.Name == "op_Explicit"), Is.False);
        }

        [TestCase(int.MinValue)] [TestCase(-1)] [TestCase(0)] [TestCase(1)] [TestCase(int.MaxValue)]
        public void ExplicitCreationFlagsAreSentAsInt32WithoutInventedMeanings(int flag)
        {
            var native = new NativeRoot();
            new OSRootAdapter(native).CreateNamedView("  \u96ea view  ", flag);
            Assert.That(native.ViewName, Is.EqualTo("  \u96ea view  "));
            Assert.That(native.CreationFlag, Is.EqualTo(flag));
            Assert.That(native.Calls, Is.EqualTo(new[] { "create-view" }));
        }

        [Test]
        public void SaveUsesOnlyTheVerifiedSingleInputNativeSignature()
        {
            var native = new NativeRoot();
            new OSRootAdapter(native).SaveNamedView("  \u96ea view  ");
            Assert.That(native.ViewName, Is.EqualTo("  \u96ea view  "));
            Assert.That(native.CreationFlag, Is.Null);
            Assert.That(native.Calls, Is.EqualTo(new[] { "save-view" }));
        }

        [TestCase(null, true)] [TestCase("", true)] [TestCase(" \t", true)]
        [TestCase(null, false)] [TestCase("", false)] [TestCase(" \t", false)]
        public void InvalidViewNamesAreRejectedBeforeNativeAccess(string name, bool create)
        {
            var native = new NativeRoot(); var root = new OSRootAdapter(native);
            var error = Assert.Catch<ArgumentException>(() =>
            {
                if (create) root.CreateNamedView(name, 1); else root.SaveNamedView(name);
            });
            Assert.That(error.ParamName, Is.EqualTo("viewName"));
            if (name == null) Assert.That(error, Is.TypeOf<ArgumentNullException>());
            Assert.That(native.Calls, Is.Empty);
        }

        [Test]
        public void QuitDispatchesOnlyOneNoArgumentRequest()
        {
            var native = new NativeRoot();
            new OSRootAdapter(native).Quit();
            Assert.That(native.Calls, Is.EqualTo(new[] { "quit" }));
        }

        [TestCase("create-view")] [TestCase("save-view")] [TestCase("handle")] [TestCase("quit")]
        public void ExceptionsPropagateByIdentityWithoutFallbackRetryOrRollback(string operation)
        {
            var failure = new COMException("Request failed", unchecked((int)0x80010108));
            var native = new NativeRoot { Failure = failure };
            Assert.That(Assert.Throws<COMException>(() => Invoke(new OSRootAdapter(native), operation)), Is.SameAs(failure));
            Assert.That(native.Calls, Is.EqualTo(new[] { operation }));
            if (operation == "create-view" || operation == "save-view") Assert.That(native.ViewName, Is.EqualTo("Name"));
        }

        [TestCase("create-view")] [TestCase("save-view")] [TestCase("handle")] [TestCase("quit")]
        public void MissingMembersRemainBindingErrors(string operation)
        {
            Assert.Throws<RuntimeBinderException>(() => Invoke(new OSRootAdapter(new object()), operation));
        }

        [TestCase("create-view")] [TestCase("save-view")]
        public void InconsistentHelpSignaturesAreNotRetriedAsAlternativeNativeMutations(string operation)
        {
            Assert.Throws<RuntimeBinderException>(() => Invoke(new OSRootAdapter(new HelpOnlySignatures()), operation));
        }

        [Test]
        public void StandaloneAndWrapperCallsStayBoundAndDisposalDoesNotQuit()
        {
            var first = new NativeRoot { Handle = 11 }; var second = new NativeRoot { Handle = 22 };
            using (var wrapper = new OpenStaadWrapper(first))
            {
                first.Calls.Clear();
                var direct = new OSRootAdapter(second);
                Assert.That(second.Calls, Is.Empty, "Construction does not probe the native server.");
                Assert.That(wrapper.Root.GetProcessHandle().RawValue, Is.EqualTo(11UL));
                Assert.That(direct.GetProcessHandle().RawValue, Is.EqualTo(22UL));
                wrapper.Root.CreateNamedView("First", 1); direct.SaveNamedView("Second");
                wrapper.Root.Quit();
                Assert.That(ReferenceEquals((object)wrapper.Root.ComObject, first), Is.True);
                Assert.That(first.Calls, Is.EqualTo(new[] { "handle", "create-view", "quit" }));
                Assert.That(second.Calls, Is.EqualTo(new[] { "handle", "save-view" }));
            }
            Assert.That(first.Calls.Count(c => c == "quit"), Is.EqualTo(1));
            Assert.That(second.Calls, Does.Not.Contain("quit"));
        }

        [Test]
        public void PublicContractsHaveNoReferenceOutputsAndPreserveVerifiedVoidSignatures()
        {
            foreach (var type in new[] { typeof(IOSRoot), typeof(OSRootAdapter) })
            {
                foreach (string name in new[] { "NewSTAADFile", "CreateNamedView", "SaveNamedView", "Quit" })
                    Assert.That(type.GetMethod(name).ReturnType, Is.EqualTo(typeof(void)));
                Assert.That(type.GetMethod("CreateNamedView").GetParameters().Any(p => p.IsOptional), Is.False);
                Assert.That(type.GetMethods().SelectMany(m => m.GetParameters()).Any(p => p.ParameterType.IsByRef || p.IsOut), Is.False);
            }
            var entries = XDocument.Load(Path.ChangeExtension(typeof(IOSRoot).Assembly.Location, ".xml"))
                .Descendants("member").ToDictionary(m => (string)m.Attribute("name"));
            Assert.That(entries["T:" + typeof(NativeProcessHandleValue).FullName].Element("summary")?.Value, Is.Not.Null.And.Not.Empty);
            Assert.That(entries["P:" + typeof(NativeProcessHandleValue).FullName + ".RawValue"].Element("value")?.Value, Is.Not.Null.And.Not.Empty);
        }

        private static void Invoke(IOSRoot root, string operation)
        {
            if (operation == "create-view") root.CreateNamedView("Name", 1);
            else if (operation == "save-view") root.SaveNamedView("Name");
            else if (operation == "handle") root.GetProcessHandle();
            else root.Quit();
        }
    }
}
