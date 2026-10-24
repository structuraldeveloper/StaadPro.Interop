# Property catalog capabilities and cleanup

Incorporated **25 Property operations**: all five remaining P9 catalog queries and twenty P10 removal/deletion functions. The current surface has **245 common overloads plus five separately acquired catalog queries**, covering **191 of 204 active native routes**, with **13 remaining**. P9 is complete; P10 has twenty of twenty-four routes. The nine earlier P7/P8 contract gaps remain pending. See [all 25 catalog/cleanup contracts](Property-Catalog-Capabilities-and-Cleanup.md).

Passed a Release build with zero warnings/errors and **5,156 offline tests**, including **197 new cases**. The isolated PackageReference-only consumer verifies **944 selected XML examples**: 490 common Property examples plus twelve optional catalog/acquisition examples, alongside the existing groups. Separate managed checks exercise all twenty new mutations and five catalog queries through both public surfaces. Live native mutation and engineering qualification remain pending.

Added a passive version/signature acquisition boundary for the newer catalog group on exact native profiles **25.00.01.424** and **26.00.00.340**. Retained operation-specific zero-success, Boolean-success and explicit no-specification removal outcomes, owned bounded bulk targets, and separate member-end, cable-definition and analytical-node selectors. No native model is modified by verification.

Renamed **17 public UPT methods** to PascalCase, including **AddUptPropertyChannel**, **AddUptPropertyWideFlange** and **CreateUptTable**. This preview requires callers to update those method names. Parameter names, types, ordering, defaults and return types are preserved. Bentley native dispatch names and recorded contract evidence retain their original spelling.

## Acquiring newer catalog APIs

The five newer catalog calls live on IOSPropertyStandardCatalog, obtained through property.Capabilities.GetStandardCatalog(). They are not members of the common IOSProperty interface or OSPropertyAdapter. The acquisition interface and optional adapter implementations have matching complete XML documentation.

Acquisition reads the acquired native version, never the host ApplicationVersion. An exact reviewed build and compatible signatures for all five methods are required. IDispatch name/type metadata is inspected without invoking catalog functions. Visible managed test receivers must expose exactly one public instance method per name, an Object return and zero or one by-value Object argument as documented by vendor CLR metadata. Missing methods, ambiguous/incompatible signatures, unreviewed builds and unknown versions withhold the entire group, with null Capability. Unexpected metadata transport exceptions propagate. A grant qualifies metadata; it does not certify live behavior.

```csharp
IOSProperty property = wrapper.Property;
var grant = property.Capabilities.GetStandardCatalog();
if (grant.IsAvailable)
{
    var origin = grant.Capability.IsStandardDatabaseSection(7);
    Console.WriteLine(origin.Data.IsStandardDatabase);
}
```

Each acquired call rechecks version and captured receiver under the same Property gate as common operations, on the caller thread. Disposal/replacement invalidates old grants. A false origin flag is successful nonstandard-source data; empty catalog text is absence, with no Data. Native strings retain whitespace/case and are not parsed or normalized. Undocumented null text is rejected for section names; default-folder null is explicitly unavailable. No filesystem check or path creation occurs.

## Removal identities and native outcomes

| Native convention | Managed outcome |
| --- | --- |
| Integral 0 success / -1 failure | Succeeded / NativeFailure; Boolean is rejected |
| Boolean true/false or integral 1/0 | Succeeded / NativeFailure, actual evidence retained |
| RemoveAllElementOffsetSpec integral 1/0 | Succeeded / NoSpecification; absence has IsSuccessful false |

Bulk plate/solid material removal accepts 1..100000 positive IDs, enumerated once, copied and retained in order with duplicates. Scalar thickness removal takes one plate ID. Deleting a property/specification definition uses its definition ID; removal from a member/plate uses analytical entity IDs. Shared definitions are not assumed deleted when an assignment is removed. Native dependency behavior may prevent deletion. Whole-model relation/offset removal is explicit and supplies no hidden confirmation or undo.

Release/offset location is MemberEnd Start=0 or End=1. Cable removal instead takes CableDefinitionKind InitialTension=0 or UnstressedLength=1, despite the native parameter being called a location. Plate-node release removal takes an analytical node ID, not a corner index. PropertyMutationResult retains these requested identities in separate immutable fields, without invented created IDs. Acknowledgement does not prove save/persistence, engineering validity or atomicity; partial changes may remain after failure. No retry, analysis, hidden save or rollback is added.

## Remaining work and verification limits

P10 still has RemoveAllElementNodeReleaseSpec, RemoveMemberFireProofingSpecFromBeam, GetPublishedProfileName and GetSTAADProfileName pending. Four P7 and five P8 transport/status/index contracts remain gated as recorded in the [roadmap](reports/OpenSTAAD-Property-Multi-Version-Architecture-and-Plan.md). None is counted as implemented. The next pending roadmap allocation is P10 RemoveAllElementNodeReleaseSpec.

The [contract index](contracts/PropertyCleanupContractIndex.json) records every managed contract. The [three-version native comparison](reports/data/OpenSTAAD-Property-2024-2026-Comparison.json) retains original evidence, native declarations, vendor metadata and fingerprints. Managed regressions cover operation-specific responses, bounded ownership, stale binding, calling threads, capability refusal, synthetic ITypeInfo descriptors and native/managed naming separation. Installed-consumer verification checks the expanded XML beside the restored DLL.

## The 25 contracts

Examples assume IOSProperty property = wrapper.Property. Mutation examples default to disabled.

### 1. RemovePropertyFromPlate

Remove Thickness Property From the specific Plate.

```csharp
PropertyMutationResult RemovePropertyFromPlate(int plateId);
```

| Parameter | Contract |
| --- | --- |
| `plateId` | Positive existing native analytical plate ID. |

**Returns:** Succeeded for actual Boolean true or integral one; NativeFailure for Boolean false or integral zero. Other scalar shapes/codes are rejected. The actual native Boolean/integer response and requested target identity are retained separately.

When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
if (applyChanges)
{
    var result = property.RemovePropertyFromPlate(101);
    System.Console.WriteLine(result.Status);
}
```

### 2. RemoveMaterialFromPlate

Remove Material From the specific Plates.

```csharp
PropertyMutationResult RemoveMaterialFromPlate(IEnumerable<int> plateIds);
```

| Parameter | Contract |
| --- | --- |
| `plateIds` | Non-null sequence of 1..100000 positive analytical IDs, enumerated once; input order and duplicates are preserved in an owned copy. No inferred member/plate/solid conversion or deduplication. |

**Returns:** Succeeded for actual Boolean true or integral one; NativeFailure for Boolean false or integral zero. Other scalar shapes/codes are rejected. The actual native Boolean/integer response and requested target identity are retained separately.

When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
if (applyChanges)
{
    var result = property.RemoveMaterialFromPlate(new[] { 101, 102 });
    System.Console.WriteLine(result.Status);
}
```

### 3. RemoveMaterialFromSolid

Remove Material From the specific Solids.

```csharp
PropertyMutationResult RemoveMaterialFromSolid(IEnumerable<int> solidIds);
```

| Parameter | Contract |
| --- | --- |
| `solidIds` | Non-null sequence of 1..100000 positive analytical IDs, enumerated once; input order and duplicates are preserved in an owned copy. No inferred member/plate/solid conversion or deduplication. |

**Returns:** Succeeded for actual Boolean true or integral one; NativeFailure for Boolean false or integral zero. Other scalar shapes/codes are rejected. The actual native Boolean/integer response and requested target identity are retained separately.

When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
if (applyChanges)
{
    var result = property.RemoveMaterialFromSolid(new[] { 101, 102 });
    System.Console.WriteLine(result.Status);
}
```

### 4. DeleteMemberReleaseSpec

Delete MEMBER RELEASE specification.

```csharp
PropertyMutationResult DeleteMemberReleaseSpec(int memberId, MemberEnd memberEnd);
```

| Parameter | Contract |
| --- | --- |
| `memberId` | Positive existing native analytical member ID. |
| `memberEnd` | Native Start=0 or End=1 selector. Other values are rejected; no both-ends or whole-member default is inferred. |

**Returns:** Succeeded for actual Boolean true or integral one; NativeFailure for Boolean false or integral zero. Other scalar shapes/codes are rejected. The actual native Boolean/integer response and requested target identity are retained separately.

When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
if (applyChanges)
{
    var result = property.DeleteMemberReleaseSpec(101, StaadPro.Interop.Enums.MemberEnd.Start);
    System.Console.WriteLine(result.Status);
}
```

### 5. DeleteAllControlDependentRelations

Delete all control/dependent joint specifications from model.

```csharp
PropertyMutationResult DeleteAllControlDependentRelations();
```

| Parameter | Contract |
| --- | --- |
| None | Uses the captured receiver. |

**Returns:** Succeeded for integral zero and NativeFailure for integral -1. Boolean, floating/string/null and undocumented integral responses are rejected.

When to use: Applies to all native control/dependent relations in the acquired model; no selective filter, confirmation dialog or implicit undo is supplied. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
if (applyChanges)
{
    var result = property.DeleteAllControlDependentRelations();
    System.Console.WriteLine(result.Status);
}
```

### 6. RemoveAllElementOffsetSpec

Remove all element node offset specification from the model.

```csharp
PropertyMutationResult RemoveAllElementOffsetSpec();
```

| Parameter | Contract |
| --- | --- |
| None | Uses the captured receiver. |

**Returns:** Succeeded for integral one; NoSpecification for integral zero, which explicitly reports that no element offset specification is present. IsSuccessful is false for NoSpecification; no success is fabricated.

When to use: Applies to all element offsets in the acquired model; zero is documented absence, not a failed engineering read. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
if (applyChanges)
{
    var result = property.RemoveAllElementOffsetSpec();
    System.Console.WriteLine(result.Status);
}
```

### 7. RemovePropertyFromBeam

Remove property from beam.

```csharp
PropertyMutationResult RemovePropertyFromBeam(int memberId);
```

| Parameter | Contract |
| --- | --- |
| `memberId` | Positive existing native analytical member ID. |

**Returns:** Succeeded for integral zero and NativeFailure for integral -1. Boolean, floating/string/null and undocumented integral responses are rejected.

When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
if (applyChanges)
{
    var result = property.RemovePropertyFromBeam(101);
    System.Console.WriteLine(result.Status);
}
```

### 8. DeleteProperty

Delete property based the on the property ID passed.

```csharp
PropertyMutationResult DeleteProperty(int propertyId);
```

| Parameter | Contract |
| --- | --- |
| `propertyId` | Positive existing native section property reference, distinct from analytical entity IDs. |

**Returns:** Succeeded for actual Boolean true or integral one; NativeFailure for Boolean false or integral zero. Other scalar shapes/codes are rejected. The actual native Boolean/integer response and requested target identity are retained separately.

When to use: Deletes a definition using its native ID; do not confuse this with merely removing one assignment. Native dependencies may prevent deletion. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
if (applyChanges)
{
    var result = property.DeleteProperty(7);
    System.Console.WriteLine(result.Status);
}
```

### 9. DeleteMemberSpec

Delete specification based on the specification number passed.

```csharp
PropertyMutationResult DeleteMemberSpec(int specificationId);
```

| Parameter | Contract |
| --- | --- |
| `specificationId` | Positive existing native specification definition ID, distinct from assigned analytical member IDs. |

**Returns:** Succeeded for actual Boolean true or integral one; NativeFailure for Boolean false or integral zero. Other scalar shapes/codes are rejected. The actual native Boolean/integer response and requested target identity are retained separately.

When to use: Deletes a definition using its native ID; do not confuse this with merely removing one assignment. Native dependencies may prevent deletion. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
if (applyChanges)
{
    var result = property.DeleteMemberSpec(7);
    System.Console.WriteLine(result.Status);
}
```

### 10. RemoveMemberReleaseSpecFromBeam

Removes the member specification from a particular member at the provided location (Start or End)..

```csharp
PropertyMutationResult RemoveMemberReleaseSpecFromBeam(int memberId, MemberEnd memberEnd);
```

| Parameter | Contract |
| --- | --- |
| `memberId` | Positive existing native analytical member ID. |
| `memberEnd` | Native Start=0 or End=1 selector. Other values are rejected; no both-ends or whole-member default is inferred. |

**Returns:** Succeeded for actual Boolean true or integral one; NativeFailure for Boolean false or integral zero. Other scalar shapes/codes are rejected. The actual native Boolean/integer response and requested target identity are retained separately.

When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
if (applyChanges)
{
    var result = property.RemoveMemberReleaseSpecFromBeam(101, StaadPro.Interop.Enums.MemberEnd.Start);
    System.Console.WriteLine(result.Status);
}
```

### 11. RemoveMemberOffsetSpecFromBeam

Removes the member offset specification from a particular member at the provided location (Start or End).

```csharp
PropertyMutationResult RemoveMemberOffsetSpecFromBeam(int memberId, MemberEnd memberEnd);
```

| Parameter | Contract |
| --- | --- |
| `memberId` | Positive existing native analytical member ID. |
| `memberEnd` | Native Start=0 or End=1 selector. Other values are rejected; no both-ends or whole-member default is inferred. |

**Returns:** Succeeded for actual Boolean true or integral one; NativeFailure for Boolean false or integral zero. Other scalar shapes/codes are rejected. The actual native Boolean/integer response and requested target identity are retained separately.

When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
if (applyChanges)
{
    var result = property.RemoveMemberOffsetSpecFromBeam(101, StaadPro.Interop.Enums.MemberEnd.Start);
    System.Console.WriteLine(result.Status);
}
```

### 12. RemoveMemberTrussSpecFromBeam

Remove member truss specification from beam.

```csharp
PropertyMutationResult RemoveMemberTrussSpecFromBeam(int memberId);
```

| Parameter | Contract |
| --- | --- |
| `memberId` | Positive existing native analytical member ID. |

**Returns:** Succeeded for integral zero and NativeFailure for integral -1. Boolean, floating/string/null and undocumented integral responses are rejected.

When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
if (applyChanges)
{
    var result = property.RemoveMemberTrussSpecFromBeam(101);
    System.Console.WriteLine(result.Status);
}
```

### 13. RemoveMemberInactiveSpecFromBeam

Remove member inactive specification from beam.

```csharp
PropertyMutationResult RemoveMemberInactiveSpecFromBeam(int memberId);
```

| Parameter | Contract |
| --- | --- |
| `memberId` | Positive existing native analytical member ID. |

**Returns:** Succeeded for integral zero and NativeFailure for integral -1. Boolean, floating/string/null and undocumented integral responses are rejected.

When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
if (applyChanges)
{
    var result = property.RemoveMemberInactiveSpecFromBeam(101);
    System.Console.WriteLine(result.Status);
}
```

### 14. RemoveMemberTensionSpecFromBeam

Remove member tension specification from beam.

```csharp
PropertyMutationResult RemoveMemberTensionSpecFromBeam(int memberId);
```

| Parameter | Contract |
| --- | --- |
| `memberId` | Positive existing native analytical member ID. |

**Returns:** Succeeded for integral zero and NativeFailure for integral -1. Boolean, floating/string/null and undocumented integral responses are rejected.

When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
if (applyChanges)
{
    var result = property.RemoveMemberTensionSpecFromBeam(101);
    System.Console.WriteLine(result.Status);
}
```

### 15. RemoveMemberCompressionSpecFromBeam

Remove member compression specification from beam.

```csharp
PropertyMutationResult RemoveMemberCompressionSpecFromBeam(int memberId);
```

| Parameter | Contract |
| --- | --- |
| `memberId` | Positive existing native analytical member ID. |

**Returns:** Succeeded for integral zero and NativeFailure for integral -1. Boolean, floating/string/null and undocumented integral responses are rejected.

When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
if (applyChanges)
{
    var result = property.RemoveMemberCompressionSpecFromBeam(101);
    System.Console.WriteLine(result.Status);
}
```

### 16. RemoveMemberIgnoreStiffSpecFromBeam

Remove member ignore stiff specification from beam.

```csharp
PropertyMutationResult RemoveMemberIgnoreStiffSpecFromBeam(int memberId);
```

| Parameter | Contract |
| --- | --- |
| `memberId` | Positive existing native analytical member ID. |

**Returns:** Succeeded for integral zero and NativeFailure for integral -1. Boolean, floating/string/null and undocumented integral responses are rejected.

When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
if (applyChanges)
{
    var result = property.RemoveMemberIgnoreStiffSpecFromBeam(101);
    System.Console.WriteLine(result.Status);
}
```

### 17. RemoveMemberCableSpecFromBeam

Removes the member cable specification from a particular member at the provided location type (Tension or Length).

```csharp
PropertyMutationResult RemoveMemberCableSpecFromBeam(int memberId, CableDefinitionKind definitionKind);
```

| Parameter | Contract |
| --- | --- |
| `memberId` | Positive existing native analytical member ID. |
| `definitionKind` | Native InitialTension=0 or UnstressedLength=1 cable-definition selector. This is not a member-end location despite the native parameter label. |

**Returns:** Succeeded for actual Boolean true or integral one; NativeFailure for Boolean false or integral zero. Other scalar shapes/codes are rejected. The actual native Boolean/integer response and requested target identity are retained separately.

When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
if (applyChanges)
{
    var result = property.RemoveMemberCableSpecFromBeam(101, StaadPro.Interop.Enums.Property.CableDefinitionKind.InitialTension);
    System.Console.WriteLine(result.Status);
}
```

### 18. RemoveElementPlaneStressSpecFromPlate

Remove element plane stress specification from plate.

```csharp
PropertyMutationResult RemoveElementPlaneStressSpecFromPlate(int plateId);
```

| Parameter | Contract |
| --- | --- |
| `plateId` | Positive existing native analytical plate ID. |

**Returns:** Succeeded for integral zero and NativeFailure for integral -1. Boolean, floating/string/null and undocumented integral responses are rejected.

When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
if (applyChanges)
{
    var result = property.RemoveElementPlaneStressSpecFromPlate(101);
    System.Console.WriteLine(result.Status);
}
```

### 19. RemoveElementIgnoreInplaneRotnSpecFromPlate

Remove element ignore in plane rotation specification from plate.

```csharp
PropertyMutationResult RemoveElementIgnoreInplaneRotnSpecFromPlate(int plateId);
```

| Parameter | Contract |
| --- | --- |
| `plateId` | Positive existing native analytical plate ID. |

**Returns:** Succeeded for integral zero and NativeFailure for integral -1. Boolean, floating/string/null and undocumented integral responses are rejected.

When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
if (applyChanges)
{
    var result = property.RemoveElementIgnoreInplaneRotnSpecFromPlate(101);
    System.Console.WriteLine(result.Status);
}
```

### 20. RemoveElementNodeReleaseSpecFromPlate

Remove element node release specification from plate.

```csharp
PropertyMutationResult RemoveElementNodeReleaseSpecFromPlate(int plateId, int nodeId);
```

| Parameter | Contract |
| --- | --- |
| `plateId` | Positive existing native analytical plate ID. |
| `nodeId` | Positive analytical node ID associated with the intended plate. This is not a plate corner index; no membership check or local/global conversion is inferred. |

**Returns:** Succeeded for integral zero and NativeFailure for integral -1. Boolean, floating/string/null and undocumented integral responses are rejected.

When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
if (applyChanges)
{
    var result = property.RemoveElementNodeReleaseSpecFromPlate(101, 11);
    System.Console.WriteLine(result.Status);
}
```

### 21. GetDefaultStandardProfileDBFolder

Gets standard profile default database folder path.

```csharp
PropertyQueryResult<string> GetDefaultStandardProfileDBFolder();
```

| Parameter | Contract |
| --- | --- |
| None | Uses the captured receiver. |

**Returns:** Exact native default-folder text; native null/DBNull/empty returns EmptyOrUnavailable with no Data. Non-string/NUL text is rejected; no filesystem existence or fallback is inferred.

When to use: observe the native standard-section catalog through the separately acquired property.Capabilities.GetStandardCatalog capability. Acquisition requires exact native 25.00.01.424 or 26.00.00.340 and all five compatible member signatures, without invoking catalog functions. Every call rechecks the captured binding and native version under the shared Property gate. The host version does not enable native APIs. No catalog parsing, profile normalization, file probing, unit conversion, model mutation, retry or engineering inference occurs. A grant is metadata qualification, not live runtime certification.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
var grant = property.Capabilities.GetStandardCatalog();
if (grant.IsAvailable)
{
    var result = grant.Capability.GetDefaultStandardProfileDBFolder();
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.Data);
}
```

### 22. IsStandardDatabaseSection

Checks if the specified section property reference number is from standard section database source or not.

```csharp
PropertyQueryResult<StandardSectionSource> IsStandardDatabaseSection(int propertyId);
```

| Parameter | Contract |
| --- | --- |
| `propertyId` | Positive native section property reference ID, distinct from analytical member IDs, table IDs and native indices. |

**Returns:** Success with requested property identity and the actual Boolean/integral 0/1 origin observation. False is valid data identifying a nonstandard source, not a native failure or a missing-reference proof.

When to use: observe the native standard-section catalog through the separately acquired property.Capabilities.GetStandardCatalog capability. Acquisition requires exact native 25.00.01.424 or 26.00.00.340 and all five compatible member signatures, without invoking catalog functions. Every call rechecks the captured binding and native version under the shared Property gate. The host version does not enable native APIs. No catalog parsing, profile normalization, file probing, unit conversion, model mutation, retry or engineering inference occurs. A grant is metadata qualification, not live runtime certification.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
var grant = property.Capabilities.GetStandardCatalog();
if (grant.IsAvailable)
{
    var result = grant.Capability.IsStandardDatabaseSection(7);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.Data.IsStandardDatabase);
}
```

### 23. GetStandardSectionDatabaseName

Gets standard section database name for the specified section property reference number.

```csharp
PropertyQueryResult<StandardSectionText> GetStandardSectionDatabaseName(int propertyId);
```

| Parameter | Contract |
| --- | --- |
| `propertyId` | Positive native section property reference ID, distinct from analytical member IDs, table IDs and native indices. |

**Returns:** Success with requested property identity and exact nonempty native text. Empty string returns EmptyOrUnavailable because the property is not in the applicable standard database/table. Undocumented null/DBNull, non-string and embedded NUL responses are rejected. Whitespace is preserved, not trimmed.

When to use: observe the native standard-section catalog through the separately acquired property.Capabilities.GetStandardCatalog capability. Acquisition requires exact native 25.00.01.424 or 26.00.00.340 and all five compatible member signatures, without invoking catalog functions. Every call rechecks the captured binding and native version under the shared Property gate. The host version does not enable native APIs. No catalog parsing, profile normalization, file probing, unit conversion, model mutation, retry or engineering inference occurs. A grant is metadata qualification, not live runtime certification.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
var grant = property.Capabilities.GetStandardCatalog();
if (grant.IsAvailable)
{
    var result = grant.Capability.GetStandardSectionDatabaseName(7);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.Data.Text);
}
```

### 24. GetStandardSectionTableName

Get the table name from the standard section database for the specified standard section property reference number.

```csharp
PropertyQueryResult<StandardSectionText> GetStandardSectionTableName(int propertyId);
```

| Parameter | Contract |
| --- | --- |
| `propertyId` | Positive native section property reference ID, distinct from analytical member IDs, table IDs and native indices. |

**Returns:** Success with requested property identity and exact nonempty native text. Empty string returns EmptyOrUnavailable because the property is not in the applicable standard database/table. Undocumented null/DBNull, non-string and embedded NUL responses are rejected. Whitespace is preserved, not trimmed.

When to use: observe the native standard-section catalog through the separately acquired property.Capabilities.GetStandardCatalog capability. Acquisition requires exact native 25.00.01.424 or 26.00.00.340 and all five compatible member signatures, without invoking catalog functions. Every call rechecks the captured binding and native version under the shared Property gate. The host version does not enable native APIs. No catalog parsing, profile normalization, file probing, unit conversion, model mutation, retry or engineering inference occurs. A grant is metadata qualification, not live runtime certification.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
var grant = property.Capabilities.GetStandardCatalog();
if (grant.IsAvailable)
{
    var result = grant.Capability.GetStandardSectionTableName(7);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.Data.Text);
}
```

### 25. GetStandardSectionName

Get the section name from the standard section database and table for the specified standard section property reference number.

```csharp
PropertyQueryResult<StandardSectionText> GetStandardSectionName(int propertyId);
```

| Parameter | Contract |
| --- | --- |
| `propertyId` | Positive native section property reference ID, distinct from analytical member IDs, table IDs and native indices. |

**Returns:** Success with requested property identity and exact nonempty native text. Empty string returns EmptyOrUnavailable because the property is not in the applicable standard database/table. Undocumented null/DBNull, non-string and embedded NUL responses are rejected. Whitespace is preserved, not trimmed.

When to use: observe the native standard-section catalog through the separately acquired property.Capabilities.GetStandardCatalog capability. Acquisition requires exact native 25.00.01.424 or 26.00.00.340 and all five compatible member signatures, without invoking catalog functions. Every call rechecks the captured binding and native version under the shared Property gate. The host version does not enable native APIs. No catalog parsing, profile normalization, file probing, unit conversion, model mutation, retry or engineering inference occurs. A grant is metadata qualification, not live runtime certification.

Invalid inputs fail before native dispatch. Stale/disposed bindings, malformed native responses and transport failures abort without retry; query failures supply no Data.

```csharp
var grant = property.Capabilities.GetStandardCatalog();
if (grant.IsAvailable)
{
    var result = grant.Capability.GetStandardSectionName(7);
    System.Console.WriteLine(result.Status);
    if (result.IsSuccessful) System.Console.WriteLine(result.Data.Text);
}
```
