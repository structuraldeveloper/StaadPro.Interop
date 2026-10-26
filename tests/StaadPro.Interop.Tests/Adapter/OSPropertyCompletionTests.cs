using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Linq;
using NUnit.Framework;
using PackageVerification;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Property;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSPropertyCompletionTests
    {
        public sealed class Native : PropertyCompletionFixture.Native { }
        private static readonly string[] Names = { "RemoveAllElementNodeReleaseSpec", "RemoveMemberFireProofingSpecFromBeam", "GetPublishedProfileName", "GetStaadProfileName" };
        private static readonly string[] NativeNames = { "RemoveAllElementNodeReleaseSpec", "RemoveMemberFireProofingSpecFromBeam", "GetPublishedProfileName", "GetSTAADProfileName" };
        private static OpenStaadWrapper Wrap(Native n) => new OpenStaadWrapper(new PropertyCleanupFixture.Root { Property = n });
        private static object[] Args(string name) => name == Names[0] ? new object[0] : name == Names[1] ? new object[] { 17 } : new object[] { " W12X26 ", Country.American };
        private static object Invoke(object receiver, string name, object[] args = null)
        {
            try { return receiver.GetType().GetMethod(name).Invoke(receiver, args ?? Args(name)); }
            catch (TargetInvocationException e) { ExceptionDispatchInfo.Capture(e.InnerException).Throw(); throw; }
        }
        private static void Respond(Native n, string op, object value) => n.Responses[op] = new Queue<object>(new[] { value });
        [TestCaseSource(nameof(Names))]
        public void EveryContractHasMatchingInterfaceAndConcreteXml(string name)
        {
            var method = typeof(IOSProperty).GetMethod(name); var concrete = typeof(OSPropertyAdapter).GetMethod(name);
            Assert.That(concrete.ReturnType, Is.EqualTo(method.ReturnType));
            Assert.That(concrete.GetParameters().Select(p => p.Name), Is.EqualTo(method.GetParameters().Select(p => p.Name)));
            Assert.That(method.GetParameters().Any(p => p.IsOut || p.ParameterType.IsByRef || p.ParameterType == typeof(object)), Is.False);
            var xml = XDocument.Load(System.IO.Path.ChangeExtension(typeof(IOSProperty).Assembly.Location, ".xml"));
            foreach (var owner in new[] { typeof(IOSProperty), typeof(OSPropertyAdapter) })
            {
                var prefix = "M:" + owner.FullName + "." + name;
                var entry = xml.Descendants("member").Single(m => ((string)m.Attribute("name")).StartsWith(prefix, StringComparison.Ordinal));
                foreach (var tag in new[] { "summary", "returns", "remarks", "example" }) Assert.That(entry.Element(tag)?.Value, Is.Not.Null.And.Not.Empty);
                Assert.That(entry.Element("remarks").Value, Does.Contain("When to use:").And.Contain("COM apartment"));
                Assert.That(entry.Elements("param").Select(p => (string)p.Attribute("name")), Is.EqualTo(method.GetParameters().Select(p => p.Name)));
            }
        }
        [TestCaseSource(nameof(Names))]
        public void EveryRouteCallsItsExactNativeMemberOnceOnTheCallerThread(string name)
        {
            var n = new Native(); using (var w = Wrap(n))
            {
                var result = Invoke(w.Property, name);
                Assert.That(n.Calls, Is.EqualTo(new[] { NativeNames[Array.IndexOf(Names, name)] }));
                Assert.That(n.Threads, Is.EqualTo(new[] { Thread.CurrentThread.ManagedThreadId }));
                if (name == Names[1]) Assert.That(((PropertyMutationResult)result).TargetIds, Is.EqualTo(new[] { 17 }));
                if (name.StartsWith("Get", StringComparison.Ordinal)) Assert.That(n.Arguments.Single(), Is.EqualTo(new object[] { " W12X26 ", 1 }));
            }
        }
        [TestCaseSource(nameof(Names))]
        public void EveryRouteRejectsDisposedReceiversBeforeNativeAccess(string name)
        { var n = new Native(); var w = Wrap(n); var p = w.Property; w.Dispose(); Assert.Throws<InvalidOperationException>(() => Invoke(p, name)); Assert.That(n.Calls, Is.Empty); }
        [TestCaseSource(nameof(Names))]
        public void EveryRouteDetectsReboundReceiversAfterOneInvocation(string name)
        {
            var n = new Native(); var root = new PropertyCleanupFixture.Root { Property = n };
            using (var w = new OpenStaadWrapper(root)) { var p = w.Property; n.OnCall = op => { n.OnCall = null; root.Property = new Native(); w.RefreshComObjects(); }; Assert.Throws<InvalidOperationException>(() => Invoke(p, name)); Assert.That(n.Calls.Count, Is.EqualTo(1)); }
        }
        [TestCaseSource(nameof(Names))]
        public void EveryRoutePreservesTransportErrorsWithoutRetry(string name)
        { var n = new Native(); var error = new COMException("Transport failed"); n.OnCall = op => throw error; using (var w = Wrap(n)) Assert.That(Assert.Throws<COMException>(() => Invoke(w.Property, name)), Is.SameAs(error)); Assert.That(n.Calls.Count, Is.EqualTo(1)); }
        [TestCase("RemoveAllElementNodeReleaseSpec")]
        [TestCase("RemoveMemberFireProofingSpecFromBeam")]
        public void MutationsKeepBooleanAndIntegralEvidenceSeparate(string name)
        {
            foreach (object response in new object[] { true, false, 1, 0, (short)1, 0L })
            {
                var n = new Native(); Respond(n, name, response);
                using (var w = Wrap(n))
                {
                    var r = (PropertyMutationResult)Invoke(w.Property, name);
                    bool success = response is bool b ? b : Convert.ToInt64(response) == 1;
                    var expected = success ? PropertyMutationStatus.Succeeded : name == Names[0] ? PropertyMutationStatus.NoSpecification : PropertyMutationStatus.NativeFailure;
                    Assert.That(r.Status, Is.EqualTo(expected)); Assert.That(r.IsSuccessful, Is.EqualTo(success));
                    Assert.That(r.NativeBooleanReturn, Is.EqualTo(response is bool flag ? (bool?)flag : null));
                    Assert.That(r.NativeReturnCode, Is.EqualTo(response is bool ? (int?)null : Convert.ToInt32(response)));
                    Assert.That(r.TargetKind, Is.EqualTo(name == Names[0] ? PropertyMutationTargetKind.Model : PropertyMutationTargetKind.Member));
                    Assert.That(r.SpecificationId, Is.Null);
                }
            }
        }
        [TestCase("RemoveAllElementNodeReleaseSpec")]
        [TestCase("RemoveMemberFireProofingSpecFromBeam")]
        public void MutationsRejectUndocumentedOrCoercibleResponses(string name)
        {
            foreach (object response in new object[] { -1, 2, null, DBNull.Value, "1", 1.0, 1m })
            { var n = new Native(); Respond(n, name, response); using (var w = Wrap(n)) Assert.Throws<InvalidOperationException>(() => Invoke(w.Property, name)); }
        }
        [TestCase(0)] [TestCase(-1)]
        public void FireproofingRemovalValidatesAnalyticalMemberIdentity(int memberId)
        { var n = new Native(); using (var w = Wrap(n)) Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => w.Property.RemoveMemberFireProofingSpecFromBeam(memberId)).ParamName, Is.EqualTo("memberId")); Assert.That(n.Calls, Is.Empty); }
        [TestCase("GetPublishedProfileName")]
        [TestCase("GetStaadProfileName")]
        public void ProfileNamesPreserveExactNativeTextAndKeepAbsenceSeparate(string name)
        {
            foreach (object response in new object[] { " EXACT ", "", null, DBNull.Value })
            {
                var n = new Native(); Respond(n, NativeNames[Array.IndexOf(Names, name)], response);
                using (var w = Wrap(n))
                {
                    var r = (PropertyQueryResult<string>)Invoke(w.Property, name);
                    if (response is string text && text.Length > 0) { Assert.That(r.IsSuccessful, Is.True); Assert.That(r.Data, Is.EqualTo(text)); }
                    else { Assert.That(r.Status, Is.EqualTo(PropertyQueryStatus.EmptyOrUnavailable)); Assert.Throws<InvalidOperationException>(() => { var ignored = r.Data; }); }
                    Assert.That(r.NativeReturnCode, Is.Null); Assert.That(r.NativeBooleanReturn, Is.Null);
                }
            }
        }
        [TestCase("GetPublishedProfileName")]
        [TestCase("GetStaadProfileName")]
        public void ProfileNamesRejectMalformedNativeDataWithoutCoercion(string name)
        {
            foreach (object response in new object[] { true, 1, new[] { "NAME" }, "A\0B" })
            { var n = new Native(); Respond(n, NativeNames[Array.IndexOf(Names, name)], response); using (var w = Wrap(n)) Assert.Throws<InvalidOperationException>(() => Invoke(w.Property, name)); }
        }
        [TestCase("GetPublishedProfileName")]
        [TestCase("GetStaadProfileName")]
        public void ProfileNamesValidateNamesAndTheReviewedCountrySubsetBeforeCallingNative(string name)
        {
            foreach (var text in new[] { null, "", "  ", "A\0B" })
            { var n = new Native(); using (var w = Wrap(n)) Assert.That(() => Invoke(w.Property, name, new object[] { text, Country.American }), Throws.InstanceOf<ArgumentException>()); Assert.That(n.Calls, Is.Empty); }
            foreach (var country in new[] { (Country)0, (Country)(-1), Country.Chilean, (Country)39 })
            { var n = new Native(); using (var w = Wrap(n)) Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => Invoke(w.Property, name, new object[] { "W12X26", country })).ParamName, Is.EqualTo("country")); Assert.That(n.Calls, Is.Empty); }
        }
    }
}
