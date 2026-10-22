using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Loads.Seismic;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class LoadReferenceCompletionTests
    {
        public class Native
        {
            public object Result = true, WallStatus = 0;
            public Exception Failure;
            public Action<object[]> OnCall;
            public readonly List<string> Calls = new List<string>();
            public readonly List<object[]> Arguments = new List<object[]>();
            public readonly List<int> Threads = new List<int>();
            private object Record(string operation, params object[] arguments)
            {
                Calls.Add(operation); Arguments.Add(arguments); Threads.Add(Thread.CurrentThread.ManagedThreadId);
                OnCall?.Invoke(arguments);
                if (Failure != null) throw Failure;
                return operation == "wall" ? WallStatus : Result;
            }
            public object AddSeismicDefMemberWeight(int code, int type, double weight, double start, double end, int[] members) =>
                Record("member", code, type, weight, start, end, members);
            public object AddSeismicDefWallArea(int code, string direction, double[] dimensions) => Record("wall", code, direction, dimensions);
            public object AddResponseSpectrumLoadEx(int code, int combination, string[] names, double[] values,
                string[] generationNames, double[] generationValues, double[] pairs) =>
                Record("extended", code, combination, names, values, generationNames, generationValues, pairs);
            public object AddDirectAnalysisDefinitionParameter(int type, int[] members, double value) => Record("direct", type, members, value);
            public object DeleteDirectAnalysisDefinitionParameter(int type) => Record("delete-type", type);
            public object DeleteDirectAnalysisDefinition() => Record("delete-all");
            public object AddRSLoad(int type, float[] factors, int quantity, float scale, int dampingType, float damping,
                int interpolation, int missing, float missingFactor, int zpa, float zpaFactor, int ff1, float ff1Factor,
                int ff2, float ff2Factor, int save, int count, float[] periods, float[] values) =>
                Record("legacy", type, factors, quantity, scale, dampingType, damping, interpolation, missing, missingFactor,
                    zpa, zpaFactor, ff1, ff1Factor, ff2, ff2Factor, save, count, periods, values);
        }
        public sealed class Once<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> values;
            public int Enumerations;
            public Once(IEnumerable<T> values) { this.values = values; }
            public IEnumerator<T> GetEnumerator()
            {
                if (++Enumerations != 1) throw new InvalidOperationException("Input was enumerated twice.");
                return values.GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        private static OpenStaadWrapper Wrap(Native native) => new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(native));
        private static Beam Beam(int id) => new Beam(id, new Node(1, 0, 0, 0), new Node(2, 1, 0, 0));
        private static ResponseSpectrumLoadParameters Legacy() => new ResponseSpectrumLoadParameters
        { SpectrumPoints = new List<SpectrumCurvePoint> { new SpectrumCurvePoint(.125, .25), new SpectrumCurvePoint(1.25, -.5) } };
        private static ResponseSpectrumLoadExParameters Extended() => new ResponseSpectrumLoadExParameters()
            .WithDirection(x: -1.25, z: .5).WithDamping(.05).WithScale(9.80665);

        [Test]
        public void MemberBeamAndConvenienceOverloadsDelegateExactlyAndRetainIdentityOrder()
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                var original = new[] { Beam(17), Beam(17), Beam(7) };
                var members = new Once<Beam>(original);
                Assert.That(wrapper.Load.AddSeismicDefMemberWeight(0, SeismicMemberLoadType.Uniform, -2.5, 1, 3, members), Is.True);
                Assert.That(members.Enumerations, Is.EqualTo(1));
                Assert.That(native.Arguments[0].Take(5), Is.EqualTo(new object[] { 0, 1, -2.5, 1.0, 3.0 }));
                Assert.That(native.Arguments[0][5], Is.EqualTo(new[] { 17, 17, 7 }));
                ((int[])native.Arguments[0][5])[0] = 99; Assert.That(original[0].Id, Is.EqualTo(17));
                wrapper.Load.AddSeismicDefMemberUniformWeight(1.25, new[] { 17 });
                Assert.That(native.Arguments[1].Take(5), Is.EqualTo(new object[] { 0, 1, 1.25, 0.0, 0.0 }));
                wrapper.Load.AddSeismicDefMemberConcentratedWeight(2.25, 1.5, new[] { 17 });
                Assert.That(native.Arguments[2].Take(5), Is.EqualTo(new object[] { 0, 2, 2.25, 1.5, 0.0 }));
                Assert.Throws<NotSupportedException>(() => wrapper.Load.AddSeismicDefMemberUniformWeight(1, new[] { 17 }, seismicCode: 5));
                Assert.That(native.Calls.Count, Is.EqualTo(3));
            }
        }

        [Test]
        public void WallsUseTheirOwnCodeDomainAndTypedPairedSnapshotWithoutUnitConversion()
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                var dimensions = new Once<double>(new[] { 4.123456789123, .25, 4.123456789123, .25 });
                Assert.That(wrapper.Load.AddSeismicDefWallArea(15, " x ", dimensions), Is.Zero);
                Assert.That(dimensions.Enumerations, Is.EqualTo(1));
                Assert.That(native.Arguments[0][0], Is.EqualTo(15)); Assert.That(native.Arguments[0][1], Is.EqualTo("X"));
                Assert.That(native.Arguments[0][2], Is.TypeOf<double[]>().And.EqualTo(new[] { 4.123456789123, .25, 4.123456789123, .25 }));
                var wall = new SeismicWallDimension(6, .3); var walls = new Once<SeismicWallDimension>(new[] { wall, wall });
                wrapper.Load.AddSeismicDefWallArea(15, SeismicWallDirection.Z, walls);
                Assert.That(walls.Enumerations, Is.EqualTo(1));
                Assert.That(native.Arguments[1][2], Is.EqualTo(new[] { 6.0, .3, 6.0, .3 }));
                ((double[])native.Arguments[1][2])[0] = 99;
                Assert.That(wall.Length, Is.EqualTo(6)); Assert.That(native.Arguments[1][1], Is.EqualTo("Z"));
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.AddSeismicDefWallArea((int)SeismicCode.IS1893_2016, "X", new[] { 4.0, .25 }));
                Assert.That(native.Calls.Count, Is.EqualTo(2));
            }
        }

        [Test]
        public void DirectAnalysisOverloadsFactoriesAndDeletionHaveExactCodesAndNoAnalysisSideEffects()
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                var ids = new Once<int>(new[] { 17, 17, 7 });
                wrapper.Load.AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType.Flex, ids, -.123456789123);
                Assert.That(ids.Enumerations, Is.EqualTo(1));
                Assert.That(native.Arguments[0], Is.EqualTo(new object[] { 0, new[] { 17, 17, 7 }, -.123456789123 }));
                var members = new Once<Beam>(new[] { Beam(17), Beam(7) });
                wrapper.Load.AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType.Axial, members);
                Assert.That(members.Enumerations, Is.EqualTo(1));
                Assert.That(native.Arguments[1], Is.EqualTo(new object[] { 2, new[] { 17, 7 }, 0.0 }));
                wrapper.Load.AddDirectAnalysisFlexParameter(.75, new[] { 17 });
                wrapper.Load.AddDirectAnalysisAxialParameter(new[] { 7 });
                Assert.That(native.Arguments[2], Is.EqualTo(new object[] { 0, new[] { 17 }, .75 }));
                Assert.That(native.Arguments[3], Is.EqualTo(new object[] { 2, new[] { 7 }, 0.0 }));
                var source = new[] { 17, 7 }; var model = DirectAnalysisParameters.CreateFlex(.8, source);
                source[0] = 99; wrapper.Load.AddDirectAnalysisDefinitionParameter(model);
                Assert.That(native.Arguments[4], Is.EqualTo(new object[] { 0, new[] { 17, 7 }, .8 }));
                var axial = new DirectAnalysisParameters(DirectAnalysisParameterType.Axial, double.NaN, new[] { 17 });
                Assert.That(axial.ParameterValue, Is.Zero); axial.Validate();
                Assert.That(DirectAnalysisParameters.CreateAxial(new[] { 17 }).ParameterValue, Is.Zero);
                wrapper.Load.DeleteDirectAnalysisDefinitionParameter(DirectAnalysisParameterType.Flex);
                wrapper.Load.DeleteDirectAnalysisDefinitionParameter(DirectAnalysisParameterType.Axial);
                wrapper.Load.DeleteDirectAnalysisDefinition();
                Assert.That(native.Arguments[5], Is.EqualTo(new object[] { 0 }));
                Assert.That(native.Arguments[6], Is.EqualTo(new object[] { 2 }));
                Assert.That(native.Arguments[7], Is.Empty);
                Assert.That(native.Calls, Is.EqualTo(new[] { "direct", "direct", "direct", "direct", "direct", "delete-type", "delete-type", "delete-all" }));
                Assert.That(native.Threads.All(t => t == Thread.CurrentThread.ManagedThreadId), Is.True);
            }
        }

        [Test]
        public void EmptyRawSelectionsAreNoOpsButTypedDirectRequestRequiresAtLeastOneMember()
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                Assert.That(wrapper.Load.AddSeismicDefMemberWeight(0, SeismicMemberLoadType.Uniform, 1, 0, 0, new Beam[0]), Is.True);
                Assert.That(wrapper.Load.AddSeismicDefMemberUniformWeight(1, new int[0]), Is.True);
                Assert.That(wrapper.Load.AddSeismicDefMemberConcentratedWeight(1, 0, new int[0]), Is.True);
                Assert.That(wrapper.Load.AddSeismicDefWallArea(15, "X", new double[0]), Is.Zero);
                Assert.That(wrapper.Load.AddSeismicDefWallArea(15, SeismicWallDirection.Z, new SeismicWallDimension[0]), Is.Zero);
                Assert.That(wrapper.Load.AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType.Flex, new int[0]), Is.True);
                Assert.That(wrapper.Load.AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType.Axial, new Beam[0]), Is.True);
                Assert.That(wrapper.Load.AddDirectAnalysisFlexParameter(.8, new int[0]), Is.True);
                Assert.That(wrapper.Load.AddDirectAnalysisAxialParameter(new int[0]), Is.True);
                Assert.Throws<ArgumentException>(() => wrapper.Load.AddDirectAnalysisDefinitionParameter(new DirectAnalysisParameters()));
                Assert.That(native.Calls, Is.Empty);
            }
        }

        [Test]
        public void ExtendedSpectrumUsesSevenOwnedDoubleSlotsAndTypedPairsRetainDictionaryAssociation()
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                var names = new[] { "X", "DAM", "SCA" }; var values = new[] { -1.123456789123, .05, 9.80665 };
                var pairs = new[] { 1.0, -.5, .125, .25 }; // Order is intentionally retained rather than sorted.
                wrapper.Load.AddResponseSpectrumLoadEx(0, 2, names, values, customDataPairs: pairs);
                var a = native.Arguments.Single();
                Assert.That(a.Take(2), Is.EqualTo(new object[] { 0, 2 }));
                Assert.That(a[2], Is.TypeOf<string[]>().And.EqualTo(names)); Assert.That(a[3], Is.TypeOf<double[]>().And.EqualTo(values));
                Assert.That(a[4], Is.Null); Assert.That(a[5], Is.Null); Assert.That(a[6], Is.TypeOf<double[]>().And.EqualTo(pairs));
                ((string[])a[2])[0] = "Y"; ((double[])a[3])[0] = 99; ((double[])a[6])[0] = 99;
                Assert.That(names[0], Is.EqualTo("X")); Assert.That(values[0], Is.EqualTo(-1.123456789123)); Assert.That(pairs[0], Is.EqualTo(1));
                var request = Extended(); request.Code = ResponseSpectrumCode.IS1893_2016;
                request.CustomSpectrumData = new List<SpectrumCurvePoint> { new SpectrumCurvePoint(.1, .2), new SpectrumCurvePoint(.1, .3) };
                var before = request.PrimaryParameters.ToArray();
                wrapper.Load.AddResponseSpectrumLoadEx(request);
                var b = native.Arguments[1];
                Assert.That(b[0], Is.EqualTo(2)); Assert.That(b[1], Is.EqualTo(2));
                Assert.That(b[2], Is.EqualTo(before.Select(p => p.Key))); Assert.That(b[3], Is.EqualTo(before.Select(p => p.Value)));
                Assert.That(b[6], Is.EqualTo(new[] { .1, .2, .1, .3 }));
                ((double[])b[6])[0] = 99; Assert.That(request.CustomSpectrumData[0].Period, Is.EqualTo(.1));
                Assert.That(native.Calls, Is.EqualTo(new[] { "extended", "extended" }));
            }
        }

        [Test]
        public void ExtendedGenerationDataAndOptionalNullNormalizationRemainDistinctFromCustomPoints()
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                wrapper.Load.AddResponseSpectrumLoadEx(2, 0, new[] { "X" }, new[] { 1.0 }, new[] { "SOI", "RF" }, new[] { 2.0, 5 });
                Assert.That(native.Arguments[0][4], Is.EqualTo(new[] { "SOI", "RF" })); Assert.That(native.Arguments[0][6], Is.Null);
                wrapper.Load.AddResponseSpectrumLoadEx(0, 0, new[] { "X" }, new[] { 1.0 }, new string[0], new double[0], new double[0]);
                Assert.That(native.Arguments[1].Skip(4), Is.All.Null);
                var request = Extended(); request.SpectrumGenerationParameters = null; request.CustomSpectrumData = null;
                request.Validate(); wrapper.Load.AddResponseSpectrumLoadEx(request);
                Assert.That(native.Arguments[2].Skip(4), Is.All.Null);
            }
        }

        [Test]
        public void BothModalDomainsAndAllExtendedEngineeringCodesRetainTheirDocumentedNumbers()
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                foreach (var code in Enum.GetValues(typeof(ResponseSpectrumCode)).Cast<ResponseSpectrumCode>())
                    foreach (var combination in Enum.GetValues(typeof(ModalCombinationMethod)).Cast<ModalCombinationMethod>())
                    {
                        wrapper.Load.AddResponseSpectrumLoadEx((int)code, (int)combination, new[] { "X" }, new[] { 1.0 });
                        Assert.That(native.Arguments.Last().Take(2), Is.EqualTo(new object[] { (int)code, (int)combination }));
                    }
                foreach (var type in Enum.GetValues(typeof(ResponseSpectrumLoadType)).Cast<ResponseSpectrumLoadType>())
                {
                    var p = Legacy(); p.LoadType = type; wrapper.Load.AddRSLoad(p);
                    Assert.That(native.Arguments.Last()[0], Is.EqualTo((int)type));
                }
                Assert.That((int)ModalCombinationMethod.SRSS, Is.Zero); Assert.That((int)ResponseSpectrumLoadType.Srss, Is.EqualTo(1));
            }
        }

        [Test]
        public void LegacySpectrumUsesNineteenSlotsWithSinglePrecisionAndOwnedArrays()
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                var p = Legacy(); p.DirectionFactors = new[] { -1.123456789123, .5, 2.0 };
                p.QuantityType = SpectrumQuantityType.Displacement; p.Scale = 9.80665;
                p.DampingType = SpectrumDampingType.Cdamp; p.DampingFactor = .035; p.InterpolationType = SpectrumInterpolationType.Linear;
                p.ConsiderMissingMass = true; p.MissingMassFactor = .75; p.ConsiderZPA = true; p.ZPAFactor = 31;
                p.ConsiderFf1 = true; p.Ff1Factor = 4.5; p.ConsiderFf2 = true; p.Ff2Factor = 19; p.SaveFlag = true;
                wrapper.Load.AddRSLoad(p);
                var a = native.Arguments.Single();
                Assert.That(a.Length, Is.EqualTo(19)); Assert.That(a[0], Is.EqualTo(2));
                Assert.That(a[1], Is.TypeOf<float[]>().And.EqualTo(new[] { (float)-1.123456789123, .5f, 2f }));
                Assert.That(a.Skip(2).Take(15), Is.EqualTo(new object[] { 0, (float)9.80665, 2, (float).035, 0, 1, .75f, 1, 31f, 1, 4.5f, 1, 19f, 1, 2 }));
                Assert.That(a[17], Is.TypeOf<float[]>().And.EqualTo(new[] { .125f, 1.25f }));
                Assert.That(a[18], Is.TypeOf<float[]>().And.EqualTo(new[] { .25f, -.5f }));
                ((float[])a[1])[0] = 99; ((float[])a[17])[0] = 99; ((float[])a[18])[0] = 99;
                Assert.That(p.DirectionFactors[0], Is.EqualTo(-1.123456789123));
                Assert.That(p.SpectrumPoints[0].Period, Is.EqualTo(.125)); Assert.That(p.SpectrumPoints[0].Value, Is.EqualTo(.25));
                Assert.That(native.Threads.Single(), Is.EqualTo(Thread.CurrentThread.ManagedThreadId));
            }
        }

        private static IEnumerable<TestCaseData> InvalidSingleInputs()
        {
            foreach (string field in new[] { "Scale", "DampingFactor", "MissingMassFactor", "ZPAFactor", "Ff1Factor", "Ff2Factor", "DirectionFactors", "Period", "Value" })
                foreach (double value in new[] { double.NaN, double.PositiveInfinity, double.NegativeInfinity, double.MaxValue, double.Epsilon })
                    yield return new TestCaseData(field, value);
        }
        [TestCaseSource(nameof(InvalidSingleInputs))]
        public void LegacySingleConversionRejectsNonfiniteOverflowAndNonzeroUnderflowBeforeMutation(string field, double value)
        {
            var native = new Native(); var p = Legacy();
            if (field == "DirectionFactors") p.DirectionFactors[1] = value;
            else if (field == "Period") p.SpectrumPoints[1].Period = value;
            else if (field == "Value") p.SpectrumPoints[1].Value = value;
            else typeof(ResponseSpectrumLoadParameters).GetProperty(field).SetValue(p, value);
            using (var wrapper = Wrap(native)) Assert.Catch<ArgumentException>(() => wrapper.Load.AddRSLoad(p));
            Assert.That(native.Calls, Is.Empty);
        }

        [Test]
        public void LegacySingleBoundariesAndExplicitZeroRemainRepresentableWithoutAlteringCallerValues()
        {
            var native = new Native(); var p = Legacy();
            p.Scale = float.MaxValue; p.DirectionFactors = new[] { (double)float.Epsilon, 0.0, -0.0 };
            p.SpectrumPoints[0] = new SpectrumCurvePoint(0, -float.MaxValue);
            using (var wrapper = Wrap(native)) Assert.That(wrapper.Load.AddRSLoad(p), Is.True);
            Assert.That(native.Arguments.Single()[1], Is.EqualTo(new[] { float.Epsilon, 0f, -0f }));
            Assert.That(native.Arguments.Single()[3], Is.EqualTo(float.MaxValue));
            Assert.That(native.Arguments.Single()[18], Is.EqualTo(new[] { -float.MaxValue, -.5f }));
        }

        private static bool Invoke(IOSLoad load, string operation)
        {
            switch (operation)
            {
                case "member": return load.AddSeismicDefMemberUniformWeight(1, new[] { 17 });
                case "extended": return load.AddResponseSpectrumLoadEx(Extended());
                case "legacy": return load.AddRSLoad(Legacy());
                case "delete-type": return load.DeleteDirectAnalysisDefinitionParameter(DirectAnalysisParameterType.Flex);
                case "delete-all": return load.DeleteDirectAnalysisDefinition();
                default: return load.AddDirectAnalysisFlexParameter(.8, new[] { 17 });
            }
        }
        [TestCase("member")] [TestCase("extended")] [TestCase("legacy")] [TestCase("direct")] [TestCase("delete-type")] [TestCase("delete-all")]
        public void NativeBooleanResultsAreCheckedWithoutTreatingUnknownFailureCodesAsTrue(string operation)
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                foreach (object value in new object[] { true, false, (short)0, 1, (long)-1 })
                { native.Result = value; Assert.That(Invoke(wrapper.Load, operation), Is.EqualTo(Convert.ToBoolean(value))); }
                foreach (object value in new object[] { null, 2, -100, "true", 1.0, 0m, DBNull.Value, (long)int.MaxValue + 1, DirectAnalysisParameterType.Flex })
                { native.Result = value; Assert.Throws<InvalidOperationException>(() => Invoke(wrapper.Load, operation)); }
            }
        }

        [Test]
        public void WallStatusesAreIntegerResultsAndNeverCoercedToBoolean()
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                foreach (object value in new object[] { 0, (short)-1, -107, -8034, -8038, -9999, (long)17 })
                { native.WallStatus = value; Assert.That(wrapper.Load.AddSeismicDefWallArea(15, "Z", new[] { 4.0, .25 }), Is.EqualTo(Convert.ToInt32(value))); }
                foreach (object value in new object[] { null, true, false, "0", 0.0, 0m, DBNull.Value, (long)int.MaxValue + 1 })
                { native.WallStatus = value; Assert.Throws<InvalidOperationException>(() => wrapper.Load.AddSeismicDefWallArea(15, "Z", new[] { 4.0, .25 })); }
            }
        }

        private static IEnumerable<TestCaseData> InvalidRequests()
        {
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefMemberWeight(0, SeismicMemberLoadType.Uniform, 1, 0, 0, (IEnumerable<Beam>)null)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefMemberWeight(0, SeismicMemberLoadType.Uniform, 1, 0, 0, new Beam[] { null })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefMemberUniformWeight(double.NaN, new[] { 17 })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefMemberConcentratedWeight(1, -1, new[] { 17 })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefMemberConcentratedWeight(1, 0, new[] { 17, 0 })));
            foreach (int code in new[] { -1, 0, 12, 18, 26 })
                yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefWallArea(code, "X", new[] { 4.0, .25 })));
            foreach (string direction in new[] { null, "", "Y", "X Z", "X\0" })
                yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefWallArea(15, direction, new[] { 4.0, .25 })));
            foreach (double[] dimensions in new[] { null, new[] { 4.0 }, new[] { 4.0, 0 }, new[] { 4.0, -1 }, new[] { 4.0, double.NaN }, new[] { double.PositiveInfinity, .25 } })
                yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefWallArea(15, "X", dimensions)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefWallArea(15, (SeismicWallDirection)2, new[] { new SeismicWallDimension(4, .25) })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefWallArea(15, SeismicWallDirection.X, null)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefWallArea(15, SeismicWallDirection.X, new SeismicWallDimension[] { null })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddResponseSpectrumLoadEx(null)));
            foreach (int code in new[] { -1, 3, 9, 15 })
                yield return new TestCaseData(new Action<IOSLoad>(l => l.AddResponseSpectrumLoadEx(code, 0, new[] { "X" }, new[] { 1.0 })));
            foreach (int combination in new[] { -1, 7 })
                yield return new TestCaseData(new Action<IOSLoad>(l => l.AddResponseSpectrumLoadEx(0, combination, new[] { "X" }, new[] { 1.0 })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddResponseSpectrumLoadEx(0, 0, null, new[] { 1.0 })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddResponseSpectrumLoadEx(0, 0, new[] { "X" }, null)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddResponseSpectrumLoadEx(0, 0, new string[0], new double[0])));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddResponseSpectrumLoadEx(0, 0, new[] { "X", "Z" }, new[] { 1.0 })));
            foreach (string keyword in new[] { null, "", "x", "X ", "1X", "X\0" })
                yield return new TestCaseData(new Action<IOSLoad>(l => l.AddResponseSpectrumLoadEx(0, 0, new[] { keyword }, new[] { 1.0 })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddResponseSpectrumLoadEx(0, 0, new[] { "X", "X" }, new[] { 1.0, 2 })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddResponseSpectrumLoadEx(0, 0, new[] { "X" }, new[] { double.NaN })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddResponseSpectrumLoadEx(0, 0, new[] { "X" }, new[] { 1.0 }, new[] { "SOI" }, null)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddResponseSpectrumLoadEx(0, 0, new[] { "X" }, new[] { 1.0 }, null, new double[0])));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddResponseSpectrumLoadEx(0, 0, new[] { "X" }, new[] { 1.0 }, new[] { "SOI" }, new double[0])));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddResponseSpectrumLoadEx(0, 0, new[] { "X" }, new[] { 1.0 }, new[] { "SOI", "SOI" }, new[] { 1.0, 2 })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddResponseSpectrumLoadEx(0, 0, new[] { "X" }, new[] { 1.0 }, new[] { "SOI" }, new[] { double.PositiveInfinity })));
            foreach (double[] pairs in new[] { new[] { .1 }, new[] { -1.0, .2 }, new[] { .1, double.NaN } })
                yield return new TestCaseData(new Action<IOSLoad>(l => l.AddResponseSpectrumLoadEx(0, 0, new[] { "X" }, new[] { 1.0 }, customDataPairs: pairs)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddResponseSpectrumLoadEx(0, 0, new[] { "X" }, new[] { 1.0 }, new[] { "SOI" }, new[] { 2.0 }, new[] { .1, .2 })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddDirectAnalysisDefinitionParameter((DirectAnalysisParameters)null)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType.Flex, (IEnumerable<int>)null)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType.Flex, (IEnumerable<Beam>)null)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType.Flex, new Beam[] { null })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType.Flex, new[] { Beam(0) })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddDirectAnalysisDefinitionParameter((DirectAnalysisParameterType)1, new[] { 17 })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType.Axial, new[] { 17 }, .8)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType.Flex, new[] { 17, 0 }, .8)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddDirectAnalysisFlexParameter(double.NaN, new[] { 17 })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.DeleteDirectAnalysisDefinitionParameter((DirectAnalysisParameterType)1)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddRSLoad(null)));
        }
        [TestCaseSource(nameof(InvalidRequests))]
        public void InvalidRequestsMakeNoNativeCalls(Action<IOSLoad> operation)
        {
            var native = new Native();
            using (var wrapper = Wrap(native)) Assert.Catch<ArgumentException>(() => operation(wrapper.Load));
            Assert.That(native.Calls, Is.Empty);
        }


        [Test]
        public void TypedSpectrumBuildersRetainSourceUpdateRulesAndRejectInvalidInputsBeforeChanges()
        {
            var p = Extended(); double previous = p.PrimaryParameters["X"];
            Assert.That(p.WithDirection(x: 0, y: 1e-10), Is.SameAs(p));
            Assert.That(p.PrimaryParameters["X"], Is.EqualTo(previous)); Assert.That(p.PrimaryParameters.ContainsKey("Y"), Is.False);
            Assert.Throws<ArgumentOutOfRangeException>(() => p.WithDirection(x: 2, y: double.NaN));
            Assert.That(p.PrimaryParameters["X"], Is.EqualTo(previous));
            Assert.Throws<ArgumentOutOfRangeException>(() => p.WithDamping(double.NaN));
            Assert.Throws<ArgumentOutOfRangeException>(() => p.WithScale(double.PositiveInfinity));
            p.PrimaryParameters = null;
            Assert.Throws<ArgumentNullException>(() => p.WithDirection(1));
            Assert.Throws<ArgumentNullException>(() => p.WithDamping(.05));
            Assert.Throws<ArgumentNullException>(() => p.WithScale(1));
        }

        private static IEnumerable<TestCaseData> InvalidTypedSpectrum()
        {
            yield return new TestCaseData(new Action<ResponseSpectrumLoadExParameters>(p => p.Code = (ResponseSpectrumCode)3));
            yield return new TestCaseData(new Action<ResponseSpectrumLoadExParameters>(p => p.CombinationMethod = (ModalCombinationMethod)7));
            yield return new TestCaseData(new Action<ResponseSpectrumLoadExParameters>(p => p.PrimaryParameters = null));
            yield return new TestCaseData(new Action<ResponseSpectrumLoadExParameters>(p => p.PrimaryParameters.Clear()));
            yield return new TestCaseData(new Action<ResponseSpectrumLoadExParameters>(p => p.PrimaryParameters["X"] = double.NaN));
            yield return new TestCaseData(new Action<ResponseSpectrumLoadExParameters>(p => p.PrimaryParameters["x "] = 1));
            yield return new TestCaseData(new Action<ResponseSpectrumLoadExParameters>(p => p.CustomSpectrumData.Add(null)));
            yield return new TestCaseData(new Action<ResponseSpectrumLoadExParameters>(p => p.CustomSpectrumData.Add(new SpectrumCurvePoint(-1, .5))));
            yield return new TestCaseData(new Action<ResponseSpectrumLoadExParameters>(p => p.CustomSpectrumData.Add(new SpectrumCurvePoint(.1, double.PositiveInfinity))));
            yield return new TestCaseData(new Action<ResponseSpectrumLoadExParameters>(p => { p.SpectrumGenerationParameters["SOI"] = 2; p.CustomSpectrumData.Add(new SpectrumCurvePoint(.1, .2)); }));
        }
        [TestCaseSource(nameof(InvalidTypedSpectrum))]
        public void TypedExtendedValidationMatchesAdapterChecksAndMakesNoMutation(Action<ResponseSpectrumLoadExParameters> change)
        {
            var p = Extended(); change(p); var native = new Native();
            Assert.Catch<ArgumentException>(() => p.Validate());
            using (var wrapper = Wrap(native)) Assert.Catch<ArgumentException>(() => wrapper.Load.AddResponseSpectrumLoadEx(p));
            Assert.That(native.Calls, Is.Empty);
        }

        private static IEnumerable<TestCaseData> InvalidLegacyStructure()
        {
            yield return new TestCaseData(new Action<ResponseSpectrumLoadParameters>(p => p.LoadType = (ResponseSpectrumLoadType)0));
            yield return new TestCaseData(new Action<ResponseSpectrumLoadParameters>(p => p.QuantityType = (SpectrumQuantityType)2));
            yield return new TestCaseData(new Action<ResponseSpectrumLoadParameters>(p => p.DampingType = (SpectrumDampingType)0));
            yield return new TestCaseData(new Action<ResponseSpectrumLoadParameters>(p => p.InterpolationType = (SpectrumInterpolationType)2));
            yield return new TestCaseData(new Action<ResponseSpectrumLoadParameters>(p => p.DirectionFactors = null));
            yield return new TestCaseData(new Action<ResponseSpectrumLoadParameters>(p => p.DirectionFactors = new[] { 1.0, 0 } ));
            yield return new TestCaseData(new Action<ResponseSpectrumLoadParameters>(p => p.DirectionFactors = new[] { 1.0, 0, 0, 0 } ));
            yield return new TestCaseData(new Action<ResponseSpectrumLoadParameters>(p => p.SpectrumPoints = null));
            yield return new TestCaseData(new Action<ResponseSpectrumLoadParameters>(p => p.SpectrumPoints.Clear()));
            yield return new TestCaseData(new Action<ResponseSpectrumLoadParameters>(p => p.SpectrumPoints.Add(null)));
            yield return new TestCaseData(new Action<ResponseSpectrumLoadParameters>(p => p.SpectrumPoints[0].Period = -1));
        }
        [TestCaseSource(nameof(InvalidLegacyStructure))]
        public void LegacyShapeAndEnumValidationOccursBeforeAnyNativeInvocation(Action<ResponseSpectrumLoadParameters> change)
        {
            var p = Legacy(); change(p); var native = new Native();
            Assert.Catch<ArgumentException>(() => p.Validate());
            using (var wrapper = Wrap(native)) Assert.Catch<ArgumentException>(() => wrapper.Load.AddRSLoad(p));
            Assert.That(native.Calls, Is.Empty);
        }

        [Test]
        public void DirectModelValidationIncludesModeValueAndNonemptyPositiveMembers()
        {
            var p = DirectAnalysisParameters.CreateFlex(.8, new[] { 17 }); p.Validate();
            p.ParameterType = (DirectAnalysisParameterType)1;
            Assert.Throws<ArgumentOutOfRangeException>(() => p.Validate());
            p.ParameterType = DirectAnalysisParameterType.Axial;
            Assert.Throws<ArgumentException>(() => p.Validate());
            p.ParameterValue = 0; p.Validate();
            p.ParameterValue = double.NaN;
            Assert.Throws<ArgumentOutOfRangeException>(() => p.Validate());
            p.ParameterValue = 0; p.MemberIds = null;
            Assert.Throws<ArgumentNullException>(() => p.Validate());
            p.MemberIds = new List<int>();
            Assert.Throws<ArgumentException>(() => p.Validate());
            p.MemberIds.Add(0);
            Assert.Throws<ArgumentOutOfRangeException>(() => p.Validate());
        }

        private static IEnumerable<T> Broken<T>(T value)
        {
            yield return value;
            throw new InvalidOperationException("Enumeration failed.");
        }
        [Test]
        public void EnumerationFailuresAndLaterInvalidEntriesStopBeforeNativeMutation()
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.AddSeismicDefMemberWeight(0, SeismicMemberLoadType.Uniform, 1, 0, 0, Broken(Beam(17))));
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.AddSeismicDefWallArea(15, "X", Broken(4.0)));
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.AddSeismicDefWallArea(15, SeismicWallDirection.X, Broken(new SeismicWallDimension(4, .25))));
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType.Flex, Broken(17), .8));
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType.Flex, Broken(Beam(17)), .8));
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.AddSeismicDefWallArea(15, "X", new[] { 4.0, .25, 3, -1 }));
                Assert.That(native.Calls, Is.Empty);
            }
        }

        [Test]
        public void WallTransportExceptionsPropagateWithoutRetry()
        {
            var failure = new COMException("Wall failed", unchecked((int)0x80004005)); var native = new Native { Failure = failure };
            using (var wrapper = Wrap(native))
                Assert.That(Assert.Throws<COMException>(() => wrapper.Load.AddSeismicDefWallArea(15, "X", new[] { 4.0, .25 })), Is.SameAs(failure));
            Assert.That(native.Calls, Is.EqualTo(new[] { "wall" }));
        }

        [TestCase("member")] [TestCase("extended")] [TestCase("legacy")] [TestCase("direct")] [TestCase("delete-type")] [TestCase("delete-all")]
        public void NativeFailuresPropagateOnceWithoutRetryOrFallback(string operation)
        {
            var failure = new COMException("Native operation failed", unchecked((int)0x80004005));
            var native = new Native { Failure = failure };
            using (var wrapper = Wrap(native)) Assert.That(Assert.Throws<COMException>(() => Invoke(wrapper.Load, operation)), Is.SameAs(failure));
            Assert.That(native.Calls.Count, Is.EqualTo(1));
        }

        [TestCase("extended")] [TestCase("legacy")] [TestCase("direct")] [TestCase("delete-type")] [TestCase("delete-all")]
        public void MissingNativeMembersRemainVisible(string operation)
        {
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(new object())))
                Assert.Throws<RuntimeBinderException>(() => Invoke(wrapper.Load, operation));
        }
    }
}
