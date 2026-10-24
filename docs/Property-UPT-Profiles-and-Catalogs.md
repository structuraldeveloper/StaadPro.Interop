# Property UPT profiles, observations and catalogs

Incorporated **25 Property functions**: 22 reviewed P8 user-provided-table operations and the next three P9 catalog/database-folder operations. Property now exposes **225 overloads**, covering **166 of 204 active native routes**, with **38 remaining**. Five P8 transport contracts and four earlier P7 contracts remain pending. P8 has 25 of 30 routes; P9 has four of nine. See [all 25 UPT/catalog contracts](Property-UPT-Profiles-and-Catalogs.md).

Passed a Release build with zero warnings/errors and **4,959 offline tests**, including **213 new cases**. The isolated PackageReference-only consumer verifies **892 selected XML examples**, including all **450 Property interface/adapter examples**. All sixteen new mutation examples are guarded; separate managed checks execute all 25 routes through both public surfaces. Live native mutation and engineering qualification remain pending.

Added immutable explicit UPT profile inputs, owned table inventories and named section observations. Kept operation-specific creation, insertion and removal outcomes separate. Qualified ten UPT read-back layouts and the database-folder setter against exact acquired native profiles 24.00.02.354, 25.00.01.424 and 26.00.00.340, retaining their different folder return conventions. Unreviewed builds cannot select those policies. Table numbers, native indices and property references retain separate identities.

## Deferred contracts

The roadmap permits reviewed work to continue while unqualified transport remains pending. No placeholder method or guessed native representation is counted as implemented.

| Pending P8 function | Missing qualification |
| --- | --- |
| `GetUptGeneralProfilePointsCount` | Reviewed CLR metadata passes output count values by value, unlike the help declaration; compatible native transport is not qualified. |
| `GetUptGeneralProfileBoundaryPoints` | Newer optional coordinate API requires a compatible acquired capability; reviewed by-value output metadata does not establish safe returned buffers. |
| `GetUptGeneralStressLocationPoints` | Newer optional coordinate output metadata is not qualified for safe paired buffer retrieval. |
| `FindUPTTable` | Documented member is absent from inspected 2025/2026 exported IOSPropertyUI metadata; no qualified dispatch receiver is established. |
| `GetUserProvidedTableSectionList` | Help specifies indexed CSStringList output without a verified CLR representation; no guessed String array or index mapping is exposed. |

The earlier P7 inactive-list, alpha-angle, centroid and indexed-attribute gates remain unchanged. The next pending roadmap entry is `GetDefaultStandardProfileDBFolder` in P9. Newer optional catalog getters require separate capability qualification.

## Native inputs, units and identities

All models are immutable. Explicit geometric dimensions/areas and constructor-specific ranges are validated before dispatch; inertia and shear values retain the documented nonnegative conventions. Native IZ/IY/IX names follow this UPT route, rather than borrowing another section layout. No derived dimensions, automatic unit conversion, private catalog lookup or member assignment occurs. Zero AY/AZ preserves the native ignore-shear meaning. Caller establishes the intended model, table type and native input units.

Channel CZ and general HSS retain their documented native labels where the help does not establish an unambiguous dimension. Tee CY follows its native back-web-to-CG description. Composite bottom-plate BPR/BPL retain their literal labels because the BPL prose duplicates a side description.

Composite packets contain exactly 12 steel slots, 16 slots with slab, or 19 slots with slab and bottom plate. A plate without a slab is rejected. Requested table IDs, returned table IDs, created section property IDs and native table indices are separate values. No index-base conversion is inferred for `GetUserProvidedTableNo`.

## Return policies and read-back qualification

Scalar UPT insertions use integral 0 success, -6032 failure and -6045 duplicate; composite insertion uses Boolean/integral success flags. Removal uses its own policies: table removal is a success flag, while named-section removal is integral 1 success, 0 failure and -100 invalid input. Acknowledgements retain actual native evidence and do not establish persistence or engineering adequacy.

Table lists use bounded count/list/count observations and owned read-only IDs. Section type is the documented scalar output checked against the returned and separately observed section counts. The ambiguously described named-section property count is retained as an observation and never used for allocation. Section-value capacity comes from `GetCountofSectionPropertyValuesEx`, capped at 100000. Applicable slots must be explicitly written and finite; unused padding is excluded. Counts cannot detect same-count external edits or establish model-wide atomicity.

UPT value layouts and database-folder mutation require exact acquired native profiles **24.00.02.354**, **25.00.01.424** or **26.00.00.340**. The host ApplicationVersion is not used. UPT layouts 412, 422, 432, 442, 452, 462, 472, 482, 492 and 502 retain native slot labels in owned packets. The folder setter uses integral 1/0 success/failure for the reviewed 2024 profile, versus 0/-1 for the reviewed 2025/2026 profiles. Other servicing builds remain unqualified for those policies. Folder text is preserved without creating, probing or normalizing paths.

Every operation uses the captured receiver and shared serialization gate on the caller thread. Disposal, receiver replacement and transport failures abort without retry. Native effects may remain after an error; no hidden save, analysis, rollback or transaction is supplied. Query failures expose no data.

## Evidence and verification

The [exact managed contract index](contracts/PropertyUptContractIndex.json) records signatures, parameter guidance, return policies and deferred reasons. The [three-version comparison](reports/data/OpenSTAAD-Property-2024-2026-Comparison.json) retains native declarations, parameter descriptions, value tables, exported vendor metadata and evidence fingerprints separately. The installed 2025 vendor help corroborates the reviewed routes. Managed fixtures verify dispatch order, return-shape boundaries, all ten layouts across three profiles, ownership, stale binding and installed XML compilation. These checks do not establish live native compatibility.

## The 25 contracts

Examples assume `IOSProperty property = wrapper.Property;` for a caller-owned wrapper. Concrete adapter documentation contains the same complete contract. Mutation examples default to disabled.

### 1. AddUptPropertyChannel

Adds explicit channel data to an existing native UPT table.

```csharp
PropertyMutationResult AddUptPropertyChannel(int tableId, string sectionName, UptChannelDefinition definition);
```

| Parameter | Contract |
| --- | --- |
| `tableId` | Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices. |
| `sectionName` | Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename. |
| `definition` | Non-null immutable UptChannelDefinition with every applicable native input supplied. Native order after table/name is AX,D,TW,WF,TF,IZ,IY,IX,CZ,AY,AZ. Values are validated finite and against each constructor's documented range; no defaults or dimensional conversion. |

**Returns:** Succeeded for integral zero, NativeFailure for -6032 and AlreadyExists for -6045, with exact requested TableId/SectionName and actual code retained. No section property ID or new table ID is fabricated.

When to use: insert the channel using its specific scalar native route. Channel CZ is retained in the native convention without guessing a centroid axis/dimension. Native IZ/IY/IX labels retain this UPT route's documented meanings instead of borrowing another section layout. Native AY/AZ zero retains ignore-shear semantics. Caller establishes table-type compatibility; no table is created, duplicate replaced or member assigned. Uses the captured receiver and shared serialization gate on the caller's thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var primary = new StaadPro.Interop.Models.Property.UptWideFlangeDefinition(0.01, 0.3, 0.006, 0.15, 0.01, 0.000001, 0.00001, 0.0001, 0.002, 0.003);
    var definition = new StaadPro.Interop.Models.Property.UptChannelDefinition(primary, 0.04);
    var result = property.AddUptPropertyChannel(7, "EXPLICIT_CHANNEL", definition);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### 2. AddUptPropertyAngle

Adds explicit angle data to an existing native UPT table.

```csharp
PropertyMutationResult AddUptPropertyAngle(int tableId, string sectionName, UptAngleDefinition definition);
```

| Parameter | Contract |
| --- | --- |
| `tableId` | Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices. |
| `sectionName` | Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename. |
| `definition` | Non-null immutable UptAngleDefinition with every applicable native input supplied. Native order after table/name is D,WF,TF,R,AY,AZ. Values are validated finite and against each constructor's documented range; no defaults or dimensional conversion. |

**Returns:** Succeeded for integral zero, NativeFailure for -6032 and AlreadyExists for -6045, with exact requested TableId/SectionName and actual code retained. No section property ID or new table ID is fabricated.

When to use: insert the angle using its specific scalar native route. Native IZ/IY/IX labels retain this UPT route's documented meanings instead of borrowing another section layout. Native AY/AZ zero retains ignore-shear semantics. Caller establishes table-type compatibility; no table is created, duplicate replaced or member assigned. Uses the captured receiver and shared serialization gate on the caller's thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var definition = new StaadPro.Interop.Models.Property.UptAngleDefinition(0.2, 0.15, 0.01, 0.03, 0.002, 0.003);
    var result = property.AddUptPropertyAngle(7, "EXPLICIT_ANGLE", definition);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### 3. AddUptPropertyDoubleAngle

Adds explicit double angle data to an existing native UPT table.

```csharp
PropertyMutationResult AddUptPropertyDoubleAngle(int tableId, string sectionName, UptDoubleAngleDefinition definition);
```

| Parameter | Contract |
| --- | --- |
| `tableId` | Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices. |
| `sectionName` | Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename. |
| `definition` | Non-null immutable UptDoubleAngleDefinition with every applicable native input supplied. Native order after table/name is D,WF,TF,SP,IZ,IY,IX,CY,AY,AZ. Values are validated finite and against each constructor's documented range; no defaults or dimensional conversion. |

**Returns:** Succeeded for integral zero, NativeFailure for -6032 and AlreadyExists for -6045, with exact requested TableId/SectionName and actual code retained. No section property ID or new table ID is fabricated.

When to use: insert the double angle using its specific scalar native route. Native IZ/IY/IX labels retain this UPT route's documented meanings instead of borrowing another section layout. Native AY/AZ zero retains ignore-shear semantics. Caller establishes table-type compatibility; no table is created, duplicate replaced or member assigned. Uses the captured receiver and shared serialization gate on the caller's thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var definition = new StaadPro.Interop.Models.Property.UptDoubleAngleDefinition(0.2, 0.15, 0.01, 0.02, 0.000001, 0.00001, 0.0001, 0.04, 0.002, 0.003);
    var result = property.AddUptPropertyDoubleAngle(7, "EXPLICIT_DOUBLEANGLE", definition);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### 4. AddUptPropertyTee

Adds explicit tee data to an existing native UPT table.

```csharp
PropertyMutationResult AddUptPropertyTee(int tableId, string sectionName, UptTeeDefinition definition);
```

| Parameter | Contract |
| --- | --- |
| `tableId` | Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices. |
| `sectionName` | Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename. |
| `definition` | Non-null immutable UptTeeDefinition with every applicable native input supplied. Native order after table/name is AX,D,WF,TF,TW,IZ,IY,IX,CY,AY,AZ. Values are validated finite and against each constructor's documented range; no defaults or dimensional conversion. |

**Returns:** Succeeded for integral zero, NativeFailure for -6032 and AlreadyExists for -6045, with exact requested TableId/SectionName and actual code retained. No section property ID or new table ID is fabricated.

When to use: insert the tee using its specific scalar native route. The tee native order sends WF/TF before TW; it does not reuse the common wide-flange slot order blindly. Native IZ/IY/IX labels retain this UPT route's documented meanings instead of borrowing another section layout. Native AY/AZ zero retains ignore-shear semantics. Caller establishes table-type compatibility; no table is created, duplicate replaced or member assigned. Uses the captured receiver and shared serialization gate on the caller's thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var primary = new StaadPro.Interop.Models.Property.UptWideFlangeDefinition(0.01, 0.3, 0.006, 0.15, 0.01, 0.000001, 0.00001, 0.0001, 0.002, 0.003);
    var definition = new StaadPro.Interop.Models.Property.UptTeeDefinition(primary, 0.04);
    var result = property.AddUptPropertyTee(7, "EXPLICIT_TEE", definition);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### 5. AddUptPropertyPipe

Adds explicit pipe data to an existing native UPT table.

```csharp
PropertyMutationResult AddUptPropertyPipe(int tableId, string sectionName, UptPipeDefinition definition);
```

| Parameter | Contract |
| --- | --- |
| `tableId` | Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices. |
| `sectionName` | Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename. |
| `definition` | Non-null immutable UptPipeDefinition with every applicable native input supplied. Native order after table/name is OD,ID,AY,AZ. Values are validated finite and against each constructor's documented range; no defaults or dimensional conversion. |

**Returns:** Succeeded for integral zero, NativeFailure for -6032 and AlreadyExists for -6045, with exact requested TableId/SectionName and actual code retained. No section property ID or new table ID is fabricated.

When to use: insert the pipe using its specific scalar native route. Native IZ/IY/IX labels retain this UPT route's documented meanings instead of borrowing another section layout. Native AY/AZ zero retains ignore-shear semantics. Caller establishes table-type compatibility; no table is created, duplicate replaced or member assigned. Uses the captured receiver and shared serialization gate on the caller's thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var definition = new StaadPro.Interop.Models.Property.UptPipeDefinition(0.2, 0.18, 0.002, 0.003);
    var result = property.AddUptPropertyPipe(7, "EXPLICIT_PIPE", definition);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### 6. AddUptPropertyTube

Adds explicit tube data to an existing native UPT table.

```csharp
PropertyMutationResult AddUptPropertyTube(int tableId, string sectionName, UptTubeDefinition definition);
```

| Parameter | Contract |
| --- | --- |
| `tableId` | Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices. |
| `sectionName` | Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename. |
| `definition` | Non-null immutable UptTubeDefinition with every applicable native input supplied. Native order after table/name is AX,D,WF,TF,IZ,IY,IX,AY,AZ. Values are validated finite and against each constructor's documented range; no defaults or dimensional conversion. |

**Returns:** Succeeded for integral zero, NativeFailure for -6032 and AlreadyExists for -6045, with exact requested TableId/SectionName and actual code retained. No section property ID or new table ID is fabricated.

When to use: insert the tube using its specific scalar native route. Native IZ/IY/IX labels retain this UPT route's documented meanings instead of borrowing another section layout. Native AY/AZ zero retains ignore-shear semantics. Caller establishes table-type compatibility; no table is created, duplicate replaced or member assigned. Uses the captured receiver and shared serialization gate on the caller's thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var definition = new StaadPro.Interop.Models.Property.UptTubeDefinition(0.01, 0.3, 0.15, 0.01, 0.000001, 0.00001, 0.0001, 0.002, 0.003);
    var result = property.AddUptPropertyTube(7, "EXPLICIT_TUBE", definition);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### 7. AddUptPropertyGeneral

Adds explicit general profile data to an existing native UPT table.

```csharp
PropertyMutationResult AddUptPropertyGeneral(int tableId, string sectionName, UptGeneralDefinition definition);
```

| Parameter | Contract |
| --- | --- |
| `tableId` | Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices. |
| `sectionName` | Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename. |
| `definition` | Non-null immutable UptGeneralDefinition with every applicable native input supplied. Native order after table/name is AX,D,TD,B,TB,IZ,IY,IX,SZ,SY,AY,AZ,PZ,PY,HSS,DEE. Values are validated finite and against each constructor's documented range; no defaults or dimensional conversion. |

**Returns:** Succeeded for integral zero, NativeFailure for -6032 and AlreadyExists for -6045, with exact requested TableId/SectionName and actual code retained. No section property ID or new table ID is fabricated.

When to use: insert the general profile using its specific scalar native route. Native IZ/IY/IX labels retain this UPT route's documented meanings instead of borrowing another section layout. Native AY/AZ zero retains ignore-shear semantics. Caller establishes table-type compatibility; no table is created, duplicate replaced or member assigned. Uses the captured receiver and shared serialization gate on the caller's thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var definition = new StaadPro.Interop.Models.Property.UptGeneralDefinition(0.01, 0.3, 0.006, 0.15, 0.01, 0.000001, 0.00001, 0.0001, 0.0002, 0.0003, 0.002, 0.003, 0.0004, 0.0005, 0.00000001, 0.28);
    var result = property.AddUptPropertyGeneral(7, "EXPLICIT_GENERAL", definition);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### 8. AddUptPropertyISection

Adds explicit I-section data to an existing native UPT table.

```csharp
PropertyMutationResult AddUptPropertyISection(int tableId, string sectionName, UptISectionDefinition definition);
```

| Parameter | Contract |
| --- | --- |
| `tableId` | Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices. |
| `sectionName` | Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename. |
| `definition` | Non-null immutable UptISectionDefinition with every applicable native input supplied. Native order after table/name is DWW,TWW,DWW1,BFF,TFF,BFF1,TFF1,AYF,AZF,XIF. Values are validated finite and against each constructor's documented range; no defaults or dimensional conversion. |

**Returns:** Succeeded for integral zero, NativeFailure for -6032 and AlreadyExists for -6045, with exact requested TableId/SectionName and actual code retained. No section property ID or new table ID is fabricated.

When to use: insert the I-section using its specific scalar native route. Native IZ/IY/IX labels retain this UPT route's documented meanings instead of borrowing another section layout. Native AY/AZ zero retains ignore-shear semantics. Caller establishes table-type compatibility; no table is created, duplicate replaced or member assigned. Uses the captured receiver and shared serialization gate on the caller's thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var definition = new StaadPro.Interop.Models.Property.UptISectionDefinition(0.4, 0.006, 0.3, 0.2, 0.01, 0.15, 0.01, 0.002, 0.003, 0.000001);
    var result = property.AddUptPropertyISection(7, "EXPLICIT_ISECTION", definition);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### 9. AddUptPropertyPrismatic

Adds explicit prismatic profile data to an existing native UPT table.

```csharp
PropertyMutationResult AddUptPropertyPrismatic(int tableId, string sectionName, UptPrismaticDefinition definition);
```

| Parameter | Contract |
| --- | --- |
| `tableId` | Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices. |
| `sectionName` | Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename. |
| `definition` | Non-null immutable UptPrismaticDefinition with every applicable native input supplied. Native order after table/name is AX,IZ,IY,IX,AY,AZ,YD,ZD. Values are validated finite and against each constructor's documented range; no defaults or dimensional conversion. |

**Returns:** Succeeded for integral zero, NativeFailure for -6032 and AlreadyExists for -6045, with exact requested TableId/SectionName and actual code retained. No section property ID or new table ID is fabricated.

When to use: insert the prismatic profile using its specific scalar native route. Native IZ/IY/IX labels retain this UPT route's documented meanings instead of borrowing another section layout. Native AY/AZ zero retains ignore-shear semantics. Caller establishes table-type compatibility; no table is created, duplicate replaced or member assigned. Uses the captured receiver and shared serialization gate on the caller's thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var definition = new StaadPro.Interop.Models.Property.UptPrismaticDefinition(0.01, 0.000001, 0.00001, 0.0001, 0.002, 0.003, 0.3, 0.15);
    var result = property.AddUptPropertyPrismatic(7, "EXPLICIT_PRISMATIC", definition);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### 10. AddUptPropertyWideFlangeComposite

Adds an explicit 12-, 16- or 19-slot composite wide-flange UPT section.

```csharp
PropertyMutationResult AddUptPropertyWideFlangeComposite(int tableId, string sectionName, UptCompositeWideFlangeDefinition definition);
```

| Parameter | Contract |
| --- | --- |
| `tableId` | Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices. |
| `sectionName` | Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename. |
| `definition` | Non-null immutable twelve-slot steel definition, optionally followed by slab CFR/CFL/CFT/MR and then plate BPR/BPL/BPT. Valid lengths are exactly 12,16,19. A plate without slab is rejected; no engineering zeros are substituted for omitted packets. |

**Returns:** Succeeded for true/integral one and NativeFailure for false/integral zero. Actual Boolean/integral evidence and requested table/name are retained; no insertion ID or duplicate-specific status is inferred.

When to use: insert a supported composite layout without guessing optional-buffer length. Sends a fresh owned double array. CFR/CFL retain documented right/left meanings; BPR/BPL labels retain the help's duplicated side-description ambiguity. No automatic table creation, material conversion or staged cleanup occurs. Uses the captured receiver and shared serialization gate on the caller's thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var primary = new StaadPro.Interop.Models.Property.UptWideFlangeDefinition(0.01, 0.3, 0.006, 0.15, 0.01, 0.000001, 0.00001, 0.0001, 0.002, 0.003);
    var steel = new StaadPro.Interop.Models.Property.UptUnequalWideFlangeDefinition(primary, 0.2, 0.015);
    var slab = new StaadPro.Interop.Models.Property.UptCompositeSlab(0.5, 0.6, 0.12, 8.0);
    var plate = new StaadPro.Interop.Models.Property.UptCompositeBottomPlate(0.1, 0.11, 0.01);
    var definition = new StaadPro.Interop.Models.Property.UptCompositeWideFlangeDefinition(steel, slab, plate);
    var result = property.AddUptPropertyWideFlangeComposite(7, "EXPLICIT_COMPOSITE", definition);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### 11. CreatePropertyFromUptTable

Creates an assigned section-property reference from an existing named UPT entry.

```csharp
PropertyMutationResult CreatePropertyFromUptTable(int tableId, string sectionName);
```

| Parameter | Contract |
| --- | --- |
| `tableId` | Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices. |
| `sectionName` | Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename. |

**Returns:** Created with a positive CreatedPropertyId; native -1 gives NativeFailure. TableId/SectionName and actual code are retained separately. Zero and other undocumented nonpositive results throw.

When to use: obtain a property reference after the table and named entry already exist. A positive return does not certify a new globally unique allocation. This native route sends tableId before sectionName and does not insert a section, assign members or synthesize a table lookup. Uses the captured receiver and shared serialization gate on the caller's thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var result = property.CreatePropertyFromUptTable(7, "EXPLICIT_WF");
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### 12. CreatePropertyFromUserTable

Obtains a positive section-property reference through the native section-name-first user-table route.

```csharp
PropertyMutationResult CreatePropertyFromUserTable(string sectionName, int tableId);
```

| Parameter | Contract |
| --- | --- |
| `sectionName` | Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename. |
| `tableId` | Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices. |

**Returns:** Created with separate positive CreatedPropertyId and requested table/name identity. No failure return code is documented: nonpositive/malformed responses throw without an inferred failure mapping.

When to use: use the distinct CreatePropertyFromUserTable route and its native sectionName,tableId order. It does not fall back to CreatePropertyFromUptTable, create the table/entry, assign geometry or establish a new-unique allocation. Uses the captured receiver and shared serialization gate on the caller's thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var result = property.CreatePropertyFromUserTable("EXPLICIT_WF", 7);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### 13. CreateUptTableEx

Creates a UPT table using an explicit requested table number and type.

```csharp
PropertyMutationResult CreateUptTableEx(int requestedTableId, UserProvidedTableType tableType);
```

| Parameter | Contract |
| --- | --- |
| `requestedTableId` | Positive requested new native table number. It is retained in TableId separately from the actual returned CreatedTableId. |
| `tableType` | Defined reviewed UPT type 1..10; not a country, property reference or table inventory index. |

**Returns:** Created for a positive integral native table ID, NativeFailure for zero; undocumented negatives throw. Requested and actual returned table IDs remain separate even if they differ. No section property ID is populated.

When to use: request a specific table number without hiding native renumbering, overwrite behavior or collisions. No table/section deletion, insertion or rollback is performed automatically. Uses the captured receiver and shared serialization gate on the caller's thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var result = property.CreateUptTableEx(7, StaadPro.Interop.Enums.Property.UserProvidedTableType.WideFlange);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### 14. RemoveUptTable

Requests native removal of an explicitly identified UPT table.

```csharp
PropertyMutationResult RemoveUptTable(int tableId);
```

| Parameter | Contract |
| --- | --- |
| `tableId` | Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices. |

**Returns:** Succeeded for true/integral one and NativeFailure for false/integral zero. Actual return representation and requested TableId are retained; no completion report for dependent sections/properties is fabricated.

When to use: deliberately remove the native table only after reviewing its current contents and references. Effects on dependent native data are not inferred or rolled back; no automatic replacement or force-removal fallback is attempted. Uses the captured receiver and shared serialization gate on the caller's thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var result = property.RemoveUptTable(7);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### 15. RemovePropertyFromUptTable

Requests deletion of an exact named section from a UPT table.

```csharp
PropertyMutationResult RemovePropertyFromUptTable(int tableId, string sectionName);
```

| Parameter | Contract |
| --- | --- |
| `tableId` | Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices. |
| `sectionName` | Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename. |

**Returns:** Succeeded for integral one, NativeFailure for zero and InvalidInput for -100 (invalid table or name). Actual integral code and requested identity are retained; Boolean and undocumented responses throw.

When to use: deliberately remove one native UPT entry, retaining the table/name identity. Zero is failure here; the scalar insertion zero-success convention is not reused. No dependent-property cleanup, cascade guarantee or force-removal fallback is supplied. Uses the captured receiver and shared serialization gate on the caller's thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var result = property.RemovePropertyFromUptTable(7, "EXPLICIT_WF");
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### 16. GetUserProvidedTableCount

Reads the native user-provided table count.

```csharp
PropertyQueryResult<int> GetUserProvidedTableCount();
```

| Parameter | Contract |
| --- | --- |
| None | Uses the captured native Property receiver. |

**Returns:** Success with the actual nonnegative integral count, including zero; Boolean/fractional/text/null and undocumented negative returns throw.

When to use: size or inspect UPT inventory without confusing a count with a maximum table ID or an index base. Uses the shared captured-binding gate; dependent dispatches check lifetime individually. Inspect IsSuccessful/Status before Data. No model mutation, geometry hydration, private catalog lookup, unit conversion or engineering certification occurs.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
var result = property.GetUserProvidedTableCount();
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
```

### 17. GetUserProvidedTableList

Reads a bounded owned snapshot of native UPT table IDs.

```csharp
PropertyQueryResult<IReadOnlyList<int>> GetUserProvidedTableList(int maximumResults = 100000);
```

| Parameter | Contract |
| --- | --- |
| `maximumResults` | Allocation limit 1..100000, default 100000. An oversize native count gives LimitExceeded before list allocation/retrieval. |

**Returns:** Success with read-only positive IDs in native order, retaining duplicates. Zero inventory skips list retrieval and returns empty data after count recheck. False/zero gives NativeFailure; count changes give StateChanged.

When to use: enumerate table numbers without assuming contiguous or zero/one-based IDs. Uses count/list/count within the shared gate, validates exact rank-one Int32 shape/length and every written positive ID. Equal counts cannot exclude same-count external edits. Uses the shared captured-binding gate; dependent dispatches check lifetime individually. Inspect IsSuccessful/Status before Data. No model mutation, geometry hydration, private catalog lookup, unit conversion or engineering certification occurs.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
var result = property.GetUserProvidedTableList();
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
else System.Console.WriteLine(result.Status);
```

### 18. GetUserProvidedTableSectionCount

Reads a native table's section count with explicit table identity.

```csharp
PropertyQueryResult<UptTableSectionCount> GetUserProvidedTableSectionCount(int tableId);
```

| Parameter | Contract |
| --- | --- |
| `tableId` | Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices. |

**Returns:** Success with an immutable requested TableId and nonnegative Count, including zero. Zero does not prove the table exists; undocumented negatives or malformed counts throw.

When to use: observe table population independently from section-name transport. It does not expose the unverified indexed CSStringList representation or infer a section list from count. Uses the shared captured-binding gate; dependent dispatches check lifetime individually. Inspect IsSuccessful/Status before Data. No model mutation, geometry hydration, private catalog lookup, unit conversion or engineering certification occurs.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
var result = property.GetUserProvidedTableSectionCount(7);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
else System.Console.WriteLine(result.Status);
```

### 19. GetUserProvidedTableSectionType

Reads the written scalar native UPT section type with count consistency checks.

```csharp
PropertyQueryResult<UptTableSectionType> GetUserProvidedTableSectionType(int tableId);
```

| Parameter | Contract |
| --- | --- |
| `tableId` | Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices. |

**Returns:** Success with requested TableId, a positive raw NativeTypeCode and the checked SectionCount. Count changes yield StateChanged. An empty-count observation without a written positive type yields EmptyOrUnavailable; no default type is exposed.

When to use: observe the singular type value described by native help without treating it as an array. The function's integral return is a section count, not a success flag; it must match separate before/after counts. Unknown positive codes are retained but do not imply a supported value layout. Uses the shared captured-binding gate; dependent dispatches check lifetime individually. Inspect IsSuccessful/Status before Data. No model mutation, geometry hydration, private catalog lookup, unit conversion or engineering certification occurs.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
var result = property.GetUserProvidedTableSectionType(7);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.NativeTypeCode);
else System.Console.WriteLine(result.Status);
```

### 20. GetUserProvidedTableSectionPropertyCount

Retains the native named-section count with exact requested UPT identity.

```csharp
PropertyQueryResult<UptNamedSectionCount> GetUserProvidedTableSectionPropertyCount(int tableId, string sectionName);
```

| Parameter | Contract |
| --- | --- |
| `tableId` | Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices. |
| `sectionName` | Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename. |

**Returns:** Success with requested table/name and the actual nonnegative NativeCount, including zero. The copied return wording is ambiguous; no engineering value count, buffer size or missing-section status is inferred.

When to use: inspect this native scalar without using it as a memory allocation contract. The help calls the return a number of sections despite the method name. GetUserProvidedTableSectionProperties instead sizes its buffer using GetCountofSectionPropertyValuesEx. Uses the shared captured-binding gate; dependent dispatches check lifetime individually. Inspect IsSuccessful/Status before Data. No model mutation, geometry hydration, private catalog lookup, unit conversion or engineering certification occurs.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
var result = property.GetUserProvidedTableSectionPropertyCount(7, "EXPLICIT_WF");
if (result.IsSuccessful) System.Console.WriteLine(result.Data.NativeCount);
else System.Console.WriteLine(result.Status);
```

### 21. GetUserProvidedTableSectionProperties

Reads an owned version-qualified native UPT section-value layout.

```csharp
PropertyQueryResult<UptSectionProperties> GetUserProvidedTableSectionProperties(int tableId, string sectionName);
```

| Parameter | Contract |
| --- | --- |
| `tableId` | Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices. |
| `sectionName` | Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename. |

**Returns:** Success for true/integral one with exact requested identity, native type/version/capacity and read-only applicable field names/values. False/zero gives NativeFailure; capacity/version changes give StateChanged. Unreviewed profiles/types fail explicitly.

When to use: decode one of ten reviewed UPT type codes 412..502 without confusing them with common section-property type codes. Reviewed acquired native profiles are exactly 24.00.02.354, 25.00.01.424 and 26.00.00.340. Unreviewed/unknown versions fail before unsafe access. The profile uses wrapper.StaadVersion, never host ApplicationVersion. Native capacity from GetCountofSectionPropertyValuesEx must be 1..100000 and cover every applicable field. All required slots must be explicitly written finite floating-point values; unused padding is excluded, not turned into engineering zeros. Capacity is rechecked and native version frozen before returning. Native labels, inertia identities and units are retained without geometry/area substitution. Uses the shared captured-binding gate; dependent dispatches check lifetime individually. Inspect IsSuccessful/Status before Data. No model mutation, geometry hydration, private catalog lookup, unit conversion or engineering certification occurs.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
var result = property.GetUserProvidedTableSectionProperties(7, "EXPLICIT_WF");
if (result.IsSuccessful) System.Console.WriteLine(result.Data.NativeTypeCode);
else System.Console.WriteLine(result.Status);
```

### 22. GetUserProvidedTableNo

Reads a table number for an explicitly supplied native UPT inventory index.

```csharp
PropertyQueryResult<UptTableReference> GetUserProvidedTableNo(int nativeTableIndex);
```

| Parameter | Contract |
| --- | --- |
| `nativeTableIndex` | Nonnegative native table index independently established by the caller. The help example uses one but does not establish an index base; this method sends the exact supplied value without incrementing/decrementing it. It is not automatically a section property reference. |

**Returns:** Success with the exact requested NativeIndex and positive returned TableId; -1 gives NativeFailure. Other nonpositive/malformed results throw.

When to use: use the native index-to-table observation without treating the varSecRefNo declaration spelling as a verified property-ID alias. No enumeration or automatic mapping from another reference domain occurs. Uses the shared captured-binding gate; dependent dispatches check lifetime individually. Inspect IsSuccessful/Status before Data. No model mutation, geometry hydration, private catalog lookup, unit conversion or engineering certification occurs.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
var result = property.GetUserProvidedTableNo(1);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.TableId);
else System.Console.WriteLine(result.Status);
```

### 23. GetShapeCode

Reads a raw country-specific section shape code with exact catalog identity.

```csharp
PropertyQueryResult<SectionShapeCode> GetShapeCode(Country country, string sectionName);
```

| Parameter | Contract |
| --- | --- |
| `country` | Reviewed repository country selector codes 1..37. Source-only Chilean=38 and undefined selectors are rejected; catalog availability is not inferred. |
| `sectionName` | Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename. |

**Returns:** Success with exact requested country/name and nonnegative raw NativeCode, including zero and unmapped values. -1 gives NativeFailure; other negatives/malformed returns throw.

When to use: inspect the native code without coercing different countries' shape domains into a universal enum or asserting a supported engineering family. No catalog section or geometry is synthesized from the integer. Uses the shared captured-binding gate; dependent dispatches check lifetime individually. Inspect IsSuccessful/Status before Data. No model mutation, geometry hydration, private catalog lookup, unit conversion or engineering certification occurs.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
var result = property.GetShapeCode(Country.Indian, "ISMB300");
if (result.IsSuccessful) System.Console.WriteLine(result.Data.NativeCode);
else System.Console.WriteLine(result.Status);
```

### 24. SetStandardProfileDBFolder

Requests a standard-profile database folder change under an exact native return-policy profile.

```csharp
PropertyMutationResult SetStandardProfileDBFolder(string folderPath);
```

| Parameter | Contract |
| --- | --- |
| `folderPath` | Exact non-null path text without NUL. Empty/whitespace/relative text is forwarded unchanged for native validation; no directory creation, existence check or path normalization. |

**Returns:** Succeeded/NativeFailure according to the exact acquired profile: 2024 uses 1/0, 2025/2026 use 0/-1. Retains actual integral response, exact FolderPath and immutable NativeContractVersion. Boolean/undocumented responses throw.

When to use: deliberately change the acquired application's profile database context, after reviewing effects on subsequent catalog operations. Reviewed acquired native profiles are exactly 24.00.02.354, 25.00.01.424 and 26.00.00.340. Unreviewed/unknown versions fail before unsafe access. The profile uses wrapper.StaadVersion, never host ApplicationVersion. Unknown servicing profiles fail before native dispatch. No default-folder reset, backup, implicit reloading or restoration is performed; a version change during the mutation throws and effects may remain. Uses the captured receiver and shared serialization gate on the caller's thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var result = property.SetStandardProfileDBFolder(@"C:\STAAD\Profiles");
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### 25. GetStandardProfileDBFolder

Reads the exact native standard-profile database folder path.

```csharp
PropertyQueryResult<string> GetStandardProfileDBFolder();
```

| Parameter | Contract |
| --- | --- |
| None | Uses the captured native Property receiver. |

**Returns:** Success with the exact nonempty string, preserving case/whitespace. Null/DBNull/empty gives EmptyOrUnavailable; non-string/NUL text throws. No path-existence guarantee is inferred.

When to use: observe current catalog context without changing it or substituting a default folder. No filesystem access, normalization or directory creation occurs. Uses the shared captured-binding gate; dependent dispatches check lifetime individually. Inspect IsSuccessful/Status before Data. No model mutation, geometry hydration, private catalog lookup, unit conversion or engineering certification occurs.

**Exceptions:** Invalid inputs fail before dispatch. Disposed or replaced bindings, malformed/undocumented native results and transport failures abort without retry. Layout-dependent reads and the folder setter reject unreviewed native profiles before their operation; an unsupported returned layout is rejected rather than decoded.

```csharp
var result = property.GetStandardProfileDBFolder();
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
```
