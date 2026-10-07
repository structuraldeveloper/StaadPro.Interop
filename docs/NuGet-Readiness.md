# NuGet readiness assessment

Assessment date: 7 October 2026. Repository baseline: `b9c5864` (second load query increment), plus the next three load functions in this working tree.

## Decision

**The repository can produce an installable NuGet preview package. A stable public release is not yet verified.** Local build, offline tests, package archive checks, isolated installation, compilation, and execution have passed. No package has been pushed to NuGet.org.

The generated artifact is `artifacts/load-port/package/StaadPro.Interop.1.0.0-preview.20261007.load3.nupkg`. It can be used through a local NuGet feed for review and testing. The project's default version remains `1.0.0`; use an explicit preview version while the live verification gates below remain open. Package ID ownership, publishing credentials, and version availability on NuGet.org have not been checked.

## Completed implementation

Nine missing functions have now been ported from the main project's `IOSLoad`/`OSLoadAdapter`:

1. `GetNodalLoads(ILoadCase lc, int nId)`.
2. `GetMemberUniformlyDistributedLoads(ILoadCase lc, int mId)`.
3. `GetMemberConcentratedLoads(ILoadCase lc, int mId)`.
4. `GetMemberConcentratedMoments(ILoadCase lc, int mId)`.
5. `GetMemberLinearVaryingLoads(ILoadCase lc, int mId)`.
6. `GetMemberTrapezoidalLoads(ILoadCase lc, int mId)`.
7. `GetMemberUniformMoments(ILoadCase lc, int mId)`.
8. `GetAllPrimaryLoadCases()`.
9. `GetAllReferenceLoadCases()`.

The current increment adds functions 7–9 and the required `MemberUniformMoment` entity. Implementations and private helpers remain in the single, non-partial `OSLoadAdapter.cs`; signatures remain in `IOSLoad.cs`. Models use this repository's converter and dynamic COM dispatch without private application dependencies. Value copying and null handling follow the existing open source load-model conventions.

Uniform moment queries preserve all five native arrays, directions 1–9, spans, offsets, raw values and supplied case identity. The model converts distributed moment intensity with `MomentConversionFactor / LengthConversionFactor` and converts eccentricity as a length; both correct the main model's dimensional/omitted-offset behavior. Primary/reference enumeration returns new metadata sets without activation, validates the case-number return as a count rather than zero success, and uses `GetReferenceLoadType` for reference classification. Sparse and overlapping family IDs, all defined engineering types, and malformed responses are tested explicitly.

Their main-project public signatures and assignment mappings are preserved. The open source port adds argument validation, checked activation/count/fetch results, strict array validation, nonzero SAFEARRAY lower-bound support, invalid-direction checks, and detection of non-finite or unwritten values. Enumeration rejects malformed metadata and inconsistent counts instead of returning partial sets. The prior connectivity port has been removed, including its public API, implementation, enums, tests, and guide. The main source project was read as a reference and was not edited by this increment.

Every new method has comprehensive, expanded XML documentation on both the interface and concrete adapter. The package includes that XML beside the DLL, a NuGet README, and the full [load query guide](Load-Queries.md). README and getting-started claims now match the actual `net481` target and avoid claiming unverified cross-version or shared-session thread safety.

## Verification evidence

| Check | Result | Scope |
| --- | --- | --- |
| Release build | Passed, zero warnings/errors | Library and test project, .NET SDK 9.0.304 / .NET Framework 4.8.1 |
| Automated test suite | **184 executed, 184 passed, 0 failed** | 33 original offline cases plus 151 query/model/documentation cases; 46 added in this increment |
| Environment-dependent provider test | One explicit live test not executed | Asserts no running STAAD session; excluded from ordinary offline runs |
| Package creation | Passed | Preview package with `lib/net481/StaadPro.Interop.dll` and XML |
| Archive/metadata checks | Passed | Package identity, MIT metadata, repository URL, README, load guide, eighteen complete XML query entries, four documented models, no connectivity API leakage |
| Isolated package restore | Passed | Fresh temporary consumer/cache; PackageReference only, no repository ProjectReference |
| Installed-package compilation/runtime | Passed, zero build warnings/errors | All eighteen XML examples, seven assignment APIs under primary/reference/empty cases, both metadata enumerations with overlapping IDs and unchanged activation, and four models' assignment dispatch using a managed stand-in; restored archive SHA256 matches the supplied package |
| NuGet dependency vulnerability query | No vulnerable dependencies reported | `dotnet list ... package --vulnerable --include-transitive`, configured NuGet sources, assessment date |
| CI verification configuration | Added | Windows CI now packs a preview and runs the installed-package verifier; remote CI has not been observed in this session |
| Real OpenSTAAD query execution | **Pending** | Native COM dispatch/marshaling, unit conventions, version compatibility |
| NuGet.org publication | **Not performed** | Account ownership, credentials, and version availability unverified |

The test result file is `artifacts/load-port/tests/load-port.trx`. Its counters report 185 discovered cases and 184 executed; the remaining case is the explicit ROT/environment test. The console's passed total is 184. Offline passing results are not a certification of live STAAD engineering output.

## What blocks a stable release

### 1. Verify these queries against native STAAD

Execute the known-model validation described in [Load-Queries.md](Load-Queries.md#live-validation-before-release) on each STAAD version and architecture to be advertised. Verify all nodal components, every supported direction, partial/full UDL positions, concentrated and uniform moments, local linear/triangular start/end/middle intensities, partial-span and projected trapezoidal loads, eccentricities, multiple and empty assignments, primary/reference activation, invalid IDs, native fetch statuses, and both metric/imperial read-back behavior. Verify distinct concentrated moment, distributed moment, length, and force-per-length conversion factors. Also verify case enumeration counts/statuses, sparse and overlapping IDs, titles/types and unchanged active case. Record expected and observed values. The installed native 2025 documentation was inspected; no live execution was performed.

The installed Bentley Python wrapper and native reference disagree in a few details. For example, the Python UDL wrapper checks a Boolean-style return while the native reference documents `0` as success. The new C# methods follow the native reference's status semantics and the main C# signatures. Live evidence must settle compatibility before claiming a supported version range.

### 2. Define and validate the release's supported API scope

A NuGet package ships the whole library, not only this increment. Audit the existing advertised APIs before treating `1.0.0` as broadly supported. Concrete items found during this review include:

- `CreatePrimaryLoadCases` uses the automatic-ID creation overload even when supplied cases contain explicit IDs. Define whether that is intended, document the behavior, and verify callers' case associations.
- `AddSupportSettlement` divides the displacement by 1000 for every direction. Verify linear and rotational unit conventions and the method's advertised millimeter contract.
- Existing `SetLoadCaseActive` ignores native activation statuses. The new queries check statuses through their own activation path; other load operations still need an audit of failure handling.
- Existing geometry helpers dispatch COM operations through parallel workers. Validate COM apartment and shared-session handling before promising general thread safety or deadlock-free use.
- Provider/wrapper setup can suppress failures or return null. Validate acquisition, connection failure, and disposal behavior through the installed package and improve user-facing error guidance where necessary.

These are inherited implementation concerns, not failures found in the new offline load-query tests. They were not changed by this focused port. Complete coverage of the private main project is not inherently required to release a useful package; the documented and tested release scope must be accurate.

### 3. Complete release administration and reproducibility

Confirm the intended NuGet ID/owner and available preview/stable versions. Verify provenance and MIT redistribution rights for the source being released. Observe a clean Windows CI run with the package consumer check, choose the release version/tag, prepare release notes and the supported-runtime matrix, and decide how to expose source/symbols for debugging. Symbol packages and Source Link are improvements to consider; their absence does not prevent NuGet installation. Publishing requires the package owner's credentials and a deliberate release action.

## Recommended sequence

1. Review uniform moments and both metadata enumerations alongside the preceding six assignment queries; keep their guide, metadata failure checks and dimensional regression tests with the port.
2. Run native known-model tests and record supported versions, architecture, units, and status behavior.
3. Resolve the inherited load-ID, settlement-unit, activation, and COM-threading concerns for the intended release scope; adjust advertised capabilities to match verified behavior.
4. Run a clean Release build, all applicable automated/live checks, and isolated package verification in Windows CI.
5. Produce and validate a preview with an explicit version. Confirm NuGet ownership/version availability and publish the preview when approved for public testing.
6. Collect consumer/live validation evidence, close stable-release gates, prepare the final release notes, and publish the chosen stable version.

## Reproduce local checks

```powershell
dotnet build StaadPro.Interop.sln -c Release
dotnet test tests/StaadPro.Interop.Tests/StaadPro.Interop.Tests.csproj -c Release --logger "trx;LogFileName=load-port.trx" --results-directory artifacts/load-port/tests
dotnet list src/StaadPro.Interop/StaadPro.Interop.csproj package --vulnerable --include-transitive
dotnet pack src/StaadPro.Interop/StaadPro.Interop.csproj -c Release -p:PackageVersion=1.0.0-preview.20261007.load3 -o artifacts/load-port/package
./eng/Verify-LoadPackage.ps1 -PackagePath artifacts/load-port/package/StaadPro.Interop.1.0.0-preview.20261007.load3.nupkg
```

The verifier leaves its uniquely named temporary consumer directory in the user's temporary folder so the restored package and executable can be inspected. It makes no changes to a STAAD model and does not publish anything.
