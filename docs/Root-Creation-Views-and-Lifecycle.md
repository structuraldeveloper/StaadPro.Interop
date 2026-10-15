# Model creation, named views, process-handle data and shutdown

This increment adds five native Root mappings to IOSRoot and its single non-partial OSRootAdapter. Internal common services isolate validation and dispatch on the captured root and caller's COM thread. These source additions are unreleased.

| Managed API | Contract |
| --- | --- |
| `void NewSTAADFile(string filePath, LengthInputUnit lengthUnit, ForceInputUnit forceUnit)` | Native creation request after destination, unit and session prechecks. |
| `void CreateNamedView(string viewName, int creationFlag)` | Native two-input request with a required, caller-established flag. |
| `void SaveNamedView(string viewName)` | Native one-input request to save the current view. |
| `NativeProcessHandleValue GetProcessHandle()` | Immutable opaque numeric data with no handle ownership. |
| `void Quit()` | Explicit shutdown request against the bound application. |

## Native signature evidence

The installed [2024](../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___root.html), [2025](../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___root.html) and [2026](../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html) help list these operations. Read-only reflection in separate processes over the installed 2025 and 2026 Bentley managed interop assemblies confirms:

```text
Object GetProcessHandle()
Void NewSTAADFile(Object, Object, Object)
Void CreateNamedView(Object, Object)
Void SaveNamedView(Object)
Void Quit()
```

The help's extra reference-error argument for CreateNamedView and SaveNamedView conflicts with both inspected assemblies. The implementation follows their two-input/one-input void contracts. No error buffer, result status or success Boolean is fabricated. An alternate signature is never retried after an uncertain mutation.

This verifies installed managed interface declarations, not live IDispatch behavior on every build. No 2024 managed interop assembly was available at the expected installation path. Shared implementation remains a compatibility candidate; native behavior still needs validation.

Inspection also identified discrepancies in the remaining named-view functions: RemoveNamedView has two Object inputs without ByRef, and ModifyNamedView has four inputs (name, array, qualifier, modification flag), without the help's entity-count/error arguments. The final implementations now follow those inspected declarations; the [completion guide](Root-Completion-and-Capabilities.md#remaining-named-view-operations) explains the managed policies.

## New model creation

```csharp
// Use an intended unnamed session and an exclusively controlled destination.
// The directory must exist and the target must not exist.
wrapper.Root.NewSTAADFile(
    filePath: @"C:\Models\NewFrame.std",
    lengthUnit: LengthInputUnit.Meter,
    forceUnit: ForceInputUnit.KiloNewton);
```

The implementation:

1. Rejects null/blank paths and normalizes to an absolute Windows path.
2. Validates the complete unit pair before filesystem/native access. Length codes 0-7 include feet alias 1; force must be a defined enum value 0-7. Existing combined SetInputUnitsEx validation remains unchanged.
3. Requires a .std filename with valid characters and rejects Windows device names, including COM/LPT digit and superscript-digit aliases.
4. Inspects the existing parent directory and rejects an existing target file or directory. Filesystem errors propagate; inaccessible targets are not treated as absent through File.Exists.
5. Reads GetSTAADFile once and rejects nonblank filename text. Empty or whitespace filename text qualifies under the managed policy.
6. Rechecks target absence after the native filename read.
7. Calls NewSTAADFile once with the normalized path and Int32 length/force codes.

A blank filename does not prove that no unsaved data exists or that initialization is complete. The caller must control the selected session. The library does not close/save another model or launch a process.

### Concurrency and completion boundary

The prechecks are not atomic with native creation. Another actor can create the target or switch the session after validation. Native overwrite/loaded-model behavior is unspecified, so this API cannot guarantee atomic no-overwrite or preservation against concurrent actors. Use an exclusively controlled session and destination; workflows requiring an atomic guarantee must not assume that prechecks provide it.

The library creates no placeholder file, directory or managed STD contents. Normal void return establishes dispatch completion, not persistence, active-model identity, unit acceptance or readiness. Observe those explicitly when needed.

There is no automatic save, polling, read-back, wrapper refresh, retry, cleanup or rollback. A native failure can follow partial effects; a file created by the native call is not automatically deleted. Cached wrapper metadata remains unchanged until explicit refresh.

## Named-view requests

```csharp
// suppliedFlag must be established for the actual connected build.
wrapper.Root.CreateNamedView(viewName: "Frame view", creationFlag: suppliedFlag);
wrapper.Root.SaveNamedView(viewName: "Frame view");
```

Both operations reject null/blank names before dispatch and preserve other text exactly, including Unicode and surrounding whitespace. CreateNamedView forwards a required Int32 flag. The reviewed help does not define allowed values or meanings, so no enum, default or supported range is invented. Every Int32 is passed unchanged; the caller must establish a valid flag.

SaveNamedView uses its own single-input entry point. It is not substituted with creation or SaveModel. Normal return does not certify success or persistence. Duplicate names, overwrite behavior, view contents, no-model cases and prompts require native validation. Neither operation adds selection edits, model save, read-back, refresh, retry or rollback. Native calls can affect view/model state, block or fail after effects.

## Opaque process-handle data

```csharp
var value = wrapper.Root.GetProcessHandle();
Console.WriteLine(value.RawValue); // diagnostic data only
```

NativeProcessHandleValue is a sealed immutable class in StaadPro.Interop.Models.Root. RawValue is UInt64. Integral CLR values and IntPtr/UIntPtr representations are accepted. Unsigned values are preserved; signed negatives are sign-extended to a 64-bit unsigned bit pattern. Zero/all-one values are retained without assigning validity or pseudo-handle meanings. Null, Boolean, enum, text and fractional responses are rejected.

This representation establishes no server pointer width, handle ownership, lifetime, access rights or caller-process usability. It exposes no SafeHandle, IDisposable, conversion operator or native pointer property. Do not pass RawValue to Windows APIs or close it. No process is opened, duplicated, enumerated or terminated by the getter. Use GetProcessId when a process identifier is required; a number is not proof of connection liveness.

This adopts the coverage plan's explicitly opaque fallback. The provisional IntPtr return was replaced to avoid presenting an unverified caller-process pointer or truncating a value in a narrower client.

## Explicit shutdown

```csharp
// First handle unsaved work and coordinate all clients of this application.
wrapper.Root.Quit();
```

Quit targets the captured application once and can affect unsaved work and other clients sharing it. There is no pre-save, silent-mode change, model close, process kill, provider lookup or automatic wrapper disposal. OpenStaadWrapper.Dispose does not invoke Quit.

Prompts or running analysis may delay/prevent exit. Normal void return does not prove termination; exceptions can follow effects. Retained native interfaces may become unusable and are not silently retargeted. Test native shutdown last in a dedicated disposable process.

## Validation and remaining work

The main source remains strictly read-only. Its NewSTAADFile usage is through a model service; matching managed Root named-view APIs, an opaque-handle model and shared validators were not found. Existing enums, strict integer conversion and filename reading are reused. Only needed internal services and a data class were added.

The Release offline suite passes **1,547 tests**, including 109 additional cases. Tests cover all unit codes, validation before dispatch, path normalization and Unicode, reserved devices, existing/read-only targets, missing parents, named/blank/malformed sessions, competing target creation during identity lookup, partial native effects, exact view signatures/flags, opaque numeric types, immutable snapshots, same-instance calls, no implicit Quit on disposal, original exceptions, no retries and matching XML.

The Release solution build passes with zero warnings or errors. All 53 common Root interface XML examples compile with warnings treated as errors. The 217-example consumer harness compiles and runs against source stand-ins; this does not verify a rebuilt NuGet archive. Both 39-row coverage matrices and the 53-row common managed API table agree, and all checked local document links resolve.

No live model was created, edited or shut down, and no NuGet archive was rebuilt. Native flag meanings, 2024 signatures, overwrite/race behavior, handle context and view persistence still need controlled validation. Coverage is **39 of 39 native Root functions implemented, none pending**, exposed through **53 common managed methods plus three optional operations**. Those five mappings and the optional capability gateway are now implemented; see the [completion guide](Root-Completion-and-Capabilities.md) for native/private coverage and release gates.
