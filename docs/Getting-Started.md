# Getting Started with StaadPro.Interop

## Installation & Setup

1. Reference `StaadPro.Interop.dll` in your .NET project (targeting .NET Framework 4.8.1).
2. Ensure Bentley STAAD.Pro is installed on the execution machine if interacting with live models.
3. Reference `OpenSTAADUI` COM interop wrapper.

## Key Namespaces
- `StaadPro.Interop.Adapters.Interfaces`: `IOSGeometry` interface.
- `StaadPro.Interop.Models`: `StaadGeometrySession` connection entry point.
- `StaadPro.Interop.Entities`: `Node`, `Beam`, `Plate`, `Member`, `EntityGroup`.
- `StaadPro.Interop.Enums`: Enums for surface types, group types, coordinate axes.
