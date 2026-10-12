# Geometry API comparison and connectivity grouping

The reference geometry interface and adapter contain 143 method overloads. Before this update, 134 were present here. The following nine methods were pending; three are now ported with the same names, parameter names, types and default values.

| Function | Status after this update | Required supporting API |
| --- | --- | --- |
| `SegregateBeamsByConnectivity(IEnumerable<Beam> beams)` | Ported | Managed beam/node entities |
| `SegregateMembersByConnectivity(IEnumerable<Member> members, MemberConnectivityScope scope = MemberConnectivityScope.EndpointsOnly)` | Ported | `MemberConnectivityScope` |
| `SegregatePlatesByConnectivity(IEnumerable<Plate> plates, PlateConnectivityScope scope = PlateConnectivityScope.SharedEdge)` | Ported | `PlateConnectivityScope` |
| `IdentifyPhysicalMembers(IEnumerable<Beam> beams, double angularToleranceDegrees = 0.005)` | Pending | `PhysicalMemberIdentificationResult` and member reconstruction |
| `GroupPhysicalMembersByPlanAngle(IEnumerable<Member> members, double angularToleranceDegrees = 1e-3)` | Pending | `PhysicalMemberPlanAngleGroup` |
| `GeneratePolygonMesh(IReadOnlyList<Node> boundaryNodes, int startingNodeId, int startingPlateId, double minimumAngleDegrees = 20.0, double maximumEdgeLength = 0.0)` | Pending | `PolygonMeshResult` and the mesh generation service |
| `CreateMultipleBeamsExtended(IOSRoot openStaad, IEnumerable<Beam> beamCollection)` | Pending | `IOSRoot` and its geometry-update operations |
| `CreateMultipleNodesExtended(IOSRoot openStaad, IEnumerable<Node> nodeCollection)` | Pending | `IOSRoot` and its geometry-update operations |
| `MergeNodesExtended(IOSRoot openStaad, Node retain, IEnumerable<Node> nodesToMerge)` | Pending | `IOSRoot` and node merging |

There are now 137 geometry overloads implemented and six pending. The counts include generic, asynchronous and collection overloads separately. The comparison covers both `IOSGeometry` and `OSGeometryAdapter`.

## Newly ported functions

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

## IntelliSense in an installed package

The earlier package contained an XML file but no method documentation for its 134 geometry overloads; 33 load-adapter methods also lacked expanded comments. The current source provides complete documentation on all 137 geometry and all 54 load overloads, on both interfaces and concrete adapters. Previously undocumented exported managed functions now have summaries, parameter descriptions and return documentation as well.

`Documentation/ApiMembers.xml` holds shared XML comments adapted from the reference interfaces and reviewed against the current implementation. Compiler `<include>` directives expand those comments into `StaadPro.Interop.xml`; consumers need neither this source file nor the private reference project. The package includes the generated XML alongside the DLL in `lib/net481`. Existing detailed load-query examples and contracts remain intact.

Malformed or unresolved documentation warnings fail the build. Regression checks inspect the compiled XML for exported functions and adapter overloads, missing summaries, parameter-name coverage, generic parameters, returns, usage remarks, and unresolved include/inheritance/reference elements. Package verification installs an isolated NuGet consumer and compiles/runs all 51 XML examples, including nine examples covering the three new functions on interface, adapter and service surfaces.

Already-published `1.0.0-preview.20261012.1` is unchanged. These additions are prepared in `1.0.0-preview.20261012.2`; publication is a separate step.

Validation passed with **383 offline tests**, a release build with zero warnings/errors, and the isolated installed-package checks above. Native engineering-unit and broader version checks remain separate from these managed connectivity algorithms.
