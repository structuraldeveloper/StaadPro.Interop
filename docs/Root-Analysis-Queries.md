# Root analysis observation

The Root adapter now exposes the common analysis queries documented for STAAD.Pro 2024, 2025 and 2026. These source additions are unreleased. They observe native state and do not start analysis or alter a model.

| Managed member | Behavior |
| --- | --- |
| `bool IsAnalyzing()` | Reads the bound application's live running flag once. Accepts Boolean values and integral 0/1; rejects other representations. |
| `AnalysisStatusResult GetAnalysisStatus(string modelFilePath)` | Normalizes the requested path and queries that model once through the captured root. The target need not be the open model. |
| `AnalysisStatusResult GetAnalysisStatus()` | Reads the current filename once, then delegates to the explicit-path overload. An unnamed model throws before querying status. |

Signatures are declared in `IOSRoot` and implemented in the single, non-partial `OSRootAdapter`. Native dispatch and output validation share one internal service under `Services/Root/Analysis`. `AnalysisStatusResult` is a sealed class under `Models/Root`; the documented response enum is under `Enums/Root`. No new public parameters use `ref` or `out`, and no tuples or mutable output buffers are returned.

## Example

With an existing connected wrapper:

```csharp
using System;
using StaadPro.Interop.Enums.Root;
using StaadPro.Interop.Models.Root;

var root = wrapper.Root;
Console.WriteLine(root.IsAnalyzing());

AnalysisStatusResult result = root.GetAnalysisStatus(@"C:\Models\Frame.STD");
if (!result.IsSuccessful)
{
    Console.WriteLine($"Status could not be retrieved: {result.NativeStatus}");
}
else if (result.Status == AnalysisStatus.CompletedWithErrors)
{
    Console.WriteLine($"Analysis completed with {result.ErrorCount} errors.");
}
else
{
    Console.WriteLine($"State: {result.Status}; CPU seconds: {result.CpuTimeSeconds}");
}

// Use only when the session has a named model:
AnalysisStatusResult currentModel = root.GetAnalysisStatus();
Console.WriteLine(currentModel.ModelFilePath);
```

`IsSuccessful` describes **status retrieval**, not the engineering outcome. A running, terminated, completed-with-errors or not-performed analysis can all be observed successfully. `IsCompleted` includes completion with errors. Inspect `Status` before using analysis results; neither query proves that stored results match current model edits.

## Status mapping

| Native code | `AnalysisStatus` | `IsSuccessful` | `IsCompleted` |
| ---: | --- | --- | --- |
| -2 | `InvalidModelPath` | false | false |
| -1 | `Terminated` | true | false |
| 0 | `GeneralError` | false | false |
| 1 | `InProgress` | true | false |
| 2 | `CompletedSuccessfully` | true | true |
| 3 | `CompletedWithWarnings` | true | true |
| 4 | `CompletedWithErrors` | true | true |
| 5 | `NotPerformed` | true | false |
| Other signed Int32 | null recognized status; original value in `NativeStatus` | false | false |

This enum is separate from the existing `AnalysisResult` returned by the analysis request API `AnalyzeEx`. That existing API and its behavior remain unchanged.

## Result data and validity

| Property | Meaning |
| --- | --- |
| `ModelFilePath` | Absolute path actually sent to the status query. |
| `NativeStatus` | Exact native signed Int32 response, including unknown codes. |
| `Status` | Nullable recognized status from the table above. |
| `IsSuccessful` | A recognized analysis state was retrieved. |
| `IsCompleted` | Native state is 2, 3 or 4. |
| `WarningCount` | Nullable nonnegative Int32 warning count. |
| `ErrorCount` | Nullable nonnegative Int32 error count. |
| `CpuTimeSeconds` | Nullable finite nonnegative Double CPU time, retaining the native seconds and precision. This is not a managed elapsed-time measurement. |

Each query starts the internal counts at `Int32.MinValue` and the time at `Double.NaN`. This distinguishes unwritten outputs from valid zeros. For completed states 2–4, the managed contract requires all three outputs to be valid; missing, negative or nonfinite values cause `InvalidOperationException` rather than a successful-looking result with fabricated zeros.

For other recognized analysis states (-1, 1, 5), each valid written field is retained independently. Initial count sentinels and NaN time become null. For those states, an unwritten time cannot be distinguished from an explicitly returned NaN; both represent unavailable time. Other negative counts/times and infinite times are rejected. The library does not infer additional consistency rules between the status and counts, or that an in-progress observation is a final total.

For native invalid-path, general-error and unknown status responses, output buffers are not qualified as valid data and all three data properties are null. These responses remain inspectable result objects. Dispatch exceptions and malformed successful outputs remain exceptions. There is no catch-and-retry fallback or implicit error-message query that could obscure the original failure.

These validation rules are managed policy derived from the documented output contract. The help does not specify output initialization for every transient state. Controlled native tests are still needed to confirm observed output behavior across supported builds.

## Path, lifetime and threading behavior

- The explicit-path overload rejects null and whitespace-only paths before COM access and normalizes relative paths with the Windows .NET Framework path API. It performs no file-existence, extension or result-file check. A missing model can be reported through the native invalid-path response.
- The parameterless overload makes one native filename query and one status query. Empty or whitespace-only names produce `InvalidOperationException`; malformed/unwritten filename outputs also throw. A model switch between those calls is possible: the result identifies the captured path, not a guarantee of the still-open model.
- All queries use the captured root on the calling thread. They do not select a global active object, acquire another process, launch STAAD or refresh the wrapper's metadata cache.
- Use the session's appropriate COM thread and coordinate shared access. No background tasks, waits, model saves, silent-mode changes, filesystem writes or analysis requests occur. Native calls can block beyond any caller-side deadline.
- A result is a read-only managed snapshot. Later native calls do not alter an earlier result. Existing wrapper disposal ownership rules still apply; captured native adapters are not automatically revoked.

## Evidence and verification

The installed [2024](../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___root.html#ga731f741a97cb618d7409544055b8a2bb), [2025](../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___root.html#ga731f741a97cb618d7409544055b8a2bb) and [2026](../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga731f741a97cb618d7409544055b8a2bb) Root references describe the same analysis query contract. Read-only reflection over the installed 2025 and 2026 Bentley interop assemblies confirms:

```text
System.Object IsAnalyzing()
Int32 GetAnalysisStatus(System.String, Int32 ByRef, Int32 ByRef, Double ByRef)
```

No Bentley assembly reference or binary was added to this project. Inspection does not establish native runtime behavior or 2024 ABI compatibility. A 2024 interop assembly was not found at the corresponding installation path.

<details>
<summary>Inspected Bentley assembly fingerprints, 15 October 2026</summary>

Both inspected files have assembly identity `StaadPro.Interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b4d201f8319cef86` and are located under each product installation's `STAAD` directory.

| Product | SHA-256 of installed `StaadPro.Interop.dll` |
| --- | --- |
| 2025 | `CC724993AC9119138B7323E8C684997257E2851DE62E1551BA5D6D67A9DADC19` |
| 2026 | `6D153E80ED356A1B167ABB2EF19227FCFF510707A578AD304154652CC6C1F6D4` |

</details>

The Release suite passes **1,266 offline tests**. New tests cover every documented/unknown status, strict native value shapes, reference argument types and order, field availability/validation, immutable snapshots, both overloads, path handling, blank sessions, instance isolation, exception propagation and matching compiled XML documentation. The Release solution build passes with zero warnings or errors.

No live STAAD model was opened, analyzed or changed. Native validation, optional version-specific analysis-message retrieval and the broader capability/version-profile engine remain pending. No NuGet package was rebuilt or published for this increment.
