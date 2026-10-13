# StaadPro.Interop

A strongly typed wrapper for Bentley STAAD.Pro OpenSTAAD geometry and load operations.

## Requirements

- Windows and .NET Framework **4.8.1** (`net481`). This package does not currently target .NET 8.
- A licensed STAAD.Pro installation exposing the OpenSTAAD methods used by your application. Bentley binaries are not distributed in this package or required to build it.
- A connected model session, with COM access coordinated by the calling application. This wrapper does not guarantee thread safety across a shared STAAD session.

## Geometry grouping and IntelliSense

`IOSGeometry`, `OSGeometryAdapter` and the standalone `ConnectivityService` expose `SegregateBeamsByConnectivity`, `SegregateMembersByConnectivity` and `SegregatePlatesByConnectivity`. These functions group supplied managed entities without accessing STAAD. Member grouping defaults to `MemberConnectivityScope.EndpointsOnly`; plate grouping defaults to `PlateConnectivityScope.SharedEdge`. See the packaged `docs/Geometry-Porting.md` for behavior, source comparison and remaining methods.

`CreateMultipleNodesExtended`, `CreateMultipleBeamsExtended` and `MergeNodesExtended` now use the original root-parameter signatures through `wrapper.Root`. Node creation sends an XYZ matrix; beam creation uses ordered native `CreateBeam` calls; merging includes the retained node ID. Native mutations do not refresh managed geometry or supply rollback. See `docs/Geometry-Porting.md` for validation and partial-failure behavior.

`IOSLoad` and `OSLoadAdapter` also expose `AddReferenceLoadsTo`, `AddRepeatLoadsTo` and the synchronous `AddAxialThermalGradientTo` overload. They validate inputs before activating an existing primary case, preserve raw factors/thermal values and leave the target case active. See the packaged `docs/Load-Porting.md` for complete contracts and remaining functions.

The four-parameter `AddAxialThermalGradientTo` overload is also available. It validates `nThreads` and preserves per-entity scalar assignments while keeping COM calls on the caller's thread. `AddMemberLoadsFromGroupsByLoadCase` and `AddNodalLoadsFromGroupsByLoadCase` apply tuples to the cases identified by their dictionary keys through the existing assignment helpers. See `docs/Load-Porting.md` for skip/null behavior, legacy activation-status handling and partial failures.

Full, expanded XML IntelliSense is included for all 140 geometry overloads and all 72 load overloads on both interfaces and concrete adapters. Shared source comments are expanded by the compiler; installed consumers need only `StaadPro.Interop.xml` beside the DLL.

Envelope metadata queries `GetEnvelopeIds()`, `GetLoadEnvelopeDetail(int envId)` and `GetAllLoadEnvelopeDetails()` return fresh IDs, classification/count/membership records, and complete envelope sets. They check native success status 1, reject invalid/malformed data and preserve the active case/load list. Successful zero counts produce non-null empty sets; a failed read throws. The standalone `LoadEnvelopeDetail` model and `LoadEnvelopType` enum accompany these functions. See the shipped `docs/Load-Queries.md` for details.

## Load API batch 1: AddSelfweightInXYZTo, CreateLoadCombinations, CreateLoadEnvelopExt

Added the pending thread-count `AddSelfweightInXYZTo` overload, `CreateLoadCombinations`, and editor-definition `CreateLoadEnvelopExt`. Preserved the existing typed self-weight and integer-envelope APIs brought in from origin/main. Added managed combination definition types, eager input filtering/validation, entity-scoped caller-thread self-weight and checked envelope creation.

## Load API batch 2: GetLoadCombination, GetMemberLoadsGroupedByLoadCase, GetNodalLoadsGroupedByLoadCase

Added combination coefficient read-back with validated Boolean native status, typed arrays and primary-case membership. SRSS overall multipliers are rejected because the return model cannot represent them. Added eager member/nodal read-and-group APIs that refresh managed lists, use original case identities, exact definition fields and duplicate-assignment buckets. Input sequences are snapshotted once before clearing; native failures may leave partial refreshes.

## Load assignment queries

The root port also includes `GetShortJobInfo()`, `GetFullJobInfo()` and `SetInputUnitsEx(LengthInputUnit, ForceInputUnit)`. The getters return standalone, editable `ShortJobInfo`/`FullJobInfo` snapshots, validating all native outputs and parsing dates using CurrentCulture; unspecified dates use DateTime.MinValue. The unit setter validates enum codes before changing native input-unit state. Full mappings, side effects, error contracts and examples are shipped in `docs/Root-Porting.md`. Root coverage is nine methods, with fourteen pending.

New load mutations include `AddSelfweightInXYZTo(int, LoadCaseType, IEnumerable<int>, SelfWeightDirection, double)`, `CreateLoadEnvelopExt(int, LoadEnvelopType, IEnumerable<int>)` and `CreateLoadList(LoadListType, IEnumerable<int>)`. They snapshot/validate inputs before native calls and check each API's distinct success contract. Self-weight activates a primary/reference target; envelope creation and load-list selection do not activate a case. These are native mutations with no automatic save or rollback; see `docs/Load-Porting.md` for full contracts.

`wrapper.Root` exposes `GetStaadFileFullPath()`, `GetStaadFileNameOnly()`, `GetAnlFileFullPath()`, `GetOpenedStaadFilename()`, `IsTarget(string)` and `ModelIsOpen(string)`. Each uses one native model-path read. Identity predicates validate and compare complete normalized paths with case-insensitive matching; equal basenames in different directories do not match. An explicitly unnamed session yields empty filename text and false identity results. The `.ANL` path does not prove file existence/current results. Matching interface/concrete IntelliSense and shipped `docs/Root-Porting.md` cover the six filename/identity methods and 14 pending reference root methods.

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

Release `1.0.0-preview.20261013.8` is authored by **Srinivasa Rao Masanam**. Copyright (c) 2026 StaadDev. This prepared preview adds the four-parameter thermal overload and grouped member/nodal load assignments with detailed XML IntelliSense. It retains the earlier geometry, factored-load, connectivity and model-attachment additions. See [release notes](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Release-Notes.md), also included as `docs/Release-Notes.md` in the package. This version has not been published to NuGet.org.

Package creation and isolated consumer checks can be run locally and in CI. Offline tests exercise a managed OpenSTAAD stand-in; live COM marshaling and engineering unit conventions still require verification on each advertised STAAD version before a stable release. See the repository's [release assessment](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/NuGet-Readiness.md).

MIT licensed. Bentley STAAD.Pro is a separate product.

See the repository's [installation guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Installation.md) for `net481`, public/local feeds and consumer examples, and [publishing guide](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/Publishing.md) for web upload, public availability checks and later release automation. Both are also included under the package's `docs` directory.
