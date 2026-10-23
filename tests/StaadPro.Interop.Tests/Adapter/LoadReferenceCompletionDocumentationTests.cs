using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Entities;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class LoadReferenceCompletionDocumentationTests
    {
        private static readonly string[] Names =
        {
            "AddSeismicDefMemberWeight", "AddSeismicDefMemberUniformWeight", "AddSeismicDefMemberConcentratedWeight",
            "AddSeismicDefWallArea", "AddResponseSpectrumLoadEx", "AddDirectAnalysisDefinitionParameter",
            "AddDirectAnalysisFlexParameter", "AddDirectAnalysisAxialParameter",
            "DeleteDirectAnalysisDefinitionParameter", "DeleteDirectAnalysisDefinition", "AddRSLoad"
        };
        private static string TypeId(Type t) => t.IsGenericType
            ? t.GetGenericTypeDefinition().FullName.Split('`')[0] + "{" + string.Join(",", t.GetGenericArguments().Select(TypeId)) + "}"
            : t.FullName;
        private static string Id(MethodInfo m) => "M:" + m.DeclaringType.FullName + "." + m.Name +
            (m.GetParameters().Length == 0 ? "" : "(" + string.Join(",", m.GetParameters().Select(p => TypeId(p.ParameterType))) + ")");

        [Test]
        public void FinalFifteenOverloadsHaveMatchingCompleteIntelliSenseAndGuardedExamples()
        {
            var methods = typeof(IOSLoad).GetMethods().Where(m => Names.Contains(m.Name) &&
                !(m.Name == "AddSeismicDefMemberWeight" && m.GetParameters().Last().ParameterType != typeof(System.Collections.Generic.IEnumerable<Beam>))).ToArray();
            Assert.That(methods.Length, Is.EqualTo(15));
            var xml = XDocument.Load(Path.ChangeExtension(typeof(IOSLoad).Assembly.Location, ".xml"));
            var entries = xml.Descendants("member").ToDictionary(e => (string)e.Attribute("name"));
            foreach (var method in methods)
            {
                var adapter = typeof(OSLoadAdapter).GetMethod(method.Name, method.GetParameters().Select(p => p.ParameterType).ToArray());
                Assert.That(adapter, Is.Not.Null);
                Assert.That(adapter.ReturnType, Is.EqualTo(method.ReturnType));
                Assert.That(adapter.GetParameters().Select(p => p.Name), Is.EqualTo(method.GetParameters().Select(p => p.Name)));
                var contractDoc = entries[Id(method)]; var adapterDoc = entries[Id(adapter)];
                Assert.That(adapterDoc.Elements().Select(e => e.ToString()), Is.EqualTo(contractDoc.Elements().Select(e => e.ToString())));
                foreach (string tag in new[] { "summary", "returns", "remarks", "example" })
                    Assert.That(contractDoc.Element(tag)?.Value, Is.Not.Null.And.Not.Empty, method.Name + " " + tag);
                Assert.That(contractDoc.Elements("param").Select(p => (string)p.Attribute("name")), Is.EqualTo(method.GetParameters().Select(p => p.Name)));
                Assert.That(contractDoc.Element("example").Element("code").Value, Does.Contain("load." + method.Name + "("));
                Assert.That(contractDoc.Element("example").Element("code").Value, Does.Contain("if (submitDefinition)"));
                Assert.That(method.GetParameters().Any(p => p.ParameterType.IsByRef), Is.False);
                Assert.That(contractDoc.Elements("exception").Count(), Is.GreaterThanOrEqualTo(5));
            }
            Assert.That(typeof(IOSLoad).GetMethods().Count(m => !m.IsSpecialName), Is.EqualTo(171));
        }
    }
}
