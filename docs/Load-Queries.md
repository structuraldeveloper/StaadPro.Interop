# Load assignment and case queries

## Scope and provenance

Three increments port nine methods from the main project's `ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs` and `Adapters/Models/OSLoadAdapter.cs` into the open source `IOSLoad` contract and `OSLoadAdapter`. The first added nodal, uniform member, and concentrated member force queries. The second added concentrated member moments, linear varying forces, and trapezoidal forces. The third adds uniform member moments with their required model, and primary/reference case discovery. Public signatures and assignment mappings are preserved. Private application dependencies and Bentley binaries are not copied. Corrections to reference metadata lookup and uniform-moment conversion are described below.

The earlier connectivity increment has been removed: its three grouping functions, service, scope enums, tests, guide, and verification script are absent. The public geometry interface and adapter are restored to the pre-increment revision. Package and documentation verification now covers load queries.

| Public method | Result type | OpenSTAAD count / fetch pair |
| --- | --- | --- |
| `GetNodalLoads(ILoadCase lc, int nId)` | `List<NodalLoad>` | `GetNodalLoadCount` / `GetNodalLoads` |
| `GetMemberUniformlyDistributedLoads(ILoadCase lc, int mId)` | `List<MemberUniformlyDistributedLoad>` | `GetUDLLoadCount` / `GetUDLLoads` |
| `GetMemberConcentratedLoads(ILoadCase lc, int mId)` | `List<MemberConcentratedLoad>` | `GetConcForceCount` / `GetConcForces` |
| `GetMemberConcentratedMoments(ILoadCase lc, int mId)` | `List<MemberConcentratedMoment>` | `GetConcMomentCount` / `GetConcMoments` |
| `GetMemberLinearVaryingLoads(ILoadCase lc, int mId)` | `List<MemberLinearVaryingLoad>` | `GetLinearVaryingLoadCount` / `GetLinearVaryingLoads` |
| `GetMemberTrapezoidalLoads(ILoadCase lc, int mId)` | `List<MemberTrapezoidalLoad>` | `GetTrapLoadCount` / `GetTrapLoads` |
| `GetMemberUniformMoments(ILoadCase lc, int mId)` | `List<MemberUniformMoment>` | `GetUNIMomentCount` / `GetUNIMoments` |

All seven assignment methods are available through `wrapper.Load` (`IOSLoad`) and the concrete `OSLoadAdapter`. They read **assigned load items**. They do not retrieve reactions, member internal forces, displacements, or other analysis results. They do not create load cases or add, remove, or alter assignments. The two case discovery methods described below are also available through both types.

Signatures are declared in `Adapters/Interfaces/IOSLoad.cs`; all implementations and their private helpers reside in `Adapters/Models/OSLoadAdapter.cs`. Adapters use a single, non-partial class. Separate helpers or services may be introduced when needed.

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

Projected directions are invalid for concentrated forces. Concentrated moments are read through the separate method below. No member query fetches beam geometry: each returned `Members` collection is empty. Retain the queried member ID in your application's surrounding data if you need that association.

### Concentrated member moments

`GetMemberConcentratedMoments` returns assigned moment items, separately from force assignments and analysis results.

| OpenSTAAD array | Managed property | Meaning |
| --- | --- | --- |
| `varDirection` | `Direction` | Codes 1–6: moment about LocalX/Y/Z or GlobalX/Y/Z |
| `varMoment` | `Magnitude` | Signed moment, with force-times-length dimensions |
| `varD1` | `Position` | Distance from member start to the applied moment |
| `varD2` | `Eccentricity` | Perpendicular distance from shear center to loading plane |

Both location and offset are copied without scaling. Projected directions are rejected for this family. A nodal moment and a concentrated member moment are separate assignment types.

### Linearly varying member forces

`GetMemberLinearVaryingLoads` preserves all three force-per-length intensities of the native full-member linear/triangular definition.

| OpenSTAAD output order | Managed property | Meaning |
| --- | --- | --- |
| `varDirection` | `Direction` | Codes **1–3 only**: LocalX/Y/Z |
| `varW1` | `WStart` | Intensity at member start |
| `varW2` | `WEnd` | Intensity at member end |
| `varW3` | `WMiddle` | Intensity at member middle, for triangular loading |

**Native output is start/end/middle, but `MemberLinearVaryingLoad(direction, wStart, wMiddle, wEnd)` accepts start/middle/end.** The adapter maps each output to the named property before calling the constructor. For native values `11, 21, 31`, the properties are `WStart=11`, `WEnd=21`, `WMiddle=31`. `ApplyTo` sends them back in native start/end/middle order. A dedicated regression test uses distinct signed values to detect swapped endpoints/middle values.

Global and projected directions are invalid even though the shared `LoadDirection` enum contains them. No intensity is inferred, interpolated, summed, or dropped when zero. This family has no loaded-span position or eccentricity output; use trapezoidal loads for that separate endpoint/span representation.

### Trapezoidal member forces

`GetMemberTrapezoidalLoads` returns distinct endpoint intensities and loaded-span distances.

| OpenSTAAD array | Managed property | Meaning |
| --- | --- | --- |
| `varDirection` | `Direction` | Codes 1–9: local/global/projected force direction |
| `varW1` | `WStart` | Force-per-length intensity at loading start |
| `varW2` | `WEnd` | Force-per-length intensity at loading end |
| `varD1` | `SPosition` | Distance from member start to loading start |
| `varD2` | `EPosition` | Distance from member start to loading end |

The method preserves partial spans, signs, projected axes, and zero endpoint sentinels without normalization. It does not fetch the member length or calculate a resultant force.

### Uniform member moments

`GetMemberUniformMoments` reads UMOM assignments separately from concentrated moments or member force loads.

| Native array | Managed property | Meaning |
| --- | --- | --- |
| `varDirection` | `Direction` | Codes 1–9: local/global/projected axes |
| `varMoment` | `Magnitude` | Signed distributed moment intensity (moment per length) |
| `varD1` | `SPosition` | Distance from member start to loading start |
| `varD2` | `EPosition` | Distance from member start to loading end |
| `varD3` | `Eccentricity` | Signed perpendicular offset from the shear center |

Records retain full/partial-span positions and projected direction codes. Zero end denotes the member end and remains zero; no member geometry is fetched. Each definition has an empty `Members` collection and shares the supplied `LoadCase`.

A distributed moment has dimensions `(force × length) / length`. Therefore converting kN-m/m to N-cm/cm multiplies intensity by **1000**, while positions and eccentricity multiply by **100**. A concentrated moment converts by **100000** in the same unit change. The main model used the concentrated moment factor for uniform intensity and omitted eccentricity conversion; the port corrects both and tests the inverse conversion too. Queries themselves never convert values. Confirm the installed API's actual source units before using `ConvertUsing`.

### Supporting load models

The four ported sealed entities derive from `MemberLoad` and work with the existing `AddMemberLoad` overloads. Their typed constructors, properties, `DeepCopy`, `ConvertUsing`, and `ApplyTo` methods have XML documentation. The port replaces the main application's Bentley COM types, external converter, and cloning extension with this repository's dynamic dispatch, local `UnitsConverter`, and explicit value copies.

- `DeepCopy` produces a new definition with copied numeric values/direction and the same `LoadCase` reference. Its `Members` collection is fresh and empty, matching the existing open source load-copy convention. It does not clone beam geometry or case metadata.
- `ConvertUsing` mutates the definition in place. Concentrated moment magnitude uses `MomentConversionFactor`; its position/eccentricity use `LengthConversionFactor`. Uniform moment intensity uses `MomentConversionFactor / LengthConversionFactor`, equivalent to `ForceConversionFactor`; its start, end and eccentricity use `LengthConversionFactor`. Varying/trapezoidal intensities use `LinearLoadConversionFactor`; trapezoidal positions use `LengthConversionFactor`. A null converter is a no-op. Choose source units from the actual read-back convention before converting.
- `ApplyTo` uses `AddMemberConcMoment`, `AddMemberLinearVari`, `AddMemberTrapezoidal`, or `AddMemberUniformMoment` without automatic unit conversion. A null Load object, null member array, or empty member array is a no-op. It follows the inherited assignment contract: native return statuses are not exposed, and thrown COM/binding failures propagate. The query methods' strict status checks do not change that existing assignment path.
- A default `MemberLinearVaryingLoad` uses **LocalY**, because this family's native methods allow only local directions. The main model inherited GlobalY, which is outside that contract. Parameterized constructors preserve the supplied direction; callers must supply a supported direction when assigning a definition. The query rejects unsupported returned codes.

## Discovering primary and reference load cases

| Method | Result | Native metadata calls |
| --- | --- | --- |
| `GetAllPrimaryLoadCases()` | `HashSet<ILoadCase>` | `GetPrimaryLoadCaseCount`, `GetPrimaryLoadCaseNumbers`, `GetLoadCaseTitle`, `GetLoadType` |
| `GetAllReferenceLoadCases()` | `HashSet<ILoadCase>` | `GetReferenceLoadCaseCount`, `GetReferenceLoadCaseNumbers`, `GetReferenceLoadCaseTitle`, `GetReferenceLoadType` |

These parameterless methods discover existing cases without case activation. They leave the session's active case unchanged and read metadata only. They exclude combination and repeat-load discovery. A licensed connected model and serialized access on the appropriate COM thread are still required.

The algorithm validates the count, returns a fresh empty set for zero, then allocates an Int32 ID buffer and fetches the numbers. **The native number-retrieval return value is the case count, not a zero success code.** It must equal the preceding count. A negative error, noninteger return or changed count throws `InvalidOperationException`; there is no silent retry or partially returned set.

IDs must form a one-dimensional Int32 array with exactly the expected length; nonzero lower bounds are supported. Every ID must be positive and unique. All IDs are validated before any metadata request, so unwritten zero entries and duplicates are rejected rather than silently discarded by the set.

For each ID, the corresponding family's title and engineering type are retrieved. Titles must be strings and retain whitespace, Unicode, empty text, and literal `NONE`. Types must be signed integer codes defined by `LoadType` (0–23, including `None=23`); negative errors and unknown values throw. A valid result sets `Id`, `Title`, `CaseType` and `Type` on a newly allocated `LoadCase`. Returned cases contain no assignment collections.

The main implementation queried reference types with `GetLoadType(id)`, whose documented ID argument refers to primary cases. This port uses `GetReferenceLoadType(id)` so equal primary/reference numbers receive their own metadata. Tests deliberately give those families overlapping IDs and different classifications/titles.

Each call returns a new set and independent case objects. Editing them does not write to STAAD. Iteration order is unspecified; sort with LINQ if presentation order matters. `LoadCase` equality/hashing includes `Id`, `CaseType` and `Type`: remove a case before changing those fields, or rebuild the set afterward. Titles can change without affecting its hash.

Enumeration errors are `InvalidOperationException` for malformed/inconsistent data, and propagated `COMException`/`RuntimeBinderException` for native or missing-signature failures. A read is not an atomic snapshot; avoid concurrent model editing even when the payload passes validation.

```csharp
var primaryCases = load.GetAllPrimaryLoadCases();
var referenceCases = load.GetAllReferenceLoadCases();
foreach (var lc in referenceCases)
{
    Console.WriteLine(lc.Id + ": " + lc.Title + " (" + lc.Type + ")");
    // Reading assignments activates this reference case and leaves it active.
    var moments = load.GetMemberUniformMoments(lc, mId: 201);
}
```

`OSLoadCaseQueryTests` covers both families, sparse/overlapping IDs, all documented engineering types, replacement/in-place and nonzero-bound arrays, exact call order, independent ownership, zero counts, count mismatches/errors, invalid IDs/titles/types, and native/binding failures. Its stand-in intentionally has no activation methods, so accidental activation causes a failure.

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

    var moments = load.GetMemberConcentratedMoments(reference, mId: 201);
    foreach (var item in moments)
        Console.WriteLine($"Moment={item.Magnitude}, position={item.Position}, offset={item.Eccentricity}");

    var varying = load.GetMemberLinearVaryingLoads(reference, mId: 201);
    foreach (var item in varying)
        Console.WriteLine($"Start={item.WStart}, middle={item.WMiddle}, end={item.WEnd}");

    var trapezoidal = load.GetMemberTrapezoidalLoads(reference, mId: 201);
    foreach (var item in trapezoidal)
        Console.WriteLine($"{item.WStart} to {item.WEnd}, from {item.SPosition} to {item.EPosition}");
    var uniformMoments = load.GetMemberUniformMoments(reference, mId: 201);
    foreach (var item in uniformMoments)
        Console.WriteLine($"{item.Magnitude}, start={item.SPosition}, end={item.EPosition}, offset={item.Eccentricity}");
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

Bentley's native 2025 API reference specifies **0 = OK** for all seven fetch methods. Every other fetch status is rejected, even if the arrays contain plausible values. All component arrays must be one dimensional, exactly match the count, and have `Double` elements (`Int32` for directions). Both zero and nonzero SAFEARRAY lower bounds are handled. Null, oversized, undersized, multidimensional, or incorrectly typed arrays fail the whole query. Non-finite values fail the query. Double buffers start as NaN so untouched components cannot masquerade as legitimate zero loads.

These safeguards extend the main project's implementation, which casts arrays directly and ignores activation/fetch statuses. No partially populated list is returned on failure. They cannot detect a concurrent external model change that produces a self-consistent payload; session serialization remains the caller's responsibility.

## IntelliSense and automated verification

Both interface and concrete methods contain expanded XML comments: summaries, every parameter, returns, field mappings, unit behavior, ownership, active-case effects, COM threading constraints, six exception categories for assignment reads (three for case discovery), and examples. The build emits `StaadPro.Interop.xml`, and NuGet places it beside `StaadPro.Interop.dll` under `lib/net481/`.

`OSLoadQueryTests` verifies all seven assignment queries' component mapping, record order, signs, case identity, both activation paths, fresh object ownership, replacement/in-place output buffers, zero counts, invalid arguments, count/status failures, every malformed component, unwritten buffers, all supported directions, zero sentinels, nonzero SAFEARRAY bounds, COM exception propagation, and missing signatures. It includes the linear start/end/middle regression. `MemberLoadDefinitionTests` verifies constructors/copies, independent definitions, dimensional conversions, assignment dispatch under both case types, native argument order, missing-input handling, and exception propagation. `OSLoadQueryDocumentationTests` validates all eighteen compiled method entries, matches interface/concrete text, and checks documentation for all four ported models.

`eng/Verify-LoadPackage.ps1` checks the actual NuGet archive, creates an isolated `net481` consumer with a fresh package cache and no project reference, verifies the restored archive hash, compiles and executes all eighteen packaged XML examples, and exercises all seven installed assignment APIs for primary/reference/empty cases and both case enumerations plus all four ported models' assignment dispatch. This consumer uses a managed stand-in; it is not a live COM compatibility test.

```powershell
dotnet test tests/StaadPro.Interop.Tests/StaadPro.Interop.Tests.csproj -c Release
dotnet pack src/StaadPro.Interop/StaadPro.Interop.csproj -c Release -p:PackageVersion=1.0.0-preview.20261007.load3 -o artifacts/load-port/package
./eng/Verify-LoadPackage.ps1 -PackagePath artifacts/load-port/package/StaadPro.Interop.1.0.0-preview.20261007.load3.nupkg
```

The existing `OpenStaadWrapperProvider_DefaultGetRunning_ReturnsNullWhenNoStaad` test is explicitly marked `LiveIntegration`: it asserts an environmental precondition, namely that no STAAD session is running. Ordinary offline runs leave it unexecuted. It does not verify the load queries.

## Live validation before release

Use an isolated known model, rather than a user's working model. For each STAAD version advertised as supported, verify primary and reference cases, a node with distinct signed values in all six components, a member with full-span and partial UDLs, projected UDL directions, concentrated forces and moments with eccentricities, full-member linear and triangular definitions with distinct endpoint/middle intensities, partial/full-span trapezoidal loads including projected directions, uniform moments with signed intensities/spans/offsets and projected axes, and entities with no such assignments. Compare every output against the model's definitions under both metric and imperial conventions, with at least one input-unit change. Check concentrated versus distributed moment and force-per-length conversion dimensions and native start/end/middle order. Exercise nonexistent IDs and case activation failures. Record application build, process architecture, units, COM statuses, payload element types, expected values, and observed values. Confirm that assignments are unchanged and the requested case remains active after assignment reads. Also verify both case enumerations, including zero/sparse cases, title/type metadata and overlapping IDs, with unchanged active case before/after enumeration.

The offline suite and package consumer do not certify native COM SAFEARRAY marshaling, live unit conventions, or all STAAD versions. These remain explicit stable-release gates in [NuGet-Readiness.md](NuGet-Readiness.md).

## Sources inspected

- Main project: `src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs` and `Adapters/Models/OSLoadAdapter.cs`, at main repository revision `41dfc15ae34b7d530a1d5a46c8ee8dad063ccd4f`.
- Supporting models: main project's `Entities/Loads/MemberConcentratedMoment.cs`, `MemberLinearVaryingLoad.cs`, `MemberTrapezoidalLoad.cs`, and `MemberUniformMoment.cs` at the same revision.
- Installed Bentley STAAD.Pro 2025 native programmer reference: `OSAPP_Help/group___property_load_nodal.html`, `group___property_load_mem.html`, `group___property_load_case.html`, and `group___property_load_def_ref_load.html`. These are under the installed STAAD.Pro 2025 directory; consult your own installation for the vendor documentation.
- Installed Bentley `openstaadpy-25.0.1.1` source was inspected as an additional reference. It conflicts with the native documentation in some places (notably UDL Boolean status handling). This port follows the native status contract and the main C# method signatures; live verification is still required.

- Installed Bentley technical reference: Help/topics/Commands_TechRef/r-stpst_Member_Load_Specification.html and uniform member load/modeling help describe distributed moments and span/offset conventions.
- Bentley [Unit System of OpenSTAAD](https://bentleysystems.service-now.com/community?id=kb_article&sysparm_article=KB0111068) describes base-system read-back units. Confirm actual read-back units rather than assuming input units.
