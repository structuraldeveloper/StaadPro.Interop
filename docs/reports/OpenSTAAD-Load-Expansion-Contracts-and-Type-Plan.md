# OpenSTAAD Load expansion contracts and supporting types

**Date:** 23 October 2026
**Scope:** All 56 native entries in the original semantic audit, including already-covered alternatives and the 15 inspection methods implemented in the preceding increment.
**Status:** Contracts and supporting type definitions prepared for review. No additional native dispatch or pending runtime method was added by this contract-preparation step.

## 1. Review files and runtime boundary

The method signatures are in [IOSLoadExpansionContract.cs](../contracts/IOSLoadExpansionContract.cs). The [56-entry contract index](../contracts/LoadExpansionContractIndex.json) maps every original native entry to an existing or proposed managed signature, return type, parameter types, default, route and qualification note. The file is compiled into the test assembly to ensure every referenced type exists; it is not shipped as a production interface or instantiated through a runtime placeholder.

Once approved for implementation, a signature moves into IOSLoad and its implementation into the existing OSLoadAdapter, which delegates to an internal service where useful. No partial adapter, user-visible service factory, sub-adapter or extra connection object is needed. Consumers continue through `wrapper.Load`. Do not add throwing method stubs merely to publish these proposals.

| Disposition of the original 56 entries | Entries | Public additions required by this proposal |
| --- | ---: | ---: |
| Inspection functions already implemented in the preceding increment | 15 | 0 further signatures; existing contracts inherited in the review interface |
| Existing creation alternative and derived combination membership | 2 | 0 duplicate methods |
| Remaining entries represented by proposed additions | 39 | 36 proposed signatures |
| Total | 56 | 36 pending signatures |

The difference of three comes from shared APIs: both native case-deletion forms map to DeleteLoadCases; legacy/extended floor loading map to AddFloorLoad; Begin/End merging belong to one bounded MergeBeamLoads operation. This is managed contract consolidation, not evidence that those unused native calls are implemented.

## 2. Compatibility and signature rules

Existing public APIs and their return types are preserved, including creation returning int or IOSLoad. New observation methods return `LoadQueryResult<T>` and mutations return `LoadOperationResult` or `LoadOperationResult<T>`. All new parameters have descriptive names. Collections use IEnumerable with explicit ownership rules and materialized read-only results; no public tuple, ValueTuple, object payload, parallel-array return or public ref/out parameter is introduced.

Reused target identity is `ILoadCase`, not a new LoadCaseIdentity. Existing primary-only inspection methods retain `int primaryLoadCaseId` and are not silently widened to reference-family activation. Mutations/assignment queries may take ILoadCase where primary/reference selection is appropriate, with family support checked per function before native invocation. No unchecked fallback to a primary case with an overlapping number is permitted.

## 3. All 56 entry mappings

Each native name links to its installed-reference contract inventory. Existing signature names/parameters are shown as they currently exist; only proposed new signatures use the new descriptive names.

| Native entry | Managed signature | Disposition |
| --- | --- | --- |
| [SplitLoadsOnBeam](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#splitloadsonbeam) | `LoadOperationResult SplitLoadsOnBeam(int originalBeamId, int newBeamId)` | Qualification required |
| [AddLoadCasesToEnvelop](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addloadcasestoenvelop) | `LoadOperationResult AddCasesToEnvelope(int envelopeId, IEnumerable<int> primaryLoadCaseIds)` | Defined; implementation pending |
| [RemoveLoadCasesFromEnvelop](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#removeloadcasesfromenvelop) | `LoadOperationResult RemoveCasesFromEnvelope(int envelopeId, IEnumerable<int> primaryLoadCaseIds)` | Defined; implementation pending |
| [DeleteLoadEnvelop](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#deleteloadenvelop) | `LoadOperationResult DeleteEnvelope(int envelopeId)` | Defined; implementation pending |
| [IsCombinationCase](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#iscombinationcase) | `IEnumerable<ILoadCase> GetAllLoadCasesOfType(LoadCaseType loadCaseType = LoadCaseType.PrimaryLoad)` | Derived capability; no duplicate API |
| [AddAutoLoadCombinations](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addautoloadcombinations) | `LoadOperationResult<AutomaticLoadGenerationData> CreateAutomaticCombinations(AutomaticCombinationRequest request)` | Defined; implementation pending |
| [AddAutoCombinationRepeat](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addautocombinationrepeat) | `LoadOperationResult<AutomaticLoadGenerationData> CreateAutomaticRepeatLoads(AutomaticRepeatLoadRequest request)` | Defined; implementation pending |
| [CreateNewPrimaryLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#createnewprimaryload) | `int CreateNewPrimaryLoad(string lcTitle, LoadType loadType)` | Covered alternative; no duplicate API |
| [GetActiveLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getactiveload) | `LoadQueryResult<int> GetActiveLoadCaseNumber()` | Implemented inspection; contract review requested |
| [SetLoadType](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#setloadtype) | `LoadOperationResult SetLoadType(IEnumerable<int> loadCaseIds, LoadType loadType)` | Defined; implementation pending |
| [GetLoadListCount](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getloadlistcount) | `LoadQueryResult<int> GetLoadListCount()` | Implemented inspection; contract review requested |
| [GetLoadCountInLoadList](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getloadcountinloadlist) | `LoadQueryResult<int> GetLoadListCaseCount(int loadListIndex)` | Implemented inspection; contract review requested |
| [GetLoadsInLoadList](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getloadsinloadlist) | `LoadQueryResult<IEnumerable<int>> GetLoadListCases(int loadListIndex, int maximumResults = 100000)` | Implemented inspection; contract review requested |
| [DeleteLoadList](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#deleteloadlist) | `LoadOperationResult DeleteLoadList(int loadListIndex)` | Defined; implementation pending |
| [SetASDLoadAttribute](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#setasdloadattribute) | `LoadOperationResult SetAllowableStressAttribute(int loadCaseId, LoadStrengthType strengthType, bool increaseAllowableStress)` | Defined; implementation pending |
| [SetLSDLoadAttribute](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#setlsdloadattribute) | `LoadOperationResult SetLimitStateAttribute(int loadCaseId)` | Defined; implementation pending |
| [RemoveAttribute](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#removeattribute) | `LoadOperationResult RemoveLoadAttribute(int loadCaseId)` | Defined; implementation pending |
| [GetAttribute](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getattribute) | `LoadQueryResult<LoadAttributeObservation> GetLoadAttribute(int loadCaseId)` | Blocked: native payload undefined |
| [GetLoadTypeCount](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getloadtypecount) | `LoadQueryResult<int> GetLoadCommandCount(int primaryLoadCaseId, LoadCommandKind commandKind)` | Implemented inspection; contract review requested |
| [GetListSizeForLoadType](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getlistsizeforloadtype) | `LoadQueryResult<int> GetAssignedEntityCount(int primaryLoadCaseId, LoadCommandKind commandKind, int commandIndex)` | Implemented inspection; contract review requested |
| [GetAssignmentListForLoadType](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getassignmentlistforloadtype) | `LoadQueryResult<IEnumerable<int>> GetAssignedEntityIds(int primaryLoadCaseId, LoadCommandKind commandKind, int commandIndex, int maximumResults = 100000)` | Defined; implementation pending |
| [DeletePrimaryLoadCases](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#deleteprimaryloadcases) | `LoadOperationResult DeleteLoadCases(LoadCaseType caseType, IEnumerable<int> loadCaseIds)` | Defined; implementation pending |
| [DeleteReferenceLoadCases](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#deletereferenceloadcases) | `LoadOperationResult DeleteLoadCases(LoadCaseType caseType, IEnumerable<int> loadCaseIds)` | Defined; implementation pending |
| [GetLoadItemsCount](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getloaditemscount) | `LoadQueryResult<int> GetLoadItemCount(int loadCaseId)` | Implemented inspection; contract review requested |
| [GetLoadItemType](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getloaditemtype) | `LoadQueryResult<LoadItemKind> GetLoadItemKind(int loadCaseId, int loadItemIndex)` | Implemented inspection; contract review requested |
| [AddSelfWeightInXYZ](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addselfweightinxyz) | `LoadOperationResult AddSelfWeight(ILoadCase targetCase, SelfWeightDirection direction, double factor)` | Defined; implementation pending |
| [GetNodalLoadInfo](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getnodalloadinfo) | `LoadQueryResult<NodalLoadCommandInfo> GetNodalLoadCommand(ILoadCase targetCase, int loadItemIndex)` | Qualification required |
| [AddMemberAreaLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addmemberareaload) | `LoadOperationResult AddMemberAreaLoad(ILoadCase targetCase, IEnumerable<int> memberIds, double areaLoad)` | Defined; implementation pending |
| [AddMemberFixedEnd](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addmemberfixedend) | `LoadOperationResult AddMemberFixedEndLoad(ILoadCase targetCase, IEnumerable<int> memberIds, FixedEndLoadDefinition definition)` | Defined; implementation pending |
| [GetMemberLoadInfo](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getmemberloadinfo) | `LoadQueryResult<MemberLoadCommandInfo> GetMemberLoadCommand(ILoadCase targetCase, int loadItemIndex)` | Defined; implementation pending |
| [AddElementHydrostaticPressure](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addelementhydrostaticpressure) | `LoadOperationResult AddHydrostaticPressure(ILoadCase targetCase, IEnumerable<int> plateIds, HydrostaticPressureDefinition definition)` | Defined; implementation pending |
| [AddElementTrapPressureEx](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addelementtrappressureex) | `LoadOperationResult AddTrapezoidalPressure(ILoadCase targetCase, IEnumerable<int> plateIds, TrapezoidalPressureDefinition definition)` | Defined; implementation pending |
| [GetElementPressureLoadCount](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getelementpressureloadcount) | `LoadQueryResult<int> GetPlatePressureLoadCount(ILoadCase targetCase, int plateId)` | Defined; implementation pending |
| [GetElementPressureLoads](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getelementpressureloads) | `LoadQueryResult<IEnumerable<PlatePressureLoad>> GetPlatePressureLoads(ILoadCase targetCase, int plateId, int maximumResults = 100000)` | Defined; implementation pending |
| [GetElementConcLoadCount](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getelementconcloadcount) | `LoadQueryResult<int> GetPlateConcentratedLoadCount(ILoadCase targetCase, int plateId)` | Defined; implementation pending |
| [GetElementConcLoads](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getelementconcloads) | `LoadQueryResult<IEnumerable<PlateConcentratedLoad>> GetPlateConcentratedLoads(ILoadCase targetCase, int plateId, int maximumResults = 100000)` | Defined; implementation pending |
| [GetElementLoadInfo](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getelementloadinfo) | `LoadQueryResult<PlateLoadCommandInfo> GetPlateLoadCommand(ILoadCase targetCase, int loadItemIndex)` | Qualification required |
| [AddMemberFloorLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addmemberfloorload) | `LoadOperationResult AddFloorLoad(ILoadCase targetCase, FloorLoadDefinition definition)` | Defined; implementation pending |
| [GetBeamCountAtFloor](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getbeamcountatfloor) | `LoadQueryResult<int> GetFloorBeamCount(FloorRegion region)` | Defined; implementation pending |
| [GetInfluenceArea](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getinfluencearea) | `LoadQueryResult<IEnumerable<BeamInfluenceArea>> GetFloorInfluenceAreas(FloorRegion region, int maximumResults = 100000)` | Defined; implementation pending |
| [AddMemberFloorLoadEx](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addmemberfloorloadex) | `LoadOperationResult AddFloorLoad(ILoadCase targetCase, FloorLoadDefinition definition)` | Defined; implementation pending |
| [AddStrainLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addstrainload) | `LoadOperationResult AddStrainLoad(ILoadCase targetCase, IEnumerable<int> elementIds, double axialElongation)` | Qualification required |
| [AddSeismicLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addseismicload) | `LoadOperationResult AddSeismicLoad(ILoadCase targetCase, GlobalAxis direction, double factor)` | Defined; implementation pending |
| [IsDynamicLoadIncluded](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#isdynamicloadincluded) | `LoadQueryResult<bool> ContainsDynamicLoad(int loadCaseId)` | Implemented inspection; contract review requested |
| [AddWindLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addwindload) | `LoadOperationResult AddWindLoad(ILoadCase targetCase, WindLoadDefinition definition)` | Defined; implementation pending |
| [AddNotionalLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addnotionalload) | `LoadOperationResult AddNotionalLoad(ILoadCase targetCase, IEnumerable<NotionalLoadContribution> contributions)` | Defined; implementation pending |
| [GetNoLoadFactorInRepeatLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getnoloadfactorinrepeatload) | `LoadQueryResult<int> GetRepeatContributionCount(int primaryLoadCaseId, int repeatItemIndex)` | Implemented inspection; contract review requested |
| [GetRepeatLoadByIndex](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getrepeatloadbyindex) | `LoadQueryResult<IEnumerable<LoadFactorContribution>> GetRepeatContributions(int primaryLoadCaseId, int repeatItemIndex, int maximumResults = 100000)` | Implemented inspection; contract review requested |
| [GetNotionalLoadCount](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getnotionalloadcount) | `LoadQueryResult<int> GetNotionalItemCount(int primaryLoadCaseId)` | Implemented inspection; contract review requested |
| [GetNoLoadFactorDirectionInNotionalLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getnoloadfactordirectioninnotionalload) | `LoadQueryResult<int> GetNotionalContributionCount(int primaryLoadCaseId, int notionalItemIndex)` | Implemented inspection; contract review requested |
| [GetNotionalLoadByIndex](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getnotionalloadbyindex) | `LoadQueryResult<IEnumerable<NotionalLoadContribution>> GetNotionalContributions(int primaryLoadCaseId, int notionalItemIndex, int maximumResults = 100000)` | Implemented inspection; contract review requested |
| [GetReferenceLoadCount](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getreferenceloadcount) | `LoadQueryResult<int> GetReferenceItemCount(int primaryLoadCaseId)` | Implemented inspection; contract review requested |
| [GetNoOfSetsInReferenceLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getnoofsetsinreferenceload) | `LoadQueryResult<int> GetReferenceContributionCount(int primaryLoadCaseId, int referenceItemIndex)` | Qualification required |
| [GetReferenceLoadByIndex](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getreferenceloadbyindex) | `LoadQueryResult<IEnumerable<ReferenceLoadContribution>> GetReferenceContributions(int primaryLoadCaseId, int referenceItemIndex, int maximumResults = 100000)` | Qualification required |
| [BeginLoadMerging](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#beginloadmerging) | `LoadOperationResult MergeBeamLoads(IEnumerable<BeamLoadMergeRequest> requests)` | Blocked: merge-body contract unverified |
| [EndLoadMerging](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#endloadmerging) | `LoadOperationResult MergeBeamLoads(IEnumerable<BeamLoadMergeRequest> requests)` | Blocked: merge-body contract unverified |

The JSON index contains the expanded parameter descriptions/defaults for pending signatures; their XML equivalents are in the separate review interface. The 15 implemented signatures retain their full XML in IOSLoad/OSLoadAdapter. For combination membership use `GetAllLoadCasesOfType(LoadCaseType.LoadCombination)` with an Id membership check, or the existing GetAllLoadCombinationCases; no duplicate IsCombinationCase contract is required. Primary creation continues through the existing Ex/Ex2 wrappers and CreateNewLoadCase routing.

## 4. Read-only main-project reuse review

Reviewed the main interop source strictly read-only, including its Load interface/adapter, load entities, Forces, case/enums, generation/template and transfer models. File fingerprints are recorded in the [contract index](../contracts/LoadExpansionContractIndex.json) for reproducibility. No main project was built or modified. Similar naming alone is not treated as an implementation.

| Existing item | Location / source | Decision |
| --- | --- | --- |
| ILoadCase / LoadCase | Existing current types, already derived from main load-case concepts | Reused for target-case identity instead of inventing LoadCaseIdentity. Snapshot ID/family before submission; do not import main result/UI collections. |
| LoadCaseType / LoadType | Existing current enums | Reused for case family and engineering classification. No new LoadCaseFamily enum; deletion accepts only primary/reference. |
| Forces / NodalLoad / MemberLoad hierarchy | Existing current types plus main definitions | Forces is reused with defensive copying for fixed-end input and nodal-command output. Existing per-entity loads remain; a command-info class adds native index identity instead of pretending grouping preserves it. |
| LoadQueryResult / LoadQueryStatus / LoadCommandKind / LoadItemKind | Existing 15-query increment | Reused unchanged for future observations. Command-kind and item-kind tables remain distinct. |
| NotionalLoadContribution | Existing 15-query result class | Added a validated public constructor so the same named type can be supplied as input. No second notional input class or tuple. |
| SelfWeightLoad | Main Entities/Loads/SelfWeightLoad.cs | Reviewed but not ported as a duplicate: existing SelfWeightDirection plus finite factor already expresses the required request. Main uses LoadDirection codes, so its object is not forwarded numerically. |
| PlatePressure | Main Entities/PlatePressure.cs | Reused its four joint-value concept in TrapezoidalPressureDefinition.AtCorners. Did not import Y-up fluid computation, four-decimal text output, invalid-direction fallback or equality-by-plate-ID into a native request. |
| ElementConcentratedLoad | Main Entities/Loads/ElementConcentratedLoad.cs | Reused direction, magnitude and local-coordinate fields in PlateConcentratedLoad. Main ApplyTo is NotImplementedException and uses proprietary bases/extensions; those are not copied. |
| EditorLoadCombination / StaadLoadCombination / LoadCombinationsDefinition | Existing current models and main definitions | Retained existing supplied-definition APIs. The main LoadCombinationsDefinition is application/template/file-persistence oriented; it does not describe native code/category automatic generation and is not ported into that request. |
| GlobalAxis | Main Enums/GlobalAxis.cs | Ported the simple X/Y/Z semantic enum with explicit 0/1/2 values and XML documentation. Future native services translate separately for 1/2/3 interpolation/ranges. |
| ASCE wind / seismic configuration models | Already ported Models/Loads/Wind and Seismic families | Reused for their existing definition APIs. WindLoadDefinition is new because it applies a definition to a case with direction/region; it is not an alternative ASCE definition. |
| TransferLoads and application services | Main Models/Tools/TransferLoads.cs and source searches | UI/storage/selection orchestration is not a native operation/result contract. No new dependency on ReInvented.Shared or proprietary OpenSTAADUI is introduced. |

No exact main-project counterpart was found for the new native automatic-generation request/status models, ASD attribute selection, bounded global/floor request, whole-command member/plate result packets, influence-area association or bounded native merge lifecycle. Their managed definitions are new and isolated; proprietary implementation helpers are not brought across.

## 5. Supporting types created before further implementation

Related small types share coherent files to avoid one tiny file per enum or result field. These are real managed definitions with XML documentation, finite-value/input validation where appropriate, owned selections and immutable snapshots; they do not call native Load functions.

| Type | Definition file | Purpose |
| --- | --- | --- |
| `LoadCaseSelection` | [AutomaticLoadGeneration.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/AutomaticLoadGeneration.cs) | Typed input or snapshot with explicit selection/mode; no native invocation. |
| `AutomaticCombinationRequest` | [AutomaticLoadGeneration.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/AutomaticLoadGeneration.cs) | Typed input or snapshot with explicit selection/mode; no native invocation. |
| `AutomaticNotionalOptions` | [AutomaticLoadGeneration.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/AutomaticLoadGeneration.cs) | Typed input or snapshot with explicit selection/mode; no native invocation. |
| `AutomaticRepeatLoadRequest` | [AutomaticLoadGeneration.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/AutomaticLoadGeneration.cs) | Typed input or snapshot with explicit selection/mode; no native invocation. |
| `AutomaticLoadGenerationData` | [AutomaticLoadGeneration.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/AutomaticLoadGeneration.cs) | Named output/status contract; populated only by a future verified reader/service. |
| `GlobalLoadRegion` | [LoadRegions.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/LoadRegions.cs) | Typed input or snapshot with explicit selection/mode; no native invocation. |
| `FloorRegion` | [LoadRegions.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/LoadRegions.cs) | Typed input or snapshot with explicit selection/mode; no native invocation. |
| `FixedEndLoadDefinition` | [LoadAssignmentDefinitions.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/LoadAssignmentDefinitions.cs) | Typed input or snapshot with explicit selection/mode; no native invocation. |
| `HydrostaticPressureDefinition` | [LoadAssignmentDefinitions.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/LoadAssignmentDefinitions.cs) | Typed input or snapshot with explicit selection/mode; no native invocation. |
| `TrapezoidalPressureDefinition` | [LoadAssignmentDefinitions.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/LoadAssignmentDefinitions.cs) | Typed input or snapshot with explicit selection/mode; no native invocation. |
| `FloorLoadDefinition` | [LoadAssignmentDefinitions.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/LoadAssignmentDefinitions.cs) | Typed input or snapshot with explicit selection/mode; no native invocation. |
| `WindLoadDefinition` | [LoadAssignmentDefinitions.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/LoadAssignmentDefinitions.cs) | Typed input or snapshot with explicit selection/mode; no native invocation. |
| `LoadOperationResult` | [LoadOperationResult.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/LoadOperationResult.cs) | Named output/status contract; populated only by a future verified reader/service. |
| `LoadOperationResult<T>` | [LoadOperationResult.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/LoadOperationResult.cs) | Named output/status contract; populated only by a future verified reader/service. |
| `LoadOperationStep` | [LoadOperationResult.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/LoadOperationResult.cs) | Typed input or snapshot with explicit selection/mode; no native invocation. |
| `MemberLoadCommandInfo` | [LoadInspectionData.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/LoadInspectionData.cs) | Named output/status contract; populated only by a future verified reader/service. |
| `PlatePressureLoad` | [LoadInspectionData.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/LoadInspectionData.cs) | Typed input or snapshot with explicit selection/mode; no native invocation. |
| `PlateConcentratedLoad` | [LoadInspectionData.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/LoadInspectionData.cs) | Typed input or snapshot with explicit selection/mode; no native invocation. |
| `PlateLoadCommandInfo` | [LoadInspectionData.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/LoadInspectionData.cs) | Named output/status contract; populated only by a future verified reader/service. |
| `BeamInfluenceArea` | [LoadInspectionData.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/LoadInspectionData.cs) | Named output/status contract; populated only by a future verified reader/service. |
| `ReferenceLoadContribution` | [LoadInspectionData.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/LoadInspectionData.cs) | Named output/status contract; populated only by a future verified reader/service. |
| `LoadAttributeObservation` | [LoadInspectionData.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/LoadInspectionData.cs) | Named output/status contract; populated only by a future verified reader/service. |
| `NodalLoadCommandInfo` | [NodalLoadCommandInfo.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/NodalLoadCommandInfo.cs) | Named output/status contract; populated only by a future verified reader/service. |
| `BeamLoadMergeRequest` | [BeamLoadMergeRequest.cs](../../src/StaadPro.Interop/Models/Loads/Contracts/BeamLoadMergeRequest.cs) | Typed input or snapshot with explicit selection/mode; no native invocation. |
| `GlobalAxis` | [GlobalAxis.cs](../../src/StaadPro.Interop/Enums/GlobalAxis.cs) | Explicit semantic/native option domain; no guessing between incompatible code tables. |
| `LoadStrengthType` | [LoadContractOptions.cs](../../src/StaadPro.Interop/Enums/Loads/LoadContractOptions.cs) | Explicit semantic/native option domain; no guessing between incompatible code tables. |
| `PlatePressureVariation` | [LoadContractOptions.cs](../../src/StaadPro.Interop/Enums/Loads/LoadContractOptions.cs) | Explicit semantic/native option domain; no guessing between incompatible code tables. |
| `WindLoadDirection` | [LoadContractOptions.cs](../../src/StaadPro.Interop/Enums/Loads/LoadContractOptions.cs) | Explicit semantic/native option domain; no guessing between incompatible code tables. |
| `AutomaticNotionalDirections` | [LoadContractOptions.cs](../../src/StaadPro.Interop/Enums/Loads/LoadContractOptions.cs) | Explicit semantic/native option domain; no guessing between incompatible code tables. |
| `LoadOperationStatus` | [LoadContractOptions.cs](../../src/StaadPro.Interop/Enums/Loads/LoadContractOptions.cs) | Explicit semantic/native option domain; no guessing between incompatible code tables. |

NotionalLoadContribution is reused, with a public constructor taking positive case ID, explicit LoadCaseType, finite factor and NotionalLoadDirection. The internal signed read-back constructor is retained. Future assignment packets partition primary/reference families with positive IDs; signed read-back IDs are not blindly forwarded to separate family input arrays.

### 5.1 Requests and ownership

- LoadCaseSelection requires either AllCases() explicitly or a nonempty SpecifiedCases sequence. The latter is enumerated once and copied with order/duplicates retained. Empty input cannot accidentally select the whole model.
- AutomaticCombinationRequest carries the exact installed Codes.ini code/category strings, explicit case selection and preferred start. AutomaticRepeatLoadRequest composes that request and optional notional settings. No enum invents a fixed list of installation-dependent code/category strings.
- AutomaticNotionalOptions requires explicit X/-X/Z/-Z flags. GB50017 floor count is permitted only when that option is enabled. A null notional option disables generation.
- FixedEndLoadDefinition uses the existing Forces type, copying both ends on construction and returning independent copies. It does not reuse member-end analysis actions as applied loads.
- GlobalLoadRegion validates finite ordered bounds; equal bounds permit planes. FloorRegion adds an independent range axis. FloorLoadDefinition factories choose range or group mode explicitly; the reserved native string 0 cannot masquerade as a group name.
- Hydrostatic pressure keeps load direction separate from interpolation axis. TrapezoidalPressureDefinition factories select two-value local-axis variation or four joint pressures without mixed states. Triangular-plate conventions remain a qualification gate.
- WindLoadDefinition is an application request with an existing definition ID, 1/3/4/6 signed X/Z direction, finite factor, explicit structure flag and bounded region. Y-up native restrictions must be checked before dispatch.

### 5.2 Outputs and status

Observations reuse the existing LoadQueryResult/Data guard. New mutation results have ordered LoadOperationStep evidence, actual integral/Boolean returns, operation-specific status and MayHaveChangedModel; failures and partial effects must not be represented as rollback. A void call returning is InvocationCompleted, not certified success. Typed Data is accessible only on qualified Success.

AutomaticLoadGenerationData separates requested and actual start. GeneratedCount is nullable because AddAutoLoadCombinations does not return a generated count; no contiguous-ID list is fabricated. ReferenceLoadContribution is separate from the existing repeat contribution type, whose contract explicitly states primary cases.

MemberLoadCommandInfo preserves the three force slots and three distances as individually named properties, retaining original command kind and index. Dimensions depend on the kind; no blanket force interpretation is imposed. PlateLoadCommandInfo similarly names four pressure and four local-coordinate values and requires the documented 0–8 direction translation. Per-entity plate readers use named PlatePressureLoad/PlateConcentratedLoad records. BeamInfluenceArea keeps each native beam number paired with its area.

### 5.3 Constructing requests before native implementations exist

The following compiles against the supporting types and makes no native call:

~~~csharp
var cases = LoadCaseSelection.SpecifiedCases(new[] { 17, 41 });
var automatic = new AutomaticCombinationRequest(
    "AISC 9th Ed", "2.3 LRFD General", cases, preferredStartCaseId: 100);
var notional = new AutomaticNotionalOptions(
    AutomaticNotionalDirections.PositiveX | AutomaticNotionalDirections.NegativeZ,
    factor: 0.002);
var repeat = new AutomaticRepeatLoadRequest(automatic,
    includeReferenceLoads: true, notionalOptions: notional);
var pressure = TrapezoidalPressureDefinition.AtCorners(
    LoadDirection.LocalZ, pressureAtNodeA: -1, pressureAtNodeB: -2,
    pressureAtNodeC: -3, pressureAtNodeD: -4);
~~~

There is deliberately no callable CreateAutomaticCombinations/AddTrapezoidalPressure adapter method yet. Those signatures remain review-only until promoted with their real implementations.

## 6. Internal organization and calling convention

~~~text
Adapters/Interfaces/IOSLoad.cs                 implemented public signatures only
Adapters/Models/OSLoadAdapter.cs               single adapter, explicit sections and delegates
Models/Loads/Contracts/                       request/result definitions prepared here
Models/Loads/Queries/                         reused implemented inspection results
Enums/Loads/                                 distinct option and status domains
Services/Load/LoadInspectionService.cs         implemented inspection routines
Services/Load/LoadContractArguments.cs         pure managed validation/copy helpers
Services/Load/LoadCaseManagementService.cs     planned: case/list/envelope/attribute operations
Services/Load/LoadAutomaticGenerationService.cs planned: generation packets and result decoding
Services/Load/LoadAssignmentService.cs         planned: additional typed load mutations
Services/Load/LoadAssignmentQueryService.cs    planned: command/entity/floor readers
Services/Load/LoadBeamTransferService.cs       planned: qualified split/merge lifecycle
docs/contracts/IOSLoadExpansionContract.cs    review-only, compiled by tests
~~~

Planned service files are not created as empty or throwing placeholders. This organization preserves a simple user call such as `wrapper.Load.AddWindLoad(targetCase, definition)` after implementation. Adapter sections identify native expansion/inspection work; the detailed code is independently reviewable in the service file.

Shared contracts stay in common services. Only evidenced native differences select narrow internal policies based on `wrapper.StaadVersion`, never host ApplicationVersion. Automatic-combination error interpretation differs between 2024 and 2025/2026. Unsupported or unverified choices must be rejected before mutation; no public year-specific adapters or strings choosing service implementations.

## 7. Evidence gates before pending functions are enabled

| Contract | Required evidence |
| --- | --- |
| GetAttribute | Establish whether the scalar is status or attribute information and how any payload is obtained. LoadAttributeObservation only preserves an undecoded value. No attribute enum/property is guessed. This pending method is withheld. |
| Begin/End merging | Qualify metadata-only MergeLoadsOnBeam body, exact packets, topology, errors and effects. Attempt End in finally on the same COM context; preserve primary and cleanup failures. No separate public Begin/End calls. The managed merge operation is withheld. |
| SplitLoadsOnBeam | Verify the inconsistent parameter table, beam relationship and case scope; do not assume geometry splitting and load transfer are atomic. |
| Reference contribution readers | Corroborate index base, count-return semantics and output sizing; current narrative labels count/ID inconsistently. |
| Nodal/plate command readers | Confirm original index semantics; only ElementLoadPressure is documented for GetElementLoadInfo. Respect its separate zero-based direction domain and -1 unimplemented status. |
| Floor loading | Confirm range/group/one-way flags and the legacy GlobalX/Y-range subset before asserting equivalent dispatch. |
| Strain/area/fixed-end and pressure quantities | Verify native dimensions, axes, signs and plate-shape conventions; requests preserve finite raw values without fabricated conversions. |
| All new native implementations | Caller COM context, explicit validated family activation, bounded buffers, operation-specific status meanings, owned snapshots, no partial Data, live read-back and disposable engineering models. |

## 8. Verification and next steps

Validation passed **2,476 offline tests**, including **28 contract-focused cases**, and a Release build with zero warnings/errors. The review interface compiles against the defined production types. Tests check all 56 unique mappings, all 36 pending signatures, no public runtime stub exposure, no tuple/object/array/ref-out signatures, complete XML on supporting public members, request validation, single enumeration and copy ownership, explicit pressure/floor modes, case-family handling, result Data guards and honest void/generation evidence.

The native implementation state remains at the preceding 15-query increment: 135 Load overloads and 88/129 documented native dispatches. Remaining 41 unused names consist of one covered alternative, one derived capability, four partial overlaps and 35 absent operations. This contract work does not convert unused names into implemented native functions.

After review, implement a coherent approved subset by promoting its signatures into IOSLoad, adding real single-adapter delegates and tests, and enabling internal services only after their required evidence is available. Update matrices from actual native calls. Prepared distribution packages remain unchanged; these supporting type definitions require a future package build/version before users receive them.
