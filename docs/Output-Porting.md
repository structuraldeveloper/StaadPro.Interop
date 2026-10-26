# Output review and implementation status

**Updated:** 26 October 2026

IOSOutput and the single non-partial OSOutputAdapter now expose **96 managed overloads**, covering 47 of the 60 documented native Output names. Thirteen documented routes and thirteen supplemental unit candidates remain separate work. The preceding 25 routes are detailed in the [extended Output guide](Output-Extended-Queries.md). The previously prepared preview 1.0.0-preview.20261026.1 includes the earlier 40-query surface and passes isolated package-consumer verification; it is not publicly published.

The existing Root member-force service continues to call AreResultsAvailable and GetMemberEndForces independently. Its public behavior and the wrapper are unchanged.

## Current implementation - plate-corner workflows

Added **25 Output overloads** for plate-corner extraction, indexed sparse/Cartesian selections, explicit metadata families and member-local projections: 18 synchronous main-source adaptations plus seven typed enabling overloads. Output now exposes **96 managed overloads reaching 47/60 documented native names**, with **13 documented native routes pending**. All additions use the newly enabled GetPlateCornerForces route. See [the plate-corner guide](Output-Plate-Corners.md) for exact signatures, comparisons, axes evidence and limitations.

Passed a Release build with zero warnings/errors and **6,036 offline tests**, including **248 new corner cases**. Isolated package verification covers **1,152 selected XML examples**, including all 192 Output examples. Read-only native checks support the global force/moment contract on sampled STAAD.Pro 2026.0.0.340 observations; broader native/engineering qualification remains pending.


## Managed Output workflows - current source

Added **25 managed Output overloads**: 23 synchronous main-project workflow adaptations and two typed enabling overloads for explicit native axes and Beam ratio queries. Output now exposes **71 managed overloads reaching 46/60 documented native names**. The fourteen native contract gates remain; this increment adds compositions, entity conveniences and ordered batches over qualified native routes. See [the managed workflow guide](Output-Managed-Workflows.md) for exact contracts and comparative usage.

Added governing-case resolution, per-item batch outcomes, staged caller utilization caches, native global offset-transfer audit packets, geometry bases, plate-center composites, ordered static checks and opposite support actions. All new APIs have matching XML documentation and examples; no public ref/out, tuples, partial adapters or thread-pool COM calls were introduced.

## Earlier steel-design-block increment

Implemented six further common documented Output routes for named design-parameter-block observations and independent failure-ratio limits. Typed Output coverage is **46/60**, with **14 documented routes pending**. Matching interface/adapter XML, named subset results and exact block identity distinguish the new overloads from the existing member-only APIs. See [the steel-block guide](Output-Steel-Design-Blocks.md).

Release build passed with zero warnings/errors and **5,523 offline tests** (170 new cases). Isolated documentation/package verification covers **1,052 selected XML examples**, including all 92 Output and 38 Support examples. Managed fixtures do not certify live 2024–2026 COM or engineering behavior.

## First fifteen queries

| Managed query | Native function | Qualified payload |
| --- | --- | --- |
| GetAnalysisResultAvailability() | AreResultsAvailable | Boolean availability observation |
| GetNodeDisplacements(nodeId, loadCaseId) | GetNodeDisplacements | X/Y/Z/Rx/Ry/Rz |
| GetSupportReactions(nodeId, loadCaseId) | GetSupportReactions | Global Fx/Fy/Fz/Mx/My/Mz reactions |
| GetMemberEndForces(memberId, memberEnd, loadCaseId) | GetMemberEndForces | Local Fx/Fy/Fz/Mx/My/Mz actions |
| GetStaticCheckResult(loadCaseId) | GetStaticCheckResult | Separate applied-load and reaction totals |
| GetPlateCenterStressesAndMoments(plateId, loadCaseId) | GetAllPlateCenterStressesAndMoments | Five stresses and three moments per width |
| GetPlateCenterNormalPrincipalStresses(plateId, loadCaseId) | GetPlateCenterNormalPrincipalStresses | Top/bottom maximum/minimum principal stresses |
| GetPlateCenterForces(plateId, loadCaseId) | GetAllPlateCenterForces | Sqx/Sqy/Sx/Sy/Sxy stresses |
| GetPlateCenterMoments(plateId, loadCaseId) | GetAllPlateCenterMoments | Mx/My/Mxy moments per unit width |
| GetPlateCenterPrincipalStressesAndAngles(plateId, loadCaseId) | GetAllPlateCenterPrincipalStressesAndAngles | Six principal/shear stresses and two angles |
| GetPlateCenterPrincipalStressesAndAnglesEx(plateId, loadCaseId) | GetAllPlateCenterPrincipalStressesAndAnglesEx | Same payload from explicit separate buffers |
| GetPlateCenterVonMisesStresses(plateId, loadCaseId) | GetPlateCenterVonMisesStresses | Top/bottom Von Mises stresses |
| GetMemberDesignSectionName(memberId) | GetMemberDesignSectionName | Exact section-name text or NotFound |
| GetMemberSteelUtilizationRatio(memberId) | GetMemberSteelDesignRatio | Finite nonnegative ratio or documented sentinel status |
| GetBucklingResultAvailability() | IsBucklingAnalysisResultsAvailable | Boolean buckling availability observation |

Every query returns OutputQueryResult&lt;T&gt;. Check IsSuccessful before reading Data; unsuccessful Data access throws InvalidOperationException. Native false retrieval produces NativeFailure without publishing a payload. Transport/binding errors propagate; malformed successful responses throw.

A successful availability observation can contain Data=false. Empty section text produces NotFound. Only a successful steel-ratio retrieval interprets -999 as MemberNotDesigned and -1 as AnalysisNotPerformed. Neither sentinel is exposed as an engineering ratio; native false takes precedence over buffer contents.

EntityId and LoadCaseId retain applicable requested IDs even on failure. End-force queries additionally retain MemberEnd. Public signatures expose no ref/out or tuples. Payloads are owned immutable snapshots; ForceComponents.ToForces creates an independent copy using the existing force algebra and conversion model.

## Twenty-five extended queries

Implemented **25 additional documented Output routes**, bringing IOSOutput and the single non-partial OSOutputAdapter to **40 typed queries covering 40/60 native names**, with **20 documented routes pending**. Added member/physical-member results, solid stresses, mat/base-pressure snapshots, modal/buckling observations, nonlinear load steps, steel-design observations and time-history queries. See [the extended contracts and comparative usage guide](Output-Extended-Queries.md).

The selected contracts are shared or cosmetic across the installed 2024–2026 references. One internal common reader performs native dispatch and validation; direction-code policies, uncertain member-end layouts, cut/point buffers and incompatible scalar-output transports remain separate work. No new public ref/out, tuples or raw VARIANT packets are exposed. Existing wrapper/provider behavior is unchanged.

Validation passed **5,353 offline tests**, including 165 new Output cases and 32 Property completion cases. The isolated installed-package consumer verifies **1,002 selected XML examples**, including all 80 Output examples and 510 Property/capability examples. Live native marshaling and engineering qualification remain pending.

- `GetMemberSteelDesignResults` → `GetMemberSteelDesignResults`.
- `GetBucklingFactorCount` → `GetNoOfBucklingFactors`.
- `GetBucklingModeDisplacements` → `GetBucklingModeDisplacementAtNode`.
- `GetRelativeMemberDisplacement` → `GetIntermediateMemberTransDisplacements`.
- `GetAbsoluteMemberDisplacement` → `GetIntermediateMemberAbsTransDisplacements`.
- `GetPhysicalMemberEndForces` → `GetPMemberEndForces`.
- `GetMemberAxialForceExtrema` → `GetMinMaxAxialForce`.
- `GetMemberForcesAtDistance` → `GetIntermediateMemberForcesAtDistance`.
- `GetPhysicalMemberForcesAtDistance` → `GetPMemberIntermediateForcesAtDistance`.
- `GetMemberDeflectionAtDistance` → `GetIntermediateDeflectionAtDistance`.
- `GetMatInfluenceAreas` → `GetMatInfluenceAreas`.
- `GetBasePressures` → `GetBasePressures`.
- `GetSolidNormalStresses` → `GetAllSolidNormalStresses`.
- `GetSolidShearStresses` → `GetAllSolidShearStresses`.
- `GetSolidPrincipalStresses` → `GetAllSolidPrincipalStresses`.
- `GetNonlinearLoadStep` → `GetNLLoadStep`.
- `GetSteelDesignParameterBlockCount` → `GetSteelDesignParameterBlockCount`.
- `GetExtractedModeCount` → `GetNoOfModesExtracted`.
- `GetModeFrequency` → `GetModeFrequency`.
- `GetModalNodeDisplacements` → `GetModalDisplacementAtNode`.
- `GetModalMassParticipationFactors` → `GetModalMassParticipationFactors`.
- `GetTimeHistoryIntegrationInfo` → `GetTimeHistoryIntegrationStepInfo`.
- `GetTimeHistoryResponseAtTime` → `GetTimeHistoryResponseAtTime`.
- `GetTimeHistoryResponse` → `GetTimeHistoryResponse`.
- `GetMultipleSteelDesignResultAvailability` → `IsMultipleMemberSteelDesignResultsAvailable`.

## Units, transport and lifetime

Values remain in native Output units. Wrapper input-unit caches do not prove those units. The original native-query overloads perform no implicit scaling, degree/radian conversion, sign change, offset transfer, case activation or model mutation. The explicitly named managed workflows document their sign reversal, offset transfer or caller-cache updates separately.

- Original three-argument end-force queries pass local flag zero. New typed overloads request Local/Global directly and retain native start/end actions.
- Support reactions remain global reactions; they are not negated into support-applied actions.
- Plate-center “forces” are stresses; plate moments are moments per unit width.
- Static-check loads and reactions remain separate. No assumed algebraic combination is labeled an equilibrium residual.
- Principal-angle values preserve native angular units. The parameter prose says float, while the concrete installed VBA examples allocate Double arrays. Both methods initialize Double buffers and accept returned Single or Double arrays after shape/finite-value validation; live marshaling still needs qualification.

All output arrays start as Double NaN sentinel buffers, following the concrete native examples. Native success requires exact rank, length, floating-point element type and written finite values. Nonzero SAFEARRAY lower bounds are supported. Native false never triggers an alternate-API retry.

The first fifteen ordinary Boolean retrieval contracts accept Boolean or exact integral 0/1/-1, with -1 representing VARIANT_TRUE. Buckling availability uses the narrower documented 0/1 convention, also accepting Boolean values. Nulls, strings, decimals, fractional values and unknown codes are rejected.

Calls execute synchronously on the established caller COM apartment. The implementation uses shared internal services for these identical 2024–2026 contracts; it introduces no artificial per-year classes. Future version-sensitive contracts must route from wrapper.StaadVersion, never ApplicationVersion.

Disposed or replaced captured bindings are rejected. Reacquire wrapper.Output after refresh and serialize session access. Binding identity does not detect every unsaved model edit, concurrent change or stale analysis.

## Usage

~~~csharp
using StaadPro.Interop.Enums;

// wrapper is already acquired; execute on its established COM apartment.
var output = wrapper.Output;
if (output != null && output.GetAnalysisResultAvailability().Data)
{
    var result = output.GetMemberEndForces(17, MemberEnd.Start, 1);
    if (result.IsSuccessful)
        System.Console.WriteLine(result.Data.ToForces().Fx);
    else
        System.Console.WriteLine(result.Status);
}
~~~

Identifiers must refer to suitable results in the acquired model. Availability alone does not certify freshness or engineering correctness.

## Cross-version review

- [Output comparison, architecture and implementation sequence](reports/OpenSTAAD-Output-Multi-Version-Architecture-and-Plan.md)
- [Complete native contracts, private-source overloads and reuse inventory](reports/OpenSTAAD-Output-API-Coverage-and-Contract-Inventory.md)
- [Machine-readable comparison and fingerprints](reports/data/OpenSTAAD-Output-2024-2026-Comparison.json)

Reviewed all installed 2024/2025/2026 Output help sections and Output-owned steel-result functions filed under Design. Counts are **59 / 60 / 60 native names**, with 59 common and 60 in the union. GetMaxBeamStresses is newly documented in 2025; four common contracts need direction/layout policy verification. Normalized 2025/2026 declarations, contracts and examples match.

The strictly read-only main project exposes **86 managed overloads**, implemented by 86 public adapter methods backed by **17 native targets**. Thirty-three return Task; two expose out outputs that should become named results in this repository. The four offset-aware/physical-joint overloads now reuse the implemented Property offset query; the latest workflow guide records their managed adaptations. The contract annex records every signature, dependency and required adaptation.

Both installed 2025/2026 vendor IOSOutputUI interfaces contain 72 matching metadata methods. Thirteen additional unit queries appear in all three native class listings without detailed contracts, with a reference/value mismatch against interop metadata; they need contract discovery, and GetMissingMassParticipationFactors is documented but absent from those interfaces. These evidence gaps require acquired-server verification, not version guesses.

## Roadmap progress and validation

Implemented the O1 foundation, all five O2 typed mappings, all seven O3 plate-center mappings and three independent O5 queries. Native qualification exit criteria remain pending for every phase. O4 corner/cut/point contracts retain unresolved layouts, axes or invocation discrepancies; the roadmap allows independent queries to proceed while those are resolved. The new increment adds independent common O5–O11 queries while the remaining twenty routes retain their specific gates. Native phase-exit qualification remains pending.

Use the acquired wrapper.StaadVersion internally. Keep one non-partial adapter, no new public ref/out or tuple outputs, and complete XML documentation for interfaces, adapters and result models. Native ambiguity must remain explicitly unverified until resolved.

Reused the main-source node/static-check vocabulary and plate component/surface order as immutable snapshots. Reused MemberEnd, strict native integral readers and the existing Forces model for editable copies. The main project remains strictly read-only. Source Task/parallel COM behavior, Property-offset conveniences and batch APIs were not copied.

Offline tests cover all 40 query packets, caller-thread execution, native failures, scalar/array sentinels, Single/Double principal buffers, nonzero lower bounds, ordering, ownership, design sentinels, stale/disposed bindings and expanded matching XML contracts. These tests do not run STAAD or certify native engineering behavior. The target remains net481 and the existing distribution archives remain unchanged.
