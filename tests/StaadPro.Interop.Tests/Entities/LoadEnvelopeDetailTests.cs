using System.Collections.Generic;
using NUnit.Framework;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Tests.Entities
{
    [TestFixture]
    public class LoadEnvelopeDetailTests
    {
        [Test]
        public void ConstructorsInitializeIndependentSetsAndPreserveMetadata()
        {
            var first = new LoadEnvelopeDetail();
            var second = new LoadEnvelopeDetail();
            Assert.That(first.Id, Is.Zero);
            Assert.That(first.EnvelopType, Is.EqualTo(LoadEnvelopType.None));
            Assert.That(first.LoadCasesCount, Is.Zero);
            Assert.That(first.LoadCaseIds, Is.Empty.And.Not.SameAs(second.LoadCaseIds));
            var metadata = new LoadEnvelopeDetail(7, LoadEnvelopType.Serviceability, 2);
            Assert.That(metadata.Id, Is.EqualTo(7));
            Assert.That(metadata.EnvelopType, Is.EqualTo(LoadEnvelopType.Serviceability));
            Assert.That(metadata.LoadCasesCount, Is.EqualTo(2));
            Assert.That(metadata.LoadCaseIds, Is.Empty);
            var source = new HashSet<int> { 17, 41 };
            var copy = new LoadEnvelopeDetail(19, LoadEnvelopType.Strength, 2, source);
            Assert.That(copy.LoadCaseIds, Is.EquivalentTo(source).And.Not.SameAs(source));
            source.Clear();
            Assert.That(copy.LoadCaseIds, Is.EquivalentTo(new[] { 17, 41 }));
            Assert.That(new LoadEnvelopeDetail(7, LoadEnvelopType.None, 0, null).LoadCaseIds, Is.Empty);
        }

        [Test]
        public void ManagedEditsNotifyOnlyForChangedPropertiesAndDoNotReconcileMembership()
        {
            var model = new LoadEnvelopeDetail();
            var changed = new List<string>();
            model.PropertyChanged += (_, e) => changed.Add(e.PropertyName);
            model.Id = 7;
            model.Id = 7;
            model.EnvelopType = LoadEnvelopType.Strength;
            model.LoadCasesCount = 2;
            model.LoadCaseIds = new HashSet<int> { 17, 41 };
            Assert.That(changed, Is.EqualTo(new[] { "Id", "EnvelopType", "LoadCasesCount", "LoadCaseIds" }));
            changed.Clear();
            model.LoadCaseIds.Clear();
            Assert.That(model.LoadCasesCount, Is.EqualTo(2));
            Assert.That(changed, Is.Empty);
            Assert.That(new LoadEnvelopeDetail(7, LoadEnvelopType.Strength, 2), Is.Not.EqualTo(model));
        }
    }
}
