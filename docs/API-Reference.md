# StaadPro.Interop API Reference

## `OpenStaadWrapperProvider` Members
- `Get(string fileFullPath = null)`: With a path, reuses that model if already open; otherwise opens it once (or creates a blank model when the file is missing). Polls every 250 ms for up to two minutes for the matching filename and available Geometry/Load interfaces and count calls. Returns null on failure/timeout with Trace diagnostics. Without a path, attaches to a running session or starts STAAD. Call on an STA thread.
- `GetRunning()`: Attaches to an existing active STAAD session without launching or modifying model state.

## `OpenStaadWrapper` Members

All 30 main-source wrapper properties, both constructor forms, RefreshComObjects, RefreshUnits and Dispose are ported. See [Wrapper-Porting](Wrapper-Porting.md) for complete member parity, examples and limits.

- `Root`, `Geometry`, `Load`: existing implemented adapters.
- `RawOpenStaad`, `RawGeometry`, `RawLoad`, `RawOutput`, `RawProperty`, `RawSupport`, `RawDesign`, `RawCommands`, `RawTable`, `RawView`: native bindings; RawCommands reads the native singular Command property.
- `Output`: 40 typed result queries; `Property`: 249 common overloads plus five optional catalog queries.
- `Support`, `Design`, `Commands`, `Table`, `View`: binding-only placeholders; their domain functions are not ported.
- `IdeaStatiCa`: integration placeholder when Geometry/Load/Output exist; no transformation, verification or export functions are available.
- `StaadVersion`: cached native STAAD version; each access returns a copy, null when unavailable. This is the source for native version selection.
- `ApplicationVersion`: entry/calling host assembly version; it is not the STAAD version.
- `StaadEdition`: None/V8i/Connect using the source major-version rule; not a feature-support guarantee.
- `StaadApplicationWindowHandle`: cached non-owning Connect-family window handle, using the same root by default.
- `BaseUnitSystem`, `InputForceUnit`, `InputLengthUnit`: cached unit snapshots, with documented source-compatible unit fallback.
- `IsDedicated`: caller-supplied dedication flag; no process ownership. `IsConnected`: managed binding-state check, not native readiness.
- `StaadInteropInteractionService`: captured host interaction service; default is noninteractive.
- `RefreshUnits()`: rereads only base/force/length units after native unit changes.
- `RefreshComObjects()`: rebinds adapters and rereads metadata after model reopening/database initialization; callers reacquire adapter properties.
- `Dispose()`: clears all bindings without saving/closing STAAD; refresh methods reject further use.

Construction/refresh read native metadata on the session’s COM thread. Five typed Root metadata queries now reuse wrapper snapshots. Common live analysis observation is also implemented. Root profile selection, other pending operations and optional capabilities remain separate work.

## `IOSGeometry` Members
- `CreateMultipleNodesExtended(IOSRoot openStaad, IEnumerable<Node> nodeCollection)`: Creates explicit node IDs using an XYZ coordinate matrix in model input units.
- `CreateMultipleBeamsExtended(IOSRoot openStaad, IEnumerable<Beam> beamCollection)`: Creates explicit beam IDs in order through synchronous native `CreateBeam` calls; endpoints must exist.
- `MergeNodesExtended(IOSRoot openStaad, Node retain, IEnumerable<Node> nodesToMerge)`: Merges existing IDs, requiring the retained ID in the collection. See [Geometry-Porting.md](Geometry-Porting.md) for validation and partial-failure behavior.
- `IsZUp()`: Returns whether the model uses global Z upwards.
- `GetGlobalVerticalAxis()`: Returns `GlobalVerticalAxis.Y` or `GlobalVerticalAxis.Z`.
- `CreateNode(Node)`: Creates a single node.
- `CreateBeam(Beam)`: Creates a single beam.
- `CreatePlate(Plate)`: Creates a 3-node or 4-node plate.
- `CreateAnnularParametricSurface(...)`: Creates an annular parametric surface with boundary generation.
- `CreateSolidCircularParametricSurface(...)`: Creates a circular parametric surface.
- `AddDensityPointToSurface(...)`: Adds mesh refinement density point.
- `AddDensityLineToSurface(...)`: Adds mesh density control line.
- `AddPolygonalRegionToSurface(...)`: Defines polygonal openings or refinements.
- `GetPlatesConnectedAtNode(int nodeId)`: Returns all incident plates for a node.
- `DistributeTorque(...)`: Distributes torsional load as equivalent nodal forces.

## Load expansion contracts for review

The [contract-first plan](reports/OpenSTAAD-Load-Expansion-Contracts-and-Type-Plan.md) and [signature review file](contracts/IOSLoadExpansionContract.cs) define the remaining additions and all required types. The review interface is compiled only into tests, not the NuGet assembly. New supporting request/result definitions reside under Models.Loads.Contracts; pending adapter methods are not exposed.

## Load expansion

The 36 approved additions are implemented through IOSLoad and OSLoadAdapter; total Load coverage is 171 overloads. See [all signatures and operation comparisons](Load-Expansion.md). New public signatures use named result/request classes and managed collections without ref/out or tuples. The native attribute getter is status-only and beam merging returns invocation evidence; these limits are explicit in IntelliSense.

## `IOSLoad` Members

### Completed main-interface reference coverage

All 115 main-interface reference signatures are implemented; source now exposes 171 overloads: 115 reference signatures, five conveniences, 15 additional native inspection queries and 36 Load expansion methods. See [Load inspection](Load-Inspection-Queries.md) for their status/data contracts and activation effects. The final 15 include AddSeismicDefMemberWeight, AddSeismicDefMemberUniformWeight, AddSeismicDefMemberConcentratedWeight, AddSeismicDefWallArea, AddResponseSpectrumLoadEx, AddDirectAnalysisDefinitionParameter, AddDirectAnalysisFlexParameter, AddDirectAnalysisAxialParameter, DeleteDirectAnalysisDefinitionParameter, DeleteDirectAnalysisDefinition, AddRSLoad. See [packet contracts, overload details, model reuse, side effects and native validation limits](Load-Porting.md#load-reference-completion). The latest additions are outside the published preview.


### ASCE wind and seismic definitions

The preceding source increment exposed 105 Load overloads, with 15 main-interface reference overloads then pending. The latest 15 include AddWindDefinitionASCE7Parameters, ComputeWallWindPressureProfile, ComputeWallWindPressureProfileASCE72016, AddSeismicDefinition, ModifySeismicDefinitionParam, ModifySeismicDefinitionParams, AddSeismicDefSelfWeight, AddSeismicDefJointWeight and AddSeismicDefMemberWeight. Signatures retain reference return types and defaults, with native ref handling kept internal. See [complete contracts, units, native-code distinctions and examples](Load-Porting.md#asce-wind-and-seismic-definitions). These are source additions after the published preview.


### Reference, Repeat and Temperature Assignments

- `AddReferenceLoadsTo(int loadCaseId, Dictionary<int, double> loadCaseNumberFactorPairs)`: Adds factored reference contributions to an existing primary case.
- `AddRepeatLoadsTo(int loadCaseId, IDictionary<int, double> loadCaseNumberFactorPairs)`: Adds factored repeat contributions to an existing primary case.
- `AddAxialThermalGradientTo(int loadCaseId, IEnumerable<int> entities, double axialThermalGradient)`: Synchronously passes the raw thermal input as the first native temperature argument, with transverse arguments zero.
- `AddAxialThermalGradientTo(int loadCaseId, IEnumerable<int> entities, double axialThermalGradient, int nThreads)`: Preserves the reference four-parameter signature, but executes one scalar assignment per entity on the caller's COM thread. `nThreads` must be positive and does not create workers.

Inputs are validated before activation. Empty collections make no native calls; nonempty assignments leave the primary target active, including on native assignment failure. Native assignment return values are not interpreted. See [Load-Porting.md](Load-Porting.md) for complete contracts, examples and the remaining load inventory.

### Grouped Assignments by Load Case

- `AddMemberLoadsFromGroupsByLoadCase(Dictionary<ILoadCase, List<(MemberLoad Load, List<Beam> Beams)>> grouped)`: Delegates member tuples to `AddMemberLoad`, skipping null load definitions and null/empty beam lists.
- `AddNodalLoadsFromGroupsByLoadCase(Dictionary<ILoadCase, List<(NodalLoad Load, List<Node> Nodes)>> grouped)`: Delegates every nodal tuple to `AddNodalLoad`, which rejects null loads/node lists and filters individual null nodes.

Dictionary keys select the destination case. These functions preserve the existing assignment helpers' native mapping and activation-status handling; they do not convert units, change load metadata, refresh managed collections or supply rollback. Use valid existing primary/reference cases. See [Load-Porting.md](Load-Porting.md) for empty-entry behavior and partial failures.

### Load Assignment Queries

- `GetNodalLoads(ILoadCase lc, int nId)`: Reads six-component nodal assignments into `List<NodalLoad>`.
- `GetMemberUniformlyDistributedLoads(ILoadCase lc, int mId)`: Reads UDL direction, magnitude, start/end distances, and eccentricity into `List<MemberUniformlyDistributedLoad>`.
- `GetMemberConcentratedLoads(ILoadCase lc, int mId)`: Reads point-force direction, magnitude, position, and eccentricity into `List<MemberConcentratedLoad>`.
- `GetMemberConcentratedMoments(ILoadCase lc, int mId)`: Reads assigned moment axis, magnitude, position, and eccentricity into `List<MemberConcentratedMoment>`.
- `GetMemberLinearVaryingLoads(ILoadCase lc, int mId)`: Reads local-only full-member start/end/middle intensities into `List<MemberLinearVaryingLoad>`, preserving the named fields despite the constructor's different argument order.
- `GetMemberTrapezoidalLoads(ILoadCase lc, int mId)`: Reads local/global/projected directions, endpoint intensities, and loaded-span positions into `List<MemberTrapezoidalLoad>`.
- `GetMemberUniformMoments(ILoadCase lc, int mId)`: Reads uniform moment intensity, span and eccentricity into `List<MemberUniformMoment>`.

These queries activate an existing primary/reference case and leave it active. They preserve record order and raw values without unit conversion, return an empty list for a valid zero count, and throw on failure or malformed data. See [Load-Queries.md](Load-Queries.md) for all field mappings, error contracts, COM/session requirements, examples, and verification steps.

### Load Case Discovery

- `GetAllPrimaryLoadCases()`: Returns a fresh `HashSet<ILoadCase>` with primary IDs, titles and engineering types.
- `GetAllReferenceLoadCases()`: Returns reference metadata using the reference-specific type API.

Discovery does not activate cases. Counts, ID arrays, titles and types are validated; errors throw rather than returning partial sets. HashSet ordering is unspecified. See [Load-Queries.md](Load-Queries.md#discovering-primary-and-reference-load-cases).

### Load Case Lookup

- `GetPrimaryLoadCaseFromId(int lcId)`: Reads a fresh primary `LoadCase` metadata object, returned as `ILoadCase`.
- `GetReferenceLoadCaseFromId(int lcId)`: Reads reference metadata with the reference-specific title/type APIs.
- `GetPrimaryLoadCasesFromIds(IEnumerable<int> loadCasesIds)`: Returns an eager `List<ILoadCase>` in input order, preserving duplicates as independent objects.
- `GetReferenceLoadCasesFromIds(IEnumerable<int> loadCasesIds)`: Uses the same ordered, eager batch contract with reference-specific title/type APIs.

All IDs must be positive. Batch input is enumerated once and completely validated before COM access; empty input returns a fresh empty list. Lookups leave the active case unchanged, do not enumerate cases, and throw on malformed/native-error metadata. See [Load-Queries.md](Load-Queries.md#looking-up-cases-by-id) for ownership, exceptions, examples and verification.

### Load Combination Metadata

- `GetAllLoadCombinationCases()`: Returns a fresh `HashSet<ILoadCase>` with combination IDs/titles, `CaseType=LoadCombination` and `Type=None`.
- `GetLoadCombinationCaseFromId(int lcId)`: Verifies membership in the combination ID list before reading the requested title; absent IDs throw.
- `GetLoadCombinationCasesFromIds(IEnumerable<int> lcIds)`: Returns an eager distinct set exposed as `IEnumerable<ILoadCase>`, validating all input and memberships before titles; duplicates collapse and order is unspecified.

These methods leave the active case unchanged and validate native count/number/title results. Combinations have no engineering type category, so no type API is called. Factors and constituent cases are not loaded. See [Load-Queries.md](Load-Queries.md#discovering-and-looking-up-load-combinations).

### Family Selection and Generic Lookup

- `GetAllLoadCasesOfType(LoadCaseType loadCaseType = LoadCaseType.PrimaryLoad)`: Eagerly returns the selected primary/reference/combination metadata set through `IEnumerable<ILoadCase>`.
- `GetLoadCase(int lcId, LoadCaseType loadCaseType)`: Reads the complete selected family, then returns its matching metadata object or null if the positive ID is absent.

Repeat and undefined families throw `NotSupportedException` before COM; nonpositive generic lookup IDs throw `ArgumentOutOfRangeException`. Native failures throw even when the requested ID is absent or another case fails. Both leave the active case unchanged. See [Load-Queries.md](Load-Queries.md#selecting-a-family-and-finding-a-case).

### Range, ID Filters and Lists

- `GetLoadCases(int fromId, int toId, LoadCaseType loadCaseType)`: Eagerly reads a family and returns cases in the inclusive positive ID range. Equal bounds are allowed; reversed/nonpositive bounds throw before COM.
- `GetLoadCasesFromIds(IEnumerable<int> loadCasesIds, LoadCaseType loadCaseType)`: Snapshots and validates input once, filters a complete family read, collapses duplicate requests and omits missing IDs. Empty input returns a fresh empty list without COM, after family validation.
- `GetLoadCasesOfType(LoadCaseType loadCaseType)`: Returns a concrete, fresh `List<ILoadCase>` for a supported family.

All three preserve active-case state and return eager metadata lists in unspecified order. Primary/reference/combination families are supported; repeat/undefined families throw. Unmatched metadata errors still throw during full-family reads. See [Load-Queries.md](Load-Queries.md#filtering-and-materializing-case-lists).

### Load Case Creation & Titles
- `CreateNewPrimaryLoad(string lcTitle, LoadType loadType)`: Creates a new primary load case with automatic ID assignment.
- `CreateNewPrimaryLoad(int lcId, string lcTitle, LoadType loadType)`: Creates a new primary load case with explicit ID.
- `CreateNewPrimaryLoadEx(LoadCase lc)`: Creates a primary load case from a `LoadCase` entity.
- `CreateNewPrimaryLoadEx2(LoadCase lc)`: Creates a primary load case with explicit ID from a `LoadCase` entity.
- `CreateNewReferenceLoad(int lcId, string lcTitle, LoadType loadType)`: Creates a new reference load case.
- `CreateNewReferenceLoad(LoadCase lc)`: Creates a reference load case from a `LoadCase` entity.
- `GetLoadCaseTitle(int id)`: Retrieves the string title of any load case or combination by ID.
- `SetLoadCaseActive(ILoadCase lc)`: Sets a primary or reference load case as active in the STAAD model.
- `CreateNewLoadCase(ILoadCase lc)`: Creates a primary or reference load case based on `ILoadCase.CaseType`.

### Support Settlement
- `AddSupportSettlement(ILoadCase, int nodeId, SettlementDirection, double mmSettlement)`: Applies support settlement/displacement to a single node by ID.
- `AddSupportSettlement(ILoadCase, Node, SettlementDirection, double mmSettlement)`: Applies support settlement/displacement to a single Node entity.
- `AddSupportSettlement(ILoadCase, IEnumerable<Node>, SettlementDirection, double mmSettlement)`: Applies support settlement/displacement to multiple Node entities.
- `AddSupportSettlement(ILoadCase, IEnumerable<int>, SettlementDirection, double mmSettlement)`: Applies support settlement/displacement to multiple node IDs.

### Nodal Loading
- `AddNodalLoad(ILoadCase lc, Node node, NodalLoad nl)`: Applies a 6-DOF concentrated nodal load to a single node.
- `AddNodalLoad(ILoadCase lc, IEnumerable<Node> nodes, NodalLoad nl)`: Applies a 6-DOF concentrated nodal load across multiple nodes.

### Member Loading
- `AddMemberLoad(ILoadCase lc, Beam beam, MemberLoad ml)`: Applies a member load (uniformly distributed load or concentrated point load) to a single beam element.
- `AddMemberLoad(ILoadCase lc, IEnumerable<Beam> beams, MemberLoad ml)`: Applies a member load to multiple beam elements.

### Plate Pressure Loading
- `AddPlateUniformPressure(ILoadCase lc, int plateId, double pressure, LoadDirection direction = LoadDirection.LocalZ)`: Applies uniform surface pressure to a single plate by plate ID.
- `AddPlateUniformPressure(ILoadCase lc, IEnumerable<int> plateIds, double pressure, LoadDirection direction = LoadDirection.LocalZ)`: Applies uniform surface pressure across multiple plate IDs.
- `AddPlateUniformPressure(ILoadCase lc, IEnumerable<Plate> plates, double pressure, LoadDirection direction = LoadDirection.LocalZ)`: Applies uniform surface pressure across multiple Plate entities.

### Load Envelope Metadata

| Method | Behavior |
| --- | --- |
| `HashSet<int> GetEnvelopeIds()` | Reads distinct positive native envelope IDs; a successful zero count returns a fresh empty set. |
| `ILoadEnvelopeDetail GetLoadEnvelopeDetail(int envId)` | Reads the classification, reported count and independent membership set for a positive envelope ID. |
| `HashSet<ILoadEnvelopeDetail> GetAllLoadEnvelopeDetails()` | Eagerly reads all envelopes after one ID discovery; throws on any failure without returning partial results. |

Each method preserves active-case/load-list state and checks native fetch success **1**, counts, array dimensions, positive IDs and duplicate IDs. Valid zero-member envelopes are retained with an empty set. `LoadEnvelopType` preserves native values 0–6 and the reference spelling. See the [full envelope guide](Load-Queries.md#load-envelope-queries) for examples, errors, ownership and native validation.

### Batch Load Case Creation
- `CreatePrimaryLoadCases(HashSet<ILoadCase> / IEnumerable<ILoadCase>)`: Batch creates primary load cases in the active model.
- `CreateReferenceLoadCases(HashSet<ILoadCase> / IEnumerable<ILoadCase>)`: Batch creates reference load cases in the active model.

### Load Case Clearing
- `ClearPrimaryLoadCase(ILoadCase)`: Clears a single primary load case.
- `ClearPrimaryLoadCase(ILoadCase, bool isReferenceLoad)`: Clears a primary load case with reference load flag.
- `ClearPrimaryLoadCases(IEnumerable<ILoadCase>)`: Clears multiple primary load cases.
- `ClearPrimaryLoadCases(IEnumerable<ILoadCase>, bool isReferenceLoad)`: Clears multiple primary load cases with reference flag.
- `ClearPrimaryLoadCases(IEnumerable<int>)`: Clears primary load cases by IDs.
- `ClearPrimaryLoadCases(IEnumerable<int>, bool isReferenceLoad)`: Clears primary load cases by IDs with reference flag.
- `ClearReferenceLoadCase(ILoadCase)`: Clears a single reference load case.
- `ClearReferenceLoadCases(IEnumerable<ILoadCase>)`: Clears multiple reference load cases.
- `ClearReferenceLoadCases(IEnumerable<int>)`: Clears reference load cases by IDs.

### Self-Weight, Envelope Creation and Load Lists

| Method | Native behavior |
| --- | --- |
| `AddSelfweightInXYZTo(int loadCaseId, LoadCaseType lcType, IEnumerable<int> entities, SelfWeightDirection loadDirection = GlobalY, double factor = -1)` | Checked primary/reference activation followed by one geometry self-weight assignment; empty input skips native access. |
| `CreateLoadEnvelopExt(int envelopId, LoadEnvelopType envelopType, IEnumerable<int> loadCaseList)` | One checked native envelope creation; no activation or read-back. |
| `CreateLoadList(LoadListType loadListType, IEnumerable<int> loadCaseIds)` | Native load-case/envelope-list selection, returning its original integer-zero success status. |

Inputs are snapshotted and validated before native access. The first two mutation APIs use Boolean success; load-list selection uses integer zero. Native existence/suitability and empty-envelope/list behavior remain native decisions. See [Load-Porting.md](Load-Porting.md) for input, empty-result, failure and state-change contracts.

## Root Adapter

`OpenStaadWrapper.Root` exposes `IOSRoot`, implemented by the existing non-partial `OSRootAdapter`.

The metadata queries below reuse cached wrapper values. RefreshUnits updates unit snapshots; RefreshComObjects updates native version and handle snapshots. Standalone OSRootAdapter bindings read their supplied native root on each call through shared internal readers. Native unit failures can use the documented compatibility fallback; these enums do not certify measured units. Metadata access through an adapter captured from a disposed wrapper throws ObjectDisposedException.

| Metadata query | Return / meaning |
| --- | --- |
| `GetApplicationVersion()` | `ApplicationVersion`: defensive copy of native StaadVersion; null when absent, distinct from the host ApplicationVersion. |
| `GetBaseUnit()` | `BaseUnitSystem`: Unknown for unavailable values; unrecognized integral codes remain inspectable. |
| `GetInputUnitForForce()` | `ForceInputUnit`: cached normalized value; fallback KiloPound for Imperial, KiloNewton otherwise. |
| `GetInputUnitForLength()` | `LengthInputUnit`: cached normalized value; fallback Feet for Imperial, Meter otherwise. |
| `GetMainWindowHandle()` | `IntPtr`: borrowed handle, possibly zero, with the source-family rule and configured wrapper service; no live-window guarantee. |

| Method | Result |
| --- | --- |
| `string GetStaadFileFolder()` | One native directory read; exact text and explicitly written empty output preserved. |
| `bool IsPhysicalModel()` | One live native classification predicate; Boolean or integral 0/1 only. |
| `string GetErrorMessage()` | One explicit exact-text native message read; no promise that reading preserves the server error state. |
| `void SetFullJobInfo(...)` | Thirteen non-null strings in native order; literal date text, no automatic save or date serialization. |
| `void Analyze()` | One native UI-equivalent analysis request; normal return does not establish completion or success. |
| `void AnalyzeModel(AnalysisEngine engine = AnalysisEngine.StaadPro)` | One validated native engine request (-1); verified void contract. |
| `ConnectedProjectResult GetConnectedProjectInfo()` | Native query flag and immutable project data only after success and written string outputs. |
| `bool SetConnectedProjectInfo(string projectId, string projectName)` | Validated nonblank text; exact native assignment flag, without cloud or persistence verification. |
| `bool SetConnectedProjectInfo(ConnectedProjectInfo projectInfo)` | Reuse immutable project data, validate and delegate without another query. |
| `NativeProcessHandleValue GetProcessHandle()` | Opaque immutable UInt64 bit pattern; no ownership, native width or caller-process HANDLE validity implied. |
| `void NewSTAADFile(string filePath, LengthInputUnit lengthUnit, ForceInputUnit forceUnit)` | Validated new .std destination and unnamed session; one native request, with no atomic no-overwrite/completion guarantee. |
| `void CreateNamedView(string viewName, int creationFlag)` | Name validation, required caller-established Int32 flag and inspected two-input void native call. |
| `void SaveNamedView(string viewName)` | Name validation and inspected one-input void native request; no fabricated error output. |
| `void Quit()` | Explicit bound-application shutdown; no automatic save, wrapper disposal or process kill. |
| `void RemoveNamedView(string viewName)` | Inspected two-input native request; zero initializes the by-value error slot without fabricating a status. |
| `void ModifyNamedView(string viewName, IEnumerable<int> entityIds, NamedViewEntityType entityType, int modificationFlag)` | Validated private positive-ID Int32 array; inspected four-input call with a caller-established mode. |
| `MemberForcesResult RetrieveMemberForces(IEnumerable<int> beamIds, IEnumerable<int> loadCaseIds)` | Bound native availability and ordered per-end success/data; source-compatible force records without unit conversion. |
| `uint GetProcessId()` | Same-root positive UInt32 PID query; shared strict validation, no process enumeration or liveness guarantee. |
| `void SetInputUnitForLength(LengthInputUnit lengthUnit)` | Independent length request; codes 0–7 including unnamed feet alias 1; explicit wrapper refresh required for fresh caches. |
| `void SetInputUnitForForce(ForceInputUnit forceUnit)` | Independent validated force request; no automatic length request, save or cache refresh. |
| `void SetShortJobInfo(string jobName, string jobClient, string engineerName)` | Submit three non-null strings in native order; preserve text, no automatic save. |
| `void SetShortJobInfo(ShortJobInfo jobInfo)` | Snapshot Name/Client/Originator from the existing model, validate, then delegate. |
| `void UpdateStructure()` | One explicit native update request; no added save, analysis, refresh or completion verification. |
| `bool IsAnalyzing()` | One live native running-state read; Boolean or integral 0/1 only. False does not indicate analysis success. |
| `AnalysisStatusResult GetAnalysisStatus(string modelFilePath)` | Normalized explicit-path query; immutable native/typed status, success, completion and nullable diagnostic counts/CPU seconds. |
| `AnalysisStatusResult GetAnalysisStatus()` | One current filename read, then delegates; unnamed models throw before status access. |
| `string GetStaadFileFullPath()` | Exact current model path from one void `GetSTAADFile` output read. |
| `string GetStaadFileNameOnly()` | Windows filename including its extension, derived from one path read. |
| `string GetAnlFileFullPath()` | Same directory and model stem with uppercase `.ANL`, derived from one path read. |
| `string GetOpenedStaadFilename()` | Normalized absolute opened-model path from one native read; empty text remains empty. |
| `bool IsTarget(string staadFileFullPath)` | Checks complete normalized path identity with ordinal case-insensitive comparison. |
| `bool ModelIsOpen(string fileFullPath)` | The same full-path identity check; equal basenames in different folders do not match. |
| `ShortJobInfo GetShortJobInfo()` | Fresh name/client/originator metadata; all native BSTR outputs must be written strings. |
| `FullJobInfo GetFullJobInfo()` | Fresh thirteen-field metadata with culture-aware dates and DateTime.MinValue for unspecified dates. |
| `IOSRoot SetInputUnitsEx(LengthInputUnit lengthUnit, ForceInputUnit forceUnit)` | Validates enum values, sends length/force integer codes to native SetInputUnits and returns this adapter. |
| `bool SetSilentMode(SilentMode silentMode)` | Sends validated native code 0/1; true means call completion, false means dispatch threw. The native previous setting is ignored. |
| `IOSRoot SaveModel(SilentMode silentMode)` | Sends validated code 0/1 to the native void save API and returns this adapter; no file-persistence verification. |
| `IOSRoot CloseSTAADFile()` | Requests native model close once and returns this adapter; no explicit save, silent-mode change, application termination or close verification. |

| `IOSRoot OpenSTAADFile(string filePath)` | Validated absolute path to one native open request; returns this adapter; Native dialogs apply; no readiness verification |
| `IOSRoot ShowApplication()` | One native visibility request; returns this adapter; No model or visibility verification |

| `bool AwaitModelHandle(string fileFullPath, double timeoutDurationInSeconds = 60, double sleepIntervalInSeconds = 0.1)` | Poll exact normalized path with monotonic timeout; Blank is a nonmatch; no model opening |
| `bool OpenStaadModel(string staadFileFullPath, int sleepInterval, int maxWaitTime = 600)` | One open request then full-path polling; milliseconds interval, seconds limit; Timeout does not undo native opening |

| `void CloseCurrentStaadModel()` | Read filename; if non-whitespace, enable silent mode and request close; Blank/whitespace skips mutation; no close verification |
| `bool CloseWriteReopenStaadFile(string filePath, List<string> fileContents, int sleepTimeMilliseconds = 200, int maxTimeMilliseconds = 5000)` | Close matching target, await blank/unlocked state, atomically replace then request reopen; False for mismatch, wait expiry or intervening switch; partial effects possible |

| `void MinimizeStaadMainWindow()` | Validate native window/PID then post ShowWindowAsync command 6; No model mutation or final-state verification |
| `void MaximizeStaadMainWindow()` | Validate native window/PID then post ShowWindowAsync command 3; No model mutation or final-state verification |

| `void RestoreStaadMainWindow()` | Validate native window/PID and post ShowWindowAsync command 9; Accepted request does not verify final state |
| `AnalysisResult AnalyzeEx(SilentMode silentMode, HiddenMode hiddenMode, WaitAnalysisMode waitMode)` | Validate/send native silent, hidden, wait codes; preserve integer analysis status; Starts native analysis; wait mode can block |

Filename/identity queries preserve empty native filenames and perform no native writes. The job getters validate all outputs and return editable managed snapshots; malformed dates throw. The unit setter changes native input-unit state and does not rescale managed values, save or roll back. Silent/save/close operations can mutate native state, write files or display dialogs; no rollback or verified unattended completion is provided. See [Root-Porting.md](Root-Porting.md) for complete native mappings, date/empty/error behavior, examples and the completed private-reference helper. Private-source coverage is 23 of 23. IOSRoot exposes 53 common methods; three separately acquired operation interfaces complete all 39 documented native functions (56 managed operations in total). See the [analysis query guide](Root-Analysis-Queries.md), [common query guide](Root-Common-Queries.md), [model setup guide](Root-Model-Setup.md), [job/analysis/project guide](Root-Job-Analysis-Projects.md), [creation/view/lifecycle guide](Root-Creation-Views-and-Lifecycle.md) and [managed API rules](Root-Managed-API-Design.md). Custom `IOSRoot` implementations must implement the added members.

AnalysisEngine is in `StaadPro.Interop.Enums.Root`. ConnectedProjectResult and ConnectedProjectInfo are in `StaadPro.Interop.Models.Root`; their properties are immutable. A false project query returns null Project, while native success requires validated string outputs. The assignment overload rejects null/blank fields before native access.

NativeProcessHandleValue is in `StaadPro.Interop.Models.Root`. It exposes immutable numeric data, not an owning or verified Windows handle. CreateNamedView/SaveNamedView follow inspected 2025/2026 managed interop declarations; their help pages contain inconsistent error-output arguments. Their void return does not establish success.

Optional access uses `root.Capabilities.GetAdinaExport()`, `GetAnalysisErrorMessages()` and `GetAnalysisWarningMessages()`. Successful acquisitions return IOSRootAdinaExport, IOSRootAnalysisErrorMessages or IOSRootAnalysisWarningMessages respectively. Each exposes its typed operation and status/data result. Unsupported results contain no adapter; exact candidate profiles and metadata checks are not runtime certification. See the [completion and capability guide](Root-Completion-and-Capabilities.md).

## Output result queries

IOSOutput and OSOutputAdapter expose 40 typed queries covering analysis/buckling availability, nodal displacement, support reactions, local member-end forces, static checks, seven plate-center result families and member design section/ratio observations. Each returns OutputQueryResult&lt;T&gt; with explicit status and owned data; inspect IsSuccessful before Data. No public reference outputs or tuples are used.

See the [Output API guide](Output-Porting.md) for all signatures, payloads, native flags, axes/units, sentinels, examples, threading/lifetime rules and the 20 pending native mappings. These are source additions after the published preview; live native qualification remains pending.

## Property observations

The current `IOSProperty` / `OSPropertyAdapter` surface contains 25 read-only queries. Every query returns `PropertyQueryResult<T>`; inspect `IsSuccessful` before accessing `Data`. No public reference outputs or tuples are used. See the [complete signature and behavior table](Property-Porting.md#implemented-apis-and-choosing-between-them) for member releases, member/plate offsets, section properties, material names/constants, isotropic observations and orthotropic observations.

Existing `MemberEnd` and `Vector3D` types are reused. New named results distinguish native success, missing entities, absent specifications, unavailable members, collection limits and changed counts. Native units, signs and array ordering are preserved. These methods do not assign properties, convert units, run analysis or certify result freshness. The documented-but-metadata-absent plate property reference query reports `Unsupported` when its exact member is unavailable.

## Extended Output and final documented Property cleanup

Implemented **25 additional documented Output routes**, bringing IOSOutput and the single non-partial OSOutputAdapter to **40 typed queries covering 40/60 native names**, with **20 documented routes pending**. Added member/physical-member results, solid stresses, mat/base-pressure snapshots, modal/buckling observations, nonlinear load steps, steel-design observations and time-history queries. See [the extended contracts and comparative usage guide](Output-Extended-Queries.md).

The selected contracts are shared or cosmetic across the installed 2024–2026 references. One internal common reader performs native dispatch and validation; direction-code policies, uncertain member-end layouts, cut/point buffers and incompatible scalar-output transports remain separate work. No new public ref/out, tuples or raw VARIANT packets are exposed. Existing wrapper/provider behavior is unchanged.

Validation passed **5,353 offline tests**, including 165 new Output cases and 32 Property completion cases. The isolated installed-package consumer verifies **1,002 selected XML examples**, including all 80 Output examples and 510 Property/capability examples. Live native marshaling and engineering qualification remain pending.

Implemented the final four safely specified P10 operations: `RemoveAllElementNodeReleaseSpec`, `RemoveMemberFireProofingSpecFromBeam`, `GetPublishedProfileName` and `GetStaadProfileName`. Property now exposes **249 common overloads plus five separately acquired catalog queries**, covering **195 of 204 active documented native routes**. P10 is complete at 24/24. The remaining **nine P7/P8 contract gates** require transport, status or context evidence; they are not implemented by guessing.

The increment deliberately excludes the **fifteen supplemental Property discovery candidates**. **Reminder checkpoint:** present those candidates and the nine documented gates before further Property expansion or the next public release, and obtain separate approval for supplemental work. See [the four complete contracts and remaining gates](Property-Profile-Names-and-Final-Cleanup.md).

The subsequent domain increment implements **25 Output routes**, bringing typed Output coverage to **40/60**. Validation passed **5,353 offline tests** with zero failures; package verification covers **1,002 selected XML examples**. These checks use managed native-signature fixtures; live 2024–2026 COM and engineering qualification remain pending.
