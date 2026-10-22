# Geometry API comparison, grouping and mutations

The reference geometry interface and adapter contain 143 method overloads. All 143 are now ported, including the final physical-member identification, plan-angle grouping and managed polygon-mesh operations. The table tracks the original nine pending functions; all are complete.

| Function | Status after this update | Required supporting API |
| --- | --- | --- |
| `SegregateBeamsByConnectivity(IEnumerable<Beam> beams)` | Ported | Managed beam/node entities |
| `SegregateMembersByConnectivity(IEnumerable<Member> members, MemberConnectivityScope scope = MemberConnectivityScope.EndpointsOnly)` | Ported | `MemberConnectivityScope` |
| `SegregatePlatesByConnectivity(IEnumerable<Plate> plates, PlateConnectivityScope scope = PlateConnectivityScope.SharedEdge)` | Ported | `PlateConnectivityScope` |
| `IdentifyPhysicalMembers(IEnumerable<Beam> beams, double angularToleranceDegrees = 0.005)` | Ported | `PhysicalMemberIdentificationResult` and member reconstruction |
| `GroupPhysicalMembersByPlanAngle(IEnumerable<Member> members, double angularToleranceDegrees = 1e-3)` | Ported | `PhysicalMemberPlanAngleGroup` |
| `GeneratePolygonMesh(IReadOnlyList<Node> boundaryNodes, int startingNodeId, int startingPlateId, double minimumAngleDegrees = 20.0, double maximumEdgeLength = 0.0)` | Ported | `PolygonMeshResult` and the mesh generation service |
| `CreateMultipleBeamsExtended(IOSRoot openStaad, IEnumerable<Beam> beamCollection)` | Ported | `IOSRoot`, exposed as `wrapper.Root` |
| `CreateMultipleNodesExtended(IOSRoot openStaad, IEnumerable<Node> nodeCollection)` | Ported | `IOSRoot`, exposed as `wrapper.Root` |
| `MergeNodesExtended(IOSRoot openStaad, Node retain, IEnumerable<Node> nodesToMerge)` | Ported | `IOSRoot`, exposed as `wrapper.Root` |

There are now 143 geometry overloads implemented, with no reference Geometry overload pending. The counts include generic, asynchronous and collection overloads separately. The comparison covers both `IOSGeometry` and `OSGeometryAdapter`.

## Connectivity grouping

All three grouping operations run entirely on supplied managed data and make no native OpenSTAAD calls. They materialize input once, ignore null entity entries, allocate fresh group lists and reuse the original entity objects. They do not modify the supplied entities, select model elements, or change STAAD model state. The standalone `ConnectivityService` provides the same operations without requiring a wrapper.

- **Beams:** compares endpoint node IDs and groups transitively connected beams. A simple unbranched chain is ordered from a lowest-degree endpoint. Branches and loops stay in one group but have no unique traversal order. Beam IDs are deduplicated explicitly, keeping the first object: the target `Beam.Equals` also compares endpoints, so relying on `Distinct()` alone could retain two objects with the same ID and fail dictionary construction.
- **Members:** the default `EndpointsOnly` scope compares each member's start/end nodes by node ID. `AnyNode` uses each member's `Nodes` collection, allowing interior-node T-junctions. The collection is not rebuilt from constituent beams.
- **Plates:** `AnyCorner` joins corner-only contacts. The default `SharedEdge` preserves the reference predicate: consecutive corners on the candidate must both occur in the compared plate's corner set; the target winding is not separately checked. Null/repeated triangle corners are omitted. Supply consistent mesh incidence; these methods do not test geometric overlap or coordinate tolerance.

Null input sequences throw `ArgumentNullException`. Empty input returns no groups. Undefined scope values retain the reference endpoint/edge fallback. Group order is unspecified; IDs and equality fields must remain stable during grouping. Fully initialized beam endpoints are required.

```csharp
using System.Linq;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Services;

var a = new Node(1, 0, 0, 0);
var b = new Node(2, 1, 0, 0);
var c = new Node(3, 2, 0, 0);
var beams = new[] { new Beam(1, a, b), new Beam(2, b, c) };
var groups = ConnectivityService.SegregateBeamsByConnectivity(beams);
var chain = groups.Single().ToList(); // two original beam objects

// With an existing wrapper, the same methods are exposed on wrapper.Geometry:
// wrapper.Geometry.SegregateMembersByConnectivity(members, MemberConnectivityScope.AnyNode);
// wrapper.Geometry.SegregatePlatesByConnectivity(plates, PlateConnectivityScope.SharedEdge);
```

## Extended geometry mutations

`wrapper.Root` supplies the minimal `IOSRoot` binding needed by the original signatures. `OSRootAdapter` wraps the raw root through late binding, without a Bentley build dependency. The reviewed Root mappings are now complete; see [Root completion](Root-Completion-and-Capabilities.md). Disposal clears the wrapper's binding; callers must not use bindings captured from a disposed or disconnected session.

- `CreateMultipleNodesExtended` sends explicit integer IDs and a `double[count, 3]` XYZ matrix through `openStaad.ComObject.Geometry.CreateMultipleNodes`. Coordinates use the target model's input length units with no conversion.
- `CreateMultipleBeamsExtended` retains the public signature but replaces the reference's compile-time-obsolete, non-working bulk call with ordered synchronous `CreateBeam` calls using integer beam and endpoint IDs. Nodes must already exist. All input is checked before the first creation; a later native failure can leave earlier beams created.
- `MergeNodesExtended` retains the reference workaround's inclusion of the retained ID in the merge array. The retained ID must occur exactly once; matching uses the ID rather than managed object equality. A retained-only collection makes no native call. Empty input is invalid.

These methods validate positive IDs, reject null entries and repeated creation/merge IDs, and enumerate input once. Node coordinates must be finite. Each method returns the receiving geometry adapter, while the supplied root determines the target model. Native return values are not interpreted and native exceptions propagate. They do not refresh managed geometry, roll back native changes or save the model. Use the STAAD COM thread and serialize session access.

```csharp
// With a connected wrapper and unused node/beam IDs in its model:
var a = new Node(210, 0, 0, 0);
var b = new Node(211, 4.5, 0, 0);
wrapper.Geometry.CreateMultipleNodesExtended(wrapper.Root, new[] { a, b });
wrapper.Geometry.CreateMultipleBeamsExtended(wrapper.Root,
    new[] { new Beam(310, a, b) });
// To merge existing nodes, include the retained ID in the collection:
// wrapper.Geometry.MergeNodesExtended(wrapper.Root, a, new[] { a, redundantNode });
```

## IntelliSense in an installed package

The earlier package contained an XML file but no method documentation for its 134 geometry overloads; 33 load-adapter methods also lacked expanded comments. The current source provides complete documentation on all 143 geometry and all 78 load overloads, on both interfaces and concrete adapters. Previously undocumented exported managed functions now have summaries, parameter descriptions and return documentation as well.

`Documentation/ApiMembers.xml` holds shared XML comments adapted from the reference interfaces and reviewed against the current implementation. Compiler `<include>` directives expand those comments into `StaadPro.Interop.xml`; consumers need neither this source file nor the private reference project. The package includes the generated XML alongside the DLL in `lib/net481`. Existing detailed load-query examples and contracts remain intact.

Malformed or unresolved documentation warnings fail the build. Regression checks inspect the compiled XML for exported functions and adapter overloads, missing summaries, parameter-name coverage, generic parameters, returns, usage remarks, and unresolved include/inheritance/reference elements. Package verification installs an isolated NuGet consumer and checks the existing examples and the final nine Geometry examples; the updated verifier expects 250 selected XML blocks in the next package.

Already-published versions are unchanged. The final three Geometry operations are source additions after `1.0.0-preview.20261015`; they require a new package version before distribution.

Offline checks cover array layouts, target-root routing, prevalidation, native exception propagation and partial beam failure. Live native mutation behavior and engineering-unit conventions require validation on the STAAD versions used by consumers.


## Completed managed Geometry operations

The final three reference functions are now implemented. The Geometry interface and its single, non-partial adapter cover **all 143 reference overloads**. The main source project was read only. These additions are source changes and are not part of the already published `1.0.0-preview.20261015` archive.

| Function | Managed behavior |
| --- | --- |
| `IdentifyPhysicalMembers(IEnumerable<Beam>, double = 0.005)` | Reconstructs straight runs in simple analytical chains; returns `PhysicalMemberIdentificationResult`. |
| `GroupPhysicalMembersByPlanAngle(IEnumerable<Member>, double = 0.001)` | Groups undirected XZ bearings; returns materialized `IEnumerable<PhysicalMemberPlanAngleGroup>`. |
| `GeneratePolygonMesh(IReadOnlyList<Node>, int, int, double = 20, double = 0)` | Generates a managed triangular mesh; returns `PolygonMeshResult` with measured quality and generated entities. |

### Physical-member reconstruction

The service snapshots input once, ignores null entries and deduplicates beam IDs while retaining the first consistent instance. Positive IDs, finite nonzero segments, shared-node coordinates and repeated beam definitions are validated before replacing any back-reference. Connectivity uses node IDs. It does not consult the connected native model.

A simple chain is traversed end to end and split when adjacent directions differ beyond the requested tolerance; antiparallel beam incidence is supported. The comparison is pairwise, so a long run can accumulate deviation. A single beam forms a member. Entire branched components and closed cycles are returned as discarded beams. This corrects the source's degree-only cycle acceptance, which could produce a member with coincident endpoints.

Member IDs are synthetic, starting at one for each call. Original nodes and beams are reused. Accepted beams receive the new `Beam.Member` back-reference; discarded beams keep their existing assignment. Member beam/node collections are sets, with no enumeration-order promise. Repeated calls replace accepted assignments. The managed-only back-reference is excluded from JSON serialization to avoid a member/beam reference cycle.

### Plan-angle grouping

Uses the source's global XZ convention: `atan2(deltaZ, deltaX)`, reduced to [0,180). It does not query Y-up/Z-up state. A member with zero XZ projection has bearing zero by convention. Opposite directions group together, including wraparound near zero/180.

Input is enumerated once. Null entries are ignored; repeated member references are retained. Each member joins the first existing group representative within tolerance, so grouping is input-order-dependent rather than transitive. Results are materialized and sorted by representative bearing. Both angular operations require a finite tolerance from zero to less than 90 degrees.

### Polygon meshing and quality

The port retains the source's managed ear-clipping, constrained edge-flip and point-insertion approach. It corrects boundary-to-node mapping after winding reversal, missing generated IDs, the source's off-by-one first plate ID, approximate-key hash inconsistencies, convex edge-flip checks and nonconforming insertion on shared edges. It removes the source's unsafe fallback that clipped a vertex without validating the ear.

Boundary input is an ordered open loop of 3–512 nonnull nodes. IDs must be positive and unique; omit the repeated closing vertex. Coordinates must be finite and define a simple planar polygon. Holes, nonadjacent touching edges, crossings and overlapping segments are rejected. Consecutive collinear subdivisions are retained when numerically resolvable. Coordinates are normalized by the largest component extent from the first node; coplanarity tolerance is 1e-9 of that extent and point separation must exceed 1e-10. Numerically unresolved geometry fails explicitly.

The first generated node uses `startingNodeId`, and the first plate uses `startingPlateId`, both inclusive. Generated sequences are checked for Int32 overflow; generated node IDs cannot overlap boundary IDs. The caller must allocate IDs free in its actual STAAD model. No IDs are reserved, and no global entity counter is read or updated.

Original boundary objects, order, IDs and positions remain unchanged. Refinement can introduce new nodes along existing boundary segments. The source-compatible `InteriorNodes` name includes these boundary split nodes as well as strictly interior nodes; it does not mean every generated node is strictly interior. The original geometric boundary is preserved, while edge discretization may change.

Targets are a finite angle strictly between zero and 60 degrees and an optional finite nonnegative maximum edge length in the input coordinate units. Zero disables the edge-length target. Refinement is bounded to 256 inserted nodes, with at most 16 local edge-flip sweeps per pass. These bounds ensure finite work; they are not elapsed-time guarantees. The implementation is a best-effort mesher, not a certified robust-predicate or quality-guaranteed mesh generator.

`ReachedTargetQuality` reports whether **both** requested targets were achieved. When progress stops or the insertion budget is exhausted, a valid mesh may return false. `AchievedMinimumAngleDegrees` reports the actual measured angle. Numeric degeneracy and invalid topology throw instead of returning an unsafe fallback. Result collections and structural entities remain mutable, matching the source models.

### Usage without STAAD

```csharp
using System;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Services;

var boundary = new[] {
    new Node(1, 0, 0, 0),
    new Node(2, 4, 0, 0),
    new Node(3, 4, 0, 4),
    new Node(4, 0, 0, 4)
};
var mesh = PolygonMeshGenerationService.GenerateMesh(boundary, 100, 200, 20, 2.5);
Console.WriteLine(mesh.ReachedTargetQuality);
Console.WriteLine(mesh.Plates.Count);

// With an existing wrapper:
// var mesh = wrapper.Geometry.GeneratePolygonMesh(boundary, 100, 200, 20, 2.5);
// Explicitly create native nodes/plates later, after inspecting quality and ID availability.
```

No operation in this section dispatches native OpenSTAAD, saves a model or changes its units. Physical-member reconstruction does update the documented managed back-references. The other operations only construct managed output.

### Verification

Regression tests cover reversed incidence, chain splitting, branches/cycles, duplicate identity, invalid geometry, single enumeration, circular bearing grouping, representative grouping, concave polygons, both windings, horizontal/vertical/tilted planes, scale variation, deterministic irregular polygons, refinement, area conservation, edge incidence, boundary preservation, hanging-node prevention, ID collision/overflow, invalid topology, quality limits, wrapper refresh and matching XML contracts. Release compilation checks all XML includes and cross-references. Future package verification expects 143 Geometry methods and 226 selected XML example blocks.

Verification on 22 October 2026 passed **1,604 offline tests**, including **57 new cases**, and a Release build with zero warnings/errors. All **nine new compiled XML example blocks** were compiled and executed in a source-reference consumer. The package verifier was updated for the next archive; no new NuGet archive or installed-package result is claimed for this increment.
