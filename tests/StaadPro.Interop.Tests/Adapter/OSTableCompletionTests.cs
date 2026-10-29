using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Media;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums.Table;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Table;
using Native = PackageVerification.TableFoundationFixture.Native;
using Root = PackageVerification.TableFoundationFixture.Root;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSTableCompletionTests
    {
        private static readonly string[] Names = { "SetCellTextColor", "SetCellTextColorHsl", "SetCellTextHorizontalAlignment", "SetCellTextVerticalAlignment", "SetCellTextSizeForTable" };
        private static MethodInfo[] Methods => typeof(IOSTable).GetMethods().Where(m => Names.Contains(m.Name)).OrderBy(m => m.Name).ThenBy(m => string.Join(",", m.GetParameters().Select(p => p.ParameterType.FullName))).ToArray();
        public static IEnumerable<TestCaseData> Ports => Enumerable.Range(0, Methods.Length).Select(i => new TestCaseData(i).SetName("TableCompletion_" + Methods[i].Name + "_" + i));
        private static object[] Arguments(MethodInfo m) => m.GetParameters().Select(p =>
            p.ParameterType == typeof(Color) ? (object)Color.FromArgb(12, 17, 34, 51) : p.ParameterType == typeof(string) ? "#0C112233" :
            p.ParameterType == typeof(TableHorizontalAlignment) ? TableHorizontalAlignment.Right : p.ParameterType == typeof(TableVerticalAlignment) ? TableVerticalAlignment.Middle :
            p.ParameterType == typeof(float) ? (object)(p.Name == "hue" ? 0f : p.Name == "saturation" ? 1f : 0.5f) : p.ParameterType == typeof(double) ? 12.5 :
            p.ParameterType == typeof(byte) ? (object)(byte)(p.Name == "alpha" ? 12 : p.Name == "red" ? 17 : p.Name == "green" ? 34 : 51) :
            (object)(p.Name == "reportId" ? 11 : p.Name == "tableId" ? 7 : p.Name == "rowNumber" ? 1 : p.Name == "columnNumber" ? 2 : p.Name == "red" ? 17 : p.Name == "green" ? 34 : p.Name == "blue" ? 51 : 0x0C112233)).ToArray();
        private static object Invoke(IOSTable api, int index, object[] args = null)
        {
            try { return Methods[index].Invoke(api, args ?? Arguments(Methods[index])); }
            catch (TargetInvocationException e) { ExceptionDispatchInfo.Capture(e.InnerException).Throw(); throw; }
        }
        private static string Target(string name) => name == "SetCellTextHorizontalAlignment" ? "SetCellTextHorzAlignment" : name == "SetCellTextVerticalAlignment" ? "SetCellTextVertAlignment" : name == "SetCellTextSizeForTable" ? "SetCellTextSizeAll" : "SetCellTextColor";

        [Test]
        public void TableHasAllTwentyNineManagedOverloadsAndCompletionHasNine()
        {
            Assert.That(typeof(IOSTable).GetMethods().Count(m => !m.IsSpecialName), Is.EqualTo(29));
            Assert.That(Methods.Length, Is.EqualTo(9));
            var index = JObject.Parse(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "ContractReview", "TableFoundationContractIndex.json")));
            Assert.That(index["functions"].Count(), Is.EqualTo(29));
            foreach (var method in Methods)
                Assert.That(index["functions"].Any(f => (string)f["managedName"] == method.Name && ((string)f["parameters"]).Split(',').Select(p => p.Trim().Split(' ').Last()).SequenceEqual(method.GetParameters().Select(p => p.Name))), Is.True);
        }

        [TestCaseSource(nameof(Ports))]
        public void EachOverloadHasMatchingCompleteXmlAndManagedSignature(int index)
        {
            var method = Methods[index];
            var implementation = typeof(OSTableAdapter).GetMethod(method.Name, method.GetParameters().Select(p => p.ParameterType).ToArray());
            Assert.That(implementation.ReturnType, Is.EqualTo(typeof(TableOperationResult)));
            Assert.That(implementation.GetParameters().Select(p => p.Name), Is.EqualTo(method.GetParameters().Select(p => p.Name)));
            Assert.That(method.GetParameters().Any(p => p.IsOut || p.ParameterType.IsByRef || p.ParameterType == typeof(object)), Is.False);
            var xml = XDocument.Load(Path.ChangeExtension(typeof(IOSTable).Assembly.Location, ".xml"));
            var entry = xml.Descendants("member").Single(m => ((string)m.Attribute("name")).StartsWith("M:" + typeof(IOSTable).FullName + "." + method.Name + "(") && m.Elements("param").Select(p => (string)p.Attribute("name")).SequenceEqual(method.GetParameters().Select(p => p.Name)));
            var adapter = xml.Descendants("member").Single(m => (string)m.Attribute("name") == ((string)entry.Attribute("name")).Replace(typeof(IOSTable).FullName, typeof(OSTableAdapter).FullName));
            Assert.That(adapter.Elements().Select(x => x.ToString()), Is.EqualTo(entry.Elements().Select(x => x.ToString())));
            foreach (string tag in new[] { "summary", "returns", "remarks", "example" }) Assert.That(entry.Element(tag)?.Value, Is.Not.Null.And.Not.Empty);
            Assert.That(entry.Element("remarks").Value, Does.Contain("Unlike"));
        }

        [TestCaseSource(nameof(Ports))]
        public void OverloadsDispatchOneExactPacketOnCallerThread(int index)
        {
            var native = new Native();
            using (var wrapper = new OpenStaadWrapper(new Root { Table = native }))
            {
                var receipt = (TableOperationResult)Invoke(wrapper.Table, index);
                var name = Methods[index].Name;
                object[] expected = name == "SetCellTextSizeForTable" ? new object[] { 11, 7, 12.5 } : name == "SetCellTextHorizontalAlignment" ? new object[] { 11, 7, 1, 2, 2 } : name == "SetCellTextVerticalAlignment" ? new object[] { 11, 7, 1, 2, 4 } : name == "SetCellTextColorHsl" ? new object[] { 11, 7, 1, 2, 255, 0, 0 } : new object[] { 11, 7, 1, 2, 17, 34, 51 };
                Assert.That(native.Calls, Is.EqualTo(new[] { Target(name) }));
                Assert.That(native.Arguments.Single(), Is.EqualTo(expected));
                Assert.That(native.Arguments.Single().Select(x => x.GetType()), Is.EqualTo(expected.Select(x => x.GetType())));
                Assert.That(native.Threads.Single(), Is.EqualTo(Thread.CurrentThread.ManagedThreadId));
                Assert.That(receipt.NativeFunction, Is.EqualTo(Target(name)));
                Assert.That(receipt.Status, Is.EqualTo(TableOperationStatus.InvokedWithoutAcknowledgement));
                Assert.That(receipt.IsSuccessful, Is.False); Assert.That(receipt.NativeReturnCode, Is.Null);
                Assert.That(receipt.ReportId, Is.EqualTo(11)); Assert.That(receipt.TableId, Is.EqualTo(7));
            }
        }

        public static IEnumerable<TestCaseData> InvalidCoordinates => Enumerable.Range(0, Methods.Length).SelectMany(i => Methods[i].GetParameters().Where(p => new[] { "reportId", "tableId", "rowNumber", "columnNumber" }.Contains(p.Name)).SelectMany(p => new[] { new TestCaseData(i, p.Position, 0), new TestCaseData(i, p.Position, -1) }));
        [TestCaseSource(nameof(InvalidCoordinates))]
        public void InvalidCoordinatesAndIdentitiesNeverReachNative(int index, int position, int bad)
        {
            var native = new Native();
            using (var wrapper = new OpenStaadWrapper(new Root { Table = native }))
            {
                var args = Arguments(Methods[index]); args[position] = bad;
                Assert.Throws<ArgumentOutOfRangeException>(() => Invoke(wrapper.Table, index, args)); Assert.That(native.Calls, Is.Empty);
            }
        }

        [TestCase(0, -1)] [TestCase(0, 256)] [TestCase(1, -1)] [TestCase(1, 256)] [TestCase(2, -1)] [TestCase(2, 256)]
        public void RgbChannelsOutsideByteRangeAreRejected(int channel, int value)
        {
            var native = new Native(); var channels = new[] { 17, 34, 51 }; channels[channel] = value;
            using (var wrapper = new OpenStaadWrapper(new Root { Table = native }))
            { Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Table.SetCellTextColor(11, 7, 1, 2, channels[0], channels[1], channels[2])); Assert.That(native.Calls, Is.Empty); }
        }

        [TestCase("#123", 17, 34, 51)] [TestCase("#112233", 17, 34, 51)] [TestCase("#80112233", 17, 34, 51)]
        [TestCase("Red", 255, 0, 0)] [TestCase("bad-color", 0, 0, 0)] [TestCase("", 0, 0, 0)]
        public void TextColorUsesMainSourceParsingAndFormatFallback(string text, int red, int green, int blue)
        {
            var native = new Native();
            using (var wrapper = new OpenStaadWrapper(new Root { Table = native }))
            { wrapper.Table.SetCellTextColor(11, 7, 1, 2, text); Assert.That(native.Arguments.Single().Skip(4), Is.EqualTo(new[] { red, green, blue })); }
        }

        [Test]
        public void NullTextRejectsAndNativeFormatExceptionDoesNotTriggerBlackRetry()
        {
            var native = new Native();
            using (var wrapper = new OpenStaadWrapper(new Root { Table = native }))
            {
                Assert.Throws<ArgumentNullException>(() => wrapper.Table.SetCellTextColor(11, 7, 1, 2, (string)null)); Assert.That(native.Calls, Is.Empty);
                var original = new FormatException("native error"); native.OnCall = _ => throw original;
                Assert.That(Assert.Throws<FormatException>(() => wrapper.Table.SetCellTextColor(11, 7, 1, 2, "#112233")), Is.SameAs(original));
                Assert.That(native.Calls.Count, Is.EqualTo(1));
                Assert.That(native.Arguments.Single().Skip(4), Is.EqualTo(new[] { 17, 34, 51 }));
            }
        }

        [TestCase(0)] [TestCase(1)] [TestCase(128)] [TestCase(255)]
        public void AlphaIsIgnoredWithoutPremultiplicationInAllArgbForms(int alpha)
        {
            var native = new Native();
            using (var wrapper = new OpenStaadWrapper(new Root { Table = native }))
            {
                wrapper.Table.SetCellTextColor(11, 7, 1, 2, Color.FromArgb((byte)alpha, 17, 34, 51));
                wrapper.Table.SetCellTextColor(11, 7, 1, 2, unchecked((alpha << 24) | 0x112233));
                wrapper.Table.SetCellTextColor(11, 7, 1, 2, (byte)alpha, (byte)17, (byte)34, (byte)51);
                Assert.That(native.Arguments.All(a => a.Skip(4).SequenceEqual(new object[] { 17, 34, 51 })), Is.True);
            }
        }

        [TestCase(0f, 1f, 0.5f, 255, 0, 0)] [TestCase(60f, 1f, 0.5f, 255, 255, 0)]
        [TestCase(120f, 1f, 0.5f, 0, 255, 0)] [TestCase(180f, 1f, 0.5f, 0, 255, 255)]
        [TestCase(240f, 1f, 0.5f, 0, 0, 255)] [TestCase(300f, 1f, 0.5f, 255, 0, 255)]
        [TestCase(360f, 1f, 0.5f, 255, 0, 0)] [TestCase(720f, 1f, 0.5f, 255, 0, 0)]
        [TestCase(-20f, 2f, 0.5f, 255, 0, 0)] [TestCase(0f, -1f, 0.5f, 128, 128, 128)]
        [TestCase(0f, 1f, -1f, 0, 0, 0)] [TestCase(0f, 1f, 2f, 255, 255, 255)]
        [TestCase(330f, 1f, 0.5f, 255, 0, 128)]
        public void HslSectorBoundariesClampAndRoundLikeTheSource(float hue, float saturation, float lightness, int red, int green, int blue)
        {
            var native = new Native();
            using (var wrapper = new OpenStaadWrapper(new Root { Table = native }))
            { wrapper.Table.SetCellTextColorHsl(11, 7, 1, 2, hue, saturation, lightness); Assert.That(native.Arguments.Single().Skip(4), Is.EqualTo(new[] { red, green, blue })); }
        }

        [TestCase(0)] [TestCase(1)] [TestCase(2)]
        public void NonfiniteHslComponentsAreRejectedBeforeDispatch(int component)
        {
            foreach (float bad in new[] { float.NaN, float.PositiveInfinity, float.NegativeInfinity })
            {
                var native = new Native(); var values = new[] { 0f, 1f, 0.5f }; values[component] = bad;
                using (var wrapper = new OpenStaadWrapper(new Root { Table = native }))
                { Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Table.SetCellTextColorHsl(11, 7, 1, 2, values[0], values[1], values[2])); Assert.That(native.Calls, Is.Empty); }
            }
        }

        [Test]
        public void AlignmentEnumsPreserveNativeCodesAndRejectWpfOrUndefinedValues()
        {
            var native = new Native();
            using (var wrapper = new OpenStaadWrapper(new Root { Table = native }))
            {
                foreach (var value in new[] { TableHorizontalAlignment.Left, TableHorizontalAlignment.Center, TableHorizontalAlignment.Right }) wrapper.Table.SetCellTextHorizontalAlignment(11, 7, 1, 2, value);
                foreach (var value in new[] { TableVerticalAlignment.Top, TableVerticalAlignment.Middle, TableVerticalAlignment.Bottom }) wrapper.Table.SetCellTextVerticalAlignment(11, 7, 1, 2, value);
                Assert.That(native.Arguments.Select(a => a[4]), Is.EqualTo(new object[] { 0, 1, 2, 0, 4, 8 }));
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Table.SetCellTextHorizontalAlignment(11, 7, 1, 2, (TableHorizontalAlignment)3));
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Table.SetCellTextVerticalAlignment(11, 7, 1, 2, (TableVerticalAlignment)1));
                Assert.That(native.Calls.Count, Is.EqualTo(6));
            }
        }

        [TestCase(0.0)] [TestCase(-1.0)] [TestCase(double.NaN)] [TestCase(double.PositiveInfinity)] [TestCase(double.NegativeInfinity)]
        public void TableWideSizeUsesTheSharedFinitePositiveGuard(double size)
        {
            var native = new Native();
            using (var wrapper = new OpenStaadWrapper(new Root { Table = native }))
            { Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Table.SetCellTextSizeForTable(11, 7, size)); Assert.That(native.Calls, Is.Empty); }
        }

        [TestCaseSource(nameof(Ports))]
        public void OldBindingsAreRejectedAfterReplacementAndDisposal(int index)
        {
            var native = new Native(); var root = new Root { Table = native }; var wrapper = new OpenStaadWrapper(root); var old = wrapper.Table;
            root.Table = new Native(); wrapper.RefreshComObjects();
            Assert.Throws<InvalidOperationException>(() => Invoke(old, index)); Assert.That(native.Calls, Is.Empty);
            var current = wrapper.Table; wrapper.Dispose();
            Assert.Throws<InvalidOperationException>(() => Invoke(current, index));
        }

        [TestCaseSource(nameof(Ports))]
        public void PostCallBindingReplacementStopsWithoutRetry(int index)
        {
            var native = new Native(); var root = new Root { Table = native };
            using (var wrapper = new OpenStaadWrapper(root))
            {
                var api = wrapper.Table; native.OnCall = _ => { root.Table = new Native(); wrapper.RefreshComObjects(); };
                Assert.Throws<InvalidOperationException>(() => Invoke(api, index)); Assert.That(native.Calls.Count, Is.EqualTo(1));
            }
        }

        [TestCaseSource(nameof(Ports))]
        public void NativeExceptionsPropagateUnchanged(int index)
        {
            var error = new COMException("native style error"); var native = new Native { OnCall = _ => throw error };
            using (var wrapper = new OpenStaadWrapper(new Root { Table = native }))
            { Assert.That(Assert.Throws<COMException>(() => Invoke(wrapper.Table, index)), Is.SameAs(error)); Assert.That(native.Calls.Count, Is.EqualTo(1)); }
        }

        [TestCase(24)] [TestCase(25)] [TestCase(26)]
        public void MatchingContractsUseTheSameServiceForReviewedVersions(int version)
        {
            foreach (int index in Enumerable.Range(0, Methods.Length))
            {
                var native = new Native();
                using (var wrapper = new OpenStaadWrapper(new Root { Table = native, Major = version })) Invoke(wrapper.Table, index);
                Assert.That(native.Calls, Is.EqualTo(new[] { Target(Methods[index].Name) }));
            }
        }
    }
}
