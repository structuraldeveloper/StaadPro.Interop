using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PackageVerification;
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
    public class OSTableFoundationTests
    {
        private static MethodInfo[] Methods => typeof(IOSTable).GetMethods().Where(m => !m.IsSpecialName).OrderBy(m => m.Name).ToArray();
        public static IEnumerable<TestCaseData> Ports => Methods.Select(m => new TestCaseData(m.Name));
        private static object[] Arguments(MethodInfo m) => m.GetParameters().Select(p =>
            p.ParameterType == typeof(string) ? (object)(p.Name == "value" ? " 42.50 " : p.Name == "unitString" ? "kN" : "  Native text  ") :
            p.Name == "reportId" ? 11 : p.Name == "tableId" ? 7 : p.Name == "columnNumber" ? 2 : p.Name == "columnCount" ? 4 : p.Name == "rowCount" ? 3 : 1).ToArray();
        private static object Invoke(IOSTable api, string name, object[] args = null)
        {
            var method = Methods.Single(m => m.Name == name);
            try { return method.Invoke(api, args ?? Arguments(method)); }
            catch (TargetInvocationException e) { ExceptionDispatchInfo.Capture(e.InnerException).Throw(); throw; }
        }

        [Test]
        public void ExactlyTenSourcePortsHaveNoPublicReferenceOrTupleArguments()
        {
            Assert.That(Methods.Length, Is.EqualTo(10));
            Assert.That(typeof(OSTableAdapter).IsSealed, Is.True);
            foreach (var m in Methods)
            {
                var implementation = typeof(OSTableAdapter).GetMethod(m.Name);
                Assert.That(implementation.ReturnType, Is.EqualTo(m.ReturnType));
                Assert.That(implementation.GetParameters().Select(p => p.Name), Is.EqualTo(m.GetParameters().Select(p => p.Name)));
                Assert.That(m.GetParameters().Any(p => p.IsOut || p.ParameterType.IsByRef || p.ParameterType == typeof(object)), Is.False);
                Assert.That(m.ReturnType.FullName, Does.Not.Contain("Tuple"));
            }
        }

        [TestCaseSource(nameof(Ports))]
        public void EveryMethodHasMatchingCompleteComparativeXml(string name)
        {
            var xml = XDocument.Load(Path.ChangeExtension(typeof(IOSTable).Assembly.Location, ".xml"));
            string prefix = "M:" + typeof(IOSTable).FullName + "." + name;
            var a = xml.Descendants("member").Single(m => ((string)m.Attribute("name")).Split('(')[0] == prefix);
            var b = xml.Descendants("member").Single(m => (string)m.Attribute("name") == ((string)a.Attribute("name")).Replace(typeof(IOSTable).FullName, typeof(OSTableAdapter).FullName));
            Assert.That(a.Elements().Select(x => x.ToString()), Is.EqualTo(b.Elements().Select(x => x.ToString())));
            foreach (string tag in new[] { "summary", "returns", "remarks", "example" }) Assert.That(a.Element(tag)?.Value, Is.Not.Null.And.Not.Empty, tag);
            Assert.That(a.Elements("param").Select(p => (string)p.Attribute("name")), Is.EqualTo(Methods.Single(m => m.Name == name).GetParameters().Select(p => p.Name)));
            Assert.That(a.Element("remarks").Value, Does.Contain("Unlike").Or.Contain("scoped to one report"));
        }

        [TestCaseSource(nameof(Ports))]
        public void ExactNativePacketUsesCallerThreadAndNoAdditionalDispatch(string name)
        {
            var native = new Native();
            using (var w = new OpenStaadWrapper(new Root { Table = native }))
            {
                Assert.That(native.Calls, Is.Empty, "Acquisition must not call report functions.");
                var args = Arguments(Methods.Single(m => m.Name == name));
                var result = Invoke(w.Table, name, args);
                Assert.That(native.Calls, Is.EqualTo(new[] { name }));
                Assert.That(native.Arguments.Single(), Is.EqualTo(args));
                Assert.That(native.Threads.Single(), Is.EqualTo(Thread.CurrentThread.ManagedThreadId));
                if (result is TableOperationResult receipt)
                {
                    Assert.That(receipt.NativeFunction, Is.EqualTo(name));
                    if (name == "CreateReport" || name == "AddTable") Assert.That(receipt.IsSuccessful, Is.True);
                    else
                    {
                        Assert.That(receipt.Status, Is.EqualTo(TableOperationStatus.InvokedWithoutAcknowledgement));
                        Assert.That(receipt.NativeReturnCode, Is.Null);
                        Assert.That(receipt.IsSuccessful, Is.False);
                        Assert.That(receipt.ReportId, Is.EqualTo(11));
                        Assert.That(receipt.TableId, Is.EqualTo(name == "SaveReport" ? (int?)null : 7));
                    }
                }
                else Assert.That(result, Is.EqualTo(name == "GetCellValue" ? (object)"Cell text" : name == "GetReportCount" ? 2 : 3));
            }
        }

        [TestCase(24)] [TestCase(25)] [TestCase(26)]
        public void UnchangedContractsShareOneImplementationAcrossReviewedVersions(int version)
        {
            foreach (var m in Methods)
            {
                var native = new Native();
                using (var w = new OpenStaadWrapper(new Root { Table = native, Major = version }))
                { Invoke(w.Table, m.Name); Assert.That(native.Calls.Single(), Is.EqualTo(m.Name)); }
            }
        }

        public static IEnumerable<TestCaseData> InvalidIntegers
        {
            get
            {
                foreach (var m in Methods)
                    foreach (var p in m.GetParameters().Where(p => p.ParameterType == typeof(int)))
                        foreach (int bad in new[] { 0, -1, int.MinValue })
                            yield return new TestCaseData(m.Name, p.Position, bad).SetName(m.Name + "_" + p.Name + "_" + bad);
            }
        }
        [TestCaseSource(nameof(InvalidIntegers))]
        public void InvalidIdentitiesDimensionsAndCoordinatesRejectBeforeNativeWork(string name, int position, int bad)
        {
            var native = new Native();
            using (var w = new OpenStaadWrapper(new Root { Table = native }))
            {
                var args = Arguments(Methods.Single(m => m.Name == name)); args[position] = bad;
                Assert.Throws<ArgumentOutOfRangeException>(() => Invoke(w.Table, name, args));
                Assert.That(native.Calls, Is.Empty);
            }
        }

        public static IEnumerable<TestCaseData> InvalidText => Methods.SelectMany(m => m.GetParameters().Where(p => p.ParameterType == typeof(string)).SelectMany(p => new[] { new TestCaseData(m.Name, p.Position, null), new TestCaseData(m.Name, p.Position, "bad\0text") }));
        [TestCaseSource(nameof(InvalidText))]
        public void InvalidTextRejectsBeforeNativeWork(string name, int position, string text)
        {
            var native = new Native();
            using (var w = new OpenStaadWrapper(new Root { Table = native }))
            {
                var args = Arguments(Methods.Single(m => m.Name == name)); args[position] = text;
                Assert.Catch<ArgumentException>(() => Invoke(w.Table, name, args)); Assert.That(native.Calls, Is.Empty);
            }
        }

        [TestCase("CreateReport", "")] [TestCase("CreateReport", " \t ")]
        [TestCase("AddTable", "")] [TestCase("AddTable", " \t ")]
        public void BlankCreationNamesAreRejected(string name, string text)
        {
            var native = new Native();
            using (var w = new OpenStaadWrapper(new Root { Table = native }))
            {
                var args = Arguments(Methods.Single(m => m.Name == name)); args[name == "CreateReport" ? 0 : 1] = text;
                Assert.Throws<ArgumentException>(() => Invoke(w.Table, name, args)); Assert.That(native.Calls, Is.Empty);
            }
        }

        [TestCase("SetCellValue")] [TestCase("SetColumnHeader")] [TestCase("SetColumnUnitString")] [TestCase("SetRowHeader")]
        public void EmptyAndWhitespaceEditsAreForwardedWithoutConversions(string name)
        {
            foreach (string text in new[] { "", " \t ", "\u03bc\u6771\r\n42,50" })
            {
                var native = new Native();
                using (var w = new OpenStaadWrapper(new Root { Table = native }))
                {
                    var args = Arguments(Methods.Single(m => m.Name == name)); args[args.Length - 1] = text;
                    Invoke(w.Table, name, args); Assert.That(native.Arguments.Single().Last(), Is.EqualTo(text));
                }
            }
        }

        [TestCase("CreateReport")] [TestCase("AddTable")]
        public void CreationRetainsActualIdAndReportsZeroAsFailure(string name)
        {
            foreach (int id in new[] { 0, 19, int.MaxValue })
            {
                var native = new Native(); native.Returns[name] = id;
                using (var w = new OpenStaadWrapper(new Root { Table = native }))
                {
                    var receipt = (TableOperationResult)Invoke(w.Table, name);
                    Assert.That(receipt.IsSuccessful, Is.EqualTo(id > 0)); Assert.That(receipt.NativeReturnCode, Is.EqualTo(id));
                    Assert.That(receipt.Status, Is.EqualTo(id == 0 ? TableOperationStatus.NativeFailure : TableOperationStatus.Succeeded));
                    Assert.That(receipt.ReportId, Is.EqualTo(name == "CreateReport" ? (id == 0 ? (int?)null : id) : 11));
                    Assert.That(receipt.TableId, Is.EqualTo(name == "AddTable" && id > 0 ? (int?)id : null));
                }
            }
        }

        public static IEnumerable<TestCaseData> BadNumerics
        {
            get
            {
                foreach (string name in new[] { "CreateReport", "AddTable", "GetReportCount", "GetTableCount" })
                    foreach (object raw in new object[] { null, true, "2", 2.0, 2m, -1, -2001, long.MaxValue, ulong.MaxValue, new int[] { 2 } })
                        yield return new TestCaseData(name, raw);
            }
        }
        [TestCaseSource(nameof(BadNumerics))]
        public void MalformedNativeCountsAndIdsAreNotCoerced(string name, object raw)
        {
            var native = new Native(); native.Returns[name] = raw;
            using (var w = new OpenStaadWrapper(new Root { Table = native }))
            { Assert.Throws<InvalidOperationException>(() => Invoke(w.Table, name)); Assert.That(native.Calls.Count, Is.EqualTo(1)); }
        }

        [TestCase("GetReportCount")] [TestCase("GetTableCount")]
        public void IntegralMarshalingAndEmptyCountsArePreserved(string name)
        {
            foreach (object raw in new object[] { (byte)0, (short)2, 3U, 4L, 5UL, int.MaxValue })
            {
                var native = new Native(); native.Returns[name] = raw;
                using (var w = new OpenStaadWrapper(new Root { Table = native })) Assert.That(Invoke(w.Table, name), Is.EqualTo(Convert.ToInt32(raw)));
            }
        }

        [Test]
        public void CellReadDistinguishesExplicitEmptyFromUntouchedNullOrMalformedOutput()
        {
            foreach (object raw in new object[] { "", " \t ", "123.50", "\u03bc\u6771", null, 0, false, new string[0] })
            {
                var native = new Native { CellOutput = raw };
                using (var w = new OpenStaadWrapper(new Root { Table = native }))
                {
                    if (raw is string text) Assert.That(w.Table.GetCellValue(11, 7, 1, 1), Is.EqualTo(text));
                    else Assert.Throws<InvalidOperationException>(() => w.Table.GetCellValue(11, 7, 1, 1));
                }
            }
            var untouched = new Native { WriteCellOutput = false };
            using (var w = new OpenStaadWrapper(new Root { Table = untouched })) Assert.Throws<InvalidOperationException>(() => w.Table.GetCellValue(11, 7, 1, 1));
        }

        [TestCaseSource(nameof(Ports))]
        public void DisposedAndReplacedBindingsAreRejectedWithoutNativeWork(string name)
        {
            var native = new Native(); var root = new Root { Table = native }; var w = new OpenStaadWrapper(root); var old = w.Table;
            root.Table = new Native(); w.RefreshComObjects();
            Assert.Throws<InvalidOperationException>(() => Invoke(old, name)); Assert.That(native.Calls, Is.Empty);
            var current = w.Table; w.Dispose();
            Assert.Throws<InvalidOperationException>(() => Invoke(current, name));
            Assert.Throws<InvalidOperationException>(() => Invoke(old, name));
        }

        [TestCaseSource(nameof(Ports))]
        public void PostCallBindingChangeThrowsWithoutRetryOrGuessedSuccess(string name)
        {
            var native = new Native(); var root = new Root { Table = native };
            using (var w = new OpenStaadWrapper(root))
            {
                var api = w.Table;
                native.OnCall = _ => { root.Table = new Native(); w.RefreshComObjects(); };
                Assert.Throws<InvalidOperationException>(() => Invoke(api, name)); Assert.That(native.Calls.Count, Is.EqualTo(1));
            }
        }

        [TestCaseSource(nameof(Ports))]
        public void NativeExceptionsPropagateUnchangedWithoutRetry(string name)
        {
            var error = new COMException("native report context", unchecked((int)0x80010108));
            var native = new Native { OnCall = _ => throw error };
            using (var w = new OpenStaadWrapper(new Root { Table = native }))
            { Assert.That(Assert.Throws<COMException>(() => Invoke(w.Table, name)), Is.SameAs(error)); Assert.That(native.Calls.Count, Is.EqualTo(1)); }
        }

        [Test]
        public void CreatedIdsDriveAOneBasedWriteReadAndSaveWorkflow()
        {
            var native = new Native();
            using (var w = new OpenStaadWrapper(new Root { Table = native }))
            {
                int report = w.Table.CreateReport("Report").ReportId.Value;
                int table = w.Table.AddTable(report, "Members", 2, 2).TableId.Value;
                w.Table.SetColumnHeader(report, table, 2, "Force"); w.Table.SetColumnUnitString(report, table, 2, "kN");
                w.Table.SetRowHeader(report, table, 1, "Member 17"); w.Table.SetCellValue(report, table, 1, 2, "42.50");
                Assert.That(w.Table.GetCellValue(report, table, 1, 2), Is.EqualTo("42.50"));
                Assert.That(w.Table.SaveReport(report).Status, Is.EqualTo(TableOperationStatus.InvokedWithoutAcknowledgement));
                Assert.That(native.Calls, Is.EqualTo(new[] { "CreateReport", "AddTable", "SetColumnHeader", "SetColumnUnitString", "SetRowHeader", "SetCellValue", "GetCellValue", "SaveReport" }));
            }
        }

        [Test]
        public void NullBindingsAreRejectedAndContractIndexMatchesTheTenPorts()
        {
            Assert.Throws<ArgumentNullException>(() => new OSTableAdapter(null));
            using (var w = new OpenStaadWrapper(new Root())) Assert.Throws<ArgumentNullException>(() => new OSTableAdapter(w));
            var index = JObject.Parse(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "ContractReview", "TableFoundationContractIndex.json")));
            Assert.That(index["functions"].Select(f => (string)f["managedName"]).OrderBy(s => s), Is.EqualTo(Methods.Select(m => m.Name)));
            Assert.That((bool)index["mainProjectReadOnly"], Is.True);
        }
    }
}
