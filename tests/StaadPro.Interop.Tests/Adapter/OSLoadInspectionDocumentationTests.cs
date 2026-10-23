using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums.Loads;
using StaadPro.Interop.Models.Loads.Queries;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSLoadInspectionDocumentationTests
    {
        [Test]
        public void FifteenQueriesHaveMatchingCompleteInterfaceAndAdapterIntelliSense()
        {
            var members = XDocument.Load(Path.ChangeExtension(typeof(OSLoadAdapter).Assembly.Location, ".xml")).Descendants("member").ToArray();
            foreach (string name in OSLoadInspectionTests.Names)
            {
                var method = typeof(IOSLoad).GetMethod(name);
                Assert.That(method, Is.Not.Null);
                Assert.That(method.GetParameters().Any(p => p.ParameterType.IsByRef), Is.False);
                Assert.That(method.ReturnType.GetGenericTypeDefinition(), Is.EqualTo(typeof(LoadQueryResult<>)));
                XElement[] entries = new[] { typeof(IOSLoad), typeof(OSLoadAdapter) }.Select(t =>
                    members.Single(m => ((string)m.Attribute("name")).Split('(')[0] == "M:" + t.FullName + "." + name)).ToArray();
                foreach (var entry in entries)
                {
                    foreach (string tag in new[] { "summary", "returns", "remarks", "example" })
                        Assert.That(entry.Element(tag)?.Value.Trim(), Is.Not.Null.And.Not.Empty, name + ": " + tag);
                    Assert.That(entry.Elements("param").Select(p => (string)p.Attribute("name")), Is.EqualTo(method.GetParameters().Select(p => p.Name)));
                    Assert.That(entry.Elements("param").All(p => !string.IsNullOrWhiteSpace(p.Value)), Is.True);
                    Assert.That(entry.Element("remarks").Value, Does.Contain("COM apartment").And.Contain("units").And.Contain("2024"));
                    Assert.That(entry.Element("example").Element("code").Value, Does.Contain("IsSuccessful"));
                }
                Assert.That(entries[0].Nodes().Select(n => n.ToString()), Is.EqualTo(entries[1].Nodes().Select(n => n.ToString())), name);
            }
        }
        [Test]
        public void NewQueryPayloadsAndEnumValuesHaveSummaryDocumentation()
        {
            var assembly = typeof(OSLoadAdapter).Assembly;
            var members = XDocument.Load(Path.ChangeExtension(assembly.Location, ".xml")).Descendants("member").ToDictionary(m => (string)m.Attribute("name"));
            foreach (var type in assembly.GetExportedTypes().Where(t => t.Namespace == typeof(LoadQueryResult<>).Namespace || t.Namespace == typeof(LoadQueryStatus).Namespace))
            {
                string name = type.FullName;
                Assert.That(members["T:" + name].Element("summary")?.Value.Trim(), Is.Not.Null.And.Not.Empty, name);
                foreach (var p in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                    Assert.That(members["P:" + name + "." + p.Name].Element("summary")?.Value.Trim(), Is.Not.Null.And.Not.Empty);
                if (type.IsEnum)
                    foreach (string value in Enum.GetNames(type)) Assert.That(members["F:" + name + "." + value].Element("summary")?.Value.Trim(), Is.Not.Null.And.Not.Empty);
            }
        }
    }
}
