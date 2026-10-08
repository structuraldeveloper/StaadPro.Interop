# Getting Started with StaadPro.Interop

## Overview & Prerequisites

StaadPro.Interop provides a modern, strongly-typed .NET interface for Bentley STAAD.Pro.

- **Target Framework**: .NET Framework 4.8.1 (`net481`) on Windows. There is currently no .NET 8 target.
- **Runtime**: A licensed STAAD.Pro installation exposing the OpenSTAAD signatures used by your application.

> [!IMPORTANT]
> **STAAD.Pro version compatibility**: Runtime dynamic dispatch removes build-time Bentley references; it does not certify API compatibility across versions. Verify the methods you use on the installed version. The load query port has offline tests and package checks; live version and unit verification remains pending. See [NuGet readiness](NuGet-Readiness.md).

---

## 1. Standard (Default) Usage: Dynamic Decoupled Interop

By default, `StaadPro.Interop` uses runtime COM dynamic dispatch via `OpenStaadWrapperProvider`. This provides major benefits:
- **Zero build-time dependencies**: You do not need Bentley DLLs (`StaadPro.dll`, `OpenSTAADUI.dll`) or registered TypeLibs to compile your projects or run automated CI/CD pipelines.
- **Runtime dispatch**: The binary resolves methods on the installed OpenSTAAD interface. Missing signatures propagate as runtime errors; test each version you intend to support.

```csharp
using StaadPro.Interop.Services;
using StaadPro.Interop.Models;

// Attach to active STAAD instance or open specified model
using (OpenStaadWrapper wrapper = OpenStaadWrapperProvider.Get(@"C:\Models\Structure.std"))
{
    // Access Geometry & Load adapters
    var geometry = wrapper.Geometry;
    var load = wrapper.Load;

    int nodeCount = geometry.GetAllNodesList().Count;
    Console.WriteLine($"Total Nodes: {nodeCount}");
}
```

---

## 2. Optional: Adding Direct `COMFileReference` Based on Installed STAAD Version

If your project requires early-bound COM type library references (`<COMFileReference>`), you can reference the `StaadPro.dll` corresponding to the installed STAAD version on your machine.

### Step 2.1: Locate Your Installed STAAD Version

You can identify the installed STAAD directory using either the Windows Registry or standard filesystem locations:

#### Option A: Via Windows Registry (Authoritative)
1. Open PowerShell and run:
   ```powershell
   Get-ItemProperty 'HKLM:\SOFTWARE\Bentley\Installed_Products\*' | Where-Object DisplayProductName -like '*STAAD.Pro*' | Select-Object DisplayProductName, Version, InstallDir
   ```
2. The output displays the installed product name, version number, and base installation folder (e.g. `C:\Program Files\Bentley\Engineering\STAAD.Pro 2025\`).

#### Option B: Standard Filesystem Installation Paths
| STAAD Version | Typical Installation Path |
| :--- | :--- |
| **STAAD.Pro 2025** | `C:\Program Files\Bentley\Engineering\STAAD.Pro 2025\STAAD\StaadPro.dll` |
| **STAAD.Pro 2024** | `C:\Program Files\Bentley\Engineering\STAAD.Pro 2024\STAAD\StaadPro.dll` |
| **STAAD.Pro 2023** | `C:\Program Files\Bentley\Engineering\STAAD.Pro 2023\STAAD\StaadPro.dll` |
| **STAAD.Pro CONNECT Edition** | `C:\Program Files\Bentley\Engineering\STAAD.Pro CONNECT Edition\STAAD\StaadPro.dll` |

---

### Step 2.2: Update Your `.csproj` with `<COMFileReference>`

Open your client `.csproj` file and add the `COMFileReference` pointing to your detected path:

```xml
<ItemGroup>
  <!-- Replace the path below with your installed STAAD version path -->
  <COMFileReference Include="C:\Program Files\Bentley\Engineering\STAAD.Pro 2025\STAAD\StaadPro.dll">
    <EmbedInteropTypes>True</EmbedInteropTypes>
  </COMFileReference>
</ItemGroup>
```

> **Tip for Multi-Machine Teams**: You can use conditional MSBuild resolution to automatically detect the path:
> ```xml
> <PropertyGroup>
>   <StaadDllPath Condition="'$(StaadDllPath)' == '' and Exists('C:\Program Files\Bentley\Engineering\STAAD.Pro 2025\STAAD\StaadPro.dll')">C:\Program Files\Bentley\Engineering\STAAD.Pro 2025\STAAD\StaadPro.dll</StaadDllPath>
>   <StaadDllPath Condition="'$(StaadDllPath)' == '' and Exists('C:\Program Files\Bentley\Engineering\STAAD.Pro 2024\STAAD\StaadPro.dll')">C:\Program Files\Bentley\Engineering\STAAD.Pro 2024\STAAD\StaadPro.dll</StaadDllPath>
>   <StaadDllPath Condition="'$(StaadDllPath)' == '' and Exists('C:\Program Files\Bentley\Engineering\STAAD.Pro 2023\STAAD\StaadPro.dll')">C:\Program Files\Bentley\Engineering\STAAD.Pro 2023\STAAD\StaadPro.dll</StaadDllPath>
> </PropertyGroup>
> <ItemGroup Condition="'$(StaadDllPath)' != '' and Exists('$(StaadDllPath)')">
>   <COMFileReference Include="$(StaadDllPath)">
>     <EmbedInteropTypes>True</EmbedInteropTypes>
>   </COMFileReference>
> </ItemGroup>
> ```

---

## 3. Application Manifest Configuration (`app.manifest`)

When building a standalone client executable (WPF, WinForms, or Console App) connecting out-of-process to STAAD.Pro without administrator elevation, Windows Side-by-Side (SxS) requires COM interface external proxy/stub declarations.

1. Copy [`app.manifest`](../src/StaadPro.Interop/app.manifest) into your client project folder.
2. Ensure the `<comInterfaceExternalProxyStub>` elements are included for OpenSTAAD interfaces (`IOpenSTAADUI`, `IOSGeometryUI`, `IOSLoadUI`, etc.).
3. In your client project properties under **Build Events** (Post-build event), add:
   ```cmd
   copy $(ProjectDir)app.manifest $(TargetDir)\$(AssemblyName).exe.manifest
   ```

---

## 4. Key Namespaces & Architecture

For all seven assignment read-back methods, including concentrated and uniform moments, linearly varying forces, and trapezoidal forces, see [Load-Queries.md](Load-Queries.md). The selected load case remains active after these queries; values retain OpenSTAAD's raw read-back units.

Discover existing case metadata with `load.GetAllPrimaryLoadCases()` and `load.GetAllReferenceLoadCases()` before reading assignments. Discovery leaves the active case unchanged and returns independent case objects in sets with unspecified order.

For known case numbers, use `load.GetPrimaryLoadCaseFromId(17)` or `load.GetReferenceLoadCaseFromId(101)`. Read several primary cases with `load.GetPrimaryLoadCasesFromIds(new[] { 41, 17, 41 })`; input order and duplicate IDs are preserved. These metadata lookups leave the active case unchanged. All IDs must be positive and already exist; failed metadata reads throw.

- `StaadPro.Interop.Services`: `OpenStaadWrapperProvider` factory for acquiring model sessions across ROT, active instances, and file launch.
- `StaadPro.Interop.Models`: `OpenStaadWrapper` model session container exposing `Geometry` (`IOSGeometry`), `Load` (`IOSLoad`), and unit system metadata.
- `StaadPro.Interop.Adapters.Interfaces`: `IOSGeometry`, `IOSLoad`, `IOSBase` adapter contracts.
- `StaadPro.Interop.Adapters.Models`: `OSGeometryAdapter`, `OSLoadAdapter`, `OSBaseAdapter` implementations.
- `StaadPro.Interop.Entities`: `Node`, `Beam`, `Plate`, `Member`, `LoadCase`, `EntityGroup<T>`.
- `StaadPro.Interop.Enums`: `LoadType`, `LoadCaseType`, `BaseUnitSystem`, `ForceInputUnit`, `LengthInputUnit`, `BeamAxis`, `BeamRelation`, `SurfaceType`, `GroupType`.
