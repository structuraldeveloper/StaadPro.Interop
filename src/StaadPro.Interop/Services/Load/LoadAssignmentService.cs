using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Loads;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Loads.Contracts;
using StaadPro.Interop.Models.Loads.Queries;

namespace StaadPro.Interop.Services.Load
{
    internal sealed class LoadAssignmentService
    {
        private readonly LoadServiceContext context;
        internal LoadAssignmentService(LoadServiceContext context) { this.context = context; }
        internal LoadOperationResult AddSelfWeight(ILoadCase targetCase, SelfWeightDirection direction, double factor)
        {
            var target = LoadServiceContext.Target(targetCase); LoadContractArguments.Defined(direction, nameof(direction)); LoadContractArguments.Finite(factor, nameof(factor));
            return context.Mutate("AddSelfWeightInXYZ", () => (object)context.Binding().AddSelfWeightInXYZ((int)direction, factor), 1, true, target);
        }
        internal LoadOperationResult AddMemberAreaLoad(ILoadCase targetCase, IEnumerable<int> memberIds, double areaLoad)
        {
            var target = LoadServiceContext.Target(targetCase); var ids = LoadServiceContext.Ids(memberIds, nameof(memberIds)); LoadContractArguments.Finite(areaLoad, nameof(areaLoad));
            return context.Mutate("AddMemberAreaLoad", () => (object)context.Binding().AddMemberAreaLoad(ids, areaLoad), 0, target: target);
        }
        internal LoadOperationResult AddMemberFixedEndLoad(ILoadCase targetCase, IEnumerable<int> memberIds, FixedEndLoadDefinition definition)
        {
            var target = LoadServiceContext.Target(targetCase); var ids = LoadServiceContext.Ids(memberIds, nameof(memberIds));
            if (definition == null) throw new ArgumentNullException(nameof(definition));
            var start = Components(definition.StartForces); var end = Components(definition.EndForces);
            return context.Mutate("AddMemberFixedEnd", () => (object)context.Binding().AddMemberFixedEnd(ids, start, end), 0, target: target);
        }
        private static double[] Components(Forces f) => new[] { f.Fx, f.Fy, f.Fz, f.Mx, f.My, f.Mz };
        internal LoadOperationResult AddHydrostaticPressure(ILoadCase targetCase, IEnumerable<int> plateIds, HydrostaticPressureDefinition definition)
        {
            var target = LoadServiceContext.Target(targetCase); var ids = LoadServiceContext.Ids(plateIds, nameof(plateIds));
            if (definition == null) throw new ArgumentNullException(nameof(definition));
            return context.Mutate("AddElementHydrostaticPressure", () => (object)context.Binding().AddElementHydrostaticPressure(ids,
                (int)definition.Direction, (int)definition.InterpolationAxis + 1, definition.MinimumPressure, definition.MaximumPressure), 1, true, target);
        }
        internal LoadOperationResult AddTrapezoidalPressure(ILoadCase targetCase, IEnumerable<int> plateIds, TrapezoidalPressureDefinition definition)
        {
            var target = LoadServiceContext.Target(targetCase); var ids = LoadServiceContext.Ids(plateIds, nameof(plateIds));
            if (definition == null) throw new ArgumentNullException(nameof(definition));
            return context.Mutate("AddElementTrapPressureEx", () => (object)context.Binding().AddElementTrapPressureEx(ids, (int)definition.Direction,
                (int)definition.Variation, definition.FirstPressure, definition.SecondPressure, definition.ThirdPressure ?? 0.0, definition.FourthPressure ?? 0.0), 1, true, target);
        }
        internal LoadOperationResult AddFloorLoad(ILoadCase targetCase, FloorLoadDefinition definition)
        {
            var target = LoadServiceContext.Target(targetCase); if (definition == null) throw new ArgumentNullException(nameof(definition));
            var b = definition.Region?.Bounds;
            // The VBA range example uses a single space for standard distribution. The parameter table
            // reserves empty string and "0" for one-way. Never trim or substitute a group name.
            string mode = definition.UsesFloorGroup ? definition.FloorGroupName : definition.IsOneWay ? "0" : " ";
            int range = definition.UsesFloorGroup ? 3 : (int)definition.Region.RangeAxis;
            return context.Mutate("AddMemberFloorLoadEx", () => (object)context.Binding().AddMemberFloorLoadEx(range, (int)definition.Direction,
                definition.Pressure, mode, b?.MinimumY ?? 0.0, b?.MaximumY ?? 0.0, b?.MinimumZ ?? 0.0, b?.MaximumZ ?? 0.0,
                b?.MinimumX ?? 0.0, b?.MaximumX ?? 0.0), 1, target: target);
        }
        internal LoadOperationResult AddStrainLoad(ILoadCase targetCase, IEnumerable<int> elementIds, double axialElongation)
        {
            var target = LoadServiceContext.Target(targetCase); var ids = LoadServiceContext.Ids(elementIds, nameof(elementIds)); LoadContractArguments.Finite(axialElongation, nameof(axialElongation));
            return context.Mutate("AddStrainLoad", () => (object)context.Binding().AddStrainLoad(ids, axialElongation), 0, target: target);
        }
        internal LoadOperationResult AddSeismicLoad(ILoadCase targetCase, GlobalAxis direction, double factor)
        {
            var target = LoadServiceContext.Target(targetCase); LoadContractArguments.Defined(direction, nameof(direction)); LoadContractArguments.Finite(factor, nameof(factor));
            return context.Mutate("AddSeismicLoad", () => (object)context.Binding().AddSeismicLoad((int)direction, factor), 0, target: target);
        }
        internal LoadOperationResult AddWindLoad(ILoadCase targetCase, WindLoadDefinition definition)
        {
            var target = LoadServiceContext.Target(targetCase); if (definition == null) throw new ArgumentNullException(nameof(definition));
            return context.Run(() =>
            {
                if (context.Wrapper.Geometry == null || context.Wrapper.Geometry.IsZUp())
                    return new LoadOperationResult(LoadOperationStatus.Unsupported, new LoadOperationStep[0], false,
                        "This wind application contract requires a verified Y-up model. No axes were remapped and no load was assigned.");
                var b = definition.Region;
                return context.Mutate("AddWindLoad", () => (object)context.Binding().AddWindLoad(definition.WindDefinitionId, (int)definition.Direction,
                    definition.Factor, definition.IsOpenStructure, b.MinimumY, b.MaximumY, b.MinimumZ, b.MaximumZ, b.MinimumX, b.MaximumX), 0, target: target);
            });
        }
        internal LoadOperationResult AddNotionalLoad(ILoadCase targetCase, IEnumerable<NotionalLoadContribution> contributions)
        {
            var target = LoadServiceContext.Target(targetCase);
            if (contributions == null) throw new ArgumentNullException(nameof(contributions));
            var values = contributions.ToArray();
            if (values.Length == 0 || values.Any(x => x == null)) throw new ArgumentException("Specify nonempty, non-null contributions.", nameof(contributions));
            var primary = values.Where(x => x.CaseType == LoadCaseType.PrimaryLoad).ToArray();
            var reference = values.Where(x => x.CaseType == LoadCaseType.ReferenceLoad).ToArray();
            return context.Mutate("AddNotionalLoad", () => (object)context.Binding().AddNotionalLoad(
                primary.Select(x => x.LoadCaseId).ToArray(), primary.Select(x => x.Factor).ToArray(), primary.Select(x => (int)x.Direction).ToArray(),
                reference.Select(x => x.LoadCaseId).ToArray(), reference.Select(x => x.Factor).ToArray(), reference.Select(x => (int)x.Direction).ToArray()), 0, target: target);
        }
    }
}
