using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.ContractReview;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Loads;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Loads.Contracts;
using StaadPro.Interop.Models.Loads.Queries;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class LoadExpansionContractTests
    {
        [Test]
        public void All56NativeEntriesHaveResolvableReviewOrExistingSignatures()
        {
            var index = JObject.Parse(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "ContractReview", "LoadExpansionContractIndex.json")));
            var rows = (JArray)index["nativeEntries"];
            Assert.That(rows.Count, Is.EqualTo(56));
            Assert.That(rows.Select(r => (string)r["nativeName"]).Distinct().Count(), Is.EqualTo(56));
            MethodInfo[] proposed = typeof(IOSLoadExpansionContract).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            Assert.That(proposed.Length, Is.EqualTo(36));
            foreach (JObject row in rows)
            {
                string name = (string)row["managedMethod"];
                Assert.That(proposed.Concat(typeof(IOSLoad).GetMethods()).Any(m => m.Name == name), Is.True, row.ToString());
                Assert.That((string)row["managedSignature"], Is.Not.Null.And.Not.Empty);
            }
            Assert.That(rows.Count(r => (string)r["source"] == "Public IOSLoad / internal services"), Is.EqualTo(39));
            Assert.That(rows.Count(r => ((string)r["state"]).StartsWith("Blocked:")), Is.EqualTo(0));
            foreach (var method in proposed)
            {
                var runtime = typeof(IOSLoad).GetMethod(method.Name);
                Assert.That(runtime, Is.Not.Null, "Approved contract was not promoted: " + method.Name);
                Assert.That(runtime.ReturnType, Is.EqualTo(method.ReturnType));
                Assert.That(method.GetParameters().Any(p => p.ParameterType.IsByRef), Is.False);
                AssertManagedType(method.ReturnType);
                foreach (var p in method.GetParameters()) AssertManagedType(p.ParameterType);
            }
            Assert.That(typeof(OpenStaadWrapper).Assembly.GetType(typeof(IOSLoadExpansionContract).FullName), Is.Null);
            Assert.That(typeof(IOSLoadExpansionContract).IsPublic, Is.False);
        }
        private static void AssertManagedType(Type type)
        {
            Assert.That(type, Is.Not.EqualTo(typeof(object)).And.Not.EqualTo(typeof(Array)));
            Assert.That(type.IsArray || type.IsPointer || type.IsByRef, Is.False, type.ToString());
            Assert.That(type.FullName, Does.Not.StartWith("System.ValueTuple").And.Not.StartWith("System.Tuple"));
            foreach (var argument in type.GetGenericArguments()) AssertManagedType(argument);
        }
        [Test]
        public void ReviewIndexMatchesEveryCompiledReturnParameterAndDefaultExactly()
        {
            var index = JObject.Parse(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "ContractReview", "LoadExpansionContractIndex.json")));
            foreach (JObject entry in (JArray)index["pendingSignatures"])
            {
                var method = typeof(IOSLoadExpansionContract).GetMethod((string)entry["name"]);
                Assert.That(FriendlyType(method.ReturnType), Is.EqualTo((string)entry["returnType"]), method.Name);
                var expected = (JArray)entry["parameters"];
                var parameters = method.GetParameters(); Assert.That(parameters.Length, Is.EqualTo(expected.Count));
                for (int i = 0; i < parameters.Length; i++)
                {
                    Assert.That(parameters[i].Name, Is.EqualTo((string)expected[i]["name"]));
                    Assert.That(FriendlyType(parameters[i].ParameterType), Is.EqualTo((string)expected[i]["type"]));
                    Assert.That(parameters[i].HasDefaultValue, Is.EqualTo(expected[i]["default"].Type != JTokenType.Null));
                    if (parameters[i].HasDefaultValue)
                        Assert.That(Convert.ToString(parameters[i].DefaultValue, System.Globalization.CultureInfo.InvariantCulture), Is.EqualTo((string)expected[i]["default"]));
                    Assert.That((string)expected[i]["description"], Is.Not.Null.And.Not.Empty);
                }
            }
        }
        private static string FriendlyType(Type type)
        {
            if (type == typeof(int)) return "int";
            if (type == typeof(double)) return "double";
            if (type == typeof(bool)) return "bool";
            if (type == typeof(string)) return "string";
            return type.IsGenericType ? type.Name.Split('`')[0] + "<" + string.Join(", ", type.GetGenericArguments().Select(FriendlyType)) + ">" : type.Name;
        }
        [Test]
        public void RequestAndPayloadTypesHaveExpandedXmlAndNamedProperties()
        {
            var assembly = typeof(OpenStaadWrapper).Assembly;
            var docs = XDocument.Load(Path.ChangeExtension(assembly.Location, ".xml")).Descendants("member").ToDictionary(m => (string)m.Attribute("name"));
            var types = assembly.GetExportedTypes().Where(t => t.Namespace == typeof(AutomaticCombinationRequest).Namespace ||
                t == typeof(GlobalAxis) || t == typeof(LoadStrengthType) || t == typeof(PlatePressureVariation) || t == typeof(WindLoadDirection) ||
                t == typeof(AutomaticNotionalDirections) || t == typeof(LoadOperationStatus)).ToArray();
            Assert.That(types.Length, Is.GreaterThan(20));
            foreach (var type in types)
            {
                string name = type.FullName;
                Assert.That(docs["T:" + name].Element("summary")?.Value.Trim(), Is.Not.Null.And.Not.Empty, name);
                foreach (var p in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    Assert.That(docs["P:" + name + "." + p.Name].Element("summary")?.Value.Trim(), Is.Not.Null.And.Not.Empty);
                    Assert.That(p.CanWrite, Is.False, p.Name);
                    AssertManagedType(p.PropertyType);
                }
                if (type.IsEnum)
                    foreach (var field in Enum.GetNames(type)) Assert.That(docs["F:" + name + "." + field].Element("summary")?.Value.Trim(), Is.Not.Null.And.Not.Empty);
            }
        }
        [Test]
        public void SelectionSnapshotsExactlyOnceAndNeverTreatsEmptyAsAll()
        {
            int enumerations = 0;
            IEnumerable<int> Source() { enumerations++; yield return 41; yield return 17; yield return 41; }
            var selected = LoadCaseSelection.SpecifiedCases(Source());
            Assert.That(enumerations, Is.EqualTo(1));
            Assert.That(selected.LoadCaseIds, Is.EqualTo(new[] { 41, 17, 41 }));
            selected.LoadCaseIds.ToArray(); Assert.That(enumerations, Is.EqualTo(1));
            Assert.That(selected.IncludesAllCases, Is.False);
            Assert.That(LoadCaseSelection.AllCases().IncludesAllCases, Is.True);
            Assert.That(LoadCaseSelection.AllCases().LoadCaseIds, Is.Empty);
            Assert.Throws<ArgumentException>(() => LoadCaseSelection.SpecifiedCases(new int[0]));
            Assert.Throws<ArgumentNullException>(() => LoadCaseSelection.SpecifiedCases(null));
            Assert.Throws<ArgumentOutOfRangeException>(() => LoadCaseSelection.SpecifiedCases(new[] { 1, 0 }));
            var source = new[] { 17, 41 }; var copy = LoadCaseSelection.SpecifiedCases(source); source[0] = 999;
            Assert.That(copy.LoadCaseIds.First(), Is.EqualTo(17));
        }
        [Test]
        public void AutomaticRequestsRetainRuleNamesAndExplicitGenerationChoices()
        {
            var request = new AutomaticCombinationRequest(" AISC 9th Ed ", "2.3 LRFD General", LoadCaseSelection.AllCases(), 100);
            var options = new AutomaticNotionalOptions(AutomaticNotionalDirections.PositiveX | AutomaticNotionalDirections.NegativeZ, -0.002, true, 4);
            var repeat = new AutomaticRepeatLoadRequest(request, true, options);
            Assert.That(request.CodeName, Is.EqualTo(" AISC 9th Ed "));
            Assert.That(repeat.Generation, Is.SameAs(request)); Assert.That(repeat.IncludeReferenceLoads, Is.True);
            Assert.That(repeat.NotionalOptions.FloorCount, Is.EqualTo(4));
            Assert.That(new AutomaticRepeatLoadRequest(request).NotionalOptions, Is.Null);
            Assert.Throws<ArgumentException>(() => new AutomaticCombinationRequest(" ", "category", LoadCaseSelection.AllCases(), 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new AutomaticCombinationRequest("code", "category", LoadCaseSelection.AllCases(), 0));
            Assert.Throws<ArgumentNullException>(() => new AutomaticCombinationRequest("code", "category", null, 1));
            Assert.Throws<ArgumentNullException>(() => new AutomaticRepeatLoadRequest(null));
        }
        [TestCase(AutomaticNotionalDirections.None)] [TestCase((AutomaticNotionalDirections)16)] [TestCase((AutomaticNotionalDirections)(-1))]
        public void AutomaticDirectionsMustBeExplicitAndDefined(AutomaticNotionalDirections directions) =>
            Assert.Throws<ArgumentOutOfRangeException>(() => new AutomaticNotionalOptions(directions, 1));
        [Test]
        public void GbFloorCountsCannotLeakIntoAnInapplicableMode()
        {
            Assert.Throws<ArgumentException>(() => new AutomaticNotionalOptions(AutomaticNotionalDirections.PositiveX, 1, true));
            Assert.Throws<ArgumentException>(() => new AutomaticNotionalOptions(AutomaticNotionalDirections.PositiveX, 1, false, 2));
            Assert.Throws<ArgumentOutOfRangeException>(() => new AutomaticNotionalOptions(AutomaticNotionalDirections.PositiveX, 1, true, 0));
        }
        [TestCase(double.NaN)] [TestCase(double.PositiveInfinity)] [TestCase(double.NegativeInfinity)]
        public void RequestQuantitiesRejectNonfiniteValues(double value)
        {
            var region = new GlobalLoadRegion(0, 1, 0, 1, 0, 1);
            Assert.Throws<ArgumentOutOfRangeException>(() => new GlobalLoadRegion(value, 1, 0, 1, 0, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new AutomaticNotionalOptions(AutomaticNotionalDirections.PositiveX, value));
            Assert.Throws<ArgumentOutOfRangeException>(() => new HydrostaticPressureDefinition(LoadDirection.LocalZ, GlobalAxis.Y, value, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => TrapezoidalPressureDefinition.AtCorners(LoadDirection.GlobalX, 1, 2, 3, value));
            Assert.Throws<ArgumentOutOfRangeException>(() => FloorLoadDefinition.ForRange(value, GlobalAxis.Y, new FloorRegion(region, GlobalAxis.Y)));
            Assert.Throws<ArgumentOutOfRangeException>(() => new WindLoadDefinition(1, WindLoadDirection.PositiveX, value, false, region));
            Assert.Throws<ArgumentOutOfRangeException>(() => new FixedEndLoadDefinition(new Forces(value, 0, 0, 0, 0, 0), new Forces()));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NotionalLoadContribution(17, LoadCaseType.PrimaryLoad, value, NotionalLoadDirection.GlobalX));
        }
        [Test]
        public void RegionsPreserveAxisBoundsWithoutAssumingYUp()
        {
            var bounds = new GlobalLoadRegion(-2, 3, 0, 0, -4, 8);
            var region = new FloorRegion(bounds, GlobalAxis.Z);
            Assert.That(region.Bounds, Is.SameAs(bounds)); Assert.That(region.RangeAxis, Is.EqualTo(GlobalAxis.Z));
            Assert.That(bounds.MinimumY, Is.Zero); Assert.That(bounds.MaximumY, Is.Zero);
            Assert.Throws<ArgumentException>(() => new GlobalLoadRegion(2, 1, 0, 1, 0, 1));
            Assert.Throws<ArgumentNullException>(() => new FloorRegion(null, GlobalAxis.X));
            Assert.Throws<ArgumentOutOfRangeException>(() => new FloorRegion(bounds, (GlobalAxis)3));
        }
        [Test]
        public void FixedEndAndNodalObservationsReuseForcesWithDefensiveCopies()
        {
            var source = new Forces(1, 2, 3, 4, 5, 6);
            var definition = new FixedEndLoadDefinition(source, new Forces(-1, -2, -3, -4, -5, -6));
            var observation = new NodalLoadCommandInfo(3, source);
            source.Fx = 999; definition.StartForces.Fy = 999; observation.Forces.Mz = 999;
            Assert.That(definition.StartForces.Fx, Is.EqualTo(1)); Assert.That(definition.StartForces.Fy, Is.EqualTo(2));
            Assert.That(definition.EndForces.Mz, Is.EqualTo(-6)); Assert.That(observation.Forces.Mz, Is.EqualTo(6));
            Assert.Throws<ArgumentNullException>(() => new FixedEndLoadDefinition(null, source));
        }
        [Test]
        public void PlatePressureFactoriesPreventMixedModesAndSilentDirectionFallback()
        {
            var line = TrapezoidalPressureDefinition.AlongLocalAxis(LoadDirection.LocalZ, PlatePressureVariation.LocalY, -2, -1);
            Assert.That(line.ThirdPressure, Is.Null); Assert.That(line.FourthPressure, Is.Null);
            var corners = TrapezoidalPressureDefinition.AtCorners(LoadDirection.GlobalZ, 1, 2, 3, 4);
            Assert.That(corners.Variation, Is.EqualTo(PlatePressureVariation.Joints)); Assert.That(corners.FourthPressure, Is.EqualTo(4));
            Assert.Throws<ArgumentOutOfRangeException>(() => TrapezoidalPressureDefinition.AlongLocalAxis(LoadDirection.LocalZ, PlatePressureVariation.Joints, 1, 2));
            Assert.Throws<ArgumentOutOfRangeException>(() => TrapezoidalPressureDefinition.AtCorners(LoadDirection.LocalX, 1, 2, 3, 4));
            var hydro = new HydrostaticPressureDefinition(LoadDirection.GlobalY, GlobalAxis.Z, -2, -8);
            Assert.That(hydro.MinimumPressure, Is.EqualTo(-2)); Assert.That(hydro.MaximumPressure, Is.EqualTo(-8));
        }
        [Test]
        public void FloorModesDoNotMixGroupsRangesAndOneWayMarkers()
        {
            var region = new FloorRegion(new GlobalLoadRegion(0, 2, 1, 1, 0, 3), GlobalAxis.Y);
            var range = FloorLoadDefinition.ForRange(-5, GlobalAxis.Z, region, true);
            Assert.That(range.IsOneWay, Is.True); Assert.That(range.UsesFloorGroup, Is.False); Assert.That(range.FloorGroupName, Is.Null);
            var group = FloorLoadDefinition.ForGroup(-5, GlobalAxis.X, "_FLOOR1");
            Assert.That(group.UsesFloorGroup, Is.True); Assert.That(group.Region, Is.Null); Assert.That(group.IsOneWay, Is.False);
            Assert.Throws<ArgumentException>(() => FloorLoadDefinition.ForGroup(-5, GlobalAxis.Y, "0"));
            Assert.Throws<ArgumentException>(() => FloorLoadDefinition.ForGroup(-5, GlobalAxis.Y, " "));
            Assert.Throws<ArgumentNullException>(() => FloorLoadDefinition.ForRange(-5, GlobalAxis.Y, null));
        }
        [TestCase(WindLoadDirection.PositiveX, 1)] [TestCase(WindLoadDirection.PositiveZ, 3)]
        [TestCase(WindLoadDirection.NegativeX, 4)] [TestCase(WindLoadDirection.NegativeZ, 6)]
        public void WindDirectionCodesAreSeparateFromLoadAndGlobalAxisEnums(WindLoadDirection direction, int code)
        {
            var request = new WindLoadDefinition(7, direction, -1, true, new GlobalLoadRegion(0, 1, 0, 2, 0, 3));
            Assert.That((int)request.Direction, Is.EqualTo(code)); Assert.That(request.Factor, Is.EqualTo(-1));
            Assert.That(request.WindDefinitionId, Is.EqualTo(7));
        }
        [Test]
        public void BeamMergeRequestRequiresAnExplicitDistinctPair()
        {
            var request = new BeamLoadMergeRequest(101, 102);
            Assert.That(request.RetainedBeamId, Is.EqualTo(101)); Assert.That(request.SourceBeamId, Is.EqualTo(102));
            Assert.Throws<ArgumentException>(() => new BeamLoadMergeRequest(101, 101));
            Assert.Throws<ArgumentOutOfRangeException>(() => new BeamLoadMergeRequest(101, 0));
        }
        [TestCase(LoadCaseType.PrimaryLoad, 17)] [TestCase(LoadCaseType.ReferenceLoad, -17)]
        public void NotionalInputReusesTheExistingOutputModelWithExplicitFamily(LoadCaseType family, int signed)
        {
            var contribution = new NotionalLoadContribution(17, family, 0, NotionalLoadDirection.GlobalY);
            Assert.That(contribution.NativeLoadCaseId, Is.EqualTo(signed)); Assert.That(contribution.LoadCaseId, Is.EqualTo(17));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NotionalLoadContribution(-17, family, 1, NotionalLoadDirection.GlobalX));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NotionalLoadContribution(17, LoadCaseType.LoadCombination, 1, NotionalLoadDirection.GlobalX));
        }
        [TestCase(LoadOperationStatus.NativeFailure)] [TestCase(LoadOperationStatus.PartialCompletion)]
        [TestCase(LoadOperationStatus.UnverifiedContract)] [TestCase(LoadOperationStatus.InvocationCompleted)]
        public void FailedUnverifiedOrVoidEvidenceNeverExposesSuccessData(LoadOperationStatus status)
        {
            var steps = new List<LoadOperationStep> { new LoadOperationStep("BeginLoadMerging", null, null, LoadOperationStatus.InvocationCompleted) };
            var result = new LoadOperationResult<AutomaticLoadGenerationData>(status, null, steps, true, "Unverified body");
            steps.Clear(); Assert.That(result.Steps.Count(), Is.EqualTo(1)); Assert.That(result.IsSuccessful, Is.False);
            Assert.That(result.MayHaveChangedModel, Is.True); Assert.Throws<InvalidOperationException>(() => { var ignored = result.Data; });
        }
        [Test]
        public void GenerationDataDoesNotInventCountsOrReplaceTheRequestedStart()
        {
            var payload = new AutomaticLoadGenerationData(10, 16, null);
            var result = new LoadOperationResult<AutomaticLoadGenerationData>(LoadOperationStatus.Success, payload,
                new[] { new LoadOperationStep("AddAutoLoadCombinations", 0, null, LoadOperationStatus.Success) }, true);
            Assert.That(result.Data.PreferredStartCaseId, Is.EqualTo(10)); Assert.That(result.Data.ActualStartCaseId, Is.EqualTo(16));
            Assert.That(result.Data.GeneratedCount, Is.Null); Assert.That(result.Steps.Single().NativeReturnCode, Is.Zero);
        }
    }
}
