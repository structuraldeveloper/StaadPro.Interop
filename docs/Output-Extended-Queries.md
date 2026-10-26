# Extended Output queries

**Updated:** 26 October 2026

Added 25 documented Output routes to IOSOutput and the single non-partial OSOutputAdapter, bringing typed native coverage to **40 of 60**. The twenty remaining documented routes and thirteen supplemental unit candidates retain separate contract and qualification work. The wrapper and providers are unchanged.

Import `StaadPro.Interop.Enums.Output` for the new coordinate, response and design enums, and `StaadPro.Interop.Models.Output` for named results. Examples assume an already acquired `wrapper` and `output = wrapper.Output`, running on the established COM apartment.

## Public contracts

Every signature below returns OutputQueryResult<T>: inspect IsSuccessful before Data. A failed result has no qualified payload; reading Data throws InvalidOperationException. Native Boolean evidence and integral codes remain separate in NativeBooleanReturn and NativeReturnCode when recorded. Transport exceptions propagate without retry; a malformed successful response throws instead of publishing default values. Counts are integral data, not flags.

### GetMemberSteelDesignResults

~~~csharp
OutputQueryResult<MemberSteelDesignResults> GetMemberSteelDesignResults(int memberId);
~~~

Native: `GetMemberSteelDesignResults`. Workstream: O5.

Reads the complete single-block steel-design result packet for one analytical member.

Prefer this query when code, decision, governing case/station/clause/section, allowable ratio, FX/MY/MZ and slenderness are needed. GetMemberSteelUtilizationRatio returns only a ratio. PASS/FAIL text is classified explicitly; numeric, Boolean or other status variants are preserved as Unknown rather than guessed. The three unreturned FY/FZ/MX actions are unavailable and are never set to zero. Governing case IDs are not hydrated or activated. Native false takes precedence over any written outputs.

| Parameter | Meaning |
| --- | --- |
| `memberId` | Positive analytical member ID; not a physical-member ID or a collection index. |

~~~csharp
var result = output.GetMemberSteelDesignResults(17);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.DesignStatus);
~~~

### GetBucklingFactorCount

~~~csharp
OutputQueryResult<int> GetBucklingFactorCount();
~~~

Native: `GetNoOfBucklingFactors`. Workstream: O5.

Counts the native eigen-buckling factors already extracted.

This returns an actual integral count, not a success flag or a factor value. Zero is a valid count and does not prove that another result family is unavailable. GetBucklingResultAvailability observes availability; GetBucklingModeDisplacements retrieves one shape. No eigenanalysis is started.

~~~csharp
var result = output.GetBucklingFactorCount();
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
~~~

### GetBucklingModeDisplacements

~~~csharp
OutputQueryResult<ModeDisplacements> GetBucklingModeDisplacements(int nativeBucklingModeNumber, int nodeId);
~~~

Native: `GetBucklingModeDisplacementAtNode`. Workstream: O5.

Reads all six native eigen-buckling mode-shape components at one node.

Use for a buckling mode shape, not static displacement or a scalar buckling factor. The native mode number/index is passed unchanged; no zero/one-based conversion or count-derived remapping occurs. Six initialized Double slots are passed by reference and must be written on native 1/true success. Mode-shape normalization is not converted into physical static length/rotation units. Native numeric -1 is not accepted as this operation’s documented 1/0 flag.

| Parameter | Meaning |
| --- | --- |
| `nativeBucklingModeNumber` | Nonnegative native eigen-buckling mode number/index, passed unchanged and not mapped to a dynamic mode. |
| `nodeId` | Positive native analytical node ID, not an array index. |

~~~csharp
var result = output.GetBucklingModeDisplacements(1, 11);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Translation);
~~~

### GetRelativeMemberDisplacement

~~~csharp
OutputQueryResult<Vector3D> GetRelativeMemberDisplacement(int memberId, double distanceFromStart, int loadCaseId);
~~~

Native: `GetIntermediateMemberTransDisplacements`. Workstream: O6.

Reads the relative native X/Y/Z member translation at a specified station.

Use for the native relative translations described in local X/Y/Z order. GetAbsoluteMemberDisplacement invokes a different native operation whose prose is ambiguous; neither query applies a managed frame or offset transformation. This returns three translations, not six nodal translation/rotation values.

| Parameter | Meaning |
| --- | --- |
| `memberId` | Positive analytical member ID; not a physical-member ID or a collection index. |
| `distanceFromStart` | Finite nonnegative native station measured from the relevant member start. No fraction/length conversion occurs; member existence and upper station validity remain native decisions. |
| `loadCaseId` | Positive native result load-case number. No activation is performed; suitability is determined by STAAD. |

~~~csharp
var result = output.GetRelativeMemberDisplacement(17, 0.5, 1);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
~~~

### GetAbsoluteMemberDisplacement

~~~csharp
OutputQueryResult<Vector3D> GetAbsoluteMemberDisplacement(int memberId, double distanceFromStart, int loadCaseId);
~~~

Native: `GetIntermediateMemberAbsTransDisplacements`. Workstream: O6.

Reads the absolute-labelled native X/Y/Z member translation at a specified station.

Calls the native AbsTrans operation, separately from GetRelativeMemberDisplacement. The installed name says absolute while its prose repeats relative; the result preserves native X/Y/Z slots and does not certify an absolute/relative transformation. No offset transfer, basis reconstruction, rotation, length-fraction conversion or zero substitution is applied.

| Parameter | Meaning |
| --- | --- |
| `memberId` | Positive analytical member ID; not a physical-member ID or a collection index. |
| `distanceFromStart` | Finite nonnegative native station measured from the relevant member start. No fraction/length conversion occurs; member existence and upper station validity remain native decisions. |
| `loadCaseId` | Positive native result load-case number. No activation is performed; suitability is determined by STAAD. |

~~~csharp
var result = output.GetAbsoluteMemberDisplacement(17, 0.5, 1);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
~~~

### GetPhysicalMemberEndForces

~~~csharp
OutputQueryResult<ForceComponents> GetPhysicalMemberEndForces(int physicalMemberId, MemberEnd memberEnd, int loadCaseId, ResultCoordinateSystem coordinateSystem = ResultCoordinateSystem.Local);
~~~

Native: `GetPMemberEndForces`. Workstream: O6.

Reads six physical-member end actions in explicitly selected native local/global axes.

Use for physical-member IDs and native physical start/end incidence. GetMemberEndForces takes analytical member IDs. MemberEnd.Start/End map to native 0/1; ResultCoordinateSystem.Local/Global map independently to 0/1 and do not reuse the oppositely numbered offset enum. Physical incidence reordering may change signs; no managed reordering, rotation or sign correction is applied. FX/FY/FZ retain force units and MX/MY/MZ force-times-length units.

| Parameter | Meaning |
| --- | --- |
| `physicalMemberId` | Positive native physical-member ID. Analytical member IDs are not interchangeable with this identity. |
| `memberEnd` | Existing MemberEnd.Start (native 0) or MemberEnd.End (native 1), interpreted for the physical-member incidence. |
| `loadCaseId` | Positive native result load-case number. No activation is performed; suitability is determined by STAAD. |
| `coordinateSystem` | Local=0 or Global=1 for this native physical-member force query. No managed rotation is applied. |

~~~csharp
var result = output.GetPhysicalMemberEndForces(7, MemberEnd.Start, 1, ResultCoordinateSystem.Local);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Fx);
~~~

### GetMemberAxialForceExtrema

~~~csharp
OutputQueryResult<MemberForceExtrema> GetMemberAxialForceExtrema(int memberId, int loadCaseId);
~~~

Native: `GetMinMaxAxialForce`. Workstream: O6.

Reads signed axial-force minimum/maximum values and both native stations.

Unlike GetMemberForcesAtDistance, this requests native extrema over the member. Values are not absolute extrema, sorted envelopes across load cases or recalculated forces. Minimum must not exceed maximum; both written stations must be nonnegative. Positions retain native length units from the analytical start.

| Parameter | Meaning |
| --- | --- |
| `memberId` | Positive analytical member ID; not a physical-member ID or a collection index. |
| `loadCaseId` | Positive native result load-case number. No activation is performed; suitability is determined by STAAD. |

~~~csharp
var result = output.GetMemberAxialForceExtrema(17, 1);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.MinimumValue);
~~~

### GetMemberForcesAtDistance

~~~csharp
OutputQueryResult<ForceComponents> GetMemberForcesAtDistance(int memberId, double distanceFromStart, int loadCaseId);
~~~

Native: `GetIntermediateMemberForcesAtDistance`. Workstream: O6.

Reads six local section actions at a specified analytical member station.

This returns FX/FY/FZ/MX/MY/MZ at a native distance from the analytical start. GetPhysicalMemberForcesAtDistance uses physical-member identity and total physical length. It is not an end-action sign conversion, result envelope or normalized eigenmode. Station zero is allowed; member membership and upper station validity remain native decisions. No implicit length-fraction conversion, unit scaling or geometry hydration occurs.

| Parameter | Meaning |
| --- | --- |
| `memberId` | Positive analytical member ID; not a physical-member ID or a collection index. |
| `distanceFromStart` | Finite nonnegative native station measured from the relevant member start. No fraction/length conversion occurs; member existence and upper station validity remain native decisions. |
| `loadCaseId` | Positive native result load-case number. No activation is performed; suitability is determined by STAAD. |

~~~csharp
var result = output.GetMemberForcesAtDistance(17, 0.5, 1);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Fx);
~~~

### GetPhysicalMemberForcesAtDistance

~~~csharp
OutputQueryResult<ForceComponents> GetPhysicalMemberForcesAtDistance(int physicalMemberId, double distanceFromStart, int loadCaseId);
~~~

Native: `GetPMemberIntermediateForcesAtDistance`. Workstream: O6.

Reads six local section actions at a specified physical member station.

This returns FX/FY/FZ/MX/MY/MZ at a native distance from the physical start. GetMemberForcesAtDistance uses analytical member identity; IDs are not interchangeable. It is not an end-action sign conversion, result envelope or normalized eigenmode. Station zero is allowed; member membership and upper station validity remain native decisions. No implicit length-fraction conversion, unit scaling or geometry hydration occurs.

| Parameter | Meaning |
| --- | --- |
| `physicalMemberId` | Positive native physical-member ID. Analytical member IDs are not interchangeable with this identity. |
| `distanceFromStart` | Finite nonnegative native station measured from the relevant member start. No fraction/length conversion occurs; member existence and upper station validity remain native decisions. |
| `loadCaseId` | Positive native result load-case number. No activation is performed; suitability is determined by STAAD. |

~~~csharp
var result = output.GetPhysicalMemberForcesAtDistance(17, 0.5, 1);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Fx);
~~~

### GetMemberDeflectionAtDistance

~~~csharp
OutputQueryResult<MemberDeflection> GetMemberDeflectionAtDistance(int memberId, double distanceFromStart, int loadCaseId);
~~~

Native: `GetIntermediateDeflectionAtDistance`. Workstream: O6.

Reads the two native Y/Z deflection outputs at one analytical member station.

Choose this for the two-output native deflection operation; GetRelativeMemberDisplacement and GetAbsoluteMemberDisplacement return three translation slots through different functions. The help does not fully establish the Y/Z frame or absolute/relative convention, so NativeY/NativeZ preserve slot identity without asserting a transformation. No missing X value or rotations are invented.

| Parameter | Meaning |
| --- | --- |
| `memberId` | Positive analytical member ID; not a physical-member ID or a collection index. |
| `distanceFromStart` | Finite nonnegative native station measured from the relevant member start. No fraction/length conversion occurs; member existence and upper station validity remain native decisions. |
| `loadCaseId` | Positive native result load-case number. No activation is performed; suitability is determined by STAAD. |

~~~csharp
var result = output.GetMemberDeflectionAtDistance(17, 0.5, 1);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.NativeY);
~~~

### GetMatInfluenceAreas

~~~csharp
OutputQueryResult<IReadOnlyList<MatInfluenceArea>> GetMatInfluenceAreas(IEnumerable<int> supportNodeIds, int maximumResults = 100000);
~~~

Native: `GetMatInfluenceAreas`. Workstream: O7.

Reads native ELASTIC MAT influence areas corresponding to an explicit support-node selection.

Use for YZ/ZX/XY influence areas, not base pressures or Load floor influence areas. Input is snapshotted once with a bounded selection; order and duplicates are retained. Empty input returns an owned empty result without native result calls. Exported metadata passes initialized arrays by value, so every applicable Double slot must be written in place. Native integer 0 means success; -107/-108 mean buffer failure. Actual Boolean acknowledgements are interpreted separately, never converted to an integer zero. Changed node correspondence rejects mixed data. Areas retain native length-squared units.

| Parameter | Meaning |
| --- | --- |
| `supportNodeIds` | Non-null one-pass selection of positive support-node IDs. Order and duplicates are preserved. Empty input is valid; native ELASTIC MAT/BASE PRESSURE prerequisites remain the caller’s responsibility. |
| `maximumResults` | Maximum support-node selection size, from 1 through 100000. Input beyond this bound is rejected before native access. |

~~~csharp
var result = output.GetMatInfluenceAreas(new[] { 11, 12, 11 });
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
~~~

### GetBasePressures

~~~csharp
OutputQueryResult<IReadOnlyList<NodeBasePressure>> GetBasePressures(int loadCaseId, IEnumerable<int> supportNodeIds, int maximumResults = 100000);
~~~

Native: `GetBasePressures`. Workstream: O7.

Reads signed base pressures for explicitly ordered support nodes and one result case.

Requires native BASE PRESSURE results; it does not calculate contact pressure from support reactions or ELASTIC MAT influence areas. Native YZ/ZX/XY arrays correspond to global X/Y/Z pressure components in force-per-area units. The Boolean-success contract differs from GetMatInfluenceAreas integer-zero success. Bounded one-pass input snapshots preserve duplicates/order, validate written fixed-size arrays and reject changed node correspondence. Empty input returns an owned empty result without native retrieval.

| Parameter | Meaning |
| --- | --- |
| `loadCaseId` | Positive native result load-case number. No activation is performed; suitability is determined by STAAD. |
| `supportNodeIds` | Non-null one-pass selection of positive support-node IDs. Order and duplicates are preserved. Empty input is valid; native ELASTIC MAT/BASE PRESSURE prerequisites remain the caller’s responsibility. |
| `maximumResults` | Maximum support-node selection size, from 1 through 100000. Input beyond this bound is rejected before native access. |

~~~csharp
var result = output.GetBasePressures(1, new[] { 11, 12 });
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
~~~

### GetSolidNormalStresses

~~~csharp
OutputQueryResult<SolidNormalStresses> GetSolidNormalStresses(int solidId, int nativeCornerIndex, int loadCaseId);
~~~

Native: `GetAllSolidNormalStresses`. Workstream: O7.

Reads SXX/SYY/SZZ normal stresses at an explicitly identified native solid corner.

Use for solid elements, not plate-center stresses. The nonnegative native corner selector is passed unchanged; it is not an analytical node ID and no zero/one-based corner mapping or center sentinel is invented. Named component ordering follows the native output slots; no principal transformation is performed. All three native stress values must be written and finite on success. Native result stress units and signs are preserved.

| Parameter | Meaning |
| --- | --- |
| `solidId` | Positive native solid-element ID; not a plate or analytical member ID. |
| `nativeCornerIndex` | Nonnegative native solid-corner selector, passed unchanged. No analytical node-ID mapping, zero/one rebasing or center sentinel is supplied. |
| `loadCaseId` | Positive native result load-case number. No activation is performed; suitability is determined by STAAD. |

~~~csharp
var result = output.GetSolidNormalStresses(101, 1, 1);
if (result.IsSuccessful) System.Console.WriteLine(result.Status);
~~~

### GetSolidShearStresses

~~~csharp
OutputQueryResult<SolidShearStresses> GetSolidShearStresses(int solidId, int nativeCornerIndex, int loadCaseId);
~~~

Native: `GetAllSolidShearStresses`. Workstream: O7.

Reads SXY/SYZ/SZX shear stresses at an explicitly identified native solid corner.

Use for solid elements, not plate-center stresses. The nonnegative native corner selector is passed unchanged; it is not an analytical node ID and no zero/one-based corner mapping or center sentinel is invented. Named component ordering follows the native output slots; no principal transformation is performed. All three native stress values must be written and finite on success. Native result stress units and signs are preserved.

| Parameter | Meaning |
| --- | --- |
| `solidId` | Positive native solid-element ID; not a plate or analytical member ID. |
| `nativeCornerIndex` | Nonnegative native solid-corner selector, passed unchanged. No analytical node-ID mapping, zero/one rebasing or center sentinel is supplied. |
| `loadCaseId` | Positive native result load-case number. No activation is performed; suitability is determined by STAAD. |

~~~csharp
var result = output.GetSolidShearStresses(101, 1, 1);
if (result.IsSuccessful) System.Console.WriteLine(result.Status);
~~~

### GetSolidPrincipalStresses

~~~csharp
OutputQueryResult<SolidPrincipalStresses> GetSolidPrincipalStresses(int solidId, int nativeCornerIndex, int loadCaseId);
~~~

Native: `GetAllSolidPrincipalStresses`. Workstream: O7.

Reads three native principal-stress slots at an explicitly identified native solid corner.

Use for solid elements, not plate-center stresses. The nonnegative native corner selector is passed unchanged; it is not an analytical node ID and no zero/one-based corner mapping or center sentinel is invented. The help repeats S1 labels, so FirstNativeValue/SecondNativeValue/ThirdNativeValue retain native slot order without sorting or claiming descending S1/S2/S3. All three native stress values must be written and finite on success. Native result stress units and signs are preserved.

| Parameter | Meaning |
| --- | --- |
| `solidId` | Positive native solid-element ID; not a plate or analytical member ID. |
| `nativeCornerIndex` | Nonnegative native solid-corner selector, passed unchanged. No analytical node-ID mapping, zero/one rebasing or center sentinel is supplied. |
| `loadCaseId` | Positive native result load-case number. No activation is performed; suitability is determined by STAAD. |

~~~csharp
var result = output.GetSolidPrincipalStresses(101, 1, 1);
if (result.IsSuccessful) System.Console.WriteLine(result.Status);
~~~

### GetNonlinearLoadStep

~~~csharp
OutputQueryResult<double> GetNonlinearLoadStep(int loadCaseId);
~~~

Native: `GetNLLoadStep`. Workstream: O8.

Reads the native nonlinear load-step value for one result load case.

The return is numeric data, not a Boolean flag or a certified step count/index range. Nonnegative finite values are preserved without rounding. Native -1 is NativeFailure, -8002 NotFound and -9911 ResultsUnavailable. No arbitrary nonzero success rule is used. This does not retrieve nonlinear node displacements or activate a load step.

| Parameter | Meaning |
| --- | --- |
| `loadCaseId` | Positive native result load-case number. No activation is performed; suitability is determined by STAAD. |

~~~csharp
var result = output.GetNonlinearLoadStep(1);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
~~~

### GetSteelDesignParameterBlockCount

~~~csharp
OutputQueryResult<int> GetSteelDesignParameterBlockCount();
~~~

Native: `GetSteelDesignParameterBlockCount`. Workstream: O10.

Counts the native steel-design parameter blocks in the applicable result context.

The reviewed help limits multiple-block extraction to AISC 360-16. GetMultipleSteelDesignResultAvailability observes that capability separately. Zero is a valid count; it is not a fabricated successful design result or a Boolean false. No block names, governing blocks, design changes or results freshness are inferred.

~~~csharp
var result = output.GetSteelDesignParameterBlockCount();
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
~~~

### GetExtractedModeCount

~~~csharp
OutputQueryResult<int> GetExtractedModeCount();
~~~

Native: `GetNoOfModesExtracted`. Workstream: O8.

Counts modes already extracted by native dynamic analysis.

This is an integral mode count, not buckling-factor count or a success flag. Zero remains valid data. It does not establish mode numbering, result normalization or whether the modal set is current. No dynamic analysis is started.

~~~csharp
var result = output.GetExtractedModeCount();
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
~~~

### GetModeFrequency

~~~csharp
OutputQueryResult<double> GetModeFrequency(int nativeModeNumber);
~~~

Native: `GetModeFrequency`. Workstream: O8.

Reads the native frequency in Hz for an explicitly identified dynamic mode.

Use for modal frequency, not a buckling eigenvalue or time-history interval. The nonnegative native mode number/index is passed unchanged and is not rebased from a managed index. Zero frequency is retained when native success reports it; negative or unwritten/nonfinite frequency is rejected. No cycles/radians conversion occurs.

| Parameter | Meaning |
| --- | --- |
| `nativeModeNumber` | Nonnegative native dynamic-mode number/index, passed unchanged. The native result context decides validity; no zero/one-based conversion is inferred. |

~~~csharp
var result = output.GetModeFrequency(1);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
~~~

### GetModalNodeDisplacements

~~~csharp
OutputQueryResult<ModeDisplacements> GetModalNodeDisplacements(int nativeModeNumber, int nodeId);
~~~

Native: `GetModalDisplacementAtNode`. Workstream: O8.

Reads six dynamic eigenmode shape components at an analytical node.

Use for a dynamic modal shape; GetBucklingModeDisplacements queries a different mode family and GetNodeDisplacements retrieves load-case responses. The native mode number/index is passed unchanged. Exported metadata takes the six-element initialized array by value, so all applicable slots must be written in place. Native normalization is not rescaled into physical static displacement and no rotations are invented.

| Parameter | Meaning |
| --- | --- |
| `nativeModeNumber` | Nonnegative native dynamic-mode number/index, passed unchanged. The native result context decides validity; no zero/one-based conversion is inferred. |
| `nodeId` | Positive native analytical node ID, not an array index. |

~~~csharp
var result = output.GetModalNodeDisplacements(1, 11);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Translation);
~~~

### GetModalMassParticipationFactors

~~~csharp
OutputQueryResult<ModalMassParticipation> GetModalMassParticipationFactors(int nativeModeNumber);
~~~

Native: `GetModalMassParticipationFactors`. Workstream: O8.

Reads native X/Y/Z mass participation factors for an explicit dynamic mode.

Use for one mode, not missing-mass factors, cumulative participation or a load-case force result. The native mode number/index is passed unchanged. All three scalar slots must be written and finite. Fraction-versus-percentage and eigenvector normalization are not inferred; no factors are rescaled or summed.

| Parameter | Meaning |
| --- | --- |
| `nativeModeNumber` | Nonnegative native dynamic-mode number/index, passed unchanged. The native result context decides validity; no zero/one-based conversion is inferred. |

~~~csharp
var result = output.GetModalMassParticipationFactors(1);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Factors);
~~~

### GetTimeHistoryIntegrationInfo

~~~csharp
OutputQueryResult<TimeHistoryIntegrationInfo> GetTimeHistoryIntegrationInfo();
~~~

Native: `GetTimeHistoryIntegrationStepInfo`. Workstream: O9.

Reads native time-history response-sample count and integration interval.

Unlike a Boolean availability query, this returns a sample count plus an interval in seconds. Native 0 means ResultsUnavailable and -1 NativeFailure; a positive count requires a positive finite interval. The first response is at time zero and the last at (count-1)*interval; no extra endpoint is invented. It does not run integration or certify result freshness.

~~~csharp
var result = output.GetTimeHistoryIntegrationInfo();
if (result.IsSuccessful) System.Console.WriteLine(result.Data.SampleCount);
~~~

### GetTimeHistoryResponseAtTime

~~~csharp
OutputQueryResult<TimeHistorySample> GetTimeHistoryResponseAtTime(int nodeId, ResponseDegreeOfFreedom degreeOfFreedom, TimeHistoryResponseKind responseKind, double timeSeconds);
~~~

Native: `GetTimeHistoryResponseAtTime`. Workstream: O9.

Reads a native time-history response at one bounded time.

GetTimeHistoryResponse retrieves the full integration series. The reserved native load-case argument is always internal zero; no caller case is activated. The native response DOF/kind selects translational or rotational displacement/velocity/acceleration units. Integration metadata is read before/after; requested time must lie in 0..(count-1)*interval. Native 1/true succeeds, 0/false means ResultsUnavailable and integer -1 means NativeFailure, never VARIANT_TRUE. Native interpolation, if any, is not reproduced in managed code. Changed integration metadata returns StateChanged with no sample.

| Parameter | Meaning |
| --- | --- |
| `nodeId` | Positive native analytical node ID, not an array index. |
| `degreeOfFreedom` | A defined translational or rotational response DOF, mapping exactly to native selectors 1 through 6. |
| `responseKind` | Displacement=0, Velocity=1 or Acceleration=2. Units depend on the requested translational/rotational DOF. |
| `timeSeconds` | Finite nonnegative time in seconds within the observed native integration-sample bounds. |

~~~csharp
var result = output.GetTimeHistoryResponseAtTime(11, ResponseDegreeOfFreedom.TranslationX, TimeHistoryResponseKind.Displacement, 0.0);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Value);
~~~

### GetTimeHistoryResponse

~~~csharp
OutputQueryResult<TimeHistorySeries> GetTimeHistoryResponse(int nodeId, ResponseDegreeOfFreedom degreeOfFreedom, TimeHistoryResponseKind responseKind, int maximumSamples = 100000);
~~~

Native: `GetTimeHistoryResponse`. Workstream: O9.

Reads a bounded, owned native time-history response series.

Use for all integration samples rather than one requested time. The reserved native load-case argument stays internal zero. A checked integration count sizes the Double buffer; counts exceeding maximumSamples produce LimitExceeded before allocation or retrieval. Sample zero is at time zero; no extra final sample is added. Native 1/true succeeds, 0/false means ResultsUnavailable and integer -1 NativeFailure. Count/interval are rechecked after retrieval; StateChanged discards mixed data. Samples preserve signs and response-dependent native units; no smoothing, interpolation, differentiation or unit conversion occurs.

| Parameter | Meaning |
| --- | --- |
| `nodeId` | Positive native analytical node ID, not an array index. |
| `degreeOfFreedom` | A defined translational or rotational response DOF, mapping exactly to native selectors 1 through 6. |
| `responseKind` | Displacement=0, Velocity=1 or Acceleration=2. Units depend on the requested translational/rotational DOF. |
| `maximumSamples` | Maximum allocated response samples, from 1 through 100000. A larger native count returns LimitExceeded without allocating or fetching the series. |

~~~csharp
var result = output.GetTimeHistoryResponse(11, ResponseDegreeOfFreedom.TranslationX, TimeHistoryResponseKind.Displacement);
if (result.IsSuccessful) System.Console.WriteLine(result.Data.Samples.Count);
~~~

### GetMultipleSteelDesignResultAvailability

~~~csharp
OutputQueryResult<bool> GetMultipleSteelDesignResultAvailability();
~~~

Native: `IsMultipleMemberSteelDesignResultsAvailable`. Workstream: O10.

Observes native availability for multiple steel-design parameter-block extraction.

This is distinct from general analysis or buckling availability. The reviewed contract identifies AISC 360-16 as the applicable design context. A successful observation with Data=false is valid data, not a query failure. Native Boolean or integral 0/1 is accepted; an integral -1 is not borrowed as success. No design code is changed and no multiple-block result packet is inferred.

~~~csharp
var result = output.GetMultipleSteelDesignResultAvailability();
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
~~~

## Result vocabulary and interpretation

Reused the existing MemberEnd enum, ForceComponents snapshot, Vector3D value type, OutputQueryResult<T> envelope and strict native-value readers. The read-only main project's MemberSteelDesignResults and node-displacement packets supplied reusable field meanings. Its public mutable design object requires private Beam/load-case dependencies and substitutes zeros for unreturned force components; this port instead owns its native observations and keeps only FX/MY/MZ in SteelDesignForces. Numeric and Boolean native design statuses remain Unknown rather than being silently classified as failure. No private source file was modified.

Named immutable classes describe axial extrema/stations, two native deflection components, solid stresses, physical support-node quantities, modal shapes, modal mass factors, steel design and time-history observations. Public methods expose no ref/out, tuple, raw VARIANT packet or mutable native array. Collections retain input correspondence, including duplicates, and own their data. The native mode and corner numbers are deliberately explicit because the reviewed help does not establish a universal managed zero-/one-based conversion.

ResultCoordinateSystem uses native **Local=0 / Global=1** for physical member end forces. This is not the Property offset coordinate enum. ResponseDegreeOfFreedom uses translations X/Y/Z=1..3 and rotations X/Y/Z=4..6. TimeHistoryResponseKind uses displacement/velocity/acceleration=0/1/2. SteelDesignStatus classifies only explicit PASS/FAIL text; other finite native status variants remain preserved observations.

Output values retain native units, signs, axes and result context. BaseUnitSystem and input-unit wrapper caches do not independently qualify every Output quantity. Modal and buckling eigenvectors have native normalization rather than ordinary static-displacement magnitudes. Modal mass factors are not silently scaled to percent. Principal solid values retain their native order because the help repeats a label; no descending S1/S2/S3 interpretation is fabricated. The two-component deflection query does not establish a verified absolute/relative coordinate interpretation. Its names preserve that limitation.

## Native return conventions

| Group | Managed interpretation |
| --- | --- |
| Buckling/mode/design-block counts | Exact nonnegative integral count; zero is valid data. Boolean, fractional and negative counts are rejected. |
| Ordinary Boolean retrieval | Native false produces NativeFailure before buffers are inspected. Actual Boolean and reviewed integral flag forms remain distinct evidence. |
| Buckling-mode retrieval / multiple-design availability | Narrow 0/1 convention, also accepting actual Boolean values; integral -1 is not treated as true. False availability is a successful observation with Data=false. |
| Mat influence areas | Integral **0 is success**; -107/-108 are failure. Actual Boolean false is failure, not integer-zero success. Other codes are rejected. |
| Nonlinear load step | Finite nonnegative step is data; -1 is NativeFailure, -8002 is NotFound, -9911 is ResultsUnavailable. Other negative values are rejected. |
| Time integration | Positive integral count and finite positive interval; zero means ResultsUnavailable, -1 means NativeFailure. |
| Time response | 1/true means success, 0/false means ResultsUnavailable, integral -1 means NativeFailure. |

The steel-design observation reports retrieval success independently from engineering PASS/FAIL. The utilization-only query retains its earlier -999/-1 sentinel conventions; those are not applied to the complete-design operation. No API performs an analysis, activates a load case, saves a file, writes a model or transforms a result.

## Buffer, allocation and state checks

Fixed arrays start with Double NaN sentinels and must contain the exact documented number of finite values. Returned arrays may have nonzero lower bounds, but incorrect rank, size, element type or unwritten slots are rejected. Modal displacement and mat/base-pressure metadata pass arrays by value; these routes verify in-place writes rather than assuming that a success flag filled them. Buckling and other reference-buffer routes accept a correctly returned replacement. Paired support-node outputs are checked against the input ID snapshot before attribution.

Selections are enumerated once into an owned bounded snapshot. maximumResults and maximumSamples must be 1..100000. Oversized support selections are rejected before native invocation. Empty support selections return an owned empty result after lifetime checks without a native call.

Time-history retrieval observes integration count/interval before allocating or fetching, enforces the requested limit, and re-observes both afterward. A changed or unavailable grid produces StateChanged, without data. Samples use exactly the returned count and times i*interval starting at zero; the last time is (count-1)*interval. No extra endpoint is created. An at-time query accepts a finite nonnegative time within that observed interval; interpolation remains native behavior. The reserved native index is always zero.

This before/after check is not an atomic STAAD snapshot: a result or model can change while count/interval remain equal. Serialize session access, avoid concurrent model edits and qualify the exact acquired result set.

## Binding, version and qualification boundary

All 25 routes run synchronously on the caller's established COM apartment through a shared internal gate. Captured receiver identity is checked before and after the query. Dispose/refresh/replacement cannot publish a result from a stale captured receiver. The gate coordinates these new queries only; direct COM access, existing other-adapter calls, external processes and other wrappers remain outside it.

The selected 2024–2026 contracts are common or differ only in wording. OutputAdvancedReader supplies one shared implementation rather than artificial per-year classes. Future version-sensitive policies must select the acquired native wrapper.StaadVersion, not the host/client ApplicationVersion. No existing direction-code or uncertain layout gate is bypassed.

Managed native-signature fixtures and isolated installed-package examples verify dispatch packets, return decoding, owned data and documentation. They do not establish live COM marshaling, output freshness or engineering correctness. Live 2024–2026 qualification remains pending.

## Remaining work and reminder

The [Output matrix](reports/OpenSTAAD-Output-Multi-Version-Architecture-and-Plan.md#3-complete-cross-version-function-matrix) retains twenty pending documented routes. Member-end displacement layout and three direction-code policies, plate corner/cut/point buffers, scalar-by-value BucklingFactor/NLNodeDisplacements transport, missing-mass metadata absence, solid Von Mises layout, advanced steel-design blocks, time-history extrema and newer beam-stress queries need their own review. Thirteen supplemental unit declarations remain outside the documented union.

Property is limited to documented routes for this increment. **Remind the user before further supplemental Property work or the next public release:** nine documented Property gates and fifteen supplemental discovery candidates remain, and the supplemental candidates need separate review/approval. The parked Load work retains its explicit-approval requirement. See the [Property completion record](Property-Profile-Names-and-Final-Cleanup.md#remaining-contract-gates) and [roadmap](../ROADMAP.md).

The [contract index](contracts/OutputAdvancedContractIndex.json) records every managed signature and comparative usage explanation. The [native inventory](reports/OpenSTAAD-Output-API-Coverage-and-Contract-Inventory.md) retains installed help declarations and main-source evidence.
