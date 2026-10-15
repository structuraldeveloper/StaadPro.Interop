using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums.Root;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture, NonParallelizable]
    public class OSRootFullJobAndAnalysisRequestTests
    {
        public class NativeRoot
        {
            public readonly List<string> Calls = new List<string>();
            public string[] JobFields;
            public int? Engine;
            public Exception Failure;

            public void SetFullJobInfo(object name, object client, object engineer, object engineerDate, object number,
                object revision, object part, object reference, object checker, object checkerDate, object approver,
                object approverDate, object comments)
            {
                Calls.Add("full");
                object[] values = { name, client, engineer, engineerDate, number, revision, part, reference,
                    checker, checkerDate, approver, approverDate, comments };
                Assert.That(values.All(v => v is string), Is.True);
                JobFields = values.Cast<string>().ToArray();
                if (Failure != null) throw Failure;
            }

            public void Analyze()
            {
                Calls.Add("analyze");
                if (Failure != null) throw Failure;
            }

            public void AnalyzeModel(object engine)
            {
                Calls.Add("engine");
                Assert.That(engine, Is.TypeOf<int>());
                Engine = (int)engine;
                if (Failure != null) throw Failure;
            }
        }

        private static readonly string[] ParameterNames = {
            "jobName", "jobClient", "engineerName", "engineerDate", "jobNumber", "revision", "part", "reference",
            "checkerName", "checkerDate", "approverName", "approverDate", "comments"
        };

        [Test]
        public void FullJobSetterSendsThirteenDistinctStringsInNativeOrder()
        {
            string[] values = Enumerable.Range(0, 13).Select(i => "field-" + i).ToArray();
            var native = new NativeRoot();
            Write(new OSRootAdapter(native), values);
            Assert.That(native.JobFields, Is.EqualTo(values));
            Assert.That(native.Calls, Is.EqualTo(new[] { "full" }));
            Assert.That(typeof(IOSRoot).GetMethod("SetFullJobInfo").GetParameters().Select(p => p.Name), Is.EqualTo(ParameterNames));
        }

        [TestCase(0)] [TestCase(1)] [TestCase(2)] [TestCase(3)] [TestCase(4)] [TestCase(5)] [TestCase(6)]
        [TestCase(7)] [TestCase(8)] [TestCase(9)] [TestCase(10)] [TestCase(11)] [TestCase(12)]
        public void EveryNullFullJobFieldIsRejectedBeforeDispatch(int index)
        {
            var values = Enumerable.Repeat("value", 13).ToArray(); values[index] = null;
            var native = new NativeRoot();
            Assert.That(Assert.Throws<ArgumentNullException>(() => Write(new OSRootAdapter(native), values)).ParamName,
                Is.EqualTo(ParameterNames[index]));
            Assert.That(native.Calls, Is.Empty);
        }

        [TestCase("en-US")] [TestCase("fr-FR")]
        public void FullJobDatesAndTextAreNotParsedFormattedOrTrimmed(string culture)
        {
            var originalCulture = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
                var values = Enumerable.Repeat("", 13).ToArray();
                values[0] = "  雪 job  "; values[3] = "literal-engineer-date";
                values[9] = "01/02/2030"; values[11] = "  "; values[12] = "line 1\r\nline 2";
                var native = new NativeRoot();
                Write(new OSRootAdapter(native), values);
                Assert.That(native.JobFields, Is.EqualTo(values));
                Assert.That(native.Calls, Is.EqualTo(new[] { "full" }));
            }
            finally { CultureInfo.CurrentCulture = originalCulture; }
        }

        [Test]
        public void FullJobAllowsExplicitEmptyTextForAllFields()
        {
            var native = new NativeRoot();
            Write(new OSRootAdapter(native), Enumerable.Repeat(string.Empty, 13).ToArray());
            Assert.That(native.JobFields, Has.All.EqualTo(string.Empty));
        }

        [Test]
        public void AnalyzeUsesOnlyItsOwnNoArgumentNativeEntryPoint()
        {
            var native = new NativeRoot();
            new OSRootAdapter(native).Analyze();
            Assert.That(native.Calls, Is.EqualTo(new[] { "analyze" }));
            Assert.That(native.Engine, Is.Null);
        }

        [TestCase(true)] [TestCase(false)]
        public void AnalyzeModelSendsTheDocumentedEngineForDefaultAndExplicitCalls(bool useDefault)
        {
            var native = new NativeRoot(); var root = new OSRootAdapter(native);
            if (useDefault) root.AnalyzeModel(); else root.AnalyzeModel(AnalysisEngine.StaadPro);
            Assert.That(native.Calls, Is.EqualTo(new[] { "engine" }));
            Assert.That(native.Engine, Is.EqualTo(-1));
            foreach (var type in new[] { typeof(IOSRoot), typeof(OSRootAdapter) })
            {
                var parameter = type.GetMethod("AnalyzeModel").GetParameters().Single();
                Assert.That(parameter.IsOptional, Is.True);
                Assert.That(parameter.DefaultValue, Is.EqualTo(AnalysisEngine.StaadPro));
            }
        }

        [TestCase(-2)] [TestCase(0)] [TestCase(1)] [TestCase(int.MinValue)] [TestCase(int.MaxValue)]
        public void UnknownEnginesAreRejectedBeforeNativeMutation(int code)
        {
            var native = new NativeRoot();
            Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => new OSRootAdapter(native).AnalyzeModel((AnalysisEngine)code)).ParamName,
                Is.EqualTo("engine"));
            Assert.That(native.Calls, Is.Empty);
        }

        [TestCase("full")] [TestCase("analyze")] [TestCase("engine")]
        public void NativeFailuresPropagateWithoutRetriesRollbackOrSubstituteCalls(string operation)
        {
            var failure = new COMException("Native request failed", unchecked((int)0x80010108));
            var native = new NativeRoot { Failure = failure };
            Assert.That(Assert.Throws<COMException>(() => Invoke(new OSRootAdapter(native), operation)), Is.SameAs(failure));
            Assert.That(native.Calls, Is.EqualTo(new[] { operation }));
            if (operation == "full") Assert.That(native.JobFields, Is.Not.Null, "The library does not infer that a failed response had no effects.");
        }

        [TestCase("full")] [TestCase("analyze")] [TestCase("engine")]
        public void MissingNativeMembersRemainDispatchErrors(string operation)
        {
            Assert.Throws<RuntimeBinderException>(() => Invoke(new OSRootAdapter(new object()), operation));
        }

        [Test]
        public void ConstructorIsCaptureOnlyAndRequestsStayOnTheirAcquiredInstances()
        {
            var first = new NativeRoot(); var second = new NativeRoot();
            var left = new OSRootAdapter(first); var right = new OSRootAdapter(second);
            Assert.That(first.Calls.Concat(second.Calls), Is.Empty);
            left.Analyze(); right.AnalyzeModel();
            Write(left, Enumerable.Repeat("first", 13).ToArray());
            Assert.That(first.Calls, Is.EqualTo(new[] { "analyze", "full" }));
            Assert.That(second.Calls, Is.EqualTo(new[] { "engine" }));
            Assert.That(second.JobFields, Is.Null);
        }

        private static void Invoke(IOSRoot root, string operation)
        {
            if (operation == "full") Write(root, Enumerable.Repeat("value", 13).ToArray());
            else if (operation == "analyze") root.Analyze();
            else root.AnalyzeModel();
        }

        private static void Write(IOSRoot root, string[] values) => root.SetFullJobInfo(values[0], values[1], values[2],
            values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12]);
    }
}
