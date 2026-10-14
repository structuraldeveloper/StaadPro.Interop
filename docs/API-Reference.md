# StaadPro.Interop API Reference

## `OpenStaadWrapperProvider` Members
- `Get(string fileFullPath = null)`: With a path, reuses that model if already open; otherwise opens it once (or creates a blank model when the file is missing). Polls every 250 ms for up to two minutes for the matching filename and available Geometry/Load interfaces and count calls. Returns null on failure/timeout with Trace diagnostics. Without a path, attaches to a running session or starts STAAD. Call on an STA thread.
- `GetRunning()`: Attaches to an existing active STAAD session without launching or modifying model state.

## `OpenStaadWrapper` Members
- `Geometry`: Managed `IOSGeometry` interface.
- `Load`: Managed `IOSLoad` interface.
- `RawOpenStaad`: Root OpenSTAAD COM object.
- `Root`: Minimal `IOSRoot` binding for extended geometry methods; cleared on disposal.
- `RawGeometry`: OpenSTAAD Geometry COM object.
- `RawLoad`: OpenSTAAD Load COM object.
- `IsDedicated`: Indicates if the instance is dedicated to the caller.
- `IsConnected`: Indicates whether a valid STAAD COM instance is attached.
- `BaseUnitSystem`: Active model base unit system (`Imperial`, `Metric`, `Unknown`).
- `InputForceUnit`: Active force unit.
- `InputLengthUnit`: Active length unit.

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

## `IOSLoad` Members

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

| Method | Result |
| --- | --- |
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

Filename/identity queries preserve empty native filenames and perform no native writes. The job getters validate all outputs and return editable managed snapshots; malformed dates throw. The unit setter changes native input-unit state and does not rescale managed values, save or roll back. Silent/save/close operations can mutate native state, write files or display dialogs; no rollback or verified unattended completion is provided. See [Root-Porting.md](Root-Porting.md) for complete native mappings, date/empty/error behavior, examples and the remaining 5 reference methods. Root coverage is 18 of 23 methods. Custom `IOSRoot` implementations must implement the added members.
