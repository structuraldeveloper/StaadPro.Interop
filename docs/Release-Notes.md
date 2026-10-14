# StaadPro.Interop release notes

## 1.0.0-preview.20261014.7 - 14 October 2026 (preview)

This maintenance release improves the package metadata and documentation. The runtime API is unchanged from `1.0.0-preview.20261014.6`.

- Set the package description to **A strongly-typed .NET Framework wrapper for Bentley STAAD.Pro OpenSTAAD**.
- Reorganized the package README into a current product overview, installation instructions, API reference links and operating guidance. Removed internal implementation-history headings and corrected the documented preview version.
- Synchronized the package release notes, README and publishing instructions. Added version checks during packaging so distribution archives require a matching, reviewed documentation entry; automated CI previews receive their actual version in the packaged documents.

The package includes geometry and load operations, model identity and lifecycle functions, application-window controls, and typed native analysis requests. Expanded XML IntelliSense accompanies the library. The existing implementation passed **914 offline tests**; installed-package verification checks **149 XML example blocks**, including managed runtime checks and guarded window examples.

Targets Windows and .NET Framework 4.8.1 (`net481`). Native save/open/close, analysis-engine behavior and compatibility across STAAD releases require further validation before a stable release. This entry describes the prepared archive; public availability depends on NuGet.org acceptance and validation.

## 1.0.0-preview.20261014.6 - 14 October 2026 (prepared)

Added RestoreStaadMainWindow and AnalyzeEx with matching signatures and complete IntelliSense. Restore validates the reported window/PID before posting command 9. Analysis validates three native flags before one call, preserves documented error/in-progress/completion codes and unknown Int32 codes, and rejects unsupported status types or ranges. Added HiddenMode, WaitAnalysisMode and AnalysisResult; no pre-save, extra silent-mode mutation, result-file retrieval, retry, rollback or managed analysis timeout occurs.

Root coverage is **22 of 23 methods**, with **1 pending**. Custom `IOSRoot` implementations must implement the new members. Validated **914 offline tests** and **149 installed-package XML examples**. Live native mutation/version validation remains pending; no public upload is claimed.

## 1.0.0-preview.20261014.5 - 14 October 2026 (prepared)

Added MinimizeStaadMainWindow and MaximizeStaadMainWindow with matching void signatures and complete IntelliSense. Read supported integral native handle/process values, reject malformed or foreign windows and post the correct Windows state request. Accepted posting does not prove the final state or focus; identity checks are snapshots and handle-recycling races remain possible. No model mutation, process launch, retry or rollback occurs.

Root coverage is **20 of 23 methods**, with **3 pending**. Custom `IOSRoot` implementations must implement the new members. Validated **868 offline tests** and **145 installed-package XML examples**. Live native mutation/version validation remains pending; no public upload is claimed.

## 1.0.0-preview.20261014.4 - 14 October 2026 (prepared)

Added CloseCurrentStaadModel and CloseWriteReopenStaadFile with matching signatures and detailed IntelliSense. Conditional close skips explicit blank filenames and propagates errors. Rewrite validates/snapshots inputs, requires the current target, waits for blank identity and released locks, replaces complete UTF-8 contents atomically and avoids reopening over an intervening model. No save, backup, silent-state restoration, rollback or hard native deadline is supplied; false can follow native/file side effects.

Root coverage is **18 of 23 methods**, with **5 pending**. Custom `IOSRoot` implementations must implement the new members. Validated **841 offline tests** and **141 installed-package XML examples**. Live native mutation/version validation remains pending; no public upload is claimed.

## 1.0.0-preview.20261014.3 - 14 October 2026 (prepared)

Added AwaitModelHandle and OpenStaadModel with matching signatures, explicit duration validation and complete IntelliSense. Poll full normalized paths using a monotonic budget, distinguish matching models from similarly named files, check immediately and cap waits to remaining time. Open requests occur once and count toward the documented seconds limit. Native errors propagate; logical timeouts cannot interrupt synchronous COM calls, and a path match does not certify Geometry/Load readiness.

Root coverage is **16 of 23 methods**, with **7 pending**. Custom `IOSRoot` implementations must implement the new members. Validated **808 offline tests** and **137 installed-package XML examples**. Live native mutation/version validation remains pending; no public upload is claimed.

## 1.0.0-preview.20261014.2 - 14 October 2026 (prepared)

Added OpenSTAADFile and ShowApplication with matching main-project signatures and expanded IntelliSense. Open validates and resolves the requested path before one native model-switch request; show dispatches once without opening a model. Both return the original root after native dispatch and propagate exceptions, without readiness/visibility verification, automatic saves, retries or rollback.

Root coverage is **14 of 23 methods**, with **9 pending**. Custom `IOSRoot` implementations must implement the new members. Validated **774 offline tests** and **133 installed-package XML examples**. Live native mutation/version validation remains pending; no public upload is claimed.

## 1.0.0-preview.20261014.1 - 14 October 2026 (root save/close batch, prepared)

- Added main-project `SetSilentMode(SilentMode)`, `SaveModel(SilentMode)` and `CloseSTAADFile()` in the existing non-partial `IOSRoot`/`OSRootAdapter`, with matching complete XML IntelliSense and executable examples. Added documented `SilentMode.Disable=0` and `Enable=1`; reject undefined values before dispatch and pass native integer codes.
- Preserve `SetSilentMode`'s Boolean call-completion contract: ignore the native previous-state VARIANT, return true on completion and false on dispatch exceptions. Save/close remain one-call void native mutations with fluent adapter returns and unchanged exception propagation; no read-back, automatic save-on-close, rollback, application termination or unattended-operation guarantee.
- Root coverage is **12 of 23 methods**, with **11 pending**. Geometry remains 140 overloads and load remains 78, with 42 pending reference load overloads. Custom `IOSRoot` implementations must implement the three new members and consumers must rebuild.
- Passed **760 offline tests**, including 27 new cases covering enum codes, input validation, previous-state semantics, errors/partial effects, call isolation, fluent identity and matching IntelliSense. Expanded installed-package verification to **129 executable XML examples**, including all 24 root examples. Live native save/close and broader-version validation remain pending; no customer model is saved or closed by offline verification. This preview is prepared locally, not publicly published.

## 1.0.0-preview.20261013.10 - 13 October 2026 (load batch 4, prepared)

Added PopulateMemberLinearVaryingLoads, PopulateMemberLoads and PopulateMemberTrapezoidalLoads with matching signatures and detailed IntelliSense. Individual-family calls append; full population replaces managed lists with all six supported families in reference order and returns an eager snapshot of original beam objects. Preserved raw fields and supplied case identity with input validation before updates and documented partial failure behavior.

Completed twelve pending overloads in four batches. Validated 733 offline tests and 123 installed-package XML examples. Load coverage is 78 overloads, with 42 reference overloads pending. Live native mutation/unit/version checks remain pending.


## 1.0.0-preview.20261013.9 - 13 October 2026 (load batch 3, prepared)

Added GetRepeatLoadCases by scanning checked primary activation and native repeat-command counts, replacing the reference stub. Returned metadata classifies underlying primary IDs as RepeatLoad; native zero-count ambiguity is documented. Added append-only PopulateMemberConcentratedLoads and PopulateMemberConcentratedMoments with original case identity, single input snapshots and validated native readers.

Validated 728 offline tests and 117 installed-package XML examples. Load coverage is 75 overloads, with 45 reference overloads pending. Live native mutation/unit/version checks remain pending.


## 1.0.0-preview.20261013.8 - 13 October 2026 (load batch 2, prepared)

Added GetLoadCombination, GetMemberLoadsGroupedByLoadCase and GetNodalLoadsGroupedByLoadCase with detailed matching IntelliSense. Validated combination Boolean status, source arrays and primary membership; rejected unrepresentable SRSS overall factors. Preserved managed refresh, reference case identity, exact definition comparison and duplicate-assignment buckets.

Validated 720 offline tests and 111 installed-package XML examples. Load coverage is 72 overloads, with 48 reference overloads pending. Live native unit/version validation remains pending.


## 1.0.0-preview.20261013.7 - 13 October 2026 (load batch 1, prepared)

Added the thread-count AddSelfweightInXYZTo overload, CreateLoadCombinations and editor-definition CreateLoadEnvelopExt with matching signatures. Preserved the existing typed self-weight and integer-envelope implementations. Added invariant-culture editor combinations, eager filtering/validation and caller-thread entity-specific self-weight. Shared XML documentation is now an explicit compiler input for reliable incremental IntelliSense updates.

Validated 708 offline tests and 105 installed-package XML examples. Load coverage is 69 overloads, with 51 reference overloads pending. Live native mutation/unit/version verification remains pending.


## 1.0.0-preview.20261013.4 - 13 October 2026 (prepared)

- Added root `GetShortJobInfo()`, `GetFullJobInfo()` and `SetInputUnitsEx(LengthInputUnit lengthUnit, ForceInputUnit forceUnit)` with reference signatures and complete matching interface/concrete XML IntelliSense. Implementations stay in the existing non-partial root adapter.
- Added standalone property-notifying `ShortJobInfo` and `FullJobInfo` models with reference property/factory signatures. Native metadata reads use per-call BSTR sentinels, validate every output before model/date mapping and return independent snapshots. Empty dates map to DateTime.MinValue; nonempty dates preserve CurrentCulture parsing and invalid dates throw.
- Validate unit enums before a single native void SetInputUnits call, preserving length/force codes and fluent return semantics. Document input-state mutation, no automatic conversion/save/rollback and the unnamed length-code alias excluded by the current enum.
- Root coverage is **9 of 23 methods**, with **14 pending**. Load coverage remains 66 overloads, with 54 reference overloads pending. Custom IOSRoot implementations must implement the new members.
- Passed **694 offline tests**, including 90 new cases for all field mappings, malformed/unwritten output, date behavior, model ownership/notifications, all 56 defined unit pairs and compiled IntelliSense. Release build passed with zero warnings/errors; isolated installed-package verification compiled/executed **99 XML examples**.
- Read-only native job queries on STAAD.Pro Advanced 2026's unnamed session left outputs unwritten and were rejected as documented. Native named-model metadata/date conventions and live unit switching remain pending. This increment has not published a new version publicly.

## 1.0.0-preview.20261013.3 - 13 October 2026 (prepared)

- Added root `GetOpenedStaadFilename()`, `IsTarget(string staadFileFullPath)` and `ModelIsOpen(string fileFullPath)` with the main project's signatures, complete matching interface/concrete XML IntelliSense and executable examples. Implementations remain in the existing non-partial root adapter.
- Normalize complete model paths and compare them case-insensitively. Validate requested paths before COM access, accept relative paths using the process working directory, return false for explicitly unnamed sessions and propagate native/path failures. Each function reads the native model filename once.
- Corrected the reference's `ModelIsOpen` full-path-flag/basename behavior and empty-filename normalization. Corrected shared root output initialization to a string sentinel so native `GetSTAADFile` receives BSTR rather than null/VT_EMPTY, while unwritten output is still detected.
- Root coverage is now **6 of 23 methods**, with **17 pending**. Custom `IOSRoot` implementations must implement the added members. Load coverage remains 66 overloads with 54 pending reference overloads.
- Passed **604 offline tests**, including 21 new cases, and a Release build with zero warnings/errors. Isolated package verification compiles/runs **93 XML examples**, including all twelve root examples, and checks normalized identities, distinct-directory basenames and unnamed sessions.
- Read-only live checks on STAAD.Pro Advanced 2026's unnamed session passed for all six root methods with the corrected string output buffer. Native named-model and broader-version validation remain pending. This increment does not change provider acquisition behavior or publish a package publicly.

## 1.0.0-preview.20261013.2 - 13 October 2026 (prepared)

- Added OSLoad `AddSelfweightInXYZTo(int, LoadCaseType, IEnumerable<int>, SelfWeightDirection, double)`, `CreateLoadEnvelopExt(int, LoadEnvelopType, IEnumerable<int>)` and `CreateLoadList(LoadListType, IEnumerable<int>)` with matching main-project signatures. Added documented native-code enums and checked activation/status handling, input snapshots and independent ID arrays.
- Added OSRoot `GetStaadFileFullPath()`, `GetStaadFileNameOnly()` and `GetAnlFileFullPath()` to the existing interface/adapter binding exposed by `wrapper.Root`. Each uses one native model-path read. Analysis paths use a single snapshot and do not imply existing/current results.
- Included the prior envelope-query batch in the committed source, with standalone models, full validation and independent metadata. No adapter partial classes or private application/Bentley binary dependencies were introduced.
- Shipped matching expanded XML for every new interface/concrete function, executable examples, parameter/default/return/exception/state-change contracts, load/native mapping documentation and a root implementation/pending-method guide.
- Load coverage is now **66 overloads** (61 reference matches plus five conveniences), with **54 reference overloads pending**. Root coverage is **3 of 23 methods**, with **20 pending**. Custom implementations of `IOSLoad`/`IOSRoot` must implement the added interface members.
- Passed **583 offline tests**, with 62 added for this batch and 113 since the last committed grouped-assignment baseline. Release build has zero warnings/errors. Isolated package verification compiles/runs **87 XML examples** and exercises all new load/root functions without ProjectReferences.
- Native self-weight/envelope mutation, load-list semantics, new root reads and envelope marshaling still require live STAAD/version validation. The preview is built for NuGet distribution; public upload is a separate step and was not performed by this increment.

## 1.0.0-preview.20261013.1 - 13 October 2026 (prepared)

- Added `GetEnvelopeIds()`, `GetLoadEnvelopeDetail(int envId)` and `GetAllLoadEnvelopeDetails()` with matching main-project public signatures in `IOSLoad` and implementations/helpers in the existing non-partial `OSLoadAdapter`.
- Added standalone `ILoadEnvelopeDetail`, `LoadEnvelopeDetail` and `LoadEnvelopType` with all native classification codes 0–6, property notification and independent constructor/query membership sets.
- Checked envelope fetch success **1** and rejected failure **-1**, malformed counts, invalid classifications, inconsistent arrays, duplicate/nonpositive IDs and unwritten outputs. Supported replaced/in-place arrays and nonzero SAFEARRAY lower bounds. Valid zero counts return non-null empty sets; all-envelope reads are eager and return no partial result on failure.
- Preserved active-case and active-load-list state. Envelope results contain metadata/membership, with no computed extrema, native writes or member-case resolution. Documented mutable-model behavior and the intentional validation/empty-result differences from the main reference.
- Added full expanded IntelliSense on interface and concrete calls, supporting-type documentation, an envelope guide and updated API/inventory references. Load coverage is now **63 overloads**, comprising 58 reference matches plus five repository-specific conveniences; **57 reference overloads remain pending**.
- Verification covers **521 passing offline tests**, a Release build with no warnings/errors, and isolated package installation with **75 compiled/executed XML examples**, including all six new envelope examples. Live envelope queries remain pending validation against native STAAD. This prepared version has not been published by this increment.

## 1.0.0-preview.20261013 - 13 October 2026 (prepared)

- Added `AddAxialThermalGradientTo(int loadCaseId, IEnumerable<int> entities, double axialThermalGradient, int nThreads)` with the reference signature and one scalar temperature assignment per entity. Validated all inputs before checked primary-case activation. Kept COM calls on the caller's thread instead of the reference worker dispatch; positive `nThreads` is retained for compatibility and does not control concurrency.
- Added `AddMemberLoadsFromGroupsByLoadCase(Dictionary<ILoadCase, List<(MemberLoad Load, List<Beam> Beams)>> grouped)`, preserving member-tuple skipping and delegation to the existing member assignment helper.
- Added `AddNodalLoadsFromGroupsByLoadCase(Dictionary<ILoadCase, List<(NodalLoad Load, List<Node> Nodes)>> grouped)`, preserving delegation to the existing six-component nodal helper and its null/empty behavior.
- Preserved grouped case-key routing, concrete member-load mapping, raw values and managed-object ownership. Documented legacy activation-status handling, native exception propagation and partial changes without rollback.
- Added matching expanded interface/concrete IntelliSense and executable examples. Updated load coverage to 60 overloads, including 55 reference matches and five repository-specific conveniences; 60 reference load overloads remain pending.
- Passed 470 offline tests and a Release build with no warnings/errors. Extended installed-package verification to restore a fresh consumer and compile/run all 69 XML examples, including the three new APIs. Native live mutation, engineering-unit and STAAD-version validation remain pending. This package is prepared for manual upload and has not been published to NuGet.org.

## 1.0.0-preview.20261012.3 - 12 October 2026 (prepared)

- Added `CreateMultipleBeamsExtended(IOSRoot openStaad, IEnumerable<Beam> beamCollection)` with the original signature and ordered native `CreateBeam` calls. Replaced the reference's non-working bulk implementation; validated all incidences before mutation and documented partial failure without rollback.
- Added `CreateMultipleNodesExtended(IOSRoot openStaad, IEnumerable<Node> nodeCollection)` with explicit IDs and a row-aligned XYZ coordinate matrix in model input units.
- Added `MergeNodesExtended(IOSRoot openStaad, Node retain, IEnumerable<Node> nodesToMerge)` preserving retained-ID inclusion in the native merge array.
- Added the minimal `IOSRoot`/`OSRootAdapter` binding and `OpenStaadWrapper.Root`, populated on construction and cleared on disposal. The supplied root selects the model for extended mutations.
- Added `AddReferenceLoadsTo(int loadCaseId, Dictionary<int, double> loadCaseNumberFactorPairs)`, `AddRepeatLoadsTo(int loadCaseId, IDictionary<int, double> loadCaseNumberFactorPairs)` and the synchronous `AddAxialThermalGradientTo(int loadCaseId, IEnumerable<int> entities, double axialThermalGradient)` overload. Added input prevalidation, paired factor snapshots, primary activation checks and empty-input no-ops. Nonempty calls leave the target active.
- Added matching detailed interface/concrete XML contracts and executable examples. Updated coverage to 140 geometry and 57 load overloads; three geometry and 63 reference load overloads remain pending. See [geometry](Geometry-Porting.md) and [load](Load-Porting.md) inventories.
- Extended installed-package verification to compile/run 63 XML examples and check all six native argument contracts. Offline validation passed 441 tests. These checks use managed native stand-ins; live native mutation and unit/version validation remain pending. This version has not been published to NuGet.org.

## 1.0.0-preview.20261012.2 - 12 October 2026 (prepared)

- Added `SegregateBeamsByConnectivity(IEnumerable<Beam> beams)` to group connected analytical beams and order simple chains.
- Added `SegregateMembersByConnectivity(IEnumerable<Member> members, MemberConnectivityScope scope = MemberConnectivityScope.EndpointsOnly)` for endpoint or all-node member grouping.
- Added `SegregatePlatesByConnectivity(IEnumerable<Plate> plates, PlateConnectivityScope scope = PlateConnectivityScope.SharedEdge)` for candidate-edge or any-corner plate grouping, preserving the reference predicate.
- Added the standalone `ConnectivityService` and matching connectivity-scope enums. The algorithms operate on managed entities without native STAAD calls. Beam input is explicitly deduplicated by ID to accommodate the target entity's endpoint-aware equality.
- Restored and expanded XML documentation for every geometry/load overload on both interfaces and concrete adapters, plus previously undocumented exported managed functions. Shared XML source comments expand into the NuGet XML file; documentation warnings now fail builds.
- Added a complete [geometry comparison](Geometry-Porting.md): nine methods were pending before this increment, three are now ported, and six remain.
- Extended package checks to validate complete adapter documentation and compile/run 51 XML examples, including the three new APIs. Added offline connectivity and compiled-XML regression coverage. This version has not been published to NuGet.org.

## 1.0.0-preview.20261012.1 - 12 October 2026

Author: **Srinivasa Rao Masanam**. Copyright (c) 2026 StaadDev. License: MIT.

- Fixes `Get(fullPath)` reopening a matching running model: `GetSTAADFile` now uses Bentley's output-VARIANT signature (`ref object filename, true`) rather than treating its return value as the filename.
- Recognizes OpenSTAAD's registered class-ID ROT monikers as well as model paths and ProgID names. Always includes the active ProgID candidate, skips unrelated solution registrations, and releases enumeration COM objects after each lookup.
- Replaces fixed three/four-second sleeps with polling every 250 ms, for up to two minutes. A path-specific acquisition returns only the requested model, with Geometry/Load interfaces and read-only count calls available. An identified loading model is waited for without reopening it. Connection failures/timeouts still return null, with Trace diagnostics.
- Clarifies legacy WPF installation: use Visual Studio's `Install-Package` with the project selected. The solution directory may contain no project, and an unconfigured legacy project can also reject `dotnet add package`.
- Validation: 361 passing offline tests, including 19 attachment/ROT regression cases; restore/build of a copy of the user's legacy WPF project against public `1.0.0-preview.20261012`; isolated corrected-package verification of 42 executable XML examples and all 21 load queries.
- Live validation on installed STAAD.Pro 2025: the supplied model opened on the first call in approximately 41.8 seconds, returning Geometry/Load with 6,444 nodes and 74 primary load cases. The second call took approximately 57 ms and used the same STAAD process with no additional session. This is one model/version check; engineering units and broader version compatibility still require validation.

This corrected version supersedes `1.0.0-preview.20261012`. Already-published `1.0.0-preview.20261012` remains unchanged. Public availability is recorded in the [release assessment](NuGet-Readiness.md); see [Publishing.md](Publishing.md) for public-feed verification.

## 1.0.0-preview.20261012 — 12 October 2026

Author: **Srinivasa Rao Masanam**. Copyright (c) 2026 Srinivasa Rao Masanam. License: MIT.

This preview targets Windows and **.NET Framework 4.8.1 (`net481`)**. It contains the existing geometry/load wrapper and twenty-one load-query functions ported from the main application's interop project. The package ID is `StaadPro.Interop`; its version is `1.0.0-preview.20261012`. Public availability requires NuGet.org submission, acceptance and public-feed verification, tracked in the [readiness assessment](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/NuGet-Readiness.md).

### Latest additions

- `GetLoadCases(int fromId, int toId, LoadCaseType loadCaseType)` returns fresh metadata for an inclusive positive ID range. Invalid bounds are rejected before COM access; sparse ranges do not require generating every integer.
- `GetLoadCasesFromIds(IEnumerable<int> loadCasesIds, LoadCaseType loadCaseType)` snapshots input once, validates IDs before COM access, collapses duplicate requests and omits missing IDs. Empty input returns a fresh empty list without native reads after validating the selected family.
- `GetLoadCasesOfType(LoadCaseType loadCaseType)` returns a fresh concrete `List<ILoadCase>` for the selected primary, reference or combination family.

These methods read the complete selected family eagerly and preserve the active load case. Metadata failures propagate even for cases excluded by a filter. Output ordering is unspecified. Repeat and undefined families are unsupported. Results contain metadata; they do not load assignments, combination factors or analysis results.

### Other load queries included in this preview

Seven assignment readers return nodal forces/moments and member distributed forces, point forces, point moments, linear varying loads, trapezoidal loads and uniform moments:

- `GetNodalLoads`
- `GetMemberUniformlyDistributedLoads`
- `GetMemberConcentratedLoads`
- `GetMemberConcentratedMoments`
- `GetMemberLinearVaryingLoads`
- `GetMemberTrapezoidalLoads`
- `GetMemberUniformMoments`

Eleven other metadata queries provide family discovery, direct lookup, batch lookup and generic family selection:

- `GetAllPrimaryLoadCases`, `GetAllReferenceLoadCases`, `GetAllLoadCombinationCases`
- `GetPrimaryLoadCaseFromId`, `GetReferenceLoadCaseFromId`, `GetLoadCombinationCaseFromId`
- `GetPrimaryLoadCasesFromIds`, `GetReferenceLoadCasesFromIds`, `GetLoadCombinationCasesFromIds`
- `GetAllLoadCasesOfType`, `GetLoadCase`

Primary/reference batches preserve requested order and duplicates as separate objects; combination batches collapse duplicates and validate all memberships before reading titles. Generic lookup returns null for a missing positive ID after successfully reading the selected family. Assignment readers activate the supplied case and preserve native signs, order and raw units. Family metadata readers leave active-case state unchanged. See [Load-Queries.md](Load-Queries.md) and the [API reference](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/API-Reference.md) for complete signatures, contracts and examples.

### Validation and documentation

- Inputs, native statuses, counts, arrays, directions and metadata are checked; malformed responses throw rather than returning partial results. SAFEARRAYs with nonzero lower bounds are supported by the assignment readers.
- Uniform moment model conversion uses moment divided by length for intensity and treats eccentricity as a length. This corrects the main model's dimensional conversion and omitted eccentricity conversion.
- All twenty-one ported functions have expanded XML IntelliSense on both `IOSLoad` and `OSLoadAdapter`, including parameters, return values, exceptions, side effects and executable examples. Adapter implementations remain in a single non-partial class.
- The package ships XML beside the DLL, a NuGet README, load-query documentation, release notes, and [installation](Installation.md)/[publishing](Publishing.md) guides.
- Release verification covers **342 passing offline tests**, **42 compiled and executed XML examples**, and a compiled installation example in an isolated PackageReference consumer using a fresh cache. The managed stand-in exercises mapping and error contracts; it does not certify native STAAD behavior.

### Compatibility and remaining validation

A licensed STAAD.Pro installation exposing the required OpenSTAAD APIs is needed for actual model access. Bentley binaries and the private main application are not bundled. Newtonsoft.Json 13.0.3 is a NuGet dependency. Shared-session COM coordination remains the application's responsibility.

Live COM dispatch/marshaling, engineering unit read-back and STAAD version/architecture compatibility remain pending. Native combination-number return-count documentation also requires validation with differing family counts. The readiness assessment records these and inherited implementation gates before a stable release. This preview makes no claim of verified support across STAAD versions.

### Installation

After this exact version becomes publicly available:

```powershell
dotnet add ./YourApp.csproj package StaadPro.Interop --version 1.0.0-preview.20261012 --source https://api.nuget.org/v3/index.json
```

Enable prerelease packages in Visual Studio's NuGet UI. The verified archive can also be installed through a local feed before publication; see [Installation.md](Installation.md).
