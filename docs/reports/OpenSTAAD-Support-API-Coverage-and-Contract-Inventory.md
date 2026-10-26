# OpenSTAAD Support API coverage and contract inventory

**Review date:** 23 October 2026<br>
**Baseline:** `687420d96014f203152fcf2e40979e5b5f351b1c`<br>
**Implementation updated:** 26 October 2026<br>
**Status:** 19 common documented routes implemented; 16 remain pending; native qualification remains separate.

Read the [architecture and implementation plan](OpenSTAAD-Support-Multi-Version-Architecture-and-Plan.md) for decisions, source limitations and verification gates. The [comparison JSON](data/OpenSTAAD-Support-2024-2026-Comparison.json) preserves each year, exact declarations, full parameter/return tables, source maps and fingerprints.

## Current implementation - foundation and mutation contracts

Implemented **19 common documented Support routes**, leaving **16/35 pending**. The new surface covers supported-node discovery, typed support information and GUIDs, foundation counts/readback, explicit fixed/pinned/fixed-but creation, assignment, unassignment, deletion and void GUID invocation. One internal shared service validates operation-specific counts/types/flags, owned bounded buffers and binding lifetime. Reused existing Releases/Springs packets and ported the main-project enums without private UI dependencies. See [all 19 contracts](../Support-Foundations.md).

Release build passed with zero warnings/errors and **5,523 offline tests** (170 new cases). Isolated documentation/package verification covers **1,052 selected XML examples**, including all 92 Output and 38 Support examples. Managed fixtures do not certify live 2024–2026 COM or engineering behavior.

## 1. Scope and counting

Each version has 35 detailed Support functions across three group pages. All declarations and normalized non-example contracts match across 2024/2025/2026. All 35 examples change between 2024 and 2025; 2025/2026 examples match. Example changes are not automatically API changes. A scan of other help group pages found no additional OSSupportUI-owned function rows.

Both inspected 2025/2026 IOSSupportUI interfaces contain exactly the same 35 methods, covering the same names as help. Native class listings contain 42 names: those 35 functions and seven implementation/lifecycle helpers, excluded below. No supplemental automation-only name was found.

The current IOSSupport and OSSupportAdapter expose nineteen domain methods reaching nineteen of thirty-five documented native routes; ComObject and inherited Wrapper are binding properties. The main source has 33 matching interface/public-adapter overloads and no private adapter methods. Those call eighteen distinct native names, leaving seventeen without a main-adapter target. Two managed overloads are obsolete(error=true), and other branches are incomplete; source presence is not a native verification claim.

All 35 native names are pending as typed managed APIs in this repository. Each proposal below returns a named status/data result; no new public ref/out, tuple or proprietary vendor COM type is proposed. A common implementation is justified by current evidence; separate year implementations would add unsupported distinctions.

## 2. Complete cross-version matrix

| Native Support function | Return (all years) | 2024 / 2025 / 2026 | Main call target | Plan | Current coverage |
| --- | --- | --- | --- | --- | --- |
| `CreateSupportFixed` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gaccbf813fd3847a7f863ca83642abded3) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gaccbf813fd3847a7f863ca83642abded3) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gaccbf813fd3847a7f863ca83642abded3) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L84) | S6 | Implemented: `CreateFixedSupport`; live pending |
| `CreateSupportPinned` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga5e2e4d1263c4218440d8d96d634b459f) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga5e2e4d1263c4218440d8d96d634b459f) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga5e2e4d1263c4218440d8d96d634b459f) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L80) | S6 | Implemented: `CreatePinnedSupport`; live pending |
| `CreateSupportFixedBut` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gad7e5628fc73b89b32f8d70f18cc81e78) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gad7e5628fc73b89b32f8d70f18cc81e78) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gad7e5628fc73b89b32f8d70f18cc81e78) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L89) | S6 | Implemented: `CreateFixedButSupport`; live pending |
| `AssignSupportToNode` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gae45409664dab164fc6ce4fbb296c9a1e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gae45409664dab164fc6ce4fbb296c9a1e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gae45409664dab164fc6ce4fbb296c9a1e) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L62) | S6 | Implemented: `AssignSupportToNodes`; live pending |
| `GetSupportName` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga73977d3c7ddb234e8d88f2b87a08f15d) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga73977d3c7ddb234e8d88f2b87a08f15d) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga73977d3c7ddb234e8d88f2b87a08f15d) | No target | S3 | Pending |
| `GetSupportUniqueID` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gaa8d7c16ec66a713c07c99b5f3bde2efa) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gaa8d7c16ec66a713c07c99b5f3bde2efa) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gaa8d7c16ec66a713c07c99b5f3bde2efa) | No target | S3 | Implemented: `GetSupportUniqueId`; live pending |
| `SetSupportUniqueID` | `void` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga2ed3d0f63a68576e7a81f57591be743e) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga2ed3d0f63a68576e7a81f57591be743e) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga2ed3d0f63a68576e7a81f57591be743e) | No target | S8 | Implemented: `SetSupportUniqueId`; live pending |
| `RemoveSupportFromNode` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gaf77763c62a89f04dc29fbd9b0e0d037d) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gaf77763c62a89f04dc29fbd9b0e0d037d) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gaf77763c62a89f04dc29fbd9b0e0d037d) | No target | S8 | Implemented: `RemoveSupportFromNode`; live pending |
| `DeleteSupport` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga63e369a9e50538229af6c07d73ae7845) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga63e369a9e50538229af6c07d73ae7845) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga63e369a9e50538229af6c07d73ae7845) | No target | S8 | Implemented: `DeleteSupport`; live pending |
| `CreateInclinedSupport` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga0de3290e927f957f41a1bd6e0e3abeac) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga0de3290e927f957f41a1bd6e0e3abeac) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga0de3290e927f957f41a1bd6e0e3abeac) | No target | S7 | Pending |
| `CreateElasticMat` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga49d5757efd9c46e1acd59260b1ead221) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga49d5757efd9c46e1acd59260b1ead221) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga49d5757efd9c46e1acd59260b1ead221) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L108) | S7 | Pending |
| `RemoveElasticMat` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga153b6a29601fbe3ff99e9b5d559433a7) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga153b6a29601fbe3ff99e9b5d559433a7) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga153b6a29601fbe3ff99e9b5d559433a7) | No target | S8 | Pending |
| `RemoveElasticMatFromNode` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga5f7616e7bb3baf0f1e8e3f8c1984c701) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga5f7616e7bb3baf0f1e8e3f8c1984c701) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga5f7616e7bb3baf0f1e8e3f8c1984c701) | No target | S8 | Pending |
| `AssignSupportToEntityList` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga86ef9166a6e38cc1d19f0138fb01d07b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga86ef9166a6e38cc1d19f0138fb01d07b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga86ef9166a6e38cc1d19f0138fb01d07b) | No target | S6 | Pending |
| `CreatePlateMat` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gaa1e39b95a2e56588edd55118f6c9c544) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gaa1e39b95a2e56588edd55118f6c9c544) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gaa1e39b95a2e56588edd55118f6c9c544) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L114) | S7 | Pending |
| `RemovePlateMat` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga81c64bbb52a4fb50612139675971f7d8) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga81c64bbb52a4fb50612139675971f7d8) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga81c64bbb52a4fb50612139675971f7d8) | No target | S8 | Pending |
| `RemovePlateMatFromPlate` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gaaa030000531d9773c5b032432c4b4215) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gaaa030000531d9773c5b032432c4b4215) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gaaa030000531d9773c5b032432c4b4215) | No target | S8 | Pending |
| `CreateElasticFooting` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga1eb1b863f39109878125d792c62fbccc) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga1eb1b863f39109878125d792c62fbccc) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga1eb1b863f39109878125d792c62fbccc) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L103) | S7 | Pending |
| `RemoveElasticFooting` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gab41a4e6af27c329d359f845891db80d8) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gab41a4e6af27c329d359f845891db80d8) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gab41a4e6af27c329d359f845891db80d8) | No target | S8 | Pending |
| `RemoveElasticFootingFromNode` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gacc712e1e6a56cb8618f30108ee913aa0) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gacc712e1e6a56cb8618f30108ee913aa0) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gacc712e1e6a56cb8618f30108ee913aa0) | No target | S8 | Pending |
| `GetSupportCount` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga2399fd56b13e12e5fa80e99ac0bbbd8c) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga2399fd56b13e12e5fa80e99ac0bbbd8c) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga2399fd56b13e12e5fa80e99ac0bbbd8c) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L128) | S2 | Implemented: `GetSupportedNodeCount`; live pending |
| `GetSupportNodes` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga8921a39485dbf131b14e8b7362d93ed5) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga8921a39485dbf131b14e8b7362d93ed5) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga8921a39485dbf131b14e8b7362d93ed5) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L169) | S2 | Implemented: `GetSupportedNodeIds`; live pending |
| `GetSupportType` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga222d7ca1ee337f5fdc17b765c9f452f5) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga222d7ca1ee337f5fdc17b765c9f452f5) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga222d7ca1ee337f5fdc17b765c9f452f5) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L191) | S2 | Implemented: `GetSupportType`; live pending |
| `GetSupportInformation` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga5cebc53658e2c36b81dde5f3e8c1c961) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga5cebc53658e2c36b81dde5f3e8c1c961) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga5cebc53658e2c36b81dde5f3e8c1c961) | No target | S2 | Implemented: `GetSupportInformation`; live pending |
| `GetSupportInformationEx` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga44751a360dec5b32dc6f4dc8149f9e64) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga44751a360dec5b32dc6f4dc8149f9e64) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga44751a360dec5b32dc6f4dc8149f9e64) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L222) | S3 | Pending |
| `GetCountOfElasticMat` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga384870741da40646f01f9e3201daf5dd) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga384870741da40646f01f9e3201daf5dd) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga384870741da40646f01f9e3201daf5dd) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L368) | S4 | Implemented: `GetElasticMatDefinitionCount`; live pending |
| `GetElasticMatDetail` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga1a8d6f586d8a9000a37b5c802487ca1a) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga1a8d6f586d8a9000a37b5c802487ca1a) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga1a8d6f586d8a9000a37b5c802487ca1a) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L438) | S4 | Implemented: `GetElasticMat`; live pending |
| `GetElasticMatAssignmentList` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga45260b57daac5ddd1c0f95074f25ed72) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga45260b57daac5ddd1c0f95074f25ed72) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga45260b57daac5ddd1c0f95074f25ed72) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L454) | S4 | Implemented: `GetElasticMatAssignedNodeIds`; live pending |
| `GetCountOfPlateMat` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#gaebf5cc1e4a8e0e3792695bb8e584102b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#gaebf5cc1e4a8e0e3792695bb8e584102b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#gaebf5cc1e4a8e0e3792695bb8e584102b) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L346) | S4 | Implemented: `GetPlateMatDefinitionCount`; live pending |
| `GetPlateMatSupportId` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#gaf19c74e5767bab8c670353de21a5b5c4) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#gaf19c74e5767bab8c670353de21a5b5c4) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#gaf19c74e5767bab8c670353de21a5b5c4) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L351) | S4 | Pending |
| `GetPlateMatDetail` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga195575a1227391d9ed4921902779b769) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga195575a1227391d9ed4921902779b769) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga195575a1227391d9ed4921902779b769) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L398) | S4 | Pending |
| `GetPlateMatAssignmentList` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#gadb8f00f7184d2f6684ebb3fcb0c53754) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#gadb8f00f7184d2f6684ebb3fcb0c53754) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#gadb8f00f7184d2f6684ebb3fcb0c53754) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L414) | S4 | Pending |
| `GetCountOfElasticFooting` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga6091112d45f222bd5c72971f8b555361) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga6091112d45f222bd5c72971f8b555361) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga6091112d45f222bd5c72971f8b555361) | No target | S5 | Implemented: `GetElasticFootingDefinitionCount`; live pending |
| `GetElasticFootingDetail` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga2fa98d49a023f379ef4e2d56dc5aa96b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga2fa98d49a023f379ef4e2d56dc5aa96b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga2fa98d49a023f379ef4e2d56dc5aa96b) | No target | S5 | Implemented: `GetElasticFooting`; live pending |
| `GetElasticFootingAssignmentList` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga43dc24b4de9c9362ccb2716faa1e22d4) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga43dc24b4de9c9362ccb2716faa1e22d4) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga43dc24b4de9c9362ccb2716faa1e22d4) | No target | S5 | Implemented: `GetElasticFootingAssignedNodeIds`; live pending |

## 3. Native contracts and managed proposals

Native long is Windows 32-bit. VARIANT/Object does not determine logical direction or scalar/SAFEARRAY subtype. Mutable references can be documented inputs; parameter-table errors and aliases are preserved explicitly. The GetSupportInformationEx direction discrepancy requires verification rather than silently rewriting native documentation.

<a id="support-createsupportfixed"></a>

### 1. CreateSupportFixed

**Section:** Support: Create Support and Assign Support to Node(s) Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gaccbf813fd3847a7f863ca83642abded3), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gaccbf813fd3847a7f863ca83642abded3) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gaccbf813fd3847a7f863ca83642abded3).<br>
**Current typed implementation:** Implemented `SupportMutationResult CreateFixedSupport()`; live native qualification pending.<br>
**Main reference:** [CreateSupport native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L84)<br>
**Workstream:** S6

```cpp
VARIANT OSSupportUI::CreateSupportFixed()
```

No native parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | Support Reference number ID. |
| `-1` | General error. |

**Installed 2025 / 2026 interop:** `System.Object CreateSupportFixed()`

**Implemented managed entry:** `SupportMutationResult CreateFixedSupport()`<br>
**Return family:** `SupportMutationResult`

**Payload, validation and limitations:** When to use: This creates a support definition and returns its qualified positive reference; it does not attach it to nodes. AssignSupportToNodes is a separate mutation. Native -1 is failure; Boolean success or native zero is not accepted as a created reference. No mutable managed support ID is overwritten. Read back assignments separately before drawing restraint/engineering conclusions.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. This implemented signature is callable; native qualification remains separate.

<a id="support-createsupportpinned"></a>

### 2. CreateSupportPinned

**Section:** Support: Create Support and Assign Support to Node(s) Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga5e2e4d1263c4218440d8d96d634b459f), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga5e2e4d1263c4218440d8d96d634b459f) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga5e2e4d1263c4218440d8d96d634b459f).<br>
**Current typed implementation:** Implemented `SupportMutationResult CreatePinnedSupport()`; live native qualification pending.<br>
**Main reference:** [CreateSupport native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L80)<br>
**Workstream:** S6

```cpp
VARIANT OSSupportUI::CreateSupportPinned()
```

No native parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | Support Reference number ID. |
| `-1` | General error. |

**Installed 2025 / 2026 interop:** `System.Object CreateSupportPinned()`

**Implemented managed entry:** `SupportMutationResult CreatePinnedSupport()`<br>
**Return family:** `SupportMutationResult`

**Payload, validation and limitations:** When to use: The native pinned operation is separate from synthesizing fixed-but release flags. Help describes local Y/Z rotations while the private source convenience releases all rotations; this method deliberately returns only the created ID and does not claim torsional restraint. Assign it separately, then use GetSupportInformation at a node to observe actual states. Native -1 is failure; success requires a positive integral reference.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. This implemented signature is callable; native qualification remains separate.

<a id="support-createsupportfixedbut"></a>

### 3. CreateSupportFixedBut

**Section:** Support: Create Support and Assign Support to Node(s) Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gad7e5628fc73b89b32f8d70f18cc81e78), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gad7e5628fc73b89b32f8d70f18cc81e78) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gad7e5628fc73b89b32f8d70f18cc81e78).<br>
**Current typed implementation:** Implemented `SupportMutationResult CreateFixedButSupport(FixedButSupportDefinition definition)`; live native qualification pending.<br>
**Main reference:** [CreateSupport native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L89)<br>
**Workstream:** S6

```cpp
VARIANT OSSupportUI::CreateSupportFixedBut(const VARIANT FAR&varReleaseSpec,const VARIANT FAR&varSpringSpec)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `varReleaseSpec` | `const VARIANT FAR&` | [in] | Degrees of freedom: Fixed (= 0) or Release (= 1) for FX, FY, FZ, MX, MY and MZ.(Type : Double) |
| `varSpringSpec` | `const VARIANT FAR&` | [in] | The variable spring constants: KFX, KFY, KFZ, KMX, KMY and KMZ.(Type : Double) |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | Support Reference number ID. |
| `-1` | General error. |

**Installed 2025 / 2026 interop:** `System.Object CreateSupportFixedBut(System.Object, System.Object)`

**Implemented managed entry:** `SupportMutationResult CreateFixedButSupport(FixedButSupportDefinition definition)`<br>
**Return family:** `SupportMutationResult`

**Payload, validation and limitations:** When to use: Use when creation flags and springs must be specified explicitly rather than using the dedicated fixed/pinned calls. FixedButSupportDefinition snapshots the existing Releases/Springs packets. Native input flags are six Double 0/1 values, not the read-back Spring=-1 state or member partial-release modes. Native arrays are fresh copies; the caller’s packets are not changed. Stiffness units and release/spring interaction remain native semantics without scaling or inferred modes. Native -1 fails; a positive integral support ID is required.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. This implemented signature is callable; native qualification remains separate.

<a id="support-assignsupporttonode"></a>

### 4. AssignSupportToNode

**Section:** Support: Create Support and Assign Support to Node(s) Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gae45409664dab164fc6ce4fbb296c9a1e), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gae45409664dab164fc6ce4fbb296c9a1e) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gae45409664dab164fc6ce4fbb296c9a1e).<br>
**Current typed implementation:** Implemented `SupportMutationResult AssignSupportToNodes(int supportId, IEnumerable<int> nodeIds, int maximumResults = 100000)`; live native qualification pending.<br>
**Main reference:** [AssignSupportToNode native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L62), [AssignSupportToNode native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L69)<br>
**Workstream:** S6

```cpp
VARIANT OSSupportUI::AssignSupportToNode(const VARIANT FAR&varnNodeNo,const VARIANT FAR&varnSupportNo)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `varnNodeNo` | `const VARIANT FAR&` | [in] | The node number ID(s) VARIANT array. |
| `varnSupportNo` | `const VARIANT FAR&` | [in] | Support Reference number ID. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 interop:** `System.Object AssignSupportToNode(System.Object, System.Object)`

**Implemented managed entry:** `SupportMutationResult AssignSupportToNodes(int supportId, IEnumerable<int> nodeIds, int maximumResults = 100000)`<br>
**Return family:** `SupportMutationResult`

**Payload, validation and limitations:** When to use: Creation and assignment are separate; this function sends one Int32 node-ID array to native AssignSupportToNode in node-array-first/support-ID-second order. The support ID must be appropriate for node assignment; this is not the plate-mat entity-list route. Input is enumerated once and bounded; order and duplicates remain unchanged. Integral 0 is success and -1 is failure; actual Boolean false is not numeric zero-success. Empty input produces NoOperation after binding checks and no native invocation. No per-node acknowledgement is invented; partial native effects on failure are possible.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. This implemented signature is callable; native qualification remains separate.

<a id="support-getsupportname"></a>

### 5. GetSupportName

**Section:** Support: Create Support and Assign Support to Node(s) Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga73977d3c7ddb234e8d88f2b87a08f15d), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga73977d3c7ddb234e8d88f2b87a08f15d) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga73977d3c7ddb234e8d88f2b87a08f15d).<br>
**Current typed implementation:** Pending.<br>
**Main reference:** No native target in the reference Support adapter.<br>
**Workstream:** S3

```cpp
VARIANT OSSupportUI::GetSupportName(const VARIANT FAR&nSupportNo)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nSupportNo` | `const VARIANT FAR&` | [in] | The support node number. |


**Native return description:** The support name string.

**Installed 2025 / 2026 interop:** `System.Object GetSupportName(System.Object)`

**Proposed managed entry:** `GetSupportNameAtNode(int nodeId)`<br>
**Proposed return class:** `SupportNameResult`

**Payload, validation and limitations:** Help names nSupportNo but describes support node number. Confirm identity and empty-name semantics; never substitute support item ID by name similarity.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. These are proposed contracts, not callable APIs.

<a id="support-getsupportuniqueid"></a>

### 6. GetSupportUniqueID

**Section:** Support: Create Support and Assign Support to Node(s) Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gaa8d7c16ec66a713c07c99b5f3bde2efa), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gaa8d7c16ec66a713c07c99b5f3bde2efa) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gaa8d7c16ec66a713c07c99b5f3bde2efa).<br>
**Current typed implementation:** Implemented `SupportQueryResult<SupportUniqueId> GetSupportUniqueId(int supportId)`; live native qualification pending.<br>
**Main reference:** No native target in the reference Support adapter.<br>
**Workstream:** S3

```cpp
VARIANT OSSupportUI::GetSupportUniqueID(const VARIANT FAR&nSupportNo)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nSupportNo` | `const VARIANT FAR&` | [in] | The support item number. |


**Native return description:** The support GUID string.

**Installed 2025 / 2026 interop:** `System.Object GetSupportUniqueID(System.Object)`

**Implemented managed entry:** `SupportQueryResult<SupportUniqueId> GetSupportUniqueId(int supportId)`<br>
**Return family:** `SupportQueryResult<SupportUniqueId>`

**Payload, validation and limitations:** When to use: This takes a support item/definition ID, not a node number, support-list index or user-facing name. Null, DBNull or empty native text gives EmptyOrUnavailable; malformed GUID text throws. Exact native text and parsed Guid are both retained. Guid.Empty, if returned, remains an observed value, not a generated replacement. SetSupportUniqueId writes a new identity and has only void invocation evidence.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. This implemented signature is callable; native qualification remains separate.

<a id="support-setsupportuniqueid"></a>

### 7. SetSupportUniqueID

**Section:** Support: Create Support and Assign Support to Node(s) Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga2ed3d0f63a68576e7a81f57591be743e), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga2ed3d0f63a68576e7a81f57591be743e) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga2ed3d0f63a68576e7a81f57591be743e).<br>
**Current typed implementation:** Implemented `SupportMutationResult SetSupportUniqueId(int supportId, Guid uniqueId)`; live native qualification pending.<br>
**Main reference:** No native target in the reference Support adapter.<br>
**Workstream:** S8

```cpp
void OSSupportUI::SetSupportUniqueID(const VARIANT FAR&nSupportNo,const VARIANT FAR&szName)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nSupportNo` | `const VARIANT FAR&` | [in] | The support item number. |
| `szName` | `const VARIANT FAR&` | [in] | A GUID string |


**Native return description:** No documented return value; distinguish completed invocation from independently verified state.

**Installed 2025 / 2026 interop:** `Void SetSupportUniqueID(System.Object, System.Object)`

**Implemented managed entry:** `SupportMutationResult SetSupportUniqueId(int supportId, Guid uniqueId)`<br>
**Return family:** `SupportMutationResult`

**Payload, validation and limitations:** When to use: This writes a nonempty explicitly supplied Guid to a support definition/item, not a node. GetSupportUniqueId reads its actual current value. The native setter returns void, so completed dispatch produces InvokedWithoutAcknowledgement and IsSuccessful=false, with the requested ID/GUID retained and no fabricated native flag. No automatic read-back or persistence claim occurs; callers may request a separate read-back if needed. The Guid is marshaled as canonical D-format text.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. This implemented signature is callable; native qualification remains separate.

<a id="support-removesupportfromnode"></a>

### 8. RemoveSupportFromNode

**Section:** Support: Create Support and Assign Support to Node(s) Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gaf77763c62a89f04dc29fbd9b0e0d037d), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gaf77763c62a89f04dc29fbd9b0e0d037d) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gaf77763c62a89f04dc29fbd9b0e0d037d).<br>
**Current typed implementation:** Implemented `SupportMutationResult RemoveSupportFromNode(int nodeId)`; live native qualification pending.<br>
**Main reference:** No native target in the reference Support adapter.<br>
**Workstream:** S8

```cpp
VARIANT OSSupportUI::RemoveSupportFromNode(const VARIANT FAR&nNodeNo)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nNodeNo` | `const VARIANT FAR&` | [in] | The node number. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1/TRUE` | OK. |
| `0/FALSE` | General error. |

**Installed 2025 / 2026 interop:** `System.Object RemoveSupportFromNode(System.Object)`

**Implemented managed entry:** `SupportMutationResult RemoveSupportFromNode(int nodeId)`<br>
**Return family:** `SupportMutationResult`

**Payload, validation and limitations:** When to use: This unassigns one node; DeleteSupport removes a shared definition and its assignments. Native 1/true is success, 0/false is failure. Failure does not prove that no support existed. No shared definition deletion, family-specific fallback, retry, save or managed node update is performed.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. This implemented signature is callable; native qualification remains separate.

<a id="support-deletesupport"></a>

### 9. DeleteSupport

**Section:** Support: Create Support and Assign Support to Node(s) Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga63e369a9e50538229af6c07d73ae7845), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga63e369a9e50538229af6c07d73ae7845) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga63e369a9e50538229af6c07d73ae7845).<br>
**Current typed implementation:** Implemented `SupportMutationResult DeleteSupport(int supportId)`; live native qualification pending.<br>
**Main reference:** No native target in the reference Support adapter.<br>
**Workstream:** S8

```cpp
VARIANT OSSupportUI::DeleteSupport(const VARIANT FAR&nSupportNo)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nSupportNo` | `const VARIANT FAR&` | [in] | The support item number. Removing the support will also remove the assignment of the support |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 interop:** `System.Object DeleteSupport(System.Object)`

**Implemented managed entry:** `SupportMutationResult DeleteSupport(int supportId)`<br>
**Return family:** `SupportMutationResult`

**Payload, validation and limitations:** When to use: This is broader than RemoveSupportFromNode, which targets only one assignment. The documented operation removes the support item and its assignments; the request retains the support ID without pretending to know every affected node. Integral 0 is success and -1 is failure; Boolean false is not decoded as numeric zero. Native partial changes may remain on failure and cannot be rolled back by this result.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. This implemented signature is callable; native qualification remains separate.

<a id="support-createinclinedsupport"></a>

### 10. CreateInclinedSupport

**Section:** Support: Create Support and Assign Support to Node(s) Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga0de3290e927f957f41a1bd6e0e3abeac), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga0de3290e927f957f41a1bd6e0e3abeac) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga0de3290e927f957f41a1bd6e0e3abeac).<br>
**Current typed implementation:** Pending.<br>
**Main reference:** No native target in the reference Support adapter.<br>
**Workstream:** S7

```cpp
VARIANT OSSupportUI::CreateInclinedSupport(const VARIANT FAR&varInclinedType,const VARIANT FAR&varType,const VARIANT FAR&vaRefNode,const VARIANT FAR&varCoord,const VARIANT FAR&varReleaseSpec,const VARIANT FAR&varSpringSpec)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `varInclinedType` | `const VARIANT FAR&` | [in] | Type of the Inclined support (Fixed, Pinned, FixedBut): Value Inclined Type 1 Pinned 2 Fixed 3 FixedBut 4 Enforced 5 EnforcedBut |
| `varType` | `const VARIANT FAR&` | [in] | Type of the reference point define: Value Table Type 0 fRefX , fRefY , fRefY global distances from the joint to the reference point. 1 fRefX , fRefY , fRefY global coordinates of the reference point. 2 a joint number ( vaRefNode ) whose x, y, z global coordinates is the reference point. |
| `vaRefNode` | `const VARIANT FAR&` | [in] | The reference node number ID. |
| `varCoord` | `const VARIANT FAR&` | [in] | Distance or coordinate in (X,Y,Z) direction. |
| `varReleaseSpec` | `const VARIANT FAR&` | [in] | Degrees of freedom: Fixed (= 0) or Release (= 1) for FX, FY, FZ, MX, MY and MZ.(Type: Double) |
| `varSpringSpec` | `const VARIANT FAR&` | [in] | The variable spring constants: KFX, KFY, KFZ, KMX, KMY and KMZ.(Type: Double) |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | Support Reference number ID. |
| `-1` | General error. |
| `-2001` | Cannot find Node vaRefNode . |

**Installed 2025 / 2026 interop:** `System.Object CreateInclinedSupport(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Proposed managed entry:** `CreateInclinedSupport(InclinedSupportRequest request)`<br>
**Proposed return class:** `SupportCreationResult`

**Payload, validation and limitations:** Separate inclined subtype 1..5 from reference kind 0 offsets, 1 global coordinates, 2 reference node. Validate geometry and release/stiffness arrays; preserve -2001 not-found and -1 error. No guessed ordinary-enforced substitute.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. These are proposed contracts, not callable APIs.

<a id="support-createelasticmat"></a>

### 11. CreateElasticMat

**Section:** Support: Create Support and Assign Support to Node(s) Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga49d5757efd9c46e1acd59260b1ead221), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga49d5757efd9c46e1acd59260b1ead221) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga49d5757efd9c46e1acd59260b1ead221).<br>
**Current typed implementation:** Pending.<br>
**Main reference:** [CreateSupport native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L108)<br>
**Workstream:** S7

```cpp
VARIANT OSSupportUI::CreateElasticMat(const VARIANT FAR&nDir,const VARIANT FAR&nSubgrade,const VARIANT FAR&nPrint,const VARIANT FAR&nSpringType)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nDir` | `const VARIANT FAR&` | [in] | (Type: Long): Value Inclined Type 0 X Direction 1 Y Direction 2 Z Direction 3 X Only Direction 4 Y Only Direction 5 Z Only Direction |
| `nSubgrade` | `const VARIANT FAR&` | [in] | Subgrade Modulus (Type: Double) |
| `nPrint` | `const VARIANT FAR&` | [in] | (Type: Long) Value Print Influence 0 Not Checked 1 Checked |
| `nSpringType` | `const VARIANT FAR&` | [in] | Generate Compression (Type: Long) Value Spring Type 0 None 1 Compression Only 2 Multi-Linear |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | Support Reference number ID. |
| `0` | General error. |

**Installed 2025 / 2026 interop:** `System.Object CreateElasticMat(System.Object, System.Object, System.Object, System.Object)`

**Proposed managed entry:** `CreateElasticMat(ElasticMatSupportRequest request)`<br>
**Proposed return class:** `FoundationSupportCreationResult`

**Payload, validation and limitations:** Directions 0..5, one scalar Double subgrade, print and spring flags as verified integer codes. Native 0 failure; preserve support ID and foundation family.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. These are proposed contracts, not callable APIs.

<a id="support-removeelasticmat"></a>

### 12. RemoveElasticMat

**Section:** Support: Create Support and Assign Support to Node(s) Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga153b6a29601fbe3ff99e9b5d559433a7), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga153b6a29601fbe3ff99e9b5d559433a7) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga153b6a29601fbe3ff99e9b5d559433a7).<br>
**Current typed implementation:** Pending.<br>
**Main reference:** No native target in the reference Support adapter.<br>
**Workstream:** S8

```cpp
VARIANT OSSupportUI::RemoveElasticMat(const VARIANT FAR&supportid)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `supportid` | `const VARIANT FAR&` | [in] | (Type: Long). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE` | Successful. |
| `FALSE` | Unsuccessful. |

**Installed 2025 / 2026 interop:** `System.Object RemoveElasticMat(System.Object)`

**Proposed managed entry:** `RemoveElasticMat(int supportId)`<br>
**Proposed return class:** `SupportRemovalResult`

**Payload, validation and limitations:** Definition-level Boolean removal; verify assignment consequences before promising cleanup semantics.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. These are proposed contracts, not callable APIs.

<a id="support-removeelasticmatfromnode"></a>

### 13. RemoveElasticMatFromNode

**Section:** Support: Create Support and Assign Support to Node(s) Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga5f7616e7bb3baf0f1e8e3f8c1984c701), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga5f7616e7bb3baf0f1e8e3f8c1984c701) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga5f7616e7bb3baf0f1e8e3f8c1984c701).<br>
**Current typed implementation:** Pending.<br>
**Main reference:** No native target in the reference Support adapter.<br>
**Workstream:** S8

```cpp
VARIANT OSSupportUI::RemoveElasticMatFromNode(const VARIANT FAR&nodeid)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nodeid` | `const VARIANT FAR&` | [in] | (Type: Long). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE` | Successful. |
| `FALSE` | Unsuccessful. |

**Installed 2025 / 2026 interop:** `System.Object RemoveElasticMatFromNode(System.Object)`

**Proposed managed entry:** `RemoveElasticMatFromNode(int nodeId)`<br>
**Proposed return class:** `SupportRemovalResult`

**Payload, validation and limitations:** Node-specific Boolean unassignment; no deletion of shared foundation by implication.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. These are proposed contracts, not callable APIs.

<a id="support-assignsupporttoentitylist"></a>

### 14. AssignSupportToEntityList

**Section:** Support: Create Support and Assign Support to Node(s) Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga86ef9166a6e38cc1d19f0138fb01d07b), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga86ef9166a6e38cc1d19f0138fb01d07b) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga86ef9166a6e38cc1d19f0138fb01d07b).<br>
**Current typed implementation:** Pending.<br>
**Main reference:** No native target in the reference Support adapter.<br>
**Workstream:** S6

```cpp
VARIANT OSSupportUI::AssignSupportToEntityList(const VARIANT FAR&supportid,const VARIANT FAR&entitylist)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `supportid` | `const VARIANT FAR&` | [in] | (Type: Long). |
| `entitylist` | `const VARIANT FAR&` | [in] | Entitylist should contain plate numbers for PlateMat or node numbers for rest of support types (Type: Long). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE` | Successful. |
| `FALSE` | Unsuccessful. |

**Installed 2025 / 2026 interop:** `System.Object AssignSupportToEntityList(System.Object, System.Object)`

**Proposed managed entry:** `AssignSupport(SupportAssignmentRequest request)`<br>
**Proposed return class:** `SupportAssignmentResult`

**Payload, validation and limitations:** Boolean return. Entity IDs are plates for plate mat and nodes for other support types. Use typed request variants; no ambiguous untyped entity ID bag.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. These are proposed contracts, not callable APIs.

<a id="support-createplatemat"></a>

### 15. CreatePlateMat

**Section:** Support: Create Support and Assign Support to Node(s) Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gaa1e39b95a2e56588edd55118f6c9c544), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gaa1e39b95a2e56588edd55118f6c9c544) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gaa1e39b95a2e56588edd55118f6c9c544).<br>
**Current typed implementation:** Pending.<br>
**Main reference:** [CreateSupport native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L114)<br>
**Workstream:** S7

```cpp
VARIANT OSSupportUI::CreatePlateMat(const VARIANT FAR&varDirection,const VARIANT FAR&varSubgrades,const VARIANT FAR&varPrint,const VARIANT FAR&varSpringType)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `varDirection` | `const VARIANT FAR&` | Not named in parameter table | Not documented under this declaration name. |
| `varSubgrades` | `const VARIANT FAR&` | [in] | Subgrade Modulus value(s), If All Direction set, varSubgrades is (Array of Double), otherwise it should be a double value.(Type: Double) |
| `varPrint` | `const VARIANT FAR&` | [in] | (Type: Long) Value Print Influence 0 Not Checked 1 Checked |
| `varSpringType` | `const VARIANT FAR&` | [in] | Generate Compression (Type: Long) Value Spring Type 0 None 1 Compression Only 2 Multi-Linear |


**Additional or differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `nDir` | [in] | (Type: Long): Value Inclined Type 0 X Direction 1 Y Direction 2 Z Direction 3 X Only Direction 4 Y Only Direction 5 Z Only Direction 6 All Direction |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | Support Reference number ID. |
| `0` | General error. |

**Installed 2025 / 2026 interop:** `System.Object CreatePlateMat(System.Object, System.Object, System.Object, System.Object)`

**Proposed managed entry:** `CreatePlateMat(PlateMatSupportRequest request)`<br>
**Proposed return class:** `FoundationSupportCreationResult`

**Payload, validation and limitations:** Directions 0..6; prose requires scalar for one-axis and three-Double array for All=6, while example uses array with direction=2. Resolve marshaling contradiction; do not copy always-array source blindly.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. These are proposed contracts, not callable APIs.

All reviewed examples pass a three-value Double array with direction 2, contradicting the prose scalar requirement for a non-All direction. Keep this as an explicit common binding discrepancy; no version difference justifies choosing one packet arbitrarily.

<a id="support-removeplatemat"></a>

### 16. RemovePlateMat

**Section:** Support: Create Support and Assign Support to Node(s) Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga81c64bbb52a4fb50612139675971f7d8), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga81c64bbb52a4fb50612139675971f7d8) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga81c64bbb52a4fb50612139675971f7d8).<br>
**Current typed implementation:** Pending.<br>
**Main reference:** No native target in the reference Support adapter.<br>
**Workstream:** S8

```cpp
VARIANT OSSupportUI::RemovePlateMat(const VARIANT FAR&supportId)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `supportId` | `const VARIANT FAR&` | Not named in parameter table | Not documented under this declaration name. |


**Additional or differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `supportid` | [in] | (Type: Long). |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE` | Successful. |
| `FALSE` | Unsuccessful. |

**Installed 2025 / 2026 interop:** `System.Object RemovePlateMat(System.Object)`

**Proposed managed entry:** `RemovePlateMat(int supportId)`<br>
**Proposed return class:** `SupportRemovalResult`

**Payload, validation and limitations:** Definition-level Boolean operation with explicit shared-assignment side effects once verified.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. These are proposed contracts, not callable APIs.

<a id="support-removeplatematfromplate"></a>

### 17. RemovePlateMatFromPlate

**Section:** Support: Create Support and Assign Support to Node(s) Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gaaa030000531d9773c5b032432c4b4215), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gaaa030000531d9773c5b032432c4b4215) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gaaa030000531d9773c5b032432c4b4215).<br>
**Current typed implementation:** Pending.<br>
**Main reference:** No native target in the reference Support adapter.<br>
**Workstream:** S8

```cpp
VARIANT OSSupportUI::RemovePlateMatFromPlate(const VARIANT FAR&varPlateNo)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `varPlateNo` | `const VARIANT FAR&` | Not named in parameter table | Not documented under this declaration name. |


**Additional or differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `nodeid` | [in] | (Type: Long). |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE` | Successful. |
| `FALSE` | Unsuccessful. |

**Installed 2025 / 2026 interop:** `System.Object RemovePlateMatFromPlate(System.Object, System.Object)`

**Proposed managed entry:** `RemovePlateMatFromPlate(int plateId)`<br>
**Proposed return class:** `SupportRemovalResult`

**Payload, validation and limitations:** Name/prototype identify plate; prose wrongly describes node/elastic mat. Verify target identity, no node-based fallback.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. These are proposed contracts, not callable APIs.

<a id="support-createelasticfooting"></a>

### 18. CreateElasticFooting

**Section:** Support: Create Support and Assign Support to Node(s) Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga1eb1b863f39109878125d792c62fbccc), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga1eb1b863f39109878125d792c62fbccc) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga1eb1b863f39109878125d792c62fbccc).<br>
**Current typed implementation:** Pending.<br>
**Main reference:** [CreateSupport native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L103)<br>
**Workstream:** S7

```cpp
VARIANT OSSupportUI::CreateElasticFooting(const VARIANT FAR&length,const VARIANT FAR&width,const VARIANT FAR&dir,const VARIANT FAR&subgrade)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `length` | `const VARIANT FAR&` | [in] | Length of footing (Type: Double) |
| `width` | `const VARIANT FAR&` | [in] | Width of footing (Type: Double) |
| `dir` | `const VARIANT FAR&` | [in] | Direction of the resistance of the spring supports (Type: Long): Value Direction 0 X Direction 1 Y Direction 2 Z Direction 3 X Only Direction 4 Y Only Direction 5 Z Only Direction |
| `subgrade` | `const VARIANT FAR&` | [in] | Subgrade Modulus of the soil (Type: Double) |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | Support reference ID. |
| `0` | General error. |

**Installed 2025 / 2026 interop:** `System.Object CreateElasticFooting(System.Object, System.Object, System.Object, System.Object)`

**Proposed managed entry:** `CreateElasticFooting(ElasticFootingSupportRequest request)`<br>
**Proposed return class:** `FoundationSupportCreationResult`

**Payload, validation and limitations:** Positive finite length/width, verified direction 0..5 and scalar subgrade. Native 0 failure; dimensions and active units must be explicit.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. These are proposed contracts, not callable APIs.

<a id="support-removeelasticfooting"></a>

### 19. RemoveElasticFooting

**Section:** Support: Create Support and Assign Support to Node(s) Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gab41a4e6af27c329d359f845891db80d8), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gab41a4e6af27c329d359f845891db80d8) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gab41a4e6af27c329d359f845891db80d8).<br>
**Current typed implementation:** Pending.<br>
**Main reference:** No native target in the reference Support adapter.<br>
**Workstream:** S8

```cpp
VARIANT OSSupportUI::RemoveElasticFooting(const VARIANT FAR&supportid)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `supportid` | `const VARIANT FAR&` | [in] | Support reference ID (Type: Long). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | if the method is successful. |
| `0` | if the method is unsuccessful. |

**Installed 2025 / 2026 interop:** `System.Object RemoveElasticFooting(System.Object)`

**Proposed managed entry:** `RemoveElasticFooting(int supportId)`<br>
**Proposed return class:** `SupportRemovalResult`

**Payload, validation and limitations:** Boolean 1/0 definition removal; qualify native result and affected assignment scope.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. These are proposed contracts, not callable APIs.

<a id="support-removeelasticfootingfromnode"></a>

### 20. RemoveElasticFootingFromNode

**Section:** Support: Create Support and Assign Support to Node(s) Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gacc712e1e6a56cb8618f30108ee913aa0), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gacc712e1e6a56cb8618f30108ee913aa0) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gacc712e1e6a56cb8618f30108ee913aa0).<br>
**Current typed implementation:** Pending.<br>
**Main reference:** No native target in the reference Support adapter.<br>
**Workstream:** S8

```cpp
VARIANT OSSupportUI::RemoveElasticFootingFromNode(const VARIANT FAR&nodeid)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nodeid` | `const VARIANT FAR&` | [in] | Node Id (Type: Long). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | if the method is successful. |
| `0` | if the method is unsuccessful. |

**Installed 2025 / 2026 interop:** `System.Object RemoveElasticFootingFromNode(System.Object)`

**Proposed managed entry:** `RemoveElasticFootingFromNode(int nodeId)`<br>
**Proposed return class:** `SupportRemovalResult`

**Payload, validation and limitations:** Boolean 1/0 node-specific operation. Preserve definition versus node identity.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. These are proposed contracts, not callable APIs.

<a id="support-getsupportcount"></a>

### 21. GetSupportCount

**Section:** Support: Get Support Information Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga2399fd56b13e12e5fa80e99ac0bbbd8c), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga2399fd56b13e12e5fa80e99ac0bbbd8c) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga2399fd56b13e12e5fa80e99ac0bbbd8c).<br>
**Current typed implementation:** Implemented `SupportQueryResult<int> GetSupportedNodeCount()`; live native qualification pending.<br>
**Main reference:** [GetSupportNodesCount native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L128)<br>
**Workstream:** S2

```cpp
VARIANT OSSupportUI::GetSupportCount()
```

No native parameters.

**Native return description:** The number of support.

**Installed 2025 / 2026 interop:** `System.Object GetSupportCount()`

**Implemented managed entry:** `SupportQueryResult<int> GetSupportedNodeCount()`<br>
**Return family:** `SupportQueryResult<int>`

**Payload, validation and limitations:** When to use: This is the supported-node count used by GetSupportedNodeIds, not the number of distinct support definitions or any contiguous ID range. Zero is valid data. Exact nonnegative integral returns are required; Boolean success is never treated as count one. No node geometry is hydrated.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. This implemented signature is callable; native qualification remains separate.

<a id="support-getsupportnodes"></a>

### 22. GetSupportNodes

**Section:** Support: Get Support Information Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga8921a39485dbf131b14e8b7362d93ed5), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga8921a39485dbf131b14e8b7362d93ed5) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga8921a39485dbf131b14e8b7362d93ed5).<br>
**Current typed implementation:** Implemented `SupportQueryResult<IReadOnlyList<int>> GetSupportedNodeIds(int maximumResults = 100000)`; live native qualification pending.<br>
**Main reference:** [GetSupportNodeIds native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L169)<br>
**Workstream:** S2

```cpp
VARIANT OSSupportUI::GetSupportNodes(VARIANT FAR&varNodeNos)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `varNodeNos` | `VARIANT FAR&` | [out] | The supported node number ID(s) VARIANT array. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<val>` | The number of supported node(s). |
| `-1` | General error. |

**Installed 2025 / 2026 interop:** `System.Object GetSupportNodes(System.Object ByRef)`

**Implemented managed entry:** `SupportQueryResult<IReadOnlyList<int>> GetSupportedNodeIds(int maximumResults = 100000)`<br>
**Return family:** `SupportQueryResult<IReadOnlyList<int>>`

**Payload, validation and limitations:** When to use: Use for node discovery instead of enumerating support-definition IDs from a count. The native return is a count, not a Boolean flag; -1 is failure. Count/list/count must agree; a changed count returns StateChanged with no payload and no retry. Zero is rechecked and returns an owned empty list without native list invocation. Initialized Int32 arrays must be completely written; native order and duplicates remain unchanged. Equal counts cannot prove an atomic model snapshot.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. This implemented signature is callable; native qualification remains separate.

<a id="support-getsupporttype"></a>

### 23. GetSupportType

**Section:** Support: Get Support Information Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga222d7ca1ee337f5fdc17b765c9f452f5), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga222d7ca1ee337f5fdc17b765c9f452f5) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga222d7ca1ee337f5fdc17b765c9f452f5).<br>
**Current typed implementation:** Implemented `SupportQueryResult<SupportTypeObservation> GetSupportType(int nodeId)`; live native qualification pending.<br>
**Main reference:** [GetSupportTypesOfNodes native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L191), [GetDistinctSupports native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L221), [GetSupportTypeExt native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L547)<br>
**Workstream:** S2

```cpp
VARIANT OSSupportUI::GetSupportType(VARIANT FAR&vaSupportNode)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `vaSupportNode` | `VARIANT FAR&` | [in] | The supported node number ID. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | No support. |
| `1` | Pinned support. |
| `2` | Fixed support. |
| `3` | Fixed support with releases. |
| `4` | Enforced support. |
| `5` | Enforced support with releases. |
| `6` | Inclined support. |
| `7` | Footing foundation. |
| `8` | Elastic mat foundation. |
| `9` | Plate mat foundation. |
| `10` | MultiLinear spring support. |
| `11` | Generated pinned support. |
| `12` | Generated fixed support. |
| `13` | Generated fixed support with releases. |
| `-1` | General error. |

**Installed 2025 / 2026 interop:** `System.Object GetSupportType(System.Object)`

**Implemented managed entry:** `SupportQueryResult<SupportTypeObservation> GetSupportType(int nodeId)`<br>
**Return family:** `SupportQueryResult<SupportTypeObservation>`

**Payload, validation and limitations:** When to use: Prefer this inexpensive type-only observation when DOF states and stiffnesses are not needed. Native 0 is successful NoSupport data; -1 is NativeFailure. Unknown nonnegative codes retain NativeTypeCode and KnownType=null, without being cast to a fabricated known enum. GetSupportInformation instead requires a reviewed type and written six-DOF buffers. The metadata input is passed by value despite a mutable-reference help declaration.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. This implemented signature is callable; native qualification remains separate.

<a id="support-getsupportinformation"></a>

### 24. GetSupportInformation

**Section:** Support: Get Support Information Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga5cebc53658e2c36b81dde5f3e8c1c961), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga5cebc53658e2c36b81dde5f3e8c1c961) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga5cebc53658e2c36b81dde5f3e8c1c961).<br>
**Current typed implementation:** Implemented `SupportQueryResult<SupportInformation> GetSupportInformation(int nodeId)`; live native qualification pending.<br>
**Main reference:** No native target in the reference Support adapter.<br>
**Workstream:** S2

```cpp
VARIANT OSSupportUI::GetSupportInformation(const VARIANT FAR&vaSupportNode,VARIANT FAR&varReleaseSpec,VARIANT FAR&varSpringSpec)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `vaSupportNode` | `const VARIANT FAR&` | [in] | The supported node number ID. |
| `varReleaseSpec` | `VARIANT FAR&` | [out] | Degrees of freedom: Release (= 1) or Fixed (= 0) or Spring (=-1) for FX, FY, FZ, MX, MY and MZ.(Type:int) |
| `varSpringSpec` | `VARIANT FAR&` | [out] | The variable spring constants: KFX, KFY, KFZ, KMX, KMY and KMZ.(Type:double) |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | No support. |
| `1` | Pinned support. |
| `2` | Fixed support. |
| `3` | Fixed support with releases. |
| `4` | Enforced support. |
| `5` | Enforced support with releases. |
| `6` | Inclined support. |
| `7` | Footing foundation. |
| `8` | Elastic mat foundation. |
| `9` | Plate mat foundation. |
| `10` | MultiLinear spring support. |
| `11` | Generated pinned support. |
| `12` | Generated fixed support. |
| `13` | Generated fixed support with releases. |
| `-1` | General error. |

**Installed 2025 / 2026 interop:** `System.Object GetSupportInformation(System.Object, System.Object ByRef, System.Object ByRef)`

**Implemented managed entry:** `SupportQueryResult<SupportInformation> GetSupportInformation(int nodeId)`<br>
**Return family:** `SupportQueryResult<SupportInformation>`

**Payload, validation and limitations:** When to use: Unlike GetSupportType, this retrieves all FX/FY/FZ/MX/MY/MZ restraint observations. The native return is a support-type code, never a Boolean success flag. Type 0 yields NoSupport without consuming untouched buffers; -1 yields NativeFailure; an unknown positive type yields UnsupportedNativeType without guessing its layout. Known positive types require six written Int32 states (-1 spring, 0 fixed, 1 released) and six finite nonnegative floating-point stiffnesses. Springs are not collapsed to released=true. Native local support axes, force/length translational stiffness and moment/angle rotational stiffness are retained without conversion.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. This implemented signature is callable; native qualification remains separate.

<a id="support-getsupportinformationex"></a>

### 25. GetSupportInformationEx

**Section:** Support: Get Support Information Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga44751a360dec5b32dc6f4dc8149f9e64), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga44751a360dec5b32dc6f4dc8149f9e64) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga44751a360dec5b32dc6f4dc8149f9e64).<br>
**Current typed implementation:** Pending.<br>
**Main reference:** [GetDistinctSupports native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L222), [GetSupport native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L576)<br>
**Workstream:** S3

```cpp
VARIANT OSSupportUI::GetSupportInformationEx(const VARIANT FAR&vaSupportNode,VARIANT FAR&varSupportNo,VARIANT FAR&varSupportType,VARIANT FAR&varReleaseSpec,VARIANT FAR&varSpringSpec)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `vaSupportNode` | `const VARIANT FAR&` | [in] | The supported node number ID. |
| `varSupportNo` | `VARIANT FAR&` | [in] | The support reference number |
| `varSupportType` | `VARIANT FAR&` | [in] | The support type code |
| `varReleaseSpec` | `VARIANT FAR&` | [out] | Degrees of freedom: Release ( = 1) or Fixed ( = 0) or Spring (=-1) for FX, FY, FZ, MX, MY and MZ.(Type:int) |
| `varSpringSpec` | `VARIANT FAR&` | [out] | The variable spring constants: KFX, KFY, KFZ, KMX, KMY and KMZ.(Type:double) |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | No support. |
| `1` | Pinned support. |
| `2` | Fixed support. |
| `3` | Fixed support with releases. |
| `4` | Enforced support. |
| `5` | Enforced support with releases. |
| `6` | Inclined support. |
| `7` | Footing foundation. |
| `8` | Elastic mat foundation. |
| `9` | Plate mat foundation. |
| `10` | MultiLinear spring support. |
| `11` | Generated pinned support. |
| `12` | Generated fixed support. |
| `13` | Generated fixed support with releases. |
| `-1` | General error. |

**Installed 2025 / 2026 interop:** `System.Object GetSupportInformationEx(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Proposed managed entry:** `GetSupportInformationExtended(int nodeId)`<br>
**Proposed return class:** `ExtendedSupportInformationResult`

**Payload, validation and limitations:** Support reference/type are marked [in] in help but mutable refs in declaration/metadata and treated as outputs by source. Verify input/output requirements before enabling this proposed query; no public ref/out.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. These are proposed contracts, not callable APIs.

The help marks support number and type as [in], the interop declares Object&, the example supplies previously created/classified values, and the source initializes then reads them as outputs. Neither direction is certified by static evidence alone. A managed query can be enabled only after required inputs/outputs and return meaning are verified.

<a id="support-getcountofelasticmat"></a>

### 26. GetCountOfElasticMat

**Section:** Support: Get Support Information Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga384870741da40646f01f9e3201daf5dd), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga384870741da40646f01f9e3201daf5dd) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga384870741da40646f01f9e3201daf5dd).<br>
**Current typed implementation:** Implemented `SupportQueryResult<int> GetElasticMatDefinitionCount()`; live native qualification pending.<br>
**Main reference:** [GetElasticMatSupports native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L368)<br>
**Workstream:** S4

```cpp
VARIANT OSSupportUI::GetCountOfElasticMat()
```

No native parameters.

**Native return description:** The number of ElasticMat supports.

**Installed 2025 / 2026 interop:** `System.Object GetCountOfElasticMat()`

**Implemented managed entry:** `SupportQueryResult<int> GetElasticMatDefinitionCount()`<br>
**Return family:** `SupportQueryResult<int>`

**Payload, validation and limitations:** When to use: This is a definition count, not supported-node count, assignment count or maximum support ID. Use GetElasticMat with a known positive definition ID; there is no inferred 1..count discovery. Zero is valid integral data. Plate-mat and footing definition counts are separate queries.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. This implemented signature is callable; native qualification remains separate.

<a id="support-getelasticmatdetail"></a>

### 27. GetElasticMatDetail

**Section:** Support: Get Support Information Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga1a8d6f586d8a9000a37b5c802487ca1a), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga1a8d6f586d8a9000a37b5c802487ca1a) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga1a8d6f586d8a9000a37b5c802487ca1a).<br>
**Current typed implementation:** Implemented `SupportQueryResult<ElasticMatObservation> GetElasticMat(int supportId)`; live native qualification pending.<br>
**Main reference:** [GetElasticMatSupport native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L438), [PopulateElasticMatSupport native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L516)<br>
**Workstream:** S4

```cpp
VARIANT OSSupportUI::GetElasticMatDetail(const VARIANT FAR&supportid,VARIANT FAR&dir,VARIANT FAR&subgrade,VARIANT FAR&print,VARIANT FAR&springType,VARIANT FAR&nodesCount)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `supportid` | `const VARIANT FAR&` | [in] | (Type: Long). |
| `dir` | `VARIANT FAR&` | [out] | (Type: Long): Value Inclined Type 0 X Direction 1 Y Direction 2 Z Direction 3 X Only Direction 4 Y Only Direction 5 Z Only Direction |
| `subgrade` | `VARIANT FAR&` | [out] | Subgrade Modulus (Type: Double) |
| `print` | `VARIANT FAR&` | [out] | (Type: Long) Value Print Influence 0 Not Checked 1 Checked |
| `springType` | `VARIANT FAR&` | [out] | Generate Compression (Type: Long) Value Spring Type 0 None 1 Compression Only 2 Multi-Linear |
| `nodesCount` | `VARIANT FAR&` | [out] | (Type: Long). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE` | Successful. |
| `FALSE` | Unsuccessful. |

**Installed 2025 / 2026 interop:** `System.Object GetElasticMatDetail(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Implemented managed entry:** `SupportQueryResult<ElasticMatObservation> GetElasticMat(int supportId)`<br>
**Return family:** `SupportQueryResult<ElasticMatObservation>`

**Payload, validation and limitations:** When to use: Use a known support definition ID, not a node ID or zero-based index. Native true qualifies direction 0..5, scalar subgrade, print flag, spring-generation type and nonnegative assignment count; false exposes no default packet. This query does not read assigned nodes; GetElasticMatAssignedNodeIds performs a bounded detail/list/detail observation. Native subgrade is force/length cubed, not force/length nodal stiffness. Only direction variants and compression/multilinear generation remain distinct. No model or geometry objects are hydrated.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. This implemented signature is callable; native qualification remains separate.

<a id="support-getelasticmatassignmentlist"></a>

### 28. GetElasticMatAssignmentList

**Section:** Support: Get Support Information Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga45260b57daac5ddd1c0f95074f25ed72), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga45260b57daac5ddd1c0f95074f25ed72) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga45260b57daac5ddd1c0f95074f25ed72).<br>
**Current typed implementation:** Implemented `SupportQueryResult<IReadOnlyList<int>> GetElasticMatAssignedNodeIds(int supportId, int maximumResults = 100000)`; live native qualification pending.<br>
**Main reference:** [GetElasticMatSupport native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L454), [PopulateElasticMatSupport native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L528)<br>
**Workstream:** S4

```cpp
VARIANT OSSupportUI::GetElasticMatAssignmentList(const VARIANT FAR&supportid,VARIANT FAR&nodeList)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `supportid` | `const VARIANT FAR&` | [in] | (Type: Long). |
| `nodeList` | `VARIANT FAR&` | [out] | (Type: Long). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE` | Successful. |
| `FALSE` | Unsuccessful. |

**Installed 2025 / 2026 interop:** `System.Object GetElasticMatAssignmentList(System.Object, System.Object ByRef)`

**Implemented managed entry:** `SupportQueryResult<IReadOnlyList<int>> GetElasticMatAssignedNodeIds(int supportId, int maximumResults = 100000)`<br>
**Return family:** `SupportQueryResult<IReadOnlyList<int>>`

**Payload, validation and limitations:** When to use: The returned IDs are analytical nodes, never plate IDs or support-definition IDs. Allocation uses qualified GetElasticMat detail, then Boolean list retrieval and a complete detail recheck. Exceeding maximumResults gives LimitExceeded before list allocation; zero assignments return an owned empty list after recheck. Changed count/direction/subgrade/flags gives StateChanged without payload or retry. Native order/duplicates are preserved; equal metadata does not detect every same-count edit.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. This implemented signature is callable; native qualification remains separate.

<a id="support-getcountofplatemat"></a>

### 29. GetCountOfPlateMat

**Section:** Support: Get Support Information Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#gaebf5cc1e4a8e0e3792695bb8e584102b), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#gaebf5cc1e4a8e0e3792695bb8e584102b) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#gaebf5cc1e4a8e0e3792695bb8e584102b).<br>
**Current typed implementation:** Implemented `SupportQueryResult<int> GetPlateMatDefinitionCount()`; live native qualification pending.<br>
**Main reference:** [GetPlateMatSupports native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L346)<br>
**Workstream:** S4

```cpp
VARIANT OSSupportUI::GetCountOfPlateMat()
```

No native parameters.

**Native return description:** The number of plate mat supports.

**Installed 2025 / 2026 interop:** `System.Object GetCountOfPlateMat()`

**Implemented managed entry:** `SupportQueryResult<int> GetPlateMatDefinitionCount()`<br>
**Return family:** `SupportQueryResult<int>`

**Payload, validation and limitations:** When to use: This count concerns plate-mat definitions, not plates, support nodes, assigned entities or maximum support reference. Zero is valid integral data. It does not resolve plate-mat ID/index or scalar/array contract gates; no plate assignments or subgrades are synthesized from this count.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. This implemented signature is callable; native qualification remains separate.

<a id="support-getplatematsupportid"></a>

### 30. GetPlateMatSupportId

**Section:** Support: Get Support Information Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#gaf19c74e5767bab8c670353de21a5b5c4), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#gaf19c74e5767bab8c670353de21a5b5c4) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#gaf19c74e5767bab8c670353de21a5b5c4).<br>
**Current typed implementation:** Pending.<br>
**Main reference:** [GetPlateMatSupports native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L351)<br>
**Workstream:** S4

```cpp
VARIANT OSSupportUI::GetPlateMatSupportId(const VARIANT FAR&varnPlateMatIndex)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `varnPlateMatIndex` | `const VARIANT FAR&` | [in] | PlateMat Index, start from 0(Type: Long). |


**Native return description:** The number of plate mat supports, -1 - invalid value for varnPlateMatIndex

**Installed 2025 / 2026 interop:** `System.Object GetPlateMatSupportId(System.Object)`

**Proposed managed entry:** `GetPlateMatSupportId(int plateMatIndex)`<br>
**Proposed return class:** `FoundationSupportReferenceResult`

**Payload, validation and limitations:** Input index explicitly starts at zero, output should identify support despite copied count return prose. Verify ID/index distinction and -1 invalid-index.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. These are proposed contracts, not callable APIs.

<a id="support-getplatematdetail"></a>

### 31. GetPlateMatDetail

**Section:** Support: Get Support Information Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga195575a1227391d9ed4921902779b769), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga195575a1227391d9ed4921902779b769) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga195575a1227391d9ed4921902779b769).<br>
**Current typed implementation:** Pending.<br>
**Main reference:** [GetPlateMatSupport native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L398), [PopulatePlateMatSupport native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L480)<br>
**Workstream:** S4

```cpp
VARIANT OSSupportUI::GetPlateMatDetail(const VARIANT FAR&varnPlateMatNo,VARIANT FAR&varDirection,VARIANT FAR&varSubgrade1,VARIANT FAR&varSubgrade2,VARIANT FAR&varSubgrade3,VARIANT FAR&varPrint,VARIANT FAR&varSpringType,VARIANT FAR&nAssignedPlateCount)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `varnPlateMatNo` | `const VARIANT FAR&` | [in] | (Type: Long). |
| `varDirection` | `VARIANT FAR&` | [out] | (Type: Long): Value Inclined Type 0 X Direction 1 Y Direction 2 Z Direction 3 X Only Direction 4 Y Only Direction 5 Z Only Direction 6 All Direction |
| `varSubgrade1` | `VARIANT FAR&` | Not named in parameter table | Not documented under this declaration name. |
| `varSubgrade2` | `VARIANT FAR&` | Not named in parameter table | Not documented under this declaration name. |
| `varSubgrade3` | `VARIANT FAR&` | Not named in parameter table | Not documented under this declaration name. |
| `varPrint` | `VARIANT FAR&` | [out] | (Type: Long) Value Print Influence 0 Not Checked 1 Checked |
| `varSpringType` | `VARIANT FAR&` | [out] | Generate Compression (Type: Long) Value Spring Type 0 None 1 Compression Only 2 Multi-Linear |
| `nAssignedPlateCount` | `VARIANT FAR&` | [out] | (Type: Long). |


**Additional or differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `varSubgrade` | [out] | Subgrade Modulus (Type: Double) |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE` | Successful. |
| `FALSE` | Unsuccessful. |

**Installed 2025 / 2026 interop:** `System.Object GetPlateMatDetail(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Proposed managed entry:** `GetPlateMat(int supportId)`<br>
**Proposed return class:** `PlateMatQueryResult`

**Payload, validation and limitations:** Preserve all three subgrades, direction including All=6, print/spring flags and plate assignment count. Prose collapses three outputs into one alias; verify axis ordering.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. These are proposed contracts, not callable APIs.

<a id="support-getplatematassignmentlist"></a>

### 32. GetPlateMatAssignmentList

**Section:** Support: Get Support Information Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#gadb8f00f7184d2f6684ebb3fcb0c53754), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#gadb8f00f7184d2f6684ebb3fcb0c53754) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#gadb8f00f7184d2f6684ebb3fcb0c53754).<br>
**Current typed implementation:** Pending.<br>
**Main reference:** [GetPlateMatSupport native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L414), [PopulatePlateMatSupport native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L492)<br>
**Workstream:** S4

```cpp
VARIANT OSSupportUI::GetPlateMatAssignmentList(const VARIANT FAR&varnPlateMatNo,VARIANT FAR&varPlateNos)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `varnPlateMatNo` | `const VARIANT FAR&` | Not named in parameter table | Not documented under this declaration name. |
| `varPlateNos` | `VARIANT FAR&` | Not named in parameter table | Not documented under this declaration name. |


**Additional or differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `supportid` | [in] | (Type: Long). |
| `nodeList` | [out] | (Type: Long). |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE` | Successful. |
| `FALSE` | Unsuccessful. |

**Installed 2025 / 2026 interop:** `System.Object GetPlateMatAssignmentList(System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetPlateMatAssignedPlateIds(int supportId)`<br>
**Proposed return class:** `SupportAssignmentListResult`

**Payload, validation and limitations:** Prototype is plate-based but prose copies elastic-mat node wording. Verify IDs against plate geometry; never hydrate via node resolver.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. These are proposed contracts, not callable APIs.

<a id="support-getcountofelasticfooting"></a>

### 33. GetCountOfElasticFooting

**Section:** Support: Get Support Information Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga6091112d45f222bd5c72971f8b555361), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga6091112d45f222bd5c72971f8b555361) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga6091112d45f222bd5c72971f8b555361).<br>
**Current typed implementation:** Implemented `SupportQueryResult<int> GetElasticFootingDefinitionCount()`; live native qualification pending.<br>
**Main reference:** No native target in the reference Support adapter.<br>
**Workstream:** S5

```cpp
VARIANT OSSupportUI::GetCountOfElasticFooting()
```

No native parameters.

**Native return description:** The number of elastic footing supports.

**Installed 2025 / 2026 interop:** `System.Object GetCountOfElasticFooting()`

**Implemented managed entry:** `SupportQueryResult<int> GetElasticFootingDefinitionCount()`<br>
**Return family:** `SupportQueryResult<int>`

**Payload, validation and limitations:** When to use: This is independent of the supported-node and elastic-mat counts. No documented footing index-to-ID enumeration is assumed; supply known support IDs to GetElasticFooting. Zero is valid, and a count is never decoded as a success flag.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. This implemented signature is callable; native qualification remains separate.

<a id="support-getelasticfootingdetail"></a>

### 34. GetElasticFootingDetail

**Section:** Support: Get Support Information Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga2fa98d49a023f379ef4e2d56dc5aa96b), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga2fa98d49a023f379ef4e2d56dc5aa96b) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga2fa98d49a023f379ef4e2d56dc5aa96b).<br>
**Current typed implementation:** Implemented `SupportQueryResult<ElasticFootingObservation> GetElasticFooting(int supportId)`; live native qualification pending.<br>
**Main reference:** No native target in the reference Support adapter.<br>
**Workstream:** S5

```cpp
VARIANT OSSupportUI::GetElasticFootingDetail(const VARIANT FAR&supportid,VARIANT FAR&length,VARIANT FAR&width,VARIANT FAR&dir,VARIANT FAR&subgrade,VARIANT FAR&nodesCount)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `supportid` | `const VARIANT FAR&` | [in] | Support reference ID (Type: Long). |
| `length` | `VARIANT FAR&` | [out] | Length of footing (Type: Double). |
| `width` | `VARIANT FAR&` | [out] | Width of footing (Type: Double). |
| `dir` | `VARIANT FAR&` | [out] | Direction of the resistance of the spring supports (Type: Long): Value Direction 0 X Direction 1 Y Direction 2 Z Direction 3 X Only Direction 4 Y Only Direction 5 Z Only Direction |
| `subgrade` | `VARIANT FAR&` | [out] | Subgrade Modulus of the soil (Type: Double). |
| `nodesCount` | `VARIANT FAR&` | [out] | Count of assigned nodes (Type: Long). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | if the method is successful. |
| `0` | if the method is unsuccessful. |

**Installed 2025 / 2026 interop:** `System.Object GetElasticFootingDetail(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Implemented managed entry:** `SupportQueryResult<ElasticFootingObservation> GetElasticFooting(int supportId)`<br>
**Return family:** `SupportQueryResult<ElasticFootingObservation>`

**Payload, validation and limitations:** When to use: Unlike GetElasticMat, the native footing packet includes finite positive length and width as well as native direction 0..5, scalar subgrade and assigned-node count. Known support definition ID is required. Native 1/true succeeds, 0/false fails; integral -1 is not VARIANT_TRUE for this narrower contract. Lengths remain native length units and subgrade remains force/length cubed; no area, spring stiffness or reaction is derived.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. This implemented signature is callable; native qualification remains separate.

<a id="support-getelasticfootingassignmentlist"></a>

### 35. GetElasticFootingAssignmentList

**Section:** Support: Get Support Information Support<br>
**Cross-version status:** Common declaration and non-example contract in [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html#ga43dc24b4de9c9362ccb2716faa1e22d4), [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html#ga43dc24b4de9c9362ccb2716faa1e22d4) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga43dc24b4de9c9362ccb2716faa1e22d4).<br>
**Current typed implementation:** Implemented `SupportQueryResult<IReadOnlyList<int>> GetElasticFootingAssignedNodeIds(int supportId, int maximumResults = 100000)`; live native qualification pending.<br>
**Main reference:** No native target in the reference Support adapter.<br>
**Workstream:** S5

```cpp
VARIANT OSSupportUI::GetElasticFootingAssignmentList(const VARIANT FAR&supportid,VARIANT FAR&nodeList)
```

| Native parameter | Declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `supportid` | `const VARIANT FAR&` | [in] | Support reference ID (Type: Long). |
| `nodeList` | `VARIANT FAR&` | [out] | List of assigned node ID (Type: Long). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | if the method is successful. |
| `0` | if the method is unsuccessful. |

**Installed 2025 / 2026 interop:** `System.Object GetElasticFootingAssignmentList(System.Object, System.Object ByRef)`

**Implemented managed entry:** `SupportQueryResult<IReadOnlyList<int>> GetElasticFootingAssignedNodeIds(int supportId, int maximumResults = 100000)`<br>
**Return family:** `SupportQueryResult<IReadOnlyList<int>>`

**Payload, validation and limitations:** When to use: Uses qualified footing detail/list/detail, retaining node identity and every observed length/width/direction/modulus/count in its state check. Native 1/true is success; 0/false is failure and numeric -1 is rejected rather than treated as true. Empty assignment is a rechecked owned empty list. LimitExceeded occurs before list allocation. StateChanged has no payload; same-count edits can remain undetected, so coordinate session access. No node geometry is hydrated or reordered.

Return the operation identity, semantic outcome and actual native code when present. Expose native data only after its status/type/shape is qualified; preserve explicit absence and unsupported interpretations. This implemented signature is callable; native qualification remains separate.

## 4. Native implementation helpers excluded

All seven additional class names match across the three reviewed listings and are absent from IOSSupportUI metadata. They are implementation/lifecycle methods, not extra support API work. Do not expose CStruct pointers, bookkeeping operations or final-release of shared RCWs to reproduce them.

| Helper | Native declaration | Class references |
| --- | --- | --- |
| `OnFinalRelease` | `virtual void OSSupportUI::OnFinalRelease()` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_support_u_i.html#a8a4fa8b15e1cd91f1e4861206210f191) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_support_u_i.html#a8a4fa8b15e1cd91f1e4861206210f191) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_support_u_i.html#a8a4fa8b15e1cd91f1e4861206210f191) |
| `SetStructure` | `void OSSupportUI::SetStructure(CStruct*pStruct)` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_support_u_i.html#a5cfd33334fc86671f1e70bd14efe92bc) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_support_u_i.html#a5cfd33334fc86671f1e70bd14efe92bc) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_support_u_i.html#a5cfd33334fc86671f1e70bd14efe92bc) |
| `UpdateSupportTable` | `void OSSupportUI::UpdateSupportTable(long nNewSupport)` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_support_u_i.html#a691a3b186d6b811adfcbb00b0f4adcf3) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_support_u_i.html#a691a3b186d6b811adfcbb00b0f4adcf3) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_support_u_i.html#a691a3b186d6b811adfcbb00b0f4adcf3) |
| `RecordAddSupport` | `void OSSupportUI::RecordAddSupport(long nSupportNo)` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_support_u_i.html#aed292dca2afbed6a0584e979c3be906a) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_support_u_i.html#aed292dca2afbed6a0584e979c3be906a) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_support_u_i.html#aed292dca2afbed6a0584e979c3be906a) |
| `AssignSupportHelper` | `BOOL OSSupportUI::AssignSupportHelper(long&nNodeNo,long&nSuppNo)` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_support_u_i.html#adb979f14dc0ee1869cf42ac4a3bce2f2) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_support_u_i.html#adb979f14dc0ee1869cf42ac4a3bce2f2) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_support_u_i.html#adb979f14dc0ee1869cf42ac4a3bce2f2) |
| `RemoveSupportHelper` | `BOOL OSSupportUI::RemoveSupportHelper(long&nodeNo)` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_support_u_i.html#adb00b60ec297530f28eefcbbd035fd8a) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_support_u_i.html#adb00b60ec297530f28eefcbbd035fd8a) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_support_u_i.html#adb00b60ec297530f28eefcbbd035fd8a) |
| `AssignPlateSupportHelper` | `void OSSupportUI::AssignPlateSupportHelper(long&nPlateNo,long&nSuppNo)` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_support_u_i.html#a2b2fef4b029517f15a6bf42c83ca955c) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_support_u_i.html#a2b2fef4b029517f15a6bf42c83ca955c) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_support_u_i.html#a2b2fef4b029517f15a6bf42c83ca955c) |

## 5. Main-source managed surface

Every one of the 33 interface overloads has a corresponding public adapter method. The table records exact source signatures and direct native calls within each method; methods without such calls can compose other methods or perform managed filtering. Fluent return values, partial switch branches and obsolete overloads are not proof that a requested native effect occurred.

| Managed source signature | Interface / adapter | Direct native targets | Porting decision |
| --- | --- | --- | --- |
| `IOSSupport AssignSupportToNode(ISupport supportInstance, int nId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L49) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L67) | `AssignSupportToNode` | Named status/data result; validate absent/unsupported/type distinctions |
| `IOSSupport AssignSupportToNode(ISupport supportInstance, Node node)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L54) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L60) | `AssignSupportToNode` | Named status/data result; validate absent/unsupported/type distinctions |
| `IOSSupport AssignSupportToNodes(ISupport supportInstance, IEnumerable<int> nodeIds)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L59) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L46) | Managed composition/filtering | Named status/data result; validate absent/unsupported/type distinctions |
| `IOSSupport AssignSupportToNodes(ISupport supportInstance, IEnumerable<Node> nodes)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L64) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L53) | Managed composition/filtering | Named status/data result; validate absent/unsupported/type distinctions |
| `IOSSupport CreateSupport(ISupport supportInstance)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L73) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L74) | `CreateSupportPinned`, `CreateSupportFixed`, `CreateSupportFixedBut`, `CreateElasticFooting`, `CreateElasticMat`, `CreatePlateMat` | Typed request dispatch; reject unsupported/no-op branches explicitly |
| `HashSet<ISupport> GetAllElasticMatSupports(IEnumerable<Node> allNodes)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L82) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L329) | Managed composition/filtering | Obsolete(error=true): requires identity/discovery repair |
| `HashSet<ISupport> GetAllElasticMatSupports(IEnumerable<Node> allNodes, IEnumerable<int> emSupportIds)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L87) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L293) | Managed composition/filtering | Named status/data result; validate absent/unsupported/type distinctions |
| `HashSet<ISupport> GetAllElasticMatSupports(IOSGeometry geometry)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L92) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L309) | Managed composition/filtering | Obsolete(error=true): requires identity/discovery repair |
| `HashSet<ISupport> GetAllElasticMatSupports(IOSGeometry geometry, IEnumerable<int> emSupportIds)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L97) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L278) | Managed composition/filtering | Named status/data result; validate absent/unsupported/type distinctions |
| `IEnumerable<ISupport> GetAllNodalSupports(IEnumerable<Node> allNodesInModel)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L106) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L638) | Managed composition/filtering | Named status/data result; validate absent/unsupported/type distinctions |
| `IEnumerable<ISupport> GetAllNodalSupports(IOSGeometry geometry)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L111) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L631) | Managed composition/filtering | Named status/data result; validate absent/unsupported/type distinctions |
| `HashSet<ISupport> GetAllPlateMatSupports(IEnumerable<Plate> allPlates)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L120) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L255) | Managed composition/filtering | Named status/data result; validate absent/unsupported/type distinctions |
| `HashSet<ISupport> GetAllPlateMatSupports(IOSGeometry geometry)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L125) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L244) | Managed composition/filtering | Named status/data result; validate absent/unsupported/type distinctions |
| `HashSet<Node> GetAllSupportNodes(IOSGeometry geometry, int nThreads)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L134) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L725) | Managed composition/filtering | Clarify actual threading; preserve native session ownership |
| `HashSet<Node> GetSupportedNodes(HashSet<Node> fromNodes, int nThreads)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L139) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L717) | Managed composition/filtering | Clarify actual threading; preserve native session ownership |
| `IEnumerable<ISupport> GetAllSupports(IEnumerable<Node> forNodes, IEnumerable<Plate> forPlates, IEnumerable<Plate> allPlates)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L148) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L38) | Managed composition/filtering | Named status/data result; validate absent/unsupported/type distinctions |
| `Dictionary<int, Enums.SupportType> GetDistinctSupports()` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L153) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L208) | `GetSupportType`, `GetSupportInformationEx` | Repair buffer type/directions and stable identity before enabling |
| `ElasticMatSupport GetElasticMatSupport(int emsId, Func<int, Node> getNodeById)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L158) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L430) | `GetElasticMatDetail`, `GetElasticMatAssignmentList` | Named status/data result; validate absent/unsupported/type distinctions |
| `HashSet<ISupport> GetElasticMatSupports(IEnumerable<int> emSupportIds, Func<int, Node> getNodeById)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L163) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L366) | `GetCountOfElasticMat` | Named status/data result; validate absent/unsupported/type distinctions |
| `IEnumerable<ISupport> GetNodalSupports(IEnumerable<Node> forNodes)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L168) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L643) | Managed composition/filtering | Named status/data result; validate absent/unsupported/type distinctions |
| `PlateMatSupport GetPlateMatSupport(int pmsId, Func<int, Plate> getPlateById)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L173) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L388) | `GetPlateMatDetail`, `GetPlateMatAssignmentList` | Named status/data result; validate absent/unsupported/type distinctions |
| `HashSet<ISupport> GetPlateMatSupports(Func<int, Plate> getPlateById)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L178) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L344) | `GetCountOfPlateMat`, `GetPlateMatSupportId` | Named status/data result; validate absent/unsupported/type distinctions |
| `HashSet<ISupport> GetPlateMatSupports(IEnumerable<Plate> forPlates, IEnumerable<Plate> allPlates)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L183) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L260) | Managed composition/filtering | Named status/data result; validate absent/unsupported/type distinctions |
| `ISupport GetSupport(int nId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L192) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L562) | `GetSupportInformationEx` | Named status/data result; validate absent/unsupported/type distinctions |
| `ISupport GetSupport(int nId, IEnumerable<ISupport> allSupports)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L197) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L622) | Managed composition/filtering | Named status/data result; validate absent/unsupported/type distinctions |
| `IEnumerable<int> GetSupportNodeIds()` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L202) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L165) | `GetSupportNodes` | Named status/data result; validate absent/unsupported/type distinctions |
| `HashSet<Node> GetSupportNodes(HashSet<Node> allNodes)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L207) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L141) | Managed composition/filtering | Named status/data result; validate absent/unsupported/type distinctions |
| `HashSet<Node> GetSupportNodes(IOSGeometry geometry)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L212) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L155) | Managed composition/filtering | Named status/data result; validate absent/unsupported/type distinctions |
| `int GetSupportNodesCount()` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L217) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L126) | `GetSupportCount` | Named status/data result; validate absent/unsupported/type distinctions |
| `Enums.SupportType GetSupportTypeExt(int nodeNumber)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L222) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L545) | `GetSupportType` | Named status/data result; validate absent/unsupported/type distinctions |
| `Dictionary<int, Enums.SupportType> GetSupportTypesOfNodes(IEnumerable<int> nodeIds)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L227) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L186) | `GetSupportType` | Named status/data result; validate absent/unsupported/type distinctions |
| `ElasticMatSupport PopulateElasticMatSupport(ElasticMatSupport emSupport, Func<int, Node> getNodeById)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L236) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L508) | `GetElasticMatDetail`, `GetElasticMatAssignmentList` | Update domain objects only after all required fields are qualified |
| `PlateMatSupport PopulatePlateMatSupport(PlateMatSupport pmSupport, Func<int, Plate> getPlateById)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs#L241) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L470) | `GetPlateMatDetail`, `GetPlateMatAssignmentList` | Update domain objects only after all required fields are qualified |

### Source limitations requiring adaptation

| Finding | Reference evidence | Required porting treatment |
| --- | --- | --- |
| Enforced creation is commented out | CreateSupport switch branches for Enforced/EnforcedBut contain no active native call. | Explicit Unsupported outcome; no returning fluent success or guessing an inclined alternative. |
| Incomplete support family lookup | GetSupport only handles type codes 1–5; foundations and other families return null. | Preserve NoSupport, Unsupported, NativeFailure and every type 0–13/unknown distinctly. |
| Aggregate GetAllSupports is incomplete | Composes that nodal lookup with plate mats; elastic mat/footing and inclined/generated/multilinear are not completed by this path. | Document scope and compose qualified per-family extraction without silently dropping members. |
| Distinct-support scan and dependent obsolete overloads | Spring buffer is Int32[6], native spring contract says Double; buffers reused and flags unchecked; two automatic elastic-mat overloads marked obsolete. | Correct buffers and per-node qualification; verify extended-query directions and map IDs safely. |
| Plate-mat scalar values dropped | GetPlateMatSupport and PopulatePlateMatSupport read three subgrades but store only the first. | Preserve all three and direction=6 with verified axis mapping. |
| Plate-mat packet disagrees with prose | CreateSupport always passes a three-value array and source direction enum omits All=6. | Typed scalar/all-direction requests plus native fixtures resolving example/prose conflict. |
| Creation/assignment statuses ignored | Native IDs copied into models and assignment fluent returns used without qualification. | Validate operation-specific codes/IDs; apply model changes only for qualified outcomes. |
| Unit conversions disagree | ElasticMat/Footing use force/volume; PlateMat uses force/length; Springs uses force/length for both translational and rotational components. | Establish physical/native dimensions; verify subgrade as pressure/displacement and rotational moment/radian separately. Do not copy a common numerical factor blindly. |
| Spring null serialization | Springs.ToArray accesses nullable.Value in its null branch. | Explicit finite required-value validation or documented absent-state policy; no hidden null dereference. |
| Spring state lost in Boolean releases | Releases.Parse maps values >0 to true, collapsing -1 spring into the same Boolean as fixed. | Use three-state DOF outcomes plus stiffness, retaining raw state codes. |
| Support equality/identity is incomplete | Support.Equals returns false for distinct instances even with same ID/type; ValueEquals throws. | Stable model/session-qualified keys, correct equality and collection ownership; do not port unused throwing helpers. |
| Factory registration is global reflection | AppDomain assembly scan and ToDictionary can fail on duplicate support-type attributes. | Internal deterministic registration with supported/unknown outcomes and no hidden mutable global catalog. |
| Missing resolver results and stale collections | Foundation hydration can add null resolved entities and clears collections before fully qualifying data. | Return unresolved IDs separately; snapshot and replace only qualified collections. |
| Thread count naming is misleading | GetSupportedNodes does not use nThreads; GetAllSupportNodes passes it into Geometry enumeration. | Document actual execution; no implied native parallel safety guarantee. |

## 6. Reuse and dependency inventory

| Source family | Reusable functionality | Boundary |
| --- | --- | --- |
| ISupport and Support subclasses | Fixed/pinned/fixed-but/enforced representations, mat/footing identity, assigned nodes/plates. | Adapt to existing property-store/units primitives; complete absent family and ID semantics. |
| Releases / Restraints / Springs | Six-DOF named fields and transformations. | Preserve Fixed/Released/Spring as separate states; validate component units and raw array subtype. |
| SupportType / foundation enums | Native 0–13 categories, directions and spring type choices. | Separate query statuses from types, unknown codes and All=6 only for plate mats; no generic enum accepted for every family. |
| SupportFactory and type attribute | Polymorphic model selection. | Keep registration internal/deterministic; do not use ErrorSupport as a silent substitute for unknown native data. |
| SupportConditions | Settlement-related support-model selection/cache and mapping. | Later managed workflow; enforced types remain subject to real native support. Settlement loads belong to Load, not an extra Support native function. |
| StaadSupportSerializer and JSON converters | Named type-discriminated model serialization with TypeNameHandling.None. | Optional reuse with schema/unknown-type/immutable-snapshot review; no arbitrary runtime type activation. |
| SupportStructureFramesExtractionService | Structural framing/projection workflow. | Not boundary-condition COM extraction; keep separate from this 35-function port. |
| Current repository | Wrapper.RawSupport/StaadVersion, geometry entities, units and native-value readers. | Reuse existing bindings without wrapper changes or proprietary interop references. |

## 7. Reproducibility evidence

### Group pages

| Year | Page | Build | Names / details | SHA-256 |
| --- | --- | --- | --- | --- |
| 2024 | [group___support_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___support_group.html) | 24.00.02.354 | 0 / 0 | `fa9c353afc1461c7463409aa78c5ca3fa7a62125410b30cd9792bd8f56d8d5aa` |
| 2024 | [group___property_create_support.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html) | 24.00.02.354 | 20 / 20 | `e40fbbe8dd9208ea40a072f3c405e4fbf00d38e25fcb546e95a27d7c791ec18e` |
| 2024 | [group___property_get_support.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_support.html) | 24.00.02.354 | 15 / 15 | `b6a6c9b4603cff2fcb5003e176b50664ce7681ca852c4c3e28ac875a5d320810` |
| 2025 | [group___support_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___support_group.html) | 25.00.01.424 | 0 / 0 | `60106da18400a5791d88b22c0d6e711cdef14ddc5925f721a0ec2200af107544` |
| 2025 | [group___property_create_support.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html) | 25.00.01.424 | 20 / 20 | `44ee86e35b600489214350bcc7d141dfa6cc86ff528d629a672b1095e35f4ae9` |
| 2025 | [group___property_get_support.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_support.html) | 25.00.01.424 | 15 / 15 | `e16f117293b03f8b464d81ee553e1fee96cac4a97cc42ebba5b546535aa14a65` |
| 2026 | [group___support_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___support_group.html) | 26.00.00.340 | 0 / 0 | `8dd380f21589db299d316fbb45203e528d6320b71203d7613467ef80ab337232` |
| 2026 | [group___property_create_support.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html) | 26.00.00.340 | 20 / 20 | `2a406309dc1f8131a96f8e71b7fcea318683a8a8cb9eedf438d1ee7944e6f94e` |
| 2026 | [group___property_get_support.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html) | 26.00.00.340 | 15 / 15 | `1a16b52b4b8b16841dfc641b923a11fa14ad4e644f19e382a6c771046de2436c` |

### Native class and generated headers

| Year | Source | Listed names | SHA-256 |
| --- | --- | --- | --- |
| 2024 | [Native Support class](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_support_u_i.html) | 42 | `43a5d01bfea955c0607e799c2a24f0ba4cb5c5f0ad846227e36d5d09dfe3de66` |
| 2025 | [Native Support class](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_support_u_i.html) | 42 | `73676d1732a236972512a2721d40c41d400a7f82dbf8c958585acd9abb851514` |
| 2026 | [Native Support class](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_support_u_i.html) | 42 | `6d0fdea8e42eb26638bf5976fa37e5995df3d7e0f64286a3ca80c06066bfeb02` |
| 2024 | [Generated Support header](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/ossupportui_8h_source.html) | Header declarations | `991e666df2334ca7c3b55e6fdc5dc8366c0ad32b704bbbc1b76ac7533fcb8e55` |
| 2025 | [Generated Support header](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/ossupportui_8h_source.html) | Header declarations | `66ca14e51cde72e60bc7f1c7a898fbcec711d6f21b253435b2f0363781ef6b7d` |
| 2026 | [Generated Support header](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/ossupportui_8h_source.html) | Header declarations | `0ffacf030a89edd9340cc9fcfc6c962d5df2112f5c54e8a4f1107b640b61a0fd` |

### Vendor metadata

| Year | Interop assembly | IOSSupportUI methods | SHA-256 |
| --- | --- | --- | --- |
| 2025 | [Installed interop](file:///C:/Program%20Files/Bentley/Engineering/STAAD.Pro%202025/STAAD/StaadPro.Interop.dll) | 35 | `CC724993AC9119138B7323E8C684997257E2851DE62E1551BA5D6D67A9DADC19` |
| 2026 | [Installed interop](file:///C:/Program%20Files/Bentley/Engineering/STAAD.Pro%202026/STAAD/StaadPro.Interop.dll) | 35 | `6D153E80ED356A1B167ABB2EF19227FCFF510707A578AD304154652CC6C1F6D4` |

Both identities are `StaadPro.Interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b4d201f8319cef86`. Method names, CLR returns/parameters and metadata match across 2025/2026. No enum parameter types appear in this interface metadata. No 2024 interop DLL was found at the corresponding expected installed path; this is not proof of native COM absence.

### Current source fingerprints

| Source | SHA-256 |
| --- | --- |
| [IOSSupport.cs](../../src/StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs) | `4a3f0f9fa4f7f4ff33838a254c87b16d19f8885c9b1e18c80fbc129b658dff75` |
| [OSSupportAdapter.cs](../../src/StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs) | `9838a58497367cd308ecd152001dbc0e197567eceb49172f3bf154de1ea69f46` |
| [OpenStaadWrapper.cs](../../src/StaadPro.Interop/Models/OpenStaadWrapper.cs) | `1c0b69fe545a379262ad70cb1d1f9c45de872aba92e6eb4df218441455628692` |
| [UnitsConverter.cs](../../src/StaadPro.Interop/Common/Units/UnitsConverter.cs) | `ada5c77a1b653d1b2ffde985978bfbe59149a426481276e0504ef0877c5eea8d` |
| [StaadPro.Interop.csproj](../../src/StaadPro.Interop/StaadPro.Interop.csproj) | `035570ef3e35b52a2772e2ec96b9bf886ce3d08d6e555c3d51ffd870de9f1a59` |

### Strictly read-only main-source fingerprints

| Source | SHA-256 |
| --- | --- |
| [Adapters/Interfaces/IOSSupport.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs) | `be21566bc6d36663bc54d55210f1541702dc394dc69c412d5c624d6dbbff78c7` |
| [Adapters/Models/OSSupportAdapter.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs) | `80b5f2c3069cfaba32cb832423f2cc0f633c25434df5e29d14493d41df260a8b` |
| [Entities/Supports/ElasticFootingSupport.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/Supports/ElasticFootingSupport.cs) | `9653afa0d115b2c75916f4e379294105f8eea6928bef00df538f02806de741f5` |
| [Entities/Supports/ElasticMatSupport.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/Supports/ElasticMatSupport.cs) | `0dc3a08953c745ad0f8a962bd57634ef01628010b6330f3971fbd225bb585788` |
| [Entities/Supports/EnforcedButSupport.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/Supports/EnforcedButSupport.cs) | `406c408fba7a6c75767621537baf377789894e72b8ff8324ed2bab6da1e2ea23` |
| [Entities/Supports/EnforcedSupport.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/Supports/EnforcedSupport.cs) | `990f0c53d0e2bb7cfb29bef8faa22d3f5b0d10d4a3ee836fc6a6be84781e3852` |
| [Entities/Supports/ErrorSupport.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/Supports/ErrorSupport.cs) | `2f046527fa09fa0564b88f6b5406bdfc96e5e4419d2dd0918b1f69da8c942c83` |
| [Entities/Supports/FixedButSupport.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/Supports/FixedButSupport.cs) | `322d60636bd1fd982975e98e2aade533d91bcc98cc2d5f954cfd5ab00fef1002` |
| [Entities/Supports/FixedSupport.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/Supports/FixedSupport.cs) | `ab0b123f382ba693ee386d9a2dd4e4783f8feb5e6eda57532dafd5b54f50464b` |
| [Entities/Supports/NoSupport.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/Supports/NoSupport.cs) | `61ef07079e9dcaa7e694cf3f52b4d28a6e9e70b448425c2cf286c00aa1b1d9b7` |
| [Entities/Supports/PinnedSupport.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/Supports/PinnedSupport.cs) | `b52f480e70db66082b2bcd1e988da171e6b6228279d334f89b0f7d7d504c5886` |
| [Entities/Supports/PlateMatSupport.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/Supports/PlateMatSupport.cs) | `0c23f8f203c637fbf11330e899fb08bff6112da173f8e47b2e5ef788ce35464d` |
| [Entities/Supports/Restraints.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/Supports/Restraints.cs) | `58a87425b7a3d5a89787d9adc7e1e96eef56cc8434d5c185ac458a1eae8b36f8` |
| [Base/Entities/Support.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Base/Entities/Support.cs) | `2ea5f2ff2fa4a715d86326b9b6283a43f19a669df4152d7613fe340dd4c88745` |
| [Base/Entities/RestrainedSupport.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Base/Entities/RestrainedSupport.cs) | `bec8d4d9363c211e7ffc2b0b4d0e9e4da40f32dab8118bd1681deb5b81742d7d` |
| [Base/Entities/ReleasedSupport.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Base/Entities/ReleasedSupport.cs) | `4efff4a754ac6cf2e545d1bf0807e547daed20ed470c81809ddf1a508699e3e0` |
| [Base/Entities/MatSupport.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Base/Entities/MatSupport.cs) | `e293a927212956df11e71c8b7929a54dce4ed520ffde084571fba2ecc025ff13` |
| [Entities/Releases.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/Releases.cs) | `53a5a6e2cf925e5d990ea635d117703c6623faccdd09fef0b06972ab3d031c9b` |
| [Entities/Springs.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/Springs.cs) | `d361194cdba27598b3a8db01bb51f157cf0d199a4eb84bf4423070650ada3040` |
| [Interfaces/ISupport.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Interfaces/ISupport.cs) | `e74dcd629785810ee9cf060b022c2163986dce4c13c0ffd53c2e991c53874c0d` |
| [Interfaces/ISupportConditions.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Interfaces/ISupportConditions.cs) | `f67dac38f95da41b952791d2b6d0f84619b5a7aed9667ecaec70ac59b0961deb` |
| [Enums/SupportType.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Enums/SupportType.cs) | `ef71c267adc43e56fb8436da6d0777b9dbfcb6cc8bbcb2a86dff2072ec48dfac` |
| [Enums/FoundationSupportDirection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Enums/FoundationSupportDirection.cs) | `dec60f616976c8efb7a3909e43a87b1f663a83c783417c49f3f2ed4bb82d7407` |
| [Enums/FoundationSpringType.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Enums/FoundationSpringType.cs) | `af64e8c13d7b80241e04659b393d2fa613bc5afe3014f5650856586db807982a` |
| [Factories/SupportFactory.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Factories/SupportFactory.cs) | `7ac1dc5e8643d289b890e31119d034834fcbd44989c1daf564c4d04dbb0cf7f4` |
| [Attributes/StaadSupportTypeAttribute.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Attributes/StaadSupportTypeAttribute.cs) | `83c74b74125e110f8b704873ccfdc78750a5d2b6c0c0f38414ece2702763aa32` |
| [Services/SupportTypeValues.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/SupportTypeValues.cs) | `67e1affcb3cbb0bfed5ea904e8c2d74602d2d523a57876a6c87f6795685225d6` |
| [Services/StaadSupportSerializer.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/StaadSupportSerializer.cs) | `c66173a7b95517f3d47494eddea1448034cfe4e7342f0fc8b3933ac4945e5d6f` |
| [Services/SupportStructureFramesExtractionService.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/SupportStructureFramesExtractionService.cs) | `89806fbd42c981d9df56a95d8f48dd68a00c949df1560cedaae2c96354cad0b3` |
| [Models/SupportConditions.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/SupportConditions.cs) | `4031c19c97a603119f80ae0f417de2c06dbccc12b9ff25ab884b8455bbbf03b5` |
| [JsonConverters/StaadSupportJsonConverter.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/JsonConverters/StaadSupportJsonConverter.cs) | `b097653dd66a91c7c5e08b2e0ce07a033211c00ff447351b9e9b95e2f46ea0ea` |
| [JsonConverters/SupportConditionsJsonConverter.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/JsonConverters/SupportConditionsJsonConverter.cs) | `610bacb9c6dbb9cc9d40ac8c158575abdb1e37093b853458db389594d3d0d1b9` |
| [Models/OpenStaadWrapper.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OpenStaadWrapper.cs) | `83737733d57a672313385f99ea0ca8ffa10f1917fb6a33b89e43ce27e3f37b14` |

## 8. Verification limits

No native COM activation, engineering fixture, library test run, target change or NuGet build/publication occurred. Analysis utilities only parsed files and inspected metadata. Equal documentation and metadata do not prove successful marshaling, servicing-build compatibility, unit correctness or engineering behavior. Main-project files were read-only and were not built.

Local help/main-source links depend on the corresponding files on the reader’s computer. The reports and comparison data preserve the reviewed contracts independently; native verification requires an appropriate licensed installation.
