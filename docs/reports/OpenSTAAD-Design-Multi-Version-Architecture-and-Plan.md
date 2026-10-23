# OpenSTAAD Design multi-version comparison, architecture and implementation plan

**Reviewed:** 23 October 2026<br>
**Baseline:** main at 1d8285ce66c95c60da55530d4c721fb7274893da<br>
**Status:** Documentation and architecture only. Design domain methods remain pending.

## 1. Findings and scope

The installed references contain **31 Design-owned functions in 2024 and 45 in both 2025 and 2026**. Four existing Chinese steel result/report declarations lose their design-code argument in 2025. Fourteen functions are added for check items, fireproof materials and thermal curves. The parameter-selector catalog also changes substantially. Design therefore needs a shared implementation with narrow, evidence-backed compatibility policies.

The current IOSDesign / OSDesignAdapter capture the native binding but implement no Design operations. The main source exposes seven managed overloads targeting only four native functions. Its fluent assignment functions and reflection-based parameter formatting need revision before porting. The other 41 native names require implementations beyond main-adapter parity.

Output-owned steel results appearing under the Design steel help page belong to the existing Output comparison. They are not counted again as Design-owned operations here.

| Measure | Finding |
| --- | --- |
| Native names (2024 / 2025 / 2026) | 31 / 45 / 45 |
| Main interface / public adapter overloads | 7 / 7 |
| Main private adapter helpers | 0 |
| Main distinct native targets | 4 |
| Native names without main-adapter target | 41 |
| Current typed domain methods | 0 (binding-only placeholder) |
| Common signature changes, 2024 to 2025 | 4 |
| Non-example text changes, 2024 to 2025 | 7 |
| Example changes, 2024 to 2025 | 30 |
| Observed changes, 2025 to 2026 | 0 |
| Native class names / excluded helpers (2026) | 47 / 2 |


## 2. References and verification method

| Version / installed help | Documented build |
| --- | --- |
| [STAAD.Pro 2024 Design](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design.html) | 24.00.02.354 |
| [STAAD.Pro 2025 Design](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design.html) | 25.00.01.424 |
| [STAAD.Pro 2026 Design](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design.html) | 26.00.00.340 |

Read-only source: [main interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSDesign.cs) and [main adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSDesignAdapter.cs).

The [complete inventory](OpenSTAAD-Design-API-Coverage-and-Contract-Inventory.md) includes each declaration, parameter table, return convention, value table, source overload and passive metadata. The [comparison JSON](data/OpenSTAAD-Design-2024-2026-Comparison.json) preserves all per-year contracts, differences and source/help fingerprints.

All three pages in the Design group tree were reviewed for each version, followed by a scan of the other group pages for OSDesignUI ownership. Summary and detailed entries were reconciled. The native class and header were checked separately. Constructors, OnFinalRelease and SetStructure are implementation details, not public design APIs.

2025 and 2026 IOSDesignUI metadata and the relevant holder interfaces were inspected in separate reflection-only processes. The two vendor assemblies have matching Design method metadata and referenced enum values. Neither COM activation nor a native design run was performed. The absence of the expected 2024 interop DLL limits ABI verification for that year.

## 3. Complete cross-version function matrix

Every nondeprecated row below remains pending as a typed managed API. Source call presence is comparison evidence, not verified engineering behavior.

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


## 4. Differences and their implementation consequences

| Native name | 24 → 25 observation | 25 → 26 |
| --- | --- | --- |
| `AssignDesignCommand` | Changed non-example text | Unchanged |
| `AssignDesignGroup` | Changed non-example text | Unchanged |
| `ChineseSteelGetDesignParameter` | Changed non-example text | Unchanged |
| `ChineseSteelGetCheckItem` | New name | Unchanged |
| `ChineseSteelSetCheckItem` | New name | Unchanged |
| `ChineseSteelGetCheckOption` | Changed non-example text | Unchanged |
| `ChineseSteelSetCheckOption` | Changed non-example text | Unchanged |
| `ChineseSteelGetResults` | Changed declaration; Changed non-example text | Unchanged |
| `ChineseSteelGetCodeItemName` | Changed non-example text | Unchanged |
| `ChineseSteelGetMaxResult` | Changed declaration | Unchanged |
| `ChineseSteelGetResultParameters` | Changed declaration | Unchanged |
| `ChineseSteelDisplayMemberReport` | Changed declaration | Unchanged |
| `ChineseSteelGetFireProofMaterialCount` | New name | Unchanged |
| `ChineseSteelGetFireProofMaterialName` | New name | Unchanged |
| `ChineseSteelAddConcreteData` | New name | Unchanged |
| `ChineseSteelAddNonIntumescentCoating` | New name | Unchanged |
| `ChineseSteelAddIntumescentCoating` | New name | Unchanged |
| `ChineseSteelDeleteFireProofMaterial` | New name | Unchanged |
| `ChineseSteelGetCurveCount` | New name | Unchanged |
| `ChineseSteelGetCurveName` | New name | Unchanged |
| `ChineseSteelDeleteCurve` | New name | Unchanged |
| `ChineseSteelAddHotSmokeCurveData` | New name | Unchanged |
| `ChineseSteelAddTemperatureRiseCurveData` | New name | Unchanged |
| `ChineseSteelAddThermalSteelPropertyCurveData` | New name | Unchanged |


### 4.1. Removed argument in four existing functions

ChineseSteelGetResults, ChineseSteelGetMaxResult, ChineseSteelGetResultParameters and ChineseSteelDisplayMemberReport take an explicit varCodeType in their 2024 declarations. That argument is absent from the 2025/2026 declarations and inspected metadata. Some later parameter prose still mentions it.

Keep a stable managed ChineseSteelMemberQuery with solutionId, memberId and an explicit or safely resolved design-code context. An internal legacy policy supplies the code argument for a qualified 2024 binding; a later policy uses the shorter signature. Validate the requested code against the solution when possible. A missing code on a 2024 path must produce a diagnostic rather than a guessed default.

Do not probe arity by invoking a mutation or a report repeatedly. Inspect dispatch/type information non-destructively, combine that observation with the captured native version, and qualify the exact invocation shapes on disposable models. A known year alone cannot settle a contradiction between help and the acquired automation interface.

### 4.2. Parameter-selector IDs are not a stable enum

ChineseSteelGetDesignParameter lists **107 selector rows (0–106)** in 2024 and **136 (0–135)** in 2025/2026. Code Type is introduced at zero; existing fields shift. Later references also repeat the label “Gyration Radius Calculation Of Single Angle” at IDs 53 and 54. This prevents a safe blanket “add one” conversion.

Define logical managed parameter descriptors with name, value kind, unit context, supported code and availability. Translate through a per-profile selector map. Preserve the full tables in the inventory; ambiguous duplicated/reserved entries remain unavailable until native evidence resolves them. Do not cast a public enum integer directly to a native selector.

Get/Set/Add parameter operations must share that mapping. A value remains strongly typed through a named parameter-value class with explicit string, integer, Boolean or finite-number kind. The help's output label “String” does not override the different value kinds in its selector table.

ChineseSteelGetCodeItemName grows from 28 to 31 rows, adding GB51249 items 29–31. New check-item operations and fire/thermal operations require optional capability interfaces; existing check-item IDs remain mapped by code/profile.

### 4.3. Assignment instructions changed or were clarified

AssignDesignCommand now lists supported command strings and specifies GROUP option strings 0–3; AssignDesignGroup now specifies GROUP with AX/SY/SZ or the SAME AS member method. The 2024 text instead refers to scSteelGroup/9987 and Ax/Ay/Az. These are **contract discrepancies requiring controlled verification**, not permission to substitute one grammar on every version.

Use typed group-selection requests and a narrowly selected internal formatter. Record the chosen grammar in diagnostics. Keep design code/material restrictions explicit: the later documentation excludes concrete command support and restricts aluminum/timber commands. Preserve native 0-success/-1-failure assignment semantics.

GetCheckOption/SetCheckOption changes are punctuation corrections. They require no independent year service. Example-only differences likewise do not justify service splits.

## 5. Main-source reuse and corrections

| Main item | Reuse | Required port treatment |
| --- | --- | --- |
| CreateDesignBrief | Design-code vocabulary and brief creation | Return named creation status and ID; validate the native ID contract separately from 0-success mutations. |
| AssignDesignParameter / AssignDesignCommand | Managed request intent and member assignment | Replace reflection over Value/MemberList with explicit contracts. Reject unsupported values; use invariant culture and verified unit context. Preserve native return codes. |
| Batch assignment overloads | Useful ordered convenience | Materialize once; validate before mutation; return per-item status and a clear partial-completion boundary. Do not claim native transactionality. |
| Source design-code creation overload | Creates a brief then fills it | Stop if creation fails; retain the created brief ID on later failure. No automatic destructive rollback. |
| GetMemberDesignParameters | Named source result and parameter rows | Adapt existing models into immutable snapshots with status; source currently throws on native failure. |
| OSMemberSteelDgnParams holder | Indexed Name/Value/Unit/Description/Default data | Keep late-bound activation and lifetime internal. Verify registration, bitness, STA thread and index/count validity. |
| DesignCommand / DesignParameter and generic interfaces | Reusable domain intent | Review dependent private units/enums; avoid exposing vendor COM or reflection-based silent no-ops. |

The source assignment functions discard native flags, can return the adapter after a missing reflected Value property, and format values using ToString without a specified culture. Repeated enumeration and member-list extraction are additional failure points. The query allows brief zero while assignment validation requires positive IDs; establish the native brief convention rather than copying inconsistent guards.

The source query creates a holder and final-releases it in finally. That pattern is appropriate only for an exclusively created holder with no surviving references. Never final-release the shared Design root or a holder borrowed from the caller. Registration of a holder must not accidentally launch an unrelated STAAD session.

## 6. Public API and internal architecture

Keep IOSDesign and the single non-partial OSDesignAdapter as the common entry point. Public DTOs belong under Models/Design; verified enums under Enums/Design. Internal selection, native calls, selector maps, holder factories and formatting belong under Services/Design, with a small Contracts or Versions subfolder only where a real difference exists.

Proposed reusable public models:

- DesignBriefResult: completion/status, native code, DesignBriefId and diagnostic.
- DesignQueryResult&lt;T&gt;: status and an owned data snapshot, with no data published on failure.
- DesignMutationResult / DesignBatchResult: native status, affected identifiers and explicit per-item outcomes.
- MemberDesignParametersSnapshot / MemberDesignParameterInfo: source-compatible names, values, units, descriptions and default indicators.
- ChineseSteelMemberQuery, ChineseSteelMaximumResult, ChineseSteelCheckResult and ChineseSteelParameterSettings: named fields instead of tuples or holder objects.
- ChineseSteelMaterialDefinition and ordered curve-sample classes: explicit physical dimensions, finite values and documented sample ordering.

Reuse an existing compatible result/validation abstraction where it fits. Do not create a new result class for every scalar query. Void functions need a distinct dispatch-completed state; absence of a COM exception does not prove that design or report generation succeeded.

Capture wrapper.StaadVersion and the exact RawDesign object for the selected binding. ApplicationVersion describes the consuming host and must never select a native contract. Cached wrapper unit properties can provide context but may be fallback values; refresh deliberately and do not fabricate native provenance.

~~~mermaid
flowchart TD
    A["Acquire wrapper and RawDesign"] --> B["Capture native version and inspect dispatch"]
    B --> C{"Binding and operation qualified?"}
    C -->|No| X["Return unavailable or unresolved capability"]
    C -->|Yes| D["Common Design service"]
    D --> E{"Contract differs?"}
    E -->|No| F["Shared formatter and invocation"]
    E -->|Yes| G["Verified legacy or later policy"]
    G --> F
    F --> H["Validate native status and copy owned data"]
    H --> I["Return named result and diagnostics"]
~~~

Common operations use shared services. The four legacy arities, parameter-selector maps and verified GROUP grammar are the only currently identified policy boundaries. Fourteen 2025 additions are exposed through acquired optional interfaces, grouped by coherent capability such as Chinese steel check items and fire design. The common adapter must not implement unavailable optional interfaces and permit a cast to bypass the gateway.

Runtime selection cannot hide statically declared C# methods or enum members from IntelliSense. Keep version-exclusive operations out of the common interface, and return a capability result containing the optional adapter only when qualified. A raw binding, a documented name and an available license are three different observations.

## 7. Ordered implementation plan

| ID | Scope | Exit criteria |
| --- | --- | --- |
| D1 | Common results, native status readers, version/capability rules, holder ownership and selector evidence | Approved API; no public ref/out, tuples, COM holder or proprietary binary reference. |
| D2 | Six non-Chinese functions; port seven source conveniences where useful | Native status and ID handling, invariant assignment formatting, owned parameter snapshots and partial-batch outcomes tested. |
| D3 | Chinese initialization, solution/parameter/check/storey/dissipated operations | Correct count/index bases, typed settings and per-profile selector map; mutations isolated on fixtures. |
| D4 | Chinese results, maxima, result parameters and report/table display | Four legacy arities verified; values, cases, units, positions and statuses independently checked. |
| D5 | Fourteen newer capabilities, including materials and curves | Gateway excludes unsupported versions; finite/ordered sample data and holder/settings ownership qualified. |
| D6 | Cross-version acceptance, XML examples and package verification | All nonexcluded inventory rows reconciled; actual installed patch levels recorded; installation smoke tests passed. |

The matrix assigns every native name to a workstream. Check-item additions are in D3; fire/thermal additions are in D5. D2 is a practical first port but does not complete the full Bentley domain.

## 8. Tests, XML documentation and unresolved gates

Unit/fake-dispatch tests must cover 0/-1 conventions, Boolean versus numeric flags, untouched outputs, malformed arrays, null strings, unknown enum codes, brief/member validation, empty batches, single enumeration, invariant formatting, model refresh/disposal and COM-holder release on every error path.

Profile tests must verify every logical selector against each table, keep ambiguous/reserved entries blocked, exercise both arities of all four result/report functions, and ensure optional capabilities cannot be accessed through a common-adapter cast. Never fall back to a different STAAD instance.

Controlled native fixtures need each installed year, supported code/material/license, design-result availability, changed solution/member IDs, negative/empty result paths and fire/thermal round trips. Verify engineering meaning against STAAD UI/input/results; passive metadata does not supply that evidence.

Every public method, request/result class, enum value and overload requires XML summary, parameter names, index bases, units, return/status semantics, exceptions, version/license prerequisites, ownership/thread rules and a compilable usage example. Query success, design pass/fail and transport success must be distinguished.

## 9. NuGet and completion boundary

This review changes documentation only. The existing package remains .NET Framework 4.8.1; reflection-only inspection with a separate utility does not add modern .NET support. Future Design ports must retain late binding, include the XML file and models in the package, and ship no Bentley interop DLL or local help files. Public preview/version, README and release notes must describe only the implemented scope when a new package is built.

The comparisons are complete for the installed evidence. Native ABI qualification, ambiguity resolution, implementation, tests and package publication remain separate work.
