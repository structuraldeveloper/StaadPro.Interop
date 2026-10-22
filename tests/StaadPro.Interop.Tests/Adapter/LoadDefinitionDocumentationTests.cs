using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Models.Loads.Wind;
using StaadPro.Interop.Models.Loads.Seismic;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class LoadDefinitionDocumentationTests
    {
        private static readonly string[] Names =
        {
            "AddWindDefinitionASCE7Parameters", "ComputeWallWindPressureProfile", "ComputeWallWindPressureProfileASCE72016",
            "AddSeismicDefinition", "ModifySeismicDefinitionParam", "ModifySeismicDefinitionParams", "AddSeismicDefSelfWeight",
            "AddSeismicDefJointWeight", "AddSeismicDefMemberWeight"
        };
        private static string TypeId(Type t) => t.IsGenericType
            ? t.GetGenericTypeDefinition().FullName.Split('`')[0] + "{" + string.Join(",", t.GetGenericArguments().Select(TypeId)) + "}"
            : t.FullName;
        private static string Id(MethodInfo m) => "M:" + m.DeclaringType.FullName + "." + m.Name +
            (m.GetParameters().Length == 0 ? "" : "(" + string.Join(",", m.GetParameters().Select(p => TypeId(p.ParameterType))) + ")");

        [Test]
        public void AllFifteenOverloadsHaveCompleteIdenticalExpandedInterfaceAndAdapterDocumentation()
        {
            var methods = typeof(IOSLoad).GetMethods().Where(m => Names.Contains(m.Name)).ToArray();
            Assert.That(methods.Length, Is.EqualTo(15));
            var xml = XDocument.Load(Path.ChangeExtension(typeof(IOSLoad).Assembly.Location, ".xml"));
            var entries = xml.Descendants("member").ToDictionary(e => (string)e.Attribute("name"));
            foreach (var method in methods)
            {
                var concrete = typeof(OSLoadAdapter).GetMethod(method.Name, method.GetParameters().Select(p => p.ParameterType).ToArray());
                Assert.That(concrete, Is.Not.Null);
                Assert.That(concrete.ReturnType, Is.EqualTo(method.ReturnType));
                Assert.That(method.GetParameters().Any(p => p.ParameterType.IsByRef), Is.False);
                var a = entries[Id(method)]; var b = entries[Id(concrete)];
                Assert.That(b.Elements().Select(e => e.ToString()), Is.EqualTo(a.Elements().Select(e => e.ToString())), method.Name);
                foreach (string tag in new[] { "summary", "returns", "remarks", "example" })
                    Assert.That(a.Element(tag)?.Value, Is.Not.Null.And.Not.Empty, method.Name + " " + tag);
                Assert.That(a.Elements("param").Select(p => (string)p.Attribute("name")), Is.EqualTo(method.GetParameters().Select(p => p.Name)));
                Assert.That(a.Element("example").Element("code").Value, Does.Contain("load." + method.Name + "("));
                Assert.That(a.Element("example").Element("code").Value, Does.Contain("if (submitDefinition)"));
                Assert.That(a.Elements("exception").Count(), Is.GreaterThanOrEqualTo(5));
            }
            Assert.That(xml.ToString(), Does.Not.Contain("cref=\"!:"));
            Assert.That(xml.Descendants("include"), Is.Empty);
            Assert.That(xml.Descendants("inheritdoc"), Is.Empty);
        }

        [Test]
        public void SupportingPublicModelsAndEnumsHaveUsableCompiledDocumentation()
        {
            var xml = XDocument.Load(Path.ChangeExtension(typeof(IOSLoad).Assembly.Location, ".xml"));
            var entries = xml.Descendants("member").ToDictionary(e => (string)e.Attribute("name"));
            var modelTypes = typeof(IOSLoad).Assembly.GetExportedTypes().Where(t =>
                (t.Namespace == typeof(ASCE7BuildingData).Namespace && t != typeof(WindIntensityPair)) ||
                t.Namespace == typeof(SeismicCode).Namespace ||
                new[] { "ASCE7BuildingClassification", "ASCE7ExposureCategory", "ASCE7LoadingCode", "ASCE7Publication",
                    "ASCE7StructureType", "ASCE7WallType", "BuildingEnclosureClassification", "EscarpmentTopographyType",
                    "OpenSignOrientationType", "TankCrossSectionType", "TrussedTowerCrossSectionType", "WindLengthUnit", "WindVelocityUnit" }.Contains(t.Name));
            foreach (Type type in modelTypes)
            {
                Assert.That(entries["T:" + type.FullName].Element("summary")?.Value, Is.Not.Null.And.Not.Empty, type.FullName);
                foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                    Assert.That(entries["P:" + type.FullName + "." + property.Name].Element("summary")?.Value, Is.Not.Null.And.Not.Empty, property.Name);
                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly).Where(m => !m.IsSpecialName))
                {
                    var entry = entries[Id(method)];
                    Assert.That(entry.Element("summary")?.Value, Is.Not.Null.And.Not.Empty, Id(method));
                    Assert.That(entry.Elements("param").Select(e => (string)e.Attribute("name")), Is.EqualTo(method.GetParameters().Select(p => p.Name)), Id(method));
                    if (method.ReturnType != typeof(void)) Assert.That(entry.Element("returns")?.Value, Is.Not.Null.And.Not.Empty, Id(method));
                }
                if (type.IsEnum)
                    foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
                        Assert.That(entries["F:" + type.FullName + "." + field.Name].Element("summary")?.Value, Is.Not.Null.And.Not.Empty, field.Name);
            }
        }
    }
}
