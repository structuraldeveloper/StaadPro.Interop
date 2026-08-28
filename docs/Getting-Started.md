# Getting Started with StaadPro.Interop

## Overview & Prerequisites

StaadPro.Interop provides a modern, strongly-typed .NET interface for Bentley STAAD.Pro.

- **Target Frameworks**: .NET Framework 4.8.1 / .NET 8.0
- **Supported STAAD Versions**: STAAD.Pro 2025, STAAD.Pro 2024, STAAD.Pro 2023, and STAAD.Pro CONNECT Edition.

> [!IMPORTANT]
> **STAAD.Pro Version Compatibility ($\ge 2024$ recommended)**:
> While basic geometry querying functions are backward-compatible, several advanced functions in this library (including extended load case creation/overloads, reference load case methods, and specific parametric surface definitions) require **STAAD.Pro 2024 or higher** ($\ge 2024$). These functions may not be available or supported in STAAD.Pro 2023 or older releases. For the best compatibility and full feature support, ensure STAAD.Pro 2024 or newer (e.g., STAAD.Pro 2024 / 2025) is installed.

---

## 1. Standard (Default) Usage: Dynamic Decoupled Interop

By default, `StaadPro.Interop` uses runtime COM dynamic dispatch via `OpenStaadWrapperProvider`. This provides major benefits:
- **Zero build-time dependencies**: You do not need Bentley DLLs (`StaadPro.dll`, `OpenSTAADUI.dll`) or registered TypeLibs to compile your projects or run automated CI/CD pipelines.
- **Version agnostic**: The same compiled binary works seamlessly with STAAD.Pro 2023, 2024, 2025, or CONNECT Edition installed on the end-user machine.

```csharp
using StaadPro.Interop.Services;
using StaadPro.Interop.Models;

// Attach to active STAAD instance or open specified model
using (OpenStaadWrapper wrapper = OpenStaadWrapperProvider.Get(@"C:\Models\Structure.std"))
{
    // Access Geometry & Load adapters
    var geometry = wrapper.Geometry;
    var load = wrapper.Load;

    int nodeCount = geometry.GetNodeCount();
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

1. Copy [`templates/app.manifest`](../templates/app.manifest) into your client project folder.
2. Ensure the `<comInterfaceExternalProxyStub>` elements are included for OpenSTAAD interfaces (`IOpenSTAADUI`, `IOSGeometryUI`, `IOSLoadUI`, etc.).
3. In your client project properties under **Build Events** (Post-build event), add:
   ```cmd
   copy $(ProjectDir)app.manifest $(TargetDir)\$(AssemblyName).exe.manifest
   ```

---

## 4. Key Namespaces & Architecture

- `StaadPro.Interop.Services`: `OpenStaadWrapperProvider` factory for acquiring model sessions across ROT, active instances, and file launch.
- `StaadPro.Interop.Models`: `OpenStaadWrapper` model session container exposing `Geometry` (`IOSGeometry`), `Load` (`IOSLoad`), and unit system metadata.
- `StaadPro.Interop.Adapters.Interfaces`: `IOSGeometry`, `IOSLoad`, `IOSBase` adapter contracts.
- `StaadPro.Interop.Adapters.Models`: `OSGeometryAdapter`, `OSLoadAdapter`, `OSBaseAdapter` implementations.
- `StaadPro.Interop.Entities`: `Node`, `Beam`, `Plate`, `Member`, `LoadCase`, `EntityGroup<T>`.
- `StaadPro.Interop.Enums`: `LoadType`, `LoadCaseType`, `BaseUnitSystem`, `ForceInputUnit`, `LengthInputUnit`, `BeamAxis`, `BeamRelation`, `SurfaceType`, `GroupType`.
