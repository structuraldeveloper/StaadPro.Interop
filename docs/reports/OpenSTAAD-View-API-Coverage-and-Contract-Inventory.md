# OpenSTAAD View API coverage and native contract inventory

**Reviewed:** 23 October 2026<br>**Status:** Planning only; no typed View domain functions are implemented in this repository.

Read the [architecture and implementation plan](OpenSTAAD-View-Multi-Version-Architecture-and-Plan.md) first. This annex records all 67 native names, all source interface overloads, declarations, logical parameter directions, return conventions, value tables, metadata and proposed managed result shapes. Native names and constants below are evidence; managed names and types are proposals requiring API review.

**ExportView discrepancy:** Every manual declares four arguments including overwrite; both inspected interop assemblies declare only three. The main adapter passes four dynamically. Keep this unresolved until actual dispatch shape and overwrite behavior are qualified; see the architecture report.

## 1. Counting and evidence boundaries

| Measure | 2024 | 2025 | 2026 |
| --- | --- | --- | --- |
| Group names | 67 | 67 | 67 |
| Detailed contracts | 67 | 67 | 67 |
| Native class names | 69 | 69 | 69 |
| Inspected automation methods | No installed DLL inspected | 67 | 67 |

Native class implementation/lifecycle helpers excluded from the public port: `OnFinalRelease`, `SetStructure`. Other group pages were scanned for OSViewUI ownership; no additional domain-owned rows were found. For Design, Output-owned functions under the Design steel page remain in the separate Output report.

Windows native long is 32-bit. VARIANT and LP(D)ISPATCH declarations do not establish a managed payload type or successful operation. Logical directions below come from parameter prose, not merely C++ reference syntax. Unmatched names and stale prose are shown explicitly; parameter positions must be reconciled against metadata and a controlled native fixture before dispatch is approved.

## 2. Complete cross-version function matrix

| Native View function | Native return | 2024 / 2025 / 2026 | Main native target | Current typed / plan |
| --- | --- | --- | --- | --- |
| `ShowFront` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga6a90c64c9b49bbf381b3f0daf02def65) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga6a90c64c9b49bbf381b3f0daf02def65) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga6a90c64c9b49bbf381b3f0daf02def65) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L109) | Pending / V2 |
| `ShowBack` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga96756d279085ba219fd0a0a2a1e438a4) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga96756d279085ba219fd0a0a2a1e438a4) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga96756d279085ba219fd0a0a2a1e438a4) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L114) | Pending / V2 |
| `ShowRight` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga5634cef1697b606aebf92c1af151f8c7) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga5634cef1697b606aebf92c1af151f8c7) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga5634cef1697b606aebf92c1af151f8c7) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L124) | Pending / V2 |
| `ShowLeft` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga4bb71b32652fae9e5ce13f1630a54a18) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga4bb71b32652fae9e5ce13f1630a54a18) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga4bb71b32652fae9e5ce13f1630a54a18) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L119) | Pending / V2 |
| `ShowPlan` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gab760ebfa8341bd98861d8baffa08a8a0) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gab760ebfa8341bd98861d8baffa08a8a0) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gab760ebfa8341bd98861d8baffa08a8a0) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L129) | Pending / V2 |
| `ShowBottom` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga06545b0c45e4cc59554f19de294708da) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga06545b0c45e4cc59554f19de294708da) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga06545b0c45e4cc59554f19de294708da) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L134) | Pending / V2 |
| `ShowIsometric` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga94bb198780b6dce70eeb1168072494d1) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga94bb198780b6dce70eeb1168072494d1) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga94bb198780b6dce70eeb1168072494d1) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L139) | Pending / V2 |
| `RotateUp` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gabb888a130a1a16176f6cdd294c43b5da) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gabb888a130a1a16176f6cdd294c43b5da) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gabb888a130a1a16176f6cdd294c43b5da) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L144) | Pending / V2 |
| `RotateDown` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga7a3c254a8265aa0c2625ecd00dd24c9b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga7a3c254a8265aa0c2625ecd00dd24c9b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga7a3c254a8265aa0c2625ecd00dd24c9b) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L149) | Pending / V2 |
| `RotateLeft` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gafb0cd59b51090892199f6958394a984d) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gafb0cd59b51090892199f6958394a984d) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gafb0cd59b51090892199f6958394a984d) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L154) | Pending / V2 |
| `RotateRight` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga8fab49a08227df3f138477d6dd15c277) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga8fab49a08227df3f138477d6dd15c277) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga8fab49a08227df3f138477d6dd15c277) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L159) | Pending / V2 |
| `SpinLeft` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga3bb9e6542ad50be943b5aab1c61ab1ad) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga3bb9e6542ad50be943b5aab1c61ab1ad) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga3bb9e6542ad50be943b5aab1c61ab1ad) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L164) | Pending / V2 |
| `SpinRight` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gaff32735f21ab2236076d3cfe350cef4e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gaff32735f21ab2236076d3cfe350cef4e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gaff32735f21ab2236076d3cfe350cef4e) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L169) | Pending / V2 |
| `ZoomAll` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gac216528ad6c739a0dce373b7d4249e82) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gac216528ad6c739a0dce373b7d4249e82) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gac216528ad6c739a0dce373b7d4249e82) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L174) | Pending / V2 |
| `CreateNewViewForSelections` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gaa5a9b4d1857d6d3affeafd606fe19a4a) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gaa5a9b4d1857d6d3affeafd606fe19a4a) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gaa5a9b4d1857d6d3affeafd606fe19a4a) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L72) | Pending / V2 |
| `SetLabel` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gad3445f13dd9c574470c69f4df8c5f25d) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gad3445f13dd9c574470c69f4df8c5f25d) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gad3445f13dd9c574470c69f4df8c5f25d) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L329) | Pending / V2 |
| `SetSectionView` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga23c282486563b12694b19d39f32696b4) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga23c282486563b12694b19d39f32696b4) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga23c282486563b12694b19d39f32696b4) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L334) | Pending / V4 |
| `SetDiagramMode` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga6b386179813255844821de5331208ea9) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga6b386179813255844821de5331208ea9) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga6b386179813255844821de5331208ea9) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L319) | Pending / V2 |
| `RefreshView` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga414258bde9211e0842bfaa8636adc7bf) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga414258bde9211e0842bfaa8636adc7bf) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga414258bde9211e0842bfaa8636adc7bf) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L104) | Pending / V4 |
| `SetNodeAnnotationMode` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga611adf3ad2fd29f851a0b88b23cabb6b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga611adf3ad2fd29f851a0b88b23cabb6b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga611adf3ad2fd29f851a0b88b23cabb6b) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L309) | Pending / V2 |
| `SetReactionAnnotationMode` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga9e1affa456e3d75e4554f8f830f8db9f) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga9e1affa456e3d75e4554f8f830f8db9f) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga9e1affa456e3d75e4554f8f830f8db9f) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L314) | Pending / V2 |
| `GetInterfaceMode` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gac867cf880740697fbf7bba4ed3748681) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gac867cf880740697fbf7bba4ed3748681) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gac867cf880740697fbf7bba4ed3748681) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L373) | Pending / V2 |
| `SetInterfaceMode` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga6efdc33221f34f1859bc841d1df3d579) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga6efdc33221f34f1859bc841d1df3d579) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga6efdc33221f34f1859bc841d1df3d579) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L378) | Pending / V2 |
| `SetModeSectionPage` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gaec917a8590cf38aec0f015657c40b148) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gaec917a8590cf38aec0f015657c40b148) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gaec917a8590cf38aec0f015657c40b148) | No target | Pending / V2 |
| `SetBeamAnnotationMode` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga8f7fe6c6ae235be13a69cc120260064e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga8f7fe6c6ae235be13a69cc120260064e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga8f7fe6c6ae235be13a69cc120260064e) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L304) | Pending / V2 |
| `ShowAllMembers` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga8867d1f819358edb9cbf5e8b17e4e3f5) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga8867d1f819358edb9cbf5e8b17e4e3f5) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga8867d1f819358edb9cbf5e8b17e4e3f5) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L184) | Pending / V2 |
| `HideAllMembers` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga21f15888dcd089e830a3112e69c07f47) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga21f15888dcd089e830a3112e69c07f47) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga21f15888dcd089e830a3112e69c07f47) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L189) | Pending / V2 |
| `SetUnits` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga677fa32f03321059c9db8812f5296e99) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga677fa32f03321059c9db8812f5296e99) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga677fa32f03321059c9db8812f5296e99) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L383) | Pending / V2 |
| `ShowMembers` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga503737c58ada53b67a06b8f137d45bd5) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga503737c58ada53b67a06b8f137d45bd5) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga503737c58ada53b67a06b8f137d45bd5) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L201) | Pending / V2 |
| `HideMembers` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga1e361992ec11851434a4d84aa434c1a6) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga1e361992ec11851434a4d84aa434c1a6) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga1e361992ec11851434a4d84aa434c1a6) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L212) | Pending / V2 |
| `ShowMember` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga99421be7751d97a3ec9e2351335c6b2b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga99421be7751d97a3ec9e2351335c6b2b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga99421be7751d97a3ec9e2351335c6b2b) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L194) | Pending / V2 |
| `HideMember` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga5b324df49811ba841c9d6087e81cb6db) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga5b324df49811ba841c9d6087e81cb6db) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga5b324df49811ba841c9d6087e81cb6db) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L206) | Pending / V2 |
| `HidePlate` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gab30e0222f8b943ce148133446fd51d48) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gab30e0222f8b943ce148133446fd51d48) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gab30e0222f8b943ce148133446fd51d48) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L217) | Pending / V2 |
| `HideSolid` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga48dff4c4424cc1d8f0cb7b4901b50087) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga48dff4c4424cc1d8f0cb7b4901b50087) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga48dff4c4424cc1d8f0cb7b4901b50087) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L229) | Pending / V2 |
| `HideSurface` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga833c334df0c097da07bfd4c84ce95dd5) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga833c334df0c097da07bfd4c84ce95dd5) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga833c334df0c097da07bfd4c84ce95dd5) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L239) | Pending / V2 |
| `HideEntity` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga958c96140856c420b3137c10286f6eaf) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga958c96140856c420b3137c10286f6eaf) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga958c96140856c420b3137c10286f6eaf) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L249) | Pending / V2 |
| `ZoomExtentsMainView` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gaeaaeff4b965bcbbb6db04074e40dc808) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gaeaaeff4b965bcbbb6db04074e40dc808) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gaeaaeff4b965bcbbb6db04074e40dc808) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L179) | Pending / V4 |
| `SelectMembersParallelTo` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga970490702a349a255731f6365ef6b1a6) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga970490702a349a255731f6365ef6b1a6) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga970490702a349a255731f6365ef6b1a6) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L279) | Pending / V2 |
| `SelectGroup` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga61635c9979e6f03f9d1006e0372bc547) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga61635c9979e6f03f9d1006e0372bc547) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga61635c9979e6f03f9d1006e0372bc547) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L264) | Pending / V2 |
| `SelectInverse` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga48ae848f32cdeaf9f0113e1875d8c7c4) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga48ae848f32cdeaf9f0113e1875d8c7c4) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga48ae848f32cdeaf9f0113e1875d8c7c4) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L269) | Pending / V2 |
| `SelectByItemList` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gaf5182664037faf061ef0a7fd16af1789) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gaf5182664037faf061ef0a7fd16af1789) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gaf5182664037faf061ef0a7fd16af1789) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L259) | Pending / V2 |
| `SelectByMissingAttribute` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga15319d1dc16b75f464056de60fd13db0) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga15319d1dc16b75f464056de60fd13db0) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga15319d1dc16b75f464056de60fd13db0) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L274) | Pending / V2 |
| `SelectEntitiesConnectedToNode` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gaca55b6cf2cdc1ca2cf3c679113989c31) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gaca55b6cf2cdc1ca2cf3c679113989c31) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gaca55b6cf2cdc1ca2cf3c679113989c31) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L289) | Pending / V2 |
| `SelectEntitiesConnectedToMember` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga8437531312f47c940bedda5fc9704623) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga8437531312f47c940bedda5fc9704623) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga8437531312f47c940bedda5fc9704623) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L284) | Pending / V2 |
| `SelectEntitiesConnectedToPlate` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gae001bc5625772b7a4bd626da5af874a3) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gae001bc5625772b7a4bd626da5af874a3) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gae001bc5625772b7a4bd626da5af874a3) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L294) | Pending / V2 |
| `SelectEntitiesConnectedToSolid` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga48f83d8c5587aed20fe6ca3a624b0868) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga48f83d8c5587aed20fe6ca3a624b0868) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga48f83d8c5587aed20fe6ca3a624b0868) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L299) | Pending / V2 |
| `GetNoOfBeamsInView` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga88f9646f7632fa5aabe61edde28ebcae) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga88f9646f7632fa5aabe61edde28ebcae) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga88f9646f7632fa5aabe61edde28ebcae) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L410) | Pending / V4 |
| `GetBeamsInView` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gac780123fd4fbb94dcd47924189aba79d) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gac780123fd4fbb94dcd47924189aba79d) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gac780123fd4fbb94dcd47924189aba79d) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L417) | Pending / V4 |
| `CreateNewViewForSelectionsEx` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gaaacd7c65c11f99baca613f39fa59855b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gaaacd7c65c11f99baca613f39fa59855b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gaaacd7c65c11f99baca613f39fa59855b) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L77) | Pending / V2 |
| `ExportView` | `long` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga55c816cb668826627941025c09eea221) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga55c816cb668826627941025c09eea221) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga55c816cb668826627941025c09eea221) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L400) | Pending / V4 |
| `CopyPicture` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga27cff2188feded7803f31424004b4cf4) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga27cff2188feded7803f31424004b4cf4) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga27cff2188feded7803f31424004b4cf4) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L391) | Pending / V4 |
| `SetWindowPosition` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga6ea386f9f5bcc55899a1dfb7fbebca72) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga6ea386f9f5bcc55899a1dfb7fbebca72) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga6ea386f9f5bcc55899a1dfb7fbebca72) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L57) | Pending / V4 |
| `GetApplicationDesktopSize` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga77d5ca16ddbf42967f848a9997f74681) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga77d5ca16ddbf42967f848a9997f74681) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga77d5ca16ddbf42967f848a9997f74681) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L65) | Pending / V4 |
| `GetScaleValues` | `long` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga5e67303a2568385954aed989bfbe5e5f) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga5e67303a2568385954aed989bfbe5e5f) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga5e67303a2568385954aed989bfbe5e5f) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L356) | Pending / V3 |
| `SetScaleValues` | `long` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gaa4262a1907f1cdf93432d376c8c83165) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gaa4262a1907f1cdf93432d376c8c83165) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gaa4262a1907f1cdf93432d376c8c83165) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L368) | Pending / V3 |
| `GetScaleValueByType` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga151f4248482bba01512bc6e53e447c7e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga151f4248482bba01512bc6e53e447c7e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga151f4248482bba01512bc6e53e447c7e) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L346) | Pending / V3 |
| `SetScaleValueByType` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga3c4dae5aaca0adc4a17ecb2402c8651e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga3c4dae5aaca0adc4a17ecb2402c8651e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga3c4dae5aaca0adc4a17ecb2402c8651e) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L363) | Pending / V3 |
| `GetScaleCount` | `long` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga332159324e4ada480a7fad4739fbdb5f) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga332159324e4ada480a7fad4739fbdb5f) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga332159324e4ada480a7fad4739fbdb5f) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L339) | Pending / V3 |
| `DetachView` | `long` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga2184529998c62f34ef9f15c072f634ee) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga2184529998c62f34ef9f15c072f634ee) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga2184529998c62f34ef9f15c072f634ee) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L99) | Pending / V4 |
| `RenameView` | `long` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gadd58ac54d096b7dedb1fe8f2dc7cfc5d) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gadd58ac54d096b7dedb1fe8f2dc7cfc5d) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gadd58ac54d096b7dedb1fe8f2dc7cfc5d) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L94) | Pending / V4 |
| `OpenView` | `long` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga830b1763a33ed1e519d6acd110178d97) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga830b1763a33ed1e519d6acd110178d97) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga830b1763a33ed1e519d6acd110178d97) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L82) | Pending / V4 |
| `SaveView` | `long` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga9d4c42c49801d5a7e286255f04d91fa8) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga9d4c42c49801d5a7e286255f04d91fa8) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga9d4c42c49801d5a7e286255f04d91fa8) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L89) | Pending / V4 |
| `GetWindowTitle` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga7098acb0d27c1578bd126a1a6078b8cd) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga7098acb0d27c1578bd126a1a6078b8cd) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga7098acb0d27c1578bd126a1a6078b8cd) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L47) | Pending / V4 |
| `GetWindowCount` | `long` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga9834880afec1f0955064a982db108a37) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga9834880afec1f0955064a982db108a37) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga9834880afec1f0955064a982db108a37) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L42) | Pending / V4 |
| `CloseActiveWindow` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga42ee1d56567dd99c7847c59ee40951d6) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga42ee1d56567dd99c7847c59ee40951d6) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga42ee1d56567dd99c7847c59ee40951d6) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L37) | Pending / V4 |
| `SetActiveWindow` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga6517f51dde92325fe29013895bb08c5d) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga6517f51dde92325fe29013895bb08c5d) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga6517f51dde92325fe29013895bb08c5d) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L52) | Pending / V4 |
| `SetDesignResults` | `long` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga4ee212aae23382b7d626e33ec1418c6a) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga4ee212aae23382b7d626e33ec1418c6a) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga4ee212aae23382b7d626e33ec1418c6a) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L324) | Pending / V2 |

## 3. Per-function contracts and managed proposals

<a id="view-showfront"></a>

### 1. ShowFront

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga6a90c64c9b49bbf381b3f0daf02def65) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga6a90c64c9b49bbf381b3f0daf02def65) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga6a90c64c9b49bbf381b3f0daf02def65)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult ShowFront; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::ShowFront()
~~~

No native parameters.

**Contract, including return/status semantics:** This function displays the front view of the structure. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void ShowFront()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [ShowFront](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L109)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-showback"></a>

### 2. ShowBack

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga96756d279085ba219fd0a0a2a1e438a4) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga96756d279085ba219fd0a0a2a1e438a4) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga96756d279085ba219fd0a0a2a1e438a4)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult ShowBack; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::ShowBack()
~~~

No native parameters.

**Contract, including return/status semantics:** This function displays the back view of the structure. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void ShowBack()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [ShowBack](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L114)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-showright"></a>

### 3. ShowRight

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga5634cef1697b606aebf92c1af151f8c7) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga5634cef1697b606aebf92c1af151f8c7) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga5634cef1697b606aebf92c1af151f8c7)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult ShowRight; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::ShowRight()
~~~

No native parameters.

**Contract, including return/status semantics:** This function displays the right view of the structure. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void ShowRight()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [ShowRight](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L124)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-showleft"></a>

### 4. ShowLeft

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga4bb71b32652fae9e5ce13f1630a54a18) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga4bb71b32652fae9e5ce13f1630a54a18) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga4bb71b32652fae9e5ce13f1630a54a18)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult ShowLeft; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::ShowLeft()
~~~

No native parameters.

**Contract, including return/status semantics:** This function displays the left view of the structure. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void ShowLeft()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [ShowLeft](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L119)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-showplan"></a>

### 5. ShowPlan

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gab760ebfa8341bd98861d8baffa08a8a0) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gab760ebfa8341bd98861d8baffa08a8a0) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gab760ebfa8341bd98861d8baffa08a8a0)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult ShowPlan; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::ShowPlan()
~~~

No native parameters.

**Contract, including return/status semantics:** This function displays the top (i.e., plan) view of the structure. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void ShowPlan()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [ShowPlan](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L129)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-showbottom"></a>

### 6. ShowBottom

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga06545b0c45e4cc59554f19de294708da) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga06545b0c45e4cc59554f19de294708da) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga06545b0c45e4cc59554f19de294708da)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult ShowBottom; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::ShowBottom()
~~~

No native parameters.

**Contract, including return/status semantics:** This function displays the bottom view of the structure. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void ShowBottom()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [ShowBottom](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L134)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-showisometric"></a>

### 7. ShowIsometric

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga94bb198780b6dce70eeb1168072494d1) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga94bb198780b6dce70eeb1168072494d1) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga94bb198780b6dce70eeb1168072494d1)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult ShowIsometric; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::ShowIsometric()
~~~

No native parameters.

**Contract, including return/status semantics:** This function displays the isometric view of the structure. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void ShowIsometric()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [ShowIsometric](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L139)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-rotateup"></a>

### 8. RotateUp

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gabb888a130a1a16176f6cdd294c43b5da) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gabb888a130a1a16176f6cdd294c43b5da) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gabb888a130a1a16176f6cdd294c43b5da)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult RotateUp; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::RotateUp(const VARIANT FAR&dDegrees)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `dDegrees` | `const VARIANT FAR&` | [in] | Variable providing the degree of rotation. |

**Contract, including return/status semantics:** Rotates the structure through Degrees about the Global X-Axis. Parameters [in] dDegrees Variable providing the degree of rotation. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void RotateUp(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [RotateUp](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L144)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-rotatedown"></a>

### 9. RotateDown

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga7a3c254a8265aa0c2625ecd00dd24c9b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga7a3c254a8265aa0c2625ecd00dd24c9b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga7a3c254a8265aa0c2625ecd00dd24c9b)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult RotateDown; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::RotateDown(const VARIANT FAR&dDegrees)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `dDegrees` | `const VARIANT FAR&` | [in] | Variable providing the degree of rotation. |

**Contract, including return/status semantics:** Rotates the structure through Degrees about the Global X-Axis. Parameters [in] dDegrees Variable providing the degree of rotation. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void RotateDown(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [RotateDown](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L149)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-rotateleft"></a>

### 10. RotateLeft

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gafb0cd59b51090892199f6958394a984d) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gafb0cd59b51090892199f6958394a984d) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gafb0cd59b51090892199f6958394a984d)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult RotateLeft; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::RotateLeft(const VARIANT FAR&dDegrees)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `dDegrees` | `const VARIANT FAR&` | [in] | Variable providing the degree of rotation. |

**Contract, including return/status semantics:** Rotates the structure through Degrees about the Global Y-Axis. Parameters [in] dDegrees Variable providing the degree of rotation. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void RotateLeft(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [RotateLeft](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L154)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-rotateright"></a>

### 11. RotateRight

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga8fab49a08227df3f138477d6dd15c277) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga8fab49a08227df3f138477d6dd15c277) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga8fab49a08227df3f138477d6dd15c277)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult RotateRight; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::RotateRight(const VARIANT FAR&dDegrees)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `dDegrees` | `const VARIANT FAR&` | [in] | Variable providing the degree of rotation. |

**Contract, including return/status semantics:** Rotates the structure through Degrees about the Global Y-Axis. Parameters [in] dDegrees Variable providing the degree of rotation. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void RotateRight(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [RotateRight](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L159)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-spinleft"></a>

### 12. SpinLeft

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga3bb9e6542ad50be943b5aab1c61ab1ad) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga3bb9e6542ad50be943b5aab1c61ab1ad) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga3bb9e6542ad50be943b5aab1c61ab1ad)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult SpinLeft; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::SpinLeft(const VARIANT FAR&dDegrees)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `dDegrees` | `const VARIANT FAR&` | [in] | Variable providing the degree of rotation. |

**Contract, including return/status semantics:** Rotates the structure through Degrees about the Global Z-Axis. Parameters [in] dDegrees Variable providing the degree of rotation. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SpinLeft(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SpinLeft](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L164)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-spinright"></a>

### 13. SpinRight

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gaff32735f21ab2236076d3cfe350cef4e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gaff32735f21ab2236076d3cfe350cef4e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gaff32735f21ab2236076d3cfe350cef4e)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult SpinRight; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::SpinRight(const VARIANT FAR&dDegrees)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `dDegrees` | `const VARIANT FAR&` | [in] | Variable providing the degree of rotation. |

**Contract, including return/status semantics:** Rotates the structure through Degrees about the Global Z-Axis. Parameters [in] dDegrees Variable providing the degree of rotation. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SpinRight(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SpinRight](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L169)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-zoomall"></a>

### 14. ZoomAll

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gac216528ad6c739a0dce373b7d4249e82) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gac216528ad6c739a0dce373b7d4249e82) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gac216528ad6c739a0dce373b7d4249e82)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult ZoomAll; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::ZoomAll()
~~~

No native parameters.

**Contract, including return/status semantics:** zoom current view to Display the whole structure. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void ZoomAll()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [ZoomAll](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L174)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-createnewviewforselections"></a>

### 15. CreateNewViewForSelections

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gaa5a9b4d1857d6d3affeafd606fe19a4a) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gaa5a9b4d1857d6d3affeafd606fe19a4a) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gaa5a9b4d1857d6d3affeafd606fe19a4a)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult CreateNewViewForSelections; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::CreateNewViewForSelections()
~~~

No native parameters.

**Contract, including return/status semantics:** Creates a new view in new window for the selected objects displayed in the active window. C++ Syntax VBA Syntax See also OSViewUI::CreateNewViewForSelectionsEx

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void CreateNewViewForSelections()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [CreateNewViewForSelections](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L72)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-setlabel"></a>

### 16. SetLabel

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gad3445f13dd9c574470c69f4df8c5f25d) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gad3445f13dd9c574470c69f4df8c5f25d) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gad3445f13dd9c574470c69f4df8c5f25d)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult SetLabel; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::SetLabel(const VARIANT FAR&which,const VARIANT FAR&showFlag)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `which` | `const VARIANT FAR&` | [in] | Variable identifying the label type. It may be one of the following values: [value table below] |
| `showFlag` | `const VARIANT FAR&` | [in] | Variable to set label mode on (True) or off (False). |

**Contract, including return/status semantics:** Sets the label on the structure diagram on or off. Parameters [in] which Variable identifying the label type. It may be one of the following values: ID Label Type 0 Node number label 1 Member number label 2 Member property reference label 3 Material property reference label 4 Support label 5 Member release label 6 Member orientation label 7 Member section label 8 Load value label 9 Axes label 10 Node position label 11 Member specification label 12 Member ends 13 Plate element number label 14 Plate element orientation label 15 Solid element number label 16 Dimension label 17 Floor load label 18 Floor load distribution diagram label 19 Wind load label 20 Wind load influence area diagram label 21 Diagram Info [in] showFlag Variable to set label mode on (True) or off (False). C++ Syntax VBA Syntax

**Value table 1** (source column groupings retained):

| ID | Label Type |
| --- | --- |
| 0 | Node number label |
| 1 | Member number label |
| 2 | Member property reference label |
| 3 | Material property reference label |
| 4 | Support label |
| 5 | Member release label |
| 6 | Member orientation label |
| 7 | Member section label |
| 8 | Load value label |
| 9 | Axes label |
| 10 | Node position label |
| 11 | Member specification label |
| 12 | Member ends |
| 13 | Plate element number label |
| 14 | Plate element orientation label |
| 15 | Solid element number label |
| 16 | Dimension label |
| 17 | Floor load label |
| 18 | Floor load distribution diagram label |
| 19 | Wind load label |
| 20 | Wind load influence area diagram label |
| 21 | Diagram Info |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SetLabel(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetLabel](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L329)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-setsectionview"></a>

### 17. SetSectionView

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga23c282486563b12694b19d39f32696b4) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga23c282486563b12694b19d39f32696b4) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga23c282486563b12694b19d39f32696b4)<br>**Current:** Pending; V4.

**Managed proposal:** `ViewOperationResult SetSectionView; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::SetSectionView(const VARIANT FAR&plane,const VARIANT FAR&minVal,const VARIANT FAR&maxVal)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `plane` | `const VARIANT FAR&` | [in] | Variable identifying the section plane. It may be one of the following values: [value table below] |
| `minVal` | `const VARIANT FAR&` | [in] | Minimum range of the cutting plane. |
| `maxVal` | `const VARIANT FAR&` | [in] | Maximum range of the cutting plane. |

**Contract, including return/status semantics:** Creates a section view of the structure. Parameters [in] plane Variable identifying the section plane. It may be one of the following values: ID Values for plane 0 XY Plane 1 YZ Plane 2 XZ Plane [in] minVal Minimum range of the cutting plane. [in] maxVal Maximum range of the cutting plane. C++ Syntax VBA Syntax

**Value table 1** (source column groupings retained):

| ID | Values for plane |
| --- | --- |
| 0 | XY Plane |
| 1 | YZ Plane |
| 2 | XZ Plane |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SetSectionView(System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetSectionView](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L334)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-setdiagrammode"></a>

### 18. SetDiagramMode

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga6b386179813255844821de5331208ea9) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga6b386179813255844821de5331208ea9) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga6b386179813255844821de5331208ea9)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult SetDiagramMode; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::SetDiagramMode(const VARIANT FAR&which,const VARIANT FAR&showFlag,const VARIANT FAR&refreshFlag)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `which` | `const VARIANT FAR&` | [in] | Variable identifying the diagram type. It may be one of the following values: [value table below] |
| `showFlag` | `const VARIANT FAR&` | [in] | Variable to set label mode on (True) or off (False). |
| `refreshFlag` | `const VARIANT FAR&` | [in] | Variable (True or False). If True, STAAD.Pro viewing windows refresh. |

**Contract, including return/status semantics:** Sets the label on the structure diagram on or off. Parameters [in] which Variable identifying the diagram type. It may be one of the following values: ID Diagram Type 0 Load 1 Displacement 2 MY 3 MZ 4 FY 5 FZ 6 AX 7 TR 8 Structure 9 Full Section 10 Section Outline 11 Stress 12 Shrink 13 Perspective 14 Hide Structure 15 Fill Plates & Solids 16 Hide Plates & Solids 18 Hide Piping 19 Sort Geometry 20 Sort Nodes 21 Plate Stress 22 Solid Stress 23 Mode Shape 24 Stress Animation 25 Plate reinforcement 26 Deck Influence Diagram* 27 Deck Carriageways* 28 Deck Triangulation* 29 Deck Loads* 30 Deck Vehicles* *Requires the STAAD.beava component [in] showFlag Variable to set label mode on (True) or off (False). [in] refreshFlag Variable (True or False). If True, STAAD.Pro viewing windows refresh. C++ Syntax VBA Syntax

**Value table 1** (source column groupings retained):

| ID | Diagram Type |
| --- | --- |
| 0 | Load |
| 1 | Displacement |
| 2 | MY |
| 3 | MZ |
| 4 | FY |
| 5 | FZ |
| 6 | AX |
| 7 | TR |
| 8 | Structure |
| 9 | Full Section |
| 10 | Section Outline |
| 11 | Stress |
| 12 | Shrink |
| 13 | Perspective |
| 14 | Hide Structure |
| 15 | Fill Plates & Solids |
| 16 | Hide Plates & Solids |
| 18 | Hide Piping |
| 19 | Sort Geometry |
| 20 | Sort Nodes |
| 21 | Plate Stress |
| 22 | Solid Stress |
| 23 | Mode Shape |
| 24 | Stress Animation |
| 25 | Plate reinforcement |
| 26 | Deck Influence Diagram* |
| 27 | Deck Carriageways* |
| 28 | Deck Triangulation* |
| 29 | Deck Loads* |
| 30 | Deck Vehicles* |
|  | *Requires the STAAD.beava component |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SetDiagramMode(System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetDiagramMode](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L319)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-refreshview"></a>

### 19. RefreshView

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga414258bde9211e0842bfaa8636adc7bf) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga414258bde9211e0842bfaa8636adc7bf) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga414258bde9211e0842bfaa8636adc7bf)<br>**Current:** Pending; V4.

**Managed proposal:** `ViewOperationResult RefreshView; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::RefreshView()
~~~

No native parameters.

**Contract, including return/status semantics:** Refreshes the viewing window. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void RefreshView()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [RefreshView](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L104)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-setnodeannotationmode"></a>

### 20. SetNodeAnnotationMode

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga611adf3ad2fd29f851a0b88b23cabb6b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga611adf3ad2fd29f851a0b88b23cabb6b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga611adf3ad2fd29f851a0b88b23cabb6b)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult SetNodeAnnotationMode; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::SetNodeAnnotationMode(const VARIANT FAR&dFlag,const VARIANT FAR&refreshFlag)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `dFlag` | `const VARIANT FAR&` | [in] | Variable controlling the annotation type. It may be one of the following values: [value table below] |
| `refreshFlag` | `const VARIANT FAR&` | [in] | Variable (True or False). If True, STAAD.Pro viewing windows refresh with the current annotation mode. |

**Contract, including return/status semantics:** Sets the node displacement annotation mode. This function works only in the post-processing mode of STAAD.Pro. Parameters [in] dFlag Variable controlling the annotation type. It may be one of the following values: ID Annotation Type 1 X Displacement 2 Y Displacement 3 Z Displacement 4 Resultant Displacement [in] refreshFlag Variable (True or False). If True, STAAD.Pro viewing windows refresh with the current annotation mode. C++ Syntax VBA Syntax

**Value table 1** (source column groupings retained):

| ID | Annotation Type |
| --- | --- |
| 1 | X Displacement |
| 2 | Y Displacement |
| 3 | Z Displacement |
| 4 | Resultant Displacement |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SetNodeAnnotationMode(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetNodeAnnotationMode](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L309)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-setreactionannotationmode"></a>

### 21. SetReactionAnnotationMode

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga9e1affa456e3d75e4554f8f830f8db9f) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga9e1affa456e3d75e4554f8f830f8db9f) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga9e1affa456e3d75e4554f8f830f8db9f)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult SetReactionAnnotationMode; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::SetReactionAnnotationMode(const VARIANT FAR&dFlag,const VARIANT FAR&refreshFlag)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `dFlag` | `const VARIANT FAR&` | [in] | Variable controlling the annotation type. It may be one of the following values: [value table below] |
| `refreshFlag` | `const VARIANT FAR&` | [in] | Variable (True or False). If True, STAAD.Pro viewing windows refresh with the current annotation mode. |

**Contract, including return/status semantics:** Sets the node displacement annotation mode. This function works only in the post-processing mode of STAAD.Pro. Parameters [in] dFlag Variable controlling the annotation type. It may be one of the following values: ID Annotation Type 1 X Reaction 2 Y Reaction 3 Z Reaction 4 X Rotation 5 Y Rotation 6 Z Rotation 7 Reaction Value Only [in] refreshFlag Variable (True or False). If True, STAAD.Pro viewing windows refresh with the current annotation mode. C++ Syntax VBA Syntax

**Value table 1** (source column groupings retained):

| ID | Annotation Type |
| --- | --- |
| 1 | X Reaction |
| 2 | Y Reaction |
| 3 | Z Reaction |
| 4 | X Rotation |
| 5 | Y Rotation |
| 6 | Z Rotation |
| 7 | Reaction Value Only |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SetReactionAnnotationMode(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetReactionAnnotationMode](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L314)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-getinterfacemode"></a>

### 22. GetInterfaceMode

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gac867cf880740697fbf7bba4ed3748681) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gac867cf880740697fbf7bba4ed3748681) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gac867cf880740697fbf7bba4ed3748681)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewQueryResult<T> GetInterfaceMode; T is the documented int count, string title or managed interface-mode enum, never a COM object.`

#### 2024 contract

~~~cpp
VARIANT OSViewUI::GetInterfaceMode()
~~~

No native parameters.

**Contract, including return/status semantics:** This function returns the current visual mode in the STAAD.Pro environment. Return values 0 Pre-processor or modeling mode. 1 Post-processing mode. 2 Interactive design mode for STAAD.etc interoperability. 4 Piping mode. 5 BEAVA (i.e., Bridge Deck ) mode. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object GetInterfaceMode()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [GetInterfaceMode](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L373)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-setinterfacemode"></a>

### 23. SetInterfaceMode

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga6efdc33221f34f1859bc841d1df3d579) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga6efdc33221f34f1859bc841d1df3d579) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga6efdc33221f34f1859bc841d1df3d579)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult SetInterfaceMode; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
VARIANT OSViewUI::SetInterfaceMode(const VARIANT FAR&interfaceMode)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `interfaceMode` | `const VARIANT FAR&` | [in] | Variable to set the current visual mode in STAAD.Pro environment. Followings are the valid values for mode: [value table below] |

**Contract, including return/status semantics:** This function sets the current visual mode in the STAAD.Pro environment. Parameters [in] interfaceMode Variable to set the current visual mode in STAAD.Pro environment. Followings are the valid values for mode: ID Mode Type 0 Pre-processor or modeling mode 1 Physical modeling mode 2 Building planner mode 3 Piping mode 5 Post Processing mode 6 FoundationDesign mode 7 ConnectionDesign mode 9 AdvancedConcreteDesign mode 10 AdvancedSlabDesign mode 11 Earthquake mode 12 SteelAutoDrafter mode 13 ChineseSteelDesign mode Return values 1 / true Successful. 0 / false Specified mode is not visible in the workflow panel C++ Syntax VBA Syntax

**Value table 1** (source column groupings retained):

| ID | Mode Type |
| --- | --- |
| 0 | Pre-processor or modeling mode |
| 1 | Physical modeling mode |
| 2 | Building planner mode |
| 3 | Piping mode |
| 5 | Post Processing mode |
| 6 | FoundationDesign mode |
| 7 | ConnectionDesign mode |
| 9 | AdvancedConcreteDesign mode |
| 10 | AdvancedSlabDesign mode |
| 11 | Earthquake mode |
| 12 | SteelAutoDrafter mode |
| 13 | ChineseSteelDesign mode |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object SetInterfaceMode(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetWorkflowMode](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L378)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-setmodesectionpage"></a>

### 24. SetModeSectionPage

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gaec917a8590cf38aec0f015657c40b148) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gaec917a8590cf38aec0f015657c40b148) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gaec917a8590cf38aec0f015657c40b148)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult SetModeSectionPage; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::SetModeSectionPage(const VARIANT FAR&interfaceMode,const VARIANT FAR&sectionNumber,const VARIANT FAR&pageNumber)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `interfaceMode` | `const VARIANT FAR&` | [in] | Variable to set the current visual mode in STAAD.Pro environment. Followings are the valid values for mode: [value table below] |
| `sectionNumber` | `const VARIANT FAR&` | [in] | Variable to set the current main page (the tabs on the left-hand side of the screen) in the STAAD.Pro environment. The following are valid values for section: [value table below] |
| `pageNumber` | `const VARIANT FAR&` | [in] | Variable to set the current sub page (within a particular main page - the tabs on the left-hand side of the screen) in the STAAD.Pro environment. The following are valid values for modeSubPage: [value table below] |

**Contract, including return/status semantics:** This function sets the current page mode in the STAAD.Pro environment. Parameters [in] interfaceMode Variable to set the current visual mode in STAAD.Pro environment. Followings are the valid values for mode: ID Interface Mode 0 Pre-processor or modeling mode 1 Post-processing mode 2 Interactive design mode for STAAD.etc interoperability 4 Piping mode 5 BEAVA (i.e., Bridge Deck) mode [in] sectionNumber Variable to set the current main page (the tabs on the left-hand side of the screen) in the STAAD.Pro environment. The following are valid values for section: ID Main Page 1 Setup page 2 Geometry page 3 General page 5 Node Results page 6 Beam Result page 7 Plate Results page 8 Solid Results page [in] pageNumber Variable to set the current sub page (within a particular main page - the tabs on the left-hand side of the screen) in the STAAD.Pro environment. The following are valid values for modeSubPage: ID Page Number 0 Job Info page 1 Beam page 4 Plate page 5 Solid page 6 Property page 7 Constant page 8 Material page 9 Support page 10 Member Specifications page 11 Load page 17 Reaction page 18 Displacement page 19 Failure page 20 Forces page 21 Beam Stress page 22 Plate Stress page 23 Solid Stress page C++ Syntax VBA Syntax

**Value table 1** (source column groupings retained):

| ID | Interface Mode |
| --- | --- |
| 0 | Pre-processor or modeling mode |
| 1 | Post-processing mode |
| 2 | Interactive design mode for STAAD.etc interoperability |
| 4 | Piping mode |
| 5 | BEAVA (i.e., Bridge Deck) mode |

**Value table 2** (source column groupings retained):

| ID | Main Page |
| --- | --- |
| 1 | Setup page |
| 2 | Geometry page |
| 3 | General page |
| 5 | Node Results page |
| 6 | Beam Result page |
| 7 | Plate Results page |
| 8 | Solid Results page |

**Value table 3** (source column groupings retained):

| ID | Page Number |
| --- | --- |
| 0 | Job Info page |
| 1 | Beam page |
| 4 | Plate page |
| 5 | Solid page |
| 6 | Property page |
| 7 | Constant page |
| 8 | Material page |
| 9 | Support page |
| 10 | Member Specifications page |
| 11 | Load page |
| 17 | Reaction page |
| 18 | Displacement page |
| 19 | Failure page |
| 20 | Forces page |
| 21 | Beam Stress page |
| 22 | Plate Stress page |
| 23 | Solid Stress page |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SetModeSectionPage(System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-setbeamannotationmode"></a>

### 25. SetBeamAnnotationMode

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga8f7fe6c6ae235be13a69cc120260064e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga8f7fe6c6ae235be13a69cc120260064e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga8f7fe6c6ae235be13a69cc120260064e)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult SetBeamAnnotationMode; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::SetBeamAnnotationMode(const VARIANT FAR&Type,const VARIANT FAR&DWFlags,const VARIANT FAR&refreshFlag)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `Type` | `const VARIANT FAR&` | [in] | Variable controlling the annotation type. It may be one of the following values: [value table below] |
| `DWFlags` | `const VARIANT FAR&` | [in] | Variable controlling what values are to be shown for the annotationType. It may be one of the following values: [value table below] |
| `refreshFlag` | `const VARIANT FAR&` | [in] | Boolean variable (True or False). If True, STAAD.Pro viewing windows refresh with the current annotation mode. |

**Contract, including return/status semantics:** This function sets the current page mode in the STAAD.Pro environment. Parameters [in] Type Variable controlling the annotation type. It may be one of the following values: ID Annotation Type 0 Axial Diagram 1 Torsion Diagram 2 Moment Diagram 3 Shear Diagram 4 Stress Diagram 5 Displacement Diagram [in] DWFlags Variable controlling what values are to be shown for the annotationType. It may be one of the following values: ID Values 1 End Values 2 Max Absolute Values 3 Mid-span Values [in] refreshFlag Boolean variable (True or False). If True, STAAD.Pro viewing windows refresh with the current annotation mode. C++ Syntax VBA Syntax

**Value table 1** (source column groupings retained):

| ID | Annotation Type |
| --- | --- |
| 0 | Axial Diagram |
| 1 | Torsion Diagram |
| 2 | Moment Diagram |
| 3 | Shear Diagram |
| 4 | Stress Diagram |
| 5 | Displacement Diagram |

**Value table 2** (source column groupings retained):

| ID | Values |
| --- | --- |
| 1 | End Values |
| 2 | Max Absolute Values |
| 3 | Mid-span Values |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SetBeamAnnotationMode(System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetBeamAnnotationMode](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L304)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-showallmembers"></a>

### 26. ShowAllMembers

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga8867d1f819358edb9cbf5e8b17e4e3f5) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga8867d1f819358edb9cbf5e8b17e4e3f5) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga8867d1f819358edb9cbf5e8b17e4e3f5)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult ShowAllMembers; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::ShowAllMembers()
~~~

No native parameters.

**Contract, including return/status semantics:** Display all members of the current structure. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void ShowAllMembers()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [ShowAllMembers](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L184)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-hideallmembers"></a>

### 27. HideAllMembers

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga21f15888dcd089e830a3112e69c07f47) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga21f15888dcd089e830a3112e69c07f47) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga21f15888dcd089e830a3112e69c07f47)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult HideAllMembers; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::HideAllMembers()
~~~

No native parameters.

**Contract, including return/status semantics:** Hides all the members in the current structure. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void HideAllMembers()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [HideAllMembers](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L189)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-setunits"></a>

### 28. SetUnits

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga677fa32f03321059c9db8812f5296e99) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga677fa32f03321059c9db8812f5296e99) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga677fa32f03321059c9db8812f5296e99)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult SetUnits; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::SetUnits(const VARIANT FAR&uType,const VARIANT FAR&strUnit)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `uType` | `const VARIANT FAR&` | [in] | Variable that holds unit type. Values are as follows: [value table below] |
| `strUnit` | `const VARIANT FAR&` | [in] | Variable array that holds the unit for the specified type. Like "cm", "kns", "feet", "kn/cm" etc. |

**Contract, including return/status semantics:** Set viewing unit for the active view. Parameters [in] uType Variable that holds unit type. Values are as follows: ID Unit Type 0 Dimension 1 Displacement 2 SectionDimension 3 SectionArea 4 Inertia 5 Force 6 Moment 7 DistributedForce 8 DistributedMoment 9 Density 10 Acceleration 11 Spring 12 RotSpring 13 MaterialModulus 14 Stress 15 Alpha 16 Temperature 17 Mass 18 SectionModulus 19 RotationalDisplacement 20 SubgradeModulus -1 NoUnit [in] strUnit Variable array that holds the unit for the specified type. Like "cm", "kns", "feet", "kn/cm" etc. C++ Syntax VBA Syntax

**Value table 1** (source column groupings retained):

| ID | Unit Type |
| --- | --- |
| 0 | Dimension |
| 1 | Displacement |
| 2 | SectionDimension |
| 3 | SectionArea |
| 4 | Inertia |
| 5 | Force |
| 6 | Moment |
| 7 | DistributedForce |
| 8 | DistributedMoment |
| 9 | Density |
| 10 | Acceleration |
| 11 | Spring |
| 12 | RotSpring |
| 13 | MaterialModulus |
| 14 | Stress |
| 15 | Alpha |
| 16 | Temperature |
| 17 | Mass |
| 18 | SectionModulus |
| 19 | RotationalDisplacement |
| 20 | SubgradeModulus |
| -1 | NoUnit |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SetUnits(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetUnitsForActiveView](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L383)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-showmembers"></a>

### 29. ShowMembers

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga503737c58ada53b67a06b8f137d45bd5) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga503737c58ada53b67a06b8f137d45bd5) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga503737c58ada53b67a06b8f137d45bd5)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult ShowMembers; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::ShowMembers(const VARIANT FAR&nMembers,const VARIANT FAR&naMemberNos)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `nMembers` | `const VARIANT FAR&` | [in] | Variable that holds member number to be shown. |
| `naMemberNos` | `const VARIANT FAR&` | [in] | Variable array that holds the member nos, which need to be shown. |

**Contract, including return/status semantics:** Show the specified member. Parameters [in] nMembers Variable that holds member number to be shown. [in] naMemberNos Variable array that holds the member nos, which need to be shown. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void ShowMembers(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [ShowMembers](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L201)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-hidemembers"></a>

### 30. HideMembers

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga1e361992ec11851434a4d84aa434c1a6) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga1e361992ec11851434a4d84aa434c1a6) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga1e361992ec11851434a4d84aa434c1a6)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult HideMembers; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::HideMembers(const VARIANT FAR&nMembers,const VARIANT FAR&naMemberNos)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `nMembers` | `const VARIANT FAR&` | [in] | Variable that holds number of members to hide. |
| `naMemberNos` | `const VARIANT FAR&` | [in] | Variable array that holds the member nos, which need to be hidden. |

**Contract, including return/status semantics:** Hide the specified member. Parameters [in] nMembers Variable that holds number of members to hide. [in] naMemberNos Variable array that holds the member nos, which need to be hidden. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void HideMembers(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [HideMembers](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L212)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-showmember"></a>

### 31. ShowMember

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga99421be7751d97a3ec9e2351335c6b2b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga99421be7751d97a3ec9e2351335c6b2b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga99421be7751d97a3ec9e2351335c6b2b)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult ShowMember; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::ShowMember(const VARIANT FAR&nMember)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `nMember` | `const VARIANT FAR&` | [in] | Variable that holds member number to be shown. |

**Contract, including return/status semantics:** Show the specified member. Parameters [in] nMember Variable that holds member number to be shown. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void ShowMember(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [ShowMember](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L194)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-hidemember"></a>

### 32. HideMember

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga5b324df49811ba841c9d6087e81cb6db) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga5b324df49811ba841c9d6087e81cb6db) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga5b324df49811ba841c9d6087e81cb6db)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult HideMember; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::HideMember(const VARIANT FAR&nMember)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `nMember` | `const VARIANT FAR&` | [in] | Variable that holds member number to be hidden. |

**Contract, including return/status semantics:** Hide the specified member. Parameters [in] nMember Variable that holds member number to be hidden. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void HideMember(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [HideMember](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L206)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-hideplate"></a>

### 33. HidePlate

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gab30e0222f8b943ce148133446fd51d48) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gab30e0222f8b943ce148133446fd51d48) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gab30e0222f8b943ce148133446fd51d48)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult HidePlate; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::HidePlate(const VARIANT FAR&nPlate)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `nPlate` | `const VARIANT FAR&` | [in] | Variable that holds plate number to be hidden. |

**Contract, including return/status semantics:** Hide the specified plate. Parameters [in] nPlate Variable that holds plate number to be hidden. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void HidePlate(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [HidePlate](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L217), [HidePlates](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L224)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-hidesolid"></a>

### 34. HideSolid

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga48dff4c4424cc1d8f0cb7b4901b50087) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga48dff4c4424cc1d8f0cb7b4901b50087) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga48dff4c4424cc1d8f0cb7b4901b50087)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult HideSolid; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::HideSolid(const VARIANT FAR&nSolid)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `nSolid` | `const VARIANT FAR&` | [in] | Variable that holds solid number to be hidden. |

**Contract, including return/status semantics:** Hide the specified solid. Parameters [in] nSolid Variable that holds solid number to be hidden. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void HideSolid(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [HideSolid](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L229), [HideSolids](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L234)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-hidesurface"></a>

### 35. HideSurface

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga833c334df0c097da07bfd4c84ce95dd5) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga833c334df0c097da07bfd4c84ce95dd5) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga833c334df0c097da07bfd4c84ce95dd5)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult HideSurface; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::HideSurface(const VARIANT FAR&nSurface)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `nSurface` | `const VARIANT FAR&` | [in] | Variable that holds surface number to be hidden. |

**Contract, including return/status semantics:** Hide the specified surface. Parameters [in] nSurface Variable that holds surface number to be hidden. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void HideSurface(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [HideSurface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L239), [HideSurfaces](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L244)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-hideentity"></a>

### 36. HideEntity

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga958c96140856c420b3137c10286f6eaf) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga958c96140856c420b3137c10286f6eaf) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga958c96140856c420b3137c10286f6eaf)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult HideEntity; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::HideEntity(const VARIANT FAR&nEntity)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `nEntity` | `const VARIANT FAR&` | [in] | Variable that holds an entity (i.e., Member, Plates etc.) number to be hidden. |

**Contract, including return/status semantics:** Hides the specified entity, which may be a Beam, Plate, Solid, or Surface. Parameters [in] nEntity Variable that holds an entity (i.e., Member, Plates etc.) number to be hidden. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void HideEntity(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [HideEntity](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L249)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-zoomextentsmainview"></a>

### 37. ZoomExtentsMainView

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gaeaaeff4b965bcbbb6db04074e40dc808) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gaeaaeff4b965bcbbb6db04074e40dc808) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gaeaaeff4b965bcbbb6db04074e40dc808)<br>**Current:** Pending; V4.

**Managed proposal:** `ViewOperationResult ZoomExtentsMainView; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::ZoomExtentsMainView()
~~~

No native parameters.

**Contract, including return/status semantics:** Adjust viewing scale to zoom main view to the extents in new view. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void ZoomExtentsMainView()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [ZoomExtentsMainView](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L179)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-selectmembersparallelto"></a>

### 38. SelectMembersParallelTo

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga970490702a349a255731f6365ef6b1a6) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga970490702a349a255731f6365ef6b1a6) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga970490702a349a255731f6365ef6b1a6)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult SelectMembersParallelTo; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::SelectMembersParallelTo(const VARIANT FAR&bstrAxis)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `bstrAxis` | `const VARIANT FAR&` | [in] | Variable that holds the Axis ID. It may have three values: [value table below] |

**Contract, including return/status semantics:** Select members parallel to the specified axis. Parameters [in] bstrAxis Variable that holds the Axis ID. It may have three values: ID Axis X X-Axis Y Y-Axis Z Z-Axis C++ Syntax VBA Syntax

**Value table 1** (source column groupings retained):

| ID | Axis |
| --- | --- |
| X | X-Axis |
| Y | Y-Axis |
| Z | Z-Axis |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SelectMembersParallelTo(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SelectMembersParallelTo](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L279)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-selectgroup"></a>

### 39. SelectGroup

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga61635c9979e6f03f9d1006e0372bc547) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga61635c9979e6f03f9d1006e0372bc547) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga61635c9979e6f03f9d1006e0372bc547)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult SelectGroup; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
VARIANT OSViewUI::SelectGroup(const VARIANT FAR&bstrGroup)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `bstrGroup` | `const VARIANT FAR&` | [in] | A string variable that holds the group name. |

**Contract, including return/status semantics:** Select the relevant entities of the specified group. Parameters [in] bstrGroup A string variable that holds the group name. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object SelectGroup(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SelectGroup](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L264)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-selectinverse"></a>

### 40. SelectInverse

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga48ae848f32cdeaf9f0113e1875d8c7c4) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga48ae848f32cdeaf9f0113e1875d8c7c4) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga48ae848f32cdeaf9f0113e1875d8c7c4)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult SelectInverse; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::SelectInverse(const VARIANT FAR&entityType)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `entityType` | `const VARIANT FAR&` | [in] | Variable that holds entity type. Values may be as follows: [value table below] |

**Contract, including return/status semantics:** Inverse geometry selection for the specified entity. Parameters [in] entityType Variable that holds entity type. Values may be as follows: ID Entity Type 1 Node 2 Beam 3 Plate 4 Solid 5 Surface C++ Syntax VBA Syntax

**Value table 1** (source column groupings retained):

| ID | Entity Type |
| --- | --- |
| 1 | Node |
| 2 | Beam |
| 3 | Plate |
| 4 | Solid |
| 5 | Surface |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SelectInverse(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SelectInverse](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L269)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-selectbyitemlist"></a>

### 41. SelectByItemList

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gaf5182664037faf061ef0a7fd16af1789) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gaf5182664037faf061ef0a7fd16af1789) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gaf5182664037faf061ef0a7fd16af1789)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult SelectByItemList; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::SelectByItemList(const VARIANT FAR&entityType,const VARIANT FAR&nItems,const VARIANT FAR&itemList)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `entityType` | `const VARIANT FAR&` | [in] | Variable that holds entity type. Values may be as follows: [value table below] |
| `nItems` | `const VARIANT FAR&` | [in] | Variable that holds total number of entities needs to be selected. |
| `itemList` | `const VARIANT FAR&` | [in] | Array List holds the entity nos, which need to be selected. |

**Contract, including return/status semantics:** Select entities as specified. Parameters [in] entityType Variable that holds entity type. Values may be as follows: ID Entity Type 1 Node 2 Beam 3 Plate 4 Solid 5 Surface [in] nItems Variable that holds total number of entities needs to be selected. [in] itemList Array List holds the entity nos, which need to be selected. C++ Syntax VBA Syntax

**Value table 1** (source column groupings retained):

| ID | Entity Type |
| --- | --- |
| 1 | Node |
| 2 | Beam |
| 3 | Plate |
| 4 | Solid |
| 5 | Surface |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SelectByItemList(System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SelectEntities](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L259)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-selectbymissingattribute"></a>

### 42. SelectByMissingAttribute

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga15319d1dc16b75f464056de60fd13db0) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga15319d1dc16b75f464056de60fd13db0) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga15319d1dc16b75f464056de60fd13db0)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult SelectByMissingAttribute; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::SelectByMissingAttribute(const VARIANT FAR&attributeCode)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `propCode` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `attributeCode` | [in] | Variable that holds attribute type. Values may be as follows: [value table below] |

**Contract, including return/status semantics:** Select entity list for which specified entity is missing. Parameters [in] attributeCode Variable that holds attribute type. Values may be as follows: ID Entity Type 1 Missing Property 2 Missing Modulus of Elasticity 3 Missing Density of Material 4 Missing Alpha (Coefficient of Thermal Expansion) 5 Missing Poisson Ratio C++ Syntax VBA Syntax

**Value table 1** (source column groupings retained):

| ID | Entity Type |
| --- | --- |
| 1 | Missing Property |
| 2 | Missing Modulus of Elasticity |
| 3 | Missing Density of Material |
| 4 | Missing Alpha (Coefficient of Thermal Expansion) |
| 5 | Missing Poisson Ratio |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SelectByMissingAttribute(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SelectByMissingAttribute](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L274)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-selectentitiesconnectedtonode"></a>

### 43. SelectEntitiesConnectedToNode

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gaca55b6cf2cdc1ca2cf3c679113989c31) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gaca55b6cf2cdc1ca2cf3c679113989c31) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gaca55b6cf2cdc1ca2cf3c679113989c31)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult SelectEntitiesConnectedToNode; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::SelectEntitiesConnectedToNode(const VARIANT FAR&entityType,const VARIANT FAR&nodeNo)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `entityType` | `const VARIANT FAR&` | [in] | Variable that holds entity type. Values may be as follows: [value table below] |
| `nodeNo` | `const VARIANT FAR&` | [in] | Variable that holds node numbers with which connected entities needs to be selected. |

**Contract, including return/status semantics:** Select entities as specified in type and connected with the specified node. Parameters [in] entityType Variable that holds entity type. Values may be as follows: ID Entity Type 0 Geometry 1 Beam 2 Plate 3 Solid [in] nodeNo Variable that holds node numbers with which connected entities needs to be selected. C++ Syntax VBA Syntax

**Value table 1** (source column groupings retained):

| ID | Entity Type |
| --- | --- |
| 0 | Geometry |
| 1 | Beam |
| 2 | Plate |
| 3 | Solid |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SelectEntitiesConnectedToNode(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SelectEntitiesConnectedToNode](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L289)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-selectentitiesconnectedtomember"></a>

### 44. SelectEntitiesConnectedToMember

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga8437531312f47c940bedda5fc9704623) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga8437531312f47c940bedda5fc9704623) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga8437531312f47c940bedda5fc9704623)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult SelectEntitiesConnectedToMember; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::SelectEntitiesConnectedToMember(const VARIANT FAR&entityType,const VARIANT FAR&memberNo)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `entityType` | `const VARIANT FAR&` | [in] | Variable that holds entity type. Values may be as follows: [value table below] |
| `memberNo` | `const VARIANT FAR&` | [in] | Variable that holds Member numbers with which connected entities needs to be selected. |

**Contract, including return/status semantics:** Select entities as specified in type and connected with the specified Member. Parameters [in] entityType Variable that holds entity type. Values may be as follows: ID Entity Type 0 Geometry 1 Beam 2 Plate 3 Solid [in] memberNo Variable that holds Member numbers with which connected entities needs to be selected. C++ Syntax VBA Syntax

**Value table 1** (source column groupings retained):

| ID | Entity Type |
| --- | --- |
| 0 | Geometry |
| 1 | Beam |
| 2 | Plate |
| 3 | Solid |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SelectEntitiesConnectedToMember(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SelectEntitiesConnectedToMember](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L284)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-selectentitiesconnectedtoplate"></a>

### 45. SelectEntitiesConnectedToPlate

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gae001bc5625772b7a4bd626da5af874a3) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gae001bc5625772b7a4bd626da5af874a3) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gae001bc5625772b7a4bd626da5af874a3)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult SelectEntitiesConnectedToPlate; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::SelectEntitiesConnectedToPlate(const VARIANT FAR&entityType,const VARIANT FAR&plateNo)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `entityType` | `const VARIANT FAR&` | [in] | Variable that holds entity type. Values may be as follows: [value table below] |
| `plateNo` | `const VARIANT FAR&` | [in] | Variable that holds Plate numbers with which connected entities needs to be selected. |

**Contract, including return/status semantics:** Select entities as specified in type and connected with the specified Plate. Parameters [in] entityType Variable that holds entity type. Values may be as follows: ID Entity Type 0 Geometry 1 Beam 2 Plate 3 Solid [in] plateNo Variable that holds Plate numbers with which connected entities needs to be selected. C++ Syntax VBA Syntax

**Value table 1** (source column groupings retained):

| ID | Entity Type |
| --- | --- |
| 0 | Geometry |
| 1 | Beam |
| 2 | Plate |
| 3 | Solid |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SelectEntitiesConnectedToPlate(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SelectEntitiesConnectedToPlate](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L294)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-selectentitiesconnectedtosolid"></a>

### 46. SelectEntitiesConnectedToSolid

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga48f83d8c5587aed20fe6ca3a624b0868) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga48f83d8c5587aed20fe6ca3a624b0868) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga48f83d8c5587aed20fe6ca3a624b0868)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult SelectEntitiesConnectedToSolid; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
void OSViewUI::SelectEntitiesConnectedToSolid(const VARIANT FAR&entityType,const VARIANT FAR&solidNo)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `entityType` | `const VARIANT FAR&` | [in] | Variable that holds entity type. Values may be as follows: [value table below] |
| `solidNo` | `const VARIANT FAR&` | [in] | Variable that holds Solid numbers with which connected entities needs to be selected. |

**Contract, including return/status semantics:** Select entities as specified in type and connected with the specified Solid. Parameters [in] entityType Variable that holds entity type. Values may be as follows: ID Entity Type 0 Geometry 1 Beam 2 Plate 3 Solid [in] solidNo Variable that holds Solid numbers with which connected entities needs to be selected. C++ Syntax VBA Syntax

**Value table 1** (source column groupings retained):

| ID | Entity Type |
| --- | --- |
| 0 | Geometry |
| 1 | Beam |
| 2 | Plate |
| 3 | Solid |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void SelectEntitiesConnectedToSolid(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SelectEntitiesConnectedToSolid](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L299)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-getnoofbeamsinview"></a>

### 47. GetNoOfBeamsInView

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga88f9646f7632fa5aabe61edde28ebcae) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga88f9646f7632fa5aabe61edde28ebcae) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga88f9646f7632fa5aabe61edde28ebcae)<br>**Current:** Pending; V4.

**Managed proposal:** `ViewQueryResult<T> GetNoOfBeamsInView; T is the documented int count, string title or managed interface-mode enum, never a COM object.`

#### 2024 contract

~~~cpp
VARIANT OSViewUI::GetNoOfBeamsInView()
~~~

No native parameters.

**Contract, including return/status semantics:** Get No Of Beams In View. C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object GetNoOfBeamsInView()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [GetNumberOfBeamsInView](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L410)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-getbeamsinview"></a>

### 48. GetBeamsInView

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gac780123fd4fbb94dcd47924189aba79d) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gac780123fd4fbb94dcd47924189aba79d) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gac780123fd4fbb94dcd47924189aba79d)<br>**Current:** Pending; V4.

**Managed proposal:** `ViewQueryResult<IReadOnlyList<int>> GetBeamsInView(); bounded count/fill validation.`

#### 2024 contract

~~~cpp
VARIANT OSViewUI::GetBeamsInView(VARIANT FAR&nBeamList)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `nBeamList` | `VARIANT FAR&` | [in] | Collection of beam |

**Contract, including return/status semantics:** Get Beams In View. Parameters [in] nBeamList Collection of beam C++ Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object GetBeamsInView(System.Object ByRef)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [GetBeamsInView](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L417)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-createnewviewforselectionsex"></a>

### 49. CreateNewViewForSelectionsEx

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gaaacd7c65c11f99baca613f39fa59855b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gaaacd7c65c11f99baca613f39fa59855b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gaaacd7c65c11f99baca613f39fa59855b)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult CreateNewViewForSelectionsEx; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
VARIANT OSViewUI::CreateNewViewForSelectionsEx(long windowOptions)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `windowOptions` | `long` | [in] | 0 = Creates a new window for the view, 1 = Display the view in the active window (type - Long). |

**Contract, including return/status semantics:** Creates a new view for the selected objects displayed in the active window based on specified options. Parameters [in] windowOptions 0 = Creates a new window for the view, 1 = Display the view in the active window (type - Long). Return values 1/True Creation of new view is successful. 0/False Creation of new view is unsuccessful. C++ Syntax VBA Syntax See also OSViewUI::CreateNewViewForSelections

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object CreateNewViewForSelectionsEx(Int32)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [CreateNewViewForSelections](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L77)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-exportview"></a>

### 50. ExportView

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga55c816cb668826627941025c09eea221) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga55c816cb668826627941025c09eea221) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga55c816cb668826627941025c09eea221)<br>**Current:** Pending; V4.

**Managed proposal:** `ViewOperationResult ExportView; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
long OSViewUI::ExportView(LPCTSTR fileLocation,LPCTSTR fileName,ImageExportTypes fileFormat,BOOL overwrite = TRUE)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `fileLocation` | `LPCTSTR` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `fileName` | `LPCTSTR` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `fileFormat` | `ImageExportTypes` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `overwrite` | `BOOL` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `FileLocation` | [in] | - Location of the saved view file (type - LPCTSTR, Folder need to be present otherwise it will return -100) . |
| `FileName` | [in] | - Name of the saved view file(type - LPCTSTR). |
| `FileFormat` | [in] | 0 = bmp, 1 = jpg, 2 = tga, 3 = tif - Create the view in the specific format (type - Long). |
| `Overwrite` | [in] | - Boolean for provide option to overwrite an existing file. True - Allow Overwrite False - No overwrite |

**Contract, including return/status semantics:** Used for exporting the information displayed in the active view window into a standard, graphical image format (e.g., Bitmap, JPEG, TIFF et.). Parameters [in] FileLocation - Location of the saved view file (type - LPCTSTR, Folder need to be present otherwise it will return -100) . [in] FileName - Name of the saved view file(type - LPCTSTR). [in] FileFormat 0 = bmp, 1 = jpg, 2 = tga, 3 = tif - Create the view in the specific format (type - Long). [in] Overwrite - Boolean for provide option to overwrite an existing file. True - Allow Overwrite False - No overwrite Return values 1 Export view is successful. -1 Generic Error. -100 Invalid Argument. -1003 File already exist. C++ Syntax C# Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Int32 ExportView(System.String, System.String, OpenSTAADUI.ImageExportTypes)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [ExportActiveView](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L400)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-copypicture"></a>

### 51. CopyPicture

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga27cff2188feded7803f31424004b4cf4) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga27cff2188feded7803f31424004b4cf4) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga27cff2188feded7803f31424004b4cf4)<br>**Current:** Pending; V4.

**Managed proposal:** `ViewQueryResult<PixelSize> CopyPicture(); owned PixelSize class (Width, Height), no tuple.`

#### 2024 contract

~~~cpp
VARIANT OSViewUI::CopyPicture(long*xDim,long*yDim)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `xDim` | `long*` | [out] | size of image in x direction (Length in Pixel). |
| `yDim` | `long*` | [out] | size of image in y direction (width in Pixel). |

**Contract, including return/status semantics:** Copy active view to clipbord and gives size of image in it's referance variables. It does not copy table views but works with Structure View, Graphs, 3d Rendered view. Parameters [out] xDim size of image in x direction (Length in Pixel). [out] yDim size of image in y direction (width in Pixel). Return values 1/True Copy view is successful. 0/False Copy view is unsuccessful. C++ Syntax C# Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object CopyPicture(Int32 ByRef, Int32 ByRef)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [CopyPicture](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L391)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-setwindowposition"></a>

### 52. SetWindowPosition

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga6ea386f9f5bcc55899a1dfb7fbebca72) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga6ea386f9f5bcc55899a1dfb7fbebca72) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga6ea386f9f5bcc55899a1dfb7fbebca72)<br>**Current:** Pending; V4.

**Managed proposal:** `ViewOperationResult SetWindowPosition; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
VARIANT OSViewUI::SetWindowPosition(long xTop,long yTop,long xWindow,long yWindow)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `xTop` | `long` | [in] | - Horizontal distance in pixels from top left corner of the desktop to the top left corner of the active window. |
| `yTop` | `long` | [in] | - Vertical distance in pixels down from top left corner of the desktop to the top left corner of the active window. |
| `xWindow` | `long` | [in] | - Width of window in pixels. xTop + xWindow must be less than the application desktop width. |
| `yWindow` | `long` | [in] | - Height of window in pixels. yTop + yWindow must be less than the application desktop height. |

**Contract, including return/status semantics:** Resize and reposition active window. Note that, this function only sets windows such as they fully fit in the application desktop. See OSViewUI::GetApplicationDesktopSize to determine the size of the application desktop. This function resizes and changes position of windows in all state. if window is in minimized or maximized state it will restore it and reposition and resize it. API works with view window as well as the grid tables. Parameters [in] xTop - Horizontal distance in pixels from top left corner of the desktop to the top left corner of the active window. [in] yTop - Vertical distance in pixels down from top left corner of the desktop to the top left corner of the active window. [in] xWindow - Width of window in pixels. xTop + xWindow must be less than the application desktop width. [in] yWindow - Height of window in pixels. yTop + yWindow must be less than the application desktop height. Return values 1/True is successful. 0/False is unsuccessful. C++ Syntax VBA Syntax See also OSViewUI::GetApplicationDesktopSize

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object SetWindowPosition(Int32, Int32, Int32, Int32)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetActiveWindowPositionAndSize](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L57)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-getapplicationdesktopsize"></a>

### 53. GetApplicationDesktopSize

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga77d5ca16ddbf42967f848a9997f74681) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga77d5ca16ddbf42967f848a9997f74681) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga77d5ca16ddbf42967f848a9997f74681)<br>**Current:** Pending; V4.

**Managed proposal:** `ViewQueryResult<PixelSize> GetApplicationDesktopSize(); owned PixelSize class (Width, Height), no tuple.`

#### 2024 contract

~~~cpp
VARIANT OSViewUI::GetApplicationDesktopSize(long*xDim,long*yDim)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `xDim` | `long*` | [out] | size of image in x direction (Length in pixel). |
| `yDim` | `long*` | [out] | size of image in y direction (width in pixel). |

**Contract, including return/status semantics:** This provides the dimension of the application desktop in pixels measured from the top left of view window to the bottom right. This is the grey zone on which all windows and dialogs in STAAD.Pro are placed. This should be used to determine the values to position the current application window using the function OSViewUI::SetWindowPosition . Parameters [out] xDim size of image in x direction (Length in pixel). [out] yDim size of image in y direction (width in pixel). Return values 1/True Successfully obtained the dimensions of the application desktop.. 0/False Failed to obtain the dimensions of the application desktop. C++ Syntax C# Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object GetApplicationDesktopSize(Int32 ByRef, Int32 ByRef)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [GetApplicationDesktopSize](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L65)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-getscalevalues"></a>

### 54. GetScaleValues

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga5e67303a2568385954aed989bfbe5e5f) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga5e67303a2568385954aed989bfbe5e5f) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga5e67303a2568385954aed989bfbe5e5f)<br>**Current:** Pending; V3.

**Managed proposal:** `ViewQueryResult<ViewScaleSnapshot> GetScaleValues(); all 14 documented slots plus preserved unknown entries.`

#### 2024 contract

~~~cpp
long OSViewUI::GetScaleValues(VARIANT&ScalesArray)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ScalesArray` | `VARIANT&` | [out] | Array of double type and size same as number of scale types. API gets all the values in this array. see OSViewUI::GetScaleCount for getsize of scalesArray. Scale type and ID in array will be as following. [value table below] |

**Contract, including return/status semantics:** Obtain the current set of scales used for displaying loads and results shown in the Diagrams&gt;Scales dialog. The values are provided in an array which should be sized using the value returned by the function OSViewUI::GetScaleCount . To obtain the value of only one specific scale item, see the function OSViewUI::GetScaleValueByType . Note that the values are returned in base units. See function OpenSTAADUI::GetBaseUnit . Parameters [out] ScalesArray Array of double type and size same as number of scale types. API gets all the values in this array. see OSViewUI::GetScaleCount for getsize of scalesArray. Scale type and ID in array will be as following. ID Type Scale Items Unit per length 0 Loads Point Force Force 1 Loads Dist. Force Force/length 2 Loads Point Moment Force*length 3 Loads Dist. Moment Force*length/length 4 Loads Pressure Force/length^2 5 Results Bending Y Force*length 6 Results Bending Z Force*length 7 Results Shear Y Force 8 Results Shear Z Force 9 Results Axial Force 10 Results Torsion Force*length 11 Results Displacement Length 12 Results Beam Stress Force/length^2 13 Results Mode Shape (none) Return values 1 Values were successfully obtained. 0 Values could not be obtained. -1 Error with defined array. C++ Syntax C# Syntax VBA Syntax See also OSViewUI::GetScaleValueByType , OSViewUI::GetScaleCount , Scales dialog..."&gt;OSViewUI::SetScaleValues , Scales dia..."&gt;OSViewUI::SetScaleValueByType

**Value table 1** (source column groupings retained):

| ID | Type | Scale Items | Unit per length |
| --- | --- | --- | --- |
| 0 | Loads | Point Force | Force |
| 1 | Loads | Dist. Force | Force/length |
| 2 | Loads | Point Moment | Force*length |
| 3 | Loads | Dist. Moment | Force*length/length |
| 4 | Loads | Pressure | Force/length^2 |
| 5 | Results | Bending Y | Force*length |
| 6 | Results | Bending Z | Force*length |
| 7 | Results | Shear Y | Force |
| 8 | Results | Shear Z | Force |
| 9 | Results | Axial | Force |
| 10 | Results | Torsion | Force*length |
| 11 | Results | Displacement | Length |
| 12 | Results | Beam Stress | Force/length^2 |
| 13 | Results | Mode Shape | (none) |

#### 2025 contract

~~~cpp
long OSViewUI::GetScaleValues(VARIANT&ScalesArray)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ScalesArray` | `VARIANT&` | [out] | Array of double type and size same as number of scale types. API gets all the values in this array. see OSViewUI::GetScaleCount for getsize of scalesArray. Scale type and ID in array will be as following. [value table below] |

**Contract, including return/status semantics:** Obtain the current set of scales used for displaying loads and results shown in the Diagrams&gt;Scales dialog. The values are provided in an array which should be sized using the value returned by the function OSViewUI::GetScaleCount . To obtain the value of only one specific scale item, see the function OSViewUI::GetScaleValueByType . Note that the values are returned in base units. See function OpenSTAADUI::GetBaseUnit. Parameters [out] ScalesArray Array of double type and size same as number of scale types. API gets all the values in this array. see OSViewUI::GetScaleCount for getsize of scalesArray. Scale type and ID in array will be as following. ID Type Scale Items Unit per length 0 Loads Point Force Force 1 Loads Dist. Force Force/length 2 Loads Point Moment Force*length 3 Loads Dist. Moment Force*length/length 4 Loads Pressure Force/length^2 5 Results Bending Y Force*length 6 Results Bending Z Force*length 7 Results Shear Y Force 8 Results Shear Z Force 9 Results Axial Force 10 Results Torsion Force*length 11 Results Displacement Length 12 Results Beam Stress Force/length^2 13 Results Mode Shape (none) Return values 1 Values were successfully obtained. 0 Values could not be obtained. -1 Error with defined array. C++ Syntax C# Syntax VBA Syntax See also OSViewUI::GetScaleValueByType , OSViewUI::GetScaleCount , OSViewUI::SetScaleValues , OSViewUI::SetScaleValueByType

**Value table 1** (source column groupings retained):

| ID | Type | Scale Items | Unit per length |
| --- | --- | --- | --- |
| 0 | Loads | Point Force | Force |
| 1 | Loads | Dist. Force | Force/length |
| 2 | Loads | Point Moment | Force*length |
| 3 | Loads | Dist. Moment | Force*length/length |
| 4 | Loads | Pressure | Force/length^2 |
| 5 | Results | Bending Y | Force*length |
| 6 | Results | Bending Z | Force*length |
| 7 | Results | Shear Y | Force |
| 8 | Results | Shear Z | Force |
| 9 | Results | Axial | Force |
| 10 | Results | Torsion | Force*length |
| 11 | Results | Displacement | Length |
| 12 | Results | Beam Stress | Force/length^2 |
| 13 | Results | Mode Shape | (none) |

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Int32 GetScaleValues(System.Object ByRef)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [GetScaleValues](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L356)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-setscalevalues"></a>

### 55. SetScaleValues

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gaa4262a1907f1cdf93432d376c8c83165) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gaa4262a1907f1cdf93432d376c8c83165) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gaa4262a1907f1cdf93432d376c8c83165)<br>**Current:** Pending; V3.

**Managed proposal:** `ViewOperationResult SetScaleValues; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
long OSViewUI::SetScaleValues(const VARIANT&ScalesArray)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ScalesArray` | `const VARIANT&` | [in] | - Array of double type and size same as number of scale types. API sets the values in this array to scale types see OSViewUI::GetScaleCount for getsize of scalesArray. For Scale type and ID in array see Scales d..."&gt;OSViewUI::GetScaleValues . |

**Contract, including return/status semantics:** Set the scales used for displaying loads and results as shown in the Diagrams&gt;Scales dialog. The values should be provided in an array which should be sized using the value returned by the function OSViewUI::GetScaleCount . To set the value of only one specific scale item, see the function Scales dia..."&gt;OSViewUI::SetScaleValueByType . Note that the values should be set in base units. See the function OpenSTAADUI::GetBaseUnit . To display/hide loads/results, see the function OSViewUI::SetDiagramMode . After setting the value, the display should be refreshed using the function OSViewUI::RefreshView() . Parameters [in] ScalesArray - Array of double type and size same as number of scale types. API sets the values in this array to scale types see OSViewUI::GetScaleCount for getsize of scalesArray. For Scale type and ID in array see Scales d..."&gt;OSViewUI::GetScaleValues . Return values 1 Values were successfully updated. 0 Values could not be updated. -1 Error with defined array. C++ Syntax C# Syntax VBA Syntax See also Scales dia..."&gt;OSViewUI::SetScaleValueByType , OSViewUI::GetScaleCount , Scales d..."&gt;OSViewUI::GetScaleValues , OSViewUI::GetScaleValueByType

#### 2025 contract

~~~cpp
long OSViewUI::SetScaleValues(const VARIANT&ScalesArray)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `ScalesArray` | `const VARIANT&` | [in] | - Array of double type and size same as number of scale types. API sets the values in this array to scale types see OSViewUI::GetScaleCount for getsize of scalesArray. For Scale type and ID in array see OSViewUI::GetScaleValues . |

**Contract, including return/status semantics:** Set the scales used for displaying loads and results as shown in the Diagrams&gt;Scales dialog. The values should be provided in an array which should be sized using the value returned by the function OSViewUI::GetScaleCount . To set the value of only one specific scale item, see the function OSViewUI::SetScaleValueByType . Note that the values should be set in base units. See the function OpenSTAADUI::GetBaseUnit. To display/hide loads/results, see the function OSViewUI::SetDiagramMode . After setting the value, the display should be refreshed using the function OSViewUI::RefreshView() . Parameters [in] ScalesArray - Array of double type and size same as number of scale types. API sets the values in this array to scale types see OSViewUI::GetScaleCount for getsize of scalesArray. For Scale type and ID in array see OSViewUI::GetScaleValues . Return values 1 Values were successfully updated. 0 Values could not be updated. -1 Error with defined array. C++ Syntax C# Syntax VBA Syntax See also OSViewUI::SetScaleValueByType , OSViewUI::GetScaleCount , OSViewUI::GetScaleValues , OSViewUI::GetScaleValueByType

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Int32 SetScaleValues(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetScaleValues](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L368)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-getscalevaluebytype"></a>

### 56. GetScaleValueByType

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga151f4248482bba01512bc6e53e447c7e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga151f4248482bba01512bc6e53e447c7e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga151f4248482bba01512bc6e53e447c7e)<br>**Current:** Pending; V3.

**Managed proposal:** `ViewQueryResult<double> GetScaleValue(ViewScaleKind scaleKind); native type mapping kept internal.`

#### 2024 contract

~~~cpp
VARIANT OSViewUI::GetScaleValueByType(long scaleTypeId,double*value)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `scaleType` | `long` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `value` | `double*` | [out] | Value of scale type listed, in Base Units (Type: Double). |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `scaleTypeId` | [in] | The index of the required load or result type (Type: Long). Refer to the function Scales d..."&gt;OSViewUI::GetScaleValues for the ID list. |

**Contract, including return/status semantics:** Obtain the value of the scale that is used to display a specified load or result diagram as defined in the Diagrams&gt;Scales dialog depending upon the scale ID passed. Note that the values are returned in base units. See function OpenSTAADUI::GetBaseUnit . To obtain the value of all available scale items, see the function Scales d..."&gt;OSViewUI::GetScaleValues . . Parameters [in] scaleTypeId The index of the required load or result type (Type: Long). Refer to the function Scales d..."&gt;OSViewUI::GetScaleValues for the ID list. [out] value Value of scale type listed, in Base Units (Type: Double). Return values 1/TRUE Value was successfully retrieved. 0/FALSE Value could not be retrieved. C++ Syntax C# Syntax VBA Syntax See also Scales d..."&gt;OSViewUI::GetScaleValues , OSViewUI::GetScaleCount , Scales dialog..."&gt;OSViewUI::SetScaleValues , Scales dia..."&gt;OSViewUI::SetScaleValueByType

#### 2025 contract

~~~cpp
VARIANT OSViewUI::GetScaleValueByType(long scaleTypeId,double*value)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `scaleType` | `long` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `value` | `double*` | [out] | Value of scale type listed, in Base Units (Type: Double). |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `scaleTypeId` | [in] | The index of the required load or result type (Type: Long). Refer to the function OSViewUI::GetScaleValues for the ID list. |

**Contract, including return/status semantics:** Obtain the value of the scale that is used to display a specified load or result diagram as defined in the Diagrams&gt;Scales dialog depending upon the scale ID passed. Note that the values are returned in base units. See function OpenSTAADUI::GetBaseUnit. To obtain the value of all available scale items, see the function OSViewUI::GetScaleValues . . Parameters [in] scaleTypeId The index of the required load or result type (Type: Long). Refer to the function OSViewUI::GetScaleValues for the ID list. [out] value Value of scale type listed, in Base Units (Type: Double). Return values 1/TRUE Value was successfully retrieved. 0/FALSE Value could not be retrieved. C++ Syntax C# Syntax VBA Syntax See also OSViewUI::GetScaleValues , OSViewUI::GetScaleCount , OSViewUI::SetScaleValues , OSViewUI::SetScaleValueByType

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Int32 GetScaleValueByType(Int32, Double ByRef)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [GetScaleValueByType](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L346)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-setscalevaluebytype"></a>

### 57. SetScaleValueByType

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga3c4dae5aaca0adc4a17ecb2402c8651e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga3c4dae5aaca0adc4a17ecb2402c8651e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga3c4dae5aaca0adc4a17ecb2402c8651e)<br>**Current:** Pending; V3.

**Managed proposal:** `ViewOperationResult SetScaleValueByType; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
VARIANT OSViewUI::SetScaleValueByType(long scaleTypeId,double value)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `scaleType` | `long` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `value` | `double` | [in] | Value of scale type to be used (Type: Double), defined in Base Units. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `scaleTypeId` | [in] | The index of the required load or result type to be set (Type: Long). Refer to the function Scales d..."&gt;OSViewUI::GetScaleValues for the ID list. |

**Contract, including return/status semantics:** Set the scale used for displaying a chosen load or result diagram as shown in the Diagrams&gt;Scales dialog. See also the function Scales dialog..."&gt;OSViewUI::SetScaleValues to set all scales in a single call. After setting the value, the display should be refreshed using the function OSViewUI::RefreshView() . To display/hide loads/results, see the function OSViewUI::SetDiagramMode . Note that the values are returned in base units. See function OpenSTAADUI::GetBaseUnit . Parameters [in] scaleTypeId The index of the required load or result type to be set (Type: Long). Refer to the function Scales d..."&gt;OSViewUI::GetScaleValues for the ID list. [in] value Value of scale type to be used (Type: Double), defined in Base Units. Return values 1/TRUE Value was successfully updated. 0/FALSE Value could not be updated. C++ Syntax C# Syntax VBA Syntax See also Scales dialog..."&gt;OSViewUI::SetScaleValues , OSViewUI::GetScaleCount , Scales d..."&gt;OSViewUI::GetScaleValues , OSViewUI::GetScaleValueByType

#### 2025 contract

~~~cpp
VARIANT OSViewUI::SetScaleValueByType(long scaleTypeId,double value)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `scaleType` | `long` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `value` | `double` | [in] | Value of scale type to be used (Type: Double), defined in Base Units. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `scaleTypeId` | [in] | The index of the required load or result type to be set (Type: Long). Refer to the function OSViewUI::GetScaleValues for the ID list. |

**Contract, including return/status semantics:** Set the scale used for displaying a chosen load or result diagram as shown in the Diagrams&gt;Scales dialog. See also the function OSViewUI::SetScaleValues to set all scales in a single call. After setting the value, the display should be refreshed using the function OSViewUI::RefreshView() . To display/hide loads/results, see the function OSViewUI::SetDiagramMode . Note that the values are returned in base units. See function OpenSTAADUI::GetBaseUnit. Parameters [in] scaleTypeId The index of the required load or result type to be set (Type: Long). Refer to the function OSViewUI::GetScaleValues for the ID list. [in] value Value of scale type to be used (Type: Double), defined in Base Units. Return values 1/TRUE Value was successfully updated. 0/FALSE Value could not be updated. C++ Syntax C# Syntax VBA Syntax See also OSViewUI::SetScaleValues , OSViewUI::GetScaleCount , OSViewUI::GetScaleValues , OSViewUI::GetScaleValueByType

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Int32 SetScaleValueByType(Int32, Double)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetScaleValueByType](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L363)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-getscalecount"></a>

### 58. GetScaleCount

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga332159324e4ada480a7fad4739fbdb5f) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga332159324e4ada480a7fad4739fbdb5f) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga332159324e4ada480a7fad4739fbdb5f)<br>**Current:** Pending; V3.

**Managed proposal:** `ViewQueryResult<T> GetScaleCount; T is the documented int count, string title or managed interface-mode enum, never a COM object.`

#### 2024 contract

~~~cpp
long OSViewUI::GetScaleCount()
~~~

No native parameters.

**Contract, including return/status semantics:** Returns the count of scales that are used in STAAD.Pro which can be read and set using the functions Scales d..."&gt;OSViewUI::GetScaleValues and Scales dialog..."&gt;OSViewUI::SetScaleValues . Return values Positive_Value Count of scales. 0/Negative_Value Unsuccessful. C++ Syntax C# Syntax VBA Syntax See also Scales d..."&gt;OSViewUI::GetScaleValues , Scales dialog..."&gt;OSViewUI::SetScaleValues , OSViewUI::GetScaleValueByType , Scales dia..."&gt;OSViewUI::SetScaleValueByType

#### 2025 contract

~~~cpp
long OSViewUI::GetScaleCount()
~~~

No native parameters.

**Contract, including return/status semantics:** Returns the count of scales that are used in STAAD.Pro which can be read and set using the functions OSViewUI::GetScaleValues and OSViewUI::SetScaleValues . Return values Positive_Value Count of scales. 0/Negative_Value Unsuccessful. C++ Syntax C# Syntax VBA Syntax See also OSViewUI::GetScaleValues , OSViewUI::SetScaleValues , OSViewUI::GetScaleValueByType , OSViewUI::SetScaleValueByType

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Int32 GetScaleCount()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [GetScaleCount](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L339)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-detachview"></a>

### 59. DetachView

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga2184529998c62f34ef9f15c072f634ee) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga2184529998c62f34ef9f15c072f634ee) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga2184529998c62f34ef9f15c072f634ee)<br>**Current:** Pending; V4.

**Managed proposal:** `ViewOperationResult DetachView; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
long OSViewUI::DetachView()
~~~

No native parameters.

**Contract, including return/status semantics:** Remove a view from the collection of saved views. The view to be removed must be open and the active window. Once the view is detached, the active window becomes &lt;untitled&gt;. To set the active window see the function OSViewUI::SetActiveWindow . Note that the active window view cannot be Whole Structure or untitled. Return values 1 View successfully detached. 0 Unsuccessful -1 Generic Error C++ Syntax C# Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Int32 DetachView()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [DetachActiveView](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L99)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-renameview"></a>

### 60. RenameView

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#gadd58ac54d096b7dedb1fe8f2dc7cfc5d) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#gadd58ac54d096b7dedb1fe8f2dc7cfc5d) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#gadd58ac54d096b7dedb1fe8f2dc7cfc5d)<br>**Current:** Pending; V4.

**Managed proposal:** `ViewOperationResult RenameView; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
long OSViewUI::RenameView(LPCTSTR viewName)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `viewName` | `LPCTSTR` | [in] | - New name of the saved view (type - LPCTSTR). |

**Contract, including return/status semantics:** Renames a saved view. The view should be open and be the active window. Note that the new name should not be the same as any other existing saved view. To rename as an existing saved view name, the existing view should first be detached. See the function OSViewUI::DetachView . Also the current window cannot be 'Whole Structure' or an untitled view. See the function OSViewUI::SetActiveWindow to ensure the view is set as the active window. Parameters [in] viewName - New name of the saved view (type - LPCTSTR). Return values 1 Rename view is successful. 0 Unsuccessful 2 View name already used. -1 Generic Error -100 Invalid Argument C++ Syntax C# Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Int32 RenameView(System.String)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [RenameActiveView](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L94)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-openview"></a>

### 61. OpenView

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga830b1763a33ed1e519d6acd110178d97) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga830b1763a33ed1e519d6acd110178d97) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga830b1763a33ed1e519d6acd110178d97)<br>**Current:** Pending; V4.

**Managed proposal:** `ViewOperationResult OpenView; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
long OSViewUI::OpenView(LPCTSTR viewName,BOOL windowOptions)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `viewName` | `LPCTSTR` | [in] | - New name of the saved view (type - LPCTSTR). |
| `OpenInNewWindow` | `BOOL` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `windowOptions` | [in] | - False / 0 = Creates a new window for the view which becomes the active window, True / 1 = Display the view in the current active window (type - BOOL). |

**Contract, including return/status semantics:** Open a previously saved view in either the active window or create a new window which becomes the active window. Parameters [in] viewName - New name of the saved view (type - LPCTSTR). [in] windowOptions - False / 0 = Creates a new window for the view which becomes the active window, True / 1 = Display the view in the current active window (type - BOOL). Return values 1 View Successfully opened. 0 Unsuccessful 2 View name does not exist. -1 Generic Error -100 Invalid Argument C++ Syntax C# Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Int32 OpenView(System.String, Int32)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [OpenView](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L82)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-saveview"></a>

### 62. SaveView

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga9d4c42c49801d5a7e286255f04d91fa8) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga9d4c42c49801d5a7e286255f04d91fa8) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga9d4c42c49801d5a7e286255f04d91fa8)<br>**Current:** Pending; V4.

**Managed proposal:** `ViewOperationResult SaveView; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
long OSViewUI::SaveView(LPCTSTR viewName,BOOL overWrite)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `viewName` | `LPCTSTR` | [in] | - New name for the view (type - LPCTSTR). |
| `overWriteIfExist` | `BOOL` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `overWrite` | [in] | - Option to overwrite if the given viewName already exists. (type - BOOL). 0/FALSE = Do not overwrite. 1/TRUE = Overwrite viewName if it exists. |

**Contract, including return/status semantics:** Save the active graphic view to the collection of saved views which can be opened using the function OSViewUI::OpenView . Parameters [in] viewName - New name for the view (type - LPCTSTR). [in] overWrite - Option to overwrite if the given viewName already exists. (type - BOOL). 0/FALSE = Do not overwrite. 1/TRUE = Overwrite viewName if it exists. Return values 1 Save view is successful. 0 Unsuccessful 2 View name already exist and overWrite is false. -1 Generic Error -100 Invalid Argument C++ Syntax C# Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Int32 SaveView(System.String, Int32)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SaveView](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L89)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-getwindowtitle"></a>

### 63. GetWindowTitle

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga7098acb0d27c1578bd126a1a6078b8cd) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga7098acb0d27c1578bd126a1a6078b8cd) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga7098acb0d27c1578bd126a1a6078b8cd)<br>**Current:** Pending; V4.

**Managed proposal:** `ViewQueryResult<T> GetWindowTitle; T is the documented int count, string title or managed interface-mode enum, never a COM object.`

#### 2024 contract

~~~cpp
VARIANT OSViewUI::GetWindowTitle(long id)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `id` | `long` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `ID` | [in] | The index of the required Window (Type: Long). Note that IDs start from 1. Window IDs depend on the creation time; that is the last created window will have the last ID. Windows which are listed under in View tab &gt; Windows Dropdown are supported in this API. |

**Contract, including return/status semantics:** Returns the Title of the Window. Parameters [in] ID The index of the required Window (Type: Long). Note that IDs start from 1. Window IDs depend on the creation time; that is the last created window will have the last ID. Windows which are listed under in View tab &gt; Windows Dropdown are supported in this API. Return values &lt;VARIANT&gt; The Window string title. Empty_String Window id not found. C++ Syntax C# Syntax VBA Syntax See also OSViewUI::GetWindowCount , OSViewUI::SetActiveWindow .

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object GetWindowTitle(Int32)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [GetWindowTitle](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L47)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-getwindowcount"></a>

### 64. GetWindowCount

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga9834880afec1f0955064a982db108a37) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga9834880afec1f0955064a982db108a37) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga9834880afec1f0955064a982db108a37)<br>**Current:** Pending; V4.

**Managed proposal:** `ViewQueryResult<T> GetWindowCount; T is the documented int count, string title or managed interface-mode enum, never a COM object.`

#### 2024 contract

~~~cpp
long OSViewUI::GetWindowCount()
~~~

No native parameters.

**Contract, including return/status semantics:** Get the number of windows currently open. This includes both graphic windows and tables. Return values Positive_Number The count of open Window. 0 Error. C++ Syntax C# Syntax VBA Syntax See also OSViewUI::GetWindowTitle , OSViewUI::SetActiveWindow .

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Int32 GetWindowCount()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [GetWindowCount](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L42)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-closeactivewindow"></a>

### 65. CloseActiveWindow

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga42ee1d56567dd99c7847c59ee40951d6) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga42ee1d56567dd99c7847c59ee40951d6) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga42ee1d56567dd99c7847c59ee40951d6)<br>**Current:** Pending; V4.

**Managed proposal:** `ViewOperationResult CloseActiveWindow; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
VARIANT OSViewUI::CloseActiveWindow()
~~~

No native parameters.

**Contract, including return/status semantics:** Closes the active graphic or table window, however there must be at least one graphic window remaining open. Note that window at position 1 can not be closed. Return values 1/True Window closed. 0/FALSE Unsuccessful. C++ Syntax C# Syntax VBA Syntax See also OSViewUI::OpenView , OSViewUI::SetActiveWindow .

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object CloseActiveWindow()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [CloseActiveWindow](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L37)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-setactivewindow"></a>

### 66. SetActiveWindow

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga6517f51dde92325fe29013895bb08c5d) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga6517f51dde92325fe29013895bb08c5d) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga6517f51dde92325fe29013895bb08c5d)<br>**Current:** Pending; V4.

**Managed proposal:** `ViewOperationResult SetActiveWindow; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
VARIANT OSViewUI::SetActiveWindow(long id)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `id` | `long` | [in] | - The id of the window to be made the active window (Type: Long). |

**Contract, including return/status semantics:** Set a given window (active graphic or table window) with the provided id as the active window. The indexing starts from 1. See the function OSViewUI::GetWindowCount to obtain the number of windows currently open. Parameters [in] id - The id of the window to be made the active window (Type: Long). Return values 1/TRUE Successful. 0/FALSE Unsuccessful. C++ Syntax C# Syntax VBA Syntax See also OSViewUI::OpenView , OSViewUI::GetWindowCount , OSViewUI::GetWindowTitle .

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object SetActiveWindow(Int32)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetActiveWindow](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L52)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="view-setdesignresults"></a>

### 67. SetDesignResults

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html#ga4ee212aae23382b7d626e33ec1418c6a) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html#ga4ee212aae23382b7d626e33ec1418c6a) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html#ga4ee212aae23382b7d626e33ec1418c6a)<br>**Current:** Pending; V2.

**Managed proposal:** `ViewOperationResult SetDesignResults; named scalar/enum inputs or an immutable options class for multi-field operations; ID and entity overloads where justified.`

#### 2024 contract

~~~cpp
long OSViewUI::SetDesignResults(long utilization,BOOL color,BOOL ShowValues)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `utilization` | `long` | [in] | - Value of type Long. (0 = None, 1 = Actual Ratio, 2 = Normalised Ratio.) |
| `color` | `BOOL` | [in] | - Value of type Boolean. (False/0 = Basic Colored, True/1 = Detailed Colored.) |
| `ShowValues` | `BOOL` | [in] | - Value of type Boolean. (False/0 = Do Not Show Values, True/1 = Show Values.) |

**Contract, including return/status semantics:** Sets Design Results to active view, this function replicates the setting of Design Results in the Diagrams&gt;Design Results dialog. Parameters [in] utilization - Value of type Long. (0 = None, 1 = Actual Ratio, 2 = Normalised Ratio.) [in] color - Value of type Boolean. (False/0 = Basic Colored, True/1 = Detailed Colored.) [in] ShowValues - Value of type Boolean. (False/0 = Do Not Show Values, True/1 = Show Values.) Return values 1 Set Design Results is successful. 0 Unsuccessful. -1 Generic Error. -2 Design Results not Loaded. -100 Invalid Argument. C++ Syntax C# Syntax VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Int32 SetDesignResults(Int32, Int32, Int32)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [SetDesignResults](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L324)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

## 4. Complete private-source overload mapping

| Main managed signature | Return | Native targets / delegation | Reference |
| --- | --- | --- | --- |
| `CloseActiveWindow()` | `bool` | `CloseActiveWindow` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L42) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L35) |
| `GetWindowCount()` | `int` | `GetWindowCount` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L50) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L40) |
| `GetWindowTitle(int windowId)` | `string` | `GetWindowTitle` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L55) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L45) |
| `SetActiveWindow(int windowId)` | `int` | `SetActiveWindow` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L62) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L50) |
| `SetActiveWindowPositionAndSize(int left, int top, int pxWidth, int pxHeight)` | `int` | `SetWindowPosition` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L72) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L55) |
| `GetApplicationDesktopSize()` | `(int pxWidth, int pxHeight)` | `GetApplicationDesktopSize` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L77) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L60) |
| `CreateNewViewForSelections()` | `void` | `CreateNewViewForSelections` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L86) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L70) |
| `CreateNewViewForSelections(ViewOptions viewOptions)` | `void` | `CreateNewViewForSelectionsEx` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L94) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L75) |
| `OpenView(string viewName, ViewOptions viewOptions = ViewOptions.ViewInNewWindow)` | `int` | `OpenView` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L102) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L80) |
| `SaveView(string viewName, bool overwrite)` | `int` | `SaveView` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L110) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L85) |
| `RenameActiveView(string newName)` | `int` | `RenameView` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L119) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L92) |
| `DetachActiveView()` | `void` | `DetachView` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L125) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L97) |
| `RefreshView()` | `void` | `RefreshView` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L130) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L102) |
| `ShowFront()` | `void` | `ShowFront` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L137) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L107) |
| `ShowBack()` | `void` | `ShowBack` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L140) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L112) |
| `ShowLeft()` | `void` | `ShowLeft` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L143) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L117) |
| `ShowRight()` | `void` | `ShowRight` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L146) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L122) |
| `ShowPlan()` | `void` | `ShowPlan` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L149) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L127) |
| `ShowBottom()` | `void` | `ShowBottom` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L152) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L132) |
| `ShowIsometric()` | `void` | `ShowIsometric` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L155) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L137) |
| `RotateUp(double angle)` | `void` | `RotateUp` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L163) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L142) |
| `RotateDown(double angle)` | `void` | `RotateDown` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L171) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L147) |
| `RotateLeft(double angle)` | `void` | `RotateLeft` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L179) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L152) |
| `RotateRight(double angle)` | `void` | `RotateRight` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L187) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L157) |
| `SpinLeft(double angle)` | `void` | `SpinLeft` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L195) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L162) |
| `SpinRight(double angle)` | `void` | `SpinRight` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L203) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L167) |
| `ZoomAll()` | `void` | `ZoomAll` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L206) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L172) |
| `ZoomExtentsMainView()` | `void` | `ZoomExtentsMainView` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L209) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L177) |
| `ShowAllMembers()` | `void` | `ShowAllMembers` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L218) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L182) |
| `HideAllMembers()` | `void` | `HideAllMembers` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L223) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L187) |
| `ShowMember(Beam beam)` | `void` | `ShowMember` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L228) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L192) |
| `ShowMembers(IEnumerable<Beam> beams)` | `void` | `ShowMembers` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L233) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L197) |
| `HideMember(Beam beam)` | `void` | `HideMember` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L238) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L204) |
| `HideMembers(IEnumerable<Beam> beams)` | `void` | `HideMembers` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L243) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L209) |
| `HidePlate(Plate plate)` | `void` | `HidePlate` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L248) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L215) |
| `HidePlates(IEnumerable<Plate> plates)` | `void` | `HidePlate` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L253) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L220) |
| `HideSolid(int solidId)` | `void` | `HideSolid` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L258) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L227) |
| `HideSolids(IEnumerable<int> solidIds)` | `void` | `HideSolid` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L263) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L232) |
| `HideSurface(int surfaceId)` | `void` | `HideSurface` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L268) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L237) |
| `HideSurfaces(IEnumerable<int> surfaceIds)` | `void` | `HideSurface` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L273) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L242) |
| `HideEntity(IEntity entity)` | `void` | `HideEntity` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L278) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L247) |
| `SelectEntities(EntityType entityType, IEnumerable<IEntity> entities)` | `void` | `SelectByItemList` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L287) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L252) |
| `SelectGroup(string groupName)` | `void` | `SelectGroup` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L292) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L262) |
| `SelectInverse(EntityType entityType)` | `void` | `SelectInverse` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L297) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L267) |
| `SelectByMissingAttribute(MissingAttribute missingAttribute)` | `void` | `SelectByMissingAttribute` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L302) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L272) |
| `SelectMembersParallelTo(GlobalAxis globalAxis)` | `void` | `SelectMembersParallelTo` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L307) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L277) |
| `SelectEntitiesConnectedToMember(EntityType entityType, Beam beam)` | `void` | `SelectEntitiesConnectedToMember` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L312) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L282) |
| `SelectEntitiesConnectedToNode(EntityType entityType, Node node)` | `void` | `SelectEntitiesConnectedToNode` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L317) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L287) |
| `SelectEntitiesConnectedToPlate(EntityType entityType, Plate plate)` | `void` | `SelectEntitiesConnectedToPlate` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L322) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L292) |
| `SelectEntitiesConnectedToSolid(EntityType entityType, int solidId)` | `void` | `SelectEntitiesConnectedToSolid` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L327) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L297) |
| `SetBeamAnnotationMode(AnnotationType annotationType, DiagramValueDisplayMode displayMode, bool refreshView)` | `void` | `SetBeamAnnotationMode` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L339) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L302) |
| `SetNodeAnnotationMode(NodeAnnotationType nodeAnnotationType, bool refresh)` | `void` | `SetNodeAnnotationMode` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L347) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L307) |
| `SetReactionAnnotationMode(ReactionAnnotationType reactionAnnotationType, bool refresh)` | `void` | `SetReactionAnnotationMode` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L355) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L312) |
| `SetDiagramMode(DiagramType diagramType, bool showLabel, bool refreshView)` | `void` | `SetDiagramMode` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L360) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L317) |
| `SetDesignResults(UtilizationRatioType urType, ColorMode colorMode, bool showValues)` | `int` | `SetDesignResults` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L368) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L322) |
| `SetLabel(LabelType labelType, bool showLabel)` | `void` | `SetLabel` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L373) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L327) |
| `SetSectionView(Plane plane, double minValue, double maxValue)` | `void` | `SetSectionView` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L396) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L332) |
| `GetScaleCount()` | `int` | `GetScaleCount` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L405) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L337) |
| `GetScaleValueByType(int scaleTypeId)` | `double` | `GetScaleValueByType` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L410) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L342) |
| `GetScaleValues()` | `Scales` | `GetScaleCount`, `GetScaleValues` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L415) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L351) |
| `SetScaleValueByType(int scaleTypeId, double scaleValue)` | `int` | `SetScaleValueByType` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L420) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L361) |
| `SetScaleValues(Scales scales)` | `int` | `SetScaleValues` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L425) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L366) |
| `GetInterfaceMode()` | `InterfaceMode` | `GetInterfaceMode` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L434) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L371) |
| `SetWorkflowMode(WorkflowMode workflowMode)` | `int` | `SetInterfaceMode` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L439) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L376) |
| `SetUnitsForActiveView(QuantityType quantityType, string unitString)` | `void` | `SetUnits` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L444) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L381) |
| `CopyPicture()` | `(int pxWidth, int pxHeight)` | `CopyPicture` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L457) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L386) |
| `ExportActiveView(string fileLocation, string fileName, ImageExportType imageExportType, bool overwrite = true)` | `int` | `ExportView` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L462) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L396) |
| `ExportActiveView(string filePath, ImageExportType imageExportType, bool overwrite = true)` | `int` | Managed helper/delegation; inspect source | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L467) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L403) |
| `GetNumberOfBeamsInView()` | `int` | `GetNoOfBeamsInView` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L487) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L408) |
| `GetBeamsInView()` | `IEnumerable<int>` | `GetBeamsInView` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSView.cs#L492) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSViewAdapter.cs#L413) |

Wrapper and ComObject properties are binding contracts, not domain methods. Constructors and private helpers are not counted as public overloads. Existing source signatures above are comparison evidence, not approved signatures for a new public port.

## 5. Passive metadata, constants and fingerprints

2025 and 2026 IOSViewUI method signatures, parameter metadata and referenced enum constants match. Both assemblies have the same identity but different binary hashes; each was inspected in a separate reflection-only process. No native object was activated. The expected 2024 STAAD interop DLL was unavailable for passive inspection.

### OpenSTAADUI.ImageExportTypes

| Constant | Value |
| --- | --- |
| `bmp` | 0 |
| `jpg` | 1 |
| `tga` | 2 |
| `tif` | 3 |
| `ImageType_LAST` | 4 |

Sentinel/end markers are not valid user selections merely because they are enum members.

| Year | Assembly SHA-256 |
| --- | --- |
| 2025 | CC724993AC9119138B7323E8C684997257E2851DE62E1551BA5D6D67A9DADC19 |
| 2026 | 6D153E80ED356A1B167ABB2EF19227FCFF510707A578AD304154652CC6C1F6D4 |

### 2024 help evidence

| Page | SHA-256 | Summary / details |
| --- | --- | --- |
| [Views](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___view.html) | 6fd6192e0a0f5123d1337deef70ac556bfb65caa9c4980985fda8cbec2996fa3 | 67 / 67 |
| [Native class listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_view_u_i.html) | ee518702aea1fedfc8b749677088a6c268a11ac4a73af60aaa2f53e68f42e7c4 | 69 names |
| [Native header listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/osviewui_8h_source.html) | fd6d1bb22ab7e987552fbbe616e3c2e9d60995a120c444225c0a578bb7d68823 | Supporting declaration evidence |

### 2025 help evidence

| Page | SHA-256 | Summary / details |
| --- | --- | --- |
| [Views](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___view.html) | 24ece5db8a7a3acb4bfc7b3782bcab243eb50f4e39ff65729d6bc81b8f9b59a3 | 67 / 67 |
| [Native class listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_view_u_i.html) | 99005cef81a92d47f11eaf01e5bdfb8c15f44f6ad4231d55d593ac1cbc0d7523 | 69 names |
| [Native header listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/osviewui_8h_source.html) | 22c7bc58d7b35830d342b184e38e57eb33218ea8342eb0165d65c487e25ddbfd | Supporting declaration evidence |

### 2026 help evidence

| Page | SHA-256 | Summary / details |
| --- | --- | --- |
| [Views](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___view.html) | dd8beb2f75cc72760c8f20b1578d0e543aa2a0f290375c386cd55b45db85aba3 | 67 / 67 |
| [Native class listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_view_u_i.html) | cbb16d724f96975c695977094b8fa0fe3038262b622485da7c353e38649e8b0d | 69 names |
| [Native header listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/osviewui_8h_source.html) | 842eb5bd4e879ad324cf62c37aa0771ee67f74b5bfcfb128e518a08ef9633989 | Supporting declaration evidence |
