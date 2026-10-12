# NuGet readiness assessment

Assessment date: 12 October 2026. Repository baseline: `5f8c406` (fifth load query increment), plus the sixth/seventh increments and preview publishing/installation documentation prepared for release.

## Decision

**The repository can produce an installable NuGet preview package. A stable public release is not yet verified.** Local build, offline tests, package archive checks, isolated installation, compilation, and execution have passed. Public submission of this preview remains pending; a browser login alone does not establish publication.

The release artifact is `artifacts/release/StaadPro.Interop.1.0.0-preview.20261012.nupkg`. It can be used through a local feed immediately after verification. The project's default version remains `1.0.0`; use an explicit preview version while live gates remain open. The public flat-container index returned HTTP 404 during preparation, so no public version was confirmed. That result does not establish name reservation or publishing ownership. The user selected web upload and reported signing in, but desktop automation could not connect to the browser. The final portal submission and public-feed verification remain outstanding. See [Publishing.md](Publishing.md) and [Installation.md](Installation.md) for the exact steps.

## Completed implementation

Twenty-one missing functions have now been ported from the main project's `IOSLoad`/`OSLoadAdapter`:

1. `GetNodalLoads(ILoadCase lc, int nId)`.
2. `GetMemberUniformlyDistributedLoads(ILoadCase lc, int mId)`.
3. `GetMemberConcentratedLoads(ILoadCase lc, int mId)`.
4. `GetMemberConcentratedMoments(ILoadCase lc, int mId)`.
5. `GetMemberLinearVaryingLoads(ILoadCase lc, int mId)`.
6. `GetMemberTrapezoidalLoads(ILoadCase lc, int mId)`.
7. `GetMemberUniformMoments(ILoadCase lc, int mId)`.
8. `GetAllPrimaryLoadCases()`.
9. `GetAllReferenceLoadCases()`.
10. `GetPrimaryLoadCaseFromId(int lcId)`.
11. `GetReferenceLoadCaseFromId(int lcId)`.
12. `GetPrimaryLoadCasesFromIds(IEnumerable<int> loadCasesIds)`.
13. `GetReferenceLoadCasesFromIds(IEnumerable<int> loadCasesIds)`.
14. `GetAllLoadCombinationCases()`.
15. `GetLoadCombinationCaseFromId(int lcId)`.
16. `GetLoadCombinationCasesFromIds(IEnumerable<int> lcIds)`.
17. `GetAllLoadCasesOfType(LoadCaseType loadCaseType = LoadCaseType.PrimaryLoad)`.
18. `GetLoadCase(int lcId, LoadCaseType loadCaseType)`.
19. `GetLoadCases(int fromId, int toId, LoadCaseType loadCaseType)`.
20. `GetLoadCasesFromIds(IEnumerable<int> loadCasesIds, LoadCaseType loadCaseType)`.
21. `GetLoadCasesOfType(LoadCaseType loadCaseType)`.

The sixth/seventh increments add functions 16–21, reusing the existing `LoadCase` model and checked family readers. Implementations remain in the single, non-partial `OSLoadAdapter.cs`; signatures remain in `IOSLoad.cs`. There are no new private application or Bentley binary dependencies.

The latest three functions eagerly filter inclusive positive ranges, filter requested IDs with duplicate collapse and omission of missing values, and return concrete family lists. Inputs are validated before COM; filters read the full selected family and still propagate unmatched metadata failures. Empty ID input skips native reads after family validation. Results are fresh metadata lists and preserve active-case state. Full IntelliSense, boundary/ownership/error tests, and packaged installation/publishing guides accompany the preview.

Combination batches preserve the main project's distinct set semantics, snapshot input once, validate every ID before COM and every membership before titles, and fetch combination IDs once. Family selection eagerly dispatches to primary/reference/combination readers with a primary default. Repeat/undefined families throw `NotSupportedException`. Generic lookup preserves the main project's null-on-absence behavior after a successful full-family read, while native/malformed metadata errors, including unrelated-case failures, still throw. All three return fresh metadata and preserve active-case state.

Reference batches snapshot input once and validate all IDs before COM access, preserve order and duplicates as independent records, and use reference-specific title/type APIs. Combination discovery validates native count/number results and titles, while direct lookup verifies membership before reading a title. Combinations have `CaseType=LoadCombination` and `Type=None`, with no type API call and no factors/constituent cases loaded. All three methods preserve active-case state, return fresh objects and throw rather than returning partial results.

Focused primary/reference FromId lookups reject nonpositive IDs before COM access and read the matching family's title/type APIs without activation or case enumeration. Batch primary lookup enumerates and snapshots input once, validates every ID before COM, preserves order and duplicate IDs as independent objects, returns a fresh empty list for empty input, and returns no partial result on failure. Its eager behavior, native errors, ownership and session constraints are documented in both interface and concrete IntelliSense. Combination FromId lookup validates membership; the new generic lookup instead reads the complete selected family and can return null for absence.

Uniform moment queries preserve all five native arrays, directions 1–9, spans, offsets, raw values and supplied case identity. The model converts distributed moment intensity with `MomentConversionFactor / LengthConversionFactor` and converts eccentricity as a length; both correct the main model's dimensional/omitted-offset behavior. Primary/reference enumeration returns new metadata sets without activation, validates the case-number return as a count rather than zero success, and uses `GetReferenceLoadType` for reference classification. Sparse and overlapping family IDs, all defined engineering types, and malformed responses are tested explicitly.

Their main-project public signatures and assignment mappings are preserved. The open source port adds argument validation, checked activation/count/fetch results, strict array validation, nonzero SAFEARRAY lower-bound support, invalid-direction checks, and detection of non-finite or unwritten values. Enumeration rejects malformed metadata and inconsistent counts instead of returning partial sets. The prior connectivity port has been removed, including its public API, implementation, enums, tests, and guide. The main source project was read as a reference and was not edited by this increment.

Every new method has comprehensive, expanded XML documentation on both the interface and concrete adapter. The package includes that XML beside the DLL, a NuGet README, and the full [load query guide](Load-Queries.md). README and getting-started claims now match the actual `net481` target and avoid claiming unverified cross-version or shared-session thread safety.

## Verification evidence

| Check | Result | Scope |
| --- | --- | --- |
| Release build | Passed, zero warnings/errors | Library and test project, .NET SDK 9.0.304 / .NET Framework 4.8.1 |
| Automated test suite | **342 executed, 342 passed, 0 failed** | 33 original offline cases plus 309 query/model/documentation cases; 41 added for filtering, 94 since the repository baseline |
| Environment-dependent provider test | One explicit live test not executed | Asserts no running STAAD session; excluded from ordinary offline runs |
| Package creation | Passed | Preview package with `lib/net481/StaadPro.Interop.dll` and XML |
| Archive/metadata checks | Passed | Package identity, MIT metadata, repository URL, README, load/installation/publishing guides, forty-two complete XML query entries, four documented models, no connectivity API leakage |
| Isolated package restore | Passed | Fresh temporary consumer/cache; PackageReference only, no repository ProjectReference |
| Installed-package compilation/runtime | Passed, zero build warnings/errors | All forty-two XML examples, compiled installation example, twenty-one ported APIs including inclusive range/ID filters and concrete family lists, unchanged activation and four models' assignment dispatch using a managed stand-in; restored archive SHA256 matches the supplied package |
| NuGet dependency vulnerability query | No vulnerable dependencies reported | `dotnet list ... package --vulnerable --include-transitive`, configured NuGet sources, assessment date |
| CI verification | Passed for implementation commit `991f7ae` | [Windows CI run](https://github.com/structuraldeveloper/StaadPro.Interop/actions/runs/38230643566) built, tested, packed and verified the implemented queries; subsequent release metadata changes are verified locally before upload |
| Real OpenSTAAD query execution | **Pending** | Native COM dispatch/marshaling, unit conventions, version compatibility |
| NuGet.org publication | **Pending web submission** | User browser login reported; automation native pipe unavailable; no accepted submission/public restore confirmed |

The test result file is `artifacts/load-port/tests/load-port.trx`. Its counters report 343 discovered cases and 342 executed; the remaining case is the explicit ROT/environment test. The console's passed total is 342. Offline passing results are not a certification of live STAAD engineering output.

## What blocks a stable release

### 1. Verify these queries against native STAAD

Execute the known-model validation described in [Load-Queries.md](Load-Queries.md#live-validation-before-release) on each STAAD version and architecture to be advertised. Verify all nodal components, every supported direction, partial/full UDL positions, concentrated and uniform moments, local linear/triangular start/end/middle intensities, partial-span and projected trapezoidal loads, eccentricities, multiple and empty assignments, primary/reference activation, invalid IDs, native fetch statuses, and both metric/imperial read-back behavior. Verify distinct concentrated moment, distributed moment, length, and force-per-length conversion factors. Also verify case enumeration counts/statuses, sparse and overlapping IDs, titles/types and unchanged active case. Verify direct lookup for both families, including missing IDs, and ordered primary batches with duplicate/empty input and failures after earlier records succeed. Record expected and observed values. The installed native 2025 documentation was inspected; no live execution was performed.

The installed Bentley Python wrapper and native reference disagree in a few details. For example, the Python UDL wrapper checks a Boolean-style return while the native reference documents `0` as success. The new C# methods follow the native reference's status semantics and the main C# signatures. Live evidence must settle compatibility before claiming a supported version range.

Verify combination number retrieval on models whose primary/reference/combination counts differ. The installed native return description inconsistently calls its positive result a primary case count; this port interprets it as the retrieved combination count. Also verify reference batches, sparse/empty combinations, title values, membership checks for absent or primary-only IDs, and unchanged activation. This documentation inconsistency remains a live release gate, not a behavior proven by offline tests.

Verify distinct combination batches, including duplicates, empty input and missing membership, then all supported generic families. Confirm null is returned only after a successful family read without the requested ID, and that unrelated native metadata failures still throw. Check default family selection, cross-family IDs and unchanged activation on each advertised STAAD version.

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

1. Review the latest range/ID/list filters alongside the preceding eighteen functions; retain boundary, input snapshot, deduplication/absence, eager ownership and unmatched-error regression tests.
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
dotnet pack src/StaadPro.Interop/StaadPro.Interop.csproj -c Release -p:PackageVersion=1.0.0-preview.20261012 -o artifacts/release
./eng/Verify-LoadPackage.ps1 -PackagePath artifacts/release/StaadPro.Interop.1.0.0-preview.20261012.nupkg
```

The verifier leaves its uniquely named temporary consumer directory in the user's temporary folder so the restored package and executable can be inspected. It makes no changes to a STAAD model and does not publish anything.
