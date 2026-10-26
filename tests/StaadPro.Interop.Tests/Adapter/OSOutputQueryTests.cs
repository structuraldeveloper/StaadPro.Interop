using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Output;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Output;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSOutputQueryTests
    {
        public sealed class Root
        {
            public object Output { get; set; }
            public object Geometry => null;
            public object Load => null;
        }
        public sealed class Native
        {
            public object Status = true, Availability = true, BucklingAvailability = 1;
            public object Ratio = 1.25, Section = "UC 305x305";
            public object Scalar1 = 1.25, Scalar2 = -2.5, Scalar3 = 3.75, Scalar4 = -4.125;
            public bool WriteOutputs = true;
            public int WriteCount = int.MaxValue;
            public Func<Array, object> ReplaceArray;
            public Exception Failure;
            public readonly List<string> Calls = new List<string>();
            public readonly List<object[]> Arguments = new List<object[]>();
            public readonly List<int> Threads = new List<int>();
            public readonly List<Array> Buffers = new List<Array>();
            private void Record(string name, params object[] arguments)
            {
                Calls.Add(name); Arguments.Add(arguments); Threads.Add(Thread.CurrentThread.ManagedThreadId);
                if (Failure != null) throw Failure;
            }
            private void Fill(ref object output, bool single)
            {
                var array = (Array)output;
                Assert.That(array.GetType().GetElementType(), Is.EqualTo(typeof(double)), "Concrete installed VBA examples use Double SAFEARRAYs.");
                foreach (object value in array) Assert.That(double.IsNaN(Convert.ToDouble(value)), Is.True, "Unwritten output must not look like zero.");
                Buffers.Add(array);
                if (!WriteOutputs) return;
                if (ReplaceArray != null) { output = ReplaceArray(array); return; }
                for (int i = 0; i < Math.Min(array.Length, WriteCount); i++)
                    array.SetValue(i + 1.125, i);
            }
            private object Read(string name, int id, int loadCaseId, ref object output, bool single = false)
            { Record(name, id, loadCaseId); Fill(ref output, single); return Status; }
            public object AreResultsAvailable() { Record(nameof(AreResultsAvailable)); return Availability; }
            public object IsBucklingAnalysisResultsAvailable() { Record(nameof(IsBucklingAnalysisResultsAvailable)); return BucklingAvailability; }
            public object GetNodeDisplacements(int id, int loadCaseId, ref object output) => Read(nameof(GetNodeDisplacements), id, loadCaseId, ref output);
            public object GetSupportReactions(int id, int loadCaseId, ref object output) => Read(nameof(GetSupportReactions), id, loadCaseId, ref output);
            public object GetMemberEndForces(int id, int end, int loadCaseId, ref object output, int localOrGlobal)
            { Record(nameof(GetMemberEndForces), id, end, loadCaseId, localOrGlobal); Fill(ref output, false); return Status; }
            public object GetStaticCheckResult(int loadCaseId, ref object forces, ref object reactions)
            { Record(nameof(GetStaticCheckResult), loadCaseId); Fill(ref forces, false); Fill(ref reactions, false); return Status; }
            public object GetAllPlateCenterStressesAndMoments(int id, int loadCaseId, ref object output) => Read(nameof(GetAllPlateCenterStressesAndMoments), id, loadCaseId, ref output);
            public object GetAllPlateCenterForces(int id, int loadCaseId, ref object output) => Read(nameof(GetAllPlateCenterForces), id, loadCaseId, ref output);
            public object GetAllPlateCenterMoments(int id, int loadCaseId, ref object output) => Read(nameof(GetAllPlateCenterMoments), id, loadCaseId, ref output);
            public object GetAllPlateCenterPrincipalStressesAndAngles(int id, int loadCaseId, ref object output) => Read(nameof(GetAllPlateCenterPrincipalStressesAndAngles), id, loadCaseId, ref output, true);
            public object GetAllPlateCenterPrincipalStressesAndAnglesEx(int id, int loadCaseId, ref object stresses, ref object angles)
            { Record(nameof(GetAllPlateCenterPrincipalStressesAndAnglesEx), id, loadCaseId); Fill(ref stresses, true); Fill(ref angles, true); return Status; }
            public object GetPlateCenterNormalPrincipalStresses(int id, int loadCaseId, ref object a, ref object b, ref object c, ref object d)
            { Record(nameof(GetPlateCenterNormalPrincipalStresses), id, loadCaseId); if (WriteOutputs) { a = Scalar1; b = Scalar2; c = Scalar3; d = Scalar4; } return Status; }
            public object GetPlateCenterVonMisesStresses(int id, int loadCaseId, ref object top, ref object bottom)
            { Record(nameof(GetPlateCenterVonMisesStresses), id, loadCaseId); if (WriteOutputs) { top = Scalar1; bottom = Scalar2; } return Status; }
            public object GetMemberSteelDesignRatio(int id, ref object ratio)
            { Record(nameof(GetMemberSteelDesignRatio), id); if (WriteOutputs) ratio = Ratio; return Status; }
            public object GetMemberDesignSectionName(int id) { Record(nameof(GetMemberDesignSectionName), id); return Section; }
        }
        private static OpenStaadWrapper Wrap(Native native) => new OpenStaadWrapper(new Root { Output = native });
        private static object[] Arguments(MethodInfo method) => method.GetParameters().Select(p =>
            p.ParameterType == typeof(MemberEnd) ? (object)MemberEnd.End : p.Name == "loadCaseId" ? 41 : 17).ToArray();
        private static object Invoke(IOSOutput output, MethodInfo method, object[] arguments = null)
        {
            try { return method.Invoke(output, arguments ?? Arguments(method)); }
            catch (TargetInvocationException ex) { System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex.InnerException).Throw(); throw; }
        }
        private static object Property(object result, string name) => result.GetType().GetProperty(name).GetValue(result);
        private static string NativeName(string name)
        { switch (name) { case "GetAnalysisResultAvailability": return "AreResultsAvailable"; case "GetBucklingResultAvailability": return "IsBucklingAnalysisResultsAvailable"; case "GetPlateCenterStressesAndMoments": return "GetAllPlateCenterStressesAndMoments"; case "GetPlateCenterForces": return "GetAllPlateCenterForces"; case "GetPlateCenterMoments": return "GetAllPlateCenterMoments"; case "GetPlateCenterPrincipalStressesAndAngles": return "GetAllPlateCenterPrincipalStressesAndAngles"; case "GetPlateCenterPrincipalStressesAndAnglesEx": return "GetAllPlateCenterPrincipalStressesAndAnglesEx"; case "GetMemberSteelUtilizationRatio": return "GetMemberSteelDesignRatio"; default: return name; } }
        private static readonly MethodInfo[] Queries = typeof(IOSOutput).GetMethods().Where(m => !m.IsSpecialName && m.GetParameters().All(p => p.ParameterType == typeof(int) || p.ParameterType == typeof(MemberEnd)) && typeof(Native).GetMethod(NativeName(m.Name)) != null).ToArray();
        private static IEnumerable<TestCaseData> EveryQuery => Queries.Select(m => new TestCaseData(m).SetArgDisplayNames(m.Name));
        private static IEnumerable<TestCaseData> Retrievals => Queries.Where(m => m.Name != "GetAnalysisResultAvailability" &&
            m.Name != "GetBucklingResultAvailability" && m.Name != "GetMemberDesignSectionName").Select(m => new TestCaseData(m));
        private static IEnumerable<TestCaseData> Arrays => Queries.Where(m => new[] {
            "GetNodeDisplacements", "GetSupportReactions", "GetMemberEndForces", "GetStaticCheckResult",
            "GetPlateCenterStressesAndMoments", "GetPlateCenterForces", "GetPlateCenterMoments",
            "GetPlateCenterPrincipalStressesAndAngles", "GetPlateCenterPrincipalStressesAndAnglesEx" }.Contains(m.Name))
            .Select(m => new TestCaseData(m));

        [TestCaseSource(nameof(EveryQuery))]
        public void EachQueryUsesOneNativeOperationOnCallerThreadAndRetainsIdentity(MethodInfo method)
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                Assert.That(native.Calls, Is.Empty);
                object result = Invoke(wrapper.Output, method);
                Assert.That(Property(result, "IsSuccessful"), Is.True);
                Assert.That(native.Calls, Has.Count.EqualTo(1));
                Assert.That(Property(result, "NativeFunction"), Is.EqualTo(native.Calls.Single()));
                Assert.That(native.Threads.Single(), Is.EqualTo(Thread.CurrentThread.ManagedThreadId));
                Assert.That(Property(result, "EntityId"), Is.EqualTo(method.GetParameters().Any(p => p.Name.EndsWith("Id") && p.Name != "loadCaseId") ? (int?)17 : null));
                Assert.That(Property(result, "LoadCaseId"), Is.EqualTo(method.GetParameters().Any(p => p.Name == "loadCaseId") ? (int?)41 : null));
                if (method.Name == "GetMemberEndForces")
                {
                    Assert.That(native.Arguments.Single(), Is.EqualTo(new object[] { 17, 1, 41, 0 }));
                    Assert.That(Property(result, "MemberEnd"), Is.EqualTo(MemberEnd.End));
                }
                else Assert.That(native.Arguments.Single(), Is.EqualTo(Arguments(method)));
            }
        }
        [TestCaseSource(nameof(Retrievals))]
        public void NativeFalseDoesNotPublishWrittenOrMalformedOutputs(MethodInfo method)
        {
            var native = new Native { Status = false, ReplaceArray = a => new object[] { "bad" }, Scalar1 = "bad", Ratio = "bad" };
            using (var wrapper = Wrap(native))
            {
                object result = Invoke(wrapper.Output, method);
                Assert.That(Property(result, "Status"), Is.EqualTo(OutputQueryStatus.NativeFailure));
                Assert.Throws<TargetInvocationException>(() => Property(result, "Data"));
                Assert.That(native.Calls, Has.Count.EqualTo(1), "No alternate-API retry.");
            }
        }
        [TestCaseSource(nameof(Retrievals))]
        public void NativeSuccessWithUnwrittenOutputsIsRejected(MethodInfo method)
        {
            using (var wrapper = Wrap(new Native { WriteOutputs = false }))
                Assert.Throws<InvalidOperationException>(() => Invoke(wrapper.Output, method));
        }
        [TestCaseSource(nameof(Arrays))]
        public void WrongRankTypeLengthNonfiniteAndPartialArraysAreRejected(MethodInfo method)
        {
            foreach (var replacement in new Func<Array, object>[] {
                a => new double[a.Length + 1], a => new double[Math.Max(0, a.Length - 1)], a => new double[1, a.Length],
                a => new object[a.Length], a => new int[a.Length], a => Enumerable.Repeat(double.NaN, a.Length).ToArray(),
                a => Enumerable.Repeat(double.PositiveInfinity, a.Length).ToArray(), a => null })
                using (var wrapper = Wrap(new Native { ReplaceArray = replacement }))
                    Assert.Throws<InvalidOperationException>(() => Invoke(wrapper.Output, method), method.Name);
            using (var wrapper = Wrap(new Native { WriteCount = 1 }))
                Assert.Throws<InvalidOperationException>(() => Invoke(wrapper.Output, method));
        }
        [Test]
        public void NamedPayloadsPreserveComponentDimensionsSurfaceOrderingAndOwnValues()
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                var displacement = wrapper.Output.GetNodeDisplacements(17, 41).Data;
                Assert.That(new[] { displacement.X, displacement.Y, displacement.Z, displacement.Rx, displacement.Ry, displacement.Rz },
                    Is.EqualTo(new[] { 1.125, 2.125, 3.125, 4.125, 5.125, 6.125 }));
                var reaction = wrapper.Output.GetSupportReactions(17, 41).Data;
                Assert.That(new[] { reaction.Fx, reaction.Fy, reaction.Fz, reaction.Mx, reaction.My, reaction.Mz },
                    Is.EqualTo(new[] { 1.125, 2.125, 3.125, 4.125, 5.125, 6.125 }));
                var editable = reaction.ToForces(); editable.Fx = 99;
                Assert.That(reaction.Fx, Is.EqualTo(1.125));
                var all = wrapper.Output.GetPlateCenterStressesAndMoments(17, 41).Data;
                Assert.That(new[] { all.Forces.Sqx, all.Forces.Sqy, all.Moments.Mx, all.Moments.My, all.Moments.Mxy, all.Forces.Sx, all.Forces.Sy, all.Forces.Sxy },
                    Is.EqualTo(Enumerable.Range(0, 8).Select(i => i + 1.125)));
                var stresses = wrapper.Output.GetPlateCenterForces(17, 41).Data;
                Assert.That(new[] { stresses.Sqx, stresses.Sqy, stresses.Sx, stresses.Sy, stresses.Sxy },
                    Is.EqualTo(Enumerable.Range(0, 5).Select(i => i + 1.125)));
                var moments = wrapper.Output.GetPlateCenterMoments(17, 41).Data;
                Assert.That(new[] { moments.Mx, moments.My, moments.Mxy }, Is.EqualTo(new[] { 1.125, 2.125, 3.125 }));
                var principal = wrapper.Output.GetPlateCenterNormalPrincipalStresses(17, 41).Data;
                Assert.That(new[] { principal.MaximumTop, principal.MinimumTop, principal.MaximumBottom, principal.MinimumBottom },
                    Is.EqualTo(new[] { 1.25, -2.5, 3.75, -4.125 }));
                var mises = wrapper.Output.GetPlateCenterVonMisesStresses(17, 41).Data;
                Assert.That(new[] { mises.Top, mises.Bottom }, Is.EqualTo(new[] { 1.25, -2.5 }));
                var check = wrapper.Output.GetStaticCheckResult(41).Data;
                Assert.That(check.Forces.Fx, Is.EqualTo(1.125)); Assert.That(check.Reactions.Mz, Is.EqualTo(6.125));
                foreach (Array buffer in native.Buffers)
                    for (int i = 0; i < buffer.Length; i++) buffer.SetValue(buffer.GetType().GetElementType() == typeof(float) ? (object)99f : 99.0, i);
                Assert.That(all.Forces.Sqx, Is.EqualTo(1.125)); Assert.That(displacement.Rz, Is.EqualTo(6.125));
            }
        }
        [Test]
        public void PrincipalCombinedAndSeparateBuffersHaveTheirDocumentedLayouts()
        {
            using (var wrapper = Wrap(new Native()))
            {
                var combined = wrapper.Output.GetPlateCenterPrincipalStressesAndAngles(17, 41).Data;
                Assert.That(new[] { combined.MaximumTop, combined.MinimumTop, combined.MaximumShearTop, combined.MaximumBottom,
                    combined.MinimumBottom, combined.MaximumShearBottom, combined.AngleTop, combined.AngleBottom },
                    Is.EqualTo(Enumerable.Range(0, 8).Select(i => i + 1.125)));
                var separate = wrapper.Output.GetPlateCenterPrincipalStressesAndAnglesEx(17, 41).Data;
                Assert.That(new[] { separate.MaximumTop, separate.MinimumTop, separate.MaximumShearTop, separate.MaximumBottom,
                    separate.MinimumBottom, separate.MaximumShearBottom, separate.AngleTop, separate.AngleBottom },
                    Is.EqualTo(new[] { 1.125, 2.125, 3.125, 4.125, 5.125, 6.125, 1.125, 2.125 }));
            }
        }
        [Test]
        public void NonzeroArrayLowerBoundsAndReturnedDoublePrincipalArraysAreAccepted()
        {
            Func<Array, object> lowerBound = source =>
            {
                Array result = Array.CreateInstance(typeof(double), new[] { source.Length }, new[] { -3 });
                for (int i = 0; i < source.Length; i++) result.SetValue(i + .125, i - 3);
                return result;
            };
            using (var wrapper = Wrap(new Native { ReplaceArray = lowerBound }))
            {
                Assert.That(wrapper.Output.GetSupportReactions(17, 41).Data.Mz, Is.EqualTo(5.125));
                Assert.That(wrapper.Output.GetPlateCenterPrincipalStressesAndAngles(17, 41).Data.AngleBottom, Is.EqualTo(7.125));
                Assert.That(wrapper.Output.GetPlateCenterPrincipalStressesAndAnglesEx(17, 41).Data.AngleBottom, Is.EqualTo(1.125));
            }
        }
        [Test]
        public void ReturnedSinglePrincipalArraysArePromotedWithoutAcceptingThemForOtherContracts()
        {
            Func<Array, object> singles = a => Enumerable.Range(0, a.Length).Select(i => (float)(i + .125)).ToArray();
            using (var wrapper = Wrap(new Native { ReplaceArray = singles }))
            {
                Assert.That(wrapper.Output.GetPlateCenterPrincipalStressesAndAngles(17, 41).Data.AngleBottom, Is.EqualTo(7.125));
                Assert.That(wrapper.Output.GetPlateCenterPrincipalStressesAndAnglesEx(17, 41).Data.AngleBottom, Is.EqualTo(1.125));
                Assert.Throws<InvalidOperationException>(() => wrapper.Output.GetSupportReactions(17, 41));
            }
        }
        [Test]
        public void FlagsAreStrictAndFalseAvailabilityIsSuccessfulData()
        {
            foreach (object status in new object[] { true, (short)1, 1, 1L, (short)-1, -1, -1L })
                using (var wrapper = Wrap(new Native { Status = status }))
                    Assert.That(wrapper.Output.GetSupportReactions(17, 41).IsSuccessful, Is.True);
            foreach (object status in new object[] { false, (short)0, 0, 0L })
                using (var wrapper = Wrap(new Native { Status = status, Availability = status, BucklingAvailability = status }))
                {
                    Assert.That(wrapper.Output.GetSupportReactions(17, 41).Status, Is.EqualTo(OutputQueryStatus.NativeFailure));
                    Assert.That(wrapper.Output.GetAnalysisResultAvailability().Data, Is.False);
                    Assert.That(wrapper.Output.GetBucklingResultAvailability().Data, Is.False);
                    Assert.That(wrapper.Output.GetAnalysisResultAvailability().IsSuccessful, Is.True);
                }
            foreach (object status in new object[] { null, DBNull.Value, 2, -2, .5, 1.0, 1m, "true", "1" })
                using (var wrapper = Wrap(new Native { Status = status, Availability = status, BucklingAvailability = status }))
                {
                    Assert.Throws<InvalidOperationException>(() => wrapper.Output.GetSupportReactions(17, 41));
                    Assert.Throws<InvalidOperationException>(() => wrapper.Output.GetAnalysisResultAvailability());
                    Assert.Throws<InvalidOperationException>(() => wrapper.Output.GetBucklingResultAvailability());
                }
            using (var wrapper = Wrap(new Native { BucklingAvailability = -1 }))
                Assert.Throws<InvalidOperationException>(() => wrapper.Output.GetBucklingResultAvailability());
        }
        [Test]
        public void SteelSentinelsEmptySectionAndNativeFailureRemainDistinct()
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                Assert.That(wrapper.Output.GetMemberSteelUtilizationRatio(17).Data, Is.EqualTo(1.25));
                native.Ratio = -999.0;
                var notDesigned = wrapper.Output.GetMemberSteelUtilizationRatio(17);
                Assert.That(notDesigned.Status, Is.EqualTo(OutputQueryStatus.MemberNotDesigned));
                Assert.Throws<InvalidOperationException>(() => { var ignored = notDesigned.Data; });
                native.Ratio = -1.0;
                Assert.That(wrapper.Output.GetMemberSteelUtilizationRatio(17).Status, Is.EqualTo(OutputQueryStatus.AnalysisNotPerformed));
                native.Status = false;
                Assert.That(wrapper.Output.GetMemberSteelUtilizationRatio(17).Status, Is.EqualTo(OutputQueryStatus.NativeFailure));
                native.Section = "";
                Assert.That(wrapper.Output.GetMemberDesignSectionName(17).Status, Is.EqualTo(OutputQueryStatus.NotFound));
                native.Section = "  UC  ";
                Assert.That(wrapper.Output.GetMemberDesignSectionName(17).Data, Is.EqualTo("  UC  "));
                native.Section = null;
                Assert.Throws<InvalidOperationException>(() => wrapper.Output.GetMemberDesignSectionName(17));
            }
        }
        [Test]
        public void ScalarsRejectCoercionNonfiniteAndUndocumentedNegativeRatios()
        {
            foreach (object value in new object[] { null, "1", 1, 1m, true, double.NaN, double.NegativeInfinity })
                using (var wrapper = Wrap(new Native { Scalar1 = value, Ratio = value }))
                {
                    Assert.Throws<InvalidOperationException>(() => wrapper.Output.GetPlateCenterNormalPrincipalStresses(17, 41));
                    Assert.Throws<InvalidOperationException>(() => wrapper.Output.GetPlateCenterVonMisesStresses(17, 41));
                    Assert.Throws<InvalidOperationException>(() => wrapper.Output.GetMemberSteelUtilizationRatio(17));
                }
            using (var wrapper = Wrap(new Native { Ratio = -2.0 }))
                Assert.Throws<InvalidOperationException>(() => wrapper.Output.GetMemberSteelUtilizationRatio(17));
        }
        [Test]
        public void ArgumentsAreValidatedBeforeNativeAccess()
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                foreach (var method in Queries)
                for (int p = 0; p < method.GetParameters().Length; p++)
                {
                    object[] args = Arguments(method);
                    args[p] = method.GetParameters()[p].ParameterType == typeof(MemberEnd) ? (object)(MemberEnd)19 : 0;
                    var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Invoke(wrapper.Output, method, args));
                    Assert.That(ex.ParamName, Is.EqualTo(method.GetParameters()[p].Name));
                }
                Assert.That(native.Calls, Is.Empty);
            }
        }
        [TestCaseSource(nameof(EveryQuery))]
        public void OriginalExceptionsPropagateAndStaleOrDisposedBindingsAreRejected(MethodInfo method)
        {
            var failure = new COMException("native failure", unchecked((int)0x80004005));
            var native = new Native { Failure = failure };
            var root = new Root { Output = native };
            using (var wrapper = new OpenStaadWrapper(root))
            {
                Assert.That(Assert.Throws<COMException>(() => Invoke(wrapper.Output, method)), Is.SameAs(failure));
                Assert.That(native.Calls, Has.Count.EqualTo(1));
                IOSOutput previous = wrapper.Output;
                root.Output = new Native(); wrapper.RefreshComObjects();
                Assert.Throws<InvalidOperationException>(() => Invoke(previous, method));
                IOSOutput current = wrapper.Output; wrapper.Dispose();
                Assert.Throws<InvalidOperationException>(() => Invoke(current, method));
            }
        }
        [Test]
        public void PublicContractHasSeventyOneTypedOperationsWithoutReferenceOutputs()
        {
            Assert.That(Queries, Has.Length.EqualTo(15));
            var all = typeof(IOSOutput).GetMethods().Where(m => !m.IsSpecialName).ToArray();
            Assert.That(all, Has.Length.EqualTo(71));
            foreach (var method in all)
            {
                Assert.That(method.GetParameters().Any(p => p.ParameterType.IsByRef), Is.False);
                Assert.That(method.ReturnType == typeof(MemberCoordinateSystem) || method.ReturnType == typeof(OutputPopulationResult) ||
                    method.ReturnType.IsGenericType && new[] { typeof(OutputQueryResult<>), typeof(OutputBatchResult<>) }.Contains(method.ReturnType.GetGenericTypeDefinition()), Is.True);
                Assert.That(typeof(OSOutputAdapter).GetMethod(method.Name, method.GetParameters().Select(p => p.ParameterType).ToArray()), Is.Not.Null);
            }
        }
    }
}
