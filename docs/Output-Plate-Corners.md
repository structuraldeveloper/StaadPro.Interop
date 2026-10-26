# Output plate-corner extraction and transformations

**Updated:** 26 October 2026

Added **25 public Output overloads**: **18 pending synchronous main-project adaptations and seven typed enabling overloads**. IOSOutput and the single non-partial OSOutputAdapter now expose **96 managed overloads reaching 47 of 60 documented native Output names**. Thirteen documented native routes remain pending. All 25 additions share the one newly enabled native target, GetPlateCornerForces; managed projections and selections are not additional native names.

## Contract evidence and coordinate decision

The installed 2024/2025/2026 VBA examples allocate **six Double values** for GetPlateCornerForces. The parameter table leaves the output description blank. The main adapter explicitly treats the packet as global Fx/Fy/Fz/Mx/My/Mz, while an older main-model comment calls it plate-local. The port follows the adapter and independently checked the interpretation against a running **STAAD.Pro 2026.0.0.340** model.

Read-only observations at **four nodes over two case selections** produced eight samples. Directly summed native plate-corner forces had relative residuals between about **8.05e-9 and 1.13e-8**; rotating those vectors as though they were plate-local produced residuals between **0.0259 and 0.382**. The corresponding moment residuals were about **6.65e-9 to 1.13e-8** unrotated, versus materially larger residuals after the assumed plate-basis rotation. The nodes had no connected beams in these samples. This supports the global packet interpretation on the sampled build; it is not a complete solver, sign or cross-version certification.

A separate read-only consumer compared the new public supplied-metadata and explicit-family hydration routes with the direct native packet. Both matched. Automatic metadata discovery correctly returned MetadataUnavailable for a case number present in multiple families. No STAAD file was opened, activated, analyzed, saved or modified. The main project remained strictly read-only.

The [reproducible diagnostic](../eng/Diagnostics/Probe-PlateCornerCoordinates.ps1) attaches to the running object and performs bounded incidence/equilibrium sampling. It prints local identifiers for correlation; do not publish project-specific diagnostic logs without reviewing them. The native method shape and main-source fingerprints are retained in the [coverage inventory](reports/OpenSTAAD-Output-API-Coverage-and-Contract-Inventory.md). 2024/2025 native behavior and other model contexts still need qualification.

## Selecting the right API

| Need | API | Behavior |
| --- | --- | --- |
| One known case/plate/corner | GetPlateCornerForces(ILoadCase, Plate, Node) | Captures supplied case metadata and canonical incidence coordinates. |
| Numeric identifiers | Numeric GetPlateCornerForces overloads | Hydrates checked geometry and resolves metadata without activating a case. |
| One sparse request | GetPlateCornerForces(PlateCornerForcesRequest) | Returns one named status/data observation. |
| Many sparse requests | Enumerable request overload | Coalesces exact duplicates, preserves first-seen order and indexes failures too. |
| Every corner | Case/plate selection overloads | Requires supplied A/B/C incidence; deduplicates triangular/repeated closing corners. |
| Exact identity keys | Enumerable key overloads | Resolves keys; explicit families avoid ambiguous numeric metadata. |
| Explicit global wording | GetPlateCornerForcesInGlobalCoordinates / Global | Returns the original global packet; no extra plate rotation. |
| Real member-local components | Beam-based overloads | Uses supplied member incidence/beta and retains the matched real end. |
| Conceptual local direction | Other-node overloads | Uses corner-to-node X and zero beta, with no fabricated member ID. |
| Transform existing observations | TransformPlateCornerForcesToMemberLocal | Pure managed math; no result re-read or implicit moment transfer. |

These are **nodal forces and moments**, not the plate-center stress families or moment-per-unit-width quantities. Source compatibility helpers marked obsolete, thread-pool async conveniences, point/cut queries and other unresolved native routes remain separate work.

## Status and metadata identity

Inspect **IsSuccessful before Data**. Native false returns NativeFailure with no data, even if the server wrote buffer values. Success requires exactly one rank-one six-Double array with all values written and finite; nonzero SAFEARRAY lower bounds are supported. No initialized zeros escape as extracted forces. Original transport exceptions propagate without retries.

OutputQueryResult retains plate EntityId, CornerNodeId, LoadCaseId and nullable LoadCaseType, including failures. NativeFunction identifies the associated target, or the actual failed geometry query where applicable; native codes/Booleans are recorded only when observed. Pure transformations have ManagedOperation and source identity, with no fabricated native acknowledgement.

PlateCornerForcesKey is a small immutable value key with case/plate/corner IDs and nullable family. A three-ID key requests automatic family resolution; an explicit-family key avoids numeric collisions. Successful packets have resolved family metadata. Unresolved automatic observations keep a null family rather than silently becoming primary cases.

Automatic numeric discovery searches primary, combination and reference metadata. Missing or multiple family matches return MetadataUnavailable before native Output extraction. Explicit-family overloads restrict metadata discovery to that family. Repeat metadata is rejected. Crucially, **the native Output call takes only a numeric case number**: selecting a managed metadata family does not activate a case or create an additional native family selector. Consumers must select a valid analyzed result context.

The indexed set owns ordered outcomes, keys and ID lists. The key indexer and Find support exact-family lookup; a family-free lookup succeeds only when unambiguous. An explicitly requested unresolved observation can itself be found by its automatic key. Find returns null for a missing request; failed observations remain real indexed entries, not missing results. Family-free lookup across multiple matching families throws. Filters retain ordinary failure observations and source order.

## Geometry, ownership and bounds

Single/sparse queries can hydrate an ID-only plate. Incidence accepts documented integer 0 success, -1 general failure and -4001 not-found. Boolean or unknown status representations are rejected. A/B/C must be written positive distinct IDs; a zero or repeated fourth node represents a triangle/closing corner. Void node-coordinate calls must write finite outputs; no invented return flag is attached.

All-corner expansion requires caller-populated A/B/C coordinates. The supplied corner node's positive ID selects the canonical plate vertex; unrelated selector coordinates are not substituted. Snapshots own IDs, case metadata and points, so later caller edits cannot corrupt result indexes. Conflicting geometry for one plate ID, or conflicting metadata for one family/case ID, is rejected before extraction. Caller geometry can still be stale relative to the native model.

Selections are enumerated once and limited to **100000** entries and expanded requests. Native metadata discovery is bounded before the reused Load adapter materializes large case arrays. Exact duplicates are coalesced in first-seen order. Pure collection transforms preserve duplicate observations, because repeated measurements can be meaningful. Ordinary native failures do not stop later requests; transport, malformed successful data or binding exceptions abort the operation. Empty selections perform no native extraction/discovery while still checking adapter lifetime.

## Transformations and signs

PlateCornerForces always owns a **global** packet and reuses existing ForceComponents. ToForces creates an independent copy for the existing mutable force algebra.

Member-local workflows return a distinct PlateCornerMemberForces containing the original packet, basis axes, beta, reference point and optional real member/end identity. This prevents a transformed local packet from being mislabeled as an original global observation or passed into the transform again accidentally.

For a supplied Beam, positive member ID and distinct finite end nodes are required. The corner must match a member end ID and point within the documented managed **1e-9 native length-unit** tolerance. X follows the member's original start-to-end incidence even at its End; no end-action sign reversal occurs. A supplied other Node instead defines a conceptual direction from the corner and zero beta; MemberId and MemberEnd remain null.

The shared internal basis uses projected global Y, or projected global Z near vertical, then rotates Y/Z about positive X by supplied beta degrees. This reuses the existing managed coordinate convention; it does not hydrate beta or certify native orientation/up-axis rules. Both force and moment vectors are rotated at the same corner point. **No force aggregation, opposite-action conversion, offset/reference-point transfer or native member-result extraction occurs.** Use the separate offset-aware member-force APIs when moment transfer is required.

## Execution, versions and architecture

Calls remain synchronous on the caller's established **COM apartment**. No Task.Run, PLINQ, case activation, analysis or save is introduced. Shared per-Output locking and participating binding checks reject disposal/refresh/replacement, but cannot exclude raw calls, other adapters or external model edits. Sequential reads do not establish an atomic or current analysis snapshot.

One internal OutputPlateCornerService implements snapshotting, hydration, discovery, dispatch and composition. MemberResultFrame contains shared pure basis math, reused by the existing GetMemberLocalCoordinateSystem. Existing wrapper, providers and domain adapter structure are unchanged. Load APIs are reused without changing their parked implementations. Identical documented transport uses one common implementation; no speculative per-year classes are introduced.

## Exact public contracts

### 1. GetPlateCornerForces

~~~csharp
OutputQueryResult<PlateCornerForces> GetPlateCornerForces(ILoadCase loadCase, Plate plate, Node cornerNode)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself.

Adapted source signature: `PlateCornerForces GetPlateCornerForces(ILoadCase loadCase, Plate plate, Node cornerNode)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var cornerNode = new Node(1, 0, 0, 0);
var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
var result = wrapper.Output.GetPlateCornerForces(loadCase, plate, cornerNode);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
else System.Console.WriteLine(result.Status);
~~~

### 2. GetPlateCornerForces

~~~csharp
OutputQueryResult<PlateCornerForces> GetPlateCornerForces(int loadCaseId, int plateId, int cornerNodeId)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself.  Resolves case metadata and plate incidence through this wrapper's current bindings before extraction. Automatic case resolution searches primary, combination and reference metadata and returns MetadataUnavailable for a missing or ambiguous number. Explicit-family overloads avoid this ambiguity; they do not add a native family selector. No case activation or model loading occurs.

Adapted source signature: `PlateCornerForces GetPlateCornerForces(int loadCaseId, int plateId, int cornerNodeId)`.

~~~csharp
var result = wrapper.Output.GetPlateCornerForces(1, 17, 1);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
else System.Console.WriteLine(result.Status);
~~~

### 3. GetPlateCornerForces

~~~csharp
OutputQueryResult<PlateCornerForces> GetPlateCornerForces(int loadCaseId, int plateId, int cornerNodeId, IOSGeometry geometry, IOSLoad load)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself.  Resolves case metadata and plate incidence through this wrapper's current bindings before extraction. Automatic case resolution searches primary, combination and reference metadata and returns MetadataUnavailable for a missing or ambiguous number. Explicit-family overloads avoid this ambiguity; they do not add a native family selector. No case activation or model loading occurs.

Adapted source signature: `PlateCornerForces GetPlateCornerForces(int loadCaseId, int plateId, int cornerNodeId, IOSGeometry geometry, IOSLoad load)`.

~~~csharp
var result = wrapper.Output.GetPlateCornerForces(1, 17, 1, wrapper.Geometry, wrapper.Load);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
else System.Console.WriteLine(result.Status);
~~~

### 4. GetPlateCornerForces

~~~csharp
OutputQueryResult<PlateCornerForces> GetPlateCornerForces(int loadCaseId, int plateId, int cornerNodeId, LoadCaseType loadCaseType, IOSGeometry geometry, IOSLoad load)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself.  Resolves case metadata and plate incidence through this wrapper's current bindings before extraction. Automatic case resolution searches primary, combination and reference metadata and returns MetadataUnavailable for a missing or ambiguous number. Explicit-family overloads avoid this ambiguity; they do not add a native family selector. No case activation or model loading occurs.

Adapted source signature: `PlateCornerForces GetPlateCornerForces(int loadCaseId, int plateId, int cornerNodeId, LoadCaseType loadCaseType, IOSGeometry geometry, IOSLoad load)`.

~~~csharp
var result = wrapper.Output.GetPlateCornerForces(1, 17, 1, LoadCaseType.PrimaryLoad, wrapper.Geometry, wrapper.Load);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
else System.Console.WriteLine(result.Status);
~~~

### 5. GetPlateCornerForces

~~~csharp
PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<ILoadCase> loadCases, Plate plate, Node cornerNode)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch.

Adapted source signature: `PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<ILoadCase> loadCases, Plate plate, Node cornerNode)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var cornerNode = new Node(1, 0, 0, 0);
var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
var result = wrapper.Output.GetPlateCornerForces(new[] { loadCase }, plate, cornerNode);
foreach (var item in result.Items)
    if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
    else System.Console.WriteLine(item.Status);
~~~

### 6. GetPlateCornerForces

~~~csharp
PlateCornerForcesResultSet GetPlateCornerForces(ILoadCase loadCase, Plate plate)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch.

Adapted source signature: `PlateCornerForcesResultSet GetPlateCornerForces(ILoadCase loadCase, Plate plate)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var cornerNode = new Node(1, 0, 0, 0);
var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
var result = wrapper.Output.GetPlateCornerForces(loadCase, plate);
foreach (var item in result.Items)
    if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
    else System.Console.WriteLine(item.Status);
~~~

### 7. GetPlateCornerForces

~~~csharp
PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<ILoadCase> loadCases, Plate plate)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch.

Adapted source signature: `PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<ILoadCase> loadCases, Plate plate)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var cornerNode = new Node(1, 0, 0, 0);
var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
var result = wrapper.Output.GetPlateCornerForces(new[] { loadCase }, plate);
foreach (var item in result.Items)
    if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
    else System.Console.WriteLine(item.Status);
~~~

### 8. GetPlateCornerForces

~~~csharp
PlateCornerForcesResultSet GetPlateCornerForces(ILoadCase loadCase, IEnumerable<Plate> plates)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch.

Adapted source signature: `PlateCornerForcesResultSet GetPlateCornerForces(ILoadCase loadCase, IEnumerable<Plate> plates)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var cornerNode = new Node(1, 0, 0, 0);
var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
var result = wrapper.Output.GetPlateCornerForces(loadCase, new[] { plate });
foreach (var item in result.Items)
    if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
    else System.Console.WriteLine(item.Status);
~~~

### 9. GetPlateCornerForces

~~~csharp
PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<ILoadCase> loadCases, IEnumerable<Plate> plates)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch.

Adapted source signature: `PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<ILoadCase> loadCases, IEnumerable<Plate> plates)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var cornerNode = new Node(1, 0, 0, 0);
var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
var result = wrapper.Output.GetPlateCornerForces(new[] { loadCase }, new[] { plate });
foreach (var item in result.Items)
    if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
    else System.Console.WriteLine(item.Status);
~~~

### 10. GetPlateCornerForces

~~~csharp
PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<PlateCornerForcesRequest> requests)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch.

Adapted source signature: `PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<PlateCornerForcesRequest> requests)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var cornerNode = new Node(1, 0, 0, 0);
var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
var result = wrapper.Output.GetPlateCornerForces(new[] { new PlateCornerForcesRequest(loadCase, plate, cornerNode) });
foreach (var item in result.Items)
    if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
    else System.Console.WriteLine(item.Status);
~~~

### 11. GetPlateCornerForces

~~~csharp
PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<int> loadCaseIds, int plateId, int cornerNodeId, IOSGeometry geometry, IOSLoad load)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch. Resolves case metadata and plate incidence through this wrapper's current bindings before extraction. Automatic case resolution searches primary, combination and reference metadata and returns MetadataUnavailable for a missing or ambiguous number. Explicit-family overloads avoid this ambiguity; they do not add a native family selector. No case activation or model loading occurs.

Adapted source signature: `PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<int> loadCaseIds, int plateId, int cornerNodeId, IOSGeometry geometry, IOSLoad load)`.

~~~csharp
var result = wrapper.Output.GetPlateCornerForces(new[] { 1 }, 17, 1, wrapper.Geometry, wrapper.Load);
foreach (var item in result.Items)
    if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
    else System.Console.WriteLine(item.Status);
~~~

### 12. GetPlateCornerForcesInGlobalCoordinates

~~~csharp
OutputQueryResult<PlateCornerForces> GetPlateCornerForcesInGlobalCoordinates(ILoadCase loadCase, Plate plate, Node cornerNode)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself.

Adapted source signature: `PlateCornerForces GetPlateCornerForcesInGlobalCoordinates(ILoadCase loadCase, Plate plate, Node cornerNode)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var cornerNode = new Node(1, 0, 0, 0);
var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
var result = wrapper.Output.GetPlateCornerForcesInGlobalCoordinates(loadCase, plate, cornerNode);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
else System.Console.WriteLine(result.Status);
~~~

### 13. GetPlateCornerForcesGlobal

~~~csharp
OutputQueryResult<PlateCornerForces> GetPlateCornerForcesGlobal(Plate plate, Node cornerNode, int loadCaseId)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself.   Replaces the source Boolean/out-vector convenience with a named status/data result; failures do not become zero vectors.

Adapted source signature: `bool GetPlateCornerForcesGlobal(Plate plate, Node cornerNode, int loadCaseId, out Vector3D globalForce, out Vector3D globalMoment)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var cornerNode = new Node(1, 0, 0, 0);
var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
var result = wrapper.Output.GetPlateCornerForcesGlobal(plate, cornerNode, 1);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
else System.Console.WriteLine(result.Status);
~~~

### 14. GetPlateCornerForces

~~~csharp
OutputQueryResult<PlateCornerMemberForces> GetPlateCornerForces(ILoadCase loadCase, Plate plate, Node cornerNode, Node otherMemberNode)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Projects global corner forces and moments into a managed member basis at the same corner point. It does not reverse member-end signs, transfer moments, sum adjacent plates or return native analytical-member result forces. Beam overloads use supplied beta degrees and require the corner to match a supplied member end ID and point; node-direction overloads use zero beta and no fabricated native member ID. Projected global Y defines the reference except near vertical, where projected global Z is used. This convention is not native orientation read-back.

Adapted source signature: `MemberForces GetPlateCornerForces(ILoadCase loadCase, Plate plate, Node cornerNode, Node otherMemberNode)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var cornerNode = new Node(1, 0, 0, 0);
var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
var otherMemberNode = plate.B;
var result = wrapper.Output.GetPlateCornerForces(loadCase, plate, cornerNode, otherMemberNode);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
else System.Console.WriteLine(result.Status);
~~~

### 15. GetPlateCornerForces

~~~csharp
OutputQueryResult<PlateCornerMemberForces> GetPlateCornerForces(ILoadCase loadCase, Plate plate, Node cornerNode, Beam member)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Projects global corner forces and moments into a managed member basis at the same corner point. It does not reverse member-end signs, transfer moments, sum adjacent plates or return native analytical-member result forces. Beam overloads use supplied beta degrees and require the corner to match a supplied member end ID and point; node-direction overloads use zero beta and no fabricated native member ID. Projected global Y defines the reference except near vertical, where projected global Z is used. This convention is not native orientation read-back.

Adapted source signature: `MemberForces GetPlateCornerForces(ILoadCase loadCase, Plate plate, Node cornerNode, Beam member)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var cornerNode = new Node(1, 0, 0, 0);
var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
var member = new Beam(18, cornerNode, plate.B) { BetaAngle = 30 };
var otherMemberNode = plate.B;
var result = wrapper.Output.GetPlateCornerForces(loadCase, plate, cornerNode, member);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
else System.Console.WriteLine(result.Status);
~~~

### 16. TransformPlateCornerForcesToMemberLocal

~~~csharp
OutputQueryResult<PlateCornerMemberForces> TransformPlateCornerForcesToMemberLocal(PlateCornerForces plateCornerForces, Node otherMemberNode)
~~~

Projects global corner forces and moments into a managed member basis at the same corner point. It does not reverse member-end signs, transfer moments, sum adjacent plates or return native analytical-member result forces. Beam overloads use supplied beta degrees and require the corner to match a supplied member end ID and point; node-direction overloads use zero beta and no fabricated native member ID. Projected global Y defines the reference except near vertical, where projected global Z is used. This convention is not native orientation read-back. This overload is pure managed math over an existing packet and performs no native result query.

Adapted source signature: `MemberForces TransformPlateCornerForcesToMemberLocal(PlateCornerForces plateCornerForces, Node otherMemberNode)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var cornerNode = new Node(1, 0, 0, 0);
var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
var member = new Beam(18, cornerNode, plate.B) { BetaAngle = 30 };
var otherMemberNode = plate.B;
var source = wrapper.Output.GetPlateCornerForces(loadCase, plate, cornerNode);
if (!source.IsSuccessful) return;
var result = wrapper.Output.TransformPlateCornerForcesToMemberLocal(source.Data, otherMemberNode);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
else System.Console.WriteLine(result.Status);
~~~

### 17. TransformPlateCornerForcesToMemberLocal

~~~csharp
OutputQueryResult<PlateCornerMemberForces> TransformPlateCornerForcesToMemberLocal(PlateCornerForces plateCornerForces, Beam member)
~~~

Projects global corner forces and moments into a managed member basis at the same corner point. It does not reverse member-end signs, transfer moments, sum adjacent plates or return native analytical-member result forces. Beam overloads use supplied beta degrees and require the corner to match a supplied member end ID and point; node-direction overloads use zero beta and no fabricated native member ID. Projected global Y defines the reference except near vertical, where projected global Z is used. This convention is not native orientation read-back. This overload is pure managed math over an existing packet and performs no native result query.

Adapted source signature: `MemberForces TransformPlateCornerForcesToMemberLocal(PlateCornerForces plateCornerForces, Beam member)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var cornerNode = new Node(1, 0, 0, 0);
var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
var member = new Beam(18, cornerNode, plate.B) { BetaAngle = 30 };
var otherMemberNode = plate.B;
var source = wrapper.Output.GetPlateCornerForces(loadCase, plate, cornerNode);
if (!source.IsSuccessful) return;
var result = wrapper.Output.TransformPlateCornerForcesToMemberLocal(source.Data, member);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
else System.Console.WriteLine(result.Status);
~~~

### 18. GetPlateCornerForcesInMemberLocalCoordinates

~~~csharp
OutputQueryResult<PlateCornerMemberForces> GetPlateCornerForcesInMemberLocalCoordinates(ILoadCase loadCase, Plate plate, Node cornerNode, Node otherMemberNode)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Projects global corner forces and moments into a managed member basis at the same corner point. It does not reverse member-end signs, transfer moments, sum adjacent plates or return native analytical-member result forces. Beam overloads use supplied beta degrees and require the corner to match a supplied member end ID and point; node-direction overloads use zero beta and no fabricated native member ID. Projected global Y defines the reference except near vertical, where projected global Z is used. This convention is not native orientation read-back.  Returns PlateCornerMemberForces rather than relabeling a global PlateCornerForces packet.

Adapted source signature: `PlateCornerForces GetPlateCornerForcesInMemberLocalCoordinates(ILoadCase loadCase, Plate plate, Node cornerNode, Node otherMemberNode)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var cornerNode = new Node(1, 0, 0, 0);
var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
var otherMemberNode = plate.B;
var result = wrapper.Output.GetPlateCornerForcesInMemberLocalCoordinates(loadCase, plate, cornerNode, otherMemberNode);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
else System.Console.WriteLine(result.Status);
~~~

### 19. GetPlateCornerForces

~~~csharp
OutputQueryResult<PlateCornerForces> GetPlateCornerForces(PlateCornerForcesRequest request)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Unlike the enumerable request overload, this returns one status/data observation without allocating an indexed set.

New typed enabling overload.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var cornerNode = new Node(1, 0, 0, 0);
var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
var result = wrapper.Output.GetPlateCornerForces(new PlateCornerForcesRequest(loadCase, plate, cornerNode));
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
else System.Console.WriteLine(result.Status);
~~~

### 20. GetPlateCornerForces

~~~csharp
PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<int> loadCaseIds, int plateId, int cornerNodeId, LoadCaseType loadCaseType, IOSGeometry geometry, IOSLoad load)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Resolves case metadata and plate incidence through this wrapper's current bindings before extraction. Automatic case resolution searches primary, combination and reference metadata and returns MetadataUnavailable for a missing or ambiguous number. Explicit-family overloads avoid this ambiguity; they do not add a native family selector. No case activation or model loading occurs. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch.

New typed enabling overload.

~~~csharp
var result = wrapper.Output.GetPlateCornerForces(new[] { 1 }, 17, 1, LoadCaseType.PrimaryLoad, wrapper.Geometry, wrapper.Load);
foreach (var item in result.Items)
    if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
    else System.Console.WriteLine(item.Status);
~~~

### 21. GetPlateCornerForcesInMemberLocalCoordinates

~~~csharp
OutputQueryResult<PlateCornerMemberForces> GetPlateCornerForcesInMemberLocalCoordinates(ILoadCase loadCase, Plate plate, Node cornerNode, Beam member)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Projects global corner forces and moments into a managed member basis at the same corner point. It does not reverse member-end signs, transfer moments, sum adjacent plates or return native analytical-member result forces. Beam overloads use supplied beta degrees and require the corner to match a supplied member end ID and point; node-direction overloads use zero beta and no fabricated native member ID. Projected global Y defines the reference except near vertical, where projected global Z is used. This convention is not native orientation read-back. Unlike the other-node overload, this retains a real member ID and Start/End identity.

New typed enabling overload.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var cornerNode = new Node(1, 0, 0, 0);
var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
var member = new Beam(18, cornerNode, plate.B) { BetaAngle = 30 };
var result = wrapper.Output.GetPlateCornerForcesInMemberLocalCoordinates(loadCase, plate, cornerNode, member);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
else System.Console.WriteLine(result.Status);
~~~

### 22. GetPlateCornerForcesInMemberLocalCoordinates

~~~csharp
OutputBatchResult<PlateCornerMemberForces> GetPlateCornerForcesInMemberLocalCoordinates(IEnumerable<PlateCornerForcesRequest> requests, Beam member)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch. Projects global corner forces and moments into a managed member basis at the same corner point. It does not reverse member-end signs, transfer moments, sum adjacent plates or return native analytical-member result forces. Beam overloads use supplied beta degrees and require the corner to match a supplied member end ID and point; node-direction overloads use zero beta and no fabricated native member ID. Projected global Y defines the reference except near vertical, where projected global Z is used. This convention is not native orientation read-back. Every requested corner must be a member end; an ordinary failed extraction remains an identified failed item.

New typed enabling overload.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var cornerNode = new Node(1, 0, 0, 0);
var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
var member = new Beam(18, cornerNode, plate.B) { BetaAngle = 30 };
var result = wrapper.Output.GetPlateCornerForcesInMemberLocalCoordinates(new[] { new PlateCornerForcesRequest(loadCase, plate, cornerNode) }, member);
foreach (var item in result.Items)
    if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
    else System.Console.WriteLine(item.Status);
~~~

### 23. TransformPlateCornerForcesToMemberLocal

~~~csharp
OutputBatchResult<PlateCornerMemberForces> TransformPlateCornerForcesToMemberLocal(IEnumerable<PlateCornerForces> plateCornerForces, Beam member)
~~~

Projects global corner forces and moments into a managed member basis at the same corner point. It does not reverse member-end signs, transfer moments, sum adjacent plates or return native analytical-member result forces. Beam overloads use supplied beta degrees and require the corner to match a supplied member end ID and point; node-direction overloads use zero beta and no fabricated native member ID. Projected global Y defines the reference except near vertical, where projected global Z is used. This convention is not native orientation read-back. Unlike sparse extraction, this preserves duplicate source observations and performs no hydration or COM result query. Every source packet is validated before a result collection is returned.

New typed enabling overload.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var cornerNode = new Node(1, 0, 0, 0);
var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
var member = new Beam(18, cornerNode, plate.B) { BetaAngle = 30 };
var source = wrapper.Output.GetPlateCornerForces(loadCase, plate, cornerNode);
if (!source.IsSuccessful) return;
var result = wrapper.Output.TransformPlateCornerForcesToMemberLocal(new[] { source.Data }, member);
foreach (var item in result.Items)
    if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
    else System.Console.WriteLine(item.Status);
~~~

### 24. GetPlateCornerForces

~~~csharp
PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<PlateCornerForcesKey> keys, IOSGeometry geometry, IOSLoad load)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Resolves case metadata and plate incidence through this wrapper's current bindings before extraction. Automatic case resolution searches primary, combination and reference metadata and returns MetadataUnavailable for a missing or ambiguous number. Explicit-family overloads avoid this ambiguity; they do not add a native family selector. No case activation or model loading occurs. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch. Family-aware keys select their metadata family; keys without a family require unambiguous discovery. Equivalent resolved keys are coalesced before native extraction.

New typed enabling overload.

~~~csharp
var result = wrapper.Output.GetPlateCornerForces(new[] { new PlateCornerForcesKey(1, 17, 1) }, wrapper.Geometry, wrapper.Load);
foreach (var item in result.Items)
    if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
    else System.Console.WriteLine(item.Status);
~~~

### 25. GetPlateCornerForces

~~~csharp
PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<PlateCornerForcesKey> keys, LoadCaseType loadCaseType, IOSGeometry geometry, IOSLoad load)
~~~

Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Resolves case metadata and plate incidence through this wrapper's current bindings before extraction. Automatic case resolution searches primary, combination and reference metadata and returns MetadataUnavailable for a missing or ambiguous number. Explicit-family overloads avoid this ambiguity; they do not add a native family selector. No case activation or model loading occurs. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch. Keys without a family inherit the explicit family; a key that declares a different family is rejected.

New typed enabling overload.

~~~csharp
var result = wrapper.Output.GetPlateCornerForces(new[] { new PlateCornerForcesKey(1, 17, 1) }, LoadCaseType.PrimaryLoad, wrapper.Geometry, wrapper.Load);
foreach (var item in result.Items)
    if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
    else System.Console.WriteLine(item.Status);
~~~

## Verification and remaining work

Release compilation passed with zero warnings/errors. **6,036 offline tests** passed, including **248 new corner cases** covering every new contract on simulated 2024/2025/2026 bindings, lifetime checks, owned buffers, scalar/status types, hydration, case ambiguity, identity indexing, bounds, partial failures and transformations. Isolated package verification compiles/executes **1,152 selected XML examples**, including all **192 Output examples**. Read-only native checks are limited to the sampled 2026 build/model described above.

Output covers **47/60** documented native names through **96 managed overloads**. Thirteen documented native routes, thirteen supplemental unit candidates, the source obsolete compatibility helper and the remaining asynchronous/convenience work are not declared complete by this increment. See the [cross-version plan](reports/OpenSTAAD-Output-Multi-Version-Architecture-and-Plan.md) and [exact machine-readable contracts](contracts/OutputPlateCornerContractIndex.json).
