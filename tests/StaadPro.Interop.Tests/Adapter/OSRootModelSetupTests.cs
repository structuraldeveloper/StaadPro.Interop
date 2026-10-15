using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;
using MetadataRoot = StaadPro.Interop.Tests.Models.OpenStaadWrapperParityTests.NativeRoot;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSRootModelSetupTests
    {
        public class NativeRoot : MetadataRoot
        {
            public object ProcessId = 42;
            public int LengthCode = 4, ForceCode = 5;
            public string[] JobFields;
            public Exception Failure;
            public Action BeforeJobWrite;
            public readonly List<string> Calls = new List<string>();

            public object GetProcessId()
            {
                Calls.Add("process");
                if (Failure != null) throw Failure;
                return ProcessId;
            }

            public void SetInputUnitForLength(object lengthUnit)
            {
                Calls.Add("length");
                Assert.That(lengthUnit, Is.TypeOf<int>());
                LengthCode = (int)lengthUnit;
                LengthText = string.Empty; LengthReturn = LengthCode;
                if (Failure != null) throw Failure;
            }

            public void SetInputUnitForForce(object forceUnit)
            {
                Calls.Add("force");
                Assert.That(forceUnit, Is.TypeOf<int>());
                ForceCode = (int)forceUnit;
                ForceText = string.Empty; ForceReturn = ForceCode;
                if (Failure != null) throw Failure;
            }

            public void SetShortJobInfo(object jobName, object jobClient, object engineerName)
            {
                Calls.Add("job");
                Assert.That(jobName, Is.TypeOf<string>());
                Assert.That(jobClient, Is.TypeOf<string>());
                Assert.That(engineerName, Is.TypeOf<string>());
                BeforeJobWrite?.Invoke();
                JobFields = new[] { (string)jobName, (string)jobClient, (string)engineerName };
                if (Failure != null) throw Failure;
            }

            public void UpdateStructure()
            {
                Calls.Add("update");
                if (Failure != null) throw Failure;
            }
        }

        [Test]
        public void ProcessQueryAcceptsIntegralSubtypesIncludingUnsignedRangeBoundaries()
        {
            foreach (object value in new object[] { (sbyte)1, (byte)1, (short)1, (ushort)1, 1, 1U, 1L, 1UL,
                int.MaxValue, (uint)int.MaxValue + 1, uint.MaxValue, (long)uint.MaxValue, (ulong)uint.MaxValue })
            {
                var native = new NativeRoot { ProcessId = value };
                Assert.That(new OSRootAdapter(native).GetProcessId(), Is.EqualTo(Convert.ToUInt32(value)));
                Assert.That(native.Calls, Is.EqualTo(new[] { "process" }));
                Assert.That(native.Queries, Is.Empty, "No HWND, version or unit lookup accompanies a PID read.");
            }
        }

        [TestCase(null)] [TestCase(0)] [TestCase(-1)] [TestCase(true)] [TestCase(false)]
        [TestCase("42")] [TestCase(42d)] [TestCase(0.5d)] [TestCase(double.NaN)]
        [TestCase(long.MaxValue)] [TestCase(long.MinValue)] [TestCase(ulong.MaxValue)]
        [TestCase(4294967296L)]
        public void ProcessQueryRejectsInvalidOrOutOfRangeValuesWithoutFallback(object value)
        {
            var native = new NativeRoot { ProcessId = value };
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).GetProcessId());
            Assert.That(native.Calls, Is.EqualTo(new[] { "process" }));
            Assert.That(native.Queries, Is.Empty);
        }

        [Test]
        public void ProcessQueryRejectsDecimalPointersAndDatabaseNull()
        {
            foreach (object value in new object[] { 42m, new IntPtr(42), new UIntPtr(42), DBNull.Value })
                Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(new NativeRoot { ProcessId = value }).GetProcessId());
        }

        [Test]
        public void ProcessQueryIsLiveAndUsesOnlyTheCapturedInstance()
        {
            var first = new NativeRoot { ProcessId = 41 };
            var second = new NativeRoot { ProcessId = 99U };
            var left = new OSRootAdapter(first); var right = new OSRootAdapter(second);
            Assert.That(first.Calls.Concat(second.Calls), Is.Empty);
            Assert.That(left.GetProcessId(), Is.EqualTo(41U));
            Assert.That(right.GetProcessId(), Is.EqualTo(99U));
            first.ProcessId = 51L;
            Assert.That(left.GetProcessId(), Is.EqualTo(51U));
            Assert.That(second.Calls, Is.EqualTo(new[] { "process" }));
        }

        [Test]
        public void PublicAndWindowControlPidValidationShareTheUnsignedBoundary()
        {
            var native = new NativeRoot { ProcessId = uint.MaxValue };
            var windows = new OSRootWindowTests.Windows { Process = uint.MaxValue };
            var root = new OSRootAdapter(native, windows);
            Assert.That(root.GetProcessId(), Is.EqualTo(uint.MaxValue));
            root.RestoreStaadMainWindow();
            Assert.That(windows.Calls, Is.EqualTo(new[] { "validate", "owner", "post" }));
            Assert.That(windows.Command, Is.EqualTo(9));
        }

        [TestCase(0)] [TestCase(1)] [TestCase(2)] [TestCase(3)]
        [TestCase(4)] [TestCase(5)] [TestCase(6)] [TestCase(7)]
        public void LengthSetterSendsEveryDocumentedInt32CodeWithoutReadingOrChangingForce(int code)
        {
            var native = new NativeRoot { ForceCode = 7 };
            new OSRootAdapter(native).SetInputUnitForLength((LengthInputUnit)code);
            Assert.That(native.LengthCode, Is.EqualTo(code));
            Assert.That(native.ForceCode, Is.EqualTo(7));
            Assert.That(native.Calls, Is.EqualTo(new[] { "length" }));
            Assert.That(native.Queries, Is.Empty);
        }

        [TestCase(0)] [TestCase(1)] [TestCase(2)] [TestCase(3)]
        [TestCase(4)] [TestCase(5)] [TestCase(6)] [TestCase(7)]
        public void ForceSetterSendsEveryDefinedInt32CodeWithoutReadingOrChangingLength(int code)
        {
            var native = new NativeRoot { LengthCode = 7 };
            new OSRootAdapter(native).SetInputUnitForForce((ForceInputUnit)code);
            Assert.That(native.ForceCode, Is.EqualTo(code));
            Assert.That(native.LengthCode, Is.EqualTo(7));
            Assert.That(native.Calls, Is.EqualTo(new[] { "force" }));
            Assert.That(native.Queries, Is.Empty);
        }

        [TestCase(-1)] [TestCase(8)] [TestCase(int.MinValue)] [TestCase(int.MaxValue)]
        public void InvalidUnitsAreRejectedBeforeAnyNativeAccess(int code)
        {
            var native = new NativeRoot(); var root = new OSRootAdapter(native);
            Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => root.SetInputUnitForLength((LengthInputUnit)code)).ParamName,
                Is.EqualTo("lengthUnit"));
            Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => root.SetInputUnitForForce((ForceInputUnit)code)).ParamName,
                Is.EqualTo("forceUnit"));
            Assert.That(native.Calls, Is.Empty);
            Assert.That(native.Queries, Is.Empty);
        }

        [Test]
        public void DocumentedLengthAliasDoesNotAlterExistingCombinedSetterValidation()
        {
            var native = new NativeRoot(); var root = new OSRootAdapter(native);
            root.SetInputUnitForLength((LengthInputUnit)1);
            Assert.That(native.LengthCode, Is.EqualTo(1));
            Assert.Throws<ArgumentOutOfRangeException>(() => root.SetInputUnitsEx((LengthInputUnit)1, ForceInputUnit.Newton));
            Assert.That(native.Calls, Is.EqualTo(new[] { "length" }));
        }

        [Test]
        public void WrapperUnitsRemainCachedUntilExplicitRefreshAfterIndependentSetters()
        {
            var native = new NativeRoot();
            using (var wrapper = new OpenStaadWrapper(native))
            {
                native.Calls.Clear(); native.Queries.Clear();
                var root = wrapper.Root;
                root.SetInputUnitForLength(LengthInputUnit.MilliMeter);
                root.SetInputUnitForForce(ForceInputUnit.Newton);
                Assert.That(wrapper.InputLengthUnit, Is.EqualTo(LengthInputUnit.Meter));
                Assert.That(wrapper.InputForceUnit, Is.EqualTo(ForceInputUnit.KiloNewton));
                Assert.That(native.Queries, Is.Empty);
                wrapper.RefreshUnits();
                Assert.That(wrapper.Root, Is.SameAs(root));
                Assert.That(wrapper.InputLengthUnit, Is.EqualTo(LengthInputUnit.MilliMeter));
                Assert.That(wrapper.InputForceUnit, Is.EqualTo(ForceInputUnit.Newton));
                Assert.That(native.Queries, Is.EqualTo(new[] { "base", "force", "length" }));
                Assert.That(native.Calls, Is.EqualTo(new[] { "length", "force" }));
            }
        }

        [TestCase("JOB", "CLIENT", "ENGINEER")]
        [TestCase("", "", "")]
        [TestCase("  雪 job  ", "Client\r\nName", " Engineer ")]
        public void ShortJobSetterPreservesAllStringsAndNativeOrder(string jobName, string jobClient, string engineerName)
        {
            var native = new NativeRoot();
            new OSRootAdapter(native).SetShortJobInfo(jobName, jobClient, engineerName);
            Assert.That(native.JobFields, Is.EqualTo(new[] { jobName, jobClient, engineerName }));
            Assert.That(native.Calls, Is.EqualTo(new[] { "job" }));
            Assert.That(native.Queries, Is.Empty);
        }

        [TestCase("jobName")] [TestCase("jobClient")] [TestCase("engineerName")]
        public void NullShortJobInputsIdentifyTheParameterAndPreventNativeMutation(string parameter)
        {
            var native = new NativeRoot();
            var error = Assert.Throws<ArgumentNullException>(() => new OSRootAdapter(native).SetShortJobInfo(
                parameter == "jobName" ? null : "name", parameter == "jobClient" ? null : "client",
                parameter == "engineerName" ? null : "engineer"));
            Assert.That(error.ParamName, Is.EqualTo(parameter));
            Assert.That(native.Calls, Is.Empty);
            Assert.That(native.Queries, Is.Empty);
        }

        [Test]
        public void ShortJobObjectOverloadReusesTheExistingModelAndSnapshotsAllFieldsBeforeDispatch()
        {
            var record = ShortJobInfo.From("name", "client", "engineer");
            var native = new NativeRoot { BeforeJobWrite = () => { record.Name = "changed"; record.Client = "changed"; record.Originator = "changed"; } };
            new OSRootAdapter(native).SetShortJobInfo(record);
            Assert.That(native.JobFields, Is.EqualTo(new[] { "name", "client", "engineer" }));
            Assert.That(record.Name, Is.EqualTo("changed"));
            Assert.That(native.Calls, Is.EqualTo(new[] { "job" }));
        }

        [Test]
        public void ShortJobObjectOverloadAcceptsEmptyTextAndOnlyUsesInheritedFieldsFromFullJobInfo()
        {
            var record = new FullJobInfo { Name = "", Client = "", Originator = "", Number = "not submitted", Comments = "not submitted" };
            var native = new NativeRoot();
            new OSRootAdapter(native).SetShortJobInfo(record);
            Assert.That(native.JobFields, Is.EqualTo(new[] { "", "", "" }));
            Assert.That(native.Calls, Is.EqualTo(new[] { "job" }));
        }

        [Test]
        public void NullJobObjectIsRejectedBeforeNativeAccess()
        {
            var native = new NativeRoot();
            Assert.That(Assert.Throws<ArgumentNullException>(() => new OSRootAdapter(native).SetShortJobInfo((ShortJobInfo)null)).ParamName,
                Is.EqualTo("jobInfo"));
            Assert.That(native.Calls, Is.Empty);
        }

        [TestCase("Name")] [TestCase("Client")] [TestCase("Originator")]
        public void JobObjectWithUninitializedFieldsIsRejectedBeforeNativeAccess(string property)
        {
            var record = ShortJobInfo.From("name", "client", "engineer");
            typeof(ShortJobInfo).GetProperty(property).SetValue(record, null);
            var native = new NativeRoot();
            Assert.That(Assert.Throws<ArgumentException>(() => new OSRootAdapter(native).SetShortJobInfo(record)).ParamName,
                Is.EqualTo("jobInfo"));
            Assert.That(native.Calls, Is.Empty);
        }

        [Test]
        public void UpdateStructureRequestsExactlyOneNativeUpdateWithoutSaveAnalysisOrRefresh()
        {
            var native = new NativeRoot();
            new OSRootAdapter(native).UpdateStructure();
            Assert.That(native.Calls, Is.EqualTo(new[] { "update" }));
            Assert.That(native.Queries, Is.Empty);
        }

        [TestCase("process")] [TestCase("length")] [TestCase("force")]
        [TestCase("job")] [TestCase("jobObject")] [TestCase("update")]
        public void NativeFailuresPropagateUnchangedWithoutRetriesOrRollback(string operation)
        {
            var failure = new COMException("Native request failed", unchecked((int)0x80010108));
            var native = new NativeRoot { Failure = failure };
            Assert.That(Assert.Throws<COMException>(() => Invoke(new OSRootAdapter(native), operation)), Is.SameAs(failure));
            Assert.That(native.Calls, Is.EqualTo(new[] { operation == "jobObject" ? "job" : operation }));
            Assert.That(native.Queries, Is.Empty);
            if (operation == "length") Assert.That(native.LengthCode, Is.EqualTo(5), "A lost response does not imply no native effects.");
            if (operation == "force") Assert.That(native.ForceCode, Is.EqualTo(4));
        }

        [TestCase("process")] [TestCase("length")] [TestCase("force")]
        [TestCase("job")] [TestCase("jobObject")] [TestCase("update")]
        public void MissingNativeSignaturesPropagateWithoutSubstitution(string operation)
        {
            Assert.Throws<RuntimeBinderException>(() => Invoke(new OSRootAdapter(new object()), operation));
        }

        [Test]
        public void MutationsRemainOnTheirOwnInstances()
        {
            var first = new NativeRoot(); var second = new NativeRoot();
            var left = new OSRootAdapter(first); var right = new OSRootAdapter(second);
            left.SetInputUnitForLength(LengthInputUnit.MilliMeter);
            right.SetInputUnitForForce(ForceInputUnit.Newton);
            left.SetShortJobInfo("first", "client", "engineer");
            right.UpdateStructure();
            Assert.That(first.Calls, Is.EqualTo(new[] { "length", "job" }));
            Assert.That(second.Calls, Is.EqualTo(new[] { "force", "update" }));
            Assert.That(first.ForceCode, Is.EqualTo(5));
            Assert.That(second.LengthCode, Is.EqualTo(4));
        }

        [Test]
        public void NewPublicMethodsUseDescriptiveNamesAndNoReferenceParameters()
        {
            var methods = typeof(IOSRoot).GetMethods().Where(m => new[] {
                "GetProcessId", "SetInputUnitForLength", "SetInputUnitForForce", "SetShortJobInfo", "UpdateStructure" }.Contains(m.Name)).ToArray();
            Assert.That(methods.Length, Is.EqualTo(6));
            Assert.That(methods.SelectMany(m => m.GetParameters()).Any(p => p.IsOut || p.ParameterType.IsByRef), Is.False);
            Assert.That(typeof(IOSRoot).GetMethod("SetInputUnitForLength").GetParameters().Single().Name, Is.EqualTo("lengthUnit"));
            Assert.That(typeof(IOSRoot).GetMethod("SetInputUnitForForce").GetParameters().Single().Name, Is.EqualTo("forceUnit"));
        }

        private static void Invoke(IOSRoot root, string operation)
        {
            switch (operation)
            {
                case "process": root.GetProcessId(); break;
                case "length": root.SetInputUnitForLength(LengthInputUnit.MilliMeter); break;
                case "force": root.SetInputUnitForForce(ForceInputUnit.Newton); break;
                case "job": root.SetShortJobInfo("name", "client", "engineer"); break;
                case "jobObject": root.SetShortJobInfo(ShortJobInfo.From("name", "client", "engineer")); break;
                default: root.UpdateStructure(); break;
            }
        }
    }
}
