using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Services;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class ApiDocumentationTests
    {
        [Test]
        public void AllExportedFunctionsHaveExpandedSummariesParametersAndReturnDocumentation()
        {
            var assembly = typeof(IOSGeometry).Assembly;
            var xml = XDocument.Load(Path.ChangeExtension(assembly.Location, ".xml"));
            var entries = xml.Descendants("member").ToDictionary(m => (string)m.Attribute("name"));
            foreach (var type in assembly.GetExportedTypes())
            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(m => !m.IsSpecialName))
            {
                string id = MethodId(method);
                Assert.That(entries.ContainsKey(id), Is.True, id);
                var entry = entries[id];
                Assert.That(entry.Element("summary")?.Value.Trim(), Is.Not.Null.And.Not.Empty, id);
                Assert.That(entry.Elements("param").Select(p => (string)p.Attribute("name")), Is.EquivalentTo(method.GetParameters().Select(p => p.Name)), id);
                Assert.That(entry.Elements("param").All(p => !string.IsNullOrWhiteSpace(p.Value)), Is.True, id);
                if (method.ReturnType != typeof(void))
                    Assert.That(entry.Element("returns")?.Value.Trim(), Is.Not.Null.And.Not.Empty, id);
                Assert.That(entry.Descendants("include"), Is.Empty, id);
                Assert.That(entry.Descendants("inheritdoc"), Is.Empty, id);
                Assert.That(entry.Descendants().Attributes("cref").Any(c => c.Value.StartsWith("!:")), Is.False, id);
            }
        }

        [TestCase(typeof(IOSGeometry))]
        [TestCase(typeof(OSGeometryAdapter))]
        [TestCase(typeof(IOSLoad))]
        [TestCase(typeof(OSLoadAdapter))]
        [TestCase(typeof(ConnectivityService))]
        public void EveryAdapterFunctionHasUsageRemarksAndGenericParameterDocumentation(Type type)
        {
            var xml = XDocument.Load(Path.ChangeExtension(type.Assembly.Location, ".xml"));
            var entries = xml.Descendants("member").ToDictionary(m => (string)m.Attribute("name"));
            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(m => !m.IsSpecialName))
            {
                var entry = entries[MethodId(method)];
                Assert.That(entry.Element("remarks")?.Value.Trim(), Is.Not.Null.And.Not.Empty, MethodId(method));
                Assert.That(entry.Elements("typeparam").Select(p => (string)p.Attribute("name")),
                    Is.EquivalentTo(method.GetGenericArguments().Select(p => p.Name)), MethodId(method));
            }
        }

        [TestCase("SegregateBeamsByConnectivity")]
        [TestCase("SegregateMembersByConnectivity")]
        [TestCase("SegregatePlatesByConnectivity")]
        public void ConnectivityFunctionsHaveMatchingInterfaceAndConcreteIntelliSense(string name)
        {
            var xml = XDocument.Load(Path.ChangeExtension(typeof(IOSGeometry).Assembly.Location, ".xml"));
            var entries = xml.Descendants("member").ToDictionary(m => (string)m.Attribute("name"));
            var contract = entries[MethodId(typeof(IOSGeometry).GetMethod(name))];
            var adapter = entries[MethodId(typeof(OSGeometryAdapter).GetMethod(name))];
            Assert.That(adapter.Elements().Select(e => e.ToString()), Is.EqualTo(contract.Elements().Select(e => e.ToString())));
            Assert.That(contract.Element("example")?.Element("code")?.Value, Does.Contain("geometry." + name));
            Assert.That(contract.Elements("exception").Any(e => ((string)e.Attribute("cref")).EndsWith("ArgumentNullException")), Is.True);
        }

        private static string MethodId(MethodInfo method)
        {
            string name = "M:" + method.DeclaringType.FullName.Replace('+', '.') + "." + method.Name;
            if (method.IsGenericMethod) name += "``" + method.GetGenericArguments().Length;
            var parameters = method.GetParameters();
            if (parameters.Length > 0) name += "(" + string.Join(",", parameters.Select(p => TypeId(p.ParameterType))) + ")";
            return name;
        }

        private static string TypeId(Type type)
        {
            if (type.IsByRef) return TypeId(type.GetElementType()) + "@";
            if (type.IsArray) return TypeId(type.GetElementType()) + (type.GetArrayRank() == 1 ? "[]" : "[" + string.Join(",", Enumerable.Repeat("0:", type.GetArrayRank())) + "]");
            if (type.IsGenericParameter) return (type.DeclaringMethod == null ? "`" : "``") + type.GenericParameterPosition;
            if (!type.IsGenericType) return type.FullName.Replace('+', '.');
            string name = type.GetGenericTypeDefinition().FullName.Replace('+', '.');
            name = name.Substring(0, name.LastIndexOf('`'));
            return name + "{" + string.Join(",", type.GetGenericArguments().Select(TypeId)) + "}";
        }
    }
}
