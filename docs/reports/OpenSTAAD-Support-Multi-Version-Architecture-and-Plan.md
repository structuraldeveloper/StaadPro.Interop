# OpenSTAAD Support multi-version comparison, architecture and implementation plan

**Implementation checkpoint, 26 October 2026:** 27/35 documented native routes are implemented; eight foundation/identity/transport gates remain. See [the current contracts and usage](../Commands-and-Support-Porting.md). Earlier review counts and proposals below retain their original review context.

**Review date:** 23 October 2026<br>
**Baseline:** main at 687420d96014f203152fcf2e40979e5b5f351b1c<br>
**Scope:** Installed STAAD.Pro 2024, 2025 and 2026 Support help, native class/header listings, passive interop metadata, current repository coverage and the strictly read-only main project.<br>
**Implementation updated:** 26 October 2026<br>
**Status:** 27 common documented routes implemented; eight remain pending; native qualification remains separate.

## Current implementation - foundation and mutation contracts

Implemented **19 common documented Support routes**, leaving **16/35 pending**. The new surface covers supported-node discovery, typed support information and GUIDs, foundation counts/readback, explicit fixed/pinned/fixed-but creation, assignment, unassignment, deletion and void GUID invocation. One internal shared service validates operation-specific counts/types/flags, owned bounded buffers and binding lifetime. Reused existing Releases/Springs packets and ported the main-project enums without private UI dependencies. See [all 19 contracts](../Support-Foundations.md).

Release build passed with zero warnings/errors and **5,523 offline tests** (170 new cases). Isolated documentation/package verification covers **1,052 selected XML examples**, including all 92 Output and 38 Support examples. Managed fixtures do not certify live 2024–2026 COM or engineering behavior.

## 1. Findings and recommended direction

All three installed references document the same **35 detailed Support functions** across three group pages. Their normalized declarations and non-example contracts match. All 35 examples change between 2024 and 2025; examples match between 2025 and 2026. Example changes do not establish API differences.

Both inspected 2025/2026 IOSSupportUI interfaces expose the same 35 methods, with matching CLR signatures and parameter metadata. There are no help-only or metadata-only automation names in the reviewed scope. Native class listings contain seven additional implementation/lifecycle helpers that must not be ported as public support operations.

**Use one shared internal Support implementation.** Current evidence does not justify separate year-specific services or year-exclusive public interfaces. Capture the acquired wrapper.StaadVersion for diagnostics and future verified compatibility rules, but do not invent a 2024/2025/2026 behavior split.

The current IOSSupport / OSSupportAdapter expose 19 typed domain methods and dispatch 19/35 native Support routes. The main project contains 33 matching interface/public-adapter overloads backed by eighteen distinct native names. Seventeen native names have no main-adapter target. Two source overloads are obsolete(error=true), and several other paths are incomplete or lose data.

| Measure | Finding |
| --- | --- |
| Group pages per version | 3 |
| Detailed native functions per version | 35 |
| Union / intersection of names | 35 / 35 |
| Common declaration changes | 0 |
| Common non-example contract changes | 0 |
| Functions with example changes, 2024 to 2025 | 35 |
| Normalized changes, 2025 to 2026 | 0 |
| Current interface / adapter domain overloads | 19 / 19 |
| Current native Support dispatches | 19 |
| Main interface / public adapter overloads | 33 / 33 |
| Main private adapter helpers / extra public methods | 0 / 0 |
| Distinct main native targets | 18 |
| Native names without a main-adapter target | 17 |
| Main obsolete(error=true) overloads | 2 |
| Methods in each inspected IOSSupportUI | 35 |
| Supplemental automation-only names | 0 |
| Native class names / excluded helpers | 42 / 7 |
| Separate year implementations justified by evidence | 0 |

Nineteen names have typed managed mappings; sixteen remain pending. RawSupport and ComObject are real bindings, not completed domain coverage. Thirty-three managed overloads do not imply thirty-three native operations, and source presence does not prove live behavior.

The [contract inventory](OpenSTAAD-Support-API-Coverage-and-Contract-Inventory.md) records every function, parameter, logical direction, native return, source mapping and proposed managed signature. The [comparison data](data/OpenSTAAD-Support-2024-2026-Comparison.json) preserves per-year evidence and fingerprints.

## 2. References and review method

| Reference | Documented build | Detailed functions |
| --- | --- | --- |
| [STAAD.Pro 2024 Support](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___support_group.html) | 24.00.02.354 | 35 |
| [STAAD.Pro 2025 Support](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___support_group.html) | 25.00.01.424 | 35 |
| [STAAD.Pro 2026 Support](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___support_group.html) | 26.00.00.340 | 35 |

| Section | Functions per year |
| --- | --- |
| Support grouping page | 0 |
| Creating supports | 20 |
| Querying supports | 15 |

All subgroup links were followed, and other help group pages were scanned for OSSupportUI ownership. No additional Support-owned function rows were found. The child filenames include “property”; that is a help naming convention, not evidence that these calls belong to IOSProperty.

The review reconciled summary/detail counts, parsed complete parameter/return and nested value tables, and compared declarations, non-example text and examples separately. Generated HTML navigation, tooltip definitions, styling and syntax headings were excluded from semantic comparison.

Roslyn analysis covered both main Support files and their models/factory/serializer dependencies. No additional native targets were found in the inspected dependencies. Every main file was read-only; none was built.

Reflection-only inspection ran in separate Windows PowerShell processes for installed 2025/2026 assemblies, avoiding their identical assembly identity being unified. Their binary hashes differ, but their Support metadata matches. No corresponding 2024 interop DLL was found at the expected installed STAAD path; that limits ABI evidence and does not prove native COM absence.

No COM object was activated and no native function was invoked. Matching documentation/metadata is not proof of marshaling, engineering correctness, runtime availability or compatibility with every servicing build.

## 3. Complete cross-version function matrix

Every row is a native OSSupportUI name with a detailed contract in all three years. Nineteen current typed implementations are mapped below; sixteen remain pending. Return types below are native declarations, not proposed managed results.

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
| `CreateElasticMat` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga49d5757efd9c46e1acd59260b1ead221) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga49d5757efd9c46e1acd59260b1ead221) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga49d5757efd9c46e1acd59260b1ead221) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L108) | S7 | Implemented / common service |
| `RemoveElasticMat` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga153b6a29601fbe3ff99e9b5d559433a7) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga153b6a29601fbe3ff99e9b5d559433a7) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga153b6a29601fbe3ff99e9b5d559433a7) | No target | S8 | Implemented / common service |
| `RemoveElasticMatFromNode` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga5f7616e7bb3baf0f1e8e3f8c1984c701) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga5f7616e7bb3baf0f1e8e3f8c1984c701) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga5f7616e7bb3baf0f1e8e3f8c1984c701) | No target | S8 | Implemented / common service |
| `AssignSupportToEntityList` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga86ef9166a6e38cc1d19f0138fb01d07b) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga86ef9166a6e38cc1d19f0138fb01d07b) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga86ef9166a6e38cc1d19f0138fb01d07b) | No target | S6 | Implemented / common service |
| `CreatePlateMat` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gaa1e39b95a2e56588edd55118f6c9c544) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gaa1e39b95a2e56588edd55118f6c9c544) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gaa1e39b95a2e56588edd55118f6c9c544) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L114) | S7 | Pending |
| `RemovePlateMat` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga81c64bbb52a4fb50612139675971f7d8) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga81c64bbb52a4fb50612139675971f7d8) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga81c64bbb52a4fb50612139675971f7d8) | No target | S8 | Implemented / common service |
| `RemovePlateMatFromPlate` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gaaa030000531d9773c5b032432c4b4215) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gaaa030000531d9773c5b032432c4b4215) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gaaa030000531d9773c5b032432c4b4215) | No target | S8 | Pending |
| `CreateElasticFooting` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#ga1eb1b863f39109878125d792c62fbccc) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#ga1eb1b863f39109878125d792c62fbccc) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#ga1eb1b863f39109878125d792c62fbccc) | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs#L103) | S7 | Implemented / common service |
| `RemoveElasticFooting` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gab41a4e6af27c329d359f845891db80d8) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gab41a4e6af27c329d359f845891db80d8) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gab41a4e6af27c329d359f845891db80d8) | No target | S8 | Implemented / common service |
| `RemoveElasticFootingFromNode` | `VARIANT` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_support.html#gacc712e1e6a56cb8618f30108ee913aa0) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_support.html#gacc712e1e6a56cb8618f30108ee913aa0) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gacc712e1e6a56cb8618f30108ee913aa0) | No target | S8 | Implemented / common service |
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


The annex expands these rows and inventories all 33 private-main-interface overloads. “Direct adapter” indicates a static invocation site, not a verified native fixture.

## 4. Common contracts that need careful interpretation

There is no documented year difference to resolve these issues. They require common verified operation-specific handling.

### 4.1 Node, definition, type and index are different identities

- GetSupportCount counts supported nodes, not unique support definitions.
- GetSupportNodes returns supported node IDs and a count.
- GetSupportType and GetSupportInformation return type codes: -1 error, 0 no support, 1–13 classified support data.
- GetSupportName's parameter is called nSupportNo but described as a support node number. Confirm this identity before exposing a named query.
- GetSupportUniqueID / SetSupportUniqueID take a support item number, not the node-number contract of the name query.
- Foundation counts count definitions; they do not prove that IDs are contiguous.
- GetPlateMatSupportId takes a zero-based plate-mat index. Its copied return prose says “number of plate mat supports”; verify the actual ID result before interpreting it as a reference.

Use descriptive parameters such as nodeId, supportId and plateMatIndex. Preserve model/session context in keys. Never enumerate elastic mats or footings by assuming definition IDs are 1..count; no corresponding documented index-to-ID function exists for those families.

### 4.2 Native return policies differ

| Native operation | Documented result interpretation |
| --- | --- |
| AssignSupportToNode, DeleteSupport | 0 success, -1 failure. |
| AssignSupportToEntityList and family-specific removals | Boolean-style success/failure. |
| RemoveSupportFromNode | 1/true success, 0/false failure. |
| CreateSupportFixed/Pinned/FixedBut | Support reference ID, -1 error; verify the valid ID domain. |
| CreateElasticMat/PlateMat/ElasticFooting | Support reference ID, 0 error. |
| CreateInclinedSupport | Reference ID, -1 error, -2001 reference node not found. |
| GetSupportType/Information/InformationEx | Type-code domain, not a Boolean success flag. |
| GetSupportNodes | Returned count or -1 error. |
| SetSupportUniqueID | void; no native success flag. |

A successful query may report NoSupport or zero count. A returned zero can mean success, empty data, no support or failure depending on the operation. A void setter can report that invocation completed, but it cannot claim an independently verified update unless read-back confirms it.

### 4.3 Extended information directions conflict

[GetSupportInformationEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_support.html#ga44751a360dec5b32dc6f4dc8149f9e64) declares support number/type as mutable references, and installed metadata exposes Object&. The help marks them [in]; its example supplies a known created support and classified type. The main source initializes them and treats them as outputs.

Static evidence does not settle whether they are required inputs, outputs or in/out. Verify required initialization, returned type code, support ID and buffer ownership on advertised builds. Keep native references internal and return a named ExtendedSupportInformationResult once the contract is established. Do not silently change the help annotations or expose a query whose required inputs are guessed.

GetSupportType also has a mutable native declaration but is documented logical input and appears by value in the interop interface. Mutable C++ reference syntax alone does not determine managed output direction.

### 4.4 Release and spring states require three-state data

Fixed-but creation documents six Double release values, 0 fixed / 1 released, plus six Double spring constants. Information getters describe six Int32 release states: **-1 spring, 0 fixed, 1 released**. Creation and read-back packets therefore have distinct semantics.

Use a named six-DOF result with a verified Fixed/Released/Spring enum and stiffness values. The source's Boolean Releases.Parse maps both -1 and 0 to false, losing the distinction between spring and fixed. Do not round-trip its Boolean array as complete native state.

Translational and rotational stiffnesses have different physical dimensions. Verify native unit conventions and angle units; do not apply a force/length factor indiscriminately to all six values. Null, nonfinite, negative or unsupported stiffness inputs need explicit rules rather than hidden coercion.

### 4.5 Plate-mat input and retrieval are inconsistent

[CreatePlateMat](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_support.html#gaa1e39b95a2e56588edd55118f6c9c544) documents directions 0–5 plus **All=6**. The prose requires one scalar subgrade for a single direction and a Double array for All. However, every reviewed example passes a three-value array with direction 2. The main source always passes three values and its FoundationSupportDirection enum omits All=6.

Resolve scalar versus array marshaling with controlled native fixtures. Use a single-direction request and an all-directions request with verified per-axis ordering; do not expose an arbitrary array packet as a strongly typed API.

GetPlateMatDetail returns three separate subgrade outputs, even though its parameter prose collapses them into one alias. The main retrieval/population methods store only the first, dropping the other two. Preserve all three, direction, flags and assigned-plate count.

GetPlateMatAssignmentList and RemovePlateMatFromPlate contain copied elastic-mat/node prose despite plate-based declarations. Verify plate identity and never hydrate those IDs through a node resolver.

### 4.6 Other shared ambiguities

| Issue | Required decision |
| --- | --- |
| Pinned support definition | Help describes only local y/z rotations free; the source default frees all three rotations. Confirm native six-DOF behavior, particularly torsion. |
| Inclined support reference | Distinguish reference-kind 0 offsets, 1 global coordinates, 2 reference node. The prose repeats fRefY; validate all X/Y/Z components and nondegenerate reference geometry. |
| Foundation directions | Elastic mat/footing accept 0–5; only plate mat documents All=6. Use family-specific selectors or strict call-level validation. |
| Spring type 2 | “Multi-linear” selection alone does not define a complete response curve. Document prerequisites and unsupported data rather than inventing a curve. |
| Subgrade units | Establish pressure/displacement dimensions and native per-family units. PlateMat's force/length conversion differs from source ElasticMat/Footing force/volume. |
| GUID strings | Preserve raw invalid/absent text and typed Guid when valid; distinguish malformed output from an empty supported identity. |

These issues are verification gates, not reasons to invent version-specific implementations.

## 5. Existing implementation and source reuse

### 5.1 Current repository

OpenStaadWrapper already captures RawSupport, creates the captured Support adapter and supplies the native version snapshot. Refresh replaces bindings; disposal clears them. No public wrapper change is needed.

Reuse current geometry entities, managed units/property-store primitives and strict native-value readers. A Support reference must stay bound to the acquired wrapper's receiver; do not fall back to the globally active STAAD instance.

### 5.2 Main-project surface

The [main interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSSupport.cs) has 33 overloads, all present in the [adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSSupportAdapter.cs). It provides:

| Source family | Reusable behavior |
| --- | --- |
| Creation/assignment | Fixed/pinned/fixed-but and foundation dispatch, entity/model conveniences and grouped assignments. |
| Nodal queries | Supported-node discovery, classification, lookup and grouping by definition. |
| Foundation queries | Known-ID elastic mat details, plate-mat index discovery, detail/assignment hydration and population. |
| Models | Support hierarchy, releases/restraints/springs, foundation fields and assignment collections. |
| Factory/type data | Polymorphic class selection and native support/foundation selector meanings. |
| Serialization | JSON model discriminators with TypeNameHandling.None. |

These are reuse candidates; their incomplete behavior below must be adapted. Eighteen native names are called, seventeen are absent. There are no additional adapter-only methods or private adapter helpers.

### 5.3 Source gaps and defects

| Finding | Required adaptation |
| --- | --- |
| Enforced/EnforcedBut creation branches contain only commented calls. | Return explicit Unsupported until a valid native operation is established. Do not return fluent success after doing nothing. |
| CreateSupport does not implement inclined, multilinear or generated families. | Reject unsupported requests before side effects; no broad “all support types” claim. |
| GetSupport handles only type codes 1–5; foundations and other families return null. | Distinguish NoSupport, Unsupported, Error and all known/unknown classifications. |
| GetAllSupports combines that limited nodal path with plate mats. | Report scope; it does not become complete elastic-mat/footing/inclined/generated extraction. |
| GetDistinctSupports initializes spring data as Int32[6], reuses buffers and ignores return qualification. | Use verified native types and fresh/sentinel buffers; preserve per-node outcome and valid definition identity. |
| Two automatic elastic-mat discovery overloads are marked obsolete(error=true) because distinct-support discovery is incomplete. | Repair identity discovery before enabling; count is not a list of IDs. |
| Plate-mat retrieval/population drops subgrades 2/3. | Preserve every qualified field and All direction. |
| Creation IDs/assignment flags are unchecked. | Apply domain-object ID/collection changes only after qualified native outcomes. |
| Springs.ToArray accesses nullable.Value in its null branch. | Explicit required/absent stiffness validation and finite-value rules. |
| Unit conversion factors are inconsistent. | Verify subgrade and translational/rotational stiffness dimensions; avoid copying an unverified factor. |
| Support.Equals returns false for distinct instances with same ID/type; ValueEquals throws. | Stable model/session-qualified identity/equality; do not port unused throwing helpers. |
| Global reflection factory can fail on duplicate attributed registrations. | Internal deterministic registry and explicit unknown-type outcome. |
| Hydration adds null resolver results and clears existing collections prematurely. | Retain unresolved IDs and replace managed collections only with qualified snapshots. |
| nThreads is unused in GetSupportedNodes and delegated to Geometry elsewhere. | Document actual execution; no native parallel-safety guarantee. |

These are static source findings. This review did not reproduce native failures or engineering results.

### 5.4 Keep adjacent workflows separate

SupportConditions and its settlement-driven model selection are later managed conveniences; settlement load commands belong to Load. Enforced model classes do not establish a working ordinary enforced-support creation function. The commented CreateSupportEnforced names are not among the 35 reviewed interop members.

CreateInclinedSupport documents enforced subtypes, but it must not be substituted for an ordinary enforced support using a guessed reference frame.

StaadSupportSerializer/JSON converters may be ported selectively after model/schema review. SupportStructureFramesExtractionService concerns structural frame projection, not boundary-condition COM extraction, and stays separate from this native-function count.

## 6. Managed API and internal architecture

### 6.1 Named results and typed requests

Return named classes containing semantic status, actual native code/type observation, query identity and qualified payload. No public ref/out or tuples. Do not disguise NoSupport as failure, a failed query as an empty supported-node list, or a void setter as verified success.

The following original design sketches are historical proposals; the current implemented signatures are in the foundation guide:

~~~csharp
SupportedNodeIdsResult GetSupportedNodeIds();
SupportTypeResult GetSupportType(int nodeId);
SupportInformationResult GetSupportInformation(int nodeId);
ElasticMatQueryResult GetElasticMat(int supportId);
PlateMatQueryResult GetPlateMat(int supportId);
SupportCreationResult CreateFixedButSupport(FixedButSupportRequest request);
SupportAssignmentResult AssignSupport(SupportAssignmentRequest request);
~~~

The annex gives a proposed managed signature/result for all 35 names. Reuse cohesive result families and support models; avoid one helper class for every native scalar.

Useful overloads accept IDs or existing nodes/plates, one or many validated IDs and explicit sparse requests. Snapshot inputs once, enforce budgets and stable first-seen ordering, and return identified partial outcomes when the native operation permits partial completion.

Keep SupportType/SupportKind distinct from operation failure. Preserve unknown native codes. DOF state, foundation direction, spring type, inclined subtype and reference kind are separate semantic domains; do not reuse one enum solely because their integers overlap.

### 6.2 Shared services and polymorphism

Proposed organization:

~~~text
Adapters/Interfaces/IOSSupport.cs
Adapters/Models/OSSupportAdapter.cs     one non-partial adapter
Models/Support/                        requests, named results and reused models
Enums/Support/                         semantic domains
Services/Support/
    Binding/                           receiver and contract evidence
    Queries/                           common count/type/detail extraction
    Foundations/                       family-specific queries and packets
    Creation/                          internal request/type dispatch
    Assignments/                       ID snapshots and partial outcomes
    Results/                           strict status/buffer decoding
~~~

These are proposed folders, not files created by this report. Start with a few cohesive classes. All native-domain signatures stay in IOSSupport, implementations in OSSupportAdapter and helper services internal.

Polymorphism is useful for support/foundation/request kinds even though year variants are unnecessary. Select fixed, fixed-but, elastic mat, plate mat, footing and inclined handling by verified semantic request type. Use one common contract profile for the reviewed years, attached to the acquired receiver and native version evidence.

~~~mermaid
flowchart TD
    A["Acquired wrapper and RawSupport"] --> B["Capture native version and receiver evidence"]
    B --> C{"Required contract verified?"}
    C -->|"No"| U["Unverified or unsupported outcome"]
    C -->|"Yes"| D["Shared Support services"]
    D --> E{"Operation or request family"}
    E -->|"Nodal query"| F["Common nodal decoder"]
    E -->|"Foundation"| G["Family-specific packet and result decoder"]
    E -->|"Creation or assignment"| H["Internal typed request handler"]
    F --> R["Qualified named result"]
    G --> R
    H --> R
~~~

Use wrapper.StaadVersion rather than host ApplicationVersion for native diagnostics. Matching help does not justify advertising unknown/servicing builds without contract evidence. Refresh/disposal invalidates captured receivers/evidence. No public wrapper changes or per-year adapter classes are needed.

Static C# metadata cannot hide methods dynamically by connected version. Since no year-exclusive Support functions were found, a year-specific public capability hierarchy is unnecessary. Add a focused optional gateway later only if real verified differences require it; avoid a cast bypass through a common adapter implementing an allegedly unavailable interface.

## 7. Query/mutation flow and edge cases

~~~mermaid
flowchart TD
    A["Managed request"] --> B["Validate identities, packet and budget"]
    B --> C{"Binding and operation contract current?"}
    C -->|"No"| X["Unsupported or stale-binding result"]
    C -->|"Yes"| D["Use verified session COM execution owner"]
    D --> E["Invoke intended operation or query stage"]
    E --> F["Interpret this native return domain"]
    F --> G{"Outcome qualifies data or effect?"}
    G -->|"No"| Y["Failure or explicit absence with identity"]
    G -->|"Yes"| H["Validate buffers, counts and created identity"]
    H --> I{"Complete qualified result?"}
    I -->|"No"| Z["Invalid-data or uncertain-effect diagnostic"]
    I -->|"Yes"| J["Own snapshots and apply qualified model changes"]
    J --> K["Return named outcome"]
~~~

No-support observations are valid query data. Empty batches may return without COM calls and cannot claim that native availability was observed. A native mutation exception/timeout can leave an uncertain effect, so “failure” must not automatically promise no model change.

| Edge case | Handling |
| --- | --- |
| Blank instance or no ready model | Return actual not-ready/empty observations; never open a model or assign supports implicitly. |
| Zero count versus error | Qualify integral count; never allocate a negative/error count or turn query failure into an empty list. |
| Count/list race | Bound allocation and validate returned count/length; coordinate with edits and retain uncertainty if the model changes. |
| Reused definition across nodes | Group by model/session/definition identity; supported-node count is not unique-definition count. |
| Definition ID versus plate index | Use explicit zero-based index only for GetPlateMatSupportId; no guessed ID sequences for other families. |
| Unknown/generated/inclined/multilinear type | Preserve raw known/unknown type and unsupported hydration; do not silently omit the node. |
| Spring state and data | Preserve -1/0/1, finite stiffnesses and mixed component units; do not collapse into Boolean releases. |
| One-axis versus all-direction plate mat | Use verified native packet layout and preserve all subgrades; never invent missing axes or map a plate ID as a node. |
| Invalid reference geometry | Validate reference-kind/node/coordinates and degenerate directions before inclined creation. |
| Creation succeeds, assignment fails | Return created ID plus assignment failure/partial completion. No guessed deletion of potentially shared definitions. |
| Duplicate or invalid inputs | Snapshot once; documented deduplication, bounded query counts, positive IDs and no hidden repeated enumeration. |
| Resolver cannot hydrate an entity | Retain unresolved native IDs separately; do not insert null into a “complete” result. |
| Definition deletion versus unassignment | Require explicit operation names; DeleteSupport also removes assignments, unlike node-specific unassignment. |
| Void GUID setter | Distinguish completed call from verified read-back and unknown completion; no invented native success flag. |
| Refresh/dispose/different active instance | Reject stale binding; do not reattach globally or final-release shared RCWs as query cleanup. |
| Busy COM/retry/cancellation | Retry only established transient conditions with duplication controls. Cancellation stops scheduling, not arbitrary in-flight native execution. |

A local adapter lock cannot coordinate all raw access and other adapters. Native calls use the caller's verified apartment or an explicit session-owned executor. Pure managed grouping/serialization can run after extraction; Task.Run/PLINQ and nThreads do not establish COM thread safety.

This review does not change the net481 target or establish .NET Core package support.

## 8. Implementation sequence

| Workstream | Scope | Completion gate |
| --- | --- | --- |
| S0 | Approve result/identity/unit contracts and source adaptations. | Resolve ambiguity priorities, supported families and explicit partial/unsupported semantics. |
| S1 | Minimal shared binding/value foundation. | Reuse receiver/native-version metadata and strict readers; no artificial year split. |
| S2 | Four basic supported-node/type/information queries. | Counts and NoSupport distinct from error; complete six-DOF read-back where verified. |
| S3 | Three extended identity/name queries. | Verify Ex directions, node-versus-definition name identity and GUID representation. |
| S4 | Seven elastic-mat/plate-mat queries. | Qualified details, full subgrades, plate index-to-ID and assignments without guessed IDs. |
| S5 | Three elastic-footing queries. | Known-ID details/assignments and honest limits of count-only discovery. |
| S6 | Five basic creation/assignment operations. | Verified IDs/packets, state/stiffness units and explicit created-versus-assigned outcomes. |
| S7 | Four foundation/inclined creation operations. | Resolve plate scalar/array conflict, family directions, reference geometry and enforced-subtype scope. |
| S8 | Nine removals/GUID mutations. | Explicit definition versus assignment scope, deletion consequences and void/read-back distinction. |
| S9 | Native, API, XML and package validation. | Advertised-build fixtures, compatibility, documented source limitations and explicitly requested release. |

### Complete native allocation

| Workstream | Native names | Functions |
| --- | --- | --- |
| S2 | 4 | `GetSupportCount`, `GetSupportNodes`, `GetSupportType`, `GetSupportInformation` |
| S3 | 3 | `GetSupportName`, `GetSupportUniqueID`, `GetSupportInformationEx` |
| S4 | 7 | `GetCountOfElasticMat`, `GetElasticMatDetail`, `GetElasticMatAssignmentList`, `GetCountOfPlateMat`, `GetPlateMatSupportId`, `GetPlateMatDetail`, `GetPlateMatAssignmentList` |
| S5 | 3 | `GetCountOfElasticFooting`, `GetElasticFootingDetail`, `GetElasticFootingAssignmentList` |
| S6 | 5 | `CreateSupportFixed`, `CreateSupportPinned`, `CreateSupportFixedBut`, `AssignSupportToNode`, `AssignSupportToEntityList` |
| S7 | 4 | `CreateInclinedSupport`, `CreateElasticMat`, `CreatePlateMat`, `CreateElasticFooting` |
| S8 | 9 | `SetSupportUniqueID`, `RemoveSupportFromNode`, `DeleteSupport`, `RemoveElasticMat`, `RemoveElasticMatFromNode`, `RemovePlateMat`, `RemovePlateMatFromPlate`, `RemoveElasticFooting`, `RemoveElasticFootingFromNode` |


These workstreams partition all 35 names. Source-parity conveniences and model reuse are scheduled with the relevant native operation; they do not add native coverage merely by being additional overloads.

Recommended first approved increment is the small S1/S2 foundation: supported-node count/list, classified support type and basic support information. Extended identity/foundation discovery should follow once parameter direction and buffer contracts are established.

Do not implement an unresolved operation from a guessed packet to satisfy a phase count. Independent verified work can progress while an ambiguous contract remains explicitly gated.

## 9. XML documentation, tests and release gates

Every new interface/adapter method, request/result property and enum value needs XML covering descriptive arguments, identity domains, units/axes, status interpretation, absence/unsupported states, side effects, version/member evidence, COM execution ownership and exceptions.

Examples must inspect outcomes before consuming data, distinguish support IDs from node IDs and guard mutation explicitly. Do not run support creation/deletion just to compile documentation. Public signatures expose no ref/out, tuples or proprietary vendor COM types.

| Layer | Required evidence |
| --- | --- |
| Inventory | 35 names/details per year, all 33 main signatures, eighteen direct targets and seven excluded helpers. |
| Common profile | Identical reviewed contracts, two acquired wrappers/versions, unknown or servicing-build diagnostics and no invented year-specific branch. |
| Status decoding | Zero-success versus Boolean versus type/count/ID; NoSupport, unknown type, invalid index and void mutation semantics. |
| Buffers | Int32 versus Double, six-DOF states, nonzero lower bounds, sentinel/finite checks, counts and owned storage. |
| Identity/discovery | Shared definitions, sparse/noncontiguous IDs, plate index and plate/node assignment families; unresolved hydration retained. |
| Source corrections | No-op branches, obsolete discovery, all three plate subgrades, spring-state preservation, nullable stiffness and stable equality. |
| Engineering/units | Pinned torsion, fixed-but springs, translational/rotational stiffness, subgrade dimensions, all-direction plate mat and inclined axes. |
| Mutations | Failed IDs, partial create/assign, deletion/unassignment scope, void GUID read-back, uncertain completion and controlled retries. |
| Execution/lifetime | Verified apartment ownership, refresh/dispose, stale receivers, reentrancy and honest cancellation limits. |
| Native fixtures | Each advertised product/build, valid/absent/invalid supports and foundation families, copied-prose conflicts and equivalent engineering checks. |
| Distribution | Complete XML, guarded compiled examples, correct framework/Windows dependencies, clean PackageReference consumer and updated professional README/release notes for an actual future package. |

No library tests were rerun for these documentation-only changes. Analysis utilities compiled only themselves and parsed source; reflection-only metadata inspection did not execute STAAD.

## 10. Review outcome

Use a shared Support adapter/service implementation with polymorphic support-kind handling. Reuse the main models and conveniences selectively, repair the recorded limitations and preserve explicit evidence gaps.

The original review did not implement Support. The current increment implements 19 routes and leaves 16 pending, with wrapper/provider behavior and the net481 target unchanged. Validation uses managed fixtures and a local CI archive; the prepared distribution archive and public package are unchanged. Live compatibility remains unqualified.
