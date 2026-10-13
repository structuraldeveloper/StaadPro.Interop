# StaadPro.Interop

[![Build Status](https://github.com/structuraldeveloper/StaadPro.Interop/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/structuraldeveloper/StaadPro.Interop/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Target: .NET 4.8.1](https://img.shields.io/badge/.NET-4.8.1-blue.svg)](https://dotnet.microsoft.com/)

A strongly typed .NET Framework 4.8.1 library providing high-level abstractions over the **Bentley STAAD.Pro OpenSTAAD COM API**. Windows and a licensed STAAD installation are required at runtime. Version compatibility and live COM behavior must be verified for the methods your application uses.

See [NuGet readiness](docs/NuGet-Readiness.md) for package verification results and remaining release gates.

Use the [installation guide](docs/Installation.md) for public/local NuGet feeds and `net481` consumer examples, and the [publishing guide](docs/Publishing.md) for verified web upload and public availability checks.

---

## Highlights

- **OpenStaadWrapper & Provider**: Flexible acquisition via `OpenStaadWrapperProvider.Get()` and `GetRunning()` using Windows Running Object Table (ROT) lookup, active instance attachment, or automated background launching.
- **Fluent API Design**: Chain node, beam, and plate creation operations seamlessly.
- **Managed Connectivity Groups**: Group beams, physical members and plates with `SegregateBeamsByConnectivity`, `SegregateMembersByConnectivity` and `SegregatePlatesByConnectivity`. See [geometry API comparison and grouping](docs/Geometry-Porting.md) for scope defaults, semantics and the three remaining geometry functions.
- **Extended Geometry Mutations**: Create nodes, create beams and merge nodes with `CreateMultipleNodesExtended`, `CreateMultipleBeamsExtended` and `MergeNodesExtended`, using `wrapper.Root` and the original public signatures.
- **Factored and Thermal Loads**: Add reference, repeat and synchronous axial thermal assignments with `AddReferenceLoadsTo`, `AddRepeatLoadsTo` and `AddAxialThermalGradientTo`. See [load porting and contracts](docs/Load-Porting.md).
- **Grouped Load Assignments**: Apply member/nodal tuples by load case with `AddMemberLoadsFromGroupsByLoadCase` and `AddNodalLoadsFromGroupsByLoadCase`. The four-parameter thermal overload preserves per-entity calls on the caller's COM thread.
- **Package IntelliSense**: Expanded XML documentation covers all geometry/load overloads on interfaces and concrete adapters and accompanies the NuGet DLL.
- **Parametric Surface & Meshing Tools**: Complete support for annular surfaces, solid circular surfaces, density lines, density control points, and polygonal openings.
- **Load Case Creation & Management**: Create primary/reference cases, discover their metadata (`GetAllPrimaryLoadCases`, `GetAllReferenceLoadCases`), look up known cases (`GetPrimaryLoadCaseFromId`, `GetReferenceLoadCaseFromId`, `GetPrimaryLoadCasesFromIds`), query titles, and clear cases.
- **Reference Batches & Combination Metadata**: Read ordered reference batches (`GetReferenceLoadCasesFromIds`), discover combinations (`GetAllLoadCombinationCases`) and verify individual combination metadata (`GetLoadCombinationCaseFromId`) without changing the active case.
- **Case Selection**: Read distinct combination batches (`GetLoadCombinationCasesFromIds`), select a metadata family (`GetAllLoadCasesOfType`) and find a case with nullable results (`GetLoadCase`). All preserve active-case state.
- **Case Filters & Lists**: Filter inclusive ranges (`GetLoadCases`) or requested IDs (`GetLoadCasesFromIds`), and materialize family metadata (`GetLoadCasesOfType`) with eager validation and unchanged active-case state.
- **Load Assignment Read-Back**: Query nodal loads, uniform and concentrated member forces, concentrated and uniform member moments, linearly varying forces, and trapezoidal forces with complete interface/concrete IntelliSense. See [Load queries](docs/Load-Queries.md).
- **Zero Proprietary Binary Dependencies**: Completely decoupled from registered COM TypeLibs at build time. Compiles cleanly on any machine and CI environment.
- **Strongly-Typed Structural Entities**: Rich domain models for `Node`, `Beam`, `Plate`, `Member`, `LoadCase`, and generic/non-generic `EntityGroup<T>`.
- **Batch & Async Geometry APIs**: The library includes batch and asynchronous helpers. Coordinate shared STAAD session access and COM apartment requirements in the calling application; these helpers do not establish a library-wide thread-safety guarantee.
- **Model Coordinate & Unit Conventions**: Query active model vertical-axis orientation (`Y-Up` vs `Z-Up`) and base unit systems (`Imperial` vs `Metric`).
- **Comprehensive Unit Testing**: Offline test suite covering analytical vector math, surface boundary algorithms, load case operations, and wrapper life-cycle without requiring live STAAD hardware licenses.

---

## Quick Start

### 1. Connecting to STAAD.Pro via OpenStaadWrapperProvider

```csharp
using StaadPro.Interop.Models;
using StaadPro.Interop.Services;
using StaadPro.Interop.Adapters.Interfaces;

// Acquire wrapper for active session or specific model file
using (OpenStaadWrapper wrapper = OpenStaadWrapperProvider.Get(@"C:\Projects\Structure.std"))
{
    IOSGeometry geometry = wrapper.Geometry;
    IOSLoad load = wrapper.Load;
    
    // Check coordinate system and base units
    bool isZUp = geometry.IsZUp();
    Console.WriteLine($"Base units: {wrapper.BaseUnitSystem}, Vertical axis: {geometry.GetGlobalVerticalAxis()}");
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

### 4. Batch Geometry Queries

```csharp
// Use one worker unless your application has verified COM threading behavior.
HashSet<Beam> allBeams = geometry.GetAllEntities<Beam>(nThreads: 1);

// Filter plates connected to a particular node
IEnumerable<Plate> incidentPlates = geometry.GetPlatesConnectedAtNode(nodeId: 45);
```

### 5. Creating & Managing Load Cases

```csharp
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;

// Create new primary load cases
int deadLoadId = wrapper.Load.CreateNewPrimaryLoad(1, "DEAD_LOAD", LoadType.Dead);
int liveLoadId = wrapper.Load.CreateNewPrimaryLoad(2, "ROOF_LIVE", LoadType.RoofLive);

// Create reference load case
int refEquipId = wrapper.Load.CreateNewReferenceLoad(101, "EQUIPMENT_DEAD", LoadType.Dead);

// Query load case title
string title = wrapper.Load.GetLoadCaseTitle(deadLoadId);
Console.WriteLine($"Load Case {deadLoadId}: {title}");

// Clear load case
var deadLoad = new LoadCase(deadLoadId, title, LoadCaseType.PrimaryLoad, LoadType.Dead);
wrapper.Load.ClearPrimaryLoadCase(deadLoad);
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
├── Enums/            # LoadType, LoadCaseType, BaseUnitSystem, ForceInputUnit, LengthInputUnit, etc.
├── Extensions/       # Math, Node, Beam, Plate extension methods
├── Helpers/          # Angle, Entity, Group, RotHelpers, and OpenStaadWrapperHelpers
├── Interfaces/       # IOpenStaadWrapperResolver
├── Models/           # OpenStaadWrapper host model
└── Services/         # OpenStaadWrapperProvider
```

---

## Building and Testing

The current load port exposes 66 overloads, with 54 reference overloads pending. The latest additions assign self-weight, create envelopes and select native load lists, alongside the envelope metadata queries. See [load porting progress](docs/Load-Porting.md) and the [envelope query guide](docs/Load-Queries.md#load-envelope-queries).

The root adapter now implements three of the reference's 23 methods: model-path, filename and analysis-file-path retrieval. Access them through `wrapper.Root`; see [root porting progress and examples](docs/Root-Porting.md).

```powershell
# Restore & Build
dotnet build StaadPro.Interop.sln -c Release

# Run Offline Unit Test Suite
dotnet test tests/StaadPro.Interop.Tests/StaadPro.Interop.Tests.csproj -c Release

# Build and verify an installable preview, including packaged IntelliSense examples
dotnet pack src/StaadPro.Interop/StaadPro.Interop.csproj -c Release -p:PackageVersion=1.0.0-preview.20261013.2 -o artifacts/release
./eng/Verify-LoadPackage.ps1 -PackagePath artifacts/release/StaadPro.Interop.1.0.0-preview.20261013.2.nupkg
```

---

## Project Roadmap & Future Modules

See [ROADMAP.md](ROADMAP.md) for the full architectural vision, milestone details, and planned modules.

---

## Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](CONTRIBUTING.md) and [CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md) before submitting pull requests.

## License

See [release notes](docs/Release-Notes.md) for the preview's changes, verification and remaining validation.

This project is licensed under the [MIT License](LICENSE).
