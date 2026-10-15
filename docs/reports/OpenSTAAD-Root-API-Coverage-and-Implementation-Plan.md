# OpenSTAAD Root API coverage and implementation plan

**Review date:** 15 October 2026  
**Scope:** Installed Bentley OpenSTAAD 2026 Root reference versus the current `main` branch and its built .NET Framework assembly.  
**Status:** The wrapper foundation and five typed Root metadata queries are implemented. Twenty-five dedicated Root functions and version-aware capability dispatch remain pending. New public API contracts follow the [managed API design rules](../Root-Managed-API-Design.md). See [Wrapper-Porting](../Wrapper-Porting.md) for wrapper scope.

For the 2024/2025 comparison, internal service architecture and version-specific API access, see the [multi-version architecture and implementation plan](OpenSTAAD-Root-Multi-Version-Architecture-and-Plan.md). Its capability-interface and unit-query interpretation proposals refine this report’s 2026-only implementation plan; the wrapper foundation and five metadata projection methods are implemented.

## 1. Findings

The supplied Root page documents **39 distinct functions**. The repository exposes **27 public methods on `IOSRoot`**, but those numbers measure different things: many adapter methods combine native operations or provide managed conveniences.

| Coverage category | Documented functions | Meaning |
| --- | ---: | --- |
| Public `IOSRoot` operations backed by the documented native function | 14 | Available through the adapter, with the qualifications below; native and managed signatures are intentionally different in several places. |
| Wrapper-property projections absent from `IOSRoot` | 0 | All five now have dedicated typed Root methods; their wrapper-bound path preserves snapshot and fallback semantics. |
| Native calls used internally, absent as public query methods | 1 | Process ID remains internal to the existing window-control operations. |
| No implementation call found in current production source | 24 | Not ported, even if callers could invoke them manually through the raw COM object. |
| **Total documented Root functions** | **39** | Every function occurs once in the coverage matrix. |

Consequently, **25 documented functions have no dedicated public `IOSRoot` method**: 24 have no production native implementation call and GetProcessId remains a native internal query. Section 7 retains the original 30 plan entries, with P02/P03/P04/P06/P09 marked implemented. Existing public coverage also has contract limitations that need separate treatment; calling a native function is not proof of complete parity or live compatibility.

The older **22 of 23** figure in [Root-Porting.md](../Root-Porting.md) compares this repository with the private main project's interface. It is not coverage of Bentley's 39-function reference. That private interface's remaining `RetrieveMemberForces` helper is not one of these 39 Root functions and is outside this plan.

## 2. Evidence and review boundaries

### Bentley reference

- [Installed Root reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html), documentation version **26.00.00.340**, generated with Doxygen **1.13.2**.
- The supplied fragment, `ga646f2f0b6983135b4b3a8ded877475d7`, points specifically to **Analyze**. This review covers the entire Root group, not only that anchor.
- Source file: [Root reference HTML](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html).
- Source size: **136,359 bytes**. SHA-256: `3CAF7922E7A0BF895CF237106B02B7D923F0CD33778DCD7D741867801BFA64A5`.
- Both the summary declarations and all 39 detailed member sections were inspected. Where these disagree, the discrepancy is recorded rather than silently resolved from an example.

### Repository and assembly baseline

The following assembly/package evidence records the original pre-wrapper-port baseline. The working tree now contains the wrapper changes described in [Wrapper-Porting](../Wrapper-Porting.md); its rebuilt DLL is newer than that package. No NuGet package was rebuilt for the wrapper port.

- Branch: `main`; reviewed commit: `36e9f811f28cf6c4594045b31b7bd374a565187e`.
- [IOSRoot](../../src/StaadPro.Interop/Adapters/Interfaces/IOSRoot.cs), [OSRootAdapter](../../src/StaadPro.Interop/Adapters/Models/OSRootAdapter.cs), [OpenStaadWrapper](../../src/StaadPro.Interop/Models/OpenStaadWrapper.cs), root helpers, provider/connection code, existing tests and package project were examined.
- Reflection over the [built net481 assembly](../../src/StaadPro.Interop/bin/Release/net481/StaadPro.Interop.dll) confirmed **22 non-property interface methods**. Assembly identity: `StaadPro.Interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null`.
- DLL SHA-256: `E579221EB5B49D8AF9A6F1884140FB85FC744B562F62DA1AD6213DB7A46B92C8`.
- At the baseline inspection, this DLL matched the DLL inside the existing `StaadPro.Interop.1.0.0-preview.20261014.7.nupkg`; the package records the reviewed repository commit.
- The current project targets **net481**. A leftover `net8.0-windows` build directory is not evidence that this branch or package targets .NET 8. No .NET Core support is inferred from it.

The initial review was static plus assembly/package inspection. The later wrapper port was built and tested offline; current validation is recorded in Wrapper-Porting. No STAAD model was opened, modified, analyzed or closed, and no live COM experiment or new NuGet build/publication was performed. Offline stand-ins cannot establish native version-specific compatibility.

Links to Bentley help target the local installed reference; their relative targets are calculated from this report’s current workspace location. Source links are relative to this report. The native declarations below are contract notation, not proposed C# source code.

## 3. Complete coverage matrix

**A:** adapter API exists. **W:** wrapper-property projection only (none remain without a Root method). **I:** internal use only. **N:** no production implementation call found. A plan ID identifies a missing dedicated adapter method.

| Bentley function | Native return | Coverage | Current exposure / plan |
| --- | --- | --- | --- |
| [Analyze](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga646f2f0b6983135b4b3a8ded877475d7) | `void` | N | Missing. [Plan P21](#p21-analyze). |
| [AnalyzeEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga46f03d1be3c56877f5af4359764df907) | `long` | A | `AnalysisResult AnalyzeEx(SilentMode, HiddenMode, WaitAnalysisMode)`. |
| [AnalyzeModel](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gadaa1e36df7e6b4fe5c056ede8270987c) | `void` | N | Missing. [Plan P22](#p22-analyzemodel). |
| [CloseSTAADFile](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gaf249dc19ac760422e78351546918e65d) | `void` | A | `IOSRoot CloseSTAADFile()` and higher-level close helpers. |
| [CreateNamedView](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gaf331e2b24c0e4f4e57c2951594423b2d) | `void` | N | Missing. [Plan P25](#p25-createnamedview). |
| [ExportModelToADINA](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga212f15a4b5984734985d197b67de4199) | `long` | N | Missing. [Plan P29](#p29-exportmodeltoadina). |
| [GetAnalysisErrorMessages](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga405205f5768fd9b1d16b8a35b72e86f4) | `long` | N | Missing. [Plan P13](#p13-getanalysiserrormessages). |
| [GetAnalysisStatus](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga731f741a97cb618d7409544055b8a2bb) | `long` | N | Missing. [Plan P12](#p12-getanalysisstatus). |
| [GetAnalysisWarningMessages](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gae891bbde4b6fd5e7f3e56dbf0644386f) | `long` | N | Missing. [Plan P14](#p14-getanalysiswarningmessages). |
| [GetApplicationVersion](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga658ee126cc9aa9c5e35866e35f60260e) | `VARIANT` | A | `OpenStaadWrapper.StaadVersion` is the cached native version; `ApplicationVersion` is the host version. Implemented; [P06](#p06-getapplicationversion). |
| [GetBaseUnit](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga7b1d941b62f317a0bdf104d2d4436ae1) | `VARIANT` | A | `OpenStaadWrapper.BaseUnitSystem`; cached, refreshed by RefreshUnits/RefreshComObjects. Implemented; [P02](#p02-getbaseunit). |
| [GetCONNECTEDProjectInfo](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gab92e524c05abdf5e59e54adc95e0c9b0) | `VARIANT` | N | Missing. [Plan P23](#p23-getconnectedprojectinfo). |
| [GetErrorMessage](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gab804cd4718b6a369ce6fb396c2df560d) | `VARIANT` | N | Missing. [Plan P07](#p07-geterrormessage). |
| [GetFullJobInfo](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga558ddaf8c6aa1913ed2ca06fd22beddd) | `void` | A | `FullJobInfo GetFullJobInfo()`; dates parsed with current culture. |
| [GetInputUnitForForce](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gabcc3db5863da3b119b2c5bd4a740f0cd) | `VARIANT` | A | `OpenStaadWrapper.InputForceUnit`; ref-output parsing and documented base-system fallback. Implemented; [P04](#p04-getinputunitforforce). |
| [GetInputUnitForLength](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga8a846a4f4597cbb6cbed46e86442431c) | `VARIANT` | A | `OpenStaadWrapper.InputLengthUnit`; ref-output parsing, feet alias and documented fallback. Implemented; [P03](#p03-getinputunitforlength). |
| [GetMainWindowHandle](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga0c0b0f425cd8588f0df3c40ea08a05da) | `VARIANT` | A | `OpenStaadWrapper.StaadApplicationWindowHandle`; bound-root default service, Connect-only cached lookup. Implemented; [P09](#p09-getmainwindowhandle). |
| [GetProcessHandle](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga275b4bdab39a0afb0764d4d2e0b2f13e) | `VARIANT` | N | Missing. [Plan P10](#p10-getprocesshandle). |
| [GetProcessId](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gac7c8010c34e57ab1443b8efacc3235a0) | `VARIANT` | I | Window-state helper calls; no public PID getter. [Plan P08](#p08-getprocessid). |
| [GetShortJobInfo](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga6d209f7c0ed0aaddfe84c6603078e5a2) | `void` | A | `ShortJobInfo GetShortJobInfo()`; three BSTR outputs. |
| [GetSTAADFile](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga2d0ebfc91441ee3b2331bcd2d0ad07d7) | `void` | A | `GetStaadFileFullPath()` and derived filename/identity helpers; native `false` flag not dispatched. |
| [GetSTAADFileFolder](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gaefa222ce265399686a53eb2a59d2e947) | `void` | N | Missing. [Plan P01](#p01-getstaadfilefolder). |
| [IsAnalyzing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga47cec0d70a609f2de2dd2e5214b7fa28) | `VARIANT` | N | Missing. [Plan P11](#p11-isanalyzing). |
| [IsPhysicalModel](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga95c33fb64a10923e179bf067fd006a7b) | `VARIANT` | N | Missing. [Plan P05](#p05-isphysicalmodel). |
| [ModifyNamedView](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gadfb2385ac03c7bf740e24eb24fcca286) | `void` | N | Missing. [Plan P28](#p28-modifynamedview). |
| [NewSTAADFile](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gacdd64b11752dc29f03537526d46553b4) | `void` | N | Missing. [Plan P17](#p17-newstaadfile). |
| [OpenSTAADFile](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga4ba715d4c1e519ed6f4645ca91db9f99) | `void` | A | `IOSRoot OpenSTAADFile(string)` and polling/rewrite helpers. |
| [Quit](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gadd624b0b5b4f45cf4ed97f9557cc88af) | `void` | N | Missing. [Plan P30](#p30-quit). |
| [RemoveNamedView](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gacd98d0159150ffada6b7e62f2e1687d4) | `void` | N | Missing. [Plan P27](#p27-removenamedview). |
| [SaveModel](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga2984f37b5dde4651ffa18deacf441645) | `void` | A | `IOSRoot SaveModel(SilentMode)`; request, no persistence verification. |
| [SaveNamedView](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gaeca040a52040da606a81af5407aa464e) | `void` | N | Missing. [Plan P26](#p26-savenamedview). |
| [SetCONNECTEDProjectInfo](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga61d5d3fc321d74be1ede4c318426b9f6) | `VARIANT` | N | Missing. [Plan P24](#p24-setconnectedprojectinfo). |
| [SetFullJobInfo](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga519b55d96da3c901a6b4149a77480c9b) | `void` | N | Missing. [Plan P19](#p19-setfulljobinfo). |
| [SetInputUnitForForce](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga0d92e92cebdffc1dbf951d07f8d29f1e) | `void` | N | Missing. [Plan P16](#p16-setinputunitforforce). |
| [SetInputUnitForLength](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga6bc9354604b80ca54912ec586c4ff931) | `void` | N | Missing. [Plan P15](#p15-setinputunitforlength). |
| [SetInputUnits](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga287a24364161a41879432ebc256faea6) | `void` | A | `SetInputUnitsEx(LengthInputUnit, ForceInputUnit)`; code 1 length alias absent. |
| [SetShortJobInfo](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga04af5d1d0aa713ba08032cf613aa2193) | `void` | N | Missing. [Plan P18](#p18-setshortjobinfo). |
| [SetSilentMode](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga21f063c7c8882dc7af286e5b18db11e0) | `VARIANT` | A | `bool SetSilentMode(SilentMode)`; previous native state discarded. |
| [UpdateStructure](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gaaf431f1bfbee142cd7d55d454d56d995) | `void` | N | Missing. [Plan P20](#p20-updatestructure). |

## 4. What the existing 22 adapter methods actually implement

| Current public signature | Implementation and limits |
| --- | --- |
| `string GetStaadFileFullPath()` | One native `GetSTAADFile(ref object, true)`; exact string, including empty; rejects unwritten/non-string output. |
| `string GetStaadFileNameOnly()` | Derives a filename from one full-path read; does not call native `GetSTAADFile(..., false)`. |
| `string GetAnlFileFullPath()` | Derives an expected `.ANL` path; does not prove that the file exists or read analysis diagnostics. |
| `string GetOpenedStaadFilename()` | Normalizes one native path; preserves an explicitly empty filename. |
| `bool IsTarget(string staadFileFullPath)` | Compares complete normalized paths, case-insensitively. Not a readiness check. |
| `bool ModelIsOpen(string fileFullPath)` | Same full-path identity contract; not process enumeration or session readiness. |
| `ShortJobInfo GetShortJobInfo()` | One three-output native metadata read; creates an editable managed snapshot. |
| `FullJobInfo GetFullJobInfo()` | One thirteen-output native read; parses dates using current culture, blank dates become `DateTime.MinValue`. |
| `IOSRoot SetInputUnitsEx(LengthInputUnit lengthUnit, ForceInputUnit forceUnit)` | Validates enum values and calls native `SetInputUnits`; no readback. |
| `bool SetSilentMode(SilentMode silentMode)` | Sends 0/1, returns true when dispatch completes, false for a caught dispatch exception. Discards native previous setting. |
| `IOSRoot SaveModel(SilentMode silentMode)` | Sends the save request with integer silent option; returns this adapter, not native success. |
| `IOSRoot CloseSTAADFile()` | Requests close; no save policy, completion polling or final-state verification. |
| `IOSRoot OpenSTAADFile(string filePath)` | Validates/normalizes path and requests opening; no file creation or readiness polling. |
| `IOSRoot ShowApplication()` | Calls native `ShowApplication`; that function is absent from the supplied Root group, so it does not increase the 39-function coverage. |
| `bool AwaitModelHandle(string fileFullPath, double timeoutDurationInSeconds = 60, double sleepIntervalInSeconds = 0.1)` | Polls model-path identity with a monotonic timeout. Does not validate geometry/load readiness. |
| `bool OpenStaadModel(string staadFileFullPath, int sleepInterval, int maxWaitTime = 600)` | Requests open then polls identity; interval is milliseconds, maximum wait is seconds. |
| `void CloseCurrentStaadModel()` | Reads filename; a named model triggers silent mode then close. Blank/whitespace skips mutation. |
| `bool CloseWriteReopenStaadFile(string filePath, List<string> fileContents, int sleepTimeMilliseconds = 200, int maxTimeMilliseconds = 5000)` | Closes a matching model, waits for file release, atomically replaces existing text and requests reopening; failures can follow partial effects. |
| `void MinimizeStaadMainWindow()` | Reads native HWND/PID, validates window ownership and posts Windows command 6. |
| `void MaximizeStaadMainWindow()` | Same validation, posts command 3. |
| `void RestoreStaadMainWindow()` | Same validation, posts command 9. Window-state requests do not prove final UI state. |
| `AnalysisResult AnalyzeEx(SilentMode silentMode, HiddenMode hiddenMode, WaitAnalysisMode waitMode)` | Sends three integer 0/1 flags, validates integer response and preserves unrecognized in-range status values. Wait mode can block. |

The 13 convenience/extra operations beyond the nine native-backed API mappings are the five derived filename/identity queries, `ShowApplication`, both polling/open helpers, both close/rewrite helpers, and three window-state operations. Constructors and the `ComObject` property are not counted as functions.

## 5. Contract gaps and ambiguities found during the review

### 5.1 Existing implementation limitations

1. **Wrapper input-unit reference outputs are now captured.** The wrapper port uses ref object string buffers, prefers parsed output over a usable return, rejects Boolean/fractional unit coercion, handles length code 1, and exposes cached units. Unit-query failures still use the source-compatible base-system fallback. The conservative 2024 rule avoids interpreting a documented success flag as a unit. This is not a native-verified version strategy, and a fallback must not be advertised as a measured unit. See Wrapper-Porting for details.
2. **`SetSilentMode` has different managed return semantics.** Bentley returns the previous mode. The current bool describes call completion, and all dispatch exceptions are swallowed into false. Do not document it as the previous mode. If exact parity is wanted later, add a separately named operation such as `SilentMode SetSilentModeAndGetPrevious(SilentMode mode)`; do not change the meaning of the existing signature. The native return subtype and allowable values still need verification.
3. **Native `GetSTAADFile` has a Boolean mode; the adapter always requests full path.** The filename-only helper performs managed path extraction. That is useful functional coverage, but it is not proof that the native false-flag branch behaves identically for every model state. An optional future `string GetSTAADFile(bool fullPath)` would expose that branch without changing existing helpers.
4. **Length-unit code 1 is not represented by the current enum.** Bentley documents 1 and 2 as feet/foot aliases. The enum has `Feet = 2`, and the existing validated setter rejects `(LengthInputUnit)1`. New getters must preserve the raw code and deliberately map this alias rather than treating it as malformed. An enum-extension or normalization decision is required before broadening existing setters.
5. **Full-job dates are converted rather than preserved as native text.** The current getter parses three BSTR date fields with `CurrentCulture`. A future setter must not assume it can reconstruct the original strings or format them arbitrarily. A thirteen-string native-shaped setter is the safest initial contract; a DTO/date-format convenience overload can follow separately after live format verification.
6. **Some operations are requests rather than verified outcomes.** Opening, saving, closing, window changes and analysis dispatch must retain this distinction in XML documentation. A successful dynamic call is not equivalent to persistence, readiness, analysis success or completed process termination.

The wrapper-specific correction in item 1 is implemented; the other Root follow-ups remain planned. The 30-entry missing-method plan does not count optional compatibility extensions to already exposed operations as additional missing Bentley functions.

### 5.2 Issues within the supplied Bentley documentation

| Area | Observed ambiguity | Required resolution before implementation/release |
| --- | --- | --- |
| Native `long` versus C# examples | Several C# snippets assign results to `long`. The native declarations use Windows `long`. | Treat the declared native status/count as 32-bit and propose C# `int`; confirm actual dispatch/typelib shape, especially ref outputs. Do not copy snippets mechanically. |
| `AnalyzeModel` | Declaration says `void`; the C# example assigns a return value. Only engine `-1` is documented. | Follow the declaration provisionally: managed `void`, input `-1`. Verify against the installed type library and a disposable live model. |
| Metadata getters and folder getter | Some output buffers are marked `[in]` in parameter prose. Their reference signatures and descriptions say they receive values. | Treat them as intended outputs, but verify readback/marshaling before release. Do not port the erroneous direction labels into XML. |
| Named-view `nError` | Marked `[in]`, but described as the error output. Error-code meanings are absent. | Verify reference-output behavior and code meanings. Do not presume zero means success until established. |
| Named-view flags | `CreateNamedView.nFlag` and `ModifyNamedView.nModifyFlag` have no documented value table. | Resolve from installed typelib/other Bentley reference and controlled tests; do not invent enum members or numeric meanings. |
| `ModifyNamedView.EntityArray` | Name and entity count imply an array; parameter prose describes a single long. Its purpose prose also resembles `SaveNamedView`. | Verify integer SAFEARRAY subtype, rank, bounds, direction and modification behavior; this is an implementation gate. |
| Process/window handles | Return type is only VARIANT; width, ownership and handle validity context are not specified. | Verify actual types and pointer conversion; a process handle must not be treated as usable/owned by the client without evidence. |
| Native parameter names | Summary/detail names differ for analysis status path, `AnalyzeEx` flags and save option. | Use exact positional order; record aliases in XML rather than treating them as overloads. |

## 6. Design rules for the planned work

The [managed API design rules](../Root-Managed-API-Design.md) govern new public contracts, result classes, collections, overloads and folder organization. No new public method uses `ref`, `out` or tuples. Native reference outputs are marshaled internally only.

- Add signatures to `Adapters/Interfaces/IOSRoot.cs` and implementations to the existing **single, non-partial** `Adapters/Models/OSRootAdapter.cs`. Helpers, DTOs and enums may have their own files. Do not create adapter partial classes.
- Reuse the now-ported wrapper as the metadata owner. Implemented P02/P03/P04/P06/P09 return its cached properties without duplicate native reads. Pass the owning wrapper through the internal metadata accessor while preserving the existing raw-root constructor. Providers are unchanged.
- Keep the late-bound integration and avoid a redistributable dependency on Bentley assemblies. Typelib inspection can establish contracts during development without requiring consumers to reference Bentley's interop DLL.
- Preserve existing method behavior. Adding members to public `IOSRoot` affects third-party implementations/mocks: record this API compatibility impact and choose an appropriate release/version policy before publishing. Existing compiled consumers that only call old members are a different compatibility case from consumers implementing the interface.
- Validate complete inputs before the first native mutation. Require valid strings, ranges, enums, counts and pointer conversions; do not coerce bool, floating-point numbers or numeric text into native integer statuses accidentally.
- Preserve native statuses rather than collapsing unrelated APIs into a generic bool. Unknown integer values must remain inspectable. Expected native failure codes are different from dispatch exceptions and malformed outputs.
- For new methods, let COM/binder exceptions propagate unless a specifically documented result contract requires otherwise. The implemented unit projections retain the wrapper's explicit compatibility fallback and optional-member handling; they do not claim an observed native unit. No retry of an uncertain mutation merely because its response was lost. Never convert connection failure into an ordinary unit or false predicate.
- A `void` native operation has no native success code. Planned managed mutators below use `void` to preserve that fact; matching the existing fluent style is possible but would still mean only that the request returned.
- Use the bound session's appropriate COM thread. No automatic `Task.Run` wrapper or assumption that a timeout can interrupt a blocked native call. Serialize coordinated operations as needed; an identity precheck does not prevent an external session change afterward.
- Native mutations remain explicit: no implicit file creation, opening, saving, closing, unit conversion, analysis, connection-project update or named-view overwrite from a query.
- All proposed signatures/DTOs below are **design proposals, not available APIs**. The exact return shape may be refined when the listed native-contract gates are resolved. Each native function remains one coverage item regardless of overloads or convenience wrappers.

### Supporting type policy

The completed wrapper now supplies the source-compatible `ApplicationVersion` model. Planned Root metadata projections use existing enum/value types; separate unit-info/version DTOs from the original proposal are superseded. Native output labels/raw return provenance are not retained by those wrapper properties and must not be fabricated.

### Other proposed supporting types

| Proposed type | Intended fields / semantics |
| --- | --- |
| `AnalysisStatusResult` | `int NativeStatus`, nullable recognized analysis state (including invalid path), explicitly defined query success, nullable `int WarningCount`, nullable `int ErrorCount`, nullable `double CpuTimeSeconds`; distinguish failed retrieval, analysis outcome and unavailable fields from real zero values. |
| `ConnectedProjectInfo` | `string ProjectId`, `string Name`; returned only for a successful native query. Project ID remains text until its accepted format is verified. |
| `NamedViewEntityType` | Documented values only: Node 0, Beam 1, Plate 2, Solid 3, Surface 4. |
| `AdinaAxisSystem` | Preserve 0, YUp 1, ZUp 2; names describe the documented export convention. |
| `AdinaLoadCaseHandling` | MultipleCases 0, TimeHistory 1. |

Do not add a speculative named-view error enum, creation/modification flag enum or owning safe-handle type before the documentation gaps are resolved.

## 7. Per-function implementation plan

Each entry includes the detailed-page native declaration, parameter direction and meaning, proposed managed API, implementation constraints, and tests. `VARIANT(text)` below means a VARIANT carrying a string/BSTR; it is not a new native type. `const VARIANT&` inputs are generally sent by value through automation; mutable VARIANT references require the verified `ref` dispatch shape. Native declarations keep Bentley's spelling and detail-section parameter names.

<a id="p01-getstaadfilefolder"></a>

### P01 — GetSTAADFileFolder

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gaefa222ce265399686a53eb2a59d2e947)

```cpp
void OpenSTAADUI::GetSTAADFileFolder ( VARIANT FAR & fileFolder );
```

| Native parameter | Direction | Type and meaning |
| --- | --- | --- |
| `fileFolder` | Output | VARIANT(text) reference receiving the model directory, excluding the filename. The prose labels it input, but describes an output. |

**Return contract:** Native void; managed result is the explicitly written directory string.

**Proposed managed signature:** `string GetStaadFileFolder()`.

**Implementation:** Call the native folder getter once with a string-initialized ref object. Preserve native spelling and trailing separator behavior. Accept an explicitly empty output for an unnamed model only if confirmed; reject null, non-string or unwritten values. Do not replace this API with Path.GetDirectoryName, which would hide the native contract.

**Verification:** Check ref output, one call, empty/unwritten/null/wrong-type responses, Unicode/UNC/trailing separators and original native exceptions. Live-check named, blank and unsaved model states.

<a id="p02-getbaseunit"></a>

### P02 — GetBaseUnit

**Implementation status:** Implemented with shared internal metadata readers, wrapper-backed snapshots, standalone same-root reads and matching XML documentation.

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga7b1d941b62f317a0bdf104d2d4436ae1)

```cpp
VARIANT OpenSTAADUI::GetBaseUnit ( );
```

**Parameters:** None.

**Native return contract (the managed projection is cached and normalized):** Native VARIANT integer: 1 = Imperial/English, 2 = Metric. English base quantities use inches and kip; metric uses metres and kN.

**Implemented managed signature:** `BaseUnitSystem GetBaseUnit()`.

**Implementation:** Return the owning wrapper’s BaseUnitSystem snapshot. Do not dispatch GetBaseUnit again. Document cache freshness and Unknown behavior; callers explicitly refresh wrapper units after a model/unit change. A raw-root-only adapter needs a defined non-wrapper fallback, not silent attachment to another instance.

**Verification:** Verify exact property projection, no additional COM reads, refreshed-unit visibility, independent wrappers and the existing raw-root constructor policy.

<a id="p03-getinputunitforlength"></a>

### P03 — GetInputUnitForLength

**Implementation status:** Implemented with shared internal metadata readers, wrapper-backed snapshots, standalone same-root reads and matching XML documentation.

**Cross-version refinement:** See the [multi-version unit-query design](OpenSTAAD-Root-Multi-Version-Architecture-and-Plan.md#41-input-unit-getters-same-signature-conflicting-documented-semantics) before applying this 2026 return contract to older STAAD versions.

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga8a846a4f4597cbb6cbed46e86442431c)

```cpp
VARIANT OpenSTAADUI::GetInputUnitForLength ( VARIANT FAR & strUnit );
```

| Native parameter | Direction | Type and meaning |
| --- | --- | --- |
| `strUnit` | Output | VARIANT(text) reference holding the native length-unit text (format to be verified). |

**Native return contract (the managed projection is cached and normalized):** Native VARIANT integer: 0 Inch, 1 Feet, 2 Foot, 3 CentiMeter, 4 Meter, 5 MilliMeter, 6 DeciMeter, 7 KiloMeter. Proposed DTO preserves code and label.

**Implemented managed signature:** `LengthInputUnit GetInputUnitForLength()`.

**Implementation:** Return the owning wrapper’s InputLengthUnit snapshot. Reuse its ref-output parsing, feet normalization and explicit fallback; do not create a second unit-query implementation in Root. Document that the value may be a base-system fallback and contains no original output string or native code provenance.

**Verification:** Verify cached projection without native calls, changes after RefreshUnits, feet normalization, 2024 success-flag protection and documented fallback behavior.

<a id="p04-getinputunitforforce"></a>

### P04 — GetInputUnitForForce

**Implementation status:** Implemented with shared internal metadata readers, wrapper-backed snapshots, standalone same-root reads and matching XML documentation.

**Cross-version refinement:** See the [multi-version unit-query design](OpenSTAAD-Root-Multi-Version-Architecture-and-Plan.md#41-input-unit-getters-same-signature-conflicting-documented-semantics) before applying this 2026 return contract to older STAAD versions.

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gabcc3db5863da3b119b2c5bd4a740f0cd)

```cpp
VARIANT OpenSTAADUI::GetInputUnitForForce ( VARIANT FAR & strUnit );
```

| Native parameter | Direction | Type and meaning |
| --- | --- | --- |
| `strUnit` | Output | VARIANT(text) reference holding the native force-unit text (format to be verified). |

**Native return contract (the managed projection is cached and normalized):** Native VARIANT integer: 0 Kilopound, 1 Pound, 2 Kilogram, 3 Metric Ton, 4 Newton, 5 KiloNewton, 6 MegaNewton, 7 DecaNewton.

**Implemented managed signature:** `ForceInputUnit GetInputUnitForForce()`.

**Implementation:** Return the owning wrapper’s InputForceUnit snapshot without another native read. Its source-compatible fallback is Kip for Imperial and kN otherwise; document this and require explicit refresh for fresh model values. No native-unit text or raw return code can be recovered from this enum property.

**Verification:** Verify cached property reuse, no duplicate dispatch, refresh visibility, zero-unit codes on newer versions and 2024 success-flag/fallback behavior.

<a id="p05-isphysicalmodel"></a>

### P05 — IsPhysicalModel

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga95c33fb64a10923e179bf067fd006a7b)

```cpp
VARIANT OpenSTAADUI::IsPhysicalModel ( );
```

**Parameters:** None.

**Return contract:** Native VARIANT integer: 1 = physical model, 0 = otherwise; proposed bool follows only the documented values.

**Proposed managed signature:** `bool IsPhysicalModel()`.

**Implementation:** Make one predicate call without changing model type. Validate the native integer flag instead of Convert.ToBoolean on arbitrary values. A blank/unsupported session behavior is not specified; document the measured result or propagated error after validation rather than assuming false.

**Verification:** Check 0/1, unsupported flags, malformed/null returns and native exceptions. Live-check analytical, physical, and blank models.

<a id="p06-getapplicationversion"></a>

### P06 — GetApplicationVersion

**Implementation status:** Implemented with shared internal metadata readers, wrapper-backed snapshots, standalone same-root reads and matching XML documentation.

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga658ee126cc9aa9c5e35866e35f60260e)

```cpp
VARIANT OpenSTAADUI::GetApplicationVersion ( VARIANT FAR & MajorA , VARIANT FAR & MajorB , VARIANT FAR & Minor , VARIANT FAR & Build );
```

| Native parameter | Direction | Type and meaning |
| --- | --- | --- |
| `MajorA` | Output | VARIANT reference; integer major version. |
| `MajorB` | Output | VARIANT reference; integer update number. |
| `Minor` | Output | VARIANT reference; integer revision number. |
| `Build` | Output | VARIANT reference; integer build number. |

**Native return contract (the managed projection is cached and normalized):** Native VARIANT text containing the complete version, in addition to four numeric reference outputs.

**Implemented managed signature:** `ApplicationVersion GetApplicationVersion()`.

**Implementation:** Return a copy of the owning wrapper’s StaadVersion, which records native STAAD.Pro. Never return wrapper.ApplicationVersion: that is the host/client assembly version. The source wrapper already performs the native version query during initialization/RefreshComObjects. Missing version remains null; no duplicate Root bootstrap COM query is needed.

**Verification:** Verify native versus host distinction, independent version copies, no additional native query, refresh visibility and missing-version handling. The optional capability selector consumes this observed native version, not the NuGet/host version.

<a id="p07-geterrormessage"></a>

### P07 — GetErrorMessage

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gab804cd4718b6a369ce6fb396c2df560d)

```cpp
VARIANT OpenSTAADUI::GetErrorMessage ( );
```

**Parameters:** None.

**Return contract:** Native VARIANT carrying the OpenSTAAD error-message text, including errors such as unavailable licenses or named views.

**Proposed managed signature:** `string GetErrorMessage()`.

**Implementation:** Return the native message without trimming or substituting an exception message. Verify whether empty/null is valid and whether reading clears or changes the native error state. Call only when explicitly requested, on the same bound session; do not automatically overwrite the primary failure while seeking diagnostics.

**Verification:** Check Unicode, multiline/empty text, null/non-string behavior and original exceptions. Live-trigger a harmless known error in a disposable model and check repeated-read/state behavior.

<a id="p08-getprocessid"></a>

### P08 — GetProcessId

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gac7c8010c34e57ab1443b8efacc3235a0)

```cpp
VARIANT OpenSTAADUI::GetProcessId ( );
```

**Parameters:** None.

**Return contract:** Native VARIANT holding the STAAD process ID; proposed unsigned 32-bit managed identifier.

**Proposed managed signature:** `uint GetProcessId()`.

**Implementation:** Extract/reuse the strict numeric conversion already needed by RootWindowControl through a helper, without creating partial adapters. Accept supported integral subtypes only, check positive UInt32 range, and return the ID without creating, launching or owning a Process. A returned PID is a snapshot and may be recycled later.

**Verification:** Check all accepted integer subtypes, zero/negative/overflow/text/bool/fractional/null and native exceptions. Verify the live ID against the bound STAAD window/process without assuming process counts identify COM objects.

<a id="p09-getmainwindowhandle"></a>

### P09 — GetMainWindowHandle

**Implementation status:** Implemented with shared internal metadata readers, wrapper-backed snapshots, standalone same-root reads and matching XML documentation.

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga0c0b0f425cd8588f0df3c40ea08a05da)

```cpp
VARIANT OpenSTAADUI::GetMainWindowHandle ( );
```

**Parameters:** None.

**Native return contract (the managed projection is cached and normalized):** Native VARIANT holding the main HWND. The reference does not declare the exact VARIANT numeric subtype or pointer width.

**Implemented managed signature:** `IntPtr GetMainWindowHandle()`.

**Implementation:** Return the wrapper’s cached StaadApplicationWindowHandle. The default service reads the same root; injected services must target that instance. The source-family rule queries it only for Connect, otherwise zero. This is a non-owning cached snapshot, not IsWindow/readiness verification. Existing window mutations keep their fresh native handle/PID checks.

**Verification:** Verify bound-instance projection, no extra COM/window/process enumeration, Connect/legacy zero policy, refreshed handles, injected service behavior and existing window-mutation regression coverage.

<a id="p10-getprocesshandle"></a>

### P10 — GetProcessHandle

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga275b4bdab39a0afb0764d4d2e0b2f13e)

```cpp
VARIANT OpenSTAADUI::GetProcessHandle ( );
```

**Parameters:** None.

**Return contract:** Native VARIANT described only as the STAAD process handle. Usability in the client process, ownership and width are not specified.

**Proposed managed signature:** `IntPtr GetProcessHandle()`.

**Implementation:** Gate public implementation on inspecting the typelib and establishing the returned handle's validity context and ownership. IntPtr is a provisional opaque representation, not permission to use or close it. Do not wrap it in an owning SafeHandle, call CloseHandle, or assume it is interchangeable with a PID. If client usability cannot be established, defer this method or expose an explicitly opaque value with that limitation.

**Verification:** Check integral/pointer marshaling and architecture boundaries with stand-ins. In controlled native validation establish what the value represents, when it expires and whether any supported client use exists. Do not add tests that close an unowned handle.

<a id="p11-isanalyzing"></a>

### P11 — IsAnalyzing

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga47cec0d70a609f2de2dd2e5214b7fa28)

```cpp
VARIANT OpenSTAADUI::IsAnalyzing ( );
```

**Parameters:** None.

**Return contract:** Native VARIANT integer: 1 means analysis is running, 0 means it is not. A zero result does not mean analysis succeeded.

**Proposed managed signature:** `bool IsAnalyzing()`.

**Implementation:** Issue a single strict flag query. Keep this separate from analysis status and result availability. Do not start an analysis or add background polling implicitly; any future polling helper needs its own timeout, cancellation and COM-thread contract.

**Verification:** Check 0/1, malformed/unknown flags and exceptions. Live-observe before, during and after analysis, including completion with errors and a blank model.

<a id="p12-getanalysisstatus"></a>

### P12 — GetAnalysisStatus

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga731f741a97cb618d7409544055b8a2bb)

```cpp
long OpenSTAADUI::GetAnalysisStatus ( LPCTSTR szModelNameWithPath , long FAR * NoOfWarnings , long FAR * NoOfErrors , double FAR * CPUTime );
```

| Native parameter | Direction | Type and meaning |
| --- | --- | --- |
| `szModelNameWithPath / szPathFileName` | Input | LPCTSTR model filename with path; managed string, validated and normalized before dispatch. |
| `NoOfWarnings` | Output | long pointer; 32-bit integer warning count. |
| `NoOfErrors` | Output | long pointer; 32-bit integer error count. |
| `CPUTime` | Output | double pointer; analysis CPU time in seconds. |

**Return contract:** Native long status: -2 invalid path; -1 terminated; 0 general error; 1 running; 2 completed cleanly; 3 completed with warnings; 4 completed with errors; 5 not performed.

**Proposed managed signature:** `AnalysisStatusResult GetAnalysisStatus(string modelFilePath)`.

**Implementation:** Use verified ref Int32 counts and ref double time in one call; do not pass ref Int64 because a C# example used long. Preserve NativeStatus, including -2 and unknown future values. Existing AnalysisResult lacks -2, so avoid unchecked assumptions based on that enum alone. Validate nonnegative counts and finite nonnegative CPU time only where the server contract says outputs are valid; otherwise retain null/unavailable fields. This query can target a named file, not only the bound open model.

**Verification:** Check every status, invalid path before dispatch, exact ref types/order, missing/negative counts, invalid time and exceptions. Live-check unanalysed/running/clean/warning/error/terminated models and a nonexistent path; determine output validity for each status.

<a id="p13-getanalysiserrormessages"></a>

### P13 — GetAnalysisErrorMessages

**Cross-version refinement:** This operation is proposed on a separately acquired [capability interface](OpenSTAAD-Root-Multi-Version-Architecture-and-Plan.md#7-proposed-api-access-and-intellisense), subject to the acquired version and verified native support; it is not proposed as a universally available IOSRoot member.

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga405205f5768fd9b1d16b8a35b72e86f4)

```cpp
long OpenSTAADUI::GetAnalysisErrorMessages ( LPCTSTR szModelNameWithPath , VARIANT FAR & ErrorMessages );
```

| Native parameter | Direction | Type and meaning |
| --- | --- | --- |
| `szModelNameWithPath` | Input | LPCTSTR full model filename; managed validated path string. |
| `ErrorMessages` | Output buffer / ref | Preallocated VARIANT containing a BSTR string array; capacity at least the error count from GetAnalysisStatus. |

**Return contract:** Native long status: 1 messages retrieved; 0 no messages; -1 general error; -2 invalid path; -107 not an array; -119 wrong array type; -108 insufficient capacity; -1001 log missing; -10004 analysis running.

**Proposed managed signature:** `AnalysisMessagesResult GetAnalysisErrorMessages(string modelFilePath)`.

**Implementation:** Obtain the required count through P12 and allocate the native BSTR array internally. Validate the path and preserve native retrieval status in AnalysisMessagesResult alongside a recognized status, IsSuccessful and materialized read-only Messages. Native status 0 is successful absence, distinct from an expected failure. Verify internal ref string[] versus ref object dispatch and SAFEARRAY shape; validate contents before publishing a successful result. Count/read races permit only a documented bounded requery after -108, with a bounded allocation policy and explicit failure when unresolved. Callers never allocate an output buffer. No automatic analysis, file deletion or invented success status.

**Verification:** Check BSTR rather than object/numeric arrays; zero/exact/oversized/undersized capacities, overwritten arrays, all listed statuses, unknown codes and COM failure. Live-check log source, Unicode/multiline text and count changes. The native failures caught by managed validation should be tested at the lower dispatch boundary where applicable.

<a id="p14-getanalysiswarningmessages"></a>

### P14 — GetAnalysisWarningMessages

**Cross-version refinement:** This operation is proposed on a separately acquired [capability interface](OpenSTAAD-Root-Multi-Version-Architecture-and-Plan.md#7-proposed-api-access-and-intellisense), subject to the acquired version and verified native support; it is not proposed as a universally available IOSRoot member.

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gae891bbde4b6fd5e7f3e56dbf0644386f)

```cpp
long OpenSTAADUI::GetAnalysisWarningMessages ( LPCTSTR szModelNameWithPath , VARIANT FAR & WarningMessages );
```

| Native parameter | Direction | Type and meaning |
| --- | --- | --- |
| `szModelNameWithPath` | Input | LPCTSTR full model filename; managed validated path string. |
| `WarningMessages` | Output buffer / ref | Preallocated VARIANT containing a BSTR string array; capacity at least WarningCount from GetAnalysisStatus. |

**Return contract:** Native long status: 1 messages retrieved; 0 no messages; -1 general error; -2 invalid path; -107 not an array; -119 wrong array type; -108 insufficient capacity; -1001 ANL missing; -10004 analysis running.

**Proposed managed signature:** `AnalysisMessagesResult GetAnalysisWarningMessages(string modelFilePath)`.

**Implementation:** Use the same validated marshaling/status helper as P13 while retaining the distinct native method and warning buffer. The page describes warning retrieval from the ANL file; do not substitute error-log parsing. Allocate buffers internally and return the same named result contract as P13. Count/read races use the documented bounded internal requery policy, not indefinite allocation growth. Preserve native text and status.

**Verification:** Check all documented and unknown statuses, ref BSTR array shape, empty/exact/oversized/undersized arrays, missing ANL, malformed successful output and native failures. Live-test actual warnings and an in-progress analysis.

<a id="p15-setinputunitforlength"></a>

### P15 — SetInputUnitForLength

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga6bc9354604b80ca54912ec586c4ff931)

```cpp
void OpenSTAADUI::SetInputUnitForLength ( const VARIANT FAR & iUnit );
```

| Native parameter | Direction | Type and meaning |
| --- | --- | --- |
| `iUnit` | Input | const VARIANT reference carrying a length integer code 0–7; both 1 and 2 are documented feet aliases. |

**Return contract:** Native void; managed completion means only that the unit-change request returned.

**Proposed managed signature:** `void SetInputUnitForLength(LengthInputUnit unit)`.

**Implementation:** Validate the documented numeric domain before dispatch, including a deliberate policy for unnamed enum code 1. Proposed new-method policy is to accept documented code 1 even though Enum.IsDefined currently rejects it; retain the old SetInputUnitsEx behavior until separately authorized. Send one integer code. Do not change force units or infer persistence, coordinate rescaling or base-unit changes.

**Verification:** Check all codes 0–7 including cast code 1, invalid enums and no native call on invalid input; one call, exact type, exceptions and force-unit preservation. Live-read units before/after in a disposable model.

<a id="p16-setinputunitforforce"></a>

### P16 — SetInputUnitForForce

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga0d92e92cebdffc1dbf951d07f8d29f1e)

```cpp
void OpenSTAADUI::SetInputUnitForForce ( const VARIANT FAR & iUnit );
```

| Native parameter | Direction | Type and meaning |
| --- | --- | --- |
| `iUnit` | Input | const VARIANT reference carrying force code 0–7: Kip, Pound, Kilogram, Metric Ton, Newton, kN, MN, DecaNewton. |

**Return contract:** Native void; no returned success/status value.

**Proposed managed signature:** `void SetInputUnitForForce(ForceInputUnit unit)`.

**Implementation:** Validate ForceInputUnit before one integer dispatch. Do not route through the two-unit setter and accidentally read/change length units. State the effect and persistence limitations in XML, including that this is an explicit native mutation.

**Verification:** Check all eight values, invalid casts rejected before dispatch, exact integer argument, one-call behavior and exception propagation. Live-check that length and base-system values remain as documented.

<a id="p17-newstaadfile"></a>

### P17 — NewSTAADFile

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gacdd64b11752dc29f03537526d46553b4)

```cpp
void OpenSTAADUI::NewSTAADFile ( const VARIANT FAR & bstrFileName , const VARIANT FAR & nLenUnitInput , const VARIANT FAR & nForceUnitInput );
```

| Native parameter | Direction | Type and meaning |
| --- | --- | --- |
| `bstrFileName` | Input | const VARIANT(text) reference identifying the .STD file to create. |
| `nLenUnitInput` | Input | const VARIANT reference carrying length code 0–7, including the feet aliases 1/2. |
| `nForceUnitInput` | Input | const VARIANT reference carrying force code 0–7. |

**Return contract:** Native void. The page promises file creation with selected input units; it does not specify completion/readiness, overwrite behavior or what happens to an existing model.

**Proposed managed signature:** `void NewSTAADFile(string filePath, LengthInputUnit lengthUnit, ForceInputUnit forceUnit)`.

**Implementation:** Validate/normalize target path and both unit codes before any mutation. Establish overwrite and loaded-model behavior first using disposable files. Proposed default policy rejects an existing target and a nonblank bound session, without claiming that prechecks eliminate races. Do not fabricate an empty STD file, launch another process, close an unrelated model, overwrite files, or retry uncertain creation. If strict no-overwrite cannot be guaranteed through this native API, document the boundary and require a separately designed safe creation workflow rather than claiming atomic safety.

**Verification:** Check argument order/types, all units including length code 1, existing file/directory, invalid/UNC/Unicode paths and validation before dispatch. Live-check file contents, requested units, active-model identity, startup/blank states, dialogs, read-only/inaccessible destinations and concurrent target creation.

<a id="p18-setshortjobinfo"></a>

### P18 — SetShortJobInfo

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga04af5d1d0aa713ba08032cf613aa2193)

```cpp
void OpenSTAADUI::SetShortJobInfo ( const VARIANT FAR & jobName , const VARIANT FAR & jobClient , const VARIANT FAR & enggName );
```

| Native parameter | Direction | Type and meaning |
| --- | --- | --- |
| `jobName` | Input | const VARIANT(text) reference; job name. |
| `jobClient` | Input | const VARIANT(text) reference; client. |
| `enggName` | Input | const VARIANT(text) reference; originating engineer. |

**Return contract:** Native void; setter completion does not imply the model has been saved.

**Proposed managed signature:** `void SetShortJobInfo(string jobName, string jobClient, string engineerName)`.

**Implementation:** Validate all three strings before dispatch; proposed policy rejects null and permits empty text to explicitly clear a field. Preserve Unicode, spaces and line breaks where native supports them. Send exactly three strings in the native order; do not call SaveModel implicitly. A later ShortJobInfo convenience overload may snapshot its three mutable properties first.

**Verification:** Check each position, null rejection before any native call, empty/Unicode/whitespace text and exception propagation. Live-read back through GetShortJobInfo and verify save/reopen separately.

<a id="p19-setfulljobinfo"></a>

### P19 — SetFullJobInfo

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga519b55d96da3c901a6b4149a77480c9b)

```cpp
void OpenSTAADUI::SetFullJobInfo ( const VARIANT FAR & jobName , const VARIANT FAR & jobClient , const VARIANT FAR & enggName , const VARIANT FAR & eDate , const VARIANT FAR & jobNumber , const VARIANT FAR & revision , const VARIANT FAR & part , const VARIANT FAR & reference , const VARIANT FAR & checkerName , const VARIANT FAR & cDate , const VARIANT FAR & approverName , const VARIANT FAR & aDate , const VARIANT FAR & comments );
```

| Native parameter | Direction | Type and meaning |
| --- | --- | --- |
| `jobName` | Input | const VARIANT(text); job name. |
| `jobClient` | Input | const VARIANT(text); client. |
| `enggName` | Input | const VARIANT(text); engineer/originator name. |
| `eDate` | Input | const VARIANT(text); engineer date text, not a native DATE. |
| `jobNumber` | Input | const VARIANT(text); job number. |
| `revision` | Input | const VARIANT(text); revision. |
| `part` | Input | const VARIANT(text); job part. |
| `reference` | Input | const VARIANT(text); job reference. |
| `checkerName` | Input | const VARIANT(text); checker name. |
| `cDate` | Input | const VARIANT(text); checker date text. |
| `approverName` | Input | const VARIANT(text); approver name. |
| `aDate` | Input | const VARIANT(text); approver date text. |
| `comments` | Input | const VARIANT(text); comments. |

**Return contract:** Native void. All thirteen inputs are const VARIANT references carrying strings, including the three dates.

**Proposed managed signature:** `void SetFullJobInfo(string jobName, string jobClient, string engineerName, string engineerDate, string jobNumber, string revision, string part, string reference, string checkerName, string checkerDate, string approverName, string approverDate, string comments)`.

**Implementation:** Validate and snapshot all thirteen inputs before one dispatch. Proposed policy rejects null, allows explicit empty text and preserves supplied date strings without culture-dependent formatting. Parameter order must follow this table, not FullJobInfo.From, whose parameter order is different. The public engineerName/engineerDate aliases map to native enggName/eDate. Do not add a DateTime-based DTO overload until accepted native date formats and unspecified-date behavior are verified. No implicit save or rollback promise.

**Verification:** Use thirteen distinct marker strings to prove order, including all three dates; reject each invalid/null input before dispatch. Test empty dates, Unicode/multiline comments and exceptions. Live-roundtrip valid dates under multiple cultures; inspect raw native fields when the existing date-parsing getter cannot preserve their original text.

<a id="p20-updatestructure"></a>

### P20 — UpdateStructure

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gaaf431f1bfbee142cd7d55d454d56d995)

```cpp
void OpenSTAADUI::UpdateStructure ( );
```

**Parameters:** None.

**Return contract:** Native void; the page states that it updates the current structure without specifying rebuild, refresh or persistence semantics.

**Proposed managed signature:** `void UpdateStructure()`.

**Implementation:** Issue one explicit native request. Do not label it an analysis, save, geometry refresh guarantee or transaction commit without evidence. Establish what becomes visible and when using a small disposable geometry edit before finalizing XML remarks.

**Verification:** Check one no-argument call, exception propagation and no accidental save/analysis. Live-check representative edits, blank sessions and analysis-in-progress behavior; document measured limits.

<a id="p21-analyze"></a>

### P21 — Analyze

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga646f2f0b6983135b4b3a8ded877475d7)

```cpp
void OpenSTAADUI::Analyze ( );
```

**Parameters:** None.

**Return contract:** Native void; analyzes the currently open .STD model, corresponding to the UI analysis operation. No status is returned by this method.

**Proposed managed signature:** `void Analyze()`.

**Implementation:** Call native Analyze once rather than silently routing to AnalyzeEx with guessed defaults. Do not assume blocking, result availability or clean completion. Document interaction with application silent mode and link to AnalyzeEx/IsAnalyzing/GetAnalysisStatus for callers needing control and observation. Never start analysis from a query or retry a request of uncertain completion.

**Verification:** Check one no-argument native call and exception propagation. Live-check UI/dialog behavior, observed return timing, failed analysis, cancellation and no-open-model behavior using disposable models.

<a id="p22-analyzemodel"></a>

### P22 — AnalyzeModel

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gadaa1e36df7e6b4fe5c056ede8270987c)

```cpp
void OpenSTAADUI::AnalyzeModel ( const VARIANT FAR & varEngine );
```

| Native parameter | Direction | Type and meaning |
| --- | --- | --- |
| `varEngine` | Input | const VARIANT reference; the only documented engine value is -1, launching the STAAD.Pro engine. |

**Return contract:** Native declaration is void despite an inconsistent C# example assigning a result. Proposed managed return is void until typelib/live evidence says otherwise.

**Proposed managed signature:** `void AnalyzeModel(int engine = -1)`.

**Implementation:** Validate engine == -1 before a single integer dispatch. Keep the optional managed default explicit in XML; it is a proposal, not evidence of a native optional argument. Do not invent alternate engine IDs or equate this operation's behavior to AnalyzeEx. Resolve the signature discrepancy before release.

**Verification:** Check accepted -1, invalid 0/positive/other-negative inputs rejected before dispatch, argument type/order and native failures. Live-confirm return contract, dialogs and behavior on valid, invalid and blank models.

<a id="p23-getconnectedprojectinfo"></a>

### P23 — GetCONNECTEDProjectInfo

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gab92e524c05abdf5e59e54adc95e0c9b0)

```cpp
VARIANT OpenSTAADUI::GetCONNECTEDProjectInfo ( VARIANT FAR & szProjID , VARIANT FAR & szName );
```

| Native parameter | Direction | Type and meaning |
| --- | --- | --- |
| `szProjID` | Output | VARIANT(text) reference; CONNECTED project ID. |
| `szName` | Output | VARIANT(text) reference; CONNECTED project name. |

**Return contract:** Native VARIANT 1 = true, 0 = false. The result class retains the verified flag and exposes project data only on native success.

**Proposed managed signature:** `ConnectedProjectResult GetConnectedProjectInfo()`.

**Implementation:** Initialize reference string outputs, call once, and strictly validate the status. On true, require valid string outputs before returning a DTO; on false, do not present partial/unwritten text as valid project information. Preserve ID and name as supplied; do not force GUID formatting without evidence. Native false is not the same as a COM exception. No account login, assignment or network retry is implied.

**Verification:** Check true/false/unknown/malformed flags, correct output order, unwritten outputs on success, false with partial outputs, Unicode and exceptions. Live-check assigned/unassigned projects and signed-out/offline conditions without modifying account settings.

<a id="p24-setconnectedprojectinfo"></a>

### P24 — SetCONNECTEDProjectInfo

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga61d5d3fc321d74be1ede4c318426b9f6)

```cpp
VARIANT OpenSTAADUI::SetCONNECTEDProjectInfo ( const VARIANT FAR & szProjID , const VARIANT FAR & szName );
```

| Native parameter | Direction | Type and meaning |
| --- | --- | --- |
| `szProjID` | Input | const VARIANT(text) reference; CONNECTED project ID. |
| `szName` | Input | const VARIANT(text) reference; CONNECTED project name. |

**Return contract:** Native VARIANT 1 = true, 0 = false; proposed bool reports that documented native result, not an independently verified cloud synchronization outcome.

**Proposed managed signature:** `bool SetConnectedProjectInfo(string projectId, string projectName)`.

**Implementation:** Validate both strings and native flag shape. Proposed policy rejects null; determine whether empty values clear the association before permitting/documenting that operation. Keep ID as text, do not claim the API creates a project or updates cloud membership, and do not silently save the model. Document credentials/network dependencies established by native validation.

**Verification:** Check argument order, null rejection, empty-value policy, true/false/invalid flags and exceptions. Live-roundtrip a test association and restore it with explicit test setup/teardown; test unavailable project and offline behavior.

<a id="p25-createnamedview"></a>

### P25 — CreateNamedView

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gaf331e2b24c0e4f4e57c2951594423b2d)

```cpp
void OpenSTAADUI::CreateNamedView ( const VARIANT FAR & strName , const VARIANT FAR & nFlag , VARIANT FAR & nError );
```

| Native parameter | Direction | Type and meaning |
| --- | --- | --- |
| `strName` | Input | const VARIANT(text) reference; view name. |
| `nFlag` | Input | const VARIANT reference; integer creation flag. Allowed values and meanings are not provided. |
| `nError` | Output, inferred | VARIANT reference; integer error code. Prose marks input, but says the method writes the error number. |

**Return contract:** Native void; the managed result retains the verified nError output, not a fabricated native return value. No success/error-code meanings are documented on this page.

**Proposed managed signature:** `NamedViewResult CreateNamedView(string viewName, int creationFlag)`.

**Implementation:** Resolve creationFlag domain, nError direction and code meanings before implementing/releasing. Validate the view name and established flag range, then issue one call with a numeric ref output. Keep the raw output code visible, including unknown values; do not assume zero success or overwrite behavior. Replace int creationFlag with an enum only after authoritative values are established.

**Verification:** Check name/flag validation, exact argument order and ref output, missing/wrong-type outputs and native exceptions. Live-check new/duplicate/empty/Unicode names and each verified flag in a disposable model.

<a id="p26-savenamedview"></a>

### P26 — SaveNamedView

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gaeca040a52040da606a81af5407aa464e)

```cpp
void OpenSTAADUI::SaveNamedView ( const VARIANT FAR & strName , VARIANT FAR & nError );
```

| Native parameter | Direction | Type and meaning |
| --- | --- | --- |
| `strName` | Input | const VARIANT(text) reference; name under which to save the current view. |
| `nError` | Output, inferred | VARIANT reference carrying the native integer error result; direction requires verification. |

**Return contract:** Native void; the proposed NamedViewResult retains nError; status/success interpretation remains gated on verified meanings.

**Proposed managed signature:** `NamedViewResult SaveNamedView(string viewName)`.

**Implementation:** Validate the name, call the distinct SaveNamedView API and read the verified error output. Establish whether the view must already exist and what is stored. Do not silently substitute CreateNamedView, overwrite an unrelated view or interpret an undocumented code as success.

**Verification:** Check output/reference behavior, malformed outputs, argument validation and exceptions. Live-check existing/missing/duplicate names, stored view contents and persistence after save/reopen.

<a id="p27-removenamedview"></a>

### P27 — RemoveNamedView

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gacd98d0159150ffada6b7e62f2e1687d4)

```cpp
void OpenSTAADUI::RemoveNamedView ( const VARIANT FAR & strName , VARIANT FAR & nError );
```

| Native parameter | Direction | Type and meaning |
| --- | --- | --- |
| `strName` | Input | const VARIANT(text) reference; name of the view to remove. |
| `nError` | Output, inferred | VARIANT reference carrying the native integer error result; documentation direction is inconsistent. |

**Return contract:** Native void; the proposed NamedViewResult retains the observed nError output with verified status interpretation.

**Proposed managed signature:** `NamedViewResult RemoveNamedView(string viewName)`.

**Implementation:** Validate the name and request removal once. Establish code meanings and missing-name behavior; preserve failure codes. Do not delete model entities, retry a response-lost removal automatically, or claim that the view is removed from the file before persistence is verified.

**Verification:** Check reference output, malformed/error responses, one-call behavior and exceptions. Live-check existing and missing views, a currently displayed view, Unicode names, and absence of geometry deletion.

<a id="p28-modifynamedview"></a>

### P28 — ModifyNamedView

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gadfb2385ac03c7bf740e24eb24fcca286)

```cpp
void OpenSTAADUI::ModifyNamedView ( const VARIANT FAR & strName , const VARIANT FAR & nEntities , const VARIANT FAR & EntityArray , const VARIANT FAR & nArrayQualifier , const VARIANT FAR & nModifyFlag , VARIANT FAR & nError );
```

| Native parameter | Direction | Type and meaning |
| --- | --- | --- |
| `strName` | Input | const VARIANT(text) reference; view name. |
| `nEntities` | Input | const VARIANT integer; number of entity IDs, derived from the captured managed array. |
| `EntityArray` | Input, to verify | const VARIANT; proposed one-dimensional Int32 SAFEARRAY of entity IDs. The prose incorrectly suggests a single long, so actual shape/bounds must be established. |
| `nArrayQualifier` | Input | const VARIANT integer: 0 Node, 1 Beam, 2 Plate, 3 Solid, 4 Surface. |
| `nModifyFlag` | Input | const VARIANT integer; modification mode. Values and meanings are not documented. |
| `nError` | Output, inferred | VARIANT reference; native integer error result. |

**Return contract:** Native void; the proposed NamedViewResult retains nError. The operation's modification flags and error-code semantics are unresolved.

**Proposed managed signature:** `NamedViewResult ModifyNamedView(string viewName, IEnumerable<int> entityIds, NamedViewEntityType entityType, int modificationFlag)`.

**Implementation:** Gate on verified array and flag contracts. Snapshot entityIds once into a fixed int[] and derive nEntities from that snapshot; do not accept a separately inconsistent count. Validate qualifier 0–4 and entity IDs under the verified native numbering rules. Decide empty-array/duplicate behavior from evidence rather than pruning or inventing semantics. Send all six native arguments in exact order and preserve error code.

**Verification:** Check all five qualifiers, array subtype/rank/bounds, count consistency under mutable input, ID validation, empty/duplicate inputs, flags and ref error output. Live-check each supported entity class and modification mode; compare before/after view membership without changing geometry.

<a id="p29-exportmodeltoadina"></a>

### P29 — ExportModelToADINA

**Cross-version refinement:** This operation is proposed on a separately acquired [capability interface](OpenSTAAD-Root-Multi-Version-Architecture-and-Plan.md#7-proposed-api-access-and-intellisense), subject to the acquired version and verified native support; it is not proposed as a universally available IOSRoot member.

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#ga212f15a4b5984734985d197b67de4199)

```cpp
long OpenSTAADUI::ExportModelToADINA ( int flagAxisSystem , int flagLCHandler );
```

| Native parameter | Direction | Type and meaning |
| --- | --- | --- |
| `flagAxisSystem` | Input | Native int: 0 keep the axis convention, 1 Y-Up, 2 Z-Up. |
| `flagLCHandler` | Input | Native int: 0 multiple load cases, 1 time-history values. |

**Return contract:** Native long status: 0 successful export; -14501 successful with warnings; -14502 failed; -101 no open model. AdinaExportResult retains every raw status, a nullable recognized status and IsSuccessful including warning success.

**Proposed managed signature:** `AdinaExportResult ExportModelToAdina(AdinaAxisSystem axisSystem, AdinaLoadCaseHandling loadCaseHandling)`.

**Implementation:** Validate the two enum domains and call once. Document that it writes an ADINA .in file and a .ADINA.log companion. The page does not define destination selection or overwrite rules: establish both in a disposable directory before release. Do not add an output-path argument that the native signature does not accept, hide warning success behind a generic bool, or infer overall success from call completion.

**Verification:** Check all six valid argument combinations, invalid casts rejected before dispatch, exact integer args, every documented/unknown status and exceptions. Live-check output locations, file contents, logs, warnings, existing outputs, inaccessible folders and no-model state.

<a id="p30-quit"></a>

### P30 — Quit

[Native reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___root.html#gadd624b0b5b4f45cf4ed97f9557cc88af)

```cpp
void OpenSTAADUI::Quit ( );
```

**Parameters:** None.

**Return contract:** Native void; closes the STAAD.Pro application environment. No completion or save-status value is returned.

**Proposed managed signature:** `void Quit()`.

**Implementation:** Expose only an explicit destructive application request, not implicit disposal behavior. Document potential impact on unsaved work and that the bound session can become unusable after dispatch. Do not change OpenStaadWrapper.Dispose, kill processes, auto-save, auto-retry, or close any other STAAD instance. Native prompt/analysis-running behavior must be verified; the method cannot promise bounded termination.

**Verification:** Check one no-argument call and exception propagation offline. Native-test last, in a disposable dedicated STAAD instance with no user work: blank/saved/unsaved cases, analysis-running behavior, prompts, correct-instance targeting and subsequent connection failure.

## 8. Recommended implementation sequence

Do not start by copying all missing methods mechanically. First settle the contract gaps; then implement queries before mutations, and leave application shutdown until the end. The order below covers **all 30** missing dedicated adapter methods exactly once. A blocked method remains deferred; the sequence does not justify guessing an undocumented behavior.

| Stage | Functions, in recommended order | Dependencies and completion gate |
| --- | --- | --- |
| 0 — Contract baseline | No new API yet. Inspect installed typelib/dispatch metadata and build controlled native fixtures. | Resolve ref argument types, integer widths, unit alias policy, status handling, named-view flags/error outputs/arrays, handle ownership and file/export overwrite behavior. Record discrepancies against the 2026 help. Preserve existing public behavior. |
| 1 — Model/application queries | P01 GetSTAADFileFolder; P02 GetBaseUnit; P03 GetInputUnitForLength; P04 GetInputUnitForForce; P05 IsPhysicalModel; P06 GetApplicationVersion; P07 GetErrorMessage; P08 GetProcessId; P09 GetMainWindowHandle. | Implement reusable strict string/integer/flag/pointer conversion helpers and query DTOs. Exercise named/blank models and independent sessions. Handle GetErrorMessage state behavior explicitly. |
| 2 — Analysis observation | P11 IsAnalyzing; P12 GetAnalysisStatus; P13 GetAnalysisErrorMessages; P14 GetAnalysisWarningMessages. | P12 precedes message allocation. Establish per-status output validity, BSTR SAFEARRAY behavior and bounded handling of count/read races. Queries must not start analysis. |
| 3 — Unit and model-information mutations | P15 SetInputUnitForLength; P16 SetInputUnitForForce; P18 SetShortJobInfo; P19 SetFullJobInfo; P20 UpdateStructure. | Use stage-1 readers for verification. Validate complete arguments before mutation. Establish date-text handling and update semantics; verify persistence separately from native completion. |
| 4 — Creation and analysis requests | P17 NewSTAADFile; P21 Analyze; P22 AnalyzeModel. | Creation needs stage-0 overwrite/current-model decisions and stage-1 identity/unit checks. Analysis requests use stage-2 observation for test verification, not implicit retry. Resolve AnalyzeModel void/example inconsistency. |
| 5 — Project integration and export | P23 GetCONNECTEDProjectInfo; P24 SetCONNECTEDProjectInfo; P29 ExportModelToADINA. | Getter before setter; use explicit disposable project/model fixtures. Resolve export destinations and overwrite behavior. Preserve return codes and model-only versus cloud effects. |
| 6 — Named views | P25 CreateNamedView; P26 SaveNamedView; P28 ModifyNamedView; P27 RemoveNamedView. | Creation/modification flags, error directions/codes and entity array shape must be established before this stage. Creation before modification/removal provides test fixtures; cover every supported entity qualifier. |
| 7 — Opaque native handles and shutdown | P10 GetProcessHandle; P30 Quit. | Handle API stays deferred if client validity/ownership cannot be established. Test shutdown only in a disposable dedicated process, after all non-destructive validation. Never alter disposal behavior. |

Five metadata methods P02/P03/P04/P06/P09 are implemented with public contracts, XML and tests. P08 still needs a dedicated process-ID query over its existing internal native support. The remaining plan follows the managed result-object design rules.

Optional follow-up work on already covered native functions is separate: expose the native filename flag if needed, expose previous silent mode under a new name if needed, and decide how to extend existing unit setters without changing established behavior unexpectedly. None of these changes is authorized by this review alone.

## 9. Test and documentation requirements for each implementation

### Offline contract tests

For every new method:

1. Verify the exact native function, argument count, argument order and managed/COM value shape. Use reference-output stand-ins for reference outputs; use the reference-output shape now implemented by the wrapper; Root projection tests must additionally prove that they do not repeat the native read.
2. Verify null/empty/range/enum/count/path validation and that validation happens before any native mutation. For file/status queries, do not replace native missing-file statuses with an unrelated filesystem precheck.
3. Verify valid, malformed, null and unwritten native results as relevant. Separate native integer failure codes from COM exceptions, binder failures and wrapper validation errors. Preserve unknown status integers where the proposed contract allows them.
4. Verify one-call behavior for mutations, session isolation and no unintended save/open/close/analysis. Test helpers through meaningful public behavior rather than duplicating implementation details.
5. For outputs, assert complete mapping before returning a DTO. Preserve native text, Unicode and line breaks; do not leak partially populated success objects.
6. For collection outputs/inputs, test element type, rank, lower bounds where relevant, empty values, capacity/count consistency and caller mutations during snapshotting. Check unsupported array forms at the dispatch helper boundary if the public managed type cannot express them.
7. For uncertain outcomes, verify that the wrapper does not retry a creation, analysis, export, removal or shutdown mutation automatically.

Add focused tests to the existing root test area, for example `OSRootUnitTests`, `OSRootApplicationInfoTests`, `OSRootAnalysisStatusTests`, `OSRootAnalysisMessageTests`, `OSRootJobInfoSetterTests`, `OSRootNamedViewTests` and `OSRootExportTests`. These are proposed test files, not files created by this review. Keep shared stand-ins faithful to the verified native contract.

### Controlled native integration tests

Offline stand-ins cannot prove COM compatibility. Run opt-in Windows tests against the licensed supported STAAD build, with explicit disposable fixtures and recorded version/bitness:

| Fixture / condition | Relevant coverage |
| --- | --- |
| Application startup with no named model; blank/unsaved/named model | Folder, units, model type, process/window identity and no-model failures. Do not assume all startup states expose the same COM interfaces. |
| Two STAAD processes with distinct named models and, where reachable, blank instances | Correct bound root; no accidental cross-session mutations. These methods do not solve arbitrary blank-instance discovery/provider binding. |
| Imperial and metric model base systems; every input length/force code | Getter/setter codes, native unit text, feet alias and preservation of unrelated settings. |
| Clean, warning, error, terminated, unanalysed and in-progress analysis | Status/count/time validity, messages, array capacities, file availability and return timing. |
| Unicode/spaced/UNC paths; nonexistent/read-only/inaccessible/existing targets | Native path handling, creation/export behavior, no accidental overwrites and original failure information. |
| Thirteen distinguishable job fields, empty dates and supported locale formats | Native argument order, lossless string setter, current getter parsing limitations and save/reopen persistence. |
| Named views for nodes, beams, plates, solids and surfaces | Verified flags, error codes, entity arrays, creation/save/modify/remove behavior and geometry preservation. |
| Test CONNECTED project assignment plus unassigned/offline cases | Getter false versus exceptions; setter outcomes and association readback. |
| Disposable export directory and disposable dedicated STAAD process | ADINA artifacts/statuses, handle observations and isolated shutdown. |

Native tests that create files, modify metadata/views/project association, analyze, export or quit must never use a user's active work model. Their setup, affected files and cleanup policy must be explicit. A native COM call can block beyond a managed polling deadline; document that limit rather than claiming a timeout makes it interruptible.

### XML IntelliSense acceptance criteria

Each new member must carry matching, substantive documentation on **both** `IOSRoot` and `OSRootAdapter`:

- A clear summary and every managed parameter's meaning, units, allowed values and null/empty rules.
- Native function mapping, output directions, status-code meanings and the difference between native status, output error code and call completion.
- Exact return/DTO semantics, including native false, unknown status, unavailable outputs and error preservation.
- Mutation, persistence, model/session, COM-thread, blocking and external-race limitations.
- Validation and native/binder exceptions that callers can actually encounter; no blanket claim of safety or readiness.
- At least one usable example with required imports, a connected root, valid inputs and appropriate status handling. Mutating examples must make their effects explicit.
- Cross-links to complementary functions such as status-before-message allocation and setters followed by readback/save where appropriate.

DTO properties and new enums also need complete XML. Compile examples against the installed package rather than assuming source snippets are sufficient. Update package verification to check the new public signatures, interface/concrete XML and examples; the current verifier's 22-method expectation must be revised only when actual implementation is added.

## 10. Release readiness after the planned work

This review does not certify the missing methods or a future release. Before publishing a version containing them:

1. Complete each method's verified native contract, implementation, focused offline tests, XML and controlled native checks; record unsupported/unresolved methods as deferred instead of claiming full Root coverage.
2. Check public API compatibility, especially the added interface members, new DTOs/enums and any deliberately separate extensions to existing methods.
3. Run the relevant root tests and the repository's required build/package validation. Expand validation when a helper affects existing behavior; do not treat previously passing tests as coverage of new functions.
4. Update API reference, this coverage report and root-porting documentation to distinguish private-project parity from Bentley API coverage. Record exact supported STAAD versions and known limitations.
5. For every new NuGet build, update the README and release notes in professional language, as required by the project's publication policy. Build the intended package version and verify its DLL, XML, documentation and metadata from the archive.
6. Validate the package in a clean net481 consumer. Do not advertise .NET Core/.NET 8 support from this branch unless a separately authorized target/runtime implementation has actually been built and verified.

The wrapper foundation and its documented placeholders are implemented and tested. No pending Root method, optional capability or version-dispatch engine has been implemented. This report remains the Root function plan for approval.
