# OpenSTAAD Commands API coverage and native contract inventory

**Implementation checkpoint, 26 October 2026:** 17 managed methods now cover all 16 active native analysis-command routes; the steel-command catalog group remains pending. See [the current contracts and usage](../Commands-and-Support-Porting.md). Earlier review counts and proposals below retain their original review context.

**Reviewed:** 23 October 2026<br>**Status:** Seventeen analysis-command methods reach sixteen active native names; steel-command creation/catalog remains pending.

Read the [architecture and implementation plan](OpenSTAAD-Commands-Multi-Version-Architecture-and-Plan.md) first. This annex records all 18 native names, all source interface overloads, declarations, logical parameter directions, return conventions, value tables, metadata and proposed managed result shapes. Native names and constants below are evidence; managed names and types are proposals requiring API review.

## 1. Counting and evidence boundaries

| Measure | 2024 | 2025 | 2026 |
| --- | --- | --- | --- |
| Group names | 18 | 18 | 18 |
| Detailed contracts | 17 | 17 | 17 |
| Native class names | 20 | 20 | 20 |
| Inspected automation methods | No installed DLL inspected | 18 | 18 |

Native class implementation/lifecycle helpers excluded from the public port: `OnFinalRelease`, `SetStructure`. Other group pages were scanned for OSCommandsUI ownership; no additional domain-owned rows were found. For Design, Output-owned functions under the Design steel page remain in the separate Output report.

Windows native long is 32-bit. VARIANT and LP(D)ISPATCH declarations do not establish a managed payload type or successful operation. Logical directions below come from parameter prose, not merely C++ reference syntax. Unmatched names and stale prose are shown explicitly; parameter positions must be reconciled against metadata and a controlled native fixture before dispatch is approved.

## 2. Complete cross-version function matrix

| Native Commands function | Native return | 2024 / 2025 / 2026 | Main native target | Current typed / plan |
| --- | --- | --- | --- | --- |
| `PerformAnalysis` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#gaf1594aca92c4a713f1ea09c381a1ea26) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#gaf1594aca92c4a713f1ea09c381a1ea26) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#gaf1594aca92c4a713f1ea09c381a1ea26) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L42) | Implemented / common service |
| `PerformPDeltaAnalysisNoConverge` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga4c29d820ed0a2df54d189ab77557399b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga4c29d820ed0a2df54d189ab77557399b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga4c29d820ed0a2df54d189ab77557399b) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L53) | Implemented / common service |
| `PerformPDeltaAnalysisEx` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga5ab6c9ae30773fe830dcf49f36baac27) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga5ab6c9ae30773fe830dcf49f36baac27) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga5ab6c9ae30773fe830dcf49f36baac27) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L66) | Implemented / common service |
| `PerformPDeltaAnalysisConverge` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#gad4c52c3aaac4cc9ab013ce9f45365662) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#gad4c52c3aaac4cc9ab013ce9f45365662) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#gad4c52c3aaac4cc9ab013ce9f45365662) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L90) | Excluded: deprecated |
| `PerformCableAnalysis` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#gae7c97c660ca1513f6a0dbac8248abec3) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#gae7c97c660ca1513f6a0dbac8248abec3) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#gae7c97c660ca1513f6a0dbac8248abec3) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L98) | Implemented / common service |
| `PerformCableAnalysisEx` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga4cca1c1583bc620304485e2cab92d20e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga4cca1c1583bc620304485e2cab92d20e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga4cca1c1583bc620304485e2cab92d20e) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L107) | Implemented / common service |
| `PerformBucklingAnalysis` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga9bcd75803c698cd18b6036f072ef72ed) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga9bcd75803c698cd18b6036f072ef72ed) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga9bcd75803c698cd18b6036f072ef72ed) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L118) | Implemented / common service |
| `PerformBucklingAnalysisEx` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga95a530e160b7f487b56c54892d273006) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga95a530e160b7f487b56c54892d273006) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga95a530e160b7f487b56c54892d273006) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L127) | Implemented / common service |
| `PerformDirectAnalysis` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga0d9299d951575f3d4ae01e251687c256) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga0d9299d951575f3d4ae01e251687c256) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga0d9299d951575f3d4ae01e251687c256) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L134) | Implemented / common service |
| `PerformNonlinearAnalysisEx` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga0eb3fa85e4d8ad3d76b302eead817a6b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga0eb3fa85e4d8ad3d76b302eead817a6b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga0eb3fa85e4d8ad3d76b302eead817a6b) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L145) | Implemented / common service |
| `SetFloorDiaphragmBaseCommand` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#gad15acf391bb459cb38a1a1ae5f49ecec) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#gad15acf391bb459cb38a1a1ae5f49ecec) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#gad15acf391bb459cb38a1a1ae5f49ecec) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L160) | Implemented / common service |
| `DeleteFloorDiaphragmBaseCommand` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#gad6e1f41f8a3be9dc64e3acbd65c03c9c) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#gad6e1f41f8a3be9dc64e3acbd65c03c9c) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#gad6e1f41f8a3be9dc64e3acbd65c03c9c) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L165) | Implemented / common service |
| `SetCheckSoftStoryCommand` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga8a44aecb38f1b524e76e133b1ebf06e5) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga8a44aecb38f1b524e76e133b1ebf06e5) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga8a44aecb38f1b524e76e133b1ebf06e5) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L171) | Implemented / common service |
| `DeleteCheckSoftStoryCommand` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga576c6058cc27d33d70ca234ee4d22315) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga576c6058cc27d33d70ca234ee4d22315) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga576c6058cc27d33d70ca234ee4d22315) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L176) | Implemented / common service |
| `SetCheckIrregularitiesCommand` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga0ac14e3de0cd68103d7a0b209dc8de55) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga0ac14e3de0cd68103d7a0b209dc8de55) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga0ac14e3de0cd68103d7a0b209dc8de55) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L182) | Implemented / common service |
| `DeleteCheckIrregularitiesCommand` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga87b66160d1f73f60ae616c57701d14c4) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga87b66160d1f73f60ae616c57701d14c4) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga87b66160d1f73f60ae616c57701d14c4) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L187) | Implemented / common service |
| `DeleteAllAnalysisCommands` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga73e72f489e9abc390958aa81fd300b72) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga73e72f489e9abc390958aa81fd300b72) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga73e72f489e9abc390958aa81fd300b72) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L192) | Implemented / common service |
| `CreateSteelDesignCommand` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___steel_design_command_group.html#gad6bd6feeb21c0ce8fced435b2cc6a158) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___steel_design_command_group.html#gad6bd6feeb21c0ce8fced435b2cc6a158) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___steel_design_command_group.html#gad6bd6feeb21c0ce8fced435b2cc6a158) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L244) | Pending / C4 |

## 3. Per-function contracts and managed proposals

<a id="commands-performanalysis"></a>

### 1. PerformAnalysis

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#gaf1594aca92c4a713f1ea09c381a1ea26) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#gaf1594aca92c4a713f1ea09c381a1ea26) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#gaf1594aca92c4a713f1ea09c381a1ea26)<br>**Current:** Implemented through the common CommandsService; see the current guide for exact managed contracts.

**Managed proposal:** `CommandSubmissionResult PerformAnalysis; expose named scalar options from the parameter table, using verified enums and bool flags.`

#### 2024 contract

~~~cpp
void OSCommandsUI::PerformAnalysis(const VARIANT FAR&PrintOption)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `PrintOption` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `iPrintOption` | [in] | Option index for specifying the print option: [value table below] |

**Contract, including return/status semantics:** Assigns the commands required to perform a FIRST ORDER ELASTIC ANALYSIS of the current structure with specified print option. Parameters [in] iPrintOption Option index for specifying the print option: Value Print Option 1 Print Load Data 2 Print Statics Check 3 Print Statics Load 4 Print Mode Shapes 5 Print Both 6 Print All 0 No Print C++ Syntax

**Value table 1** (source column groupings retained):

| Value | Print Option |
| --- | --- |
| 1 | Print Load Data |
| 2 | Print Statics Check |
| 3 | Print Statics Load |
| 4 | Print Mode Shapes |
| 5 | Print Both |
| 6 | Print All |
| 0 | No Print |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void PerformAnalysis(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [PerformAnalysis](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L42)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="commands-performpdeltaanalysisnoconverge"></a>

### 2. PerformPDeltaAnalysisNoConverge

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga4c29d820ed0a2df54d189ab77557399b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga4c29d820ed0a2df54d189ab77557399b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga4c29d820ed0a2df54d189ab77557399b)<br>**Current:** Implemented through the common CommandsService; see the current guide for exact managed contracts.

**Managed proposal:** `CommandSubmissionResult PerformPDeltaAnalysisNoConverge; expose named scalar options from the parameter table, using verified enums and bool flags.`

#### 2024 contract

~~~cpp
void OSCommandsUI::PerformPDeltaAnalysisNoConverge(const VARIANT FAR&NoOfIterations,const VARIANT FAR&PrintOption)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `NoOfIterations` | `const VARIANT FAR&` | [in] | Desired number of iterations. |
| `PrintOption` | `const VARIANT FAR&` | [in] | Option for specifying the print option. Option should be specified in accordance with below table: [value table below] |

**Contract, including return/status semantics:** Creates a command for performing PDELTA ANALYSIS with SmallDelta option. Parameters [in] NoOfIterations Desired number of iterations. [in] PrintOption Option for specifying the print option. Option should be specified in accordance with below table: Value Print Option 1 Print Load Data 2 Print Statics Check 3 Print Statics Load 4 Print Mode Shapes 5 Print Both 6 Print All 0 No Print C++ Syntax VBA Syntax See also OSCommandsUI::PerformPDeltaAnalysisEx

**Value table 1** (source column groupings retained):

| Value | Print Option |
| --- | --- |
| 1 | Print Load Data |
| 2 | Print Statics Check |
| 3 | Print Statics Load |
| 4 | Print Mode Shapes |
| 5 | Print Both |
| 6 | Print All |
| 0 | No Print |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void PerformPDeltaAnalysisNoConverge(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [PerformPDeltaAnalysisNoConverge](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L53)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="commands-performpdeltaanalysisex"></a>

### 3. PerformPDeltaAnalysisEx

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga5ab6c9ae30773fe830dcf49f36baac27) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga5ab6c9ae30773fe830dcf49f36baac27) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga5ab6c9ae30773fe830dcf49f36baac27)<br>**Current:** Implemented through the common CommandsService; see the current guide for exact managed contracts.

**Managed proposal:** `CommandSubmissionResult PerformPDeltaAnalysisEx(PDeltaAnalysisOptions options); reuse audited source option classes.`

#### 2024 contract

~~~cpp
void OSCommandsUI::PerformPDeltaAnalysisEx(const VARIANT FAR&NoOfIterations,const VARIANT FAR&PrintOption,const VARIANT FAR&bSmallDelta,const VARIANT FAR&bKG)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `NoOfIterations` | `const VARIANT FAR&` | [in] | Desired number of iterations. |
| `PrintOption` | `const VARIANT FAR&` | [in] | Option for specifying the print option. Option should be specified in accordance with below table: [value table below] |
| `bSmallDelta` | `const VARIANT FAR&` | [in] | 1 = Use P-SMALL-Delta effect, 0 = Use P-LARGE-Delta effect. |
| `bKG` | `const VARIANT FAR&` | [in] | Add geometric stiffness( K g ) to the Stiffness Matrix( K ): 1 = TRUE or 0 = FALSE. |

**Contract, including return/status semantics:** Creates a command for performing PDELTA ANALYSIS. Parameters [in] NoOfIterations Desired number of iterations. [in] PrintOption Option for specifying the print option. Option should be specified in accordance with below table: Value Print Option 1 Print Load Data 2 Print Statics Check 3 Print Statics Load 4 Print Mode Shapes 5 Print Both 6 Print All 0 No Print [in] bSmallDelta 1 = Use P-SMALL-Delta effect, 0 = Use P-LARGE-Delta effect. [in] bKG Add geometric stiffness( K g ) to the Stiffness Matrix( K ): 1 = TRUE or 0 = FALSE. C++ Syntax VBA Syntax See also OSCommandsUI::PerformPDeltaAnalysisNoConverge

**Value table 1** (source column groupings retained):

| Value | Print Option |
| --- | --- |
| 1 | Print Load Data |
| 2 | Print Statics Check |
| 3 | Print Statics Load |
| 4 | Print Mode Shapes |
| 5 | Print Both |
| 6 | Print All |
| 0 | No Print |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void PerformPDeltaAnalysisEx(System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [PerformPDeltaAnalysisEx](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L66)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="commands-performpdeltaanalysisconverge"></a>

### 4. PerformPDeltaAnalysisConverge

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#gad4c52c3aaac4cc9ab013ce9f45365662) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#gad4c52c3aaac4cc9ab013ce9f45365662) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#gad4c52c3aaac4cc9ab013ce9f45365662)<br>**Current:** Excluded from new APIs (deprecated)..

**Managed proposal:** `Exclude from new public API: deprecated native entry; use the documented P-Delta alternatives.`

#### 2024 contract

~~~cpp
void OSCommandsUI::PerformPDeltaAnalysisConverge(const VARIANT FAR&NoOfIterations,const VARIANT FAR&PrintOption)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `NoOfIterations` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `PrintOption` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Contract, including return/status semantics:** This API has been deprecated. Please use the APIs OSCommandsUI::PerformPDeltaAnalysisNoConverge or OSCommandsUI::PerformPDeltaAnalysisEx instead.

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void PerformPDeltaAnalysisConverge(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [PerformPDeltaAnalysisConverge](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L90)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="commands-performcableanalysis"></a>

### 5. PerformCableAnalysis

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#gae7c97c660ca1513f6a0dbac8248abec3) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#gae7c97c660ca1513f6a0dbac8248abec3) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#gae7c97c660ca1513f6a0dbac8248abec3)<br>**Current:** Implemented through the common CommandsService; see the current guide for exact managed contracts.

**Managed proposal:** `CommandSubmissionResult PerformCableAnalysis; expose named scalar options from the parameter table, using verified enums and bool flags.`

#### 2024 contract

~~~cpp
void OSCommandsUI::PerformCableAnalysis(const VARIANT FAR&iNoOfIterations,const VARIANT FAR&iPrintOption)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `iNoOfIterations` | `const VARIANT FAR&` | [in] | Desired number of iterations. |
| `iPrintOption` | `const VARIANT FAR&` | [in] | Option for specifying the print option. Option should be specified in accordance with below table: [value table below] |

**Contract, including return/status semantics:** Creates commands required to perform a CABLE ANALYSIS. This requires the presence of cable members in the structure. Advanced algorithm will be used only if Advanced license is enabled, else basic algorithm will be used. For further details, please check TR.37.3 of STAAD.Pro Help manual. Parameters [in] iNoOfIterations Desired number of iterations. [in] iPrintOption Option for specifying the print option. Option should be specified in accordance with below table: Value Print Option 1 Print Load Data 2 Print Statics Check 3 Print Statics Load 5 Print Both 6 Print All 0 No Print C++ Syntax VBA Syntax

**Value table 1** (source column groupings retained):

| Value | Print Option |
| --- | --- |
| 1 | Print Load Data |
| 2 | Print Statics Check |
| 3 | Print Statics Load |
| 5 | Print Both |
| 6 | Print All |
| 0 | No Print |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void PerformCableAnalysis(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [PerformCableAnalysis](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L98)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="commands-performcableanalysisex"></a>

### 6. PerformCableAnalysisEx

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga4cca1c1583bc620304485e2cab92d20e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga4cca1c1583bc620304485e2cab92d20e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga4cca1c1583bc620304485e2cab92d20e)<br>**Current:** Implemented through the common CommandsService; see the current guide for exact managed contracts.

**Managed proposal:** `CommandSubmissionResult PerformCableAnalysisEx(CableAnalysisOptions options); reuse audited source option classes.`

#### 2024 contract

~~~cpp
VARIANT OSCommandsUI::PerformCableAnalysisEx(const VARIANT FAR&iAdvancedCableAnalysis,const VARIANT FAR&varAdvOptions,const VARIANT FAR&varParams,const VARIANT FAR&PrintOption)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `iAdvancedCableAnalysis` | `const VARIANT FAR&` | [in] | Is Advanced Cable Analysis to perform (= TRUE/FALSE ) |
| `varAdvOptions` | `const VARIANT FAR&` | [in] | The additional options for Advanced Cable Analysis stored in a BOOL/short/int array object. [value table below] User can supply NULL or an array of any size. For Advanced Cable Analysis, if NULL is supplied both these values will be assumed as FALSE If array of size=1 is supplied the second value will be assumed as FALSE If array of size&gt;2 is supplied all the values except the first two will be ignored |
| `varParams` | `const VARIANT FAR&` | [in] | The additional parameters required for Cable Analysis stored in a double array object. Some of these values are used to represent integer value. For basic cable analysis: - [value table below] For Advanced cable analysis: - [value table below] |
| `PrintOption` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `iPrintOption` | [in] | Option index for specifying the print option: [value table below] |

**Contract, including return/status semantics:** Assigns the commands required to perform a CABLE ANALYSIS on the model. This requires the presence of nonlinear cables in the structure. Parameters [in] iAdvancedCableAnalysis Is Advanced Cable Analysis to perform (= TRUE/FALSE ) [in] varAdvOptions The additional options for Advanced Cable Analysis stored in a BOOL/short/int array object. Array Index Default STAAD Command Options 0 1 REFORM f11 Use Full Newton-Raphson method? (0 = FALSE ; 1 = TRUE ) 1 0 KGEOM f12 Use Geometric Matrix (Kg)? (0 = FALSE ; 1 = TRUE ) User can supply NULL or an array of any size. For Advanced Cable Analysis, if NULL is supplied both these values will be assumed as FALSE If array of size=1 is supplied the second value will be assumed as FALSE If array of size&gt;2 is supplied all the values except the first two will be ignored [in] varParams The additional parameters required for Cable Analysis stored in a double array object. Some of these values are used to represent integer value. For basic cable analysis: - Array Index Default value STAAD Command Parameters 0 145 STEPS f1 The number of load steps. The applied loads will be applied gradually in this many steps. Each step will be iterated to convergence. Should be in the range 5 to 145, with 145 as the default number. 1 300 EQITERATIONS f2 Maximum number of iterations permitted in each load step. Should be in the range of 10 to 500. 2 1.0E-4 EQTOLERANCE f3 The convergence tolerance for the above iterations. 3 0.0 SAGMINIMUM f4 Sag Minimum. Default is 0.0. 4 1.0 STABILITY f5 A stiffness matrix value to be added to the global matrix at each translational direction for joints connected to cables and nonlinear trusses for the first (f6) Load Steps. Should be within the range of 0.0 to 1000.0. 5 1 f6 The number of load steps over which the Stability stiffness matrix(f5) is gradually applied. 6 0.0 KSMALL f7 A stiffness matrix value to be added to the global matrix at each translational direction for joints connected to cables and nonlinear trusses for every load step. Should be within the range of 0.0 and 1.0. For Advanced cable analysis: - Array Index Default value STAAD Command Parameters 0 1 STEPS f1 Used to divide the loading into small increments. Any positive integer is valid. Generally, a value larger than one (1) may be beneficial for solution convergence, because the loading is applied gradually. However, with cable elements having fixed prestressing, a single step typical converges faster. Default is 1. 1 300 EQITERATIONS f2 Maximum number of iterations permitted in each load step. During each loading increment, the analysis will iterate to find the converging solution. Any positive integer is valid. Values too small may prevent the solution to achieve convergence. However, excessively large values may cost unnecessary running time when the problem diverges. 2 1.0E-6 EQTOLERANCE f3 The convergence tolerance for the iterations specified in EQITERATIONS. The residual force norm is used for this convergence. This error threshold indicates when the nonlinear solver will stop iterating and consider the ongoing step as converged once the computed error is equal or smaller than this value. A value that is too small may prevent the solution to be considered as converged. However, value that is too larger may result in inaccurate results. [in] iPrintOption Option index for specifying the print option: Value Print Option 1 Print Load Data 2 Print Statics Check 3 Print Statics Load 5 Print Both 6 Print All 0 No Print Return values TRUE(1) OK. FALSE(0) Failed to add or update. C++ Syntax C# Syntax VB Syntax See also OSCommandsUI::CreateNewCableAnalysisBlock

**Value table 1** (source column groupings retained):

| Array Index | Default | STAAD Command | Options |
| --- | --- | --- | --- |
| 0 | 1 | REFORM f11 | Use Full Newton-Raphson method? (0 = FALSE ; 1 = TRUE ) |
| 1 | 0 | KGEOM f12 | Use Geometric Matrix (Kg)? (0 = FALSE ; 1 = TRUE ) |

**Value table 2** (source column groupings retained):

| Array Index | Default value | STAAD Command | Parameters |
| --- | --- | --- | --- |
| 0 | 145 | STEPS f1 | The number of load steps. The applied loads will be applied gradually in this many steps. Each step will be iterated to convergence. Should be in the range 5 to 145, with 145 as the default number. |
| 1 | 300 | EQITERATIONS f2 | Maximum number of iterations permitted in each load step. Should be in the range of 10 to 500. |
| 2 | 1.0E-4 | EQTOLERANCE f3 | The convergence tolerance for the above iterations. |
| 3 | 0.0 | SAGMINIMUM f4 | Sag Minimum. Default is 0.0. |
| 4 | 1.0 | STABILITY f5 | A stiffness matrix value to be added to the global matrix at each translational direction for joints connected to cables and nonlinear trusses for the first (f6) Load Steps. Should be within the range of 0.0 to 1000.0. |
| 5 | 1 | f6 | The number of load steps over which the Stability stiffness matrix(f5) is gradually applied. |
| 6 | 0.0 | KSMALL f7 | A stiffness matrix value to be added to the global matrix at each translational direction for joints connected to cables and nonlinear trusses for every load step. Should be within the range of 0.0 and 1.0. |

**Value table 3** (source column groupings retained):

| Array Index | Default value | STAAD Command | Parameters |
| --- | --- | --- | --- |
| 0 | 1 | STEPS f1 | Used to divide the loading into small increments. Any positive integer is valid. Generally, a value larger than one (1) may be beneficial for solution convergence, because the loading is applied gradually. However, with cable elements having fixed prestressing, a single step typical converges faster. Default is 1. |
| 1 | 300 | EQITERATIONS f2 | Maximum number of iterations permitted in each load step. During each loading increment, the analysis will iterate to find the converging solution. Any positive integer is valid. Values too small may prevent the solution to achieve convergence. However, excessively large values may cost unnecessary running time when the problem diverges. |
| 2 | 1.0E-6 | EQTOLERANCE f3 | The convergence tolerance for the iterations specified in EQITERATIONS. The residual force norm is used for this convergence. This error threshold indicates when the nonlinear solver will stop iterating and consider the ongoing step as converged once the computed error is equal or smaller than this value. A value that is too small may prevent the solution to be considered as converged. However, value that is too larger may result in inaccurate results. |

**Value table 4** (source column groupings retained):

| Value | Print Option |
| --- | --- |
| 1 | Print Load Data |
| 2 | Print Statics Check |
| 3 | Print Statics Load |
| 5 | Print Both |
| 6 | Print All |
| 0 | No Print |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object PerformCableAnalysisEx(System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [PerformCableAnalysisEx](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L107)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="commands-performbucklinganalysis"></a>

### 7. PerformBucklingAnalysis

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga9bcd75803c698cd18b6036f072ef72ed) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga9bcd75803c698cd18b6036f072ef72ed) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga9bcd75803c698cd18b6036f072ef72ed)<br>**Current:** Implemented through the common CommandsService; see the current guide for exact managed contracts.

**Managed proposal:** `CommandSubmissionResult PerformBucklingAnalysis; expose named scalar options from the parameter table, using verified enums and bool flags.`

#### 2024 contract

~~~cpp
void OSCommandsUI::PerformBucklingAnalysis(const VARIANT FAR&iMaxSteps,const VARIANT FAR&iPrintOption)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `iMaxSteps` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `iPrintOption` | `const VARIANT FAR&` | [in] | Option for specifying the print option. Option should be specified in accordance with below table: [value table below] |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `iNoOfIterations` | [in] | Maximum no. of iterations desired |

**Contract, including return/status semantics:** Creates commands required to perform BUCKLING ANALYSIS. Parameters [in] iNoOfIterations Maximum no. of iterations desired [in] iPrintOption Option for specifying the print option. Option should be specified in accordance with below table: Value Print Option 1 Print Load Data 2 Print Statics Check 3 Print Statics Load 5 Print Both 6 Print All 0 No Print C++ Syntax VBA Syntax

**Value table 1** (source column groupings retained):

| Value | Print Option |
| --- | --- |
| 1 | Print Load Data |
| 2 | Print Statics Check |
| 3 | Print Statics Load |
| 5 | Print Both |
| 6 | Print All |
| 0 | No Print |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void PerformBucklingAnalysis(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [PerformBucklingAnalysis](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L118)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="commands-performbucklinganalysisex"></a>

### 8. PerformBucklingAnalysisEx

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga95a530e160b7f487b56c54892d273006) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga95a530e160b7f487b56c54892d273006) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga95a530e160b7f487b56c54892d273006)<br>**Current:** Implemented through the common CommandsService; see the current guide for exact managed contracts.

**Managed proposal:** `CommandSubmissionResult PerformBucklingAnalysisEx(BucklingAnalysisOptions options); reuse audited source option classes.`

#### 2024 contract

~~~cpp
VARIANT OSCommandsUI::PerformBucklingAnalysisEx(const VARIANT FAR&Method,const VARIANT FAR&MaxSteps,const VARIANT FAR&PrintOption)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `Method` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `MaxSteps` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `PrintOption` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `method` | [in] | Buckling Analysis method using enum type BucklingAnalysisMethod. [value table below] |
| `iMaxSteps` | [in] | Maximum no. of iterations desired (default value of n = 10). 15 is recommended. Used for Basic Solver, only. |
| `iPrintOption` | [in] | Option index for specifying the print option: [value table below] |

**Contract, including return/status semantics:** Assigns the commands required to perform a BUCKLING ANALYSIS on the model. This requires the presence of nonlinear cables in the structure. Parameters [in] method Buckling Analysis method using enum type BucklingAnalysisMethod. Value Method Iterative = 0 BucklingAnalysisMethod.Iterative Eigen = 1 BucklingAnalysisMethod.Eigen [in] iMaxSteps Maximum no. of iterations desired (default value of n = 10). 15 is recommended. Used for Basic Solver, only. [in] iPrintOption Option index for specifying the print option: Value Print Option 1 Print Load Data 2 Print Statics Check 3 Print Statics Load 4 Reserved, do not use 5 Print Both 6 Print All 0 No Print Return values TRUE(1) OK. FALSE(0) Failed to add or update. C++ Syntax C# Syntax VB Syntax

**Value table 1** (source column groupings retained):

| Value | Method |
| --- | --- |
| Iterative = 0 | BucklingAnalysisMethod.Iterative |
| Eigen = 1 | BucklingAnalysisMethod.Eigen |

**Value table 2** (source column groupings retained):

| Value | Print Option |
| --- | --- |
| 1 | Print Load Data |
| 2 | Print Statics Check |
| 3 | Print Statics Load |
| 4 | Reserved, do not use |
| 5 | Print Both |
| 6 | Print All |
| 0 | No Print |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object PerformBucklingAnalysisEx(System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [PerformBucklingAnalysisEx](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L127)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="commands-performdirectanalysis"></a>

### 9. PerformDirectAnalysis

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga0d9299d951575f3d4ae01e251687c256) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga0d9299d951575f3d4ae01e251687c256) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga0d9299d951575f3d4ae01e251687c256)<br>**Current:** Implemented through the common CommandsService; see the current guide for exact managed contracts.

**Managed proposal:** `CommandSubmissionResult PerformDirectAnalysis(DirectAnalysisOptions options); reuse audited source option classes.`

#### 2024 contract

~~~cpp
VARIANT OSCommandsUI::PerformDirectAnalysis(const VARIANT FAR&Option,const VARIANT FAR&varParams,const VARIANT FAR&varAddOptions,const VARIANT FAR&PrintOption)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `Option` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `varParams` | `const VARIANT FAR&` | [in] | The additional parameters required for Direct Analysis stored in a double array object. Some of these values are used to represent integer value. [value table below] |
| `varAddOptions` | `const VARIANT FAR&` | [in] | The additional options for Direct Analysis stored in a BOOL/short/int array object. [value table below] User can supply NULL or an array of any size. if NULL is supplied both these values will be assumed as FALSE If array of size=1 is supplied the second value will be assumed as FALSE If array of size&gt;2 is supplied all the values except the first two will be ignored |
| `PrintOption` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `option` | [in] | Direct Analysis option (LRFD/ASD) using enum type DirectAnalysisOption. Default is 1 (LRFD). [value table below] |
| `iPrintOption` | [in] | Option index for specifying the print option: [value table below] |

**Contract, including return/status semantics:** Assigns the commands required to perform a DIRECT ANALYSIS for AISC on the model. Parameters [in] option Direct Analysis option (LRFD/ASD) using enum type DirectAnalysisOption. Default is 1 (LRFD). Value Method LRFD = 1 DirectAnalysisOption.LRFD ASD = 2 DirectAnalysisOption.ASD [in] varParams The additional parameters required for Direct Analysis stored in a double array object. Some of these values are used to represent integer value. Array Index Default value STAAD Command Parameters 0 0.01 TAUTOL f1 Tau-b tolerance. Is normally 0.001 to 1.0. 1 0.01 DISPTOL f2 Displacement tolerance - 0.01 in (displacement) 0.01 radians (rotation) The value should not be too tight. 2 1 ITERDIRECT i3 Limits the number of iterations. A value between 1 to 10 is typically sufficient. 3 15 PDiter i5 The number of iterations, used in the iterative PDelta with SmallDelta analysis procedure within Direct Analysis. 5 to 15 iterations is the normal range. Any value more than 15 will reset to 15. [in] varAddOptions The additional options for Direct Analysis stored in a BOOL/short/int array object. Array Index Default STAAD Command Options 0 0 REDUCEDEI i4 whether to use the reduced EI (Tau-b * 0.8 * EI) for member section moment and section displacement? (0 = FALSE (full EI); 1 = TRUE (reduced EI)) 1 0 TBITER Iterate Tau-b? (0 = FALSE ; 1 = TRUE ) User can supply NULL or an array of any size. if NULL is supplied both these values will be assumed as FALSE If array of size=1 is supplied the second value will be assumed as FALSE If array of size&gt;2 is supplied all the values except the first two will be ignored [in] iPrintOption Option index for specifying the print option: Value Print Option 1 Print Load Data 2 Print Statics Check 3 Print Statics Load 4 Print Mode Shapes 5 Print Both 6 Print All 0 No Print Return values TRUE(1) OK. FALSE(0) Failed to add or update. C++ Syntax C# Syntax VB Syntax

**Value table 1** (source column groupings retained):

| Value | Method |
| --- | --- |
| LRFD = 1 | DirectAnalysisOption.LRFD |
| ASD = 2 | DirectAnalysisOption.ASD |

**Value table 2** (source column groupings retained):

| Array Index | Default value | STAAD Command | Parameters |
| --- | --- | --- | --- |
| 0 | 0.01 | TAUTOL f1 | Tau-b tolerance. Is normally 0.001 to 1.0. |
| 1 | 0.01 | DISPTOL f2 | Displacement tolerance - 0.01 in (displacement) 0.01 radians (rotation) The value should not be too tight. |
| 2 | 1 | ITERDIRECT i3 | Limits the number of iterations. A value between 1 to 10 is typically sufficient. |
| 3 | 15 | PDiter i5 | The number of iterations, used in the iterative PDelta with SmallDelta analysis procedure within Direct Analysis. 5 to 15 iterations is the normal range. Any value more than 15 will reset to 15. |

**Value table 3** (source column groupings retained):

| Array Index | Default | STAAD Command | Options |
| --- | --- | --- | --- |
| 0 | 0 | REDUCEDEI i4 | whether to use the reduced EI (Tau-b * 0.8 * EI) for member section moment and section displacement? (0 = FALSE (full EI); 1 = TRUE (reduced EI)) |
| 1 | 0 | TBITER | Iterate Tau-b? (0 = FALSE ; 1 = TRUE ) |

**Value table 4** (source column groupings retained):

| Value | Print Option |
| --- | --- |
| 1 | Print Load Data |
| 2 | Print Statics Check |
| 3 | Print Statics Load |
| 4 | Print Mode Shapes |
| 5 | Print Both |
| 6 | Print All |
| 0 | No Print |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object PerformDirectAnalysis(System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [PerformDirectAnalysis](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L134)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="commands-performnonlinearanalysisex"></a>

### 10. PerformNonlinearAnalysisEx

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga0eb3fa85e4d8ad3d76b302eead817a6b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga0eb3fa85e4d8ad3d76b302eead817a6b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga0eb3fa85e4d8ad3d76b302eead817a6b)<br>**Current:** Implemented through the common CommandsService; see the current guide for exact managed contracts.

**Managed proposal:** `CommandSubmissionResult PerformNonlinearAnalysisEx(NonlinearAnalysisOptions options); reuse audited source option classes.`

#### 2024 contract

~~~cpp
VARIANT OSCommandsUI::PerformNonlinearAnalysisEx(const VARIANT FAR&PrintOption,const VARIANT FAR&Arclength,const VARIANT FAR&NoOfIterations,const VARIANT FAR&Tolerance,const VARIANT FAR&Steps,const VARIANT FAR&Rebuild,const VARIANT FAR&KG,const VARIANT FAR&varDispLimitData)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `PrintOption` | `const VARIANT FAR&` | [in] | Option index for specifying the print option: [value table below] |
| `Arclength` | `const VARIANT FAR&` | [in] | Absolute displacement limit (float/double) for the first analysis step for Displacement Control (= 0 for no Displacement Control). |
| `NoOfIterations` | `const VARIANT FAR&` | [in] | Upper limit for the times of iteration(s) to achieve equilibrium in the deformed position to the tolerance specified. |
| `Tolerance` | `const VARIANT FAR&` | [in] | Tolerance value (float/double) for determining convergence. |
| `Steps` | `const VARIANT FAR&` | [in] | Number of Load Step(s): load is applied in stage(s). |
| `Rebuild` | `const VARIANT FAR&` | [in] | Frequency of rebuilds of the Tangent or Stiffness Matrix(K) per load step & iteration. 0 = once per load step 1 = every load step & iteration |
| `KG` | `const VARIANT FAR&` | [in] | Add the geometric stiffness(K g ) to the Stiffness Matrix(K): TRUE or FALSE . |
| `varDispLimitData` | `const VARIANT FAR&` | [in] | The displacement limit data to specify target displacement. [value table below] |

**Contract, including return/status semantics:** Assigns the commands required to perform NONLINEAR ANALYSIS of the current structure with specified print option. Parameters [in] PrintOption Option index for specifying the print option: Value Print Option 1 Print Load Data 2 Print Statics Check 3 Print Statics Load 4 Print Mode Shapes 5 Print Both 6 Print All 0 No Print [in] Arclength Absolute displacement limit (float/double) for the first analysis step for Displacement Control (= 0 for no Displacement Control). [in] NoOfIterations Upper limit for the times of iteration(s) to achieve equilibrium in the deformed position to the tolerance specified. [in] Tolerance Tolerance value (float/double) for determining convergence. [in] Steps Number of Load Step(s): load is applied in stage(s). [in] Rebuild Frequency of rebuilds of the Tangent or Stiffness Matrix(K) per load step & iteration. 0 = once per load step 1 = every load step & iteration [in] KG Add the geometric stiffness(K g ) to the Stiffness Matrix(K): TRUE or FALSE . [in] varDispLimitData The displacement limit data to specify target displacement. Array Index Default STAAD Command Options 0 1 JOINT_TARGET i1 Joint being monitored in a displacement target analysis 1 1 i2 Global degree of freedom (1 through 6): 1:Global X 2:Global Y 3:Global Z 4:Moment about Global X 5:Moment about Global Y 6:Moment about Global Z 2 0 f1 Displacement target value in current length units. Return values TRUE(1) OK. FALSE(0) Failed to add or update. C++ Syntax # Syntax VB Syntax @ sa COSCommand::CreateNewNonlinearAnalysisBlock

**Value table 1** (source column groupings retained):

| Value | Print Option |
| --- | --- |
| 1 | Print Load Data |
| 2 | Print Statics Check |
| 3 | Print Statics Load |
| 4 | Print Mode Shapes |
| 5 | Print Both |
| 6 | Print All |
| 0 | No Print |

**Value table 2** (source column groupings retained):

| Array Index | Default | STAAD Command | Options |
| --- | --- | --- | --- |
| 0 | 1 | JOINT_TARGET i1 | Joint being monitored in a displacement target analysis |
| 1 | 1 | i2 | Global degree of freedom (1 through 6): 1:Global X 2:Global Y 3:Global Z 4:Moment about Global X 5:Moment about Global Y 6:Moment about Global Z |
| 2 | 0 | f1 | Displacement target value in current length units. |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object PerformNonlinearAnalysisEx(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [PerformNonlinearAnalysisEx](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L145)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="commands-setfloordiaphragmbasecommand"></a>

### 11. SetFloorDiaphragmBaseCommand

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#gad15acf391bb459cb38a1a1ae5f49ecec) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#gad15acf391bb459cb38a1a1ae5f49ecec) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#gad15acf391bb459cb38a1a1ae5f49ecec)<br>**Current:** Implemented through the common CommandsService; see the current guide for exact managed contracts.

**Managed proposal:** `CommandSubmissionResult SetFloorDiaphragmBaseCommand; expose named scalar options from the parameter table, using verified enums and bool flags.`

#### 2024 contract

~~~cpp
VARIANT OSCommandsUI::SetFloorDiaphragmBaseCommand(const VARIANT FAR&varValue)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varValue` | `const VARIANT FAR&` | [in] | Base elevation (Unit: Length) |

**Contract, including return/status semantics:** Add or update Base command for Floor Diaphragms. Parameters [in] varValue Base elevation (Unit: Length) Return values 1 OK. 0 Failed to add or update. C++ Syntax VBA Syntax See also OSCommandsUI::DeleteFloorDiaphragmBaseCommand OSCommandsUI::SetCheckSoftStoryCommand OSCommandsUI::DeleteCheckSoftStoryCommand OSCommandsUI::SetCheckIrregularitiesCommand OSCommandsUI::DeleteCheckIrregularitiesCommand

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object SetFloorDiaphragmBaseCommand(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetFloorDiaphragmBaseCommand](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L160)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="commands-deletefloordiaphragmbasecommand"></a>

### 12. DeleteFloorDiaphragmBaseCommand

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#gad6e1f41f8a3be9dc64e3acbd65c03c9c) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#gad6e1f41f8a3be9dc64e3acbd65c03c9c) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#gad6e1f41f8a3be9dc64e3acbd65c03c9c)<br>**Current:** Implemented through the common CommandsService; see the current guide for exact managed contracts.

**Managed proposal:** `CommandSubmissionResult DeleteFloorDiaphragmBaseCommand; expose named scalar options from the parameter table, using verified enums and bool flags.`

#### 2024 contract

~~~cpp
VARIANT OSCommandsUI::DeleteFloorDiaphragmBaseCommand()
~~~

No native parameters.

**Contract, including return/status semantics:** Delete existing Base command for Floor Diaphragms. Return values 1 OK. 0 Failed to delete. C++ Syntax VBA Syntax See also OSCommandsUI::SetFloorDiaphragmBaseCommand OSCommandsUI::SetCheckSoftStoryCommand OSCommandsUI::DeleteCheckSoftStoryCommand OSCommandsUI::SetCheckIrregularitiesCommand OSCommandsUI::DeleteCheckIrregularitiesCommand

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object DeleteFloorDiaphragmBaseCommand()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [DeleteFloorDiaphragmBaseCommand](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L165)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="commands-setchecksoftstorycommand"></a>

### 13. SetCheckSoftStoryCommand

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga8a44aecb38f1b524e76e133b1ebf06e5) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga8a44aecb38f1b524e76e133b1ebf06e5) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga8a44aecb38f1b524e76e133b1ebf06e5)<br>**Current:** Implemented through the common CommandsService; see the current guide for exact managed contracts.

**Managed proposal:** `CommandSubmissionResult SetCheckSoftStoryCommand; expose named scalar options from the parameter table, using verified enums and bool flags.`

#### 2024 contract

~~~cpp
VARIANT OSCommandsUI::SetCheckSoftStoryCommand(const VARIANT FAR&varDesignCode)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varDesignCode` | `const VARIANT FAR&` | unspecified | Option index for specifying code to be used: [value table below] |

**Contract, including return/status semantics:** Add or update Check Soft Story command. Parameters varDesignCode Option index for specifying code to be used: Value Code 1 IS1893 2002 2 ASCE7 05/10/16 3 IS1893 2016 Return values 1 OK. 0 Failed to add or update. C++ Syntax VBA Syntax See also OSCommandsUI::DeleteCheckSoftStoryCommand OSCommandsUI::SetFloorDiaphragmBaseCommand OSCommandsUI::DeleteFloorDiaphragmBaseCommand OSCommandsUI::SetCheckIrregularitiesCommand OSCommandsUI::DeleteCheckIrregularitiesCommand

**Value table 1** (source column groupings retained):

| Value | Code |
| --- | --- |
| 1 | IS1893 2002 |
| 2 | ASCE7 05/10/16 |
| 3 | IS1893 2016 |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object SetCheckSoftStoryCommand(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetCheckSoftStoryCommand](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L171)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="commands-deletechecksoftstorycommand"></a>

### 14. DeleteCheckSoftStoryCommand

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga576c6058cc27d33d70ca234ee4d22315) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga576c6058cc27d33d70ca234ee4d22315) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga576c6058cc27d33d70ca234ee4d22315)<br>**Current:** Implemented through the common CommandsService; see the current guide for exact managed contracts.

**Managed proposal:** `CommandSubmissionResult DeleteCheckSoftStoryCommand; expose named scalar options from the parameter table, using verified enums and bool flags.`

#### 2024 contract

~~~cpp
VARIANT OSCommandsUI::DeleteCheckSoftStoryCommand()
~~~

No native parameters.

**Contract, including return/status semantics:** Delete existing Check Soft Story command. Return values 1 OK. 0 Failed to delete. C++ Syntax VBA Syntax See also OSCommandsUI::SetCheckSoftStoryCommand OSCommandsUI::SetFloorDiaphragmBaseCommand OSCommandsUI::DeleteFloorDiaphragmBaseCommand OSCommandsUI::SetCheckIrregularitiesCommand OSCommandsUI::DeleteCheckIrregularitiesCommand

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object DeleteCheckSoftStoryCommand()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [DeleteCheckSoftStoryCommand](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L176)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="commands-setcheckirregularitiescommand"></a>

### 15. SetCheckIrregularitiesCommand

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga0ac14e3de0cd68103d7a0b209dc8de55) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga0ac14e3de0cd68103d7a0b209dc8de55) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga0ac14e3de0cd68103d7a0b209dc8de55)<br>**Current:** Implemented through the common CommandsService; see the current guide for exact managed contracts.

**Managed proposal:** `CommandSubmissionResult SetCheckIrregularitiesCommand; expose named scalar options from the parameter table, using verified enums and bool flags.`

#### 2024 contract

~~~cpp
VARIANT OSCommandsUI::SetCheckIrregularitiesCommand(const VARIANT FAR&varDesignCode)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varDesignCode` | `const VARIANT FAR&` | unspecified | Option index for specifying code to be used: [value table below] |

**Contract, including return/status semantics:** Add or update Check Irregularities command. This will work when Floor Diaphragm data is available in the model. Parameters varDesignCode Option index for specifying code to be used: Value Code 2 ASCE 7 2016 3 IS1893 2016 Return values 1 OK. 0 Failed to add or update. C++ Syntax VBA Syntax See also OSCommandsUI::DeleteCheckIrregularitiesCommand OSCommandsUI::SetFloorDiaphragmBaseCommand OSCommandsUI::DeleteFloorDiaphragmBaseCommand OSCommandsUI::SetCheckSoftStoryCommand OSCommandsUI::DeleteCheckSoftStoryCommand

**Value table 1** (source column groupings retained):

| Value | Code |
| --- | --- |
| 2 | ASCE 7 2016 |
| 3 | IS1893 2016 |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object SetCheckIrregularitiesCommand(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetCheckIrregularitiesCommand](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L182)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="commands-deletecheckirregularitiescommand"></a>

### 16. DeleteCheckIrregularitiesCommand

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga87b66160d1f73f60ae616c57701d14c4) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga87b66160d1f73f60ae616c57701d14c4) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga87b66160d1f73f60ae616c57701d14c4)<br>**Current:** Implemented through the common CommandsService; see the current guide for exact managed contracts.

**Managed proposal:** `CommandSubmissionResult DeleteCheckIrregularitiesCommand; expose named scalar options from the parameter table, using verified enums and bool flags.`

#### 2024 contract

~~~cpp
VARIANT OSCommandsUI::DeleteCheckIrregularitiesCommand()
~~~

No native parameters.

**Contract, including return/status semantics:** Delete existing Check Irregularities command. Return values 1 OK. 0 Failed to delete. C++ Syntax VBA Syntax See also OSCommandsUI::SetCheckIrregularitiesCommand OSCommandsUI::SetFloorDiaphragmBaseCommand OSCommandsUI::DeleteFloorDiaphragmBaseCommand OSCommandsUI::SetCheckSoftStoryCommand OSCommandsUI::DeleteCheckSoftStoryCommand

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object DeleteCheckIrregularitiesCommand()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [DeleteCheckIrregularitiesCommand](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L187)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="commands-deleteallanalysiscommands"></a>

### 17. DeleteAllAnalysisCommands

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html#ga73e72f489e9abc390958aa81fd300b72) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html#ga73e72f489e9abc390958aa81fd300b72) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html#ga73e72f489e9abc390958aa81fd300b72)<br>**Current:** Implemented through the common CommandsService; see the current guide for exact managed contracts.

**Managed proposal:** `CommandSubmissionResult DeleteAllAnalysisCommands; expose named scalar options from the parameter table, using verified enums and bool flags.`

#### 2024 contract

~~~cpp
VARIANT OSCommandsUI::DeleteAllAnalysisCommands()
~~~

No native parameters.

**Contract, including return/status semantics:** Delete all existing analysis commands. Return values 1 OK. 0 Perform analysis commands not present C++ Syntax C# Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object DeleteAllAnalysisCommands()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [DeleteAllAnalysisCommands](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L192)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="commands-createsteeldesigncommand"></a>

### 18. CreateSteelDesignCommand

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___steel_design_command_group.html#gad6bd6feeb21c0ce8fced435b2cc6a158) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___steel_design_command_group.html#gad6bd6feeb21c0ce8fced435b2cc6a158) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___steel_design_command_group.html#gad6bd6feeb21c0ce8fced435b2cc6a158)<br>**Current:** Pending; C4.

**Managed proposal:** `CommandSubmissionResult CreateSteelDesignCommand(SteelDesignCommandRequest request); preserve submission status and catalog identity.`

#### 2024 contract

~~~cpp
void OSCommandsUI::CreateSteelDesignCommand(const VARIANT FAR&NDesignCode,const VARIANT FAR&NCommandNo,const VARIANT FAR&IntValues,const VARIANT FAR&FloatValues,const VARIANT FAR&StringValues,const VARIANT FAR&NAssignList)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `NDesignCode` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `NCommandNo` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `IntValues` | `const VARIANT FAR&` | [in] | Integer Parameter(s) VARIANT array. |
| `FloatValues` | `const VARIANT FAR&` | [in] | Float Parameter(s) VARIANT array. |
| `StringValues` | `const VARIANT FAR&` | [in] | String Parameter(s) VARIANT array. |
| `NAssignList` | `const VARIANT FAR&` | [in] | Reference ID(s) list of Object(s) to be assigned. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `iDesignCode` | [in] | Index of Design Code. [value table below] |
| `iDesignCommandNo` | [in] | Index of Design Command. Table 1/10 [value table below] |

**Contract, including return/status semantics:** Creates steel design command. Parameters [in] iDesignCode Index of Design Code. iDesignCode Description 1001 AASHTO ASD 1002 AISC ASD 1003 AS 4100-1998 1004 BS 5950-1:2000 1005 BS 5400:Part 3:1982 1006 CAN/CSA-S16-01 1007 French CM66 1977 1008 DIN 18 800 Part 1 1009 IS 800 1984, ASD 1010 Japan AIJ 2002 1011 AISC LRFD 1012 Norway NS 3472 2001 1014 Norway NPD 1993 1016 API 2A-WSD 1020 ASCE 10-97 1025 Russia SNiP 2.23-81* 1990 1027 Canada S136-94 1028 IS 801 1975 1029 IS 802 1995 1030 Mexico NTC 1987 1032 IS 800 2007, LSD 1052 IS 800 2007, WSD 1033 BS 5950-5:1998 1034 South Africa SANS 10162-1:2011 1044 AASHTO LRFD 1045 ANSI/AISC 360-05 1046 ASME NF 3000 1989 1047 ASME NF 3000 1998 1048 ASME NF 3000 1974 1049 ASME NF 3000 1977 1050 Norway NORSOK N-004 1051 ASME NF 3000 2004 1053 ASME NF 3000 2001 1060 ASCE 52 1061 ANSI/AISC 360-10 1062 Canadian S16_09 1063 Russia SP 16.13330.2011 1064 South Africa SANS10162-1:1993 1065 Canadian S16_14 1066 NZS3404_1997 1067 ANSI/AISC 360-16 1068 Aisi_S100_2016 1069 Canadian S16_19 1102 AISC CASTELLATED 1202 AISC N690 1994 1204 AISC N690 1984 1210 Japan AIJ 2005 1220 BS EN 1993-1-1:2005 1221 Russia SP 16.13330.2017 [in] iDesignCommandNo Index of Design Command. Table 1/10 Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo AISC 360-05, 360-10, and 360-16 AISC LRFD (2nd and 3rd Ed.) AASHTO (ASD) AASHTO (LRFD) CODE 9010 CODE 9010 CODE 9010 CODE 9010 ALH 9674 AXIS 9892 BEAM 9380 BEAM 9380 BEAM 9380 BEAM 9380 CB 9280 DMAX 9160 BRC 9955 CAN 9883 CMY 9310 DMIN 9170 CAN 9883 CB 9280 CMZ 9320 DFF 9210 CB 9280 DFF 9210 DFF 9210 DJ1 9390 CSPACING 9103 DJ1 9390 DJ1 9390 DJ2 9400 DFF 9210 DJ2 9400 DJ2 9400 GRADE 9580 DJ1 9390 DMAX 9160 DMAX 9160 KY 9240 DJ2 9400 DMIN 9170 DMIN 9170 KZ 9250 DMAX 9160 FLX 9781 FYLD 9100 LY 9130 DMIN 9170 FYLD 9100 KY 9240 LZ 9140 DUCT 9964 FU 9705 KZ 9250 MAIN 9330 E5P 9133 KX 9235 LY 9130 NSF 9260 FLX 9781 KY 9240 LZ 9140 TRACK 9350 FRM 9954 KZ 9250 MAIN 9330 UNB 9660 FU 9705 LX 9125 NSF 9260 UNT 9650 FYLD 9100 LY 9130 PROFILE 9260 IMM 9877 LZ 9140 PUNCH 9340 KX 9235 MAIN 9330 RATIO 9360 INT 9957 NSF 9260 SSY 9290 KY 9240 PROFILE 9520 SSZ 9300 KZ 9250 RATIO 9360 STIFF 9200 LBRC 9968 STIFF 9200 TRACK 9350 LEG 9500 STP 9894 UNF 9270 LX 9125 TMAIN 9335 UNL 9150 LY 9130 TRACK 9350 WSTR 9110 LZ 9140 UNB 9660 MAIN 9330 UNT 9650 METHOD 9923 MTYP 9956 NBRC 9972 NSF 9260 PROFILE 9520 RATIO 9360 SEISMIC 9953 SGR 9460 SLF 9922 SNUG 9102 SOE 9975 SRT 9131 STB 9973 STT 9974 STIFF 9200 STP 9894 TBRC 9969 TFA 9976 TSL 9132 TMAIN 9335 TND 9959 TORSION 9670 TRACK 9350 UNB 9660 UNL 9150 UNR 9970 UNT 9650 WTYPE 9958 Table 2/10 Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo AISI 2016 API 2A-WSD ASCE 10-97 ASME NF 89/98/01/04 CODE 9010 CODE 9010 CODE 9010 CODE 9010 AXIS 9892 BEAM 9380 BEAM 9380 CAN 9883 BEAM 9380 CB 9280 CMY 9310 CB 9280 CAN 9883 CMY 9310 CMZ 9320 CMY 9310 DFF 9210 CMZ 9320 DMAX 9160 CMZ 9320 DJ1 9390 DMAX 9160 DBL 9863 CT 9924 DJ2 9400 DMIN 9170 DMIN 9170 DFF 9210 FLX 9781 FSJ 9503 ELA 9861 DJ1 9390 FU 9705 FYLD 9100 ELB 9862 DJ2 9400 FYLD 9100 KY 9240 FVB 9865 DMAX 9160 KT 9805 KZ 9250 FYB 9864 DMIN 9170 KY 9240 LY 9130 FYLD 9100 FYLD 9100 KZ 9250 LZ 9140 KT 9805 FU 9705 LT 9806 MAIN 9330 KY 9240 KBK 9950 LY 9130 NSF 9260 KZ 9250 KS 9948 LZ 9140 RATIO 9360 LEG 9500 KV 9949 METHOD 9923 SSY 9290 LT 9806 KY 9240 NSF 9260 SSZ 9300 LY 9130 KZ 9250 RATIO 9360 TRACK 9350 LZ 9140 LY 9130 SSY 9290 UNF 9270 MAIN 9867 LZ 9140 SSZ 9300 UNL 9150 NHL 9866 MAIN 9330 STIFF 9200 WELD 9370 NSF 9260 NSF 9260 TRACK 9350 WMIN 9180 RATIO 9360 PROFILE 9520 TSA 9782 WSTR 9110 SSY 9290 RATIO 9360 UNB 9660 SSZ 9300 SRL 9947 UNT 9650 TRACK 9350 STIFF 9200 UNB 9660 STYPE 9101 UNT 9650 TMAIN 9335 TRACK 9350 UNB 9660 UNT 9650 Table 3/10 Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo ANSI/AISC N690 1994 ANSI/AISC N690 1984 AISC CASTELLATED ASCE 52 CODE 9010 CODE 9010 CODE 9010 CODE 9010 BEAM 9380 CAN 9883 CB 9280 BEAM 9380 CAN 9883 CB 9280 CMZ 9320 DBL 9863 CB 9280 CMY 9310 EOPEN 9885 DMAX 9160 CMY 9310 CMZ 9320 FYLD 9100 DMIN 9170 CMZ 9320 CT 9924 RATIO 9360 ELA 9861 COMPOSITE 9710 DFF 9210 SOPEN 9884 ELB 9862 CONDIA 9715 DJ1 9390 TRACK 9350 FVB 9865 CONHEIGHT 9720 DJ2 9400 UNL 9151 FYB 9864 CYCLES 9725 DMAX 9160 FYLD 9100 DFF 9210 DMIN 9170 KT 9805 DJ1 9390 FU 9705 KY 9240 DJ2 9400 FYLD 9100 KZ 9250 DLR2 9735 KY 9240 LEG 9500 DLRATIO 9730 KZ 9250 LT 9806 DMAX 9160 LY 9130 LY 9130 DMIN 9170 LZ 9140 LZ 9140 EFFWIDTH 9740 MAIN 9330 MAIN 9330 FYLD 9100 NSF 9260 NHL 9866 FPC 9745 PROFILE 9520 NSF 9260 FSS 9882 RATIO 9360 RATIO 9360 FU 9705 SFC 9942 TRACK 9350 FYLD 9100 SFT 9943 KX 9235 SMY 9945 KY 9240 SMZ 9944 KZ 9250 STIFF 9200 LX 9125 STYPE 9101 LY 9130 TMAIN 9335 LZ 9140 TRACK 9350 MAIN 9330 UNB 9660 NSF 9260 UNT 9650 OVR 9880 PLTHICK 9755 PLTWIDTH 9760 PROFILE 9520 RATIO 9360 RIBHEIGHT 9765 RIBWIDTH 9770 SFC 9942 SFT 9943 SHE 9893 SHORING 9775 SLABTHICK 9780 SMY 9945 SMZ 9944 SSY 9290 SSZ 9300 STIFF 9200 STYPE 9101 TAPER 9868 TMAIN 9335 TORSION 9670 TRACK 9350 UNB 9660 UNT 9650 WELD 9370 WMAX 9881 WMIN 9180 WSTR 9110 Table 4/10 Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo BS 5950 BS 5400 Canada S16-01 Canada S16-09/14/19 CODE 9010 CODE 9010 CODE 9010 CODE 9010 AD 9902 ESTIFF 9690 BEAM 9380 BEAM 9380 BEAM 9381 KY 9240 CB 9280 CAN 9883 CAN 9883 KZ 9250 CMY 9790 CB 9280 CB 9281 LY 9130 CMZ 9795 CMY 9790 DFF 9210 LZ 9140 DFF 9210 CMZ 9795 DJ1 9390 MAIN 9332 DJ1 9390 CPSACING 9103 DJ2 9400 NSF 9260 DJ2 9400 DFF 9210 DMAX 9160 PY 9120 DMAX 9160 DJ1 9390 DMIN 9170 RATIO 9360 DMIN 9170 DJ2 9400 ESTIFF 9690 SBLT 9472 FU 9705 DMAX 9160 KY 9240 SGR 9464 FYLD 9100 DMIN 9170 KZ 9250 TRACK 9352 KT 9805 FLX 9781 LEG 9501 UNL 9150 KY 9240 FU 9705 LVV 9190 WET 9510 KZ 9250 FYLD 9100 LY 9130 LT 9806 IMM 9877 LZ 9140 LY 9130 KT 9805 MLT 9820 LZ 9140 KY 9240 MX 9840 MAIN 9338 KZ 9250 MY 9850 NSF 9260 LAT 9929 MYX 9830 RATIO 9360 LEG 9500 NSF 9260 SHEAR 9893 LHT 9938 PNL 9700 SSY 9290 LT 9806 PY 9120 SSZ 9300 LY 9130 MAIN 9331 TRACK 9350 LZ 9140 RATIO 9360 UNB 9660 MAIN 9338 SAME 9870 UNT 9650 NCR 9967 SBLT 9472 NSF 9260 SWAY 9860 PROFILE 9520 SGR 9464 RATIO 9360 TB 9530 SLF 9922 TRACK 9357 SNUG 9102 UNF 9270 SSY 9290 UNL 9150 SSZ 9300 WELD 9371 STIFF 9200 STP 9971 TMAIN 9335 TRACK 9350 UNB 9660 UNT 9650 Table 5/10 Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Canada S136-94 French German Indian 800 2007 LSD CODE 9010 CODE 9010 CODE 9010 CODE 9010 BEAM 9380 BEAM 9385 BEAM 9380 ALPHA 9926 CMZ 9790 C1 9550 CB 9280 ANG 9960 CMY 9795 C2 9560 CMM 9483 ATG 9936 CWY 9783 DFF 9210 DMAX 9160 ATN 9937 DMAX 9160 DJ1 9390 DMIN 9170 AVG 9934 DMIN 9170 DJ2 9400 KY 9240 AVN 9935 FLX 9781 DMAX 9160 KZ 9250 BEAM 9380 FU 9705 DMIN 9170 LY 9130 CAN 9883 FYLD 9100 FYLD 9100 LZ 9140 CMX 9925 KT 9805 KY 9240 PY 9120 CMY 9310 KY 9240 KZ 9250 NSF 9260 CMZ 9320 KZ 9250 LY 9130 RATIO 9360 DFF 9210 LT 9806 LZ 9140 SAME 9870 DJ1 9390 LY 9130 NSF 9260 SBLT 9472 DJ2 9400 LZ 9140 RATIO 9360 SGR 9462 DMAX 9160 NSF 9260 SAME 9870 TRACK 9354 DMIN 9170 STIFF 9200 TRACK 9355 UNF 9270 FU 9705 TRACK 9350 UNF 9270 UNL 9150 FXTY 9962 TSA 9782 UNL 9150 FYLD 9100 IMM 9877 KX 9235 KY 9240 KZ 9250 LAT 9929 LST 9933 LX 9125 LY 9130 LZ 9140 MAIN 9334 NBL 9961 NSF 9260 PROFILE 9520 PSI 9928 RATIO 9360 SEISMIC 9953 STP 9894 SSY 9290 SSZ 9300 TMAIN 9335 TRACK 9350 TSP 9932 TST 9931 Table 6/10 Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Indian 800 1984 Indian 801 1975 Indian 802 1995 Japanese AIJ 2005 CODE 9010 CODE 9010 CODE 9010 CODE 9010 BEAM 9380 BEAM 9380 CNSF 9871 BEAM 9380 CMY 9310 CB 9280 DANGLE 9872 CAN 9883 CMZ 9320 CMY 9310 DBL 9863 CB 9280 DFF 9210 CMZ 9320 DMAX 9160 DFF 9210 DJ1 9390 CWY 9783 DMIN 9170 DJ1 9390 DJ2 9400 FLX 9781 ELA 9861 DJ2 9400 DMAX 9160 FU 9705 FVB 9865 DMAX 9160 DMIN 9170 FYLD 9100 FYB 9864 DMIN 9170 FYLD 9100 KX 9235 FYLD 9100 FYLD 9100 KY 9240 KY 9240 GUSSET 9873 KY 9240 KZ 9250 KZ 9250 KY 9240 KZ 9250 LY 9130 LX 9125 KZ 9250 LY 9130 LZ 9140 LY 9130 LEG 9500 LZ 9140 MAIN 9334 LZ 9140 LY 9130 MAIN 9336 NSF 9260 NSF 9260 LZ 9140 MBG 9823 PROFILE 9520 RATIO 9360 MAIN 9334 MISES 9819 RATIO 9360 TRACK 9350 NSF 9260 NSF 9260 SSY 9290 TSA 9782 NHL 9866 PLB 9822 SSZ 9300 UNL 9270 TRACK 9350 RATIO 9360 TMAIN 9335 SLF 9922 TRACK 9350 TMAIN 9335 UNF 9270 TRACK 9358 UNL 9150 UNF 9270 UNL 9150 Table 7/10 Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Japanese AIJ 2002 Mexian New Zealand Norwegian NS 3472/NPD 1993 CODE 9010 CODE 9010 CODE 9010 CODE 9010 BEAM 9380 BEAM 9380 ALM 9803 BEAM 9380 CAN 9883 CB 9280 BEAM 9380 BY 9430 CB 9280 CMB 9878 DFF 9210 BZ 9440 DFF 9210 DFF 9210 DJ1 9390 CB 9280 DJ1 9390 DJ1 9390 DJ2 9400 CMY 9310 DJ2 9400 DJ2 9400 DMAX 9160 CMZ 9320 DMAX 9160 DMAX 9160 DMIN 9170 CY 9410 DMIN 9170 DMIN 9170 DUCT 9964 CZ 9420 FYLD 9100 DSD 9879 FU 9705 DMAX 9160 KY 9240 FU 9705 FYLD 9100 DMIN 9170 KZ 9250 FYLD 9100 GLD 9965 FYLD 9100 LY 9130 IMM 9877 IST 9800 MF 9450 LZ 9140 INO 9876 KT 9805 RATIO 9360 MAIN 9336 IRR 9875 KY 9240 SSY 9290 MBG 9823 KX 9235 KZ 9250 SSZ 9300 MISES 9819 KY 9240 LHT 9938 TRACK 9351 NSF 9260 KZ 9250 LX 9125 UNL 9150 RATIO 9360 LDR 9874 LY 9130 SLF 9922 LX 9125 LZ 9140 TMAIN 9335 LY 9130 MAIN 9330 TRACK 9358 LZ 9140 NSC 9802 UNF 9270 NSF 9260 NSF 9260 UNL 9150 RATIO 9360 PBRACE 9939 YNG 9824 STIFF 9200 PBCRES 9963 TRACK 9350 RATIO 9360 UNB 9660 SGR 9460 UNT 9650 SKL 9808 SKR 9809 SKT 9807 TMAIN 9335 TRACK 9350 TSP 9932 UNB 9660 UNT 9650 Table 8/10 Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Norwegian NORSOK N-004 Russian SNip 81 South African 1993 South African 2001 CODE 9010 CODE 9010 CODE 9010 CODE 9010 BEAM 9380 BEAM 9380 BEAM 9380 BEAM 9380 CMY 9310 CB 9281 CB 9280 CB 9280 CMZ 9320 CMM 9494 CMY 9310 CMY 9310 DFF 9210 CMN 9494 CMZ 9320 CMZ 9320 DJ1 9390 DFF 9210 DFF 9210 DFF 9210 DJ2 9400 DMAX 9160 DJ1 9390 DJ1 9390 DMAX 9160 DMIN 9170 DJ2 9400 DJ2 9400 DMIN 9170 GAMC1 9809 DMAX 9160 DMAX 9160 FYLD 9100 GAMC2 9810 DMIN 9170 DMIN 9170 FU 9705 GMF 9977 FU 9705 FU 9705 HYD 9940 KY 9240 FYLD 9100 FYLD 9100 KY 9240 KZ 9250 KT 9805 KT 9805 KZ 9250 LEG 9502 KY 9240 KY 9240 LX 9125 LY 9130 KZ 9250 KZ 9250 LY 9130 LZ 9140 LT 9806 LT 9806 LZ 9140 MAIN 9333 LY 9130 LY 9130 MAIN 9330 NSF 9260 LZ 9140 LZ 9140 PSD 9941 PY 9121 MAIN 9330 MAIN 9330 RATIO 9360 RATIO 9360 NSF 9260 NSF 9260 SGR 9466 SBLT 9471 RATIO 9360 RATIO 9360 TMAIN 9335 SGR 9463 SSY 9290 SSY 9290 TRACK 9351 TB 9952 SSZ 9300 SSZ 9300 TRACK 9356 TRACK 9350 STIFF 9200 UNL 9151 UNB 9660 TRACK 9350 UNT 9650 UNB 9660 UNT 9650 Table 9/10 Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo EC3 Australian BS5950 Part 5 ASME NF 1974/1977 CODE 9010 CODE 9010 CODE 9010 CODE 9010 ALH 9674 ALB 9804 BEAM 9380 CAN 9883 ALPHA 9926 ALM 9803 CMY 9310 CB 9280 BEAM 9382 BEAM 9380 CMZ 9320 CMY 9310 BETA 9382 DFF 9210 CWY 9781 CMZ 9320 C1 9550 DJ1 9390 FLX 9781 CT 9924 C2 9560 DJ2 9400 FU 9705 DFF 9210 C3 9565 DMAX 9160 FYLD 9100 DJ1 9390 CAN 9883 DMIN 9170 KX 9235 DJ2 9400 CMM 9482 FU 9705 KY 9240 DMAX 9160 CMN 9492 FYLD 9100 KZ 9250 DMIN 9170 CMT 9671 IST 9800 LX 9125 FYLD 9100 DFF 9210 KT 9805 LY 9130 FU 9705 DJ1 9390 KY 9240 LZ 9140 KBK 9950 DJ2 9400 KZ 9250 MAIN 9330 KS 9948 DMAX 9160 LHT 9938 NSF 9260 KV 9949 DMIN 9170 LX 9125 DMAX 9160 KY 9240 EFT 9673 LY 9130 RATIO 9360 KZ 9250 ELB 9697 LZ 9140 TRACK 9350 LY 9130 ESTIFF 9690 MAIN 9330 LZ 9140 FAB 9508 NSC 9802 MAIN 9330 FU 9705 NSF 9260 NSF 9260 GM0 9504 PBRACE 9939 PROFILE 9520 GM1 9505 PHI 9801 RATIO 9360 GM2 9506 RATIO 9360 SRL 9947 GST 9676 SGR 9466 STIFF 9200 HGT 9720 SKL 9808 TMAIN 9335 KC 9696 SKR 9809 TRACK 9350 KY 9240 SKT 9807 UNB 9660 KZ 9250 TMAIN 9335 UNT 9650 LEG 9501 TRACK 9350 LVV 9190 TSP 9932 LY 9130 UNB 9660 LZ 9140 UNT 9650 MTH 9677 MU 9695 NA 9465 NSF 9260 PLG 9930 PY 9120 RATIO 9360 SBLT 9472 SGR 9461 STIFF 9202 TOM 9672 TORSION 9688 TRACK 9353 UNF 9270 UNL 9150 ZG 9507 Table 10/10 Design Parameter iDesignCommandNo Design Parameter iDesignCommandNo Russian SP 2011 Russian SP 2017 CODE 9010 CODE 9010 BEAM 9380 BMT 9951 BMT 9951 CB 9281 CB 9281 CMM 9494 CMM 9494 CMN 9494 CMN 9494 DFF 9210 DFF 9210 DJ1 9390 DJ1 9390 DJ2 9400 DJ2 9400 DMAX 9160 DMAX 9160 DMIN 9170 DMIN 9170 ENSGR 9818 ENMAIN 9817 FU 9705 ENSGR 9818 GAMC1 9809 GAMC1 9809 GAMC2 9810 GAMC2 9810 GAMM 9978 GAMM 9815 GMF 9977 GMF 9977 KY 9240 KY 9240 KZ 9250 KZ 9250 LEG 9502 LEG 9502 LY 9130 LY 9130 LZ 9140 LZ 9140 NSF 9260 MAIN 9333 PY 9121 NSF 9260 SBLT 9471 PY 9121 STP 9894 RATIO 9360 TB 9952 SBLT 9471 TRACK 9356 SGR 9463 TB 9952 TRACK 9356 UNL 9151 Parameters [in] IntValues Integer Parameter(s) VARIANT array. [in] FloatValues Float Parameter(s) VARIANT array. [in] StringValues String Parameter(s) VARIANT array. [in] NAssignList Reference ID(s) list of Object(s) to be assigned. C++ Syntax

**Value table 1** (source column groupings retained):

| iDesignCode | Description |
| --- | --- |
| 1001 | AASHTO ASD |
| 1002 | AISC ASD |
| 1003 | AS 4100-1998 |
| 1004 | BS 5950-1:2000 |
| 1005 | BS 5400:Part 3:1982 |
| 1006 | CAN/CSA-S16-01 |
| 1007 | French CM66 1977 |
| 1008 | DIN 18 800 Part 1 |
| 1009 | IS 800 1984, ASD |
| 1010 | Japan AIJ 2002 |
| 1011 | AISC LRFD |
| 1012 | Norway NS 3472 2001 |
| 1014 | Norway NPD 1993 |
| 1016 | API 2A-WSD |
| 1020 | ASCE 10-97 |
| 1025 | Russia SNiP 2.23-81* 1990 |
| 1027 | Canada S136-94 |
| 1028 | IS 801 1975 |
| 1029 | IS 802 1995 |
| 1030 | Mexico NTC 1987 |
| 1032 | IS 800 2007, LSD |
| 1052 | IS 800 2007, WSD |
| 1033 | BS 5950-5:1998 |
| 1034 | South Africa SANS 10162-1:2011 |
| 1044 | AASHTO LRFD |
| 1045 | ANSI/AISC 360-05 |
| 1046 | ASME NF 3000 1989 |
| 1047 | ASME NF 3000 1998 |
| 1048 | ASME NF 3000 1974 |
| 1049 | ASME NF 3000 1977 |
| 1050 | Norway NORSOK N-004 |
| 1051 | ASME NF 3000 2004 |
| 1053 | ASME NF 3000 2001 |
| 1060 | ASCE 52 |
| 1061 | ANSI/AISC 360-10 |
| 1062 | Canadian S16_09 |
| 1063 | Russia SP 16.13330.2011 |
| 1064 | South Africa SANS10162-1:1993 |
| 1065 | Canadian S16_14 |
| 1066 | NZS3404_1997 |
| 1067 | ANSI/AISC 360-16 |
| 1068 | Aisi_S100_2016 |
| 1069 | Canadian S16_19 |
| 1102 | AISC CASTELLATED |
| 1202 | AISC N690 1994 |
| 1204 | AISC N690 1984 |
| 1210 | Japan AIJ 2005 |
| 1220 | BS EN 1993-1-1:2005 |
| 1221 | Russia SP 16.13330.2017 |

**Value table 2** (source column groupings retained):

| Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| AISC 360-05, 360-10, and 360-16 |  |  | AISC LRFD (2nd and 3rd Ed.) |  |  | AASHTO (ASD) |  |  | AASHTO (LRFD) |  |
| CODE | 9010 |  | CODE | 9010 |  | CODE | 9010 |  | CODE | 9010 |
| ALH | 9674 |  | AXIS | 9892 |  | BEAM | 9380 |  | BEAM | 9380 |
| BEAM | 9380 |  | BEAM | 9380 |  | CB | 9280 |  | DMAX | 9160 |
| BRC | 9955 |  | CAN | 9883 |  | CMY | 9310 |  | DMIN | 9170 |
| CAN | 9883 |  | CB | 9280 |  | CMZ | 9320 |  | DFF | 9210 |
| CB | 9280 |  | DFF | 9210 |  | DFF | 9210 |  | DJ1 | 9390 |
| CSPACING | 9103 |  | DJ1 | 9390 |  | DJ1 | 9390 |  | DJ2 | 9400 |
| DFF | 9210 |  | DJ2 | 9400 |  | DJ2 | 9400 |  | GRADE | 9580 |
| DJ1 | 9390 |  | DMAX | 9160 |  | DMAX | 9160 |  | KY | 9240 |
| DJ2 | 9400 |  | DMIN | 9170 |  | DMIN | 9170 |  | KZ | 9250 |
| DMAX | 9160 |  | FLX | 9781 |  | FYLD | 9100 |  | LY | 9130 |
| DMIN | 9170 |  | FYLD | 9100 |  | KY | 9240 |  | LZ | 9140 |
| DUCT | 9964 |  | FU | 9705 |  | KZ | 9250 |  | MAIN | 9330 |
| E5P | 9133 |  | KX | 9235 |  | LY | 9130 |  | NSF | 9260 |
| FLX | 9781 |  | KY | 9240 |  | LZ | 9140 |  | TRACK | 9350 |
| FRM | 9954 |  | KZ | 9250 |  | MAIN | 9330 |  | UNB | 9660 |
| FU | 9705 |  | LX | 9125 |  | NSF | 9260 |  | UNT | 9650 |
| FYLD | 9100 |  | LY | 9130 |  | PROFILE | 9260 |  |  |  |
| IMM | 9877 |  | LZ | 9140 |  | PUNCH | 9340 |  |  |  |
| KX | 9235 |  | MAIN | 9330 |  | RATIO | 9360 |  |  |  |
| INT | 9957 |  | NSF | 9260 |  | SSY | 9290 |  |  |  |
| KY | 9240 |  | PROFILE | 9520 |  | SSZ | 9300 |  |  |  |
| KZ | 9250 |  | RATIO | 9360 |  | STIFF | 9200 |  |  |  |
| LBRC | 9968 |  | STIFF | 9200 |  | TRACK | 9350 |  |  |  |
| LEG | 9500 |  | STP | 9894 |  | UNF | 9270 |  |  |  |
| LX | 9125 |  | TMAIN | 9335 |  | UNL | 9150 |  |  |  |
| LY | 9130 |  | TRACK | 9350 |  | WSTR | 9110 |  |  |  |
| LZ | 9140 |  | UNB | 9660 |  |  |  |  |  |  |
| MAIN | 9330 |  | UNT | 9650 |  |  |  |  |  |  |
| METHOD | 9923 |  |  |  |  |  |  |  |  |  |
| MTYP | 9956 |  |  |  |  |  |  |  |  |  |
| NBRC | 9972 |  |  |  |  |  |  |  |  |  |
| NSF | 9260 |  |  |  |  |  |  |  |  |  |
| PROFILE | 9520 |  |  |  |  |  |  |  |  |  |
| RATIO | 9360 |  |  |  |  |  |  |  |  |  |
| SEISMIC | 9953 |  |  |  |  |  |  |  |  |  |
| SGR | 9460 |  |  |  |  |  |  |  |  |  |
| SLF | 9922 |  |  |  |  |  |  |  |  |  |
| SNUG | 9102 |  |  |  |  |  |  |  |  |  |
| SOE | 9975 |  |  |  |  |  |  |  |  |  |
| SRT | 9131 |  |  |  |  |  |  |  |  |  |
| STB | 9973 |  |  |  |  |  |  |  |  |  |
| STT | 9974 |  |  |  |  |  |  |  |  |  |
| STIFF | 9200 |  |  |  |  |  |  |  |  |  |
| STP | 9894 |  |  |  |  |  |  |  |  |  |
| TBRC | 9969 |  |  |  |  |  |  |  |  |  |
| TFA | 9976 |  |  |  |  |  |  |  |  |  |
| TSL | 9132 |  |  |  |  |  |  |  |  |  |
| TMAIN | 9335 |  |  |  |  |  |  |  |  |  |
| TND | 9959 |  |  |  |  |  |  |  |  |  |
| TORSION | 9670 |  |  |  |  |  |  |  |  |  |
| TRACK | 9350 |  |  |  |  |  |  |  |  |  |
| UNB | 9660 |  |  |  |  |  |  |  |  |  |
| UNL | 9150 |  |  |  |  |  |  |  |  |  |
| UNR | 9970 |  |  |  |  |  |  |  |  |  |
| UNT | 9650 |  |  |  |  |  |  |  |  |  |
| WTYPE | 9958 |  |  |  |  |  |  |  |  |  |

**Value table 3** (source column groupings retained):

| Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| AISI 2016 |  |  | API 2A-WSD |  |  | ASCE 10-97 |  |  | ASME NF 89/98/01/04 |  |
| CODE | 9010 |  | CODE | 9010 |  | CODE | 9010 |  | CODE | 9010 |
| AXIS | 9892 |  | BEAM | 9380 |  | BEAM | 9380 |  | CAN | 9883 |
| BEAM | 9380 |  | CB | 9280 |  | CMY | 9310 |  | CB | 9280 |
| CAN | 9883 |  | CMY | 9310 |  | CMZ | 9320 |  | CMY | 9310 |
| DFF | 9210 |  | CMZ | 9320 |  | DMAX | 9160 |  | CMZ | 9320 |
| DJ1 | 9390 |  | DMAX | 9160 |  | DBL | 9863 |  | CT | 9924 |
| DJ2 | 9400 |  | DMIN | 9170 |  | DMIN | 9170 |  | DFF | 9210 |
| FLX | 9781 |  | FSJ | 9503 |  | ELA | 9861 |  | DJ1 | 9390 |
| FU | 9705 |  | FYLD | 9100 |  | ELB | 9862 |  | DJ2 | 9400 |
| FYLD | 9100 |  | KY | 9240 |  | FVB | 9865 |  | DMAX | 9160 |
| KT | 9805 |  | KZ | 9250 |  | FYB | 9864 |  | DMIN | 9170 |
| KY | 9240 |  | LY | 9130 |  | FYLD | 9100 |  | FYLD | 9100 |
| KZ | 9250 |  | LZ | 9140 |  | KT | 9805 |  | FU | 9705 |
| LT | 9806 |  | MAIN | 9330 |  | KY | 9240 |  | KBK | 9950 |
| LY | 9130 |  | NSF | 9260 |  | KZ | 9250 |  | KS | 9948 |
| LZ | 9140 |  | RATIO | 9360 |  | LEG | 9500 |  | KV | 9949 |
| METHOD | 9923 |  | SSY | 9290 |  | LT | 9806 |  | KY | 9240 |
| NSF | 9260 |  | SSZ | 9300 |  | LY | 9130 |  | KZ | 9250 |
| RATIO | 9360 |  | TRACK | 9350 |  | LZ | 9140 |  | LY | 9130 |
| SSY | 9290 |  | UNF | 9270 |  | MAIN | 9867 |  | LZ | 9140 |
| SSZ | 9300 |  | UNL | 9150 |  | NHL | 9866 |  | MAIN | 9330 |
| STIFF | 9200 |  | WELD | 9370 |  | NSF | 9260 |  | NSF | 9260 |
| TRACK | 9350 |  | WMIN | 9180 |  | RATIO | 9360 |  | PROFILE | 9520 |
| TSA | 9782 |  | WSTR | 9110 |  | SSY | 9290 |  | RATIO | 9360 |
| UNB | 9660 |  |  |  |  | SSZ | 9300 |  | SRL | 9947 |
| UNT | 9650 |  |  |  |  | TRACK | 9350 |  | STIFF | 9200 |
|  |  |  |  |  |  | UNB | 9660 |  | STYPE | 9101 |
|  |  |  |  |  |  | UNT | 9650 |  | TMAIN | 9335 |
|  |  |  |  |  |  |  |  |  | TRACK | 9350 |
|  |  |  |  |  |  |  |  |  | UNB | 9660 |
|  |  |  |  |  |  |  |  |  | UNT | 9650 |

**Value table 4** (source column groupings retained):

| Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| ANSI/AISC N690 1994 |  |  | ANSI/AISC N690 1984 |  |  | AISC CASTELLATED |  |  | ASCE 52 |  |
| CODE | 9010 |  | CODE | 9010 |  | CODE | 9010 |  | CODE | 9010 |
| BEAM | 9380 |  | CAN | 9883 |  | CB | 9280 |  | BEAM | 9380 |
| CAN | 9883 |  | CB | 9280 |  | CMZ | 9320 |  | DBL | 9863 |
| CB | 9280 |  | CMY | 9310 |  | EOPEN | 9885 |  | DMAX | 9160 |
| CMY | 9310 |  | CMZ | 9320 |  | FYLD | 9100 |  | DMIN | 9170 |
| CMZ | 9320 |  | CT | 9924 |  | RATIO | 9360 |  | ELA | 9861 |
| COMPOSITE | 9710 |  | DFF | 9210 |  | SOPEN | 9884 |  | ELB | 9862 |
| CONDIA | 9715 |  | DJ1 | 9390 |  | TRACK | 9350 |  | FVB | 9865 |
| CONHEIGHT | 9720 |  | DJ2 | 9400 |  | UNL | 9151 |  | FYB | 9864 |
| CYCLES | 9725 |  | DMAX | 9160 |  |  |  |  | FYLD | 9100 |
| DFF | 9210 |  | DMIN | 9170 |  |  |  |  | KT | 9805 |
| DJ1 | 9390 |  | FU | 9705 |  |  |  |  | KY | 9240 |
| DJ2 | 9400 |  | FYLD | 9100 |  |  |  |  | KZ | 9250 |
| DLR2 | 9735 |  | KY | 9240 |  |  |  |  | LEG | 9500 |
| DLRATIO | 9730 |  | KZ | 9250 |  |  |  |  | LT | 9806 |
| DMAX | 9160 |  | LY | 9130 |  |  |  |  | LY | 9130 |
| DMIN | 9170 |  | LZ | 9140 |  |  |  |  | LZ | 9140 |
| EFFWIDTH | 9740 |  | MAIN | 9330 |  |  |  |  | MAIN | 9330 |
| FYLD | 9100 |  | NSF | 9260 |  |  |  |  | NHL | 9866 |
| FPC | 9745 |  | PROFILE | 9520 |  |  |  |  | NSF | 9260 |
| FSS | 9882 |  | RATIO | 9360 |  |  |  |  | RATIO | 9360 |
| FU | 9705 |  | SFC | 9942 |  |  |  |  | TRACK | 9350 |
| FYLD | 9100 |  | SFT | 9943 |  |  |  |  |  |  |
| KX | 9235 |  | SMY | 9945 |  |  |  |  |  |  |
| KY | 9240 |  | SMZ | 9944 |  |  |  |  |  |  |
| KZ | 9250 |  | STIFF | 9200 |  |  |  |  |  |  |
| LX | 9125 |  | STYPE | 9101 |  |  |  |  |  |  |
| LY | 9130 |  | TMAIN | 9335 |  |  |  |  |  |  |
| LZ | 9140 |  | TRACK | 9350 |  |  |  |  |  |  |
| MAIN | 9330 |  | UNB | 9660 |  |  |  |  |  |  |
| NSF | 9260 |  | UNT | 9650 |  |  |  |  |  |  |
| OVR | 9880 |  |  |  |  |  |  |  |  |  |
| PLTHICK | 9755 |  |  |  |  |  |  |  |  |  |
| PLTWIDTH | 9760 |  |  |  |  |  |  |  |  |  |
| PROFILE | 9520 |  |  |  |  |  |  |  |  |  |
| RATIO | 9360 |  |  |  |  |  |  |  |  |  |
| RIBHEIGHT | 9765 |  |  |  |  |  |  |  |  |  |
| RIBWIDTH | 9770 |  |  |  |  |  |  |  |  |  |
| SFC | 9942 |  |  |  |  |  |  |  |  |  |
| SFT | 9943 |  |  |  |  |  |  |  |  |  |
| SHE | 9893 |  |  |  |  |  |  |  |  |  |
| SHORING | 9775 |  |  |  |  |  |  |  |  |  |
| SLABTHICK | 9780 |  |  |  |  |  |  |  |  |  |
| SMY | 9945 |  |  |  |  |  |  |  |  |  |
| SMZ | 9944 |  |  |  |  |  |  |  |  |  |
| SSY | 9290 |  |  |  |  |  |  |  |  |  |
| SSZ | 9300 |  |  |  |  |  |  |  |  |  |
| STIFF | 9200 |  |  |  |  |  |  |  |  |  |
| STYPE | 9101 |  |  |  |  |  |  |  |  |  |
| TAPER | 9868 |  |  |  |  |  |  |  |  |  |
| TMAIN | 9335 |  |  |  |  |  |  |  |  |  |
| TORSION | 9670 |  |  |  |  |  |  |  |  |  |
| TRACK | 9350 |  |  |  |  |  |  |  |  |  |
| UNB | 9660 |  |  |  |  |  |  |  |  |  |
| UNT | 9650 |  |  |  |  |  |  |  |  |  |
| WELD | 9370 |  |  |  |  |  |  |  |  |  |
| WMAX | 9881 |  |  |  |  |  |  |  |  |  |
| WMIN | 9180 |  |  |  |  |  |  |  |  |  |
| WSTR | 9110 |  |  |  |  |  |  |  |  |  |

**Value table 5** (source column groupings retained):

| Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| BS 5950 |  |  | BS 5400 |  |  | Canada S16-01 |  |  | Canada S16-09/14/19 |  |
| CODE | 9010 |  | CODE | 9010 |  | CODE | 9010 |  | CODE | 9010 |
| AD | 9902 |  | ESTIFF | 9690 |  | BEAM | 9380 |  | BEAM | 9380 |
| BEAM | 9381 |  | KY | 9240 |  | CB | 9280 |  | CAN | 9883 |
| CAN | 9883 |  | KZ | 9250 |  | CMY | 9790 |  | CB | 9280 |
| CB | 9281 |  | LY | 9130 |  | CMZ | 9795 |  | CMY | 9790 |
| DFF | 9210 |  | LZ | 9140 |  | DFF | 9210 |  | CMZ | 9795 |
| DJ1 | 9390 |  | MAIN | 9332 |  | DJ1 | 9390 |  | CPSACING | 9103 |
| DJ2 | 9400 |  | NSF | 9260 |  | DJ2 | 9400 |  | DFF | 9210 |
| DMAX | 9160 |  | PY | 9120 |  | DMAX | 9160 |  | DJ1 | 9390 |
| DMIN | 9170 |  | RATIO | 9360 |  | DMIN | 9170 |  | DJ2 | 9400 |
| ESTIFF | 9690 |  | SBLT | 9472 |  | FU | 9705 |  | DMAX | 9160 |
| KY | 9240 |  | SGR | 9464 |  | FYLD | 9100 |  | DMIN | 9170 |
| KZ | 9250 |  | TRACK | 9352 |  | KT | 9805 |  | FLX | 9781 |
| LEG | 9501 |  | UNL | 9150 |  | KY | 9240 |  | FU | 9705 |
| LVV | 9190 |  | WET | 9510 |  | KZ | 9250 |  | FYLD | 9100 |
| LY | 9130 |  |  |  |  | LT | 9806 |  | IMM | 9877 |
| LZ | 9140 |  |  |  |  | LY | 9130 |  | KT | 9805 |
| MLT | 9820 |  |  |  |  | LZ | 9140 |  | KY | 9240 |
| MX | 9840 |  |  |  |  | MAIN | 9338 |  | KZ | 9250 |
| MY | 9850 |  |  |  |  | NSF | 9260 |  | LAT | 9929 |
| MYX | 9830 |  |  |  |  | RATIO | 9360 |  | LEG | 9500 |
| NSF | 9260 |  |  |  |  | SHEAR | 9893 |  | LHT | 9938 |
| PNL | 9700 |  |  |  |  | SSY | 9290 |  | LT | 9806 |
| PY | 9120 |  |  |  |  | SSZ | 9300 |  | LY | 9130 |
| MAIN | 9331 |  |  |  |  | TRACK | 9350 |  | LZ | 9140 |
| RATIO | 9360 |  |  |  |  | UNB | 9660 |  | MAIN | 9338 |
| SAME | 9870 |  |  |  |  | UNT | 9650 |  | NCR | 9967 |
| SBLT | 9472 |  |  |  |  |  |  |  | NSF | 9260 |
| SWAY | 9860 |  |  |  |  |  |  |  | PROFILE | 9520 |
| SGR | 9464 |  |  |  |  |  |  |  | RATIO | 9360 |
| TB | 9530 |  |  |  |  |  |  |  | SLF | 9922 |
| TRACK | 9357 |  |  |  |  |  |  |  | SNUG | 9102 |
| UNF | 9270 |  |  |  |  |  |  |  | SSY | 9290 |
| UNL | 9150 |  |  |  |  |  |  |  | SSZ | 9300 |
| WELD | 9371 |  |  |  |  |  |  |  | STIFF | 9200 |
|  |  |  |  |  |  |  |  |  | STP | 9971 |
|  |  |  |  |  |  |  |  |  | TMAIN | 9335 |
|  |  |  |  |  |  |  |  |  | TRACK | 9350 |
|  |  |  |  |  |  |  |  |  | UNB | 9660 |
|  |  |  |  |  |  |  |  |  | UNT | 9650 |

**Value table 6** (source column groupings retained):

| Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Canada S136-94 |  |  | French |  |  | German |  |  | Indian 800 2007 LSD |  |
| CODE | 9010 |  | CODE | 9010 |  | CODE | 9010 |  | CODE | 9010 |
| BEAM | 9380 |  | BEAM | 9385 |  | BEAM | 9380 |  | ALPHA | 9926 |
| CMZ | 9790 |  | C1 | 9550 |  | CB | 9280 |  | ANG | 9960 |
| CMY | 9795 |  | C2 | 9560 |  | CMM | 9483 |  | ATG | 9936 |
| CWY | 9783 |  | DFF | 9210 |  | DMAX | 9160 |  | ATN | 9937 |
| DMAX | 9160 |  | DJ1 | 9390 |  | DMIN | 9170 |  | AVG | 9934 |
| DMIN | 9170 |  | DJ2 | 9400 |  | KY | 9240 |  | AVN | 9935 |
| FLX | 9781 |  | DMAX | 9160 |  | KZ | 9250 |  | BEAM | 9380 |
| FU | 9705 |  | DMIN | 9170 |  | LY | 9130 |  | CAN | 9883 |
| FYLD | 9100 |  | FYLD | 9100 |  | LZ | 9140 |  | CMX | 9925 |
| KT | 9805 |  | KY | 9240 |  | PY | 9120 |  | CMY | 9310 |
| KY | 9240 |  | KZ | 9250 |  | NSF | 9260 |  | CMZ | 9320 |
| KZ | 9250 |  | LY | 9130 |  | RATIO | 9360 |  | DFF | 9210 |
| LT | 9806 |  | LZ | 9140 |  | SAME | 9870 |  | DJ1 | 9390 |
| LY | 9130 |  | NSF | 9260 |  | SBLT | 9472 |  | DJ2 | 9400 |
| LZ | 9140 |  | RATIO | 9360 |  | SGR | 9462 |  | DMAX | 9160 |
| NSF | 9260 |  | SAME | 9870 |  | TRACK | 9354 |  | DMIN | 9170 |
| STIFF | 9200 |  | TRACK | 9355 |  | UNF | 9270 |  | FU | 9705 |
| TRACK | 9350 |  | UNF | 9270 |  | UNL | 9150 |  | FXTY | 9962 |
| TSA | 9782 |  | UNL | 9150 |  |  |  |  | FYLD | 9100 |
|  |  |  |  |  |  |  |  |  | IMM | 9877 |
|  |  |  |  |  |  |  |  |  | KX | 9235 |
|  |  |  |  |  |  |  |  |  | KY | 9240 |
|  |  |  |  |  |  |  |  |  | KZ | 9250 |
|  |  |  |  |  |  |  |  |  | LAT | 9929 |
|  |  |  |  |  |  |  |  |  | LST | 9933 |
|  |  |  |  |  |  |  |  |  | LX | 9125 |
|  |  |  |  |  |  |  |  |  | LY | 9130 |
|  |  |  |  |  |  |  |  |  | LZ | 9140 |
|  |  |  |  |  |  |  |  |  | MAIN | 9334 |
|  |  |  |  |  |  |  |  |  | NBL | 9961 |
|  |  |  |  |  |  |  |  |  | NSF | 9260 |
|  |  |  |  |  |  |  |  |  | PROFILE | 9520 |
|  |  |  |  |  |  |  |  |  | PSI | 9928 |
|  |  |  |  |  |  |  |  |  | RATIO | 9360 |
|  |  |  |  |  |  |  |  |  | SEISMIC | 9953 |
|  |  |  |  |  |  |  |  |  | STP | 9894 |
|  |  |  |  |  |  |  |  |  | SSY | 9290 |
|  |  |  |  |  |  |  |  |  | SSZ | 9300 |
|  |  |  |  |  |  |  |  |  | TMAIN | 9335 |
|  |  |  |  |  |  |  |  |  | TRACK | 9350 |
|  |  |  |  |  |  |  |  |  | TSP | 9932 |
|  |  |  |  |  |  |  |  |  | TST | 9931 |

**Value table 7** (source column groupings retained):

| Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Indian 800 1984 |  |  | Indian 801 1975 |  |  | Indian 802 1995 |  |  | Japanese AIJ 2005 |  |
| CODE | 9010 |  | CODE | 9010 |  | CODE | 9010 |  | CODE | 9010 |
| BEAM | 9380 |  | BEAM | 9380 |  | CNSF | 9871 |  | BEAM | 9380 |
| CMY | 9310 |  | CB | 9280 |  | DANGLE | 9872 |  | CAN | 9883 |
| CMZ | 9320 |  | CMY | 9310 |  | DBL | 9863 |  | CB | 9280 |
| DFF | 9210 |  | CMZ | 9320 |  | DMAX | 9160 |  | DFF | 9210 |
| DJ1 | 9390 |  | CWY | 9783 |  | DMIN | 9170 |  | DJ1 | 9390 |
| DJ2 | 9400 |  | FLX | 9781 |  | ELA | 9861 |  | DJ2 | 9400 |
| DMAX | 9160 |  | FU | 9705 |  | FVB | 9865 |  | DMAX | 9160 |
| DMIN | 9170 |  | FYLD | 9100 |  | FYB | 9864 |  | DMIN | 9170 |
| FYLD | 9100 |  | KX | 9235 |  | FYLD | 9100 |  | FYLD | 9100 |
| KY | 9240 |  | KY | 9240 |  | GUSSET | 9873 |  | KY | 9240 |
| KZ | 9250 |  | KZ | 9250 |  | KY | 9240 |  | KZ | 9250 |
| LY | 9130 |  | LX | 9125 |  | KZ | 9250 |  | LY | 9130 |
| LZ | 9140 |  | LY | 9130 |  | LEG | 9500 |  | LZ | 9140 |
| MAIN | 9334 |  | LZ | 9140 |  | LY | 9130 |  | MAIN | 9336 |
| NSF | 9260 |  | NSF | 9260 |  | LZ | 9140 |  | MBG | 9823 |
| PROFILE | 9520 |  | RATIO | 9360 |  | MAIN | 9334 |  | MISES | 9819 |
| RATIO | 9360 |  | TRACK | 9350 |  | NSF | 9260 |  | NSF | 9260 |
| SSY | 9290 |  | TSA | 9782 |  | NHL | 9866 |  | PLB | 9822 |
| SSZ | 9300 |  | UNL | 9270 |  | TRACK | 9350 |  | RATIO | 9360 |
| TMAIN | 9335 |  |  |  |  |  |  |  | SLF | 9922 |
| TRACK | 9350 |  |  |  |  |  |  |  | TMAIN | 9335 |
| UNF | 9270 |  |  |  |  |  |  |  | TRACK | 9358 |
| UNL | 9150 |  |  |  |  |  |  |  | UNF | 9270 |
|  |  |  |  |  |  |  |  |  | UNL | 9150 |

**Value table 8** (source column groupings retained):

| Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Japanese AIJ 2002 |  |  | Mexian |  |  | New Zealand |  |  | Norwegian NS 3472/NPD 1993 |  |
| CODE | 9010 |  | CODE | 9010 |  | CODE | 9010 |  | CODE | 9010 |
| BEAM | 9380 |  | BEAM | 9380 |  | ALM | 9803 |  | BEAM | 9380 |
| CAN | 9883 |  | CB | 9280 |  | BEAM | 9380 |  | BY | 9430 |
| CB | 9280 |  | CMB | 9878 |  | DFF | 9210 |  | BZ | 9440 |
| DFF | 9210 |  | DFF | 9210 |  | DJ1 | 9390 |  | CB | 9280 |
| DJ1 | 9390 |  | DJ1 | 9390 |  | DJ2 | 9400 |  | CMY | 9310 |
| DJ2 | 9400 |  | DJ2 | 9400 |  | DMAX | 9160 |  | CMZ | 9320 |
| DMAX | 9160 |  | DMAX | 9160 |  | DMIN | 9170 |  | CY | 9410 |
| DMIN | 9170 |  | DMIN | 9170 |  | DUCT | 9964 |  | CZ | 9420 |
| FYLD | 9100 |  | DSD | 9879 |  | FU | 9705 |  | DMAX | 9160 |
| KY | 9240 |  | FU | 9705 |  | FYLD | 9100 |  | DMIN | 9170 |
| KZ | 9250 |  | FYLD | 9100 |  | GLD | 9965 |  | FYLD | 9100 |
| LY | 9130 |  | IMM | 9877 |  | IST | 9800 |  | MF | 9450 |
| LZ | 9140 |  | INO | 9876 |  | KT | 9805 |  | RATIO | 9360 |
| MAIN | 9336 |  | IRR | 9875 |  | KY | 9240 |  | SSY | 9290 |
| MBG | 9823 |  | KX | 9235 |  | KZ | 9250 |  | SSZ | 9300 |
| MISES | 9819 |  | KY | 9240 |  | LHT | 9938 |  | TRACK | 9351 |
| NSF | 9260 |  | KZ | 9250 |  | LX | 9125 |  | UNL | 9150 |
| RATIO | 9360 |  | LDR | 9874 |  | LY | 9130 |  |  |  |
| SLF | 9922 |  | LX | 9125 |  | LZ | 9140 |  |  |  |
| TMAIN | 9335 |  | LY | 9130 |  | MAIN | 9330 |  |  |  |
| TRACK | 9358 |  | LZ | 9140 |  | NSC | 9802 |  |  |  |
| UNF | 9270 |  | NSF | 9260 |  | NSF | 9260 |  |  |  |
| UNL | 9150 |  | RATIO | 9360 |  | PBRACE | 9939 |  |  |  |
| YNG | 9824 |  | STIFF | 9200 |  | PBCRES | 9963 |  |  |  |
|  |  |  | TRACK | 9350 |  | RATIO | 9360 |  |  |  |
|  |  |  | UNB | 9660 |  | SGR | 9460 |  |  |  |
|  |  |  | UNT | 9650 |  | SKL | 9808 |  |  |  |
|  |  |  |  |  |  | SKR | 9809 |  |  |  |
|  |  |  |  |  |  | SKT | 9807 |  |  |  |
|  |  |  |  |  |  | TMAIN | 9335 |  |  |  |
|  |  |  |  |  |  | TRACK | 9350 |  |  |  |
|  |  |  |  |  |  | TSP | 9932 |  |  |  |
|  |  |  |  |  |  | UNB | 9660 |  |  |  |
|  |  |  |  |  |  | UNT | 9650 |  |  |  |

**Value table 9** (source column groupings retained):

| Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Norwegian NORSOK N-004 |  |  | Russian SNip 81 |  |  | South African 1993 |  |  | South African 2001 |  |
| CODE | 9010 |  | CODE | 9010 |  | CODE | 9010 |  | CODE | 9010 |
| BEAM | 9380 |  | BEAM | 9380 |  | BEAM | 9380 |  | BEAM | 9380 |
| CMY | 9310 |  | CB | 9281 |  | CB | 9280 |  | CB | 9280 |
| CMZ | 9320 |  | CMM | 9494 |  | CMY | 9310 |  | CMY | 9310 |
| DFF | 9210 |  | CMN | 9494 |  | CMZ | 9320 |  | CMZ | 9320 |
| DJ1 | 9390 |  | DFF | 9210 |  | DFF | 9210 |  | DFF | 9210 |
| DJ2 | 9400 |  | DMAX | 9160 |  | DJ1 | 9390 |  | DJ1 | 9390 |
| DMAX | 9160 |  | DMIN | 9170 |  | DJ2 | 9400 |  | DJ2 | 9400 |
| DMIN | 9170 |  | GAMC1 | 9809 |  | DMAX | 9160 |  | DMAX | 9160 |
| FYLD | 9100 |  | GAMC2 | 9810 |  | DMIN | 9170 |  | DMIN | 9170 |
| FU | 9705 |  | GMF | 9977 |  | FU | 9705 |  | FU | 9705 |
| HYD | 9940 |  | KY | 9240 |  | FYLD | 9100 |  | FYLD | 9100 |
| KY | 9240 |  | KZ | 9250 |  | KT | 9805 |  | KT | 9805 |
| KZ | 9250 |  | LEG | 9502 |  | KY | 9240 |  | KY | 9240 |
| LX | 9125 |  | LY | 9130 |  | KZ | 9250 |  | KZ | 9250 |
| LY | 9130 |  | LZ | 9140 |  | LT | 9806 |  | LT | 9806 |
| LZ | 9140 |  | MAIN | 9333 |  | LY | 9130 |  | LY | 9130 |
| MAIN | 9330 |  | NSF | 9260 |  | LZ | 9140 |  | LZ | 9140 |
| PSD | 9941 |  | PY | 9121 |  | MAIN | 9330 |  | MAIN | 9330 |
| RATIO | 9360 |  | RATIO | 9360 |  | NSF | 9260 |  | NSF | 9260 |
| SGR | 9466 |  | SBLT | 9471 |  | RATIO | 9360 |  | RATIO | 9360 |
| TMAIN | 9335 |  | SGR | 9463 |  | SSY | 9290 |  | SSY | 9290 |
| TRACK | 9351 |  | TB | 9952 |  | SSZ | 9300 |  | SSZ | 9300 |
|  |  |  | TRACK | 9356 |  | TRACK | 9350 |  | STIFF | 9200 |
|  |  |  | UNL | 9151 |  | UNB | 9660 |  | TRACK | 9350 |
|  |  |  |  |  |  | UNT | 9650 |  | UNB | 9660 |
|  |  |  |  |  |  |  |  |  | UNT | 9650 |

**Value table 10** (source column groupings retained):

| Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| EC3 |  |  | Australian |  |  | BS5950 Part 5 |  |  | ASME NF 1974/1977 |  |
| CODE | 9010 |  | CODE | 9010 |  | CODE | 9010 |  | CODE | 9010 |
| ALH | 9674 |  | ALB | 9804 |  | BEAM | 9380 |  | CAN | 9883 |
| ALPHA | 9926 |  | ALM | 9803 |  | CMY | 9310 |  | CB | 9280 |
| BEAM | 9382 |  | BEAM | 9380 |  | CMZ | 9320 |  | CMY | 9310 |
| BETA | 9382 |  | DFF | 9210 |  | CWY | 9781 |  | CMZ | 9320 |
| C1 | 9550 |  | DJ1 | 9390 |  | FLX | 9781 |  | CT | 9924 |
| C2 | 9560 |  | DJ2 | 9400 |  | FU | 9705 |  | DFF | 9210 |
| C3 | 9565 |  | DMAX | 9160 |  | FYLD | 9100 |  | DJ1 | 9390 |
| CAN | 9883 |  | DMIN | 9170 |  | KX | 9235 |  | DJ2 | 9400 |
| CMM | 9482 |  | FU | 9705 |  | KY | 9240 |  | DMAX | 9160 |
| CMN | 9492 |  | FYLD | 9100 |  | KZ | 9250 |  | DMIN | 9170 |
| CMT | 9671 |  | IST | 9800 |  | LX | 9125 |  | FYLD | 9100 |
| DFF | 9210 |  | KT | 9805 |  | LY | 9130 |  | FU | 9705 |
| DJ1 | 9390 |  | KY | 9240 |  | LZ | 9140 |  | KBK | 9950 |
| DJ2 | 9400 |  | KZ | 9250 |  | MAIN | 9330 |  | KS | 9948 |
| DMAX | 9160 |  | LHT | 9938 |  | NSF | 9260 |  | KV | 9949 |
| DMIN | 9170 |  | LX | 9125 |  | DMAX | 9160 |  | KY | 9240 |
| EFT | 9673 |  | LY | 9130 |  | RATIO | 9360 |  | KZ | 9250 |
| ELB | 9697 |  | LZ | 9140 |  | TRACK | 9350 |  | LY | 9130 |
| ESTIFF | 9690 |  | MAIN | 9330 |  |  |  |  | LZ | 9140 |
| FAB | 9508 |  | NSC | 9802 |  |  |  |  | MAIN | 9330 |
| FU | 9705 |  | NSF | 9260 |  |  |  |  | NSF | 9260 |
| GM0 | 9504 |  | PBRACE | 9939 |  |  |  |  | PROFILE | 9520 |
| GM1 | 9505 |  | PHI | 9801 |  |  |  |  | RATIO | 9360 |
| GM2 | 9506 |  | RATIO | 9360 |  |  |  |  | SRL | 9947 |
| GST | 9676 |  | SGR | 9466 |  |  |  |  | STIFF | 9200 |
| HGT | 9720 |  | SKL | 9808 |  |  |  |  | TMAIN | 9335 |
| KC | 9696 |  | SKR | 9809 |  |  |  |  | TRACK | 9350 |
| KY | 9240 |  | SKT | 9807 |  |  |  |  | UNB | 9660 |
| KZ | 9250 |  | TMAIN | 9335 |  |  |  |  | UNT | 9650 |
| LEG | 9501 |  | TRACK | 9350 |  |  |  |  |  |  |
| LVV | 9190 |  | TSP | 9932 |  |  |  |  |  |  |
| LY | 9130 |  | UNB | 9660 |  |  |  |  |  |  |
| LZ | 9140 |  | UNT | 9650 |  |  |  |  |  |  |
| MTH | 9677 |  |  |  |  |  |  |  |  |  |
| MU | 9695 |  |  |  |  |  |  |  |  |  |
| NA | 9465 |  |  |  |  |  |  |  |  |  |
| NSF | 9260 |  |  |  |  |  |  |  |  |  |
| PLG | 9930 |  |  |  |  |  |  |  |  |  |
| PY | 9120 |  |  |  |  |  |  |  |  |  |
| RATIO | 9360 |  |  |  |  |  |  |  |  |  |
| SBLT | 9472 |  |  |  |  |  |  |  |  |  |
| SGR | 9461 |  |  |  |  |  |  |  |  |  |
| STIFF | 9202 |  |  |  |  |  |  |  |  |  |
| TOM | 9672 |  |  |  |  |  |  |  |  |  |
| TORSION | 9688 |  |  |  |  |  |  |  |  |  |
| TRACK | 9353 |  |  |  |  |  |  |  |  |  |
| UNF | 9270 |  |  |  |  |  |  |  |  |  |
| UNL | 9150 |  |  |  |  |  |  |  |  |  |
| ZG | 9507 |  |  |  |  |  |  |  |  |  |

**Value table 11** (source column groupings retained):

| Design Parameter | iDesignCommandNo |  | Design Parameter | iDesignCommandNo |
| --- | --- | --- | --- | --- |
| Russian SP 2011 |  |  | Russian SP 2017 |  |
| CODE | 9010 |  | CODE | 9010 |
| BEAM | 9380 |  | BMT | 9951 |
| BMT | 9951 |  | CB | 9281 |
| CB | 9281 |  | CMM | 9494 |
| CMM | 9494 |  | CMN | 9494 |
| CMN | 9494 |  | DFF | 9210 |
| DFF | 9210 |  | DJ1 | 9390 |
| DJ1 | 9390 |  | DJ2 | 9400 |
| DJ2 | 9400 |  | DMAX | 9160 |
| DMAX | 9160 |  | DMIN | 9170 |
| DMIN | 9170 |  | ENSGR | 9818 |
| ENMAIN | 9817 |  | FU | 9705 |
| ENSGR | 9818 |  | GAMC1 | 9809 |
| GAMC1 | 9809 |  | GAMC2 | 9810 |
| GAMC2 | 9810 |  | GAMM | 9978 |
| GAMM | 9815 |  | GMF | 9977 |
| GMF | 9977 |  | KY | 9240 |
| KY | 9240 |  | KZ | 9250 |
| KZ | 9250 |  | LEG | 9502 |
| LEG | 9502 |  | LY | 9130 |
| LY | 9130 |  | LZ | 9140 |
| LZ | 9140 |  | NSF | 9260 |
| MAIN | 9333 |  | PY | 9121 |
| NSF | 9260 |  | SBLT | 9471 |
| PY | 9121 |  | STP | 9894 |
| RATIO | 9360 |  | TB | 9952 |
| SBLT | 9471 |  | TRACK | 9356 |
| SGR | 9463 |  |  |  |
| TB | 9952 |  |  |  |
| TRACK | 9356 |  |  |  |
| UNL | 9151 |  |  |  |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void CreateSteelDesignCommand(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [CreateSteelDesignCommand](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L244)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

## 4. Complete private-source overload mapping

| Main managed signature | Return | Native targets / delegation | Reference |
| --- | --- | --- | --- |
| `PerformAnalysis(PrintOption printOption)` | `IOSCommands` | `PerformAnalysis` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L19) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L40) |
| `PerformPDeltaAnalysisNoConverge(int numberOfIterations, PrintOption printOption)` | `IOSCommands` | `PerformPDeltaAnalysisNoConverge` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L23) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L49) |
| `PerformPDeltaAnalysisEx(int numberOfIterations, PrintOption printOption, PDeltaDisplacementEffect displacementEffect, bool includeGeometricStiffness)` | `IOSCommands` | `PerformPDeltaAnalysisEx` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L24) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L57) |
| `PerformPDeltaAnalysis(PDeltaAnalysisOptions options)` | `IOSCommands` | `PerformPDeltaAnalysisEx` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L29) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L74) |
| `PerformPDeltaAnalysisConverge(int numberOfIterations, PrintOption printOption)` | `IOSCommands` | `PerformPDeltaAnalysisConverge` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L31) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L85) |
| `PerformCableAnalysis(int numberOfIterations, PrintOption printOption)` | `IOSCommands` | `PerformCableAnalysis` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L34) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L94) |
| `PerformCableAnalysisEx(CableAnalysisOptions options)` | `bool` | `PerformCableAnalysisEx` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L35) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L102) |
| `PerformBucklingAnalysis(int maximumSteps, PrintOption printOption)` | `IOSCommands` | `PerformBucklingAnalysis` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L36) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L114) |
| `PerformBucklingAnalysisEx(ManagedBucklingAnalysisMethod method, int maximumSteps, PrintOption printOption)` | `bool` | `PerformBucklingAnalysisEx` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L37) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L122) |
| `PerformDirectAnalysis(DirectAnalysisOptions options)` | `bool` | `PerformDirectAnalysis` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L38) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L130) |
| `PerformNonlinearAnalysisEx(NonlinearAnalysisOptions options)` | `bool` | `PerformNonlinearAnalysisEx` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L39) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L141) |
| `SetFloorDiaphragmBaseCommand(double baseElevation)` | `bool` | `SetFloorDiaphragmBaseCommand` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L41) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L156) |
| `DeleteFloorDiaphragmBaseCommand()` | `bool` | `DeleteFloorDiaphragmBaseCommand` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L42) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L163) |
| `SetCheckSoftStoryCommand(SoftStoryDesignCode designCode)` | `bool` | `SetCheckSoftStoryCommand` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L43) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L168) |
| `DeleteCheckSoftStoryCommand()` | `bool` | `DeleteCheckSoftStoryCommand` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L44) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L174) |
| `SetCheckIrregularitiesCommand(IrregularitiesDesignCode designCode)` | `bool` | `SetCheckIrregularitiesCommand` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L45) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L179) |
| `DeleteCheckIrregularitiesCommand()` | `bool` | `DeleteCheckIrregularitiesCommand` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L46) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L185) |
| `DeleteAllAnalysisCommands()` | `bool` | `DeleteAllAnalysisCommands` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L47) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L190) |
| `CreateSteelDesignCommand(SteelDesignCommandRequest request)` | `void` | `CreateSteelDesignCommand` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L54) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L233) |
| `ValidateSteelDesignCommand(SteelDesignCommandRequest request)` | `SteelDesignCommandValidationResult` | Managed helper/delegation; inspect source | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L57) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L228) |
| `GetSteelDesignCommands(StaadSteelDesignCode designCode)` | `IReadOnlyList<SteelDesignCommandDescriptor>` | Managed helper/delegation; inspect source | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSCommands.cs#L60) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSCommandsAdapter.cs#L223) |

Wrapper and ComObject properties are binding contracts, not domain methods. Constructors and private helpers are not counted as public overloads. Existing source signatures above are comparison evidence, not approved signatures for a new public port.

## 5. Passive metadata, constants and fingerprints

2025 and 2026 IOSCommandsUI method signatures, parameter metadata and referenced enum constants match. Both assemblies have the same identity but different binary hashes; each was inspected in a separate reflection-only process. No native object was activated. The expected 2024 STAAD interop DLL was unavailable for passive inspection.

| Year | Assembly SHA-256 |
| --- | --- |
| 2025 | CC724993AC9119138B7323E8C684997257E2851DE62E1551BA5D6D67A9DADC19 |
| 2026 | 6D153E80ED356A1B167ABB2EF19227FCFF510707A578AD304154652CC6C1F6D4 |

### 2024 help evidence

| Page | SHA-256 | Summary / details |
| --- | --- | --- |
| [Command](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___command_group.html) | 2457570229831deef520370fc35267e81645c823141347fde900d5d9986e14a3 | 0 / 0 |
| [Command: Analysis Commands Command](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___analysis_command_group.html) | e1baf0546ea6b2325fa8232df1a8b846aec045b46b066b5c2cc9e89588c47377 | 17 / 16 |
| [Command: Steel Design Commands Command](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___steel_design_command_group.html) | 5169e19d663b092e65fdc32af5d42a7753b00bb67fea53b13de9572dd7430a9c | 1 / 1 |
| [Native class listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_commands_u_i.html) | 9c03526944553157aa19667794a607d9a2e04216996fcdd53f7aa74e1c9afb29 | 20 names |
| [Native header listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/oscommandsui_8h_source.html) | 197e704d56b2439b4b5ddfb5c967172471381beea19f03869d4e59b36797a8f2 | Supporting declaration evidence |

### 2025 help evidence

| Page | SHA-256 | Summary / details |
| --- | --- | --- |
| [Command](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___command_group.html) | d5103cde8fd9b9044bcf320ba05fd1490305cdd55bc6e9db527c7853aa6db7af | 0 / 0 |
| [Command: Analysis Commands Command](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___analysis_command_group.html) | 7abb55ea3f7573767b3ec833c67bfbbc6041b0d6f6c308862920558fcf34fb91 | 17 / 16 |
| [Command: Steel Design Commands Command](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___steel_design_command_group.html) | 19f2d9b38fb905f6d7dab7f7c67bb424a70f2c924f87aa46e81f5127cff12f2c | 1 / 1 |
| [Native class listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_commands_u_i.html) | d2fce18a7baf0323cb64fc8c91cb0f798fd22d6af702f8ce07be6a02e71e6bef | 20 names |
| [Native header listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/oscommandsui_8h_source.html) | 060a56d066329c20160751b293ef3681cc9b73399d4b6b16a636490c371ad160 | Supporting declaration evidence |

### 2026 help evidence

| Page | SHA-256 | Summary / details |
| --- | --- | --- |
| [Command](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___command_group.html) | 3c23a714657459f3db5918a987dac3b55068a67e0922db728df435cf8395c4f9 | 0 / 0 |
| [Command: Analysis Commands Command](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___analysis_command_group.html) | 5c34b379e848ec9f03c11cfec99ab3e20fb118684fd7ac14e53fd04cc7e81996 | 17 / 16 |
| [Command: Steel Design Commands Command](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___steel_design_command_group.html) | 3d0aae9ada2adf5b788a5c059197152df65364afe0cba9204b6a33c8db700d80 | 1 / 1 |
| [Native class listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_commands_u_i.html) | f48e5f442b9cfc3648e181c3fc8d3ba4622c50fa6cf2d4675b411fc80a17cf9b | 20 names |
| [Native header listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/oscommandsui_8h_source.html) | f31a42ab7783f686242fe7d1d4e7cf1a8f7287b4009c40225aaac8291783263b | Supporting declaration evidence |
