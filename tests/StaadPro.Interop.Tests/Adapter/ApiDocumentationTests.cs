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
        [TestCase(typeof(IOSRoot))]
        [TestCase(typeof(OSRootAdapter))]
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

        [TestCase(typeof(IOSGeometry), typeof(OSGeometryAdapter), "CreateMultipleBeamsExtended", "CreateBeam")]
        [TestCase(typeof(IOSGeometry), typeof(OSGeometryAdapter), "CreateMultipleNodesExtended", "CreateMultipleNodes")]
        [TestCase(typeof(IOSGeometry), typeof(OSGeometryAdapter), "MergeNodesExtended", "MergeNodes")]
        [TestCase(typeof(IOSLoad), typeof(OSLoadAdapter), "AddReferenceLoadsTo", "AddReferenceLoad")]
        [TestCase(typeof(IOSLoad), typeof(OSLoadAdapter), "AddRepeatLoadsTo", "AddRepeatLoad")]
        [TestCase(typeof(IOSLoad), typeof(OSLoadAdapter), "AddAxialThermalGradientTo", "AddTemperatureLoad")]
        public void NewMutationFunctionsHaveMatchingExpandedIntelliSenseAndExamples(Type contractType, Type adapterType, string name, string nativeName)
        {
            var xml = XDocument.Load(Path.ChangeExtension(contractType.Assembly.Location, ".xml"));
            var entries = xml.Descendants("member").ToDictionary(m => (string)m.Attribute("name"));
            var contractMethod = contractType.GetMethods().Single(m => m.Name == name && (name != "AddAxialThermalGradientTo" || m.GetParameters().Length == 3));
            var adapterMethod = adapterType.GetMethods().Single(m => m.Name == name && (name != "AddAxialThermalGradientTo" || m.GetParameters().Length == 3));
            Assert.That(adapterMethod.GetParameters().Select(p => (p.Name, p.ParameterType, p.DefaultValue)),
                Is.EqualTo(contractMethod.GetParameters().Select(p => (p.Name, p.ParameterType, p.DefaultValue))));
            var contract = entries[MethodId(contractMethod)];
            var adapter = entries[MethodId(adapterMethod)];
            Assert.That(adapter.Elements().Select(e => e.ToString()), Is.EqualTo(contract.Elements().Select(e => e.ToString())));
            Assert.That(contract.Element("remarks").Value, Does.Contain(nativeName));
            Assert.That(contract.Element("example")?.Element("code")?.Value, Does.Contain("." + name + "("));
            Assert.That(contract.Elements("exception").Count(), Is.GreaterThanOrEqualTo(3));
        }

        [TestCase("AddAxialThermalGradientTo", 4, "AddTemperatureLoad")]
        [TestCase("AddMemberLoadsFromGroupsByLoadCase", 1, "ApplyTo")]
        [TestCase("AddNodalLoadsFromGroupsByLoadCase", 1, "Fx, Fy, Fz, Mx, My, Mz")]
        public void NextLoadFunctionsHaveMatchingInterfaceAndConcreteContracts(string name, int parameterCount, string mapping)
        {
            var xml = XDocument.Load(Path.ChangeExtension(typeof(IOSLoad).Assembly.Location, ".xml"));
            var entries = xml.Descendants("member").ToDictionary(m => (string)m.Attribute("name"));
            var contractMethod = typeof(IOSLoad).GetMethods().Single(m => m.Name == name && m.GetParameters().Length == parameterCount);
            var adapterMethod = typeof(OSLoadAdapter).GetMethods().Single(m => m.Name == name && m.GetParameters().Length == parameterCount);
            var contract = entries[MethodId(contractMethod)];
            var adapter = entries[MethodId(adapterMethod)];
            Assert.That(adapter.Elements().Select(e => e.ToString()), Is.EqualTo(contract.Elements().Select(e => e.ToString())));
            Assert.That(adapterMethod.GetParameters().Select(p => (p.Name, p.ParameterType, p.DefaultValue)),
                Is.EqualTo(contractMethod.GetParameters().Select(p => (p.Name, p.ParameterType, p.DefaultValue))));
            Assert.That(contract.Element("remarks").Value, Does.Contain(mapping));
            Assert.That(contract.Element("example")?.Element("code")?.Value, Does.Contain("load." + name + "("));
            if (parameterCount == 4) Assert.That(contract.Elements("param").Single(p => (string)p.Attribute("name") == "nThreads").Value, Does.Contain("does not launch worker threads"));
        }

        [TestCase(typeof(IOSLoad), typeof(OSLoadAdapter), "AddSelfweightInXYZTo", "load")]
        [TestCase(typeof(IOSLoad), typeof(OSLoadAdapter), "CreateLoadEnvelopExt", "load")]
        [TestCase(typeof(IOSLoad), typeof(OSLoadAdapter), "CreateLoadList", "load")]
        [TestCase(typeof(IOSRoot), typeof(OSRootAdapter), "GetStaadFileFullPath", "root")]
        [TestCase(typeof(IOSRoot), typeof(OSRootAdapter), "GetStaadFileNameOnly", "root")]
        [TestCase(typeof(IOSRoot), typeof(OSRootAdapter), "GetAnlFileFullPath", "root")]
        [TestCase(typeof(IOSRoot), typeof(OSRootAdapter), "GetOpenedStaadFilename", "root")]
        [TestCase(typeof(IOSRoot), typeof(OSRootAdapter), "IsTarget", "root")]
        [TestCase(typeof(IOSRoot), typeof(OSRootAdapter), "ModelIsOpen", "root")]
        [TestCase(typeof(IOSRoot), typeof(OSRootAdapter), "GetShortJobInfo", "root")]
        [TestCase(typeof(IOSRoot), typeof(OSRootAdapter), "GetFullJobInfo", "root")]
        [TestCase(typeof(IOSRoot), typeof(OSRootAdapter), "SetInputUnitsEx", "root")]
        [TestCase(typeof(IOSRoot), typeof(OSRootAdapter), "SetSilentMode", "root")]
        [TestCase(typeof(IOSRoot), typeof(OSRootAdapter), "SaveModel", "root")]
        [TestCase(typeof(IOSRoot), typeof(OSRootAdapter), "CloseSTAADFile", "root")]
        public void LoadAndRootPortHasMatchingCompleteExecutableIntelliSense(Type contractType, Type adapterType, string name, string receiver)
        {
            var xml = XDocument.Load(Path.ChangeExtension(contractType.Assembly.Location, ".xml"));
            var entries = xml.Descendants("member").ToDictionary(m => (string)m.Attribute("name"));
            foreach (var method in contractType.GetMethods().Where(m => m.Name == name))
            {
            var adapterMethod = adapterType.GetMethod(name, method.GetParameters().Select(p => p.ParameterType).ToArray());
            var contract = entries[MethodId(method)];
            var adapter = entries[MethodId(adapterMethod)];
            Assert.That(adapter.Elements().Select(e => e.ToString()), Is.EqualTo(contract.Elements().Select(e => e.ToString())));
            Assert.That(method.ReturnType, Is.EqualTo(adapterMethod.ReturnType));
            Assert.That(adapterMethod.GetParameters().Select(p => (p.Name, p.ParameterType, p.DefaultValue)), Is.EqualTo(method.GetParameters().Select(p => (p.Name, p.ParameterType, p.DefaultValue))));
            Assert.That(contract.Element("example")?.Element("code")?.Value, Does.Contain(receiver + "." + name + "("));
            Assert.That(contract.Elements("exception").All(e => !string.IsNullOrWhiteSpace(e.Value)), Is.True);
            if (name == "SetSilentMode") Assert.That(contract.Elements("exception").Count(), Is.EqualTo(1));
            else if (name == "CloseSTAADFile") Assert.That(contract.Elements("exception").Count(), Is.EqualTo(2));
            else Assert.That(contract.Elements("exception").Count(), Is.GreaterThanOrEqualTo(3));
            }
        }

        [TestCase("SelfWeightDirection", 4)]
        [TestCase("LoadListType", 3)]
        [TestCase("SilentMode", 3)]
        public void NativeSelectionEnumsHaveDocumentedTypesAndAllValues(string typeName, int count)
        {
            var xml = XDocument.Load(Path.ChangeExtension(typeof(IOSLoad).Assembly.Location, ".xml"));
            string owner = "StaadPro.Interop.Enums." + typeName;
            var entries = xml.Descendants("member").Where(e => ((string)e.Attribute("name")).Substring(2) == owner || ((string)e.Attribute("name")).Substring(2).StartsWith(owner + ".")).ToList();
            Assert.That(entries.Count, Is.EqualTo(count));
            Assert.That(entries.All(e => !string.IsNullOrWhiteSpace(e.Element("summary")?.Value)), Is.True);
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
