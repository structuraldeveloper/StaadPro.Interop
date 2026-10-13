using System;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSRootJobInfoTests
    {
        public class NativeRoot
        {
            public object[] Fields = { " 雪 Name ", "Client", "Engineer", "2026-10-13", "Number", "Revision", "Part", "Reference", "Checker", "2026-10-12", "Approver", "2026-10-11", "line1\r\nline2" };
            public int Skip = -1;
            public int Calls;
            public Exception Failure;
            public int LengthCode, ForceCode;
            private void Begin() { Calls++; if (Failure != null) throw Failure; }
            private void Fill(ref object output, int index)
            {
                Assert.That(output, Is.TypeOf<string>(), "Native output must be marshaled as BSTR.");
                if (Skip != index) output = Fields[index];
            }
            public void GetShortJobInfo(ref object name, ref object client, ref object engineer)
            { Begin(); Fill(ref name, 0); Fill(ref client, 1); Fill(ref engineer, 2); }
            public void GetFullJobInfo(ref object name, ref object client, ref object engineer, ref object eDate,
                ref object number, ref object revision, ref object part, ref object reference, ref object checker,
                ref object cDate, ref object approver, ref object aDate, ref object comments)
            {
                Begin(); Fill(ref name, 0); Fill(ref client, 1); Fill(ref engineer, 2); Fill(ref eDate, 3);
                Fill(ref number, 4); Fill(ref revision, 5); Fill(ref part, 6); Fill(ref reference, 7);
                Fill(ref checker, 8); Fill(ref cDate, 9); Fill(ref approver, 10); Fill(ref aDate, 11); Fill(ref comments, 12);
            }
            public void SetInputUnits(int length, int force) { Begin(); LengthCode = length; ForceCode = force; }
        }

        [Test]
        public void ShortInfoMapsTextWithFreshStorageAndOneReadPerCall()
        {
            var native = new NativeRoot(); var root = new OSRootAdapter(native);
            var first = root.GetShortJobInfo(); var second = root.GetShortJobInfo();
            Assert.That(first.Name, Is.EqualTo(native.Fields[0]));
            Assert.That(first.Client, Is.EqualTo(native.Fields[1]));
            Assert.That(first.Originator, Is.EqualTo(native.Fields[2]));
            Assert.That(first, Is.Not.SameAs(second));
            first.Name = "edited"; native.Fields[1] = "native edit";
            Assert.That(second.Name, Is.EqualTo(" 雪 Name "));
            Assert.That(second.Client, Is.EqualTo("Client"));
            Assert.That(native.Calls, Is.EqualTo(2));
        }

        [Test]
        public void FullInfoMapsAllThirteenNativePositionsAndReturnsFreshSnapshots()
        {
            var native = new NativeRoot(); var root = new OSRootAdapter(native);
            var first = root.GetFullJobInfo(); var second = root.GetFullJobInfo();
            Assert.That(new[] { first.Name, first.Client, first.Originator, first.Number, first.Revision, first.Part,
                first.Reference, first.Checker, first.Approver, first.Comments },
                Is.EqualTo(new[] { " 雪 Name ", "Client", "Engineer", "Number", "Revision", "Part", "Reference", "Checker", "Approver", "line1\r\nline2" }));
            Assert.That(first.Originated, Is.EqualTo(new DateTime(2026, 10, 13)));
            Assert.That(first.Checked, Is.EqualTo(new DateTime(2026, 10, 12)));
            Assert.That(first.Approved, Is.EqualTo(new DateTime(2026, 10, 11)));
            Assert.That(first, Is.Not.SameAs(second));
            first.Checker = "edited"; first.Approved = DateTime.MinValue;
            Assert.That(second.Checker, Is.EqualTo("Checker"));
            Assert.That(second.Approved, Is.EqualTo(new DateTime(2026, 10, 11)));
            Assert.That(native.Calls, Is.EqualTo(2));
        }

        [Test]
        public void ExplicitEmptyTextAndUnspecifiedDatesAreValid()
        {
            var native = new NativeRoot { Fields = Enumerable.Repeat<object>(string.Empty, 13).ToArray() };
            native.Fields[9] = " \t ";
            var root = new OSRootAdapter(native);
            Assert.That(root.GetShortJobInfo().Name, Is.Empty);
            var full = root.GetFullJobInfo();
            Assert.That(full.Name, Is.Empty);
            Assert.That(full.Comments, Is.Empty);
            Assert.That(new[] { full.Originated, full.Checked, full.Approved }, Is.All.EqualTo(DateTime.MinValue));
            Assert.That(native.Calls, Is.EqualTo(2));
        }

        [Test]
        public void LiteralMarkerLikeJobNamesAreNotMistakenForUnwrittenOutput()
        {
            var native = new NativeRoot(); native.Fields[0] = "<StaadPro.Interop.UnwrittenJobInfo>";
            var root = new OSRootAdapter(native);
            Assert.That(root.GetShortJobInfo().Name, Is.EqualTo(native.Fields[0]));
            Assert.That(root.GetFullJobInfo().Name, Is.EqualTo(native.Fields[0]));
        }

        [TestCase(0)] [TestCase(1)] [TestCase(2)]
        public void ShortInfoRejectsEveryUnwrittenOrNonStringOutput(int index)
        {
            foreach (object invalid in new object[] { null, false, 17 })
            {
                var native = new NativeRoot(); native.Fields[index] = invalid;
                Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).GetShortJobInfo());
                Assert.That(native.Calls, Is.EqualTo(1));
            }
            var unwritten = new NativeRoot { Skip = index };
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(unwritten).GetShortJobInfo());
        }

        [TestCase(0)] [TestCase(1)] [TestCase(2)] [TestCase(3)] [TestCase(4)] [TestCase(5)]
        [TestCase(6)] [TestCase(7)] [TestCase(8)] [TestCase(9)] [TestCase(10)] [TestCase(11)] [TestCase(12)]
        public void FullInfoChecksEveryOutputBeforeConstructingOrParsing(int index)
        {
            foreach (object invalid in new object[] { null, true, 17 })
            {
                var native = new NativeRoot(); native.Fields[index] = invalid;
                Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).GetFullJobInfo());
                Assert.That(native.Calls, Is.EqualTo(1));
            }
            var unwritten = new NativeRoot { Skip = index };
            unwritten.Fields[3] = "bad date";
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(unwritten).GetFullJobInfo(), "All native string checks precede date parsing.");
        }

        [TestCase(3)] [TestCase(9)] [TestCase(11)]
        public void InvalidNonemptyDatesThrowInsteadOfReturningDefaultOrPartialRecords(int index)
        {
            var native = new NativeRoot(); native.Fields[index] = "not a date";
            Assert.Throws<FormatException>(() => new OSRootAdapter(native).GetFullJobInfo());
            Assert.That(native.Calls, Is.EqualTo(1));
        }

        public static IEnumerable UnitPairs
        {
            get { foreach (LengthInputUnit length in Enum.GetValues(typeof(LengthInputUnit)))
                foreach (ForceInputUnit force in Enum.GetValues(typeof(ForceInputUnit))) yield return new TestCaseData(length, force); }
        }

        [TestCaseSource(nameof(UnitPairs))]
        public void UnitSetterPreservesEveryDefinedCodeAndFluentReturn(LengthInputUnit length, ForceInputUnit force)
        {
            var native = new NativeRoot(); var root = new OSRootAdapter(native);
            Assert.That(root.SetInputUnitsEx(length, force), Is.SameAs(root));
            Assert.That(native.LengthCode, Is.EqualTo((int)length));
            Assert.That(native.ForceCode, Is.EqualTo((int)force));
            Assert.That(native.Calls, Is.EqualTo(1));
        }

        [Test]
        public void UnitSetterRejectsUndefinedEnumsBeforeNativeMutation()
        {
            var native = new NativeRoot(); var root = new OSRootAdapter(native);
            foreach (int invalid in new[] { -1, 1, 8, int.MaxValue })
                Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => root.SetInputUnitsEx((LengthInputUnit)invalid, ForceInputUnit.Newton)).ParamName, Is.EqualTo("lengthUnit"));
            foreach (int invalid in new[] { -1, 8, int.MaxValue })
                Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => root.SetInputUnitsEx(LengthInputUnit.Meter, (ForceInputUnit)invalid)).ParamName, Is.EqualTo("forceUnit"));
            Assert.That(native.Calls, Is.Zero);
        }

        [TestCase("short")] [TestCase("full")] [TestCase("units")]
        public void NativeAndMissingSignatureFailuresPropagateUnchanged(string operation)
        {
            var native = new NativeRoot { Failure = new COMException("Job/unit API failed") };
            Assert.That(Assert.Throws<COMException>(() => Invoke(new OSRootAdapter(native), operation)), Is.SameAs(native.Failure));
            Assert.That(native.Calls, Is.EqualTo(1));
            Assert.Throws<RuntimeBinderException>(() => Invoke(new OSRootAdapter(new object()), operation));
        }

        private static void Invoke(OSRootAdapter root, string operation)
        {
            if (operation == "short") root.GetShortJobInfo();
            else if (operation == "full") root.GetFullJobInfo();
            else root.SetInputUnitsEx(LengthInputUnit.Meter, ForceInputUnit.KiloNewton);
        }
    }
}
