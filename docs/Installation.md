# Installing and using StaadPro.Interop

## Package and runtime

The locally prepared preview is `1.0.0-preview.20261023`, including the approved wrapper surface, all 143 reference Geometry overloads, 120 Load overloads (115 reference plus five conveniences), 15 typed Output queries, 53 common Root operations and three optional Root operations. Use this exact version with the local-feed instructions below. This local build does not establish a new public publication. Public-feed examples retain the previously verified version; consult [release notes](Release-Notes.md), [Load contracts](Load-Porting.md), [Output usage](Output-Porting.md) and [Root capabilities](Root-Completion-and-Capabilities.md) for scope and limits.

This guide uses preview version `1.0.0-preview.20261012.1` of package `StaadPro.Interop`. Public-feed commands apply after that exact version is accepted and available on NuGet.org; [Publishing.md](Publishing.md) explains upload and verification. The same verified archive can be installed immediately through a local feed.

| Requirement | Purpose |
| --- | --- |
| Windows with .NET Framework 4.8.1 | Runs the library's `net481` assembly |
| .NET Framework 4.8.1 targeting tools and suitable SDK/Visual Studio | Builds a consumer targeting `net481` |
| Licensed STAAD.Pro exposing the needed OpenSTAAD APIs | Required for actual STAAD operations |
| A connected, open model | Required for model metadata and assignment queries |
| Serialized COM access on the appropriate thread | Coordinates the shared STAAD session |

Installation and managed package checks do not require a STAAD license. Native STAAD operations do. Bentley software/binaries are separately installed and licensed. The package targets `net481`; this release does not advertise .NET 8/9/10 application compatibility. SDK version and the consuming application's target framework are different things.

Newtonsoft.Json is restored as a package dependency. XML documentation is included beside the DLL for IntelliSense on both `IOSLoad` and `OSLoadAdapter`.

## Install from the public feed

For an existing SDK-style project targeting `net481`, run:

```powershell
dotnet add ./YourApp.csproj package StaadPro.Interop --version 1.0.0-preview.20261012.1 --source https://api.nuget.org/v3/index.json
dotnet restore ./YourApp.csproj
dotnet build ./YourApp.csproj -c Release
```

For SDK 10's noun-first CLI, the equivalent command is `dotnet package add StaadPro.Interop --project ./YourApp.csproj --version 1.0.0-preview.20261012.1 --source https://api.nuget.org/v3/index.json`. The older verb-first command above matches the SDK used for this repository's verification. See Microsoft's [package add reference](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-package-add).

The resulting project reference should pin the version:

```xml
<PropertyGroup>
  <TargetFramework>net481</TargetFramework>
</PropertyGroup>
<ItemGroup>
  <PackageReference Include="StaadPro.Interop" Version="1.0.0-preview.20261012.1" />
</ItemGroup>
```

For a fresh-cache public restore check, create a new consumer directory and this `NuGet.Config`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
  </packageSources>
</configuration>
```

Then run `dotnet restore ./YourApp.csproj --configfile ./NuGet.Config --packages ./packages`. A new `packages` directory avoids treating a previously cached/local installation as proof of public availability. When public publication is pending, use the local-feed procedure below rather than claiming this restore succeeded.

## Install in Visual Studio

1. Create or open a Windows project targeting **.NET Framework 4.8.1**.
2. Open **Manage NuGet Packages** for the project and choose **nuget.org** as the source.
3. Enable **Include prerelease**, search for **StaadPro.Interop**, select the exact preview version, and install it.
4. Build the project and use the sample below with a licensed open STAAD model.

The NuGet Package Manager Console equivalent is:

```powershell
Install-Package StaadPro.Interop -Version 1.0.0-preview.20261012.1 -Source https://api.nuget.org/v3/index.json
```

For the legacy WPF test solution `NugetTestApp`, the project is at `NugetTest\NugetTest.csproj`, one directory below the solution. Use the Package Manager Console command with an explicit project:

```powershell
Install-Package StaadPro.Interop -Version 1.0.0-preview.20261012.1 -ProjectName NugetTest -Source https://api.nuget.org/v3/index.json
```

`dotnet add package` searches its current directory for a project; Visual Studio's **Default project** selection does not change that directory for an external CLI command. Running it in the solution directory produces **Could not find any project**. Pointing it at this unconfigured legacy project produces **The project does not support adding package references through the add package command**. Use `Install-Package` or the Visual Studio NuGet UI. Alternatively, add a `PackageReference` to the legacy project and restore/build it with Visual Studio MSBuild; converting the entire WPF project to SDK style is unnecessary.

Visual Studio source/version selection is described in the official [NuGet Package Manager guide](https://learn.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio).

## Install the prepared archive through a local feed

Copy the verified `.nupkg` to a dedicated folder, for example `C:\NuGetFeeds\StaadProInterop`. A local feed is a folder containing package archives; do not extract the DLL and add it as an assembly reference, because that bypasses dependency/version/documentation handling.

In the consumer directory, use a project-scoped `NuGet.Config`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <add key="StaadInteropLocal" value="C:\NuGetFeeds\StaadProInterop" />
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
  </packageSources>
</configuration>
```

NuGet.org remains configured for dependencies. Add the exact package reference with:

```powershell
dotnet add ./YourApp.csproj package StaadPro.Interop --version 1.0.0-preview.20261023
dotnet restore ./YourApp.csproj --configfile ./NuGet.Config --packages ./packages
dotnet build ./YourApp.csproj -c Release --no-restore
```

Alternatively, configure the folder as a Visual Studio package source and select it when installing the preview. Source configuration is covered by Microsoft's [NuGet.Config reference](https://learn.microsoft.com/en-us/nuget/reference/nuget-config-file).

## Minimal consumer project and first metadata read

For a small Windows console consumer, this SDK-style project is sufficient:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net481</TargetFramework>
    <OutputType>Exe</OutputType>
    <LangVersion>latest</LangVersion>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="Microsoft.CSharp" />
    <PackageReference Include="StaadPro.Interop" Version="1.0.0-preview.20261012.1" />
  </ItemGroup>
</Project>
```

Save this as `Program.cs`. Open STAAD.Pro with a licensed model first:

```csharp
using System;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Services;

internal static class Program
{
    private static void Main()
    {
        using (var wrapper = OpenStaadWrapperProvider.GetRunning())
        {
            if (wrapper?.Load == null)
                throw new InvalidOperationException("Open a STAAD model exposing OpenSTAAD first.");

            var load = wrapper.Load;
            foreach (var lc in load.GetLoadCasesOfType(LoadCaseType.PrimaryLoad))
                Console.WriteLine(lc.Id + ": " + lc.Title + " (" + lc.Type + ")");

            // Filters return metadata only and leave the active case unchanged.
            foreach (var lc in load.GetLoadCases(1, 50, LoadCaseType.PrimaryLoad))
                Console.WriteLine("In range: " + lc.Id);
        }
    }
}
```

These functions read existing case metadata. They do not create or activate a case. The new range includes both endpoints, ID filters omit missing IDs and collapse duplicates, and family lists have unspecified ordering. See [Load-Queries.md](Load-Queries.md) for the complete behavior and exceptions.

Assignment queries such as `GetNodalLoads` activate the supplied primary/reference case and leave it active. They preserve raw native values; validate read-back units before applying conversions. Combination metadata is not an assignment-query case, and repeat-load discovery remains unsupported.

## Acquire a specific model

Call `OpenStaadWrapperProvider.Get(fullPath)` on an STA thread (for example, the WPF UI thread). The provider queries the full filename using OpenSTAAD's output-VARIANT signature, compares normalized paths without case sensitivity, and reuses a matching model. If no matching model is registered, it opens the requested file once. It polls every 250 ms for up to two minutes until the requested filename, Geometry and Load interfaces, and read-only node/load-case count calls are available. Another model registered under the global ProgID is never accepted as a match.

If the path does not exist, the provider retains its existing behavior of creating a blank model there. Connection failures/timeouts return null and emit `System.Diagnostics.Trace` messages. Check the result before using the adapters. `GetRunning()` attaches to a running session and never launches STAAD. Wrapper disposal releases managed references; it does not close the model or application.

These connection fixes require a new package build; they are not present in published `1.0.0-preview.20261012`. Use a new version such as `1.0.0-preview.20261012.1` for the corrected archive.

## Check IntelliSense and package contents

Under the installed package's `lib/net481` folder, check for both `StaadPro.Interop.dll` and `StaadPro.Interop.xml`. With the default cache, this is beneath `%USERPROFILE%\.nuget\packages\staadpro.interop\1.0.0-preview.20261012.1`. A custom cache uses the location you passed to restore.

The XML supplies full method summaries, parameters, return/null semantics, mappings, side effects, exceptions and examples for the 21 ported query methods on both the interface and concrete adapter. The package's `docs` directory contains the load, installation and publishing guides. Editing a local package-cache copy is not a supported way to change the library; create a new source build/version instead.

## Upgrading and diagnostics

Pin the desired preview version explicitly, restore, then build and validate against a known model. Avoid relying on unspecified/floating preview selection for reproducible engineering tools. Retest native API availability, COM marshaling and unit conventions when changing either the package or STAAD version.

| Symptom | Check |
| --- | --- |
| Package/version not found | Exact public availability, selected source, preview checkbox, and pinned spelling/version |
| Target compatibility error | Consumer must target `net481`; installing a newer SDK alone does not retarget the app |
| Framework reference/runtime missing | Install the Windows .NET Framework 4.8.1 targeting tools/runtime needed by the consumer |
| Dependency restore fails | Include nuget.org for Newtonsoft.Json and any required framework reference dependencies |
| IntelliSense missing | Restore through PackageReference, inspect XML beside the DLL, and refresh the IDE/project |
| Wrapper or Load is null | Confirm a licensed running STAAD model, connection/provider behavior and process compatibility |
| RuntimeBinderException | Installed STAAD Load interface may lack the called native signature |
| InvalidOperationException | Inspect validated native counts/statuses/arrays/metadata; do not treat an error as an empty result |
| Missing generic lookup returns null | Valid absence; `GetLoadCase` preserves nullable lookup behavior |
| Results change after another operation | Coordinate shared-session edits and active-case changes on the appropriate COM thread |

Report the package version, target framework, STAAD build/architecture, invoked method, units, expected/observed behavior and exception details when filing an issue. Use a small known model for reproduction and avoid attaching proprietary project data unnecessarily.
