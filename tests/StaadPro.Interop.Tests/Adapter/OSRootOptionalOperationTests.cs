using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums.Root;
using StaadPro.Interop.Models.Root;
using StaadPro.Interop.Services.Root.Analysis;
using StaadPro.Interop.Services.Root.Common;
using static StaadPro.Interop.Tests.Adapter.RootOptionalTestDoubles;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSRootOptionalOperationTests
    {
        [TestCase(0,0)] [TestCase(0,1)] [TestCase(1,0)] [TestCase(1,1)] [TestCase(2,0)] [TestCase(2,1)]
        public void ExportOptionsMapExactlyToNativeInt32Arguments(int axis, int loads)
        {
            var native = new NativeRoot();
            var result = new OSRootAdapter(native, new Metadata()).Capabilities.GetAdinaExport().Capability
                .ExportModelToAdina((AdinaAxisSystem)axis, (AdinaLoadCaseHandling)loads);
            Assert.That(native.Axis, Is.EqualTo(axis)); Assert.That(native.LoadHandling, Is.EqualTo(loads));
            Assert.That(result.AxisSystem, Is.EqualTo((AdinaAxisSystem)axis));
            Assert.That(result.LoadCaseHandling, Is.EqualTo((AdinaLoadCaseHandling)loads));
            Assert.That(native.Calls, Is.EqualTo(new[] { "export" }));
        }
        [TestCase(0,true,false)] [TestCase(-14501,true,true)] [TestCase(-14502,false,false)]
        [TestCase(-101,false,false)] [TestCase(101,false,false)] [TestCase(int.MinValue,false,false)]
        public void ExportPreservesDocumentedAndUnknownNativeStatuses(int code, bool success, bool warnings)
        {
            var native = new NativeRoot { ExportCode = code };
            var result = RootAdinaExportService.Export(native, AdinaAxisSystem.Preserve, AdinaLoadCaseHandling.MultipleLoadCases);
            Assert.That(result.NativeStatus, Is.EqualTo(code)); Assert.That(result.IsSuccessful, Is.EqualTo(success));
            Assert.That(result.HasWarnings, Is.EqualTo(warnings));
            Assert.That(result.Status.HasValue, Is.EqualTo(Enum.IsDefined(typeof(AdinaExportStatus), code)));
        }
        [TestCase(-1,0,"axisSystem")] [TestCase(3,0,"axisSystem")] [TestCase(0,-1,"loadCaseHandling")] [TestCase(0,2,"loadCaseHandling")]
        public void InvalidExportOptionsDoNotEvenReadVersion(int axis, int loads, string argument)
        {
            var native = new NativeRoot(); var metadata = new Metadata();
            var api = new OSRootAdapter(native, metadata).Capabilities.GetAdinaExport().Capability;
            int reads = metadata.Reads;
            Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => api.ExportModelToAdina((AdinaAxisSystem)axis, (AdinaLoadCaseHandling)loads)).ParamName, Is.EqualTo(argument));
            Assert.That(metadata.Reads, Is.EqualTo(reads)); Assert.That(native.Calls, Is.Empty);
        }
        [Test]
        public void ExportFailureIsNotRetriedAndDoesNotClaimNoEffects()
        {
            var failure = new COMException("Export failed");
            var native = new NativeRoot { Failure = failure };
            Assert.That(Assert.Throws<COMException>(() => RootAdinaExportService.Export(native, AdinaAxisSystem.ZUp, AdinaLoadCaseHandling.TimeHistory)), Is.SameAs(failure));
            Assert.That(native.Calls, Is.EqualTo(new[] { "export" }));
            Assert.That(native.Axis, Is.EqualTo(2));
        }

        [TestCase(true)] [TestCase(false)]
        public void MessageCallsUseFreshCountAndBstrRefObjectWithoutPublicBuffers(bool errors)
        {
            var native = new NativeRoot();
            MessageResponder verify = (string path, ref object buffer) =>
            {
                Assert.That(buffer, Is.TypeOf<string[]>());
                var array = (string[])buffer;
                Assert.That(array.Length, Is.EqualTo(errors ? 2 : 1));
                Assert.That(array.All(x => x.Contains("UnwrittenMessage")), Is.True);
                for (int i = 0; i < array.Length; i++) array[i] = "  雪 message " + i + "\r\ntext";
                return 1;
            };
            if (errors) native.Errors = verify; else native.Warnings = verify;
            var result = Read(native, errors);
            Assert.That(result.IsSuccessful, Is.True);
            Assert.That(result.RetrievalAttempts, Is.EqualTo(1));
            Assert.That(result.Messages[0], Is.EqualTo("  雪 message 0\r\ntext"));
            Assert.That(native.Calls.Select(c => c.Split(':')[0]), Is.EqualTo(new[] { "status", errors ? "errors" : "warnings" }));
        }
        [TestCase(0)] [TestCase(-1)] [TestCase(-2)] [TestCase(-107)] [TestCase(-119)]
        [TestCase(-1001)] [TestCase(-10004)] [TestCase(999)] [TestCase(int.MinValue)]
        public void FailedAndEmptyResponsesNeverExposeMalformedPartialMessages(int code)
        {
            var native = new NativeRoot { Errors = (string p, ref object b) => { b = 42; return code; } };
            var result = Read(native);
            Assert.That(result.NativeStatus, Is.EqualTo(code));
            Assert.That(result.Messages, Is.Empty);
            Assert.That(result.IsSuccessful, Is.EqualTo(code == 0));
            Assert.That(result.Status.HasValue, Is.EqualTo(Enum.IsDefined(typeof(AnalysisMessageRetrievalStatus), code)));
            Assert.That(native.Calls, Has.Count.EqualTo(2));
        }
        [TestCase(true)] [TestCase(false)]
        public void AZeroCountStillObtainsAnActualNativeAbsenceResponse(bool errors)
        {
            var native = new NativeRoot { ErrorCount = 0, WarningCount = 0 };
            MessageResponder responder = (string p, ref object b) => { Assert.That(b, Is.EqualTo(new string[0])); return 0; };
            native.Errors = responder; native.Warnings = responder;
            var result = Read(native, errors);
            Assert.That(result.Outcome, Is.EqualTo(AnalysisMessageQueryOutcome.NoMessages));
            Assert.That(result.NativeStatus, Is.EqualTo(0));
            Assert.That(result.RetrievalAttempts, Is.EqualTo(1));
        }
        [TestCase(-2)] [TestCase(0)] [TestCase(999)] [TestCase(5)]
        public void UnavailableCountsAreNotTreatedAsZeroOrInventedNativeFailures(int analysisCode)
        {
            var native = new NativeRoot { Status = (string p, ref int w, ref int e, ref double cpu) => analysisCode };
            var result = Read(native);
            Assert.That(result.Outcome, Is.EqualTo(AnalysisMessageQueryOutcome.AnalysisCountUnavailable));
            Assert.That(result.NativeStatus, Is.Null);
            Assert.That(result.RetrievalAttempts, Is.Zero);
            Assert.That(result.AnalysisState.NativeStatus, Is.EqualTo(analysisCode));
            Assert.That(result.Messages, Is.Empty);
            Assert.That(native.Calls, Has.Count.EqualTo(1));
        }
        [TestCase(true)] [TestCase(false)]
        public void AllocationLimitPreventsUnboundedNativeBufferRequests(bool errors)
        {
            var native = new NativeRoot { ErrorCount = 100001, WarningCount = 100001 };
            var result = Read(native, errors);
            Assert.That(result.Outcome, Is.EqualTo(AnalysisMessageQueryOutcome.MessageLimitExceeded));
            Assert.That(result.NativeStatus, Is.Null); Assert.That(native.Calls, Has.Count.EqualTo(1));
        }
        [Test]
        public void LimitBoundaryIsAllowedAndEmptyNativeResponseIgnoresBufferContents()
        {
            var native = new NativeRoot { ErrorCount = 100000, Errors = (string p, ref object b) => { Assert.That(((string[])b).Length, Is.EqualTo(100000)); return 0; } };
            Assert.That(Read(native).IsSuccessful, Is.True);
        }
        [TestCase(true)] [TestCase(false)]
        public void OnlyExplicitInsufficientCapacityAllowsOneFreshCountAndRetry(bool succeedOnRetry)
        {
            int statusCalls = 0, retrievals = 0;
            var native = new NativeRoot();
            native.Status = (string p, ref int w, ref int e, ref double cpu) => { w = 0; e = ++statusCalls; cpu = 0; return 2; };
            native.Errors = (string p, ref object b) =>
            {
                retrievals++;
                Assert.That(((string[])b).Length, Is.EqualTo(retrievals));
                if (retrievals == 1 || !succeedOnRetry) return -108;
                b = new[] { "one", "two" }; return 1;
            };
            var result = Read(native);
            Assert.That(result.RetrievalAttempts, Is.EqualTo(2));
            Assert.That(result.IsSuccessful, Is.EqualTo(succeedOnRetry));
            Assert.That(result.NativeStatus, Is.EqualTo(succeedOnRetry ? 1 : -108));
            Assert.That(statusCalls, Is.EqualTo(2));
            Assert.That(native.Calls.Select(c => c.Split(':')[0]), Is.EqualTo(new[] { "status", "errors", "status", "errors" }));
        }
        [TestCase(true)] [TestCase(false)]
        public void RetryPreflightFailureRetainsLastNativeCapacityResponse(bool overLimit)
        {
            int queries = 0;
            var native = new NativeRoot();
            native.Status = (string p, ref int w, ref int e, ref double cpu) =>
            {
                if (++queries == 1) { w = 0; e = 1; cpu = 0; return 2; }
                if (!overLimit) return 0;
                w = 0; e = int.MaxValue; cpu = 0; return 2;
            };
            native.Errors = (string p, ref object b) => -108;
            var result = Read(native);
            Assert.That(result.NativeStatus, Is.EqualTo(-108));
            Assert.That(result.RetrievalAttempts, Is.EqualTo(1));
            Assert.That(result.Outcome, Is.EqualTo(overLimit ? AnalysisMessageQueryOutcome.MessageLimitExceeded : AnalysisMessageQueryOutcome.AnalysisCountUnavailable));
        }
        [Test]
        public void ReplacedArraysWithNonzeroLowerBoundsAreMaterializedWithoutLosingText()
        {
            Array source = Array.CreateInstance(typeof(string), new[] { 2 }, new[] { 1 });
            source.SetValue("first", 1); source.SetValue("", 2);
            var native = new NativeRoot { Errors = (string p, ref object b) => { b = source; return 1; } };
            var result = Read(native);
            source.SetValue("changed", 1);
            Assert.That(result.Messages, Is.EqualTo(new[] { "first", "" }));
            Assert.Throws<NotSupportedException>(() => ((IList<string>)result.Messages)[0] = "changed");
        }
        [TestCase(0)] [TestCase(1)] [TestCase(2)] [TestCase(3)] [TestCase(4)] [TestCase(5)]
        public void SuccessfulMalformedOrUnwrittenBuffersRemainExceptions(int shape)
        {
            var native = new NativeRoot { Errors = (string p, ref object b) =>
            {
                if (shape == 0) b = null;
                if (shape == 1) b = new object[] { "not BSTR[]" };
                if (shape == 2) b = new string[1,1];
                if (shape == 3) b = new string[] { null };
                if (shape == 4) b = new string[100001];
                // shape 5 keeps the initialized unwritten sentinels.
                return 1;
            } };
            Assert.Throws<InvalidOperationException>(() => Read(native));
            Assert.That(native.Calls, Has.Count.EqualTo(2));
        }
        [TestCase(null)] [TestCase("")] [TestCase(" \t")] [TestCase("bad\0path")]
        public void InvalidPathsDoNotReadMetadataOrCounts(string path)
        {
            var native = new NativeRoot(); var metadata = new Metadata();
            var api = new OSRootAdapter(native, metadata).Capabilities.GetAnalysisErrorMessages().Capability;
            int reads = metadata.Reads;
            Assert.Catch<ArgumentException>(() => api.GetAnalysisErrorMessages(path));
            Assert.That(metadata.Reads, Is.EqualTo(reads)); Assert.That(native.Calls, Is.Empty);
        }
        [Test]
        public void RelativePathIsNormalizedOnceAndResultRetainsQueriedPath()
        {
            var native = new NativeRoot();
            var result = new OSRootAdapter(native, new Metadata()).Capabilities.GetAnalysisWarningMessages().Capability.GetAnalysisWarningMessages("relative.std");
            Assert.That(result.AnalysisState.ModelFilePath, Is.EqualTo(Path.GetFullPath("relative.std")));
            Assert.That(native.Calls.All(c => c.EndsWith(Path.GetFullPath("relative.std"))), Is.True);
        }
        [TestCase(true)] [TestCase(false)]
        public void NativeExceptionsDoNotBecomeExpectedFailureResultsOrRetries(bool atCounts)
        {
            var failure = new COMException("Native failure");
            var native = new NativeRoot();
            if (atCounts) native.Failure = failure;
            else native.Errors = (string p, ref object b) => throw failure;
            Assert.That(Assert.Throws<COMException>(() => Read(native)), Is.SameAs(failure));
            Assert.That(native.Calls, Has.Count.EqualTo(atCounts ? 1 : 2));
        }
        public class MalformedNative
        {
            public object Code;
            public int GetAnalysisStatus(string path, ref int warnings, ref int errors, ref double cpu)
            { warnings = 0; errors = 1; cpu = 0; return 2; }
            public object GetAnalysisErrorMessages(string path, ref object buffer) => Code;
            public object ExportModelToADINA(int axis, int loads) => Code;
        }
        [TestCase(null)] [TestCase(true)] [TestCase(1d)] [TestCase("1")] [TestCase(long.MaxValue)]
        public void MalformedStatusesAreNotCoercedIntoNativeSuccess(object code)
        {
            var native = new MalformedNative { Code = code };
            Assert.Throws<InvalidOperationException>(() => RootAnalysisMessagesService.Read(native, Path.GetFullPath("test.std"), true));
            Assert.Throws<InvalidOperationException>(() => RootAdinaExportService.Export(native, AdinaAxisSystem.Preserve, AdinaLoadCaseHandling.MultipleLoadCases));
        }
        private static AnalysisMessagesResult Read(NativeRoot native, bool errors = true) =>
            RootAnalysisMessagesService.Read(native, Path.GetFullPath("test.std"), errors);
    }
}
