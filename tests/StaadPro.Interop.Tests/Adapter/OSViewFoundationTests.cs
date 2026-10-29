using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Linq;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums.View;
using StaadPro.Interop.Models;
using Native = PackageVerification.ViewFoundationFixture.Native;
using Root = PackageVerification.ViewFoundationFixture.Root;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSViewFoundationTests
    {
        [TestCase(24)] [TestCase(25)] [TestCase(26)]
        public void ShowFrontUsesCapturedViewAndCallerThreadWithoutInventedAcknowledgement(int version)
        {
            var native = new Native();
            using (var wrapper = new OpenStaadWrapper(new Root { View = native, Major = version }))
            {
                Assert.That(native.Calls, Is.Empty);
                var result = wrapper.View.ShowFront();
                Assert.That(native.Calls, Is.EqualTo(new[] { "ShowFront" }));
                Assert.That(native.Threads.Single(), Is.EqualTo(Thread.CurrentThread.ManagedThreadId));
                Assert.That(result.NativeFunction, Is.EqualTo("ShowFront"));
                Assert.That(result.Status, Is.EqualTo(ViewOperationStatus.InvokedWithoutAcknowledgement));
                Assert.That(result.IsSuccessful, Is.False); Assert.That(result.NativeReturnCode, Is.Null);
            }
        }

        [Test]
        public void OldViewBindingsRejectReplacementAndDisposal()
        {
            var native = new Native(); var root = new Root { View = native }; var wrapper = new OpenStaadWrapper(root); var old = wrapper.View;
            root.View = new Native(); wrapper.RefreshComObjects();
            Assert.Throws<InvalidOperationException>(() => old.ShowFront()); Assert.That(native.Calls, Is.Empty);
            var current = wrapper.View; wrapper.Dispose();
            Assert.Throws<InvalidOperationException>(() => current.ShowFront());
        }

        [Test]
        public void PostCallReplacementThrowsWithoutRetry()
        {
            var native = new Native(); var root = new Root { View = native };
            using (var wrapper = new OpenStaadWrapper(root))
            {
                var api = wrapper.View; native.OnCall = () => { root.View = new Native(); wrapper.RefreshComObjects(); };
                Assert.Throws<InvalidOperationException>(() => api.ShowFront()); Assert.That(native.Calls.Count, Is.EqualTo(1));
            }
        }

        [Test]
        public void OriginalNativeErrorPropagatesWithoutAnotherInstanceOrRetry()
        {
            var error = new COMException("native UI error"); var native = new Native { OnCall = () => throw error };
            using (var wrapper = new OpenStaadWrapper(new Root { View = native }))
            { Assert.That(Assert.Throws<COMException>(() => wrapper.View.ShowFront()), Is.SameAs(error)); Assert.That(native.Calls.Count, Is.EqualTo(1)); }
        }

        [Test]
        public void ShowFrontHasMatchingComparativeXmlAndNoOtherViewPortIsClaimed()
        {
            Assert.That(typeof(IOSView).GetMethods().Count(m => !m.IsSpecialName), Is.EqualTo(1));
            var xml = XDocument.Load(Path.ChangeExtension(typeof(IOSView).Assembly.Location, ".xml"));
            var a = xml.Descendants("member").Single(m => (string)m.Attribute("name") == "M:" + typeof(IOSView).FullName + ".ShowFront");
            var b = xml.Descendants("member").Single(m => (string)m.Attribute("name") == "M:" + typeof(OSViewAdapter).FullName + ".ShowFront");
            Assert.That(a.Elements().Select(x => x.ToString()), Is.EqualTo(b.Elements().Select(x => x.ToString())));
            foreach (string tag in new[] { "summary", "returns", "remarks", "example" }) Assert.That(a.Element(tag)?.Value, Is.Not.Null.And.Not.Empty);
            Assert.That(a.Element("remarks").Value, Does.Contain("Unlike Root"));
        }

        [Test]
        public void NullViewBindingsAreRejected()
        {
            Assert.Throws<ArgumentNullException>(() => new OSViewAdapter(null));
            using (var wrapper = new OpenStaadWrapper(new Root())) Assert.Throws<ArgumentNullException>(() => new OSViewAdapter(wrapper));
        }
    }
}
