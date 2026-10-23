using System;
using StaadPro.Interop.Enums.Loads;
using StaadPro.Interop.Models.Loads.Contracts;

namespace StaadPro.Interop.Services.Load
{
    internal sealed class LoadAutomaticGenerationService
    {
        private readonly LoadServiceContext context;
        internal LoadAutomaticGenerationService(LoadServiceContext context) { this.context = context; }
        internal LoadOperationResult<AutomaticLoadGenerationData> CreateAutomaticCombinations(AutomaticCombinationRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            return context.Run(() =>
            {
                const string op = "AddAutoLoadCombinations";
                object start = request.PreferredStartCaseId;
                object ids = LoadServiceContext.IdsForGeneration(request.Cases);
                var step = LoadServiceContext.Step(op, (object)context.Binding().AddAutoLoadCombinations(request.CodeName, request.CategoryName, ids, ref start), 0);
                if (step.Status != LoadOperationStatus.Success)
                    return new LoadOperationResult<AutomaticLoadGenerationData>(step.Status, null, new[] { step }, true, CombinationDiagnostic(step.NativeReturnCode));
                int actual = LoadServiceContext.NativeId(LoadServiceContext.Integer(start, op + " actual start"), op);
                return new LoadOperationResult<AutomaticLoadGenerationData>(LoadOperationStatus.Success,
                    new AutomaticLoadGenerationData(request.PreferredStartCaseId, actual, null), new[] { step }, true);
            });
        }
        private string CombinationDiagnostic(int? code)
        {
            // Native STAAD version, never the client assembly's ApplicationVersion. Unknown versions
            // retain the raw code without applying a guessed newest-version error table.
            int? major = context.Wrapper.StaadVersion?.Major;
            if (major == 24) return code == -1 ? "STAAD 2024 reported general failure." : "Unclassified STAAD 2024 response: " + code;
            if (major == 25 || major == 26)
            {
                if (code == -8002) return "A selected load case was not found.";
                if (code == -8040) return "The installed combination code name was not found.";
                if (code == -8041) return "The installed combination category name was not found.";
            }
            return "Native automatic-combination response " + code + "; no error interpretation was assumed for an unreviewed version.";
        }
        internal LoadOperationResult<AutomaticLoadGenerationData> CreateAutomaticRepeatLoads(AutomaticRepeatLoadRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            return context.Run(() =>
            {
                const string op = "AddAutoCombinationRepeat";
                var generation = request.Generation; var options = request.NotionalOptions;
                var directions = options?.Directions ?? AutomaticNotionalDirections.None;
                object start = generation.PreferredStartCaseId, generated = -1;
                object ids = LoadServiceContext.IdsForGeneration(generation.Cases);
                var step = LoadServiceContext.Step(op, (object)context.Binding().AddAutoCombinationRepeat(generation.CodeName, generation.CategoryName,
                    ids, ref start, ref generated, request.IncludeReferenceLoads, options != null, options?.Factor ?? 0.0,
                    options?.UseGb50017Factor ?? false, options?.FloorCount ?? 0,
                    directions.HasFlag(AutomaticNotionalDirections.PositiveX), directions.HasFlag(AutomaticNotionalDirections.NegativeX),
                    directions.HasFlag(AutomaticNotionalDirections.PositiveZ), directions.HasFlag(AutomaticNotionalDirections.NegativeZ)), 0);
                if (step.Status != LoadOperationStatus.Success) return new LoadOperationResult<AutomaticLoadGenerationData>(step.Status, null, new[] { step }, true);
                int actual = LoadServiceContext.NativeId(LoadServiceContext.Integer(start, op + " actual start"), op);
                int count = LoadServiceContext.Integer(generated, op + " generated count");
                if (count < 0) throw new InvalidOperationException(op + " returned an invalid or unwritten generated count. Native generation may already have changed the model.");
                return new LoadOperationResult<AutomaticLoadGenerationData>(LoadOperationStatus.Success,
                    new AutomaticLoadGenerationData(generation.PreferredStartCaseId, actual, count), new[] { step }, true);
            });
        }
    }
}
