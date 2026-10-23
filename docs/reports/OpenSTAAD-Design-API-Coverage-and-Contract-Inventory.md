# OpenSTAAD Design API coverage and native contract inventory

**Reviewed:** 23 October 2026<br>**Status:** Planning only; no typed Design domain functions are implemented in this repository.

Read the [architecture and implementation plan](OpenSTAAD-Design-Multi-Version-Architecture-and-Plan.md) first. This annex records all 45 native names, all source interface overloads, declarations, logical parameter directions, return conventions, value tables, metadata and proposed managed result shapes. Native names and constants below are evidence; managed names and types are proposals requiring API review.

## 1. Counting and evidence boundaries

| Measure | 2024 | 2025 | 2026 |
| --- | --- | --- | --- |
| Group names | 31 | 45 | 45 |
| Detailed contracts | 31 | 45 | 45 |
| Native class names | 33 | 47 | 47 |
| Inspected automation methods | No installed DLL inspected | 45 | 45 |

Native class implementation/lifecycle helpers excluded from the public port: `OnFinalRelease`, `SetStructure`. Other group pages were scanned for OSDesignUI ownership; no additional domain-owned rows were found. For Design, Output-owned functions under the Design steel page remain in the separate Output report.

Windows native long is 32-bit. VARIANT and LP(D)ISPATCH declarations do not establish a managed payload type or successful operation. Logical directions below come from parameter prose, not merely C++ reference syntax. Unmatched names and stale prose are shown explicitly; parameter positions must be reconciled against metadata and a controlled native fixture before dispatch is approved.

## 2. Complete cross-version function matrix

| Native Design function | Native return | 2024 / 2025 / 2026 | Main native target | Current typed / plan |
| --- | --- | --- | --- | --- |
| `GetMemberDesignParameters` | `long` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#ga873ef30c05c4d69dff3528c5ffdf8306) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#ga873ef30c05c4d69dff3528c5ffdf8306) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#ga873ef30c05c4d69dff3528c5ffdf8306) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSDesignAdapter.cs#L163) | Pending / D2 |
| `CreateDesignBrief` | `long` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gabe19d807c0c64886f1a1919be3c0c3e3) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gabe19d807c0c64886f1a1919be3c0c3e3) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gabe19d807c0c64886f1a1919be3c0c3e3) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSDesignAdapter.cs#L38) | Pending / D2 |
| `AssignDesignParameter` | `long` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gad388f8ab10890423fa60c072dba15c1e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gad388f8ab10890423fa60c072dba15c1e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gad388f8ab10890423fa60c072dba15c1e) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSDesignAdapter.cs#L115) | Pending / D2 |
| `AssignDesignCommand` | `long` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#ga3691808f3cb60303725af5d30d43ee40) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#ga3691808f3cb60303725af5d30d43ee40) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#ga3691808f3cb60303725af5d30d43ee40) | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSDesignAdapter.cs#L145) | Pending / D2 |
| `AssignDesignGroup` | `long` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gac158379b764ecc82c5953973815438ce) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gac158379b764ecc82c5953973815438ce) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gac158379b764ecc82c5953973815438ce) | No target | Pending / D2 |
| `GetDesignBriefCode` | `long` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gaf5f1bd64481fc68cc681c132f4bff2cc) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gaf5f1bd64481fc68cc681c132f4bff2cc) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gaf5f1bd64481fc68cc681c132f4bff2cc) | No target | Pending / D2 |
| `ChineseSteelInitial` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga278ae7ffbaf8d0d06f9a16da92d60686) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga278ae7ffbaf8d0d06f9a16da92d60686) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga278ae7ffbaf8d0d06f9a16da92d60686) | No target | Pending / D3 |
| `ChineseSteelPerformDesign` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#gaadad0c586459e114752afc40ca264b92) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gaadad0c586459e114752afc40ca264b92) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gaadad0c586459e114752afc40ca264b92) | No target | Pending / D3 |
| `ChineseSteelGetSolutionCount` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga71998921f42bac574bab8f05227a29d7) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga71998921f42bac574bab8f05227a29d7) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga71998921f42bac574bab8f05227a29d7) | No target | Pending / D3 |
| `ChineseSteelGetSolution` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga3748d5f4026136903962eef8eeffa239) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga3748d5f4026136903962eef8eeffa239) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga3748d5f4026136903962eef8eeffa239) | No target | Pending / D3 |
| `ChineseSteelAddSolution` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#gad26a5295638e746f5ab1413112f77199) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gad26a5295638e746f5ab1413112f77199) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gad26a5295638e746f5ab1413112f77199) | No target | Pending / D3 |
| `ChineseSteelDeletedSolution` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga89a13b0b6d68cb88ad6814e792d86419) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga89a13b0b6d68cb88ad6814e792d86419) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga89a13b0b6d68cb88ad6814e792d86419) | No target | Pending / D3 |
| `ChineseSteelGetDesignParametersCount` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga23be0a4a9d7d3174b1e4c5956face361) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga23be0a4a9d7d3174b1e4c5956face361) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga23be0a4a9d7d3174b1e4c5956face361) | No target | Pending / D3 |
| `ChineseSteelAddDesignParameter` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#gae82d98cea3139965caa9e2164d029c59) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gae82d98cea3139965caa9e2164d029c59) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gae82d98cea3139965caa9e2164d029c59) | No target | Pending / D3 |
| `ChineseSteelDeletedDesignParameter` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga9559879e869cdbf43ccaf9bf60adb979) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga9559879e869cdbf43ccaf9bf60adb979) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga9559879e869cdbf43ccaf9bf60adb979) | No target | Pending / D3 |
| `ChineseSteelGetDesignParameter` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#gaa3de8522adca8442deecbd5acc6130a7) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gaa3de8522adca8442deecbd5acc6130a7) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gaa3de8522adca8442deecbd5acc6130a7) | No target | Pending / D3 |
| `ChineseSteelSetDesignParameter` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga76f0311b021cc9ac3b3a8e7651ebf7e6) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga76f0311b021cc9ac3b3a8e7651ebf7e6) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga76f0311b021cc9ac3b3a8e7651ebf7e6) | No target | Pending / D3 |
| `ChineseSteelAssignDesigParameter` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga91fbd6d81cf12c745f281f884834a3eb) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga91fbd6d81cf12c745f281f884834a3eb) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga91fbd6d81cf12c745f281f884834a3eb) | No target | Pending / D3 |
| `ChineseSteelGetCheckItem` | `VARIANT` | Absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gab07918fff1ccf3d77181402ee0d832cb) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gab07918fff1ccf3d77181402ee0d832cb) | No target | Pending / D3 |
| `ChineseSteelSetCheckItem` | `VARIANT` | Absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga170b6c2a5b67f85db1cb8ff371bcbced) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga170b6c2a5b67f85db1cb8ff371bcbced) | No target | Pending / D3 |
| `ChineseSteelGetCheckOption` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga5143ed8fdb53208773dfbba2d0ba2ee6) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga5143ed8fdb53208773dfbba2d0ba2ee6) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga5143ed8fdb53208773dfbba2d0ba2ee6) | No target | Pending / D3 |
| `ChineseSteelSetCheckOption` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga65aeaf44307ec7a763e88718eb7eeaaf) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga65aeaf44307ec7a763e88718eb7eeaaf) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga65aeaf44307ec7a763e88718eb7eeaaf) | No target | Pending / D3 |
| `ChineseSteelGetResults` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga2b7360c7b4cc2f37c9a1de7a9422f8a5) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga65ed76eaa22e34d9986cc812c97710fe) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga65ed76eaa22e34d9986cc812c97710fe) | No target | Pending / D4 |
| `ChineseSteelGetCodeItemName` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga6384ab46038a19abdba0da63d56a0d12) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga6384ab46038a19abdba0da63d56a0d12) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga6384ab46038a19abdba0da63d56a0d12) | No target | Pending / D3 |
| `ChineseSteelGetMaxResult` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga4012017d72237f2033f29b6cf1e35743) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga50dbfaee1df1de79af8b9b102350fc5d) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga50dbfaee1df1de79af8b9b102350fc5d) | No target | Pending / D4 |
| `ChineseSteelGetResultParameters` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga24d6593a6e04f2a8798c91e1f7da1804) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga8c61c27bcb8325eb35fbc5e1eb461595) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga8c61c27bcb8325eb35fbc5e1eb461595) | No target | Pending / D4 |
| `ChineseSteelDisplayMemberReport` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga5f3346d843b70dd654712dc989c316f6) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga9b22538e5ba992a748ba4d7af245c9b3) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga9b22538e5ba992a748ba4d7af245c9b3) | No target | Pending / D4 |
| `ChineseSteelDisplayResultTable` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#gaa089f006ee5bda11366db2e40cb078c3) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gaa089f006ee5bda11366db2e40cb078c3) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gaa089f006ee5bda11366db2e40cb078c3) | No target | Pending / D4 |
| `ChineseSteelGetStoreyCount` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#gabab6bba0303627fc3f4f707a93b4c7f6) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gabab6bba0303627fc3f4f707a93b4c7f6) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gabab6bba0303627fc3f4f707a93b4c7f6) | No target | Pending / D3 |
| `ChineseSteelAddStorey` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#gaca405995a9f0981e3755e3f03f3b9796) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gaca405995a9f0981e3755e3f03f3b9796) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gaca405995a9f0981e3755e3f03f3b9796) | No target | Pending / D3 |
| `ChineseSteelDeleteStorey` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga490a1cf5fec619a86b1c04229ccc9b96) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga490a1cf5fec619a86b1c04229ccc9b96) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga490a1cf5fec619a86b1c04229ccc9b96) | No target | Pending / D3 |
| `ChineseSteelAssignStorey` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#gacc8521b0a206a9f41ec31f0120f82574) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gacc8521b0a206a9f41ec31f0120f82574) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gacc8521b0a206a9f41ec31f0120f82574) | No target | Pending / D3 |
| `ChineseSteelAssignDissipated` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#gac7b2874f7aa3bbb5ef2b4e6809b24930) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gac7b2874f7aa3bbb5ef2b4e6809b24930) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gac7b2874f7aa3bbb5ef2b4e6809b24930) | No target | Pending / D3 |
| `ChineseSteelGetFireProofMaterialCount` | `VARIANT` | Absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gaf80f0f1aa9881542cdca646282ee1828) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gaf80f0f1aa9881542cdca646282ee1828) | No target | Pending / D5 |
| `ChineseSteelGetFireProofMaterialName` | `VARIANT` | Absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga5c494793d911ff30700c617e4f1452f4) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga5c494793d911ff30700c617e4f1452f4) | No target | Pending / D5 |
| `ChineseSteelAddConcreteData` | `VARIANT` | Absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gac80cace58d2fdd6d0cca6b1d4eb997a3) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gac80cace58d2fdd6d0cca6b1d4eb997a3) | No target | Pending / D5 |
| `ChineseSteelAddNonIntumescentCoating` | `VARIANT` | Absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga1db283accdcc2aed8e7448aa19ad2537) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga1db283accdcc2aed8e7448aa19ad2537) | No target | Pending / D5 |
| `ChineseSteelAddIntumescentCoating` | `VARIANT` | Absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga1ae88a648e5a1307bf70ea2cf7533948) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga1ae88a648e5a1307bf70ea2cf7533948) | No target | Pending / D5 |
| `ChineseSteelDeleteFireProofMaterial` | `VARIANT` | Absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga75982b349fdb882ab187ca9ccdf2f09c) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga75982b349fdb882ab187ca9ccdf2f09c) | No target | Pending / D5 |
| `ChineseSteelGetCurveCount` | `VARIANT` | Absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga7d1d7c7374eeb6802eed60badf43ee8c) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga7d1d7c7374eeb6802eed60badf43ee8c) | No target | Pending / D5 |
| `ChineseSteelGetCurveName` | `VARIANT` | Absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gaff17ac3918f078c6d2233a12e99d35d3) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gaff17ac3918f078c6d2233a12e99d35d3) | No target | Pending / D5 |
| `ChineseSteelDeleteCurve` | `VARIANT` | Absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga1669cee47f7396cc61ce8ca1bb39eba6) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga1669cee47f7396cc61ce8ca1bb39eba6) | No target | Pending / D5 |
| `ChineseSteelAddHotSmokeCurveData` | `VARIANT` | Absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga2f6c560c157d426e516a60e0775033c4) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga2f6c560c157d426e516a60e0775033c4) | No target | Pending / D5 |
| `ChineseSteelAddTemperatureRiseCurveData` | `VARIANT` | Absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gac12e447eadc75710b58060b3cf1de279) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gac12e447eadc75710b58060b3cf1de279) | No target | Pending / D5 |
| `ChineseSteelAddThermalSteelPropertyCurveData` | `VARIANT` | Absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga6890e7076be246370638b04340bfd5ed) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga6890e7076be246370638b04340bfd5ed) | No target | Pending / D5 |

## 3. Per-function contracts and managed proposals

<a id="design-getmemberdesignparameters"></a>

### 1. GetMemberDesignParameters

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#ga873ef30c05c4d69dff3528c5ffdf8306) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#ga873ef30c05c4d69dff3528c5ffdf8306) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#ga873ef30c05c4d69dff3528c5ffdf8306)<br>**Current:** Pending; D2.

**Managed proposal:** `DesignQueryResult<MemberDesignParametersSnapshot> GetMemberDesignParameters(int designBriefId, int memberId); immutable name/value/unit/description/default rows copied from an internal owned holder.`

#### 2024 contract

~~~cpp
long OSDesignUI::GetMemberDesignParameters(long nBriefRef,long nMemberNo,LPDISPATCH pDesignParams)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `nBriefRef` | `long` | [in] | The Design Brief reference ID. |
| `nMemberNo` | `long` | [in] | Member number ID. |
| `pDispatch` | `LPDISPATCH` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `pDesignParams` | [out] | Design parameters: Name, Value, Unit, Description, Default . |

**Contract, including return/status semantics:** Returns the design parameters for specified member in specified design brief. Parameters [in] nBriefRef The Design Brief reference ID. [in] nMemberNo Member number ID. [out] pDesignParams Design parameters: Name, Value, Unit, Description, Default . Return values 0 OK. -1 General error. C++ Syntax VBA Syntax See also OSDesignUI::AssignDesignParameter

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Int32 GetMemberDesignParameters(Int32, Int32, OpenSTAADUI.OSMemberSteelDgnParams)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [GetMemberDesignParameters](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSDesignAdapter.cs#L163)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-createdesignbrief"></a>

### 2. CreateDesignBrief

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gabe19d807c0c64886f1a1919be3c0c3e3) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gabe19d807c0c64886f1a1919be3c0c3e3) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gabe19d807c0c64886f1a1919be3c0c3e3)<br>**Current:** Pending; D2.

**Managed proposal:** `DesignBriefResult CreateDesignBrief(StaadDesignCode designCode); status plus DesignBriefId; do not confuse successful ID with zero-success mutation codes.`

#### 2024 contract

~~~cpp
long OSDesignUI::CreateDesignBrief(Code nDesignCode)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `nDesignCode` | `Code` | [in] | Design Code Index. |

**Contract, including return/status semantics:** Creates a new design brief with specified design code. This should then be populated with the necessary design parameters (see the function OSDesignUI::AssignDesignParameter ) and completed with a design command (see function OSDesignUI::AssignDesignCommand ). Parameters [in] nDesignCode Design Code Index. nDesignCode Description Code Type 1001 AASHTO ASD Steel Design 1002 AISC ASD Steel Design 1003 AS 4100-1998 Steel Design 1004 BS 5950-1:2000 Steel Design 1005 BS 5400:Part 3:1982 Steel Design 1006 CAN/CSA-S16-01 Steel Design 1007 French CM66 1977 Steel Design 1008 DIN 18 800 Part 1 Steel Design 1009 IS 800 1984, ASD Steel Design 1010 Japan AIJ 2002 Steel Design 1011 AISC LRFD Steel Design 1012 Norway NS 3472 2001 Steel Design 1014 Norway NPD 1993 Steel Design 1016 API 2A-WSD Steel Design 1020 ASCE 10-97 Steel Design 1025 Russia SNiP 2.23-81* 1990 Steel Design 1027 Canada S136-94 Steel Design 1028 IS 801 1975 Steel Design 1029 IS 802 1995 Steel Design 1030 Mexico NTC 1987 Steel Design 1032 IS 800 2007, LSD Steel Design 1052 IS 800 2007, WSD Steel Design 1033 BS 5950-5:1998 Steel Design 1034 South Africa SANS 10162-1:2011 Steel Design 1044 AASHTO LRFD Steel Design 1045 ANSI/AISC 360-05 Steel Design 1046 ASME NF 3000 1989 Steel Design 1047 ASME NF 3000 1998 Steel Design 1048 ASME NF 3000 1974 Steel Design 1049 ASME NF 3000 1977 Steel Design 1050 Norway NORSOK N-004 Steel Design 1051 ASME NF 3000 2004 Steel Design 1053 ASME NF 3000 2001 Steel Design 1061 ANSI/AISC 360-10 Steel Design 1062 Canadian S16_09 Steel Design 1063 Russia SP 16.13330.2011 Steel Design 1064 South Africa SANS10162-1:1993 Steel Design 1065 Canadian S16_14 Steel Design 1066 NZS3404_1997 Steel Design 1067 ANSI/AISC 360-16 Steel Design 1068 Aisi_S100_2016 Steel Design 1069 Canadian S16_19 Steel Design 1102 AISC CASTELLATED Steel Design 1202 AISC N690 1994 Steel Design 1204 AISC N690 1984 Steel Design 1210 Japan AIJ 2005 Steel Design 1220 BS EN 1993-1-1:2005 Steel Design 1221 Russia SP 16.13330.2017 Steel Design 2001 ACI 318 2014 Concrete Design 2003 BS 8007 Concrete Design 2004 CAN/CSA A23.3-94 Concrete Design 2007 IS456 Concrete Design 2008 AIJ Concrete Design 2019 IS13920 2016 Concrete Design 2016 AS 3600 Concrete Design 2020 Mexican Concrete Design 2022 SABS0100 Concrete Design 2026 ACI 318 2005 Concrete Design 2025 ACI 318 2002 Concrete Design 2024 ACI 318 1999 Concrete Design 2027 ACI 318 2008 Concrete Design 2029 ACI 318 2011 Concrete Design 2030 IS13920 1993 Concrete Design 3001 AITC 1984 Timber Design 3002 AITC 1994 Timber Design 3003 EC5 Timber Design 3004 CSA086-01 Timber Design 4001 Aluminum US Aluminum Design Return values &lt;Val&gt; The reference ID of the design brief created. -1 General error. C++ Syntax VBA Syntax See also OSDesignUI::GetDesignBriefCode

**Value table 1** (source column groupings retained):

| nDesignCode | Description | Code Type |
| --- | --- | --- |
| 1001 | AASHTO ASD | Steel Design |
| 1002 | AISC ASD | Steel Design |
| 1003 | AS 4100-1998 | Steel Design |
| 1004 | BS 5950-1:2000 | Steel Design |
| 1005 | BS 5400:Part 3:1982 | Steel Design |
| 1006 | CAN/CSA-S16-01 | Steel Design |
| 1007 | French CM66 1977 | Steel Design |
| 1008 | DIN 18 800 Part 1 | Steel Design |
| 1009 | IS 800 1984, ASD | Steel Design |
| 1010 | Japan AIJ 2002 | Steel Design |
| 1011 | AISC LRFD | Steel Design |
| 1012 | Norway NS 3472 2001 | Steel Design |
| 1014 | Norway NPD 1993 | Steel Design |
| 1016 | API 2A-WSD | Steel Design |
| 1020 | ASCE 10-97 | Steel Design |
| 1025 | Russia SNiP 2.23-81* 1990 | Steel Design |
| 1027 | Canada S136-94 | Steel Design |
| 1028 | IS 801 1975 | Steel Design |
| 1029 | IS 802 1995 | Steel Design |
| 1030 | Mexico NTC 1987 | Steel Design |
| 1032 | IS 800 2007, LSD | Steel Design |
| 1052 | IS 800 2007, WSD | Steel Design |
| 1033 | BS 5950-5:1998 | Steel Design |
| 1034 | South Africa SANS 10162-1:2011 | Steel Design |
| 1044 | AASHTO LRFD | Steel Design |
| 1045 | ANSI/AISC 360-05 | Steel Design |
| 1046 | ASME NF 3000 1989 | Steel Design |
| 1047 | ASME NF 3000 1998 | Steel Design |
| 1048 | ASME NF 3000 1974 | Steel Design |
| 1049 | ASME NF 3000 1977 | Steel Design |
| 1050 | Norway NORSOK N-004 | Steel Design |
| 1051 | ASME NF 3000 2004 | Steel Design |
| 1053 | ASME NF 3000 2001 | Steel Design |
| 1061 | ANSI/AISC 360-10 | Steel Design |
| 1062 | Canadian S16_09 | Steel Design |
| 1063 | Russia SP 16.13330.2011 | Steel Design |
| 1064 | South Africa SANS10162-1:1993 | Steel Design |
| 1065 | Canadian S16_14 | Steel Design |
| 1066 | NZS3404_1997 | Steel Design |
| 1067 | ANSI/AISC 360-16 | Steel Design |
| 1068 | Aisi_S100_2016 | Steel Design |
| 1069 | Canadian S16_19 | Steel Design |
| 1102 | AISC CASTELLATED | Steel Design |
| 1202 | AISC N690 1994 | Steel Design |
| 1204 | AISC N690 1984 | Steel Design |
| 1210 | Japan AIJ 2005 | Steel Design |
| 1220 | BS EN 1993-1-1:2005 | Steel Design |
| 1221 | Russia SP 16.13330.2017 | Steel Design |
| 2001 | ACI 318 2014 | Concrete Design |
| 2003 | BS 8007 | Concrete Design |
| 2004 | CAN/CSA A23.3-94 | Concrete Design |
| 2007 | IS456 | Concrete Design |
| 2008 | AIJ | Concrete Design |
| 2019 | IS13920 2016 | Concrete Design |
| 2016 | AS 3600 | Concrete Design |
| 2020 | Mexican | Concrete Design |
| 2022 | SABS0100 | Concrete Design |
| 2026 | ACI 318 2005 | Concrete Design |
| 2025 | ACI 318 2002 | Concrete Design |
| 2024 | ACI 318 1999 | Concrete Design |
| 2027 | ACI 318 2008 | Concrete Design |
| 2029 | ACI 318 2011 | Concrete Design |
| 2030 | IS13920 1993 | Concrete Design |
| 3001 | AITC 1984 | Timber Design |
| 3002 | AITC 1994 | Timber Design |
| 3003 | EC5 | Timber Design |
| 3004 | CSA086-01 | Timber Design |
| 4001 | Aluminum US | Aluminum Design |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Int32 CreateDesignBrief(OpenSTAADUI.DesignCodes)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [CreateDesignBrief](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSDesignAdapter.cs#L38), [AssignDesignParameters](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSDesignAdapter.cs#L68)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-assigndesignparameter"></a>

### 3. AssignDesignParameter

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gad388f8ab10890423fa60c072dba15c1e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gad388f8ab10890423fa60c072dba15c1e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gad388f8ab10890423fa60c072dba15c1e)<br>**Current:** Pending; D2.

**Managed proposal:** `DesignMutationResult AssignDesignParameter; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2024 contract

~~~cpp
long OSDesignUI::AssignDesignParameter(long nBriefRef,LPCTSTR strParamName,LPCTSTR strParamValue,const VARIANT FAR&varMembers)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `nBriefRef` | `long` | [in] | The Design Brief reference ID(Type:Long). |
| `strParamName` | `LPCTSTR` | [in] | Design command name(Type:String). |
| `strParamValue` | `LPCTSTR` | [in] | Parameter for design command(Type:String). |
| `varMembers` | `const VARIANT FAR&` | [in] | Member number ID(s) VARIANT array(Type:Long Array). |

**Contract, including return/status semantics:** Assigns Design Parameters to specified Design Brief using Design Command. Parameters [in] nBriefRef The Design Brief reference ID(Type:Long). [in] strParamName Design command name(Type:String). [in] strParamValue Parameter for design command(Type:String). [in] varMembers Member number ID(s) VARIANT array(Type:Long Array). Return values 0 Assign Design Parameters Successful. -1 Assign Design Parameters General error. C++ Syntax VBA Syntax See also OSDesignUI::GetMemberDesignParameters

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Int32 AssignDesignParameter(Int32, System.String, System.String, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [AssignDesignParameter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSDesignAdapter.cs#L115)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-assigndesigncommand"></a>

### 4. AssignDesignCommand

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#ga3691808f3cb60303725af5d30d43ee40) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#ga3691808f3cb60303725af5d30d43ee40) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#ga3691808f3cb60303725af5d30d43ee40)<br>**Current:** Pending; D2.

**Managed proposal:** `DesignMutationResult AssignDesignCommand; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2024 contract

~~~cpp
long OSDesignUI::AssignDesignCommand(long nBriefRef,LPCTSTR strCommandName,LPCTSTR strCommandValue,const VARIANT FAR&varMembers)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `nBriefRef` | `long` | [in] | The Design Brief reference ID(Type:Long). |
| `strCommandName` | `LPCTSTR` | [in] | Design command name(Type:String). |
| `strCommandValue` | `LPCTSTR` | [in] | Parameter for design command(Type:String). |
| `varMembers` | `const VARIANT FAR&` | [in] | VARIANT array number ID of Member(s) to be assigned to(Type:Long Array). |

**Contract, including return/status semantics:** Design Command. Parameters [in] nBriefRef The Design Brief reference ID(Type:Long). [in] strCommandName Design command name(Type:String). [in] strCommandValue Parameter for design command(Type:String). [in] varMembers VARIANT array number ID of Member(s) to be assigned to(Type:Long Array). Return values 0 Assign Design Command Successful. -1 Assign Design Command Generate error. C++ Syntax VBA Syntax

#### 2025 contract

~~~cpp
long OSDesignUI::AssignDesignCommand(long nBriefRef,LPCTSTR strCommandName,LPCTSTR strCommandValue,const VARIANT FAR&varMembers)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `nBriefRef` | `long` | [in] | Design Brief reference ID. (Type: Long). |
| `strCommandName` | `LPCTSTR` | [in] | Design command string. Supported command names are included in below table. (Type: String) [value table below] |
| `strCommandValue` | `LPCTSTR` | [in] | only used when strCommandName is 'GROUP'. In that case, the string must contain the index of the selected option from the table below; otherwise, it must be an empty string.: (Type: String) [value table below] |
| `varMembers` | `const VARIANT FAR&` | [in] | VARIANT containing an array of member IDs to assign the command to (Type: Long Array). |

**Contract, including return/status semantics:** Creates and assigns a design command instruction for steel and aluminum design. Concrete Design commands are currently not supported. Parameters [in] nBriefRef Design Brief reference ID. (Type: Long). [in] strCommandName Design command string. Supported command names are included in below table. (Type: String) Design Commands "CHECK CODE" "FIXED GROUP" "GROUP" "MEMBER TAKE OFF" "SELECT" "SELECT OPTIMIZED" "SELECT WELD" "SELECT WELD TRUSS" "TAKE OFF" [in] strCommandValue only used when strCommandName is 'GROUP'. In that case, the string must contain the index of the selected option from the table below; otherwise, it must be an empty string.: (Type: String) Command Value Specification Parameter "0" None "1" Ax "2" Sy "3" Sz [in] varMembers VARIANT containing an array of member IDs to assign the command to (Type: Long Array). Note For aluminum designs only 'check code', 'select' and 'take off' apply and for timber design, only 'check code' applies. This function only supports the GROUP' option in which a single parameter is provided. A complimentary function, OSDesign::AssignGroupCommand has been provided that supports the options to create the command which can define both a property specification along with a SAME AS option. Return values 0 Command assigned successfully. -1 Error occurred while assigning the command. C++ Syntax - Example 1 C++ Syntax - Example 2 - Sample 'GROUP' Command VBA Syntax - Example 1 VBA Syntax - Example 2 - Sample 'GROUP' Command See also OSDesignUI::AssignDesignGroup

**Value table 1** (source column groupings retained):

| Design Commands |
| --- |
| "CHECK CODE" |
| "FIXED GROUP" |
| "GROUP" |
| "MEMBER TAKE OFF" |
| "SELECT" |
| "SELECT OPTIMIZED" |
| "SELECT WELD" |
| "SELECT WELD TRUSS" |
| "TAKE OFF" |

**Value table 2** (source column groupings retained):

| Command Value | Specification Parameter |
| --- | --- |
| "0" | None |
| "1" | Ax |
| "2" | Sy |
| "3" | Sz |

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Int32 AssignDesignCommand(Int32, System.String, System.String, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** [AssignDesignCommand](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSDesignAdapter.cs#L145)

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-assigndesigngroup"></a>

### 5. AssignDesignGroup

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gac158379b764ecc82c5953973815438ce) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gac158379b764ecc82c5953973815438ce) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gac158379b764ecc82c5953973815438ce)<br>**Current:** Pending; D2.

**Managed proposal:** `DesignMutationResult AssignDesignGroup; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2024 contract

~~~cpp
long OSDesignUI::AssignDesignGroup(long nBriefRef,LPCTSTR strCommandName,LPCTSTR strCommandValue,long sameAsMember,const VARIANT FAR&varMembers)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `nBriefRef` | `long` | [in] | Design Brief reference ID(Type:Long). |
| `strCommandName` | `LPCTSTR` | [in] | Name of the command: scSteelGroup (= 9987)(Type:String). |
| `strCommandValue` | `LPCTSTR` | [in] | Property Specification: "Ax", "Ay", "Az"(Type:String). |
| `sameAsMember` | `long` | [in] | Reference ID of the beam to represent members in this group for design(Type:Long). |
| `varMembers` | `const VARIANT FAR&` | [in] | Member number ID(s) VARIANT array(Type:Long Array). |

**Contract, including return/status semantics:** Assign physical member(s) to Design Group using Design Command. Parameters [in] nBriefRef Design Brief reference ID(Type:Long). [in] strCommandName Name of the command: scSteelGroup (= 9987)(Type:String). [in] strCommandValue Property Specification: "Ax", "Ay", "Az"(Type:String). [in] sameAsMember Reference ID of the beam to represent members in this group for design(Type:Long). [in] varMembers Member number ID(s) VARIANT array(Type:Long Array). Return values 0 OK. -1 General error. C++ Syntax VBA Syntax

#### 2025 contract

~~~cpp
long OSDesignUI::AssignDesignGroup(long nBriefRef,LPCTSTR strCommandName,LPCTSTR strCommandValue,long sameAsMember,const VARIANT FAR&varMembers)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `nBriefRef` | `long` | [in] | Reference ID of the parameter block to assign the command to. (Type: Long) |
| `strCommandName` | `LPCTSTR` | [in] | Only takes the string "GROUP". (Type: String) |
| `strCommandValue` | `LPCTSTR` | [in] | The section property used to pick the profile when using the Property Method. Valid inputs: "AX", "SY", "SZ" and empty string ("") if none. (Type: String) |
| `sameAsMember` | `long` | [in] | The ID of the member to obtain the profile from, to assign to the varMembers array when using the Selection Method. (Type: Long) |
| `varMembers` | `const VARIANT FAR&` | [in] | Array of member IDs to assign the identified profile to. (Type: Long Array) |

**Contract, including return/status semantics:** Creates and assigns a GROUP command, used with a design parameter block to rationalise the profiles resulting from a SELECT or SELECT OPTIMISE command. This can use two different methods. Property method - Choose the profile which has the maximum given property of the selected group. Selection method - Choose the profile from a specified member id. Parameters [in] nBriefRef Reference ID of the parameter block to assign the command to. (Type: Long) [in] strCommandName Only takes the string "GROUP". (Type: String) [in] strCommandValue The section property used to pick the profile when using the Property Method. Valid inputs: "AX", "SY", "SZ" and empty string ("") if none. (Type: String) [in] sameAsMember The ID of the member to obtain the profile from, to assign to the varMembers array when using the Selection Method. (Type: Long) [in] varMembers Array of member IDs to assign the identified profile to. (Type: Long Array) Note This provides a method to create the extended form of the GROUP command, the standard form of the command can be generated using the function AssignDesignCommand. Return values 0 The command was created successfully. -1 The function could not create the command C++ Syntax - Example 1 - Property Method C++ Syntax - Example 2 - Selection Method VBA Syntax - Example 1 - Property Method VBA Syntax - Example 2 - Selection Method See also OSDesignUI::AssignDesignCommand

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Int32 AssignDesignGroup(Int32, System.String, System.String, Int32, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-getdesignbriefcode"></a>

### 6. GetDesignBriefCode

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gaf5f1bd64481fc68cc681c132f4bff2cc) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gaf5f1bd64481fc68cc681c132f4bff2cc) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gaf5f1bd64481fc68cc681c132f4bff2cc)<br>**Current:** Pending; D2.

**Managed proposal:** `DesignQueryResult<StaadDesignCode> GetDesignBriefCode(int designBriefId); preserve unknown raw code as diagnostic data.`

#### 2024 contract

~~~cpp
long OSDesignUI::GetDesignBriefCode(long nBriefRef)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `nBriefRef` | `long` | [in] | Design Brief reference ID. |

**Contract, including return/status semantics:** Returns the Design Brief Code for specified Design brief. Parameters [in] nBriefRef Design Brief reference ID. Return values &lt;Val&gt; The design brief code in long integer. 0 STAAD Design Brief Not Found. C++ Syntax VBA Syntax See also OSDesignUI::CreateDesignBrief

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `OpenSTAADUI.DesignCodes GetDesignBriefCode(Int32)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelinitial"></a>

### 7. ChineseSteelInitial

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga278ae7ffbaf8d0d06f9a16da92d60686) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga278ae7ffbaf8d0d06f9a16da92d60686) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga278ae7ffbaf8d0d06f9a16da92d60686)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignMutationResult Initial; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2024 contract

~~~cpp
void OSDesignUI::ChineseSteelInitial()
~~~

No native parameters.

**Contract, including return/status semantics:** Initial Chinese Steel Design module. VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void ChineseSteelInitial()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelperformdesign"></a>

### 8. ChineseSteelPerformDesign

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#gaadad0c586459e114752afc40ca264b92) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gaadad0c586459e114752afc40ca264b92) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gaadad0c586459e114752afc40ca264b92)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignMutationResult PerformDesign; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2024 contract

~~~cpp
void OSDesignUI::ChineseSteelPerformDesign()
~~~

No native parameters.

**Contract, including return/status semantics:** Perform Chinese Steel Design. VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void ChineseSteelPerformDesign()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelgetsolutioncount"></a>

### 9. ChineseSteelGetSolutionCount

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga71998921f42bac574bab8f05227a29d7) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga71998921f42bac574bab8f05227a29d7) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga71998921f42bac574bab8f05227a29d7)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignQueryResult<int> GetSolutionCount; named solutionId/curveKind inputs where documented; distinguish valid empty count from failure.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetSolutionCount()
~~~

No native parameters.

**Contract, including return/status semantics:** get the solution count in Chinese Steel Design. Returns The total number of solution in Chinese Steel Design(Type: Long/Integer). VBA Syntax See also OSDesignUI::ChineseSteelGetSolution

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelGetSolutionCount()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelgetsolution"></a>

### 10. ChineseSteelGetSolution

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga3748d5f4026136903962eef8eeffa239) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga3748d5f4026136903962eef8eeffa239) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga3748d5f4026136903962eef8eeffa239)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignQueryResult<T> GetSolution; T is a named solution/settings/check-items record, string name or typed parameter value according to the outputs below; no public native holder.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetSolution(const VARIANT FAR&index,VARIANT FAR&solutionNo,VARIANT FAR&solutionName,VARIANT FAR&solutionType)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `index` | `const VARIANT FAR&` | [in] | Solution Index, start from 0(Type: Long). |
| `VarSolutionNo` | `VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `VarSolutionName` | `VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `VarSolutionType` | `VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `solutionNo` | [out] | Solution Id(Type: Long). |
| `solutionName` | [out] | Solution Name(Type: String). |
| `solutionType` | [out] | Solution Type(Type: Long). [value table below] |

**Contract, including return/status semantics:** get the solution information. Parameters [in] index Solution Index, start from 0(Type: Long). [out] solutionNo Solution Id(Type: Long). [out] solutionName Solution Name(Type: String). [out] solutionType Solution Type(Type: Long). Value Solution Type 0 First Order 1 P - Delta 2 Direct Analysis Return values true Success. false General error. VBA Syntax See also OSDesignUI::ChineseSteelGetSolutionCount

**Value table 1** (source column groupings retained):

| Value | Solution Type |
| --- | --- |
| 0 | First Order |
| 1 | P - Delta |
| 2 | Direct Analysis |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelGetSolution(System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteeladdsolution"></a>

### 11. ChineseSteelAddSolution

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#gad26a5295638e746f5ab1413112f77199) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gad26a5295638e746f5ab1413112f77199) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gad26a5295638e746f5ab1413112f77199)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignMutationResult AddSolution; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelAddSolution(const VARIANT FAR&varSolutionName,const VARIANT FAR&varSolutionType)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varSolutionName` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `varSolutionType` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `solutionName` | [in] | solution Name(Type: String). |
| `solutionType` | [in] | solution Type(Type: long/Integer). [value table below] |

**Contract, including return/status semantics:** add a new solution in Chinese Steel Design. Parameters [in] solutionName solution Name(Type: String). [in] solutionType solution Type(Type: long/Integer). Value Solution Type 0 First Order 1 P - Delta Return values -1 Add solution with error. other,Solution No. VBA Syntax See also OSDesignUI::ChineseSteelGetSolution OSDesignUI::ChineseSteelDeletedSolution

**Value table 1** (source column groupings retained):

| Value | Solution Type |
| --- | --- |
| 0 | First Order |
| 1 | P - Delta |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelAddSolution(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteeldeletedsolution"></a>

### 12. ChineseSteelDeletedSolution

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga89a13b0b6d68cb88ad6814e792d86419) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga89a13b0b6d68cb88ad6814e792d86419) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga89a13b0b6d68cb88ad6814e792d86419)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignMutationResult DeletedSolution; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelDeletedSolution(const VARIANT FAR&varSolutionNo)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varSolutionNo` | `const VARIANT FAR&` | [in] | solution Id(Type: long/Integer). |

**Contract, including return/status semantics:** Delete a solution in Chinese Steel Design. Parameters [in] varSolutionNo solution Id(Type: long/Integer). Return values true Success. false General error. VBA Syntax See also OSDesignUI::ChineseSteelGetSolution OSDesignUI::ChineseSteelAddSolution

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelDeletedSolution(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelgetdesignparameterscount"></a>

### 13. ChineseSteelGetDesignParametersCount

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga23be0a4a9d7d3174b1e4c5956face361) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga23be0a4a9d7d3174b1e4c5956face361) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga23be0a4a9d7d3174b1e4c5956face361)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignQueryResult<int> GetDesignParametersCount; named solutionId/curveKind inputs where documented; distinguish valid empty count from failure.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetDesignParametersCount(const VARIANT FAR&varSolutionNo)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varSolutionNo` | `const VARIANT FAR&` | [in] | Solution Id(Type: Long/Integer). |

**Contract, including return/status semantics:** get the parameter count for a specified solution in Chinese Steel Design. Parameters [in] varSolutionNo Solution Id(Type: Long/Integer). Returns The total number of parameters in the solution(Type: Long/Integer). VBA Syntax See also OSDesignUI::ChineseSteelGetSolution

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelGetDesignParametersCount(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteeladddesignparameter"></a>

### 14. ChineseSteelAddDesignParameter

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#gae82d98cea3139965caa9e2164d029c59) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gae82d98cea3139965caa9e2164d029c59) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gae82d98cea3139965caa9e2164d029c59)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignMutationResult AddDesignParameter; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelAddDesignParameter(const VARIANT FAR&varSolutionNo)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varSolutionNo` | `const VARIANT FAR&` | [in] | Solution Id(Type: Long/Integer). |

**Contract, including return/status semantics:** add a design parameter into the solution in Chinese Steel Design. Parameters [in] varSolutionNo Solution Id(Type: Long/Integer). Returns the new parameter index in the solution(Type: Long/Integer). VBA Syntax See also OSDesignUI::ChineseSteelGetSolution

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelAddDesignParameter(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteeldeleteddesignparameter"></a>

### 15. ChineseSteelDeletedDesignParameter

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga9559879e869cdbf43ccaf9bf60adb979) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga9559879e869cdbf43ccaf9bf60adb979) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga9559879e869cdbf43ccaf9bf60adb979)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignMutationResult DeletedDesignParameter; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelDeletedDesignParameter(const VARIANT FAR&varSolutionNo,const VARIANT FAR&varParamIndex)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varSolutionNo` | `const VARIANT FAR&` | [in] | Solution Id(Type: Long/Integer). |
| `varParamIndex` | `const VARIANT FAR&` | [in] | Design Parameter Index in the solution, Start from 0.(Type: Long/Integer). |

**Contract, including return/status semantics:** Deleted a design parameter from the solution in Chinese Steel Design. Parameters [in] varSolutionNo Solution Id(Type: Long/Integer). [in] varParamIndex Design Parameter Index in the solution, Start from 0.(Type: Long/Integer). Return values true Success. false General error. VBA Syntax See also OSDesignUI::ChineseSteelGetSolution

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelDeletedDesignParameter(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelgetdesignparameter"></a>

### 16. ChineseSteelGetDesignParameter

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#gaa3de8522adca8442deecbd5acc6130a7) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gaa3de8522adca8442deecbd5acc6130a7) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gaa3de8522adca8442deecbd5acc6130a7)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignQueryResult<T> GetDesignParameter; T is a named solution/settings/check-items record, string name or typed parameter value according to the outputs below; no public native holder.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetDesignParameter(const VARIANT FAR&varSolutionNo,const VARIANT FAR&varParamIndex,const VARIANT FAR&varParamType,VARIANT FAR&varParamValue)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varSolutionNo` | `const VARIANT FAR&` | [in] | Solution Id(Type: Long/Integer). |
| `varParamIndex` | `const VARIANT FAR&` | [in] | Design Parameter Index in the solution, Start from 0.(Type: Long/Integer). |
| `varParamType` | `const VARIANT FAR&` | [in] | Parameter Item Type(Type: Long/Integer). [value table below] |
| `varParamValue` | `VARIANT FAR&` | [out] | Parameter Item value(Type: String). |

**Contract, including return/status semantics:** Get a design parameter value from the solution in Chinese Steel Design. Parameters [in] varSolutionNo Solution Id(Type: Long/Integer). [in] varParamIndex Design Parameter Index in the solution, Start from 0.(Type: Long/Integer). [in] varParamType Parameter Item Type(Type: Long/Integer). Value Parameter Item Type Type Description In Page 0 Parameter Name String General 1 Steel No String "Auto","Q235","Q345","Q355","Q390","Q420","Q460" could be set, or other user defined material. General 2 Member Type Integer 0 for auto, 1 for Bending Member, 2 for Truss Member, 3 for Column Member, 4 for Defined Member. General 3 Judgement Principle for Member Type Integer Only Member Type is 0, This value is worked.0 for Geometry, 1 for Stress Feature. General 4 Cantilever Type Integer 0 for None-Cantilever, 1 for Cantilever in Both, 2 for Cantilever in Z Axis, 3 for Cantilever in Y Axis. General 5 Bending with Axial Force Integer 0 for Warning, 1 for Ignore, 2 for Check. General 6 Use Axis forces member Post-Buckling Boolean General 7 Truss Secondary Moment Boolean General 8 Section Slenderness Grade Integer 1 for S1, 2 for S2, 3 for S3, 4 for S4, 5 for S5 General 9 Optimize Design Boolean General 10 Fatigue Check Request Boolean General 11 Safety Ratio Double General 12 Failure Ratio Double General 13 Force Loads List String General 14 Displacement Loads List String General 15 Use Seismic Adjusting Factor Boolean Seismic Design 16 Strength Seismic Adjusting Factor Double 0 for auto Seismic Design 17 Stability Seismic Adjusting Factor Double 0 for auto Seismic Design 18 Seismic Measure Grade Integer -1 for Auto, 0 for None, 1 for Level1, 2 for Level2, 3 for Level3, 4 for Level4. Seismic Design 19 Reduction Factor of Bearing Capacity Double Seismic Design 20 Allowable Compression Slenderness Double 0 for auto Allowable Slenderness 21 Allowable Tension Slenderness Double 0 for auto Allowable Slenderness 22 Seismic Column, GB50011-2010, 8_3_1 Boolean Allowable Slenderness 23 Seismic Brace, GB50011-2010,8.4.1 Boolean Allowable Slenderness 24 Seismic Single-storey Plant, GB50011-2010,9.2.13 Boolean Allowable Slenderness 25 Seismic Multi-storey Plant, GB50011-2010,H.2.8 Boolean Allowable Slenderness 26 Plastic Development Factor in Z Axis Double 0 for auto Plastic Development Factor 27 Plastic Development Factor in Y Axis Double 0 for auto Plastic Development Factor 28 Plastic Development Factor in Sharp side Double 0 for auto Plastic Development Factor 29 Equivalent Moment Factor in-Plane of Z Axis Double 0 for auto Equivalent Moment Factor 30 Equivalent Moment Factor in-Plane of Y Axis Double 0 for auto Equivalent Moment Factor 31 Equivalent Moment Factor out-Plane of Z Axis Double 0 for auto Equivalent Moment Factor 32 Equivalent Moment Factor out-Plane of Y Axis Double 0 for auto Equivalent Moment Factor 33 Lateral Load Along Local z Boolean Equivalent Moment Factor 34 Lateral Load Along Local y Boolean Equivalent Moment Factor 35 Span/Deflection (DFF) Double Deflection Parameters 36 Start Joint (DJ1) Integer -1 for Member Start Joint, 0 Auto Deflection Parameters 37 End Joint (DJ2) Integer -1 for Member End Joint, 0 Auto Deflection Parameters 38 Check Horizontal Section Displacement Boolean Deflection Parameters 39 Overall Stability Factor of Beam in Z Axis Double 0 for auto Stability Factors 40 Overall Stability Factor of Beam in Y Axis Double 0 for auto Stability Factors 41 Stress Feature Integer 1 for Type1, 2 for Type2, ... 10 for Type10. If Cantilever type is in Z-Axis or Both Axis, Only 1,2,3 support. Stability Factors 42 Consider C.0.5 Boolean Stability Factors 43 Stability Factor of Axial Compression in Z Axis Double 0 for auto Stability Factors 44 Stability Factor of Axial Compression in Y Axis Double 0 for auto Stability Factors 45 Axial Compression Section Type in Z Axis Integer 0 for auto, 1 for Class a, 2 for Class b, 3 for Class c, 4 for Class d Stability Factors 46 Axial Compression Section Type in Y Axis Integer 0 for auto, 1 for Class a, 2 for Class b, 3 for Class c, 4 for Class d Stability Factors 47 Unbraced Length in Z Axis Double 0 for auto Effective Length 48 Unbraced Length in Y Axis Double 0 for auto Effective Length 49 Effective Length Factor in Z Axis Double 0 for auto Effective Length 50 Effective Length Factor in Y Axis Double 0 for auto Effective Length 51 Brace Type Integer 0 for unbraced member, 1 for braced in Both Axes, 2 for Only braced in y Axis, 3 for Only braced in z Axis Effective Length 52 Gyration Radius Calculation Of Single Angle Integer 0 for Strong Weak Axis Calculation (U-V Axis), 1 for Parallel Leg Calculation (Y-Z Axis) Effective Length 53 Flange Slenderness Limit (b/t) Double 0 for auto Detailing Checks 54 Web Slenderness Limit (h0/tw) Double 0 for auto Detailing Checks 55 Include check for web of H profiles in a Truss as per GB50017-2017, Table 8.5.2 Boolean Detailing Checks 56 Net Area Factor (An/A) double Section Factor 57 Net Resistance Moment Factor (Wnz/Wz) double Section Factor 58 Net Resistance Moment Factor (Wny/Wy) double Section Factor 59 Effective Section Factor Of Axis Force (nA) double Section Factor 60 Reduce Strength Of Angle Section (Ref 7.6.1) boolean Section Factor 61 Unequal Angle Connection Type Integer 0 for Short Leg, 1 for Long Leg. Section Factor 62 Strength Reduction Factor double 0 for auto Section Factor 63 Stability Reduction Factor double 0 for auto Section Factor 64 Performance_StructureType double Performance Design 65 Performance_PerformanceGrade double Performance Design 66 Performance_PrecautionaryCategory double Performance Design 67 Performance_DuctilityGrade double Performance Design 68 Performance_PerformanceFactor double Performance Design 69 Performance_AmplificationFactor double Performance Design 70 Performance_Yita_y double Performance Design 71 Performance_DissipatedLength double Performance Design 72 ThinWall_SteelNo String "Q235","Q345", Other value regard as "auto" GB50018 Only 73 ThinWall_CheckCompressedFlakiness Boolean 0 for unchecked, 1 for checked GB50018 Only 74 ThinWall_CheckLipFlakiness Boolean 0 for unchecked, 1 for checked GB50018 Only 75 ThinWall_BearingStatus Integer 0 for Auto, 1 for HingedWarpFree, 2 for BuildingInWarpRestrained, 3 for HingedWarpRestrained GB50018 Only 76 ThinWall_HavingBattenPlate Boolean 0 for unchecked, 1 for checked GB50018 Only 77 ThinWall_BattenPlateSpace double GB50018 Only 78 ThinWall_UnstiffenedFlakinessLimit double 0 for auto GB50018 Only 79 ThinWall_PartStiffenedFlakinessLimit double 0 for auto GB50018 Only 80 ThinWall_StiffenedFlakinessLimit double 0 for auto GB50018 Only 81 ThinWall_LipFlakinessLimit double 0 for auto GB50018 Only 82 ThinWall_BeamBearing Integer 1 for uniformForce, 2 for concentratedForce, 3 for endMoment3, 4 for endMoment4, 5 for endMoment5,6 for endMoment6,7 for endMoment7 GB50018 Only 83 ThinWall_LateralSupport Integer 1 for NoSupport, 2 for OneSupport, 3 for TwoOrMoreSupport GB50018 Only 84 ThinWall_IsConsiderColdFormingEffect Boolean 0 for unchecked, 1 for checked GB50018 Only 85 ThinWall_IsHighFrequencyWelding Boolean 0 for unchecked, 1 for checked GB50018 Only 86 ThinWall_ForceToFlexuralCenter_Z double can be changed in gsp file GB50018 Only 87 ThinWall_ForceToFlexuralCenter_Y double can be changed in gsp file GB50018 Only 88 ThinWall_ReduceStrength double 0 for auto GB50018 Only 89 ThinWall_ReduceStability double 0 for auto GB50018 Only 90 Beam Bending Strength Boolean Beam/Defined Member Optional Checks 91 Beam Shear Strength Boolean Beam/Column/Defined Member Optional Checks 92 Beam Equivalent Stress Boolean Beam/Defined Member Optional Checks 93 Beam Overall Stability Boolean Beam/Defined Member Optional Checks 94 Beam Web Slenderness Boolean Beam/Defined Member Optional Checks 95 Beam Flange Slenderness Boolean Beam/Defined Member Optional Checks 96 Truss Strength Boolean Truss/Defined Member Optional Checks 97 Truss Stability Boolean Truss/Defined Member Optional Checks 98 Shear Strength Boolean Truss/Defined Member Optional Checks 99 Column Strength Boolean Column/Defined Member Optional Checks 100 Column Stability in-plane Boolean Column/Defined Member Optional Checks 101 Column Stability out-plane Boolean Column/Defined Member Optional Checks 102 Compression Slenderness Boolean Truss/Column/Defined Member Optional Checks 103 Tension Slenderness Boolean Truss/Column/Defined Member Optional Checks 104 Compression Flange Slenderness Boolean Truss/Column/Defined Member Optional Checks 105 Compression Web Slenderness Boolean Truss/Column/Defined Member Optional Checks 106 Beam Deflection Boolean Beam/Defined Member Optional Checks [out] varParamValue Parameter Item value(Type: String). Return values true Success. false General error. VBA Syntax See also OSDesignUI::ChineseSteelGetSolution OSDesignUI::ChineseSteelSetDesignParameter OSDesignUI::ChineseSteelAddDesignParameter OSDesignUI::ChineseSteelDeletedDesignParameter

**Value table 1** (source column groupings retained):

| Value | Parameter Item Type | Type | Description | In Page |
| --- | --- | --- | --- | --- |
| 0 | Parameter Name | String |  | General |
| 1 | Steel No | String | "Auto","Q235","Q345","Q355","Q390","Q420","Q460" could be set, or other user defined material. | General |
| 2 | Member Type | Integer | 0 for auto, 1 for Bending Member, 2 for Truss Member, 3 for Column Member, 4 for Defined Member. | General |
| 3 | Judgement Principle for Member Type | Integer | Only Member Type is 0, This value is worked.0 for Geometry, 1 for Stress Feature. | General |
| 4 | Cantilever Type | Integer | 0 for None-Cantilever, 1 for Cantilever in Both, 2 for Cantilever in Z Axis, 3 for Cantilever in Y Axis. | General |
| 5 | Bending with Axial Force | Integer | 0 for Warning, 1 for Ignore, 2 for Check. | General |
| 6 | Use Axis forces member Post-Buckling | Boolean |  | General |
| 7 | Truss Secondary Moment | Boolean |  | General |
| 8 | Section Slenderness Grade | Integer | 1 for S1, 2 for S2, 3 for S3, 4 for S4, 5 for S5 | General |
| 9 | Optimize Design | Boolean |  | General |
| 10 | Fatigue Check Request | Boolean |  | General |
| 11 | Safety Ratio | Double |  | General |
| 12 | Failure Ratio | Double |  | General |
| 13 | Force Loads List | String |  | General |
| 14 | Displacement Loads List | String |  | General |
| 15 | Use Seismic Adjusting Factor | Boolean |  | Seismic Design |
| 16 | Strength Seismic Adjusting Factor | Double | 0 for auto | Seismic Design |
| 17 | Stability Seismic Adjusting Factor | Double | 0 for auto | Seismic Design |
| 18 | Seismic Measure Grade | Integer | -1 for Auto, 0 for None, 1 for Level1, 2 for Level2, 3 for Level3, 4 for Level4. | Seismic Design |
| 19 | Reduction Factor of Bearing Capacity | Double |  | Seismic Design |
| 20 | Allowable Compression Slenderness | Double | 0 for auto | Allowable Slenderness |
| 21 | Allowable Tension Slenderness | Double | 0 for auto | Allowable Slenderness |
| 22 | Seismic Column, GB50011-2010, 8_3_1 | Boolean |  | Allowable Slenderness |
| 23 | Seismic Brace, GB50011-2010,8.4.1 | Boolean |  | Allowable Slenderness |
| 24 | Seismic Single-storey Plant, GB50011-2010,9.2.13 | Boolean |  | Allowable Slenderness |
| 25 | Seismic Multi-storey Plant, GB50011-2010,H.2.8 | Boolean |  | Allowable Slenderness |
| 26 | Plastic Development Factor in Z Axis | Double | 0 for auto | Plastic Development Factor |
| 27 | Plastic Development Factor in Y Axis | Double | 0 for auto | Plastic Development Factor |
| 28 | Plastic Development Factor in Sharp side | Double | 0 for auto | Plastic Development Factor |
| 29 | Equivalent Moment Factor in-Plane of Z Axis | Double | 0 for auto | Equivalent Moment Factor |
| 30 | Equivalent Moment Factor in-Plane of Y Axis | Double | 0 for auto | Equivalent Moment Factor |
| 31 | Equivalent Moment Factor out-Plane of Z Axis | Double | 0 for auto | Equivalent Moment Factor |
| 32 | Equivalent Moment Factor out-Plane of Y Axis | Double | 0 for auto | Equivalent Moment Factor |
| 33 | Lateral Load Along Local z | Boolean |  | Equivalent Moment Factor |
| 34 | Lateral Load Along Local y | Boolean |  | Equivalent Moment Factor |
| 35 | Span/Deflection (DFF) | Double |  | Deflection Parameters |
| 36 | Start Joint (DJ1) | Integer | -1 for Member Start Joint, 0 Auto | Deflection Parameters |
| 37 | End Joint (DJ2) | Integer | -1 for Member End Joint, 0 Auto | Deflection Parameters |
| 38 | Check Horizontal Section Displacement | Boolean |  | Deflection Parameters |
| 39 | Overall Stability Factor of Beam in Z Axis | Double | 0 for auto | Stability Factors |
| 40 | Overall Stability Factor of Beam in Y Axis | Double | 0 for auto | Stability Factors |
| 41 | Stress Feature | Integer | 1 for Type1, 2 for Type2, ... 10 for Type10. If Cantilever type is in Z-Axis or Both Axis, Only 1,2,3 support. | Stability Factors |
| 42 | Consider C.0.5 | Boolean |  | Stability Factors |
| 43 | Stability Factor of Axial Compression in Z Axis | Double | 0 for auto | Stability Factors |
| 44 | Stability Factor of Axial Compression in Y Axis | Double | 0 for auto | Stability Factors |
| 45 | Axial Compression Section Type in Z Axis | Integer | 0 for auto, 1 for Class a, 2 for Class b, 3 for Class c, 4 for Class d | Stability Factors |
| 46 | Axial Compression Section Type in Y Axis | Integer | 0 for auto, 1 for Class a, 2 for Class b, 3 for Class c, 4 for Class d | Stability Factors |
| 47 | Unbraced Length in Z Axis | Double | 0 for auto | Effective Length |
| 48 | Unbraced Length in Y Axis | Double | 0 for auto | Effective Length |
| 49 | Effective Length Factor in Z Axis | Double | 0 for auto | Effective Length |
| 50 | Effective Length Factor in Y Axis | Double | 0 for auto | Effective Length |
| 51 | Brace Type | Integer | 0 for unbraced member, 1 for braced in Both Axes, 2 for Only braced in y Axis, 3 for Only braced in z Axis | Effective Length |
| 52 | Gyration Radius Calculation Of Single Angle | Integer | 0 for Strong Weak Axis Calculation (U-V Axis), 1 for Parallel Leg Calculation (Y-Z Axis) | Effective Length |
| 53 | Flange Slenderness Limit (b/t) | Double | 0 for auto | Detailing Checks |
| 54 | Web Slenderness Limit (h0/tw) | Double | 0 for auto | Detailing Checks |
| 55 | Include check for web of H profiles in a Truss as per GB50017-2017, Table 8.5.2 | Boolean |  | Detailing Checks |
| 56 | Net Area Factor (An/A) | double |  | Section Factor |
| 57 | Net Resistance Moment Factor (Wnz/Wz) | double |  | Section Factor |
| 58 | Net Resistance Moment Factor (Wny/Wy) | double |  | Section Factor |
| 59 | Effective Section Factor Of Axis Force (nA) | double |  | Section Factor |
| 60 | Reduce Strength Of Angle Section (Ref 7.6.1) | boolean |  | Section Factor |
| 61 | Unequal Angle Connection Type | Integer | 0 for Short Leg, 1 for Long Leg. | Section Factor |
| 62 | Strength Reduction Factor | double | 0 for auto | Section Factor |
| 63 | Stability Reduction Factor | double | 0 for auto | Section Factor |
| 64 | Performance_StructureType | double |  | Performance Design |
| 65 | Performance_PerformanceGrade | double |  | Performance Design |
| 66 | Performance_PrecautionaryCategory | double |  | Performance Design |
| 67 | Performance_DuctilityGrade | double |  | Performance Design |
| 68 | Performance_PerformanceFactor | double |  | Performance Design |
| 69 | Performance_AmplificationFactor | double |  | Performance Design |
| 70 | Performance_Yita_y | double |  | Performance Design |
| 71 | Performance_DissipatedLength | double |  | Performance Design |
| 72 | ThinWall_SteelNo | String | "Q235","Q345", Other value regard as "auto" | GB50018 Only |
| 73 | ThinWall_CheckCompressedFlakiness | Boolean | 0 for unchecked, 1 for checked | GB50018 Only |
| 74 | ThinWall_CheckLipFlakiness | Boolean | 0 for unchecked, 1 for checked | GB50018 Only |
| 75 | ThinWall_BearingStatus | Integer | 0 for Auto, 1 for HingedWarpFree, 2 for BuildingInWarpRestrained, 3 for HingedWarpRestrained | GB50018 Only |
| 76 | ThinWall_HavingBattenPlate | Boolean | 0 for unchecked, 1 for checked | GB50018 Only |
| 77 | ThinWall_BattenPlateSpace | double |  | GB50018 Only |
| 78 | ThinWall_UnstiffenedFlakinessLimit | double | 0 for auto | GB50018 Only |
| 79 | ThinWall_PartStiffenedFlakinessLimit | double | 0 for auto | GB50018 Only |
| 80 | ThinWall_StiffenedFlakinessLimit | double | 0 for auto | GB50018 Only |
| 81 | ThinWall_LipFlakinessLimit | double | 0 for auto | GB50018 Only |
| 82 | ThinWall_BeamBearing | Integer | 1 for uniformForce, 2 for concentratedForce, 3 for endMoment3, 4 for endMoment4, 5 for endMoment5,6 for endMoment6,7 for endMoment7 | GB50018 Only |
| 83 | ThinWall_LateralSupport | Integer | 1 for NoSupport, 2 for OneSupport, 3 for TwoOrMoreSupport | GB50018 Only |
| 84 | ThinWall_IsConsiderColdFormingEffect | Boolean | 0 for unchecked, 1 for checked | GB50018 Only |
| 85 | ThinWall_IsHighFrequencyWelding | Boolean | 0 for unchecked, 1 for checked | GB50018 Only |
| 86 | ThinWall_ForceToFlexuralCenter_Z | double | can be changed in gsp file | GB50018 Only |
| 87 | ThinWall_ForceToFlexuralCenter_Y | double | can be changed in gsp file | GB50018 Only |
| 88 | ThinWall_ReduceStrength | double | 0 for auto | GB50018 Only |
| 89 | ThinWall_ReduceStability | double | 0 for auto | GB50018 Only |
| 90 | Beam Bending Strength | Boolean | Beam/Defined Member | Optional Checks |
| 91 | Beam Shear Strength | Boolean | Beam/Column/Defined Member | Optional Checks |
| 92 | Beam Equivalent Stress | Boolean | Beam/Defined Member | Optional Checks |
| 93 | Beam Overall Stability | Boolean | Beam/Defined Member | Optional Checks |
| 94 | Beam Web Slenderness | Boolean | Beam/Defined Member | Optional Checks |
| 95 | Beam Flange Slenderness | Boolean | Beam/Defined Member | Optional Checks |
| 96 | Truss Strength | Boolean | Truss/Defined Member | Optional Checks |
| 97 | Truss Stability | Boolean | Truss/Defined Member | Optional Checks |
| 98 | Shear Strength | Boolean | Truss/Defined Member | Optional Checks |
| 99 | Column Strength | Boolean | Column/Defined Member | Optional Checks |
| 100 | Column Stability in-plane | Boolean | Column/Defined Member | Optional Checks |
| 101 | Column Stability out-plane | Boolean | Column/Defined Member | Optional Checks |
| 102 | Compression Slenderness | Boolean | Truss/Column/Defined Member | Optional Checks |
| 103 | Tension Slenderness | Boolean | Truss/Column/Defined Member | Optional Checks |
| 104 | Compression Flange Slenderness | Boolean | Truss/Column/Defined Member | Optional Checks |
| 105 | Compression Web Slenderness | Boolean | Truss/Column/Defined Member | Optional Checks |
| 106 | Beam Deflection | Boolean | Beam/Defined Member | Optional Checks |

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetDesignParameter(const VARIANT FAR&varSolutionNo,const VARIANT FAR&varParamIndex,const VARIANT FAR&varParamType,VARIANT FAR&varParamValue)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varSolutionNo` | `const VARIANT FAR&` | [in] | Solution Id(Type: Long/Integer). |
| `varParamIndex` | `const VARIANT FAR&` | [in] | Design Parameter Index in the solution, Start from 0.(Type: Long/Integer). |
| `varParamType` | `const VARIANT FAR&` | [in] | Parameter Item Type(Type: Long/Integer). [value table below] |
| `varParamValue` | `VARIANT FAR&` | [out] | Parameter Item value(Type: String). |

**Contract, including return/status semantics:** Get a design parameter value from the solution in Chinese Steel Design. Parameters [in] varSolutionNo Solution Id(Type: Long/Integer). [in] varParamIndex Design Parameter Index in the solution, Start from 0.(Type: Long/Integer). [in] varParamType Parameter Item Type(Type: Long/Integer). Value Parameter Item Type Type Description In Page 0 Code Type String 1 for GB50017, 2 for GB50018, 3 for GB51249 General 1 Parameter Name String General 2 Steel No String "Q235","Q345","Q355","Q390","Q420","Q460" could be set, or other user defined material. General 3 Member Type Integer 0 for auto, 1 for Bending Member, 2 for Truss Member, 3 for Column Member, 4 for Defined Member. General 4 Judgement Principle for Member Type Integer Only Member Type is 0, This value is worked.0 for Geometry, 1 for Stress Feature. General 5 Cantilever Type Integer 0 for None-Cantilever, 1 for Cantilever in Both, 2 for Cantilever in Z Axis, 3 for Cantilever in Y Axis. General 6 Bending with Axial Force Integer 0 for Warning, 1 for Ignore, 2 for Check. General 7 Use Axis forces member Post-Buckling Boolean General 8 Truss Secondary Moment Boolean General 9 Section Slenderness Grade Integer 1 for S1, 2 for S2, 3 for S3, 4 for S4, 5 for S5 General 10 Optimize Design Boolean General 11 Fatigue Check Request Boolean General 12 Safety Ratio Double General 13 Failure Ratio Double General 14 Force Loads List String General 15 Displacement Loads List String General 16 Use Seismic Adjusting Factor Boolean Seismic Design 17 Strength Seismic Adjusting Factor Double 0 for auto Seismic Design 18 Stability Seismic Adjusting Factor Double 0 for auto Seismic Design 19 Seismic Measure Grade Integer -1 for Auto, 0 for None, 1 for Level1, 2 for Level2, 3 for Level3, 4 for Level4. Seismic Design 20 Reduction Factor of Bearing Capacity Double Seismic Design 21 Allowable Compression Slenderness Double 0 for auto Allowable Slenderness 22 Allowable Tension Slenderness Double 0 for auto Allowable Slenderness 23 Seismic Column, GB50011-2010, 8_3_1 Boolean Allowable Slenderness 24 Seismic Brace, GB50011-2010,8.4.1 Boolean Allowable Slenderness 25 Seismic Single-storey Plant, GB50011-2010,9.2.13 Boolean Allowable Slenderness 26 Seismic Multi-storey Plant, GB50011-2010,H.2.8 Boolean Allowable Slenderness 27 Plastic Development Factor in Z Axis Double 0 for auto Plastic Development Factor 28 Plastic Development Factor in Y Axis Double 0 for auto Plastic Development Factor 29 Plastic Development Factor in Sharp side Double 0 for auto Plastic Development Factor 30 Equivalent Moment Factor in-Plane of Z Axis Double 0 for auto Equivalent Moment Factor 31 Equivalent Moment Factor in-Plane of Y Axis Double 0 for auto Equivalent Moment Factor 32 Equivalent Moment Factor out-Plane of Z Axis Double 0 for auto Equivalent Moment Factor 33 Equivalent Moment Factor out-Plane of Y Axis Double 0 for auto Equivalent Moment Factor 34 Lateral Load Along Local z Boolean Equivalent Moment Factor 35 Lateral Load Along Local y Boolean Equivalent Moment Factor 36 Span/Deflection (DFF) Double Deflection Parameters 37 Start Joint (DJ1) Integer -1 for Member Start Joint, 0 Auto Deflection Parameters 38 End Joint (DJ2) Integer -1 for Member End Joint, 0 Auto Deflection Parameters 39 Check Horizontal Section Displacement Boolean Deflection Parameters 40 Overall Stability Factor of Beam in Z Axis Double 0 for auto Stability Factors 41 Overall Stability Factor of Beam in Y Axis Double 0 for auto Stability Factors 42 Stress Feature Integer 1 for Type1, 2 for Type2, ... 10 for Type10. If Cantilever type is in Z-Axis or Both Axis, Only 1,2,3 support. Stability Factors 43 Consider C.0.5 Boolean Stability Factors 44 Stability Factor of Axial Compression in Z Axis Double 0 for auto Stability Factors 45 Stability Factor of Axial Compression in Y Axis Double 0 for auto Stability Factors 46 Axial Compression Section Type in Z Axis Integer 0 for auto, 1 for Class a, 2 for Class b, 3 for Class c, 4 for Class d Stability Factors 47 Axial Compression Section Type in Y Axis Integer 0 for auto, 1 for Class a, 2 for Class b, 3 for Class c, 4 for Class d Stability Factors 48 Unbraced Length in Z Axis Double 0 for auto Effective Length 49 Unbraced Length in Y Axis Double 0 for auto Effective Length 50 Effective Length Factor in Z Axis Double 0 for auto Effective Length 51 Effective Length Factor in Y Axis Double 0 for auto Effective Length 52 Brace Type Integer 0 for unbraced member, 1 for braced in Both Axes, 2 for Only braced in y Axis, 3 for Only braced in z Axis Effective Length 53 Gyration Radius Calculation Of Single Angle Integer 0 for Strong Weak Axis Calculation (U-V Axis), 1 for Parallel Leg Calculation (Y-Z Axis) Effective Length 54 Gyration Radius Calculation Of Single Angle Integer 0 for Strong Weak Axis Calculation (U-V Axis), 1 for Parallel Leg Calculation (Y-Z Axis) Effective Length 55 Flange Slenderness Limit (b/t) Double 0 for auto Detailing Checks 56 Web Slenderness Limit (h0/tw) Double 0 for auto Detailing Checks 57 Include check for web of H profiles in a Truss as per GB50017-2017, Table 8.5.2 Boolean Detailing Checks 58 Net Area Factor (An/A) double Section Factor 59 Net Resistance Moment Factor (Wnz/Wz) double Section Factor 60 Net Resistance Moment Factor (Wny/Wy) double Section Factor 61 Effective Section Factor Of Axis Force (nA) double Section Factor 62 Reduce Strength Of Angle Section (Ref 7.6.1) boolean Section Factor 63 Unequal Angle Connection Type Integer 0 for Short Leg, 1 for Long Leg. Section Factor 64 Strength Reduction Factor double 0 for auto Section Factor 65 Stability Reduction Factor double 0 for auto Section Factor 66 Performance_StructureType Integer Performance Design 67 Performance_PerformanceGrade Integer Performance Design 68 Performance_PrecautionaryCategory Integer Performance Design 69 Performance_DuctilityGrade Integer Performance Design 70 Performance_PerformanceFactor double Performance Design 71 Performance_AmplificationFactor double Performance Design 72 Performance_Yita_y double Performance Design 73 Performance_DissipatedLength double Performance Design 74 ThinWall_SteelNo String "Q235","Q345", Other value regard as "auto" GB50018 Only 75 ThinWall_CheckCompressedFlakiness Boolean 0 for unchecked, 1 for checked GB50018 Only 76 ThinWall_CheckLipFlakiness Boolean 0 for unchecked, 1 for checked GB50018 Only 77 ThinWall_BearingStatus Integer 0 for Auto, 1 for HingedWarpFree, 2 for BuildingInWarpRestrained, 3 for HingedWarpRestrained GB50018 Only 78 ThinWall_HavingBattenPlate Boolean 0 for unchecked, 1 for checked GB50018 Only 79 ThinWall_BattenPlateSpace double GB50018 Only 80 ThinWall_UnstiffenedFlakinessLimit double 0 for auto GB50018 Only 81 ThinWall_PartStiffenedFlakinessLimit double 0 for auto GB50018 Only 82 ThinWall_StiffenedFlakinessLimit double 0 for auto GB50018 Only 83 ThinWall_LipFlakinessLimit double 0 for auto GB50018 Only 84 ThinWall_BeamBearing Integer 1 for uniformForce, 2 for concentratedForce, 3 for endMoment3, 4 for endMoment4, 5 for endMoment5,6 for endMoment6,7 for endMoment7 GB50018 Only 85 ThinWall_LateralSupport Integer 1 for NoSupport, 2 for OneSupport, 3 for TwoOrMoreSupport GB50018 Only 86 ThinWall_IsConsiderColdFormingEffect Boolean 0 for unchecked, 1 for checked GB50018 Only 87 ThinWall_IsHighFrequencyWelding Boolean 0 for unchecked, 1 for checked GB50018 Only 88 ThinWall_ForceToFlexuralCenter_Z double can be changed in gsp file GB50018 Only 89 ThinWall_ForceToFlexuralCenter_Y double can be changed in gsp file GB50018 Only 90 ThinWall_ReduceStrength double 0 for auto GB50018 Only 91 ThinWall_ReduceStability double 0 for auto GB50018 Only 92 GB51249 Member Type Integer 0 for auto, 1 for Bending Member, 2 for Truss,3 for Column, 4 for Frame Beam, 5 for Frame Column GB51249 Only 93 Fire Protection Duration Time double unit:hour GB51249 Only 94 Fire Protection Time Step double unit:second GB51249 Only 95 Fire Protection Temperature Rise Curve String GB51249 Only 96 Fire Protection Hot Smoke Curve String GB51249 Only 97 Fire Protection Epsilon_r double GB51249 Only 98 Fire Protection Steel Type Integer 0 for Structural Steel, 1 for Fire Resistant Steel GB51249 Only 99 Fire Protection Thermal Steel Material Property Reduce Curve String GB51249 Only 100 Fire Protection Stored Item Integer 0 for Fibers, 1 for HC Hydrocarbon GB51249 Only 101 Fire Protection Material Type Integer 0 for Not set Fire Protection material, 1 For Non-Lightweight Fire Protection material, 2 For Intumescent Coating, 3 For Nonintumescent Coating GB51249 Only 102 Fire Protection Material Thickness double GB51249 Only 103 Fire Protection Material Name String GB51249 Only 104 Fire Protection Shape Type Integer 1 for Outer with 3 Edges, 2 for Outer with 4 Edges,3 for Inner with 3 Edges, 4 for Inner with 4 Edges GB51249 Only 105 Beam Bending Strength Boolean Beam/Defined Member Optional Checks 106 Beam Shear Strength Boolean Beam/Column/Defined Member Optional Checks 107 Beam Equivalent Stress Boolean Beam/Defined Member Optional Checks 108 Beam Overall Stability Boolean Beam/Defined Member Optional Checks 109 Beam Web Slenderness Boolean Beam/Defined Member Optional Checks 100 Beam Flange Slenderness Boolean Beam/Defined Member Optional Checks 111 Truss Strength Boolean Truss/Defined Member Optional Checks 112 Truss Stability Boolean Truss/Defined Member Optional Checks 113 Shear Strength Boolean Truss/Defined Member Optional Checks 114 Column Strength Boolean Column/Defined Member Optional Checks 115 Column Stability in-plane Boolean Column/Defined Member Optional Checks 116 Column Stability out-plane Boolean Column/Defined Member Optional Checks 117 Compression Slenderness Boolean Truss/Column/Defined Member Optional Checks 118 Tension Slenderness Boolean Truss/Column/Defined Member Optional Checks 119 Compression Flange Slenderness Boolean Truss/Column/Defined Member Optional Checks 120 Compression Web Slenderness Boolean Truss/Column/Defined Member Optional Checks 121 Beam Deflection Boolean Beam/Defined Member Optional Checks 122 Performance MinFactor Boolean GB50017 Performance Design Optional Checks, reserved 123 Performance BeamAxialForce Boolean GB50017 Performance Design Optional Checks, reserved 124 Performance BeamShear Boolean GB50017 Performance Design Optional Checks, reserved 125 Performance Lambda Boolean GB50017 Performance Design Optional Checks, reserved 126 Performance WebSlenderness Boolean GB50017 Performance Design Optional Checks, reserved 127 Performance FlangeSlenderness Boolean GB50017 Performance Design Optional Checks, reserved 128 Performance TrussNetArea Boolean GB50017 Performance Design Optional Checks, reserved 129 Performance DissipatedLen Boolean GB50017 Performance Design Optional Checks, reserved 130 Performance StrongColumn Boolean GB50017 Performance Design Optional Checks, reserved 131 Thin Wall Pressed Flakiness Boolean GB50018 Only Optional Checks 132 Thin Wall Lip Flakiness Boolean GB50018 Only Optional Checks 133 GB51249 Frame Beam Strength Boolean GB51249 Only Optional Checks, reserved 134 GB51249 Frame Column Stability Boolean GB51249 Only Optional Checks, reserved 135 GB51249 FireproofThickness Boolean GB51249 Only Optional Checks, reserved [out] varParamValue Parameter Item value(Type: String). Return values true Success. false General error. VBA Syntax See also OSDesignUI::ChineseSteelGetSolution OSDesignUI::ChineseSteelSetDesignParameter OSDesignUI::ChineseSteelAddDesignParameter OSDesignUI::ChineseSteelDeletedDesignParameter

**Value table 1** (source column groupings retained):

| Value | Parameter Item Type | Type | Description | In Page |
| --- | --- | --- | --- | --- |
| 0 | Code Type | String | 1 for GB50017, 2 for GB50018, 3 for GB51249 | General |
| 1 | Parameter Name | String |  | General |
| 2 | Steel No | String | "Q235","Q345","Q355","Q390","Q420","Q460" could be set, or other user defined material. | General |
| 3 | Member Type | Integer | 0 for auto, 1 for Bending Member, 2 for Truss Member, 3 for Column Member, 4 for Defined Member. | General |
| 4 | Judgement Principle for Member Type | Integer | Only Member Type is 0, This value is worked.0 for Geometry, 1 for Stress Feature. | General |
| 5 | Cantilever Type | Integer | 0 for None-Cantilever, 1 for Cantilever in Both, 2 for Cantilever in Z Axis, 3 for Cantilever in Y Axis. | General |
| 6 | Bending with Axial Force | Integer | 0 for Warning, 1 for Ignore, 2 for Check. | General |
| 7 | Use Axis forces member Post-Buckling | Boolean |  | General |
| 8 | Truss Secondary Moment | Boolean |  | General |
| 9 | Section Slenderness Grade | Integer | 1 for S1, 2 for S2, 3 for S3, 4 for S4, 5 for S5 | General |
| 10 | Optimize Design | Boolean |  | General |
| 11 | Fatigue Check Request | Boolean |  | General |
| 12 | Safety Ratio | Double |  | General |
| 13 | Failure Ratio | Double |  | General |
| 14 | Force Loads List | String |  | General |
| 15 | Displacement Loads List | String |  | General |
| 16 | Use Seismic Adjusting Factor | Boolean |  | Seismic Design |
| 17 | Strength Seismic Adjusting Factor | Double | 0 for auto | Seismic Design |
| 18 | Stability Seismic Adjusting Factor | Double | 0 for auto | Seismic Design |
| 19 | Seismic Measure Grade | Integer | -1 for Auto, 0 for None, 1 for Level1, 2 for Level2, 3 for Level3, 4 for Level4. | Seismic Design |
| 20 | Reduction Factor of Bearing Capacity | Double |  | Seismic Design |
| 21 | Allowable Compression Slenderness | Double | 0 for auto | Allowable Slenderness |
| 22 | Allowable Tension Slenderness | Double | 0 for auto | Allowable Slenderness |
| 23 | Seismic Column, GB50011-2010, 8_3_1 | Boolean |  | Allowable Slenderness |
| 24 | Seismic Brace, GB50011-2010,8.4.1 | Boolean |  | Allowable Slenderness |
| 25 | Seismic Single-storey Plant, GB50011-2010,9.2.13 | Boolean |  | Allowable Slenderness |
| 26 | Seismic Multi-storey Plant, GB50011-2010,H.2.8 | Boolean |  | Allowable Slenderness |
| 27 | Plastic Development Factor in Z Axis | Double | 0 for auto | Plastic Development Factor |
| 28 | Plastic Development Factor in Y Axis | Double | 0 for auto | Plastic Development Factor |
| 29 | Plastic Development Factor in Sharp side | Double | 0 for auto | Plastic Development Factor |
| 30 | Equivalent Moment Factor in-Plane of Z Axis | Double | 0 for auto | Equivalent Moment Factor |
| 31 | Equivalent Moment Factor in-Plane of Y Axis | Double | 0 for auto | Equivalent Moment Factor |
| 32 | Equivalent Moment Factor out-Plane of Z Axis | Double | 0 for auto | Equivalent Moment Factor |
| 33 | Equivalent Moment Factor out-Plane of Y Axis | Double | 0 for auto | Equivalent Moment Factor |
| 34 | Lateral Load Along Local z | Boolean |  | Equivalent Moment Factor |
| 35 | Lateral Load Along Local y | Boolean |  | Equivalent Moment Factor |
| 36 | Span/Deflection (DFF) | Double |  | Deflection Parameters |
| 37 | Start Joint (DJ1) | Integer | -1 for Member Start Joint, 0 Auto | Deflection Parameters |
| 38 | End Joint (DJ2) | Integer | -1 for Member End Joint, 0 Auto | Deflection Parameters |
| 39 | Check Horizontal Section Displacement | Boolean |  | Deflection Parameters |
| 40 | Overall Stability Factor of Beam in Z Axis | Double | 0 for auto | Stability Factors |
| 41 | Overall Stability Factor of Beam in Y Axis | Double | 0 for auto | Stability Factors |
| 42 | Stress Feature | Integer | 1 for Type1, 2 for Type2, ... 10 for Type10. If Cantilever type is in Z-Axis or Both Axis, Only 1,2,3 support. | Stability Factors |
| 43 | Consider C.0.5 | Boolean |  | Stability Factors |
| 44 | Stability Factor of Axial Compression in Z Axis | Double | 0 for auto | Stability Factors |
| 45 | Stability Factor of Axial Compression in Y Axis | Double | 0 for auto | Stability Factors |
| 46 | Axial Compression Section Type in Z Axis | Integer | 0 for auto, 1 for Class a, 2 for Class b, 3 for Class c, 4 for Class d | Stability Factors |
| 47 | Axial Compression Section Type in Y Axis | Integer | 0 for auto, 1 for Class a, 2 for Class b, 3 for Class c, 4 for Class d | Stability Factors |
| 48 | Unbraced Length in Z Axis | Double | 0 for auto | Effective Length |
| 49 | Unbraced Length in Y Axis | Double | 0 for auto | Effective Length |
| 50 | Effective Length Factor in Z Axis | Double | 0 for auto | Effective Length |
| 51 | Effective Length Factor in Y Axis | Double | 0 for auto | Effective Length |
| 52 | Brace Type | Integer | 0 for unbraced member, 1 for braced in Both Axes, 2 for Only braced in y Axis, 3 for Only braced in z Axis | Effective Length |
| 53 | Gyration Radius Calculation Of Single Angle | Integer | 0 for Strong Weak Axis Calculation (U-V Axis), 1 for Parallel Leg Calculation (Y-Z Axis) | Effective Length |
| 54 | Gyration Radius Calculation Of Single Angle | Integer | 0 for Strong Weak Axis Calculation (U-V Axis), 1 for Parallel Leg Calculation (Y-Z Axis) | Effective Length |
| 55 | Flange Slenderness Limit (b/t) | Double | 0 for auto | Detailing Checks |
| 56 | Web Slenderness Limit (h0/tw) | Double | 0 for auto | Detailing Checks |
| 57 | Include check for web of H profiles in a Truss as per GB50017-2017, Table 8.5.2 | Boolean |  | Detailing Checks |
| 58 | Net Area Factor (An/A) | double |  | Section Factor |
| 59 | Net Resistance Moment Factor (Wnz/Wz) | double |  | Section Factor |
| 60 | Net Resistance Moment Factor (Wny/Wy) | double |  | Section Factor |
| 61 | Effective Section Factor Of Axis Force (nA) | double |  | Section Factor |
| 62 | Reduce Strength Of Angle Section (Ref 7.6.1) | boolean |  | Section Factor |
| 63 | Unequal Angle Connection Type | Integer | 0 for Short Leg, 1 for Long Leg. | Section Factor |
| 64 | Strength Reduction Factor | double | 0 for auto | Section Factor |
| 65 | Stability Reduction Factor | double | 0 for auto | Section Factor |
| 66 | Performance_StructureType | Integer |  | Performance Design |
| 67 | Performance_PerformanceGrade | Integer |  | Performance Design |
| 68 | Performance_PrecautionaryCategory | Integer |  | Performance Design |
| 69 | Performance_DuctilityGrade | Integer |  | Performance Design |
| 70 | Performance_PerformanceFactor | double |  | Performance Design |
| 71 | Performance_AmplificationFactor | double |  | Performance Design |
| 72 | Performance_Yita_y | double |  | Performance Design |
| 73 | Performance_DissipatedLength | double |  | Performance Design |
| 74 | ThinWall_SteelNo | String | "Q235","Q345", Other value regard as "auto" | GB50018 Only |
| 75 | ThinWall_CheckCompressedFlakiness | Boolean | 0 for unchecked, 1 for checked | GB50018 Only |
| 76 | ThinWall_CheckLipFlakiness | Boolean | 0 for unchecked, 1 for checked | GB50018 Only |
| 77 | ThinWall_BearingStatus | Integer | 0 for Auto, 1 for HingedWarpFree, 2 for BuildingInWarpRestrained, 3 for HingedWarpRestrained | GB50018 Only |
| 78 | ThinWall_HavingBattenPlate | Boolean | 0 for unchecked, 1 for checked | GB50018 Only |
| 79 | ThinWall_BattenPlateSpace | double | GB50018 Only |  |
| 80 | ThinWall_UnstiffenedFlakinessLimit | double | 0 for auto | GB50018 Only |
| 81 | ThinWall_PartStiffenedFlakinessLimit | double | 0 for auto | GB50018 Only |
| 82 | ThinWall_StiffenedFlakinessLimit | double | 0 for auto | GB50018 Only |
| 83 | ThinWall_LipFlakinessLimit | double | 0 for auto | GB50018 Only |
| 84 | ThinWall_BeamBearing | Integer | 1 for uniformForce, 2 for concentratedForce, 3 for endMoment3, 4 for endMoment4, 5 for endMoment5,6 for endMoment6,7 for endMoment7 | GB50018 Only |
| 85 | ThinWall_LateralSupport | Integer | 1 for NoSupport, 2 for OneSupport, 3 for TwoOrMoreSupport | GB50018 Only |
| 86 | ThinWall_IsConsiderColdFormingEffect | Boolean | 0 for unchecked, 1 for checked | GB50018 Only |
| 87 | ThinWall_IsHighFrequencyWelding | Boolean | 0 for unchecked, 1 for checked | GB50018 Only |
| 88 | ThinWall_ForceToFlexuralCenter_Z | double | can be changed in gsp file | GB50018 Only |
| 89 | ThinWall_ForceToFlexuralCenter_Y | double | can be changed in gsp file | GB50018 Only |
| 90 | ThinWall_ReduceStrength | double | 0 for auto | GB50018 Only |
| 91 | ThinWall_ReduceStability | double | 0 for auto | GB50018 Only |
| 92 | GB51249 Member Type | Integer | 0 for auto, 1 for Bending Member, 2 for Truss,3 for Column, 4 for Frame Beam, 5 for Frame Column | GB51249 Only |
| 93 | Fire Protection Duration Time | double | unit:hour | GB51249 Only |
| 94 | Fire Protection Time Step | double | unit:second | GB51249 Only |
| 95 | Fire Protection Temperature Rise Curve | String |  | GB51249 Only |
| 96 | Fire Protection Hot Smoke Curve | String |  | GB51249 Only |
| 97 | Fire Protection Epsilon_r | double |  | GB51249 Only |
| 98 | Fire Protection Steel Type | Integer | 0 for Structural Steel, 1 for Fire Resistant Steel | GB51249 Only |
| 99 | Fire Protection Thermal Steel Material Property Reduce Curve | String |  | GB51249 Only |
| 100 | Fire Protection Stored Item | Integer | 0 for Fibers, 1 for HC Hydrocarbon | GB51249 Only |
| 101 | Fire Protection Material Type | Integer | 0 for Not set Fire Protection material, 1 For Non-Lightweight Fire Protection material, 2 For Intumescent Coating, 3 For Nonintumescent Coating | GB51249 Only |
| 102 | Fire Protection Material Thickness | double |  | GB51249 Only |
| 103 | Fire Protection Material Name | String |  | GB51249 Only |
| 104 | Fire Protection Shape Type | Integer | 1 for Outer with 3 Edges, 2 for Outer with 4 Edges,3 for Inner with 3 Edges, 4 for Inner with 4 Edges | GB51249 Only |
| 105 | Beam Bending Strength | Boolean | Beam/Defined Member | Optional Checks |
| 106 | Beam Shear Strength | Boolean | Beam/Column/Defined Member | Optional Checks |
| 107 | Beam Equivalent Stress | Boolean | Beam/Defined Member | Optional Checks |
| 108 | Beam Overall Stability | Boolean | Beam/Defined Member | Optional Checks |
| 109 | Beam Web Slenderness | Boolean | Beam/Defined Member | Optional Checks |
| 100 | Beam Flange Slenderness | Boolean | Beam/Defined Member | Optional Checks |
| 111 | Truss Strength | Boolean | Truss/Defined Member | Optional Checks |
| 112 | Truss Stability | Boolean | Truss/Defined Member | Optional Checks |
| 113 | Shear Strength | Boolean | Truss/Defined Member | Optional Checks |
| 114 | Column Strength | Boolean | Column/Defined Member | Optional Checks |
| 115 | Column Stability in-plane | Boolean | Column/Defined Member | Optional Checks |
| 116 | Column Stability out-plane | Boolean | Column/Defined Member | Optional Checks |
| 117 | Compression Slenderness | Boolean | Truss/Column/Defined Member | Optional Checks |
| 118 | Tension Slenderness | Boolean | Truss/Column/Defined Member | Optional Checks |
| 119 | Compression Flange Slenderness | Boolean | Truss/Column/Defined Member | Optional Checks |
| 120 | Compression Web Slenderness | Boolean | Truss/Column/Defined Member | Optional Checks |
| 121 | Beam Deflection | Boolean | Beam/Defined Member | Optional Checks |
| 122 | Performance MinFactor | Boolean | GB50017 Performance Design | Optional Checks, reserved |
| 123 | Performance BeamAxialForce | Boolean | GB50017 Performance Design | Optional Checks, reserved |
| 124 | Performance BeamShear | Boolean | GB50017 Performance Design | Optional Checks, reserved |
| 125 | Performance Lambda | Boolean | GB50017 Performance Design | Optional Checks, reserved |
| 126 | Performance WebSlenderness | Boolean | GB50017 Performance Design | Optional Checks, reserved |
| 127 | Performance FlangeSlenderness | Boolean | GB50017 Performance Design | Optional Checks, reserved |
| 128 | Performance TrussNetArea | Boolean | GB50017 Performance Design | Optional Checks, reserved |
| 129 | Performance DissipatedLen | Boolean | GB50017 Performance Design | Optional Checks, reserved |
| 130 | Performance StrongColumn | Boolean | GB50017 Performance Design | Optional Checks, reserved |
| 131 | Thin Wall Pressed Flakiness | Boolean | GB50018 Only | Optional Checks |
| 132 | Thin Wall Lip Flakiness | Boolean | GB50018 Only | Optional Checks |
| 133 | GB51249 Frame Beam Strength | Boolean | GB51249 Only | Optional Checks, reserved |
| 134 | GB51249 Frame Column Stability | Boolean | GB51249 Only | Optional Checks, reserved |
| 135 | GB51249 FireproofThickness | Boolean | GB51249 Only | Optional Checks, reserved |

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelGetDesignParameter(System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelsetdesignparameter"></a>

### 17. ChineseSteelSetDesignParameter

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga76f0311b021cc9ac3b3a8e7651ebf7e6) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga76f0311b021cc9ac3b3a8e7651ebf7e6) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga76f0311b021cc9ac3b3a8e7651ebf7e6)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignMutationResult SetDesignParameter; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelSetDesignParameter(const VARIANT FAR&varSolutionNo,const VARIANT FAR&varParamIndex,const VARIANT FAR&varParamType,const VARIANT FAR&varParamValue)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varSolutionNo` | `const VARIANT FAR&` | [in] | Solution Id(Type: Long/Integer). |
| `varParamIndex` | `const VARIANT FAR&` | [in] | Design Parameter Index in the solution, Start from 0.(Type: Long/Integer). |
| `varParamType` | `const VARIANT FAR&` | [in] | Parameter Item Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetDesignParameter . |
| `varParamValue` | `const VARIANT FAR&` | [in] | Parameter Item value(Type: String). |

**Contract, including return/status semantics:** Get a design parameter value from the solution in Chinese Steel Design. Parameters [in] varSolutionNo Solution Id(Type: Long/Integer). [in] varParamIndex Design Parameter Index in the solution, Start from 0.(Type: Long/Integer). [in] varParamType Parameter Item Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetDesignParameter . [in] varParamValue Parameter Item value(Type: String). Return values true Success. false General error. VBA Syntax See also OSDesignUI::ChineseSteelGetSolution OSDesignUI::ChineseSteelGetDesignParameter OSDesignUI::ChineseSteelAddDesignParameter OSDesignUI::ChineseSteelDeletedDesignParameter

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelSetDesignParameter(System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelassigndesigparameter"></a>

### 18. ChineseSteelAssignDesigParameter

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga91fbd6d81cf12c745f281f884834a3eb) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga91fbd6d81cf12c745f281f884834a3eb) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga91fbd6d81cf12c745f281f884834a3eb)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignMutationResult AssignDesigParameter; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelAssignDesigParameter(const VARIANT FAR&varSolutionNo,const VARIANT FAR&varParamIndex,const VARIANT FAR&varMemeberNos)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varSolutionNo` | `const VARIANT FAR&` | [in] | Solution Id(Type: Long/Integer). |
| `varParamIndex` | `const VARIANT FAR&` | [in] | Design Parameter Index in the solution, Start from 0.(Type: Long/Integer). |
| `varMemeberNos` | `const VARIANT FAR&` | [in] | Member Nos(Type: Long / Long Array). |

**Contract, including return/status semantics:** Assign Member Nos to a design parameter in Chinese Steel Design. Parameters [in] varSolutionNo Solution Id(Type: Long/Integer). [in] varParamIndex Design Parameter Index in the solution, Start from 0.(Type: Long/Integer). [in] varMemeberNos Member Nos(Type: Long / Long Array). Return values true Success. false General error. VBA Syntax See also OSDesignUI::ChineseSteelGetSolution OSDesignUI::ChineseSteelGetDesignParameter OSDesignUI::ChineseSteelAddDesignParameter OSDesignUI::ChineseSteelDeletedDesignParameter

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelAssignDesigParameter(System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelgetcheckitem"></a>

### 19. ChineseSteelGetCheckItem

**Native references:** 2024: absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gab07918fff1ccf3d77181402ee0d832cb) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gab07918fff1ccf3d77181402ee0d832cb)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignQueryResult<T> GetCheckItem; T is a named solution/settings/check-items record, string name or typed parameter value according to the outputs below; no public native holder.`

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetCheckItem(const VARIANT FAR&varCodeType,LPDISPATCH pCheckItem)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varCodeType` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `pCheckItem` | `LPDISPATCH` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `codeType` | [in] | Code Type(Type - long), please reference OSDesignUI::ChineseSteelGetDesignParameter |
| `pCheckOption` | [out] | Check Items(Type - COSChineseSteelCheckItem ). |

**Contract, including return/status semantics:** get Code Check Item in Chinese Steel Design. Parameters [in] codeType Code Type(Type - long), please reference OSDesignUI::ChineseSteelGetDesignParameter [out] pCheckOption Check Items(Type - COSChineseSteelCheckItem ). Return values true Success. false Generate error. VBA Syntax See also COSChineseSteelCheckItem OSDesignUI::ChineseSteelSetCheckItem

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelGetCheckItem(System.Object, OpenSTAADUI.OSChineseSteelCheckItem)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelsetcheckitem"></a>

### 20. ChineseSteelSetCheckItem

**Native references:** 2024: absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga170b6c2a5b67f85db1cb8ff371bcbced) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga170b6c2a5b67f85db1cb8ff371bcbced)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignMutationResult SetCheckItem; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelSetCheckItem(const VARIANT FAR&varCodeType,const LPDISPATCH pCheckItem)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varCodeType` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `pCheckItem` | `const LPDISPATCH` | [out] | Check Items(Type - COSChineseSteelCheckItem ). |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `codeType` | [in] | Code Type(Type - long), please reference OSDesignUI::ChineseSteelGetDesignParameter |

**Contract, including return/status semantics:** Set Code Check Item in Chinese Steel Design. Parameters [in] codeType Code Type(Type - long), please reference OSDesignUI::ChineseSteelGetDesignParameter [out] pCheckItem Check Items(Type - COSChineseSteelCheckItem ). Return values true Success. false Generate error. VBA Syntax See also COSChineseSteelCheckItem OSDesignUI::ChineseSteelGetCheckItem

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelSetCheckItem(System.Object, OpenSTAADUI.OSChineseSteelCheckItem)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelgetcheckoption"></a>

### 21. ChineseSteelGetCheckOption

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga5143ed8fdb53208773dfbba2d0ba2ee6) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga5143ed8fdb53208773dfbba2d0ba2ee6) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga5143ed8fdb53208773dfbba2d0ba2ee6)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignQueryResult<T> GetCheckOption; T is a named solution/settings/check-items record, string name or typed parameter value according to the outputs below; no public native holder.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetCheckOption(LPDISPATCH pCheckOption)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `pCheckOption` | `LPDISPATCH` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `pCheckOption,Check` | [out] | Option(Type - COSChineseSteelCheckOption ). |

**Contract, including return/status semantics:** get Code Check Option in Chinese Steel Design. Parameters [out] pCheckOption,Check Option(Type - COSChineseSteelCheckOption ). Return values true Success. false General error. VBA Syntax See also COSChineseSteelCheckOption OSDesignUI::ChineseSteelSetCheckOption

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetCheckOption(LPDISPATCH pCheckOption)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `pCheckOption` | `LPDISPATCH` | [out] | Check Option(Type - COSChineseSteelCheckOption ). |

**Contract, including return/status semantics:** get Code Check Option in Chinese Steel Design. Parameters [out] pCheckOption Check Option(Type - COSChineseSteelCheckOption ). Return values true Success. false General error. VBA Syntax See also COSChineseSteelCheckOption OSDesignUI::ChineseSteelSetCheckOption

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelGetCheckOption(OpenSTAADUI.OSChineseSteelCheckOption)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelsetcheckoption"></a>

### 22. ChineseSteelSetCheckOption

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga65aeaf44307ec7a763e88718eb7eeaaf) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga65aeaf44307ec7a763e88718eb7eeaaf) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga65aeaf44307ec7a763e88718eb7eeaaf)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignMutationResult SetCheckOption; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelSetCheckOption(const LPDISPATCH pCheckOption)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `pCheckOption` | `const LPDISPATCH` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `pCheckOption,Check` | [in] | Option(Type - COSChineseSteelCheckOption ). |

**Contract, including return/status semantics:** Set Code Check Option in Chinese Steel Design. Parameters [in] pCheckOption,Check Option(Type - COSChineseSteelCheckOption ). Return values true Success. false General error. VBA Syntax See also COSChineseSteelCheckOption OSDesignUI::ChineseSteelGetCheckOption

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelSetCheckOption(const LPDISPATCH pCheckOption)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `pCheckOption` | `const LPDISPATCH` | [in] | Check Option(Type - COSChineseSteelCheckOption ). |

**Contract, including return/status semantics:** Set Code Check Option in Chinese Steel Design. Parameters [in] pCheckOption Check Option(Type - COSChineseSteelCheckOption ). Return values true Success. false General error. VBA Syntax See also COSChineseSteelCheckOption OSDesignUI::ChineseSteelGetCheckOption

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelSetCheckOption(OpenSTAADUI.OSChineseSteelCheckOption)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelgetresults"></a>

### 23. ChineseSteelGetResults

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga2b7360c7b4cc2f37c9a1de7a9422f8a5) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga65ed76eaa22e34d9986cc812c97710fe) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga65ed76eaa22e34d9986cc812c97710fe)<br>**Current:** Pending; D4.

**Managed proposal:** `DesignQueryResult<IReadOnlyList<ChineseSteelCheckResult>> GetChineseSteelResults(ChineseSteelMemberQuery query, ChineseSteelResultCategory resultCategory); snapshot holder contents.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetResults(const VARIANT FAR&varSolutionNo,const VARIANT FAR&varMemberNo,const VARIANT FAR&varCodeType,const VARIANT FAR&varResultCategory,LPDISPATCH pDesignResult)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varSolutionNo` | `const VARIANT FAR&` | [in] | Chinese Steel Design solution No(Type-Int). |
| `varMemberNo` | `const VARIANT FAR&` | [in] | Member number ID(Type-Int). |
| `varCodeType` | `const VARIANT FAR&` | [in] | Design Code Type(Type-Int). [value table below] |
| `varResultCategory` | `const VARIANT FAR&` | [in] | Result Category Type(Type-Int). [value table below] |
| `pDesignResult` | `LPDISPATCH` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `pDesignResult,Design` | [out] | Results(Type - COSChineseSteelDgnResults ). |

**Contract, including return/status semantics:** get the specified member design results in a specified solution. Parameters [in] varSolutionNo Chinese Steel Design solution No(Type-Int). [in] varMemberNo Member number ID(Type-Int). [in] varCodeType Design Code Type(Type-Int). Value Design Code Type 1 GB50017 2 GB50018 [in] varResultCategory Result Category Type(Type-Int). Value Result Category Type 1 General result 2 Dissipated result 3 NonDissipated result [out] pDesignResult,Design Results(Type - COSChineseSteelDgnResults ). Return values true Success. false General error. VBA Syntax See also OSDesignUI::ChineseSteelGetSolution OSDesignUI::ChineseSteelGetCodeItemName COSChineseSteelDgnResults

**Value table 1** (source column groupings retained):

| Value | Design Code Type |
| --- | --- |
| 1 | GB50017 |
| 2 | GB50018 |

**Value table 2** (source column groupings retained):

| Value | Result Category Type |
| --- | --- |
| 1 | General result |
| 2 | Dissipated result |
| 3 | NonDissipated result |

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetResults(const VARIANT FAR&varSolutionNo,const VARIANT FAR&varMemberNo,const VARIANT FAR&varResultCategory,LPDISPATCH pDesignResult)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varSolutionNo` | `const VARIANT FAR&` | [in] | Chinese Steel Design solution No(Type-Int). |
| `varMemberNo` | `const VARIANT FAR&` | [in] | Member number ID(Type-Int). |
| `varResultCategory` | `const VARIANT FAR&` | [in] | Result Category Type(Type-Int). [value table below] |
| `pDesignResult` | `LPDISPATCH` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `pDesignResult,Design` | [out] | Results(Type - COSChineseSteelDgnResults ). |

**Contract, including return/status semantics:** get the specified member design results in a specified solution. Parameters [in] varSolutionNo Chinese Steel Design solution No(Type-Int). [in] varMemberNo Member number ID(Type-Int). [in] varResultCategory Result Category Type(Type-Int). Value Result Category Type 1 General result 2 Dissipated result 3 NonDissipated result [out] pDesignResult,Design Results(Type - COSChineseSteelDgnResults ). Return values true Success. false General error. VBA Syntax See also OSDesignUI::ChineseSteelGetSolution OSDesignUI::ChineseSteelGetCodeItemName COSChineseSteelDgnResults

**Value table 1** (source column groupings retained):

| Value | Result Category Type |
| --- | --- |
| 1 | General result |
| 2 | Dissipated result |
| 3 | NonDissipated result |

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelGetResults(System.Object, System.Object, System.Object, OpenSTAADUI.OSChineseSteelDgnResults)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelgetcodeitemname"></a>

### 24. ChineseSteelGetCodeItemName

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga6384ab46038a19abdba0da63d56a0d12) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga6384ab46038a19abdba0da63d56a0d12) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga6384ab46038a19abdba0da63d56a0d12)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignQueryResult<T> GetCodeItemName; T is a named solution/settings/check-items record, string name or typed parameter value according to the outputs below; no public native holder.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetCodeItemName(const VARIANT FAR&varCodeItem)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varCodeItem` | `const VARIANT FAR&` | [in] | Code Check item ID(Type: Long). [value table below] |

**Contract, including return/status semantics:** get the check item Name in Chinese Steel Design. Parameters [in] varCodeItem Code Check item ID(Type: Long). Value Code Check Item Code 1 Beam Bending Strength GB50017/GB50018 2 Shear Strength GB50017/GB50018 3 Equivalent Stress GB50017/GB50018 4 Overall Stability GB50017/GB50018 5 Web Slenderness GB50017/GB50018 6 Flange Slenderness GB50017/GB50018 7 Truss Strength GB50017/GB50018 8 Compression Stability GB50017/GB50018 9 Compression Shear GB50017/GB50018 10 Column Strength GB50017/GB50018 11 Stability around Z Axis GB50017/GB50018 12 Stability around Y Axis GB50017/GB50018 13 Compression Slendernes GB50017/GB50018 14 Tension Slenderness GB50017/GB50018 15 Compression Flange Slenderness GB50017 16 Compression Web Slenderness GB50017 17 Beam Deflection GB50017/GB50018 18 Min Performance Factor GB50017-Performance 19 Beam Axial Force GB50017-Performance 20 Plastic Shear Force GB50017-Performance 21 Dissipated Slenderness GB50017-Performance 22 Dissipated Web Slenderness GB50017-Performance 23 Dissipated Flange Slenderness GB50017-Performance 24 Truss Net Area GB50017-Performance 25 Dissipated Beam Length GB50017-Performance 26 Strong Column GB50017-Performance 27 Pressed Plate Flakiness Ratio GB50018 28 Lip Flakiness Ratio GB50018 Returns check item Name(Type: String). VBA Syntax

**Value table 1** (source column groupings retained):

| Value | Code Check Item | Code |
| --- | --- | --- |
| 1 | Beam Bending Strength | GB50017/GB50018 |
| 2 | Shear Strength | GB50017/GB50018 |
| 3 | Equivalent Stress | GB50017/GB50018 |
| 4 | Overall Stability | GB50017/GB50018 |
| 5 | Web Slenderness | GB50017/GB50018 |
| 6 | Flange Slenderness | GB50017/GB50018 |
| 7 | Truss Strength | GB50017/GB50018 |
| 8 | Compression Stability | GB50017/GB50018 |
| 9 | Compression Shear | GB50017/GB50018 |
| 10 | Column Strength | GB50017/GB50018 |
| 11 | Stability around Z Axis | GB50017/GB50018 |
| 12 | Stability around Y Axis | GB50017/GB50018 |
| 13 | Compression Slendernes | GB50017/GB50018 |
| 14 | Tension Slenderness | GB50017/GB50018 |
| 15 | Compression Flange Slenderness | GB50017 |
| 16 | Compression Web Slenderness | GB50017 |
| 17 | Beam Deflection | GB50017/GB50018 |
| 18 | Min Performance Factor | GB50017-Performance |
| 19 | Beam Axial Force | GB50017-Performance |
| 20 | Plastic Shear Force | GB50017-Performance |
| 21 | Dissipated Slenderness | GB50017-Performance |
| 22 | Dissipated Web Slenderness | GB50017-Performance |
| 23 | Dissipated Flange Slenderness | GB50017-Performance |
| 24 | Truss Net Area | GB50017-Performance |
| 25 | Dissipated Beam Length | GB50017-Performance |
| 26 | Strong Column | GB50017-Performance |
| 27 | Pressed Plate Flakiness Ratio | GB50018 |
| 28 | Lip Flakiness Ratio | GB50018 |

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetCodeItemName(const VARIANT FAR&varCodeItem)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varCodeItem` | `const VARIANT FAR&` | [in] | Code Check item ID(Type: Long). [value table below] |

**Contract, including return/status semantics:** get the check item Name in Chinese Steel Design. Parameters [in] varCodeItem Code Check item ID(Type: Long). Value Code Check Item Code 1 Beam Bending Strength GB50017/GB50018 2 Shear Strength GB50017/GB50018 3 Equivalent Stress GB50017/GB50018 4 Overall Stability GB50017/GB50018 5 Web Slenderness GB50017/GB50018 6 Flange Slenderness GB50017/GB50018 7 Truss Strength GB50017/GB50018 8 Compression Stability GB50017/GB50018 9 Compression Shear GB50017/GB50018 10 Column Strength GB50017/GB50018 11 Stability around Z Axis GB50017/GB50018 12 Stability around Y Axis GB50017/GB50018 13 Compression Slendernes GB50017/GB50018 14 Tension Slenderness GB50017/GB50018 15 Compression Flange Slenderness GB50017 16 Compression Web Slenderness GB50017 17 Beam Deflection GB50017/GB50018 18 Min Performance Factor GB50017-Performance 19 Beam Axial Force GB50017-Performance 20 Plastic Shear Force GB50017-Performance 21 Dissipated Slenderness GB50017-Performance 22 Dissipated Web Slenderness GB50017-Performance 23 Dissipated Flange Slenderness GB50017-Performance 24 Truss Net Area GB50017-Performance 25 Dissipated Beam Length GB50017-Performance 26 Strong Column GB50017-Performance 27 Pressed Plate Flakiness Ratio GB50018 28 Lip Flakiness Ratio GB50018 29 Frame Beam Strength GB51249 30 Frame Column Stability GB51249 31 Fireproof Thickness GB51249 Returns check item Name(Type: String). VBA Syntax

**Value table 1** (source column groupings retained):

| Value | Code Check Item | Code |
| --- | --- | --- |
| 1 | Beam Bending Strength | GB50017/GB50018 |
| 2 | Shear Strength | GB50017/GB50018 |
| 3 | Equivalent Stress | GB50017/GB50018 |
| 4 | Overall Stability | GB50017/GB50018 |
| 5 | Web Slenderness | GB50017/GB50018 |
| 6 | Flange Slenderness | GB50017/GB50018 |
| 7 | Truss Strength | GB50017/GB50018 |
| 8 | Compression Stability | GB50017/GB50018 |
| 9 | Compression Shear | GB50017/GB50018 |
| 10 | Column Strength | GB50017/GB50018 |
| 11 | Stability around Z Axis | GB50017/GB50018 |
| 12 | Stability around Y Axis | GB50017/GB50018 |
| 13 | Compression Slendernes | GB50017/GB50018 |
| 14 | Tension Slenderness | GB50017/GB50018 |
| 15 | Compression Flange Slenderness | GB50017 |
| 16 | Compression Web Slenderness | GB50017 |
| 17 | Beam Deflection | GB50017/GB50018 |
| 18 | Min Performance Factor | GB50017-Performance |
| 19 | Beam Axial Force | GB50017-Performance |
| 20 | Plastic Shear Force | GB50017-Performance |
| 21 | Dissipated Slenderness | GB50017-Performance |
| 22 | Dissipated Web Slenderness | GB50017-Performance |
| 23 | Dissipated Flange Slenderness | GB50017-Performance |
| 24 | Truss Net Area | GB50017-Performance |
| 25 | Dissipated Beam Length | GB50017-Performance |
| 26 | Strong Column | GB50017-Performance |
| 27 | Pressed Plate Flakiness Ratio | GB50018 |
| 28 | Lip Flakiness Ratio | GB50018 |
| 29 | Frame Beam Strength | GB51249 |
| 30 | Frame Column Stability | GB51249 |
| 31 | Fireproof Thickness | GB51249 |

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelGetCodeItemName(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelgetmaxresult"></a>

### 25. ChineseSteelGetMaxResult

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga4012017d72237f2033f29b6cf1e35743) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga50dbfaee1df1de79af8b9b102350fc5d) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga50dbfaee1df1de79af8b9b102350fc5d)<br>**Current:** Pending; D4.

**Managed proposal:** `DesignQueryResult<ChineseSteelMaximumResult> GetChineseSteelMaximumResult(ChineseSteelMemberQuery query); named status, ratio, actual/limit, check item, case, position and category fields.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetMaxResult(const VARIANT FAR&varSolutionNo,const VARIANT FAR&varMemberNo,const VARIANT FAR&varCodeType,VARIANT FAR&varStatus,VARIANT FAR&varMaxRatio,VARIANT FAR&varActualValue,VARIANT FAR&varLimitValue,VARIANT FAR&varControlItem,VARIANT FAR&varControlLoadNo,VARIANT FAR&varControlPosition,VARIANT FAR&varResultCategory)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varSolutionNo` | `const VARIANT FAR&` | [in] | Chinese Steel Design solution No(Type-Int). |
| `varMemberNo` | `const VARIANT FAR&` | [in] | Member number ID(Type-Int). |
| `varCodeType` | `const VARIANT FAR&` | [in] | Design Code Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetResults . |
| `varStatus` | `VARIANT FAR&` | [out] | Design Status(Type - Int). [value table below] |
| `varMaxRatio` | `VARIANT FAR&` | [out] | Design Ratio in the control check item(Type - Double). |
| `varActualValue` | `VARIANT FAR&` | [out] | Calculate value in the control check item(Type - Double). |
| `varLimitValue` | `VARIANT FAR&` | [out] | Limit value in the control check item(Type - Double). |
| `varControlItem` | `VARIANT FAR&` | [out] | control check item(Type - Int), use the reference link to get the check item name, please reference OSDesignUI::ChineseSteelGetCodeItemName . |
| `varControlLoadNo` | `VARIANT FAR&` | [out] | control load case no(Type - Int). |
| `varControlPosition` | `VARIANT FAR&` | [out] | max ratio position in the control check item(Type - Double). |
| `varResultCategory` | `VARIANT FAR&` | [out] | Result Category Type(Type-Int). [value table below] |

**Contract, including return/status semantics:** get the specified member maximum design result in a specified solution. Parameters [in] varSolutionNo Chinese Steel Design solution No(Type-Int). [in] varMemberNo Member number ID(Type-Int). [in] varCodeType Design Code Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetResults . [out] varStatus Design Status(Type - Int). Value Design Status 0 Uncheck 1 Passed 2 TooSafe 3 Failure 4 Warning [out] varMaxRatio Design Ratio in the control check item(Type - Double). [out] varActualValue Calculate value in the control check item(Type - Double). [out] varLimitValue Limit value in the control check item(Type - Double). [out] varControlItem control check item(Type - Int), use the reference link to get the check item name, please reference OSDesignUI::ChineseSteelGetCodeItemName . [out] varControlLoadNo control load case no(Type - Int). [out] varControlPosition max ratio position in the control check item(Type - Double). [out] varResultCategory Result Category Type(Type-Int). Value Result Category Type 1 General result 2 Dissipated result 3 NonDissipated result Return values true Success. false General error. VBA Syntax See also OSDesignUI::ChineseSteelGetSolution OSDesignUI::ChineseSteelGetCodeItemName

**Value table 1** (source column groupings retained):

| Value | Design Status |
| --- | --- |
| 0 | Uncheck |
| 1 | Passed |
| 2 | TooSafe |
| 3 | Failure |
| 4 | Warning |

**Value table 2** (source column groupings retained):

| Value | Result Category Type |
| --- | --- |
| 1 | General result |
| 2 | Dissipated result |
| 3 | NonDissipated result |

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetMaxResult(const VARIANT FAR&varSolutionNo,const VARIANT FAR&varMemberNo,VARIANT FAR&varStatus,VARIANT FAR&varMaxRatio,VARIANT FAR&varActualValue,VARIANT FAR&varLimitValue,VARIANT FAR&varControlItem,VARIANT FAR&varControlLoadNo,VARIANT FAR&varControlPosition,VARIANT FAR&varResultCategory)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varSolutionNo` | `const VARIANT FAR&` | [in] | Chinese Steel Design solution No(Type-Int). |
| `varMemberNo` | `const VARIANT FAR&` | [in] | Member number ID(Type-Int). |
| `varStatus` | `VARIANT FAR&` | [out] | Design Status(Type - Int). [value table below] |
| `varMaxRatio` | `VARIANT FAR&` | [out] | Design Ratio in the control check item(Type - Double). |
| `varActualValue` | `VARIANT FAR&` | [out] | Calculate value in the control check item(Type - Double). |
| `varLimitValue` | `VARIANT FAR&` | [out] | Limit value in the control check item(Type - Double). |
| `varControlItem` | `VARIANT FAR&` | [out] | control check item(Type - Int), use the reference link to get the check item name, please reference OSDesignUI::ChineseSteelGetCodeItemName . |
| `varControlLoadNo` | `VARIANT FAR&` | [out] | control load case no(Type - Int). |
| `varControlPosition` | `VARIANT FAR&` | [out] | max ratio position in the control check item(Type - Double). |
| `varResultCategory` | `VARIANT FAR&` | [out] | Result Category Type(Type-Int). [value table below] |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `varCodeType` | [in] | Design Code Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetResults . |

**Contract, including return/status semantics:** get the specified member maximum design result in a specified solution. Parameters [in] varSolutionNo Chinese Steel Design solution No(Type-Int). [in] varMemberNo Member number ID(Type-Int). [in] varCodeType Design Code Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetResults . [out] varStatus Design Status(Type - Int). Value Design Status 0 Uncheck 1 Passed 2 TooSafe 3 Failure 4 Warning [out] varMaxRatio Design Ratio in the control check item(Type - Double). [out] varActualValue Calculate value in the control check item(Type - Double). [out] varLimitValue Limit value in the control check item(Type - Double). [out] varControlItem control check item(Type - Int), use the reference link to get the check item name, please reference OSDesignUI::ChineseSteelGetCodeItemName . [out] varControlLoadNo control load case no(Type - Int). [out] varControlPosition max ratio position in the control check item(Type - Double). [out] varResultCategory Result Category Type(Type-Int). Value Result Category Type 1 General result 2 Dissipated result 3 NonDissipated result Return values true Success. false General error. VBA Syntax See also OSDesignUI::ChineseSteelGetSolution OSDesignUI::ChineseSteelGetCodeItemName

**Value table 1** (source column groupings retained):

| Value | Design Status |
| --- | --- |
| 0 | Uncheck |
| 1 | Passed |
| 2 | TooSafe |
| 3 | Failure |
| 4 | Warning |

**Value table 2** (source column groupings retained):

| Value | Result Category Type |
| --- | --- |
| 1 | General result |
| 2 | Dissipated result |
| 3 | NonDissipated result |

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelGetMaxResult(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelgetresultparameters"></a>

### 26. ChineseSteelGetResultParameters

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga24d6593a6e04f2a8798c91e1f7da1804) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga8c61c27bcb8325eb35fbc5e1eb461595) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga8c61c27bcb8325eb35fbc5e1eb461595)<br>**Current:** Pending; D4.

**Managed proposal:** `DesignQueryResult<ChineseSteelParameterSettings> GetChineseSteelResultParameters(ChineseSteelMemberQuery query); isolate holder and legacy code-argument policy.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetResultParameters(const VARIANT FAR&varSolutionNo,const VARIANT FAR&varMemberNo,const VARIANT FAR&varCodeType,LPDISPATCH pDesignParameters)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varSolutionNo` | `const VARIANT FAR&` | [in] | Chinese Steel Design solution No(Type-Int). |
| `varMemberNo` | `const VARIANT FAR&` | [in] | Member number ID(Type-Int). |
| `varCodeType` | `const VARIANT FAR&` | [in] | Design Code Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetResults . |
| `pDesignParameters` | `LPDISPATCH` | [out] | Design Parameters(Type - COSChineseSteelDgnParameters ). |

**Contract, including return/status semantics:** get the specified member design results in a specified solution. Parameters [in] varSolutionNo Chinese Steel Design solution No(Type-Int). [in] varMemberNo Member number ID(Type-Int). [in] varCodeType Design Code Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetResults . [out] pDesignParameters Design Parameters(Type - COSChineseSteelDgnParameters ). Return values true Success. false General error. VBA Syntax See also OSDesignUI::ChineseSteelGetSolution COSChineseSteelDgnParameters

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetResultParameters(const VARIANT FAR&varSolutionNo,const VARIANT FAR&varMemberNo,LPDISPATCH pDesignParameters)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varSolutionNo` | `const VARIANT FAR&` | [in] | Chinese Steel Design solution No(Type-Int). |
| `varMemberNo` | `const VARIANT FAR&` | [in] | Member number ID(Type-Int). |
| `pDesignParameters` | `LPDISPATCH` | [out] | Design Parameters(Type - COSChineseSteelDgnParameters ). |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `varCodeType` | [in] | Design Code Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetResults . |

**Contract, including return/status semantics:** get the specified member design results in a specified solution. Parameters [in] varSolutionNo Chinese Steel Design solution No(Type-Int). [in] varMemberNo Member number ID(Type-Int). [in] varCodeType Design Code Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetResults . [out] pDesignParameters Design Parameters(Type - COSChineseSteelDgnParameters ). Return values true Success. false General error. VBA Syntax See also OSDesignUI::ChineseSteelGetSolution COSChineseSteelDgnParameters

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelGetResultParameters(System.Object, System.Object, OpenSTAADUI.OSChineseSteelDgnParameters)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteeldisplaymemberreport"></a>

### 27. ChineseSteelDisplayMemberReport

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga5f3346d843b70dd654712dc989c316f6) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga9b22538e5ba992a748ba4d7af245c9b3) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga9b22538e5ba992a748ba4d7af245c9b3)<br>**Current:** Pending; D4.

**Managed proposal:** `DesignMutationResult DisplayMemberReport; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2024 contract

~~~cpp
void OSDesignUI::ChineseSteelDisplayMemberReport(const VARIANT FAR&varSolutionNo,const VARIANT FAR&varMemberNo,const VARIANT FAR&varCodeType)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varSolutionNo` | `const VARIANT FAR&` | [in] | Chinese Steel Design solution No(Type-Int). |
| `varMemberNo` | `const VARIANT FAR&` | [in] | Member number ID(Type-Int). |
| `varCodeType` | `const VARIANT FAR&` | [in] | Design Code Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetResults . |

**Contract, including return/status semantics:** Display Member Report in Chinese Steel Design. Parameters [in] varSolutionNo Chinese Steel Design solution No(Type-Int). [in] varMemberNo Member number ID(Type-Int). [in] varCodeType Design Code Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetResults . VBA Syntax

#### 2025 contract

~~~cpp
void OSDesignUI::ChineseSteelDisplayMemberReport(const VARIANT FAR&varSolutionNo,const VARIANT FAR&varMemberNo)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varSolutionNo` | `const VARIANT FAR&` | [in] | Chinese Steel Design solution No(Type-Int). |
| `varMemberNo` | `const VARIANT FAR&` | [in] | Member number ID(Type-Int). |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `varCodeType` | [in] | Design Code Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetResults . |

**Contract, including return/status semantics:** Display Member Report in Chinese Steel Design. Parameters [in] varSolutionNo Chinese Steel Design solution No(Type-Int). [in] varMemberNo Member number ID(Type-Int). [in] varCodeType Design Code Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetResults . VBA Syntax

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void ChineseSteelDisplayMemberReport(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteeldisplayresulttable"></a>

### 28. ChineseSteelDisplayResultTable

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#gaa089f006ee5bda11366db2e40cb078c3) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gaa089f006ee5bda11366db2e40cb078c3) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gaa089f006ee5bda11366db2e40cb078c3)<br>**Current:** Pending; D4.

**Managed proposal:** `DesignMutationResult DisplayResultTable; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2024 contract

~~~cpp
void OSDesignUI::ChineseSteelDisplayResultTable()
~~~

No native parameters.

**Contract, including return/status semantics:** Display Chinese Steel Design Result Table. VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `Void ChineseSteelDisplayResultTable()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelgetstoreycount"></a>

### 29. ChineseSteelGetStoreyCount

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#gabab6bba0303627fc3f4f707a93b4c7f6) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gabab6bba0303627fc3f4f707a93b4c7f6) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gabab6bba0303627fc3f4f707a93b4c7f6)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignQueryResult<int> GetStoreyCount; named solutionId/curveKind inputs where documented; distinguish valid empty count from failure.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetStoreyCount()
~~~

No native parameters.

**Contract, including return/status semantics:** get the storey count in Chinese Steel Design. Returns The total number of storey in Chinese Steel Design(Type: Long/Integer). VBA Syntax

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelGetStoreyCount()`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteeladdstorey"></a>

### 30. ChineseSteelAddStorey

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#gaca405995a9f0981e3755e3f03f3b9796) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gaca405995a9f0981e3755e3f03f3b9796) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gaca405995a9f0981e3755e3f03f3b9796)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignMutationResult AddStorey; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelAddStorey(const VARIANT FAR&varName,const VARIANT FAR&varElevation)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varName` | `const VARIANT FAR&` | [in] | storey Name(Type: String). |
| `varElevation` | `const VARIANT FAR&` | [in] | storey elevation(Type: float/double). |

**Contract, including return/status semantics:** add a new storey in Chinese Steel Design. Parameters [in] varName storey Name(Type: String). [in] varElevation storey elevation(Type: float/double). Return values true Success. false General error. VBA Syntax See also OSDesignUI::ChineseSteelDeleteStorey

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelAddStorey(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteeldeletestorey"></a>

### 31. ChineseSteelDeleteStorey

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#ga490a1cf5fec619a86b1c04229ccc9b96) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga490a1cf5fec619a86b1c04229ccc9b96) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga490a1cf5fec619a86b1c04229ccc9b96)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignMutationResult DeleteStorey; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelDeleteStorey(const VARIANT FAR&varIndex)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varIndex` | `const VARIANT FAR&` | [in] | storey index(Type: long/Integer). |

**Contract, including return/status semantics:** Delete a storey from Chinese Steel Design. Parameters [in] varIndex storey index(Type: long/Integer). Return values true Success. false General error. VBA Syntax See also OSDesignUI::ChineseSteelGetSolution OSDesignUI::ChineseSteelAddSolution

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelDeleteStorey(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelassignstorey"></a>

### 32. ChineseSteelAssignStorey

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#gacc8521b0a206a9f41ec31f0120f82574) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gacc8521b0a206a9f41ec31f0120f82574) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gacc8521b0a206a9f41ec31f0120f82574)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignMutationResult AssignStorey; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelAssignStorey(const VARIANT FAR&varIndex,const VARIANT FAR&varMemeberNos)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varIndex` | `const VARIANT FAR&` | [in] | Storey index(Type: Long/Integer). |
| `varMemeberNos` | `const VARIANT FAR&` | [in] | Member Nos(Type: Long / Long Array). |

**Contract, including return/status semantics:** Assign Member Nos to a Storey in Chinese Steel Design. Parameters [in] varIndex Storey index(Type: Long/Integer). [in] varMemeberNos Member Nos(Type: Long / Long Array). Return values true Success. false General error. VBA Syntax See also OSDesignUI::ChineseSteelGetStoreyCount OSDesignUI::ChineseSteelAddStorey OSDesignUI::ChineseSteelDeleteStorey

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelAssignStorey(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelassigndissipated"></a>

### 33. ChineseSteelAssignDissipated

**Native references:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html#gac7b2874f7aa3bbb5ef2b4e6809b24930) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gac7b2874f7aa3bbb5ef2b4e6809b24930) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gac7b2874f7aa3bbb5ef2b4e6809b24930)<br>**Current:** Pending; D3.

**Managed proposal:** `DesignMutationResult AssignDissipated; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2024 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelAssignDissipated(const VARIANT FAR&varMemeberNos,const VARIANT FAR&varPlasticZone)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varMemeberNos` | `const VARIANT FAR&` | [in] | Member Nos(Type: Long / Long Array). |
| `varPlasticZone` | `const VARIANT FAR&` | [in] | Plastic Zone(Type: Long/Integer). [value table below] |

**Contract, including return/status semantics:** Assign Member Nos to plastic dissipated zone in Chinese Steel Design. Parameters [in] varMemeberNos Member Nos(Type: Long / Long Array). [in] varPlasticZone Plastic Zone(Type: Long/Integer). Value Plastic Zone 0 Start 1 End Return values true Success. false General error. VBA Syntax

**Value table 1** (source column groupings retained):

| Value | Plastic Zone |
| --- | --- |
| 0 | Start |
| 1 | End |

**2025:** Same declaration and non-example contract as the preceding documented version.

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelAssignDissipated(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelgetfireproofmaterialcount"></a>

### 34. ChineseSteelGetFireProofMaterialCount

**Native references:** 2024: absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gaf80f0f1aa9881542cdca646282ee1828) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gaf80f0f1aa9881542cdca646282ee1828)<br>**Current:** Pending; D5.

**Managed proposal:** `DesignQueryResult<int> GetFireProofMaterialCount; named solutionId/curveKind inputs where documented; distinguish valid empty count from failure.`

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetFireProofMaterialCount(const VARIANT FAR&varType)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varType` | `const VARIANT FAR&` | [in] | Fireproof Material Type(Type: Long/Integer). [value table below] |

**Contract, including return/status semantics:** get the Fireproof Material Count. Parameters [in] varType Fireproof Material Type(Type: Long/Integer). Value Fireproof Material Type 1 Concrete 2 Intumescent Coating 3 Nonintumescent Coating Return values 0 Get Fireproof Material Count Generate Error. other The total number of fireproof Material Count(Type: Long/Integer). VBA Syntax See also OSDesignUI::ChineseSteelGetFireProofMaterialName OSDesignUI::ChineseSteelAddConcreteData OSDesignUI::ChineseSteelAddNonIntumescentCoating OSDesignUI::ChineseSteelAddIntumescentCoating OSDesignUI::ChineseSteelDeleteFireProofMaterial

**Value table 1** (source column groupings retained):

| Value | Fireproof Material Type |
| --- | --- |
| 1 | Concrete |
| 2 | Intumescent Coating |
| 3 | Nonintumescent Coating |

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelGetFireProofMaterialCount(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelgetfireproofmaterialname"></a>

### 35. ChineseSteelGetFireProofMaterialName

**Native references:** 2024: absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga5c494793d911ff30700c617e4f1452f4) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga5c494793d911ff30700c617e4f1452f4)<br>**Current:** Pending; D5.

**Managed proposal:** `DesignQueryResult<T> GetFireProofMaterialName; T is a named solution/settings/check-items record, string name or typed parameter value according to the outputs below; no public native holder.`

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetFireProofMaterialName(const VARIANT FAR&varType,const VARIANT FAR&varFireproofIndex)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varType` | `const VARIANT FAR&` | [in] | Fireproof Material Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetFireProofMaterialCount . |
| `varFireproofIndex` | `const VARIANT FAR&` | [in] | The pireproof material index, start from 0(Type: Long/Integer). |

**Contract, including return/status semantics:** Get the Fireproof Material Name. Parameters [in] varType Fireproof Material Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetFireProofMaterialCount . [in] varFireproofIndex The pireproof material index, start from 0(Type: Long/Integer). Return values empty-string The varType or varFireproofIndex is invalid. others The Fireproof Material Name(Type: String). VBA Syntax See also OSDesignUI::ChineseSteelGetFireProofMaterialCount OSDesignUI::ChineseSteelAddConcreteData OSDesignUI::ChineseSteelAddNonIntumescentCoating OSDesignUI::ChineseSteelAddIntumescentCoating OSDesignUI::ChineseSteelDeleteFireProofMaterial

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelGetFireProofMaterialName(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteeladdconcretedata"></a>

### 36. ChineseSteelAddConcreteData

**Native references:** 2024: absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gac80cace58d2fdd6d0cca6b1d4eb997a3) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gac80cace58d2fdd6d0cca6b1d4eb997a3)<br>**Current:** Pending; D5.

**Managed proposal:** `DesignMutationResult AddConcreteData; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelAddConcreteData(const VARIANT FAR&varName,const VARIANT FAR&varDensity,const VARIANT FAR&varSpecificHeatCapacity,const VARIANT FAR&varHeatConduction)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varName` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `varDensity` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `varSpecificHeatCapacity` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `varHeatConduction` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `Name` | [in] | Fireproof Concrete Name(Type: String). |
| `Density` | [in] | Density of Concrete(Type: Double). |
| `SpecificHeatCapacity` | [in] | Specific Heat Capacity of Concrete(Type: Double). |
| `HeatConduction` | [in] | Heat Conduction factor of Concrete(Type: Double). |

**Contract, including return/status semantics:** Add the Fireproof Concrete Data. Parameters [in] Name Fireproof Concrete Name(Type: String). [in] Density Density of Concrete(Type: Double). [in] SpecificHeatCapacity Specific Heat Capacity of Concrete(Type: Double). [in] HeatConduction Heat Conduction factor of Concrete(Type: Double). Return values -1 Chinese Steel Design has not Initialized. 0 Add the Fireproof Concrete Data successful. VBA Syntax See also OSDesignUI::ChineseSteelGetFireProofMaterialCount OSDesignUI::ChineseSteelGetFireProofMaterialName OSDesignUI::ChineseSteelAddNonIntumescentCoating OSDesignUI::ChineseSteelAddIntumescentCoating OSDesignUI::ChineseSteelDeleteFireProofMaterial

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelAddConcreteData(System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteeladdnonintumescentcoating"></a>

### 37. ChineseSteelAddNonIntumescentCoating

**Native references:** 2024: absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga1db283accdcc2aed8e7448aa19ad2537) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga1db283accdcc2aed8e7448aa19ad2537)<br>**Current:** Pending; D5.

**Managed proposal:** `DesignMutationResult AddNonIntumescentCoating; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelAddNonIntumescentCoating(const VARIANT FAR&varName,const VARIANT FAR&varThickness,const VARIANT FAR&varHeatConductions)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varName` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `varThickness` | `const VARIANT FAR&` | [in] | Thicknesses(Type: Double Array). |
| `varHeatConductions` | `const VARIANT FAR&` | [in] | Heat Conduction factors(Type: Double Array). |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `Name` | [in] | Coating Name(Type: String). |

**Contract, including return/status semantics:** Add the Nonintumescent fireproof Coating Data. Parameters [in] Name Coating Name(Type: String). [in] varThickness Thicknesses(Type: Double Array). [in] varHeatConductions Heat Conduction factors(Type: Double Array). Return values -1 Generate error. 0 Add the Nonintumescent fireproof Coating Data successful. VBA Syntax See also OSDesignUI::ChineseSteelGetFireProofMaterialCount OSDesignUI::ChineseSteelGetFireProofMaterialName OSDesignUI::ChineseSteelAddConcreteData OSDesignUI::ChineseSteelAddIntumescentCoating OSDesignUI::ChineseSteelDeleteFireProofMaterial

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelAddNonIntumescentCoating(System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteeladdintumescentcoating"></a>

### 38. ChineseSteelAddIntumescentCoating

**Native references:** 2024: absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga1ae88a648e5a1307bf70ea2cf7533948) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga1ae88a648e5a1307bf70ea2cf7533948)<br>**Current:** Pending; D5.

**Managed proposal:** `DesignMutationResult AddIntumescentCoating; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelAddIntumescentCoating(const VARIANT FAR&varName,const VARIANT FAR&varThickness,const VARIANT FAR&varHeatThermalResistances)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varName` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `varThickness` | `const VARIANT FAR&` | [in] | Thicknesses(Type: Double Array). |
| `varHeatThermalResistances` | `const VARIANT FAR&` | [in] | Heat Thermal Resistances(Type: Double Array). |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `Name` | [in] | Coating Name(Type: String). |

**Contract, including return/status semantics:** Add the Intumescent fireproof Coating Data. Parameters [in] Name Coating Name(Type: String). [in] varThickness Thicknesses(Type: Double Array). [in] varHeatThermalResistances Heat Thermal Resistances(Type: Double Array). Return values -1 Generate error. 0 Add the Intumescent fireproof Coating Data successful. VBA Syntax See also OSDesignUI::ChineseSteelGetFireProofMaterialCount OSDesignUI::ChineseSteelGetFireProofMaterialName OSDesignUI::ChineseSteelAddConcreteData OSDesignUI::ChineseSteelAddNonIntumescentCoating OSDesignUI::ChineseSteelDeleteFireProofMaterial

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelAddIntumescentCoating(System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteeldeletefireproofmaterial"></a>

### 39. ChineseSteelDeleteFireProofMaterial

**Native references:** 2024: absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga75982b349fdb882ab187ca9ccdf2f09c) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga75982b349fdb882ab187ca9ccdf2f09c)<br>**Current:** Pending; D5.

**Managed proposal:** `DesignMutationResult DeleteFireProofMaterial; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelDeleteFireProofMaterial(const VARIANT FAR&type,const VARIANT FAR&varName)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `type` | `const VARIANT FAR&` | [in] | Fireproof Material Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetFireProofMaterialCount . |
| `varName` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `Name` | [in] | Fireproof Material Name(Type: String). |

**Contract, including return/status semantics:** Delete the Fireproof Material. Parameters [in] type Fireproof Material Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetFireProofMaterialCount . [in] Name Fireproof Material Name(Type: String). Return values false Generate Error. true Delete the Fireproof Material successful. VBA Syntax See also OSDesignUI::ChineseSteelGetFireProofMaterialName OSDesignUI::ChineseSteelAddConcreteData OSDesignUI::ChineseSteelAddNonIntumescentCoating OSDesignUI::ChineseSteelAddIntumescentCoating OSDesignUI::ChineseSteelDeleteFireProofMaterial

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelDeleteFireProofMaterial(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelgetcurvecount"></a>

### 40. ChineseSteelGetCurveCount

**Native references:** 2024: absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga7d1d7c7374eeb6802eed60badf43ee8c) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga7d1d7c7374eeb6802eed60badf43ee8c)<br>**Current:** Pending; D5.

**Managed proposal:** `DesignQueryResult<int> GetCurveCount; named solutionId/curveKind inputs where documented; distinguish valid empty count from failure.`

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetCurveCount(const VARIANT FAR&varCurveType)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varCurveType` | `const VARIANT FAR&` | [in] | User Define Curve Type(Type: Long/Integer). [value table below] |

**Contract, including return/status semantics:** Get the User Define Curve Count. Parameters [in] varCurveType User Define Curve Type(Type: Long/Integer). Value User Define Curve Type 0 Hot Smoke Curve 1 Temperature Rise Curve 2 Thermal Material Property Factor Curve Return values 0 Generate Error or there is no specified type curve. other The count of user defined curve(Type: Long/Integer). VBA Syntax See also OSDesignUI::ChineseSteelGetCurveName OSDesignUI::ChineseSteelDeleteCurve OSDesignUI::ChineseSteelAddHotSmokeCurveData OSDesignUI::ChineseSteelAddTemperatureRiseCurveData OSDesignUI::ChineseSteelAddThermalSteelPropertyCurveData

**Value table 1** (source column groupings retained):

| Value | User Define Curve Type |
| --- | --- |
| 0 | Hot Smoke Curve |
| 1 | Temperature Rise Curve |
| 2 | Thermal Material Property Factor Curve |

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelGetCurveCount(System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteelgetcurvename"></a>

### 41. ChineseSteelGetCurveName

**Native references:** 2024: absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gaff17ac3918f078c6d2233a12e99d35d3) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gaff17ac3918f078c6d2233a12e99d35d3)<br>**Current:** Pending; D5.

**Managed proposal:** `DesignQueryResult<T> GetCurveName; T is a named solution/settings/check-items record, string name or typed parameter value according to the outputs below; no public native holder.`

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelGetCurveName(const VARIANT FAR&varCurveType,const VARIANT FAR&varCurveIndex)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varCurveType` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `varCurveIndex` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `curveType` | [in] | User Define Curve Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetCurveCount . |
| `index` | [in] | The User Define Curve index, start from 0(Type: Long/Integer). |

**Contract, including return/status semantics:** Get the User Define Curve Name. Parameters [in] curveType User Define Curve Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetCurveCount . [in] index The User Define Curve index, start from 0(Type: Long/Integer). Return values empty-string The varCurveType or varCurveIndex is invalid. Returns The User Define Curve Name. VBA Syntax See also OSDesignUI::ChineseSteelGetCurveCount OSDesignUI::ChineseSteelDeleteCurve OSDesignUI::ChineseSteelAddHotSmokeCurveData OSDesignUI::ChineseSteelAddTemperatureRiseCurveData OSDesignUI::ChineseSteelAddThermalSteelPropertyCurveData

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelGetCurveName(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteeldeletecurve"></a>

### 42. ChineseSteelDeleteCurve

**Native references:** 2024: absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga1669cee47f7396cc61ce8ca1bb39eba6) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga1669cee47f7396cc61ce8ca1bb39eba6)<br>**Current:** Pending; D5.

**Managed proposal:** `DesignMutationResult DeleteCurve; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelDeleteCurve(const VARIANT FAR&varCurveType,const VARIANT FAR&varName)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varCurveType` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `varName` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `curveType` | [in] | User Define Curve Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetCurveCount . |
| `Name` | [in] | User Define Curve Name(Type: String). |

**Contract, including return/status semantics:** Delete the User Define Curve. Parameters [in] curveType User Define Curve Type(Type: Long/Integer), please reference OSDesignUI::ChineseSteelGetCurveCount . [in] Name User Define Curve Name(Type: String). Return values -1 Generate Error. 0 Delete the User Define Curve successful. VBA Syntax See also OSDesignUI::ChineseSteelGetCurveCount OSDesignUI::ChineseSteelGetCurveName OSDesignUI::ChineseSteelAddHotSmokeCurveData OSDesignUI::ChineseSteelAddTemperatureRiseCurveData OSDesignUI::ChineseSteelAddThermalSteelPropertyCurveData

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelDeleteCurve(System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteeladdhotsmokecurvedata"></a>

### 43. ChineseSteelAddHotSmokeCurveData

**Native references:** 2024: absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga2f6c560c157d426e516a60e0775033c4) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga2f6c560c157d426e516a60e0775033c4)<br>**Current:** Pending; D5.

**Managed proposal:** `DesignMutationResult AddHotSmokeCurveData; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelAddHotSmokeCurveData(const VARIANT FAR&varName,const VARIANT FAR&varTimes,const VARIANT FAR&varTemperatures)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varName` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `varTimes` | `const VARIANT FAR&` | [in] | Times(Type: Double Array, Unit: minute). |
| `varTemperatures` | `const VARIANT FAR&` | [in] | Temperatures(Type: Double Array., Unit:Degree). |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `Name` | [in] | Hot Smoke Curve Name(Type: String). |

**Contract, including return/status semantics:** Add the Add Hot Smoke Curve Data. Parameters [in] Name Hot Smoke Curve Name(Type: String). [in] varTimes Times(Type: Double Array, Unit: minute). [in] varTemperatures Temperatures(Type: Double Array., Unit:Degree). Return values -1 Generate error. 0 Add the Add Hot Smoke Curve Data successful. VBA Syntax See also OSDesignUI::ChineseSteelGetCurveCount OSDesignUI::ChineseSteelGetCurveName OSDesignUI::ChineseSteelDeleteCurve OSDesignUI::ChineseSteelAddTemperatureRiseCurveData OSDesignUI::ChineseSteelAddThermalSteelPropertyCurveData

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelAddHotSmokeCurveData(System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteeladdtemperaturerisecurvedata"></a>

### 44. ChineseSteelAddTemperatureRiseCurveData

**Native references:** 2024: absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#gac12e447eadc75710b58060b3cf1de279) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#gac12e447eadc75710b58060b3cf1de279)<br>**Current:** Pending; D5.

**Managed proposal:** `DesignMutationResult AddTemperatureRiseCurveData; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelAddTemperatureRiseCurveData(const VARIANT FAR&varName,const VARIANT FAR&varTimes,const VARIANT FAR&varTemperatures)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varName` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `varTimes` | `const VARIANT FAR&` | [in] | Times(Type: Double Array, Unit: minute). |
| `varTemperatures` | `const VARIANT FAR&` | [in] | Temperatures(Type: Double Array., Unit:Degree). |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `Name` | [in] | Member Temperature Rise Curve Name(Type: String). |

**Contract, including return/status semantics:** Add the Add Member Temperature Rise Curve Data. Parameters [in] Name Member Temperature Rise Curve Name(Type: String). [in] varTimes Times(Type: Double Array, Unit: minute). [in] varTemperatures Temperatures(Type: Double Array., Unit:Degree). Return values -1 Generate error. 0 Add Member Temperature Rise Data successful. VBA Syntax See also OSDesignUI::ChineseSteelGetCurveCount OSDesignUI::ChineseSteelGetCurveName OSDesignUI::ChineseSteelDeleteCurve OSDesignUI::ChineseSteelAddHotSmokeCurveData OSDesignUI::ChineseSteelAddThermalSteelPropertyCurveData

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelAddTemperatureRiseCurveData(System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

<a id="design-chinesesteeladdthermalsteelpropertycurvedata"></a>

### 45. ChineseSteelAddThermalSteelPropertyCurveData

**Native references:** 2024: absent / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html#ga6890e7076be246370638b04340bfd5ed) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html#ga6890e7076be246370638b04340bfd5ed)<br>**Current:** Pending; D5.

**Managed proposal:** `DesignMutationResult AddThermalSteelPropertyCurveData; use typed request classes for grouped settings, material properties and curve samples, otherwise named scalar/enum and IReadOnlyList<int> memberIds inputs.`

#### 2025 contract

~~~cpp
VARIANT OSDesignUI::ChineseSteelAddThermalSteelPropertyCurveData(const VARIANT FAR&varName,const VARIANT FAR&varTemperatures,const VARIANT FAR&varStrengthReduces,const VARIANT FAR&varElasticityReduces)
~~~

| Native parameter | Declared type | Logical direction | Parameter meaning / constraints |
| --- | --- | --- | --- |
| `varName` | `const VARIANT FAR&` | Unmatched; reconcile below | No exact parameter-table match; see the separately preserved parameter prose. |
| `varTemperatures` | `const VARIANT FAR&` | [in] | Temperatures(Type: Double Array., Unit:Degree). |
| `varStrengthReduces` | `const VARIANT FAR&` | [in] | Strength Reduce factors(Type: Double Array). |
| `varElasticityReduces` | `const VARIANT FAR&` | [in] | Elasticity Reduce factors(Type: Double Array). |

**Parameter-table aliases or discrepancies:**

| Documented name | Direction | Meaning |
| --- | --- | --- |
| `Name` | [in] | Member Temperature Rise Curve Name(Type: String). |

**Contract, including return/status semantics:** Add the Add Thermal Material Property Factor Curve Data. Parameters [in] Name Member Temperature Rise Curve Name(Type: String). [in] varTemperatures Temperatures(Type: Double Array., Unit:Degree). [in] varStrengthReduces Strength Reduce factors(Type: Double Array). [in] varElasticityReduces Elasticity Reduce factors(Type: Double Array). Return values -1 Generate error. 0 Add Thermal Material Property Factor Curve Data successful. VBA Syntax See also OSDesignUI::ChineseSteelGetCurveCount OSDesignUI::ChineseSteelGetCurveName OSDesignUI::ChineseSteelDeleteCurve OSDesignUI::ChineseSteelAddHotSmokeCurveData OSDesignUI::ChineseSteelAddTemperatureRiseCurveData

**2026:** Same declaration and non-example contract as the preceding documented version.

**2025/2026 inspected automation signature:** `System.Object ChineseSteelAddThermalSteelPropertyCurveData(System.Object, System.Object, System.Object, System.Object)`. The full parameter metadata is in the comparison JSON.

**Main implementation:** No call target in the reviewed main adapter.

**Acceptance:** Verify the exact native status convention before publishing data; validate array dimensions, units, enum values and ID/index bases from this contract. Preserve native failure and transport exceptions separately. No untouched output buffer may become successful data. For native void, record dispatch completion only unless an explicit read-back verifies the effect.

## 4. Complete private-source overload mapping

| Main managed signature | Return | Native targets / delegation | Reference |
| --- | --- | --- | --- |
| `CreateDesignBrief(StaadDesignCode designCode)` | `int` | `CreateDesignBrief` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSDesign.cs#L14) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSDesignAdapter.cs#L36) |
| `AssignDesignCommand(int designBriefId, IDesignCommand designCommand)` | `IOSDesign` | `AssignDesignCommand` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSDesign.cs#L15) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSDesignAdapter.cs#L121) |
| `AssignDesignCommands(int designBriefId, IEnumerable<IDesignCommand> designCommands)` | `IOSDesign` | `AssignDesignCommand` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSDesign.cs#L16) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSDesignAdapter.cs#L41) |
| `AssignDesignParameter(int designBriefId, IDesignParameter designParameter)` | `IOSDesign` | `AssignDesignParameter` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSDesign.cs#L17) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSDesignAdapter.cs#L91) |
| `AssignDesignParameters(StaadDesignCode designCode, IEnumerable<IDesignParameter> designParameters)` | `IOSDesign` | `CreateDesignBrief`, `AssignDesignParameter` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSDesign.cs#L18) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSDesignAdapter.cs#L57) |
| `AssignDesignParameters(int designBriefId, IEnumerable<IDesignParameter> designParameters)` | `IOSDesign` | `AssignDesignParameter` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSDesign.cs#L19) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSDesignAdapter.cs#L75) |
| `GetMemberDesignParameters(int designBriefId, int memberId)` | `MemberDesignParametersResult` | `GetMemberDesignParameters` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSDesign.cs#L27) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSDesignAdapter.cs#L153) |

Wrapper and ComObject properties are binding contracts, not domain methods. Constructors and private helpers are not counted as public overloads. Existing source signatures above are comparison evidence, not approved signatures for a new public port.

## 5. Passive metadata, constants and fingerprints

2025 and 2026 IOSDesignUI method signatures, parameter metadata and referenced enum constants match. Both assemblies have the same identity but different binary hashes; each was inspected in a separate reflection-only process. No native object was activated. The expected 2024 STAAD interop DLL was unavailable for passive inspection.

### OpenSTAADUI.DesignCodes

| Constant | Value |
| --- | --- |
| `STEELDESIGNCODEUNKNOWN` | 0 |
| `STEELDESIGNAASHTO` | 1001 |
| `STEELDESIGNAISC` | 1002 |
| `STEELDESIGNAUSTRALIAN` | 1003 |
| `STEELDESIGNBS5950` | 1004 |
| `STEELDESIGNBS5400` | 1005 |
| `STEELDESIGNCANADIAN` | 1006 |
| `STEELDESIGNFRENCH` | 1007 |
| `STEELDESIGNDIN18800` | 1008 |
| `STEELDESIGNIS800` | 1009 |
| `STEELDESIGNJAPANESE` | 1010 |
| `STEELDESIGNLRFD` | 1011 |
| `STEELDESIGNNS3472` | 1012 |
| `STEELDESIGNSPANISH` | 1013 |
| `STEELDESIGNNPD` | 1014 |
| `STEELDESIGNBSK90` | 1015 |
| `STEELDESIGNAPI` | 1016 |
| `STEELDESIGNEC3` | 1017 |
| `STEELDESIGNCHINESE` | 1018 |
| `STEELDESIGNDS412` | 1019 |
| `STEELDESIGNASCE` | 1020 |
| `STEELDESIGNB7` | 1021 |
| `STEELDESIGNBSK99` | 1022 |
| `STEELDESIGNDUTCH` | 1023 |
| `STEELDESIGNCYPRUS` | 1024 |
| `STEELDESIGNRUSSIAN` | 1025 |
| `STEELDESIGNAISI` | 1026 |
| `STEELDESIGNS136` | 1027 |
| `STEELDESIGNIS801` | 1028 |
| `STEELDESIGNIS802` | 1029 |
| `STEELDESIGNMEXICANLRFD` | 1030 |
| `STEELDESIGNEGYPTIAN` | 1031 |
| `STEELDESIGNIS800LSD` | 1032 |
| `STEELDESIGNIS800WSD` | 1052 |
| `STEELDESIGNBS5950_COLD` | 1033 |
| `STEELDESIGNSOUTHAFRICAN` | 1034 |
| `STEELDESIGNAISC_RECO` | 1035 |
| `STEELDESIGNCANADIANRECO_1994` | 1036 |
| `STEELDESIGNCANADIANRECO_2001` | 1037 |
| `STEELDESIGNAISI_2001RCECO` | 1038 |
| `STEELDESIGNAISI_1999RCECO` | 1039 |
| `STEELDESIGNAISI_1996RCECO` | 1040 |
| `STEELDESIGNS136_2001RCECO` | 1041 |
| `STEELDESIGNS136_1999RCECO` | 1042 |
| `STEELDESIGNS136_1996RCECO` | 1043 |
| `STEELDESIGNAASHTO_LRFD` | 1044 |
| `STEELDESIGNAISC_UNIFIED` | 1045 |
| `STEELDESIGNNF3000_1989` | 1046 |
| `STEELDESIGNNF3000_1998` | 1047 |
| `STEELDESIGNNF3000_1974` | 1048 |
| `STEELDESIGNNF3000_1977` | 1049 |
| `STEELDESIGNNORSOK` | 1050 |
| `STEELDESIGNNF3000_2004` | 1051 |
| `STEELDESIGNNF3000_2001` | 1053 |
| `STEELDESIGNASCE52` | 1060 |
| `STEELDESIGNAISC_UNIFIED_2010` | 1061 |
| `STEELDESIGNCANADIANS16_09` | 1062 |
| `STEELDESIGNRUSSIAN2011` | 1063 |
| `STEELDESIGNSOUTHAFRICAN1993` | 1064 |
| `STEELDESIGNCANADIANS16_14` | 1065 |
| `STEELDESIGNNZS3404_1997` | 1066 |
| `STEELDESIGNAISC_UNIFIED_2016` | 1067 |
| `STEELDESIGNAISI_S100_2016` | 1068 |
| `STEELDESIGNCANADIANS16_19` | 1069 |
| `STEELDESIGNAISC_CAST` | 1102 |
| `STEELDESIGNBS5950_1990` | 1104 |
| `STEELDESIGNLRFD_CAST` | 1111 |
| `STEELDESIGNAISC_N690` | 1202 |
| `STEELDESIGNEC3BRITISH` | 1203 |
| `STEELDESIGNAISCN690_1984` | 1204 |
| `STEELDESIGNJAPANESE2005` | 1210 |
| `STEELDESIGNEN1993` | 1220 |
| `STEELDESIGNRUSSIAN2017` | 1221 |
| `EARTHQUAKEDESIGNEC8` | 1230 |
| `CONCRETEDESIGNACI` | 2001 |
| `CONCRETEDESIGNBS8110` | 2002 |
| `CONCRETEDESIGNBS8007` | 2003 |
| `CONCRETEDESIGNCANADIAN` | 2004 |
| `CONCRETEDESIGNFRENCH` | 2005 |
| `CONCRETEDESIGNDIN1045` | 2006 |
| `CONCRETEDESIGNINDIAN` | 2007 |
| `CONCRETEDESIGNJAPANESE` | 2008 |
| `CONCRETEDESIGNNS3473` | 2009 |
| `CONCRETEDESIGNEC2` | 2010 |
| `CONCRETEDESIGNCHINESE` | 2011 |
| `CONCRETEDESIGNSPANISH` | 2012 |
| `CONCRETEDESIGNDUTCH` | 2013 |
| `CONCRETEDESIGNCYPRUS` | 2014 |
| `CONCRETEDESIGNRUSSIAN` | 2015 |
| `CONCRETEDESIGNAUSTRALIAN` | 2016 |
| `CONCRETEDESIGNSWEDISH` | 2017 |
| `CONCRETEDESIGNFINNISH` | 2018 |
| `CONCRETEDESIGNIS13920` | 2019 |
| `CONCRETEDESIGNMEXICAN` | 2020 |
| `CONCRETEDESIGNDANISH` | 2021 |
| `CONCRETEDESIGNSOUTHAFRICAN` | 2022 |
| `CONCRETEDESIGNCP65` | 2023 |
| `CONCRETEDESIGNACI1999` | 2024 |
| `CONCRETEDESIGNACI2002` | 2025 |
| `CONCRETEDESIGNACI2005` | 2026 |
| `CONCRETEDESIGNACI2008` | 2027 |
| `CONCRETEDESIGNRUSSIAN2012` | 2028 |
| `CONCRETEDESIGNACI2011` | 2029 |
| `CONCRETEDESIGNIS13920_1993` | 2030 |
| `TIMBERDESIGN` | 3001 |
| `AITCTIMBERDESIGN` | 3002 |
| `EC5TIMBERDESIGN` | 3003 |
| `CANADIANTIMBERDESIGN` | 3004 |
| `ALUMINUMDESIGN` | 4001 |
| `CANADIANALUMINUMDESIGN` | 4002 |
| `FOOTINGDESIGNAMERICAN` | 5001 |
| `SHEARWALLDESIGNACI` | 6001 |
| `SHEARWALLDESIGNBS8110` | 6002 |
| `SHEARWALLDESIGNINDIAN` | 6003 |

Sentinel/end markers are not valid user selections merely because they are enum members.

### OpenSTAADUI.IOSMemberSteelDgnParams auxiliary holder

| Property declaration |
| --- |
| `Int32 Count` |
| `Int32 MemberNumber` |
| `System.String Name [Int32]` |
| `System.String Value [Int32]` |
| `System.String Unit [Int32]` |
| `System.String Description [Int32]` |
| `Int32 Default [Int32]` |

Keep these COM-facing holders internal; expose owned managed snapshots. The complete passive methods and GUID observations for both years are in JSON.

### OpenSTAADUI.OSMemberSteelDgnParams auxiliary holder

| Property declaration |
| --- |


Keep these COM-facing holders internal; expose owned managed snapshots. The complete passive methods and GUID observations for both years are in JSON.

### OpenSTAADUI.IOSChineseSteelDgnResults auxiliary holder

| Property declaration |
| --- |
| `Int32 Count` |
| `Double Ratio [Int32]` |
| `Double Limit [Int32]` |
| `Double Actual [Int32]` |
| `Int32 LoadCase [Int32]` |
| `Double Position [Int32]` |
| `System.String CheckItemString [Int32]` |
| `Int32 CheckItem [Int32]` |

Keep these COM-facing holders internal; expose owned managed snapshots. The complete passive methods and GUID observations for both years are in JSON.

### OpenSTAADUI.OSChineseSteelDgnResults auxiliary holder

| Property declaration |
| --- |


Keep these COM-facing holders internal; expose owned managed snapshots. The complete passive methods and GUID observations for both years are in JSON.

### OpenSTAADUI.IOSChineseSteelDgnParameters auxiliary holder

| Property declaration |
| --- |
| `Int32 CodeType` |
| `System.String Name` |
| `Int32 MemberType` |
| `Int32 SectionSlendernessRatioGrade` |
| `Double GamaReStr` |
| `Double GamaReSta` |
| `Int32 SeismicLevel` |
| `Double LmdCompressed` |
| `Double LmdTension` |
| `Double GamaZ` |
| `Double GamaY` |
| `Double GamaSharp` |
| `Double Betamz` |
| `Double Betamy` |
| `Double Betatz` |
| `Double Betaty` |
| `Int32 DJ1` |
| `Int32 DJ2` |
| `Double Fabz` |
| `Double Faby` |
| `Double Faz` |
| `Double Fay` |
| `Double Lz` |
| `Double Ly` |
| `Double Miuz` |
| `Double Miuy` |
| `Double K1z` |
| `Double K2z` |
| `Double K1y` |
| `Double K2y` |
| `Double RFlange` |
| `Double RWeb` |
| `Int32 SectionTypeZ` |
| `Int32 SectionTypeY` |
| `Int32 ThinWallBearingStatus` |

Keep these COM-facing holders internal; expose owned managed snapshots. The complete passive methods and GUID observations for both years are in JSON.

### OpenSTAADUI.OSChineseSteelDgnParameters auxiliary holder

| Property declaration |
| --- |


Keep these COM-facing holders internal; expose owned managed snapshots. The complete passive methods and GUID observations for both years are in JSON.

### OpenSTAADUI.IOSChineseSteelCheckItem auxiliary holder

| Property declaration |
| --- |
| `Int32 CodeItem_BeamBendingStrength` |
| `Int32 CodeItem_BeamShearStrength` |
| `Int32 CodeItem_BeamEquivalentStress` |
| `Int32 CodeItem_BeamOverallStability` |
| `Int32 CodeItem_BeamSlendernessWeb` |
| `Int32 CodeItem_BeamSlendernessFlange` |
| `Int32 CodeItem_TrussStrength` |
| `Int32 CodeItem_TrussStability` |
| `Int32 CodeItem_TrussShearStrength` |
| `Int32 CodeItem_ColumnStrength` |
| `Int32 CodeItem_ColumnStabilityMzMy` |
| `Int32 CodeItem_ColumnStabilityMyMz` |
| `Int32 CodeItem_PressedTrussSlenderness` |
| `Int32 CodeItem_TensionTrussSlenderness` |
| `Int32 CodeItem_ColumnSlendernessFlange` |
| `Int32 CodeItem_ColumnSlendernessWeb` |
| `Int32 CodeItem_BeamDeflection` |
| `Int32 CodeItem_Performance_MinFactor` |
| `Int32 CodeItem_Performance_BeamAxialForce` |
| `Int32 CodeItem_Performance_BeamShear` |
| `Int32 CodeItem_Performance_Lambda` |
| `Int32 CodeItem_Performance_WebSlenderness` |
| `Int32 CodeItem_Performance_FlangeSlenderness` |
| `Int32 CodeItem_Performance_TrussNetArea` |
| `Int32 CodeItem_Performance_DissipatedLen` |
| `Int32 CodeItem_Performance_StrongColumn` |
| `Int32 CodeItem_ThinPressedFlakiness` |
| `Int32 CodeItem_ThinLipFlakiness` |
| `Int32 GB51249_FrameBeamStrength` |
| `Int32 GB51249_FrameColumnStability` |
| `Int32 GB51249_FireproofThickness` |

Keep these COM-facing holders internal; expose owned managed snapshots. The complete passive methods and GUID observations for both years are in JSON.

### OpenSTAADUI.OSChineseSteelCheckItem auxiliary holder

| Property declaration |
| --- |


Keep these COM-facing holders internal; expose owned managed snapshots. The complete passive methods and GUID observations for both years are in JSON.

### OpenSTAADUI.IOSChineseSteelCheckOption auxiliary holder

| Property declaration |
| --- |
| `System.String SecondMembers` |
| `Double Angle1` |
| `Double Angle2` |
| `Double Angle3` |
| `Int32 UseDefaultParameter` |
| `Int32 DesignRange` |
| `Int32 AllSolutions` |
| `Int32 DesignSolutionNo` |
| `Int32 SeimicGrade` |
| `Int32 OptimizeGroup` |
| `Int32 OptimizeFast` |
| `Int32 OptimizeIteration` |
| `Int32 OptimizeSectionSortType` |
| `Int32 OptimizeLimitWeightLoss` |
| `Double OptimizeLimitWeightLossRatio` |
| `Int32 OptimizeKeepSectionSlenderness` |
| `System.String ForceLoadCase` |
| `System.String DisplacementLoadCase` |
| `Int32 PerformanceDesign` |
| `Int32 PerformanceStoreyNo` |
| `Int32 PerformanceGravityNo` |
| `Int32 PerformanceSeismicHXNo` |
| `Int32 PerformanceSeismicHXPositive` |
| `Int32 PerformanceSeismicHXNegative` |
| `Int32 PerformanceSeismicHZNo` |
| `Int32 PerformanceSeismicHZPositive` |
| `Int32 PerformanceSeismicHZNegative` |
| `Int32 PerformanceSeismicVNo` |
| `Int32 PerformanceSeismicVPositive` |
| `Int32 PerformanceSeismicVNegative` |
| `Int32 GB51249BearingCapacity` |
| `Int32 GB51249CriticalTemperature` |
| `Int32 GB51249GenerateReducedEModel` |

Keep these COM-facing holders internal; expose owned managed snapshots. The complete passive methods and GUID observations for both years are in JSON.

### OpenSTAADUI.OSChineseSteelCheckOption auxiliary holder

| Property declaration |
| --- |


Keep these COM-facing holders internal; expose owned managed snapshots. The complete passive methods and GUID observations for both years are in JSON.

| Year | Assembly SHA-256 |
| --- | --- |
| 2025 | CC724993AC9119138B7323E8C684997257E2851DE62E1551BA5D6D67A9DADC19 |
| 2026 | 6D153E80ED356A1B167ABB2EF19227FCFF510707A578AD304154652CC6C1F6D4 |

### 2024 help evidence

| Page | SHA-256 | Summary / details |
| --- | --- | --- |
| [Design Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design.html) | fdcdd95fcdc4e18ac5a8f98576f6ff03db44e77121d33b06bef725bd2c4cd935 | 0 / 0 |
| [Design: Steel Design Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html) | cbb6c514c7619d96bdfb37681af8e848d73607a345da670ef183e373d98297d0 | 6 / 6 |
| [Design Steel: Chinese Steel Design Design: Steel](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html) | d37ecc3bcf5de50c635dee08a9b3e278fccb57dd8c606c16f5b90fc86669e6f0 | 25 / 25 |
| [Native class listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_design_u_i.html) | 353ea966a5371117f41f91232a0d6606c84fb657107007c7103ac8392dbb1d81 | 33 names |
| [Native header listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/_o_s_design_u_i_8h_source.html) | eb2af9ec960b082d697bf010cf300c91b84f69c026f262c5e057efbc33bfcdfb | Supporting declaration evidence |

### 2025 help evidence

| Page | SHA-256 | Summary / details |
| --- | --- | --- |
| [Design Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design.html) | 1d717263e5990da860f13a907b4630b2297e52d3bf0c338a74368bf00b1cca18 | 0 / 0 |
| [Design: Steel Design Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html) | 5e32a0960a7c1b90781e1533a3d9744d166f1c53bd68c4abe4b896fe58d28bfe | 6 / 6 |
| [Design Steel: Chinese Steel Design Design Results › Design: Steel](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html) | 7f96d2b8a997eea83e08253ee032e23599fc009584831b4e0a5841a0bec923b2 | 39 / 39 |
| [Native class listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_design_u_i.html) | d3558f6c53c50dc5098c444079ccd88bca12518dffc84f3223018cefa4714162 | 47 names |
| [Native header listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/_o_s_design_u_i_8h_source.html) | f1bd1efbe9f78d5c65cea895fdddfe5cc2342747dae17ac0fd10b77dbd7c5feb | Supporting declaration evidence |

### 2026 help evidence

| Page | SHA-256 | Summary / details |
| --- | --- | --- |
| [Design Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design.html) | 6abedff8986a03e7c23988a517c6bbf47d4ef1d622d83cdcfcf4e1515f0fc99e | 0 / 0 |
| [Design: Steel Design Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html) | f179af4eb90bd78f772aaa3fd75318f57138522b1636db334056972b752045a4 | 6 / 6 |
| [Design Steel: Chinese Steel Design Design Results › Design: Steel](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html) | cc3a4c98cbc1eeaa0f354b45df4cef2c2ec9b75eb16c9a272f02320403d11985 | 39 / 39 |
| [Native class listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_design_u_i.html) | f7a2cd10677394b0cee42fe46202d698917140d7df22ed851c8963a17cff025c | 47 names |
| [Native header listing](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/_o_s_design_u_i_8h_source.html) | d4fff53f55b85ef62f8543ada6dad444a6eb0b00469187b8cb8d5a9e74dd6bbd | Supporting declaration evidence |
