# OpenSTAAD to IDEA StatiCa: complete source API and contract inventory

**Reviewed:** 23 October 2026<br>**Status:** Planning only. No typed integration methods are implemented.

This is an inventory of the strictly read-only main project, not a fictitious native IDEA automation group. Read the [integration architecture and plan](OpenSTAAD-IDEA-StatiCa-Architecture-and-Porting-Plan.md) for compatibility, engineering and package gates.

## 1. Surface counts and preserved boundaries

| Measure | Finding |
| --- | --- |
| Main interface / public adapter methods | 24 / 24 |
| Main private adapter methods | 54 |
| Main constructors | 2 (wrapper; wrapper plus options) |
| Main interface property | Options |
| Current placeholder public property | Inherited Wrapper |
| Current extraction / mapping / export methods | 0 |
| Related integration model files / service files | 7 / 12 |
| Inventoried public source type declarations | 54 |
| Inventoried property declarations | 353 |

Public source helper types are evidence to review, not an instruction to expose all helpers in the new package. DTOs/enums necessary for public contracts remain public; implementation services stay internal. Existing wrapper construction/bindings are unchanged.

## 2. Every source overload and implementation status

| Source managed signature | Reference | Workstream / current status |
| --- | --- | --- |
| `IReadOnlyList<Plate> GetPlatesConnectedToMember(int memberNumber)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L18) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L65) | I2 / pending |
| `IReadOnlyList<Plate> GetPlatesConnectedToMember(Beam member)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L20) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L72) | I2 / pending |
| `IReadOnlyList<IdeaStatiCaMemberForcesResult> GetMemberEndForces(int memberNumber, int targetJointNumber, IEnumerable<int> loadCaseIds)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L22) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L85) | I2 / pending |
| `IReadOnlyList<IdeaStatiCaMemberForcesResult> GetMemberEndForces(Beam member, Node targetJoint, IEnumerable<ILoadCase> loadCases)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L27) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L99) | I2 / pending |
| `IReadOnlyList<IdeaStatiCaMemberForcesResult> GetMemberEndForcesWithConnectedPlateForces(int memberNumber, int targetJointNumber, IEnumerable<int> loadCaseIds)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L32) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L119) | I2 / pending |
| `IReadOnlyList<IdeaStatiCaMemberForcesResult> GetMemberEndForcesWithConnectedPlateForces(Beam member, Node targetJoint, IEnumerable<ILoadCase> loadCases)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L37) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L133) | I2 / pending |
| `IReadOnlyList<IdeaStatiCaMemberForcesResult> GetJointForces(int jointNumber, IEnumerable<int> loadCaseIds)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L42) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L154) | I2 / pending |
| `IReadOnlyList<IdeaStatiCaMemberForcesResult> GetJointForces(Node joint, IEnumerable<ILoadCase> loadCases)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L46) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L164) | I2 / pending |
| `IReadOnlyList<IdeaStatiCaJointEquilibriumResult> GetJointEquilibrium(int jointNumber, IEnumerable<int> loadCaseIds)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L50) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L234) | I3 / pending |
| `IReadOnlyList<IdeaStatiCaJointEquilibriumResult> GetJointEquilibrium(Node joint, IEnumerable<ILoadCase> loadCases)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L54) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L244) | I3 / pending |
| `IdeaConnectionJointLoadPackage GetMappedJointLoadPackage(int jointNumber, IEnumerable<int> loadCaseIds, IdeaConnectionJointMapping mapping)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L62) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L365) | I4 / pending |
| `string ExportMappedJointForcesToExcel(IdeaConnectionJointLoadPackage package, string outputFilePath, bool allowUnsafeExport = false)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L68) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L496) | I5 / pending |
| `string ExportMappedJointForcesToExcel(int jointNumber, IEnumerable<int> loadCaseIds, IdeaConnectionJointMapping mapping, string outputFilePath, bool allowUnsafeExport = false)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L74) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L527) | I5 / pending |
| `string ExportMappedJointForcesToExcel(int jointNumber, IEnumerable<int> loadCaseIds, IdeaConnectionJointMapping mapping, string outputFilePath, string staadModelPath, string ideaMappingSourcePath, bool allowUnsafeExport = false)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L82) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L540) | I5 / pending |
| `string ExportJointForcesToExcel(int jointNumber, IEnumerable<int> loadCaseIds, string outputFilePath)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L91) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L574) | I5 / pending |
| `string ExportJointForcesToExcel(int jointNumber, IEnumerable<int> loadCaseIds, string outputFilePath, string staadModelPath)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L97) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L587) | I5 / pending |
| `string ExportJointEquilibriumVerificationToHtml(int jointNumber, IEnumerable<int> loadCaseIds, string outputFilePath, string staadModelPath, string ideaCheckbotPath)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L103) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L619) | I5 / pending |
| `string ExportMappedJointEquilibriumVerificationToHtml(int jointNumber, IEnumerable<int> loadCaseIds, IdeaConnectionJointMapping mapping, string outputFilePath, string staadModelPath, string ideaCheckbotPath)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L114) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L658) | I5 / pending |
| `IdeaStatiCaJointTopologyDiagram GetJointTopologyDiagram(int jointNumber, IEnumerable<int> loadCaseIds, IdeaConnectionJointMapping mapping = null)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L126) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L707) | I4 / pending |
| `IdeaStatiCaBatchExportResult ExportJointBatch(IdeaStatiCaBatchExportRequest request)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L135) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L721) | I6 / pending |
| `IReadOnlyList<int> GetJointIdsFromNodeGroup(string groupName)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L138) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L853) | I6 / pending |
| `IReadOnlyList<int> DiscoverConnectionJoints(IdeaStatiCaJointDiscoveryOptions options)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L141) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L870) | I6 / pending |
| `IReadOnlyList<IdeaStatiCaJointBatchPreview> PreviewJointBatch(IEnumerable<int> jointIds)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L144) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L899) | I6 / pending |
| `string ExportForcesToExcel(IEnumerable<IdeaStatiCaMemberForcesResult> results, string outputFilePath)` | [Interface](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L146) / [Adapter](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L937) | I5 / pending |

## 3. Per-overload parameters, returns and port treatment

<a id="idea-method-1"></a>

### 1. GetPlatesConnectedToMember

~~~csharp
IReadOnlyList<Plate> GetPlatesConnectedToMember(int memberNumber);
~~~

**Source meaning:** No method-level XML summary is supplied for this source overload; complete its managed contract before implementation.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L18) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L65)<br>**Port workstream:** I2; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `memberNumber` | `int` | Required | Not described in source XML; complete during API review. | Positive STAAD member ID, not a collection index; resolve from the acquired model. |

**Return:** `IReadOnlyList<Plate>`. No separate source returns text.

**Managed proposal:** Return an IdeaQueryResult wrapping the named owned data type above, with separate extraction/validation status. Reuse the existing source DTO meaning and ID/entity convenience overloads. Never use an empty collection or an equilibrium-corrected value as an undocumented failure signal.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-2"></a>

### 2. GetPlatesConnectedToMember

~~~csharp
IReadOnlyList<Plate> GetPlatesConnectedToMember(Beam member);
~~~

**Source meaning:** No method-level XML summary is supplied for this source overload; complete its managed contract before implementation.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L20) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L72)<br>**Port workstream:** I2; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `member` | `Beam` | Required | Not described in source XML; complete during API review. | Member entity belonging to the acquired wrapper/model; snapshot its geometry. |

**Return:** `IReadOnlyList<Plate>`. No separate source returns text.

**Managed proposal:** Return an IdeaQueryResult wrapping the named owned data type above, with separate extraction/validation status. Reuse the existing source DTO meaning and ID/entity convenience overloads. Never use an empty collection or an equilibrium-corrected value as an undocumented failure signal.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-3"></a>

### 3. GetMemberEndForces

~~~csharp
IReadOnlyList<IdeaStatiCaMemberForcesResult> GetMemberEndForces(int memberNumber, int targetJointNumber, IEnumerable<int> loadCaseIds);
~~~

**Source meaning:** No method-level XML summary is supplied for this source overload; complete its managed contract before implementation.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L22) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L85)<br>**Port workstream:** I2; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `memberNumber` | `int` | Required | Not described in source XML; complete during API review. | Positive STAAD member ID, not a collection index; resolve from the acquired model. |
| `targetJointNumber` | `int` | Required | Not described in source XML; complete during API review. | Positive target joint ID incident to the requested member. |
| `loadCaseIds` | `IEnumerable<int>` | Required | Not described in source XML; complete during API review. | Requested native case IDs; materialize once and validate availability and combination/result semantics. |

**Return:** `IReadOnlyList<IdeaStatiCaMemberForcesResult>`. No separate source returns text.

**Managed proposal:** Return an IdeaQueryResult wrapping the named owned data type above, with separate extraction/validation status. Reuse the existing source DTO meaning and ID/entity convenience overloads. Never use an empty collection or an equilibrium-corrected value as an undocumented failure signal.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-4"></a>

### 4. GetMemberEndForces

~~~csharp
IReadOnlyList<IdeaStatiCaMemberForcesResult> GetMemberEndForces(Beam member, Node targetJoint, IEnumerable<ILoadCase> loadCases);
~~~

**Source meaning:** No method-level XML summary is supplied for this source overload; complete its managed contract before implementation.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L27) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L99)<br>**Port workstream:** I2; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `member` | `Beam` | Required | Not described in source XML; complete during API review. | Member entity belonging to the acquired wrapper/model; snapshot its geometry. |
| `targetJoint` | `Node` | Required | Not described in source XML; complete during API review. | Target joint entity; require incidence to the member and the same model. |
| `loadCases` | `IEnumerable<ILoadCase>` | Required | Not described in source XML; complete during API review. | Resolved same-model cases; preserve case kind, factors and result identity. |

**Return:** `IReadOnlyList<IdeaStatiCaMemberForcesResult>`. No separate source returns text.

**Managed proposal:** Return an IdeaQueryResult wrapping the named owned data type above, with separate extraction/validation status. Reuse the existing source DTO meaning and ID/entity convenience overloads. Never use an empty collection or an equilibrium-corrected value as an undocumented failure signal.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-5"></a>

### 5. GetMemberEndForcesWithConnectedPlateForces

~~~csharp
IReadOnlyList<IdeaStatiCaMemberForcesResult> GetMemberEndForcesWithConnectedPlateForces(int memberNumber, int targetJointNumber, IEnumerable<int> loadCaseIds);
~~~

**Source meaning:** No method-level XML summary is supplied for this source overload; complete its managed contract before implementation.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L32) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L119)<br>**Port workstream:** I2; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `memberNumber` | `int` | Required | Not described in source XML; complete during API review. | Positive STAAD member ID, not a collection index; resolve from the acquired model. |
| `targetJointNumber` | `int` | Required | Not described in source XML; complete during API review. | Positive target joint ID incident to the requested member. |
| `loadCaseIds` | `IEnumerable<int>` | Required | Not described in source XML; complete during API review. | Requested native case IDs; materialize once and validate availability and combination/result semantics. |

**Return:** `IReadOnlyList<IdeaStatiCaMemberForcesResult>`. No separate source returns text.

**Managed proposal:** Return an IdeaQueryResult wrapping the named owned data type above, with separate extraction/validation status. Reuse the existing source DTO meaning and ID/entity convenience overloads. Never use an empty collection or an equilibrium-corrected value as an undocumented failure signal.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-6"></a>

### 6. GetMemberEndForcesWithConnectedPlateForces

~~~csharp
IReadOnlyList<IdeaStatiCaMemberForcesResult> GetMemberEndForcesWithConnectedPlateForces(Beam member, Node targetJoint, IEnumerable<ILoadCase> loadCases);
~~~

**Source meaning:** No method-level XML summary is supplied for this source overload; complete its managed contract before implementation.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L37) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L133)<br>**Port workstream:** I2; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `member` | `Beam` | Required | Not described in source XML; complete during API review. | Member entity belonging to the acquired wrapper/model; snapshot its geometry. |
| `targetJoint` | `Node` | Required | Not described in source XML; complete during API review. | Target joint entity; require incidence to the member and the same model. |
| `loadCases` | `IEnumerable<ILoadCase>` | Required | Not described in source XML; complete during API review. | Resolved same-model cases; preserve case kind, factors and result identity. |

**Return:** `IReadOnlyList<IdeaStatiCaMemberForcesResult>`. No separate source returns text.

**Managed proposal:** Return an IdeaQueryResult wrapping the named owned data type above, with separate extraction/validation status. Reuse the existing source DTO meaning and ID/entity convenience overloads. Never use an empty collection or an equilibrium-corrected value as an undocumented failure signal.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-7"></a>

### 7. GetJointForces

~~~csharp
IReadOnlyList<IdeaStatiCaMemberForcesResult> GetJointForces(int jointNumber, IEnumerable<int> loadCaseIds);
~~~

**Source meaning:** No method-level XML summary is supplied for this source overload; complete its managed contract before implementation.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L42) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L154)<br>**Port workstream:** I2; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `jointNumber` | `int` | Required | Not described in source XML; complete during API review. | Positive STAAD joint ID in the acquired model. |
| `loadCaseIds` | `IEnumerable<int>` | Required | Not described in source XML; complete during API review. | Requested native case IDs; materialize once and validate availability and combination/result semantics. |

**Return:** `IReadOnlyList<IdeaStatiCaMemberForcesResult>`. No separate source returns text.

**Managed proposal:** Return an IdeaQueryResult wrapping the named owned data type above, with separate extraction/validation status. Reuse the existing source DTO meaning and ID/entity convenience overloads. Never use an empty collection or an equilibrium-corrected value as an undocumented failure signal.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-8"></a>

### 8. GetJointForces

~~~csharp
IReadOnlyList<IdeaStatiCaMemberForcesResult> GetJointForces(Node joint, IEnumerable<ILoadCase> loadCases);
~~~

**Source meaning:** No method-level XML summary is supplied for this source overload; complete its managed contract before implementation.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L46) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L164)<br>**Port workstream:** I2; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `joint` | `Node` | Required | Not described in source XML; complete during API review. | Same-model joint entity; retain its coordinate/unit context. |
| `loadCases` | `IEnumerable<ILoadCase>` | Required | Not described in source XML; complete during API review. | Resolved same-model cases; preserve case kind, factors and result identity. |

**Return:** `IReadOnlyList<IdeaStatiCaMemberForcesResult>`. No separate source returns text.

**Managed proposal:** Return an IdeaQueryResult wrapping the named owned data type above, with separate extraction/validation status. Reuse the existing source DTO meaning and ID/entity convenience overloads. Never use an empty collection or an equilibrium-corrected value as an undocumented failure signal.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-9"></a>

### 9. GetJointEquilibrium

~~~csharp
IReadOnlyList<IdeaStatiCaJointEquilibriumResult> GetJointEquilibrium(int jointNumber, IEnumerable<int> loadCaseIds);
~~~

**Source meaning:** No method-level XML summary is supplied for this source overload; complete its managed contract before implementation.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L50) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L234)<br>**Port workstream:** I3; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `jointNumber` | `int` | Required | Not described in source XML; complete during API review. | Positive STAAD joint ID in the acquired model. |
| `loadCaseIds` | `IEnumerable<int>` | Required | Not described in source XML; complete during API review. | Requested native case IDs; materialize once and validate availability and combination/result semantics. |

**Return:** `IReadOnlyList<IdeaStatiCaJointEquilibriumResult>`. No separate source returns text.

**Managed proposal:** Return an IdeaQueryResult wrapping the named owned data type above, with separate extraction/validation status. Reuse the existing source DTO meaning and ID/entity convenience overloads. Never use an empty collection or an equilibrium-corrected value as an undocumented failure signal.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-10"></a>

### 10. GetJointEquilibrium

~~~csharp
IReadOnlyList<IdeaStatiCaJointEquilibriumResult> GetJointEquilibrium(Node joint, IEnumerable<ILoadCase> loadCases);
~~~

**Source meaning:** No method-level XML summary is supplied for this source overload; complete its managed contract before implementation.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L54) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L244)<br>**Port workstream:** I3; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `joint` | `Node` | Required | Not described in source XML; complete during API review. | Same-model joint entity; retain its coordinate/unit context. |
| `loadCases` | `IEnumerable<ILoadCase>` | Required | Not described in source XML; complete during API review. | Resolved same-model cases; preserve case kind, factors and result identity. |

**Return:** `IReadOnlyList<IdeaStatiCaJointEquilibriumResult>`. No separate source returns text.

**Managed proposal:** Return an IdeaQueryResult wrapping the named owned data type above, with separate extraction/validation status. Reuse the existing source DTO meaning and ID/entity convenience overloads. Never use an empty collection or an equilibrium-corrected value as an undocumented failure signal.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-11"></a>

### 11. GetMappedJointLoadPackage

~~~csharp
IdeaConnectionJointLoadPackage GetMappedJointLoadPackage(int jointNumber, IEnumerable<int> loadCaseIds, IdeaConnectionJointMapping mapping);
~~~

**Source meaning:** Maps verified STAAD-global joint actions to actual IDEA member bases, load positions, application points, and active model-type components, then predicts IDEA equilibrium.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L62) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L365)<br>**Port workstream:** I4; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `jointNumber` | `int` | Required | Not described in source XML; complete during API review. | Positive STAAD joint ID in the acquired model. |
| `loadCaseIds` | `IEnumerable<int>` | Required | Not described in source XML; complete during API review. | Requested native case IDs; materialize once and validate availability and combination/result semantics. |
| `mapping` | `IdeaConnectionJointMapping` | Required | Not described in source XML; complete during API review. | Validated mapping for this exact joint and incident members, including basis, load position and active components. |

**Return:** `IdeaConnectionJointLoadPackage`. No separate source returns text.

**Managed proposal:** Return an IdeaQueryResult wrapping the named owned data type above, with separate extraction/validation status. Reuse the existing source DTO meaning and ID/entity convenience overloads. Never use an empty collection or an equilibrium-corrected value as an undocumented failure signal.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-12"></a>

### 12. ExportMappedJointForcesToExcel

~~~csharp
string ExportMappedJointForcesToExcel(IdeaConnectionJointLoadPackage package, string outputFilePath, bool allowUnsafeExport = false);
~~~

**Source meaning:** Exports a prevalidated mapped package; unsafe export requires an explicit override.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L68) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L496)<br>**Port workstream:** I5; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `package` | `IdeaConnectionJointLoadPackage` | Required | Not described in source XML; complete during API review. | Owned mapped load package; validate model/mapping provenance, omitted components and equilibrium status before export. |
| `outputFilePath` | `string` | Required | Not described in source XML; complete during API review. | Destination artifact path; validate format, directory, overwrite policy, staging and completed delivery. |
| `allowUnsafeExport` | `bool` | `false` | Not described in source XML; complete during API review. | Explicit override of mapped export guards; default false. Preserve validation diagnostics and override provenance. |

**Return:** `string`. No separate source returns text.

**Managed proposal:** Return an IdeaExportResult (or the existing named batch result with explicit completion state) containing artifact paths, hashes, diagnostics and per-joint outcomes. Preserve explicit overwrite/unsafe-export/cancellation policy. A path string alone is insufficient completion evidence.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-13"></a>

### 13. ExportMappedJointForcesToExcel

~~~csharp
string ExportMappedJointForcesToExcel(int jointNumber, IEnumerable<int> loadCaseIds, IdeaConnectionJointMapping mapping, string outputFilePath, bool allowUnsafeExport = false);
~~~

**Source meaning:** Builds, validates, and exports an actual-IDEA-mapped joint load package.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L74) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L527)<br>**Port workstream:** I5; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `jointNumber` | `int` | Required | Not described in source XML; complete during API review. | Positive STAAD joint ID in the acquired model. |
| `loadCaseIds` | `IEnumerable<int>` | Required | Not described in source XML; complete during API review. | Requested native case IDs; materialize once and validate availability and combination/result semantics. |
| `mapping` | `IdeaConnectionJointMapping` | Required | Not described in source XML; complete during API review. | Validated mapping for this exact joint and incident members, including basis, load position and active components. |
| `outputFilePath` | `string` | Required | Not described in source XML; complete during API review. | Destination artifact path; validate format, directory, overwrite policy, staging and completed delivery. |
| `allowUnsafeExport` | `bool` | `false` | Not described in source XML; complete during API review. | Explicit override of mapped export guards; default false. Preserve validation diagnostics and override provenance. |

**Return:** `string`. No separate source returns text.

**Managed proposal:** Return an IdeaExportResult (or the existing named batch result with explicit completion state) containing artifact paths, hashes, diagnostics and per-joint outcomes. Preserve explicit overwrite/unsafe-export/cancellation policy. A path string alone is insufficient completion evidence.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-14"></a>

### 14. ExportMappedJointForcesToExcel

~~~csharp
string ExportMappedJointForcesToExcel(int jointNumber, IEnumerable<int> loadCaseIds, IdeaConnectionJointMapping mapping, string outputFilePath, string staadModelPath, string ideaMappingSourcePath, bool allowUnsafeExport = false);
~~~

**Source meaning:** Exports mapped forces with model and mapping provenance embedded in the workbook.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L82) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L540)<br>**Port workstream:** I5; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `jointNumber` | `int` | Required | Not described in source XML; complete during API review. | Positive STAAD joint ID in the acquired model. |
| `loadCaseIds` | `IEnumerable<int>` | Required | Not described in source XML; complete during API review. | Requested native case IDs; materialize once and validate availability and combination/result semantics. |
| `mapping` | `IdeaConnectionJointMapping` | Required | Not described in source XML; complete during API review. | Validated mapping for this exact joint and incident members, including basis, load position and active components. |
| `outputFilePath` | `string` | Required | Not described in source XML; complete during API review. | Destination artifact path; validate format, directory, overwrite policy, staging and completed delivery. |
| `staadModelPath` | `string` | Required | Not described in source XML; complete during API review. | Model file identity for provenance; does not open a different model or prove saved/live state equality. |
| `ideaMappingSourcePath` | `string` | Required | Not described in source XML; complete during API review. | Read-only mapping-source path for provenance and format validation. |
| `allowUnsafeExport` | `bool` | `false` | Not described in source XML; complete during API review. | Explicit override of mapped export guards; default false. Preserve validation diagnostics and override provenance. |

**Return:** `string`. No separate source returns text.

**Managed proposal:** Return an IdeaExportResult (or the existing named batch result with explicit completion state) containing artifact paths, hashes, diagnostics and per-joint outcomes. Preserve explicit overwrite/unsafe-export/cancellation policy. A path string alone is insufficient completion evidence.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-15"></a>

### 15. ExportJointForcesToExcel

~~~csharp
string ExportJointForcesToExcel(int jointNumber, IEnumerable<int> loadCaseIds, string outputFilePath);
~~~

**Source meaning:** No method-level XML summary is supplied for this source overload; complete its managed contract before implementation.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L91) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L574)<br>**Port workstream:** I5; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `jointNumber` | `int` | Required | Not described in source XML; complete during API review. | Positive STAAD joint ID in the acquired model. |
| `loadCaseIds` | `IEnumerable<int>` | Required | Not described in source XML; complete during API review. | Requested native case IDs; materialize once and validate availability and combination/result semantics. |
| `outputFilePath` | `string` | Required | Not described in source XML; complete during API review. | Destination artifact path; validate format, directory, overwrite policy, staging and completed delivery. |

**Return:** `string`. No separate source returns text.

**Managed proposal:** Return an IdeaExportResult (or the existing named batch result with explicit completion state) containing artifact paths, hashes, diagnostics and per-joint outcomes. Preserve explicit overwrite/unsafe-export/cancellation policy. A path string alone is insufficient completion evidence.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-16"></a>

### 16. ExportJointForcesToExcel

~~~csharp
string ExportJointForcesToExcel(int jointNumber, IEnumerable<int> loadCaseIds, string outputFilePath, string staadModelPath);
~~~

**Source meaning:** Exports generic forces with source-model provenance embedded in the workbook.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L97) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L587)<br>**Port workstream:** I5; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `jointNumber` | `int` | Required | Not described in source XML; complete during API review. | Positive STAAD joint ID in the acquired model. |
| `loadCaseIds` | `IEnumerable<int>` | Required | Not described in source XML; complete during API review. | Requested native case IDs; materialize once and validate availability and combination/result semantics. |
| `outputFilePath` | `string` | Required | Not described in source XML; complete during API review. | Destination artifact path; validate format, directory, overwrite policy, staging and completed delivery. |
| `staadModelPath` | `string` | Required | Not described in source XML; complete during API review. | Model file identity for provenance; does not open a different model or prove saved/live state equality. |

**Return:** `string`. No separate source returns text.

**Managed proposal:** Return an IdeaExportResult (or the existing named batch result with explicit completion state) containing artifact paths, hashes, diagnostics and per-joint outcomes. Preserve explicit overwrite/unsafe-export/cancellation policy. A path string alone is insufficient completion evidence.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-17"></a>

### 17. ExportJointEquilibriumVerificationToHtml

~~~csharp
string ExportJointEquilibriumVerificationToHtml(int jointNumber, IEnumerable<int> loadCaseIds, string outputFilePath, string staadModelPath, string ideaCheckbotPath);
~~~

**Source meaning:** No method-level XML summary is supplied for this source overload; complete its managed contract before implementation.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L103) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L619)<br>**Port workstream:** I5; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `jointNumber` | `int` | Required | Not described in source XML; complete during API review. | Positive STAAD joint ID in the acquired model. |
| `loadCaseIds` | `IEnumerable<int>` | Required | Not described in source XML; complete during API review. | Requested native case IDs; materialize once and validate availability and combination/result semantics. |
| `outputFilePath` | `string` | Required | Not described in source XML; complete during API review. | Destination artifact path; validate format, directory, overwrite policy, staging and completed delivery. |
| `staadModelPath` | `string` | Required | Not described in source XML; complete during API review. | Model file identity for provenance; does not open a different model or prove saved/live state equality. |
| `ideaCheckbotPath` | `string` | Required | Not described in source XML; complete during API review. | Read-only Checkbot/mapping source context; not an instruction to modify or launch an IDEA project. |

**Return:** `string`. No separate source returns text.

**Managed proposal:** Return an IdeaExportResult (or the existing named batch result with explicit completion state) containing artifact paths, hashes, diagnostics and per-joint outcomes. Preserve explicit overwrite/unsafe-export/cancellation policy. A path string alone is insufficient completion evidence.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-18"></a>

### 18. ExportMappedJointEquilibriumVerificationToHtml

~~~csharp
string ExportMappedJointEquilibriumVerificationToHtml(int jointNumber, IEnumerable<int> loadCaseIds, IdeaConnectionJointMapping mapping, string outputFilePath, string staadModelPath, string ideaCheckbotPath);
~~~

**Source meaning:** Exports the generic STAAD audit together with the actual IDEA mapping, applied mapped rows, active-component checks, and reconstructed equilibrium.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L114) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L658)<br>**Port workstream:** I5; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `jointNumber` | `int` | Required | Not described in source XML; complete during API review. | Positive STAAD joint ID in the acquired model. |
| `loadCaseIds` | `IEnumerable<int>` | Required | Not described in source XML; complete during API review. | Requested native case IDs; materialize once and validate availability and combination/result semantics. |
| `mapping` | `IdeaConnectionJointMapping` | Required | Not described in source XML; complete during API review. | Validated mapping for this exact joint and incident members, including basis, load position and active components. |
| `outputFilePath` | `string` | Required | Not described in source XML; complete during API review. | Destination artifact path; validate format, directory, overwrite policy, staging and completed delivery. |
| `staadModelPath` | `string` | Required | Not described in source XML; complete during API review. | Model file identity for provenance; does not open a different model or prove saved/live state equality. |
| `ideaCheckbotPath` | `string` | Required | Not described in source XML; complete during API review. | Read-only Checkbot/mapping source context; not an instruction to modify or launch an IDEA project. |

**Return:** `string`. No separate source returns text.

**Managed proposal:** Return an IdeaExportResult (or the existing named batch result with explicit completion state) containing artifact paths, hashes, diagnostics and per-joint outcomes. Preserve explicit overwrite/unsafe-export/cancellation policy. A path string alone is insufficient completion evidence.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-19"></a>

### 19. GetJointTopologyDiagram

~~~csharp
IdeaStatiCaJointTopologyDiagram GetJointTopologyDiagram(int jointNumber, IEnumerable<int> loadCaseIds, IdeaConnectionJointMapping mapping = null);
~~~

**Source meaning:** Builds a deterministic topology snapshot and accessible SVG diagram for one audited joint. The diagram uses the exact member bases and plate ownership used by force transformation.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L126) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L707)<br>**Port workstream:** I4; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `jointNumber` | `int` | Required | Not described in source XML; complete during API review. | Positive STAAD joint ID in the acquired model. |
| `loadCaseIds` | `IEnumerable<int>` | Required | Not described in source XML; complete during API review. | Requested native case IDs; materialize once and validate availability and combination/result semantics. |
| `mapping` | `IdeaConnectionJointMapping` | `null` | Not described in source XML; complete during API review. | Validated mapping for this exact joint and incident members, including basis, load position and active components. |

**Return:** `IdeaStatiCaJointTopologyDiagram`. No separate source returns text.

**Managed proposal:** Return an IdeaQueryResult wrapping the named owned data type above, with separate extraction/validation status. Reuse the existing source DTO meaning and ID/entity convenience overloads. Never use an empty collection or an equilibrium-corrected value as an undocumented failure signal.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-20"></a>

### 20. ExportJointBatch

~~~csharp
IdeaStatiCaBatchExportResult ExportJointBatch(IdeaStatiCaBatchExportRequest request);
~~~

**Source meaning:** Exports several joints as one provenance-controlled package containing a multi-sheet workbook, consolidated audit, detailed per-joint audits, topology diagrams, and a JSON manifest.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L135) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L721)<br>**Port workstream:** I6; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `request` | `IdeaStatiCaBatchExportRequest` | Required | Not described in source XML; complete during API review. | Validated batch request with joint/case selections, outputs, failure policy, mappings, progress and cancellation. |

**Return:** `IdeaStatiCaBatchExportResult`. No separate source returns text.

**Managed proposal:** Return an IdeaExportResult (or the existing named batch result with explicit completion state) containing artifact paths, hashes, diagnostics and per-joint outcomes. Preserve explicit overwrite/unsafe-export/cancellation policy. A path string alone is insufficient completion evidence.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-21"></a>

### 21. GetJointIdsFromNodeGroup

~~~csharp
IReadOnlyList<int> GetJointIdsFromNodeGroup(string groupName);
~~~

**Source meaning:** Returns node IDs from a declared STAAD node group.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L138) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L853)<br>**Port workstream:** I6; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `groupName` | `string` | Required | Not described in source XML; complete during API review. | Node-group name resolved against the acquired model; do not silently select another group. |

**Return:** `IReadOnlyList<int>`. No separate source returns text.

**Managed proposal:** Return an IdeaQueryResult wrapping the named owned data type above, with separate extraction/validation status. Reuse the existing source DTO meaning and ID/entity convenience overloads. Never use an empty collection or an equilibrium-corrected value as an undocumented failure signal.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-22"></a>

### 22. DiscoverConnectionJoints

~~~csharp
IReadOnlyList<int> DiscoverConnectionJoints(IdeaStatiCaJointDiscoveryOptions options);
~~~

**Source meaning:** Discovers connection candidates from live STAAD topology using explicit filters.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L141) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L870)<br>**Port workstream:** I6; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `options` | `IdeaStatiCaJointDiscoveryOptions` | Required | Not described in source XML; complete during API review. | Validated discovery filters/options; document geometric criteria and deduplication. |

**Return:** `IReadOnlyList<int>`. No separate source returns text.

**Managed proposal:** Return an IdeaQueryResult wrapping the named owned data type above, with separate extraction/validation status. Reuse the existing source DTO meaning and ID/entity convenience overloads. Never use an empty collection or an equilibrium-corrected value as an undocumented failure signal.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-23"></a>

### 23. PreviewJointBatch

~~~csharp
IReadOnlyList<IdeaStatiCaJointBatchPreview> PreviewJointBatch(IEnumerable<int> jointIds);
~~~

**Source meaning:** Previews topology validity and anticipated rows without extracting analysis forces.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L144) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L899)<br>**Port workstream:** I6; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `jointIds` | `IEnumerable<int>` | Required | Not described in source XML; complete during API review. | Selected native joint IDs; materialize, validate and deduplicate deterministically. |

**Return:** `IReadOnlyList<IdeaStatiCaJointBatchPreview>`. No separate source returns text.

**Managed proposal:** Return an IdeaQueryResult wrapping the named owned data type above, with separate extraction/validation status. Reuse the existing source DTO meaning and ID/entity convenience overloads. Never use an empty collection or an equilibrium-corrected value as an undocumented failure signal.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

<a id="idea-method-24"></a>

### 24. ExportForcesToExcel

~~~csharp
string ExportForcesToExcel(IEnumerable<IdeaStatiCaMemberForcesResult> results, string outputFilePath);
~~~

**Source meaning:** No method-level XML summary is supplied for this source overload; complete its managed contract before implementation.

**Reference:** [Interface XML contract](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L146) / [Implementation](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L937)<br>**Port workstream:** I5; pending.

| Managed input | Type | Default | Source parameter contract | Proposed managed meaning |
| --- | --- | --- | --- | --- |
| `results` | `IEnumerable<IdeaStatiCaMemberForcesResult>` | Required | Not described in source XML; complete during API review. | Owned force/result rows with explicit frames, units, case IDs and extraction/validation status. |
| `outputFilePath` | `string` | Required | Not described in source XML; complete during API review. | Destination artifact path; validate format, directory, overwrite policy, staging and completed delivery. |

**Return:** `string`. No separate source returns text.

**Managed proposal:** Return an IdeaExportResult (or the existing named batch result with explicit completion state) containing artifact paths, hashes, diagnostics and per-joint outcomes. Preserve explicit overwrite/unsafe-export/cancellation policy. A path string alone is insufficient completion evidence.

**Acceptance:** Validate owning model/case identity, coordinate frame and units; copy mutable source entities/options before computation; expose no native COM object, public ref/out or tuple. Source exceptions/status behavior must be documented deliberately rather than copied blindly.

## 4. Constructors, Options and configuration defaults

The two source constructors take OpenStaadWrapper, with an optional explicit OpenStaadIdeaStatiCaOptions object. The default constructor delegates to a new options instance. The source requires Geometry, Load, Output and Property; Support is additionally needed for requested reaction-aware paths. The current placeholder requires only Geometry/Load/Output. A future feature capability check must test real operations instead of non-null placeholders.

Options is a mutable source reference. Default field values are assigned in the source constructor rather than necessarily as property initializers; the source reference below is authoritative. Important defaults include DiagnosticSingleOwner closure, support reactions disabled, opposite mapped load position required, ConnectionLoadEffects workbook output and explicit absolute/relative tolerances. Snapshot options/dictionaries before an operation; the plan proposes StrictNoClosure as an initial public default subject to review.

### Source option-constructor defaults

| Option | Source default expression |
| --- | --- |
| `StaadToIdeaRotation` | `IdeaStatiCaForceRotation.Default` |
| `StaadToIdeaMomentConvention` | `IdeaStatiCaForceRotation.DefaultMomentConvention` |
| `PlateCornerContributionFactor` | `1.0` |
| `GlobalForceEquilibriumTolerance` | `1e-3` |
| `GlobalMomentEquilibriumTolerance` | `1e-3` |
| `GlobalForceEquilibriumRelativeTolerance` | `5e-6` |
| `GlobalMomentEquilibriumRelativeTolerance` | `5e-6` |
| `MaximumEquilibriumClosureForceRelative` | `5e-3` |
| `MaximumEquilibriumClosureMomentRelative` | `5e-3` |
| `IdeaMappingBasisTolerance` | `1e-8` |
| `IdeaMappingPointTolerance` | `1e-6` |
| `IdeaDisabledComponentTolerance` | `1e-9` |
| `EquilibriumClosurePolicy` | `IdeaEquilibriumClosurePolicyKind.DiagnosticSingleOwner` |
| `MemberClosureStiffnessWeights` | `new Dictionary<int, double>()` |
| `RequireOppositeIdeaLoadPositionToStaadJointEnd` | `true` |
| `ExcelForceSystem` | `IdeaStatiCaExcelForceSystem.ConnectionLoadEffects` |
| `MemberNamePrefix` | `"M"` |
| `LoadCaseNamePrefix` | `"LC"` |
| `PlateMemberAssignments` | `new Dictionary<int, int>()` |
| `IdeaMemberNames` | `new Dictionary<int, string>()` |

Values below are source defaults, not approved new-public defaults. Properties absent from this constructor retain their C# default unless another source initializer applies. The architecture plan recommends reviewing closure defaults separately.

## 5. Complete source model, enum and public helper surface

### 1. IOpenStaadIdeaStatiCaAdapter

**Kind:** InterfaceDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs#L13)

Retrieves STAAD member and plate actions and converts them into IDEA StatiCa force conventions.

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `Options` | `OpenStaadIdeaStatiCaOptions` | Gets the live adapter configuration. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 2. OpenStaadIdeaStatiCaAdapter

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L25)

High-level adapter for retrieving OpenSTAAD member/plate forces and producing IDEA StatiCa structural internal forces and Connection load effects.

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `Options` | `OpenStaadIdeaStatiCaOptions` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 3. IdeaConnectionEnd

**Kind:** EnumDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMapping.cs#L15)

| Enum member | Explicit source value | Source meaning |
| --- | --- | --- |
| `Begin` | 0 | No source XML meaning. |
| `End` | 1 | No source XML meaning. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 4. IdeaConnectionLoadComponents

**Kind:** EnumDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMapping.cs#L22)

| Enum member | Explicit source value | Source meaning |
| --- | --- | --- |
| `None` | 0 | No source XML meaning. |
| `N` | 1 | No source XML meaning. |
| `Vy` | 2 | No source XML meaning. |
| `Vz` | 4 | No source XML meaning. |
| `Mx` | 8 | No source XML meaning. |
| `My` | 16 | No source XML meaning. |
| `Mz` | 32 | No source XML meaning. |
| `All` | N \| Vy \| Vz \| Mx \| My \| Mz | No source XML meaning. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 5. IdeaConnectionCoordinates

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMapping.cs#L37)

JSON-friendly Cartesian coordinate or direction.

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `X` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `Y` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `Z` | `double` | Source does not specify XML meaning/default here; complete before port. |

| Source method signature | Reference |
| --- | --- |
| `Vector3D ToVector3D()` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMapping.cs#L52) |
| `Point3D ToPoint3D()` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMapping.cs#L53) |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 6. IdeaConnectionCoordinateSystem

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMapping.cs#L68)

Actual IDEA member-local basis and load-application point in global XYZ, metres.

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `OriginGlobal` | `IdeaConnectionCoordinates` | Source does not specify XML meaning/default here; complete before port. |
| `LocalXInGlobal` | `IdeaConnectionCoordinates` | Source does not specify XML meaning/default here; complete before port. |
| `LocalYInGlobal` | `IdeaConnectionCoordinates` | Source does not specify XML meaning/default here; complete before port. |
| `LocalZInGlobal` | `IdeaConnectionCoordinates` | Source does not specify XML meaning/default here; complete before port. |

| Source method signature | Reference |
| --- | --- |
| `void Validate(double tolerance = 1e-8)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMapping.cs#L83) |
| `Forces GlobalToLocal(Forces global)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMapping.cs#L113) |
| `Forces LocalToGlobal(Forces local)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMapping.cs#L129) |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 7. IdeaConnectionMemberMapping

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMapping.cs#L145)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `StaadMemberId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `IdeaMemberName` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `LoadPosition` | `IdeaConnectionEnd` | Source does not specify XML meaning/default here; complete before port. |
| `CoordinateSystem` | `IdeaConnectionCoordinateSystem` | Source does not specify XML meaning/default here; complete before port. |
| `ActiveComponents` | `IdeaConnectionLoadComponents` | Source does not specify XML meaning/default here; complete before port. |

| Source method signature | Reference |
| --- | --- |
| `void Validate(double basisTolerance = 1e-8)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMapping.cs#L160) |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 8. IdeaConnectionJointMapping

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMapping.cs#L176)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `StaadJointId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `JointPointGlobal` | `IdeaConnectionCoordinates` | Source does not specify XML meaning/default here; complete before port. |
| `Members` | `List<IdeaConnectionMemberMapping>` | Source does not specify XML meaning/default here; complete before port. |

| Source method signature | Reference |
| --- | --- |
| `void Validate(double basisTolerance = 1e-8)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMapping.cs#L188) |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 9. IdeaConnectionLoadEffectResult

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMapping.cs#L221)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `JointId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `LoadCaseId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `LoadCaseName` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `StaadMemberId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `StaadJointEnd` | `MemberEnd` | Source does not specify XML meaning/default here; complete before port. |
| `IdeaMemberName` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `LoadPosition` | `IdeaConnectionEnd` | Source does not specify XML meaning/default here; complete before port. |
| `ActiveComponents` | `IdeaConnectionLoadComponents` | Source does not specify XML meaning/default here; complete before port. |
| `CoordinateSystem` | `IdeaConnectionCoordinateSystem` | Source does not specify XML meaning/default here; complete before port. |
| `SourceActionsAtStaadJointGlobal` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `ActionsAtIdeaPointGlobal` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `IdeaMemberLocalEndActions` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `InternalForces` | `IdeaStatiCaInternalForces` | Source does not specify XML meaning/default here; complete before port. |
| `ConnectionLoadEffects` | `IdeaStatiCaInternalForces` | Source does not specify XML meaning/default here; complete before port. |
| `AppliedConnectionLoadEffects` | `IdeaStatiCaInternalForces` | Source does not specify XML meaning/default here; complete before port. |
| `OmittedConnectionLoadEffects` | `IdeaStatiCaInternalForces` | Source does not specify XML meaning/default here; complete before port. |
| `ReconstructedActionsAtStaadJointGlobal` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `FullRoundTripResidualAtStaadJointGlobal` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `HasNonzeroDisabledComponents` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `Position` | `string` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 10. IdeaConnectionMappedEquilibriumResult

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMapping.cs#L245)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `JointId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `LoadCaseId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `LoadCaseName` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `ReconstructedMemberActionsAtJointGlobalSum` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `AppliedNodalActionsAtJointGlobal` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `SupportActionsAtJointGlobal` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `IsSupportActionIncluded` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `ResidualGlobal` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `ForceActionScale` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `MomentActionScale` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `ForceTolerance` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `MomentTolerance` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `HasNonzeroDisabledComponents` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `MemberResults` | `IReadOnlyList<IdeaConnectionLoadEffectResult>` | Source does not specify XML meaning/default here; complete before port. |
| `ForceResidualMagnitude` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `MomentResidualMagnitude` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `IsForceEquilibriumSatisfied` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `IsMomentEquilibriumSatisfied` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `IsEquilibriumSatisfied` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `IsReadyForExport` | `bool` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 11. IdeaConnectionJointLoadPackage

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMapping.cs#L275)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `Mapping` | `IdeaConnectionJointMapping` | Source does not specify XML meaning/default here; complete before port. |
| `LoadEffects` | `IReadOnlyList<IdeaConnectionLoadEffectResult>` | Source does not specify XML meaning/default here; complete before port. |
| `EquilibriumResults` | `IReadOnlyList<IdeaConnectionMappedEquilibriumResult>` | Source does not specify XML meaning/default here; complete before port. |
| `IsReadyForExport` | `bool` | Source does not specify XML meaning/default here; complete before port. |

| Source method signature | Reference |
| --- | --- |
| `void ValidateForExport()` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMapping.cs#L292) |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 12. IdeaMappingProfileCompatibility

**Kind:** EnumDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMappingProfile.cs#L10)

| Enum member | Explicit source value | Source meaning |
| --- | --- | --- |
| `Exact` | 0 | No source XML meaning. |
| `GeometricallyCompatibleRename` | 1 | No source XML meaning. |
| `Changed` | 2 | No source XML meaning. |
| `Missing` | 3 | No source XML meaning. |
| `New` | 4 | No source XML meaning. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 13. IdeaConnectionMappingProfileJoint

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMappingProfile.cs#L20)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `Mapping` | `IdeaConnectionJointMapping` | Source does not specify XML meaning/default here; complete before port. |
| `PlateOwnerOverrides` | `IDictionary<int, int>` | Source does not specify XML meaning/default here; complete before port. |
| `ContinuityAliases` | `IDictionary<int, string>` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 14. IdeaConnectionMappingProfile

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMappingProfile.cs#L35)

Versioned, reusable IDEA mapping decisions tied to a STAAD model fingerprint.

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `Schema` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `ProfileId` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `Name` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `StaadModelFileName` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `StaadModelSha256` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `CreatedUtc` | `DateTime` | Source does not specify XML meaning/default here; complete before port. |
| `UpdatedUtc` | `DateTime` | Source does not specify XML meaning/default here; complete before port. |
| `Joints` | `List<IdeaConnectionMappingProfileJoint>` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 15. IdeaMappingProfileJointDiff

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMappingProfile.cs#L54)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `ProfileJointId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `CurrentJointId` | `int?` | Source does not specify XML meaning/default here; complete before port. |
| `Compatibility` | `IdeaMappingProfileCompatibility` | Source does not specify XML meaning/default here; complete before port. |
| `Diagnostic` | `string` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 16. IdeaMappingProfileDiff

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMappingProfile.cs#L62)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `IsModelFingerprintExact` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `Joints` | `IReadOnlyList<IdeaMappingProfileJointDiff>` | Source does not specify XML meaning/default here; complete before port. |
| `RequiresReview` | `bool` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 17. IdeaEquilibriumClosurePolicyKind

**Kind:** EnumDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaEquilibriumClosurePolicy.cs#L8)

| Enum member | Explicit source value | Source meaning |
| --- | --- | --- |
| `DiagnosticSingleOwner` | 0 | No source XML meaning. |
| `StrictNoClosure` | 1 | No source XML meaning. |
| `MagnitudeWeighted` | 2 | No source XML meaning. |
| `StiffnessWeighted` | 3 | No source XML meaning. |
| `ReactionAware` | 4 | No source XML meaning. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 18. IdeaEquilibriumClosureCarrier

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaEquilibriumClosurePolicy.cs#L19)

One eligible carrier used by an equilibrium-closure policy.

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `ResultIndex` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `MemberId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `HasPhysicalMember` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `ForceMagnitude` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `MomentMagnitude` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `StiffnessWeight` | `double` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 19. IdeaEquilibriumClosureAllocation

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaEquilibriumClosurePolicy.cs#L30)

Auditable output of a closure-policy evaluation.

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `Policy` | `IdeaEquilibriumClosurePolicyKind` | Source does not specify XML meaning/default here; complete before port. |
| `CorrectionsByResultIndex` | `IReadOnlyDictionary<int, Forces>` | Source does not specify XML meaning/default here; complete before port. |
| `IsClosureApplied` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `Diagnostic` | `string` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 20. IdeaStatiCaBatchFailurePolicy

**Kind:** EnumDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaBatchExport.cs#L8)

| Enum member | Explicit source value | Source meaning |
| --- | --- | --- |
| `Atomic` | 0 | No source XML meaning. |
| `Isolated` | 1 | No source XML meaning. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 21. IdeaExportPathDisclosure

**Kind:** EnumDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaBatchExport.cs#L14)

| Enum member | Explicit source value | Source meaning |
| --- | --- | --- |
| `FullPath` | 0 | No source XML meaning. |
| `FileNameOnly` | 1 | No source XML meaning. |
| `Redacted` | 2 | No source XML meaning. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 22. IdeaStatiCaBatchPhase

**Kind:** EnumDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaBatchExport.cs#L21)

| Enum member | Explicit source value | Source meaning |
| --- | --- | --- |
| `Validating` | Implicit; resolve declaration order | No source XML meaning. |
| `Extracting` | Implicit; resolve declaration order | No source XML meaning. |
| `WritingArtifacts` | Implicit; resolve declaration order | No source XML meaning. |
| `Completed` | Implicit; resolve declaration order | No source XML meaning. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 23. IdeaStatiCaBatchProgress

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaBatchExport.cs#L29)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `Phase` | `IdeaStatiCaBatchPhase` | Source does not specify XML meaning/default here; complete before port. |
| `CompletedJoints` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `TotalJoints` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `CurrentJointId` | `int?` | Source does not specify XML meaning/default here; complete before port. |
| `Message` | `string` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 24. IdeaStatiCaJointDiscoveryOptions

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaBatchExport.cs#L38)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `MinimumIncidentMemberCount` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `RequireConnectedPlate` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `RequireBothMembersAndPlates` | `bool` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 25. IdeaStatiCaJointBatchPreview

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaBatchExport.cs#L50)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `JointId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `IncidentMemberCount` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `ConnectedPlateCount` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `EstimatedRowsPerLoadCase` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `IsValid` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `Diagnostic` | `string` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 26. IdeaStatiCaBatchOutputOptions

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaBatchExport.cs#L60)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `WriteWorkbook` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `WriteIndividualJointWorkbooks` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `WriteIndividualJointAudits` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `WriteConsolidatedAudit` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `WriteManifest` | `bool` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 27. IdeaStatiCaBatchExportRequest

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaBatchExport.cs#L78)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `JointIds` | `IReadOnlyList<int>` | Source does not specify XML meaning/default here; complete before port. |
| `LoadCaseIds` | `IReadOnlyList<int>` | Source does not specify XML meaning/default here; complete before port. |
| `MappingsByJoint` | `IReadOnlyDictionary<int, IdeaConnectionJointMapping>` | Source does not specify XML meaning/default here; complete before port. |
| `FailurePolicy` | `IdeaStatiCaBatchFailurePolicy` | Source does not specify XML meaning/default here; complete before port. |
| `Output` | `IdeaStatiCaBatchOutputOptions` | Source does not specify XML meaning/default here; complete before port. |
| `PathDisclosure` | `IdeaExportPathDisclosure` | Source does not specify XML meaning/default here; complete before port. |
| `OutputWorkbookPath` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `IndividualJointArtifactRootDirectory` | `string` | Optional root for per-joint batch artifacts; when absent, legacy flat paths are retained. |
| `StaadModelPath` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `IdeaMappingSourcePath` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `CancellationToken` | `CancellationToken` | Source does not specify XML meaning/default here; complete before port. |
| `Progress` | `IProgress<IdeaStatiCaBatchProgress>` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 28. IdeaExportFileFingerprint

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaBatchExport.cs#L106)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `DisplayPath` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `FileName` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `ByteLength` | `long` | Source does not specify XML meaning/default here; complete before port. |
| `LastWriteTimeUtc` | `DateTime` | Source does not specify XML meaning/default here; complete before port. |
| `Algorithm` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `Sha256` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `IsDirectoryManifest` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `ManifestFileCount` | `int` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 29. IdeaExportArtifactFingerprint

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaBatchExport.cs#L118)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `Kind` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `Path` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `MediaType` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `ByteLength` | `long` | Source does not specify XML meaning/default here; complete before port. |
| `Sha256` | `string` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 30. IdeaExportLoadCaseEvidence

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaBatchExport.cs#L127)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `Id` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `Name` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `CaseType` | `string` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 31. IdeaExportProvenance

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaBatchExport.cs#L134)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `Schema` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `OperationFingerprint` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `StartedUtc` | `DateTime` | Source does not specify XML meaning/default here; complete before port. |
| `CompletedUtc` | `DateTime?` | Source does not specify XML meaning/default here; complete before port. |
| `ElapsedSeconds` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `AdapterAssemblyVersion` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `RuntimeVersion` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `ForceConventionSchema` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `SourceForceUnit` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `SourceLengthUnit` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `MappingMode` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `OptionsCanonicalText` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `PathDisclosure` | `IdeaExportPathDisclosure` | Source does not specify XML meaning/default here; complete before port. |
| `StaadModel` | `IdeaExportFileFingerprint` | Source does not specify XML meaning/default here; complete before port. |
| `IdeaMappingSource` | `IdeaExportFileFingerprint` | Source does not specify XML meaning/default here; complete before port. |
| `JointIds` | `IReadOnlyList<int>` | Source does not specify XML meaning/default here; complete before port. |
| `LoadCaseIds` | `IReadOnlyList<int>` | Source does not specify XML meaning/default here; complete before port. |
| `RequestedJointIds` | `IReadOnlyList<int>` | Source does not specify XML meaning/default here; complete before port. |
| `RequestedLoadCaseIds` | `IReadOnlyList<int>` | Source does not specify XML meaning/default here; complete before port. |
| `AnalysisResultsAvailable` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `LoadCases` | `IReadOnlyList<IdeaExportLoadCaseEvidence>` | Source does not specify XML meaning/default here; complete before port. |
| `Artifacts` | `IReadOnlyList<IdeaExportArtifactFingerprint>` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 32. IdeaBatchJointFailure

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaBatchExport.cs#L173)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `JointId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `Stage` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `ExceptionType` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `Message` | `string` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 33. IdeaStatiCaJointExportPackage

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaBatchExport.cs#L181)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `JointId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `EquilibriumResults` | `IReadOnlyList<IdeaStatiCaJointEquilibriumResult>` | Source does not specify XML meaning/default here; complete before port. |
| `ForceRows` | `IReadOnlyList<IdeaStatiCaMemberForcesResult>` | Source does not specify XML meaning/default here; complete before port. |
| `MappedLoadPackage` | `IdeaConnectionJointLoadPackage` | Source does not specify XML meaning/default here; complete before port. |
| `Topology` | `IdeaStatiCaJointTopology` | Source does not specify XML meaning/default here; complete before port. |
| `Diagram` | `IdeaStatiCaJointTopologyDiagram` | Source does not specify XML meaning/default here; complete before port. |
| `IndividualWorkbookPath` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `IndividualAuditPath` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `IsMapped` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `IsReadyForExport` | `bool` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 34. IdeaStatiCaBatchArtifactManifest

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaBatchExport.cs#L205)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `WorkbookPath` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `ConsolidatedAuditPath` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `ManifestPath` | `string` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 35. IdeaStatiCaBatchExportResult

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaBatchExport.cs#L212)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `Provenance` | `IdeaExportProvenance` | Source does not specify XML meaning/default here; complete before port. |
| `Joints` | `IReadOnlyList<IdeaStatiCaJointExportPackage>` | Source does not specify XML meaning/default here; complete before port. |
| `Failures` | `IReadOnlyList<IdeaBatchJointFailure>` | Source does not specify XML meaning/default here; complete before port. |
| `Artifacts` | `IdeaStatiCaBatchArtifactManifest` | Source does not specify XML meaning/default here; complete before port. |
| `IsComplete` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `IsReadyForIdea` | `bool` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 36. IdeaStatiCaExcelForceSystem

**Kind:** EnumDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaForces.cs#L14)

Identifies which IDEA StatiCa force convention is written to an export.

| Enum member | Explicit source value | Source meaning |
| --- | --- | --- |
| `StructuralInternalForces` | 0 | IDEA structural-model internal forces. |
| `ConnectionLoadEffects` | 1 | IDEA Connection load effects. This is the convention required by the Connection application's XLS Import table. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 37. IdeaStatiCaJointForceContributionKind

**Kind:** EnumDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaForces.cs#L27)

Describes the physical source represented by an exported row.

| Enum member | Explicit source value | Source meaning |
| --- | --- | --- |
| `MemberEnd` | 0 | No source XML meaning. |
| `MemberEndWithSharedEdgePlates` | 1 | No source XML meaning. |
| `PlateTransferredToProximityMember` | 2 | No source XML meaning. |
| `PlateOnlyGlobal` | 3 | No source XML meaning. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 38. IdeaStatiCaForceRotation

**Kind:** StructDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaForces.cs#L39)

Proper three-dimensional rotation used to map STAAD member-local vector components into the IDEA StatiCa member-local coordinate system.

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `M11` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `M12` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `M13` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `M21` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `M22` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `M23` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `M31` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `M32` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `M33` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `Default` | `IdeaStatiCaForceRotation` | Default mapping established from the IDEA Checkbot OpenModel results: IDEA x = STAAD x, IDEA y = STAAD z, IDEA z = -STAAD y. This is a proper +90-degree rotation about local x and preserves a right-handed basis. It reproduces IDEA's imported N/Qy/Qz/Mx/My/Mz values for both member incidences in the reference STAAD model. |
| `DefaultMomentConvention` | `IdeaStatiCaForceRotation` | IDEA structural-result bending-component convention verified against Checkbot: IDEA Mx = STAAD Mx, IDEA My = STAAD Mz, IDEA Mz = STAAD My. Unlike a physical vector rotation this is an orthogonal convention map with determinant -1 because IDEA's bending-result signs are defined by fibre tension, not solely by coordinate-vector rotation. |

| Source method signature | Reference |
| --- | --- |
| `Vector3D Transform(Vector3D vector)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaForces.cs#L87) |
| `Vector3D InverseTransform(Vector3D vector)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaForces.cs#L96) |
| `void Validate(double tolerance = 1e-8)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaForces.cs#L105) |
| `void ValidateComponentConvention(double tolerance = 1e-8)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaForces.cs#L111) |
| `bool Equals(IdeaStatiCaForceRotation other)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaForces.cs#L142) |
| `bool Equals(object obj)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaForces.cs#L150) |
| `int GetHashCode()` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaForces.cs#L152) |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 39. IdeaStatiCaInternalForces

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaForces.cs#L182)

Six force resultants expressed in an IDEA StatiCa member-local system. Forces are normally in kN and moments in kNm.

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `N` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `Vy` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `Vz` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `Mx` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `My` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `Mz` | `double` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 40. IdeaStatiCaPlateForceContribution

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaForces.cs#L205)

One plate-corner action allocated to one connected member.

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `PlateId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `AssignedMemberId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `AllocationFactor` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `RawGlobalActionsAtJoint` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `AllocatedGlobalActionsAtJoint` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `AllocatedMemberLocalActionsAtJoint` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `SourceCoordinateSystem` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `TargetCoordinateSystem` | `string` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 41. IdeaStatiCaMemberForcesResult

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaForces.cs#L220)

Complete and auditable result for one STAAD member end and one load case.

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `JointId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `LoadCaseId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `LoadCaseName` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `MemberId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `IdeaMemberName` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `MemberEnd` | `MemberEnd` | Source does not specify XML meaning/default here; complete before port. |
| `ContributionKind` | `IdeaStatiCaJointForceContributionKind` | Source does not specify XML meaning/default here; complete before port. |
| `AllocationReason` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `PositionOverride` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `HasPhysicalMember` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `StaadMemberEndActions` | `Forces` | Raw OpenSTAAD member-end actions in STAAD member-local axes. |
| `StaadMemberEndActionsGlobal` | `Forces` | Raw OpenSTAAD member-end actions in STAAD global axes at the offset member end. |
| `StaadMemberEndActionsGlobalFromLocal` | `Forces` | Member-local result independently reconstructed in global axes. |
| `MemberLocalToGlobalResidual` | `Forces` | Reconstructed-global minus direct-global; validates the member basis. |
| `StaadMemberEndActionsAtJointGlobal` | `Forces` | Member actions transferred from the offset member end to the joint, in global axes. |
| `PlateCornerActionsAtJointGlobal` | `Forces` | Allocated plate-corner actions at the joint, in global axes. |
| `CombinedStaadMemberEndActions` | `Forces` | Combined member, plate and closure actions transferred back to the offset member reference end and expressed in member-local axes. These are the actions converted for IDEA export. |
| `CombinedStaadMemberEndActionsAtMemberEndGlobal` | `Forces` | Combined export actions at the offset member reference end, in global axes. |
| `CombinedStaadMemberEndActionsGlobal` | `Forces` | Combined member and allocated plate actions at the joint, in global axes. |
| `RawCombinedStaadMemberEndActionsGlobal` | `Forces` | Combined action before any explicit numerical equilibrium closure. |
| `EquilibriumClosureActionsAtJointGlobal` | `Forces` | Explicit numerical closure added at the joint. It is zero unless the caller nominated this member as the equilibrium-closure member. |
| `MemberLocalXInGlobal` | `Vector3D` | Source does not specify XML meaning/default here; complete before port. |
| `MemberLocalYInGlobal` | `Vector3D` | Source does not specify XML meaning/default here; complete before port. |
| `MemberLocalZInGlobal` | `Vector3D` | Source does not specify XML meaning/default here; complete before port. |
| `MemberBetaAngle` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `GlobalMemberEndOffset` | `Vector3D` | Source does not specify XML meaning/default here; complete before port. |
| `SourceForceUnit` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `SourceLengthUnit` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `GlobalCoordinateSystem` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `LocalCoordinateSystem` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `InternalForces` | `IdeaStatiCaInternalForces` | Source does not specify XML meaning/default here; complete before port. |
| `ConnectionLoadEffects` | `IdeaStatiCaInternalForces` | Source does not specify XML meaning/default here; complete before port. |
| `IdeaExportRoundTripResidualMemberLocal` | `Forces` | IDEA export transformed back to STAAD member-local minus the source export action. |
| `ConnectedPlateIds` | `IReadOnlyList<int>` | Source does not specify XML meaning/default here; complete before port. |
| `PlateContributions` | `IReadOnlyList<IdeaStatiCaPlateForceContribution>` | Source does not specify XML meaning/default here; complete before port. |
| `IdeaLoadPosition` | `IdeaConnectionEnd` | IDEA Connection load-table position at the remote boundary of the cut member. It is opposite the STAAD incidence end located at the joint. |
| `Position` | `string` | Source does not specify XML meaning/default here; complete before port. |

| Source method signature | Reference |
| --- | --- |
| `IdeaStatiCaInternalForces GetForces(IdeaStatiCaExcelForceSystem forceSystem)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaForces.cs#L310) |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 42. IdeaStatiCaJointEquilibriumResult

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaForces.cs#L319)

Global force and moment equilibrium for one STAAD joint and load case.

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `JointId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `LoadCaseId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `LoadCaseName` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `CoordinateSystem` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `SourceForceUnit` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `SourceLengthUnit` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `MemberEndActionsAtJointGlobalSum` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `UniquePlateCornerActionsGlobalSum` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `AllocatedPlateCornerActionsGlobalSum` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `PlateAllocationResidualGlobal` | `Forces` | Allocated minus uniquely counted, signed plate actions. |
| `AppliedNodalActionsGlobal` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `SupportActionsGlobal` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `IsSupportActionIncluded` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `SupportActionSource` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `EquilibriumClosurePolicy` | `IdeaEquilibriumClosurePolicyKind` | Source does not specify XML meaning/default here; complete before port. |
| `EquilibriumClosureDiagnostic` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `RawCombinedActionsGlobalSum` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `RawResidualGlobal` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `EquilibriumClosureActionsGlobalSum` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `CombinedActionsGlobalSum` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `ResidualGlobal` | `Forces` | Source does not specify XML meaning/default here; complete before port. |
| `RawForceActionScale` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `RawMomentActionScale` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `ForceActionScale` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `MomentActionScale` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `AbsoluteForceTolerance` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `AbsoluteMomentTolerance` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `RelativeForceTolerance` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `RelativeMomentTolerance` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `MaximumEquilibriumClosureForceRelative` | `double` | Maximum closure-force/action-scale ratio recommended before engineering review. This is diagnostic and does not invalidate the final equilibrium postcondition. |
| `MaximumEquilibriumClosureMomentRelative` | `double` | Maximum closure-moment/action-scale ratio recommended before engineering review. This is diagnostic and does not invalidate the final equilibrium postcondition. |
| `ForceTolerance` | `double` | Effective tolerance: max(absolute tolerance, relative tolerance × action scale). |
| `MomentTolerance` | `double` | Effective tolerance: max(absolute tolerance, relative tolerance × action scale). |
| `MemberResults` | `IReadOnlyList<IdeaStatiCaMemberForcesResult>` | Source does not specify XML meaning/default here; complete before port. |
| `ForceResidualMagnitude` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `MomentResidualMagnitude` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `RawForceResidualMagnitude` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `RawMomentResidualMagnitude` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `EquilibriumClosureForceMagnitude` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `EquilibriumClosureMomentMagnitude` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `EquilibriumClosureForceRelative` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `EquilibriumClosureMomentRelative` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `PlateAllocationForceResidualMagnitude` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `PlateAllocationMomentResidualMagnitude` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `MemberBasisForceResidualMaximum` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `MemberBasisMomentResidualMaximum` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `IdeaExportRoundTripForceResidualMaximum` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `IdeaExportRoundTripMomentResidualMaximum` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `IsForceEquilibriumSatisfied` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `IsMomentEquilibriumSatisfied` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `IsPlateAllocationConserved` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `IsMemberBasisTransformationSatisfied` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `IsIdeaExportRoundTripSatisfied` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `IsEquilibriumClosureWithinConfiguredLimits` | `bool` | Source does not specify XML meaning/default here; complete before port. |
| `IsEquilibriumSatisfied` | `bool` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 43. IdeaStatiCaTopologyMember

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaJointTopology.cs#L8)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `MemberId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `StartNodeId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `EndNodeId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `TargetEnd` | `MemberEnd` | Source does not specify XML meaning/default here; complete before port. |
| `StartGlobal` | `IdeaConnectionCoordinates` | Source does not specify XML meaning/default here; complete before port. |
| `EndGlobal` | `IdeaConnectionCoordinates` | Source does not specify XML meaning/default here; complete before port. |
| `LocalXInGlobal` | `IdeaConnectionCoordinates` | Source does not specify XML meaning/default here; complete before port. |
| `LocalYInGlobal` | `IdeaConnectionCoordinates` | Source does not specify XML meaning/default here; complete before port. |
| `LocalZInGlobal` | `IdeaConnectionCoordinates` | Source does not specify XML meaning/default here; complete before port. |
| `BetaAngleDegrees` | `double` | Source does not specify XML meaning/default here; complete before port. |
| `ConnectedPlateIds` | `IReadOnlyList<int>` | Source does not specify XML meaning/default here; complete before port. |
| `IdeaMemberName` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `IdeaLoadPosition` | `IdeaConnectionEnd?` | Source does not specify XML meaning/default here; complete before port. |
| `ActiveComponents` | `IdeaConnectionLoadComponents?` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 44. IdeaStatiCaTopologyPlate

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaJointTopology.cs#L31)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `PlateId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `NodeIds` | `IReadOnlyList<int>` | Source does not specify XML meaning/default here; complete before port. |
| `NodesGlobal` | `IReadOnlyList<IdeaConnectionCoordinates>` | Source does not specify XML meaning/default here; complete before port. |
| `OwnerMemberId` | `int?` | Source does not specify XML meaning/default here; complete before port. |
| `OwnershipReason` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `IsSharedEdgeOwner` | `bool` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 45. IdeaStatiCaJointTopology

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaJointTopology.cs#L48)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `JointId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `JointGlobal` | `IdeaConnectionCoordinates` | Source does not specify XML meaning/default here; complete before port. |
| `SourceLengthUnit` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `Members` | `IReadOnlyList<IdeaStatiCaTopologyMember>` | Source does not specify XML meaning/default here; complete before port. |
| `Plates` | `IReadOnlyList<IdeaStatiCaTopologyPlate>` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 46. IdeaStatiCaJointTopologyDiagram

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaJointTopology.cs#L65)

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `JointId` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `AccessibleTitle` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `AccessibleDescription` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `SvgMarkup` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `MemberCount` | `int` | Source does not specify XML meaning/default here; complete before port. |
| `PlateCount` | `int` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 47. OpenStaadIdeaStatiCaOptions

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OpenStaadIdeaStatiCaOptions.cs#L8)

Configuration for the OpenSTAAD to IDEA StatiCa adapter.

| Property | Source type | Initializer / contract |
| --- | --- | --- |
| `StaadToIdeaRotation` | `IdeaStatiCaForceRotation` | STAAD member-local to IDEA member-local axis rotation. |
| `StaadToIdeaMomentConvention` | `IdeaStatiCaForceRotation` | STAAD member-local moments to IDEA structural-result moment convention. |
| `PlateCornerContributionFactor` | `double` | Multiplier applied to transformed plate-corner actions before they are added to the member-end action. The OpenSTAAD action convention uses +1. Set -1 only for a result source known to return the opposite action. |
| `PlateMemberAssignments` | `IDictionary<int, int>` | Optional explicit owner of a plate-corner action, keyed by plate ID with the incident member ID as value. Shared edges are preferred automatically. A plate without a shared edge is assigned to the closest incident member direction. Every corner is owned exactly once and is never split or duplicated. |
| `IdeaMemberNames` | `IDictionary<int, string>` | Optional STAAD-member to IDEA-member name mapping. This is required when IDEA combines consecutive STAAD analytical members into one continuous member; the two STAAD IDs then use the same IDEA name at opposite positions. |
| `EquilibriumClosureMemberId` | `int?` | Optional incident member which receives the explicit equilibrium-closure action. When null, a physical member is selected automatically; a plate-only joint uses its first global plate row. The correction is separately audited and is never represented as an additional plate contribution. |
| `EquilibriumClosurePolicy` | `IdeaEquilibriumClosurePolicyKind` | Controls whether and how the audited raw joint residual is allocated. |
| `MemberClosureStiffnessWeights` | `IDictionary<int, double>` | Explicit positive weights used only by IdeaEquilibriumClosurePolicyKind.StiffnessWeighted. |
| `IncludeSupportReactions` | `bool` | Includes a supported joint's OpenSTAAD support action in the free-body equilibrium. |
| `MaximumEquilibriumClosureForceRelative` | `double` | Closure-force/action-scale ratio above which the audit requests engineering review. It is diagnostic and never blocks the corrected export. |
| `MaximumEquilibriumClosureMomentRelative` | `double` | Closure-moment/action-scale ratio above which the audit requests engineering review. It is diagnostic and never blocks the corrected export. |
| `GlobalForceEquilibriumTolerance` | `double` | Allowable magnitude of the STAAD-global force residual at the joint. |
| `GlobalMomentEquilibriumTolerance` | `double` | Allowable absolute magnitude of the STAAD-global moment residual at the joint. |
| `GlobalForceEquilibriumRelativeTolerance` | `double` | Allowable force residual as a fraction of the sum of incident global action magnitudes. The effective tolerance is the greater of this scaled value and the absolute tolerance. |
| `GlobalMomentEquilibriumRelativeTolerance` | `double` | Allowable moment residual as a fraction of the sum of incident global action magnitudes. The effective tolerance is the greater of this scaled value and the absolute tolerance. |
| `IdeaMappingBasisTolerance` | `double` | Orthonormal/right-handed tolerance for supplied IDEA member bases. |
| `IdeaMappingPointTolerance` | `double` | Allowable distance in metres between mapped and live STAAD joint points. |
| `IdeaDisabledComponentTolerance` | `double` | Maximum ignored component magnitude before mapped export is blocked. |
| `RequireOppositeIdeaLoadPositionToStaadJointEnd` | `bool` | Requires IDEA's load-table position to be opposite the STAAD incidence at the joint, matching a cut member loaded at its remote boundary. |
| `StaadResultForceUnit` | `ForceUnit?` | Optional override for the force unit in which OpenSTAAD returns results. When omitted, the wrapped model's current input force unit is used. |
| `StaadResultLengthUnit` | `LengthUnit?` | Optional override for the length unit used by OpenSTAAD result moments. When omitted, the wrapped model's current input length unit is used. |
| `ExcelForceSystem` | `IdeaStatiCaExcelForceSystem` | Values written by the Excel exporter. XLS Import requires ConnectionLoadEffects. |
| `MemberNamePrefix` | `string` | Source does not specify XML meaning/default here; complete before port. |
| `LoadCaseNamePrefix` | `string` | Source does not specify XML meaning/default here; complete before port. |

**Port rule:** Use an owned managed DTO/enum only where the public feature needs it; adapt unit/vector/entity dependencies coherently and document every field.

### 48. IdeaCheckbotJointMappingReader

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaCheckbotJointMappingReader.cs#L19)

Creates an IDEA Connection joint mapping from a Checkbot project without loading the IDEA SDK. The reader treats Import.xml as read-only and streams the file so large structural models do not have to be materialized as one XML document.

| Source method signature | Reference |
| --- | --- |
| `IdeaConnectionJointMapping Load(string ideaSourcePath, int staadJointId)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaCheckbotJointMappingReader.cs#L29) |
| `string ResolveImportXmlPath(string ideaSourcePath)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaCheckbotJointMappingReader.cs#L57) |

**Port rule:** Reuse the responsibility internally unless a separately approved public utility is justified.

### 49. IdeaConnectionJointMappingSerializer

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaConnectionJointMappingSerializer.cs#L13)

Reads and writes validated IDEA Connection member mappings.

| Source method signature | Reference |
| --- | --- |
| `IdeaConnectionJointMapping Load(string filePath)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaConnectionJointMappingSerializer.cs#L24) |
| `string Save(IdeaConnectionJointMapping mapping, string filePath)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaConnectionJointMappingSerializer.cs#L43) |

**Port rule:** Reuse the responsibility internally unless a separately approved public utility is justified.

### 50. IdeaConnectionMappingProfileService

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaConnectionMappingProfileService.cs#L17)

Creates, validates, compares, and atomically persists reusable mapping profiles.

| Source method signature | Reference |
| --- | --- |
| `IdeaConnectionMappingProfile Create(string name, string staadModelPath, IEnumerable<IdeaConnectionJointMapping> mappings, OpenStaadIdeaStatiCaOptions options)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaConnectionMappingProfileService.cs#L30) |
| `IdeaConnectionMappingProfile Load(string filePath)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaConnectionMappingProfileService.cs#L69) |
| `string SaveAtomic(IdeaConnectionMappingProfile profile, string filePath)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaConnectionMappingProfileService.cs#L78) |
| `IdeaMappingProfileDiff Compare(IdeaConnectionMappingProfile profile, string staadModelPath, IEnumerable<IdeaConnectionJointMapping> currentMappings, double pointTolerance = 1e-6, double basisTolerance = 1e-8)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaConnectionMappingProfileService.cs#L106) |
| `void Validate(IdeaConnectionMappingProfile profile)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaConnectionMappingProfileService.cs#L182) |

**Port rule:** Reuse the responsibility internally unless a separately approved public utility is justified.

### 51. IdeaEquilibriumClosurePolicyService

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaEquilibriumClosurePolicyService.cs#L11)

Deterministically allocates a global joint residual without changing its resultant.

| Source method signature | Reference |
| --- | --- |
| `IdeaEquilibriumClosureAllocation Allocate(IdeaEquilibriumClosurePolicyKind policy, Forces residual, IEnumerable<IdeaEquilibriumClosureCarrier> carriers, int? nominatedMemberId = null)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaEquilibriumClosurePolicyService.cs#L13) |

**Port rule:** Reuse the responsibility internally unless a separately approved public utility is justified.

### 52. IdeaExportArtifactPathService

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaExportArtifactPathService.cs#L12)

Creates deterministic delivery paths and short, same-directory transaction paths for STAAD-to-IDEA export artifacts.

| Source method signature | Reference |
| --- | --- |
| `string CreateSingleJointWorkbookPath(string outputRootDirectory, string modelPath, int jointId)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaExportArtifactPathService.cs#L23) |
| `string CreateBatchWorkbookPath(string outputRootDirectory, string modelPath)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaExportArtifactPathService.cs#L33) |
| `string CreateBatchJointWorkbookPath(string batchWorkbookPath, int jointId)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaExportArtifactPathService.cs#L42) |
| `string CreateCompanionPath(string anchorWorkbookPath, string suffix, string extension)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaExportArtifactPathService.cs#L57) |
| `string CreatePendingPath(string finalPath)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaExportArtifactPathService.cs#L95) |
| `string CreateBackupPath(string finalPath)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaExportArtifactPathService.cs#L103) |
| `void ValidateFinalPath(string path, string artifactDescription)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaExportArtifactPathService.cs#L112) |

**Port rule:** Reuse the responsibility internally unless a separately approved public utility is justified.

### 53. IdeaStatiCaJointVerificationHtmlExporter

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaStatiCaJointVerificationHtmlExporter.cs#L19)

Writes a self-contained, richly styled joint-force verification report and numerical audit manual with full case-by-case tabular breakdowns.

| Source method signature | Reference |
| --- | --- |
| `string Export(IEnumerable<IdeaStatiCaJointEquilibriumResult> sourceResults, string outputFilePath, string staadModelPath, string ideaCheckbotPath)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaStatiCaJointVerificationHtmlExporter.cs#L21) |
| `string Export(IEnumerable<IdeaStatiCaJointEquilibriumResult> sourceResults, string outputFilePath, string staadModelPath, string ideaCheckbotPath, IdeaConnectionJointLoadPackage mappedPackage)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaStatiCaJointVerificationHtmlExporter.cs#L39) |
| `string Export(IEnumerable<IdeaStatiCaJointEquilibriumResult> sourceResults, string outputFilePath, string staadModelPath, string ideaCheckbotPath, IdeaConnectionJointLoadPackage mappedPackage, IdeaExportProvenance provenance, IdeaStatiCaJointTopologyDiagram topologyDiagram)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaStatiCaJointVerificationHtmlExporter.cs#L60) |
| `string F3(double value)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaStatiCaJointVerificationHtmlExporter.cs#L899) |

**Port rule:** Reuse the responsibility internally unless a separately approved public utility is justified.

### 54. StaadToIdeaStatiCaForceTransformer

**Kind:** ClassDeclaration<br>**Reference:** [Source declaration](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/StaadToIdeaStatiCaForceTransformer.cs#L13)

Converts OpenSTAAD member-end actions into IDEA StatiCa structural internal forces and, when required, IDEA Connection load effects.

| Source method signature | Reference |
| --- | --- |
| `IdeaStatiCaInternalForces ToInternalForces(Forces staadMemberLocalEndActions, MemberEnd memberEnd)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/StaadToIdeaStatiCaForceTransformer.cs#L20) |
| `IdeaStatiCaInternalForces ToInternalForces(Forces staadMemberLocalEndActions, MemberEnd memberEnd, IdeaStatiCaForceRotation staadToIdeaRotation)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/StaadToIdeaStatiCaForceTransformer.cs#L31) |
| `IdeaStatiCaInternalForces ToInternalForces(Forces staadMemberLocalEndActions, MemberEnd memberEnd, IdeaStatiCaForceRotation staadToIdeaForceRotation, IdeaStatiCaForceRotation staadToIdeaMomentConvention)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/StaadToIdeaStatiCaForceTransformer.cs#L43) |
| `IdeaStatiCaInternalForces ToConnectionLoadEffects(IdeaStatiCaInternalForces internalForces, MemberEnd memberEnd)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/StaadToIdeaStatiCaForceTransformer.cs#L77) |
| `IdeaStatiCaInternalForces ToInternalForces(Forces ideaMemberLocalEndActions, IdeaConnectionEnd loadPosition)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/StaadToIdeaStatiCaForceTransformer.cs#L110) |
| `IdeaStatiCaInternalForces ToConnectionLoadEffects(IdeaStatiCaInternalForces internalForces, IdeaConnectionEnd loadPosition)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/StaadToIdeaStatiCaForceTransformer.cs#L129) |
| `Forces ToMemberLocalEndActions(IdeaStatiCaInternalForces connectionLoadEffects, IdeaConnectionEnd loadPosition)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/StaadToIdeaStatiCaForceTransformer.cs#L147) |
| `Forces ToStaadMemberLocalEndActions(IdeaStatiCaInternalForces connectionLoadEffects, MemberEnd staadJointEnd, IdeaStatiCaForceRotation staadToIdeaForceRotation, IdeaStatiCaForceRotation staadToIdeaMomentConvention)` | [Source](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/StaadToIdeaStatiCaForceTransformer.cs#L176) |

**Port rule:** Reuse the responsibility internally unless a separately approved public utility is justified.

## 6. Exact upstream method dependencies

| Upstream managed method | First source use | Readiness |
| --- | --- | --- |
| `Geometry.GetAllNodesList` | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L878) | Reference implementation exists; integration/native qualification still required. |
| `Geometry.GetBeam` | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L68) | Reference implementation exists; integration/native qualification still required. |
| `Geometry.GetBeamsConnectedAtNode` | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L882) | Reference implementation exists; integration/native qualification still required. |
| `Geometry.GetGroupNames` | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L857) | Reference implementation exists; integration/native qualification still required. |
| `Geometry.GetIdsOfEntitiesInGroup` | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L862) | Reference implementation exists; integration/native qualification still required. |
| `Geometry.GetNode` | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L2052) | Reference implementation exists; integration/native qualification still required. |
| `Geometry.GetPlatesConnectedAtNode` | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L76) | Reference implementation exists; integration/native qualification still required. |
| `Load.GetAllLoadCasesOfType` | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L2000) | Reference implementation exists; integration/native qualification still required. |
| `Load.GetLoadCombination` | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L1914) | Reference implementation exists; integration/native qualification still required. |
| `Load.GetNodalLoads` | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L1860) | Reference implementation exists; integration/native qualification still required. |
| `Output.AreResultsAvailable` | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L520) | Binding-only placeholder; required operation must be ported first. |
| `Output.GetMemberEndForces` | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L1333) | Binding-only placeholder; required operation must be ported first. |
| `Output.GetMemberLocalCoordinateSystem` | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L1352) | Binding-only placeholder; required operation must be ported first. |
| `Output.GetPlateCornerForcesInGlobalCoordinates` | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L1365) | Binding-only placeholder; required operation must be ported first. |
| `Output.GetSupportForces` | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L1880) | Binding-only placeholder; required operation must be ported first. |
| `Property.PopulateBetaAngles` | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L184) | Binding-only placeholder; required operation must be ported first. |
| `Property.PopulateMemberOffsets` | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L185) | Binding-only placeholder; required operation must be ported first. |
| `Root.GetStaadFileFullPath` | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L729) | Reference implementation exists; integration/native qualification still required. |
| `Support.GetSupportNodeIds` | [Source call](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs#L1890) | Binding-only placeholder; required operation must be ported first. |

This list includes direct method calls only. Property access, entity extensions, private unit conversions and underlying model behavior remain dependencies too. Support is conditional on the requested reaction feature. Root model path retrieval and source file identity must not be treated as proof that a model has no unsaved changes.

## 7. Source files and reproducibility

| Read-only source | Bytes | SHA-256 |
| --- | --- | --- |
| [IOpenStaadIdeaStatiCaAdapter.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Interfaces/IOpenStaadIdeaStatiCaAdapter.cs) | 6407 | 8ffdaa44e8af444031a48245843cb252ae2c7aada16b34a1daaab235b604b360 |
| [OpenStaadIdeaStatiCaAdapter.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Adapters/Models/OpenStaadIdeaStatiCaAdapter.cs) | 111999 | d51f68efcc966ddecbc32240ad8b6a108849ac2a9763ec40789ec8c79e9b6a32 |
| [IdeaConnectionMapping.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMapping.cs) | 16959 | 40a007a34270931556da43d0fbf5f55b3145d8f70a96ecb36819a76abafd29dc |
| [IdeaConnectionMappingProfile.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaConnectionMappingProfile.cs) | 2580 | 227fee8c955f4709155947e2357ced68c9eaf099c68a65bf30a76d6f2b7ee534 |
| [IdeaEquilibriumClosurePolicy.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaEquilibriumClosurePolicy.cs) | 1435 | 6f30b09ddfec2f3aa3cff49ec3993a0113644c3f78fb4fded1a91ebe29b2d734 |
| [IdeaStatiCaBatchExport.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaBatchExport.cs) | 9553 | 3144ccba5909e6572080e3d79f1b4402d7261987a3606aa71623e10d675310f3 |
| [IdeaStatiCaForces.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaForces.cs) | 21301 | 1e94661942426a3f2f14132b41a7801ebfbe7c4c459415d2958134a84041caaa |
| [IdeaStatiCaJointTopology.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/IdeaStatiCaJointTopology.cs) | 2981 | 6d0e208275717711a005e8dd3898ce9eab961df2e9bbb1bdd075084f6ec854cc |
| [OpenStaadIdeaStatiCaOptions.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OpenStaadIdeaStatiCaOptions.cs) | 7458 | 9aa544c90ddf12aa8d025d192ac3f0d1a865978acd84dd58b2b7f1804f2cb814 |
| [IdeaCheckbotJointMappingReader.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaCheckbotJointMappingReader.cs) | 23235 | 7d088ddf96219cbb08ce8e7df26481a7b22bcd61bbe924952ec0a789eded4822 |
| [IdeaConnectionJointMappingSerializer.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaConnectionJointMappingSerializer.cs) | 2460 | 2a96d0812ad5e44869f8b7d5cb9f5d85f3cfacef7ed4bfbe242fefed072da807 |
| [IdeaConnectionMappingProfileService.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaConnectionMappingProfileService.cs) | 13462 | 875b08536996828925c11ac71f1f4159298902efa0a330ec627fcd96e9fc1707 |
| [IdeaEquilibriumClosurePolicyService.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaEquilibriumClosurePolicyService.cs) | 6842 | ee7ecf3550fb4c00512bc4c80fb25edf57387a9a08446a2054b3bbebb0acac44 |
| [IdeaExportArtifactPathService.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaExportArtifactPathService.cs) | 9554 | 34cb4fac9d3ce9b5b816cf27596804526d21d2ea570fceda80e075d746d1dc2c |
| [IdeaExportProvenanceService.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaExportProvenanceService.cs) | 21546 | 4aaec679db49fc3ccc22877c50f49b27c2211af21ba4f21c6c06dcd72530887b |
| [IdeaStatiCaBatchExcelExporter.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaStatiCaBatchExcelExporter.cs) | 27472 | 1080c9751d9269c1c9f5395185a6d8ff3dfcf933d8123804d158946089f3896a |
| [IdeaStatiCaBatchVerificationHtmlExporter.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaStatiCaBatchVerificationHtmlExporter.cs) | 16414 | c881e355741a5ae366c33e576bcaae9b934f66a9d18be827c5ae9f690a0fc51b |
| [IdeaStatiCaForcesExcelExporter.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaStatiCaForcesExcelExporter.cs) | 30681 | c86bd847963ee4808056023179c7f291ecfeb3742fd38282311a6f84cb0188b3 |
| [IdeaStatiCaJointTopologyDiagramRenderer.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaStatiCaJointTopologyDiagramRenderer.cs) | 32157 | f10948b24496ac3fc724d34f375ab8d4eae8390a95b2eb8d0db720bc32387887 |
| [IdeaStatiCaJointVerificationHtmlExporter.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/IdeaStatiCaJointVerificationHtmlExporter.cs) | 63358 | 366411a882ba2771d162d55151f83367fb75e82ecb1bfeb24eacc916c13bee2e |
| [StaadToIdeaStatiCaForceTransformer.cs](file:///D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Services/StaadToIdeaStatiCaForceTransformer.cs) | 10285 | 96a97069d860368ce39342ecd0d74c47a0d742bf6853671339ffda36bd8440a2 |

The [JSON evidence](data/OpenSTAAD-IDEA-StatiCa-Source-Inventory.json) retains method declarations/call records, model properties/XML and fingerprints for audit. This is source evidence, not a native runtime test result. No proprietary runtime binary is added to the package.
