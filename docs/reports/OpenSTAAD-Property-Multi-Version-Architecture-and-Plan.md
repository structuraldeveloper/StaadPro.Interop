# OpenSTAAD Property multi-version comparison, architecture and implementation plan

**Review date:** 23 October 2026<br>
**Repository baseline:** main at 3b94e637839f0c3e28c1486d8f03f0a8af230b43<br>
**Scope:** All Property group sections installed with STAAD.Pro 2024, 2025 and 2026; native class/header listings; installed vendor interop metadata; current repository coverage; and the strictly read-only main project, including delegated section models.<br>
**Implementation updated:** 26 October 2026<br>
**Status:** Updated route coverage: 249 common + five optional operations, 195/204 active native routes; live qualification remains pending.

## Current implementation - documented P10 completion

Implemented the final four safely specified P10 operations: `RemoveAllElementNodeReleaseSpec`, `RemoveMemberFireProofingSpecFromBeam`, `GetPublishedProfileName` and `GetStaadProfileName`. Property now exposes **249 common overloads plus five separately acquired catalog queries**, covering **195 of 204 active documented native routes**. P10 is complete at 24/24. The remaining **nine P7/P8 contract gates** require transport, status or context evidence; they are not implemented by guessing.

The increment deliberately excludes the **fifteen supplemental Property discovery candidates**. **Reminder checkpoint:** present those candidates and the nine documented gates before further Property expansion or the next public release, and obtain separate approval for supplemental work. See [the four complete contracts and remaining gates](../Property-Profile-Names-and-Final-Cleanup.md).

The subsequent domain increment implements **25 Output routes**, bringing typed Output coverage to **40/60**. Validation passed **5,353 offline tests** with zero failures; package verification covers **1,002 selected XML examples**. These checks use managed native-signature fixtures; live 2024–2026 COM and engineering qualification remain pending.

| Phase | Implemented native routes | Remaining |
| --- | ---: | ---: |
| P2 | 13 | 0 |
| P3 | 23 | 0 |
| P4 | 24 | 0 |
| P5 | 17 | 0 |
| P6 | 18 | 0 |
| P7 | 42 | 4 |
| P8 | 25 | 5 |
| P9 | 9 | 0 |
| P10 | 24 | 0 |

Four pending P7 gates: `GetInactiveMemberList`, `GetAlphaAngleForSection`, `GetCentroidLocationForSection`, `GetAssignedAttributeByIndex`. Five additional P8 transport gates remain: `GetUptGeneralProfilePointsCount`, `GetUptGeneralProfileBoundaryPoints`, `GetUptGeneralStressLocationPoints`, `FindUPTTable` and `GetUserProvidedTableSectionList`. P9 and P10 are complete; only the nine listed P7/P8 gates remain in the documented allocation. None of these gates is counted as implemented.

## 1. Findings and recommended direction

The Property group tree contains **204 / 209 / 209 names** in the three reviewed years, with **199 / 207 / 207 detailed contracts**. The union has **212 names**, the intersection **201**. Eight detailed functions appear in 2025/2026; three different 2024 entries disappear, but those three are explicitly reserved and say not to use them. They are not supported legacy equivalents for the newer APIs.

All 201 common normalized native declarations match. Thirteen common non-example contracts differ: **four warrant focused version-policy review**, six clarify types/meanings, and three change formatting or cross-references only. The normalized 2025 and 2026 declarations, contracts and examples match.

The current IOSProperty and OSPropertyAdapter expose **249 common overloads plus five optional catalog queries** backed by 195 native dispatch routes, including qualified observations/mutations and source-compatibility methods. The main project has **103 interface overloads, 107 public adapter methods and seven private adapter helpers**. Its 56 direct native targets and nineteen additional targets in delegated section models provide substantial reusable work, but source presence is not native verification.

Use common internal services with narrow policies for section-value interpretation, shape/catalog interpretation, section-record identity and database-folder mutation status. Select the profile from **wrapper.StaadVersion**, which identifies the acquired native application. **wrapper.ApplicationVersion identifies the client/host assembly**, so it must not select native behavior.

Do not duplicate the entire adapter for each product year. Do not copy private models' raw COM methods into the public model layer or pull in the entire private Sections/Units/Shared project graph. Reuse their useful domain semantics behind a small managed boundary and internal polymorphic services.

| Measure | Finding |
| --- | ---: |
| Property group pages per year | 18 |
| Group names, 2024 / 2025 / 2026 | 204 / 209 / 209 |
| Detailed contracts, 2024 / 2025 / 2026 | 199 / 207 / 207 |
| Group-name union / intersection | 212 / 201 |
| Changed common native declarations | 0 |
| Newly documented detailed functions in 2025 | 8 |
| Reserved 2024-only summaries | 3 |
| Reserved names across the union | 5 |
| Native private helpers inside group inventory | 2 |
| Deprecated setters inside group inventory | 1 |
| Active public API candidates after those exclusions | 204 |
| Changed common non-example contracts | 13 |
| Version-policy candidates / clarifications / cosmetic changes | 4 / 6 / 3 |
| Other unchanged common contracts | 188 |
| Common functions whose examples change in 2025 | 75 |
| Normalized 2025-to-2026 changes | 0 |
| Current common Property interface / adapter domain methods | 249 / 249 |
| Optional catalog operations | 5 |
| Current Property native dispatch targets | 195 |
| Remaining active public candidates | 13 |
| Main interface / public adapter methods | 103 / 107 |
| Main adapter-only conveniences / private helpers | 4 / 7 |
| Main direct / additional delegated native targets | 56 / 19 |
| Main audited native target union | 75 |
| Reviewed group-union names without a main call target | 137 |
| Methods in each inspected 2025/2026 IOSPropertyUI | 218 |
| Group-union names represented in that metadata | 204 |
| Metadata names outside group inventory | 14 |
| Group names absent from inspected metadata | 8 |
| Supplemental class/metadata candidate union | 15 |
| Additional class-only implementation helpers excluded | 22 |

**195 of 204 active Property candidates have managed routes; 9 remain pending.** RawProperty and ComObject alone are binding properties, not additional typed methods. The 137 unmapped source names include eight reserved/private/deprecated entries; **129 active candidates have no main-source native target**. It would be misleading to call all 137 required new public methods. Conversely, matching a managed function name to a native name does not establish a dispatch.

The [contract annex](OpenSTAAD-Property-API-Coverage-and-Contract-Inventory.md) inventories every native parameter, direction, return contract, source signature and phase. The [comparison evidence](data/OpenSTAAD-Property-2024-2026-Comparison.json) retains all years separately, complete native tables, signatures, metadata and fingerprints.

## 2. References and methodology

| Reference | Documented build | Group names / detailed contracts |
| --- | --- | ---: |
| [STAAD.Pro 2024 Property help](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_group.html) | 24.00.02.354 | 204 / 199 |
| [STAAD.Pro 2025 Property help](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_group.html) | 25.00.01.424 | 209 / 207 |
| [STAAD.Pro 2026 Property help](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_group.html) | 26.00.00.340 | 209 / 207 |

| Section | 2024 names / details | 2025 names / details | 2026 names / details |
| --- | --- | --- | --- |
| [Property](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_group.html) | 0 / 0 | 0 / 0 | 0 / 0 |
| [Section: Create Profile Property](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html) | 18 / 18 | 18 / 18 | 18 / 18 |
| [Section: Create Profile from UPT Property](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html) | 17 / 14 | 17 / 17 | 17 / 17 |
| [Section: Beta Angle Operation Property](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_beta.html) | 2 / 2 | 2 / 2 | 2 / 2 |
| [Section: Get and Remove General Section Profile Property](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html) | 29 / 29 | 29 / 29 | 29 / 29 |
| [Section: Get and Remove UPT Profile Property](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html) | 13 / 13 | 13 / 13 | 13 / 13 |
| [Specification: Create Specification Property](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html) | 17 / 17 | 17 / 17 | 17 / 17 |
| [Specification: Get and Remove Specification Property](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html) | 36 / 36 | 36 / 36 | 36 / 36 |
| [Section: Assign Section to Element Property](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html) | 7 / 6 | 7 / 6 | 7 / 6 |
| [Material: Create Material Information Property](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_mat.html) | 6 / 6 | 6 / 6 | 6 / 6 |
| [Material: Get and Remove Material Property](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html) | 24 / 24 | 24 / 24 | 24 / 24 |
| [Material: Assign Material to Section and Element Property](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_mat.html) | 6 / 5 | 6 / 5 | 6 / 5 |
| [Attribute: Create Attribute Property](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_attri.html) | 2 / 2 | 2 / 2 | 2 / 2 |
| [Attribute: Get and Remove Attribute Property](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html) | 11 / 11 | 11 / 11 | 11 / 11 |
| [Attribute: Assign Attribute to Element Property](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_attri.html) | 2 / 2 | 2 / 2 | 2 / 2 |
| [ElementProperty: Functions Get Property information through Element Property](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html) | 10 / 10 | 10 / 10 | 10 / 10 |
| [Publish: Functions Related to Publish Information Property](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_publish.html) | 2 / 2 | 2 / 2 | 2 / 2 |
| [Standard Section: Functions related to standard section Property](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html) | 2 / 2 | 7 / 7 | 7 / 7 |

The review followed all Property subgroup links and scanned the other group pages for OSPropertyUI ownership. No additional Property-owned group function rows were found outside this tree. Similar filenames for Load or Support are not evidence that those interfaces belong to Property.

Summaries were reconciled with detail blocks, preserving summary-only and reserved entries. Native declarations, non-example contracts and examples were compared separately. Nested parameter/engineering tables were parsed without truncating outer tables. Doxygen-generated navigation, tooltips, headings and HTML styling were excluded from semantic comparison.

The main interface, adapter, section bases, concrete section models, interfaces and factory/helpers were parsed read-only with Roslyn. Native call sites were separated from plain managed delegation. Fully qualified Country/TypeSpecification spellings were normalized when comparing interface signatures.

Vendor IOSPropertyUI was inspected in separate reflection-only Windows PowerShell processes for 2025 and 2026. All 218 method signatures and parameter metadata match. This interface inspection exposes no enum parameter types. Both assemblies have the same identity but different byte hashes; separate processes prevented identity unification. No matching 2024 DLL was found at the corresponding expected STAAD path.

No COM object was acquired, no native function executed and no private project built. Documentation presence, metadata presence, native IDispatch availability, successful marshaling, unit correctness and package inclusion are separate evidence layers. The annex records all relevant file fingerprints.

## 3. Complete cross-version function matrix

Every row is a reviewed native Property name. “Summary only” means no detailed contract; reservations/private helpers are retained for completeness but excluded from public porting. Returns are native declaration types, not proposed managed results. The current mapping column identifies 195 implemented routes; the remaining 9 active candidates are pending and eight entries are excluded.

| Native Property name | 2024 | 2025 / 2026 | Contract classification | Main source call targets | Plan | Current typed mapping |
| --- | --- | --- | --- | --- | --- | --- |
| [CreateBeamPropertyFromTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#gaf2cc429be392fe527feed7dc33f78d6c) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1086) | P6 | Implemented route; live qualification pending |
| [CreateBeamPropertyFromTableWithCoverPlates](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#gab96ba7f158851c24a2eb289af7f17c6f) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L253) | P6 | Implemented route; live qualification pending |
| [CreateBeamPropertyFromTableComposite](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga50040de9e0075880f3f7285284b7c79a) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1770) | P6 | Implemented route; live qualification pending |
| [CreateWideFlangePropertyFromTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga78cec5f9be32c1061963f94ed050d331) | `VARIANT` | `VARIANT` | Unchanged | No target | P6 | `CreateWideFlangePropertyFromTable`; managed verified |
| [CreateBeamPropertyFromTableEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#gadce802ef32861d83b01a4226b234939c) | `VARIANT` | `VARIANT` | Unchanged | No target | P6 | `CreateBeamPropertyFromTableEx`; managed verified |
| [CreateTeePropertyFromTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga9623d2b80ee04d55f188772469ae23e7) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1740) | P6 | Implemented route; live qualification pending |
| [CreateChannelPropertyFromTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#gaa08e9a925a2afa0ba3abde267f4eadb1) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1780) | P6 | Implemented route; live qualification pending |
| [CreateAnglePropertyFromTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga6f1c73d26e890715030ddd5e1f765b20) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1803) | P6 | Implemented route; live qualification pending |
| [CreateTubePropertyFromTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga64ab21d46e0b14bdb71c9bd0f1e84c29) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1835) | P6 | Implemented route; live qualification pending |
| [CreatePipePropertyFromTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga8df35fc2730a95842893d45040aaa00b) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1819) | P6 | Implemented route; live qualification pending |
| [CreatePrismaticRectangleProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga4ef3ab387d4b23c1c8e3d3e2d7b7783d) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticRectangleSection.cs#L47) | P6 | `CreatePrismaticRectangleProperty`; managed verified |
| [CreatePrismaticCircleProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga016cb17310c1214ad731ef8603f74fcc) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticCircleSection.cs#L38) | P6 | `CreatePrismaticCircleProperty`; managed verified |
| [CreatePrismaticTeeProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga26721d08a647bddd960d0499b42ac5c9) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticTeeSection.cs#L57) | P6 | `CreatePrismaticTeeProperty`; managed verified |
| [CreatePrismaticTrapezoidalProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga0fdc7ad624d8f07839d85b017b518350) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticTrapezoidalSection.cs#L52) | P6 | `CreatePrismaticTrapezoidalProperty`; managed verified |
| [CreatePrismaticGeneralProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#gae35d506434e6db9abd1edcb540a9d1cb) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/PrismaticGeneralSection.cs#L63) | P6 | `CreatePrismaticGeneralProperty`; managed verified |
| [CreateTaperedIProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#gad01c26661a6e9a588c4e5cfb46ce9403) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/TaperISection.cs#L71) | P6 | Implemented route; live qualification pending |
| [CreateTaperedTubeProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga558bd4cf571d37c5f3520b2e7e56b9e5) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/TaperTubeSection.cs#L52) | P6 | `CreateTaperedTubeProperty`; managed verified |
| [CreatePlateThicknessProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_section.html#ga4ab7a973986b611d748302e8dcdc0154) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1167) | P6 | Implemented route; live qualification pending |
| [CreateUPTTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#gacffe1377c08a287a4b95c1fc315f4127) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptAngleSection.cs#L49) | P8 | `CreateUptTable`; managed renamed route, live pending |
| [AddUPTPropertyWIDEFLANGE](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#gaf8641a9ec57a7ad52f1f4ee44c4f5168) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptWideFlangeSection.cs#L56) | P8 | `AddUptPropertyWideFlange`; managed renamed route, live pending |
| [AddUPTPropertyWIDEFLANGEUNEQUAL](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga416ce6e066c01171353a67dceb72d404) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | `AddUptPropertyWideFlangeUnequal`; managed renamed route, live pending |
| [AddUPTPropertyWIDEFLANGECOMPOSITE](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#gad0aeaec8b6052eaa4e713b184506e935) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | `AddUptPropertyWideFlangeComposite`; managed renamed route, live pending |
| [AddUPTPropertyCHANNEL](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga762e3143fd9fa6d9ffc3f213af23d57f) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptChannelSection.cs#L53) | P8 | `AddUptPropertyChannel`; managed renamed route, live pending |
| [AddUPTPropertyANGLE](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga4d33b8f55788d2d6a28d75542179a38e) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptAngleSection.cs#L50) | P8 | `AddUptPropertyAngle`; managed renamed route, live pending |
| [AddUPTPropertyDOUBLEANGLE](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga7659bafc3884233e8006403264df40b2) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptDoubleAngleSection.cs#L53) | P8 | `AddUptPropertyDoubleAngle`; managed renamed route, live pending |
| [AddUPTPropertyTEE](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga6657114c7f25e838f5189e45571532d2) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptTeeSection.cs#L53) | P8 | `AddUptPropertyTee`; managed renamed route, live pending |
| [AddUPTPropertyPIPE](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga927bbbce72db4a45e0006b936840ffea) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptPipeSection.cs#L56) | P8 | `AddUptPropertyPipe`; managed renamed route, live pending |
| [AddUPTPropertyTUBE](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga3a2f714b28bce009a5df574409915b70) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptTubeSection.cs#L43) | P8 | `AddUptPropertyTube`; managed renamed route, live pending |
| [AddUPTPropertyGENERAL](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#gaa701fb0dc527b5cd73cb0c0d6616487f) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptGeneralSection.cs#L89) | P8 | `AddUptPropertyGeneral`; managed renamed route, live pending |
| [AddUPTPropertyISECTION](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga192963517f316eef4bf5cb2822d78424) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptISection.cs#L89) | P8 | `AddUptPropertyISection`; managed renamed route, live pending |
| [AddUPTPropertyPRISMATIC](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga828f857560168eb9d6a9809950454017) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptPrismaticSection.cs#L57) | P8 | `AddUptPropertyPrismatic`; managed renamed route, live pending |
| [CreatePropertyFromUPTTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga4d6c2edb2612f542b9a1596221a9f784) | `VARIANT` | `VARIANT` | Unchanged | [Delegated section](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OSProperty/UptGeneralSection.cs#L91) | P8 | `CreatePropertyFromUptTable`; managed renamed route, live pending |
| [GetUptGeneralProfilePointsCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#gac11402eeb7ce630b8a4b40b0a63cb83e) | Absent | `VARIANT` | Newly documented in 2025 | No target | P8 | Pending; Reviewed CLR metadata passes output count values by value, unlike the help declaration; compatible native transport is not qualified. |
| [GetUptGeneralProfileBoundaryPoints](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga9f135f3a5b84d9041a60bf5c276699c2) | Absent | `VARIANT` | Newly documented in 2025 | No target | P8 | Pending; Newer optional coordinate API requires a compatible acquired capability; reviewed by-value output metadata does not establish safe returned buffers. |
| [GetUptGeneralStressLocationPoints](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_u_p_t_section.html#ga206fc2be959408948d3e81aa6751ed7a) | Absent | `VARIANT` | Newly documented in 2025 | No target | P8 | Pending; Newer optional coordinate output metadata is not qualified for safe paired buffer retrieval. |
| [AssignBetaAngle](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_beta.html#gac25ed74556a088b204e59721179f1660) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1575) | P5 | Implemented route; live qualification pending |
| [GetBetaAngle](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_beta.html#ga7dbd2b865f74ec34570f04fad356f42b) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L631) | P2 | Implemented route; live qualification pending |
| [GetSectionPropertyValues](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga91afb41d03b9b4df33f3b47b36de3938) | `VARIANT` | `VARIANT` | Unchanged | No target | P4 | `GetSectionPropertiesForProfile`; managed verified |
| [GetSectionPropertyAssignedBeamCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga008bbb46f75923080b95eb63962f1acd) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L55) | P4 | Implemented route; live qualification pending |
| [GetSectionPropertyAssignedBeamList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga4cb2980c099d339e6f0d5532448f1bb1) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1056) | P4 | Implemented route; live qualification pending |
| [GetBeamSectionPropertyRefNo](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga3c67b414c91c210bf5cc4b70a2f3be74) | `VARIANT` | `VARIANT` | Unchanged | No target | P4 | `GetBeamSectionPropertyRefNo`; managed verified |
| [GetBeamSectionPropertyValuesEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga8d1dc394d29fa932cd97c281ab619ab6) | `VARIANT` | `VARIANT` | Unchanged | No target | P4 | `GetBeamSectionPropertyValuesEx`; managed verified |
| [GetSectionPropertyValuesEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga0497f1f458d11eb783f465a85dc6ad27) | `VARIANT` | `VARIANT` | Version policy candidate | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1019) | P4 | Implemented route; live qualification pending |
| [GetSectionTableNo](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga9c3b455750b4ed8046d78fecb0b8cbc7) | `VARIANT` | `VARIANT` | Unchanged | No target | P4 | `GetSectionTableNo`; managed verified |
| [GetBeamSectionPropertyTypeNo](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga37be18e3c326761c52fb42fa0773a97c) | `VARIANT` | `VARIANT` | Unchanged | No target | P4 | `GetBeamSectionPropertyTypeNo`; managed verified |
| [GetBeamSectionName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga398cd10fa2ef3710d2807797445be4bb) | `VARIANT` | `VARIANT` | Unchanged | No target | P4 | `GetBeamSectionName`; managed verified |
| [GetBeamSectionDisplayName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga967af878ba279f8d3a2fe58a0d209a14) | `VARIANT` | `VARIANT` | Unchanged | No target | P4 | `GetBeamSectionDisplayName`; managed verified |
| [GetCountryTableNo](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga5ed5bfb3b3caf8dd8d395858eb6bbdec) | `VARIANT` | `VARIANT` | Unchanged | No target | P4 | `GetCountryTableNo`; managed verified |
| [GetSectionPropertyCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gabd01f29a4421f6fe05c2138cda657595) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L598) | P4 | Implemented route; live qualification pending |
| [GetSectionPropertyList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga58470c040e0adc4607e00e054b4c6daa) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L602) | P4 | Implemented route; live qualification pending |
| [GetSectionPropertyName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga22f293fd885cd2f6a2102d5a9f9e691d) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1080) | P4 | Implemented route; live qualification pending |
| [GetSectionPropertyType](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga70e659ae3da254327aa62b82914dee1f) | `VARIANT` | `VARIANT` | Unchanged | No target | P4 | Implemented route; live qualification pending |
| [GetSectionPropertyCountry](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga7ecdf0e76eeebbeb8cb5d225ab319084) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1022) | P4 | Implemented route; live qualification pending |
| [GetShapeCode](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gae13c9d4c20e1a0a487728cbdcdf73024) | `long` | `long` | Version policy candidate | No target | P9 | `GetShapeCode`; managed verified, live pending |
| [GetRecordForSection](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga75d00082ceba112a311334739850896b) | `long` | `long` | Version policy candidate | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1029) | P9 | Implemented route; live qualification pending |
| [GetPlateThickness](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga2fa3663eee2b4f19cfd109177de444fe) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1278) | P4 | Implemented route; live qualification pending |
| [GetThicknessPropertyCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga1b82bcd70a84320fa1c5f153255426f8) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1246) | P4 | Implemented route; live qualification pending |
| [GetThicknessPropertyList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga080e308745d58857189ab422b9b6102e) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1249) | P4 | Implemented route; live qualification pending |
| [GetThicknessPropertyAssignedPlateCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga6b405e7c6b5a5a294277ffc910dbe42e) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1294) | P4 | Implemented route; live qualification pending |
| [GetThicknessPropertyAssignedPlateList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gacec0dbd476ed855f50796f7bb4166d2d) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1296) | P4 | Implemented route; live qualification pending |
| [GetThicknessPropertyValues](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gafd3b17a65417534a41a8f1497e4517c6) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1270) | P4 | Implemented route; live qualification pending |
| [RemovePropertyFromPlate](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga99c86bf4b01408c684ff009e0e00079b) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `RemovePropertyFromPlate`; managed verified, live pending |
| [RemoveMaterialFromPlate](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gaa8489347df16c3ea9aea52ca421e4e53) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `RemoveMaterialFromPlate`; managed verified, live pending |
| [RemoveMaterialFromSolid](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gaefebf74d842cbed7e39736e94abb8ea4) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `RemoveMaterialFromSolid`; managed verified, live pending |
| [GetIsotropicMaterialAssignedPlateList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gaf6b9b9403e5b7e0ae90ba797491f17af) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L411) | P4 | Implemented route; live qualification pending |
| [GetCountofSectionPropertyValuesEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga2e1e4793b70cdd102f55d878b92685cc) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1015) | P4 | Implemented route; live qualification pending |
| [FindUPTTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#gac238a7c187f6e53ca29b25f2331b20a3) | `long` | `long` | Unchanged | No target | P8 | Pending; Documented member is absent from inspected 2025/2026 exported IOSPropertyUI metadata; no qualified dispatch receiver is established. |
| [CreateUPTTableEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga0fb2e9ebe08ff961ce2e620616ceefa0) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | `CreateUptTableEx`; managed renamed route, live pending |
| [RemoveUPTTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#gaf713bf43b402d4efc3af2b5296ede180) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | `RemoveUptTable`; managed renamed route, live pending |
| [RemovePropertyFromUPTTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#gaf6f7c76255b453edb2cf220fb297ec72) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | `RemovePropertyFromUptTable`; managed renamed route, live pending |
| [CreatePropertyFromUserTable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#gabb8beaa20258b93f62714c9672415350) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | `CreatePropertyFromUserTable`; managed verified, live pending |
| [GetUserProvidedTableCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga32c6d22bc1d698ce9e1ea6b2f8473cd9) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | `GetUserProvidedTableCount`; managed verified, live pending |
| [GetUserProvidedTableList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga76a5e72dc10bf0cb788454375ef3717b) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | `GetUserProvidedTableList`; managed verified, live pending |
| [GetUserProvidedTableSectionCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga300e2d80e44a9a02dc31dd461dde271e) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | `GetUserProvidedTableSectionCount`; managed verified, live pending |
| [GetUserProvidedTableSectionList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga01f780a22d5a0720124a9a18c91461d7) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | Pending; Help specifies indexed CSStringList output without a verified CLR representation; no guessed String array or index mapping is exposed. |
| [GetUserProvidedTableSectionType](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#gadf28c45e67614bb66eb332b8dd76588b) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | `GetUserProvidedTableSectionType`; managed verified, live pending |
| [GetUserProvidedTableSectionPropertyCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga43427a4ed7d22d28a82038f6351fee1e) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | `GetUserProvidedTableSectionPropertyCount`; managed verified, live pending |
| [GetUserProvidedTableSectionProperties](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga6fa18da33cf823a4a528ddaa35d7aeb3) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | `GetUserProvidedTableSectionProperties`; managed verified, live pending |
| [GetUserProvidedTableNo](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_u_p_t_section.html#ga778d97759d0fae3321f116992db15f50) | `VARIANT` | `VARIANT` | Unchanged | No target | P8 | `GetUserProvidedTableNo`; managed verified, live pending |
| [CreateMemberReleaseSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gabfb89e094b1aa1a943716dfb7a0d4841) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1585) | P7 | Implemented route; live qualification pending |
| [CreateMemberPartialReleaseSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gae408d55746db59e4c22c2e505bb403a2) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `CreateMemberPartialReleaseSpec`; managed verified |
| [CreateMemberTrussSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#ga19c64f2a9dc77cab9176f382524d17ca) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `CreateMemberTrussSpec`; managed verified |
| [CreateMemberInactiveSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gabdcfbecf01cd5889eb04da0aa8102a41) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `CreateMemberInactiveSpec`; managed verified |
| [CreateMemberTensionSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#ga8db5109360921dde0350b2cdd4fe611a) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `CreateMemberTensionSpec`; managed verified |
| [CreateMemberCompressionSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gafd9eeea22012202b700e42de2dae4dc0) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `CreateMemberCompressionSpec`; managed verified |
| [CreateMemberIgnoreStiffSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gad30bdd6dd57392faa1cf132df4819164) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `CreateMemberIgnoreStiffSpec`; managed verified |
| [CreateMemberCableSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#ga354fa1642399ba13018d1a610149589c) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `CreateMemberCableSpec`; managed verified |
| [CreateMemberCableSpecEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#ga1fe56e4b2b563db42107d0f2b68b8fb4) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `CreateMemberCableSpecEx`; managed verified |
| [CreateMemberOffsetSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#ga452a2b2f15fcbf10a46a11e8dec7ef42) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L732) | P7 | Implemented route; live qualification pending |
| [CreateElementNodeReleaseSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gafd355c2d78879ae36eb5cabf7c0d99eb) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `CreateElementNodeReleaseSpec`; managed verified |
| [CreateElementPlaneStressSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gaec273848a190a0e1c308a4c770b45f1f) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `CreateElementPlaneStressSpec`; managed verified |
| [CreateElementIgnoreInplaneRotnSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gade9e2bced8795b0cd4dab888244829b3) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `CreateElementIgnoreInplaneRotnSpec`; managed verified |
| [AddControlDependentRelation](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gacfbf4be0e4710af48fbbb2c08bec0cfd) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `AddControlDependentRelation`; managed verified |
| [CreateElementOffsetSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gafd77251b4fa48baa9491672bf3a3a3ca) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `CreateElementOffsetSpec`; managed verified |
| [CreateElementLocalZOffsetSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#gac1057cd910b775a08d6c2c7df22e2781) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `CreateElementLocalZOffsetSpec`; managed verified |
| [CreateMemberFireProofingSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_spec.html#ga6792430dddf478392843d424059c8730) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `CreateMemberFireProofingSpec`; managed verified |
| [DeleteMemberReleaseSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gafe30a2d90280de4c4fd6149959730507) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `DeleteMemberReleaseSpec`; managed verified, live pending |
| [DeleteAllControlDependentRelations](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gad780df368b309c15dec931245113040c) | `long` | `long` | Unchanged | No target | P10 | `DeleteAllControlDependentRelations`; managed verified, live pending |
| [GetElementOffsetSpecCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga5facbddbfb9ea2ad0b48c454a122cf24) | `VARIANT` | `VARIANT` | Unchanged | No target | P2 | Implemented route; live qualification pending |
| [RemoveAllElementOffsetSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gaa7bdd908c360ccf8ca9e41ee0e894b5a) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `RemoveAllElementOffsetSpec`; managed verified, live pending |
| [GetMemberSpecCode](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga6b19b83068026b147eeb88a3528328d0) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L874) | P7 | Implemented route; live qualification pending |
| [RemovePropertyFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga551c8845b034009e68a16ec2813c50bc) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `RemovePropertyFromBeam`; managed verified, live pending |
| [RemoveBeamPropertyHelper](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gadb028ea4f8477119ab50eb4881ebef61) | `BOOL` | `BOOL` | Native private helper: excluded | No target | P0 | Excluded |
| [DeleteProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga26852ecd49fc5ea7657d2b5198a1c7ba) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `DeleteProperty`; managed verified, live pending |
| [GetPropertyUniqueID](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga5aa6aa08217095c0a0eb65530dd867e8) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `GetPropertyUniqueID`; managed verified |
| [SetPropertyUniqueID](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gac7758ac39ea94bba561989b4898e59bb) | `void` | `void` | Unchanged | No target | P7 | `SetPropertyUniqueID`; managed verified |
| [DeleteMemberSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga1956ce5d3c9487681c3dcca8c2a7f4a4) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `DeleteMemberSpec`; managed verified, live pending |
| [RemoveMemberReleaseSpecFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gaaee16932dc7a02f3d707576fc2d009a0) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `RemoveMemberReleaseSpecFromBeam`; managed verified, live pending |
| [RemoveMemberOffsetSpecFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga76d69608bda66b72f7a1c9011b68a46d) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `RemoveMemberOffsetSpecFromBeam`; managed verified, live pending |
| [RemoveMemberTrussSpecFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga01e39b8ece2146ea7aa09b7ff33423cd) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `RemoveMemberTrussSpecFromBeam`; managed verified, live pending |
| [RemoveMemberInactiveSpecFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gaec27f611f0d02de6278bdb6601d106f8) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `RemoveMemberInactiveSpecFromBeam`; managed verified, live pending |
| [RemoveMemberTensionSpecFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga22be38efdbc61ef92dabf2b7eb25ad89) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `RemoveMemberTensionSpecFromBeam`; managed verified, live pending |
| [RemoveMemberCompressionSpecFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga1fac54088e17dbc9a7e7e93a5a9d1d45) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `RemoveMemberCompressionSpecFromBeam`; managed verified, live pending |
| [RemoveMemberIgnoreStiffSpecFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga159089ba705d76f58988ecd3cd052be3) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `RemoveMemberIgnoreStiffSpecFromBeam`; managed verified, live pending |
| [RemoveMemberCableSpecFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga2b82e36f819c8df67990bdc88da6dd6f) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `RemoveMemberCableSpecFromBeam`; managed verified, live pending |
| [RemoveElementPlaneStressSpecFromPlate](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gafc527399dbd62917400f8922ba0b4294) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `RemoveElementPlaneStressSpecFromPlate`; managed verified, live pending |
| [RemoveElementIgnoreInplaneRotnSpecFromPlate](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga9b1e19ac200b1c62409785f5c80dd845) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `RemoveElementIgnoreInplaneRotnSpecFromPlate`; managed verified, live pending |
| [RemoveElementNodeReleaseSpecFromPlate](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga6503cab6a999f8024fe606c4e32c614a) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `RemoveElementNodeReleaseSpecFromPlate`; managed verified, live pending |
| [RemoveAllElementNodeReleaseSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga8a3da913ec174caaa00066aac9ac2cad) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `RemoveAllElementNodeReleaseSpec`; managed verified, live pending |
| [GetMemberReleaseSpecEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga8ba58d357fc17509602aff5e2e99c9d4) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L968) | P2 | Implemented route; live qualification pending |
| [GetInactiveMemberCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga1b96f8f630976e634b4906f1a1bb60b3) | `long` | `long` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L832) | P7 | `GetInactiveMemberCount`; managed verified |
| [GetInactiveMemberList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga25d477cfe62c96ccbd7901073e76540d) | `void` | `void` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L834) | P7 | Pending; contract qualification gate |
| [GetAlphaAngleForSection](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga6aa625ced61caf38f8f164bbb4ecb0c0) | `VARIANT` | `VARIANT` | Formatting/cross-reference only | No target | P7 | Pending; contract qualification gate |
| [GetCentroidLocationForSection](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gac9e334a5b43efdadb0faaaa95b48e20b) | `VARIANT` | `VARIANT` | Formatting/cross-reference only | No target | P7 | Pending; contract qualification gate |
| [GetFireProofedBeamCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga2bfefd59d0590e70c72b7f086e6ede10) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `GetFireProofedBeamCount`; managed verified |
| [GetFireProofedBeamList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga838cda3edcec54845560c94c9163d004) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `GetFireProofedBeamList`; managed verified |
| [GetFireProofDataForBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#gad5ab8626f3f670ef18fc37c7851528f3) | `VARIANT` | `VARIANT` | Type/meaning clarification | No target | P7 | `GetFireProofDataForBeam`; managed verified |
| [GetFireProofingSpecCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga5b41580941d25c3f519a74ae1e840a60) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `GetFireProofingSpecCount`; managed verified |
| [GetFireProofingSpecDetails](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga9bdbff55763121a7a63149630ed66435) | `VARIANT` | `VARIANT` | Type/meaning clarification | No target | P7 | `GetFireProofingSpecDetails`; managed verified |
| [GetFireProofingSpecAssignedBeamCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga7fef558917ef800217ce7ef0160c06d1) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `GetFireProofingSpecAssignedBeamCount`; managed verified |
| [GetFireProofingSpecAssignedBeamList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga1cc2f6b4d2a0be3115cdc362d07b48e4) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `GetFireProofingSpecAssignedBeamList`; managed verified |
| [RemoveMemberFireProofingSpecFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_spec.html#ga5ed5d25078d40c5c9c9f011dfc38db57) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `RemoveMemberFireProofingSpecFromBeam`; managed verified, live pending |
| [CreateAssignProfileProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#ga9e14482ffc29d710f72d6e8f648945b6) | `VARIANT` | `VARIANT` | Unchanged | No target | P5 | `CreateAssignProfileProperty`; managed verified |
| [AssignBeamProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#gab13d04addd4f49d9ddeb79bc04e076cd) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1091) | P5 | Implemented route; live qualification pending |
| [AssignMemberSpecToBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#ga04773e6f7e7e123337dd8808525659d6) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L734) | P5 | Implemented route; live qualification pending |
| [AssignPlateThickness](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#ga1a61c38085b3e44b4017ad4b5fba7329) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1185) | P5 | Implemented route; live qualification pending |
| [AssignParametricSurfaceThickness](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#ga452172a2ca1781a313b638e1e92f1d05) | `VARIANT` (summary only) | `VARIANT` (summary only) | Reserved: do not use | No target | P0 | Excluded |
| [AssignElementSpecToPlate](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#ga7cfac9861855bd012a1c0e9ab75577bd) | `VARIANT` | `VARIANT` | Unchanged | No target | P5 | `AssignElementSpecToPlate`; managed verified |
| [UpdatePropertiesToDesignSection](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_section.html#ga0ee52e9e4368ba1a8ac84bfff8c336dd) | `VARIANT` | `VARIANT` | Unchanged | No target | P5 | `UpdatePropertiesToDesignSection`; managed verified |
| [CreateIsotropicMaterialProperties](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_mat.html#ga21f578122b014d7b963ac9cbffb0cd33) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1446) | P5 | Implemented route; live qualification pending |
| [CreateIsotropicMaterialPropertiesEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_mat.html#ga0bc4eb672546e351191883694a504138) | `VARIANT` | `VARIANT` | Type/meaning clarification | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1476) | P5 | `CreateIsotropicMaterialPropertiesEx`; managed verified |
| [CreateIsotropicMaterialSteel](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_mat.html#ga6d780606438b7dd9a2621feeb2addc46) | `VARIANT` | `VARIANT` | Unchanged | No target | P5 | `CreateIsotropicMaterialSteel`; managed verified |
| [CreateIsotropicMaterialConcrete](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_mat.html#gae3f66093596a98c0a09a9483c1fc61db) | `VARIANT` | `VARIANT` | Unchanged | No target | P5 | `CreateIsotropicMaterialConcrete`; managed verified |
| [CreateIsotropicMaterialAluminum](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_mat.html#ga9edf9ae98088d07e28177a6ce0f7e915) | `VARIANT` | `VARIANT` | Unchanged | No target | P5 | `CreateIsotropicMaterialAluminum`; managed verified |
| [CreateIsotropicMaterialTimber](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_mat.html#ga120025647e015ec700e4b19452f35e92) | `VARIANT` | `VARIANT` | Unchanged | No target | P5 | `CreateIsotropicMaterialTimber`; managed verified |
| [GetIsotropicMaterialAssignedBeamCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gae487189c55613e48e06b63ef9669c0fb) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L440) | P3 | Implemented route; live qualification pending |
| [GetIsotropicMaterialAssignedBeamList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga40e7abe1185d79a7053ab5f5b7f19555) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L443) | P3 | Implemented route; live qualification pending |
| [GetMaterialProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga90b6809ba31300363321cce519f0b9c3) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L180) | P3 | Implemented route; live qualification pending |
| [GetBeamMaterialName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gade8859bea46c3bccd85204f73593e0b0) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L105) | P3 | Implemented route; live qualification pending |
| [GetElementMaterialName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga44859f04996f6facbe39d62d67594b8c) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | Implemented route; live qualification pending |
| [GetIsotropicMaterialCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gaa8e5a0c6288ae8d53eded96a1fb0c114) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L331) | P3 | Implemented route; live qualification pending |
| [GetIsotropicMaterialProperties](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gaeead1ecb0ed0c1587d432a1e3b9887b5) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L374) | P3 | Implemented route; live qualification pending |
| [GetMaterialPropertyEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga02bbaecea45e8ab88558707f439acbe1) | `VARIANT` | `VARIANT` | Type/meaning clarification | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L151) | P3 | Implemented route; live qualification pending |
| [GetIsotropicMaterialPropertiesEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gab82a93e375ef05e9f893b5ba272b7ad5) | `VARIANT` | `VARIANT` | Type/meaning clarification | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L348) | P3 | Implemented route; live qualification pending |
| [GetIsotropicMaterialPropertiesAssigned](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga74438ad3dd2db13772443598f8ee1707) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | Implemented route; live qualification pending |
| [GetOrthotropic2DMaterialCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga34d6f21ac2f41814c0a2ca78f0d387ed) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | Implemented route; live qualification pending |
| [GetOrthotropic2DMaterialProperties](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga04b76b162db405e4a289b71f05b7e673) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | Implemented route; live qualification pending |
| [GetOrthotropic3DMaterialCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gab247088c8327421655e4ad0b53b8a19f) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | `GetOrthotropic3DMaterialCount`; managed verified |
| [GetOrthotropic3DMaterialProperties](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga7be4c04b41e1326016a62cd43ccbf8e6) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | `GetOrthotropic3DMaterialProperties`; managed verified |
| [DeleteMaterial](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga10ded1e9c1480a77783bd89565a1b082) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L71) | P3 | Implemented route; live qualification pending |
| [RemoveMaterialFromBeam](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#gac084be83e5205fc0f6c766025e063531) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | `RemoveMaterialFromBeam`; managed verified |
| [RemoveBeamMaterialHelper](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga3caee4c622453d9c30f56cc797d04231) | `BOOL` | `BOOL` | Native private helper: excluded | No target | P0 | Excluded |
| [GetPlateMaterialName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga0a6b8e8cdb65ed2b9747e95d7fb7706a) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L109) | P3 | Implemented route; live qualification pending |
| [GetIsotropicMaterialAssignedPlateCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga2eaaa9ee8060d95d5f4c587ac45860e5) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L408) | P3 | Implemented route; live qualification pending |
| [GetSolidMaterialName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga9b909a32be274f8a4a797ea85bdf2340) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | `GetSolidMaterialName`; managed verified |
| [GetIsotropicMaterialAssignedSolidCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga875c78622314eabe9df5600344f39a87) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | `GetIsotropicMaterialAssignedSolidCount`; managed verified |
| [GetIsotropicMaterialAssignedSolidList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga87626360957ef20687a901188e328fdd) | `VARIANT` | `VARIANT` | Type/meaning clarification | No target | P3 | `GetIsotropicMaterialAssignedSolidIds`; managed verified |
| [SetTypeToIsotropicMaterial](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga14f907ed47c4fbfd9d1c644ce60ed823) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | `SetTypeToIsotropicMaterial`; managed verified |
| [GetTypeForIsotropicMaterial](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_mat.html#ga3f9536e2090de7917bb53289dcccf2f0) | `VARIANT` | `VARIANT` | Unchanged | No target | P3 | `GetTypeForIsotropicMaterial`; managed verified |
| [SetMaterialID](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_mat.html#gaeef73e958f01de09e3c761d0272599d5) | `void` | `void` | Deprecated: use SetMaterialName | No target | P0 | Excluded |
| [SetMaterialName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_mat.html#ga3d08cca38e2d9db5f1e7fdfae318ede6) | `void` | `void` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L232) | P5 | Implemented route; live qualification pending |
| [AssignMaterialToMember](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_mat.html#ga1d4300751f2837d7af62dc01dbb2432a) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L211) | P5 | Implemented route; live qualification pending |
| [AssignMaterialToPlate](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_mat.html#gad8b53c7251166270f27431a17b8172e1) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1567) | P5 | Implemented route; live qualification pending |
| [AssignMaterialToParametricSurface](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_mat.html#gaebf142533eb1edfef25b3f787da3119d) | `VARIANT` (summary only) | `VARIANT` (summary only) | Reserved: do not use | No target | P0 | Excluded |
| [AssignMaterialToSolid](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_mat.html#ga0a26c0b843f84f87f796b75646fe0b72) | `VARIANT` | `VARIANT` | Unchanged | No target | P5 | `AssignMaterialToSolid`; managed verified |
| [CreateMemberAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_attri.html#ga596e62e697d449368e61f64155d2e3e3) | `long` | `long` | Unchanged | No target | P7 | `CreateMemberAttribute`; managed verified |
| [CreateElementAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_create_attri.html#gaf9f5aa915388204e73930be35b8936fc) | `long` | `long` | Unchanged | No target | P7 | `CreateElementAttribute`; managed verified |
| [DeleteMemberAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga624209a58bc90aa8debb826c76cf4850) | `long` | `long` | Unchanged | No target | P7 | `DeleteMemberAttribute`; managed verified |
| [GetMemberCountByAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#gacb8947a2d5bc779fcd83573c288a024f) | `long` | `long` | Unchanged | No target | P7 | `GetMemberCountByAttribute`; managed verified |
| [GetMemberListByAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga601c518a43ee9b2181bb74a1eef40cdb) | `long` | `long` | Unchanged | No target | P7 | `GetMemberListByAttribute`; managed verified |
| [GetMemberAttributeCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga65d055a80eda1974297c41556424518a) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `GetMemberAttributeCount`; managed verified |
| [GetMemberAttributeList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga8ee3d1f547137a80ac72d653fadb49a9) | `VARIANT` | `VARIANT` | Unchanged | No target | P7 | `GetMemberAttributeList`; managed verified |
| [DeleteElementAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga33b257dd90f2aa0dfdc229de5f4f09be) | `long` | `long` | Unchanged | No target | P7 | `DeleteElementAttribute`; managed verified |
| [GetElementCountByAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#gae0769d51f8406a5d3a64daba5ade7c1c) | `long` | `long` | Unchanged | No target | P7 | `GetElementCountByAttribute`; managed verified |
| [GetElementListByAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#gafc4b745f61d6e1d6679342b777c32e55) | `long` | `long` | Unchanged | No target | P7 | `GetElementListByAttribute`; managed verified |
| [GetAssignedAttributeCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga152207bd66e3c7aaa590a87dfde0ee79) | `long` | `long` | Unchanged | No target | P7 | `GetAssignedAttributeCount`; managed verified |
| [GetAssignedAttributeByIndex](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#ga5fb1fa2b9a41c80056c21d5af43ead20) | `long` | `long` | Unchanged | No target | P7 | Pending; contract qualification gate |
| [RemoveAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_attri.html#gae2b97c9c2b4b73c84e9f221f50062150) | `long` | `long` | Unchanged | No target | P7 | `RemoveAttribute`; managed verified |
| [AssignMemberAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_attri.html#gad33b01a3d202d730de11bd3e13e0d8c6) | `long` | `long` | Unchanged | No target | P7 | `AssignMemberAttribute`; managed verified |
| [AssignElementAttribute](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_assign_attri.html#ga07b29997e1c5a2ccee2677104bd3dced) | `long` | `long` | Unchanged | No target | P7 | `AssignElementAttribute`; managed verified |
| [GetMemberReleaseSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga87a6522925d50ef55f783c90659e0180) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L916) | P2 | Implemented route; live qualification pending |
| [GetMemberLocalOffSet](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#gafdd6f4200ccd7a34763427950ff01281) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L709) | P2 | Implemented route; live qualification pending |
| [GetMemberGlobalOffSet](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#gaf5e75d28dc527fdefb40ac84111caaa3) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L707) | P2 | Implemented route; live qualification pending |
| [GetBeamProperty](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga185a7f1dd1d7576097df4e187e23bfe3) | `VARIANT` | `VARIANT` | Unchanged | No target | P2 | Implemented route; live qualification pending |
| [GetBeamPropertyAll](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga2ecc4ba0fec2945bfdda017ed702cbea) | `VARIANT` | `VARIANT` | Unchanged | No target | P2 | Implemented route; live qualification pending |
| [GetBeamConstants](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga13b2745c44a68596591a21b578efa331) | `VARIANT` | `VARIANT` | Unchanged | No target | P2 | Implemented route; live qualification pending |
| [GetElementLocalOffset](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#gaef9c6cdae820d578c8a9a093edc8ae91) | `VARIANT` | `VARIANT` | Unchanged | No target | P2 | Implemented route; live qualification pending |
| [GetElementGlobalOffset](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga4cc99a4ac6ee14397d95e411c515f1d8) | `VARIANT` | `VARIANT` | Unchanged | No target | P2 | Implemented route; live qualification pending |
| [GetElementOffsetSpec](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga94a58d54353f25487a5127b28f6f83ec) | `VARIANT` | `VARIANT` | Unchanged | [Direct adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs#L1195) | P2 | Implemented route; live qualification pending |
| [GetPlateSectionPropertyRefNo](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_ele_property.html#ga365e1db0fa5a9fde9ff8ba9359f81cc3) | `VARIANT` | `VARIANT` | Unchanged | No target | P2 | Implemented route; live qualification pending |
| [GetPublishedProfileName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_publish.html#ga255dfc3675daa744d31d7cd41dc4e1f1) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `GetPublishedProfileName`; managed verified, live pending |
| [GetSTAADProfileName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_publish.html#ga505bb1119e1751bc9006263b43016121) | `VARIANT` | `VARIANT` | Unchanged | No target | P10 | `GetStaadProfileName`; managed verified, live pending |
| [SetStandardProfileDBFolder](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#ga4e87d6b72233137048da0333c8565781) | `long` | `long` | Version policy candidate | No target | P9 | `SetStandardProfileDBFolder`; managed verified, live pending |
| [GetStandardProfileDBFolder](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#gaa29514922cc9ae65f0d92da202144f5f) | `VARIANT` | `VARIANT` | Formatting/cross-reference only | No target | P9 | `GetStandardProfileDBFolder`; managed verified, live pending |
| [GetDefaultStandardProfileDBFolder](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#gaf3c05fdd108cc02b0269073ae1543663) | Absent | `VARIANT` | Newly documented in 2025 | No target | P9 | `GetDefaultStandardProfileDBFolder`; acquired catalog verified, live pending |
| [IsStandardDatabaseSection](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#gaeb4bb81496f3d6451e9a126ea0a6d3a6) | Absent | `VARIANT` | Newly documented in 2025 | No target | P9 | `IsStandardDatabaseSection`; acquired catalog verified, live pending |
| [GetStandardSectionDatabaseName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#ga527cf81f6e5369eec3076be58fde90cc) | Absent | `VARIANT` | Newly documented in 2025 | No target | P9 | `GetStandardSectionDatabaseName`; acquired catalog verified, live pending |
| [GetStandardSectionTableName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#ga45b152c818ab6a2b89be7d90892d0ded) | Absent | `VARIANT` | Newly documented in 2025 | No target | P9 | `GetStandardSectionTableName`; acquired catalog verified, live pending |
| [GetStandardSectionName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#ga87dfe66234b751a9c9f5914de89e2fb1) | Absent | `VARIANT` | Newly documented in 2025 | No target | P9 | `GetStandardSectionName`; acquired catalog verified, live pending |
| [GetProfileBoundaryInformation](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#ga97b4352067b45df2388452488c772be1) | `VARIANT` (summary only) | Absent | Reserved: do not use | No target | P0 | Excluded |
| [GetProfileNoOfPoints](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#ga32bba6cd08a1fb0c8b776376e6306c2f) | `VARIANT` (summary only) | Absent | Reserved: do not use | No target | P0 | Excluded |
| [GetProfileBoundaryPoints](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_create_u_p_t_section.html#ga51857b4c21c7bec5391a0a26f34a6a40) | `VARIANT` (summary only) | Absent | Reserved: do not use | No target | P0 | Excluded |

The annex expands all 212 entries and identifies their proposed managed request/result families. Preliminary families should be refined into a few cohesive classes or typed request variants during API review; the plan does not require one new model class for every native function.

## 4. Actual version differences

### 4.1 Nine section-value layouts change in the help

[GetSectionPropertyValuesEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga0497f1f458d11eb783f465a85dc6ad27) retains its declaration while nine of its 47 named section rows change. The full old/new tables are in the annex and comparison data.

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

These differences affect field positions, omitted dimensions and cardinality. Examples include adding channel depth before subsequent values, adding composite CD, omitting plate-strip thickness and changing cold-formed lip fields. Treat the 2024 literal "<LIP" text as an ambiguity, not a mathematical operation.

Proposed handling:

- Allocate using GetCountofSectionPropertyValuesEx with strict bounds/type validation; do not allocate solely from a hand-coded per-shape table.
- Select an internal field-layout policy by acquired version and returned section type, with a common layout for unchanged types.
- Confirm all nine candidate layouts on actual advertised builds. Help table differences are evidence of a contract risk, not certification of binary behavior.
- Return a named SectionDefinitionResult containing status, type, verified named fields, schema evidence and any qualified uninterpreted values. Preserve unknown type codes without casting them blindly to an enum.
- Do not fill absent fields with engineering zeros, trim unexplained tails or assume every buffer slot returned by the maximum-count API is meaningful.

The private section factory/model graph currently interprets PropertyValues positionally. Its classes must be reviewed against these layouts before they are used unchanged on every year.

### 4.2 Section database-folder status changes

[SetStandardProfileDBFolder](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_standard_section.html#ga4e87d6b72233137048da0333c8565781) has the same native long return and string parameter in every version, but its documented status mapping changes:

| Version | Success | Invalid/empty/missing path |
| --- | ---: | ---: |
| 2024 | 1 | 0 |
| 2025 / 2026 | 0 | -1 |

Use a focused internal status policy. Determine the profile before the mutation and invoke once; do not change the folder, reinterpret a zero through another profile and retry. A named PropertyContextChangeResult should retain the raw code and verified outcome. Unknown versions should not mutate with a guessed policy.

GetStandardProfileDBFolder itself has only a See-also change. It should stay in a common query service. The new GetDefaultStandardProfileDBFolder is a separate capability candidate; the current folder cannot stand in for the installation default.

### 4.3 Section record identity changes its description

[GetRecordForSection](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#ga75d00082ceba112a311334739850896b) describes a record index starting at one in 2024. The newer help calls it the record number in the section database. The declaration is unchanged.

Return a SectionRecordResult with country/catalog/name and the native identifier. Do not silently convert between zero-based index, one-based index, database record number and model property ID. The source stores this value in SectionRecNo, which is useful naming, but needs profile-aware meaning. Confirmation should use a catalog whose record numbers are not contiguous list positions.

### 4.4 Country-qualified shape catalogs change

[GetShapeCode](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_get_section.html#gae13c9d4c20e1a0a487728cbdcdf73024) retains its declaration. Its table changes as follows:

| Catalog | 2024 | 2025 / 2026 |
| --- | --- | --- |
| American | Codes 17–22 include WT/MT/ST and double-angle/channel variants. | Those entries are omitted; 23/24 remain. |
| Australian | Codes 7–9 include Rhs/Shs/Chs. | Those entries are omitted. |
| British | Codes 9/10 include double angle/channel. | Those entries are omitted. |
| RCecoColdFormed | Listed without reservation. | Country and shape entries are marked Reserved. |

A shape code is country/catalog-qualified, not a universal enum. Use a named SectionShapeResult with semantic classification where verified and retained raw code for unknown values. Table omission does not prove that a native server or installed catalog rejects a shape. Do not advertise removed support or retry alternate countries based only on these tables.

Version policies should describe verified interpretation/capabilities; database contents and configured folder are also relevant. A small catalog boundary is preferable to importing the private global SectionsRepository singleton.

### 4.5 Eight newer detailed APIs and three older reserved entries

| Newly documented in 2025 / 2026 | Contract and planned access |
| --- | --- |
| GetUptGeneralProfilePointsCount | Outer/inner counts; inner count is reserved. Optional UPT general-profile capability. |
| GetUptGeneralProfileBoundaryPoints | Paired local Z/Y coordinates; inner selector must be false. No public inner-hole support inferred. |
| GetUptGeneralStressLocationPoints | Paired Z/Y arrays of size four plus returned count. |
| GetDefaultStandardProfileDBFolder | Installation default folder, distinct from configured current folder. |
| IsStandardDatabaseSection | Native Boolean data distinguishing standard database source. |
| GetStandardSectionDatabaseName | Nonempty database name or empty for nonstandard origin. |
| GetStandardSectionTableName | Nonempty table name or empty when not applicable. |
| GetStandardSectionName | Section identity within the standard database/table, or empty when not applicable. |

The 2024-only GetProfileBoundaryInformation, GetProfileNoOfPoints and GetProfileBoundaryPoints all say **reserved, do not use**. Their signatures differ from the newer trio. Do not implement a 2024 fallback by calling them, and do not claim a one-to-one supported rename.

AssignParametricSurfaceThickness and AssignMaterialToParametricSurface remain reserved in all three years. SetMaterialID is deprecated in favor of SetMaterialName. RemoveBeamPropertyHelper and RemoveBeamMaterialHelper are explicitly private native implementation functions even though they appear in help. Keep them out of public managed APIs.

Native capability evidence can later establish broader support; omission from 2024 help is not proof of absence in every native build.

### 4.6 Clarifications and cosmetic changes

| Functions | Change | Recommended implementation |
| --- | --- | --- |
| CreateIsotropicMaterialPropertiesEx, GetMaterialPropertyEx, GetIsotropicMaterialPropertiesEx | Fcu is clarified/corrected to compressive strength; 2024 getter text calls it yield stress. | Common field named CompressiveStrength, preserving native Fcu mapping and stress units. No per-year physics change inferred. |
| GetFireProofDataForBeam, GetFireProofingSpecDetails | Fireproof type explicitly described as integer; codes remain BFP=1, CFP=2. | Common semantic enum with unknown-code retention. |
| GetIsotropicMaterialAssignedSolidList | Explicit string material name and long-array output annotation. | Common strict buffer handling. |
| GetAlphaAngleForSection, GetCentroidLocationForSection | Annotation markers change. | Common service. Alpha remains radians; preserve centroid reference/axes. |
| GetStandardProfileDBFolder | See-also links change. | Common folder query. |

The other 188 common contracts match after syntax-heading normalization. Of thirteen changed contracts, only the four discussed policies require separate interpretation candidates; six clarifications and three cosmetic changes should not create whole version adapters. Seventy-five common example blocks change, recorded separately. No normalized changes were found from 2025 to 2026.

## 5. Current coverage, main-source parity and reuse

### 5.1 Current repository

RawProperty, the Property adapter binding and owning Wrapper are already captured and refreshed by OpenStaadWrapper. The single adapter now delegates qualified observations/mutations and compatibility workflows to shared internal readers with strict PropertyValueReader helpers. Queries capture the Property binding, validate it before/after access, preserve native units and distinguish absence from successful zero. Its raw object is not additional typed coverage.

Reuse current Beam/Plate identity, base/input unit metadata, unit primitives, property stores and strict native-value readers. The wrapper needs no additional public properties for this plan. Its cached input/base units are context, not proof of every native property array's physical units.

### 5.2 Main interface and adapter

The [main IOSProperty](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs) has 103 overloads; [OSPropertyAdapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSPropertyAdapter.cs) implements them all and adds four GetIsotropicMaterialAssignedBeams/Plates overloads. The annex records all 107 signatures and seven private helpers.

| Source functionality | Reuse direction |
| --- | --- |
| Standard/extended material queries and populated assignments | Preserve named fields, resolve names/indexes correctly and add qualified status/data. |
| Table/composite/built-up sections | Preserve selectors and dimensions behind typed managed requests; verify current-material context and unit packet. |
| Generic section factory and concrete section types | Reuse polymorphic modeling selectively, after layout verification and decoupling proprietary COM/catalog references. |
| Beta, offsets and release population | High-value first read APIs; preserve axes/end/DOF identity and distinguish absent data from failure. |
| Thickness and assignment queries | Materialized named results with count/cardinality checks and native correspondence. |
| Creation/assignment/grouping pipelines | Explicit multi-stage outcomes, shared specification IDs only when native creation succeeds, partial progress retained. |
| Catalog-backed area/name conveniences | Optional small catalog resolver; do not require a global private database dependency. |
| nThreads/PLINQ variants | Rework execution ownership; source parallelism does not certify shared COM safety. |

**56 direct adapter names plus nineteen additional delegated section-model names give 75 audited static native targets.** This is source evidence, not verified native coverage or completed open-source porting. The annex identifies every additional delegated call.

The source managed GetBeamProperty returns a property ID by scanning section assignments. It does not call Bentley's eight-output GetBeamProperty function. Coverage based on matching names would be wrong. Likewise, scalar convenience returns, rich composed models and multiple overloads are different measurements.

### 5.3 Source defects and limitations to address

| Source issue | Required adaptation |
| --- | --- |
| GetBeamOffsetSpecification ignores native return codes and turns a zero buffer into OffsetSpecification.None. | Qualify 0-success, -6027 no-offset, invalid member and other errors distinctly. Failed extraction must not become “no offset.” |
| Several queries use unchecked flags/default buffers and cast arrays directly. | Operation-specific return decoding, sentinel/type/rank/lower-bound/cardinality validation and owned snapshots. |
| CreateMaterialPropertyEx stores native 0-created/1-updated status into MaterialProperty.Id. | Return MaterialMutationResult with Created/Updated, name and raw status. Do not fabricate an ID. |
| Extended-create and inactive-list wrappers carry Obsolete(error=true) in the source. | Treat them as investigations, not validated functions ready for copying. |
| Hard-coded E/G ×1000 and inconsistent G conversion in extended creation. | Explicit verified unit policy for E/G/strength/weight density/thermal/damping; no blanket scaling. |
| ISectionWithCoverPlates bottom-only branch passes TC instead of BC. | Preserve correct BC semantics; direct composite and adapter branches are reference alternatives, not proof that every source path is correct. |
| GetUnusedSectionsIds treats a nonpositive assigned count as unused, potentially including native errors. | Qualify error/count domains before classifying a section as unused. |
| GetSectionBy uses a positional factory and accesses section after a nullable result; double-section area uses a private catalog workaround. | Qualified unsupported-kind results, guarded lookup and explicit provenance/units; no null dereference or invented area. |
| Composite/built-up creation relies on native current material and caller context. | Coordinate material selection and creation within the session, validate all preconditions before mutation and document side effects. |
| UPT model pipelines create tables/add sections/create properties with unchecked intermediate flags. | Report each stage, stop on failure, retain partial-created identities and avoid duplicate blind retries. |
| Local model IDs and collections are mutated as calls proceed. | Apply managed updates only for qualified outcomes; own stable identities and report partial changes. |
| PLINQ/native access crosses worker threads without a common session executor. | Keep native work on a verified apartment/owner; parallelize pure transformations only after extraction. |

These are static findings. No live failure or engineering result was reproduced by this review.

### 5.4 Dependencies and the Output plan

Port MaterialProperty, OffsetSpecification, release/spring/MP, thickness and generic-section models selectively, adapting private Shared/Units/Sections dependencies to existing repository primitives. Do not expose vendor OSPropertyUI in any new public request/model function, including the source's CreatePlateThicknessProperty overload.

Concrete private section objects currently call COM from CreateSectionInStaad. Preserve their modeling benefits but move native dispatch into internal Property creation services; expose interfaces/adapters and managed requests/results.

The planned Output offset-aware and physical-joint functions depend on Property's GetBeamOffsetSpecification semantics. Prioritize qualified global/local offset reads so Output can distinguish missing information from a genuine zero offset. This dependency can be satisfied without porting the full section catalog. Output implementation remains separate work.

## 6. Supplemental native and metadata discrepancies

Both inspected IOSPropertyUI interfaces contain 218 matching methods. Eight group-union names are absent: three older reserved profile entries, two private removal helpers, deprecated SetMaterialID, FindUPTTable and GetPlateSectionPropertyRefNo.

The first six have explicit exclusion reasons. The latter two need acquired native interface/signature verification; metadata omission alone is not native absence.

Fourteen metadata names fall outside the detailed group inventory. Thirteen class-level surface summaries overlap with twelve of them, producing **fifteen supplemental candidates** in total:

| Supplemental native/interop name | Class listing | Inspected IOSPropertyUI |
| --- | --- | --- |
| `AssignMaterialToSurface` | Summary in all three years | Present in 2025/2026 |
| `AssignSurfaceThickness` | Summary in all three years | Present in 2025/2026 |
| `CreateParametricSurfaceThicknessProperty` | Summary in all three years | Present in 2025/2026 |
| `CreateSurfaceThicknessProperty` | Summary in all three years | Present in 2025/2026 |
| `GetIsotropicMaterialAssignedSurfaceCount` | Summary in all three years | Present in 2025/2026 |
| `GetIsotropicMaterialAssignedSurfaceList` | Summary in all three years | Present in 2025/2026 |
| `GetPlateThicknessPropertyRefNo` | Absent under this name | Present in 2025/2026 |
| `GetSurfaceMaterialName` | Summary in all three years | Present in 2025/2026 |
| `GetSurfaceSectionPropertyRefNo` | Summary in all three years | Absent under this name |
| `GetSurfaceThickness` | Summary in all three years | Present in 2025/2026 |
| `GetSurfaceThicknessPropertyRefNo` | Absent under this name | Present in 2025/2026 |
| `GetThicknessPropertyAssignedSurfaceCount` | Summary in all three years | Present in 2025/2026 |
| `GetThicknessPropertyAssignedSurfaceList` | Summary in all three years | Present in 2025/2026 |
| `RemoveMaterialFromSurface` | Summary in all three years | Present in 2025/2026 |
| `RemovePropertyFromSurface` | Summary in all three years | Present in 2025/2026 |

Class help uses GetPlateSectionPropertyRefNo / GetSurfaceSectionPropertyRefNo; metadata uses GetPlateThicknessPropertyRefNo / GetSurfaceThicknessPropertyRefNo. Treat these as candidate aliases requiring proof, not interchangeable strings. Physical surfaces, parametric surfaces and mesh plates are distinct identities. A summary-only surface call cannot inherit a plate array contract by analogy.

The native class also includes twenty-two additional implementation helpers, plus the two private helpers already in the group inventory. They are excluded. DoesUPTSectionExists appears in the newer native class but is not an exported IOSPropertyUI member; it is not a new managed capability.

Other important verification gaps include:

- GetInactiveMemberList is void with a by-value Object buffer in installed metadata, and the source marks its wrappers unusable. Do not manufacture success from an initialized list.
- GetThicknessPropertyValues reports success with 4, whereas GetPlateThickness reports success with 0.
- CreateMemberPartialReleaseSpec and CreateElementNodeReleaseSpec document 0 OK; nearby creation APIs return IDs. Created identity cannot be inferred from that status.
- CreateMemberFireProofingSpec describes a zero-based index; detail/list getters use nonzero-based indices. Verify their relationship rather than applying a universal +1.
- Material getters mix name-returning index queries and Boolean-status name queries; zero-based material index is not material ID.
- Some assignment/count return descriptions appear copied from other queries, and prototype names differ from parameter tables. The annex retains these conflicts explicitly.
- Thickness/UPT/release/section array element types, lower bounds, reserved slots and current-unit conventions need per-operation fixtures.

## 7. Managed result and API design

### 7.1 Public contracts

Every new operation should return a named result containing semantic status, actual native status/code when supplied, operation/entity identity and qualified data. Availability, empty data, no assignment, no offset, updated versus created, unsupported member and native failure must remain distinguishable.

Use concrete result families such as OffsetQueryResult, MemberReleaseQueryResult, MaterialQueryResult, MaterialMutationResult, SectionDefinitionResult, SectionCreationResult, PropertyAssignmentResult and PropertyIdentifierListResult. Avoid both untyped object payloads and a separate class for every scalar field.

New APIs expose **no ref/out and no tuples**. Native references remain inside internal calls. Return owned read-only data, with named records for correspondence such as property-to-member assignments. Keep request/result identities stable even if legacy domain models remain mutable.

Proposed examples, not currently callable APIs:

~~~csharp
OffsetQueryResult GetMemberOffset(
    int memberId,
    MemberEnd memberEnd,
    OffsetCoordinateSystem coordinateSystem);

MemberReleaseQueryResult GetMemberRelease(
    int memberId,
    MemberEnd memberEnd);

MaterialMutationResult CreateOrUpdateMaterial(
    MaterialDefinitionRequest request);

SectionDefinitionResult GetSectionDefinition(int propertyId);

PropertyContextChangeResult SetStandardProfileDatabaseFolder(
    string folderPath);
~~~

Use descriptive managed names: memberId, materialName, materialIndex, propertyId, sectionName, tableId, thicknessPropertyId, memberEnd and coordinateSystem. Do not retain aliases such as mId, pId or propNo in newly exposed contracts. The annex preserves Bentley names only to identify native declarations.

Overloads can accept IDs or existing entities, scalar or bounded ID sequences, and typed section/material/release requests. Snapshot enumerables once; deterministic deduplication and partial-result policy must be documented. Prefer typed subclasses for materially different section inputs rather than a large “all possible fields” request with invalid combinations.

Use enums for verified country/table/specification/axis/DOF meanings, retaining raw unknown values. Country-specific shape codes, catalog records and property IDs require separate identities. A positive creation ID, Boolean success, zero-success code, zero-based index and count are not interchangeable integers.

### 7.2 Preserve compatibility

The first 25 Property queries follow the new managed conventions; future additions must retain those conventions. Existing wrapper/Root/Geometry/Load/Output bindings and published model members must remain compatible. C# cannot overload solely by return type.

Adding members to IOSProperty can affect third-party implementers; note that interface evolution in release documentation. Do not add throwing placeholders to simulate progress.

## 8. Internal organization and version selection

Keep one non-partial OSPropertyAdapter. Public native-domain signatures stay in IOSProperty and implementations stay in the adapter. Native helpers, decoding and policy selection remain internal:

~~~text
Adapters/Interfaces/IOSProperty.cs
Adapters/Interfaces/Property/           optional capabilities
Adapters/Models/OSPropertyAdapter.cs     single non-partial adapter
Models/Property/                       requests, results and reused models
Enums/Property/                        verified semantic domains
Services/Property/
    Binding/                           acquired receiver/profile evidence
    Contracts/                         narrow shared/version policies
    Materials/                         common material operations
    Sections/                          creation and layout decoding
    Specifications/                    offsets, releases and attributes
    Assignments/                       owned batches and partial outcomes
    Catalogs/                          small optional catalog boundary
~~~

These are proposed folders, not files created by this review. Start with cohesive services needed by the first increment; do not create an empty hierarchy or duplicate a whole service tree by year.

Use a common operation layer plus narrow internal policy objects for the four changed contracts. Shared 2025/2026 policies are justified by matching reviewed documentation/metadata. The 2024 policy supplies only established differences. Schema selection also depends on returned section type and verified catalog context.

~~~mermaid
flowchart TD
    A["Acquired wrapper and RawProperty"] --> B["Capture native StaadVersion and receiver"]
    B --> C{"Verified contract profile?"}
    C -->|"No"| U["Unverified or unavailable capability"]
    C -->|"2024"| D["2024 status, schema and identity policies"]
    C -->|"2025 or 2026"| E["Shared 2025/2026 policies"]
    D --> F["Common Property services"]
    E --> F
    F --> G["Stable IOSProperty operations"]
    F --> H{"Optional member and context verified?"}
    H -->|"Yes"| I["Acquire separate optional interface"]
    H -->|"No"| U
~~~

Native version, member metadata and runtime invocation evidence must agree before changed or optional contracts are enabled. Unknown/servicing builds cannot inherit unsafe positional layouts or mutation status rules by guess. Binding/profile caches belong to the acquired wrapper/receiver and invalidate on refresh/dispose.

Static C# interfaces/enum metadata cannot disappear from IntelliSense after connecting to a particular STAAD version. Use separately acquired optional interfaces for the newer UPT/general-profile and standard-section identity functions. The common adapter must not implement those optional interfaces and permit an availability-bypassing cast. Separate internal implementations expose them only after validation.

Capability absence, reserved functionality, unsupported catalog contents and a native operation failure need different diagnostics. Do not bind to another active instance to obtain a missing function.

## 9. Mutation sequence, failure and concurrency

~~~mermaid
flowchart TD
    A["Managed request"] --> B["Snapshot and validate fields, units and IDs"]
    B --> C{"Binding, policy and prerequisites verified?"}
    C -->|"No"| X["Qualified unsupported or validation outcome"]
    C -->|"Yes"| D["Use session COM execution owner"]
    D --> E["Capture relevant context and preconditions"]
    E --> F["Invoke one intended native operation or stage"]
    F --> G["Interpret operation-specific native return"]
    G --> H{"Stage qualified?"}
    H -->|"No"| Y["Report failure and already completed stages"]
    H -->|"Yes"| I["Validate outputs or created identity"]
    I --> J{"More approved stages?"}
    J -->|"Yes"| F
    J -->|"No"| K["Apply qualified managed updates"]
    K --> L["Return named outcome with context and partial-effect evidence"]
~~~

Queries use the same binding/status/data discipline without mutation stages. Empty batches can succeed without native calls; they cannot certify that model data was observed. Composite operations are not automatically transactional.

| Edge case | Handling |
| --- | --- |
| Blank/model-less instance | Distinguish not-ready/no model from valid empty properties; do not create/open a model implicitly. |
| Model changes after count/validation | Bound counts and allocations; verify list length and coupled data. Coordinate with edits; report races instead of returning default entries. |
| Native 0/1/negative/count/index | Decode by operation. Never use a universal nonzero-success rule. |
| Zero offset versus no specification/error | Preserve native -6027/no-offset and invalid-member outcomes separately from successful zero vector. |
| Unknown section/catalog/spec code | Retain raw code and return unsupported interpretation; do not reinterpret through a different year or country. |
| Unassigned property/material | Use explicit not-assigned/not-found result; avoid fabricated default material or thickness. |
| Material creation updates an existing name | Report Updated, not a newly assigned ID. Document collision/overwrite behavior before invoking. |
| Current material/database context | Coordinate context selection and dependent creation. Restore only when verified and explicitly safe; report persistent context changes and partial effects. |
| Table creation/add/property creation | Track every created object and returned status. Stop after failure; do not delete shared definitions as a guessed rollback. |
| Partial member/batch assignment | Retain per-ID outcome where observable and state uncertainty for atomicity not guaranteed by native API. |
| Release arrays/MP flags | Validate six DOFs, spring dimensions, scalar and rotational factors, supported sentinel values and valid ranges. |
| Triangular plates and element offsets | Preserve incidence versus node-ID distinction, repeated corners and correct coordinate/offset selector. |
| Invalid unit conversion | Apply dimensional factors once; no hard-coded ×1000, stress-versus-weight-density confusion or omitted modulus scaling. |
| Invalid successful buffers | Reject wrong rank/type/cardinality, NaN/Infinity and unwritten sentinel values. Honor SAFEARRAY lower bounds and own storage. |
| Global folder or catalog changes | Document native scope; avoid changing another wrapper's assumptions and invalidate dependent catalog evidence. |
| Refresh/dispose/session switching | Reject stale receivers and capabilities; never silently reattach to a different active model. |
| Rejected/busy COM call | Only bounded retry of established transient transport failures where duplication is controlled. Do not repeat mutations after ambiguous completion. |
| Cancellation/timeout | Honor before/between stages. A timeout does not prove native mutation was aborted. Preserve uncertain/partial completion. |

A local adapter lock is not assembly-wide COM coordination. Property, Geometry, Load, Root, Output and raw consumer access may share a native session. Synchronous calls must honor an established apartment or explicit session executor; PLINQ/Task.Run do not provide that guarantee.

Keep pure factory/grouping/transform work separate from native calls. No background parallel mutation or native cancellation guarantee should be inferred from the source's nThreads overloads. This plan does not change the net481 target or establish .NET Core package support.

## 10. Implementation sequence

| Phase | Work | Completion gate |
| --- | --- | --- |
| P0 | Approve managed contracts and exclusions. | Separate five reserved entries, two private helpers and deprecated setter; agree ambiguity/failure and dependency rules. |
| P1 | Minimal binding, policy and value readers. | Reuse wrapper native version and strict readers; per-session evidence; no global version switch or partial adapter. |
| P2 | Qualified offsets, releases, beta and entity-property references. | Thirteen names; axes/end/DOF identity and errors qualified. Unlock Output offset-read dependency where verified. |
| P3 | Material queries and assignments read-back. | Twenty-three names; name/index distinctions, full fields, immutable correspondence and absence handling. |
| P4 | Section/thickness read-back and schemas. | Twenty-four names; nine changed layouts, native count bounds and typed named values verified. |
| P5 | Material creation/context and basic assignments. | Seventeen names; explicit Created/Updated statuses, units, current material and partial assignment behavior. |
| P6 | Standard/composite/prismatic/tapered creation. | Eighteen names; typed input packets, BC selector, native material preconditions and safe created identities. |
| P7 | Specification/attribute creation and remaining reads. | Forty-six names; release/MP, cable, fireproof, control-dependent and element attributes with operation-specific status/index handling. |
| P8 | UPT lifecycle and newer general-profile APIs. | Thirty names; staged creation, reserved inner restrictions, named coordinate results and optional capability acquisition. |
| P9 | Catalog identities and database-folder APIs. | Nine names; verified folder status, country/record identity and five newer optional functions. |
| P10 | Controlled removals, publication and cleanup. | Twenty-four names; explicit scope, partial effects, no destructive guessed rollback and verified publication side effects. |
| P11 | Supplemental surface contracts and release verification. | Fifteen discovery candidates outside the group partition, then native/API/package/consumer gates. |

This partitions the 212 reviewed group names: eight exclusions plus 204 active candidates. A phase is a review allocation, not permission to implement guessed native contracts. Investigations such as inactive-list transport, FindUPTTable, property-reference aliases and created-spec identity can remain gated while other verified functions progress.

### Function allocation

| Workstream | Reviewed native names | Allocated names |
| --- | --- | --- |
| P0 | 8 | `RemoveBeamPropertyHelper`, `AssignParametricSurfaceThickness`, `RemoveBeamMaterialHelper`, `SetMaterialID`, `AssignMaterialToParametricSurface`, `GetProfileBoundaryInformation`, `GetProfileNoOfPoints`, `GetProfileBoundaryPoints` |
| P2 | 13 | `GetBetaAngle`, `GetElementOffsetSpecCount`, `GetMemberReleaseSpecEx`, `GetMemberReleaseSpec`, `GetMemberLocalOffSet`, `GetMemberGlobalOffSet`, `GetBeamProperty`, `GetBeamPropertyAll`, `GetBeamConstants`, `GetElementLocalOffset`, `GetElementGlobalOffset`, `GetElementOffsetSpec`, `GetPlateSectionPropertyRefNo` |
| P3 | 23 | `GetIsotropicMaterialAssignedBeamCount`, `GetIsotropicMaterialAssignedBeamList`, `GetMaterialProperty`, `GetBeamMaterialName`, `GetElementMaterialName`, `GetIsotropicMaterialCount`, `GetIsotropicMaterialProperties`, `GetMaterialPropertyEx`, `GetIsotropicMaterialPropertiesEx`, `GetIsotropicMaterialPropertiesAssigned`, `GetOrthotropic2DMaterialCount`, `GetOrthotropic2DMaterialProperties`, `GetOrthotropic3DMaterialCount`, `GetOrthotropic3DMaterialProperties`, `DeleteMaterial`, `RemoveMaterialFromBeam`, `GetPlateMaterialName`, `GetIsotropicMaterialAssignedPlateCount`, `GetSolidMaterialName`, `GetIsotropicMaterialAssignedSolidCount`, `GetIsotropicMaterialAssignedSolidList`, `SetTypeToIsotropicMaterial`, `GetTypeForIsotropicMaterial` |
| P4 | 24 | `GetSectionPropertyValues`, `GetSectionPropertyAssignedBeamCount`, `GetSectionPropertyAssignedBeamList`, `GetBeamSectionPropertyRefNo`, `GetBeamSectionPropertyValuesEx`, `GetSectionPropertyValuesEx`, `GetSectionTableNo`, `GetBeamSectionPropertyTypeNo`, `GetBeamSectionName`, `GetBeamSectionDisplayName`, `GetCountryTableNo`, `GetSectionPropertyCount`, `GetSectionPropertyList`, `GetSectionPropertyName`, `GetSectionPropertyType`, `GetSectionPropertyCountry`, `GetPlateThickness`, `GetThicknessPropertyCount`, `GetThicknessPropertyList`, `GetThicknessPropertyAssignedPlateCount`, `GetThicknessPropertyAssignedPlateList`, `GetThicknessPropertyValues`, `GetIsotropicMaterialAssignedPlateList`, `GetCountofSectionPropertyValuesEx` |
| P5 | 17 | `AssignBetaAngle`, `CreateAssignProfileProperty`, `AssignBeamProperty`, `AssignMemberSpecToBeam`, `AssignPlateThickness`, `AssignElementSpecToPlate`, `UpdatePropertiesToDesignSection`, `CreateIsotropicMaterialProperties`, `CreateIsotropicMaterialPropertiesEx`, `CreateIsotropicMaterialSteel`, `CreateIsotropicMaterialConcrete`, `CreateIsotropicMaterialAluminum`, `CreateIsotropicMaterialTimber`, `SetMaterialName`, `AssignMaterialToMember`, `AssignMaterialToPlate`, `AssignMaterialToSolid` |
| P6 | 18 | `CreateBeamPropertyFromTable`, `CreateBeamPropertyFromTableWithCoverPlates`, `CreateBeamPropertyFromTableComposite`, `CreateWideFlangePropertyFromTable`, `CreateBeamPropertyFromTableEx`, `CreateTeePropertyFromTable`, `CreateChannelPropertyFromTable`, `CreateAnglePropertyFromTable`, `CreateTubePropertyFromTable`, `CreatePipePropertyFromTable`, `CreatePrismaticRectangleProperty`, `CreatePrismaticCircleProperty`, `CreatePrismaticTeeProperty`, `CreatePrismaticTrapezoidalProperty`, `CreatePrismaticGeneralProperty`, `CreateTaperedIProperty`, `CreateTaperedTubeProperty`, `CreatePlateThicknessProperty` |
| P7 | 46 | `CreateMemberReleaseSpec`, `CreateMemberPartialReleaseSpec`, `CreateMemberTrussSpec`, `CreateMemberInactiveSpec`, `CreateMemberTensionSpec`, `CreateMemberCompressionSpec`, `CreateMemberIgnoreStiffSpec`, `CreateMemberCableSpec`, `CreateMemberCableSpecEx`, `CreateMemberOffsetSpec`, `CreateElementNodeReleaseSpec`, `CreateElementPlaneStressSpec`, `CreateElementIgnoreInplaneRotnSpec`, `AddControlDependentRelation`, `CreateElementOffsetSpec`, `CreateElementLocalZOffsetSpec`, `CreateMemberFireProofingSpec`, `GetMemberSpecCode`, `GetPropertyUniqueID`, `SetPropertyUniqueID`, `GetInactiveMemberCount`, `GetInactiveMemberList`, `GetAlphaAngleForSection`, `GetCentroidLocationForSection`, `GetFireProofedBeamCount`, `GetFireProofedBeamList`, `GetFireProofDataForBeam`, `GetFireProofingSpecCount`, `GetFireProofingSpecDetails`, `GetFireProofingSpecAssignedBeamCount`, `GetFireProofingSpecAssignedBeamList`, `CreateMemberAttribute`, `CreateElementAttribute`, `DeleteMemberAttribute`, `GetMemberCountByAttribute`, `GetMemberListByAttribute`, `GetMemberAttributeCount`, `GetMemberAttributeList`, `DeleteElementAttribute`, `GetElementCountByAttribute`, `GetElementListByAttribute`, `GetAssignedAttributeCount`, `GetAssignedAttributeByIndex`, `RemoveAttribute`, `AssignMemberAttribute`, `AssignElementAttribute` |
| P8 | 30 | `CreateUPTTable`, `AddUPTPropertyWIDEFLANGE`, `AddUPTPropertyWIDEFLANGEUNEQUAL`, `AddUPTPropertyWIDEFLANGECOMPOSITE`, `AddUPTPropertyCHANNEL`, `AddUPTPropertyANGLE`, `AddUPTPropertyDOUBLEANGLE`, `AddUPTPropertyTEE`, `AddUPTPropertyPIPE`, `AddUPTPropertyTUBE`, `AddUPTPropertyGENERAL`, `AddUPTPropertyISECTION`, `AddUPTPropertyPRISMATIC`, `CreatePropertyFromUPTTable`, `GetUptGeneralProfilePointsCount`, `GetUptGeneralProfileBoundaryPoints`, `GetUptGeneralStressLocationPoints`, `FindUPTTable`, `CreateUPTTableEx`, `RemoveUPTTable`, `RemovePropertyFromUPTTable`, `CreatePropertyFromUserTable`, `GetUserProvidedTableCount`, `GetUserProvidedTableList`, `GetUserProvidedTableSectionCount`, `GetUserProvidedTableSectionList`, `GetUserProvidedTableSectionType`, `GetUserProvidedTableSectionPropertyCount`, `GetUserProvidedTableSectionProperties`, `GetUserProvidedTableNo` |
| P9 | 9 | `GetShapeCode`, `GetRecordForSection`, `SetStandardProfileDBFolder`, `GetStandardProfileDBFolder`, `GetDefaultStandardProfileDBFolder`, `IsStandardDatabaseSection`, `GetStandardSectionDatabaseName`, `GetStandardSectionTableName`, `GetStandardSectionName` |
| P10 | 24 | `RemovePropertyFromPlate`, `RemoveMaterialFromPlate`, `RemoveMaterialFromSolid`, `DeleteMemberReleaseSpec`, `DeleteAllControlDependentRelations`, `RemoveAllElementOffsetSpec`, `RemovePropertyFromBeam`, `DeleteProperty`, `DeleteMemberSpec`, `RemoveMemberReleaseSpecFromBeam`, `RemoveMemberOffsetSpecFromBeam`, `RemoveMemberTrussSpecFromBeam`, `RemoveMemberInactiveSpecFromBeam`, `RemoveMemberTensionSpecFromBeam`, `RemoveMemberCompressionSpecFromBeam`, `RemoveMemberIgnoreStiffSpecFromBeam`, `RemoveMemberCableSpecFromBeam`, `RemoveElementPlaneStressSpecFromPlate`, `RemoveElementIgnoreInplaneRotnSpecFromPlate`, `RemoveElementNodeReleaseSpecFromPlate`, `RemoveAllElementNodeReleaseSpec`, `RemoveMemberFireProofingSpecFromBeam`, `GetPublishedProfileName`, `GetSTAADProfileName` |

The current implementation includes P1 and all native routes allocated to P2, P3, P4, P5 and P6. P7 has forty-two routes and four gated contracts remaining; P8 has three routes, with the next sequence starting at AddUPTPropertyWIDEFLANGECOMPOSITE. Later phases retain their original allocation order. Live COM, mutation persistence and engineering qualification remain separate completion gates.

Port source models/helper behavior alongside each usable function. The 103 interface signatures and four extra conveniences are source-parity evidence; their obsolete/inconsistent signatures need deliberate adaptation, not an unconditional copy-all completion claim.

## 11. IntelliSense, tests and release gates

Every new public interface/adapter method, request/result property and enum value needs XML documentation for descriptive parameter names, valid ranges, units/axes/reference point, returned status/data, absence versus failure, version/capability requirements, side effects, COM execution ownership and exceptions.

Examples must inspect outcomes before consuming data and guard native mutation explicitly. Optional interfaces are acquired through the capability gateway. Include realistic single/batch and source-model reuse examples without executing destructive operations during documentation compilation.

| Verification layer | Required evidence |
| --- | --- |
| Inventory | 204/209/209 names, 199/207/207 details, 212 union, 201 common, 103 matching source signatures, four adapter extras and delegated targets. |
| Version policies | Nine section rows, folder status 1/0 versus 0/-1, record identity, country/catalog domains, unknown and mixed-version wrappers. |
| Optional access | Eight newer detailed names, no reserved fallback, verified receiver signatures and cast-bypass prevention. |
| Native return handling | Boolean, zero-success, created IDs, Created/Updated status, direct counts, zero-based indices, no-offset and unknown codes. |
| Buffers | Correct native element types, lower bounds, sentinel/finite checks, dynamic counts, reserved fields and paired coordinate arrays. |
| Dependencies | Safe catalog absence, no proprietary build reference, BC/TC correctness, no null factory dereference and per-layout section hydration. |
| Mutations | Material/context collisions, UPT staged failures, shared spec IDs, partial assignment, no duplicated side effects after timeouts/retries. |
| Units/engineering | E/G/strength/density/thermal dimensions, thickness and offset units, release spring/MP semantics and axes/incidence orientation. |
| Lifetime/execution | Refresh/dispose, stale capabilities, two acquired versions/sessions, apartment ownership, reentrancy and truthful cancellation behavior. |
| XML/public API | Complete interface/adapter/model docs, no new public ref/out/tuple/proprietary COM types, compiled guarded examples and existing API compatibility. |
| Native fixtures | Every advertised version/build and relevant section/material/spec family, empty/unassigned/error data, catalogs and known physical results. |
| Distribution | Correct Windows/framework requirements, DLL/XML/dependencies, clean PackageReference consumer, actual coverage in README/release notes and explicitly requested package release. |

The original static-analysis review did not execute STAAD. Subsequent implementation validation is recorded above; it uses managed stand-ins and does not certify live native invocation or engineering correctness.

## 12. Review outcome and boundaries

Proceed after approval with one adapter, common internal services and small verified contract policies. Reuse private models and grouping/factory behavior selectively, correct documented source limitations and keep unsupported native contracts explicit.

The original comparison remains the evidence baseline. The current implementation has 249 common overloads, five optional catalog queries and 195 native routes with full XML; wrapper/provider behavior and target frameworks are unchanged. Validation uses managed stand-ins and an installed local preview package, not live STAAD qualification or public publication. A public release still requires reviewed version/feature claims and current package-facing documentation.
