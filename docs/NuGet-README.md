# StaadPro.Interop

A strongly typed wrapper for Bentley STAAD.Pro OpenSTAAD geometry and load operations.

## Requirements

- Windows and .NET Framework **4.8.1** (`net481`). This package does not currently target .NET 8.
- A licensed STAAD.Pro installation exposing the OpenSTAAD methods used by your application. Bentley binaries are not distributed in this package or required to build it.
- A connected model session, with COM access coordinated by the calling application. This wrapper does not guarantee thread safety across a shared STAAD session.

## Load assignment queries

The package exposes six read-back methods through `IOSLoad` and `OSLoadAdapter`:

- `GetNodalLoads(ILoadCase lc, int nId)` returns nodal forces and moments.
- `GetMemberUniformlyDistributedLoads(ILoadCase lc, int mId)` returns member uniform forces.
- `GetMemberConcentratedLoads(ILoadCase lc, int mId)` returns member point forces.
- `GetMemberConcentratedMoments(ILoadCase lc, int mId)` returns member point moments.
- `GetMemberLinearVaryingLoads(ILoadCase lc, int mId)` returns local full-member linear/triangular force intensities.
- `GetMemberTrapezoidalLoads(ILoadCase lc, int mId)` returns varying endpoint intensities and loaded-span distances.

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

The selected case remains active after each query. Values preserve OpenSTAAD signs, order, and raw units without conversion. An empty list means a successful zero count; failures and malformed data throw. These methods read assignments rather than analysis results.

Full XML IntelliSense accompanies the DLL for both interface and concrete methods, including parameter contracts, mapping, side effects, exceptions, and examples. The package also includes `docs/Load-Queries.md` with field mappings and verification guidance.

## Release status

Package creation and isolated consumer checks can be run locally and in CI. Offline tests exercise a managed OpenSTAAD stand-in; live COM marshaling and engineering unit conventions still require verification on each advertised STAAD version before a stable release. See the repository's [release assessment](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/NuGet-Readiness.md).

MIT licensed. Bentley STAAD.Pro is a separate product.
