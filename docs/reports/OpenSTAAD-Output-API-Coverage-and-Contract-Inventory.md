# OpenSTAAD Output API coverage and contract inventory

**Review date:** 22 October 2026<br>
**Repository baseline:** `3c67959163450d1b2c150bbc8f25a5c1f99dc30c`<br>
**Status:** Reference inventory and proposed contracts; no Output domain implementation was added.

Read the [cross-version architecture and plan](OpenSTAAD-Output-Multi-Version-Architecture-and-Plan.md) for conclusions, policy boundaries and release gates. The [comparison evidence](data/OpenSTAAD-Output-2024-2026-Comparison.json) contains exact declarations, complete parameter-table descriptions, return tables, fingerprints and managed-source mappings.

## 1. Scope and counting rules

The Analysis Results tree contributes 48 functions in 2024 and 49 in 2025/2026. Eleven additional functions belong to OSOutputUI but are filed under Steel Design Results. This inventory includes those functions, giving 59 / 60 / 60 native names. The linked Chinese Steel section was inspected and contributes zero OSOutputUI functions; other native owners in that section are outside this Output review. Ten pages per year were traversed; nine contain Output functions.

Current IOSOutput and OSOutputAdapter expose zero domain methods; ComObject and the inherited Wrapper binding are real properties. RootMemberForcesService separately calls AreResultsAvailable and GetMemberEndForces. Thus no typed Output method is complete, while two of the 60 native names already have an indirect production dispatch elsewhere in the assembly.

The read-only main IOSOutput has 86 method overloads, and its OSOutputAdapter implements all 86, plus 24 private methods. These map to 17 distinct native targets; 43 of the 60 documented native names have no direct target in that reference adapter. Main-source parity, typed adapter coverage and native-name coverage are separate counts.

All 60 entries below remain pending as typed Output APIs. Proposed classes and signatures are design proposals. Their names may be refined at API review; they do not exist merely because they appear here. Every proposed query result carries qualified status, operation identity and the listed payload. No proposed signature exposes ref/out or tuples.

## 2. Complete cross-version matrix

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

**Comparison:** Unchanged means normalized declaration and non-example contract match; formatting-only entries preserve equivalent meanings; policy candidates require validation of changed input or output conventions. “Not in help/header” is an evidence boundary, not proof about every installed native server.

## 3. Native contracts and proposed managed mappings

Native VARIANT does not determine its boxed CLR type or array element type. Native long is a Windows 32-bit integer. const VARIANT references remain input values; mutable references/pointers do not establish logical direction without the parameter contract. A missing or differently named parameter-table entry is recorded explicitly rather than matched by position.

Descriptions below retain the functional parameter details and native codes. The original help links remain authoritative for tables, examples and engineering context. The evidence file retains each year separately, including unchanged entries.

<a id="output-areresultsavailable"></a>

### 1. AreResultsAvailable

**Section:** Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___ana_rlt.html#gab326f7578cd6d53aa4f4400f74640491) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___ana_rlt.html#gab326f7578cd6d53aa4f4400f74640491) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___ana_rlt.html#gab326f7578cd6d53aa4f4400f74640491)<br>
**Comparison:** Unchanged<br>
**Current implementation:** [Root AreResultsAvailable](../../src/StaadPro.Interop/Services/Root/Analysis/RootMemberForcesService.cs#L22); no typed Output method.<br>
**Main reference:** [AreResultsAvailable native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L49), [GetMemberEndForces native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L220)<br>
**Workstream:** O2

```cpp
VARIANT OSOutputUI::AreResultsAvailable(void)
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return description:** Boolean (TRUE/FALSE) whether analysis completed or not.

**Installed 2025 / 2026 interop signature:**

`System.Object AreResultsAvailable()`

**Proposed managed entry:** `GetAnalysisResultAvailability()`<br>
**Proposed return class:** `AnalysisResultAvailabilityResult`<br>
**Result and validation contract:** False means unavailable, not a failed analysis diagnosis; distinguish unavailable from a COM failure.

<a id="output-getmemberdesignsectionname"></a>

### 2. GetMemberDesignSectionName

**Section:** Design: Steel Design Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gaa4e3a6905c36de9f5e0edb39ab022217) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gaa4e3a6905c36de9f5e0edb39ab022217) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gaa4e3a6905c36de9f5e0edb39ab022217)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O5

```cpp
VARIANT OSOutputUI::GetMemberDesignSectionName(const VARIANT FAR&varnBeamNo)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `varnBeamNo` | `const VARIANT FAR&` | [in] | Member number ID. |


**Native return codes / values:**

| Value | Meaning |
| --- | --- |
| `Returns` | design section name for the specified member. Returns empty string if not found. |

**Installed 2025 / 2026 interop signature:**

`System.Object GetMemberDesignSectionName(System.Object)`

**Proposed managed entry:** `GetMemberDesignSectionName(int memberId)`<br>
**Proposed return class:** `MemberDesignSectionResult`<br>
**Result and validation contract:** Direct string; empty means not found per help. Preserve unavailable/not-found separately from successful nonempty section.

<a id="output-ismultiplemembersteeldesignresultsavailable"></a>

### 3. IsMultipleMemberSteelDesignResultsAvailable

**Section:** Design: Steel Design Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#ga23663ecf75659aa873be867cea01f42c) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#ga23663ecf75659aa873be867cea01f42c) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#ga23663ecf75659aa873be867cea01f42c)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O10

```cpp
VARIANT OSOutputUI::IsMultipleMemberSteelDesignResultsAvailable()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return description:** Returns TRUE (for Boolean variable, for Long variable, return value is 1) if result extraction from multiple steel design block is possible (i.e. AISC 360-16 code is used). Else the return value will be FALSE (0 for Long variable).

**Installed 2025 / 2026 interop signature:**

`System.Object IsMultipleMemberSteelDesignResultsAvailable()`

**Proposed managed entry:** `GetMultipleSteelDesignResultAvailability()`<br>
**Proposed return class:** `SteelDesignAvailabilityResult`<br>
**Result and validation contract:** AISC 360-16 limitation in all reviewed references. Native availability, design code and result context govern access in addition to application version.

<a id="output-getsteeldesignparameterblockcount"></a>

### 4. GetSteelDesignParameterBlockCount

**Section:** Design: Steel Design Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#ga65c592e2e4b54a57c0321bbb38e29939) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#ga65c592e2e4b54a57c0321bbb38e29939) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#ga65c592e2e4b54a57c0321bbb38e29939)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O10

```cpp
VARIANT OSOutputUI::GetSteelDesignParameterBlockCount()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return description:** Returns the count of steel design parameter blocks.

**Installed 2025 / 2026 interop signature:**

`System.Object GetSteelDesignParameterBlockCount()`

**Proposed managed entry:** `GetSteelDesignParameterBlockCount()`<br>
**Proposed return class:** `SteelDesignBlockCountResult`<br>
**Result and validation contract:** AISC 360-16; direct count, zero may be valid. Snapshot with names to detect races.

<a id="output-getsteeldesignparameterblocknamebyindex"></a>

### 5. GetSteelDesignParameterBlockNameByIndex

**Section:** Design: Steel Design Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gae4d12cb96249a53e02ac209d4ec28bb7) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gae4d12cb96249a53e02ac209d4ec28bb7) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gae4d12cb96249a53e02ac209d4ec28bb7)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O10

```cpp
VARIANT OSOutputUI::GetSteelDesignParameterBlockNameByIndex(const VARIANT FAR&vnIdx,VARIANT FAR&vszParamBlkName)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `vnIdx` | `const VARIANT FAR&` | [in] | The index value of steel design parameter block list. Note, the index is zero based. |
| `vszParamBlkName` | `VARIANT FAR&` | [out] | Steel design parameter block name |


**Native return description:** Returns true if successful, false otherwise.

**Installed 2025 / 2026 interop signature:**

`System.Object GetSteelDesignParameterBlockNameByIndex(System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetSteelDesignParameterBlockName(int parameterBlockIndex)`<br>
**Proposed return class:** `SteelDesignBlockNameResult`<br>
**Result and validation contract:** Explicitly zero-based index. Return status and block name; prefer a bounded GetSteelDesignParameterBlocks convenience.

<a id="output-getmultiplemembersteeldesignratio"></a>

### 6. GetMultipleMemberSteelDesignRatio

**Section:** Design: Steel Design Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gae9107fd34396f30c46e3e34660194db4) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gae9107fd34396f30c46e3e34660194db4) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gae9107fd34396f30c46e3e34660194db4)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O10

```cpp
VARIANT OSOutputUI::GetMultipleMemberSteelDesignRatio(const VARIANT FAR&vszParamBlkName,const VARIANT FAR&vnBeamNo,VARIANT FAR&vfCriticalRatio)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `vszParamBlkName` | `const VARIANT FAR&` | [in] | Steel design parameter block name. |
| `vnBeamNo` | `const VARIANT FAR&` | [in] | Beam number ID. |
| `vfCriticalRatio` | `VARIANT FAR&` | [out] | Returns the critical steel design ratio. |


**Native return description:** Returns true if ratio for the specified member is obtained successfully otherwise false.

**Installed 2025 / 2026 interop signature:**

`System.Object GetMultipleMemberSteelDesignRatio(System.Object, System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetMemberSteelDesignRatio(string parameterBlockName, int memberId)`<br>
**Proposed return class:** `SteelDesignRatioResult`<br>
**Result and validation contract:** AISC 360-16, named block/member identity; finite qualified utilization ratio, not a pass/fail guess.

<a id="output-getmultiplemembersteeldesignmaxratio"></a>

### 7. GetMultipleMemberSteelDesignMaxRatio

**Section:** Design: Steel Design Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gab44e212393084c79c2da98b206b9c612) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gab44e212393084c79c2da98b206b9c612) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gab44e212393084c79c2da98b206b9c612)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O10

```cpp
VARIANT OSOutputUI::GetMultipleMemberSteelDesignMaxRatio(const VARIANT FAR&vnBeamNo,VARIANT FAR&vfCriticalRatio)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `vnBeamNo` | `const VARIANT FAR&` | [in] | Beam number ID. |
| `vfCriticalRatio` | `VARIANT FAR&` | [out] | Returns the maximum critical steel design ratio. |


**Native return description:** Returns true if ratio for the specified member is obtained successfully otherwise false.

**Installed 2025 / 2026 interop signature:**

`System.Object GetMultipleMemberSteelDesignMaxRatio(System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetMaximumMemberSteelDesignRatio(int memberId)`<br>
**Proposed return class:** `SteelDesignRatioResult`<br>
**Result and validation contract:** Maximum over parameter blocks; do not invent a governing block identity absent from native output.

<a id="output-getmultiplemembersteeldesignresults"></a>

### 8. GetMultipleMemberSteelDesignResults

**Section:** Design: Steel Design Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gaa2dd5aae686f3943747519b19cc2dbcd) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gaa2dd5aae686f3943747519b19cc2dbcd) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gaa2dd5aae686f3943747519b19cc2dbcd)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O10

```cpp
VARIANT OSOutputUI::GetMultipleMemberSteelDesignResults(const VARIANT FAR&vszParamBlkName,const VARIANT FAR&vnBeamNo,VARIANT FAR&vszDesignCode,VARIANT FAR&vszDesignStatus,VARIANT FAR&vfCriticalRatio,VARIANT FAR&vfAllowableRatio,VARIANT FAR&vnCriticalLoadCase,VARIANT FAR&vszCriticalClause,VARIANT FAR&vszDesignSection)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `vszParamBlkName` | `const VARIANT FAR&` | [in] | Steel design parameter block name. See OSOutputUI::GetSteelDesignParameterBlockNameByIndex |
| `vnBeamNo` | `const VARIANT FAR&` | [in] | Member number ID. |
| `vszDesignCode` | `VARIANT FAR&` | [out] | The design code name |
| `vszDesignStatus` | `VARIANT FAR&` | [out] | The design status (PASS or FAIL will be returned) |
| `vfCriticalRatio` | `VARIANT FAR&` | [out] | The design utilization ratio. |
| `vfAllowableRatio` | `VARIANT FAR&` | [out] | The allowable design utilization ratio. |
| `vnCriticalLoadCase` | `VARIANT FAR&` | [out] | The critical design load case number. |
| `vszCriticalClause` | `VARIANT FAR&` | [out] | The critical design clause. |
| `vszDesignSection` | `VARIANT FAR&` | [out] | The design section name. |


**Native return description:** Returns true if results for the specified member is obtained successfully otherwise false.

**Installed 2025 / 2026 interop signature:**

`System.Object GetMultipleMemberSteelDesignResults(System.Object, System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Proposed managed entry:** `GetMemberSteelDesignResults(string parameterBlockName, int memberId)`<br>
**Proposed return class:** `MemberSteelDesignQueryResult`<br>
**Result and validation contract:** Return design code/status, critical/allowable ratio, governing case, clause and section. Keep fields not supplied by this API explicitly unavailable.

<a id="output-getmembersteeldesignratio"></a>

### 9. GetMemberSteelDesignRatio

**Section:** Design: Steel Design Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gafcd9cbfb77d9a4df423fb92993d542c4) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gafcd9cbfb77d9a4df423fb92993d542c4) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gafcd9cbfb77d9a4df423fb92993d542c4)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** [GetMemberSteelUtilizationRatios native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L178), [PopulateMemberSteelUtilizationRatios native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L190), [GetMemberSteelUtilizationRatio native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L199), [GetMemberSteelUtilizationRatioAsync native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1010)<br>
**Workstream:** O5

```cpp
VARIANT OSOutputUI::GetMemberSteelDesignRatio(const VARIANT FAR&vnBeamNo,VARIANT FAR&vfCriticalRatio)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `vnBeamNo` | `const VARIANT FAR&` | [in] | Beam number ID. |
| `vfCriticalRatio` | `VARIANT FAR&` | [out] | Returns the critical steel design ratio. Returns -999 if analysis is performed but the member is not designed. Returns -1 if analysis is not performed. |


**Native return description:** Returns true if ratio for the specified member is obtained successfully otherwise false.

**Installed 2025 / 2026 interop signature:**

`System.Object GetMemberSteelDesignRatio(System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetMemberSteelUtilizationRatio(int memberId)`<br>
**Proposed return class:** `SteelDesignRatioResult`<br>
**Result and validation contract:** Reuse source ratio conveniences after strict success/numeric checks; copy batch data without mutating Beam unless explicitly requested.

<a id="output-getmembersteeldesignresults"></a>

### 10. GetMemberSteelDesignResults

**Section:** Design: Steel Design Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#gaf0f6f888d59db3ae5f4896025fc44dd2) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#gaf0f6f888d59db3ae5f4896025fc44dd2) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#gaf0f6f888d59db3ae5f4896025fc44dd2)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** [GetSteelDesignResultsForBeam native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L155)<br>
**Workstream:** O5

```cpp
VARIANT OSOutputUI::GetMemberSteelDesignResults(const VARIANT FAR&vnBeamNo,VARIANT FAR&vszDesignCode,VARIANT FAR&vszDesignStatus,VARIANT FAR&vfCriticalRatio,VARIANT FAR&vfAllowableRatio,VARIANT FAR&vnCriticalLoadCase,VARIANT FAR&vfCriticalSection,VARIANT FAR&vszCriticalClause,VARIANT FAR&vszDesignSection,VARIANT FAR&vfaDesignForces,VARIANT FAR&vfKLByR)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `vnBeamNo` | `const VARIANT FAR&` | [in] | Id of the member for which design results should be retrieved. |
| `vszDesignCode` | `VARIANT FAR&` | [out] | The design code name |
| `vszDesignStatus` | `VARIANT FAR&` | [out] | The design status (PASS or FAIL will be returned) |
| `vfCriticalRatio` | `VARIANT FAR&` | [out] | The design utilization ratio. |
| `vfAllowableRatio` | `VARIANT FAR&` | [out] | The allowable design utilization ratio. |
| `vnCriticalLoadCase` | `VARIANT FAR&` | [out] | The critical design load case number. |
| `vfCriticalSection` | `VARIANT FAR&` | [out] | The critical section location from the start of member in current base unit. |
| `vszCriticalClause` | `VARIANT FAR&` | [out] | The critical design clause. |
| `vszDesignSection` | `VARIANT FAR&` | [out] | The design section name. |
| `vfaDesignForces` | `VARIANT FAR&` | [out] | Array of size 3 and type double to hold critical section forces. Returns FX, MY and MZ values at 0, 1, & 2 index respectively in current base units. |
| `vfKLByR` | `VARIANT FAR&` | [out] | KL/R ratio of the specified member |


**Native return description:** true if design results can be obtained successfully otherwise false.

**Installed 2025 / 2026 interop signature:**

`System.Object GetMemberSteelDesignResults(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Proposed managed entry:** `GetMemberSteelDesignResults(int memberId)`<br>
**Proposed return class:** `MemberSteelDesignQueryResult`<br>
**Result and validation contract:** Ten outputs include three design forces, critical section and slenderness. Source TODO reports inconsistent status variants; preserve raw status and Unknown instead of treating all non-PASS values as Failed.

<a id="output-getmembersteeldesignminfailureratio"></a>

### 11. GetMemberSteelDesignMinFailureRatio

**Section:** Design: Steel Design Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#ga33d973a3a58d9bb7dbb1a551dbe112c8) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#ga33d973a3a58d9bb7dbb1a551dbe112c8) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#ga33d973a3a58d9bb7dbb1a551dbe112c8)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O10

```cpp
VARIANT OSOutputUI::GetMemberSteelDesignMinFailureRatio(VARIANT FAR&pdRatio)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `pdRatio` | `VARIANT FAR&` | [out] | The minimum failure ratio. |


**Native return description:** true if minimum ratio can be obtained successfully otherwise false.

**Installed 2025 / 2026 interop signature:**

`System.Object GetMemberSteelDesignMinFailureRatio(System.Object ByRef)`

**Proposed managed entry:** `GetSteelDesignFailureRatioBounds()`<br>
**Proposed return class:** `SteelDesignFailureRatioBoundsResult`<br>
**Result and validation contract:** Review individual calls and result semantics before combining; preserve per-call statuses and do not assume both succeeded. Native query does not accept a member ID.

<a id="output-getmembersteeldesignmaxfailureratio"></a>

### 12. GetMemberSteelDesignMaxFailureRatio

**Section:** Design: Steel Design Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html#ga5bedb4627e488a567f4786374d07cea9) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html#ga5bedb4627e488a567f4786374d07cea9) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html#ga5bedb4627e488a567f4786374d07cea9)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O10

```cpp
VARIANT OSOutputUI::GetMemberSteelDesignMaxFailureRatio(VARIANT FAR&pdRatio)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `pdRatio` | `VARIANT FAR&` | [out] | The maximum failure ratio. |


**Native return description:** true if maximum ratio can be obtained successfully otherwise false.

**Installed 2025 / 2026 interop signature:**

`System.Object GetMemberSteelDesignMaxFailureRatio(System.Object ByRef)`

**Proposed managed entry:** `GetSteelDesignFailureRatioBounds()`<br>
**Proposed return class:** `SteelDesignFailureRatioBoundsResult`<br>
**Result and validation contract:** Review individual calls and result semantics before combining; preserve per-call statuses and do not assume both succeeded. Native query does not accept a member ID.

<a id="output-getnodedisplacements"></a>

### 13. GetNodeDisplacements

**Section:** Result: Nodes/Joints/Supports Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___nod_rlt.html#gad7bb8f56f4f2096c0240d644e99479dc) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___nod_rlt.html#gad7bb8f56f4f2096c0240d644e99479dc) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___nod_rlt.html#gad7bb8f56f4f2096c0240d644e99479dc)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O2

```cpp
VARIANT OSOutputUI::GetNodeDisplacements(const VARIANT FAR&nNodeNo,const VARIANT FAR&nLC,VARIANT FAR&pdDisps)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nNodeNo` | `const VARIANT FAR&` | [in] | Node number ID. |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `pdDisps` | `VARIANT FAR&` | [out] | VARIANT array with nodal translational displacements in X, Y and Z directions, rotation about X, Y and Z directions, respectively. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetNodeDisplacements(System.Object, System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetNodeDisplacements(int nodeId, int loadCaseId)`<br>
**Proposed return class:** `NodalDisplacementResult`<br>
**Result and validation contract:** Six translations/rotations in native order; identify axes and units; preserve node/case identity.

<a id="output-getsupportreactions"></a>

### 14. GetSupportReactions

**Section:** Result: Nodes/Joints/Supports Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___nod_rlt.html#ga8bfa8a82d929856113b7920b030989d1) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___nod_rlt.html#ga8bfa8a82d929856113b7920b030989d1) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___nod_rlt.html#ga8bfa8a82d929856113b7920b030989d1)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** [GetSupportReactions native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L71), [GetSupportReactionsAsync native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L969)<br>
**Workstream:** O2

```cpp
VARIANT OSOutputUI::GetSupportReactions(const VARIANT FAR&nNodeNo,const VARIANT FAR&nLC,VARIANT FAR&pdReactions)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nNodeNo` | `const VARIANT FAR&` | [in] | Node number ID. |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `pdReactions` | `VARIANT FAR&` | [out] | Support Reaction in GLOBAL direction:[ FX, FY, FZ, MX, MY, MZ]. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetSupportReactions(System.Object, System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetSupportReactions(int nodeId, int loadCaseId)`<br>
**Proposed return class:** `SupportReactionResult`<br>
**Result and validation contract:** Six force/moment values. A separately named applied-support-force convenience negates a copy, preserving action/reaction semantics.

<a id="output-getmatinfluenceareas"></a>

### 15. GetMatInfluenceAreas

**Section:** Result: Nodes/Joints/Supports Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___nod_rlt.html#ga8a408a6468ecf006e35de27075584856) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___nod_rlt.html#ga8a408a6468ecf006e35de27075584856) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___nod_rlt.html#ga8a408a6468ecf006e35de27075584856)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O7

```cpp
VARIANT OSOutputUI::GetMatInfluenceAreas(VARIANT FAR&nNodes,VARIANT FAR&varYZAreas,VARIANT FAR&varZXAreas,VARIANT FAR&varXYAreas)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nNodes` | `VARIANT FAR&` | [in] | Array containing the support node numbers. |
| `varYZAreas` | `VARIANT FAR&` | [out] | Array containing the influence areas in YZ plase ordered per the node nos. in nodeNos array |
| `varZXAreas` | `VARIANT FAR&` | [out] | Array containing the influence areas in ZX plase ordered per the node nos. in nodeNos array |
| `varXYAreas` | `VARIANT FAR&` | [out] | Array containing the influence areas in XY plase ordered per the node nos. in nodeNos array |


**Native return codes / values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-107` | Array of double expected. |
| `-108` | Array size is smaller than expected. |

**Installed 2025 / 2026 interop signature:**

`System.Object GetMatInfluenceAreas(System.Object, System.Object, System.Object, System.Object)`

**Proposed managed entry:** `GetMatInfluenceAreas(IEnumerable<int> supportNodeIds)`<br>
**Proposed return class:** `MatInfluenceAreasResult`<br>
**Result and validation contract:** Snapshot node IDs once; preserve correspondence of YZ/ZX/XY arrays; zero is native success; validate ELASTIC MAT context.

<a id="output-getbasepressures"></a>

### 16. GetBasePressures

**Section:** Result: Nodes/Joints/Supports Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___nod_rlt.html#ga6b71fd3346b2e89a6c47bfd7945f0d99) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___nod_rlt.html#ga6b71fd3346b2e89a6c47bfd7945f0d99) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___nod_rlt.html#ga6b71fd3346b2e89a6c47bfd7945f0d99)<br>
**Comparison:** Formatting or typo only<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O7

```cpp
VARIANT OSOutputUI::GetBasePressures(const VARIANT FAR&varLC,VARIANT FAR&nNodes,VARIANT FAR&varYZPress,VARIANT FAR&varZXPress,VARIANT FAR&varXYPress)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `varLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `nNodes` | `VARIANT FAR&` | [in] | Array containing the support node numbers. |
| `varYZPress` | `VARIANT FAR&` | [out] | Array containing the base pressures in X direction per load case reference ID |
| `varZXPress` | `VARIANT FAR&` | [out] | Array containing the base pressures in Y direction per load case reference ID |
| `varXYPress` | `VARIANT FAR&` | [out] | Array containing the base pressures in Z direction per load case reference ID |


**Native return codes / values:**

| Value | Meaning |
| --- | --- |
| `TRUE` |  |
| `FALSE` |  |

**Installed 2025 / 2026 interop signature:**

`System.Object GetBasePressures(System.Object, System.Object, System.Object, System.Object, System.Object)`

**Proposed managed entry:** `GetBasePressures(int loadCaseId, IEnumerable<int> supportNodeIds)`<br>
**Proposed return class:** `BasePressuresResult`<br>
**Result and validation contract:** YZ/ZX/XY pressure arrays map to global X/Y/Z; preserve node order and Boolean status; BASE PRESSURE prerequisite.

<a id="output-getmemberenddisplacements"></a>

### 17. GetMemberEndDisplacements

**Section:** Result: Members Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#gaf4f46574a9c80c6398e062781d131cf2) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#gaf4f46574a9c80c6398e062781d131cf2) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#gaf4f46574a9c80c6398e062781d131cf2)<br>
**Comparison:** Version policy candidate<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O6

```cpp
VARIANT OSOutputUI::GetMemberEndDisplacements(const VARIANT FAR&nMemberNo,const VARIANT FAR&nEnd,const VARIANT FAR&nLC,VARIANT FAR&pdDisps)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nMemberNo` | `const VARIANT FAR&` | [in] | Member number ID. (Type: Long) |
| `nEnd` | `const VARIANT FAR&` | [in] | Member End (0 for starting and 1 for ending). (Type: Long) |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. (Type: Long) |
| `pdDisps` | `VARIANT FAR&` | [out] | Displacements of Member End in terms of X, Y, Z (in order). (Type: Double Array) |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**2024 parameter description differences:**

| Parameter | 2024 description | 2025 / 2026 description |
| --- | --- | --- |
| `nMemberNo` | Member number ID. | Member number ID. (Type: Long) |
| `nEnd` | Member End (0 for starting and 1 for ending). | Member End (0 for starting and 1 for ending). (Type: Long) |
| `nLC` | Load Case reference ID. | Load Case reference ID. (Type: Long) |
| `pdDisps` | Displacements of Member End in terms of FX, FY, FZ, MX, MY and MZ (in order). | Displacements of Member End in terms of X, Y, Z (in order). (Type: Double Array) |

**Installed 2025 / 2026 interop signature:**

`System.Object GetMemberEndDisplacements(System.Object, System.Object, System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetMemberEndDisplacements(int memberId, MemberEnd memberEnd, int loadCaseId)`<br>
**Proposed return class:** `MemberEndDisplacementResult`<br>
**Result and validation contract:** 2024 example allocates six values; 2025/2026 allocate three. Confirm native shape per build before enabling a profile. Never manufacture rotations.

The 2024 example declares eDisp(0 To 5) and reads six entries; 2025/2026 declare eDisp(0 To 2) and read three. This is stronger evidence than a wording-only correction, but still requires native shape verification.

<a id="output-getintermediatemembertransdisplacements"></a>

### 18. GetIntermediateMemberTransDisplacements

**Section:** Result: Members Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#ga0ac61ab06bab4b6188a03445edc8bacd) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#ga0ac61ab06bab4b6188a03445edc8bacd) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#ga0ac61ab06bab4b6188a03445edc8bacd)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O6

```cpp
VARIANT OSOutputUI::GetIntermediateMemberTransDisplacements(const VARIANT FAR&nMemberNo,const VARIANT FAR&dDistance,const VARIANT FAR&nLC,VARIANT FAR&pdDisps)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nMemberNo` | `const VARIANT FAR&` | [in] | Member number ID. |
| `dDistance` | `const VARIANT FAR&` | [in] | Distance from starting end in terms of member length. |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `pdDisps` | `VARIANT FAR&` | [out] | Relative displacements at specified section in terms of LOCAL X, Y, Z coordinates (in order). |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetIntermediateMemberTransDisplacements(System.Object, System.Object, System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetRelativeMemberDisplacement(int memberId, double distanceFromStart, int loadCaseId)`<br>
**Proposed return class:** `MemberTranslationResult`<br>
**Result and validation contract:** Three local translations at a validated station; do not confuse relative displacement with absolute displacement.

<a id="output-getmemberendforces"></a>

### 19. GetMemberEndForces

**Section:** Result: Members Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#ga9ee15e2a2869bfb68049fcc03d062329) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#ga9ee15e2a2869bfb68049fcc03d062329) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#ga9ee15e2a2869bfb68049fcc03d062329)<br>
**Comparison:** Unchanged<br>
**Current implementation:** [Root GetMemberEndForces](../../src/StaadPro.Interop/Services/Root/Analysis/RootMemberForcesService.cs#L36); no typed Output method.<br>
**Main reference:** [GetMemberEndForces native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L233), [GetMemberEndForcesAsync native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1038)<br>
**Workstream:** O2

```cpp
VARIANT OSOutputUI::GetMemberEndForces(const VARIANT FAR&nMemberNo,const VARIANT FAR&nEnd,const VARIANT FAR&nLC,VARIANT FAR&pdForces,const VARIANT FAR&LocalOrGlobal)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nMemberNo` | `const VARIANT FAR&` | [in] | Member number ID. |
| `nEnd` | `const VARIANT FAR&` | [in] | Member End (0 for starting and 1 for ending). |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `pdForces` | `VARIANT FAR&` | [out] | Force of Member End in LOCAL coordinates in terms of FX, FY, FZ, MX, MY and MZ (in order). |
| `LocalOrGlobal` | `const VARIANT FAR&` | [in] | Local Or Global direction. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetMemberEndForces(System.Object, System.Object, System.Object, System.Object ByRef, System.Object)`

**Proposed managed entry:** `GetMemberEndForces(int memberId, MemberEnd memberEnd, int loadCaseId, ResultCoordinateSystem coordinateSystem)`<br>
**Proposed return class:** `MemberEndForcesResult`<br>
**Result and validation contract:** Six values. Reuse Root decoding rules; request native local/global axes explicitly, preserving member end and reference point.

<a id="output-getpmemberendforces"></a>

### 20. GetPMemberEndForces

**Section:** Result: Members Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#gafd324bfa929f74ec38f73d4d43463060) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#gafd324bfa929f74ec38f73d4d43463060) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#gafd324bfa929f74ec38f73d4d43463060)<br>
**Comparison:** Formatting or typo only<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O6

```cpp
VARIANT OSOutputUI::GetPMemberEndForces(const VARIANT FAR&nMemberNo,const VARIANT FAR&nEnd,const VARIANT FAR&nLC,VARIANT FAR&pdForces,const VARIANT FAR&LocalOrGlobal)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nMemberNo` | `const VARIANT FAR&` | [in] | Member number ID. (Type : Long/Int) |
| `nEnd` | `const VARIANT FAR&` | [in] | Member End (0 for starting and 1 for ending). (Type : Long/Int) |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. (Type : Long/Int) |
| `pdForces` | `VARIANT FAR&` | [out] | Force of Member End in LOCAL coordinates in terms of FX, FY, FZ, MX, MY and MZ (in order). (Type : Double) |
| `LocalOrGlobal` | `const VARIANT FAR&` | [in] | Results returned in either local or global axes. 0= Local, 1= Global direction. (Type : Long/Int) |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetPMemberEndForces(System.Object, System.Object, System.Object, System.Object ByRef, System.Object)`

**Proposed managed entry:** `GetPhysicalMemberEndForces(int physicalMemberId, MemberEnd memberEnd, int loadCaseId, ResultCoordinateSystem coordinateSystem)`<br>
**Proposed return class:** `PhysicalMemberEndForcesResult`<br>
**Result and validation contract:** Physical member incidence reordering can change signs. Validate physical identity and native start/end; do not treat its ID as an analytical beam ID.

<a id="output-getminmaxbendingmoment"></a>

### 21. GetMinMaxBendingMoment

**Section:** Result: Members Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#gaf63c90d9d6acf2007bd0dc194d59a813) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#gaf63c90d9d6acf2007bd0dc194d59a813) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#gaf63c90d9d6acf2007bd0dc194d59a813)<br>
**Comparison:** Version policy candidate<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O6

```cpp
VARIANT OSOutputUI::GetMinMaxBendingMoment(const VARIANT FAR&nMemberNo,const VARIANT FAR&strDir,const VARIANT FAR&nLoadNo,VARIANT FAR&dMin,VARIANT FAR&dMinPos,VARIANT FAR&dMax,VARIANT FAR&dMaxPos)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nMemberNo` | `const VARIANT FAR&` | [in] | Member number ID. (Long) |
| `strDir` | `const VARIANT FAR&` | [in] | Bending direction in LOCAL coordinate: MY or MZ. (String) |
| `nLoadNo` | `const VARIANT FAR&` | [in] | Load Case reference ID. (Long) |
| `dMin` | `VARIANT FAR&` | [out] | Minimum bending moment. (Double) |
| `dMinPos` | `VARIANT FAR&` | [out] | The location along the length of the member where the minimum bending moment is located. (Double) |
| `dMax` | `VARIANT FAR&` | [out] | Maximum bending moment. (Double) |
| `dMaxPos` | `VARIANT FAR&` | [out] | The location along the length of the member where the maximum bending moment is located. (Double) |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**2024 parameter description differences:**

| Parameter | 2024 description | 2025 / 2026 description |
| --- | --- | --- |
| `nMemberNo` | Member number ID. | Member number ID. (Long) |
| `strDir` | Bending direction in LOCAL coordinate: MY = 5, MZ = 6; | Bending direction in LOCAL coordinate: MY or MZ. (String) |
| `nLoadNo` | Load Case reference ID. | Load Case reference ID. (Long) |
| `dMin` | Minimum bending moment. | Minimum bending moment. (Double) |
| `dMinPos` | The location along the length of the member where the minimum bending moment is located. | The location along the length of the member where the minimum bending moment is located. (Double) |
| `dMax` | Maximum bending moment. | Maximum bending moment. (Double) |
| `dMaxPos` | The location along the length of the member where the maximum bending moment is located. | The location along the length of the member where the maximum bending moment is located. (Double) |

**Installed 2025 / 2026 interop signature:**

`System.Object GetMinMaxBendingMoment(System.Object, System.Object, System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Proposed managed entry:** `GetMemberBendingMomentExtrema(int memberId, int loadCaseId, BendingMomentAxis axis)`<br>
**Proposed return class:** `MemberForceExtremaResult`<br>
**Result and validation contract:** Profile candidate maps My/Mz to 2024 numeric 5/6 versus 2025/2026 strings MY/MZ. Confirm VARIANT numeric subtype live; preserve extrema and both positions.

<a id="output-getminmaxshearforce"></a>

### 22. GetMinMaxShearForce

**Section:** Result: Members Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#gac5691dcf8af2b39c3ca8a1d6ed06a93a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#gac5691dcf8af2b39c3ca8a1d6ed06a93a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#gac5691dcf8af2b39c3ca8a1d6ed06a93a)<br>
**Comparison:** Version policy candidate<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O6

```cpp
VARIANT OSOutputUI::GetMinMaxShearForce(const VARIANT FAR&nMemberNo,const VARIANT FAR&strDir,const VARIANT FAR&nLoadNo,VARIANT FAR&dMin,VARIANT FAR&dMinPos,VARIANT FAR&dMax,VARIANT FAR&dMaxPos)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nMemberNo` | `const VARIANT FAR&` | [in] | Member number ID. (Long) |
| `strDir` | `const VARIANT FAR&` | [in] | Bending direction in LOCAL coordinate: FY or FZ (String) |
| `nLoadNo` | `const VARIANT FAR&` | [in] | Load Case reference ID. (Long) |
| `dMin` | `VARIANT FAR&` | [out] | Minimum shear force. (Double) |
| `dMinPos` | `VARIANT FAR&` | [out] | The location along the length of the member where the minimum shear force is located. (Double) |
| `dMax` | `VARIANT FAR&` | [out] | Maximum shear force. (Double) |
| `dMaxPos` | `VARIANT FAR&` | [out] | The location along the length of the member where the maximum shear force is located. (Double) |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**2024 parameter description differences:**

| Parameter | 2024 description | 2025 / 2026 description |
| --- | --- | --- |
| `nMemberNo` | Member number ID. | Member number ID. (Long) |
| `strDir` | Bending direction in LOCAL coordinate: FY = 2, FZ = 3; | Bending direction in LOCAL coordinate: FY or FZ (String) |
| `nLoadNo` | Load Case reference ID. | Load Case reference ID. (Long) |
| `dMin` | Minimum shear force. | Minimum shear force. (Double) |
| `dMinPos` | The location along the length of the member where the minimum shear force is located. | The location along the length of the member where the minimum shear force is located. (Double) |
| `dMax` | Maximum shear force. | Maximum shear force. (Double) |
| `dMaxPos` | The location along the length of the member where the maximum shear force t is located. | The location along the length of the member where the maximum shear force is located. (Double) |

**Installed 2025 / 2026 interop signature:**

`System.Object GetMinMaxShearForce(System.Object, System.Object, System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Proposed managed entry:** `GetMemberShearForceExtrema(int memberId, int loadCaseId, ShearForceAxis axis)`<br>
**Proposed return class:** `MemberForceExtremaResult`<br>
**Result and validation contract:** Profile candidate maps Fy/Fz to 2024 numeric 2/3 versus 2025/2026 strings FY/FZ. 2024 example comments incorrectly suggest direction 5; follow verified contract, not that comment.

<a id="output-getminmaxaxialforce"></a>

### 23. GetMinMaxAxialForce

**Section:** Result: Members Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#ga072e86e3cb9e6c606510480f01ce76a9) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#ga072e86e3cb9e6c606510480f01ce76a9) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#ga072e86e3cb9e6c606510480f01ce76a9)<br>
**Comparison:** Formatting or typo only<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O6

```cpp
VARIANT OSOutputUI::GetMinMaxAxialForce(const VARIANT FAR&nMemberNo,const VARIANT FAR&nLoadNo,VARIANT FAR&dMin,VARIANT FAR&dMinPos,VARIANT FAR&dMax,VARIANT FAR&dMaxPos)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nMemberNo` | `const VARIANT FAR&` | [in] | Member number ID. |
| `nLoadNo` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `dMin` | `VARIANT FAR&` | [out] | Minimum axial force. |
| `dMinPos` | `VARIANT FAR&` | [out] | The location along the length of the member where the minimum axial force is located. |
| `dMax` | `VARIANT FAR&` | [out] | Maximum axial force. |
| `dMaxPos` | `VARIANT FAR&` | [out] | The location along the length of the member where the maximum axial force is located. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetMinMaxAxialForce(System.Object, System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Proposed managed entry:** `GetMemberAxialForceExtrema(int memberId, int loadCaseId)`<br>
**Proposed return class:** `MemberForceExtremaResult`<br>
**Result and validation contract:** Signed minimum/maximum and positions, not absolute extrema. Formatting/typo change needs no separate implementation.

<a id="output-getintermediatememberforcesatdistance"></a>

### 24. GetIntermediateMemberForcesAtDistance

**Section:** Result: Members Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#ga914b3980dd567b9042f94fca42a7e641) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#ga914b3980dd567b9042f94fca42a7e641) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#ga914b3980dd567b9042f94fca42a7e641)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O6

```cpp
VARIANT OSOutputUI::GetIntermediateMemberForcesAtDistance(const VARIANT FAR&nMemberNo,const VARIANT FAR&dDistance,const VARIANT FAR&nLC,VARIANT FAR&pdForces)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nMemberNo` | `const VARIANT FAR&` | [in] | Member number ID. |
| `dDistance` | `const VARIANT FAR&` | [in] | Distance from the starting end of the member. |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `pdForces` | `VARIANT FAR&` | [out] | Array of Section axial force, Shear force in LOCAL Y & Z direction, Torsion and Bending moment in Local MY & MZ direction |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetIntermediateMemberForcesAtDistance(System.Object, System.Object, System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetMemberForcesAtDistance(int memberId, double distanceFromStart, int loadCaseId)`<br>
**Proposed return class:** `MemberSectionForcesResult`<br>
**Result and validation contract:** Six local force/moment values; distance measured from the analytical start in native length units; validate against member length.

<a id="output-getpmemberintermediateforcesatdistance"></a>

### 25. GetPMemberIntermediateForcesAtDistance

**Section:** Result: Members Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#ga43d4b974d3b4f05d28156edbf7a390f6) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#ga43d4b974d3b4f05d28156edbf7a390f6) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#ga43d4b974d3b4f05d28156edbf7a390f6)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O6

```cpp
VARIANT OSOutputUI::GetPMemberIntermediateForcesAtDistance(const VARIANT FAR&nMemberNo,const VARIANT FAR&dDistance,const VARIANT FAR&nLC,VARIANT FAR&pdForces)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nMemberNo` | `const VARIANT FAR&` | [in] | Physical Member number ID. (Type : Long/Int) |
| `dDistance` | `const VARIANT FAR&` | [in] | Distance from the starting end of the member. (Type : Double) |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. (Type : Long/Int) |
| `pdForces` | `VARIANT FAR&` | [out] | 6 item array of Section axial force, Shear force in LOCAL Y & Z direction, Torsion and Bending moment in Local MY & MZ direction. (Type : Double) |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetPMemberIntermediateForcesAtDistance(System.Object, System.Object, System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetPhysicalMemberForcesAtDistance(int physicalMemberId, double distanceFromStart, int loadCaseId)`<br>
**Proposed return class:** `PhysicalMemberSectionForcesResult`<br>
**Result and validation contract:** Physical start and total length require physical geometry; preserve analytical-versus-physical identity.

<a id="output-getmaxsectiondisplacement"></a>

### 26. GetMaxSectionDisplacement

**Section:** Result: Members Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#ga1ddbbc7a3b403db9d08040fff625326b) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#ga1ddbbc7a3b403db9d08040fff625326b) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#ga1ddbbc7a3b403db9d08040fff625326b)<br>
**Comparison:** Version policy candidate<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O6

```cpp
VARIANT OSOutputUI::GetMaxSectionDisplacement(const VARIANT FAR&nMemberNo,const VARIANT FAR&strDir,const VARIANT FAR&nLoadNo,VARIANT FAR&dMax,VARIANT FAR&dMaxPos)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nMemberNo` | `const VARIANT FAR&` | [in] | Member number ID. (Long) |
| `strDir` | `const VARIANT FAR&` | [in] | Direction in GLOBAL: X, Y or Z (String) |
| `nLoadNo` | `const VARIANT FAR&` | [in] | Load Case reference ID. (Long) |
| `dMax` | `VARIANT FAR&` | [out] | Maximum section displacement in specified direction. (Double) |
| `dMaxPos` | `VARIANT FAR&` | [out] | The location along the length of the member where the maximum section displacement is located. (Double) |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**2024 parameter description differences:**

| Parameter | 2024 description | 2025 / 2026 description |
| --- | --- | --- |
| `nMemberNo` | Member number ID. | Member number ID. (Long) |
| `strDir` | Direction in GLOBAL: X = 1, Y = 2, Z = 3. | Direction in GLOBAL: X, Y or Z (String) |
| `nLoadNo` | Load Case reference ID. | Load Case reference ID. (Long) |
| `dMax` | Maximum section displacement in specified direction. | Maximum section displacement in specified direction. (Double) |
| `dMaxPos` | The location along the length of the member where the maximum section displacement is located. | The location along the length of the member where the maximum section displacement is located. (Double) |

**Installed 2025 / 2026 interop signature:**

`System.Object GetMaxSectionDisplacement(System.Object, System.Object, System.Object, System.Object ByRef, System.Object ByRef)`

**Proposed managed entry:** `GetMaximumMemberDisplacement(int memberId, int loadCaseId, GlobalTranslationAxis axis)`<br>
**Proposed return class:** `MemberDisplacementExtremumResult`<br>
**Result and validation contract:** 2024 numeric 1/2/3 versus 2025/2026 X/Y/Z strings. Confirm maximum convention and position; do not silently turn it into maximum absolute.

<a id="output-getintermediatedeflectionatdistance"></a>

### 27. GetIntermediateDeflectionAtDistance

**Section:** Result: Members Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#ga8adea9e89760a8344ca297b111408b56) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#ga8adea9e89760a8344ca297b111408b56) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#ga8adea9e89760a8344ca297b111408b56)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O6

```cpp
VARIANT OSOutputUI::GetIntermediateDeflectionAtDistance(const VARIANT FAR&nMemberNo,const VARIANT FAR&dDistance,const VARIANT FAR&nLC,VARIANT FAR&dY,VARIANT FAR&dZ)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nMemberNo` | `const VARIANT FAR&` | [in] | Member number ID. |
| `dDistance` | `const VARIANT FAR&` | [in] | Distance from the starting end of the member. |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `dY` | `VARIANT FAR&` | [out] | Displacement in Y direction. |
| `dZ` | `VARIANT FAR&` | [out] | Displacement in Z direction. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetIntermediateDeflectionAtDistance(System.Object, System.Object, System.Object, System.Object ByRef, System.Object ByRef)`

**Proposed managed entry:** `GetMemberDeflectionAtDistance(int memberId, double distanceFromStart, int loadCaseId)`<br>
**Proposed return class:** `MemberDeflectionResult`<br>
**Result and validation contract:** Return the two qualified Y/Z outputs; confirm axes and absolute/relative convention before documenting them as such.

<a id="output-getintermediatememberabstransdisplacements"></a>

### 28. GetIntermediateMemberAbsTransDisplacements

**Section:** Result: Members Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html#ga06e324e0eed5e1b970331c6f113d6f34) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html#ga06e324e0eed5e1b970331c6f113d6f34) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html#ga06e324e0eed5e1b970331c6f113d6f34)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O6

```cpp
VARIANT OSOutputUI::GetIntermediateMemberAbsTransDisplacements(const VARIANT FAR&nMemberNo,const VARIANT FAR&dDistance,const VARIANT FAR&nLC,VARIANT FAR&pdDisps)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nMemberNo` | `const VARIANT FAR&` | [in] | Member number ID. |
| `dDistance` | `const VARIANT FAR&` | [in] | Distance from starting end in terms of member length. |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `pdDisps` | `VARIANT FAR&` | [out] | Relative displacements at specified section in terms of LOCAL X, Y, Z coordinates (in order). |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetIntermediateMemberAbsTransDisplacements(System.Object, System.Object, System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetAbsoluteMemberDisplacement(int memberId, double distanceFromStart, int loadCaseId)`<br>
**Proposed return class:** `MemberTranslationResult`<br>
**Result and validation contract:** Name says absolute, prose repeats relative. Resolve via paired native fixtures; share transport only, not unverified semantics.

<a id="output-getallplatecenterstressesandmoments"></a>

### 29. GetAllPlateCenterStressesAndMoments

**Section:** Result: Plates Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#ga461a2ec82c9fb789f92cfb7f705a3e88) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#ga461a2ec82c9fb789f92cfb7f705a3e88) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#ga461a2ec82c9fb789f92cfb7f705a3e88)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O3

```cpp
VARIANT OSOutputUI::GetAllPlateCenterStressesAndMoments(const VARIANT FAR&nPlateNo,const VARIANT FAR&nLC,VARIANT FAR&pdForces)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nPlateNo` | `const VARIANT FAR&` | [in] | Plate number ID. |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `pdForces` | `VARIANT FAR&` | [out] | VARIANT array [ SQX , SQY , MX , MY , MXY , SX , SY , SXY ]: Variable Load Type SQX Shear stress on the local X face in the Z direction SQY Shear stress on the local Y face in the Z direction MX Moment per unit width about the local X face MY Moment per unit width about the local Y face MXY Torsional Moment per unit width in the local X-Y plane SX Axial stress in the local X direction SY Axial stress in the local Y direction SXY Shear stress in the local XY plane For additional information, please refer to Section: "Sign Convention of Plate Element Stresses and Moments" and Section 5.42 of the Technical Reference manual. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetAllPlateCenterStressesAndMoments(System.Object, System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetPlateCenterStressesAndMoments(int plateId, int loadCaseId)`<br>
**Proposed return class:** `PlateCenterStressAndMomentResult`<br>
**Result and validation contract:** Eight SQX,SQY,MX,MY,MXY,SX,SY,SXY values mix stress with moment per unit width; keep physical dimensions separate.

<a id="output-getplatecenternormalprincipalstresses"></a>

### 30. GetPlateCenterNormalPrincipalStresses

**Section:** Result: Plates Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#gae2958f7723d482e818d88e51d320880f) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#gae2958f7723d482e818d88e51d320880f) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#gae2958f7723d482e818d88e51d320880f)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** [GetPlatePrincipalStresses native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L895), [GetPlatePrincipalStressesAsync native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1336)<br>
**Workstream:** O3

```cpp
VARIANT OSOutputUI::GetPlateCenterNormalPrincipalStresses(const VARIANT FAR&nPlateNo,const VARIANT FAR&nLC,VARIANT FAR&pdSMAXTop,VARIANT FAR&pdSMINTop,VARIANT FAR&pdSMAXBottom,VARIANT FAR&pdSMINBottom)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nPlateNo` | `const VARIANT FAR&` | [in] | Plate number ID. |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `pdSMAXTop` | `VARIANT FAR&` | [out] | Maximum in-plane Principal stress at top surface of the plate ( SMAXT ). |
| `pdSMINTop` | `VARIANT FAR&` | [out] | Minimum in-plane Principal stress at top surface of the plate ( SMINT ). |
| `pdSMAXBottom` | `VARIANT FAR&` | [out] | Maximum in-plane Principal stress at top bottom of the plate ( SMAXB ). |
| `pdSMINBottom` | `VARIANT FAR&` | [out] | Minimum in-plane Principal stress at top bottom of the plate ( SMINB ). For additional information, please refer to Section: "Sign Convention of Plate Element Stresses and Moments" and Section 5.42 of the Technical Reference manual. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetPlateCenterNormalPrincipalStresses(System.Object, System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Proposed managed entry:** `GetPlateCenterPrincipalStresses(int plateId, int loadCaseId)`<br>
**Proposed return class:** `PlatePrincipalStressResult`<br>
**Result and validation contract:** Top maximum/minimum and bottom maximum/minimum; reuse source model meaning while adding qualified status and immutable snapshots.

<a id="output-getallplatecenterforces"></a>

### 31. GetAllPlateCenterForces

**Section:** Result: Plates Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#gad14d57e4e0404457454a3ea1394a8537) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#gad14d57e4e0404457454a3ea1394a8537) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#gad14d57e4e0404457454a3ea1394a8537)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** [GetPlateCenterForces native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L865), [GetPlateCenterForcesAsync native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1306)<br>
**Workstream:** O3

```cpp
VARIANT OSOutputUI::GetAllPlateCenterForces(const VARIANT FAR&nPlateNo,const VARIANT FAR&nLC,VARIANT FAR&pdForces)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nPlateNo` | `const VARIANT FAR&` | [in] | Plate number ID. |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `pdForces` | `VARIANT FAR&` | [out] | VARIANT array [ SQX , SQY , SX , SY , SXY ]: Variable Load Type SQX Shear stress on the local X face in the Z direction SQY Shear stress on the local Y face in the Z direction SX Axial stress in the local X direction SY Axial stress in the local Y direction SXY Shear stress in the local XY plane For additional information, please refer to Section: "Sign Convention of Plate Element Stresses and Moments" and Section 5.42 of the Technical Reference manual. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetAllPlateCenterForces(System.Object, System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetPlateCenterMembraneAndShearStresses(int plateId, int loadCaseId)`<br>
**Proposed return class:** `PlateCenterStressResult`<br>
**Result and validation contract:** Native name says forces but five outputs SQX,SQY,SX,SY,SXY are stresses. Retain source convenience only with explicit units/meaning.

<a id="output-getallplatecentermoments"></a>

### 32. GetAllPlateCenterMoments

**Section:** Result: Plates Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#ga911d707d71f6114ca6587ee315e168fa) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#ga911d707d71f6114ca6587ee315e168fa) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#ga911d707d71f6114ca6587ee315e168fa)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** [GetPlateCenterMoments native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L873), [GetPlateCenterMomentsAsync native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1314)<br>
**Workstream:** O3

```cpp
VARIANT OSOutputUI::GetAllPlateCenterMoments(const VARIANT FAR&nPlateNo,const VARIANT FAR&nLC,VARIANT FAR&pdForces)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nPlateNo` | `const VARIANT FAR&` | [in] | Plate number ID. |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `pdForces` | `VARIANT FAR&` | [out] | VARIANT array [ MX , MY , MXY ]: Variable Load Type MX Moment per unit width about the local X face MY Moment per unit width about the local Y face MXY Torsional Moment per unit width in the local X-Y plane For additional information, please refer to Section: "Sign Convention of Plate Element Stresses and Moments" and Section 5.42 of the Technical Reference manual. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetAllPlateCenterMoments(System.Object, System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetPlateCenterMoments(int plateId, int loadCaseId)`<br>
**Proposed return class:** `PlateCenterMomentResult`<br>
**Result and validation contract:** Three MX,MY,MXY moments per unit width; distinguish these from a six-component concentrated moment vector.

<a id="output-getallplatecenterprincipalstressesandangles"></a>

### 33. GetAllPlateCenterPrincipalStressesAndAngles

**Section:** Result: Plates Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#gac13e9962fbf82d34625f618241fc733c) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#gac13e9962fbf82d34625f618241fc733c) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#gac13e9962fbf82d34625f618241fc733c)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O3

```cpp
VARIANT OSOutputUI::GetAllPlateCenterPrincipalStressesAndAngles(const VARIANT FAR&nPlateNo,const VARIANT FAR&nLC,VARIANT FAR&pdStresses)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nPlateNo` | `const VARIANT FAR&` | [in] | Plate number ID. |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `pdStresses` | `VARIANT FAR&` | [out] | Array with 8 float values. Variable Description pdStresses [0] Top-Maximum in-plane Principal stress pdStresses [1] Top-Minimum in-plane Principal stress pdStresses [2] Top-Maximum in-plane Shear stress pdStresses [3] Bottom-Maximum in-plane Principal stress pdStresses [4] Bottom-Minimum in-plane Principal stress pdStresses [5] Bottom-Maximum in-plane Shear stress pdStresses [6] Top-Angle which determines direction of maximum principal stress with respect to local X axis pdStresses [7] Bottom-Angle which determines direction of maximum principal stress with respect to local X axis For additional information, please refer to Section: "Sign Convention of Plate Element Stresses and Moments" and Section 5.42 of the Technical Reference manual. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetAllPlateCenterPrincipalStressesAndAngles(System.Object, System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetPlateCenterPrincipalStressesAndAngles(int plateId, int loadCaseId)`<br>
**Proposed return class:** `PlatePrincipalStressAndAngleResult`<br>
**Result and validation contract:** Eight values: six top/bottom principal/shear stresses and two angles. Help says float; verify Single/Double SAFEARRAY element type and angle units.

<a id="output-getallplatecenterprincipalstressesandanglesex"></a>

### 34. GetAllPlateCenterPrincipalStressesAndAnglesEx

**Section:** Result: Plates Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#ga7a893aa342562cc268da3d1a8a1ea49c) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#ga7a893aa342562cc268da3d1a8a1ea49c) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#ga7a893aa342562cc268da3d1a8a1ea49c)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O3

```cpp
VARIANT OSOutputUI::GetAllPlateCenterPrincipalStressesAndAnglesEx(const VARIANT FAR&nPlateNo,const VARIANT FAR&nLC,VARIANT FAR&pdStresses,VARIANT FAR&pdAngles)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nPlateNo` | `const VARIANT FAR&` | [in] | Plate number ID. |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `pdStresses` | `VARIANT FAR&` | [out] | Array with 6 float values. |
| `pdAngles` | `VARIANT FAR&` | [out] | Array with 2 float values. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetAllPlateCenterPrincipalStressesAndAnglesEx(System.Object, System.Object, System.Object ByRef, System.Object ByRef)`

**Proposed managed entry:** `GetPlateCenterPrincipalStressesAndAngles(PlatePrincipalStressQuery query)`<br>
**Proposed return class:** `PlatePrincipalStressAndAngleResult`<br>
**Result and validation contract:** Separate six-stress/two-angle buffers; explicit request can select separate-buffer transport. Do not retry an engineering failure through another native API.

<a id="output-getplatecentervonmisesstresses"></a>

### 35. GetPlateCenterVonMisesStresses

**Section:** Result: Plates Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#ga2ee9f974e72eec15760b7e6da620d7c8) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#ga2ee9f974e72eec15760b7e6da620d7c8) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#ga2ee9f974e72eec15760b7e6da620d7c8)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** [GetPlateCenterVonMises native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L883), [GetPlateCenterVonMisesAsync native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1324)<br>
**Workstream:** O3

```cpp
VARIANT OSOutputUI::GetPlateCenterVonMisesStresses(const VARIANT FAR&nPlateNo,const VARIANT FAR&nLC,VARIANT FAR&pdVONT,VARIANT FAR&pdVONB)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nPlateNo` | `const VARIANT FAR&` | [in] | Plate number ID. |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `pdVONT` | `VARIANT FAR&` | [out] | Von Mises stress on the top surface of the plate |
| `pdVONB` | `VARIANT FAR&` | [out] | Von Mises stress on the bottom surface of the plate |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetPlateCenterVonMisesStresses(System.Object, System.Object, System.Object ByRef, System.Object ByRef)`

**Proposed managed entry:** `GetPlateCenterVonMisesStresses(int plateId, int loadCaseId)`<br>
**Proposed return class:** `PlateVonMisesStressResult`<br>
**Result and validation contract:** Two qualified top/bottom scalar outputs; preserve plate/case identity and stress units.

<a id="output-getplatecornerforces"></a>

### 36. GetPlateCornerForces

**Section:** Result: Plates Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#ga68bc8a450a29bc91630da63201e05870) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#ga68bc8a450a29bc91630da63201e05870) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#ga68bc8a450a29bc91630da63201e05870)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** [GetPlateCornerForces native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L378)<br>
**Workstream:** O4

```cpp
VARIANT OSOutputUI::GetPlateCornerForces(const VARIANT FAR&nPlateNo,const VARIANT FAR&nCornerNode,const VARIANT FAR&nLC,VARIANT FAR&pdForces)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nPlateNo` | `const VARIANT FAR&` | [in] | Plate number ID. |
| `nCornerNode` | `const VARIANT FAR&` | [in] | Corner Node No. |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `pdForces` | `VARIANT FAR&` | [out] | Description not supplied under this declaration name. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetPlateCornerForces(System.Object, System.Object, System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetPlateCornerForces(int plateId, int cornerNodeId, int loadCaseId)`<br>
**Proposed return class:** `PlateCornerForcesQueryResult`<br>
**Result and validation contract:** Reuse source hydration/request/index models. Help omits output layout/axes; source claims six global values and prior checks, which require new reproducible fixtures. Avoid double rotation.

<a id="output-getresultantforcealonglineforplatelist"></a>

### 37. GetResultantForceAlongLineForPlateList

**Section:** Result: Plates Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#ga16b671df915dfa2ca6691372f0c827be) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#ga16b671df915dfa2ca6691372f0c827be) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#ga16b671df915dfa2ca6691372f0c827be)<br>
**Comparison:** Formatting or typo only<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** [GetResultantForceAlongLineForPlateList native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L808)<br>
**Workstream:** O4

```cpp
VARIANT OSOutputUI::GetResultantForceAlongLineForPlateList(VARIANT FAR&plateList,const VARIANT FAR&nplates,VARIANT FAR&loadIdList,VARIANT FAR&startNode,VARIANT FAR&endNode,const VARIANT FAR&isTransformForceToGlobal,const VARIANT FAR&firstNode,const VARIANT FAR&secondNode,const VARIANT FAR&thirdNode,VARIANT FAR&resultantForces)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `plateList` | `VARIANT FAR&` | [in] | List of plates IDs. a) All plates in model, b) plates through which the cut line crosses (both would work but 'a' is computationally expensive) |
| `nplates` | `const VARIANT FAR&` | [in] | No of plates in plateList |
| `loadIdList` | `VARIANT FAR&` | Not named in parameter table | Description not supplied under this declaration name. |
| `startNode` | `VARIANT FAR&` | [in] | x, y, z values of the start node at indexes 0, 1 and 2. API always expects an array size of 3. |
| `endNode` | `VARIANT FAR&` | [in] | x, y, z values of the end node at indexes 0, 1 and 2. API always expects an array size of 3. |
| `isTransformForceToGlobal` | `const VARIANT FAR&` | [in] | 1: return force in Global System 0: return forces in local system of cut line |
| `firstNode` | `const VARIANT FAR&` | [in] | Node no representing the origin point for building the surface axis system |
| `secondNode` | `const VARIANT FAR&` | [in] | Node no representing the second point for building the surface axis system |
| `thirdNode` | `const VARIANT FAR&` | [in] | Node no representing the third point for building the surface axis system |
| `resultantForces` | `VARIANT FAR&` | [out] | Fx, Fy, Fz, Mx, My, Mz at array indexes 0, 1, 2, 3, 4, 5 |


**Additional or differently named parameter-table entries:**

| Table name | Direction | Description |
| --- | --- | --- |
| `loadId` | [in] | the load case for plate analysis |
| `facingNode` | [in] | x, y, z values of the facing node at indexes 0, 1 and 2. API always expects an array size of 3. Definition of facingNode: It is the node which sits on the tip of a vector which is orthogonal to the vector startNode -> endNode and lies in the same plane as that of the plates through which the cut line passes. |

**Native return description:** Boolean(TRUE / FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetResultantForceAlongLineForPlateList(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Proposed managed entry:** `GetPlateCutResultant(PlateCutQuery query)`<br>
**Proposed return class:** `PlateCutResultantResult`<br>
**Result and validation contract:** Prototype has loadIdList and no facingNode, contrary to parameter table; vendor metadata marks buffers by value. Gate transport until verified. Nonzero cut, coplanar geometry, stable IDs and -1/-1/-1 default axes.

<a id="output-getresultantforcealonglineforparametricsurface"></a>

### 38. GetResultantForceAlongLineForParametricSurface

**Section:** Result: Plates Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#ga2d8565301564943488f7604ce1b44e8b) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#ga2d8565301564943488f7604ce1b44e8b) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#ga2d8565301564943488f7604ce1b44e8b)<br>
**Comparison:** Formatting or typo only<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O4

```cpp
VARIANT OSOutputUI::GetResultantForceAlongLineForParametricSurface(const VARIANT FAR&parametricSurfaceName,VARIANT FAR&loadIdList,VARIANT FAR&startNode,VARIANT FAR&endNode,const VARIANT FAR&isTransformForceToGlobal,const VARIANT FAR&firstNode,const VARIANT FAR&secondNode,const VARIANT FAR&thirdNode,VARIANT FAR&resultantForces)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `parametricSurfaceName` | `const VARIANT FAR&` | [in] | Name of the parametric surface |
| `loadIdList` | `VARIANT FAR&` | Not named in parameter table | Description not supplied under this declaration name. |
| `startNode` | `VARIANT FAR&` | [in] | x, y, z values of the start node at indexes 0, 1 and 2. API always expects an array size of 3. |
| `endNode` | `VARIANT FAR&` | [in] | x, y, z values of the end node at indexes 0, 1 and 2. API always expects an array size of 3. |
| `isTransformForceToGlobal` | `const VARIANT FAR&` | [in] | 1: return force in Global System 0: return forces in local system of cut line |
| `firstNode` | `const VARIANT FAR&` | [in] | Node no representing the origin point for building the surface axis system |
| `secondNode` | `const VARIANT FAR&` | [in] | Node no representing the second point for building the surface axis system |
| `thirdNode` | `const VARIANT FAR&` | [in] | Node no representing the third point for building the surface axis system |
| `resultantForces` | `VARIANT FAR&` | [out] | Fx, Fy, Fz, Mx, My, Mz at array indexes 0, 1, 2, 3, 4, 5 |


**Additional or differently named parameter-table entries:**

| Table name | Direction | Description |
| --- | --- | --- |
| `nplates` | [in] | No of plates in plateList |
| `loadId` | [in] | the load case for plate analysis |
| `facingNode` | [in] | x, y, z values of the facing node at indexes 0, 1 and 2. API always expects an array size of 3. Definition of facingNode: It is the node which sits on the tip of a vector which is orthogonal to the vector startNode -> endNode and lies in the same plane as that of the plates through which the cut line passes. |

**Native return description:** Boolean(TRUE / FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetResultantForceAlongLineForParametricSurface(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)`

**Proposed managed entry:** `GetSurfaceCutResultant(ParametricSurfaceCutQuery query)`<br>
**Proposed return class:** `PlateCutResultantResult`<br>
**Result and validation contract:** Prototype has no nplates/facingNode despite parameter table. Surface name, load-list layout, output ownership and explicit/default axes require native fixtures.

<a id="output-getplatestressatpoint"></a>

### 39. GetPlateStressAtPoint

**Section:** Result: Plates Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html#ga7e9ab82a5558ce71e3926e225755acc6) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html#ga7e9ab82a5558ce71e3926e225755acc6) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html#ga7e9ab82a5558ce71e3926e225755acc6)<br>
**Comparison:** Parameter-name clarification<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** [GetPlateStressAtPoint native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1941)<br>
**Workstream:** O4

```cpp
VARIANT OSOutputUI::GetPlateStressAtPoint(const VARIANT FAR&plateNo,const VARIANT FAR&loadNo,VARIANT FAR&stressPoint,VARIANT FAR&facingPoint,VARIANT FAR&stresses)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `plateNo` | `const VARIANT FAR&` | [in] | Number of the plate (long) |
| `loadNo` | `const VARIANT FAR&` | [in] | Load number for which stress is requested (long) |
| `stressPoint` | `VARIANT FAR&` | [in] | The coordinate at which the stress is required in global axes as an array size of 3 doubles. The values of x, y, z values of the point defined in the array index (0), (1) and (2). |
| `facingPoint` | `VARIANT FAR&` | [in] | x, y, z values of the facing node at indexes 0, 1 and 2. API always expects an array size of 3 doubles. Definition of facingPoint: It is the node which sits on the tip of a vector which is orthogonal to the vector stressPoint -> facingPoint and lies in the same plane as that of the plates through which the cut line passes. |
| `stresses` | `VARIANT FAR&` | [out] | stress array size of 32 doubles. Array Index stress Type 0 None 1 MaxAbs 2 TopMax 3 TopMin 4 TopTauMax 5 BotMax 6 BotMin 7 BotTauMax 8 MaxVM 9 VMTopMax 10 VMBotMax 11 MaxTresca 12 TopTresca 13 BotTresca 14 FX 15 FY 16 FXY 17 MX 18 MY 19 MZ 20 QX 21 QY 22 Global 23 GlobalMembraneStresses 24 GlobalshearStresses 25 BasePres 26 CombXTop 27 CombYTop 28 CombXYTop 29 CombXBot 30 CombYBot 31 CombXYBot |


**Native return description:** Boolean(TRUE / FALSE) whether succeeded or not.

**2024 parameter description differences:**

| Parameter | 2024 description | 2025 / 2026 description |
| --- | --- | --- |
| `facingPoint` | Not supplied under this name. | x, y, z values of the facing node at indexes 0, 1 and 2. API always expects an array size of 3 doubles. Definition of facingPoint: It is the node which sits on the tip of a vector which is orthogonal to the vector stressPoint -> facingPoint and lies in the same plane as that of the plates through which the cut line passes. |

2024 unmatched table entries: `facingNode` — x, y, z values of the facing node at indexes 0, 1 and 2. API always expects an array size of 3 doubles. Definition of facingNode: It is the node which sits on the tip of a vector which is orthogonal to the vector stressPoint -> facingNode and lies in the same plane as that of the plates through which the cut line passes..

**Installed 2025 / 2026 interop signature:**

`System.Object GetPlateStressAtPoint(System.Object, System.Object, System.Object, System.Object, System.Object)`

**Proposed managed entry:** `GetPlateStressAtPoint(PlateStressPointQuery query)`<br>
**Proposed return class:** `PlateStressPointResult`<br>
**Result and validation contract:** 32 indexed values have heterogeneous meanings; reuse source named accessors with copied storage, finite-value validation and descriptive ID/point parameters. facingPoint name clarification does not change declaration.

<a id="output-getallsolidnormalstresses"></a>

### 40. GetAllSolidNormalStresses

**Section:** Result: Solids Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_rlt.html#gaba6b09a64357f09d33a1dfa8588028bc) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_rlt.html#gaba6b09a64357f09d33a1dfa8588028bc) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_rlt.html#gaba6b09a64357f09d33a1dfa8588028bc)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O7

```cpp
VARIANT OSOutputUI::GetAllSolidNormalStresses(const VARIANT FAR&nSolidNo,const VARIANT FAR&nCorner,const VARIANT FAR&nLC,VARIANT FAR&pdStresses)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nSolidNo` | `const VARIANT FAR&` | [in] | Solid number ID. |
| `nCorner` | `const VARIANT FAR&` | [in] | Corner of the solid. |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `pdStresses` | `VARIANT FAR&` | [out] | Normal stresses: [ SXX , SYY , SZZ ]. For additional information, please refer to Section: "Output of Element Stresses" of the Technical Reference manual. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetAllSolidNormalStresses(System.Object, System.Object, System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetSolidNormalStresses(int solidId, int cornerIndex, int loadCaseId)`<br>
**Proposed return class:** `SolidNormalStressResult`<br>
**Result and validation contract:** Three SXX,SYY,SZZ outputs; determine corner index base and whether any center sentinel is supported before defining an enum.

<a id="output-getallsolidshearstresses"></a>

### 41. GetAllSolidShearStresses

**Section:** Result: Solids Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_rlt.html#ga58a11e689111b93551978060d3beb068) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_rlt.html#ga58a11e689111b93551978060d3beb068) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_rlt.html#ga58a11e689111b93551978060d3beb068)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O7

```cpp
VARIANT OSOutputUI::GetAllSolidShearStresses(const VARIANT FAR&nSolidNo,const VARIANT FAR&nCorner,const VARIANT FAR&nLC,VARIANT FAR&pdStresses)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nSolidNo` | `const VARIANT FAR&` | [in] | Solid number ID. |
| `nCorner` | `const VARIANT FAR&` | [in] | Corner of the solid. |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `pdStresses` | `VARIANT FAR&` | [out] | Shear stresses: [ SXY , SYZ , SZX ]. For additional information, please refer to Section: "Output of Element Stresses" of the Technical Reference manual. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetAllSolidShearStresses(System.Object, System.Object, System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetSolidShearStresses(int solidId, int cornerIndex, int loadCaseId)`<br>
**Proposed return class:** `SolidShearStressResult`<br>
**Result and validation contract:** Three SXY,SYZ,SZX outputs; preserve native coordinate system and corner identity.

<a id="output-getallsolidprincipalstresses"></a>

### 42. GetAllSolidPrincipalStresses

**Section:** Result: Solids Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_rlt.html#ga7e416935359b156473f8f628ea038729) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_rlt.html#ga7e416935359b156473f8f628ea038729) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_rlt.html#ga7e416935359b156473f8f628ea038729)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O7

```cpp
VARIANT OSOutputUI::GetAllSolidPrincipalStresses(const VARIANT FAR&nSolidNo,const VARIANT FAR&nCorner,const VARIANT FAR&nLC,VARIANT FAR&pdStresses)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nSolidNo` | `const VARIANT FAR&` | [in] | Solid number ID. |
| `nCorner` | `const VARIANT FAR&` | [in] | Corner of the solid. |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `pdStresses` | `VARIANT FAR&` | [out] | Principal stresses: [ S_1 , S_1 , S_2 ]. For additional information, please refer to Section: "Output of Element Stresses" of the Technical Reference manual. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetAllSolidPrincipalStresses(System.Object, System.Object, System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetSolidPrincipalStresses(int solidId, int cornerIndex, int loadCaseId)`<br>
**Proposed return class:** `SolidPrincipalStressResult`<br>
**Result and validation contract:** Help repeats S1,S1,S2. Confirm ordering of three principal stresses before labeling S1/S2/S3 or sorting data.

<a id="output-getallsolidvonmisesstresses"></a>

### 43. GetAllSolidVonMisesStresses

**Section:** Result: Solids Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_rlt.html#ga2203456c21ee383624d6a165b279057f) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_rlt.html#ga2203456c21ee383624d6a165b279057f) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_rlt.html#ga2203456c21ee383624d6a165b279057f)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O7

```cpp
VARIANT OSOutputUI::GetAllSolidVonMisesStresses(const VARIANT FAR&nSolidNo,const VARIANT FAR&nCorner,const VARIANT FAR&nLC,VARIANT FAR&pdStress)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nSolidNo` | `const VARIANT FAR&` | [in] | Solid number ID. |
| `nCorner` | `const VARIANT FAR&` | [in] | Corner of the solid. |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `pdStress` | `VARIANT FAR&` | [out] | Von Mises stresses: |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetAllSolidVonMisesStresses(System.Object, System.Object, System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetSolidVonMisesStress(int solidId, int cornerIndex, int loadCaseId)`<br>
**Proposed return class:** `SolidVonMisesStressResult`<br>
**Result and validation contract:** Scalar/array cardinality is unspecified in the parameter description; validate shape rather than assuming from plural native name.

<a id="output-getnlloadstep"></a>

### 44. GetNLLoadStep

**Section:** Result: Dynamic Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#gaabe6a654c4c8143ebb2742cb7fb689dd) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#gaabe6a654c4c8143ebb2742cb7fb689dd) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#gaabe6a654c4c8143ebb2742cb7fb689dd)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O8

```cpp
VARIANT OSOutputUI::GetNLLoadStep(const VARIANT FAR&nLC)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |


**Native return codes / values:**

| Value | Meaning |
| --- | --- |
| `<Val>` | Load Step value. |
| `-1` | General error. |
| `-8002` | Load Case nLC not found. |
| `-9911` | Nonlinear result set is not available. |

**Installed 2025 / 2026 interop signature:**

`System.Object GetNLLoadStep(System.Object)`

**Proposed managed entry:** `GetNonlinearLoadStep(int loadCaseId)`<br>
**Proposed return class:** `NonlinearLoadStepResult`<br>
**Result and validation contract:** Direct numeric data with -1/-8002/-9911 errors; do not treat arbitrary nonzero as success or assume count rather than step semantics.

<a id="output-getnlnodedisplacements"></a>

### 45. GetNLNodeDisplacements

**Section:** Result: Dynamic Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#gac9537ae0e34eb6d10cc8119749e7aa78) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#gac9537ae0e34eb6d10cc8119749e7aa78) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#gac9537ae0e34eb6d10cc8119749e7aa78)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O8

```cpp
VARIANT OSOutputUI::GetNLNodeDisplacements(const VARIANT FAR&nNodeNo,const VARIANT FAR&nLC,const VARIANT FAR&loadStep,VARIANT FAR&loadLevel,VARIANT FAR&pdDisps)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nNodeNo` | `const VARIANT FAR&` | [in] | Node number ID. |
| `nLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `loadStep` | `const VARIANT FAR&` | [in] | Load Step index. |
| `loadLevel` | `VARIANT FAR&` | [out] | Value of Load Level. |
| `pdDisps` | `VARIANT FAR&` | [out] | Nodal displacement in GLOBAL [X, Y, Z, rX, rY, rZ]. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetNLNodeDisplacements(System.Object, System.Object, System.Object, System.Object, System.Object)`

**Proposed managed entry:** `GetNonlinearNodeDisplacements(int nodeId, int loadCaseId, int loadStepIndex)`<br>
**Proposed return class:** `NonlinearNodeDisplacementResult`<br>
**Result and validation contract:** Return load level plus six global displacement/rotation components. Verify step index base and valid range per acquired result set.

<a id="output-getnoofmodesextracted"></a>

### 46. GetNoOfModesExtracted

**Section:** Result: Dynamic Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#ga9332682c8f686e537fbbe73131a02eed) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#ga9332682c8f686e537fbbe73131a02eed) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#ga9332682c8f686e537fbbe73131a02eed)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O8

```cpp
VARIANT OSOutputUI::GetNoOfModesExtracted()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return description:** The number of mode(s) extracted by a dynamic analysis.

**Installed 2025 / 2026 interop signature:**

`System.Object GetNoOfModesExtracted()`

**Proposed managed entry:** `GetExtractedModeCount()`<br>
**Proposed return class:** `ModeCountResult`<br>
**Result and validation contract:** Direct count, not success flag; allow valid zero and reject malformed/nonintegral values.

<a id="output-getmodefrequency"></a>

### 47. GetModeFrequency

**Section:** Result: Dynamic Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#ga19eeb7e1d7f95329e97881d4aecdc42c) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#ga19eeb7e1d7f95329e97881d4aecdc42c) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#ga19eeb7e1d7f95329e97881d4aecdc42c)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O8

```cpp
VARIANT OSOutputUI::GetModeFrequency(const VARIANT FAR&varMode,VARIANT FAR&varFreq)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `varMode` | `const VARIANT FAR&` | [in] | Variant variable containing the mode number. |
| `varFreq` | `VARIANT FAR&` | [out] | Variant variable for storing the frequency value. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetModeFrequency(System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetModeFrequency(int modeNumber)`<br>
**Proposed return class:** `ModeFrequencyResult`<br>
**Result and validation contract:** Frequency in Hz with Boolean native status; confirm mode numbering; avoid invented unit conversion.

<a id="output-getmodaldisplacementatnode"></a>

### 48. GetModalDisplacementAtNode

**Section:** Result: Dynamic Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#ga6ad3b3a30f91debfd7a443ebc279cea1) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#ga6ad3b3a30f91debfd7a443ebc279cea1) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#ga6ad3b3a30f91debfd7a443ebc279cea1)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O8

```cpp
VARIANT OSOutputUI::GetModalDisplacementAtNode(const VARIANT FAR&varMode,const VARIANT FAR&varNode,VARIANT FAR&varModalDisps)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `varMode` | `const VARIANT FAR&` | [in] | Variant variable containing the mode number. |
| `varNode` | `const VARIANT FAR&` | [in] | Variant variable containing the node number. |
| `varModalDisps` | `VARIANT FAR&` | [out] | Variant array of dimension six, which returns nodal displacements. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetModalDisplacementAtNode(System.Object, System.Object, System.Object)`

**Proposed managed entry:** `GetModalNodeDisplacements(int modeNumber, int nodeId)`<br>
**Proposed return class:** `ModalNodeDisplacementResult`<br>
**Result and validation contract:** Six modal components; eigenvector normalization is not physical static displacement. Preserve mode/node metadata.

<a id="output-getmodalmassparticipationfactors"></a>

### 49. GetModalMassParticipationFactors

**Section:** Result: Dynamic Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#ga118cbf967b0d0392fa170b5f57f64081) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#ga118cbf967b0d0392fa170b5f57f64081) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#ga118cbf967b0d0392fa170b5f57f64081)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O8

```cpp
VARIANT OSOutputUI::GetModalMassParticipationFactors(const VARIANT FAR&varMode,VARIANT FAR&varfactorX,VARIANT FAR&varfactorY,VARIANT FAR&varfactorZ)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `varMode` | `const VARIANT FAR&` | [in] | Mode index. |
| `varfactorX` | `VARIANT FAR&` | [out] | Modal mass participation factor for X direction. |
| `varfactorY` | `VARIANT FAR&` | [out] | Modal mass participation factor for Y direction. |
| `varfactorZ` | `VARIANT FAR&` | [out] | Modal mass participation factor for Z direction. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetModalMassParticipationFactors(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef)`

**Proposed managed entry:** `GetModalMassParticipationFactors(int modeNumber)`<br>
**Proposed return class:** `MassParticipationResult`<br>
**Result and validation contract:** Three direction factors; verify fraction/percentage convention and normalization; do not rescale without evidence.

<a id="output-getmissingmassparticipationfactors"></a>

### 50. GetMissingMassParticipationFactors

**Section:** Result: Dynamic Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#ga7bae007519dd0144777b9874096b47b9) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#ga7bae007519dd0144777b9874096b47b9) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#ga7bae007519dd0144777b9874096b47b9)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O8

```cpp
VARIANT OSOutputUI::GetMissingMassParticipationFactors(const VARIANT FAR&varLC,VARIANT FAR&varfactorX,VARIANT FAR&varfactorY,VARIANT FAR&varfactorZ)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `varLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `varfactorX` | `VARIANT FAR&` | [out] | Missing mass modal participation factor for X direction. |
| `varfactorY` | `VARIANT FAR&` | [out] | Missing mass modal participation factor for Y direction. |
| `varfactorZ` | `VARIANT FAR&` | [out] | Missing mass modal participation factor for Z direction. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

No member with this name appears in either inspected IOSOutputUI interface. This does not establish native IDispatch absence.

**Proposed managed entry:** `GetMissingMassParticipationFactors(int loadCaseId)`<br>
**Proposed return class:** `MassParticipationResult`<br>
**Result and validation contract:** Documented in all three helps but absent from both inspected IOSOutputUI interfaces. Verify acquired IDispatch member/signature; unsupported until established, not automatically unavailable for the entire year.

<a id="output-gettimehistoryintegrationstepinfo"></a>

### 51. GetTimeHistoryIntegrationStepInfo

**Section:** Result: Dynamic Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#gaea1ad7d38067027dbe6c74a6152f2f5d) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#gaea1ad7d38067027dbe6c74a6152f2f5d) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#gaea1ad7d38067027dbe6c74a6152f2f5d)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O9

```cpp
VARIANT OSOutputUI::GetTimeHistoryIntegrationStepInfo(double*deltaT)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `deltaT` | `double*` | [out] | - Time step used for the integration in seconds. |


**Native return codes / values:**

| Value | Meaning |
| --- | --- |
| `0` | No time-history analysis is done. |
| `<val>` | Number of time-step responses are computed. |
| `-1` | General error. |

**Installed 2025 / 2026 interop signature:**

`System.Object GetTimeHistoryIntegrationStepInfo(Double ByRef)`

**Proposed managed entry:** `GetTimeHistoryIntegrationInfo()`<br>
**Proposed return class:** `TimeHistoryIntegrationResult`<br>
**Result and validation contract:** Return count plus deltaTimeSeconds; zero means no time-history result and -1 error. Validate count, positive finite interval and the documented t=0 sample convention.

<a id="output-gettimehistoryresponseattime"></a>

### 52. GetTimeHistoryResponseAtTime

**Section:** Result: Dynamic Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#ga646f263e1b9a53e4212f3c2cc5ed8acc) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#ga646f263e1b9a53e4212f3c2cc5ed8acc) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#ga646f263e1b9a53e4212f3c2cc5ed8acc)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O9

```cpp
VARIANT OSOutputUI::GetTimeHistoryResponseAtTime(long lCase,long nodeNo,long dofNo,long responseType,double atTime,double*response)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `lCase` | `long` | [in] | Load case number for future use. Use 0 at present. |
| `nodeNo` | `long` | [in] | Node number where the response is sought. |
| `dofNo` | `long` | [in] | Degrees of freedom define as DegreesOfFreedom enum. Value Degrees Of Freedom Fx = 1 DegreesOfFreedom.Fx Fy = 2 DegreesOfFreedom.Fy Fz = 3 DegreesOfFreedom.Fz Mx = 4 DegreesOfFreedom.Mx My = 5 DegreesOfFreedom.My Mz = 6 DegreesOfFreedom.Mz |
| `responseType` | `long` | [in] | Response type, i.e., displacement, velocity, or acceleration defined in TimeHistoryResponseType enum. Value Response Type displacement = 0 TimeHistoryResponseType.dispResponse velocity = 1 TimeHistoryResponseType.velResponse acceleration = 2 TimeHistoryResponseType.acclResponse |
| `atTime` | `double` | [in] | Time in seconds for which the respose is sought. |
| `response` | `double*` | [out] | The response. |


**Native return codes / values:**

| Value | Meaning |
| --- | --- |
| `0` | No time-history analysis is done. |
| `-1` | General error. |
| `1` | Results successfuly returned. |

**Installed 2025 / 2026 interop signature:**

`System.Object GetTimeHistoryResponseAtTime(Int32, Int32, OpenSTAADUI.DegreesOfFreedom, OpenSTAADUI.TimeHistoryResponseType, Double, Double ByRef)`

**Proposed managed entry:** `GetTimeHistoryResponseAtTime(int nodeId, ResponseDegreeOfFreedom degreeOfFreedom, TimeHistoryResponseKind responseKind, double timeSeconds)`<br>
**Proposed return class:** `TimeHistorySampleResult`<br>
**Result and validation contract:** Keep reserved native load-case argument internal and zero. Validate integration bounds; map DOF 1..6 and kind 0..2; preserve response-specific units.

<a id="output-gettimehistoryresponse"></a>

### 53. GetTimeHistoryResponse

**Section:** Result: Dynamic Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#ga2e7ec7357bfaef20ecd21bba24e06a57) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#ga2e7ec7357bfaef20ecd21bba24e06a57) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#ga2e7ec7357bfaef20ecd21bba24e06a57)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O9

```cpp
VARIANT OSOutputUI::GetTimeHistoryResponse(long lCase,long nodeNo,long dofNo,long responseType,VARIANT FAR*responses)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `lCase` | `long` | [in] | Load case number for future use. Use 0 at present. |
| `nodeNo` | `long` | [in] | Node number where the response is sought. |
| `dofNo` | `long` | [in] | Degrees of freedom define as DegreesOfFreedom enum. Value Degrees Of Freedom Fx = 1 DegreesOfFreedom.Fx Fy = 2 DegreesOfFreedom.Fy Fz = 3 DegreesOfFreedom.Fz Mx = 4 DegreesOfFreedom.Mx My = 5 DegreesOfFreedom.My Mz = 6 DegreesOfFreedom.Mz |
| `responseType` | `long` | [in] | Response type, i.e., displacement, velocity, or acceleration defined in TimeHistoryResponseType enum. Value Response Type displacement = 0 TimeHistoryResponseType.dispResponse velocity = 1 TimeHistoryResponseType.velResponse acceleration = 2 TimeHistoryResponseType.acclResponse |
| `responses` | `VARIANT FAR*` | [out] | VARIANT double array returning the responses at the integration steps. The size of the array is (no of integration steps), the first location is for the response at time = 0.0. |


**Native return codes / values:**

| Value | Meaning |
| --- | --- |
| `0` | No time-history analysis is done. |
| `-1` | General error. |
| `1` | Results successfuly returned. |

**Installed 2025 / 2026 interop signature:**

`System.Object GetTimeHistoryResponse(Int32, Int32, OpenSTAADUI.DegreesOfFreedom, OpenSTAADUI.TimeHistoryResponseType, System.Object ByRef)`

**Proposed managed entry:** `GetTimeHistoryResponse(int nodeId, ResponseDegreeOfFreedom degreeOfFreedom, TimeHistoryResponseKind responseKind)`<br>
**Proposed return class:** `TimeHistorySeriesResult`<br>
**Result and validation contract:** Materialized bounded samples, t=0 at first entry, stable count/interval snapshot; reject count mismatch and avoid unbounded allocation.

<a id="output-gettimehistoryresponseminmax"></a>

### 54. GetTimeHistoryResponseMinMax

**Section:** Result: Dynamic Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html#ga3206fd5f32a98d437bd0dab1e3a82bf4) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#ga3206fd5f32a98d437bd0dab1e3a82bf4) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#ga3206fd5f32a98d437bd0dab1e3a82bf4)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O9

```cpp
VARIANT OSOutputUI::GetTimeHistoryResponseMinMax(long lCase,long nodeNo,long dofNo,long responseType,double*responseMax,double*timeMax,double*responseMin,double*timeMin)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `lCase` | `long` | [in] | Use 0. |
| `nodeNo` | `long` | [in] | Node number where the response is sought. |
| `dofNo` | `long` | [in] | Degrees of freedom. Degrees Of Freedom Value Fx 1 Fy 2 Fz 3 |
| `responseType` | `long` | [in] | Response type, i.e., displacement, velocity, or acceleration. Response Type Value displacement 0 velocity 1 acceleration 2 |
| `responseMax` | `double*` | [out] | Maximum response. |
| `timeMax` | `double*` | [out] | The time when the maximum response occurs. |
| `responseMin` | `double*` | [out] | Minimum response. |
| `timeMin` | `double*` | [out] | The time when the minimum response occurs. |


**Native return codes / values:**

| Value | Meaning |
| --- | --- |
| `0` | No time-history analysis is done. |
| `-1` | General error. |
| `-2001` | Node not found |
| `-8002` | Load Case lCase not found. |
| `-9915` | Result is not found or could not be loaded. |

**Installed 2025 / 2026 interop signature:**

`System.Object GetTimeHistoryResponseMinMax(Int32, Int32, OpenSTAADUI.DegreesOfFreedom, OpenSTAADUI.TimeHistoryResponseType, Double ByRef, Double ByRef, Double ByRef, Double ByRef)`

**Proposed managed entry:** `GetTimeHistoryResponseExtrema(int nodeId, ResponseDegreeOfFreedom degreeOfFreedom, TimeHistoryResponseKind responseKind)`<br>
**Proposed return class:** `TimeHistoryExtremaResult`<br>
**Result and validation contract:** Maximum/minimum with occurrence times; success code is omitted from table and DOF table lists only 1..3 while metadata uses six-value enum. Verify both; never infer Boolean conversion for negative codes.

The return table lists only unavailable/error outcomes and omits a positive success entry. Treat a tested success convention as a release prerequisite, not an inferred constant.

<a id="output-getmaxbeamstresses"></a>

### 55. GetMaxBeamStresses

**Section:** Result: Dynamic Analysis Results<br>
**Availability in help:** 2024 absent · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html#gae0623051a17845003fd341a3ca0bb007) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html#gae0623051a17845003fd341a3ca0bb007)<br>
**Comparison:** Added help member<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** No direct native target in the reference Output adapter.<br>
**Workstream:** O10

```cpp
long OSOutputUI::GetMaxBeamStresses(long nBeamNo,long nLC,double*pdCompStress,long*nCompCorner,double*pdTensileStress,long*nTensileCorner)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `nBeamNo` | `long` | [in] | Beam ID. |
| `nLC` | `long` | [in] | Load Case reference ID. |
| `pdCompStress` | `double*` | [out] | Value of maximum compressive stress |
| `nCompCorner` | `long*` | [out] | 0 for non-corner-section material (eg. prismatic circle and pipe), 1 to 4 for section corner where maximum compressive stress was found. |
| `pdTensileStress` | `double*` | [out] | Value of maximum tensile stress |
| `nTensileCorner` | `long*` | [out] | 0 for non-corner-section material (eg. prismatic circle and pipe), 1 to 4 for section corner where maximum tensile stress was found. |


**Native return codes / values:**

| Value | Meaning |
| --- | --- |
| `0` | OK. |
| `-3001` | Cannot find member nBeamNo . |
| `-8002` | Load Case nLC not found. |
| `-9004` | Beam Forces results not loaded. |

**2024 boundary:** This member is absent from the reviewed Output group and generated header. The 2024 interop assembly was not found at the corresponding path. Do not advertise it from version assumptions alone.

**Installed 2025 / 2026 interop signature:**

`Int32 GetMaxBeamStresses(Int32, Int32, Double ByRef, Int32 ByRef, Double ByRef, Int32 ByRef)`

**Proposed managed entry:** `GetMaximumBeamStresses(int memberId, int loadCaseId)`<br>
**Proposed return class:** `BeamStressExtremaResult`<br>
**Result and validation contract:** Optional capability candidate for 2025/2026; native zero success, -3001/-8002/-9004 errors. Return compressive/tensile stress and both corner locations including zero for non-corner sections.

<a id="output-getstaticcheckresult"></a>

### 56. GetStaticCheckResult

**Section:** Result: Static Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___static_check.html#ga8343b8a41832142e342fb4b8750a6e38) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___static_check.html#ga8343b8a41832142e342fb4b8750a6e38) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___static_check.html#ga8343b8a41832142e342fb4b8750a6e38)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** [GetStaticCheckResults native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L91), [GetStaticCheckResultAsync native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L985), [GetStaticCheckResults native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1394)<br>
**Workstream:** O2

```cpp
VARIANT OSOutputUI::GetStaticCheckResult(const VARIANT FAR&varLC,VARIANT FAR&varForces,VARIANT FAR&varReactions)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `varLC` | `const VARIANT FAR&` | [in] | Load Case reference ID. |
| `varForces` | `VARIANT FAR&` | [out] | Loads: [FX, FY, FZ, MX, MY, MZ]. |
| `varReactions` | `VARIANT FAR&` | [out] | Reactions:[FX, FY, FZ, MX, MY, MZ]. |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetStaticCheckResult(System.Object, System.Object ByRef, System.Object ByRef)`

**Proposed managed entry:** `GetStaticCheckResult(int loadCaseId)`<br>
**Proposed return class:** `StaticCheckQueryResult`<br>
**Result and validation contract:** Two six-component arrays with loads/reactions. Reuse source StaticCheckResult meaning; status and both buffers must be valid before computing residuals.

<a id="output-isbucklinganalysisresultsavailable"></a>

### 57. IsBucklingAnalysisResultsAvailable

**Section:** Result: Buckling Analysis Result Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___buckling_rlt.html#ga65682c9aeb4eacc4a9ee03d9e21d166c) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___buckling_rlt.html#ga65682c9aeb4eacc4a9ee03d9e21d166c) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___buckling_rlt.html#ga65682c9aeb4eacc4a9ee03d9e21d166c)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** [AreBucklingAnalysisResultsAvailable native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L54)<br>
**Workstream:** O5

```cpp
VARIANT OSOutputUI::IsBucklingAnalysisResultsAvailable()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return codes / values:**

| Value | Meaning |
| --- | --- |
| `1` | Buckling Analysis results are available |
| `0` | Buckling Analysis results are not available |

**Installed 2025 / 2026 interop signature:**

`System.Object IsBucklingAnalysisResultsAvailable()`

**Proposed managed entry:** `GetBucklingResultAvailability()`<br>
**Proposed return class:** `BucklingResultAvailabilityResult`<br>
**Result and validation contract:** Availability 0/1 is data; false must not become generic query failure.

<a id="output-getnoofbucklingfactors"></a>

### 58. GetNoOfBucklingFactors

**Section:** Result: Buckling Analysis Result Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___buckling_rlt.html#ga61ea226383660ee7e9bc9cb687dfb20b) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___buckling_rlt.html#ga61ea226383660ee7e9bc9cb687dfb20b) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___buckling_rlt.html#ga61ea226383660ee7e9bc9cb687dfb20b)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** [GetNumberOfBucklingFactorsComputed native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L923)<br>
**Workstream:** O5

```cpp
VARIANT OSOutputUI::GetNoOfBucklingFactors()
```

**Native parameters (2026, also 2025):**

No parameters.

**Native return codes / values:**

| Value | Meaning |
| --- | --- |
| `nBucklingFactors` | The number of buckling factor(s) extracted by the eigen analysis. (Type: long) |

**Installed 2025 / 2026 interop signature:**

`System.Object GetNoOfBucklingFactors()`

**Proposed managed entry:** `GetBucklingFactorCount()`<br>
**Proposed return class:** `BucklingFactorCountResult`<br>
**Result and validation contract:** Direct integral count; distinguish zero factors from native failure.

<a id="output-getbucklingfactor"></a>

### 59. GetBucklingFactor

**Section:** Result: Buckling Analysis Result Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___buckling_rlt.html#gab1fae0d3b4a716ba9919da5d54f242c0) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___buckling_rlt.html#gab1fae0d3b4a716ba9919da5d54f242c0) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___buckling_rlt.html#gab1fae0d3b4a716ba9919da5d54f242c0)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** [GetBucklingFactor native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L931)<br>
**Workstream:** O5

```cpp
VARIANT OSOutputUI::GetBucklingFactor(const VARIANT FAR&varBucklingMode,VARIANT FAR&varLambda)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `varBucklingMode` | `const VARIANT FAR&` | [in] | Buckling mode number (Type: Long) |
| `varLambda` | `VARIANT FAR&` | [out] | The buckling factor. (Type: double) |


**Native return description:** Boolean (TRUE/FALSE) whether succeeded or not.

**Installed 2025 / 2026 interop signature:**

`System.Object GetBucklingFactor(System.Object, System.Object)`

**Proposed managed entry:** `GetBucklingFactor(int bucklingModeNumber)`<br>
**Proposed return class:** `BucklingFactorResult`<br>
**Result and validation contract:** Help requires output reference; both vendor assemblies expose varLambda by value and source passes it by value. Establish a working native binding or return unsupported; never report initialized zero as extracted data.

<a id="output-getbucklingmodedisplacementatnode"></a>

### 60. GetBucklingModeDisplacementAtNode

**Section:** Result: Buckling Analysis Result Analysis Results<br>
**Availability in help:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___buckling_rlt.html#ga4a1940a1616821e91e5cf3e42492d6f6) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___buckling_rlt.html#ga4a1940a1616821e91e5cf3e42492d6f6) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___buckling_rlt.html#ga4a1940a1616821e91e5cf3e42492d6f6)<br>
**Comparison:** Unchanged<br>
**Current implementation:** No typed Output method or production dispatch found.<br>
**Main reference:** [GetBucklingModeDisplacementAtNode native call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L945)<br>
**Workstream:** O5

```cpp
VARIANT OSOutputUI::GetBucklingModeDisplacementAtNode(const VARIANT FAR&varBucklingMode,const VARIANT FAR&varNode,VARIANT FAR&varDisps)
```

**Native parameters (2026, also 2025):**

| Parameter | Native declaration type | Logical direction | Meaning |
| --- | --- | --- | --- |
| `varBucklingMode` | `const VARIANT FAR&` | [in] | Buckling mode number (Type: Long) |
| `varNode` | `const VARIANT FAR&` | [in] | Node number at which buckling analysis result is to be extracted (Type: Long) |
| `varDisps` | `VARIANT FAR&` | Not named in parameter table | Description not supplied under this declaration name. |


**Additional or differently named parameter-table entries:**

| Table name | Direction | Description |
| --- | --- | --- |
| `varModalDisps` | [out] | Array of buckling mode displacements (Type: Double, Size: 6) |

**Native return codes / values:**

| Value | Meaning |
| --- | --- |
| `1` | Buckling analysis result extraction successful |
| `0` | Buckling analysis result extraction unsuccessful |

**Installed 2025 / 2026 interop signature:**

`System.Object GetBucklingModeDisplacementAtNode(System.Object, System.Object, System.Object ByRef)`

**Proposed managed entry:** `GetBucklingModeDisplacements(int bucklingModeNumber, int nodeId)`<br>
**Proposed return class:** `BucklingModeDisplacementResult`<br>
**Result and validation contract:** Six displacements, not the source scalar return. varDisps prototype corresponds to varModalDisps prose. Preserve normalization and six components.

## 4. Supplemental unit declarations and interop candidates

Both vendor IOSOutputUI interfaces contain 72 methods and match in method order, names, CLR return/parameter shapes and parameter metadata. They cover 59 of the 60 documented names. GetMissingMassParticipationFactors is help-only relative to those inspected interfaces; thirteen unit queries sit outside the detailed group inventory. All thirteen also appear as summary-only native class declarations in every reviewed year. The resulting evidence union has 73 names.

The native class listing declares all thirteen as VARIANT-returning functions with mutable VARIANT FAR& strUnit parameters; those declarations match across all three years. No detailed parameter/return contract is supplied. Both inspected interop interfaces instead take System.Object strUnit by value and return System.Object. The metadata alone does not say whether that argument is an input selector, a caller-supplied holder or a legacy signature issue, nor whether the result is a status, unit label or code. All are deferred to O11 contract discovery. Wrapper base/input unit properties cannot substitute for per-result Output units.

| Supplemental unit query | Native class declaration in all three years | 2024 / 2025 / 2026 class references | Inspected interop signature |
| --- | --- | --- | --- |
| `GetOutputUnitForDimension` | `VARIANT OSOutputUI::GetOutputUnitForDimension(VARIANT FAR&strUnit)` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_output_u_i.html#a3abde325948e729a05c77c488e3f7cb5) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_output_u_i.html#a3abde325948e729a05c77c488e3f7cb5) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_output_u_i.html#a3abde325948e729a05c77c488e3f7cb5) | `System.Object GetOutputUnitForDimension(System.Object)` |
| `GetOutputUnitForSectDimension` | `VARIANT OSOutputUI::GetOutputUnitForSectDimension(VARIANT FAR&strUnit)` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_output_u_i.html#a1ce8c4ad2bfb01d6609939e70047b3d5) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_output_u_i.html#a1ce8c4ad2bfb01d6609939e70047b3d5) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_output_u_i.html#a1ce8c4ad2bfb01d6609939e70047b3d5) | `System.Object GetOutputUnitForSectDimension(System.Object)` |
| `GetOutputUnitForSectArea` | `VARIANT OSOutputUI::GetOutputUnitForSectArea(VARIANT FAR&strUnit)` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_output_u_i.html#a55f25054f3bc3909fbe6a8804217fc51) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_output_u_i.html#a55f25054f3bc3909fbe6a8804217fc51) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_output_u_i.html#a55f25054f3bc3909fbe6a8804217fc51) | `System.Object GetOutputUnitForSectArea(System.Object)` |
| `GetOutputUnitForSectInertia` | `VARIANT OSOutputUI::GetOutputUnitForSectInertia(VARIANT FAR&strUnit)` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_output_u_i.html#af3055442417904faadf169269129c4df) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_output_u_i.html#af3055442417904faadf169269129c4df) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_output_u_i.html#af3055442417904faadf169269129c4df) | `System.Object GetOutputUnitForSectInertia(System.Object)` |
| `GetOutputUnitForSectModulus` | `VARIANT OSOutputUI::GetOutputUnitForSectModulus(VARIANT FAR&strUnit)` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_output_u_i.html#a55fed45bb6d6b8a2e5a3a677e1d2f157) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_output_u_i.html#a55fed45bb6d6b8a2e5a3a677e1d2f157) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_output_u_i.html#a55fed45bb6d6b8a2e5a3a677e1d2f157) | `System.Object GetOutputUnitForSectModulus(System.Object)` |
| `GetOutputUnitForDensity` | `VARIANT OSOutputUI::GetOutputUnitForDensity(VARIANT FAR&strUnit)` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_output_u_i.html#a06da8643fc65922a0c98199202ae81be) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_output_u_i.html#a06da8643fc65922a0c98199202ae81be) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_output_u_i.html#a06da8643fc65922a0c98199202ae81be) | `System.Object GetOutputUnitForDensity(System.Object)` |
| `GetOutputUnitForDisplacement` | `VARIANT OSOutputUI::GetOutputUnitForDisplacement(VARIANT FAR&strUnit)` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_output_u_i.html#aacee193a6e7b8f9251782bc476b28cab) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_output_u_i.html#aacee193a6e7b8f9251782bc476b28cab) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_output_u_i.html#aacee193a6e7b8f9251782bc476b28cab) | `System.Object GetOutputUnitForDisplacement(System.Object)` |
| `GetOutputUnitForRotation` | `VARIANT OSOutputUI::GetOutputUnitForRotation(VARIANT FAR&strUnit)` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_output_u_i.html#a183459694370e64580afb21db6ac117a) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_output_u_i.html#a183459694370e64580afb21db6ac117a) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_output_u_i.html#a183459694370e64580afb21db6ac117a) | `System.Object GetOutputUnitForRotation(System.Object)` |
| `GetOutputUnitForForce` | `VARIANT OSOutputUI::GetOutputUnitForForce(VARIANT FAR&strUnit)` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_output_u_i.html#a38aaffb61f9a3bb643cd1c598c76c65f) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_output_u_i.html#a38aaffb61f9a3bb643cd1c598c76c65f) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_output_u_i.html#a38aaffb61f9a3bb643cd1c598c76c65f) | `System.Object GetOutputUnitForForce(System.Object)` |
| `GetOutputUnitForMoment` | `VARIANT OSOutputUI::GetOutputUnitForMoment(VARIANT FAR&strUnit)` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_output_u_i.html#ab7357da6f1f6c8f6e446ac2794d7b940) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_output_u_i.html#ab7357da6f1f6c8f6e446ac2794d7b940) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_output_u_i.html#ab7357da6f1f6c8f6e446ac2794d7b940) | `System.Object GetOutputUnitForMoment(System.Object)` |
| `GetOutputUnitForDistForce` | `VARIANT OSOutputUI::GetOutputUnitForDistForce(VARIANT FAR&strUnit)` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_output_u_i.html#acf08076fa6f2d3c793f447c153fc5e6a) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_output_u_i.html#acf08076fa6f2d3c793f447c153fc5e6a) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_output_u_i.html#acf08076fa6f2d3c793f447c153fc5e6a) | `System.Object GetOutputUnitForDistForce(System.Object)` |
| `GetOutputUnitForDistMoment` | `VARIANT OSOutputUI::GetOutputUnitForDistMoment(VARIANT FAR&strUnit)` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_output_u_i.html#ad74eafff53250fabb09be2babb1aaf8d) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_output_u_i.html#ad74eafff53250fabb09be2babb1aaf8d) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_output_u_i.html#ad74eafff53250fabb09be2babb1aaf8d) | `System.Object GetOutputUnitForDistMoment(System.Object)` |
| `GetOutputUnitForStress` | `VARIANT OSOutputUI::GetOutputUnitForStress(VARIANT FAR&strUnit)` | [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_output_u_i.html#a4088a495e7f7fc54f4130f720098bea3) / [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_output_u_i.html#a4088a495e7f7fc54f4130f720098bea3) / [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_output_u_i.html#a4088a495e7f7fc54f4130f720098bea3) | `System.Object GetOutputUnitForStress(System.Object)` |

The native class listing additionally contains `OnFinalRelease()` and `SetStructure(CStruct* pStruct)`. These are native implementation/lifecycle helpers, absent from IOSOutputUI metadata, and are excluded from managed domain porting. They are not instructions to final-release a shared COM object or pass a native structural pointer from managed code. The class lists 74 / 75 / 75 names: 59 / 60 / 60 detailed domain methods, thirteen summary-only unit queries and these two helpers. The three supplemental class pages are fingerprinted separately from the ten traversed group pages per year.

The vendor enum domain is identical between both inspected assemblies:

| Enum | Underlying type | Values |
| --- | --- | --- |
| `OpenSTAADUI.DegreesOfFreedom` | `System.Int32` | `Fx = 1`, `Fy = 2`, `Fz = 3`, `Mx = 4`, `My = 5`, `Mz = 6` |
| `OpenSTAADUI.TimeHistoryResponseType` | `System.Int32` | `dispResponse = 0`, `velResponse = 1`, `acclResponse = 2` |

Use semantic managed response/DOF enums with internal verified translations. The min/max help shows only three translational DOFs; metadata exposing six enum values does not prove that all six are accepted by that operation.

## 5. Main-project managed reference surface

All 86 overloads are pending in the open-source Output adapter. The table records the exact read-only source signature, its matching adapter method and reachable native targets. Local method-call expansion is conservative by name/arity; direct native call-site evidence is retained in the comparison data. Pure transformations and convenience wrappers are not counted as additional native functions.

There are 33 Task-returning overloads. Two overloads expose out Vector3D pairs; these are source evidence, not proposed public signatures. Adapt them to named status/data results rather than porting public reference outputs. Four offset-aware/physical-joint overloads additionally require an unported Property operation.

| Reference managed signature | Source interface / adapter | Native targets reached or dependency | Port decision |
| --- | --- | --- | --- |
| `MemberSteelDesignResults GetSteelDesignResultsForBeam(Beam beam, IEnumerable<ILoadCase> allLoadCases)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L18) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L142) | `GetMemberSteelDesignResults` | Reuse semantics with status/data and dependency review |
| `MemberSteelDesignResults GetSteelDesignResultsForBeam(IOSLoad load, Beam beam)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L19) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L133) | `GetMemberSteelDesignResults` | Reuse semantics with status/data and dependency review |
| `IEnumerable<MemberSteelDesignResults> GetSteelDesignResultsForBeams(IEnumerable<Beam> beams, IEnumerable<ILoadCase> allLoadCases)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L20) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L105) | `GetMemberSteelDesignResults` | Reuse semantics with status/data and dependency review |
| `IEnumerable<MemberSteelDesignResults> GetSteelDesignResultsForBeams(IOSLoad load, IEnumerable<Beam> beams)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L21) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L118) | `GetMemberSteelDesignResults` | Reuse semantics with status/data and dependency review |
| `bool AreBucklingAnalysisResultsAvailable()` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L22) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L52) | `IsBucklingAnalysisResultsAvailable` | Reuse semantics with status/data and dependency review |
| `int GetNumberOfBucklingFactorsComputed()` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L23) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L921) | `GetNoOfBucklingFactors` | Reuse semantics with status/data and dependency review |
| `double GetBucklingFactor(int bucklingMode)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L24) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L926) | `GetBucklingFactor` | Reuse semantics with status/data and dependency review |
| `double GetBucklingModeDisplacement(int bucklingMode, Node atNode)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L25) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L936) | `GetBucklingModeDisplacementAtNode` | Reuse semantics with status/data and dependency review |
| `double GetBucklingModeDisplacementAtNode(int bucklingMode, int nodeId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L26) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L941) | `GetBucklingModeDisplacementAtNode` | Reuse semantics with status/data and dependency review |
| `bool AreResultsAvailable()` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L28) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L47) | `AreResultsAvailable` | Reuse semantics with status/data and dependency review |
| `double GetMemberSteelUtilizationRatio(int beamId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L29) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L196) | `GetMemberSteelDesignRatio` | Reuse semantics with status/data and dependency review |
| `MemberForces GetMemberEndForces(string lcTitle, int beamId, int loadCaseId, MemberEnd memberEnd)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L31) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L203) | `AreResultsAvailable`, `GetMemberEndForces` | Reuse semantics with status/data and dependency review |
| `MemberForces GetMemberEndForces(string lcTitle, int beamId, int loadCaseId, MemberEnd memberEnd, LocalOrGlobal coordinateSystem)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L32) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L213) | `AreResultsAvailable`, `GetMemberEndForces` | Reuse semantics with status/data and dependency review |
| `OffsetAwareMemberEndForces GetMemberEndForcesConsideringOffsets(string loadCaseTitle, Beam beam, int loadCaseId, MemberEnd memberEnd)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L46) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L270) | `AreResultsAvailable`, `GetMemberEndForces` | Defer until offset-property dependency is available |
| `OffsetAwareMemberEndForces GetMemberEndForcesConsideringOffsets(string loadCaseTitle, int beamId, int loadCaseId, MemberEnd memberEnd)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L53) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L258) | `AreResultsAvailable`, `GetMemberEndForces` | Defer until offset-property dependency is available |
| `MemberForces GetMemberEndForcesAtPhysicalJoint(string loadCaseTitle, Beam beam, int loadCaseId, MemberEnd memberEnd)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L60) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L332) | `AreResultsAvailable`, `GetMemberEndForces` | Defer until offset-property dependency is available |
| `MemberForces GetMemberEndForcesAtPhysicalJoint(string loadCaseTitle, int beamId, int loadCaseId, MemberEnd memberEnd)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L67) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L341) | `AreResultsAvailable`, `GetMemberEndForces` | Defer until offset-property dependency is available |
| `Task<MemberForces> GetMemberEndForcesAsync(string lcTitle, int beamId, int loadCaseId, MemberEnd memberEnd)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L75) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1034) | `GetMemberEndForces` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `Task<double> GetMemberSteelUtilizationRatioAsync(Beam b)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L76) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1015) | `GetMemberSteelDesignRatio` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `Task<double> GetMemberSteelUtilizationRatioAsync(int beamId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L77) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1007) | `GetMemberSteelDesignRatio` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `Dictionary<Beam, double> GetMemberSteelUtilizationRatios(HashSet<Beam> beams)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L78) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L171) | `GetMemberSteelDesignRatio` | Reuse semantics with status/data and dependency review |
| `void PopulateMemberSteelUtilizationRatios(HashSet<Beam> forBeams)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L79) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L185) | `GetMemberSteelDesignRatio` | Reuse semantics with status/data and dependency review |
| `Task<Dictionary<Beam, double>> GetMemberSteelUtilizationRatiosAsync(HashSet<Beam> beams)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L80) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1020) | `GetMemberSteelDesignRatio` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `PlateCornerForces GetPlateCornerForces(ILoadCase loadCase, Plate plate, Node cornerNode)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L100) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L361) | `GetPlateCornerForces` | Reuse semantics with status/data and dependency review |
| `PlateCornerForces GetPlateCornerForces(int loadCaseId, int plateId, int cornerNodeId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L115) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L403) | `GetPlateCornerForces` | Reuse semantics with status/data and dependency review |
| `PlateCornerForces GetPlateCornerForces(int loadCaseId, int plateId, int cornerNodeId, IOSGeometry geometry, IOSLoad load)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L131) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L414) | `GetPlateCornerForces` | Reuse semantics with status/data and dependency review |
| `PlateCornerForces GetPlateCornerForces(int loadCaseId, int plateId, int cornerNodeId, LoadCaseType loadCaseType, IOSGeometry geometry, IOSLoad load)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L149) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L431) | `GetPlateCornerForces` | Reuse semantics with status/data and dependency review |
| `PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<ILoadCase> loadCases, Plate plate, Node cornerNode)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L163) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L462) | `GetPlateCornerForces` | Reuse semantics with status/data and dependency review |
| `PlateCornerForcesResultSet GetPlateCornerForces(ILoadCase loadCase, Plate plate)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L169) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L474) | `GetPlateCornerForces` | Reuse semantics with status/data and dependency review |
| `PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<ILoadCase> loadCases, Plate plate)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L175) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L485) | `GetPlateCornerForces` | Reuse semantics with status/data and dependency review |
| `PlateCornerForcesResultSet GetPlateCornerForces(ILoadCase loadCase, IEnumerable<Plate> plates)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L181) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L499) | `GetPlateCornerForces` | Reuse semantics with status/data and dependency review |
| `PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<ILoadCase> loadCases, IEnumerable<Plate> plates)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L188) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L514) | `GetPlateCornerForces` | Reuse semantics with status/data and dependency review |
| `PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<PlateCornerForcesRequest> requests)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L195) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L532) | `GetPlateCornerForces` | Reuse semantics with status/data and dependency review |
| `PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<int> loadCaseIds, int plateId, int cornerNodeId, IOSGeometry geometry, IOSLoad load)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L204) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L553) | `GetPlateCornerForces` | Reuse semantics with status/data and dependency review |
| `Task<PlateCornerForces> GetPlateCornerForcesAsync(ILoadCase loadCase, Plate plate, Node cornerNode)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L215) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1060) | `GetPlateCornerForces` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `Task<PlateCornerForces> GetPlateCornerForcesAsync(int loadCaseId, int plateId, int cornerNodeId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L221) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1069) | `GetPlateCornerForces` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `Task<PlateCornerForces> GetPlateCornerForcesAsync(int loadCaseId, int plateId, int cornerNodeId, IOSGeometry geometry, IOSLoad load)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L225) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1079) | `GetPlateCornerForces` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `Task<PlateCornerForces> GetPlateCornerForcesAsync(int loadCaseId, int plateId, int cornerNodeId, LoadCaseType loadCaseType, IOSGeometry geometry, IOSLoad load)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L234) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1096) | `GetPlateCornerForces` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `Task<PlateCornerForcesResultSet> GetPlateCornerForcesAsync(IEnumerable<ILoadCase> loadCases, Plate plate, Node cornerNode)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L244) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1115) | `GetPlateCornerForces` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `Task<PlateCornerForcesResultSet> GetPlateCornerForcesAsync(ILoadCase loadCase, Plate plate)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L248) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1124) | `GetPlateCornerForces` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `Task<PlateCornerForcesResultSet> GetPlateCornerForcesAsync(IEnumerable<ILoadCase> loadCases, Plate plate)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L252) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1132) | `GetPlateCornerForces` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `Task<PlateCornerForcesResultSet> GetPlateCornerForcesAsync(ILoadCase loadCase, IEnumerable<Plate> plates)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L256) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1140) | `GetPlateCornerForces` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `Task<PlateCornerForcesResultSet> GetPlateCornerForcesAsync(IEnumerable<ILoadCase> loadCases, IEnumerable<Plate> plates)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L260) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1148) | `GetPlateCornerForces` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `Task<PlateCornerForcesResultSet> GetPlateCornerForcesAsync(IEnumerable<PlateCornerForcesRequest> requests)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L264) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1156) | `GetPlateCornerForces` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `Task<PlateCornerForcesResultSet> GetPlateCornerForcesAsync(IEnumerable<int> loadCaseIds, int plateId, int cornerNodeId, IOSGeometry geometry, IOSLoad load)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L268) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1163) | `GetPlateCornerForces` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `PlateCornerForces GetPlateCornerForcesInGlobalCoordinates(ILoadCase loadCase, Plate plate, Node cornerNode)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L286) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L577) | `GetPlateCornerForces` | Reuse semantics with status/data and dependency review |
| `Task<PlateCornerForces> GetPlateCornerForcesInGlobalCoordinatesAsync(ILoadCase loadCase, Plate plate, Node cornerNode)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L292) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1180) | `GetPlateCornerForces` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `bool GetPlateCornerForcesGlobal(Plate plate, Node cornerNode, int loadCaseId, out Vector3D globalForce, out Vector3D globalMoment)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L303) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L588) | `GetPlateCornerForces` | Adapt to a named result; no public reference outputs |
| `MemberForces GetPlateCornerForces(ILoadCase loadCase, Plate plate, Node cornerNode, Node otherMemberNode)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L318) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L621) | `GetPlateCornerForces` | Reuse semantics with status/data and dependency review |
| `MemberForces GetPlateCornerForces(ILoadCase loadCase, Plate plate, Node cornerNode, Beam member)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L332) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L638) | `GetPlateCornerForces` | Reuse semantics with status/data and dependency review |
| `Task<MemberForces> GetPlateCornerForcesAsync(ILoadCase loadCase, Plate plate, Node cornerNode, Node otherMemberNode)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L338) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1193) | `GetPlateCornerForces` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `Task<MemberForces> GetPlateCornerForcesAsync(ILoadCase loadCase, Plate plate, Node cornerNode, Beam member)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L344) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1210) | `GetPlateCornerForces` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `MemberForces TransformPlateCornerForcesToMemberLocal(PlateCornerForces plateCornerForces, Node otherMemberNode)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L361) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L655) | Managed composition / transformation | Reuse semantics with status/data and dependency review |
| `MemberForces TransformPlateCornerForcesToMemberLocal(PlateCornerForces plateCornerForces, Beam member)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L375) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L676) | Managed composition / transformation | Reuse semantics with status/data and dependency review |
| `LocalCS GetMemberLocalCoordinateSystem(Beam member)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L381) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L712) | Managed composition / transformation | Reuse semantics with status/data and dependency review |
| `Task<MemberForces> TransformPlateCornerForcesToMemberLocalAsync(PlateCornerForces plateCornerForces, Node otherMemberNode)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L387) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1227) | Managed composition / transformation | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `Task<MemberForces> TransformPlateCornerForcesToMemberLocalAsync(PlateCornerForces plateCornerForces, Beam member)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L393) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1238) | Managed composition / transformation | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `PlateCornerForces GetPlateCornerForcesInMemberLocalCoordinates(ILoadCase loadCase, Plate plate, Node cornerNode, Node otherMemberNode)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L405) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L723) | `GetPlateCornerForces` | Reuse semantics with status/data and dependency review |
| `Task<PlateCornerForces> GetPlateCornerForcesInMemberLocalCoordinatesAsync(ILoadCase loadCase, Plate plate, Node cornerNode, Node otherMemberNode)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L411) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1249) | `GetPlateCornerForces` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `PlateCornerForces TransformPlateCornerForcesToMemberLocalPlateResult(PlateCornerForces plateCornerForces, Node otherMemberNode)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L421) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L743) | Managed composition / transformation | Reuse semantics with status/data and dependency review |
| `Task<PlateCornerForces> TransformPlateCornerForcesToMemberLocalPlateResultAsync(PlateCornerForces plateCornerForces, Node otherMemberNode)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L428) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1269) | Managed composition / transformation | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `bool GetResultantForceAlongLineForPlateList(IEnumerable<Plate> plateList, int loadCaseId, Point3D startPoint, Point3D endPoint, bool transformForceToGlobal, out Vector3D resultantForce, out Vector3D resultantMoment)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L448) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L760) | `GetResultantForceAlongLineForPlateList` | Adapt to a named result; no public reference outputs |
| `Forces GetResultantForceAlongLineForPlateList(IEnumerable<Plate> plateList, int loadCaseId, Point3D startPoint, Point3D endPoint, bool transformForceToGlobal)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L463) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L839) | `GetResultantForceAlongLineForPlateList` | Reuse semantics with status/data and dependency review |
| `Task<Forces> GetResultantForceAlongLineForPlateListAsync(IEnumerable<Plate> plateList, int loadCaseId, Point3D startPoint, Point3D endPoint, bool transformForceToGlobal)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L474) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1287) | `GetResultantForceAlongLineForPlateList` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `PlateCenterForces GetPlateCenterForces(ILoadCase loadCase, Plate plate)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L480) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L862) | `GetAllPlateCenterForces` | Reuse semantics with status/data and dependency review |
| `Task<PlateCenterForces> GetPlateCenterForcesAsync(ILoadCase loadCase, Plate plate)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L481) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1303) | `GetAllPlateCenterForces` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `PlateCenterMoments GetPlateCenterMoments(ILoadCase loadCase, Plate plate)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L482) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L870) | `GetAllPlateCenterMoments` | Reuse semantics with status/data and dependency review |
| `Task<PlateCenterMoments> GetPlateCenterMomentsAsync(ILoadCase loadCase, Plate plate)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L483) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1311) | `GetAllPlateCenterMoments` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `IEnumerable<PlateCenterResults> GetPlateCenterResults(IEnumerable<ILoadCase> loadCases, IEnumerable<Plate> plates)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L484) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L910) | `GetAllPlateCenterForces`, `GetAllPlateCenterMoments`, `GetPlateCenterNormalPrincipalStresses`, `GetPlateCenterVonMisesStresses` | Reuse semantics with status/data and dependency review |
| `IEnumerable<PlateCenterResults> GetPlateCenterResults(IEnumerable<ILoadCase> loadCases, IEnumerable<Plate> plates, int nThreads)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L485) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1374) | `GetAllPlateCenterForces`, `GetAllPlateCenterMoments`, `GetPlateCenterNormalPrincipalStresses`, `GetPlateCenterVonMisesStresses` | Reuse semantics with status/data and dependency review |
| `PlateCenterResults GetPlateCenterResults(ILoadCase loadCase, Plate plate)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L486) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L900) | `GetAllPlateCenterForces`, `GetAllPlateCenterMoments`, `GetPlateCenterNormalPrincipalStresses`, `GetPlateCenterVonMisesStresses` | Reuse semantics with status/data and dependency review |
| `Task<IEnumerable<PlateCenterResults>> GetPlateCenterResultsAsync(IEnumerable<ILoadCase> loadCases, IEnumerable<Plate> plates)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L487) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1351) | `GetAllPlateCenterForces`, `GetAllPlateCenterMoments`, `GetPlateCenterNormalPrincipalStresses`, `GetPlateCenterVonMisesStresses` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `Task<PlateCenterResults> GetPlateCenterResultsAsync(ILoadCase loadCase, Plate plate)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L488) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1341) | `GetAllPlateCenterForces`, `GetAllPlateCenterMoments`, `GetPlateCenterNormalPrincipalStresses`, `GetPlateCenterVonMisesStresses` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `PlateCenterVonMises GetPlateCenterVonMises(ILoadCase loadCase, Plate plate)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L489) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L878) | `GetPlateCenterVonMisesStresses` | Reuse semantics with status/data and dependency review |
| `Task<PlateCenterVonMises> GetPlateCenterVonMisesAsync(ILoadCase loadCase, Plate plate)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L490) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1319) | `GetPlateCenterVonMisesStresses` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `PlatePrincipalStresses GetPlatePrincipalStresses(ILoadCase loadCase, Plate plate)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L491) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L888) | `GetPlateCenterNormalPrincipalStresses` | Reuse semantics with status/data and dependency review |
| `Task<PlatePrincipalStresses> GetPlatePrincipalStressesAsync(ILoadCase loadCase, Plate plate)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L492) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1329) | `GetPlateCenterNormalPrincipalStresses` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `Task<StaticCheckResult> GetStaticCheckResultAsync(int loadCaseId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L493) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L980) | `GetStaticCheckResult` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `HashSet<StaticCheckResult> GetStaticCheckResults(IEnumerable<int> loadCaseIds)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L494) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L82) | `GetStaticCheckResult` | Reuse semantics with status/data and dependency review |
| `HashSet<StaticCheckResult> GetStaticCheckResults(IEnumerable<int> loadCaseIds, int nThreads)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L495) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1385) | `GetStaticCheckResult` | Reuse semantics with status/data and dependency review |
| `Task<HashSet<StaticCheckResult>> GetStaticCheckResultsAsync(IEnumerable<int> loadCaseIds)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L496) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L990) | `GetStaticCheckResult` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `LoadCaseForces GetSupportForces(int supportId, int loadCaseId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L497) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L59) | `GetSupportReactions` | Reuse semantics with status/data and dependency review |
| `Task<LoadCaseForces> GetSupportForcesAsync(int supportId, int loadCaseId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L498) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L957) | `GetSupportReactions` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `LoadCaseForces GetSupportReactions(int supportId, int loadCaseId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L499) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L67) | `GetSupportReactions` | Reuse semantics with status/data and dependency review |
| `Task<LoadCaseForces> GetSupportReactionsAsync(int supportId, int loadCaseId)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L500) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L965) | `GetSupportReactions` | Rework execution ownership; do not copy Task.Run as a COM guarantee |
| `PlateStressResult GetPlateStressAtPoint(int pId, int lcId, Node stressPoint, Node facingPoint)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs#L502) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1927) | `GetPlateStressAtPoint` | Reuse semantics with status/data and dependency review |

### Reference private helpers

The adapter contains 24 private methods for validation, hydration, batch identity, native status/buffer checks and coordinate construction. Reuse and extract them into coherent internal Output services where suitable; they are not new public native APIs.

| Private method | Source | Purpose visible in source |
| --- | --- | --- |
| `void ValidatePlateCornerIdentityArguments(ILoadCase loadCase, Plate plate, Node cornerNode)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1407) | ValidatePlateCornerIdentityArguments |
| `void ValidatePlateCornerArguments(ILoadCase loadCase, Plate plate, Node cornerNode)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1426) | ValidatePlateCornerArguments |
| `void ValidateHydrationArguments(int loadCaseId, int plateId, int cornerNodeId, IOSGeometry geometry, IOSLoad load)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1440) | ValidateHydrationArguments |
| `void ValidateHydrationAdaptersAndGeometryIds(int plateId, int cornerNodeId, IOSGeometry geometry, IOSLoad load)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1453) | ValidateHydrationAdaptersAndGeometryIds |
| `bool IsPlateGeometryPopulated(Plate plate)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1469) | IsPlateGeometryPopulated |
| `Plate ResolvePlate(IOSGeometry geometry, int plateId)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1474) | ResolvePlate |
| `Node ResolvePlateCorner(Plate plate, int cornerNodeId)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1494) | ResolvePlateCorner |
| `List<Node> GetUniquePlateCorners(Plate plate)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1520) | GetUniquePlateCorners |
| `List<ILoadCase> MaterializeLoadCases(IEnumerable<ILoadCase> loadCases)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1547) | MaterializeLoadCases |
| `List<Plate> MaterializePlates(IEnumerable<Plate> plates)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1568) | MaterializePlates |
| `List<PlateCornerForcesRequest> MaterializeUniqueRequests(IEnumerable<PlateCornerForcesRequest> requests)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1591) | MaterializeUniqueRequests |
| `List<ILoadCase> ResolveLoadCases(IOSLoad load, IEnumerable<int> loadCaseIds)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1614) | ResolveLoadCases |
| `void ValidateCornerBelongsToPlate(Plate plate, Node cornerNode)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1666) | ValidateCornerBelongsToPlate |
| `bool AreSameNode(Node first, Node second)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1682) | AreSameNode |
| `string CreatePlateCornerFailureMessage(int loadCaseId, int plateId, int cornerNodeId)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1694) | CreatePlateCornerFailureMessage |
| `bool IsFinite(Point3D point)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1701) | IsFinite |
| `bool TryReadComSuccess(object callResult, out bool succeeded)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1709) | TryReadComSuccess |
| `bool TryReadFirstSixComponentResult(object value, out double[] result)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1724) | TryReadFirstSixComponentResult |
| `bool ReadComSuccess(object callResult)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1770) | ReadComSuccess |
| `double[] ReadPlateCornerForceArray(object value)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1793) | ReadPlateCornerForceArray |
| `void ValidatePlateCornerForcesForTransformation(PlateCornerForces plateCornerForces)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1835) | ValidatePlateCornerForcesForTransformation |
| `MemberForces CreateMemberForcesFromPlateCornerForces(PlateCornerForces plateCornerForces, LocalCS memberLocal, int beamId, MemberEnd memberEnd)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1849) | CreateMemberForcesFromPlateCornerForces |
| `LocalCS CreateMemberLocalCoordinateSystem(Beam member)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1871) | CreateMemberLocalCoordinateSystem |
| `LocalCS CreateZeroBetaMemberLocalCoordinateSystem(Node startNode, Node endNode)` | [Definition](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs#L1886) | CreateZeroBetaMemberLocalCoordinateSystem |

## 6. Reuse inventory and dependency review

| Existing item | Reusable behavior | Required adaptation |
| --- | --- | --- |
| Current Forces / MemberForces / IForces | Existing six-component data, algebra and unit abstractions. | Extend only missing validated helpers; do not duplicate force models or import private Shared/Units projects. |
| Current RootMemberForcesService and Root result classes | Strict Boolean decoding, six-Double buffers, nonzero lower bounds, finite/sentinel validation, ordered identified outcomes. | Extract a minimal shared internal reader where justified; preserve the published Root API and return types. |
| Source StaticCheckResult | Load/reaction records and residual calculations. | Wrap query status; copy qualified buffers; ensure residual sign conventions; reuse current property-store primitives. |
| Source PlateCenterForces / Moments / VonMises / PrincipalStresses / Results | Named result families and aggregate composition. | Preserve stress versus force and moment-per-width dimensions; adapt Base/Shared/Units dependencies; retain per-subquery failure. |
| Source PlateCornerForces and request/key/result-set types | Identity, incidence validation, hydration, deterministic sparse/Cartesian batches, indexes and moment transfer. | Membership is read-only but domain objects remain mutable; freeze identity or snapshot keys. Key struct is justified by small immutable composite identity; use classes for result payloads. Replace public out lookup forms for new contracts. |
| Source OffsetAwareMemberEndForces | Auditable member-face and physical-joint forces, geometry and global offset. | GetBeamOffsetSpecification is absent from current Property placeholder; no silent zero-offset substitution. Require property port or explicit validated offset request. |
| Source LocalCS and coordinate helpers | Member incidence/beta transformations, zero-beta special cases. | Verify Y-up/Z-up and beta conventions; GlobalY assumptions in source helper are a review gate; use pure managed snapshot math after extraction. |
| Source PlateStressResult | Named 32-slot stress accessors. | Own cloned immutable storage; current source Raw returns the original mutable array, and source extraction turns null elements into zero. Reject unqualified/unwritten values. |
| Source MemberSteelDesignResults / DesignStatus | Steel result identity, governing case/code/clause/section, ratio and slenderness. | Source has a status TODO and string casts; add Unknown/raw-status retention and distinguish missing governing metadata. |
| Source PlateStressSampler and contour helpers | Sampling, grids and presentation/export built on point queries. | Optional later feature, outside native Output-function count; review geometry, WPF/rendering and COM execution dependencies separately. |

## 7. Reproducibility evidence

File fingerprints identify the exact reviewed installed documentation and source snapshots. A byte hash change alone is not an API change; Doxygen generator/header/style differences were excluded from contract classification. No Bentley binary or main-project source was copied into production.

| Year | Page | Build | Output summary / detail count | Bytes | SHA-256 |
| --- | --- | --- | --- | --- | --- |
| 2024 | [group___ana_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___ana_rlt.html) | `24.00.02.354` | 1 / 1 | 10579 | `69c0d062a88962d84b7ad11c00d6780030772a6d44c00956450461f099da3cf3` |
| 2024 | [group___design_steel.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___design_steel.html) | `24.00.02.354` | 11 / 11 | 122127 | `cbb6c514c7619d96bdfb37681af8e848d73607a345da670ef183e373d98297d0` |
| 2024 | [group___nod_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___nod_rlt.html) | `24.00.02.354` | 4 / 4 | 28464 | `979ab186f92341dc16e36f9446c42ca6194d4df565048181ee624548648e19cb` |
| 2024 | [group___mem_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_rlt.html) | `24.00.02.354` | 12 / 12 | 76031 | `351caaffc83bbb0db11960982cce04a03ee993470aececbeccb9953bf8e44292` |
| 2024 | [group___plate_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_rlt.html) | `24.00.02.354` | 11 / 11 | 82417 | `6145d3b297d25d8c59f55b0e217ba54e3c78d021d679240019c5337fcac35616` |
| 2024 | [group___solid_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_rlt.html) | `24.00.02.354` | 4 / 4 | 26793 | `3e878c1e7294cf1bba40ad865f65f25f0639f18c101bf4e299ce13b632d5eb90` |
| 2024 | [group___dynamic_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___dynamic_rlt.html) | `24.00.02.354` | 11 / 11 | 67801 | `c125babff2915bea20a22c0ab20a7b0b2bbc839d6d1cd2cba92b55a62b529793` |
| 2024 | [group___static_check.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___static_check.html) | `24.00.02.354` | 1 / 1 | 10273 | `71c9db60dc158797e042ddbf91268d4e685ca0da5d5501eed92a2cbe720eb8ea` |
| 2024 | [group___buckling_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___buckling_rlt.html) | `24.00.02.354` | 4 / 4 | 17469 | `38a150d56a0e29d63b09b67755736b6aa443391511dc35335b29010d88af0f00` |
| 2024 | [group___chinese_steel.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___chinese_steel.html) | `24.00.02.354` | 0 / 0 | 140288 | `d37ecc3bcf5de50c635dee08a9b3e278fccb57dd8c606c16f5b90fc86669e6f0` |
| 2025 | [group___ana_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___ana_rlt.html) | `25.00.01.424` | 1 / 1 | 9585 | `b3e50d55031b0f5cd26646fd693cb3f025b5238050885eee90db4755427a8350` |
| 2025 | [group___design_steel.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___design_steel.html) | `25.00.01.424` | 11 / 11 | 129007 | `5e32a0960a7c1b90781e1533a3d9744d166f1c53bd68c4abe4b896fe58d28bfe` |
| 2025 | [group___nod_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___nod_rlt.html) | `25.00.01.424` | 4 / 4 | 25314 | `8ee79df65973a2d7d5473006be34f0c3d6c96fd834432c184e0bcdfc5eaa1cb9` |
| 2025 | [group___mem_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_rlt.html) | `25.00.01.424` | 12 / 12 | 67879 | `6d9af28ab9eecdaec3e9f47f2073fe6db185d1062adf37b322e436e4149fe7cd` |
| 2025 | [group___plate_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_rlt.html) | `25.00.01.424` | 11 / 11 | 80598 | `a70c6e081cec64187e7b71010cef9a9136023d1e335ea3e0f46a1e2efd1c67a3` |
| 2025 | [group___solid_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_rlt.html) | `25.00.01.424` | 4 / 4 | 24080 | `4e59f4d923f8fd73fce5ec55098b2fe33558d283eb4498f01d5c630dbbbde166` |
| 2025 | [group___dynamic_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___dynamic_rlt.html) | `25.00.01.424` | 12 / 12 | 75108 | `2333a092db14c137b2f9636fb3bc317fc8e085df8d5ec31806494c041ff4eeae` |
| 2025 | [group___static_check.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___static_check.html) | `25.00.01.424` | 1 / 1 | 9402 | `a31b1bb6c4ae2e57ee9bae5555e5261810e45ab1a6312e20b03b9b9d922ca7e8` |
| 2025 | [group___buckling_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___buckling_rlt.html) | `25.00.01.424` | 4 / 4 | 16032 | `31504254abf4beb89167497a87755b62ad0fb40e3a3d1c250cbdb04c0046c160` |
| 2025 | [group___chinese_steel.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___chinese_steel.html) | `25.00.01.424` | 0 / 0 | 236753 | `7f96d2b8a997eea83e08253ee032e23599fc009584831b4e0a5841a0bec923b2` |
| 2026 | [group___ana_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___ana_rlt.html) | `26.00.00.340` | 1 / 1 | 9585 | `09e3b54212e5f2369f999ce21eb5d8f46496f8c2b00d52b327c67f5dbcff737f` |
| 2026 | [group___design_steel.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___design_steel.html) | `26.00.00.340` | 11 / 11 | 129007 | `f179af4eb90bd78f772aaa3fd75318f57138522b1636db334056972b752045a4` |
| 2026 | [group___nod_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___nod_rlt.html) | `26.00.00.340` | 4 / 4 | 25314 | `51d565e537ae1e4c3dd3f3027321c84058d27383cd0599b3774761f82e850afc` |
| 2026 | [group___mem_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_rlt.html) | `26.00.00.340` | 12 / 12 | 67879 | `d65ed56efb4ca3c44d0d3cb047cf832f8f62b8b82466a7b66dd4d7c171fa184b` |
| 2026 | [group___plate_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_rlt.html) | `26.00.00.340` | 11 / 11 | 80598 | `d606636ef8c35960bc45834a3477a649da9737205ef85c5ba3945a132d0175bd` |
| 2026 | [group___solid_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_rlt.html) | `26.00.00.340` | 4 / 4 | 24080 | `d0297ce0ee520030493f80fef0d2fedc8b78b2244d8568b4defb443fda5ac9e7` |
| 2026 | [group___dynamic_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___dynamic_rlt.html) | `26.00.00.340` | 12 / 12 | 75108 | `a4a45db362d04b29f25e52cd9b87026b264b5b8d00976414f84964015a2c4910` |
| 2026 | [group___static_check.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___static_check.html) | `26.00.00.340` | 1 / 1 | 9402 | `936971647df9cd02a35d14e70d5c7c5fcf944ed230705139521d3bb38227cd48` |
| 2026 | [group___buckling_rlt.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___buckling_rlt.html) | `26.00.00.340` | 4 / 4 | 16032 | `fc7ffe392a0aa72412004da6f0293b6d1aa349e7fb58effd8a652887156f5f0c` |
| 2026 | [group___chinese_steel.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___chinese_steel.html) | `26.00.00.340` | 0 / 0 | 236753 | `cc3a4c98cbc1eeaa0f354b45df4cef2c2ec9b75eb16c9a272f02320403d11985` |

### Supplemental native class listings

| Year | Class page | Listed names | SHA-256 |
| --- | --- | --- | --- |
| 2024 | [Native Output class](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/class_o_s_output_u_i.html) | 74 | `1161c70a965858f0a3ab7d83658442c418e99db98486973f6a4f5eada227dbaa` |
| 2025 | [Native Output class](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/class_o_s_output_u_i.html) | 75 | `0e9e6b4f0c33967e4841e52a32083768a7019b89213a5cf4b78b9c8c109498f2` |
| 2026 | [Native Output class](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/class_o_s_output_u_i.html) | 75 | `05d14369bcc5c6216d7a54b0593d50ced5995536865d88e9d4cbcdfb363bb895` |

### Generated native header evidence

| Year | Header | GetMaxBeamStresses declaration present | SHA-256 |
| --- | --- | --- | --- |
| 2024 | [Generated Output header](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/osoutputui_8h_source.html) | No | `fed442af20bbec6089e3b7370774f80cc902d61521fdfe29b4e767e07aff9c96` |
| 2025 | [Generated Output header](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/osoutputui_8h_source.html) | Yes | `1aefde6852d37601a2d78b8b6a340387d22397ad4f22612a0deac6ae20fe5512` |
| 2026 | [Generated Output header](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/osoutputui_8h_source.html) | Yes | `a30c32770012c6097fd1453f2bc1f2de80c232e3f67f9ebfcd387b3dae436b22` |

### Installed interop assemblies

| Year | Assembly | Methods | SHA-256 |
| --- | --- | --- | --- |
| 2025 | [Installed interop assembly](file:///C:/Program%20Files/Bentley/Engineering/STAAD.Pro%202025/STAAD/StaadPro.Interop.dll) | 72 | `CC724993AC9119138B7323E8C684997257E2851DE62E1551BA5D6D67A9DADC19` |
| 2026 | [Installed interop assembly](file:///C:/Program%20Files/Bentley/Engineering/STAAD.Pro%202026/STAAD/StaadPro.Interop.dll) | 72 | `6D153E80ED356A1B167ABB2EF19227FCFF510707A578AD304154652CC6C1F6D4` |

Both assembly identities are `StaadPro.Interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b4d201f8319cef86`. The passive inspection ran in separate Windows PowerShell processes to avoid assembly-identity unification; no COM activation occurred. No corresponding 2024 vendor interop assembly was found at the expected STAAD subfolder.

### Current source fingerprints

| Source | Bytes | SHA-256 |
| --- | --- | --- |
| [IOSOutput.cs](../../src/StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs) | 605 | `bf16e384858875ab718bd58daa5bc8a6b69f84a26dd78ba19776beda7d6e8366` |
| [OSOutputAdapter.cs](../../src/StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs) | 1245 | `9871341acede230cbd4197e1b0a2f551578beaad8432c9cfce0b0da05393fd3d` |
| [OpenStaadWrapper.cs](../../src/StaadPro.Interop/Models/OpenStaadWrapper.cs) | 21039 | `1c0b69fe545a379262ad70cb1d1f9c45de872aba92e6eb4df218441455628692` |
| [RootMemberForcesService.cs](../../src/StaadPro.Interop/Services/Root/Analysis/RootMemberForcesService.cs) | 4436 | `53f374a6b497f66d49d8d1ae59d61505274f5c3c582b55ecf6f9022e50beec92` |
| [MemberForcesResult.cs](../../src/StaadPro.Interop/Models/Root/MemberForcesResult.cs) | 2208 | `ad876d0290a43337013b1347a44268a70c95cbd6068db1f02cf626157b6ba33e` |
| [StaadPro.Interop.csproj](../../src/StaadPro.Interop/StaadPro.Interop.csproj) | 5056 | `035570ef3e35b52a2772e2ec96b9bf886ce3d08d6e555c3d51ffd870de9f1a59` |

### Read-only main-source fingerprints

| Source | Bytes | SHA-256 |
| --- | --- | --- |
| [Adapters/Interfaces/IOSOutput.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSOutput.cs) | 32143 | `f29278e63847e802794e9008b23adba83f052312eb8b584af3a310edb790a882` |
| [Adapters/Models/OSOutputAdapter.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OSOutputAdapter.cs) | 76913 | `f74bc58b86229cefa8761e0316f5b3b23666200b86834ae0b227ffc07061ab72` |
| [Models/OpenStaadWrapper.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OpenStaadWrapper.cs) | 20273 | `83737733d57a672313385f99ea0ca8ffa10f1917fb6a33b89e43ce27e3f37b14` |
| [Models/Forces.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/Forces.cs) | 4451 | `dbbe4a83c0655004d23f4e82327c3752f64da731091a9d65cd7eed2d10b6a218` |
| [Entities/MemberForces.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/MemberForces.cs) | 1267 | `0fbe3e9391c43ab1f773d7c8a3f7a35d3149ca911cf8c71b1a09e09e593d7804` |
| [Models/StaticCheckResult.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/StaticCheckResult.cs) | 1569 | `6477c5445df62dd5c27cb57459b47f0479260e11ecab356613fe7b7b1d96485c` |
| [Models/PlateCornerForces.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/PlateCornerForces.cs) | 10217 | `b02972217cd377c30a3b455e1d0fd00a38c8eed699a94641e6efbb4f46963554` |
| [Models/PlateCornerForcesRequest.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/PlateCornerForcesRequest.cs) | 2342 | `b47a5530017e0a4accacb545c4371f6afc17c0e9a64869ab711355bc8b674f6d` |
| [Models/PlateCornerForcesKey.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/PlateCornerForcesKey.cs) | 3329 | `c99d3b630e9ba6f7f3edb23d1db02b2deaf57c4fd192603c92584f1df19eb11a` |
| [Models/PlateCornerForcesResultSet.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/PlateCornerForcesResultSet.cs) | 8695 | `09767984d85efea3295a93a2c5031347e4e9e6ffba45fa17364fe0974648c2e8` |
| [Models/OffsetAwareMemberEndForces.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OffsetAwareMemberEndForces.cs) | 749 | `f2a4d47299955163fb685af63d75fae70b8e2f2be340f627913b396e9a0c0dae` |
| [Models/PlateCenterForces.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/PlateCenterForces.cs) | 2391 | `8bf80b281c216584e443bda1a775c2c306108d370a49d3fae70cff5e017f11d9` |
| [Models/PlateCenterMoments.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/PlateCenterMoments.cs) | 2405 | `06c7f77ab6bca37f0159577768a3ef0694e56d1694777d0d066cb98d752d575e` |
| [Models/PlateCenterVonMises.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/PlateCenterVonMises.cs) | 2203 | `32389d570b29f557b7c6d4823ed1b6ee9adaf3bad06c077c4b85437c6dc0b94b` |
| [Models/PlatePrincipalStresses.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/PlatePrincipalStresses.cs) | 2164 | `956b6a2f99749c2bd05eeb0028a6340c957cf000d67da023310074ba00194e67` |
| [Models/PlateCenterResults.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/PlateCenterResults.cs) | 2962 | `d1c54d66881163b4b3011d00eb7265dbc4d141a02d78a2de2a76bda9b572aef3` |
| [Models/PlateStressResult.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/PlateStressResult.cs) | 1891 | `1bc39b12e393ed49a9cab6e30e43ceed8b0e3e095904329f1349694801c498ad` |
| [Models/CoordinateSystems/LocalCS.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/CoordinateSystems/LocalCS.cs) | 20996 | `25374b7d7d73d2d6d71958850e72a4065fd2945203ef25d48617c8f4a4d7e21e` |
| [Helpers/CoordinateSystemHelpers.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Helpers/CoordinateSystemHelpers.cs) | 1345 | `513be8460b719f5503328a19174f6fd54436fc827be8a25ed91562f813d2931a` |
| [Services/PlateStressSampler.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/PlateStressSampler.cs) | 15470 | `112196fb98f30fcad23ff636b0dd4a9e14218e14055342b96d0a319126a843a1` |
| [Entities/MemberSteelDesignResults.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/MemberSteelDesignResults.cs) | 4887 | `65734eee63d2782717f247affe743b86e68a23034558f450b26f06fec297b4a5` |
| [Adapters/Interfaces/IOSProperty.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOSProperty.cs) | 28352 | `7b4184fb1e7f25982a1e3c5225affbad1678fb9b0f8af161aafa33a0140c0040` |
| [Enums/OffsetLocation.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Enums/OffsetLocation.cs) | 137 | `36e85b5dba41d5e18aed9183478c9d3e29f8e4b525268838e3078e9db6fce5e7` |
| [Enums/OffsetAxis.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Enums/OffsetAxis.cs) | 136 | `e2b4f9d803b10f60017a6fc6e6c291c61fe3e3d3749646c982a211f5cbec0d03` |
| [Entities/OffsetSpecification.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Entities/OffsetSpecification.cs) | 3714 | `be92850983fc2765b233eaa2b9d55f386805cd48edc5c90a9f32c25f8ac0fd95` |
| [Base/PlateStress.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Base/PlateStress.cs) | 602 | `c0f9167c1bdfe4943811d35e5daec0deb317286e413e6a76edf1a82579af4050` |

## 8. Review limits

This review parsed every scoped native declaration, parameter table and return contract in the installed help, compared all common signatures/non-example text/examples, passively compared vendor interface metadata and inspected the current and private source. No native result extraction was executed. No Output function, version-policy implementation, library target, package or release metadata was changed. Offline parsing and evidence reconciliation cannot certify COM marshaling, engine availability, units or engineering correctness.

Local-help and main-source links require those files on the reader’s computer. The repository remains readable without them through the contract inventory and machine-readable evidence; external contributors will need their own licensed installation for native verification.
