# OpenSTAAD Load managed capability audit

**Review date:** 23 October 2026
**Reviewed source:** main at `60b47fa`.
**Scope:** Semantic review of the 56 documented native Load names without dispatch, across the current interface, adapter, delegated entities, grouping extensions, helper services, wrapper and related Geometry/Root surfaces. The installed 2024–2026 contract inventory is the native reference. The private main project remained read-only; this audit does not port functions or change production code.

## 1. Corrected conclusion

An unused native entry point is not sufficient evidence of a missing managed capability. The original **56** is an exact native-dispatch count, not a count of missing user features. All **115 private main-interface signatures** remain ported; IOSLoad and OSLoadAdapter each expose 120 overloads.

| Classification among the 56 | Count | Meaning |
| --- | ---: | --- |
| Covered by an existing alternative | 1 | Primary creation already uses richer native entry points. |
| Available through a managed composition | 1 | Combination membership can be derived from fresh combination discovery. |
| Partially covered; distinct scope still absent | 4 | Related numeric data or narrower assignments exist, but the whole native contract is not reproduced. |
| No managed equivalent found | 50 | No implemented managed operation/composition supplies the documented capability. |
| Total exact native names without dispatch | 56 | This static metric remains unchanged. |

Thus **54 native contracts have outstanding scope**, comprising 50 absent operations and four partial cases. This is not a promise that 54 separate public methods are needed: alternate native deletion/floor operations and merge begin/end may share a suitable managed API. Some contracts require further clarification before implementation. The count is local to these 56 rows; it is not a certification that all already-dispatched functions have complete behavioral or engineering parity.

## 2. Confirmed alternatives and compositions

[CreateNewLoadCase](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1860) routes by `CaseType`: reference cases call [CreateNewReferenceLoad](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1818), primary cases call [CreateNewPrimaryLoad](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1786) and its explicit-ID overload. These dispatch `CreateNewReferenceLoad`, `CreateNewPrimaryLoadEx` and `CreateNewPrimaryLoadEx2`. Primary creation is already available; a second title-only native binding is optional. The convenience returns the adapter, while the direct creation overloads return the native numeric result. This audit does not certify their existing status/validation behavior or equate unspecified native defaults with an explicit managed classification.

`CreateNewReferenceLoad` was never one of the 56 undispatched names: it is already called directly and through the combined router.

[GetAllLoadCombinationCases](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1411) reads fresh native combination IDs and metadata. [GetLoadCase](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1585) can select the combination family. Membership is therefore already expressible without calling `IsCombinationCase`:

~~~csharp
using System.Linq;
using StaadPro.Interop.Enums;

// load is a connected IOSLoad; targetId is a positive case ID.
bool isCombination = load.GetAllLoadCombinationCases()
    .Any(candidate => candidate.Id == targetId);
// Alternatively:
bool foundInCombinationFamily =
    load.GetLoadCase(targetId, LoadCaseType.LoadCombination) != null;
~~~

This is an observed-inventory predicate with managed exceptions on discovery failure, not a compiled IsCombinationCase method or an emulation of its 1/0/-1 result packet. It requires more native calls and metadata than the direct predicate; changes during discovery remain possible. A dedicated predicate may still be useful for performance/diagnostics, but should be described as a convenience/optimization rather than a previously unavailable capability.

## 3. Partial coverage that must not be overstated

| Native operation | Existing managed path | Remaining distinction |
| --- | --- | --- |
| AddSelfWeightInXYZ | [AddSelfweightInXYZTo](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2220); Geometry inventories can enumerate current beams, plates and solids | Explicit IDs are supported. There is no integrated whole-model assignment, and no evidence here that an explicit snapshot has identical whole-model command semantics. |
| GetNodalLoadInfo | [GetNodalLoads](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L784) and [GetNodalLoadsGroupedByLoadCase](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L663) | Force/moment data is available per node, but native command-index identity is not preserved. |
| GetMemberLoadInfo | Six per-member typed readers and [GetMemberLoadsGroupedByLoadCase](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L651) | Supported numeric definitions can be read; original generic command-index lookup and broader command families are not covered. |
| GetAssignmentListForLoadType | Existing nodal/member grouping helpers | Buckets reconstructed for supplied entities are not the original native command-kind/index assignments. |

Grouping by equal numeric definitions can join independently entered commands. Duplicate bucketing preserves assignment multiplicity but cannot recover original command indices/order. The caller-supplied entity set can also omit assignments. These helpers must not be presented as full native command inventory readers.

## 4. Complete semantic disposition of all 56 entries

Each native-name link opens its detailed declaration, parameter and return contract in the existing inventory. Status describes managed capability, separately from the absence of an exact native call.

| Native name | Managed capability | Evidence or remaining scope |
| --- | --- | --- |
| [SplitLoadsOnBeam](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#splitloadsonbeam) | Absent | No implementation transfers/splits load definitions when a member is split. Geometry splitting and ordinary member-load assignment are not equivalent. |
| [AddLoadCasesToEnvelop](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addloadcasestoenvelop) | Absent | Envelope creation and membership read-back exist; adding members to an existing envelope is not implemented. Recreating an envelope is not a verified equivalent. |
| [RemoveLoadCasesFromEnvelop](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#removeloadcasesfromenvelop) | Absent | No removal of selected members from an existing envelope. Creation/read-back cannot establish safe in-place removal. |
| [DeleteLoadEnvelop](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#deleteloadenvelop) | Absent | No deletion of an existing envelope. |
| [IsCombinationCase](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#iscombinationcase) | Derived capability | GetAllLoadCombinationCases().Any(c => c.Id == targetId), or GetLoadCase(targetId, LoadCaseType.LoadCombination) != null, answers combination membership from a fresh native inventory. A dedicated predicate/status-result API is absent, but the user capability exists. |
| [AddAutoLoadCombinations](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addautoloadcombinations) | Absent | CreateLoadCombinations consumes caller-supplied definitions/factors. It does not generate code/category-based automatic combinations or return their generated IDs. |
| [AddAutoCombinationRepeat](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addautocombinationrepeat) | Absent | AddRepeatLoadsTo consumes caller-supplied factors; EditorLoadCombination title helpers only format text. Neither performs native automatic repeat-load generation. |
| [CreateNewPrimaryLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#createnewprimaryload) | Covered alternative | Primary creation is implemented by CreateNewPrimaryLoad overloads using native Ex/Ex2, and by CreateNewLoadCase routing on CaseType. The unused title-only native entry point is an alternative, not a missing creation capability. Default classification and exact native status behavior are not asserted identical. |
| [GetActiveLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getactiveload) | Absent | SetLoadCaseActive changes activation; it does not read current activation. OpenStaadWrapper has no active-load property. |
| [SetLoadType](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#setloadtype) | Absent | LoadCase.Type is managed metadata; setting it does not update an existing native case. Creation-time type selection is not reclassification. |
| [GetLoadListCount](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getloadlistcount) | Absent | CreateLoadList exists; there is no reader for the number of native LOAD LIST records. Envelope count is a different inventory. |
| [GetLoadCountInLoadList](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getloadcountinloadlist) | Absent | No count reader for a native load-list index. Envelope member counts refer to envelopes, not LOAD LIST records. |
| [GetLoadsInLoadList](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getloadsinloadlist) | Absent | No reader for a native LOAD LIST record. GetLoadEnvelopeDetail reads envelope membership, a different object. |
| [DeleteLoadList](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#deleteloadlist) | Absent | No native load-list deletion. Creating another list is not a verified deletion contract. |
| [SetASDLoadAttribute](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#setasdloadattribute) | Absent | No ASD attribute mutation on an existing native load case. |
| [SetLSDLoadAttribute](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#setlsdloadattribute) | Absent | No LSD attribute mutation on an existing native load case. |
| [RemoveAttribute](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#removeattribute) | Absent | No native load-attribute removal. |
| [GetAttribute](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getattribute) | Absent | No native load-attribute reader. The help documents 0/-1 status without a usable attribute payload, so result semantics require clarification before implementation. |
| [GetLoadTypeCount](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getloadtypecount) | Absent | No generic count of native commands by command-kind code in the active case. Per-entity loads and grouped definitions cannot reliably reconstruct command multiplicity. |
| [GetListSizeForLoadType](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getlistsizeforloadtype) | Absent | No assignment count for the original native command-kind/index. Grouped entity counts lose native command identity. |
| [GetAssignmentListForLoadType](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getassignmentlistforloadtype) | Partial | GetNodalLoadsGroupedByLoadCase and GetMemberLoadsGroupedByLoadCase reconstruct definition/entity buckets for supplied nodes/beams and supported families. They require an entity scope and do not preserve native command identity, order or a general command-type/index assignment list. |
| [DeletePrimaryLoadCases](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#deleteprimaryloadcases) | Absent | ClearPrimaryLoadCase/ClearPrimaryLoadCases remove load items; they do not implement deletion of the case definitions. The native deletion entry point also accepts a reference-family flag. |
| [DeleteReferenceLoadCases](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#deletereferenceloadcases) | Absent | ClearReferenceLoadCase/ClearReferenceLoadCases clear items; they do not implement reference-case definition deletion. A future shared managed deletion API may cover both native deletion alternatives. |
| [GetLoadItemsCount](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getloaditemscount) | Absent | No generic count of original load items in a specified case; counting typed per-entity results can duplicate or omit original commands. |
| [GetLoadItemType](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getloaditemtype) | Absent | LoadItemType is a coarser managed classification of known objects. It does not query the original native item code at a case/index, and cannot replace this native table. |
| [AddSelfWeightInXYZ](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addselfweightinxyz) | Partial | AddSelfweightInXYZTo already applies self-weight to explicit beam/plate/solid IDs. Geometry inventories can supply current IDs, but no integrated whole-model self-weight operation exists; an explicit list does not establish identical whole-model command scope or future-geometry behavior. |
| [GetNodalLoadInfo](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getnodalloadinfo) | Partial | GetNodalLoads reads six force/moment components per supplied node; PopulateNodalLoads and grouping helpers also exist. They do not select or preserve the original native load-command index. |
| [AddMemberAreaLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addmemberareaload) | Absent | No built-in area-load assignment. Existing member uniform-force definitions have different engineering dimensions/semantics. |
| [AddMemberFixedEnd](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addmemberfixedend) | Absent | No built-in fixed-end member-load definition or assignment. End actions in Output are results, not load assignments. |
| [GetMemberLoadInfo](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getmemberloadinfo) | Partial | Six typed per-member readers and population/grouping helpers already return supported member force/moment definitions and positions. They do not expose the original generic command-index lookup or every native command family. |
| [AddElementHydrostaticPressure](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addelementhydrostaticpressure) | Absent | No plate hydrostatic pressure definition/assignment. Uniform plate pressure and seismic member weights do not provide this operation. |
| [AddElementTrapPressureEx](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addelementtrappressureex) | Absent | AddPlateUniformPressure implements uniform pressure, not a varying/corner-pressure definition. The uniform special case does not cover this contract. |
| [GetElementPressureLoadCount](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getelementpressureloadcount) | Absent | No plate-pressure load count reader. AddPlateUniformPressure is a write operation; plate Output stress queries are analysis results. |
| [GetElementPressureLoads](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getelementpressureloads) | Absent | No plate-pressure definition read-back, including direction, pressure and local rectangle coordinates. |
| [GetElementConcLoadCount](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getelementconcloadcount) | Absent | No plate concentrated-load count reader. |
| [GetElementConcLoads](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getelementconcloads) | Absent | No plate concentrated-load definition reader. |
| [GetElementLoadInfo](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getelementloadinfo) | Absent | No element load-command index reader. Plate geometry and Output results do not supply applied load-command definitions. |
| [AddMemberFloorLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addmemberfloorload) | Absent | No automatic floor-panel discovery/distribution assignment. A caller manually generating member loads is not an implemented floor-load operation. |
| [GetBeamCountAtFloor](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getbeamcountatfloor) | Absent | No floor-region beam count service matching this native operation. Geometry inventories do not by themselves implement floor-region semantics. |
| [GetInfluenceArea](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getinfluencearea) | Absent | No floor influence-area calculation returning beam/area associations. |
| [AddMemberFloorLoadEx](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addmemberfloorloadex) | Absent | No range/group/one-way floor-load request or assignment. A future common floor-load API can cover overlapping legacy and extended cases. |
| [AddStrainLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addstrainload) | Absent | No strain-load assignment. AddAxialThermalGradientTo uses AddTemperatureLoad and is not strain loading. |
| [AddSeismicLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addseismicload) | Absent | Seismic definition/weight APIs and LoadType.SeismicH/SeismicV metadata exist; no seismic-load application to a case is implemented. |
| [IsDynamicLoadIncluded](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#isdynamicloadincluded) | Absent | No dynamic-load inclusion query. Case classification alone does not identify actual dynamic commands. |
| [AddWindLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addwindload) | Absent | Wind definition, exposure, intensity and profile computations exist; applying a wind-load command to a case is not implemented. |
| [AddNotionalLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#addnotionalload) | Absent | No notional-load assignment. Repeat/reference factor APIs do not add direction-bearing notional contributions. |
| [GetNoLoadFactorInRepeatLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getnoloadfactorinrepeatload) | Absent | GetRepeatLoadCases discovers cases containing repeat commands; it does not return the number of contributions in a particular repeat command. |
| [GetRepeatLoadByIndex](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getrepeatloadbyindex) | Absent | No read-back of case-ID/factor contributions by repeat-command index. AddRepeatLoadsTo writes contributions and does not read existing definitions. |
| [GetNotionalLoadCount](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getnotionalloadcount) | Absent | No count of notional-load commands in the active case. |
| [GetNoLoadFactorDirectionInNotionalLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getnoloadfactordirectioninnotionalload) | Absent | No count of contributions for a notional-command index. |
| [GetNotionalLoadByIndex](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getnotionalloadbyindex) | Absent | No indexed notional case-ID/factor/direction reader. |
| [GetReferenceLoadCount](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getreferenceloadcount) | Absent | GetAllReferenceLoadCases enumerates reference definitions via GetReferenceLoadCaseCount; it does not count reference-load command items in an active case. |
| [GetNoOfSetsInReferenceLoad](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getnoofsetsinreferenceload) | Absent | No contribution count for a reference-load command index. |
| [GetReferenceLoadByIndex](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#getreferenceloadbyindex) | Absent | No indexed reference case-ID/factor reader. AddReferenceLoadsTo is a write operation. |
| [BeginLoadMerging](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#beginloadmerging) | Absent | No managed merge lifecycle or bounded merge operation. Duplicate-assignment grouping helpers do not merge native beam loads. |
| [EndLoadMerging](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md#endloadmerging) | Absent | No paired native merge cleanup. A future managed merge operation should own both begin/end calls rather than expose two unrelated user obligations. |

## 5. Similar names that do not establish equivalence

- [ClearPrimaryLoadCases](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1748) and [ClearReferenceLoadCases](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1771) clear load items. The installed native help distinguishes this from deleting the case definitions.
- [CreateLoadCombinations](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L724) consumes supplied combinations/factors. It does not generate combinations automatically from a design code/category.
- [CreateLoadEnvelopExt](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2260) creates an envelope. It does not prove safe append/removal/deletion of an existing envelope.
- [AddWindDefinition](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L364) and [AddSeismicDefinition](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L219) create definitions, not wind/seismic commands applied to a case.
- [AddPlateUniformPressure](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2051) writes uniform pressure. It does not read plate loads or implement general trapezoidal/hydrostatic pressures.
- `LoadCase.Type` and `LoadItemType` are managed metadata/classifications, not native reclassification or native command-index queries.
- `AddAxialThermalGradientTo` uses temperature loading, not strain loading.
- `GetRepeatLoadCases` discovers cases with repeat commands, not their indexed case/factor contribution definitions. Reference-definition counts similarly differ from reference-command counts.
- `ComObject`, `RawLoad`, custom `MemberLoad.ApplyTo` subclasses, and rewriting a model file can enable consumer-written operations. They are extension/escape mechanisms, not preimplemented typed coverage for every native function. Output forces/stresses are analysis results rather than applied load definitions.

## 6. Implementation implications

Do not add a duplicate primary-creation method merely to remove an unused-native row. Treat a dedicated combination predicate as an optional improvement, retaining the existing derived capability in the coverage report. Prioritize missing observations such as current active load, native load lists, original item/command types and counts, then typed indexed contribution/assignment readers. Explicitly retain the distinct command-index scope of the four partial cases.

For a next group of 15 genuinely absent native contracts, a coherent read-only/observation-oriented choice is: `GetActiveLoad`, `GetLoadListCount`, `GetLoadCountInLoadList`, `GetLoadsInLoadList`, `GetLoadTypeCount`, `GetListSizeForLoadType`, `GetLoadItemsCount`, `GetLoadItemType`, `IsDynamicLoadIncluded`, `GetNoLoadFactorInRepeatLoad`, `GetRepeatLoadByIndex`, `GetNotionalLoadCount`, `GetNoLoadFactorDirectionInNotionalLoad`, `GetNotionalLoadByIndex`, `GetReferenceLoadCount`. Active-case methods require an explicit managed case/activation policy; this list is a proposal, not implemented APIs.

Keep three metadata-only candidates separate: `AddSeismicDefElementWeight`, `AddSeismicDefFloorWeight` and `MergeLoadsOnBeam`. No typed equivalent was found, but their contract/support evidence must be established independently; they are not part of the 56 or the 50.

## 7. Verification and limits

Inspected current production implementations rather than relying on interface names, comments or source-count subtraction. Traced creation, family filtering, combination discovery, case clearing, explicit self-weight, typed nodal/member readers, delegated ApplyTo implementations and grouping helpers. Rechecked native scope against installed Bentley help, including the distinctions between clear/delete, definition/application, entity/command indexing and case-definition/command counts. Existing parsed method bodies were matched against the current adapter source.

Reran **490 existing offline tests**, covering Load creation, combinations, family filtering, explicit self-weight/envelope/list mutations, nodal/member readers and grouped/definition operations: all passed. No new production functions or tests were added, no native COM/model calls were made, and no NuGet package was built. This source/contract audit does not certify native marshaling, unspecified defaults, equivalence under external model edits, or live engineering correctness.

Related: [cross-version architecture and plan](OpenSTAAD-Load-Multi-Version-Architecture-and-Plan.md), [full native contract inventory](OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md), [Load porting guide](../Load-Porting.md).
