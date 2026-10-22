# OpenSTAAD Geometry native contract inventory and coverage annex

**Review date:** 22 October 2026. **Source baseline:** `1483c9c`. This annex supports the [multi-version comparison and architecture plan](OpenSTAAD-Geometry-Multi-Version-Architecture-and-Plan.md). It is an inventory and review plan, not implemented new APIs or a native runtime certification.

The 135 documented names below occur in all three references. There are 134 detailed entries and the deprecated summary-only CreatePMember alias. D means a native dispatch is present in the current adapter; N means this exact native name is not dispatched. Neither label establishes that the operation succeeded on a live model.

Parameter direction is taken from the 2026 help, whose parameter-direction/name tables match the 2024 and 2025 tables. The CLR slot shape comes from the matching installed 2025/2026 vendor interfaces; Object represents an Automation value, not a promise of a particular scalar or array layout. C++ FAR reference notation must not be used mechanically as a managed ref/out instruction.

Return labels list the documented value categories/codes, without inventing a success predicate for an omitted table. A prose Returns section may exist even when no return-code table is provided. CreateGroupEx and GetPlateNodeCount have incomplete parameter tables; missing direction entries are deliberately shown as not stated. The primary report explains the meaning of all version-dependent changes. Consult the linked local help for the original detailed contract and examples.

## 1. Complete documented native inventory

### General section

#### DoTranslationalRepeat

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___geometry_group.html#ga12070670794760edbc6b6ad41d62d078) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___geometry_group.html#ga12070670794760edbc6b6ad41d62d078) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___geometry_group.html#ga12070670794760edbc6b6ad41d62d078). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::DoTranslationalRepeat(
    const VARIANT FAR& varLinkBays,
    const VARIANT FAR& varOpenBase,
    const VARIANT FAR& varAxisDir,
    const VARIANT FAR& varSpacingArray,
    const VARIANT FAR& varNoBays,
    const VARIANT FAR& varRenumberBay,
    const VARIANT FAR& varRenumberArray,
    const VARIANT FAR& varGeometryOnly);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `varLinkBays` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varOpenBase` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varAxisDir` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varSpacingArray` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varNoBays` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `varRenumberBay` | `const VARIANT FAR&` | [in] | `System.Object` |
| 7 | `varRenumberArray` | `const VARIANT FAR&` | [in] | `System.Object` |
| 8 | `varGeometryOnly` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `1`, `0`.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### SetFlagForHiddenEntities

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___geometry_group.html#ga2932ced8b91c04c2150a04b832cb4fdc) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___geometry_group.html#ga2932ced8b91c04c2150a04b832cb4fdc) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___geometry_group.html#ga2932ced8b91c04c2150a04b832cb4fdc). **Comparison:** Common. **Current dispatch:** N.

```cpp
void OSGeometryUI::SetFlagForHiddenEntities(
    const VARIANT FAR& nFlagValue);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nFlagValue` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Review disposition:** G1/G3: bound context and explicit query scope. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetFlagForHiddenEntities

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___geometry_group.html#ga5645f5cee0257a2f8c26563666c227d3) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___geometry_group.html#ga5645f5cee0257a2f8c26563666c227d3) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___geometry_group.html#ga5645f5cee0257a2f8c26563666c227d3). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetFlagForHiddenEntities();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `nFlagValue`.

**Review disposition:** G1/G3: bound context and explicit query scope. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### HasHiddenEntities

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___geometry_group.html#gad36e1400b2660aa9eb13f31c15a81c4a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___geometry_group.html#gad36e1400b2660aa9eb13f31c15a81c4a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___geometry_group.html#gad36e1400b2660aa9eb13f31c15a81c4a). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::HasHiddenEntities();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `1`, `0`.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

### Node section

#### CreateNode

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#gadede1ccb23b89e1e1bb8539dc1b62c3a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#gadede1ccb23b89e1e1bb8539dc1b62c3a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#gadede1ccb23b89e1e1bb8539dc1b62c3a). **Comparison:** Common. **Current dispatch:** D.

```cpp
void OSGeometryUI::CreateNode(
    const VARIANT FAR& nNodeNo,
    const VARIANT FAR& fCoordX,
    const VARIANT FAR& fCoordY,
    const VARIANT FAR& fCoordZ);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `fCoordX` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `fCoordY` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `fCoordZ` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Current callers:** `CreateNode(Node)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L213)); `CreateMultipleNodes(HashSet<Node>, int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L856)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G6: controlled native mutations.

#### AddNode

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#ga99d586496fdc2fe5f96f70c592816177) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#ga99d586496fdc2fe5f96f70c592816177) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#ga99d586496fdc2fe5f96f70c592816177). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::AddNode(
    const VARIANT FAR& fCoordX,
    const VARIANT FAR& fCoordY,
    const VARIANT FAR& fCoordZ);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `fCoordX` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `fCoordY` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `fCoordZ` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<Val>`, `0`, `-2004`.

**Current callers:** `AddNode(Node)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L633)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G6: controlled native mutations.

#### DeleteNode

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#ga17d3a7ce308b8db04af97cd5bb45d15c) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#ga17d3a7ce308b8db04af97cd5bb45d15c) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#ga17d3a7ce308b8db04af97cd5bb45d15c). **Comparison:** Common. **Current dispatch:** D.

```cpp
void OSGeometryUI::DeleteNode(
    const VARIANT FAR& nNodeNo);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeNo` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Current callers:** `DeleteAllNodes(int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L908)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G6: controlled native mutations.

#### GetLastNodeNo

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#gab1d3939f78bfba9f4ec380b8da0ce936) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#gab1d3939f78bfba9f4ec380b8da0ce936) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#gab1d3939f78bfba9f4ec380b8da0ce936). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetLastNodeNo();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<Val>`, `-1`.

**Current callers:** `GetLastNodeNo()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L498)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### GetNoOfSelectedNodes

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#gaf8eac5db1d9fc7866221e2e3908a168a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#gaf8eac5db1d9fc7866221e2e3908a168a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#gaf8eac5db1d9fc7866221e2e3908a168a). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetNoOfSelectedNodes();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `The`.

**Current callers:** `AnyItemsSelected()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L505)); `GetSelectedNodesIds()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L575)); `GetSelectedNodesListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1177)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G2: selection contracts and entity support.

#### GetSelectedNodes

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#ga0178f5f0ab53cb5a52002770e5d97985) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#ga0178f5f0ab53cb5a52002770e5d97985) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#ga0178f5f0ab53cb5a52002770e5d97985). **Comparison:** Common. **Current dispatch:** D.

```cpp
void OSGeometryUI::GetSelectedNodes(
    VARIANT FAR& naNodeNos,
    VARIANT FAR& nIsSorted);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `naNodeNos` | `VARIANT FAR&` | [out] | `System.Object&` |
| 2 | `nIsSorted` | `VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Current callers:** `GetSelectedNodesIds()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L575)); `GetSelectedNodesListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1177)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G2: selection contracts and entity support.

#### GetNodeCoordinates

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#ga3105f93484eca2596a659bf68cddf66c) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#ga3105f93484eca2596a659bf68cddf66c) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#ga3105f93484eca2596a659bf68cddf66c). **Comparison:** Common. **Current dispatch:** D.

```cpp
void OSGeometryUI::GetNodeCoordinates(
    const VARIANT FAR& nNodeNo,
    VARIANT FAR& fCoordX,
    VARIANT FAR& fCoordY,
    VARIANT FAR& fCoordZ);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `fCoordX` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `fCoordY` | `VARIANT FAR&` | [out] | `System.Object&` |
| 4 | `fCoordZ` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Current callers:** `GetNode(int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L737)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### SetNodeCoordinate

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#ga148833fe28dae09d77681d9fc928bd27) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#ga148833fe28dae09d77681d9fc928bd27) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#ga148833fe28dae09d77681d9fc928bd27). **Comparison:** Common. **Current dispatch:** D.

```cpp
void OSGeometryUI::SetNodeCoordinate(
    const VARIANT FAR& nNodeNo,
    const VARIANT FAR& fCoordX,
    const VARIANT FAR& fCoordY,
    const VARIANT FAR& fCoordZ);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `fCoordX` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `fCoordY` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `fCoordZ` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Current callers:** `SetNodeCoordinate(int, double, double, double)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L495)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G6: controlled native mutations.

#### GetNodeNumber

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#ga6c05f160cc71e4d0de33ad784c8e3357) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#ga6c05f160cc71e4d0de33ad784c8e3357) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#ga6c05f160cc71e4d0de33ad784c8e3357). **Comparison:** Status policy. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetNodeNumber(
    const VARIANT FAR& fCoordX,
    const VARIANT FAR& fCoordY,
    const VARIANT FAR& fCoordZ);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `fCoordX` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `fCoordY` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `fCoordZ` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** 2024 — `<Val>`, `-1`; 2025/2026 — `<Val>`, `-2001`, `-1`.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetNodeDistance

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#gae103e955a853451c5a7abc6393d1267e) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#gae103e955a853451c5a7abc6393d1267e) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#gae103e955a853451c5a7abc6393d1267e). **Comparison:** Status policy. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetNodeDistance(
    const VARIANT FAR& nNodeNoA,
    const VARIANT FAR& nNodeNoB);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeNoA` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `nNodeNoB` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** 2024 — `<Val>`, `-1`; 2025/2026 — `<Val>`, `-1`, `-2001`.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### SelectMultipleNodes

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#gacdf9f9ada351e906981bf8a993119e38) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#gacdf9f9ada351e906981bf8a993119e38) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#gacdf9f9ada351e906981bf8a993119e38). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::SelectMultipleNodes(
    const VARIANT FAR& naNodeNos);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `naNodeNos` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `0`, `1`.

**Current callers:** `SelectMultipleNodes(IEnumerable<int>)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L484)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G2: selection contracts and entity support.

#### SelectNode

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#gabe35568c795c6380e74040ac30399368) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#gabe35568c795c6380e74040ac30399368) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#gabe35568c795c6380e74040ac30399368). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::SelectNode(
    const VARIANT FAR& nNodeNo);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeNo` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** No return table provided.

**Review disposition:** G2: selection contracts and entity support. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### AddMultipleNodes

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#gadca7c14419a3a1bfea9bada5260cc262) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#gadca7c14419a3a1bfea9bada5260cc262) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#gadca7c14419a3a1bfea9bada5260cc262). **Comparison:** Common. **Current dispatch:** N.

```cpp
void OSGeometryUI::AddMultipleNodes(
    const VARIANT FAR& faCoordinates);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `faCoordinates` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### CreateMultipleNodes

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#ga7cb1897a94230abce092204ea1f0eec1) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#ga7cb1897a94230abce092204ea1f0eec1) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#ga7cb1897a94230abce092204ea1f0eec1). **Comparison:** Common. **Current dispatch:** D.

```cpp
void OSGeometryUI::CreateMultipleNodes(
    const VARIANT FAR& nNodeIdArray,
    const VARIANT FAR& dCoordArray);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeIdArray` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `dCoordArray` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Current callers:** `CreateMultipleNodesExtended(IOSRoot, IEnumerable<Node>)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L83)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G6: controlled native mutations.

#### GetNodeCount

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#ga2f3966e116a500cb292d61e5dc97e1bd) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#ga2f3966e116a500cb292d61e5dc97e1bd) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#ga2f3966e116a500cb292d61e5dc97e1bd). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetNodeCount();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<Val>`.

**Current callers:** `GetAllNodesList()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L788)); `GetAllNodesListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1150)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### GetNodeList

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#ga2ad1d66e6248fcb0be49724baba64ffb) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#ga2ad1d66e6248fcb0be49724baba64ffb) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#ga2ad1d66e6248fcb0be49724baba64ffb). **Comparison:** Common. **Current dispatch:** D.

```cpp
void OSGeometryUI::GetNodeList(
    VARIANT FAR& nNodeList);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeList` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Current callers:** `GetAllNodesList()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L788)); `GetAllNodesListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1150)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### GetNodeIncidence

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#gae5e798eb1f40c74f3989f3bd6966c4aa) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#gae5e798eb1f40c74f3989f3bd6966c4aa) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#gae5e798eb1f40c74f3989f3bd6966c4aa). **Comparison:** Clarification. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetNodeIncidence(
    const VARIANT FAR& nNodeNo,
    VARIANT FAR& fCoordX,
    VARIANT FAR& fCoordY,
    VARIANT FAR& fCoordZ);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `fCoordX` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `fCoordY` | `VARIANT FAR&` | [out] | `System.Object&` |
| 4 | `fCoordZ` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `0`, `-1`, `-2001`.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### ClearNodeSelection

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#ga6e40fd0051358ad1252de5a2bba44e54) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#ga99aa626353f9ffe5abf8241ef7108e11) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#ga99aa626353f9ffe5abf8241ef7108e11). **Comparison:** Return shape. **Current dispatch:** D.

**2024:** `void ClearNodeSelection()`. **2025/2026:** `long ClearNodeSelection()`.

**Parameters:** none.

**Return declaration:** see the version distinction above. **Inspected CLR return (2025/2026):** `System.Int32`.

**Return labels:** 2024 — No return table provided; 2025/2026 — `0`, `-1`, `-2005`.

**Current callers:** `ClearNodeSelection()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L462)).

**Review disposition:** Apply the version-sensitive contract workstream before claiming qualified native success. G2: selection contracts and entity support.

#### SetNodeUniqueID

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#ga659e31b5410b50e08bcc659e73d0ee15) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#ga659e31b5410b50e08bcc659e73d0ee15) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#ga659e31b5410b50e08bcc659e73d0ee15). **Comparison:** Common. **Current dispatch:** N.

```cpp
void OSGeometryUI::SetNodeUniqueID(
    const VARIANT FAR& nNodeNo,
    const VARIANT FAR& szName);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `szName` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Review disposition:** G7: identity and exchange metadata. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetNodeUniqueID

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#gab6f65fdc91d066325086f92ab30a9fe3) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#gab6f65fdc91d066325086f92ab30a9fe3) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#gab6f65fdc91d066325086f92ab30a9fe3). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetNodeUniqueID(
    const VARIANT FAR& nNodeNo);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeNo` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<VARIANT>`.

**Review disposition:** G7: identity and exchange metadata. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetNodeIncidence_CIS2

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#ga8fd3b6508a89f4092ea54c6d54adceb0) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#ga8fd3b6508a89f4092ea54c6d54adceb0) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#ga8fd3b6508a89f4092ea54c6d54adceb0). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetNodeIncidence_CIS2(
    const VARIANT FAR& nNodeNo,
    VARIANT FAR& szName,
    VARIANT FAR& fCoordX,
    VARIANT FAR& fCoordY,
    VARIANT FAR& fCoordZ);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `szName` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `fCoordX` | `VARIANT FAR&` | [out] | `System.Object&` |
| 4 | `fCoordY` | `VARIANT FAR&` | [out] | `System.Object&` |
| 5 | `fCoordZ` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `0`, `-1`, `-2001`.

**Review disposition:** G7: identity and exchange metadata. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### IsOrphanNode

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#gadd56dd50d016c8a219e129f6076ec36a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#gadd56dd50d016c8a219e129f6076ec36a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#gadd56dd50d016c8a219e129f6076ec36a). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::IsOrphanNode(
    const VARIANT FAR& varNodeNo);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `varNodeNo` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `1`, `0`.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### MergeNodes

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html#gac23ef3fbcd22a260c576cd78c3993fd5) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html#gac23ef3fbcd22a260c576cd78c3993fd5) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html#gac23ef3fbcd22a260c576cd78c3993fd5). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::MergeNodes(
    const VARIANT FAR& varNodeNo,
    const VARIANT FAR& nNodeIdArray);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `varNodeNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `nNodeIdArray` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `1`, `0`.

**Current callers:** `MergeNodesExtended(IOSRoot, Node, IEnumerable<Node>)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L111)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G6: controlled native mutations.

### Beam section

#### CreateBeam

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga0db25e622b229b79fed304db8cc9ceb1) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga0db25e622b229b79fed304db8cc9ceb1) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga0db25e622b229b79fed304db8cc9ceb1). **Comparison:** Common. **Current dispatch:** D.

```cpp
void OSGeometryUI::CreateBeam(
    const VARIANT FAR& nBeamNo,
    const VARIANT FAR& nNodeA,
    const VARIANT FAR& nNodeB);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `nNodeA` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `nNodeB` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Current callers:** `CreateMultipleBeamsExtended(IOSRoot, IEnumerable<Beam>)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L51)); `CreateBeam(Beam)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L220)); `CreateMultipleBeams(HashSet<Beam>, int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L863)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G6: controlled native mutations.

#### AddBeam

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#gaa80b6e8c60ee521abc18adef10db331e) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#gaa80b6e8c60ee521abc18adef10db331e) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#gaa80b6e8c60ee521abc18adef10db331e). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::AddBeam(
    const VARIANT FAR& nNodeA,
    const VARIANT FAR& nNodeB);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeA` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `nNodeB` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<Val>`, `-1`, `-2001`.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### DeleteBeam

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga21e519d3d6f431f8a2eceae35159bd8b) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga21e519d3d6f431f8a2eceae35159bd8b) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga21e519d3d6f431f8a2eceae35159bd8b). **Comparison:** Common. **Current dispatch:** D.

```cpp
void OSGeometryUI::DeleteBeam(
    const VARIANT FAR& nBeamNo);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Current callers:** `DeleteAllBeams(int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L901)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G6: controlled native mutations.

#### SplitBeam

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#gaf548964ec30978c451b96592de2c22b4) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#gaf548964ec30978c451b96592de2c22b4) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#gaf548964ec30978c451b96592de2c22b4). **Comparison:** Common. **Current dispatch:** N.

```cpp
void OSGeometryUI::SplitBeam(
    const VARIANT FAR& nBeamNo,
    const VARIANT FAR& nNodes,
    const VARIANT FAR& faDistToNodes);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `nNodes` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `faDistToNodes` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### SplitBeamInEqlParts

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga86c90a78d70f4f7686b58e02187956b7) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga86c90a78d70f4f7686b58e02187956b7) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga86c90a78d70f4f7686b58e02187956b7). **Comparison:** Common. **Current dispatch:** N.

```cpp
void OSGeometryUI::SplitBeamInEqlParts(
    const VARIANT FAR& nBeamNo,
    const VARIANT FAR& nParts);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `nParts` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetLastBeamNo

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga5ff6be60637bbe6b6dbb6662bf1eadb0) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga5ff6be60637bbe6b6dbb6662bf1eadb0) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga5ff6be60637bbe6b6dbb6662bf1eadb0). **Comparison:** Clarification. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetLastBeamNo();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<Val>`, `-1`.

**Current callers:** `GetLastBeamNo()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L500)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### GetNoOfSelectedBeams

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga8ef4cf38d6e94967828ea887d65e1acf) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga8ef4cf38d6e94967828ea887d65e1acf) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga8ef4cf38d6e94967828ea887d65e1acf). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetNoOfSelectedBeams();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `The`.

**Current callers:** `AnyItemsSelected()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L505)); `GetSelectedBeamsIds()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L539)); `GetSelectedBeamsListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1168)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G2: selection contracts and entity support.

#### GetSelectedBeams

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga4a9f2582f4d749d0f958087792042a1d) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga4a9f2582f4d749d0f958087792042a1d) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga4a9f2582f4d749d0f958087792042a1d). **Comparison:** Common. **Current dispatch:** D.

```cpp
void OSGeometryUI::GetSelectedBeams(
    VARIANT FAR& naBeamNos,
    VARIANT FAR& nIsSorted);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `naBeamNos` | `VARIANT FAR&` | [out] | `System.Object&` |
| 2 | `nIsSorted` | `VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Current callers:** `GetSelectedBeamsIds()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L539)); `GetSelectedBeamsListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1168)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G2: selection contracts and entity support.

#### GetBeamLength

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga705ebaf73ae58b2d4f67b02bdbfb75c8) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga705ebaf73ae58b2d4f67b02bdbfb75c8) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga705ebaf73ae58b2d4f67b02bdbfb75c8). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetBeamLength(
    const VARIANT FAR& nBeamNo);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<Val>`, `0`.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### SelectMultipleBeams

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga70d22e97024806f372426b855da286d0) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga70d22e97024806f372426b855da286d0) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga70d22e97024806f372426b855da286d0). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::SelectMultipleBeams(
    const VARIANT FAR& naBeamNos);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `naBeamNos` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `0`, `1`.

**Current callers:** `SelectMultipleBeams(IEnumerable<int>)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L486)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G2: selection contracts and entity support.

#### SelectBeam

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga8b0ce15bd30c7b31ef336c243ee7d1bb) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga8b0ce15bd30c7b31ef336c243ee7d1bb) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga8b0ce15bd30c7b31ef336c243ee7d1bb). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::SelectBeam(
    const VARIANT FAR& nBeamNo);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** No return table provided.

**Review disposition:** G2: selection contracts and entity support. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### AddMultipleBeams

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#gaf857067eb642076b8202cec4f0df1b09) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#gaf857067eb642076b8202cec4f0df1b09) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#gaf857067eb642076b8202cec4f0df1b09). **Comparison:** Common. **Current dispatch:** N.

```cpp
void OSGeometryUI::AddMultipleBeams(
    const VARIANT FAR& naIncidences);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `naIncidences` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### SetCheckForIdenticalEntity

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga3da5d18cd82dd747b5f7331e3003e2de) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga3da5d18cd82dd747b5f7331e3003e2de) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga3da5d18cd82dd747b5f7331e3003e2de). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::SetCheckForIdenticalEntity(
    long entityType,
    long bEnable);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `entityType` | `long` | [in] | `System.Int32` |
| 2 | `bEnable` | `long` | [in] | `System.Int32` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `0`.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### CreateMultipleBeams

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga24fd360103cd5e261d145ed5e91e045b) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga24fd360103cd5e261d145ed5e91e045b) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga24fd360103cd5e261d145ed5e91e045b). **Comparison:** Common. **Current dispatch:** N.

```cpp
void OSGeometryUI::CreateMultipleBeams(
    const VARIANT FAR& nBeamIdArray,
    const VARIANT FAR& nBeamIncidenceArray);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamIdArray` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `nBeamIncidenceArray` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Review disposition:** G6: retain existing serial workaround; native bulk dispatch requires separate evidence. CreateMultipleBeamsExtended already provides ordered serial CreateBeam requests. This is not a verified native bulk call.

#### GetMemberCount

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga949e2df9a040dfc277e4ec2a73b1a800) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga949e2df9a040dfc277e4ec2a73b1a800) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga949e2df9a040dfc277e4ec2a73b1a800). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetMemberCount();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<Val>`.

**Current callers:** `GetAllBeamsList()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L779)); `GetAllBeamsListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1141)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### GetBeamList

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga5e2082b8d7be7ba22d7b6f13081b9be6) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga5e2082b8d7be7ba22d7b6f13081b9be6) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga5e2082b8d7be7ba22d7b6f13081b9be6). **Comparison:** Common. **Current dispatch:** D.

```cpp
void OSGeometryUI::GetBeamList(
    VARIANT FAR& nBeamList);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamList` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Current callers:** `GetAllBeamsList()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L779)); `GetAllBeamsListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1141)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### GetMemberIncidence

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga5f3c5f19ca82ed9dde4c463534d44c10) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga5f3c5f19ca82ed9dde4c463534d44c10) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga5f3c5f19ca82ed9dde4c463534d44c10). **Comparison:** Clarification. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetMemberIncidence(
    const VARIANT FAR& nBeamNo,
    VARIANT FAR& nNodeA,
    VARIANT FAR& nNodeB);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `nNodeA` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `nNodeB` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `0`, `-1`, `-3001`.

**Current callers:** `GetBeam(int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L709)); `GetBeam(int, IEnumerable<Node>)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L720)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### ClearMemberSelection

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga5a0669ba75ec602204b0ac7e3c14ec0a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga761c068531727a05ba07c3379154b9fe) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga761c068531727a05ba07c3379154b9fe). **Comparison:** Return shape. **Current dispatch:** D.

**2024:** `void ClearMemberSelection()`. **2025/2026:** `long ClearMemberSelection()`.

**Parameters:** none.

**Return declaration:** see the version distinction above. **Inspected CLR return (2025/2026):** `System.Int32`.

**Return labels:** 2024 — No return table provided; 2025/2026 — `0`, `-1`, `-3005`.

**Current callers:** `ClearMemberSelection()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L464)).

**Review disposition:** Apply the version-sensitive contract workstream before claiming qualified native success. G2: selection contracts and entity support.

#### SetMemberUniqueID

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga40eea400ba8ff5c69836b30d28657e21) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga40eea400ba8ff5c69836b30d28657e21) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga40eea400ba8ff5c69836b30d28657e21). **Comparison:** Common. **Current dispatch:** N.

```cpp
void OSGeometryUI::SetMemberUniqueID(
    const VARIANT FAR& nMembNo,
    const VARIANT FAR& szName);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nMembNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `szName` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Review disposition:** G7: identity and exchange metadata. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetMemberUniqueID

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga81e60a3d395a4fdaaae305a3be5414e9) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga81e60a3d395a4fdaaae305a3be5414e9) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga81e60a3d395a4fdaaae305a3be5414e9). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetMemberUniqueID(
    const VARIANT FAR& nMembNo);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nMembNo` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<VARIANT>`.

**Review disposition:** G7: identity and exchange metadata. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetMemberIncidence_CIS2

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga8faf66aa6cd624ed6488885c105d524a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga8faf66aa6cd624ed6488885c105d524a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga8faf66aa6cd624ed6488885c105d524a). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetMemberIncidence_CIS2(
    const VARIANT FAR& nBeamNo,
    VARIANT FAR& szName,
    VARIANT FAR& nNodeA,
    VARIANT FAR& nNodeB);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `szName` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `nNodeA` | `VARIANT FAR&` | [out] | `System.Object&` |
| 4 | `nNodeB` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `0`, `-1`, `-3001`.

**Review disposition:** G7: identity and exchange metadata. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### IsZUp

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga54c14fbd6b433b1cb19c6a55aea0cc5b) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga54c14fbd6b433b1cb19c6a55aea0cc5b) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga54c14fbd6b433b1cb19c6a55aea0cc5b). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::IsZUp();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `1`, `0`.

**Current callers:** `IsZUp()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L179)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### IsBeam

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga0fd98a4e5f1e658513c6a521765a82f9) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga0fd98a4e5f1e658513c6a521765a82f9) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga0fd98a4e5f1e658513c6a521765a82f9). **Comparison:** Status policy. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::IsBeam(
    const VARIANT FAR& nMemberNo,
    const VARIANT FAR& dTolAngle);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nMemberNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `dTolAngle` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** 2024 — `1`, `0`, `-2001`, `-3001`; 2025/2026 — `1`, `0`, `-3001`.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### IsColumn

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga8250cc536dbce64de7ddb1d7915b2e4b) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga8250cc536dbce64de7ddb1d7915b2e4b) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga8250cc536dbce64de7ddb1d7915b2e4b). **Comparison:** Status policy. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::IsColumn(
    const VARIANT FAR& nMemberNo,
    const VARIANT FAR& dTolAngle);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nMemberNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `dTolAngle` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** 2024 — `1`, `0`, `-2001`, `-3001`; 2025/2026 — `1`, `0`, `-3001`.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetNoOfBeamsConnectedAtNode

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga67204dfa35cf10b102e7dd88b06b2d93) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga67204dfa35cf10b102e7dd88b06b2d93) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga67204dfa35cf10b102e7dd88b06b2d93). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetNoOfBeamsConnectedAtNode(
    const VARIANT FAR& nNodeNo);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeNo` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** No return table provided.

**Current callers:** `GetBeamsConnectedAtNode(Node, int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1072)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### GetBeamsConnectedAtNode

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#gaaaf0f9e0a46deb41c36d037d358763f7) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#gaaaf0f9e0a46deb41c36d037d358763f7) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#gaaaf0f9e0a46deb41c36d037d358763f7). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetBeamsConnectedAtNode(
    const VARIANT FAR& nNodeNo,
    VARIANT FAR& nBeamList);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `nBeamList` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** No return table provided.

**Current callers:** `GetBeamsConnectedAtNode(Node, int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1072)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### RenumberBeam

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga4904dd27eaff80af6cc3de5532a31448) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga4904dd27eaff80af6cc3de5532a31448) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga4904dd27eaff80af6cc3de5532a31448). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::RenumberBeam(
    const VARIANT FAR& varBeamNoOld,
    const VARIANT FAR& varBeamNoNew);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `varBeamNoOld` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varBeamNoNew` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `1`, `0`.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### MergeBeams

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#gaae5c43ab7eaf3f65eac9d18e579f8208) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#gaae5c43ab7eaf3f65eac9d18e579f8208) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#gaae5c43ab7eaf3f65eac9d18e579f8208). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::MergeBeams(
    const VARIANT FAR& nBeamIdArray,
    const VARIANT FAR& varBeamNo,
    const VARIANT FAR& varPropertyNo,
    const VARIANT FAR& varBetaAngle,
    const VARIANT FAR& varMaterialName);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nBeamIdArray` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varBeamNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varPropertyNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varBetaAngle` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varMaterialName` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `1`, `0`.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### IntersectBeams

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga5baa2ac3c82eadbcc8bd6cf9d4320692) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga5baa2ac3c82eadbcc8bd6cf9d4320692) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga5baa2ac3c82eadbcc8bd6cf9d4320692). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::IntersectBeams(
    const VARIANT FAR& Method,
    const VARIANT FAR& BeamNosArray,
    const VARIANT FAR& varTolerance,
    VARIANT FAR& NewBeamNosArray);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `Method` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `BeamNosArray` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varTolerance` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `NewBeamNosArray` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `0`, `1`.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetCountOfBreakableBeamsAtSpecificNodes

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga980459aa4994655dc5df5b7555a7164a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga980459aa4994655dc5df5b7555a7164a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga980459aa4994655dc5df5b7555a7164a). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetCountOfBreakableBeamsAtSpecificNodes(
    const VARIANT FAR& nNodeIdArray);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeIdArray` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `returns`.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### BreakBeamsAtSpecificNodes

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga31d6570cc8be3e733d8115b495aaf0a1) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga31d6570cc8be3e733d8115b495aaf0a1) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga31d6570cc8be3e733d8115b495aaf0a1). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::BreakBeamsAtSpecificNodes(
    const VARIANT FAR& nNodeIdArray,
    VARIANT FAR& nBrokenBeamIdArray,
    VARIANT FAR& nNewBeamIdArray);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeIdArray` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `nBrokenBeamIdArray` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `nNewBeamIdArray` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `1`, `0`.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetIntersectBeamsCount

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html#ga9d7a9d731e091e7d188e319a0c38f599) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html#ga9d7a9d731e091e7d188e319a0c38f599) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html#ga9d7a9d731e091e7d188e319a0c38f599). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetIntersectBeamsCount(
    const VARIANT FAR& BeamNosArray,
    const VARIANT FAR& varTolerance);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `BeamNosArray` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varTolerance` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `returns`.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

### Physical Member section

#### SetPhysicalMemberUniqueID

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___phy_mem_group.html#gae86d014395f43e4442a81a1254b82ad5) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___phy_mem_group.html#gae86d014395f43e4442a81a1254b82ad5) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___phy_mem_group.html#gae86d014395f43e4442a81a1254b82ad5). **Comparison:** Common. **Current dispatch:** N.

```cpp
void OSGeometryUI::SetPhysicalMemberUniqueID(
    const VARIANT FAR& nPhyMembNo,
    const VARIANT FAR& szGUID);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nPhyMembNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `szGUID` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Review disposition:** G7: identity and exchange metadata. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetPhysicalMemberUniqueID

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___phy_mem_group.html#ga4b41f2f7d54507ee9ec436cb0712d719) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___phy_mem_group.html#ga4b41f2f7d54507ee9ec436cb0712d719) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___phy_mem_group.html#ga4b41f2f7d54507ee9ec436cb0712d719). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetPhysicalMemberUniqueID(
    const VARIANT FAR& nPhyMembNo);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nPhyMembNo` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<VARIANT>`.

**Review disposition:** G7: identity and exchange metadata. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### CreatePhysicalMember

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___phy_mem_group.html#gab8ad4c9d8f3b16d93daefd8738683a1c) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___phy_mem_group.html#gab8ad4c9d8f3b16d93daefd8738683a1c) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___phy_mem_group.html#gab8ad4c9d8f3b16d93daefd8738683a1c). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::CreatePhysicalMember(
    const VARIANT FAR& nAnalyticalMembers,
    const VARIANT FAR& MemberList,
    const VARIANT FAR& MemberAttribute);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nAnalyticalMembers` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `MemberList` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `MemberAttribute` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<val>`, `0`.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### DeletePhysicalMember

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___phy_mem_group.html#ga9986e3152d51b12a9235f3ff3d24996a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___phy_mem_group.html#ga9986e3152d51b12a9235f3ff3d24996a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___phy_mem_group.html#ga9986e3152d51b12a9235f3ff3d24996a). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::DeletePhysicalMember(
    const VARIANT FAR& nPhyMembNo);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nPhyMembNo` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `1`, `0`.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetAnalyticalMemberCountForPhysicalMember

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___phy_mem_group.html#ga22edd8693b535084365b01159b977a9a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___phy_mem_group.html#ga22edd8693b535084365b01159b977a9a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___phy_mem_group.html#ga22edd8693b535084365b01159b977a9a). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetAnalyticalMemberCountForPhysicalMember(
    const VARIANT FAR& nPhyMembNo);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nPhyMembNo` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<Val>`, `-1`.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetAnalyticalMembersForPhysicalMember

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___phy_mem_group.html#ga6b56b1d6311c6cfd8bee94074e55d06c) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___phy_mem_group.html#ga6b56b1d6311c6cfd8bee94074e55d06c) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___phy_mem_group.html#ga6b56b1d6311c6cfd8bee94074e55d06c). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetAnalyticalMembersForPhysicalMember(
    const VARIANT FAR& nPhyMembNo,
    const VARIANT FAR& nAnalyticalMember,
    VARIANT FAR& MemberList);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nPhyMembNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `nAnalyticalMember` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `MemberList` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `0`, `-107`, `-108`.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetLastPhysicalMemberNo

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___phy_mem_group.html#ga290a478ba0f5994046ab87de93c25c7d) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___phy_mem_group.html#ga290a478ba0f5994046ab87de93c25c7d) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___phy_mem_group.html#ga290a478ba0f5994046ab87de93c25c7d). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetLastPhysicalMemberNo();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<Val>`, `-1`.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetNoOfSelectedPhysicalMembers

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___phy_mem_group.html#gaf01a150e6d9856217982959867682af4) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___phy_mem_group.html#gaf01a150e6d9856217982959867682af4) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___phy_mem_group.html#gaf01a150e6d9856217982959867682af4). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetNoOfSelectedPhysicalMembers();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<Val>`, `-1`.

**Current callers:** `AnyItemsSelected()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L505)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G2: selection contracts and entity support.

#### GetSelectedPhysicalMembers

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___phy_mem_group.html#ga5a37ffddd8695df6ac2ed1603e561e8d) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___phy_mem_group.html#ga5a37ffddd8695df6ac2ed1603e561e8d) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___phy_mem_group.html#ga5a37ffddd8695df6ac2ed1603e561e8d). **Comparison:** Common. **Current dispatch:** N.

```cpp
void OSGeometryUI::GetSelectedPhysicalMembers(
    VARIANT FAR& naPhyMembNos,
    const VARIANT& nIsSorted);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `naPhyMembNos` | `VARIANT FAR&` | [out] | `System.Object&` |
| 2 | `nIsSorted` | `const VARIANT&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Review disposition:** G2: selection contracts and entity support. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### SelectPhysicalMember

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___phy_mem_group.html#ga05bb8c246f4f1859d82f4dea93e823c1) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___phy_mem_group.html#ga05bb8c246f4f1859d82f4dea93e823c1) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___phy_mem_group.html#ga05bb8c246f4f1859d82f4dea93e823c1). **Comparison:** Common. **Current dispatch:** N.

```cpp
void OSGeometryUI::SelectPhysicalMember(
    const VARIANT FAR& nPhyMembNo);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nPhyMembNo` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Review disposition:** G2: selection contracts and entity support. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### SelectMultiplePhysicalMembers

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___phy_mem_group.html#gab70eb230045bdeeff2b1804152c297d6) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___phy_mem_group.html#gab70eb230045bdeeff2b1804152c297d6) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___phy_mem_group.html#gab70eb230045bdeeff2b1804152c297d6). **Comparison:** Common. **Current dispatch:** D.

```cpp
void OSGeometryUI::SelectMultiplePhysicalMembers(
    const VARIANT FAR& nPhyMembNos);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nPhyMembNos` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Current callers:** `SelectMultiplePhysicalMembers(IEnumerable<int>)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L492)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G2: selection contracts and entity support.

#### GetPhysicalMemberCount

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___phy_mem_group.html#gacd5015175741fdbee9a2ffc0280a314a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___phy_mem_group.html#gacd5015175741fdbee9a2ffc0280a314a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___phy_mem_group.html#gacd5015175741fdbee9a2ffc0280a314a). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetPhysicalMemberCount();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<Val>`, `-1`.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetPhysicalMemberList

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___phy_mem_group.html#ga92d1db4b1a397d5cb92eea38773773be) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___phy_mem_group.html#ga92d1db4b1a397d5cb92eea38773773be) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___phy_mem_group.html#ga92d1db4b1a397d5cb92eea38773773be). **Comparison:** Common. **Current dispatch:** N.

```cpp
void OSGeometryUI::GetPhysicalMemberList(
    VARIANT FAR& nPhyMemberList);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nPhyMemberList` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetPMemberCount

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___phy_mem_group.html#ga7f62a79ec26db34b5661795ad7bd82c7) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___phy_mem_group.html#ga7f62a79ec26db34b5661795ad7bd82c7) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___phy_mem_group.html#ga7f62a79ec26db34b5661795ad7bd82c7). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetPMemberCount();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** No return table provided.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### CreatePMember

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___phy_mem_group.html) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___phy_mem_group.html) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___phy_mem_group.html). **Comparison:** Deprecated. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::CreatePMember(
    const VARIANT FAR& nAnalyticalMember,
    const VARIANT FAR& MemberList,
    const VARIANT FAR& MemberAttribute);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nAnalyticalMember` | `const VARIANT FAR&` | Not stated | `System.Object` |
| 2 | `MemberList` | `const VARIANT FAR&` | Not stated | `System.Object` |
| 3 | `MemberAttribute` | `const VARIANT FAR&` | Not stated | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** No return table provided.

**Review disposition:** G0: retain deprecated alias outside new API. Deprecated in all three summaries; no detailed contract. Do not create a new public replacement for this obsolete alias.

### Plate section

#### CreatePlate

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_group.html#ga8a4c1d7afeb60ee577a25492d526c1cc) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_group.html#ga8a4c1d7afeb60ee577a25492d526c1cc) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_group.html#ga8a4c1d7afeb60ee577a25492d526c1cc). **Comparison:** Common. **Current dispatch:** D.

```cpp
void OSGeometryUI::CreatePlate(
    const VARIANT FAR& nPlateNo,
    const VARIANT FAR& nNodeA,
    const VARIANT FAR& nNodeB,
    const VARIANT FAR& nNodeC,
    const VARIANT FAR& nNodeD);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nPlateNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `nNodeA` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `nNodeB` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `nNodeC` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `nNodeD` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Current callers:** `CreatePlate(Plate)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L227)); `CreateMultiplePlates(HashSet<Plate>, int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L870)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G6: controlled native mutations.

#### AddPlate

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_group.html#ga508a5c465ce389b6db8d4e8462290923) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_group.html#ga508a5c465ce389b6db8d4e8462290923) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_group.html#ga508a5c465ce389b6db8d4e8462290923). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::AddPlate(
    const VARIANT FAR& nNodeA,
    const VARIANT FAR& nNodeB,
    const VARIANT FAR& nNodeC,
    const VARIANT FAR& nNodeD);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeA` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `nNodeB` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `nNodeC` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `nNodeD` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `0`, `-1`, `-2001`, `-4004`.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### DeletePlate

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_group.html#ga5cbe1da89f6fb0c3ee0dc99a1a0761ca) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_group.html#ga5cbe1da89f6fb0c3ee0dc99a1a0761ca) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_group.html#ga5cbe1da89f6fb0c3ee0dc99a1a0761ca). **Comparison:** Common. **Current dispatch:** D.

```cpp
void OSGeometryUI::DeletePlate(
    const VARIANT FAR& nPlateNo);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nPlateNo` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Current callers:** `DeleteAllPlates(int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L894)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G6: controlled native mutations.

#### GetLastPlateNo

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_group.html#gaa52b12616c1c0cbb136b39a91f6ff2e0) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_group.html#gaa52b12616c1c0cbb136b39a91f6ff2e0) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_group.html#gaa52b12616c1c0cbb136b39a91f6ff2e0). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetLastPlateNo();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<Val>`, `-1`.

**Current callers:** `GetLastPlateNo()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L502)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### GetNoOfSelectedPlates

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_group.html#ga239d538dabbdb25d9f383e2b891c98e8) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_group.html#ga239d538dabbdb25d9f383e2b891c98e8) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_group.html#ga239d538dabbdb25d9f383e2b891c98e8). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetNoOfSelectedPlates();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** No return table provided.

**Current callers:** `AnyItemsSelected()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L505)); `GetSelectedPlatesIds()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L512)); `GetSelectedPlatesListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1159)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G2: selection contracts and entity support.

#### GetSelectedPlates

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_group.html#gad1925ca845c2ca6655b1c4d10104d371) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_group.html#gad1925ca845c2ca6655b1c4d10104d371) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_group.html#gad1925ca845c2ca6655b1c4d10104d371). **Comparison:** Common. **Current dispatch:** D.

```cpp
void OSGeometryUI::GetSelectedPlates(
    VARIANT FAR& naPlateNos,
    VARIANT FAR& nIsSorted);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `naPlateNos` | `VARIANT FAR&` | [out] | `System.Object&` |
| 2 | `nIsSorted` | `VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Current callers:** `GetSelectedPlatesIds()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L512)); `GetSelectedPlatesListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1159)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G2: selection contracts and entity support.

#### SelectMultiplePlates

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_group.html#ga7ee89b5f01003fae8dee1e092e3e5dc3) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_group.html#ga7ee89b5f01003fae8dee1e092e3e5dc3) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_group.html#ga7ee89b5f01003fae8dee1e092e3e5dc3). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::SelectMultiplePlates(
    const VARIANT FAR& naPlateNos);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `naPlateNos` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `0`, `1`.

**Current callers:** `SelectMultiplePlates(IEnumerable<int>)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L488)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G2: selection contracts and entity support.

#### SelectPlate

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_group.html#ga1cf73e62f13570600fd4e1568e20e0bf) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_group.html#ga1cf73e62f13570600fd4e1568e20e0bf) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_group.html#ga1cf73e62f13570600fd4e1568e20e0bf). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::SelectPlate(
    const VARIANT FAR& nPlateNo);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nPlateNo` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** No return table provided.

**Review disposition:** G2: selection contracts and entity support. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### CreateMultiplePlates

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_group.html#ga7c689c0da1fe53a9b7a3332846469d71) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_group.html#ga7c689c0da1fe53a9b7a3332846469d71) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_group.html#ga7c689c0da1fe53a9b7a3332846469d71). **Comparison:** Common. **Current dispatch:** N.

```cpp
void OSGeometryUI::CreateMultiplePlates(
    const VARIANT FAR& nPlateIds,
    const VARIANT FAR& nPlateIncidences);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nPlateIds` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `nPlateIncidences` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### AddMultiplePlates

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_group.html#ga8cc5d7726f3077b25c4fe32084af66a6) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_group.html#ga8cc5d7726f3077b25c4fe32084af66a6) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_group.html#ga8cc5d7726f3077b25c4fe32084af66a6). **Comparison:** Common. **Current dispatch:** N.

```cpp
void OSGeometryUI::AddMultiplePlates(
    const VARIANT FAR& naIncidences);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `naIncidences` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetPlateCount

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_group.html#ga6309ea1d3827606a3826b296ebadb437) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_group.html#ga6309ea1d3827606a3826b296ebadb437) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_group.html#ga6309ea1d3827606a3826b296ebadb437). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetPlateCount();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<Val>`.

**Current callers:** `GetAllPlatesList()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L770)); `GetAllPlatesListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1102)); `GetAllPlateIdsFromCom()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1306)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### GetPlateList

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_group.html#ga12eb65824cb564b32b50eea069317e1e) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_group.html#ga12eb65824cb564b32b50eea069317e1e) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_group.html#ga12eb65824cb564b32b50eea069317e1e). **Comparison:** Common. **Current dispatch:** D.

```cpp
void OSGeometryUI::GetPlateList(
    VARIANT FAR& nPlateList);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nPlateList` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Current callers:** `GetAllPlatesList()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L770)); `GetAllPlatesListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1102)); `GetAllPlateIdsFromCom()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1306)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### GetPlateIncidence

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_group.html#gace0bd8d034605ea6a9bc8032804dd661) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_group.html#gace0bd8d034605ea6a9bc8032804dd661) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_group.html#gace0bd8d034605ea6a9bc8032804dd661). **Comparison:** Clarification. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetPlateIncidence(
    const VARIANT FAR& nPlateNo,
    VARIANT FAR& nNodeA,
    VARIANT FAR& nNodeB,
    VARIANT FAR& nNodeC,
    VARIANT FAR& nNodeD);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nPlateNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `nNodeA` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `nNodeB` | `VARIANT FAR&` | [out] | `System.Object&` |
| 4 | `nNodeC` | `VARIANT FAR&` | [out] | `System.Object&` |
| 5 | `nNodeD` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `0`, `-1`, `-4001`.

**Current callers:** `GetPlateIncidence(int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L664)); `GetPlate(int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L679)); `GetPlate(int, IEnumerable<Node>)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L694)); `GetPlateIncidenceIdsFromCom(int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1334)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### ClearPlateSelection

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_group.html#ga7f97a775eab6d9864a8f4180b781b943) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_group.html#gaafca4ce4e500324f0cb9e29a398f09fd) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_group.html#gaafca4ce4e500324f0cb9e29a398f09fd). **Comparison:** Return shape. **Current dispatch:** D.

**2024:** `void ClearPlateSelection()`. **2025/2026:** `long ClearPlateSelection()`.

**Parameters:** none.

**Return declaration:** see the version distinction above. **Inspected CLR return (2025/2026):** `System.Int32`.

**Return labels:** 2024 — No return table provided; 2025/2026 — `0`, `-1`, `-4005`.

**Current callers:** `ClearPlateSelection()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L468)).

**Review disposition:** Apply the version-sensitive contract workstream before claiming qualified native success. G2: selection contracts and entity support.

#### SetPlateUniqueID

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_group.html#gaec663bc2b15935324492071b38cf8ec4) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_group.html#gaec663bc2b15935324492071b38cf8ec4) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_group.html#gaec663bc2b15935324492071b38cf8ec4). **Comparison:** Common. **Current dispatch:** N.

```cpp
void OSGeometryUI::SetPlateUniqueID(
    const VARIANT FAR& nPlateNo,
    const VARIANT FAR& szName);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nPlateNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `szName` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Review disposition:** G7: identity and exchange metadata. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetPlateUniqueID

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_group.html#ga728c1670d741a4fe47679ac41b2d6d6b) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_group.html#ga728c1670d741a4fe47679ac41b2d6d6b) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_group.html#ga728c1670d741a4fe47679ac41b2d6d6b). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetPlateUniqueID(
    const VARIANT FAR& nPlateNo);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nPlateNo` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<VARIANT>`.

**Review disposition:** G7: identity and exchange metadata. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetPlateIncidence_CIS2

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_group.html#ga985a15a5f15a40cc74a316f2a1c9ca12) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_group.html#ga985a15a5f15a40cc74a316f2a1c9ca12) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_group.html#ga985a15a5f15a40cc74a316f2a1c9ca12). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetPlateIncidence_CIS2(
    const VARIANT FAR& nPlateNo,
    VARIANT FAR& szName,
    VARIANT FAR& nNodeA,
    VARIANT FAR& nNodeB,
    VARIANT FAR& nNodeC,
    VARIANT FAR& nNodeD);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nPlateNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `szName` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `nNodeA` | `VARIANT FAR&` | [out] | `System.Object&` |
| 4 | `nNodeB` | `VARIANT FAR&` | [out] | `System.Object&` |
| 5 | `nNodeC` | `VARIANT FAR&` | [out] | `System.Object&` |
| 6 | `nNodeD` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `0`, `-1`, `-4001`.

**Review disposition:** G7: identity and exchange metadata. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetPlateNodeCount

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_group.html#gafe55d08c7a5ef983dae11cc0a996444e) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_group.html#gafe55d08c7a5ef983dae11cc0a996444e) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_group.html#gafe55d08c7a5ef983dae11cc0a996444e). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetPlateNodeCount(
    const VARIANT FAR& varnPlateNo);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `varnPlateNo` | `const VARIANT FAR&` | Not stated | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** No return table provided.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetAreaOfPlates

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_group.html#gadcb883f4891ec07856113a2d9fe215cb) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_group.html#gadcb883f4891ec07856113a2d9fe215cb) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_group.html#gadcb883f4891ec07856113a2d9fe215cb). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetAreaOfPlates(
    const VARIANT FAR& varPlateNos,
    VARIANT FAR& varPlatesArea);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `varPlateNos` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varPlatesArea` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** No return table provided.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

### Solid section

#### CreateSolid

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_group.html#gaa3fb9f329aaf4de51593ac66d38d1793) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_group.html#gaa3fb9f329aaf4de51593ac66d38d1793) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_group.html#gaa3fb9f329aaf4de51593ac66d38d1793). **Comparison:** Common. **Current dispatch:** N.

```cpp
void OSGeometryUI::CreateSolid(
    const VARIANT FAR& nSolidNo,
    const VARIANT FAR& nNodeA,
    const VARIANT FAR& nNodeB,
    const VARIANT FAR& nNodeC,
    const VARIANT FAR& nNodeD,
    const VARIANT FAR& nNodeE,
    const VARIANT FAR& nNodeF,
    const VARIANT FAR& nNodeG,
    const VARIANT FAR& nNodeH);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nSolidNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `nNodeA` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `nNodeB` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `nNodeC` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `nNodeD` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `nNodeE` | `const VARIANT FAR&` | [in] | `System.Object` |
| 7 | `nNodeF` | `const VARIANT FAR&` | [in] | `System.Object` |
| 8 | `nNodeG` | `const VARIANT FAR&` | [in] | `System.Object` |
| 9 | `nNodeH` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### AddSolid

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_group.html#gac5dbfcb0fa33dd65c5981837aff25ea2) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_group.html#gac5dbfcb0fa33dd65c5981837aff25ea2) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_group.html#gac5dbfcb0fa33dd65c5981837aff25ea2). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::AddSolid(
    const VARIANT FAR& nNodeA,
    const VARIANT FAR& nNodeB,
    const VARIANT FAR& nNodeC,
    const VARIANT FAR& nNodeD,
    const VARIANT FAR& nNodeE,
    const VARIANT FAR& nNodeF,
    const VARIANT FAR& nNodeG,
    const VARIANT FAR& nNodeH);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nNodeA` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `nNodeB` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `nNodeC` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `nNodeD` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `nNodeE` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `nNodeF` | `const VARIANT FAR&` | [in] | `System.Object` |
| 7 | `nNodeG` | `const VARIANT FAR&` | [in] | `System.Object` |
| 8 | `nNodeH` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `Id`, `-1`, `-2001`, `-5004`.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### DeleteSolid

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_group.html#gadb173f6cd72c6cfae23112aced4c7c66) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_group.html#gadb173f6cd72c6cfae23112aced4c7c66) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_group.html#gadb173f6cd72c6cfae23112aced4c7c66). **Comparison:** Common. **Current dispatch:** D.

```cpp
void OSGeometryUI::DeleteSolid(
    const VARIANT FAR& nSolidNo);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nSolidNo` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Current callers:** `DeleteAllSolids(int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L887)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G6: controlled native mutations.

#### GetLastSolidNo

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_group.html#ga5b8db583ebfe7478525e59275a1d9510) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_group.html#ga5b8db583ebfe7478525e59275a1d9510) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_group.html#ga5b8db583ebfe7478525e59275a1d9510). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetLastSolidNo();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<Val>`, `-1`.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetNoOfSelectedSolids

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_group.html#ga78302b9e966dece5f43073abb209a0e3) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_group.html#ga78302b9e966dece5f43073abb209a0e3) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_group.html#ga78302b9e966dece5f43073abb209a0e3). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetNoOfSelectedSolids();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** No return table provided.

**Current callers:** `AnyItemsSelected()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L505)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G2: selection contracts and entity support.

#### GetSelectedSolids

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_group.html#ga36f903900617588ed0d44f8357c011af) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_group.html#ga36f903900617588ed0d44f8357c011af) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_group.html#ga36f903900617588ed0d44f8357c011af). **Comparison:** Common. **Current dispatch:** N.

```cpp
void OSGeometryUI::GetSelectedSolids(
    VARIANT FAR& naSolidNos,
    VARIANT FAR& nIsSorted);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `naSolidNos` | `VARIANT FAR&` | [out] | `System.Object&` |
| 2 | `nIsSorted` | `VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Review disposition:** G2: selection contracts and entity support. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### SelectMultipleSolids

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_group.html#ga3686e1e3968ac182ae7645563b8c685f) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_group.html#ga3686e1e3968ac182ae7645563b8c685f) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_group.html#ga3686e1e3968ac182ae7645563b8c685f). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::SelectMultipleSolids(
    const VARIANT FAR& naSolidNos);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `naSolidNos` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `0`, `1`.

**Current callers:** `SelectMultipleSolids(IEnumerable<int>)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L490)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G2: selection contracts and entity support.

#### SelectSolid

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_group.html#gaf8f465bbefc2395b7807486ac039ec5f) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_group.html#gaf8f465bbefc2395b7807486ac039ec5f) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_group.html#gaf8f465bbefc2395b7807486ac039ec5f). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::SelectSolid(
    const VARIANT FAR& nSolidNo);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nSolidNo` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** No return table provided.

**Review disposition:** G2: selection contracts and entity support. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### AddMultipleSolids

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_group.html#ga090538564ee1748994f2da6a60f94bd0) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_group.html#ga090538564ee1748994f2da6a60f94bd0) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_group.html#ga090538564ee1748994f2da6a60f94bd0). **Comparison:** Common. **Current dispatch:** N.

```cpp
void OSGeometryUI::AddMultipleSolids(
    const VARIANT FAR& naIncidences);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `naIncidences` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetSolidCount

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_group.html#ga7941b067bc8c76437067d55629d0c3f2) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_group.html#ga7941b067bc8c76437067d55629d0c3f2) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_group.html#ga7941b067bc8c76437067d55629d0c3f2). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetSolidCount();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<Val>`.

**Current callers:** `GetAllSolidsList()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L761)); `GetAllSolidsListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1093)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### GetSolidList

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_group.html#ga1f4334f52c6f74cc452774522b1b340a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_group.html#ga1f4334f52c6f74cc452774522b1b340a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_group.html#ga1f4334f52c6f74cc452774522b1b340a). **Comparison:** Common. **Current dispatch:** D.

```cpp
void OSGeometryUI::GetSolidList(
    VARIANT FAR& nSolidList);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nSolidList` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Current callers:** `GetAllSolidsList()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L761)); `GetAllSolidsListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1093)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### GetSolidIncidence

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_group.html#gabc6798b3a42c987325ac20a8c0ac4a6c) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_group.html#gabc6798b3a42c987325ac20a8c0ac4a6c) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_group.html#gabc6798b3a42c987325ac20a8c0ac4a6c). **Comparison:** Status policy. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetSolidIncidence(
    const VARIANT FAR& nSolidNo,
    VARIANT FAR& nNodeA,
    VARIANT FAR& nNodeB,
    VARIANT FAR& nNodeC,
    VARIANT FAR& nNodeD,
    VARIANT FAR& nNodeE,
    VARIANT FAR& nNodeF,
    VARIANT FAR& nNodeG,
    VARIANT FAR& nNodeH);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nSolidNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `nNodeA` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `nNodeB` | `VARIANT FAR&` | [out] | `System.Object&` |
| 4 | `nNodeC` | `VARIANT FAR&` | [out] | `System.Object&` |
| 5 | `nNodeD` | `VARIANT FAR&` | [out] | `System.Object&` |
| 6 | `nNodeE` | `VARIANT FAR&` | [out] | `System.Object&` |
| 7 | `nNodeF` | `VARIANT FAR&` | [out] | `System.Object&` |
| 8 | `nNodeG` | `VARIANT FAR&` | [out] | `System.Object&` |
| 9 | `nNodeH` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** 2024 — No return table provided; 2025/2026 — `0`, `-1`, `-5001`.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### ClearSolidSelection

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_group.html#gac59e963ae254f1c2323aa748fe181c09) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_group.html#ga0abd0408cd11523dcd5f2fee4afa76b3) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_group.html#ga0abd0408cd11523dcd5f2fee4afa76b3). **Comparison:** Return shape. **Current dispatch:** D.

**2024:** `void ClearSolidSelection()`. **2025/2026:** `long ClearSolidSelection()`.

**Parameters:** none.

**Return declaration:** see the version distinction above. **Inspected CLR return (2025/2026):** `System.Int32`.

**Return labels:** 2024 — `0`; 2025/2026 — `0`, `-1`, `-5005`.

**Current callers:** `ClearSolidSelection()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L470)).

**Review disposition:** Apply the version-sensitive contract workstream before claiming qualified native success. G2: selection contracts and entity support.

#### SetSolidUniqueID

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_group.html#ga6c8d6c4412edea850a221f50ab0ffaf7) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_group.html#ga6c8d6c4412edea850a221f50ab0ffaf7) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_group.html#ga6c8d6c4412edea850a221f50ab0ffaf7). **Comparison:** Common. **Current dispatch:** N.

```cpp
void OSGeometryUI::SetSolidUniqueID(
    const VARIANT FAR& nSolidNo,
    const VARIANT FAR& szName);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nSolidNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `szName` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `void` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Void`.

**Return labels:** No return table provided.

**Review disposition:** G7: identity and exchange metadata. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetSolidUniqueID

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_group.html#ga091c95da0744749af73b150952f3f434) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_group.html#ga091c95da0744749af73b150952f3f434) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_group.html#ga091c95da0744749af73b150952f3f434). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetSolidUniqueID(
    const VARIANT FAR& nSolidNo);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nSolidNo` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<VARIANT>`.

**Review disposition:** G7: identity and exchange metadata. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetSolidIncidence_CIS2

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_group.html#gad12b0b25029975c5091c8847cb8dfb03) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_group.html#gad12b0b25029975c5091c8847cb8dfb03) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_group.html#gad12b0b25029975c5091c8847cb8dfb03). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetSolidIncidence_CIS2(
    const VARIANT FAR& nSolidNo,
    VARIANT FAR& szName,
    VARIANT FAR& nNodeA,
    VARIANT FAR& nNodeB,
    VARIANT FAR& nNodeC,
    VARIANT FAR& nNodeD,
    VARIANT FAR& nNodeE,
    VARIANT FAR& nNodeF,
    VARIANT FAR& nNodeG,
    VARIANT FAR& nNodeH);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nSolidNo` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `szName` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `nNodeA` | `VARIANT FAR&` | [out] | `System.Object&` |
| 4 | `nNodeB` | `VARIANT FAR&` | [out] | `System.Object&` |
| 5 | `nNodeC` | `VARIANT FAR&` | [out] | `System.Object&` |
| 6 | `nNodeD` | `VARIANT FAR&` | [out] | `System.Object&` |
| 7 | `nNodeE` | `VARIANT FAR&` | [out] | `System.Object&` |
| 8 | `nNodeF` | `VARIANT FAR&` | [out] | `System.Object&` |
| 9 | `nNodeG` | `VARIANT FAR&` | [out] | `System.Object&` |
| 10 | `nNodeH` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** No return table provided.

**Review disposition:** G7: identity and exchange metadata. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

### Group section

#### CreateGroup

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___group_group.html#gaa71bf19a58f2315de3b486082e6ebc25) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___group_group.html#gaa71bf19a58f2315de3b486082e6ebc25) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___group_group.html#gaa71bf19a58f2315de3b486082e6ebc25). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::CreateGroup(
    const VARIANT FAR& groupType,
    const VARIANT FAR& bstrGroup);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `groupType` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `bstrGroup` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `0`, `-1`, `-100`, `-110`, `-2005`, `-3005`, `-4005`, `-5005`, `-7001`.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetGroupCountAll

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___group_group.html#ga836e6b8494225a4be001ca33774268c1) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___group_group.html#ga836e6b8494225a4be001ca33774268c1) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___group_group.html#ga836e6b8494225a4be001ca33774268c1). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetGroupCountAll();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** No return table provided.

**Review disposition:** G3: qualified native queries. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetGroupCount

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___group_group.html#ga2e4859e882e8f0cc49a07bac0266fe4f) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___group_group.html#ga2e4859e882e8f0cc49a07bac0266fe4f) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___group_group.html#ga2e4859e882e8f0cc49a07bac0266fe4f). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetGroupCount(
    const VARIANT FAR& varGroupType);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `varGroupType` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** No return table provided.

**Current callers:** `GetGroupNames(GroupType)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L636)); `GetEntityGroups(HashSet<T>, int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L997)); `GetEntityGroupsAsync(HashSet<T>, int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1280)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### GetGroupNames

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___group_group.html#ga18527dea508aa2a7bbcf1db81a69378b) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___group_group.html#ga18527dea508aa2a7bbcf1db81a69378b) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___group_group.html#ga18527dea508aa2a7bbcf1db81a69378b). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetGroupNames(
    const VARIANT FAR& varGroupType,
    VARIANT FAR& szGroupNameList);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `varGroupType` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `szGroupNameList` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `0`, `-107`.

**Current callers:** `GetGroupNames(GroupType)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L636)); `GetEntityGroups(HashSet<T>, int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L997)); `GetEntityGroupsAsync(HashSet<T>, int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1280)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### GetGroupEntityCount

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___group_group.html#ga185ba5c3ac858788baa781a1c1672963) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___group_group.html#ga185ba5c3ac858788baa781a1c1672963) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___group_group.html#ga185ba5c3ac858788baa781a1c1672963). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetGroupEntityCount(
    const VARIANT FAR& szGroupName);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `szGroupName` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** No return table provided.

**Current callers:** `GetIdsOfEntitiesInGroup(string)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L655)); `GetEntitiesInGroup(string, HashSet<T>, int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1033)); `GetEntitiesInGroupAsync(string, HashSet<T>, int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1255)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### GetGroupEntities

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___group_group.html#ga71faf45acfe0633b562b34e4e0547f6f) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___group_group.html#ga71faf45acfe0633b562b34e4e0547f6f) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___group_group.html#ga71faf45acfe0633b562b34e4e0547f6f). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetGroupEntities(
    const VARIANT FAR& szGroupName,
    VARIANT FAR& varEntityList);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `szGroupName` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varEntityList` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `-1`, `-107`.

**Current callers:** `GetIdsOfEntitiesInGroup(string)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L655)); `GetEntitiesInGroup(string, HashSet<T>, int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1033)); `GetEntitiesInGroupAsync(string, HashSet<T>, int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L1255)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G3: qualified native queries.

#### CreateGroupEx

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___group_group.html#ga1bd0b4c646a808eaf83b3511a2fdb53f) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___group_group.html#ga1bd0b4c646a808eaf83b3511a2fdb53f) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___group_group.html#ga1bd0b4c646a808eaf83b3511a2fdb53f). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::CreateGroupEx(
    const VARIANT FAR& varGroupType,
    const VARIANT FAR& szGroupName,
    const VARIANT FAR& varEntityCount,
    const VARIANT FAR& varEntityList);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `varGroupType` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `szGroupName` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varEntityCount` | `const VARIANT FAR&` | Not stated | `System.Object` |
| 4 | `varEntityList` | `const VARIANT FAR&` | Not stated | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `0`, `-1`, `-100`, `-110`, `-2005`, `-3005`, `-4005`, `-5005`, `-7001`.

**Current callers:** `CreateGroupFrom(EntityGroup)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L249)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G6: controlled native mutations.

#### DeleteGroup

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___group_group.html#ga0df8a74e4de94d59080776640064d9a8) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___group_group.html#ga0df8a74e4de94d59080776640064d9a8) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___group_group.html#ga0df8a74e4de94d59080776640064d9a8). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::DeleteGroup(
    const VARIANT FAR& szGroupName);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `szGroupName` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `0`, `-1`.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### UpdateGroup

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___group_group.html#gad30c168709cc74b90abebfacf1f2ddd7) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___group_group.html#gad30c168709cc74b90abebfacf1f2ddd7) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___group_group.html#gad30c168709cc74b90abebfacf1f2ddd7). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::UpdateGroup(
    const VARIANT FAR& szGroupName,
    const VARIANT FAR& varFlag,
    const VARIANT FAR& varEntityCount,
    VARIANT FAR& varEntityList);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `szGroupName` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varFlag` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varEntityCount` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varEntityList` | `VARIANT FAR&` | [out] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `0`, `-1`, `-107`.

**Review disposition:** G6: controlled native mutations. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

### Parametric Surface section

#### DefineParametricSurface

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___parametric_surface_group.html#ga86b68fa2fcde44811bcca1db3b37029a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___parametric_surface_group.html#ga86b68fa2fcde44811bcca1db3b37029a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___parametric_surface_group.html#ga86b68fa2fcde44811bcca1db3b37029a). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::DefineParametricSurface(
    LPCTSTR strName,
    const VARIANT FAR& varType,
    const VARIANT FAR& nodeO,
    const VARIANT FAR& nodeX,
    const VARIANT FAR& node3rd,
    const VARIANT FAR& varCount,
    VARIANT FAR& varVertices,
    const VARIANT FAR& varAutoGenerate);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `strName` | `LPCTSTR` | [in] | `System.String` |
| 2 | `varType` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `nodeO` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `nodeX` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `node3rd` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `varCount` | `const VARIANT FAR&` | [in] | `System.Object` |
| 7 | `varVertices` | `VARIANT FAR&` | [in] | `System.Object` |
| 8 | `varAutoGenerate` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<nId>`, `-1`.

**Current callers:** `DefineParametricSurface(string, SurfaceType, int, int, int, int, int[], int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L439)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G6: controlled native mutations.

#### AddParametricSurfaceToModel

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___parametric_surface_group.html#gaef3d7b15f67c1ea16870f24dbcc7d34e) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___parametric_surface_group.html#gaef3d7b15f67c1ea16870f24dbcc7d34e) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___parametric_surface_group.html#gaef3d7b15f67c1ea16870f24dbcc7d34e). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::AddParametricSurfaceToModel(
    const VARIANT FAR& nSurfaceId);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nSurfaceId` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `1`, `0`.

**Current callers:** `AddParametricSurfaceToModel(int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L453)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G6: controlled native mutations.

#### CommitParametricSurfaceMesh

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___parametric_surface_group.html#ga51c9c7bd44f30b58027c9971c3c4573e) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___parametric_surface_group.html#ga51c9c7bd44f30b58027c9971c3c4573e) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___parametric_surface_group.html#ga51c9c7bd44f30b58027c9971c3c4573e). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::CommitParametricSurfaceMesh(
    const VARIANT FAR& nSurfaceId);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nSurfaceId` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `1`, `0`.

**Current callers:** `CommitParametricSurfaceMesh(int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L455)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G6: controlled native mutations.

#### RemoveParametricSurfaceMesh

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___parametric_surface_group.html#gadb32d3cb050c738b98a8c836712991d0) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___parametric_surface_group.html#gadb32d3cb050c738b98a8c836712991d0) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___parametric_surface_group.html#gadb32d3cb050c738b98a8c836712991d0). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::RemoveParametricSurfaceMesh(
    const VARIANT FAR& nSurfaceId);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nSurfaceId` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `1`, `0`.

**Current callers:** `RemoveParametricSurfaceMesh(int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L279)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G6: controlled native mutations.

#### AddDensityPointToSurface

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___parametric_surface_group.html#ga81b897a8d35da03911f39a9b548f837f) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___parametric_surface_group.html#ga81b897a8d35da03911f39a9b548f837f) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___parametric_surface_group.html#ga81b897a8d35da03911f39a9b548f837f). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::AddDensityPointToSurface(
    const VARIANT FAR& nSurfaceId,
    const VARIANT FAR& varX,
    const VARIANT FAR& varY,
    const VARIANT FAR& varZ,
    const VARIANT FAR& varDensity);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nSurfaceId` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varX` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varY` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varZ` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varDensity` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<val>`, `-1`.

**Current callers:** `AddDensityPointToSurface(int, double, double, double, int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L356)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G6: controlled native mutations.

#### AddDensityLineToSurface

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___parametric_surface_group.html#ga13c8b799948fcb6b0815097251870e1f) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___parametric_surface_group.html#ga13c8b799948fcb6b0815097251870e1f) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___parametric_surface_group.html#ga13c8b799948fcb6b0815097251870e1f). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::AddDensityLineToSurface(
    const VARIANT FAR& nSurfaceId,
    const VARIANT FAR& varX1,
    const VARIANT FAR& varY1,
    const VARIANT FAR& varZ1,
    const VARIANT FAR& varDensity1,
    const VARIANT FAR& varX2,
    const VARIANT FAR& varY2,
    const VARIANT FAR& varZ2,
    const VARIANT FAR& varDensity2,
    const VARIANT FAR& varDivs);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nSurfaceId` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varX1` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varY1` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varZ1` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varDensity1` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `varX2` | `const VARIANT FAR&` | [in] | `System.Object` |
| 7 | `varY2` | `const VARIANT FAR&` | [in] | `System.Object` |
| 8 | `varZ2` | `const VARIANT FAR&` | [in] | `System.Object` |
| 9 | `varDensity2` | `const VARIANT FAR&` | [in] | `System.Object` |
| 10 | `varDivs` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<val>`, `-1`.

**Current callers:** `AddDensityLineToSurface(int, Node, Node, int, int, int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L362)); `AddDensityLineToSurface(int, double, double, double, int, double, double, double, int, int)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L369)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G6: controlled native mutations.

#### AddCircularRegionToSurface

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___parametric_surface_group.html#gae49c93263f71a959b9841147013616e9) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___parametric_surface_group.html#gae49c93263f71a959b9841147013616e9) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___parametric_surface_group.html#gae49c93263f71a959b9841147013616e9). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::AddCircularRegionToSurface(
    const VARIANT FAR& nSurfaceId,
    const VARIANT FAR& varX,
    const VARIANT FAR& varY,
    const VARIANT FAR& varZ,
    const VARIANT FAR& varR,
    const VARIANT FAR& varDiv,
    const VARIANT FAR& varDensity,
    const VARIANT FAR& varIsOpening);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nSurfaceId` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varX` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varY` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varZ` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varR` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `varDiv` | `const VARIANT FAR&` | [in] | `System.Object` |
| 7 | `varDensity` | `const VARIANT FAR&` | [in] | `System.Object` |
| 8 | `varIsOpening` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<val>`, `-1`.

**Current callers:** `AddCircularRegionToSurface(int, double, double, double, double, int, int, RegionType)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L416)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G6: controlled native mutations.

#### AddPolygonalRegionToSurface

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___parametric_surface_group.html#gabbe67b98b65d50544e4f57bf091967c3) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___parametric_surface_group.html#gabbe67b98b65d50544e4f57bf091967c3) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___parametric_surface_group.html#gabbe67b98b65d50544e4f57bf091967c3). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::AddPolygonalRegionToSurface(
    const VARIANT FAR& nSurfaceId,
    const VARIANT FAR& varVertexCount,
    const VARIANT FAR& varX,
    const VARIANT FAR& varY,
    const VARIANT FAR& varZ,
    const VARIANT FAR& varDensities,
    const VARIANT FAR& varEdgeDivs,
    const VARIANT FAR& varIsOpening);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nSurfaceId` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varVertexCount` | `const VARIANT FAR&` | [in] | `System.Object` |
| 3 | `varX` | `const VARIANT FAR&` | [in] | `System.Object` |
| 4 | `varY` | `const VARIANT FAR&` | [in] | `System.Object` |
| 5 | `varZ` | `const VARIANT FAR&` | [in] | `System.Object` |
| 6 | `varDensities` | `const VARIANT FAR&` | [in] | `System.Object` |
| 7 | `varEdgeDivs` | `const VARIANT FAR&` | [in] | `System.Object` |
| 8 | `varIsOpening` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `<val>`, `-1`.

**Current callers:** `AddPolygonalRegionToSurface(int, int, double[], double[], double[], int[], int[], RegionType)` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L396)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G6: controlled native mutations.

#### GetParametricSurfaceCount

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___parametric_surface_group.html#gac0939a90d84852530e6518adbea151ec) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___parametric_surface_group.html#gac0939a90d84852530e6518adbea151ec) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___parametric_surface_group.html#gac0939a90d84852530e6518adbea151ec). **Comparison:** Common. **Current dispatch:** D.

```cpp
VARIANT OSGeometryUI::GetParametricSurfaceCount();
```

**Parameters:** none.

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** No return table provided.

**Current callers:** `GetParametricSurfaceCount()` ([source](../../src/StaadPro.Interop/Adapters/Models/OSGeometryAdapter.cs#L273)).

**Review disposition:** Use shared native behavior where confirmed; call presence is not evidence of complete result validation. G5: native surface and generated-mesh read-back.

#### GetParametricSurfaceInfoEx

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___parametric_surface_group.html#ga24b26091c82ee3ac1b3b7f354b56d93a) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___parametric_surface_group.html#ga24b26091c82ee3ac1b3b7f354b56d93a) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___parametric_surface_group.html#ga24b26091c82ee3ac1b3b7f354b56d93a). **Comparison:** Common. **Current dispatch:** N.

```cpp
long OSGeometryUI::GetParametricSurfaceInfoEx(
    long nSurfaceId,
    BSTR FAR* strName,
    long* nSurfaceType,
    BSTR FAR* strSurfaceSubType,
    long* nVertices,
    double* meshsize,
    long* iDivOpt,
    long* method,
    long* isQuad,
    long* originNode,
    long* nodeOnXAxis,
    long* nodeTowardsYAxis,
    long* nCircularOpenings,
    long* nPolygonalOpening,
    long* nCicularRegions,
    long* nPolygonalRegions,
    long* nDensityPoints,
    long* nDensityLines);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nSurfaceId` | `long` | [in] | `System.Int32` |
| 2 | `strName` | `BSTR FAR*` | [out] | `System.String&` |
| 3 | `nSurfaceType` | `long*` | [out] | `System.Int32&` |
| 4 | `strSurfaceSubType` | `BSTR FAR*` | [out] | `System.String&` |
| 5 | `nVertices` | `long*` | [out] | `System.Int32&` |
| 6 | `meshsize` | `double*` | [out] | `System.Double&` |
| 7 | `iDivOpt` | `long*` | [out] | `System.Int32&` |
| 8 | `method` | `long*` | [out] | `System.Int32&` |
| 9 | `isQuad` | `long*` | [out] | `System.Int32&` |
| 10 | `originNode` | `long*` | [out] | `System.Int32&` |
| 11 | `nodeOnXAxis` | `long*` | [out] | `System.Int32&` |
| 12 | `nodeTowardsYAxis` | `long*` | [out] | `System.Int32&` |
| 13 | `nCircularOpenings` | `long*` | [out] | `System.Int32&` |
| 14 | `nPolygonalOpening` | `long*` | [out] | `System.Int32&` |
| 15 | `nCicularRegions` | `long*` | [out] | `System.Int32&` |
| 16 | `nPolygonalRegions` | `long*` | [out] | `System.Int32&` |
| 17 | `nDensityPoints` | `long*` | [out] | `System.Int32&` |
| 18 | `nDensityLines` | `long*` | [out] | `System.Int32&` |

**Return declaration:** `long` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Int32`.

**Return labels:** `1`, `0`.

**Review disposition:** G5: native surface and generated-mesh read-back. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetParametricSurfaceInfo

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___parametric_surface_group.html#ga93b10ab90640ebc723dade3ce256cebd) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___parametric_surface_group.html#ga93b10ab90640ebc723dade3ce256cebd) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___parametric_surface_group.html#ga93b10ab90640ebc723dade3ce256cebd). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetParametricSurfaceInfo(
    const VARIANT FAR& varSurfaceId,
    VARIANT FAR& varName,
    VARIANT FAR& varSType,
    VARIANT FAR& varBPCount,
    VARIANT FAR& varDPCount,
    VARIANT FAR& varDLCount,
    VARIANT FAR& varOpeningCount,
    VARIANT FAR& varRegionCount);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `varSurfaceId` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varName` | `VARIANT FAR&` | [out] | `System.Object&` |
| 3 | `varSType` | `VARIANT FAR&` | [out] | `System.Object&` |
| 4 | `varBPCount` | `VARIANT FAR&` | [out] | `System.Object&` |
| 5 | `varDPCount` | `VARIANT FAR&` | [out] | `System.Object&` |
| 6 | `varDLCount` | `VARIANT FAR&` | [out] | `System.Object&` |
| 7 | `varOpeningCount` | `VARIANT FAR&` | [out] | `System.Object&` |
| 8 | `varRegionCount` | `VARIANT FAR&` | [out] | `System.Object&` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `1`, `0`.

**Review disposition:** G5: native surface and generated-mesh read-back. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetParametricSurfaceMeshInfo

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___parametric_surface_group.html#ga9ac478e12f6fbfdff818afd6c896bafa) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___parametric_surface_group.html#ga9ac478e12f6fbfdff818afd6c896bafa) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___parametric_surface_group.html#ga9ac478e12f6fbfdff818afd6c896bafa). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetParametricSurfaceMeshInfo(
    const VARIANT FAR& nSurfaceId,
    VARIANT FAR& varNodeCount,
    VARIANT FAR& varElementCount);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nSurfaceId` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varNodeCount` | `VARIANT FAR&` | [in] | `System.Object&` |
| 3 | `varElementCount` | `VARIANT FAR&` | [in] | `System.Object&` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `1`, `0`.

**Review disposition:** G5: native surface and generated-mesh read-back. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetParametricSurfaceMeshData

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___parametric_surface_group.html#ga852ad2d25bdd7c96cc920d842f186a97) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___parametric_surface_group.html#ga852ad2d25bdd7c96cc920d842f186a97) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___parametric_surface_group.html#ga852ad2d25bdd7c96cc920d842f186a97). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetParametricSurfaceMeshData(
    const VARIANT FAR& nSurfaceId,
    VARIANT FAR& varGeneratedNodes,
    VARIANT FAR& varGenereatedElements);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `nSurfaceId` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `varGeneratedNodes` | `VARIANT FAR&` | [in] | `System.Object&` |
| 3 | `varGenereatedElements` | `VARIANT FAR&` | [in] | `System.Object&` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `1`, `0`.

**Review disposition:** G5: native surface and generated-mesh read-back. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### SetParametricSurfaceUniqueID

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___parametric_surface_group.html#ga8dc0783dd74ac7033a1c55be9b0c06ad) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___parametric_surface_group.html#ga8dc0783dd74ac7033a1c55be9b0c06ad) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___parametric_surface_group.html#ga8dc0783dd74ac7033a1c55be9b0c06ad). **Comparison:** Status policy. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::SetParametricSurfaceUniqueID(
    const VARIANT FAR& szName,
    const VARIANT FAR& szUID);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `szName` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `szUID` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** 2024 — `0`, `-5603`; 2025/2026 — `0`, `-115`, `-5603`.

**Review disposition:** G7: identity and exchange metadata. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetParametricSurfaceUniqueID

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___parametric_surface_group.html#ga7e73b4a1c92abe03d7957d852cda4638) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___parametric_surface_group.html#ga7e73b4a1c92abe03d7957d852cda4638) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___parametric_surface_group.html#ga7e73b4a1c92abe03d7957d852cda4638). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetParametricSurfaceUniqueID(
    const VARIANT FAR& szName);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `szName` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `Non-empty`, `Empty`.

**Review disposition:** G7: identity and exchange metadata. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### SetParametricSurfaceSubType

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___parametric_surface_group.html#gae284387de7402350eb7ee92a397fe40f) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___parametric_surface_group.html#gae284387de7402350eb7ee92a397fe40f) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___parametric_surface_group.html#gae284387de7402350eb7ee92a397fe40f). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::SetParametricSurfaceSubType(
    const VARIANT FAR& szName,
    const VARIANT FAR& szSubType);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `szName` | `const VARIANT FAR&` | [in] | `System.Object` |
| 2 | `szSubType` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `0`, `1`.

**Review disposition:** G7: identity and exchange metadata. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

#### GetParametricSurfaceSubType

**References:** [2024](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___parametric_surface_group.html#ga10445de74c2b219cdf0c23955d64501f) · [2025](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___parametric_surface_group.html#ga10445de74c2b219cdf0c23955d64501f) · [2026](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___parametric_surface_group.html#ga10445de74c2b219cdf0c23955d64501f). **Comparison:** Common. **Current dispatch:** N.

```cpp
VARIANT OSGeometryUI::GetParametricSurfaceSubType(
    const VARIANT FAR& szName);
```

| Slot | Native parameter | Native declared type | Help direction | 2025/2026 CLR slot |
| ---: | --- | --- | --- | --- |
| 1 | `szName` | `const VARIANT FAR&` | [in] | `System.Object` |

**Return declaration:** `VARIANT` in all reviewed years. **Inspected CLR return (2025/2026):** `System.Object`.

**Return labels:** `Non-empty`, `Empty`.

**Review disposition:** G7: identity and exchange metadata. No direct call to this native name exists in the reviewed adapter. Decide whether a dedicated managed API is needed or a documented existing composition is sufficient.

## 2. Supplemental installed interop inventory

These 41 additional names appear in the inspected 2025/2026 Geometry interface but not in the eight Geometry help pages per year. The two inspected declaration sets match. This is not a statement that these names are undocumented everywhere or safe to expose from signatures alone. A corresponding 2024 vendor DLL was not available at the inspected path.

- [2025 installed vendor interop assembly](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/STAAD/StaadPro.Interop.dll): `CC724993AC9119138B7323E8C684997257E2851DE62E1551BA5D6D67A9DADC19`.
- [2026 installed vendor interop assembly](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/STAAD/StaadPro.Interop.dll): `6D153E80ED356A1B167ABB2EF19227FCFF510707A578AD304154652CC6C1F6D4`.

| Native name | Inspected CLR declaration | Current native call | Proposed review boundary |
| --- | --- | --- | --- |
| `GetSurfaceNetArea` | `System.Object GetSurfaceNetArea(System.Object)` | No | Surface/selection semantics and buffer layout require review. |
| `GetSurfaceOpeningArea` | `System.Object GetSurfaceOpeningArea(System.Object, System.Object)` | No | Surface/selection semantics and buffer layout require review. |
| `GetAttachedSurfaceForPlate` | `System.Object GetAttachedSurfaceForPlate(System.Object)` | No | Surface/selection semantics and buffer layout require review. |
| `SetAttachedSurfaceForPlate` | `System.Object SetAttachedSurfaceForPlate(System.Object, System.Object)` | No | Surface/selection semantics and buffer layout require review. |
| `FindParametricSurfaceByName` | `System.Object FindParametricSurfaceByName(System.Object)` | No | Surface/selection semantics and buffer layout require review. |
| `GetSurfaceCount` | `System.Object GetSurfaceCount()` | No | Surface/selection semantics and buffer layout require review. |
| `GetSurfaceList` | `Void GetSurfaceList(System.Object ByRef)` | No | Surface/selection semantics and buffer layout require review. |
| `InstallTSCallBack` | `Void InstallTSCallBack(System.Object)` | No | Callback/lifetime contract required; do not infer from a signature. |
| `AttachMemberToPhysicalMember` | `System.Object AttachMemberToPhysicalMember(System.Object, System.Object)` | No | Native physical-member ownership/attributes require review. |
| `SetPhysicalMemberAttribute` | `System.Object SetPhysicalMemberAttribute(System.Object, System.Object)` | No | Native physical-member ownership/attributes require review. |
| `GetPhysicalMemberAttribute` | `System.Object GetPhysicalMemberAttribute(System.Object)` | No | Native physical-member ownership/attributes require review. |
| `ClearPhysicalMemberSelection` | `Void ClearPhysicalMemberSelection()` | Yes | Native physical-member ownership/attributes require review. |
| `ClearSurfaceSelection` | `Void ClearSurfaceSelection()` | Yes | Surface/selection semantics and buffer layout require review. |
| `GetNoOfSelectedSurfaces` | `System.Object GetNoOfSelectedSurfaces()` | Yes | Surface/selection semantics and buffer layout require review. |
| `GetSelectedSurfaces` | `Void GetSelectedSurfaces(System.Object ByRef, System.Object)` | No | Surface/selection semantics and buffer layout require review. |
| `SetPID` | `Void SetPID(System.Object, System.Object, System.Object)` | No | Process/internal context meaning requires separate evidence. |
| `GetPID` | `System.Object GetPID(System.Object, System.Object)` | No | Process/internal context meaning requires separate evidence. |
| `GetNoOfGeneratedQuadPanels` | `System.Object GetNoOfGeneratedQuadPanels()` | No | Generated-panel count/data contract requires review. |
| `GetGeneratedQuadPanelIncidences` | `System.Object GetGeneratedQuadPanelIncidences(System.Object ByRef, System.Object ByRef, System.Object ByRef, System.Object ByRef)` | No | Generated-panel count/data contract requires review. |
| `AttachMemberToPMember` | `System.Object AttachMemberToPMember(System.Object, System.Object)` | No | Native physical-member ownership/attributes require review. |
| `CreateSurface` | `System.Object CreateSurface(System.Object, System.Object)` | No | Surface/selection semantics and buffer layout require review. |
| `AddSurface` | `System.Object AddSurface(System.Object)` | No | Surface/selection semantics and buffer layout require review. |
| `SetSurfaceUniqueID` | `System.Object SetSurfaceUniqueID(System.Object, System.Object)` | No | Surface/selection semantics and buffer layout require review. |
| `GetSurfaceUniqueID` | `System.Object GetSurfaceUniqueID(System.Object)` | No | Surface/selection semantics and buffer layout require review. |
| `GetSurfaceNodeCount` | `System.Object GetSurfaceNodeCount(System.Object)` | No | Surface/selection semantics and buffer layout require review. |
| `GetSurfaceNodeList` | `System.Object GetSurfaceNodeList(System.Object, System.Object ByRef)` | No | Surface/selection semantics and buffer layout require review. |
| `GetSurfaceOpeningCount` | `System.Object GetSurfaceOpeningCount(System.Object)` | No | Surface/selection semantics and buffer layout require review. |
| `GetSurfaceOpeningList` | `System.Object GetSurfaceOpeningList(System.Object, System.Object ByRef)` | No | Surface/selection semantics and buffer layout require review. |
| `GetSurfaceOpeningDetails` | `System.Object GetSurfaceOpeningDetails(System.Object, System.Object, System.Object ByRef, System.Object ByRef)` | No | Surface/selection semantics and buffer layout require review. |
| `GetSurfaceOpeningVertices` | `System.Object GetSurfaceOpeningVertices(System.Object, System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef)` | No | Surface/selection semantics and buffer layout require review. |
| `GetSurfaceEdgeDivision` | `System.Object GetSurfaceEdgeDivision(System.Object, System.Object)` | No | Surface/selection semantics and buffer layout require review. |
| `GetSurfaceOpeningEdgeDivision` | `System.Object GetSurfaceOpeningEdgeDivision(System.Object, System.Object, System.Object)` | No | Surface/selection semantics and buffer layout require review. |
| `AddSurfaceOpening` | `System.Object AddSurfaceOpening(System.Object, System.Object, System.Object, System.Object, System.Object, System.Object)` | No | Surface/selection semantics and buffer layout require review. |
| `AddSurfaceEdgeDivision` | `System.Object AddSurfaceEdgeDivision(System.Object, System.Object, System.Object)` | No | Surface/selection semantics and buffer layout require review. |
| `AddSurfaceOpeningEdgeDivision` | `System.Object AddSurfaceOpeningEdgeDivision(System.Object, System.Object, System.Object, System.Object)` | No | Surface/selection semantics and buffer layout require review. |
| `RemoveSurfaceOpening` | `System.Object RemoveSurfaceOpening(System.Object, System.Object)` | No | Surface/selection semantics and buffer layout require review. |
| `DeleteSurface` | `System.Object DeleteSurface(System.Object)` | No | Surface/selection semantics and buffer layout require review. |
| `GetSurfaceOrigin` | `System.Object GetSurfaceOrigin(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef)` | No | Surface/selection semantics and buffer layout require review. |
| `GetSurfaceLocalAxis` | `System.Object GetSurfaceLocalAxis(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef)` | No | Surface/selection semantics and buffer layout require review. |
| `GetSurfaceCenter` | `System.Object GetSurfaceCenter(System.Object, System.Object ByRef, System.Object ByRef, System.Object ByRef)` | No | Surface/selection semantics and buffer layout require review. |
| `GetSurfaceGrossArea` | `System.Object GetSurfaceGrossArea(System.Object)` | No | Surface/selection semantics and buffer layout require review. |

Three current native calls are in this supplemental set: ClearPhysicalMemberSelection, ClearSurfaceSelection and GetNoOfSelectedSurfaces. All have matching argument counts and explicit ref/value shapes against the inspected declarations. This does not establish 2024 support or native behavior.

## 3. Current managed interface inventory

The following 143 overloads are the current public IOSGeometry surface, with matching public OSGeometryAdapter overloads. Their count measures the private-source port, not the number of native functions. Native targets listed are direct calls in that method body only; an empty list can mean delegation, a managed algorithm or a helper path. It does not mean that the method is missing.

| Managed declaration | Direct native targets in this body |
| --- | --- |
| `PhysicalMemberIdentificationResult IdentifyPhysicalMembers(IEnumerable<Beam> beams, double angularToleranceDegrees = 0.005)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L17)) | Delegation / managed logic; see implementation |
| `IEnumerable<PhysicalMemberPlanAngleGroup> GroupPhysicalMembersByPlanAngle(IEnumerable<Member> members, double angularToleranceDegrees = 1e-3)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L20)) | Delegation / managed logic; see implementation |
| `PolygonMeshResult GeneratePolygonMesh(IReadOnlyList<Node> boundaryNodes, int startingNodeId, int startingPlateId, double minimumAngleDegrees = 20.0, double maximumEdgeLength = 0.0)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L23)) | Delegation / managed logic; see implementation |
| `IOSGeometry CreateMultipleBeamsExtended(IOSRoot openStaad, IEnumerable<Beam> beamCollection)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L33)) | `CreateBeam` |
| `IOSGeometry CreateMultipleNodesExtended(IOSRoot openStaad, IEnumerable<Node> nodeCollection)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L36)) | `CreateMultipleNodes` |
| `IOSGeometry MergeNodesExtended(IOSRoot openStaad, Node retain, IEnumerable<Node> nodesToMerge)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L39)) | `MergeNodes` |
| `IEnumerable<IEnumerable<Beam>> SegregateBeamsByConnectivity(IEnumerable<Beam> beams)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L42)) | Delegation / managed logic; see implementation |
| `IEnumerable<IEnumerable<Member>> SegregateMembersByConnectivity(IEnumerable<Member> members, MemberConnectivityScope scope = MemberConnectivityScope.EndpointsOnly)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L45)) | Delegation / managed logic; see implementation |
| `IEnumerable<IEnumerable<Plate>> SegregatePlatesByConnectivity(IEnumerable<Plate> plates, PlateConnectivityScope scope = PlateConnectivityScope.SharedEdge)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L48)) | Delegation / managed logic; see implementation |
| `bool IsZUp()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L51)) | `IsZUp` |
| `GlobalVerticalAxis GetGlobalVerticalAxis()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L54)) | Delegation / managed logic; see implementation |
| `int GetParametricSurfaceCount()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L61)) | `GetParametricSurfaceCount` |
| `bool RemoveParametricSurfaceMesh(int surfaceId)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L64)) | `RemoveParametricSurfaceMesh` |
| `IOSGeometry AddAndCommitParametricSurfaceToModel(int surfaceId)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L67)) | Delegation / managed logic; see implementation |
| `int AddParametricSurfaceToModel(int surfaceId)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L70)) | `AddParametricSurfaceToModel` |
| `int CommitParametricSurfaceMesh(int surfaceId)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L73)) | `CommitParametricSurfaceMesh` |
| `(int SurfaceId, IEnumerable<Node> PeripheralNodes, IEnumerable<Node> OpeningNodes) CreateAnnularParametricSurface(string surfaceName, Node center, double rOuter, int divOuter, double rInner, int divInner, int idLastUsedNode = 0, AutoGenerate autoGenerate = AutoGenerate.No)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L76)) | Delegation / managed logic; see implementation |
| `(int SurfaceId, IEnumerable<Node> OpeningNodes) CreateAnnularParametricSurface(string surfaceName, Node center, IEnumerable<Node> peripheralNodes, double rInner, int divInner, int idLastUsedNode = 0, AutoGenerate autoGenerate = AutoGenerate.No)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L80)) | Delegation / managed logic; see implementation |
| `(int SurfaceId, IEnumerable<Node> PeripheralNodes) CreateAnnularParametricSurface(string surfaceName, Node center, double rOuter, int divOuter, IEnumerable<Node> openingNodes, int idLastUsedNode = 0, AutoGenerate autoGenerate = AutoGenerate.No)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L84)) | Delegation / managed logic; see implementation |
| `int CreateAnnularParametricSurface(string surfaceName, IEnumerable<Node> outerVertices, IEnumerable<Node> innerVertices, SurfaceType type = SurfaceType.None, AutoGenerate autoGenerate = AutoGenerate.No)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L88)) | Delegation / managed logic; see implementation |
| `int CreateSolidCircularParametricSurface(string surfaceName, Node center, IEnumerable<Node> vertices, AutoGenerate autoGenerate = AutoGenerate.No)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L91)) | Delegation / managed logic; see implementation |
| `(int SurfaceId, IEnumerable<Node> GeneratedNodes) CreateSolidCircularParametricSurface(string surfaceName, Node center, double radius, int divisions, int idLastUsedNode = 0, AutoGenerate autoGenerate = AutoGenerate.No)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L94)) | Delegation / managed logic; see implementation |
| `int AddDensityPointToSurface(int surfaceId, Node densityPoint, int density)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L97)) | Delegation / managed logic; see implementation |
| `int AddDensityPointToSurface(int surfaceId, double xDensityPoint, double yDensityPoint, double zDensityPoint, int density)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L100)) | `AddDensityPointToSurface` |
| `int AddDensityLineToSurface(int surfaceId, Node sNodeDensityLine, Node eNodeDensityLine, int sDensity, int eDensity, int nDivisions)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L103)) | `AddDensityLineToSurface` |
| `int AddDensityLineToSurface(int surfaceId, double xDensityLineSNode, double yDensityLineSNode, double zDensityLineSNode, int sDensity, double xDensityLineENode, double yDensityLineENode, double zDensityLineENode, int eDensity, int nDivisions)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L106)) | `AddDensityLineToSurface` |
| `int AddPolygonalRegionToSurface(int surfaceId, IEnumerable<Node> polygonNodes, RegionType regionType)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L110)) | Delegation / managed logic; see implementation |
| `int AddPolygonalRegionToSurface(int surfaceId, IEnumerable<Node> polygonNodes, int[] densities, int[] edgeDivs, RegionType regionType)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L113)) | Delegation / managed logic; see implementation |
| `int AddPolygonalRegionToSurface(int surfaceId, int countVertices, double[] xArrayRegion, double[] yArrayRegion, double[] zArrayRegion, int[] densities, int[] edgeDivs, RegionType regionType)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L116)) | `AddPolygonalRegionToSurface` |
| `(int RegionId, int IdLastUsedNode) AddCircularRegionToSurface(int surfaceId, Node center, double radius, int divisions, RegionType regionType, int idLastUsedNode = 0)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L119)) | Delegation / managed logic; see implementation |
| `int AddCircularRegionToSurface(int surfaceId, IEnumerable<Node> regionNodes, RegionType regionType)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L122)) | Delegation / managed logic; see implementation |
| `int AddCircularRegionToSurface(int surfaceId, double xCenter, double yCenter, double zCenter, double rRegion, int nDivisions, int density, RegionType regionType)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L125)) | `AddCircularRegionToSurface` |
| `int DefineParametricSurface(string surfaceName, IEnumerable<Node> vertices, SurfaceType type = SurfaceType.None, AutoGenerate autoGenerate = AutoGenerate.No)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L128)) | Delegation / managed logic; see implementation |
| `int DefineParametricSurface(string surfaceName, Node originNode, Node localXVertex, Node localYVertex, IEnumerable<Node> vertices, SurfaceType type = SurfaceType.None, AutoGenerate autoGenerate = AutoGenerate.No)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L131)) | Delegation / managed logic; see implementation |
| `int DefineParametricSurface(string surfaceName, SurfaceType type, int idOriginNode, int idLocalXVertex, int idLocalYVertex, int nVertices, int[] idsVertices, int autoGenerate)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L134)) | `DefineParametricSurface` |
| `IOSGeometry ClearNodeSelection()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L141)) | `ClearNodeSelection` |
| `IOSGeometry ClearMemberSelection()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L143)) | `ClearMemberSelection` |
| `IOSGeometry ClearPhysicalMemberSelection()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L145)) | `ClearPhysicalMemberSelection` |
| `IOSGeometry ClearPlateSelection()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L147)) | `ClearPlateSelection` |
| `IOSGeometry ClearSolidSelection()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L149)) | `ClearSolidSelection` |
| `IOSGeometry ClearSurfaceSelection()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L151)) | `ClearSurfaceSelection` |
| `IOSGeometry SetNodeCoordinate(int id, double x, double y, double z)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L153)) | `SetNodeCoordinate` |
| `bool SelectMultipleNodes(IEnumerable<Node> nodes)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L156)) | Delegation / managed logic; see implementation |
| `bool SelectMultipleBeams(IEnumerable<Beam> beams)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L158)) | Delegation / managed logic; see implementation |
| `IOSGeometry SelectMultiplePhysicalMembers(IEnumerable<Member> members)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L160)) | Delegation / managed logic; see implementation |
| `bool SelectMultiplePlates(IEnumerable<Plate> plates)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L162)) | Delegation / managed logic; see implementation |
| `bool SelectMultipleNodes(IEnumerable<int> nodeIds)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L165)) | `SelectMultipleNodes` |
| `bool SelectMultipleBeams(IEnumerable<int> beamIds)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L167)) | `SelectMultipleBeams` |
| `bool SelectMultiplePlates(IEnumerable<int> plateIds)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L169)) | `SelectMultiplePlates` |
| `int SelectMultipleSolids(IEnumerable<int> solidIds)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L171)) | `SelectMultipleSolids` |
| `IOSGeometry SelectMultiplePhysicalMembers(IEnumerable<int> memberIds)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L173)) | `SelectMultiplePhysicalMembers` |
| `int GetLastNodeNo()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L176)) | `GetLastNodeNo` |
| `int GetLastBeamNo()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L178)) | `GetLastBeamNo` |
| `int GetLastPlateNo()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L180)) | `GetLastPlateNo` |
| `int AddNode(Node node)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L183)) | `AddNode` |
| `bool AnyItemsSelected()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L185)) | `GetNoOfSelectedNodes`, `GetNoOfSelectedBeams`, `GetNoOfSelectedPlates`, `GetNoOfSelectedSolids`, `GetNoOfSelectedSurfaces`, `GetNoOfSelectedPhysicalMembers` |
| `IOSGeometry CreateBeam(Beam beam)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L188)) | `CreateBeam` |
| `IOSGeometry CreateGroupFrom(EntityGroup entityGroup)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L190)) | `CreateGroupEx` |
| `IOSGeometry CreateGroupFrom(IEnumerable<EntityGroup> entityGroups)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L192)) | Delegation / managed logic; see implementation |
| `IOSGeometry CreateMultiplePlates(HashSet<Plate> plates, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L195)) | `CreatePlate` |
| `IOSGeometry CreateNode(Node node)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L197)) | `CreateNode` |
| `IOSGeometry CreatePlate(Plate plate)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L199)) | `CreatePlate` |
| `IEnumerable<int> GetAllBeamsList()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L202)) | `GetMemberCount`, `GetBeamList` |
| `IEnumerable<int> GetAllNodesList()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L204)) | `GetNodeCount`, `GetNodeList` |
| `IEnumerable<int> GetAllPlatesList()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L206)) | `GetPlateCount`, `GetPlateList` |
| `IEnumerable<int> GetAllSolidsList()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L208)) | `GetSolidCount`, `GetSolidList` |
| `Beam GetBeam(int beamNumber)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L211)) | `GetMemberIncidence` |
| `Beam GetBeam(int bId, IEnumerable<Beam> fromModelNodes)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L213)) | Delegation / managed logic; see implementation |
| `Beam GetBeam(int bId, IEnumerable<Node> fromModelNodes)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L215)) | `GetMemberIncidence` |
| `IEnumerable<Beam> GetBeamsConnectedAtNode(Node node)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L217)) | Delegation / managed logic; see implementation |
| `IEnumerable<Plate> GetPlatesConnectedAtNode(int nodeId)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L220)) | Delegation / managed logic; see implementation |
| `IEnumerable<Plate> GetPlatesConnectedAtNode(Node node)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L222)) | Delegation / managed logic; see implementation |
| `IEnumerable<Plate> GetPlatesConnectedAtNode(int nodeId, IEnumerable<Plate> allPlates)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L224)) | Delegation / managed logic; see implementation |
| `IEnumerable<Plate> GetPlatesConnectedAtNode(Node node, IEnumerable<Plate> allPlates)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L226)) | Delegation / managed logic; see implementation |
| `HashSet<T> GetAllEntities<T>(int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L229)) | Delegation / managed logic; see implementation |
| `HashSet<int> GetAllEntitiesIds<T>()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L231)) | Delegation / managed logic; see implementation |
| `IEnumerable<string> GetAllGroupNames()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L233)) | Delegation / managed logic; see implementation |
| `HashSet<T> GetEntities<T>(IEnumerable<int> entitiesIds, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L236)) | Delegation / managed logic; see implementation |
| `HashSet<T> GetEntitiesInGroup<T>(string groupName, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L238)) | Delegation / managed logic; see implementation |
| `HashSet<T> GetEntitiesInGroups<T>(IEnumerable<string> groupNames, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L240)) | Delegation / managed logic; see implementation |
| `HashSet<EntityGroup<T>> GetEntityGroups<T>(int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L243)) | Delegation / managed logic; see implementation |
| `HashSet<EntityGroup> GetEntityGroups<T>(string groupNameContaining, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L245)) | Delegation / managed logic; see implementation |
| `HashSet<EntityGroup<T>> GetEntityGroupsStartingWith<T>(string groupNameStartsWith, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L247)) | Delegation / managed logic; see implementation |
| `IEnumerable<string> GetGroupNames(GroupType groupType)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L250)) | `GetGroupCount`, `GetGroupNames` |
| `IEnumerable<int> GetIdsOfEntitiesInGroup(string groupName)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L252)) | `GetGroupEntityCount`, `GetGroupEntities` |
| `Node GetNode(int nId)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L255)) | `GetNodeCoordinates` |
| `Node GetNode(int nId, IEnumerable<Node> fromModelNodes)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L257)) | Delegation / managed logic; see implementation |
| `Plate GetPlate(int pId)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L260)) | `GetPlateIncidence` |
| `Plate GetPlate(int pId, IEnumerable<Node> fromModelNodes)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L262)) | `GetPlateIncidence` |
| `IEnumerable<Node> GetPlateIncidence(int pId)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L264)) | `GetPlateIncidence` |
| `HashSet<Beam> GetSelectedBeamsExt()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L267)) | Delegation / managed logic; see implementation |
| `HashSet<Beam> GetSelectedBeamsExt(IEnumerable<Beam> fromModelBeams)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L269)) | Delegation / managed logic; see implementation |
| `HashSet<Beam> GetSelectedBeamsExt(IEnumerable<Node> fromModelNodes)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L271)) | Delegation / managed logic; see implementation |
| `HashSet<int> GetSelectedBeamsIds()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L273)) | `GetNoOfSelectedBeams`, `GetSelectedBeams` |
| `HashSet<T> GetSelectedEntities<T>()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L276)) | Delegation / managed logic; see implementation |
| `HashSet<T> GetSelectedEntities<T>(IEnumerable<Node> fromModelNodes)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L278)) | Delegation / managed logic; see implementation |
| `HashSet<int> GetSelectedEntitiesIds<T>()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L280)) | Delegation / managed logic; see implementation |
| `HashSet<Node> GetSelectedNodesExt()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L283)) | Delegation / managed logic; see implementation |
| `HashSet<Node> GetSelectedNodesExt(IEnumerable<Node> fromModelNodes)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L285)) | Delegation / managed logic; see implementation |
| `HashSet<int> GetSelectedNodesIds()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L287)) | `GetNoOfSelectedNodes`, `GetSelectedNodes` |
| `HashSet<Plate> GetSelectedPlatesExt()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L290)) | Delegation / managed logic; see implementation |
| `HashSet<Plate> GetSelectedPlatesExt(IEnumerable<Node> fromModelNodes)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L292)) | Delegation / managed logic; see implementation |
| `HashSet<int> GetSelectedPlatesIds()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L294)) | `GetNoOfSelectedPlates`, `GetSelectedPlates` |
| `IEnumerable<Node> GetUniqueNodesFromPlateGroup(string platesGroupName, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L297)) | Delegation / managed logic; see implementation |
| `IOSGeometry CreateMultipleBeams(HashSet<Beam> beams, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L304)) | `CreateBeam` |
| `IOSGeometry CreateMultipleNodes(HashSet<Node> nodes, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L306)) | `CreateNode` |
| `IOSGeometry DeleteAllBeams(int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L308)) | `DeleteBeam` |
| `IOSGeometry DeleteAllNodes(int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L310)) | `DeleteNode` |
| `IOSGeometry DeleteAllPlates(int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L312)) | `DeletePlate` |
| `IOSGeometry DeleteAllSolids(int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L314)) | `DeleteSolid` |
| `IOSGeometry DeleteExistingGeometry(int nThreads = 1)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L316)) | Delegation / managed logic; see implementation |
| `List<(int NodeId, double Fx, double Fz)> DistributeTorque(string nodeGroupName, double torqueToBeDistributed, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L319)) | Delegation / managed logic; see implementation |
| `HashSet<T> GetAllEntities<T>(IEnumerable<Node> allNodes, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L322)) | Delegation / managed logic; see implementation |
| `HashSet<T> GetEntities<T>(IEnumerable<int> entitiesIds, IEnumerable<Node> allNodes, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L324)) | Delegation / managed logic; see implementation |
| `HashSet<T> GetEntitiesInGroup<T>(string groupName, HashSet<T> allEntities, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L326)) | `GetGroupEntityCount`, `GetGroupEntities` |
| `HashSet<EntityGroup<T>> GetEntityGroups<T>(HashSet<T> allEntities, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L328)) | `GetGroupCount`, `GetGroupNames` |
| `IEnumerable<int> GetIdsOfEntitiesInGroups(IEnumerable<string> groupNames, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L331)) | Delegation / managed logic; see implementation |
| `IEnumerable<Beam> GetBeamsConnectedAtNode(Node node, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L333)) | `GetNoOfBeamsConnectedAtNode`, `GetBeamsConnectedAtNode` |
| `IEnumerable<Beam> GetBeamsWithNoGroupAssignment(int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L335)) | Delegation / managed logic; see implementation |
| `IEnumerable<Plate> GetPlatesWithNoGroupAssignment(int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L337)) | Delegation / managed logic; see implementation |
| `Task<List<(int NodeId, double Fx, double Fz)>> DistributeTorqueAsync(string nodeGroupName, double torqueToBeDistributed, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L344)) | Delegation / managed logic; see implementation |
| `Task<int[]> GetAllBeamsListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L346)) | `GetMemberCount`, `GetBeamList` |
| `Task<HashSet<T>> GetAllEntitiesAsync<T>(int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L348)) | Delegation / managed logic; see implementation |
| `Task<HashSet<int>> GetAllEntitiesIdsAsync<T>()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L350)) | Delegation / managed logic; see implementation |
| `Task<int[]> GetAllNodesListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L352)) | `GetNodeCount`, `GetNodeList` |
| `Task<int[]> GetAllPlatesListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L354)) | `GetPlateCount`, `GetPlateList` |
| `Task<IEnumerable<Plate>> GetPlatesConnectedAtNodeAsync(int nodeId)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L357)) | Delegation / managed logic; see implementation |
| `Task<IEnumerable<Plate>> GetPlatesConnectedAtNodeAsync(Node node)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L359)) | Delegation / managed logic; see implementation |
| `Task<IEnumerable<Plate>> GetPlatesConnectedAtNodeAsync(int nodeId, IEnumerable<Plate> allPlates)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L361)) | Delegation / managed logic; see implementation |
| `Task<IEnumerable<Plate>> GetPlatesConnectedAtNodeAsync(Node node, IEnumerable<Plate> allPlates)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L363)) | Delegation / managed logic; see implementation |
| `Task<int[]> GetAllSolidsListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L366)) | `GetSolidCount`, `GetSolidList` |
| `Task<HashSet<T>> GetEntitiesAsync<T>(IEnumerable<int> entitiesIds, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L368)) | Delegation / managed logic; see implementation |
| `Task<HashSet<T>> GetEntitiesInGroupAsync<T>(string groupName, HashSet<T> allEntities, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L370)) | `GetGroupEntityCount`, `GetGroupEntities` |
| `Task<HashSet<T>> GetEntitiesInGroupAsync<T>(string groupName, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L372)) | Delegation / managed logic; see implementation |
| `Task<HashSet<T>> GetEntitiesInGroupsAsync<T>(IEnumerable<string> groupNames, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L374)) | Delegation / managed logic; see implementation |
| `Task<HashSet<EntityGroup<T>>> GetEntityGroupsAsync<T>(HashSet<T> allEntities, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L376)) | `GetGroupCount`, `GetGroupNames` |
| `Task<HashSet<EntityGroup<T>>> GetEntityGroupsAsync<T>(int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L378)) | Delegation / managed logic; see implementation |
| `Task<HashSet<EntityGroup>> GetEntityGroupsAsync<T>(string groupNameContaining, int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L380)) | Delegation / managed logic; see implementation |
| `Task<int[]> GetSelectedBeamsListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L383)) | `GetNoOfSelectedBeams`, `GetSelectedBeams` |
| `Task<HashSet<T>> GetSelectedEntitiesAsync<T>(int nThreads)` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L385)) | Delegation / managed logic; see implementation |
| `Task<HashSet<int>> GetSelectedEntitiesIdsAsync<T>()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L387)) | Delegation / managed logic; see implementation |
| `Task<int[]> GetSelectedNodesListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L389)) | `GetNoOfSelectedNodes`, `GetSelectedNodes` |
| `Task<int[]> GetSelectedPlatesListAsync()` ([source](../../src/StaadPro.Interop/Adapters/Interfaces/IOSGeometry.cs#L391)) | `GetNoOfSelectedPlates`, `GetSelectedPlates` |

## 4. Fingerprints and validation boundaries

All 24 reviewed help-page fingerprints are retained here and in the comparison data. Project-number headers identify documentation builds, not the version of an acquired runtime instance.

| Year | Page | Bytes | SHA-256 |
| --- | --- | ---: | --- |
| 2024 | [group___geometry_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___geometry_group.html) | 24,703 | `914D1210B82F48DC25DB937152F47A96CFFDFA57CBD5594261F544789C4F706E` |
| 2024 | [group___node_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___node_group.html) | 90,541 | `BAF595AF54F6E70FA1662C1997FC33D785DBD0AAED4F16C7DDA2DB54254F0D6F` |
| 2024 | [group___mem_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___mem_group.html) | 135,477 | `111E02E6D5916A5AEC5949F9417BBFCFED2059955331629C5E4FB45CE2A5E0F9` |
| 2024 | [group___phy_mem_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___phy_mem_group.html) | 51,352 | `BEE41CF9F544293B2E0804E8C393B752DAC3B5161913CA3045FA92626978EC28` |
| 2024 | [group___plate_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___plate_group.html) | 76,853 | `22B592102324E348437F0715032FB8F8CDCC7C565B5C8047704B96647B8099CE` |
| 2024 | [group___solid_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___solid_group.html) | 69,194 | `E6C9592A57EE53567C755C37C376C6C6FB2B5BF2EACBE8BE8BC1C91A0C3974BD` |
| 2024 | [group___group_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___group_group.html) | 41,449 | `95216E0C101E82FB423894542252562232FECC40E57BA773C13D2799928A0F3E` |
| 2024 | [group___parametric_surface_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202024/OSAPP_Help/group___parametric_surface_group.html) | 98,246 | `DA8CE00A9500127DAC7799450776CB887F88BC975FFBCEE87F3ABA349C70AE38` |
| 2025 | [group___geometry_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___geometry_group.html) | 25,679 | `A1C987C2ADC0E37A77FB0A6839896103111F6078262235C73DD50F23B99B9E56` |
| 2025 | [group___node_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___node_group.html) | 104,360 | `ADF7B0C8EDE38660DE125EA1A721DCAF0BD464EC01F4204CDFA4F5613E86D4CA` |
| 2025 | [group___mem_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___mem_group.html) | 152,191 | `F30EFFD365771BAAEC163F7C46B5B509A10C09AB805C9A9314424C956F01B882` |
| 2025 | [group___phy_mem_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___phy_mem_group.html) | 55,461 | `8601042BCC85B9309DD449FFAE0970EA0B6C1591A37E4050ED013F5E0968C66F` |
| 2025 | [group___plate_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___plate_group.html) | 86,637 | `CBAF1A8D7763AB87DA10A785EE8DCC1172DBB64C2D0CF8B17E5607474B707CA0` |
| 2025 | [group___solid_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___solid_group.html) | 79,439 | `E2D8608659A0DDCC81AED9569CC477C62FA213D30C229C0022E7FC6B7F72836E` |
| 2025 | [group___group_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___group_group.html) | 46,923 | `8205614C3EE0AB4F718850AE0BC610B73E90DA74223CD3F1C91EBFB665127B4E` |
| 2025 | [group___parametric_surface_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202025/OSAPP_Help/group___parametric_surface_group.html) | 103,637 | `ADCEF3C18FB7C9F6F7C32127C9A03533C104358CA9FEE74C6ECF36834594F697` |
| 2026 | [group___geometry_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___geometry_group.html) | 25,679 | `487F3B0DF9E1485E03FC46040F5B90F1CF70ADDC9460512402ABE71F209E3081` |
| 2026 | [group___node_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___node_group.html) | 104,360 | `78B2C28A6EF564D535B8A76B961C3AF43A0138F1B9AA926021B2AD75FE43B0BA` |
| 2026 | [group___mem_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___mem_group.html) | 152,191 | `DD91B279D916110D55C124A4ECD88B45C1CAB4C3F68DA2F31A43AB2A144C2496` |
| 2026 | [group___phy_mem_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___phy_mem_group.html) | 55,461 | `4D741651DB76AE343E6E555C737A8B4628823D6D75482088AB6C805C19A1284F` |
| 2026 | [group___plate_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___plate_group.html) | 86,637 | `817748DEBE79974588865C795AE24AAF4197BC2CE1F3CC25B15C59477A3F309C` |
| 2026 | [group___solid_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___solid_group.html) | 79,439 | `C31A574750C4E26B538DB338B8E137113758E3816AC58C6132D82D4FAD45BF46` |
| 2026 | [group___group_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___group_group.html) | 46,923 | `83606C0B9B51EF54AE5591044A453E943FF74B1331B57A3AAF7BFA4CFA4DE11B` |
| 2026 | [group___parametric_surface_group.html](../../../../../../../../Program%20Files/Bentley/Engineering/STAAD.Pro%202026/OSAPP_Help/group___parametric_surface_group.html) | 103,637 | `818B62B03D3C30B3C063A1C6D3B5DB000DD29584FDFF79C5BBCDB615D13CAC03` |

The comparison checks all 135 names in each reference, 134 detailed blocks and one deprecated summary, all 143 interface/adapter overload pairs, 176 distinct vendor metadata methods in each inspected year, and every current native call argument count/ref shape against the inspected 2025/2026 declarations. It does not execute COM, inspect the running object, test engineering results, change code or publish a package.
