# OpenSTAAD Commands multi-version comparison, architecture and implementation plan

**Reviewed:** 23 October 2026<br>
**Baseline:** main at 1d8285ce66c95c60da55530d4c721fb7274893da<br>
**Status:** Documentation and architecture only. Commands domain functions remain pending.

## 1. Findings

All three references list **18 native Commands names**: 17 detailed contracts and the summary-only deprecated PerformPDeltaAnalysisConverge. Their declarations and non-example contracts match. The single changed example between 2024 and 2025 does not establish a contract change. Use one shared internal Commands implementation; no year-specific strategy is currently justified.

The current IOSCommands / OSCommandsAdapter are binding-only placeholders. The main source provides 21 interface/public-adapter overloads and four private helper methods. Eighteen distinct native targets include the deprecated function. Completing safe public coverage therefore means 17 active native functions plus useful managed conveniences, with an explicit exclusion for the deprecated entry.

| Measure | Finding |
| --- | --- |
| Native names (2024 / 2025 / 2026) | 18 / 18 / 18 |
| Main interface / public adapter overloads | 21 / 21 |
| Main private adapter helpers | 4 |
| Main distinct native targets | 18 |
| Native names without main-adapter target | 0 |
| Current typed domain methods | 0 (binding-only placeholder) |
| Common signature changes, 2024 to 2025 | 0 |
| Non-example text changes, 2024 to 2025 | 0 |
| Example changes, 2024 to 2025 | 1 |
| Observed changes, 2025 to 2026 | 0 |
| Native class names / excluded helpers (2026) | 20 / 2 |


## 2. References and method

| Version / installed help | Documented build |
| --- | --- |
| [STAAD.Pro 2024 Commands](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___command_group.html) | 24.00.02.354 |
| [STAAD.Pro 2025 Commands](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___command_group.html) | 25.00.01.424 |
| [STAAD.Pro 2026 Commands](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___command_group.html) | 26.00.00.340 |

Read-only source: [main interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs) and [main adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs).

The [complete inventory](OpenSTAAD-Commands-API-Coverage-and-Contract-Inventory.md) includes each declaration, parameter table, return convention, value table, source overload and passive metadata. The [comparison JSON](data/OpenSTAAD-Commands-2024-2026-Comparison.json) preserves all per-year contracts, differences and source/help fingerprints.

The group tree has three pages: grouping, analysis commands and steel-design commands. Native class/header listings and other group pages were reconciled with that tree. IOSCommandsUI metadata in the installed 2025 and 2026 assemblies matches, with no extra automation-only names. OnFinalRelease and SetStructure are excluded native implementation helpers.

Both assemblies were inspected in separate reflection-only processes. No COM activation, model mutation or solver run was performed. No corresponding 2024 DLL was available at the expected installed path.

## 3. Complete cross-version function matrix

| Native Commands function | Native return | 2024 / 2025 / 2026 | Main native target | Current typed / plan |
| --- | --- | --- | --- | --- |
| `PerformAnalysis` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#gaf1594aca92c4a713f1ea09c381a1ea26) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#gaf1594aca92c4a713f1ea09c381a1ea26) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#gaf1594aca92c4a713f1ea09c381a1ea26) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L42) | Pending / C2 |
| `PerformPDeltaAnalysisNoConverge` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga4c29d820ed0a2df54d189ab77557399b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga4c29d820ed0a2df54d189ab77557399b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga4c29d820ed0a2df54d189ab77557399b) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L53) | Pending / C2 |
| `PerformPDeltaAnalysisEx` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga5ab6c9ae30773fe830dcf49f36baac27) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga5ab6c9ae30773fe830dcf49f36baac27) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga5ab6c9ae30773fe830dcf49f36baac27) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L66) | Pending / C2 |
| `PerformPDeltaAnalysisConverge` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#gad4c52c3aaac4cc9ab013ce9f45365662) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#gad4c52c3aaac4cc9ab013ce9f45365662) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#gad4c52c3aaac4cc9ab013ce9f45365662) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L90) | Excluded: deprecated |
| `PerformCableAnalysis` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#gae7c97c660ca1513f6a0dbac8248abec3) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#gae7c97c660ca1513f6a0dbac8248abec3) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#gae7c97c660ca1513f6a0dbac8248abec3) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L98) | Pending / C2 |
| `PerformCableAnalysisEx` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga4cca1c1583bc620304485e2cab92d20e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga4cca1c1583bc620304485e2cab92d20e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga4cca1c1583bc620304485e2cab92d20e) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L107) | Pending / C2 |
| `PerformBucklingAnalysis` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga9bcd75803c698cd18b6036f072ef72ed) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga9bcd75803c698cd18b6036f072ef72ed) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga9bcd75803c698cd18b6036f072ef72ed) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L118) | Pending / C2 |
| `PerformBucklingAnalysisEx` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga95a530e160b7f487b56c54892d273006) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga95a530e160b7f487b56c54892d273006) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga95a530e160b7f487b56c54892d273006) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L127) | Pending / C2 |
| `PerformDirectAnalysis` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga0d9299d951575f3d4ae01e251687c256) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga0d9299d951575f3d4ae01e251687c256) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga0d9299d951575f3d4ae01e251687c256) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L134) | Pending / C2 |
| `PerformNonlinearAnalysisEx` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga0eb3fa85e4d8ad3d76b302eead817a6b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga0eb3fa85e4d8ad3d76b302eead817a6b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga0eb3fa85e4d8ad3d76b302eead817a6b) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L145) | Pending / C2 |
| `SetFloorDiaphragmBaseCommand` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#gad15acf391bb459cb38a1a1ae5f49ecec) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#gad15acf391bb459cb38a1a1ae5f49ecec) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#gad15acf391bb459cb38a1a1ae5f49ecec) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L160) | Pending / C3 |
| `DeleteFloorDiaphragmBaseCommand` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#gad6e1f41f8a3be9dc64e3acbd65c03c9c) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#gad6e1f41f8a3be9dc64e3acbd65c03c9c) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#gad6e1f41f8a3be9dc64e3acbd65c03c9c) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L165) | Pending / C3 |
| `SetCheckSoftStoryCommand` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga8a44aecb38f1b524e76e133b1ebf06e5) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga8a44aecb38f1b524e76e133b1ebf06e5) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga8a44aecb38f1b524e76e133b1ebf06e5) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L171) | Pending / C3 |
| `DeleteCheckSoftStoryCommand` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga576c6058cc27d33d70ca234ee4d22315) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga576c6058cc27d33d70ca234ee4d22315) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga576c6058cc27d33d70ca234ee4d22315) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L176) | Pending / C3 |
| `SetCheckIrregularitiesCommand` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga0ac14e3de0cd68103d7a0b209dc8de55) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga0ac14e3de0cd68103d7a0b209dc8de55) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga0ac14e3de0cd68103d7a0b209dc8de55) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L182) | Pending / C3 |
| `DeleteCheckIrregularitiesCommand` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga87b66160d1f73f60ae616c57701d14c4) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga87b66160d1f73f60ae616c57701d14c4) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga87b66160d1f73f60ae616c57701d14c4) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L187) | Pending / C3 |
| `DeleteAllAnalysisCommands` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga73e72f489e9abc390958aa81fd300b72) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga73e72f489e9abc390958aa81fd300b72) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga73e72f489e9abc390958aa81fd300b72) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L192) | Pending / C3 |
| `CreateSteelDesignCommand` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___steel_design_command_group.html#gad6bd6feeb21c0ce8fced435b2cc6a158) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___steel_design_command_group.html#gad6bd6feeb21c0ce8fced435b2cc6a158) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___steel_design_command_group.html#gad6bd6feeb21c0ce8fced435b2cc6a158) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L244) | Pending / C4 |


## 4. Cross-version differences and command catalog

All common declarations and non-example contracts match across 2024, 2025 and 2026. No per-year service split is justified by these references.

### 4.1. Analysis command creation is not solver execution

The PerformAnalysis/PDelta/Cable/Buckling/Direct/Nonlinear functions create or assign model commands. Their names do not mean that a solver run has completed. Root analysis execution/status APIs remain separate. Document this distinction in the public XML and usage examples so callers do not read stale Output results after merely adding an analysis instruction.

PerformPDeltaAnalysisConverge is deprecated in all three references. Do not introduce it into a new public API. Retain the main source as historical evidence and use the documented NoConverge or Ex alternatives, with explicit displacement-effect semantics.

### 4.2. Steel-design catalog depth

CreateSteelDesignCommand has six native inputs: design-code ID, command number, integer operands, floating operands, string operands and assignment IDs. The manual lists 49 design-code rows and **1,066 keyword/number occurrences** across 38 command groups, laid out in ten multicolumn command tables. The extracted keyword/number sequences match across 2024, 2025 and 2026.

The main project's embedded catalog records those 49 codes, 38 groups and 1,066 source occurrences, including a duplicate FYLD/9100 occurrence for ANSI/AISC N690 1994. It marks 48 code entries Provisional and AISC ASD Unsupported because a command group is absent. Those are catalog qualification labels, not native license/support guarantees.

Reuse the source catalog schema, descriptors, validation and marshaller after review; do not rebuild a looser ad hoc command list. Compare the embedded source SHA with the actual installed page and maintain reproducible provenance. An unchanged catalog can be shared across these three reviewed profiles; its file name containing 2025 does not itself restrict or establish compatibility.

The manual does **not** supply a complete per-command slot schema. It does not justify inventing operand counts, dimensions, assignment kinds, defaults, numerical bounds or null-versus-empty SAFEARRAY behavior. Keep unqualified commands provisional until a native fixture establishes those facts. A public “allow provisional” switch must remain explicit and produce diagnostics.

The inventory preserves every table cell, including grouped columns, and the JSON records catalog counts and hashes. Group labels in the source catalog must remain part of a command identity; a keyword or number alone is insufficient across codes.

## 5. Source reuse and implementation corrections

| Source item | Reuse | Required treatment |
| --- | --- | --- |
| PDeltaAnalysisOptions | Descriptive displacement/geometric-stiffness options | Verify translation to bSmallDelta and 16-bit Boolean values; retain named scalar convenience overloads. |
| CableAnalysisOptions | Named advanced/basic options and parameter-array factory | Verify array slot order, lengths, defaults and actual license outcome. |
| DirectAnalysisOptions | LRFD/ASD and separate parameter/additional-option arrays | Keep integer-valued slots distinct even when stored in a double array; validate finite values and units. |
| NonlinearAnalysisOptions | Displacement target, rebuild policy and convergence settings | Validate coupled parameters and exact node/direction/displacement array layout. |
| PrintOption and analysis enums | Existing vocabulary | Verify allowed values per function; Buckling's documented print table omits Mode Shapes (4). |
| SteelDesignCommandCatalog and request/result models | Descriptor lookup, validation errors/warnings and explicit unit context | Preserve provisional/unsupported state and immutable snapshots; do not infer operand schemas. |
| SteelDesignCommandComMarshaller | Owned arrays in correct six-argument order | Check null/empty SAFEARRAY rules per qualified tuple; keep native arrays internal. |
| Source command lock | Serializes one adapter's steel submissions | Does not make COM apartment access safe or serialize other adapters targeting the same model. |

Source legacy functions return the adapter after a native void call. The Ex and command-removal functions normalize flags through CommandSucceeded, which accepts Boolean and Convert.ToInt32(value)==1. A public port needs strict shape validation so a fractional/coercible value cannot silently become success. Preserve unexpected codes for diagnostics.

CreateSteelDesignCommand returns native void; the source's request validation is useful but does not establish that STAAD applied the command. Return a named submission result with completion evidence; verify command insertion separately when a reliable read-back exists. Do not report a completed solver analysis.

SetFloorDiaphragmBaseCommand uses a length value. Negative elevations may be valid; validate finiteness and explicit unit context rather than requiring every number to be positive. Floor-diaphragm checks require their documented model data and distinct code tables: soft-story accepts 1/2/3, irregularities accepts 2/3.

## 6. Proposed public and internal design

Keep one IOSCommands and non-partial OSCommandsAdapter. Store public requests/results under Models/Commands, verified enums under Enums/Commands, and internal invocation, array layout and catalog services under Services/Commands. A Catalogs/Commands area can contain a small owned machine-readable catalog and schema after licensing/provenance review; never embed installed Bentley binaries/help.

Use CommandSubmissionResult with submission state, native code where supplied, operation name, catalog/profile identity and diagnostics. Native void produces DispatchCompleted evidence, not a fabricated successful Boolean. Where native 1/0 exists, retain that distinction; deletion returning zero can mean no matching command rather than a transport failure.

Request option classes expose named fields; constructors/validation enforce coupled requirements. Result collections are owned and read-only. Validation returns a named class containing errors, warnings and normalized immutable operands. Exposed functions have no ref/out or tuples. Convenience overloads delegate to one options path.

Capture wrapper.StaadVersion and RawCommands, whose native root property is **Command (singular)**. ApplicationVersion must not route native calls. The current metadata evidence supports common services with capability/context checks, not one class per year.

~~~mermaid
flowchart TD
    A["Capture acquired wrapper and Command binding"] --> B["Validate request and operation capability"]
    B --> C{"Steel command?"}
    C -->|Yes| D["Resolve code and qualified catalog descriptor"]
    D --> E["Validate typed operands, assignment and unit context"]
    C -->|No| F["Build documented analysis-command options"]
    E --> G["Invoke once on the owning COM apartment"]
    F --> G
    G --> H["Preserve native status or dispatch-only evidence"]
    H --> I["Return submission result"]
    I --> J["Caller explicitly requests Root analysis if needed"]
~~~

Avoid retries after ambiguous mutations: a rejected COM call may still have inserted a command. No automatic rerun or delete-all recovery. Unsupported versions/operations return an actionable capability diagnostic; they must not redirect to another running model.

## 7. Ordered implementation plan

| ID | Scope | Exit criteria |
| --- | --- | --- |
| C1 | Common submission results, strict native-value handling, options/enums and serialized invocation seam | Approved public API; per-function flags and void behavior specified. |
| C2 | Nine active analysis command functions and source options/conveniences | All slot maps/defaults/flags tested; deprecated Converge excluded; solver execution remains explicit. |
| C3 | Seven diaphragm/check/deletion operations | Correct code lists, unit context, model prerequisites and destructive-operation semantics. |
| C4 | Steel catalog, validator, marshaller and CreateSteelDesignCommand | Provenance reconciled; command identity preserved; unqualified schemas clearly provisional. |
| C5 | Native cross-version fixtures, XML examples and packaging | Input-file command verification and error paths recorded for each qualified profile. |

The matrix assigns every entry, including the excluded deprecated name. C2's nine active native names plus C3's seven and C4's one make 17 active functions; source managed helper counts must not inflate that total.

## 8. Validation and IntelliSense requirements

Test all documented array indexes/defaults and exact flag widths; invalid/unknown enums; NaN/infinity; invalid iterations; coupled cable/direct/nonlinear parameters; null/duplicate assignment IDs; missing unit context; provisional/unsupported catalog entries; duplicate catalog tuples; operand slot gaps; invariant serialization and single enumeration.

Test native 1/0, Boolean, unexpected integer, fractional/coercible, null and exception shapes independently. Verify that void dispatch cannot masquerade as confirmed model mutation or completed analysis. Test model refresh/disposal and command submission on the owning apartment. Locks cannot replace apartment marshaling.

Native qualification must inspect resulting STAAD input commands for each analysis family and representative steel-code tuple, check parameter/assignment order and null/empty behavior, and record model/license prerequisites. Never run destructive commands against a user's working model as a capability probe.

Every method, option property, enum value and result field needs XML summary, descriptive parameter text, valid values, units/defaults, return semantics, exceptions, thread/ownership rules, model/license prerequisites and a compiling example. Specify whether the operation inserts, replaces or deletes commands.

## 9. Package boundary

This documentation review implements nothing and builds no package. Future ports remain late-bound and must include XML documentation. Catalog/JSON dependencies require an explicit packaging decision; reuse the existing approved dependency where suitable instead of silently adding another serializer. The current package targets .NET Framework 4.8.1; no modern .NET compatibility is inferred from this review.
