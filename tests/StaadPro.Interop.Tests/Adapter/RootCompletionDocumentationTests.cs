using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Interfaces.Capabilities;
using StaadPro.Interop.Models.Root;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class RootCompletionDocumentationTests
    {
        [TestCase(typeof(IOSRootCapabilities))]
        [TestCase(typeof(IOSRootAdinaExport))]
        [TestCase(typeof(IOSRootAnalysisErrorMessages))]
        [TestCase(typeof(IOSRootAnalysisWarningMessages))]
        public void OptionalInterfacesHaveCompletePublicIntelliSenseAndNoReferenceParameters(Type contract)
        {
            var entries = XDocument.Load(Path.ChangeExtension(contract.Assembly.Location, ".xml"))
                .Descendants("member").ToDictionary(m => (string)m.Attribute("name"));
            foreach (var method in contract.GetMethods())
            {
                string prefix = "M:" + contract.FullName + "." + method.Name;
                var documentation = entries.Single(e => e.Key == prefix || e.Key.StartsWith(prefix + "(")).Value;
                foreach (string tag in new[] { "summary", "returns", "remarks", "example" })
                    Assert.That(documentation.Element(tag)?.Value, Is.Not.Null.And.Not.Empty, method.Name + ":" + tag);
                Assert.That(documentation.Element("example")?.Element("code")?.Value, Is.Not.Null.And.Not.Empty);
                Assert.That(documentation.Elements("param").Select(p => (string)p.Attribute("name")),
                    Is.EquivalentTo(method.GetParameters().Select(p => p.Name)));
                Assert.That(method.GetParameters().Any(p => p.IsOut || p.ParameterType.IsByRef), Is.False);
            }
        }

        [Test]
        public void EveryRootResultAndStatusHasDocumentedPropertiesAndValues()
        {
            var assembly = typeof(IOSRoot).Assembly;
            var entries = XDocument.Load(Path.ChangeExtension(assembly.Location, ".xml"))
                .Descendants("member").ToDictionary(m => (string)m.Attribute("name"));
            foreach (Type type in assembly.GetExportedTypes().Where(t => t.Namespace == "StaadPro.Interop.Models.Root" || t.Namespace == "StaadPro.Interop.Enums.Root"))
            {
                string typeName = type.FullName;
                // XML generic type IDs retain the arity just like FullName.
                Assert.That(entries["T:" + typeName].Element("summary")?.Value, Is.Not.Null.And.Not.Empty, typeName);
                foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    var docs = entries["P:" + typeName + "." + property.Name];
                    Assert.That(docs.Element("summary")?.Value, Is.Not.Null.And.Not.Empty);
                    Assert.That(docs.Element("value")?.Value, Is.Not.Null.And.Not.Empty);
                    Assert.That(property.CanWrite, Is.False);
                }
                foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
                    Assert.That(entries["F:" + typeName + "." + field.Name].Element("summary")?.Value, Is.Not.Null.And.Not.Empty);
            }
            foreach (Type type in new[] { typeof(AnalysisMessagesResult), typeof(AdinaExportResult), typeof(MemberForcesResult), typeof(MemberForceQueryResult) })
                Assert.That(type.IsSealed, Is.True);
        }
    }
}
