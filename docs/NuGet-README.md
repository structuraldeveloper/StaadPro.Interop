# StaadPro.Interop

A strongly-typed .NET Framework wrapper for Bentley STAAD.Pro OpenSTAAD

## Version 1.0.0-preview.20261014.7

StaadPro.Interop provides typed adapters and managed models for working with an existing Bentley STAAD.Pro application through OpenSTAAD. It includes geometry and load operations, model information and lifecycle functions, application-window controls, and native analysis requests.

This preview updates package metadata and documentation. Its runtime API is unchanged from `1.0.0-preview.20261014.6`. See the [release notes](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Release-Notes.md) for the changes included in each version.

## Requirements

- Windows and **.NET Framework 4.8.1** (`net481`).
- A separately installed and licensed STAAD.Pro application exposing the OpenSTAAD functions used by your application.
- OpenSTAAD access from an appropriate STA thread, with shared model and application operations coordinated by the caller.

The package does not include Bentley executables, COM binaries or a STAAD.Pro licence. This distribution targets .NET Framework; it does not contain a .NET 8 library asset.

## Installation

After this version is available on NuGet.org:

```powershell
dotnet add package StaadPro.Interop --version 1.0.0-preview.20261014.7
```

For a .NET Framework project, the package reference is:

```xml
<PropertyGroup>
  <TargetFramework>net481</TargetFramework>
</PropertyGroup>
<ItemGroup>
  <PackageReference Include="StaadPro.Interop" Version="1.0.0-preview.20261014.7" />
</ItemGroup>
```

Enable prerelease packages when searching in Visual Studio. The [installation guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Installation.md) covers runtime requirements, public and local feeds, and consumer setup.

## Connect to an open model

Use a synchronous STA entry point for a console application. This example attaches to an available running session and reads the model path:

```csharp
using System;
using StaadPro.Interop.Services;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        using (var wrapper = OpenStaadWrapperProvider.GetRunning())
        {
            if (wrapper == null)
            {
                Console.WriteLine("No usable running OpenSTAAD session was found.");
                return;
            }

            Console.WriteLine(wrapper.Root.GetStaadFileFullPath());
            // Use wrapper.Geometry, wrapper.Load and wrapper.Root on this same thread.
        }
    }
}
```

Keep acquisition and subsequent COM operations on the appropriate session thread. The application is responsible for coordinating concurrent access, native dialogs and model changes.

## Geometry and connectivity

`IOSGeometry` and `OSGeometryAdapter` expose typed geometry operations. Managed connectivity grouping is available through `SegregateBeamsByConnectivity`, `SegregateMembersByConnectivity` and `SegregatePlatesByConnectivity`, including the standalone `ConnectivityService`.

Extended root-bound operations include `CreateMultipleNodesExtended`, `CreateMultipleBeamsExtended` and `MergeNodesExtended`. These operations use the supplied root session; mutations can have partial native effects and do not automatically refresh managed geometry or roll back earlier changes.

See the [geometry guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Geometry-Porting.md) for signatures, connectivity rules and mutation contracts.

## Load assignments and metadata

Assignment readers include:

- `GetNodalLoads`
- `GetMemberUniformlyDistributedLoads`
- `GetMemberConcentratedLoads`
- `GetMemberConcentratedMoments`
- `GetMemberLinearVaryingLoads`
- `GetMemberTrapezoidalLoads`
- `GetMemberUniformMoments`

Returned values preserve native signs and units. Assignment reads can activate the selected case; they are distinct from analysis-result retrieval.

Case discovery and filtering include `GetAllPrimaryLoadCases`, `GetAllReferenceLoadCases`, `GetAllLoadCombinationCases`, `GetRepeatLoadCases`, `GetAllLoadCasesOfType`, `GetLoadCase`, `GetLoadCases`, `GetLoadCasesFromIds` and `GetLoadCasesOfType`. Direct and collection lookups include `GetPrimaryLoadCaseFromId`, `GetReferenceLoadCaseFromId`, `GetLoadCombinationCaseFromId`, `GetPrimaryLoadCasesFromIds`, `GetReferenceLoadCasesFromIds` and `GetLoadCombinationCasesFromIds`.

Envelope queries include `GetEnvelopeIds`, `GetLoadEnvelopeDetail` and `GetAllLoadEnvelopeDetails`. Combination definition read-back is available through `GetLoadCombination`.

Assignment and creation operations include `AddReferenceLoadsTo`, `AddRepeatLoadsTo`, `AddAxialThermalGradientTo`, `AddSelfweightInXYZTo`, `CreateLoadCombinations`, `CreateLoadEnvelopExt` and `CreateLoadList`. Grouped operations include `GetMemberLoadsGroupedByLoadCase`, `GetNodalLoadsGroupedByLoadCase`, `AddMemberLoadsFromGroupsByLoadCase` and `AddNodalLoadsFromGroupsByLoadCase`.

Managed member-load population is available through `PopulateMemberConcentratedLoads`, `PopulateMemberConcentratedMoments`, `PopulateMemberLinearVaryingLoads`, `PopulateMemberTrapezoidalLoads` and `PopulateMemberLoads`. Individual-family population and full refresh have different append/replacement behavior; see their XML documentation before combining calls.

The [load-query guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Load-Queries.md) and [load-operation guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Load-Porting.md) explain native mappings, activation, validation, units and partial-failure behavior.

## Model and application operations

`OpenStaadWrapper.Root` exposes `IOSRoot`, implemented by `OSRootAdapter`.

| Area | Functions |
| --- | --- |
| Filenames and model identity | `GetStaadFileFullPath`, `GetStaadFileNameOnly`, `GetAnlFileFullPath`, `GetOpenedStaadFilename`, `IsTarget`, `ModelIsOpen` |
| Job information and units | `GetShortJobInfo`, `GetFullJobInfo`, `SetInputUnitsEx` |
| Saving and closing | `SetSilentMode`, `SaveModel`, `CloseSTAADFile`, `CloseCurrentStaadModel`, `CloseWriteReopenStaadFile` |
| Opening and model waiting | `OpenSTAADFile`, `OpenStaadModel`, `AwaitModelHandle` |
| Application presentation | `ShowApplication`, `MinimizeStaadMainWindow`, `MaximizeStaadMainWindow`, `RestoreStaadMainWindow` |
| Analysis | `AnalyzeEx`, with `SilentMode`, `HiddenMode`, `WaitAnalysisMode` and `AnalysisResult` |

Filename identity does not establish geometry/load readiness. Saving, closing, opening, rewriting and analysis are explicit native mutations that can affect unsaved work, files or captured interfaces. Window requests do not independently verify final state or focus. Inspect analysis status explicitly; asynchronous return mode does not mean analysis has completed.

See the [root guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Porting.md) and [API reference](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/API-Reference.md) for complete contracts.

## IntelliSense and included documentation

Expanded XML IntelliSense is included for **140 geometry overloads, 78 load overloads and 22 root functions**, on both interfaces and concrete adapters. Keep `StaadPro.Interop.xml` beside the library when distributing assemblies outside normal NuGet restore.

The archive includes geometry, load and root guides, installation and publishing instructions, and the release history. The published package metadata identifies **Srinivasa Rao Masanam** as the author.

## Preview validation and operating limits

The current implementation passed **914 offline tests**. Installed-package verification checks **149 XML example blocks**, compiles a fresh consumer and exercises managed native stand-ins; window-changing examples are guarded off. These checks do not certify every native COM response, STAAD release or engineering-unit convention.

Silent and hidden options do not guarantee unattended operation. Synchronous native calls can block beyond logical polling budgets, and mutations can leave partial effects when a later step fails. Validate native behavior against controlled models and the STAAD versions used by your application before production deployment.

## Licence

MIT. Copyright (c) 2026 StaadDev. Bentley STAAD.Pro is a separate product.

For package submission and availability checks, see the [publishing guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Publishing.md).
