using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Models
{
    [TestFixture]
    public class JobInfoTests
    {
        [Test]
        public void FactorySnapshotsAndPropertyNotificationsUseIndependentManagedStorage()
        {
            var a = ShortJobInfo.From(null, " Client ", "Engineer");
            var b = ShortJobInfo.From(null, " Client ", "Engineer");
            var changed = new List<string>(); a.PropertyChanged += (_, e) => changed.Add(e.PropertyName);
            Assert.That(a.Name, Is.Null);
            a.Client = "changed"; a.Client = "changed";
            Assert.That(b.Client, Is.EqualTo(" Client "));
            Assert.That(changed, Is.EqualTo(new[] { "Client" }));
            var full = FullJobInfo.From("N", "C", "E", null, "Checker", " ", "Approver", "", "No", "Rev", "Part", "Ref", "Comments");
            Assert.That(full, Is.InstanceOf<ShortJobInfo>());
            Assert.That(new[] { full.Originated, full.Checked, full.Approved }, Is.All.EqualTo(DateTime.MinValue));
            full.PropertyChanged += (_, e) => changed.Add(e.PropertyName);
            full.Name = "new"; full.Checked = new DateTime(2026, 10, 13); full.Comments = "new comments";
            Assert.That(changed, Is.EqualTo(new[] { "Client", "Name", "Checked", "Comments" }));
        }

        [Test]
        [SetCulture("en-GB")]
        public void FactoryUsesCurrentCultureWithoutAlternativeDateGuessing()
        {
            var job = FullJobInfo.From("N", "C", "E", "10/11/2026", "Check", "12/10/2026", "Approve", "11/10/2026", "No", "Rev", "Part", "Ref", "Comments");
            Assert.That(job.Originated, Is.EqualTo(new DateTime(2026, 11, 10)));
            Assert.That(job.Checked, Is.EqualTo(new DateTime(2026, 10, 12)));
            Assert.Throws<FormatException>(() => FullJobInfo.From("N", "C", "E", "invalid", "Check", "", "Approve", "", "No", "Rev", "Part", "Ref", "Comments"));
        }

        [TestCase("ShortJobInfo", 6)]
        [TestCase("FullJobInfo", 13)]
        public void ModelsFactoriesAndEveryPropertyHaveExpandedIntelliSense(string name, int count)
        {
            var xml = XDocument.Load(Path.ChangeExtension(typeof(ShortJobInfo).Assembly.Location, ".xml"));
            string owner = "StaadPro.Interop.Models." + name;
            var entries = xml.Descendants("member").Where(e => ((string)e.Attribute("name")).Substring(2) == owner || ((string)e.Attribute("name")).Substring(2).StartsWith(owner + ".")).ToArray();
            Assert.That(entries.Length, Is.EqualTo(count));
            Assert.That(entries.All(e => !string.IsNullOrWhiteSpace(e.Element("summary")?.Value)), Is.True);
            var factory = entries.Single(e => ((string)e.Attribute("name")).Contains(".From("));
            Assert.That(factory.Element("returns")?.Value, Is.Not.Null.And.Not.Empty);
            Assert.That(factory.Element("remarks")?.Value, Is.Not.Null.And.Not.Empty);
            Assert.That(factory.Elements("param").All(p => !string.IsNullOrWhiteSpace(p.Value)), Is.True);
            Assert.That(entries.SelectMany(e => e.Descendants().Attributes("cref")).Any(a => a.Value.StartsWith("!:")), Is.False);
        }
    }
}
