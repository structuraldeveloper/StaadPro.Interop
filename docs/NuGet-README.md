# StaadPro.Interop

A strongly-typed .NET Framework wrapper for Bentley STAAD.Pro OpenSTAAD

## Version 1.0.0-preview.20261023

StaadPro.Interop provides typed adapters and managed models for working with an existing Bentley STAAD.Pro application through OpenSTAAD. It includes geometry and load operations, model information and lifecycle functions, application-window controls, and native analysis requests.

This preview includes the completed reference Geometry and Load ports, 15 typed Output queries, the reviewed Root APIs and approved wrapper surface, including separately acquired, version-aware export and analysis-message capabilities. See the [release notes](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Release-Notes.md) for the changes included in each version.

## Root and wrapper APIs included in this preview

This preview includes the completed wrapper bindings and five additional Root metadata queries: `GetApplicationVersion`, `GetBaseUnit`, `GetInputUnitForForce`, `GetInputUnitForLength` and `GetMainWindowHandle`. Wrapper-owned queries reuse cached properties; directly constructed Root adapters read their supplied native root. Unit compatibility fallbacks are documented in the [Root guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Porting.md). Analysis observation also adds `IsAnalyzing()`, `GetAnalysisStatus()` and `GetAnalysisStatus(string modelFilePath)`, with immutable status/data results and no public reference outputs. See the [analysis guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Analysis-Queries.md). Common reads also add `GetStaadFileFolder()`, `IsPhysicalModel()` and `GetErrorMessage()`; see the [common query guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Common-Queries.md). This preview also includes `GetProcessId()`, `SetInputUnitForLength(LengthInputUnit)`, `SetInputUnitForForce(ForceInputUnit)`, `SetShortJobInfo(...)` (including a ShortJobInfo overload) and `UpdateStructure()`. See the [model setup guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Model-Setup.md). These APIs are included in this preview archive.

Additional operations are `SetFullJobInfo(...)`, `Analyze()`, `AnalyzeModel(AnalysisEngine)`, `GetConnectedProjectInfo()` and `SetConnectedProjectInfo(...)`, including a project-data overload. They provide literal native date text, a typed analysis-engine selector and immutable project query results with no public reference outputs. Complete contracts, examples and native-validation limits are in the [job/analysis/project guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Job-Analysis-Projects.md). These APIs are included in this preview archive.

This preview also adds `NewSTAADFile(...)`, `CreateNamedView(string, int)`, `SaveNamedView(string)`, `GetProcessHandle()` and explicit `Quit()`. These include guarded native creation, inspected view signatures and opaque handle data. See the [creation/view/lifecycle guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Creation-Views-and-Lifecycle.md) for concurrency limits, native signature discrepancies, raw flag policy and shutdown behavior. These APIs are included in this preview archive.

The reviewed Root port is complete: all 39 reviewed native Root functions and all 23 private-source Root methods have managed mappings. The final additions include `RemoveNamedView`, `ModifyNamedView`, `RetrieveMemberForces`, and separately acquired `ExportModelToAdina`, `GetAnalysisErrorMessages` and `GetAnalysisWarningMessages`. Access optional operations through `Capabilities.GetAdinaExport()`, `GetAnalysisErrorMessages()` and `GetAnalysisWarningMessages()`. Exact candidate profiles and compatible member metadata are required. See the [completion and capability guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Completion-and-Capabilities.md) for usage, named results, limits and native validation gates. The package exposes 53 common Root operations and three optional operations. Native runtime validation remains pending; capability availability is not runtime certification.

## Geometry, Load and Output included in this preview

Geometry exposes all **143 reference overloads**, including IdentifyPhysicalMembers, GroupPhysicalMembersByPlanAngle and GeneratePolygonMesh. See the [Geometry guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Geometry-Porting.md) for connectivity, managed grouping, mesh quality and native mutation contracts.

Load exposes **120 overloads: all 115 main-interface signatures plus five conveniences**. Included operations cover load cases, assignments and read-back, population/grouping, envelopes, wind/seismic definitions, response spectra and direct-analysis parameters. Reference-source parity is complete; it does not establish complete Bentley Load API coverage. Of 56 undispatched native names, one has an alternative and one is derivable; four have partial coverage and 50 have no managed equivalent found. See the [Load guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Load-Porting.md) and [semantic coverage audit](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/reports/OpenSTAAD-Load-Managed-Capability-Audit.md). Those additional scopes were not implemented in this preview.

Output provides **15 typed queries** for analysis/buckling result availability, node displacements, support reactions, local member-end forces, static checks, seven plate-center operations, member design section names and steel utilization ratios. Named status/data results preserve native units, documented failure/sentinel distinctions and immutable owned data. Inspect IsSuccessful before Data. These observations do not run analysis or establish result freshness. See the [Output guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Output-Porting.md). Forty-five documented native Output mappings remain pending.

The approved wrapper surface is complete. Property, Support, Design, Commands, Table, View and IDEA StatiCa remain binding/contract placeholders; their domain implementations are separate work.

## Requirements

- Windows and **.NET Framework 4.8.1** (`net481`).
- A separately installed and licensed STAAD.Pro application exposing the OpenSTAAD functions used by your application.
- OpenSTAAD access from an appropriate STA thread, with shared model and application operations coordinated by the caller.

The package does not include Bentley executables, COM binaries or a STAAD.Pro licence. This distribution targets .NET Framework; it does not contain a .NET 8 library asset.

## Installation

After this version is available on NuGet.org:

```powershell
dotnet add package StaadPro.Interop --version 1.0.0-preview.20261023
```

For a .NET Framework project, the package reference is:

```xml
<PropertyGroup>
  <TargetFramework>net481</TargetFramework>
</PropertyGroup>
<ItemGroup>
  <PackageReference Include="StaadPro.Interop" Version="1.0.0-preview.20261023" />
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

Additional included operations are:

| Area | Managed operations |
| --- | --- |
| Population | `PopulateMemberUniformlyDistributedLoads`, `PopulateMemberUniformMoments`, `PopulateNodalLoads` |
| Wind definitions and profiles | `AddWindDefinition`, `AddWindExposure`, `AddWindIntensity`, `DeleteWindDefinition`, `DeleteAllWindDefinitions`, `AddWindDefinitionASCE7Parameters`, `ComputeWallWindPressureProfile`, `ComputeWallWindPressureProfileASCE72016` |
| Seismic definitions and weights | `AddSeismicDefinition`, `ModifySeismicDefinitionParam`, `ModifySeismicDefinitionParams`, `AddSeismicDefSelfWeight`, `AddSeismicDefJointWeight`, `AddSeismicDefMemberWeight`, `AddSeismicDefMemberUniformWeight`, `AddSeismicDefMemberConcentratedWeight`, `AddSeismicDefWallArea` |
| Response spectra | `AddRSLoad`, `AddResponseSpectrumLoadEx` |
| Direct analysis | `AddDirectAnalysisDefinitionParameter`, `AddDirectAnalysisFlexParameter`, `AddDirectAnalysisAxialParameter`, `DeleteDirectAnalysisDefinitionParameter`, `DeleteDirectAnalysisDefinition` |

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

Expanded XML IntelliSense is included for **143 Geometry overloads, 120 Load overloads, 15 Output queries, 53 common Root operations and three optional Root operation interfaces**, on both interfaces and concrete adapters. Keep `StaadPro.Interop.xml` beside the library when distributing assemblies outside normal NuGet restore.

The archive includes Geometry, Load, Output and Root guides, installation and publishing instructions, and the release history. The package metadata identifies **Srinivasa Rao Masanam** as the author.

## Preview validation and operating limits

The current implementation passed **2,259 offline tests**. Installed-package verification checks **340 selected XML example blocks**, compiles a fresh consumer and exercises managed native stand-ins; window-changing examples are guarded off. These checks do not certify every native COM response, STAAD release or engineering-unit convention.

Silent and hidden options do not guarantee unattended operation. Synchronous native calls can block beyond logical polling budgets, and mutations can leave partial effects when a later step fails. Validate native behavior against controlled models and the STAAD versions used by your application before production deployment.

## Licence

MIT. Copyright (c) 2026 StaadDev. Bentley STAAD.Pro is a separate product.

For package submission and availability checks, see the [publishing guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Publishing.md).
