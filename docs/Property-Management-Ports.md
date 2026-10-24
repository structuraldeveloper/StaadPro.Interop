# Property creation, assignments and managed population

This guide records the preceding management increment. Property now exposes 125 overloads; see [the material and release workflows](Property-Workflows.md) and [the latest section completion](Property-Sections.md).


This increment adds 25 main-project methods to IOSProperty and the single OSPropertyAdapter. Twenty-one are main-interface methods; the four material-assignment subset overloads were public adapter methods in the main project. Return types, parameter names/order and optional defaults are preserved after namespace adaptation. Private catalog and unit-library dependencies are not imported.

At that management-port checkpoint there were 75 Property overloads: 25 typed observations and 50 main-project compatibility ports. Ten additional native names have routes, giving 54 of 204 active candidates and 150 pending. This is dispatch coverage, not live qualification.

## Shared rules

Executes synchronously on the caller's established COM apartment and shares this adapter's serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.

Creation and assignment methods change native state. Examples assume a disposable test model and matching existing material/entity/property IDs. Package verification runs examples only against managed stand-ins. Staged population methods update caller-owned Beam objects after all native reads succeed; they do not modify STAAD. Mutating native batches can partially complete and have no transaction or rollback.

## Supporting packets

PlateElementThickness retains Id, A/B/C/D, AssignedPlates, constructors, array copying and factories. OffsetSpecification retains Id, Location, Axis, X/Y/Z, Beams, the zero threshold, equality and Y factories. Their private UnitsConverter helpers remain deferred; the package performs no implicit conversion. Offset packets are mutable: changing definition values while in a HashSet invalidates hash membership. Beam.StartOffset/EndOffset are managed references excluded from JSON to avoid assignment cycles. Country and TypeSpecification retain the original numeric codes; catalog support is still native-controlled.

## Deliberate contract corrections

- Interprets native returns per operation: GetThicknessPropertyValues requires 4; AssignPlateThickness and AssignMemberSpecToBeam require 0; AssignBetaAngle requires true/1.
- Uses bounded, sentinel-initialized buffers and validates actual output shape/type/content. Native failures never become populated zero defaults.
- Corrects the source bulk offset null guard. Null/empty creation sets fail before COM; null/empty assignment sets remain no-ops.
- Keeps zero/no-specification compatibility offsets independent with the requested end/frame, rather than returning a mutable shared global-start None packet. Typed queries remain available when absence must be distinguished from zero.
- Retains nThreads (1-512) without worker-thread COM dispatch. Calls use the established caller apartment and adapter lock.
- Copies creation arrays and stages assignment IDs. The Metres parameter suffix is retained for source compatibility; callers must verify the acquired native creation units. No unit switch/conversion is hidden.

## 1. DeleteMaterial

```csharp
bool DeleteMaterial(string mName)
```

Deletes a native material definition by its exact name.

- `mName`: Nonblank existing native material name; case and whitespace are passed unchanged.

**Returns:** The native success flag: true for deletion and false for native refusal or failure; no cause is inferred from false.

When to use: Use only when a material should be removed from the model. This mutates native state. Unlike material queries this does not retrieve or reassign entities, and it does not save. Boolean or documented integral 0/1 evidence is decoded without treating arbitrary nonzero codes as success.

Native routes: `DeleteMaterial`.

```csharp
System.Console.WriteLine(property.DeleteMaterial("UNUSED_STEEL"));
```

## 2. CreateBeamPropertyFromTable

```csharp
int CreateBeamPropertyFromTable(Country country, string sectionName, TypeSpecification typeSpec, double additionalSpec1, double additionalSpec2)
```

Creates a beam section definition from an installed native section table.

- `country`: Named table selector with the preserved main-project integer code; database availability is native-controlled.
- `sectionName`: Exact nonblank native database section designation.
- `typeSpec`: Preserved native section modifier. The reviewed function documents ST, D and T; other named codes require acquired-build support.
- `additionalSpec1`: Finite extra specification in the native creation length convention; clear spacing for a double profile, otherwise as defined by STAAD.
- `additionalSpec2`: Finite second extra specification; the reviewed contract requests 0.0.

**Returns:** The native signed integral result unchanged. Only a positive result is a created property ID; zero is a documented creation failure. Never assign a nonpositive result.

When to use: Use for a table-defined section and call SetMaterialName with the intended existing material immediately beforehand. Does not choose or create material, assign the section, convert dimensions or save. The enum is a catalog of codes, not a per-build capability check. Prefer the composite helper for TC/BC/TB cover plates.

Native routes: `CreateBeamPropertyFromTable`.

```csharp
property.SetMaterialName("STEEL");
int id = property.CreateBeamPropertyFromTable(Country.Indian, "ISMB300", TypeSpecification.ST, 0.0, 0.0);
System.Console.WriteLine(id);
```

## 3. CreateCompositeBeamPropertyFromTable

```csharp
int CreateCompositeBeamPropertyFromTable(Country country, string baseStaadName, TypeSpecification typeSpecification, double[] plateValuesMetres)
```

Creates a rolled-table section with top, bottom or both cover plates.

- `country`: Preserved native table country selector.
- `baseStaadName`: Exact nonblank rolled base section name in that table.
- `typeSpecification`: TC for top, BC for bottom, or TB for both; all other values are rejected.
- `plateValuesMetres`: Owned-copy input: exactly two positive finite values [width, thickness] for TC/BC, or four [top width, top thickness, bottom width, bottom thickness] for TB. The source parameter name is retained; values pass unchanged and must match the acquired creation-unit convention.

**Returns:** The signed native result unchanged. Positive values are created property IDs; 0, -6004, -6005 and -6006 represent documented creation/database/type failures.

When to use: Use after SetMaterialName for an existing intended material. Calls CreateBeamPropertyFromTableWithCoverPlates with the supplied TC/BC/TB code; BC is never substituted with TC. The wrapper does not set units, infer plate geometry, assign members or save. Do not infer the native unit convention solely from the Metres parameter suffix.

Native routes: `CreateBeamPropertyFromTableWithCoverPlates`.

```csharp
property.SetMaterialName("STEEL");
int id = property.CreateCompositeBeamPropertyFromTable(Country.Indian, "ISMB300", TypeSpecification.BC, new[] { 0.2, 0.01 });
System.Console.WriteLine(id);
```

## 4. CreateBuiltUpBeamProperty

```csharp
int CreateBuiltUpBeamProperty(string designation, double[] propertyValuesMetres)
```

Creates a fabricated or tapered I-section from seven explicit plate dimensions.

- `designation`: Source-compatible display designation parameter. The main tapered-I path derives its designation; this argument is not sent to STAAD or used to rename the definition.
- `propertyValuesMetres`: Exactly seven finite positive values: start depth, web thickness, end depth, top flange width, top flange thickness, bottom flange width, bottom flange thickness. Copied and passed unchanged; verify the acquired native creation units despite the retained Metres suffix.

**Returns:** The native signed integral result unchanged: a positive property ID, zero for creation failure, or a native error such as -106/-108. Nonpositive results must not be assigned.

When to use: Use when no rolled table lookup is needed. Dispatches the same CreateTaperedIProperty route as the main TaperISection without importing its private catalog graph. Equal start/end depths describe a prismatic fabricated I-section. Set the intended existing material beforehand. No engineering adequacy check, unit conversion, member assignment or save is performed.

Native routes: `CreateTaperedIProperty`.

```csharp
property.SetMaterialName("STEEL");
System.Console.WriteLine(property.CreateBuiltUpBeamProperty("Fabricated I", new[] { 0.6, 0.01, 0.6, 0.25, 0.02, 0.25, 0.02 }));
```

## 5. AssignBetaAnglesToBeams

```csharp
IOSProperty AssignBetaAnglesToBeams(HashSet<Beam> beams)
```

Assigns each supplied beam its managed beta angle in degrees.

- `beams`: Non-null set of non-null beams with positive analytical IDs and finite BetaAngle values in degrees. The set and angles are snapshotted before dispatch.

**Returns:** The same Property adapter for fluent chaining, after all requested operations complete.

When to use: Use to write Beam.BetaAngle values into STAAD; PopulateBetaAngles reads in the opposite direction. Calls AssignBetaAngle once per beam and requires native true/1; false/0 fails. Empty sets are valid no-ops. Stops at the first failure and does not roll back earlier assignments. The degree argument is retained without copying the unsafe worker-thread COM dispatch of the reference implementation.

Native routes: `AssignBetaAngle`.

```csharp
var beams = new System.Collections.Generic.HashSet<Beam> { new Beam { Id = 17, BetaAngle = 25.0 } };
property.AssignBetaAnglesToBeams(beams);
```

## 6. AssignBetaAnglesToBeams

```csharp
IOSProperty AssignBetaAnglesToBeams(HashSet<Beam> beams, int nThreads)
```

Assigns each supplied beam its managed beta angle in degrees.

- `beams`: Non-null set of non-null beams with positive analytical IDs and finite BetaAngle values in degrees. The set and angles are snapshotted before dispatch.
- `nThreads`: Source-compatible degree argument, 1 through 512. Native calls remain synchronous and serialized on the caller COM apartment; no worker COM calls are created.

**Returns:** The same Property adapter for fluent chaining, after all requested operations complete.

When to use: Use to write Beam.BetaAngle values into STAAD; PopulateBetaAngles reads in the opposite direction. Calls AssignBetaAngle once per beam and requires native true/1; false/0 fails. Empty sets are valid no-ops. Stops at the first failure and does not roll back earlier assignments. The degree argument is retained without copying the unsafe worker-thread COM dispatch of the reference implementation.

Native routes: `AssignBetaAngle`.

```csharp
var beams = new System.Collections.Generic.HashSet<Beam> { new Beam { Id = 17, BetaAngle = 25.0 } };
property.AssignBetaAnglesToBeams(beams, 2);
```

## 7. PopulateBetaAngles

```csharp
IOSProperty PopulateBetaAngles(IEnumerable<Beam> forBeams)
```

Reads native beta angles into the supplied managed beam objects.

- `forBeams`: Non-null finite sequence of non-null positive-ID beams. Equal beams are queried once; original instances are retained.

**Returns:** The same Property adapter for fluent chaining, after all requested operations complete.

When to use: Use before inspecting Beam.BetaAngle. Stages all successful finite observations before updating the supplied objects, so a native read failure leaves their angles unchanged. Changes managed beam angles/local-axis caches only; no STAAD assignments, save or analysis. Native -3001 is a failure to populate, not an angle.

Native routes: `GetBetaAngle`.

```csharp
var beams = new[] { new Beam { Id = 17 } };
property.PopulateBetaAngles(beams);
System.Console.WriteLine(beams[0].BetaAngle);
```

## 8. GetIsotropicMaterialAssignedBeams

```csharp
HashSet<Beam> GetIsotropicMaterialAssignedBeams(string mName, IEnumerable<Beam> allBeams)
```

Finds supplied or hydrated beams assigned to one isotropic material.

- `mName`: Exact nonblank material name; passed unchanged, not a list index.
- `allBeams`: Non-null sequence of non-null positive-ID candidates. Results reference the first supplied object matching each native ID.

**Returns:** A new mutable set of matching Beam references; empty when native assignment count is zero. Native IDs absent from the candidates are not fabricated.

When to use: Use the candidate overload when geometry is already loaded, or the Geometry overload for full hydration. Bounds native counts to 100000, validates positive Int32 IDs and exact array shape, then rechecks positive counts. Zero skips fetching. The member count follows native purpose and main usage despite an inconsistent help return table; the member list requires integral zero success. No entity assignments or native edits occur. Geometry must refer to the same model; the wrapper cannot prove the provenance of arbitrary candidates. Same-count external changes remain possible.

Native routes: `GetIsotropicMaterialAssignedBeamCount`, `GetIsotropicMaterialAssignedBeamList`.

```csharp
System.Console.WriteLine(property.GetIsotropicMaterialAssignedBeams("STEEL", new[] { new Beam { Id = 17 } }).Count);
```

## 9. GetIsotropicMaterialAssignedBeams

```csharp
HashSet<Beam> GetIsotropicMaterialAssignedBeams(string mName, IOSGeometry geometry)
```

Finds supplied or hydrated beams assigned to one isotropic material.

- `mName`: Exact nonblank material name; passed unchanged, not a list index.
- `geometry`: Non-null Geometry adapter for the same model/binding. Retrieves all candidate entities with GetAllEntities<T>(1); node/incidence hydration may be expensive.

**Returns:** A new mutable set of matching Beam references; empty when native assignment count is zero. Native IDs absent from the candidates are not fabricated.

When to use: Use the candidate overload when geometry is already loaded, or the Geometry overload for full hydration. Bounds native counts to 100000, validates positive Int32 IDs and exact array shape, then rechecks positive counts. Zero skips fetching. The member count follows native purpose and main usage despite an inconsistent help return table; the member list requires integral zero success. No entity assignments or native edits occur. Geometry must refer to the same model; the wrapper cannot prove the provenance of arbitrary candidates. Same-count external changes remain possible.

Native routes: `GetIsotropicMaterialAssignedBeamCount`, `GetIsotropicMaterialAssignedBeamList`.

```csharp
System.Console.WriteLine(property.GetIsotropicMaterialAssignedBeams("STEEL", wrapper.Geometry).Count);
```

## 10. GetIsotropicMaterialAssignedPlates

```csharp
HashSet<Plate> GetIsotropicMaterialAssignedPlates(string mName, IEnumerable<Plate> allPlates)
```

Finds supplied or hydrated plates assigned to one isotropic material.

- `mName`: Exact nonblank material name; passed unchanged, not a list index.
- `allPlates`: Non-null sequence of non-null positive-ID candidates. Results reference the first supplied object matching each native ID.

**Returns:** A new mutable set of matching Plate references; empty when native assignment count is zero. Native IDs absent from the candidates are not fabricated.

When to use: Use the candidate overload when geometry is already loaded, or the Geometry overload for full hydration. Bounds native counts to 100000, validates positive Int32 IDs and exact array shape, then rechecks positive counts. Zero skips fetching. The plate count must be nonnegative; its list requires true/1 success. No entity assignments or native edits occur. Geometry must refer to the same model; the wrapper cannot prove the provenance of arbitrary candidates. Same-count external changes remain possible.

Native routes: `GetIsotropicMaterialAssignedPlateCount`, `GetIsotropicMaterialAssignedPlateList`.

```csharp
System.Console.WriteLine(property.GetIsotropicMaterialAssignedPlates("STEEL", new[] { new Plate { Id = 17 } }).Count);
```

## 11. GetIsotropicMaterialAssignedPlates

```csharp
HashSet<Plate> GetIsotropicMaterialAssignedPlates(string mName, IOSGeometry geometry)
```

Finds supplied or hydrated plates assigned to one isotropic material.

- `mName`: Exact nonblank material name; passed unchanged, not a list index.
- `geometry`: Non-null Geometry adapter for the same model/binding. Retrieves all candidate entities with GetAllEntities<T>(1); node/incidence hydration may be expensive.

**Returns:** A new mutable set of matching Plate references; empty when native assignment count is zero. Native IDs absent from the candidates are not fabricated.

When to use: Use the candidate overload when geometry is already loaded, or the Geometry overload for full hydration. Bounds native counts to 100000, validates positive Int32 IDs and exact array shape, then rechecks positive counts. Zero skips fetching. The plate count must be nonnegative; its list requires true/1 success. No entity assignments or native edits occur. Geometry must refer to the same model; the wrapper cannot prove the provenance of arbitrary candidates. Same-count external changes remain possible.

Native routes: `GetIsotropicMaterialAssignedPlateCount`, `GetIsotropicMaterialAssignedPlateList`.

```csharp
System.Console.WriteLine(property.GetIsotropicMaterialAssignedPlates("STEEL", wrapper.Geometry).Count);
```

## 12. CreatePlateElementThickness

```csharp
IOSProperty CreatePlateElementThickness(PlateElementThickness pet)
```

Creates native plate thickness definitions and stores their IDs in the supplied packets.

- `pet`: Non-null thickness packet. A/B/C must be finite positive lengths; D may be zero for the native triangular fourth slot. No corner substitution occurs.

**Returns:** The same Property adapter for fluent chaining, after all requested operations complete.

When to use: Use before AssignPlateElementThickness. Copies A/B/C/D in incidence order into CreatePlateThicknessProperty and stores only a positive native ID. Zero or a negative creation error throws without replacing that packet's prior ID. Repeated calls create definitions again. Earlier successful creations survive a later failure; no rollback, assignment, save or unit conversion occurs.

Native routes: `CreatePlateThicknessProperty`.

```csharp
var pet = new PlateElementThickness(0.2);
property.CreatePlateElementThickness(pet);
System.Console.WriteLine(pet.Id);
```

## 13. CreatePlateElementThicknesses

```csharp
IOSProperty CreatePlateElementThicknesses(HashSet<PlateElementThickness> petSet)
```

Creates native plate thickness definitions and stores their IDs in the supplied packets.

- `petSet`: Non-null, nonempty set without null entries. All packets are validated before the first native call.

**Returns:** The same Property adapter for fluent chaining, after all requested operations complete.

When to use: Use before AssignPlateElementThickness. Copies A/B/C/D in incidence order into CreatePlateThicknessProperty and stores only a positive native ID. Zero or a negative creation error throws without replacing that packet's prior ID. Repeated calls create definitions again. Earlier successful creations survive a later failure; no rollback, assignment, save or unit conversion occurs.

Native routes: `CreatePlateThicknessProperty`.

```csharp
var pet = new PlateElementThickness(0.2);
property.CreatePlateElementThicknesses(new System.Collections.Generic.HashSet<PlateElementThickness> { pet });
System.Console.WriteLine(pet.Id);
```

## 14. AssignPlateElementThickness

```csharp
IOSProperty AssignPlateElementThickness(PlateElementThickness pet)
```

Assigns existing native thickness definitions to their candidate plate sets.

- `pet`: Null or no AssignedPlates is a no-op. Otherwise Id must identify an existing positive native thickness definition and candidates must be non-null positive-ID plates.

**Returns:** The same Property adapter for fluent chaining, after all requested operations complete.

When to use: Use after creating or resolving thickness IDs. Snapshots assignment IDs before dispatch, calls AssignPlateThickness and requires integral zero success. Native -4008 can report partial assignment and is surfaced as an exception; already applied changes are not undone. Does not create definitions, alter managed geometry, save or retry.

Native routes: `AssignPlateThickness`.

```csharp
var pet = new PlateElementThickness(0.2) { Id = 45 };
pet.AssignedPlates.Add(new Plate { Id = 17 });
property.AssignPlateElementThickness(pet);
```

## 15. AssignPlateElementThicknesses

```csharp
IOSProperty AssignPlateElementThicknesses(HashSet<PlateElementThickness> petSet)
```

Assigns existing native thickness definitions to their candidate plate sets.

- `petSet`: Null or empty is a no-op. Null entries and packets with no assignments are skipped; nonempty assignments require positive packet and plate IDs.

**Returns:** The same Property adapter for fluent chaining, after all requested operations complete.

When to use: Use after creating or resolving thickness IDs. Snapshots assignment IDs before dispatch, calls AssignPlateThickness and requires integral zero success. Native -4008 can report partial assignment and is surfaced as an exception; already applied changes are not undone. Does not create definitions, alter managed geometry, save or retry.

Native routes: `AssignPlateThickness`.

```csharp
var pet = new PlateElementThickness(0.2) { Id = 45 };
pet.AssignedPlates.Add(new Plate { Id = 17 });
property.AssignPlateElementThicknesses(new System.Collections.Generic.HashSet<PlateElementThickness> { pet });
```

## 16. GetPlateThicknessByPlateId

```csharp
PlateElementThickness GetPlateThicknessByPlateId(int plateId)
```

Reads four native thickness slots by analytical plate ID.

- `plateId`: Positive analytical plate ID; not a thickness definition ID.

**Returns:** A new independent packet with four finite nonnegative native length values, no assigned plates and Id zero because the query does not resolve a definition ID.

When to use: Use the property-ID method for definitions or the plate-ID method for effective plate values. GetPlateThickness must return integral zero success. All four output slots must be written and finite. No uniformity assumption, triangular fourth-slot replacement, averaging, conversion or assignment occurs.

Native routes: `GetPlateThickness`.

```csharp
var pet = property.GetPlateThicknessByPlateId(17);
System.Console.WriteLine(pet.D);
```

## 17. GetPlateThicknessByThicknessPropertyId

```csharp
PlateElementThickness GetPlateThicknessByThicknessPropertyId(int petId)
```

Reads four native thickness slots by definition ID.

- `petId`: Positive native thickness definition ID; not a plate ID.

**Returns:** A new independent packet with four finite nonnegative native length values, no assigned plates and Id set to petId.

When to use: Use the property-ID method for definitions or the plate-ID method for effective plate values. GetThicknessPropertyValues must return exactly integral 4; zero is failure. All four output slots must be written and finite. No uniformity assumption, triangular fourth-slot replacement, averaging, conversion or assignment occurs.

Native routes: `GetThicknessPropertyValues`.

```csharp
var pet = property.GetPlateThicknessByThicknessPropertyId(45);
System.Console.WriteLine(pet.D);
```

## 18. GetAllPlateElementThicknesses

```csharp
HashSet<PlateElementThickness> GetAllPlateElementThicknesses()
```

Reads all native thickness definitions and their four corner values.


**Returns:** A new mutable set of independent packets with native definition IDs and empty AssignedPlates sets; empty for an empty inventory.

When to use: Use to inspect definitions without hydrating plates. Uses the bounded thickness inventory and four-value read contract, then rereads inventory IDs to reject an observed reorder/replacement. Duplicate inventory IDs retain separate packets as in the reference type's identity equality. Other receivers can still make same-ID edits. No model changes, defaults or unit conversion occur.

Native routes: `GetThicknessPropertyCount`, `GetThicknessPropertyList`, `GetThicknessPropertyValues`.

```csharp
foreach (var pet in property.GetAllPlateElementThicknesses()) System.Console.WriteLine(pet.Id);
```

## 19. GetAllPlateElementThicknessesWithPopulatedAssignments

```csharp
HashSet<PlateElementThickness> GetAllPlateElementThicknessesWithPopulatedAssignments(IEnumerable<Plate> allPlates)
```

Reads thickness definitions and populates their candidate plate references.

- `allPlates`: Non-null sequence of non-null positive-ID candidate plates. Matching references are reused; no unknown plates are synthesized.

**Returns:** A new set of thickness packets whose AssignedPlates sets contain the matching original or hydrated candidate instances.

When to use: Use when thickness assignments are needed alongside values. The candidate overload avoids geometry hydration. Uses checked native thickness inventories, four-value reads and assignment count/list contracts; positive list counts are rechecked. No thickness or native assignment is created. External edits across separate observations can still prevent a model-wide atomic snapshot.

Native routes: `GetThicknessPropertyCount`, `GetThicknessPropertyList`, `GetThicknessPropertyValues`, `GetThicknessPropertyAssignedPlateCount`, `GetThicknessPropertyAssignedPlateList`.

```csharp
System.Console.WriteLine(property.GetAllPlateElementThicknessesWithPopulatedAssignments(new[] { new Plate { Id = 17 } }).Count);
```

## 20. GetAllPlateElementThicknessesWithPopulatedAssignments

```csharp
HashSet<PlateElementThickness> GetAllPlateElementThicknessesWithPopulatedAssignments(IOSGeometry geometry)
```

Reads thickness definitions and populates their candidate plate references.

- `geometry`: Non-null Geometry adapter for the same model. Hydrates plates using GetAllEntities<Plate>(1).

**Returns:** A new set of thickness packets whose AssignedPlates sets contain the matching original or hydrated candidate instances.

When to use: Use when thickness assignments are needed alongside values. The candidate overload avoids geometry hydration. Uses checked native thickness inventories, four-value reads and assignment count/list contracts; positive list counts are rechecked. No thickness or native assignment is created. External edits across separate observations can still prevent a model-wide atomic snapshot.

Native routes: `GetThicknessPropertyCount`, `GetThicknessPropertyList`, `GetThicknessPropertyValues`, `GetThicknessPropertyAssignedPlateCount`, `GetThicknessPropertyAssignedPlateList`.

```csharp
System.Console.WriteLine(property.GetAllPlateElementThicknessesWithPopulatedAssignments(wrapper.Geometry).Count);
```

## 21. GetBeamOffsetSpecification

```csharp
OffsetSpecification GetBeamOffsetSpecification(int bId, OffsetLocation location, OffsetAxis offsetAxis)
```

Reads the components of one member-end offset in the requested frame.

- `bId`: Positive analytical beam/member ID.
- `location`: Start=0 or End=1; not an incidence node ID.
- `offsetAxis`: Global=0 or Local=1. No managed coordinate transform is substituted.

**Returns:** An independent unnumbered packet preserving the requested end/frame and native components. Native NoSpecification is represented by zero components for compatibility; other failures throw.

When to use: Use for a direct mutable compatibility packet; use GetMemberGlobalOffset/GetMemberLocalOffset when absence must remain distinct from successful zero. Checks the existing typed native contract (0 success, -6027 no specification). Unlike the shared None shortcut in the main implementation, zero packets remain independent and retain end/frame. No native ID, candidate assignments, unit conversion or model edits are inferred.

Native routes: `GetMemberGlobalOffSet`, `GetMemberLocalOffSet`.

```csharp
var offset = property.GetBeamOffsetSpecification(17, OffsetLocation.End, OffsetAxis.Local);
System.Console.WriteLine(offset.Y);
```

## 22. PopulateMemberOffsets

```csharp
IOSProperty PopulateMemberOffsets(IEnumerable<Beam> forBeams, OffsetAxis offsetAxis = OffsetAxis.Global)
```

Populates the supplied beams with independent start/end offset packets.

- `forBeams`: Non-null finite sequence of non-null positive-ID beams. Equal beams are read once; original references are updated.
- `offsetAxis`: Requested component frame, Global by default; only Global or Local is accepted.

**Returns:** The same Property adapter for fluent chaining, after all requested operations complete.

When to use: Use before reading Beam.StartOffset/EndOffset. Stages every native read before updating the supplied objects. NoSpecification yields a zero compatibility packet; use the typed observations to retain absence status. Failure leaves managed offset references unchanged. Does not create native specifications, assign members, save or convert units.

Native routes: `GetMemberGlobalOffSet`, `GetMemberLocalOffSet`.

```csharp
var beams = new[] { new Beam { Id = 17 } };
property.PopulateMemberOffsets(beams);
System.Console.WriteLine(beams[0].EndOffset.Y);
```

## 23. CreateOffsetSpecification

```csharp
IOSProperty CreateOffsetSpecification(OffsetSpecification oSpec)
```

Creates nonzero member-end offset definitions and stores their native IDs.

- `oSpec`: Non-null definition with Start/End location, Global/Local frame and finite signed components in native length units.

**Returns:** The same Property adapter for fluent chaining, after all requested operations complete.

When to use: Use before AssignOffsetSpecifications. A packet is zero when every component magnitude is below 1e-9 native units; zero packets skip creation and retain their prior IDs. Nonzero CreateMemberOffsetSpec must return a positive ID before that packet is updated. Repeated creation may add new native definitions. Earlier successes survive later failure; no rollback, assignment, conversion or save. The bulk null guard is corrected from the reference's unsafe null dereference.

Native routes: `CreateMemberOffsetSpec`.

```csharp
var offset = OffsetSpecification.GlobalYEnd(-0.15);
property.CreateOffsetSpecification(offset);
System.Console.WriteLine(offset.Id);
```

## 24. CreateOffsetSpecifications

```csharp
IOSProperty CreateOffsetSpecifications(HashSet<OffsetSpecification> osSet)
```

Creates nonzero member-end offset definitions and stores their native IDs.

- `osSet`: Non-null nonempty set of non-null definitions. Location/frame and finite components are validated before native dispatch; do not change definition values while in the set.

**Returns:** The same Property adapter for fluent chaining, after all requested operations complete.

When to use: Use before AssignOffsetSpecifications. A packet is zero when every component magnitude is below 1e-9 native units; zero packets skip creation and retain their prior IDs. Nonzero CreateMemberOffsetSpec must return a positive ID before that packet is updated. Repeated creation may add new native definitions. Earlier successes survive later failure; no rollback, assignment, conversion or save. The bulk null guard is corrected from the reference's unsafe null dereference.

Native routes: `CreateMemberOffsetSpec`.

```csharp
var offset = OffsetSpecification.GlobalYEnd(-0.15);
property.CreateOffsetSpecifications(new System.Collections.Generic.HashSet<OffsetSpecification> { offset });
System.Console.WriteLine(offset.Id);
```

## 25. AssignOffsetSpecifications

```csharp
IOSProperty AssignOffsetSpecifications(HashSet<OffsetSpecification> osSet)
```

Assigns nonzero existing offset specifications to their candidate members.

- `osSet`: Null or empty is a no-op; otherwise non-null valid definitions. Zero definitions or empty Beams sets are skipped. Other definitions require positive existing specification/member IDs.

**Returns:** The same Property adapter for fluent chaining, after all requested operations complete.

When to use: Use after CreateOffsetSpecifications or resolving native IDs. Snapshots each member ID set before dispatch and requires integral zero from AssignMemberSpecToBeam. Stops at the first failure; earlier native assignments remain applied. Does not create or delete specifications, save, retry or repair stale native IDs.

Native routes: `AssignMemberSpecToBeam`.

```csharp
var offset = OffsetSpecification.GlobalYEnd(-0.15);
offset.Id = 72;
offset.Beams.Add(new Beam { Id = 17 });
property.AssignOffsetSpecifications(new System.Collections.Generic.HashSet<OffsetSpecification> { offset });
```

## Qualification and remaining work

Offline tests and the isolated installed-package consumer validate managed contracts and XML examples. Live mutation and engineering qualification on each acquired STAAD build remain pending; no user model is changed by the verification suite. Remaining Property work includes the material/domain population graph, section catalog graph, release pipelines, private unit conversion helpers and unported native creation/specification routes. Existing observation methods with matching parameter types but qualified return packets cannot be duplicated using only a different return type; those compatibility decisions remain explicit future work.
