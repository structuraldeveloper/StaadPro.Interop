# Load expansion: case management, assignments and readback

Implemented all **36 approved Load expansion methods** through the existing IOSLoad and single OSLoadAdapter. Source exposes **171 overloads**: 115 main-reference signatures, five earlier conveniences, 15 inspection methods and these 36 additions. Actual dispatch covers **126/129 documented native names**, plus the metadata-corroborated MergeLoadsOnBeam body. The three intentionally unused names are CreateNewPrimaryLoad (richer creation alternatives), IsCombinationCase (derived inventory capability), and AddMemberFloorLoad (the extended floor call supplies the managed route). Native dispatch coverage is not live compatibility or engineering certification.

GetLoadAttribute is a documented status-only observation and does not decode ASD/LSD settings. MergeBeamLoads returns InvocationCompleted with IsSuccessful false because Bentley does not document the merge-body response meanings. Reference-command index base, nodal index convention, plate-command UI selection, floor distribution and live 2024–2026 marshaling remain explicit qualification requirements. See [Load expansion usage and method comparisons](Load-Expansion.md).

## Result and state rules

Mutations return LoadOperationResult or its typed variant, with operation-specific Status, ordered Steps, actual native return evidence and MayHaveChangedModel. A native failure can follow partial mutation; no save, retry or rollback is implied. Transport exceptions retain their original type/stack. Generation returns named actual start/count fields; successful Data is guarded. Queries return LoadQueryResult<T>; malformed or unwritten native outputs raise InvalidOperationException rather than publishing plausible data. A negative count or failure never exposes a partial payload.

The new calls share one internal adapter lock with inspection methods. They execute on the caller’s COM apartment and do not move COM calls to worker tasks. Legacy calls, raw objects, other wrappers and external STAAD edits can still change state. Target-case methods snapshot the supplied ID/family, activate primary or reference appropriately, verify the numeric active case and leave it selected. Same-number external family switches and same-count edits cannot be detected reliably; these checks do not establish an atomic model snapshot.

Collections are enumerated once before mutation, preserve order/duplicates and reject null/empty selections. Only LoadCaseSelection.AllCases deliberately invokes native automatic-generation all-case behavior. Returned collections are materialized, owned, read-only snapshots, bounded by maximumResults; no lazy COM enumeration escapes. Values remain in native model units.

## Choosing an operation

### SplitLoadsOnBeam

~~~csharp
LoadOperationResult SplitLoadsOnBeam(int originalBeamId, int newBeamId);
~~~

Use after splitting beam geometry when the existing load definitions must be transferred to the two beam IDs. Unlike AddMemberLoad, this edits existing load definitions and does not create beam geometry or assign a new load template. Unlike MergeBeamLoads, it splits rather than combines definitions. Native scope across cases is controlled by STAAD; no managed case filter is implied. The IDs follow the native declaration and VBA example; its parameter-table description is erroneous. Native TRUE/1 means success.

### AddCasesToEnvelope

~~~csharp
LoadOperationResult AddCasesToEnvelope(int envelopeId, IEnumerable<int> primaryLoadCaseIds);
~~~

Use to change membership of an existing envelope. CreateLoadEnvelopExt creates an envelope; CreateLoadCombinations creates combination cases. This method does neither and never changes load factors. Pass primary case IDs, not an envelope index or LOAD LIST record index. Native 1 means success.

### RemoveCasesFromEnvelope

~~~csharp
LoadOperationResult RemoveCasesFromEnvelope(int envelopeId, IEnumerable<int> primaryLoadCaseIds);
~~~

Use to remove selected memberships while retaining the envelope and the load-case definitions. DeleteEnvelope removes the envelope itself; DeleteLoadCases removes case definitions. An empty list is rejected and never means remove all. Native 1 means success.

### DeleteEnvelope

~~~csharp
LoadOperationResult DeleteEnvelope(int envelopeId);
~~~

Use to delete an envelope definition. The cases previously listed in it remain case definitions; use DeleteLoadCases only when those cases should also be deleted. This method does not delete a LOAD LIST. Native 1 means success.

### CreateAutomaticCombinations

~~~csharp
LoadOperationResult<AutomaticLoadGenerationData> CreateAutomaticCombinations(AutomaticCombinationRequest request);
~~~

Use installed code/category rule files to generate combination cases. CreateLoadCombinations instead accepts explicit caller-defined factors; CreateAutomaticRepeatLoads generates repeat cases, not result-combination definitions. Code and category strings must match the acquired installation, not a hard-coded engineering-code enum. AllCases is an explicit opt-in to the native empty-array all-case behavior. STAAD may advance the requested start ID; read ActualStartCaseId. GeneratedCount is null because this native API supplies no count, and IDs are never inferred as contiguous. Native 0 means success; the 2024 help lists -1 failure, while 2025/2026 list -8002/-8040/-8041. Error descriptions use wrapper.StaadVersion, never the host ApplicationVersion.

### CreateAutomaticRepeatLoads

~~~csharp
LoadOperationResult<AutomaticLoadGenerationData> CreateAutomaticRepeatLoads(AutomaticRepeatLoadRequest request);
~~~

Use code/category rules to generate repeat load cases whose loads participate in a subsequent analysis. CreateAutomaticCombinations creates combination definitions instead; AddRepeatLoadsTo assigns explicit factors to one existing case. Optional reference and notional participation is explicit; null NotionalOptions disables all notional flags. The returned ActualStartCaseId and GeneratedCount come from native outputs, not a guessed contiguous ID range. Native 0 means success; -1 means failure.

### SetLoadType

~~~csharp
LoadOperationResult SetLoadType(IEnumerable<int> loadCaseIds, LoadType loadType);
~~~

Use to reclassify existing cases as Dead, Live, Wind and similar engineering categories. This LoadType is different from LoadCaseType (primary/reference/combination family), LoadCommandKind (command table) and LoadItemKind (original item code). This changes native cases directly; setting a detached LoadCase.Type alone does not invoke this operation. Native 0 means success.

### DeleteLoadList

~~~csharp
LoadOperationResult DeleteLoadList(int loadListIndex);
~~~

Use to remove one LOAD LIST record created through CreateLoadList. The argument is a one-based list record index from GetLoadListCount, not a case number or envelope ID. Cases and envelopes remain defined. Use DeleteLoadCases or DeleteEnvelope for those scopes. Native 0 means success.

### SetAllowableStressAttribute

~~~csharp
LoadOperationResult SetAllowableStressAttribute(int loadCaseId, LoadStrengthType strengthType, bool increaseAllowableStress);
~~~

Use for an ASD case attribute with an explicit strength-of-forces choice and optional one-third allowable-stress increase. SetLimitStateAttribute instead sets LSD; SetLoadType changes engineering classification rather than design attributes. This does not select a model design code. GetLoadAttribute cannot reconstruct these settings: Bentley documents only a status observation for that getter. Native 0 means success.

### SetLimitStateAttribute

~~~csharp
LoadOperationResult SetLimitStateAttribute(int loadCaseId);
~~~

Use to set the LSD case attribute. SetAllowableStressAttribute instead exposes ASD strength and allowable-stress choices; SetLoadType changes engineering classification. This does not select a design code or run a design. GetLoadAttribute cannot reconstruct LSD/ASD settings from its documented status-only return. Native 0 means success.

### RemoveLoadAttribute

~~~csharp
LoadOperationResult RemoveLoadAttribute(int loadCaseId);
~~~

Use to remove a case design attribute while retaining its loads and definition. ClearPrimaryLoadCase clears items, and DeleteLoadCases removes case definitions; neither is a substitute for this attribute operation. Native 0 means success.

### GetLoadAttribute

~~~csharp
LoadQueryResult<LoadAttributeObservation> GetLoadAttribute(int loadCaseId);
~~~

Use only to observe the documented native GetAttribute status. A successful Data.NativeValue is 0; it does not identify ASD, LSD, LoadStrengthType, stress-increase settings, or the absence of an attribute. Unlike the attribute setters, this API cannot return their settings because Bentley exposes no documented output payload. -1 produces NativeFailure; any other integral value is rejected as an undocumented schema rather than decoded speculatively.

### GetAssignedEntityIds

~~~csharp
LoadQueryResult<IEnumerable<int>> GetAssignedEntityIds(int primaryLoadCaseId, LoadCommandKind commandKind, int commandIndex, int maximumResults = 100000);
~~~

Use to preserve the entity assignment list of one original command, including native order and duplicates. GetAssignedEntityCount returns only its size. GetNodalLoads/GetMemberUniformlyDistributedLoads read loads for an entity, while grouped population methods reorganize loads across entities and can lose original command identity. The zero-based commandIndex is within commandKind, not the loadItemIndex used by GetLoadItemKind. Native zero is an error/unavailable observation, not a proven empty assignment. Sizing, returned size and observed command/entity counts must agree.

### DeleteLoadCases

~~~csharp
LoadOperationResult DeleteLoadCases(LoadCaseType caseType, IEnumerable<int> loadCaseIds);
~~~

Use to remove primary or reference case definitions. ClearPrimaryLoadCases/ClearReferenceLoadCases clear their contents while retaining the cases. Combination and RepeatLoad enum choices are rejected; repeat commands live inside primary cases and require primary-family selection. Primary requests dispatch DeletePrimaryLoadCases with the reference flag false; reference requests use DeleteReferenceLoadCases. Native 1 means success. Dependency failures are reported without deletion/recreation fallback.

### AddSelfWeight

~~~csharp
LoadOperationResult AddSelfWeight(ILoadCase targetCase, SelfWeightDirection direction, double factor);
~~~

Use for native whole-model self-weight covering beams, plates and solids. AddSelfweightInXYZTo instead assigns to an explicit geometry list; these scopes are different even when a caller currently lists every entity. The factor is dimensionless, with its sign preserved, and the global direction uses SelfWeightDirection. Native Boolean TRUE means success.

### GetNodalLoadCommand

~~~csharp
LoadQueryResult<NodalLoadCommandInfo> GetNodalLoadCommand(ILoadCase targetCase, int loadItemIndex);
~~~

Use to inspect the six components of one original ordinary nodal command. GetNodalLoads reads loads assigned to a particular node; GetNodalLoadsGroupedByLoadCase reorganizes entity data. This packet contains no node list: use GetAssignedEntityIds with the appropriate command index for assignments. Only native Nodal items are accepted; inclined loads and support displacement are not interchangeable. Pass the original zero-based GetLoadItemKind index unchanged. The nodal help omits an explicit base; confirm this command-index convention against the acquired build before production use. Native Boolean TRUE means success.

### AddMemberAreaLoad

~~~csharp
LoadOperationResult AddMemberAreaLoad(ILoadCase targetCase, IEnumerable<int> memberIds, double areaLoad);
~~~

Use to assign the native member AREA load parameter to selected beam IDs. AddFloorLoad instead searches enclosed floor panels and distributes floor pressure; AddMemberLoad accepts supported line-load templates. These operations can produce different distributions on the same geometry. The signed area-load value uses current native units. Native 0 means success.

### AddMemberFixedEndLoad

~~~csharp
LoadOperationResult AddMemberFixedEndLoad(ILoadCase targetCase, IEnumerable<int> memberIds, FixedEndLoadDefinition definition);
~~~

Use to supply six force/moment components at each member end as a fixed-end load definition. This is different from assigning concentrated member forces at a distance, support settlements, or reading Output reactions. StartForces and EndForces follow Fx/Fy/Fz/Mx/My/Mz order; signs and current force/moment units are preserved without support-reaction sign reversal. Native 0 means success.

### GetMemberLoadCommand

~~~csharp
LoadQueryResult<MemberLoadCommandInfo> GetMemberLoadCommand(ILoadCase targetCase, int loadItemIndex);
~~~

Use to retain one original member-command packet and its native item kind. GetMemberUniformlyDistributedLoads and the concentrated/linear/trapezoidal readers instead select per-member load definitions. Force parameter dimensions depend on Kind; do not interpret moment slots as forces. Distances retain their native meanings and length units. Only the six ordinary uniform-force, uniform-moment, concentrated-force, concentrated-moment, linear and trapezoidal kinds documented for this reader are accepted. Physical-member and fixed-end packets are not inferred. The item index is zero-based. Native Boolean TRUE means success.

### AddHydrostaticPressure

~~~csharp
LoadOperationResult AddHydrostaticPressure(ILoadCase targetCase, IEnumerable<int> plateIds, HydrostaticPressureDefinition definition);
~~~

Use for pressures interpolated along a global coordinate axis. AddTrapezoidalPressure varies along plate-local X/Y or by joint; AddPlateUniformPressure assigns a uniform value. Pressure direction and interpolation axis are independent: the latter maps GlobalAxis X/Y/Z to native 1/2/3. Signed minimum/maximum pressure parameters are passed unchanged, not sorted or recalculated from fluid density. Native Boolean TRUE/1 means success.

### AddTrapezoidalPressure

~~~csharp
LoadOperationResult AddTrapezoidalPressure(ILoadCase targetCase, IEnumerable<int> plateIds, TrapezoidalPressureDefinition definition);
~~~

Use for two-value variation along plate-local X/Y or four joint-ordered pressures. AddHydrostaticPressure interpolates along a global axis; AddPlateUniformPressure supplies one uniform pressure. AlongLocalAxis supplies zero only for the two native ignored corner slots; AtCorners retains all four joint values. No joint sorting or geometry conversion occurs. Confirm the fourth-value convention separately for triangular plates. Native Boolean TRUE/1 means success.

### GetPlatePressureLoadCount

~~~csharp
LoadQueryResult<int> GetPlatePressureLoadCount(ILoadCase targetCase, int plateId);
~~~

Use to count applied pressure entries for one plate in the selected case. GetLoadItemCount counts original case command items, which need not equal this per-plate count; GetPlateConcentratedLoadCount covers concentrated loads. GetPlatePressureLoads returns the entries with values and local extents. Zero is a valid empty count, -1 is failure.

### GetPlatePressureLoads

~~~csharp
LoadQueryResult<IEnumerable<PlatePressureLoad>> GetPlatePressureLoads(ILoadCase targetCase, int plateId, int maximumResults = 100000);
~~~

Use to read applied pressure entries and local rectangular extents for one plate. GetPlateLoadCommand reads an original full-pressure command packet by item index; GetPlateConcentratedLoads reads force entries at local points. These are load definitions, not Output plate stresses. Native top-left/bottom-right order is retained without min/max sorting. The bounded owned snapshot validates all directions and finite parameters and rechecks the count. Native 0 means success; -1 means failure. Zero count returns a read-only empty snapshot without fetching.

### GetPlateConcentratedLoadCount

~~~csharp
LoadQueryResult<int> GetPlateConcentratedLoadCount(ILoadCase targetCase, int plateId);
~~~

Use to count concentrated applied-load entries for one plate. GetPlatePressureLoadCount counts pressure entries; GetLoadItemCount counts original commands for the case. GetPlateConcentratedLoads returns magnitude and local position. Zero is a valid empty count, -1 is failure.

### GetPlateConcentratedLoads

~~~csharp
LoadQueryResult<IEnumerable<PlateConcentratedLoad>> GetPlateConcentratedLoads(ILoadCase targetCase, int plateId, int maximumResults = 100000);
~~~

Use for concentrated applied loads at local plate positions. GetPlatePressureLoads instead returns pressure entries and rectangular extents; GetPlateLoadCommand returns an original pressure command. Magnitudes remain in current native units and are not divided by area. Native 0 means success, -1 means failure; bounded paired buffers and before/after counts protect the owned snapshot. A zero count returns an empty snapshot without fetching.

### GetPlateLoadCommand

~~~csharp
LoadQueryResult<PlateLoadCommandInfo> GetPlateLoadCommand(ILoadCase targetCase, int loadItemIndex);
~~~

Use only for the original ElementLoadPressure (PlateFullPressure, 3310) item packet. GetPlatePressureLoads reads per-plate pressure assignments; it is the alternative for those values and extents. Concentrated, trapezoidal, hydrostatic and partial-pressure item codes are not treated as full-pressure packets. The item index is zero-based. This native reader alone reports directions 0-8, translated to LoadDirection 1-9; member/plate-list readers already use 1-9. Native 1 means success, 0 failure, -1 Unsupported. Bentley explicitly requires selecting the load case in the STAAD UI: managed activation and numeric checks do not certify UI selection. Ensure that UI prerequisite before calling.

### AddFloorLoad

~~~csharp
LoadOperationResult AddFloorLoad(ILoadCase targetCase, FloorLoadDefinition definition);
~~~

Use to let STAAD find enclosed floor panels and distribute pressure using a coordinate range or an existing FLOOR group. AddMemberAreaLoad applies the separate member AREA command; GetFloorInfluenceAreas observes geometry and assigns nothing. The extended native floor function handles all modes, so the legacy fixed-direction call is not duplicated. Range axis and loading axis are independent. Standard range mode passes the single-space marker shown in Bentley's VBA example; one-way mode passes "0" from its parameter table. Group mode passes the exact group name and zero ignored bounds; it does not create the group. Native 1 means success. Verify generated distributions, particularly one-way floors, against the target model.

### GetFloorBeamCount

~~~csharp
LoadQueryResult<int> GetFloorBeamCount(FloorRegion region);
~~~

Use for the native beam count in a bounded floor region, without assigning loads or selecting a load case. This is not GetAssignedEntityCount, which counts entities attached to an existing command. GetFloorInfluenceAreas returns beam/area pairs for the same region. Coordinates are converted to native Single precision (with range checks); RangeAxis maps X/Y/Z to native 1/2/3. Zero is an empty count; malformed negatives are rejected except -1, returned as NativeFailure.

### GetFloorInfluenceAreas

~~~csharp
LoadQueryResult<IEnumerable<BeamInfluenceArea>> GetFloorInfluenceAreas(FloorRegion region, int maximumResults = 100000);
~~~

Use to observe beam/tributary influence-area pairs calculated by STAAD for a floor region. GetFloorBeamCount provides only sizing; AddFloorLoad actually assigns pressure. This method does not multiply areas by pressure or calculate managed tributary areas. It uses the same converted Single-precision bounds and range axis for sizing and retrieval (correcting the differing axes in Bentley's example). Validates positive beam IDs, finite nonnegative areas, retrieval limit and stable count. Native Boolean TRUE means success; areas use squared native length units.

### AddStrainLoad

~~~csharp
LoadOperationResult AddStrainLoad(ILoadCase targetCase, IEnumerable<int> elementIds, double axialElongation);
~~~

Use to assign the native initial axial elongation parameter to explicit element IDs. Despite the native Strain name, axialElongation is a length in current model units, not dimensionless strain; no division or multiplication by member length occurs. AddAxialThermalGradientTo instead supplies a thermal gradient, and fixed-end loading instead supplies force/moment components. Native 0 means success.

### AddSeismicLoad

~~~csharp
LoadOperationResult AddSeismicLoad(ILoadCase targetCase, GlobalAxis direction, double factor);
~~~

Use to apply the existing seismic definition to a case with a global axis and multiplier. AddSeismicDefinition creates definition settings; AddSeismicDefSelfWeight and joint/member weight methods contribute seismic weights. AddRSLoad/AddResponseSpectrumLoadEx create response-spectrum commands instead of this generated static seismic command. GlobalAxis maps directly to native 0/1/2, unlike self-weight or hydrostatic interpolation. Native 0 means success; -1/-8001 are failures.

### AddWindLoad

~~~csharp
LoadOperationResult AddWindLoad(ILoadCase targetCase, WindLoadDefinition definition);
~~~

Use to apply an existing wind definition to a case over an explicit global region. AddWindDefinition/AddWindIntensity/AddWindExposure prepare that definition; ComputeWallWindPressureProfile computes a profile and does not assign this command. This native contract assumes Y vertical: a Z-up or unavailable geometry binding returns Unsupported before activation/assignment. No automatic coordinate remapping occurs. Uses documented X/Z signed codes 1/3/4/6 rather than the inconsistent example code 2. Native 0 means success. Signed factors are retained, so a negative factor can reverse the selected direction. Bentley’s all-zero-bounds example denotes whole-range walls; an all-zero region must not be assumed to select only a geometric point. Supply appropriate nondegenerate bounds when restricting scope.

### AddNotionalLoad

~~~csharp
LoadOperationResult AddNotionalLoad(ILoadCase targetCase, IEnumerable<NotionalLoadContribution> contributions);
~~~

Use to assign explicitly chosen primary and reference contributions to an existing case. AddRepeatLoadsTo/AddReferenceLoadsTo do not specify notional directions; CreateAutomaticRepeatLoads can generate notional participation through code rules rather than this explicit list. Contributions are partitioned into six aligned native buffers, preserving order and duplicates within each family. Assignment uses positive IDs in both families; GetNotionalContributions uses signed native IDs on readback. Factors retain signs, including zero, and directions use global X/Y/Z 1/2/3. Native 0 means success.

### GetReferenceContributionCount

~~~csharp
LoadQueryResult<int> GetReferenceContributionCount(int primaryLoadCaseId, int referenceItemIndex);
~~~

Use to size the case/factor pairs inside one REFERENCE command of the activated primary case. GetReferenceItemCount counts REFERENCE commands; GetRepeatContributionCount counts primary repeat contributions. referenceItemIndex is passed unchanged to the native API: the installed reference help does not establish its index base and has an erroneous ID description in its count table. Confirm the native index on the acquired build; no speculative rebasing or ordinal-to-ID conversion is performed. -1 is NativeFailure.

### GetReferenceContributions

~~~csharp
LoadQueryResult<IEnumerable<ReferenceLoadContribution>> GetReferenceContributions(int primaryLoadCaseId, int referenceItemIndex, int maximumResults = 100000);
~~~

Use to retain ordered reference-case/factor pairs in one REFERENCE command. GetReferenceItemCount counts commands, and GetRepeatContributions returns primary repeat pairs. Duplicate reference IDs and signed/zero factors are preserved. Unlike the repeat reader, the reference API's scalar is described as an ID, not a returned count; it is retained as NativeReturnCode and is not required to equal the buffer size. Data is validated against the sizing call and rechecked count. The native index is passed unchanged; Bentley does not specify its base. Confirm it on the acquired build. -1 is NativeFailure.

### MergeBeamLoads

~~~csharp
LoadOperationResult MergeBeamLoads(IEnumerable<BeamLoadMergeRequest> requests);
~~~

Use after a geometry merge when source-beam load definitions must be merged into retained beams. SplitLoadsOnBeam performs the opposite load transfer; neither method creates or merges geometry. Requests run in supplied order, with one BeginLoadMerging and an EndLoadMerging attempt in finally, even if begin/body throws. The body signature is corroborated by the Bentley class listing and installed CLR metadata, but its return meanings and affected-case scope are not documented. Consequently normal return is InvocationCompleted, IsSuccessful is false, and actual Boolean/integral responses remain in Steps; inspect model results rather than interpreting this as certified success. The merge is not transactional, may affect loads across cases, and is never retried. Both body and cleanup exceptions are preserved in AggregateException; a single exception retains its original stack. MayHaveChangedModel is true after invocation; no rollback is promised.

## Native validation still required

Offline tests validate managed dispatch, data ownership, arguments, failures and cleanup against stand-ins. They cannot certify installed COM marshaling, index semantics or generated engineering loads. Validate the targeted builds using disposable models and inspect generated commands/readback. In particular, a normal MergeBeamLoads return deliberately remains invocation evidence, and GetLoadAttribute cannot be used to read attribute settings. Existing provider, wrapper properties, main-reference overloads and unrelated adapters are unchanged.
