# OpenSTAAD Load API coverage and contract inventory

**Review date:** 22 October 2026
**Baseline:** main at 1483c9c
**Status:** Original native evidence and API design proposals, updated with all 30 originally pending reference-overload implementations. All 36 approved expansion contracts are now implemented; broader legacy migration and native qualification remain separate work.

This annex supports the [Load multi-version architecture and plan](OpenSTAAD-Load-Multi-Version-Architecture-and-Plan.md). It inventories every detailed Load function from the installed 2024/2025/2026 help and distinguishes current native dispatches from planned additions.

## Current source — approved Load expansion

Implemented all **36 approved Load expansion methods** through the existing IOSLoad and single OSLoadAdapter. Source exposes **171 overloads**: 115 main-reference signatures, five earlier conveniences, 15 inspection methods and these 36 additions. Actual dispatch covers **126/129 documented native names**, plus the metadata-corroborated MergeLoadsOnBeam body. The three intentionally unused names are CreateNewPrimaryLoad (richer creation alternatives), IsCombinationCase (derived inventory capability), and AddMemberFloorLoad (the extended floor call supplies the managed route). Native dispatch coverage is not live compatibility or engineering certification.

GetLoadAttribute is a documented status-only observation and does not decode ASD/LSD settings. MergeBeamLoads returns InvocationCompleted with IsSuccessful false because Bentley does not document the merge-body response meanings. Reference-command index base, nodal index convention, plate-command UI selection, floor distribution and live 2024–2026 marshaling remain explicit qualification requirements. See [Load expansion usage and method comparisons](../Load-Expansion.md).

The [approved contract/type record](OpenSTAAD-Load-Expansion-Contracts-and-Type-Plan.md) retains the 56 original audit mappings, all required model/enum definitions and read-only main-source reuse decisions. All 36 signatures are callable; the separate contract file is now a test-only approved signature snapshot. Earlier numerical baselines below remain historical.

## Historical managed capability audit — 23 October 2026

The **56 native names without dispatch are not 56 missing managed features**. A semantic review found **one covered alternative** (primary creation), **one derived capability** (combination membership), **four partial overlaps**, and **50 native operations without an implemented managed equivalent**. Consequently 54 native contracts still have outstanding scope, but they do not necessarily require 54 separate public methods. No production code changed in this audit. See the [complete managed capability audit](OpenSTAAD-Load-Managed-Capability-Audit.md) for all 56 dispositions, source evidence and a revised proposed next 15.

## 1. Complete documented native inventory

There are **129 distinct native functions in each year**, all with detailed entries. The native summary and detailed declarations match across product years after formatting normalization. Parameter identifiers can differ between a summary, a detailed prototype, its parameter table and vendor interop metadata; those differences are preserved rather than treated as additional overloads.

Each entry includes the common native declaration, positional parameters, documented directions, inspected 2025/2026 CLR shape, return/error information and current source callers. C++ FAR/const reference notation must not be mechanically translated into public C# ref parameters. IsIn/IsOut flags in inspected metadata are often both false; Object& still records a CLR by-reference parameter.

**S:** internal Load service dispatch. **D:** direct adapter dispatch. **E:** delegated member-load mutation. **A:** unused native entry point with a managed alternative/composition. There are 126 exact documented dispatches and three A entries. Public-method coverage is separate from live qualification; method-specific limitations below are authoritative.

Return tables below preserve functional error labels and short native explanations. A missing return table does not imply void: some entries describe the return in prose. Follow the linked reference and the declared return type. Native counts, IDs, Boolean values and zero/one success codes must not share a universal converter.

Parameter detail excerpts identify meaning and link back to full code/array tables. Long engineering tables are summarized by an excerpt; the primary report analyzes changed seismic encodings and shared array ambiguities. No default is implied when the native declaration does not specify one.

### Load

#### AddRSLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#ga1b938bf65248e91a620868453a458b9d)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [AddRSLoad — line 1076](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1076).

#### SplitLoadsOnBeam

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___load_group.html#gaa4ef18bf50db77e41cc496a915955be4) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___load_group.html#gaa4ef18bf50db77e41cc496a915955be4) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___load_group.html#gaa4ef18bf50db77e41cc496a915955be4)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use after splitting beam geometry when the existing load definitions must be transferred to the two beam IDs. Unlike AddMemberLoad, this edits existing load definitions and does not create beam geometry or assign a new load template. Unlike MergeBeamLoads, it splits rather than combines definitions. Native scope across cases is controlled by STAAD; no managed case filter is implied. The IDs follow the native declaration and VBA example; its parameter-table description is erroneous. Native TRUE/1 means success.

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


**Current call sites:** [SplitLoadsOnBeam — line 18](../../src/StaadPro.Interop/Services/Load/LoadBeamTransferService.cs#L18).

**Implemented managed contract:**

~~~csharp
LoadOperationResult SplitLoadsOnBeam(int originalBeamId, int newBeamId);
~~~

**Behavior and alternatives:** Use after splitting beam geometry when the existing load definitions must be transferred to the two beam IDs. Unlike AddMemberLoad, this edits existing load definitions and does not create beam geometry or assign a new load template. Unlike MergeBeamLoads, it splits rather than combines definitions. Native scope across cases is controlled by STAAD; no managed case filter is implied. The IDs follow the native declaration and VBA example; its parameter-table description is erroneous. Native TRUE/1 means success. See [usage and qualification limits](../Load-Expansion.md#splitloadsonbeam).

### Load: Definition

No native function declarations occur in this reviewed grouping page. Subgroups, where present, were traversed separately.

### Load: Load Case Details

#### GetPrimaryLoadCaseCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_case.html#ga8c4ac07c10d5cfd0a2bda5d3f837db24) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_case.html#ga8c4ac07c10d5cfd0a2bda5d3f837db24) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_case.html#ga8c4ac07c10d5cfd0a2bda5d3f837db24)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

```cpp
VARIANT OSLoadUI::GetPrimaryLoadCaseCount();
```

No native parameters.

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

No return-value table is present. Read the return prose in the linked reference; do not infer Boolean success or invent a payload.


**Current call sites:** [ReadAllLoadCases — line 3258](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3258).

#### SetLoadActive

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_case.html#ga065a2faa02e196d4ba47854653a7946b) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_case.html#ga065a2faa02e196d4ba47854653a7946b) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_case.html#ga065a2faa02e196d4ba47854653a7946b)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [SetLoadCaseActive — line 2762](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2762); [ActivateQueryCase — line 3342](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3342); [Active — line 60](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L60); [Activate — line 63](../../src/StaadPro.Interop/Services/Load/LoadServiceContext.cs#L63).

### Load: Load Envelopes

#### CreateLoadEnvelop

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_enve.html#ga9f28063ba9f67609d5489f069da32154) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_enve.html#ga9f28063ba9f67609d5489f069da32154) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#ga9f28063ba9f67609d5489f069da32154)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [CreateLoadEnvelopExt — line 3174](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3174).

#### AddLoadCasesToEnvelop

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_enve.html#ga16a1d26c9c356a3c96cf81d43fac81fa) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_enve.html#ga16a1d26c9c356a3c96cf81d43fac81fa) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#ga16a1d26c9c356a3c96cf81d43fac81fa)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to change membership of an existing envelope. CreateLoadEnvelopExt creates an envelope; CreateLoadCombinations creates combination cases. This method does neither and never changes load factors. Pass primary case IDs, not an envelope index or LOAD LIST record index. Native 1 means success.

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


**Current call sites:** [AddCasesToEnvelope — line 17](../../src/StaadPro.Interop/Services/Load/LoadCaseManagementService.cs#L17).

**Implemented managed contract:**

~~~csharp
LoadOperationResult AddCasesToEnvelope(int envelopeId, IEnumerable<int> primaryLoadCaseIds);
~~~

**Behavior and alternatives:** Use to change membership of an existing envelope. CreateLoadEnvelopExt creates an envelope; CreateLoadCombinations creates combination cases. This method does neither and never changes load factors. Pass primary case IDs, not an envelope index or LOAD LIST record index. Native 1 means success. See [usage and qualification limits](../Load-Expansion.md#addcasestoenvelope).

#### RemoveLoadCasesFromEnvelop

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_enve.html#gac630898fc921f930e89fa9f8701ea9ba) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_enve.html#gac630898fc921f930e89fa9f8701ea9ba) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#gac630898fc921f930e89fa9f8701ea9ba)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to remove selected memberships while retaining the envelope and the load-case definitions. DeleteEnvelope removes the envelope itself; DeleteLoadCases removes case definitions. An empty list is rejected and never means remove all. Native 1 means success.

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


**Current call sites:** [RemoveCasesFromEnvelope — line 22](../../src/StaadPro.Interop/Services/Load/LoadCaseManagementService.cs#L22).

**Implemented managed contract:**

~~~csharp
LoadOperationResult RemoveCasesFromEnvelope(int envelopeId, IEnumerable<int> primaryLoadCaseIds);
~~~

**Behavior and alternatives:** Use to remove selected memberships while retaining the envelope and the load-case definitions. DeleteEnvelope removes the envelope itself; DeleteLoadCases removes case definitions. An empty list is rejected and never means remove all. Native 1 means success. See [usage and qualification limits](../Load-Expansion.md#removecasesfromenvelope).

#### DeleteLoadEnvelop

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_enve.html#ga1ae3d8ba1801dbf2b3e85dff88948d05) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_enve.html#ga1ae3d8ba1801dbf2b3e85dff88948d05) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#ga1ae3d8ba1801dbf2b3e85dff88948d05)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to delete an envelope definition. The cases previously listed in it remain case definitions; use DeleteLoadCases only when those cases should also be deleted. This method does not delete a LOAD LIST. Native 1 means success.

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


**Current call sites:** [DeleteEnvelope — line 27](../../src/StaadPro.Interop/Services/Load/LoadCaseManagementService.cs#L27).

**Implemented managed contract:**

~~~csharp
LoadOperationResult DeleteEnvelope(int envelopeId);
~~~

**Behavior and alternatives:** Use to delete an envelope definition. The cases previously listed in it remain case definitions; use DeleteLoadCases only when those cases should also be deleted. This method does not delete a LOAD LIST. Native 1 means success. See [usage and qualification limits](../Load-Expansion.md#deleteenvelope).

#### GetEnvelopeCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_enve.html#ga0b8f688b0cddf6f418356af7112bb474) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_enve.html#ga0b8f688b0cddf6f418356af7112bb474) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#ga0b8f688b0cddf6f418356af7112bb474)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

```cpp
long OSLoadUI::GetEnvelopeCount();
```

No native parameters.

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `Long/Integer` | Total Number of load Envelopes present. |


**Current call sites:** [GetEnvelopeIds — line 3009](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3009).

#### GetLoadEnvelopeDetails

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_enve.html#gaf8b78c76d6f4c0151f6534f5848e77d6) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_enve.html#gaf8b78c76d6f4c0151f6534f5848e77d6) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#gaf8b78c76d6f4c0151f6534f5848e77d6)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [GetLoadEnvelopeDetail — line 3053](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3053).

#### GetLoadListfromLoadEnvelope

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_enve.html#ga05374c9b5b38ea705b4db65d6f3ce20a) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_enve.html#ga05374c9b5b38ea705b4db65d6f3ce20a) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#ga05374c9b5b38ea705b4db65d6f3ce20a)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [GetLoadEnvelopeDetail — line 3061](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3061).

#### GetEnvelopeIDs

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_enve.html#gaea479c68d7b2b2114d16e1ffd1a5a199) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_enve.html#gaea479c68d7b2b2114d16e1ffd1a5a199) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_enve.html#gaea479c68d7b2b2114d16e1ffd1a5a199)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [GetEnvelopeIds — line 3013](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3013).

### Load Definition: Wind

#### AddWindDefinition

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_wind.html#ga2ae52c29c70f50fad759adefb71761e4) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_wind.html#ga2ae52c29c70f50fad759adefb71761e4) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga2ae52c29c70f50fad759adefb71761e4)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [AddWindDefinition — line 1279](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1279).

#### AddWindIntensity

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_wind.html#ga8a39105b37ab53253129a83229559bee) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_wind.html#ga8a39105b37ab53253129a83229559bee) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga8a39105b37ab53253129a83229559bee)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [AddWindIntensity — line 1322](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1322); [AddWindIntensity — line 1337](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1337).

#### ComputeWallWindPressureProfile

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_wind.html#ga14842eb0090688108f87a2412922b71d) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_wind.html#ga14842eb0090688108f87a2412922b71d) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga14842eb0090688108f87a2412922b71d)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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



**Current call sites:** [ComputeWallWindPressureProfile — line 1108](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1108).

#### ComputeWallWindPressureProfileASCE72016

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_wind.html#ga09d85745d67966ad0b323ddf4751b739) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_wind.html#ga09d85745d67966ad0b323ddf4751b739) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga09d85745d67966ad0b323ddf4751b739)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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



**Current call sites:** [ComputeWallWindPressureProfileASCE72016 — line 1121](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1121).

#### AddWindDefinitionASCE7Parameters

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_wind.html#gaeaa2d3d5e5b318160defa2f8596dc343) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_wind.html#gaeaa2d3d5e5b318160defa2f8596dc343) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#gaeaa2d3d5e5b318160defa2f8596dc343)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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



**Current call sites:** [AddWindDefinitionASCE7Parameters — line 1094](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1094).

#### AddWindExposure

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_wind.html#ga30c27ad6bfef60382db306f1d8168cef) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_wind.html#ga30c27ad6bfef60382db306f1d8168cef) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga30c27ad6bfef60382db306f1d8168cef)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [AddWindExposure — line 1291](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1291).

#### DeleteWindDefinition

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_wind.html#ga0b2356ca2a0d681720b9da8d3c022cc9) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_wind.html#ga0b2356ca2a0d681720b9da8d3c022cc9) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_wind.html#ga0b2356ca2a0d681720b9da8d3c022cc9)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [DeleteWindDefinition — line 1353](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1353).

### Load Definition: Seismic

#### AddSeismicDefinition

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_seismic.html#ga885d12987341c6323c4c71b02979af9b) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_seismic.html#ga885d12987341c6323c4c71b02979af9b) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#ga885d12987341c6323c4c71b02979af9b)

**Comparison:** Capability. **Coverage:** D — production dispatch; native qualification remains pending.

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



**Current call sites:** [AddSeismicDefinition — line 1142](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1142).

#### ModifySeismicDefinitionParams

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_seismic.html#ga5b4e6c8e6e7c46b829de3cb21c1f963d) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_seismic.html#ga5b4e6c8e6e7c46b829de3cb21c1f963d) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#ga5b4e6c8e6e7c46b829de3cb21c1f963d)

**Comparison:** Capability. **Coverage:** D — production dispatch; native qualification remains pending.

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



**Current call sites:** [ModifySeismicDefinitionParam — line 1150](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1150).

#### AddSeismicDefSelfWeight

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_seismic.html#ga0aee693e0d969e310fc41938f5ca6cd3) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_seismic.html#ga0aee693e0d969e310fc41938f5ca6cd3) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#ga0aee693e0d969e310fc41938f5ca6cd3)

**Comparison:** Clarified. **Coverage:** D — production dispatch; native qualification remains pending.

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



**Current call sites:** [AddSeismicDefSelfWeight — line 1196](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1196).

#### AddSeismicDefMemberWeight

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_seismic.html#ga2299de54c2eeddab2a1ba7ba11b86052) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_seismic.html#ga2299de54c2eeddab2a1ba7ba11b86052) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#ga2299de54c2eeddab2a1ba7ba11b86052)

**Comparison:** Mapping. **Coverage:** D — production dispatch; native qualification remains pending.

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



**Current call sites:** [AddSeismicDefMemberWeight — line 1234](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1234).

#### AddSeismicDefJointWeight

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_seismic.html#gae8c289357923e66c14f2b08417055bc7) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_seismic.html#gae8c289357923e66c14f2b08417055bc7) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#gae8c289357923e66c14f2b08417055bc7)

**Comparison:** Status. **Coverage:** D — production dispatch; native qualification remains pending.

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



**Current call sites:** [AddSeismicDefJointWeight — line 1205](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1205).

#### AddSeismicDefWallArea

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_seismic.html#gad481a21e57f2ee6b953d5e1d111c34c7) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_seismic.html#gad481a21e57f2ee6b953d5e1d111c34c7) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#gad481a21e57f2ee6b953d5e1d111c34c7)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [AddSeismicDefWallArea — line 979](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L979).

#### AddResponseSpectrumLoadEx

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_seismic.html#gac24b840992cf7f15558b3c6844857f5a) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_seismic.html#gac24b840992cf7f15558b3c6844857f5a) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_seismic.html#gac24b840992cf7f15558b3c6844857f5a)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [SubmitExtendedSpectrum — line 1016](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1016).

### Load Definition: Time History

No native function declarations occur in this reviewed grouping page. Subgroups, where present, were traversed separately.

### Load Definition: Snow

No native function declarations occur in this reviewed grouping page. Subgroups, where present, were traversed separately.

### Load Definition: Reference

#### GetReferenceLoadCaseCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_ref_load.html#ga70c105eb346388eeed599db3ec1ea109) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_ref_load.html#ga70c105eb346388eeed599db3ec1ea109) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_ref_load.html#ga70c105eb346388eeed599db3ec1ea109)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

```cpp
long OSLoadUI::GetReferenceLoadCaseCount();
```

No native parameters.

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The number of reference load case. |
| `-1` | General error. |


**Current call sites:** [ReadAllLoadCases — line 3258](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3258).

#### GetReferenceLoadCaseNumbers

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_ref_load.html#gad56e67bc7dedd7e719cf97661581550c) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_ref_load.html#gad56e67bc7dedd7e719cf97661581550c) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_ref_load.html#gad56e67bc7dedd7e719cf97661581550c)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [ReadAllLoadCases — line 3266](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3266).

#### CreateNewReferenceLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_ref_load.html#ga13220ea514164923069da40dea5065fb) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_ref_load.html#ga13220ea514164923069da40dea5065fb) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_ref_load.html#ga13220ea514164923069da40dea5065fb)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [CreateNewReferenceLoad — line 2731](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2731).

#### SetReferenceLoadActive

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_def_ref_load.html#gad59a03aa194d6f31cbb49c59959dc053) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_def_ref_load.html#gad59a03aa194d6f31cbb49c59959dc053) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_def_ref_load.html#gad59a03aa194d6f31cbb49c59959dc053)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [SetLoadCaseActive — line 2758](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2758); [ActivateQueryCase — line 3336](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3336); [Activate — line 58](../../src/StaadPro.Interop/Services/Load/LoadServiceContext.cs#L58).

### Load Definition: Direct Analysis

#### AddDirectAnalysisDefinitionParameter

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_direct_analysis.html#gac8fe142a3657334769e6cbe960e40d1f) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_direct_analysis.html#gac8fe142a3657334769e6cbe960e40d1f) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_direct_analysis.html#gac8fe142a3657334769e6cbe960e40d1f)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [AddDirectAnalysisDefinitionParameter — line 1029](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1029).

#### DeleteDirectAnalysisDefinitionParameter

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_direct_analysis.html#gacfb6b7aa2b317a90aced0bb8df579237) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_direct_analysis.html#gacfb6b7aa2b317a90aced0bb8df579237) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_direct_analysis.html#gacfb6b7aa2b317a90aced0bb8df579237)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [DeleteDirectAnalysisDefinitionParameter — line 1063](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1063).

#### DeleteDirectAnalysisDefinition

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_direct_analysis.html#ga5577e10064596d7933dc30e5b68a66fd) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_direct_analysis.html#ga5577e10064596d7933dc30e5b68a66fd) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_direct_analysis.html#ga5577e10064596d7933dc30e5b68a66fd)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [DeleteDirectAnalysisDefinition — line 1069](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1069).

### Load Case Details: Load Items

No native function declarations occur in this reviewed grouping page. Subgroups, where present, were traversed separately.

### Load Case Details: Load Combination

#### GetLoadCombinationCaseCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_comb.html#ga4c6b39b1198e6e63aba6a7b56ca59dcc) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_comb.html#ga4c6b39b1198e6e63aba6a7b56ca59dcc) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga4c6b39b1198e6e63aba6a7b56ca59dcc)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

```cpp
VARIANT OSLoadUI::GetLoadCombinationCaseCount();
```

No native parameters.

**Declared return:** `VARIANT`. **Inspected CLR return:** `System.Object`.

No return-value table is present. Read the return prose in the linked reference; do not infer Boolean success or invent a payload.


**Current call sites:** [ReadCombinationCaseIds — line 3294](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3294).

#### GetLoadCombinationCaseNumbers

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_comb.html#gafe01a8e664b6722b0e341e37152cb4a9) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_comb.html#gafe01a8e664b6722b0e341e37152cb4a9) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gafe01a8e664b6722b0e341e37152cb4a9)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [ReadCombinationCaseIds — line 3297](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3297).

#### CreateNewLoadCombination

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_comb.html#ga8bb03f4733ba650a9df0a8fc16e4e523) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_comb.html#ga8bb03f4733ba650a9df0a8fc16e4e523) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga8bb03f4733ba650a9df0a8fc16e4e523)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [CreateLoadCombinations — line 1647](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1647).

#### AddLoadAndFactorToCombination

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_comb.html#ga6f75602769cefbbcce48fa0098130c2a) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_comb.html#ga6f75602769cefbbcce48fa0098130c2a) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga6f75602769cefbbcce48fa0098130c2a)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [CreateLoadCombinations — line 1649](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1649).

#### GetNoOfLoadAndFactorPairsForCombination

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_comb.html#ga3f68ee33ca96f501438b78f008da34c8) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_comb.html#ga3f68ee33ca96f501438b78f008da34c8) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga3f68ee33ca96f501438b78f008da34c8)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [GetLoadCombination — line 1531](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1531).

#### GetLoadAndFactorForCombination

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_comb.html#ga02d917d448cea428dae77061868f19eb) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_comb.html#ga02d917d448cea428dae77061868f19eb) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga02d917d448cea428dae77061868f19eb)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [GetLoadCombination — line 1537](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1537).

#### IsCombinationCase

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_comb.html#gad07c8023e20a2452b79f0ac5a394a2d2) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_comb.html#gad07c8023e20a2452b79f0ac5a394a2d2) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gad07c8023e20a2452b79f0ac5a394a2d2)

**Comparison:** Common. **Coverage:** A — managed alternative/composition; intentionally no exact dispatch.

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

**Existing managed mapping (no duplicate proposed):**

~~~csharp
IEnumerable<ILoadCase> GetAllLoadCasesOfType(LoadCaseType loadCaseType = LoadCaseType.PrimaryLoad);
~~~

**Contract disposition:** Use LoadCombination and test Id membership; existing GetAllLoadCombinationCases is also available. No new direct predicate is required.

#### AddAutoLoadCombinations

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_comb.html#ga0c6fba1b5eca1c912d70b36cbcac5763) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_comb.html#ga0c6fba1b5eca1c912d70b36cbcac5763) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#ga0c6fba1b5eca1c912d70b36cbcac5763)

**Comparison:** Status. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use installed code/category rule files to generate combination cases. CreateLoadCombinations instead accepts explicit caller-defined factors; CreateAutomaticRepeatLoads generates repeat cases, not result-combination definitions. Code and category strings must match the acquired installation, not a hard-coded engineering-code enum. AllCases is an explicit opt-in to the native empty-array all-case behavior. STAAD may advance the requested start ID; read ActualStartCaseId. GeneratedCount is null because this native API supplies no count, and IDs are never inferred as contiguous. Native 0 means success; the 2024 help lists -1 failure, while 2025/2026 list -8002/-8040/-8041. Error descriptions use wrapper.StaadVersion, never the host ApplicationVersion.

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


**Current call sites:** [CreateAutomaticCombinations — line 19](../../src/StaadPro.Interop/Services/Load/LoadAutomaticGenerationService.cs#L19).

**Implemented managed contract:**

~~~csharp
LoadOperationResult<AutomaticLoadGenerationData> CreateAutomaticCombinations(AutomaticCombinationRequest request);
~~~

**Behavior and alternatives:** Use installed code/category rule files to generate combination cases. CreateLoadCombinations instead accepts explicit caller-defined factors; CreateAutomaticRepeatLoads generates repeat cases, not result-combination definitions. Code and category strings must match the acquired installation, not a hard-coded engineering-code enum. AllCases is an explicit opt-in to the native empty-array all-case behavior. STAAD may advance the requested start ID; read ActualStartCaseId. GeneratedCount is null because this native API supplies no count, and IDs are never inferred as contiguous. Native 0 means success; the 2024 help lists -1 failure, while 2025/2026 list -8002/-8040/-8041. Error descriptions use wrapper.StaadVersion, never the host ApplicationVersion. See [usage and qualification limits](../Load-Expansion.md#createautomaticcombinations).

#### AddAutoCombinationRepeat

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_comb.html#gab0a905a41051c3ae5c0d1d6e0586f544) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_comb.html#gab0a905a41051c3ae5c0d1d6e0586f544) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_comb.html#gab0a905a41051c3ae5c0d1d6e0586f544)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use code/category rules to generate repeat load cases whose loads participate in a subsequent analysis. CreateAutomaticCombinations creates combination definitions instead; AddRepeatLoadsTo assigns explicit factors to one existing case. Optional reference and notional participation is explicit; null NotionalOptions disables all notional flags. The returned ActualStartCaseId and GeneratedCount come from native outputs, not a guessed contiguous ID range. Native 0 means success; -1 means failure.

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


**Current call sites:** [CreateAutomaticRepeatLoads — line 51](../../src/StaadPro.Interop/Services/Load/LoadAutomaticGenerationService.cs#L51).

**Implemented managed contract:**

~~~csharp
LoadOperationResult<AutomaticLoadGenerationData> CreateAutomaticRepeatLoads(AutomaticRepeatLoadRequest request);
~~~

**Behavior and alternatives:** Use code/category rules to generate repeat load cases whose loads participate in a subsequent analysis. CreateAutomaticCombinations creates combination definitions instead; AddRepeatLoadsTo assigns explicit factors to one existing case. Optional reference and notional participation is explicit; null NotionalOptions disables all notional flags. The returned ActualStartCaseId and GeneratedCount come from native outputs, not a guessed contiguous ID range. Native 0 means success; -1 means failure. See [usage and qualification limits](../Load-Expansion.md#createautomaticrepeatloads).

### Load Case Details: Load Case Operation

#### GetPrimaryLoadCaseNumbers

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga3e518083ebff1f7eda2bad62d3696c0c) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga3e518083ebff1f7eda2bad62d3696c0c) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga3e518083ebff1f7eda2bad62d3696c0c)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [ReadAllLoadCases — line 3265](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3265).

#### CreateNewPrimaryLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga65f4045dbd028cd4233c11f9797e453e) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga65f4045dbd028cd4233c11f9797e453e) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga65f4045dbd028cd4233c11f9797e453e)

**Comparison:** Common. **Coverage:** A — managed alternative/composition; intentionally no exact dispatch.

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

**Existing managed mapping (no duplicate proposed):**

~~~csharp
int CreateNewPrimaryLoad(string lcTitle, LoadType loadType);
~~~

**Contract disposition:** Existing Ex/Ex2 routing covers creation; existing signatures and return behavior are retained.

#### GetActiveLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga0333e9a3b2b913fe2bd20a70807dd917) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga0333e9a3b2b913fe2bd20a70807dd917) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga0333e9a3b2b913fe2bd20a70807dd917)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Returns the positive native number or NativeFailure for -1. Primary/reference numbers can overlap; this result is not a LoadCase identity and performs no activation.

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

**Current call sites:** [CheckActive — line 49](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L49); [GetActiveLoadCaseNumber — line 76](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L76); [ActiveId — line 71](../../src/StaadPro.Interop/Services/Load/LoadServiceContext.cs#L71).

#### GetLoadCaseTitle

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#gab2539a2b6546b0431bf1f871f5251365) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#gab2539a2b6546b0431bf1f871f5251365) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gab2539a2b6546b0431bf1f871f5251365)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [GetLoadCombination — line 1534](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1534); [GetLoadCaseTitle — line 2746](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2746); [ReadLoadCaseMetadata — line 3282](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3282); [ReadCombinationCaseMetadata — line 3310](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3310).

#### SetLoadType

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga80c30f8628faf5a95ae2058d3dc7da91) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga80c30f8628faf5a95ae2058d3dc7da91) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga80c30f8628faf5a95ae2058d3dc7da91)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to reclassify existing cases as Dead, Live, Wind and similar engineering categories. This LoadType is different from LoadCaseType (primary/reference/combination family), LoadCommandKind (command table) and LoadItemKind (original item code). This changes native cases directly; setting a detached LoadCase.Type alone does not invoke this operation. Native 0 means success.

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


**Current call sites:** [SetLoadType — line 32](../../src/StaadPro.Interop/Services/Load/LoadCaseManagementService.cs#L32).

**Implemented managed contract:**

~~~csharp
LoadOperationResult SetLoadType(IEnumerable<int> loadCaseIds, LoadType loadType);
~~~

**Behavior and alternatives:** Use to reclassify existing cases as Dead, Live, Wind and similar engineering categories. This LoadType is different from LoadCaseType (primary/reference/combination family), LoadCommandKind (command table) and LoadItemKind (original item code). This changes native cases directly; setting a detached LoadCase.Type alone does not invoke this operation. Native 0 means success. See [usage and qualification limits](../Load-Expansion.md#setloadtype).

#### CreateNewPrimaryLoadEx

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#gafe08d35e098340b64412fc7af7cddef5) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#gafe08d35e098340b64412fc7af7cddef5) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gafe08d35e098340b64412fc7af7cddef5)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [CreateNewPrimaryLoad — line 2699](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2699).

#### GetLoadType

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#gae089cbb4d51b68b2ce6a39f82cf2e9cd) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#gae089cbb4d51b68b2ce6a39f82cf2e9cd) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gae089cbb4d51b68b2ce6a39f82cf2e9cd)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [ReadLoadCaseMetadata — line 3285](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3285).

#### CreateLoadList

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga7179492fe5d8a09023270b31fb703f00) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga7179492fe5d8a09023270b31fb703f00) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga7179492fe5d8a09023270b31fb703f00)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [CreateLoadList — line 3209](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3209).

#### GetLoadListCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga5197fada7df8eb6345eef3983c9daca1) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga5197fada7df8eb6345eef3983c9daca1) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga5197fada7df8eb6345eef3983c9daca1)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Zero is a valid count; -1 is NativeFailure. This is not an envelope count or a count of cases in an individual list.

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

**Current call sites:** [GetLoadListCount — line 82](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L82); [CheckList — line 85](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L85); [GetLoadListCaseCount — line 100](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L100); [GetLoadListCases — line 129](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L129).

#### GetLoadCountInLoadList

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#gabd66e32af1113e62601c497524654d9b) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#gabd66e32af1113e62601c497524654d9b) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gabd66e32af1113e62601c497524654d9b)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Validates the one-based list index against GetLoadListCount and rechecks that total after reading. Out-of-range lists are NotFound. The native count function has no documented negative error code; negative counts are rejected as malformed.

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

**Current call sites:** [ListCaseCount — line 90](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L90).

#### GetLoadsInLoadList

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga43ded5daa43065693c67f76dad7f022c) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga43ded5daa43065693c67f76dad7f022c) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga43ded5daa43065693c67f76dad7f022c)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Uses an Int32 SAFEARRAY output passed internally by reference following installed CLR metadata, despite const wording in the C++ prototype. Preserves order/duplicates; does not infer case families. Rechecks list and member counts; detected changes return StateChanged. Native false is NativeFailure. Zero cases returns a read-only empty list without fetching.

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

**Current call sites:** [GetLoadListCases — line 120](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L120).

#### DeleteLoadList

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga42fbeecd03729b12aac29dd0b2c59bd0) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga42fbeecd03729b12aac29dd0b2c59bd0) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga42fbeecd03729b12aac29dd0b2c59bd0)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to remove one LOAD LIST record created through CreateLoadList. The argument is a one-based list record index from GetLoadListCount, not a case number or envelope ID. Cases and envelopes remain defined. Use DeleteLoadCases or DeleteEnvelope for those scopes. Native 0 means success.

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


**Current call sites:** [DeleteLoadList — line 37](../../src/StaadPro.Interop/Services/Load/LoadCaseManagementService.cs#L37).

**Implemented managed contract:**

~~~csharp
LoadOperationResult DeleteLoadList(int loadListIndex);
~~~

**Behavior and alternatives:** Use to remove one LOAD LIST record created through CreateLoadList. The argument is a one-based list record index from GetLoadListCount, not a case number or envelope ID. Cases and envelopes remain defined. Use DeleteLoadCases or DeleteEnvelope for those scopes. Native 0 means success. See [usage and qualification limits](../Load-Expansion.md#deleteloadlist).

#### SetASDLoadAttribute

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga7946c813af9dd0c7427730474355b9c6) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga7946c813af9dd0c7427730474355b9c6) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga7946c813af9dd0c7427730474355b9c6)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use for an ASD case attribute with an explicit strength-of-forces choice and optional one-third allowable-stress increase. SetLimitStateAttribute instead sets LSD; SetLoadType changes engineering classification rather than design attributes. This does not select a model design code. GetLoadAttribute cannot reconstruct these settings: Bentley documents only a status observation for that getter. Native 0 means success.

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


**Current call sites:** [SetAllowableStressAttribute — line 42](../../src/StaadPro.Interop/Services/Load/LoadCaseManagementService.cs#L42).

**Implemented managed contract:**

~~~csharp
LoadOperationResult SetAllowableStressAttribute(int loadCaseId, LoadStrengthType strengthType, bool increaseAllowableStress);
~~~

**Behavior and alternatives:** Use for an ASD case attribute with an explicit strength-of-forces choice and optional one-third allowable-stress increase. SetLimitStateAttribute instead sets LSD; SetLoadType changes engineering classification rather than design attributes. This does not select a model design code. GetLoadAttribute cannot reconstruct these settings: Bentley documents only a status observation for that getter. Native 0 means success. See [usage and qualification limits](../Load-Expansion.md#setallowablestressattribute).

#### SetLSDLoadAttribute

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#gad4c0b38a84da05308d4844d122df5135) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#gad4c0b38a84da05308d4844d122df5135) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gad4c0b38a84da05308d4844d122df5135)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to set the LSD case attribute. SetAllowableStressAttribute instead exposes ASD strength and allowable-stress choices; SetLoadType changes engineering classification. This does not select a design code or run a design. GetLoadAttribute cannot reconstruct LSD/ASD settings from its documented status-only return. Native 0 means success.

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


**Current call sites:** [SetLimitStateAttribute — line 47](../../src/StaadPro.Interop/Services/Load/LoadCaseManagementService.cs#L47).

**Implemented managed contract:**

~~~csharp
LoadOperationResult SetLimitStateAttribute(int loadCaseId);
~~~

**Behavior and alternatives:** Use to set the LSD case attribute. SetAllowableStressAttribute instead exposes ASD strength and allowable-stress choices; SetLoadType changes engineering classification. This does not select a design code or run a design. GetLoadAttribute cannot reconstruct LSD/ASD settings from its documented status-only return. Native 0 means success. See [usage and qualification limits](../Load-Expansion.md#setlimitstateattribute).

#### RemoveAttribute

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga9110cacabbaba9bc87e54fa1774885f3) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga9110cacabbaba9bc87e54fa1774885f3) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga9110cacabbaba9bc87e54fa1774885f3)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to remove a case design attribute while retaining its loads and definition. ClearPrimaryLoadCase clears items, and DeleteLoadCases removes case definitions; neither is a substitute for this attribute operation. Native 0 means success.

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


**Current call sites:** [RemoveLoadAttribute — line 52](../../src/StaadPro.Interop/Services/Load/LoadCaseManagementService.cs#L52).

**Implemented managed contract:**

~~~csharp
LoadOperationResult RemoveLoadAttribute(int loadCaseId);
~~~

**Behavior and alternatives:** Use to remove a case design attribute while retaining its loads and definition. ClearPrimaryLoadCase clears items, and DeleteLoadCases removes case definitions; neither is a substitute for this attribute operation. Native 0 means success. See [usage and qualification limits](../Load-Expansion.md#removeloadattribute).

#### GetAttribute

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#gace56ab4ec380cbf3061131ddd5e2d6a1) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#gace56ab4ec380cbf3061131ddd5e2d6a1) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gace56ab4ec380cbf3061131ddd5e2d6a1)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented status observation. Use only to observe the documented native GetAttribute status. A successful Data.NativeValue is 0; it does not identify ASD, LSD, LoadStrengthType, stress-increase settings, or the absence of an attribute. Unlike the attribute setters, this API cannot return their settings because Bentley exposes no documented output payload. -1 produces NativeFailure; any other integral value is rejected as an undocumented schema rather than decoded speculatively.

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


**Current call sites:** [GetLoadAttribute — line 59](../../src/StaadPro.Interop/Services/Load/LoadCaseManagementService.cs#L59).

**Implemented managed contract:**

~~~csharp
LoadQueryResult<LoadAttributeObservation> GetLoadAttribute(int loadCaseId);
~~~

**Behavior and alternatives:** Use only to observe the documented native GetAttribute status. A successful Data.NativeValue is 0; it does not identify ASD, LSD, LoadStrengthType, stress-increase settings, or the absence of an attribute. Unlike the attribute setters, this API cannot return their settings because Bentley exposes no documented output payload. -1 produces NativeFailure; any other integral value is rejected as an undocumented schema rather than decoded speculatively. See [usage and qualification limits](../Load-Expansion.md#getloadattribute).

#### GetLoadTypeCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#gad2e249f1e4ba8d8d76f6a7698da94a2a) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#gad2e249f1e4ba8d8d76f6a7698da94a2a) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gad2e249f1e4ba8d8d76f6a7698da94a2a)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Command kind is distinct from engineering LoadType and LoadItemKind. Native zero is documented as case not found and is returned as EmptyOrUnavailable, with no Data, rather than a proven empty inventory.

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

**Current call sites:** [CommandCount — line 139](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L139).

#### GetListSizeForLoadType

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga8e2b9d3639488a9442eb4c4d1ec21e87) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga8e2b9d3639488a9442eb4c4d1ec21e87) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga8e2b9d3639488a9442eb4c4d1ec21e87)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Validates the zero-based command index against GetLoadTypeCount and rechecks that count afterwards. A zero command count is EmptyOrUnavailable; an index beyond a positive count is NotFound. A valid entity count can be zero. This returns a count, not the assignment-ID list.

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

**Current call sites:** [GetAssignedEntityCount — line 160](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L160); [GetAssignedEntityIds — line 281](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L281); [GetAssignedEntityIds — line 292](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L292).

#### GetAssignmentListForLoadType

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga35276014d19da32994c0adf4ab3e53eb) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga35276014d19da32994c0adf4ab3e53eb) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga35276014d19da32994c0adf4ab3e53eb)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to preserve the entity assignment list of one original command, including native order and duplicates. GetAssignedEntityCount returns only its size. GetNodalLoads/GetMemberUniformlyDistributedLoads read loads for an entity, while grouped population methods reorganize loads across entities and can lose original command identity. The zero-based commandIndex is within commandKind, not the loadItemIndex used by GetLoadItemKind. Native zero is an error/unavailable observation, not a proven empty assignment. Sizing, returned size and observed command/entity counts must agree.

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


**Current call sites:** [GetAssignedEntityIds — line 285](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L285).

**Implemented managed contract:**

~~~csharp
LoadQueryResult<IEnumerable<int>> GetAssignedEntityIds(int primaryLoadCaseId, LoadCommandKind commandKind, int commandIndex, int maximumResults = 100000);
~~~

**Behavior and alternatives:** Use to preserve the entity assignment list of one original command, including native order and duplicates. GetAssignedEntityCount returns only its size. GetNodalLoads/GetMemberUniformlyDistributedLoads read loads for an entity, while grouped population methods reorganize loads across entities and can lose original command identity. The zero-based commandIndex is within commandKind, not the loadItemIndex used by GetLoadItemKind. Native zero is an error/unavailable observation, not a proven empty assignment. Sizing, returned size and observed command/entity counts must agree. See [usage and qualification limits](../Load-Expansion.md#getassignedentityids).

#### DeletePrimaryLoadCases

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga969b11631fb0bc3fcef22c4cc54b9017) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga969b11631fb0bc3fcef22c4cc54b9017) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga969b11631fb0bc3fcef22c4cc54b9017)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to remove primary or reference case definitions. ClearPrimaryLoadCases/ClearReferenceLoadCases clear their contents while retaining the cases. Combination and RepeatLoad enum choices are rejected; repeat commands live inside primary cases and require primary-family selection. Primary requests dispatch DeletePrimaryLoadCases with the reference flag false; reference requests use DeleteReferenceLoadCases. Native 1 means success. Dependency failures are reported without deletion/recreation fallback.

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


**Current call sites:** [DeleteLoadCases — line 71](../../src/StaadPro.Interop/Services/Load/LoadCaseManagementService.cs#L71).

**Implemented managed contract:**

~~~csharp
LoadOperationResult DeleteLoadCases(LoadCaseType caseType, IEnumerable<int> loadCaseIds);
~~~

**Behavior and alternatives:** Use to remove primary or reference case definitions. ClearPrimaryLoadCases/ClearReferenceLoadCases clear their contents while retaining the cases. Combination and RepeatLoad enum choices are rejected; repeat commands live inside primary cases and require primary-family selection. Primary requests dispatch DeletePrimaryLoadCases with the reference flag false; reference requests use DeleteReferenceLoadCases. Native 1 means success. Dependency failures are reported without deletion/recreation fallback. See [usage and qualification limits](../Load-Expansion.md#deleteloadcases).

#### DeleteReferenceLoadCases

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga68d5bc5d7bbf772281bdec969832dd00) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga68d5bc5d7bbf772281bdec969832dd00) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga68d5bc5d7bbf772281bdec969832dd00)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to remove primary or reference case definitions. ClearPrimaryLoadCases/ClearReferenceLoadCases clear their contents while retaining the cases. Combination and RepeatLoad enum choices are rejected; repeat commands live inside primary cases and require primary-family selection. Primary requests dispatch DeletePrimaryLoadCases with the reference flag false; reference requests use DeleteReferenceLoadCases. Native 1 means success. Dependency failures are reported without deletion/recreation fallback.

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


**Current call sites:** [DeleteLoadCases — line 72](../../src/StaadPro.Interop/Services/Load/LoadCaseManagementService.cs#L72).

**Implemented managed contract:**

~~~csharp
LoadOperationResult DeleteLoadCases(LoadCaseType caseType, IEnumerable<int> loadCaseIds);
~~~

**Behavior and alternatives:** Use to remove primary or reference case definitions. ClearPrimaryLoadCases/ClearReferenceLoadCases clear their contents while retaining the cases. Combination and RepeatLoad enum choices are rejected; repeat commands live inside primary cases and require primary-family selection. Primary requests dispatch DeletePrimaryLoadCases with the reference flag false; reference requests use DeleteReferenceLoadCases. Native 1 means success. Dependency failures are reported without deletion/recreation fallback. See [usage and qualification limits](../Load-Expansion.md#deleteloadcases).

#### ClearPrimaryLoadCase

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#gac60cdde591385334dbbfbc0e76da682c) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#gac60cdde591385334dbbfbc0e76da682c) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#gac60cdde591385334dbbfbc0e76da682c)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [ClearPrimaryLoadCases — line 2640](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2640); [ClearPrimaryLoadCases — line 2663](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2663).

#### ClearReferenceLoadCase

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga5651ca43e11e3932abde242666b7f6ad) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga5651ca43e11e3932abde242666b7f6ad) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga5651ca43e11e3932abde242666b7f6ad)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [ClearReferenceLoadCases — line 2686](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2686).

#### CreateNewPrimaryLoadEx2

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga9246e3b7e18d22d556a9bbbd73572c6a) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga9246e3b7e18d22d556a9bbbd73572c6a) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga9246e3b7e18d22d556a9bbbd73572c6a)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Priority verification:** The inspected Ex2 declaration has by-reference title/type slots; the current call passes both by value. See the primary report; no implementation was changed.

**Current call sites:** [CreateNewPrimaryLoad — line 2708](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2708).

#### GetLoadItemsCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga3597b11d8f108005e9be64e4761887e2) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga3597b11d8f108005e9be64e4761887e2) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga3597b11d8f108005e9be64e4761887e2)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Zero is valid and -1 is NativeFailure. Passes the case number directly without activation or family inference. Counts native items rather than per-entity duplicates or grouped managed definitions.

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

**Current call sites:** [Item — line 23](../../src/StaadPro.Interop/Services/Load/LoadAssignmentQueryService.cs#L23); [Item — line 31](../../src/StaadPro.Interop/Services/Load/LoadAssignmentQueryService.cs#L31); [GetLoadItemCount — line 169](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L169).

#### GetLoadItemType

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_operation.html#ga90d695c8921e1e5eeb570ba37e1744fd) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_operation.html#ga90d695c8921e1e5eeb570ba37e1744fd) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_operation.html#ga90d695c8921e1e5eeb570ba37e1744fd)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Native zero is NotFound. Unknown positive codes are retained in the enum value; use Enum.IsDefined to test recognition. Code 3312 remains explicitly ambiguous between region nodal and partial plate pressure. Negative codes are malformed. No activation or family inference occurs.

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

**Current call sites:** [Item — line 26](../../src/StaadPro.Interop/Services/Load/LoadAssignmentQueryService.cs#L26); [Item — line 33](../../src/StaadPro.Interop/Services/Load/LoadAssignmentQueryService.cs#L33); [GetLoadItemKind — line 177](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L177).

### Load Items: SelfWeight

#### AddSelfWeightInXYZ

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_self_weight.html#gac54227b07c765ba1cb94fb1d1f4271af) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_self_weight.html#gac54227b07c765ba1cb94fb1d1f4271af) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_self_weight.html#gac54227b07c765ba1cb94fb1d1f4271af)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use for native whole-model self-weight covering beams, plates and solids. AddSelfweightInXYZTo instead assigns to an explicit geometry list; these scopes are different even when a caller currently lists every entity. The factor is dimensionless, with its sign preserved, and the global direction uses SelfWeightDirection. Native Boolean TRUE means success.

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


**Current call sites:** [AddSelfWeight — line 20](../../src/StaadPro.Interop/Services/Load/LoadAssignmentService.cs#L20).

**Implemented managed contract:**

~~~csharp
LoadOperationResult AddSelfWeight(ILoadCase targetCase, SelfWeightDirection direction, double factor);
~~~

**Behavior and alternatives:** Use for native whole-model self-weight covering beams, plates and solids. AddSelfweightInXYZTo instead assigns to an explicit geometry list; these scopes are different even when a caller currently lists every entity. The factor is dimensionless, with its sign preserved, and the global direction uses SelfWeightDirection. Native Boolean TRUE means success. See [usage and qualification limits](../Load-Expansion.md#addselfweight).

#### AddSelfWeightInXYZToGeometry

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_self_weight.html#gac7bd07b68033a341b2ccd05d66cb1c90) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_self_weight.html#gac7bd07b68033a341b2ccd05d66cb1c90) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_self_weight.html#gac7bd07b68033a341b2ccd05d66cb1c90)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [AddSelfweightInXYZTo — line 3139](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3139).

### Load Items: Nodal

#### AddNodalLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_nodal.html#gafd43c265f2925cc1f0a3c0c654c981c1) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_nodal.html#gafd43c265f2925cc1f0a3c0c654c981c1) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#gafd43c265f2925cc1f0a3c0c654c981c1)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [AddNodalLoad — line 2914](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2914).

#### AddSupportDisplacement

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_nodal.html#gadbf5b06ad7b6120475c99d605c785118) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_nodal.html#gadbf5b06ad7b6120475c99d605c785118) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#gadbf5b06ad7b6120475c99d605c785118)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [AddSupportSettlement — line 2831](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2831).

#### GetNodalLoadCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_nodal.html#gafe7311f8ee6677a22d22fde168a7c0dc) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_nodal.html#gafe7311f8ee6677a22d22fde168a7c0dc) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#gafe7311f8ee6677a22d22fde168a7c0dc)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [GetNodalLoads — line 1697](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1697).

#### GetNodalLoads

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_nodal.html#ga6ac19e170ef258bfd97ee9345850ebf1) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_nodal.html#ga6ac19e170ef258bfd97ee9345850ebf1) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#ga6ac19e170ef258bfd97ee9345850ebf1)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [GetNodalLoads — line 1703](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1703).

#### GetNodalLoadInfo

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_nodal.html#ga53757ae693de6e733c1d842becdfccd9) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_nodal.html#ga53757ae693de6e733c1d842becdfccd9) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_nodal.html#ga53757ae693de6e733c1d842becdfccd9)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to inspect the six components of one original ordinary nodal command. GetNodalLoads reads loads assigned to a particular node; GetNodalLoadsGroupedByLoadCase reorganizes entity data. This packet contains no node list: use GetAssignedEntityIds with the appropriate command index for assignments. Only native Nodal items are accepted; inclined loads and support displacement are not interchangeable. Pass the original zero-based GetLoadItemKind index unchanged. The nodal help omits an explicit base; confirm this command-index convention against the acquired build before production use. Native Boolean TRUE means success.

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


**Current call sites:** [GetNodalLoadCommand — line 41](../../src/StaadPro.Interop/Services/Load/LoadAssignmentQueryService.cs#L41).

**Implemented managed contract:**

~~~csharp
LoadQueryResult<NodalLoadCommandInfo> GetNodalLoadCommand(ILoadCase targetCase, int loadItemIndex);
~~~

**Behavior and alternatives:** Use to inspect the six components of one original ordinary nodal command. GetNodalLoads reads loads assigned to a particular node; GetNodalLoadsGroupedByLoadCase reorganizes entity data. This packet contains no node list: use GetAssignedEntityIds with the appropriate command index for assignments. Only native Nodal items are accepted; inclined loads and support displacement are not interchangeable. Pass the original zero-based GetLoadItemKind index unchanged. The nodal help omits an explicit base; confirm this command-index convention against the acquired build before production use. Native Boolean TRUE means success. See [usage and qualification limits](../Load-Expansion.md#getnodalloadcommand).

### Load Items: Member

#### AddMemberUniformForce

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga2c1823f8c9b8348eff1b2efb08011201) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga2c1823f8c9b8348eff1b2efb08011201) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga2c1823f8c9b8348eff1b2efb08011201)

**Comparison:** Common. **Coverage:** E — production dispatch; native qualification remains pending.

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


**Current call sites:** [ApplyTo — line 84](../../src/StaadPro.Interop/Entities/MemberUniformlyDistributedLoad.cs#L84).

#### AddMemberUniformMoment

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga3da137976a73640b541a66c260693e48) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga3da137976a73640b541a66c260693e48) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga3da137976a73640b541a66c260693e48)

**Comparison:** Common. **Coverage:** E — production dispatch; native qualification remains pending.

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


**Current call sites:** [ApplyTo — line 75](../../src/StaadPro.Interop/Entities/MemberUniformMoment.cs#L75).

#### AddMemberConcForce

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#gaa6fd59d87e1e940b83400684a9a21e63) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#gaa6fd59d87e1e940b83400684a9a21e63) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaa6fd59d87e1e940b83400684a9a21e63)

**Comparison:** Common. **Coverage:** E — production dispatch; native qualification remains pending.

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


**Current call sites:** [ApplyTo — line 76](../../src/StaadPro.Interop/Entities/MemberConcentratedLoad.cs#L76).

#### AddMemberConcMoment

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga80f43e61a87b337b821f317ab5b712cc) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga80f43e61a87b337b821f317ab5b712cc) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga80f43e61a87b337b821f317ab5b712cc)

**Comparison:** Common. **Coverage:** E — production dispatch; native qualification remains pending.

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


**Current call sites:** [ApplyTo — line 67](../../src/StaadPro.Interop/Entities/MemberConcentratedMoment.cs#L67).

#### AddMemberLinearVari

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#gae3ca9258f043cd379cfd0255ee55eb7b) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#gae3ca9258f043cd379cfd0255ee55eb7b) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gae3ca9258f043cd379cfd0255ee55eb7b)

**Comparison:** Common. **Coverage:** E — production dispatch; native qualification remains pending.

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


**Current call sites:** [ApplyTo — line 68](../../src/StaadPro.Interop/Entities/MemberLinearVaryingLoad.cs#L68).

#### AddMemberTrapezoidal

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga8e3a9cf9619680e33c05618e7de30d52) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga8e3a9cf9619680e33c05618e7de30d52) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga8e3a9cf9619680e33c05618e7de30d52)

**Comparison:** Common. **Coverage:** E — production dispatch; native qualification remains pending.

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


**Current call sites:** [ApplyTo — line 73](../../src/StaadPro.Interop/Entities/MemberTrapezoidalLoad.cs#L73).

#### AddMemberAreaLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga3ead37c8f7e53bd37687be53194dccc4) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga3ead37c8f7e53bd37687be53194dccc4) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga3ead37c8f7e53bd37687be53194dccc4)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to assign the native member AREA load parameter to selected beam IDs. AddFloorLoad instead searches enclosed floor panels and distributes floor pressure; AddMemberLoad accepts supported line-load templates. These operations can produce different distributions on the same geometry. The signed area-load value uses current native units. Native 0 means success.

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


**Current call sites:** [AddMemberAreaLoad — line 25](../../src/StaadPro.Interop/Services/Load/LoadAssignmentService.cs#L25).

**Implemented managed contract:**

~~~csharp
LoadOperationResult AddMemberAreaLoad(ILoadCase targetCase, IEnumerable<int> memberIds, double areaLoad);
~~~

**Behavior and alternatives:** Use to assign the native member AREA load parameter to selected beam IDs. AddFloorLoad instead searches enclosed floor panels and distributes floor pressure; AddMemberLoad accepts supported line-load templates. These operations can produce different distributions on the same geometry. The signed area-load value uses current native units. Native 0 means success. See [usage and qualification limits](../Load-Expansion.md#addmemberareaload).

#### AddMemberFixedEnd

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#gaaf4468c2960776a1a18df2d7df79340b) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#gaaf4468c2960776a1a18df2d7df79340b) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaaf4468c2960776a1a18df2d7df79340b)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to supply six force/moment components at each member end as a fixed-end load definition. This is different from assigning concentrated member forces at a distance, support settlements, or reading Output reactions. StartForces and EndForces follow Fx/Fy/Fz/Mx/My/Mz order; signs and current force/moment units are preserved without support-reaction sign reversal. Native 0 means success.

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


**Current call sites:** [AddMemberFixedEndLoad — line 32](../../src/StaadPro.Interop/Services/Load/LoadAssignmentService.cs#L32).

**Implemented managed contract:**

~~~csharp
LoadOperationResult AddMemberFixedEndLoad(ILoadCase targetCase, IEnumerable<int> memberIds, FixedEndLoadDefinition definition);
~~~

**Behavior and alternatives:** Use to supply six force/moment components at each member end as a fixed-end load definition. This is different from assigning concentrated member forces at a distance, support settlements, or reading Output reactions. StartForces and EndForces follow Fx/Fy/Fz/Mx/My/Mz order; signs and current force/moment units are preserved without support-reaction sign reversal. Native 0 means success. See [usage and qualification limits](../Load-Expansion.md#addmemberfixedendload).

#### GetUDLLoadCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga91881f1b488a97a80c79f05d19c9acb1) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga91881f1b488a97a80c79f05d19c9acb1) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga91881f1b488a97a80c79f05d19c9acb1)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [GetMemberUniformlyDistributedLoads — line 1755](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1755).

#### GetUDLLoads

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga37c0da9a1d06ec1a63b641c3023af35c) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga37c0da9a1d06ec1a63b641c3023af35c) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga37c0da9a1d06ec1a63b641c3023af35c)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [GetMemberUniformlyDistributedLoads — line 1761](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1761).

#### GetUNIMomentCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga6aeeb7f578026b4ef92ad0f89212b2a4) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga6aeeb7f578026b4ef92ad0f89212b2a4) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga6aeeb7f578026b4ef92ad0f89212b2a4)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [GetMemberUniformMoments — line 2057](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2057).

#### GetUNIMoments

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga6a32b9122fb5a9c15b664aeeb0961862) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga6a32b9122fb5a9c15b664aeeb0961862) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga6a32b9122fb5a9c15b664aeeb0961862)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [GetMemberUniformMoments — line 2063](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2063).

#### GetTrapLoadCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga69350aaa0a2878dbbf9df1054f5f082e) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga69350aaa0a2878dbbf9df1054f5f082e) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga69350aaa0a2878dbbf9df1054f5f082e)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [GetMemberTrapezoidalLoads — line 1996](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1996).

#### GetTrapLoads

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#gad2ea85860050c89c48c74460358a65ca) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#gad2ea85860050c89c48c74460358a65ca) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gad2ea85860050c89c48c74460358a65ca)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [GetMemberTrapezoidalLoads — line 2002](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2002).

#### GetLinearVaryingLoadCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga7d655dfad4f7f4b445f87b8888ab212c) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga7d655dfad4f7f4b445f87b8888ab212c) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga7d655dfad4f7f4b445f87b8888ab212c)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [GetMemberLinearVaryingLoads — line 1936](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1936).

#### GetLinearVaryingLoads

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga1fad2339bc39c95b60b75d110f0cd17e) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga1fad2339bc39c95b60b75d110f0cd17e) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga1fad2339bc39c95b60b75d110f0cd17e)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [GetMemberLinearVaryingLoads — line 1943](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1943).

#### GetConcForceCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#gab1a51f83aa40e6178ba62688607c7877) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#gab1a51f83aa40e6178ba62688607c7877) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gab1a51f83aa40e6178ba62688607c7877)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [GetMemberConcentratedLoads — line 1814](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1814).

#### GetConcForces

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga76ee32fe8074198ba6e64f446fea4aa0) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga76ee32fe8074198ba6e64f446fea4aa0) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga76ee32fe8074198ba6e64f446fea4aa0)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [GetMemberConcentratedLoads — line 1820](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1820).

#### GetConcMomentCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#gaa25fc3980a9d39f787f0af3c16a661b6) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#gaa25fc3980a9d39f787f0af3c16a661b6) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaa25fc3980a9d39f787f0af3c16a661b6)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [GetMemberConcentratedMoments — line 1877](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1877).

#### GetConcMoments

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#ga35e3e090648f7acf2f1b6dd40bf07ba0) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#ga35e3e090648f7acf2f1b6dd40bf07ba0) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#ga35e3e090648f7acf2f1b6dd40bf07ba0)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [GetMemberConcentratedMoments — line 1883](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1883).

#### GetMemberLoadInfo

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_mem.html#gaa1df58fb5514e4c8814edb8e7987facd) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_mem.html#gaa1df58fb5514e4c8814edb8e7987facd) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_mem.html#gaa1df58fb5514e4c8814edb8e7987facd)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to retain one original member-command packet and its native item kind. GetMemberUniformlyDistributedLoads and the concentrated/linear/trapezoidal readers instead select per-member load definitions. Force parameter dimensions depend on Kind; do not interpret moment slots as forces. Distances retain their native meanings and length units. Only the six ordinary uniform-force, uniform-moment, concentrated-force, concentrated-moment, linear and trapezoidal kinds documented for this reader are accepted. Physical-member and fixed-end packets are not inferred. The item index is zero-based. Native Boolean TRUE means success.

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


**Current call sites:** [GetMemberLoadCommand — line 51](../../src/StaadPro.Interop/Services/Load/LoadAssignmentQueryService.cs#L51).

**Implemented managed contract:**

~~~csharp
LoadQueryResult<MemberLoadCommandInfo> GetMemberLoadCommand(ILoadCase targetCase, int loadItemIndex);
~~~

**Behavior and alternatives:** Use to retain one original member-command packet and its native item kind. GetMemberUniformlyDistributedLoads and the concentrated/linear/trapezoidal readers instead select per-member load definitions. Force parameter dimensions depend on Kind; do not interpret moment slots as forces. Distances retain their native meanings and length units. Only the six ordinary uniform-force, uniform-moment, concentrated-force, concentrated-moment, linear and trapezoidal kinds documented for this reader are accepted. Physical-member and fixed-end packets are not inferred. The item index is zero-based. Native Boolean TRUE means success. See [usage and qualification limits](../Load-Expansion.md#getmemberloadcommand).

### Load Items: Physical Member

No native function declarations occur in this reviewed grouping page. Subgroups, where present, were traversed separately.

### Load Items: Element

#### AddElementPressure

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_ele.html#gaeec2ec071bdd84d6139282dc089b83a2) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_ele.html#gaeec2ec071bdd84d6139282dc089b83a2) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gaeec2ec071bdd84d6139282dc089b83a2)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Priority verification:** Current call supplies three arguments; the inspected declaration requires seven. See the primary report; no implementation was changed.

**Current call sites:** [AddPlateUniformPressure — line 2970](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2970).

#### AddElementHydrostaticPressure

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_ele.html#gac5b8a98ce4cceb2bea7775adb2b62ba2) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_ele.html#gac5b8a98ce4cceb2bea7775adb2b62ba2) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gac5b8a98ce4cceb2bea7775adb2b62ba2)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use for pressures interpolated along a global coordinate axis. AddTrapezoidalPressure varies along plate-local X/Y or by joint; AddPlateUniformPressure assigns a uniform value. Pressure direction and interpolation axis are independent: the latter maps GlobalAxis X/Y/Z to native 1/2/3. Signed minimum/maximum pressure parameters are passed unchanged, not sorted or recalculated from fluid density. Native Boolean TRUE/1 means success.

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


**Current call sites:** [AddHydrostaticPressure — line 39](../../src/StaadPro.Interop/Services/Load/LoadAssignmentService.cs#L39).

**Implemented managed contract:**

~~~csharp
LoadOperationResult AddHydrostaticPressure(ILoadCase targetCase, IEnumerable<int> plateIds, HydrostaticPressureDefinition definition);
~~~

**Behavior and alternatives:** Use for pressures interpolated along a global coordinate axis. AddTrapezoidalPressure varies along plate-local X/Y or by joint; AddPlateUniformPressure assigns a uniform value. Pressure direction and interpolation axis are independent: the latter maps GlobalAxis X/Y/Z to native 1/2/3. Signed minimum/maximum pressure parameters are passed unchanged, not sorted or recalculated from fluid density. Native Boolean TRUE/1 means success. See [usage and qualification limits](../Load-Expansion.md#addhydrostaticpressure).

#### AddElementTrapPressureEx

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_ele.html#ga829edeccde827aef75e1d8fb8e297cdf) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_ele.html#ga829edeccde827aef75e1d8fb8e297cdf) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga829edeccde827aef75e1d8fb8e297cdf)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use for two-value variation along plate-local X/Y or four joint-ordered pressures. AddHydrostaticPressure interpolates along a global axis; AddPlateUniformPressure supplies one uniform pressure. AlongLocalAxis supplies zero only for the two native ignored corner slots; AtCorners retains all four joint values. No joint sorting or geometry conversion occurs. Confirm the fourth-value convention separately for triangular plates. Native Boolean TRUE/1 means success.

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


**Current call sites:** [AddTrapezoidalPressure — line 46](../../src/StaadPro.Interop/Services/Load/LoadAssignmentService.cs#L46).

**Implemented managed contract:**

~~~csharp
LoadOperationResult AddTrapezoidalPressure(ILoadCase targetCase, IEnumerable<int> plateIds, TrapezoidalPressureDefinition definition);
~~~

**Behavior and alternatives:** Use for two-value variation along plate-local X/Y or four joint-ordered pressures. AddHydrostaticPressure interpolates along a global axis; AddPlateUniformPressure supplies one uniform pressure. AlongLocalAxis supplies zero only for the two native ignored corner slots; AtCorners retains all four joint values. No joint sorting or geometry conversion occurs. Confirm the fourth-value convention separately for triangular plates. Native Boolean TRUE/1 means success. See [usage and qualification limits](../Load-Expansion.md#addtrapezoidalpressure).

#### GetElementPressureLoadCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_ele.html#gac739a3b9dff3d045724072650172bf03) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_ele.html#gac739a3b9dff3d045724072650172bf03) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gac739a3b9dff3d045724072650172bf03)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to count applied pressure entries for one plate in the selected case. GetLoadItemCount counts original case command items, which need not equal this per-plate count; GetPlateConcentratedLoadCount covers concentrated loads. GetPlatePressureLoads returns the entries with values and local extents. Zero is a valid empty count, -1 is failure.

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


**Current call sites:** [PlateCount — line 70](../../src/StaadPro.Interop/Services/Load/LoadAssignmentQueryService.cs#L70).

**Implemented managed contract:**

~~~csharp
LoadQueryResult<int> GetPlatePressureLoadCount(ILoadCase targetCase, int plateId);
~~~

**Behavior and alternatives:** Use to count applied pressure entries for one plate in the selected case. GetLoadItemCount counts original case command items, which need not equal this per-plate count; GetPlateConcentratedLoadCount covers concentrated loads. GetPlatePressureLoads returns the entries with values and local extents. Zero is a valid empty count, -1 is failure. See [usage and qualification limits](../Load-Expansion.md#getplatepressureloadcount).

#### GetElementPressureLoads

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_ele.html#ga29a49308a9a4be3345a5dde809ccd6cf) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_ele.html#ga29a49308a9a4be3345a5dde809ccd6cf) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga29a49308a9a4be3345a5dde809ccd6cf)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to read applied pressure entries and local rectangular extents for one plate. GetPlateLoadCommand reads an original full-pressure command packet by item index; GetPlateConcentratedLoads reads force entries at local points. These are load definitions, not Output plate stresses. Native top-left/bottom-right order is retained without min/max sorting. The bounded owned snapshot validates all directions and finite parameters and rechecks the count. Native 0 means success; -1 means failure. Zero count returns a read-only empty snapshot without fetching.

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


**Current call sites:** [ReadPlate — line 101](../../src/StaadPro.Interop/Services/Load/LoadAssignmentQueryService.cs#L101).

**Implemented managed contract:**

~~~csharp
LoadQueryResult<IEnumerable<PlatePressureLoad>> GetPlatePressureLoads(ILoadCase targetCase, int plateId, int maximumResults = 100000);
~~~

**Behavior and alternatives:** Use to read applied pressure entries and local rectangular extents for one plate. GetPlateLoadCommand reads an original full-pressure command packet by item index; GetPlateConcentratedLoads reads force entries at local points. These are load definitions, not Output plate stresses. Native top-left/bottom-right order is retained without min/max sorting. The bounded owned snapshot validates all directions and finite parameters and rechecks the count. Native 0 means success; -1 means failure. Zero count returns a read-only empty snapshot without fetching. See [usage and qualification limits](../Load-Expansion.md#getplatepressureloads).

#### GetElementConcLoadCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_ele.html#ga86fa47a705ec3bd23b1a8ef5ab4353b6) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_ele.html#ga86fa47a705ec3bd23b1a8ef5ab4353b6) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga86fa47a705ec3bd23b1a8ef5ab4353b6)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to count concentrated applied-load entries for one plate. GetPlatePressureLoadCount counts pressure entries; GetLoadItemCount counts original commands for the case. GetPlateConcentratedLoads returns magnitude and local position. Zero is a valid empty count, -1 is failure.

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


**Current call sites:** [PlateCount — line 71](../../src/StaadPro.Interop/Services/Load/LoadAssignmentQueryService.cs#L71).

**Implemented managed contract:**

~~~csharp
LoadQueryResult<int> GetPlateConcentratedLoadCount(ILoadCase targetCase, int plateId);
~~~

**Behavior and alternatives:** Use to count concentrated applied-load entries for one plate. GetPlatePressureLoadCount counts pressure entries; GetLoadItemCount counts original commands for the case. GetPlateConcentratedLoads returns magnitude and local position. Zero is a valid empty count, -1 is failure. See [usage and qualification limits](../Load-Expansion.md#getplateconcentratedloadcount).

#### GetElementConcLoads

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_ele.html#ga68a485fc7281ecf5e60ce93672d6445d) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_ele.html#ga68a485fc7281ecf5e60ce93672d6445d) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#ga68a485fc7281ecf5e60ce93672d6445d)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use for concentrated applied loads at local plate positions. GetPlatePressureLoads instead returns pressure entries and rectangular extents; GetPlateLoadCommand returns an original pressure command. Magnitudes remain in current native units and are not divided by area. Native 0 means success, -1 means failure; bounded paired buffers and before/after counts protect the owned snapshot. A zero count returns an empty snapshot without fetching.

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


**Current call sites:** [ReadPlate — line 102](../../src/StaadPro.Interop/Services/Load/LoadAssignmentQueryService.cs#L102).

**Implemented managed contract:**

~~~csharp
LoadQueryResult<IEnumerable<PlateConcentratedLoad>> GetPlateConcentratedLoads(ILoadCase targetCase, int plateId, int maximumResults = 100000);
~~~

**Behavior and alternatives:** Use for concentrated applied loads at local plate positions. GetPlatePressureLoads instead returns pressure entries and rectangular extents; GetPlateLoadCommand returns an original pressure command. Magnitudes remain in current native units and are not divided by area. Native 0 means success, -1 means failure; bounded paired buffers and before/after counts protect the owned snapshot. A zero count returns an empty snapshot without fetching. See [usage and qualification limits](../Load-Expansion.md#getplateconcentratedloads).

#### GetElementLoadInfo

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_ele.html#gae252989768c7a639b3f13bb01cc84809) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_ele.html#gae252989768c7a639b3f13bb01cc84809) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_ele.html#gae252989768c7a639b3f13bb01cc84809)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use only for the original ElementLoadPressure (PlateFullPressure, 3310) item packet. GetPlatePressureLoads reads per-plate pressure assignments; it is the alternative for those values and extents. Concentrated, trapezoidal, hydrostatic and partial-pressure item codes are not treated as full-pressure packets. The item index is zero-based. This native reader alone reports directions 0-8, translated to LoadDirection 1-9; member/plate-list readers already use 1-9. Native 1 means success, 0 failure, -1 Unsupported. Bentley explicitly requires selecting the load case in the STAAD UI: managed activation and numeric checks do not certify UI selection. Ensure that UI prerequisite before calling.

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


**Current call sites:** [GetPlateLoadCommand — line 61](../../src/StaadPro.Interop/Services/Load/LoadAssignmentQueryService.cs#L61).

**Implemented managed contract:**

~~~csharp
LoadQueryResult<PlateLoadCommandInfo> GetPlateLoadCommand(ILoadCase targetCase, int loadItemIndex);
~~~

**Behavior and alternatives:** Use only for the original ElementLoadPressure (PlateFullPressure, 3310) item packet. GetPlatePressureLoads reads per-plate pressure assignments; it is the alternative for those values and extents. Concentrated, trapezoidal, hydrostatic and partial-pressure item codes are not treated as full-pressure packets. The item index is zero-based. This native reader alone reports directions 0-8, translated to LoadDirection 1-9; member/plate-list readers already use 1-9. Native 1 means success, 0 failure, -1 Unsupported. Bentley explicitly requires selecting the load case in the STAAD UI: managed activation and numeric checks do not certify UI selection. Ensure that UI prerequisite before calling. See [usage and qualification limits](../Load-Expansion.md#getplateloadcommand).

### Load Items: Floor

#### AddMemberFloorLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_floor.html#gad8d2365f154d84c6ac48bf733b3a41fd) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_floor.html#gad8d2365f154d84c6ac48bf733b3a41fd) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gad8d2365f154d84c6ac48bf733b3a41fd)

**Comparison:** Common. **Coverage:** A — managed alternative/composition; intentionally no exact dispatch.

**Managed capability:** Covered alternative. Use to let STAAD find enclosed floor panels and distribute pressure using a coordinate range or an existing FLOOR group. AddMemberAreaLoad applies the separate member AREA command; GetFloorInfluenceAreas observes geometry and assigns nothing. The extended native floor function handles all modes, so the legacy fixed-direction call is not duplicated. Range axis and loading axis are independent. Standard range mode passes the single-space marker shown in Bentley's VBA example; one-way mode passes "0" from its parameter table. Group mode passes the exact group name and zero ignored bounds; it does not create the group. Native 1 means success. Verify generated distributions, particularly one-way floors, against the target model.

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


**Implemented managed contract:**

~~~csharp
LoadOperationResult AddFloorLoad(ILoadCase targetCase, FloorLoadDefinition definition);
~~~

**Behavior and alternatives:** Use to let STAAD find enclosed floor panels and distribute pressure using a coordinate range or an existing FLOOR group. AddMemberAreaLoad applies the separate member AREA command; GetFloorInfluenceAreas observes geometry and assigns nothing. The extended native floor function handles all modes, so the legacy fixed-direction call is not duplicated. Range axis and loading axis are independent. Standard range mode passes the single-space marker shown in Bentley's VBA example; one-way mode passes "0" from its parameter table. Group mode passes the exact group name and zero ignored bounds; it does not create the group. Native 1 means success. Verify generated distributions, particularly one-way floors, against the target model. See [usage and qualification limits](../Load-Expansion.md#addfloorload).

#### GetBeamCountAtFloor

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_floor.html#gaa95d436c89c336e5002ffc642fbed2b6) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_floor.html#gaa95d436c89c336e5002ffc642fbed2b6) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gaa95d436c89c336e5002ffc642fbed2b6)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use for the native beam count in a bounded floor region, without assigning loads or selecting a load case. This is not GetAssignedEntityCount, which counts entities attached to an existing command. GetFloorInfluenceAreas returns beam/area pairs for the same region. Coordinates are converted to native Single precision (with range checks); RangeAxis maps X/Y/Z to native 1/2/3. Zero is an empty count; malformed negatives are rejected except -1, returned as NativeFailure.

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


**Current call sites:** [FloorCount — line 125](../../src/StaadPro.Interop/Services/Load/LoadAssignmentQueryService.cs#L125).

**Implemented managed contract:**

~~~csharp
LoadQueryResult<int> GetFloorBeamCount(FloorRegion region);
~~~

**Behavior and alternatives:** Use for the native beam count in a bounded floor region, without assigning loads or selecting a load case. This is not GetAssignedEntityCount, which counts entities attached to an existing command. GetFloorInfluenceAreas returns beam/area pairs for the same region. Coordinates are converted to native Single precision (with range checks); RangeAxis maps X/Y/Z to native 1/2/3. Zero is an empty count; malformed negatives are rejected except -1, returned as NativeFailure. See [usage and qualification limits](../Load-Expansion.md#getfloorbeamcount).

#### GetInfluenceArea

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_floor.html#gaba0ac897a60241ed8a3a8e00506e5bed) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_floor.html#gaba0ac897a60241ed8a3a8e00506e5bed) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gaba0ac897a60241ed8a3a8e00506e5bed)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to observe beam/tributary influence-area pairs calculated by STAAD for a floor region. GetFloorBeamCount provides only sizing; AddFloorLoad actually assigns pressure. This method does not multiply areas by pressure or calculate managed tributary areas. It uses the same converted Single-precision bounds and range axis for sizing and retrieval (correcting the differing axes in Bentley's example). Validates positive beam IDs, finite nonnegative areas, retrieval limit and stable count. Native Boolean TRUE means success; areas use squared native length units.

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


**Current call sites:** [GetFloorInfluenceAreas — line 143](../../src/StaadPro.Interop/Services/Load/LoadAssignmentQueryService.cs#L143).

**Implemented managed contract:**

~~~csharp
LoadQueryResult<IEnumerable<BeamInfluenceArea>> GetFloorInfluenceAreas(FloorRegion region, int maximumResults = 100000);
~~~

**Behavior and alternatives:** Use to observe beam/tributary influence-area pairs calculated by STAAD for a floor region. GetFloorBeamCount provides only sizing; AddFloorLoad actually assigns pressure. This method does not multiply areas by pressure or calculate managed tributary areas. It uses the same converted Single-precision bounds and range axis for sizing and retrieval (correcting the differing axes in Bentley's example). Validates positive beam IDs, finite nonnegative areas, retrieval limit and stable count. Native Boolean TRUE means success; areas use squared native length units. See [usage and qualification limits](../Load-Expansion.md#getfloorinfluenceareas).

#### AddMemberFloorLoadEx

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_floor.html#gae4dbae2a1476dc8b9105842bd568fb67) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_floor.html#gae4dbae2a1476dc8b9105842bd568fb67) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_floor.html#gae4dbae2a1476dc8b9105842bd568fb67)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to let STAAD find enclosed floor panels and distribute pressure using a coordinate range or an existing FLOOR group. AddMemberAreaLoad applies the separate member AREA command; GetFloorInfluenceAreas observes geometry and assigns nothing. The extended native floor function handles all modes, so the legacy fixed-direction call is not duplicated. Range axis and loading axis are independent. Standard range mode passes the single-space marker shown in Bentley's VBA example; one-way mode passes "0" from its parameter table. Group mode passes the exact group name and zero ignored bounds; it does not create the group. Native 1 means success. Verify generated distributions, particularly one-way floors, against the target model.

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


**Current call sites:** [AddFloorLoad — line 57](../../src/StaadPro.Interop/Services/Load/LoadAssignmentService.cs#L57).

**Implemented managed contract:**

~~~csharp
LoadOperationResult AddFloorLoad(ILoadCase targetCase, FloorLoadDefinition definition);
~~~

**Behavior and alternatives:** Use to let STAAD find enclosed floor panels and distribute pressure using a coordinate range or an existing FLOOR group. AddMemberAreaLoad applies the separate member AREA command; GetFloorInfluenceAreas observes geometry and assigns nothing. The extended native floor function handles all modes, so the legacy fixed-direction call is not duplicated. Range axis and loading axis are independent. Standard range mode passes the single-space marker shown in Bentley's VBA example; one-way mode passes "0" from its parameter table. Group mode passes the exact group name and zero ignored bounds; it does not create the group. Native 1 means success. Verify generated distributions, particularly one-way floors, against the target model. See [usage and qualification limits](../Load-Expansion.md#addfloorload).

### Load Items: Temperature

#### AddTemperatureLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_temp.html#ga63fe8160cb8445ac3d7acd1697b5b428) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_temp.html#ga63fe8160cb8445ac3d7acd1697b5b428) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_temp.html#ga63fe8160cb8445ac3d7acd1697b5b428)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [AddAxialThermalGradientTo — line 1411](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1411); [AddAxialThermalGradientTo — line 1429](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1429).

#### AddStrainLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_temp.html#ga33d9916b222ffd303f9625232a287451) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_temp.html#ga33d9916b222ffd303f9625232a287451) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_temp.html#ga33d9916b222ffd303f9625232a287451)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to assign the native initial axial elongation parameter to explicit element IDs. Despite the native Strain name, axialElongation is a length in current model units, not dimensionless strain; no division or multiplication by member length occurs. AddAxialThermalGradientTo instead supplies a thermal gradient, and fixed-end loading instead supplies force/moment components. Native 0 means success.

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


**Current call sites:** [AddStrainLoad — line 64](../../src/StaadPro.Interop/Services/Load/LoadAssignmentService.cs#L64).

**Implemented managed contract:**

~~~csharp
LoadOperationResult AddStrainLoad(ILoadCase targetCase, IEnumerable<int> elementIds, double axialElongation);
~~~

**Behavior and alternatives:** Use to assign the native initial axial elongation parameter to explicit element IDs. Despite the native Strain name, axialElongation is a length in current model units, not dimensionless strain; no division or multiplication by member length occurs. AddAxialThermalGradientTo instead supplies a thermal gradient, and fixed-end loading instead supplies force/moment components. Native 0 means success. See [usage and qualification limits](../Load-Expansion.md#addstrainload).

### Load Items: Seismic

#### AddSeismicLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_seismic.html#gaef2b657a5ddda1beb6c8af149585ffc7) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_seismic.html#gaef2b657a5ddda1beb6c8af149585ffc7) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_seismic.html#gaef2b657a5ddda1beb6c8af149585ffc7)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to apply the existing seismic definition to a case with a global axis and multiplier. AddSeismicDefinition creates definition settings; AddSeismicDefSelfWeight and joint/member weight methods contribute seismic weights. AddRSLoad/AddResponseSpectrumLoadEx create response-spectrum commands instead of this generated static seismic command. GlobalAxis maps directly to native 0/1/2, unlike self-weight or hydrostatic interpolation. Native 0 means success; -1/-8001 are failures.

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


**Current call sites:** [AddSeismicLoad — line 69](../../src/StaadPro.Interop/Services/Load/LoadAssignmentService.cs#L69).

**Implemented managed contract:**

~~~csharp
LoadOperationResult AddSeismicLoad(ILoadCase targetCase, GlobalAxis direction, double factor);
~~~

**Behavior and alternatives:** Use to apply the existing seismic definition to a case with a global axis and multiplier. AddSeismicDefinition creates definition settings; AddSeismicDefSelfWeight and joint/member weight methods contribute seismic weights. AddRSLoad/AddResponseSpectrumLoadEx create response-spectrum commands instead of this generated static seismic command. GlobalAxis maps directly to native 0/1/2, unlike self-weight or hydrostatic interpolation. Native 0 means success; -1/-8001 are failures. See [usage and qualification limits](../Load-Expansion.md#addseismicload).

#### IsDynamicLoadIncluded

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_seismic.html#gafe4f103eedc99ab92900eb5342952e6f) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_seismic.html#gafe4f103eedc99ab92900eb5342952e6f) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_seismic.html#gafe4f103eedc99ab92900eb5342952e6f)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Native 1/0 means true/false successful data; -1 is NativeFailure, never Boolean true. Actual Boolean native returns are also accepted. Does not classify a case from its title or engineering type, activate it, or start analysis.

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

**Current call sites:** [ContainsDynamicLoad — line 189](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L189).

### Load Items: Time History

No native function declarations occur in this reviewed grouping page. Subgroups, where present, were traversed separately.

### Load Items: Wind and Snow

#### AddWindLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_wind.html#gaa9ed8f688a0b105d17f3540581472685) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_wind.html#gaa9ed8f688a0b105d17f3540581472685) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_wind.html#gaa9ed8f688a0b105d17f3540581472685)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to apply an existing wind definition to a case over an explicit global region. AddWindDefinition/AddWindIntensity/AddWindExposure prepare that definition; ComputeWallWindPressureProfile computes a profile and does not assign this command. This native contract assumes Y vertical: a Z-up or unavailable geometry binding returns Unsupported before activation/assignment. No automatic coordinate remapping occurs. Uses documented X/Z signed codes 1/3/4/6 rather than the inconsistent example code 2. Native 0 means success. Signed factors are retained, so a negative factor can reverse the selected direction. Bentley’s all-zero-bounds example denotes whole-range walls; an all-zero region must not be assumed to select only a geometric point. Supply appropriate nondegenerate bounds when restricting scope.

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


**Current call sites:** [AddWindLoad — line 80](../../src/StaadPro.Interop/Services/Load/LoadAssignmentService.cs#L80).

**Implemented managed contract:**

~~~csharp
LoadOperationResult AddWindLoad(ILoadCase targetCase, WindLoadDefinition definition);
~~~

**Behavior and alternatives:** Use to apply an existing wind definition to a case over an explicit global region. AddWindDefinition/AddWindIntensity/AddWindExposure prepare that definition; ComputeWallWindPressureProfile computes a profile and does not assign this command. This native contract assumes Y vertical: a Z-up or unavailable geometry binding returns Unsupported before activation/assignment. No automatic coordinate remapping occurs. Uses documented X/Z signed codes 1/3/4/6 rather than the inconsistent example code 2. Native 0 means success. Signed factors are retained, so a negative factor can reverse the selected direction. Bentley’s all-zero-bounds example denotes whole-range walls; an all-zero region must not be assumed to select only a geometric point. Supply appropriate nondegenerate bounds when restricting scope. See [usage and qualification limits](../Load-Expansion.md#addwindload).

### Load Items: Repeat

#### AddRepeatLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga605977fe7246b588f3bcacfa4f74d80a) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga605977fe7246b588f3bcacfa4f74d80a) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga605977fe7246b588f3bcacfa4f74d80a)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [AddRepeatLoadsTo — line 1396](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1396).

#### AddNotionalLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#gad19ac47adbd00da2d8e56ff8c7bea779) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#gad19ac47adbd00da2d8e56ff8c7bea779) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gad19ac47adbd00da2d8e56ff8c7bea779)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to assign explicitly chosen primary and reference contributions to an existing case. AddRepeatLoadsTo/AddReferenceLoadsTo do not specify notional directions; CreateAutomaticRepeatLoads can generate notional participation through code rules rather than this explicit list. Contributions are partitioned into six aligned native buffers, preserving order and duplicates within each family. Assignment uses positive IDs in both families; GetNotionalContributions uses signed native IDs on readback. Factors retain signs, including zero, and directions use global X/Y/Z 1/2/3. Native 0 means success.

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


**Current call sites:** [AddNotionalLoad — line 92](../../src/StaadPro.Interop/Services/Load/LoadAssignmentService.cs#L92).

**Implemented managed contract:**

~~~csharp
LoadOperationResult AddNotionalLoad(ILoadCase targetCase, IEnumerable<NotionalLoadContribution> contributions);
~~~

**Behavior and alternatives:** Use to assign explicitly chosen primary and reference contributions to an existing case. AddRepeatLoadsTo/AddReferenceLoadsTo do not specify notional directions; CreateAutomaticRepeatLoads can generate notional participation through code rules rather than this explicit list. Contributions are partitioned into six aligned native buffers, preserving order and duplicates within each family. Assignment uses positive IDs in both families; GetNotionalContributions uses signed native IDs on readback. Factors retain signs, including zero, and directions use global X/Y/Z 1/2/3. Native 0 means success. See [usage and qualification limits](../Load-Expansion.md#addnotionalload).

#### GetRepeatLoadCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga079ca807e856f915ffdf411f9c7fa468) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga079ca807e856f915ffdf411f9c7fa468) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga079ca807e856f915ffdf411f9c7fa468)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

```cpp
long OSLoadUI::GetRepeatLoadCount();
```

No native parameters.

**Declared return:** `long`. **Inspected CLR return:** `System.Int32`.

| Native return label | Native meaning |
| --- | --- |
| `<Val>` | The number of repeat load commands in the active load case.To set the active load case use: - VARIANT OSLoadUI::SetLoadActive(const VARIANT FAR & varLoadNo) . To get the active load case use:- VARIANT OSLoadUI::GetActiveLoad() |
| `0` | General error. |


**Current call sites:** [GetRepeatLoadCases — line 1509](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1509).

#### GetNoLoadFactorInRepeatLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga6ec117c499264c58e0b43ed223909834) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga6ec117c499264c58e0b43ed223909834) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga6ec117c499264c58e0b43ed223909834)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Uses the documented one-based repeat index. Zero is a successful empty count and -1 is NotFound for an invalid repeat index. This is a command contribution count, not the number of cases containing repeat loads.

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

**Current call sites:** [ContributionCount — line 221](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L221).

#### GetRepeatLoadByIndex

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga823d0f1628893bee6ab495c46bffcfe7) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga823d0f1628893bee6ab495c46bffcfe7) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga823d0f1628893bee6ab495c46bffcfe7)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Sizes Int32 ID and Double factor buffers with GetNoLoadFactorInRepeatLoad; checks native returned size and a post-fetch count. Preserves duplicate positive primary IDs and finite signed/zero factors in an immutable materialized sequence. Zero pairs returns an empty sequence without fetching; -1 is NotFound. No dictionary deduplication or summation occurs.

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

**Current call sites:** [ReadContributions — line 248](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L248).

#### GetNotionalLoadCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga407c6622c4ba44ae45e96b60d943e51a) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga407c6622c4ba44ae45e96b60d943e51a) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga407c6622c4ba44ae45e96b60d943e51a)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Zero is valid and -1 is NativeFailure. Counts command items, not their constituent case/factor/direction contributions.

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

**Current call sites:** [GetNotionalItemCount — line 215](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L215).

#### GetNoLoadFactorDirectionInNotionalLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga6a4ecff14aed8f0398fed83ab7781e9e) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga6a4ecff14aed8f0398fed83ab7781e9e) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga6a4ecff14aed8f0398fed83ab7781e9e)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Forwards a positive, one-based index following the installed examples that address command 1. Zero is valid and -1 is NativeFailure. Interprets the result as the paired reader buffer size; the manual also calls it a factor without documenting a numeric factor payload. Live index/count semantics require qualification.

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

**Current call sites:** [ContributionCount — line 220](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L220).

#### GetNotionalLoadByIndex

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#gad309996311f49a218df37558d7ad7cb8) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#gad309996311f49a218df37558d7ad7cb8) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gad309996311f49a218df37558d7ad7cb8)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Sizes paired Int32 ID/direction and Double factor buffers using GetNoLoadFactorDirectionInNotionalLoad. Positive native IDs mean primary cases and negative IDs reference cases; returns their family, positive ID and original signed ID. Global direction codes 1/2/3 are X/Y/Z, not LoadDirection local axes. Preserves order, duplicates and signed/zero factors. Zero contributions skips fetch; -1 is NativeFailure. One-based indexing follows installed examples and remains a live qualification requirement.

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

**Current call sites:** [ReadContributions — line 247](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L247).

#### AddReferenceLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga6b018e4f7084d51fdbcafafc7773257b) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga6b018e4f7084d51fdbcafafc7773257b) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga6b018e4f7084d51fdbcafafc7773257b)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [AddReferenceLoadsTo — line 1386](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1386).

#### GetReferenceLoadCount

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga1ee158fd662d041c3676a3af85d76f17) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga1ee158fd662d041c3676a3af85d76f17) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga1ee158fd662d041c3676a3af85d76f17)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Zero is valid and -1 is NativeFailure. Does not count reference-case definitions; use existing GetAllReferenceLoadCases for that separate inventory.

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

**Current call sites:** [GetReferenceItemCount — line 218](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L218).

#### GetNoOfSetsInReferenceLoad

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga121e0d6b8f66e0ed27aed4ebdf73d67c) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga121e0d6b8f66e0ed27aed4ebdf73d67c) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga121e0d6b8f66e0ed27aed4ebdf73d67c)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to size the case/factor pairs inside one REFERENCE command of the activated primary case. GetReferenceItemCount counts REFERENCE commands; GetRepeatContributionCount counts primary repeat contributions. referenceItemIndex is passed unchanged to the native API: the installed reference help does not establish its index base and has an erroneous ID description in its count table. Confirm the native index on the acquired build; no speculative rebasing or ordinal-to-ID conversion is performed. -1 is NativeFailure.

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


**Current call sites:** [GetReferenceContributionCount — line 301](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L301); [GetReferenceContributions — line 309](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L309); [GetReferenceContributions — line 324](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L324).

**Implemented managed contract:**

~~~csharp
LoadQueryResult<int> GetReferenceContributionCount(int primaryLoadCaseId, int referenceItemIndex);
~~~

**Behavior and alternatives:** Use to size the case/factor pairs inside one REFERENCE command of the activated primary case. GetReferenceItemCount counts REFERENCE commands; GetRepeatContributionCount counts primary repeat contributions. referenceItemIndex is passed unchanged to the native API: the installed reference help does not establish its index base and has an erroneous ID description in its count table. Confirm the native index on the acquired build; no speculative rebasing or ordinal-to-ID conversion is performed. -1 is NativeFailure. See [usage and qualification limits](../Load-Expansion.md#getreferencecontributioncount).

#### GetReferenceLoadByIndex

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#gaa32d2d60d54563abc19c9be826990e3a) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#gaa32d2d60d54563abc19c9be826990e3a) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gaa32d2d60d54563abc19c9be826990e3a)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented. Use to retain ordered reference-case/factor pairs in one REFERENCE command. GetReferenceItemCount counts commands, and GetRepeatContributions returns primary repeat pairs. Duplicate reference IDs and signed/zero factors are preserved. Unlike the repeat reader, the reference API's scalar is described as an ID, not a returned count; it is retained as NativeReturnCode and is not required to equal the buffer size. Data is validated against the sizing call and rechecked count. The native index is passed unchanged; Bentley does not specify its base. Confirm it on the acquired build. -1 is NativeFailure.

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


**Current call sites:** [GetReferenceContributions — line 316](../../src/StaadPro.Interop/Services/Load/LoadInspectionService.cs#L316).

**Implemented managed contract:**

~~~csharp
LoadQueryResult<IEnumerable<ReferenceLoadContribution>> GetReferenceContributions(int primaryLoadCaseId, int referenceItemIndex, int maximumResults = 100000);
~~~

**Behavior and alternatives:** Use to retain ordered reference-case/factor pairs in one REFERENCE command. GetReferenceItemCount counts commands, and GetRepeatContributions returns primary repeat pairs. Duplicate reference IDs and signed/zero factors are preserved. Unlike the repeat reader, the reference API's scalar is described as an ID, not a returned count; it is retained as NativeReturnCode and is not required to equal the buffer size. Data is validated against the sizing call and rechecked count. The native index is passed unchanged; Bentley does not specify its base. Confirm it on the acquired build. -1 is NativeFailure. See [usage and qualification limits](../Load-Expansion.md#getreferencecontributions).

#### GetReferenceLoadType

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#gad0770a978141a1c4f3a8874fe446da54) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#gad0770a978141a1c4f3a8874fe446da54) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#gad0770a978141a1c4f3a8874fe446da54)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [ReadLoadCaseMetadata — line 3285](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3285).

#### GetReferenceLoadCaseTitle

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga801c4cec74cc10e3f8ac91559417414c) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga801c4cec74cc10e3f8ac91559417414c) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga801c4cec74cc10e3f8ac91559417414c)

**Comparison:** Common. **Coverage:** D — production dispatch; native qualification remains pending.

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


**Current call sites:** [ReadLoadCaseMetadata — line 3282](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3282).

#### BeginLoadMerging

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga4a4a3ab7b12b6456ddce1d83568b50bf) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga4a4a3ab7b12b6456ddce1d83568b50bf) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga4a4a3ab7b12b6456ddce1d83568b50bf)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented invocation evidence. Use after a geometry merge when source-beam load definitions must be merged into retained beams. SplitLoadsOnBeam performs the opposite load transfer; neither method creates or merges geometry. Requests run in supplied order, with one BeginLoadMerging and an EndLoadMerging attempt in finally, even if begin/body throws. The body signature is corroborated by the Bentley class listing and installed CLR metadata, but its return meanings and affected-case scope are not documented. Consequently normal return is InvocationCompleted, IsSuccessful is false, and actual Boolean/integral responses remain in Steps; inspect model results rather than interpreting this as certified success. The merge is not transactional, may affect loads across cases, and is never retried. Both body and cleanup exceptions are preserved in AggregateException; a single exception retains its original stack. MayHaveChangedModel is true after invocation; no rollback is promised.

```cpp
void OSLoadUI::BeginLoadMerging();
```

No native parameters.

**Declared return:** `void`. **Inspected CLR return:** `System.Void`.

No return-value table is present. Read the return prose in the linked reference; do not infer Boolean success or invent a payload.


**Current call sites:** [MergeBeamLoads — line 30](../../src/StaadPro.Interop/Services/Load/LoadBeamTransferService.cs#L30).

**Implemented managed contract:**

~~~csharp
LoadOperationResult MergeBeamLoads(IEnumerable<BeamLoadMergeRequest> requests);
~~~

**Behavior and alternatives:** Use after a geometry merge when source-beam load definitions must be merged into retained beams. SplitLoadsOnBeam performs the opposite load transfer; neither method creates or merges geometry. Requests run in supplied order, with one BeginLoadMerging and an EndLoadMerging attempt in finally, even if begin/body throws. The body signature is corroborated by the Bentley class listing and installed CLR metadata, but its return meanings and affected-case scope are not documented. Consequently normal return is InvocationCompleted, IsSuccessful is false, and actual Boolean/integral responses remain in Steps; inspect model results rather than interpreting this as certified success. The merge is not transactional, may affect loads across cases, and is never retried. Both body and cleanup exceptions are preserved in AggregateException; a single exception retains its original stack. MayHaveChangedModel is true after invocation; no rollback is promised. See [usage and qualification limits](../Load-Expansion.md#mergebeamloads).

#### EndLoadMerging

[2024 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___property_load_repeat.html#ga07b273284cfdb265a5fae76d7631155f) · [2025 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___property_load_repeat.html#ga07b273284cfdb265a5fae76d7631155f) · [2026 reference](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___property_load_repeat.html#ga07b273284cfdb265a5fae76d7631155f)

**Comparison:** Common. **Coverage:** S — production dispatch; native qualification remains pending.

**Managed capability:** Implemented invocation evidence. Use after a geometry merge when source-beam load definitions must be merged into retained beams. SplitLoadsOnBeam performs the opposite load transfer; neither method creates or merges geometry. Requests run in supplied order, with one BeginLoadMerging and an EndLoadMerging attempt in finally, even if begin/body throws. The body signature is corroborated by the Bentley class listing and installed CLR metadata, but its return meanings and affected-case scope are not documented. Consequently normal return is InvocationCompleted, IsSuccessful is false, and actual Boolean/integral responses remain in Steps; inspect model results rather than interpreting this as certified success. The merge is not transactional, may affect loads across cases, and is never retried. Both body and cleanup exceptions are preserved in AggregateException; a single exception retains its original stack. MayHaveChangedModel is true after invocation; no rollback is promised.

```cpp
void OSLoadUI::EndLoadMerging();
```

No native parameters.

**Declared return:** `void`. **Inspected CLR return:** `System.Void`.

No return-value table is present. Read the return prose in the linked reference; do not infer Boolean success or invent a payload.


**Current call sites:** [MergeBeamLoads — line 48](../../src/StaadPro.Interop/Services/Load/LoadBeamTransferService.cs#L48).

**Implemented managed contract:**

~~~csharp
LoadOperationResult MergeBeamLoads(IEnumerable<BeamLoadMergeRequest> requests);
~~~

**Behavior and alternatives:** Use after a geometry merge when source-beam load definitions must be merged into retained beams. SplitLoadsOnBeam performs the opposite load transfer; neither method creates or merges geometry. Requests run in supplied order, with one BeginLoadMerging and an EndLoadMerging attempt in finally, even if begin/body throws. The body signature is corroborated by the Bentley class listing and installed CLR metadata, but its return meanings and affected-case scope are not documented. Consequently normal return is InvocationCompleted, IsSuccessful is false, and actual Boolean/integral responses remain in Steps; inspect model results rather than interpreting this as certified success. The merge is not transactional, may affect loads across cases, and is never retried. Both body and cleanup exceptions are preserved in AggregateException; a single exception retains its original stack. MayHaveChangedModel is true after invocation; no rollback is promised. See [usage and qualification limits](../Load-Expansion.md#mergebeamloads).

## 2. Supplemental installed interop inventory

The following names appear in both passively inspected IOSLoadUI interfaces but not in the 129-function Load help inventory. MergeLoadsOnBeam is now invoked through the balanced MergeBeamLoads operation; its response is retained as invocation evidence without a success interpretation. The two seismic-weight candidates remain outside the approved expansion and have no dispatch. Presence in 2025/2026 metadata does not establish introduction in 2025 or absence in 2024.

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

**Disposition:** Implemented through MergeBeamLoads, with Begin/End cleanup ownership, actual integral/Boolean evidence, InvocationCompleted rather than Success, and preserved body/cleanup exceptions. Signature presence is corroborated; native return meanings, topology and engineering effects remain unqualified. See [the operation contract](../Load-Expansion.md#mergebeamloads).

## 3. Existing managed interface and adapter surface

All **171 interface method overloads** have matching public adapter signatures: 115 main-reference methods, five earlier conveniences, 15 inspection operations and 36 approved expansion methods. The direct-call column covers only the adapter method body; delegated services/entities are included in the native coverage inventory above. ComObject and inherited properties are outside the method count.

| # | Current managed signature | Interface / adapter | Direct native calls in method body |
| ---: | --- | --- | --- |
| 1 | `LoadOperationResult SplitLoadsOnBeam(int originalBeamId, int newBeamId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L36) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L66) | Delegates / composes managed operations |
| 2 | `LoadOperationResult AddCasesToEnvelope(int envelopeId, IEnumerable<int> primaryLoadCaseIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L53) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L84) | Delegates / composes managed operations |
| 3 | `LoadOperationResult RemoveCasesFromEnvelope(int envelopeId, IEnumerable<int> primaryLoadCaseIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L70) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L102) | Delegates / composes managed operations |
| 4 | `LoadOperationResult DeleteEnvelope(int envelopeId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L86) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L119) | Delegates / composes managed operations |
| 5 | `LoadOperationResult<AutomaticLoadGenerationData> CreateAutomaticCombinations(AutomaticCombinationRequest request)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L103) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L137) | Delegates / composes managed operations |
| 6 | `LoadOperationResult<AutomaticLoadGenerationData> CreateAutomaticRepeatLoads(AutomaticRepeatLoadRequest request)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L120) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L155) | Delegates / composes managed operations |
| 7 | `LoadOperationResult SetLoadType(IEnumerable<int> loadCaseIds, LoadType loadType)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L137) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L173) | Delegates / composes managed operations |
| 8 | `LoadOperationResult DeleteLoadList(int loadListIndex)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L153) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L190) | Delegates / composes managed operations |
| 9 | `LoadOperationResult SetAllowableStressAttribute(int loadCaseId, LoadStrengthType strengthType, bool increaseAllowableStress)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L171) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L209) | Delegates / composes managed operations |
| 10 | `LoadOperationResult SetLimitStateAttribute(int loadCaseId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L187) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L226) | Delegates / composes managed operations |
| 11 | `LoadOperationResult RemoveLoadAttribute(int loadCaseId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L203) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L243) | Delegates / composes managed operations |
| 12 | `LoadQueryResult<LoadAttributeObservation> GetLoadAttribute(int loadCaseId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L220) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L261) | Delegates / composes managed operations |
| 13 | `LoadQueryResult<IEnumerable<int>> GetAssignedEntityIds(int primaryLoadCaseId, LoadCommandKind commandKind, int commandIndex, int maximumResults = 100000)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L240) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L282) | Delegates / composes managed operations |
| 14 | `LoadOperationResult DeleteLoadCases(LoadCaseType caseType, IEnumerable<int> loadCaseIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L257) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L300) | Delegates / composes managed operations |
| 15 | `LoadOperationResult AddSelfWeight(ILoadCase targetCase, SelfWeightDirection direction, double factor)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L275) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L319) | Delegates / composes managed operations |
| 16 | `LoadQueryResult<NodalLoadCommandInfo> GetNodalLoadCommand(ILoadCase targetCase, int loadItemIndex)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L293) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L338) | Delegates / composes managed operations |
| 17 | `LoadOperationResult AddMemberAreaLoad(ILoadCase targetCase, IEnumerable<int> memberIds, double areaLoad)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L311) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L357) | Delegates / composes managed operations |
| 18 | `LoadOperationResult AddMemberFixedEndLoad(ILoadCase targetCase, IEnumerable<int> memberIds, FixedEndLoadDefinition definition)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L329) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L376) | Delegates / composes managed operations |
| 19 | `LoadQueryResult<MemberLoadCommandInfo> GetMemberLoadCommand(ILoadCase targetCase, int loadItemIndex)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L347) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L395) | Delegates / composes managed operations |
| 20 | `LoadOperationResult AddHydrostaticPressure(ILoadCase targetCase, IEnumerable<int> plateIds, HydrostaticPressureDefinition definition)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L365) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L414) | Delegates / composes managed operations |
| 21 | `LoadOperationResult AddTrapezoidalPressure(ILoadCase targetCase, IEnumerable<int> plateIds, TrapezoidalPressureDefinition definition)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L383) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L433) | Delegates / composes managed operations |
| 22 | `LoadQueryResult<int> GetPlatePressureLoadCount(ILoadCase targetCase, int plateId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L401) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L452) | Delegates / composes managed operations |
| 23 | `LoadQueryResult<IEnumerable<PlatePressureLoad>> GetPlatePressureLoads(ILoadCase targetCase, int plateId, int maximumResults = 100000)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L420) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L472) | Delegates / composes managed operations |
| 24 | `LoadQueryResult<int> GetPlateConcentratedLoadCount(ILoadCase targetCase, int plateId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L438) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L491) | Delegates / composes managed operations |
| 25 | `LoadQueryResult<IEnumerable<PlateConcentratedLoad>> GetPlateConcentratedLoads(ILoadCase targetCase, int plateId, int maximumResults = 100000)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L457) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L511) | Delegates / composes managed operations |
| 26 | `LoadQueryResult<PlateLoadCommandInfo> GetPlateLoadCommand(ILoadCase targetCase, int loadItemIndex)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L475) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L530) | Delegates / composes managed operations |
| 27 | `LoadOperationResult AddFloorLoad(ILoadCase targetCase, FloorLoadDefinition definition)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L492) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L548) | Delegates / composes managed operations |
| 28 | `LoadQueryResult<int> GetFloorBeamCount(FloorRegion region)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L509) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L566) | Delegates / composes managed operations |
| 29 | `LoadQueryResult<IEnumerable<BeamInfluenceArea>> GetFloorInfluenceAreas(FloorRegion region, int maximumResults = 100000)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L527) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L585) | Delegates / composes managed operations |
| 30 | `LoadOperationResult AddStrainLoad(ILoadCase targetCase, IEnumerable<int> elementIds, double axialElongation)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L545) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L604) | Delegates / composes managed operations |
| 31 | `LoadOperationResult AddSeismicLoad(ILoadCase targetCase, GlobalAxis direction, double factor)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L563) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L623) | Delegates / composes managed operations |
| 32 | `LoadOperationResult AddWindLoad(ILoadCase targetCase, WindLoadDefinition definition)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L580) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L641) | Delegates / composes managed operations |
| 33 | `LoadOperationResult AddNotionalLoad(ILoadCase targetCase, IEnumerable<NotionalLoadContribution> contributions)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L597) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L659) | Delegates / composes managed operations |
| 34 | `LoadQueryResult<int> GetReferenceContributionCount(int primaryLoadCaseId, int referenceItemIndex)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L615) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L678) | Delegates / composes managed operations |
| 35 | `LoadQueryResult<IEnumerable<ReferenceLoadContribution>> GetReferenceContributions(int primaryLoadCaseId, int referenceItemIndex, int maximumResults = 100000)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L634) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L698) | Delegates / composes managed operations |
| 36 | `LoadOperationResult MergeBeamLoads(IEnumerable<BeamLoadMergeRequest> requests)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L651) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L716) | Delegates / composes managed operations |
| 37 | `LoadQueryResult<int> GetActiveLoadCaseNumber()` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L666) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L732) | Delegates / composes managed operations |
| 38 | `LoadQueryResult<int> GetLoadListCount()` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L677) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L744) | Delegates / composes managed operations |
| 39 | `LoadQueryResult<int> GetLoadListCaseCount(int loadListIndex)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L690) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L758) | Delegates / composes managed operations |
| 40 | `LoadQueryResult<IEnumerable<int>> GetLoadListCases(int loadListIndex, int maximumResults = 100000)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L704) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L773) | Delegates / composes managed operations |
| 41 | `LoadQueryResult<int> GetLoadCommandCount(int primaryLoadCaseId, LoadCommandKind commandKind)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L718) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L788) | Delegates / composes managed operations |
| 42 | `LoadQueryResult<int> GetAssignedEntityCount(int primaryLoadCaseId, LoadCommandKind commandKind, int commandIndex)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L733) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L804) | Delegates / composes managed operations |
| 43 | `LoadQueryResult<int> GetLoadItemCount(int loadCaseId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L746) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L818) | Delegates / composes managed operations |
| 44 | `LoadQueryResult<LoadItemKind> GetLoadItemKind(int loadCaseId, int loadItemIndex)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L760) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L833) | Delegates / composes managed operations |
| 45 | `LoadQueryResult<bool> ContainsDynamicLoad(int loadCaseId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L773) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L847) | Delegates / composes managed operations |
| 46 | `LoadQueryResult<int> GetRepeatContributionCount(int primaryLoadCaseId, int repeatItemIndex)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L787) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L862) | Delegates / composes managed operations |
| 47 | `LoadQueryResult<IEnumerable<LoadFactorContribution>> GetRepeatContributions(int primaryLoadCaseId, int repeatItemIndex, int maximumResults = 100000)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L802) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L878) | Delegates / composes managed operations |
| 48 | `LoadQueryResult<int> GetNotionalItemCount(int primaryLoadCaseId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L815) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L892) | Delegates / composes managed operations |
| 49 | `LoadQueryResult<int> GetNotionalContributionCount(int primaryLoadCaseId, int notionalItemIndex)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L829) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L907) | Delegates / composes managed operations |
| 50 | `LoadQueryResult<IEnumerable<NotionalLoadContribution>> GetNotionalContributions(int primaryLoadCaseId, int notionalItemIndex, int maximumResults = 100000)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L844) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L923) | Delegates / composes managed operations |
| 51 | `LoadQueryResult<int> GetReferenceItemCount(int primaryLoadCaseId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L857) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L937) | Delegates / composes managed operations |
| 52 | `bool AddSeismicDefMemberWeight(int seismicCode, SeismicMemberLoadType loadType, double weight, double startDistance, double endDistance, IEnumerable<Beam> members)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L862) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L945) | Delegates / composes managed operations |
| 53 | `bool AddSeismicDefMemberUniformWeight(double weight, IEnumerable<int> memberIds, double startDistance = 0, double endDistance = 0, int seismicCode = 0)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L865) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L955) | Delegates / composes managed operations |
| 54 | `bool AddSeismicDefMemberConcentratedWeight(double weight, double distance, IEnumerable<int> memberIds, int seismicCode = 0)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L868) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L960) | Delegates / composes managed operations |
| 55 | `int AddSeismicDefWallArea(int seismicCode, string direction, IEnumerable<double> dimensions)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L871) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L964) | AddSeismicDefWallArea |
| 56 | `int AddSeismicDefWallArea(int seismicCode, SeismicWallDirection direction, IEnumerable<SeismicWallDimension> walls)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L874) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L983) | Delegates / composes managed operations |
| 57 | `bool AddResponseSpectrumLoadEx(ResponseSpectrumLoadExParameters parameters)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L877) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L999) | Delegates / composes managed operations |
| 58 | `bool AddResponseSpectrumLoadEx(int spectrumCode, int combinationMethod, string[] primaryParameterNames, double[] primaryParameterValues, string[] generationParameterNames = null, double[] generationParameterValues = null, double[] customDataPairs = null)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L880) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1006) | Delegates / composes managed operations |
| 59 | `bool AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType parameterType, IEnumerable<int> memberIds, double parameterValue = 0.0)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L883) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1021) | AddDirectAnalysisDefinitionParameter |
| 60 | `bool AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType parameterType, IEnumerable<Beam> members, double parameterValue = 0.0)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L886) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1034) | Delegates / composes managed operations |
| 61 | `bool AddDirectAnalysisFlexParameter(double flexFactor, IEnumerable<int> memberIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L889) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1043) | Delegates / composes managed operations |
| 62 | `bool AddDirectAnalysisAxialParameter(IEnumerable<int> memberIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L892) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1047) | Delegates / composes managed operations |
| 63 | `bool AddDirectAnalysisDefinitionParameter(DirectAnalysisParameters parameters)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L895) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1051) | Delegates / composes managed operations |
| 64 | `bool DeleteDirectAnalysisDefinitionParameter(DirectAnalysisParameterType parameterType)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L898) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1060) | DeleteDirectAnalysisDefinitionParameter |
| 65 | `bool DeleteDirectAnalysisDefinition()` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L901) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1068) | DeleteDirectAnalysisDefinition |
| 66 | `bool AddRSLoad(ResponseSpectrumLoadParameters parameters)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L904) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1072) | AddRSLoad |
| 67 | `int AddWindDefinitionASCE7Parameters(ASCE7WindDefinitionParameters parameters)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L907) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1087) | AddWindDefinitionASCE7Parameters |
| 68 | `int ComputeWallWindPressureProfile(WallWindPressureProfileParameters parameters)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L910) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1102) | ComputeWallWindPressureProfile |
| 69 | `int ComputeWallWindPressureProfileASCE72016(ASCE72016WallWindPressureProfileParameters parameters)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L913) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1115) | ComputeWallWindPressureProfileASCE72016 |
| 70 | `bool AddSeismicDefinition(SeismicCode code, bool considerAccidentalTorsion = false)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L916) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1128) | Delegates / composes managed operations |
| 71 | `bool AddSeismicDefinition(int code, int accidentalTorsion = 0)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L919) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1132) | AddSeismicDefinition |
| 72 | `int ModifySeismicDefinitionParam(string parameterName, double value)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L922) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1146) | ModifySeismicDefinitionParams |
| 73 | `int ModifySeismicDefinitionParams(IDictionary<string, double> parameters)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L925) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1154) | Delegates / composes managed operations |
| 74 | `int ModifySeismicDefinitionParams(IS1893_2016SeismicParameters parameters)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L928) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1172) | Delegates / composes managed operations |
| 75 | `int ModifySeismicDefinitionParams(IS1893_2002SeismicParameters parameters)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L931) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1179) | Delegates / composes managed operations |
| 76 | `int ModifySeismicDefinitionParams(IBCSeismicParameters parameters)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L934) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1186) | Delegates / composes managed operations |
| 77 | `bool AddSeismicDefSelfWeight(double weightFactor = 1.0)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L937) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1193) | AddSeismicDefSelfWeight |
| 78 | `int AddSeismicDefJointWeight(double weight, IEnumerable<int> nodeIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L940) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1200) | AddSeismicDefJointWeight |
| 79 | `int AddSeismicDefJointWeight(double weight, IEnumerable<Node> nodes)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L943) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1209) | Delegates / composes managed operations |
| 80 | `int AddSeismicDefJointWeight(double weight, int nodeId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L946) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1218) | Delegates / composes managed operations |
| 81 | `bool AddSeismicDefMemberWeight(int seismicCode, SeismicMemberLoadType loadType, double weight, double startDistance, double endDistance, IEnumerable<int> memberIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L949) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1221) | AddSeismicDefMemberWeight |
| 82 | `void PopulateMemberUniformlyDistributedLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L952) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1243) | Delegates / composes managed operations |
| 83 | `void PopulateMemberUniformMoments(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L955) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1250) | Delegates / composes managed operations |
| 84 | `IEnumerable<Node> PopulateNodalLoads(IEnumerable<Node> forNodes, IEnumerable<ILoadCase> fromLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L958) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1257) | Delegates / composes managed operations |
| 85 | `int AddWindDefinition(int windDefinitionId, string definitionName)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L961) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1273) | AddWindDefinition |
| 86 | `int AddWindExposure(int windDefinitionId, double exposureFactor, IEnumerable<int> nodeIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L964) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1283) | AddWindExposure |
| 87 | `int AddWindExposure(int windDefinitionId, double exposureFactor, IEnumerable<Node> nodes)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L967) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1295) | Delegates / composes managed operations |
| 88 | `int AddWindExposure(int windDefinitionId, double exposureFactor, int nodeId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L970) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1308) | Delegates / composes managed operations |
| 89 | `int AddWindIntensity(int windDefinitionId, double intensity, double height)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L973) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1317) | AddWindIntensity |
| 90 | `int AddWindIntensity(int windDefinitionId, IEnumerable<double> intensities, IEnumerable<double> heights)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L976) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1326) | AddWindIntensity |
| 91 | `int AddWindIntensity(int windDefinitionId, IEnumerable<WindIntensityPair> pairs)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L979) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1341) | Delegates / composes managed operations |
| 92 | `int DeleteWindDefinition(int windDefinitionId = 0)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L982) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1350) | DeleteWindDefinition |
| 93 | `int DeleteAllWindDefinitions()` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L985) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1357) | Delegates / composes managed operations |
| 94 | `IOSLoad AddReferenceLoadsTo(int loadCaseId, Dictionary<int, double> loadCaseNumberFactorPairs)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L988) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1381) | AddReferenceLoad |
| 95 | `IOSLoad AddRepeatLoadsTo(int loadCaseId, IDictionary<int, double> loadCaseNumberFactorPairs)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L991) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1391) | AddRepeatLoad |
| 96 | `IOSLoad AddAxialThermalGradientTo(int loadCaseId, IEnumerable<int> entities, double axialThermalGradient)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L994) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1401) | AddTemperatureLoad |
| 97 | `IOSLoad AddAxialThermalGradientTo(int loadCaseId, IEnumerable<int> entities, double axialThermalGradient, int nThreads)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L997) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1416) | AddTemperatureLoad |
| 98 | `IOSLoad AddMemberLoadsFromGroupsByLoadCase(Dictionary<ILoadCase, List<(MemberLoad Load, List<Beam> Beams)>> grouped)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1000) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1450) | Delegates / composes managed operations |
| 99 | `IOSLoad AddNodalLoadsFromGroupsByLoadCase(Dictionary<ILoadCase, List<(NodalLoad Load, List<Node> Nodes)>> grouped)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1003) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1466) | Delegates / composes managed operations |
| 100 | `void PopulateMemberLinearVaryingLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1008) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1482) | Delegates / composes managed operations |
| 101 | `IEnumerable<Beam> PopulateMemberLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1010) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1488) | Delegates / composes managed operations |
| 102 | `void PopulateMemberTrapezoidalLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1012) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1497) | Delegates / composes managed operations |
| 103 | `HashSet<ILoadCase> GetRepeatLoadCases()` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1015) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1503) | GetRepeatLoadCount |
| 104 | `void PopulateMemberConcentratedLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1017) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1516) | Delegates / composes managed operations |
| 105 | `void PopulateMemberConcentratedMoments(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1019) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1522) | Delegates / composes managed operations |
| 106 | `StaadLoadCombination GetLoadCombination(int id)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1022) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1528) | GetNoOfLoadAndFactorPairsForCombination, GetLoadCaseTitle, GetLoadAndFactorForCombination |
| 107 | `Dictionary<ILoadCase, List<(MemberLoad Load, List<Beam> Beams)>> GetMemberLoadsGroupedByLoadCase(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1024) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1560) | Delegates / composes managed operations |
| 108 | `Dictionary<ILoadCase, List<(NodalLoad Load, List<Node> Nodes)>> GetNodalLoadsGroupedByLoadCase(IEnumerable<Node> forNodes, IEnumerable<ILoadCase> fromLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1026) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1572) | Delegates / composes managed operations |
| 109 | `IOSLoad AddSelfweightInXYZTo(int loadCaseId, IEnumerable<int> entities, int nThreads, SelfWeightDirection loadDirection = SelfWeightDirection.GlobalY, double factor = -1)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1029) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1617) | Delegates / composes managed operations |
| 110 | `IOSLoad CreateLoadEnvelopExt(int envelopId, LoadEnvelopType envelopType, IEnumerable<EditorLoadCombination> loadCombinations)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1031) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1625) | Delegates / composes managed operations |
| 111 | `IOSLoad CreateLoadCombinations(IEnumerable<EditorLoadCombination> loadCombinations)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1033) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1633) | CreateNewLoadCombination, AddLoadAndFactorToCombination |
| 112 | `List<NodalLoad> GetNodalLoads(ILoadCase lc, int nId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1074) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1693) | GetNodalLoadCount, GetNodalLoads |
| 113 | `List<MemberUniformlyDistributedLoad> GetMemberUniformlyDistributedLoads(ILoadCase lc, int mId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1115) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1751) | GetUDLLoadCount, GetUDLLoads |
| 114 | `List<MemberConcentratedLoad> GetMemberConcentratedLoads(ILoadCase lc, int mId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1156) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1810) | GetConcForceCount, GetConcForces |
| 115 | `List<MemberConcentratedMoment> GetMemberConcentratedMoments(ILoadCase lc, int mId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1197) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1873) | GetConcMomentCount, GetConcMoments |
| 116 | `List<MemberLinearVaryingLoad> GetMemberLinearVaryingLoads(ILoadCase lc, int mId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1238) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1932) | GetLinearVaryingLoadCount, GetLinearVaryingLoads |
| 117 | `List<MemberTrapezoidalLoad> GetMemberTrapezoidalLoads(ILoadCase lc, int mId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1279) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L1992) | GetTrapLoadCount, GetTrapLoads |
| 118 | `bool ClearPrimaryLoadCase(ILoadCase loadCase, bool isReferenceLoad = false)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1286) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2622) | Delegates / composes managed operations |
| 119 | `bool ClearPrimaryLoadCases(IEnumerable<ILoadCase> loadCases, bool isReferenceLoad = false)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1289) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2626) | Delegates / composes managed operations |
| 120 | `bool ClearPrimaryLoadCases(IEnumerable<int> lcIds, bool isReferenceLoad = false)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1292) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2634) | ClearPrimaryLoadCase |
| 121 | `bool ClearPrimaryLoadCase(ILoadCase loadCase)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1295) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2645) | Delegates / composes managed operations |
| 122 | `bool ClearPrimaryLoadCases(IEnumerable<ILoadCase> loadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1298) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2649) | Delegates / composes managed operations |
| 123 | `bool ClearPrimaryLoadCases(IEnumerable<int> lcIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1301) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2657) | ClearPrimaryLoadCase |
| 124 | `bool ClearReferenceLoadCase(ILoadCase loadCase)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1304) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2668) | Delegates / composes managed operations |
| 125 | `bool ClearReferenceLoadCases(IEnumerable<ILoadCase> loadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1307) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2672) | Delegates / composes managed operations |
| 126 | `bool ClearReferenceLoadCases(IEnumerable<int> lcIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1310) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2680) | ClearReferenceLoadCase |
| 127 | `int CreateNewPrimaryLoad(string lcTitle, LoadType loadType)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1317) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2695) | CreateNewPrimaryLoadEx |
| 128 | `int CreateNewPrimaryLoad(int lcId, string lcTitle, LoadType loadType)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1320) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2704) | CreateNewPrimaryLoadEx2 |
| 129 | `int CreateNewPrimaryLoadEx(LoadCase lc)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1323) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2713) | Delegates / composes managed operations |
| 130 | `int CreateNewPrimaryLoadEx2(LoadCase lc)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1326) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2720) | Delegates / composes managed operations |
| 131 | `int CreateNewReferenceLoad(int lcId, string lcTitle, LoadType loadType)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1329) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2727) | CreateNewReferenceLoad |
| 132 | `int CreateNewReferenceLoad(LoadCase lc)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1332) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2736) | Delegates / composes managed operations |
| 133 | `string GetLoadCaseTitle(int id)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1335) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2743) | GetLoadCaseTitle |
| 134 | `IOSLoad SetLoadCaseActive(ILoadCase lc)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1338) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2751) | SetReferenceLoadActive, SetLoadActive |
| 135 | `IOSLoad CreateNewLoadCase(ILoadCase lc)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1341) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2769) | Delegates / composes managed operations |
| 136 | `IOSLoad AddSupportSettlement(ILoadCase lc, int nodeId, SettlementDirection direction, double mmSettlement)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1348) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2802) | Delegates / composes managed operations |
| 137 | `IOSLoad AddSupportSettlement(ILoadCase lc, Node node, SettlementDirection direction, double mmSettlement)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1351) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2808) | Delegates / composes managed operations |
| 138 | `IOSLoad AddSupportSettlement(ILoadCase lc, IEnumerable<Node> nodes, SettlementDirection direction, double mmSettlement)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1354) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2815) | Delegates / composes managed operations |
| 139 | `IOSLoad AddSupportSettlement(ILoadCase lc, IEnumerable<int> nodeIds, SettlementDirection direction, double mmSettlement)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1357) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2822) | AddSupportDisplacement |
| 140 | `IOSLoad CreatePrimaryLoadCases(HashSet<ILoadCase> primaryLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1364) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2842) | Delegates / composes managed operations |
| 141 | `IOSLoad CreatePrimaryLoadCases(IEnumerable<ILoadCase> primaryLoadCases)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1367) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2846) | Delegates / composes managed operations |
| 142 | `IOSLoad CreateReferenceLoadCases(HashSet<ILoadCase> referenceLoads)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1370) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2867) | Delegates / composes managed operations |
| 143 | `IOSLoad CreateReferenceLoadCases(IEnumerable<ILoadCase> referenceLoads)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1373) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2871) | Delegates / composes managed operations |
| 144 | `IOSLoad AddNodalLoad(ILoadCase lc, Node node, NodalLoad nl)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1380) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2896) | Delegates / composes managed operations |
| 145 | `IOSLoad AddNodalLoad(ILoadCase lc, IEnumerable<Node> nodes, NodalLoad nl)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1383) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2903) | AddNodalLoad |
| 146 | `IOSLoad AddMemberLoad(ILoadCase lc, Beam beam, MemberLoad ml)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1390) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2925) | Delegates / composes managed operations |
| 147 | `IOSLoad AddMemberLoad(ILoadCase lc, IEnumerable<Beam> beams, MemberLoad ml)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1393) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2932) | Delegates / composes managed operations |
| 148 | `IOSLoad AddPlateUniformPressure(ILoadCase lc, int plateId, double pressure, LoadDirection direction = LoadDirection.LocalZ)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1400) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2954) | Delegates / composes managed operations |
| 149 | `IOSLoad AddPlateUniformPressure(ILoadCase lc, IEnumerable<int> plateIds, double pressure, LoadDirection direction = LoadDirection.LocalZ)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1403) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2960) | AddElementPressure |
| 150 | `IOSLoad AddPlateUniformPressure(ILoadCase lc, IEnumerable<Plate> plates, double pressure, LoadDirection direction = LoadDirection.LocalZ)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1406) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2977) | Delegates / composes managed operations |
| 151 | `List<MemberUniformMoment> GetMemberUniformMoments(ILoadCase lc, int mId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1448) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2053) | GetUNIMomentCount, GetUNIMoments |
| 152 | `HashSet<ILoadCase> GetAllPrimaryLoadCases()` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1480) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2103) | Delegates / composes managed operations |
| 153 | `HashSet<ILoadCase> GetAllReferenceLoadCases()` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1512) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2135) | Delegates / composes managed operations |
| 154 | `ILoadCase GetPrimaryLoadCaseFromId(int lcId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1544) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2167) | Delegates / composes managed operations |
| 155 | `ILoadCase GetReferenceLoadCaseFromId(int lcId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1576) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2202) | Delegates / composes managed operations |
| 156 | `List<ILoadCase> GetPrimaryLoadCasesFromIds(IEnumerable<int> loadCasesIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1609) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2238) | Delegates / composes managed operations |
| 157 | `List<ILoadCase> GetReferenceLoadCasesFromIds(IEnumerable<int> loadCasesIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1642) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2280) | Delegates / composes managed operations |
| 158 | `HashSet<ILoadCase> GetAllLoadCombinationCases()` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1673) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2320) | Delegates / composes managed operations |
| 159 | `ILoadCase GetLoadCombinationCaseFromId(int lcId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1707) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2358) | Delegates / composes managed operations |
| 160 | `IEnumerable<ILoadCase> GetLoadCombinationCasesFromIds(IEnumerable<int> lcIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1744) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2401) | Delegates / composes managed operations |
| 161 | `IEnumerable<ILoadCase> GetAllLoadCasesOfType(LoadCaseType loadCaseType = LoadCaseType.PrimaryLoad)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1779) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2450) | Delegates / composes managed operations |
| 162 | `ILoadCase GetLoadCase(int lcId, LoadCaseType loadCaseType)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1815) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2494) | Delegates / composes managed operations |
| 163 | `IEnumerable<ILoadCase> GetLoadCases(int fromId, int toId, LoadCaseType loadCaseType)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1851) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2534) | Delegates / composes managed operations |
| 164 | `IEnumerable<ILoadCase> GetLoadCasesFromIds(IEnumerable<int> loadCasesIds, LoadCaseType loadCaseType)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1889) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2577) | Delegates / composes managed operations |
| 165 | `List<ILoadCase> GetLoadCasesOfType(LoadCaseType loadCaseType)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1919) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L2616) | Delegates / composes managed operations |
| 166 | `HashSet<int> GetEnvelopeIds()` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1943) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3007) | GetEnvelopeCount, GetEnvelopeIDs |
| 167 | `ILoadEnvelopeDetail GetLoadEnvelopeDetail(int envId)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L1975) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3048) | GetLoadEnvelopeDetails, GetLoadListfromLoadEnvelope |
| 168 | `HashSet<ILoadEnvelopeDetail> GetAllLoadEnvelopeDetails()` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L2000) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3090) | Delegates / composes managed operations |
| 169 | `IOSLoad AddSelfweightInXYZTo(int loadCaseId, LoadCaseType lcType, IEnumerable<int> entities, SelfWeightDirection loadDirection = SelfWeightDirection.GlobalY, double factor = -1)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L2032) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3129) | AddSelfWeightInXYZToGeometry |
| 170 | `IOSLoad CreateLoadEnvelopExt(int envelopId, LoadEnvelopType envelopType, IEnumerable<int> loadCaseList)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L2060) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3169) | CreateLoadEnvelop |
| 171 | `dynamic CreateLoadList(LoadListType loadListType, IEnumerable<int> loadCaseIds)` | [interface](../../src/StaadPro.Interop/Adapters/Interfaces/IOSLoad.cs#L2089) / [adapter](../../src/StaadPro.Interop/Adapters/Models/OSLoadAdapter.cs#L3205) | CreateLoadList |

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

The original review inspected production source without edits; subsequent increments ported all 30 originally pending reference overloads and updated this inventory. The main project remained read-only throughout. Native method presence, reviewed help and passive vendor metadata do not establish actual acquired-server compatibility, licensing, engineering correctness or package contents. No live STAAD mutation was performed. A separate local CI package passed isolated restore/build/runtime verification of 442 XML examples with native mutation guards disabled; the prepared distribution archive is unchanged. See the primary report for verification gates and unresolved contracts.
