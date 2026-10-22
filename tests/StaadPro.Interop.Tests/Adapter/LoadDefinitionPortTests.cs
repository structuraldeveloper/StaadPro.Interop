using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Loads.Wind;
using StaadPro.Interop.Models.Loads.Seismic;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class LoadDefinitionPortTests
    {
        public class Native
        {
            public object Result = 0;
            public Exception Failure;
            public Action<object[]> OnCall;
            public readonly Queue<object> Results = new Queue<object>();
            public readonly List<string> Calls = new List<string>();
            public readonly List<object[]> Arguments = new List<object[]>();
            public readonly List<int> Threads = new List<int>();
            private object Record(string name, params object[] args)
            {
                Calls.Add(name); Arguments.Add(args); Threads.Add(Thread.CurrentThread.ManagedThreadId);
                OnCall?.Invoke(args);
                if (Failure != null) throw Failure;
                return Results.Count == 0 ? Result : Results.Dequeue();
            }
            public object AddWindDefinitionASCE7Parameters(int id, int code, double speed, double elevation,
                int category, int structure, int exposure, int escarpment, int wall, int flexible,
                double[] terrain, double[] geometry, int[] units, int[] flags, double[] factors)
                => Record("wind", id, code, speed, elevation, category, structure, exposure, escarpment, wall, flexible, terrain, geometry, units, flags, factors);
            public object ComputeWallWindPressureProfile(int code, double speed, int category, int structure, int exposure,
                int escarpment, int[] units, double[] terrain, double[] geometry, int wall)
                => Record("profile", code, speed, category, structure, exposure, escarpment, units, terrain, geometry, wall);
            public object ComputeWallWindPressureProfileASCE72016(double speed, double elevation, int category, int structure,
                int exposure, int escarpment, int[] units, double[] terrain, double[] geometry, int wall)
                => Record("profile2016", speed, elevation, category, structure, exposure, escarpment, units, terrain, geometry, wall);
            public object AddSeismicDefinition(int code, int torsion) => Record("seismic", code, torsion);
            public object ModifySeismicDefinitionParams(string keyword, double value) => Record("parameter", keyword, value);
            public object AddSeismicDefSelfWeight(double factor) => Record("self", factor);
            public object AddSeismicDefJointWeight(double weight, int[] nodes) => Record("joint", weight, nodes);
            public object AddSeismicDefMemberWeight(int code, int type, double weight, double start, double end, int[] members)
                => Record("member", code, type, weight, start, end, members);
        }
        public class VersionRoot : OSLoadQueryTests.QueryRoot
        {
            public VersionRoot(object load, string version) : base(load) { Version = version; }
            public string Version;
            public string GetApplicationVersion(ref object major, ref object update, ref object revision, ref object build) => Version;
        }
        public sealed class Once<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> items;
            public int Enumerations;
            public Once(IEnumerable<T> items) { this.items = items; }
            public IEnumerator<T> GetEnumerator()
            {
                if (++Enumerations != 1) throw new InvalidOperationException("Repeated enumeration");
                return items.GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        private static OpenStaadWrapper Wrap(Native native, string version = null) =>
            new OpenStaadWrapper(version == null ? (object)new OSLoadQueryTests.QueryRoot(native) : new VersionRoot(native, version));
        private static ASCE7BuildingData Building() => new ASCE7BuildingData
        { Height = 12, LengthAlongWindDirection = 20, WidthNormalToWindDirection = 8, NaturalFrequency = 1.25, DampingRatio = .02 };
        private static ASCE7WindDefinitionParameters Wind() => new ASCE7WindDefinitionParameters { StructureData = Building() };
        private static WallWindPressureProfileParameters Profile() => new WallWindPressureProfileParameters { StructureData = Building() };
        private static ASCE72016WallWindPressureProfileParameters Profile2016() => new ASCE72016WallWindPressureProfileParameters { StructureData = Building() };

        [Test]
        public void CompleteDefinitionUsesFifteenSlotsAndOwnedTypedArraysWithoutChangingCallerModels()
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                var p = Wind(); p.TypeNumber = 9; p.WindSpeed = 43.123456789123; p.HeightAboveSeaLevel = 15;
                p.ConsiderEscarpment = true; p.IsFlexible = true; p.WallType = ASCE7WallType.SideWall;
                p.ExposureCategory = ASCE7ExposureCategory.ExposureD; p.BuildingClassification = ASCE7BuildingClassification.CategoryIV;
                p.Units = WindUnitsConfiguration.CreateMetric();
                p.EscarpmentData = new WindEscarpmentData(EscarpmentTopographyType.TwoDimensionalEscarpment, 10, 20, -3);
                p.Factors.IsKzUserInput = true; p.Factors.IsGcpiUserInput = true; p.Factors.Kz = 1.23; p.Factors.Gcpi = -.18;
                Assert.That(wrapper.Load.AddWindDefinitionASCE7Parameters(p), Is.Zero);
                var a = native.Arguments.Single();
                Assert.That(a.Take(10), Is.EqualTo(new object[] { 9, 3, p.WindSpeed, 15.0, 3, 0, 3, 1, 2, 1 }));
                Assert.That(a[10], Is.EqualTo(new[] { 1.0, 10, 20, -3 }));
                Assert.That(a[11], Is.EqualTo(new[] { 3.0, 12, 20, 8, 1.25, .02, 0 }));
                Assert.That(a[12], Is.EqualTo(new[] { 1, 4, 4, 4, 4, 4, 4, 4 }));
                Assert.That(a[13], Is.EqualTo(new[] { 1, 0, 0, 0, 0, 0, 0, 1 }));
                Assert.That(a[14], Is.EqualTo(new[] { 1.23, 1, 1, .85, 1, .85, .8, -.18 }));
                ((double[])a[11])[1] = 999; ((int[])a[12])[0] = 99;
                Assert.That(((ASCE7BuildingData)p.StructureData).Height, Is.EqualTo(12));
                Assert.That(p.Units.WindSpeedUnit, Is.EqualTo(WindVelocityUnit.MetersPerSecond));
                Assert.That(native.Calls, Is.EqualTo(new[] { "wind" }));
                Assert.That(native.Threads.Single(), Is.EqualTo(Thread.CurrentThread.ManagedThreadId));
            }
        }

        [TestCase(false)] [TestCase(true)]
        public void ProfileCallsUseTenSlotsWin32BooleanFlagAndReturnCountWithoutInventingPressurePoints(bool modern)
        {
            var native = new Native { Result = 7 };
            using (var wrapper = Wrap(native))
            {
                int result;
                if (modern) { var p = Profile2016(); p.WindSpeed = 51.25; p.HeightAboveSeaLevel = 19; p.ConsiderEscarpment = true; result = wrapper.Load.ComputeWallWindPressureProfileASCE72016(p); }
                else { var p = Profile(); p.WindSpeed = 51.25; p.LoadingCode = ASCE7LoadingCode.ASCE7_2002; p.ConsiderEscarpment = true; result = wrapper.Load.ComputeWallWindPressureProfile(p); }
                Assert.That(result, Is.EqualTo(7));
                var a = native.Arguments.Single();
                Assert.That(a.Length, Is.EqualTo(10)); Assert.That(a[5], Is.TypeOf<int>().And.EqualTo(1));
                Assert.That(a[0], Is.EqualTo(modern ? (object)51.25 : 1));
                Assert.That(a[1], Is.EqualTo(modern ? (object)19.0 : 51.25));
                Assert.That(a[6], Is.TypeOf<int[]>()); Assert.That(((int[])a[6]).Length, Is.EqualTo(8));
                Assert.That(a[8], Is.EqualTo(new[] { 2.0, 12, 20, 8, 1.25, .02, 0 }));
            }
        }

        private static IEnumerable<TestCaseData> StructurePackets()
        {
            yield return new TestCaseData(new ASCE7BuildingData { Height = 12, LengthAlongWindDirection = 20, WidthNormalToWindDirection = 8 },
                new[] { 3.0, 12, 20, 8, 1, .01, 0 }, new[] { 2.0, 12, 20, 8, 1, .01, 0 });
            yield return new TestCaseData(new ASCE7TankData { Height = 12, LeastHorizontalDimension = 4 },
                new[] { 6.0, 12, 4, 0, 1, .01, 0 }, new[] { 3.0, 12, 4, 0, 1, .01, 0 });
            yield return new TestCaseData(new ASCE7SolidSignData { Height = 12, DimensionM = 4, DimensionN = 5 },
                new[] { 12.0, 4, 5, 1, .01, 0, 0 }, new[] { 12.0, 4, 5, 1, .01, 0, 0 });
            yield return new TestCaseData(new ASCE7OpenSignData { Height = 12, Width = 4, MemberDiameter = .1 },
                new[] { 0.0, 12, 4, .1, .3, 1, .01 }, new[] { 0.0, 12, 4, .1, 1, .01, .3 });
            yield return new TestCaseData(new ASCE7OpenSignData { StructureType = ASCE7StructureType.LatticeFramework, Height = 12, Width = 4, MemberDiameter = .1 },
                new[] { 0.0, 12, 4, .1, .3, 1, .01 }, new[] { 0.0, 12, 4, .1, 1, .01, .3 });
            yield return new TestCaseData(new ASCE7TrussedTowerData { Height = 12, Width = 4 },
                new[] { 1.0, 12, 4, 20, 1, .01, 0 }, new[] { 1.0, 12, 4, 1, .01, 20, 0 });
        }
        [TestCaseSource(nameof(StructurePackets))]
        public void EveryStructureFamilyUsesDocumentedDefinitionAndCalculationOrdering(IASCE7StructureData structure, double[] definition, double[] profile)
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                var d = Wind(); d.StructureData = structure; wrapper.Load.AddWindDefinitionASCE7Parameters(d);
                Assert.That(native.Arguments[0][11], Is.EqualTo(definition));
                var p = Profile(); p.StructureData = structure; wrapper.Load.ComputeWallWindPressureProfile(p);
                Assert.That(native.Arguments[1][8], Is.EqualTo(profile));
                var m = Profile2016(); m.StructureData = structure; wrapper.Load.ComputeWallWindPressureProfileASCE72016(m);
                Assert.That(native.Arguments[2][8], Is.EqualTo(profile));
            }
        }

        private static IEnumerable<TestCaseData> InvalidWindNumbers()
        {
            foreach (Type type in new[] { typeof(ASCE7BuildingData), typeof(ASCE7TankData), typeof(ASCE7SolidSignData),
                typeof(ASCE7OpenSignData), typeof(ASCE7TrussedTowerData), typeof(WindEscarpmentData), typeof(WindFactorsConfiguration),
                typeof(ASCE7WindDefinitionParameters), typeof(WallWindPressureProfileParameters), typeof(ASCE72016WallWindPressureProfileParameters) })
                foreach (var property in type.GetProperties().Where(p => p.PropertyType == typeof(double) && p.CanWrite))
                    foreach (double value in new[] { double.NaN, double.PositiveInfinity, double.NegativeInfinity })
                        yield return new TestCaseData(type, property.Name, value);
        }
        [TestCaseSource(nameof(InvalidWindNumbers))]
        public void EveryWindNumericFieldRejectsNonfiniteValuesBeforeDispatch(Type type, string property, double value)
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                object target; Action invoke;
                var p = Wind();
                if (type == typeof(ASCE7WindDefinitionParameters)) target = p;
                else if (type == typeof(WallWindPressureProfileParameters))
                { var profile = Profile(); target = profile; invoke = () => wrapper.Load.ComputeWallWindPressureProfile(profile); type.GetProperty(property).SetValue(target, value); Assert.Throws<ArgumentOutOfRangeException>(() => invoke()); Assert.That(native.Calls, Is.Empty); return; }
                else if (type == typeof(ASCE72016WallWindPressureProfileParameters))
                { var profile = Profile2016(); target = profile; invoke = () => wrapper.Load.ComputeWallWindPressureProfileASCE72016(profile); type.GetProperty(property).SetValue(target, value); Assert.Throws<ArgumentOutOfRangeException>(() => invoke()); Assert.That(native.Calls, Is.Empty); return; }
                else if (type == typeof(WindEscarpmentData)) target = p.EscarpmentData;
                else if (type == typeof(WindFactorsConfiguration)) target = p.Factors;
                else
                {
                    var model = (IASCE7StructureData)Activator.CreateInstance(type);
                    foreach (string dimension in new[] { "Height", "Width", "DimensionM", "DimensionN", "LeastHorizontalDimension", "LengthAlongWindDirection", "WidthNormalToWindDirection" })
                        type.GetProperty(dimension)?.SetValue(model, 10.0);
                    p.StructureData = model; target = model;
                }
                type.GetProperty(property).SetValue(target, value);
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.AddWindDefinitionASCE7Parameters(p));
                Assert.That(native.Calls, Is.Empty);
            }
        }

        private static int IntOperation(IOSLoad load, string kind)
        {
            switch (kind)
            {
                case "wind": return load.AddWindDefinitionASCE7Parameters(Wind());
                case "profile": return load.ComputeWallWindPressureProfile(Profile());
                case "profile2016": return load.ComputeWallWindPressureProfileASCE72016(Profile2016());
                case "parameter": return load.ModifySeismicDefinitionParam("ZONE", .16);
                default: return load.AddSeismicDefJointWeight(2.5, 101);
            }
        }
        [TestCase("wind")] [TestCase("profile")] [TestCase("profile2016")] [TestCase("parameter")] [TestCase("joint")]
        public void IntegerResultsPreserveFailuresAndRejectCoercion(string kind)
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                foreach (object code in new object[] { (short)-1, -8034, (long)0, 17, int.MinValue, int.MaxValue })
                { native.Result = code; Assert.That(IntOperation(wrapper.Load, kind), Is.EqualTo(Convert.ToInt32(code))); }
                foreach (object malformed in new object[] { null, true, false, 0.0, 0m, "0", DBNull.Value, (long)int.MaxValue + 1, SeismicCode.UBC1985 })
                { native.Result = malformed; Assert.Throws<InvalidOperationException>(() => IntOperation(wrapper.Load, kind)); }
            }
        }

        private static bool BoolOperation(IOSLoad load, string kind)
        {
            switch (kind)
            {
                case "definition": return load.AddSeismicDefinition(SeismicCode.IS1893_2016);
                case "self": return load.AddSeismicDefSelfWeight();
                default: return load.AddSeismicDefMemberWeight(0, SeismicMemberLoadType.Uniform, 2.5, 0, 0, new[] { 101 });
            }
        }
        [TestCase("definition")] [TestCase("self")] [TestCase("member")]
        public void BooleanFamiliesAcceptOnlyDocumentedBooleanShapes(string kind)
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                foreach (object valid in new object[] { true, false, (short)0, 1, (long)-1 })
                { native.Result = valid; Assert.That(BoolOperation(wrapper.Load, kind), Is.EqualTo(Convert.ToBoolean(valid))); }
                foreach (object malformed in new object[] { null, 2, -100, "true", 1.0, 0m, (long)int.MaxValue + 1, DBNull.Value })
                { native.Result = malformed; Assert.Throws<InvalidOperationException>(() => BoolOperation(wrapper.Load, kind)); }
            }
        }

        [TestCase("24.0.2.354", false)] [TestCase("25.0.1.424", true)] [TestCase("26.0.0.340", true)]
        [TestCase("26.0.0.341", false)] [TestCase("27.0.0.1", false)] [TestCase(null, false)]
        public void Nrc2020RequiresReviewedNativeVersionAndDoesNotUseHostVersion(string version, bool allowed)
        {
            var native = new Native { Result = true };
            using (var wrapper = Wrap(native, version))
            {
                if (allowed) { Assert.That(wrapper.Load.AddSeismicDefinition(SeismicCode.NRC2020, true), Is.True); Assert.That(native.Arguments[0], Is.EqualTo(new object[] { 25, 1 })); }
                else { Assert.Throws<NotSupportedException>(() => wrapper.Load.AddSeismicDefinition(25)); Assert.That(native.Calls, Is.Empty); }
            }
        }

        [TestCase("24.0.2.354", 25)] [TestCase("25.0.1.424", 26)] [TestCase("26.0.0.340", 26)]
        public void ExplicitMemberCodesUseAcquiredNativeTableWithoutCastingDefinitionEnum(string version, int maximum)
        {
            var native = new Native { Result = true };
            using (var wrapper = Wrap(native, version))
            {
                foreach (int code in Enumerable.Range(1, maximum))
                {
                    Assert.That(wrapper.Load.AddSeismicDefMemberWeight(code, SeismicMemberLoadType.Concentrated, -2.5, 1.25, 0, new[] { 101 }), Is.True);
                    Assert.That(native.Arguments.Last().Take(5), Is.EqualTo(new object[] { code, 2, -2.5, 1.25, 0.0 }));
                }
                int calls = native.Calls.Count;
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.AddSeismicDefMemberWeight(maximum + 1, SeismicMemberLoadType.Uniform, 1, 0, 0, new[] { 101 }));
                Assert.That(native.Calls.Count, Is.EqualTo(calls));
            }
        }

        [TestCase(null)] [TestCase("26.0.0.341")] [TestCase("27.0.0.1")]
        public void UnreviewedMemberCodeIsRejectedWhileExplicitAutoDetectionIsCommon(string version)
        {
            var native = new Native { Result = true };
            using (var wrapper = Wrap(native, version))
            {
                Assert.Throws<NotSupportedException>(() => wrapper.Load.AddSeismicDefMemberWeight(5, SeismicMemberLoadType.Uniform, 1, 0, 0, new[] { 101 }));
                Assert.That(native.Calls, Is.Empty);
                Assert.That(wrapper.Load.AddSeismicDefMemberWeight(0, SeismicMemberLoadType.Uniform, 1, 0, 0, new[] { 101 }), Is.True);
            }
        }

        [Test]
        public void CommonDefinitionCodesAndDefaultsArePreserved()
        {
            var native = new Native { Result = true };
            using (var wrapper = Wrap(native))
            {
                foreach (int code in Enumerable.Range(0, 25))
                { Assert.That(wrapper.Load.AddSeismicDefinition((SeismicCode)code), Is.True); Assert.That(native.Arguments.Last(), Is.EqualTo(new object[] { code, 0 })); }
                Assert.That(wrapper.Load.AddSeismicDefSelfWeight(), Is.True);
                Assert.That(native.Arguments.Last(), Is.EqualTo(new object[] { 1.0 }));
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.AddSeismicDefinition(-1));
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.AddSeismicDefinition((SeismicCode)26));
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.AddSeismicDefinition(12, 2));
            }
        }

        [Test]
        public void DictionaryValidatesEntireSnapshotBeforeMutationAndStopsAtFirstReportedFailure()
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                Assert.Throws<ArgumentException>(() => wrapper.Load.ModifySeismicDefinitionParams(new Dictionary<string, double> { { "ZONE", .16 }, { "bad keyword", 1 } }));
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.ModifySeismicDefinitionParams(new Dictionary<string, double> { { "ZONE", .16 }, { "I", double.NaN } }));
                Assert.That(native.Calls, Is.Empty);
                var values = new Dictionary<string, double> { { "ZONE", .16 }, { "I", 1.25 }, { "RF", 5 } };
                native.Results.Enqueue(0); native.Results.Enqueue(-9876);
                native.OnCall = _ => values.Clear();
                Assert.That(wrapper.Load.ModifySeismicDefinitionParams(values), Is.EqualTo(-9876));
                Assert.That(native.Arguments.Select(a => a[0]), Is.EqualTo(new[] { "ZONE", "I" }));
                Assert.That(native.Arguments.Select(a => a[1]), Is.EqualTo(new[] { .16, 1.25 }));
                Assert.That(wrapper.Load.ModifySeismicDefinitionParams(new Dictionary<string, double>()), Is.Zero);
                Assert.That(native.Calls.Count, Is.EqualTo(2));
            }
        }

        [Test]
        public void TypedSeismicModelsMapOptionalValuesWithoutConvertingOrChangingNativeDefinition()
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                var p = new IS1893_2016SeismicParameters { Height = 19, BaseDimensionX = 8, SpectralAcceleration = .125, GroundLevel = -3 };
                Assert.That(wrapper.Load.ModifySeismicDefinitionParams(p), Is.Zero);
                var dictionary = native.Arguments.ToDictionary(a => (string)a[0], a => (double)a[1]);
                Assert.That(dictionary, Is.EquivalentTo(p.ToParameterDictionary()));
                Assert.That(dictionary["GL"], Is.EqualTo(-3)); Assert.That(dictionary["HT"], Is.EqualTo(19));
                var copy = p.ToParameterDictionary(); copy["ZONE"] = 99; Assert.That(p.Zone, Is.EqualTo(.16));
                native.Calls.Clear(); native.Arguments.Clear();
                var old = new IS1893_2002SeismicParameters();
                wrapper.Load.ModifySeismicDefinitionParams(old);
                Assert.That(native.Arguments.ToDictionary(a => (string)a[0], a => (double)a[1]), Is.EquivalentTo(old.ToParameterDictionary()));
                native.Calls.Clear(); native.Arguments.Clear();
                var ibc = new IBCSeismicParameters { SS = .7, S1 = .2, CTX = .03, CTZ = .04, TL = 8, FA = 1, FV = 1.5, ExponentX = .75, ExponentZ = .8 };
                wrapper.Load.ModifySeismicDefinitionParams(ibc);
                Assert.That(native.Arguments.ToDictionary(a => (string)a[0], a => (double)a[1]), Is.EquivalentTo(ibc.ToParameterDictionary()));
                Assert.That(native.Calls.All(c => c == "parameter"), Is.True);
            }
        }

        [Test]
        public void JointAndMemberSelectionsSnapshotOnceRetainDuplicatesAndDoNotExposeCallerArrays()
        {
            var native = new Native { Result = 0 };
            using (var wrapper = Wrap(native))
            {
                var ids = new[] { 101, 101, 7 }; var once = new Once<int>(ids);
                Assert.That(wrapper.Load.AddSeismicDefJointWeight(-2.123456789123, once), Is.Zero);
                Assert.That(once.Enumerations, Is.EqualTo(1)); Assert.That(native.Arguments[0][1], Is.EqualTo(ids));
                ((int[])native.Arguments[0][1])[0] = 999; Assert.That(ids[0], Is.EqualTo(101));
                var nodes = new Once<Node>(new[] { new Node(101, 0, 0, 0), new Node(7, 0, 0, 0) });
                wrapper.Load.AddSeismicDefJointWeight(2, nodes); Assert.That(nodes.Enumerations, Is.EqualTo(1));
                wrapper.Load.AddSeismicDefJointWeight(3, 17); Assert.That(native.Arguments.Last()[1], Is.EqualTo(new[] { 17 }));
                native.Result = true; var members = new Once<int>(new[] { 101, 101, 7 });
                wrapper.Load.AddSeismicDefMemberWeight(0, SeismicMemberLoadType.Uniform, 2, 0, 0, members);
                Assert.That(members.Enumerations, Is.EqualTo(1)); Assert.That(native.Arguments.Last()[5], Is.EqualTo(new[] { 101, 101, 7 }));
                int count = native.Calls.Count;
                Assert.That(wrapper.Load.AddSeismicDefJointWeight(1, new int[0]), Is.Zero);
                Assert.That(wrapper.Load.AddSeismicDefMemberWeight(0, SeismicMemberLoadType.Uniform, 1, 0, 0, new int[0]), Is.True);
                Assert.That(native.Calls.Count, Is.EqualTo(count));
            }
        }

        private static IEnumerable<TestCaseData> InvalidRequests()
        {
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddWindDefinitionASCE7Parameters(null)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.ComputeWallWindPressureProfile(null)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.ComputeWallWindPressureProfileASCE72016(null)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.ModifySeismicDefinitionParams((IDictionary<string, double>)null)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.ModifySeismicDefinitionParams((IS1893_2016SeismicParameters)null)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.ModifySeismicDefinitionParams((IS1893_2002SeismicParameters)null)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.ModifySeismicDefinitionParams((IBCSeismicParameters)null)));
            foreach (string keyword in new[] { null, "", "ZONE ", "zone", "Z\0ONE", "É" })
                yield return new TestCaseData(new Action<IOSLoad>(l => l.ModifySeismicDefinitionParam(keyword, 1)));
            foreach (double value in new[] { double.NaN, double.PositiveInfinity, double.NegativeInfinity })
            {
                yield return new TestCaseData(new Action<IOSLoad>(l => l.ModifySeismicDefinitionParam("ZONE", value)));
                yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefSelfWeight(value)));
                yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefJointWeight(value, 101)));
                yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefMemberWeight(0, SeismicMemberLoadType.Uniform, value, 0, 0, new[] { 101 })));
            }
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefJointWeight(1, (IEnumerable<int>)null)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefJointWeight(1, (IEnumerable<Node>)null)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefJointWeight(1, new Node[] { null })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefJointWeight(1, 0)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefJointWeight(1, new[] { 101, -1 })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefMemberWeight(-1, SeismicMemberLoadType.Uniform, 1, 0, 0, new[] { 101 })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefMemberWeight(0, (SeismicMemberLoadType)0, 1, 0, 0, new[] { 101 })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefMemberWeight(0, SeismicMemberLoadType.Uniform, 1, -1, 0, new[] { 101 })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefMemberWeight(0, SeismicMemberLoadType.Uniform, 1, 3, 2, new[] { 101 })));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefMemberWeight(0, SeismicMemberLoadType.Uniform, 1, 0, 0, (IEnumerable<int>)null)));
            yield return new TestCaseData(new Action<IOSLoad>(l => l.AddSeismicDefMemberWeight(0, SeismicMemberLoadType.Uniform, 1, 0, 0, new[] { 101, 0 })));
        }
        [TestCaseSource(nameof(InvalidRequests))]
        public void InvalidInputStopsBeforeAnyNativeMutation(Action<IOSLoad> action)
        {
            var native = new Native();
            using (var wrapper = Wrap(native)) Assert.Catch<ArgumentException>(() => action(wrapper.Load));
            Assert.That(native.Calls, Is.Empty);
        }

        [TestCase("wind")] [TestCase("profile")] [TestCase("profile2016")] [TestCase("parameter")] [TestCase("joint")]
        public void NativeExceptionsPropagateWithoutRetry(string kind)
        {
            var failure = new COMException("Native definition failed", unchecked((int)0x80004005));
            var native = new Native { Failure = failure };
            using (var wrapper = Wrap(native)) Assert.That(Assert.Throws<COMException>(() => IntOperation(wrapper.Load, kind)), Is.SameAs(failure));
            Assert.That(native.Calls.Count, Is.EqualTo(1));
        }


        public sealed class CustomStructure : IASCE7StructureData
        {
            public ASCE7StructureType StructureType => ASCE7StructureType.Building;
            public double NaturalFrequency { get; set; } = 1;
            public double DampingRatio { get; set; } = .01;
            public double[] Packet;
            public void Validate() { }
            public double[] ToDoubleArray(ASCE7Publication publication) => Packet;
        }

        [Test]
        public void CustomStructurePacketsAreCheckedAndCopiedBeforeNativeUse()
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                var custom = new CustomStructure();
                var request = Wind(); request.StructureData = custom;
                Assert.Throws<ArgumentException>(() => wrapper.Load.AddWindDefinitionASCE7Parameters(request));
                custom.Packet = new double[6];
                Assert.Throws<ArgumentException>(() => wrapper.Load.AddWindDefinitionASCE7Parameters(request));
                custom.Packet = new[] { 0.0, 1, 1, 1, double.NaN, .01, 0 };
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.AddWindDefinitionASCE7Parameters(request));
                Assert.That(native.Calls, Is.Empty);
                custom.Packet = new[] { 0.0, 1, 1, 1, 1, .01, 0 };
                wrapper.Load.AddWindDefinitionASCE7Parameters(request);
                ((double[])native.Arguments.Single()[11])[1] = 9;
                Assert.That(custom.Packet[1], Is.EqualTo(1));
            }
        }

        [TestCase(ASCE7Publication.ASCE7_95)] [TestCase(ASCE7Publication.ASCE7_02)] [TestCase(ASCE7Publication.ASCE7_05_10)]
        public void PartiallyOpenBuildingsAreNotSilentlySubstitutedInLegacyPackets(ASCE7Publication publication)
        {
            var structure = Building(); structure.EnclosureClassification = BuildingEnclosureClassification.PartiallyOpen;
            Assert.Throws<NotSupportedException>(() => structure.ToDoubleArray(publication));
            Assert.That(structure.ToDoubleArray(ASCE7Publication.ASCE7_16)[0], Is.EqualTo(1));
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                var p = Profile2016(); p.StructureData = structure;
                Assert.Throws<NotSupportedException>(() => wrapper.Load.ComputeWallWindPressureProfileASCE72016(p));
                Assert.That(native.Calls, Is.Empty);
            }
        }

        private static IEnumerable<TestCaseData> InvalidWindRequests()
        {
            yield return new TestCaseData(new Action<ASCE7WindDefinitionParameters>(p => p.TypeNumber = 0));
            yield return new TestCaseData(new Action<ASCE7WindDefinitionParameters>(p => p.WindSpeed = 0));
            yield return new TestCaseData(new Action<ASCE7WindDefinitionParameters>(p => p.HeightAboveSeaLevel = -1));
            yield return new TestCaseData(new Action<ASCE7WindDefinitionParameters>(p => p.Publication = (ASCE7Publication)4));
            yield return new TestCaseData(new Action<ASCE7WindDefinitionParameters>(p => p.BuildingClassification = (ASCE7BuildingClassification)4));
            yield return new TestCaseData(new Action<ASCE7WindDefinitionParameters>(p => p.ExposureCategory = (ASCE7ExposureCategory)4));
            yield return new TestCaseData(new Action<ASCE7WindDefinitionParameters>(p => p.WallType = (ASCE7WallType)3));
            yield return new TestCaseData(new Action<ASCE7WindDefinitionParameters>(p => p.StructureData = null));
            yield return new TestCaseData(new Action<ASCE7WindDefinitionParameters>(p => p.EscarpmentData = null));
            yield return new TestCaseData(new Action<ASCE7WindDefinitionParameters>(p => p.Units = null));
            yield return new TestCaseData(new Action<ASCE7WindDefinitionParameters>(p => p.Factors = null));
            yield return new TestCaseData(new Action<ASCE7WindDefinitionParameters>(p => p.Units.WindSpeedUnit = (WindVelocityUnit)8));
            yield return new TestCaseData(new Action<ASCE7WindDefinitionParameters>(p => p.Units.StructureDimension2Unit = (WindLengthUnit)10));
            yield return new TestCaseData(new Action<ASCE7WindDefinitionParameters>(p => p.EscarpmentData.TopographyType = (EscarpmentTopographyType)3));
            yield return new TestCaseData(new Action<ASCE7WindDefinitionParameters>(p => p.EscarpmentData.Height = -1));
            yield return new TestCaseData(new Action<ASCE7WindDefinitionParameters>(p => p.EscarpmentData.DistanceUpwindOfCrest = -1));
            yield return new TestCaseData(new Action<ASCE7WindDefinitionParameters>(p => ((ASCE7BuildingData)p.StructureData).DampingRatio = 1.01));
            yield return new TestCaseData(new Action<ASCE7WindDefinitionParameters>(p => ((ASCE7BuildingData)p.StructureData).EnclosureClassification = (BuildingEnclosureClassification)4));
        }
        [TestCaseSource(nameof(InvalidWindRequests))]
        public void InvalidNestedWindFieldsStopBeforeDispatch(Action<ASCE7WindDefinitionParameters> change)
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                var request = Wind(); change(request);
                Assert.Catch<ArgumentException>(() => wrapper.Load.AddWindDefinitionASCE7Parameters(request));
                Assert.That(native.Calls, Is.Empty);
            }
        }

        private static IEnumerable<TestCaseData> InvalidSeismicModelNumbers()
        {
            foreach (Type type in new[] { typeof(IS1893_2016SeismicParameters), typeof(IS1893_2002SeismicParameters), typeof(IBCSeismicParameters) })
                foreach (var property in type.GetProperties().Where(p => p.PropertyType == typeof(double) || p.PropertyType == typeof(double?)))
                    yield return new TestCaseData(type, property.Name);
        }
        [TestCaseSource(nameof(InvalidSeismicModelNumbers))]
        public void EveryTypedSeismicScalarIsValidatedBeforeAnyUpdate(Type type, string property)
        {
            var model = Activator.CreateInstance(type);
            type.GetProperty(property).SetValue(model, double.NaN);
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                if (model is IS1893_2016SeismicParameters p) Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.ModifySeismicDefinitionParams(p));
                else if (model is IS1893_2002SeismicParameters old) Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.ModifySeismicDefinitionParams(old));
                else Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Load.ModifySeismicDefinitionParams((IBCSeismicParameters)model));
                Assert.That(native.Calls, Is.Empty);
            }
        }

        private static IEnumerable<int> BrokenIds()
        {
            yield return 101;
            throw new InvalidOperationException("Enumeration failed");
        }
        [Test]
        public void EnumerationFailuresOccurBeforeDispatchAndNumericKeywordSuffixesAreAccepted()
        {
            var native = new Native();
            using (var wrapper = Wrap(native))
            {
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.AddSeismicDefJointWeight(1, BrokenIds()));
                Assert.Throws<InvalidOperationException>(() => wrapper.Load.AddSeismicDefMemberWeight(0, SeismicMemberLoadType.Uniform, 1, 0, 0, BrokenIds()));
                Assert.That(native.Calls, Is.Empty);
                Assert.That(wrapper.Load.ModifySeismicDefinitionParam("SA1", .123456789123), Is.Zero);
                Assert.That(native.Arguments.Single(), Is.EqualTo(new object[] { "SA1", .123456789123 }));
            }
        }

        [Test]
        public void MissingNativeMembersRemainVisibleToCallers()
        {
            using (var wrapper = new OpenStaadWrapper(new OSLoadQueryTests.QueryRoot(new object())))
                Assert.Throws<RuntimeBinderException>(() => wrapper.Load.AddSeismicDefSelfWeight());
        }
    }
}
