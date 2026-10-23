# OpenSTAAD Load expansion contracts and supporting types

**Date:** 23 October 2026
**Scope:** All 56 native entries in the original semantic audit, including already-covered alternatives and the 15 inspection methods implemented in the preceding increment.
**Status:** Approved contracts implemented. Implemented all **36 approved Load expansion methods** through the existing IOSLoad and single OSLoadAdapter. Source exposes **171 overloads**: 115 main-reference signatures, five earlier conveniences, 15 inspection methods and these 36 additions. Actual dispatch covers **126/129 documented native names**, plus the metadata-corroborated MergeLoadsOnBeam body. The three intentionally unused names are CreateNewPrimaryLoad (richer creation alternatives), IsCombinationCase (derived inventory capability), and AddMemberFloorLoad (the extended floor call supplies the managed route). Native dispatch coverage is not live compatibility or engineering certification.

## 1. Approved contracts and runtime boundary

The [approved signature snapshot](../contracts/IOSLoadExpansionContract.cs) is compiled only by tests; the callable signatures are in [IOSLoad](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs) with implementations in the existing [OSLoadAdapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs). The [56-entry index](../contracts/LoadExpansionContractIndex.json) records exact signatures, parameters, type reuse and internal routes. All 36 methods now have implementations and identical expanded XML on interface/adapter, with “When to use” comparisons, units, state effects, native success conventions and guarded examples. No partial adapter or public helper-service selection was introduced.

GetLoadAttribute is a documented status-only observation and does not decode ASD/LSD settings. MergeBeamLoads returns InvocationCompleted with IsSuccessful false because Bentley does not document the merge-body response meanings. Reference-command index base, nodal index convention, plate-command UI selection, floor distribution and live 2024–2026 marshaling remain explicit qualification requirements. See [Load expansion usage and method comparisons](../Load-Expansion.md).

## 2. Compatibility and signature rules

Existing public APIs and their return types are preserved, including creation returning int or IOSLoad. New observation methods return `LoadQueryResult<T>` and mutations return `LoadOperationResult` or `LoadOperationResult<T>`. All new parameters have descriptive names. Collections use IEnumerable with explicit ownership rules and materialized read-only results; no public tuple, ValueTuple, object payload, parallel-array return or public ref/out parameter is introduced.

Reused target identity is `ILoadCase`, not a new LoadCaseIdentity. Existing primary-only inspection methods retain `int primaryLoadCaseId` and are not silently widened to reference-family activation. Mutations/assignment queries may take ILoadCase where primary/reference selection is appropriate, with family support checked per function before native invocation. No unchecked fallback to a primary case with an overlapping number is permitted.

## 3. All 56 entry mappings

The original audit population is retained. “Implemented” identifies actual dispatch; it does not assert live qualification.

| Native entry | Current disposition | Managed method | Internal route |
| --- | --- | --- | --- |
| SplitLoadsOnBeam | Implemented | `SplitLoadsOnBeam` | LoadBeamTransferService.cs |
| AddLoadCasesToEnvelop | Implemented | `AddCasesToEnvelope` | LoadCaseManagementService.cs |
| RemoveLoadCasesFromEnvelop | Implemented | `RemoveCasesFromEnvelope` | LoadCaseManagementService.cs |
| DeleteLoadEnvelop | Implemented | `DeleteEnvelope` | LoadCaseManagementService.cs |
| IsCombinationCase | Derived capability; no duplicate API | `GetAllLoadCasesOfType` | Existing managed API |
| AddAutoLoadCombinations | Implemented | `CreateAutomaticCombinations` | LoadAutomaticGenerationService.cs |
| AddAutoCombinationRepeat | Implemented | `CreateAutomaticRepeatLoads` | LoadAutomaticGenerationService.cs |
| CreateNewPrimaryLoad | Covered alternative; no duplicate API | `CreateNewPrimaryLoad` | Existing managed API |
| GetActiveLoad | Implemented inspection | `GetActiveLoadCaseNumber` | Existing managed API |
| SetLoadType | Implemented | `SetLoadType` | LoadCaseManagementService.cs |
| GetLoadListCount | Implemented inspection | `GetLoadListCount` | Existing managed API |
| GetLoadCountInLoadList | Implemented inspection | `GetLoadListCaseCount` | Existing managed API |
| GetLoadsInLoadList | Implemented inspection | `GetLoadListCases` | Existing managed API |
| DeleteLoadList | Implemented | `DeleteLoadList` | LoadCaseManagementService.cs |
| SetASDLoadAttribute | Implemented | `SetAllowableStressAttribute` | LoadCaseManagementService.cs |
| SetLSDLoadAttribute | Implemented | `SetLimitStateAttribute` | LoadCaseManagementService.cs |
| RemoveAttribute | Implemented | `RemoveLoadAttribute` | LoadCaseManagementService.cs |
| GetAttribute | Implemented | `GetLoadAttribute` | LoadCaseManagementService.cs |
| GetLoadTypeCount | Implemented inspection | `GetLoadCommandCount` | Existing managed API |
| GetListSizeForLoadType | Implemented inspection | `GetAssignedEntityCount` | Existing managed API |
| GetAssignmentListForLoadType | Implemented | `GetAssignedEntityIds` | LoadInspectionService.cs |
| DeletePrimaryLoadCases | Implemented | `DeleteLoadCases` | LoadCaseManagementService.cs |
| DeleteReferenceLoadCases | Implemented | `DeleteLoadCases` | LoadCaseManagementService.cs |
| GetLoadItemsCount | Implemented inspection | `GetLoadItemCount` | Existing managed API |
| GetLoadItemType | Implemented inspection | `GetLoadItemKind` | Existing managed API |
| AddSelfWeightInXYZ | Implemented | `AddSelfWeight` | LoadAssignmentService.cs |
| GetNodalLoadInfo | Implemented | `GetNodalLoadCommand` | LoadAssignmentQueryService.cs |
| AddMemberAreaLoad | Implemented | `AddMemberAreaLoad` | LoadAssignmentService.cs |
| AddMemberFixedEnd | Implemented | `AddMemberFixedEndLoad` | LoadAssignmentService.cs |
| GetMemberLoadInfo | Implemented | `GetMemberLoadCommand` | LoadAssignmentQueryService.cs |
| AddElementHydrostaticPressure | Implemented | `AddHydrostaticPressure` | LoadAssignmentService.cs |
| AddElementTrapPressureEx | Implemented | `AddTrapezoidalPressure` | LoadAssignmentService.cs |
| GetElementPressureLoadCount | Implemented | `GetPlatePressureLoadCount` | LoadAssignmentQueryService.cs |
| GetElementPressureLoads | Implemented | `GetPlatePressureLoads` | LoadAssignmentQueryService.cs |
| GetElementConcLoadCount | Implemented | `GetPlateConcentratedLoadCount` | LoadAssignmentQueryService.cs |
| GetElementConcLoads | Implemented | `GetPlateConcentratedLoads` | LoadAssignmentQueryService.cs |
| GetElementLoadInfo | Implemented | `GetPlateLoadCommand` | LoadAssignmentQueryService.cs |
| AddMemberFloorLoad | Covered alternative via AddMemberFloorLoadEx | `AddFloorLoad` | LoadAssignmentService.cs |
| GetBeamCountAtFloor | Implemented | `GetFloorBeamCount` | LoadAssignmentQueryService.cs |
| GetInfluenceArea | Implemented | `GetFloorInfluenceAreas` | LoadAssignmentQueryService.cs |
| AddMemberFloorLoadEx | Implemented | `AddFloorLoad` | LoadAssignmentService.cs |
| AddStrainLoad | Implemented | `AddStrainLoad` | LoadAssignmentService.cs |
| AddSeismicLoad | Implemented | `AddSeismicLoad` | LoadAssignmentService.cs |
| IsDynamicLoadIncluded | Implemented inspection | `ContainsDynamicLoad` | Existing managed API |
| AddWindLoad | Implemented | `AddWindLoad` | LoadAssignmentService.cs |
| AddNotionalLoad | Implemented | `AddNotionalLoad` | LoadAssignmentService.cs |
| GetNoLoadFactorInRepeatLoad | Implemented inspection | `GetRepeatContributionCount` | Existing managed API |
| GetRepeatLoadByIndex | Implemented inspection | `GetRepeatContributions` | Existing managed API |
| GetNotionalLoadCount | Implemented inspection | `GetNotionalItemCount` | Existing managed API |
| GetNoLoadFactorDirectionInNotionalLoad | Implemented inspection | `GetNotionalContributionCount` | Existing managed API |
| GetNotionalLoadByIndex | Implemented inspection | `GetNotionalContributions` | Existing managed API |
| GetReferenceLoadCount | Implemented inspection | `GetReferenceItemCount` | Existing managed API |
| GetNoOfSetsInReferenceLoad | Implemented | `GetReferenceContributionCount` | LoadInspectionService.cs |
| GetReferenceLoadByIndex | Implemented | `GetReferenceContributions` | LoadInspectionService.cs |
| BeginLoadMerging | Implemented | `MergeBeamLoads` | LoadBeamTransferService.cs |
| EndLoadMerging | Implemented | `MergeBeamLoads` | LoadBeamTransferService.cs |

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

CreateAutomaticCombinations and AddTrapezoidalPressure are now callable through wrapper.Load with the shown request types. Review IDs, current units and the method-specific IntelliSense before enabling mutations.

## 6. Internal organization and calling convention

~~~text
Adapters/Interfaces/IOSLoad.cs                 implemented public signatures only
Adapters/Models/OSLoadAdapter.cs               single adapter, explicit sections and delegates
Models/Loads/Contracts/                       immutable request/result definitions
Models/Loads/Queries/                         reused implemented inspection results
Enums/Loads/                                 distinct option and status domains
Services/Load/LoadInspectionService.cs         implemented inspection routines
Services/Load/LoadContractArguments.cs         pure managed validation/copy helpers
Services/Load/LoadCaseManagementService.cs     implemented: case/list/envelope/attribute operations
Services/Load/LoadAutomaticGenerationService.cs implemented: generation packets and result decoding
Services/Load/LoadAssignmentService.cs         implemented: additional typed load mutations
Services/Load/LoadAssignmentQueryService.cs    implemented: command/entity/floor readers
Services/Load/LoadBeamTransferService.cs       implemented: qualified split/merge lifecycle
docs/contracts/IOSLoadExpansionContract.cs    approved snapshot, compiled by tests
~~~

All five planned service files are implemented, with LoadServiceContext providing one captured binding, activation handling and shared lock. Consumers use wrapper.Load directly; helpers remain internal and independently reviewable.

Shared contracts stay in common services. Only evidenced native differences select narrow internal policies based on `wrapper.StaadVersion`, never host ApplicationVersion. Automatic-combination error interpretation differs between 2024 and 2025/2026. Unsupported or unverified choices must be rejected before mutation; no public year-specific adapters or strings choosing service implementations.

## 7. Contract decisions and remaining native qualification

GetLoadAttribute is a documented status-only observation and does not decode ASD/LSD settings. MergeBeamLoads returns InvocationCompleted with IsSuccessful false because Bentley does not document the merge-body response meanings. Reference-command index base, nodal index convention, plate-command UI selection, floor distribution and live 2024–2026 marshaling remain explicit qualification requirements. See [Load expansion usage and method comparisons](../Load-Expansion.md).

| Contract | Implemented decision and remaining check |
| --- | --- |
| GetAttribute | Calls the documented scalar contract; exposes 0 only as a status observation, -1 as failure, and rejects unknown values. No attribute encoding is inferred. |
| Merge lifecycle | Class listing and CLR metadata corroborate the body signature. Begin/body/End execute on one context, End is attempted in finally, and body/cleanup exceptions are preserved. Returned codes remain uninterpreted; normal completion is InvocationCompleted, never Success. Live return meanings, topology and case scope remain unqualified. |
| SplitLoadsOnBeam | Uses the declaration and VBA two-beam example, correcting the unrelated parameter-table entry. Does not split geometry or promise case-local scope. |
| Reference contributions | Native index is nonnegative and forwarded unchanged; no guessed base. Count is used for sizing; the fetch scalar is not forced to equal count. Validate index conventions on the acquired build. |
| Command readers | Supported-kind preflight, zero-based original item indices, strict initialized buffers and stable count/kind checks. Nodal base and ElementLoadInfo UI-selection prerequisite remain documented native checks. |
| Floor loading | Extended call for all managed modes; standard range uses the example’s single-space marker, one-way uses documented 0, and group names are exact. No legacy-direction fallback. Verify generated distribution. |
| ASD flag | CLR metadata declares the native increase flag Int32, so the managed Boolean maps to 1/0 internally. |
| Wind application | Requires available Y-up geometry before activation/assignment. No automatic axis remapping. |
| All implementations | Offline behavior is tested. Validate actual COM, engineering values, units and model commands on disposable models before advertising build compatibility. |

## 8. Verification and release state

Passed **2,900 offline tests**, including **424 new expansion cases**, and a Release build with zero warnings/errors. A separate local validation archive passed isolated PackageReference restore/build/runtime checks for **442 selected XML examples**, including all 72 expansion examples with mutation guards disabled. The examples compile against the installed DLL and XML; native paths are exercised by the managed test fakes, not live STAAD. Native integration/engineering qualification remains pending. This source update does not replace or publish the prepared distribution archive; package validation uses a separate local CI version.
