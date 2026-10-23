# Output review and implementation status

**Updated:** 23 October 2026

IOSOutput and the single non-partial OSOutputAdapter now expose **15 typed queries**, covering 15 of the 60 documented native Output names. Forty-five documented native names remain pending. Thirteen supplemental metadata-only unit candidates remain a separate contract-discovery task. These are source additions after the published preview; no NuGet archive was built or published for this increment.

The existing Root member-force service continues to call AreResultsAvailable and GetMemberEndForces independently. Its public behavior and the wrapper are unchanged.

## Implemented queries

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

## Units, transport and lifetime

Values remain in native Output units. Wrapper input-unit caches do not prove those units. No implicit scaling, degree/radian conversion, sign change, offset transfer, case activation or model mutation occurs.

- End-force queries pass the explicit local-coordinate flag zero and retain native start/end actions.
- Support reactions remain global reactions; they are not negated into support-applied actions.
- Plate-center “forces” are stresses; plate moments are moments per unit width.
- Static-check loads and reactions remain separate. No assumed algebraic combination is labeled an equilibrium residual.
- Principal-angle values preserve native angular units. The parameter prose says float, while the concrete installed VBA examples allocate Double arrays. Both methods initialize Double buffers and accept returned Single or Double arrays after shape/finite-value validation; live marshaling still needs qualification.

All output arrays start as Double NaN sentinel buffers, following the concrete native examples. Native success requires exact rank, length, floating-point element type and written finite values. Nonzero SAFEARRAY lower bounds are supported. Native false never triggers an alternate-API retry.

Boolean contracts accept Boolean or exact integral 0/1/-1, with -1 representing VARIANT_TRUE. Buckling availability uses the narrower documented 0/1 convention, also accepting Boolean values. Nulls, strings, decimals, fractional values and unknown codes are rejected.

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

The strictly read-only main project exposes **86 managed overloads**, implemented by 86 public adapter methods backed by **17 native targets**. Thirty-three return Task; two expose out outputs that should become named results in this repository. Four offset-aware/physical-joint overloads depend on an unported Property operation. The contract annex records every signature, dependency and required adaptation.

Both installed 2025/2026 vendor IOSOutputUI interfaces contain 72 matching metadata methods. Thirteen additional unit queries appear in all three native class listings without detailed contracts, with a reference/value mismatch against interop metadata; they need contract discovery, and GetMissingMassParticipationFactors is documented but absent from those interfaces. These evidence gaps require acquired-server verification, not version guesses.

## Roadmap progress and validation

Implemented the O1 foundation, all five O2 typed mappings, all seven O3 plate-center mappings and three independent O5 queries. Native qualification exit criteria remain pending for every phase. O4 corner/cut/point contracts retain unresolved layouts, axes or invocation discrepancies; the roadmap allows independent queries to proceed while those are resolved. The remaining O5 queries and O6–O11 remain pending.

Use the acquired wrapper.StaadVersion internally. Keep one non-partial adapter, no new public ref/out or tuple outputs, and complete XML documentation for interfaces, adapters and result models. Native ambiguity must remain explicitly unverified until resolved.

Reused the main-source node/static-check vocabulary and plate component/surface order as immutable snapshots. Reused MemberEnd, strict native integral readers and the existing Forces model for editable copies. The main project remains strictly read-only. Source Task/parallel COM behavior, Property-offset conveniences and batch APIs were not copied.

Offline tests cover all 15 call packets, caller-thread execution, native failures, scalar/array sentinels, Single/Double principal buffers, nonzero lower bounds, ordering, ownership, design sentinels, stale/disposed bindings and expanded matching XML contracts. These tests do not run STAAD or certify native engineering behavior. The target remains net481 and the published preview remains unchanged.
