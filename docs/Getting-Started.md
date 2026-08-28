# Getting Started with StaadPro.Interop

## Installation & Setup

1. Reference `StaadPro.Interop.dll` in your .NET project (targeting .NET Framework 4.8.1).
2. Ensure Bentley STAAD.Pro is installed on the execution machine if interacting with live models.
3. No COM registration or type library installation is required at build time.

## Key Namespaces
- `StaadPro.Interop.Services`: `OpenStaadWrapperProvider` factory entry point.
- `StaadPro.Interop.Models`: `OpenStaadWrapper` root container.
- `StaadPro.Interop.Adapters.Interfaces`: `IOSGeometry` and `IOSLoad` interfaces.
- `StaadPro.Interop.Entities`: `Node`, `Beam`, `Plate`, `Member`, `LoadCase`, `EntityGroup`.
- `StaadPro.Interop.Enums`: Enums for load types (`LoadType`), load case classifications (`LoadCaseType`), unit systems (`BaseUnitSystem`), surface types, group types, coordinate axes.
