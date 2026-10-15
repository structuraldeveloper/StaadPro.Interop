# Root adapter implementation and porting progress

For coverage against Bentley's installed OpenSTAAD 2026 reference (39 native Root functions), see the [Root API coverage and implementation plan](reports/OpenSTAAD-Root-API-Coverage-and-Implementation-Plan.md). The counts below describe parity with the private main project, not coverage of Bentley's full Root API.

For the implemented 2024-2026 candidate selection and capability access, see the [multi-version Root architecture plan](reports/OpenSTAAD-Root-Multi-Version-Architecture-and-Plan.md). Metadata/common operations, exact candidate profiles and optional capability infrastructure are implemented. Native runtime compatibility is not inferred from source coverage.

The [complete wrapper foundation](Wrapper-Porting.md) is now ported with raw bindings, cached application metadata, refresh methods and explicitly limited domain placeholders. Five Root metadata methods now reuse its properties; common analysis queries use a separate live native service.

The main project's `IOSRoot` exposes 23 methods. This repository now covers **all 23**, with **no private-source Root method pending**. The existing late-bound root binding is retained: signatures belong to `Adapters/Interfaces/IOSRoot.cs`, implementations belong to the single non-partial `Adapters/Models/OSRootAdapter.cs`, and the connected `OpenStaadWrapper.Root` exposes that adapter.

Reference source: `ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSRoot.cs` and `Adapters/Models/OSRootAdapter.cs`, main-project commit `3f930d742f972c27d961f0e8eff9f1e11c520ff2`. The original 22 signatures are preserved. RetrieveMemberForces retains the source function name but uses descriptive parameters and a named status/data result under the approved managed API rules. The constructor accepts the raw root as an object and `ComObject` remains dynamic so no proprietary Bentley assembly is needed to build/install this package. The main project was not changed.

The first Root increment added five vendor metadata queries beyond the private interface: GetApplicationVersion, GetBaseUnit, GetInputUnitForForce, GetInputUnitForLength and GetMainWindowHandle. IOSRoot now has 53 common methods. Three optional operation interfaces complete all 39 reviewed native Root mappings; capability acquisition methods are counted separately. Wrapper-bound calls reuse snapshots; standalone bindings read the same supplied native root through shared helpers. See the [managed API design rules](Root-Managed-API-Design.md) and [API reference](API-Reference.md#root-adapter). These additions do not change the private-interface parity count below.

## Analysis observation

`IsAnalyzing()`, `GetAnalysisStatus(string modelFilePath)` and the current-model `GetAnalysisStatus()` overload are implemented. The immutable AnalysisStatusResult includes native/typed status, query success, completion state and nullable counts/time. Signatures, status mappings, validation policy, examples and compatibility evidence are covered in the [analysis query guide](Root-Analysis-Queries.md). These two native functions and one convenience overload are additional to the private-source parity count.

## Common model and error queries

GetStaadFileFolder, IsPhysicalModel and GetErrorMessage are now implemented with full XML and shared strict native validation. They perform live same-root reads and preserve explicit native empty strings; no public reference parameters are introduced. See the [common query guide](Root-Common-Queries.md). These vendor additions do not change the private-source parity count below.

## Process identity and model setup

GetProcessId, SetInputUnitForLength, SetInputUnitForForce, SetShortJobInfo and UpdateStructure are implemented. SetShortJobInfo also accepts the existing ShortJobInfo model. Native mutations return void and add no save, retry or cache refresh; PID reads share strict validation with window control. See the [model setup guide](Root-Model-Setup.md). These are five additional native mappings and six managed signatures, distinct from private-source parity.

## Full job metadata, analysis requests and CONNECTED projects

SetFullJobInfo, Analyze, AnalyzeModel, GetConnectedProjectInfo and SetConnectedProjectInfo are implemented. The project setter also accepts ConnectedProjectInfo. Full-job fields preserve native text and date strings; analysis requests retain verified void semantics, with a typed AnalysisEngine selector. Project retrieval returns immutable status/data with no public reference parameters. See the [job/analysis/project guide](Root-Job-Analysis-Projects.md) for inputs, flags, exceptions, examples and verification limits. These five native mappings and six managed signatures do not change private-source parity.

## Creation, views, process-handle data and shutdown

NewSTAADFile, CreateNamedView, SaveNamedView, GetProcessHandle and Quit are implemented. Native model creation validates an absent target and unnamed session without promising atomic no-overwrite. View requests follow the inspected two-input create and one-input save signatures, preserving required raw creation flags without guessed meanings. Process-handle data is an opaque NativeProcessHandleValue, not an owning or verified caller-process handle. Quit is explicit and does not change disposal. See the [creation/view/lifecycle guide](Root-Creation-Views-and-Lifecycle.md). These five native mappings add five managed signatures; private-interface parity is unchanged.

## Completed final functions and capabilities

RemoveNamedView and ModifyNamedView use the inspected by-value/four-input signatures. GetAnalysisErrorMessages, GetAnalysisWarningMessages and ExportModelToAdina are available through independent typed acquisitions on Capabilities (GetAnalysisErrorMessages, GetAnalysisWarningMessages and GetAdinaExport). Exact candidate profiles and compatible metadata are required; these checks are not runtime certification. RetrieveMemberForces completes the private helper with MemberForcesResult and source-compatible models. See the [completion guide](Root-Completion-and-Capabilities.md) for all statuses, arrays, lifetime rules and limits.

## Ported functions

| Signature | Native/managed implementation | Empty/unnamed-session behavior |
| --- | --- | --- |
| `string GetStaadFileFullPath()` | One `GetSTAADFile(ref object, true)` call; returns the exact native string | Empty string |
| `string GetStaadFileNameOnly()` | One full-path read, then Windows `Path.GetFileName` | Empty string |
| `string GetAnlFileFullPath()` | One full-path read; same directory and filename stem plus uppercase `.ANL` | Empty string |
| `string GetOpenedStaadFilename()` | One full-path read, normalized with Windows `Path.GetFullPath` | Empty string |
| `bool IsTarget(string staadFileFullPath)` | Validate/normalize requested path, read native path once, compare complete normalized paths with ordinal case-insensitive comparison | False for a valid requested path |
| `bool ModelIsOpen(string fileFullPath)` | Same complete-path identity contract as `IsTarget`; not a Geometry/Load readiness check | False for a valid requested path |
| `ShortJobInfo GetShortJobInfo()` | One void native three-field metadata read; fresh standalone record | Explicit strings required; unwritten fields throw |
| `FullJobInfo GetFullJobInfo()` | One void native thirteen-field read; three dates mapped with CurrentCulture | Unwritten fields throw; explicitly empty dates map to DateTime.MinValue |
| `IOSRoot SetInputUnitsEx(LengthInputUnit lengthUnit, ForceInputUnit forceUnit)` | Validate enums, then one native input-unit mutation using integer codes | Native handling applies; no readiness check/read-back |
| `bool SetSilentMode(SilentMode silentMode)` | Validate code 0/1, then one native call; discard previous-state VARIANT | True for call completion, false for dispatch exception; no readiness check |
| `IOSRoot SaveModel(SilentMode silentMode)` | Validate code 0/1, then one void native save call; return this adapter | Native handling/dialogs apply; no persistence verification |
| `IOSRoot CloseSTAADFile()` | One void native close call; return this adapter | Native handling/dialogs apply; no close verification |

| `IOSRoot OpenSTAADFile(string filePath)` | Validated absolute path to one native open request; returns this adapter | Native dialogs apply; no readiness verification |
| `IOSRoot ShowApplication()` | One native visibility request; returns this adapter | No model or visibility verification |

| `bool AwaitModelHandle(string fileFullPath, double timeoutDurationInSeconds = 60, double sleepIntervalInSeconds = 0.1)` | Poll exact normalized path with monotonic timeout | Blank is a nonmatch; no model opening |
| `bool OpenStaadModel(string staadFileFullPath, int sleepInterval, int maxWaitTime = 600)` | One open request then full-path polling; milliseconds interval, seconds limit | Timeout does not undo native opening |

| `void CloseCurrentStaadModel()` | Read filename; if non-whitespace, enable silent mode and request close | Blank/whitespace skips mutation; no close verification |
| `bool CloseWriteReopenStaadFile(string filePath, List<string> fileContents, int sleepTimeMilliseconds = 200, int maxTimeMilliseconds = 5000)` | Close matching target, await blank/unlocked state, atomically replace then request reopen | False for mismatch, wait expiry or intervening switch; partial effects possible |

| `void MinimizeStaadMainWindow()` | Validate native window/PID then post ShowWindowAsync command 6 | No model mutation or final-state verification |
| `void MaximizeStaadMainWindow()` | Validate native window/PID then post ShowWindowAsync command 3 | No model mutation or final-state verification |

| `void RestoreStaadMainWindow()` | Validate native window/PID and post ShowWindowAsync command 9 | Accepted request does not verify final state |
| `AnalysisResult AnalyzeEx(SilentMode silentMode, HiddenMode hiddenMode, WaitAnalysisMode waitMode)` | Validate/send native silent, hidden, wait codes; preserve integer analysis status | Starts native analysis; wait mode can block |

The six filename/identity methods are eager, read-only session queries. They do not launch STAAD, open/close/save a model, change activation or load lists, read analysis contents, create files, perform filesystem existence checks or start analysis. A licensed STAAD installation and a valid native root/session are required for real calls. Coordinate COM access on the session's appropriate thread; a managed binding does not establish readiness or thread safety.

### Native contract and error handling

The installed Bentley STAAD.Pro 2025 reference `OSAPP_Help/group___root.html` documents `GetSTAADFile` as **void**, with a VARIANT string output and a Boolean full-path flag. The port sends Boolean `true` rather than the main reference's string `"TRUE"`; no numeric return status is expected or interpreted. The output is initialized with a string sentinel, so it marshals as BSTR while detecting unwritten responses. A null initializer caused native `DISP_E_BADVARTYPE` (0x80020008) and is corrected in this increment. Only an explicitly written string other than the sentinel is accepted.

Null, non-string and unwritten output throw `InvalidOperationException`. A genuinely empty native string is preserved. Full-path retrieval retains the original case, whitespace, Unicode and relative/absolute form; it does not normalize or certify the path. COM exceptions and missing-signature `RuntimeBinderException` propagate unchanged.

The two derived queries use .NET Framework's Windows `Path` APIs. Invalid path syntax, such as embedded NUL, can throw `ArgumentException`. Filename retrieval keeps the original final extension and all preceding periods. Analysis-path derivation removes only the final extension and appends `.ANL`. Relative filenames produce relative analysis paths. The returned path does not prove that an analysis file exists or that results are current.

### Improvement over the main implementation

The main `GetAnlFileFullPath` reads the native model path twice: directly and through `GetStaadFileNameOnly`. If the native session changes between reads, it can mix one model's directory with another model's name. This port derives both parts from **one native read**. It returns an empty string for empty native input instead of attempting to combine a missing directory with `.ANL`. Native model changes after the read remain possible.

### Normalized model identity

`GetOpenedStaadFilename` normalizes a single native read with Windows `Path.GetFullPath`. Empty native text stays empty; relative text is resolved against the process working directory. It does not check file existence, resolve symlinks/junctions or certify model readiness.

`IsTarget` and `ModelIsOpen` validate their requested path before native access. Null throws `ArgumentNullException` with the respective public parameter name; empty/whitespace throws `ArgumentException`. Malformed, unsupported or excessively long paths propagate the Windows Path API's exceptions. Each predicate then reads the native path once and compares complete normalized paths using `StringComparison.OrdinalIgnoreCase`. Case and dot-segment differences can match; equal basenames in different folders cannot. An explicitly unnamed session returns false for a valid requested path. Native failures throw rather than returning false.

The reference `ModelIsOpen` passes the filename as the API's Boolean full-path flag and compares only basenames. This port corrects both behaviors. The reference `GetOpenedStaadFilename` attempts to normalize empty text; this port returns an empty string for that legitimate native output. No model switching, launching, saving or native mutation is introduced.

### Usage and IntelliSense

With a connected wrapper:

```csharp
IOSRoot root = wrapper.Root;
Console.WriteLine(root.GetStaadFileFullPath());
Console.WriteLine(root.GetStaadFileNameOnly());
Console.WriteLine(root.GetAnlFileFullPath());
Console.WriteLine(root.GetOpenedStaadFilename());
Console.WriteLine(root.IsTarget(@"C:\Models\Frame.v2.STD"));
Console.WriteLine(root.ModelIsOpen(@"C:\Models\Frame.v2.STD"));
```

Both interface and concrete methods include matching expanded XML summaries, returns, usage remarks, exception contracts and examples. The compiled XML ships next to the `net481` DLL, so IntelliSense works from an installed NuGet package without source files. Constructor and root-binding properties also retain their documentation.

Offline coverage checks local/UNC/relative and Unicode paths, multiple periods, empty input, no filesystem assumptions, malformed/unwritten outputs, native exceptions, missing signatures, invalid path syntax, one-call derivation and isolation between wrapper sessions. Isolated package verification compiles/runs all twelve root XML examples and checks all six root methods. Native root path reads should still be checked against the connected STAAD session before claiming version-specific support.

## Job information and input units

The next batch preserves these main-project signatures:

```csharp
ShortJobInfo GetShortJobInfo();
FullJobInfo GetFullJobInfo();
IOSRoot SetInputUnitsEx(LengthInputUnit lengthUnit, ForceInputUnit forceUnit);
```

### Short and full job snapshots

Both getters make one native **void** call with BSTR-initialized reference outputs; no status is interpreted. A fresh per-call string sentinel detects unwritten fields without confusing ordinary marker-like user text with an old fixed sentinel. Every output must be explicitly written as a string. Null/non-string/unwritten responses throw `InvalidOperationException`; COM and missing-signature binding failures propagate unchanged. All full-job strings are validated before parsing dates or constructing a result, so failures return no partial metadata object.

`GetShortJobInfo` maps name, client and engineer to `ShortJobInfo.Name`, `Client` and `Originator`. `GetFullJobInfo` uses this exact thirteen-slot native order:

| Native output | Managed property |
| --- | --- |
| jobName | `Name` |
| jobClient | `Client` |
| enggName | `Originator` |
| eDate | `Originated` |
| jobNumber | `Number` |
| revision | `Revision` |
| part | `Part` |
| reference | `Reference` |
| checkerName | `Checker` |
| cDate | `Checked` |
| approverName | `Approver` |
| aDate | `Approved` |
| comments | `Comments` |

Standalone `ShortJobInfo` and `FullJobInfo` live in `StaadPro.Interop.Models`, retaining the main model inheritance, property names/types and factory signatures while using the repository's property store. Both are editable snapshots with `PropertyChanged`; edits perform no native writes. Successive calls return independent records. Text preserves empty strings, whitespace, Unicode and line breaks; manual factory callers may also supply null text.

The three full-job date properties retain the reference's `DateTime` type. Null/empty/whitespace factory date input means unspecified and maps to `DateTime.MinValue`; native null output is rejected earlier by the adapter. Nonempty dates use `DateTime.Parse` with the calling thread's `CurrentCulture` and `DateTimeStyles.None`. Invalid dates throw `FormatException`; ambiguous date strings follow that culture without trying alternatives. This preserves the reference's culture behavior while handling unspecified dates explicitly. Original date strings are not retained. Consumers must distinguish unspecified dates before displaying them as real dates.

The getters query job metadata associated with the current `.STD` model. They do not open/save models, change units, certify readiness or read analysis results. An unnamed application session is not guaranteed to supply job metadata: read-only checks on STAAD.Pro Advanced 2026's unnamed session left the outputs unwritten, and both getters rejected that response rather than inventing a blank record. Named-model native metadata and date formats remain a live-validation gate.

### Input-unit selection

`SetInputUnitsEx` validates both public enums before one **void** `SetInputUnits` call, passing integer length then force codes unchanged. It returns this adapter once the native call completes without exception. No status parsing or read-back occurs. Undefined values throw `ArgumentOutOfRangeException` naming `lengthUnit` or `forceUnit` before native mutation; COM/binding exceptions propagate. Native failures may have partial effects, and the wrapper supplies no rollback or automatic save.

| LengthInputUnit | Code |
| --- | --- |
| Inch | 0 |
| Feet | 2 |
| CentiMeter | 3 |
| Meter | 4 |
| MilliMeter | 5 |
| DeciMeter | 6 |
| KiloMeter | 7 |

Force codes 0–7 are `KiloPound`, `Pound`, `KiloGram`, `MetricTon`, `Newton`, `KiloNewton`, `MegaNewton` and `DecaNewton`. The installed vendor reference also lists length code 1 as Feet, but it has no named member in the existing public enum and is rejected. The port preserves the published enum rather than introducing an undocumented cast-only alias.

This changes native input-unit state for subsequent operations. It does not convert existing managed data/results, change the base unit system, restore previous units or save the model. Use the correct COM thread and coordinate session mutations. Offline coverage checks every defined length/force pair and invalid enums; live unit switching has not been performed by this increment.

```csharp
var shortJob = root.GetShortJobInfo();
Console.WriteLine(shortJob.Name + ": " + shortJob.Client);
var fullJob = root.GetFullJobInfo();
if (fullJob.Originated != DateTime.MinValue) Console.WriteLine(fullJob.Originated);
root.SetInputUnitsEx(LengthInputUnit.Meter, ForceInputUnit.KiloNewton);
```

All new interface/concrete methods and model types, constructors, factories and properties ship expanded XML IntelliSense. The job/unit batch's installed-package consumer compiled/ran **99 XML examples**, including all eighteen root examples, and checked field mapping, date handling, typed model availability and unit-call arguments. Vendor source: installed Bentley STAAD.Pro 2025 `OSAPP_Help/group___root.html`. Main-project provenance remains the commit and source paths listed above, plus `Models/ShortJobInfo.cs` and `Models/FullJobInfo.cs`.

## Silent mode, saving and closing

This batch ports `SetSilentMode(SilentMode)`, `SaveModel(SilentMode)` and `CloseSTAADFile()` from the main project, with the same parameter names and return types. `SilentMode` is a standalone public enum with `Disable=0` and `Enable=1`. Both enum-accepting methods reject undefined values with `ArgumentOutOfRangeException` naming `silentMode` before native access; the main project previously passed unchecked values. Native arguments are integers, without requiring Bentley interop types at build time.

### Silent-mode return contract

The installed STAAD.Pro 2026 `OSAPP_Help/group___root.html` documents native `SetSilentMode` as a VARIANT-returning method whose result is the **previous** mode. This port preserves the main project's Boolean **call-completion** meaning: true if the call returns without an exception, false if native dispatch throws, including a missing signature. It does not convert the previous flag to Boolean, so a previous zero remains a successful call. It does not validate or expose that native return, verify the resulting state, or return the caught exception. Argument-validation failures throw instead of returning false.

The flag changes application-level warning suppression for later analysis and saving. Enabling it can suppress supported warning boxes such as empty loads and repeat-load expansion; licensing and inaccessible-folder errors can still display dialogs. It is not a headless-operation guarantee, is not automatically restored, and does not itself save, close or analyze a model. A false result does not prove that no native state changed before the exception.

### Save and close contracts

Native `SaveModel` and `CloseSTAADFile` are **void**. The adapter calls each once and returns the same `IOSRoot` reference after the call returns without throwing. It interprets no success status and performs no filename/readiness/read-back checks. Native COM and missing-signature errors propagate unchanged; no retries or rollback are supplied.

`SaveModel` passes the caller's validated silent option directly to the native save function, without a separate `SetSilentMode` call. It may write the current model's file or display native prompts, especially for unnamed models or errors. Returning the adapter does not independently establish durable persistence or whether a user completed a dialog.

`CloseSTAADFile` requests closing the current `.STD` model. It does not explicitly save, set or restore silent mode, terminate STAAD, dispose the adapter, or open a replacement model. Native dialogs and user decisions can affect completion. Previously captured model interfaces can become unusable after closing; the returned root reference does not certify that it or the old wrapper adapters remain ready.

Call and serialize all three methods on the correct session COM thread. Synchronous native calls can block on dialogs. The adapter does not supply cancellation, forced close, dirty-state detection or timeout control. Native failure may have partial effects.

```csharp
bool completed = root.SetSilentMode(SilentMode.Enable);
Console.WriteLine(completed); // call completion, not the previous mode

// Explicit native mutations; perform only when the application intends to save/close.
root.SaveModel(SilentMode.Enable);
root.CloseSTAADFile();
```

Offline tests cover both codes, validation before dispatch, previous zero/one/null/Boolean results, native side effects on failure, exception/binder handling, exact single-call dispatch, fluent return identity, explicit save-then-close order and isolation between bindings. All **760 tests** pass, including 27 new cases. Matching interface/concrete XML documents all parameters, returns, side effects, exceptions and examples. The isolated installed-package verifier compiles and executes **129 XML examples**, including six additional examples for this batch and all 24 root examples, using managed native stand-ins; it does not save or close a customer STAAD model. Live native save/close and broader-version validation remain pending. Custom `IOSRoot` implementations must add these three methods, and consumers must rebuild.

## Model opening and application visibility

`OpenSTAADFile(string filePath)` preserves the main signature and delegates to one native void OpenSTAADFile call. Null throws `ArgumentNullException` naming `filePath`; empty/whitespace and invalid Windows syntax throw before native access. Relative paths are resolved with `Path.GetFullPath` once before dispatch, so native working-directory changes do not reinterpret the original relative input. File existence is not checked; no file or application process is created by the adapter. Native dialogs, model switching and unsaved work are STAAD's responsibility. No automatic save, retry or rollback is supplied. The fluent return does not establish identity or readiness, and captured model interfaces can become unusable after switching.

`ShowApplication()` calls the captured root's visibility method once without arguments and returns the same adapter. It does not explicitly maximize/restore/focus the window, open a model or start STAAD. The native return is ignored; dispatch completion does not independently verify visibility. COM and missing-signature errors propagate for both methods. Serialize access on the correct COM thread; synchronous calls can block on dialogs.

```csharp
root.OpenSTAADFile(@"C:\Models\Frame.v2.STD");
root.ShowApplication();
```

Tests cover full/relative/UNC/Unicode paths, input rejection before calls, exact dispatch, native exception identity, missing signatures and fluent return identity. Main-project signatures remain unchanged; path validation/normalization is an explicit improvement over unchecked input. The installed STAAD.Pro 2026 root reference documents OpenSTAADFile as void; ShowApplication remains late-bound to the reference's no-argument call.

Validation: **774 offline tests** passed. Installed-package verification executes **133 XML examples**. Native mutation/version checks remain pending.

## Waiting for model identity and opening with a deadline

`AwaitModelHandle` is an attachment/identity poll only. It normalizes the requested path before native access and validates finite positive timeout/interval values within TimeSpan limits. It checks immediately and reads one native filename per poll, comparing full normalized paths case-insensitively. Explicit blank filenames are nonmatches; unreadable/unwritten/malformed native output and COM/binding failures throw. It does not launch, open, save or close a model.

`OpenStaadModel` validates its path, positive millisecond `sleepInterval` and positive second `maxWaitTime`, starts its monotonic budget, requests native OpenSTAADFile once, then polls the same exact identity predicate. It requests opening even if that model was already open; callers should explicitly select this mutation. Unlike the main reference, it uses elapsed seconds instead of a count of loop iterations and never matches a basename substring from another path. The open call counts toward the budget. A timeout does not undo native effects or establish that the model stayed closed.

Both methods cap each sleep to remaining time, use caller-thread synchronous waits without message pumping/cancellation, and return true only when the match is observed within the logical budget. Thread.Sleep may overshoot, and blocked COM calls cannot be forcibly interrupted. The result establishes filename identity rather than Geometry/Load readiness, analysis availability, durable file state or permanent session identity. Serialize operations on the correct COM thread. Native dialogs, unsaved work and captured-interface invalidation remain relevant to opening.

```csharp
bool matches = root.AwaitModelHandle(@"C:\Models\Frame.v2.STD", 1, 0.01);
bool opened = root.OpenStaadModel(@"C:\Models\Frame.v2.STD", 10, 1);
```

Deterministic managed clocks cover immediate/later matches, blanks, same-name/different-folder mismatches, capped final sleeps, expiry during open/read, invalid paths/durations and native failures without real-time waits. The original public default values and parameter names are preserved. An internal per-call clock factory supports tests without adding public members or partial adapters.

Validation: **808 offline tests** passed. Installed-package verification executes **137 XML examples**. Native mutation/version checks remain pending.

## Conditional closing and close/write/reopen coordination

`CloseCurrentStaadModel()` reads one full native filename, skips mutation for explicitly empty/whitespace text, otherwise calls native SetSilentMode(1) then CloseSTAADFile once. It corrects the reference's null full-path flag. Failures propagate in order; this operation does not explicitly save, verify closing, close the application, dispose the wrapper or restore silent mode. Unreadable/unwritten output is an error rather than an invented blank. Captured model interfaces can become unusable after closing and the filename can change after the read.

`CloseWriteReopenStaadFile` preserves the main signature and defaults. It validates the requested Windows path, non-null List<string>, positive sleep interval and nonnegative wait limit, and snapshots lines before mutation. A different/blank current model returns false before filesystem validation or native changes. A matching existing target is validated for missing/directory/read-only errors, then native silent mode is enabled and closing requested without saving.

The monotonic limit applies to close completion/file-lock release after the close request. The method awaits an explicitly empty native filename and exclusive read/write availability. Zero permits an immediate check without sleeping. Windows sharing/lock violations are retryable; other errors propagate rather than becoming lock timeouts. Sleeps are capped by the remaining budget. A different intervening model prevents writing; blank identity is rechecked before publishing. After writing, a different model prevents reopen and returns false, although replacement has already occurred.

A complete UTF-8 file without BOM is written to an exclusively created sibling temporary path, with platform newlines and null entries as empty lines. File.Replace publishes it over the existing target without truncation fallback, backup or directory creation. Only owned temporary cleanup is attempted; cleanup failure can leave that temporary file. Original encoding/metadata preservation is not promised. Availability probes do not lock out external writers or the STAAD UI through publication; external races can still cause failure or changes. A successful rewrite is intentional replacement, not an append or managed snapshot edit.

True means replacement completed and the requested target was already identified or a reopen request returned; it does not verify reopen readiness. False can leave the model closed or the replacement published. Native/IO failures propagate, with no rollback or silent-mode restoration. Synchronous close/write/open calls are outside forced timeout control. Serialize all access on the appropriate COM thread and keep the supplied list stable during snapshotting.

```csharp
root.CloseCurrentStaadModel(); // explicit close without an automatic save
// Rewrite applies only if this path is still the current model.
var lines = new List<string> { "STAAD SPACE", "UNIT METER KN", "FINISH" };
bool requested = root.CloseWriteReopenStaadFile(@"C:\Models\Frame.v2.STD", lines);
```

Tests cover call ordering, blank/nonmatching models, argument validation, line snapshots, partial native/file effects, bounded waits, intervening switches and original failures. Owned temporary files additionally verify sharing locks, read-only/missing targets, complete UTF-8 publication, failed-replacement byte preservation and cleanup. Installed-package validation rewrites an owned file through a managed native stand-in; it never closes a customer model.

Validation: **841 offline tests** passed. Installed-package verification executes **141 XML examples**. Native mutation/version checks remain pending.

## Main-window minimization and maximization

`MinimizeStaadMainWindow()` and `MaximizeStaadMainWindow()` preserve the reference's no-argument void signatures. Both read the captured root's native GetMainWindowHandle and GetProcessId once. Supported signed/unsigned CLR integer values and IntPtr handles are accepted within pointer-size limits; null, Boolean, floating-point/string, zero/sentinel handle and invalid process outputs throw InvalidOperationException. No model path or Geometry/Load query is issued.

A standalone internal window service checks that the window exists and its owning process matches the root-reported PID, then posts ShowWindowAsync command 6 (minimize) or 3 (maximize). A rejected request throws without inventing a last-error value. Accepted posting means the operation started, not that the final state or focus was observed. Unlike the main helper's blocking ShowWindow call, asynchronous posting avoids waiting for another application's show-state processing. Native COM identity reads can still block. No retry, rollback, model mutation, application launch or completion wait is provided. See Microsoft's [ShowWindowAsync contract](https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindowasync).

Identity checks are snapshots, not a permanent ownership guarantee. Windows handles can be destroyed and recycled after validation, so UI/session coordination is still required; see Microsoft's [IsWindow race guidance](https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-iswindow). Calls must use the appropriate COM thread. Windows APIs require an interactive Windows session with appropriate privileges; accepted posting does not override native UI restrictions.

```csharp
root.MinimizeStaadMainWindow();
root.MaximizeStaadMainWindow();
```

Managed tests inject a fake window API to verify codes, handle representations, PID association, absent/foreign/malformed windows, rejected posts and original COM/binding failures. Tests do not change customer windows. The four new installed-package XML examples are compiled with window mutations guarded off; their real UI branches are deliberately not invoked by package verification. Live window-state and broader-version validation remain pending.

Validation: **868 offline tests** passed. Installed-package verification checks **145 XML example blocks**. Native mutation/version checks remain pending.

## Window restoration and native analysis requests

`RestoreStaadMainWindow()` uses the same validated internal window service as minimize/maximize, posting ShowWindowAsync command 9 after one native handle/PID read and a matching owner check. It preserves the main project's void signature. Accepted posting does not certify restored state or focus, and handle-recycling/UI races remain possible. Native/window failures propagate; there is no model mutation, process launch or completion wait. The same [Microsoft show-state contract](https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindowasync) and [handle-lifetime caveat](https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-iswindow) apply.

`AnalyzeEx(SilentMode, HiddenMode, WaitAnalysisMode)` validates all enum values before one native AnalyzeEx call, passing integer silent, hidden and wait codes in that exact order. No pre-save, filename/readiness query, extra application SetSilentMode call, result-file retrieval, retry, rollback or managed timeout/cancellation is supplied. It requests real analysis of the current model and can generate/change output files or display native dialogs. Hidden and silent options are not a guarantee of unattended execution. Wait mode can block until the engine completes; Return mode does not mean analysis finished. Coordinate all operations on the correct COM thread.

| Enum | Codes |
| --- | --- |
| SilentMode | Disable=0, Enable=1 |
| HiddenMode | Disable=0 displays engine dialog, Enable=1 requests hiding it |
| WaitAnalysisMode | Return=0 requests returning without completion waiting, Wait=1 requests native waiting |

The installed STAAD.Pro 2026 root reference documents a native LONG analysis result:

| AnalysisResult | Code |
| --- | --- |
| Terminated | -1 |
| GeneralError | 0 |
| InProgress | 1 |
| CompletedSuccessfully | 2 |
| CompletedWithWarnings | 3 |
| CompletedWithErrors | 4 |
| NotPerformed | 5 |

These statuses are returned without turning native error/in-progress codes into exceptions. Unknown Int32 codes are preserved as undefined enum values, retaining the reference's numeric information; callers must inspect them rather than assume success. Only supported CLR integer values within Int32 range are accepted. Null, Boolean, floating-point, decimal/string responses or oversized integers throw InvalidOperationException after the request; the analysis side effect is not undone. Undefined input flags throw ArgumentOutOfRangeException before dispatch. COM and missing-signature exceptions propagate unchanged.

```csharp
root.RestoreStaadMainWindow(); // explicit UI-state request
AnalysisResult status = root.AnalyzeEx(SilentMode.Enable, HiddenMode.Enable, WaitAnalysisMode.Wait);
if (status == AnalysisResult.CompletedSuccessfully) Console.WriteLine("Analysis reported success");
else Console.WriteLine("Inspect native status: " + status);
```

Managed tests cover all eight flag combinations, every documented status, unknown integers, supported integer representations, rejected types/ranges, invalid inputs and original native failures. Restore tests verify command 9, foreign-window rejection and lookup-error propagation. The installed consumer uses a managed AnalyzeEx stand-in; all six window-example UI branches are guarded off. No customer analysis or window change is performed. Live engine/file/window behavior and broader-version validation remain pending.

Validation: **914 offline tests** passed. Installed-package verification checks **149 XML example blocks**. Native mutation/version checks remain pending.

## Completed private-source helper

`MemberForcesResult RetrieveMemberForces(IEnumerable<int> beamIds, IEnumerable<int> loadCaseIds)` completes the final private Root function. It reuses the ported MemberForces/MemberEnd models, validates and snapshots IDs, preserves native availability and per-end success/data, and retains local native units. The public Output adapter remains a placeholder; dependency calls are internal. See the [completion guide](Root-Completion-and-Capabilities.md#completed-private-source-helper).

Current source verification passes **1,547 tests**. Native behavior across actual STAAD builds, named-view modes, result units, file effects and shutdown still requires controlled validation. No NuGet archive was rebuilt; the historical installed-package checks above describe older archives.
