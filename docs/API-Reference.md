# StaadPro.Interop API Reference

## `OpenStaadWrapperProvider` Members
- `Get(string fileFullPath = null)`: Acquires an `OpenStaadWrapper` by querying the Running Object Table (ROT), attaching to the active instance, opening the file, or starting STAAD.
- `GetRunning()`: Attaches to an existing active STAAD session without launching or modifying model state.

## `OpenStaadWrapper` Members
- `Geometry`: Managed `IOSGeometry` interface.
- `Load`: Managed `IOSLoad` interface.
- `RawOpenStaad`: Root OpenSTAAD COM object.
- `RawGeometry`: OpenSTAAD Geometry COM object.
- `RawLoad`: OpenSTAAD Load COM object.
- `IsDedicated`: Indicates if the instance is dedicated to the caller.
- `IsConnected`: Indicates whether a valid STAAD COM instance is attached.
- `BaseUnitSystem`: Active model base unit system (`Imperial`, `Metric`, `Unknown`).
- `InputForceUnit`: Active force unit.
- `InputLengthUnit`: Active length unit.

## `IOSGeometry` Members
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

All IDs must be positive. Batch input is enumerated once and completely validated before COM access; empty input returns a fresh empty list. Lookups leave the active case unchanged, do not enumerate cases, and throw on malformed/native-error metadata. See [Load-Queries.md](Load-Queries.md#looking-up-cases-by-id) for ownership, exceptions, examples and verification.

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
