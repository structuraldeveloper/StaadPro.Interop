![StaadPro.Interop](https://raw.githubusercontent.com/structuraldeveloper/StaadPro.Interop/main/docs/images/logo-with-text-400.png)

# StaadPro.Interop

A strongly-typed .NET Framework wrapper for Bentley STAAD.Pro OpenSTAAD

## Version 1.0.0-preview.20261028.2

This preview ports ten further existing main-project Table functions for deletion, renaming, resizing, scoped saves and cell typography. Table now has 20 main-source overloads reaching 20/24 documented native names; nine main-source overloads remain. Complete comparative XML documentation explains native void completion, save scope, one-based coordinates, dimensions and font-size conventions.

The package reuses the established Table result/status types and internal service. It retains the compact Markdown banner, both original logos and independence/trademark notices. Native report effects, persistence and live COM compatibility remain method/build-specific acceptance work. Public NuGet.org availability of this prepared version is not confirmed.

Verified **6,780 offline tests** and **1,262 installed-package XML examples**, including forty Table examples and an isolated report lifecycle/typography workflow. The 332 Table cases include 145 additional cases in this increment; live COM, UI context and report persistence remain separate acceptance work.

By **Srinivasa Rao Masanam**.

StaadPro.Interop provides typed adapters and managed models for working with an existing Bentley STAAD.Pro application through OpenSTAAD. It includes geometry and load operations, model information and lifecycle functions, application-window controls, and native analysis requests.

This preview includes 249 common Property overloads and five optional catalog queries, all 171 current Load overloads, the completed reference Geometry ports, 96 managed Output overloads reaching 47 native names, 27 Support operations, seventeen Commands methods, ten Design methods and twenty Table methods, the reviewed Root APIs and approved wrapper surface, including separately acquired, version-aware export and analysis-message capabilities. See the [release notes](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Release-Notes.md) for the changes included in each version.

## Table lifecycle and formatting

New operations cover report/table deletion, renaming, growing existing table dimensions, saving all reports or one table, and cell bold/italic/underline/font sizing. All reuse the existing named receipt; normal native void completion does not certify success or persistence. See [exact contracts and examples](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Table-Lifecycle-and-Formatting.md).

## Earlier Table report foundations

The preceding foundation increment adapted ten existing functions from the read-only main Table adapter. Rows and columns are one-based; counts and cell strings return validated values directly. Creation/mutation receipts retain native creation IDs or explicit unacknowledged void completion. See the [Table guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Table-Report-Foundations.md). The latest increment brings the total to twenty implemented Table overloads; nine remain pending.

## Commands, Support and Design foundations

The package also includes the preceding foundation increment of 35 managed methods: seventeen analysis-command methods, eight Support foundation/assignment operations and ten common Design brief/assignment/query methods. Commands reaches sixteen active native analysis routes, Support covers 27/35 documented native routes, and Design reaches five native names. The package retains 96 managed Output overloads covering 47/60 documented native names, the existing Root/Geometry/Load/Property APIs and the approved wrapper surface.

The Release build passed with zero warnings/errors and 6,448 offline tests, including 412 new Commands/Support/Design cases. Exact-archive verification compiled and executed 1,222 selected XML examples through an isolated PackageReference consumer, including all 34 Commands, 54 Support and twenty Design examples. These checks validate managed behavior and packaging; live native COM and engineering qualification remain pending.

The symbol-only icon and logo-with-text banner are included as the adopted package branding. See [analysis commands and Support](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Commands-and-Support-Porting.md) and [Design contracts](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Design-Porting.md) for exact signatures, differences between related methods, status handling, units and examples.

## Earlier preview - plate-corner workflows

Added **25 Output overloads** for plate-corner extraction, indexed sparse/Cartesian selections, explicit metadata families and member-local projections: 18 synchronous main-source adaptations plus seven typed enabling overloads. Output now exposes **96 managed overloads reaching 47/60 documented native names**, with **13 documented native routes pending**. All additions use the newly enabled GetPlateCornerForces route. See [the plate-corner guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Output-Plate-Corners.md) for exact signatures, comparisons, axes evidence and limitations.

Passed a Release build with zero warnings/errors and **6,036 offline tests**, including **248 new corner cases**. Isolated package verification covers **1,152 selected XML examples**, including all 192 Output examples. Read-only native checks support the global force/moment contract on sampled STAAD.Pro 2026.0.0.340 observations; broader native/engineering qualification remains pending.

## Earlier source increment - managed Output workflows

Added **25 managed Output overloads**: 23 synchronous main-project workflow adaptations and two typed enabling overloads for explicit native axes and Beam ratio queries. Output now exposes **71 managed overloads reaching 46/60 documented native names**. The fourteen native contract gates remain; this increment adds compositions, entity conveniences and ordered batches over qualified native routes. See [the managed workflow guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Output-Managed-Workflows.md) for exact contracts and comparative usage.

Added governing-case resolution, per-item batch outcomes, staged caller utilization caches, native global offset-transfer audit packets, geometry bases, plate-center composites, ordered static checks and opposite support actions. All new APIs have matching XML documentation and examples; no public ref/out, tuples, partial adapters or thread-pool COM calls were introduced.

## Output steel-design blocks and Support foundations

Added **six common documented Output routes and 19 Support functions**, with full matching interface/adapter IntelliSense and named managed results. Output now covers **46/60** documented native names; Support covers **19/35**. The new Output overloads distinguish named parameter blocks from member-only observations, while Support preserves node/definition identity, six-DOF states and operation-specific native acknowledgements.

Passed a Release build with zero warnings/errors and **5,523 offline tests**, including 170 new cases. A separate local CI archive verifies **1,052 selected XML examples** using managed native-signature fixtures. Live 2024–2026 COM and engineering qualification remain pending. This source increment has not been published.

See [the Output steel-block guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Output-Steel-Design-Blocks.md) and [the Support foundation guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Support-Foundations.md).

## Earlier Property completion and extended Output queries

Property provides **249 common overloads plus five optional catalog queries**, covering **195/204** active documented native routes. The four new operations complete the documented P10 cleanup/profile-name allocation. Nine documented contract gates and fifteen supplemental discovery candidates remain separate work. See [the Property completion guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Property-Profile-Names-and-Final-Cleanup.md).

Output provides **40 typed queries**, including 25 new member, solid, support-node, modal, buckling, nonlinear, steel-design and time-history operations. Twenty documented native routes remain. See [the extended Output guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Output-Extended-Queries.md) for exact signatures and comparisons between similar methods.

The Release build and **5,353 offline tests** passed. The isolated package consumer verifies **1,002 selected XML examples**, including all 80 Output and 510 Property/capability examples. Native COM and engineering qualification remain pending. The library targets Windows/.NET Framework 4.8.1.

Public UPT methods use PascalCase, including `AddUptPropertyChannel`. See [the API rename table](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Property-API-Naming.md) when upgrading from an earlier preview.

## Earlier Property UPT profiles and catalogs

Incorporated **25 Property functions**: 22 reviewed P8 user-provided-table operations and the next three P9 catalog/database-folder operations. Property now exposes **225 overloads**, covering **166 of 204 active native routes**, with **38 remaining**. Five P8 transport contracts and four earlier P7 contracts remain pending. P8 has 25 of 30 routes; P9 has four of nine. See [all 25 UPT/catalog contracts](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Property-UPT-Profiles-and-Catalogs.md).

Passed a Release build with zero warnings/errors and **4,959 offline tests**, including **213 new cases**. The isolated PackageReference-only consumer verifies **892 selected XML examples**, including all **450 Property interface/adapter examples**. All sixteen new mutation examples are guarded; separate managed checks execute all 25 routes through both public surfaces. Live native mutation and engineering qualification remain pending.

Added immutable explicit UPT profile inputs, owned table inventories and named section observations. Kept operation-specific creation, insertion and removal outcomes separate. Qualified ten UPT read-back layouts and the database-folder setter against exact acquired native profiles 24.00.02.354, 25.00.01.424 and 26.00.00.340, retaining their different folder return conventions. Unreviewed builds cannot select those policies. Table numbers, native indices and property references retain separate identities.

## Earlier Property fireproofing, attributes and UPT

Incorporated **25 Property functions**: 22 reviewed P7 inactive/fireproofing/attribute operations and the first three P8 UPT operations. Property now exposes **200 overloads** and reaches **141 of 204 active native routes**, with **63 remaining**. P7 has 42 of 46 routes; four contract gaps remain explicitly pending. P8 has three of thirty routes. See [the 25 fireproofing/attribute/UPT contracts](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Property-Fireproofing-Attributes-and-UPT.md).

Passed a Release build with zero warnings/errors and **4,746 offline tests**, including **195 new cases**. The fresh PackageReference-only consumer verifies **842 selected XML examples**, including all **400 Property interface/adapter examples**. All ten new mutation examples are guarded; dedicated managed checks execute every new route through both public surfaces. Verification does not change a live STAAD model.

Added immutable exact attribute pairs, pair/domain/ID snapshots, fireproofing correspondence and explicit UPT value packets. Bounded count/list/count reads validate written outputs and preserve order/duplicates. By-reference native attribute-value rewriting returns StateChanged rather than data for the wrong pair. UPT table IDs, insertion acknowledgements and duplicate outcomes remain separate; scalar and unequal wide-flange routes retain their different return conventions.

## Earlier Property section and specification creation

Incorporated **25 Property functions**: the eight remaining P6 section creators and the next seventeen P7 specification/property operations. Property now exposes **175 overloads** and reaches **116 of 204 active native routes**, with **88 remaining**. All P2/P3/P4/P5/P6 allocations have managed routes; P7 has 20 of 46. Route coverage does not establish live native or engineering qualification. See [all 25 creation/specification contracts](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Property-Section-and-Specification-Creation.md).

Passed a Release build with zero warnings/errors and **4,551 offline tests**, including **205 new cases**. The isolated PackageReference-only consumer verifies **792 selected XML examples**, including all **350 Property interface/adapter examples**. All 24 new mutation examples are guarded; dedicated managed checks exercise every new route through both the interface and concrete adapter. Verification does not change a live STAAD model.

Added explicit immutable wide-flange, general-prismatic, six-DOF and partial-factor inputs. Retained separate created section IDs, created specification IDs, zero-based fireproofing indices and zero-success status-only results. The void unique-ID setter returns InvokedWithoutAcknowledgement with no fabricated native code and IsSuccessful false; its exact requested property/text identity is retained. Existing signatures, current-material context and native units remain explicit.

## Earlier Property material and member-section completion

Incorporated **25 Property functions**: the eight remaining P3 material operations, eight remaining P4 member-section queries and nine remaining P5 mutations. Property now exposes **150 overloads** and reaches **91 of 204 active native routes**, with **113 remaining**. All P2/P3/P4/P5 allocations have managed routes; this is implementation coverage, not live native or engineering qualification. See [the 25 material/member-section contracts](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Property-Materials-and-Member-Sections.md).

Passed a Release build with zero warnings/errors and **4,346 offline tests**, including **190 new cases**. The isolated PackageReference-only consumer verifies **742 selected XML examples**, including all **300 Property interface/adapter examples**. New mutation examples are guarded; separate installed-consumer checks invoke all eleven mutations against managed stand-ins. No live STAAD model is changed by verification.

Added immutable explicit material definitions/strengths, orthotropic constants, member-section snapshots and named mutation acknowledgements retaining actual native evidence and requested identity. Kept Steel Fu/Fy/Rt/Ry order distinct from extended Fy/Fu/Ry/Rt/Fcu order. Added bounded owned solid-ID snapshots, strict written-output checks, operation-specific absence/error handling and native-version section layouts. A successful mutation acknowledgement does not prove design-strength persistence, save the model or provide rollback.

## Primary and combination case enumeration fix

Fixed primary and combination discovery rejecting the Boolean `true` returned by `GetPrimaryLoadCaseNumbers` and `GetLoadCombinationCaseNumbers` on the observed STAAD.Pro 25.00.00.539 build. The shared readers now handle Boolean success separately from integral case counts, validate the complete ID array, and recheck the separate count before reading metadata. Boolean false, malformed arrays and inconsistent counts still fail. Reference discovery retains its integer-count contract; matching documented integral primary/combination results remain supported.

Passed a Release build with zero warnings/errors and **4,346 offline tests**, including the preceding **43 case-number return regressions**. Isolated PackageReference verification compiles and executes **742 selected XML examples**; the packaged primary and combination stand-ins now return Boolean success. Native return shapes were observed read-only during diagnosis; wider native/version and engineering qualification remains pending. Upgrade the consuming application to this exact package version and rebuild to load the corrected DLL. Existing Load API signatures are unchanged.

## Earlier Property section completion

Incorporated the remaining **19 active, nonconflicting main-interface Property signatures** plus **six documented native observations**, bringing Property to **125 overloads**. Added section creation/assignment/read-back, explicit material conversion and captured-receiver uniform-thickness creation with complete matching XML IntelliSense. Native dispatch now covers **66 of 204 active candidates**, with **138 remaining**. See [all 25 section-completion contracts](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Property-Sections.md).

Passed a Release build with zero warnings/errors and **4,746 offline tests**, including **225 new section cases**. Isolated installed-package verification compiles and runs **692 selected XML examples**, including all **250 Property interface/adapter examples**, through a fresh PackageReference-only consumer and cache. Managed stand-ins perform every example; live native mutation and engineering qualification remain pending.

Selected 47 reviewed section layouts per acquired STAAD 24/25/26 profile, reading native version metadata after wrapper initialization and rechecking it before returning packets. Corrected the source assignment query filtering an unfilled array. Preserved native identity/field labels, explicit missing-area results, operation-specific statuses and partial-write behavior. Added small section selection/data models without importing private catalog/UI services; reused the repository UnitsConverter for explicit modulus/weight-density conversion. Existing typed-return collisions (eight) and obsolete source overloads (five) remain documented exceptions.

## Earlier source — Load expansion

The current source includes 171 Load overloads, comprising all 115 main-reference signatures, five earlier conveniences, 15 inspection operations and 36 typed expansion methods. The expansion covers case/envelope/list mutations, automatic generation, new assignments, plate/floor and original-command readback, reference contributions and beam transfers, with comparative IntelliSense and 2,900 passing offline tests. GetLoadAttribute is status-only; MergeBeamLoads provides invocation evidence rather than certified success. The native and engineering qualification limits are described in the packaged docs/Load-Expansion.md guide. These Load additions are included in this locally prepared archive; it has not been uploaded to NuGet.org.

## Root and wrapper APIs included in this preview

This preview includes the completed wrapper bindings and five additional Root metadata queries: `GetApplicationVersion`, `GetBaseUnit`, `GetInputUnitForForce`, `GetInputUnitForLength` and `GetMainWindowHandle`. Wrapper-owned queries reuse cached properties; directly constructed Root adapters read their supplied native root. Unit compatibility fallbacks are documented in the [Root guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Porting.md). Analysis observation also adds `IsAnalyzing()`, `GetAnalysisStatus()` and `GetAnalysisStatus(string modelFilePath)`, with immutable status/data results and no public reference outputs. See the [analysis guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Analysis-Queries.md). Common reads also add `GetStaadFileFolder()`, `IsPhysicalModel()` and `GetErrorMessage()`; see the [common query guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Common-Queries.md). This preview also includes `GetProcessId()`, `SetInputUnitForLength(LengthInputUnit)`, `SetInputUnitForForce(ForceInputUnit)`, `SetShortJobInfo(...)` (including a ShortJobInfo overload) and `UpdateStructure()`. See the [model setup guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Model-Setup.md). These APIs are included in this preview archive.

Additional operations are `SetFullJobInfo(...)`, `Analyze()`, `AnalyzeModel(AnalysisEngine)`, `GetConnectedProjectInfo()` and `SetConnectedProjectInfo(...)`, including a project-data overload. They provide literal native date text, a typed analysis-engine selector and immutable project query results with no public reference outputs. Complete contracts, examples and native-validation limits are in the [job/analysis/project guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Job-Analysis-Projects.md). These APIs are included in this preview archive.

This preview also adds `NewSTAADFile(...)`, `CreateNamedView(string, int)`, `SaveNamedView(string)`, `GetProcessHandle()` and explicit `Quit()`. These include guarded native creation, inspected view signatures and opaque handle data. See the [creation/view/lifecycle guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Creation-Views-and-Lifecycle.md) for concurrency limits, native signature discrepancies, raw flag policy and shutdown behavior. These APIs are included in this preview archive.

The reviewed Root port is complete: all 39 reviewed native Root functions and all 23 private-source Root methods have managed mappings. The final additions include `RemoveNamedView`, `ModifyNamedView`, `RetrieveMemberForces`, and separately acquired `ExportModelToAdina`, `GetAnalysisErrorMessages` and `GetAnalysisWarningMessages`. Access optional operations through `Capabilities.GetAdinaExport()`, `GetAnalysisErrorMessages()` and `GetAnalysisWarningMessages()`. Exact candidate profiles and compatible member metadata are required. See the [completion and capability guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Root-Completion-and-Capabilities.md) for usage, named results, limits and native validation gates. The package exposes 53 common Root operations and three optional operations. Native runtime validation remains pending; capability availability is not runtime certification.

## Geometry, Load and Output included in this preview

Geometry exposes all **143 reference overloads**, including IdentifyPhysicalMembers, GroupPhysicalMembersByPlanAngle and GeneratePolygonMesh. See the [Geometry guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Geometry-Porting.md) for connectivity, managed grouping, mesh quality and native mutation contracts.

Load exposes **171 managed overloads**, including all 115 main-interface reference signatures, the existing conveniences, inspection queries and approved native expansion. Operations cover load cases, assignments/read-back, envelopes, environmental and dynamic loads. Further Load additions and qualification-driven changes remain parked for explicit review; source parity is not complete Bentley/runtime qualification. See the [Load guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Load-Porting.md), [implemented expansion](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Load-Expansion.md) and [approval checkpoint](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/reports/OpenSTAAD-Load-Expansion-Contracts-and-Type-Plan.md).

Output provides **96 managed overloads reaching 47 native names**, including parameter-block steel-design observations, model-wide failure ratios, member and physical-member actions, nodal and solid results, plate-center quantities, support-node observations, steel design, modal/buckling information and time histories. Named status/data results preserve native units and operation-specific failure distinctions. Inspect IsSuccessful before Data. See the [Output guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Output-Porting.md) and [extended-query contracts](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Output-Extended-Queries.md). Thirteen documented Output routes remain; live native qualification is pending. Support provides 27 common documented operations, with eight native contract gates pending; see the packaged Support foundation and Commands/Support guides.

The approved wrapper surface is complete. This archive includes all 249 common Property overloads and five acquired catalog queries described above. Commands provides seventeen analysis-configuration methods and Design provides ten common brief/assignment/query methods. Table, View and IDEA StatiCa remain binding/contract placeholders.

## Requirements

- Windows and **.NET Framework 4.8.1** (`net481`).
- A separately installed and licensed STAAD.Pro application exposing the OpenSTAAD functions used by your application.
- OpenSTAAD access from an appropriate STA thread, with shared model and application operations coordinated by the caller.

The package does not include Bentley executables, COM binaries or a STAAD.Pro licence. This distribution targets .NET Framework; it does not contain a .NET 8 library asset.

## Installation

After this version is available on NuGet.org:

```powershell
dotnet add package StaadPro.Interop --version 1.0.0-preview.20261026.4
```

For a .NET Framework project, the package reference is:

```xml
<PropertyGroup>
  <TargetFramework>net481</TargetFramework>
</PropertyGroup>
<ItemGroup>
  <PackageReference Include="StaadPro.Interop" Version="1.0.0-preview.20261026.4" />
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

Expanded XML IntelliSense is included for **143 Geometry overloads, 171 Load overloads, 249 common Property overloads plus five optional catalog queries, 96 Output overloads, 19 Support operations, 53 common Root operations and three optional Root operation interfaces**, on both interfaces and concrete adapters. Keep `StaadPro.Interop.xml` beside the library when distributing assemblies outside normal NuGet restore.

The archive includes Property, Geometry, Load, Output and Root guides, installation and publishing instructions, and the release history.

## Preview validation and operating limits

The current implementation passed **6,448 offline tests**. Installed-package verification checks **1,222 selected XML example blocks**, including 498 common Property, twelve optional catalog/acquisition, all 192 Output, 54 Support, 34 Commands and twenty Design examples, compiles a fresh consumer and exercises managed native stand-ins; mutation and window-changing examples are guarded off. These checks do not certify every native COM response, STAAD release or engineering-unit convention.

Silent and hidden options do not guarantee unattended operation. Synchronous native calls can block beyond logical polling budgets, and mutations can leave partial effects when a later step fails. Validate native behavior against controlled models and the STAAD versions used by your application before production deployment.

## Licence

MIT. Package copyright (c) 2026 TensorStruct. Bentley STAAD.Pro is a separate product.

For package submission and availability checks, see the [publishing guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Publishing.md).

## Independence and trademarks

StaadPro.Interop is an independent open-source project published by TensorStruct. Neither this project, its repository, nor its NuGet packages are affiliated with, sponsored by, endorsed by, or officially supported by Bentley Systems, Incorporated or its subsidiaries or affiliates.

Bentley, STAAD, STAAD.Pro and OpenSTAAD are trademarks or registered trademarks of Bentley Systems, Incorporated and/or its subsidiaries or affiliates. All other product names and trademarks belong to their respective owners. These names are used solely to identify the products with which this library interoperates; no trademark rights or endorsement are claimed.

The MIT license applies to this project's code. It does not grant a license to Bentley software or trademarks. A separately licensed STAAD.Pro installation is required for live use; no Bentley proprietary binaries are included.
