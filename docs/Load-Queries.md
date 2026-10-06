# Load assignment queries

## Scope and provenance

This increment ports exactly three methods from the main project's `ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs` and `Adapters/Models/OSLoadAdapter.cs` into the open source `IOSLoad` contract and `OSLoadAdapter`. The source signatures, result types, parameter names, array mappings, record order, and `LoadCase` reference association are preserved. Private application dependencies and Bentley binaries are not copied.

The earlier connectivity increment has been removed: its three grouping functions, service, scope enums, tests, guide, and verification script are absent. The public geometry interface and adapter are restored to the pre-increment revision. Package and documentation verification now covers load queries.

| Public method | Result type | OpenSTAAD count / fetch pair |
| --- | --- | --- |
| `GetNodalLoads(ILoadCase lc, int nId)` | `List<NodalLoad>` | `GetNodalLoadCount` / `GetNodalLoads` |
| `GetMemberUniformlyDistributedLoads(ILoadCase lc, int mId)` | `List<MemberUniformlyDistributedLoad>` | `GetUDLLoadCount` / `GetUDLLoads` |
| `GetMemberConcentratedLoads(ILoadCase lc, int mId)` | `List<MemberConcentratedLoad>` | `GetConcForceCount` / `GetConcForces` |

All three methods are available through `wrapper.Load` (`IOSLoad`) and the concrete `OSLoadAdapter`. They read **assigned load items**. They do not retrieve reactions, member internal forces, displacements, or other analysis results. They do not create load cases or add, remove, or alter assignments.

Signatures are declared in `Adapters/Interfaces/IOSLoad.cs`; all three implementations and their private helpers reside in `Adapters/Models/OSLoadAdapter.cs`. Adapters use a single, non-partial class. Separate helpers or services may be introduced when needed.

## Before calling

Use Windows with .NET Framework 4.8.1 and a licensed, connected STAAD model exposing these APIs. Keep the wrapper undisposed. The supplied case and entity must already exist in that model. IDs are STAAD numbers, not list offsets. The wrapper checks positivity, while OpenSTAAD determines whether the numbers exist.

`lc` must be non-null, have a positive `Id`, and use `LoadCaseType.PrimaryLoad` or `LoadCaseType.ReferenceLoad`. Combinations, repeat loads, and undefined enum values are rejected before COM access. `Title` and engineering `Type` are descriptive metadata and are not sent by these queries.

Each call executes in this order:

1. Validate arguments before contacting OpenSTAAD.
2. Activate the primary case with `SetLoadActive`, or the reference case with `SetReferenceLoadActive`.
3. Check activation success, then obtain and validate the assignment count for the supplied node/member.
4. Return a fresh empty list when that count is zero; no fetch call is made.
5. Allocate typed buffers for a positive count and fetch all component arrays through `ref object` arguments.
6. Check the fetch status and every array, then construct a complete list of new managed load objects.

The requested case remains active after the operation. If a count or fetch fails after activation, the case still remains active. The previous active case is not restored. Activation failure stops the query before counting.

STAAD's active case is shared session state. Coordinate all queries, assignments, raw COM calls, and other wrappers sharing that session on the appropriate COM thread. These methods provide neither locking across session operations nor COM apartment marshaling. Do not call them concurrently while another operation changes the active case or model.

## Field mappings

### Nodal loads

The same-index elements of six arrays form one `NodalLoad`. Each record gets its own new `Forces` object.

| OpenSTAAD array | Managed property | Quantity |
| --- | --- | --- |
| `varFX` | `Forces.Fx` | Force along X |
| `varFY` | `Forces.Fy` | Force along Y |
| `varFZ` | `Forces.Fz` | Force along Z |
| `varMX` | `Forces.Mx` | Moment about X |
| `varMY` | `Forces.My` | Moment about Y |
| `varMZ` | `Forces.Mz` | Moment about Z |

No component is summed, discarded, or transformed. Distinct assignments remain distinct, including duplicate records. The query does not fetch a `Node` or attach the returned definitions to a node collection.

### Uniform member forces

| OpenSTAAD array | Managed property | Meaning |
| --- | --- | --- |
| `varDirection` | `Direction` | Codes 1–9: LocalX/Y/Z, GlobalX/Y/Z, ProjectedX/Y/Z |
| `varForce` | `Magnitude` | Uniform force per length |
| `varD1` | `SPosition` | Distance from member start to load start |
| `varD2` | `EPosition` | Distance from member start to load end |
| `varD3` | `Eccentricity` | Perpendicular distance from shear center to local loading plane |

Zero start/end sentinels are preserved; the method does not replace them with the member length. Negative magnitudes and eccentricities are preserved. Projected directions are retained, without converting the magnitude to a local or global axis. Uniform moments, trapezoidal loads, and other load families are outside this method's scope.

### Concentrated member forces

| OpenSTAAD array | Managed property | Meaning |
| --- | --- | --- |
| `varDirection` | `Direction` | Codes 1–6: LocalX/Y/Z, GlobalX/Y/Z |
| `varForce` | `Magnitude` | Concentrated force |
| `varD1` | `Position` | Distance from member start to concentrated force |
| `varD2` | `Eccentricity` | Perpendicular distance from shear center to local loading plane |

Projected directions are invalid for concentrated forces. Concentrated moments are not included. Neither member query fetches beam geometry: each returned `Members` collection is empty. Retain the queried member ID in your application's surrounding data if you need that association.

## Units and ownership

Every numeric value is copied directly from the OpenSTAAD output. There is no force, moment, length, or distributed-load scaling. Input units and read-back units must not be assumed to match. Confirm the installed API convention and compare a known assignment in that STAAD version before applying conversions. A conversion from force units alone is insufficient for a moment or UDL; account for the associated length dimension.

Lists and records are new on every query. Modifying a returned list, load, or `Forces` object does not write back to STAAD. COM arrays are copied and are not exposed to the caller. Each result's `LoadCase` points to the **same object supplied as `lc`**, so changing that case object is visible through all associated results. It is not a deep snapshot of case metadata.

## Examples

```csharp
using System;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Services;

using (var wrapper = OpenStaadWrapperProvider.GetRunning())
{
    if (wrapper?.Load == null)
        throw new InvalidOperationException("Connect to a STAAD model exposing OpenSTAAD Load.");

    IOSLoad load = wrapper.Load;
    var primary = new LoadCase(1, "DEAD", LoadCaseType.PrimaryLoad);
    var reference = new LoadCase(101, "EQUIPMENT", LoadCaseType.ReferenceLoad);

    // All IDs below must already exist in the active model.
    var nodal = load.GetNodalLoads(primary, nId: 10);
    foreach (var item in nodal)
        Console.WriteLine($"Fy={item.Forces.Fy}; Mz={item.Forces.Mz}");

    var uniform = load.GetMemberUniformlyDistributedLoads(primary, mId: 201);
    foreach (var item in uniform)
        Console.WriteLine($"{item.Direction}: {item.Magnitude}, start={item.SPosition}, end={item.EPosition}, offset={item.Eccentricity}");

    var concentrated = load.GetMemberConcentratedLoads(reference, mId: 201);
    foreach (var item in concentrated)
        Console.WriteLine($"{item.Direction}: {item.Magnitude}, position={item.Position}, offset={item.Eccentricity}");
    // Reference case 101 remains active here.
}
```

Use the list's `Count` to check for absence. Do not treat an exception as an empty list; a failed query and a valid model without that type of assignment are different outcomes.

## Errors and safeguards

| Exception | Trigger |
| --- | --- |
| `ArgumentNullException` (`lc`) | Missing case |
| `ArgumentOutOfRangeException` (`lc`, `nId`, or `mId`) | Nonpositive case or entity ID |
| `NotSupportedException` | Combination, repeat, or undefined case type |
| `InvalidOperationException` | Activation/count/fetch failure or malformed payload |
| `COMException` | Native COM failure; propagated unchanged |
| `RuntimeBinderException` | Required dynamic API signature unavailable; propagated |

Primary activation must report `true` (or an integer Boolean representation, `1`/`-1`). Reference activation must return the requested positive case ID; errors such as `-1` and `-8002` are rejected. Counts must be nonnegative, representable as a CLR array length, and integral. Nulls, strings, Booleans, and floating-point status/count values are not coerced. Signed `Int16`, `Int32`, and `Int64` representations are supported.

Bentley's native 2025 API reference specifies **0 = OK** for all three fetch methods. Every other fetch status is rejected, even if the arrays contain plausible values. All component arrays must be one dimensional, exactly match the count, and have `Double` elements (`Int32` for directions). Both zero and nonzero SAFEARRAY lower bounds are handled. Null, oversized, undersized, multidimensional, or incorrectly typed arrays fail the whole query. Non-finite values fail the query. Double buffers start as NaN so untouched components cannot masquerade as legitimate zero loads.

These safeguards extend the main project's implementation, which casts arrays directly and ignores activation/fetch statuses. No partially populated list is returned on failure. They cannot detect a concurrent external model change that produces a self-consistent payload; session serialization remains the caller's responsibility.

## IntelliSense and automated verification

Both interface and concrete methods contain expanded XML comments: summaries, every parameter, returns, field mappings, unit behavior, ownership, active-case effects, COM threading constraints, six exception categories, and examples. The build emits `StaadPro.Interop.xml`, and NuGet places it beside `StaadPro.Interop.dll` under `lib/net481/`.

`OSLoadQueryTests` verifies component mapping, record order, signs, case identity, both activation paths, fresh object ownership, replacement/in-place output buffers, zero counts, invalid arguments, count/status failures, every malformed component, unwritten buffers, all supported directions, zero sentinels, nonzero SAFEARRAY bounds, COM exception propagation, and missing signatures. `OSLoadQueryDocumentationTests` validates all six compiled documentation entries and matches interface/concrete text.

`eng/Verify-LoadPackage.ps1` checks the actual NuGet archive, creates an isolated `net481` consumer with a fresh package cache and no project reference, compiles and executes all six packaged XML examples, and exercises all three installed APIs for primary/reference/empty cases. This consumer uses a managed stand-in; it is not a live COM compatibility test.

```powershell
dotnet test tests/StaadPro.Interop.Tests/StaadPro.Interop.Tests.csproj -c Release
dotnet pack src/StaadPro.Interop/StaadPro.Interop.csproj -c Release -p:PackageVersion=1.0.0-preview.20261006.load1 -o artifacts/load-port/package
./eng/Verify-LoadPackage.ps1 -PackagePath artifacts/load-port/package/StaadPro.Interop.1.0.0-preview.20261006.load1.nupkg
```

The existing `OpenStaadWrapperProvider_DefaultGetRunning_ReturnsNullWhenNoStaad` test is explicitly marked `LiveIntegration`: it asserts an environmental precondition, namely that no STAAD session is running. Ordinary offline runs leave it unexecuted. It does not verify these three queries.

## Live validation before release

Use an isolated known model, rather than a user's working model. For each STAAD version advertised as supported, verify primary and reference cases, a node with distinct signed values in all six components, a member with full-span and partial UDLs, projected UDL directions, a member with concentrated forces and eccentricities, and entities with no such assignments. Compare every output against the model's definitions under both metric and imperial conventions, with at least one input-unit change. Exercise nonexistent IDs and case activation failures. Record application build, process architecture, units, COM statuses, payload element types, expected values, and observed values. Confirm that assignments are unchanged and the requested case remains active.

The offline suite and package consumer do not certify native COM SAFEARRAY marshaling, live unit conventions, or all STAAD versions. These remain explicit stable-release gates in [NuGet-Readiness.md](NuGet-Readiness.md).

## Sources inspected

- Main project: `src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs` and `Adapters/Models/OSLoadAdapter.cs`, at main repository revision `41dfc15ae34b7d530a1d5a46c8ee8dad063ccd4f`.
- Installed Bentley STAAD.Pro 2025 native programmer reference: `OSAPP_Help/group___property_load_nodal.html`, `group___property_load_mem.html`, `group___property_load_case.html`, and `group___property_load_def_ref_load.html`. These are under the installed STAAD.Pro 2025 directory; consult your own installation for the vendor documentation.
- Installed Bentley `openstaadpy-25.0.1.1` source was inspected as an additional reference. It conflicts with the native documentation in some places (notably UDL Boolean status handling). This port follows the native status contract and the main C# method signatures; live verification is still required.
