# Full job metadata, analysis requests and CONNECTED projects

This increment implements five common native Root functions and one project-assignment overload. Signatures are on IOSRoot, bodies remain in the single non-partial OSRootAdapter, and native calls are isolated in internal common/analysis services. These source additions are unreleased.

| Managed API | Contract |
| --- | --- |
| `void SetFullJobInfo(...)` | Validate thirteen non-null strings and submit them once in documented native order. Dates remain literal text. |
| `void Analyze()` | Invoke the native UI-equivalent analysis entry point once, without choosing AnalyzeEx defaults. |
| `void AnalyzeModel(AnalysisEngine engine = AnalysisEngine.StaadPro)` | Validate the typed selector and send native engine code -1 explicitly. |
| `ConnectedProjectResult GetConnectedProjectInfo()` | Return native success and qualified immutable project data, with reference outputs hidden internally. |
| `bool SetConnectedProjectInfo(string projectId, string projectName)` | Validate nonblank identity/name, then return the native assignment flag. |
| `bool SetConnectedProjectInfo(ConnectedProjectInfo projectInfo)` | Reuse the project data class returned by the getter; validate and delegate without another query. |

## Full job information

The parameter order is:

1. `jobName`
2. `jobClient`
3. `engineerName`
4. `engineerDate`
5. `jobNumber`
6. `revision`
7. `part`
8. `reference`
9. `checkerName`
10. `checkerDate`
11. `approverName`
12. `approverDate`
13. `comments`

Every field is validated before native access. Null is rejected with the corresponding parameter name. Explicit empty strings, whitespace, Unicode and line breaks are sent unchanged; the server determines their acceptance, text limits and clearing behavior.

The three date parameters are native strings rather than DATE values. No date parsing, culture-dependent formatting or guessed format is added. Supply the date text expected by the actual STAAD installation. The existing FullJobInfo class stores DateTime values and its factory uses a different parameter order, so it is not automatically serialized by this setter. A DateTime-based overload remains deferred until native format/empty-date behavior is established; no parallel full-job model was introduced.

```csharp
// This requests a model change. Replace date text with values accepted by the target installation.
wrapper.Root.SetFullJobInfo(
    jobName: "Frame study", jobClient: "Client", engineerName: "Engineer", engineerDate: "",
    jobNumber: "J-1", revision: "A", part: "Frame", reference: "Drawing reference",
    checkerName: "Checker", checkerDate: "", approverName: "Approver", approverDate: "",
    comments: "Model notes");
```

Native void return means dispatch completed. It does not verify storage, date acceptance or save. There is no automatic save, read-back, analysis, refresh or rollback. The existing full-job getter still parses returned date strings using its documented culture policy and can reject text that the native server returns.

## Analysis entry points

```csharp
using StaadPro.Interop.Enums.Root;

// Request only the entry point needed by the caller's workflow:
wrapper.Root.AnalyzeModel(AnalysisEngine.StaadPro);

// Observe explicitly; this is separate from requesting analysis.
var state = wrapper.Root.GetAnalysisStatus();
```

Analyze and AnalyzeModel are distinct native operations, with no substitution through AnalyzeEx. AnalyzeModel defaults to the single documented engine selector (-1), which is still passed explicitly to COM. Other enum values are rejected before native access. The existing AnalyzeEx controls remain available for workflows that need their native silent/hidden/wait options.

Both new entry points return void. Normal return does not mean analysis succeeded, completed or produced current results. The help's C# AnalyzeModel example assigns a return value, but its declaration and the inspected 2025/2026 managed interop signature are void. The managed API follows that verified signature and does not fabricate a status flag.

The library adds no save, polling, timeout, silent-mode change, background dispatch or retry. Native calls may block, display dialogs or fail after an analysis has started. Use the session's COM thread and coordinate analysis and edits. IsAnalyzing/GetAnalysisStatus remain separate observations and do not prove result freshness.

## CONNECTED result and data classes

| Type/property | Meaning |
| --- | --- |
| `ConnectedProjectResult.IsSuccessful` | Verified native Boolean/integral 0/1 query flag. |
| `ConnectedProjectResult.Project` | Non-null ConnectedProjectInfo only after native success and validated written string outputs; null on native false. |
| `ConnectedProjectInfo.ProjectId` | Immutable exact identifier text, without GUID conversion or trimming. |
| `ConnectedProjectInfo.Name` | Immutable exact project name, including an explicitly returned empty string or whitespace. |

The getter initializes two distinct string VARIANT buffers with per-call sentinels, invokes GetCONNECTEDProjectInfo once, validates the native flag and then qualifies the outputs. Successful responses require both written strings. Native false returns no project even if partial or malformed output buffers were written. Malformed flags, unwritten successful outputs and COM/binder failures remain exceptions; there is no invented failure reason or automatic diagnostic read.

```csharp
var result = wrapper.Root.GetConnectedProjectInfo();
if (result.IsSuccessful)
{
    Console.WriteLine(result.Project.ProjectId);
    Console.WriteLine(result.Project.Name);
}
```

The data class permits explicit empty text so a successful native snapshot can be preserved without guessing association semantics. Its public constructor rejects null. Both classes are sealed with read-only properties; earlier snapshots are unaffected by later native changes.

Assignment requires non-null, non-whitespace ID and name. Surrounding whitespace in otherwise nonblank text is preserved, and GUID syntax is not imposed. Blank values are rejected because native unassignment/clearing behavior is not established. This is a managed input policy, not a claim about all possible native inputs.

```csharp
// With projectInfo selected by the caller, or obtained from a qualified query:
bool assigned = wrapper.Root.SetConnectedProjectInfo(projectInfo);
```

The setter returns the documented native flag. A true response does not verify cloud synchronization, credentials, membership, project creation or persistence. A false response is an expected native result, distinct from exceptions. The library adds no login, network retry, save or read-back. Native operations may still have account/network/licensing requirements and can display UI or block. Failures or malformed responses can follow native effects; no rollback is attempted.

## Reuse, evidence and validation

The read-only main interop project has no implementations of these five operations and no equivalent CONNECTED data/result classes. Existing FullJobInfo remains unchanged; its DateTime representation is not silently repurposed as native date text. Shared NativeValues flag/text validation is reused, and the existing model-edit service is extended. New service implementations remain internal.

The reviewed [2024](../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___root.html), [2025](../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___root.html) and [2026](../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html) help describe unchanged contracts for these operations. Links target the installed help in this workspace. Read-only reflection over the 2025/2026 Bentley interop assemblies confirms:

```text
Void SetFullJobInfo(Object, Object, Object, Object, Object, Object, Object,
                    Object, Object, Object, Object, Object, Object)
Void Analyze()
Void AnalyzeModel(Object)
Object GetCONNECTEDProjectInfo(Object ByRef, Object ByRef)
Object SetCONNECTEDProjectInfo(Object, Object)
```

No Bentley binary reference or live COM activation was added. The Release suite passes **1,547 offline tests**, including 76 additional cases for this increment. Tests cover all thirteen input positions, null validation, literal dates across cultures, engine/default selection, no substituted analysis calls, native flag shapes, successful/failed/partial project outputs, immutable data, assignment validation, exception identity, no retry/rollback, session isolation and matching XML documentation.

The Release solution build passes with zero warnings or errors. All 53 common Root interface XML examples compile with warnings treated as errors. The consumer harness also compiles and runs all 217 selected geometry/load/Root XML examples against the source assembly and controlled stand-ins; this is not verification of a rebuilt NuGet archive. Its fake Root supports the new project query, common reads and analysis observation, and status examples receive a named model fixture after earlier close examples. Both 39-row coverage matrices agree on implementation status, and all checked local documentation links resolve.

No live model was analyzed or edited, and no CONNECTED association or account was changed. Actual runtime behavior, native date formats, prompts, credential/offline cases and project persistence still need controlled validation. No NuGet archive was rebuilt for this increment. Coverage is **39 implemented native Root functions and none pending**, exposed through **53 common methods plus three separately acquired operations** including conveniences and overloads.
