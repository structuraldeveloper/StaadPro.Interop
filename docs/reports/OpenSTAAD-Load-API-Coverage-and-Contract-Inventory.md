# OpenSTAAD Load API coverage and contract inventory

**Review date:** 22 October 2026
**Baseline:** main at 1483c9c
**Status:** Original native evidence and API design proposals, updated with all 30 originally pending reference-overload implementations. Broader named-result architecture remains proposed.

This annex supports the [Load multi-version architecture and plan](OpenSTAAD-Load-Multi-Version-Architecture-and-Plan.md). It inventories every detailed Load function from the installed 2024/2025/2026 help and distinguishes current native dispatches from planned additions.

## Current source — 15 Load inspection queries

Implemented all 15 inspection/contribution queries selected after the semantic audit. IOSLoad and OSLoadAdapter now expose **135 overloads** (115 reference, five earlier conveniences and 15 native expansions). Documented native coverage is **88/129**: 67 direct adapter targets, 15 additional common-service targets and six member-entity targets. The **41** unused native names comprise one covered alternative, one derived capability, four partial overlaps and **35 absent operations**; 39 native contracts retain outstanding scope, plus three separate metadata-only candidates.

Passed **2,448 offline tests**, including **189 new cases**, and all **30 new XML examples** on source consumers. See [Load inspection contracts and examples](../Load-Inspection-Queries.md) for status/data results, primary activation, index bases, buffer limits, ambiguity and native qualification requirements. The prepared 20261023 archive predates these source additions. Earlier audit/reference-port counts below are historical baselines.

## Historical managed capability audit — 23 October 2026

The **56 native names without dispatch are not 56 missing managed features**. A semantic review found **one covered alternative** (primary creation), **one derived capability** (combination membership), **four partial overlaps**, and **50 native operations without an implemented managed equivalent**. Consequently 54 native contracts still have outstanding scope, but they do not necessarily require 54 separate public methods. No production code changed in this audit. See the [complete managed capability audit](OpenSTAAD-Load-Managed-Capability-Audit.md) for all 56 dispositions, source evidence and a revised proposed next 15.

## 1. Complete documented native inventory

There are **129 distinct native functions in each year**, all with detailed entries. The native summary and detailed declarations match across product years after formatting normalization. Parameter identifiers can differ between a summary, a detailed prototype, its parameter table and vendor interop metadata; those differences are preserved rather than treated as additional overloads.

Each entry includes the common native declaration, positional parameters, documented directions, inspected 2025/2026 CLR shape, return/error information and current source callers. C++ FAR/const reference notation must not be mechanically translated into public C# ref parameters. IsIn/IsOut flags in inspected metadata are often both false; Object& still records a CLR by-reference parameter.

**S:** shared internal inspection-service dispatch. **D:** direct adapter dispatch. **E:** member-load entity dispatch reached through AddMemberLoad. **N:** neither exact dispatch form; it does not by itself mean a missing managed capability. The 88 dispatch-covered names include unresolved shape/status concerns described in the primary report. The remaining 41 N entries include historical proposed managed signatures and function-specific considerations; the semantic audit identifies alternatives/compositions that should not be duplicated solely for native-name coverage, and these signatures are design proposals, not compiled APIs. All named request/result classes in those proposals that are absent today are pending.

Return tables below preserve functional error labels and short native explanations. A missing return table does not imply void: some entries describe the return in prose. Follow the linked reference and the declared return type. Native counts, IDs, Boolean values and zero/one success codes must not share a universal converter.

Parameter detail excerpts identify meaning and link back to full code/array tables. Long engineering tables are summarized by an excerpt; the primary report analyzes changed seismic encodings and shared array ambiguities. No default is implied when the native declaration does not specify one.

### Load

#### AddRSLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d)

**Comparison:** Common. **Coverage:** D — adapter dispatch (reference overload port).

```cpp
VARIANT OSLoadUI::AddRSLoad(
    const VARIANT FAR&varType,
    VARIANT FAR&varFactArray,
    const VARIANT FAR&varAccOrDis,
    const VARIANT FAR&varScale,
    const VARIANT FAR&varDampType,
    const VARIANT FAR&varDampFact,
    const VARIANT FAR&varLinOrLog,
    const VARIANT FAR&varMis,
    const VARIANT FAR&varMisFact,
    const VARIANT FAR&varZpa,
    const VARIANT FAR&varZpaFact,
    const VARIANT FAR&varFf1,
    const VARIANT FAR&varFf1Fact,
    const VARIANT FAR&varFf2,
    const VARIANT FAR&varFf2Fact,
    const VARIANT FAR&varSaveFlag,
    const VARIANT FAR&varPairs,
    VARIANT FAR&varDispOrAccSet,
    VARIANT FAR&varVals
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varType` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varFactArray` | `VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varAccOrDis` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varScale` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varDampType` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `varDampFact` | `const VARIANT FAR&` | [in] | `System.Object` |
| 7 | `varLinOrLog` | `const VARIANT FAR&` | [in] | `System.Object` |
| 8 | `varMis` | `const VARIANT FAR&` | [in] | `System.Object` |
| 9 | `varMisFact` | `const VARIANT FAR&` | [in] | `System.Object` |
| 10 | `varZpa` | `const VARIANT FAR&` | [in] | `System.Object` |
| 11 | `varZpaFact` | `const VARIANT FAR&` | [in] | `System.Object` |
| 12 | `varFf1` | `const VARIANT FAR&` | [in] | `System.Object` |
| 13 | `varFf1Fact` | `const VARIANT FAR&` | [in] | `System.Object` |
| 14 | `varFf2` | `const VARIANT FAR&` | [in] | `System.Object` |
| 15 | `varFf2Fact` | `const VARIANT FAR&` | [in] | `System.Object` |
| 16 | `varSaveFlag` | `const VARIANT FAR&` | [in] | `System.Object` |
| 17 | `varPairs` | `const VARIANT FAR&` | [in] | `System.Object` |
| 18 | `varDispOrAccSet` | `VARIANT FAR&` | [in] | `System.Object` |
| 19 | `varVals` | `VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varType` | [in] | Response Spectrum Load type(1=Srss, 2=Cqc, 3=Absolute, 4=Asce, 5=Ten, 6=Csm)(Type: Long/Integer). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) |
| `varFactArray` | [in] | factor Array.(Type: float Array) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) |
| `varAccOrDis` | [in] | Acceleration(1) or Displacement(0)(Type: Integer) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) |
| `varScale` | [in] | Scale(Type: float) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) |
| `varDampType` | [in] | Damp Type(1=DAMP, 2=CDAMP, 3 = MDAMP)(Type: Integer) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) |
| `varDampFact` | [in] | Damp Factor(Type: float) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) |
| `varLinOrLog` | [in] | Interpolation Type Logarithmic(1) or Linear(0)(Type: Integer) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) |
| `varMis` | [in] | Missing Mass(1 for checked, 0 unchecked)(Type: Integer) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) |
| `varMisFact` | [in] | Missing Mass Factor(Type: float) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) |
| `varZpa` | [in] | ZPA(1 for checked, 0 unchecked)(Type: Integer) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) |
| `varZpaFact` | [in] | ZPA Factor(Type: float) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) |
| `varFf1` | [in] | Ff1(1 for checked, 0 unchecked)(Type: Integer) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) |
| `varFf1Fact` | [in] | Ff1 Factor(Type: float) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) |
| `varFf2` | [in] | Ff2(1 for checked, 0 unchecked)(Type: Integer) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) |
| `varFf2Fact` | [in] | Ff2 Factor(Type: float) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) |
| `varSaveFlag` | [in] | Save Flag(1 for checked, 0 unchecked)(Type: Integer) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) |
| `varPairs` | [in] | Disp Or Acc Set pair count(Type: Integer) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) |
| `varDispOrAccSet` | [in] | Disp or Acc Array.(Type: float Array) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) |
| `varVals` | [in] | value Array.(Type: float Array) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `1(TRUE)` | Add Response Spectrum Load8 Successful. |
| `0(FALSE)` | Generate Error. |

**Further named-result API proposal (not implemented by the reference-signature port):**

```csharp
LoadOperationResult AddResponseSpectrumLoad(ResponseSpectrumLoadParameters parameters);
```

**Implementation consideration:** Keep 19 native slots internal; snapshot all arrays, validate spectrum pair count and distinguish input buffers from returned/replaced buffers.


**Ported reference overloads:** `bool AddRSLoad(ResponseSpectrumLoadParameters parameters)`.

**Current call sites:** [AddRSLoad](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L167).

#### SplitLoadsOnBeam

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___load_group.html#gaa4ef18bf50db77e41cc496a915955be4) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___load_group.html#gaa4ef18bf50db77e41cc496a915955be4) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#gaa4ef18bf50db77e41cc496a915955be4)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L7.

**Managed capability:** Absent. No implementation transfers/splits load definitions when a member is split. Geometry splitting and ordinary member-load assignment are not equivalent.

```cpp
VARIANT OSLoadUI::SplitLoadsOnBeam(
    const VARIANT FAR&varBeamOld,
    const VARIANT FAR&varBeamNew
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varBeamOld` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |
| 2 | `varBeamNew` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varLoadNo` | [in] | The Reference Load No(Type: Integer/Long). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#gaa4ef18bf50db77e41cc496a915955be4) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `1(TRUE)` | Successful. |
| `0(FALSE)` | Generate Error. |

**Proposed managed entry:**

```csharp
LoadOperationResult SplitLoadsOnBeam(int originalBeamId, int newBeamId);
```

**Implementation consideration:** Verify beam topology and native splitting semantics; do not assume a geometry split and a load split are atomic.

### Load: Definition

No native function declarations occur in this reviewed grouping page. Subgroups, where present, were traversed separately.

### Load: Load Case Details

#### GetPrimaryLoadCaseCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_case.html#ga8c4ac07c10d5cfd0a2bda5d3f837db24) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_case.html#ga8c4ac07c10d5cfd0a2bda5d3f837db24) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_case.html#ga8c4ac07c10d5cfd0a2bda5d3f837db24)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetPrimaryLoadCaseCount();
```

No native parameters.

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

No return-value table is present. Read the return prose in the linked reference; do not infer Boolean success or invent a payload.

**Current call sites:** [ReadAllLoadCases — line 2349](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2349).

#### SetLoadActive

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_case.html#ga065a2faa02e196d4ba47854653a7946b) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_case.html#ga065a2faa02e196d4ba47854653a7946b) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_case.html#ga065a2faa02e196d4ba47854653a7946b)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::SetLoadActive(
    const VARIANT FAR&varLoadNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varLoadNo` | `const VARIANT FAR&` | Not stated | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varLoadNo` | Not stated | The load case reference number ID (type - Long/Integer). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_case.html#ga065a2faa02e196d4ba47854653a7946b) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `TRUE` | OK. |
| `FALSE` | General error. |

**Current call sites:** [SetLoadCaseActive — line 1853](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1853); [ActivateQueryCase — line 2433](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2433).

### Load: Load Envelopes

#### CreateLoadEnvelop

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_enve.html#ga9f28063ba9f67609d5489f069da32154) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_enve.html#ga9f28063ba9f67609d5489f069da32154) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#ga9f28063ba9f67609d5489f069da32154)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::CreateLoadEnvelop(
    const VARIANT FAR&varEnvNo,
    const VARIANT FAR&varEnvType,
    const VARIANT FAR&varLoadCaseList
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varEnvNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varEnvType` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varLoadCaseList` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varEnvNo` | [in] | Load Envelop reference ID(Type: Integer/Long). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#ga9f28063ba9f67609d5489f069da32154) |
| `varEnvType` | [in] | Type of the load envelop(Type: Integer/Long): Value Load Envelop Type 0 NONE 1 STRESS 2 SERVICEABILITY 3 COLUMN 4 CONNECTION 5 STRENGTH 6 TEMPORARY [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#ga9f28063ba9f67609d5489f069da32154) |
| `varLoadCaseList` | [in] | (Primary) load case(s) reference ID(s) VARIANT array(Type: Integer/Long or Integer/Long Array). For additional information, please refer to TR.40 (TR.40 Load Envelope) of the Technical Reference manual. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#ga9f28063ba9f67609d5489f069da32154) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `1/True` | OK. |
| `0/False` | General error. |

**Current call sites:** [CreateLoadEnvelopExt — line 2265](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2265).

#### AddLoadCasesToEnvelop

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_enve.html#ga16a1d26c9c356a3c96cf81d43fac81fa) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_enve.html#ga16a1d26c9c356a3c96cf81d43fac81fa) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#ga16a1d26c9c356a3c96cf81d43fac81fa)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L5.

**Managed capability:** Absent. Envelope creation and membership read-back exist; adding members to an existing envelope is not implemented. Recreating an envelope is not a verified equivalent.

```cpp
VARIANT OSLoadUI::AddLoadCasesToEnvelop(
    const VARIANT FAR&varEnvNo,
    const VARIANT FAR&varLoadCaseList
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varEnvNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varLoadCaseList` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varEnvNo` | [in] | Load Envelop reference ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#ga16a1d26c9c356a3c96cf81d43fac81fa) |
| `varLoadCaseList` | [in] | (Primary) load case(s) reference ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#ga16a1d26c9c356a3c96cf81d43fac81fa) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `1` | OK. |
| `0` | General error. |

**Proposed managed entry:**

```csharp
LoadOperationResult AddCasesToEnvelope(int envelopeId, IEnumerable<int> loadCaseIds);
```

**Implementation consideration:** Validate positive IDs; preserve membership order; native 1 means success.

#### RemoveLoadCasesFromEnvelop

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_enve.html#gac630898fc921f930e89fa9f8701ea9ba) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_enve.html#gac630898fc921f930e89fa9f8701ea9ba) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#gac630898fc921f930e89fa9f8701ea9ba)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L5.

**Managed capability:** Absent. No removal of selected members from an existing envelope. Creation/read-back cannot establish safe in-place removal.

```cpp
VARIANT OSLoadUI::RemoveLoadCasesFromEnvelop(
    const VARIANT FAR&varEnvNo,
    const VARIANT FAR&varLoadCaseList
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varEnvNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varLoadCaseList` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varEnvNo` | [in] | Load Envelop reference ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#gac630898fc921f930e89fa9f8701ea9ba) |
| `varLoadCaseList` | [in] | (Primary) load case(s) reference ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#gac630898fc921f930e89fa9f8701ea9ba) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `1` | OK. |
| `0` | General error. |

**Proposed managed entry:**

```csharp
LoadOperationResult RemoveCasesFromEnvelope(int envelopeId, IEnumerable<int> loadCaseIds);
```

**Implementation consideration:** Do not silently interpret empty input as removing all members.

#### DeleteLoadEnvelop

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_enve.html#ga1ae3d8ba1801dbf2b3e85dff88948d05) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_enve.html#ga1ae3d8ba1801dbf2b3e85dff88948d05) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#ga1ae3d8ba1801dbf2b3e85dff88948d05)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L5.

**Managed capability:** Absent. No deletion of an existing envelope.

```cpp
VARIANT OSLoadUI::DeleteLoadEnvelop(
    const VARIANT FAR&varEnvNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varEnvNo` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varEnvNo` | [in] | Load Envelop reference ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#ga1ae3d8ba1801dbf2b3e85dff88948d05) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `1` | OK. |
| `0` | General error. |

**Proposed managed entry:**

```csharp
LoadOperationResult DeleteEnvelope(int envelopeId);
```

**Implementation consideration:** Document destructive scope and dependency failures; no implicit save.

#### GetEnvelopeCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_enve.html#ga0b8f688b0cddf6f418356af7112bb474) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_enve.html#ga0b8f688b0cddf6f418356af7112bb474) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#ga0b8f688b0cddf6f418356af7112bb474)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
long OSLoadUI::GetEnvelopeCount();
```

No native parameters.

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `Long/Integer` | Total Number of load Envelopes present. |

**Current call sites:** [GetEnvelopeIds — line 2100](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2100).

#### GetLoadEnvelopeDetails

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_enve.html#gaf8b78c76d6f4c0151f6534f5848e77d6) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_enve.html#gaf8b78c76d6f4c0151f6534f5848e77d6) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#gaf8b78c76d6f4c0151f6534f5848e77d6)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
long OSLoadUI::GetLoadEnvelopeDetails(
    long EnvNo,
    long&EnvelopeType,
    long&NumberofLoadCasesInEnvelope
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `EnvNo` | `long` | [in] | `System.Int32` |
| 2 | `EnvelopeType` | `long&` | [out] | `System.Int32&` |
| 3 | `NumberofLoadCasesInEnvelope` | `long&` | [out] | `System.Int32&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `EnvNo` | [in] | Load Envelope reference ID.(Long) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#gaf8b78c76d6f4c0151f6534f5848e77d6) |
| `EnvelopeType` | [out] | Type of Load Envelope.(Long) Value Load Envelop Type 0 NONE 1 STRESS 2 SERVICEABILITY 3 COLUMN 4 CONNECTION 5 STRENGTH 6 TEMPORARY [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#gaf8b78c76d6f4c0151f6534f5848e77d6) |
| `NumberofLoadCasesInEnvelope` | [out] | Number of Load cases in Envelope.(Long) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#gaf8b78c76d6f4c0151f6534f5848e77d6) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `1` | Successful. |
| `-1` | Unsuccessful. |

**Summary/detailed declaration note:** The evidence data retains both forms, including parameter-name aliases. Do not bind by the help-table spelling; compare argument position/type and acquired signature.

**Current call sites:** [GetLoadEnvelopeDetail — line 2144](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2144).

#### GetLoadListfromLoadEnvelope

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_enve.html#ga05374c9b5b38ea705b4db65d6f3ce20a) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_enve.html#ga05374c9b5b38ea705b4db65d6f3ce20a) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#ga05374c9b5b38ea705b4db65d6f3ce20a)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
long OSLoadUI::GetLoadListfromLoadEnvelope(
    long EnvNo,
    VARIANT&varLoadCaseList
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `EnvNo` | `long` | [in] | `System.Int32` |
| 2 | `varLoadCaseList` | `VARIANT&` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `EnvNo` | [in] | Load Envelope reference ID(Type: Integer/Long). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#ga05374c9b5b38ea705b4db65d6f3ce20a) |
| `varLoadCaseList` | [out] | (Primary) load case(s) reference ID(s) VARIANT array(Type: Integer/Long Array). See the function OSLoadUI::GetLoadEnvelopeDetails to obtain the total number of Load cases present in the load Envelope passed. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#ga05374c9b5b38ea705b4db65d6f3ce20a) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `1` | Successful. |
| `-1` | Unsuccessful. |

**Current call sites:** [GetLoadEnvelopeDetail — line 2152](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2152).

#### GetEnvelopeIDs

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_enve.html#gaea479c68d7b2b2114d16e1ffd1a5a199) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_enve.html#gaea479c68d7b2b2114d16e1ffd1a5a199) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#gaea479c68d7b2b2114d16e1ffd1a5a199)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
long OSLoadUI::GetEnvelopeIDs(
    VARIANT&varLoadCaseList
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varLoadCaseList` | `VARIANT&` | Not matched by name; see native reference | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varEnvelopeIDList` | [out] | Envelope ID(s) VARIANT array(Integer/Long Array). Use OSLoadUI::GetEnvelopeCount to set size of Array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#gaea479c68d7b2b2114d16e1ffd1a5a199) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `1` | OK. |
| `-1` | Unsuccessful. |

**Current call sites:** [GetEnvelopeIds — line 2104](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2104).

### Load Definition: Wind

#### AddWindDefinition

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_wind.html#ga2ae52c29c70f50fad759adefb71761e4) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_wind.html#ga2ae52c29c70f50fad759adefb71761e4) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga2ae52c29c70f50fad759adefb71761e4)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::AddWindDefinition(
    const VARIANT FAR&varTypeNo,
    const VARIANT FAR&varTypeName
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varTypeNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varTypeName` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varTypeNo` | [in] | Wind Definition Type number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga2ae52c29c70f50fad759adefb71761e4) |
| `varTypeName` | [in] | String name of this new type ("Comment" in STAAD). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga2ae52c29c70f50fad759adefb71761e4) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Current call sites:** [AddWindDefinition — line 370](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L370).

#### AddWindIntensity

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_wind.html#ga8a39105b37ab53253129a83229559bee) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_wind.html#ga8a39105b37ab53253129a83229559bee) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga8a39105b37ab53253129a83229559bee)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::AddWindIntensity(
    const VARIANT FAR&varTypeNo,
    const VARIANT FAR&varIntensity,
    const VARIANT FAR&varHeight
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varTypeNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varIntensity` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varHeight` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varTypeNo` | [in] | Wind Definition Type number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga8a39105b37ab53253129a83229559bee) |
| `varIntensity` | [in] | Intensity value VARIANT (Double) array (Unit: Force/Length^2). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga8a39105b37ab53253129a83229559bee) |
| `varHeight` | [in] | Height value VARIANT (Double) array (Unit: Length). For additional information, please refer to Section 5.31.3 of the Technical Reference manual. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga8a39105b37ab53253129a83229559bee) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Current call sites:** [AddWindIntensity — line 413](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L413); [AddWindIntensity — line 428](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L428).

#### ComputeWallWindPressureProfile

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_wind.html#ga14842eb0090688108f87a2412922b71d) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_wind.html#ga14842eb0090688108f87a2412922b71d) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga14842eb0090688108f87a2412922b71d)

**Comparison:** Common. **Coverage:** D — adapter dispatch (reference overload port).

```cpp
long OSLoadUI::ComputeWallWindPressureProfile(
    LoadingCode code,
    double windSpeed,
    BldgClass bldgclass,
    BldgType bldgtype,
    ExposureCategory expCat,
    BOOL bEscarpment,
    const VARIANT FAR&varUnitData,
    const VARIANT FAR&varescarpmentData,
    const VARIANT FAR&varbldgData,
    WallType wallType
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `code` | `LoadingCode` | [in] | `OpenSTAADUI.LoadingCode` |
| 2 | `windSpeed` | `double` | [in] | `System.Double` |
| 3 | `bldgclass` | `BldgClass` | [in] | `OpenSTAADUI.BldgClass` |
| 4 | `bldgtype` | `BldgType` | [in] | `OpenSTAADUI.BldgType` |
| 5 | `expCat` | `ExposureCategory` | [in] | `OpenSTAADUI.ExposureCategory` |
| 6 | `bEscarpment` | `BOOL` | [in] | `System.Int32` |
| 7 | `varUnitData` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |
| 8 | `varescarpmentData` | `const VARIANT FAR&` | [in] | `System.Object` |
| 9 | `varbldgData` | `const VARIANT FAR&` | [in] | `System.Object` |
| 10 | `wallType` | `WallType` | [in] | `OpenSTAADUI.WallType` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `code` | [in] | ASCE CODE: Value ASCE CODE ASCE7Y95 = 0 ACSE 7-1995 ACSE702 = 1 ACSE 7-2002 ACSE705_10 = 2 ACSE 7-2010 [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga14842eb0090688108f87a2412922b71d) |
| `windSpeed` | [in] | Wind speed. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga14842eb0090688108f87a2412922b71d) |
| `bldgclass` | [in] | Building Classification Category: Value Building Classification Category TypeI = 0 Category I TypeII = 1 Category II TypeIII = 2 Category III TypeIV = 3 Category IV [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga14842eb0090688108f87a2412922b71d) |
| `bldgtype` | [in] | Structure Type: Value Structure Type Building = 0 Building Structures Chimney = 1 Chimney, Tank and similar structures Solidsign = 2 Solid Signs Opensign = 3 Open Signs Laticeframe = 4 Lattice Framework Trusstower = 5 Trussed T… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga14842eb0090688108f87a2412922b71d) |
| `expCat` | [in] | Exposure Category. Value Exposure Category ExpA = 0 Exposure A ExpB = 1 Exposure B ExpC = 2 Exposure C ExpD = 3 Exposure D [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga14842eb0090688108f87a2412922b71d) |
| `bEscarpment` | [in] | Consider Wind Speed-up over Hills ( FALSE ) or Escarpment ( TRUE ). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga14842eb0090688108f87a2412922b71d) |
| `varescarpmentData` | [in] | Information describing Hills or Escarpment: Index Data 0 Type: 2D Ridge (0), 2D Escarpment (1), 3D Escarpment (2) 1 Height ( H ) 2 Distance upwind of crest ( Lh ) 3 Distance from the crest to the building ( x ) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga14842eb0090688108f87a2412922b71d) |
| `varbldgData` | [in] | - Variant Array of Double of size 7 containing information describing the building based on structure type. Building Data Index Item 0 Enclosure Classification: Open Building (0)/ Partially Enclosed (1)/ Enclosed Building (2) 1… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga14842eb0090688108f87a2412922b71d) |
| `wallType` | [in] | Building wall to generate Wind Load on: Value Wall Type WindWard = 0 WindWard LeeWard = 1 Leeward SideWall = 2 SideWall [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga14842eb0090688108f87a2412922b71d) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `number` | of Height or Intensity data. |
| `-1` | General error. |

**Further named-result API proposal (not implemented by this reference-signature port):**

```csharp
WindProfileComputationResult ComputeWallWindPressureProfile(WallWindPressureProfileParameters parameters);
```

**Implementation consideration:** Return generated point count and status, not fabricated height/pressure data; the referenced profile getters are outside the inspected IOSLoadUI inventory.

**Ported reference overloads:** `int ComputeWallWindPressureProfile(WallWindPressureProfileParameters parameters)`.

**Current call sites:** [ComputeWallWindPressureProfile](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L199).


#### ComputeWallWindPressureProfileASCE72016

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_wind.html#ga09d85745d67966ad0b323ddf4751b739) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_wind.html#ga09d85745d67966ad0b323ddf4751b739) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga09d85745d67966ad0b323ddf4751b739)

**Comparison:** Common. **Coverage:** D — adapter dispatch (reference overload port).

```cpp
long OSLoadUI::ComputeWallWindPressureProfileASCE72016(
    double windSpeed,
    double heightAboveSeaLvl,
    BldgClass bldgclass,
    BldgType bldgtype,
    ExposureCategory expCat,
    BOOL bEscarpment,
    const VARIANT FAR&varUnitData,
    const VARIANT FAR&varescarpmentData,
    const VARIANT FAR&varbldgData,
    WallType wallType
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `windSpeed` | `double` | [in] | `System.Double` |
| 2 | `heightAboveSeaLvl` | `double` | [in] | `System.Double` |
| 3 | `bldgclass` | `BldgClass` | [in] | `OpenSTAADUI.BldgClass` |
| 4 | `bldgtype` | `BldgType` | [in] | `OpenSTAADUI.BldgType` |
| 5 | `expCat` | `ExposureCategory` | [in] | `OpenSTAADUI.ExposureCategory` |
| 6 | `bEscarpment` | `BOOL` | [in] | `System.Int32` |
| 7 | `varUnitData` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |
| 8 | `varescarpmentData` | `const VARIANT FAR&` | [in] | `System.Object` |
| 9 | `varbldgData` | `const VARIANT FAR&` | [in] | `System.Object` |
| 10 | `wallType` | `WallType` | [in] | `OpenSTAADUI.WallType` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `windSpeed` | [in] | Wind speed. Default value 85 mph. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga09d85745d67966ad0b323ddf4751b739) |
| `heightAboveSeaLvl` | [in] | Ground height above sea level. Used only for ASCE7-2016 Wind. Default value 0.0 ft. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga09d85745d67966ad0b323ddf4751b739) |
| `bldgclass` | [in] | Building Classification Category: Value Building Classification Category TypeI = 0 Category I TypeII = 1 Category II TypeIII = 2 Category III TypeIV = 3 Category IV [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga09d85745d67966ad0b323ddf4751b739) |
| `bldgtype` | [in] | Structure Type: Value Structure Type Building = 0 Building Structures Chimney = 1 Chimney, Tank and similar structures Solidsign = 2 Solid Signs Opensign = 3 Open Signs Laticeframe = 4 Lattice Framework Trusstower = 5 Trussed T… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga09d85745d67966ad0b323ddf4751b739) |
| `expCat` | [in] | Exposure Category. Value Exposure Category ExpA = 0 Exposure A ExpB = 1 Exposure B ExpC = 2 Exposure C ExpD = 3 Exposure D [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga09d85745d67966ad0b323ddf4751b739) |
| `bEscarpment` | [in] | Consider Wind Speed-up over Hills ( FALSE ) or Escarpment ( TRUE ). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga09d85745d67966ad0b323ddf4751b739) |
| `varUnitsData` | [in] | - VARIANT Integer array of size 8 containing Units of data inputs: Index Data 0 Unit of Wind Speed {mph(VelocityUnit::mph or 0) or m/sec(VelocityUnit::metersec or 1) or cm/sec(VelocityUnit::cmsec or 2) or mm/sec(VelocityUnit::m… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga09d85745d67966ad0b323ddf4751b739) |
| `varescarpmentData` | [in] | Information describing Hills or Escarpment: Index Data 0 Type: 2D Ridge (0), 2D Escarpment (1), 3D Escarpment (2) 1 Height ( H ) 2 Distance upwind of crest ( Lh ) 3 Distance from the crest to the building ( x ) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga09d85745d67966ad0b323ddf4751b739) |
| `varbldgData` | [in] | - Variant Array of Double of size 7 containing information describing the building based on structure type. Building Data Index Item 0 Enclosure Classification: Open Building (0)/ Partially Enclosed (1)/ Enclosed Building (2) 1… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga09d85745d67966ad0b323ddf4751b739) |
| `wallType` | [in] | Building wall to generate Wind Load on: Value Wall Type WindWard = 0 WindWard LeeWard = 1 Leeward SideWall = 2 SideWall [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga09d85745d67966ad0b323ddf4751b739) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `number` | of Height or Intensity data. |
| `-1` | General error. |

**Further named-result API proposal (not implemented by this reference-signature port):**

```csharp
WindProfileComputationResult ComputeWallWindPressureProfile(ASCE72016WallWindPressureProfileParameters parameters);
```

**Implementation consideration:** Dispatch by engineering code edition; ASCE 7-2016 is not the STAAD product year.

**Ported reference overloads:** `int ComputeWallWindPressureProfileASCE72016(ASCE72016WallWindPressureProfileParameters parameters)`.

**Current call sites:** [ComputeWallWindPressureProfileASCE72016](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L212).


#### AddWindDefinitionASCE7Parameters

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_wind.html#gaeaa2d3d5e5b318160defa2f8596dc343) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_wind.html#gaeaa2d3d5e5b318160defa2f8596dc343) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#gaeaa2d3d5e5b318160defa2f8596dc343)

**Comparison:** Common. **Coverage:** D — adapter dispatch (reference overload port).

```cpp
VARIANT OSLoadUI::AddWindDefinitionASCE7Parameters(
    long nTypeNo,
    ASCE7_Publication code,
    const VARIANT FAR&windSpeed,
    const VARIANT FAR&heightAboveSeaLvl,
    ASCE7_BldgClass bldgclass,
    ASCE7_BldgType bldgtype,
    ASCE7_ExposureCategory expCat,
    long bEscarpment,
    ASCE7_WallType wallType,
    long isFlexible,
    const VARIANT FAR&varescarpmentData,
    const VARIANT FAR&varbldgData,
    const VARIANT FAR&varUnitsData,
    const VARIANT FAR&varFactorsUserInput,
    const VARIANT FAR&varFactors
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nTypeNo` | `long` | Not matched by name; see native reference | `System.Int32` |
| 2 | `code` | `ASCE7_Publication` | [in] | `OpenSTAADUI.ASCE7_Publication` |
| 3 | `windSpeed` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `heightAboveSeaLvl` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `bldgclass` | `ASCE7_BldgClass` | [in] | `OpenSTAADUI.ASCE7_BldgClass` |
| 6 | `bldgtype` | `ASCE7_BldgType` | [in] | `OpenSTAADUI.ASCE7_BldgType` |
| 7 | `expCat` | `ASCE7_ExposureCategory` | [in] | `OpenSTAADUI.ASCE7_ExposureCategory` |
| 8 | `bEscarpment` | `long` | Not matched by name; see native reference | `System.Int32` |
| 9 | `wallType` | `ASCE7_WallType` | [in] | `OpenSTAADUI.ASCE7_WallType` |
| 10 | `isFlexible` | `long` | Not matched by name; see native reference | `System.Int32` |
| 11 | `varescarpmentData` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |
| 12 | `varbldgData` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |
| 13 | `varUnitsData` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |
| 14 | `varFactorsUserInput` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |
| 15 | `varFactors` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varTypeNo` | [in] | Wind Definition Type number ID (Integer). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#gaeaa2d3d5e5b318160defa2f8596dc343) |
| `code` | [in] | ASCE CODE: Value ASCE CODE ASCE7_95 ACSE 7-1995 ASCE7_02 ACSE 7-2002 ASCE7_05_10 ACSE 7-2010 ASCE7_16 ACSE 7-2016 [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#gaeaa2d3d5e5b318160defa2f8596dc343) |
| `windSpeed` | [in] | Wind speed (Double). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#gaeaa2d3d5e5b318160defa2f8596dc343) |
| `heightAboveSeaLvl` | [in] | Ground Height above sea level (Double) [Required only for ASCE7-2016. For other versions give 0.0] [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#gaeaa2d3d5e5b318160defa2f8596dc343) |
| `bldgclass` | [in] | Building Classification Category: Value Building Classification Category ASCE7_TypeI = 0 Category I ASCE7_TypeII = 1 Category II ASCE7_TypeIII = 2 Category III ASCE7_TypeIV = 3 Category IV [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#gaeaa2d3d5e5b318160defa2f8596dc343) |
| `bldgtype` | [in] | Structure Type: Value Structure Type ASCE7_Building = 0 Building Structures ASCE7_Chimney = 1 Chimney, Tank and similar structures ASCE7_Solidsign = 2 Solid Signs ASCE7_Opensign = 3 Open Signs ASCE7_Laticeframe = 4 Lattice Fram… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#gaeaa2d3d5e5b318160defa2f8596dc343) |
| `expCat` | [in] | Exposure Category. Value Exposure Category ASCE7_ExpA = 0 Exposure A ASCE7_ExpB = 1 Exposure B ASCE7_ExpC = 2 Exposure C ASCE7_ExpD = 3 Exposure D [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#gaeaa2d3d5e5b318160defa2f8596dc343) |
| `varEscarpment` | [in] | Consider Wind Speed-up over Hills ( FALSE ) or Escarpment ( TRUE ). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#gaeaa2d3d5e5b318160defa2f8596dc343) |
| `wallType` | [in] | Building wall to generate Wind Load on: Value Wall Type ASCE7_WindWard = 0 WindWard ASCE7_LeeWard = 1 Leeward ASCE7_SideWall = 2 SideWall [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#gaeaa2d3d5e5b318160defa2f8596dc343) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Further named-result API proposal (not implemented by this reference-signature port):**

```csharp
LoadOperationResult ConfigureWindDefinition(ASCE7WindDefinitionParameters parameters);
```

**Implementation consideration:** Reuse source structure/unit/factor models after review; resolve seven-versus-eight slot inconsistencies before release.

**Ported reference overloads:** `int AddWindDefinitionASCE7Parameters(ASCE7WindDefinitionParameters parameters)`.

**Current call sites:** [AddWindDefinitionASCE7Parameters](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L185).


#### AddWindExposure

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_wind.html#ga30c27ad6bfef60382db306f1d8168cef) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_wind.html#ga30c27ad6bfef60382db306f1d8168cef) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga30c27ad6bfef60382db306f1d8168cef)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::AddWindExposure(
    const VARIANT FAR&varTypeNo,
    const VARIANT FAR&varExposureFactor,
    const VARIANT FAR&varNodeArray
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varTypeNo` | `const VARIANT FAR&` | Not stated | `System.Object` |
| 2 | `varExposureFactor` | `const VARIANT FAR&` | Not stated | `System.Object` |
| 3 | `varNodeArray` | `const VARIANT FAR&` | Not stated | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varTypeNo` | Not stated | Wind Definition Type number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga30c27ad6bfef60382db306f1d8168cef) |
| `varExposureFactor` | Not stated | Exposure factor. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga30c27ad6bfef60382db306f1d8168cef) |
| `varNodeArray` | Not stated | Node number ID(s) VARIANT array. For additional information, please refer to Section 5.31.3 of the Technical Reference manual. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga30c27ad6bfef60382db306f1d8168cef) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Current call sites:** [AddWindExposure — line 382](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L382).

#### DeleteWindDefinition

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_wind.html#ga0b2356ca2a0d681720b9da8d3c022cc9) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_wind.html#ga0b2356ca2a0d681720b9da8d3c022cc9) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga0b2356ca2a0d681720b9da8d3c022cc9)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::DeleteWindDefinition(
    long nTypeNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nTypeNo` | `long` | [in] | `System.Int32` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nTypeNo` | [in] | Type of Wind. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga0b2356ca2a0d681720b9da8d3c022cc9) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-8039` | Invalid load definition. |

**Current call sites:** [DeleteWindDefinition — line 444](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L444).

### Load Definition: Seismic

#### AddSeismicDefinition

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_seismic.html#ga885d12987341c6323c4c71b02979af9b) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_seismic.html#ga885d12987341c6323c4c71b02979af9b) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#ga885d12987341c6323c4c71b02979af9b)

**Comparison:** Capability. **Coverage:** D — adapter dispatch (reference overload port).

```cpp
VARIANT OSLoadUI::AddSeismicDefinition(
    const VARIANT FAR&varType,
    const VARIANT FAR&varAccidental
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varType` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varAccidental` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varType` | [in] | Type of seismic code. Value Seismic Code 0 UBC 1985 1 UBC 1994 2 UBC 1997 3 Indian: IS 1893-1984 4 Indian: IS 1893-2002/2005 5 IBC 2000 6 IBC 2003 7 COLOMBIAN: NSR 98 8 JAPANESE (AIJ) 9 ALGERIAN: RPA 10 MEX: CFE-1993 11 MEX: NT… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#ga885d12987341c6323c4c71b02979af9b) |
| `varAccidental` | [in] | Consider accidental torsion (= 1 ); ignore (= 0 ). For additional information, please refer to Section 5.31.2 of the Technical Reference manual. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#ga885d12987341c6323c4c71b02979af9b) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `TRUE` | Successful. |
| `FALSE` | Unsuccessful. |

**Further named-result API proposal (not implemented by this reference-signature port):**

```csharp
LoadOperationResult CreateSeismicDefinition(SeismicCode seismicCode, bool includeAccidentalTorsion = false);
```

**Implementation consideration:** Use definition-code mapping, distinct from member-weight mapping; gate NRC 2020 by acquired profile.

**Ported reference overloads:** `bool AddSeismicDefinition(SeismicCode code, bool considerAccidentalTorsion = false)`; `bool AddSeismicDefinition(int code, int accidentalTorsion = 0)`.

**Current call sites:** [AddSeismicDefinition](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L233).


#### ModifySeismicDefinitionParams

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_seismic.html#ga5b4e6c8e6e7c46b829de3cb21c1f963d) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_seismic.html#ga5b4e6c8e6e7c46b829de3cb21c1f963d) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#ga5b4e6c8e6e7c46b829de3cb21c1f963d)

**Comparison:** Capability. **Coverage:** D — adapter dispatch (reference overload port).

```cpp
VARIANT OSLoadUI::ModifySeismicDefinitionParams(
    const VARIANT FAR&varParamName,
    const VARIANT FAR&varValue
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varParamName` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varValue` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varParamName` | [in] | Parameter name for the corresponding code in the seismic definition. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#ga5b4e6c8e6e7c46b829de3cb21c1f963d) |
| `varValue` | [in] | Value corresponding to the above parameter. (Type : Double) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#ga5b4e6c8e6e7c46b829de3cb21c1f963d) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Further named-result API proposal (not implemented by this reference-signature port):**

```csharp
LoadOperationResult SetSeismicParameters(SeismicDefinitionParameters parameters);
```

**Implementation consideration:** Validate allowed keywords for the selected seismic code and acquired profile; preserve original native failure code.

**Ported reference overloads:** `int ModifySeismicDefinitionParam(string parameterName, double value)`; `int ModifySeismicDefinitionParams(IDictionary<string, double> parameters)`; `int ModifySeismicDefinitionParams(IS1893_2016SeismicParameters parameters)`; `int ModifySeismicDefinitionParams(IS1893_2002SeismicParameters parameters)`; `int ModifySeismicDefinitionParams(IBCSeismicParameters parameters)`.

**Current call sites:** [ModifySeismicDefinitionParam](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L241).


#### AddSeismicDefSelfWeight

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_seismic.html#ga0aee693e0d969e310fc41938f5ca6cd3) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_seismic.html#ga0aee693e0d969e310fc41938f5ca6cd3) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#ga0aee693e0d969e310fc41938f5ca6cd3)

**Comparison:** Clarified. **Coverage:** D — adapter dispatch (reference overload port).

```cpp
VARIANT OSLoadUI::AddSeismicDefSelfWeight(
    const VARIANT FAR&varWeightFactor
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varWeightFactor` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varWeightFactor` | [in] | (Type : Double) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#ga0aee693e0d969e310fc41938f5ca6cd3) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `true` | Successful. |
| `false` | General error. |

**Further named-result API proposal (not implemented by this reference-signature port):**

```csharp
LoadOperationResult AddSeismicSelfWeight(double weightFactor = 1.0);
```

**Implementation consideration:** Finite Double factor; native Boolean success, not integer-zero success.

**Ported reference overloads:** `bool AddSeismicDefSelfWeight(double weightFactor = 1.0)`.

**Current call sites:** [AddSeismicDefSelfWeight](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L287).


#### AddSeismicDefMemberWeight

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_seismic.html#ga2299de54c2eeddab2a1ba7ba11b86052) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_seismic.html#ga2299de54c2eeddab2a1ba7ba11b86052) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#ga2299de54c2eeddab2a1ba7ba11b86052)

**Comparison:** Mapping. **Coverage:** D — adapter dispatch (reference overload port).

```cpp
VARIANT OSLoadUI::AddSeismicDefMemberWeight(
    const VARIANT FAR&varSeismicType,
    const VARIANT FAR&varLoadType,
    const VARIANT FAR&varWeight,
    const VARIANT FAR&varStartDist,
    const VARIANT FAR&varEndDist,
    const VARIANT FAR&varMemberArray
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varSeismicType` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varLoadType` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varWeight` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varStartDist` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varEndDist` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `varMemberArray` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varSeismicType` | [in] | Type of seismic code (see Table below) (Type : Long). Value Seismic Code 0 AUTO DETECT 1 ALGERIAN: RPA 2 CANADIAN: NRC-1995 3 CANADIAN: NRC-2005 4 CANADIAN: NRC-2010 5 CANADIAN: NRC-2020 6 Chinese: GB50011-2001 7 Chinese: GB500… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#ga2299de54c2eeddab2a1ba7ba11b86052) |
| `varLoadType` | [in] | 1 - UNI and 2 - CON (Type : long). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#ga2299de54c2eeddab2a1ba7ba11b86052) |
| `varWeight` | [in] | Uniform weight (Type : double). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#ga2299de54c2eeddab2a1ba7ba11b86052) |
| `varStartDist` | [in] | Starting distance( = distance from member starting node to weight starting point)(Type : double). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#ga2299de54c2eeddab2a1ba7ba11b86052) |
| `varEndDist` | [in] | Ending distance( = distance from member starting node to weight ending point)(Type : double). if varLoadType is UNI and varStartDist and varEndDist are 0, the load is assumed to cover the entire length of the member. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#ga2299de54c2eeddab2a1ba7ba11b86052) |
| `varMemberArray` | [in] | Member number ID(s) VARIANT array.(Type : long) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#ga2299de54c2eeddab2a1ba7ba11b86052) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `TRUE` | If it successfully adds member concentrated/uniform weight to Seismic Definition. |
| `FALSE` | Unsuccessful. |

**Further named-result API proposal (not implemented by this reference-signature port):**

```csharp
LoadOperationResult AddSeismicMemberWeight(SeismicMemberWeightDefinition definition, IEnumerable<int> memberIds);
```

**Implementation consideration:** Map stable semantic code to the acquired version-specific member-weight number; UNI=1 and CON=2.

**Ported reference overloads:** `bool AddSeismicDefMemberWeight(int seismicCode, SeismicMemberLoadType loadType, double weight, double startDistance, double endDistance, IEnumerable<int> memberIds)`.

**Current call sites:** [AddSeismicDefMemberWeight](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L325).


#### AddSeismicDefJointWeight

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_seismic.html#gae8c289357923e66c14f2b08417055bc7) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_seismic.html#gae8c289357923e66c14f2b08417055bc7) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#gae8c289357923e66c14f2b08417055bc7)

**Comparison:** Status. **Coverage:** D — adapter dispatch (reference overload port).

```cpp
VARIANT OSLoadUI::AddSeismicDefJointWeight(
    const VARIANT FAR&varWeight,
    const VARIANT FAR&varNodeArray
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varWeight` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varNodeArray` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varWeight` | [in] | Weight value. (Type: Double) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#gae8c289357923e66c14f2b08417055bc7) |
| `varNodeArray` | [in] | Node number ID(s) VARIANT array. (Type: Long Array/Long) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#gae8c289357923e66c14f2b08417055bc7) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |
| `-100` | Invalid argument. |
| `-106` | 1 dimensional array of long expected. |
| `-113` | Integer array/Integer expected. |
| `-2006` | Invalid Node Number. |
| `-8034` | Seisemic Code not found. |

**2024 return table:** `0` = OK.; `-1` = General error.; `-106` = 1 dimensional array of long expected.. The table above reflects 2025/2026. Preserve unknown failure codes.

**Further named-result API proposal (not implemented by this reference-signature port):**

```csharp
LoadOperationResult AddSeismicJointWeight(double weight, IEnumerable<int> nodeIds);
```

**Implementation consideration:** Preserve Double and Int32-array shapes; distinguish expanded 2025/2026 error codes from unknown errors.

**Ported reference overloads:** `int AddSeismicDefJointWeight(double weight, IEnumerable<int> nodeIds)`; `int AddSeismicDefJointWeight(double weight, IEnumerable<Node> nodes)`; `int AddSeismicDefJointWeight(double weight, int nodeId)`.

**Current call sites:** [AddSeismicDefJointWeight](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L296).


#### AddSeismicDefWallArea

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_seismic.html#gad481a21e57f2ee6b953d5e1d111c34c7) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_seismic.html#gad481a21e57f2ee6b953d5e1d111c34c7) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#gad481a21e57f2ee6b953d5e1d111c34c7)

**Comparison:** Common. **Coverage:** D — adapter dispatch (reference overload port).

```cpp
VARIANT OSLoadUI::AddSeismicDefWallArea(
    long nTypeNo,
    LPCTSTR szDirection,
    const VARIANT FAR&varMembArray
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nTypeNo` | `long` | [in] | `System.Int32` |
| 2 | `szDirection` | `LPCTSTR` | Not matched by name; see native reference | `System.String` |
| 3 | `varMembArray` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nTypeNo` | [in] | Type of seismic code (see Table below). Value Seismic Code 15 Indian: IS 1893-2016 [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#gad481a21e57f2ee6b953d5e1d111c34c7) |
| `direction` | [in] | value. [X directin or Z direction] [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#gad481a21e57f2ee6b953d5e1d111c34c7) |
| `varMembArray` | [in] | - Length and Width VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#gad481a21e57f2ee6b953d5e1d111c34c7) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |
| `-107` | 1 dimensional array of long expected. |
| `-8034` | Invalid seismic code. |
| `-8038` | Invalid Direction. |

**Further named-result API proposal (not implemented by the reference-signature port):**

```csharp
LoadOperationResult AddSeismicWallArea(int definitionId, SeismicWallDirection direction, IEnumerable<SeismicWallDimension> walls);
```

**Implementation consideration:** IS 1893-2016 engineering-code constraint; validate flattened dimension layout, not an arbitrary member-ID array.


**Ported reference overloads:** `int AddSeismicDefWallArea(int seismicCode, string direction, IEnumerable<double> dimensions)`; `int AddSeismicDefWallArea(int seismicCode, SeismicWallDirection direction, IEnumerable<SeismicWallDimension> walls)`.

**Current call sites:** [AddSeismicDefWallArea](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L70).

#### AddResponseSpectrumLoadEx

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_seismic.html#gac24b840992cf7f15558b3c6844857f5a) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_seismic.html#gac24b840992cf7f15558b3c6844857f5a) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#gac24b840992cf7f15558b3c6844857f5a)

**Comparison:** Common. **Coverage:** D — adapter dispatch (reference overload port).

```cpp
VARIANT OSLoadUI::AddResponseSpectrumLoadEx(
    long rsaCode,
    long rsaCombination,
    const VARIANT FAR&varSet1Names,
    const VARIANT FAR&varSet1Vals,
    const VARIANT FAR&varSet2Names,
    const VARIANT FAR&varSet2Vals,
    const VARIANT FAR&varSpectrumDataPairs
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `rsaCode` | `long` | [in] | `System.Int32` |
| 2 | `rsaCombination` | `long` | [in] | `System.Int32` |
| 3 | `varSet1Names` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varSet1Vals` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varSet2Names` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `varSet2Vals` | `const VARIANT FAR&` | [in] | `System.Object` |
| 7 | `varSpectrumDataPairs` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `rsaCode` | [in] | Response Spectrum Loading Code. Refer to the following table for the integers corresponding to different codes. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#gac24b840992cf7f15558b3c6844857f5a) |
| `rsaCombination` | [in] | Modal combination rule. (SRSS = 0, ABS = 1, CQC = 2, ASCE = 3, TEN = 4, CSM = 5, GRP = 6) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#gac24b840992cf7f15558b3c6844857f5a) |
| `varSet1Names` | [in] | VARIANT BSTR array containing parameter key words. Refer to the Techincal Reference sections as indicated below. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#gac24b840992cf7f15558b3c6844857f5a) |
| `varSet1Vals` | [in] | Parameters values corresponding to the keywords supplied in varSet1Names array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#gac24b840992cf7f15558b3c6844857f5a) |
| `varSet2Names` | [in] | Optional VARIANT BSTR array containing parameter key words for the spectrum generation data command. NULL can be used if not neeed. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#gac24b840992cf7f15558b3c6844857f5a) |
| `varSet2Vals` | [in] | Parameters values corresponding to the keywords supplied in varSet2Names array. NULL can be used if not needed. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#gac24b840992cf7f15558b3c6844857f5a) |
| `varDataPairs` | [in] | VARIANT double array containing pairs of time period and accleration data. NULL can used if not needed. Inputs (varSet2Names, varSet2Vals) and (varDataPairs) are mutually exclusive, i.e. if one is specified, other should not sp… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#gac24b840992cf7f15558b3c6844857f5a) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | FALSE. |
| `1` | TRUE. |

**Further named-result API proposal (not implemented by the reference-signature port):**

```csharp
LoadOperationResult AddResponseSpectrumLoad(ResponseSpectrumLoadExParameters parameters);
```

**Implementation consideration:** Validate code-specific keywords, modal rule and paired arrays; generated-spectrum settings and custom data pairs are mutually exclusive.


**Ported reference overloads:** `bool AddResponseSpectrumLoadEx(ResponseSpectrumLoadExParameters parameters)`; `bool AddResponseSpectrumLoadEx(int spectrumCode, int combinationMethod, string[] primaryParameterNames, double[] primaryParameterValues, string[] generationParameterNames = null, double[] generationParameterValues = null, double[] customDataPairs = null)`.

**Current call sites:** [SubmitExtendedSpectrum](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L107).

### Load Definition: Time History

No native function declarations occur in this reviewed grouping page. Subgroups, where present, were traversed separately.

### Load Definition: Snow

No native function declarations occur in this reviewed grouping page. Subgroups, where present, were traversed separately.

### Load Definition: Reference

#### GetReferenceLoadCaseCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_ref_load.html#ga70c105eb346388eeed599db3ec1ea109) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_ref_load.html#ga70c105eb346388eeed599db3ec1ea109) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_ref_load.html#ga70c105eb346388eeed599db3ec1ea109)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
long OSLoadUI::GetReferenceLoadCaseCount();
```

No native parameters.

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The number of reference load case. |
| `-1` | General error. |

**Current call sites:** [ReadAllLoadCases — line 2349](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2349).

#### GetReferenceLoadCaseNumbers

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_ref_load.html#gad56e67bc7dedd7e719cf97661581550c) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_ref_load.html#gad56e67bc7dedd7e719cf97661581550c) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_ref_load.html#gad56e67bc7dedd7e719cf97661581550c)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
long OSLoadUI::GetReferenceLoadCaseNumbers(
    VARIANT FAR*LoadNoArray
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `LoadNoArray` | `VARIANT FAR*` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `LoadNoArray` | [out] | Reference load case number ID(s) VARIANT array [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_ref_load.html#gad56e67bc7dedd7e719cf97661581550c) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The number of reference load case(s). |
| `-1` | General error. |
| `-106` | 1 dimensional array of long expected. |
| `-114` | OLE Exception Occurred. |

**Current call sites:** [ReadAllLoadCases — line 2357](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2357).

#### CreateNewReferenceLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_ref_load.html#ga13220ea514164923069da40dea5065fb) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_ref_load.html#ga13220ea514164923069da40dea5065fb) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_ref_load.html#ga13220ea514164923069da40dea5065fb)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
long OSLoadUI::CreateNewReferenceLoad(
    const long nLoadNo,
    const VARIANT FAR&varLoadCaseTitle,
    const long nLoadType
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nLoadNo` | `const long` | [in] | `System.Int32` |
| 2 | `varLoadCaseTitle` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `nLoadType` | `const long` | [in] | `System.Int32` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nLoadNo` | [in] | (Reference) load case reference ID to be assigned to new created reference load case. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_ref_load.html#ga13220ea514164923069da40dea5065fb) |
| `varLoadCaseTitle` | [in] | Load case string title. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_ref_load.html#ga13220ea514164923069da40dea5065fb) |
| `nLoadType` | [in] | Type of the load. Value Load Type Value Load Type 0 Dead 12 Traffic 1 Live 13 Temp 2 Roof Live 14 Imperfection 3 Wind 15 Accidental 4 Seismic-H 16 Flood 5 Seismic-V 17 Ice 6 Snow 18 Wind Ice 7 Fluids 19 Crane Hook 8 Soil 20 Mas… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_ref_load.html#ga13220ea514164923069da40dea5065fb) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | Reference load case number ID. |
| `-1` | General error. |
| `-8004` | Fail to create load. |

**Current call sites:** [CreateNewReferenceLoad — line 1822](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1822).

#### SetReferenceLoadActive

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_ref_load.html#gad59a03aa194d6f31cbb49c59959dc053) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_ref_load.html#gad59a03aa194d6f31cbb49c59959dc053) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_ref_load.html#gad59a03aa194d6f31cbb49c59959dc053)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
long OSLoadUI::SetReferenceLoadActive(
    const long nLoadCaseNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nLoadCaseNo` | `const long` | [in] | `System.Int32` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nLoadCaseNo` | [in] | Load case reference ID in Load Cases Details. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_ref_load.html#gad59a03aa194d6f31cbb49c59959dc053) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | Reference load case number ID. |
| `-1` | General error. |
| `-8002` | Load Case nLoadCaseNo not found. |

**Current call sites:** [SetLoadCaseActive — line 1849](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1849); [ActivateQueryCase — line 2427](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2427).

### Load Definition: Direct Analysis

#### AddDirectAnalysisDefinitionParameter

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_direct_analysis.html#gac8fe142a3657334769e6cbe960e40d1f) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_direct_analysis.html#gac8fe142a3657334769e6cbe960e40d1f) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_direct_analysis.html#gac8fe142a3657334769e6cbe960e40d1f)

**Comparison:** Common. **Coverage:** D — adapter dispatch (reference overload port).

```cpp
VARIANT OSLoadUI::AddDirectAnalysisDefinitionParameter(
    DirectAnalysisParameterTypes pParamType,
    const VARIANT FAR&members,
    double param
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `pParamType` | `DirectAnalysisParameterTypes` | [in] | `OpenSTAADUI.DirectAnalysisParameterTypes` |
| 2 | `members` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `param` | `double` | [in] | `System.Double` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `pParamType` | [in] | Integer indicating type of direct analysis parameter to be added. Integer value should be taken from following table. Value AnalysisCommand FLEX = 0 DirectAnalysisParameterTypes.FLEX AXIAL = 2 DirectAnalysisParameterTypes.AXIAL [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_direct_analysis.html#gac8fe142a3657334769e6cbe960e40d1f) |
| `members` | [in] | one dimensional array of members of int/long type. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_direct_analysis.html#gac8fe142a3657334769e6cbe960e40d1f) |
| `param` | [in] | FLEX parameter value of double type. [For AXIAL this value is Not Applicable. Should pass 0(zero)] [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_direct_analysis.html#gac8fe142a3657334769e6cbe960e40d1f) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `TRUE/1` | OK. |
| `FALSE/0` | ERROR |

**Further named-result API proposal (not implemented by the reference-signature port):**

```csharp
LoadOperationResult SetDirectAnalysisParameter(DirectAnalysisParameters parameters);
```

**Implementation consideration:** Reuse FLEX/AXIAL model; native codes FLEX=0, AXIAL=2; AXIAL scalar must be zero.


**Ported reference overloads:** `bool AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType parameterType, IEnumerable<int> memberIds, double parameterValue = 0.0)`; `bool AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType parameterType, IEnumerable<Beam> members, double parameterValue = 0.0)`; `bool AddDirectAnalysisDefinitionParameter(DirectAnalysisParameters parameters)`.

**Current call sites:** [AddDirectAnalysisDefinitionParameter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L120).

#### DeleteDirectAnalysisDefinitionParameter

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_direct_analysis.html#gacfb6b7aa2b317a90aced0bb8df579237) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_direct_analysis.html#gacfb6b7aa2b317a90aced0bb8df579237) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_direct_analysis.html#gacfb6b7aa2b317a90aced0bb8df579237)

**Comparison:** Common. **Coverage:** D — adapter dispatch (reference overload port).

```cpp
VARIANT OSLoadUI::DeleteDirectAnalysisDefinitionParameter(
    DirectAnalysisParameterTypes pParamType
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `pParamType` | `DirectAnalysisParameterTypes` | [in] | `OpenSTAADUI.DirectAnalysisParameterTypes` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `pParamType` | [in] | Integer indicating type of direct analysis parameter to be added. Integer value should be taken from following table. Value AnalysisCommand FLEX = 0 DirectAnalysisParameterTypes.FLEX AXIAL = 2 DirectAnalysisParameterTypes.AXIAL [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_direct_analysis.html#gacfb6b7aa2b317a90aced0bb8df579237) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `TRUE/1` | OK. |
| `FALSE/0` | ERROR |

**Further named-result API proposal (not implemented by the reference-signature port):**

```csharp
LoadOperationResult DeleteDirectAnalysisParameter(DirectAnalysisParameterType parameterType);
```

**Implementation consideration:** Verify supported parameter code and preserve Boolean native result.


**Ported reference overloads:** `bool DeleteDirectAnalysisDefinitionParameter(DirectAnalysisParameterType parameterType)`.

**Current call sites:** [DeleteDirectAnalysisDefinitionParameter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L154).

#### DeleteDirectAnalysisDefinition

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_direct_analysis.html#ga5577e10064596d7933dc30e5b68a66fd) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_direct_analysis.html#ga5577e10064596d7933dc30e5b68a66fd) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_direct_analysis.html#ga5577e10064596d7933dc30e5b68a66fd)

**Comparison:** Common. **Coverage:** D — adapter dispatch (reference overload port).

```cpp
VARIANT OSLoadUI::DeleteDirectAnalysisDefinition();
```

No native parameters.

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `TRUE/1` | OK. |
| `FALSE/0` | ERROR |

**Further named-result API proposal (not implemented by the reference-signature port):**

```csharp
LoadOperationResult DeleteDirectAnalysisDefinition();
```

**Implementation consideration:** Explicit whole-definition deletion; no synthetic rollback.


**Ported reference overloads:** `bool DeleteDirectAnalysisDefinition()`.

**Current call sites:** [DeleteDirectAnalysisDefinition](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L160).

### Load Case Details: Load Items

No native function declarations occur in this reviewed grouping page. Subgroups, where present, were traversed separately.

### Load Case Details: Load Combination

#### GetLoadCombinationCaseCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_comb.html#ga4c6b39b1198e6e63aba6a7b56ca59dcc) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_comb.html#ga4c6b39b1198e6e63aba6a7b56ca59dcc) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga4c6b39b1198e6e63aba6a7b56ca59dcc)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetLoadCombinationCaseCount();
```

No native parameters.

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

No return-value table is present. Read the return prose in the linked reference; do not infer Boolean success or invent a payload.

**Current call sites:** [ReadCombinationCaseIds — line 2385](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2385).

#### GetLoadCombinationCaseNumbers

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_comb.html#gafe01a8e664b6722b0e341e37152cb4a9) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_comb.html#gafe01a8e664b6722b0e341e37152cb4a9) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gafe01a8e664b6722b0e341e37152cb4a9)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetLoadCombinationCaseNumbers(
    VARIANT FAR&lCases
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `lCases` | `VARIANT FAR&` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `lCases` | [out] | The load case reference number ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gafe01a8e664b6722b0e341e37152cb4a9) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The number of primary load case(s). |
| `-1` | General error. |
| `-106` | 1 dimensional array of long expected. |
| `-114` | OLE Exception Occurred. |

**Current call sites:** [ReadCombinationCaseIds — line 2388](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2388).

#### CreateNewLoadCombination

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_comb.html#ga8bb03f4733ba650a9df0a8fc16e4e523) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_comb.html#ga8bb03f4733ba650a9df0a8fc16e4e523) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga8bb03f4733ba650a9df0a8fc16e4e523)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::CreateNewLoadCombination(
    const VARIANT FAR&varLoadCombTitle,
    const VARIANT FAR&varLoadCombNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varLoadCombTitle` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varLoadCombNo` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varLoadCombTitle` | [in] | Load case string title. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga8bb03f4733ba650a9df0a8fc16e4e523) |
| `varLoadCombNo` | [in] | Load case reference number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga8bb03f4733ba650a9df0a8fc16e4e523) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | Number ID, assigned to new load combination. |
| `-1` | General error. |

**Current call sites:** [CreateLoadCombinations — line 738](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L738).

#### AddLoadAndFactorToCombination

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_comb.html#ga6f75602769cefbbcce48fa0098130c2a) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_comb.html#ga6f75602769cefbbcce48fa0098130c2a) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga6f75602769cefbbcce48fa0098130c2a)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::AddLoadAndFactorToCombination(
    const VARIANT FAR&varLoadCombNo,
    const VARIANT FAR&varLoadNo,
    const VARIANT FAR&varFactor
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varLoadCombNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varLoadNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varFactor` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varLoadCombNo` | [in] | (Combination) Load case reference number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga6f75602769cefbbcce48fa0098130c2a) |
| `varLoadNo` | [in] | (Primary) Load case reference number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga6f75602769cefbbcce48fa0098130c2a) |
| `varFactor` | [in] | Multiplication factor for the specified primary load case. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga6f75602769cefbbcce48fa0098130c2a) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Current call sites:** [CreateLoadCombinations — line 740](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L740).

#### GetNoOfLoadAndFactorPairsForCombination

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_comb.html#ga3f68ee33ca96f501438b78f008da34c8) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_comb.html#ga3f68ee33ca96f501438b78f008da34c8) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga3f68ee33ca96f501438b78f008da34c8)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetNoOfLoadAndFactorPairsForCombination(
    const VARIANT FAR&varLoadCombNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varLoadCombNo` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varLoadCombNo` | [in] | (Combination) Load case reference number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga3f68ee33ca96f501438b78f008da34c8) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

No return-value table is present. Read the return prose in the linked reference; do not infer Boolean success or invent a payload.

**Current call sites:** [GetLoadCombination — line 622](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L622).

#### GetLoadAndFactorForCombination

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_comb.html#ga02d917d448cea428dae77061868f19eb) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_comb.html#ga02d917d448cea428dae77061868f19eb) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga02d917d448cea428dae77061868f19eb)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetLoadAndFactorForCombination(
    const VARIANT FAR&varLoadCombNo,
    VARIANT FAR&varLoadNos,
    VARIANT FAR&varFactors
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varLoadCombNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varLoadNos` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `varFactors` | `VARIANT FAR&` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varLoadCombNo` | [in] | (Combination) Load case reference number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga02d917d448cea428dae77061868f19eb) |
| `varLoadNos` | [out] | (Primary) Load case reference number ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga02d917d448cea428dae77061868f19eb) |
| `varFactors` | [out] | Multiplication factor(s) VARIANT array. For SRSS allocate size of this array as no. of primary load cases + 1. The last value will return the overall multiplication factor for SRSS combination. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga02d917d448cea428dae77061868f19eb) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `True` | if the method is successful. |
| `False` | if the method is unsuccessful. |

**Current call sites:** [GetLoadCombination — line 628](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L628).

#### IsCombinationCase

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_comb.html#gad07c8023e20a2452b79f0ac5a394a2d2) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_comb.html#gad07c8023e20a2452b79f0ac5a394a2d2) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gad07c8023e20a2452b79f0ac5a394a2d2)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L5.

**Managed capability:** Derived capability. GetAllLoadCombinationCases().Any(c => c.Id == targetId), or GetLoadCase(targetId, LoadCaseType.LoadCombination) != null, answers combination membership from a fresh native inventory. A dedicated predicate/status-result API is absent, but the user capability exists.

```cpp
VARIANT OSLoadUI::IsCombinationCase(
    long nLoadCase
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nLoadCase` | `long` | [in] | `System.Int32` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nLoadCase` | [in] | Load case reference ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gad07c8023e20a2452b79f0ac5a394a2d2) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `1` | YES. |
| `0` | NO. |
| `-1` | General error. |

**Proposed managed entry:**

```csharp
LoadQueryResult<bool> IsCombinationCase(int loadCaseId);
```

**Implementation consideration:** Distinguish 1=true, 0=false and -1=error; false is valid data.

#### AddAutoLoadCombinations

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_comb.html#ga0c6fba1b5eca1c912d70b36cbcac5763) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_comb.html#ga0c6fba1b5eca1c912d70b36cbcac5763) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga0c6fba1b5eca1c912d70b36cbcac5763)

**Comparison:** Status. **Coverage:** N — no current production dispatch; planned phase L5.

**Managed capability:** Absent. CreateLoadCombinations consumes caller-supplied definitions/factors. It does not generate code/category-based automatic combinations or return their generated IDs.

```cpp
VARIANT OSLoadUI::AddAutoLoadCombinations(
    const VARIANT FAR&varCode,
    const VARIANT FAR&varCategory,
    const VARIANT FAR&varLoadList,
    VARIANT FAR&varStartLoadCaseNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varCode` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varCategory` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varLoadList` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varStartLoadCaseNo` | `VARIANT FAR&` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varCode` | [in] | Load Combination Code string name (refer to "Codes.ini") (type - String). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga0c6fba1b5eca1c912d70b36cbcac5763) |
| `varCategory` | [in] | Load Combination Category string name (refer to corresponding rule ini file defined in "Codes.ini") (type - String). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga0c6fba1b5eca1c912d70b36cbcac5763) |
| `varLoadList` | [in] | Load case reference ID(s), Array of Load case numbers. If the array is either null or empty then all load cases in current model will be considered (type - Long/Integer). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga0c6fba1b5eca1c912d70b36cbcac5763) |
| `varStartLoadCaseNo` | [out] | (Combination) load case reference ID with which automatically load combination generation starts (type - Long/Integer). \t If nStartLoadCaseNo is valid, auto load combinations will be created from the provided ID. \t If nStartL… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga0c6fba1b5eca1c912d70b36cbcac5763) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | if successful. |
| `-8002` | load case not found |
| `-8040` | invalid load combination code name |
| `-8041` | invalid load combination category name |

**2024 return table:** `0` = if successful.; `-1` = if unsuccessful. The table above reflects 2025/2026. Preserve unknown failure codes.

**Proposed managed entry:**

```csharp
AutoCombinationResult CreateAutomaticCombinations(AutoCombinationRequest request);
```

**Implementation consideration:** Return requested and actual start IDs; interpret 2024 versus 2025/2026 status tables separately; snapshot supplied case IDs.

#### AddAutoCombinationRepeat

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_comb.html#gab0a905a41051c3ae5c0d1d6e0586f544) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_comb.html#gab0a905a41051c3ae5c0d1d6e0586f544) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gab0a905a41051c3ae5c0d1d6e0586f544)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L5.

**Managed capability:** Absent. AddRepeatLoadsTo consumes caller-supplied factors; EditorLoadCombination title helpers only format text. Neither performs native automatic repeat-load generation.

```cpp
VARIANT OSLoadUI::AddAutoCombinationRepeat(
    const VARIANT FAR&varCode,
    const VARIANT FAR&varCategory,
    const VARIANT FAR&varLoadList,
    VARIANT FAR&varStartLoadCaseNo,
    VARIANT FAR&VarGeneratedLCS,
    const VARIANT FAR&bVarReference,
    const VARIANT FAR&bVarNotional,
    const VARIANT FAR&dVarFactor,
    const VARIANT FAR&bVarGB50017,
    const VARIANT FAR&nVarFloor,
    const VARIANT FAR&bVarX,
    const VARIANT FAR&bVarNegtiveX,
    const VARIANT FAR&bVarZ,
    const VARIANT FAR&bVarNegtiveZ
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varCode` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varCategory` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varLoadList` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varStartLoadCaseNo` | `VARIANT FAR&` | [out] | `System.Object&` |
| 5 | `VarGeneratedLCS` | `VARIANT FAR&` | Not matched by name; see native reference | `System.Object&` |
| 6 | `bVarReference` | `const VARIANT FAR&` | [in] | `System.Object` |
| 7 | `bVarNotional` | `const VARIANT FAR&` | [in] | `System.Object` |
| 8 | `dVarFactor` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |
| 9 | `bVarGB50017` | `const VARIANT FAR&` | [in] | `System.Object` |
| 10 | `nVarFloor` | `const VARIANT FAR&` | [in] | `System.Object` |
| 11 | `bVarX` | `const VARIANT FAR&` | [in] | `System.Object` |
| 12 | `bVarNegtiveX` | `const VARIANT FAR&` | [in] | `System.Object` |
| 13 | `bVarZ` | `const VARIANT FAR&` | [in] | `System.Object` |
| 14 | `bVarNegtiveZ` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varCode` | [in] | Load Combination Code string name (refer to "Codes.ini") (type - String). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gab0a905a41051c3ae5c0d1d6e0586f544) |
| `varCategory` | [in] | Load Combination Category string name (refer to corresponding rule ini file defined in "Codes.ini") (type - String). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gab0a905a41051c3ae5c0d1d6e0586f544) |
| `varLoadList` | [in] | Load case reference ID(s), Array of Load case numbers. If the array is either null or empty then all load cases in current model will be considered (type - Long/Integer). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gab0a905a41051c3ae5c0d1d6e0586f544) |
| `varStartLoadCaseNo` | [out] | (Repeat Load) load case reference ID with which automatically generation starts (type - Long/Integer). \t If nStartLoadCaseNo is valid, auto repeat load will be created from the provided ID. \t If nStartLoadCaseNo is invalid Lo… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gab0a905a41051c3ae5c0d1d6e0586f544) |
| `varGeneratedLCS` | [out] | (Repeat Load) The counts of automatically generated repeat loads. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gab0a905a41051c3ae5c0d1d6e0586f544) |
| `bVarReference` | [in] | Whether include Reference load [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gab0a905a41051c3ae5c0d1d6e0586f544) |
| `bVarNotional` | [in] | Whether include Notional load. If it's True but all Directions are Flase, return -1. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gab0a905a41051c3ae5c0d1d6e0586f544) |
| `dVarNotionalLoadFactor` | [in] | If bVarNotional is valid, the value of Notional load factor [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gab0a905a41051c3ae5c0d1d6e0586f544) |
| `bVarGB50017` | [in] | Consider Notional load factor per GB 50017 Design code [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gab0a905a41051c3ae5c0d1d6e0586f544) |
| `nVarFloor` | [in] | The count of floor, it is valid when bVarGB50017 is True only [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gab0a905a41051c3ae5c0d1d6e0586f544) |
| `bVarX` | [in] | Consider X Direction of Notional Load [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gab0a905a41051c3ae5c0d1d6e0586f544) |
| `bVarNegtiveX` | [in] | Consider -X Direction of Notional Load [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gab0a905a41051c3ae5c0d1d6e0586f544) |
| `bVarZ` | [in] | Consider Z Direction of Notional Load [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gab0a905a41051c3ae5c0d1d6e0586f544) |
| `bVarNegtiveZ` | [in] | Consider -Z Direction of Notional Load [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gab0a905a41051c3ae5c0d1d6e0586f544) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | if successful. |
| `-1` | if unsuccessful |

**Proposed managed entry:**

```csharp
AutoRepeatCombinationResult CreateAutomaticRepeatLoads(AutoRepeatCombinationRequest request);
```

**Implementation consideration:** Return actual start ID and generated count from native by-reference slots; explicit all-cases selection because null/empty means all.

### Load Case Details: Load Case Operation

#### GetPrimaryLoadCaseNumbers

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga3e518083ebff1f7eda2bad62d3696c0c) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga3e518083ebff1f7eda2bad62d3696c0c) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga3e518083ebff1f7eda2bad62d3696c0c)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetPrimaryLoadCaseNumbers(
    VARIANT FAR&lCases
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `lCases` | `VARIANT FAR&` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `lCases` | [out] | The load case reference number ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga3e518083ebff1f7eda2bad62d3696c0c) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The number of primary load case(s). |
| `-1` | General error. |
| `-106` | 1 dimensional array of VARIANT expected. |
| `-114` | OLE Exception Occurred. |

**Current call sites:** [ReadAllLoadCases — line 2356](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2356).

#### CreateNewPrimaryLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga65f4045dbd028cd4233c11f9797e453e) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga65f4045dbd028cd4233c11f9797e453e) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga65f4045dbd028cd4233c11f9797e453e)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L5.

**Managed capability:** Covered alternative. Primary creation is implemented by CreateNewPrimaryLoad overloads using native Ex/Ex2, and by CreateNewLoadCase routing on CaseType. The unused title-only native entry point is an alternative, not a missing creation capability. Default classification and exact native status behavior are not asserted identical.

```cpp
VARIANT OSLoadUI::CreateNewPrimaryLoad(
    const VARIANT FAR&varPrimaryLoadTitle
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varPrimaryLoadTitle` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varPrimaryLoadTitle` | [in] | The load case string title. Value Load Type Value Load Type 0 Dead 12 Traffic 1 Live 13 Temp 2 Roof Live 14 Imperfection 3 Wind 15 Accidental 4 Seismic-H 16 Flood 5 Seismic-V 17 Ice 6 Snow 18 Wind Ice 7 Fluids 19 Crane Hook 8 S… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga65f4045dbd028cd4233c11f9797e453e) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | nLoadNo . |
| `-1` | General error. |
| `-8004` | Fail to create load. |

**Proposed managed entry:**

```csharp
LoadCaseCreationResult CreatePrimaryLoad(string title);
```

**Implementation consideration:** Optional simple form; existing typed Ex/Ex2 wrappers already cover richer creation. Preserve returned ID and native failure distinctions.

#### GetActiveLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga0333e9a3b2b913fe2bd20a70807dd917) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga0333e9a3b2b913fe2bd20a70807dd917) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga0333e9a3b2b913fe2bd20a70807dd917)

**Comparison:** Common. **Coverage:** S — shared LoadInspectionService dispatch.

**Managed capability:** Implemented through `GetActiveLoadCaseNumber`. Returns the positive native number or NativeFailure for -1. Primary/reference numbers can overlap; this result is not a LoadCase identity and performs no activation.

```cpp
VARIANT OSLoadUI::GetActiveLoad();
```

No native parameters.

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | Active load case number ID. |
| `-1` | General error. |

**Implemented managed entry:**

~~~csharp
LoadQueryResult<int> GetActiveLoadCaseNumber();
~~~

**Implementation contract:** Returns the positive native number or NativeFailure for -1. Primary/reference numbers can overlap; this result is not a LoadCase identity and performs no activation. See [Load inspection](../Load-Inspection-Queries.md) for shared activation, result and buffer rules.

#### GetLoadCaseTitle

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#gab2539a2b6546b0431bf1f871f5251365) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#gab2539a2b6546b0431bf1f871f5251365) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gab2539a2b6546b0431bf1f871f5251365)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetLoadCaseTitle(
    const VARIANT FAR&varLoadNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varLoadNo` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varLoadNo` | [in] | The load case string title. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gab2539a2b6546b0431bf1f871f5251365) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `<VARIANT>` | The load case string title. |
| `NONE` | Load case varLoadNo not found. |

**Summary/detailed declaration note:** The evidence data retains both forms, including parameter-name aliases. Do not bind by the help-table spelling; compare argument position/type and acquired signature.

**Current call sites:** [GetLoadCombination — line 625](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L625); [GetLoadCaseTitle — line 1837](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1837); [ReadLoadCaseMetadata — line 2373](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2373); [ReadCombinationCaseMetadata — line 2401](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2401).

#### SetLoadType

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga80c30f8628faf5a95ae2058d3dc7da91) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga80c30f8628faf5a95ae2058d3dc7da91) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga80c30f8628faf5a95ae2058d3dc7da91)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L5.

**Managed capability:** Absent. LoadCase.Type is managed metadata; setting it does not update an existing native case. Creation-time type selection is not reclassification.

```cpp
VARIANT OSLoadUI::SetLoadType(
    const VARIANT FAR&varLoadNo,
    const VARIANT FAR&varLoadType
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varLoadNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varLoadType` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varLoadNo` | [in] | The load case reference number ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga80c30f8628faf5a95ae2058d3dc7da91) |
| `varLoadType` | [in] | Type of the load. Value Load Type Value Load Type 0 Dead 12 Traffic 1 Live 13 Temp 2 Roof Live 14 Imperfection 3 Wind 15 Accidental 4 Seismic-H 16 Flood 5 Seismic-V 17 Ice 6 Snow 18 Wind Ice 7 Fluids 19 Crane Hook 8 Soil 20 Mas… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga80c30f8628faf5a95ae2058d3dc7da91) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Proposed managed entry:**

```csharp
LoadOperationResult SetLoadType(IEnumerable<int> loadCaseIds, LoadType loadType);
```

**Implementation consideration:** Reuse semantic LoadType only after native code verification; do not change case-family classification.

#### CreateNewPrimaryLoadEx

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#gafe08d35e098340b64412fc7af7cddef5) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#gafe08d35e098340b64412fc7af7cddef5) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gafe08d35e098340b64412fc7af7cddef5)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::CreateNewPrimaryLoadEx(
    const VARIANT FAR&varPrimaryLoadTitle,
    const VARIANT FAR&varLoadType
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varPrimaryLoadTitle` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varLoadType` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varPrimaryLoadTitle` | [in] | The primary load case string title. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gafe08d35e098340b64412fc7af7cddef5) |
| `varLoadType` | [in] | Type of the load. Value Load Type Value Load Type 0 Dead 12 Traffic 1 Live 13 Temp 2 Roof Live 14 Imperfection 3 Wind 15 Accidental 4 Seismic-H 16 Flood 5 Seismic-V 17 Ice 6 Snow 18 Wind Ice 7 Fluids 19 Crane Hook 8 Soil 20 Mas… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gafe08d35e098340b64412fc7af7cddef5) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | nLoadNo . |
| `-1` | General error. |
| `-8004` | Fail to create load. |

**Current call sites:** [CreateNewPrimaryLoad — line 1790](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1790).

#### GetLoadType

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#gae089cbb4d51b68b2ce6a39f82cf2e9cd) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#gae089cbb4d51b68b2ce6a39f82cf2e9cd) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gae089cbb4d51b68b2ce6a39f82cf2e9cd)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetLoadType(
    const VARIANT FAR&varLoadNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varLoadNo` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varLoadNo` | [in] | Primary load case reference ID. Pass in 0 to get information about current active load case or reference load case [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gae089cbb4d51b68b2ce6a39f82cf2e9cd) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | Dead. |
| `1` | Live. |
| `2` | Roof Live. |
| `3` | Wind. |
| `4` | Seismic-H. |
| `5` | Seismic-V. |
| `6` | Snow. |
| `7` | Fluids. |
| `8` | Soil. |
| `9` | Rain. |
| `10` | Ponding. |
| `11` | Dust. |
| `12` | Traffic. |
| `13` | Temperature. |
| `14` | Imperfection. |
| `15` | Accidental. |
| `16` | Flood. |
| `17` | Ice. |
| `18` | Wind Ice. |
| `19` | Crane Hook. |
| `20` | Mass. |
| `21` | Gravity. |
| `22` | Push. |
| `23` | None. |
| `-1` | General error. |

**Current call sites:** [ReadLoadCaseMetadata — line 2376](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2376).

#### CreateLoadList

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga7179492fe5d8a09023270b31fb703f00) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga7179492fe5d8a09023270b31fb703f00) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga7179492fe5d8a09023270b31fb703f00)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::CreateLoadList(
    const VARIANT FAR&varListType,
    const VARIANT FAR&varLoadCaseList
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varListType` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varLoadCaseList` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varListType` | [in] | Load list type: 0 and 1 for load list and load envelope list, respectively. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga7179492fe5d8a09023270b31fb703f00) |
| `varLoadCaseList` | [in] | Load case reference ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga7179492fe5d8a09023270b31fb703f00) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Summary/detailed declaration note:** The evidence data retains both forms, including parameter-name aliases. Do not bind by the help-table spelling; compare argument position/type and acquired signature.

**Current call sites:** [CreateLoadList — line 2300](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2300).

#### GetLoadListCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga5197fada7df8eb6345eef3983c9daca1) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga5197fada7df8eb6345eef3983c9daca1) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga5197fada7df8eb6345eef3983c9daca1)

**Comparison:** Common. **Coverage:** S — shared LoadInspectionService dispatch.

**Managed capability:** Implemented through `GetLoadListCount`. Zero is a valid count; -1 is NativeFailure. This is not an envelope count or a count of cases in an individual list.

```cpp
VARIANT OSLoadUI::GetLoadListCount();
```

No native parameters.

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The number of load list(s). |
| `-1` | General error. |

**Implemented managed entry:**

~~~csharp
LoadQueryResult<int> GetLoadListCount();
~~~

**Implementation contract:** Zero is a valid count; -1 is NativeFailure. This is not an envelope count or a count of cases in an individual list. See [Load inspection](../Load-Inspection-Queries.md) for shared activation, result and buffer rules.

#### GetLoadCountInLoadList

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#gabd66e32af1113e62601c497524654d9b) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#gabd66e32af1113e62601c497524654d9b) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gabd66e32af1113e62601c497524654d9b)

**Comparison:** Common. **Coverage:** S — shared LoadInspectionService dispatch.

**Managed capability:** Implemented through `GetLoadListCaseCount`. Validates the one-based list index against GetLoadListCount and rechecks that total after reading. Out-of-range lists are NotFound. The native count function has no documented negative error code; negative counts are rejected as malformed.

```cpp
VARIANT OSLoadUI::GetLoadCountInLoadList(
    const VARIANT FAR&varLoadListIndex
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varLoadListIndex` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varLoadListIndex` | [in] | Load list index. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gabd66e32af1113e62601c497524654d9b) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

No return-value table is present. Read the return prose in the linked reference; do not infer Boolean success or invent a payload.

**Implemented managed entry:**

~~~csharp
LoadQueryResult<int> GetLoadListCaseCount(int loadListIndex);
~~~

**Implementation contract:** Validates the one-based list index against GetLoadListCount and rechecks that total after reading. Out-of-range lists are NotFound. The native count function has no documented negative error code; negative counts are rejected as malformed. See [Load inspection](../Load-Inspection-Queries.md) for shared activation, result and buffer rules.

#### GetLoadsInLoadList

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga43ded5daa43065693c67f76dad7f022c) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga43ded5daa43065693c67f76dad7f022c) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga43ded5daa43065693c67f76dad7f022c)

**Comparison:** Common. **Coverage:** S — shared LoadInspectionService dispatch.

**Managed capability:** Implemented through `GetLoadListCases`. Uses an Int32 SAFEARRAY output passed internally by reference following installed CLR metadata, despite const wording in the C++ prototype. Preserves order/duplicates; does not infer case families. Rechecks list and member counts; detected changes return StateChanged. Native false is NativeFailure. Zero cases returns a read-only empty list without fetching.

```cpp
VARIANT OSLoadUI::GetLoadsInLoadList(
    const VARIANT FAR&varLoadListIndex,
    const VARIANT FAR&varLoadList
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varLoadListIndex` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varLoadList` | `const VARIANT FAR&` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varLoadListIndex` | [in] | Load list index(Starts from one). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga43ded5daa43065693c67f76dad7f022c) |
| `varLoadList` | [out] | Load Case reference ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga43ded5daa43065693c67f76dad7f022c) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `1` | / true Successful. |
| `0` | / false Failed. |

**Implemented managed entry:**

~~~csharp
LoadQueryResult<IEnumerable<int>> GetLoadListCases(int loadListIndex, int maximumResults = 100000);
~~~

**Implementation contract:** Uses an Int32 SAFEARRAY output passed internally by reference following installed CLR metadata, despite const wording in the C++ prototype. Preserves order/duplicates; does not infer case families. Rechecks list and member counts; detected changes return StateChanged. Native false is NativeFailure. Zero cases returns a read-only empty list without fetching. See [Load inspection](../Load-Inspection-Queries.md) for shared activation, result and buffer rules.

#### DeleteLoadList

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga42fbeecd03729b12aac29dd0b2c59bd0) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga42fbeecd03729b12aac29dd0b2c59bd0) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga42fbeecd03729b12aac29dd0b2c59bd0)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L5.

**Managed capability:** Absent. No native load-list deletion. Creating another list is not a verified deletion contract.

```cpp
VARIANT OSLoadUI::DeleteLoadList(
    const VARIANT FAR&varLoadListIndex
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varLoadListIndex` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varLoadListIndex` | [in] | Load list index. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga42fbeecd03729b12aac29dd0b2c59bd0) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Proposed managed entry:**

```csharp
LoadOperationResult DeleteLoadList(int loadListIndex);
```

**Implementation consideration:** List index is not an envelope ID or case ID; native zero means success.

#### SetASDLoadAttribute

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga7946c813af9dd0c7427730474355b9c6) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga7946c813af9dd0c7427730474355b9c6) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga7946c813af9dd0c7427730474355b9c6)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L5.

**Managed capability:** Absent. No ASD attribute mutation on an existing native load case.

```cpp
long OSLoadUI::SetASDLoadAttribute(
    long nLoadCase,
    StrengthType l,
    bool bIncrease
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nLoadCase` | `long` | [in] | `System.Int32` |
| 2 | `l` | `StrengthType` | [in] | `OpenSTAADUI.StrengthType` |
| 3 | `bIncrease` | `bool` | [in] | `System.Int32` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nLoadCase` | [in] | Load case reference ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga7946c813af9dd0c7427730474355b9c6) |
| `l` | [in] | Strength Type: Value STRENGTH_TYPE_NONE = 0 NORMAL_ASD_WORKING_STRESS_FORCES_WITHOUT_P_DELTA = 1 NORMAL_ASD_WORKING_STRESS_FORCES_WITH_P_DELTA = 2 STRENGTH_TYPE_OF_FORCES_WITHOUT_P_DELTA = 3 STRENGTH_TYPE_OF_FORCES_WITH_P_DELTA… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga7946c813af9dd0c7427730474355b9c6) |
| `bIncrease` | [in] | Allow 1/3 stress increase in ASD: FALSE or TRUE . [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga7946c813af9dd0c7427730474355b9c6) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Proposed managed entry:**

```csharp
LoadOperationResult SetAllowableStressAttribute(int loadCaseId, LoadStrengthType strengthType, bool increaseAllowableStress);
```

**Implementation consideration:** Use dedicated strength enum after values are checked against metadata/help.

#### SetLSDLoadAttribute

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#gad4c0b38a84da05308d4844d122df5135) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#gad4c0b38a84da05308d4844d122df5135) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gad4c0b38a84da05308d4844d122df5135)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L5.

**Managed capability:** Absent. No LSD attribute mutation on an existing native load case.

```cpp
long OSLoadUI::SetLSDLoadAttribute(
    long nLoadCase
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nLoadCase` | `long` | [in] | `System.Int32` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nLoadCase` | [in] | Load case reference ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gad4c0b38a84da05308d4844d122df5135) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Proposed managed entry:**

```csharp
LoadOperationResult SetLimitStateAttribute(int loadCaseId);
```

**Implementation consideration:** Native zero success; avoid implicit changes to model design code.

#### RemoveAttribute

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga9110cacabbaba9bc87e54fa1774885f3) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga9110cacabbaba9bc87e54fa1774885f3) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga9110cacabbaba9bc87e54fa1774885f3)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L5.

**Managed capability:** Absent. No native load-attribute removal.

```cpp
long OSLoadUI::RemoveAttribute(
    long lLoadCase
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `lLoadCase` | `long` | [in] | `System.Int32` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `lLoadCase` | [in] | Load case reference ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga9110cacabbaba9bc87e54fa1774885f3) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Proposed managed entry:**

```csharp
LoadOperationResult RemoveLoadAttribute(int loadCaseId);
```

**Implementation consideration:** Explicit attribute removal only.

#### GetAttribute

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#gace56ab4ec380cbf3061131ddd5e2d6a1) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#gace56ab4ec380cbf3061131ddd5e2d6a1) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gace56ab4ec380cbf3061131ddd5e2d6a1)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L5.

**Managed capability:** Absent. No native load-attribute reader. The help documents 0/-1 status without a usable attribute payload, so result semantics require clarification before implementation.

```cpp
long OSLoadUI::GetAttribute(
    long lLoadCase
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `lLoadCase` | `long` | [in] | `System.Int32` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `lLoadCase` | [in] | Load case reference ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gace56ab4ec380cbf3061131ddd5e2d6a1) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Proposed managed entry:**

```csharp
LoadQueryResult<LoadAttributeInfo> GetLoadAttribute(int loadCaseId);
```

**Implementation consideration:** Release blocked until result meaning is established: help documents status 0/-1 without explaining an attribute payload.

#### GetLoadTypeCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#gad2e249f1e4ba8d8d76f6a7698da94a2a) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#gad2e249f1e4ba8d8d76f6a7698da94a2a) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gad2e249f1e4ba8d8d76f6a7698da94a2a)

**Comparison:** Common. **Coverage:** S — shared LoadInspectionService dispatch.

**Managed capability:** Implemented through `GetLoadCommandCount`. Command kind is distinct from engineering LoadType and LoadItemKind. Native zero is documented as case not found and is returned as EmptyOrUnavailable, with no Data, rather than a proven empty inventory.

```cpp
long OSLoadUI::GetLoadTypeCount(
    int loadType
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `loadType` | `int` | [in] | `OpenSTAADUI.LoadType` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `loadType` | [in] | Type of the load. Value LoadType Value LoadType 4000 SelfWeight 3275 Uniform Force (Physical) 3110 Nodal Load (Node) 3280 Uniform Moment (Physical) 3120 Nodal Load (Inclined) 3285 Concentrated Force (Physical) 3910 Nodal Load (… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gad2e249f1e4ba8d8d76f6a7698da94a2a) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | the number of load(s). |
| `0` | Load Case loadCaseNo not found. |

**Implemented managed entry:**

~~~csharp
LoadQueryResult<int> GetLoadCommandCount(int primaryLoadCaseId, LoadCommandKind commandKind);
~~~

**Implementation contract:** Command kind is distinct from engineering LoadType and LoadItemKind. Native zero is documented as case not found and is returned as EmptyOrUnavailable, with no Data, rather than a proven empty inventory. See [Load inspection](../Load-Inspection-Queries.md) for shared activation, result and buffer rules.

#### GetListSizeForLoadType

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga8e2b9d3639488a9442eb4c4d1ec21e87) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga8e2b9d3639488a9442eb4c4d1ec21e87) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga8e2b9d3639488a9442eb4c4d1ec21e87)

**Comparison:** Common. **Coverage:** S — shared LoadInspectionService dispatch.

**Managed capability:** Implemented through `GetAssignedEntityCount`. Validates the zero-based command index against GetLoadTypeCount and rechecks that count afterwards. A zero command count is EmptyOrUnavailable; an index beyond a positive count is NotFound. A valid entity count can be zero. This returns a count, not the assignment-ID list.

```cpp
long OSLoadUI::GetListSizeForLoadType(
    int loadType,
    int loadIndex
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `loadType` | `int` | [in] | `OpenSTAADUI.LoadType` |
| 2 | `loadIndex` | `int` | [in] | `System.Int32` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `loadType` | [in] | Type of the load. Value LoadType Value LoadType 4000 SelfWeight 3275 Uniform Force (Physical) 3110 Nodal Load (Node) 3280 Uniform Moment (Physical) 3120 Nodal Load (Inclined) 3285 Concentrated Force (Physical) 3910 Nodal Load (… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga8e2b9d3639488a9442eb4c4d1ec21e87) |
| `loadIndex` | [in] | Load item index of specified load type (Zero based). Program returns the first one start from loadIndex if exist the same load type in specified load case. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga8e2b9d3639488a9442eb4c4d1ec21e87) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The number of entities. |

**Implemented managed entry:**

~~~csharp
LoadQueryResult<int> GetAssignedEntityCount(int primaryLoadCaseId, LoadCommandKind commandKind, int commandIndex);
~~~

**Implementation contract:** Validates the zero-based command index against GetLoadTypeCount and rechecks that count afterwards. A zero command count is EmptyOrUnavailable; an index beyond a positive count is NotFound. A valid entity count can be zero. This returns a count, not the assignment-ID list. See [Load inspection](../Load-Inspection-Queries.md) for shared activation, result and buffer rules.

#### GetAssignmentListForLoadType

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga35276014d19da32994c0adf4ab3e53eb) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga35276014d19da32994c0adf4ab3e53eb) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga35276014d19da32994c0adf4ab3e53eb)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L5.

**Managed capability:** Partial. GetNodalLoadsGroupedByLoadCase and GetMemberLoadsGroupedByLoadCase reconstruct definition/entity buckets for supplied nodes/beams and supported families. They require an entity scope and do not preserve native command identity, order or a general command-type/index assignment list.

```cpp
VARIANT OSLoadUI::GetAssignmentListForLoadType(
    const VARIANT FAR&loadType,
    const VARIANT FAR&loadIndex,
    VARIANT FAR&varList
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `loadType` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `loadIndex` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varList` | `VARIANT FAR&` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `loadType` | [in] | Type of the load. Value LoadType Value LoadType 4000 SelfWeight 3275 Uniform Force (Physical) 3110 Nodal Load (Node) 3280 Uniform Moment (Physical) 3120 Nodal Load (Inclined) 3285 Concentrated Force (Physical) 3910 Nodal Load (… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga35276014d19da32994c0adf4ab3e53eb) |
| `loadIndex` | [in] | Load item index of specified load type (Zero based). Program counts the first instance starting from 0 if the same load type exists in a specified load case. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga35276014d19da32994c0adf4ab3e53eb) |
| `varList` | [out] | Entities number ID(s) in VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga35276014d19da32994c0adf4ab3e53eb) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | Size of varList . |
| `0` | General error. |

**Proposed managed entry:**

```csharp
LoadQueryResult<IEnumerable<int>> GetAssignedEntityIds(NativeLoadCommandKind commandKind, int commandIndex);
```

**Implementation consideration:** Return independent IDs plus count/status interpretation; validate initialized buffer slots.

#### DeletePrimaryLoadCases

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga969b11631fb0bc3fcef22c4cc54b9017) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga969b11631fb0bc3fcef22c4cc54b9017) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga969b11631fb0bc3fcef22c4cc54b9017)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L5.

**Managed capability:** Absent. ClearPrimaryLoadCase/ClearPrimaryLoadCases remove load items; they do not implement deletion of the case definitions. The native deletion entry point also accepts a reference-family flag.

```cpp
VARIANT OSLoadUI::DeletePrimaryLoadCases(
    const VARIANT FAR&varPrimaryLoadCaseNos,
    const VARIANT FAR&varIsReferenceLoads
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varPrimaryLoadCaseNos` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varIsReferenceLoads` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varPrimaryLoadCaseNos` | [in] | Primary/Reference load case reference ID(s) (type - Long/Integer). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga969b11631fb0bc3fcef22c4cc54b9017) |
| `varIsReferenceLoads` | [in] | If reference load case(s): TRUE(1) or FALSE(0) (type - Boolean/Long/Integer). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga969b11631fb0bc3fcef22c4cc54b9017) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `1` | OK. |
| `0` | Failed to delete load(s) |

**Summary/detailed declaration note:** The evidence data retains both forms, including parameter-name aliases. Do not bind by the help-table spelling; compare argument position/type and acquired signature.

**Proposed managed entry:**

```csharp
LoadOperationResult DeleteLoadCases(LoadCaseFamily family, IEnumerable<int> loadCaseIds);
```

**Implementation consideration:** Use explicit family selection; expose primary/reference scope without an unexplained Boolean flag.

#### DeleteReferenceLoadCases

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga68d5bc5d7bbf772281bdec969832dd00) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga68d5bc5d7bbf772281bdec969832dd00) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga68d5bc5d7bbf772281bdec969832dd00)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L5.

**Managed capability:** Absent. ClearReferenceLoadCase/ClearReferenceLoadCases clear items; they do not implement reference-case definition deletion. A future shared managed deletion API may cover both native deletion alternatives.

```cpp
VARIANT OSLoadUI::DeleteReferenceLoadCases(
    const VARIANT FAR&varReferenceLoadCaseNos
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varReferenceLoadCaseNos` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varReferenceLoadCaseNos` | [in] | Reference load case reference ID(s) in VARIANT array, [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga68d5bc5d7bbf772281bdec969832dd00) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `1` | OK. |
| `0` | Failed to delete load(s) |

**Summary/detailed declaration note:** The evidence data retains both forms, including parameter-name aliases. Do not bind by the help-table spelling; compare argument position/type and acquired signature.

**Proposed managed entry:**

```csharp
LoadOperationResult DeleteReferenceLoadCases(IEnumerable<int> loadCaseIds);
```

**Implementation consideration:** Separate deletion from existing clear-content APIs; reject accidental destructive defaults.

#### ClearPrimaryLoadCase

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#gac60cdde591385334dbbfbc0e76da682c) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#gac60cdde591385334dbbfbc0e76da682c) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gac60cdde591385334dbbfbc0e76da682c)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::ClearPrimaryLoadCase(
    const VARIANT FAR&varLoadCaseNos,
    const VARIANT FAR&varIsReferenceLoads
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varLoadCaseNos` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varIsReferenceLoads` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varLoadCaseNos` | [in] | Primary load case reference ID(s) in VARIANT array, [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gac60cdde591385334dbbfbc0e76da682c) |
| `varIsReferenceLoads` | [in] | If reference load case(s): TRUE or FALSE . [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gac60cdde591385334dbbfbc0e76da682c) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `1` | OK. |
| `0` | Failed to delete load(s) |

**Summary/detailed declaration note:** The evidence data retains both forms, including parameter-name aliases. Do not bind by the help-table spelling; compare argument position/type and acquired signature.

**Current call sites:** [ClearPrimaryLoadCases — line 1731](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1731); [ClearPrimaryLoadCases — line 1754](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1754).

#### ClearReferenceLoadCase

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga5651ca43e11e3932abde242666b7f6ad) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga5651ca43e11e3932abde242666b7f6ad) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga5651ca43e11e3932abde242666b7f6ad)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::ClearReferenceLoadCase(
    const VARIANT FAR&varLoadCaseNos
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varLoadCaseNos` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varLoadCaseNos` | [in] | Primary load case reference ID(s) in VARIANT array, [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga5651ca43e11e3932abde242666b7f6ad) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `1` | OK. |
| `0` | Failed to delete load(s) |

**Current call sites:** [ClearReferenceLoadCases — line 1777](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1777).

#### CreateNewPrimaryLoadEx2

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga9246e3b7e18d22d556a9bbbd73572c6a) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga9246e3b7e18d22d556a9bbbd73572c6a) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga9246e3b7e18d22d556a9bbbd73572c6a)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::CreateNewPrimaryLoadEx2(
    const VARIANT FAR&varPrimaryLoadTitle,
    const VARIANT FAR&varLoadType,
    long nLoadCaseNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varPrimaryLoadTitle` | `const VARIANT FAR&` | [in] | `System.Object&` |
| 2 | `varLoadType` | `const VARIANT FAR&` | [in] | `System.Object&` |
| 3 | `nLoadCaseNo` | `long` | [in] | `System.Int32` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varPrimaryLoadTitle` | [in] | The primary load case string title. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga9246e3b7e18d22d556a9bbbd73572c6a) |
| `varLoadType` | [in] | Type of the load. Value Load Type Value Load Type 0 Dead 12 Traffic 1 Live 13 Temp 2 Roof Live 14 Imperfection 3 Wind 15 Accidental 4 Seismic-H 16 Flood 5 Seismic-V 17 Ice 6 Snow 18 Wind Ice 7 Fluids 19 Crane Hook 8 Soil 20 Mas… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga9246e3b7e18d22d556a9bbbd73572c6a) |
| `nLoadCaseNo` | [in] | The load case number. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga9246e3b7e18d22d556a9bbbd73572c6a) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | nLoadNo . |
| `0` | Failed to create load. |

**Current call sites:** [CreateNewPrimaryLoad — line 1799](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1799).

**Priority verification:** The inspected Ex2 declaration has by-reference title/type slots; the current call passes both by value. See the primary report; no implementation was changed.

#### GetLoadItemsCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga3597b11d8f108005e9be64e4761887e2) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga3597b11d8f108005e9be64e4761887e2) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga3597b11d8f108005e9be64e4761887e2)

**Comparison:** Common. **Coverage:** S — shared LoadInspectionService dispatch.

**Managed capability:** Implemented through `GetLoadItemCount`. Zero is valid and -1 is NativeFailure. Passes the case number directly without activation or family inference. Counts native items rather than per-entity duplicates or grouped managed definitions.

```cpp
VARIANT OSLoadUI::GetLoadItemsCount(
    long nLoadCaseNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nLoadCaseNo` | `long` | Not matched by name; see native reference | `System.Int32` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `loadCaseNo` | [in] | Load case number. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga3597b11d8f108005e9be64e4761887e2) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `-1` | General error. |

**Implemented managed entry:**

~~~csharp
LoadQueryResult<int> GetLoadItemCount(int loadCaseId);
~~~

**Implementation contract:** Zero is valid and -1 is NativeFailure. Passes the case number directly without activation or family inference. Counts native items rather than per-entity duplicates or grouped managed definitions. See [Load inspection](../Load-Inspection-Queries.md) for shared activation, result and buffer rules.

#### GetLoadItemType

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga90d695c8921e1e5eeb570ba37e1744fd) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga90d695c8921e1e5eeb570ba37e1744fd) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga90d695c8921e1e5eeb570ba37e1744fd)

**Comparison:** Common. **Coverage:** S — shared LoadInspectionService dispatch.

**Managed capability:** Implemented through `GetLoadItemKind`. Native zero is NotFound. Unknown positive codes are retained in the enum value; use Enum.IsDefined to test recognition. Code 3312 remains explicitly ambiguous between region nodal and partial plate pressure. Negative codes are malformed. No activation or family inference occurs.

```cpp
VARIANT OSLoadUI::GetLoadItemType(
    long nLoadCaseNo,
    long nLoadItemIndex
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nLoadCaseNo` | `long` | Not matched by name; see native reference | `System.Int32` |
| 2 | `nLoadItemIndex` | `long` | Not matched by name; see native reference | `System.Int32` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nloadCaseNo` | [in] | Load case reference ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga90d695c8921e1e5eeb570ba37e1744fd) |
| `nloadItemIndex` | [in] | Load item index (Zero based). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga90d695c8921e1e5eeb570ba37e1744fd) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | LoadCase/LoadItemIndex Not Found. |

**Implemented managed entry:**

~~~csharp
LoadQueryResult<LoadItemKind> GetLoadItemKind(int loadCaseId, int loadItemIndex);
~~~

**Implementation contract:** Native zero is NotFound. Unknown positive codes are retained in the enum value; use Enum.IsDefined to test recognition. Code 3312 remains explicitly ambiguous between region nodal and partial plate pressure. Negative codes are malformed. No activation or family inference occurs. See [Load inspection](../Load-Inspection-Queries.md) for shared activation, result and buffer rules.

### Load Items: SelfWeight

#### AddSelfWeightInXYZ

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_self_weight.html#gac54227b07c765ba1cb94fb1d1f4271af) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_self_weight.html#gac54227b07c765ba1cb94fb1d1f4271af) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_self_weight.html#gac54227b07c765ba1cb94fb1d1f4271af)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L6.

**Managed capability:** Partial. AddSelfweightInXYZTo already applies self-weight to explicit beam/plate/solid IDs. Geometry inventories can supply current IDs, but no integrated whole-model self-weight operation exists; an explicit list does not establish identical whole-model command scope or future-geometry behavior.

```cpp
VARIANT OSLoadUI::AddSelfWeightInXYZ(
    const VARIANT FAR&varInDirection,
    const VARIANT FAR&varLoadFactor
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varInDirection` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varLoadFactor` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varInDirection` | [in] | Self weight direction index (= 1, 2, 3 for X, Y, Z, respectively). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_self_weight.html#gac54227b07c765ba1cb94fb1d1f4271af) |
| `varLoadFactor` | [in] | Multiplying factor for self-weight. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_self_weight.html#gac54227b07c765ba1cb94fb1d1f4271af) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `true` | Add self weight successful. |
| `false` | General error. |

**Proposed managed entry:**

```csharp
LoadOperationResult AddSelfWeight(LoadCaseIdentity targetCase, SelfWeightDirection direction, double factor);
```

**Implementation consideration:** Whole-model operation; existing entity-scoped API is not identical. Check activation before assigning.

#### AddSelfWeightInXYZToGeometry

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_self_weight.html#gac7bd07b68033a341b2ccd05d66cb1c90) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_self_weight.html#gac7bd07b68033a341b2ccd05d66cb1c90) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_self_weight.html#gac7bd07b68033a341b2ccd05d66cb1c90)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::AddSelfWeightInXYZToGeometry(
    VARIANT FAR&varGeomNo,
    const VARIANT FAR&varInDirection,
    const VARIANT FAR&varLoadFactor
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varGeomNo` | `VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varInDirection` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varLoadFactor` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varGeomNo` | [in] | Beam, Plate or Solid number ID(s) VARIANT array(Type:Long Array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_self_weight.html#gac7bd07b68033a341b2ccd05d66cb1c90) |
| `varInDirection` | [in] | Self weight direction index (= 1, 2, 3 for X, Y, Z, respectively). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_self_weight.html#gac7bd07b68033a341b2ccd05d66cb1c90) |
| `varLoadFactor` | [in] | Multiplying factor for self-weight. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_self_weight.html#gac7bd07b68033a341b2ccd05d66cb1c90) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `true` | OK. |
| `false` | General error. |

**Current call sites:** [AddSelfweightInXYZTo — line 2230](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2230).

### Load Items: Nodal

#### AddNodalLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_nodal.html#gafd43c265f2925cc1f0a3c0c654c981c1) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_nodal.html#gafd43c265f2925cc1f0a3c0c654c981c1) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#gafd43c265f2925cc1f0a3c0c654c981c1)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::AddNodalLoad(
    const VARIANT FAR&varNodeNo,
    const VARIANT FAR&varFX,
    const VARIANT FAR&varFY,
    const VARIANT FAR&varFZ,
    const VARIANT FAR&varMX,
    const VARIANT FAR&varMY,
    const VARIANT FAR&varMZ
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varNodeNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varFX` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varFY` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varFZ` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varMX` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `varMY` | `const VARIANT FAR&` | [in] | `System.Object` |
| 7 | `varMZ` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varNodeNo` | [in] | Node number ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#gafd43c265f2925cc1f0a3c0c654c981c1) |
| `varFX` | [in] | Force in X direction. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#gafd43c265f2925cc1f0a3c0c654c981c1) |
| `varFY` | [in] | Force in Y direction. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#gafd43c265f2925cc1f0a3c0c654c981c1) |
| `varFZ` | [in] | Force in Z direction. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#gafd43c265f2925cc1f0a3c0c654c981c1) |
| `varMX` | [in] | Moment in X direction. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#gafd43c265f2925cc1f0a3c0c654c981c1) |
| `varMY` | [in] | Moment in Y direction. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#gafd43c265f2925cc1f0a3c0c654c981c1) |
| `varMZ` | [in] | Moment in Z direction. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#gafd43c265f2925cc1f0a3c0c654c981c1) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |
| `-106` | One dimensional array of long expected. |
| `-8004` | Library Error: Unable to add self weight. |
| `-8005` | Library Error: Unable to assign load. |

**Summary/detailed declaration note:** The evidence data retains both forms, including parameter-name aliases. Do not bind by the help-table spelling; compare argument position/type and acquired signature.

**Current call sites:** [AddNodalLoad — line 2005](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2005).

#### AddSupportDisplacement

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_nodal.html#gadbf5b06ad7b6120475c99d605c785118) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_nodal.html#gadbf5b06ad7b6120475c99d605c785118) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#gadbf5b06ad7b6120475c99d605c785118)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::AddSupportDisplacement(
    const VARIANT FAR&varNodeNo,
    const VARIANT FAR&varDirection,
    const VARIANT FAR&varDispValue
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varNodeNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varDirection` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varDispValue` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varNodeNo` | [in] | Node number ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#gadbf5b06ad7b6120475c99d605c785118) |
| `varDirection` | [in] | A number indicating direction: (= 1, 2, 3 for X, Y, Z direction, respectively). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#gadbf5b06ad7b6120475c99d605c785118) |
| `varDispValue` | [in] | Support displacement in given direction. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#gadbf5b06ad7b6120475c99d605c785118) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Current call sites:** [AddSupportSettlement — line 1922](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1922).

#### GetNodalLoadCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_nodal.html#gafe7311f8ee6677a22d22fde168a7c0dc) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_nodal.html#gafe7311f8ee6677a22d22fde168a7c0dc) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#gafe7311f8ee6677a22d22fde168a7c0dc)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetNodalLoadCount(
    const VARIANT FAR&nNodeNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeNo` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nNodeNo` | [in] | The number of node(s). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#gafe7311f8ee6677a22d22fde168a7c0dc) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The number of node(s). |
| `-1` | General error (perhaps load case not found). |

**Current call sites:** [GetNodalLoads — line 788](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L788).

#### GetNodalLoads

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_nodal.html#ga6ac19e170ef258bfd97ee9345850ebf1) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_nodal.html#ga6ac19e170ef258bfd97ee9345850ebf1) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#ga6ac19e170ef258bfd97ee9345850ebf1)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetNodalLoads(
    const VARIANT FAR&nNodeNo,
    VARIANT FAR&varFX,
    VARIANT FAR&varFY,
    VARIANT FAR&varFZ,
    VARIANT FAR&varMX,
    VARIANT FAR&varMY,
    VARIANT FAR&varMZ
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varFX` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `varFY` | `VARIANT FAR&` | [out] | `System.Object&` |
| 4 | `varFZ` | `VARIANT FAR&` | [out] | `System.Object&` |
| 5 | `varMX` | `VARIANT FAR&` | [out] | `System.Object&` |
| 6 | `varMY` | `VARIANT FAR&` | [out] | `System.Object&` |
| 7 | `varMZ` | `VARIANT FAR&` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nNodeNo` | [in] | The node number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#ga6ac19e170ef258bfd97ee9345850ebf1) |
| `varFX` | [out] | Force(s) in X direction (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#ga6ac19e170ef258bfd97ee9345850ebf1) |
| `varFY` | [out] | Force(s) in Y direction (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#ga6ac19e170ef258bfd97ee9345850ebf1) |
| `varFZ` | [out] | Force(s) in Z direction (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#ga6ac19e170ef258bfd97ee9345850ebf1) |
| `varMX` | [out] | Moment(s) about X direction (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#ga6ac19e170ef258bfd97ee9345850ebf1) |
| `varMY` | [out] | Moment(s) about Y direction (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#ga6ac19e170ef258bfd97ee9345850ebf1) |
| `varMZ` | [out] | Moment(s) about Z direction (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#ga6ac19e170ef258bfd97ee9345850ebf1) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Current call sites:** [GetNodalLoads — line 794](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L794).

#### GetNodalLoadInfo

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_nodal.html#ga53757ae693de6e733c1d842becdfccd9) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_nodal.html#ga53757ae693de6e733c1d842becdfccd9) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#ga53757ae693de6e733c1d842becdfccd9)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L6.

**Managed capability:** Partial. GetNodalLoads reads six force/moment components per supplied node; PopulateNodalLoads and grouping helpers also exist. They do not select or preserve the original native load-command index.

```cpp
VARIANT OSLoadUI::GetNodalLoadInfo(
    const VARIANT FAR&varloadIndex,
    VARIANT FAR&varForce
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varloadIndex` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |
| 2 | `varForce` | `VARIANT FAR&` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `loadIndex` | [in] | Load item index. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#ga53757ae693de6e733c1d842becdfccd9) |
| `varForce` | [out] | Nodal force VARIANT array: 0 to 5 for FX, FY, FZ, MX, MY and MZ, respectively. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#ga53757ae693de6e733c1d842becdfccd9) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `FALSE` | Failed |
| `TRUE` | success |

**Proposed managed entry:**

```csharp
LoadQueryResult<NodalLoadCommandInfo> GetNodalLoadCommand(LoadCaseIdentity targetCase, int loadItemIndex);
```

**Implementation consideration:** Read node force/moment components into named fields; do not expose mutable by-reference arrays.

### Load Items: Member

#### AddMemberUniformForce

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga2c1823f8c9b8348eff1b2efb08011201) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga2c1823f8c9b8348eff1b2efb08011201) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga2c1823f8c9b8348eff1b2efb08011201)

**Comparison:** Common. **Coverage:** E — delegated member-load dispatch.

```cpp
VARIANT OSLoadUI::AddMemberUniformForce(
    const VARIANT FAR&varBeamNo,
    const VARIANT FAR&varDirection,
    const VARIANT FAR&varForce,
    const VARIANT FAR&varD1,
    const VARIANT FAR&varD2,
    const VARIANT FAR&varD3
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varDirection` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varForce` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varD1` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varD2` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `varD3` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varBeamNo` | [in] | Member number ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga2c1823f8c9b8348eff1b2efb08011201) |
| `varDirection` | [in] | Load direction: (= 1 to 9 for LocalX, LocalY, LocalZ, GlobalX, GlobalY, GlobalZ, ProjectedX, ProjectedY, ProjectedZ, respectively). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga2c1823f8c9b8348eff1b2efb08011201) |
| `varForce` | [in] | Magnitude of the uniform force in current units. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga2c1823f8c9b8348eff1b2efb08011201) |
| `varD1` | [in] | Distance from the start of the member to the start of the load. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga2c1823f8c9b8348eff1b2efb08011201) |
| `varD2` | [in] | Distance from the start of the member to the end of the load. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga2c1823f8c9b8348eff1b2efb08011201) |
| `varD3` | [in] | Perpendicular distance from the member shear center to the local plane of loading. For additional information, please refer to Section 5.32.2 of the Technical Reference manual. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga2c1823f8c9b8348eff1b2efb08011201) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Current call sites:** [MemberUniformlyDistributedLoad.ApplyTo — line 84](../../src/StaadPro.Interop/Entities/MemberUniformlyDistributedLoad.cs#L84).

#### AddMemberUniformMoment

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga3da137976a73640b541a66c260693e48) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga3da137976a73640b541a66c260693e48) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga3da137976a73640b541a66c260693e48)

**Comparison:** Common. **Coverage:** E — delegated member-load dispatch.

```cpp
VARIANT OSLoadUI::AddMemberUniformMoment(
    const VARIANT FAR&varBeamNo,
    const VARIANT FAR&varDirection,
    const VARIANT FAR&varMoment,
    const VARIANT FAR&varD1,
    const VARIANT FAR&varD2,
    const VARIANT FAR&varD3
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varDirection` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varMoment` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varD1` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varD2` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `varD3` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varBeamNo` | [in] | Member number ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga3da137976a73640b541a66c260693e48) |
| `varDirection` | [in] | Load direction: (= 1 to 9 for LocalX, LocalY, LocalZ, GlobalX, GlobalY, GlobalZ, ProjectedX, ProjectedY, ProjectedZ, respectively). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga3da137976a73640b541a66c260693e48) |
| `varMoment` | [in] | Magnitude of the uniform moment in current units. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga3da137976a73640b541a66c260693e48) |
| `varD1` | [in] | Distance from the start of the member to the start of the load. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga3da137976a73640b541a66c260693e48) |
| `varD2` | [in] | Distance from the start of the member to the end of the load. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga3da137976a73640b541a66c260693e48) |
| `varD3` | [in] | Perpendicular distance from the member shear center to the local plane of loading. For additional information, please refer to Section 5.32.2 of the Technical Reference manual. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga3da137976a73640b541a66c260693e48) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Current call sites:** [MemberUniformMoment.ApplyTo — line 75](../../src/StaadPro.Interop/Entities/MemberUniformMoment.cs#L75).

#### AddMemberConcForce

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#gaa6fd59d87e1e940b83400684a9a21e63) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#gaa6fd59d87e1e940b83400684a9a21e63) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaa6fd59d87e1e940b83400684a9a21e63)

**Comparison:** Common. **Coverage:** E — delegated member-load dispatch.

```cpp
VARIANT OSLoadUI::AddMemberConcForce(
    const VARIANT FAR&varBeamNo,
    const VARIANT FAR&varDirection,
    const VARIANT FAR&varForce,
    const VARIANT FAR&varD1,
    const VARIANT FAR&varD2
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varDirection` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varForce` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varD1` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varD2` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varBeamNo` | [in] | Member number ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaa6fd59d87e1e940b83400684a9a21e63) |
| `varDirection` | [in] | Load direction: (= 1 to 6 for LocalX, LocalY, LocalZ, GlobalX, GlobalY and GlobalZ, respectively). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaa6fd59d87e1e940b83400684a9a21e63) |
| `varForce` | [in] | Magnitude of the concentrate force in current units. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaa6fd59d87e1e940b83400684a9a21e63) |
| `varD1` | [in] | Distance from the start of the member to concentrated force or moment. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaa6fd59d87e1e940b83400684a9a21e63) |
| `varD2` | [in] | Perpendicular distance from the member shear center to the local plane of loading. For additional information, please refer to Section 5.32.2 of the Technical Reference manual. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaa6fd59d87e1e940b83400684a9a21e63) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Current call sites:** [MemberConcentratedLoad.ApplyTo — line 76](../../src/StaadPro.Interop/Entities/MemberConcentratedLoad.cs#L76).

#### AddMemberConcMoment

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga80f43e61a87b337b821f317ab5b712cc) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga80f43e61a87b337b821f317ab5b712cc) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga80f43e61a87b337b821f317ab5b712cc)

**Comparison:** Common. **Coverage:** E — delegated member-load dispatch.

```cpp
VARIANT OSLoadUI::AddMemberConcMoment(
    const VARIANT FAR&varBeamNo,
    const VARIANT FAR&varDirection,
    const VARIANT FAR&varMoment,
    const VARIANT FAR&varD1,
    const VARIANT FAR&varD2
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varDirection` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varMoment` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varD1` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varD2` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varBeamNo` | [in] | Member number ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga80f43e61a87b337b821f317ab5b712cc) |
| `varDirection` | [in] | Load direction: (= 1 to 6 for LocalX, LocalY, LocalZ, GlobalX, GlobalY and GlobalZ, respectively). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga80f43e61a87b337b821f317ab5b712cc) |
| `varMoment` | [in] | Magnitude of the concentrate moment in current units. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga80f43e61a87b337b821f317ab5b712cc) |
| `varD1` | [in] | Distance from the start of the member to concentrated force or moment. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga80f43e61a87b337b821f317ab5b712cc) |
| `varD2` | [in] | Perpendicular distance from the member shear center to the local plane of loading. For additional information, please refer to Section 5.32.2 of the Technical Reference manual. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga80f43e61a87b337b821f317ab5b712cc) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Current call sites:** [MemberConcentratedMoment.ApplyTo — line 67](../../src/StaadPro.Interop/Entities/MemberConcentratedMoment.cs#L67).

#### AddMemberLinearVari

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#gae3ca9258f043cd379cfd0255ee55eb7b) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#gae3ca9258f043cd379cfd0255ee55eb7b) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gae3ca9258f043cd379cfd0255ee55eb7b)

**Comparison:** Common. **Coverage:** E — delegated member-load dispatch.

```cpp
VARIANT OSLoadUI::AddMemberLinearVari(
    const VARIANT FAR&varBeamNo,
    const VARIANT FAR&varDirection,
    const VARIANT FAR&varW1,
    const VARIANT FAR&varW2,
    const VARIANT FAR&varW3
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varDirection` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varW1` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varW2` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varW3` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varBeamNo` | [in] | Member number ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gae3ca9258f043cd379cfd0255ee55eb7b) |
| `varDirection` | [in] | Load direction: (= 1 to 3 for local X, Y and Z, respectively). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gae3ca9258f043cd379cfd0255ee55eb7b) |
| `varW1` | [in] | Load at the start of the member. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gae3ca9258f043cd379cfd0255ee55eb7b) |
| `varW2` | [in] | Load at the end of the member. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gae3ca9258f043cd379cfd0255ee55eb7b) |
| `varW3` | [in] | Load in the middle of the member (for triangular load). For additional information, please refer to Section 5.32.2 of the Technical Reference manual. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gae3ca9258f043cd379cfd0255ee55eb7b) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |
| `-8001` | Load direction is invalid. |

**Current call sites:** [MemberLinearVaryingLoad.ApplyTo — line 68](../../src/StaadPro.Interop/Entities/MemberLinearVaryingLoad.cs#L68).

#### AddMemberTrapezoidal

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga8e3a9cf9619680e33c05618e7de30d52) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga8e3a9cf9619680e33c05618e7de30d52) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga8e3a9cf9619680e33c05618e7de30d52)

**Comparison:** Common. **Coverage:** E — delegated member-load dispatch.

```cpp
VARIANT OSLoadUI::AddMemberTrapezoidal(
    const VARIANT FAR&varBeamNo,
    const VARIANT FAR&varDirection,
    const VARIANT FAR&varW1,
    const VARIANT FAR&varW2,
    const VARIANT FAR&varD1,
    const VARIANT FAR&varD2
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varDirection` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varW1` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varW2` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varD1` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `varD2` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varBeamNo` | [in] | Member number ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga8e3a9cf9619680e33c05618e7de30d52) |
| `varDirection` | [in] | Load direction: (= 1 to 9 for LocalX, LocalY, LocalZ, GlobalX, GlobalY, GlobalZ, ProjectedX, ProjectedY, ProjectedZ, respectively). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga8e3a9cf9619680e33c05618e7de30d52) |
| `varW1` | [in] | Load at the start of the member. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga8e3a9cf9619680e33c05618e7de30d52) |
| `varW2` | [in] | Load at the end of the member. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga8e3a9cf9619680e33c05618e7de30d52) |
| `varD1` | [in] | Distance from the start of the member to loading starting point. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga8e3a9cf9619680e33c05618e7de30d52) |
| `varD2` | [in] | Distance from the start of the member to loading stopping point. If dD1 and dD2 are not given, the load is assumed to cover the full member length. For additional information, please refer to Section 5.32.2 of the Technical Ref… [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga8e3a9cf9619680e33c05618e7de30d52) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Current call sites:** [MemberTrapezoidalLoad.ApplyTo — line 73](../../src/StaadPro.Interop/Entities/MemberTrapezoidalLoad.cs#L73).

#### AddMemberAreaLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga3ead37c8f7e53bd37687be53194dccc4) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga3ead37c8f7e53bd37687be53194dccc4) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga3ead37c8f7e53bd37687be53194dccc4)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L6.

**Managed capability:** Absent. No built-in area-load assignment. Existing member uniform-force definitions have different engineering dimensions/semantics.

```cpp
VARIANT OSLoadUI::AddMemberAreaLoad(
    const VARIANT FAR&varBeamNo,
    const VARIANT FAR&varLoad
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varLoad` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varBeamNo` | [in] | Member number ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga3ead37c8f7e53bd37687be53194dccc4) |
| `varLoad` | [in] | Magnitude of the load value. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga3ead37c8f7e53bd37687be53194dccc4) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Proposed managed entry:**

```csharp
LoadOperationResult AddMemberAreaLoad(LoadCaseIdentity targetCase, IEnumerable<int> memberIds, double areaLoad);
```

**Implementation consideration:** Preserve engineering quantity and current model units; validate active-case routing.

#### AddMemberFixedEnd

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#gaaf4468c2960776a1a18df2d7df79340b) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#gaaf4468c2960776a1a18df2d7df79340b) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaaf4468c2960776a1a18df2d7df79340b)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L6.

**Managed capability:** Absent. No built-in fixed-end member-load definition or assignment. End actions in Output are results, not load assignments.

```cpp
VARIANT OSLoadUI::AddMemberFixedEnd(
    const VARIANT FAR&varBeamNo,
    const VARIANT FAR&varLoadStart,
    const VARIANT FAR&varLoadEnd
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varLoadStart` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varLoadEnd` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varBeamNo` | [in] | Member number ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaaf4468c2960776a1a18df2d7df79340b) |
| `varLoadStart` | [in] | Load at starting point, VARIANT array of 6 double elements, indexes from 0 to 5 stands for FX, FY, FZ, MX, MY, MZ. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaaf4468c2960776a1a18df2d7df79340b) |
| `varLoadEnd` | [in] | Load at stopping point, VARIANT array of 6 double elements, indexes from 0 to 5 stands for FX, FY, FZ, MX, MY, MZ. For additional information, please refer to Section 5.32.2 of the Technical Reference manual. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaaf4468c2960776a1a18df2d7df79340b) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Proposed managed entry:**

```csharp
LoadOperationResult AddMemberFixedEndLoad(LoadCaseIdentity targetCase, IEnumerable<int> memberIds, FixedEndLoadDefinition definition);
```

**Implementation consideration:** Use named start/end force and moment components with verified array ordering.

#### GetUDLLoadCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga91881f1b488a97a80c79f05d19c9acb1) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga91881f1b488a97a80c79f05d19c9acb1) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga91881f1b488a97a80c79f05d19c9acb1)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetUDLLoadCount(
    const VARIANT FAR&nBeamNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nBeamNo` | [in] | The beam number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga91881f1b488a97a80c79f05d19c9acb1) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The number of uniformly distributed load item(s) applied. |
| `-1` | General error. |

**Current call sites:** [GetMemberUniformlyDistributedLoads — line 846](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L846).

#### GetUDLLoads

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga37c0da9a1d06ec1a63b641c3023af35c) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga37c0da9a1d06ec1a63b641c3023af35c) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga37c0da9a1d06ec1a63b641c3023af35c)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetUDLLoads(
    const VARIANT FAR&nBeamNo,
    VARIANT FAR&varDirection,
    VARIANT FAR&varForce,
    VARIANT FAR&varD1,
    VARIANT FAR&varD2,
    VARIANT FAR&varD3
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varDirection` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `varForce` | `VARIANT FAR&` | [out] | `System.Object&` |
| 4 | `varD1` | `VARIANT FAR&` | [out] | `System.Object&` |
| 5 | `varD2` | `VARIANT FAR&` | [out] | `System.Object&` |
| 6 | `varD3` | `VARIANT FAR&` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nBeamNo` | [in] | Beam number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga37c0da9a1d06ec1a63b641c3023af35c) |
| `varDirection` | [out] | Load direction: 1 to 9 for LocalX, LocalY, LocalZ, GlobalX, GlobalY, GlobalZ, ProjectedX, ProjectedY, ProjectedZ, respectively (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga37c0da9a1d06ec1a63b641c3023af35c) |
| `varForce` | [out] | Magnitude of the uniform force in current units (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga37c0da9a1d06ec1a63b641c3023af35c) |
| `varD1` | [out] | Distance from the start of the member to the start of the load (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga37c0da9a1d06ec1a63b641c3023af35c) |
| `varD2` | [out] | Distance from the start of the member to the end of the load (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga37c0da9a1d06ec1a63b641c3023af35c) |
| `varD3` | [out] | Perpendicular distance from the member shear center to the local plane of loading (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga37c0da9a1d06ec1a63b641c3023af35c) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Current call sites:** [GetMemberUniformlyDistributedLoads — line 852](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L852).

#### GetUNIMomentCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga6aeeb7f578026b4ef92ad0f89212b2a4) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga6aeeb7f578026b4ef92ad0f89212b2a4) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga6aeeb7f578026b4ef92ad0f89212b2a4)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetUNIMomentCount(
    const VARIANT FAR&nBeamNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nBeamNo` | [in] | Beam number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga6aeeb7f578026b4ef92ad0f89212b2a4) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The number of uniformly distributed (UNI) moment item(s) applied. |
| `-1` | General error. |

**Current call sites:** [GetMemberUniformMoments — line 1148](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1148).

#### GetUNIMoments

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga6a32b9122fb5a9c15b664aeeb0961862) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga6a32b9122fb5a9c15b664aeeb0961862) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga6a32b9122fb5a9c15b664aeeb0961862)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetUNIMoments(
    const VARIANT FAR&nBeamNo,
    VARIANT FAR&varDirection,
    VARIANT FAR&varForce,
    VARIANT FAR&varD1,
    VARIANT FAR&varD2,
    VARIANT FAR&varD3
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varDirection` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `varForce` | `VARIANT FAR&` | [out] | `System.Object&` |
| 4 | `varD1` | `VARIANT FAR&` | [out] | `System.Object&` |
| 5 | `varD2` | `VARIANT FAR&` | [out] | `System.Object&` |
| 6 | `varD3` | `VARIANT FAR&` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nBeamNo` | [in] | The beam number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga6a32b9122fb5a9c15b664aeeb0961862) |
| `varDirection` | [out] | Load direction = 1 to 9 for LocalX, LocalY, LocalZ, GlobalX, GlobalY, GlobalZ, ProjectedX, ProjectedY, ProjectedZ, respectively (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga6a32b9122fb5a9c15b664aeeb0961862) |
| `varForce` | [out] | Magnitude of the uniform moment in current units (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga6a32b9122fb5a9c15b664aeeb0961862) |
| `varD1` | [out] | Distance from the start of the member to the start of the load (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga6a32b9122fb5a9c15b664aeeb0961862) |
| `varD2` | [out] | Distance from the start of the member to the end of the load (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga6a32b9122fb5a9c15b664aeeb0961862) |
| `varD3` | [out] | Perpendicular distance from the member shear center to the local plane of loading (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga6a32b9122fb5a9c15b664aeeb0961862) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Current call sites:** [GetMemberUniformMoments — line 1154](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1154).

#### GetTrapLoadCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga69350aaa0a2878dbbf9df1054f5f082e) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga69350aaa0a2878dbbf9df1054f5f082e) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga69350aaa0a2878dbbf9df1054f5f082e)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetTrapLoadCount(
    const VARIANT FAR&nBeamNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nBeamNo` | [in] | Beam number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga69350aaa0a2878dbbf9df1054f5f082e) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The number of trapezoidal load item(s) applied. |
| `-1` | General error. |

**Current call sites:** [GetMemberTrapezoidalLoads — line 1087](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1087).

#### GetTrapLoads

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#gad2ea85860050c89c48c74460358a65ca) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#gad2ea85860050c89c48c74460358a65ca) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gad2ea85860050c89c48c74460358a65ca)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetTrapLoads(
    const VARIANT FAR&nBeamNo,
    VARIANT FAR&varDirection,
    VARIANT FAR&varW1,
    VARIANT FAR&varW2,
    VARIANT FAR&varD1,
    VARIANT FAR&varD2
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varDirection` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `varW1` | `VARIANT FAR&` | [out] | `System.Object&` |
| 4 | `varW2` | `VARIANT FAR&` | [out] | `System.Object&` |
| 5 | `varD1` | `VARIANT FAR&` | [out] | `System.Object&` |
| 6 | `varD2` | `VARIANT FAR&` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nBeamNo` | [in] | Beam number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gad2ea85860050c89c48c74460358a65ca) |
| `varDirection` | [out] | Load direction = 1 to 9 for LocalX, LocalY, LocalZ, GlobalX, GlobalY, GlobalZ, ProjectedX, ProjectedY, ProjectedZ, respectively (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gad2ea85860050c89c48c74460358a65ca) |
| `varW1` | [out] | Load at the start of the member (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gad2ea85860050c89c48c74460358a65ca) |
| `varW2` | [out] | Load at the end of the member (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gad2ea85860050c89c48c74460358a65ca) |
| `varD1` | [out] | Distance from the start of the member to loading starting point (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gad2ea85860050c89c48c74460358a65ca) |
| `varD2` | [out] | Distance from the start of the member to loading stopping point (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gad2ea85860050c89c48c74460358a65ca) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK |
| `-1` | General error. |

**Current call sites:** [GetMemberTrapezoidalLoads — line 1093](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1093).

#### GetLinearVaryingLoadCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga7d655dfad4f7f4b445f87b8888ab212c) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga7d655dfad4f7f4b445f87b8888ab212c) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga7d655dfad4f7f4b445f87b8888ab212c)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
long OSLoadUI::GetLinearVaryingLoadCount(
    const VARIANT FAR&nBeamNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nBeamNo` | [in] | Beam number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga7d655dfad4f7f4b445f87b8888ab212c) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The number of linear varying load item(s) applied. |
| `-1` | General error. |

**Current call sites:** [GetMemberLinearVaryingLoads — line 1027](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1027).

#### GetLinearVaryingLoads

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga1fad2339bc39c95b60b75d110f0cd17e) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga1fad2339bc39c95b60b75d110f0cd17e) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga1fad2339bc39c95b60b75d110f0cd17e)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetLinearVaryingLoads(
    const VARIANT FAR&nBeamNo,
    VARIANT FAR&varDirection,
    VARIANT FAR&varW1,
    VARIANT FAR&varW2,
    VARIANT FAR&varW3
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varDirection` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `varW1` | `VARIANT FAR&` | [out] | `System.Object&` |
| 4 | `varW2` | `VARIANT FAR&` | [out] | `System.Object&` |
| 5 | `varW3` | `VARIANT FAR&` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nBeamNo` | [in] | Beam number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga1fad2339bc39c95b60b75d110f0cd17e) |
| `varDirection` | [out] | Load direction = 1 to 3 for local X, Y and Z, respectively (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga1fad2339bc39c95b60b75d110f0cd17e) |
| `varW1` | [out] | Load at the start of the member (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga1fad2339bc39c95b60b75d110f0cd17e) |
| `varW2` | [out] | Load at the end of the member (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga1fad2339bc39c95b60b75d110f0cd17e) |
| `varW3` | [out] | Load in the middle of the member (for triangular load) (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga1fad2339bc39c95b60b75d110f0cd17e) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK |
| `-1` | General error. |

**Current call sites:** [GetMemberLinearVaryingLoads — line 1034](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1034).

#### GetConcForceCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#gab1a51f83aa40e6178ba62688607c7877) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#gab1a51f83aa40e6178ba62688607c7877) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gab1a51f83aa40e6178ba62688607c7877)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetConcForceCount(
    const VARIANT FAR&nBeamNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nBeamNo` | [in] | Beam number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gab1a51f83aa40e6178ba62688607c7877) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The number of concentrated force item(s) applied. |
| `-1` | General error. |

**Summary/detailed declaration note:** The evidence data retains both forms, including parameter-name aliases. Do not bind by the help-table spelling; compare argument position/type and acquired signature.

**Current call sites:** [GetMemberConcentratedLoads — line 905](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L905).

#### GetConcForces

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga76ee32fe8074198ba6e64f446fea4aa0) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga76ee32fe8074198ba6e64f446fea4aa0) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga76ee32fe8074198ba6e64f446fea4aa0)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetConcForces(
    const VARIANT FAR&nBeamNo,
    VARIANT FAR&varDirection,
    VARIANT FAR&varForce,
    VARIANT FAR&varD1,
    VARIANT FAR&varD2
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varDirection` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `varForce` | `VARIANT FAR&` | [out] | `System.Object&` |
| 4 | `varD1` | `VARIANT FAR&` | [out] | `System.Object&` |
| 5 | `varD2` | `VARIANT FAR&` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nBeamNo` | [in] | Beam number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga76ee32fe8074198ba6e64f446fea4aa0) |
| `varDirection` | [out] | Load direction = 1 to 6 for LocalX, LocalY, LocalZ, GlobalX, GlobalY and GlobalZ, respectively (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga76ee32fe8074198ba6e64f446fea4aa0) |
| `varForce` | [out] | Magnitude of the concentrate force in current units (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga76ee32fe8074198ba6e64f446fea4aa0) |
| `varD1` | [out] | Distance from the start of the member to concentrated force or moment (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga76ee32fe8074198ba6e64f446fea4aa0) |
| `varD2` | [out] | Perpendicular distance from the member shear center to the local plane of loading (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga76ee32fe8074198ba6e64f446fea4aa0) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK |
| `-1` | General error. |

**Summary/detailed declaration note:** The evidence data retains both forms, including parameter-name aliases. Do not bind by the help-table spelling; compare argument position/type and acquired signature.

**Current call sites:** [GetMemberConcentratedLoads — line 911](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L911).

#### GetConcMomentCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#gaa25fc3980a9d39f787f0af3c16a661b6) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#gaa25fc3980a9d39f787f0af3c16a661b6) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaa25fc3980a9d39f787f0af3c16a661b6)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetConcMomentCount(
    const VARIANT FAR&nBeamNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nBeamNo` | [in] | Beam number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaa25fc3980a9d39f787f0af3c16a661b6) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The number of concentrated moment item(s) applied. |
| `-1` | General error. |

**Summary/detailed declaration note:** The evidence data retains both forms, including parameter-name aliases. Do not bind by the help-table spelling; compare argument position/type and acquired signature.

**Current call sites:** [GetMemberConcentratedMoments — line 968](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L968).

#### GetConcMoments

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga35e3e090648f7acf2f1b6dd40bf07ba0) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga35e3e090648f7acf2f1b6dd40bf07ba0) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga35e3e090648f7acf2f1b6dd40bf07ba0)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetConcMoments(
    const VARIANT FAR&nBeamNo,
    VARIANT FAR&varDirection,
    VARIANT FAR&varMoment,
    VARIANT FAR&varD1,
    VARIANT FAR&varD2
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varDirection` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `varMoment` | `VARIANT FAR&` | [out] | `System.Object&` |
| 4 | `varD1` | `VARIANT FAR&` | [out] | `System.Object&` |
| 5 | `varD2` | `VARIANT FAR&` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nBeamNo` | [in] | Beam number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga35e3e090648f7acf2f1b6dd40bf07ba0) |
| `varDirection` | [out] | Load direction = 1 to 6 for LocalX, LocalY, LocalZ, GlobalX, GlobalY and GlobalZ, respectively (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga35e3e090648f7acf2f1b6dd40bf07ba0) |
| `varMoment` | [out] | Magnitude of the concentrate moment in current units (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga35e3e090648f7acf2f1b6dd40bf07ba0) |
| `varD1` | [out] | Distance from the start of the member to concentrated force or moment (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga35e3e090648f7acf2f1b6dd40bf07ba0) |
| `varD2` | [out] | Perpendicular distance from the member shear center to the local plane of loading (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga35e3e090648f7acf2f1b6dd40bf07ba0) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK |
| `-1` | General error. |

**Summary/detailed declaration note:** The evidence data retains both forms, including parameter-name aliases. Do not bind by the help-table spelling; compare argument position/type and acquired signature.

**Current call sites:** [GetMemberConcentratedMoments — line 974](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L974).

#### GetMemberLoadInfo

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#gaa1df58fb5514e4c8814edb8e7987facd) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#gaa1df58fb5514e4c8814edb8e7987facd) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaa1df58fb5514e4c8814edb8e7987facd)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L6.

**Managed capability:** Partial. Six typed per-member readers and population/grouping helpers already return supported member force/moment definitions and positions. They do not expose the original generic command-index lookup or every native command family.

```cpp
VARIANT OSLoadUI::GetMemberLoadInfo(
    const VARIANT FAR&varloadIndex,
    VARIANT FAR&varDir,
    VARIANT FAR&varForce,
    VARIANT FAR&varDist
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varloadIndex` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |
| 2 | `varDir` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `varForce` | `VARIANT FAR&` | [out] | `System.Object&` |
| 4 | `varDist` | `VARIANT FAR&` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `loadIndex` | [in] | Load item index (Zero based) (Type - Integer/Long). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaa1df58fb5514e4c8814edb8e7987facd) |
| `varDir` | [out] | Load direction: (= 1 to 9 for LocalX, LocalY, LocalZ, GlobalX, GlobalY, GlobalZ, ProjectedX, ProjectedY, ProjectedZ, respectively) (type - Integer/Long). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaa1df58fb5514e4c8814edb8e7987facd) |
| `varForce` | [out] | Member force parameters VARIANT array: dW1, dW2 and dW3 (see commands for add member force) (type - double Array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaa1df58fb5514e4c8814edb8e7987facd) |
| `varDist` | [out] | Member force distances VARIANT array: dD1, dD2 and dD3 (see commands for add member force) (type - double Array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaa1df58fb5514e4c8814edb8e7987facd) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `FALSE` | Failed |
| `TRUE` | success |

**Summary/detailed declaration note:** The evidence data retains both forms, including parameter-name aliases. Do not bind by the help-table spelling; compare argument position/type and acquired signature.

**Proposed managed entry:**

```csharp
LoadQueryResult<MemberLoadCommandInfo> GetMemberLoadCommand(LoadCaseIdentity targetCase, int loadItemIndex);
```

**Implementation consideration:** Return direction, forces and distances as typed definition data; not an entity assignment query.

### Load Items: Physical Member

No native function declarations occur in this reviewed grouping page. Subgroups, where present, were traversed separately.

### Load Items: Element

#### AddElementPressure

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_ele.html#gaeec2ec071bdd84d6139282dc089b83a2) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_ele.html#gaeec2ec071bdd84d6139282dc089b83a2) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gaeec2ec071bdd84d6139282dc089b83a2)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::AddElementPressure(
    const VARIANT FAR&varPlateNo,
    const VARIANT FAR&varDirection,
    const VARIANT FAR&varPressure,
    const VARIANT FAR&varX1,
    const VARIANT FAR&varY1,
    const VARIANT FAR&varX2,
    const VARIANT FAR&varY2
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varPlateNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varDirection` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varPressure` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varX1` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varY1` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `varX2` | `const VARIANT FAR&` | [in] | `System.Object` |
| 7 | `varY2` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varPlateNo` | [in] | Plate number ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gaeec2ec071bdd84d6139282dc089b83a2) |
| `varDirection` | [in] | Load direction: (= 1 to 9 for LocalX, LocalY, LocalZ, GlobalX, GlobalY, GlobalZ, ProjectedX, ProjectedY and ProjectedZ respectively). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gaeec2ec071bdd84d6139282dc089b83a2) |
| `varPressure` | [in] | Magnitude of the pressure or concentrate load on the element. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gaeec2ec071bdd84d6139282dc089b83a2) |
| `varX1` | [in] | Top-Left coordinate X (local). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gaeec2ec071bdd84d6139282dc089b83a2) |
| `varY1` | [in] | Top-Left coordinate Y (local). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gaeec2ec071bdd84d6139282dc089b83a2) |
| `varX2` | [in] | Bottom-Right coordinate X (local). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gaeec2ec071bdd84d6139282dc089b83a2) |
| `varY2` | [in] | Bottom-Right coordinate Y (local). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gaeec2ec071bdd84d6139282dc089b83a2) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |
| `-8001` | Load direction is invalid. |

**Current call sites:** [AddPlateUniformPressure — line 2061](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2061).

**Priority verification:** Current call supplies three arguments; the inspected declaration requires seven. See the primary report; no implementation was changed.

#### AddElementHydrostaticPressure

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_ele.html#gac5b8a98ce4cceb2bea7775adb2b62ba2) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_ele.html#gac5b8a98ce4cceb2bea7775adb2b62ba2) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gac5b8a98ce4cceb2bea7775adb2b62ba2)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L6.

**Managed capability:** Absent. No plate hydrostatic pressure definition/assignment. Uniform plate pressure and seismic member weights do not provide this operation.

```cpp
VARIANT OSLoadUI::AddElementHydrostaticPressure(
    const VARIANT FAR&varPlateNo,
    const VARIANT FAR&varLoadDirection,
    const VARIANT FAR&varInterpolateDirection,
    const VARIANT FAR&varMinLoad,
    const VARIANT FAR&varMaxLoad
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varPlateNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varLoadDirection` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varInterpolateDirection` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varMinLoad` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varMaxLoad` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varPlateNo` | [in] | Plate number ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gac5b8a98ce4cceb2bea7775adb2b62ba2) |
| `varLoadDirection` | [in] | Load direction: (= 3 to 6 for LocalZ, GlobalX, GlobalY, GlobalZ, respectively)(type - Integer) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gac5b8a98ce4cceb2bea7775adb2b62ba2) |
| `varInterpolateDirection` | [in] | Interpolate along Global Axis(Int or Long), valid direction codes are 1, 2, 3 for Interpolate along Global X, Y, Z. No other direction is a valid input. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gac5b8a98ce4cceb2bea7775adb2b62ba2) |
| `varMinLoad` | [in] | minimum Pressure load (double). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gac5b8a98ce4cceb2bea7775adb2b62ba2) |
| `varMaxLoad` | [in] | maximum Pressure load (double). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gac5b8a98ce4cceb2bea7775adb2b62ba2) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `1/True` | OK. |
| `0/False` | General error. |

**Summary/detailed declaration note:** The evidence data retains both forms, including parameter-name aliases. Do not bind by the help-table spelling; compare argument position/type and acquired signature.

**Proposed managed entry:**

```csharp
LoadOperationResult AddHydrostaticPressure(LoadCaseIdentity targetCase, IEnumerable<int> plateIds, HydrostaticPressureDefinition definition);
```

**Implementation consideration:** Separate pressure direction from interpolation direction and preserve min/max units.

#### AddElementTrapPressureEx

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_ele.html#ga829edeccde827aef75e1d8fb8e297cdf) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_ele.html#ga829edeccde827aef75e1d8fb8e297cdf) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga829edeccde827aef75e1d8fb8e297cdf)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L6.

**Managed capability:** Absent. AddPlateUniformPressure implements uniform pressure, not a varying/corner-pressure definition. The uniform special case does not cover this contract.

```cpp
VARIANT OSLoadUI::AddElementTrapPressureEx(
    const VARIANT FAR&varPlateNo,
    const VARIANT FAR&varLoadDirection,
    const VARIANT FAR&varLoadVaryDirection,
    const VARIANT FAR&varStartPressure,
    const VARIANT FAR&varEndPressure,
    const VARIANT FAR&varPressure3,
    const VARIANT FAR&varPressure4
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varPlateNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varLoadDirection` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varLoadVaryDirection` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varStartPressure` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varEndPressure` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `varPressure3` | `const VARIANT FAR&` | [in] | `System.Object` |
| 7 | `varPressure4` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varPlateNo` | [in] | Plate number ID(s) (type - VARIANT array of.Long) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga829edeccde827aef75e1d8fb8e297cdf) |
| `varLoadDirection` | [in] | Load direction: (= 3 to 6 for LocalZ, GlobalX, GlobalY, GlobalZ, respectively)(type - Integer) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga829edeccde827aef75e1d8fb8e297cdf) |
| `varLoadVaryDirection` | [in] | Load varying direction: (= 1, 2 ,3 for X, Y and JOINT respectively) (type - Integer) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga829edeccde827aef75e1d8fb8e297cdf) |
| `varStartPressure` | [in] | Pressure at loading starting point.(Node1 when JOINT is selected) (type - Double) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga829edeccde827aef75e1d8fb8e297cdf) |
| `varEndPressure` | [in] | Pressure at loading ending point.(Node2 when JOINT is selected) (type - Double) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga829edeccde827aef75e1d8fb8e297cdf) |
| `varPressure3` | [in] | Pressure at loading point.(applicable only when JOINT is selected) (type - Double) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga829edeccde827aef75e1d8fb8e297cdf) |
| `varPressure4` | [in] | Pressure at loading point.(applicable only when JOINT is selected) (type - Double) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga829edeccde827aef75e1d8fb8e297cdf) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `1/True` | OK. |
| `0/False` | General error. |

**Summary/detailed declaration note:** The evidence data retains both forms, including parameter-name aliases. Do not bind by the help-table spelling; compare argument position/type and acquired signature.

**Proposed managed entry:**

```csharp
LoadOperationResult AddTrapezoidalPressure(LoadCaseIdentity targetCase, IEnumerable<int> plateIds, TrapezoidalPressureDefinition definition);
```

**Implementation consideration:** Validate variation/corner-pressure layout without flattening semantically different directions.

#### GetElementPressureLoadCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_ele.html#gac739a3b9dff3d045724072650172bf03) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_ele.html#gac739a3b9dff3d045724072650172bf03) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gac739a3b9dff3d045724072650172bf03)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L6.

**Managed capability:** Absent. No plate-pressure load count reader. AddPlateUniformPressure is a write operation; plate Output stress queries are analysis results.

```cpp
VARIANT OSLoadUI::GetElementPressureLoadCount(
    const VARIANT FAR&varPlateNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varPlateNo` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varPlateNo` | [in] | Plate number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gac739a3b9dff3d045724072650172bf03) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The number of pressure load(s). |
| `-1` | General error. |

**Proposed managed entry:**

```csharp
LoadQueryResult<int> GetPlatePressureLoadCount(LoadCaseIdentity targetCase, int plateId);
```

**Implementation consideration:** Count and fetch require the same active case; existing AddPlateUniformPressure is a separate mutation.

#### GetElementPressureLoads

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_ele.html#ga29a49308a9a4be3345a5dde809ccd6cf) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_ele.html#ga29a49308a9a4be3345a5dde809ccd6cf) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga29a49308a9a4be3345a5dde809ccd6cf)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L6.

**Managed capability:** Absent. No plate-pressure definition read-back, including direction, pressure and local rectangle coordinates.

```cpp
VARIANT OSLoadUI::GetElementPressureLoads(
    const VARIANT FAR&varPlateNo,
    VARIANT FAR&varDirection,
    VARIANT FAR&varW1,
    VARIANT FAR&varX1,
    VARIANT FAR&varY1,
    VARIANT FAR&varX2,
    VARIANT FAR&varY2
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varPlateNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varDirection` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `varW1` | `VARIANT FAR&` | [out] | `System.Object&` |
| 4 | `varX1` | `VARIANT FAR&` | [out] | `System.Object&` |
| 5 | `varY1` | `VARIANT FAR&` | [out] | `System.Object&` |
| 6 | `varX2` | `VARIANT FAR&` | [out] | `System.Object&` |
| 7 | `varY2` | `VARIANT FAR&` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varPlateNo` | [in] | Plate number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga29a49308a9a4be3345a5dde809ccd6cf) |
| `varDirection` | [out] | Load direction = 1 to 9 for LocalX, LocalY, LocalZ, GlobalX, GlobalY, GlobalZ, ProjectedX, ProjectedY and ProjectedZ respectively (in VARIANT array). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga29a49308a9a4be3345a5dde809ccd6cf) |
| `varW1` | [out] | Magnitude of the pressure load(s) in VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga29a49308a9a4be3345a5dde809ccd6cf) |
| `varX1` | [out] | Top-Left coordinate X (local) in VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga29a49308a9a4be3345a5dde809ccd6cf) |
| `varY1` | [out] | Top-Left coordinate Y (local) in VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga29a49308a9a4be3345a5dde809ccd6cf) |
| `varX2` | [out] | Bottom-Right coordinate X (local) in VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga29a49308a9a4be3345a5dde809ccd6cf) |
| `varY2` | [out] | Bottom-Right coordinate Y (local) in VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga29a49308a9a4be3345a5dde809ccd6cf) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Proposed managed entry:**

```csharp
LoadQueryResult<IEnumerable<PlatePressureLoad>> GetPlatePressureLoads(LoadCaseIdentity targetCase, int plateId);
```

**Implementation consideration:** Return named pressure/rectangle coordinates with exact buffer counts, lower-bound handling and finite data.

#### GetElementConcLoadCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_ele.html#ga86fa47a705ec3bd23b1a8ef5ab4353b6) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_ele.html#ga86fa47a705ec3bd23b1a8ef5ab4353b6) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga86fa47a705ec3bd23b1a8ef5ab4353b6)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L6.

**Managed capability:** Absent. No plate concentrated-load count reader.

```cpp
VARIANT OSLoadUI::GetElementConcLoadCount(
    const VARIANT FAR&varPlateNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varPlateNo` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varPlateNo` | [in] | Plate number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga86fa47a705ec3bd23b1a8ef5ab4353b6) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The number of concentrated load on specified plate. |
| `-1` | General error. |

**Proposed managed entry:**

```csharp
LoadQueryResult<int> GetPlateConcentratedLoadCount(LoadCaseIdentity targetCase, int plateId);
```

**Implementation consideration:** Valid zero count returns successful empty query data through the paired reader.

#### GetElementConcLoads

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_ele.html#ga68a485fc7281ecf5e60ce93672d6445d) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_ele.html#ga68a485fc7281ecf5e60ce93672d6445d) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga68a485fc7281ecf5e60ce93672d6445d)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L6.

**Managed capability:** Absent. No plate concentrated-load definition reader.

```cpp
VARIANT OSLoadUI::GetElementConcLoads(
    const VARIANT FAR&varPlateNo,
    VARIANT FAR&varDirection,
    VARIANT FAR&varW1,
    VARIANT FAR&varX1,
    VARIANT FAR&varY1
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varPlateNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varDirection` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `varW1` | `VARIANT FAR&` | [out] | `System.Object&` |
| 4 | `varX1` | `VARIANT FAR&` | [out] | `System.Object&` |
| 5 | `varY1` | `VARIANT FAR&` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varPlateNo` | [in] | Plate number ID [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga68a485fc7281ecf5e60ce93672d6445d) |
| `varDirection` | [out] | If non-null, the direction. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga68a485fc7281ecf5e60ce93672d6445d) |
| `varW1` | [out] | If non-null, the pressure. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga68a485fc7281ecf5e60ce93672d6445d) |
| `varX1` | [out] | If non-null, the first d x coordinate. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga68a485fc7281ecf5e60ce93672d6445d) |
| `varY1` | [out] | If non-null, the first d y coordinate. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga68a485fc7281ecf5e60ce93672d6445d) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Proposed managed entry:**

```csharp
LoadQueryResult<IEnumerable<PlateConcentratedLoad>> GetPlateConcentratedLoads(LoadCaseIdentity targetCase, int plateId);
```

**Implementation consideration:** Map direction, magnitude and local coordinates to named records.

#### GetElementLoadInfo

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_ele.html#gae252989768c7a639b3f13bb01cc84809) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_ele.html#gae252989768c7a639b3f13bb01cc84809) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gae252989768c7a639b3f13bb01cc84809)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L6.

**Managed capability:** Absent. No element load-command index reader. Plate geometry and Output results do not supply applied load-command definitions.

```cpp
VARIANT OSLoadUI::GetElementLoadInfo(
    const VARIANT FAR&varloadIndex,
    VARIANT FAR&varDir,
    VARIANT FAR&varForce,
    VARIANT FAR&varDist
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varloadIndex` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |
| 2 | `varDir` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `varForce` | `VARIANT FAR&` | [out] | `System.Object&` |
| 4 | `varDist` | `VARIANT FAR&` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `loadIndex` | [in] | Load item index (Zero based). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gae252989768c7a639b3f13bb01cc84809) |
| `varDir` | [out] | Load direction: (= 0 to 8 for LocalX, LocalY, LocalZ, GlobalX, GlobalY and GlobalZ, ProjectedX, ProjectedY, ProjectedZ respectively). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gae252989768c7a639b3f13bb01cc84809) |
| `varForce` | [out] | Element pressures VARIANT array: dW1, dW2, dW3 and dW4 (see commands for add element pressure). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gae252989768c7a639b3f13bb01cc84809) |
| `varDist` | [out] | Element force distances VARIANT array: dX1, dY1, dX2 and dY2 (see commands for add element force). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gae252989768c7a639b3f13bb01cc84809) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `1` | Successful. |
| `0` | Unsuccessful. |
| `-1` | Not Implemented. |

**Proposed managed entry:**

```csharp
LoadQueryResult<PlateLoadCommandInfo> GetPlateLoadCommand(LoadCaseIdentity targetCase, int loadItemIndex);
```

**Implementation consideration:** Manual supports ElementLoadPressure only and requires selected load case; do not advertise all element commands.

### Load Items: Floor

#### AddMemberFloorLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_floor.html#gad8d2365f154d84c6ac48bf733b3a41fd) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_floor.html#gad8d2365f154d84c6ac48bf733b3a41fd) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gad8d2365f154d84c6ac48bf733b3a41fd)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L6.

**Managed capability:** Absent. No automatic floor-panel discovery/distribution assignment. A caller manually generating member loads is not an implemented floor-load operation.

```cpp
VARIANT OSLoadUI::AddMemberFloorLoad(
    const VARIANT FAR&varPressure,
    const VARIANT FAR&varYMIN,
    const VARIANT FAR&varYMAX,
    const VARIANT FAR&varZMIN,
    const VARIANT FAR&varZMAX,
    const VARIANT FAR&varXMIN,
    const VARIANT FAR&varXMAX
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varPressure` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varYMIN` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varYMAX` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varZMIN` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varZMAX` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `varXMIN` | `const VARIANT FAR&` | [in] | `System.Object` |
| 7 | `varXMAX` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varPressure` | [in] | Magnitude of the pressure or concentrate load on the element. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gad8d2365f154d84c6ac48bf733b3a41fd) |
| `varYMIN` | [in] | Y range from which the load start (in global coordinate). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gad8d2365f154d84c6ac48bf733b3a41fd) |
| `varYMAX` | [in] | Y range at which the load end (in global coordinate). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gad8d2365f154d84c6ac48bf733b3a41fd) |
| `varZMIN` | [in] | Z range from which the load start (in global coordinate). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gad8d2365f154d84c6ac48bf733b3a41fd) |
| `varZMAX` | [in] | Z range at which the load end (in global coordinate). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gad8d2365f154d84c6ac48bf733b3a41fd) |
| `varXMIN` | [in] | X range from which the load start (in global coordinate). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gad8d2365f154d84c6ac48bf733b3a41fd) |
| `varXMAX` | [in] | X range at which the load end (in global coordinate). For additional information, please refer to Section 5.32.4.2 and 5.32.4.3 of the Technical Reference manual. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gad8d2365f154d84c6ac48bf733b3a41fd) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `1` | OK. |
| `0` | General error. |
| `-8001` | Load direction is invalid. |

**Summary/detailed declaration note:** The evidence data retains both forms, including parameter-name aliases. Do not bind by the help-table spelling; compare argument position/type and acquired signature.

**Proposed managed entry:**

```csharp
LoadOperationResult AddFloorLoad(LoadCaseIdentity targetCase, FloorLoadDefinition definition);
```

**Implementation consideration:** Check documented direction/range restrictions and engineering axes before wrapping legacy form.

#### GetBeamCountAtFloor

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_floor.html#gaa95d436c89c336e5002ffc642fbed2b6) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_floor.html#gaa95d436c89c336e5002ffc642fbed2b6) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gaa95d436c89c336e5002ffc642fbed2b6)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L6.

**Managed capability:** Absent. No floor-region beam count service matching this native operation. Geometry inventories do not by themselves implement floor-region semantics.

```cpp
VARIANT OSLoadUI::GetBeamCountAtFloor(
    const VARIANT FAR&fMinX,
    const VARIANT FAR&fMaxX,
    const VARIANT FAR&fMinY,
    const VARIANT FAR&fMaxY,
    const VARIANT FAR&fMinZ,
    const VARIANT FAR&fMaxZ,
    const VARIANT FAR&nDirection
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `fMinX` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |
| 2 | `fMaxX` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |
| 3 | `fMinY` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |
| 4 | `fMaxY` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |
| 5 | `fMinZ` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |
| 6 | `fMaxZ` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |
| 7 | `nDirection` | `const VARIANT FAR&` | Not matched by name; see native reference | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varfMinX` | [in] | X range start (in global coordinate).(Type: float) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gaa95d436c89c336e5002ffc642fbed2b6) |
| `varfMaxX` | [in] | X range end (in global coordinate).(Type: float) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gaa95d436c89c336e5002ffc642fbed2b6) |
| `varfMinY` | [in] | Y range start (in global coordinate).(Type: float) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gaa95d436c89c336e5002ffc642fbed2b6) |
| `varfMaxY` | [in] | Y range end (in global coordinate).(Type: float) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gaa95d436c89c336e5002ffc642fbed2b6) |
| `varfMinZ` | [in] | Z range start (in global coordinate).(Type: float) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gaa95d436c89c336e5002ffc642fbed2b6) |
| `varfMaxZ` | [in] | Z range end (in global coordinate).(Type: float) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gaa95d436c89c336e5002ffc642fbed2b6) |
| `varnDirection` | [in] | Direction(1 for XRange, 2 for YRange, 3 for ZRange).(Type: Long/Integer) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gaa95d436c89c336e5002ffc642fbed2b6) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

No return-value table is present. Read the return prose in the linked reference; do not infer Boolean success or invent a payload.

**Proposed managed entry:**

```csharp
LoadQueryResult<int> GetFloorBeamCount(FloorRegion region);
```

**Implementation consideration:** Range direction 1/2/3 is a different enum from force direction; finite ordered coordinate bounds.

#### GetInfluenceArea

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_floor.html#gaba0ac897a60241ed8a3a8e00506e5bed) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_floor.html#gaba0ac897a60241ed8a3a8e00506e5bed) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gaba0ac897a60241ed8a3a8e00506e5bed)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L6.

**Managed capability:** Absent. No floor influence-area calculation returning beam/area associations.

```cpp
VARIANT OSLoadUI::GetInfluenceArea(
    const VARIANT FAR&varfMinX,
    const VARIANT FAR&varfMaxX,
    const VARIANT FAR&varfMinY,
    const VARIANT FAR&varfMaxY,
    const VARIANT FAR&varfMinZ,
    const VARIANT FAR&varfMaxZ,
    const VARIANT FAR&varnDirection,
    VARIANT FAR&varnBeamNos,
    VARIANT FAR&varAreas
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varfMinX` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varfMaxX` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varfMinY` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varfMaxY` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varfMinZ` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `varfMaxZ` | `const VARIANT FAR&` | [in] | `System.Object` |
| 7 | `varnDirection` | `const VARIANT FAR&` | [in] | `System.Object` |
| 8 | `varnBeamNos` | `VARIANT FAR&` | [out] | `System.Object&` |
| 9 | `varAreas` | `VARIANT FAR&` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varfMinX` | [in] | X range start (in global coordinate).(Type: float) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gaba0ac897a60241ed8a3a8e00506e5bed) |
| `varfMaxX` | [in] | X range end (in global coordinate).(Type: float) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gaba0ac897a60241ed8a3a8e00506e5bed) |
| `varfMinY` | [in] | Y range start (in global coordinate).(Type: float) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gaba0ac897a60241ed8a3a8e00506e5bed) |
| `varfMaxY` | [in] | Y range end (in global coordinate).(Type: float) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gaba0ac897a60241ed8a3a8e00506e5bed) |
| `varfMinZ` | [in] | Z range start (in global coordinate).(Type: float) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gaba0ac897a60241ed8a3a8e00506e5bed) |
| `varfMaxZ` | [in] | Z range end (in global coordinate).(Type: float) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gaba0ac897a60241ed8a3a8e00506e5bed) |
| `varnDirection` | [in] | Direction(1 for XRange, 2 for YRange, 3 for ZRange).(Type: Long/Integer) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gaba0ac897a60241ed8a3a8e00506e5bed) |
| `varnBeamNos` | [out] | Influence Beam No Array.(Type: Long Array) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gaba0ac897a60241ed8a3a8e00506e5bed) |
| `varAreas` | [out] | Influence Area Array.(Type: Double Array) [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gaba0ac897a60241ed8a3a8e00506e5bed) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `1(TRUE)` | Get Influence Area Successful. |
| `0(FALSE)` | Generate Error. |

**Summary/detailed declaration note:** The evidence data retains both forms, including parameter-name aliases. Do not bind by the help-table spelling; compare argument position/type and acquired signature.

**Proposed managed entry:**

```csharp
LoadQueryResult<IEnumerable<BeamInfluenceArea>> GetFloorInfluenceAreas(FloorRegion region);
```

**Implementation consideration:** Count/fetch consistent region; return beam-ID/area associations, not two unpaired sequences.

#### AddMemberFloorLoadEx

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_floor.html#gae4dbae2a1476dc8b9105842bd568fb67) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_floor.html#gae4dbae2a1476dc8b9105842bd568fb67) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gae4dbae2a1476dc8b9105842bd568fb67)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L6.

**Managed capability:** Absent. No range/group/one-way floor-load request or assignment. A future common floor-load API can cover overlapping legacy and extended cases.

```cpp
VARIANT OSLoadUI::AddMemberFloorLoadEx(
    const VARIANT FAR&varRange,
    const VARIANT FAR&varDirection,
    const VARIANT FAR&dPressure,
    const VARIANT FAR&varGrpOrOneWay,
    const VARIANT FAR&dYMIN,
    const VARIANT FAR&dYMAX,
    const VARIANT FAR&dZMIN,
    const VARIANT FAR&dZMAX,
    const VARIANT FAR&dXMIN,
    const VARIANT FAR&dXMAX
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varRange` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varDirection` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `dPressure` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varGrpOrOneWay` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `dYMIN` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `dYMAX` | `const VARIANT FAR&` | [in] | `System.Object` |
| 7 | `dZMIN` | `const VARIANT FAR&` | [in] | `System.Object` |
| 8 | `dZMAX` | `const VARIANT FAR&` | [in] | `System.Object` |
| 9 | `dXMIN` | `const VARIANT FAR&` | [in] | `System.Object` |
| 10 | `dXMAX` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varRange` | [in] | Type of the Range : Value Range Type 0 X-RANGE 1 Y-RANGE 2 Z-RANGE 3 Group Load [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gae4dbae2a1476dc8b9105842bd568fb67) |
| `varDirection` | [in] | Load direction : Value Direction 0 Global X 1 Global Y 2 Global Z [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gae4dbae2a1476dc8b9105842bd568fb67) |
| `dPressure` | [in] | Magnitude of the pressure or concentrate load on the element.(type - float/double). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gae4dbae2a1476dc8b9105842bd568fb67) |
| `varGrpOrOneWay` | [in] | One-Way Load (if it is either "" or "0") or corresponding group name to add Floor Group Load (if it contains Group string name).(type - String). Note, group name should be of FLOOR group type. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gae4dbae2a1476dc8b9105842bd568fb67) |
| `dYMIN` | [in] | Y range from which the load start(in global coordinate). (type - float/double). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gae4dbae2a1476dc8b9105842bd568fb67) |
| `dYMAX` | [in] | Y range at which the load end(in global coordinate). (type - float/double). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gae4dbae2a1476dc8b9105842bd568fb67) |
| `dZMIN` | [in] | Z range from which the load start(in global coordinate). (type - float/double). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gae4dbae2a1476dc8b9105842bd568fb67) |
| `dZMAX` | [in] | Z range at which the load end(in global coordinate). (type - float/double). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gae4dbae2a1476dc8b9105842bd568fb67) |
| `dXMIN` | [in] | X range from which the load start(in global coordinate). (type - float/double). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gae4dbae2a1476dc8b9105842bd568fb67) |
| `dXMAX` | [in] | X range at which the load end(in global coordinate). (type - float/double). For additional information, please refer to Section 5.32.4.2 and 5.32.4.3 of the Technical Reference manual. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gae4dbae2a1476dc8b9105842bd568fb67) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `1` | OK. |
| `0` | General error. |

**Proposed managed entry:**

```csharp
LoadOperationResult AddFloorLoad(LoadCaseIdentity targetCase, ExtendedFloorLoadDefinition definition);
```

**Implementation consideration:** Discriminate coordinate-range, group and one-way modes in the request model.

### Load Items: Temperature

#### AddTemperatureLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_temp.html#ga63fe8160cb8445ac3d7acd1697b5b428) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_temp.html#ga63fe8160cb8445ac3d7acd1697b5b428) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_temp.html#ga63fe8160cb8445ac3d7acd1697b5b428)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::AddTemperatureLoad(
    const VARIANT FAR&varElementNo,
    const VARIANT FAR&varTempAxialElong,
    const VARIANT FAR&varTempDiffTopAndBtm,
    const VARIANT FAR&varTemDiffSide
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varElementNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varTempAxialElong` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varTempDiffTopAndBtm` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varTemDiffSide` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varElementNo` | [in] | Element number ID(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_temp.html#ga63fe8160cb8445ac3d7acd1697b5b428) |
| `varTempAxialElong` | [in] | Change in temperature. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_temp.html#ga63fe8160cb8445ac3d7acd1697b5b428) |
| `varTempDiffTopAndBtm` | [in] | Temperature difference from the top to the bottom of the element (for calculating bending). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_temp.html#ga63fe8160cb8445ac3d7acd1697b5b428) |
| `varTemDiffSide` | [in] | Temperature difference from side to side of the element (local Z axis). For additional information, please refer to Section 5.32.6 of the Technical Reference manual. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_temp.html#ga63fe8160cb8445ac3d7acd1697b5b428) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Current call sites:** [AddAxialThermalGradientTo — line 502](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L502); [AddAxialThermalGradientTo — line 520](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L520).

#### AddStrainLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_temp.html#ga33d9916b222ffd303f9625232a287451) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_temp.html#ga33d9916b222ffd303f9625232a287451) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_temp.html#ga33d9916b222ffd303f9625232a287451)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L6.

**Managed capability:** Absent. No strain-load assignment. AddAxialThermalGradientTo uses AddTemperatureLoad and is not strain loading.

```cpp
VARIANT OSLoadUI::AddStrainLoad(
    const VARIANT FAR&varElementNo,
    const VARIANT FAR&varAxialElong
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varElementNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varAxialElong` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varElementNo` | [in] | Element number ID(s). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_temp.html#ga33d9916b222ffd303f9625232a287451) |
| `varAxialElong` | [in] | Initial axial elongation (+)/ shrinkage (-) in member due to misfit, etc. For additional information, please refer to Section 5.32.6 of the Technical Reference manual. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_temp.html#ga33d9916b222ffd303f9625232a287451) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Proposed managed entry:**

```csharp
LoadOperationResult AddStrainLoad(LoadCaseIdentity targetCase, IEnumerable<int> elementIds, double axialElongation);
```

**Implementation consideration:** Check element types and dimensional meaning from native technical reference before deciding units conversion.

### Load Items: Seismic

#### AddSeismicLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_seismic.html#gaef2b657a5ddda1beb6c8af149585ffc7) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_seismic.html#gaef2b657a5ddda1beb6c8af149585ffc7) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_seismic.html#gaef2b657a5ddda1beb6c8af149585ffc7)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L6.

**Managed capability:** Absent. Seismic definition/weight APIs and LoadType.SeismicH/SeismicV metadata exist; no seismic-load application to a case is implemented.

```cpp
VARIANT OSLoadUI::AddSeismicLoad(
    const VARIANT FAR&varDirection,
    const VARIANT FAR&varFactor
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varDirection` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varFactor` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varDirection` | [in] | Load direction: (= 0 to 2 for global X, Y and Z, respectively). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_seismic.html#gaef2b657a5ddda1beb6c8af149585ffc7) |
| `varFactor` | [in] | Multiplication factor to be used to multiply the seismic load. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_seismic.html#gaef2b657a5ddda1beb6c8af149585ffc7) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |
| `-8001` | Load direction is invalid. |

**Proposed managed entry:**

```csharp
LoadOperationResult AddSeismicLoad(LoadCaseIdentity targetCase, SeismicLoadDirection direction, double factor);
```

**Implementation consideration:** Use native seismic direction domain; zero success and -8001 invalid direction.

#### IsDynamicLoadIncluded

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_seismic.html#gafe4f103eedc99ab92900eb5342952e6f) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_seismic.html#gafe4f103eedc99ab92900eb5342952e6f) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_seismic.html#gafe4f103eedc99ab92900eb5342952e6f)

**Comparison:** Common. **Coverage:** S — shared LoadInspectionService dispatch.

**Managed capability:** Implemented through `ContainsDynamicLoad`. Native 1/0 means true/false successful data; -1 is NativeFailure, never Boolean true. Actual Boolean native returns are also accepted. Does not classify a case from its title or engineering type, activate it, or start analysis.

```cpp
VARIANT OSLoadUI::IsDynamicLoadIncluded(
    long nLoadCase
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nLoadCase` | `long` | [in] | `System.Int32` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nLoadCase` | [in] | Load case reference ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_seismic.html#gafe4f103eedc99ab92900eb5342952e6f) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `1` | YES. |
| `0` | NO. |
| `-1` | General error. |

**Implemented managed entry:**

~~~csharp
LoadQueryResult<bool> ContainsDynamicLoad(int loadCaseId);
~~~

**Implementation contract:** Native 1/0 means true/false successful data; -1 is NativeFailure, never Boolean true. Actual Boolean native returns are also accepted. Does not classify a case from its title or engineering type, activate it, or start analysis. See [Load inspection](../Load-Inspection-Queries.md) for shared activation, result and buffer rules.

### Load Items: Time History

No native function declarations occur in this reviewed grouping page. Subgroups, where present, were traversed separately.

### Load Items: Wind and Snow

#### AddWindLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_wind.html#gaa9ed8f688a0b105d17f3540581472685) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_wind.html#gaa9ed8f688a0b105d17f3540581472685) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_wind.html#gaa9ed8f688a0b105d17f3540581472685)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L6.

**Managed capability:** Absent. Wind definition, exposure, intensity and profile computations exist; applying a wind-load command to a case is not implemented.

```cpp
VARIANT OSLoadUI::AddWindLoad(
    const VARIANT FAR&varTypeNo,
    const VARIANT FAR&varDirection,
    const VARIANT FAR&dFraction,
    const VARIANT FAR&varOpenStructure,
    const VARIANT FAR&dYMIN,
    const VARIANT FAR&dYMAX,
    const VARIANT FAR&dZMIN,
    const VARIANT FAR&dZMAX,
    const VARIANT FAR&dXMIN,
    const VARIANT FAR&dXMAX
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varTypeNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varDirection` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `dFraction` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varOpenStructure` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `dYMIN` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `dYMAX` | `const VARIANT FAR&` | [in] | `System.Object` |
| 7 | `dZMIN` | `const VARIANT FAR&` | [in] | `System.Object` |
| 8 | `dZMAX` | `const VARIANT FAR&` | [in] | `System.Object` |
| 9 | `dXMIN` | `const VARIANT FAR&` | [in] | `System.Object` |
| 10 | `dXMAX` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varTypeNo` | [in] | Wind Definition Type number ID. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_wind.html#gaa9ed8f688a0b105d17f3540581472685) |
| `varDirection` | [in] | Wind load direction: Value Direction 1 Global X 3 Global Z 4 Global -X 6 Global -Z [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_wind.html#gaa9ed8f688a0b105d17f3540581472685) |
| `dFraction` | [in] | factor to be used to multiply the wind loads. Negative signs may be used to indicate opposite direction of resulting load (default=1.0)? [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_wind.html#gaa9ed8f688a0b105d17f3540581472685) |
| `varOpenStructure` | [in] | Open-type of structure (= TRUE ), closed-type of structure (= FALSE ). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_wind.html#gaa9ed8f688a0b105d17f3540581472685) |
| `dYMIN` | [in] | Ymin of GLOBAL Y range in which Wind load applied (assume Y axis is vertical). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_wind.html#gaa9ed8f688a0b105d17f3540581472685) |
| `dYMAX` | [in] | Ymax of GLOBAL Y range in which Wind load applied (assume Y axis is vertical). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_wind.html#gaa9ed8f688a0b105d17f3540581472685) |
| `dZMIN` | [in] | Zmin of GLOBAL Z range in which Wind load applied (assume Y axis is vertical). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_wind.html#gaa9ed8f688a0b105d17f3540581472685) |
| `dZMAX` | [in] | Zmax of GLOBAL Z range in which Wind load applied (assume Y axis is vertical). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_wind.html#gaa9ed8f688a0b105d17f3540581472685) |
| `dXMIN` | [in] | Xmin of GLOBAL X range in which Wind load applied (assume Y axis is vertical). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_wind.html#gaa9ed8f688a0b105d17f3540581472685) |
| `dXMAX` | [in] | Xmax of GLOBAL X range in which Wind load applied (assume Y axis is vertical). For additional information, please refer to Section 5.31.3 of the Technical Reference manual. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_wind.html#gaa9ed8f688a0b105d17f3540581472685) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Proposed managed entry:**

```csharp
LoadOperationResult AddWindLoad(LoadCaseIdentity targetCase, WindLoadDefinition definition);
```

**Implementation consideration:** Definition ID, direction, factor, structure flag and ranges belong in a typed request; no automatic case/definition creation.

### Load Items: Repeat

#### AddRepeatLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga605977fe7246b588f3bcacfa4f74d80a) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga605977fe7246b588f3bcacfa4f74d80a) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga605977fe7246b588f3bcacfa4f74d80a)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::AddRepeatLoad(
    const VARIANT FAR&varLoadCaseList,
    const VARIANT FAR&varFactorList
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varLoadCaseList` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varFactorList` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varLoadCaseList` | [in] | (Primary) load case reference number ID(s) array (Type: Long). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga605977fe7246b588f3bcacfa4f74d80a) |
| `varFactorList` | [in] | Multiplication factor array (Type: Double). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga605977fe7246b588f3bcacfa4f74d80a) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `1` | If Load Case is added successfully, 0 otherwise. |

**Current call sites:** [AddRepeatLoadsTo — line 487](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L487).

#### AddNotionalLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#gad19ac47adbd00da2d8e56ff8c7bea779) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#gad19ac47adbd00da2d8e56ff8c7bea779) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gad19ac47adbd00da2d8e56ff8c7bea779)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L7.

**Managed capability:** Absent. No notional-load assignment. Repeat/reference factor APIs do not add direction-bearing notional contributions.

```cpp
long OSLoadUI::AddNotionalLoad(
    const VARIANT FAR&varPrimaryLoadCaseList,
    const VARIANT FAR&varPLFactorList,
    const VARIANT FAR&varPLDirectionList,
    const VARIANT FAR&varReferenceLoadCaseList,
    const VARIANT FAR&varRLFactorList,
    const VARIANT FAR&varRLDirectionList
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varPrimaryLoadCaseList` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varPLFactorList` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varPLDirectionList` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varReferenceLoadCaseList` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varRLFactorList` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `varRLDirectionList` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varPrimaryLoadCaseList` | [in] | (Primary) load case reference number ID(s) VARIANT array of integer [array of integer]. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gad19ac47adbd00da2d8e56ff8c7bea779) |
| `varPLFactorList` | [in] | Multiplication factor VARIANT array of Primary load cases [array of double]. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gad19ac47adbd00da2d8e56ff8c7bea779) |
| `varPLDirectionList` | [in] | Direction of Primary load cases [array of integer]. X Direction = 1 or GlobalLoadDirection.X Y Direction = 2 or GlobalLoadDirection.Y Z Direction = 3 or GlobalLoadDirection.Z [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gad19ac47adbd00da2d8e56ff8c7bea779) |
| `varReferenceLoadCaseList` | [in] | (Reference) load case reference number ID(s) VARIANT array [array of integer]. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gad19ac47adbd00da2d8e56ff8c7bea779) |
| `varRLFactorList` | [in] | Multiplication factor VARIANT array of Reference load cases [array of double]. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gad19ac47adbd00da2d8e56ff8c7bea779) |
| `varRLDirectionList` | [in] | Direction of Reference load cases [array of integer]. X Direction = 1 or GlobalLoadDirection.X Y Direction = 2 or GlobalLoadDirection.Y Z Direction = 3 or GlobalLoadDirection.Z [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gad19ac47adbd00da2d8e56ff8c7bea779) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Proposed managed entry:**

```csharp
LoadOperationResult AddNotionalLoad(LoadCaseIdentity targetCase, IEnumerable<NotionalLoadContribution> contributions);
```

**Implementation consideration:** Keep primary and reference families separate; one record carries ID, factor and direction.

#### GetRepeatLoadCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga079ca807e856f915ffdf411f9c7fa468) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga079ca807e856f915ffdf411f9c7fa468) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga079ca807e856f915ffdf411f9c7fa468)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
long OSLoadUI::GetRepeatLoadCount();
```

No native parameters.

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The number of repeat load commands in the active load case.To set the active load case use: - VARIANT OSLoadUI::SetLoadActive(const VARIANT FAR & varLoadNo) . To get the active load case use:- VARIANT OSLoadUI::GetActiveLoad() |
| `0` | General error. |

**Current call sites:** [GetRepeatLoadCases — line 600](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L600).

#### GetNoLoadFactorInRepeatLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga6ec117c499264c58e0b43ed223909834) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga6ec117c499264c58e0b43ed223909834) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga6ec117c499264c58e0b43ed223909834)

**Comparison:** Common. **Coverage:** S — shared LoadInspectionService dispatch.

**Managed capability:** Implemented through `GetRepeatContributionCount`. Uses the documented one-based repeat index. Zero is a successful empty count and -1 is NotFound for an invalid repeat index. This is a command contribution count, not the number of cases containing repeat loads.

```cpp
long OSLoadUI::GetNoLoadFactorInRepeatLoad(
    long nIndex
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nIndex` | `long` | [in] | `System.Int32` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nIndex` | [in] | The index(One based) for repeat load. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga6ec117c499264c58e0b43ed223909834) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | Number of load and factor pairs associated with a given repeat load command in the active load case. To set the active load case use: - VARIANT OSLoadUI::SetLoadActive(const VARIANT FAR & varLoadNo) . To get the active load case use:- VARIANT OSLoadUI::GetActiveLoad() |
| `-1` | in case of invalid repeat load index. |

**Implemented managed entry:**

~~~csharp
LoadQueryResult<int> GetRepeatContributionCount(int primaryLoadCaseId, int repeatItemIndex);
~~~

**Implementation contract:** Uses the documented one-based repeat index. Zero is a successful empty count and -1 is NotFound for an invalid repeat index. This is a command contribution count, not the number of cases containing repeat loads. See [Load inspection](../Load-Inspection-Queries.md) for shared activation, result and buffer rules.

#### GetRepeatLoadByIndex

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga823d0f1628893bee6ab495c46bffcfe7) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga823d0f1628893bee6ab495c46bffcfe7) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga823d0f1628893bee6ab495c46bffcfe7)

**Comparison:** Common. **Coverage:** S — shared LoadInspectionService dispatch.

**Managed capability:** Implemented through `GetRepeatContributions`. Sizes Int32 ID and Double factor buffers with GetNoLoadFactorInRepeatLoad; checks native returned size and a post-fetch count. Preserves duplicate positive primary IDs and finite signed/zero factors in an immutable materialized sequence. Zero pairs returns an empty sequence without fetching; -1 is NotFound. No dictionary deduplication or summation occurs.

```cpp
long OSLoadUI::GetRepeatLoadByIndex(
    const long nIdex,
    VARIANT*iLoad,
    VARIANT*dFactor
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nIdex` | `const long` | Not matched by name; see native reference | `System.Int32` |
| 2 | `iLoad` | `VARIANT*` | [out] | `System.Object&` |
| 3 | `dFactor` | `VARIANT*` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `Index` | [in] | The index for repeat load(One based). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga823d0f1628893bee6ab495c46bffcfe7) |
| `iLoad` | [out] | Load case reference ID(s) in Long array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga823d0f1628893bee6ab495c46bffcfe7) |
| `dFactor` | [out] | Factor(s) in Double array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga823d0f1628893bee6ab495c46bffcfe7) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | Number of load and factor pairs associated with a given repeat load command in the active load case. To set the active load case use: - VARIANT OSLoadUI::SetLoadActive(const VARIANT FAR & varLoadNo) . To get the active load case use:- VARIANT OSLoadUI::GetActiveLoad() . |
| `-1` | invalid repeat load index used. |

**Implemented managed entry:**

~~~csharp
LoadQueryResult<IEnumerable<LoadFactorContribution>> GetRepeatContributions(int primaryLoadCaseId, int repeatItemIndex, int maximumResults = 100000);
~~~

**Implementation contract:** Sizes Int32 ID and Double factor buffers with GetNoLoadFactorInRepeatLoad; checks native returned size and a post-fetch count. Preserves duplicate positive primary IDs and finite signed/zero factors in an immutable materialized sequence. Zero pairs returns an empty sequence without fetching; -1 is NotFound. No dictionary deduplication or summation occurs. See [Load inspection](../Load-Inspection-Queries.md) for shared activation, result and buffer rules.

#### GetNotionalLoadCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga407c6622c4ba44ae45e96b60d943e51a) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga407c6622c4ba44ae45e96b60d943e51a) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga407c6622c4ba44ae45e96b60d943e51a)

**Comparison:** Common. **Coverage:** S — shared LoadInspectionService dispatch.

**Managed capability:** Implemented through `GetNotionalItemCount`. Zero is valid and -1 is NativeFailure. Counts command items, not their constituent case/factor/direction contributions.

```cpp
long OSLoadUI::GetNotionalLoadCount();
```

No native parameters.

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The number of Notional load. |
| `-1` | General error. |

**Implemented managed entry:**

~~~csharp
LoadQueryResult<int> GetNotionalItemCount(int primaryLoadCaseId);
~~~

**Implementation contract:** Zero is valid and -1 is NativeFailure. Counts command items, not their constituent case/factor/direction contributions. See [Load inspection](../Load-Inspection-Queries.md) for shared activation, result and buffer rules.

#### GetNoLoadFactorDirectionInNotionalLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga6a4ecff14aed8f0398fed83ab7781e9e) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga6a4ecff14aed8f0398fed83ab7781e9e) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga6a4ecff14aed8f0398fed83ab7781e9e)

**Comparison:** Common. **Coverage:** S — shared LoadInspectionService dispatch.

**Managed capability:** Implemented through `GetNotionalContributionCount`. Forwards a positive, one-based index following the installed examples that address command 1. Zero is valid and -1 is NativeFailure. Interprets the result as the paired reader buffer size; the manual also calls it a factor without documenting a numeric factor payload. Live index/count semantics require qualification.

```cpp
long OSLoadUI::GetNoLoadFactorDirectionInNotionalLoad(
    long nIndex
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nIndex` | `long` | [in] | `System.Int32` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nIndex` | [in] | The index for Notional load. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga6a4ecff14aed8f0398fed83ab7781e9e) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The factor for specified Notional load. |
| `-1` | General error. |

**Implemented managed entry:**

~~~csharp
LoadQueryResult<int> GetNotionalContributionCount(int primaryLoadCaseId, int notionalItemIndex);
~~~

**Implementation contract:** Forwards a positive, one-based index following the installed examples that address command 1. Zero is valid and -1 is NativeFailure. Interprets the result as the paired reader buffer size; the manual also calls it a factor without documenting a numeric factor payload. Live index/count semantics require qualification. See [Load inspection](../Load-Inspection-Queries.md) for shared activation, result and buffer rules.

#### GetNotionalLoadByIndex

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#gad309996311f49a218df37558d7ad7cb8) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#gad309996311f49a218df37558d7ad7cb8) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gad309996311f49a218df37558d7ad7cb8)

**Comparison:** Common. **Coverage:** S — shared LoadInspectionService dispatch.

**Managed capability:** Implemented through `GetNotionalContributions`. Sizes paired Int32 ID/direction and Double factor buffers using GetNoLoadFactorDirectionInNotionalLoad. Positive native IDs mean primary cases and negative IDs reference cases; returns their family, positive ID and original signed ID. Global direction codes 1/2/3 are X/Y/Z, not LoadDirection local axes. Preserves order, duplicates and signed/zero factors. Zero contributions skips fetch; -1 is NativeFailure. One-based indexing follows installed examples and remains a live qualification requirement.

```cpp
long OSLoadUI::GetNotionalLoadByIndex(
    long nIdex,
    VARIANT*iLoad,
    VARIANT*dFactor,
    VARIANT*iDirection
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nIdex` | `long` | Not matched by name; see native reference | `System.Int32` |
| 2 | `iLoad` | `VARIANT*` | [out] | `System.Object&` |
| 3 | `dFactor` | `VARIANT*` | [out] | `System.Object&` |
| 4 | `iDirection` | `VARIANT*` | Not matched by name; see native reference | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nIndex` | [in] | The index for Notional load. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gad309996311f49a218df37558d7ad7cb8) |
| `iLoad` | [out] | Load case reference ID(s) in VARIANT array. +ve values = Primary Load Cases -ve values = Reference Load Cases [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gad309996311f49a218df37558d7ad7cb8) |
| `dFactor` | [out] | Factor(s) in VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gad309996311f49a218df37558d7ad7cb8) |
| `dDirection` | [out] | Direction(s) in VARIANT array. 1 = X Direction 2 = Y Direction 3 = Z Direction [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gad309996311f49a218df37558d7ad7cb8) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The size of iLoad or dFactor or iDirection . |
| `-1` | General error. |

**Implemented managed entry:**

~~~csharp
LoadQueryResult<IEnumerable<NotionalLoadContribution>> GetNotionalContributions(int primaryLoadCaseId, int notionalItemIndex, int maximumResults = 100000);
~~~

**Implementation contract:** Sizes paired Int32 ID/direction and Double factor buffers using GetNoLoadFactorDirectionInNotionalLoad. Positive native IDs mean primary cases and negative IDs reference cases; returns their family, positive ID and original signed ID. Global direction codes 1/2/3 are X/Y/Z, not LoadDirection local axes. Preserves order, duplicates and signed/zero factors. Zero contributions skips fetch; -1 is NativeFailure. One-based indexing follows installed examples and remains a live qualification requirement. See [Load inspection](../Load-Inspection-Queries.md) for shared activation, result and buffer rules.

#### AddReferenceLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga6b018e4f7084d51fdbcafafc7773257b) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga6b018e4f7084d51fdbcafafc7773257b) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga6b018e4f7084d51fdbcafafc7773257b)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
long OSLoadUI::AddReferenceLoad(
    const VARIANT FAR&varRefLoadCaseList,
    const VARIANT FAR&varFactorList
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varRefLoadCaseList` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varFactorList` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varRefLoadCaseList` | [in] | VARIANt array of reference load case number ID(s) from Reference Load Definitions. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga6b018e4f7084d51fdbcafafc7773257b) |
| `varFactorList` | [in] | Factor(s) VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga6b018e4f7084d51fdbcafafc7773257b) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `0` | OK. |
| `-1` | General error. |

**Summary/detailed declaration note:** The evidence data retains both forms, including parameter-name aliases. Do not bind by the help-table spelling; compare argument position/type and acquired signature.

**Current call sites:** [AddReferenceLoadsTo — line 477](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L477).

#### GetReferenceLoadCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga1ee158fd662d041c3676a3af85d76f17) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga1ee158fd662d041c3676a3af85d76f17) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga1ee158fd662d041c3676a3af85d76f17)

**Comparison:** Common. **Coverage:** S — shared LoadInspectionService dispatch.

**Managed capability:** Implemented through `GetReferenceItemCount`. Zero is valid and -1 is NativeFailure. Does not count reference-case definitions; use existing GetAllReferenceLoadCases for that separate inventory.

```cpp
long OSLoadUI::GetReferenceLoadCount();
```

No native parameters.

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The number of reference load case item(s). |
| `-1` | General error. |

**Implemented managed entry:**

~~~csharp
LoadQueryResult<int> GetReferenceItemCount(int primaryLoadCaseId);
~~~

**Implementation contract:** Zero is valid and -1 is NativeFailure. Does not count reference-case definitions; use existing GetAllReferenceLoadCases for that separate inventory. See [Load inspection](../Load-Inspection-Queries.md) for shared activation, result and buffer rules.

#### GetNoOfSetsInReferenceLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga121e0d6b8f66e0ed27aed4ebdf73d67c) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga121e0d6b8f66e0ed27aed4ebdf73d67c) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga121e0d6b8f66e0ed27aed4ebdf73d67c)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L7.

**Managed capability:** Absent. No contribution count for a reference-load command index.

```cpp
long OSLoadUI::GetNoOfSetsInReferenceLoad(
    const long nIndex
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nIndex` | `const long` | [in] | `System.Int32` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nIndex` | [in] | The index for reference load item. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga121e0d6b8f66e0ed27aed4ebdf73d67c) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | Reference load case reference ID. |
| `-1` | General error. |

**Proposed managed entry:**

```csharp
LoadQueryResult<int> GetReferenceContributionCount(LoadCaseIdentity targetCase, int referenceItemIndex);
```

**Implementation consideration:** Distinguish command index and referenced case number.

#### GetReferenceLoadByIndex

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#gaa32d2d60d54563abc19c9be826990e3a) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#gaa32d2d60d54563abc19c9be826990e3a) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gaa32d2d60d54563abc19c9be826990e3a)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L7.

**Managed capability:** Absent. No indexed reference case-ID/factor reader. AddReferenceLoadsTo is a write operation.

```cpp
long OSLoadUI::GetReferenceLoadByIndex(
    const long nIdex,
    VARIANT*iLoad,
    VARIANT*dFactor
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `nIdex` | `const long` | Not matched by name; see native reference | `System.Int32` |
| 2 | `iLoad` | `VARIANT*` | [out] | `System.Object&` |
| 3 | `dFactor` | `VARIANT*` | [out] | `System.Object&` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `nIndex` | [in] | The reference load and factor set number. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gaa32d2d60d54563abc19c9be826990e3a) |
| `iLoad` | [out] | Load case(s) reference Id(s) in VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gaa32d2d60d54563abc19c9be826990e3a) |
| `dFactor` | [out] | Factor(s) in VARIANT array. [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gaa32d2d60d54563abc19c9be826990e3a) |

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `reference` | load case reference number ID(s). |
| `-1` | General error. |

**Proposed managed entry:**

```csharp
LoadQueryResult<IEnumerable<LoadFactorContribution>> GetReferenceContributions(LoadCaseIdentity targetCase, int referenceItemIndex);
```

**Implementation consideration:** Return original order/factors and explicit reference family; validate native count result.

#### GetReferenceLoadType

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#gad0770a978141a1c4f3a8874fe446da54) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#gad0770a978141a1c4f3a8874fe446da54) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gad0770a978141a1c4f3a8874fe446da54)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetReferenceLoadType(
    const VARIANT FAR&varLoadNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varLoadNo` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varLoadNo` | [in] | The Reference Load No(Type: Integer/Long). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gad0770a978141a1c4f3a8874fe446da54) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `-1` | General error. |
| `Others` | (value can be 0 to 23) Reference load Type, please ref OSLoadUI::CreateNewPrimaryLoad . |

**Current call sites:** [ReadLoadCaseMetadata — line 2376](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2376).

#### GetReferenceLoadCaseTitle

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga801c4cec74cc10e3f8ac91559417414c) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga801c4cec74cc10e3f8ac91559417414c) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga801c4cec74cc10e3f8ac91559417414c)

**Comparison:** Common. **Coverage:** D — adapter dispatch.

```cpp
VARIANT OSLoadUI::GetReferenceLoadCaseTitle(
    const VARIANT FAR&varLoadNo
);
```

| Slot | Native detailed parameter | Native type | Documented direction | Inspected CLR type (2025/2026) |
| ---: | --- | --- | --- | --- |
| 1 | `varLoadNo` | `const VARIANT FAR&` | [in] | `System.Object` |

| Parameter-table name | Direction | Meaning / full-table reference |
| --- | --- | --- |
| `varLoadNo` | [in] | The Reference Load No(Type: Integer/Long). [details](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga801c4cec74cc10e3f8ac91559417414c) |

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

| Native return label | Native meaning |
| --- | --- |
| `Reference` | load Title. |

**Summary/detailed declaration note:** The evidence data retains both forms, including parameter-name aliases. Do not bind by the help-table spelling; compare argument position/type and acquired signature.

**Current call sites:** [ReadLoadCaseMetadata — line 2373](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2373).

#### BeginLoadMerging

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga4a4a3ab7b12b6456ddce1d83568b50bf) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga4a4a3ab7b12b6456ddce1d83568b50bf) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga4a4a3ab7b12b6456ddce1d83568b50bf)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L7.

**Managed capability:** Absent. No managed merge lifecycle or bounded merge operation. Duplicate-assignment grouping helpers do not merge native beam loads.

```cpp
void OSLoadUI::BeginLoadMerging();
```

No native parameters.

**Declared return:** `void`. **Inspected CLR return:** `System.Void`.

No return-value table is present. Read the return prose in the linked reference; do not infer Boolean success or invent a payload.

**Proposed managed entry:**

```csharp
LoadOperationResult MergeBeamLoads(IEnumerable<BeamLoadMergeRequest> requests);
```

**Implementation consideration:** Expose a bounded managed operation rather than a user-maintained begin/end pair; void native call does not prove success.

#### EndLoadMerging

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga07b273284cfdb265a5fae76d7631155f) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga07b273284cfdb265a5fae76d7631155f) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga07b273284cfdb265a5fae76d7631155f)

**Comparison:** Common. **Coverage:** N — no current production dispatch; planned phase L7.

**Managed capability:** Absent. No paired native merge cleanup. A future managed merge operation should own both begin/end calls rather than expose two unrelated user obligations.

```cpp
void OSLoadUI::EndLoadMerging();
```

No native parameters.

**Declared return:** `void`. **Inspected CLR return:** `System.Void`.

No return-value table is present. Read the return prose in the linked reference; do not infer Boolean success or invent a payload.

**Proposed managed entry:**

```csharp
LoadOperationResult MergeBeamLoads(IEnumerable<BeamLoadMergeRequest> requests);
```

**Implementation consideration:** Pair with BeginLoadMerging in try/finally on the same COM thread; preserve both body and cleanup failures.

## 2. Supplemental installed interop inventory

The following names appear in both passively inspected IOSLoadUI interfaces but not in the 129-function Load help inventory. None currently has a production Load dispatch. Presence in 2025/2026 metadata does not establish introduction in 2025 or absence in 2024.

Before exposing them, locate an authoritative contract, verify acquired signatures and establish native statuses, units, model/case state and failure behavior. Do not infer engineering semantics solely from the name or CLR Object parameters.

Inspected files: [2025 vendor interop](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/STAAD/StaadPro.Interop.dll); [2026 vendor interop](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/STAAD/StaadPro.Interop.dll). The assembly identities, hashes and all declarations are in the [comparison data](data/OpenSTAAD-Load-2024-2026-Comparison.json).

### AddSeismicDefElementWeight

```text
System.Object AddSeismicDefElementWeight(System.Object, System.Object)
```

| Slot | Metadata name | CLR type |
| ---: | --- | --- |
| 1 | `varPressure` | `System.Object` |
| 2 | `varElementArray` | `System.Object` |

**Disposition:** Pending independent contract review. Do not alias this function to joint/member/wall weights; establish dimensional meaning and array layout separately.

### AddSeismicDefFloorWeight

```text
System.Object AddSeismicDefFloorWeight(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)
```

| Slot | Metadata name | CLR type |
| ---: | --- | --- |
| 1 | `varRange` | `System.Object` |
| 2 | `varDirection` | `System.Object` |
| 3 | `dPressure` | `System.Object` |
| 4 | `varGrpOrOneWay` | `System.Object` |
| 5 | `dYMIN` | `System.Object` |
| 6 | `dYMAX` | `System.Object` |
| 7 | `dZMIN` | `System.Object` |
| 8 | `dZMAX` | `System.Object` |
| 9 | `dXMIN` | `System.Object` |
| 10 | `dXMAX` | `System.Object` |

**Disposition:** Pending independent contract review. Do not alias this function to joint/member/wall weights; establish dimensional meaning and array layout separately.

### MergeLoadsOnBeam

```text
System.Object MergeLoadsOnBeam(System.Object, System.Object)
```

| Slot | Metadata name | CLR type |
| ---: | --- | --- |
| 1 | `varBeamToKeep` | `System.Object` |
| 2 | `varBeamToMerge` | `System.Object` |

**Disposition:** Pending independent contract review. Coordinate with BeginLoadMerging/EndLoadMerging; verify cleanup and error behavior before a managed merge operation is exposed.

## 3. Existing managed interface and adapter surface

These **120 interface method overloads** have matching public adapter signatures. The table lists direct native calls present in that public method body only; a blank direct-call cell does not mean the managed method is unimplemented. Many overloads delegate to private helpers or other overloads. AddMemberLoad reaches six native mutations through member-load entity ApplyTo implementations; the complete native inventory above includes those dispatches.

The separate public ComObject property is an escape hatch, not a method overload. Interface/base properties are outside the 120-method count.

| # | Current managed signature | Interface / adapter | Direct native calls in method body |
| ---: | --- | --- | --- |
| 1 | `bool AddSeismicDefMemberWeight(int seismicCode, SeismicMemberLoadType loadType, double weight, double startDistance, double endDistance, IEnumerable<Beam> members)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L17) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L36) | Delegates / composes managed operations |
| 2 | `bool AddSeismicDefMemberUniformWeight(double weight, IEnumerable<int> memberIds, double startDistance = 0, double endDistance = 0, int seismicCode = 0)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L20) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L46) | Delegates / composes managed operations |
| 3 | `bool AddSeismicDefMemberConcentratedWeight(double weight, double distance, IEnumerable<int> memberIds, int seismicCode = 0)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L23) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L51) | Delegates / composes managed operations |
| 4 | `int AddSeismicDefWallArea(int seismicCode, string direction, IEnumerable<double> dimensions)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L26) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L55) | [AddSeismicDefWallArea](#addseismicdefwallarea) |
| 5 | `int AddSeismicDefWallArea(int seismicCode, SeismicWallDirection direction, IEnumerable<SeismicWallDimension> walls)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L29) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L74) | Delegates / composes managed operations |
| 6 | `bool AddResponseSpectrumLoadEx(ResponseSpectrumLoadExParameters parameters)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L32) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L90) | Delegates / composes managed operations |
| 7 | `bool AddResponseSpectrumLoadEx(int spectrumCode, int combinationMethod, string[] primaryParameterNames, double[] primaryParameterValues, string[] generationParameterNames = null, double[] generationParameterValues = null, double[] customDataPairs = null)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L35) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L97) | Delegates / composes managed operations |
| 8 | `bool AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType parameterType, IEnumerable<int> memberIds, double parameterValue = 0.0)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L38) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L112) | [AddDirectAnalysisDefinitionParameter](#adddirectanalysisdefinitionparameter) |
| 9 | `bool AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType parameterType, IEnumerable<Beam> members, double parameterValue = 0.0)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L41) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L125) | Delegates / composes managed operations |
| 10 | `bool AddDirectAnalysisFlexParameter(double flexFactor, IEnumerable<int> memberIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L44) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L134) | Delegates / composes managed operations |
| 11 | `bool AddDirectAnalysisAxialParameter(IEnumerable<int> memberIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L47) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L138) | Delegates / composes managed operations |
| 12 | `bool AddDirectAnalysisDefinitionParameter(DirectAnalysisParameters parameters)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L50) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L142) | Delegates / composes managed operations |
| 13 | `bool DeleteDirectAnalysisDefinitionParameter(DirectAnalysisParameterType parameterType)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L53) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L151) | [DeleteDirectAnalysisDefinitionParameter](#deletedirectanalysisdefinitionparameter) |
| 14 | `bool DeleteDirectAnalysisDefinition()` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L56) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L159) | [DeleteDirectAnalysisDefinition](#deletedirectanalysisdefinition) |
| 15 | `bool AddRSLoad(ResponseSpectrumLoadParameters parameters)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L59) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L163) | [AddRSLoad](#addrsload) |
| 16 | `int AddWindDefinitionASCE7Parameters(ASCE7WindDefinitionParameters parameters)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L62) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L178) | [AddWindDefinitionASCE7Parameters](#addwinddefinitionasce7parameters) |
| 17 | `int ComputeWallWindPressureProfile(WallWindPressureProfileParameters parameters)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L65) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L193) | [ComputeWallWindPressureProfile](#computewallwindpressureprofile) |
| 18 | `int ComputeWallWindPressureProfileASCE72016(ASCE72016WallWindPressureProfileParameters parameters)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L68) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L206) | [ComputeWallWindPressureProfileASCE72016](#computewallwindpressureprofileasce72016) |
| 19 | `bool AddSeismicDefinition(SeismicCode code, bool considerAccidentalTorsion = false)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L71) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L219) | Delegates / composes managed operations |
| 20 | `bool AddSeismicDefinition(int code, int accidentalTorsion = 0)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L74) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L223) | [AddSeismicDefinition](#addseismicdefinition) |
| 21 | `int ModifySeismicDefinitionParam(string parameterName, double value)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L77) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L237) | [ModifySeismicDefinitionParams](#modifyseismicdefinitionparams) |
| 22 | `int ModifySeismicDefinitionParams(IDictionary<string, double> parameters)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L80) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L245) | Delegates / composes managed operations |
| 23 | `int ModifySeismicDefinitionParams(IS1893_2016SeismicParameters parameters)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L83) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L263) | Delegates / composes managed operations |
| 24 | `int ModifySeismicDefinitionParams(IS1893_2002SeismicParameters parameters)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L86) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L270) | Delegates / composes managed operations |
| 25 | `int ModifySeismicDefinitionParams(IBCSeismicParameters parameters)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L89) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L277) | Delegates / composes managed operations |
| 26 | `bool AddSeismicDefSelfWeight(double weightFactor = 1.0)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L92) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L284) | [AddSeismicDefSelfWeight](#addseismicdefselfweight) |
| 27 | `int AddSeismicDefJointWeight(double weight, IEnumerable<int> nodeIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L95) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L291) | [AddSeismicDefJointWeight](#addseismicdefjointweight) |
| 28 | `int AddSeismicDefJointWeight(double weight, IEnumerable<Node> nodes)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L98) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L300) | Delegates / composes managed operations |
| 29 | `int AddSeismicDefJointWeight(double weight, int nodeId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L101) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L309) | Delegates / composes managed operations |
| 30 | `bool AddSeismicDefMemberWeight(int seismicCode, SeismicMemberLoadType loadType, double weight, double startDistance, double endDistance, IEnumerable<int> memberIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L104) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L312) | [AddSeismicDefMemberWeight](#addseismicdefmemberweight) |
| 31 | `void PopulateMemberUniformlyDistributedLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L107) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L334) | Delegates / composes managed operations |
| 32 | `void PopulateMemberUniformMoments(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L110) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L341) | Delegates / composes managed operations |
| 33 | `IEnumerable<Node> PopulateNodalLoads(IEnumerable<Node> forNodes, IEnumerable<ILoadCase> fromLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L113) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L348) | Delegates / composes managed operations |
| 34 | `int AddWindDefinition(int windDefinitionId, string definitionName)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L116) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L364) | [AddWindDefinition](#addwinddefinition) |
| 35 | `int AddWindExposure(int windDefinitionId, double exposureFactor, IEnumerable<int> nodeIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L119) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L374) | [AddWindExposure](#addwindexposure) |
| 36 | `int AddWindExposure(int windDefinitionId, double exposureFactor, IEnumerable<Node> nodes)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L122) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L386) | Delegates / composes managed operations |
| 37 | `int AddWindExposure(int windDefinitionId, double exposureFactor, int nodeId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L125) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L399) | Delegates / composes managed operations |
| 38 | `int AddWindIntensity(int windDefinitionId, double intensity, double height)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L128) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L408) | [AddWindIntensity](#addwindintensity) |
| 39 | `int AddWindIntensity(int windDefinitionId, IEnumerable<double> intensities, IEnumerable<double> heights)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L131) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L417) | [AddWindIntensity](#addwindintensity) |
| 40 | `int AddWindIntensity(int windDefinitionId, IEnumerable<WindIntensityPair> pairs)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L134) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L432) | Delegates / composes managed operations |
| 41 | `int DeleteWindDefinition(int windDefinitionId = 0)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L137) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L441) | [DeleteWindDefinition](#deletewinddefinition) |
| 42 | `int DeleteAllWindDefinitions()` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L140) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L448) | Delegates / composes managed operations |
| 43 | `IOSLoad AddReferenceLoadsTo(int loadCaseId, Dictionary<int, double> loadCaseNumberFactorPairs)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L143) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L472) | [AddReferenceLoad](#addreferenceload) |
| 44 | `IOSLoad AddRepeatLoadsTo(int loadCaseId, IDictionary<int, double> loadCaseNumberFactorPairs)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L146) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L482) | [AddRepeatLoad](#addrepeatload) |
| 45 | `IOSLoad AddAxialThermalGradientTo(int loadCaseId, IEnumerable<int> entities, double axialThermalGradient)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L149) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L492) | [AddTemperatureLoad](#addtemperatureload) |
| 46 | `IOSLoad AddAxialThermalGradientTo(int loadCaseId, IEnumerable<int> entities, double axialThermalGradient, int nThreads)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L152) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L507) | [AddTemperatureLoad](#addtemperatureload) |
| 47 | `IOSLoad AddMemberLoadsFromGroupsByLoadCase(Dictionary<ILoadCase, List<(MemberLoad Load, List<Beam> Beams)>> grouped)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L155) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L541) | Delegates / composes managed operations |
| 48 | `IOSLoad AddNodalLoadsFromGroupsByLoadCase(Dictionary<ILoadCase, List<(NodalLoad Load, List<Node> Nodes)>> grouped)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L158) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L557) | Delegates / composes managed operations |
| 49 | `void PopulateMemberLinearVaryingLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L163) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L573) | Delegates / composes managed operations |
| 50 | `IEnumerable<Beam> PopulateMemberLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L165) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L579) | Delegates / composes managed operations |
| 51 | `void PopulateMemberTrapezoidalLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L167) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L588) | Delegates / composes managed operations |
| 52 | `HashSet<ILoadCase> GetRepeatLoadCases()` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L170) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L594) | [GetRepeatLoadCount](#getrepeatloadcount) |
| 53 | `void PopulateMemberConcentratedLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L172) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L607) | Delegates / composes managed operations |
| 54 | `void PopulateMemberConcentratedMoments(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L174) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L613) | Delegates / composes managed operations |
| 55 | `StaadLoadCombination GetLoadCombination(int id)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L177) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L619) | [GetNoOfLoadAndFactorPairsForCombination](#getnoofloadandfactorpairsforcombination), [GetLoadCaseTitle](#getloadcasetitle), [GetLoadAndFactorForCombination](#getloadandfactorforcombination) |
| 56 | `Dictionary<ILoadCase, List<(MemberLoad Load, List<Beam> Beams)>> GetMemberLoadsGroupedByLoadCase(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L179) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L651) | Delegates / composes managed operations |
| 57 | `Dictionary<ILoadCase, List<(NodalLoad Load, List<Node> Nodes)>> GetNodalLoadsGroupedByLoadCase(IEnumerable<Node> forNodes, IEnumerable<ILoadCase> fromLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L181) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L663) | Delegates / composes managed operations |
| 58 | `IOSLoad AddSelfweightInXYZTo(int loadCaseId, IEnumerable<int> entities, int nThreads, SelfWeightDirection loadDirection = SelfWeightDirection.GlobalY, double factor = -1)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L184) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L708) | Delegates / composes managed operations |
| 59 | `IOSLoad CreateLoadEnvelopExt(int envelopId, LoadEnvelopType envelopType, IEnumerable<EditorLoadCombination> loadCombinations)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L186) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L716) | Delegates / composes managed operations |
| 60 | `IOSLoad CreateLoadCombinations(IEnumerable<EditorLoadCombination> loadCombinations)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L188) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L724) | [CreateNewLoadCombination](#createnewloadcombination), [AddLoadAndFactorToCombination](#addloadandfactortocombination) |
| 61 | `List<NodalLoad> GetNodalLoads(ILoadCase lc, int nId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L229) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L784) | [GetNodalLoadCount](#getnodalloadcount), [GetNodalLoads](#getnodalloads) |
| 62 | `List<MemberUniformlyDistributedLoad> GetMemberUniformlyDistributedLoads(ILoadCase lc, int mId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L270) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L842) | [GetUDLLoadCount](#getudlloadcount), [GetUDLLoads](#getudlloads) |
| 63 | `List<MemberConcentratedLoad> GetMemberConcentratedLoads(ILoadCase lc, int mId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L311) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L901) | [GetConcForceCount](#getconcforcecount), [GetConcForces](#getconcforces) |
| 64 | `List<MemberConcentratedMoment> GetMemberConcentratedMoments(ILoadCase lc, int mId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L352) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L964) | [GetConcMomentCount](#getconcmomentcount), [GetConcMoments](#getconcmoments) |
| 65 | `List<MemberLinearVaryingLoad> GetMemberLinearVaryingLoads(ILoadCase lc, int mId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L393) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1023) | [GetLinearVaryingLoadCount](#getlinearvaryingloadcount), [GetLinearVaryingLoads](#getlinearvaryingloads) |
| 66 | `List<MemberTrapezoidalLoad> GetMemberTrapezoidalLoads(ILoadCase lc, int mId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L434) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1083) | [GetTrapLoadCount](#gettraploadcount), [GetTrapLoads](#gettraploads) |
| 67 | `bool ClearPrimaryLoadCase(ILoadCase loadCase, bool isReferenceLoad = false)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L441) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1713) | Delegates / composes managed operations |
| 68 | `bool ClearPrimaryLoadCases(IEnumerable<ILoadCase> loadCases, bool isReferenceLoad = false)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L444) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1717) | Delegates / composes managed operations |
| 69 | `bool ClearPrimaryLoadCases(IEnumerable<int> lcIds, bool isReferenceLoad = false)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L447) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1725) | [ClearPrimaryLoadCase](#clearprimaryloadcase) |
| 70 | `bool ClearPrimaryLoadCase(ILoadCase loadCase)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L450) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1736) | Delegates / composes managed operations |
| 71 | `bool ClearPrimaryLoadCases(IEnumerable<ILoadCase> loadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L453) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1740) | Delegates / composes managed operations |
| 72 | `bool ClearPrimaryLoadCases(IEnumerable<int> lcIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L456) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1748) | [ClearPrimaryLoadCase](#clearprimaryloadcase) |
| 73 | `bool ClearReferenceLoadCase(ILoadCase loadCase)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L459) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1759) | Delegates / composes managed operations |
| 74 | `bool ClearReferenceLoadCases(IEnumerable<ILoadCase> loadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L462) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1763) | Delegates / composes managed operations |
| 75 | `bool ClearReferenceLoadCases(IEnumerable<int> lcIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L465) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1771) | [ClearReferenceLoadCase](#clearreferenceloadcase) |
| 76 | `int CreateNewPrimaryLoad(string lcTitle, LoadType loadType)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L472) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1786) | [CreateNewPrimaryLoadEx](#createnewprimaryloadex) |
| 77 | `int CreateNewPrimaryLoad(int lcId, string lcTitle, LoadType loadType)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L475) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1795) | [CreateNewPrimaryLoadEx2](#createnewprimaryloadex2) |
| 78 | `int CreateNewPrimaryLoadEx(LoadCase lc)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L478) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1804) | Delegates / composes managed operations |
| 79 | `int CreateNewPrimaryLoadEx2(LoadCase lc)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L481) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1811) | Delegates / composes managed operations |
| 80 | `int CreateNewReferenceLoad(int lcId, string lcTitle, LoadType loadType)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L484) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1818) | [CreateNewReferenceLoad](#createnewreferenceload) |
| 81 | `int CreateNewReferenceLoad(LoadCase lc)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L487) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1827) | Delegates / composes managed operations |
| 82 | `string GetLoadCaseTitle(int id)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L490) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1834) | [GetLoadCaseTitle](#getloadcasetitle) |
| 83 | `IOSLoad SetLoadCaseActive(ILoadCase lc)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L493) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1842) | [SetReferenceLoadActive](#setreferenceloadactive), [SetLoadActive](#setloadactive) |
| 84 | `IOSLoad CreateNewLoadCase(ILoadCase lc)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L496) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1860) | Delegates / composes managed operations |
| 85 | `IOSLoad AddSupportSettlement(ILoadCase lc, int nodeId, SettlementDirection direction, double mmSettlement)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L503) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1893) | Delegates / composes managed operations |
| 86 | `IOSLoad AddSupportSettlement(ILoadCase lc, Node node, SettlementDirection direction, double mmSettlement)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L506) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1899) | Delegates / composes managed operations |
| 87 | `IOSLoad AddSupportSettlement(ILoadCase lc, IEnumerable<Node> nodes, SettlementDirection direction, double mmSettlement)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L509) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1906) | Delegates / composes managed operations |
| 88 | `IOSLoad AddSupportSettlement(ILoadCase lc, IEnumerable<int> nodeIds, SettlementDirection direction, double mmSettlement)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L512) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1913) | [AddSupportDisplacement](#addsupportdisplacement) |
| 89 | `IOSLoad CreatePrimaryLoadCases(HashSet<ILoadCase> primaryLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L519) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1933) | Delegates / composes managed operations |
| 90 | `IOSLoad CreatePrimaryLoadCases(IEnumerable<ILoadCase> primaryLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L522) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1937) | Delegates / composes managed operations |
| 91 | `IOSLoad CreateReferenceLoadCases(HashSet<ILoadCase> referenceLoads)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L525) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1958) | Delegates / composes managed operations |
| 92 | `IOSLoad CreateReferenceLoadCases(IEnumerable<ILoadCase> referenceLoads)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L528) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1962) | Delegates / composes managed operations |
| 93 | `IOSLoad AddNodalLoad(ILoadCase lc, Node node, NodalLoad nl)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L535) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1987) | Delegates / composes managed operations |
| 94 | `IOSLoad AddNodalLoad(ILoadCase lc, IEnumerable<Node> nodes, NodalLoad nl)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L538) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1994) | [AddNodalLoad](#addnodalload) |
| 95 | `IOSLoad AddMemberLoad(ILoadCase lc, Beam beam, MemberLoad ml)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L545) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2016) | Delegates / composes managed operations |
| 96 | `IOSLoad AddMemberLoad(ILoadCase lc, IEnumerable<Beam> beams, MemberLoad ml)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L548) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2023) | Delegates / composes managed operations |
| 97 | `IOSLoad AddPlateUniformPressure(ILoadCase lc, int plateId, double pressure, LoadDirection direction = LoadDirection.LocalZ)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L555) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2045) | Delegates / composes managed operations |
| 98 | `IOSLoad AddPlateUniformPressure(ILoadCase lc, IEnumerable<int> plateIds, double pressure, LoadDirection direction = LoadDirection.LocalZ)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L558) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2051) | [AddElementPressure](#addelementpressure) |
| 99 | `IOSLoad AddPlateUniformPressure(ILoadCase lc, IEnumerable<Plate> plates, double pressure, LoadDirection direction = LoadDirection.LocalZ)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L561) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2068) | Delegates / composes managed operations |
| 100 | `List<MemberUniformMoment> GetMemberUniformMoments(ILoadCase lc, int mId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L603) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1144) | [GetUNIMomentCount](#getunimomentcount), [GetUNIMoments](#getunimoments) |
| 101 | `HashSet<ILoadCase> GetAllPrimaryLoadCases()` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L635) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1194) | Delegates / composes managed operations |
| 102 | `HashSet<ILoadCase> GetAllReferenceLoadCases()` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L667) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1226) | Delegates / composes managed operations |
| 103 | `ILoadCase GetPrimaryLoadCaseFromId(int lcId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L699) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1258) | Delegates / composes managed operations |
| 104 | `ILoadCase GetReferenceLoadCaseFromId(int lcId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L731) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1293) | Delegates / composes managed operations |
| 105 | `List<ILoadCase> GetPrimaryLoadCasesFromIds(IEnumerable<int> loadCasesIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L764) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1329) | Delegates / composes managed operations |
| 106 | `List<ILoadCase> GetReferenceLoadCasesFromIds(IEnumerable<int> loadCasesIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L797) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1371) | Delegates / composes managed operations |
| 107 | `HashSet<ILoadCase> GetAllLoadCombinationCases()` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L828) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1411) | Delegates / composes managed operations |
| 108 | `ILoadCase GetLoadCombinationCaseFromId(int lcId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L862) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1449) | Delegates / composes managed operations |
| 109 | `IEnumerable<ILoadCase> GetLoadCombinationCasesFromIds(IEnumerable<int> lcIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L899) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1492) | Delegates / composes managed operations |
| 110 | `IEnumerable<ILoadCase> GetAllLoadCasesOfType(LoadCaseType loadCaseType = LoadCaseType.PrimaryLoad)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L934) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1541) | Delegates / composes managed operations |
| 111 | `ILoadCase GetLoadCase(int lcId, LoadCaseType loadCaseType)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L970) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1585) | Delegates / composes managed operations |
| 112 | `IEnumerable<ILoadCase> GetLoadCases(int fromId, int toId, LoadCaseType loadCaseType)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1006) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1625) | Delegates / composes managed operations |
| 113 | `IEnumerable<ILoadCase> GetLoadCasesFromIds(IEnumerable<int> loadCasesIds, LoadCaseType loadCaseType)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1044) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1668) | Delegates / composes managed operations |
| 114 | `List<ILoadCase> GetLoadCasesOfType(LoadCaseType loadCaseType)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1074) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1707) | Delegates / composes managed operations |
| 115 | `HashSet<int> GetEnvelopeIds()` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1098) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2098) | [GetEnvelopeCount](#getenvelopecount), [GetEnvelopeIDs](#getenvelopeids) |
| 116 | `ILoadEnvelopeDetail GetLoadEnvelopeDetail(int envId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1130) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2139) | [GetLoadEnvelopeDetails](#getloadenvelopedetails), [GetLoadListfromLoadEnvelope](#getloadlistfromloadenvelope) |
| 117 | `HashSet<ILoadEnvelopeDetail> GetAllLoadEnvelopeDetails()` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1155) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2181) | Delegates / composes managed operations |
| 118 | `IOSLoad AddSelfweightInXYZTo(int loadCaseId, LoadCaseType lcType, IEnumerable<int> entities, SelfWeightDirection loadDirection = SelfWeightDirection.GlobalY, double factor = -1)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1187) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2220) | [AddSelfWeightInXYZToGeometry](#addselfweightinxyztogeometry) |
| 119 | `IOSLoad CreateLoadEnvelopExt(int envelopId, LoadEnvelopType envelopType, IEnumerable<int> loadCaseList)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1215) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2260) | [CreateLoadEnvelop](#createloadenvelop) |
| 120 | `dynamic CreateLoadList(LoadListType loadListType, IEnumerable<int> loadCaseIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1244) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2296) | [CreateLoadList](#createloadlist) |


## 4. Completed private-reference coverage

The read-only main project declares **115 IOSLoad method overloads**. Current source matches **all 115** by method name, generic arity and parameter types; **no reference interface overload remains pending**. Five repository conveniences bring the total to **120**. New parameter names are descriptive while reference types, order, returns and defaults are retained. This does not include every adapter-only source convenience or every Bentley native function.

Three pre-existing clear-primary overloads additionally make isReferenceLoad optional with default false, whereas the main interface requires that Boolean argument. This existing convenience difference is unchanged; coverage compares method names, generic arity and parameter types. All newly ported overloads retain the main reference return types and optional defaults.

The former 30-overload backlog is complete. The original source fingerprints and baseline inventory remain in the [comparison evidence](data/OpenSTAAD-Load-2024-2026-Comparison.json). Broader named-result/semantic capability API design remains proposed and cannot be introduced by changing return types alone.

**Repository convenience overloads outside that interface:**

- `IOSLoad CreatePrimaryLoadCases(IEnumerable<ILoadCase> primaryLoadCases)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L522)).
- `IOSLoad CreateReferenceLoadCases(IEnumerable<ILoadCase> referenceLoads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L528)).
- `IOSLoad AddPlateUniformPressure(ILoadCase lc, int plateId, double pressure, LoadDirection direction = LoadDirection.LocalZ)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L555)).
- `IOSLoad AddPlateUniformPressure(ILoadCase lc, IEnumerable<int> plateIds, double pressure, LoadDirection direction = LoadDirection.LocalZ)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L558)).
- `IOSLoad AddPlateUniformPressure(ILoadCase lc, IEnumerable<Plate> plates, double pressure, LoadDirection direction = LoadDirection.LocalZ)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L561)).

## 5. Vendor enum inventory and supporting model reuse

The thirteen enum types used by the inspected native signatures have identical names, Int32 underlying type and values in the 2025/2026 assemblies. These tables do not corroborate a 2024 ABI. An enum constant's presence alone does not prove a function accepts that constant.

### OpenSTAADUI.ASCE7_BldgClass

| Constant | Value |
| --- | ---: |
| `ASCE7_TypeI` | 0 |
| `ASCE7_TypeII` | 1 |
| `ASCE7_TypeIII` | 2 |
| `ASCE7_TypeIV` | 3 |

### OpenSTAADUI.ASCE7_BldgType

| Constant | Value |
| --- | ---: |
| `ASCE7_Building` | 0 |
| `ASCE7_Chimney` | 1 |
| `ASCE7_Solidsign` | 2 |
| `ASCE7_Opensign` | 3 |
| `ASCE7_Laticeframe` | 4 |
| `ASCE7_Trusstower` | 5 |

### OpenSTAADUI.ASCE7_ExposureCategory

| Constant | Value |
| --- | ---: |
| `ASCE7_ExpA` | 0 |
| `ASCE7_ExpB` | 1 |
| `ASCE7_ExpC` | 2 |
| `ASCE7_ExpD` | 3 |

### OpenSTAADUI.ASCE7_Publication

| Constant | Value |
| --- | ---: |
| `ASCE7Y1995` | 0 |
| `ASCE7Y2002` | 1 |
| `ASCE7Y2010` | 2 |
| `ASCE7Y2016` | 3 |
| `ASCEY_LAST` | 4 |

### OpenSTAADUI.ASCE7_WallType

| Constant | Value |
| --- | ---: |
| `ASCE7_WindWard` | 0 |
| `ASCE7_LeeWard` | 1 |
| `ASCE7_SideWall` | 2 |

### OpenSTAADUI.BldgClass

| Constant | Value |
| --- | ---: |
| `TypeI` | 0 |
| `TypeII` | 1 |
| `TypeIII` | 2 |
| `TypeIV` | 3 |

### OpenSTAADUI.BldgType

| Constant | Value |
| --- | ---: |
| `Building` | 0 |
| `Chimney` | 1 |
| `Solidsign` | 2 |
| `Opensign` | 3 |
| `Laticeframe` | 4 |
| `Trusstower` | 5 |

### OpenSTAADUI.DirectAnalysisParameterTypes

| Constant | Value |
| --- | ---: |
| `DA_FLEX` | 0 |
| `DA_FYLD` | 1 |
| `DA_AXIAL` | 2 |

### OpenSTAADUI.ExposureCategory

| Constant | Value |
| --- | ---: |
| `ExpA` | 0 |
| `ExpB` | 1 |
| `ExpC` | 2 |
| `ExpD` | 3 |

### OpenSTAADUI.LoadingCode

| Constant | Value |
| --- | ---: |
| `ASCE7Y95` | 0 |
| `ASCE7Y02` | 1 |
| `ASCE7Y05_10` | 2 |
| `AISC97` | 3 |
| `AISC02` | 4 |

### OpenSTAADUI.LoadType

| Constant | Value |
| --- | ---: |
| `nodalLoad` | 3110 |
| `memberLoadUni` | 3210 |
| `memberLoadUniMom` | 3220 |
| `memberLoadCon` | 3230 |
| `memberLoadConMom` | 3240 |
| `memberLoadLinear` | 3250 |
| `memberLoadTrap` | 3260 |
| `elementLoadPressure` | 3310 |
| `elementLoadJoint` | 3311 |
| `elementLoadJtPressure` | 3312 |
| `elementLoadTrap` | 3320 |
| `elementLoadFacePressure` | 3322 |

### OpenSTAADUI.StrengthType

| Constant | Value |
| --- | ---: |
| `STRENGTH_TYPE_NONE` | 0 |
| `NORMAL_ASD_WORKING_STRESS_FORCES_WITHOUT_P_DELTA` | 1 |
| `NORMAL_ASD_WORKING_STRESS_FORCES_WITH_P_DELTA` | 2 |
| `STRENGTH_TYPE_OF_FORCES_WITHOUT_P_DELTA` | 3 |
| `STRENGTH_TYPE_OF_FORCES_WITH_P_DELTA` | 4 |
| `COLUMN_ONLY_STRENGTH_TYPE_OF_FORCES_WITHOUT_P_DELTA` | 5 |
| `COLUMN_ONLY_STRENGTH_TYPE_OF_FORCES_WITH_P_DELTA` | 6 |

### OpenSTAADUI.WallType

| Constant | Value |
| --- | ---: |
| `WindWard` | 0 |
| `LeeWard` | 1 |
| `SideWall` | 2 |

**Unverified extras:** DA_FYLD and the AISC LoadingCode choices are metadata evidence only; the reviewed function tables do not establish supported usage. The native LoadType enum is a command-kind domain, not the repository case-type enum.

### Read-only main-project model candidates

Reuse or adapt these existing source models before introducing parallel abstractions. Their presence does not mean their native numeric encodings, validation or array layouts are correct for every supported version. Preserve public compatibility deliberately; new results and policies remain separate.

| Area | Existing source file | Disposition |
| --- | --- | --- |
| Wind | [ASCE72016WallWindPressureProfileParameters.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/Loads/Wind/ASCE72016WallWindPressureProfileParameters.cs) | Ported with finite-value/enum validation and documented packet contracts; native numerical validation remains pending. |
| Wind | [ASCE7BuildingData.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/Loads/Wind/ASCE7BuildingData.cs) | Ported with finite-value/enum validation and documented packet contracts; native numerical validation remains pending. |
| Wind | [ASCE7OpenSignData.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/Loads/Wind/ASCE7OpenSignData.cs) | Ported with finite-value/enum validation and documented packet contracts; native numerical validation remains pending. |
| Wind | [ASCE7SolidSignData.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/Loads/Wind/ASCE7SolidSignData.cs) | Ported with finite-value/enum validation and documented packet contracts; native numerical validation remains pending. |
| Wind | [ASCE7TankData.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/Loads/Wind/ASCE7TankData.cs) | Ported with finite-value/enum validation and documented packet contracts; native numerical validation remains pending. |
| Wind | [ASCE7TrussedTowerData.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/Loads/Wind/ASCE7TrussedTowerData.cs) | Ported with finite-value/enum validation and documented packet contracts; native numerical validation remains pending. |
| Wind | [ASCE7WindDefinitionParameters.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/Loads/Wind/ASCE7WindDefinitionParameters.cs) | Ported with finite-value/enum validation and documented packet contracts; native numerical validation remains pending. |
| Wind | [IASCE7StructureData.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/Loads/Wind/IASCE7StructureData.cs) | Ported with finite-value/enum validation and documented packet contracts; native numerical validation remains pending. |
| Wind | [WallWindPressureProfileParameters.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/Loads/Wind/WallWindPressureProfileParameters.cs) | Ported with finite-value/enum validation and documented packet contracts; native numerical validation remains pending. |
| Wind | [WindEscarpmentData.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/Loads/Wind/WindEscarpmentData.cs) | Ported with finite-value/enum validation and documented packet contracts; native numerical validation remains pending. |
| Wind | [WindFactorsConfiguration.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/Loads/Wind/WindFactorsConfiguration.cs) | Ported with finite-value/enum validation and documented packet contracts; native numerical validation remains pending. |
| Wind | [WindIntensityPair.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/Loads/Wind/WindIntensityPair.cs) | Already ported; reuse existing value semantics. |
| Wind | [WindUnitsConfiguration.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/Loads/Wind/WindUnitsConfiguration.cs) | Ported with finite-value/enum validation and documented packet contracts; native numerical validation remains pending. |
| Seismic | [DirectAnalysisParameters.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/Loads/Seismic/DirectAnalysisParameters.cs) | Ported with checked input capture and complete XML documentation; native marshaling/engineering verification remains pending. |
| Seismic | [ResponseSpectrumLoadExParameters.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/Loads/Seismic/ResponseSpectrumLoadExParameters.cs) | Ported with checked input capture and complete XML documentation; native marshaling/engineering verification remains pending. |
| Seismic | [ResponseSpectrumLoadParameters.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/Loads/Seismic/ResponseSpectrumLoadParameters.cs) | Ported with checked input capture and complete XML documentation; native marshaling/engineering verification remains pending. |
| Seismic | [SeismicDefinitionParameters.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/Loads/Seismic/SeismicDefinitionParameters.cs) | Ported IS1893-2016, IS1893-2002, IBC and shear-wall dimension models; wall-area APIs are also implemented. |
| Seismic | [SeismicEnums.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/Loads/Seismic/SeismicEnums.cs) | Ported definition/distribution, wall, spectrum and direct-analysis enums required by the interface. Semantic member-weight enum translation remains proposed. |

## 6. Source-page fingerprints

All 24 pages were read for each year, including zero-function grouping sections. SHA-256 and byte length identify the installed files used for this review. Links target local help; they depend on those installations and are not portable public web references. Read-only main-project links use file URLs because that project is on a different drive; a preview may require explicit permission to open local links.

| Year | Source page | Functions | Bytes | SHA-256 |
| --- | --- | ---: | ---: | --- |
| 2024 | [group___load_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___load_group.html) | 2 | 18074 | `1e49c98ea3694d07486f7496490596939edcb0805a72084c771687217a15f11f` |
| 2024 | [group___property_load_def.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def.html) | 0 | 7538 | `7b35388128999d0b69b3676b1dc73a5a3734104f1551f663fc38d1456fdebc25` |
| 2024 | [group___property_load_case.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_case.html) | 2 | 12245 | `454b06043afe804a2672076caac775bb0ec4089ebe708fd05f853233988cce63` |
| 2024 | [group___property_load_enve.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_enve.html) | 8 | 43102 | `fbf67d1b621433a75345a4b44d1b879ce126ba6d34eddec55889e47fae9fca0e` |
| 2024 | [group___property_load_def_wind.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_wind.html) | 7 | 68055 | `6e63fd823d3226f79826117db3807754d109e19046d874ef751c3ad808032627` |
| 2024 | [group___property_load_def_seismic.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_seismic.html) | 7 | 48990 | `44a3fe5e127034f57c7db4068bed354030ece985e504f49429fee9dc6accbdd4` |
| 2024 | [group___property_load_def_t_h.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_t_h.html) | 0 | 5307 | `06e480e73dedad92620537ea6a0d810481d9fb810b5ee040e6577024223f2f4d` |
| 2024 | [group___property_load_def_snow.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_snow.html) | 0 | 5284 | `4d9f6b76f2b3f570ded99b31b3ac48b789477b02c8f3f4efbe9c57d77454931d` |
| 2024 | [group___property_load_def_ref_load.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_ref_load.html) | 4 | 25418 | `36b9a12f39bd9cde97d69fb35fce3fe155c7edc3a100bee620650399107bee77` |
| 2024 | [group___property_direct_analysis.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_direct_analysis.html) | 3 | 18332 | `a059dbb02f12ec29a4cdc3d5e2b00f02d03dff77ecc0a51befbfb2fe0b769a30` |
| 2024 | [group___property_load_items.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_items.html) | 0 | 9137 | `f63f740624953cec836bffc7e8cafe561983f01ce2cafa04edbbc7d4c200e8c1` |
| 2024 | [group___property_load_comb.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_comb.html) | 9 | 47620 | `d0c43423ca6ff6abe52f79c9969a7a9e0b2fbb3df165852844a9bb6673705ca5` |
| 2024 | [group___property_load_operation.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html) | 26 | 122491 | `127594bc04b483aa18a29a946325ae0a3d0e95c167c6dfe1493a569ac7a33376` |
| 2024 | [group___property_load_self_weight.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_self_weight.html) | 2 | 15188 | `ad88a9bc0bc9e5d000914c60c0963e1a49f07ec7c20295fc121d451433a67fb4` |
| 2024 | [group___property_load_nodal.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_nodal.html) | 5 | 25625 | `fe5c27731298b4dc9af47b32bf524622342f0c8cbeb8b9949c6d557db48a6ec8` |
| 2024 | [group___property_load_mem.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html) | 21 | 95557 | `b6b9a67e4adfda38b4f179b141802e2fc0d08cd297c93959617b9516d780ead5` |
| 2024 | [group___property_load_p_mem.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_p_mem.html) | 0 | 5323 | `dca76d773c8c79a9cc7e6dce8d912a9cb82ffa7768843890fb450cfdd048b31b` |
| 2024 | [group___property_load_ele.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_ele.html) | 8 | 52212 | `34f1340baaf26eccbc34730c5646e91ea9189acbca97f33792e465618ab7770e` |
| 2024 | [group___property_load_floor.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_floor.html) | 4 | 33539 | `0d1d37ee1863c1f549aa9ac97dda9fb20bae116cd3f48d2559543bf1cb27dc58` |
| 2024 | [group___property_load_temp.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_temp.html) | 2 | 12481 | `0023ba92a1ee157cf3dd02492753e8dabc7490164b05e1b536ddebc758e916b4` |
| 2024 | [group___property_load_seismic.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_seismic.html) | 2 | 11581 | `d941a6541fb175a0609b32cd85e2521fb7e167a7ea0cdd58b9a0b3d7182d6cac` |
| 2024 | [group___property_load_t_h.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_t_h.html) | 0 | 5297 | `f21077a92d4ef1f5a8e3b490f2beae4cdd1acdcaedef6f42ab7a0c60b4c67d53` |
| 2024 | [group___property_load_wind.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_wind.html) | 1 | 12404 | `138a7cd3df36f5566759336c1836a78840515661b258a2496820833a0e2d44a9` |
| 2024 | [group___property_load_repeat.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html) | 16 | 73188 | `a9a92528a7a56ee644a831106be1da8982c92f0ff58ae6d261e856edee46b772` |
| 2025 | [group___load_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___load_group.html) | 2 | 17956 | `d95317682734521576fc88680b534c019b83304c82f6a9b1934f9e64bc6a31c6` |
| 2025 | [group___property_load_def.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def.html) | 0 | 6849 | `5b19db4338b69385a424476e3a20e5861ee8e358a64f98aeeba73b8dd4038455` |
| 2025 | [group___property_load_case.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_case.html) | 2 | 12628 | `a8fa9ba27667f2ba853f42812ff0d08bb4132c658df81fcb6652d38d1fb113aa` |
| 2025 | [group___property_load_enve.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_enve.html) | 8 | 46139 | `1f92da70a11f9ac9428df09c5b8dfe52d7f3494454001eaf55229727dc93f1f1` |
| 2025 | [group___property_load_def_wind.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_wind.html) | 7 | 86022 | `8066bc1354831571c267117a2cc85fdff7a524919c644fbdc3d49d0b3a66d9f7` |
| 2025 | [group___property_load_def_seismic.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_seismic.html) | 7 | 61281 | `725c57997cc88f3f520c588da2c35abe04077deac8bd98d04d9ae452a8e5b500` |
| 2025 | [group___property_load_def_t_h.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_t_h.html) | 0 | 4858 | `57f1f3f80ced88558cd1becc07eb99ce08cdab34b9a96d4c40b9113ac68db059` |
| 2025 | [group___property_load_def_snow.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_snow.html) | 0 | 4835 | `17211808d4f7e9f5f7bdf6c664a0b366c03b1eccca85755e860eac448453f93e` |
| 2025 | [group___property_load_def_ref_load.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_ref_load.html) | 4 | 27906 | `939de44c98d6eaaec26a37943523df8c7b4b89cb43d6e27385508da43d2e082a` |
| 2025 | [group___property_direct_analysis.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_direct_analysis.html) | 3 | 19150 | `179a9aff78a9f05b0165d31e11a43d43d146dd25b4b93ff2058bd72ce6ca5dd3` |
| 2025 | [group___property_load_items.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_items.html) | 0 | 8400 | `7590fed2b8ab7d55360e564da67e0606a496dea88e9f99f30e6c0f1058b807dc` |
| 2025 | [group___property_load_comb.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_comb.html) | 9 | 51276 | `ea4f6bf49dba0adfe5b2ff5dbaf4a56a1b11ae3f4d382a09babf2e3589e07fd8` |
| 2025 | [group___property_load_operation.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html) | 26 | 141146 | `09797de1a66518251040851dcb573bbc0d8b748bf48a8e232a9fc42a26a9f588` |
| 2025 | [group___property_load_self_weight.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_self_weight.html) | 2 | 15487 | `8ff1e1be20a8e4997fa3060f378ef465ecee9e7e8666bbfe77d488d2f873cff0` |
| 2025 | [group___property_load_nodal.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_nodal.html) | 5 | 27860 | `e7868c365eee21bb5ba70e4a7593eece5f7722fcde1f52f8a3c79f1350927a5f` |
| 2025 | [group___property_load_mem.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html) | 21 | 105830 | `f414adaf370de647b51bc5a054609da3e003177d01d80e050cc4a8b1c5218e21` |
| 2025 | [group___property_load_p_mem.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_p_mem.html) | 0 | 4963 | `3355cfa3e249475e17c051bc02ff4a3c89e45e5c5e012b92d46aa4675ac2fd2b` |
| 2025 | [group___property_load_ele.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_ele.html) | 8 | 55424 | `50b65146e5ab3e6be1e8f567f0dbab025fe952f63ec44e2ad95fd76eaacfb4eb` |
| 2025 | [group___property_load_floor.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_floor.html) | 4 | 34368 | `0e82f34eaf5f376104790b3f86e6c4b4d3816be8b669696e7c72e0c7aa536e25` |
| 2025 | [group___property_load_temp.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_temp.html) | 2 | 13165 | `767d957eea41707d73f886348c235ff95965335c49a9ed11008a97b552f55f8b` |
| 2025 | [group___property_load_seismic.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_seismic.html) | 2 | 11982 | `cead0e5bec6e09a05c6cff9f2a618464a53edf6d448e54d05c09a3edf2820274` |
| 2025 | [group___property_load_t_h.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_t_h.html) | 0 | 4937 | `bd2cd0cebcb4c4bec804cebd43ee88dfe74dfb3e7796bcbfee0794f1440a4dee` |
| 2025 | [group___property_load_wind.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_wind.html) | 1 | 13246 | `5b6a751660c17162568630ae8d739f9b681a01fd2cdc02652746a9d28d7c87e6` |
| 2025 | [group___property_load_repeat.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html) | 16 | 77449 | `a7449721f4a6affa7a3c0ed475a7c463e0342cd4a827a5be415a853d200926b4` |
| 2026 | [group___load_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html) | 2 | 17956 | `ca01309ffd461d86d88efd4dfc7fb52c4b0313b06a679f89914d08d9117a98af` |
| 2026 | [group___property_load_def.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def.html) | 0 | 6849 | `ee85be5cba8855b54d73542cf3dc39d7c76aecb30cadf4508e90e679a5831387` |
| 2026 | [group___property_load_case.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_case.html) | 2 | 12628 | `137137ceb057a2f88a1c270380ad5006472a844244c1d3642bbaa96f2289bc8d` |
| 2026 | [group___property_load_enve.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html) | 8 | 46139 | `b3751955fb87dc8f19f1fd94b5c2dc7c5801bad2bfe650cc3e2f52b54ac7211c` |
| 2026 | [group___property_load_def_wind.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html) | 7 | 86022 | `565aa7894cc5df042562edeca663ad4677b555e43459f0b1beb046cea9469758` |
| 2026 | [group___property_load_def_seismic.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html) | 7 | 61281 | `eb22ec0462b464bb1945911846a1bfae7cc10abc1ecb2be36be37453e71abe82` |
| 2026 | [group___property_load_def_t_h.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_t_h.html) | 0 | 4858 | `ad92925acf30ad246bc81bf7856772e19deec16122bb61e9706c91666d19af36` |
| 2026 | [group___property_load_def_snow.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_snow.html) | 0 | 4835 | `14b55ea05460e470654e98f02fd1e7be4107689b66baff4e540a73f5d319b241` |
| 2026 | [group___property_load_def_ref_load.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_ref_load.html) | 4 | 27906 | `ef867af74697c6910ce5cefe5e6439896bc792f696cf3dcb31a47d385801efca` |
| 2026 | [group___property_direct_analysis.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_direct_analysis.html) | 3 | 19150 | `0e742f0bdb742251b548ece132f340f4bac665134925be5329a482fa847105cd` |
| 2026 | [group___property_load_items.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_items.html) | 0 | 8400 | `0e2287fe7193baf5c85aef065398de297da847a3acf2f3a1c67e1633aa342708` |
| 2026 | [group___property_load_comb.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html) | 9 | 51276 | `7a4813a2725f90f78b84579200ebb8f801c600732bcb5047e5699b744c238731` |
| 2026 | [group___property_load_operation.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html) | 26 | 141146 | `96de401a444ad400c5775034b070fb267bfe78364b76695f9c93d9a520213336` |
| 2026 | [group___property_load_self_weight.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_self_weight.html) | 2 | 15487 | `bc192da0daa919a1fbbc9b914acce7754d52d3564d376dfe577dba6a52661078` |
| 2026 | [group___property_load_nodal.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html) | 5 | 27860 | `e848e7c2861b21feea44bfbdd1a5559afcc302b631150cbfc701592b2ca52532` |
| 2026 | [group___property_load_mem.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html) | 21 | 105830 | `d61892ade37a584f89654173fa0d8bb0d872b8af35551004f5a181e4bccd795d` |
| 2026 | [group___property_load_p_mem.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_p_mem.html) | 0 | 4963 | `91e49a3be1925309ba7fea0ba909ed5a9932cbe1da4d2544f8767f372d356f3f` |
| 2026 | [group___property_load_ele.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html) | 8 | 55424 | `cbee7acfa73fda7b53dde30e7b39601d47f788ab57353d488a0b4097f1223b36` |
| 2026 | [group___property_load_floor.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html) | 4 | 34368 | `db101a86f557ba9ca24f035d7da0687c38e37428af3366db2afc93213c1b3143` |
| 2026 | [group___property_load_temp.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_temp.html) | 2 | 13165 | `4b451a838bf66e56a1b3fdc294eaae89929932a9e77c4fec01f9a5e1fcb08978` |
| 2026 | [group___property_load_seismic.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_seismic.html) | 2 | 11982 | `6cb6485cfdd3d1e077f03e6c42dfe4655e1a7dfd97af5b00b24b7ea41a0bfb8c` |
| 2026 | [group___property_load_t_h.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_t_h.html) | 0 | 4937 | `3244993f8a69d52055460facfd2770c5a3b08a90daafa0675a1b5a3d75a72943` |
| 2026 | [group___property_load_wind.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_wind.html) | 1 | 13246 | `4ebc2905cd3bc68e10ba4c4a371c88bb64a95e4a709e4814896fe773e3b5d369` |
| 2026 | [group___property_load_repeat.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html) | 16 | 77449 | `3ddf0fb6eccaf1f392a85038ce835ad5aae61e29df5d09d7849b4c373a10c3bb` |

## 7. Evidence boundaries

The original review inspected production source without edits; subsequent increments ported all 30 originally pending reference overloads and updated this inventory. The main project remained read-only throughout. Native method presence, reviewed help and passive vendor metadata do not establish actual acquired-server compatibility, licensing, engineering correctness or package contents. No live STAAD mutation or package build was performed. See the primary report for verification gates and unresolved contracts.
