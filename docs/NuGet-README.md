# StaadPro.Interop

A strongly-typed .NET Framework wrapper for Bentley STAAD.Pro OpenSTAAD

## Version 1.0.0-preview.20261024.11

Package author: **Srinivasa Rao Masanam**. Company: **TensorStruct**. Copyright (c) 2026 TensorStruct.

StaadPro.Interop provides typed adapters and managed models for working with an existing Bentley STAAD.Pro application through OpenSTAAD. It includes geometry and load operations, model information and lifecycle functions, application-window controls, and native analysis requests.

This preview includes 245 common Property overloads and five optional catalog queries, all 171 current Load overloads, the completed reference Geometry ports, 15 typed Output queries, the reviewed Root APIs and approved wrapper surface, including separately acquired, version-aware export and analysis-message capabilities. See the [release notes](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Release-Notes.md) for the changes included in each version.

## Property catalog capabilities and cleanup

Incorporated **25 Property operations**: all five remaining P9 catalog queries and twenty P10 removal/deletion functions. The current surface has **245 common overloads plus five separately acquired catalog queries**, covering **191 of 204 active native routes**, with **13 remaining**. P9 is complete; P10 has twenty of twenty-four routes. The nine earlier P7/P8 contract gaps remain pending. See [all 25 catalog/cleanup contracts](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Property-Catalog-Capabilities-and-Cleanup.md).

Passed a Release build with zero warnings/errors and **5,156 offline tests**, including **197 new cases**. The isolated PackageReference-only consumer verifies **944 selected XML examples**: 490 common Property examples plus twelve optional catalog/acquisition examples, alongside the existing groups. Separate managed checks exercise all twenty new mutations and five catalog queries through both public surfaces. Live native mutation and engineering qualification remain pending.

Added a passive version/signature acquisition boundary for the newer catalog group on exact native profiles **25.00.01.424** and **26.00.00.340**. Retained operation-specific zero-success, Boolean-success and explicit no-specification removal outcomes, owned bounded bulk targets, and separate member-end, cable-definition and analytical-node selectors. No native model is modified by verification.

Renamed **17 public UPT methods** to PascalCase, including **AddUptPropertyChannel**, **AddUptPropertyWideFlange** and **CreateUptTable**. This preview requires callers to update those method names. Parameter names, types, ordering, defaults and return types are preserved. Bentley native dispatch names and recorded contract evidence retain their original spelling.

Authors remains exactly **Srinivasa Rao Masanam**. Company and copyright remain **TensorStruct**. README prose retains neutral bold API references and standard documentation links. See [the API rename table](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Property-API-Naming.md).

## Earlier Property UPT profiles and catalogs

Incorporated **25 Property functions**: 22 reviewed P8 user-provided-table operations and the next three P9 catalog/database-folder operations. Property now exposes **225 overloads**, covering **166 of 204 active native routes**, with **38 remaining**. Five P8 transport contracts and four earlier P7 contracts remain pending. P8 has 25 of 30 routes; P9 has four of nine. See [all 25 UPT/catalog contracts](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Property-UPT-Profiles-and-Catalogs.md).

Passed a Release build with zero warnings/errors and **4,959 offline tests**, including **213 new cases**. The isolated PackageReference-only consumer verifies **892 selected XML examples**, including all **450 Property interface/adapter examples**. All sixteen new mutation examples are guarded; separate managed checks execute all 25 routes through both public surfaces. Live native mutation and engineering qualification remain pending.

Added immutable explicit UPT profile inputs, owned table inventories and named section observations. Kept operation-specific creation, insertion and removal outcomes separate. Qualified ten UPT read-back layouts and the database-folder setter against exact acquired native profiles 24.00.02.354, 25.00.01.424 and 26.00.00.340, retaining their different folder return conventions. Unreviewed builds cannot select those policies. Table numbers, native indices and property references retain separate identities.

Replaced legacy publisher branding throughout the package with **TensorStruct**, including assembly Company, copyright and shipped documentation. Package Authors remains exactly **Srinivasa Rao Masanam**. Registry ownership remains separately managed.

## Earlier package copyright update

Updated package and assembly copyright to **Copyright (c) 2026 TensorStruct**. Authors remains **Srinivasa Rao Masanam** and assembly Company remains **TensorStruct**. Updated README API references to neutral bold text with standard blue documentation links, removing inline-code badges from prose. Retained the preceding preview's API surface, runnable fenced examples and XML IntelliSense.

Rebuilt the Release package with zero warnings/errors. Verified the generated manifest and installed assembly copyright, exact Authors/Company metadata, DLL/XML and shipped guides through a fresh PackageReference-only consumer, including **842 selected XML examples**. The preceding source regression record remains 4,746 passing offline tests; this metadata update adds no API behavior.

## Earlier Property fireproofing, attributes and UPT

Incorporated **25 Property functions**: 22 reviewed P7 inactive/fireproofing/attribute operations and the first three P8 UPT operations. Property now exposes **200 overloads** and reaches **141 of 204 active native routes**, with **63 remaining**. P7 has 42 of 46 routes; four contract gaps remain explicitly pending. P8 has three of thirty routes. See [the 25 fireproofing/attribute/UPT contracts](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Property-Fireproofing-Attributes-and-UPT.md).

Passed a Release build with zero warnings/errors and **4,746 offline tests**, including **195 new cases**. The fresh PackageReference-only consumer verifies **842 selected XML examples**, including all **400 Property interface/adapter examples**. All ten new mutation examples are guarded; dedicated managed checks execute every new route through both public surfaces. Verification does not change a live STAAD model.

Added immutable exact attribute pairs, pair/domain/ID snapshots, fireproofing correspondence and explicit UPT value packets. Bounded count/list/count reads validate written outputs and preserve order/duplicates. By-reference native attribute-value rewriting returns StateChanged rather than data for the wrong pair. UPT table IDs, insertion acknowledgements and duplicate outcomes remain separate; scalar and unequal wide-flange routes retain their different return conventions.

Package Authors remains exactly **Srinivasa Rao Masanam**; company and copyright remain **TensorStruct**. Existing author packing/verification safeguards remain in place.

## Earlier Property section and specification creation

Incorporated **25 Property functions**: the eight remaining P6 section creators and the next seventeen P7 specification/property operations. Property now exposes **175 overloads** and reaches **116 of 204 active native routes**, with **88 remaining**. All P2/P3/P4/P5/P6 allocations have managed routes; P7 has 20 of 46. Route coverage does not establish live native or engineering qualification. See [all 25 creation/specification contracts](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Property-Section-and-Specification-Creation.md).

Passed a Release build with zero warnings/errors and **4,551 offline tests**, including **205 new cases**. The isolated PackageReference-only consumer verifies **792 selected XML examples**, including all **350 Property interface/adapter examples**. All 24 new mutation examples are guarded; dedicated managed checks exercise every new route through both the interface and concrete adapter. Verification does not change a live STAAD model.

Added explicit immutable wide-flange, general-prismatic, six-DOF and partial-factor inputs. Retained separate created section IDs, created specification IDs, zero-based fireproofing indices and zero-success status-only results. The void unique-ID setter returns InvokedWithoutAcknowledgement with no fabricated native code and IsSuccessful false; its exact requested property/text identity is retained. Existing signatures, current-material context and native units remain explicit.

Package Authors remains exactly **Srinivasa Rao Masanam**. Company and copyright remain **TensorStruct**. Packing rejects a different Authors value, and installed-package verification checks the exact manifest value. NuGet.org registry ownership is managed separately.

## Earlier Property material and member-section completion

Incorporated **25 Property functions**: the eight remaining P3 material operations, eight remaining P4 member-section queries and nine remaining P5 mutations. Property now exposes **150 overloads** and reaches **91 of 204 active native routes**, with **113 remaining**. All P2/P3/P4/P5 allocations have managed routes; this is implementation coverage, not live native or engineering qualification. See [the 25 material/member-section contracts](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Property-Materials-and-Member-Sections.md).

Passed a Release build with zero warnings/errors and **4,346 offline tests**, including **190 new cases**. The isolated PackageReference-only consumer verifies **742 selected XML examples**, including all **300 Property interface/adapter examples**. New mutation examples are guarded; separate installed-consumer checks invoke all eleven mutations against managed stand-ins. No live STAAD model is changed by verification.

Added immutable explicit material definitions/strengths, orthotropic constants, member-section snapshots and named mutation acknowledgements retaining actual native evidence and requested identity. Kept Steel Fu/Fy/Rt/Ry order distinct from extended Fy/Fu/Ry/Rt/Fcu order. Added bounded owned solid-ID snapshots, strict written-output checks, operation-specific absence/error handling and native-version section layouts. A successful mutation acknowledgement does not prove design-strength persistence, save the model or provide rollback.

## Primary and combination case enumeration fix

Fixed primary and combination discovery rejecting the Boolean **true** returned by **GetPrimaryLoadCaseNumbers** and **GetLoadCombinationCaseNumbers** on the observed STAAD.Pro 25.00.00.539 build. The shared readers now handle Boolean success separately from integral case counts, validate the complete ID array, and recheck the separate count before reading metadata. Boolean false, malformed arrays and inconsistent counts still fail. Reference discovery retains its integer-count contract; matching documented integral primary/combination results remain supported.

Passed a Release build with zero warnings/errors and **4,346 offline tests**, including the preceding **43 case-number return regressions**. Isolated PackageReference verification compiles and executes **742 selected XML examples**; the packaged primary and combination stand-ins now return Boolean success. Native return shapes were observed read-only during diagnosis; wider native/version and engineering qualification remains pending. Upgrade the consuming application to this exact package version and rebuild to load the corrected DLL. Existing Load API signatures are unchanged.

## Earlier Property section completion

Incorporated the remaining **19 active, nonconflicting main-interface Property signatures** plus **six documented native observations**, bringing Property to **125 overloads**. Added section creation/assignment/read-back, explicit material conversion and captured-receiver uniform-thickness creation with complete matching XML IntelliSense. Native dispatch now covers **66 of 204 active candidates**, with **138 remaining**. See [all 25 section-completion contracts](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Property-Sections.md).

Passed a Release build with zero warnings/errors and **4,746 offline tests**, including **225 new section cases**. Isolated installed-package verification compiles and runs **692 selected XML examples**, including all **250 Property interface/adapter examples**, through a fresh PackageReference-only consumer and cache. Managed stand-ins perform every example; live native mutation and engineering qualification remain pending.

Selected 47 reviewed section layouts per acquired STAAD 24/25/26 profile, reading native version metadata after wrapper initialization and rechecking it before returning packets. Corrected the source assignment query filtering an unfilled array. Preserved native identity/field labels, explicit missing-area results, operation-specific statuses and partial-write behavior. Added small section selection/data models without importing private catalog/UI services; reused the repository UnitsConverter for explicit modulus/weight-density conversion. Existing typed-return collisions (eight) and obsolete source overloads (five) remain documented exceptions.

## Earlier source — Load expansion

The current source includes 171 Load overloads, comprising all 115 main-reference signatures, five earlier conveniences, 15 inspection operations and 36 typed expansion methods. The expansion covers case/envelope/list mutations, automatic generation, new assignments, plate/floor and original-command readback, reference contributions and beam transfers, with comparative IntelliSense and 2,900 passing offline tests. GetLoadAttribute is status-only; MergeBeamLoads provides invocation evidence rather than certified success. The native and engineering qualification limits are described in the packaged docs/Load-Expansion.md guide. These Load additions are included in this locally prepared archive; it has not been uploaded to NuGet.org.

## Root and wrapper APIs included in this preview

This preview includes the completed wrapper bindings and five additional Root metadata queries: **GetApplicationVersion**, **GetBaseUnit**, **GetInputUnitForForce**, **GetInputUnitForLength** and **GetMainWindowHandle**. Wrapper-owned queries reuse cached properties; directly constructed Root adapters read their supplied native root. Unit compatibility fallbacks are documented in the [Root guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Porting.md). Analysis observation also adds **IsAnalyzing()**, **GetAnalysisStatus()** and **GetAnalysisStatus(string modelFilePath)**, with immutable status/data results and no public reference outputs. See the [analysis guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Analysis-Queries.md). Common reads also add **GetStaadFileFolder()**, **IsPhysicalModel()** and **GetErrorMessage()**; see the [common query guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Common-Queries.md). This preview also includes **GetProcessId()**, **SetInputUnitForLength(LengthInputUnit)**, **SetInputUnitForForce(ForceInputUnit)**, **SetShortJobInfo(...)** (including a ShortJobInfo overload) and **UpdateStructure()**. See the [model setup guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Model-Setup.md). These APIs are included in this preview archive.

Additional operations are **SetFullJobInfo(...)**, **Analyze()**, **AnalyzeModel(AnalysisEngine)**, **GetConnectedProjectInfo()** and **SetConnectedProjectInfo(...)**, including a project-data overload. They provide literal native date text, a typed analysis-engine selector and immutable project query results with no public reference outputs. Complete contracts, examples and native-validation limits are in the [job/analysis/project guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Job-Analysis-Projects.md). These APIs are included in this preview archive.

This preview also adds **NewSTAADFile(...)**, **CreateNamedView(string, int)**, **SaveNamedView(string)**, **GetProcessHandle()** and explicit **Quit()**. These include guarded native creation, inspected view signatures and opaque handle data. See the [creation/view/lifecycle guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Creation-Views-and-Lifecycle.md) for concurrency limits, native signature discrepancies, raw flag policy and shutdown behavior. These APIs are included in this preview archive.

The reviewed Root port is complete: all 39 reviewed native Root functions and all 23 private-source Root methods have managed mappings. The final additions include **RemoveNamedView**, **ModifyNamedView**, **RetrieveMemberForces**, and separately acquired **ExportModelToAdina**, **GetAnalysisErrorMessages** and **GetAnalysisWarningMessages**. Access optional operations through **Capabilities.GetAdinaExport()**, **GetAnalysisErrorMessages()** and **GetAnalysisWarningMessages()**. Exact candidate profiles and compatible member metadata are required. See the [completion and capability guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Completion-and-Capabilities.md) for usage, named results, limits and native validation gates. The package exposes 53 common Root operations and three optional operations. Native runtime validation remains pending; capability availability is not runtime certification.

## Geometry, Load and Output included in this preview

Geometry exposes all **143 reference overloads**, including IdentifyPhysicalMembers, GroupPhysicalMembersByPlanAngle and GeneratePolygonMesh. See the [Geometry guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Geometry-Porting.md) for connectivity, managed grouping, mesh quality and native mutation contracts.

Load exposes **120 overloads: all 115 main-interface signatures plus five conveniences**. Included operations cover load cases, assignments and read-back, population/grouping, envelopes, wind/seismic definitions, response spectra and direct-analysis parameters. Reference-source parity is complete; it does not establish complete Bentley Load API coverage. Of 56 undispatched native names, one has an alternative and one is derivable; four have partial coverage and 50 have no managed equivalent found. See the [Load guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Load-Porting.md) and [semantic coverage audit](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/reports/OpenSTAAD-Load-Managed-Capability-Audit.md). Those additional scopes were not implemented in this preview.

Output provides **15 typed queries** for analysis/buckling result availability, node displacements, support reactions, local member-end forces, static checks, seven plate-center operations, member design section names and steel utilization ratios. Named status/data results preserve native units, documented failure/sentinel distinctions and immutable owned data. Inspect IsSuccessful before Data. These observations do not run analysis or establish result freshness. See the [Output guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Output-Porting.md). Forty-five documented native Output mappings remain pending.

The approved wrapper surface is complete. This archive includes all 245 common Property overloads and five acquired catalog queries described above. Support, Design, Commands, Table, View and IDEA StatiCa remain binding/contract placeholders.

## Requirements

- Windows and **.NET Framework 4.8.1** (**net481**).
- A separately installed and licensed STAAD.Pro application exposing the OpenSTAAD functions used by your application.
- OpenSTAAD access from an appropriate STA thread, with shared model and application operations coordinated by the caller.

The package does not include Bentley executables, COM binaries or a STAAD.Pro licence. This distribution targets .NET Framework; it does not contain a .NET 8 library asset.

## Installation

After this version is available on NuGet.org:

```powershell
dotnet add package StaadPro.Interop --version 1.0.0-preview.20261024.9
```

For a .NET Framework project, the package reference is:

```xml
<PropertyGroup>
  <TargetFramework>net481</TargetFramework>
</PropertyGroup>
<ItemGroup>
  <PackageReference Include="StaadPro.Interop" Version="1.0.0-preview.20261024.9" />
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

**IOSGeometry** and **OSGeometryAdapter** expose typed geometry operations. Managed connectivity grouping is available through **SegregateBeamsByConnectivity**, **SegregateMembersByConnectivity** and **SegregatePlatesByConnectivity**, including the standalone **ConnectivityService**.

Extended root-bound operations include **CreateMultipleNodesExtended**, **CreateMultipleBeamsExtended** and **MergeNodesExtended**. These operations use the supplied root session; mutations can have partial native effects and do not automatically refresh managed geometry or roll back earlier changes.

See the [geometry guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Geometry-Porting.md) for signatures, connectivity rules and mutation contracts.

## Load assignments and metadata

Assignment readers include:

- **GetNodalLoads**
- **GetMemberUniformlyDistributedLoads**
- **GetMemberConcentratedLoads**
- **GetMemberConcentratedMoments**
- **GetMemberLinearVaryingLoads**
- **GetMemberTrapezoidalLoads**
- **GetMemberUniformMoments**

Returned values preserve native signs and units. Assignment reads can activate the selected case; they are distinct from analysis-result retrieval.

Case discovery and filtering include **GetAllPrimaryLoadCases**, **GetAllReferenceLoadCases**, **GetAllLoadCombinationCases**, **GetRepeatLoadCases**, **GetAllLoadCasesOfType**, **GetLoadCase**, **GetLoadCases**, **GetLoadCasesFromIds** and **GetLoadCasesOfType**. Direct and collection lookups include **GetPrimaryLoadCaseFromId**, **GetReferenceLoadCaseFromId**, **GetLoadCombinationCaseFromId**, **GetPrimaryLoadCasesFromIds**, **GetReferenceLoadCasesFromIds** and **GetLoadCombinationCasesFromIds**.

Envelope queries include **GetEnvelopeIds**, **GetLoadEnvelopeDetail** and **GetAllLoadEnvelopeDetails**. Combination definition read-back is available through **GetLoadCombination**.

Assignment and creation operations include **AddReferenceLoadsTo**, **AddRepeatLoadsTo**, **AddAxialThermalGradientTo**, **AddSelfweightInXYZTo**, **CreateLoadCombinations**, **CreateLoadEnvelopExt** and **CreateLoadList**. Grouped operations include **GetMemberLoadsGroupedByLoadCase**, **GetNodalLoadsGroupedByLoadCase**, **AddMemberLoadsFromGroupsByLoadCase** and **AddNodalLoadsFromGroupsByLoadCase**.

Managed member-load population is available through **PopulateMemberConcentratedLoads**, **PopulateMemberConcentratedMoments**, **PopulateMemberLinearVaryingLoads**, **PopulateMemberTrapezoidalLoads** and **PopulateMemberLoads**. Individual-family population and full refresh have different append/replacement behavior; see their XML documentation before combining calls.

Additional included operations are:

| Area | Managed operations |
| --- | --- |
| Population | **PopulateMemberUniformlyDistributedLoads**, **PopulateMemberUniformMoments**, **PopulateNodalLoads** |
| Wind definitions and profiles | **AddWindDefinition**, **AddWindExposure**, **AddWindIntensity**, **DeleteWindDefinition**, **DeleteAllWindDefinitions**, **AddWindDefinitionASCE7Parameters**, **ComputeWallWindPressureProfile**, **ComputeWallWindPressureProfileASCE72016** |
| Seismic definitions and weights | **AddSeismicDefinition**, **ModifySeismicDefinitionParam**, **ModifySeismicDefinitionParams**, **AddSeismicDefSelfWeight**, **AddSeismicDefJointWeight**, **AddSeismicDefMemberWeight**, **AddSeismicDefMemberUniformWeight**, **AddSeismicDefMemberConcentratedWeight**, **AddSeismicDefWallArea** |
| Response spectra | **AddRSLoad**, **AddResponseSpectrumLoadEx** |
| Direct analysis | **AddDirectAnalysisDefinitionParameter**, **AddDirectAnalysisFlexParameter**, **AddDirectAnalysisAxialParameter**, **DeleteDirectAnalysisDefinitionParameter**, **DeleteDirectAnalysisDefinition** |

The [load-query guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Load-Queries.md) and [load-operation guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Load-Porting.md) explain native mappings, activation, validation, units and partial-failure behavior.

## Model and application operations

**OpenStaadWrapper.Root** exposes **IOSRoot**, implemented by **OSRootAdapter**.

| Area | Functions |
| --- | --- |
| Filenames and model identity | **GetStaadFileFullPath**, **GetStaadFileNameOnly**, **GetAnlFileFullPath**, **GetOpenedStaadFilename**, **IsTarget**, **ModelIsOpen** |
| Job information and units | **GetShortJobInfo**, **GetFullJobInfo**, **SetInputUnitsEx** |
| Saving and closing | **SetSilentMode**, **SaveModel**, **CloseSTAADFile**, **CloseCurrentStaadModel**, **CloseWriteReopenStaadFile** |
| Opening and model waiting | **OpenSTAADFile**, **OpenStaadModel**, **AwaitModelHandle** |
| Application presentation | **ShowApplication**, **MinimizeStaadMainWindow**, **MaximizeStaadMainWindow**, **RestoreStaadMainWindow** |
| Analysis | **AnalyzeEx**, with **SilentMode**, **HiddenMode**, **WaitAnalysisMode** and **AnalysisResult** |

Filename identity does not establish geometry/load readiness. Saving, closing, opening, rewriting and analysis are explicit native mutations that can affect unsaved work, files or captured interfaces. Window requests do not independently verify final state or focus. Inspect analysis status explicitly; asynchronous return mode does not mean analysis has completed.

See the [root guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Porting.md) and [API reference](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/API-Reference.md) for complete contracts.

## IntelliSense and included documentation

Expanded XML IntelliSense is included for **143 Geometry overloads, 171 Load overloads, 245 common Property overloads plus five optional catalog queries, 15 Output queries, 53 common Root operations and three optional Root operation interfaces**, on both interfaces and concrete adapters. Keep **StaadPro.Interop.xml** beside the library when distributing assemblies outside normal NuGet restore.

The archive includes Property, Geometry, Load, Output and Root guides, installation and publishing instructions, and the release history. The package metadata identifies **Srinivasa Rao Masanam** as the author; company is **TensorStruct** and copyright is **TensorStruct**. NuGet.org Owners are managed separately by the registry.

## Preview validation and operating limits

The current implementation passed **5,156 offline tests**. Installed-package verification checks **944 selected XML example blocks**, including 490 common Property and twelve optional catalog/acquisition examples, compiles a fresh consumer and exercises managed native stand-ins; window-changing examples are guarded off. These checks do not certify every native COM response, STAAD release or engineering-unit convention.

Silent and hidden options do not guarantee unattended operation. Synchronous native calls can block beyond logical polling budgets, and mutations can leave partial effects when a later step fails. Validate native behavior against controlled models and the STAAD versions used by your application before production deployment.

## Licence

MIT. Package copyright (c) 2026 TensorStruct. Bentley STAAD.Pro is a separate product.

For package submission and availability checks, see the [publishing guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Publishing.md).
