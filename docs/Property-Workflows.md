# Property material, thickness, offset and release workflows

This guide records the preceding 25-workflow checkpoint. Property now exposes 175 overloads; see [the section completion](Property-Sections.md) and [the material/member-section routes](Property-Materials-and-Member-Sections.md) and [the latest section/specification creators](Property-Section-and-Specification-Creation.md). The basic material converter overloads deferred at this checkpoint are now included using the repository UnitsConverter.


This increment ports 25 further main-interface overloads while preserving names, parameter types/names/order and defaults after adapting namespaces. Property now exposes 100 overloads: 25 qualified observations and 75 main-project compatibility ports. Three additional native names have routes, reaching 57 of 204 active candidates with 147 pending. This is dispatch coverage, not native qualification.

## Shared contracts

Executes synchronously on the established caller COM apartment and shares this Property adapter's captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.

Read/population examples change only managed packets. Creation and assignment examples assume an appropriate disposable native test model with matching material/entity/property IDs; package verification runs every example against managed stand-ins. Native writes can partially complete and have no rollback.

## Supporting models and deliberate corrections

- MaterialProperty retains source field names, mutable candidate sets, assumed creation defaults and derived G/K. Defaults are not native observations. Qualified material queries retain their original immutable status/data contract and directly observed G.
- Material creation native 0/1 means Created/Updated. The packet records LastCreationStatus and leaves Id unchanged; it never writes those statuses into a material list index. Basic creation does not send strength fields. The legacy non-NoConversion overloads retain their fixed E/G x1000 scale, which is not unit detection.
- Indexed material collection reads retain actual native names. Equality/hash both ignore name case in the current culture, correcting unnamed-packet collapse and the source hash mismatch. Do not rename packets inside hash sets.
- PlateThickness represents one uniform scalar value, unlike the earlier four-corner PlateElementThickness packet. Null bulk assignment guards are corrected. Explicit-foreign-receiver creation and private converter overloads remain deferred.
- MemberReleases retains the original flag/spring/factor projections and an immutable NativeSpecification carrying per-DOF modes. Read-back stages both ends before changing beams. Basic creation rejects partial-moment packets instead of losing their modes/factors. Native partial-mode provenance survives projection edits, even for zero/unreturned factors, until explicitly replaced by a new parsed/basic packet. Its full release/spring flag semantics still need live qualification.
- Release/Springs/MpFactors supporting packets have expanded XML documentation; private mapping interfaces and unit converters are omitted. Null unused spring inputs map to zero only for native basic creation. Aggregate release classification is derived without duplicated event subscriptions.
- Offset pipelines group exact definitions, retain canonical existing targets and supplied beam references, skip near-zero definitions in both overloads, and propagate created IDs to equal source packets. Degree-based direct assignment remains ID-based and does not inspect components.
- Every nThreads argument keeps its source signature/range (1-512); calls remain on the established caller COM apartment under the existing shared Property gate.

## All 25 signatures

## 1. AssignMaterialProperties

```csharp
IOSProperty AssignMaterialProperties(HashSet<MaterialProperty> mpSet)
```

Assigns each named material to its managed beam and plate candidates.

- `mpSet`: Material packet, or set of packets. Null/empty is a no-op; null packets in sets are rejected. Nonempty candidate sets require non-null positive-ID entities and an exact nonblank native material name.

**Returns:** The same Property adapter for fluent chaining after the requested workflow completes.

When to use: Use after the named materials already exist. Snapshots names and candidate IDs before native access, then assigns members before plates for each packet. AssignMaterialToMember requires true/1; AssignMaterialToPlate requires integral 0. A -4008 plate result can mean partial assignment and throws. Does not create definitions, assign solids, save or roll back prior writes. Null candidate sets are treated as empty. The degree argument retains the source signature without copying its worker-thread COM dispatch.

Native routes: `AssignMaterialToMember`, `AssignMaterialToPlate`.

```csharp
var material = new MaterialProperty("STEEL") { E = 200000.0, Poisson = 0.3, Density = 77.0, Alpha = 0.000012, Damping = 0.02 };
material.Beams.Add(new Beam { Id = 17 });
material.Plates.Add(new Plate { Id = 17 });
property.AssignMaterialProperties(new System.Collections.Generic.HashSet<MaterialProperty> { material });
```

## 2. AssignMaterialProperties

```csharp
IOSProperty AssignMaterialProperties(HashSet<MaterialProperty> materialPropertyCollection, int nThreads)
```

Assigns each named material to its managed beam and plate candidates.

- `materialPropertyCollection`: Material packet, or set of packets. Null/empty is a no-op; null packets in sets are rejected. Nonempty candidate sets require non-null positive-ID entities and an exact nonblank native material name.
- `nThreads`: Source-compatible degree value from 1 through 512. COM calls remain synchronous on the caller apartment; no worker-thread COM dispatch occurs.

**Returns:** The same Property adapter for fluent chaining after the requested workflow completes.

When to use: Use after the named materials already exist. Snapshots names and candidate IDs before native access, then assigns members before plates for each packet. AssignMaterialToMember requires true/1; AssignMaterialToPlate requires integral 0. A -4008 plate result can mean partial assignment and throws. Does not create definitions, assign solids, save or roll back prior writes. Null candidate sets are treated as empty. The degree argument retains the source signature without copying its worker-thread COM dispatch.

Native routes: `AssignMaterialToMember`, `AssignMaterialToPlate`.

```csharp
var material = new MaterialProperty("STEEL") { E = 200000.0, Poisson = 0.3, Density = 77.0, Alpha = 0.000012, Damping = 0.02 };
material.Beams.Add(new Beam { Id = 17 });
material.Plates.Add(new Plate { Id = 17 });
property.AssignMaterialProperties(new System.Collections.Generic.HashSet<MaterialProperty> { material }, 2);
```

## 3. AssignMaterialProperty

```csharp
IOSProperty AssignMaterialProperty(MaterialProperty mp, int nThreads)
```

Assigns each named material to its managed beam and plate candidates.

- `mp`: Material packet, or set of packets. Null/empty is a no-op; null packets in sets are rejected. Nonempty candidate sets require non-null positive-ID entities and an exact nonblank native material name.
- `nThreads`: Source-compatible degree value from 1 through 512. COM calls remain synchronous on the caller apartment; no worker-thread COM dispatch occurs.

**Returns:** The same Property adapter for fluent chaining after the requested workflow completes.

When to use: Use after the named materials already exist. Snapshots names and candidate IDs before native access, then assigns members before plates for each packet. AssignMaterialToMember requires true/1; AssignMaterialToPlate requires integral 0. A -4008 plate result can mean partial assignment and throws. Does not create definitions, assign solids, save or roll back prior writes. Null candidate sets are treated as empty. The degree argument retains the source signature without copying its worker-thread COM dispatch.

Native routes: `AssignMaterialToMember`, `AssignMaterialToPlate`.

```csharp
var material = new MaterialProperty("STEEL") { E = 200000.0, Poisson = 0.3, Density = 77.0, Alpha = 0.000012, Damping = 0.02 };
material.Beams.Add(new Beam { Id = 17 });
material.Plates.Add(new Plate { Id = 17 });
property.AssignMaterialProperty(material, 2);
```

## 4. CreateMaterialProperties

```csharp
IOSProperty CreateMaterialProperties(HashSet<MaterialProperty> mpSet)
```

Creates or updates named isotropic material definitions using the source legacy E/G scale.

- `mpSet`: Non-null, nonempty set without null packets. All input values are snapshotted and validated before the first native call.

**Returns:** The same Property adapter for fluent chaining after the requested workflow completes.

When to use: This legacy compatibility overload multiplies E and the derived G by exactly 1000 and leaves density, thermal expansion and damping unchanged, matching the main source. This is a fixed legacy scale, not a detected unit conversion. Prefer the NoConversion overload after preparing inputs in the acquired native creation convention. Calls CreateIsotropicMaterialProperties: native 0 means Created and 1 Updated, not a material ID. Records LastCreationStatus after success and leaves Id unchanged, correcting the source status-as-ID assignment. General error -1 and other undocumented responses throw without changing that packet's prior status/Id. Earlier successful definitions survive a later failure. G is computed from E/Poisson as in the source packet, not read from native storage. Does not populate assignment sets, assign entities, send Fy/Fu/Ry/Rt/Fcu, save or roll back.

Native routes: `CreateIsotropicMaterialProperties`.

```csharp
var material = new MaterialProperty("STEEL") { E = 200000.0, Poisson = 0.3, Density = 77.0, Alpha = 0.000012, Damping = 0.02 };
property.CreateMaterialProperties(new System.Collections.Generic.HashSet<MaterialProperty> { material });
System.Console.WriteLine(material.LastCreationStatus);
```

## 5. CreateMaterialPropertiesNoConversion

```csharp
IOSProperty CreateMaterialPropertiesNoConversion(HashSet<MaterialProperty> mpSet)
```

Creates or updates named isotropic material definitions using unconverted constants.

- `mpSet`: Non-null, nonempty set without null packets. All input values are snapshotted and validated before the first native call.

**Returns:** The same Property adapter for fluent chaining after the requested workflow completes.

When to use: Use when E and derived G are already in the acquired native creation force/area convention. Density remains weight per volume; alpha and damping pass unchanged. No conversion or native input-unit changes occur. Calls CreateIsotropicMaterialProperties: native 0 means Created and 1 Updated, not a material ID. Records LastCreationStatus after success and leaves Id unchanged, correcting the source status-as-ID assignment. General error -1 and other undocumented responses throw without changing that packet's prior status/Id. Earlier successful definitions survive a later failure. G is computed from E/Poisson as in the source packet, not read from native storage. Does not populate assignment sets, assign entities, send Fy/Fu/Ry/Rt/Fcu, save or roll back.

Native routes: `CreateIsotropicMaterialProperties`.

```csharp
var material = new MaterialProperty("STEEL") { E = 200000.0, Poisson = 0.3, Density = 77.0, Alpha = 0.000012, Damping = 0.02 };
property.CreateMaterialPropertiesNoConversion(new System.Collections.Generic.HashSet<MaterialProperty> { material });
System.Console.WriteLine(material.LastCreationStatus);
```

## 6. CreateMaterialProperty

```csharp
IOSProperty CreateMaterialProperty(MaterialProperty mp)
```

Creates or updates named isotropic material definitions using the source legacy E/G scale.

- `mp`: Non-null named material with finite constants. E/G must be positive, -1 < Poisson < 0.5, and weight density/damping nonnegative. Basic creation does not send strength fields.

**Returns:** The same Property adapter for fluent chaining after the requested workflow completes.

When to use: This legacy compatibility overload multiplies E and the derived G by exactly 1000 and leaves density, thermal expansion and damping unchanged, matching the main source. This is a fixed legacy scale, not a detected unit conversion. Prefer the NoConversion overload after preparing inputs in the acquired native creation convention. Calls CreateIsotropicMaterialProperties: native 0 means Created and 1 Updated, not a material ID. Records LastCreationStatus after success and leaves Id unchanged, correcting the source status-as-ID assignment. General error -1 and other undocumented responses throw without changing that packet's prior status/Id. Earlier successful definitions survive a later failure. G is computed from E/Poisson as in the source packet, not read from native storage. Does not populate assignment sets, assign entities, send Fy/Fu/Ry/Rt/Fcu, save or roll back.

Native routes: `CreateIsotropicMaterialProperties`.

```csharp
var material = new MaterialProperty("STEEL") { E = 200000.0, Poisson = 0.3, Density = 77.0, Alpha = 0.000012, Damping = 0.02 };
property.CreateMaterialProperty(material);
System.Console.WriteLine(material.LastCreationStatus);
```

## 7. CreateMaterialPropertyNoConversion

```csharp
IOSProperty CreateMaterialPropertyNoConversion(MaterialProperty mp)
```

Creates or updates named isotropic material definitions using unconverted constants.

- `mp`: Non-null named material with finite constants. E/G must be positive, -1 < Poisson < 0.5, and weight density/damping nonnegative. Basic creation does not send strength fields.

**Returns:** The same Property adapter for fluent chaining after the requested workflow completes.

When to use: Use when E and derived G are already in the acquired native creation force/area convention. Density remains weight per volume; alpha and damping pass unchanged. No conversion or native input-unit changes occur. Calls CreateIsotropicMaterialProperties: native 0 means Created and 1 Updated, not a material ID. Records LastCreationStatus after success and leaves Id unchanged, correcting the source status-as-ID assignment. General error -1 and other undocumented responses throw without changing that packet's prior status/Id. Earlier successful definitions survive a later failure. G is computed from E/Poisson as in the source packet, not read from native storage. Does not populate assignment sets, assign entities, send Fy/Fu/Ry/Rt/Fcu, save or roll back.

Native routes: `CreateIsotropicMaterialProperties`.

```csharp
var material = new MaterialProperty("STEEL") { E = 200000.0, Poisson = 0.3, Density = 77.0, Alpha = 0.000012, Damping = 0.02 };
property.CreateMaterialPropertyNoConversion(material);
System.Console.WriteLine(material.LastCreationStatus);
```

## 8. GetAllExtendedIsotropicMaterialProperties

```csharp
HashSet<MaterialProperty> GetAllExtendedIsotropicMaterialProperties()
```

Reads all extended isotropic material definitions by native zero-based index.


**Returns:** A new mutable name-equal set of material packets with actual native names and zero-based Id values; empty for a zero inventory. Case-insensitive duplicate names collapse, retaining the first packet.

When to use: Use to enumerate definitions regardless of their use by supplied entities. Bounds the count to 100000 and uses the existing qualified indexed query for every item, checking its native name, constants and count consistency. Rechecks the total count afterward. Correctly retains returned names instead of leaving every indexed packet unnamed as in the source. Retains native strength values. Compatibility G/K remain derived; use the qualified indexed query to inspect directly observed G. Assignment sets are initially empty. Same-count external edits cannot be ruled out. No native mutation, inferred units or defaults replace failed reads.

Native routes: `GetIsotropicMaterialCount`, `GetIsotropicMaterialPropertiesEx`.

```csharp
foreach (var material in property.GetAllExtendedIsotropicMaterialProperties()) System.Console.WriteLine(material.Name);
```

## 9. GetAllIsotropicMaterialProperties

```csharp
HashSet<MaterialProperty> GetAllIsotropicMaterialProperties()
```

Reads all isotropic material definitions by native zero-based index.


**Returns:** A new mutable name-equal set of material packets with actual native names and zero-based Id values; empty for a zero inventory. Case-insensitive duplicate names collapse, retaining the first packet.

When to use: Use to enumerate definitions regardless of their use by supplied entities. Bounds the count to 100000 and uses the existing qualified indexed query for every item, checking its native name, constants and count consistency. Rechecks the total count afterward. Correctly retains returned names instead of leaving every indexed packet unnamed as in the source. Basic packets have zero strength placeholders; these are not observations of absent strength fields. Compatibility G/K remain derived; use the qualified indexed query to inspect directly observed G. Assignment sets are initially empty. Same-count external edits cannot be ruled out. No native mutation, inferred units or defaults replace failed reads.

Native routes: `GetIsotropicMaterialCount`, `GetIsotropicMaterialProperties`.

```csharp
foreach (var material in property.GetAllIsotropicMaterialProperties()) System.Console.WriteLine(material.Name);
```

## 10. GetAllExtendedIsotropicMaterialPropertiesWithPopulatedAssignments

```csharp
HashSet<MaterialProperty> GetAllExtendedIsotropicMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates)
```

Reads all indexed isotropic definitions and populates candidate assignments.

- `forBeams`: Optional finite sequence of non-null positive-ID beam candidates; null/empty skips replacing beam assignments.
- `forPlates`: Optional finite sequence of non-null positive-ID plate candidates; null/empty skips replacing plate assignments.

**Returns:** New material packets with matching original candidate references in Beams/Plates. Definitions are name-equal using the current culture, ignoring case; unknown native entity IDs are not synthesized.

When to use: Use to include unused definitions as well as assigned materials. Retrieves all indexed isotropic materials first. Uses extended strength reads. Snapshots each input sequence once, validates candidate IDs, and stages all native assignment reads before updating packet sets. Native material subset count/list contracts and the 100000-entry limits apply. Null/empty candidate sides leave that side unchanged. The typed single-material APIs retain their qualified return packets; compatibility collection methods map those observations into mutable MaterialProperty packets. No geometry hydration, native assignment, conversion, save or model-wide atomic snapshot is implied.

Native routes: `GetIsotropicMaterialCount`, `GetIsotropicMaterialPropertiesEx`, `GetIsotropicMaterialAssignedBeamCount`, `GetIsotropicMaterialAssignedBeamList`, `GetIsotropicMaterialAssignedPlateCount`, `GetIsotropicMaterialAssignedPlateList`.

```csharp
var materials = property.GetAllExtendedIsotropicMaterialPropertiesWithPopulatedAssignments(new[] { new Beam { Id = 17 } }, new[] { new Plate { Id = 17 } });
System.Console.WriteLine(materials.Count);
```

## 11. GetAllIsotropicMaterialPropertiesWithPopulatedAssignments

```csharp
HashSet<MaterialProperty> GetAllIsotropicMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates)
```

Reads all indexed isotropic definitions and populates candidate assignments.

- `forBeams`: Optional finite sequence of non-null positive-ID beam candidates; null/empty skips replacing beam assignments.
- `forPlates`: Optional finite sequence of non-null positive-ID plate candidates; null/empty skips replacing plate assignments.

**Returns:** New material packets with matching original candidate references in Beams/Plates. Definitions are name-equal using the current culture, ignoring case; unknown native entity IDs are not synthesized.

When to use: Use to include unused definitions as well as assigned materials. Retrieves all indexed isotropic materials first. Uses basic reads with zero placeholders for unreturned strengths. Snapshots each input sequence once, validates candidate IDs, and stages all native assignment reads before updating packet sets. Native material subset count/list contracts and the 100000-entry limits apply. Null/empty candidate sides leave that side unchanged. The typed single-material APIs retain their qualified return packets; compatibility collection methods map those observations into mutable MaterialProperty packets. No geometry hydration, native assignment, conversion, save or model-wide atomic snapshot is implied.

Native routes: `GetIsotropicMaterialCount`, `GetIsotropicMaterialProperties`, `GetIsotropicMaterialAssignedBeamCount`, `GetIsotropicMaterialAssignedBeamList`, `GetIsotropicMaterialAssignedPlateCount`, `GetIsotropicMaterialAssignedPlateList`.

```csharp
var materials = property.GetAllIsotropicMaterialPropertiesWithPopulatedAssignments(new[] { new Beam { Id = 17 } }, new[] { new Plate { Id = 17 } });
System.Console.WriteLine(materials.Count);
```

## 12. GetAllExtendedMaterialPropertiesWithPopulatedAssignments

```csharp
HashSet<MaterialProperty> GetAllExtendedMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates)
```

Reads material definitions used by the supplied entities and populates candidate assignments.

- `forBeams`: Optional finite sequence of non-null positive-ID beam candidates; null/empty skips replacing beam assignments.
- `forPlates`: Optional finite sequence of non-null positive-ID plate candidates; null/empty skips replacing plate assignments.

**Returns:** New material packets with matching original candidate references in Beams/Plates. Definitions are name-equal using the current culture, ignoring case; unknown native entity IDs are not synthesized.

When to use: Use to query only material names actually used by the supplied beams/plates; both empty inputs return an empty set. Names are discovered with the beam/plate-specific APIs and resolved once per exact distinct name. Uses extended strength reads. Snapshots each input sequence once, validates candidate IDs, and stages all native assignment reads before updating packet sets. Native material subset count/list contracts and the 100000-entry limits apply. Null/empty candidate sides leave that side unchanged. The typed single-material APIs retain their qualified return packets; compatibility collection methods map those observations into mutable MaterialProperty packets. No geometry hydration, native assignment, conversion, save or model-wide atomic snapshot is implied.

Native routes: `GetBeamMaterialName`, `GetPlateMaterialName`, `GetMaterialPropertyEx`, `GetIsotropicMaterialAssignedBeamCount`, `GetIsotropicMaterialAssignedBeamList`, `GetIsotropicMaterialAssignedPlateCount`, `GetIsotropicMaterialAssignedPlateList`.

```csharp
var materials = property.GetAllExtendedMaterialPropertiesWithPopulatedAssignments(new[] { new Beam { Id = 17 } }, new[] { new Plate { Id = 17 } });
System.Console.WriteLine(materials.Count);
```

## 13. GetAllMaterialPropertiesWithPopulatedAssignments

```csharp
HashSet<MaterialProperty> GetAllMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates)
```

Reads material definitions used by the supplied entities and populates candidate assignments.

- `forBeams`: Optional finite sequence of non-null positive-ID beam candidates; null/empty skips replacing beam assignments.
- `forPlates`: Optional finite sequence of non-null positive-ID plate candidates; null/empty skips replacing plate assignments.

**Returns:** New material packets with matching original candidate references in Beams/Plates. Definitions are name-equal using the current culture, ignoring case; unknown native entity IDs are not synthesized.

When to use: Use to query only material names actually used by the supplied beams/plates; both empty inputs return an empty set. Names are discovered with the beam/plate-specific APIs and resolved once per exact distinct name. Uses basic reads with zero placeholders for unreturned strengths. Snapshots each input sequence once, validates candidate IDs, and stages all native assignment reads before updating packet sets. Native material subset count/list contracts and the 100000-entry limits apply. Null/empty candidate sides leave that side unchanged. The typed single-material APIs retain their qualified return packets; compatibility collection methods map those observations into mutable MaterialProperty packets. No geometry hydration, native assignment, conversion, save or model-wide atomic snapshot is implied.

Native routes: `GetBeamMaterialName`, `GetPlateMaterialName`, `GetMaterialProperty`, `GetIsotropicMaterialAssignedBeamCount`, `GetIsotropicMaterialAssignedBeamList`, `GetIsotropicMaterialAssignedPlateCount`, `GetIsotropicMaterialAssignedPlateList`.

```csharp
var materials = property.GetAllMaterialPropertiesWithPopulatedAssignments(new[] { new Beam { Id = 17 } }, new[] { new Plate { Id = 17 } });
System.Console.WriteLine(materials.Count);
```

## 14. GetAllMaterialPropertiesWithPopulatedAssignments

```csharp
HashSet<MaterialProperty> GetAllMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates, Func<string, MaterialProperty> materialResolver)
```

Resolves distinct entity material names with a caller delegate and populates their candidate assignments.

- `forBeams`: Optional finite sequence of non-null positive-ID beam candidates; enumerated once.
- `forPlates`: Optional finite sequence of non-null positive-ID plate candidates; enumerated once.
- `materialResolver`: Non-null synchronous callback, invoked once per exact distinct discovered native name on the caller apartment under this adapter's reentrant lock. Must return a non-null packet with a nonblank Name. Its returned Name selects native assignment lookups; it may deliberately resolve an alias.

**Returns:** A mutable set of the resolver-returned original material packets, deduplicated by their current-culture case-insensitive names, with staged assignment subsets populated.

When to use: Use for custom material resolution/caching without recreating packets. Does not itself query constants; the callback decides how they are obtained. Native name discovery must succeed before resolution. A callback exception propagates. All assignment reads complete before any returned packet assignment set is changed; nonempty candidate sides clear/repopulate the existing sets (or initialize null sets), while null/empty sides preserve them. Native and callback side effects cannot be rolled back. Do not rename packets already stored in sets. No save, unit conversion or entity assignment occurs in this helper.

Native routes: `GetBeamMaterialName`, `GetPlateMaterialName`, `GetIsotropicMaterialAssignedBeamCount`, `GetIsotropicMaterialAssignedBeamList`, `GetIsotropicMaterialAssignedPlateCount`, `GetIsotropicMaterialAssignedPlateList`.

```csharp
var materials = property.GetAllMaterialPropertiesWithPopulatedAssignments(new[] { new Beam { Id = 17 } }, new[] { new Plate { Id = 17 } }, name => new MaterialProperty(name));
System.Console.WriteLine(materials.Count);
```

## 15. CreatePlateThicknessProperties

```csharp
IOSProperty CreatePlateThicknessProperties(HashSet<PlateThickness> ptSet)
```

Creates uniform native thickness definitions from scalar thickness packets.

- `ptSet`: Non-null nonempty set of non-null packets with finite positive Thickness values in the acquired native creation length convention. All values are copied before native access.

**Returns:** The same Property adapter for fluent chaining after the requested workflow completes.

When to use: Use for one uniform scalar thickness per definition; use PlateElementThickness creation for explicit four-corner values. Preserves the source scalar CreatePlateThicknessProperty route. Stores only a positive returned native definition ID; invalid/error responses throw and retain that packet's prior ID. Does not assign plates, convert units or save. Earlier successful creations survive later failure.

Native routes: `CreatePlateThicknessProperty`.

```csharp
var thickness = new PlateThickness { Thickness = 0.2 };
property.CreatePlateThicknessProperties(new System.Collections.Generic.HashSet<PlateThickness> { thickness });
System.Console.WriteLine(thickness.Id);
```

## 16. AssignThicknessProperties

```csharp
IOSProperty AssignThicknessProperties(HashSet<PlateThickness> tpSet)
```

Assigns existing uniform thickness IDs to their managed plate candidates.

- `tpSet`: Null/empty input or no plate candidates is a no-op. Nonempty assignments require a positive existing thickness Id and non-null positive-ID plates. Null packets in sets are rejected.

**Returns:** The same Property adapter for fluent chaining after the requested workflow completes.

When to use: Use after native thickness definitions exist. Snapshots target IDs before dispatch; assigns each packet's plate-ID array in one native call. Requires integral zero from AssignPlateThickness; -4008 can indicate a partial assignment and throws. Corrects the source bulk null guard; no definition creation, rollback, save or unit conversion. An assignment can skip invalid positive plate IDs inside native STAAD before returning an error; there is no implicit existence preflight.

Native routes: `AssignPlateThickness`.

```csharp
var thickness = new PlateThickness { Id = 45, Thickness = 0.2 };
thickness.Plates.Add(new Plate { Id = 17 });
property.AssignThicknessProperties(new System.Collections.Generic.HashSet<PlateThickness> { thickness });
```

## 17. AssignThicknessProperties

```csharp
IOSProperty AssignThicknessProperties(HashSet<PlateThickness> thicknessPropertyCollection, int nThreads)
```

Assigns existing uniform thickness IDs to their managed plate candidates.

- `thicknessPropertyCollection`: Null/empty input or no plate candidates is a no-op. Nonempty assignments require a positive existing thickness Id and non-null positive-ID plates. Null packets in sets are rejected.
- `nThreads`: Source-compatible degree value from 1 through 512. COM calls remain synchronous on the caller apartment; no worker-thread COM dispatch occurs.

**Returns:** The same Property adapter for fluent chaining after the requested workflow completes.

When to use: Use after native thickness definitions exist. Snapshots target IDs before dispatch; assigns plates individually with the source scalar route. Requires integral zero from AssignPlateThickness; -4008 can indicate a partial assignment and throws. Corrects the source bulk null guard; no definition creation, rollback, save or unit conversion. An assignment can skip invalid positive plate IDs inside native STAAD before returning an error; there is no implicit existence preflight.

Native routes: `AssignPlateThickness`.

```csharp
var thickness = new PlateThickness { Id = 45, Thickness = 0.2 };
thickness.Plates.Add(new Plate { Id = 17 });
property.AssignThicknessProperties(new System.Collections.Generic.HashSet<PlateThickness> { thickness }, 2);
```

## 18. AssignThicknessProperty

```csharp
IOSProperty AssignThicknessProperty(PlateThickness thicknessProperty, int nThreads)
```

Assigns existing uniform thickness IDs to their managed plate candidates.

- `thicknessProperty`: Null/empty input or no plate candidates is a no-op. Nonempty assignments require a positive existing thickness Id and non-null positive-ID plates. Null packets in sets are rejected.
- `nThreads`: Source-compatible degree value from 1 through 512. COM calls remain synchronous on the caller apartment; no worker-thread COM dispatch occurs.

**Returns:** The same Property adapter for fluent chaining after the requested workflow completes.

When to use: Use after native thickness definitions exist. Snapshots target IDs before dispatch; assigns plates individually with the source scalar route. Requires integral zero from AssignPlateThickness; -4008 can indicate a partial assignment and throws. Corrects the source bulk null guard; no definition creation, rollback, save or unit conversion. An assignment can skip invalid positive plate IDs inside native STAAD before returning an error; there is no implicit existence preflight.

Native routes: `AssignPlateThickness`.

```csharp
var thickness = new PlateThickness { Id = 45, Thickness = 0.2 };
thickness.Plates.Add(new Plate { Id = 17 });
property.AssignThicknessProperty(thickness, 2);
```

## 19. AssignOffsetSpecification

```csharp
IOSProperty AssignOffsetSpecification(OffsetSpecification os, int nThreads)
```

Assigns existing offset specification IDs to individual candidate members.

- `os`: Null/empty input or no member candidates is a no-op. Nonempty targets require positive existing specification and member IDs. Assignment uses Id; these overloads do not inspect component values or skip IsZero packets.
- `nThreads`: Source-compatible degree value from 1 through 512. COM calls remain synchronous on the caller apartment; no worker-thread COM dispatch occurs.

**Returns:** The same Property adapter for fluent chaining after the requested workflow completes.

When to use: Use the source-compatible degree overload after resolving native IDs. Snapshots target IDs and calls AssignMemberSpecToBeam per member, requiring integral zero success. The non-degree bulk overload filters zero definitions; these ID-based overloads deliberately preserve the source distinction. No definition creation, unit conversion, retry, save or rollback. All native calls stay on the caller apartment.

Native routes: `AssignMemberSpecToBeam`.

```csharp
var offset = OffsetSpecification.GlobalYEnd(-0.15);
offset.Id = 72;
offset.Beams.Add(new Beam { Id = 17 });
property.AssignOffsetSpecification(offset, 2);
```

## 20. AssignOffsetSpecifications

```csharp
IOSProperty AssignOffsetSpecifications(HashSet<OffsetSpecification> offsetSpecificationCollection, int nThreads)
```

Assigns existing offset specification IDs to individual candidate members.

- `offsetSpecificationCollection`: Null/empty input or no member candidates is a no-op. Nonempty targets require positive existing specification and member IDs. Assignment uses Id; these overloads do not inspect component values or skip IsZero packets.
- `nThreads`: Source-compatible degree value from 1 through 512. COM calls remain synchronous on the caller apartment; no worker-thread COM dispatch occurs.

**Returns:** The same Property adapter for fluent chaining after the requested workflow completes.

When to use: Use the source-compatible degree overload after resolving native IDs. Snapshots target IDs and calls AssignMemberSpecToBeam per member, requiring integral zero success. The non-degree bulk overload filters zero definitions; these ID-based overloads deliberately preserve the source distinction. No definition creation, unit conversion, retry, save or rollback. All native calls stay on the caller apartment.

Native routes: `AssignMemberSpecToBeam`.

```csharp
var offset = OffsetSpecification.GlobalYEnd(-0.15);
offset.Id = 72;
offset.Beams.Add(new Beam { Id = 17 });
property.AssignOffsetSpecifications(new System.Collections.Generic.HashSet<OffsetSpecification> { offset }, 2);
```

## 21. CreateAndAssignOffsetSpecifications

```csharp
IOSProperty CreateAndAssignOffsetSpecifications(HashSet<Beam> beams)
```

Groups managed member-end offsets, creates native definitions and assigns their target members.

- `beams`: Non-null set of positive-ID beams. Null StartOffset/EndOffset is ignored. Definition location/frame and components must be valid and finite; packet Location is used as supplied rather than inferred from the Beam property.

**Returns:** The same Property adapter for fluent chaining after the requested workflow completes.

When to use: Use to apply populated or manually supplied nonzero offset packets. Groups by exact Location/Axis/X/Y/Z equality, preserving the first packet's existing Beams and adding the supplied beams using equal offsets. Creates all definitions before starting assignments. Copies each positive created ID into all equal source packets and adds candidate references to the canonical packet. Definitions below the 1e-9 per-component zero threshold are skipped in both overloads, avoiding source degree-pipeline assignments of uncreated ID zero. Assigns each grouped ID array. Requires positive creation IDs and integral zero assignment success. Empty beams/no offsets is a no-op. Earlier creations/assignments remain if a later step fails; no cleanup, unit conversion, save or analysis occurs. Do not mutate definition values while held in hash sets.

Native routes: `CreateMemberOffsetSpec`, `AssignMemberSpecToBeam`.

```csharp
var beam = new Beam { Id = 17, EndOffset = OffsetSpecification.GlobalYEnd(-0.15) };
property.CreateAndAssignOffsetSpecifications(new System.Collections.Generic.HashSet<Beam> { beam });
```

## 22. CreateAndAssignOffsetSpecifications

```csharp
IOSProperty CreateAndAssignOffsetSpecifications(HashSet<Beam> beams, int nThreads)
```

Groups managed member-end offsets, creates native definitions and assigns their target members.

- `beams`: Non-null set of positive-ID beams. Null StartOffset/EndOffset is ignored. Definition location/frame and components must be valid and finite; packet Location is used as supplied rather than inferred from the Beam property.
- `nThreads`: Source-compatible degree value from 1 through 512. COM calls remain synchronous on the caller apartment; no worker-thread COM dispatch occurs.

**Returns:** The same Property adapter for fluent chaining after the requested workflow completes.

When to use: Use to apply populated or manually supplied nonzero offset packets. Groups by exact Location/Axis/X/Y/Z equality, preserving the first packet's existing Beams and adding the supplied beams using equal offsets. Creates all definitions before starting assignments. Copies each positive created ID into all equal source packets and adds candidate references to the canonical packet. Definitions below the 1e-9 per-component zero threshold are skipped in both overloads, avoiding source degree-pipeline assignments of uncreated ID zero. Assigns individual members. Requires positive creation IDs and integral zero assignment success. Empty beams/no offsets is a no-op. Earlier creations/assignments remain if a later step fails; no cleanup, unit conversion, save or analysis occurs. Do not mutate definition values while held in hash sets.

Native routes: `CreateMemberOffsetSpec`, `AssignMemberSpecToBeam`.

```csharp
var beam = new Beam { Id = 17, EndOffset = OffsetSpecification.GlobalYEnd(-0.15) };
property.CreateAndAssignOffsetSpecifications(new System.Collections.Generic.HashSet<Beam> { beam }, 2);
```

## 23. PopulateMemberReleases

```csharp
IOSProperty PopulateMemberReleases(IEnumerable<Beam> forBeams)
```

Reads both member-end release specifications into the supplied managed beams.

- `forBeams`: Non-null finite sequence of non-null positive-ID beams. Equal candidates are queried once; original beam references are updated only after all reads succeed.

**Returns:** The same Property adapter for fluent chaining after the requested workflow completes.

When to use: Use before inspecting StartRelease/EndRelease. Calls the checked basic GetMemberReleaseSpec route for each end, preserving the source query choice. Projects Boolean release/spring fields while retaining every native 0/1/-1/-2/-3 mode in NativeSpecification, including mixed modes. Basic reads do not supply separate common/directional extended MP outputs; compatibility zero placeholders are not evidence of native zero. Use the existing extended typed query when those outputs are needed. Does not resolve native specification IDs, create/assign releases, save or convert units. Any read failure leaves all supplied beam release references unchanged.

Native routes: `GetMemberReleaseSpec`.

```csharp
var beams = new[] { new Beam { Id = 17 } };
property.PopulateMemberReleases(beams);
System.Console.WriteLine(beams[0].EndRelease.ReleaseType);
```

## 24. CreateAndAssignMemberEndReleases

```csharp
IOSProperty CreateAndAssignMemberEndReleases(HashSet<Beam> beams)
```

Creates and assigns basic full-release/spring specifications at supplied member ends.

- `beams`: Non-null set of non-null positive-ID beams. Null/no-release ends are skipped. Use independent release packets for start and end; applicable spring inputs must be finite and nonnegative.

**Returns:** The same Property adapter for fluent chaining after the requested workflow completes.

When to use: Use for the source basic 0/1 release-flag and six-spring workflow. Snapshots all end requests before the first native write and rejects common/directional partial-moment packets with NotSupportedException rather than silently dropping -2/-3 modes or MP factors. Each end is created with CreateMemberReleaseSpec, its positive ID is stored, then it is assigned to the member using integral-zero AssignMemberSpecToBeam success. Starts precede ends for each supplied beam. Translational springs retain force/length units; rotational springs retain moment/angle units. Native interpretation of combined flags/springs requires live qualification. Does not save, convert units, run analysis, roll back earlier operations or delete orphaned definitions after assignment failure.

Native routes: `CreateMemberReleaseSpec`, `AssignMemberSpecToBeam`.

```csharp
var releases = new MemberReleases();
releases.Releases.My = true;
releases.Releases.Mz = true;
var beam = new Beam { Id = 17, EndRelease = releases };
property.CreateAndAssignMemberEndReleases(new System.Collections.Generic.HashSet<Beam> { beam });
```

## 25. CreateAndAssignMemberEndReleases

```csharp
IOSProperty CreateAndAssignMemberEndReleases(HashSet<Beam> beams, int nThreads)
```

Creates and assigns basic full-release/spring specifications at supplied member ends.

- `beams`: Non-null set of non-null positive-ID beams. Null/no-release ends are skipped. Use independent release packets for start and end; applicable spring inputs must be finite and nonnegative.
- `nThreads`: Source-compatible degree value from 1 through 512. COM calls remain synchronous on the caller apartment; no worker-thread COM dispatch occurs.

**Returns:** The same Property adapter for fluent chaining after the requested workflow completes.

When to use: Use for the source basic 0/1 release-flag and six-spring workflow. Snapshots all end requests before the first native write and rejects common/directional partial-moment packets with NotSupportedException rather than silently dropping -2/-3 modes or MP factors. Each end is created with CreateMemberReleaseSpec, its positive ID is stored, then it is assigned to the member using integral-zero AssignMemberSpecToBeam success. Starts precede ends for each supplied beam. Translational springs retain force/length units; rotational springs retain moment/angle units. Native interpretation of combined flags/springs requires live qualification. Does not save, convert units, run analysis, roll back earlier operations or delete orphaned definitions after assignment failure.

Native routes: `CreateMemberReleaseSpec`, `AssignMemberSpecToBeam`.

```csharp
var releases = new MemberReleases();
releases.Releases.My = true;
releases.Releases.Mz = true;
var beam = new Beam { Id = 17, EndRelease = releases };
property.CreateAndAssignMemberEndReleases(new System.Collections.Generic.HashSet<Beam> { beam }, 2);
```

## Remaining scope

Private unit/catalog integrations, polymorphic section-definition creation/read-back and obsolete main functions remain separate work. Existing qualified single-material/release methods have parameter-type collisions with the source's mutable return types and cannot be duplicated by return type alone; this batch keeps those APIs unchanged and uses qualified observations internally. Partial-moment creation, caller-apartment marshaling and native engineering results remain qualification work. No native STAAD model is changed by offline tests or installed-package verification.

## Verification checkpoint

Passed a Release build with zero warnings/errors and **3,931 offline tests**, including **168 new workflow cases**. Isolated installed-package verification compiles and runs **642 selected XML examples**, including all **200 Property interface/adapter examples**, through a fresh PackageReference-only consumer and package cache. Examples run against managed native stand-ins; live mutation and engineering qualification remain pending.
