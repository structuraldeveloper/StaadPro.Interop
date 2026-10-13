# Geometry API comparison, grouping and mutations

The reference geometry interface and adapter contain 143 method overloads. The connectivity increment increased coverage from 134 to 137. This increment adds the three extended geometry mutations with the same public names, parameter names and types, bringing coverage to 140. The table tracks the original nine pending methods.

| Function | Status after this update | Required supporting API |
| --- | --- | --- |
| `SegregateBeamsByConnectivity(IEnumerable<Beam> beams)` | Ported | Managed beam/node entities |
| `SegregateMembersByConnectivity(IEnumerable<Member> members, MemberConnectivityScope scope = MemberConnectivityScope.EndpointsOnly)` | Ported | `MemberConnectivityScope` |
| `SegregatePlatesByConnectivity(IEnumerable<Plate> plates, PlateConnectivityScope scope = PlateConnectivityScope.SharedEdge)` | Ported | `PlateConnectivityScope` |
| `IdentifyPhysicalMembers(IEnumerable<Beam> beams, double angularToleranceDegrees = 0.005)` | Pending | `PhysicalMemberIdentificationResult` and member reconstruction |
| `GroupPhysicalMembersByPlanAngle(IEnumerable<Member> members, double angularToleranceDegrees = 1e-3)` | Pending | `PhysicalMemberPlanAngleGroup` |
| `GeneratePolygonMesh(IReadOnlyList<Node> boundaryNodes, int startingNodeId, int startingPlateId, double minimumAngleDegrees = 20.0, double maximumEdgeLength = 0.0)` | Pending | `PolygonMeshResult` and the mesh generation service |
| `CreateMultipleBeamsExtended(IOSRoot openStaad, IEnumerable<Beam> beamCollection)` | Ported | `IOSRoot`, exposed as `wrapper.Root` |
| `CreateMultipleNodesExtended(IOSRoot openStaad, IEnumerable<Node> nodeCollection)` | Ported | `IOSRoot`, exposed as `wrapper.Root` |
| `MergeNodesExtended(IOSRoot openStaad, Node retain, IEnumerable<Node> nodesToMerge)` | Ported | `IOSRoot`, exposed as `wrapper.Root` |

There are now 140 geometry overloads implemented and three pending: `IdentifyPhysicalMembers`, `GroupPhysicalMembersByPlanAngle` and `GeneratePolygonMesh`. The counts include generic, asynchronous and collection overloads separately. The comparison covers both `IOSGeometry` and `OSGeometryAdapter`.

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

`wrapper.Root` supplies the minimal `IOSRoot` binding needed by the original signatures. `OSRootAdapter` wraps the raw root through late binding, without a Bentley build dependency. Other root methods remain outside this port. Disposal clears the wrapper's binding; callers must not use bindings captured from a disposed or disconnected session.

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

The earlier package contained an XML file but no method documentation for its 134 geometry overloads; 33 load-adapter methods also lacked expanded comments. The current source provides complete documentation on all 140 geometry and all 66 load overloads, on both interfaces and concrete adapters. Previously undocumented exported managed functions now have summaries, parameter descriptions and return documentation as well.

`Documentation/ApiMembers.xml` holds shared XML comments adapted from the reference interfaces and reviewed against the current implementation. Compiler `<include>` directives expand those comments into `StaadPro.Interop.xml`; consumers need neither this source file nor the private reference project. The package includes the generated XML alongside the DLL in `lib/net481`. Existing detailed load-query examples and contracts remain intact.

Malformed or unresolved documentation warnings fail the build. Regression checks inspect the compiled XML for exported functions and adapter overloads, missing summaries, parameter-name coverage, generic parameters, returns, usage remarks, and unresolved include/inheritance/reference elements. Package verification installs an isolated NuGet consumer and compiles/runs all 87 XML examples, including eighteen examples for the nine mutation overloads on interfaces and adapters.

Already-published versions are unchanged. These additions are prepared in `1.0.0-preview.20261013.2`; publication is a separate step.

Offline checks cover array layouts, target-root routing, prevalidation, native exception propagation and partial beam failure. Live native mutation behavior and engineering-unit conventions require validation on the STAAD versions used by consumers.
