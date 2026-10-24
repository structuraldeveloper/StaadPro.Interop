# OpenSTAAD Property API coverage and contract inventory

**Review date:** 23 October 2026<br>
**Baseline:** `3b94e637839f0c3e28c1486d8f03f0a8af230b43`<br>
**Status:** Native comparison evidence retained; first 25 common typed query mappings implemented. The main project remained strictly read-only.

Read the [comparison and architecture plan](OpenSTAAD-Property-Multi-Version-Architecture-and-Plan.md) for policy decisions and sequencing. The [comparison JSON](data/OpenSTAAD-Property-2024-2026-Comparison.json) preserves all three years separately, exact declarations, complete parameter descriptions, native tables, vendor metadata, source mappings and file fingerprints.

## Current source — first 25 Property queries

Implemented **25 typed Property queries** through IOSProperty and the single non-partial OSPropertyAdapter: all 13 P2 native names and the first 12 P3 names in the documented allocation. Shared 2024–2026 contracts use one internal reader and strict value decoder. The 212-name union contains eight excluded entries, leaving 204 active candidates; **25 have managed routes and 179 remain pending**. GetPlateSectionPropertyRefNo is documented but absent from inspected vendor CLR metadata, so its acquired receiver may return Unsupported. Implementation coverage does not certify live COM or engineering behavior.

Passed **3,313 offline tests**, including **413 new Property cases**, with a Release build free of warnings/errors. Both interface and concrete adapter have matching expanded XML with method comparisons, units/axes, indices, native statuses, exceptions and examples. See [Property usage and reuse decisions](../Property-Porting.md) and the [exact contract index](../contracts/PropertyFoundationContractIndex.json). Main-source review remained read-only; all 74 recorded source fingerprints still match.

## 1. Scope and counting rules

The Property group tree has 204 / 209 / 209 names in 2024/2025/2026, with 199 / 207 / 207 detailed contracts. The union has 212 names, the intersection 201. Three reserved 2024 summaries disappear in later help; eight detailed names appear in 2025. Every one of those names is inventoried, including five reserved names, two native private helper functions and one deprecated setter. Excluding those eight entries leaves 204 active public-API candidates; native support still requires verification.

There are 18 group pages per year. All were traversed, and a scan of the other group pages found no additional OSPropertyUI-owned function rows. Class-level native listings and generated headers are supplemental evidence. Thirteen surface-related class summaries, fourteen metadata names outside the group tree, and their overlaps form fifteen supplemental candidates; twenty-two additional class-only implementation helpers are excluded.

Current IOSProperty and OSPropertyAdapter contain 25 typed domain queries and 25 Property native dispatch routes. ComObject and inherited Wrapper are binding properties, not domain implementations. The main IOSProperty has 103 overloads; its adapter implements all of them and adds four assignment-query conveniences. It has seven private helpers.

Source coverage counts actual native invocation names, not managed name matches: 56 direct adapter targets plus nineteen additional delegated section-model targets, 75 in total. A source method named GetBeamProperty discovers a property ID by enumerating assignments; it does not call the native eight-output GetBeamProperty. A name-only coverage search would incorrectly mark that native API complete.

The remaining 179 active candidates remain pending as a typed Property API. Named request/result families below are preliminary design proposals, not exported types. Closely related methods should reuse a few cohesive models or typed request subclasses; this is not a plan to create one class per native function. All new public outputs avoid ref/out and tuples.

## 2. Complete cross-version function matrix

| Native Property name | 2024 | 2025 / 2026 | Contract classification | Main source call targets | Plan | Current typed mapping |
| --- | --- | --- | --- | --- | --- | --- |
| [CreateBeamPropertyFromTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#gaf2cc429be392fe527feed7dc33f78d6c) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1086) | P6 | Pending |
| [CreateBeamPropertyFromTableWithCoverPlates](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#gab96ba7f158851c24a2eb289af7f17c6f) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L253) | P6 | Pending |
| [CreateBeamPropertyFromTableComposite](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga50040de9e0075880f3f7285284b7c79a) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1770) | P6 | Pending |
| [CreateWideFlangePropertyFromTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga78cec5f9be32c1061963f94ed050d331) | `VARIANT` | `VARIANT` | Unchanged | No target | P6 | Pending |
| [CreateBeamPropertyFromTableEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#gadce802ef32861d83b01a4226b234939c) | `VARIANT` | `VARIANT` | Unchanged | No target | P6 | Pending |
| [CreateTeePropertyFromTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga9623d2b80ee04d55f188772469ae23e7) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1740) | P6 | Pending |
| [CreateChannelPropertyFromTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#gaa08e9a925a2afa0ba3abde267f4eadb1) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1780) | P6 | Pending |
| [CreateAnglePropertyFromTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga6f1c73d26e890715030ddd5e1f765b20) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1803) | P6 | Pending |
| [CreateTubePropertyFromTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga64ab21d46e0b14bdb71c9bd0f1e84c29) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1835) | P6 | Pending |
| [CreatePipePropertyFromTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga8df35fc2730a95842893d45040aaa00b) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1819) | P6 | Pending |
| [CreatePrismaticRectangleProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga4ef3ab387d4b23c1c8e3d3e2d7b7783d) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticRectangleSection.cs#L47) | P6 | Pending |
| [CreatePrismaticCircleProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga016cb17310c1214ad731ef8603f74fcc) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticCircleSection.cs#L38) | P6 | Pending |
| [CreatePrismaticTeeProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga26721d08a647bddd960d0499b42ac5c9) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticTeeSection.cs#L57) | P6 | Pending |
| [CreatePrismaticTrapezoidalProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga0fdc7ad624d8f07839d85b017b518350) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticTrapezoidalSection.cs#L52) | P6 | Pending |
| [CreatePrismaticGeneralProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#gae35d506434e6db9abd1edcb540a9d1cb) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticGeneralSection.cs#L63) | P6 | Pending |
| [CreateTaperedIProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#gad01c26661a6e9a588c4e5cfb46ce9403) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/TaperISection.cs#L71) | P6 | Pending |
| [CreateTaperedTubeProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga558bd4cf571d37c5f3520b2e7e56b9e5) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/TaperTubeSection.cs#L52) | P6 | Pending |
| [CreatePlateThicknessProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga4ab7a973986b611d748302e8dcdc0154) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1167) | P6 | Pending |
| [CreateUPTTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#gacffe1377c08a287a4b95c1fc315f4127) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptAngleSection.cs#L49) | P8 | Pending |
| [AddUPTPropertyWIDEFLANGE](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#gaf8641a9ec57a7ad52f1f4ee44c4f5168) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptWideFlangeSection.cs#L56) | P8 | Pending |
| [AddUPTPropertyWIDEFLANGEUNEQUAL](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga416ce6e066c01171353a67dceb72d404) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | Pending |
| [AddUPTPropertyWIDEFLANGECOMPOSITE](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#gad0aeaec8b6052eaa4e713b184506e935) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | Pending |
| [AddUPTPropertyCHANNEL](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga762e3143fd9fa6d9ffc3f213af23d57f) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptChannelSection.cs#L53) | P8 | Pending |
| [AddUPTPropertyANGLE](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga4d33b8f55788d2d6a28d75542179a38e) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptAngleSection.cs#L50) | P8 | Pending |
| [AddUPTPropertyDOUBLEANGLE](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga7659bafc3884233e8006403264df40b2) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptDoubleAngleSection.cs#L53) | P8 | Pending |
| [AddUPTPropertyTEE](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga6657114c7f25e838f5189e45571532d2) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptTeeSection.cs#L53) | P8 | Pending |
| [AddUPTPropertyPIPE](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga927bbbce72db4a45e0006b936840ffea) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptPipeSection.cs#L56) | P8 | Pending |
| [AddUPTPropertyTUBE](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga3a2f714b28bce009a5df574409915b70) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptTubeSection.cs#L43) | P8 | Pending |
| [AddUPTPropertyGENERAL](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#gaa701fb0dc527b5cd73cb0c0d6616487f) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptGeneralSection.cs#L89) | P8 | Pending |
| [AddUPTPropertyISECTION](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga192963517f316eef4bf5cb2822d78424) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptISection.cs#L89) | P8 | Pending |
| [AddUPTPropertyPRISMATIC](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga828f857560168eb9d6a9809950454017) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptPrismaticSection.cs#L57) | P8 | Pending |
| [CreatePropertyFromUPTTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga4d6c2edb2612f542b9a1596221a9f784) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptGeneralSection.cs#L91) | P8 | Pending |
| [GetUptGeneralProfilePointsCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#gac11402eeb7ce630b8a4b40b0a63cb83e) | Absent | `VARIANT` | Newly documented in 2025 | No target | P8 | Pending |
| [GetUptGeneralProfileBoundaryPoints](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga9f135f3a5b84d9041a60bf5c276699c2) | Absent | `VARIANT` | Newly documented in 2025 | No target | P8 | Pending |
| [GetUptGeneralStressLocationPoints](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga206fc2be959408948d3e81aa6751ed7a) | Absent | `VARIANT` | Newly documented in 2025 | No target | P8 | Pending |
| [AssignBetaAngle](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_beta.html#gac25ed74556a088b204e59721179f1660) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1575) | P5 | Pending |
| [GetBetaAngle](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_beta.html#ga7dbd2b865f74ec34570f04fad356f42b) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L631) | P2 | [GetBetaAngle](../Property-Porting.md#getbetaangle) |
| [GetSectionPropertyValues](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga91afb41d03b9b4df33f3b47b36de3938) | `VARIANT` | `VARIANT` | Unchanged | No target | P4 | Pending |
| [GetSectionPropertyAssignedBeamCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga008bbb46f75923080b95eb63962f1acd) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L55) | P4 | Pending |
| [GetSectionPropertyAssignedBeamList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga4cb2980c099d339e6f0d5532448f1bb1) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1056) | P4 | Pending |
| [GetBeamSectionPropertyRefNo](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga3c67b414c91c210bf5cc4b70a2f3be74) | `VARIANT` | `VARIANT` | Unchanged | No target | P4 | Pending |
| [GetBeamSectionPropertyValuesEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga8d1dc394d29fa932cd97c281ab619ab6) | `VARIANT` | `VARIANT` | Unchanged | No target | P4 | Pending |
| [GetSectionPropertyValuesEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga0497f1f458d11eb783f465a85dc6ad27) | `VARIANT` | `VARIANT` | Version policy candidate | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1019) | P4 | Pending |
| [GetSectionTableNo](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga9c3b455750b4ed8046d78fecb0b8cbc7) | `VARIANT` | `VARIANT` | Unchanged | No target | P4 | Pending |
| [GetBeamSectionPropertyTypeNo](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga37be18e3c326761c52fb42fa0773a97c) | `VARIANT` | `VARIANT` | Unchanged | No target | P4 | Pending |
| [GetBeamSectionName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga398cd10fa2ef3710d2807797445be4bb) | `VARIANT` | `VARIANT` | Unchanged | No target | P4 | Pending |
| [GetBeamSectionDisplayName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga967af878ba279f8d3a2fe58a0d209a14) | `VARIANT` | `VARIANT` | Unchanged | No target | P4 | Pending |
| [GetCountryTableNo](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga5ed5bfb3b3caf8dd8d395858eb6bbdec) | `VARIANT` | `VARIANT` | Unchanged | No target | P4 | Pending |
| [GetSectionPropertyCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gabd01f29a4421f6fe05c2138cda657595) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L598) | P4 | Pending |
| [GetSectionPropertyList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga58470c040e0adc4607e00e054b4c6daa) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L602) | P4 | Pending |
| [GetSectionPropertyName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga22f293fd885cd2f6a2102d5a9f9e691d) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1080) | P4 | Pending |
| [GetSectionPropertyType](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga70e659ae3da254327aa62b82914dee1f) | `VARIANT` | `VARIANT` | Unchanged | No target | P4 | Pending |
| [GetSectionPropertyCountry](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga7ecdf0e76eeebbeb8cb5d225ab319084) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1022) | P4 | Pending |
| [GetShapeCode](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gae13c9d4c20e1a0a487728cbdcdf73024) | `long` | `long` | Version policy candidate | No target | P9 | Pending |
| [GetRecordForSection](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga75d00082ceba112a311334739850896b) | `long` | `long` | Version policy candidate | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1029) | P9 | Pending |
| [GetPlateThickness](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga2fa3663eee2b4f19cfd109177de444fe) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1278) | P4 | Pending |
| [GetThicknessPropertyCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga1b82bcd70a84320fa1c5f153255426f8) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1246) | P4 | Pending |
| [GetThicknessPropertyList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga080e308745d58857189ab422b9b6102e) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1249) | P4 | Pending |
| [GetThicknessPropertyAssignedPlateCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga6b405e7c6b5a5a294277ffc910dbe42e) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1294) | P4 | Pending |
| [GetThicknessPropertyAssignedPlateList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gacec0dbd476ed855f50796f7bb4166d2d) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1296) | P4 | Pending |
| [GetThicknessPropertyValues](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gafd3b17a65417534a41a8f1497e4517c6) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1270) | P4 | Pending |
| [RemovePropertyFromPlate](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga99c86bf4b01408c684ff009e0e00079b) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [RemoveMaterialFromPlate](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gaa8489347df16c3ea9aea52ca421e4e53) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [RemoveMaterialFromSolid](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gaefebf74d842cbed7e39736e94abb8ea4) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [GetIsotropicMaterialAssignedPlateList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gaf6b9b9403e5b7e0ae90ba797491f17af) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L411) | P4 | Pending |
| [GetCountofSectionPropertyValuesEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga2e1e4793b70cdd102f55d878b92685cc) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1015) | P4 | Pending |
| [FindUPTTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#gac238a7c187f6e53ca29b25f2331b20a3) | `long` | `long` | Unchanged | No target | P8 | Pending |
| [CreateUPTTableEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga0fb2e9ebe08ff961ce2e620616ceefa0) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | Pending |
| [RemoveUPTTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#gaf713bf43b402d4efc3af2b5296ede180) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | Pending |
| [RemovePropertyFromUPTTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#gaf6f7c76255b453edb2cf220fb297ec72) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | Pending |
| [CreatePropertyFromUserTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#gabb8beaa20258b93f62714c9672415350) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | Pending |
| [GetUserProvidedTableCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga32c6d22bc1d698ce9e1ea6b2f8473cd9) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | Pending |
| [GetUserProvidedTableList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga76a5e72dc10bf0cb788454375ef3717b) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | Pending |
| [GetUserProvidedTableSectionCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga300e2d80e44a9a02dc31dd461dde271e) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | Pending |
| [GetUserProvidedTableSectionList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga01f780a22d5a0720124a9a18c91461d7) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | Pending |
| [GetUserProvidedTableSectionType](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#gadf28c45e67614bb66eb332b8dd76588b) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | Pending |
| [GetUserProvidedTableSectionPropertyCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga43427a4ed7d22d28a82038f6351fee1e) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | Pending |
| [GetUserProvidedTableSectionProperties](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga6fa18da33cf823a4a528ddaa35d7aeb3) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | Pending |
| [GetUserProvidedTableNo](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga778d97759d0fae3321f116992db15f50) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | Pending |
| [CreateMemberReleaseSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gabfb89e094b1aa1a943716dfb7a0d4841) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1585) | P7 | Pending |
| [CreateMemberPartialReleaseSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gae408d55746db59e4c22c2e505bb403a2) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [CreateMemberTrussSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#ga19c64f2a9dc77cab9176f382524d17ca) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [CreateMemberInactiveSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gabdcfbecf01cd5889eb04da0aa8102a41) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [CreateMemberTensionSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#ga8db5109360921dde0350b2cdd4fe611a) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [CreateMemberCompressionSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gafd9eeea22012202b700e42de2dae4dc0) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [CreateMemberIgnoreStiffSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gad30bdd6dd57392faa1cf132df4819164) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [CreateMemberCableSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#ga354fa1642399ba13018d1a610149589c) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [CreateMemberCableSpecEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#ga1fe56e4b2b563db42107d0f2b68b8fb4) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [CreateMemberOffsetSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#ga452a2b2f15fcbf10a46a11e8dec7ef42) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L732) | P7 | Pending |
| [CreateElementNodeReleaseSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gafd355c2d78879ae36eb5cabf7c0d99eb) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [CreateElementPlaneStressSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gaec273848a190a0e1c308a4c770b45f1f) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [CreateElementIgnoreInplaneRotnSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gade9e2bced8795b0cd4dab888244829b3) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [AddControlDependentRelation](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gacfbf4be0e4710af48fbbb2c08bec0cfd) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [CreateElementOffsetSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gafd77251b4fa48baa9491672bf3a3a3ca) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [CreateElementLocalZOffsetSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gac1057cd910b775a08d6c2c7df22e2781) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [CreateMemberFireProofingSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#ga6792430dddf478392843d424059c8730) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [DeleteMemberReleaseSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gafe30a2d90280de4c4fd6149959730507) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [DeleteAllControlDependentRelations](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gad780df368b309c15dec931245113040c) | `long` | `long` | Unchanged | No target | P10 | Pending |
| [GetElementOffsetSpecCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga5facbddbfb9ea2ad0b48c454a122cf24) | `VARIANT` | `VARIANT` | Unchanged | No target | P2 | [GetElementOffsetSpecificationCount](../Property-Porting.md#getelementoffsetspecificationcount) |
| [RemoveAllElementOffsetSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gaa7bdd908c360ccf8ca9e41ee0e894b5a) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [GetMemberSpecCode](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga6b19b83068026b147eeb88a3528328d0) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L874) | P7 | Pending |
| [RemovePropertyFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga551c8845b034009e68a16ec2813c50bc) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [RemoveBeamPropertyHelper](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gadb028ea4f8477119ab50eb4881ebef61) | `BOOL` | `BOOL` | Native private helper: excluded | No target | P0 | Excluded |
| [DeleteProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga26852ecd49fc5ea7657d2b5198a1c7ba) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [GetPropertyUniqueID](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga5aa6aa08217095c0a0eb65530dd867e8) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [SetPropertyUniqueID](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gac7758ac39ea94bba561989b4898e59bb) | `void` | `void` | Unchanged | No target | P7 | Pending |
| [DeleteMemberSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga1956ce5d3c9487681c3dcca8c2a7f4a4) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [RemoveMemberReleaseSpecFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gaaee16932dc7a02f3d707576fc2d009a0) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [RemoveMemberOffsetSpecFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga76d69608bda66b72f7a1c9011b68a46d) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [RemoveMemberTrussSpecFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga01e39b8ece2146ea7aa09b7ff33423cd) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [RemoveMemberInactiveSpecFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gaec27f611f0d02de6278bdb6601d106f8) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [RemoveMemberTensionSpecFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga22be38efdbc61ef92dabf2b7eb25ad89) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [RemoveMemberCompressionSpecFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga1fac54088e17dbc9a7e7e93a5a9d1d45) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [RemoveMemberIgnoreStiffSpecFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga159089ba705d76f58988ecd3cd052be3) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [RemoveMemberCableSpecFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga2b82e36f819c8df67990bdc88da6dd6f) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [RemoveElementPlaneStressSpecFromPlate](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gafc527399dbd62917400f8922ba0b4294) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [RemoveElementIgnoreInplaneRotnSpecFromPlate](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga9b1e19ac200b1c62409785f5c80dd845) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [RemoveElementNodeReleaseSpecFromPlate](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga6503cab6a999f8024fe606c4e32c614a) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [RemoveAllElementNodeReleaseSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga8a3da913ec174caaa00066aac9ac2cad) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [GetMemberReleaseSpecEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga8ba58d357fc17509602aff5e2e99c9d4) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L968) | P2 | [GetMemberReleaseSpecificationExtended](../Property-Porting.md#getmemberreleasespecificationextended) |
| [GetInactiveMemberCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga1b96f8f630976e634b4906f1a1bb60b3) | `long` | `long` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L832) | P7 | Pending |
| [GetInactiveMemberList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga25d477cfe62c96ccbd7901073e76540d) | `void` | `void` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L834) | P7 | Pending |
| [GetAlphaAngleForSection](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga6aa625ced61caf38f8f164bbb4ecb0c0) | `VARIANT` | `VARIANT` | Formatting/cross-reference only | No target | P7 | Pending |
| [GetCentroidLocationForSection](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gac9e334a5b43efdadb0faaaa95b48e20b) | `VARIANT` | `VARIANT` | Formatting/cross-reference only | No target | P7 | Pending |
| [GetFireProofedBeamCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga2bfefd59d0590e70c72b7f086e6ede10) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [GetFireProofedBeamList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga838cda3edcec54845560c94c9163d004) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [GetFireProofDataForBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gad5ab8626f3f670ef18fc37c7851528f3) | `VARIANT` | `VARIANT` | Type/meaning clarification | No target | P7 | Pending |
| [GetFireProofingSpecCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga5b41580941d25c3f519a74ae1e840a60) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [GetFireProofingSpecDetails](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga9bdbff55763121a7a63149630ed66435) | `VARIANT` | `VARIANT` | Type/meaning clarification | No target | P7 | Pending |
| [GetFireProofingSpecAssignedBeamCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga7fef558917ef800217ce7ef0160c06d1) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [GetFireProofingSpecAssignedBeamList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga1cc2f6b4d2a0be3115cdc362d07b48e4) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [RemoveMemberFireProofingSpecFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga5ed5d25078d40c5c9c9f011dfc38db57) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [CreateAssignProfileProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#ga9e14482ffc29d710f72d6e8f648945b6) | `VARIANT` | `VARIANT` | Unchanged | No target | P5 | Pending |
| [AssignBeamProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#gab13d04addd4f49d9ddeb79bc04e076cd) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1091) | P5 | Pending |
| [AssignMemberSpecToBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#ga04773e6f7e7e123337dd8808525659d6) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L734) | P5 | Pending |
| [AssignPlateThickness](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#ga1a61c38085b3e44b4017ad4b5fba7329) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1185) | P5 | Pending |
| [AssignParametricSurfaceThickness](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#ga452172a2ca1781a313b638e1e92f1d05) | `VARIANT` (summary only) | `VARIANT` (summary only) | Reserved: do not use | No target | P0 | Excluded |
| [AssignElementSpecToPlate](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#ga7cfac9861855bd012a1c0e9ab75577bd) | `VARIANT` | `VARIANT` | Unchanged | No target | P5 | Pending |
| [UpdatePropertiesToDesignSection](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#ga0ee52e9e4368ba1a8ac84bfff8c336dd) | `VARIANT` | `VARIANT` | Unchanged | No target | P5 | Pending |
| [CreateIsotropicMaterialProperties](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_mat.html#ga21f578122b014d7b963ac9cbffb0cd33) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1446) | P5 | Pending |
| [CreateIsotropicMaterialPropertiesEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_mat.html#ga0bc4eb672546e351191883694a504138) | `VARIANT` | `VARIANT` | Type/meaning clarification | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1476) | P5 | Pending |
| [CreateIsotropicMaterialSteel](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_mat.html#ga6d780606438b7dd9a2621feeb2addc46) | `VARIANT` | `VARIANT` | Unchanged | No target | P5 | Pending |
| [CreateIsotropicMaterialConcrete](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_mat.html#gae3f66093596a98c0a09a9483c1fc61db) | `VARIANT` | `VARIANT` | Unchanged | No target | P5 | Pending |
| [CreateIsotropicMaterialAluminum](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_mat.html#ga9edf9ae98088d07e28177a6ce0f7e915) | `VARIANT` | `VARIANT` | Unchanged | No target | P5 | Pending |
| [CreateIsotropicMaterialTimber](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_mat.html#ga120025647e015ec700e4b19452f35e92) | `VARIANT` | `VARIANT` | Unchanged | No target | P5 | Pending |
| [GetIsotropicMaterialAssignedBeamCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gae487189c55613e48e06b63ef9669c0fb) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L440) | P3 | [GetIsotropicMaterialAssignedMemberCount](../Property-Porting.md#getisotropicmaterialassignedmembercount) |
| [GetIsotropicMaterialAssignedBeamList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga40e7abe1185d79a7053ab5f5b7f19555) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L443) | P3 | [GetIsotropicMaterialAssignedMemberIds](../Property-Porting.md#getisotropicmaterialassignedmemberids) |
| [GetMaterialProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga90b6809ba31300363321cce519f0b9c3) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L180) | P3 | [GetMaterialProperty](../Property-Porting.md#getmaterialproperty) |
| [GetBeamMaterialName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gade8859bea46c3bccd85204f73593e0b0) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L105) | P3 | [GetBeamMaterialName](../Property-Porting.md#getbeammaterialname) |
| [GetElementMaterialName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga44859f04996f6facbe39d62d67594b8c) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | [GetElementMaterialName](../Property-Porting.md#getelementmaterialname) |
| [GetIsotropicMaterialCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gaa8e5a0c6288ae8d53eded96a1fb0c114) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L331) | P3 | [GetIsotropicMaterialCount](../Property-Porting.md#getisotropicmaterialcount) |
| [GetIsotropicMaterialProperties](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gaeead1ecb0ed0c1587d432a1e3b9887b5) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L374) | P3 | [GetIsotropicMaterialProperty](../Property-Porting.md#getisotropicmaterialproperty) |
| [GetMaterialPropertyEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga02bbaecea45e8ab88558707f439acbe1) | `VARIANT` | `VARIANT` | Type/meaning clarification | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L151) | P3 | [GetExtendedMaterialProperty](../Property-Porting.md#getextendedmaterialproperty) |
| [GetIsotropicMaterialPropertiesEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gab82a93e375ef05e9f893b5ba272b7ad5) | `VARIANT` | `VARIANT` | Type/meaning clarification | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L348) | P3 | [GetExtendedIsotropicMaterialProperty](../Property-Porting.md#getextendedisotropicmaterialproperty) |
| [GetIsotropicMaterialPropertiesAssigned](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga74438ad3dd2db13772443598f8ee1707) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | [GetIsotropicMaterialPropertiesAssigned](../Property-Porting.md#getisotropicmaterialpropertiesassigned) |
| [GetOrthotropic2DMaterialCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga34d6f21ac2f41814c0a2ca78f0d387ed) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | [GetOrthotropic2DMaterialCount](../Property-Porting.md#getorthotropic2dmaterialcount) |
| [GetOrthotropic2DMaterialProperties](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga04b76b162db405e4a289b71f05b7e673) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | [GetOrthotropic2DMaterialProperties](../Property-Porting.md#getorthotropic2dmaterialproperties) |
| [GetOrthotropic3DMaterialCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gab247088c8327421655e4ad0b53b8a19f) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | Pending |
| [GetOrthotropic3DMaterialProperties](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga7be4c04b41e1326016a62cd43ccbf8e6) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | Pending |
| [DeleteMaterial](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga10ded1e9c1480a77783bd89565a1b082) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L71) | P3 | Pending |
| [RemoveMaterialFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gac084be83e5205fc0f6c766025e063531) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | Pending |
| [RemoveBeamMaterialHelper](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga3caee4c622453d9c30f56cc797d04231) | `BOOL` | `BOOL` | Native private helper: excluded | No target | P0 | Excluded |
| [GetPlateMaterialName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga0a6b8e8cdb65ed2b9747e95d7fb7706a) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L109) | P3 | Pending |
| [GetIsotropicMaterialAssignedPlateCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga2eaaa9ee8060d95d5f4c587ac45860e5) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L408) | P3 | Pending |
| [GetSolidMaterialName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga9b909a32be274f8a4a797ea85bdf2340) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | Pending |
| [GetIsotropicMaterialAssignedSolidCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga875c78622314eabe9df5600344f39a87) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | Pending |
| [GetIsotropicMaterialAssignedSolidList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga87626360957ef20687a901188e328fdd) | `VARIANT` | `VARIANT` | Type/meaning clarification | No target | P3 | Pending |
| [SetTypeToIsotropicMaterial](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga14f907ed47c4fbfd9d1c644ce60ed823) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | Pending |
| [GetTypeForIsotropicMaterial](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga3f9536e2090de7917bb53289dcccf2f0) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | Pending |
| [SetMaterialID](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_mat.html#gaeef73e958f01de09e3c761d0272599d5) | `void` | `void` | Deprecated: use SetMaterialName | No target | P0 | Excluded |
| [SetMaterialName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_mat.html#ga3d08cca38e2d9db5f1e7fdfae318ede6) | `void` | `void` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L232) | P5 | Pending |
| [AssignMaterialToMember](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_mat.html#ga1d4300751f2837d7af62dc01dbb2432a) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L211) | P5 | Pending |
| [AssignMaterialToPlate](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_mat.html#gad8b53c7251166270f27431a17b8172e1) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1567) | P5 | Pending |
| [AssignMaterialToParametricSurface](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_mat.html#gaebf142533eb1edfef25b3f787da3119d) | `VARIANT` (summary only) | `VARIANT` (summary only) | Reserved: do not use | No target | P0 | Excluded |
| [AssignMaterialToSolid](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_mat.html#ga0a26c0b843f84f87f796b75646fe0b72) | `VARIANT` | `VARIANT` | Unchanged | No target | P5 | Pending |
| [CreateMemberAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_attri.html#ga596e62e697d449368e61f64155d2e3e3) | `long` | `long` | Unchanged | No target | P7 | Pending |
| [CreateElementAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_attri.html#gaf9f5aa915388204e73930be35b8936fc) | `long` | `long` | Unchanged | No target | P7 | Pending |
| [DeleteMemberAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga624209a58bc90aa8debb826c76cf4850) | `long` | `long` | Unchanged | No target | P7 | Pending |
| [GetMemberCountByAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#gacb8947a2d5bc779fcd83573c288a024f) | `long` | `long` | Unchanged | No target | P7 | Pending |
| [GetMemberListByAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga601c518a43ee9b2181bb74a1eef40cdb) | `long` | `long` | Unchanged | No target | P7 | Pending |
| [GetMemberAttributeCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga65d055a80eda1974297c41556424518a) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [GetMemberAttributeList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga8ee3d1f547137a80ac72d653fadb49a9) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | Pending |
| [DeleteElementAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga33b257dd90f2aa0dfdc229de5f4f09be) | `long` | `long` | Unchanged | No target | P7 | Pending |
| [GetElementCountByAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#gae0769d51f8406a5d3a64daba5ade7c1c) | `long` | `long` | Unchanged | No target | P7 | Pending |
| [GetElementListByAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#gafc4b745f61d6e1d6679342b777c32e55) | `long` | `long` | Unchanged | No target | P7 | Pending |
| [GetAssignedAttributeCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga152207bd66e3c7aaa590a87dfde0ee79) | `long` | `long` | Unchanged | No target | P7 | Pending |
| [GetAssignedAttributeByIndex](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga5fb1fa2b9a41c80056c21d5af43ead20) | `long` | `long` | Unchanged | No target | P7 | Pending |
| [RemoveAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#gae2b97c9c2b4b73c84e9f221f50062150) | `long` | `long` | Unchanged | No target | P7 | Pending |
| [AssignMemberAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_attri.html#gad33b01a3d202d730de11bd3e13e0d8c6) | `long` | `long` | Unchanged | No target | P7 | Pending |
| [AssignElementAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_attri.html#ga07b29997e1c5a2ccee2677104bd3dced) | `long` | `long` | Unchanged | No target | P7 | Pending |
| [GetMemberReleaseSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga87a6522925d50ef55f783c90659e0180) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L916) | P2 | [GetMemberReleaseSpecification](../Property-Porting.md#getmemberreleasespecification) |
| [GetMemberLocalOffSet](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#gafdd6f4200ccd7a34763427950ff01281) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L709) | P2 | [GetMemberLocalOffset](../Property-Porting.md#getmemberlocaloffset) |
| [GetMemberGlobalOffSet](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#gaf5e75d28dc527fdefb40ac84111caaa3) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L707) | P2 | [GetMemberGlobalOffset](../Property-Porting.md#getmemberglobaloffset) |
| [GetBeamProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga185a7f1dd1d7576097df4e187e23bfe3) | `VARIANT` | `VARIANT` | Unchanged | No target | P2 | [GetMemberSectionProperties](../Property-Porting.md#getmembersectionproperties) |
| [GetBeamPropertyAll](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga2ecc4ba0fec2945bfdda017ed702cbea) | `VARIANT` | `VARIANT` | Unchanged | No target | P2 | [GetMemberSectionPropertiesExtended](../Property-Porting.md#getmembersectionpropertiesextended) |
| [GetBeamConstants](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga13b2745c44a68596591a21b578efa331) | `VARIANT` | `VARIANT` | Unchanged | No target | P2 | [GetMemberMaterialConstants](../Property-Porting.md#getmembermaterialconstants) |
| [GetElementLocalOffset](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#gaef9c6cdae820d578c8a9a093edc8ae91) | `VARIANT` | `VARIANT` | Unchanged | No target | P2 | [GetPlateLocalOffset](../Property-Porting.md#getplatelocaloffset) |
| [GetElementGlobalOffset](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga4cc99a4ac6ee14397d95e411c515f1d8) | `VARIANT` | `VARIANT` | Unchanged | No target | P2 | [GetPlateGlobalOffset](../Property-Porting.md#getplateglobaloffset) |
| [GetElementOffsetSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga94a58d54353f25487a5127b28f6f83ec) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1195) | P2 | [GetPlateOffsetSpecification](../Property-Porting.md#getplateoffsetspecification) |
| [GetPlateSectionPropertyRefNo](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga365e1db0fa5a9fde9ff8ba9359f81cc3) | `VARIANT` | `VARIANT` | Unchanged | No target | P2 | [GetPlateSectionPropertyId](../Property-Porting.md#getplatesectionpropertyid) |
| [GetPublishedProfileName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_publish.html#ga255dfc3675daa744d31d7cd41dc4e1f1) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [GetSTAADProfileName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_publish.html#ga505bb1119e1751bc9006263b43016121) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | Pending |
| [SetStandardProfileDBFolder](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#ga4e87d6b72233137048da0333c8565781) | `long` | `long` | Version policy candidate | No target | P9 | Pending |
| [GetStandardProfileDBFolder](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#gaa29514922cc9ae65f0d92da202144f5f) | `VARIANT` | `VARIANT` | Formatting/cross-reference only | No target | P9 | Pending |
| [GetDefaultStandardProfileDBFolder](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#gaf3c05fdd108cc02b0269073ae1543663) | Absent | `VARIANT` | Newly documented in 2025 | No target | P9 | Pending |
| [IsStandardDatabaseSection](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#gaeb4bb81496f3d6451e9a126ea0a6d3a6) | Absent | `VARIANT` | Newly documented in 2025 | No target | P9 | Pending |
| [GetStandardSectionDatabaseName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#ga527cf81f6e5369eec3076be58fde90cc) | Absent | `VARIANT` | Newly documented in 2025 | No target | P9 | Pending |
| [GetStandardSectionTableName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#ga45b152c818ab6a2b89be7d90892d0ded) | Absent | `VARIANT` | Newly documented in 2025 | No target | P9 | Pending |
| [GetStandardSectionName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#ga87dfe66234b751a9c9f5914de89e2fb1) | Absent | `VARIANT` | Newly documented in 2025 | No target | P9 | Pending |
| [GetProfileBoundaryInformation](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#ga97b4352067b45df2388452488c772be1) | `VARIANT` (summary only) | Absent | Reserved: do not use | No target | P0 | Excluded |
| [GetProfileNoOfPoints](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#ga32bba6cd08a1fb0c8b776376e6306c2f) | `VARIANT` (summary only) | Absent | Reserved: do not use | No target | P0 | Excluded |
| [GetProfileBoundaryPoints](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#ga51857b4c21c7bec5391a0a26f34a6a40) | `VARIANT` (summary only) | Absent | Reserved: do not use | No target | P0 | Excluded |

## 3. Function-by-function contracts and implementation proposals

Native VARIANT/Object does not establish a boxed scalar type, logical direction or SAFEARRAY element type. Windows native long is 32-bit. Mutable reference declarations are not automatically outputs: some are documented inputs. Parameter-table aliases and missing documentation remain explicit rather than being matched by position. Retained tables are functional contract data, not a claim of runtime verification.

Managed result families include qualified status/native codes, operation identity and data only when valid. Request field names must be descriptive at API review; the native names below are preserved solely to identify Bentley parameters. The source/private exclusions are deliberate, not pending implementations.

<a id="property-createbeampropertyfromtable"></a>

### 1. CreateBeamPropertyFromTable

**Section:** Section: Create Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_section.html#gaf2cc429be392fe527feed7dc33f78d6c) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_section.html#gaf2cc429be392fe527feed7dc33f78d6c) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#gaf2cc429be392fe527feed7dc33f78d6c)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P6 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateBeamPropertyFromTable(const VARIANT FAR&Country,const VARIANT FAR&SectionName,const VARIANT FAR&TypeSpec,const VARIANT FAR&AddSpec_1,const VARIANT FAR&AddSpec_2)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `Country` | `const VARIANT FAR&` | [in] | The value for the specified country (Type: long/Integer). Refer table below: Country CODE Country 1 American 2 Australian 3 British 4 Canadian 5 Chinese 6 Dutch 7 European 8 French 9 German 10 Indian 11 Japanese 12 Russian 13 SouthAfrican 14 Spanish 15 Venezuelan 16 Korean 17 Aluminum 18 American cold formed 19 Indian cold formed 20 Mexican 21 American Steel Joist 22 AITCTimber 23 Lysaght cold formed 24 British cold formed 25 Canadian Timber 26 Butler cold formed 27 Kingspan cold formed 28 RCeco cold formed 29 Japanese cold formed 30 Australian cold formed 31 Russian cold formed 32 STOASChM 33 Jindal 34 European cold formed 35 Tata Structura 36 Brazilian 37 APL Apollo Tubes |
| `SectionName` | `const VARIANT FAR&` | [in] | Name of the section(Type: string). |
| `TypeSpec` | `const VARIANT FAR&` | [in] | The type specification number(Type: long/Integer): Value Type Spec. Description 0 ST 2 D Double profile. 5 T Tee section cut from I shaped section (for aluminium) |
| `AddSpec_1` | `const VARIANT FAR&` | [in] | clear Spacing for Double profile(Type: double/float) |
| `AddSpec_2` | `const VARIANT FAR&` | [in] | please set it with 0.0(Type: double/float) |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned section property ID. |
| `0` | Library Error: Unable to create property. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateBeamPropertyFromTable(System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** [CreateBeamPropertyFromTable](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1086), [CreateHSectionProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1736), [CreateHSectionProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1746)<br>
**Main delegated targets:** [ISectionDoubleProfile.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/ISectionDoubleProfile.cs#L47), [TCISection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/TCISection.cs#L56)

**Proposed managed entry:** `CreateBeamPropertyFromTable(SectionDefinitionRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createbeampropertyfromtablewithcoverplates"></a>

### 2. CreateBeamPropertyFromTableWithCoverPlates

**Section:** Section: Create Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_section.html#gab96ba7f158851c24a2eb289af7f17c6f) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_section.html#gab96ba7f158851c24a2eb289af7f17c6f) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#gab96ba7f158851c24a2eb289af7f17c6f)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P6 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateBeamPropertyFromTableWithCoverPlates(const VARIANT FAR&Country,const VARIANT FAR&SectionName,const VARIANT FAR&TypeSpec,const VARIANT FAR&AddSpecArray)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `Country` | `const VARIANT FAR&` | [in] | The value for the specified country(Type: long/Integer), refer OSPropertyUI::CreateBeamPropertyFromTable |
| `SectionName` | `const VARIANT FAR&` | [in] | Name of the section(Type: string). |
| `TypeSpec` | `const VARIANT FAR&` | [in] | The type specification number(Type: long/Integer): Index Spec Type -1 Define 0 ST 1 RA 2 D 3 LD 4 SD 5 T (for aluminium) 6 CM 7 TC 8 BC 9 TB 10 BA (for aluminium) 11 FR 12 SA (for aluminium) |
| `AddSpecArray` | `const VARIANT FAR&` | [in] | VARIANT array of additional specification values(Type: long/Integer array): Spec Value Specification Description WP TH for TC and BC WP TH BW BT for TB / WP TH for TB CT FC for CM SP for D, BA and FR SP for LD and SD TH WT DT for Tube define OD ID for Pipe define |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned section property ID. |
| `0` | Library Error: Unable to create property. |
| `-6004` | Section is not found in profile database. |
| `-6005` | Section data for a section is not found. |
| `-6006` | Invalid section type. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateBeamPropertyFromTableWithCoverPlates(System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** [CreateCompositeBeamPropertyFromTable](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L253), [CreateHSectionProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1752), [CreateHSectionProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1758), [CreateHSectionProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1764)<br>
**Main delegated targets:** [ISectionWithCoverPlates.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/ISectionWithCoverPlates.cs#L74), [ISectionWithCoverPlates.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/ISectionWithCoverPlates.cs#L78), [ISectionWithCoverPlates.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/ISectionWithCoverPlates.cs#L82)

**Proposed managed entry:** `CreateBeamPropertyFromTableWithCoverPlates(SectionDefinitionRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createbeampropertyfromtablecomposite"></a>

### 3. CreateBeamPropertyFromTableComposite

**Section:** Section: Create Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_section.html#ga50040de9e0075880f3f7285284b7c79a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_section.html#ga50040de9e0075880f3f7285284b7c79a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga50040de9e0075880f3f7285284b7c79a)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P6 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateBeamPropertyFromTableComposite(const VARIANT FAR&Country,const VARIANT FAR&SectionName,const VARIANT FAR&TypeSpec,const VARIANT FAR&AddSpecArray)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `Country` | `const VARIANT FAR&` | [in] | The value for the specified country(Type: long/Integer), refer OSPropertyUI::CreateBeamPropertyFromTable |
| `SectionName` | `const VARIANT FAR&` | [in] | Name of the section(Type: string). |
| `TypeSpec` | `const VARIANT FAR&` | [in] | The type specification number(Type: long/Integer): Index Spec Type -1 Define 0 ST 1 RA 2 D 3 LD 4 SD 5 T (for aluminium) 6 CM 7 TC 8 BC 9 TB 10 BA (for aluminium) 11 FR 12 SA (for aluminium) |
| `AddSpecArray` | `const VARIANT FAR&` | [in] | VARIANT array of additional specification values(Type: long/Integer array): Spec Value Specification Description WP TH for TC and BC WP TH BW BT for TB / WP TH for TB CT FC for CM SP for D, BA and FR SP for LD and SD TH WT DT for Tube define OD ID for Pipe define |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned section property ID. |
| `0` | Library Error: Unable to create property. |
| `-6004` | Section is not found in profile database. |
| `-6005` | Section data for a section is not found. |
| `-6006` | Invalid section type. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateBeamPropertyFromTableComposite(System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** [CreateHSectionProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1770)<br>
**Main delegated targets:** [ISectionComposite.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/ISectionComposite.cs#L50)

**Proposed managed entry:** `CreateBeamPropertyFromTableComposite(SectionDefinitionRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createwideflangepropertyfromtable"></a>

### 4. CreateWideFlangePropertyFromTable

**Section:** Section: Create Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_section.html#ga78cec5f9be32c1061963f94ed050d331) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_section.html#ga78cec5f9be32c1061963f94ed050d331) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga78cec5f9be32c1061963f94ed050d331)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P6 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateWideFlangePropertyFromTable(const VARIANT FAR&nCountry,const VARIANT FAR&SectionName,const VARIANT FAR&nTypeSpec,const VARIANT FAR&varSpecs)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nCountry` | `const VARIANT FAR&` | [in] | The country CODE. (American = 1, Australian = 2, British = 3, Canadian = 4, Chinese = 5, Dutch = 6, European = 7, French = 8, German = 9, Indian = 10, Japanese = 11, Russian = 12, SouthAfrican = 13, Spanish = 14, Venezuelan = 15, Korean = 16). |
| `SectionName` | `const VARIANT FAR&` | [in] | Name of the section. |
| `nTypeSpec` | `const VARIANT FAR&` | [in] | The type specification number. (ST = 0; D = 2; T = 5; CM = 6; TC = 7; BC = 8; TB = 9). |
| `varSpecs` | `const VARIANT FAR&` | [in] | The specification values in VARIANT type array: Array Index Spec Type Description 0 SP/CT/WP SP:-Spacing for double-I, double-C, double-L CT:-Conc. thickness for composite-I WP:-Width of top cover plate for TC,TB and bottom cover plate for BC 1 FC/TH FC:-Concrete grade for composite-I TH:-Thickness of top cover plate for TC,TB and bottom cover plate for BC 2 CW/BW CW:-Concrete width for composite-I BW:-Width of bottom cover plate for TB 3 CD/BT CD:-Concrete density for composite-I BT:-Thickness of bottom cover plate for TB |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned section property ID. |
| `-1` | General Error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateWideFlangePropertyFromTable(System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateWideFlangePropertyFromTable(SectionDefinitionRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createbeampropertyfromtableex"></a>

### 5. CreateBeamPropertyFromTableEx

**Section:** Section: Create Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_section.html#gadce802ef32861d83b01a4226b234939c) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_section.html#gadce802ef32861d83b01a4226b234939c) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#gadce802ef32861d83b01a4226b234939c)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P6 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateBeamPropertyFromTableEx(const VARIANT FAR&Country,const VARIANT FAR&SectionName,const VARIANT FAR&TypeSpec)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `Country` | `const VARIANT FAR&` | [in] | The value for the specified country(Type: long/Integer), refer OSPropertyUI::CreateBeamPropertyFromTable |
| `SectionName` | `const VARIANT FAR&` | [in] | Name of the section(Type: string). |
| `TypeSpec` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `TypeSolidShape` | [in] | The type specification number(Type: long/Integer): Solid Shape ID The shape of section 1 Plate Strip 2 Solid Rect 3 Solid Round 4 Round 5 Cable |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned section property ID. |
| `0` | Library Error: Unable to create property. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateBeamPropertyFromTableEx(System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateBeamPropertyFromTableEx(SectionDefinitionRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createteepropertyfromtable"></a>

### 6. CreateTeePropertyFromTable

**Section:** Section: Create Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_section.html#ga9623d2b80ee04d55f188772469ae23e7) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_section.html#ga9623d2b80ee04d55f188772469ae23e7) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga9623d2b80ee04d55f188772469ae23e7)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P6 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateTeePropertyFromTable(const VARIANT FAR&Country,const VARIANT FAR&SectionName,const VARIANT FAR&TypeSpec)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `Country` | `const VARIANT FAR&` | [in] | The value for the specified country(Type: long/Integer), refer OSPropertyUI::CreateBeamPropertyFromTable |
| `SectionName` | `const VARIANT FAR&` | [in] | Name of the section(Type: string). |
| `TypeSpec` | `const VARIANT FAR&` | [in] | The type specification number(Type: long/Integer): Index Spec Type -1 Define 0 ST 5 T From Wide Flange |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned section property ID. |
| `0` | Library Error: Unable to create property. |
| `-6004` | Section is not found in profile database. |
| `-6005` | Section data for a section is not found. |
| `-6006` | Invalid section type. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateTeePropertyFromTable(System.Object, System.Object, System.Object)`

**Main adapter targets:** [CreateHSectionProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1740)<br>
**Main delegated targets:** [TCISection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/TCISection.cs#L60), [TCISection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/TCISection.cs#L68)

**Proposed managed entry:** `CreateTeePropertyFromTable(SectionDefinitionRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createchannelpropertyfromtable"></a>

### 7. CreateChannelPropertyFromTable

**Section:** Section: Create Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_section.html#gaa08e9a925a2afa0ba3abde267f4eadb1) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_section.html#gaa08e9a925a2afa0ba3abde267f4eadb1) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#gaa08e9a925a2afa0ba3abde267f4eadb1)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P6 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateChannelPropertyFromTable(const VARIANT FAR&Country,const VARIANT FAR&SectionName,const VARIANT FAR&TypeSpec,const VARIANT FAR&AddSpec_1)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `Country` | `const VARIANT FAR&` | [in] | The value for the specified country(Type: long/Integer), refer OSPropertyUI::CreateBeamPropertyFromTable |
| `SectionName` | `const VARIANT FAR&` | [in] | Name of the section(Type: string). |
| `TypeSpec` | `const VARIANT FAR&` | [in] | The type specification number(Type: long/Integer): Index Spec Type -1 Define 0 ST 1 RA 2 D 3 LD 4 SD 5 T (for aluminium) 6 CM 7 TC 8 BC 9 TB 10 BA (for aluminium) 11 FR 12 SA (for aluminium) |
| `AddSpec_1` | `const VARIANT FAR&` | [in] | Additional specification value(Type: double/float) SP : Spec Value Specification Description WP TH for TC and BC WP TH BW BT for TB / WP TH for TB CT FC for CM SP for D, BA and FR SP for LD and SD TH WT DT for Tube define OD ID for Pipe define |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned section property ID. |
| `0` | Library Error: Unable to create property. |
| `-6004` | Section is not found in profile database. |
| `-6005` | Section data for a section is not found. |
| `-6006` | Invalid section type. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateChannelPropertyFromTable(System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** [CreateCSectionProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1780), [CreateCSectionProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1786), [CreateCSectionProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1792)<br>
**Main delegated targets:** [CSectionDoubleProfile.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/CSectionDoubleProfile.cs#L51), [CSectionDoubleProfile.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/CSectionDoubleProfile.cs#L55), [TCISection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/TCISection.cs#L64)

**Proposed managed entry:** `CreateChannelPropertyFromTable(SectionDefinitionRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createanglepropertyfromtable"></a>

### 8. CreateAnglePropertyFromTable

**Section:** Section: Create Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_section.html#ga6f1c73d26e890715030ddd5e1f765b20) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_section.html#ga6f1c73d26e890715030ddd5e1f765b20) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga6f1c73d26e890715030ddd5e1f765b20)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P6 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateAnglePropertyFromTable(const VARIANT FAR&Country,const VARIANT FAR&SectionName,const VARIANT FAR&TypeSpec,const VARIANT FAR&AddSpec_1)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `Country` | `const VARIANT FAR&` | [in] | The value for the specified country(Type: long/Integer), refer OSPropertyUI::CreateBeamPropertyFromTable |
| `SectionName` | `const VARIANT FAR&` | [in] | Name of the section(Type: string). |
| `TypeSpec` | `const VARIANT FAR&` | [in] | The type specification number(Type: long/Integer) [Please refer to enum 'ProfileType']: Value Spec Type Description 0 ST Single section from the standard built-in tables. 1 RA Single angle with reverse Y-Z axes (refer to G.4.2 Local Coordinate System). 3 LD Double angle with long legs back-to-back. 4 SD Double angle with short legs back-to-back 12 SA Double angle in a star arrangement (heel to heel)[for Aluminium] |
| `AddSpec_1` | `const VARIANT FAR&` | [in] | Additional specification value(Type: double/float) SP : Spec Value Specification Description WP TH for TC and BC WP TH BW BT for TB / WP TH for TB CT FC for CM SP for D, BA and FR SP for LD and SD TH WT DT for Tube define OD ID for Pipe define |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned section property ID. |
| `0` | Library Error: Unable to create property. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateAnglePropertyFromTable(System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** [CreateLSectionProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1803), [CreateLSectionProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1809)<br>
**Main delegated targets:** [LOrBoxSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/LOrBoxSection.cs#L56), [LOrBoxSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/LOrBoxSection.cs#L60), [LOrBoxSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/LOrBoxSection.cs#L64), [LSectionDoubleProfile.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/LSectionDoubleProfile.cs#L50), [LSectionDoubleProfile.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/LSectionDoubleProfile.cs#L54)

**Proposed managed entry:** `CreateAnglePropertyFromTable(SectionDefinitionRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createtubepropertyfromtable"></a>

### 9. CreateTubePropertyFromTable

**Section:** Section: Create Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_section.html#ga64ab21d46e0b14bdb71c9bd0f1e84c29) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_section.html#ga64ab21d46e0b14bdb71c9bd0f1e84c29) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga64ab21d46e0b14bdb71c9bd0f1e84c29)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P6 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateTubePropertyFromTable(const VARIANT FAR&Country,const VARIANT FAR&SectionName,const VARIANT FAR&TypeSpec,const VARIANT FAR&AddSpec_1,const VARIANT FAR&AddSpec_2,const VARIANT FAR&AddSpec_3)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `Country` | `const VARIANT FAR&` | [in] | The value for the specified country(Type: long/Integer), refer OSPropertyUI::CreateBeamPropertyFromTable |
| `SectionName` | `const VARIANT FAR&` | [in] | Name of the section(Type: string). |
| `TypeSpec` | `const VARIANT FAR&` | [in] | The type specification number(Type: long/Integer): Index Spec Type -1 Define 0 ST 1 RA 2 D 3 LD 4 SD 5 T (for aluminium) 6 CM 7 TC 8 BC 9 TB 10 BA (for aluminium) 11 FR 12 SA (for aluminium) |
| `AddSpec_1` | `const VARIANT FAR&` | [in] | Additional specification value(Type: double/float) TH .: Spec Value Specification Description WP TH for TC and BC WP TH BW BT for TB / WP TH for TB CT FC for CM SP for D, BA and FR SP for LD and SD TH WT DT for Tube define OD ID for Pipe define |
| `AddSpec_2` | `const VARIANT FAR&` | [in] | Additional specification value(Type: double/float) WT : Spec Specification Value WP TH for TC and BC WP TH BW BT for TB / WP TH for TB CT FC for CM SP for D, BA and FR SP for LD and SD TH WT DT for Tube define OD ID for Pipe define |
| `AddSpec_3` | `const VARIANT FAR&` | [in] | Additional specification value(Type: double/float) DT : Spec Specification Value WP TH for TC and BC WP TH BW BT for TB / WP TH for TB CT FC for CM SP for D, BA and FR SP for LD and SD TH WT DT for Tube define OD ID for Pipe define |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned section property ID. |
| `0` | Library Error: Unable to create property. |
| `-6004` | Section is not found in profile database. |
| `-6005` | Section data for a section is not found. |
| `-6006` | Invalid section type. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateTubePropertyFromTable(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** [CreateTubeSectionProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1835), [CreateTubeSectionProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1841)<br>
**Main delegated targets:** [LOrBoxSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/LOrBoxSection.cs#L68)

**Proposed managed entry:** `CreateTubePropertyFromTable(SectionDefinitionRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createpipepropertyfromtable"></a>

### 10. CreatePipePropertyFromTable

**Section:** Section: Create Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_section.html#ga8df35fc2730a95842893d45040aaa00b) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_section.html#ga8df35fc2730a95842893d45040aaa00b) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga8df35fc2730a95842893d45040aaa00b)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P6 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreatePipePropertyFromTable(const VARIANT FAR&Country,const VARIANT FAR&SectionName,const VARIANT FAR&TypeSpec,const VARIANT FAR&AddSpec_1,const VARIANT FAR&AddSpec_2)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `Country` | `const VARIANT FAR&` | [in] | The value for the specified country(Type: long/Integer), refer OSPropertyUI::CreateBeamPropertyFromTable |
| `SectionName` | `const VARIANT FAR&` | [in] | Name of the section(Type: string). |
| `TypeSpec` | `const VARIANT FAR&` | [in] | The type specification number(Type: long/Integer): Index Spec Type -1 Define 0 ST 1 RA 2 D 3 LD 4 SD 5 T (for aluminium) 6 CM 7 TC 8 BC 9 TB 10 BA (for aluminium) 11 FR 12 SA (for aluminium) |
| `AddSpec_1` | `const VARIANT FAR&` | [in] | Additional specification value(Type: double/float) OD : Spec Value Specification Description WP TH for TC and BC WP TH BW BT for TB / WP TH for TB CT FC for CM SP for D, BA and FR SP for LD and SD TH WT DT for Tube define OD ID for Pipe define |
| `AddSpec_2` | `const VARIANT FAR&` | [in] | Additional specification value(Type: double/float) ID : Spec Value Specification Description WP TH for TC and BC WP TH BW BT for TB / WP TH for TB CT FC for CM SP for D, BA and FR SP for LD and SD TH WT DT for Tube define OD ID for Pipe define |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned section property ID. |
| `0` | Library Error: Unable to create property. |
| `-6004` | Section is not found in profile database. |
| `-6005` | Section data for a section is not found. |
| `-6006` | Invalid section type. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreatePipePropertyFromTable(System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** [CreatePipeSectionProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1819), [CreatePipeSectionProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1825)<br>
**Main delegated targets:** [PipeSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PipeSection.cs#L48)

**Proposed managed entry:** `CreatePipePropertyFromTable(SectionDefinitionRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createprismaticrectangleproperty"></a>

### 11. CreatePrismaticRectangleProperty

**Section:** Section: Create Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_section.html#ga4ef3ab387d4b23c1c8e3d3e2d7b7783d) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_section.html#ga4ef3ab387d4b23c1c8e3d3e2d7b7783d) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga4ef3ab387d4b23c1c8e3d3e2d7b7783d)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P6 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreatePrismaticRectangleProperty(const VARIANT FAR&varfYD,const VARIANT FAR&varfZD)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varfYD` | `const VARIANT FAR&` | [in] | The depth along the local Y-axis. |
| `varfZD` | `const VARIANT FAR&` | [in] | The width along the local Z-axis. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned section property ID. |
| `0` | Library Error: Unable to create property. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreatePrismaticRectangleProperty(System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** [PrismaticRectangleSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticRectangleSection.cs#L47)

**Proposed managed entry:** `CreatePrismaticRectangleProperty(SectionDefinitionRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createprismaticcircleproperty"></a>

### 12. CreatePrismaticCircleProperty

**Section:** Section: Create Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_section.html#ga016cb17310c1214ad731ef8603f74fcc) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_section.html#ga016cb17310c1214ad731ef8603f74fcc) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga016cb17310c1214ad731ef8603f74fcc)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P6 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreatePrismaticCircleProperty(const VARIANT FAR&varfYD)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varfYD` | `const VARIANT FAR&` | [in] | The circle diameter. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned section property ID. |
| `0` | Library Error: Unable to create property. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreatePrismaticCircleProperty(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** [PrismaticCircleSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticCircleSection.cs#L38)

**Proposed managed entry:** `CreatePrismaticCircleProperty(SectionDefinitionRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createprismaticteeproperty"></a>

### 13. CreatePrismaticTeeProperty

**Section:** Section: Create Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_section.html#ga26721d08a647bddd960d0499b42ac5c9) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_section.html#ga26721d08a647bddd960d0499b42ac5c9) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga26721d08a647bddd960d0499b42ac5c9)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P6 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreatePrismaticTeeProperty(const VARIANT FAR&varfYD,const VARIANT FAR&varfZD,const VARIANT FAR&varfYB,const VARIANT FAR&varfZB)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varfYD` | `const VARIANT FAR&` | [in] | Total depth of section (top fiber of flange to bottom fiber of web). |
| `varfZD` | `const VARIANT FAR&` | [in] | Width of flange. |
| `varfYB` | `const VARIANT FAR&` | [in] | Depth of stem. |
| `varfZB` | `const VARIANT FAR&` | [in] | Width of stem. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned section property ID. |
| `0` | Library Error: Unable to create property. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreatePrismaticTeeProperty(System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** [PrismaticTeeSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticTeeSection.cs#L57)

**Proposed managed entry:** `CreatePrismaticTeeProperty(SectionDefinitionRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createprismatictrapezoidalproperty"></a>

### 14. CreatePrismaticTrapezoidalProperty

**Section:** Section: Create Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_section.html#ga0fdc7ad624d8f07839d85b017b518350) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_section.html#ga0fdc7ad624d8f07839d85b017b518350) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga0fdc7ad624d8f07839d85b017b518350)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P6 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreatePrismaticTrapezoidalProperty(const VARIANT FAR&varfYD,const VARIANT FAR&varfZD,const VARIANT FAR&varfZB)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varfYD` | `const VARIANT FAR&` | [in] | Total depth of section. |
| `varfZD` | `const VARIANT FAR&` | [in] | Width of section at top fiber. |
| `varfZB` | `const VARIANT FAR&` | [in] | Width of section at bottom fiber. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned section property ID. |
| `0` | Library Error: Unable to create property. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreatePrismaticTrapezoidalProperty(System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** [PrismaticTrapezoidalSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticTrapezoidalSection.cs#L52)

**Proposed managed entry:** `CreatePrismaticTrapezoidalProperty(SectionDefinitionRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createprismaticgeneralproperty"></a>

### 15. CreatePrismaticGeneralProperty

**Section:** Section: Create Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_section.html#gae35d506434e6db9abd1edcb540a9d1cb) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_section.html#gae35d506434e6db9abd1edcb540a9d1cb) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#gae35d506434e6db9abd1edcb540a9d1cb)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P6 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreatePrismaticGeneralProperty(const VARIANT FAR&varfaProperties)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varfaProperties` | `const VARIANT FAR&` | [in] | The property values in VARIANT type array: Array Index Property Type Description 0 AX Cross section area 1 AY Shear area in local Y-axis. If zero, shear deformation is ignored in the analysis. 2 AZ Shear area in local Z-axis. If zero, shear deformation is ignored in the analysis. 3 IX Torsional constant 4 IY Moment of inertia about local Y-axis 5 IZ Moment of inertia about local Z-axis 6 YD Depth of the section in the direction of the local Y-axis. 7 ZD Depth of the section in the direction of the local Z-axis. 8 YB Depth of stem (T-beams); width of section at top fiber (trapezoidal beams) 9 ZB Width of stem (T-beams); width of section at bottom fiber (trapezoidal beams) |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned section property ID. |
| `0` | Library Error: Unable to create property. |
| `-106` | One dimensional array of double expected. |
| `-108` | Array size is smaller than expected. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreatePrismaticGeneralProperty(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** [PrismaticGeneralSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticGeneralSection.cs#L63)

**Proposed managed entry:** `CreatePrismaticGeneralProperty(SectionDefinitionRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createtaperediproperty"></a>

### 16. CreateTaperedIProperty

**Section:** Section: Create Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_section.html#gad01c26661a6e9a588c4e5cfb46ce9403) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_section.html#gad01c26661a6e9a588c4e5cfb46ce9403) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#gad01c26661a6e9a588c4e5cfb46ce9403)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P6 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateTaperedIProperty(const VARIANT FAR&varfaProperties)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varfaProperties` | `const VARIANT FAR&` | [in] | The varfa properties. Array Index Property Type Description 0 F1 Depth of section at start node. 1 F2 Thickness of web. 2 F3 Depth of section at end node. 3 F4 Width of top flange. 4 F5 Thickness of top flange. 5 F6 Width of bottom flange. Defaults to F4 if left out. 6 F7 Thickness of bottom flange. Defaults to F5 left out. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned section property ID. |
| `0` | Library Error: Unable to create property. |
| `-106` | One dimensional array of double expected. |
| `-108` | Array size is smaller than expected. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateTaperedIProperty(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** [TaperISection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/TaperISection.cs#L71)

**Proposed managed entry:** `CreateTaperedIProperty(SectionDefinitionRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createtaperedtubeproperty"></a>

### 17. CreateTaperedTubeProperty

**Section:** Section: Create Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_section.html#ga558bd4cf571d37c5f3520b2e7e56b9e5) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_section.html#ga558bd4cf571d37c5f3520b2e7e56b9e5) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga558bd4cf571d37c5f3520b2e7e56b9e5)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P6 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateTaperedTubeProperty(const VARIANT FAR&varnTubeType,const VARIANT FAR&varfd1,const VARIANT FAR&varfd2,const VARIANT FAR&varfTh)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnTubeType` | `const VARIANT FAR&` | [in] | Type of the tube: Type of Tube Value Round 0 HexDecagonal 1 Dodecagonal 2 Octagonal 3 Hexagonal 4 Square 5 For additional information, please refer to Section 5.20.2.1 of the Technical Reference Manual. |
| `varfd1` | `const VARIANT FAR&` | [in] | Depth of section at start of member. |
| `varfd2` | `const VARIANT FAR&` | [in] | Depth of section at end of member. |
| `varfTh` | `const VARIANT FAR&` | [in] | Thickness of section (constant throughout the member length). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned section property ID. |
| `0` | Library Error: Unable to create property. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateTaperedTubeProperty(System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** [TaperTubeSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/TaperTubeSection.cs#L52)

**Proposed managed entry:** `CreateTaperedTubeProperty(SectionDefinitionRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createplatethicknessproperty"></a>

### 18. CreatePlateThicknessProperty

**Section:** Section: Create Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_section.html#ga4ab7a973986b611d748302e8dcdc0154) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_section.html#ga4ab7a973986b611d748302e8dcdc0154) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga4ab7a973986b611d748302e8dcdc0154)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P6 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreatePlateThicknessProperty(const VARIANT FAR&faThickness)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `faThickness` | `const VARIANT FAR&` | [in] | The thickness for all nodes. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned section property ID. |
| `-106` | faThickness dimensional array error. |
| `-6003` | Library error: Unable to create property. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreatePlateThicknessProperty(System.Object)`

**Main adapter targets:** [CreatePlateElementThickness](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1167), [CreatePlateThicknessProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1402)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreatePlateThicknessProperty(ThicknessDefinitionRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createupttable"></a>

### 19. CreateUPTTable

**Section:** Section: Create Profile from UPT Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#gacffe1377c08a287a4b95c1fc315f4127) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_u_p_t_section.html#gacffe1377c08a287a4b95c1fc315f4127) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#gacffe1377c08a287a4b95c1fc315f4127)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateUPTTable(VARIANT FAR&nTableType)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableType` | `VARIANT FAR&` | [in] | Type of the table. No. Table Type 1 scUserTableWideFlangeTitle 2 scUserTableChannelTitle 3 scUserTableAngleTitle 4 scUserTableDoubleAngleTitle 5 scUserTableTeeTitle 6 scUserTablePipeTitle 7 scUserTableTubeTitle 8 scUserTableGeneralTitle 9 scUserTableIsectionTitle 10 scUserTablePrismaticTitle |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | User Provided Table (UPT) number ID. |
| `-6031` | Cannot create UPT. Unknown table type specified. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateUPTTable(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** [UptAngleSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptAngleSection.cs#L49), [UptChannelSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptChannelSection.cs#L52), [UptDoubleAngleSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptDoubleAngleSection.cs#L52), [UptGeneralSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptGeneralSection.cs#L88), [UptISection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptISection.cs#L88), [UptPipeSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptPipeSection.cs#L55), [UptPrismaticSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptPrismaticSection.cs#L56), [UptTeeSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptTeeSection.cs#L52), [UptTubeSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptTubeSection.cs#L42), [UptWideFlangeSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptWideFlangeSection.cs#L55)

**Proposed managed entry:** `CreateUPTTable(UserTableRequest request)`<br>
**Proposed named result:** `UserTableCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-adduptpropertywideflange"></a>

### 20. AddUPTPropertyWIDEFLANGE

**Section:** Section: Create Profile from UPT Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#gaf8641a9ec57a7ad52f1f4ee44c4f5168) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_u_p_t_section.html#gaf8641a9ec57a7ad52f1f4ee44c4f5168) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#gaf8641a9ec57a7ad52f1f4ee44c4f5168)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AddUPTPropertyWIDEFLANGE(VARIANT FAR&nTableRef,VARIANT FAR&varSectionName,VARIANT FAR&varAX,VARIANT FAR&varD,VARIANT FAR&varTW,VARIANT FAR&varWF,VARIANT FAR&varTF,VARIANT FAR&varIZ,VARIANT FAR&varIY,VARIANT FAR&varIX,VARIANT FAR&varAY,VARIANT FAR&varAZ)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableRef` | `VARIANT FAR&` | [in] | The existing table number ID. |
| `varSectionName` | `VARIANT FAR&` | [in] | UPT section string name. |
| `varAX` | `VARIANT FAR&` | [in] | Cross section area ( AX ). |
| `varD` | `VARIANT FAR&` | [in] | Depth of the section ( D ). |
| `varTW` | `VARIANT FAR&` | [in] | Thickness of web ( TW ). |
| `varWF` | `VARIANT FAR&` | [in] | Width of the top flange ( WF ). |
| `varTF` | `VARIANT FAR&` | [in] | Thickness of top flange ( TF ). |
| `varIZ` | `VARIANT FAR&` | [in] | Torsional constant ( IZ ). |
| `varIY` | `VARIANT FAR&` | [in] | Moment of inertia about local y-axis ( IY ). |
| `varIX` | `VARIANT FAR&` | [in] | Moment of inertia about local z-axis ( IX ). |
| `varAY` | `VARIANT FAR&` | [in] | Shear area in local y-axis. If zero, shear deformation is ignored in the analysis ( AY ). |
| `varAZ` | `VARIANT FAR&` | [in] | Shear area in local z-axis. If zero, shear deformation is ignored in the analysis ( AZ ). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-6032` | Unable to add section varSectionName in UPT nTableRef . |
| `-6045` | Section with same name exits in UPT nTableRef . |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AddUPTPropertyWIDEFLANGE(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** [UptWideFlangeSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptWideFlangeSection.cs#L56)

**Proposed managed entry:** `AddUPTPropertyWIDEFLANGE(UserTableRequest request)`<br>
**Proposed named result:** `UserTableSectionMutationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-adduptpropertywideflangeunequal"></a>

### 21. AddUPTPropertyWIDEFLANGEUNEQUAL

**Section:** Section: Create Profile from UPT Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#ga416ce6e066c01171353a67dceb72d404) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_u_p_t_section.html#ga416ce6e066c01171353a67dceb72d404) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga416ce6e066c01171353a67dceb72d404)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AddUPTPropertyWIDEFLANGEUNEQUAL(VARIANT FAR&nTableRef,VARIANT FAR&varSectionName,VARIANT FAR&varPropSpecArray)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableRef` | `VARIANT FAR&` | [in] | The existing table number ID. |
| `varSectionName` | `VARIANT FAR&` | [in] | UPT section string name(Type: string). |
| `varPropSpecArray` | `VARIANT FAR&` | [in] | section property array (Type: double array with 12 size allocated). index Index Prop Spec Value 0 Ax 1 D 2 TW 3 WF 4 TF 5 IZ 6 IY 7 IX 8 AY 9 AZ 10 WF1 11 TF1 |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE` | Add unequal wide flange successful. |
| `FALSE` | Add unequal wide flange generate error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AddUPTPropertyWIDEFLANGEUNEQUAL(System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `AddUPTPropertyWIDEFLANGEUNEQUAL(UserTableRequest request)`<br>
**Proposed named result:** `UserTableSectionMutationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-adduptpropertywideflangecomposite"></a>

### 22. AddUPTPropertyWIDEFLANGECOMPOSITE

**Section:** Section: Create Profile from UPT Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#gad0aeaec8b6052eaa4e713b184506e935) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_u_p_t_section.html#gad0aeaec8b6052eaa4e713b184506e935) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#gad0aeaec8b6052eaa4e713b184506e935)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AddUPTPropertyWIDEFLANGECOMPOSITE(const VARIANT FAR&nTableRef,const VARIANT FAR&varSectionName,const VARIANT FAR&varPropSpecArray)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableRef` | `const VARIANT FAR&` | [in] | The existing table number ID. |
| `varSectionName` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varPropSpecArray` | `const VARIANT FAR&` | [in] | - VARIANT double array of Profile Specifications data of size 12 (without additional composite flange & bottom plate) 16(with only additional composite flange) 19 (with both additional composite flange & bottom plate inputs) : Index Data 0 Cross section area ( AX ). 1 Depth of the section ( D ). 2 Thickness of web ( TW ). 3 Width of the top flange ( WF ). 4 Thickness of top flange ( TF ). 5 Torsional constant ( IZ ). 6 Moment of inertia about local y-axis ( IY ). 7 Moment of inertia about local z-axis ( IX ). 8 Shear area in local y-axis. If zero, shear deformation is ignored in the analysis ( AY ). 9 Shear area in local z-axis. If zero, shear deformation is ignored in the analysis ( AZ ). 10 Width of the bottom flange ( WF1 ). 11 Thickness of bottom flange ( TF1 ). 12 ( for additional composite flange ) Width of the composite slab to the right of the web center line ( CFR ). 13 ( for additional composite flange ) Width of the composite slab to the left of the web center line ( CFL ). 14 ( for additional composite flange ) Thickness of the composite slab ( CFT ). 15 ( for additional composite flange ) Modular ratio of the concrete in the composite slab ( MR ). 16 ( for additional bottom plate ) Width of the additional bottom flange plate to the right of the web center line ( BPR ). 17 ( for additional bottom plate ) Width of the additional bottom flange plate to the right of the web center line ( BPL ). 18 ( for additional bottom plate ) Thickness of the additional bottom flange plate ( BPT ). For additional information, please refer to Section 5.19.1 of the Technical Reference Manual. |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `strSectionName` | [in] | UPT section string name. |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE/1` | OK. |
| `FALSE/0` | Error |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AddUPTPropertyWIDEFLANGECOMPOSITE(System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `AddUPTPropertyWIDEFLANGECOMPOSITE(UserTableRequest request)`<br>
**Proposed named result:** `UserTableSectionMutationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-adduptpropertychannel"></a>

### 23. AddUPTPropertyCHANNEL

**Section:** Section: Create Profile from UPT Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#ga762e3143fd9fa6d9ffc3f213af23d57f) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_u_p_t_section.html#ga762e3143fd9fa6d9ffc3f213af23d57f) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga762e3143fd9fa6d9ffc3f213af23d57f)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AddUPTPropertyCHANNEL(VARIANT FAR&nTableRef,VARIANT FAR&varSectionName,VARIANT FAR&varAX,VARIANT FAR&varD,VARIANT FAR&varTW,VARIANT FAR&varWF,VARIANT FAR&varTF,VARIANT FAR&varIZ,VARIANT FAR&varIY,VARIANT FAR&varIX,VARIANT FAR&varCZ,VARIANT FAR&varAY,VARIANT FAR&varAZ)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableRef` | `VARIANT FAR&` | [in] | The existing table number ID. |
| `varSectionName` | `VARIANT FAR&` | [in] | UPT section string name. |
| `varAX` | `VARIANT FAR&` | [in] | Cross section area ( AX ). |
| `varD` | `VARIANT FAR&` | [in] | Depth of the section ( D ). |
| `varTW` | `VARIANT FAR&` | [in] | Thickness of web ( TW ). |
| `varWF` | `VARIANT FAR&` | [in] | Width of the top flange ( WF ). |
| `varTF` | `VARIANT FAR&` | [in] | Thickness of top flange ( TF ). |
| `varIZ` | `VARIANT FAR&` | [in] | Torsional constant ( IZ ). |
| `varIY` | `VARIANT FAR&` | [in] | Moment of inertia about local y-axis ( IY ). |
| `varIX` | `VARIANT FAR&` | [in] | Moment of inertia about local z-axis ( IX ). |
| `varCZ` | `VARIANT FAR&` | [in] | Value CZ |
| `varAY` | `VARIANT FAR&` | [in] | Shear area in local y-axis. If zero, shear deformation is ignored in the analysis ( AY ). |
| `varAZ` | `VARIANT FAR&` | [in] | Shear area in local z-axis. If zero, shear deformation is ignored in the analysis ( AZ ). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-6032` | Unable to add section varSectionName in UPT nTableRef . |
| `-6045` | Section with same name exits in UPT nTableRef . |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AddUPTPropertyCHANNEL(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** [UptChannelSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptChannelSection.cs#L53)

**Proposed managed entry:** `AddUPTPropertyCHANNEL(UserTableRequest request)`<br>
**Proposed named result:** `UserTableSectionMutationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-adduptpropertyangle"></a>

### 24. AddUPTPropertyANGLE

**Section:** Section: Create Profile from UPT Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#ga4d33b8f55788d2d6a28d75542179a38e) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_u_p_t_section.html#ga4d33b8f55788d2d6a28d75542179a38e) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga4d33b8f55788d2d6a28d75542179a38e)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AddUPTPropertyANGLE(VARIANT FAR&nTableRef,VARIANT FAR&varSectionName,VARIANT FAR&varD,VARIANT FAR&varWF,VARIANT FAR&varTF,VARIANT FAR&varR,VARIANT FAR&varAY,VARIANT FAR&varAZ)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableRef` | `VARIANT FAR&` | [in] | The existing table number ID. |
| `varSectionName` | `VARIANT FAR&` | [in] | UPT section string name. |
| `varD` | `VARIANT FAR&` | [in] | Depth of angle ( D ). |
| `varWF` | `VARIANT FAR&` | [in] | Width of angle ( WF ). |
| `varTF` | `VARIANT FAR&` | [in] | Thickness of flanges ( TF ). |
| `varR` | `VARIANT FAR&` | [in] | Radius of gyration about principal axis ( R ). |
| `varAY` | `VARIANT FAR&` | [in] | Shear area in local y-axis. If zero, shear deformation is ignored in the analysis ( AY ). |
| `varAZ` | `VARIANT FAR&` | [in] | Shear area in local z-axis. If zero, shear deformation is ignored in the analysis ( AZ ). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-6032` | Unable to add section varSectionName in UPT nTableRef . |
| `-6045` | Section with same name exits in UPT nTableRef . |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AddUPTPropertyANGLE(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** [UptAngleSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptAngleSection.cs#L50)

**Proposed managed entry:** `AddUPTPropertyANGLE(UserTableRequest request)`<br>
**Proposed named result:** `UserTableSectionMutationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-adduptpropertydoubleangle"></a>

### 25. AddUPTPropertyDOUBLEANGLE

**Section:** Section: Create Profile from UPT Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#ga7659bafc3884233e8006403264df40b2) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_u_p_t_section.html#ga7659bafc3884233e8006403264df40b2) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga7659bafc3884233e8006403264df40b2)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AddUPTPropertyDOUBLEANGLE(VARIANT FAR&nTableRef,VARIANT FAR&varSectionName,VARIANT FAR&varD,VARIANT FAR&varWF,VARIANT FAR&varTF,VARIANT FAR&varSP,VARIANT FAR&varIZ,VARIANT FAR&varIY,VARIANT FAR&varIX,VARIANT FAR&varCY,VARIANT FAR&varAY,VARIANT FAR&varAZ)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableRef` | `VARIANT FAR&` | [in] | The existing table number ID. |
| `varSectionName` | `VARIANT FAR&` | [in] | UPT section string name. |
| `varD` | `VARIANT FAR&` | [in] | Depth of angle ( D ). |
| `varWF` | `VARIANT FAR&` | [in] | Width of angle ( WF ). |
| `varTF` | `VARIANT FAR&` | [in] | Thickness of flanges ( TF ). |
| `varSP` | `VARIANT FAR&` | [in] | Distance between two angle ( SP ). |
| `varIZ` | `VARIANT FAR&` | [in] | Torsional constant ( IZ ). |
| `varIY` | `VARIANT FAR&` | [in] | Moment of inertia about local y-axis ( IY ). |
| `varIX` | `VARIANT FAR&` | [in] | Moment of inertia about local z-axis ( IX ). |
| `varCY` | `VARIANT FAR&` | [in] | Distance from z axis to the top of section ( CY ). |
| `varAY` | `VARIANT FAR&` | [in] | Shear area in local y-axis. If zero, shear deformation is ignored in the analysis ( AY ). |
| `varAZ` | `VARIANT FAR&` | [in] | Shear area in local z-axis. If zero, shear deformation is ignored in the analysis ( AZ ). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-6032` | Unable to add section varSectionName in UPT nTableRef . |
| `-6045` | Section with same name exits in UPT nTableRef . |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AddUPTPropertyDOUBLEANGLE(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** [UptDoubleAngleSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptDoubleAngleSection.cs#L53)

**Proposed managed entry:** `AddUPTPropertyDOUBLEANGLE(UserTableRequest request)`<br>
**Proposed named result:** `UserTableSectionMutationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-adduptpropertytee"></a>

### 26. AddUPTPropertyTEE

**Section:** Section: Create Profile from UPT Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#ga6657114c7f25e838f5189e45571532d2) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_u_p_t_section.html#ga6657114c7f25e838f5189e45571532d2) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga6657114c7f25e838f5189e45571532d2)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AddUPTPropertyTEE(VARIANT FAR&nTableRef,VARIANT FAR&varSectionName,VARIANT FAR&varAX,VARIANT FAR&varD,VARIANT FAR&varWF,VARIANT FAR&varTF,VARIANT FAR&varTW,VARIANT FAR&varIZ,VARIANT FAR&varIY,VARIANT FAR&varIX,VARIANT FAR&varCY,VARIANT FAR&varAY,VARIANT FAR&varAZ)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableRef` | `VARIANT FAR&` | [in] | The existing table number ID. |
| `varSectionName` | `VARIANT FAR&` | [in] | UPT section string name. |
| `varAX` | `VARIANT FAR&` | [in] | Cross section area ( AX ). |
| `varD` | `VARIANT FAR&` | [in] | Depth of the section ( D ). |
| `varWF` | `VARIANT FAR&` | [in] | Width of the top flange ( WF ). |
| `varTF` | `VARIANT FAR&` | [in] | Thickness of top flange ( TF ). |
| `varTW` | `VARIANT FAR&` | [in] | Thickness of web ( TW ). |
| `varIZ` | `VARIANT FAR&` | [in] | Torsional constant ( IZ ). |
| `varIY` | `VARIANT FAR&` | [in] | Moment of inertia about local y-axis ( IY ). |
| `varIX` | `VARIANT FAR&` | [in] | Moment of inertia about local z-axis ( IX ). |
| `varCY` | `VARIANT FAR&` | [in] | Distance from back of web to center of gravity (C.G.) of the shape along the local y-axis. |
| `varAY` | `VARIANT FAR&` | [in] | Shear area in local Y-axis. If zero, shear deformation is ignored in the analysis ( AY ). |
| `varAZ` | `VARIANT FAR&` | [in] | Shear area in local Z-axis. If zero, shear deformation is ignored in the analysis ( AZ ). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-6032` | Unable to add section varSectionName in UPT nTableRef . |
| `-6045` | Section with same name exits in UPT nTableRef . |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AddUPTPropertyTEE(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** [UptTeeSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptTeeSection.cs#L53)

**Proposed managed entry:** `AddUPTPropertyTEE(UserTableRequest request)`<br>
**Proposed named result:** `UserTableSectionMutationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-adduptpropertypipe"></a>

### 27. AddUPTPropertyPIPE

**Section:** Section: Create Profile from UPT Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#ga927bbbce72db4a45e0006b936840ffea) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_u_p_t_section.html#ga927bbbce72db4a45e0006b936840ffea) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga927bbbce72db4a45e0006b936840ffea)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AddUPTPropertyPIPE(VARIANT FAR&nTableRef,VARIANT FAR&varSectionName,VARIANT FAR&varOD,VARIANT FAR&varID,VARIANT FAR&varAY,VARIANT FAR&varAZ)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableRef` | `VARIANT FAR&` | [in] | The existing table number ID. |
| `varSectionName` | `VARIANT FAR&` | [in] | UPT section string name. |
| `varOD` | `VARIANT FAR&` | [in] | Outer diameter ( OD ). |
| `varID` | `VARIANT FAR&` | [in] | Inner diameter ( ID ). |
| `varAY` | `VARIANT FAR&` | [in] | Shear area in local y-axis. If zero, shear deformation is ignored in the analysis ( AY ). |
| `varAZ` | `VARIANT FAR&` | [in] | Shear area in local z-axis. If zero, shear deformation is ignored in the analysis ( AZ ). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-6032` | Unable to add section varSectionName in UPT nTableRef . |
| `-6045` | Section with same name exits in UPT nTableRef . |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AddUPTPropertyPIPE(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** [UptPipeSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptPipeSection.cs#L56)

**Proposed managed entry:** `AddUPTPropertyPIPE(UserTableRequest request)`<br>
**Proposed named result:** `UserTableSectionMutationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-adduptpropertytube"></a>

### 28. AddUPTPropertyTUBE

**Section:** Section: Create Profile from UPT Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#ga3a2f714b28bce009a5df574409915b70) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_u_p_t_section.html#ga3a2f714b28bce009a5df574409915b70) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga3a2f714b28bce009a5df574409915b70)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AddUPTPropertyTUBE(VARIANT FAR&nTableRef,VARIANT FAR&varSectionName,VARIANT FAR&varAX,VARIANT FAR&varD,VARIANT FAR&varWF,VARIANT FAR&varTF,VARIANT FAR&varIZ,VARIANT FAR&varIY,VARIANT FAR&varIX,VARIANT FAR&varAY,VARIANT FAR&varAZ)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableRef` | `VARIANT FAR&` | [in] | The existing table number ID. |
| `varSectionName` | `VARIANT FAR&` | [in] | UPT section string name. |
| `varAX` | `VARIANT FAR&` | [in] | Cross section area ( AX ). |
| `varD` | `VARIANT FAR&` | [in] | Depth of the section ( D ). |
| `varWF` | `VARIANT FAR&` | [in] | Width of the top flange ( WF ). |
| `varTF` | `VARIANT FAR&` | [in] | Thickness of top flange ( TF ). |
| `varIZ` | `VARIANT FAR&` | [in] | Torsional constant ( Iz ). |
| `varIY` | `VARIANT FAR&` | [in] | Moment of inertia about local y-axis ( IY ). |
| `varIX` | `VARIANT FAR&` | [in] | Moment of inertia about local z-axis ( IX ). |
| `varAY` | `VARIANT FAR&` | [in] | Shear area in local y-axis. If zero, shear deformation is ignored in the analysis ( AY ). |
| `varAZ` | `VARIANT FAR&` | [in] | Shear area in local z-axis. If zero, shear deformation is ignored in the analysis ( AZ ). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-6032` | Unable to add section varSectionName in UPT nTableRef . |
| `-6045` | Section with same name exits in UPT nTableRef . |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AddUPTPropertyTUBE(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** [UptTubeSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptTubeSection.cs#L43)

**Proposed managed entry:** `AddUPTPropertyTUBE(UserTableRequest request)`<br>
**Proposed named result:** `UserTableSectionMutationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-adduptpropertygeneral"></a>

### 29. AddUPTPropertyGENERAL

**Section:** Section: Create Profile from UPT Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#gaa701fb0dc527b5cd73cb0c0d6616487f) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_u_p_t_section.html#gaa701fb0dc527b5cd73cb0c0d6616487f) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#gaa701fb0dc527b5cd73cb0c0d6616487f)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AddUPTPropertyGENERAL(VARIANT FAR&nTableRef,VARIANT FAR&varSectionName,VARIANT FAR&varAX,VARIANT FAR&varD,VARIANT FAR&varTD,VARIANT FAR&varB,VARIANT FAR&varTB,VARIANT FAR&varIZ,VARIANT FAR&varIY,VARIANT FAR&varIX,VARIANT FAR&varSZ,VARIANT FAR&varSY,VARIANT FAR&varAY,VARIANT FAR&varAZ,VARIANT FAR&varPZ,VARIANT FAR&varPY,VARIANT FAR&varHSS,VARIANT FAR&varDEE)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableRef` | `VARIANT FAR&` | [in] | The existing table number ID. |
| `varSectionName` | `VARIANT FAR&` | [in] | UPT section string name. |
| `varAX` | `VARIANT FAR&` | [in] | Cross section area ( AX ). |
| `varD` | `VARIANT FAR&` | [in] | Depth of the section ( D ). |
| `varTD` | `VARIANT FAR&` | [in] | Thickness associated with section element parallel to depth ( TD ). |
| `varB` | `VARIANT FAR&` | [in] | Width of the section ( B ). |
| `varTB` | `VARIANT FAR&` | [in] | Thickness associated with section element parallel to flange( TB ). |
| `varIZ` | `VARIANT FAR&` | [in] | Torsional constant ( IZ ). |
| `varIY` | `VARIANT FAR&` | [in] | Moment of inertia about local y-axis ( IY ). |
| `varIX` | `VARIANT FAR&` | [in] | Moment of inertia about local z-axis ( IX ). |
| `varSZ` | `VARIANT FAR&` | [in] | Section modulus about local Z-axis ( SZ ). |
| `varSY` | `VARIANT FAR&` | [in] | Section modulus about local Y-axis ( SY ). |
| `varAY` | `VARIANT FAR&` | [in] | Shear area for shear parallel to local Y-axis ( AY ). |
| `varAZ` | `VARIANT FAR&` | [in] | Shear area for shear parallel to local Z-axis ( AZ ). |
| `varPZ` | `VARIANT FAR&` | [in] | Plastic modulus about local Z-axis ( PZ ). |
| `varPY` | `VARIANT FAR&` | [in] | Plastic modulus about local Y-axis ( PY ). |
| `varHSS` | `VARIANT FAR&` | [in] | Warping constant for lateral torsional buckling calculations ( HSS ). |
| `varDEE` | `VARIANT FAR&` | [in] | Depth of web. For rolled sections, distance between fillets should be provided ( DEE ). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-6032` | Unable to add section varSectionName in UPT nTableRef . |
| `-6045` | Section with same name exits in UPT nTableRef . |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AddUPTPropertyGENERAL(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** [UptGeneralSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptGeneralSection.cs#L89)

**Proposed managed entry:** `AddUPTPropertyGENERAL(UserTableRequest request)`<br>
**Proposed named result:** `UserTableSectionMutationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-adduptpropertyisection"></a>

### 30. AddUPTPropertyISECTION

**Section:** Section: Create Profile from UPT Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#ga192963517f316eef4bf5cb2822d78424) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_u_p_t_section.html#ga192963517f316eef4bf5cb2822d78424) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga192963517f316eef4bf5cb2822d78424)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AddUPTPropertyISECTION(VARIANT FAR&nTableRef,VARIANT FAR&varSectionName,VARIANT FAR&varDWW,VARIANT FAR&varTWW,VARIANT FAR&varDWW1,VARIANT FAR&varBFF,VARIANT FAR&varTFF,VARIANT FAR&varBFF1,VARIANT FAR&varTFF1,VARIANT FAR&varAYF,VARIANT FAR&varAZF,VARIANT FAR&varXIF)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableRef` | `VARIANT FAR&` | [in] | The existing table number ID. |
| `varSectionName` | `VARIANT FAR&` | [in] | UPT section string name. |
| `varDWW` | `VARIANT FAR&` | [in] | Depth of section at start node( DWW ). |
| `varTWW` | `VARIANT FAR&` | [in] | Thickness of web( TWW ). |
| `varDWW1` | `VARIANT FAR&` | [in] | Depth of section at end node( DWW1 ). |
| `varBFF` | `VARIANT FAR&` | [in] | Width of top flange( BFF ). |
| `varTFF` | `VARIANT FAR&` | [in] | Thickness of top flange( TFF ). |
| `varBFF1` | `VARIANT FAR&` | [in] | Width of bottom flange( BFF1 ). |
| `varTFF1` | `VARIANT FAR&` | [in] | Thickness of bottom flange( TFF1 ). |
| `varAYF` | `VARIANT FAR&` | [in] | Shear area for shear parallel to Y-axis( AYF ). |
| `varAZF` | `VARIANT FAR&` | [in] | Shear area for shear parallel to Z-axis( AZF ). |
| `varXIF` | `VARIANT FAR&` | [in] | Torsional constant ( XIF ). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-6032` | Unable to add section varSectionName in UPT nTableRef . |
| `-6045` | Section with same name exits in UPT nTableRef . |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AddUPTPropertyISECTION(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** [UptISection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptISection.cs#L89)

**Proposed managed entry:** `AddUPTPropertyISECTION(UserTableRequest request)`<br>
**Proposed named result:** `UserTableSectionMutationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-adduptpropertyprismatic"></a>

### 31. AddUPTPropertyPRISMATIC

**Section:** Section: Create Profile from UPT Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#ga828f857560168eb9d6a9809950454017) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_u_p_t_section.html#ga828f857560168eb9d6a9809950454017) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga828f857560168eb9d6a9809950454017)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AddUPTPropertyPRISMATIC(VARIANT FAR&nTableRef,VARIANT FAR&varSectionName,VARIANT FAR&varAX,VARIANT FAR&varIZ,VARIANT FAR&varIY,VARIANT FAR&varIX,VARIANT FAR&varAY,VARIANT FAR&varAZ,VARIANT FAR&varYD,VARIANT FAR&varZD)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableRef` | `VARIANT FAR&` | [in] | The existing table number ID. |
| `varSectionName` | `VARIANT FAR&` | [in] | UPT section string name. |
| `varAX` | `VARIANT FAR&` | [in] | Cross section area ( AX ). |
| `varIZ` | `VARIANT FAR&` | [in] | Torsional constant ( IZ ). |
| `varIY` | `VARIANT FAR&` | [in] | Moment of inertia about local y-axis ( IY ). |
| `varIX` | `VARIANT FAR&` | [in] | Moment of inertia about local z-axis ( IX ). |
| `varAY` | `VARIANT FAR&` | [in] | Shear area for shear parallel to local Y-axis ( AY ). |
| `varAZ` | `VARIANT FAR&` | [in] | Shear area for shear parallel to local Z-axis ( AZ ). |
| `varYD` | `VARIANT FAR&` | [in] | Depth of the section in the direction of the local Y-axis ( YD ). |
| `varZD` | `VARIANT FAR&` | [in] | Depth of the section in the direction of the local Z-axis ( ZD ). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-6032` | Unable to add section varSectionName in UPT nTableRef . |
| `-6045` | Section with same name exits in UPT nTableRef . |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AddUPTPropertyPRISMATIC(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** [UptPrismaticSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptPrismaticSection.cs#L57)

**Proposed managed entry:** `AddUPTPropertyPRISMATIC(UserTableRequest request)`<br>
**Proposed named result:** `UserTableSectionMutationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createpropertyfromupttable"></a>

### 32. CreatePropertyFromUPTTable

**Section:** Section: Create Profile from UPT Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#ga4d6c2edb2612f542b9a1596221a9f784) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_u_p_t_section.html#ga4d6c2edb2612f542b9a1596221a9f784) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga4d6c2edb2612f542b9a1596221a9f784)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreatePropertyFromUPTTable(VARIANT FAR&nTableID,LPCTSTR strSectionName)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableID` | `VARIANT FAR&` | [in] | The existing table number ID. |
| `strSectionName` | `LPCTSTR` | [in] | UPT section string name. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | Section property number ID. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreatePropertyFromUPTTable(System.Object, System.String)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** [UptGeneralSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptGeneralSection.cs#L91), [UptISection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptISection.cs#L91), [UptPipeSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptPipeSection.cs#L58), [UptPrismaticSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptPrismaticSection.cs#L59), [UptSection.CreatePropertyFromUptTable](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Base/OSProperty/UptSection.cs#L39)

**Proposed managed entry:** `CreatePropertyFromUPTTable(UserTableRequest request)`<br>
**Proposed named result:** `UserTableCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getuptgeneralprofilepointscount"></a>

### 33. GetUptGeneralProfilePointsCount

**Section:** Section: Create Profile from UPT Property<br>
**Help availability:** 2024 absent · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_u_p_t_section.html#gac11402eeb7ce630b8a4b40b0a63cb83e) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#gac11402eeb7ce630b8a4b40b0a63cb83e)<br>
**Classification:** Newly documented in 2025<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetUptGeneralProfilePointsCount(const VARIANT FAR&varTableRef,const VARIANT FAR&varSectionName,VARIANT FAR&varCountOfOuter,VARIANT FAR&varCountOfInner)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varTableRef` | `const VARIANT FAR&` | [in] | The existing table number ID(Type: Long). |
| `varSectionName` | `const VARIANT FAR&` | [in] | UPT section string name(Type: String). |
| `varCountOfOuter` | `VARIANT FAR&` | [out] | Count of outer profile points(Type: Long). |
| `varCountOfInner` | `VARIANT FAR&` | [out] | Count of inner profile points(Reserved, not be used now)(Type: Long). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `True` | Get profile points count successful. |
| `False` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetUptGeneralProfilePointsCount(System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetUserTableGeneralProfilePointCounts(int tableId, string sectionName)`<br>
**Proposed named result:** `UserTablePointCountsResult`

2025+; outer count plus reserved inner count, do not advertise holes/inner profiles as supported.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getuptgeneralprofileboundarypoints"></a>

### 34. GetUptGeneralProfileBoundaryPoints

**Section:** Section: Create Profile from UPT Property<br>
**Help availability:** 2024 absent · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_u_p_t_section.html#ga9f135f3a5b84d9041a60bf5c276699c2) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga9f135f3a5b84d9041a60bf5c276699c2)<br>
**Classification:** Newly documented in 2025<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetUptGeneralProfileBoundaryPoints(const VARIANT FAR&varTableRef,const VARIANT FAR&strSectionName,const VARIANT FAR&varIsInner,VARIANT FAR&varZP,VARIANT FAR&varYP)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varTableRef` | `const VARIANT FAR&` | [in] | The existing table number ID(Type: Long). |
| `strSectionName` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varIsInner` | `const VARIANT FAR&` | [in] | (Reserved for inner points, set it to false)(Type: Boolean). |
| `varZP` | `VARIANT FAR&` | [out] | Profile Points coordinate in Z(Type: double array). |
| `varYP` | `VARIANT FAR&` | [out] | Profile Points coordinate in Y(Type: double array). |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `varSectionName` | [in] | UPT section string name(Type: String). |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | Profile Points Count. |
| `0` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetUptGeneralProfileBoundaryPoints(System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetUserTableGeneralBoundary(int tableId, string sectionName)`<br>
**Proposed named result:** `UserTableBoundaryResult`

2025+; varIsInner must remain false internally. Own paired Z/Y arrays and validate returned count; no use of old reserved profile APIs.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getuptgeneralstresslocationpoints"></a>

### 35. GetUptGeneralStressLocationPoints

**Section:** Section: Create Profile from UPT Property<br>
**Help availability:** 2024 absent · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_u_p_t_section.html#ga206fc2be959408948d3e81aa6751ed7a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga206fc2be959408948d3e81aa6751ed7a)<br>
**Classification:** Newly documented in 2025<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetUptGeneralStressLocationPoints(const VARIANT FAR&varTableRef,const VARIANT FAR&strSectionName,VARIANT FAR&varZP,VARIANT FAR&varYP)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varTableRef` | `const VARIANT FAR&` | [in] | The existing table number ID(Type: Long). |
| `strSectionName` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varZP` | `VARIANT FAR&` | [out] | Stress Location coordinate in Z(Type: double array of size 4). |
| `varYP` | `VARIANT FAR&` | [out] | Stress Location coordinate in Y(Type: double array of size 4). |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `varSectionName` | [in] | UPT section string name(Type: String). |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | Stress Location Count. |
| `0` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetUptGeneralStressLocationPoints(System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetUserTableGeneralStressPoints(int tableId, string sectionName)`<br>
**Proposed named result:** `UserTableStressPointsResult`

2025+; two arrays of size four, returned count must qualify both; name local section axes explicitly.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-assignbetaangle"></a>

### 36. AssignBetaAngle

**Section:** Section: Beta Angle Operation Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_beta.html#gac25ed74556a088b204e59721179f1660) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_beta.html#gac25ed74556a088b204e59721179f1660) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_beta.html#gac25ed74556a088b204e59721179f1660)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P5 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AssignBetaAngle(const VARIANT FAR&varnBeamNo,const VARIANT FAR&varfBetaAngle)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID(s) of VARIANT array. |
| `varfBetaAngle` | `const VARIANT FAR&` | [in] | The beta angle in degrees. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | OK. |
| `0` | General Error |
| `0` | 1 dimensional array of long expected. |
| `0` | Library Error: Unable to assign BETA angle. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AssignBetaAngle(System.Object, System.Object)`

**Main adapter targets:** [AssignBetaAnglesToBeams](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1575), [AssignBetaAnglesToBeams](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1700)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `AssignBetaAngle(PropertyAssignmentRequest request)`<br>
**Proposed named result:** `PropertyAssignmentResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getbetaangle"></a>

### 37. GetBetaAngle

**Section:** Section: Beta Angle Operation Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_beta.html#ga7dbd2b865f74ec34570f04fad356f42b) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_beta.html#ga7dbd2b865f74ec34570f04fad356f42b) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_beta.html#ga7dbd2b865f74ec34570f04fad356f42b)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<double> GetBetaAngle(int memberId)`.<br>
**Plan:** P2 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetBetaAngle(const VARIANT FAR&varnBeamNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | Beta angle. |
| `-3001` | Cannot find member varnBeamNo . |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetBetaAngle(System.Object)`

**Main adapter targets:** [PopulateBetaAngles](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L631), [GetBetaAngle](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L639)<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<double> GetBetaAngle(int memberId)`<br>
**Implemented result:** `PropertyQueryResult<double>`

Distinguish a valid zero/negative angle from documented member-not-found code -3001; verify angle units.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getsectionpropertyvalues"></a>

**Implemented behavior and alternatives:** Use to observe orientation about the analytical member axis; offsets and releases do not represent beta rotation. Returns the finite signed native angle unchanged, including zero. Native -3001 is NotFound. No degrees/radians conversion or managed local-axis reconstruction is performed.

**Current native call sites:** [GetBetaAngle](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L46). See [usage and qualification](../Property-Porting.md#getbetaangle).

### 38. GetSectionPropertyValues

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga91afb41d03b9b4df33f3b47b36de3938) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga91afb41d03b9b4df33f3b47b36de3938) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga91afb41d03b9b4df33f3b47b36de3938)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetSectionPropertyValues(const VARIANT FAR&varProfRefNo,VARIANT FAR&varfWidth,VARIANT FAR&varfDepth,VARIANT FAR&varfAx,VARIANT FAR&varfAy,VARIANT FAR&varfAz,VARIANT FAR&varfIx,VARIANT FAR&varfIy,VARIANT FAR&varfIz,VARIANT FAR&varfTf,VARIANT FAR&varfTw)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varProfRefNo` | `const VARIANT FAR&` | [in] | Assign Profile Type: Prof Type Value AssignAngle 0 AssignDoubleAngle 1 AssignBeam 2 AssignColumn 3 AssignChannel 4 |
| `varfWidth` | `VARIANT FAR&` | [out] | Width of the section ( WID ). |
| `varfDepth` | `VARIANT FAR&` | [out] | Depth of the section ( DEP ). |
| `varfAx` | `VARIANT FAR&` | [out] | Cross section area ( Ax ). |
| `varfAy` | `VARIANT FAR&` | [out] | Shear area in local y-axis. If zero, shear deformation is ignored in the analysis ( Ay ). |
| `varfAz` | `VARIANT FAR&` | [out] | Shear area in local z-axis. If zero, shear deformation is ignored in the analysis ( Az ). |
| `varfIx` | `VARIANT FAR&` | [out] | Moment of inertia about local z-axis ( Ix ). |
| `varfIy` | `VARIANT FAR&` | [out] | Moment of inertia about local y-axis ( Iy ). |
| `varfIz` | `VARIANT FAR&` | [out] | Torsional constant ( Iz ). |
| `varfTf` | `VARIANT FAR&` | [out] | Thickness of top flange ( Tf ). |
| `varfTw` | `VARIANT FAR&` | [out] | Thickness of web ( Tw ). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-3001` | Cannot find member nMemberNo . |
| `-6022` | No property is attached to the member/element. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetSectionPropertyValues(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetSectionPropertyValues(SectionQuery request)`<br>
**Proposed named result:** `SectionDefinitionResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getsectionpropertyassignedbeamcount"></a>

### 39. GetSectionPropertyAssignedBeamCount

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga008bbb46f75923080b95eb63962f1acd) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga008bbb46f75923080b95eb63962f1acd) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga008bbb46f75923080b95eb63962f1acd)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetSectionPropertyAssignedBeamCount(const VARIANT FAR&varProfRefNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varProfRefNo` | `const VARIANT FAR&` | [in] | Assign Profile Type: Prof Type Value AssignAngle 0 AssignDoubleAngle 1 AssignBeam 2 AssignColumn 3 AssignChannel 4 |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The section table number. |
| `-3001` | Cannot find member varnBeamNo . |
| `-6004` | Section not found in profile database. |
| `-6022` | No property is attached to the member/element. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetSectionPropertyAssignedBeamCount(System.Object)`

**Main adapter targets:** [GetUnusedSectionsIds](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L55), [GetSectionByPropertyIdWithAssignedBeamsList](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L983), [GetSectionByPropertyIdWithAssignedBeamsList](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1001), [GetSectionPropertyAssignedBeams](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1054), [GetBeamProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1109)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetSectionPropertyAssignedBeamCount(SectionQuery request)`<br>
**Proposed named result:** `PropertyCountResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getsectionpropertyassignedbeamlist"></a>

### 40. GetSectionPropertyAssignedBeamList

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga4cb2980c099d339e6f0d5532448f1bb1) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga4cb2980c099d339e6f0d5532448f1bb1) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga4cb2980c099d339e6f0d5532448f1bb1)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetSectionPropertyAssignedBeamList(const VARIANT FAR&varProfRefNo,VARIANT FAR&nBeamList)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varProfRefNo` | `const VARIANT FAR&` | [in] | Assign Profile Type: Prof Type Value AssignAngle 0 AssignDoubleAngle 1 AssignBeam 2 AssignColumn 3 AssignChannel 4 |
| `nBeamList` | `VARIANT FAR&` | [out] | List of beam. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The section table number. |
| `-3001` | Cannot find member varnBeamNo . |
| `-6004` | Section not found in profile database. |
| `-6022` | No property is attached to the member/element. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetSectionPropertyAssignedBeamList(System.Object, System.Object ByRef)`

**Main adapter targets:** [GetSectionPropertyAssignedBeams](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1056), [GetBeamProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1113)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetSectionPropertyAssignedBeamList(SectionQuery request)`<br>
**Proposed named result:** `PropertyIdentifierListResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getbeamsectionpropertyrefno"></a>

### 41. GetBeamSectionPropertyRefNo

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga3c67b414c91c210bf5cc4b70a2f3be74) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga3c67b414c91c210bf5cc4b70a2f3be74) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga3c67b414c91c210bf5cc4b70a2f3be74)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetBeamSectionPropertyRefNo(const VARIANT FAR&varnBeamNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID (Type: Int). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `Section` | property ref number assigned to the specified beam. Zero if not found. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetBeamSectionPropertyRefNo(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetBeamSectionPropertyRefNo(SectionQuery request)`<br>
**Proposed named result:** `PropertyReferenceResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getbeamsectionpropertyvaluesex"></a>

### 42. GetBeamSectionPropertyValuesEx

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga8d1dc394d29fa932cd97c281ab619ab6) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga8d1dc394d29fa932cd97c281ab619ab6) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga8d1dc394d29fa932cd97c281ab619ab6)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetBeamSectionPropertyValuesEx(const VARIANT FAR&varnBeamNo,VARIANT FAR&varPropType,VARIANT FAR&varProperties)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID (Type: Int/Long). |
| `varPropType` | `VARIANT FAR&` | [out] | Number referring to the below table(Type: Long), refer to OSPropertyUI::GetSectionPropertyValuesEx . |
| `varProperties` | `VARIANT FAR&` | [out] | A double VARIANT array for section property parameters (Type:Array size of 24 doubles). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `FALSE` | Get the property Values generate Error. |
| `TRUE` | Get the property Values Successful. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetBeamSectionPropertyValuesEx(System.Object, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetBeamSectionPropertyValuesEx(SectionQuery request)`<br>
**Proposed named result:** `SectionDefinitionResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getsectionpropertyvaluesex"></a>

### 43. GetSectionPropertyValuesEx

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga0497f1f458d11eb783f465a85dc6ad27) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga0497f1f458d11eb783f465a85dc6ad27) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga0497f1f458d11eb783f465a85dc6ad27)<br>
**Classification:** Version policy candidate<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetSectionPropertyValuesEx(const VARIANT FAR&varProfRefNo,VARIANT FAR&varPropType,VARIANT FAR&varPropValues)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varProfRefNo` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varPropType` | `VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varPropValues` | `VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `propNo` | [in] | Section property ID. |
| `propType` | [out] | Number referring to the below table. |
| `propValues` | [out] | A double VARIANT array for section property parameters (get array size by calling the API OSPropertyUI::GetCountofSectionPropertyValuesEx . |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | OK. |
| `0` | Error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetSectionPropertyValuesEx(System.Object, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** [GetSectionBy](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1019), [GetSectionPropertyValues](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1131)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetSectionDefinition(int propertyId)`<br>
**Proposed named result:** `SectionDefinitionResult`

Profile-aware semantic mapping of nine changed section rows; use native count for safe allocation, retain raw codes/unused tails and reject unverified layouts.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

**Changed section layouts:**

| Section / code | 2024 documented values | 2025 / 2026 documented values |
| --- | --- | --- |
| BEAM CM / 612 | `Ax D Bf Tf Tw Iz Iy Ix CT FC CW` | `Ax D Bf Tf Tw Iz Iy Ix CT FC CW CD` |
| CHANNEL ST / 630 | `Ax D Bf Tf Tw Iz Iy Ix CT` | `Ax D Bf Tf Tw Iz Iy Ix` |
| PLATE STRIP / 666 | `Ax D B T Iz Iy Ix` | `Ax D B Iz Iy Ix` |
| ANGLE COLD ST / 644 | `Ax D B T Iz Iy Ix R LIP Ay Az` | `Ax D B T Iz Iy Ix R Ay Az` |
| CHANNEL COLD ST / 634 | `Ax Bf T R Iz Iy Ix LIP Ay Az` | `Ax D Bf T R Iz Iy Ix Ay Az` |
| CHANNEL COLD ST WITH LIPS / 635 | `Ax Bf T R Iz Iy Ix LIP Ay Az` | `Ax D Bf T R Iz Iy Ix LIP Ay Az` |
| ZEES COLD ST / 662 | `Ax D B T LIP <LIP R Iz Iy Ix Ay Az` | `Ax D B T R Iz Iy Ix Ay Az` |
| ZEES COLD ST WITH LIPS / 663 | `Ax D B T LIP <LIP R Iz Iy Ix Ay Az` | `Ax D B T LIP LIP_Angle R Iz Iy Ix Ay Az` |
| SOLID ROUND / 668 | `Ax OD Tw Iz Iy Ix` | `Ax OD Tw Iz Iy Ix Z` |

These are documentation differences, not certified native buffer changes. The count API determines allocation; semantic field layout requires acquired-version/type verification.

<a id="property-getsectiontableno"></a>

### 44. GetSectionTableNo

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga9c3b455750b4ed8046d78fecb0b8cbc7) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga9c3b455750b4ed8046d78fecb0b8cbc7) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga9c3b455750b4ed8046d78fecb0b8cbc7)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetSectionTableNo(const VARIANT FAR&varnBeamNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The section table number. |
| `-3001` | Cannot find member varnBeamNo . |
| `-6004` | Section not found in profile database. |
| `-6022` | No property is attached to the member/element. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetSectionTableNo(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetSectionTableNo(SectionQuery request)`<br>
**Proposed named result:** `PropertyReferenceResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getbeamsectionpropertytypeno"></a>

### 45. GetBeamSectionPropertyTypeNo

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga37be18e3c326761c52fb42fa0773a97c) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga37be18e3c326761c52fb42fa0773a97c) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga37be18e3c326761c52fb42fa0773a97c)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetBeamSectionPropertyTypeNo(const VARIANT FAR&varnBeamNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The section property type number. |
| `0` | Error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetBeamSectionPropertyTypeNo(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetBeamSectionPropertyTypeNo(SectionQuery request)`<br>
**Proposed named result:** `SpecificationQueryResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getbeamsectionname"></a>

### 46. GetBeamSectionName

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga398cd10fa2ef3710d2807797445be4bb) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga398cd10fa2ef3710d2807797445be4bb) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga398cd10fa2ef3710d2807797445be4bb)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetBeamSectionName(const VARIANT FAR&varnBeamNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID. |


**Native return description:** The section string name. Refer the table below for probable section names :

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetBeamSectionName(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetBeamSectionName(SectionQuery request)`<br>
**Proposed named result:** `PropertyNameResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getbeamsectiondisplayname"></a>

### 47. GetBeamSectionDisplayName

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga967af878ba279f8d3a2fe58a0d209a14) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga967af878ba279f8d3a2fe58a0d209a14) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga967af878ba279f8d3a2fe58a0d209a14)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetBeamSectionDisplayName(const VARIANT FAR&varnBeamNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID. |


**Native return description:** The section string name. Empty string if the specified beam is not found or property is not assigned to it. Refer the table below for probable section names :

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetBeamSectionDisplayName(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetBeamSectionDisplayName(SectionQuery request)`<br>
**Proposed named result:** `PropertyNameResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getcountrytableno"></a>

### 48. GetCountryTableNo

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga5ed5bfb3b3caf8dd8d395858eb6bbdec) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga5ed5bfb3b3caf8dd8d395858eb6bbdec) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga5ed5bfb3b3caf8dd8d395858eb6bbdec)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetCountryTableNo(const VARIANT FAR&varnBeamNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The country CODE. |
| `-3001` | Cannot find member varnBeamNo . |
| `-6022` | No property is attached to the member/element. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetCountryTableNo(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetCountryTableNo(SpecificationQuery request)`<br>
**Proposed named result:** `PropertyCountResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getsectionpropertycount"></a>

### 49. GetSectionPropertyCount

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#gabd01f29a4421f6fe05c2138cda657595) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#gabd01f29a4421f6fe05c2138cda657595) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gabd01f29a4421f6fe05c2138cda657595)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetSectionPropertyCount()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return description:** The total number of different sectional properties.

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetSectionPropertyCount()`

**Main adapter targets:** [GetAllSections](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L598), [GetSectionPropertyList](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1045)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetSectionPropertyCount()`<br>
**Proposed named result:** `PropertyCountResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getsectionpropertylist"></a>

### 50. GetSectionPropertyList

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga58470c040e0adc4607e00e054b4c6daa) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga58470c040e0adc4607e00e054b4c6daa) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga58470c040e0adc4607e00e054b4c6daa)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetSectionPropertyList(VARIANT FAR&nPropList)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nPropList` | `VARIANT FAR&` | [out] | List of Section Property refrence IDs (Type: Long Array), for list size refer OSPropertyUI::GetSectionPropertyCount . |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `FALSE` | Get Section Property refrence IDs generate Error. |
| `TRUE` | Get Section Property refrence IDs Successful. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetSectionPropertyList(System.Object ByRef)`

**Main adapter targets:** [GetAllSections](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L602), [GetSectionPropertyList](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1047)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetSectionPropertyList(SectionQuery request)`<br>
**Proposed named result:** `PropertyIdentifierListResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getsectionpropertyname"></a>

### 51. GetSectionPropertyName

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga22f293fd885cd2f6a2102d5a9f9e691d) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga22f293fd885cd2f6a2102d5a9f9e691d) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga22f293fd885cd2f6a2102d5a9f9e691d)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetSectionPropertyName(const VARIANT FAR&varSecRefNo,VARIANT FAR&varstrName)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varSecRefNo` | `const VARIANT FAR&` | [in] | The assigned section property ID. |
| `varstrName` | `VARIANT FAR&` | [out] | Identification title of material. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |
| `-6025` | No property is defined in the model. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetSectionPropertyName(System.Object, System.Object ByRef)`

**Main adapter targets:** [GetSectionPropertyName](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1080)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetSectionPropertyName(SectionQuery request)`<br>
**Proposed named result:** `SectionNameResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getsectionpropertytype"></a>

### 52. GetSectionPropertyType

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga70e659ae3da254327aa62b82914dee1f) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga70e659ae3da254327aa62b82914dee1f) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga70e659ae3da254327aa62b82914dee1f)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetSectionPropertyType(const VARIANT FAR&varSecRefNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varSecRefNo` | `const VARIANT FAR&` | [in] | The assigned section property ID. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | Number referring to Section Type Code table. |
| `-6025` | No property is defined in the model. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetSectionPropertyType(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetSectionPropertyType(SectionQuery request)`<br>
**Proposed named result:** `SpecificationQueryResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getsectionpropertycountry"></a>

### 53. GetSectionPropertyCountry

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga7ecdf0e76eeebbeb8cb5d225ab319084) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga7ecdf0e76eeebbeb8cb5d225ab319084) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga7ecdf0e76eeebbeb8cb5d225ab319084)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetSectionPropertyCountry(const VARIANT FAR&varSecRefNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varSecRefNo` | `const VARIANT FAR&` | [in] | The assigned section property ID(Type: long/Integer). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | Country CODE(Type: long/Integer), please reference OSPropertyUI::CreateBeamPropertyFromTable |
| `-6025` | No property is defined in the model. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetSectionPropertyCountry(System.Object)`

**Main adapter targets:** [GetSectionBy](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1022)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetSectionPropertyCountry(SectionQuery request)`<br>
**Proposed named result:** `PropertyCountResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getshapecode"></a>

### 54. GetShapeCode

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#gae13c9d4c20e1a0a487728cbdcdf73024) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#gae13c9d4c20e1a0a487728cbdcdf73024) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gae13c9d4c20e1a0a487728cbdcdf73024)<br>
**Classification:** Version policy candidate<br>
**Current typed coverage:** None.<br>
**Plan:** P9 — Pending; contract verification before implementation

```cpp
long OSPropertyUI::GetShapeCode(const VARIANT FAR&Country,LPCTSTR SectionName)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `Country` | `const VARIANT FAR&` | [in] | Country ID(Type: long/Integer), please reference OSPropertyUI::CreateBeamPropertyFromTable . |
| `SectionName` | `LPCTSTR` | [in] | Section Name(Type: String). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | Shape Code(Type: long/Integer) |
| `-1` | Get the Shape Code generate Error. |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 GetShapeCode(System.Object, System.String)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetSectionShape(SectionCatalogQuery query)`<br>
**Proposed named result:** `SectionShapeResult`

Country-specific shape IDs; 2025 omits some codes and reserves RCeco. Preserve unknown raw codes; help table omission does not prove native removal.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

**Country/shape table changes:**

| Country | 2024 | 2025 / 2026 |
| --- | --- | --- |
| American | Codes 17–22 list WT/MT/ST and double-angle/channel forms. | Those entries are omitted; codes 23/24 remain. |
| Australian | Codes 7–9 list Rhs/Shs/Chs. | Those entries are omitted. |
| British | Codes 9/10 list 2L/2C. | Those entries are omitted. |
| RCecoColdFormed | Listed without reservation. | Country row and shape descriptions are marked Reserved. |

Absence from this table does not establish that the acquired native database rejects the corresponding section. Preserve country-qualified codes and unknown values.

<a id="property-getrecordforsection"></a>

### 55. GetRecordForSection

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga75d00082ceba112a311334739850896b) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga75d00082ceba112a311334739850896b) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga75d00082ceba112a311334739850896b)<br>
**Classification:** Version policy candidate<br>
**Current typed coverage:** None.<br>
**Plan:** P9 — Pending; contract verification before implementation

```cpp
long OSPropertyUI::GetRecordForSection(const VARIANT FAR&Country,LPCTSTR SectionName)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `Country` | `const VARIANT FAR&` | [in] | Country ID(Type: long/Integer), please reference OSPropertyUI::CreateBeamPropertyFromTable . |
| `SectionName` | `LPCTSTR` | [in] | Section Name(Type: String). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | Record No For specific Section(Type: long/Integer) |
| `-1` | Get the Record For the specific Section generate Error. |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 GetRecordForSection(System.Object, System.String)`

**Main adapter targets:** [GetSectionBy](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1029)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetSectionRecord(SectionCatalogQuery query)`<br>
**Proposed named result:** `SectionRecordResult`

2024 describes 1-based index, newer help database record number; do not apply +1/-1 conversions or conflate with property ID.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

**2024 versus 2025/2026:**

| Year | Documented contract |
| --- | --- |
| 2024 | Get the Record Index (start from 1) in table with specific Country ID and specific Section Name. Parameters [in] Country Country ID(Type: long/Integer), please reference OSPropertyUI::CreateBeamPropertyFromTable . [in] SectionName Section Name(Type: String). Return values <Val> Record Index For specific Section(Type: long/Integer) -1 Get the Record For the specific Section generate Error. C++ Syntax VBA Syntax |
| 2025 / 2026 | Get the Record No (Record No in Section database) in table with specific Country ID and specific Section Name. Parameters [in] Country Country ID(Type: long/Integer), please reference OSPropertyUI::CreateBeamPropertyFromTable . [in] SectionName Section Name(Type: String). Return values <Val> Record No For specific Section(Type: long/Integer) -1 Get the Record For the specific Section generate Error. C++ Syntax VBA Syntax |

<a id="property-getplatethickness"></a>

### 56. GetPlateThickness

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga2fa3663eee2b4f19cfd109177de444fe) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga2fa3663eee2b4f19cfd109177de444fe) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga2fa3663eee2b4f19cfd109177de444fe)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetPlateThickness(const VARIANT FAR&varnPlateNo,VARIANT FAR&varfThkArray)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnPlateNo` | `const VARIANT FAR&` | [in] | The plate number ID. |
| `varfThkArray` | `VARIANT FAR&` | [out] | VARIANT array of the thickness for all nodes. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-107` | Array of double expected. |
| `-108` | Array size is smaller than expected. |
| `-4006` | Invalid member number. |
| `-6022` | No property is attached to the member/element. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetPlateThickness(System.Object, System.Object ByRef)`

**Main adapter targets:** [GetPlateThicknessByPlateId](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1278)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetPlateThickness(ThicknessQuery request)`<br>
**Proposed named result:** `PlateThicknessQueryResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getthicknesspropertycount"></a>

### 57. GetThicknessPropertyCount

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga1b82bcd70a84320fa1c5f153255426f8) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga1b82bcd70a84320fa1c5f153255426f8) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga1b82bcd70a84320fa1c5f153255426f8)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetThicknessPropertyCount()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return description:** Thickness Property Count.

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetThicknessPropertyCount()`

**Main adapter targets:** [GetAllPlateElementThicknesses](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1246), [GetThicknessPropertyList](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1285)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetThicknessPropertyCount()`<br>
**Proposed named result:** `PropertyCountResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getthicknesspropertylist"></a>

### 58. GetThicknessPropertyList

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga080e308745d58857189ab422b9b6102e) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga080e308745d58857189ab422b9b6102e) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga080e308745d58857189ab422b9b6102e)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetThicknessPropertyList(VARIANT FAR&nPropList)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nPropList` | `VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `Thickness` | [out] | Property ID list(Type: Long Array), for size refer to OSPropertyUI::GetThicknessPropertyCount . |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE` | Get Thickness Property list Successful. |
| `FALSE` | Get Thickness Property list Generate Error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetThicknessPropertyList(System.Object ByRef)`

**Main adapter targets:** [GetAllPlateElementThicknesses](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1249), [GetThicknessPropertyList](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1287)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetThicknessPropertyList(ThicknessQuery request)`<br>
**Proposed named result:** `PropertyIdentifierListResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getthicknesspropertyassignedplatecount"></a>

### 59. GetThicknessPropertyAssignedPlateCount

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga6b405e7c6b5a5a294277ffc910dbe42e) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga6b405e7c6b5a5a294277ffc910dbe42e) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga6b405e7c6b5a5a294277ffc910dbe42e)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetThicknessPropertyAssignedPlateCount(const VARIANT FAR&varProfRefNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varProfRefNo` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `Thickness` | [in] | Property reference ID(Type: Long). |

**Native return description:** Count of plates which are assigned with the specified Thickness Property reference ID(Type:Long).

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetThicknessPropertyAssignedPlateCount(System.Object)`

**Main adapter targets:** [GetThicknessPropertyAssignedPlates](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1294)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetThicknessPropertyAssignedPlateCount(ThicknessQuery request)`<br>
**Proposed named result:** `PropertyCountResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getthicknesspropertyassignedplatelist"></a>

### 60. GetThicknessPropertyAssignedPlateList

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#gacec0dbd476ed855f50796f7bb4166d2d) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#gacec0dbd476ed855f50796f7bb4166d2d) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gacec0dbd476ed855f50796f7bb4166d2d)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetThicknessPropertyAssignedPlateList(const VARIANT FAR&varProfRefNo,VARIANT FAR&nPlateList)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varProfRefNo` | `const VARIANT FAR&` | [in] | the specific Thickness Property reference ID(Type: Long). |
| `nPlateList` | `VARIANT FAR&` | [out] | Plate No list(Type: Long Array), for size refer to OSPropertyUI::GetThicknessPropertyAssignedPlateCount . |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE` | Get plate No list Successful. |
| `FALSE` | Get plate No list Generate Error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetThicknessPropertyAssignedPlateList(System.Object, System.Object ByRef)`

**Main adapter targets:** [GetThicknessPropertyAssignedPlates](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1296)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetThicknessPropertyAssignedPlateList(ThicknessQuery request)`<br>
**Proposed named result:** `PropertyIdentifierListResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getthicknesspropertyvalues"></a>

### 61. GetThicknessPropertyValues

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#gafd3b17a65417534a41a8f1497e4517c6) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#gafd3b17a65417534a41a8f1497e4517c6) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gafd3b17a65417534a41a8f1497e4517c6)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetThicknessPropertyValues(const VARIANT FAR&varProfRefNo,VARIANT FAR&dThkList)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varProfRefNo` | `const VARIANT FAR&` | [in] | the specific Thickness Property refernce ID(Type: Long). |
| `dThkList` | `VARIANT FAR&` | [out] | Thickness value list(Type: Double Array), array of size 4. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | Get Thickness Property Value Generate Error. |
| `4` | Get Thickness Property Value Successful. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetThicknessPropertyValues(System.Object, System.Object ByRef)`

**Main adapter targets:** [GetPlateThicknessByThicknessPropertyId](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1270)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetThicknessDefinition(int thicknessPropertyId)`<br>
**Proposed named result:** `PlateThicknessQueryResult`

Native success returns 4, not Boolean 1; preserve four incidence thicknesses in order and confirm triangular conventions.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removepropertyfromplate"></a>

### 62. RemovePropertyFromPlate

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga99c86bf4b01408c684ff009e0e00079b) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga99c86bf4b01408c684ff009e0e00079b) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga99c86bf4b01408c684ff009e0e00079b)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemovePropertyFromPlate(const VARIANT FAR&nPlateNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nPlateNo` | `const VARIANT FAR&` | [in] | Plate ID(Type: Long). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `FALSE` | Remove Thickness Property Generate Error. |
| `TRUE` | Remove Thickness Property Successful. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemovePropertyFromPlate(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemovePropertyFromPlate(MemberSpecificationRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removematerialfromplate"></a>

### 63. RemoveMaterialFromPlate

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#gaa8489347df16c3ea9aea52ca421e4e53) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#gaa8489347df16c3ea9aea52ca421e4e53) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gaa8489347df16c3ea9aea52ca421e4e53)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemoveMaterialFromPlate(const VARIANT FAR&nPlateNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nPlateNo` | `const VARIANT FAR&` | [in] | Plates ID(Type: Long Array). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `FALSE` | Remove Material Generate Error. |
| `TRUE` | Remove Material Successful. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemoveMaterialFromPlate(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemoveMaterialFromPlate(MaterialDefinitionRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removematerialfromsolid"></a>

### 64. RemoveMaterialFromSolid

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#gaefebf74d842cbed7e39736e94abb8ea4) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#gaefebf74d842cbed7e39736e94abb8ea4) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gaefebf74d842cbed7e39736e94abb8ea4)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemoveMaterialFromSolid(const VARIANT FAR&nSolidNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nSolidNo` | `const VARIANT FAR&` | [in] | Solids ID(Type: Long Array). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `FALSE` | Remove Material Generate Error. |
| `TRUE` | Remove Material Successful. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemoveMaterialFromSolid(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemoveMaterialFromSolid(MaterialDefinitionRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getisotropicmaterialassignedplatelist"></a>

### 65. GetIsotropicMaterialAssignedPlateList

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#gaf6b9b9403e5b7e0ae90ba797491f17af) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#gaf6b9b9403e5b7e0ae90ba797491f17af) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gaf6b9b9403e5b7e0ae90ba797491f17af)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetIsotropicMaterialAssignedPlateList(const VARIANT&strMaterialName,VARIANT FAR&nPlateList)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strMaterialName` | `const VARIANT&` | [in] | Material Name(Type: String). |
| `nPlateList` | `VARIANT FAR&` | [out] | Plate No list(Type: Long Array), for size refer to OSPropertyUI::GetIsotropicMaterialAssignedPlateCount . |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE` | Get plate No list Successful. |
| `FALSE` | Get plate No list Generate Error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetIsotropicMaterialAssignedPlateList(System.Object, System.Object ByRef)`

**Main adapter targets:** [GetIsotropicMaterialAssignedPlates](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L411)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetIsotropicMaterialAssignedPlateList(MaterialQuery request)`<br>
**Proposed named result:** `PropertyIdentifierListResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getcountofsectionpropertyvaluesex"></a>

### 66. GetCountofSectionPropertyValuesEx

**Section:** Section: Get and Remove General Section Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html#ga2e1e4793b70cdd102f55d878b92685cc) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html#ga2e1e4793b70cdd102f55d878b92685cc) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga2e1e4793b70cdd102f55d878b92685cc)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P4 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetCountofSectionPropertyValuesEx()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The total count of Section Property values. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetCountofSectionPropertyValuesEx()`

**Main adapter targets:** [GetSectionBy](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1015), [GetSectionPropertyValues](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1127)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetCountofSectionPropertyValuesEx()`<br>
**Proposed named result:** `PropertyCountResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-findupttable"></a>

### 67. FindUPTTable

**Section:** Section: Get and Remove UPT Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_u_p_t_section.html#gac238a7c187f6e53ca29b25f2331b20a3) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_u_p_t_section.html#gac238a7c187f6e53ca29b25f2331b20a3) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#gac238a7c187f6e53ca29b25f2331b20a3)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
long OSPropertyUI::FindUPTTable(VARIANT FAR&varTableType)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varTableType` | `VARIANT FAR&` | [in] | Table type number: No. Table Type 1 scUserTableWideFlangeTitle 2 scUserTableChannelTitle 3 scUserTableAngleTitle 4 scUserTableDoubleAngleTitle 5 scUserTableTeeTitle 6 scUserTablePipeTitle 7 scUserTableTubeTitle 8 scUserTableGeneralTitle 9 scUserTableIsectionTitle 10 scUserTablePrismaticTitle |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | Table number ID. |
| `-6036` | Cannot find UPT. Unknown table type varTableType specified. |

**Installed 2025 / 2026 IOSPropertyUI:** No matching name in either inspected interface; native availability must be established independently.

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `FindUserTable(UserTableSectionKind sectionKind)`<br>
**Proposed named result:** `PropertyReferenceResult`

Documented but absent from both inspected interfaces. Verify acquired native member; do not expose helper pointers or guess fallback.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createupttableex"></a>

### 68. CreateUPTTableEx

**Section:** Section: Get and Remove UPT Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_u_p_t_section.html#ga0fb2e9ebe08ff961ce2e620616ceefa0) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_u_p_t_section.html#ga0fb2e9ebe08ff961ce2e620616ceefa0) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga0fb2e9ebe08ff961ce2e620616ceefa0)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateUPTTableEx(VARIANT FAR&nTableNo,VARIANT FAR&nTableType)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableNo` | `VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `nTableType` | `VARIANT FAR&` | [in] | Type of the table, Ref the function OSPropertyUI::CreateUPTTable . |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `nTableRef` | [in] | a new table number ID. |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | Create new User Provided Table generate Error. |
| `<Val>` | table number ID. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateUPTTableEx(System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateUPTTableEx(UserTableRequest request)`<br>
**Proposed named result:** `UserTableCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removeupttable"></a>

### 69. RemoveUPTTable

**Section:** Section: Get and Remove UPT Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_u_p_t_section.html#gaf713bf43b402d4efc3af2b5296ede180) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_u_p_t_section.html#gaf713bf43b402d4efc3af2b5296ede180) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#gaf713bf43b402d4efc3af2b5296ede180)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemoveUPTTable(VARIANT FAR&nTableRef)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableRef` | `VARIANT FAR&` | [in] | The existing table number ID. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE/1` | OK. |
| `FALSE/0` | Error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemoveUPTTable(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemoveUPTTable(UserTableRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removepropertyfromupttable"></a>

### 70. RemovePropertyFromUPTTable

**Section:** Section: Get and Remove UPT Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_u_p_t_section.html#gaf6f7c76255b453edb2cf220fb297ec72) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_u_p_t_section.html#gaf6f7c76255b453edb2cf220fb297ec72) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#gaf6f7c76255b453edb2cf220fb297ec72)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemovePropertyFromUPTTable(VARIANT FAR&nTableRef,VARIANT FAR&varSectionName)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableRef` | `VARIANT FAR&` | [in] | The existing table number ID.(Long) |
| `varSectionName` | `VARIANT FAR&` | [in] | UPT section string name.(String) |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | Error |
| `1` | OK. Property has been deleted. |
| `-100` | Invalid Table Number or Section Name. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemovePropertyFromUPTTable(System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemovePropertyFromUPTTable(UserTableRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createpropertyfromusertable"></a>

### 71. CreatePropertyFromUserTable

**Section:** Section: Get and Remove UPT Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_u_p_t_section.html#gabb8beaa20258b93f62714c9672415350) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_u_p_t_section.html#gabb8beaa20258b93f62714c9672415350) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#gabb8beaa20258b93f62714c9672415350)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreatePropertyFromUserTable(const VARIANT FAR&SectionName,const VARIANT FAR&TableNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `SectionName` | `const VARIANT FAR&` | [in] | Section name(Type: string) |
| `TableNo` | `const VARIANT FAR&` | [in] | Table Id(Type: long/integer) |


**Native return description:** the section property Ref ID.

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreatePropertyFromUserTable(System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreatePropertyFromUserTable(MemberSpecificationRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getuserprovidedtablecount"></a>

### 72. GetUserProvidedTableCount

**Section:** Section: Get and Remove UPT Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_u_p_t_section.html#ga32c6d22bc1d698ce9e1ea6b2f8473cd9) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_u_p_t_section.html#ga32c6d22bc1d698ce9e1ea6b2f8473cd9) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga32c6d22bc1d698ce9e1ea6b2f8473cd9)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetUserProvidedTableCount()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return description:** The number of UPT tables.

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetUserProvidedTableCount()`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetUserProvidedTableCount()`<br>
**Proposed named result:** `PropertyCountResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getuserprovidedtablelist"></a>

### 73. GetUserProvidedTableList

**Section:** Section: Get and Remove UPT Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_u_p_t_section.html#ga76a5e72dc10bf0cb788454375ef3717b) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_u_p_t_section.html#ga76a5e72dc10bf0cb788454375ef3717b) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga76a5e72dc10bf0cb788454375ef3717b)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetUserProvidedTableList(VARIANT FAR&nTableList)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableList` | `VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `UPT` | [out] | table ID list(Type: Long Array),array size refer to OSPropertyUI::GetUserProvidedTableCount |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE` | Retrieves the UPT Table list successfully. |
| `FALSE` | Unable to retrieve the UPT table list. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetUserProvidedTableList(System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetUserProvidedTableList(UserTableQuery request)`<br>
**Proposed named result:** `PropertyIdentifierListResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getuserprovidedtablesectioncount"></a>

### 74. GetUserProvidedTableSectionCount

**Section:** Section: Get and Remove UPT Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_u_p_t_section.html#ga300e2d80e44a9a02dc31dd461dde271e) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_u_p_t_section.html#ga300e2d80e44a9a02dc31dd461dde271e) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga300e2d80e44a9a02dc31dd461dde271e)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetUserProvidedTableSectionCount(const VARIANT FAR&nTableNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableNo` | `const VARIANT FAR&` | [in] | The User Provided Table (UPT) number ID. |


**Native return description:** The number of section(s) in given UPT.

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetUserProvidedTableSectionCount(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetUserProvidedTableSectionCount(UserTableQuery request)`<br>
**Proposed named result:** `PropertyCountResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getuserprovidedtablesectionlist"></a>

### 75. GetUserProvidedTableSectionList

**Section:** Section: Get and Remove UPT Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_u_p_t_section.html#ga01f780a22d5a0720124a9a18c91461d7) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_u_p_t_section.html#ga01f780a22d5a0720124a9a18c91461d7) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga01f780a22d5a0720124a9a18c91461d7)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetUserProvidedTableSectionList(const VARIANT FAR&nTableNo,VARIANT FAR&sectionList)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableNo` | `const VARIANT FAR&` | [in] | The User Provided Table (UPT) number ID. |
| `sectionList` | `VARIANT FAR&` | [out] | CSStringList with indexes and corresponding section string names included. |


**Native return description:** The number of section(s) in given UPT.

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetUserProvidedTableSectionList(System.Object, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetUserProvidedTableSectionList(UserTableQuery request)`<br>
**Proposed named result:** `PropertyIdentifierListResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getuserprovidedtablesectiontype"></a>

### 76. GetUserProvidedTableSectionType

**Section:** Section: Get and Remove UPT Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_u_p_t_section.html#gadf28c45e67614bb66eb332b8dd76588b) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_u_p_t_section.html#gadf28c45e67614bb66eb332b8dd76588b) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#gadf28c45e67614bb66eb332b8dd76588b)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetUserProvidedTableSectionType(const VARIANT FAR&nTableNo,VARIANT FAR&sectionType)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableNo` | `const VARIANT FAR&` | [in] | The User Provided Table (UPT) number ID. |
| `sectionType` | `VARIANT FAR&` | [out] | Number referring to Section Type Code table, ref function OSPropertyUI::GetUserProvidedTableSectionProperties . |


**Native return description:** The number of section(s) in given UPT.

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetUserProvidedTableSectionType(System.Object, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetUserProvidedTableSectionType(UserTableQuery request)`<br>
**Proposed named result:** `SpecificationQueryResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getuserprovidedtablesectionpropertycount"></a>

### 77. GetUserProvidedTableSectionPropertyCount

**Section:** Section: Get and Remove UPT Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_u_p_t_section.html#ga43427a4ed7d22d28a82038f6351fee1e) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_u_p_t_section.html#ga43427a4ed7d22d28a82038f6351fee1e) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga43427a4ed7d22d28a82038f6351fee1e)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetUserProvidedTableSectionPropertyCount(const VARIANT FAR&nTableNo,const VARIANT FAR&sectionName)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableNo` | `const VARIANT FAR&` | [in] | The User Provided Table (UPT) number ID. |
| `sectionName` | `const VARIANT FAR&` | [in] | UPT section string name given to this section property. |


**Native return description:** The number of section(s) in given UPT.

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetUserProvidedTableSectionPropertyCount(System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetUserProvidedTableSectionPropertyCount(UserTableQuery request)`<br>
**Proposed named result:** `UserTableSectionResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getuserprovidedtablesectionproperties"></a>

### 78. GetUserProvidedTableSectionProperties

**Section:** Section: Get and Remove UPT Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_u_p_t_section.html#ga6fa18da33cf823a4a528ddaa35d7aeb3) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_u_p_t_section.html#ga6fa18da33cf823a4a528ddaa35d7aeb3) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga6fa18da33cf823a4a528ddaa35d7aeb3)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetUserProvidedTableSectionProperties(const VARIANT FAR&nTableNo,const VARIANT FAR&sectionName,VARIANT FAR&sectionType,VARIANT FAR&propertyVals)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nTableNo` | `const VARIANT FAR&` | [in] | The User Provided Table (UPT) number ID. |
| `sectionName` | `const VARIANT FAR&` | [in] | UPT section string name given to this section property. |
| `sectionType` | `VARIANT FAR&` | [out] | UPT Section Type from the below table. |
| `propertyVals` | `VARIANT FAR&` | [out] | A double VARIANT array for section property parameters (get array size by calling the API OSPropertyUI::GetCountofSectionPropertyValuesEx . |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE` | Successful. |
| `FALSE` | Unsuccessful. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetUserProvidedTableSectionProperties(System.Object, System.Object, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetUserProvidedTableSectionProperties(UserTableQuery request)`<br>
**Proposed named result:** `UserTableSectionResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getuserprovidedtableno"></a>

### 79. GetUserProvidedTableNo

**Section:** Section: Get and Remove UPT Profile Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_u_p_t_section.html#ga778d97759d0fae3321f116992db15f50) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_u_p_t_section.html#ga778d97759d0fae3321f116992db15f50) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga778d97759d0fae3321f116992db15f50)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P8 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetUserProvidedTableNo(const VARIANT FAR&varSecRefNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varSecRefNo` | `const VARIANT FAR&` | [in] | User Provided Table (UPT) index. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | User Provided Table (UPT) number ID. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetUserProvidedTableNo(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetUserProvidedTableNo(UserTableQuery request)`<br>
**Proposed named result:** `PropertyReferenceResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-creatememberreleasespec"></a>

### 80. CreateMemberReleaseSpec

**Section:** Specification: Create Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_spec.html#gabfb89e094b1aa1a943716dfb7a0d4841) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_spec.html#gabfb89e094b1aa1a943716dfb7a0d4841) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gabfb89e094b1aa1a943716dfb7a0d4841)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateMemberReleaseSpec(const VARIANT FAR&varLocation,const VARIANT FAR&varDOFRelease,const VARIANT FAR&varSpringConst)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varLocation` | `const VARIANT FAR&` | [in] | The offset location at START (= 0) or END (= 1) of the member. |
| `varDOFRelease` | `const VARIANT FAR&` | [in] | Degrees of freedom: No Release (= 0) or Release (= 1) for FX, FY, FZ, MX, MY and MZ. |
| `varSpringConst` | `const VARIANT FAR&` | [in] | The variable spring constants KFX, KFY, KFZ, KMX, KMY and KMZ. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned specification number ID. |
| `-106` | 1 dimensional array of long for varDOFRelease and 1 dimensional array of double for varSpringConst expected. |
| `-108` | Array size is smaller than expected (size should be 6). |
| `-6020` | Library Error: Unable to create MEMBER RELEASE specification. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateMemberReleaseSpec(System.Object, System.Object, System.Object)`

**Main adapter targets:** [CreateAndAssignMemberEndReleases](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1585), [CreateAndAssignMemberEndReleases](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1590), [CreateAndAssignMemberEndReleases](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1711), [CreateAndAssignMemberEndReleases](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1716)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateMemberReleaseSpec(MemberReleaseRequest request)`<br>
**Proposed named result:** `SpecificationCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-creatememberpartialreleasespec"></a>

### 81. CreateMemberPartialReleaseSpec

**Section:** Specification: Create Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_spec.html#gae408d55746db59e4c22c2e505bb403a2) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_spec.html#gae408d55746db59e4c22c2e505bb403a2) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gae408d55746db59e4c22c2e505bb403a2)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateMemberPartialReleaseSpec(const VARIANT FAR&varLocation,const VARIANT FAR&varDOFRelease,const VARIANT FAR&varFactor)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varLocation` | `const VARIANT FAR&` | [in] | The offset location at START (= 0) or END (= 1) of the member. |
| `varDOFRelease` | `const VARIANT FAR&` | [in] | Degrees of freedom: No Release (= 0) or Release (= 1) for FX, FY, FZ, MX, MY and MZ. |
| `varFactor` | `const VARIANT FAR&` | [in] | The partial release factor in respective DOFs. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK |
| `-106` | 1 dimensional array of long for varDOFRelease and 1 dimensional array of double for varFactor expected. |
| `-108` | Array size is smaller than expected (size should be 6). |
| `-6020` | Library Error: Unable to create MEMBER RELEASE specification. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateMemberPartialReleaseSpec(System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreatePartialMemberRelease(MemberReleaseRequest request)`<br>
**Proposed named result:** `SpecificationCreationResult`

Native table says 0 OK, unlike adjacent specification-ID-returning APIs; do not assume zero is an assigned spec ID. Verify actual creation handle/read-back.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createmembertrussspec"></a>

### 82. CreateMemberTrussSpec

**Section:** Specification: Create Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_spec.html#ga19c64f2a9dc77cab9176f382524d17ca) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_spec.html#ga19c64f2a9dc77cab9176f382524d17ca) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#ga19c64f2a9dc77cab9176f382524d17ca)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateMemberTrussSpec()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned specification number ID. |
| `-6010` | Library Error: Unable to create MEMBER TRUSS specification. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateMemberTrussSpec()`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateMemberTrussSpec()`<br>
**Proposed named result:** `SpecificationCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-creatememberinactivespec"></a>

### 83. CreateMemberInactiveSpec

**Section:** Specification: Create Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_spec.html#gabdcfbecf01cd5889eb04da0aa8102a41) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_spec.html#gabdcfbecf01cd5889eb04da0aa8102a41) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gabdcfbecf01cd5889eb04da0aa8102a41)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateMemberInactiveSpec()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned specification number ID. |
| `-6011` | Library Error: Unable to create MEMBER INACTIVE specification. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateMemberInactiveSpec()`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateMemberInactiveSpec()`<br>
**Proposed named result:** `SpecificationCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createmembertensionspec"></a>

### 84. CreateMemberTensionSpec

**Section:** Specification: Create Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_spec.html#ga8db5109360921dde0350b2cdd4fe611a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_spec.html#ga8db5109360921dde0350b2cdd4fe611a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#ga8db5109360921dde0350b2cdd4fe611a)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateMemberTensionSpec()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned specification number ID. |
| `-6012` | Library Error: Unable to create MEMBER TENSION specification. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateMemberTensionSpec()`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateMemberTensionSpec()`<br>
**Proposed named result:** `SpecificationCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createmembercompressionspec"></a>

### 85. CreateMemberCompressionSpec

**Section:** Specification: Create Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_spec.html#gafd9eeea22012202b700e42de2dae4dc0) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_spec.html#gafd9eeea22012202b700e42de2dae4dc0) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gafd9eeea22012202b700e42de2dae4dc0)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateMemberCompressionSpec()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned specification number ID. |
| `-6013` | Library Error: Unable to create MEMBER COMPRESSION specification. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateMemberCompressionSpec()`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateMemberCompressionSpec()`<br>
**Proposed named result:** `SpecificationCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-creatememberignorestiffspec"></a>

### 86. CreateMemberIgnoreStiffSpec

**Section:** Specification: Create Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_spec.html#gad30bdd6dd57392faa1cf132df4819164) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_spec.html#gad30bdd6dd57392faa1cf132df4819164) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gad30bdd6dd57392faa1cf132df4819164)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateMemberIgnoreStiffSpec()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned specification number ID. |
| `-6014` | Library Error: Unable to create IGNORE STIFFNESS specification. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateMemberIgnoreStiffSpec()`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateMemberIgnoreStiffSpec()`<br>
**Proposed named result:** `SpecificationCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createmembercablespec"></a>

### 87. CreateMemberCableSpec

**Section:** Specification: Create Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_spec.html#ga354fa1642399ba13018d1a610149589c) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_spec.html#ga354fa1642399ba13018d1a610149589c) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#ga354fa1642399ba13018d1a610149589c)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateMemberCableSpec(const VARIANT FAR&varnTension,const VARIANT FAR&varfValue)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnTension` | `const VARIANT FAR&` | [in] | Specify additional information about the cable: 0 = Initial TENSION of Value in the cable to be considered. 1 = Unstressed LENGTH of Value to be considered. |
| `varfValue` | `const VARIANT FAR&` | [in] | Value for TENSION or Unstressed LENGTH . |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned specification number ID. |
| `-6015` | Library Error: Unable to create MEMBER CABLE specification. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateMemberCableSpec(System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateMemberCableSpec(MemberSpecificationRequest request)`<br>
**Proposed named result:** `SpecificationCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createmembercablespecex"></a>

### 88. CreateMemberCableSpecEx

**Section:** Specification: Create Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_spec.html#ga1fe56e4b2b563db42107d0f2b68b8fb4) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_spec.html#ga1fe56e4b2b563db42107d0f2b68b8fb4) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#ga1fe56e4b2b563db42107d0f2b68b8fb4)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateMemberCableSpecEx(const VARIANT FAR&varbTension,const VARIANT FAR&varfValue,const VARIANT FAR&varnAppliedEnd,const VARIANT FAR&varfFwX,const VARIANT FAR&varfFwY,const VARIANT FAR&varfFwZ)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varbTension` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varfValue` | `const VARIANT FAR&` | [in] | Value for TENSION or Unstressed LENGTH . |
| `varnAppliedEnd` | `const VARIANT FAR&` | [in] | Initial tension end for TENSION . To be used for Advanced Cable Analysis 0 = cable start or end node will not be considered. 1 = cable start node to be considered. 2 = cable end node to be considered. |
| `varfFwX` | `const VARIANT FAR&` | [in] | Multiplying factor on self weight component applied in the global X. |
| `varfFwY` | `const VARIANT FAR&` | [in] | Multiplying factor on self weight component applied in the global Y. |
| `varfFwZ` | `const VARIANT FAR&` | [in] | Multiplying factor on self weight component applied in the global Z. |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `varnTension` | [in] | Specify additional information about the cable: 0 = Initial TENSION of Value in the cable to be considered. 1 = Unstressed LENGTH of Value to be considered. |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned specification number ID. |
| `-6015` | Library Error: Unable to create MEMBER CABLE specification. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateMemberCableSpecEx(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateMemberCableSpecEx(MemberSpecificationRequest request)`<br>
**Proposed named result:** `SpecificationCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-creatememberoffsetspec"></a>

### 89. CreateMemberOffsetSpec

**Section:** Specification: Create Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_spec.html#ga452a2b2f15fcbf10a46a11e8dec7ef42) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_spec.html#ga452a2b2f15fcbf10a46a11e8dec7ef42) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#ga452a2b2f15fcbf10a46a11e8dec7ef42)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateMemberOffsetSpec(const VARIANT FAR&varnLocation,const VARIANT FAR&varnRefLocal,const VARIANT FAR&varfOffsetX,const VARIANT FAR&varfOffsetY,const VARIANT FAR&varfOffsetZ)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnLocation` | `const VARIANT FAR&` | [in] | The offset location at START (= 0) or END (= 1) of the member. |
| `varnRefLocal` | `const VARIANT FAR&` | [in] | Offset with respect ti Global axis (= 0) or Local axis (= 1). |
| `varfOffsetX` | `const VARIANT FAR&` | [in] | The offset x coordinate. |
| `varfOffsetY` | `const VARIANT FAR&` | [in] | The offset y coordinate. |
| `varfOffsetZ` | `const VARIANT FAR&` | [in] | The offset z coordinate. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned specification number ID. |
| `<Val>` | The assigned specification number ID. |
| `-6016` | Library Error: Unable to create OFFSET specification. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateMemberOffsetSpec(System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** [AssignMemberEndOffsets](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L732), [AssignMemberEndOffsets](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L733), [AssignMemberEndOffset](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L753), [AssignMemberEndOffsets](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L782), [AssignMemberEndOffsets](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L783), [AssignMemberEndOffset](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L806), [CreateOffsetSpecification](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1425)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateMemberOffsetSpec(OffsetSpecificationRequest request)`<br>
**Proposed named result:** `SpecificationCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createelementnodereleasespec"></a>

### 90. CreateElementNodeReleaseSpec

**Section:** Specification: Create Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_spec.html#gafd355c2d78879ae36eb5cabf7c0d99eb) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_spec.html#gafd355c2d78879ae36eb5cabf7c0d99eb) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gafd355c2d78879ae36eb5cabf7c0d99eb)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateElementNodeReleaseSpec(const VARIANT FAR&varNode,const VARIANT FAR&varDOFRelease)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varNode` | `const VARIANT FAR&` | [in] | The node number ID to be released. |
| `varDOFRelease` | `const VARIANT FAR&` | [in] | Degrees of freedom: No Release (= 0) or Release (= 1) for FX, FY, FZ, MX, MY and MZ. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK |
| `-106` | 1 dimensional array of long for varDOFRelease expected. |
| `-108` | Array size is smaller than expected (size should be 6). |
| `-6021` | Library Error: Unable to create ELEMENT NODE RELEASE specification. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateElementNodeReleaseSpec(System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateElementNodeRelease(ElementReleaseRequest request)`<br>
**Proposed named result:** `SpecificationCreationResult`

Native table says 0 OK; keep status and optional created identity separate, verify incidence/DOF conventions.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createelementplanestressspec"></a>

### 91. CreateElementPlaneStressSpec

**Section:** Specification: Create Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_spec.html#gaec273848a190a0e1c308a4c770b45f1f) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_spec.html#gaec273848a190a0e1c308a4c770b45f1f) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gaec273848a190a0e1c308a4c770b45f1f)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateElementPlaneStressSpec()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned specification number ID. |
| `-6018` | Library Error: Unable to create ELEMENT PLANE STRESS specification. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateElementPlaneStressSpec()`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateElementPlaneStressSpec()`<br>
**Proposed named result:** `SpecificationCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createelementignoreinplanerotnspec"></a>

### 92. CreateElementIgnoreInplaneRotnSpec

**Section:** Specification: Create Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_spec.html#gade9e2bced8795b0cd4dab888244829b3) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_spec.html#gade9e2bced8795b0cd4dab888244829b3) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gade9e2bced8795b0cd4dab888244829b3)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateElementIgnoreInplaneRotnSpec()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned specification number ID. |
| `-6019` | Library Error: Unable to create ELEMENT INPLANE ROTATION specification. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateElementIgnoreInplaneRotnSpec()`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateElementIgnoreInplaneRotnSpec()`<br>
**Proposed named result:** `SpecificationCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-addcontroldependentrelation"></a>

### 93. AddControlDependentRelation

**Section:** Specification: Create Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_spec.html#gacfbf4be0e4710af48fbbb2c08bec0cfd) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_spec.html#gacfbf4be0e4710af48fbbb2c08bec0cfd) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gacfbf4be0e4710af48fbbb2c08bec0cfd)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AddControlDependentRelation(const VARIANT FAR&varControlNode,const VARIANT FAR&varRegid_XY_YZ_ZX,const VARIANT FAR&varFX,const VARIANT FAR&varFY,const VARIANT FAR&varFZ,const VARIANT FAR&varMX,const VARIANT FAR&varMY,const VARIANT FAR&varMZ,const VARIANT FAR&varDependentNodeArray)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varControlNode` | `const VARIANT FAR&` | [in] | Set node (number ID) control node. |
| `varRegid_XY_YZ_ZX` | `const VARIANT FAR&` | [in] | Set plate rigid: all directions rigid (= 0), XY plate rigid (= 1), YZ plate rigid (= 2), ZX plate rigid (= 3), specific define FX, FY, FZ, MX, MY, MZ rigid (= others). |
| `varFX` | `const VARIANT FAR&` | [in] | Rigid in X direction translation (Rigid = 1, Not Rigid = 0). |
| `varFY` | `const VARIANT FAR&` | [in] | Rigid in Y direction translation (Rigid = 1, Not Rigid = 0). |
| `varFZ` | `const VARIANT FAR&` | [in] | Rigid in Z direction translation (Rigid = 1, Not Rigid = 0). |
| `varMX` | `const VARIANT FAR&` | [in] | Rigid in X direction rotation (Rigid = 1, Not Rigid = 0). |
| `varMY` | `const VARIANT FAR&` | [in] | Rigid in Y direction rotation (Rigid = 1, Not Rigid = 0). |
| `varMZ` | `const VARIANT FAR&` | [in] | Rigid in Z direction rotation (Rigid = 1, Not Rigid = 0). |
| `varDependentNodeArray` | `const VARIANT FAR&` | [in] | Nodes number ID VARIANT array. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-106` | 1 dimensional array of long expected. |
| `-6029` | Library Error: Unable to create CONTROL/DEPENDENT specification. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AddControlDependentRelation(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `AddControlDependentRelation(ControlDependentRequest request)`<br>
**Proposed named result:** `ControlDependentMutationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createelementoffsetspec"></a>

### 94. CreateElementOffsetSpec

**Section:** Specification: Create Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_spec.html#gafd77251b4fa48baa9491672bf3a3a3ca) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_spec.html#gafd77251b4fa48baa9491672bf3a3a3ca) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gafd77251b4fa48baa9491672bf3a3a3ca)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateElementOffsetSpec(const VARIANT FAR&varnDirection,const VARIANT FAR&varnPlateNodeIndex,const VARIANT FAR&varfOffsetX,const VARIANT FAR&varfOffsetY,const VARIANT FAR&varfOffsetZ)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnDirection` | `const VARIANT FAR&` | [in] | The offset direction at Local (= 0) or Global (= 1) of the element. |
| `varnPlateNodeIndex` | `const VARIANT FAR&` | [in] | The Node index at which the offset is to be applied for local and global directions (1/2/3/4). |
| `varfOffsetX` | `const VARIANT FAR&` | [in] | The offset x coordinate. |
| `varfOffsetY` | `const VARIANT FAR&` | [in] | The offset y coordinate. |
| `varfOffsetZ` | `const VARIANT FAR&` | [in] | The offset z coordinate. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned specification number ID. |
| `<Val>` | The assigned specification number ID. |
| `-6016` | Library Error: Unable to create OFFSET specification. |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 CreateElementOffsetSpec(System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateElementOffsetSpec(OffsetSpecificationRequest request)`<br>
**Proposed named result:** `SpecificationCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createelementlocalzoffsetspec"></a>

### 95. CreateElementLocalZOffsetSpec

**Section:** Specification: Create Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_spec.html#gac1057cd910b775a08d6c2c7df22e2781) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_spec.html#gac1057cd910b775a08d6c2c7df22e2781) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gac1057cd910b775a08d6c2c7df22e2781)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateElementLocalZOffsetSpec(const VARIANT FAR&varfZOffsetNode1,const VARIANT FAR&varfZOffsetNode2,const VARIANT FAR&varfZOffsetNode3,const VARIANT FAR&varfZOffsetNode4)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varfZOffsetNode1` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varfZOffsetNode2` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varfZOffsetNode3` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varfZOffsetNode4` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `dZOffsetNode1` | [in] | The offset at Node 1 for local-Z offset. |
| `dZOffsetNode2` | [in] | The offset at Node 2 for local-Z offset. |
| `dZOffsetNode3` | [in] | The offset at Node 3 for local-Z offset. |
| `dZOffsetNode4` | [in] | The offset at Node 4 for local-Z offset. |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned specification number ID. |
| `<Val>` | The assigned specification number ID. |
| `-6016` | Library Error: Unable to create OFFSET specification. |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 CreateElementLocalZOffsetSpec(System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateElementLocalZOffsetSpec(OffsetSpecificationRequest request)`<br>
**Proposed named result:** `SpecificationCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-creatememberfireproofingspec"></a>

### 96. CreateMemberFireProofingSpec

**Section:** Specification: Create Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_spec.html#ga6792430dddf478392843d424059c8730) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_spec.html#ga6792430dddf478392843d424059c8730) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#ga6792430dddf478392843d424059c8730)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateMemberFireProofingSpec(const VARIANT FAR&varnFireProofType,const VARIANT FAR&varfThickness,const VARIANT FAR&varfDensity)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnFireProofType` | `const VARIANT FAR&` | [in] | Specify type of fire proofing: (type - Long/Integer) 1 = BFP Block Fireproofing. 2 = CFP Contour Fireproofing. |
| `varfThickness` | `const VARIANT FAR&` | [in] | Thickness of the Fireproofing (type - Double). |
| `varfDensity` | `const VARIANT FAR&` | [in] | Density of the Fireproofing material (type - Double). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | Zero based index for the newly created specification (-1: Unable to create MEMBER FIREPROOFING specification). |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateMemberFireProofingSpec(System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateMemberFireProofing(FireProofingRequest request)`<br>
**Proposed named result:** `SpecificationCreationResult`

Native creation index is zero-based (-1 failure), while detail getter uses nonzero-based index; do not translate without explicit proof.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-deletememberreleasespec"></a>

### 97. DeleteMemberReleaseSpec

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#gafe30a2d90280de4c4fd6149959730507) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#gafe30a2d90280de4c4fd6149959730507) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gafe30a2d90280de4c4fd6149959730507)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::DeleteMemberReleaseSpec(const VARIANT FAR&varnBeamNo,const VARIANT FAR&varLocation)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID(Type: Long/Integer). |
| `varLocation` | `const VARIANT FAR&` | [in] | The Release location at START (= 0) or END (= 1) of the member. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `FALSE` | Delete Member Release specification failed |
| `TRUE` | Delete Member Release specification Successful. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object DeleteMemberReleaseSpec(System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `DeleteMemberReleaseSpec(MemberReleaseRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-deleteallcontroldependentrelations"></a>

### 98. DeleteAllControlDependentRelations

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#gad780df368b309c15dec931245113040c) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#gad780df368b309c15dec931245113040c) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gad780df368b309c15dec931245113040c)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
long OSPropertyUI::DeleteAllControlDependentRelations()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK Successfully deleted. |
| `-1` | ERROR Delete unsuccessful.. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object DeleteAllControlDependentRelations()`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `DeleteAllControlDependentRelations()`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getelementoffsetspeccount"></a>

### 99. GetElementOffsetSpecCount

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga5facbddbfb9ea2ad0b48c454a122cf24) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga5facbddbfb9ea2ad0b48c454a122cf24) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga5facbddbfb9ea2ad0b48c454a122cf24)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<int> GetElementOffsetSpecificationCount()`.<br>
**Plan:** P2 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetElementOffsetSpecCount()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The total number of element offset specifications. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetElementOffsetSpecCount()`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<int> GetElementOffsetSpecificationCount()`<br>
**Implemented result:** `PropertyQueryResult<int>`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-removeallelementoffsetspec"></a>

**Implemented behavior and alternatives:** This counts native offset specifications, not plates, corners, nonzero vectors or assigned members. Zero is a valid count. Use GetPlateOffsetSpecification to inspect one plate corner and its stored convention. Undocumented negative counts are rejected.

**Current native call sites:** [GetElementOffsetSpecificationCount](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L53). See [usage and qualification](../Property-Porting.md#getelementoffsetspecificationcount).

### 100. RemoveAllElementOffsetSpec

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#gaa7bdd908c360ccf8ca9e41ee0e894b5a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#gaa7bdd908c360ccf8ca9e41ee0e894b5a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gaa7bdd908c360ccf8ca9e41ee0e894b5a)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemoveAllElementOffsetSpec()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | OK. |
| `0` | No element offset specification present |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemoveAllElementOffsetSpec()`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemoveAllElementOffsetSpec()`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getmemberspeccode"></a>

### 101. GetMemberSpecCode

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga6b19b83068026b147eeb88a3528328d0) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga6b19b83068026b147eeb88a3528328d0) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga6b19b83068026b147eeb88a3528328d0)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetMemberSpecCode(const VARIANT FAR&varMembNo,VARIANT FAR&SpecCode)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varMembNo` | `const VARIANT FAR&` | [in] | The member number ID. |
| `SpecCode` | `VARIANT FAR&` | [out] | Value referring to type of member specification; Value Type of Member Specification 0 Truss Member 1 Tension-only Member 2 Compression-only Member 3 Cable-only Member 4 Joist Member -1 Other For additional information, please refer to Section "TR.23 Axial Member Specifications" of the Technical Reference Manual. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE/1` | OK. |
| `FALSE/0` | Error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetMemberSpecCode(System.Object, System.Object ByRef)`

**Main adapter targets:** [GetMemberSpecification](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L874)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetMemberSpecCode(SpecificationQuery request)`<br>
**Proposed named result:** `SpecificationQueryResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removepropertyfrombeam"></a>

### 102. RemovePropertyFromBeam

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga551c8845b034009e68a16ec2813c50bc) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga551c8845b034009e68a16ec2813c50bc) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga551c8845b034009e68a16ec2813c50bc)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemovePropertyFromBeam(const VARIANT FAR&nBeamNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemovePropertyFromBeam(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemovePropertyFromBeam(MemberSpecificationRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removebeampropertyhelper"></a>

### 103. RemoveBeamPropertyHelper

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#gadb028ea4f8477119ab50eb4881ebef61) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#gadb028ea4f8477119ab50eb4881ebef61) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gadb028ea4f8477119ab50eb4881ebef61)<br>
**Classification:** Native private helper: excluded<br>
**Current typed coverage:** None.<br>
**Plan:** P0 — Native private helper: excluded

```cpp
BOOL OSPropertyUI::RemoveBeamPropertyHelper(long&beamNo)private
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `beamNo` | `long&` | [in] | The beam number ID. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** No matching name in either inspected interface; native availability must be established independently.

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Public API decision:** Native private helper: excluded. No new managed entry proposed.

<a id="property-deleteproperty"></a>

### 104. DeleteProperty

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga26852ecd49fc5ea7657d2b5198a1c7ba) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga26852ecd49fc5ea7657d2b5198a1c7ba) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga26852ecd49fc5ea7657d2b5198a1c7ba)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::DeleteProperty(const VARIANT FAR&nProperty)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nProperty` | `const VARIANT FAR&` | [in] | Property ID(Type:Long). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `FALSE` | Delete Property Generate Error. |
| `TRUE` | Delete Property Successful. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object DeleteProperty(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `DeleteProperty(MemberSpecificationRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getpropertyuniqueid"></a>

### 105. GetPropertyUniqueID

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga5aa6aa08217095c0a0eb65530dd867e8) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga5aa6aa08217095c0a0eb65530dd867e8) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga5aa6aa08217095c0a0eb65530dd867e8)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetPropertyUniqueID(const VARIANT FAR&nPropNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nPropNo` | `const VARIANT FAR&` | [in] | Property number(Type:Long). |


**Native return description:** Property Unique ID(Type: String).

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetPropertyUniqueID(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetPropertyUniqueID(SpecificationQuery request)`<br>
**Proposed named result:** `SpecificationQueryResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-setpropertyuniqueid"></a>

### 106. SetPropertyUniqueID

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#gac7758ac39ea94bba561989b4898e59bb) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#gac7758ac39ea94bba561989b4898e59bb) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gac7758ac39ea94bba561989b4898e59bb)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
void OSPropertyUI::SetPropertyUniqueID(const VARIANT FAR&nPropNo,const VARIANT FAR&szID)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nPropNo` | `const VARIANT FAR&` | [in] | Property number(Type:Long). |
| `szID` | `const VARIANT FAR&` | [in] | Property Unique ID(Type:String). |


**Native return description:** No explicit return section; verify before implementation.

**Installed 2025 / 2026 IOSPropertyUI:** `Void SetPropertyUniqueID(System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `SetPropertyUniqueID(MemberSpecificationRequest request)`<br>
**Proposed named result:** `PropertyContextChangeResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-deletememberspec"></a>

### 107. DeleteMemberSpec

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga1956ce5d3c9487681c3dcca8c2a7f4a4) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga1956ce5d3c9487681c3dcca8c2a7f4a4) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga1956ce5d3c9487681c3dcca8c2a7f4a4)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::DeleteMemberSpec(const VARIANT FAR&varnSpecNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnSpecNo` | `const VARIANT FAR&` | [in] | The specification number. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `FALSE` | Delete specification Failed. |
| `TRUE` | Delete specification Successful. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object DeleteMemberSpec(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `DeleteMemberSpec(MemberSpecificationRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removememberreleasespecfrombeam"></a>

### 108. RemoveMemberReleaseSpecFromBeam

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#gaaee16932dc7a02f3d707576fc2d009a0) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#gaaee16932dc7a02f3d707576fc2d009a0) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gaaee16932dc7a02f3d707576fc2d009a0)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemoveMemberReleaseSpecFromBeam(const VARIANT FAR&varnBeamNo,const VARIANT FAR&varLocation)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID(Type: Long/Integer). |
| `varLocation` | `const VARIANT FAR&` | [in] | The Release location at START (= 0) or END (= 1) of the member. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `FALSE` | Remove Member Release specification failed |
| `TRUE` | Remove Member Release specification Successful. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemoveMemberReleaseSpecFromBeam(System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemoveMemberReleaseSpecFromBeam(MemberReleaseRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removememberoffsetspecfrombeam"></a>

### 109. RemoveMemberOffsetSpecFromBeam

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga76d69608bda66b72f7a1c9011b68a46d) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga76d69608bda66b72f7a1c9011b68a46d) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga76d69608bda66b72f7a1c9011b68a46d)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemoveMemberOffsetSpecFromBeam(const VARIANT FAR&varnBeamNo,const VARIANT FAR&varLocation)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID(Type: Long/Integer). |
| `varLocation` | `const VARIANT FAR&` | [in] | The offset location at START (= 0) or END (= 1) of the member. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `FALSE` | Remove Member offset specification failed |
| `TRUE` | Remove Member offset specification Successful. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemoveMemberOffsetSpecFromBeam(System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemoveMemberOffsetSpecFromBeam(OffsetSpecificationRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removemembertrussspecfrombeam"></a>

### 110. RemoveMemberTrussSpecFromBeam

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga01e39b8ece2146ea7aa09b7ff33423cd) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga01e39b8ece2146ea7aa09b7ff33423cd) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga01e39b8ece2146ea7aa09b7ff33423cd)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemoveMemberTrussSpecFromBeam(const VARIANT FAR&varnBeamNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemoveMemberTrussSpecFromBeam(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemoveMemberTrussSpecFromBeam(MemberSpecificationRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removememberinactivespecfrombeam"></a>

### 111. RemoveMemberInactiveSpecFromBeam

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#gaec27f611f0d02de6278bdb6601d106f8) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#gaec27f611f0d02de6278bdb6601d106f8) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gaec27f611f0d02de6278bdb6601d106f8)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemoveMemberInactiveSpecFromBeam(const VARIANT FAR&varnBeamNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemoveMemberInactiveSpecFromBeam(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemoveMemberInactiveSpecFromBeam(MemberSpecificationRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removemembertensionspecfrombeam"></a>

### 112. RemoveMemberTensionSpecFromBeam

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga22be38efdbc61ef92dabf2b7eb25ad89) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga22be38efdbc61ef92dabf2b7eb25ad89) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga22be38efdbc61ef92dabf2b7eb25ad89)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemoveMemberTensionSpecFromBeam(const VARIANT FAR&varnBeamNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemoveMemberTensionSpecFromBeam(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemoveMemberTensionSpecFromBeam(MemberSpecificationRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removemembercompressionspecfrombeam"></a>

### 113. RemoveMemberCompressionSpecFromBeam

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga1fac54088e17dbc9a7e7e93a5a9d1d45) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga1fac54088e17dbc9a7e7e93a5a9d1d45) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga1fac54088e17dbc9a7e7e93a5a9d1d45)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemoveMemberCompressionSpecFromBeam(const VARIANT FAR&varnBeamNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemoveMemberCompressionSpecFromBeam(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemoveMemberCompressionSpecFromBeam(MemberSpecificationRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removememberignorestiffspecfrombeam"></a>

### 114. RemoveMemberIgnoreStiffSpecFromBeam

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga159089ba705d76f58988ecd3cd052be3) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga159089ba705d76f58988ecd3cd052be3) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga159089ba705d76f58988ecd3cd052be3)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemoveMemberIgnoreStiffSpecFromBeam(const VARIANT FAR&varnBeamNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemoveMemberIgnoreStiffSpecFromBeam(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemoveMemberIgnoreStiffSpecFromBeam(MemberSpecificationRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removemembercablespecfrombeam"></a>

### 115. RemoveMemberCableSpecFromBeam

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga2b82e36f819c8df67990bdc88da6dd6f) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga2b82e36f819c8df67990bdc88da6dd6f) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga2b82e36f819c8df67990bdc88da6dd6f)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemoveMemberCableSpecFromBeam(const VARIANT FAR&varnBeamNo,const VARIANT FAR&varLocation)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID(Type: Long/Integer). |
| `varLocation` | `const VARIANT FAR&` | [in] | The Cable location at Tension (= 0) or Length (= 1) of the member. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `FALSE` | Remove Member Cable specification failed |
| `TRUE` | Remove Member Cable specification Successful. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemoveMemberCableSpecFromBeam(System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemoveMemberCableSpecFromBeam(MemberSpecificationRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removeelementplanestressspecfromplate"></a>

### 116. RemoveElementPlaneStressSpecFromPlate

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#gafc527399dbd62917400f8922ba0b4294) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#gafc527399dbd62917400f8922ba0b4294) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gafc527399dbd62917400f8922ba0b4294)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemoveElementPlaneStressSpecFromPlate(const VARIANT FAR&varnPlateNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnPlateNo` | `const VARIANT FAR&` | [in] | The plate number ID. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemoveElementPlaneStressSpecFromPlate(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemoveElementPlaneStressSpecFromPlate(MemberSpecificationRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removeelementignoreinplanerotnspecfromplate"></a>

### 117. RemoveElementIgnoreInplaneRotnSpecFromPlate

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga9b1e19ac200b1c62409785f5c80dd845) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga9b1e19ac200b1c62409785f5c80dd845) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga9b1e19ac200b1c62409785f5c80dd845)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemoveElementIgnoreInplaneRotnSpecFromPlate(const VARIANT FAR&varnPlateNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnPlateNo` | `const VARIANT FAR&` | [in] | The plate number ID. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemoveElementIgnoreInplaneRotnSpecFromPlate(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemoveElementIgnoreInplaneRotnSpecFromPlate(MemberSpecificationRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removeelementnodereleasespecfromplate"></a>

### 118. RemoveElementNodeReleaseSpecFromPlate

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga6503cab6a999f8024fe606c4e32c614a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga6503cab6a999f8024fe606c4e32c614a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga6503cab6a999f8024fe606c4e32c614a)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemoveElementNodeReleaseSpecFromPlate(const VARIANT FAR&varnPlateNo,const VARIANT FAR&varNodeNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnPlateNo` | `const VARIANT FAR&` | [in] | The plate number ID. |
| `varNodeNo` | `const VARIANT FAR&` | [in] | The node number ID to be released. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemoveElementNodeReleaseSpecFromPlate(System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemoveElementNodeReleaseSpecFromPlate(MemberReleaseRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removeallelementnodereleasespec"></a>

### 119. RemoveAllElementNodeReleaseSpec

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga8a3da913ec174caaa00066aac9ac2cad) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga8a3da913ec174caaa00066aac9ac2cad) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga8a3da913ec174caaa00066aac9ac2cad)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemoveAllElementNodeReleaseSpec()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | OK. |
| `0` | No element release specification present |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemoveAllElementNodeReleaseSpec()`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemoveAllElementNodeReleaseSpec()`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getmemberreleasespecex"></a>

### 120. GetMemberReleaseSpecEx

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga8ba58d357fc17509602aff5e2e99c9d4) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga8ba58d357fc17509602aff5e2e99c9d4) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga8ba58d357fc17509602aff5e2e99c9d4)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<MemberReleaseSpecification> GetMemberReleaseSpecificationExtended(int memberId, MemberEnd memberEnd)`.<br>
**Plan:** P2 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetMemberReleaseSpecEx(const VARIANT FAR&varnBeamNo,const VARIANT FAR&varnEnd,VARIANT FAR&varnReleaseArray,VARIANT FAR&varfSpringConstArray,VARIANT FAR&varfMPFactor,VARIANT FAR&varfMPFactorArray)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID. |
| `varnEnd` | `const VARIANT FAR&` | [in] | Member Start end (= 0); member End end (= 1). |
| `varnReleaseArray` | `VARIANT FAR&` | [out] | Translational release VARIANT array with 6 elements for 6 DOFs. @ Element value: No release or spring = 0, release = 1, spring = -1 , Only MP defined = -3 , MPX, MPY or MPZ defined = -2 . |
| `varfSpringConstArray` | `VARIANT FAR&` | [out] | Rotational releases VARIANT array with 6 elements for 6 DOFs @ Element values Spring value or partial moment factor in floating point number |
| `varfMPFactor` | `VARIANT FAR&` | [out] | Partial moment release factor (same for MX, MY and MZ) |
| `varfMPFactorArray` | `VARIANT FAR&` | [out] | Rotational releases VARIANT array with 3 elements for 3 rotational DOFs @ Element values Spring value or partial moment factor in floating point number |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | OK. |
| `0` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetMemberReleaseSpecEx(System.Object, System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** [GetMemberReleaseSpecificationExtended](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L968)<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<MemberReleaseSpecification> GetMemberReleaseSpecificationExtended(int memberId, MemberEnd memberEnd)`<br>
**Implemented result:** `PropertyQueryResult<MemberReleaseSpecification>`

Include scalar MP plus three rotational MP factors; preserve -2/-3 flags and do not infer fixed releases from failed zero buffers.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getinactivemembercount"></a>

**Implemented behavior and alternatives:** Prefer this query when common MP or directional MPX/MPY/MPZ factors are needed. The basic GetMemberReleaseSpecification returns only six mode/parameter slots and cannot supply the separate extended factors. Native 1/true succeeds and 0/false fails. Modes 0/1/-1/-2/-3 are retained per DOF rather than converted to Boolean release flags. Unused spring/MP slots may remain unwritten and are represented as null; an applicable spring or factor must be written and finite. The native common MP factor is dimensionless; translational and rotational spring stiffness retain their different native dimensions.

**Current native call sites:** [Release](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L141). See [usage and qualification](../Property-Porting.md#getmemberreleasespecificationextended).

### 121. GetInactiveMemberCount

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga1b96f8f630976e634b4906f1a1bb60b3) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga1b96f8f630976e634b4906f1a1bb60b3) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga1b96f8f630976e634b4906f1a1bb60b3)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
long OSPropertyUI::GetInactiveMemberCount()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The total number of inactive member(s). |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 GetInactiveMemberCount()`

**Main adapter targets:** [GetInactiveMembersList](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L832)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetInactiveMemberCount()`<br>
**Proposed named result:** `PropertyCountResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getinactivememberlist"></a>

### 122. GetInactiveMemberList

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga25d477cfe62c96ccbd7901073e76540d) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga25d477cfe62c96ccbd7901073e76540d) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga25d477cfe62c96ccbd7901073e76540d)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
void OSPropertyUI::GetInactiveMemberList(VARIANT FAR&nInactiveMemList)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nInactiveMemList` | `VARIANT FAR&` | [out] | VARIANT array of LONG type, for storing returned member number ID(s) of all the members that are inactive. |


**Native return description:** No explicit return section; verify before implementation.

**Installed 2025 / 2026 IOSPropertyUI:** `Void GetInactiveMemberList(System.Object)`

**Main adapter targets:** [GetInactiveMembersList](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L834)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetInactiveMemberIds()`<br>
**Proposed named result:** `PropertyIdentifierListResult`

Main source marks both wrappers Obsolete(error=true). Native metadata is Void(Object) by value; require count/buffer/native fixtures, do not treat source presence as verified functionality.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getalphaangleforsection"></a>

### 123. GetAlphaAngleForSection

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga6aa625ced61caf38f8f164bbb4ecb0c0) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga6aa625ced61caf38f8f164bbb4ecb0c0) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga6aa625ced61caf38f8f164bbb4ecb0c0)<br>
**Classification:** Formatting/cross-reference only<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetAlphaAngleForSection(const VARIANT FAR&nPropNo,VARIANT FAR&dAlpha)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nPropNo` | `const VARIANT FAR&` | [in] | The specified property ID. |
| `dAlpha` | `VARIANT FAR&` | [out] | alpha angle returned (in Radian). |


**Native return description:** No explicit return section; verify before implementation.

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetAlphaAngleForSection(System.Object, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetAlphaAngleForSection(SectionQuery request)`<br>
**Proposed named result:** `SectionAngleResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

**2024 versus 2025/2026:**

| Year | Documented contract |
| --- | --- |
| 2024 | Returns the alpha angle of the section in radian. = Gets the angle between the principal axis and geometric axis of the section Parameters [in] nPropNo The specified property ID. [out] dAlpha alpha angle returned (in Radian). C++ Syntax VBA Syntax See also OSPropertyUI::GetAlphaAngleForSection |
| 2025 / 2026 | Returns the alpha angle of the section in radian. @ALPHA = Gets the angle between the principal axis and geometric axis of the section Parameters [in] nPropNo The specified property ID. [out] dAlpha alpha angle returned (in Radian). C++ Syntax VBA Syntax See also OSPropertyUI::GetAlphaAngleForSection |

<a id="property-getcentroidlocationforsection"></a>

### 124. GetCentroidLocationForSection

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#gac9e334a5b43efdadb0faaaa95b48e20b) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#gac9e334a5b43efdadb0faaaa95b48e20b) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gac9e334a5b43efdadb0faaaa95b48e20b)<br>
**Classification:** Formatting/cross-reference only<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetCentroidLocationForSection(const VARIANT FAR&nPropNo,VARIANT FAR&dCey,VARIANT FAR&dCez)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nPropNo` | `const VARIANT FAR&` | [in] | The specified property ID. |
| `dCey` | `VARIANT FAR&` | [out] | returns offset value of Centroid along Y axis. |
| `dCez` | `VARIANT FAR&` | [out] | returns offset value of Centroid along Z axis. |


**Native return description:** No explicit return section; verify before implementation.

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetCentroidLocationForSection(System.Object, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetCentroidLocationForSection(SectionQuery request)`<br>
**Proposed named result:** `SectionCentroidResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

**2024 versus 2025/2026:**

| Year | Documented contract |
| --- | --- |
| 2024 | Returns the location of the Centroid of the section. Gets the location of the Centroid of the specified section. Cez and Cey are distances to centroid from top left outer edge to the centroid in terms of Y axis and Z axis respectively. Parameters [in] nPropNo The specified property ID. [out] dCey returns offset value of Centroid along Y axis. [out] dCez returns offset value of Centroid along Z axis. C++ Syntax VBA Syntax See also OSPropertyUI::GetCentroidLocationForSection |
| 2025 / 2026 | Returns the location of the Centroid of the section. Gets the location of the Centroid of the specified section. @The Cez and Cey are distances to centroid from top left outer edge to the centroid in terms of Y axis and Z axis respectively. Parameters [in] nPropNo The specified property ID. [out] dCey returns offset value of Centroid along Y axis. [out] dCez returns offset value of Centroid along Z axis. C++ Syntax VBA Syntax See also OSPropertyUI::GetCentroidLocationForSection |

<a id="property-getfireproofedbeamcount"></a>

### 125. GetFireProofedBeamCount

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga2bfefd59d0590e70c72b7f086e6ede10) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga2bfefd59d0590e70c72b7f086e6ede10) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga2bfefd59d0590e70c72b7f086e6ede10)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetFireProofedBeamCount()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The total number of fire proofed beams in the current model. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetFireProofedBeamCount()`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetFireProofedBeamCount()`<br>
**Proposed named result:** `PropertyCountResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getfireproofedbeamlist"></a>

### 126. GetFireProofedBeamList

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga838cda3edcec54845560c94c9163d004) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga838cda3edcec54845560c94c9163d004) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga838cda3edcec54845560c94c9163d004)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetFireProofedBeamList(VARIANT FAR&nFireProofedBeamList)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nFireProofedBeamList` | `VARIANT FAR&` | [out] | VARIANT array of LONG type, for storing returned member number ID(s) of all the members that are fire proofed. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | if the method is successful. |
| `0` | if the method is unsuccessful. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetFireProofedBeamList(System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetFireProofedBeamList(SpecificationQuery request)`<br>
**Proposed named result:** `PropertyIdentifierListResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getfireproofdataforbeam"></a>

### 127. GetFireProofDataForBeam

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#gad5ab8626f3f670ef18fc37c7851528f3) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#gad5ab8626f3f670ef18fc37c7851528f3) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gad5ab8626f3f670ef18fc37c7851528f3)<br>
**Classification:** Type/meaning clarification<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetFireProofDataForBeam(const VARIANT FAR&varnBeamNo,VARIANT FAR&varnFireProofType,VARIANT FAR&varfThickness,VARIANT FAR&varfDensity)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number (type - Long/Integer). |
| `varnFireProofType` | `VARIANT FAR&` | [out] | Type of fire proof [1 for BFP, 2 for CFP] (type - Long/Integer). |
| `varfThickness` | `VARIANT FAR&` | [out] | Thickness of fire proof (type - Double). |
| `varfDensity` | `VARIANT FAR&` | [out] | Density of fire proof (type - Double). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | if the method is successful. |
| `0` | if the method is unsuccessful. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetFireProofDataForBeam(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetFireProofDataForBeam(SpecificationQuery request)`<br>
**Proposed named result:** `SpecificationQueryResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

**2024 versus 2025/2026:**

| Year | Documented contract |
| --- | --- |
| 2024 | Get fire proofing data for beam. Parameters [in] varnBeamNo The beam number (type - Long/Integer). [out] varnFireProofType Type of fire proof 1 for BFP, 2 for CFP . [out] varfThickness Thickness of fire proof (type - Double). [out] varfDensity Density of fire proof (type - Double). Return values 1 if the method is successful. 0 if the method is unsuccessful. VBA Syntax |
| 2025 / 2026 | Get fire proofing data for beam. Parameters [in] varnBeamNo The beam number (type - Long/Integer). [out] varnFireProofType Type of fire proof [1 for BFP, 2 for CFP] (type - Long/Integer). [out] varfThickness Thickness of fire proof (type - Double). [out] varfDensity Density of fire proof (type - Double). Return values 1 if the method is successful. 0 if the method is unsuccessful. VBA Syntax |

<a id="property-getfireproofingspeccount"></a>

### 128. GetFireProofingSpecCount

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga5b41580941d25c3f519a74ae1e840a60) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga5b41580941d25c3f519a74ae1e840a60) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga5b41580941d25c3f519a74ae1e840a60)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetFireProofingSpecCount()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The total number of fire proofing specification. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetFireProofingSpecCount()`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetFireProofingSpecCount()`<br>
**Proposed named result:** `PropertyCountResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getfireproofingspecdetails"></a>

### 129. GetFireProofingSpecDetails

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga9bdbff55763121a7a63149630ed66435) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga9bdbff55763121a7a63149630ed66435) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga9bdbff55763121a7a63149630ed66435)<br>
**Classification:** Type/meaning clarification<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetFireProofingSpecDetails(const VARIANT FAR&varnIndex,VARIANT FAR&varnFireProofType,VARIANT FAR&varfThickness,VARIANT FAR&varfDensity,VARIANT FAR&varnAssignCount)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnIndex` | `const VARIANT FAR&` | [in] | Non-zero based index of the fire proofing specification (type - Long/Integer). |
| `varnFireProofType` | `VARIANT FAR&` | [out] | Type of fire proof [1 for BFP, 2 for CFP] (type - Long/Interger). |
| `varfThickness` | `VARIANT FAR&` | [out] | Thickness of fire proof (type - Double). |
| `varfDensity` | `VARIANT FAR&` | [out] | Density of fire proof (type - Double). |
| `varnAssignCount` | `VARIANT FAR&` | [out] | Number of beams assigned (type - array of Long) |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | if the method is successful. |
| `0` | if the method is unsuccessful. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetFireProofingSpecDetails(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetFireProofingSpecDetails(SpecificationQuery request)`<br>
**Proposed named result:** `SpecificationQueryResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

**2024 versus 2025/2026:**

| Year | Documented contract |
| --- | --- |
| 2024 | Get the details for the specified fire proofing specification number. Parameters [in] varnIndex Non-zero based index of the fire proofing specification (type - Long/Integer). [out] varnFireProofType Type of fire proof 1 for BFP, 2 for CFP . [out] varfThickness Thickness of fire proof (type - Double). [out] varfDensity Density of fire proof (type - Double). [out] varnAssignCount Number of beams assigned (type - array of Long) Return values 1 if the method is successful. 0 if the method is unsuccessful. VBA Syntax See also OSPropertyUI::GetFireProofingSpecCount() |
| 2025 / 2026 | Get the details for the specified fire proofing specification number. Parameters [in] varnIndex Non-zero based index of the fire proofing specification (type - Long/Integer). [out] varnFireProofType Type of fire proof [1 for BFP, 2 for CFP] (type - Long/Interger). [out] varfThickness Thickness of fire proof (type - Double). [out] varfDensity Density of fire proof (type - Double). [out] varnAssignCount Number of beams assigned (type - array of Long) Return values 1 if the method is successful. 0 if the method is unsuccessful. VBA Syntax See also OSPropertyUI::GetFireProofingSpecCount() |

<a id="property-getfireproofingspecassignedbeamcount"></a>

### 130. GetFireProofingSpecAssignedBeamCount

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga7fef558917ef800217ce7ef0160c06d1) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga7fef558917ef800217ce7ef0160c06d1) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga7fef558917ef800217ce7ef0160c06d1)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetFireProofingSpecAssignedBeamCount(const VARIANT FAR&varnIndex)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnIndex` | `const VARIANT FAR&` | [in] | Non-zero based index of the fire proofing specification (type - Long/Integer). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `Returns` | the number of beams assigned with a particular fire proofing specification. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetFireProofingSpecAssignedBeamCount(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetFireProofingSpecAssignedBeamCount(SpecificationQuery request)`<br>
**Proposed named result:** `PropertyCountResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getfireproofingspecassignedbeamlist"></a>

### 131. GetFireProofingSpecAssignedBeamList

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga1cc2f6b4d2a0be3115cdc362d07b48e4) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga1cc2f6b4d2a0be3115cdc362d07b48e4) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga1cc2f6b4d2a0be3115cdc362d07b48e4)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetFireProofingSpecAssignedBeamList(const VARIANT FAR&varnIndex,VARIANT FAR&nFireProofedBeamList)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnIndex` | `const VARIANT FAR&` | [in] | Non-zero based index of the fire proofing specification (type - Long/Integer). |
| `nFireProofedBeamList` | `VARIANT FAR&` | [out] | VARIANT array of LONG type, for storing returned member numbers of all the members that are fire proofed with a particular fire proofing specification. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | if the method is successful. |
| `0` | if the method is unsuccessful. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetFireProofingSpecAssignedBeamList(System.Object, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetFireProofingSpecAssignedBeamList(SpecificationQuery request)`<br>
**Proposed named result:** `PropertyIdentifierListResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removememberfireproofingspecfrombeam"></a>

### 132. RemoveMemberFireProofingSpecFromBeam

**Section:** Specification: Get and Remove Specification Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html#ga5ed5d25078d40c5c9c9f011dfc38db57) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html#ga5ed5d25078d40c5c9c9f011dfc38db57) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga5ed5d25078d40c5c9c9f011dfc38db57)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemoveMemberFireProofingSpecFromBeam(const VARIANT FAR&varnBeamNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID (type - Long/Integer). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | Fire proofing specification removed from beam. |
| `0` | Unable to remove fire proofing specification from beam. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemoveMemberFireProofingSpecFromBeam(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemoveMemberFireProofingSpecFromBeam(FireProofingRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createassignprofileproperty"></a>

### 133. CreateAssignProfileProperty

**Section:** Section: Assign Section to Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_assign_section.html#ga9e14482ffc29d710f72d6e8f648945b6) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_assign_section.html#ga9e14482ffc29d710f72d6e8f648945b6) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#ga9e14482ffc29d710f72d6e8f648945b6)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P5 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateAssignProfileProperty(const VARIANT FAR&varnAssignType)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnAssignType` | `const VARIANT FAR&` | [in] | Profile type number ID. Type of Profile Value Angle 0 Double Angle 1 Beam 2 Column 3 Channel 4 |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The assigned section property ID. |
| `0` | Library Error: Unable to create property. |
| `-6008` | Invalid assign profile type. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateAssignProfileProperty(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateAssignProfileProperty(MemberSpecificationRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-assignbeamproperty"></a>

### 134. AssignBeamProperty

**Section:** Section: Assign Section to Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_assign_section.html#gab13d04addd4f49d9ddeb79bc04e076cd) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_assign_section.html#gab13d04addd4f49d9ddeb79bc04e076cd) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#gab13d04addd4f49d9ddeb79bc04e076cd)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P5 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AssignBeamProperty(const VARIANT FAR&nBeamNo,const VARIANT FAR&nProperty)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nBeamNo` | `const VARIANT FAR&` | [in] | The beam number IDs in VARIANT type. |
| `nProperty` | `const VARIANT FAR&` | [in] | The number ID identifying a property. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OS: OK. |
| `-106` | nBeamNo array dimension error. |
| `-3006` | Invalid member number ID(s). |
| `-6001` | Invalid section The assigned section property ID. |
| `-6002` | Library Error: Property Assign. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AssignBeamProperty(System.Object, System.Object)`

**Main adapter targets:** [AssignBeamProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1091), [AssignBeamProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1098), [AssignSectionProperties](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1518), [AssignSectionProperties](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1527), [AssignSectionProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1536), [AssignSectionProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1631)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `AssignBeamProperty(PropertyAssignmentRequest request)`<br>
**Proposed named result:** `PropertyAssignmentResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-assignmemberspectobeam"></a>

### 135. AssignMemberSpecToBeam

**Section:** Section: Assign Section to Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_assign_section.html#ga04773e6f7e7e123337dd8808525659d6) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_assign_section.html#ga04773e6f7e7e123337dd8808525659d6) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#ga04773e6f7e7e123337dd8808525659d6)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P5 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AssignMemberSpecToBeam(const VARIANT FAR&varnBeamNo,const VARIANT FAR&varnSpecNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID(s) in VARIANT array. |
| `varnSpecNo` | `const VARIANT FAR&` | [in] | The specification number ID. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK |
| `-106` | 1 dimensional array of long expected. |
| `-6017` | Library Error: Unable to assign specification. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AssignMemberSpecToBeam(System.Object, System.Object)`

**Main adapter targets:** [AssignMemberEndOffsets](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L734), [AssignMemberEndOffsets](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L735), [AssignMemberEndOffset](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L754), [AssignMemberEndOffsets](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L784), [AssignMemberEndOffsets](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L785), [AssignMemberEndOffset](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L807), [AssignOffsetSpecifications](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1554), [CreateAndAssignMemberEndReleases](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1586), [CreateAndAssignMemberEndReleases](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1591), [AssignOffsetSpecification](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1668), [CreateAndAssignMemberEndReleases](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1712), [CreateAndAssignMemberEndReleases](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1717)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `AssignMemberSpecToBeam(PropertyAssignmentRequest request)`<br>
**Proposed named result:** `PropertyAssignmentResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-assignplatethickness"></a>

### 136. AssignPlateThickness

**Section:** Section: Assign Section to Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_assign_section.html#ga1a61c38085b3e44b4017ad4b5fba7329) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_assign_section.html#ga1a61c38085b3e44b4017ad4b5fba7329) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#ga1a61c38085b3e44b4017ad4b5fba7329)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P5 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AssignPlateThickness(const VARIANT FAR&nPlateNo,const VARIANT FAR&nProperty)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nPlateNo` | `const VARIANT FAR&` | [in] | The plate number ID/s. (VARIANT / VARIANT Array). |
| `nProperty` | `const VARIANT FAR&` | [in] | The assigned section property ID. (Long) |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | Error. |
| `-106` | nPlateNo array dimension error. |
| `-113` | nPlateNo type error (Long or Int Expected) |
| `-4009` | All the plate numbers are invalid. |
| `-4008` | Some of the plate numbers are invalid. |
| `-6001` | The assigned section property ID is invalid. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AssignPlateThickness(System.Object, System.Object)`

**Main adapter targets:** [AssignPlateElementThickness](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1185), [AssignThicknessProperties](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1545), [AssignThicknessProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1649)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `AssignPlateThickness(PropertyAssignmentRequest request)`<br>
**Proposed named result:** `PropertyAssignmentResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-assignparametricsurfacethickness"></a>

### 137. AssignParametricSurfaceThickness

**Section:** Section: Assign Section to Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_assign_section.html#ga452172a2ca1781a313b638e1e92f1d05) (summary only) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_assign_section.html#ga452172a2ca1781a313b638e1e92f1d05) (summary only) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#ga452172a2ca1781a313b638e1e92f1d05) (summary only)<br>
**Classification:** Reserved: do not use<br>
**Current typed coverage:** None.<br>
**Plan:** P0 — Reserved: do not use

```cpp
VARIANT OSPropertyUI::AssignParametricSurfaceThickness(const VARIANT FAR&nPlateNo,const VARIANT FAR&nProperty)
```

**Summary-only contract:** This function is reserved, do not use.
There is no detailed parameter/return contract. The source explicitly reserves this entry; do not invent a callable public method.

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AssignParametricSurfaceThickness(System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Public API decision:** Reserved: do not use. No new managed entry proposed.

<a id="property-assignelementspectoplate"></a>

### 138. AssignElementSpecToPlate

**Section:** Section: Assign Section to Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_assign_section.html#ga7cfac9861855bd012a1c0e9ab75577bd) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_assign_section.html#ga7cfac9861855bd012a1c0e9ab75577bd) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#ga7cfac9861855bd012a1c0e9ab75577bd)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P5 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AssignElementSpecToPlate(const VARIANT FAR&varnPlateNo,const VARIANT FAR&varnSpecNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnPlateNo` | `const VARIANT FAR&` | [in] | The plate number ID(s) in VARIANT array. |
| `varnSpecNo` | `const VARIANT FAR&` | [in] | The specification number ID. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK |
| `-106` | 1 dimensional array of long expected. |
| `-6017` | Library Error: Unable to assign specification. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AssignElementSpecToPlate(System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `AssignElementSpecToPlate(PropertyAssignmentRequest request)`<br>
**Proposed named result:** `PropertyAssignmentResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-updatepropertiestodesignsection"></a>

### 139. UpdatePropertiesToDesignSection

**Section:** Section: Assign Section to Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_assign_section.html#ga0ee52e9e4368ba1a8ac84bfff8c336dd) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_assign_section.html#ga0ee52e9e4368ba1a8ac84bfff8c336dd) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#ga0ee52e9e4368ba1a8ac84bfff8c336dd)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P5 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::UpdatePropertiesToDesignSection()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | if assignment is successful. |
| `0` | if assignment is unsuccessful. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object UpdatePropertiesToDesignSection()`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `UpdatePropertiesToDesignSection()`<br>
**Proposed named result:** `SpecificationQueryResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createisotropicmaterialproperties"></a>

### 140. CreateIsotropicMaterialProperties

**Section:** Material: Create Material Information Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_mat.html#ga21f578122b014d7b963ac9cbffb0cd33) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_mat.html#ga21f578122b014d7b963ac9cbffb0cd33) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_mat.html#ga21f578122b014d7b963ac9cbffb0cd33)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P5 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateIsotropicMaterialProperties(const VARIANT&strName,const VARIANT&varE,const VARIANT&varPoisson,const VARIANT&varG,const VARIANT&varDensity,const VARIANT&varAlpha,const VARIANT&varCrDamp)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strName` | `const VARIANT&` | [in] | Material Name. |
| `varE` | `const VARIANT&` | [out] | Modulus of elasticity ( E ) VARIANT array (of size 3). |
| `varPoisson` | `const VARIANT&` | [out] | Poisson's ratio ( POI ) VARIANT array (of size 3). |
| `varG` | `const VARIANT&` | [out] | Shear modulus ( G ) VARIANT array (of size 3). |
| `varDensity` | `const VARIANT&` | [out] | Weight density ( DEN ) VARIANT array (of size 3). |
| `varAlpha` | `const VARIANT&` | [out] | Coefficient of thermal expansion ( ALP ) VARIANT array (of size 3). |
| `varCrDamp` | `const VARIANT&` | [out] | Damping ratio ( DAMP ) VARIANT array (of size 3). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | Material is updated as a material with that name was already present. |
| `0` | Material is created. |
| `-1` | General Error |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateIsotropicMaterialProperties(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** [CreateMaterialProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1446), [CreateMaterialPropertyNoConversion](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1467), [CreateMaterialProperties](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1496), [CreateMaterialProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1507)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateIsotropicMaterialProperties(MaterialDefinitionRequest request)`<br>
**Proposed named result:** `MaterialMutationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createisotropicmaterialpropertiesex"></a>

### 141. CreateIsotropicMaterialPropertiesEx

**Section:** Material: Create Material Information Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_mat.html#ga0bc4eb672546e351191883694a504138) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_mat.html#ga0bc4eb672546e351191883694a504138) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_mat.html#ga0bc4eb672546e351191883694a504138)<br>
**Classification:** Type/meaning clarification<br>
**Current typed coverage:** None.<br>
**Plan:** P5 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateIsotropicMaterialPropertiesEx(const VARIANT&strName,const VARIANT&varE,const VARIANT&varPoisson,const VARIANT&varG,const VARIANT&varDensity,const VARIANT&varAlpha,const VARIANT&varCrDamp,const VARIANT&varFy,const VARIANT&varFu,const VARIANT&varRy,const VARIANT&varRt,const VARIANT&varFcu)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strName` | `const VARIANT&` | [in] | Identification title of material of material.. |
| `varE` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varPoisson` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varG` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varDensity` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varAlpha` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varCrDamp` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varFy` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varFu` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varRy` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varRt` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varFcu` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `dE` | [in] | Modulus of elasticity ( E ). |
| `dPoisson` | [in] | Poisson's ratio ( POI ). |
| `dG` | [in] | Shear modulus ( G ). |
| `dDensity` | [in] | Weight density ( DEN ). |
| `dAlpha` | [in] | Coefficient of thermal expansion ( ALP ). |
| `dCrDamp` | [in] | Damping ratio ( DAMP ). |
| `dFy` | [in] | Yield stress ( Fy ) |
| `dFu` | [in] | Tensile strength ( Fu ). |
| `dRy` | [in] | Yield strength ratio ( Ry ). |
| `dRt` | [in] | Tensile strength ratio ( Rt ). |
| `dFcu` | [in] | Compressive strength ( Fcu ). |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | Material is updated as a material with that name was already present. |
| `0` | Material is created. |
| `-1` | General Error |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateIsotropicMaterialPropertiesEx(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** [CreateMaterialPropertyEx](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1476)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateOrUpdateMaterial(MaterialDefinitionRequest request)`<br>
**Proposed named result:** `MaterialMutationResult`

Native 0 Created, 1 Updated, -1 Error; return Created/Updated and name, not a fabricated material ID. Fcu means compressive strength.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

**2024 versus 2025/2026:**

| Year | Documented contract |
| --- | --- |
| 2024 | Creates isotropic material property extended. Parameters [in] strName Identification title of material of material.. [in] dE Modulus of elasticity ( E ). [in] dPoisson Poisson's ratio ( POI ). [in] dG Shear modulus ( G ). [in] dDensity Weight density ( DEN ). [in] dAlpha Coefficient of thermal expansion ( ALP ). [in] dCrDamp Damping ratio ( DAMP ). [in] dFy Yield stress ( Fy ) [in] dFu Tensile strength ( Fu ). [in] dRy Yield strength ratio ( Ry ). [in] dRt Tensile strength ratio ( Rt ). [in] dFcu Value of Fcu ( Fcu ). Return values 1 Material is updated as a material with that name was already present. 0 Material is created. -1 General Error C++ Syntax VB Syntax See also OSPropertyUI::CreateIsotropicMaterialSteel OSPropertyUI::CreateIsotropicMaterialConcrete OSPropertyUI::CreateIsotropicMaterialTimber OSPropertyUI::CreateIsotropicMaterialProperties OSPropertyUI::CreateIsotropicMaterialAluminum |
| 2025 / 2026 | Creates isotropic material property extended. Parameters [in] strName Identification title of material of material.. [in] dE Modulus of elasticity ( E ). [in] dPoisson Poisson's ratio ( POI ). [in] dG Shear modulus ( G ). [in] dDensity Weight density ( DEN ). [in] dAlpha Coefficient of thermal expansion ( ALP ). [in] dCrDamp Damping ratio ( DAMP ). [in] dFy Yield stress ( Fy ) [in] dFu Tensile strength ( Fu ). [in] dRy Yield strength ratio ( Ry ). [in] dRt Tensile strength ratio ( Rt ). [in] dFcu Compressive strength ( Fcu ). Return values 1 Material is updated as a material with that name was already present. 0 Material is created. -1 General Error C++ Syntax VB Syntax See also OSPropertyUI::CreateIsotropicMaterialSteel OSPropertyUI::CreateIsotropicMaterialConcrete OSPropertyUI::CreateIsotropicMaterialTimber OSPropertyUI::CreateIsotropicMaterialProperties OSPropertyUI::CreateIsotropicMaterialAluminum |

<a id="property-createisotropicmaterialsteel"></a>

### 142. CreateIsotropicMaterialSteel

**Section:** Material: Create Material Information Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_mat.html#ga6d780606438b7dd9a2621feeb2addc46) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_mat.html#ga6d780606438b7dd9a2621feeb2addc46) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_mat.html#ga6d780606438b7dd9a2621feeb2addc46)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P5 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateIsotropicMaterialSteel(LPCSTR strName,const VARIANT&dE,const VARIANT&dPoisson,const VARIANT&dG,const VARIANT&dDensity,const VARIANT&dAlpha,const VARIANT&dCrDamp,const VARIANT&dfu,const VARIANT&dfy,const VARIANT&drt,const VARIANT&dry,const VARIANT&bPhysical)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strName` | `LPCSTR` | [in] | Identification title of material. |
| `dE` | `const VARIANT&` | [in] | Modulus of elasticity ( E ). |
| `dPoisson` | `const VARIANT&` | [in] | Poisson's ratio ( POI ). |
| `dG` | `const VARIANT&` | [in] | Shear modulus ( G ). |
| `dDensity` | `const VARIANT&` | [in] | Weight density ( DEN ). |
| `dAlpha` | `const VARIANT&` | [in] | Coefficient of thermal expansion ( ALP ). |
| `dCrDamp` | `const VARIANT&` | [in] | Damping ratio ( DAMP ). |
| `dfu` | `const VARIANT&` | [in] | Tensile strength ( Fu ). |
| `dfy` | `const VARIANT&` | [in] | Yield stress ( Fy ). |
| `drt` | `const VARIANT&` | [in] | Tensile strength ratio ( Rt ). |
| `dry` | `const VARIANT&` | [in] | Yield strength ratio ( Ry ). |
| `bPhysical` | `const VARIANT&` | [in] | Indetifies if the material is for physical member |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | Material is updated as a material with that name was already present. |
| `0` | Material is created. |
| `-1` | General Error |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateIsotropicMaterialSteel(System.String, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateIsotropicMaterialSteel(MaterialDefinitionRequest request)`<br>
**Proposed named result:** `MaterialMutationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createisotropicmaterialconcrete"></a>

### 143. CreateIsotropicMaterialConcrete

**Section:** Material: Create Material Information Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_mat.html#gae3f66093596a98c0a09a9483c1fc61db) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_mat.html#gae3f66093596a98c0a09a9483c1fc61db) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_mat.html#gae3f66093596a98c0a09a9483c1fc61db)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P5 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateIsotropicMaterialConcrete(LPCSTR strName,const VARIANT&dE,const VARIANT&dPoisson,const VARIANT&dG,const VARIANT&dDensity,const VARIANT&dAlpha,const VARIANT&dCrDamp,const VARIANT&dfcu,const VARIANT&bPhysical)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strName` | `LPCSTR` | [in] | Identification title of material. |
| `dE` | `const VARIANT&` | [in] | Modulus of elasticity ( E ). |
| `dPoisson` | `const VARIANT&` | [in] | Poisson's ratio ( POI ). |
| `dG` | `const VARIANT&` | [in] | Shear modulus ( G ). |
| `dDensity` | `const VARIANT&` | [in] | Weight density ( DEN ). |
| `dAlpha` | `const VARIANT&` | [in] | Coefficient of thermal expansion ( ALP ). |
| `dCrDamp` | `const VARIANT&` | [in] | Damping ratio ( DAMP ). |
| `dfcu` | `const VARIANT&` | [in] | Compressive strength ( Fcu ). |
| `bPhysical` | `const VARIANT&` | [in] | Indetifies if the material is for physical member |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | Material is updated as a material with that name was already present. |
| `0` | Material is created. |
| `-1` | General Error |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateIsotropicMaterialConcrete(System.String, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateIsotropicMaterialConcrete(MaterialDefinitionRequest request)`<br>
**Proposed named result:** `MaterialMutationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createisotropicmaterialaluminum"></a>

### 144. CreateIsotropicMaterialAluminum

**Section:** Material: Create Material Information Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_mat.html#ga9edf9ae98088d07e28177a6ce0f7e915) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_mat.html#ga9edf9ae98088d07e28177a6ce0f7e915) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_mat.html#ga9edf9ae98088d07e28177a6ce0f7e915)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P5 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateIsotropicMaterialAluminum(LPCSTR strName,const VARIANT&varE,const VARIANT&varPoisson,const VARIANT&varG,const VARIANT&varDensity,const VARIANT&varAlpha,const VARIANT&varCrDamp,const VARIANT&varPhysical)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strName` | `LPCSTR` | [in] | Identification title of material. |
| `varE` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varPoisson` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varG` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varDensity` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varAlpha` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varCrDamp` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varPhysical` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `dE` | [in] | Modulus of elasticity ( E ). |
| `dPoisson` | [in] | Poisson's ratio ( POI ). |
| `dG` | [in] | Shear modulus ( G ). |
| `dDensity` | [in] | Weight density ( DEN ). |
| `dAlpha` | [in] | Coefficient of thermal expansion ( ALP ). |
| `dCrDamp` | [in] | Damping ratio ( DAMP ). |
| `bPhysical` | [in] | Indetifies if the material is for physical member |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | Material is updated as a material with that name was already present. |
| `0` | Material is created. |
| `-1` | General Error |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateIsotropicMaterialAluminum(System.String, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateIsotropicMaterialAluminum(MaterialDefinitionRequest request)`<br>
**Proposed named result:** `MaterialMutationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createisotropicmaterialtimber"></a>

### 145. CreateIsotropicMaterialTimber

**Section:** Material: Create Material Information Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_mat.html#ga120025647e015ec700e4b19452f35e92) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_mat.html#ga120025647e015ec700e4b19452f35e92) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_mat.html#ga120025647e015ec700e4b19452f35e92)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P5 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::CreateIsotropicMaterialTimber(LPCSTR strName,const VARIANT&varE,const VARIANT&varPoisson,const VARIANT&varG,const VARIANT&varDensity,const VARIANT&varAlpha,const VARIANT&varCrDamp,const VARIANT&varPhysical)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strName` | `LPCSTR` | [in] | Identification title of material. |
| `varE` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varPoisson` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varG` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varDensity` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varAlpha` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varCrDamp` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |
| `varPhysical` | `const VARIANT&` | Not named in parameter table | No matching parameter-table description. |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `dE` | [in] | Modulus of elasticity ( E ). |
| `dPoisson` | [in] | Poisson's ratio ( POI ). |
| `dG` | [in] | Shear modulus ( G ). |
| `dDensity` | [in] | Weight density ( DEN ). |
| `dAlpha` | [in] | Coefficient of thermal expansion ( ALP ). |
| `dCrDamp` | [in] | Damping ratio ( DAMP ). |
| `bPhysical` | [in] | Indetifies if the material is for physical member |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | Material is updated as a material with that name was already present. |
| `0` | Material is created. |
| `-1` | General Error |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object CreateIsotropicMaterialTimber(System.String, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateIsotropicMaterialTimber(MaterialDefinitionRequest request)`<br>
**Proposed named result:** `MaterialMutationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getisotropicmaterialassignedbeamcount"></a>

### 146. GetIsotropicMaterialAssignedBeamCount

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#gae487189c55613e48e06b63ef9669c0fb) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#gae487189c55613e48e06b63ef9669c0fb) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gae487189c55613e48e06b63ef9669c0fb)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<int> GetIsotropicMaterialAssignedMemberCount(string materialName)`.<br>
**Plan:** P3 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetIsotropicMaterialAssignedBeamCount(const VARIANT&strMaterialName)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strMaterialName` | `const VARIANT&` | [in] | Identification title of the material. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-6023` | Material not found. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetIsotropicMaterialAssignedBeamCount(System.Object)`

**Main adapter targets:** [GetIsotropicMaterialAssignedBeams](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L440)<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<int> GetIsotropicMaterialAssignedMemberCount(string materialName)`<br>
**Implemented result:** `PropertyQueryResult<int>`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getisotropicmaterialassignedbeamlist"></a>

**Implemented behavior and alternatives:** Use for sizing member assignments. GetIsotropicMaterialCount counts material definitions; GetIsotropicMaterialAssignedMemberIds returns the member IDs. A material name is not an index or section/property ID. Nonnegative values are treated as counts following the native purpose and read-only main-source usage; the help table incorrectly lists only 0 OK and its example passes a number despite the named input. Zero is a valid empty count; -6023 is NotFound. This count interpretation remains a live qualification requirement.

**Current native call sites:** [AssignedCount](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L225). See [usage and qualification](../Property-Porting.md#getisotropicmaterialassignedmembercount).

### 147. GetIsotropicMaterialAssignedBeamList

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#ga40e7abe1185d79a7053ab5f5b7f19555) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#ga40e7abe1185d79a7053ab5f5b7f19555) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga40e7abe1185d79a7053ab5f5b7f19555)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<IEnumerable<int>> GetIsotropicMaterialAssignedMemberIds(string materialName, int maximumResults = 100000)`.<br>
**Plan:** P3 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetIsotropicMaterialAssignedBeamList(const VARIANT&strMaterialName,VARIANT FAR&nBeamList)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strMaterialName` | `const VARIANT&` | [in] | Identification title of the material. |
| `nBeamList` | `VARIANT FAR&` | [out] | List of beam. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-6023` | Material not found. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetIsotropicMaterialAssignedBeamList(System.Object, System.Object ByRef)`

**Main adapter targets:** [GetIsotropicMaterialAssignedBeams](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L443)<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<IEnumerable<int>> GetIsotropicMaterialAssignedMemberIds(string materialName, int maximumResults = 100000)`<br>
**Implemented result:** `PropertyQueryResult<IEnumerable<int>>`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getmaterialproperty"></a>

**Implemented behavior and alternatives:** Use when native assignment IDs are required without preloading Beam objects. The main source convenience filters a caller-supplied Beam collection; this query returns every native listed ID, preserving order and duplicates. It sizes with GetIsotropicMaterialAssignedBeamCount, validates positive Int32 IDs and exact array shape, and rechecks the count. Native list return 0 succeeds and -6023 is NotFound. Zero count returns an empty read-only snapshot without fetching; a larger count than maximumResults returns LimitExceeded before allocation. Count interpretation follows the native purpose/main usage despite the inconsistent help table. Same-count replacement or concurrent changes through other receivers cannot be proved absent.

**Current native call sites:** [GetIsotropicMaterialAssignedMemberIds](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L251). See [usage and qualification](../Property-Porting.md#getisotropicmaterialassignedmemberids).

### 148. GetMaterialProperty

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#ga90b6809ba31300363321cce519f0b9c3) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#ga90b6809ba31300363321cce519f0b9c3) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga90b6809ba31300363321cce519f0b9c3)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<MaterialProperties> GetMaterialProperty(string materialName)`.<br>
**Plan:** P3 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetMaterialProperty(const VARIANT FAR&varstrMaterialName,VARIANT FAR&vardElasticity,VARIANT FAR&vardPoisson,VARIANT FAR&vardDensity,VARIANT FAR&vardAlpha,VARIANT FAR&vardDamp)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varstrMaterialName` | `const VARIANT FAR&` | [in] | Identification title of the material. |
| `vardElasticity` | `VARIANT FAR&` | [out] | Modulus of elasticity ( E ). |
| `vardPoisson` | `VARIANT FAR&` | [out] | Poisson's ratio ( POI ). |
| `vardDensity` | `VARIANT FAR&` | [out] | Weight density ( DEN ). |
| `vardAlpha` | `VARIANT FAR&` | [out] | Coefficient of thermal expansion ( ALP ). |
| `vardDamp` | `VARIANT FAR&` | [out] | Damping ratio ( DAMP ). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-6023` | Material not found. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetMaterialProperty(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** [GetMaterialProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L180)<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<MaterialProperties> GetMaterialProperty(string materialName)`<br>
**Implemented result:** `PropertyQueryResult<MaterialProperties>`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getbeammaterialname"></a>

**Implemented behavior and alternatives:** Use an exact material name obtained from an entity or native material observation. GetIsotropicMaterialProperty instead uses a zero-based list index and supplies native G; GetExtendedMaterialProperty also supplies strength fields. Native return 0 succeeds and -6023 is NotFound. These are different success semantics from the Boolean extended function. ShearModulus, Strengths and AssignmentState are null because this function does not return them. No default engineering values or computed G are substituted. Density remains weight per volume.

**Current native call sites:** [NamedMaterial](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L273). See [usage and qualification](../Property-Porting.md#getmaterialproperty).

### 149. GetBeamMaterialName

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#gade8859bea46c3bccd85204f73593e0b0) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#gade8859bea46c3bccd85204f73593e0b0) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gade8859bea46c3bccd85204f73593e0b0)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<string> GetBeamMaterialName(int memberId)`.<br>
**Plan:** P3 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetBeamMaterialName(const VARIANT FAR&varnBeamNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID. |


**Native return description:** The beam material string name.

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetBeamMaterialName(System.Object)`

**Main adapter targets:** [GetAllMaterialPropertiesWithPopulatedAssignments](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L105), [GetBeamMaterialName](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L199)<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<string> GetBeamMaterialName(int memberId)`<br>
**Implemented result:** `PropertyQueryResult<string>`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getelementmaterialname"></a>

**Implemented behavior and alternatives:** Use for name discovery before a named material query. This returns a name, not constants, an index, a property ID or a new assignment. Native spelling/case/whitespace are preserved. Empty or native-null text is EmptyOrUnavailable because the help supplies no way to distinguish no assignment from a missing/unavailable member. Non-string responses are rejected; no guessed material name is returned.

**Current native call sites:** [EntityMaterialName](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L208). See [usage and qualification](../Property-Porting.md#getbeammaterialname).

### 150. GetElementMaterialName

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#ga44859f04996f6facbe39d62d67594b8c) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#ga44859f04996f6facbe39d62d67594b8c) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga44859f04996f6facbe39d62d67594b8c)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<string> GetElementMaterialName(int plateId)`.<br>
**Plan:** P3 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetElementMaterialName(const VARIANT FAR&varnPlateNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnPlateNo` | `const VARIANT FAR&` | [in] | The plate number ID. |


**Native return description:** The entity material string name.

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetElementMaterialName(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<string> GetElementMaterialName(int plateId)`<br>
**Implemented result:** `PropertyQueryResult<string>`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getisotropicmaterialcount"></a>

**Implemented behavior and alternatives:** This native element query takes a plate ID. Use GetBeamMaterialName for members; do not pass a node, solid ID or material-list index. It returns the exact name, not constants or thickness. Empty/native-null text is EmptyOrUnavailable because the contract does not distinguish unassigned and unavailable entities. No fallback to a similarly named plate/material API is attempted.

**Current native call sites:** [EntityMaterialName](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L208). See [usage and qualification](../Property-Porting.md#getelementmaterialname).

### 151. GetIsotropicMaterialCount

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#gaa8e5a0c6288ae8d53eded96a1fb0c114) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#gaa8e5a0c6288ae8d53eded96a1fb0c114) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gaa8e5a0c6288ae8d53eded96a1fb0c114)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<int> GetIsotropicMaterialCount()`.<br>
**Plan:** P3 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetIsotropicMaterialCount()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The number of isotropic material. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetIsotropicMaterialCount()`

**Main adapter targets:** [GetAllIsotropicMaterialProperties](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L331)<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<int> GetIsotropicMaterialCount()`<br>
**Implemented result:** `PropertyQueryResult<int>`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getisotropicmaterialproperties"></a>

**Implemented behavior and alternatives:** Use to bound the zero-based materialIndex accepted by GetIsotropicMaterialProperty and GetExtendedIsotropicMaterialProperty. This is not a member/plate assignment count. Zero is a valid empty definition inventory; undocumented negative or nonintegral counts are rejected. A count is only an observation, not a stable reservation of material identities.

**Current native call sites:** [GetIsotropicMaterialCount](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L217); [IndexedMaterial](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L293); [IndexedMaterial](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L303). See [usage and qualification](../Property-Porting.md#getisotropicmaterialcount).

### 152. GetIsotropicMaterialProperties

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#gaeead1ecb0ed0c1587d432a1e3b9887b5) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#gaeead1ecb0ed0c1587d432a1e3b9887b5) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gaeead1ecb0ed0c1587d432a1e3b9887b5)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<MaterialProperties> GetIsotropicMaterialProperty(int materialIndex)`.<br>
**Plan:** P3 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetIsotropicMaterialProperties(const VARIANT FAR&varMatNo,VARIANT FAR&varE,VARIANT FAR&varPoisson,VARIANT FAR&varG,VARIANT FAR&varDensity,VARIANT FAR&varAlpha,VARIANT FAR&varCrDamp)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varMatNo` | `const VARIANT FAR&` | [in] | Zero based index of the material. |
| `varE` | `VARIANT FAR&` | [out] | Modulus of elasticity ( E ). |
| `varPoisson` | `VARIANT FAR&` | [out] | Poisson's ratio ( POI ). |
| `varG` | `VARIANT FAR&` | [out] | Shear modulus ( G ). |
| `varDensity` | `VARIANT FAR&` | [out] | Weight density ( DEN ). |
| `varAlpha` | `VARIANT FAR&` | [out] | Coefficient of thermal expansion ( ALP ). |
| `varCrDamp` | `VARIANT FAR&` | [out] | Damping ratio ( DAMP ). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `VARIANT` | Material string name. |
| `NULL` | Cannot find material varMatNo . |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetIsotropicMaterialProperties(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** [GetIsotropicMaterialProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L374)<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<MaterialProperties> GetIsotropicMaterialProperty(int materialIndex)`<br>
**Implemented result:** `PropertyQueryResult<MaterialProperties>`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getmaterialpropertyex"></a>

**Implemented behavior and alternatives:** Use for material enumeration, unlike GetMaterialProperty which selects by name. materialIndex is zero-based, not a native property ID or member ID. This function returns G directly; that native value is retained instead of the main MaterialProperty computed E/(2*(1+Poisson)) substitute. The native return is the material name, not a success flag. Null/empty name or an index outside the observed count is NotFound. Before/after material counts detect observed changes but not same-count reorderings. Strengths and AssignmentState are null.

**Current native call sites:** [IndexedMaterial](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L298). See [usage and qualification](../Property-Porting.md#getisotropicmaterialproperty).

### 153. GetMaterialPropertyEx

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#ga02bbaecea45e8ab88558707f439acbe1) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#ga02bbaecea45e8ab88558707f439acbe1) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga02bbaecea45e8ab88558707f439acbe1)<br>
**Classification:** Type/meaning clarification<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<MaterialProperties> GetExtendedMaterialProperty(string materialName)`.<br>
**Plan:** P3 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetMaterialPropertyEx(const VARIANT FAR&varstrMaterialName,VARIANT FAR&vardElasticity,VARIANT FAR&vardPoisson,VARIANT FAR&vardDensity,VARIANT FAR&vardAlpha,VARIANT FAR&vardDamp,VARIANT FAR&varFy,VARIANT FAR&varFu,VARIANT FAR&varRy,VARIANT FAR&varRt,VARIANT FAR&varFcu)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varstrMaterialName` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `vardElasticity` | `VARIANT FAR&` | [out] | Modulus of elasticity (Type:double)( E ). |
| `vardPoisson` | `VARIANT FAR&` | [out] | Poisson's ratio (Type:double)( POI ). |
| `vardDensity` | `VARIANT FAR&` | [out] | Weight density (Type:double)( DEN ). |
| `vardAlpha` | `VARIANT FAR&` | [out] | Coefficient of thermal expansion (Type:double)( ALP ). |
| `vardDamp` | `VARIANT FAR&` | [out] | Damping ratio (Type:double)( DAMP ). |
| `varFy` | `VARIANT FAR&` | [out] | Yield Stress (Type:double)( Fy ). |
| `varFu` | `VARIANT FAR&` | [out] | Tensile Strength (Type:double)( Fu ). |
| `varRy` | `VARIANT FAR&` | [out] | Yield Strength Ratio (Type:double)( Ry ). |
| `varRt` | `VARIANT FAR&` | [out] | Tensile Strength Ratio (Type:double)( Rt ). |
| `varFcu` | `VARIANT FAR&` | [out] | Compressive Strength (Type:double)( Fcu ). |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `varstrMaterial` | [in] | Name material name(Type:string). |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE` | Get Material Property Successful. |
| `FALSE` | Get Material Property Failed. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetMaterialPropertyEx(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** [GetExtendedMaterialProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L151)<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<MaterialProperties> GetExtendedMaterialProperty(string materialName)`<br>
**Implemented result:** `PropertyQueryResult<MaterialProperties>`

Status plus extended outputs; Fcu compressive strength, name-based form does not expose G. Do not invent a zero shear modulus.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

**2024 versus 2025/2026:**

| Year | Documented contract |
| --- | --- |
| 2024 | Get the properties for the specified isotropic material Name. Parameters [in] varstrMaterial Name material name(Type:string). [out] vardElasticity Modulus of elasticity (Type:double)( E ). [out] vardPoisson Poisson's ratio (Type:double)( POI ). [out] vardDensity Weight density (Type:double)( DEN ). [out] vardAlpha Coefficient of thermal expansion (Type:double)( ALP ). [out] vardDamp Damping ratio (Type:double)( DAMP ). [out] varFy Yield Stress (Type:double)( Fy ). [out] varFu Tensile Strength (Type:double)( Fu ). [out] varRy Yield Strength Ratio (Type:double)( Ry ). [out] varRt Tensile Strength Ratio (Type:double)( Rt ). [out] varFcu Yield Stress (Type:double)( Fcu ). Return values TRUE Get Material Property Successful. FALSE Get Material Property Failed. C++ Syntax VBA Syntax See also OSPropertyUI::GetIsotropicMaterialCount() |
| 2025 / 2026 | Get the properties for the specified isotropic material Name. Parameters [in] varstrMaterial Name material name(Type:string). [out] vardElasticity Modulus of elasticity (Type:double)( E ). [out] vardPoisson Poisson's ratio (Type:double)( POI ). [out] vardDensity Weight density (Type:double)( DEN ). [out] vardAlpha Coefficient of thermal expansion (Type:double)( ALP ). [out] vardDamp Damping ratio (Type:double)( DAMP ). [out] varFy Yield Stress (Type:double)( Fy ). [out] varFu Tensile Strength (Type:double)( Fu ). [out] varRy Yield Strength Ratio (Type:double)( Ry ). [out] varRt Tensile Strength Ratio (Type:double)( Rt ). [out] varFcu Compressive Strength (Type:double)( Fcu ). Return values TRUE Get Material Property Successful. FALSE Get Material Property Failed. C++ Syntax VBA Syntax See also OSPropertyUI::GetIsotropicMaterialCount() |

<a id="property-getisotropicmaterialpropertiesex"></a>

**Implemented behavior and alternatives:** Choose over GetMaterialProperty when Fy/Fu/Ry/Rt/Fcu are needed. GetExtendedIsotropicMaterialProperty selects a zero-based index and additionally returns native G; this name-based function omits G, so ShearModulus is null. Unlike basic named GetMaterialProperty, native Boolean true means success and false means failure. Strength stresses retain native force/area units; Ry/Rt are dimensionless. No material classification, design pass/fail, default strength or computed shear modulus is inferred.

**Current native call sites:** [NamedMaterial](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L272). See [usage and qualification](../Property-Porting.md#getextendedmaterialproperty).

### 154. GetIsotropicMaterialPropertiesEx

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#gab82a93e375ef05e9f893b5ba272b7ad5) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#gab82a93e375ef05e9f893b5ba272b7ad5) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gab82a93e375ef05e9f893b5ba272b7ad5)<br>
**Classification:** Type/meaning clarification<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<MaterialProperties> GetExtendedIsotropicMaterialProperty(int materialIndex)`.<br>
**Plan:** P3 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetIsotropicMaterialPropertiesEx(const VARIANT FAR&varMatNo,VARIANT FAR&varE,VARIANT FAR&varPoisson,VARIANT FAR&varG,VARIANT FAR&varDensity,VARIANT FAR&varAlpha,VARIANT FAR&varCrDamp,VARIANT FAR&varFy,VARIANT FAR&varFu,VARIANT FAR&varRy,VARIANT FAR&varRt,VARIANT FAR&varFcu)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varMatNo` | `const VARIANT FAR&` | [in] | Zero based index of the material(Type:Long). |
| `varE` | `VARIANT FAR&` | [out] | Modulus of elasticity,(Type:double) ( E ). |
| `varPoisson` | `VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varG` | `VARIANT FAR&` | [out] | Shear modulus (Type:double)( G ). |
| `varDensity` | `VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varAlpha` | `VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varCrDamp` | `VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varFy` | `VARIANT FAR&` | [out] | Yield Stress(Type:double) ( Fy ). |
| `varFu` | `VARIANT FAR&` | [out] | Tensile Strength(Type:double) ( Fu ). |
| `varRy` | `VARIANT FAR&` | [out] | Yield Strength Ratio(Type:double) ( Ry ). |
| `varRt` | `VARIANT FAR&` | [out] | Tensile Strength Ratio(Type:double) ( Rt ). |
| `varFcu` | `VARIANT FAR&` | [out] | Compressive Strength(Type:double) ( Fcu ). |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `vardPoisson` | [out] | Poisson's ratio(Type:double) ( POI ). |
| `vardDensity` | [out] | Weight density (Type:double)( DEN ). |
| `vardAlpha` | [out] | Coefficient of thermal expansion(Type:double) ( ALP ). |
| `vardDamp` | [out] | Damping ratio (Type:double)( DAMP ). |

**Native return description:** The specified material Name.

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetIsotropicMaterialPropertiesEx(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** [GetExtendedIsotropicMaterialProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L348)<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<MaterialProperties> GetExtendedIsotropicMaterialProperty(int materialIndex)`<br>
**Implemented result:** `PropertyQueryResult<MaterialProperties>`

Zero-based index; native returns material name with eleven outputs, not an ID or Boolean. Fcu compressive strength.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

**2024 versus 2025/2026:**

| Year | Documented contract |
| --- | --- |
| 2024 | Get the properties for the specified isotropic material number. Parameters [in] varMatNo Zero based index of the material(Type:Long). [out] varE Modulus of elasticity,(Type:double) ( E ). [out] vardPoisson Poisson's ratio(Type:double) ( POI ). [out] varG Shear modulus (Type:double)( G ). [out] vardDensity Weight density (Type:double)( DEN ). [out] vardAlpha Coefficient of thermal expansion(Type:double) ( ALP ). [out] vardDamp Damping ratio (Type:double)( DAMP ). [out] varFy Yield Stress(Type:double) ( Fy ). [out] varFu Tensile Strength(Type:double) ( Fu ). [out] varRy Yield Strength Ratio(Type:double) ( Ry ). [out] varRt Tensile Strength Ratio(Type:double) ( Rt ). [out] varFcu Yield Stress(Type:double) ( Fcu ). Returns The specified material Name. C++ Syntax VBA Syntax See also OSPropertyUI::GetIsotropicMaterialCount() |
| 2025 / 2026 | Get the properties for the specified isotropic material number. Parameters [in] varMatNo Zero based index of the material(Type:Long). [out] varE Modulus of elasticity,(Type:double) ( E ). [out] vardPoisson Poisson's ratio(Type:double) ( POI ). [out] varG Shear modulus (Type:double)( G ). [out] vardDensity Weight density (Type:double)( DEN ). [out] vardAlpha Coefficient of thermal expansion(Type:double) ( ALP ). [out] vardDamp Damping ratio (Type:double)( DAMP ). [out] varFy Yield Stress(Type:double) ( Fy ). [out] varFu Tensile Strength(Type:double) ( Fu ). [out] varRy Yield Strength Ratio(Type:double) ( Ry ). [out] varRt Tensile Strength Ratio(Type:double) ( Rt ). [out] varFcu Compressive Strength(Type:double) ( Fcu ). Returns The specified material Name. C++ Syntax VBA Syntax See also OSPropertyUI::GetIsotropicMaterialCount() |

<a id="property-getisotropicmaterialpropertiesassigned"></a>

**Implemented behavior and alternatives:** Use for enumeration with native G and Fy/Fu/Ry/Rt/Fcu. GetExtendedMaterialProperty selects by name and lacks G; GetIsotropicMaterialProperty omits strengths. The native return is the material name, not a Boolean or numeric status. Null/empty name or an out-of-range observed index is NotFound. Coupled counts are checked before/after; no atomic snapshot or stable same-count ordering is promised. All supplied constants and strength fields must be written and finite; optional AssignmentState remains null.

**Current native call sites:** [IndexedMaterial](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L297). See [usage and qualification](../Property-Porting.md#getextendedisotropicmaterialproperty).

### 155. GetIsotropicMaterialPropertiesAssigned

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#ga74438ad3dd2db13772443598f8ee1707) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#ga74438ad3dd2db13772443598f8ee1707) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga74438ad3dd2db13772443598f8ee1707)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<MaterialProperties> GetIsotropicMaterialPropertiesAssigned(int nativeMaterialNumber)`.<br>
**Plan:** P3 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetIsotropicMaterialPropertiesAssigned(const VARIANT FAR&varMatNo,VARIANT FAR&varE,VARIANT FAR&varPoisson,VARIANT FAR&varG,VARIANT FAR&varDensity,VARIANT FAR&varAlpha,VARIANT FAR&varCrDamp,VARIANT FAR&varAssigned)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varMatNo` | `const VARIANT FAR&` | [in] | Material number ID. |
| `varE` | `VARIANT FAR&` | [out] | Modulus of elasticity ( E ). |
| `varPoisson` | `VARIANT FAR&` | [out] | Poisson's ratio ( POI ). |
| `varG` | `VARIANT FAR&` | [out] | Shear modulus ( G ). |
| `varDensity` | `VARIANT FAR&` | [out] | Weight density ( DEN ). |
| `varAlpha` | `VARIANT FAR&` | [out] | Coefficient of thermal expansion ( ALP ). |
| `varCrDamp` | `VARIANT FAR&` | [out] | Damping ratio ( DAMP ). |
| `varAssigned` | `VARIANT FAR&` | [out] | Material assigned to elements or not: unassigned (= 1), assigned (=2). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `VARIANT` | Material string name. |
| `NULL` | Cannot find material varMatNo . |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetIsotropicMaterialPropertiesAssigned(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<MaterialProperties> GetIsotropicMaterialPropertiesAssigned(int nativeMaterialNumber)`<br>
**Implemented result:** `PropertyQueryResult<MaterialProperties>`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getorthotropic2dmaterialcount"></a>

**Implemented behavior and alternatives:** Use when the native assignment observation is required; the ordinary and extended indexed functions do not return it. nativeMaterialNumber is forwarded unchanged: this help says material number ID, unlike the explicitly zero-based indexed methods. Nonnegative input is accepted without guessed rebasing or a count-based ID/index conversion; qualify the selector on the acquired build. Native string name supplies success; null/empty means NotFound. Assignment output 1 means Unassigned and 2 Assigned, not Boolean 0/1; documented Double or integral forms are decoded exactly. Strengths remain null.

**Current native call sites:** [GetIsotropicMaterialPropertiesAssigned](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L315). See [usage and qualification](../Property-Porting.md#getisotropicmaterialpropertiesassigned).

### 156. GetOrthotropic2DMaterialCount

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#ga34d6f21ac2f41814c0a2ca78f0d387ed) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#ga34d6f21ac2f41814c0a2ca78f0d387ed) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga34d6f21ac2f41814c0a2ca78f0d387ed)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<int> GetOrthotropic2DMaterialCount()`.<br>
**Plan:** P3 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetOrthotropic2DMaterialCount()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The number of 2D orthotropic material. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetOrthotropic2DMaterialCount()`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<int> GetOrthotropic2DMaterialCount()`<br>
**Implemented result:** `PropertyQueryResult<int>`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getorthotropic2dmaterialproperties"></a>

**Implemented behavior and alternatives:** Use for the 2D orthotropic inventory, not isotropic or 3D material counts and not element assignments. Zero is valid. The associated property getter documents a native material number rather than an explicit zero-based list index; this count does not establish a selector rebasing rule. Undocumented negative/nonintegral counts are rejected.

**Current native call sites:** [GetOrthotropic2DMaterialCount](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L222). See [usage and qualification](../Property-Porting.md#getorthotropic2dmaterialcount).

### 157. GetOrthotropic2DMaterialProperties

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#ga04b76b162db405e4a289b71f05b7e673) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#ga04b76b162db405e4a289b71f05b7e673) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga04b76b162db405e4a289b71f05b7e673)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<Orthotropic2DMaterialProperties> GetOrthotropic2DMaterialProperties(int nativeMaterialNumber)`.<br>
**Plan:** P3 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetOrthotropic2DMaterialProperties(const VARIANT FAR&varMatNo,VARIANT FAR&varE,VARIANT FAR&varPoisson,VARIANT FAR&varG,VARIANT FAR&varDensity,VARIANT FAR&varAlpha,VARIANT FAR&varCrDamp)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varMatNo` | `const VARIANT FAR&` | [in] | Material number ID. |
| `varE` | `VARIANT FAR&` | [out] | Modulus of elasticity ( E ) VARIANT array (of size 2). |
| `varPoisson` | `VARIANT FAR&` | [out] | Poisson's ratio ( POI ) VARIANT array (of size 2). |
| `varG` | `VARIANT FAR&` | [out] | Shear modulus ( G ) VARIANT array (of size 3). |
| `varDensity` | `VARIANT FAR&` | [out] | Weight density ( DEN ) VARIANT array (of size 2). |
| `varAlpha` | `VARIANT FAR&` | [out] | Coefficient of thermal expansion ( ALP ) VARIANT array (of size 2). |
| `varCrDamp` | `VARIANT FAR&` | [out] | Damping ratio ( DAMP ) VARIANT array (of size 2). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `VARIANT` | Material string name. |
| `NULL` | Fail to get the properties. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetOrthotropic2DMaterialProperties(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<Orthotropic2DMaterialProperties> GetOrthotropic2DMaterialProperties(int nativeMaterialNumber)`<br>
**Implemented result:** `PropertyQueryResult<Orthotropic2DMaterialProperties>`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getorthotropic3dmaterialcount"></a>

**Implemented behavior and alternatives:** Use for independent directional material constants; isotropic queries cannot substitute for them. nativeMaterialNumber is forwarded unchanged and must be qualified on the acquired build because the help does not establish its base. Native string name indicates success; null/empty is NotFound. Buffer sizes are E=2, Poisson=2, G=3, weight density=2, thermal expansion=2 and damping=2; each output must be finite and owned. Named first/second/third fields preserve native order because the help does not identify physical axis/plane ordering. Do not assume global X/Y or XY/YZ/ZX, derive G, or replace missing values with isotropic defaults.

**Current native call sites:** [GetOrthotropic2DMaterialProperties](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L331). See [usage and qualification](../Property-Porting.md#getorthotropic2dmaterialproperties).

### 158. GetOrthotropic3DMaterialCount

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#gab247088c8327421655e4ad0b53b8a19f) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#gab247088c8327421655e4ad0b53b8a19f) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gab247088c8327421655e4ad0b53b8a19f)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P3 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetOrthotropic3DMaterialCount()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return description:** The orthotropic 3D material count.

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetOrthotropic3DMaterialCount()`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetOrthotropic3DMaterialCount()`<br>
**Proposed named result:** `PropertyCountResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getorthotropic3dmaterialproperties"></a>

### 159. GetOrthotropic3DMaterialProperties

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#ga7be4c04b41e1326016a62cd43ccbf8e6) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#ga7be4c04b41e1326016a62cd43ccbf8e6) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga7be4c04b41e1326016a62cd43ccbf8e6)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P3 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetOrthotropic3DMaterialProperties(const VARIANT FAR&varMatNo,VARIANT FAR&varE,VARIANT FAR&varPoisson,VARIANT FAR&varG,VARIANT FAR&varDensity,VARIANT FAR&varAlpha,VARIANT FAR&varCrDamp)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varMatNo` | `const VARIANT FAR&` | [in] | Material number ID. |
| `varE` | `VARIANT FAR&` | [out] | Modulus of elasticity ( E ) VARIANT array (of size 3). |
| `varPoisson` | `VARIANT FAR&` | [out] | Poisson's ratio ( POI ) VARIANT array (of size 3). |
| `varG` | `VARIANT FAR&` | [out] | Shear modulus ( G ) VARIANT array (of size 3). |
| `varDensity` | `VARIANT FAR&` | [out] | Weight density ( DEN ) VARIANT array (of size 3). |
| `varAlpha` | `VARIANT FAR&` | [out] | Coefficient of thermal expansion ( ALP ) VARIANT array (of size 3). |
| `varCrDamp` | `VARIANT FAR&` | [out] | Damping ratio ( DAMP ) VARIANT array (of size 3). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `VARIANT` | Material string name. |
| `NULL` | Fail to get the properties. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetOrthotropic3DMaterialProperties(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetOrthotropic3DMaterialProperties(MaterialQuery request)`<br>
**Proposed named result:** `MaterialQueryResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-deletematerial"></a>

### 160. DeleteMaterial

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#ga10ded1e9c1480a77783bd89565a1b082) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#ga10ded1e9c1480a77783bd89565a1b082) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga10ded1e9c1480a77783bd89565a1b082)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P3 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::DeleteMaterial(const VARIANT FAR&varMaterialName)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varMaterialName` | `const VARIANT FAR&` | [in] | Material Name(Type:String). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `FALSE` | Delete Material Generate Error. |
| `TRUE` | Delete Material Successful. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object DeleteMaterial(System.Object)`

**Main adapter targets:** [DeleteMaterial](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L71)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `DeleteMaterial(MaterialDefinitionRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removematerialfrombeam"></a>

### 161. RemoveMaterialFromBeam

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#gac084be83e5205fc0f6c766025e063531) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#gac084be83e5205fc0f6c766025e063531) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gac084be83e5205fc0f6c766025e063531)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P3 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::RemoveMaterialFromBeam(const VARIANT FAR&nBeamNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object RemoveMaterialFromBeam(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemoveMaterialFromBeam(MaterialDefinitionRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removebeammaterialhelper"></a>

### 162. RemoveBeamMaterialHelper

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#ga3caee4c622453d9c30f56cc797d04231) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#ga3caee4c622453d9c30f56cc797d04231) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga3caee4c622453d9c30f56cc797d04231)<br>
**Classification:** Native private helper: excluded<br>
**Current typed coverage:** None.<br>
**Plan:** P0 — Native private helper: excluded

```cpp
BOOL OSPropertyUI::RemoveBeamMaterialHelper(long&beamNo)private
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `beamNo` | `long&` | [in] | The beam number ID. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** No matching name in either inspected interface; native availability must be established independently.

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Public API decision:** Native private helper: excluded. No new managed entry proposed.

<a id="property-getplatematerialname"></a>

### 163. GetPlateMaterialName

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#ga0a6b8e8cdb65ed2b9747e95d7fb7706a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#ga0a6b8e8cdb65ed2b9747e95d7fb7706a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga0a6b8e8cdb65ed2b9747e95d7fb7706a)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P3 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetPlateMaterialName(const VARIANT FAR&varnPlateNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnPlateNo` | `const VARIANT FAR&` | [in] | The plate number ID. |


**Native return description:** The plate material string name.

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetPlateMaterialName(System.Object)`

**Main adapter targets:** [GetAllMaterialPropertiesWithPopulatedAssignments](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L109), [GetPlateMaterialName](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L204)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetPlateMaterialName(MaterialQuery request)`<br>
**Proposed named result:** `MaterialNameResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getisotropicmaterialassignedplatecount"></a>

### 164. GetIsotropicMaterialAssignedPlateCount

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#ga2eaaa9ee8060d95d5f4c587ac45860e5) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#ga2eaaa9ee8060d95d5f4c587ac45860e5) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga2eaaa9ee8060d95d5f4c587ac45860e5)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P3 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetIsotropicMaterialAssignedPlateCount(const VARIANT&strMaterialName)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strMaterialName` | `const VARIANT&` | [in] | Material Name(Type: String). |


**Native return description:** Count of plates assigned with the specific isotropic material(Type: Long).

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetIsotropicMaterialAssignedPlateCount(System.Object)`

**Main adapter targets:** [GetIsotropicMaterialAssignedPlates](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L408)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetIsotropicMaterialAssignedPlateCount(MaterialQuery request)`<br>
**Proposed named result:** `PropertyCountResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getsolidmaterialname"></a>

### 165. GetSolidMaterialName

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#ga9b909a32be274f8a4a797ea85bdf2340) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#ga9b909a32be274f8a4a797ea85bdf2340) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga9b909a32be274f8a4a797ea85bdf2340)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P3 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetSolidMaterialName(const VARIANT FAR&varnSolidNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnSolidNo` | `const VARIANT FAR&` | [in] | The Solid number ID (Type: Int). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `Material` | name of the specified solid. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetSolidMaterialName(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetSolidMaterialName(MaterialQuery request)`<br>
**Proposed named result:** `MaterialNameResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getisotropicmaterialassignedsolidcount"></a>

### 166. GetIsotropicMaterialAssignedSolidCount

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#ga875c78622314eabe9df5600344f39a87) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#ga875c78622314eabe9df5600344f39a87) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga875c78622314eabe9df5600344f39a87)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P3 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetIsotropicMaterialAssignedSolidCount(const VARIANT&strMaterialName)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strMaterialName` | `const VARIANT&` | [in] | Identification title of the material. |


**Native return description:** Count of solids assigned with the specified isotropic material(Type:Long).

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetIsotropicMaterialAssignedSolidCount(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetIsotropicMaterialAssignedSolidCount(MaterialQuery request)`<br>
**Proposed named result:** `PropertyCountResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getisotropicmaterialassignedsolidlist"></a>

### 167. GetIsotropicMaterialAssignedSolidList

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#ga87626360957ef20687a901188e328fdd) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#ga87626360957ef20687a901188e328fdd) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga87626360957ef20687a901188e328fdd)<br>
**Classification:** Type/meaning clarification<br>
**Current typed coverage:** None.<br>
**Plan:** P3 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetIsotropicMaterialAssignedSolidList(const VARIANT&strMaterialName,VARIANT FAR&nSolidList)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strMaterialName` | `const VARIANT&` | [in] | Identification title of the material(type: string). |
| `nSolidList` | `VARIANT FAR&` | [out] | List of solid(type: long array). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `true` | Get solid list successful. |
| `false` | Generate Error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetIsotropicMaterialAssignedSolidList(System.Object, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetIsotropicMaterialAssignedSolidList(MaterialQuery request)`<br>
**Proposed named result:** `PropertyIdentifierListResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

**2024 versus 2025/2026:**

| Year | Documented contract |
| --- | --- |
| 2024 | Get isotropic material assigned solid list. Parameters [in] strMaterialName Identification title of the material. [out] nSolidList List of solid. Return values true Get solid list successful. false Generate Error. C++ Syntax VBA Syntax See also OSPropertyUI::GetIsotropicMaterialAssignedSolidCount |
| 2025 / 2026 | Get isotropic material assigned solid list. Parameters [in] strMaterialName Identification title of the material(type: string). [out] nSolidList List of solid(type: long array). Return values true Get solid list successful. false Generate Error. C++ Syntax VBA Syntax See also OSPropertyUI::GetIsotropicMaterialAssignedSolidCount |

<a id="property-settypetoisotropicmaterial"></a>

### 168. SetTypeToIsotropicMaterial

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#ga14f907ed47c4fbfd9d1c644ce60ed823) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#ga14f907ed47c4fbfd9d1c644ce60ed823) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga14f907ed47c4fbfd9d1c644ce60ed823)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P3 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::SetTypeToIsotropicMaterial(const VARIANT&varMatName,const VARIANT&varType)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varMatName` | `const VARIANT&` | [in] | Identification title of the material(Type: String). |
| `varType` | `const VARIANT&` | [in] | material Type(Type: Long), please Refer OSPropertyUI::GetTypeForIsotropicMaterial . |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `true` | Set Type to Material successful. |
| `false` | Generate Error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object SetTypeToIsotropicMaterial(System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `SetTypeToIsotropicMaterial(MaterialDefinitionRequest request)`<br>
**Proposed named result:** `PropertyContextChangeResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-gettypeforisotropicmaterial"></a>

### 169. GetTypeForIsotropicMaterial

**Section:** Material: Get and Remove Material Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html#ga3f9536e2090de7917bb53289dcccf2f0) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html#ga3f9536e2090de7917bb53289dcccf2f0) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga3f9536e2090de7917bb53289dcccf2f0)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P3 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetTypeForIsotropicMaterial(const VARIANT FAR&varMatName,VARIANT FAR&varType)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varMatName` | `const VARIANT FAR&` | [in] | Identification title of the material(Type: String). |
| `varType` | `VARIANT FAR&` | [out] | material Type(Type: Long). No. material Type 0 Not Specified 1 Steel 2 Concrete 3 Aluminum 4 Timber |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `FALSE` | Can not find the specified material. |
| `TRUE` | Find the specified material successful. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetTypeForIsotropicMaterial(System.Object, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetTypeForIsotropicMaterial(MaterialQuery request)`<br>
**Proposed named result:** `SpecificationQueryResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-setmaterialid"></a>

### 170. SetMaterialID

**Section:** Material: Assign Material to Section and Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_assign_mat.html#gaeef73e958f01de09e3c761d0272599d5) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_assign_mat.html#gaeef73e958f01de09e3c761d0272599d5) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_mat.html#gaeef73e958f01de09e3c761d0272599d5)<br>
**Classification:** Deprecated: use SetMaterialName<br>
**Current typed coverage:** None.<br>
**Plan:** P0 — Deprecated: use SetMaterialName

```cpp
void OSPropertyUI::SetMaterialID(const VARIANT FAR&nMaterial)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nMaterial` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |


**Native return description:** No explicit return section; verify before implementation.

**Installed 2025 / 2026 IOSPropertyUI:** No matching name in either inspected interface; native availability must be established independently.

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Public API decision:** Deprecated: use SetMaterialName. No new managed entry proposed.

<a id="property-setmaterialname"></a>

### 171. SetMaterialName

**Section:** Material: Assign Material to Section and Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_assign_mat.html#ga3d08cca38e2d9db5f1e7fdfae318ede6) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_assign_mat.html#ga3d08cca38e2d9db5f1e7fdfae318ede6) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_mat.html#ga3d08cca38e2d9db5f1e7fdfae318ede6)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P5 — Pending; contract verification before implementation

```cpp
void OSPropertyUI::SetMaterialName(LPCTSTR strMaterialName)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strMaterialName` | `LPCTSTR` | [in] | Name of the material. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | Succeed. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object SetMaterialName(System.String)`

**Main adapter targets:** [SetMaterialName](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L232)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `SetMaterialName(MaterialDefinitionRequest request)`<br>
**Proposed named result:** `PropertyContextChangeResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-assignmaterialtomember"></a>

### 172. AssignMaterialToMember

**Section:** Material: Assign Material to Section and Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_assign_mat.html#ga1d4300751f2837d7af62dc01dbb2432a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_assign_mat.html#ga1d4300751f2837d7af62dc01dbb2432a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_mat.html#ga1d4300751f2837d7af62dc01dbb2432a)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P5 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AssignMaterialToMember(const VARIANT&strMaterialName,const VARIANT&varMemberNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strMaterialName` | `const VARIANT&` | [in] | Identification title of material. |
| `varMemberNo` | `const VARIANT&` | [in] | Single or an array of intergers containing list of member nos. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | TRUE. |
| `0` | FALSE. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AssignMaterialToMember(System.Object, System.Object)`

**Main adapter targets:** [AssignMaterialToMember](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L211), [AssignMaterialToMember](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L219), [AssignMaterialProperties](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1566), [AssignMaterialProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1691)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `AssignMaterialToMember(PropertyAssignmentRequest request)`<br>
**Proposed named result:** `PropertyAssignmentResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-assignmaterialtoplate"></a>

### 173. AssignMaterialToPlate

**Section:** Material: Assign Material to Section and Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_assign_mat.html#gad8b53c7251166270f27431a17b8172e1) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_assign_mat.html#gad8b53c7251166270f27431a17b8172e1) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_mat.html#gad8b53c7251166270f27431a17b8172e1)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P5 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AssignMaterialToPlate(const VARIANT&strMaterialName,const VARIANT&varPlateNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strMaterialName` | `const VARIANT&` | [in] | Identification title of material. (String) |
| `varPlateNo` | `const VARIANT&` | [in] | Single or an array of intergers containing list of plate nos. (Long / Interger) |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-113` | varPlateNos type error (Long or Int Expected) |
| `-4009` | All the plate numbers are invalid. |
| `-4008` | Some of the plate numbers are invalid. |
| `-6023` | Material not found. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AssignMaterialToPlate(System.String, Int32)`

**Main adapter targets:** [AssignMaterialProperties](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1567), [AssignMaterialProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1692)<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `AssignMaterialToPlate(PropertyAssignmentRequest request)`<br>
**Proposed named result:** `PropertyAssignmentResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-assignmaterialtoparametricsurface"></a>

### 174. AssignMaterialToParametricSurface

**Section:** Material: Assign Material to Section and Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_assign_mat.html#gaebf142533eb1edfef25b3f787da3119d) (summary only) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_assign_mat.html#gaebf142533eb1edfef25b3f787da3119d) (summary only) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_mat.html#gaebf142533eb1edfef25b3f787da3119d) (summary only)<br>
**Classification:** Reserved: do not use<br>
**Current typed coverage:** None.<br>
**Plan:** P0 — Reserved: do not use

```cpp
VARIANT OSPropertyUI::AssignMaterialToParametricSurface(const VARIANT&strMaterialName,const VARIANT&varHandle)
```

**Summary-only contract:** This function is reserved, do not use.
There is no detailed parameter/return contract. The source explicitly reserves this entry; do not invent a callable public method.

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AssignMaterialToParametricSurface(System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Public API decision:** Reserved: do not use. No new managed entry proposed.

<a id="property-assignmaterialtosolid"></a>

### 175. AssignMaterialToSolid

**Section:** Material: Assign Material to Section and Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_assign_mat.html#ga0a26c0b843f84f87f796b75646fe0b72) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_assign_mat.html#ga0a26c0b843f84f87f796b75646fe0b72) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_mat.html#ga0a26c0b843f84f87f796b75646fe0b72)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P5 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::AssignMaterialToSolid(const VARIANT&strMaterialName,const VARIANT&varSolidNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strMaterialName` | `const VARIANT&` | [in] | Identification title of material. |
| `varSolidNo` | `const VARIANT&` | [in] | Signle or an array of intergers contains list of solid nos. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | TRUE. |
| `0` | FALSE. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object AssignMaterialToSolid(System.Object, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `AssignMaterialToSolid(PropertyAssignmentRequest request)`<br>
**Proposed named result:** `PropertyAssignmentResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-creatememberattribute"></a>

### 176. CreateMemberAttribute

**Section:** Attribute: Create Attribute Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_attri.html#ga596e62e697d449368e61f64155d2e3e3) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_attri.html#ga596e62e697d449368e61f64155d2e3e3) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_attri.html#ga596e62e697d449368e61f64155d2e3e3)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
long OSPropertyUI::CreateMemberAttribute(LPCTSTR strAttributeName,LPCTSTR strValue)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strAttributeName` | `LPCTSTR` | [in] | Name of the attribute. |
| `strValue` | `LPCTSTR` | [in] | A string value |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 CreateMemberAttribute(System.String, System.String)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateMemberAttribute(MemberSpecificationRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-createelementattribute"></a>

### 177. CreateElementAttribute

**Section:** Attribute: Create Attribute Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_attri.html#gaf9f5aa915388204e73930be35b8936fc) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_attri.html#gaf9f5aa915388204e73930be35b8936fc) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_attri.html#gaf9f5aa915388204e73930be35b8936fc)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
long OSPropertyUI::CreateElementAttribute(LPCTSTR strAttributeName,LPCTSTR strValue)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strAttributeName` | `LPCTSTR` | [in] | Name of the attribute. |
| `strValue` | `LPCTSTR` | [in] | A string value |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 CreateElementAttribute(System.String, System.String)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `CreateElementAttribute(MemberSpecificationRequest request)`<br>
**Proposed named result:** `SectionCreationResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-deletememberattribute"></a>

### 178. DeleteMemberAttribute

**Section:** Attribute: Get and Remove Attribute Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_attri.html#ga624209a58bc90aa8debb826c76cf4850) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_attri.html#ga624209a58bc90aa8debb826c76cf4850) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga624209a58bc90aa8debb826c76cf4850)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
long OSPropertyUI::DeleteMemberAttribute(LPCTSTR strAttributeName,LPCTSTR strValue)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strAttributeName` | `LPCTSTR` | [in] | Name of the attribute. |
| `strValue` | `LPCTSTR` | [in] | A string value |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 DeleteMemberAttribute(System.String, System.String)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `DeleteMemberAttribute(MemberSpecificationRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getmembercountbyattribute"></a>

### 179. GetMemberCountByAttribute

**Section:** Attribute: Get and Remove Attribute Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_attri.html#gacb8947a2d5bc779fcd83573c288a024f) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_attri.html#gacb8947a2d5bc779fcd83573c288a024f) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#gacb8947a2d5bc779fcd83573c288a024f)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
long OSPropertyUI::GetMemberCountByAttribute(LPCTSTR strAttributeName,LPCTSTR strValue)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strAttributeName` | `LPCTSTR` | [in] | Name of the attribute. |
| `strValue` | `LPCTSTR` | [in] | A string value |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The number of member in specified attribute. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 GetMemberCountByAttribute(System.String, System.String ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetMemberCountByAttribute(SpecificationQuery request)`<br>
**Proposed named result:** `PropertyCountResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getmemberlistbyattribute"></a>

### 180. GetMemberListByAttribute

**Section:** Attribute: Get and Remove Attribute Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_attri.html#ga601c518a43ee9b2181bb74a1eef40cdb) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_attri.html#ga601c518a43ee9b2181bb74a1eef40cdb) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga601c518a43ee9b2181bb74a1eef40cdb)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
long OSPropertyUI::GetMemberListByAttribute(LPCTSTR strAttributeName,LPCTSTR strValue,const VARIANT FAR&varMemberList)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strAttributeName` | `LPCTSTR` | [in] | Name of the attribute. |
| `strValue` | `LPCTSTR` | [in] | A string value |
| `varMemberList` | `const VARIANT FAR&` | [out] | Member(s) number ID VARIANT array. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 GetMemberListByAttribute(System.String, System.String ByRef, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetMemberListByAttribute(SpecificationQuery request)`<br>
**Proposed named result:** `PropertyIdentifierListResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getmemberattributecount"></a>

### 181. GetMemberAttributeCount

**Section:** Attribute: Get and Remove Attribute Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_attri.html#ga65d055a80eda1974297c41556424518a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_attri.html#ga65d055a80eda1974297c41556424518a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga65d055a80eda1974297c41556424518a)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetMemberAttributeCount()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return values:**

| Value | Meaning |
| --- | --- |
| `Member` | Attribute Count(Type: Long). |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetMemberAttributeCount()`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetMemberAttributeCount()`<br>
**Proposed named result:** `PropertyCountResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getmemberattributelist"></a>

### 182. GetMemberAttributeList

**Section:** Attribute: Get and Remove Attribute Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_attri.html#ga8ee3d1f547137a80ac72d653fadb49a9) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_attri.html#ga8ee3d1f547137a80ac72d653fadb49a9) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga8ee3d1f547137a80ac72d653fadb49a9)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetMemberAttributeList(VARIANT FAR&attributeNameList,VARIANT FAR&attributeValueList)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `attributeNameList` | `VARIANT FAR&` | [out] | attribute Name List(Type: String Array). |
| `attributeValueList` | `VARIANT FAR&` | [out] | attribute Value List(Type: String Array). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `Member` | Attribute Count(Type: Long). |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetMemberAttributeList(System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetMemberAttributeList(SpecificationQuery request)`<br>
**Proposed named result:** `PropertyIdentifierListResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-deleteelementattribute"></a>

### 183. DeleteElementAttribute

**Section:** Attribute: Get and Remove Attribute Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_attri.html#ga33b257dd90f2aa0dfdc229de5f4f09be) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_attri.html#ga33b257dd90f2aa0dfdc229de5f4f09be) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga33b257dd90f2aa0dfdc229de5f4f09be)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
long OSPropertyUI::DeleteElementAttribute(LPCTSTR strAttributeName,LPCTSTR strValue)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strAttributeName` | `LPCTSTR` | [in] | Name of the attribute. |
| `strValue` | `LPCTSTR` | [in] | A string value |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 DeleteElementAttribute(System.String, System.String)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `DeleteElementAttribute(MemberSpecificationRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getelementcountbyattribute"></a>

### 184. GetElementCountByAttribute

**Section:** Attribute: Get and Remove Attribute Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_attri.html#gae0769d51f8406a5d3a64daba5ade7c1c) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_attri.html#gae0769d51f8406a5d3a64daba5ade7c1c) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#gae0769d51f8406a5d3a64daba5ade7c1c)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
long OSPropertyUI::GetElementCountByAttribute(LPCTSTR strAttributeName,LPCTSTR strValue)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strAttributeName` | `LPCTSTR` | [in] | Name of the attribute. |
| `strValue` | `LPCTSTR` | [in] | A string value |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | The number of element(s) in specified attribute. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 GetElementCountByAttribute(System.String, System.String ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetElementCountByAttribute(SpecificationQuery request)`<br>
**Proposed named result:** `PropertyCountResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getelementlistbyattribute"></a>

### 185. GetElementListByAttribute

**Section:** Attribute: Get and Remove Attribute Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_attri.html#gafc4b745f61d6e1d6679342b777c32e55) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_attri.html#gafc4b745f61d6e1d6679342b777c32e55) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#gafc4b745f61d6e1d6679342b777c32e55)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
long OSPropertyUI::GetElementListByAttribute(LPCTSTR strAttributeName,LPCTSTR strValue,const VARIANT FAR&varElementList)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strAttributeName` | `LPCTSTR` | [in] | Name of the attribute. |
| `strValue` | `LPCTSTR` | [in] | A string value |
| `varElementList` | `const VARIANT FAR&` | [out] | Element(s) number ID VARIANT array. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 GetElementListByAttribute(System.String, System.String ByRef, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetElementListByAttribute(SpecificationQuery request)`<br>
**Proposed named result:** `PropertyIdentifierListResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getassignedattributecount"></a>

### 186. GetAssignedAttributeCount

**Section:** Attribute: Get and Remove Attribute Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_attri.html#ga152207bd66e3c7aaa590a87dfde0ee79) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_attri.html#ga152207bd66e3c7aaa590a87dfde0ee79) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga152207bd66e3c7aaa590a87dfde0ee79)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
long OSPropertyUI::GetAssignedAttributeCount(long nMembOrPlateNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nMembOrPlateNo` | `long` | [in] | The number ID of member or plate. |


**Native return description:** The number of attribute associated with nMembOrPlateNo member (if member with this ID exists) or plate (if member not exist).

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 GetAssignedAttributeCount(Int32)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetAssignedAttributeCount(SpecificationQuery request)`<br>
**Proposed named result:** `PropertyCountResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getassignedattributebyindex"></a>

### 187. GetAssignedAttributeByIndex

**Section:** Attribute: Get and Remove Attribute Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_attri.html#ga5fb1fa2b9a41c80056c21d5af43ead20) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_attri.html#ga5fb1fa2b9a41c80056c21d5af43ead20) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga5fb1fa2b9a41c80056c21d5af43ead20)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
long OSPropertyUI::GetAssignedAttributeByIndex(long nIndex,BSTR FAR*varAttributeName,BSTR FAR*varValue)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `nIndex` | `long` | [in] | The attribute index. |
| `varAttributeName` | `BSTR FAR*` | [out] | Attribute name. |
| `varValue` | `BSTR FAR*` | [out] | A string value |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 GetAssignedAttributeByIndex(Int32, System.String ByRef, System.String ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetAssignedAttributeByIndex(SpecificationQuery request)`<br>
**Proposed named result:** `SpecificationQueryResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-removeattribute"></a>

### 188. RemoveAttribute

**Section:** Attribute: Get and Remove Attribute Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_attri.html#gae2b97c9c2b4b73c84e9f221f50062150) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_attri.html#gae2b97c9c2b4b73c84e9f221f50062150) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#gae2b97c9c2b4b73c84e9f221f50062150)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
long OSPropertyUI::RemoveAttribute(LPCTSTR strAttributeName,LPCTSTR strValue,const VARIANT FAR&varMemberList)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strAttributeName` | `LPCTSTR` | [in] | Name of the attribute. |
| `strValue` | `LPCTSTR` | [in] | A string value |
| `varMemberList` | `const VARIANT FAR&` | [in] | Member(s) number ID list VARIANT array. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 RemoveAttribute(System.String, System.String, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `RemoveAttribute(MemberSpecificationRequest request)`<br>
**Proposed named result:** `PropertyRemovalResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-assignmemberattribute"></a>

### 189. AssignMemberAttribute

**Section:** Attribute: Assign Attribute to Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_assign_attri.html#gad33b01a3d202d730de11bd3e13e0d8c6) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_assign_attri.html#gad33b01a3d202d730de11bd3e13e0d8c6) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_attri.html#gad33b01a3d202d730de11bd3e13e0d8c6)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
long OSPropertyUI::AssignMemberAttribute(LPCTSTR strAttributeName,LPCTSTR strValue,const VARIANT FAR&varMemberList)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strAttributeName` | `LPCTSTR` | [in] | Name of the attribute. |
| `strValue` | `LPCTSTR` | [in] | A string value |
| `varMemberList` | `const VARIANT FAR&` | [in] | Member(s) number ID VARIANT array. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 AssignMemberAttribute(System.String, System.String, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `AssignMemberAttribute(PropertyAssignmentRequest request)`<br>
**Proposed named result:** `PropertyAssignmentResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-assignelementattribute"></a>

### 190. AssignElementAttribute

**Section:** Attribute: Assign Attribute to Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_assign_attri.html#ga07b29997e1c5a2ccee2677104bd3dced) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_assign_attri.html#ga07b29997e1c5a2ccee2677104bd3dced) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_attri.html#ga07b29997e1c5a2ccee2677104bd3dced)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P7 — Pending; contract verification before implementation

```cpp
long OSPropertyUI::AssignElementAttribute(LPCTSTR strAttributeName,LPCTSTR strValue,const VARIANT FAR&varElementList)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strAttributeName` | `LPCTSTR` | [in] | Name of the attribute. |
| `strValue` | `LPCTSTR` | [in] | A string value |
| `varElementList` | `const VARIANT FAR&` | [in] | Element(s) number ID VARIANT array. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 AssignElementAttribute(System.String, System.String, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `AssignElementAttribute(PropertyAssignmentRequest request)`<br>
**Proposed named result:** `PropertyAssignmentResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getmemberreleasespec"></a>

### 191. GetMemberReleaseSpec

**Section:** ElementProperty: Functions Get Property information through Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_ele_property.html#ga87a6522925d50ef55f783c90659e0180) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_ele_property.html#ga87a6522925d50ef55f783c90659e0180) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga87a6522925d50ef55f783c90659e0180)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<MemberReleaseSpecification> GetMemberReleaseSpecification(int memberId, MemberEnd memberEnd)`.<br>
**Plan:** P2 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetMemberReleaseSpec(const VARIANT FAR&varnBeamNo,const VARIANT FAR&varnEnd,VARIANT FAR&varnReleaseArray,VARIANT FAR&varfSpringConstArray)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID. |
| `varnEnd` | `const VARIANT FAR&` | [in] | Member Start end (= 0); member End end (= 1). |
| `varnReleaseArray` | `VARIANT FAR&` | [out] | Translational release VARIANT array with 6 elements for 6 DOFs. @ Element value: No release or spring = 0, release = 1, spring = -1, Only MP defined = -3 , MPX, MPY or MPZ defined = -2 . |
| `varfSpringConstArray` | `VARIANT FAR&` | [out] | Rotational releases VARIANT array with 6 elements for 6 DOFs @ Element values Spring value or partial moment factor in floating point number |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | OK. |
| `0` | General error. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetMemberReleaseSpec(System.Object, System.Object, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** [GetMemberReleaseSpecification](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L916)<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<MemberReleaseSpecification> GetMemberReleaseSpecification(int memberId, MemberEnd memberEnd)`<br>
**Implemented result:** `PropertyQueryResult<MemberReleaseSpecification>`

Six integer release flags and six Double springs; preserve DOF order. No public reference parameters.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getmemberlocaloffset"></a>

**Implemented behavior and alternatives:** Use for basic release/spring observations. Choose GetMemberReleaseSpecificationExtended for separate common MP and MPX/MPY/MPZ outputs; this basic query never invents zero for those missing outputs. Native 1/true succeeds and 0/false fails. FX/FY/FZ/MX/MY/MZ mode order and 0/1/-1/-2/-3 codes are preserved. Only applicable parameter slots are consumed; a spring or directional partial-factor slot must be finite. These are model specifications, not support reactions or member-end Output forces.

**Current native call sites:** [Release](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L142). See [usage and qualification](../Property-Porting.md#getmemberreleasespecification).

### 192. GetMemberLocalOffSet

**Section:** ElementProperty: Functions Get Property information through Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_ele_property.html#gafdd6f4200ccd7a34763427950ff01281) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_ele_property.html#gafdd6f4200ccd7a34763427950ff01281) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#gafdd6f4200ccd7a34763427950ff01281)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<Vector3D> GetMemberLocalOffset(int memberId, MemberEnd memberEnd)`.<br>
**Plan:** P2 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetMemberLocalOffSet(const VARIANT FAR&varnBeamNo,const VARIANT FAR&varnEnd,VARIANT FAR&varfxOffSet,VARIANT FAR&varfyOffSet,VARIANT FAR&varfzOffSet)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID. |
| `varnEnd` | `const VARIANT FAR&` | [in] | Member Start end (= 0); member End end (= 1). |
| `varfxOffSet` | `VARIANT FAR&` | [out] | The offset x in coordinate (local). |
| `varfyOffSet` | `VARIANT FAR&` | [out] | The offset y in coordinate (local). |
| `varfzOffSet` | `VARIANT FAR&` | [out] | The offset y in coordinate (local). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-3001` | Cannot find member varnBeamNo . |
| `-6026` | Invalid member/element reference. |
| `-6027` | No OFFSET information is found for the member end. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetMemberLocalOffSet(System.Object, System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** [GetBeamOffsetSpecification](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L709)<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<Vector3D> GetMemberLocalOffset(int memberId, MemberEnd memberEnd)`<br>
**Implemented result:** `PropertyQueryResult<Vector3D>`

Local coordinates are a deliberate selector, not an arbitrary non-global fallback. Validate enum domain and unit context.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getmemberglobaloffset"></a>

**Implemented behavior and alternatives:** Choose this query when a local-coordinate offset is needed; GetMemberGlobalOffset asks STAAD for global components. The vector is an offset in native length units, not a position, displacement result or beta rotation. Native 0 succeeds; -6027 is NoSpecification, -3001 NotFound and -6026 InvalidReference. A successful zero vector stays successful and is not replaced by a shared no-offset singleton. No coordinate conversion or sign change is applied.

**Current native call sites:** [MemberOffset](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L80). See [usage and qualification](../Property-Porting.md#getmemberlocaloffset).

### 193. GetMemberGlobalOffSet

**Section:** ElementProperty: Functions Get Property information through Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_ele_property.html#gaf5e75d28dc527fdefb40ac84111caaa3) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_ele_property.html#gaf5e75d28dc527fdefb40ac84111caaa3) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#gaf5e75d28dc527fdefb40ac84111caaa3)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<Vector3D> GetMemberGlobalOffset(int memberId, MemberEnd memberEnd)`.<br>
**Plan:** P2 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetMemberGlobalOffSet(const VARIANT FAR&varnBeamNo,const VARIANT FAR&varnEnd,VARIANT FAR&varfxOffSet,VARIANT FAR&varfyOffSet,VARIANT FAR&varfzOffSet)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID. |
| `varnEnd` | `const VARIANT FAR&` | [in] | Member Start end (= 0); member End end (= 1). |
| `varfxOffSet` | `VARIANT FAR&` | [out] | The offset x in coordinate (global). |
| `varfyOffSet` | `VARIANT FAR&` | [out] | The offset y in coordinate (global). |
| `varfzOffSet` | `VARIANT FAR&` | [out] | The offset y in coordinate (global). |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-3001` | Cannot find member varnBeamNo . |
| `-6026` | Invalid member/element reference. |
| `-6027` | No OFFSET information is found for the member end. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetMemberGlobalOffSet(System.Object, System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** [GetBeamOffsetSpecification](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L707)<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<Vector3D> GetMemberGlobalOffset(int memberId, MemberEnd memberEnd)`<br>
**Implemented result:** `PropertyQueryResult<Vector3D>`

Global coordinates; verify native flag before interpreting a zero vector as NoOffset. Shared service with local form, preserve end/axes/native status.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getbeamproperty"></a>

**Implemented behavior and alternatives:** Choose this query for global components; GetMemberLocalOffset requests member-local components. No managed rotation is substituted. Native 0 succeeds; -6027 is NoSpecification, -3001 NotFound and -6026 InvalidReference. A successful zero vector differs from an absent specification. The returned Vector3D is an independent value in native length units. These are modeling offsets, not Output displacements.

**Current native call sites:** [MemberOffset](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L81). See [usage and qualification](../Property-Porting.md#getmemberglobaloffset).

### 194. GetBeamProperty

**Section:** ElementProperty: Functions Get Property information through Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_ele_property.html#ga185a7f1dd1d7576097df4e187e23bfe3) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_ele_property.html#ga185a7f1dd1d7576097df4e187e23bfe3) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga185a7f1dd1d7576097df4e187e23bfe3)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<BeamSectionProperties> GetMemberSectionProperties(int memberId)`.<br>
**Plan:** P2 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetBeamProperty(const VARIANT FAR&varnBeamNo,VARIANT FAR&varfWidth,VARIANT FAR&varfDepth,VARIANT FAR&varfAx,VARIANT FAR&varfAy,VARIANT FAR&varfAz,VARIANT FAR&varfIx,VARIANT FAR&varfIy,VARIANT FAR&varfIz)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID. (Type: Int) |
| `varfWidth` | `VARIANT FAR&` | [out] | Width of the section ( WID ). (Type: Double) |
| `varfDepth` | `VARIANT FAR&` | [out] | Depth of the section ( DEP ). (Type: Double) |
| `varfAx` | `VARIANT FAR&` | [out] | Cross section area ( Ax ). (Type: Double) |
| `varfAy` | `VARIANT FAR&` | [out] | Shear area in local y-axis. If zero, shear deformation is ignored in the analysis ( Ay ). (Type: Double) |
| `varfAz` | `VARIANT FAR&` | [out] | Shear area in local z-axis. If zero, shear deformation is ignored in the analysis ( Az ). (Type: Double) |
| `varfIx` | `VARIANT FAR&` | [out] | Moment of inertia about local z-axis ( Ix ). (Type: Double) |
| `varfIy` | `VARIANT FAR&` | [out] | Moment of inertia about local y-axis ( Iy ). (Type: Double) |
| `varfIz` | `VARIANT FAR&` | [out] | Torsional constant ( Iz ). (Type: Double) |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | OK. |
| `0` | General Error |
| `0` | Cannot find member nMemberNo . |
| `0` | No property is attached to the member/element. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetBeamProperty(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<BeamSectionProperties> GetMemberSectionProperties(int memberId)`<br>
**Implemented result:** `PropertyQueryResult<BeamSectionProperties>`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getbeampropertyall"></a>

**Implemented behavior and alternatives:** Use for width, depth, axial/shear areas and the three named native inertia slots. GetMemberSectionPropertiesExtended additionally returns flange/web thickness; those fields are null here. This does not hydrate a section catalog or select a section shape. Native 1/true succeeds; 0/false cannot distinguish a missing member, unassigned property and general failure, so it returns NativeFailure without fabricated data. Native Ix/Iz descriptions conflict with conventional physical-axis labels; their slot names/order are preserved without assuming a torsion or principal-axis mapping. Length, area and inertia slots retain their respective native length, length-squared and length-to-the-fourth units.

**Current native call sites:** [Section](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L177). See [usage and qualification](../Property-Porting.md#getmembersectionproperties).

### 195. GetBeamPropertyAll

**Section:** ElementProperty: Functions Get Property information through Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_ele_property.html#ga2ecc4ba0fec2945bfdda017ed702cbea) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_ele_property.html#ga2ecc4ba0fec2945bfdda017ed702cbea) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga2ecc4ba0fec2945bfdda017ed702cbea)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<BeamSectionProperties> GetMemberSectionPropertiesExtended(int memberId)`.<br>
**Plan:** P2 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetBeamPropertyAll(const VARIANT FAR&varnBeamNo,VARIANT FAR&varfWidth,VARIANT FAR&varfDepth,VARIANT FAR&varfAx,VARIANT FAR&varfAy,VARIANT FAR&varfAz,VARIANT FAR&varfIx,VARIANT FAR&varfIy,VARIANT FAR&varfIz,VARIANT FAR&varfTf,VARIANT FAR&varfTw)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID. (Type: Int) |
| `varfWidth` | `VARIANT FAR&` | [out] | Width of the section ( WID ). (Type: Double) |
| `varfDepth` | `VARIANT FAR&` | [out] | Depth of the section ( DEP ). (Type: Double) |
| `varfAx` | `VARIANT FAR&` | [out] | Cross section area ( Ax ). (Type: Double) |
| `varfAy` | `VARIANT FAR&` | [out] | Shear area in local y-axis. If zero, shear deformation is ignored in the analysis ( Ay ). (Type: Double) |
| `varfAz` | `VARIANT FAR&` | [out] | Shear area in local z-axis. If zero, shear deformation is ignored in the analysis ( Az ). (Type: Double) |
| `varfIx` | `VARIANT FAR&` | [out] | Moment of inertia about local z-axis ( Ix ). (Type: Double) |
| `varfIy` | `VARIANT FAR&` | [out] | Moment of inertia about local y-axis ( Iy ). (Type: Double) |
| `varfIz` | `VARIANT FAR&` | [out] | Torsional constant ( Iz ). (Type: Double) |
| `varfTf` | `VARIANT FAR&` | [out] | Thickness of top flange ( Tf ). (Type: Double) |
| `varfTw` | `VARIANT FAR&` | [out] | Thickness of web ( Tw ). (Type: Double) |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `1` | OK. |
| `0` | Cannot find member nMemberNo . |
| `0` | No property is attached to the member/element. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetBeamPropertyAll(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<BeamSectionProperties> GetMemberSectionPropertiesExtended(int memberId)`<br>
**Implemented result:** `PropertyQueryResult<BeamSectionProperties>`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getbeamconstants"></a>

**Implemented behavior and alternatives:** Use instead of GetMemberSectionProperties when the two additional thickness outputs are required. No section shape, catalog identity, material or new property is inferred. Native 1/true succeeds; 0/false is an ambiguous failure and exposes no packet. All returned slots must be finite. Native Ix/Iz prose is inconsistent, so NativeIx/NativeIy/NativeIz retain native slot identity rather than a guessed torsional/principal interpretation. Values retain native length, area and fourth-power length units.

**Current native call sites:** [Section](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L176). See [usage and qualification](../Property-Porting.md#getmembersectionpropertiesextended).

### 196. GetBeamConstants

**Section:** ElementProperty: Functions Get Property information through Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_ele_property.html#ga13b2745c44a68596591a21b578efa331) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_ele_property.html#ga13b2745c44a68596591a21b578efa331) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga13b2745c44a68596591a21b578efa331)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<MaterialConstants> GetMemberMaterialConstants(int memberId)`.<br>
**Plan:** P2 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetBeamConstants(const VARIANT FAR&varnBeamNo,VARIANT FAR&vardElasticity,VARIANT FAR&vardPoisson,VARIANT FAR&vardDensity,VARIANT FAR&vardAlpha,VARIANT FAR&vardDamp)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | The beam number ID.(Type: Long) |
| `vardElasticity` | `VARIANT FAR&` | [out] | Modulus of elasticity ( E ).(Type: Double) |
| `vardPoisson` | `VARIANT FAR&` | [out] | Poisson's ratio ( POI ).(Type: Double) |
| `vardDensity` | `VARIANT FAR&` | [out] | Weight density ( DEN ).(Type: Double) |
| `vardAlpha` | `VARIANT FAR&` | [out] | Coefficient of thermal expansion ( ALP ).(Type: Double) |
| `vardDamp` | `VARIANT FAR&` | [out] | Damping ratio ( DAMP ).(Type: Double) |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE` | Beam Constants found. |
| `FALSE` | Beam not found/beam constants not found. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetBeamConstants(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<MaterialConstants> GetMemberMaterialConstants(int memberId)`<br>
**Implemented result:** `PropertyQueryResult<MaterialConstants>`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getelementlocaloffset"></a>

**Implemented behavior and alternatives:** Use when the member is known but its material name is not. GetMaterialProperty instead selects a material by exact name; indexed isotropic queries select a material-list index and additionally return G. This native call returns E, Poisson ratio, weight density, thermal expansion and damping, not G or strength fields. ShearModulus is therefore null, never computed from E/Poisson. Native Boolean true succeeds; false does not distinguish a missing member from unavailable constants. WeightDensity is weight per volume, not mass density.

**Current native call sites:** [GetMemberMaterialConstants](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L191). See [usage and qualification](../Property-Porting.md#getmembermaterialconstants).

### 197. GetElementLocalOffset

**Section:** ElementProperty: Functions Get Property information through Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_ele_property.html#gaef9c6cdae820d578c8a9a093edc8ae91) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_ele_property.html#gaef9c6cdae820d578c8a9a093edc8ae91) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#gaef9c6cdae820d578c8a9a093edc8ae91)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<Vector3D> GetPlateLocalOffset(int plateId, int cornerIndex)`.<br>
**Plan:** P2 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetElementLocalOffset(const VARIANT FAR&varnPlateNo,const VARIANT FAR&varnPlateNodeIndex,VARIANT FAR&varfOffsetX,VARIANT FAR&varfOffsetY,VARIANT FAR&varfOffsetZ)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnPlateNo` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varnPlateNodeIndex` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varfOffsetX` | `VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varfOffsetY` | `VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varfOffsetZ` | `VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `nPlateNo` | [in] | The plate number ID. |
| `nPlateNodeIndex` | [in] | The Node Index at which the offset is to be applied (1/2/3/4). |
| `dxOffSet` | [out] | The offset x in coordinate (local). |
| `dyOffSet` | [out] | The offset y in coordinate (local). |
| `dzOffSet` | [out] | The offset y in coordinate (local). |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-4001` | Cannot find member nPlateNo . |
| `-6027` | No OFFSET information is found for the node index. |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 GetElementLocalOffset(System.Object, System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<Vector3D> GetPlateLocalOffset(int plateId, int cornerIndex)`<br>
**Implemented result:** `PropertyQueryResult<Vector3D>`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getelementglobaloffset"></a>

**Implemented behavior and alternatives:** Choose this query for local components, GetPlateGlobalOffset for global components, or GetPlateOffsetSpecification for the stored selector and vector. cornerIndex is native incidence position 1/2/3/4, never a node ID; triangular-corner validity remains STAAD-controlled. Native 0 succeeds, -4001 is NotFound and -6027 NoSpecification. A successful zero vector is retained. No incidence lookup, coordinate transform or unit conversion occurs.

**Current native call sites:** [PlateOffset](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L93). See [usage and qualification](../Property-Porting.md#getplatelocaloffset).

### 198. GetElementGlobalOffset

**Section:** ElementProperty: Functions Get Property information through Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_ele_property.html#ga4cc99a4ac6ee14397d95e411c515f1d8) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_ele_property.html#ga4cc99a4ac6ee14397d95e411c515f1d8) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga4cc99a4ac6ee14397d95e411c515f1d8)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<Vector3D> GetPlateGlobalOffset(int plateId, int cornerIndex)`.<br>
**Plan:** P2 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetElementGlobalOffset(const VARIANT FAR&varnPlateNo,const VARIANT FAR&varnPlateNodeIndex,VARIANT FAR&varfOffsetX,VARIANT FAR&varfOffsetY,VARIANT FAR&varfOffsetZ)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnPlateNo` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varnPlateNodeIndex` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varfOffsetX` | `VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varfOffsetY` | `VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varfOffsetZ` | `VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `nPlateNo` | [in] | The plate number ID. |
| `nPlateNodeIndex` | [in] | The Node Index at which the offset is to be applied (1/2/3/4). |
| `dxOffSet` | [out] | The offset x in coordinate (global). |
| `dyOffSet` | [out] | The offset y in coordinate (global). |
| `dzOffSet` | [out] | The offset y in coordinate (global). |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-4001` | Cannot find member nPlateNo . |
| `-6027` | No OFFSET information is found for the node index. |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 GetElementGlobalOffset(System.Object, System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<Vector3D> GetPlateGlobalOffset(int plateId, int cornerIndex)`<br>
**Implemented result:** `PropertyQueryResult<Vector3D>`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getelementoffsetspec"></a>

**Implemented behavior and alternatives:** Use for global components; GetPlateLocalOffset returns local components and GetPlateOffsetSpecification returns the stored convention. The native Global function is called despite its help summary mistakenly saying local. cornerIndex is a one-based incidence position 1-4, not a node number. Native 0 succeeds, -4001 is NotFound and -6027 NoSpecification. A successful zero vector is not absence; no conversion or sign change occurs.

**Current native call sites:** [PlateOffset](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L94). See [usage and qualification](../Property-Porting.md#getplateglobaloffset).

### 199. GetElementOffsetSpec

**Section:** ElementProperty: Functions Get Property information through Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_ele_property.html#ga94a58d54353f25487a5127b28f6f83ec) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_ele_property.html#ga94a58d54353f25487a5127b28f6f83ec) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga94a58d54353f25487a5127b28f6f83ec)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<ElementOffsetSpecification> GetPlateOffsetSpecification(int plateId, int cornerIndex)`.<br>
**Plan:** P2 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetElementOffsetSpec(const VARIANT FAR&varnPlateNo,const VARIANT FAR&varnPlateNodeIndex,VARIANT FAR&varnDirection,VARIANT FAR&varfxOffSet,VARIANT FAR&varfyOffSet,VARIANT FAR&varfzOffSet)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnPlateNo` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varnPlateNodeIndex` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varnDirection` | `VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varfxOffSet` | `VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varfyOffSet` | `VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |
| `varfzOffSet` | `VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `nPlateNo` | [in] | The plate number ID. |
| `nPlateNodeIndex` | [in] | nPlateNodeIndex The Node Index at which the offset is to be applied (1/2/3/4). |
| `nDirection` | [out] | The offset direction at Local (= 0) or Global (= 1) or Z-Offset (=2) of the member. |
| `dxOffSet` | [out] | The offset x in coordinate. |
| `dyOffSet` | [out] | The offset y in coordinate. |
| `dzOffSet` | [out] | The offset y in coordinate. |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | OK, Not offset. |
| `1` | OK, offset. |
| `-4001` | Cannot find element nPlateNo . |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 GetElementOffsetSpec(System.Object, System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Main adapter targets:** [GetElementNodeOffset](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1195)<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<ElementOffsetSpecification> GetPlateOffsetSpecification(int plateId, int cornerIndex)`<br>
**Implemented result:** `PropertyQueryResult<ElementOffsetSpecification>`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getplatesectionpropertyrefno"></a>

**Implemented behavior and alternatives:** Use when the original Local/Global/Z selector is needed; the local/global offset queries instead request components in a particular coordinate frame. Native 1 supplies an offset, native 0 explicitly means NoSpecification, and -4001 means NotFound. This differs from the local/global getters, where 0 is success. Stored selectors are Local=0, Global=1 and Z=2, matching the main ElementNodeOffsetAxis enum; they must not be cast from its opposite member OffsetAxis numbering. No physical meaning or conversion beyond the native Z selector is inferred. A code-1 zero vector remains valid data.

**Current native call sites:** [GetPlateOffsetSpecification](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L104). See [usage and qualification](../Property-Porting.md#getplateoffsetspecification).

### 200. GetPlateSectionPropertyRefNo

**Section:** ElementProperty: Functions Get Property information through Element Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_ele_property.html#ga365e1db0fa5a9fde9ff8ba9359f81cc3) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_ele_property.html#ga365e1db0fa5a9fde9ff8ba9359f81cc3) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga365e1db0fa5a9fde9ff8ba9359f81cc3)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** Implemented as `PropertyQueryResult<int> GetPlateSectionPropertyId(int plateId)`.<br>
**Plan:** P2 — Implemented; native qualification remains pending.

```cpp
VARIANT OSPropertyUI::GetPlateSectionPropertyRefNo(const VARIANT FAR&varnPlateNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varnPlateNo` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `nPlateNo` | [in] | The plate number ID. |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | the assigned section property ID. |
| `-4001` | Cannot find plate nPlateNo . |
| `-6022` | No property is attached to the plate. |

**Installed 2025 / 2026 IOSPropertyUI:** No matching name in either inspected interface; native availability must be established independently.

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Implemented managed entry:** `PropertyQueryResult<int> GetPlateSectionPropertyId(int plateId)`<br>
**Implemented result:** `PropertyQueryResult<int>`

Help name differs from interop GetPlateThicknessPropertyRefNo. Verify alias and returned ID semantics rather than automatically substituting.
The implemented result guards Data by status and exposes no public ref/out, tuples or raw VARIANT data.

<a id="property-getpublishedprofilename"></a>

**Implemented behavior and alternatives:** Use to identify an assigned property, not to read its thickness values or count property definitions. Positive return values are property IDs; -4001 is NotFound and -6022 NotAssigned. Zero is undocumented and rejected. The method is documented in all three reviewed help sets but absent from inspected 2025/2026 CLR interfaces: a missing acquired dispatch member returns Unsupported. No alternate function name, different entity type or fallback call is attempted; other transport errors propagate.

**Current native call sites:** [GetPlateSectionPropertyId](../../src/StaadPro.Interop/Services/Property/PropertyQueryReader.cs#L121). See [usage and qualification](../Property-Porting.md#getplatesectionpropertyid).

### 201. GetPublishedProfileName

**Section:** Publish: Functions Related to Publish Information Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_publish.html#ga255dfc3675daa744d31d7cd41dc4e1f1) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_publish.html#ga255dfc3675daa744d31d7cd41dc4e1f1) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_publish.html#ga255dfc3675daa744d31d7cd41dc4e1f1)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetPublishedProfileName(LPCTSTR strSTAADName,const VARIANT FAR&varnCountry)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strSTAADName` | `LPCTSTR` | [in] | STAAD profile name(Type: string) |
| `varnCountry` | `const VARIANT FAR&` | [in] | The value for the specified country(Type: long/Integer), refer OSPropertyUI::CreateBeamPropertyFromTable |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<BSTR>` | Published profile name |
| `NULL` | Unable to find any equivalent published name corresponding to STAAD profile name strSTAADName . |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetPublishedProfileName(System.String, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetPublishedProfileName(SpecificationQuery request)`<br>
**Proposed named result:** `PropertyNameResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getstaadprofilename"></a>

### 202. GetSTAADProfileName

**Section:** Publish: Functions Related to Publish Information Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_publish.html#ga505bb1119e1751bc9006263b43016121) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_publish.html#ga505bb1119e1751bc9006263b43016121) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_publish.html#ga505bb1119e1751bc9006263b43016121)<br>
**Classification:** Unchanged<br>
**Current typed coverage:** None.<br>
**Plan:** P10 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetSTAADProfileName(LPCTSTR strPublishedName,const VARIANT FAR&varnCountry)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strPublishedName` | `LPCTSTR` | [in] | Published profile name(Type: string) |
| `varnCountry` | `const VARIANT FAR&` | [in] | The value for the specified country(Type: long/Integer), refer OSPropertyUI::CreateBeamPropertyFromTable |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<BSTR>` | STAAD profile name |
| `NULL` | Unable to find any equivalent STAAD name corresponding to profile name strPublishedName . |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetSTAADProfileName(System.String, System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetSTAADProfileName(SpecificationQuery request)`<br>
**Proposed named result:** `PropertyNameResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-setstandardprofiledbfolder"></a>

### 203. SetStandardProfileDBFolder

**Section:** Standard Section: Functions related to standard section Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_standard_section.html#ga4e87d6b72233137048da0333c8565781) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_standard_section.html#ga4e87d6b72233137048da0333c8565781) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#ga4e87d6b72233137048da0333c8565781)<br>
**Classification:** Version policy candidate<br>
**Current typed coverage:** None.<br>
**Plan:** P9 — Pending; contract verification before implementation

```cpp
long OSPropertyUI::SetStandardProfileDBFolder(LPCTSTR strFolderName)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `strFolderName` | `LPCTSTR` | [in] | Path of the folder. |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `0` | Succeed. |
| `-1` | Error, If path is empty or does not exist. |

**Installed 2025 / 2026 IOSPropertyUI:** `Int32 SetStandardProfileDBFolder(System.String)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `SetStandardProfileDatabaseFolder(string folderPath)`<br>
**Proposed named result:** `PropertyContextChangeResult`

2024 documented success=1/error=0; 2025/2026 success=0/error=-1. Validate exact profile before mutation; no retry with another policy.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

**2024 versus 2025/2026:**

| Year | Documented contract |
| --- | --- |
| 2024 | Sets standard profile database path. Parameters [in] strFolderName Path of the folder. Return values 0 Error, If path is empty or does not exist. 1 Succeed. VBA Syntax See also OSPropertyUI::GetStandardProfileDBFolder |
| 2025 / 2026 | Sets standard profile database path. Parameters [in] strFolderName Path of the folder. Return values 0 Succeed. -1 Error, If path is empty or does not exist. VBA Syntax See also OSPropertyUI::GetStandardProfileDBFolder OSPropertyUI::GetDefaultStandardProfileDBFolder |

<a id="property-getstandardprofiledbfolder"></a>

### 204. GetStandardProfileDBFolder

**Section:** Standard Section: Functions related to standard section Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_standard_section.html#gaa29514922cc9ae65f0d92da202144f5f) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_standard_section.html#gaa29514922cc9ae65f0d92da202144f5f) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#gaa29514922cc9ae65f0d92da202144f5f)<br>
**Classification:** Formatting/cross-reference only<br>
**Current typed coverage:** None.<br>
**Plan:** P9 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetStandardProfileDBFolder()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return description:** The standard profile database folder path.

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetStandardProfileDBFolder()`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetStandardProfileDatabaseFolder()`<br>
**Proposed named result:** `ProfileDatabaseFolderResult`

Common query; only See-also changes. Filesystem path is data, not model path or active section name.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

**2024 versus 2025/2026:**

| Year | Documented contract |
| --- | --- |
| 2024 | Gets standard profile database folder path. Returns The standard profile database folder path. VBA Syntax See also OSPropertyUI::SetStandardProfileDBFolder |
| 2025 / 2026 | Gets standard profile database folder path. Returns The standard profile database folder path. VBA Syntax See also OSPropertyUI::GetDefaultStandardProfileDBFolder OSPropertyUI::SetStandardProfileDBFolder |

<a id="property-getdefaultstandardprofiledbfolder"></a>

### 205. GetDefaultStandardProfileDBFolder

**Section:** Standard Section: Functions related to standard section Property<br>
**Help availability:** 2024 absent · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_standard_section.html#gaf3c05fdd108cc02b0269073ae1543663) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#gaf3c05fdd108cc02b0269073ae1543663)<br>
**Classification:** Newly documented in 2025<br>
**Current typed coverage:** None.<br>
**Plan:** P9 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetDefaultStandardProfileDBFolder()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return description:** The standard profile default database folder path.

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetDefaultStandardProfileDBFolder()`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetDefaultStandardProfileDatabaseFolder()`<br>
**Proposed named result:** `ProfileDatabaseFolderResult`

New help member 2025+; gate with acquired member/profile evidence, never synthesize from current folder.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-isstandarddatabasesection"></a>

### 206. IsStandardDatabaseSection

**Section:** Standard Section: Functions related to standard section Property<br>
**Help availability:** 2024 absent · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_standard_section.html#gaeb4bb81496f3d6451e9a126ea0a6d3a6) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#gaeb4bb81496f3d6451e9a126ea0a6d3a6)<br>
**Classification:** Newly documented in 2025<br>
**Current typed coverage:** None.<br>
**Plan:** P9 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::IsStandardDatabaseSection(const VARIANT FAR&varSecRefNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varSecRefNo` | `const VARIANT FAR&` | Not named in parameter table | No matching parameter-table description. |


**Differently named parameter-table entries:**

| Table name | Direction | Meaning |
| --- | --- | --- |
| `nSecRefNo` | [in] | The section property reference ID. (Type: Long/Integer) |

**Native return values:**

| Value | Meaning |
| --- | --- |
| `TRUE` | Section source is standard database. |
| `FALSE` | Section source is other than standard database. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object IsStandardDatabaseSection(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `IsStandardDatabaseSection(SectionQuery request)`<br>
**Proposed named result:** `StandardSectionSourceResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getstandardsectiondatabasename"></a>

### 207. GetStandardSectionDatabaseName

**Section:** Standard Section: Functions related to standard section Property<br>
**Help availability:** 2024 absent · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_standard_section.html#ga527cf81f6e5369eec3076be58fde90cc) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#ga527cf81f6e5369eec3076be58fde90cc)<br>
**Classification:** Newly documented in 2025<br>
**Current typed coverage:** None.<br>
**Plan:** P9 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetStandardSectionDatabaseName(const VARIANT FAR&varSecRefNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varSecRefNo` | `const VARIANT FAR&` | [in] | The section property reference ID. (Type: Long/Integer) |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Non-Empty-String>` | The standard section database name. |
| `<Empty-String>` | Specified section property reference does not belong to Standard section database. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetStandardSectionDatabaseName(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetStandardSectionDatabaseName(SectionQuery request)`<br>
**Proposed named result:** `StandardSectionIdentityResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getstandardsectiontablename"></a>

### 208. GetStandardSectionTableName

**Section:** Standard Section: Functions related to standard section Property<br>
**Help availability:** 2024 absent · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_standard_section.html#ga45b152c818ab6a2b89be7d90892d0ded) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#ga45b152c818ab6a2b89be7d90892d0ded)<br>
**Classification:** Newly documented in 2025<br>
**Current typed coverage:** None.<br>
**Plan:** P9 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetStandardSectionTableName(const VARIANT FAR&varSecRefNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varSecRefNo` | `const VARIANT FAR&` | [in] | The section property reference ID. (Type: Long/Integer) |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Non-Empty-String>` | The standard section table name. |
| `<Empty-String>` | Specified section property does not belong to any table of Standard section database. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetStandardSectionTableName(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetStandardSectionTableName(SectionQuery request)`<br>
**Proposed named result:** `StandardSectionIdentityResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getstandardsectionname"></a>

### 209. GetStandardSectionName

**Section:** Standard Section: Functions related to standard section Property<br>
**Help availability:** 2024 absent · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_standard_section.html#ga87dfe66234b751a9c9f5914de89e2fb1) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#ga87dfe66234b751a9c9f5914de89e2fb1)<br>
**Classification:** Newly documented in 2025<br>
**Current typed coverage:** None.<br>
**Plan:** P9 — Pending; contract verification before implementation

```cpp
VARIANT OSPropertyUI::GetStandardSectionName(const VARIANT FAR&varSecRefNo)
```

**Native parameters (2026, also 2025):**

| Native parameter | Declaration type | Logical direction | Description |
| --- | --- | --- | --- |
| `varSecRefNo` | `const VARIANT FAR&` | [in] | The section property reference ID. (Type: Long/Integer) |


**Native return values:**

| Value | Meaning |
| --- | --- |
| `<Non-Empty-String>` | The standard section name. |
| `<Empty-String>` | Specified section property does not belong to any table of Standard section database. |

**Installed 2025 / 2026 IOSPropertyUI:** `System.Object GetStandardSectionName(System.Object)`

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Proposed managed entry:** `GetStandardSectionName(SectionQuery request)`<br>
**Proposed named result:** `StandardSectionIdentityResult`

Use the native parameters and output names below as contract evidence; map to descriptive managed request fields and named qualified payloads. Interpret this operation’s return independently.
The result payload must carry the qualified native output fields above (or direct returned data), together with a correctly interpreted operation status. Argument/request shape remains subject to typed API review; never forward unknown VARIANT objects through the new managed surface.

<a id="property-getprofileboundaryinformation"></a>

### 210. GetProfileBoundaryInformation

**Section:** Section: Create Profile from UPT Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#ga97b4352067b45df2388452488c772be1) (summary only) · 2025 absent · 2026 absent<br>
**Classification:** Reserved: do not use<br>
**Current typed coverage:** None.<br>
**Plan:** P0 — Reserved: do not use

```cpp
VARIANT OSPropertyUI::GetProfileBoundaryInformation(const VARIANT FAR&nTableRef,LPCTSTR strSectionName,VARIANT FAR&varNoOfOuter,VARIANT FAR&varNoOfInner)
```

**Summary-only contract:** This function is reserved, do not use.
There is no detailed parameter/return contract. The source explicitly reserves this entry; do not invent a callable public method.

**Installed 2025 / 2026 IOSPropertyUI:** No matching name in either inspected interface; native availability must be established independently.

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Public API decision:** Reserved: do not use. No new managed entry proposed.

<a id="property-getprofilenoofpoints"></a>

### 211. GetProfileNoOfPoints

**Section:** Section: Create Profile from UPT Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#ga32bba6cd08a1fb0c8b776376e6306c2f) (summary only) · 2025 absent · 2026 absent<br>
**Classification:** Reserved: do not use<br>
**Current typed coverage:** None.<br>
**Plan:** P0 — Reserved: do not use

```cpp
VARIANT OSPropertyUI::GetProfileNoOfPoints(const VARIANT FAR&nTableRef,LPCTSTR strSectionName,const VARIANT FAR&varIsInner,const VARIANT FAR&varIndex)
```

**Summary-only contract:** This function is reserved, do not use.
There is no detailed parameter/return contract. The source explicitly reserves this entry; do not invent a callable public method.

**Installed 2025 / 2026 IOSPropertyUI:** No matching name in either inspected interface; native availability must be established independently.

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Public API decision:** Reserved: do not use. No new managed entry proposed.

<a id="property-getprofileboundarypoints"></a>

### 212. GetProfileBoundaryPoints

**Section:** Section: Create Profile from UPT Property<br>
**Help availability:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#ga51857b4c21c7bec5391a0a26f34a6a40) (summary only) · 2025 absent · 2026 absent<br>
**Classification:** Reserved: do not use<br>
**Current typed coverage:** None.<br>
**Plan:** P0 — Reserved: do not use

```cpp
VARIANT OSPropertyUI::GetProfileBoundaryPoints(const VARIANT FAR&nTableRef,LPCTSTR strSectionName,const VARIANT FAR&varIsInner,const VARIANT FAR&varIndex,VARIANT FAR&varZP,VARIANT FAR&varYP)
```

**Summary-only contract:** This function is reserved, do not use.
There is no detailed parameter/return contract. The source explicitly reserves this entry; do not invent a callable public method.

**Installed 2025 / 2026 IOSPropertyUI:** No matching name in either inspected interface; native availability must be established independently.

**Main adapter targets:** None found.<br>
**Main delegated targets:** None found.

**Public API decision:** Reserved: do not use. No new managed entry proposed.

## 4. Complete section-value layouts

These tables describe GetSectionPropertyValuesEx. The 47 named section rows include unchanged forms and the nine changed rows; an additional unlabelled tapered-tube note is retained in the evidence file. Do not use a single unversioned positional parser for all section kinds.

### 2024 layouts

| Section | Type code | Documented values |
| --- | --- | --- |
| BEAM ST | 610 | Ax D Bf Tf Tw Iz Iy Ix |
| BEAM D | 616 | D Bf Tf Tw Iz Iy Ix SP |
| BEAM TC | 613 | Ax D Bf Tf Tw Iz Iy Ix WP TH |
| BEAM BC | 614 | Ax D Bf Tf Tw Iz Iy Ix WP TH |
| BEAM TB | 615 | Ax D Bf Tf Tw Iz Iy Ix WP TH BW BT |
| BEAM T | 611 | Ax D Bf Tf Tw Iz Iy Ix |
| BEAM CM | 612 | Ax D Bf Tf Tw Iz Iy Ix CT FC CW |
| CHANNEL ST | 630 | Ax D Bf Tf Tw Iz Iy Ix CT |
| CHANNEL D | 631 | Ax D Bf Tf Tw Iz Iy Ix SP |
| CHANNEL FR | 633 | Ax D Bf Tf Tw Iz Iy Ix FR |
| ANGLE ST | 640 | Ax D B T Iz Iy Ix |
| ANGLE LD | 642 | Ax D B T Iz Iy Ix LD |
| ANGLE SD | 643 | Ax D B T Iz Iy Ix SD |
| ANGLE RA | 641 | Ax D B T Iz Iy Ix |
| ANGLE SA | 646 | Ax D B T Iz Iy Ix |
| PIPE ST | 660 | Ax OD Tw Iz Iy Ix |
| HSS RECTANGLE | 654 | Ax D B T Iz Iy Ix |
| HSS ROUND | 655 | Ax OD Tw Iz Iy Ix |
| CASTEL ST | 656 | Ax D Bf Tf Tw Iz Iy Ix |
| TUBE ST | 650 | Ax D B T Iz Iy Ix |
| TEE ST | 620 | Ax D Bf Tf Tw Iz Iy Ix |
| PLATE STRIP | 666 | Ax D B T Iz Iy Ix |
| ANGLE COLD ST | 644 | Ax D B T Iz Iy Ix R LIP Ay Az |
| ANGLE COLD ST WITH LIPS | 645 | Ax D B T Iz Iy Ix R LIP Ay Az |
| CHANNEL COLD ST | 634 | Ax Bf T R Iz Iy Ix LIP Ay Az |
| CHANNEL COLD ST WITH LIPS | 635 | Ax Bf T R Iz Iy Ix LIP Ay Az |
| ZEES COLD ST | 662 | Ax D B T LIP <LIP R Iz Iy Ix Ay Az |
| ZEES COLD ST WITH LIPS | 663 | Ax D B T LIP <LIP R Iz Iy Ix Ay Az |
| HAT COLD ST | 664 | Ax D B T BOT_F R Iz Iy Ix Ay Az |
| TAPER | 680 | F1 F2 F3 F4 F5 F6 F7 |
| TAPERED TUBE | 675 | Ax Iz Iy Ix D1 D2 TH SECTION_TYPE |
| PRISMATIC CIRCLE | 671 | Ax Iz Iy Ix YD |
| PRISMATIC RECT | 672 | Ax Iz Iy Ix YD ZD |
| PRISMATIC TRAP | 674 | Ax Iz Iy Ix YD ZD ZB |
| PRISMATIC TEE | 673 | Ax Iz Iy Ix YD ZD YB ZB |
| PRISMATIC GENERAL | 676 | Ax Ay Az Ix Iy Iz YD ZD YB ZB |
| SOLID ROUND | 668 | Ax OD Tw Iz Iy Ix |
| UPT PRISMATIC | 699 | Ax Iz Iy Ix Ay Az YD ZD |
| UPT GENERAL | 697 | Ax D Td B Tb Iz Iy Ix Sz Sy Ay Az Pz Py Hss Dee |
| UPT WIDE FLANGE | 690 | Ax D Tw Wf Tf Iz Iy Ix Ay Az Wf1 Tf1 |
| UPT CHANNEL | 691 | Ax D Tw Wf Tf Iz Iy Ix Cz Ay Az |
| UPT ANGLE | 692 | Ax D Wf Tf R Ay Az Iz Iy Ix |
| UPT DOUBLE ANGLE | 693 | Ax D Wf Tf SP Iz Iy Ix Cy Ay Az |
| UPT TEE | 694 | Ax D Wf Tf Tw Iz Iy Ix Cy Ay Az |
| UPT PIPE | 695 | Ax OD ID Ay Az Iz Iy Ix |
| UPT TUBE | 696 | Ax D Wf Tf Iz Iy Ix Ay Az |
| UPT ISECTION | 698 | Dww Tww Dww1 Bff Tff Bff1 Tff1 Ayf Azf Xif |

### 2025 and 2026 layouts

| Section | Type code | Documented values |
| --- | --- | --- |
| BEAM ST | 610 | Ax D Bf Tf Tw Iz Iy Ix |
| BEAM D | 616 | D Bf Tf Tw Iz Iy Ix SP |
| BEAM TC | 613 | Ax D Bf Tf Tw Iz Iy Ix WP TH |
| BEAM BC | 614 | Ax D Bf Tf Tw Iz Iy Ix WP TH |
| BEAM TB | 615 | Ax D Bf Tf Tw Iz Iy Ix WP TH BW BT |
| BEAM T | 611 | Ax D Bf Tf Tw Iz Iy Ix |
| BEAM CM | 612 | Ax D Bf Tf Tw Iz Iy Ix CT FC CW CD |
| CHANNEL ST | 630 | Ax D Bf Tf Tw Iz Iy Ix |
| CHANNEL D | 631 | Ax D Bf Tf Tw Iz Iy Ix SP |
| CHANNEL FR | 633 | Ax D Bf Tf Tw Iz Iy Ix FR |
| ANGLE ST | 640 | Ax D B T Iz Iy Ix |
| ANGLE LD | 642 | Ax D B T Iz Iy Ix LD |
| ANGLE SD | 643 | Ax D B T Iz Iy Ix SD |
| ANGLE RA | 641 | Ax D B T Iz Iy Ix |
| ANGLE SA | 646 | Ax D B T Iz Iy Ix |
| PIPE ST | 660 | Ax OD Tw Iz Iy Ix |
| HSS RECTANGLE | 654 | Ax D B T Iz Iy Ix |
| HSS ROUND | 655 | Ax OD Tw Iz Iy Ix |
| CASTEL ST | 656 | Ax D Bf Tf Tw Iz Iy Ix |
| TUBE ST | 650 | Ax D B T Iz Iy Ix |
| TEE ST | 620 | Ax D Bf Tf Tw Iz Iy Ix |
| PLATE STRIP | 666 | Ax D B Iz Iy Ix |
| ANGLE COLD ST | 644 | Ax D B T Iz Iy Ix R Ay Az |
| ANGLE COLD ST WITH LIPS | 645 | Ax D B T Iz Iy Ix R LIP Ay Az |
| CHANNEL COLD ST | 634 | Ax D Bf T R Iz Iy Ix Ay Az |
| CHANNEL COLD ST WITH LIPS | 635 | Ax D Bf T R Iz Iy Ix LIP Ay Az |
| ZEES COLD ST | 662 | Ax D B T R Iz Iy Ix Ay Az |
| ZEES COLD ST WITH LIPS | 663 | Ax D B T LIP LIP_Angle R Iz Iy Ix Ay Az |
| HAT COLD ST | 664 | Ax D B T BOT_F R Iz Iy Ix Ay Az |
| TAPER | 680 | F1 F2 F3 F4 F5 F6 F7 |
| TAPERED TUBE | 675 | Ax Iz Iy Ix D1 D2 TH SECTION_TYPE |
| PRISMATIC CIRCLE | 671 | Ax Iz Iy Ix YD |
| PRISMATIC RECT | 672 | Ax Iz Iy Ix YD ZD |
| PRISMATIC TRAP | 674 | Ax Iz Iy Ix YD ZD ZB |
| PRISMATIC TEE | 673 | Ax Iz Iy Ix YD ZD YB ZB |
| PRISMATIC GENERAL | 676 | Ax Ay Az Ix Iy Iz YD ZD YB ZB |
| SOLID ROUND | 668 | Ax OD Tw Iz Iy Ix Z |
| UPT PRISMATIC | 699 | Ax Iz Iy Ix Ay Az YD ZD |
| UPT GENERAL | 697 | Ax D Td B Tb Iz Iy Ix Sz Sy Ay Az Pz Py Hss Dee |
| UPT WIDE FLANGE | 690 | Ax D Tw Wf Tf Iz Iy Ix Ay Az Wf1 Tf1 |
| UPT CHANNEL | 691 | Ax D Tw Wf Tf Iz Iy Ix Cz Ay Az |
| UPT ANGLE | 692 | Ax D Wf Tf R Ay Az Iz Iy Ix |
| UPT DOUBLE ANGLE | 693 | Ax D Wf Tf SP Iz Iy Ix Cy Ay Az |
| UPT TEE | 694 | Ax D Wf Tf Tw Iz Iy Ix Cy Ay Az |
| UPT PIPE | 695 | Ax OD ID Ay Az Iz Iy Ix |
| UPT TUBE | 696 | Ax D Wf Tf Iz Iy Ix Ay Az |
| UPT ISECTION | 698 | Dww Tww Dww1 Bff Tff Bff1 Tff1 Ayf Azf Xif |

## 5. Supplemental native class and interop candidates

Fifteen unique names lie beyond the group tree when native class summaries and vendor metadata are combined. Fourteen appear in the inspected metadata; thirteen appear as summary-only native class declarations, with twelve overlaps. The remaining class-only GetSurfaceSectionPropertyRefNo must not be silently aliased to metadata GetSurfaceThicknessPropertyRefNo. Metadata also uses GetPlateThicknessPropertyRefNo where group help documents GetPlateSectionPropertyRefNo.

All fifteen are P11 discovery candidates. Their missing detailed contracts require surface-versus-parametric identity, array shape, flags, units and receiver-member verification. An Object& signature alone cannot provide that engineering contract.

| Candidate | 2024 / 2025 / 2026 native class declaration | Inspected IOSPropertyUI | Proposed result family |
| --- | --- | --- | --- |
| `AssignMaterialToSurface` | `VARIANT OSPropertyUI::AssignMaterialToSurface(const VARIANT&strMaterialName,const VARIANT&varSurfaceNo)` — [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_property_u_i.html#ac49a11d742e4b5f8823ebb13554a690c) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_property_u_i.html#ac49a11d742e4b5f8823ebb13554a690c) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_property_u_i.html#ac49a11d742e4b5f8823ebb13554a690c) | `System.Object AssignMaterialToSurface(System.Object, System.Object)` | `PropertyAssignmentResult` |
| `AssignSurfaceThickness` | `VARIANT OSPropertyUI::AssignSurfaceThickness(const VARIANT FAR&nSurfaceNo,const VARIANT FAR&nProperty)` — [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_property_u_i.html#a2afd6695c2183953b81b17deefdaf4a1) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_property_u_i.html#a2afd6695c2183953b81b17deefdaf4a1) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_property_u_i.html#a2afd6695c2183953b81b17deefdaf4a1) | `System.Object AssignSurfaceThickness(System.Object, System.Object)` | `PropertyAssignmentResult` |
| `CreateParametricSurfaceThicknessProperty` | `VARIANT OSPropertyUI::CreateParametricSurfaceThicknessProperty(const VARIANT FAR&faThickness)` — [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_property_u_i.html#ae115423b23cbf1a7a595697641f79bcc) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_property_u_i.html#ae115423b23cbf1a7a595697641f79bcc) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_property_u_i.html#ae115423b23cbf1a7a595697641f79bcc) | `System.Object CreateParametricSurfaceThicknessProperty(System.Object)` | `SectionCreationResult` |
| `CreateSurfaceThicknessProperty` | `VARIANT OSPropertyUI::CreateSurfaceThicknessProperty(const VARIANT FAR&faThickness)` — [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_property_u_i.html#a0812a6f48ab3d59da11a2e76154e9421) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_property_u_i.html#a0812a6f48ab3d59da11a2e76154e9421) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_property_u_i.html#a0812a6f48ab3d59da11a2e76154e9421) | `System.Object CreateSurfaceThicknessProperty(System.Object)` | `SectionCreationResult` |
| `GetIsotropicMaterialAssignedSurfaceCount` | `VARIANT OSPropertyUI::GetIsotropicMaterialAssignedSurfaceCount(const VARIANT&strMaterialName)` — [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_property_u_i.html#a13d0ecd7a0c5f541a913411f0b559415) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_property_u_i.html#a13d0ecd7a0c5f541a913411f0b559415) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_property_u_i.html#a13d0ecd7a0c5f541a913411f0b559415) | `System.Object GetIsotropicMaterialAssignedSurfaceCount(System.Object)` | `PropertyCountResult` |
| `GetIsotropicMaterialAssignedSurfaceList` | `VARIANT OSPropertyUI::GetIsotropicMaterialAssignedSurfaceList(const VARIANT&strMaterialName,VARIANT FAR&nSurfaceList)` — [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_property_u_i.html#a72210e78126274b304dfe80d986b0b71) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_property_u_i.html#a72210e78126274b304dfe80d986b0b71) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_property_u_i.html#a72210e78126274b304dfe80d986b0b71) | `System.Object GetIsotropicMaterialAssignedSurfaceList(System.Object, System.Object ByRef)` | `PropertyIdentifierListResult` |
| `GetPlateThicknessPropertyRefNo` | Not listed under this name | `System.Object GetPlateThicknessPropertyRefNo(System.Object)` | `PropertyReferenceResult` |
| `GetSurfaceMaterialName` | `VARIANT OSPropertyUI::GetSurfaceMaterialName(const VARIANT FAR&varnSurfaceNo)` — [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_property_u_i.html#a7df1255128b885286b24d8accd8e697d) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_property_u_i.html#a7df1255128b885286b24d8accd8e697d) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_property_u_i.html#a7df1255128b885286b24d8accd8e697d) | `System.Object GetSurfaceMaterialName(System.Object)` | `MaterialNameResult` |
| `GetSurfaceSectionPropertyRefNo` | `VARIANT OSPropertyUI::GetSurfaceSectionPropertyRefNo(const VARIANT FAR&varnSurfaceNo)` — [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_property_u_i.html#aacace1f1d8b42e6b9ef0b5dd9cb5f476) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_property_u_i.html#aacace1f1d8b42e6b9ef0b5dd9cb5f476) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_property_u_i.html#aacace1f1d8b42e6b9ef0b5dd9cb5f476) | Absent under this name | `PropertyReferenceResult` |
| `GetSurfaceThickness` | `VARIANT OSPropertyUI::GetSurfaceThickness(const VARIANT FAR&varnSurfaceNo,VARIANT FAR&varfThk)` — [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_property_u_i.html#a2e4386f1d58a2b3348e677cd6ff0d4e2) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_property_u_i.html#a2e4386f1d58a2b3348e677cd6ff0d4e2) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_property_u_i.html#a2e4386f1d58a2b3348e677cd6ff0d4e2) | `System.Object GetSurfaceThickness(System.Object, System.Object ByRef)` | `SurfaceThicknessQueryResult` |
| `GetSurfaceThicknessPropertyRefNo` | Not listed under this name | `System.Object GetSurfaceThicknessPropertyRefNo(System.Object)` | `PropertyReferenceResult` |
| `GetThicknessPropertyAssignedSurfaceCount` | `VARIANT OSPropertyUI::GetThicknessPropertyAssignedSurfaceCount(const VARIANT FAR&varProfRefNo)` — [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_property_u_i.html#ae13b813f7eb4bcce0b1c88cc4491da1d) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_property_u_i.html#ae13b813f7eb4bcce0b1c88cc4491da1d) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_property_u_i.html#ae13b813f7eb4bcce0b1c88cc4491da1d) | `System.Object GetThicknessPropertyAssignedSurfaceCount(System.Object)` | `PropertyCountResult` |
| `GetThicknessPropertyAssignedSurfaceList` | `VARIANT OSPropertyUI::GetThicknessPropertyAssignedSurfaceList(const VARIANT FAR&varProfRefNo,VARIANT FAR&nSurfaceList)` — [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_property_u_i.html#a8ab6e4229e6c44b153b9f13ce4dd00c2) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_property_u_i.html#a8ab6e4229e6c44b153b9f13ce4dd00c2) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_property_u_i.html#a8ab6e4229e6c44b153b9f13ce4dd00c2) | `System.Object GetThicknessPropertyAssignedSurfaceList(System.Object, System.Object ByRef)` | `PropertyIdentifierListResult` |
| `RemoveMaterialFromSurface` | `VARIANT OSPropertyUI::RemoveMaterialFromSurface(const VARIANT FAR&nSurfaceNo)` — [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_property_u_i.html#a4bb29ba1561d732c4bf3f3e9689886ae) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_property_u_i.html#a4bb29ba1561d732c4bf3f3e9689886ae) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_property_u_i.html#a4bb29ba1561d732c4bf3f3e9689886ae) | `System.Object RemoveMaterialFromSurface(System.Object)` | `PropertyRemovalResult` |
| `RemovePropertyFromSurface` | `VARIANT OSPropertyUI::RemovePropertyFromSurface(const VARIANT FAR&nSurfaceNo)` — [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_property_u_i.html#aae4f7f92159be9a8197816c10842c565) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_property_u_i.html#aae4f7f92159be9a8197816c10842c565) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_property_u_i.html#aae4f7f92159be9a8197816c10842c565) | `System.Object RemovePropertyFromSurface(System.Object)` | `PropertyRemovalResult` |

### Native implementation helpers excluded from porting

The group inventory already includes two explicitly private RemoveBeam*Helper entries. The following twenty-two additional native implementation helpers occur only in the 2025/2026 class listing. The last helper, DoesUPTSectionExists, is absent from the 2024 class listing; it is not a new COM capability. No managed API should pass native CStruct/SCommand pointers or final-release shared RCWs to reproduce them.

| Native implementation helper | Declaration | 2024 class presence |
| --- | --- | --- |
| `OnFinalRelease` | `virtual void OSPropertyUI::OnFinalRelease()` | Yes |
| `SetStructure` | `void OSPropertyUI::SetStructure(CStruct*pStruct)` | Yes |
| `CreateProperty` | `long OSPropertyUI::CreateProperty(SectionPropertyData&data)` | Yes |
| `RecordAddSpecification` | `void OSPropertyUI::RecordAddSpecification(long nSpecItem)` | Yes |
| `UpdateSpecItemTable` | `void OSPropertyUI::UpdateSpecItemTable(SCommand*pNewSpecItem)` | Yes |
| `AssignBeamPropertyHelper` | `BOOL OSPropertyUI::AssignBeamPropertyHelper(long&beamNo,long&propNo)` | Yes |
| `AssignPlatePropertyHelper` | `BOOL OSPropertyUI::AssignPlatePropertyHelper(long&plateNo,long&propNo)` | Yes |
| `AssignSurfacePropertyHelper` | `BOOL OSPropertyUI::AssignSurfacePropertyHelper(long&surfaceNo,long&propNo)` | Yes |
| `AssignBetaAngleHelper` | `BOOL OSPropertyUI::AssignBetaAngleHelper(long&nBeamNo,float&fBetaAngle)` | Yes |
| `AssignMemberSpecHelper` | `BOOL OSPropertyUI::AssignMemberSpecHelper(long&nBeamNo,long&nSpecNo)` | Yes |
| `AssignElementSpecHelper` | `BOOL OSPropertyUI::AssignElementSpecHelper(long&nPlateNo,long&nSpecNo)` | Yes |
| `RemovePlatePropertyHelper` | `BOOL OSPropertyUI::RemovePlatePropertyHelper(long&plateNo)` | Yes |
| `RemoveSurfacePropertyHelper` | `BOOL OSPropertyUI::RemoveSurfacePropertyHelper(long&surfaceNo)` | Yes |
| `RemovePlateMaterialHelper` | `BOOL OSPropertyUI::RemovePlateMaterialHelper(long&plateNo)` | Yes |
| `RemoveSurfaceMaterialHelper` | `BOOL OSPropertyUI::RemoveSurfaceMaterialHelper(long&surfaceNo)` | Yes |
| `RemoveSolidMaterialHelper` | `BOOL OSPropertyUI::RemoveSolidMaterialHelper(long&surfaceNo)` | Yes |
| `DeleteFromProperty` | `void OSPropertyUI::DeleteFromProperty(SMaterialData*pData)` | Yes |
| `DeleteFromDatabase` | `void OSPropertyUI::DeleteFromDatabase(SCommand*pCo)` | Yes |
| `IsHSSRECTSectionType` | `BOOL OSPropertyUI::IsHSSRECTSectionType(long nCountry,long nSectionType)` | Yes |
| `IsHSSROUNDSectionType` | `BOOL OSPropertyUI::IsHSSROUNDSectionType(long nCountry,long nSectionType)` | Yes |
| `CreateIsotropicMaterialHelper` | `long OSPropertyUI::CreateIsotropicMaterialHelper(CString strName,IsotropicMaterialType matType,std::vector< double > matParams,int bPhysical)` | Yes |
| `DoesUPTSectionExists` | `BOOL OSPropertyUI::DoesUPTSectionExists(int nTableRef,CString strSection)` | No |

## 6. Main-project managed reference surface

All 103 interface overloads have matching adapter implementations after normalizing the fully qualified Country/TypeSpecification names. The current open-source Property interface has none of them. These are source-parity candidates, not a commitment to copy obsolete, ambiguous or unsafe behavior. Existing return types in other published adapters remain compatibility constraints.

| Main interface signature | Source interface / adapter | Direct targets in that adapter method | Adaptation |
| --- | --- | --- | --- |
| `int GetUnusedSectionsCount()` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L48) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L43) | Managed composition or delegation | Named status/data outcome; reuse valid models and internal readers |
| `HashSet<int> GetUnusedSectionsIds()` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L50) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L48) | `GetSectionPropertyAssignedBeamCount` | Named status/data outcome; reuse valid models and internal readers |
| `int CreateBeamPropertyFromTable(ReInvented.Sections.Domain.Enums.Country country, string sectionName, ReInvented.Sections.Domain.Enums.TypeSpecification typeSpec, double additionalSpec1, double additionalSpec2)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L52) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1084) | `CreateBeamPropertyFromTable` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `void AssignBeamProperty(int memberId, int propertyId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L54) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1089) | `AssignBeamProperty` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `void AssignBeamProperty(int[] memberIds, int propertyId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L56) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1094) | `AssignBeamProperty` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `int GetBeamProperty(int memberId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L58) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1102) | `GetSectionPropertyAssignedBeamCount`, `GetSectionPropertyAssignedBeamList` | Named status/data outcome; reuse valid models and internal readers |
| `bool DeleteMaterial(string mName)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L70) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L69) | `DeleteMaterial` | Named status/data outcome; reuse valid models and internal readers |
| `string GetBeamMaterialName(int pId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L77) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L197) | `GetBeamMaterialName` | Named status/data outcome; reuse valid models and internal readers |
| `string GetPlateMaterialName(int pId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L84) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L202) | `GetPlateMaterialName` | Named status/data outcome; reuse valid models and internal readers |
| `void AssignMaterialToMember(string materialName, int memberId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L93) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L207) | `AssignMaterialToMember` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `void AssignMaterialToMember(string materialName, int[] memberIds)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L100) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L215) | `AssignMaterialToMember` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `void SetMaterialName(string materialName)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L114) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L228) | `SetMaterialName` | Named status/data outcome; reuse valid models and internal readers |
| `int CreateCompositeBeamPropertyFromTable(Country country, string baseStaadName, TypeSpecification typeSpecification, double[] plateValuesMetres)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L130) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L247) | `CreateBeamPropertyFromTableWithCoverPlates` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `int CreateBuiltUpBeamProperty(string designation, double[] propertyValuesMetres)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L145) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L265) | Managed composition or delegation | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty AssignBetaAnglesToBeams(HashSet<Beam> beams)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L154) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1573) | `AssignBetaAngle` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty AssignBetaAnglesToBeams(HashSet<Beam> beams, int nThreads)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L159) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1698) | `AssignBetaAngle` | Replace shared-session parallel calls with verified execution ownership |
| `IOSProperty AssignMaterialProperties(HashSet<MaterialProperty> mpSet)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L168) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1559) | `AssignMaterialToMember`, `AssignMaterialToPlate` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty AssignMaterialProperties(HashSet<MaterialProperty> materialPropertyCollection, int nThreads)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L173) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1674) | Managed composition or delegation | Replace shared-session parallel calls with verified execution ownership |
| `IOSProperty AssignMaterialProperty(MaterialProperty mp, int nThreads)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L178) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1687) | `AssignMaterialToMember`, `AssignMaterialToPlate` | Replace shared-session parallel calls with verified execution ownership |
| `IOSProperty AssignOffsetSpecification(OffsetSpecification os, int nThreads)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L187) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1664) | `AssignMemberSpecToBeam` | Replace shared-session parallel calls with verified execution ownership |
| `IOSProperty AssignOffsetSpecifications(HashSet<OffsetSpecification> osSet)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L192) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1550) | `AssignMemberSpecToBeam` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty AssignOffsetSpecifications(HashSet<OffsetSpecification> offsetSpecificationCollection, int nThreads)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L197) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1654) | Managed composition or delegation | Replace shared-session parallel calls with verified execution ownership |
| `IOSProperty AssignPlateElementThickness(PlateElementThickness pet)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L206) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1181) | `AssignPlateThickness` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty AssignPlateElementThicknesses(HashSet<PlateElementThickness> petSet)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L211) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1172) | Managed composition or delegation | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty AssignSectionProperties(HashSet<IGenericSection> gsSet)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L220) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1514) | `AssignBeamProperty` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty AssignSectionProperties(HashSet<SectionProperty> spSet)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L225) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1523) | `AssignBeamProperty` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty AssignSectionProperties(HashSet<SectionProperty> sectionProperties, int nThreads)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L230) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1617) | Managed composition or delegation | Replace shared-session parallel calls with verified execution ownership |
| `IOSProperty AssignSectionProperty(SectionProperty sp)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L235) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1532) | `AssignBeamProperty` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty AssignSectionProperty(SectionProperty sectionProperty, int nThreads)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L240) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1627) | `AssignBeamProperty` | Replace shared-session parallel calls with verified execution ownership |
| `IOSProperty AssignThicknessProperties(HashSet<PlateThickness> tpSet)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L249) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1541) | `AssignPlateThickness` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty AssignThicknessProperties(HashSet<PlateThickness> thicknessPropertyCollection, int nThreads)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L254) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1637) | Managed composition or delegation | Replace shared-session parallel calls with verified execution ownership |
| `IOSProperty AssignThicknessProperty(PlateThickness thicknessProperty, int nThreads)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L259) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1647) | `AssignPlateThickness` | Replace shared-session parallel calls with verified execution ownership |
| `IOSProperty CreateAndAssignMemberEndReleases(HashSet<Beam> beams)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L268) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1579) | `CreateMemberReleaseSpec`, `AssignMemberSpecToBeam` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty CreateAndAssignMemberEndReleases(HashSet<Beam> beams, int nThreads)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L273) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1705) | `CreateMemberReleaseSpec`, `AssignMemberSpecToBeam` | Replace shared-session parallel calls with verified execution ownership |
| `IOSProperty CreateAndAssignOffsetSpecifications(HashSet<Beam> beams)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L278) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1332) | Managed composition or delegation | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty CreateAndAssignOffsetSpecifications(HashSet<Beam> beams, int nThreads)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L283) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1610) | Managed composition or delegation | Replace shared-session parallel calls with verified execution ownership |
| `IOSProperty CreateAndAssignSectionProperties(HashSet<Beam> beams)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L288) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1326) | Managed composition or delegation | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty CreateAndAssignSectionProperties(HashSet<Beam> beams, int nThreads)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L293) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1603) | Managed composition or delegation | Replace shared-session parallel calls with verified execution ownership |
| `IOSProperty CreateMaterialProperties(HashSet<MaterialProperty> mpSet)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L302) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1489) | `CreateIsotropicMaterialProperties` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty CreateMaterialProperties(HashSet<MaterialProperty> mpSet, UnitsConverter uc)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L307) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1431) | Managed composition or delegation | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty CreateMaterialPropertiesNoConversion(HashSet<MaterialProperty> mpSet)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L312) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1451) | Managed composition or delegation | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty CreateMaterialProperty(MaterialProperty mp)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L317) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1502) | `CreateIsotropicMaterialProperties` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty CreateMaterialProperty(MaterialProperty mp, UnitsConverter uc)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L322) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1442) | `CreateIsotropicMaterialProperties` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty CreateMaterialPropertyEx(MaterialProperty mp, UnitsConverter uc)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L327) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1471) | `CreateIsotropicMaterialPropertiesEx` | Obsolete(error=true) in source; investigate rather than enable |
| `IOSProperty CreateMaterialPropertyNoConversion(MaterialProperty mp)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L332) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1463) | `CreateIsotropicMaterialProperties` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty CreateOffsetSpecification(OffsetSpecification oSpec)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L341) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1417) | `CreateMemberOffsetSpec` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty CreateOffsetSpecifications(HashSet<OffsetSpecification> osSet)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L346) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1406) | Managed composition or delegation | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty CreatePlateElementThickness(PlateElementThickness pet)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L351) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1160) | `CreatePlateThicknessProperty` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty CreatePlateElementThicknesses(HashSet<PlateElementThickness> petSet)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L356) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1148) | Managed composition or delegation | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty CreatePlateThicknessProperties(HashSet<PlateThickness> ptSet)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L361) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1384) | Managed composition or delegation | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty CreatePlateThicknessProperty(OSPropertyUI property, PlateThickness pt)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L366) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1395) | `CreatePlateThicknessProperty` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty CreateSectionProperties(HashSet<SectionProperty> spSet)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L371) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1338) | Managed composition or delegation | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty CreateSectionProperty(SectionProperty sp)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L376) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1356) | Managed composition or delegation | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `HashSet<MaterialProperty> GetAllExtendedIsotropicMaterialProperties()` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L385) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L313) | Managed composition or delegation | Named status/data outcome; reuse valid models and internal readers |
| `HashSet<MaterialProperty> GetAllExtendedIsotropicMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L390) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L286) | Managed composition or delegation | Named status/data outcome; reuse valid models and internal readers |
| `HashSet<MaterialProperty> GetAllExtendedMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L395) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L123) | Managed composition or delegation | Named status/data outcome; reuse valid models and internal readers |
| `HashSet<MaterialProperty> GetAllIsotropicMaterialProperties()` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L400) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L321) | Managed composition or delegation | Named status/data outcome; reuse valid models and internal readers |
| `HashSet<MaterialProperty> GetAllIsotropicMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L405) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L300) | Managed composition or delegation | Named status/data outcome; reuse valid models and internal readers |
| `HashSet<MaterialProperty> GetAllMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L410) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L132) | Managed composition or delegation | Named status/data outcome; reuse valid models and internal readers |
| `HashSet<MaterialProperty> GetAllMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates, Func<string, MaterialProperty> materialResolver)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L415) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L99) | `GetBeamMaterialName`, `GetPlateMaterialName` | Named status/data outcome; reuse valid models and internal readers |
| `HashSet<PlateElementThickness> GetAllPlateElementThicknesses()` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L424) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1244) | `GetThicknessPropertyCount`, `GetThicknessPropertyList` | Named status/data outcome; reuse valid models and internal readers |
| `HashSet<PlateElementThickness> GetAllPlateElementThicknessesWithPopulatedAssignments(IEnumerable<Plate> allPlates)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L429) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1222) | Managed composition or delegation | Named status/data outcome; reuse valid models and internal readers |
| `HashSet<PlateElementThickness> GetAllPlateElementThicknessesWithPopulatedAssignments(IOSGeometry geometry)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L434) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1237) | Managed composition or delegation | Named status/data outcome; reuse valid models and internal readers |
| `HashSet<IGenericSection> GetAllSections()` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L443) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L596) | `GetSectionPropertyCount`, `GetSectionPropertyList` | Named status/data outcome; reuse valid models and internal readers |
| `HashSet<IGenericSection> GetAllSectionsWithAssignedBeamsList(IEnumerable<Beam> allBeams)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L448) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L552) | Managed composition or delegation | Named status/data outcome; reuse valid models and internal readers |
| `HashSet<IGenericSection> GetAllSectionsWithAssignedBeamsList(IOSGeometry geometry)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L453) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L522) | Managed composition or delegation | Named status/data outcome; reuse valid models and internal readers |
| `double GetAveragePlateThickness(int plateId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L462) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1316) | Managed composition or delegation | Named status/data outcome; reuse valid models and internal readers |
| `HashSet<Beam> GetBeamsWithMissingSectionProperty(HashSet<Beam> allBeams, IEnumerable<int> propIds)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L467) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1063) | Managed composition or delegation | Named status/data outcome; reuse valid models and internal readers |
| `HashSet<int> GetInactiveMembersList()` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L472) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L829) | `GetInactiveMemberCount`, `GetInactiveMemberList` | Obsolete(error=true) in source; investigate rather than enable |
| `HashSet<Beam> GetInactiveMembersList(IOSGeometry geometry)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L477) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L845) | Managed composition or delegation | Obsolete(error=true) in source; investigate rather than enable |
| `ElementNodeOffset GetElementNodeOffset(Plate plate, int nIndex, int nId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L486) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1191) | `GetElementOffsetSpec` | Named status/data outcome; reuse valid models and internal readers |
| `HashSet<ElementOffset> GetElementOffsets(IEnumerable<Plate> plates)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L491) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1200) | Managed composition or delegation | Named status/data outcome; reuse valid models and internal readers |
| `OffsetSpecification GetBeamOffsetSpecification(int bId, OffsetLocation location, OffsetAxis offsetAxis)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L496) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L701) | `GetMemberGlobalOffSet`, `GetMemberLocalOffSet` | Named status/data outcome; reuse valid models and internal readers |
| `MemberReleases GetMemberReleaseSpecification(int memberId, MemberEnd memberEnd)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L501) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L912) | `GetMemberReleaseSpec` | Named status/data outcome; reuse valid models and internal readers |
| `MemberReleases GetMemberReleaseSpecificationExtended(int memberId, MemberEnd memberEnd)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L506) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L954) | `GetMemberReleaseSpecEx` | Named status/data outcome; reuse valid models and internal readers |
| `MemberSpecificationType GetMemberSpecification(int msId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L511) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L871) | `GetMemberSpecCode` | Named status/data outcome; reuse valid models and internal readers |
| `MaterialProperty GetExtendedIsotropicMaterialProperty(int mId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L520) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L343) | `GetIsotropicMaterialPropertiesEx` | Named status/data outcome; reuse valid models and internal readers |
| `MaterialProperty GetExtendedMaterialProperty(string mName)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L525) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L146) | `GetMaterialPropertyEx` | Named status/data outcome; reuse valid models and internal readers |
| `MaterialProperty GetIsotropicMaterialProperty(int mId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L530) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L370) | `GetIsotropicMaterialProperties` | Named status/data outcome; reuse valid models and internal readers |
| `MaterialProperty GetMaterialProperty(string mName)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L535) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L176) | `GetMaterialProperty` | Named status/data outcome; reuse valid models and internal readers |
| `HashSet<Plate> GetPlatesWithMissingThicknessProperty(HashSet<Plate> allPlates, IEnumerable<int> propIds)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L540) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1303) | Managed composition or delegation | Named status/data outcome; reuse valid models and internal readers |
| `PlateElementThickness GetPlateThicknessByPlateId(int plateId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L545) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1275) | `GetPlateThickness` | Named status/data outcome; reuse valid models and internal readers |
| `PlateElementThickness GetPlateThicknessByThicknessPropertyId(int petId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L550) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1267) | `GetThicknessPropertyValues` | Named status/data outcome; reuse valid models and internal readers |
| `double? GetSectionalArea(int propId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L555) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1135) | Managed composition or delegation | Named status/data outcome; reuse valid models and internal readers |
| `IGenericSection GetSectionBy(int propId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L560) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1013) | `GetCountofSectionPropertyValuesEx`, `GetSectionPropertyValuesEx`, `GetSectionPropertyCountry`, `GetRecordForSection` | Named status/data outcome; reuse valid models and internal readers |
| `IGenericSection GetSectionByPropertyIdWithAssignedBeamsList(int propId, IEnumerable<Beam> allBeams)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L565) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L977) | `GetSectionPropertyAssignedBeamCount` | Named status/data outcome; reuse valid models and internal readers |
| `IGenericSection GetSectionByPropertyIdWithAssignedBeamsList(int propId, IOSGeometry geometry)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L570) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L995) | `GetSectionPropertyAssignedBeamCount` | Named status/data outcome; reuse valid models and internal readers |
| `HashSet<Beam> GetSectionPropertyAssignedBeams(HashSet<Beam> allBeams, int propId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L575) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1052) | `GetSectionPropertyAssignedBeamCount`, `GetSectionPropertyAssignedBeamList` | Named status/data outcome; reuse valid models and internal readers |
| `int[] GetSectionPropertyList()` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L580) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1043) | `GetSectionPropertyCount`, `GetSectionPropertyList` | Named status/data outcome; reuse valid models and internal readers |
| `string GetSectionPropertyName(int propId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L585) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1076) | `GetSectionPropertyName` | Named status/data outcome; reuse valid models and internal readers |
| `double[] GetSectionPropertyValues(int propId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L590) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1125) | `GetCountofSectionPropertyValuesEx`, `GetSectionPropertyValuesEx` | Named status/data outcome; reuse valid models and internal readers |
| `HashSet<Plate> GetThicknessPropertyAssignedPlates(HashSet<Plate> allPlates, int propId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L595) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1292) | `GetThicknessPropertyAssignedPlateCount`, `GetThicknessPropertyAssignedPlateList` | Named status/data outcome; reuse valid models and internal readers |
| `int[] GetThicknessPropertyList()` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L600) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1283) | `GetThicknessPropertyCount`, `GetThicknessPropertyList` | Named status/data outcome; reuse valid models and internal readers |
| `double GetBetaAngle(int bId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L605) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L637) | `GetBetaAngle` | Named status/data outcome; reuse valid models and internal readers |
| `IOSProperty PopulateBetaAngles(IEnumerable<Beam> forBeams)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L614) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L627) | `GetBetaAngle` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty PopulateMemberOffsets(IEnumerable<Beam> forBeams, OffsetAxis offsetAxis = OffsetAxis.Global)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L619) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L690) | Managed composition or delegation | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `IOSProperty PopulateMemberReleases(IEnumerable<Beam> forBeams)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L624) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L659) | Managed composition or delegation | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `bool AssignMemberEndOffsets(int memberId, double startY, double endY)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L629) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L727) | `CreateMemberOffsetSpec`, `AssignMemberSpecToBeam` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `bool AssignMemberEndOffsets(int[] memberIds, double startY, double endY)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L636) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L776) | `CreateMemberOffsetSpec`, `AssignMemberSpecToBeam` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `bool AssignMemberEndOffset(int memberId, int location, double offsetY)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L641) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L748) | `CreateMemberOffsetSpec`, `AssignMemberSpecToBeam` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `bool AssignMemberEndOffset(int[] memberIds, int location, double offsetY)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L647) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L800) | `CreateMemberOffsetSpec`, `AssignMemberSpecToBeam` | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `bool AssignMemberEndOffset(int memberId, OffsetLocation location, double offsetY)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L652) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L766) | Managed composition or delegation | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |
| `bool AssignMemberEndOffset(int[] memberIds, OffsetLocation location, double offsetY)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs#L658) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L819) | Managed composition or delegation | Explicit mutation outcome and qualified identity; review dependency/unit/context semantics |

### Four adapter-only conveniences

| Signature | Source |
| --- | --- |
| `HashSet<Plate> GetIsotropicMaterialAssignedPlates(string mName, IEnumerable<Plate> allPlates)` | [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L406) |
| `HashSet<Beam> GetIsotropicMaterialAssignedBeams(string mName, IEnumerable<Beam> allBeams)` | [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L438) |
| `HashSet<Plate> GetIsotropicMaterialAssignedPlates(string mName, IOSGeometry geometry)` | [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L473) |
| `HashSet<Beam> GetIsotropicMaterialAssignedBeams(string mName, IOSGeometry geometry)` | [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L494) |

### Seven private adapter helpers

| Signature | Source |
| --- | --- |
| `HashSet<MaterialProperty> GetAllIsotropicMaterialProperties(Func<int, MaterialProperty> materialResolver)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L329) |
| `void CreateHSectionProperty(SectionProperty sp)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1730) |
| `void CreateCSectionProperty(SectionProperty sp)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1774) |
| `void CreateLSectionProperty(SectionProperty sp)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1796) |
| `void CreatePipeSectionProperty(SectionProperty sp)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1813) |
| `void CreateTubeSectionProperty(SectionProperty sp)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1829) |
| `void PopulateMaterialFor(MaterialProperty mat, IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1845) |

### Additional delegated native targets

The following nineteen targets are called in concrete section-model implementations but not directly by OSPropertyAdapter. They bring audited static native targets to 75. A call site is not proof of a correct native result or that every model path is usable. Abstract declarations are not throwing/no-op implementations and are not counted as native targets.

| Additional native target | Section implementation |
| --- | --- |
| `CreatePrismaticCircleProperty` | [PrismaticCircleSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticCircleSection.cs#L38) |
| `CreatePrismaticGeneralProperty` | [PrismaticGeneralSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticGeneralSection.cs#L63) |
| `CreatePrismaticRectangleProperty` | [PrismaticRectangleSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticRectangleSection.cs#L47) |
| `CreatePrismaticTeeProperty` | [PrismaticTeeSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticTeeSection.cs#L57) |
| `CreatePrismaticTrapezoidalProperty` | [PrismaticTrapezoidalSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticTrapezoidalSection.cs#L52) |
| `CreateTaperedIProperty` | [TaperISection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/TaperISection.cs#L71) |
| `CreateTaperedTubeProperty` | [TaperTubeSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/TaperTubeSection.cs#L52) |
| `CreateUPTTable` | [UptAngleSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptAngleSection.cs#L49) |
| `AddUPTPropertyANGLE` | [UptAngleSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptAngleSection.cs#L50) |
| `AddUPTPropertyCHANNEL` | [UptChannelSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptChannelSection.cs#L53) |
| `AddUPTPropertyDOUBLEANGLE` | [UptDoubleAngleSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptDoubleAngleSection.cs#L53) |
| `AddUPTPropertyGENERAL` | [UptGeneralSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptGeneralSection.cs#L89) |
| `CreatePropertyFromUPTTable` | [UptGeneralSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptGeneralSection.cs#L91) |
| `AddUPTPropertyISECTION` | [UptISection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptISection.cs#L89) |
| `AddUPTPropertyPIPE` | [UptPipeSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptPipeSection.cs#L56) |
| `AddUPTPropertyPRISMATIC` | [UptPrismaticSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptPrismaticSection.cs#L57) |
| `AddUPTPropertyTEE` | [UptTeeSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptTeeSection.cs#L53) |
| `AddUPTPropertyTUBE` | [UptTubeSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptTubeSection.cs#L43) |
| `AddUPTPropertyWIDEFLANGE` | [UptWideFlangeSection.CreateSectionInStaadInternal](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptWideFlangeSection.cs#L56) |

## 7. Source reuse and dependencies

| Source family | Reusable behavior | Required adaptation |
| --- | --- | --- |
| MaterialProperty | Material identity, elasticity/strength/thermal/damping fields and assignments. | Adapt private Units/Shared dependencies; preserve modulus/strength dimensions; distinguish name, index, ID and mutation status. |
| OffsetSpecification / ElementNodeOffset / ElementOffset | End/corner identity, coordinate selectors and grouping. | Qualify native success/NoOffset before returning a zero singleton; immutable identity and axes/units are required. |
| MemberReleases / Releases / Springs / MpFactors | Six DOFs, springs, scalar/directional MP fields and grouping. | Validate -2/-3 semantics and buffer types; no direct unchecked casts or false-to-fixed interpretation. |
| PlateElementThickness / PlateThickness | Four incidence values and assignments. | Validate count/status and triangle conventions; source model ID changes occur only after qualified creation. |
| GenericSection and concrete models | Polymorphic table/prismatic/tapered/UPT sections, schema-based fields and native creation. | Move raw COM creation into internal services; verify nine layout differences; no public proprietary interop types. |
| SectionFactory / SectionPropertyServices / IGenericSectionExtensions | Factory selection and grouping useful to high-level batches. | Separate pure grouping from native calls, retain explicit unsupported kinds and stable identity. |
| Private Sections.Domain repositories and profiles | Catalog/shape/designation and source composition. | Do not import the whole private catalog or proprietary binaries. Define a small injectable/resolved catalog boundary and reuse approved models selectively. |
| Current repository infrastructure | Beam/Plate geometry, units, property store, strict native readers and wrapper.StaadVersion. | Extend only required dependencies. Property functions remain in one adapter; no wrapper changes needed. |

The private bottom-cover ISectionWithCoverPlates branch passes TC where BC is intended; the direct composite wrapper and adapter dispatch pass their supplied type. Extended material creation stores a Created/Updated status into MaterialProperty.Id, while hard-coded E/G ×1000 helpers and an inconsistent extended G conversion also require review. The inactive-list and extended-create wrappers are explicitly obsolete(error=true) in source. The architecture report expands these findings.

## 8. Reproducibility evidence

### Scoped help pages

| Year | Page | Help build | Names / details | SHA-256 |
| --- | --- | --- | --- | --- |
| 2024 | [group___property_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_group.html) | 24.00.02.354 | 0 / 0 | `759b342a7da6b42aff72a0e872569cadb8eed937baa8db3b55525e91d5abf9ab` |
| 2024 | [group___property_create_section.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_section.html) | 24.00.02.354 | 18 / 18 | `36075965e4bcc0961e3c33898f070978a1e451f1c65afce8a984bc13188b07dd` |
| 2024 | [group___property_create_u_p_t_section.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html) | 24.00.02.354 | 17 / 14 | `8ac82ed5baa0839a3b8688833d72c067bf89bf446b1ca364849f6a7b9dc8734b` |
| 2024 | [group___property_beta.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_beta.html) | 24.00.02.354 | 2 / 2 | `a79c550a37d3bf94596dd86086932f5b23d29e991d1fd3f259560afa038db532` |
| 2024 | [group___property_get_section.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_section.html) | 24.00.02.354 | 29 / 29 | `8bb06cb869924314a337e1c589610216bb9fd7177ccd2180ecbe1dfc00c2563a` |
| 2024 | [group___property_get_u_p_t_section.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_u_p_t_section.html) | 24.00.02.354 | 13 / 13 | `99348a9382ccd89b85b762766bf4544953231ca9259e1e3df773e68e3043ba0a` |
| 2024 | [group___property_create_spec.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_spec.html) | 24.00.02.354 | 17 / 17 | `61a09d75627ee9dbcad54557a23371502fc1a8d39eb0baa22e51b65640ef197e` |
| 2024 | [group___property_get_spec.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_spec.html) | 24.00.02.354 | 36 / 36 | `91b39f1607a9168b7e8f6800cb8eee4b7c76b69b0acadc1341be2fca3092995e` |
| 2024 | [group___property_assign_section.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_assign_section.html) | 24.00.02.354 | 7 / 6 | `03185aa3ed3c77101307a558d3ba96ef93d62539b003db7527d36ce1b9566538` |
| 2024 | [group___property_create_mat.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_mat.html) | 24.00.02.354 | 6 / 6 | `4ae998af067231306604a325bbb8bff06aa17ba102084f29edfd530992a1b6e2` |
| 2024 | [group___property_get_mat.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_mat.html) | 24.00.02.354 | 24 / 24 | `6a4dce5799f393a8db602c0a042872bfbb28ac436288b44a1fcf5c57793e9dd5` |
| 2024 | [group___property_assign_mat.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_assign_mat.html) | 24.00.02.354 | 6 / 5 | `afe39ba91ac53eef8c841e48111320ca81b1e89d75a8e67d0c2aa57395006d99` |
| 2024 | [group___property_create_attri.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_attri.html) | 24.00.02.354 | 2 / 2 | `9ac1c8c11450402d2f229834a2120a6946a0de2d13cacb66bb5ad97bdd28603e` |
| 2024 | [group___property_get_attri.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_attri.html) | 24.00.02.354 | 11 / 11 | `13c65155f375e7c2226d90b73efa5ab0aaa644ee1c885b8d814ac1121a7e2423` |
| 2024 | [group___property_assign_attri.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_assign_attri.html) | 24.00.02.354 | 2 / 2 | `5ad1413949d1de85c306470fa81a8e3f5667f9d707073e2b7d12160dcb6f40b4` |
| 2024 | [group___property_get_ele_property.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_get_ele_property.html) | 24.00.02.354 | 10 / 10 | `dbdaed38aa206dad3f35b152012d925fb3e0901632d0ab28ba1c48412347c265` |
| 2024 | [group___property_publish.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_publish.html) | 24.00.02.354 | 2 / 2 | `5dfe814eb075ec6f06d5a5aec92e3b6505dbdc5898f44a0b1bf1c980570bcf01` |
| 2024 | [group___property_standard_section.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_standard_section.html) | 24.00.02.354 | 2 / 2 | `44e8b7a326034805f9646f0dc255d1c96f58b9add30e2b1e0b3ee9dfc7ad8c5a` |
| 2025 | [group___property_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_group.html) | 25.00.01.424 | 0 / 0 | `a1d86b5ccfbf0a7084145e243ac9f986a30999e12f8ac147bc66aee02b7bfec6` |
| 2025 | [group___property_create_section.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_section.html) | 25.00.01.424 | 18 / 18 | `64a25a202c3d79fb678b61ac24085c75874d1ec650845ababc2f21901b9ee515` |
| 2025 | [group___property_create_u_p_t_section.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_u_p_t_section.html) | 25.00.01.424 | 17 / 17 | `7e85a08a920df24b6e2e6030e276db7274c7de894f204d88ce54abd97097a335` |
| 2025 | [group___property_beta.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_beta.html) | 25.00.01.424 | 2 / 2 | `31bc7689f7212cc4331de2367b7e586c1296a2c3d6dff2dd28de988e951c0680` |
| 2025 | [group___property_get_section.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_section.html) | 25.00.01.424 | 29 / 29 | `e966cf7333112a5b483cf3c1af3f922c02a478e558da5b62dcf25ac002112b33` |
| 2025 | [group___property_get_u_p_t_section.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_u_p_t_section.html) | 25.00.01.424 | 13 / 13 | `231fda3d108330b78f361b4ff21de95607098d5f0ee8178a7718977c7e9059cb` |
| 2025 | [group___property_create_spec.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_spec.html) | 25.00.01.424 | 17 / 17 | `6ecec20f30f879dda9ec0d7aa791088031e87d00c1ec4c3d3bc7020493e65f97` |
| 2025 | [group___property_get_spec.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_spec.html) | 25.00.01.424 | 36 / 36 | `c9f432fdf38beced8eb8eaca832c7dab7dfd5980cc2a034f4525f868b4991e6a` |
| 2025 | [group___property_assign_section.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_assign_section.html) | 25.00.01.424 | 7 / 6 | `6fcb5a198f3431ec57ec0e74b3d643416116f32ff60b9429b8a3d598032fe869` |
| 2025 | [group___property_create_mat.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_mat.html) | 25.00.01.424 | 6 / 6 | `9382ed92d1bcfc46f1a6fa2c6960ef77597beec64cb69771cdb165ffe3744ddd` |
| 2025 | [group___property_get_mat.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_mat.html) | 25.00.01.424 | 24 / 24 | `9f6f85b5d97b3d20327f256b143a9d8ebe1c457ed05aa9c26164722e65c1792d` |
| 2025 | [group___property_assign_mat.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_assign_mat.html) | 25.00.01.424 | 6 / 5 | `e2d7a3d7fec9eab3f4476d8fa78a75d9d920743d3a030290e2b399c55acef9a9` |
| 2025 | [group___property_create_attri.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_create_attri.html) | 25.00.01.424 | 2 / 2 | `ef57f1c9c9e4ad23a22131a1f6db7da84038090a7232230942c13f271173ece0` |
| 2025 | [group___property_get_attri.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_attri.html) | 25.00.01.424 | 11 / 11 | `43010b0c54e600971dbdbb4d82d32f95196bd7046554be897bb844579abdb802` |
| 2025 | [group___property_assign_attri.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_assign_attri.html) | 25.00.01.424 | 2 / 2 | `c1fd4c4b9f36d5ffa7ef0196a58a80d4339984aac7d593ded7054425f1ff63e5` |
| 2025 | [group___property_get_ele_property.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_get_ele_property.html) | 25.00.01.424 | 10 / 10 | `f77bf39c71278248f78ded8e6091dd4b209402a6d6a61f78c09b24d015f99b61` |
| 2025 | [group___property_publish.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_publish.html) | 25.00.01.424 | 2 / 2 | `eeff23793c87c509b92ba6a9187c4e9e3df9f8496c29203fbb34cdc04cd04e71` |
| 2025 | [group___property_standard_section.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_standard_section.html) | 25.00.01.424 | 7 / 7 | `9680440ff8844436ae4fe9154fb55a1692f56acc1108be4569962b6972691944` |
| 2026 | [group___property_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_group.html) | 26.00.00.340 | 0 / 0 | `921bc5ab1edb6d84e2650cf3aee80594c75feaec8865063795144e1188128a02` |
| 2026 | [group___property_create_section.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html) | 26.00.00.340 | 18 / 18 | `46c45ea1675f2e9dac2077b68f63f0cf309d12667901b064313325ff0ee4f68b` |
| 2026 | [group___property_create_u_p_t_section.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html) | 26.00.00.340 | 17 / 17 | `34500c7c0d5a81ec50fd7de668cb13409a74fcacf4aeb7630a8c3723cfc9e635` |
| 2026 | [group___property_beta.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_beta.html) | 26.00.00.340 | 2 / 2 | `6768092445e796a7301289b01f41042fed01388d9af108504f6944ae721830ab` |
| 2026 | [group___property_get_section.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html) | 26.00.00.340 | 29 / 29 | `afda493745551fa593fa5847ffa7809f3e11d68fc2e7a9be70c50d97803cc43f` |
| 2026 | [group___property_get_u_p_t_section.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html) | 26.00.00.340 | 13 / 13 | `285e8d03f50f648a8d8158b47330f3e7b600cc1f83bbeba62d3708b88c6bd55a` |
| 2026 | [group___property_create_spec.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html) | 26.00.00.340 | 17 / 17 | `0c9723447270dd746b8b5bceda02fe34a80880b2a8327261bf789d1f9977b389` |
| 2026 | [group___property_get_spec.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html) | 26.00.00.340 | 36 / 36 | `23f434b2d3c1385ba07a01deb46c7b7bef56228c90e71f7a1643d106d724ebec` |
| 2026 | [group___property_assign_section.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html) | 26.00.00.340 | 7 / 6 | `182db160fef9735d3278edfc6317420c7515906e434d79d778e5bf0be44cd0c8` |
| 2026 | [group___property_create_mat.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_mat.html) | 26.00.00.340 | 6 / 6 | `2cc3cab38da9c0da60278cca655350e9a4a4c38f4c7b8ca88d80abfcb22108cd` |
| 2026 | [group___property_get_mat.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html) | 26.00.00.340 | 24 / 24 | `b96f7f28a38593d2b5c9fcada3e772fef3870a93b0763191979b8296761932f0` |
| 2026 | [group___property_assign_mat.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_mat.html) | 26.00.00.340 | 6 / 5 | `dac3a99b393afc77b3387519c8e0dade59ae31fab5425153ffe353c000ac094f` |
| 2026 | [group___property_create_attri.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_attri.html) | 26.00.00.340 | 2 / 2 | `7f81cfffa183afd2fb89c2bb527548664d3e72c3223bf7706b94a5b87ca2855f` |
| 2026 | [group___property_get_attri.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html) | 26.00.00.340 | 11 / 11 | `81f4d20a330ce323c1fe0874317da9588842c4e0ef6be156a9499110a408271d` |
| 2026 | [group___property_assign_attri.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_attri.html) | 26.00.00.340 | 2 / 2 | `cf06e2a7aa45b90ff4d9a538c16c0d911c916f01957a8f14fea35837d7b768cc` |
| 2026 | [group___property_get_ele_property.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html) | 26.00.00.340 | 10 / 10 | `51c90a99b97a331e4011fa1054c81ee4ad93c2d9176fa2243f34d8c2a12a5b98` |
| 2026 | [group___property_publish.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_publish.html) | 26.00.00.340 | 2 / 2 | `60dbff3ce20430dbef9083f3dee58c604a86e62151cdc38ecabc979e29e2484b` |
| 2026 | [group___property_standard_section.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html) | 26.00.00.340 | 7 / 7 | `1aa6b1a5442f140dda6c4805d8462522b6278c3b06d83b09379cb74ee16f1bf4` |

### Supplemental native class listings

| Year | Native class | Listed names | SHA-256 |
| --- | --- | --- | --- |
| 2024 | [Property native class](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_property_u_i.html) | 238 | `7a04968d3ded23a05d22d9999f62c1a8165262dfd5a475d3195d57df2e441beb` |
| 2025 | [Property native class](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_property_u_i.html) | 244 | `6c27f68d3fee848d980dba5f2164bac83b4f39ff79ba548325469744b41ecdd6` |
| 2026 | [Property native class](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_property_u_i.html) | 244 | `61227665ed182234baadf6458aa037ff677d2a7d1633200811d9b67d814b38d0` |

### Generated headers

| Year | Header | SHA-256 |
| --- | --- | --- |
| 2024 | [Generated Property header](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/ospropertyui_8h_source.html) | `3c618f9d0d1e98d1b7c583d005b3c7a9f67bf078ac301b548fcb2cccbb2eaf0b` |
| 2025 | [Generated Property header](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/ospropertyui_8h_source.html) | `9c5ddb94e7d7e4f58d20dbd1e0aa27489f8230bcbbe08102f19bf9712bb83538` |
| 2026 | [Generated Property header](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/ospropertyui_8h_source.html) | `0faf2f96432a0eecbd18c0d06085a57aa3b4d9fe690ad2b2a2ddfd0ca7f3bd3a` |

### Passive interop inspection

| Year | Assembly | Methods | SHA-256 |
| --- | --- | --- | --- |
| 2025 | [Installed interop assembly](file:///C:/Program%20Files/Bentley/Engineering/STAAD.Pro%202025/STAAD/StaadPro.Interop.dll) | 218 | `CC724993AC9119138B7323E8C684997257E2851DE62E1551BA5D6D67A9DADC19` |
| 2026 | [Installed interop assembly](file:///C:/Program%20Files/Bentley/Engineering/STAAD.Pro%202026/STAAD/StaadPro.Interop.dll) | 218 | `6D153E80ED356A1B167ABB2EF19227FCFF510707A578AD304154652CC6C1F6D4` |

Both identities are `StaadPro.Interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b4d201f8319cef86`. All 218 method signatures and parameter metadata match between 2025/2026; no enum parameter types were exposed by this interface inspection. Separate reflection-only processes prevented assembly identity unification. The expected 2024 DLL was not found; this is not evidence of missing native COM support.

### Current source snapshots

| Source | SHA-256 |
| --- | --- |
| [IOSProperty.cs](../../src/StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs) | `c4f3bfa21bd3bd60ef9b37833dec199408f9345bf18bfbf240c1acd80795d4ec` |
| [OSPropertyAdapter.cs](../../src/StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs) | `d3075faa6b92777a8d95868229f37b60898fca596cb4b400cdfe09332960562c` |
| [OpenStaadWrapper.cs](../../src/StaadPro.Interop/Models/OpenStaadWrapper.cs) | `1c0b69fe545a379262ad70cb1d1f9c45de872aba92e6eb4df218441455628692` |
| [Beam.cs](../../src/StaadPro.Interop/Entities/Beam.cs) | `6bb4f962f01b843672ae3284490c0ec05a91db9b8484c6913190d9de35822e19` |
| [StaadPro.Interop.csproj](../../src/StaadPro.Interop/StaadPro.Interop.csproj) | `035570ef3e35b52a2772e2ec96b9bf886ce3d08d6e555c3d51ffd870de9f1a59` |

### Strictly read-only main-source snapshots

| Source | SHA-256 |
| --- | --- |
| [Adapters/Interfaces/IOSProperty.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs) | `7b4184fb1e7f25982a1e3c5225affbad1678fb9b0f8af161aafa33a0140c0040` |
| [Adapters/Models/OSPropertyAdapter.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs) | `cfd93f64c48bcc31256c7f0cfe88eff3a5cc354d3cb46cdb04d1542ffeaf7171` |
| [Models/OSProperty/CastellatedSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/CastellatedSection.cs) | `826c92fd109637a6dee5337e436c8812d25ab2fad6ec9fecdf3161285c539fb7` |
| [Models/OSProperty/ColdChannelLippedSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/ColdChannelLippedSection.cs) | `a648f5e533c5fcabd41fac9a40aa65c46f7d5d4a15a8748c1132943838cbecfc` |
| [Models/OSProperty/ColdChannelSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/ColdChannelSection.cs) | `62dceb090788cf53d770f92f7503ee547d3782730fdee1aceedc75b5c2467d8d` |
| [Models/OSProperty/CSectionDoubleProfile.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/CSectionDoubleProfile.cs) | `4857db36588827055d689dd3fb77b6915e7555ce48a68df175b3cf0115ee0d1b` |
| [Models/OSProperty/ISectionComposite.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/ISectionComposite.cs) | `7a84b9d154b0546dfd7a6c35d691ea3dd843e065f40e3c4fd78b4cbefd389fa5` |
| [Models/OSProperty/ISectionDoubleProfile.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/ISectionDoubleProfile.cs) | `d94925b8d8eec532f18871557ed0f5bd24768c0c06181a21f2801222f335d7d2` |
| [Models/OSProperty/ISectionWithCoverPlates.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/ISectionWithCoverPlates.cs) | `79139a01ea50c60ad9991f0d9d346be8a0f0c64c0632732109047fc4742c11e4` |
| [Models/OSProperty/LOrBoxSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/LOrBoxSection.cs) | `f51db7c386feafebc959726a8a90c65dead014c1f77e95515fcf56363e00f434` |
| [Models/OSProperty/LSectionDoubleProfile.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/LSectionDoubleProfile.cs) | `fd35b5e420b791cdf0a37b1eb2b30f3e965c0ddd80c828dd457ff972dc34feba` |
| [Models/OSProperty/PipeSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PipeSection.cs) | `f9093faa164edf87463324e3d4166cd6cdf4564568ddd2372d4f688e7517bc68` |
| [Models/OSProperty/PlateStrip.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PlateStrip.cs) | `0fc5c001c62e68cd052c56080f12ea388174e283e7284a1c011aa34129479c25` |
| [Models/OSProperty/PrismaticCircleSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticCircleSection.cs) | `866deaa8d93aa81cf31de452703032c88dd21e091c5143d6e3b9ceba15610d8b` |
| [Models/OSProperty/PrismaticGeneralSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticGeneralSection.cs) | `e8d697e4187e5351e7a9b423c111b75cc4735e61839c7f3bbe8d4ee25ab3db40` |
| [Models/OSProperty/PrismaticRectangleSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticRectangleSection.cs) | `ef1fe2c59dbaf721a5b181c1e215506fe328a0bcad2eefdb48f69f3a282e89ac` |
| [Models/OSProperty/PrismaticTeeSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticTeeSection.cs) | `00b1c7aa7da2fdb96e0cbed547f3760eaa1b6f95e5610cc0d002343f407696c1` |
| [Models/OSProperty/PrismaticTrapezoidalSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticTrapezoidalSection.cs) | `f61d9b8daf5f5abfd1ed5d2e99e013af7a493050dca41f0f26ba9fa45390bf13` |
| [Models/OSProperty/SolidRoundSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/SolidRoundSection.cs) | `1496050614913b3a904368b381c7a92e89a130ea4d12a9cb7e5cf3274cea9f67` |
| [Models/OSProperty/TaperISection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/TaperISection.cs) | `cfdc8eaba326d3367022e99de7b9a6b64313dc808b48b670f61e1723e419ebef` |
| [Models/OSProperty/TaperTubeSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/TaperTubeSection.cs) | `f35e199afe53b09b5cfbd53fb3d2162f40b8640c3bb37f0f11ec2d71bb7b22b5` |
| [Models/OSProperty/TCISection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/TCISection.cs) | `8aac0696d6fb08e142b93c585919c743d9552a12bcf264a0f58d62c417235475` |
| [Models/OSProperty/UptAngleSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptAngleSection.cs) | `47fc7724f9c7330096698406742fca9631eefcf20f0610ffa03b56f54490c846` |
| [Models/OSProperty/UptChannelSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptChannelSection.cs) | `b6f2581ade1e608d99a6b36dc4ffd2b412c27f278d108fc36642e51be10a8d1a` |
| [Models/OSProperty/UptDoubleAngleSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptDoubleAngleSection.cs) | `44db0e485e6c749e08e5bd054dd40d8033e6dba2f692de345f3eb3470fedd2c2` |
| [Models/OSProperty/UptGeneralSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptGeneralSection.cs) | `01222b89ba2d331c67179810ec1d3fc0b600f783fdabcc22fadd1efd499f1dda` |
| [Models/OSProperty/UptISection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptISection.cs) | `2df153dee4fbdfbde1bee533d753399f3ce63080ed3838af238c6a036014687a` |
| [Models/OSProperty/UptPipeSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptPipeSection.cs) | `b30f6a42529636ab80b041433b603ca44ca18d627b8aeda32f2b166474ddee1e` |
| [Models/OSProperty/UptPrismaticSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptPrismaticSection.cs) | `83cd2566eb413b61bbbe75e24b610a9e5c2271d9e7976ce00b6db7559f4463ac` |
| [Models/OSProperty/UptTeeSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptTeeSection.cs) | `154afbda28e853d06c682201d3f25a7cd8c11cda07a0bac8ffa5f901727117be` |
| [Models/OSProperty/UptTubeSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptTubeSection.cs) | `63b36bff87e9a2a7cc5cbe1ebfc373a4f188e5b8fa5f169915d1feaf7313e22d` |
| [Models/OSProperty/UptWideFlangeSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptWideFlangeSection.cs) | `c436718e05388a99f76e76f8ebfff01999e8d8094cd189e41a3828453700d7db` |
| [Base/OSProperty/GenericSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Base/OSProperty/GenericSection.cs) | `59ca21957b148d8107414ebbf77288132a8d3b500ba7af1718ad327b47bce73c` |
| [Base/OSProperty/InertialSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Base/OSProperty/InertialSection.cs) | `9ab6ae7deda561a52098180aae57bb1cd988ab03ac1b824db2ff93d3396b6e7a` |
| [Base/OSProperty/PrismaticSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Base/OSProperty/PrismaticSection.cs) | `78cc2660e38f6c52451321667058e23ce6b390c85c3252c22c90dc415ef0e21c` |
| [Base/OSProperty/StandardSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Base/OSProperty/StandardSection.cs) | `b9cdf9d6716e99f2acbd3d362c530536e61f51e9775299fb8cab7cf1e1d920bc` |
| [Base/OSProperty/UptSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Base/OSProperty/UptSection.cs) | `2c0e8331bde55f0c9b3e0ed934928fdf8f96dc9780ff4a293bd29f72dc2e7bca` |
| [Interfaces/OSProperty/ICastellatedSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Interfaces/OSProperty/ICastellatedSection.cs) | `3f0e08c63709f32d1f442b35f8c0c19d7a4bba3cd809a5f791977a24d09d747e` |
| [Interfaces/OSProperty/IColdChannelSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Interfaces/OSProperty/IColdChannelSection.cs) | `d9f4e9ddb6935aa77f2829815ba33b832aacb7c4a1a3c65242ca20fb483ddf46` |
| [Interfaces/OSProperty/IGenericSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Interfaces/OSProperty/IGenericSection.cs) | `8c34b25081bc14718c4b72ad045330a8a770ef0b2512b6f468c5cec16f7169dc` |
| [Interfaces/OSProperty/IInertialSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Interfaces/OSProperty/IInertialSection.cs) | `bfa9f989b295eff3b3dccd3b0ddaeda381c7caac09771c0f8498aa46c9dec81f` |
| [Interfaces/OSProperty/ILOrBoxSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Interfaces/OSProperty/ILOrBoxSection.cs) | `9ee3e1cd275ad0bcaff813c14e8eb0f914b41a41161464eb43e5baa8eb917e7b` |
| [Interfaces/OSProperty/IPipeSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Interfaces/OSProperty/IPipeSection.cs) | `d4a3a1fa957e21dc1f97a82f6f034a9ace0fe9a4f01ff90c80d7d734529d20e2` |
| [Interfaces/OSProperty/IPlateStrip.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Interfaces/OSProperty/IPlateStrip.cs) | `321827c7313ebf0cfcff32265d29c4f0c7d5a72dc72a6df4b89ecc3e67229869` |
| [Interfaces/OSProperty/IPrismaticSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Interfaces/OSProperty/IPrismaticSection.cs) | `03e4618939cc85a5fb054bbf33541899f9012a96dbc65f4b5dd3f8cba37dd630` |
| [Interfaces/OSProperty/ISolidRoundSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Interfaces/OSProperty/ISolidRoundSection.cs) | `9d1a93ace567edb049f914dfaef808bba538985f6ccf5000d05a4d44bfc70585` |
| [Interfaces/OSProperty/IStandardSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Interfaces/OSProperty/IStandardSection.cs) | `04411662c74781aad7445fc86309c94ee4389ac28f5ad397ebc81e353a524a7e` |
| [Interfaces/OSProperty/ITaperSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Interfaces/OSProperty/ITaperSection.cs) | `caa325fea68a7f4d8a077f3d98004fcec17baa232a995d3c6f2d81a57fc3e8b4` |
| [Interfaces/OSProperty/ITaperTubeSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Interfaces/OSProperty/ITaperTubeSection.cs) | `53b03fecab8ed51ef8ee543676606868facf8f65bb263e8305edd30e88460b47` |
| [Interfaces/OSProperty/ITCISection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Interfaces/OSProperty/ITCISection.cs) | `8f2f09c2df55d38e7a69a63e5c97324120db2ebdb93e35cc4de807c75532f03b` |
| [Interfaces/OSProperty/IUptGeneralSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Interfaces/OSProperty/IUptGeneralSection.cs) | `860b19e52b82a04b0be9bb40070eba8c80ddef33da375c91924bf63eb0ab860f` |
| [Interfaces/OSProperty/IUptISection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Interfaces/OSProperty/IUptISection.cs) | `9fd66213ae7b22ceee6370779195fbf8c6db9c13e32686376782bbab90da52e2` |
| [Interfaces/OSProperty/IUptPipeSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Interfaces/OSProperty/IUptPipeSection.cs) | `6b3a806896c3d316dc7b47ce70f4823138359d0cb2729fc9995eb9e5f638537a` |
| [Interfaces/OSProperty/IUptPrismaticSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Interfaces/OSProperty/IUptPrismaticSection.cs) | `270dab9fea7f5db39bffe5e00a2d0fe06e5189b76699e9f5523ab6746e8306c1` |
| [Interfaces/OSProperty/IUptSection.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Interfaces/OSProperty/IUptSection.cs) | `7983549cd6433ae3cc24548f98ade55e3fc83a49baa524c8725a8e8b913f6343` |
| [Services/SectionPropertyServices.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/SectionPropertyServices.cs) | `c3a8a3209c986ff5465104ab28ec0e56c9643e6ccab642f2c88aa983a2645698` |
| [Extensions/IGenericSectionExtensions.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Extensions/IGenericSectionExtensions.cs) | `337d34da6ab8a02940ee5a4777af4260329105c77c5a115efc7cc6a1ccc99a14` |
| [Factories/SectionFactory.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Factories/SectionFactory.cs) | `ba167b6e46f4d4bba2be6f73ad38ad4721750cbf2bac06138b830de14041719f` |
| [Entities/MaterialProperty.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/MaterialProperty.cs) | `534c584482fc3463634573136cf1f35f82a4cba12a6bfa6efa8acce39970e6ec` |
| [Entities/OffsetSpecification.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/OffsetSpecification.cs) | `be92850983fc2765b233eaa2b9d55f386805cd48edc5c90a9f32c25f8ac0fd95` |
| [Entities/PlateThickness.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/PlateThickness.cs) | `7dd1a5ce0b612bd3d32ddbeb61199c76ea3bff51a6d73b3e9a5f905122b8c77d` |
| [Entities/PlateElementThickness.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/PlateElementThickness.cs) | `130b6e29caa883da1f056b0b33e860b5930bcf53dc9217974cf4773dbbf8cea4` |
| [Entities/MemberReleases.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/MemberReleases.cs) | `48cd0a7a6c6b8f56a2af8d9f1978aad796f25bbbfd70d9106d7b24774e5bb67c` |
| [Entities/Releases.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/Releases.cs) | `53a5a6e2cf925e5d990ea635d117703c6623faccdd09fef0b06972ab3d031c9b` |
| [Entities/ElementNodeOffset.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/ElementNodeOffset.cs) | `21d05fed728d3aa97d10f0e37396336de9c85cfdcbfe7da3dbf1f141a44a746c` |
| [Entities/ElementOffset.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/ElementOffset.cs) | `97efcab9fa4fee567486b1ce64f11c84d6d0f02858b7a8681837bc6e73eae208` |
| [Entities/ElementReleases.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/ElementReleases.cs) | `758cfd8e17dfef93ef86f9890bc63d98303f756166431973b2d317d7fa9a3c84` |
| [Entities/SectionProperties/SectionProperty.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/SectionProperties/SectionProperty.cs) | `1e357b352b641ae0bd16b1b6c503e793b74769a5c9504716392d46524d3a7f31` |
| [Enums/OSProperty/SectionPropertyType.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Enums/OSProperty/SectionPropertyType.cs) | `9b3c2ce8a34b15745a0d32144d3053764e2d68dda9cb7b1a07f8e460ece1bb7a` |
| [Enums/OffsetLocation.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Enums/OffsetLocation.cs) | `36e85b5dba41d5e18aed9183478c9d3e29f8e4b525268838e3078e9db6fce5e7` |
| [Enums/OffsetAxis.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Enums/OffsetAxis.cs) | `e2b4f9d803b10f60017a6fc6e6c291c61fe3e3d3749646c982a211f5cbec0d03` |
| [Enums/ReleaseType.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Enums/ReleaseType.cs) | `cbd2bb560b214286620e21ef7cde8b1c9df6e69e1dca6b56fc5f876c28847174` |
| [Models/OpenStaadWrapper.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OpenStaadWrapper.cs) | `83737733d57a672313385f99ea0ca8ffa10f1917fb6a33b89e43ce27e3f37b14` |
| [Models/OffsetAwareMemberEndForces.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OffsetAwareMemberEndForces.cs) | `f2a4d47299955163fb685af63d75fae70b8e2f2be340f627913b396e9a0c0dae` |

## 9. Verification boundaries

This inventory compared installed documentation and passive metadata and parsed current/private source. No COM object was activated, no model was edited, no native call was executed and no private project was built. No library tests, source implementation, targets or NuGet contents changed. Offline static analysis cannot certify marshaling, engineering semantics or version compatibility.

Local help/main-source links require the corresponding files on the reader’s computer. The repository reports and comparison data remain readable independently; native verification requires a suitable licensed installation.

## 10. First Property implementation outcome

Implemented **25 typed Property queries** through IOSProperty and the single non-partial OSPropertyAdapter: all 13 P2 native names and the first 12 P3 names in the documented allocation. Shared 2024–2026 contracts use one internal reader and strict value decoder. The 212-name union contains eight excluded entries, leaving 204 active candidates; **25 have managed routes and 179 remain pending**. GetPlateSectionPropertyRefNo is documented but absent from inspected vendor CLR metadata, so its acquired receiver may return Unsupported. Implementation coverage does not certify live COM or engineering behavior.

Passed 3,313 offline tests (413 new Property cases). Fifty expanded XML examples are included in the package-consumer validation inventory. See [the Property guide](../Property-Porting.md) for native qualification limits and source model reuse. Load remains parked behind its separate explicit approval checkpoint.

## 11. Subsequent management-port checkpoint

On 24 October 2026 the managed surface reached 75 overloads: 25 qualified observations and 50 main-project compatibility methods. The latest increment adds 25 methods for section creation, beta angles, material subsets, plate thicknesses and member offsets. Native route coverage is 54/204 active candidates, with 150 pending; this does not establish live qualification. The earlier per-function plans/coverage labels above are archived evidence. Use [the current Property guide](../Property-Porting.md) and [all 25 management contracts](../Property-Management-Ports.md) for implementation status and behavior.

## 12. Subsequent material and release workflow checkpoint

On 24 October 2026 the next 25 main-interface ports brought Property to 100 managed overloads and 57/204 active native routes, leaving 147 pending. The new material, uniform-thickness, offset and member-release workflows retain signatures and document operation-specific statuses, the legacy E/G scale, retained release modes and nontransactional mutations. Earlier per-function coverage/plans above remain archived evidence. See [the current Property guide](../Property-Porting.md) and [all 25 workflow contracts](../Property-Workflows.md) for current behavior and qualification limits.
