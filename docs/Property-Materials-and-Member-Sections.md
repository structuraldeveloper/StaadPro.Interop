# Property materials and member sections

Incorporated **25 Property functions**: the eight remaining P3 material operations, eight remaining P4 member-section queries and nine remaining P5 mutations. This checkpoint reached **150 overloads**; the [latest creation/specification increment](Property-Section-and-Specification-Creation.md) reached **175**; the [latest fireproofing/attribute/UPT increment](Property-Fireproofing-Attributes-and-UPT.md) brings the current total to **200** and reaches **91 of 204 active native routes**, with **113 remaining**. All P2/P3/P4/P5 allocations have managed routes; this is implementation coverage, not live native or engineering qualification.

Passed a Release build with zero warnings/errors and **4,346 offline tests**, including **190 new cases**. The isolated PackageReference-only consumer verifies **742 selected XML examples**, including all **300 Property interface/adapter examples**. New mutation examples are guarded; separate installed-consumer checks invoke all eleven mutations against managed stand-ins. No live STAAD model is changed by verification.

Added immutable explicit material definitions/strengths, orthotropic constants, member-section snapshots and named mutation acknowledgements retaining actual native evidence and requested identity. Kept Steel Fu/Fy/Rt/Ry order distinct from extended Fy/Fu/Ry/Rt/Fcu order. Added bounded owned solid-ID snapshots, strict written-output checks, operation-specific absence/error handling and native-version section layouts. A successful mutation acknowledgement does not prove design-strength persistence, save the model or provide rollback.

## Contracts and native evidence

These additions follow the remaining P3, P4 and P5 allocations in the [multi-version roadmap](reports/OpenSTAAD-Property-Multi-Version-Architecture-and-Plan.md). The [contract index](contracts/PropertyRoadmapContractIndex.json) records all 25 exact managed signatures, native routes, parameters, outcomes and examples. Native declarations, parameter descriptions, tables and operation-specific return contracts come from the separately preserved 2024/2025/2026 Bentley help in the [comparison evidence](reports/data/OpenSTAAD-Property-2024-2026-Comparison.json); no public signature is inferred from a function-name match alone.

The single non-partial OSPropertyAdapter delegates to an internal reader using the same captured receiver, serialization gate and before/after binding checks as existing Property queries. Every dependent count/list dispatch checks binding independently. Unknown native layouts or undocumented results fail explicitly. New public signatures use no ref/out, tuples, object payloads or mutable output arrays. Existing 125 overloads remain available, including the property-ID compatibility query GetSectionPropertyValues(int propId).

## Inputs and results

| Type | Meaning |
| --- | --- |
| `PropertyQueryResult<T>` | Inspect IsSuccessful/Status before Data; failed queries expose no default payload. Actual Boolean and integral evidence remain separate. |
| `PropertyMutationResult` | Operation, status, actual native return, target scope, owned requested IDs/name and applicable specification/profile/created-section identity. CreatedPropertyId is populated only for successful assigned-profile creation. |
| `PropertyMutationStatus` | Succeeded, Created, Updated, NativeFailure or InvalidInput, interpreted independently for each operation. Material zero/one means Created/Updated, never an ID. |
| `IsotropicMaterialDefinition` | Required exact name and explicit E, Poisson, G, weight density, thermal expansion and damping. No defaults, implicit unit conversion or derived G. |
| `IsotropicMaterialStrengths` | Explicit Fy, Fu, Ry, Rt and Fcu for the extended creator. Strengths use native stress units and ratios are dimensionless. |
| `SteelMaterialStrengths` | Explicit Fu, Fy, Rt and Ry in the Steel creator's different native order. |
| `Orthotropic3DMaterialProperties` | Exact native name/number and eighteen named scalar constants, six groups of three. First/second/third retain native order without guessing physical axes/planes. |
| `MemberSectionValues` | Requested member ID, native version/type and read-only applicable labels/values from the fixed 24-double buffer. Padding is excluded; no property ID/country/catalog identity is fabricated. |
| `AssignedSectionProfile` | Angle=0, DoubleAngle=1, Beam=2, Column=3, Channel=4. These selectors are not property reference IDs. |
| `IsotropicMaterialType` | NotSpecified=0, Steel=1, Concrete=2, Aluminum=3, Timber=4. Classification does not supply elastic/design constants. |

Numeric material inputs must be finite; E/G positive, -1 < Poisson < 0.5, weight density/damping/strengths/ratios nonnegative. Thermal expansion may be negative. Names are exact nonblank strings without NUL characters. These checks do not certify engineering adequacy. Set input constants in the intended native creation convention; no ambient unit change occurs.

Assignment inputs enumerate once, stop at 100001 items and reject counts outside 1..100000 before writing. Order/duplicates are retained. Solid reads enforce caller allocation limits and check before/after counts, which cannot detect all same-count external edits. Empty native assignment inventory is successful empty data, without proving a named material exists.

## Functions

### GetOrthotropic3DMaterialCount

```csharp
PropertyQueryResult<int> GetOrthotropic3DMaterialCount()
```

Native route: `GetOrthotropic3DMaterialCount` (P3). Reads the count of orthotropic 3D material definitions.

Returns: Success with the actual nonnegative count, including zero; the integral response is retained as evidence.

When to use: size or inspect the orthotropic 3D inventory. A count is not an upper bound on native material number IDs and does not establish their indexing. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.

```csharp
var result = property.GetOrthotropic3DMaterialCount();
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
```

### GetOrthotropic3DMaterialProperties

```csharp
PropertyQueryResult<Orthotropic3DMaterialProperties> GetOrthotropic3DMaterialProperties(int nativeMaterialNumber)
```

Native route: `GetOrthotropic3DMaterialProperties` (P3). Reads all eighteen constants of one native orthotropic 3D material.

Parameters:

- `nativeMaterialNumber`: Nonnegative native material number ID established by the caller. It is not inferred from the 3D count or a managed zero/one-based list index.

Returns: Success with the exact native name and eighteen owned immutable constants; native null/DBNull/empty name yields NotFound with no Data.

When to use: obtain anisotropic constants without an isotropic E/Poisson-to-G relation. Sends six sentinel double arrays of exactly three slots each, including density, alpha and damping. Every returned array must be rank one, exactly length three, floating-point and finite. Native first/second/third order is retained without guessing physical axis/plane labels. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.

```csharp
var result = property.GetOrthotropic3DMaterialProperties(0);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Name);
else System.Console.WriteLine(result.Status);
```

### RemoveMaterialFromBeam

```csharp
PropertyMutationResult RemoveMaterialFromBeam(int memberId)
```

Native route: `RemoveMaterialFromBeam` (P3). Removes a material assignment from one analytical member.

Parameters:

- `memberId`: Positive analytical member ID in the acquired model; not a section property ID, table number or section type.

Returns: Succeeded for integral zero, NativeFailure for -1; retains the requested member ID and actual return code.

When to use: deliberately clear a member material assignment while retaining the material definition. Does not delete a definition or replace the assignment with a default material. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.

```csharp
bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
if (applyChanges)
{
    var result = property.RemoveMaterialFromBeam(17);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### GetSolidMaterialName

```csharp
PropertyQueryResult<string> GetSolidMaterialName(int solidId)
```

Native route: `GetSolidMaterialName` (P3). Reads the exact material name assigned to an analytical solid.

Parameters:

- `solidId`: Positive analytical solid ID in the acquired model; not a plate/member ID or material number.

Returns: Success with the exact native string. Null/DBNull/empty names yield EmptyOrUnavailable because no-assignment and missing-entity states cannot be distinguished.

When to use: inspect solid material assignment without hydrating solid geometry. Retains whitespace and does not reinterpret the material name as its classification. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.

```csharp
var result = property.GetSolidMaterialName(71);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
```

### GetIsotropicMaterialAssignedSolidCount

```csharp
PropertyQueryResult<int> GetIsotropicMaterialAssignedSolidCount(string materialName)
```

Native route: `GetIsotropicMaterialAssignedSolidCount` (P3). Reads the count of solids assigned to a named isotropic material.

Parameters:

- `materialName`: Exact nonblank native material name without NUL characters. Case and whitespace are preserved; no material is created by this input.

Returns: Success with the nonnegative native count; zero does not prove the named material exists. Negative/Boolean/fractional counts are malformed.

When to use: inspect assignment size before requesting IDs. The help documents a count, without a separate missing-material code. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.

```csharp
var result = property.GetIsotropicMaterialAssignedSolidCount("STEEL");
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
```

### GetIsotropicMaterialAssignedSolidIds

```csharp
PropertyQueryResult<IReadOnlyList<int>> GetIsotropicMaterialAssignedSolidIds(string materialName, int maximumResults = 100000)
```

Native route: `GetIsotropicMaterialAssignedSolidList` (P3). Reads a bounded owned snapshot of solid IDs assigned to an isotropic material.

Parameters:

- `materialName`: Exact nonblank native material name without NUL characters. Case and whitespace are preserved; no material is created by this input.
- `maximumResults`: Allocation limit from 1 through 100000, default 100000. A larger native count returns LimitExceeded before allocating/retrieving a list.

Returns: Success with a read-only owned ID list retaining native order and duplicates; zero count gives an empty list without retrieval. False retrieval gives NativeFailure; changed before/after counts give StateChanged.

When to use: enumerate assignment IDs without loading solids. Uses GetIsotropicMaterialAssignedSolidCount before and after GetIsotropicMaterialAssignedSolidList; checks rank-one Int32 shape, exact length and positive written IDs. Every native dispatch rechecks captured receiver binding. Count equality cannot rule out same-count external edits; no model-wide atomic snapshot is promised. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.

```csharp
var result = property.GetIsotropicMaterialAssignedSolidIds("STEEL");
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
else System.Console.WriteLine(result.Status);
```

### SetTypeToIsotropicMaterial

```csharp
PropertyMutationResult SetTypeToIsotropicMaterial(string materialName, IsotropicMaterialType materialType)
```

Native route: `SetTypeToIsotropicMaterial` (P3). Sets the classification of an existing named isotropic material.

Parameters:

- `materialName`: Exact nonblank native material name without NUL characters. Case and whitespace are preserved; no material is created by this input.
- `materialType`: Defined classification: NotSpecified=0, Steel=1, Concrete=2, Aluminum=3 or Timber=4. Unknown enum values are rejected before native access.

Returns: Succeeded for true/integral one, NativeFailure for false/integral zero, with exact name and actual return representation.

When to use: change material classification without creating a material or supplying elastic/design constants. Acknowledgement does not mean that E, G, density or strengths were changed. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.

```csharp
bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
if (applyChanges)
{
    var result = property.SetTypeToIsotropicMaterial("STEEL", StaadPro.Interop.Enums.Property.IsotropicMaterialType.Steel);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### GetTypeForIsotropicMaterial

```csharp
PropertyQueryResult<IsotropicMaterialType> GetTypeForIsotropicMaterial(string materialName)
```

Native route: `GetTypeForIsotropicMaterial` (P3). Reads the native classification of an isotropic material.

Parameters:

- `materialName`: Exact nonblank native material name without NUL characters. Case and whitespace are preserved; no material is created by this input.

Returns: Success with a defined classification 0 through 4; false/zero gives NotFound. An unwritten or unknown type on success throws.

When to use: inspect classification independently from material constants. Native NotSpecified=0 is successful data, distinct from a false return indicating no such material. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.

```csharp
var result = property.GetTypeForIsotropicMaterial("STEEL");
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
```

### GetSectionPropertiesForProfile

```csharp
PropertyQueryResult<BeamSectionProperties> GetSectionPropertiesForProfile(AssignedSectionProfile profileType)
```

Native route: `GetSectionPropertyValues` (P4). Reads the ten native section values for an ASSIGN profile selector.

Parameters:

- `profileType`: Defined ASSIGN profile selector: Angle=0, DoubleAngle=1, Beam=2, Column=3 or Channel=4. Not a property reference ID.

Returns: Success for native zero with width/depth, Ax/Ay/Az, native Ix/Iy/Iz and flange/web thickness. -3001 gives NotFound and -6022 NotAssigned; failures have no Data.

When to use: query GetSectionPropertyValues using its documented profile selector, rather than a member or property ID. The older GetSectionPropertyValues(int propId) compatibility method calls the different native GetSectionPropertyValuesEx property-ID route and remains unchanged. All ten scalar outputs must be explicitly written finite floating-point values. Length, area and inertia slots retain native units and names; no physical-axis reinterpretation is inferred. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.

```csharp
var result = property.GetSectionPropertiesForProfile(StaadPro.Interop.Enums.Property.AssignedSectionProfile.Beam);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.AxialArea);
else System.Console.WriteLine(result.Status);
```

### GetBeamSectionPropertyRefNo

```csharp
PropertyQueryResult<int> GetBeamSectionPropertyRefNo(int memberId)
```

Native route: `GetBeamSectionPropertyRefNo` (P4). Reads the section property reference assigned to a member.

Parameters:

- `memberId`: Positive analytical member ID in the acquired model; not a section property ID, table number or section type.

Returns: Success with a positive native property reference. Native zero gives EmptyOrUnavailable, without distinguishing missing member from missing assignment.

When to use: bridge a member identity to existing property-ID readers. Negative results are undocumented and throw. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.

```csharp
var result = property.GetBeamSectionPropertyRefNo(17);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
```

### GetBeamSectionPropertyValuesEx

```csharp
PropertyQueryResult<MemberSectionValues> GetBeamSectionPropertyValuesEx(int memberId)
```

Native route: `GetBeamSectionPropertyValuesEx` (P4). Reads the version-decoded section-value packet directly from a member.

Parameters:

- `memberId`: Positive analytical member ID in the acquired model; not a section property ID, table number or section type.

Returns: Success with an immutable member identity, native type/version, applicable field labels and matching read-only values. False/zero gives NativeFailure with no Data.

When to use: inspect assigned member section parameters without synthesizing a property reference, country, catalog identity or area. Allocates the documented fixed 24-double sentinel buffer, validates its exact rank/type/length, and validates only applicable slots using the reviewed native STAAD 24/25/26 layout selected at execution from wrapper.StaadVersion. Unknown versions/types fail explicitly. Unused padding is excluded. Required nonfinite/unwritten values throw. No extra entity lookup or atomic external-edit guarantee is implied. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.

```csharp
var result = property.GetBeamSectionPropertyValuesEx(17);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.NativePropertyType);
else System.Console.WriteLine(result.Status);
```

### GetSectionTableNo

```csharp
PropertyQueryResult<int> GetSectionTableNo(int memberId)
```

Native route: `GetSectionTableNo` (P4). Reads the native section table number for a member.

Parameters:

- `memberId`: Positive analytical member ID in the acquired model; not a section property ID, table number or section type.

Returns: Success with a nonnegative native table number (including zero); -3001 is NotFound, -6004 InvalidReference (profile database section absent), and -6022 NotAssigned.

When to use: inspect table identity for an assigned member. This output is not a country code, property reference ID or section type code. Unknown nonnegative codes are retained. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.

```csharp
var result = property.GetSectionTableNo(17);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
```

### GetBeamSectionPropertyTypeNo

```csharp
PropertyQueryResult<int> GetBeamSectionPropertyTypeNo(int memberId)
```

Native route: `GetBeamSectionPropertyTypeNo` (P4). Reads the native section property type code for a member.

Parameters:

- `memberId`: Positive analytical member ID in the acquired model; not a section property ID, table number or section type.

Returns: Success with a positive raw native type code, including unknown positive codes. Zero gives NativeFailure; undocumented negatives throw.

When to use: inspect section type without decoding its value layout. A retained unknown type does not imply layout support in GetBeamSectionPropertyValuesEx. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.

```csharp
var result = property.GetBeamSectionPropertyTypeNo(17);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
```

### GetBeamSectionName

```csharp
PropertyQueryResult<string> GetBeamSectionName(int memberId)
```

Native route: `GetBeamSectionName` (P4). Reads the exact section name assigned to a member.

Parameters:

- `memberId`: Positive analytical member ID in the acquired model; not a section property ID, table number or section type.

Returns: Success with the exact nonempty native string; null/DBNull/empty yields EmptyOrUnavailable, without guessing whether the member or assignment is missing.

When to use: obtain the native base name, which can be shared by differently modified assignments. Does not construct a catalog designation or parse dimensions from the string. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.

```csharp
var result = property.GetBeamSectionName(17);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
```

### GetBeamSectionDisplayName

```csharp
PropertyQueryResult<string> GetBeamSectionDisplayName(int memberId)
```

Native route: `GetBeamSectionDisplayName` (P4). Reads the exact display section name assigned to a member.

Parameters:

- `memberId`: Positive analytical member ID in the acquired model; not a section property ID, table number or section type.

Returns: Success with the exact nonempty native string; null/DBNull/empty yields EmptyOrUnavailable, without guessing whether the member or assignment is missing.

When to use: show the native assignment description, which may include modifiers and spacing. Does not construct a catalog designation or parse dimensions from the string. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.

```csharp
var result = property.GetBeamSectionDisplayName(17);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
```

### GetCountryTableNo

```csharp
PropertyQueryResult<int> GetCountryTableNo(int memberId)
```

Native route: `GetCountryTableNo` (P4). Reads the native country code associated with a member section.

Parameters:

- `memberId`: Positive analytical member ID in the acquired model; not a section property ID, table number or section type.

Returns: Success with a nonnegative raw country code, retaining zero and unknown positive codes. -3001 gives NotFound; -6022 gives NotAssigned.

When to use: inspect the country of an assigned member section. The input is a member ID; the output is a country code, not a section table number. No forced conversion to the repository Country enum occurs. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.

```csharp
var result = property.GetCountryTableNo(17);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
```

### CreateAssignProfileProperty

```csharp
PropertyMutationResult CreateAssignProfileProperty(AssignedSectionProfile profileType)
```

Native route: `CreateAssignProfileProperty` (P5). Creates an ASSIGN profile section definition.

Parameters:

- `profileType`: Defined ASSIGN profile selector: Angle=0, DoubleAngle=1, Beam=2, Column=3 or Channel=4. Not a property reference ID.

Returns: Created with a separate positive CreatedPropertyId on success; zero is NativeFailure and -6008 InvalidInput. The actual native code and requested profile are retained.

When to use: create one of the five ASSIGN profile definitions. Does not assign members or fabricate section dimensions. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.

```csharp
bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
if (applyChanges)
{
    var result = property.CreateAssignProfileProperty(StaadPro.Interop.Enums.Property.AssignedSectionProfile.Beam);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### AssignElementSpecToPlate

```csharp
PropertyMutationResult AssignElementSpecToPlate(IEnumerable<int> plateIds, int specificationId)
```

Native route: `AssignElementSpecToPlate` (P5). Assigns an existing element specification to explicit plate IDs.

Parameters:

- `plateIds`: Finite sequence of 1 through 100000 positive analytical plate IDs. Enumerated once with a hard bound; order/duplicates retained. Empty/null/invalid sequences are rejected before native writes.
- `specificationId`: Positive existing native element specification ID; no specification is created or inferred.

Returns: Succeeded for integral zero, InvalidInput for -106 (native array representation rejected), NativeFailure for -6017. Retains requested IDs/specification and actual code.

When to use: deliberately apply an already-created element specification. Sends an owned Int32 array even for one plate; native help requires the array route. There is no per-plate success result, so partial native assignment is possible. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.

```csharp
bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
if (applyChanges)
{
    var result = property.AssignElementSpecToPlate(new[] { 31, 47 }, 9);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### UpdatePropertiesToDesignSection

```csharp
PropertyMutationResult UpdatePropertiesToDesignSection()
```

Native route: `UpdatePropertiesToDesignSection` (P5). Updates model section assignments to their design sections.

Returns: Succeeded for true/integral one and NativeFailure for false/integral zero, retaining the actual representation and model-wide scope.

When to use: deliberately apply existing design section selections to the acquired model. This is a whole-model mutation; does not run design/analysis or produce a per-member change report. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.

```csharp
bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
if (applyChanges)
{
    var result = property.UpdatePropertiesToDesignSection();
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### CreateIsotropicMaterialPropertiesEx

```csharp
PropertyMutationResult CreateIsotropicMaterialPropertiesEx(IsotropicMaterialDefinition material, IsotropicMaterialStrengths strengths)
```

Native route: `CreateIsotropicMaterialPropertiesEx` (P5). Creates or updates an isotropic material with explicit extended strengths.

Parameters:

- `material`: Non-null immutable definition with explicit name, E, Poisson, G, weight density, thermal expansion and damping in the acquired native creation convention. No defaults, derived G or conversion are applied.
- `strengths`: Non-null immutable explicit Fy, Fu, Ry, Rt and Fcu packet. All strengths/ratios are finite nonnegative and sent in this native order without conversion.

Returns: Created for integral zero, Updated for one, NativeFailure for -1. Retains exact material name and actual code; no material ID is inferred.

When to use: explicitly request all five extended strength fields through the native route. Native acknowledgement does not prove strength persistence; the older obsolete compatibility creator was excluded because persistence was not verified. This method exposes only the native acknowledgement, without a read-back guarantee. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.

```csharp
bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
if (applyChanges)
{
    var material = new StaadPro.Interop.Models.Property.IsotropicMaterialDefinition("EXPLICIT_MATERIAL", 200000000.0, 0.3, 76923076.9, 77.0, 0.000012, 0.02);
    var result = property.CreateIsotropicMaterialPropertiesEx(material, new StaadPro.Interop.Models.Property.IsotropicMaterialStrengths(250000.0, 400000.0, 1.1, 1.2, 30000.0));
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### CreateIsotropicMaterialSteel

```csharp
PropertyMutationResult CreateIsotropicMaterialSteel(IsotropicMaterialDefinition material, SteelMaterialStrengths strengths, bool isPhysical)
```

Native route: `CreateIsotropicMaterialSteel` (P5). Creates or updates a Steel material with explicit strengths and physical flag.

Parameters:

- `material`: Non-null immutable definition with explicit name, E, Poisson, G, weight density, thermal expansion and damping in the acquired native creation convention. No defaults, derived G or conversion are applied.
- `strengths`: Non-null immutable explicit Fu, Fy, Rt and Ry packet. Sent in this native order, which differs from CreateIsotropicMaterialPropertiesEx.
- `isPhysical`: Explicit native physical-material flag; forwarded unchanged. Does not create physical members or assign entities.

Returns: Created for integral zero, Updated for one, NativeFailure for -1, with exact name and actual code; statuses are not material IDs.

When to use: explicitly request Steel creation/classification and strength fields. Does not derive strengths, convert units or certify that all design fields persisted. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.

```csharp
bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
if (applyChanges)
{
    var material = new StaadPro.Interop.Models.Property.IsotropicMaterialDefinition("EXPLICIT_MATERIAL", 200000000.0, 0.3, 76923076.9, 77.0, 0.000012, 0.02);
    var result = property.CreateIsotropicMaterialSteel(material, new StaadPro.Interop.Models.Property.SteelMaterialStrengths(400000.0, 250000.0, 1.2, 1.1), false);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### CreateIsotropicMaterialConcrete

```csharp
PropertyMutationResult CreateIsotropicMaterialConcrete(IsotropicMaterialDefinition material, double compressiveStrength, bool isPhysical)
```

Native route: `CreateIsotropicMaterialConcrete` (P5). Creates or updates a Concrete material with explicit Fcu and physical flag.

Parameters:

- `material`: Non-null immutable definition with explicit name, E, Poisson, G, weight density, thermal expansion and damping in the acquired native creation convention. No defaults, derived G or conversion are applied.
- `compressiveStrength`: Finite nonnegative Fcu in the acquired native stress convention. Sent unchanged; no cylinder/cube conversion is inferred.
- `isPhysical`: Explicit native physical-material flag; forwarded unchanged. Does not create physical members or assign entities.

Returns: Created for integral zero, Updated for one, NativeFailure for -1; exact name and native code retained without a fabricated material ID.

When to use: explicitly request Concrete creation and its compressive strength field. No default strength, conversion or strength-persistence guarantee is supplied. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.

```csharp
bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
if (applyChanges)
{
    var material = new StaadPro.Interop.Models.Property.IsotropicMaterialDefinition("EXPLICIT_MATERIAL", 200000000.0, 0.3, 76923076.9, 77.0, 0.000012, 0.02);
    var result = property.CreateIsotropicMaterialConcrete(material, 30000.0, false);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### CreateIsotropicMaterialAluminum

```csharp
PropertyMutationResult CreateIsotropicMaterialAluminum(IsotropicMaterialDefinition material, bool isPhysical)
```

Native route: `CreateIsotropicMaterialAluminum` (P5). Creates or updates a Aluminum material from explicit common constants.

Parameters:

- `material`: Non-null immutable definition with explicit name, E, Poisson, G, weight density, thermal expansion and damping in the acquired native creation convention. No defaults, derived G or conversion are applied.
- `isPhysical`: Explicit native physical-material flag; forwarded unchanged. Does not create physical members or assign entities.

Returns: Created for integral zero, Updated for one, NativeFailure for -1. Retains exact material name/native code; no material ID is fabricated.

When to use: explicitly request the native Aluminum creator without assuming built-in material defaults or deriving shear modulus. No design strength packet is accepted by this native function. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.

```csharp
bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
if (applyChanges)
{
    var material = new StaadPro.Interop.Models.Property.IsotropicMaterialDefinition("EXPLICIT_MATERIAL", 200000000.0, 0.3, 76923076.9, 77.0, 0.000012, 0.02);
    var result = property.CreateIsotropicMaterialAluminum(material, false);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### CreateIsotropicMaterialTimber

```csharp
PropertyMutationResult CreateIsotropicMaterialTimber(IsotropicMaterialDefinition material, bool isPhysical)
```

Native route: `CreateIsotropicMaterialTimber` (P5). Creates or updates a Timber material from explicit common constants.

Parameters:

- `material`: Non-null immutable definition with explicit name, E, Poisson, G, weight density, thermal expansion and damping in the acquired native creation convention. No defaults, derived G or conversion are applied.
- `isPhysical`: Explicit native physical-material flag; forwarded unchanged. Does not create physical members or assign entities.

Returns: Created for integral zero, Updated for one, NativeFailure for -1. Retains exact material name/native code; no material ID is fabricated.

When to use: explicitly request the native Timber creator without assuming built-in material defaults or deriving shear modulus. No design strength packet is accepted by this native function. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.

```csharp
bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
if (applyChanges)
{
    var material = new StaadPro.Interop.Models.Property.IsotropicMaterialDefinition("EXPLICIT_MATERIAL", 200000000.0, 0.3, 76923076.9, 77.0, 0.000012, 0.02);
    var result = property.CreateIsotropicMaterialTimber(material, false);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### AssignMaterialToSolid

```csharp
PropertyMutationResult AssignMaterialToSolid(string materialName, IEnumerable<int> solidIds)
```

Native route: `AssignMaterialToSolid` (P5). Assigns an existing named material to explicit analytical solids.

Parameters:

- `materialName`: Exact nonblank native material name without NUL characters. Case and whitespace are preserved; no material is created by this input.
- `solidIds`: Finite sequence of 1 through 100000 positive analytical solid IDs. Enumerated once with a hard bound and copied; empty/null/invalid input is rejected. Order and duplicates are retained.

Returns: Succeeded for true/integral one, NativeFailure for false/integral zero. Retains exact name, owned read-only requested solid IDs and actual return evidence.

When to use: assign solid material directly without entity hydration. Uses the documented native Int32-array route even for a single solid. Does not create the material, change current material context or report per-solid completion; partial assignment can remain. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.

```csharp
bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
if (applyChanges)
{
    var result = property.AssignMaterialToSolid("STEEL", new[] { 71, 89 });
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

## Qualification and next work

All 25 additions have offline call, binding-lifetime, transport and matching compiled-XML coverage. Focused cases cover native numeric/Boolean distinctions, malformed/unwritten arrays, three native layout profiles, material strength argument order, immutable ownership, bounded single-pass assignment inputs, status-only acknowledgements and shared serialization. Package verification uses a fresh local feed, project and cache, compares the restored archive hash, and compiles against the installed DLL/XML without a project reference.

Live COM mutation, units, physical-material behavior, strength persistence, design-section updates and engineering results still require qualification on disposable models for the acquired STAAD build. Failed native writes may be partial; there is no transaction, hidden save or automatic retry. Guarded examples do not execute writes unless deliberately enabled.

At this historical checkpoint, the next unimplemented allocation started in P6: eight section creators remained, followed by 43 P7, 30 P8, eight P9 and 24 P10 routes. Existing P6/P7/P9 compatibility dispatches count as implementation coverage only. Custom IOSProperty implementations must implement the 25 added members.
