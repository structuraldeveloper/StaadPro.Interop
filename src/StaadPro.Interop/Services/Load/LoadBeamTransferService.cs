using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using StaadPro.Interop.Enums.Loads;
using StaadPro.Interop.Models.Loads.Contracts;

namespace StaadPro.Interop.Services.Load
{
    internal sealed class LoadBeamTransferService
    {
        private readonly LoadServiceContext context;
        internal LoadBeamTransferService(LoadServiceContext context) { this.context = context; }
        internal LoadOperationResult SplitLoadsOnBeam(int originalBeamId, int newBeamId)
        {
            LoadContractArguments.Positive(originalBeamId, nameof(originalBeamId)); LoadContractArguments.Positive(newBeamId, nameof(newBeamId));
            if (originalBeamId == newBeamId) throw new ArgumentException("Beam IDs must differ.", nameof(newBeamId));
            return context.Mutate("SplitLoadsOnBeam", () => (object)context.Binding().SplitLoadsOnBeam(originalBeamId, newBeamId), 1, true);
        }
        internal LoadOperationResult MergeBeamLoads(IEnumerable<BeamLoadMergeRequest> requests)
        {
            if (requests == null) throw new ArgumentNullException(nameof(requests));
            var selected = requests.ToArray();
            if (selected.Length == 0 || selected.Any(x => x == null)) throw new ArgumentException("Specify nonempty, non-null merge requests.", nameof(requests));
            return context.Run(() =>
            {
                var steps = new List<LoadOperationStep>(); Exception bodyFailure = null, cleanupFailure = null;
                try
                {
                    context.Binding().BeginLoadMerging();
                    steps.Add(new LoadOperationStep("BeginLoadMerging", null, null, LoadOperationStatus.InvocationCompleted));
                    foreach (var request in selected)
                    {
                        // Public Bentley class listing and CLR metadata agree on the body member and
                        // its two input IDs. Neither defines its VARIANT return semantics. Retain that
                        // response as evidence; never label an assumed zero/one interpretation Success.
                        object raw = context.Binding().MergeLoadsOnBeam(request.RetainedBeamId, request.SourceBeamId);
                        bool? flag = raw is bool b ? b : (bool?)null;
                        int? code = raw == null || flag.HasValue ? (int?)null : LoadServiceContext.Integer(raw, "MergeLoadsOnBeam");
                        steps.Add(new LoadOperationStep("MergeLoadsOnBeam", code, flag, LoadOperationStatus.InvocationCompleted));
                    }
                }
                catch (Exception error) { bodyFailure = error; }
                finally
                {
                    try
                    {
                        context.Binding().EndLoadMerging();
                        steps.Add(new LoadOperationStep("EndLoadMerging", null, null, LoadOperationStatus.InvocationCompleted));
                    }
                    catch (Exception error) { cleanupFailure = error; }
                }
                if (bodyFailure != null || cleanupFailure != null)
                {
                    Exception error = bodyFailure != null && cleanupFailure != null
                        ? new AggregateException("Load merging and cleanup both failed; native effects may be partial.", bodyFailure, cleanupFailure)
                        : bodyFailure ?? cleanupFailure;
                    error.Data["LoadOperationSteps"] = Array.AsReadOnly(steps.ToArray());
                    error.Data["MayHaveChangedModel"] = true;
                    ExceptionDispatchInfo.Capture(error).Throw();
                }
                return new LoadOperationResult(LoadOperationStatus.InvocationCompleted, steps, true,
                    "Merge calls and cleanup returned. Bentley does not document the merge-body return semantics or an atomic rollback contract; verify the resulting loads before use. This is invocation evidence, not certified success.");
            });
        }
    }
}
