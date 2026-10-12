# StaadPro.Interop release notes

## 1.0.0-preview.20261012 — 12 October 2026

Author: **Srinivasa Rao Masanam**. Copyright (c) 2026 Srinivasa Rao Masanam. License: MIT.

This preview targets Windows and **.NET Framework 4.8.1 (`net481`)**. It contains the existing geometry/load wrapper and twenty-one load-query functions ported from the main application's interop project. The package ID is `StaadPro.Interop`; its version is `1.0.0-preview.20261012`. Public availability requires NuGet.org submission, acceptance and public-feed verification, tracked in the [readiness assessment](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/NuGet-Readiness.md).

### Latest additions

- `GetLoadCases(int fromId, int toId, LoadCaseType loadCaseType)` returns fresh metadata for an inclusive positive ID range. Invalid bounds are rejected before COM access; sparse ranges do not require generating every integer.
- `GetLoadCasesFromIds(IEnumerable<int> loadCasesIds, LoadCaseType loadCaseType)` snapshots input once, validates IDs before COM access, collapses duplicate requests and omits missing IDs. Empty input returns a fresh empty list without native reads after validating the selected family.
- `GetLoadCasesOfType(LoadCaseType loadCaseType)` returns a fresh concrete `List<ILoadCase>` for the selected primary, reference or combination family.

These methods read the complete selected family eagerly and preserve the active load case. Metadata failures propagate even for cases excluded by a filter. Output ordering is unspecified. Repeat and undefined families are unsupported. Results contain metadata; they do not load assignments, combination factors or analysis results.

### Other load queries included in this preview

Seven assignment readers return nodal forces/moments and member distributed forces, point forces, point moments, linear varying loads, trapezoidal loads and uniform moments:

- `GetNodalLoads`
- `GetMemberUniformlyDistributedLoads`
- `GetMemberConcentratedLoads`
- `GetMemberConcentratedMoments`
- `GetMemberLinearVaryingLoads`
- `GetMemberTrapezoidalLoads`
- `GetMemberUniformMoments`

Eleven other metadata queries provide family discovery, direct lookup, batch lookup and generic family selection:

- `GetAllPrimaryLoadCases`, `GetAllReferenceLoadCases`, `GetAllLoadCombinationCases`
- `GetPrimaryLoadCaseFromId`, `GetReferenceLoadCaseFromId`, `GetLoadCombinationCaseFromId`
- `GetPrimaryLoadCasesFromIds`, `GetReferenceLoadCasesFromIds`, `GetLoadCombinationCasesFromIds`
- `GetAllLoadCasesOfType`, `GetLoadCase`

Primary/reference batches preserve requested order and duplicates as separate objects; combination batches collapse duplicates and validate all memberships before reading titles. Generic lookup returns null for a missing positive ID after successfully reading the selected family. Assignment readers activate the supplied case and preserve native signs, order and raw units. Family metadata readers leave active-case state unchanged. See [Load-Queries.md](Load-Queries.md) and the [API reference](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/API-Reference.md) for complete signatures, contracts and examples.

### Validation and documentation

- Inputs, native statuses, counts, arrays, directions and metadata are checked; malformed responses throw rather than returning partial results. SAFEARRAYs with nonzero lower bounds are supported by the assignment readers.
- Uniform moment model conversion uses moment divided by length for intensity and treats eccentricity as a length. This corrects the main model's dimensional conversion and omitted eccentricity conversion.
- All twenty-one ported functions have expanded XML IntelliSense on both `IOSLoad` and `OSLoadAdapter`, including parameters, return values, exceptions, side effects and executable examples. Adapter implementations remain in a single non-partial class.
- The package ships XML beside the DLL, a NuGet README, load-query documentation, release notes, and [installation](Installation.md)/[publishing](Publishing.md) guides.
- Release verification covers **342 passing offline tests**, **42 compiled and executed XML examples**, and a compiled installation example in an isolated PackageReference consumer using a fresh cache. The managed stand-in exercises mapping and error contracts; it does not certify native STAAD behavior.

### Compatibility and remaining validation

A licensed STAAD.Pro installation exposing the required OpenSTAAD APIs is needed for actual model access. Bentley binaries and the private main application are not bundled. Newtonsoft.Json 13.0.3 is a NuGet dependency. Shared-session COM coordination remains the application's responsibility.

Live COM dispatch/marshaling, engineering unit read-back and STAAD version/architecture compatibility remain pending. Native combination-number return-count documentation also requires validation with differing family counts. The readiness assessment records these and inherited implementation gates before a stable release. This preview makes no claim of verified support across STAAD versions.

### Installation

After this exact version becomes publicly available:

```powershell
dotnet add ./YourApp.csproj package StaadPro.Interop --version 1.0.0-preview.20261012 --source https://api.nuget.org/v3/index.json
```

Enable prerelease packages in Visual Studio's NuGet UI. The verified archive can also be installed through a local feed before publication; see [Installation.md](Installation.md).
