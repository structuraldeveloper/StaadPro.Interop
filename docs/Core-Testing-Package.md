# StaadPro.Interop.Core.Testing

This is a separate, temporary testing package for **Windows .NET 8** applications. It contains the independent `PreferExistingOpenStaadWrapperProvider`, expanded XML IntelliSense and the existing wrapper/adapters. It is a preview for testing, not a stable production release. Its package ID differs from the main `StaadPro.Interop` package; the assembly name and namespaces remain `StaadPro.Interop`.

| Item | Value |
| --- | --- |
| Package ID | `StaadPro.Interop.Core.Testing` |
| Test version | `1.0.0-preview.20261013.1` |
| Target | `net8.0-windows` (NuGet asset `net8.0-windows7.0`) |
| Author | Srinivasa Rao Masanam |
| Copyright | Copyright (c) 2026 StaadDev |
| License | MIT |
| Source branch | `main-wrapper-redesign` |
| Upload archive | `artifacts/core-testing/StaadPro.Interop.Core.Testing.1.0.0-preview.20261013.1.nupkg` |

The asset suffix is a framework identifier; it does not certify Windows 7 as a supported .NET 8 operating system. Use an operating system supported by the Windows .NET 8 runtime. This package does not target .NET Core 3.1 or non-Windows applications. Bentley executables, licensing and native components are not included.

## Build and verify the archive

From a clean committed checkout on Windows, with the .NET SDK and Windows Desktop targeting tools installed:

```powershell
./eng/Build-CoreTestingPackage.ps1
```

The script packages only the modern asset, records the exact source commit and branch, verifies package metadata, the WPF framework reference, dependencies, guide and XML documentation, and prints SHA256. It does not change the ordinary package ID or publish anything. Choose a new prerelease version with `-Version` for subsequent uploads; an existing published version cannot be replaced.

The implementation's managed tests passed with 765 tests on each of `net481` and `net8.0-windows`. Controlled native tests on both runtimes exercised new-target startup, repeated attachment, the conservative closed-model checks, fallback and cleanup on local STAAD.Pro 2026. See [the recorded evidence](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main-wrapper-redesign/docs/reports/evidence/Prefer-Existing-Provider-Validation-20261013.json). These results do not certify every STAAD release or every ready-blank state.

## Upload to NuGet.org

The selected publication route is manual web upload. A locally generated package is **not yet publicly available**.

1. Sign in to [NuGet.org](https://www.nuget.org/) with the intended owner account and select **Upload**.
2. Select the exact archive listed above. Check the ID, version, author, MIT license, testing release notes, source commit, README and modern framework asset before submission. The main package is not the upload target.
3. Submit, inspect the displayed status and wait for validation/indexing. Resolve an ownership/name rejection rather than assuming an absent feed entry guarantees an available ID.
4. Verify the exact version through the public feed and a fresh consumer restore as shown below. Until those checks pass, treat publication as pending.

The expected package page after acceptance is [StaadPro.Interop.Core.Testing 1.0.0-preview.20261013.1](https://www.nuget.org/packages/StaadPro.Interop.Core.Testing/1.0.0-preview.20261013.1). It may not exist before upload. The publisher becomes the owner; the Authors metadata does not grant ownership. See Microsoft's [publishing instructions](https://learn.microsoft.com/en-us/nuget/nuget-org/publish-a-package).

```powershell
$testVersion = '1.0.0-preview.20261013.1'
$index = Invoke-RestMethod 'https://api.nuget.org/v3-flatcontainer/staadpro.interop.core.testing/index.json'
if ($index.versions -notcontains $testVersion) {
    throw 'The exact testing version is not publicly downloadable yet.'
}
```

## Install in a Windows .NET 8 application

Do not reference this package alongside `StaadPro.Interop`: both provide the same assembly. Replace the main package reference in the test application. A console application also needs the Windows Desktop framework because the library exposes WPF geometry types.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0-windows</TargetFramework>
    <UseWPF>true</UseWPF>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="StaadPro.Interop.Core.Testing"
                      Version="1.0.0-preview.20261013.1" />
  </ItemGroup>
</Project>
```

After publication, the installation command is:

```powershell
dotnet add package StaadPro.Interop.Core.Testing --version 1.0.0-preview.20261013.1 --source https://api.nuget.org/v3/index.json
dotnet restore --source https://api.nuget.org/v3/index.json --packages ./public-restore-cache
dotnet build --no-restore
```

Use a new cache directory to demonstrate a public-feed restore. Before publication, restore from the local package directory instead, retaining NuGet.org for Newtonsoft.Json:

```powershell
dotnet restore --source '<repo-root>/artifacts/core-testing' --source https://api.nuget.org/v3/index.json --packages ./local-test-cache
```

An attachment-only smoke test uses a synchronous STA entry point and leaves the existing model untouched:

```csharp
using System;
using StaadPro.Interop.Services;

internal static class Program
{
    [STAThread]
    private static int Main()
    {
        using (var provider = new PreferExistingOpenStaadWrapperProvider())
        {
            var result = provider.Acquire();
            foreach (string diagnostic in result.Diagnostics)
                Console.WriteLine(diagnostic);
            if (!result.Succeeded)
            {
                Console.WriteLine(result.Failure);
                return 1;
            }
            using (result.Wrapper)
                Console.WriteLine($"{result.Outcome}: PID {result.ProcessId}, {result.ActualPath}");
            return 0;
        }
    }
}
```

Run against an already-open licensed STAAD model in the same Windows session and privilege context. `Acquire()` without a path never creates files or launches STAAD. It reports failure if no usable root is available. For filename acquisition, launcher configuration, discovery policies, blank semantics, ownership and cancellation, read [the full provider guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main-wrapper-redesign/docs/Prefer-Existing-Provider.md).

Acquisition and wrapper use must remain on the same STA thread. Unmapped blank processes cannot be bound reliably by arbitrary PID; a synchronous native COM call can exceed the logical timeout. Existing provider/wrapper source remains unchanged. Trimmed/AOT deployment and other STAAD releases have not been validated.

## Retire the test package

When testing is complete, unlist this ID/version in NuGet.org **Manage package** and remove its reference from test applications. Switch to a main-package version that actually includes the tested implementation. Do not assume an earlier Framework-only main preview contains modern assets.

Unlisting hides a package from normal search; exact-version downloads remain possible. NuGet.org restricts deletion, so a published test package is not generally disposable through permanent removal. See the official [unlisting/deletion policy](https://learn.microsoft.com/en-us/nuget/nuget-org/policies/deleting-packages).
