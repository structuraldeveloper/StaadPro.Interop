# Property fireproofing, attributes and UPT

Incorporated **25 Property functions**: 22 reviewed P7 inactive/fireproofing/attribute operations and the first three P8 UPT operations. Property now exposes **200 overloads** and reaches **141 of 204 active native routes**, with **63 remaining**. P7 has 42 of 46 routes; four contract gaps remain explicitly pending. P8 has three of thirty routes. See [the 25 fireproofing/attribute/UPT contracts](Property-Fireproofing-Attributes-and-UPT.md).

Passed a Release build with zero warnings/errors and **4,746 offline tests**, including **195 new cases**. The fresh PackageReference-only consumer verifies **842 selected XML examples**, including all **400 Property interface/adapter examples**. All ten new mutation examples are guarded; dedicated managed checks execute every new route through both public surfaces. Verification does not change a live STAAD model.

Added immutable exact attribute pairs, pair/domain/ID snapshots, fireproofing correspondence and explicit UPT value packets. Bounded count/list/count reads validate written outputs and preserve order/duplicates. By-reference native attribute-value rewriting returns StateChanged rather than data for the wrong pair. UPT table IDs, insertion acknowledgements and duplicate outcomes remain separate; scalar and unequal wide-flange routes retain their different return conventions.

Package Authors remains exactly **Srinivasa Rao Masanam**; company remains **TensorStruct** and the current package copyright is **TensorStruct**. Existing author packing/verification safeguards remain in place.

## Scope and deferred contracts

The [roadmap](reports/OpenSTAAD-Property-Multi-Version-Architecture-and-Plan.md) permits unverified contracts to remain gated while reviewed operations progress. Four next P7 functions remain unimplemented; none is counted as complete or exposed as an always-unsupported stub:

| Pending native function | Missing qualification |
| --- | --- |
| `GetInactiveMemberList` | Void by-value Object transport is marked unusable in reference source; a live native written-buffer fixture is missing. |
| `GetAlphaAngleForSection` | Native help supplies no interpretable return-status contract; no finite default/output is promoted to certified success. |
| `GetCentroidLocationForSection` | Native return status is undocumented; qualification must establish success/absence before exposing engineering data. |
| `GetAssignedAttributeByIndex` | Index base and native observation-context relationship are not verified; no guessed mapping to a requested entity. |

The 25 implemented contracts therefore cover the other 22 pending P7 functions, then CreateUptTable, AddUptPropertyWideFlange and AddUptPropertyWideFlangeUnequal in P8 order. The [exact contract index](contracts/PropertyAttributeContractIndex.json) records every signature and deferred reason. Native declarations, parameter descriptions, tables and operation-specific outcomes come from the separate Bentley 2024/2025/2026 [comparison evidence](reports/data/OpenSTAAD-Property-2024-2026-Comparison.json), supplemented by the installed vendor 2025 help and passive CLR metadata.

## Data, units and native identities

| Type or rule | Meaning |
| --- | --- |
| PropertyAttribute | Immutable exact Name/Value strings, including empty and whitespace text. Null and embedded NUL are rejected. No normalization, GUID parsing, case folding or string-to-number conversion. |
| PropertyAttributeMatchCount | Exact requested pair, member/element domain and qualified nonnegative count. Zero does not establish definition existence. |
| PropertyAttributeEntityIds | Exact pair/domain plus owned read-only positive IDs in native order, retaining duplicates. |
| AssignedAttributeSummary | Requested positive memberOrPlateId and count. Native chooses a member when that ID exists and a plate otherwise; this wrapper does not infer a resolved kind or enable indexed enumeration. |
| FireProofingData | Requested member ID or positive nonzero-based specification getter index, verified BFP/CFP type, finite nonnegative native thickness/density and an optional scalar assignment count. Getter indices are not converted from zero-based creation indices. |
| UptWideFlangeDefinition | Explicit AX,D,TW,WF,TF,IZ,IY,IX,AY,AZ values in that order. Native inertia labels and this function's documented slot meanings remain intact. |
| UptUnequalWideFlangeDefinition | Immutable primary ten values plus explicit WF1/TF1 in slots ten/eleven. No physical orientation is guessed from those labels. |
| UserProvidedTableType | Reviewed selectors 1..10. A positive created table ID is separate from section property IDs and specification IDs. |

Geometric input area/dimensions are finite positive; inertia/shear-area inputs finite nonnegative. Read-back thickness/density may contain an explicitly returned zero. Native length, area, inertia and density conventions are retained. No private catalog lookup, implicit material selection, unit conversion, derived geometry or engineering adequacy check occurs.

The single non-partial OSPropertyAdapter uses PropertyAttributeReader with the same captured receiver and serialization gate as earlier Property services. Every dependent dispatch rechecks binding. Disposal/replacement aborts the observation; transport exceptions propagate without retry. Actual Boolean and integral return evidence remain separate.

## Buffers and operation-specific returns

Fireproofed-member lists use the documented true/1 success flag; attribute entity lists use integral zero success; the paired member-attribute inventory returns its actual count. Those contracts are not interchangeable. Rank, exact length, element type, positive IDs and written strings are checked. Null String sentinels and invalid zero Int32 sentinels detect unwritten entries; paired names/values retain correspondence.

Count/list/count observations stop before allocation when the caller's limit is exceeded. A zero inventory skips list retrieval and returns owned empty data after rechecking count. Counts cannot rule out same-count external edits or guarantee a model-wide atomic snapshot. Input ID sequences enumerate once with a hard 100000-item limit, retain order/duplicates, and reject empty/null/nonpositive inputs before native writes.

Vendor CLR metadata makes attribute query values ref String inputs, despite input-only prose in the help. Local copies protect caller state. If native changes the requested value during count or list retrieval, StateChanged has no Data, preventing attribution to the wrong pair.

Fireproofing specification details use a scalar Long assignment count, as shown by the vendor VBA example. The parameter table's array-of-Long wording is not used to guess or unwrap a buffer. A malformed or unwritten scalar fails.

CreateUptTable returns a positive table ID; -6031 is InvalidInput. Scalar wide-flange insertion returns zero success, -6032 failure and -6045 duplicate. Unequal insertion returns true/1 success and false/0 failure, without a duplicate-specific distinction. No insertion acknowledgement is treated as a created section property ID. Table creation and insertion are separate mutations, with no automatic cleanup when a later stage fails.

## Functions

### GetInactiveMemberCount

```csharp
PropertyQueryResult<int> GetInactiveMemberCount()
```

Native route: `GetInactiveMemberCount` (P7). Reads the native inactive-member count without relying on the gated list transport.

Returns: Success with the actual nonnegative integral count, including zero; Boolean/fractional/text/null and undocumented negative counts throw.

When to use: inspect this inventory size without deriving IDs or inferring model engineering behavior. A count is not a maximum entity ID or a guarantee of stable contents. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.

```csharp
var result = property.GetInactiveMemberCount();
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
```

### GetFireProofedBeamCount

```csharp
PropertyQueryResult<int> GetFireProofedBeamCount()
```

Native route: `GetFireProofedBeamCount` (P7). Reads the native count of fireproofed members.

Returns: Success with the actual nonnegative integral count, including zero; Boolean/fractional/text/null and undocumented negative counts throw.

When to use: inspect this inventory size without deriving IDs or inferring model engineering behavior. A count is not a maximum entity ID or a guarantee of stable contents. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.

```csharp
var result = property.GetFireProofedBeamCount();
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
```

### GetFireProofingSpecCount

```csharp
PropertyQueryResult<int> GetFireProofingSpecCount()
```

Native route: `GetFireProofingSpecCount` (P7). Reads the native count of fireproofing specifications.

Returns: Success with the actual nonnegative integral count, including zero; Boolean/fractional/text/null and undocumented negative counts throw.

When to use: inspect this inventory size without deriving IDs or inferring model engineering behavior. A count is not a maximum entity ID or a guarantee of stable contents. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.

```csharp
var result = property.GetFireProofingSpecCount();
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
```

### GetFireProofedBeamList

```csharp
PropertyQueryResult<IReadOnlyList<int>> GetFireProofedBeamList(int maximumResults = 100000)
```

Native route: `GetFireProofedBeamList` (P7). Reads an owned bounded snapshot of native fireproofed member IDs.

Parameters:

- `maximumResults`: Allocation limit from 1 through 100000, default 100000. Larger native counts give LimitExceeded before buffer allocation/retrieval.

Returns: Success with a read-only ID list in native order, retaining duplicates. Zero count gives an empty list without list dispatch. False/zero list flag gives NativeFailure; changed counts give StateChanged; failures have no Data.

When to use: enumerate fireproofed member IDs without loading beam objects. Uses GetFireProofedBeamCount before and after the flag-returning list call, validates exact rank-one Int32 shape/length and positive written IDs. Equal counts cannot exclude same-count external edits. No assumption about specification indices is made. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.

```csharp
var result = property.GetFireProofedBeamList();
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
else System.Console.WriteLine(result.Status);
```

### GetFireProofDataForBeam

```csharp
PropertyQueryResult<FireProofingData> GetFireProofDataForBeam(int memberId)
```

Native route: `GetFireProofDataForBeam` (P7). Reads a member's native fireproofing type, thickness and density.

Parameters:

- `memberId`: Positive analytical member ID; not a property reference or fireproofing specification index.

Returns: Success for true/integral one with immutable requested member identity and validated fields; false/zero gives NativeFailure without Data. It does not distinguish missing member from no fireproofing.

When to use: inspect one member without resolving a specification index. Requires written native type BFP=1/CFP=2 and finite nonnegative floating thickness/density, including explicit zero. Native density convention is retained without mass/weight assumptions. AssignedMemberCount and SpecificationIndex are null. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.

```csharp
var result = property.GetFireProofDataForBeam(17);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Type);
else System.Console.WriteLine(result.Status);
```

### GetFireProofingSpecDetails

```csharp
PropertyQueryResult<FireProofingData> GetFireProofingSpecDetails(int specificationIndex)
```

Native route: `GetFireProofingSpecDetails` (P7). Reads native fireproofing definition data and its scalar assigned-member count.

Parameters:

- `specificationIndex`: Positive nonzero-based native fireproofing getter index. Not a specification ID or the zero-based creation index; no +1 conversion is performed.

Returns: Success for true/integral one with immutable requested getter index, verified type, finite nonnegative thickness/density and a nonnegative scalar assigned-member count. False/zero gives NativeFailure.

When to use: inspect a fireproofing definition without converting the zero-based creation result into a getter index. The vendor VBA example declares varAssignCount as Long; the array-of-Long wording in one parameter row is a typo. Only a written scalar integral count is accepted; an array is not silently unwrapped. MemberId is null. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.

```csharp
var result = property.GetFireProofingSpecDetails(1);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.AssignedMemberCount);
else System.Console.WriteLine(result.Status);
```

### GetFireProofingSpecAssignedBeamCount

```csharp
PropertyQueryResult<int> GetFireProofingSpecAssignedBeamCount(int specificationIndex)
```

Native route: `GetFireProofingSpecAssignedBeamCount` (P7). Reads the member count assigned to a native fireproofing getter index.

Parameters:

- `specificationIndex`: Positive nonzero-based native fireproofing getter index. Not a specification ID or the zero-based creation index; no +1 conversion is performed.

Returns: Success with the nonnegative native count, including zero. Undocumented negatives/malformed counts throw; no specification-existence flag is inferred from zero.

When to use: inspect assignment size for an explicitly established positive native getter index. It is not an assignment ID or a conversion of CreatedSpecificationIndex. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.

```csharp
var result = property.GetFireProofingSpecAssignedBeamCount(1);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
```

### GetFireProofingSpecAssignedBeamList

```csharp
PropertyQueryResult<IReadOnlyList<int>> GetFireProofingSpecAssignedBeamList(int specificationIndex, int maximumResults = 100000)
```

Native route: `GetFireProofingSpecAssignedBeamList` (P7). Reads a bounded owned member-ID list for a native fireproofing getter index.

Parameters:

- `specificationIndex`: Positive nonzero-based native fireproofing getter index. Not a specification ID or the zero-based creation index; no +1 conversion is performed.
- `maximumResults`: Allocation limit from 1 through 100000, default 100000. Larger native counts give LimitExceeded before buffer allocation/retrieval.

Returns: Success with a read-only native-order ID list, including duplicates; zero count gives empty data without retrieval. False/zero gives NativeFailure, count changes StateChanged, and oversize counts LimitExceeded.

When to use: enumerate a specification's assignments without synthesizing a specification ID. Uses the indexed count before/after retrieval and exact rank-one Int32 positive-ID validation. Every dependent dispatch rechecks binding. Count equality does not establish a model-wide atomic snapshot. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.

```csharp
var result = property.GetFireProofingSpecAssignedBeamList(1);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
else System.Console.WriteLine(result.Status);
```

### CreateMemberAttribute

```csharp
PropertyMutationResult CreateMemberAttribute(PropertyAttribute attribute)
```

Native route: `CreateMemberAttribute` (P7). Creates/updates an exact native member attribute definition.

Parameters:

- `attribute`: Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.

Returns: Succeeded for integral zero and NativeFailure for -1; retains actual code, immutable pair and explicit native member scope. No attribute ID or Created/Updated distinction is fabricated.

When to use: request an exact name/value definition without assuming that zero means a newly allocated ID or distinguishing creation from update. Does not implicitly assign entities, trim text or convert strings. Input snapshots are validated before native writes. Native acknowledgement is not persistence read-back; partial changes may remain after failure or a stale-binding/transport exception. No hidden save, retry, transaction, unit conversion or rollback occurs.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute("Purpose", " REVIEWED ");
    var result = property.CreateMemberAttribute(attribute);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### CreateElementAttribute

```csharp
PropertyMutationResult CreateElementAttribute(PropertyAttribute attribute)
```

Native route: `CreateElementAttribute` (P7). Creates/updates an exact native element attribute definition.

Parameters:

- `attribute`: Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.

Returns: Succeeded for integral zero and NativeFailure for -1; retains actual code, immutable pair and explicit native element scope. No attribute ID or Created/Updated distinction is fabricated.

When to use: request an exact name/value definition without assuming that zero means a newly allocated ID or distinguishing creation from update. Does not implicitly assign entities, trim text or convert strings. Input snapshots are validated before native writes. Native acknowledgement is not persistence read-back; partial changes may remain after failure or a stale-binding/transport exception. No hidden save, retry, transaction, unit conversion or rollback occurs.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute("Purpose", " REVIEWED ");
    var result = property.CreateElementAttribute(attribute);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### DeleteMemberAttribute

```csharp
PropertyMutationResult DeleteMemberAttribute(PropertyAttribute attribute)
```

Native route: `DeleteMemberAttribute` (P7). Deletes an exact native member attribute definition.

Parameters:

- `attribute`: Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.

Returns: Succeeded for integral zero and NativeFailure for -1; retains actual code, immutable pair and explicit native member scope. No attribute ID or Created/Updated distinction is fabricated.

When to use: deliberately remove the exact definition; existing native assignments/effects are subject to STAAD and no recreation/rollback is attempted. Does not implicitly assign entities, trim text or convert strings. Input snapshots are validated before native writes. Native acknowledgement is not persistence read-back; partial changes may remain after failure or a stale-binding/transport exception. No hidden save, retry, transaction, unit conversion or rollback occurs.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute("Purpose", " REVIEWED ");
    var result = property.DeleteMemberAttribute(attribute);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### DeleteElementAttribute

```csharp
PropertyMutationResult DeleteElementAttribute(PropertyAttribute attribute)
```

Native route: `DeleteElementAttribute` (P7). Deletes an exact native element attribute definition.

Parameters:

- `attribute`: Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.

Returns: Succeeded for integral zero and NativeFailure for -1; retains actual code, immutable pair and explicit native element scope. No attribute ID or Created/Updated distinction is fabricated.

When to use: deliberately remove the exact definition; existing native assignments/effects are subject to STAAD and no recreation/rollback is attempted. Does not implicitly assign entities, trim text or convert strings. Input snapshots are validated before native writes. Native acknowledgement is not persistence read-back; partial changes may remain after failure or a stale-binding/transport exception. No hidden save, retry, transaction, unit conversion or rollback occurs.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute("Purpose", " REVIEWED ");
    var result = property.DeleteElementAttribute(attribute);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### GetMemberCountByAttribute

```csharp
PropertyQueryResult<PropertyAttributeMatchCount> GetMemberCountByAttribute(PropertyAttribute attribute)
```

Native route: `GetMemberCountByAttribute` (P7). Reads the native member count for one exact attribute pair.

Parameters:

- `attribute`: Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.

Returns: Success with an immutable requested pair/domain and nonnegative count, including zero. Native -1 gives NativeFailure. A rewritten by-reference input value gives StateChanged with no Data.

When to use: inspect an attribute subset in the explicit member domain. Vendor metadata passes the value string by reference although the help describes it as input; a local copy is sent and compared ordinally afterward. No data is attributed to the original pair if native rewrites it. Zero does not prove a definition exists. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.

```csharp
var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute("Purpose", " REVIEWED ");
var result = property.GetMemberCountByAttribute(attribute);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
else System.Console.WriteLine(result.Status);
```

### GetMemberListByAttribute

```csharp
PropertyQueryResult<PropertyAttributeEntityIds> GetMemberListByAttribute(PropertyAttribute attribute, int maximumResults = 100000)
```

Native route: `GetMemberListByAttribute` (P7). Reads a bounded owned native member ID snapshot for an exact attribute pair.

Parameters:

- `attribute`: Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.
- `maximumResults`: Allocation limit from 1 through 100000, default 100000. Larger native counts give LimitExceeded before buffer allocation/retrieval.

Returns: Success with immutable pair/domain correspondence and read-only positive EntityIds in native order, retaining duplicates. Native -1 gives NativeFailure, oversize count LimitExceeded, and count/input-value changes StateChanged. Zero inventory is successful empty data without list retrieval.

When to use: enumerate matching member IDs without geometry hydration. Uses the matching native count/list/count sequence within the shared gate; the list's integral zero is success, not a returned count. Native metadata requires ref string and ref Object outputs; local copies keep caller text immutable. Exact Int32 rank/length and every written positive ID are validated. Count equality cannot exclude same-count external edits. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.

```csharp
var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute("Purpose", " REVIEWED ");
var result = property.GetMemberListByAttribute(attribute);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.EntityIds.Count);
else System.Console.WriteLine(result.Status);
```

### GetElementCountByAttribute

```csharp
PropertyQueryResult<PropertyAttributeMatchCount> GetElementCountByAttribute(PropertyAttribute attribute)
```

Native route: `GetElementCountByAttribute` (P7). Reads the native element count for one exact attribute pair.

Parameters:

- `attribute`: Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.

Returns: Success with an immutable requested pair/domain and nonnegative count, including zero. Native -1 gives NativeFailure. A rewritten by-reference input value gives StateChanged with no Data.

When to use: inspect an attribute subset in the explicit element domain. Vendor metadata passes the value string by reference although the help describes it as input; a local copy is sent and compared ordinally afterward. No data is attributed to the original pair if native rewrites it. Zero does not prove a definition exists. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.

```csharp
var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute("Purpose", " REVIEWED ");
var result = property.GetElementCountByAttribute(attribute);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
else System.Console.WriteLine(result.Status);
```

### GetElementListByAttribute

```csharp
PropertyQueryResult<PropertyAttributeEntityIds> GetElementListByAttribute(PropertyAttribute attribute, int maximumResults = 100000)
```

Native route: `GetElementListByAttribute` (P7). Reads a bounded owned native element ID snapshot for an exact attribute pair.

Parameters:

- `attribute`: Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.
- `maximumResults`: Allocation limit from 1 through 100000, default 100000. Larger native counts give LimitExceeded before buffer allocation/retrieval.

Returns: Success with immutable pair/domain correspondence and read-only positive EntityIds in native order, retaining duplicates. Native -1 gives NativeFailure, oversize count LimitExceeded, and count/input-value changes StateChanged. Zero inventory is successful empty data without list retrieval.

When to use: enumerate matching element IDs without geometry hydration. Uses the matching native count/list/count sequence within the shared gate; the list's integral zero is success, not a returned count. Native metadata requires ref string and ref Object outputs; local copies keep caller text immutable. Exact Int32 rank/length and every written positive ID are validated. Count equality cannot exclude same-count external edits. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.

```csharp
var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute("Purpose", " REVIEWED ");
var result = property.GetElementListByAttribute(attribute);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.EntityIds.Count);
else System.Console.WriteLine(result.Status);
```

### GetMemberAttributeCount

```csharp
PropertyQueryResult<int> GetMemberAttributeCount()
```

Native route: `GetMemberAttributeCount` (P7). Reads the native member-attribute definition count.

Returns: Success with an actual nonnegative integral count, including zero; malformed/negative counts throw.

When to use: size the paired name/value inventory without assuming attributes have numerical definition IDs or that the count represents assignment count. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.

```csharp
var result = property.GetMemberAttributeCount();
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
```

### GetMemberAttributeList

```csharp
PropertyQueryResult<IReadOnlyList<PropertyAttribute>> GetMemberAttributeList(int maximumResults = 100000)
```

Native route: `GetMemberAttributeList` (P7). Reads a bounded owned paired inventory of native member attribute names and values.

Parameters:

- `maximumResults`: Allocation limit from 1 through 100000, default 100000. Larger native counts give LimitExceeded before buffer allocation/retrieval.

Returns: Success with read-only immutable pairs in native order, retaining duplicates and empty/whitespace strings. Count/input state changes give StateChanged and allocation excess LimitExceeded. Zero count skips list dispatch.

When to use: enumerate paired metadata without losing name/value correspondence. GetMemberAttributeList returns an integral count, not a success flag; it must equal the preflight count and separate postflight count. Both output buffers must be rank-one String arrays of exactly that size, with every string written and free of NUL. Null sentinels detect unwritten output. No deduplication, case folding or numerical parsing occurs. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.

```csharp
var result = property.GetMemberAttributeList();
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
else System.Console.WriteLine(result.Status);
```

### GetAssignedAttributeCount

```csharp
PropertyQueryResult<AssignedAttributeSummary> GetAssignedAttributeCount(int memberOrPlateId)
```

Native route: `GetAssignedAttributeCount` (P7). Reads the native assigned-attribute count under member-first/plate-fallback identity rules.

Parameters:

- `memberOrPlateId`: Positive native integer ID. STAAD resolves an existing member with that ID first, or a plate if no member exists; the wrapper cannot force plate selection when IDs overlap.

Returns: Success with immutable requested integer identity and a nonnegative count, including zero. Resolved geometry kind, assigned-index base and enumeration context are not certified.

When to use: observe assignment count with native identity priority made explicit. No GetAssignedAttributeByIndex call follows automatically: its index context/base remain gated. This getter can affect native observation context, but no model write is requested. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.

```csharp
var result = property.GetAssignedAttributeCount(17);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
else System.Console.WriteLine(result.Status);
```

### RemoveAttribute

```csharp
PropertyMutationResult RemoveAttribute(PropertyAttribute attribute, IEnumerable<int> memberOrPlateIds)
```

Native route: `RemoveAttribute` (P7). Removes native attribute assignments from explicit ambiguous member/plate IDs.

Parameters:

- `attribute`: Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.
- `memberOrPlateIds`: Finite sequence of 1..100000 positive native ambiguous member/plate integer IDs. Enumerated once with a hard limit, copied, and sent as an Int32 array even for one ID. Order/duplicates retained; empty/null/invalid input is rejected before native writes.

Returns: Succeeded for integral zero and NativeFailure for -1; retains actual code, immutable requested pair/domain and owned requested TargetIds. No per-entity completion result or created identity is inferred.

When to use: deliberately remove the pair from supplied native identities. The help calls these member IDs while vendor metadata names a member/plate list; the result preserves this ambiguity and does not claim a resolved entity kind. Native failure may leave a partially applied change. Input snapshots are validated before native writes. Native acknowledgement is not persistence read-back; partial changes may remain after failure or a stale-binding/transport exception. No hidden save, retry, transaction, unit conversion or rollback occurs.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute("Purpose", " REVIEWED ");
    var result = property.RemoveAttribute(attribute, new[] { 17, 41 });
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### AssignMemberAttribute

```csharp
PropertyMutationResult AssignMemberAttribute(PropertyAttribute attribute, IEnumerable<int> memberIds)
```

Native route: `AssignMemberAttribute` (P7). Assigns an exact native attribute to explicit member IDs.

Parameters:

- `attribute`: Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.
- `memberIds`: Finite sequence of 1..100000 positive native member integer IDs. Enumerated once with a hard limit, copied, and sent as an Int32 array even for one ID. Order/duplicates retained; empty/null/invalid input is rejected before native writes.

Returns: Succeeded for integral zero and NativeFailure for -1; retains actual code, immutable requested pair/domain and owned requested TargetIds. No per-entity completion result or created identity is inferred.

When to use: deliberately assign the existing pair in the explicitly named native member domain without creating a definition or hydrating entities. Native failure may leave a partially applied change. Input snapshots are validated before native writes. Native acknowledgement is not persistence read-back; partial changes may remain after failure or a stale-binding/transport exception. No hidden save, retry, transaction, unit conversion or rollback occurs.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute("Purpose", " REVIEWED ");
    var result = property.AssignMemberAttribute(attribute, new[] { 17, 41 });
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### AssignElementAttribute

```csharp
PropertyMutationResult AssignElementAttribute(PropertyAttribute attribute, IEnumerable<int> elementIds)
```

Native route: `AssignElementAttribute` (P7). Assigns an exact native attribute to explicit element IDs.

Parameters:

- `attribute`: Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.
- `elementIds`: Finite sequence of 1..100000 positive native element integer IDs. Enumerated once with a hard limit, copied, and sent as an Int32 array even for one ID. Order/duplicates retained; empty/null/invalid input is rejected before native writes.

Returns: Succeeded for integral zero and NativeFailure for -1; retains actual code, immutable requested pair/domain and owned requested TargetIds. No per-entity completion result or created identity is inferred.

When to use: deliberately assign the existing pair in the explicitly named native element domain without creating a definition or hydrating entities. Native failure may leave a partially applied change. Input snapshots are validated before native writes. Native acknowledgement is not persistence read-back; partial changes may remain after failure or a stale-binding/transport exception. No hidden save, retry, transaction, unit conversion or rollback occurs.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute("Purpose", " REVIEWED ");
    var result = property.AssignElementAttribute(attribute, new[] { 17, 41 });
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### CreateUptTable

```csharp
PropertyMutationResult CreateUptTable(UserProvidedTableType tableType)
```

Native route: `CreateUptTable` (P8). Creates a native user-provided table of an explicit reviewed type.

Parameters:

- `tableType`: Defined native type 1..10: WideFlange, Channel, Angle, DoubleAngle, Tee, Pipe, Tube, General, ISection or Prismatic. This is not a section property reference or country code.

Returns: Created with a separate positive CreatedTableId for a positive integral native result. -6031 gives InvalidInput; zero/other undocumented responses throw. No section property/specification ID is populated.

When to use: create an empty UPT table independently from adding a named section or assigning members. Requests run on the captured native receiver using the reviewed metadata's by-value input, despite C++ VARIANT-reference spelling. No staged workflow/cleanup or table-type lookup is hidden. Input snapshots are validated before native writes. Native acknowledgement is not persistence read-back; partial changes may remain after failure or a stale-binding/transport exception. No hidden save, retry, transaction, unit conversion or rollback occurs.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var result = property.CreateUptTable(StaadPro.Interop.Enums.Property.UserProvidedTableType.WideFlange);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### AddUptPropertyWideFlange

```csharp
PropertyMutationResult AddUptPropertyWideFlange(int tableId, string sectionName, UptWideFlangeDefinition definition)
```

Native route: `AddUptPropertyWideFlange` (P8). Adds an explicit wide-flange section to an existing native UPT table.

Parameters:

- `tableId`: Positive existing native UPT table ID, not an analytical member or section property ID. Caller must establish that the table type supports the requested insertion.
- `sectionName`: Exact nonblank native UPT section name without NUL. Case/whitespace preserved; no catalog normalization or automatic replacement.
- `definition`: Non-null immutable AX,D,TW,WF,TF,IZ,IY,IX,AY,AZ values in this native order. Required area/dimensions are finite positive; inertias/shear areas finite nonnegative. Areas use length squared, inertia slots length to fourth power, dimensions length. This route documents IZ as torsional constant and IX about local Z; no relabeling from another layout.

Returns: Succeeded for integral zero, NativeFailure for -6032 and AlreadyExists for -6045. Retains actual response and requested TableId/SectionName. No created section property reference or table ID is fabricated.

When to use: insert explicit scalar UPT values without silently changing a duplicate named section. Table creation and insertion are separate model mutations; an insertion failure does not remove the table. Vendor metadata accepts inputs by value. Native success does not prove a new definition rather than an accepted insertion and does not assign members. Input snapshots are validated before native writes. Native acknowledgement is not persistence read-back; partial changes may remain after failure or a stale-binding/transport exception. No hidden save, retry, transaction, unit conversion or rollback occurs.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var definition = new StaadPro.Interop.Models.Property.UptWideFlangeDefinition(0.01, 0.3, 0.006, 0.15, 0.01, 0.000001, 0.00001, 0.0001, 0.002, 0.003);
    var result = property.AddUptPropertyWideFlange(7, "EXPLICIT_WF", definition);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

### AddUptPropertyWideFlangeUnequal

```csharp
PropertyMutationResult AddUptPropertyWideFlangeUnequal(int tableId, string sectionName, UptUnequalWideFlangeDefinition definition)
```

Native route: `AddUptPropertyWideFlangeUnequal` (P8). Adds an explicit unequal wide-flange section through the native twelve-double UPT route.

Parameters:

- `tableId`: Positive existing native UPT table ID, not an analytical member or section property ID. Caller must establish that the table type supports the requested insertion.
- `sectionName`: Exact nonblank native UPT section name without NUL. Case/whitespace preserved; no catalog normalization or automatic replacement.
- `definition`: Non-null immutable primary AX,D,TW,WF,TF,IZ,IY,IX,AY,AZ values followed by explicit WF1 and TF1 in native slots ten/eleven. A fresh exact twelve-double array is sent by value per reviewed vendor metadata. WF1/TF1 retain native second-flange labels without guessing their physical orientation.

Returns: Succeeded for true/integral one and NativeFailure for false/integral zero. Actual Boolean/integral evidence is retained separately with requested TableId/SectionName; no duplicate-specific status or created section ID is inferred.

When to use: insert the unequal shape using its distinct Boolean-returning native route; zero is failure here, unlike the scalar wide-flange route. Input values remain immutable even if a native buffer is altered. Does not create a table, replace a duplicate by fallback, create an assigned property or assign members. Input snapshots are validated before native writes. Native acknowledgement is not persistence read-back; partial changes may remain after failure or a stale-binding/transport exception. No hidden save, retry, transaction, unit conversion or rollback occurs.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
if (applyChanges)
{
    var definition = new StaadPro.Interop.Models.Property.UptWideFlangeDefinition(0.01, 0.3, 0.006, 0.15, 0.01, 0.000001, 0.00001, 0.0001, 0.002, 0.003);
    var unequal = new StaadPro.Interop.Models.Property.UptUnequalWideFlangeDefinition(definition, 0.2, 0.015);
    var result = property.AddUptPropertyWideFlangeUnequal(7, "EXPLICIT_UNEQUAL", unequal);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
}
```

## Qualification and next work

Offline checks cover every new route, exact interface/adapter XML, binding replacement/disposal, transport failures, strict native return conventions, immutable ownership, bounded snapshots, caller-thread serialization, by-reference input rewriting and native UPT scalar/array ordering. The installed consumer uses a fresh project/feed/cache, checks the restored archive hash and compiles against the installed DLL/XML without a project reference.

Live native transport, actual attribute deletion/assignment effects, UPT persistence, model units and engineering behavior remain unqualified. Mutation failures may leave partial changes; no implicit save, retry or rollback is supplied. Guarded XML examples are supplemented by dedicated managed mutation checks; no live model is changed by verification.

Four gated P7 routes remain, followed by 27 P8, eight P9 and 24 P10 routes (63 total). The next reviewed sequence continues at P8 AddUptPropertyWideFlangeComposite while the four gaps await evidence. Custom IOSProperty implementations must implement these 25 additional methods.
