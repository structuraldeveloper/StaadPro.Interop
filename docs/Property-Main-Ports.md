# Property main-project ports

This guide records the preceding 25-method increment. Property now has 100 overloads; see [the management ports](Property-Management-Ports.md) and [the latest 25 workflows](Property-Workflows.md) for subsequent creation, assignment and population methods.


**Updated:** 23 October 2026

This increment adds **25 main-project method overloads** to both `IOSProperty` and the single `OSPropertyAdapter`, preserving function names, parameter names/types/order and return types. The existing 25 typed observations remain available, giving **50 public Property overloads**. The main project was inspected read-only.

These compatibility methods retain direct return types (`int`, `string`, arrays, sets, enums, Boolean or void). The earlier observation APIs return `PropertyQueryResult<T>`. A direct-return failure throws unless the individual contract explicitly specifies a sentinel, no-op or Boolean failure. No overload is counted twice because it is exposed on both the interface and adapter.

All new methods ship matching, fully expanded interface/adapter XML with summaries, every parameter, applicable returns, comparative usage, errors and executable examples. XML is installed beside the net481 DLL. Custom IOSProperty implementations must implement the 25 additional overloads.

## Shared behavior

Calls execute synchronously on the caller's established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.

Inventory/assignment buffers are bounded to 100000 entries, checked for element type, shape and positive IDs, and copied before returning. Positive counts are rechecked after list retrieval; zero counts skip fetching. Input collections are snapshotted before native access. No save, STAAD launch, refresh or analysis is implicit. Returned entity sets retain candidate object references and never populate missing native IDs. Native mutation is not transactional, and errors may leave partial assignments or orphan offset definitions.

The two obsolete main-project inactive-member methods remain excluded. Section-catalog hydration and typed/version-specific section-buffer interpretation are also separate work. `GetSectionPropertyValues` intentionally returns the main-project raw zero-padded array; nine documented section layouts change between 2024 and 2025/2026. Its array must not be interpreted through a universal positional schema.

## Added overloads

### 1. GetUnusedSectionsCount

```csharp
int GetUnusedSectionsCount();
```

Counts distinct section property definitions with no assigned analytical members.

Returns: The number of distinct section IDs whose native assigned-member count is exactly zero.

When to use: Choose this for an unused-definition count and GetUnusedSectionsIds when IDs are needed. Enumerates the section inventory and queries each assigned-member count; it is not the native total definition count. Negative counts throw instead of being classified as unused. No properties are deleted.

```csharp
System.Console.WriteLine(property.GetUnusedSectionsCount());
```

### 2. GetUnusedSectionsIds

```csharp
HashSet<int> GetUnusedSectionsIds();
```

Finds section property definitions with no assigned analytical members.

Returns: A new mutable set of positive native section IDs having exactly zero assigned members; an empty set is valid.

When to use: Use to inspect unused definitions before deciding whether to remove them. Enumerates the section inventory and queries each assigned-member count; duplicate property IDs collapse in the result. A native failure is never treated as an unused definition. Does not delete or assign properties.

```csharp
foreach (int id in property.GetUnusedSectionsIds()) System.Console.WriteLine(id);
```

### 3. GetBeamProperty

```csharp
int GetBeamProperty(int memberId);
```

Finds the first section property assigned to an analytical member.

- `memberId`: Positive analytical member number; not a property ID, list index or physical-member number.

Returns: The first matching positive property ID in native property-list order, or zero when no enumerated assignment contains the member.

When to use: Use to discover a section ID before GetSectionPropertyName or GetSectionPropertyValues. This managed search enumerates sections and their member assignments, matching the main-project implementation; it does not call the similarly named native dimensional GetBeamProperty API used by GetMemberSectionProperties. Zero does not distinguish an absent member from an unassigned member. Failures throw instead of returning zero.

```csharp
int propertyId = property.GetBeamProperty(17);
System.Console.WriteLine(propertyId);
```

### 4. GetPlateMaterialName

```csharp
string GetPlateMaterialName(int pId);
```

Reads the material name assigned to a plate using the native plate-specific query.

- `pId`: Positive analytical plate number; not a material index, property ID, node or solid number.

Returns: The exact native string, preserving case and whitespace; null or empty text is retained when returned by STAAD.

When to use: Use the main-project plate-specific compatibility method when the plate number is known. GetElementMaterialName is the existing status/data observation over a different native entry point; this port calls GetPlateMaterialName without alias fallback. Null or empty text cannot distinguish a missing plate from no material or an unavailable response. Does not retrieve constants or assign material.

```csharp
System.Console.WriteLine(property.GetPlateMaterialName(17));
```

### 5. GetSectionPropertyList

```csharp
int[] GetSectionPropertyList();
```

Lists the native section property reference IDs.

Returns: A new owned array of positive IDs in native order, retaining duplicates; an empty array when the count is zero.

When to use: Use to enumerate section definitions, not assigned members or thickness properties. Sizes from GetSectionPropertyCount and validates GetSectionPropertyList success, array shape and IDs. A positive-count retrieval rechecks the count and rejects observed changes. Zero count skips fetching. Counts above 100000 are rejected before allocation.

```csharp
foreach (int id in property.GetSectionPropertyList()) System.Console.WriteLine(id);
```

### 6. GetSectionPropertyName

```csharp
string GetSectionPropertyName(int propId);
```

Reads the native identification name of a section property.

- `propId`: Positive native section property reference ID, typically from GetSectionPropertyList; not a member number.

Returns: The explicitly written native name, preserving empty text, whitespace and case.

When to use: Use for a section label after locating its property ID. GetBeamProperty first resolves a member to a property ID. The native GetSectionPropertyName code must be zero; general error -1, no-definition -6025 and any other nonzero code throw. Null, non-string and unwritten string outputs throw. No section catalog or material identity is inferred.

```csharp
System.Console.WriteLine(property.GetSectionPropertyName(37));
```

### 7. GetSectionPropertyValues

```csharp
double[] GetSectionPropertyValues(int propId);
```

Reads the main-project raw, zero-padded native section-definition value buffer.

- `propId`: Positive native section property reference ID; not an analytical member number.

Returns: A new owned array sized by GetCountofSectionPropertyValuesEx, in native section-specific order and units, including zero padding.

When to use: Use only when the native section type and acquired STAAD version are already known. This compatibility port calls GetSectionPropertyValuesEx, not native GetSectionPropertyValues or the member-dimensional queries. Preserves the main-project zero-initialized full buffer and discards the returned section type after validating it. Unused padding and genuinely zero fields cannot be distinguished from unwritten zero slots. Layouts differ by section kind and some change between 2024 and 2025/2026; no universal Ax/inertia index, section parser, unit conversion or inferred physical axis is supplied. The native call must succeed; capacity must be 1-100000 and remain unchanged. Nonfinite or malformed outputs throw.

```csharp
double[] values = property.GetSectionPropertyValues(37);
System.Console.WriteLine(values.Length);
```

### 8. GetSectionPropertyAssignedBeams

```csharp
HashSet<Beam> GetSectionPropertyAssignedBeams(HashSet<Beam> allBeams, int propId);
```

Filters supplied beam objects by one section property assignment.

- `allBeams`: Non-null candidate set containing non-null Beam objects with positive analytical IDs. Objects are not cloned or hydrated.
- `propId`: Positive section property reference ID; not a member number or profile type code.

Returns: A new mutable set containing the original candidate objects whose IDs appear in the native assignment list.

When to use: Use when beam objects are already available and only their assignment subset is needed. Unlisted candidates are excluded and native IDs absent from allBeams are not synthesized. Sizes the native list with GetSectionPropertyAssignedBeamCount and rechecks the count; rejects invalid IDs and failures. Vendor section-assignment return tables are inconsistent: counts follow the native purpose/main usage, and a nonnegative integral list return or Boolean true is accepted with a fully validated buffer. Negative/false responses fail. This interpretation requires qualification on the acquired build.

```csharp
var beams = new System.Collections.Generic.HashSet<Beam> { new Beam { Id = 17 } };
System.Console.WriteLine(property.GetSectionPropertyAssignedBeams(beams, 37).Count);
```

### 9. GetThicknessPropertyList

```csharp
int[] GetThicknessPropertyList();
```

Lists native plate thickness property reference IDs.

Returns: A new owned array of positive thickness IDs in native order, retaining duplicates; an empty array for zero definitions.

When to use: Choose this inventory for thickness definitions; GetSectionPropertyList enumerates member sections. Sizes from GetThicknessPropertyCount, requires successful GetThicknessPropertyList, validates shape/IDs and rechecks positive counts. Counts above 100000 fail before allocation and zero skips fetching. Does not return thickness magnitudes or assigned plate IDs.

```csharp
foreach (int id in property.GetThicknessPropertyList()) System.Console.WriteLine(id);
```

### 10. GetThicknessPropertyAssignedPlates

```csharp
HashSet<Plate> GetThicknessPropertyAssignedPlates(HashSet<Plate> allPlates, int propId);
```

Filters supplied plate objects by one thickness property assignment.

- `allPlates`: Non-null candidate set of non-null plates with positive analytical IDs. Returned objects are these original instances.
- `propId`: Positive native thickness property reference ID; not a plate number or member section ID.

Returns: A new mutable set of original candidate plates listed by the native assignment query.

When to use: Use for assignment membership when plate objects are already available; GetAveragePlateThickness reads a magnitude instead. Sizes with GetThicknessPropertyAssignedPlateCount, requires successful GetThicknessPropertyAssignedPlateList, validates IDs and rechecks the count. Native IDs absent from the candidates are not constructed. Zero count produces an empty set without a list call. No managed plate properties are changed.

```csharp
var plates = new System.Collections.Generic.HashSet<Plate> { new Plate { Id = 17 } };
System.Console.WriteLine(property.GetThicknessPropertyAssignedPlates(plates, 45).Count);
```

### 11. GetBeamsWithMissingSectionProperty

```csharp
HashSet<Beam> GetBeamsWithMissingSectionProperty(HashSet<Beam> allBeams, IEnumerable<int> propIds);
```

Finds supplied beams absent from the assignments of the supplied property IDs.

- `allBeams`: Non-null candidate set of non-null Beam objects with positive analytical IDs; original instances are retained.
- `propIds`: Non-null sequence of positive section property reference IDs. Enumerated once and validated completely before native access.

Returns: A new mutable set of the candidate objects not listed by any supplied property; an empty property sequence returns all candidates.

When to use: Use as a missing-assignment check only when propIds includes every relevant section definition. With a subset, the result means unassigned to that subset and can include entities assigned elsewhere. Combines the same checked native count/list queries as GetSectionPropertyAssignedBeams; each list is bounded and count-rechecked. Does not validate member/plate existence, change objects, or assume a global unassigned status. Input collections are never modified.

```csharp
var candidates = new System.Collections.Generic.HashSet<Beam> { new Beam { Id = 17 } };
System.Console.WriteLine(property.GetBeamsWithMissingSectionProperty(candidates, new[] { 37 }).Count);
```

### 12. GetPlatesWithMissingThicknessProperty

```csharp
HashSet<Plate> GetPlatesWithMissingThicknessProperty(HashSet<Plate> allPlates, IEnumerable<int> propIds);
```

Finds supplied plates absent from the assignments of the supplied property IDs.

- `allPlates`: Non-null candidate set of non-null Plate objects with positive analytical IDs; original instances are retained.
- `propIds`: Non-null sequence of positive thickness property reference IDs. Enumerated once and validated completely before native access.

Returns: A new mutable set of the candidate objects not listed by any supplied property; an empty property sequence returns all candidates.

When to use: Use as a missing-assignment check only when propIds includes every relevant thickness definition. With a subset, the result means unassigned to that subset and can include entities assigned elsewhere. Combines the same checked native count/list queries as GetThicknessPropertyAssignedPlates; each list is bounded and count-rechecked. Does not validate member/plate existence, change objects, or assume a global unassigned status. Input collections are never modified.

```csharp
var candidates = new System.Collections.Generic.HashSet<Plate> { new Plate { Id = 17 } };
System.Console.WriteLine(property.GetPlatesWithMissingThicknessProperty(candidates, new[] { 45 }).Count);
```

### 13. GetAveragePlateThickness

```csharp
double GetAveragePlateThickness(int plateId);
```

Computes the rounded arithmetic mean of four native plate-corner thickness slots.

- `plateId`: Positive analytical plate number, not a thickness property ID.

Returns: The sum of four native thickness values divided by four, rounded to five decimal places with midpoint-to-even rounding, in unchanged native length units.

When to use: Use for the main-project four-slot arithmetic average, not an area-weighted or integration-point thickness. Calls GetPlateThickness once and requires native code zero and four explicitly written finite values. No interpolation, triangular-corner substitution, sign adjustment or unit conversion is performed; an unwritten fourth slot throws. Native missing-property and invalid-plate errors throw rather than yielding a zero thickness. The intermediate sum must remain finite.

```csharp
System.Console.WriteLine(property.GetAveragePlateThickness(17));
```

### 14. GetMemberSpecification

```csharp
MemberSpecificationType GetMemberSpecification(int msId);
```

Reads the native axial behavior specification of an analytical member.

- `msId`: Positive analytical member number forwarded to GetMemberSpecCode. The preserved main-project parameter name does not mean a specification record ID.

Returns: The defined native specification: Other=-1, Truss=0, TensionOnly=1, CompressionOnly=2, CableOnly=3 or Joist=4.

When to use: Use for member behavior, not section, material, release or offset classification. Requires Boolean/integral native success and an explicitly written known code. Unknown or unwritten codes throw rather than being cast silently. No member attributes, model state, units or results are changed.

```csharp
System.Console.WriteLine(property.GetMemberSpecification(17));
```

### 15. AssignBeamProperty

```csharp
void AssignBeamProperty(int memberId, int propertyId);
```

Assigns an existing section property to one analytical member.

- `memberId`: Positive analytical member ID, not a property reference ID.
- `propertyId`: Positive existing section property reference ID; the method does not create a section.

When to use: Use to assign an existing property; section inventory/read methods do not mutate assignments. Calls native AssignBeamProperty once with the scalar or copied array. Native zero is success and any nonzero code throws, including invalid member -3006, invalid property -6001 and assignment error -6002. This changes the live model without saving or rollback; errors may follow partial changes. No managed Beam object is updated.

```csharp
property.AssignBeamProperty(17, 37);
```

### 16. AssignBeamProperty

```csharp
void AssignBeamProperty(int[] memberIds, int propertyId);
```

Assigns an existing section property to a batch of analytical members.

- `memberIds`: Analytical member IDs. Null or empty arrays are a no-op; otherwise copied and all entries must be positive. Order and duplicates are forwarded.
- `propertyId`: Positive existing section property reference ID; the method does not create a section.

When to use: Use to assign an existing property; section inventory/read methods do not mutate assignments. Calls native AssignBeamProperty once with the scalar or copied array. Native zero is success and any nonzero code throws, including invalid member -3006, invalid property -6001 and assignment error -6002. This changes the live model without saving or rollback; errors may follow partial changes. Null/empty arrays return before binding or property-ID validation.

```csharp
property.AssignBeamProperty(new[] { 17, 41 }, 37);
```

### 17. AssignMaterialToMember

```csharp
void AssignMaterialToMember(string materialName, int memberId);
```

Assigns an existing named material to one analytical member.

- `materialName`: Exact native material name; case and surrounding whitespace are preserved. Null/empty/whitespace-only names are a no-op. Nonblank names must not contain NUL.
- `memberId`: Positive analytical member number, validated only for a nonblank material name.

When to use: Use to change the actual member material assignment. SetMaterialName changes only the Property creation context and cannot substitute for assignment. Calls native AssignMaterialToMember once with the exact name and scalar or copied array; false/zero fails with InvalidOperationException. No trimming, material creation, conversion, save, rollback or managed entity update occurs. A failure can have partial native effects. No-op inputs return before binding/native access.

```csharp
property.AssignMaterialToMember("STEEL", 17);
```

### 18. AssignMaterialToMember

```csharp
void AssignMaterialToMember(string materialName, int[] memberIds);
```

Assigns an existing named material to a batch of analytical members.

- `materialName`: Exact native material name; case and surrounding whitespace are preserved. Null/empty/whitespace-only names are a no-op. Nonblank names must not contain NUL.
- `memberIds`: Member IDs copied before dispatch; positive entries required. Null or empty arrays are a no-op.

When to use: Use to change the actual member material assignment. SetMaterialName changes only the Property creation context and cannot substitute for assignment. Calls native AssignMaterialToMember once with the exact name and scalar or copied array; false/zero fails with InvalidOperationException. No trimming, material creation, conversion, save, rollback or managed entity update occurs. A failure can have partial native effects. No-op inputs return before binding/native access.

```csharp
property.AssignMaterialToMember("STEEL", new[] { 17, 41 });
```

### 19. SetMaterialName

```csharp
void SetMaterialName(string materialName);
```

Sets the native Property material context for subsequent creation operations.

- `materialName`: Exact existing native material name, preserving case and whitespace. Null/empty/whitespace-only names are a no-op; nonblank names must not contain NUL.

When to use: Use before a native property creation API that consumes the current material context. AssignMaterialToMember instead changes member assignments. Calls SetMaterialName once and requires native Boolean/integral success; the documented integer success code is one. Does not create material, assign members, restore the previous context or save the model. The context is shared native state: serialization covers this call, not a later separate creation transaction. No-op names return before binding checks.

```csharp
property.SetMaterialName("STEEL");
```

### 20. AssignMemberEndOffsets

```csharp
bool AssignMemberEndOffsets(int memberId, double startY, double endY);
```

Creates and assigns global-Y offsets at both analytical member ends.

- `memberId`: Positive analytical member number, not a specification ID.
- `startY`: Finite signed global-Y start offset in the current native length units. Zero is a valid explicit offset.
- `endY`: Finite signed global-Y end offset in the same native length units. Zero is valid.

Returns: True only when both positive specification IDs are created and both native assignment calls return zero; false for native error codes, COM/binding failures.

When to use: Use to create and assign both end specifications; AssignMemberEndOffset changes one end. Creates the start then end specification with native global selector zero and X=Z=0, then assigns start followed by end. This mutates the live model; failure can leave orphan definitions or partial assignments. No rollback, cleanup, save or managed entity update occurs. Validation, stale/disposed bindings and malformed nonintegral native responses throw; the Boolean result does not certify physical placement or engineering suitability.

```csharp
System.Console.WriteLine(property.AssignMemberEndOffsets(17, 0.15, -0.20));
```

### 21. AssignMemberEndOffsets

```csharp
bool AssignMemberEndOffsets(int[] memberIds, double startY, double endY);
```

Creates and assigns global-Y offsets at both analytical member ends.

- `memberIds`: Positive analytical member IDs, copied before dispatch; null/empty arrays return false without native calls.
- `startY`: Finite signed global-Y start offset in the current native length units. Zero is a valid explicit offset.
- `endY`: Finite signed global-Y end offset in the same native length units. Zero is valid.

Returns: True only when both positive specification IDs are created and both native assignment calls return zero; false for native error codes, COM/binding failures or null/empty member arrays.

When to use: Use to create and assign both end specifications; AssignMemberEndOffset changes one end. Creates the start then end specification with native global selector zero and X=Z=0, then assigns start followed by end. Each specification is created once for the whole copied member array, not once per member. This mutates the live model; failure can leave orphan definitions or partial assignments. No rollback, cleanup, save or managed entity update occurs. Validation, stale/disposed bindings and malformed nonintegral native responses throw; the Boolean result does not certify physical placement or engineering suitability.

```csharp
System.Console.WriteLine(property.AssignMemberEndOffsets(new[] { 17, 41 }, 0.15, -0.20));
```

### 22. AssignMemberEndOffset

```csharp
bool AssignMemberEndOffset(int memberId, int location, double offsetY);
```

Creates and assigns a global-Y offset at one analytical member end.

- `memberId`: Positive analytical member number.
- `location`: Native member-end selector: zero for start, one for end. Other values are rejected.
- `offsetY`: Finite signed global-Y offset in current native length units. Zero creates an explicit zero offset.

Returns: True when a positive specification ID is created and its native assignment returns zero; false for native error codes, COM/binding failures.

When to use: Choose this for one end and AssignMemberEndOffsets for both ends. Calls CreateMemberOffsetSpec once using global selector zero and X=Z=0, then AssignMemberSpecToBeam once with the scalar or copied array. Existing opposite-end state is not explicitly changed by this method. Native state is mutated without rollback, cleanup, save or managed entity synchronization; partial assignment or an orphan definition can remain on failure. Validation/stale-binding/malformed-response errors throw. The offset uses analytical member incidence, not a physical-member endpoint.

```csharp
System.Console.WriteLine(property.AssignMemberEndOffset(17, 1, -0.20));
```

### 23. AssignMemberEndOffset

```csharp
bool AssignMemberEndOffset(int memberId, OffsetLocation location, double offsetY);
```

Creates and assigns a global-Y offset at one analytical member end.

- `memberId`: Positive analytical member number.
- `location`: OffsetLocation.Start (native zero) or OffsetLocation.End (native one); undefined enum values are rejected.
- `offsetY`: Finite signed global-Y offset in current native length units. Zero creates an explicit zero offset.

Returns: True when a positive specification ID is created and its native assignment returns zero; false for native error codes, COM/binding failures.

When to use: Choose this for one end and AssignMemberEndOffsets for both ends. The typed overload forwards the exact enum code to the integer overload. Calls CreateMemberOffsetSpec once using global selector zero and X=Z=0, then AssignMemberSpecToBeam once with the scalar or copied array. Existing opposite-end state is not explicitly changed by this method. Native state is mutated without rollback, cleanup, save or managed entity synchronization; partial assignment or an orphan definition can remain on failure. Validation/stale-binding/malformed-response errors throw. The offset uses analytical member incidence, not a physical-member endpoint.

```csharp
System.Console.WriteLine(property.AssignMemberEndOffset(17, OffsetLocation.End, -0.20));
```

### 24. AssignMemberEndOffset

```csharp
bool AssignMemberEndOffset(int[] memberIds, int location, double offsetY);
```

Creates and assigns a global-Y offset at one analytical member end.

- `memberIds`: Positive analytical member IDs copied before dispatch. Null or empty arrays return false without native calls.
- `location`: Native member-end selector: zero for start, one for end. Other values are rejected.
- `offsetY`: Finite signed global-Y offset in current native length units. Zero creates an explicit zero offset.

Returns: True when a positive specification ID is created and its native assignment returns zero; false for native error codes, COM/binding failures or null/empty arrays.

When to use: Choose this for one end and AssignMemberEndOffsets for both ends. Calls CreateMemberOffsetSpec once using global selector zero and X=Z=0, then AssignMemberSpecToBeam once with the scalar or copied array. Existing opposite-end state is not explicitly changed by this method. Native state is mutated without rollback, cleanup, save or managed entity synchronization; partial assignment or an orphan definition can remain on failure. Validation/stale-binding/malformed-response errors throw. The offset uses analytical member incidence, not a physical-member endpoint.

```csharp
System.Console.WriteLine(property.AssignMemberEndOffset(new[] { 17, 41 }, 1, -0.20));
```

### 25. AssignMemberEndOffset

```csharp
bool AssignMemberEndOffset(int[] memberIds, OffsetLocation location, double offsetY);
```

Creates and assigns a global-Y offset at one analytical member end.

- `memberIds`: Positive analytical member IDs copied before dispatch. Null or empty arrays return false without native calls.
- `location`: OffsetLocation.Start (native zero) or OffsetLocation.End (native one); undefined enum values are rejected.
- `offsetY`: Finite signed global-Y offset in current native length units. Zero creates an explicit zero offset.

Returns: True when a positive specification ID is created and its native assignment returns zero; false for native error codes, COM/binding failures or null/empty arrays.

When to use: Choose this for one end and AssignMemberEndOffsets for both ends. The typed overload forwards the exact enum code to the integer overload. Calls CreateMemberOffsetSpec once using global selector zero and X=Z=0, then AssignMemberSpecToBeam once with the scalar or copied array. Existing opposite-end state is not explicitly changed by this method. Native state is mutated without rollback, cleanup, save or managed entity synchronization; partial assignment or an orphan definition can remain on failure. Validation/stale-binding/malformed-response errors throw. The offset uses analytical member incidence, not a physical-member endpoint.

```csharp
System.Console.WriteLine(property.AssignMemberEndOffset(new[] { 17, 41 }, OffsetLocation.End, -0.20));
```

## Reference corrections and validation boundaries

- `GetBeamProperty` preserves the main managed inventory search and zero sentinel; it is not an alias of the similarly named native dimensional getter.
- `GetMemberSpecification` preserves the parameter name `msId`, but the vendor contract identifies its input as a member number, not a specification record ID. Unknown and unwritten values fail.
- Negative assigned-member counts no longer label a section unused. Failed list/name/thickness operations no longer expose initialized defaults as data.
- Section-assignment help repeats an inconsistent profile/table return description. Nonnegative counts follow the native purpose and main implementation. List returns accept nonnegative integral evidence or true with validated output; negative codes/false fail. Live qualification is required.
- The raw section buffer retains the main zero-padding convention; success/shape/nonfinite checks cannot detect unwritten zero slots or infer section kind. Callers need a qualified section/version schema.
- Assignment methods check native status: zero means success for beam/spec assignment, whereas material assignment/context uses Boolean/integral success. Offset helpers check positive created specification IDs and zero assignment statuses; COM/binding errors return false. Invalid inputs, stale bindings and malformed responses throw rather than being swallowed.
- Four-slot plate-thickness averaging and midpoint-to-even rounding to five decimal places match the main implementation. Triangular fourth-slot behavior must be qualified; no corner is invented.
- Managed tests and installed-package examples use stand-ins only. Native COM marshaling, partial-mutation behavior, units and engineering values must be qualified on disposable models for each advertised build.

Native contract evidence: installed Bentley OpenSTAAD 2025 help groups for general section/thickness, specifications, assignments and materials, cross-checked with the repository 2024-2026 contract inventory. No native model is changed by automated verification.
