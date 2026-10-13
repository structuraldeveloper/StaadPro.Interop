# Root adapter implementation and porting progress

The main project's `IOSRoot` exposes 23 methods. This repository now implements **three**, with **20 pending**. The existing late-bound root binding is retained: signatures belong to `Adapters/Interfaces/IOSRoot.cs`, implementations belong to the single non-partial `Adapters/Models/OSRootAdapter.cs`, and the connected `OpenStaadWrapper.Root` exposes that adapter.

Reference source: `ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSRoot.cs` and `Adapters/Models/OSRootAdapter.cs`, main-project commit `3f930d742f972c27d961f0e8eff9f1e11c520ff2`. Function names and signatures are preserved. The constructor accepts the raw root as an object and `ComObject` remains dynamic so no proprietary Bentley assembly is needed to build/install this package. The main project was not changed.

## Ported functions

| Signature | Native/managed implementation | Empty native filename |
| --- | --- | --- |
| `string GetStaadFileFullPath()` | One `GetSTAADFile(ref object, true)` call; returns the exact native string | Empty string |
| `string GetStaadFileNameOnly()` | One full-path read, then Windows `Path.GetFileName` | Empty string |
| `string GetAnlFileFullPath()` | One full-path read; same directory and filename stem plus uppercase `.ANL` | Empty string |

All three are eager, read-only session queries. They do not launch STAAD, open/close/save a model, change activation or load lists, read analysis contents, create files, perform filesystem existence checks or start analysis. A licensed STAAD installation and a valid native root/session are required for real calls. Coordinate COM access on the session's appropriate thread; a managed binding does not establish readiness or thread safety.

### Native contract and error handling

The installed Bentley STAAD.Pro 2025 reference `OSAPP_Help/group___root.html` documents `GetSTAADFile` as **void**, with a VARIANT string output and a Boolean full-path flag. The port sends Boolean `true` rather than the main reference's string `"TRUE"`; no numeric return status is expected or interpreted. The output starts as null to detect an unwritten response. Only an explicitly written string is accepted.

Null, non-string and unwritten output throw `InvalidOperationException`. A genuinely empty native string is preserved. Full-path retrieval retains the original case, whitespace, Unicode and relative/absolute form; it does not normalize or certify the path. COM exceptions and missing-signature `RuntimeBinderException` propagate unchanged.

The two derived queries use .NET Framework's Windows `Path` APIs. Invalid path syntax, such as embedded NUL, can throw `ArgumentException`. Filename retrieval keeps the original final extension and all preceding periods. Analysis-path derivation removes only the final extension and appends `.ANL`. Relative filenames produce relative analysis paths. The returned path does not prove that an analysis file exists or that results are current.

### Improvement over the main implementation

The main `GetAnlFileFullPath` reads the native model path twice: directly and through `GetStaadFileNameOnly`. If the native session changes between reads, it can mix one model's directory with another model's name. This port derives both parts from **one native read**. It returns an empty string for empty native input instead of attempting to combine a missing directory with `.ANL`. Native model changes after the read remain possible.

### Usage and IntelliSense

With a connected wrapper:

```csharp
IOSRoot root = wrapper.Root;
Console.WriteLine(root.GetStaadFileFullPath());
Console.WriteLine(root.GetStaadFileNameOnly());
Console.WriteLine(root.GetAnlFileFullPath());
```

Both interface and concrete methods include matching expanded XML summaries, returns, usage remarks, exception contracts and examples. The compiled XML ships next to the `net481` DLL, so IntelliSense works from an installed NuGet package without source files. Constructor and root-binding properties also retain their documentation.

Offline coverage checks local/UNC/relative and Unicode paths, multiple periods, empty input, no filesystem assumptions, malformed/unwritten outputs, native exceptions, missing signatures, invalid path syntax, one-call derivation and isolation between wrapper sessions. Isolated package verification compiles/runs all six root XML examples and checks all three root methods. Native root path reads should still be checked against the connected STAAD session before claiming version-specific support.

## Pending reference methods

These source signatures describe future ports; their supporting enums/models/services are not implied to be available already:

- `bool AwaitModelHandle(string fileFullPath, double timeoutDurationInSeconds = 60, double sleepIntervalInSeconds = 0.1)`
- `void CloseCurrentStaadModel()`
- `bool CloseWriteReopenStaadFile(string filePath, List<string> fileContents, int sleepTimeMilliseconds = 200, int maxTimeMilliseconds = 5000)`
- `FullJobInfo GetFullJobInfo()`
- `string GetOpenedStaadFilename()`
- `ShortJobInfo GetShortJobInfo()`
- `bool IsTarget(string staadFileFullPath)`
- `void MaximizeStaadMainWindow()`
- `void MinimizeStaadMainWindow()`
- `bool ModelIsOpen(string fileFullPath)`
- `bool OpenStaadModel(string staadFileFullPath, int sleepInterval, int maxWaitTime = 600)`
- `void RestoreStaadMainWindow()`
- `HashSet<MemberForces> RetrieveMemberForces(IEnumerable<int> beams, IEnumerable<int> loadCases)`
- `IOSRoot SetInputUnitsEx(LengthInputUnit lengthUnit, ForceInputUnit forceUnit)`
- `bool SetSilentMode(SilentMode silentMode)`
- `IOSRoot SaveModel(SilentMode silentMode)`
- `IOSRoot CloseSTAADFile()`
- `IOSRoot OpenSTAADFile(string filePath)`
- `IOSRoot ShowApplication()`
- `AnalysisResult AnalyzeEx(SilentMode silentMode, HiddenMode hiddenMode, WaitAnalysisMode waitMode)`

## Suggested next sequence

Port the remaining path/identity predicates first, then input-unit/job metadata and silent/save operations. Follow with open/close/wait handling, window control through a standalone helper, and analysis/results retrieval with their required models. Each batch should include matching interface/adapter XML, input/native-contract tests and installed-package examples before making public compatibility claims.
