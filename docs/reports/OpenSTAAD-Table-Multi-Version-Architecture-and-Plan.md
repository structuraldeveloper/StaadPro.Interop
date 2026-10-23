# OpenSTAAD Table multi-version comparison, architecture and implementation plan

**Reviewed:** 23 October 2026<br>
**Baseline:** main at 1d8285ce66c95c60da55530d4c721fb7274893da<br>
**Status:** Documentation and architecture only. Table domain functions remain pending.

## 1. Findings

The installed manuals document the same **24 detailed Table functions** in every year. Declarations and non-example contracts match. All 24 examples change from 2024 to 2025; 2025/2026 examples match. No year-specific implementation is justified.

The current IOSTable / OSTableAdapter provide only native binding and Wrapper/ComObject properties. The main project has 29 matching interface/public-adapter overloads, targeting all 24 native names. Five extra overloads provide color conveniences. Native coverage in the main source does not imply correctness: its cell-coordinate XML contradicts the manuals, and its color fallback silently changes invalid input to black.

| Measure | Finding |
| --- | --- |
| Native names (2024 / 2025 / 2026) | 24 / 24 / 24 |
| Main interface / public adapter overloads | 29 / 29 |
| Main private adapter helpers | 0 |
| Main distinct native targets | 24 |
| Native names without main-adapter target | 0 |
| Current typed domain methods | 0 (binding-only placeholder) |
| Common signature changes, 2024 to 2025 | 0 |
| Non-example text changes, 2024 to 2025 | 0 |
| Example changes, 2024 to 2025 | 24 |
| Observed changes, 2025 to 2026 | 0 |
| Native class names / excluded helpers (2026) | 33 / 9 |


## 2. References and review method

| Version / installed help | Documented build |
| --- | --- |
| [STAAD.Pro 2024 Table](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html) | 24.00.02.354 |
| [STAAD.Pro 2025 Table](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html) | 25.00.01.424 |
| [STAAD.Pro 2026 Table](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html) | 26.00.00.340 |

Read-only source: [main interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSTable.cs) and [main adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs).

The [complete inventory](OpenSTAAD-Table-API-Coverage-and-Contract-Inventory.md) includes each declaration, parameter table, return convention, value table, source overload and passive metadata. The [comparison JSON](data/OpenSTAAD-Table-2024-2026-Comparison.json) preserves all per-year contracts, differences and source/help fingerprints.

The single group page, native class/header and other group pages were reconciled. Nine class-list helpers are implementation/report-map lifecycle operations, excluded from the public plan. The 2025/2026 IOSTableUI signatures and parameter metadata match, without metadata-only automation names. The native API is not an Excel or general-purpose data-table library.

Reflection-only inspection was performed in separate processes; no COM object was activated, report created or report file saved. No corresponding 2024 interop DLL was available at the expected path.

## 3. Complete cross-version function matrix

| Native Table function | Native return | 2024 / 2025 / 2026 | Main native target | Current typed / plan |
| --- | --- | --- | --- | --- |
| `CreateReport` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gae01ef57411174492e697268fdc10aeab) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gae01ef57411174492e697268fdc10aeab) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gae01ef57411174492e697268fdc10aeab) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L55) | Pending / T2 |
| `SaveReport` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga51c64edd9634d684b57d1360012c1129) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga51c64edd9634d684b57d1360012c1129) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga51c64edd9634d684b57d1360012c1129) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L99) | Pending / T4 |
| `SaveReportAll` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gaf846474a8802876be358da52ff41ec21) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gaf846474a8802876be358da52ff41ec21) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gaf846474a8802876be358da52ff41ec21) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L104) | Pending / T4 |
| `GetReportCount` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gaf44cf23f412b6f167687daeb2a6a27da) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gaf44cf23f412b6f167687daeb2a6a27da) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gaf44cf23f412b6f167687daeb2a6a27da) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L79) | Pending / T2 |
| `AddTable` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga290b4a43a1c62b9373448d09dfcf91bd) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga290b4a43a1c62b9373448d09dfcf91bd) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga290b4a43a1c62b9373448d09dfcf91bd) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L50) | Pending / T2 |
| `RenameTable` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gaa103253fe1917dd7858ef95d877a2aef) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gaa103253fe1917dd7858ef95d877a2aef) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gaa103253fe1917dd7858ef95d877a2aef) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L89) | Pending / T2 |
| `DeleteReport` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gad1b6c0e8d133a68842788c5d6dc00420) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gad1b6c0e8d133a68842788c5d6dc00420) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gad1b6c0e8d133a68842788c5d6dc00420) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L60) | Pending / T2 |
| `DeleteTable` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga98f2843f774ff48bd424e52b750a8ea0) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga98f2843f774ff48bd424e52b750a8ea0) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga98f2843f774ff48bd424e52b750a8ea0) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L65) | Pending / T2 |
| `ResizeTable` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gaeeeabb0ab44d224ed382da6c67f5dcab) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gaeeeabb0ab44d224ed382da6c67f5dcab) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gaeeeabb0ab44d224ed382da6c67f5dcab) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L94) | Pending / T2 |
| `SaveTable` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga47c79f2977543e071cf7f36693010c7e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga47c79f2977543e071cf7f36693010c7e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga47c79f2977543e071cf7f36693010c7e) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L109) | Pending / T4 |
| `GetTableCount` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gae03d9fe3b9c0e5ce9c6a7e44b286c9a2) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gae03d9fe3b9c0e5ce9c6a7e44b286c9a2) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gae03d9fe3b9c0e5ce9c6a7e44b286c9a2) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L84) | Pending / T2 |
| `GetCellValue` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gab3fa167c0c15a7ab5cc620bbc6c758c7) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gab3fa167c0c15a7ab5cc620bbc6c758c7) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gab3fa167c0c15a7ab5cc620bbc6c758c7) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L72) | Pending / T2 |
| `SetCellValue` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga8fdf9c6f724e6a89e01e2c98fe9b342c) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga8fdf9c6f724e6a89e01e2c98fe9b342c) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga8fdf9c6f724e6a89e01e2c98fe9b342c) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L154) | Pending / T2 |
| `SetColumnHeader` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gae61b64578ab572c3ef5fb3e6a3e12476) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gae61b64578ab572c3ef5fb3e6a3e12476) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gae61b64578ab572c3ef5fb3e6a3e12476) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L159) | Pending / T2 |
| `SetColumnUnitString` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga3683e0ed94cd0586cf931812dc5ccf2b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga3683e0ed94cd0586cf931812dc5ccf2b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga3683e0ed94cd0586cf931812dc5ccf2b) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L164) | Pending / T2 |
| `SetRowHeader` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#gace3b6978fac76ae72edbb2d25937cd5f) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#gace3b6978fac76ae72edbb2d25937cd5f) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#gace3b6978fac76ae72edbb2d25937cd5f) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L169) | Pending / T2 |
| `SetCellTextColor` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga82643e4ba7ce238d2f31abd50734bea9) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga82643e4ba7ce238d2f31abd50734bea9) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga82643e4ba7ce238d2f31abd50734bea9) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L119) | Pending / T3 |
| `SetCellTextBold` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga276740e7efec12ecc0778ca11031836e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga276740e7efec12ecc0778ca11031836e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga276740e7efec12ecc0778ca11031836e) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L114) | Pending / T3 |
| `SetCellTextItalic` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga32f5eff8ba94c8258edd82a9e5b0f40f) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga32f5eff8ba94c8258edd82a9e5b0f40f) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga32f5eff8ba94c8258edd82a9e5b0f40f) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L129) | Pending / T3 |
| `SetCellTextUnderline` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga6230aedbfdf0d9f8d6466c6a4927f0fb) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga6230aedbfdf0d9f8d6466c6a4927f0fb) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga6230aedbfdf0d9f8d6466c6a4927f0fb) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L144) | Pending / T3 |
| `SetCellTextHorzAlignment` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga4b2886afe16ebd9632f97fe4b5637e8d) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga4b2886afe16ebd9632f97fe4b5637e8d) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga4b2886afe16ebd9632f97fe4b5637e8d) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L124) | Pending / T3 |
| `SetCellTextVertAlignment` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga9846d7eeef4bd24a9eecffa3af7452f1) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga9846d7eeef4bd24a9eecffa3af7452f1) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga9846d7eeef4bd24a9eecffa3af7452f1) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L149) | Pending / T3 |
| `SetCellTextSize` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga83f1ee3a8b9c5f964284a575b7c33811) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga83f1ee3a8b9c5f964284a575b7c33811) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga83f1ee3a8b9c5f964284a575b7c33811) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L134) | Pending / T3 |
| `SetCellTextSizeAll` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___table.html#ga03e55a45d5ec4b92ecf64ab3b696f2e9) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___table.html#ga03e55a45d5ec4b92ecf64ab3b696f2e9) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___table.html#ga03e55a45d5ec4b92ecf64ab3b696f2e9) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSTableAdapter.cs#L139) | Pending / T3 |


## 4. Version and contract findings

All common declarations and non-example contracts match across 2024, 2025 and 2026. No per-year service split is justified by these references.

### 4.1. Coordinates and identifiers

Bentley explicitly documents **one-based row and column numbers**. The main IOSTable XML uses zero-based terminology; its implementation forwards the numbers unchanged. The new API must use rowNumber/columnNumber and document the one-based native convention consistently. If a zero-based convenience is later justified, give it an explicit name/type and checked translation.

Report IDs and table IDs are returned native identifiers, not array offsets. GetCellValue/SetCellValue call their second source parameter cellId, but it is a **table ID**. Use reportId and tableId publicly. Do not derive IDs by assuming contiguous values after deletion.

CreateReport/AddTable use zero as a failure sentinel and otherwise return an identifier. Count queries use their own return conventions. Native void formatting/cell operations do not return a success flag, so there is no hidden 1/0 to normalize.

### 4.2. Formatting values

Horizontal alignment uses Left=0, Center=1, Right=2. Vertical alignment uses Top=0, Center=4, Bottom=8. These are different native vocabularies; do not reuse arbitrary WPF alignment enum values.

RGB channel values are integers with explicit 0–255 validation. Native Table formatting does not define alpha transparency. Source ARGB/WPF conveniences discard alpha; preserve that behavior only as a clearly documented convenience, or prefer an explicit RgbColor class.

The source hex/named-color overload uses WPF ColorConverter and replaces FormatException with black. Reject invalid color input before dispatch and report it clearly. HSL conversion currently clamps values; choose and document finite/range validation and a hue wrapping policy rather than silently changing malformed requests. Keep the baseline API independent of WPF merely for parsing a color.

Font size, column-unit labels and headers are presentation settings. A unit string label does not convert the cell's numerical value. Verify font-size units and any allowed ranges from native fixtures when the help does not specify them.

### 4.3. Persistence and lifetime

SaveReport, SaveReportAll and SaveTable persist native report artifacts using the installed application's semantics. They are not equivalent to saving the STAAD model. The API does not expose an arbitrary output path for these calls; do not invent one in a managed signature without a separately implemented export.

Report/table handles can become stale after deletion, model changes or application closure. Capture diagnostics identifying the acquired model/binding. Do not recreate a deleted report behind a caller's back or silently redirect a save.

## 5. Main-source mapping and reuse

| Managed source name | Native target / treatment |
| --- | --- |
| SaveAllReports | SaveReportAll; preserve the descriptive managed name. |
| SetCellTextHorizontalAlignment | SetCellTextHorzAlignment; retain typed validated enum. |
| SetCellTextVerticalAlignment | SetCellTextVertAlignment; retain 0/4/8 mapping. |
| SetCellTextSizeForTable | SetCellTextSizeAll; distinguish whole-table and single-cell changes. |
| GetCellValue | Native output string currently copied from a local reference; return named status/data result. |
| RGB plus five color conveniences | Route through one validated RGB path; avoid WPF dependency and silent fallback. |
| Report/table creation, counts and cell/header functions | Source intent is reusable; preserve native sentinels and one-based semantics. |

There are no private adapter helper methods. The source's fluent/void style does not preserve operation status. For void operations return a named result indicating dispatch completion; use an explicit cell read-back only where meaningful, without claiming atomicity or that formatting was verified.

## 6. Proposed managed architecture

Keep IOSTable and a single non-partial OSTableAdapter. Public TableCreationResult, TableCountResult, TableCellValueResult, TableOperationResult and formatting request/value types belong under Models/Table. Verified alignment enums belong under Enums/Table. Internal invocation/coordinate/color conversion belongs under Services/Table.

TableOperationResult distinguishes DispatchCompleted, ConfirmedSuccess where the native contract supports it, NativeFailure, and unresolved effect after a transport problem. Query results publish their data only when the applicable native operation completed successfully. Empty cell text is legitimate data and must not double as a failure sentinel.

Use int reportId/tableId, rowNumber/columnNumber, rowCount/columnCount, string title/tableName/headerText/unitText, double fontSize and a validated RGB value object. Creation results include the returned ID and status. No new exposed ref/out, tuples, native holder objects or per-method boilerplate services are needed.

Offer entity-independent scalar methods first. A bulk fill/format convenience is useful only with a defined order, one-time materialization and per-cell partial-completion results. The native API provides no transaction; no automatic destructive rollback is planned.

The internal factory uses captured RawTable and wrapper.StaadVersion for identity/diagnostics. Current evidence supports one shared implementation. Missing binding or a disconnected application yields a capability/transport diagnostic, not a fallback to another model.

~~~mermaid
flowchart TD
    A["Capture wrapper and Table binding"] --> B["Validate report/table identity and one-based coordinates"]
    B --> C["Normalize text, size or RGB request"]
    C --> D["Invoke once on owning COM apartment"]
    D --> E{"Native result kind?"}
    E -->|Identifier or count| F["Validate sentinel and publish typed data"]
    E -->|Void or output text| G["Preserve dispatch evidence and output"]
    F --> H["Return named result"]
    G --> H
~~~

## 7. Ordered implementation plan

| ID | Scope | Exit criteria |
| --- | --- | --- |
| T1 | Common results, report/table identity conventions, one-based coordinates and color/alignment models | API/XML contracts reviewed; WPF-free baseline; strict native value handling. |
| T2 | Report/table create/delete/count/rename/resize and cell/header/unit text | All native IDs/sentinels and stale-object paths tested; cellId renamed appropriately. |
| T3 | Cell bold/italic/underline/color/alignment/font operations | Correct native enum values; no silent black fallback or alpha claim; whole-table distinction explicit. |
| T4 | Report/table save operations and source convenience overloads | Persistence location/overwrite behavior qualified and documented; partial completion explicit. |
| T5 | Cross-version acceptance and packaging | All 24 native names reconciled, full XML/examples included and package installation checked. |

Every native row is assigned in the matrix. T2 includes Save-independent lifecycle and cell functions; T3 includes the color conveniences after the validated baseline exists.

## 8. Test and IntelliSense plan

Test zero/negative/overflow identifiers and counts, row/column zero and boundaries, native one-based forwarding, deleted IDs, empty text, Unicode, null versus empty values, resize truncation, RGB boundaries, unknown alignment values, invalid color syntax, NaN/infinite HSL/font values and single enumeration for any bulk helper.

Fake dispatch must distinguish returned zero IDs, valid counts, unmodified output text, native void completion and transport exceptions. COM calls belong on the owning apartment. Same-instance refresh/disposal must invalidate use assumptions without final-releasing shared bindings.

Controlled native fixtures should create a report and multiple tables, read/write boundary cells, resize/rename/delete, apply all formatting, inspect the report visually and inspect persistence behavior for each installed year. No destructive save/delete operation should be used as a capability probe against a working model.

XML must document identifiers versus indexes, one-based coordinates, font/color units/ranges, alpha handling, formatting scope, native void semantics, report persistence and exceptions. All public models/enums/overloads need useful property/value documentation and a compiling end-to-end report example.

## 9. NuGet boundary

No production code or package was changed. Future Table ports should use shared services and late binding, ship the generated XML and avoid adding WPF solely for optional color conveniences. The current .NET Framework 4.8.1 target remains unchanged; modern .NET support requires separate build/runtime qualification.
