# OpenSTAAD Table API coverage and native contract inventory

**Implementation checkpoint, 28 October 2026:** Ported ten existing main-project Table methods, reaching 10/24 documented native names. Nineteen of 29 main-source overloads remain pending. Main-source parity has priority over native-only expansion. See [report/table contracts and usage](../Table-Report-Foundations.md). The review baseline below is historical.

**Reviewed:** 23 October 2026<br>**Status:** Planning only; no typed Table domain functions are implemented in this repository.

Read the [architecture and implementation plan](OpenSTAAD-Table-Multi-Version-Architecture-and-Plan.md) first. This annex records all 24 native names, all source interface overloads, declarations, logical parameter directions, return conventions, value tables, metadata and proposed managed result shapes. Native names and constants below are evidence; managed names and types are proposals requiring API review.

## 1. Counting and evidence boundaries

| Measure | 2024 | 2025 | 2026 |
| --- | --- | --- | --- |
| Group names | 24 | 24 | 24 |
| Detailed contracts | 24 | 24 | 24 |
| Native class names | 33 | 33 | 33 |
| Inspected automation methods | No installed DLL inspected | 24 | 24 |

Native class implementation/lifecycle helpers excluded from the public port: `SetStructure`, `SetReportToMap`, `GetReportFromMap`, `RemoveReportFromMap`, `RemoveAllReportsFromMap`, `GetReportsInMap`, `GetReportCaption`, `BuildReportData`, `OnFinalRelease`. Other group pages were scanned for OSTableUI ownership; no additional domain-owned rows were found. For Design, Output-owned functions under the Design steel page remain in the separate Output report.

Windows native long is 32-bit. VARIANT and LP(D)ISPATCH declarations do not establish a managed payload type or successful operation. Logical directions below come from parameter prose, not merely C++ reference syntax. Unmatched names and stale prose are shown explicitly; parameter positions must be reconciled against metadata and a controlled native fixture before dispatch is approved.

## 2. Complete cross-version function matrix

| Native Table function | Native return | 2024 / 2025 / 2026 | Main native target | Current typed / plan |
| --- | --- | --- | --- | --- |
| `CreateReport` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gae01ef57411174492e697268fdc10aeab) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gae01ef57411174492e697268fdc10aeab) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gae01ef57411174492e697268fdc10aeab) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L55) | Implemented / common TableService |
| `SaveReport` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga51c64edd9634d684b57d1360012c1129) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga51c64edd9634d684b57d1360012c1129) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga51c64edd9634d684b57d1360012c1129) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L99) | Implemented / common TableService |
| `SaveReportAll` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gaf846474a8802876be358da52ff41ec21) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gaf846474a8802876be358da52ff41ec21) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gaf846474a8802876be358da52ff41ec21) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L104) | Pending / T4 |
| `GetReportCount` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gaf44cf23f412b6f167687daeb2a6a27da) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gaf44cf23f412b6f167687daeb2a6a27da) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gaf44cf23f412b6f167687daeb2a6a27da) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L79) | Implemented / common TableService |
| `AddTable` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga290b4a43a1c62b9373448d09dfcf91bd) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga290b4a43a1c62b9373448d09dfcf91bd) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga290b4a43a1c62b9373448d09dfcf91bd) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L50) | Implemented / common TableService |
| `RenameTable` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gaa103253fe1917dd7858ef95d877a2aef) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gaa103253fe1917dd7858ef95d877a2aef) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gaa103253fe1917dd7858ef95d877a2aef) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L89) | Pending / T2 |
| `DeleteReport` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gad1b6c0e8d133a68842788c5d6dc00420) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gad1b6c0e8d133a68842788c5d6dc00420) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gad1b6c0e8d133a68842788c5d6dc00420) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L60) | Pending / T2 |
| `DeleteTable` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga98f2843f774ff48bd424e52b750a8ea0) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga98f2843f774ff48bd424e52b750a8ea0) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga98f2843f774ff48bd424e52b750a8ea0) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L65) | Pending / T2 |
| `ResizeTable` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gaeeeabb0ab44d224ed382da6c67f5dcab) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gaeeeabb0ab44d224ed382da6c67f5dcab) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gaeeeabb0ab44d224ed382da6c67f5dcab) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L94) | Pending / T2 |
| `SaveTable` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga47c79f2977543e071cf7f36693010c7e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga47c79f2977543e071cf7f36693010c7e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga47c79f2977543e071cf7f36693010c7e) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L109) | Pending / T4 |
| `GetTableCount` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gae03d9fe3b9c0e5ce9c6a7e44b286c9a2) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gae03d9fe3b9c0e5ce9c6a7e44b286c9a2) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gae03d9fe3b9c0e5ce9c6a7e44b286c9a2) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L84) | Implemented / common TableService |
| `GetCellValue` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gab3fa167c0c15a7ab5cc620bbc6c758c7) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gab3fa167c0c15a7ab5cc620bbc6c758c7) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gab3fa167c0c15a7ab5cc620bbc6c758c7) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L72) | Implemented / common TableService |
| `SetCellValue` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga8fdf9c6f724e6a89e01e2c98fe9b342c) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga8fdf9c6f724e6a89e01e2c98fe9b342c) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga8fdf9c6f724e6a89e01e2c98fe9b342c) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L154) | Implemented / common TableService |
| `SetColumnHeader` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gae61b64578ab572c3ef5fb3e6a3e12476) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gae61b64578ab572c3ef5fb3e6a3e12476) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gae61b64578ab572c3ef5fb3e6a3e12476) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L159) | Implemented / common TableService |
| `SetColumnUnitString` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga3683e0ed94cd0586cf931812dc5ccf2b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga3683e0ed94cd0586cf931812dc5ccf2b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga3683e0ed94cd0586cf931812dc5ccf2b) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L164) | Implemented / common TableService |
| `SetRowHeader` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gace3b6978fac76ae72edbb2d25937cd5f) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gace3b6978fac76ae72edbb2d25937cd5f) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gace3b6978fac76ae72edbb2d25937cd5f) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L169) | Implemented / common TableService |
| `SetCellTextColor` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga82643e4ba7ce238d2f31abd50734bea9) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga82643e4ba7ce238d2f31abd50734bea9) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga82643e4ba7ce238d2f31abd50734bea9) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L119) | Pending / T3 |
| `SetCellTextBold` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga276740e7efec12ecc0778ca11031836e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga276740e7efec12ecc0778ca11031836e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga276740e7efec12ecc0778ca11031836e) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L114) | Pending / T3 |
| `SetCellTextItalic` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga32f5eff8ba94c8258edd82a9e5b0f40f) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga32f5eff8ba94c8258edd82a9e5b0f40f) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga32f5eff8ba94c8258edd82a9e5b0f40f) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L129) | Pending / T3 |
| `SetCellTextUnderline` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga6230aedbfdf0d9f8d6466c6a4927f0fb) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga6230aedbfdf0d9f8d6466c6a4927f0fb) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga6230aedbfdf0d9f8d6466c6a4927f0fb) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L144) | Pending / T3 |
| `SetCellTextHorzAlignment` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga4b2886afe16ebd9632f97fe4b5637e8d) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga4b2886afe16ebd9632f97fe4b5637e8d) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga4b2886afe16ebd9632f97fe4b5637e8d) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L124) | Pending / T3 |
| `SetCellTextVertAlignment` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga9846d7eeef4bd24a9eecffa3af7452f1) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga9846d7eeef4bd24a9eecffa3af7452f1) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga9846d7eeef4bd24a9eecffa3af7452f1) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L149) | Pending / T3 |
| `SetCellTextSize` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga83f1ee3a8b9c5f964284a575b7c33811) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga83f1ee3a8b9c5f964284a575b7c33811) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga83f1ee3a8b9c5f964284a575b7c33811) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L134) | Pending / T3 |
| `SetCellTextSizeAll` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga03e55a45d5ec4b92ecf64ab3b696f2e9) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga03e55a45d5ec4b92ecf64ab3b696f2e9) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga03e55a45d5ec4b92ecf64ab3b696f2e9) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L139) | Pending / T3 |

## 3. Per-function contracts and managed proposals

<a id="table-createreport"></a>

### 1. CreateReport

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gae01ef57411174492e697268fdc10aeab) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gae01ef57411174492e697268fdc10aeab) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gae01ef57411174492e697268fdc10aeab)<br>**Current:** Pending; T2.

**Managed proposal:** `TableCreationResult with ReportId/TableId and native creation status; named title, tableName, rowCount and columnCount inputs.`

#### 2024 contract

~~~cpp
VARIANT OSTableUI::CreateReport(const VARIANT FAR&szReportTitle)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `szReportTitle` | `const VARIANT FAR&` | [in] | A null terminated string containing the title of the report. |

**Contract, including return/status semantics:** Creates a report with the specified title. Parameters [in] szReportTitle A null terminated string containing the title of the report. Return values 0 Create Report error. Other Report number. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object CreateReport(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [CreateReport](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L55)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-savereport"></a>

### 2. SaveReport

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga51c64edd9634d684b57d1360012c1129) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga51c64edd9634d684b57d1360012c1129) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga51c64edd9634d684b57d1360012c1129)<br>**Current:** Pending; T4.

**Managed proposal:** `TableOperationResult SaveReport; descriptive reportId/tableId/rowNumber/columnNumber inputs as applicable; typed formatting or size arguments; no external ref/out.`

#### 2024 contract

~~~cpp
void OSTableUI::SaveReport(const VARIANT FAR&ReportNo)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ReportNo` | `const VARIANT FAR&` | [in] | Providing the report number identifying the STAAD report which is to be saved(Type: Int). |

**Contract, including return/status semantics:** Saves the specified report along with all its tables. Parameters [in] ReportNo Providing the report number identifying the STAAD report which is to be saved(Type: Int). C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SaveReport(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SaveReport](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L99)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-savereportall"></a>

### 3. SaveReportAll

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gaf846474a8802876be358da52ff41ec21) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gaf846474a8802876be358da52ff41ec21) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gaf846474a8802876be358da52ff41ec21)<br>**Current:** Pending; T4.

**Managed proposal:** `TableOperationResult SaveReportAll; descriptive reportId/tableId/rowNumber/columnNumber inputs as applicable; typed formatting or size arguments; no external ref/out.`

#### 2024 contract

~~~cpp
void OSTableUI::SaveReportAll()
~~~

No native parameters.

**Contract, including return/status semantics:** Saves all the reports created. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SaveReportAll()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SaveAllReports](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L104)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-getreportcount"></a>

### 4. GetReportCount

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gaf44cf23f412b6f167687daeb2a6a27da) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gaf44cf23f412b6f167687daeb2a6a27da) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gaf44cf23f412b6f167687daeb2a6a27da)<br>**Current:** Pending; T2.

**Managed proposal:** `TableCountResult GetReportCount; Count and completion evidence, optional reportId as documented.`

#### 2024 contract

~~~cpp
VARIANT OSTableUI::GetReportCount()
~~~

No native parameters.

**Contract, including return/status semantics:** Returns the number of reports created. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object GetReportCount()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [GetReportCount](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L79)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-addtable"></a>

### 5. AddTable

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga290b4a43a1c62b9373448d09dfcf91bd) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga290b4a43a1c62b9373448d09dfcf91bd) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga290b4a43a1c62b9373448d09dfcf91bd)<br>**Current:** Pending; T2.

**Managed proposal:** `TableCreationResult with ReportId/TableId and native creation status; named title, tableName, rowCount and columnCount inputs.`

#### 2024 contract

~~~cpp
VARIANT OSTableUI::AddTable(const VARIANT FAR&ToReportNo,const VARIANT FAR&szTableName,const VARIANT FAR&RowNos,const VARIANT FAR&ColNos)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ToReportNo` | `const VARIANT FAR&` | [in] | Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . |
| `szTableName` | `const VARIANT FAR&` | [in] | Report Table Name(Type: String). |
| `RowNos` | `const VARIANT FAR&` | [in] | row count of the specified table(Type: long). |
| `ColNos` | `const VARIANT FAR&` | [in] | column count of the specified table(Type: long). |

**Contract, including return/status semantics:** Add or create a report data table and returns the table no for a specified table. Parameters [in] ToReportNo Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . [in] szTableName Report Table Name(Type: String). [in] RowNos row count of the specified table(Type: long). [in] ColNos column count of the specified table(Type: long). Return values 0 Create table error. Other Table number. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object AddTable(System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [AddTable](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L50)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-renametable"></a>

### 6. RenameTable

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gaa103253fe1917dd7858ef95d877a2aef) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gaa103253fe1917dd7858ef95d877a2aef) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gaa103253fe1917dd7858ef95d877a2aef)<br>**Current:** Pending; T2.

**Managed proposal:** `TableOperationResult RenameTable; descriptive reportId/tableId/rowNumber/columnNumber inputs as applicable; typed formatting or size arguments; no external ref/out.`

#### 2024 contract

~~~cpp
void OSTableUI::RenameTable(const VARIANT FAR&ReportNo,const VARIANT FAR&TableNo,const VARIANT FAR&szTableName)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ReportNo` | `const VARIANT FAR&` | [in] | Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . |
| `TableNo` | `const VARIANT FAR&` | [in] | Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . |
| `szTableName` | `const VARIANT FAR&` | [in] | Report Table Name(Type: String). |

**Contract, including return/status semantics:** Rename an existing report table. Parameters [in] ReportNo Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . [in] TableNo Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . [in] szTableName Report Table Name(Type: String). C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void RenameTable(System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [RenameTable](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L89)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-deletereport"></a>

### 7. DeleteReport

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gad1b6c0e8d133a68842788c5d6dc00420) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gad1b6c0e8d133a68842788c5d6dc00420) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gad1b6c0e8d133a68842788c5d6dc00420)<br>**Current:** Pending; T2.

**Managed proposal:** `TableOperationResult DeleteReport; descriptive reportId/tableId/rowNumber/columnNumber inputs as applicable; typed formatting or size arguments; no external ref/out.`

#### 2024 contract

~~~cpp
void OSTableUI::DeleteReport(const VARIANT FAR&FromReportNo)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `FromReportNo` | `const VARIANT FAR&` | [in] | Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . |

**Contract, including return/status semantics:** Delete an existing report. Parameters [in] FromReportNo Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void DeleteReport(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [DeleteReport](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L60)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-deletetable"></a>

### 8. DeleteTable

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga98f2843f774ff48bd424e52b750a8ea0) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga98f2843f774ff48bd424e52b750a8ea0) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga98f2843f774ff48bd424e52b750a8ea0)<br>**Current:** Pending; T2.

**Managed proposal:** `TableOperationResult DeleteTable; descriptive reportId/tableId/rowNumber/columnNumber inputs as applicable; typed formatting or size arguments; no external ref/out.`

#### 2024 contract

~~~cpp
void OSTableUI::DeleteTable(const VARIANT FAR&FromReportNo,const VARIANT FAR&TableNo)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `FromReportNo` | `const VARIANT FAR&` | [in] | Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . |
| `TableNo` | `const VARIANT FAR&` | [in] | Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . |

**Contract, including return/status semantics:** Delete an existing table from report data. Parameters [in] FromReportNo Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . [in] TableNo Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void DeleteTable(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [DeleteTable](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L65)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-resizetable"></a>

### 9. ResizeTable

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gaeeeabb0ab44d224ed382da6c67f5dcab) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gaeeeabb0ab44d224ed382da6c67f5dcab) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gaeeeabb0ab44d224ed382da6c67f5dcab)<br>**Current:** Pending; T2.

**Managed proposal:** `TableOperationResult ResizeTable; descriptive reportId/tableId/rowNumber/columnNumber inputs as applicable; typed formatting or size arguments; no external ref/out.`

#### 2024 contract

~~~cpp
void OSTableUI::ResizeTable(const VARIANT FAR&ReportNo,const VARIANT FAR&TableNo,const VARIANT FAR&RowNos,const VARIANT FAR&ColNos)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ReportNo` | `const VARIANT FAR&` | [in] | Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . |
| `TableNo` | `const VARIANT FAR&` | [in] | Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . |
| `RowNos` | `const VARIANT FAR&` | [in] | row count for specified table(Type: long). |
| `ColNos` | `const VARIANT FAR&` | [in] | column count for specified table(Type: long). |

**Contract, including return/status semantics:** Resize existing table by increasing numbers of rows and columns from report data. Parameters [in] ReportNo Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . [in] TableNo Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . [in] RowNos row count for specified table(Type: long). [in] ColNos column count for specified table(Type: long). C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void ResizeTable(System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [ResizeTable](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L94)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-savetable"></a>

### 10. SaveTable

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga47c79f2977543e071cf7f36693010c7e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga47c79f2977543e071cf7f36693010c7e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga47c79f2977543e071cf7f36693010c7e)<br>**Current:** Pending; T4.

**Managed proposal:** `TableOperationResult SaveTable; descriptive reportId/tableId/rowNumber/columnNumber inputs as applicable; typed formatting or size arguments; no external ref/out.`

#### 2024 contract

~~~cpp
void OSTableUI::SaveTable(const VARIANT FAR&ReportNo,const VARIANT FAR&TableNo)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ReportNo` | `const VARIANT FAR&` | [in] | Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . |
| `TableNo` | `const VARIANT FAR&` | [in] | Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . |

**Contract, including return/status semantics:** Save a report table. Parameters [in] ReportNo Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . [in] TableNo Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SaveTable(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SaveTable](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L109)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-gettablecount"></a>

### 11. GetTableCount

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gae03d9fe3b9c0e5ce9c6a7e44b286c9a2) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gae03d9fe3b9c0e5ce9c6a7e44b286c9a2) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gae03d9fe3b9c0e5ce9c6a7e44b286c9a2)<br>**Current:** Pending; T2.

**Managed proposal:** `TableCountResult GetTableCount; Count and completion evidence, optional reportId as documented.`

#### 2024 contract

~~~cpp
VARIANT OSTableUI::GetTableCount(const VARIANT FAR&OfReportNo)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `OfReportNo` | `const VARIANT FAR&` | [in] | Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . |

**Contract, including return/status semantics:** Get table count in a specified report.. Parameters [in] OfReportNo Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object GetTableCount(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [GetTableCount](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L84)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-getcellvalue"></a>

### 12. GetCellValue

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gab3fa167c0c15a7ab5cc620bbc6c758c7) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gab3fa167c0c15a7ab5cc620bbc6c758c7) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gab3fa167c0c15a7ab5cc620bbc6c758c7)<br>**Current:** Pending; T2.

**Managed proposal:** `TableCellValueResult GetCellValue(int reportId, int tableId, int rowNumber, int columnNumber); one-based coordinates; output Value string plus completion evidence.`

#### 2024 contract

~~~cpp
void OSTableUI::GetCellValue(const VARIANT FAR&ReportNo,const VARIANT FAR&TableNo,const VARIANT FAR&lRow,const VARIANT FAR&lCol,VARIANT FAR&varValueStr)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ReportNo` | `const VARIANT FAR&` | [in] | Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . |
| `TableNo` | `const VARIANT FAR&` | [in] | Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . |
| `lRow` | `const VARIANT FAR&` | [in] | row number for specified table, Start from 1(Type: long). |
| `lCol` | `const VARIANT FAR&` | [in] | column Number for specified table, Start from 1(Type: long). |
| `varValueStr` | `VARIANT FAR&` | [out] | Get cell value for specified cell in the table (Type: String). |

**Contract, including return/status semantics:** Get cell values for a specified table. Parameters [in] ReportNo Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . [in] TableNo Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . [in] lRow row number for specified table, Start from 1(Type: long). [in] lCol column Number for specified table, Start from 1(Type: long). [out] varValueStr Get cell value for specified cell in the table (Type: String). C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void GetCellValue(System.Object, System.Object, System.Object, System.Object, System.Object ByRef)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [GetCellValue](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L72)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-setcellvalue"></a>

### 13. SetCellValue

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga8fdf9c6f724e6a89e01e2c98fe9b342c) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga8fdf9c6f724e6a89e01e2c98fe9b342c) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga8fdf9c6f724e6a89e01e2c98fe9b342c)<br>**Current:** Pending; T2.

**Managed proposal:** `TableOperationResult SetCellValue; descriptive reportId/tableId/rowNumber/columnNumber inputs as applicable; typed formatting or size arguments; no external ref/out.`

#### 2024 contract

~~~cpp
void OSTableUI::SetCellValue(const VARIANT FAR&ReportNo,const VARIANT FAR&TableNo,const VARIANT FAR&lRow,const VARIANT FAR&lCol,const VARIANT FAR&varValue)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ReportNo` | `const VARIANT FAR&` | [in] | Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . |
| `TableNo` | `const VARIANT FAR&` | [in] | Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . |
| `lRow` | `const VARIANT FAR&` | [in] | row number for specified table, start from 1(Type: long). |
| `lCol` | `const VARIANT FAR&` | [in] | column number for specified table, start from 1(Type: long). |
| `varValue` | `const VARIANT FAR&` | [in] | set cell value for specified cell in the table(Type: String). |

**Contract, including return/status semantics:** Set cell values for a specified table. Parameters [in] ReportNo Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . [in] TableNo Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . [in] lRow row number for specified table, start from 1(Type: long). [in] lCol column number for specified table, start from 1(Type: long). [in] varValue set cell value for specified cell in the table(Type: String). C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SetCellValue(System.Object, System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetCellValue](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L154)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-setcolumnheader"></a>

### 14. SetColumnHeader

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gae61b64578ab572c3ef5fb3e6a3e12476) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gae61b64578ab572c3ef5fb3e6a3e12476) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gae61b64578ab572c3ef5fb3e6a3e12476)<br>**Current:** Pending; T2.

**Managed proposal:** `TableOperationResult SetColumnHeader; descriptive reportId/tableId/rowNumber/columnNumber inputs as applicable; typed formatting or size arguments; no external ref/out.`

#### 2024 contract

~~~cpp
void OSTableUI::SetColumnHeader(const VARIANT FAR&ReportNo,const VARIANT FAR&TableNo,const VARIANT FAR&lCol,const VARIANT FAR&varHeader)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ReportNo` | `const VARIANT FAR&` | [in] | Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . |
| `TableNo` | `const VARIANT FAR&` | [in] | Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . |
| `lCol` | `const VARIANT FAR&` | [in] | column number for specified table, start from 1(Type: long). |
| `varHeader` | `const VARIANT FAR&` | [in] | Column header for a specified column in table(Type: String). |

**Contract, including return/status semantics:** Sets column header for a specified table. Parameters [in] ReportNo Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . [in] TableNo Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . [in] lCol column number for specified table, start from 1(Type: long). [in] varHeader Column header for a specified column in table(Type: String). C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SetColumnHeader(System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetColumnHeader](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L159)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-setcolumnunitstring"></a>

### 15. SetColumnUnitString

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga3683e0ed94cd0586cf931812dc5ccf2b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga3683e0ed94cd0586cf931812dc5ccf2b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga3683e0ed94cd0586cf931812dc5ccf2b)<br>**Current:** Pending; T2.

**Managed proposal:** `TableOperationResult SetColumnUnitString; descriptive reportId/tableId/rowNumber/columnNumber inputs as applicable; typed formatting or size arguments; no external ref/out.`

#### 2024 contract

~~~cpp
void OSTableUI::SetColumnUnitString(const VARIANT FAR&ReportNo,const VARIANT FAR&TableNo,const VARIANT FAR&lCol,const VARIANT FAR&varUnitString)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ReportNo` | `const VARIANT FAR&` | [in] | Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . |
| `TableNo` | `const VARIANT FAR&` | [in] | Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . |
| `lCol` | `const VARIANT FAR&` | [in] | column number for specified table, start from 1(Type: long). |
| `varUnitString` | `const VARIANT FAR&` | [in] | set unit for the specific column of a specified table(Type: String). |

**Contract, including return/status semantics:** Sets unit for the specific column of a specified table. Parameters [in] ReportNo Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . [in] TableNo Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . [in] lCol column number for specified table, start from 1(Type: long). [in] varUnitString set unit for the specific column of a specified table(Type: String). C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SetColumnUnitString(System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetColumnUnitString](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L164)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-setrowheader"></a>

### 16. SetRowHeader

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gace3b6978fac76ae72edbb2d25937cd5f) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gace3b6978fac76ae72edbb2d25937cd5f) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gace3b6978fac76ae72edbb2d25937cd5f)<br>**Current:** Pending; T2.

**Managed proposal:** `TableOperationResult SetRowHeader; descriptive reportId/tableId/rowNumber/columnNumber inputs as applicable; typed formatting or size arguments; no external ref/out.`

#### 2024 contract

~~~cpp
void OSTableUI::SetRowHeader(const VARIANT FAR&ReportNo,const VARIANT FAR&TableNo,const VARIANT FAR&lRow,const VARIANT FAR&varHeader)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ReportNo` | `const VARIANT FAR&` | [in] | Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . |
| `TableNo` | `const VARIANT FAR&` | [in] | Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . |
| `lRow` | `const VARIANT FAR&` | [in] | row number for specified table, start from 1(Type: long). |
| `varHeader` | `const VARIANT FAR&` | [in] | set header for the specific row of a specified table(Type: String). |

**Contract, including return/status semantics:** Sets row header for the specific row of a specified table. Parameters [in] ReportNo Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . [in] TableNo Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . [in] lRow row number for specified table, start from 1(Type: long). [in] varHeader set header for the specific row of a specified table(Type: String). C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SetRowHeader(System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetRowHeader](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L169)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-setcelltextcolor"></a>

### 17. SetCellTextColor

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga82643e4ba7ce238d2f31abd50734bea9) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga82643e4ba7ce238d2f31abd50734bea9) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga82643e4ba7ce238d2f31abd50734bea9)<br>**Current:** Pending; T3.

**Managed proposal:** `TableOperationResult SetCellTextColor; descriptive reportId/tableId/rowNumber/columnNumber inputs as applicable; typed formatting or size arguments; no external ref/out.`

#### 2024 contract

~~~cpp
void OSTableUI::SetCellTextColor(const VARIANT FAR&ReportNo,const VARIANT FAR&TableNo,const VARIANT FAR&lRow,const VARIANT FAR&lCol,const VARIANT FAR&lRedValue,const VARIANT FAR&lGreenValue,const VARIANT FAR&lBlueValue)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ReportNo` | `const VARIANT FAR&` | [in] | Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . |
| `TableNo` | `const VARIANT FAR&` | [in] | Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . |
| `lRow` | `const VARIANT FAR&` | [in] | row number for specified table, start from 1(Type: long). |
| `lCol` | `const VARIANT FAR&` | [in] | column number for specified table, start from 1(Type: long). |
| `lRedValue` | `const VARIANT FAR&` | [in] | An integer between 0 and 255 that represents the intensity of red in the color for the text(Type: long). |
| `lGreenValue` | `const VARIANT FAR&` | [in] | An integer between 0 and 255 that represents the intensity of green in the color for the text(Type: long). |
| `lBlueValue` | `const VARIANT FAR&` | [in] | An integer between 0 and 255 that represents the intensity of blue in the color for the text(Type: long). |

**Contract, including return/status semantics:** Sets the color of the text to be displayed in a particular cell. By default, the color is always set to black. Parameters [in] ReportNo Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . [in] TableNo Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . [in] lRow row number for specified table, start from 1(Type: long). [in] lCol column number for specified table, start from 1(Type: long). [in] lRedValue An integer between 0 and 255 that represents the intensity of red in the color for the text(Type: long). [in] lGreenValue An integer between 0 and 255 that represents the intensity of green in the color for the text(Type: long). [in] lBlueValue An integer between 0 and 255 that represents the intensity of blue in the color for the text(Type: long). C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SetCellTextColor(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetCellTextColor](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L119)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-setcelltextbold"></a>

### 18. SetCellTextBold

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga276740e7efec12ecc0778ca11031836e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga276740e7efec12ecc0778ca11031836e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga276740e7efec12ecc0778ca11031836e)<br>**Current:** Pending; T3.

**Managed proposal:** `TableOperationResult SetCellTextBold; descriptive reportId/tableId/rowNumber/columnNumber inputs as applicable; typed formatting or size arguments; no external ref/out.`

#### 2024 contract

~~~cpp
void OSTableUI::SetCellTextBold(const VARIANT FAR&ReportNo,const VARIANT FAR&TableNo,const VARIANT FAR&lRow,const VARIANT FAR&lCol)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ReportNo` | `const VARIANT FAR&` | [in] | Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . |
| `TableNo` | `const VARIANT FAR&` | [in] | Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . |
| `lRow` | `const VARIANT FAR&` | [in] | row number for specified table, start from 1(Type: long). |
| `lCol` | `const VARIANT FAR&` | [in] | column number for specified table, start from 1(Type: long). |

**Contract, including return/status semantics:** Sets the text in a given row and column to bold. Parameters [in] ReportNo Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . [in] TableNo Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . [in] lRow row number for specified table, start from 1(Type: long). [in] lCol column number for specified table, start from 1(Type: long). C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SetCellTextBold(System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetCellTextBold](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L114)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-setcelltextitalic"></a>

### 19. SetCellTextItalic

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga32f5eff8ba94c8258edd82a9e5b0f40f) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga32f5eff8ba94c8258edd82a9e5b0f40f) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga32f5eff8ba94c8258edd82a9e5b0f40f)<br>**Current:** Pending; T3.

**Managed proposal:** `TableOperationResult SetCellTextItalic; descriptive reportId/tableId/rowNumber/columnNumber inputs as applicable; typed formatting or size arguments; no external ref/out.`

#### 2024 contract

~~~cpp
void OSTableUI::SetCellTextItalic(const VARIANT FAR&ReportNo,const VARIANT FAR&TableNo,const VARIANT FAR&lRow,const VARIANT FAR&lCol)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ReportNo` | `const VARIANT FAR&` | [in] | Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . |
| `TableNo` | `const VARIANT FAR&` | [in] | Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . |
| `lRow` | `const VARIANT FAR&` | [in] | row number for specified table, start from 1(Type: long). |
| `lCol` | `const VARIANT FAR&` | [in] | column number for specified table, start from 1(Type: long). |

**Contract, including return/status semantics:** Italicizes the text in a given row and column. Parameters [in] ReportNo Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . [in] TableNo Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . [in] lRow row number for specified table, start from 1(Type: long). [in] lCol column number for specified table, start from 1(Type: long). C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SetCellTextItalic(System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetCellTextItalic](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L129)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-setcelltextunderline"></a>

### 20. SetCellTextUnderline

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga6230aedbfdf0d9f8d6466c6a4927f0fb) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga6230aedbfdf0d9f8d6466c6a4927f0fb) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga6230aedbfdf0d9f8d6466c6a4927f0fb)<br>**Current:** Pending; T3.

**Managed proposal:** `TableOperationResult SetCellTextUnderline; descriptive reportId/tableId/rowNumber/columnNumber inputs as applicable; typed formatting or size arguments; no external ref/out.`

#### 2024 contract

~~~cpp
void OSTableUI::SetCellTextUnderline(const VARIANT FAR&ReportNo,const VARIANT FAR&TableNo,const VARIANT FAR&lRow,const VARIANT FAR&lCol)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ReportNo` | `const VARIANT FAR&` | [in] | Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . |
| `TableNo` | `const VARIANT FAR&` | [in] | Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . |
| `lRow` | `const VARIANT FAR&` | [in] | row number for specified table, start from 1(Type: long). |
| `lCol` | `const VARIANT FAR&` | [in] | column number for specified table, start from 1(Type: long). |

**Contract, including return/status semantics:** Underlines the text in a given row and column. Parameters [in] ReportNo Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . [in] TableNo Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . [in] lRow row number for specified table, start from 1(Type: long). [in] lCol column number for specified table, start from 1(Type: long). C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SetCellTextUnderline(System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetCellTextUnderline](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L144)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-setcelltexthorzalignment"></a>

### 21. SetCellTextHorzAlignment

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga4b2886afe16ebd9632f97fe4b5637e8d) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga4b2886afe16ebd9632f97fe4b5637e8d) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga4b2886afe16ebd9632f97fe4b5637e8d)<br>**Current:** Pending; T3.

**Managed proposal:** `TableOperationResult SetCellTextHorzAlignment; descriptive reportId/tableId/rowNumber/columnNumber inputs as applicable; typed formatting or size arguments; no external ref/out.`

#### 2024 contract

~~~cpp
void OSTableUI::SetCellTextHorzAlignment(const VARIANT FAR&ReportNo,const VARIANT FAR&TableNo,const VARIANT FAR&lRow,const VARIANT FAR&lCol,const VARIANT FAR&lAlign)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ReportNo` | `const VARIANT FAR&` | [in] | Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . |
| `TableNo` | `const VARIANT FAR&` | [in] | Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . |
| `lRow` | `const VARIANT FAR&` | [in] | row number for specified table, start from 1(Type: long). |
| `lCol` | `const VARIANT FAR&` | [in] | column number for specified table, start from 1(Type: long). |
| `lAlign` | `const VARIANT FAR&` | [in] | Sets the text in a particular row and column to a specified horizontal alignment(Type: long). The possible values are:0 = left; 1 = center; 2 = right |

**Contract, including return/status semantics:** Sets the text in a particular row and column to a specified horizontal alignment. By default, all the text is right justified. Parameters [in] ReportNo Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . [in] TableNo Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . [in] lRow row number for specified table, start from 1(Type: long). [in] lCol column number for specified table, start from 1(Type: long). [in] lAlign Sets the text in a particular row and column to a specified horizontal alignment(Type: long). The possible values are:0 = left; 1 = center; 2 = right C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SetCellTextHorzAlignment(System.Object, System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetCellTextHorizontalAlignment](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L124)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-setcelltextvertalignment"></a>

### 22. SetCellTextVertAlignment

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga9846d7eeef4bd24a9eecffa3af7452f1) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga9846d7eeef4bd24a9eecffa3af7452f1) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga9846d7eeef4bd24a9eecffa3af7452f1)<br>**Current:** Pending; T3.

**Managed proposal:** `TableOperationResult SetCellTextVertAlignment; descriptive reportId/tableId/rowNumber/columnNumber inputs as applicable; typed formatting or size arguments; no external ref/out.`

#### 2024 contract

~~~cpp
void OSTableUI::SetCellTextVertAlignment(const VARIANT FAR&ReportNo,const VARIANT FAR&TableNo,const VARIANT FAR&lRow,const VARIANT FAR&lCol,const VARIANT FAR&lAlign)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ReportNo` | `const VARIANT FAR&` | [in] | Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . |
| `TableNo` | `const VARIANT FAR&` | [in] | Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . |
| `lRow` | `const VARIANT FAR&` | [in] | row number for specified table, start from 1(Type: long). |
| `lCol` | `const VARIANT FAR&` | [in] | column number for specified table, start from 1(Type: long). |
| `lAlign` | `const VARIANT FAR&` | [in] | Sets the text in a particular row and column to a specified vertical alignment(Type: long). The possible values are:0 = top; 4 = center; 8 = bottom |

**Contract, including return/status semantics:** Sets the text in a particular row and column to a specified vertical alignment. By default, all the text is top justified. Parameters [in] ReportNo Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . [in] TableNo Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . [in] lRow row number for specified table, start from 1(Type: long). [in] lCol column number for specified table, start from 1(Type: long). [in] lAlign Sets the text in a particular row and column to a specified vertical alignment(Type: long). The possible values are:0 = top; 4 = center; 8 = bottom C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SetCellTextVertAlignment(System.Object, System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetCellTextVerticalAlignment](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L149)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-setcelltextsize"></a>

### 23. SetCellTextSize

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga83f1ee3a8b9c5f964284a575b7c33811) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga83f1ee3a8b9c5f964284a575b7c33811) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga83f1ee3a8b9c5f964284a575b7c33811)<br>**Current:** Pending; T3.

**Managed proposal:** `TableOperationResult SetCellTextSize; descriptive reportId/tableId/rowNumber/columnNumber inputs as applicable; typed formatting or size arguments; no external ref/out.`

#### 2024 contract

~~~cpp
void OSTableUI::SetCellTextSize(const VARIANT FAR&ReportNo,const VARIANT FAR&TableNo,const VARIANT FAR&lRow,const VARIANT FAR&lCol,const VARIANT FAR&dSize)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ReportNo` | `const VARIANT FAR&` | [in] | Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . |
| `TableNo` | `const VARIANT FAR&` | [in] | Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . |
| `lRow` | `const VARIANT FAR&` | [in] | row number for specified table, start from 1(Type: long). |
| `lCol` | `const VARIANT FAR&` | [in] | column number for specified table, start from 1(Type: long). |
| `dSize` | `const VARIANT FAR&` | [in] | Containing the size of the font to set the text to(Type: Double). |

**Contract, including return/status semantics:** Sets the text in a particular row and column to a certain font size. The font sizes are equivalent to the ones used in Microsoft Word. Parameters [in] ReportNo Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . [in] TableNo Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . [in] lRow row number for specified table, start from 1(Type: long). [in] lCol column number for specified table, start from 1(Type: long). [in] dSize Containing the size of the font to set the text to(Type: Double). C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SetCellTextSize(System.Object, System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetCellTextSize](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L134)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="table-setcelltextsizeall"></a>

### 24. SetCellTextSizeAll

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga03e55a45d5ec4b92ecf64ab3b696f2e9) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga03e55a45d5ec4b92ecf64ab3b696f2e9) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga03e55a45d5ec4b92ecf64ab3b696f2e9)<br>**Current:** Pending; T3.

**Managed proposal:** `TableOperationResult SetCellTextSizeAll; descriptive reportId/tableId/rowNumber/columnNumber inputs as applicable; typed formatting or size arguments; no external ref/out.`

#### 2024 contract

~~~cpp
void OSTableUI::SetCellTextSizeAll(const VARIANT FAR&ReportNo,const VARIANT FAR&TableNo,const VARIANT FAR&dSize)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ReportNo` | `const VARIANT FAR&` | [in] | Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . |
| `TableNo` | `const VARIANT FAR&` | [in] | Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . |
| `dSize` | `const VARIANT FAR&` | [in] | Containing the size of the font to set the entire table to(Type: Double). |

**Contract, including return/status semantics:** Sets the text in the entire table to FontSize. The font sizes are equivalent to the ones used in Microsoft Word. Parameters [in] ReportNo Report number(Type: int), It can be obtained be calling OSTableUI::CreateReport . [in] TableNo Table number in specified report (Type: long), It can be obtained be calling OSTableUI::AddTable . [in] dSize Containing the size of the font to set the entire table to(Type: Double). C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SetCellTextSizeAll(System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetCellTextSizeForTable](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L139)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

## 4. Complete private-source overload mapping

| Main managed signature | Return | Native targets / delegation | Reference |
| --- | --- | --- | --- |
| `AddTable(int reportId, string tableName, int numRows, int numCols)` | `int` | `AddTable` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L28) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L48) |
| `CreateReport(string title)` | `int` | `CreateReport` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L35) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L53) |
| `DeleteReport(int reportId)` | `void` | `DeleteReport` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L41) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L58) |
| `DeleteTable(int reportId, int tableId)` | `void` | `DeleteTable` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L48) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L63) |
| `GetCellValue(int reportId, int cellId, int iRow, int iCol)` | `string` | `GetCellValue` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L58) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L68) |
| `GetReportCount()` | `int` | `GetReportCount` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L64) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L77) |
| `GetTableCount(int reportId)` | `int` | `GetTableCount` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L71) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L82) |
| `RenameTable(int reportId, int tableId, string newName)` | `void` | `RenameTable` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L79) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L87) |
| `ResizeTable(int reportId, int tableId, int numRows, int numCols)` | `void` | `ResizeTable` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L88) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L92) |
| `SaveReport(int reportId)` | `void` | `SaveReport` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L94) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L97) |
| `SaveAllReports()` | `void` | `SaveReportAll` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L99) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L102) |
| `SaveTable(int reportId, int tableId)` | `void` | `SaveTable` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L106) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L107) |
| `SetCellTextBold(int reportId, int tableId, int iRow, int iCol)` | `void` | `SetCellTextBold` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L111) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L112) |
| `SetCellTextColor(int reportId, int tableId, int iRow, int iCol, int red, int green, int blue)` | `void` | `SetCellTextColor` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L119) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L117) |
| `SetCellTextHorizontalAlignment(int reportId, int tableId, int iRow, int iCol, HorizontalAlignment alignment)` | `void` | `SetCellTextHorzAlignment` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L124) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L122) |
| `SetCellTextItalic(int reportId, int tableId, int iRow, int iCol)` | `void` | `SetCellTextItalic` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L129) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L127) |
| `SetCellTextSize(int reportId, int tableId, int iRow, int iCol, double fontSize)` | `void` | `SetCellTextSize` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L134) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L132) |
| `SetCellTextSizeForTable(int reportId, int tableId, double fontSize)` | `void` | `SetCellTextSizeAll` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L139) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L137) |
| `SetCellTextUnderline(int reportId, int tableId, int iRow, int iCol)` | `void` | `SetCellTextUnderline` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L144) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L142) |
| `SetCellTextVerticalAlignment(int reportId, int tableId, int iRow, int iCol, VerticalAlignment alignment)` | `void` | `SetCellTextVertAlignment` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L149) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L147) |
| `SetCellValue(int reportId, int cellId, int iRow, int iCol, string value)` | `void` | `SetCellValue` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L154) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L152) |
| `SetColumnHeader(int reportId, int tableId, int iCol, string headerText)` | `void` | `SetColumnHeader` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L159) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L157) |
| `SetColumnUnitString(int reportId, int tableId, int iCol, string unitString)` | `void` | `SetColumnUnitString` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L164) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L162) |
| `SetRowHeader(int reportId, int tableId, int iRow, string headerText)` | `void` | `SetRowHeader` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L169) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L167) |
| `SetCellTextColor(int reportId, int tableId, int iRow, int iCol, string hexColor)` | `void` | `SetCellTextColor` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L177) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L177) |
| `SetCellTextColor(int reportId, int tableId, int iRow, int iCol, System.Windows.Media.Color color)` | `void` | `SetCellTextColor` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L182) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L172) |
| `SetCellTextColor(int reportId, int tableId, int iRow, int iCol, int argbValue)` | `void` | `SetCellTextColor` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L188) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L195) |
| `SetCellTextColor(int reportId, int tableId, int iRow, int iCol, byte a, byte r, byte g, byte b)` | `void` | `SetCellTextColor` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L193) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L206) |
| `SetCellTextColorHsl(int reportId, int tableId, int iRow, int iCol, float hue, float saturation, float lightness)` | `void` | `SetCellTextColor` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs#L201) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L212) |

Wrapper and ComObject properties are binding contracts, not domain methods. Constructors and private helpers are not counted as public overloads. Existing source signatures above are comparison evidence, not approved signatures for a new public port.

## 5. Passive metadata, constants and fingerprints

2025 and 2026 IOSTableUI method signatures, parameter metadata and referenced enum constants match. Both assemblies have the same identity but different binary hashes; each was inspected in a separate reflection-only process. No native object was activated. The expected 2024 STAAD interop DLL was unavailable for passive inspection.

| Year | Assembly SHA-256 |
| --- | --- |
| 2025 | CC724993AC9119138B7323E8C684997257E2851DE62E1551BA5D6D67A9DADC19 |
| 2026 | 6D153E80ED356A1B167ABB2EF19227FCFF510707A578AD304154652CC6C1F6D4 |

### 2024 help evidence

| Page | SHA-256 | Summary / details |
| --- | --- | --- |
| [Tables](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html) | 71692d0e48efae4416344ab753bedff3869693aadad7f59a15abb3bcf37b2c3a | 24 / 24 |
| [Native class listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_table_u_i.html) | bbeb8c2c85bc4e08b63d5bfd04dd1415edaeac55b43af70bead427755719636f | 33 names |
| [Native header listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/ostableui_8h_source.html) | 52345629b9c595c6092c6ecea9f8a2efa0f0a7be38859e31af2e5eaf4f3a95e9 | Supporting declaration evidence |

### 2025 help evidence

| Page | SHA-256 | Summary / details |
| --- | --- | --- |
| [Tables](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html) | 4ecb0e67dd2b71a5a5423b09b407d3ecf573ec3f1437f74f23a166e577b234ce | 24 / 24 |
| [Native class listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_table_u_i.html) | 589df97a2628f0cc058214e949d84d7dd05de9fbb08a6344cac1a194a4768271 | 33 names |
| [Native header listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/ostableui_8h_source.html) | 1dfbf7b4c4e77f91fe413fbdbb50123d676e2a6772c2fadad6bb606ee76269f6 | Supporting declaration evidence |

### 2026 help evidence

| Page | SHA-256 | Summary / details |
| --- | --- | --- |
| [Tables](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html) | 18734b76af17c2091351e92138a4e62b05a246012a38bfbebe695f885989e020 | 24 / 24 |
| [Native class listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_table_u_i.html) | c5e2d4cad36a5f6c6a89ac42775b40d87448d7d6ba5022393991d1b241a97713 | 33 names |
| [Native header listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/ostableui_8h_source.html) | f7e559a61dada42d49eec33ff3af53fce935b24e02fe6c90ba4df4eb346f83de | Supporting declaration evidence |
