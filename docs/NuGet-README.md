# StaadPro.Interop

A strongly typed wrapper for Bentley STAAD.Pro OpenSTAAD geometry and load operations.

## Requirements

- Windows and .NET Framework **4.8.1** (`net481`). This package does not currently target .NET 8.
- A licensed STAAD.Pro installation exposing the OpenSTAAD methods used by your application. Bentley binaries are not distributed in this package or required to build it.
- A connected model session, with COM access coordinated by the calling application. This wrapper does not guarantee thread safety across a shared STAAD session.

## Load assignment queries

The package exposes seven assignment read-back methods through `IOSLoad` and `OSLoadAdapter`:

- `GetNodalLoads(ILoadCase lc, int nId)` returns nodal forces and moments.
- `GetMemberUniformlyDistributedLoads(ILoadCase lc, int mId)` returns member uniform forces.
- `GetMemberConcentratedLoads(ILoadCase lc, int mId)` returns member point forces.
- `GetMemberConcentratedMoments(ILoadCase lc, int mId)` returns member point moments.
- `GetMemberLinearVaryingLoads(ILoadCase lc, int mId)` returns local full-member linear/triangular force intensities.
- `GetMemberTrapezoidalLoads(ILoadCase lc, int mId)` returns varying endpoint intensities and loaded-span distances.
- `GetMemberUniformMoments(ILoadCase lc, int mId)` returns uniform moment intensity, span and offset.

`GetAllPrimaryLoadCases()` and `GetAllReferenceLoadCases()` return fresh `HashSet<ILoadCase>` metadata sets without changing the active case. Both validate native counts, IDs, titles and engineering types; set ordering is unspecified.

`GetPrimaryLoadCaseFromId(int lcId)` and `GetReferenceLoadCaseFromId(int lcId)` read metadata for a known positive ID without activating or enumerating cases. `GetPrimaryLoadCasesFromIds(IEnumerable<int> loadCasesIds)` eagerly returns primary metadata in input order, preserving duplicates as independent objects. It validates the entire input before COM access; empty input produces a fresh empty list. Native errors and malformed metadata throw rather than returning partial results.

`GetReferenceLoadCasesFromIds(IEnumerable<int> loadCasesIds)` provides the same batch contract for reference metadata. `GetAllLoadCombinationCases()` discovers combination metadata, and `GetLoadCombinationCaseFromId(int lcId)` verifies combination membership before reading a title. Combination results use `Type=None`; factors and constituent cases are not loaded. All three methods leave the active case unchanged and throw on malformed/native-error data.

`GetLoadCombinationCasesFromIds(IEnumerable<int> lcIds)` reads a distinct combination set, collapsing duplicate IDs with unspecified output order. It enumerates input once and validates all memberships before reading titles. `GetAllLoadCasesOfType(LoadCaseType loadCaseType = LoadCaseType.PrimaryLoad)` selects a primary/reference/combination metadata family. `GetLoadCase(int lcId, LoadCaseType loadCaseType)` reads that whole family and returns the matching object or null if the positive ID is absent; native errors still throw. Repeat/undefined families are unsupported. These eager reads preserve active-case state and provide fresh metadata.

`GetLoadCases(int fromId, int toId, LoadCaseType loadCaseType)` filters inclusive positive ID ranges. `GetLoadCasesFromIds(IEnumerable<int> loadCasesIds, LoadCaseType loadCaseType)` filters a family by requested IDs, omitting missing values and collapsing duplicates. `GetLoadCasesOfType(LoadCaseType loadCaseType)` returns a concrete family list. All are eager metadata reads with unchanged active-case state and strict errors, including unmatched metadata failures.

```csharp
using System;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Services;

using (var wrapper = OpenStaadWrapperProvider.GetRunning())
{
    if (wrapper?.Load == null)
        throw new InvalidOperationException("Open a STAAD model with OpenSTAAD available first.");

    // These IDs must already exist in the currently open model.
    var lc = new LoadCase(1, "DEAD");
    foreach (var item in wrapper.Load.GetNodalLoads(lc, nId: 10))
        Console.WriteLine(item.Forces.Fy);
}
```

The selected case remains active after each assignment query. Values preserve OpenSTAAD signs, order, and raw units without conversion. An empty list means a successful zero count; failures and malformed data throw. These methods read assignments rather than analysis results.

Full XML IntelliSense accompanies the DLL for both interface and concrete methods, including parameter contracts, mapping, side effects, exceptions, and examples. The package also includes `docs/Load-Queries.md` with field mappings and verification guidance.

## Release status

Package creation and isolated consumer checks can be run locally and in CI. Offline tests exercise a managed OpenSTAAD stand-in; live COM marshaling and engineering unit conventions still require verification on each advertised STAAD version before a stable release. See the repository's [release assessment](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/NuGet-Readiness.md).

MIT licensed. Bentley STAAD.Pro is a separate product.

See the repository's [installation guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Installation.md) for `net481`, public/local feeds and consumer examples, and [publishing guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Publishing.md) for web upload, public availability checks and later release automation. Both are also included under the package's `docs` directory.
