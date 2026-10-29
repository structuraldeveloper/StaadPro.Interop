# Design brief and parameter operations

**Implemented:** 27 October 2026 on main, after the [interrupted-work audit](reports/Porting-Continuation-Audit-2026-10-27.md). The API-usability experiment remains isolated on main-api-usability.

This increment implements **ten managed methods over five documented native names**: CreateDesignBrief, AssignDesignParameter, AssignDesignCommand, GetMemberDesignParameters and GetDesignBriefCode. Seven methods adapt all seven common workflows in the read-only main IOSDesign; the code query and two ordered query batches extend that foundation. These are managed workflow ports with improved input/result contracts, not binary signature parity.

Coverage is **5/31** documented Design names in 2024 and **5/45** in 2025/2026. Extended AssignDesignGroup and all Chinese steel operations remain pending; forty union native names are unimplemented. Output-owned steel forces/utilization remain in Output.

## Exact signatures and differences

### CreateDesignBrief

~~~csharp
DesignBriefResult CreateDesignBrief(StaadDesignCode designCode);
~~~

When to use: Creates only the brief; it does not assign parameters, append a design command, run design or save. Unlike assignment methods, the returned nonnegative integer is the brief reference, including zero if returned; -1 is native failure.

### AssignDesignCommand

~~~csharp
DesignMutationResult AssignDesignCommand(int designBriefId, DesignCommandAssignment designCommand);
~~~

When to use: Appends CHECK CODE, SELECT or another typed command; it does not assign a design parameter or execute design. Native 0 acknowledges success and -1 reports failure. Non-GROUP commands pass an empty value. Single-property GROUP passes 0..3 only on acquired STAAD.Pro 2025/2026; 2024/unknown/future GROUP profiles are rejected before mutation. Extended GROUP/SAME AS remains unavailable.

### AssignDesignCommands

~~~csharp
DesignBatchResult AssignDesignCommands(int designBriefId, IEnumerable<DesignCommandAssignment> designCommands, int maximumResults = 100000);
~~~

When to use: Use this for several commands on an existing brief. Materializes once and validates every request/profile before mutation, then sorts by Order with stable input ties. Stops after the first native -1; returns all items with original InputIndex and explicit NotAttempted outcomes. It is not a transaction. An empty selection checks binding but makes no native calls and returns IsNoOperation.

### AssignDesignParameter

~~~csharp
DesignMutationResult AssignDesignParameter(int designBriefId, DesignParameterAssignment designParameter);
~~~

When to use: Assigns a parameter such as FYLD or LY, rather than a CHECK CODE/SELECT instruction. Uses the immutable request's invariant NativeValue and owned Int32 member array without reflection or unit conversion. Code-specific meanings, units, ranges and availability remain native responsibilities. Native 0 acknowledges success; -1 reports failure.

### AssignDesignParametersExisting

~~~csharp
DesignBatchResult AssignDesignParameters(int designBriefId, IEnumerable<DesignParameterAssignment> designParameters, int maximumResults = 100000);
~~~

When to use: Reuses the supplied brief instead of creating another. Materializes once, orders by Order with stable ties, stops on the first native -1 and retains both failed and unattempted items. Validates the complete input before the first mutation. Empty input makes no native calls. No rollback, automatic command creation or design execution occurs.

### AssignDesignParametersNew

~~~csharp
DesignBatchResult AssignDesignParameters(StaadDesignCode designCode, IEnumerable<DesignParameterAssignment> designParameters, int maximumResults = 100000);
~~~

When to use: Unlike the brief-ID overload, creates a new brief after validating all requests. Creation -1 prevents every assignment. Later assignment failure preserves Creation and the created DesignBriefId, including zero if returned, with explicit unattempted items. An empty selection creates no brief. This operation does not append CHECK CODE or run design, and never deletes a partially populated brief.

### GetMemberDesignParametersSingle

~~~csharp
DesignQueryResult<MemberDesignParametersResult> GetMemberDesignParameters(int designBriefId, int memberId, int maximumParameters = 100000);
~~~

When to use: Retrieves assigned/default parameter metadata, not steel utilization or design forces. Native 0 is success and -1 is failure; failure has no Data. A successful acknowledged zero-count holder returns an empty Parameters list. Names, textual values, unit labels, descriptions and default flags are copied in zero-based native row order before the private holder is released. No parsing or unit conversion occurs.

### GetDesignBriefCode

~~~csharp
DesignQueryResult<DesignBriefCodeObservation> GetDesignBriefCode(int designBriefId);
~~~

When to use: Looks up a brief; it does not create one. Native zero means NotFound, unlike zero-success assignments and a valid zero reference from creation. Positive codes are preserved in NativeCode; KnownCode is null for unlisted codes. Negative or malformed code returns throw instead of being treated as absence.

### GetDesignBriefCodes

~~~csharp
IReadOnlyList<DesignQueryResult<DesignBriefCodeObservation>> GetDesignBriefCodes(IEnumerable<int> designBriefIds, int maximumResults = 100000);
~~~

When to use: Calls the same single-brief native query once per requested reference, retaining order, duplicates, brief identity and individual NotFound results. Enumerates input once and validates every ID before native calls. Empty input returns an owned empty list after binding validation. Transport/malformed-response errors abort rather than silently dropping items; this is not an atomic model snapshot.

### GetMemberDesignParametersBatch

~~~csharp
IReadOnlyList<DesignQueryResult<MemberDesignParametersResult>> GetMemberDesignParameters(int designBriefId, IEnumerable<int> memberIds, int maximumResults = 100000, int maximumParameters = 100000);
~~~

When to use: Shares one brief while obtaining a separate owned native-holder snapshot per member. Retains duplicates, order and identified native failures, continuing after ordinary -1 query failures. Enumerates and validates all member IDs before querying. Empty input creates no holders. Transport, malformed-output, binding or holder errors abort the batch; completed earlier reads are not an atomic model snapshot.

## Managed requests and main-source reuse

Reused all fifty names/numeric values from the main source's actual StaadDesignCode enum, without the private UI converter/availability attributes. The separate experimental StaadDesignCodeGPT enum is not part of this port. An enum entry is not a license, patch-level compatibility or command-availability guarantee; an unlisted positive observed code is retained in DesignBriefCodeObservation.NativeCode and has null KnownCode.

DesignParameterAssignment and DesignCommandAssignment adapt the source's value/name/order/member-list concepts into sealed immutable requests. They replace reflection over mutable generic requests with explicit contracts and owned selections. MemberDesignParametersResult and MemberDesignParameterInfo retain the main source's payload names/properties as immutable snapshots, with NativeDefaultFlag added to preserve evidence. Main files remain strictly read-only.

A parameter keyword begins with an ASCII letter and contains only letters, digits or underscores. A value may be a nonblank string without control characters, a Boolean, finite numeric scalar, or an enum fitting Int32. Numeric formatting uses invariant culture and round-trip floating-point formatting; Booleans become 1/0. No arbitrary ToString is invoked. Text and units are not normalized or interpreted. The caller supplies the correct units, keyword, code-specific range and engineering intent. The native return remains authoritative.

Commands use typed names: CHECK CODE, FIXED GROUP, GROUP, MEMBER TAKE OFF, SELECT, SELECT OPTIMIZED, SELECT WELD, SELECT WELD TRUSS and TAKE OFF. Non-GROUP values are empty. The later native documentation restricts aluminum to CHECK CODE/SELECT/TAKE OFF and timber to CHECK CODE, and excludes concrete command support; the service does not manufacture acceptance from a code enum or preempt native status with an extra lookup.

## Status, brief identities and batch boundaries

| Operation | Success | Ordinary failure/absence | Data/evidence |
| --- | --- | --- | --- |
| CreateDesignBrief | Nonnegative reference | -1 failure | Actual reference, including zero if returned |
| AssignDesignParameter / AssignDesignCommand | 0 | -1 failure | Named receipt, member selection, actual code |
| GetMemberDesignParameters | 0 plus validated holder | -1 failure | Owned parameter rows only after success |
| GetDesignBriefCode | Positive code | 0 NotFound | Known enum when recognized plus raw code |

Brief arguments accept nonnegative references consistently with the native creation contract, which reserves -1 for failure. The source's assignment-positive/query-nonnegative inconsistency is not copied. A zero *returned code* from GetDesignBriefCode means absence; a zero *status* from assignment means success. Boolean, string and floating status coercions are rejected. Unexpected negative creation/code values and assignment statuses other than 0/-1 throw.

Assignments materialize input once, validate before mutation and sort by Order with input-order ties. Each report retains original InputIndex. On a native -1, subsequent items are NotAttempted with no fabricated NativeReturnCode. The create-and-assign overload retains Creation and the created brief after later failure and performs no rollback. Empty input is IsNoOperation and creates no brief.

Query batches preserve input order, duplicates, requested identities and ordinary per-item failure/absence, and continue after them. Malformed responses, transport failures, holder errors or binding changes abort instead of silently dropping items. Native exceptions remain the original exceptions and may follow earlier completed mutations. These batches are not transactions or atomic model snapshots.

Selections and per-member row counts are bounded by configurable 1..100,000 limits. Request member arrays are capped at 100,000, enumerated once and copied again for native calls. Caller/native mutation of source arrays cannot alter request or result snapshots. No public ref/out, tuples, partial adapter classes or thread-pool COM dispatch were introduced.

## Native holder lifetime and version handling

GetMemberDesignParameters activates a private OSMemberSteelDgnParams CoClass using the GUID verified in both installed 2025/2026 interop assemblies. Activation requires the caller's established STA apartment and matching Bentley COM registration/process bitness. Missing registration/activation failures propagate; no new STAAD root is acquired as a fallback. The common 2024 manual contract is implemented, but actual 2024 holder activation/ABI has not been validated by this work.

Indexed values are read as the native Name/Value/Unit/Description/Default properties with an explicit property-get invocation, without assuming generated interop accessor names are automation methods. This follows the [Type.InvokeMember property-get contract](https://learn.microsoft.com/en-us/dotnet/api/system.type.invokemember?view=netframework-4.8.1); it still requires native holder qualification.

The service preserves the freshly activated holder's native initialization and reads Count after native zero-success acknowledgement. It does not write an undocumented negative sentinel into a native allocation/count property. A native success response requires a valid nonnegative count within maximumParameters, valid string rows, a nonblank name, an integral default flag and a stable final count. A zero-count holder after native success is returned as an acknowledged empty snapshot. The API cannot independently prove whether the native function rewrote a zero that was already present; it relies on the documented success acknowledgement and does not invent additional write evidence. IsDefault preserves the source's nonzero rule, and NativeDefaultFlag retains the original integer.

The exclusively created holder is released on success, native failure and extraction errors. Shared root/Design objects are never released. If cleanup also fails during another exception, the original error is preserved and cleanup evidence is attached as DesignHolderCleanupError in its Data dictionary. Cleanup failure after otherwise successful work propagates. No holder or vendor interop type escapes the public API.

Matching contracts use one internal DesignService. The single-property GROUP grammar is explicitly gated to acquired native StaadVersion major 25/26; 2024, unknown and unreviewed future GROUP profiles fail before the whole batch mutates. The extended AssignDesignGroup route remains pending because its 2024 and later grammars conflict. ApplicationVersion is the host version and is not used for native selection. Chinese steel arity/selector policies and version-exclusive interfaces remain future work; no dummy implementation is exposed.

All typed operations run synchronously under a per-adapter gate and check captured binding identity between native calls and after completion. Raw calls, other wrappers/adapters and external model edits are not locked out. Reacquire adapters after wrapper refresh.

## Usage

~~~csharp
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Design;
using StaadPro.Interop.Models.Design;

bool applyChanges = false;
if (applyChanges)
{
    var assignment = wrapper.Design.AssignDesignParameters(
        StaadDesignCode.ANSIAISC360Year16,
        new[] { new DesignParameterAssignment("FYLD", 250.0, new[] { 17, 41 }) });
    if (assignment.IsSuccessful)
    {
        var command = wrapper.Design.AssignDesignCommand(assignment.DesignBriefId.Value,
            new DesignCommandAssignment(DesignCommandKind.CheckCode, new[] { 17, 41 }));
        System.Console.WriteLine(command.Status);
    }
}
// Run analysis/design and save separately when appropriate for the model.
~~~

~~~csharp
// Requires an STA caller, correct Bentley COM registration and a valid connected model.
bool readParameters = false;
if (readParameters)
{
    var observation = wrapper.Design.GetMemberDesignParameters(1, 17);
    if (observation.IsSuccessful)
        foreach (var row in observation.Data.Parameters)
            System.Console.WriteLine(row.Name + " = " + row.Value + " " + row.Unit);
}
~~~

## Verification and remaining work

Targeted managed regression tests cover all ten methods, native status distinctions, version grammar gates, original exception identity, owned arrays/results, single enumeration, stable partial batches, empty input, strict output validation, holder activation/release and matching XML contracts. Every new public type, enum and property has XML documentation. Release validation passed **6,448 offline tests**, including **128 new Design cases**, with zero warnings/errors. Documentation verification compiled and executed **70 guarded interface/adapter XML examples**, including all twenty Design examples, with no native operations invoked.

No native design command, solver, saved model mutation or live holder activation was exercised. Managed implementation and tests do not certify engineering behavior or live compatibility on every build. These source additions and the adopted branding are included in preview 1.0.0-preview.20261027.1.

Next qualify extended GROUP, then implement the Chinese steel common/legacy/later policies and optional 2025+ capabilities in the [architecture plan](reports/OpenSTAAD-Design-Multi-Version-Architecture-and-Plan.md). Existing Support/Commands/Output gates and parked Load/Property approval checkpoints remain separate. See [all contract evidence](reports/OpenSTAAD-Design-API-Coverage-and-Contract-Inventory.md) and [the exact method index](contracts/DesignFoundationContractIndex.json).
