# Load API porting and new assignments

## Current source — 15 additional inspection queries

Implemented the 15 genuinely missing observation contracts selected after the semantic audit. Current IOSLoad/OSLoadAdapter each expose **135 overloads**, retaining all 115 private reference signatures and five conveniences plus 15 native expansions. Native dispatch coverage is now **88/129**, leaving 41 unused entry points: one alternative, one derived capability, four partial overlaps and 35 absent operations. Three metadata-only candidates remain separate. See [Load inspection contracts and examples](Load-Inspection-Queries.md) for all signatures, statuses, index bases, primary activation effects, bounded snapshots and qualification limits. Passed 2,448 offline tests (189 new) and 30 new XML examples; the prepared preview archive predates this source increment.

The audit and reference-port entries below describe the baseline before this increment.

## Managed capability audit — 23 October 2026

The 56 undispatched native names are not 56 missing features. Semantic review found one existing creation alternative, one derived combination-membership capability, four partial overlaps and 50 operations without a managed equivalent. See the [complete semantic audit](reports/OpenSTAAD-Load-Managed-Capability-Audit.md) before selecting new work; 54 native contracts have outstanding scope, without implying 54 separate public methods. No functions were added for this review.

## Reference porting audit — 23 October 2026

Reparsed the strictly read-only main IOSLoad/OSLoadAdapter and the current interface/adapter. All **115 main-interface signatures** are present in both current types, which each expose **120 overloads**. No main-interface port is pending and no NotImplementedException or executable placeholder was found in the Load adapter. Existing NotSupportedException paths describe documented family, profile or representation restrictions, rather than unimplemented stubs.

The full offline regression suite was run with the new Output work. No Load implementation changes were necessary. The separate vendor inventory still has **56 documented native names without dispatch**, plus three metadata-only candidates. These require semantic disposition before API expansion, not a blanket implementation backlog; they are not missing main-project overloads. The semantic audit above records the corrected disposition.

The private main project’s reference `IOSLoad` contains 115 method overloads. This repository now exposes 120: 115 matching reference overloads plus five repository-specific convenience overloads. There are no main-interface reference overloads pending. Counts treat overloads separately and compare both interface and adapter method signatures.

The repository-specific methods are `CreatePrimaryLoadCases`, `CreateReferenceLoadCases` and three `AddPlateUniformPressure` overloads. They explain why subtracting 120 from 115 does not give the pending reference count.

## Cross-version native coverage review — 22 October 2026

Private-source overload parity is separate from Bentley native API coverage. The installed 2024/2025/2026 Load references each document 129 functions across 24 pages. The current source dispatches 73 of those names: 67 directly in OSLoadAdapter and six through member-load entity implementations. Fifty-six documented names have no current dispatch. The inspected 2025/2026 vendor interfaces each declare another three names outside those help sections.

See the [Load multi-version comparison and architecture plan](reports/OpenSTAAD-Load-Multi-Version-Architecture-and-Plan.md) and [complete native contract and managed coverage inventory](reports/OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md). They include the complete matrix, version-sensitive seismic code mapping, status differences, per-function proposals, private-reference backlog and release verification gates. This review changes documentation only; its architecture and API proposals are not implemented.

## Batch 1: AddSelfweightInXYZTo, CreateLoadCombinations, CreateLoadEnvelopExt

Added the pending thread-count `AddSelfweightInXYZTo` overload, `CreateLoadCombinations`, and editor-definition `CreateLoadEnvelopExt`. Preserved the existing typed self-weight and integer-envelope APIs brought in from origin/main. Added managed combination definition types, eager input filtering/validation, entity-scoped caller-thread self-weight and checked envelope creation.

## Batch 2: GetLoadCombination, GetMemberLoadsGroupedByLoadCase, GetNodalLoadsGroupedByLoadCase

Added combination coefficient read-back with validated Boolean native status, typed arrays and primary-case membership. SRSS overall multipliers are rejected because the return model cannot represent them. Added eager member/nodal read-and-group APIs that refresh managed lists, use original case identities, exact definition fields and duplicate-assignment buckets. Input sequences are snapshotted once before clearing; native failures may leave partial refreshes.

## Batch 3: GetRepeatLoadCases, PopulateMemberConcentratedLoads, PopulateMemberConcentratedMoments

Implemented the reference repeat-discovery stub by scanning validated primary cases and checking GetRepeatLoadCount. Positive reports produce independent RepeatLoad metadata with underlying primary IDs; zero native results have documented no-command/general-error ambiguity. Added append-only concentrated force and moment population methods with single input snapshots, validated readers and original case identity. Managed/native partial failure and active-case effects are documented.

## Batch 4: PopulateMemberLinearVaryingLoads, PopulateMemberLoads, PopulateMemberTrapezoidalLoads

Added append-only linear-varying and trapezoidal member population methods. Added eager full member refresh that clears old entries and repopulates all six families in reference order, returning a reusable snapshot of original beam objects. Inputs are enumerated once and validated before managed mutation. Exact native field mapping, case identity, no conversion and partial refresh failure behavior are documented.

## Newly ported functions

```csharp
IOSLoad AddSelfweightInXYZTo(int loadCaseId, LoadCaseType lcType, IEnumerable<int> entities,
    SelfWeightDirection loadDirection = SelfWeightDirection.GlobalY, double factor = -1);
IOSLoad CreateLoadEnvelopExt(int envelopId, LoadEnvelopType envelopType, IEnumerable<int> loadCaseList);
dynamic CreateLoadList(LoadListType loadListType, IEnumerable<int> loadCaseIds);
```

Each retains the main signature and uses the existing non-partial adapter. `SelfWeightDirection` and `LoadListType` preserve native codes/spelling and ship full enum IntelliSense. Input sequences are snapshotted once before native calls; every ID must be positive, order/duplicates are preserved, and caller collections remain unchanged. Null input throws `ArgumentNullException`; invalid IDs/enums/non-finite factors throw `ArgumentOutOfRangeException`. Native COM/binding errors propagate. No mutation is automatically saved or rolled back.

### Self-weight assignment

The family overload supports primary/reference targets only; other families throw `NotSupportedException` before COM. Validates positive case ID, global direction X=1/Y=2/Z=3 and a finite dimensionless factor (default GlobalY/-1). Zero and either sign are allowed. Empty input returns this adapter without activation or assignment. Nonempty input performs checked family activation, then one `AddSelfWeightInXYZToGeometry` call with an independent Int32 array, integer direction and double factor. All COM calls stay on the caller's thread. The target remains active after success or a later mutation failure; no prior-state restoration occurs.

### Envelope creation

`CreateLoadEnvelopExt` validates a positive envelope ID and a defined envelope code 0–6, then calls native `CreateLoadEnvelop` once with integer ID/type and an Int32 case-ID array. Empty input is forwarded for native acceptance/rejection. Case suitability, duplicate membership, existing envelope conflicts and empty-envelope support remain native decisions. No preliminary case lookup or activation occurs. Read-back is available through `GetLoadEnvelopeDetail`; the creation method does not populate managed envelope metadata.

### Load-list selection and different success contracts

`CreateLoadList` validates native list type 0 (`LoadList`) or 1 (`EnvelopList`) and calls the native method once. For an envelope list, IDs identify envelopes rather than cases, despite the reference parameter name `loadCaseIds`. Empty input is forwarded; the wrapper does not infer whether it clears a list. Selection changes native load-list state used by subsequent native commands. It does not activate a case or start analysis.

Bentley's installed 2025 native documentation specifies Boolean success for self-weight, 1/True success for envelope creation, and integer **0 success / -1 failure** for load-list creation. The first two accept Boolean true or native integer 1/-1 for VARIANT_TRUE; false, zero and other/malformed statuses throw `InvalidOperationException`. For load-list creation, only boxed integer zero (`short`, `int`, `long`) is accepted, and the original boxed result is returned through the preserved dynamic signature. Boolean values and nonzero integers are rejected. These mutation semantics deliberately differ from envelope queries' integer-1 success.

This port checks statuses/activation that the reference ignores and snapshots input rather than repeatedly enumerating it. Native references: `OSAPP_Help/group___property_load_self_weight.html`, `group___property_load_enve.html` and `group___property_load_operation.html`. Live mutation, native status marshaling and engineering behavior still need validation on each advertised STAAD version. Offline tests cover argument layouts, input ownership/enumeration/disposal, defaults, every supported classification, activation ordering, failure stopping, exact factors and integer/Boolean status distinctions.

```csharp
load.AddSelfweightInXYZTo(17, LoadCaseType.PrimaryLoad, new[] { 101, 102 });
load.CreateLoadEnvelopExt(7, LoadEnvelopType.Strength, new[] { 17, 41 });
object status = load.CreateLoadList(LoadListType.EnvelopList, new[] { 7 });
```

These IDs must exist and be suitable in the connected model. The statements mutate native state; coordinate COM session access and native persistence in the calling application.

## Envelope queries from the previous batch

The envelope query batch adds `GetEnvelopeIds()`, `GetLoadEnvelopeDetail(int envId)` and `GetAllLoadEnvelopeDetails()`, along with standalone `LoadEnvelopeDetail`, `ILoadEnvelopeDetail` and `LoadEnvelopType`. All function signatures are on `IOSLoad`; all native implementations/helpers are in the existing non-partial `OSLoadAdapter`. The [envelope query guide](Load-Queries.md#load-envelope-queries) covers exact native statuses, empty/invalid results, ownership, examples and live validation.

These three functions complete envelope metadata discovery and membership reads. Unlike the reference's nullable empty results and unchecked native calls, this port returns fresh empty sets on successful zero counts, rejects invalid envelope arguments before COM and throws on native failures or malformed data. Envelope fetching requires integer success status **1**, with **-1** treated as failure. It does not use assignment-query zero success or case-number count semantics. Returned membership is an independent snapshot; editing it performs no native writes.

Reference provenance: main project commit `3f930d742f972c27d961f0e8eff9f1e11c520ff2`, `Adapters/Interfaces/IOSLoad.cs`, `Adapters/Models/OSLoadAdapter.cs`, `Interfaces/ILoadEnvelopeDetail.cs`, `Entities/Loads/LoadEnvelopeDetail.cs` and `Enums/Loads/LoadEnvelopType.cs`. The main project was not modified.

## Earlier grouped assignment additions

The previous batch exposed these three overloads on both `IOSLoad` and `OSLoadAdapter`:

```csharp
IOSLoad AddAxialThermalGradientTo(int loadCaseId, IEnumerable<int> entities, double axialThermalGradient, int nThreads);
IOSLoad AddMemberLoadsFromGroupsByLoadCase(Dictionary<ILoadCase, List<(MemberLoad Load, List<Beam> Beams)>> grouped);
IOSLoad AddNodalLoadsFromGroupsByLoadCase(Dictionary<ILoadCase, List<(NodalLoad Load, List<Node> Nodes)>> grouped);
```

- **Four-parameter thermal overload:** preserves parameter names and types, validates positive `nThreads`, snapshots and validates all input, activates the primary target once and issues one scalar `AddTemperatureLoad(entityId, value, 0.0, 0.0)` call per entity in input order. The reference uses worker threads; this port keeps every COM call on the caller's thread. `nThreads` is retained for compatibility and does not control concurrency. Empty input makes no native calls; duplicate IDs and raw thermal values are preserved. A later native exception stops processing without rolling back earlier assignments.
- **Grouped member assignments:** delegates each retained tuple to the existing `AddMemberLoad` helper. Null load definitions, null beam lists and empty beam lists are skipped, preserving the reference loop. Individual null beams are filtered by the helper; a nonempty list of only null beams still attempts case activation. Concrete member-load types retain their `ApplyTo` native mapping.
- **Grouped nodal assignments:** delegates every tuple to the existing `AddNodalLoad` helper. Null loads or node lists are rejected by that helper. Individual null nodes are filtered, remaining ID order and duplicates are preserved, and Fx/Fy/Fz/Mx/My/Mz are passed unchanged. Empty/all-null node lists or null Forces still attempt case activation but supply no assignment.

Grouped dictionary keys select the destination independently of the load definition's `LoadCase` property. Dictionaries and lists are not sorted, copied or aggregated, and managed entity/load collections are not refreshed. Empty dictionaries and empty case-group lists do nothing. Null case-group lists throw when reached. Validation and failures occur tuple by tuple, so earlier changes may remain after a later invalid entry or native exception. Keep mutable inputs and case-key hash/equality fields stable during processing.

The grouped methods retain the existing assignment helpers' activation behavior: `SetLoadCaseActive` only selects primary/reference cases and ignores native return statuses. Use valid existing primary/reference targets. A failed status-only activation does not prevent assignment; these wrappers do not strengthen that legacy contract. The thermal overload checks activation status before any assignment. All three preserve raw input units, supply no rollback or automatic save, and require serialized access on the appropriate COM thread.

## Earlier factored and thermal assignments

The following signatures preserve the reference method names, parameter names, types and order:

```csharp
IOSLoad AddReferenceLoadsTo(int loadCaseId, Dictionary<int, double> loadCaseNumberFactorPairs);
IOSLoad AddRepeatLoadsTo(int loadCaseId, IDictionary<int, double> loadCaseNumberFactorPairs);
IOSLoad AddAxialThermalGradientTo(int loadCaseId, IEnumerable<int> entities, double axialThermalGradient);
```

- **Reference loads:** snapshots source-case/factor pairs together, activates an existing primary target with `SetLoadActive`, then calls `AddReferenceLoad(int[] ids, double[] factors)`.
- **Repeat loads:** uses the same paired snapshot and activation contract, then calls `AddRepeatLoad(int[] ids, double[] factors)`. It does not enumerate `Keys` and `Values` independently.
- **Thermal loading:** the three-parameter overload snapshots entity IDs, activates the primary target and calls `AddTemperatureLoad(ids, axialThermalGradient, 0.0, 0.0)` with one array. The supplied value is the raw first native temperature argument. The method does not divide by a length or interpret node coordinates; use the installed STAAD temperature-input convention. The four-parameter overload is now ported as described above.

All target/source/entity IDs must be positive, dictionaries and sequences must be non-null, and factors/thermal values must be finite. Negative and zero factors and thermal values are accepted. Thermal ID order and duplicates are preserved. Validation finishes before native activation or assignment; empty inputs return the same adapter without native access. Native case existence, source-family suitability and entity suitability are handled by OpenSTAAD.

Failed or unrecognized activation throws before assignment. Nonempty calls leave the target primary case active afterward, including when assignment throws. They do not restore previous activation, clear assignments, refresh managed collections, save the model or supply rollback. Native assignment return values are not interpreted; COM and binding exceptions propagate. Serialize session access on the appropriate COM thread.

```csharp
// With a connected wrapper and existing primary target 17, reference source 41,
// suitable repeat source 42 and native temperature-load entities 101/102:
wrapper.Load.AddReferenceLoadsTo(17, new Dictionary<int, double> { { 41, 1.25 } });
wrapper.Load.AddRepeatLoadsTo(17, new Dictionary<int, double> { { 42, -0.75 } });
wrapper.Load.AddAxialThermalGradientTo(17, new[] { 101, 102 }, 15.5);
```

## IntelliSense and validation

Each new method has identical expanded XML on `IOSLoad` and `OSLoadAdapter`, including inputs, native mapping, empty input, activation side effects, exceptions and executable examples. The XML ships beside the DLL and does not require source access. The published 1.0.0-preview.20261015 archive documents 78 Load overloads. The current source documents 90; the updated verifier will check 250 selected XML examples in the next package, including the new population and guarded wind examples.

Offline tests check paired arrays, double precision, one thermal input snapshot, caller-thread scalar assignments, primary/reference grouped routing, member subtype dispatch, all six nodal components, skipped/null/empty group entries, unchanged managed data, activation ordering/failure and partial native failures. Installed-package verification compiles and runs the interface and concrete XML examples and checks argument layouts using managed native stand-ins. Live native mutations, engineering units and version-specific behavior remain unverified for these additions.


## Population completion and basic wind definitions — 22 October 2026

Ported the next **12 reference overloads**: two uniform member-load population methods, nodal-load refresh, wind-definition creation, three exposure overloads, three intensity overloads, definition deletion and explicit deletion of all definitions. The single non-partial adapter contains implementations; the interface contains signatures. These are source additions after the published `1.0.0-preview.20261015` archive.

| Function | Result and behavior |
| --- | --- |
| `PopulateMemberUniformlyDistributedLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)` | void; appends force UDL records using the existing reader. |
| `PopulateMemberUniformMoments(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)` | void; appends uniform moment records using the existing reader. |
| `PopulateNodalLoads(IEnumerable<Node> forNodes, IEnumerable<ILoadCase> fromLoadCases)` | Materialized `IEnumerable<Node>`; clears managed nodal loads, then reads the selected cases. |
| `AddWindDefinition(int windDefinitionId, string definitionName)` | Native Int32 status; one ID/name request. |
| `AddWindExposure(int windDefinitionId, double exposureFactor, IEnumerable<int> nodeIds)` | Native status; one Int32-array assignment. |
| `AddWindExposure(int windDefinitionId, double exposureFactor, IEnumerable<Node> nodes)` | Native status; snapshots node IDs and delegates. |
| `AddWindExposure(int windDefinitionId, double exposureFactor, int nodeId)` | Native status; forwards a one-element Int32 array. |
| `AddWindIntensity(int windDefinitionId, double intensity, double height)` | Native status; uses the documented scalar Double form. |
| `AddWindIntensity(int windDefinitionId, IEnumerable<double> intensities, IEnumerable<double> heights)` | Native status; sends parallel Double arrays. |
| `AddWindIntensity(int windDefinitionId, IEnumerable<WindIntensityPair> pairs)` | Native status; snapshots immutable value pairs and delegates. |
| `DeleteWindDefinition(int windDefinitionId = 0)` | Native status; positive ID deletes one; **zero/default requests deletion of all**. |
| `DeleteAllWindDefinitions()` | Native status; explicit alias for deletion with zero. |

### Population contracts

Both member methods are append-only, preserving existing managed member loads. Repeated calls append new records again. Nodal population is an eager refresh: after validating both sequences, it clears every supplied node's entire NodalLoadItems collection, including previously stored cases outside the requested selection. Empty case input therefore clears nodal lists, while it leaves member lists unchanged. Empty entity input makes no native reads.

Each public method snapshots its entity and case sequences once before managed mutations or native reads. Null entities, nonpositive IDs, null/unsupported cases and enumeration failures stop validation before changes. Order and duplicates are preserved. Duplicate entity references trigger duplicate queries and appended records. Results retain the original structural entities and the supplied load-case identities.

Reads run case-major and entity-minor on the caller's COM thread. Existing primary/reference activation and strict count/fetch/array/direction/finite-value checks are reused. No native assignments are created by these population methods. The last activated case remains active; there is no automatic restoration, save, conversion, retry or rollback. A later failure leaves earlier managed results in place. The grouped nodal read now shares the same internal population core, retaining its existing behavior.

### Wind contracts and status handling

Public wind parameters use descriptive names; `windDefinitionId` corresponds to the main project's type number. The parameter types, overload defaults and return types remain source-compatible. Only the three exposure signatures declared in the main interface are counted; its adapter-only single-Node convenience method is outside this increment.

Creation requires a positive ID and a nonblank name without NUL. Name text, including surrounding whitespace and Unicode, is preserved. Exposure requires a finite nonnegative factor and at least one positive node ID. Managed node overloads use IDs only, without reading coordinates or changing the objects. Order and duplicate IDs remain intact.

Intensity pressures must be finite; signed values are preserved because the source contract imposes no positivity rule. Heights must be finite and nonnegative. Parallel sequences must be nonempty and equal in length. Pair overloads preserve index association. Inputs are not sorted or deduplicated; no assumed monotonic profile is imposed. Pressure units are the model's force/length-squared units and heights use model length units, with no conversion.

`WindIntensityPair` is the source's small immutable value-pair abstraction, retained as a readonly struct with value equality and hashing. It holds two doubles, has no identity or lifecycle, and is suitable for value semantics. Its constructor rejects nonfinite values and negative height; the default zero/zero pair is valid. It avoids tuples and mutable profile rows.

The [installed Bentley wind reference](<file:///C:/Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html>) documents:

| Native operation | Documented status |
| --- | --- |
| AddWindDefinition / AddWindExposure / AddWindIntensity | 0 success; -1 general error |
| DeleteWindDefinition | 0 success; -8039 invalid load definition |

The managed methods return these integer statuses unchanged, including unknown codes. They do not convert a nonzero code into success or throw merely because the native operation reported failure. Statuses must be supported integral CLR values fitting Int32; Boolean, null, floating/decimal values, text, enums and overflow are rejected instead of coerced. Inspect the return value explicitly. Malformed status rejection occurs **after** native dispatch and cannot undo native effects.

Every wind operation makes one native request after complete input validation. Scalar intensity preserves its scalar argument form; batch intensity sends arrays. Exposure always sends an Int32 array. No case activation, definition discovery, native-ID availability query, wind-load-case generation, save, retry or rollback is added. Missing native members and COM failures propagate. Deletion defaults to all definitions to preserve the main interface contract; callers should choose the explicit positive ID or explicit all-delete method deliberately.

### Usage

```csharp
// On the correct COM thread, with suitable model IDs:
int status = wrapper.Load.AddWindDefinition(1, "North wind");
if (status == 0)
{
    status = wrapper.Load.AddWindIntensity(1, new[] {
        new StaadPro.Interop.Models.Loads.Wind.WindIntensityPair(1.24, 10),
        new StaadPro.Interop.Models.Loads.Wind.WindIntensityPair(1.41, 15)
    });
    if (status == 0)
        status = wrapper.Load.AddWindExposure(1, 1.0, new[] { 101, 102 });
}
Console.WriteLine(status); // Each step remains a separate native mutation.
```

The wind documentation and signatures were inspected, and argument layouts/status behavior are tested with managed stand-ins. Verification passed **1,761 offline tests**, including **157 new cases**, and a Release solution build with zero warnings/errors. All **250 selected XML examples** compiled and executed in a source-reference consumer using the production verifier selection logic; wind mutation examples remained guarded. No new distribution archive or installed-package result is claimed for this increment. This does not certify live STAAD wind commands or engineering units. The main source project remains strictly read-only.


<a id="asce-wind-and-seismic-definitions"></a>

## ASCE wind and seismic definitions — 22 October 2026

Ported the next **15 reference overloads** into IOSLoad and the existing non-partial OSLoadAdapter. Source coverage is now **105 overloads: 100 matching the main interface plus five repository conveniences**, leaving **15 reference overloads pending**. All new signatures preserve source return types and optional defaults; no public ref/out parameters or tuples were added. These source changes are not included in the published 1.0.0-preview.20261015 archive.

| Managed function | Overloads | Contract |
| --- | ---: | --- |
| AddWindDefinitionASCE7Parameters | 1 | Complete typed ASCE wind definition request; original native Int32 status. |
| ComputeWallWindPressureProfile | 1 | Pre-2016 engineering edition; native generated-point count/error. |
| ComputeWallWindPressureProfileASCE72016 | 1 | ASCE 7-2016 calculation with explicit elevation; count/error only. |
| AddSeismicDefinition | 2 | Typed definition enum or native definition code; checked Boolean result and torsion 0/1. |
| ModifySeismicDefinitionParam | 1 | One uppercase alphanumeric keyword/value update. |
| ModifySeismicDefinitionParams | 4 | Dictionary plus IS1893-2016, IS1893-2002/2005 and IBC model overloads. |
| AddSeismicDefSelfWeight | 1 | Finite dimensionless factor, default 1.0; native Boolean result. |
| AddSeismicDefJointWeight | 3 | ID sequence, Node sequence or single ID; original integer status. |
| AddSeismicDefMemberWeight | 1 | ID-sequence overload with acquired-profile native code, distribution, weight and distances. |

### Wind inputs and wire contracts

Reused the main project's wind structure hierarchy (building, tank/chimney, solid sign, open sign/lattice and trussed tower), explicit velocity/length units, escarpment data and factor overrides. Enums retain their native numeric domains without proprietary assembly dependencies. Models reject nonfinite fields and undefined enum values; nested requests are validated before dispatch. Callers must keep mutable models stable during use.

Definition requests send fifteen native arguments. The topography packet has four Doubles; structure data has seven Doubles; units and override flags each have eight Int32 values; factor values have eight Doubles. The eight-element forms follow source serialization and native index tables 0–7, despite inconsistent size-seven wording in help. Arrays supplied to native code are owned snapshots.

Both profile calculations use ten native arguments, including the explicit unit packet and an Int32 1/0 escarpment flag, matching the inspected Win32 BOOL shape. Profile tables use a different open-sign/tower slot order from definition tables; internal packet preparation reorders these families. The calculation tables use legacy enclosure/tank classifications, including in the 2016 calculation entry. Definition serialization uses the requested publication's classification map. PartiallyOpen is rejected for a legacy packet because it has no distinct documented legacy value; it is not silently substituted with PartiallyEnclosed. These departures from the source's shared packet path are documented and tested against the installed tables; native numerical validation remains pending.

Wind values use the request's explicit unit configuration. ASCE engineering edition is independent of STAAD product year. Profile calculations return a generated-point count; they do not fabricate or return pressure/height points. Referenced native profile getters are outside the inspected Load inventory. These calls can modify native calculation/definition state.

### Seismic identity, validation and results

SeismicCode identifies **definition codes**, not member-weight codes. Values 0–24 have common documented encodings; NRC2020=25 requires an acquired native 25.00.01.424 or 26.00.00.340 profile. The reviewed 2024 contract and unknown/unreviewed builds reject that choice before mutation. Selection uses wrapper.StaadVersion, never the host ApplicationVersion.

The newly ported member-weight overload preserves the reference integer parameter as the **native member-weight code for the acquired profile**. Zero requests native auto-detection. Explicit codes require a reviewed profile and valid range: 1–25 on 24.00.02.354, 1–26 on the reviewed 2025/2026 builds. Integer 5 denotes Chinese GB50011-2001 on the reviewed 2024 table and NRC 2020 on the newer table. No semantic mapping or enum cast is inferred from an integer. A future semantic overload/version policy remains separate planned work.

Distribution codes are Uniform=1 and Concentrated=2. Weight is forwarded unchanged (force for joint/concentrated weight, force per length for uniform member weight). Distances use native model length units from member start, must be finite/nonnegative, and a nonzero end cannot precede start. Both zero describe full-length uniform weight. Use zero for the unused concentrated end distance.

Joint/member selections are enumerated once; positive IDs, input order and duplicates are preserved. Node overloads read only IDs. Empty joint/member selections return the source-compatible successful no-op (0/true) without native dispatch. No load case or seismic definition is implicitly created or selected.

Integer-returning methods preserve native statuses/counts, including unknown integers, and reject null, Boolean, text, floating-point, enum or overflowing results. Zero is success for wind/seismic parameter/joint updates; profile counts have their own native meaning, with -1 documenting failure. Boolean families accept actual bool or integral 0/1/-1 only, and reject other statuses. This Boolean rule must not be applied to integer-status APIs.

### Parameter updates and partial completion

The dictionary overload snapshots and validates every entry before the first update. Keywords must start with an uppercase ASCII letter and contain only uppercase ASCII letters/digits. Their text is preserved; no case normalization or unit conversion occurs. Entries are submitted in snapshot order. The first nonzero native code stops the sequence and is returned unchanged; a later exception/malformed result also stops execution. Earlier native updates remain. Empty dictionaries return zero without dispatch.

Typed seismic models produce independent keyword dictionaries, retain main-source defaults and nullable optional fields, and reject nonfinite values before mutation. They do not select or track the active engineering code. Caller must ensure that keywords, location alternatives, units and values are appropriate to the existing native seismic definition. Validation does not certify engineering suitability or atomicity.

All calls stay on the caller's COM thread. No automatic save, analysis, retry, restoration or rollback is added. A native operation may have effects before reporting an error or returning an invalid value. XML IntelliSense on both interface and adapter covers these contracts, result semantics, defaults, exceptions, side effects and guarded examples.

### Verification

Passed 2,024 offline tests (263 new cases), a Release build and all 280 selected source-reference XML examples. Native mutations in examples remained guarded. These results validate managed contracts and stand-in dispatch, not live native marshaling or engineering calculations. No new package was built or published.

### Usage

```csharp
// With a connected wrapper on its COM thread, and an appropriate existing definition:
var load = wrapper.Load;
bool definitionCreated = load.AddSeismicDefinition(
    StaadPro.Interop.Models.Loads.Seismic.SeismicCode.IS1893_2016,
    considerAccidentalTorsion: true);
if (definitionCreated)
{
    int status = load.ModifySeismicDefinitionParams(
        new StaadPro.Interop.Models.Loads.Seismic.IS1893_2016SeismicParameters
        { Zone = 0.16, ImportanceFactor = 1.0 });
    if (status == 0)
        status = load.AddSeismicDefJointWeight(2.5, new[] { 101, 102 });
    Console.WriteLine(status);
}
// Each call is a separate native mutation; no save or transaction is implied.
```

<a id="load-reference-completion"></a>

## Completed reference port: spectrum, walls and direct analysis — 22 October 2026

Ported the final **15 main-interface reference overloads**, completing **115 of 115 reference signatures**. IOSLoad and the existing non-partial OSLoadAdapter each expose **120 method overloads**, including five repository conveniences. No reference overload remains pending. This measures private-source interface parity, not complete coverage of the Bentley native manual or all adapter-only conveniences in the private project. Current native dispatch coverage is **73 of 129 documented names** (67 direct adapter targets and six delegated member-load mutations); **56 native names remain without a current dispatch**.

| Function | Added overloads | Managed behavior |
| --- | ---: | --- |
| AddSeismicDefMemberWeight | 1 | Beam-selection overload; reuses existing checked ID path and version-sensitive code validation. |
| AddSeismicDefMemberUniformWeight | 1 | Uniform=1; defaults start/end to 0 and native code to auto-detect 0. |
| AddSeismicDefMemberConcentratedWeight | 1 | Concentrated=2; forwards location and zero unused end distance. |
| AddSeismicDefWallArea | 2 | Positive length/thickness pairs, raw or typed, global X/Z; native integer status. |
| AddResponseSpectrumLoadEx | 2 | Typed request or explicit paired arrays, Double precision, seven native arguments. |
| AddDirectAnalysisDefinitionParameter | 3 | IDs, Beams or typed model; documented FLEX/AXIAL modes only. |
| AddDirectAnalysisFlexParameter | 1 | Finite FLEX value and member IDs; no analysis request. |
| AddDirectAnalysisAxialParameter | 1 | AXIAL=2 and exact zero unused scalar. |
| DeleteDirectAnalysisDefinitionParameter | 1 | Explicit removal of FLEX=0 or AXIAL=2 parameters. |
| DeleteDirectAnalysisDefinition | 1 | Explicit whole-definition deletion. |
| AddRSLoad | 1 | Source request model, nineteen native slots and checked Single conversion. |

New public parameters use descriptive names while preserving reference parameter types, order, return types and optional defaults. Implementations remain in the single adapter; packet capture/validation is isolated internally. OpenStaadWrapper and its properties are unchanged. Native calls stay on the caller’s COM thread; no task fan-out, case activation, save, analysis, retry or rollback is added. The main source project remained read-only.

### Seismic member and wall contracts

The Beam member-weight overload snapshots once, rejects null/invalid IDs, preserves ordering and duplicates, and uses IDs without reading coordinates. Both convenience methods reuse the existing ID implementation and its acquired-profile safeguards. Empty member selections retain the successful no-op behavior.

The wall-area code is **15 for IS1893-2016 in the reviewed wall-area table**. It is a separate native domain from SeismicCode.IS1893_2016=12 and member-weight code values. Both wall overloads require 15, validate global X/Z, and supply ordered length/thickness pairs in current native model length units. The string direction is trimmed and uppercased invariantly. Dimensions must be finite and positive, with even array length; empty input returns integer zero without native dispatch.

Wall data follows the main source’s Double-array convention. Help examples mention float arrays while the -107 error text mentions long arrays, so the managed source convention is recorded explicitly and **live native type acceptance remains unverified**. Native integer statuses are preserved, including -1, -107, -8034, -8038 and unknown codes. They are never converted to Boolean success.

### Extended response spectrum

Reused ResponseSpectrumLoadExParameters, SpectrumCurvePoint and the separate ResponseSpectrumCode/ModalCombinationMethod enums. Both overloads capture owned keyword/value/data arrays before one native call. Primary names and values must be non-null, nonempty and equal length. Names must be unique uppercase ASCII keywords containing letters/digits and starting with a letter. Values must be finite. Keyword syntax is checked; code-specific keyword meaning, physical suitability and the intended active case remain caller/native responsibilities.

Generation names/values must both be null or both supplied with equal length. Paired empty arrays become native null. Nonempty generation data and custom spectrum data are mutually exclusive. Custom data is an even-length Double array of period/value pairs; periods are nonnegative and all values finite. Null/empty custom data is omitted. Typed dictionaries preserve key/value association through a single entry snapshot, rather than separate mutable key/value enumeration. Point order and duplicate periods remain unchanged; no sorting, interpolation, aggregation or unit conversion occurs.

Extended modal codes are SRSS=0, ABS=1, CQC=2, ASCE=3, TEN=4, CSM=5 and GRP=6. These differ from the legacy AddRSLoad modal codes. Request-builder helpers retain main-source update rules: WithDirection skips magnitudes at most 1e-9 without removing an earlier factor; WithDamping and WithScale replace their named entries. Nonfinite builder inputs are rejected before updates. Mutable models must remain stable during capture.

### Legacy AddRSLoad

Reused ResponseSpectrumLoadParameters and its modal, quantity, damping and interpolation enums. The request must have exactly three direction factors and at least one non-null curve point. Its six float-valued scalar fields and direction/point arrays are checked before conversion from Double to native Single. Nonfinite inputs, Single overflow and nonzero values that would underflow to zero are rejected before mutation. Ordinary Single rounding is inherent; extended-spectrum inputs retain Double precision.

The native call contains exactly nineteen arguments. Factors, periods and ordinates are independent Single arrays with a matching point count. Modal codes are Srss=1, Cqc=2, Absolute=3, Asce=4, Ten=5 and Csm=6. Integer flags retain explicit 0/1 values. Periods must be nonnegative; order and duplicate entries are preserved, including zero where supplied. Disabled option factors are still validated because their scalar slots are always passed.

All nineteen slots are by value in the inspected 2025/2026 vendor interop metadata, despite FAR-reference notation in the C++ help. Public APIs expose no ref/out. Owned arrays prevent native in-place updates from mutating caller models. The current active load case and native quantity/scale units must be appropriate; these methods do not select or create a load case or certify spectrum engineering results.

### Direct-analysis definitions

Reused DirectAnalysisParameters and DirectAnalysisParameterType. Only **Flex=0** and **Axial=2** are admitted. Metadata also contains FYLD=1, but the reviewed function contract documents only FLEX and AXIAL, so FYLD is not exposed as a supported choice.

IDs are snapshotted once, validated positive and kept in original order with duplicates. Beam overloads use IDs only. FLEX scalars are finite Double values with no engineering range normalization. AXIAL must pass exactly zero for the unused scalar. Raw selection overloads allow empty successful no-ops; typed model requests require a nonempty member list, matching the main model contract. Factories copy source IDs; the AXIAL constructor retains the source rule that canonicalizes its unused value to zero.

The add APIs define parameters only; they do not run direct analysis. The two deletion APIs mutate the definition explicitly, without existence probing, member deletion, saving or fallback operations. Boolean families return native true/false and accept evidenced integral 0/1/-1 forms; other values/types throw rather than treating unknown errors as true. Native failures/exceptions may leave effects and are not retried.

### Verification and publication boundary

Passed **2,185 offline tests**, including **161 new cases**, and a Release solution build with zero warnings/errors. XML contracts cover every new interface/adapter overload, model, constructor, helper and enum member. All **310 selected source-reference XML examples compiled and executed**, with native mutations guarded. The reusable package verifier was updated for the same inventory.

These are source additions after the published 1.0.0-preview.20261015 archive. No new NuGet archive was built or published. Native marshaling, numerical behavior, engineering-code applicability and licensed feature behavior remain live validation gates. Broader named-result APIs and semantic/polymorphic Load capability architecture remain separate planned work.

### Usage

```csharp
// On the connected session's COM thread, with the intended case active:
var spectrum = new StaadPro.Interop.Models.Loads.Seismic.ResponseSpectrumLoadExParameters()
    .WithDirection(x: 1)
    .WithDamping(0.05)
    .WithScale(9.80665);
spectrum.CustomSpectrumData.Add(
    new StaadPro.Interop.Models.Loads.Seismic.SpectrumCurvePoint(0.1, 0.2));
spectrum.CustomSpectrumData.Add(
    new StaadPro.Interop.Models.Loads.Seismic.SpectrumCurvePoint(1.0, 0.5));
bool added = wrapper.Load.AddResponseSpectrumLoadEx(spectrum);
Console.WriteLine(added);
// Native engineering/units validation and saving remain the caller's responsibility.
```

## Reference coverage status

All 115 method signatures in the main project’s IOSLoad interface are implemented, plus the repository’s five convenience overloads. No reference interface overload remains pending. Additional native functions, adapter-only conveniences and broader version-aware managed API design remain separate work; see the [cross-version coverage report](reports/OpenSTAAD-Load-Multi-Version-Architecture-and-Plan.md).
