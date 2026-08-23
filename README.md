# StaadPro.Interop

[![Build Status](https://github.com/structuraldeveloper/StaadPro.Interop/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/structuraldeveloper/StaadPro.Interop/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Target: .NET 4.8.1](https://img.shields.io/badge/.NET-4.8.1-blue.svg)](https://dotnet.microsoft.com/)

A modern, strongly-typed, thread-safe .NET library providing high-level abstractions over the **Bentley STAAD.Pro OpenSTAAD COM API**.

---

## Highlights

- **Fluent API Design**: Chain node, beam, and plate creation operations seamlessly.
- **Parametric Surface & Meshing Tools**: Complete support for annular surfaces, solid circular surfaces, density lines, density control points, and polygonal openings.
- **Strongly-Typed Structural Entities**: Rich domain models for `Node`, `Beam`, `Plate`, `Member`, and generic/non-generic `EntityGroup<T>`.
- **Thread-Safe Multi-Threading & Async**: Parallelized entity creation, batch querying, and async Task-based geometry interrogation without COM deadlocks.
- **Model Coordinate Conventions**: Query active model vertical-axis orientation (`Y-Up` vs `Z-Up`).
- **Comprehensive Unit Testing**: Offline test suite covering analytical vector math, surface boundary algorithms, and orientation checks without requiring live STAAD hardware licenses.

---

## Quick Start

### 1. Connecting to an Active STAAD.Pro Instance

```csharp
using StaadPro.Interop.Models;
using StaadPro.Interop.Adapters.Interfaces;

// Connect to running STAAD.Pro instance via COM
using (var session = StaadGeometrySession.ConnectActiveInstance())
{
    IOSGeometry geometry = session.Geometry;
    
    // Check coordinate system convention
    bool isZUp = geometry.IsZUp();
    Console.WriteLine($"Active vertical axis: {geometry.GetGlobalVerticalAxis()}");
}
```

### 2. Creating Geometry (Fluent API)

```csharp
using StaadPro.Interop.Entities;

var n1 = new Node(1, 0.0, 0.0, 0.0);
var n2 = new Node(2, 5.0, 0.0, 0.0);
var beam = new Beam(101, n1, n2);

geometry.CreateNode(n1)
        .CreateNode(n2)
        .CreateBeam(beam);
```

### 3. Creating Parametric Annular Surface with Meshing

```csharp
var center = new Node(100, 0.0, 0.0, 0.0);

// Generates outer boundary (radius 10m, 24 divs) and inner boundary (radius 3m, 12 divs)
var (surfaceId, outerNodes, innerNodes) = geometry.CreateAnnularParametricSurface(
    surfaceName: "SILO_BASE_ANNULUS",
    center: center,
    rOuter: 10.0,
    divOuter: 24,
    rInner: 3.0,
    divInner: 12,
    idLastUsedNode: 200,
    autoGenerate: AutoGenerate.Yes);

// Add mesh refinement density line across diameter
geometry.AddDensityLineToSurface(surfaceId, outerNodes.First(), outerNodes.Last(), sDensity: 5, eDensity: 5, nDivisions: 10);
geometry.CommitParametricSurfaceMesh(surfaceId);
```

### 4. Parallel & Multi-Threaded Batch Queries

```csharp
// Retrieve all beams in parallel across 4 worker threads
HashSet<Beam> allBeams = geometry.GetAllEntities<Beam>(nThreads: 4);

// Filter plates connected to a particular node
IEnumerable<Plate> incidentPlates = geometry.GetPlatesConnectedAtNode(nodeId: 45);
```

---

## Architecture

```
StaadPro.Interop/
├── Adapters/
│   ├── Interfaces/   # IOSGeometry and IOSBase contracts
│   └── Models/       # OSGeometryAdapter implementation
├── Common/           # Standalone IEntity and property store
├── Entities/         # Node, Beam, Plate, Member, EntityGroup
├── Enums/            # SurfaceType, RegionType, GroupType, BeamAxis, etc.
├── Extensions/       # Math, Node, Beam, Plate extension methods
├── Helpers/          # Angle, Entity, Group, and coordinate math helpers
└── Models/           # StaadGeometrySession host wrapper
```

---

## Building and Testing

```powershell
# Restore & Build
dotnet build StaadPro.Interop.sln -c Release

# Run Offline Unit Test Suite
dotnet test tests/StaadPro.Interop.Tests/StaadPro.Interop.Tests.csproj -c Release
```

---

## Project Roadmap & Future Modules

StaadPro.Interop begins with a high-performance Geometry foundation. Future milestones will expand the ecosystem with dedicated, decoupled modules for Loads, Properties, Supports, Analysis Results, Code Design, Viewport Visualization, and BIM Bridges.

See [ROADMAP.md](ROADMAP.md) for the full architectural vision, milestone details, and planned modules.

---

## Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](CONTRIBUTING.md) and [CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md) before submitting pull requests.

## License

This project is licensed under the [MIT License](LICENSE).
