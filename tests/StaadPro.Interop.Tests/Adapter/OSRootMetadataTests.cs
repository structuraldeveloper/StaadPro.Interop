using System;
using System.Linq;
using System.Runtime.InteropServices;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;
using StaadPro.Interop.Tests.Models;
using NativeRoot = StaadPro.Interop.Tests.Models.OpenStaadWrapperParityTests.NativeRoot;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSRootMetadataTests
    {
        [Test]
        public void WrapperQueriesReuseSnapshotsWithoutNativeDispatch()
        {
            var native = new NativeRoot();
            using (var wrapper = new OpenStaadWrapper(native))
            {
                native.Queries.Clear();
                native.FailVersion = true;
                native.FailUnits = true;
                Assert.Multiple(() =>
                {
                    Assert.That(wrapper.Root.GetApplicationVersion().Display, Is.EqualTo(wrapper.StaadVersion.Display));
                    Assert.That(wrapper.Root.GetBaseUnit(), Is.EqualTo(wrapper.BaseUnitSystem));
                    Assert.That(wrapper.Root.GetInputUnitForForce(), Is.EqualTo(wrapper.InputForceUnit));
                    Assert.That(wrapper.Root.GetInputUnitForLength(), Is.EqualTo(wrapper.InputLengthUnit));
                    Assert.That(wrapper.Root.GetMainWindowHandle(), Is.EqualTo(wrapper.StaadApplicationWindowHandle));
                    Assert.That(native.Queries, Is.Empty);
                });
            }
        }

        [Test]
        public void WrapperUnitsStayCachedUntilExplicitRefreshAndKeepAdapterIdentity()
        {
            var native = new NativeRoot();
            using (var wrapper = new OpenStaadWrapper(native))
            {
                IOSRoot root = wrapper.Root;
                native.BaseUnit = 1; native.ForceText = "lb"; native.LengthText = "ft";
                Assert.That(root.GetBaseUnit(), Is.EqualTo(BaseUnitSystem.Metric));
                Assert.That(root.GetInputUnitForForce(), Is.EqualTo(ForceInputUnit.KiloNewton));
                wrapper.RefreshUnits();
                Assert.That(wrapper.Root, Is.SameAs(root));
                Assert.That(root.GetBaseUnit(), Is.EqualTo(BaseUnitSystem.Imperial));
                Assert.That(root.GetInputUnitForForce(), Is.EqualTo(ForceInputUnit.Pound));
                Assert.That(root.GetInputUnitForLength(), Is.EqualTo(LengthInputUnit.Feet));
            }
        }

        [Test]
        public void NativeVersionIsDefensivelyCopiedAndRefreshUpdatesTheSnapshot()
        {
            var native = new NativeRoot();
            using (var wrapper = new OpenStaadWrapper(native))
            {
                var root = wrapper.Root;
                var version = root.GetApplicationVersion();
                version.Major = 99;
                Assert.That(root.GetApplicationVersion().Major, Is.EqualTo(26));
                native.VersionText = "25.00.01.424"; native.Handle = 999;
                Assert.That(root.GetApplicationVersion().Major, Is.EqualTo(26));
                wrapper.RefreshComObjects();
                Assert.That(wrapper.Root, Is.Not.SameAs(root));
                Assert.That(wrapper.Root.GetApplicationVersion().Major, Is.EqualTo(25));
                Assert.That(wrapper.Root.GetMainWindowHandle(), Is.EqualTo(new IntPtr(999)));
            }
        }

        [Test]
        public void WindowProjectionUsesTheConfiguredWrapperService()
        {
            var native = new NativeRoot();
            var windows = new OpenStaadWrapperParityTests.WindowService();
            using (var wrapper = new OpenStaadWrapper(native, false, windows, new OpenStaadWrapperParityTests.Interaction()))
            {
                native.Queries.Clear();
                Assert.That(wrapper.Root.GetMainWindowHandle(), Is.EqualTo(new IntPtr(987)));
                Assert.That(windows.Calls, Is.EqualTo(1));
                Assert.That(native.Queries, Is.Empty);
            }
        }

        [TestCase("version")]
        [TestCase("base")]
        [TestCase("force")]
        [TestCase("length")]
        [TestCase("window")]
        public void CapturedMetadataQueriesRejectDisposedWrappers(string query)
        {
            var wrapper = new OpenStaadWrapper(new NativeRoot());
            IOSRoot root = wrapper.Root;
            wrapper.Dispose();
            Assert.Throws<ObjectDisposedException>(() => Query(root, query));
        }

        [Test]
        public void StandaloneConstructorCapturesOnlyAndQueriesRemainFresh()
        {
            var native = new NativeRoot();
            var root = new OSRootAdapter(native);
            Assert.That(native.Queries, Is.Empty);
            Assert.That(root.GetApplicationVersion().Major, Is.EqualTo(26));
            Assert.That(native.Queries, Is.EqualTo(new[] { "version" }));
            native.VersionText = "25.00.01.424";
            Assert.That(root.GetApplicationVersion().Major, Is.EqualTo(25));
            native.BaseUnit = 1;
            Assert.That(root.GetBaseUnit(), Is.EqualTo(BaseUnitSystem.Imperial));
            native.BaseUnit = 2;
            Assert.That(root.GetBaseUnit(), Is.EqualTo(BaseUnitSystem.Metric));
        }

        [TestCase("force")]
        [TestCase("length")]
        public void StandaloneUnitQueriesReadOnlyTheirOwnUnitAndNormalizeNativeOutput(string query)
        {
            var native = new NativeRoot { ForceText = "lb", LengthText = "ft" };
            var root = new OSRootAdapter(native);
            if (query == "force") Assert.That(root.GetInputUnitForForce(), Is.EqualTo(ForceInputUnit.Pound));
            else Assert.That(root.GetInputUnitForLength(), Is.EqualTo(LengthInputUnit.Feet));
            Assert.That(native.Queries, Is.EqualTo(new[] { "version", "base", query }));
        }

        [TestCase(24, LengthInputUnit.Meter)]
        [TestCase(25, LengthInputUnit.Inch)]
        [TestCase(26, LengthInputUnit.Inch)]
        public void StandaloneUnitReturnUsesTheSharedVersionRule(int major, LengthInputUnit expected)
        {
            var native = new NativeRoot { VersionText = major + ".0.0.1", LengthText = "", LengthReturn = 0 };
            Assert.That(new OSRootAdapter(native).GetInputUnitForLength(), Is.EqualTo(expected));
        }

        [Test]
        public void StandaloneUnitFailuresRetainDocumentedCompatibilityFallbacks()
        {
            var native = new NativeRoot { FailUnits = true, BaseUnit = 1 };
            var root = new OSRootAdapter(native);
            Assert.That(root.GetInputUnitForForce(), Is.EqualTo(ForceInputUnit.KiloPound));
            Assert.That(root.GetInputUnitForLength(), Is.EqualTo(LengthInputUnit.Feet));
            native.BaseUnit = 2;
            Assert.That(root.GetInputUnitForForce(), Is.EqualTo(ForceInputUnit.KiloNewton));
            Assert.That(root.GetInputUnitForLength(), Is.EqualTo(LengthInputUnit.Meter));
        }

        [Test]
        public void TwoBindingsNeverMixInstancesOrRefreshAnotherWrapper()
        {
            var first = new NativeRoot { VersionText = "25.0.1.424", BaseUnit = 1, ForceText = "lb", Handle = 111 };
            var second = new NativeRoot { Handle = 222 };
            using (var left = new OpenStaadWrapper(first))
            using (var right = new OpenStaadWrapper(second))
            {
                first.Queries.Clear(); second.Queries.Clear();
                Assert.That(left.Root.GetApplicationVersion().Major, Is.EqualTo(25));
                Assert.That(right.Root.GetApplicationVersion().Major, Is.EqualTo(26));
                Assert.That(left.Root.GetMainWindowHandle(), Is.EqualTo(new IntPtr(111)));
                Assert.That(right.Root.GetMainWindowHandle(), Is.EqualTo(new IntPtr(222)));
                Assert.That(left.Root.GetInputUnitForForce(), Is.EqualTo(ForceInputUnit.Pound));
                Assert.That(right.Root.GetInputUnitForForce(), Is.EqualTo(ForceInputUnit.KiloNewton));
                Assert.That(first.Queries.Concat(second.Queries), Is.Empty);
                first.BaseUnit = 2; left.RefreshUnits();
                Assert.That(second.Queries, Is.Empty);
            }
        }

        [TestCase(20)]
        [TestCase(24)]
        [TestCase(26)]
        public void StandaloneWindowReadUsesTheSameRootAndTheDocumentedFamilyRule(int major)
        {
            var native = new NativeRoot { VersionText = major + ".0.0.1", Handle = 321 };
            var actual = new OSRootAdapter(native).GetMainWindowHandle();
            Assert.That(actual, Is.EqualTo(major <= 20 ? IntPtr.Zero : new IntPtr(321)));
            Assert.That(native.Queries, Is.EqualTo(major <= 20 ? new[] { "version" } : new[] { "version", "window" }));
        }

        [Test]
        public void MissingMembersAndUnknownBaseCodesStayExplicit()
        {
            var root = new OSRootAdapter(new object());
            Assert.That(root.GetApplicationVersion(), Is.Null);
            Assert.That(root.GetBaseUnit(), Is.EqualTo(BaseUnitSystem.Unknown));
            Assert.That(root.GetMainWindowHandle(), Is.EqualTo(IntPtr.Zero));
            Assert.That(new OSRootAdapter(new NativeRoot { BaseUnit = 77 }).GetBaseUnit(), Is.EqualTo((BaseUnitSystem)77));
        }

        [Test]
        public void StandaloneVersionFailuresPropagateAndContradictoryDataIsRejected()
        {
            Assert.Throws<COMException>(() => new OSRootAdapter(new NativeRoot { FailVersion = true }).GetApplicationVersion());
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(new NativeRoot { ContradictVersion = true }).GetApplicationVersion());
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(new NativeRoot { BaseUnit = true }).GetBaseUnit());
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(new NativeRoot { Handle = true }).GetMainWindowHandle());
        }

        private static void Query(IOSRoot root, string query)
        {
            switch (query)
            {
                case "version": root.GetApplicationVersion(); break;
                case "base": root.GetBaseUnit(); break;
                case "force": root.GetInputUnitForForce(); break;
                case "length": root.GetInputUnitForLength(); break;
                default: root.GetMainWindowHandle(); break;
            }
        }
    }
}
