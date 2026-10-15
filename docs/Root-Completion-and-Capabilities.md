# Completed Root APIs and version-aware capabilities

The Root implementation now covers **all 39 functions in the reviewed Bentley reference** and **all 23 methods in the private source Root interface**. It exposes 53 common IOSRoot methods plus three optional operation methods on separately acquired interfaces. The three capability-acquisition methods and the Capabilities property are infrastructure, not additional native Root functions.

Implementation coverage means the managed mapping exists. It does not certify every STAAD version/build, native flag, file effect or analysis result. These source additions are unreleased; the existing NuGet archive has not been rebuilt.

## Final public surface

| API | Location and return |
| --- | --- |
| `RemoveNamedView(string viewName)` | IOSRoot; void native removal request. |
| `ModifyNamedView(string viewName, IEnumerable<int> entityIds, NamedViewEntityType entityType, int modificationFlag)` | IOSRoot; void native four-input request. |
| `GetAdinaExport()` | IOSRoot.Capabilities; RootCapabilityResult&lt;IOSRootAdinaExport&gt;. |
| `GetAnalysisErrorMessages()` | IOSRoot.Capabilities; independently acquired error-message interface. |
| `GetAnalysisWarningMessages()` | IOSRoot.Capabilities; independently acquired warning-message interface. |
| `ExportModelToAdina(AdinaAxisSystem axisSystem, AdinaLoadCaseHandling loadCaseHandling)` | IOSRootAdinaExport; AdinaExportResult. |
| `GetAnalysisErrorMessages(string modelFilePath)` | IOSRootAnalysisErrorMessages; AnalysisMessagesResult. |
| `GetAnalysisWarningMessages(string modelFilePath)` | IOSRootAnalysisWarningMessages; AnalysisMessagesResult. |
| `RetrieveMemberForces(IEnumerable<int> beamIds, IEnumerable<int> loadCaseIds)` | IOSRoot; MemberForcesResult, completing the additional private-source helper. |

Public contracts live in Adapters/Interfaces, including Capabilities. OSRootAdapter remains a single non-partial class and does not implement the optional operation interfaces. Their concrete adapters and constructors are internal under Adapters/Models/Capabilities. Results/options live in Models/Root and Enums/Root; the source-compatible MemberForces and MemberEnd retain their Entities/Enums homes. Native services and selection remain internal.

## Acquiring optional operations

```csharp
using StaadPro.Interop.Enums.Root;

var grant = wrapper.Root.Capabilities.GetAdinaExport();
if (!grant.IsAvailable)
{
    Console.WriteLine(grant.Availability + ": " + grant.Reason);
}
else
{
    // Native export writes files. Use only a controlled model and destination.
    var result = grant.Capability.ExportModelToAdina(
        AdinaAxisSystem.Preserve, AdinaLoadCaseHandling.MultipleLoadCases);
    Console.WriteLine(result.NativeStatus);
    Console.WriteLine(result.HasWarnings);
}
```

A runtime version cannot remove C# interface declarations from assembly metadata. The implemented boundary therefore returns an operation interface only after acquisition checks; an unsupported common adapter cannot be cast to an optional interface. Deliberate raw ComObject access remains available.

Capabilities property access captures the existing root/accessor without native inspection. Each acquisition reads the owning wrapper's native StaadVersion snapshot, or the same supplied raw root for standalone adapters. It never uses host ApplicationVersion, a registry entry, installed-folder names, a PID or a global active object.

### Conservative candidate profiles

| Observed native version | Export | Error messages | Warning messages |
| --- | --- | --- | --- |
| 25.0.1.424 | Candidate | Disabled by version policy | Disabled by version policy |
| 26.0.0.340 | Candidate | Candidate | Candidate |
| Other servicing builds in the candidate year | ContractUnverified | ContractUnverified when the year permits the feature | Same |
| Missing/incomplete native version | UnknownVersion | UnknownVersion | UnknownVersion |
| Older/future families outside each feature policy | DisabledByVersionPolicy | DisabledByVersionPolicy | DisabledByVersionPolicy |

These exact profiles reflect the installed help/managed interop declarations inspected during development. They are **not native runtime certification**. A candidate is granted only when its member metadata also matches; a year alone never grants a feature. Future build coverage requires an explicit evidence-backed profile update.

Acquisition uses non-mutating name resolution and type information on the actual COM object. Name resolution alone is insufficient: the inspector checks function kind, arity, return type and parameter/reference shapes, including normalized HRESULT/retval declarations. Message interfaces also require a compatible GetAnalysisStatus dependency. Missing names return NativeMemberAbsent; unavailable/incompatible metadata returns ContractUnverified. Unicode BSTR/LPWSTR string declarations and equivalent by-reference VARIANT shapes are recognized. Unresolved aliases and ambiguous declarations fail closed.

The implementation follows Microsoft's [GetIDsOfNames](https://learn.microsoft.com/en-us/windows/win32/api/oaidl/nf-oaidl-idispatch-getidsofnames), [GetTypeInfo](https://learn.microsoft.com/en-us/windows/win32/api/oaidl/nf-oaidl-idispatch-gettypeinfo) and [FUNCDESC](https://learn.microsoft.com/en-us/windows/win32/api/oaidl/ns-oaidl-funcdesc) contracts. It releases only inspection-owned descriptors/type-info references and never final-releases the caller's root. Publicly visible managed automation shims are inspected through exact public instance signatures without invoking their methods.

Busy, disconnected, wrong-thread, locale and other native failures remain exceptions; they are not cached as absence. Each acquisition is independent, with no global grant cache. The granted adapter stays bound to the original root and checks its native-version snapshot before work. A changed snapshot rejects the call and requires reacquisition. A disposed owning wrapper invalidates captured gateways/grants. Refresh with unchanged root/version does not turn a grant into a different process or a model lease. No thread migration or automatic retry is added.

RootCapabilityResult provides Availability, IsAvailable, Capability, Reason and a defensive NativeVersion copy. A Capability is non-null exactly for Available. A grant proves the configured metadata checks, not model readiness, licensing, output freshness or operation success.

## Analysis messages

```csharp
var grant = wrapper.Root.Capabilities.GetAnalysisErrorMessages();
if (grant.IsAvailable)
{
    var result = grant.Capability.GetAnalysisErrorMessages(@"C:\Models\Frame.std");
    Console.WriteLine(result.Outcome);
    Console.WriteLine(result.NativeStatus); // null if retrieval was never invoked
    foreach (string text in result.Messages) Console.WriteLine(text);
}
```

The warning interface is acquired and inspected separately. It uses the ANL file; the error interface uses the analysis log. Neither parses files itself or starts analysis.

The managed implementation validates/normalizes the model path before native access, reads GetAnalysisStatus for the selected count and allocates a BSTR string array internally. No public ref/out parameters or caller-owned buffers are exposed. An unavailable count is not silently treated as zero. Allocation is limited to **100,000 messages** per attempt. A count of zero still makes the actual native retrieval call, preserving its real successful-absence response.

| Native retrieval code | Interpretation |
| --- | --- |
| 1 | Retrieved successfully |
| 0 | Successful absence |
| -1 / -2 | General error / invalid model path |
| -107 / -119 | Not an array / wrong array type |
| -108 | Insufficient capacity |
| -1001 | Required log/ANL source missing |
| -10004 | Analysis running |
| Any other Int32 | Preserved unknown native failure |

Only an explicit -108 permits **one** fresh count and one additional retrieval. Exceptions, busy responses and other failures are not retried. An unsuccessful retry preflight preserves the earlier -108 code and reports its managed count/limit outcome. There is no unbounded growth loop.

AnalysisMessagesResult distinguishes Outcome from NativeStatus. Outcome may be Retrieved, NoMessages, NativeFailure, AnalysisCountUnavailable or MessageLimitExceeded. NativeStatus is null only when retrieval was not invoked. Status is the recognized nullable native enum. AnalysisState preserves the last count-query result/path, RetrievalAttempts is zero through two, and IsSuccessful means retrieval success rather than successful analysis.

Native success requires a one-dimensional BSTR array with all returned entries written as strings and at most 100,000 elements. Replacement arrays and nonzero lower bounds are accepted. Native false/error or successful absence exposes no partial buffer content. Malformed successful shapes, null entries or unwritten sentinels remain exceptions. Messages are copied into a read-only collection; ordering, empty strings, whitespace, Unicode and line breaks are retained.

Counts and retrieval are sequential observations, not a transaction. The count/source may change between calls; inconsistent successful outputs fail rather than being presented as qualified data. The allocation bound governs managed preallocation and accepted output size; it cannot prevent a native server/marshaler from allocating an oversized response before validation.

## ADINA export

AdinaAxisSystem defines Preserve=0, YUp=1 and ZUp=2. AdinaLoadCaseHandling defines MultipleLoadCases=0 and TimeHistory=1. Invalid enum casts are rejected before native metadata or export access.

The operation makes one ExportModelToADINA call using the two Int32 codes. AdinaExportResult retains NativeStatus, a nullable recognized Status, requested options, IsSuccessful and HasWarnings. Zero is success; -14501 is success with warnings; -14502 is failure; -101 means no open model. Unknown codes remain failures without losing the raw value.

Native export writes an ADINA input file and a companion .ADINA.log. The signature returns no path and provides no destination/overwrite option. The library does not infer an output path, save or analyse the model, promise atomic no-overwrite, retry the export or roll back files. Use a disposable controlled model/destination and inspect files/logs separately. Runtime destination, overwrite, prompt and licensing behavior still require validation.

## Remaining named-view operations

Inspected 2025/2026 declarations are:

```text
Void RemoveNamedView(Object name, Object nError)
Void ModifyNamedView(Object name, Object entityArray, Object qualifier, Object modificationFlag)
```

These conflict with the help's reference error outputs and, for modification, separate count argument. The implemented signatures follow the inspected declarations and do not retry alternative mutations.

RemoveNamedView validates the literal name and supplies Int32 zero to initialize the required by-value nError slot, consistent with the help example's initially zero numeric variable. Zero is not interpreted as success; no output/error result is observable through this declaration. The managed return is void.

ModifyNamedView snapshots its identifier sequence once into a private zero-based Int32 array, validates positive IDs and a documented entity qualifier, and makes one four-input native request. NamedViewEntityType defines Node=0, Beam=1, Plate=2, Solid=3 and Surface=4. Order and duplicates are preserved. Empty arrays are forwarded as explicit native requests, not converted into guessed no-ops. A caller count cannot disagree with the array length because no public count is accepted.

The required modificationFlag remains Int32 because authoritative meanings/defaults are unavailable. Every Int32 is forwarded unchanged; callers must establish the mode and empty-array behavior for the actual build. Void return proves dispatch completion only. Native existence, membership, persistence, flags, 2024 signatures and marshaling still require controlled validation. No geometry deletion, selection edit, SaveModel, rollback or mutation retry is added.

## Completed private-source helper

```csharp
var batch = wrapper.Root.RetrieveMemberForces(new[] { 1, 2 }, new[] { 3 });
if (!batch.IsSuccessful) Console.WriteLine("Inspect availability and per-end outcomes.");
foreach (var query in batch.Queries)
{
    if (query.IsSuccessful) Console.WriteLine(query.Forces.ToString());
}
```

RetrieveMemberForces completes the additional private Root interface method, which is not one of Bentley's 39 Root functions. Its public return is refined from the source HashSet to MemberForcesResult under the approved managed API rules. No public Output methods are added.

Both input sequences are captured once, validated as positive IDs and deduplicated in first-seen order. At most 100,000 end queries may be requested. An empty Cartesian request performs no native access, succeeds as empty work and reports null AreResultsAvailable. Otherwise, the bound raw Output availability is queried once. Native false returns an unsuccessful empty result without fetching load titles or forces.

For available results, each distinct case title is read once through the same root's raw Load object. Each member is queried at start/end using local coordinate flag zero and an internal six-Double buffer. Native true requires six written finite values; native false retains the failed beam/case/end identity and null data, while other requested queries continue. Unknown/malformed flags or successful buffers throw without returning a partial batch.

MemberForces and MemberEnd were ported from the main source and reuse the existing Forces model. Records retain native local result units without conversion, coordinate reconstruction or offset transfer. Their managed properties remain editable. The read-only collections preserve distinct physical identities even when their six component values match; inherited component-based Forces equality must not collapse them through a HashSet. Queries carries all per-end outcomes, Forces contains only qualified records, and IsSuccessful requires availability plus success of all requested end queries.

No case activation, analysis, model save, GUI warning, task dispatch or provider acquisition is introduced. Native unit conventions, actual results availability and force accuracy remain subject to runtime validation.

## Verification and release boundary

The Release suite currently passes **1,547 tests**. The main source remains strictly read-only. All new public operations, results, options and statuses have XML documentation; interfaces and concrete common adapters have matching contracts. Metadata inspection is tested with allocated ITypeInfo descriptor stand-ins and a harmless empty Windows automation object; no STAAD mutation was used for validation.

The Release build passes with zero warnings/errors. All 60 public Root interface/gateway XML examples compile with warnings treated as errors. The 217-example source consumer harness also compiles/runs; this is source validation, not a rebuilt NuGet archive check. Both 39-row coverage matrices, the 53-row common API table and built private-source name parity (23 of 23) are verified.

Complete native API coverage does not remove release gates. Actual 2024/2025/2026 runtime behavior, unverified servicing builds, named-view flags, result units/accuracy, native array marshaling, export file effects, model creation races and shutdown prompts still need controlled STAAD validation. The existing package/archive is unchanged; a new NuGet release needs fresh package metadata, release notes and isolated archive verification.
