# Root model setup and process identity

This source increment implements five native Root functions selected for general model setup and instance identification. It also adds one overload that reuses the existing ShortJobInfo model. These additions are unreleased.

| Managed API | Purpose |
| --- | --- |
| `uint GetProcessId()` | Read the PID reported by the acquired root, without process enumeration or another session lookup. |
| `void SetInputUnitForLength(LengthInputUnit lengthUnit)` | Request one input length-unit change without issuing a force-unit request. |
| `void SetInputUnitForForce(ForceInputUnit forceUnit)` | Request one input force-unit change without issuing a length-unit request. |
| `void SetShortJobInfo(string jobName, string jobClient, string engineerName)` | Submit the job name, client and originating engineer in native order. |
| `void SetShortJobInfo(ShortJobInfo jobInfo)` | Snapshot and validate the existing model's three short-job properties, then delegate. |
| `void UpdateStructure()` | Explicitly invoke the native structure-update operation once. |

Signatures belong to IOSRoot; implementations remain in the single, non-partial OSRootAdapter. Internal services stay under Services/Root/Common. No new public service, tuple, reference parameter or parallel job-information model is introduced.

## Instance identity

GetProcessId accepts integral native representations within **1–UInt32.MaxValue**. It rejects zero, negatives, overflow, null/DBNull, strings, Booleans, floating-point/decimal values and pointers. A PID is an identifier rather than an owning process handle. The method does not construct a Process, query a window, check liveness, launch STAAD or select a process by name. PIDs can become stale or be recycled.

The shared Services/Interop/NativeValues reader is also used by existing window-control validation. Strict signed Int32 status conversion retains its range and type restrictions; pointer-width window conversion remains distinct from positive PID validation. Existing window ownership checks still run before posting a window-state request.

## Independent units and cached metadata

Both unit setters validate their complete argument before one native call, sending an Int32 code by value through the VARIANT input. Neither setter performs native unit read-back or routes through SetInputUnitsEx.

| Length code | Meaning | Force code | Meaning |
| ---: | --- | ---: | --- |
| 0 | Inch | 0 | KiloPound |
| 1 | Feet alias, accepted as `(LengthInputUnit)1` | 1 | Pound |
| 2 | Feet | 2 | KiloGram |
| 3 | CentiMeter | 3 | MetricTon |
| 4 | Meter | 4 | Newton |
| 5 | MilliMeter | 5 | KiloNewton |
| 6 | DeciMeter | 6 | MegaNewton |
| 7 | KiloMeter | 7 | DecaNewton |

The new length setter sends documented alias code 1 unchanged. The existing named enum remains unchanged, and SetInputUnitsEx retains its previous validation, including rejecting unnamed code 1.

Wrapper metadata is cached. Explicitly refresh when a fresh snapshot is needed:

```csharp
using StaadPro.Interop.Enums;

uint processId = wrapper.Root.GetProcessId();
wrapper.Root.SetInputUnitForLength(LengthInputUnit.Meter);
wrapper.Root.SetInputUnitForForce(ForceInputUnit.KiloNewton);
wrapper.RefreshUnits();
```

RefreshUnits makes additional native reads and is a separate caller-controlled operation. If refresh fails or uses a documented compatibility fallback, this does not roll back the preceding native unit requests. The library adds no value rescaling, base-system change, save or structure update to these setters.

## Job metadata and explicit update

All three job strings must be non-null. Empty strings, whitespace, Unicode and line breaks are passed unchanged; native text limits, clearing behavior and storage remain the server's responsibility. The direct overload validates all inputs before dispatch.

The object overload reads Name, Client and Originator once into local string snapshots. It rejects a null object or any null short field before native access. Coordinate concurrent edits when a coherent snapshot is needed; three property reads are not an atomic transaction. A FullJobInfo can be passed through its inherited short fields, but dates and other full-job properties are not submitted.

```csharp
using StaadPro.Interop.Models;

var jobInfo = ShortJobInfo.From("Frame study", "Client", "Engineer");
wrapper.Root.SetShortJobInfo(jobInfo);
var observed = wrapper.Root.GetShortJobInfo(); // explicit read-back

wrapper.Root.UpdateStructure(); // explicit native request when needed by the workflow
```

UpdateStructure has no arguments or native status result. The help says that it updates the current structure but does not define rebuild completion, readiness, persistence or result-validity guarantees. The adapter adds no save, analysis, wait, read-back or wrapper refresh. Calling it is an explicit model operation, not a prerequisite silently inserted into another setter.

## Completion and failure semantics

The four native mutations return void. Returning normally means dispatch completed; it does not certify persistence or invent a success flag. No native output data is passed back through reference arguments, so an additional result class is unnecessary for these contracts.

Argument errors occur before native mutation. COM/binder exceptions propagate without retries, automatic diagnostic reads or rollback. A failed/lost response can follow native effects. Use the acquired session's COM thread and serialize model edits, metadata reads and refreshes. Native calls may block or display UI. These methods do not establish unattended operation or an interruptible timeout.

## Evidence and verification

The five contracts match in the reviewed [2024](../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___root.html), [2025](../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___root.html) and [2026](../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html) Root help. These local links resolve in the reviewed workspace. Read-only inspection of the installed 2025 and 2026 Bentley managed interop assemblies confirms:

```text
System.Object GetProcessId()
Void SetInputUnitForLength(System.Object)
Void SetInputUnitForForce(System.Object)
Void SetShortJobInfo(System.Object, System.Object, System.Object)
Void UpdateStructure()
```

The main source was checked for reusable implementations: these dedicated functions are absent there. Existing ShortJobInfo and unit enums are reused, and existing internal PID validation was shared rather than duplicated. No Bentley binary dependency or change to the read-only main project was introduced.

The Release suite passes **1,547 offline tests**, including 73 additional cases for this increment. Tests cover PID boundaries/types, every unit code, code-1 compatibility policy, native argument shapes/order, validation before dispatch, stale caches and explicit refresh, object snapshots, inherited short fields, no automatic save/analysis, same-instance behavior, exception identity and no retries/rollback. Matching compiled XML is checked on both interface and adapter. The Release solution build passes with zero warnings or errors. All 53 common interface Root XML examples also compile in an isolated net481 project with warnings treated as errors; matching adapter XML is checked by the suite.

Native behavior with actual saved/unsaved models, unit changes, blank sessions and structure updates still requires controlled runtime validation. No live STAAD model was modified and no NuGet archive was rebuilt for this increment. Current coverage is **39 of 39 native Root functions implemented, none pending**. IOSRoot exposes **53 common managed methods plus three optional operations**, including conveniences and overloads; managed convenience methods are counted separately from native coverage.
