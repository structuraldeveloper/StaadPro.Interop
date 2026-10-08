using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSLoadCaseLookupTests
    {
        // Only metadata APIs are exposed: activation or case enumeration would fail binding.
        public class MetadataLoad
        {
            public object Title { get; set; } = "  雪\r\nCASE  ";
            public object Type { get; set; } = (int)LoadType.None;
            public string ThrowAt { get; set; }
            public int FailureId { get; set; } = 17;
            public Exception Failure { get; set; } = new COMException("lookup failure");
            public Action OnRead { get; set; }
            public List<string> Calls { get; } = new List<string>();
            private object Read(string family, string stage, int id, object result)
            {
                Calls.Add(family + ":" + stage + ":" + id);
                OnRead?.Invoke();
                if (stage == ThrowAt && id == FailureId) throw Failure;
                return result;
            }
            public object GetLoadCaseTitle(int id) => Read("primary", "title", id, Title);
            public object GetReferenceLoadCaseTitle(int id) => Read("reference", "title", id, Title);
            public object GetLoadType(int id) => Read("primary", "type", id, Type);
            public object GetReferenceLoadType(int id) => Read("reference", "type", id, Type);
        }

        public class TitleOnlyLoad
        {
            public string GetLoadCaseTitle(int id) => "TITLE";
            public string GetReferenceLoadCaseTitle(int id) => "TITLE";
        }

        private static ILoadCase Read(IOSLoad load, bool reference, int id = 17) => reference
            ? load.GetReferenceLoadCaseFromId(id) : load.GetPrimaryLoadCaseFromId(id);
        private static string Family(bool reference) => reference ? "reference" : "primary";

        [TestCase(false)]
        [TestCase(true)]
        public void Lookup_ReadsOnlyRequestedMetadataAndReturnsIndependentObjects(bool reference)
        {
            var raw = new MetadataLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var first = Read(wrapper.Load, reference, int.MaxValue);
                Assert.That(first, Is.TypeOf<LoadCase>());
                Assert.That(first.Id, Is.EqualTo(int.MaxValue));
                Assert.That(first.Title, Is.EqualTo(raw.Title));
                Assert.That(first.Type, Is.EqualTo(LoadType.None));
                Assert.That(first.CaseType, Is.EqualTo(reference ? LoadCaseType.ReferenceLoad : LoadCaseType.PrimaryLoad));
                Assert.That(raw.Calls, Is.EqualTo(new[] { Family(reference) + ":title:" + int.MaxValue, Family(reference) + ":type:" + int.MaxValue }));
                var second = Read(wrapper.Load, reference, int.MaxValue);
                Assert.That(second, Is.EqualTo(first).And.Not.SameAs(first));
                ((LoadCase)first).Title = "edited";
                first.Type = LoadType.Dead;
                Assert.That(second.Title, Is.EqualTo(raw.Title));
                Assert.That(second.Type, Is.EqualTo(LoadType.None));
                raw.Title = "native change";
                Assert.That(Read(wrapper.Load, reference, int.MaxValue).Title, Is.EqualTo("native change"));
                Assert.That(second.Title, Is.Not.EqualTo("native change"));
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void Lookup_RejectsNonpositiveIdsBeforeCom(bool reference)
        {
            var raw = new MetadataLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                foreach (int id in new[] { 0, -1, int.MinValue })
                    Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => Read(wrapper.Load, reference, id)).ParamName, Is.EqualTo("lcId"));
            Assert.That(raw.Calls, Is.Empty);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void Lookup_AcceptsAllDefinedEngineeringTypesAndSignedIntegerRepresentations(bool reference)
        {
            var raw = new MetadataLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                foreach (LoadType type in Enum.GetValues(typeof(LoadType)))
                    foreach (object native in new object[] { (short)type, (int)type, (long)type })
                    {
                        raw.Type = native;
                        Assert.That(Read(wrapper.Load, reference).Type, Is.EqualTo(type));
                    }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void Lookup_PreservesStringTitlesAndRejectsOtherTitleResults(bool reference)
        {
            foreach (object title in new object[] { "", " ", "NONE", "雪\r\nTitle", null, 0, true, new char[] { 'X' } })
            {
                var raw = new MetadataLoad { Title = title };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                {
                    if (title is string text) Assert.That(Read(wrapper.Load, reference).Title, Is.EqualTo(text));
                    else
                    {
                        Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, reference));
                        Assert.That(raw.Calls, Is.EqualTo(new[] { Family(reference) + ":title:17" }));
                    }
                }
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void Lookup_RejectsNativeErrorsAndMalformedTypesWithoutReturningNull(bool reference)
        {
            foreach (object type in new object[] { -1, -106, -114, 24, int.MaxValue, long.MaxValue, long.MinValue, null, true, "0", 0.0, (uint)0, (byte)0 })
            {
                var raw = new MetadataLoad { Type = type };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.Throws<InvalidOperationException>(() => Read(wrapper.Load, reference));
                Assert.That(raw.Calls.Count, Is.EqualTo(2));
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void Lookup_PropagatesNativeFailuresAndStopsAtFailingStage(bool reference)
        {
            foreach (string stage in new[] { "title", "type" })
            {
                var raw = new MetadataLoad { ThrowAt = stage };
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.That(Assert.Throws<COMException>(() => Read(wrapper.Load, reference)), Is.SameAs(raw.Failure));
                Assert.That(raw.Calls.Count, Is.EqualTo(stage == "title" ? 1 : 2));
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void Lookup_PropagatesMissingTitleOrTypeSignature(bool reference)
        {
            foreach (object raw in new[] { new object(), new TitleOnlyLoad() })
                using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                    Assert.Throws<RuntimeBinderException>(() => Read(wrapper.Load, reference));
        }

        [Test]
        public void Lookup_UsesSeparateMetadataFamiliesWhenIdsOverlap()
        {
            var raw = new OSLoadCaseQueryTests.CaseLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var primary = wrapper.Load.GetPrimaryLoadCaseFromId(17);
                var reference = wrapper.Load.GetReferenceLoadCaseFromId(17);
                Assert.That(primary.Type, Is.EqualTo(LoadType.Dead));
                Assert.That(reference.Type, Is.EqualTo(LoadType.Mass));
                Assert.That(primary.Title, Does.Contain("primary"));
                Assert.That(reference.Title, Does.Contain("reference"));
                Assert.That(primary.Id, Is.EqualTo(reference.Id));
                Assert.That(raw.Calls, Is.EqualTo(new[] { "primary:title:17", "primary:type:17", "reference:title:17", "reference:type:17" }));
            }
        }

        [Test]
        public void Batch_PreservesInputOrderDuplicatesAndIndependentOwnership()
        {
            var raw = new MetadataLoad();
            var ids = new[] { 41, 17, 41 };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var result = wrapper.Load.GetPrimaryLoadCasesFromIds(ids);
                Assert.That(result.Select(c => c.Id), Is.EqualTo(ids));
                Assert.That(result.All(c => c.CaseType == LoadCaseType.PrimaryLoad && c.Type == LoadType.None && c.Title == (string)raw.Title), Is.True);
                Assert.That(result[0], Is.EqualTo(result[2]).And.Not.SameAs(result[2]));
                Assert.That(raw.Calls, Is.EqualTo(new[] { "primary:title:41", "primary:type:41", "primary:title:17", "primary:type:17", "primary:title:41", "primary:type:41" }));
                ids[0] = 99;
                Assert.That(result[0].Id, Is.EqualTo(41));
                ((LoadCase)result[0]).Title = "edited";
                Assert.That(result[2].Title, Is.EqualTo(raw.Title));
                var again = wrapper.Load.GetPrimaryLoadCasesFromIds(new[] { 41, 17, 41 });
                Assert.That(again, Is.Not.SameAs(result));
                Assert.That(again[2], Is.Not.SameAs(result[2]));
                Assert.That(again[0].Title, Is.EqualTo(raw.Title));
            }
        }

        [Test]
        public void Batch_EmptyInputReturnsFreshEmptyListsWithoutCom()
        {
            var raw = new MetadataLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                var first = wrapper.Load.GetPrimaryLoadCasesFromIds(new int[0]);
                var second = wrapper.Load.GetPrimaryLoadCasesFromIds(Enumerable.Empty<int>());
                Assert.That(first, Is.Empty);
                Assert.That(second, Is.Empty.And.Not.SameAs(first));
            }
            Assert.That(raw.Calls, Is.Empty);
        }

        [Test]
        public void Batch_ValidatesNullAndEveryIdBeforeCom()
        {
            var raw = new MetadataLoad();
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
            {
                Assert.That(Assert.Throws<ArgumentNullException>(() => wrapper.Load.GetPrimaryLoadCasesFromIds(null)).ParamName, Is.EqualTo("loadCasesIds"));
                foreach (int id in new[] { 0, -1, int.MinValue })
                    Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.GetPrimaryLoadCasesFromIds(new[] { 17, 41, id })).ParamName, Is.EqualTo("loadCasesIds"));
            }
            Assert.That(raw.Calls, Is.Empty);
        }

        private sealed class SinglePassIds : IEnumerable<int>
        {
            private readonly IEnumerable<int> source;
            public int Enumerations { get; private set; }
            public SinglePassIds(IEnumerable<int> source) { this.source = source; }
            public IEnumerator<int> GetEnumerator()
            {
                if (++Enumerations != 1) throw new InvalidOperationException("Input was enumerated twice.");
                return source.GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Test]
        public void Batch_SnapshotsInputOnceBeforeNativeAccess()
        {
            var backing = new List<int> { 41, 17, 41 };
            var ids = new SinglePassIds(backing);
            var raw = new MetadataLoad { OnRead = () => backing.Clear() };
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.That(wrapper.Load.GetPrimaryLoadCasesFromIds(ids).Select(c => c.Id), Is.EqualTo(new[] { 41, 17, 41 }));
            Assert.That(ids.Enumerations, Is.EqualTo(1));
        }

        private static IEnumerable<int> FailingIds(Exception failure, Action disposed)
        {
            try { yield return 17; throw failure; }
            finally { disposed(); }
        }

        [Test]
        public void Batch_PropagatesEnumeratorFailureBeforeComAndDisposesEnumerator()
        {
            var raw = new MetadataLoad();
            var failure = new ApplicationException("input failed");
            bool disposed = false;
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.That(Assert.Throws<ApplicationException>(() => wrapper.Load.GetPrimaryLoadCasesFromIds(FailingIds(failure, () => disposed = true))), Is.SameAs(failure));
            Assert.That(disposed, Is.True);
            Assert.That(raw.Calls, Is.Empty);
        }

        [TestCase("title", 3)]
        [TestCase("type", 4)]
        public void Batch_LaterNativeFailureStopsBeforeRemainingIds(string stage, int callCount)
        {
            var raw = new MetadataLoad { ThrowAt = stage };
            List<ILoadCase> result = null;
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.That(Assert.Throws<COMException>(() => result = wrapper.Load.GetPrimaryLoadCasesFromIds(new[] { 41, 17, 99 })), Is.SameAs(raw.Failure));
            Assert.That(result, Is.Null, "No partial list escapes.");
            Assert.That(raw.Calls.Count, Is.EqualTo(callCount));
            Assert.That(raw.Calls.Any(c => c.EndsWith(":99")), Is.False);
        }

        [Test]
        public void Batch_LaterMalformedMetadataDoesNotReturnPartialList()
        {
            var raw = new MetadataLoad();
            raw.OnRead = () => { if (raw.Calls.Count == 3) raw.Type = -1; };
            List<ILoadCase> result = null;
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(raw)))
                Assert.Throws<InvalidOperationException>(() => result = wrapper.Load.GetPrimaryLoadCasesFromIds(new[] { 41, 17, 99 }));
            Assert.That(result, Is.Null);
            Assert.That(raw.Calls.Count, Is.EqualTo(4));
        }

        [Test]
        public void Batch_PropagatesMissingNativeSignature()
        {
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(new TitleOnlyLoad())))
                Assert.Throws<RuntimeBinderException>(() => wrapper.Load.GetPrimaryLoadCasesFromIds(new[] { 17 }));
        }

        [Test]
        public void ConcreteAdapter_ExposesAllThreeLookupMethods()
        {
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(new MetadataLoad())))
            {
                var adapter = new OSLoadAdapter(wrapper);
                Assert.That(adapter.GetPrimaryLoadCaseFromId(17).CaseType, Is.EqualTo(LoadCaseType.PrimaryLoad));
                Assert.That(adapter.GetReferenceLoadCaseFromId(17).CaseType, Is.EqualTo(LoadCaseType.ReferenceLoad));
                Assert.That(adapter.GetPrimaryLoadCasesFromIds(new[] { 41, 17 }).Select(c => c.Id), Is.EqualTo(new[] { 41, 17 }));
            }
        }
    }
}
