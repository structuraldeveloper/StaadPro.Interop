using System;
using System.Collections.Generic;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Loads;
using StaadPro.Interop.Models.Loads.Contracts;
using StaadPro.Interop.Models.Loads.Queries;

namespace StaadPro.Interop.Services.Load
{
    internal sealed class LoadCaseManagementService
    {
        private readonly LoadServiceContext context;
        internal LoadCaseManagementService(LoadServiceContext context) { this.context = context; }
        internal LoadOperationResult AddCasesToEnvelope(int envelopeId, IEnumerable<int> primaryLoadCaseIds)
        {
            LoadContractArguments.Positive(envelopeId, nameof(envelopeId)); var ids = LoadServiceContext.Ids(primaryLoadCaseIds, nameof(primaryLoadCaseIds));
            return context.Mutate("AddLoadCasesToEnvelop", () => (object)context.Binding().AddLoadCasesToEnvelop(envelopeId, ids), 1);
        }
        internal LoadOperationResult RemoveCasesFromEnvelope(int envelopeId, IEnumerable<int> primaryLoadCaseIds)
        {
            LoadContractArguments.Positive(envelopeId, nameof(envelopeId)); var ids = LoadServiceContext.Ids(primaryLoadCaseIds, nameof(primaryLoadCaseIds));
            return context.Mutate("RemoveLoadCasesFromEnvelop", () => (object)context.Binding().RemoveLoadCasesFromEnvelop(envelopeId, ids), 1);
        }
        internal LoadOperationResult DeleteEnvelope(int envelopeId)
        {
            LoadContractArguments.Positive(envelopeId, nameof(envelopeId));
            return context.Mutate("DeleteLoadEnvelop", () => (object)context.Binding().DeleteLoadEnvelop(envelopeId), 1);
        }
        internal LoadOperationResult SetLoadType(IEnumerable<int> loadCaseIds, LoadType loadType)
        {
            var ids = LoadServiceContext.Ids(loadCaseIds, nameof(loadCaseIds)); LoadContractArguments.Defined(loadType, nameof(loadType));
            return context.Mutate("SetLoadType", () => (object)context.Binding().SetLoadType(ids, (int)loadType), 0);
        }
        internal LoadOperationResult DeleteLoadList(int loadListIndex)
        {
            LoadContractArguments.Positive(loadListIndex, nameof(loadListIndex));
            return context.Mutate("DeleteLoadList", () => (object)context.Binding().DeleteLoadList(loadListIndex), 0);
        }
        internal LoadOperationResult SetAllowableStressAttribute(int loadCaseId, LoadStrengthType strengthType, bool increaseAllowableStress)
        {
            LoadContractArguments.Positive(loadCaseId, nameof(loadCaseId)); LoadContractArguments.Defined(strengthType, nameof(strengthType));
            return context.Mutate("SetASDLoadAttribute", () => (object)context.Binding().SetASDLoadAttribute(loadCaseId, (int)strengthType, increaseAllowableStress ? 1 : 0), 0);
        }
        internal LoadOperationResult SetLimitStateAttribute(int loadCaseId)
        {
            LoadContractArguments.Positive(loadCaseId, nameof(loadCaseId));
            return context.Mutate("SetLSDLoadAttribute", () => (object)context.Binding().SetLSDLoadAttribute(loadCaseId), 0);
        }
        internal LoadOperationResult RemoveLoadAttribute(int loadCaseId)
        {
            LoadContractArguments.Positive(loadCaseId, nameof(loadCaseId));
            return context.Mutate("RemoveAttribute", () => (object)context.Binding().RemoveAttribute(loadCaseId), 0);
        }
        internal LoadQueryResult<LoadAttributeObservation> GetLoadAttribute(int loadCaseId)
        {
            LoadContractArguments.Positive(loadCaseId, nameof(loadCaseId));
            return context.Run(() =>
            {
                const string op = "GetAttribute"; int code = LoadServiceContext.Integer((object)context.Binding().GetAttribute(loadCaseId), op);
                if (code == -1) return LoadServiceContext.Query<LoadAttributeObservation>(op, LoadQueryStatus.NativeFailure, code: code);
                if (code != 0) throw new InvalidOperationException(op + " returned a value whose attribute meaning is undocumented.");
                return LoadServiceContext.Query(op, LoadQueryStatus.Success, new LoadAttributeObservation(code), code,
                    diagnostic: "Native status observation only. The installed help exposes no attribute payload or ASD/LSD decoding.");
            });
        }
        internal LoadOperationResult DeleteLoadCases(LoadCaseType caseType, IEnumerable<int> loadCaseIds)
        {
            if (caseType != LoadCaseType.PrimaryLoad && caseType != LoadCaseType.ReferenceLoad) throw new ArgumentOutOfRangeException(nameof(caseType));
            var ids = LoadServiceContext.Ids(loadCaseIds, nameof(loadCaseIds));
            return caseType == LoadCaseType.PrimaryLoad
                ? context.Mutate("DeletePrimaryLoadCases", () => (object)context.Binding().DeletePrimaryLoadCases(ids, false), 1)
                : context.Mutate("DeleteReferenceLoadCases", () => (object)context.Binding().DeleteReferenceLoadCases(ids), 1);
        }
    }
}
