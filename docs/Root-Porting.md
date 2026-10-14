# Root adapter implementation and porting progress

The main project's `IOSRoot` exposes 23 methods. This repository now implements **14**, with **9 pending**. The existing late-bound root binding is retained: signatures belong to `Adapters/Interfaces/IOSRoot.cs`, implementations belong to the single non-partial `Adapters/Models/OSRootAdapter.cs`, and the connected `OpenStaadWrapper.Root` exposes that adapter.

Reference source: `ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSRoot.cs` and `Adapters/Models/OSRootAdapter.cs`, main-project commit `3f930d742f972c27d961f0e8eff9f1e11c520ff2`. Function names and signatures are preserved. The constructor accepts the raw root as an object and `ComObject` remains dynamic so no proprietary Bentley assembly is needed to build/install this package. The main project was not changed.

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

## Pending reference methods

This batch adds normalized-path, complete-path comparison and BSTR-buffer regression coverage. Read-only native checks on the running **STAAD.Pro Advanced 2026 unnamed session** succeeded for all six root methods: the four string queries returned empty text, and both identity predicates returned false for a valid named target. No model was opened or modified. Named/UNC comparisons are covered offline; broader native-version and named-model validation remains pending.

These source signatures describe future ports; their supporting enums/models/services are not implied to be available already:

- `bool AwaitModelHandle(string fileFullPath, double timeoutDurationInSeconds = 60, double sleepIntervalInSeconds = 0.1)`
- `void CloseCurrentStaadModel()`
- `bool CloseWriteReopenStaadFile(string filePath, List<string> fileContents, int sleepTimeMilliseconds = 200, int maxTimeMilliseconds = 5000)`
- `void MaximizeStaadMainWindow()`
- `void MinimizeStaadMainWindow()`
- `bool OpenStaadModel(string staadFileFullPath, int sleepInterval, int maxWaitTime = 600)`
- `void RestoreStaadMainWindow()`
- `HashSet<MemberForces> RetrieveMemberForces(IEnumerable<int> beams, IEnumerable<int> loadCases)`
- `AnalysisResult AnalyzeEx(SilentMode silentMode, HiddenMode hiddenMode, WaitAnalysisMode waitMode)`

## Suggested next sequence

Port open/wait handling next, followed by higher-level close/write/reopen coordination, window control through a standalone helper, and analysis/results retrieval with their required models. Each batch should include matching interface/adapter XML, input/native-contract tests and installed-package examples before making public compatibility claims.
