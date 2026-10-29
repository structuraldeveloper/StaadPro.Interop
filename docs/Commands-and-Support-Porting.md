# Analysis commands and Support foundation operations

**Implemented:** 26 October 2026, on `main`. The separate `main-api-usability` branch remains unchanged.

This increment adds **25 managed methods**: 17 Commands methods covering all sixteen active documented analysis-command routes, and eight Support methods. Support now covers **27/35 documented routes**. Commands reaches **16/17 active documented native names** (the eighteenth documented name is deprecated and excluded).

Seventeen Commands methods and three Support targets adapt the main reference implementation; five additional foundation-removal routes use the installed Bentley contracts because the main adapter has no wrappers for them.

Commands configure model instructions; Root methods execute the solver. A submission receipt does not establish analysis execution, convergence, code compliance, license availability, saved state or result freshness. The methods share the reviewed installed 2024–2026 contracts, with one common internal service per domain and no artificial year-specific copies.

## Commands: exact contracts

### PerformAnalysis

~~~csharp
CommandSubmissionResult PerformAnalysis(PrintOption printOption);
~~~

Configures first-order elastic analysis instructions. Unlike Root.AnalyzeEx, this adds model commands and never runs the solver. Native void completion supplies no success flag.

### PerformPDeltaAnalysisNoConverge

~~~csharp
CommandSubmissionResult PerformPDeltaAnalysisNoConverge(int numberOfIterations, PrintOption printOption);
~~~

Configures the documented small-delta P-Delta command. Use PerformPDeltaAnalysisEx to select large versus small delta or geometric stiffness. The deprecated Converge entry is not used. Native void gives no acknowledgement.

### PerformPDeltaAnalysisEx

~~~csharp
CommandSubmissionResult PerformPDeltaAnalysisEx(int numberOfIterations, PrintOption printOption, PDeltaDisplacementEffect displacementEffect, bool includeGeometricStiffness);
~~~

Configures P-Delta instructions with an explicit displacement formulation. SmallDelta is native 1 and LargeDelta 0; geometric stiffness is a separate Int16 0/1 flag. Unlike the NoConverge shortcut, both choices are explicit. Native void gives no acknowledgement.

### PerformPDeltaAnalysis

~~~csharp
CommandSubmissionResult PerformPDeltaAnalysis(PDeltaAnalysisOptions options);
~~~

Configures P-Delta instructions from the main-project typed options. This convenience delegates once to PerformPDeltaAnalysisEx and reports that exact native function. It does not call both P-Delta variants or execute analysis. Native void gives no acknowledgement.

### PerformCableAnalysis

~~~csharp
CommandSubmissionResult PerformCableAnalysis(int numberOfIterations, PrintOption printOption);
~~~

Configures the legacy cable-analysis instruction. Requires cable members and a compatible license/context. Native may select its licensed algorithm. Use PerformCableAnalysisEx for explicit algorithm and parameter packets. PrintModeShapes=4 is unavailable here. Native void gives no acknowledgement.

### PerformCableAnalysisEx

~~~csharp
CommandSubmissionResult PerformCableAnalysisEx(CableAnalysisOptions options);
~~~

Configures basic or advanced cable analysis using typed settings. Basic sends seven Double parameters and null advanced flags; Advanced sends three Double parameters and two Int16 flags. Source defaults and validation are retained. PrintModeShapes=4 is unavailable. Native 1/true acknowledges configuration, 0/false reports failure; neither proves license availability or convergence.

### PerformBucklingAnalysis

~~~csharp
CommandSubmissionResult PerformBucklingAnalysis(int maximumSteps, PrintOption printOption);
~~~

Configures legacy buckling-analysis instructions. Use PerformBucklingAnalysisEx to choose Iterative or Eigen explicitly and obtain a native acknowledgement. PrintModeShapes=4 is unavailable here. Native void gives no acknowledgement.

### PerformBucklingAnalysisEx

~~~csharp
CommandSubmissionResult PerformBucklingAnalysisEx(BucklingAnalysisMethod method, int maximumSteps, PrintOption printOption);
~~~

Configures buckling analysis with an explicit solver method. Iterative=0 and Eigen=1 are native selectors. maximumSteps applies to the basic solver as documented; it is not a promised iteration count for every engine. PrintModeShapes=4 is reserved. Native 1/true acknowledges and 0/false fails.

### PerformDirectAnalysis

~~~csharp
CommandSubmissionResult PerformDirectAnalysis(DirectAnalysisOptions options);
~~~

Configures AISC Direct Analysis instructions. Uses LRFD=1 or ASD=2, four Double parameters and two Int16 flags in the documented order. P-Delta iterations above 15 are rejected rather than silently reset. Translation/rotation displacement tolerance retains the native mixed convention; no unit conversion occurs. Native 1/true acknowledges and 0/false fails.

### PerformNonlinearAnalysisEx

~~~csharp
CommandSubmissionResult PerformNonlinearAnalysisEx(NonlinearAnalysisOptions options);
~~~

Configures nonlinear-analysis and optional displacement-control instructions. ArcLength zero disables displacement control. Null target sends native null; an explicit target sends joint ID, global DOF and signed target value as three Doubles. Rebuild and geometric stiffness remain independent selectors. Native 1/true acknowledges and 0/false fails. No result or convergence is inferred.

### SetFloorDiaphragmBaseCommand

~~~csharp
CommandSubmissionResult SetFloorDiaphragmBaseCommand(double baseElevation);
~~~

Adds or updates the floor-diaphragm base instruction. Elevation is a finite signed value in current native length units, not a node ID or floor number. Does not create diaphragm geometry. Native 1/true acknowledges and 0/false fails.

### DeleteFloorDiaphragmBaseCommand

~~~csharp
CommandSubmissionResult DeleteFloorDiaphragmBaseCommand();
~~~

Deletes the floor-diaphragm base instruction. This removes command configuration, not diaphragm entities or an analysis result. Native 1/true acknowledges and 0/false fails; false is not interpreted as proven absence.

### SetCheckSoftStoryCommand

~~~csharp
CommandSubmissionResult SetCheckSoftStoryCommand(SoftStoryDesignCode designCode);
~~~

Adds or updates the soft-story checking instruction. Native codes are IS1893:2002=1, ASCE7:2005/2010/2016=2, IS1893:2016=3. This selector differs from the irregularities selector, whose value 1 is unavailable. Native 1/true acknowledges and 0/false fails.

### DeleteCheckSoftStoryCommand

~~~csharp
CommandSubmissionResult DeleteCheckSoftStoryCommand();
~~~

Deletes the soft-story checking instruction. Does not run a structural-code check or delete model stories. Native 1/true acknowledges and 0/false fails.

### SetCheckIrregularitiesCommand

~~~csharp
CommandSubmissionResult SetCheckIrregularitiesCommand(IrregularitiesDesignCode designCode);
~~~

Adds or updates the irregularities checking instruction. Requires floor-diaphragm data in the native model. ASCE7:2016=2 and IS1893:2016=3; value 1 is not supported by this contract. No applicability or code-compliance conclusion is produced. Native 1/true acknowledges and 0/false fails.

### DeleteCheckIrregularitiesCommand

~~~csharp
CommandSubmissionResult DeleteCheckIrregularitiesCommand();
~~~

Deletes the irregularities checking instruction. Does not delete floor diaphragms or clear existing engineering results. Native 1/true acknowledges and 0/false fails.

### DeleteAllAnalysisCommands

~~~csharp
CommandSubmissionResult DeleteAllAnalysisCommands();
~~~

Deletes the native perform-analysis command inventory. Native 1/true acknowledges deletion. Native 0/false specifically means no perform-analysis commands were present and returns NoCommandsPresent, not a generic failure. Other model instructions and files are not asserted to be cleared.

## Support: exact contracts

### CreateElasticMat

~~~csharp
SupportMutationResult CreateElasticMat(FoundationSupportDirection direction, double subgradeModulus, bool printInfluenceAreas = false, FoundationSpringType springType = FoundationSpringType.None);
~~~

Creates an elastic-mat support definition without assigning it. Uses one scalar Double subgrade in force/length cubed, direction 0..5 and separate Long print/spring flags. This differs from plate-mat scalar/array creation, which remains gated. A positive return is a support definition ID; zero is native failure, unlike fixed-support creation whose failure is -1.

### CreateElasticFooting

~~~csharp
SupportMutationResult CreateElasticFooting(double length, double width, FoundationSupportDirection direction, double subgradeModulus);
~~~

Creates an elastic-footing definition without assigning it. Length and width must be finite and positive in current native length units. Subgrade is finite and nonnegative in force/length cubed, not a nodal spring stiffness. Direction 0..5 preserves X/Y/Z versus XOnly/YOnly/ZOnly. Positive return is a definition ID; zero is native failure.

### RemoveElasticMat

~~~csharp
SupportMutationResult RemoveElasticMat(int supportId);
~~~

Removes an elastic-mat definition by its support reference ID. This definition-level call differs from RemoveElasticMatFromNode. Shared-assignment consequences are left to STAAD and are not enumerated or certified. Actual Boolean or integral 1/0 acknowledgements are supported; undocumented negative values are rejected.

### RemoveElasticMatFromNode

~~~csharp
SupportMutationResult RemoveElasticMatFromNode(int nodeId);
~~~

Removes the elastic-mat assignment from one analytical node. The argument is a node ID, not a support definition ID. No shared-definition deletion is inferred. TRUE/FALSE is decoded independently of the zero-success legacy AssignSupportToNodes route. Actual Boolean or integral 1/0 acknowledgements are supported; undocumented negative values are rejected.

### RemovePlateMat

~~~csharp
SupportMutationResult RemovePlateMat(int supportId);
~~~

Removes a plate-mat definition by its support reference ID. Accepts an existing definition ID even though plate-mat creation/readback remain gated. This is not the unresolved two-argument RemovePlateMatFromPlate operation. Shared-assignment consequences are left to STAAD. Actual Boolean or integral 1/0 values implement the documented TRUE/FALSE return; negative values are rejected.

### RemoveElasticFooting

~~~csharp
SupportMutationResult RemoveElasticFooting(int supportId);
~~~

Removes an elastic-footing definition by its support reference ID. This definition-level operation differs from node-specific removal. Its documented numeric 1/0 convention is decoded narrowly; integral -1 is rejected, not treated as true. No assignment enumeration, effect verification or automatic deletion of other foundations is promised.

### RemoveElasticFootingFromNode

~~~csharp
SupportMutationResult RemoveElasticFootingFromNode(int nodeId);
~~~

Removes the elastic-footing assignment from one analytical node. The argument is an analytical node ID, not a support definition ID. Native 1/true acknowledges and 0/false fails. Integral -1 is not a success flag for this operation. Shared definition deletion is not inferred.

### AssignSupportToEntities

~~~csharp
SupportMutationResult AssignSupportToEntities(int supportId, SupportEntityKind entityKind, IEnumerable<int> entityIds, int maximumResults = 100000);
~~~

Assigns a support through the native entity-list route with an explicit requested entity family. Calls AssignSupportToEntityList(supportId, Int32[]), preserving order/duplicates in an owned packet. Plates are required for plate-mat definitions; nodes for other types. entityKind records caller intent and is not a native selector or proof of the definition type. The caller must choose the matching family. Unlike AssignSupportToNodes, native TRUE/FALSE uses actual Boolean or integral 1/0 rather than integral zero-success. Empty input is NoOperation without native invocation. Receipts preserve EntityIds/EntityKind; NodeIds is empty for plates.

## Result and input semantics

`CommandSubmissionResult` exposes Status, IsSuccessful, NativeFunction and the actual integral/Boolean native evidence directly; it does not have Data. Explicit 1/true gives Succeeded. Zero/false gives NativeFailure except DeleteAllAnalysisCommands, where the documented meaning is NoCommandsPresent. Documented void calls give InvokedWithoutAcknowledgement, with IsSuccessful false and no fabricated native code. Void completion is not a negative acknowledgement. Unsupported return types, unknown codes, null/DBNull flags and malformed packets throw. No generic truthy/nonzero conversion occurs.

`SupportMutationResult` is reused. Foundation creation returns a positive support definition ID or native failure at zero; this differs from existing fixed/pinned creation at -1. Native Boolean or integral 1/0 acknowledges the new removal/assignment routes. Undocumented negative flags are rejected. AssignSupportToNodes retains its separate integral zero-success contract. Native failure and transport exceptions can follow partial changes; no rollback, automatic save or retry is attempted.

`AssignSupportToEntities` requires an explicit managed `SupportEntityKind`. Plate-mat definitions take plate IDs; other support types take node IDs. The family is caller intent, not a native selector or verification of the support definition. IDs are enumerated once, bounded to maximumResults (1..100000), copied, and returned in owned EntityIds/EntityKind metadata with order/duplicates preserved. NodeIds is empty for plate requests. A separate native input copy prevents native rewriting from changing the receipt. Empty input gives NoOperation without native invocation. Definition-level removal is distinct from node unassignment; effects on shared assignments are left to STAAD and are not certified.

Foundation subgrade modulus is force/length cubed; footing dimensions and base elevation use current native length units. Translational and rotational displacement-control/tolerance settings keep their native length/angle conventions. No unit conversion or fallback-unit inference occurs.

## Reused main-source types

Ported the main project’s PrintOption, BucklingAnalysisMethod, seven analysis selector enums and eight option/parameter classes. Retained their defaults and validation, including Basic cable’s seven values, Advanced cable’s three values/two flags, Direct Analysis’s four values/two flags and the nonlinear optional three-value target. Default cable nested option objects are required even when inactive; only the active algorithm’s parameter values are marshaled. Native packets are fresh arrays. Do not mutate mutable settings or nested settings concurrently with submission.

Reused FoundationSupportDirection, FoundationSpringType, SupportMutationResult, strict native value readers and the existing Support binding/gate. Added only the managed node/plate family selector and receipt metadata needed to distinguish assignment identities. Main source files were inspected read-only. Source helper validation is internal under Services/Commands; public options, enums, interfaces and adapters remain separately organized. No public ref/out, tuples, partial adapter classes or thread-pool COM dispatch were added.

## Usage

~~~csharp
IOSCommands commands = wrapper.Commands;
var options = new StaadPro.Interop.Models.Commands.PDeltaAnalysisOptions();
bool applyChanges = false;
if (applyChanges)
{
    var submission = commands.PerformPDeltaAnalysis(options);
    Console.WriteLine(submission.Status); // Void completion is unacknowledged.
    // Execute analysis separately through an appropriate Root method.
}
~~~

~~~csharp
IOSSupport support = wrapper.Support;
bool applyChanges = false;
if (applyChanges)
{
    var creation = support.CreateElasticMat(FoundationSupportDirection.Y, 25000.0);
    if (creation.IsSuccessful)
    {
        var assignment = support.AssignSupportToEntities(creation.SupportId.Value,
            StaadPro.Interop.Enums.Support.SupportEntityKind.Nodes, new[] { 17, 41 });
        Console.WriteLine(assignment.Status);
    }
}
~~~

Use the existing StaadPro.Interop.Adapters.Interfaces and StaadPro.Interop.Enums namespaces. Calls run synchronously on the caller’s established COM apartment. Each domain gate serializes its typed calls and checks captured binding identity before and after dispatch. External model edits, raw calls, other adapters and other wrappers are not locked out. Reacquire an adapter after wrapper refresh. A post-call error may occur after native mutation.

## Remaining work

Commands still needs CreateSteelDesignCommand and its source catalog-query/validation conveniences, with operand-schema qualification. The deprecated PerformPDeltaAnalysisConverge is excluded. The eight remaining Support routes are GetSupportName, CreateInclinedSupport, CreatePlateMat, RemovePlateMatFromPlate, GetSupportInformationEx, GetPlateMatSupportId, GetPlateMatDetail and GetPlateMatAssignmentList. Their identity, scalar/array, reference-direction and arity gates remain explicit.

Output remains at 47/60 native routes and 96 managed overloads. Its unresolved native contracts are not treated as solved by this increment. Parked Property/Load work and their approval checkpoints remain unchanged. These additions are included in preview 1.0.0-preview.20261027.1; date-based package numbering is retained.

## Verification

Release validation passed all 6,320 offline tests, including 284 new Commands/Support regression cases. All 50 new interface/adapter XML examples compiled and executed with mutation guards disabled; no native calls occurred. The Release consumer build completed with zero warnings and errors. Managed tests do not certify live COM marshaling, native model effects or engineering behavior. No real model mutation or solver execution is used by these checks.

See [the exact contract index](contracts/CommandsAndSupportPortContractIndex.json), [Commands architecture](reports/OpenSTAAD-Commands-Multi-Version-Architecture-and-Plan.md) and [Support architecture](reports/OpenSTAAD-Support-Multi-Version-Architecture-and-Plan.md).
