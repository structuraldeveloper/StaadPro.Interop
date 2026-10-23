
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Models.Loads.Wind;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class LoadPopulationAndWindDocumentationTests
    {
        [TestCase("PopulateMemberUniformlyDistributedLoads", typeof(System.Collections.Generic.IEnumerable<ILoadCase>))]
        [TestCase("PopulateMemberUniformMoments", typeof(System.Collections.Generic.IEnumerable<ILoadCase>))]
        [TestCase("PopulateNodalLoads", typeof(System.Collections.Generic.IEnumerable<ILoadCase>))]
        [TestCase("AddWindDefinition", typeof(string))]
        [TestCase("AddWindExposure", typeof(System.Collections.Generic.IEnumerable<int>))]
        [TestCase("AddWindExposure", typeof(System.Collections.Generic.IEnumerable<Node>))]
        [TestCase("AddWindExposure", typeof(int))]
        [TestCase("AddWindIntensity", typeof(double))]
        [TestCase("AddWindIntensity", typeof(System.Collections.Generic.IEnumerable<double>))]
        [TestCase("AddWindIntensity", typeof(System.Collections.Generic.IEnumerable<WindIntensityPair>))]
        [TestCase("DeleteWindDefinition", typeof(int))]
        [TestCase("DeleteAllWindDefinitions", typeof(void))]
        public void NewLoadOverloadsHaveMatchingCompleteExecutableIntelliSense(string name, Type lastParameterType)
        {
            var contract = typeof(IOSLoad).GetMethods().Single(method => method.Name == name &&
                (method.GetParameters().Length == 0 ? lastParameterType == typeof(void) : method.GetParameters().Last().ParameterType == lastParameterType));
            var adapter = typeof(OSLoadAdapter).GetMethod(name, contract.GetParameters().Select(parameter => parameter.ParameterType).ToArray());
            Assert.That(adapter, Is.Not.Null);
            Assert.That(adapter.ReturnType, Is.EqualTo(contract.ReturnType));
            Assert.That(adapter.GetParameters().Select(p => p.Name), Is.EqualTo(contract.GetParameters().Select(p => p.Name)));
            Assert.That(adapter.GetParameters().Select(p => p.DefaultValue), Is.EqualTo(contract.GetParameters().Select(p => p.DefaultValue)));
            Assert.That(contract.GetParameters().Any(parameter => parameter.ParameterType.IsByRef), Is.False);
            var xml = XDocument.Load(Path.ChangeExtension(typeof(IOSLoad).Assembly.Location, ".xml"));
            var entries = xml.Descendants("member").ToDictionary(e => (string)e.Attribute("name"));
            var first = entries[MethodId(contract)]; var second = entries[MethodId(adapter)];
            Assert.That(second.Elements().Select(element => element.ToString()), Is.EqualTo(first.Elements().Select(element => element.ToString())));
            foreach (string tag in new[] { "summary", "param", "returns", "remarks", "example" })
                if (tag != "param" || contract.GetParameters().Length != 0)
                    Assert.That(first.Element(tag)?.Value, Is.Not.Null.And.Not.Empty);
            Assert.That(first.Elements("param").Select(p => (string)p.Attribute("name")), Is.EqualTo(contract.GetParameters().Select(p => p.Name)));
            Assert.That(first.Element("example").Element("code").Value, Does.Contain("load." + name + "("));
            Assert.That(first.Elements("exception").Count(), Is.GreaterThanOrEqualTo(3));
            Assert.That(first.Descendants().Attributes("cref").Any(attribute => attribute.Value.StartsWith("!:")), Is.False);
        }

        [Test]
        public void LoadInterfaceAndAdapterExposeMatchingOverloadsAndDeletionDefaultIsExplicit()
        {
            var methods = typeof(IOSLoad).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Where(method => !method.IsSpecialName).ToList();
            Assert.That(methods.Count, Is.EqualTo(171));
            foreach (var method in methods)
                Assert.That(typeof(OSLoadAdapter).GetMethod(method.Name, method.GetParameters().Select(p => p.ParameterType).ToArray()), Is.Not.Null);
            Assert.That(typeof(IOSLoad).GetMethod("DeleteWindDefinition").GetParameters()[0].DefaultValue, Is.EqualTo(0));
            Assert.That(typeof(WindIntensityPair).GetProperties().All(property => property.SetMethod == null), Is.True);
        }

        private static string TypeId(Type type)
        {
            if (!type.IsGenericType) return type.FullName;
            return type.GetGenericTypeDefinition().FullName.Split('`')[0] + "{" + string.Join(",", type.GetGenericArguments().Select(TypeId)) + "}";
        }

        private static string MethodId(MethodInfo method) => "M:" + method.DeclaringType.FullName + "." + method.Name +
            (method.GetParameters().Length == 0 ? "" : "(" + string.Join(",", method.GetParameters().Select(parameter => TypeId(parameter.ParameterType))) + ")");
    }
}
