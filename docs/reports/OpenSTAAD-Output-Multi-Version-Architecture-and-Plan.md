# OpenSTAAD Output multi-version comparison, architecture and implementation plan

**Review date:** 22 October 2026<br>
**Baseline:** main at 3c67959163450d1b2c150bbc8f25a5c1f99dc30c<br>
**Scope:** Installed STAAD.Pro 2024, 2025 and 2026 Output contracts, including Output-owned steel results filed under Design; current production code; the strictly read-only main project; and passive vendor interop metadata.<br>
**Status:** Documentation and architecture proposal only. Output domain functions and the proposed internal policies remain unimplemented.

## 1. Findings and recommended direction

The three installed references document **59 / 60 / 60 Output function names**. Their union contains 60 names; 59 occur in all three years. The Analysis Results tree alone contains 48 / 49 / 49; eleven further functions belong to OSOutputUI but appear in the Steel Design Results section. Including them prevents an incomplete Output comparison.

GetMaxBeamStresses appears in the 2025/2026 help and generated Output header, and is absent from those reviewed 2024 sources. All 59 common native declarations match. Their semantics require closer attention: **four common functions have changed input/output documentation that warrants version policies**, one has a parameter-name clarification, and five have only formatting or typo changes. The normalized 2025 and 2026 declarations, contracts and examples match.

The current IOSOutput / OSOutputAdapter are **binding-only placeholders with zero domain methods**. Separately, the implemented Root member-force service already calls two Output members. The private main project exposes **86 managed overloads backed by 17 native targets**; this provides reusable functionality, but does not cover every native Output function and includes behaviors that should be corrected during porting.

Use shared internal services for common extraction and decoding, with narrow policies for the three direction arguments and member-end displacement layout. Select the profile from **wrapper.StaadVersion**, which identifies the acquired native server. **wrapper.ApplicationVersion identifies the host/client assembly** and cannot select STAAD behavior. Treat help differences as candidate contracts requiring native confirmation rather than assuming every difference is a tested binary change.

| Measure | Finding |
| --- | ---: |
| Pages traversed per year / pages containing Output functions | 10 / 9 |
| Detailed native functions, 2024 / 2025 / 2026 | 59 / 60 / 60 |
| Union / intersection of documented names | 60 / 59 |
| Common functions with changed native declarations | 0 |
| Common functions with unchanged semantics after formatting cleanup | 54 |
| Common functions needing input/output policy review | 4 |
| Further parameter-name clarifications | 1 |
| Further formatting or typo-only changes | 5 |
| Common functions whose examples changed, 2024 to 2025 | 49 |
| Normalized changes, 2025 to 2026 | 0 |
| Current IOSOutput / OSOutputAdapter domain overloads | 0 / 0 |
| Native names currently dispatched through Root | 2 |
| Documented names with no current production dispatch | 58 |
| Main-source interface / public adapter overloads | 86 / 86 |
| Main-source private adapter helpers | 24 |
| Main-source Task-returning / public out overloads | 33 / 2 |
| Distinct documented native targets in the main Output adapter | 17 |
| Documented native names without a main Output target | 43 |
| Methods in each inspected 2025/2026 IOSOutputUI | 72 |
| Documented names represented in that metadata | 59 |
| Supplemental summary-only class / interop unit-query names | 13 |
| Help names absent from both inspected interfaces | 1 |
| Union of documented and inspected metadata names | 73 |

**All 60 typed Output functions are pending.** “Two native names already dispatched through Root” means existing assembly reuse, not two implemented IOSOutput functions. RawOutput and ComObject bindings do not constitute typed result APIs. Similarly, 86 private overloads do not mean 86 native functions.

The full parameter/return inventory, every private overload, source links and fingerprints are in the [contract annex](OpenSTAAD-Output-API-Coverage-and-Contract-Inventory.md). The [comparison evidence](data/OpenSTAAD-Output-2024-2026-Comparison.json) retains the exact reviewed declarations and per-year contracts.

## 2. References, ownership and review method

| Reference | Help build | Documented Output functions |
| --- | --- | ---: |
| [STAAD.Pro 2024 Analysis Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___ana_rlt.html) and [Steel Design Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html) | 24.00.02.354 | 59 |
| [STAAD.Pro 2025 Analysis Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___ana_rlt.html) and [Steel Design Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html) | 25.00.01.424 | 60 |
| [STAAD.Pro 2026 Analysis Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___ana_rlt.html) and [Steel Design Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html) | 26.00.00.340 | 60 |

| Help section | 2024 | 2025 | 2026 |
| --- | --- | --- | --- |
| [Analysis Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___ana_rlt.html) | 1 | 1 | 1 |
| [Design: Steel Design Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html) | 11 | 11 | 11 |
| [Result: Nodes/Joints/Supports Analysis Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___nod_rlt.html) | 4 | 4 | 4 |
| [Result: Members Analysis Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html) | 12 | 12 | 12 |
| [Result: Plates Analysis Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html) | 11 | 11 | 11 |
| [Result: Solids Analysis Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_rlt.html) | 4 | 4 | 4 |
| [Result: Dynamic Analysis Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html) | 11 | 12 | 12 |
| [Result: Static Analysis Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___static_check.html) | 1 | 1 | 1 |
| [Result: Buckling Analysis Result Analysis Results](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___buckling_rlt.html) | 4 | 4 | 4 |
| [Design Steel: Chinese Steel Design Design Results › Design: Steel](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html) | 0 | 0 | 0 |


The linked Chinese Steel page contains functions owned by other native interfaces, so it contributes zero to this Output inventory. Review followed native ownership rather than assigning all functions under a navigation heading to one adapter. GetMaxBeamStresses is filed under Dynamic Results even though it returns beam stresses; that location does not restrict it to a managed “dynamic-only” family.

The review:

1. Traversed both help entry points and their group links, reconciled summaries with detailed declarations, and retained every Output-owned function.
2. Compared normalized declarations, non-example contracts and examples separately. Excluded generated navigation, tooltips, HTML formatting and syntax headings from semantic comparison.
3. Read all parameter and return tables, including nested tables and unmatched parameter aliases. Declaration names were not silently matched to different table entries.
4. Parsed current production methods and both private Output files using Roslyn, then inspected call sites, result models and dependencies. No main project was built or modified.
5. Inspected installed 2025 and 2026 IOSOutputUI metadata in separate reflection-only processes. No native object was acquired or invoked.
6. Recorded group-page, supplemental class-listing, generated-header, vendor-binary and relevant source fingerprints in the [annex evidence section](OpenSTAAD-Output-API-Coverage-and-Contract-Inventory.md#7-reproducibility-evidence).

The corresponding 2024 vendor interop DLL was not found in the expected STAAD subfolder. This limits metadata comparison; it does not imply that STAAD.Pro 2024 lacks COM support. Metadata presence, help presence, native IDispatch availability, successful marshaling, result availability, licensing and engineering correctness remain separate evidence layers.

## 3. Complete cross-version function matrix

Every row is a native OSOutputUI name. The return shown is the native declaration type, not the proposed managed return class. The annex expands each row into parameters, logical directions, return codes, source coverage, proposed managed signature and workstream.

| Native Output function | 2024 | 2025 / 2026 | Comparison | Current coverage | Main-source target | Plan |
| --- | --- | --- | --- | --- | --- | --- |
| [AreResultsAvailable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___ana_rlt.html#gab326f7578cd6d53aa4f4400f74640491) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___ana_rlt.html#gab326f7578cd6d53aa4f4400f74640491) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___ana_rlt.html#gab326f7578cd6d53aa4f4400f74640491) / `VARIANT` | Unchanged | Root delegation only | [Called](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L49) | O2 |
| [GetMemberDesignSectionName](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gaa4e3a6905c36de9f5e0edb39ab022217) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gaa4e3a6905c36de9f5e0edb39ab022217) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gaa4e3a6905c36de9f5e0edb39ab022217) / `VARIANT` | Unchanged | Pending | Not called | O5 |
| [IsMultipleMemberSteelDesignResultsAvailable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#ga23663ecf75659aa873be867cea01f42c) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#ga23663ecf75659aa873be867cea01f42c) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#ga23663ecf75659aa873be867cea01f42c) / `VARIANT` | Unchanged | Pending | Not called | O10 |
| [GetSteelDesignParameterBlockCount](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#ga65c592e2e4b54a57c0321bbb38e29939) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#ga65c592e2e4b54a57c0321bbb38e29939) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#ga65c592e2e4b54a57c0321bbb38e29939) / `VARIANT` | Unchanged | Pending | Not called | O10 |
| [GetSteelDesignParameterBlockNameByIndex](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gae4d12cb96249a53e02ac209d4ec28bb7) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gae4d12cb96249a53e02ac209d4ec28bb7) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gae4d12cb96249a53e02ac209d4ec28bb7) / `VARIANT` | Unchanged | Pending | Not called | O10 |
| [GetMultipleMemberSteelDesignRatio](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gae9107fd34396f30c46e3e34660194db4) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gae9107fd34396f30c46e3e34660194db4) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gae9107fd34396f30c46e3e34660194db4) / `VARIANT` | Unchanged | Pending | Not called | O10 |
| [GetMultipleMemberSteelDesignMaxRatio](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gab44e212393084c79c2da98b206b9c612) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gab44e212393084c79c2da98b206b9c612) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gab44e212393084c79c2da98b206b9c612) / `VARIANT` | Unchanged | Pending | Not called | O10 |
| [GetMultipleMemberSteelDesignResults](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gaa2dd5aae686f3943747519b19cc2dbcd) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gaa2dd5aae686f3943747519b19cc2dbcd) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gaa2dd5aae686f3943747519b19cc2dbcd) / `VARIANT` | Unchanged | Pending | Not called | O10 |
| [GetMemberSteelDesignRatio](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gafcd9cbfb77d9a4df423fb92993d542c4) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gafcd9cbfb77d9a4df423fb92993d542c4) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gafcd9cbfb77d9a4df423fb92993d542c4) / `VARIANT` | Unchanged | Pending | [Called](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L178) | O5 |
| [GetMemberSteelDesignResults](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gaf0f6f888d59db3ae5f4896025fc44dd2) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gaf0f6f888d59db3ae5f4896025fc44dd2) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gaf0f6f888d59db3ae5f4896025fc44dd2) / `VARIANT` | Unchanged | Pending | [Called](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L155) | O5 |
| [GetMemberSteelDesignMinFailureRatio](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#ga33d973a3a58d9bb7dbb1a551dbe112c8) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#ga33d973a3a58d9bb7dbb1a551dbe112c8) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#ga33d973a3a58d9bb7dbb1a551dbe112c8) / `VARIANT` | Unchanged | Pending | Not called | O10 |
| [GetMemberSteelDesignMaxFailureRatio](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#ga5bedb4627e488a567f4786374d07cea9) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#ga5bedb4627e488a567f4786374d07cea9) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#ga5bedb4627e488a567f4786374d07cea9) / `VARIANT` | Unchanged | Pending | Not called | O10 |
| [GetNodeDisplacements](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___nod_rlt.html#gad7bb8f56f4f2096c0240d644e99479dc) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___nod_rlt.html#gad7bb8f56f4f2096c0240d644e99479dc) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___nod_rlt.html#gad7bb8f56f4f2096c0240d644e99479dc) / `VARIANT` | Unchanged | Pending | Not called | O2 |
| [GetSupportReactions](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___nod_rlt.html#ga8bfa8a82d929856113b7920b030989d1) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___nod_rlt.html#ga8bfa8a82d929856113b7920b030989d1) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___nod_rlt.html#ga8bfa8a82d929856113b7920b030989d1) / `VARIANT` | Unchanged | Pending | [Called](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L71) | O2 |
| [GetMatInfluenceAreas](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___nod_rlt.html#ga8a408a6468ecf006e35de27075584856) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___nod_rlt.html#ga8a408a6468ecf006e35de27075584856) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___nod_rlt.html#ga8a408a6468ecf006e35de27075584856) / `VARIANT` | Unchanged | Pending | Not called | O7 |
| [GetBasePressures](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___nod_rlt.html#ga6b71fd3346b2e89a6c47bfd7945f0d99) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___nod_rlt.html#ga6b71fd3346b2e89a6c47bfd7945f0d99) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___nod_rlt.html#ga6b71fd3346b2e89a6c47bfd7945f0d99) / `VARIANT` | Formatting or typo only | Pending | Not called | O7 |
| [GetMemberEndDisplacements](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#gaf4f46574a9c80c6398e062781d131cf2) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#gaf4f46574a9c80c6398e062781d131cf2) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#gaf4f46574a9c80c6398e062781d131cf2) / `VARIANT` | Version policy candidate | Pending | Not called | O6 |
| [GetIntermediateMemberTransDisplacements](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#ga0ac61ab06bab4b6188a03445edc8bacd) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#ga0ac61ab06bab4b6188a03445edc8bacd) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#ga0ac61ab06bab4b6188a03445edc8bacd) / `VARIANT` | Unchanged | Pending | Not called | O6 |
| [GetMemberEndForces](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#ga9ee15e2a2869bfb68049fcc03d062329) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#ga9ee15e2a2869bfb68049fcc03d062329) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#ga9ee15e2a2869bfb68049fcc03d062329) / `VARIANT` | Unchanged | Root delegation only | [Called](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L233) | O2 |
| [GetPMemberEndForces](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#gafd324bfa929f74ec38f73d4d43463060) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#gafd324bfa929f74ec38f73d4d43463060) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#gafd324bfa929f74ec38f73d4d43463060) / `VARIANT` | Formatting or typo only | Pending | Not called | O6 |
| [GetMinMaxBendingMoment](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#gaf63c90d9d6acf2007bd0dc194d59a813) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#gaf63c90d9d6acf2007bd0dc194d59a813) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#gaf63c90d9d6acf2007bd0dc194d59a813) / `VARIANT` | Version policy candidate | Pending | Not called | O6 |
| [GetMinMaxShearForce](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#gac5691dcf8af2b39c3ca8a1d6ed06a93a) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#gac5691dcf8af2b39c3ca8a1d6ed06a93a) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#gac5691dcf8af2b39c3ca8a1d6ed06a93a) / `VARIANT` | Version policy candidate | Pending | Not called | O6 |
| [GetMinMaxAxialForce](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#ga072e86e3cb9e6c606510480f01ce76a9) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#ga072e86e3cb9e6c606510480f01ce76a9) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#ga072e86e3cb9e6c606510480f01ce76a9) / `VARIANT` | Formatting or typo only | Pending | Not called | O6 |
| [GetIntermediateMemberForcesAtDistance](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#ga914b3980dd567b9042f94fca42a7e641) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#ga914b3980dd567b9042f94fca42a7e641) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#ga914b3980dd567b9042f94fca42a7e641) / `VARIANT` | Unchanged | Pending | Not called | O6 |
| [GetPMemberIntermediateForcesAtDistance](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#ga43d4b974d3b4f05d28156edbf7a390f6) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#ga43d4b974d3b4f05d28156edbf7a390f6) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#ga43d4b974d3b4f05d28156edbf7a390f6) / `VARIANT` | Unchanged | Pending | Not called | O6 |
| [GetMaxSectionDisplacement](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#ga1ddbbc7a3b403db9d08040fff625326b) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#ga1ddbbc7a3b403db9d08040fff625326b) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#ga1ddbbc7a3b403db9d08040fff625326b) / `VARIANT` | Version policy candidate | Pending | Not called | O6 |
| [GetIntermediateDeflectionAtDistance](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#ga8adea9e89760a8344ca297b111408b56) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#ga8adea9e89760a8344ca297b111408b56) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#ga8adea9e89760a8344ca297b111408b56) / `VARIANT` | Unchanged | Pending | Not called | O6 |
| [GetIntermediateMemberAbsTransDisplacements](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#ga06e324e0eed5e1b970331c6f113d6f34) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#ga06e324e0eed5e1b970331c6f113d6f34) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#ga06e324e0eed5e1b970331c6f113d6f34) / `VARIANT` | Unchanged | Pending | Not called | O6 |
| [GetAllPlateCenterStressesAndMoments](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#ga461a2ec82c9fb789f92cfb7f705a3e88) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#ga461a2ec82c9fb789f92cfb7f705a3e88) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#ga461a2ec82c9fb789f92cfb7f705a3e88) / `VARIANT` | Unchanged | Pending | Not called | O3 |
| [GetPlateCenterNormalPrincipalStresses](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#gae2958f7723d482e818d88e51d320880f) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#gae2958f7723d482e818d88e51d320880f) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#gae2958f7723d482e818d88e51d320880f) / `VARIANT` | Unchanged | Pending | [Called](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L895) | O3 |
| [GetAllPlateCenterForces](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#gad14d57e4e0404457454a3ea1394a8537) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#gad14d57e4e0404457454a3ea1394a8537) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#gad14d57e4e0404457454a3ea1394a8537) / `VARIANT` | Unchanged | Pending | [Called](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L865) | O3 |
| [GetAllPlateCenterMoments](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#ga911d707d71f6114ca6587ee315e168fa) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#ga911d707d71f6114ca6587ee315e168fa) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#ga911d707d71f6114ca6587ee315e168fa) / `VARIANT` | Unchanged | Pending | [Called](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L873) | O3 |
| [GetAllPlateCenterPrincipalStressesAndAngles](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#gac13e9962fbf82d34625f618241fc733c) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#gac13e9962fbf82d34625f618241fc733c) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#gac13e9962fbf82d34625f618241fc733c) / `VARIANT` | Unchanged | Pending | Not called | O3 |
| [GetAllPlateCenterPrincipalStressesAndAnglesEx](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#ga7a893aa342562cc268da3d1a8a1ea49c) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#ga7a893aa342562cc268da3d1a8a1ea49c) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#ga7a893aa342562cc268da3d1a8a1ea49c) / `VARIANT` | Unchanged | Pending | Not called | O3 |
| [GetPlateCenterVonMisesStresses](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#ga2ee9f974e72eec15760b7e6da620d7c8) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#ga2ee9f974e72eec15760b7e6da620d7c8) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#ga2ee9f974e72eec15760b7e6da620d7c8) / `VARIANT` | Unchanged | Pending | [Called](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L883) | O3 |
| [GetPlateCornerForces](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#ga68bc8a450a29bc91630da63201e05870) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#ga68bc8a450a29bc91630da63201e05870) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#ga68bc8a450a29bc91630da63201e05870) / `VARIANT` | Unchanged | Pending | [Called](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L378) | O4 |
| [GetResultantForceAlongLineForPlateList](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#ga16b671df915dfa2ca6691372f0c827be) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#ga16b671df915dfa2ca6691372f0c827be) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#ga16b671df915dfa2ca6691372f0c827be) / `VARIANT` | Formatting or typo only | Pending | [Called](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L808) | O4 |
| [GetResultantForceAlongLineForParametricSurface](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#ga2d8565301564943488f7604ce1b44e8b) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#ga2d8565301564943488f7604ce1b44e8b) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#ga2d8565301564943488f7604ce1b44e8b) / `VARIANT` | Formatting or typo only | Pending | Not called | O4 |
| [GetPlateStressAtPoint](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#ga7e9ab82a5558ce71e3926e225755acc6) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#ga7e9ab82a5558ce71e3926e225755acc6) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#ga7e9ab82a5558ce71e3926e225755acc6) / `VARIANT` | Parameter-name clarification | Pending | [Called](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1941) | O4 |
| [GetAllSolidNormalStresses](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_rlt.html#gaba6b09a64357f09d33a1dfa8588028bc) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_rlt.html#gaba6b09a64357f09d33a1dfa8588028bc) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_rlt.html#gaba6b09a64357f09d33a1dfa8588028bc) / `VARIANT` | Unchanged | Pending | Not called | O7 |
| [GetAllSolidShearStresses](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_rlt.html#ga58a11e689111b93551978060d3beb068) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_rlt.html#ga58a11e689111b93551978060d3beb068) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_rlt.html#ga58a11e689111b93551978060d3beb068) / `VARIANT` | Unchanged | Pending | Not called | O7 |
| [GetAllSolidPrincipalStresses](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_rlt.html#ga7e416935359b156473f8f628ea038729) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_rlt.html#ga7e416935359b156473f8f628ea038729) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_rlt.html#ga7e416935359b156473f8f628ea038729) / `VARIANT` | Unchanged | Pending | Not called | O7 |
| [GetAllSolidVonMisesStresses](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_rlt.html#ga2203456c21ee383624d6a165b279057f) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_rlt.html#ga2203456c21ee383624d6a165b279057f) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_rlt.html#ga2203456c21ee383624d6a165b279057f) / `VARIANT` | Unchanged | Pending | Not called | O7 |
| [GetNLLoadStep](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#gaabe6a654c4c8143ebb2742cb7fb689dd) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#gaabe6a654c4c8143ebb2742cb7fb689dd) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#gaabe6a654c4c8143ebb2742cb7fb689dd) / `VARIANT` | Unchanged | Pending | Not called | O8 |
| [GetNLNodeDisplacements](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#gac9537ae0e34eb6d10cc8119749e7aa78) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#gac9537ae0e34eb6d10cc8119749e7aa78) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#gac9537ae0e34eb6d10cc8119749e7aa78) / `VARIANT` | Unchanged | Pending | Not called | O8 |
| [GetNoOfModesExtracted](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#ga9332682c8f686e537fbbe73131a02eed) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#ga9332682c8f686e537fbbe73131a02eed) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#ga9332682c8f686e537fbbe73131a02eed) / `VARIANT` | Unchanged | Pending | Not called | O8 |
| [GetModeFrequency](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#ga19eeb7e1d7f95329e97881d4aecdc42c) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#ga19eeb7e1d7f95329e97881d4aecdc42c) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#ga19eeb7e1d7f95329e97881d4aecdc42c) / `VARIANT` | Unchanged | Pending | Not called | O8 |
| [GetModalDisplacementAtNode](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#ga6ad3b3a30f91debfd7a443ebc279cea1) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#ga6ad3b3a30f91debfd7a443ebc279cea1) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#ga6ad3b3a30f91debfd7a443ebc279cea1) / `VARIANT` | Unchanged | Pending | Not called | O8 |
| [GetModalMassParticipationFactors](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#ga118cbf967b0d0392fa170b5f57f64081) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#ga118cbf967b0d0392fa170b5f57f64081) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#ga118cbf967b0d0392fa170b5f57f64081) / `VARIANT` | Unchanged | Pending | Not called | O8 |
| [GetMissingMassParticipationFactors](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#ga7bae007519dd0144777b9874096b47b9) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#ga7bae007519dd0144777b9874096b47b9) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#ga7bae007519dd0144777b9874096b47b9) / `VARIANT` | Unchanged | Pending | Not called | O8 |
| [GetTimeHistoryIntegrationStepInfo](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#gaea1ad7d38067027dbe6c74a6152f2f5d) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#gaea1ad7d38067027dbe6c74a6152f2f5d) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#gaea1ad7d38067027dbe6c74a6152f2f5d) / `VARIANT` | Unchanged | Pending | Not called | O9 |
| [GetTimeHistoryResponseAtTime](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#ga646f263e1b9a53e4212f3c2cc5ed8acc) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#ga646f263e1b9a53e4212f3c2cc5ed8acc) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#ga646f263e1b9a53e4212f3c2cc5ed8acc) / `VARIANT` | Unchanged | Pending | Not called | O9 |
| [GetTimeHistoryResponse](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#ga2e7ec7357bfaef20ecd21bba24e06a57) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#ga2e7ec7357bfaef20ecd21bba24e06a57) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#ga2e7ec7357bfaef20ecd21bba24e06a57) / `VARIANT` | Unchanged | Pending | Not called | O9 |
| [GetTimeHistoryResponseMinMax](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#ga3206fd5f32a98d437bd0dab1e3a82bf4) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#ga3206fd5f32a98d437bd0dab1e3a82bf4) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#ga3206fd5f32a98d437bd0dab1e3a82bf4) / `VARIANT` | Unchanged | Pending | Not called | O9 |
| [GetMaxBeamStresses](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#gae0623051a17845003fd341a3ca0bb007) | Not in help/header | [long](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#gae0623051a17845003fd341a3ca0bb007) / `long` | Added help member | Pending | Not called | O10 |
| [GetStaticCheckResult](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___static_check.html#ga8343b8a41832142e342fb4b8750a6e38) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___static_check.html#ga8343b8a41832142e342fb4b8750a6e38) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___static_check.html#ga8343b8a41832142e342fb4b8750a6e38) / `VARIANT` | Unchanged | Pending | [Called](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L91) | O2 |
| [IsBucklingAnalysisResultsAvailable](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___buckling_rlt.html#ga65682c9aeb4eacc4a9ee03d9e21d166c) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___buckling_rlt.html#ga65682c9aeb4eacc4a9ee03d9e21d166c) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___buckling_rlt.html#ga65682c9aeb4eacc4a9ee03d9e21d166c) / `VARIANT` | Unchanged | Pending | [Called](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L54) | O5 |
| [GetNoOfBucklingFactors](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___buckling_rlt.html#ga61ea226383660ee7e9bc9cb687dfb20b) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___buckling_rlt.html#ga61ea226383660ee7e9bc9cb687dfb20b) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___buckling_rlt.html#ga61ea226383660ee7e9bc9cb687dfb20b) / `VARIANT` | Unchanged | Pending | [Called](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L923) | O5 |
| [GetBucklingFactor](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___buckling_rlt.html#gab1fae0d3b4a716ba9919da5d54f242c0) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___buckling_rlt.html#gab1fae0d3b4a716ba9919da5d54f242c0) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___buckling_rlt.html#gab1fae0d3b4a716ba9919da5d54f242c0) / `VARIANT` | Unchanged | Pending | [Called](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L931) | O5 |
| [GetBucklingModeDisplacementAtNode](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___buckling_rlt.html#ga4a1940a1616821e91e5cf3e42492d6f6) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___buckling_rlt.html#ga4a1940a1616821e91e5cf3e42492d6f6) | [VARIANT](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___buckling_rlt.html#ga4a1940a1616821e91e5cf3e42492d6f6) / `VARIANT` | Unchanged | Pending | [Called](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L945) | O5 |


“Root delegation only” means the current Root service calls that Output member. “Called” links to a direct call in the read-only main Output adapter. Neither label certifies native behavior on all versions. Every row still needs a typed Output API.

## 4. Version differences and policy boundaries

### 4.1 Member-end displacement output changes

[GetMemberEndDisplacements in 2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#gaf4f46574a9c80c6398e062781d131cf2) describes six components using force/moment labels, and its VBA example allocates six entries and reads six. [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#gaf4f46574a9c80c6398e062781d131cf2) and [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#gaf4f46574a9c80c6398e062781d131cf2) describe three translational X/Y/Z components, allocate three entries and read three. The native VARIANT declaration remains identical.

This is a material output-shape ambiguity. It is unsafe to read six from a three-value buffer or to pad rotations with zeros and present them as native data. Proposed handling:

- A shared MemberEndDisplacementResult contains translations and explicitly qualified optional rotations/component availability.
- A 2024 layout policy is a six-component candidate; the 2025/2026 candidate is three components.
- Confirm both candidate layouts on advertised builds, including axes, rotation interpretation and native element type. The 2024 force/moment terminology cannot be copied into displacement documentation.
- Keep unverified layouts unavailable with a diagnostic. Detecting shape after a call cannot justify passing an unsafe caller buffer beforehand.
- Do not probe alternate buffer layouts with undersized unmanaged storage.

This policy applies only to member-end displacements. Node, modal and buckling-mode displacement APIs have separate six-component contracts.

### 4.2 Three direction arguments change their documented representation

| Function | 2024 documentation | 2025 / 2026 documentation | Managed selector |
| --- | --- | --- | --- |
| [GetMinMaxBendingMoment](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#gaf63c90d9d6acf2007bd0dc194d59a813) | Numeric MY=5, MZ=6; example declares Double | String MY or MZ; example declares String | BendingMomentAxis |
| [GetMinMaxShearForce](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#gac5691dcf8af2b39c3ca8a1d6ed06a93a) | Numeric FY=2, FZ=3; example declares Double | String FY or FZ; example declares String | ShearForceAxis |
| [GetMaxSectionDisplacement](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#ga1ddbbc7a3b403db9d08040fff625326b) | Numeric global X=1, Y=2, Z=3; example declares Double | String X, Y or Z; example declares String | GlobalTranslationAxis |

The VARIANT declarations do not change. Use semantic managed enums and internal per-profile translation. Native numeric subtype also needs verification: the 2024 examples use Double, so boxing an enum as Int32 without checking coercion is insufficient. The 2024 shear example comment suggests direction 5, contradicting its own parameter table; that comment must not become an implementation constant.

These are documented policy candidates, not a claim that every 2024 binary rejects strings or later binaries reject numbers. Native fixtures must establish supported forms. Do not retry an engineering failure with another direction representation and silently classify it as a binding mismatch.

### 4.3 GetMaxBeamStresses is newly documented in 2025

[GetMaxBeamStresses](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#gae0623051a17845003fd341a3ca0bb007) is present in the 2025/2026 groups, generated header and installed IOSOutputUI metadata. It is absent from the reviewed 2024 group/header; no corresponding 2024 metadata was available.

Its native return is a 32-bit integer: **0 means success**, while documented errors include -3001 (member not found), -8002 (load case not found) and -9004 (beam-force results not loaded). Outputs are compressive stress, compressive corner, tensile stress and tensile corner. Corner zero denotes non-corner-section material; 1–4 identify section corners.

Recommend a separately acquired IOSBeamStressOutput capability with GetMaximumBeamStresses. Initially offer it only for verified 2025/2026 profiles with matching member metadata. Unknown/2024 profiles receive an unavailable or unverified capability result. Positive acquired-server verification could later extend support; help omission alone does not prove native absence.

The common OSOutputAdapter must not implement that optional interface, which would allow a cast to bypass gating. A separate internal implementation supplies the optional interface after validation. No change to OpenStaadWrapper is required.

### 4.4 Parameter clarification and formatting changes

GetPlateStressAtPoint changes its parameter-table term from facingNode to facingPoint in 2025. Its declaration already uses facingPoint in all years. This is a documentation correction; use one common service.

Five common functions have equivalent contracts after cleanup:

| Function | Difference | Implementation consequence |
| --- | --- | --- |
| GetBasePressures | Repeated return-values heading. | Shared Boolean policy. |
| GetPMemberEndForces | Explicit note marker in newer help. | Shared logic; retain physical-incidence/sign warning. |
| GetMinMaxAxialForce | Removed a stray character. | Shared logic. |
| GetResultantForceAlongLineForPlateList | Explicit note marker. | Shared logic after independent binding ambiguities are resolved. |
| GetResultantForceAlongLineForParametricSurface | Same note-marker change. | Same boundary. |

The other 49 common non-example contracts match after syntax-heading cleanup. Including the five formatting-only cases gives 54 common functions needing no year-specific implementation on available evidence. Forty-nine common example blocks changed from 2024 to 2025; that does not automatically mean 49 API changes. No normalized declaration, contract or example change was found between 2025 and 2026.

## 5. Cross-reference discrepancies requiring verification

These issues exist independently of year selection. Creating three entire adapters would not resolve them.

| Contract | Evidence | Required handling |
| --- | --- | --- |
| GetMissingMassParticipationFactors | Help/header describe it in every year; neither inspected 2025/2026 IOSOutputUI contains it. | Inspect the acquired native interface and confirm invocation metadata. Do not infer native absence from a stale interop DLL. |
| GetBucklingFactor | Help declares an output reference; both inspected interfaces expose the second Object argument by value. Main source passes it by value and leaves a testing TODO. | Establish a working native binding or mark unverified/unsupported. Never return initialized zero as extracted data. |
| Plate/surface cut resultants | Prototype uses loadIdList; prose names loadId and adds facingNode; surface prose also includes nplates absent from its declaration. Metadata marks all arguments Object by value, including outputs. | Verify actual signature, load-list layout, buffer ownership and shape. Do not add phantom arguments or guess ref behavior. |
| GetPlateCornerForces | Help leaves output description empty. Source specifies six global components and comments about prior equilibrium checks. | Source assertions are reference evidence, not verification performed here. Reproduce axes/sign/layout checks; avoid double rotation. |
| GetPlateStressAtPoint | 32-slot table mixes named stresses, resultants and aggregate categories; source exposes a mutable Raw array. | Preserve slot mapping and qualify dimensions per field. Copy storage and reject unqualified/unwritten output. |
| Principal stresses and angles | Help says float; VARIANT/Object& metadata do not establish Single versus Double. | Verify element type, angle units and eight-value versus six-plus-two layout. |
| Solid principal stresses | Help lists S1, S1, S2, repeating the first label. | Resolve ordering before labeling S1/S2/S3 or sorting values. |
| Absolute member translations | Name says absolute; prose repeats relative local displacement wording. | Compare APIs on a model with support movement/rigid translation. Preserve separate semantics. |
| Time-history extrema | Return table omits success; prose shows three DOFs while metadata enum has six. | Verify success and accepted DOFs independently; preserve unknown codes. |
| Output-unit queries | All three native class listings declare thirteen mutable VARIANT-reference parameters without detailed contracts; interop metadata exposes Object strUnit by value. | Resolve this additional binding discrepancy in O11. Do not assume result meaning or substitute wrapper input/base units. |

The supplemental native class listing has thirteen matching unit-query declarations in all three years, plus OnFinalRelease and SetStructure native implementation helpers. The latter are outside IOSOutputUI and excluded from managed porting. Class listings are fingerprinted separately from the ten group pages per year.

Vendor metadata and its two time-history enums match between 2025/2026. Both assemblies share an identity despite different binary hashes. The annex preserves separate hashes and signatures. Metadata supports binding hypotheses; it does not establish a working native invocation.

## 6. Existing implementation and reuse decisions

### 6.1 Current repository

- OpenStaadWrapper captures RawOutput, constructs the binding placeholder and supplies native version/unit snapshots. Refresh replaces bindings; disposal clears them.
- IOSOutput / OSOutputAdapter expose ComObject and inherited Wrapper, with no domain methods.
- [RootMemberForcesService](../../src/StaadPro.Interop/Services/Root/Analysis/RootMemberForcesService.cs#L14) obtains root.Output, checks availability, reads local member-end forces and validates status plus six finite Double values, including nonzero array lower bounds.
- Forces, MemberForces, IForces and unit/property-store primitives already exist. Root result classes provide ordered, identified status/data outcomes.

Reuse those primitives. Extract a minimal shared internal reader if it prevents actual duplication, preserving published Root returns and behavior. Do not create a second public force-vector class solely for Output.

### 6.2 Main-source functionality

The [private source interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs) and [adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs) provide:

| Area | Source functionality | Porting decision |
| --- | --- | --- |
| Availability and supports | Analysis/buckling availability, reactions, negated forces and static-check batches. | First useful queries; qualify native status and action/reaction identity. |
| Member forces | Local/global native selection, case/entity metadata and async convenience. | Reuse Root decoding and source semantics; do not derive axes from incomplete beta geometry. |
| Member offsets | Global member-face actions transferred to physical joint. | Preserve audit data; requires unported Property offsets or an approved explicit-offset request. |
| Steel design | Per-beam/batch ratios and full result hydration. | Reuse models/load lookup; fix status coercion and unchecked flags. |
| Plate center | Named stress/moment/Von Mises/principal results and aggregation. | Reuse models with qualified status and dimensional clarity. |
| Plate corner | Entity/ID hydration, explicit load family, sparse/Cartesian batches, keys/indexes and native checks. | Preserve useful overloads and deterministic ordering; replace out-return conveniences with named results. |
| Coordinates | Incidence/beta transforms, conceptual zero-beta axes and compatibility helpers. | Separate pure math from COM; label conceptual member ID zero and verify vertical-axis conventions. |
| Cuts and point stress | Cut integration and named 32-slot result. | Resolve prototype/metadata ambiguities, storage ownership and finite validation. |
| Buckling | Count, factor and displacement conveniences. | Fix scalar/vector and output-binding defects before claiming parity. |
| Async/parallel | Task.Run, Task.WhenAll and PLINQ; newer corner/cut paths use a local lock. | Rework execution ownership; a lock establishes neither apartment correctness nor cross-adapter coordination. |

The [annex](OpenSTAAD-Output-API-Coverage-and-Contract-Inventory.md#5-main-project-managed-reference-surface) inventories **all 86 overloads** and 24 private helpers. Thirty-three return Task; two expose public out Vector3D pairs. Four offset-aware/physical-joint overloads depend on the unported Property API.

### 6.3 Source adaptations required

1. Several support, center, design and buckling methods ignore native flags before consuming default buffers. Require operation-specific success and complete data.
2. GetBucklingFactor passes a candidate output by value, matching the questionable metadata. Initialized zero cannot establish extraction success.
3. GetBucklingModeDisplacementAtNode uses a scalar/double return, despite the six-value help contract. Return the complete mode data.
4. Steel design casts status to string and has a TODO describing unexpected numeric status. Preserve Unknown/raw status instead of treating all non-PASS values as a failed design.
5. GetBeamOffsetSpecification is absent from the current Property placeholder. Missing offset information cannot safely mean zero offset.
6. The source zero-beta helper fixes global Y as its reference. Verify Z-up, near-vertical members and beta conventions.
7. Point-stress extraction substitutes zero for null entries; its result retains the caller's mutable array. Reject unwritten data and own snapshots.
8. Read-only collection membership does not freeze mutable Plate/Node/LoadCase identities. Indexed identity can become stale.
9. UI warnings/null returns, Populate model mutations and thread-pool COM calls need explicit managed contracts.

Private Forces/plate models reference Shared and Units infrastructure. Adapt them to existing repository equivalents; do not add proprietary project references or import unrelated dependency trees. Sampling, contour rendering and export are useful later features, outside this native-function count.

## 7. Managed API design

### Results and overloads

Every new query returns a named class with IsSuccessful/semantic status, native code when supplied, operation identity and a payload qualified by successful status plus valid data. Statuses should distinguish Success, ResultsUnavailable, EntityNotFound, Unsupported, UnverifiedContract, NativeFailure and InvalidNativeData. Preserve unknown native representations; never expose an initialized buffer as a successful payload.

Include axes, reference point and verified units/normalization where relevant. A successful availability query can report Available=false; a successful count can return zero. Those observations are not generic failures.

Keep concrete results/requests under Models/Output, selectors under Enums/Output and decoding/services internal. Prefer classes; a small immutable composite key struct is justified for indexed identity. Avoid an unnecessary public generic result hierarchy.

The annex gives proposed managed signatures and return classes for all 60 names. Examples:

~~~csharp
MemberEndForcesResult GetMemberEndForces(
    int memberId, MemberEnd memberEnd, int loadCaseId,
    ResultCoordinateSystem coordinateSystem);

MemberForceExtremaResult GetMemberBendingMomentExtrema(
    int memberId, int loadCaseId, BendingMomentAxis axis);

PlateCutResultantResult GetPlateCutResultant(PlateCutQuery query);

BucklingModeDisplacementResult GetBucklingModeDisplacements(
    int bucklingModeNumber, int nodeId);
~~~

These are proposals, not callable APIs. **No new public ref/out or tuples.** Native references remain internal. Missing rotations in a three-component output do not mean zero rotations.

Useful overloads include validated IDs/existing entities, one versus multiple cases, sparse plate-corner requests and explicit cut/point-query objects. Materialize batches once, deduplicate by documented composite identity, enforce budgets and return owned ordered outcomes. Partial-result behavior must be explicit.

Use semantic selectors for axes, member end, DOF and response kind. Keep reserved time-history lCase=0 internal. Do not define a solid-corner enum before verifying its index convention.

### Compatibility and optional access

Output's empty domain surface permits these result conventions from its first implementation. Existing Root/wrapper/Geometry/Load/force-model signatures remain compatibility constraints. C# cannot overload on return type alone. Adding interface members also affects third-party implementers; document that release impact.

Runtime version cannot remove static interface/enum metadata from IntelliSense. Use common interfaces for established common operations and separately acquired optional interfaces where appropriate. Optional implementations remain internal; the common adapter must not implement them and permit a cast bypass.

Multiple steel-design-block results are documented as limited to AISC 360-16 in all three references. Gate by actual native availability, design/result context and member contracts, not only application year. Do not add throwing stubs to simulate coverage.

## 8. Internal architecture and polymorphic selection

Proposed organization retains one non-partial adapter:

~~~text
Adapters/Interfaces/IOSOutput.cs
Adapters/Interfaces/Output/             optional capability interfaces
Adapters/Models/OSOutputAdapter.cs       one adapter, not partial
Models/Output/                          requests and named results
Enums/Output/                           selectors and statuses
Services/Output/
    Binding/                            receiver/profile/capability evidence
    Contracts/                          small internal policies
    Queries/                            common extraction/composition
    Results/                            strict decoding
    Coordinates/                        pure transforms and moment transfer
~~~

These are proposed folders. Start with a few cohesive classes and split only where justified. Reuse Services/Interop readers and current unit primitives.

An internal contract policy translates semantic selectors and supplies a verified displacement layout. A shared 2025/2026 policy suffices for matching contracts; the 2024 candidate supplies only the differing rules. Common extraction remains shared. Optional beam stresses and unresolved bindings are capability decisions, not reasons to duplicate every query.

~~~mermaid
flowchart TD
    A["Acquired wrapper"] --> B["Capture RawOutput and native StaadVersion"]
    B --> C{"Binding and contract evidence sufficient?"}
    C -->|"No"| U["Unavailable or unverified capability"]
    C -->|"Yes"| D{"Verified profile"}
    D -->|"2024"| E["2024 direction and displacement policy"]
    D -->|"2025 or 2026"| F["Shared 2025/2026 policy"]
    D -->|"Unknown"| U
    E --> G["Shared Output services"]
    F --> G
    G --> H["Common IOSOutput operations"]
    G --> I{"Optional member and result context verified?"}
    I -->|"Yes"| J["Acquire separate optional interface"]
    I -->|"No"| U
~~~

Resolution belongs to the acquired receiver/version snapshot, not a process-global latest-installed setting. Existing Root binding infrastructure is a reuse candidate, but Output needs its own receiver/member contracts. Never substitute global active-object lookup.

Unknown/servicing builds must not inherit changed policies by guess. Record exact evidence and a compatibility policy. Common operations can be enabled where their required contracts are established. Missing members differ from unavailable results. Refresh invalidates capability snapshots; stale/disposed bindings fail clearly instead of attaching to another model.

## 9. Extraction, failure and concurrency

~~~mermaid
flowchart TD
    A["Managed request"] --> B["Snapshot and validate arguments and budget"]
    B --> C{"Contract and binding current?"}
    C -->|"No"| X["Qualified unsupported or stale-binding outcome"]
    C -->|"Yes"| D["Use verified COM execution owner"]
    D --> E{"Required result context available?"}
    E -->|"No"| Y["Results-unavailable outcome"]
    E -->|"Yes"| F["Allocate verified sentinel buffers and invoke"]
    F --> G["Interpret operation-specific native return"]
    G --> H{"Status qualifies output?"}
    H -->|"No"| N["Native failure with identity/code"]
    H -->|"Yes"| I["Check types, rank, bounds, size and finite values"]
    I --> J{"Complete payload?"}
    J -->|"No"| Z["Invalid-data diagnostic"]
    J -->|"Yes"| K["Copy data; attach verified units and axes"]
    K --> L["Requested pure managed composition"]
    L --> M["Named result or ordered batch outcomes"]
~~~

Empty batches and pure transformations can return without native calls, with availability explicitly unobserved. Availability queries return their observation without recursively requiring results. Document argument/binding exceptions versus returned operation failures rather than suppressing every exception.

| Edge case | Required behavior |
| --- | --- |
| Blank/model-less instance or no analysis | Return unavailable/not-ready observations. Never implicitly open a model or run analysis. |
| Existing results after model changes | Availability is not freshness proof. Coordinate extraction against edits and disclose observable limits. |
| Another active STAAD session | Use the acquired receiver only. |
| Deleted ID, invalid station or unrelated corner | Validate IDs, finite inputs and geometry; retain native not-found outcomes for races. |
| Empty/duplicate/large batches | Snapshot once, stable first-seen order, documented deduplication and allocation/query budget. Empty success does not certify native availability. |
| Native false, zero, negative or count | Apply the specific Boolean/zero-success/direct-data policy; preserve unknown codes. |
| Malformed successful output | Reject wrong element type/rank/length, unwritten sentinels, NaN/Infinity and inconsistent coupled buffers. Honor nonzero lower bounds. |
| Mutable arrays and identities | Return owned storage and stable index keys; do not call shared mutable buffers immutable. |
| Units changed mid-query | Capture verified result-unit context coherently. Wrapper base/input units do not establish every Output quantity's units. |
| Reversed incidence, axes or beta | Preserve member/end identity; request native coordinates where possible and verify transformations. |
| Offsets/reference points | Distinguish member face/physical joint and transfer moments about an identified point. Missing offset is not zero. |
| Plate/solid topology | Validate incidence, triangular repeated corners, coplanarity, nondegenerate cuts and verified solid-corner indexing. |
| Eigen/buckling results | Preserve normalization; normalized eigenvectors are not static physical displacement. |
| Multiple steel blocks | Verify actual design-code/result availability and preserve block identity only when supplied. |
| Busy/rejected RPC | Only bounded retries for established transient transport errors; no alternate-API retry of an engineering failure. |
| Cancellation/timeout | Honor before/between calls. A managed timeout cannot safely abort arbitrary blocking COM. |
| Partial batches | Preserve identity/outcome and explicit stop/continue rules; aggregate success requires every required subquery. |
| Refresh/dispose/reentrancy | Coordinate binding lifetime; avoid reentrant edits and final-release of shared RCWs as failure cleanup. |

Synchronous calls use the caller's established apartment or an explicitly owned session executor. A private Output lock cannot coordinate raw access and other adapters. Do not claim assembly-wide serialization unless all relevant paths participate.

Establish execution/marshaling ownership before adding the 33 reference Task overloads. Task.Run and PLINQ do not prove COM safety; cancellation cannot guarantee native interruption. Pure math can run in parallel after qualified extraction. .NET Core support remains a separate targeting/package decision; this review does not change the net481 target.

## 10. Implementation sequence

Each approved increment needs full XML, appropriate tests, updated coverage and explicit native-verification limits.

| Phase | Work | Completion condition |
| --- | --- | --- |
| O0 | Approve contracts and scope. | Agree status/data, optional access, unit policy, source adaptations and dependency deferrals. |
| O1 | Minimal binding/profile/decoder foundation. | Reuse native version/strict readers, per-wrapper evidence, no partial adapters or global switches. |
| O2 | Availability, supports, static checks and member-end forces. | Five names; reuse Root validation, identify payloads and verify units/axes/status. |
| O3 | Plate-center families. | Seven names; dimensions, top/bottom order, aggregate statuses and buffer types. |
| O4 | Plate corners, point stresses and cuts. | Four names plus useful sparse/batch conveniences; resolve layout/axes/prototype ambiguities. |
| O5 | Buckling and single-block steel. | Seven names; correct scalar/vector/output issues and unknown design status. |
| O6 | Remaining member/physical-member stations/extrema. | Eleven names; verify four profile candidates and physical/absolute-relative semantics. |
| O7 | Influence areas, base pressure and solids. | Six names; zero-success, ordered arrays, corner convention and principal ordering. |
| O8 | Modal, nonlinear and missing mass. | Seven names; index/count semantics, normalization and missing native-member metadata resolved. |
| O9 | Time history. | Four names; count/time consistency, reserved zero argument, DOF scope and success policy. |
| O10 | Optional beam stress and remaining steel/block queries. | Nine names; capability gates, AISC context, zero-based block indexing and complete design metadata. |
| O11 | Supplemental units and release validation. | Resolve thirteen extra metadata contracts; then verified tests/consumer/package and explicitly requested release. |

Unresolved O4 cuts or O5 buckling factor can remain unverified while independent qualified queries progress. A phase never authorizes guessed native transport. Four offset/physical-joint conveniences additionally need Property offsets or an approved explicit-offset request; they do not add native names to the O6 count.

### Native-name allocation

This partitions all 60 documented names, including the two currently delegated through Root:

| Workstream | Documented native names | Functions to review |
| --- | --- | --- |
| O2 | 5 | `AreResultsAvailable`, `GetNodeDisplacements`, `GetSupportReactions`, `GetMemberEndForces`, `GetStaticCheckResult` |
| O3 | 7 | `GetAllPlateCenterStressesAndMoments`, `GetPlateCenterNormalPrincipalStresses`, `GetAllPlateCenterForces`, `GetAllPlateCenterMoments`, `GetAllPlateCenterPrincipalStressesAndAngles`, `GetAllPlateCenterPrincipalStressesAndAnglesEx`, `GetPlateCenterVonMisesStresses` |
| O4 | 4 | `GetPlateCornerForces`, `GetResultantForceAlongLineForPlateList`, `GetResultantForceAlongLineForParametricSurface`, `GetPlateStressAtPoint` |
| O5 | 7 | `GetMemberDesignSectionName`, `GetMemberSteelDesignRatio`, `GetMemberSteelDesignResults`, `IsBucklingAnalysisResultsAvailable`, `GetNoOfBucklingFactors`, `GetBucklingFactor`, `GetBucklingModeDisplacementAtNode` |
| O6 | 11 | `GetMemberEndDisplacements`, `GetIntermediateMemberTransDisplacements`, `GetPMemberEndForces`, `GetMinMaxBendingMoment`, `GetMinMaxShearForce`, `GetMinMaxAxialForce`, `GetIntermediateMemberForcesAtDistance`, `GetPMemberIntermediateForcesAtDistance`, `GetMaxSectionDisplacement`, `GetIntermediateDeflectionAtDistance`, `GetIntermediateMemberAbsTransDisplacements` |
| O7 | 6 | `GetMatInfluenceAreas`, `GetBasePressures`, `GetAllSolidNormalStresses`, `GetAllSolidShearStresses`, `GetAllSolidPrincipalStresses`, `GetAllSolidVonMisesStresses` |
| O8 | 7 | `GetNLLoadStep`, `GetNLNodeDisplacements`, `GetNoOfModesExtracted`, `GetModeFrequency`, `GetModalDisplacementAtNode`, `GetModalMassParticipationFactors`, `GetMissingMassParticipationFactors` |
| O9 | 4 | `GetTimeHistoryIntegrationStepInfo`, `GetTimeHistoryResponseAtTime`, `GetTimeHistoryResponse`, `GetTimeHistoryResponseMinMax` |
| O10 | 9 | `IsMultipleMemberSteelDesignResultsAvailable`, `GetSteelDesignParameterBlockCount`, `GetSteelDesignParameterBlockNameByIndex`, `GetMultipleMemberSteelDesignRatio`, `GetMultipleMemberSteelDesignMaxRatio`, `GetMultipleMemberSteelDesignResults`, `GetMemberSteelDesignMinFailureRatio`, `GetMemberSteelDesignMaxFailureRatio`, `GetMaxBeamStresses` |


Counts describe native names, not managed overloads/models. All 86 source overloads are in the annex. Thirteen supplemental unit candidates belong to O11 discovery and are outside the 60-name partition.

Recommended first approved increment is O1/O2, delivering qualified availability, reactions, static checks and member-end forces with existing validation. Implement only the foundation needed for those functions, rather than creating a large empty framework.

## 11. XML documentation, tests and release gates

Every new interface/adapter method, request/result property and enum value needs IntelliSense documenting intent, descriptive parameters, valid ranges, units, axes, reference point, status interpretation, absent data, version/capability prerequisites, exceptions, execution ownership and examples. Keep ID/entity overloads consistent.

Examples inspect status before consuming payloads and acquire optional interfaces through the gateway. Analysis/model mutations require explicit guarded steps. Document actual verified meanings, especially stress versus force, moment per width, modal normalization and three-value translations.

| Layer | Required evidence |
| --- | --- |
| Inventory | 59/60/60 names, 59 common, 60 union, eleven Design-filed functions, all 86 source overloads and thirteen supplemental candidates. |
| Policies | Three direction translations including subtype; 6/3 displacement candidates; common reuse; unknown versions; optional cast bypass prevented. |
| Status | Boolean/zero-success/direct data/count, negative/unknown codes, unavailable observations and unknown steel status. |
| Buffers | Element types, rank/lower bounds, 3/5/6/8/32 lengths where applicable, split arrays, sentinel/finite checks and copied storage. |
| Identity/batches | Load family, triangle repetition, sparse/dense order, deduplication, budget overflow, partial failure and stable keys. |
| Engineering | Action/reaction signs, moment transfer, axes/beta/Y-up/Z-up, physical/member-face identity, extrema positions and dimensional conversions. |
| Lifetime/execution | Two versions/wrappers, host-version mismatch, refresh/dispose, stale capabilities, apartment/marshaling, reentrancy and honest cancellation. |
| Native fixtures | Each advertised build/result family, missing/stale results, invalid IDs, AISC/block availability, time-history/mode indexing and binding discrepancies. |
| Public API | No new public ref/out/tuple outputs, interface/adapter XML parity, owned results, compiled examples and existing Root compatibility. |
| Distribution | Correct frameworks/Windows requirements, DLL/XML/dependencies, clean PackageReference consumer and updated README/release notes for the actual package. |

No library tests were run for this documentation-only comparison. The analysis utility compiled only itself and parsed source; reflection-only inspection did not execute Bentley code. Earlier offline test totals do not validate these proposed Output APIs.

## 12. Review outcome

Use shared internal Output services and narrow verified policies. Reuse source models/helpers where they add established functionality, adapt public reference outputs to named results and defer unresolved native contracts explicitly. Preserve the wrapper and existing public adapter behavior.

This report does not implement Output, change targets, rebuild NuGet or establish native compatibility. A future package needs approved implementations, professional README/release notes and verified XML. The published preview's contents remain unchanged.
