# Load API porting and new assignments

The reference `IOSLoad` contains 115 method overloads. This repository now exposes 78: 73 matching reference overloads plus five repository-specific convenience overloads. There are 42 reference overloads still pending. Counts treat overloads separately and compare both interface and adapter method signatures.

The repository-specific methods are `CreatePrimaryLoadCases`, `CreateReferenceLoadCases` and three `AddPlateUniformPressure` overloads. They explain why subtracting 78 from 115 does not give the pending reference count.

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

Each new method has identical expanded XML on `IOSLoad` and `OSLoadAdapter`, including inputs, native mapping, empty input, activation side effects, exceptions and executable examples. The XML ships beside the DLL and does not require source access. The current package includes documentation for 78 load overloads and verifies all 123 XML examples across the library, including envelope reads, both thermal signatures and the grouped methods.

Offline tests check paired arrays, double precision, one thermal input snapshot, caller-thread scalar assignments, primary/reference grouped routing, member subtype dispatch, all six nodal components, skipped/null/empty group entries, unchanged managed data, activation ordering/failure and partial native failures. Installed-package verification compiles and runs the interface and concrete XML examples and checks argument layouts using managed native stand-ins. Live native mutations, engineering units and version-specific behavior remain unverified for these additions.

## Pending reference overloads

The following inventory was compared by method name, generic arity and parameter types. It lists source signatures, including supporting types that may themselves need porting. A name already present here can still appear when a different overload is pending.

- `void PopulateMemberUniformlyDistributedLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)`
- `void PopulateMemberUniformMoments(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)`
- `IEnumerable<Node> PopulateNodalLoads(IEnumerable<Node> forNodes, IEnumerable<ILoadCase> fromLoadCases)`
- `int AddWindDefinition(int typeNo, string typeName)`
- `int AddWindExposure(int typeNo, double exposureFactor, IEnumerable<int> nodeIds)`
- `int AddWindExposure(int typeNo, double exposureFactor, IEnumerable<Node> nodes)`
- `int AddWindExposure(int typeNo, double exposureFactor, int nodeId)`
- `int AddWindIntensity(int typeNo, double intensity, double height)`
- `int AddWindIntensity(int typeNo, IEnumerable<double> intensities, IEnumerable<double> heights)`
- `int AddWindIntensity(int typeNo, IEnumerable<WindIntensityPair> pairs)`
- `int DeleteWindDefinition(int typeNo = 0)`
- `int DeleteAllWindDefinitions()`
- `int AddWindDefinitionASCE7Parameters(ASCE7WindDefinitionParameters parameters)`
- `int ComputeWallWindPressureProfile(WallWindPressureProfileParameters parameters)`
- `int ComputeWallWindPressureProfileASCE72016(ASCE72016WallWindPressureProfileParameters parameters)`
- `bool AddSeismicDefinition(SeismicCode code, bool considerAccidentalTorsion = false)`
- `bool AddSeismicDefinition(int code, int accidentalTorsion = 0)`
- `int ModifySeismicDefinitionParam(string parameterName, double value)`
- `int ModifySeismicDefinitionParams(IDictionary<string, double> parameters)`
- `int ModifySeismicDefinitionParams(IS1893_2016SeismicParameters parameters)`
- `int ModifySeismicDefinitionParams(IS1893_2002SeismicParameters parameters)`
- `int ModifySeismicDefinitionParams(IBCSeismicParameters parameters)`
- `bool AddSeismicDefSelfWeight(double weightFactor = 1.0)`
- `int AddSeismicDefJointWeight(double weight, IEnumerable<int> nodeIds)`
- `int AddSeismicDefJointWeight(double weight, IEnumerable<Node> nodes)`
- `int AddSeismicDefJointWeight(double weight, int nodeId)`
- `bool AddSeismicDefMemberWeight(int seismicCode, SeismicMemberLoadType loadType, double weight, double startDistance, double endDistance, IEnumerable<int> memberIds)`
- `bool AddSeismicDefMemberWeight(int seismicCode, SeismicMemberLoadType loadType, double weight, double startDistance, double endDistance, IEnumerable<Beam> members)`
- `bool AddSeismicDefMemberUniformWeight(double weight, IEnumerable<int> memberIds, double startDistance = 0, double endDistance = 0, int seismicCode = 0)`
- `bool AddSeismicDefMemberConcentratedWeight(double weight, double distance, IEnumerable<int> memberIds, int seismicCode = 0)`
- `int AddSeismicDefWallArea(int typeNo, string direction, IEnumerable<double> dimensions)`
- `int AddSeismicDefWallArea(int typeNo, SeismicWallDirection direction, IEnumerable<SeismicWallDimension> walls)`
- `bool AddResponseSpectrumLoadEx(ResponseSpectrumLoadExParameters parameters)`
- `bool AddResponseSpectrumLoadEx(int rsaCode, int rsaCombination, string[] set1Names, double[] set1Vals, string[] set2Names = null, double[] set2Vals = null, double[] customDataPairs = null)`
- `bool AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType paramType, IEnumerable<int> memberIds, double param = 0.0)`
- `bool AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType paramType, IEnumerable<Beam> members, double param = 0.0)`
- `bool AddDirectAnalysisFlexParameter(double flexFactor, IEnumerable<int> memberIds)`
- `bool AddDirectAnalysisAxialParameter(IEnumerable<int> memberIds)`
- `bool AddDirectAnalysisDefinitionParameter(DirectAnalysisParameters parameters)`
- `bool DeleteDirectAnalysisDefinitionParameter(DirectAnalysisParameterType paramType)`
- `bool DeleteDirectAnalysisDefinition()`
- `bool AddRSLoad(ResponseSpectrumLoadParameters parameters)`
