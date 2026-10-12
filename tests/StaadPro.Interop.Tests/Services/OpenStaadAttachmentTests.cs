using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework;
using StaadPro.Interop.Helpers;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Services
{
    [TestFixture]
    public class OpenStaadAttachmentTests
    {
        private const string Target = @"C:\Models\Requested.std";
        private const string Other = @"C:\Models\Other.std";

        public class ModelRoot
        {
            public string FilePath { get; set; } = Target;
            public bool Ready { get; set; } = true;
            public bool RejectFileQuery { get; set; }
            public GeometryRoot GeometryInterface { get; } = new GeometryRoot();
            public LoadRoot LoadInterface { get; } = new LoadRoot();
            public object Geometry => Ready ? GeometryInterface : null;
            public object Load => Ready ? LoadInterface : null;

            // Match Bentley's output-VARIANT signature, including a void return.
            public void GetSTAADFile(ref object filename, bool includeFullPath)
            {
                if (RejectFileQuery) throw new COMException("STAAD is busy.");
                Assert.IsTrue(includeFullPath);
                filename = FilePath;
            }
        }

        public class GeometryRoot
        {
            public int Count { get; set; }
            public int GetNodeCount() => Count;
        }

        public class LoadRoot
        {
            public int Count { get; set; }
            public bool Busy { get; set; }
            public int GetPrimaryLoadCaseCount()
            {
                if (Busy) throw new COMException("STAAD is still loading.");
                return Count;
            }
        }

        private static OpenStaadWrapper Acquire(IList<object> initial, Func<IList<object>> discover,
            Action<string> launch, TimeSpan? timeout = null)
        {
            return OpenStaadWrapperHelpers.AcquireModel(Target, initial, discover, launch,
                timeout ?? TimeSpan.FromSeconds(1), TimeSpan.Zero);
        }

        [Test]
        public void AlreadyOpen_AttachesUsingOutputFilenameWithoutLaunchingOrPolling()
        {
            var root = new ModelRoot();
            using (var wrapper = Acquire(new object[] { root },
                () => throw new AssertionException("Ready model should not be polled."),
                _ => Assert.Fail("Already-open model must not be launched.")))
            {
                Assert.IsNotNull(wrapper);
                Assert.AreSame(root, wrapper.RawOpenStaad);
                Assert.AreSame(root.GeometryInterface, wrapper.RawGeometry);
                Assert.AreSame(root.LoadInterface, wrapper.RawLoad);
                Assert.IsNotNull(wrapper.Geometry);
                Assert.IsNotNull(wrapper.Load);
                Assert.IsTrue(wrapper.IsDedicated);
            }
        }

        [Test]
        public void MultipleOpenModels_SelectsRequestedModelAndIgnoresUnreadableCandidate()
        {
            var target = new ModelRoot();
            using (var wrapper = Acquire(new object[]
            {
                new ModelRoot { RejectFileQuery = true },
                new ModelRoot { FilePath = Other }, target
            }, () => Array.Empty<object>(), _ => Assert.Fail("Must reuse requested model.")))
                Assert.AreSame(target, wrapper.RawOpenStaad);
        }

        [Test]
        public void AlreadyOpen_NormalizesPathAndIgnoresCase()
        {
            var root = new ModelRoot { FilePath = @"c:\models\subfolder\..\REQUESTED.STD" };
            using (var wrapper = Acquire(new object[] { root }, () => Array.Empty<object>(),
                _ => Assert.Fail("Equivalent paths must reuse the model.")))
                Assert.AreSame(root, wrapper.RawOpenStaad);
        }

        [Test]
        public void AlreadyOpenButLoading_WaitsForBothInterfacesWithoutLaunching()
        {
            var root = new ModelRoot { Ready = false };
            int polls = 0;
            using (var wrapper = Acquire(new object[] { root }, () =>
            {
                if (++polls == 3) root.Ready = true;
                return new object[] { root };
            }, _ => Assert.Fail("Loading model must not be reopened.")))
            {
                Assert.AreEqual(3, polls);
                Assert.AreSame(root, wrapper.RawOpenStaad);
                Assert.IsNotNull(wrapper.Geometry);
                Assert.IsNotNull(wrapper.Load);
            }
        }

        [Test]
        public void AlreadyOpenButBusy_WaitsForReadOnlyProbeToSucceed()
        {
            var root = new ModelRoot();
            root.LoadInterface.Busy = true;
            using (var wrapper = Acquire(new object[] { root }, () =>
            {
                root.LoadInterface.Busy = false;
                return Array.Empty<object>();
            }, _ => Assert.Fail("Busy matching model must not be reopened.")))
                Assert.AreSame(root, wrapper.RawOpenStaad);
        }

        [Test]
        public void NotOpen_LaunchesOnceAndWaitsPastOtherActiveModelForRequestedModel()
        {
            var wrong = new ModelRoot { FilePath = Other };
            var target = new ModelRoot { Ready = false };
            int launches = 0;
            int polls = 0;
            using (var wrapper = Acquire(new object[] { wrong }, () =>
            {
                ++polls;
                if (polls < 3) return new object[] { wrong };
                if (polls == 4) target.Ready = true;
                return new object[] { wrong, target };
            }, path =>
            {
                Assert.AreEqual(Target, path);
                ++launches;
            }))
            {
                Assert.AreEqual(1, launches);
                Assert.AreEqual(4, polls);
                Assert.AreSame(target, wrapper.RawOpenStaad);
            }
        }

        [Test]
        public void NoRunningModels_LaunchesAndWaitsForRegistration()
        {
            var root = new ModelRoot();
            int launches = 0;
            int polls = 0;
            using (var wrapper = Acquire(Array.Empty<object>(),
                () => ++polls < 3 ? Array.Empty<object>() : new object[] { root }, _ => ++launches))
            {
                Assert.AreEqual(1, launches);
                Assert.AreEqual(3, polls);
                Assert.AreSame(root, wrapper.RawOpenStaad);
            }
        }

        [Test]
        public void Timeout_DoesNotReturnAnotherActiveModelOrLaunchAgain()
        {
            var wrong = new ModelRoot { FilePath = Other };
            int launches = 0;
            var wrapper = Acquire(new object[] { wrong }, () => new object[] { wrong },
                _ => ++launches, TimeSpan.Zero);
            Assert.IsNull(wrapper);
            Assert.AreEqual(1, launches);
        }

        [Test]
        public void TimeoutOnAlreadyOpenModel_DoesNotReturnIncompleteWrapperOrLaunch()
        {
            var root = new ModelRoot { Ready = false };
            var wrapper = Acquire(new object[] { root }, () => new object[] { root },
                _ => Assert.Fail("Must not reopen matching model."), TimeSpan.Zero);
            Assert.IsNull(wrapper);
        }

        [Test]
        public void InvalidNativeCount_DoesNotReturnReadyWrapper()
        {
            var root = new ModelRoot();
            root.GeometryInterface.Count = -1;
            var wrapper = Acquire(new object[] { root }, () => Array.Empty<object>(),
                _ => Assert.Fail("Must not reopen matching model."), TimeSpan.Zero);
            Assert.IsNull(wrapper);
        }

        [Test]
        public void InvalidCandidate_DoesNotUseWrappersRootInterfaceFallbackAsReadiness()
        {
            var wrapper = OpenStaadWrapperHelpers.WaitForReadyWrapper(null, new object[] { new object() },
                () => Array.Empty<object>(), TimeSpan.Zero, TimeSpan.Zero);
            Assert.IsNull(wrapper);
        }

        [TestCase("!{1797F52C-C5E2-4F41-B524-F5A107EA6453}", true)]
        [TestCase("!{1797f52c-c5e2-4f41-b524-f5a107ea6453}:1234", true)]
        [TestCase(@"C:\Models\Requested.STD", true)]
        [TestCase("StaadPro.OpenSTAAD", true)]
        [TestCase(@"C:\Staad\NugetTestApp.sln", false)]
        [TestCase(@"C:\Models\Requested.std.bak", false)]
        [TestCase("!{00000000-0000-0000-0000-000000000000}", false)]
        [TestCase(null, false)]
        public void RotFilter_RecognizesClassIdFilenameAndProgId(string name, bool expected)
        {
            Assert.AreEqual(expected, RotHelpers.IsOpenStaadMoniker(name,
                new Guid("1797F52C-C5E2-4F41-B524-F5A107EA6453")));
        }
    }
}
