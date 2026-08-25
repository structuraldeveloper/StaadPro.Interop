# StaadPro.Interop

[![Build Status](https://github.com/structuraldeveloper/StaadPro.Interop/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/structuraldeveloper/StaadPro.Interop/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Target: .NET 4.8.1](https://img.shields.io/badge/.NET-4.8.1-blue.svg)](https://dotnet.microsoft.com/)

A modern, strongly-typed, thread-safe .NET library providing high-level abstractions over the **Bentley STAAD.Pro OpenSTAAD COM API**.

---

## Highlights

- **Fluent API Design**: Chain node, beam, and plate creation operations seamlessly.
- **Parametric Surface & Meshing Tools**: Complete support for annular surfaces, solid circular surfaces, density lines, density control points, and polygonal openings.
- **Load Case Management**: Clear primary and reference load cases with type-safe methods (`ClearPrimaryLoadCase`, `ClearReferenceLoadCase`).
- **Zero Proprietary Binary Dependencies**: Completely decoupled from registered COM TypeLibs at build time. Compiles cleanly on any machine and CI environment.
- **Strongly-Typed Structural Entities**: Rich domain models for `Node`, `Beam`, `Plate`, `Member`, `LoadCase`, and generic/non-generic `EntityGroup<T>`.
- **Thread-Safe Multi-Threading & Async**: Parallelized entity creation, batch querying, and async Task-based geometry interrogation without COM deadlocks.
- **Model Coordinate Conventions**: Query active model vertical-axis orientation (`Y-Up` vs `Z-Up`).
- **Comprehensive Unit Testing**: Offline test suite covering analytical vector math, surface boundary algorithms, load case operations, and orientation checks without requiring live STAAD hardware licenses.

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
    IOSLoad load = session.Load;
    
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

### 5. Clearing Primary & Reference Load Cases

```csharp
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;

var deadLoad = new LoadCase(1, "DEAD_LOAD", LoadCaseType.PrimaryLoad);
var refLoad = new LoadCase(101, "REF_EQUIPMENT", LoadCaseType.ReferenceLoad);

// Clear single primary load case
session.Load.ClearPrimaryLoadCase(deadLoad);

// Clear reference load case
session.Load.ClearReferenceLoadCase(refLoad);

// Clear multiple load cases by ID
session.Load.ClearPrimaryLoadCases(new[] { 1, 2, 3 });
session.Load.ClearReferenceLoadCases(new[] { 101, 102 });
```

---

## Architecture

```
StaadPro.Interop/
├── Adapters/
│   ├── Interfaces/   # IOSGeometry, IOSLoad, and IOSBase contracts
│   └── Models/       # OSGeometryAdapter and OSLoadAdapter implementations
├── Common/           # Standalone IEntity and property store
├── Entities/         # Node, Beam, Plate, Member, LoadCase, EntityGroup
├── Enums/            # LoadCaseType, SurfaceType, RegionType, GroupType, etc.
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

See [ROADMAP.md](ROADMAP.md) for the full architectural vision, milestone details, and planned modules.

---

## Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](CONTRIBUTING.md) and [CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md) before submitting pull requests.

## License

This project is licensed under the [MIT License](LICENSE).
