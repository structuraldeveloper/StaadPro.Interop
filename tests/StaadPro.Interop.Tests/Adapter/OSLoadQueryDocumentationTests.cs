using System.IO;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSLoadQueryDocumentationTests
    {
        [TestCase("Entities.LoadEnvelopeDetail", 8)]
        [TestCase("Entities.ILoadEnvelopeDetail", 4)]
        [TestCase("Enums.LoadEnvelopType", 8)]
        public void EnvelopeSupportingTypesHaveExpandedDocumentation(string typeName, int expectedCount)
        {
            var document = XDocument.Load(Path.ChangeExtension(typeof(IOSLoad).Assembly.Location, ".xml"));
            string owner = "StaadPro.Interop." + typeName;
            var members = document.Descendants("member").Where(m => (string)m.Attribute("name") == "T:" + owner || ((string)m.Attribute("name")).Substring(2).StartsWith(owner + ".")).ToList();
            Assert.That(members.Count, Is.EqualTo(expectedCount));
            foreach (var member in members)
            {
                Assert.That(member.Element("summary")?.Value.Trim(), Is.Not.Null.And.Not.Empty);
                Assert.That(member.Elements("param").All(p => !string.IsNullOrWhiteSpace(p.Value)), Is.True);
                Assert.That(member.Descendants().Attributes("cref").Any(c => c.Value.StartsWith("!:")), Is.False);
            }
        }

        [TestCase("MemberConcentratedMoment", 9)]
        [TestCase("MemberLinearVaryingLoad", 9)]
        [TestCase("MemberTrapezoidalLoad", 10)]
        [TestCase("MemberUniformMoment", 10)]
        public void NewLoadModels_IncludeCompleteCompiledDocumentation(string typeName, int expectedCount)
        {
            var document = XDocument.Load(Path.ChangeExtension(typeof(IOSLoad).Assembly.Location, ".xml"));
            string owner = "StaadPro.Interop.Entities." + typeName;
            var members = document.Descendants("member").Where(m => (string)m.Attribute("name") == "T:" + owner || ((string)m.Attribute("name")).StartsWith("M:" + owner + ".") || ((string)m.Attribute("name")).StartsWith("P:" + owner + ".")).ToList();
            Assert.That(members.Count, Is.EqualTo(expectedCount));
            foreach (var member in members)
            {
                Assert.That(member.Element("summary")?.Value.Trim(), Is.Not.Null.And.Not.Empty, (string)member.Attribute("name"));
                Assert.That(member.Elements("param").All(p => !string.IsNullOrWhiteSpace(p.Value)), Is.True);
                Assert.That(member.Descendants().Attributes("cref").Any(c => c.Value.StartsWith("!:")), Is.False);
            }
            Assert.That(members.Single(m => ((string)m.Attribute("name")).EndsWith(".DeepCopy")).Element("returns")?.Value.Trim(), Is.Not.Null.And.Not.Empty);
        }

        [TestCase("GetNodalLoads", "nId")]
        [TestCase("GetMemberUniformlyDistributedLoads", "mId")]
        [TestCase("GetMemberConcentratedLoads", "mId")]
        [TestCase("GetMemberConcentratedMoments", "mId")]
        [TestCase("GetMemberLinearVaryingLoads", "mId")]
        [TestCase("GetMemberTrapezoidalLoads", "mId")]
        [TestCase("GetMemberUniformMoments", "mId")]
        public void CompiledXml_ContainsCompleteMatchingInterfaceAndConcreteIntelliSense(string method, string id)
        {
            string path = Path.ChangeExtension(typeof(IOSLoad).Assembly.Location, ".xml");
            Assert.That(File.Exists(path), Is.True, "XML documentation must accompany the library.");
            var document = XDocument.Load(path);
            XElement previous = null;
            foreach (var type in new[] { typeof(IOSLoad), typeof(OSLoadAdapter) })
            {
                string memberName = "M:" + type.FullName + "." + method + "(StaadPro.Interop.Entities.ILoadCase,System.Int32)";
                var member = document.Descendants("member").Single(m => (string)m.Attribute("name") == memberName);
                foreach (string tag in new[] { "summary", "returns", "remarks", "example" })
                    Assert.That(member.Element(tag)?.Value.Trim(), Is.Not.Null.And.Not.Empty, tag);
                Assert.That(member.Elements("param").Select(p => (string)p.Attribute("name")), Is.EqualTo(new[] { "lc", id }));
                Assert.That(member.Elements("param").All(p => !string.IsNullOrWhiteSpace(p.Value)), Is.True);
                Assert.That(member.Elements("exception").Count(), Is.EqualTo(6));
                Assert.That(member.Descendants("inheritdoc"), Is.Empty, "NuGet XML needs expanded docs on concrete calls.");
                Assert.That(member.Descendants().Attributes("cref").Any(c => c.Value.StartsWith("!:")), Is.False);
                Assert.That(member.Element("example").Element("code").Value, Does.Contain("load." + method + "("));
                if (previous != null) Assert.That(member.Elements().Select(e => e.ToString()), Is.EqualTo(previous.Elements().Select(e => e.ToString())));
                previous = member;
            }
        }
        [TestCase("GetPrimaryLoadCaseFromId", "System.Int32", "lcId", 4)]
        [TestCase("GetLoadEnvelopeDetail", "System.Int32", "envId", 4)]
        [TestCase("GetLoadCombinationCasesFromIds", "System.Collections.Generic.IEnumerable{System.Int32}", "lcIds", 5)]
        [TestCase("GetAllLoadCasesOfType", "StaadPro.Interop.Enums.LoadCaseType", "loadCaseType", 4)]
        [TestCase("GetLoadCasesOfType", "StaadPro.Interop.Enums.LoadCaseType", "loadCaseType", 4)]
        [TestCase("GetReferenceLoadCaseFromId", "System.Int32", "lcId", 4)]
        [TestCase("GetLoadCombinationCaseFromId", "System.Int32", "lcId", 4)]
        [TestCase("GetReferenceLoadCasesFromIds", "System.Collections.Generic.IEnumerable{System.Int32}", "loadCasesIds", 5)]
        [TestCase("GetPrimaryLoadCasesFromIds", "System.Collections.Generic.IEnumerable{System.Int32}", "loadCasesIds", 5)]
        public void CaseLookup_ContainsCompleteMatchingIntelliSense(string method, string signature, string parameter, int exceptions)
        {
            var document = XDocument.Load(Path.ChangeExtension(typeof(IOSLoad).Assembly.Location, ".xml"));
            XElement previous = null;
            foreach (var type in new[] { typeof(IOSLoad), typeof(OSLoadAdapter) })
            {
                var member = document.Descendants("member").Single(m => (string)m.Attribute("name") == "M:" + type.FullName + "." + method + "(" + signature + ")");
                foreach (string tag in new[] { "summary", "returns", "remarks", "example" })
                    Assert.That(member.Element(tag)?.Value.Trim(), Is.Not.Null.And.Not.Empty, tag);
                Assert.That(member.Elements("param").Select(p => (string)p.Attribute("name")), Is.EqualTo(new[] { parameter }));
                Assert.That(member.Element("param").Value.Trim(), Is.Not.Empty);
                Assert.That(member.Elements("exception").Count(), Is.EqualTo(exceptions));
                Assert.That(member.Elements("exception").All(e => !string.IsNullOrWhiteSpace(e.Value)), Is.True);
                Assert.That(member.Descendants("inheritdoc"), Is.Empty);
                Assert.That(member.Descendants().Attributes("cref").Any(c => c.Value.StartsWith("!:")), Is.False);
                Assert.That(member.Element("example").Element("code").Value, Does.Contain("load." + method + "("));
                if (previous != null) Assert.That(member.Elements().Select(e => e.ToString()), Is.EqualTo(previous.Elements().Select(e => e.ToString())));
                previous = member;
            }
        }

        [TestCase("GetLoadCase", "System.Int32,StaadPro.Interop.Enums.LoadCaseType", "lcId|loadCaseType", 5)]
        [TestCase("GetLoadCases", "System.Int32,System.Int32,StaadPro.Interop.Enums.LoadCaseType", "fromId|toId|loadCaseType", 6)]
        [TestCase("GetLoadCasesFromIds", "System.Collections.Generic.IEnumerable{System.Int32},StaadPro.Interop.Enums.LoadCaseType", "loadCasesIds|loadCaseType", 6)]
        public void MultiParameterQuery_ContainsCompleteMatchingIntelliSense(string method, string signature, string parameters, int exceptionCount)
        {
            var document = XDocument.Load(Path.ChangeExtension(typeof(IOSLoad).Assembly.Location, ".xml"));
            XElement previous = null;
            foreach (var type in new[] { typeof(IOSLoad), typeof(OSLoadAdapter) })
            {
                var member = document.Descendants("member").Single(m => (string)m.Attribute("name") == "M:" + type.FullName + "." + method + "(" + signature + ")");
                foreach (string tag in new[] { "summary", "returns", "remarks", "example" })
                    Assert.That(member.Element(tag)?.Value.Trim(), Is.Not.Null.And.Not.Empty, tag);
                Assert.That(member.Elements("param").Select(p => (string)p.Attribute("name")), Is.EqualTo(parameters.Split('|')));
                Assert.That(member.Elements("param").All(p => !string.IsNullOrWhiteSpace(p.Value)), Is.True);
                Assert.That(member.Elements("exception").Count(), Is.EqualTo(exceptionCount));
                Assert.That(member.Elements("exception").All(e => !string.IsNullOrWhiteSpace(e.Value)), Is.True);
                Assert.That(member.Descendants("inheritdoc"), Is.Empty);
                Assert.That(member.Descendants().Attributes("cref").Any(c => c.Value.StartsWith("!:")), Is.False);
                Assert.That(member.Element("example").Element("code").Value, Does.Contain("load." + method + "("));
                if (previous != null) Assert.That(member.Elements().Select(e => e.ToString()), Is.EqualTo(previous.Elements().Select(e => e.ToString())));
                previous = member;
            }
        }

        [TestCase("GetAllLoadCombinationCases")]
        [TestCase("GetEnvelopeIds")]
        [TestCase("GetAllLoadEnvelopeDetails")]
        [TestCase("GetAllPrimaryLoadCases")]
        [TestCase("GetAllReferenceLoadCases")]
        public void CaseEnumeration_ContainsCompleteMatchingIntelliSense(string method)
        {
            var document = XDocument.Load(Path.ChangeExtension(typeof(IOSLoad).Assembly.Location, ".xml"));
            XElement previous = null;
            foreach (var type in new[] { typeof(IOSLoad), typeof(OSLoadAdapter) })
            {
                var member = document.Descendants("member").Single(m => (string)m.Attribute("name") == "M:" + type.FullName + "." + method);
                foreach (string tag in new[] { "summary", "returns", "remarks", "example" })
                    Assert.That(member.Element(tag)?.Value.Trim(), Is.Not.Null.And.Not.Empty, tag);
                Assert.That(member.Elements("param"), Is.Empty);
                Assert.That(member.Elements("exception").Count(), Is.EqualTo(3));
                Assert.That(member.Descendants("inheritdoc"), Is.Empty);
                Assert.That(member.Descendants().Attributes("cref").Any(c => c.Value.StartsWith("!:")), Is.False);
                Assert.That(member.Element("example").Element("code").Value, Does.Contain("load." + method + "()"));
                if (previous != null) Assert.That(member.Elements().Select(e => e.ToString()), Is.EqualTo(previous.Elements().Select(e => e.ToString())));
                previous = member;
            }
        }
    }
}
