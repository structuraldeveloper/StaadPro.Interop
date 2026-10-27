# Support discovery, foundation observations and explicit mutations

**Current checkpoint:** Support now implements 27/35 documented routes after eight foundation/assignment additions. See [the current contracts and remaining gates](Commands-and-Support-Porting.md). The nineteen-route increment below remains its historical record.

**Updated:** 26 October 2026

Implemented **19 of 35** documented Support native routes in IOSSupport and the single non-partial OSSupportAdapter. Sixteen documented routes remain. The selected contracts match across installed 2024–2026 references and use one internal common service. There is no artificial per-year adapter or wrapper/provider change.

## Public API and identity rules

Queries return SupportQueryResult<T>; inspect IsSuccessful before Data. Native -1, count zero, support-type zero and Boolean false have operation-specific meanings. SupportMutationResult records requested/created identity and actual native acknowledgement. Void completion has IsSuccessful=false, not fabricated success. No new public ref/out, tuple, raw VARIANT or native array is exposed.

Node IDs, support item/definition IDs and discovery indices are separate. The current creation/definition APIs support positive support references; a native zero is not assumed to be a created ID. Counts never enumerate an ID range. Only GetSupportedNodeIds discovers supported analytical nodes.

Import `StaadPro.Interop.Models.Support` and `StaadPro.Interop.Enums.Support` for results/requests; the ported main-project SupportType, FoundationSupportDirection and FoundationSpringType remain under `StaadPro.Interop.Enums`. Examples assume `support = wrapper.Support` on the established COM apartment. Mutation examples are deliberately guarded.

## Implemented contracts

### GetSupportedNodeCount

~~~csharp
SupportQueryResult<int> GetSupportedNodeCount();
~~~

Native: `GetSupportCount`.

When to use: This is the supported-node count used by GetSupportedNodeIds, not the number of distinct support definitions or any contiguous ID range. Zero is valid data. Exact nonnegative integral returns are required; Boolean success is never treated as count one. No node geometry is hydrated.

~~~csharp
var result = support.GetSupportedNodeCount();
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
~~~

### GetSupportedNodeIds

~~~csharp
SupportQueryResult<IReadOnlyList<int>> GetSupportedNodeIds(int maximumResults = 100000);
~~~

Native: `GetSupportNodes`.

When to use: Use for node discovery instead of enumerating support-definition IDs from a count. The native return is a count, not a Boolean flag; -1 is failure. Count/list/count must agree; a changed count returns StateChanged with no payload and no retry. Zero is rechecked and returns an owned empty list without native list invocation. Initialized Int32 arrays must be completely written; native order and duplicates remain unchanged. Equal counts cannot prove an atomic model snapshot.

| Parameter | Meaning |
| --- | --- |
| `maximumResults` | Managed selection/allocation limit from 1 through 100000. Counts beyond the read limit produce LimitExceeded before allocation; oversized input selections are rejected before native invocation. |

~~~csharp
var result = support.GetSupportedNodeIds(1000);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
~~~

### GetSupportType

~~~csharp
SupportQueryResult<SupportTypeObservation> GetSupportType(int nodeId);
~~~

Native: `GetSupportType`.

When to use: Prefer this inexpensive type-only observation when DOF states and stiffnesses are not needed. Native 0 is successful NoSupport data; -1 is NativeFailure. Unknown nonnegative codes retain NativeTypeCode and KnownType=null, without being cast to a fabricated known enum. GetSupportInformation instead requires a reviewed type and written six-DOF buffers. The metadata input is passed by value despite a mutable-reference help declaration.

| Parameter | Meaning |
| --- | --- |
| `nodeId` | Positive native analytical node ID. It is not a support-definition ID or list index. |

~~~csharp
var result = support.GetSupportType(11);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.NativeTypeCode);
~~~

### GetSupportInformation

~~~csharp
SupportQueryResult<SupportInformation> GetSupportInformation(int nodeId);
~~~

Native: `GetSupportInformation`.

When to use: Unlike GetSupportType, this retrieves all FX/FY/FZ/MX/MY/MZ restraint observations. The native return is a support-type code, never a Boolean success flag. Type 0 yields NoSupport without consuming untouched buffers; -1 yields NativeFailure; an unknown positive type yields UnsupportedNativeType without guessing its layout. Known positive types require six written Int32 states (-1 spring, 0 fixed, 1 released) and six finite nonnegative floating-point stiffnesses. Springs are not collapsed to released=true. Native local support axes, force/length translational stiffness and moment/angle rotational stiffness are retained without conversion.

| Parameter | Meaning |
| --- | --- |
| `nodeId` | Positive native analytical node ID. It is not a support-definition ID or list index. |

~~~csharp
var result = support.GetSupportInformation(11);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Fx.State);
~~~

### GetSupportUniqueId

~~~csharp
SupportQueryResult<SupportUniqueId> GetSupportUniqueId(int supportId);
~~~

Native: `GetSupportUniqueID`.

When to use: This takes a support item/definition ID, not a node number, support-list index or user-facing name. Null, DBNull or empty native text gives EmptyOrUnavailable; malformed GUID text throws. Exact native text and parsed Guid are both retained. Guid.Empty, if returned, remains an observed value, not a generated replacement. SetSupportUniqueId writes a new identity and has only void invocation evidence.

| Parameter | Meaning |
| --- | --- |
| `supportId` | Known positive native support item/definition reference, not a node number or zero-based index. Creation returns a reference but does not assign it. |

~~~csharp
var result = support.GetSupportUniqueId(4);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Value);
~~~

### GetElasticMatDefinitionCount

~~~csharp
SupportQueryResult<int> GetElasticMatDefinitionCount();
~~~

Native: `GetCountOfElasticMat`.

When to use: This is a definition count, not supported-node count, assignment count or maximum support ID. Use GetElasticMat with a known positive definition ID; there is no inferred 1..count discovery. Zero is valid integral data. Plate-mat and footing definition counts are separate queries.

~~~csharp
var result = support.GetElasticMatDefinitionCount();
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
~~~

### GetElasticMat

~~~csharp
SupportQueryResult<ElasticMatObservation> GetElasticMat(int supportId);
~~~

Native: `GetElasticMatDetail`.

When to use: Use a known support definition ID, not a node ID or zero-based index. Native true qualifies direction 0..5, scalar subgrade, print flag, spring-generation type and nonnegative assignment count; false exposes no default packet. This query does not read assigned nodes; GetElasticMatAssignedNodeIds performs a bounded detail/list/detail observation. Native subgrade is force/length cubed, not force/length nodal stiffness. Only direction variants and compression/multilinear generation remain distinct. No model or geometry objects are hydrated.

| Parameter | Meaning |
| --- | --- |
| `supportId` | Known positive native support item/definition reference, not a node number or zero-based index. Creation returns a reference but does not assign it. |

~~~csharp
var result = support.GetElasticMat(4);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.SubgradeModulus);
~~~

### GetElasticMatAssignedNodeIds

~~~csharp
SupportQueryResult<IReadOnlyList<int>> GetElasticMatAssignedNodeIds(int supportId, int maximumResults = 100000);
~~~

Native: `GetElasticMatAssignmentList`.

When to use: The returned IDs are analytical nodes, never plate IDs or support-definition IDs. Allocation uses qualified GetElasticMat detail, then Boolean list retrieval and a complete detail recheck. Exceeding maximumResults gives LimitExceeded before list allocation; zero assignments return an owned empty list after recheck. Changed count/direction/subgrade/flags gives StateChanged without payload or retry. Native order/duplicates are preserved; equal metadata does not detect every same-count edit.

| Parameter | Meaning |
| --- | --- |
| `supportId` | Known positive native support item/definition reference, not a node number or zero-based index. Creation returns a reference but does not assign it. |
| `maximumResults` | Managed selection/allocation limit from 1 through 100000. Counts beyond the read limit produce LimitExceeded before allocation; oversized input selections are rejected before native invocation. |

~~~csharp
var result = support.GetElasticMatAssignedNodeIds(4, 1000);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
~~~

### GetPlateMatDefinitionCount

~~~csharp
SupportQueryResult<int> GetPlateMatDefinitionCount();
~~~

Native: `GetCountOfPlateMat`.

When to use: This count concerns plate-mat definitions, not plates, support nodes, assigned entities or maximum support reference. Zero is valid integral data. It does not resolve plate-mat ID/index or scalar/array contract gates; no plate assignments or subgrades are synthesized from this count.

~~~csharp
var result = support.GetPlateMatDefinitionCount();
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
~~~

### GetElasticFootingDefinitionCount

~~~csharp
SupportQueryResult<int> GetElasticFootingDefinitionCount();
~~~

Native: `GetCountOfElasticFooting`.

When to use: This is independent of the supported-node and elastic-mat counts. No documented footing index-to-ID enumeration is assumed; supply known support IDs to GetElasticFooting. Zero is valid, and a count is never decoded as a success flag.

~~~csharp
var result = support.GetElasticFootingDefinitionCount();
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
~~~

### GetElasticFooting

~~~csharp
SupportQueryResult<ElasticFootingObservation> GetElasticFooting(int supportId);
~~~

Native: `GetElasticFootingDetail`.

When to use: Unlike GetElasticMat, the native footing packet includes finite positive length and width as well as native direction 0..5, scalar subgrade and assigned-node count. Known support definition ID is required. Native 1/true succeeds, 0/false fails; integral -1 is not VARIANT_TRUE for this narrower contract. Lengths remain native length units and subgrade remains force/length cubed; no area, spring stiffness or reaction is derived.

| Parameter | Meaning |
| --- | --- |
| `supportId` | Known positive native support item/definition reference, not a node number or zero-based index. Creation returns a reference but does not assign it. |

~~~csharp
var result = support.GetElasticFooting(4);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Length);
~~~

### GetElasticFootingAssignedNodeIds

~~~csharp
SupportQueryResult<IReadOnlyList<int>> GetElasticFootingAssignedNodeIds(int supportId, int maximumResults = 100000);
~~~

Native: `GetElasticFootingAssignmentList`.

When to use: Uses qualified footing detail/list/detail, retaining node identity and every observed length/width/direction/modulus/count in its state check. Native 1/true is success; 0/false is failure and numeric -1 is rejected rather than treated as true. Empty assignment is a rechecked owned empty list. LimitExceeded occurs before list allocation. StateChanged has no payload; same-count edits can remain undetected, so coordinate session access. No node geometry is hydrated or reordered.

| Parameter | Meaning |
| --- | --- |
| `supportId` | Known positive native support item/definition reference, not a node number or zero-based index. Creation returns a reference but does not assign it. |
| `maximumResults` | Managed selection/allocation limit from 1 through 100000. Counts beyond the read limit produce LimitExceeded before allocation; oversized input selections are rejected before native invocation. |

~~~csharp
var result = support.GetElasticFootingAssignedNodeIds(4, 1000);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
~~~

### CreateFixedSupport

~~~csharp
SupportMutationResult CreateFixedSupport();
~~~

Native: `CreateSupportFixed`.

When to use: This creates a support definition and returns its qualified positive reference; it does not attach it to nodes. AssignSupportToNodes is a separate mutation. Native -1 is failure; Boolean success or native zero is not accepted as a created reference. No mutable managed support ID is overwritten. Read back assignments separately before drawing restraint/engineering conclusions.

~~~csharp
bool allowNativeMutation = false;
if (allowNativeMutation)
{
    var result = support.CreateFixedSupport();
    System.Console.WriteLine(result.Status);
}
~~~

### CreatePinnedSupport

~~~csharp
SupportMutationResult CreatePinnedSupport();
~~~

Native: `CreateSupportPinned`.

When to use: The native pinned operation is separate from synthesizing fixed-but release flags. Help describes local Y/Z rotations while the private source convenience releases all rotations; this method deliberately returns only the created ID and does not claim torsional restraint. Assign it separately, then use GetSupportInformation at a node to observe actual states. Native -1 is failure; success requires a positive integral reference.

~~~csharp
bool allowNativeMutation = false;
if (allowNativeMutation)
{
    var result = support.CreatePinnedSupport();
    System.Console.WriteLine(result.Status);
}
~~~

### CreateFixedButSupport

~~~csharp
SupportMutationResult CreateFixedButSupport(FixedButSupportDefinition definition);
~~~

Native: `CreateSupportFixedBut`.

When to use: Use when creation flags and springs must be specified explicitly rather than using the dedicated fixed/pinned calls. FixedButSupportDefinition snapshots the existing Releases/Springs packets. Native input flags are six Double 0/1 values, not the read-back Spring=-1 state or member partial-release modes. Native arrays are fresh copies; the caller’s packets are not changed. Stiffness units and release/spring interaction remain native semantics without scaling or inferred modes. Native -1 fails; a positive integral support ID is required.

| Parameter | Meaning |
| --- | --- |
| `definition` | Non-null immutable snapshot of the existing managed Releases and Springs packets, with explicit finite nonnegative stiffnesses. |

~~~csharp
bool allowNativeMutation = false;
if (allowNativeMutation)
{
    var definition = new FixedButSupportDefinition(new StaadPro.Interop.Entities.Releases { My = true, Mz = true }, new StaadPro.Interop.Entities.Springs());
    var result = support.CreateFixedButSupport(definition);
    System.Console.WriteLine(result.Status);
}
~~~

### AssignSupportToNodes

~~~csharp
SupportMutationResult AssignSupportToNodes(int supportId, IEnumerable<int> nodeIds, int maximumResults = 100000);
~~~

Native: `AssignSupportToNode`.

When to use: Creation and assignment are separate; this function sends one Int32 node-ID array to native AssignSupportToNode in node-array-first/support-ID-second order. The support ID must be appropriate for node assignment; this is not the plate-mat entity-list route. Input is enumerated once and bounded; order and duplicates remain unchanged. Integral 0 is success and -1 is failure; actual Boolean false is not numeric zero-success. Empty input produces NoOperation after binding checks and no native invocation. No per-node acknowledgement is invented; partial native effects on failure are possible.

| Parameter | Meaning |
| --- | --- |
| `supportId` | Known positive native support item/definition reference, not a node number or zero-based index. Creation returns a reference but does not assign it. |
| `nodeIds` | Non-null one-pass selection of positive analytical node IDs. Order and duplicates are preserved; empty input causes no mutation. |
| `maximumResults` | Managed selection/allocation limit from 1 through 100000. Counts beyond the read limit produce LimitExceeded before allocation; oversized input selections are rejected before native invocation. |

~~~csharp
bool allowNativeMutation = false;
if (allowNativeMutation)
{
    var result = support.AssignSupportToNodes(4, new[] { 11, 12 }, 1000);
    System.Console.WriteLine(result.Status);
}
~~~

### RemoveSupportFromNode

~~~csharp
SupportMutationResult RemoveSupportFromNode(int nodeId);
~~~

Native: `RemoveSupportFromNode`.

When to use: This unassigns one node; DeleteSupport removes a shared definition and its assignments. Native 1/true is success, 0/false is failure. Failure does not prove that no support existed. No shared definition deletion, family-specific fallback, retry, save or managed node update is performed.

| Parameter | Meaning |
| --- | --- |
| `nodeId` | Positive native analytical node ID. It is not a support-definition ID or list index. |

~~~csharp
bool allowNativeMutation = false;
if (allowNativeMutation)
{
    var result = support.RemoveSupportFromNode(11);
    System.Console.WriteLine(result.Status);
}
~~~

### DeleteSupport

~~~csharp
SupportMutationResult DeleteSupport(int supportId);
~~~

Native: `DeleteSupport`.

When to use: This is broader than RemoveSupportFromNode, which targets only one assignment. The documented operation removes the support item and its assignments; the request retains the support ID without pretending to know every affected node. Integral 0 is success and -1 is failure; Boolean false is not decoded as numeric zero. Native partial changes may remain on failure and cannot be rolled back by this result.

| Parameter | Meaning |
| --- | --- |
| `supportId` | Known positive native support item/definition reference, not a node number or zero-based index. Creation returns a reference but does not assign it. |

~~~csharp
bool allowNativeMutation = false;
if (allowNativeMutation)
{
    var result = support.DeleteSupport(4);
    System.Console.WriteLine(result.Status);
}
~~~

### SetSupportUniqueId

~~~csharp
SupportMutationResult SetSupportUniqueId(int supportId, Guid uniqueId);
~~~

Native: `SetSupportUniqueID`.

When to use: This writes a nonempty explicitly supplied Guid to a support definition/item, not a node. GetSupportUniqueId reads its actual current value. The native setter returns void, so completed dispatch produces InvokedWithoutAcknowledgement and IsSuccessful=false, with the requested ID/GUID retained and no fabricated native flag. No automatic read-back or persistence claim occurs; callers may request a separate read-back if needed. The Guid is marshaled as canonical D-format text.

| Parameter | Meaning |
| --- | --- |
| `supportId` | Known positive native support item/definition reference, not a node number or zero-based index. Creation returns a reference but does not assign it. |
| `uniqueId` | Explicit nonempty GUID to send as canonical D-format text. No GUID is generated implicitly. |

~~~csharp
bool allowNativeMutation = false;
if (allowNativeMutation)
{
    var result = support.SetSupportUniqueId(4, System.Guid.Parse("dfe2cb8e-1b42-487f-a02c-953ac006d64a"));
    System.Console.WriteLine(result.Status);
}
~~~

## Reuse and source adaptations

Ported the main project's SupportType, FoundationSupportDirection and FoundationSpringType codes without its private UI type-converter dependencies. Reused existing Releases and Springs as fixed-but input vocabulary, strict native integer/float/array readers and normal wrapper binding. FixedButSupportDefinition takes an immutable snapshot and exposes independent copies through the existing packet classes; null stiffness is not silently converted to zero.

The private support/foundation entities depend on mutable identity, Node/Plate hydration, private factories, attributes and unit services. Their useful field meanings are retained in immutable SupportInformation, ElasticMatObservation and ElasticFootingObservation snapshots. This increment does not import the entire private graph, source serializers, frame-extraction conveniences or parallel COM utilities.

Corrected the source patterns that ignore assignment status, overwrite a managed ID before validating creation, treat type codes as truthy flags or initialize unwritten buffers with plausible zeroes. Support read-back preserves Spring=-1 separately from Fixed=0 and Released=1. The native pinned creation does not claim the source convenience's all-rotation restraint assumptions. No enforced-creation no-op is exposed.

## Native conventions and ownership

| Operation | Qualified interpretation |
| --- | --- |
| Four count queries | Exact nonnegative integral data; zero is a valid empty count. |
| GetSupportNodes | Returned integral count, -1 failure; count/list/count must agree. |
| GetSupportType | -1 failure; 0 successful NoSupport type observation; unknown positive codes remain raw data. |
| GetSupportInformation | -1 failure; 0 NoSupport without data; unknown positive code UnsupportedNativeType; reviewed positive types require written DOF/stiffness arrays. |
| Elastic mat detail/list | Boolean true/false and integral 0/1/-1 VARIANT_BOOL representations, with strict written outputs. |
| Elastic footing detail/list | Narrow 1/0 success/failure, also actual Boolean; numeric -1 is not true. |
| Create fixed/pinned/fixed-but | Positive integral created support ID; -1 failure. Zero/Boolean is not treated as a reference. |
| AssignSupportToNodes / DeleteSupport | Integral 0 success, -1 failure; Boolean false is not zero-success. |
| RemoveSupportFromNode | 1/true success; 0/false failure without an absence inference. |
| SetSupportUniqueId | Void completion only; no acknowledgement or automatic read-back. |

List allocations are bounded by maximumResults (1..100000). Input IDs are enumerated once and owned; order and duplicates are preserved. Empty assignment is NoOperation with no native invocation. Empty readback is rechecked before returning an owned empty list. Arrays must have exact rank/type/length and written valid identifiers or finite values; nonzero native lower bounds are supported.

Foundation assignment lists use qualified detail/list/detail and compare the full known detail packet. Changed count, direction, subgrade, print/spring flags or footing dimensions returns StateChanged without data. This is not an atomic model transaction: equal metadata cannot detect all same-count edits. No retry, geometry hydration or automatic family fallback occurs.

DOF stiffnesses preserve native support-local axes; translational units are force/length and rotational units are moment/angle. Foundation subgrade modulus is force/length cubed, distinct from nodal spring stiffness. No conversion, positivity-based restraint synthesis, foundation area calculation or reaction derivation occurs.

## Binding and mutation boundary

Reviewed declarations and non-example contracts match across STAAD.Pro 2024–2026; a shared internal service supplies these common routes. Calls run synchronously on the caller’s established COM apartment under the Support gate. Captured binding identity is checked before and after work; reacquire wrapper.Support after refresh. Other adapters, raw access and external edits are not excluded by this gate. Native units and signs are retained without conversion. No automatic analysis, save, rollback or retry occurs; partial native effects can precede a mutation failure or post-call lifetime exception. Managed tests do not certify live marshaling, result freshness or engineering behavior.

Creating a definition does not assign it. Deleting a definition can remove shared assignments; removing a node assignment does not delete the definition. The void GUID setter returns the exact requested support/GUID as request evidence only. A post-call lifetime exception can occur after a mutation has taken effect. No rollback, persistence check or implicit save is promised.

## Remaining routes

The sixteen documented routes listed in the [Support matrix](reports/OpenSTAAD-Support-Multi-Version-Architecture-and-Plan.md#3-complete-cross-version-function-matrix) remain pending. Some are later straightforward family operations; others retain explicit identity, layout or transport gates. In particular, GetSupportName input identity, GetSupportInformationEx reference semantics, plate-mat scalar/array and ID/axis/assignment semantics, and the native RemovePlateMatFromPlate arity mismatch must be resolved separately. No remaining function is presented as implemented merely because a binding exists.

The parked Property supplemental candidates and Load work retain their separate review/approval boundaries. The main source project was read-only; the existing prepared release archive remains unchanged. Managed tests and installed-documentation examples do not certify live 2024–2026 COM or engineering behavior.

See [the contract index](contracts/SupportFoundationContractIndex.json), [Support implementation status](Support-Porting.md) and [native contract inventory](reports/OpenSTAAD-Support-API-Coverage-and-Contract-Inventory.md).
