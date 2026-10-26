using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Interfaces;
using StaadPro.Interop.Models;
using StaadPro.Interop.Services;

namespace StaadPro.Interop.Tests.Models
{
    [TestFixture, NonParallelizable]
    public class OpenStaadWrapperParityTests
    {
        public class NativeRoot
        {
            public object Geometry { get; set; } = new object();
            public object Load { get; set; } = new object();
            public object Output { get; set; } = new object();
            public object Property { get; set; } = new object();
            public object Support { get; set; } = new object();
            public object Design { get; set; } = new object();
            public object Command { get; set; } = new object();
            public object Commands { get; } = new object(); // Decoy: native spelling is singular.
            public object Table { get; set; } = new object();
            public object View { get; set; } = new object();
            public string VersionText = "26.00.00.340";
            public object BaseUnit = 2, ForceText = "kN", LengthText = "m", ForceReturn = 5, LengthReturn = 4, Handle = 12345;
            public bool FailUnits, ContradictVersion, FailVersion;
            public readonly List<string> Queries = new List<string>();
            public object GetBaseUnit() { Queries.Add("base"); return BaseUnit; }
            public object GetInputUnitForForce(ref object value)
            {
                Queries.Add("force");
                Assert.That(value, Is.TypeOf<string>());
                if (FailUnits) throw new COMException("Model transitioning");
                value = ForceText; return ForceReturn;
            }
            public object GetInputUnitForLength(ref object value)
            {
                Queries.Add("length");
                Assert.That(value, Is.TypeOf<string>());
                if (FailUnits) throw new COMException("Model transitioning");
                value = LengthText; return LengthReturn;
            }
            public object GetApplicationVersion(ref object major, ref object update, ref object revision, ref object build)
            {
                Queries.Add("version");
                if (FailVersion) throw new COMException("Native version failure", unchecked((int)0x80010108));
                if (VersionText != null && VersionText.Split('.').Length == 4 && VersionText.Split('.').All(p => int.TryParse(p, out _)))
                {
                    int[] values = VersionText.Split('.').Select(int.Parse).ToArray();
                    major = ContradictVersion ? values[0] + 1 : values[0]; update = values[1]; revision = values[2]; build = values[3];
                }
                return VersionText;
            }
            public object GetMainWindowHandle() { Queries.Add("window"); return Handle; }
        }

        public class WindowService : IStaadModelServices
        {
            public int Calls;
            public IntPtr GetStaadApplicationWindowHandle() { Calls++; return new IntPtr(987); }
        }

        public class Interaction : IStaadInteropInteractionService
        {
            public int Calls;
            public string SelectExistingStaadModelFile() { Calls++; return null; }
            public string SelectNewStaadModelFile(string fileName, string initialDirectory = null) { Calls++; return null; }
            public string SelectDirectory(string currentDirectoryPath = null) { Calls++; return null; }
            public void ShowInformation(string message, string title) { Calls++; }
            public void ShowWarning(string message, string title) { Calls++; }
            public void ShowError(string message, string title) { Calls++; }
        }

        public class MissingOptionalRoot
        {
            public object Geometry { get; } = new object();
            public object Load { get; } = new object();
            public int GetBaseUnit() => 2;
        }

        public class BrokenInterfaceRoot : NativeRoot
        {
            public new object Output => throw new COMException("Disconnected", unchecked((int)0x80010108));
        }

        private static readonly string[] MainPropertyNames = {
            "BaseUnitSystem", "InputForceUnit", "InputLengthUnit", "IsDedicated", "RawOpenStaad", "Root",
            "RawGeometry", "Geometry", "RawLoad", "Load", "RawOutput", "Output", "RawProperty", "Property",
            "RawSupport", "Support", "RawDesign", "Design", "RawCommands", "Commands", "RawTable", "RawView",
            "Table", "View", "IdeaStatiCa", "StaadVersion", "ApplicationVersion", "StaadEdition",
            "StaadApplicationWindowHandle", "StaadInteropInteractionService"
        };

        [Test]
        public void AllThirtySourcePropertiesAndPublicFunctionsArePresent()
        {
            var names = typeof(OpenStaadWrapper).GetProperties().Select(p => p.Name).ToArray();
            Assert.That(MainPropertyNames, Is.SubsetOf(names));
            Assert.That(names.Except(MainPropertyNames), Is.EquivalentTo(new[] { "IsConnected" }));
            foreach (string name in new[] { "RefreshComObjects", "RefreshUnits", "Dispose" })
                Assert.That(typeof(OpenStaadWrapper).GetMethod(name, Type.EmptyTypes), Is.Not.Null);
        }

        [Test]
        public void EveryNativeBindingAndPlaceholderRetainsTheCorrectObject()
        {
            var native = new NativeRoot();
            using (var wrapper = new OpenStaadWrapper(native, true))
            {
                foreach (string domain in new[] { "Geometry", "Load", "Output", "Property", "Support", "Design", "Commands", "Table", "View" })
                {
                    object expected = native.GetType().GetProperty(domain == "Commands" ? "Command" : domain).GetValue(native);
                    object actual = typeof(OpenStaadWrapper).GetProperty("Raw" + domain).GetValue(wrapper);
                    Assert.That(actual, Is.SameAs(expected), domain);
                    var adapter = (IOSBase)typeof(OpenStaadWrapper).GetProperty(domain).GetValue(wrapper);
                    Assert.That(adapter.Wrapper, Is.SameAs(wrapper), domain);
                    object com = adapter.GetType().GetProperty("ComObject").GetValue(adapter);
                    Assert.That(com, Is.SameAs(expected), domain);
                }
                Assert.That((object)wrapper.Root.ComObject, Is.SameAs(native));
                Assert.That(wrapper.IdeaStatiCa.Wrapper, Is.SameAs(wrapper));
                Assert.That(wrapper.IsDedicated, Is.True);
            }
        }

        [Test]
        public void PlaceholdersExposeNoUnportedDomainFunctions()
        {
            foreach (Type type in new[] { typeof(IOSDesign), typeof(IOSCommands), typeof(IOSTable), typeof(IOSView), typeof(IOpenStaadIdeaStatiCaAdapter) })
                Assert.That(type.GetMethods().Where(m => !m.IsSpecialName), Is.Empty, type.Name);
        }

        [Test]
        public void NativeVersionAndHostVersionHaveDistinctMeaningAndCachedReads()
        {
            var native = new NativeRoot();
            using (var wrapper = new OpenStaadWrapper(native))
            {
                Assert.That(wrapper.StaadVersion.Display, Is.EqualTo("v26.00.00.340"));
                Version hostVersion = (Assembly.GetEntryAssembly() ?? typeof(OpenStaadWrapper).Assembly).GetName().Version;
                Assert.That(wrapper.ApplicationVersion.Display, Is.EqualTo(ApplicationVersion.Parse(hostVersion).Display));
                Assert.That(wrapper.StaadEdition, Is.EqualTo(ApplicationEdition.Connect));
                Assert.That(wrapper.StaadApplicationWindowHandle, Is.EqualTo(new IntPtr(12345)));
                string[] calls = native.Queries.ToArray();
                var ignored = new object[] { wrapper.StaadVersion, wrapper.ApplicationVersion, wrapper.StaadEdition, wrapper.StaadApplicationWindowHandle, wrapper.BaseUnitSystem, wrapper.InputForceUnit, wrapper.InputLengthUnit };
                Assert.That(native.Queries, Is.EqualTo(calls));
                Assert.That(calls, Is.EqualTo(new[] { "version", "base", "force", "length", "window" }));
            }
        }

        [Test]
        public void RefreshUnits_UpdatesOnlyCachedUnits()
        {
            var native = new NativeRoot();
            using (var wrapper = new OpenStaadWrapper(native))
            {
                IOSRoot root = wrapper.Root; IOSGeometry geometry = wrapper.Geometry;
                native.BaseUnit = 1; native.ForceText = "lb"; native.LengthText = "mm";
                Assert.That(wrapper.BaseUnitSystem, Is.EqualTo(BaseUnitSystem.Metric));
                Assert.That(wrapper.InputLengthUnit, Is.EqualTo(LengthInputUnit.Meter));
                native.Queries.Clear(); wrapper.RefreshUnits();
                Assert.That(wrapper.BaseUnitSystem, Is.EqualTo(BaseUnitSystem.Imperial));
                Assert.That(wrapper.InputForceUnit, Is.EqualTo(ForceInputUnit.Pound));
                Assert.That(wrapper.InputLengthUnit, Is.EqualTo(LengthInputUnit.MilliMeter));
                Assert.That(native.Queries, Is.EqualTo(new[] { "base", "force", "length" }));
                Assert.That(wrapper.Root, Is.SameAs(root)); Assert.That(wrapper.Geometry, Is.SameAs(geometry));
            }
        }

        [Test]
        public void RefreshComObjects_ReplacesBindingsAndRereadsMetadata()
        {
            var native = new NativeRoot();
            using (var wrapper = new OpenStaadWrapper(native))
            {
                IOSRoot oldRoot = wrapper.Root; IOSOutput oldOutput = wrapper.Output;
                native.Output = new object(); native.Command = new object(); native.Geometry = new object(); native.VersionText = "25.00.01.424";
                native.Handle = 654; native.ForceText = "N"; native.LengthText = "ft";
                wrapper.RefreshComObjects();
                Assert.That(wrapper.Root, Is.Not.SameAs(oldRoot)); Assert.That(wrapper.Output, Is.Not.SameAs(oldOutput));
                Assert.That((object)wrapper.Output.ComObject, Is.SameAs(native.Output));
                Assert.That((object)wrapper.RawCommands, Is.SameAs(native.Command));
                Assert.That((object)wrapper.RawGeometry, Is.SameAs(native.Geometry));
                Assert.That(wrapper.StaadVersion.Display, Is.EqualTo("v25.00.01.424"));
                Assert.That(wrapper.StaadApplicationWindowHandle, Is.EqualTo(new IntPtr(654)));
                Assert.That(wrapper.InputForceUnit, Is.EqualTo(ForceInputUnit.Newton));
                Assert.That(wrapper.InputLengthUnit, Is.EqualTo(LengthInputUnit.Feet));
            }
        }

        [Test]
        public void OptionalMissingInterfacesAreNullAndIdeaRequiresItsThreeBindings()
        {
            using (var wrapper = new OpenStaadWrapper(new MissingOptionalRoot()))
            {
                Assert.That(wrapper.Output, Is.Null); Assert.That((object)wrapper.RawOutput, Is.Null);
                Assert.That(wrapper.IdeaStatiCa, Is.Null); Assert.That(wrapper.StaadVersion, Is.Null);
                Assert.That(wrapper.StaadEdition, Is.EqualTo(ApplicationEdition.None));
                Assert.That(wrapper.StaadApplicationWindowHandle, Is.EqualTo(IntPtr.Zero));
                Assert.That(wrapper.Root, Is.Not.Null);
            }
            var native = new NativeRoot();
            using (var wrapper = new OpenStaadWrapper(native))
            {
                native.Output = null; wrapper.RefreshComObjects(); Assert.That(wrapper.IdeaStatiCa, Is.Null);
                native.Output = new object(); native.Load = null; wrapper.RefreshComObjects(); Assert.That(wrapper.IdeaStatiCa, Is.Null);
            }
        }

        [TestCase(1, ForceInputUnit.KiloPound, LengthInputUnit.Feet)]
        [TestCase(2, ForceInputUnit.KiloNewton, LengthInputUnit.Meter)]
        public void UnitTransitionFailuresUseSourceBaseSystemFallback(int code, ForceInputUnit force, LengthInputUnit length)
        {
            using (var wrapper = new OpenStaadWrapper(new NativeRoot { BaseUnit = code, FailUnits = true }))
            { Assert.That(wrapper.InputForceUnit, Is.EqualTo(force)); Assert.That(wrapper.InputLengthUnit, Is.EqualTo(length)); }
        }

        [TestCase("25.00.01.424")]
        [TestCase("26.00.00.340")]
        public void NewerReturnCodeZeroIsAUnit_NotAnUnsuccessfulQuery(string version)
        {
            using (var wrapper = new OpenStaadWrapper(new NativeRoot { VersionText = version, ForceText = "", LengthText = "", ForceReturn = 0, LengthReturn = 0 }))
            { Assert.That(wrapper.InputForceUnit, Is.EqualTo(ForceInputUnit.KiloPound)); Assert.That(wrapper.InputLengthUnit, Is.EqualTo(LengthInputUnit.Inch)); }
        }

        [Test]
        public void LegacySuccessOrBooleanReturnDoesNotBecomeFeetOrPound()
        {
            foreach (var native in new[] {
                new NativeRoot { VersionText = "24.00.02.354", ForceText = "", LengthText = "", ForceReturn = 1, LengthReturn = 1 },
                new NativeRoot { ForceText = "", LengthText = "", ForceReturn = true, LengthReturn = true } })
            using (var wrapper = new OpenStaadWrapper(native))
            { Assert.That(wrapper.InputForceUnit, Is.EqualTo(ForceInputUnit.KiloNewton)); Assert.That(wrapper.InputLengthUnit, Is.EqualTo(LengthInputUnit.Meter)); }
        }

        [Test]
        public void NativeOutputTakesPrecedenceOverReturnCode()
        {
            using (var wrapper = new OpenStaadWrapper(new NativeRoot { ForceText = "N", LengthText = "mm", ForceReturn = 0, LengthReturn = 0 }))
            { Assert.That(wrapper.InputForceUnit, Is.EqualTo(ForceInputUnit.Newton)); Assert.That(wrapper.InputLengthUnit, Is.EqualTo(LengthInputUnit.MilliMeter)); }
        }

        [Test]
        public void InjectedServicesAreCapturedAndOnlyConnectRequestsAWindow()
        {
            var interaction = new Interaction(); var service = new WindowService(); var native = new NativeRoot();
            using (var wrapper = new OpenStaadWrapper(native, false, service, interaction))
            {
                Assert.That(wrapper.StaadInteropInteractionService, Is.SameAs(interaction));
                Assert.That(wrapper.StaadApplicationWindowHandle, Is.EqualTo(new IntPtr(987)));
                Assert.That(service.Calls, Is.EqualTo(1)); Assert.That(native.Queries, Does.Not.Contain("window"));
                Assert.That(interaction.Calls, Is.Zero);
            }
            using (var wrapper = new OpenStaadWrapper(new NativeRoot { VersionText = "20.07.11.82" }, false, service, interaction))
            { Assert.That(wrapper.StaadEdition, Is.EqualTo(ApplicationEdition.V8i)); Assert.That(wrapper.StaadApplicationWindowHandle, Is.EqualTo(IntPtr.Zero)); Assert.That(service.Calls, Is.EqualTo(1)); }
        }

        [Test]
        public void HostDefaultIsNoninteractiveAndExistingWrappersKeepTheirService()
        {
            IStaadInteropInteractionService previous = StaadInteropHostServices.Interaction;
            try
            {
                var interaction = new Interaction(); StaadInteropHostServices.Interaction = interaction;
                using (var wrapper = new OpenStaadWrapper(new NativeRoot()))
                {
                    StaadInteropHostServices.Interaction = null;
                    Assert.That(wrapper.StaadInteropInteractionService, Is.SameAs(interaction));
                    Assert.That(StaadInteropHostServices.Interaction.SelectExistingStaadModelFile(), Is.Null);
                    Assert.That(interaction.Calls, Is.Zero);
                }
            }
            finally { StaadInteropHostServices.Interaction = previous; }
        }

        [Test]
        public void InjectionRejectsMissingServicesBeforeNativeQueries()
        {
            var native = new NativeRoot();
            Assert.Throws<ArgumentNullException>(() => new OpenStaadWrapper(native, false, null, new Interaction()));
            Assert.Throws<ArgumentNullException>(() => new OpenStaadWrapper(native, false, new WindowService(), null));
            Assert.That(native.Queries, Is.Empty);
        }

        [TestCase(null)]
        [TestCase("bad")]
        [TestCase("26.0.0")]
        [TestCase("26.0.0.999999999999999")]
        public void MalformedNativeVersionCannotMasqueradeAsAValidStaadVersion(string text)
            => Assert.Throws<InvalidOperationException>(() => new OpenStaadWrapper(new NativeRoot { VersionText = text }));

        [Test]
        public void ContradictoryNativeVersionOutputsAreRejected()
            => Assert.Throws<InvalidOperationException>(() => new OpenStaadWrapper(new NativeRoot { ContradictVersion = true }));

        [Test]
        public void NativeVersionAndInterfaceFailuresPropagate()
        {
            Assert.Throws<COMException>(() => new OpenStaadWrapper(new NativeRoot { FailVersion = true }));
            Assert.Throws<COMException>(() => new OpenStaadWrapper(new BrokenInterfaceRoot()));
        }

        [Test]
        public void BoundHandlesAreIndependentAcrossInstances()
        {
            using (var first = new OpenStaadWrapper(new NativeRoot { Handle = 101 }))
            using (var second = new OpenStaadWrapper(new NativeRoot { Handle = 202, VersionText = "25.0.1.424" }))
            { Assert.That(first.StaadApplicationWindowHandle, Is.EqualTo(new IntPtr(101))); Assert.That(second.StaadApplicationWindowHandle, Is.EqualTo(new IntPtr(202))); }
        }

        [TestCase("123")]
        [TestCase(true)]
        public void HandleRejectsCoercibleNonintegerTypes(object value)
            => Assert.Throws<InvalidOperationException>(() => new OpenStaadWrapper(new NativeRoot { Handle = value }));

        [Test]
        public void HandleSupportsPointerAndZeroButRejectsSentinelsAndOverflow()
        {
            foreach (object value in new object[] { 0, new IntPtr(73), 73L, 73U })
            using (var wrapper = new OpenStaadWrapper(new NativeRoot { Handle = value }))
                Assert.That(wrapper.StaadApplicationWindowHandle.ToInt64(), Is.EqualTo(value is IntPtr p ? p.ToInt64() : Convert.ToInt64(value)));
            foreach (object value in new object[] { -1, new IntPtr(-1), ulong.MaxValue })
                Assert.Throws<InvalidOperationException>(() => new OpenStaadWrapper(new NativeRoot { Handle = value }));
        }

        [Test]
        public void DisposeClearsEveryBindingAndRefreshCannotResurrectIt()
        {
            var native = new NativeRoot(); var wrapper = new OpenStaadWrapper(native);
            var version = wrapper.StaadVersion; string[] calls = native.Queries.ToArray();
            wrapper.Dispose(); wrapper.Dispose();
            foreach (PropertyInfo property in typeof(OpenStaadWrapper).GetProperties().Where(p => p.Name.StartsWith("Raw") || new[] { "Root", "Geometry", "Load", "Output", "Property", "Support", "Design", "Commands", "Table", "View", "IdeaStatiCa" }.Contains(p.Name)))
                Assert.That(property.GetValue(wrapper), Is.Null, property.Name);
            Assert.That(wrapper.IsConnected, Is.False); Assert.That(wrapper.StaadVersion.Display, Is.EqualTo(version.Display));
            Assert.Throws<ObjectDisposedException>(() => wrapper.RefreshComObjects());
            Assert.Throws<ObjectDisposedException>(() => wrapper.RefreshUnits());
            Assert.That(native.Queries, Is.EqualTo(calls));
        }

        [Test]
        public void EveryWrapperPublicMemberHasCompiledXmlDocumentation()
        {
            string xmlPath = Path.ChangeExtension(typeof(OpenStaadWrapper).Assembly.Location, ".xml");
            XDocument document = XDocument.Load(xmlPath);
            foreach (string property in MainPropertyNames.Concat(new[] { "IsConnected" }))
                Assert.That(document.Descendants("member").Any(m => (string)m.Attribute("name") == "P:StaadPro.Interop.Models.OpenStaadWrapper." + property && !string.IsNullOrWhiteSpace(m.Element("summary")?.Value)), Is.True, property);
            foreach (string method in new[] { "RefreshComObjects", "RefreshUnits", "Dispose" })
                Assert.That(document.Descendants("member").Any(m => (string)m.Attribute("name") == "M:StaadPro.Interop.Models.OpenStaadWrapper." + method && m.Element("summary") != null), Is.True, method);
        }

        [Test]
        public void EditingReturnedNativeVersionCannotAlterRoutingEvidence()
        {
            using (var wrapper = new OpenStaadWrapper(new NativeRoot { VersionText = "24.00.02.354", ForceText = "", ForceReturn = 1 }))
            {
                wrapper.StaadVersion.Major = 26;
                Assert.That(wrapper.StaadVersion.Major, Is.EqualTo(24));
                wrapper.RefreshUnits();
                Assert.That(wrapper.InputForceUnit, Is.EqualTo(ForceInputUnit.KiloNewton));
            }
        }
    }
}
