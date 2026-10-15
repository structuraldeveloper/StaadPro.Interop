using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums.Root;
using StaadPro.Interop.Models.Root;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSRootAnalysisQueryTests
    {
        // Int32/Double references deliberately exclude VARIANT and Int64 alternatives.
        public class NativeRoot
        {
            public object Running = 0, Status = 2, FileName = @"C:\Models\雪 frame.STD";
            public int? Warnings = 0, Errors = 0;
            public double? CpuTime = 1.25;
            public bool WriteFileName = true;
            public Exception PredicateFailure, StatusFailure, FileFailure;
            public string RequestedPath;
            public readonly List<string> Calls = new List<string>();

            public object IsAnalyzing()
            {
                Calls.Add("running");
                if (PredicateFailure != null) throw PredicateFailure;
                return Running;
            }

            public object GetAnalysisStatus(string modelFilePath, ref int warningCount, ref int errorCount, ref double cpuTimeSeconds)
            {
                Calls.Add("status"); RequestedPath = modelFilePath;
                Assert.That(warningCount, Is.EqualTo(int.MinValue));
                Assert.That(errorCount, Is.EqualTo(int.MinValue));
                Assert.That(double.IsNaN(cpuTimeSeconds), Is.True);
                if (StatusFailure != null) throw StatusFailure;
                if (Warnings.HasValue) warningCount = Warnings.Value;
                if (Errors.HasValue) errorCount = Errors.Value;
                if (CpuTime.HasValue) cpuTimeSeconds = CpuTime.Value;
                return Status;
            }

            public void GetSTAADFile(ref object fileName, bool fullPath)
            {
                Calls.Add("filename");
                Assert.That(fullPath, Is.True);
                Assert.That(fileName, Is.TypeOf<string>());
                if (FileFailure != null) throw FileFailure;
                if (WriteFileName) fileName = FileName;
            }
        }

        [TestCase(false, false)] [TestCase(true, true)]
        [TestCase(0, false)] [TestCase(1, true)]
        public void RunningQueryAcceptsDocumentedFlagsAndBooleanVariants(object value, bool expected)
        {
            var native = new NativeRoot { Running = value };
            Assert.That(new OSRootAdapter(native).IsAnalyzing(), Is.EqualTo(expected));
            Assert.That(native.Calls, Is.EqualTo(new[] { "running" }));
        }

        [Test]
        public void RunningQueryAcceptsIntegralRepresentationsWithoutCoercion()
        {
            foreach (object value in new object[] { (sbyte)1, (byte)1, (short)1, (ushort)1, 1, 1U, 1L, 1UL })
                Assert.That(new OSRootAdapter(new NativeRoot { Running = value }).IsAnalyzing(), Is.True);
        }

        [TestCase(null)] [TestCase(-1)] [TestCase(2)] [TestCase("1")]
        [TestCase(1d)] [TestCase(0.5d)] [TestCase(double.NaN)]
        [TestCase(long.MaxValue)] [TestCase(ulong.MaxValue)]
        public void RunningQueryRejectsInvalidFlagsWithoutFallback(object value)
        {
            var native = new NativeRoot { Running = value };
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).IsAnalyzing());
            Assert.That(native.Calls, Is.EqualTo(new[] { "running" }));
        }

        [Test]
        public void RunningQueryRejectsDecimalAndDatabaseNull()
        {
            foreach (object value in new object[] { 1m, DBNull.Value })
                Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(new NativeRoot { Running = value }).IsAnalyzing());
        }

        [TestCase(-2, false, false)] [TestCase(-1, true, false)]
        [TestCase(0, false, false)] [TestCase(1, true, false)]
        [TestCase(2, true, true)] [TestCase(3, true, true)]
        [TestCase(4, true, true)] [TestCase(5, true, false)]
        public void EveryDocumentedStatusSeparatesQuerySuccessFromAnalysisCompletion(int code, bool successful, bool completed)
        {
            var native = new NativeRoot { Status = code, Warnings = 3, Errors = 7, CpuTime = 0.0123456789 };
            var result = new OSRootAdapter(native).GetAnalysisStatus(@"Z:\uncreated-folder\Frame.STD");
            Assert.Multiple(() =>
            {
                Assert.That(result.NativeStatus, Is.EqualTo(code));
                Assert.That(result.Status, Is.EqualTo((AnalysisStatus)code));
                Assert.That(result.IsSuccessful, Is.EqualTo(successful));
                Assert.That(result.IsCompleted, Is.EqualTo(completed));
                Assert.That(result.WarningCount, Is.EqualTo(successful ? (int?)3 : null));
                Assert.That(result.ErrorCount, Is.EqualTo(successful ? (int?)7 : null));
                Assert.That(result.CpuTimeSeconds, Is.EqualTo(successful ? (double?)0.0123456789 : null));
                Assert.That(native.Calls, Is.EqualTo(new[] { "status" }));
                Assert.That(result.ModelFilePath, Is.EqualTo(native.RequestedPath));
            });
        }

        [TestCase(-99)] [TestCase(6)] [TestCase(int.MinValue)] [TestCase(int.MaxValue)]
        public void UnknownStatusIsPreservedWithoutPublishingUnqualifiedOutputData(int code)
        {
            var native = new NativeRoot { Status = code, Warnings = -9, Errors = -8, CpuTime = double.PositiveInfinity };
            var result = new OSRootAdapter(native).GetAnalysisStatus("model.STD");
            Assert.That(result.NativeStatus, Is.EqualTo(code));
            Assert.That(result.Status, Is.Null);
            Assert.That(result.IsSuccessful, Is.False);
            Assert.That(result.IsCompleted, Is.False);
            Assert.That(result.WarningCount, Is.Null);
            Assert.That(result.ErrorCount, Is.Null);
            Assert.That(result.CpuTimeSeconds, Is.Null);
            Assert.That(native.Calls, Is.EqualTo(new[] { "status" }));
        }

        [TestCase(-2)] [TestCase(0)]
        public void ExpectedNativeQueryFailuresIgnoreInvalidOutputsAndRemainResults(int code)
        {
            var native = new NativeRoot { Status = code, Warnings = -9, Errors = -8, CpuTime = double.NegativeInfinity };
            var result = new OSRootAdapter(native).GetAnalysisStatus("model.STD");
            Assert.That(result.NativeStatus, Is.EqualTo(code));
            Assert.That(result.WarningCount, Is.Null);
            Assert.That(result.ErrorCount, Is.Null);
            Assert.That(result.CpuTimeSeconds, Is.Null);
        }

        [TestCase(-1)] [TestCase(1)] [TestCase(5)]
        public void UnwrittenNoncompletedStateOutputsStayUnavailableRatherThanZero(int code)
        {
            var native = new NativeRoot { Status = code, Warnings = null, Errors = null, CpuTime = null };
            var result = new OSRootAdapter(native).GetAnalysisStatus("model.STD");
            Assert.That(result.IsSuccessful, Is.True);
            Assert.That(result.WarningCount, Is.Null);
            Assert.That(result.ErrorCount, Is.Null);
            Assert.That(result.CpuTimeSeconds, Is.Null);
            native.Warnings = 0;
            result = new OSRootAdapter(native).GetAnalysisStatus("model.STD");
            Assert.That(result.WarningCount, Is.Zero);
            Assert.That(result.ErrorCount, Is.Null);
            Assert.That(result.CpuTimeSeconds, Is.Null);
        }

        [TestCase(2)] [TestCase(3)] [TestCase(4)]
        public void CompletedStatesRequireEveryOutput(int code)
        {
            foreach (string missing in new[] { "warnings", "errors", "cpu" })
            {
                var native = new NativeRoot { Status = code };
                if (missing == "warnings") native.Warnings = null;
                if (missing == "errors") native.Errors = null;
                if (missing == "cpu") native.CpuTime = null;
                Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).GetAnalysisStatus("model.STD"), missing);
                Assert.That(native.Calls, Is.EqualTo(new[] { "status" }));
            }
        }

        [TestCase(-1)] [TestCase(1)] [TestCase(2)] [TestCase(3)] [TestCase(4)] [TestCase(5)]
        public void RecognizedStatesRejectNegativeCountsAndInvalidWrittenTime(int code)
        {
            var invalid = new[] {
                new NativeRoot { Status = code, Warnings = -1 },
                new NativeRoot { Status = code, Errors = -1 },
                new NativeRoot { Status = code, CpuTime = -0.001 },
                new NativeRoot { Status = code, CpuTime = double.PositiveInfinity },
                new NativeRoot { Status = code, CpuTime = double.NegativeInfinity }
            };
            foreach (var native in invalid)
            {
                Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).GetAnalysisStatus("model.STD"));
                Assert.That(native.Calls, Is.EqualTo(new[] { "status" }));
            }
        }

        [Test]
        public void ValidZeroAndBoundaryValuesArePreservedInAnImmutableSnapshot()
        {
            var native = new NativeRoot { Warnings = 0, Errors = 0, CpuTime = 0 };
            var root = new OSRootAdapter(native);
            var original = root.GetAnalysisStatus("model.STD");
            native.Warnings = int.MaxValue; native.Errors = int.MaxValue; native.CpuTime = double.MaxValue;
            var next = root.GetAnalysisStatus("model.STD");
            Assert.That(original.WarningCount, Is.Zero);
            Assert.That(original.ErrorCount, Is.Zero);
            Assert.That(original.CpuTimeSeconds, Is.Zero);
            Assert.That(next.WarningCount, Is.EqualTo(int.MaxValue));
            Assert.That(next.ErrorCount, Is.EqualTo(int.MaxValue));
            Assert.That(next.CpuTimeSeconds, Is.EqualTo(double.MaxValue));
            Assert.That(typeof(AnalysisStatusResult).IsSealed, Is.True);
            Assert.That(typeof(AnalysisStatusResult).GetConstructors(), Is.Empty);
            Assert.That(typeof(AnalysisStatusResult).GetProperties().All(p => p.GetSetMethod(true) == null), Is.True);
        }

        [Test]
        public void IntegralStatusSubtypesAreAccepted()
        {
            foreach (object code in new object[] { (sbyte)2, (byte)2, (short)2, (ushort)2, 2, 2U, 2L, 2UL })
                Assert.That(new OSRootAdapter(new NativeRoot { Status = code }).GetAnalysisStatus("model.STD").Status,
                    Is.EqualTo(AnalysisStatus.CompletedSuccessfully));
        }

        [TestCase(null)] [TestCase(true)] [TestCase("2")] [TestCase(2d)]
        [TestCase(double.NaN)] [TestCase(long.MaxValue)] [TestCase(ulong.MaxValue)]
        public void MalformedNativeStatusThrowsAfterExactlyOneRead(object code)
        {
            var native = new NativeRoot { Status = code };
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).GetAnalysisStatus("model.STD"));
            Assert.That(native.Calls, Is.EqualTo(new[] { "status" }));
        }

        [Test]
        public void DecimalAndDatabaseNullAreNotStatusCodes()
        {
            foreach (object value in new object[] { 2m, DBNull.Value })
                Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(new NativeRoot { Status = value }).GetAnalysisStatus("model.STD"));
        }

        [TestCase("sub\\..\\雪.STD")]
        [TestCase(@"\\server\share\模型.STD")]
        [TestCase(@"C:\Models\Frame.v2.STD")]
        public void ExplicitPathsAreNormalizedWithoutFilenameOrFilesystemQueries(string modelFilePath)
        {
            var native = new NativeRoot();
            var result = new OSRootAdapter(native).GetAnalysisStatus(modelFilePath);
            Assert.That(native.RequestedPath, Is.EqualTo(Path.GetFullPath(modelFilePath)));
            Assert.That(result.ModelFilePath, Is.EqualTo(Path.GetFullPath(modelFilePath)));
            Assert.That(native.Calls, Is.EqualTo(new[] { "status" }));
        }

        [TestCase(null)] [TestCase("")] [TestCase(" \t")] [TestCase("bad\0model.STD")]
        public void InvalidExplicitPathsAreRejectedBeforeAnyNativeCall(string modelFilePath)
        {
            var native = new NativeRoot();
            var error = Assert.Catch<ArgumentException>(() => new OSRootAdapter(native).GetAnalysisStatus(modelFilePath));
            if (string.IsNullOrWhiteSpace(modelFilePath)) Assert.That(error.ParamName, Is.EqualTo("modelFilePath"));
            Assert.That(native.Calls, Is.Empty);
        }

        [Test]
        public void CurrentModelOverloadReadsFilenameOnceAndDelegatesToTheCapturedPath()
        {
            var native = new NativeRoot { FileName = @"sub\..\雪.STD" };
            var result = new OSRootAdapter(native).GetAnalysisStatus();
            Assert.That(result.ModelFilePath, Is.EqualTo(Path.GetFullPath((string)native.FileName)));
            Assert.That(native.Calls, Is.EqualTo(new[] { "filename", "status" }));
        }

        [TestCase("")] [TestCase(" \t")] [TestCase(null)] [TestCase(42)]
        public void CurrentModelOverloadRejectsUnnamedOrMalformedFilenamesBeforeStatusAccess(object fileName)
        {
            var native = new NativeRoot { FileName = fileName };
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).GetAnalysisStatus());
            Assert.That(native.Calls, Is.EqualTo(new[] { "filename" }));
        }

        [Test]
        public void CurrentModelOverloadRejectsUnwrittenAndInvalidPathOutputs()
        {
            var native = new NativeRoot { WriteFileName = false };
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).GetAnalysisStatus());
            Assert.That(native.Calls, Is.EqualTo(new[] { "filename" }));
            native = new NativeRoot { FileName = "bad\0model.STD" };
            Assert.Catch<ArgumentException>(() => new OSRootAdapter(native).GetAnalysisStatus());
            Assert.That(native.Calls, Is.EqualTo(new[] { "filename" }));
        }

        [TestCase("running")] [TestCase("status")] [TestCase("filename")]
        public void NativeExceptionsPropagateUnchangedWithoutRetries(string operation)
        {
            var failure = new COMException("Disconnected", unchecked((int)0x80010108));
            var native = new NativeRoot();
            var root = new OSRootAdapter(native);
            if (operation == "running") native.PredicateFailure = failure;
            if (operation == "status") native.StatusFailure = failure;
            if (operation == "filename") native.FileFailure = failure;
            Assert.That(Assert.Throws<COMException>(() =>
            {
                if (operation == "running") root.IsAnalyzing();
                else if (operation == "status") root.GetAnalysisStatus("model.STD");
                else root.GetAnalysisStatus();
            }), Is.SameAs(failure));
            Assert.That(native.Calls, Is.EqualTo(new[] { operation }));
        }

        [Test]
        public void MissingSignaturesAreNotPresentedAsOrdinaryNativeFailures()
        {
            var root = new OSRootAdapter(new object());
            Assert.Throws<RuntimeBinderException>(() => root.IsAnalyzing());
            Assert.Throws<RuntimeBinderException>(() => root.GetAnalysisStatus("model.STD"));
            Assert.Throws<RuntimeBinderException>(() => root.GetAnalysisStatus());
        }

        [Test]
        public void DistinctBindingsRemainIsolatedAndQueriesRemainLive()
        {
            var first = new NativeRoot { Running = 1, Status = 3, Warnings = 5 };
            var second = new NativeRoot { Status = 5, Warnings = null, Errors = null, CpuTime = null };
            var left = new OSRootAdapter(first); var right = new OSRootAdapter(second);
            Assert.That(left.IsAnalyzing(), Is.True);
            Assert.That(right.IsAnalyzing(), Is.False);
            Assert.That(left.GetAnalysisStatus("one.STD").WarningCount, Is.EqualTo(5));
            Assert.That(right.GetAnalysisStatus("two.STD").WarningCount, Is.Null);
            first.Running = 0; first.Status = 4; first.Errors = 2;
            Assert.That(left.IsAnalyzing(), Is.False);
            Assert.That(left.GetAnalysisStatus("three.STD").ErrorCount, Is.EqualTo(2));
            Assert.That(second.Calls, Is.EqualTo(new[] { "running", "status" }));
            Assert.That(first.RequestedPath, Is.EqualTo(Path.GetFullPath("three.STD")));
            Assert.That(second.RequestedPath, Is.EqualTo(Path.GetFullPath("two.STD")));
        }

        [Test]
        public void NewResultPropertiesAndStatusMembersHaveCompiledXmlDocumentation()
        {
            var assembly = typeof(AnalysisStatusResult).Assembly;
            var entries = XDocument.Load(Path.ChangeExtension(assembly.Location, ".xml"))
                .Descendants("member").ToDictionary(m => (string)m.Attribute("name"));
            foreach (var type in new[] { typeof(AnalysisStatusResult), typeof(AnalysisStatus) })
            {
                Assert.That(entries["T:" + type.FullName].Element("summary")?.Value, Is.Not.Null.And.Not.Empty);
                foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    var entry = entries["P:" + type.FullName + "." + property.Name];
                    Assert.That(entry.Element("summary")?.Value, Is.Not.Null.And.Not.Empty);
                    Assert.That(entry.Element("value")?.Value, Is.Not.Null.And.Not.Empty);
                }
                foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
                    Assert.That(entries["F:" + type.FullName + "." + field.Name].Element("summary")?.Value, Is.Not.Null.And.Not.Empty);
            }
        }

        [Test]
        public void NewPublicQueriesExposeNoReferenceParametersOrNativeServices()
        {
            var methods = typeof(IOSRoot).GetMethods().Where(m => m.Name == "IsAnalyzing" || m.Name == "GetAnalysisStatus").ToArray();
            Assert.That(methods.Length, Is.EqualTo(3));
            Assert.That(methods.SelectMany(m => m.GetParameters()).Any(p => p.ParameterType.IsByRef || p.IsOut), Is.False);
            Assert.That(typeof(AnalysisStatusResult).Assembly.GetExportedTypes().Any(t => t.Namespace == "StaadPro.Interop.Services.Root.Analysis"), Is.False);
        }
    }
}
