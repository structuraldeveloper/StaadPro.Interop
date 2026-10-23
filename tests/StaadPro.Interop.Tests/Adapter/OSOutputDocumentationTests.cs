using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums.Output;
using StaadPro.Interop.Models.Output;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSOutputDocumentationTests
    {
        [Test]
        public void AllOutputOperationsShipExpandedMatchingIntelliSenseContractsAndExamples()
        {
            var assembly = typeof(OSOutputAdapter).Assembly;
            var xml = XDocument.Load(Path.ChangeExtension(assembly.Location, ".xml"));
            var members = xml.Descendants("member").ToArray();
            foreach (var method in typeof(IOSOutput).GetMethods().Where(m => !m.IsSpecialName))
            {
                string[] methodNames = { "M:" + typeof(IOSOutput).FullName + "." + method.Name,
                    "M:" + typeof(OSOutputAdapter).FullName + "." + method.Name };
                XElement[] entries = methodNames.Select(name => members.Single(m =>
                    ((string)m.Attribute("name")).Split('(')[0] == name)).ToArray();
                foreach (var entry in entries)
                {
                    foreach (string element in new[] { "summary", "returns", "remarks", "example" })
                        Assert.That(entry.Element(element)?.Value.Trim(), Is.Not.Null.And.Not.Empty, method.Name + ": " + element);
                    Assert.That(entry.Elements("param").Select(p => (string)p.Attribute("name")),
                        Is.EqualTo(method.GetParameters().Select(p => p.Name)), method.Name);
                    Assert.That(entry.Elements("param").All(p => !string.IsNullOrWhiteSpace(p.Value)), Is.True);
                    Assert.That(entry.Element("remarks").Value, Does.Contain("COM apartment").And.Contain("units"));
                    Assert.That(entry.Element("example").Element("code").Value, Does.Contain("IsSuccessful"));
                }
                Assert.That(entries[0].Nodes().Select(n => n.ToString()), Is.EqualTo(entries[1].Nodes().Select(n => n.ToString())), method.Name);
            }
        }

        [Test]
        public void EveryNewPublicPayloadAndEnumMemberHasSummaryDocumentation()
        {
            var assembly = typeof(OSOutputAdapter).Assembly;
            var members = XDocument.Load(Path.ChangeExtension(assembly.Location, ".xml")).Descendants("member")
                .ToDictionary(m => (string)m.Attribute("name"));
            foreach (Type type in assembly.GetExportedTypes().Where(t => t.Namespace == typeof(ForceComponents).Namespace ||
                t.Namespace == typeof(OutputQueryStatus).Namespace))
            {
                string typeName = type.IsGenericType ? type.GetGenericTypeDefinition().FullName : type.FullName;
                Assert.That(members.ContainsKey("T:" + typeName), Is.True, typeName);
                Assert.That(members["T:" + typeName].Element("summary")?.Value.Trim(), Is.Not.Null.And.Not.Empty);
                foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                    Assert.That(members["P:" + typeName + "." + property.Name].Element("summary")?.Value.Trim(), Is.Not.Null.And.Not.Empty);
                if (type.IsEnum)
                    foreach (string name in Enum.GetNames(type))
                        Assert.That(members["F:" + typeName + "." + name].Element("summary")?.Value.Trim(), Is.Not.Null.And.Not.Empty);
            }
        }
    }
}
