# StaadPro.Interop release notes

## 1.0.0-preview.20261024.1 - 24 October 2026 (prepared preview)

Updated the package author and assembly company to **IngeneStructs**. Updated both package and assembly copyright to **Copyright (c) 2026 IngeneStructs**, and aligned the packaged README, publishing guide and release metadata. NuGet.org account ownership is configured separately through the portal; the archive metadata does not transfer it.

This metadata update retains the preceding Boolean case-number fix and the existing public API/target-framework inventory. Earlier release entries retain their historical metadata. Built a new net481 archive and verified its manifest, installed assembly company/copyright, DLL/XML assets, shipped guides and all **542 selected XML examples** through isolated PackageReference restore/build/runtime verification. The implementation's preceding **3,574-test** verification and scoped STAAD.Pro 25.00.00.539 live query validation are recorded below.

The archive is prepared for manual upload as a new version; earlier archives are retained. Author/company: **IngeneStructs**. Copyright (c) 2026 IngeneStructs.

## 1.0.0-preview.20261024 - 24 October 2026 (prepared preview)

Fixed primary and combination discovery rejecting the Boolean `true` returned by `GetPrimaryLoadCaseNumbers` and `GetLoadCombinationCaseNumbers` on the observed STAAD.Pro 25.00.00.539 build. The shared readers now handle Boolean success separately from integral case counts, validate the complete ID array, and recheck the separate count before reading metadata. Boolean false, malformed arrays and inconsistent counts still fail. Reference discovery retains its integer-count contract; matching documented integral primary/combination results remain supported.

- Corrected the readers used by `GetLoadCasesFromIds`, `GetAllPrimaryLoadCases`, family/range filters, primary population queries and combination discovery/lookups. Kept the general integer decoder strict and public signatures unchanged.
- Added Boolean-success/failure, 74-primary/230-combination, in-place buffer, malformed-array, changed-count, strict reference-return and integral compatibility regressions. Removed the test expectation that Boolean true is invalid for primary/combination number retrieval.
- Updated matching interface/adapter XML IntelliSense, query/porting guides, installed-package fixtures and installation/publishing documentation. Retained all 50 Property overloads, 171 Load overloads, 143 reference Geometry overloads, 15 typed Output queries and reviewed Root/wrapper APIs.

Passed a Release build with zero warnings/errors and **3,574 offline tests**, including **43 new case-number return regressions**. Isolated PackageReference verification compiles and executes **542 selected XML examples**; the packaged primary and combination stand-ins now return Boolean success. Native return shapes were observed read-only during diagnosis; wider native/version and engineering qualification remains pending.

The net481 archive is prepared for manual upload and has not been published to NuGet.org by this task. No model-opening, case-activation, geometry-editing or save operation is added by the correction. Author: Srinivasa Rao Masanam. Copyright (c) 2026 StaadDev.

Post-package validation on STAAD.Pro **25.00.00.539** passed using the DLL extracted from this exact archive: the reported ID filter returned cases 1, 2 and 3, and primary/reference/combination discovery matched the native inventories. The model path, active case, node count and case counts remained unchanged. This additional source documentation records the live result without replacing the prepared archive. See [the validation record](NuGet-Readiness.md#post-package-live-verification) for scope and limits.

## 1.0.0-preview.20261023.1 - 23 October 2026 (prepared preview)

Added 25 main-project Property overloads for section/thickness inventories, assignment discovery, material context and assignment, member specifications, and global-Y member-end offsets. Property now exposes **50 overloads**: the existing 25 typed observations and these 25 signature-compatible ports. All new interface and adapter methods include expanded XML IntelliSense with parameters, return values, examples, units, side effects and failure contracts. Shared locking/lifetime checks, bounded native buffers and checked operation-specific statuses are included. Passed **3,531 offline tests**, including **209 new Property cases**, with zero build warnings/errors. The installed-package verifier compiles and runs **542 selected XML examples**, including all **100 Property examples**, against managed stand-ins. Live COM and engineering qualification remain pending.

- Ported section/thickness inventory and assignment lookups: `GetUnusedSectionsCount`, `GetUnusedSectionsIds`, `GetBeamProperty`, `GetSectionPropertyList`, `GetSectionPropertyName`, `GetSectionPropertyValues`, `GetSectionPropertyAssignedBeams`, `GetBeamsWithMissingSectionProperty`, `GetThicknessPropertyList`, `GetThicknessPropertyAssignedPlates`, `GetPlatesWithMissingThicknessProperty` and `GetAveragePlateThickness`.
- Ported plate material and member behavior reads: `GetPlateMaterialName` and `GetMemberSpecification`, with exact main-project enum codes.
- Ported both scalar/array `AssignBeamProperty` and `AssignMaterialToMember` overloads, `SetMaterialName`, both `AssignMemberEndOffsets` overloads and all four `AssignMemberEndOffset` overloads. Preserved main no-op inputs and documented global-Y units, native status conventions and partial-mutation effects.
- Retained the existing 25 typed Property observations, 171 Load overloads, all 143 reference Geometry overloads, 15 typed Output queries and reviewed Root/wrapper APIs. Added matching interface/adapter XML, two supporting enums, shared-gate/lifetime validation and the packaged [25-method guide](Property-Main-Ports.md).
- Rejected failed native responses, negative/oversized/changing counts and malformed buffers. Preserved raw zero-padded section values without a universal version-dependent parser. Kept obsolete inactive-member methods excluded.

The net481 archive includes the earlier source-only Property foundation, Load inspection and Load expansion increments recorded below. Verification uses managed stand-ins and an isolated PackageReference consumer restoring the exact local archive; it performs no customer-model mutations. Native COM marshaling, section-layout interpretation and engineering qualification remain pending. This archive is prepared for manual upload and has not been published to NuGet.org. Author: Srinivasa Rao Masanam. Copyright (c) 2026 StaadDev.

### Earlier source validation — Property foundations

Implemented the first 25 typed Property queries for member and plate offsets, releases, section properties and material observations. Results use named status/data classes, existing MemberEnd and Vector3D types, owned collections and comparative XML IntelliSense. Shared 2024–2026 contracts use one internal reader. Native naming, selector and documentation ambiguities are recorded rather than replaced with inferred values. Passed 3,322 offline tests, including 422 new Property cases, and a Release build with zero warnings/errors. Native COM and engineering qualification remain pending; 179 active Property mappings remain to be implemented. The parked Load work and prepared distribution archive are unchanged. Verified a separate local CI archive through isolated installation and compilation/execution of all 492 selected XML examples, including the 50 new Property examples. See the [Property guide](Property-Porting.md) for all 25 signatures, source reuse decisions and native qualification limits.

### Earlier source validation — Load expansion implementation

Implemented all 36 approved Load expansion methods for case/list/envelope management, automatic combinations and repeat generation, whole-model self-weight, additional assignments, original-command and plate/floor readback, reference contributions and beam load transfer. Source now exposes 171 Load overloads and dispatches 126 of 129 documented native names, plus the metadata-corroborated merge body. Retained managed alternatives for the three unused entry points. Added matching comparative XML IntelliSense on interface and adapter, typed results, validated snapshots, explicit family activation, bounded initialized outputs, changed-state checks and balanced merge cleanup. GetLoadAttribute exposes documented status only; MergeBeamLoads reports invocation completion without certifying success. Passed 2,900 offline tests, including 424 new expansion cases, and a Release build with zero warnings/errors. A separate local CI archive passed isolated restore/build/runtime verification for 442 selected XML examples, including all 72 expansion examples with mutation guards disabled. Native COM and engineering qualification remain pending. The prepared distribution archive is unchanged.

### Earlier source validation — Load contract preparation

Prepared the original 56-entry contract catalog and 36 proposed managed additions, with consistent named results, typed requests and descriptive parameters. Reused existing case, force and inspection models, ported the semantic GlobalAxis enum, and defined required supporting classes/enums with XML documentation and managed validation. Added a public validated constructor to the existing notional contribution type for future assignment input. Kept pending signatures in a separate compile-checked review file rather than exposing throwing runtime methods. Documented internal service boundaries and withheld attribute/merge contracts pending native evidence. Passed 2,476 offline tests, including 28 contract-focused cases, and a Release build with zero warnings/errors. This preparation adds no native dispatch and does not rebuild the prepared package.

### Earlier source validation — Load inspection queries

Added 15 Load inspection operations for active-case number, native lists, command/item counts and kinds, dynamic inclusion, indexed repeat/notional contributions and reference-command counts. Added distinct command/item/direction enums, named status/data results, immutable contribution snapshots, bounded internal buffers, checked primary activation, binding-lifetime validation and complete interface/adapter/model XML documentation. Preserved order, duplicate contributions, native status meanings and signed notional case-family identity.

Source now exposes 135 Load overloads and dispatches 88 of 129 documented native names. Of 41 undispatched names, one has an existing alternative, one is derivable, four are partially covered and 35 have no managed equivalent. Passed 2,448 offline tests, including 189 new cases, and compiled/executed all 30 new XML examples. Verified a validation-only CI archive through isolated PackageReference installation and all 370 selected XML examples. This increment is not included in the prepared preview archive below; no new distribution archive was built or published, and live native qualification remains pending.

## 1.0.0-preview.20261023 - 23 October 2026 (prepared preview)

This preview includes the completed main-interface Geometry and Load ports and the first 15 typed Output queries, alongside the reviewed Root APIs and approved wrapper surface.

- Completed all 143 reference Geometry overloads, including physical-member identification, plan-angle grouping and bounded polygon meshing.
- Completed all 115 main-interface Load signatures, exposed through 120 managed overloads including five conveniences. Included wind and seismic configuration, response-spectrum requests, direct-analysis definitions, population readers and related typed parameter models.
- Added 15 Output observations for analysis/buckling availability, node displacements, support reactions, local member-end actions, static checks, seven plate-center result operations and member design section/utilization. Results use named status/data classes and immutable payloads, with strict native-response validation and expanded interface/adapter/model IntelliSense.
- Corrected Load coverage terminology: 56 documented native entry points lack direct/delegated calls, but one capability has an existing alternative and one is derivable. Four have partial coverage and 50 have no managed equivalent found. This release does not add those broader native operations.
- Updated packaged usage, installation and publishing guidance. Retained 53 common Root operations, three optional Root operations and the approved wrapper bindings. Seven remaining domain adapters retain placeholder APIs.

Release verification covers a Release solution build, **2,259 offline tests**, and isolated PackageReference installation with compilation/execution of **340 selected XML examples** against managed stand-ins, including all 30 new Output examples. Package verification checks metadata, target assets, XML documentation and shipped guides. These checks do not invoke a native STAAD model or certify live marshaling, result units or engineering accuracy.

Targets Windows and .NET Framework 4.8.1 (net481). The archive contains no .NET Core/.NET 8 library asset. Live native compatibility and engineering qualification remain pending; 45 documented Output mappings and additional Load scopes remain separate work. This entry describes a locally prepared preview, not a new public NuGet.org publication.

### Unreleased source development — 23 October 2026

Implemented 15 typed Output queries for analysis/buckling availability, nodal displacement, support reactions, local member-end forces, static checks, seven plate-center result families and member design section/ratio observations. Added named status/data results, immutable owned payloads, strict native flags and buffer validation, documented design sentinel outcomes, binding-lifetime guards and complete interface/adapter/model IntelliSense. Reused source component conventions and existing force models without modifying the private source project or wrapper. Passed 2,259 offline tests, including 74 new Output cases, and a Release build with zero warnings/errors. Compiled and executed all 30 new Output XML examples against managed native stand-ins.

Reconfirmed complete Load reference parity: 115 main-interface overloads are present in both current types, each exposing 120 overloads including five conveniences. No reference Load port was pending. The broader vendor-only Load inventory remains separate work. Forty-five documented Output native names and live marshaling/engineering qualification remain pending. No NuGet archive was built or published for this increment.

### Unreleased source development — 22 October 2026

Completed the final 15 main-interface Load reference overloads, including remaining seismic member/wall operations, extended and legacy response-spectrum loads and direct-analysis definition APIs. Reused source request models and enum domains; added independent native packets, strict validation, checked Single conversion, guarded examples and complete interface/adapter/model IntelliSense. Source now exposes 120 Load overloads (115 reference plus five conveniences), with no reference interface overload pending. Passed 2,185 offline tests, including 161 new cases, and a Release build with zero warnings/errors. Compiled and executed all 310 selected source-reference XML examples with native mutations guarded; updated reusable package verification for the same inventory. Native numerical/marshaling validation and the broader managed capability/result architecture remain separate work; no NuGet archive was built or published.

Ported the next 15 reference Load overloads for ASCE wind definitions/profile calculations and seismic definition, parameter and weight operations. Reused the main source model hierarchy without proprietary binary dependencies. Added acquired-profile seismic guards, owned array packets, strict operation-specific result validation and complete interface/adapter/model IntelliSense. Preserved caller-thread execution and documented partial updates, native code distinctions and calculation-layout discrepancies. Current source exposes 105 Load overloads (100 reference plus five conveniences), leaving 15 reference overloads pending. Validated 2,024 offline tests, including 263 new cases. Compiled and executed all 280 selected source-reference XML examples with native mutations guarded; updated the reusable package verifier accordingly. No NuGet archive was built or published for this increment; live native numerical and cross-version marshaling validation remains pending.

Added 12 reference Load overloads for uniform member-load population, nodal refresh and basic wind definitions, exposure, intensity and deletion. Reused validated population readers and strict native integer handling. Added the immutable WindIntensityPair value model, descriptive wind parameters, full XML documentation, guarded examples and regression tests. Source Load coverage is now 90 overloads with 30 reference overloads pending; the next package verifier expects 250 selected XML examples. Validated 1,761 offline tests, including 157 new Load cases, and compiled/executed all 250 selected source XML examples with guarded native mutation samples. This work is not included in the published preview.

Completed the remaining three Geometry functions: IdentifyPhysicalMembers, GroupPhysicalMembersByPlanAngle and GeneratePolygonMesh. Added source-compatible result models, the Beam.Member managed back-reference, validated member reconstruction, XZ plan-bearing groups and bounded planar polygon meshing with explicit node/plate IDs and honest quality reporting. Corrected source mesh mapping, topology and generated-ID defects. Added complete interface/adapter/service XML contracts, regression tests and wrapper integration checks. Reconfirmed wrapper member parity; kept the eight placeholder domain APIs separate. Updated future package verification for 143 Geometry methods and 226 selected XML examples. These additions are not in the published 1.0.0-preview.20261015 package.

## 1.0.0-preview.20261015 - 15 October 2026 (prepared preview)

Completed the reviewed Root API port and expanded the wrapper surface. This preview includes all 39 reviewed native Root function mappings and all 23 private-source Root method mappings, exposed through 53 common managed operations and three separately acquired optional operations.

- Added the complete approved wrapper surface, raw native object bindings, application metadata and adapter placeholders for domains whose operations will be ported separately.
- Added wrapper-backed metadata queries, analysis state and status results, model information, input-unit selection, job metadata, CONNECTED project information and explicit analysis requests.
- Added guarded model creation, named-view creation, saving, removal and modification, opaque process-handle values, explicit application shutdown and qualified member-end force results.
- Added optional ADINA export and analysis error/warning retrieval through capability interfaces. Availability requires an exact candidate-build policy and compatible native member metadata. Candidate profiles cover export on 25.0.1.424 and export/messages on 26.0.0.340; these declarations do not certify live native behavior.
- Added descriptive managed parameters, named status/data results, typed options, bounded message retrieval and complete XML IntelliSense for the new operations. Expanded the packaged Root and wrapper usage guides.

Release validation covers 1,547 offline tests, a Release solution build, and isolated installation of the archive with compilation and execution of 217 selected XML examples against managed stand-ins. Mutating examples remain guarded; these checks do not run native STAAD operations. See the [Root completion guide](Root-Completion-and-Capabilities.md) for contracts and remaining validation gates.

Targets Windows and .NET Framework 4.8.1 (net481). This package does not contain a .NET Core library asset. New interface members require updates to custom interface implementations. Existing geometry and load APIs remain included. Live STAAD compatibility, native flag meanings, result-unit conventions and file side effects require validation before a stable release. This entry describes a prepared archive; public availability requires a separate NuGet.org upload and verification.

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
