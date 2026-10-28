# Publishing StaadPro.Interop

## Release being prepared

| Item | Value |
| --- | --- |
| Package ID | `StaadPro.Interop` |
| Preview version | `1.0.0-preview.20261028.1` |
| Author | Srinivasa Rao Masanam |
| Company | TensorStruct |
| Intended NuGet.org owner | TensorStruct (managed in the registry) |
| Copyright | Copyright (c) 2026 TensorStruct |
| Package icon | Embedded symbol-only logo; see [branding](Branding.md) |
| README banner | Logo with text, hosted from the repository for NuGet.org; see [branding](Branding.md) |
| Release notes | [Release-Notes.md](Release-Notes.md), also included in package metadata and the archive |
| Target | .NET Framework 4.8.1 (`net481`), Windows |
| Upload artifact | `artifacts/release/StaadPro.Interop.1.0.0-preview.20261028.1.nupkg` |
| Public feed | `https://api.nuget.org/v3/index.json` |
| Source | [structuraldeveloper/StaadPro.Interop](https://github.com/structuraldeveloper/StaadPro.Interop) |

Public publication is complete after NuGet.org accepts the submission, validates/indexes it, and the exact version restores from the public feed. A generated archive alone does not establish public availability. See [NuGet-Readiness.md](NuGet-Readiness.md) for the current publication status and remaining native STAAD validation gates. This release uses a preview version because offline tests do not certify live COM/unit behavior.

The package contains the `net481` DLL, expanded XML IntelliSense, a NuGet README, the load query guide, and installation/publishing guides. Newtonsoft.Json is a NuGet dependency. Bentley binaries and the private main application's dependencies are not bundled.

The project embeds both supplied PNG logos and sets `PackageIcon` to `images/symbol-only-logo.png`. Keep this selection for subsequent releases. Use the full logo with text in the repository and NuGet README. Before publication, push both root logo files and check that the README's [hosted banner](https://raw.githubusercontent.com/structuraldeveloper/StaadPro.Interop/main/logo-with-text.png) is publicly reachable. NuGet.org does not render relative local README image paths, even when those images are embedded in the package. See [NuGet's README image guidance](https://learn.microsoft.com/en-us/nuget/nuget-org/package-readme-on-nuget-org#allowed-domains-for-images-and-badges).

## Publisher metadata and NuGet.org ownership

This archive declares `Srinivasa Rao Masanam` in package Authors and `TensorStruct` in assembly Company, with `Copyright (c) 2026 TensorStruct` in package and assembly copyright metadata. NuGet.org account ownership is managed separately from the archive: its Authors/Owners manifest fields do not transfer portal ownership. To display TensorStruct as an owner, an existing owner must add the intended TensorStruct account or organization through the package's **Manage package → Owners** page and complete the required confirmation. See Microsoft's [ownership instructions](https://learn.microsoft.com/en-us/nuget/nuget-org/publish-a-package#manage-package-owners-on-nugetorg).

## Visual Studio search attribution

Keep `<Authors>Srinivasa Rao Masanam</Authors>` exactly. The pack target rejects any override that changes this value, and the archive verifier checks the generated nuspec. Company remains TensorStruct; copyright is TensorStruct.

The Visual Studio NuGet search “by” line cannot be forced by package Authors on every client/feed. NuGet 6.11 introduced owner-first display for feeds supplying registry owner metadata, with author fallback when owners are unavailable. See [NuGet's completed owner-display change](https://github.com/NuGet/Home/issues/12501) and [implementation](https://github.com/NuGet/NuGet.Client/pull/5766). Local feeds and older clients can display the package Authors value; NuGet.org-backed newer clients can show the registered owner instead. Archive owners metadata cannot transfer registry ownership or override this behavior. Registry ownership changes remain the package owner's separate responsibility; this build retains the exact requested author.

## 1. Build the exact release artifact

Before preparing a distribution archive, update all three publication surfaces: the project's `Description` and `PackageReleaseNotes`, the latest entry in `docs/Release-Notes.md`, and `docs/NuGet-README.md`. Describe the changes, compatibility, installation and validation in professional language for a reader who has no knowledge of the development discussion. Keep historical release entries intact.

Set `ReleaseDocumentationVersion` in the project to the new distribution version, use the same version in the README's `## Version` heading and the first release-notes heading, and review their content before packaging. The project defaults `PackageVersion` to that documented preview. Normal distribution builds reject a different or undocumented version. `eng/Prepare-PackageDocumentation.ps1` runs during packing and places the reviewed documents in the archive. Automated `preview.ci` builds receive their actual version in those packaged documents without changing the maintained source files.

Run these commands from the repository root in PowerShell on Windows with the .NET Framework 4.8.1 targeting tools and a suitable .NET SDK. The repository has been verified with a locally installed .NET SDK; the repository's existing Windows CI workflow also builds and verifies packages.

```powershell
dotnet restore StaadPro.Interop.sln
dotnet build StaadPro.Interop.sln -c Release --no-restore
dotnet test tests/StaadPro.Interop.Tests/StaadPro.Interop.Tests.csproj -c Release --no-build --logger "trx;LogFileName=load-port.trx" --results-directory artifacts/load-port/tests
dotnet list src/StaadPro.Interop/StaadPro.Interop.csproj package --vulnerable --include-transitive

$releaseVersion = '1.0.0-preview.20261028.1'
$releaseCommit = git rev-parse HEAD
dotnet pack src/StaadPro.Interop/StaadPro.Interop.csproj -c Release -p:PackageVersion=$releaseVersion -p:RepositoryCommit=$releaseCommit -o artifacts/release
./eng/Verify-LoadPackage.ps1 -PackagePath "artifacts/release/StaadPro.Interop.$releaseVersion.nupkg"
Get-FileHash "artifacts/release/StaadPro.Interop.$releaseVersion.nupkg" -Algorithm SHA256
```

Stop if a command fails. Publish the exact archive that passed verification. Keep its version, source commit, SHA256 and test results together. When preparing an ordinary release, commit the implementation and documentation before packing so repository metadata identifies the matching source. The project defaults PackageVersion to the reviewed ReleaseDocumentationVersion; retain that preview version until the stable-release gates are closed.

The verifier restores through a fresh temporary PackageReference-only consumer/cache, checks the restored archive hash, validates package metadata and shipped documentation, and compiles/runs 1,152 selected XML example blocks. These include 498 common Property examples, twelve optional catalog/acquisition examples, all 192 Output examples, all 38 Support examples and the existing Geometry/Load/Root examples and runtime checks. Mutation examples are guarded; managed native-signature fixtures exercise dispatch and result decoding without modifying a STAAD model. This verification does not upload anything to NuGet.org.

## 2. Upload the verified archive through NuGet.org

The package owner selected manual upload for this release; the workspace has not submitted it. Open [NuGet.org](https://www.nuget.org/) in the browser where you signed in:

1. Choose **Upload** and select `StaadPro.Interop.1.0.0-preview.20261028.1.nupkg` from `artifacts/release`.
2. Review the package ID, exact version, author **Srinivasa Rao Masanam**, copyright, release notes, `net481` target, MIT license, repository link, dependency and README preview. Choose the intended owner account/organization if the page offers that selection. Leave the version listed for public discovery.
3. Choose **Submit** after the metadata matches the verified artifact.
4. Inspect the resulting package status, wait for validation/indexing, then perform the public-feed checks below.

The first publisher becomes the package owner; the project's `Authors` field does not grant publishing ownership. An existing ID requires authorized ownership. NuGet.org may take time to make an accepted upload searchable. These portal/ownership rules are documented in Microsoft's [publishing guide](https://learn.microsoft.com/en-us/nuget/nuget-org/publish-a-package).

An account login remains in the official NuGet/Microsoft browser flow. A browser session is separate from command-line authentication; signing in on the website does not populate a CLI publishing key. Account authentication cannot be supplied by this package or its adapters.

If Upload reports an unavailable ID, ownership problem, or rejected metadata, resolve the displayed error before retrying. A missing public flat-container index is only an availability observation; it does not guarantee that a name is unreserved or that a particular account can publish it. Do not rename the package casually: installation commands, project references and release documentation must change together.

## 3. Verify public availability and public installation

After uploading, allow NuGet.org validation and indexing to finish before using the public installation commands. The expected public page, once accepted, is:

`https://www.nuget.org/packages/StaadPro.Interop/1.0.0-preview.20261028.1`

Check the exact version in the public feed:

```powershell
$releaseVersion = '1.0.0-preview.20261028.1'
$index = Invoke-RestMethod 'https://api.nuget.org/v3-flatcontainer/staadpro.interop/index.json'
if ($index.versions -notcontains $releaseVersion) {
    throw 'The exact preview version is not available from the public flat-container feed yet.'
}
```

Also inspect the NuGet.org page and search with prerelease results enabled. An exact-version download can succeed for an unlisted version; search visibility requires listing/indexing. Microsoft describes this distinction in its [unlisting policy](https://learn.microsoft.com/en-us/nuget/nuget-org/policies/deleting-packages).

For a download check:

```powershell
$releaseVersion = '1.0.0-preview.20261028.1'
$downloadDirectory = Join-Path ([IO.Path]::GetTempPath()) ('StaadPublicPackage-' + [Guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $downloadDirectory | Out-Null
$downloadPath = Join-Path $downloadDirectory "StaadPro.Interop.$releaseVersion.nupkg"
Invoke-WebRequest "https://api.nuget.org/v3-flatcontainer/staadpro.interop/$releaseVersion/staadpro.interop.$releaseVersion.nupkg" -OutFile $downloadPath
./eng/Verify-LoadPackage.ps1 -PackagePath $downloadPath
```

The verifier checks a downloaded archive using its temporary local feed. To prove **public-feed restore**, separately use [Installation.md](Installation.md#install-from-the-public-feed) in a fresh `net481` consumer with a new package cache, explicitly sourcing NuGet.org. Build it and run its metadata sample against a licensed known STAAD model for native validation. Record the public page, accepted version, actual owner, source commit and public restore outcome in the release assessment. NuGet.org may sign the uploaded package, so compare DLL/XML contents when correlating local and downloaded artifacts rather than assuming whole-archive hashes must remain identical; see the [signed package reference](https://learn.microsoft.com/en-us/nuget/reference/signed-packages-reference).

## 4. Optional command-line publication

Web upload needs no API key. For later CLI publication, create a scoped Push key in your NuGet.org account for `StaadPro.Interop`, allowing new packages/versions as needed. Configure it locally as `NUGET_API_KEY`, never in source files or chat. Pass the value explicitly for compatibility with SDKs that do not read it from the environment:

```powershell
$releaseVersion = '1.0.0-preview.20261028.1'
if ([string]::IsNullOrWhiteSpace($env:NUGET_API_KEY)) {
    throw 'Configure a scoped NUGET_API_KEY in this local terminal first.'
}
dotnet nuget push "artifacts/release/StaadPro.Interop.$releaseVersion.nupkg" --api-key $env:NUGET_API_KEY --source https://api.nuget.org/v3/index.json
if ($LASTEXITCODE -ne 0) { throw 'NuGet push failed; inspect the response before treating this version as published.' }
```

Avoid a literal key in command history. The key is passed to the publishing process; use an appropriately protected terminal/runner. The locally verified SDK 10.0.401 also supports direct environment-variable handling; the explicit argument shown works with older SDKs as well. See the official [dotnet nuget push reference](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-push) and [scoped key documentation](https://learn.microsoft.com/en-us/nuget/nuget-org/scoped-api-keys).

## 5. Later releases through Trusted Publishing

For GitHub automation, configure a NuGet.org Trusted Publishing policy for repository owner `structuraldeveloper`, repository `StaadPro.Interop`, and the actual publishing workflow filename. Select the intended account/organization, package scope and optional environment. The existing `build-and-test.yml` workflow verifies packages but does not publish them.

After build/test/pack/consumer verification steps, a publishing job can use this fragment:

```yaml
permissions:
  contents: read
  id-token: write

steps:
  # Checkout, build, test, pack and verify the exact version before these steps.
  - name: Authenticate with NuGet.org
    uses: NuGet/login@v1
    id: nuget-login
    with:
      user: ${{ vars.NUGET_USER }}
  - name: Publish verified package
    shell: pwsh
    env:
      NUGET_API_KEY: ${{ steps.nuget-login.outputs.NUGET_API_KEY }}
      PACKAGE_VERSION: ${{ inputs.package_version }}
    run: dotnet nuget push "artifacts/release/StaadPro.Interop.$env:PACKAGE_VERSION.nupkg" --api-key $env:NUGET_API_KEY --source https://api.nuget.org/v3/index.json
```

This is a fragment, not an installed publication workflow. `NUGET_USER` must be the NuGet profile name, and `package_version` must be defined and validated by the enclosing workflow. Its filename/environment must match the registered policy. NuGet issues short-lived credentials after verifying GitHub's OIDC token, as described in [Trusted Publishing](https://learn.microsoft.com/en-us/nuget/nuget-org/trusted-publishing). Configure the policy before running a publish job.

## 6. Version updates and stable release

Use a new version for each different public build, for example `1.0.0-preview.20261014` for a release on the following day, or `1.0.0-preview.20261013.5` for a subsequent release on the same day. Never treat `--skip-duplicate` as permission to replace an existing version. Keep the version pinned in release commands, installation examples and release notes; this preview uses a date-based version without a load-increment suffix.

After a successful preview publication, keep validation evidence and optionally create a matching Git tag from the source commit. Publish stable `1.0.0` only after the live compatibility/unit and inherited API gates in [NuGet-Readiness.md](NuGet-Readiness.md) are closed for the advertised scope. Source Link/symbol packages can improve debugging later; they are not bundled in this preview.

## Troubleshooting publication

| Symptom | Next action |
| --- | --- |
| Upload requests sign-in | Complete official browser sign-in/MFA, then return to Upload |
| ID/owner rejected | Check account ownership or resolve the name in NuGet.org; Authors metadata is insufficient |
| Version already exists | Prepare a new version and reverify that archive; do not overwrite |
| API push 401/403 | Check expiry, package scope and owner authorization; browser login does not authenticate the CLI |
| Accepted but not searchable | Check validation/listing status and enable prerelease search |
| Exact version still unavailable | Check the public index/package status before installing from that feed |
| Native compatibility uncertain | Keep the preview label and run the documented known-model STAAD checks |

Use [Installation.md](Installation.md) for consumer commands and runtime requirements.
