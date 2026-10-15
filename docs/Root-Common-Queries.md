# Root model and error queries

The current source implements three common Root queries: `GetStaadFileFolder()`, `IsPhysicalModel()` and `GetErrorMessage()`. These additions are unreleased. Signatures belong to `IOSRoot`; implementations remain in the single, non-partial `OSRootAdapter` and delegate native work to an internal common query service.

## API contracts

| Method | Return and validation | Native behavior |
| --- | --- | --- |
| `string GetStaadFileFolder()` | Exact written directory text. Rejects unwritten, null and non-string outputs. Preserves an explicitly written empty string. | Calls native `GetSTAADFileFolder(ref VARIANT)` once. Does not derive the directory from a filename or normalize/check/create a path. |
| `bool IsPhysicalModel()` | Native Boolean or integral 0/1 only. Rejects null, other integers, strings, floating-point and decimal values. | Calls the native predicate once. False does not establish that an analytical model is loaded and ready. |
| `string GetErrorMessage()` | Exact native message, including Unicode, whitespace, line breaks and empty text. Rejects null/non-string values. | Calls the native message getter once, only when requested. Reading may affect native error state; the help does not specify consumption or clearing behavior. |

These APIs return a single datum and have no separate native success flag to combine with output data. A native folder query returns void, so the managed string does not imply an invented success code. No public reference parameters, mutable output buffers, tuples or additional result classes are necessary.

## Example

With a connected wrapper on the session's COM thread:

```csharp
var root = wrapper.Root;
string folder = root.GetStaadFileFolder();
bool physicalModel = root.IsPhysicalModel();
Console.WriteLine(folder);
Console.WriteLine(physicalModel);

// Explicitly request the server's error text when appropriate for the caller's workflow.
string nativeMessage = root.GetErrorMessage();
Console.WriteLine(nativeMessage);
```

Do not interpret an empty folder as proof that a model is blank, an empty message as proof that all prior operations succeeded, or a false physical-model predicate as a readiness guarantee. Native behavior in blank/unsupported sessions remains subject to native validation.

## Shared implementation and reuse

- `Services/Root/Common/RootCommonQueryService` owns the three same-root native calls. It performs no global instance discovery or wrapper construction.
- `Services/Interop/NativeValues` contains shared strict Int32, Boolean-flag and string validation. The former wrapper integer parser and duplicated analysis status conversion now use this single implementation. Existing `AnalyzeEx` status values and wrapper version/unit validation retain their behavior.
- The main read-only interop project contains no equivalent implementation of these three queries. Its permissive integer conversion inside OpenStaadWrapper is not suitable for native status validation because it permits unrelated numeric conversions. Its existing wrapper-derived unit parsing remains reused separately.
- No enum consolidation or unrelated model-service/domain port is included in this increment.

All calls remain live observations on the supplied root. They do not refresh cached wrapper properties, open/save/close a model, start analysis, write files or retry failed native dispatch. Use the correct COM thread and coordinate shared access; native calls can block. Error text can be replaced by other operations, and repeated reads need not return the same message. Original COM and binder exceptions propagate.

## Evidence and validation

The installed Root references for [2024](../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___root.html), [2025](../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___root.html) and [2026](../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html) describe unchanged contracts for these calls. Read-only reflection over the installed 2026 Bentley interop assembly confirms:

```text
Void GetSTAADFileFolder(System.Object ByRef)
System.Object IsPhysicalModel()
System.Object GetErrorMessage()
```

No Bentley binary reference was added. No live STAAD session or model was changed. Managed policy accepts an explicitly written empty folder/message; it does not claim those values have been observed in every native blank-model state. Error-state consumption and actual 2024–2026 server behavior still require controlled native validation.

The Release suite passes **1,547 offline tests**, including 43 added cases for this increment. New cases cover exact text, empty/Unicode/UNC paths, initialized string reference buffers, unwritten/malformed output, strict flags, wrapper-owned and standalone access, live reads, instance isolation, exception identity, no extra diagnostic calls and matching compiled XML. Existing wrapper/analysis tests cover the shared integer-conversion refactor. The Release build passes with zero warnings or errors.

Subsequent increments bring coverage to **39 implemented native Root mappings** and **no pending vendor functions**, with no internal-only coverage items remaining. IOSRoot exposes 53 common methods plus three separately acquired operation methods; see the [completion guide](Root-Completion-and-Capabilities.md). No NuGet package was rebuilt or published for this increment.
