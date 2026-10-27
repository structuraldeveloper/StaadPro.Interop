using System;
using System.Collections.Generic;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Design;
using StaadPro.Interop.Models.Design;
namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>Provides common design-brief, assignment and parameter-query operations.</summary>
    /// <remarks>Steel result queries remain in Output. Extended GROUP and Chinese steel capabilities remain separate pending work.</remarks>
    public interface IOSDesign : IOSBase
    {
        /// <summary>Gets the captured native Design object; direct calls follow native lifetime and apartment requirements.</summary>
        dynamic ComObject { get; }
        /// <summary>Creates an empty design parameter brief for a source-compatible design code.</summary>
        /// <param name="designCode">Defined nonzero main-source design code. Licensing/patch availability is not inferred from the enum.</param>
        /// <returns>A named receipt retaining actual native evidence and requested/created identities. A batch also includes explicit unattempted items and any creation receipt.</returns>
        /// <remarks><para>When to use: Creates only the brief; it does not assign parameters, append a design command, run design or save. Unlike assignment methods, the returned nonnegative integer is the brief reference, including zero if returned; -1 is native failure.</para><para>Uses the parent main return contracts; the separate API-usability experiment is not merged. Runs synchronously on the caller's established COM apartment, with a per-adapter gate and binding checks between calls and after completion. No automatic analysis/design, save, retry, rollback, unit conversion or new STAAD root acquisition occurs. External edits/raw calls are not locked out. Reacquire the adapter after wrapper refresh. A native mutation can precede an exception. Common contracts share one internal service; only the documented GROUP grammar boundary is gated by native StaadVersion.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, malformed, outside its documented range or exceeds a configured bound; no native mutation is invoked for preflight errors.</exception>
        /// <exception cref="InvalidOperationException">Binding changed/disposed or native output/type/status is undocumented; holder activation requires an STA caller. A post-call error may follow native changes.</exception>
        /// <exception cref="NotSupportedException">The requested GROUP grammar is unresolved for the acquired native version.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native/activation error propagates; an earlier mutation may have completed. No retry occurs.</exception>
        /// <exception cref="MissingMemberException">A required indexed native holder property is unavailable.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">A compatible native operation or holder member is unavailable.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = design.CreateDesignBrief(StaadDesignCode.ANSIAISC360Year16);
        ///     System.Console.WriteLine(result.IsSuccessful);
        /// }</code></example>
        DesignBriefResult CreateDesignBrief(StaadDesignCode designCode);

        /// <summary>Assigns one typed design command to the requested member selection.</summary>
        /// <param name="designBriefId">Nonnegative native design-brief reference. Zero is forwarded because the creation contract reserves only -1 for failure; it is not a member ID or design-code number.</param>
        /// <param name="designCommand">Non-null immutable typed command request with owned positive member IDs and stable Order.</param>
        /// <returns>A named receipt retaining actual native evidence and requested/created identities. A batch also includes explicit unattempted items and any creation receipt.</returns>
        /// <remarks><para>When to use: Appends CHECK CODE, SELECT or another typed command; it does not assign a design parameter or execute design. Native 0 acknowledges success and -1 reports failure. Non-GROUP commands pass an empty value. Single-property GROUP passes 0..3 only on acquired STAAD.Pro 2025/2026; 2024/unknown/future GROUP profiles are rejected before mutation. Extended GROUP/SAME AS remains unavailable.</para><para>Uses the parent main return contracts; the separate API-usability experiment is not merged. Runs synchronously on the caller's established COM apartment, with a per-adapter gate and binding checks between calls and after completion. No automatic analysis/design, save, retry, rollback, unit conversion or new STAAD root acquisition occurs. External edits/raw calls are not locked out. Reacquire the adapter after wrapper refresh. A native mutation can precede an exception. Common contracts share one internal service; only the documented GROUP grammar boundary is gated by native StaadVersion.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, malformed, outside its documented range or exceeds a configured bound; no native mutation is invoked for preflight errors.</exception>
        /// <exception cref="InvalidOperationException">Binding changed/disposed or native output/type/status is undocumented; holder activation requires an STA caller. A post-call error may follow native changes.</exception>
        /// <exception cref="NotSupportedException">The requested GROUP grammar is unresolved for the acquired native version.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native/activation error propagates; an earlier mutation may have completed. No retry occurs.</exception>
        /// <exception cref="MissingMemberException">A required indexed native holder property is unavailable.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">A compatible native operation or holder member is unavailable.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = design.AssignDesignCommand(1, new DesignCommandAssignment(DesignCommandKind.CheckCode, new[] { 17, 41 }));
        ///     System.Console.WriteLine(result.IsSuccessful);
        /// }</code></example>
        DesignMutationResult AssignDesignCommand(int designBriefId, DesignCommandAssignment designCommand);

        /// <summary>Assigns a validated, stably ordered batch of design commands to an existing brief.</summary>
        /// <param name="designBriefId">Nonnegative native design-brief reference. Zero is forwarded because the creation contract reserves only -1 for failure; it is not a member ID or design-code number.</param>
        /// <param name="designCommands">Non-null bounded sequence of non-null immutable command requests, materialized once and stably sorted by Order.</param>
        /// <param name="maximumResults">Bound from 1 through 100,000 on supplied batch items, not the largest native ID.</param>
        /// <returns>A named receipt retaining actual native evidence and requested/created identities. A batch also includes explicit unattempted items and any creation receipt.</returns>
        /// <remarks><para>When to use: Use this for several commands on an existing brief. Materializes once and validates every request/profile before mutation, then sorts by Order with stable input ties. Stops after the first native -1; returns all items with original InputIndex and explicit NotAttempted outcomes. It is not a transaction. An empty selection checks binding but makes no native calls and returns IsNoOperation.</para><para>Uses the parent main return contracts; the separate API-usability experiment is not merged. Runs synchronously on the caller's established COM apartment, with a per-adapter gate and binding checks between calls and after completion. No automatic analysis/design, save, retry, rollback, unit conversion or new STAAD root acquisition occurs. External edits/raw calls are not locked out. Reacquire the adapter after wrapper refresh. A native mutation can precede an exception. Common contracts share one internal service; only the documented GROUP grammar boundary is gated by native StaadVersion.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, malformed, outside its documented range or exceeds a configured bound; no native mutation is invoked for preflight errors.</exception>
        /// <exception cref="InvalidOperationException">Binding changed/disposed or native output/type/status is undocumented; holder activation requires an STA caller. A post-call error may follow native changes.</exception>
        /// <exception cref="NotSupportedException">The requested GROUP grammar is unresolved for the acquired native version.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native/activation error propagates; an earlier mutation may have completed. No retry occurs.</exception>
        /// <exception cref="MissingMemberException">A required indexed native holder property is unavailable.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">A compatible native operation or holder member is unavailable.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = design.AssignDesignCommands(1, new[] { new DesignCommandAssignment(DesignCommandKind.CheckCode, new[] { 17 }) });
        ///     System.Console.WriteLine(result.IsSuccessful);
        /// }</code></example>
        DesignBatchResult AssignDesignCommands(int designBriefId, IEnumerable<DesignCommandAssignment> designCommands, int maximumResults = 100000);

        /// <summary>Assigns one named parameter value to the selected analytical members.</summary>
        /// <param name="designBriefId">Nonnegative native design-brief reference. Zero is forwarded because the creation contract reserves only -1 for failure; it is not a member ID or design-code number.</param>
        /// <param name="designParameter">Non-null immutable parameter request with validated keyword, invariant native text, member IDs and Order.</param>
        /// <returns>A named receipt retaining actual native evidence and requested/created identities. A batch also includes explicit unattempted items and any creation receipt.</returns>
        /// <remarks><para>When to use: Assigns a parameter such as FYLD or LY, rather than a CHECK CODE/SELECT instruction. Uses the immutable request's invariant NativeValue and owned Int32 member array without reflection or unit conversion. Code-specific meanings, units, ranges and availability remain native responsibilities. Native 0 acknowledges success; -1 reports failure.</para><para>Uses the parent main return contracts; the separate API-usability experiment is not merged. Runs synchronously on the caller's established COM apartment, with a per-adapter gate and binding checks between calls and after completion. No automatic analysis/design, save, retry, rollback, unit conversion or new STAAD root acquisition occurs. External edits/raw calls are not locked out. Reacquire the adapter after wrapper refresh. A native mutation can precede an exception. Common contracts share one internal service; only the documented GROUP grammar boundary is gated by native StaadVersion.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, malformed, outside its documented range or exceeds a configured bound; no native mutation is invoked for preflight errors.</exception>
        /// <exception cref="InvalidOperationException">Binding changed/disposed or native output/type/status is undocumented; holder activation requires an STA caller. A post-call error may follow native changes.</exception>
        /// <exception cref="NotSupportedException">The requested GROUP grammar is unresolved for the acquired native version.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native/activation error propagates; an earlier mutation may have completed. No retry occurs.</exception>
        /// <exception cref="MissingMemberException">A required indexed native holder property is unavailable.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">A compatible native operation or holder member is unavailable.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = design.AssignDesignParameter(1, new DesignParameterAssignment("FYLD", 250.0, new[] { 17, 41 }));
        ///     System.Console.WriteLine(result.IsSuccessful);
        /// }</code></example>
        DesignMutationResult AssignDesignParameter(int designBriefId, DesignParameterAssignment designParameter);

        /// <summary>Assigns a validated, stably ordered parameter batch to an existing brief.</summary>
        /// <param name="designBriefId">Nonnegative native design-brief reference. Zero is forwarded because the creation contract reserves only -1 for failure; it is not a member ID or design-code number.</param>
        /// <param name="designParameters">Non-null bounded sequence of non-null immutable parameter requests, validated before creating or changing any brief.</param>
        /// <param name="maximumResults">Bound from 1 through 100,000 on supplied batch items, not the largest native ID.</param>
        /// <returns>A named receipt retaining actual native evidence and requested/created identities. A batch also includes explicit unattempted items and any creation receipt.</returns>
        /// <remarks><para>When to use: Reuses the supplied brief instead of creating another. Materializes once, orders by Order with stable ties, stops on the first native -1 and retains both failed and unattempted items. Validates the complete input before the first mutation. Empty input makes no native calls. No rollback, automatic command creation or design execution occurs.</para><para>Uses the parent main return contracts; the separate API-usability experiment is not merged. Runs synchronously on the caller's established COM apartment, with a per-adapter gate and binding checks between calls and after completion. No automatic analysis/design, save, retry, rollback, unit conversion or new STAAD root acquisition occurs. External edits/raw calls are not locked out. Reacquire the adapter after wrapper refresh. A native mutation can precede an exception. Common contracts share one internal service; only the documented GROUP grammar boundary is gated by native StaadVersion.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, malformed, outside its documented range or exceeds a configured bound; no native mutation is invoked for preflight errors.</exception>
        /// <exception cref="InvalidOperationException">Binding changed/disposed or native output/type/status is undocumented; holder activation requires an STA caller. A post-call error may follow native changes.</exception>
        /// <exception cref="NotSupportedException">The requested GROUP grammar is unresolved for the acquired native version.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native/activation error propagates; an earlier mutation may have completed. No retry occurs.</exception>
        /// <exception cref="MissingMemberException">A required indexed native holder property is unavailable.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">A compatible native operation or holder member is unavailable.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = design.AssignDesignParameters(1, new[] { new DesignParameterAssignment("FYLD", 250.0, new[] { 17 }) });
        ///     System.Console.WriteLine(result.IsSuccessful);
        /// }</code></example>
        DesignBatchResult AssignDesignParameters(int designBriefId, IEnumerable<DesignParameterAssignment> designParameters, int maximumResults = 100000);

        /// <summary>Creates a design brief and assigns a validated ordered parameter batch.</summary>
        /// <param name="designCode">Defined nonzero main-source design code. Licensing/patch availability is not inferred from the enum.</param>
        /// <param name="designParameters">Non-null bounded sequence of non-null immutable parameter requests, validated before creating or changing any brief.</param>
        /// <param name="maximumResults">Bound from 1 through 100,000 on supplied batch items, not the largest native ID.</param>
        /// <returns>A named receipt retaining actual native evidence and requested/created identities. A batch also includes explicit unattempted items and any creation receipt.</returns>
        /// <remarks><para>When to use: Unlike the brief-ID overload, creates a new brief after validating all requests. Creation -1 prevents every assignment. Later assignment failure preserves Creation and the created DesignBriefId, including zero if returned, with explicit unattempted items. An empty selection creates no brief. This operation does not append CHECK CODE or run design, and never deletes a partially populated brief.</para><para>Uses the parent main return contracts; the separate API-usability experiment is not merged. Runs synchronously on the caller's established COM apartment, with a per-adapter gate and binding checks between calls and after completion. No automatic analysis/design, save, retry, rollback, unit conversion or new STAAD root acquisition occurs. External edits/raw calls are not locked out. Reacquire the adapter after wrapper refresh. A native mutation can precede an exception. Common contracts share one internal service; only the documented GROUP grammar boundary is gated by native StaadVersion.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, malformed, outside its documented range or exceeds a configured bound; no native mutation is invoked for preflight errors.</exception>
        /// <exception cref="InvalidOperationException">Binding changed/disposed or native output/type/status is undocumented; holder activation requires an STA caller. A post-call error may follow native changes.</exception>
        /// <exception cref="NotSupportedException">The requested GROUP grammar is unresolved for the acquired native version.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native/activation error propagates; an earlier mutation may have completed. No retry occurs.</exception>
        /// <exception cref="MissingMemberException">A required indexed native holder property is unavailable.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">A compatible native operation or holder member is unavailable.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = design.AssignDesignParameters(StaadDesignCode.ANSIAISC360Year16, new[] { new DesignParameterAssignment("FYLD", 250.0, new[] { 17 }) });
        ///     System.Console.WriteLine(result.IsSuccessful);
        /// }</code></example>
        DesignBatchResult AssignDesignParameters(StaadDesignCode designCode, IEnumerable<DesignParameterAssignment> designParameters, int maximumResults = 100000);

        /// <summary>Copies one member's native design parameter rows into an owned managed snapshot.</summary>
        /// <param name="designBriefId">Nonnegative native design-brief reference. Zero is forwarded because the creation contract reserves only -1 for failure; it is not a member ID or design-code number.</param>
        /// <param name="memberId">Positive analytical member ID, not a zero-based holder index.</param>
        /// <param name="maximumParameters">Per-member native holder row limit from 1 through 100,000; a malformed, negative, changing or oversized count is rejected.</param>
        /// <returns>Identified status and owned data after success; inspect IsSuccessful before Data. Ordinary native absence/failure is retained. Batch queries retain all ordinary outcomes.</returns>
        /// <remarks><para>When to use: Retrieves assigned/default parameter metadata, not steel utilization or design forces. Native 0 is success and -1 is failure; failure has no Data. A successful acknowledged zero-count holder returns an empty Parameters list. Names, textual values, unit labels, descriptions and default flags are copied in zero-based native row order before the private holder is released. No parsing or unit conversion occurs.</para><para>Uses the parent main return contracts; the separate API-usability experiment is not merged. Runs synchronously on the caller's established COM apartment, with a per-adapter gate and binding checks between calls and after completion. No automatic analysis/design, save, retry, rollback, unit conversion or new STAAD root acquisition occurs. External edits/raw calls are not locked out. Reacquire the adapter after wrapper refresh. A native mutation can precede an exception. Common contracts share one internal service; only the documented GROUP grammar boundary is gated by native StaadVersion.</para><para>Requires registered Bentley OSMemberSteelDgnParams of compatible process bitness and an STA caller for private holder activation. The exclusively created temporary holder is released on success, failure and extraction exceptions. Shared Design/root objects are never released. Managed fixtures and metadata do not establish live compatibility on every installed build.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, malformed, outside its documented range or exceeds a configured bound; no native mutation is invoked for preflight errors.</exception>
        /// <exception cref="InvalidOperationException">Binding changed/disposed or native output/type/status is undocumented; holder activation requires an STA caller. A post-call error may follow native changes.</exception>
        /// <exception cref="NotSupportedException">The requested GROUP grammar is unresolved for the acquired native version.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native/activation error propagates; an earlier mutation may have completed. No retry occurs.</exception>
        /// <exception cref="MissingMemberException">A required indexed native holder property is unavailable.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">A compatible native operation or holder member is unavailable.</exception>
        /// <example><code>bool readExample = false;
        /// if (readExample)
        /// {
        ///     var result = design.GetMemberDesignParameters(1, 17);
        ///     System.Console.WriteLine(result.IsSuccessful);
        /// }</code></example>
        DesignQueryResult<MemberDesignParametersResult> GetMemberDesignParameters(int designBriefId, int memberId, int maximumParameters = 100000);

        /// <summary>Observes the native design code of an existing design brief.</summary>
        /// <param name="designBriefId">Nonnegative native design-brief reference. Zero is forwarded because the creation contract reserves only -1 for failure; it is not a member ID or design-code number.</param>
        /// <returns>Identified status and owned data after success; inspect IsSuccessful before Data. Ordinary native absence/failure is retained. Batch queries retain all ordinary outcomes.</returns>
        /// <remarks><para>When to use: Looks up a brief; it does not create one. Native zero means NotFound, unlike zero-success assignments and a valid zero reference from creation. Positive codes are preserved in NativeCode; KnownCode is null for unlisted codes. Negative or malformed code returns throw instead of being treated as absence.</para><para>Uses the parent main return contracts; the separate API-usability experiment is not merged. Runs synchronously on the caller's established COM apartment, with a per-adapter gate and binding checks between calls and after completion. No automatic analysis/design, save, retry, rollback, unit conversion or new STAAD root acquisition occurs. External edits/raw calls are not locked out. Reacquire the adapter after wrapper refresh. A native mutation can precede an exception. Common contracts share one internal service; only the documented GROUP grammar boundary is gated by native StaadVersion.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, malformed, outside its documented range or exceeds a configured bound; no native mutation is invoked for preflight errors.</exception>
        /// <exception cref="InvalidOperationException">Binding changed/disposed or native output/type/status is undocumented; holder activation requires an STA caller. A post-call error may follow native changes.</exception>
        /// <exception cref="NotSupportedException">The requested GROUP grammar is unresolved for the acquired native version.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native/activation error propagates; an earlier mutation may have completed. No retry occurs.</exception>
        /// <exception cref="MissingMemberException">A required indexed native holder property is unavailable.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">A compatible native operation or holder member is unavailable.</exception>
        /// <example><code>bool readExample = false;
        /// if (readExample)
        /// {
        ///     var result = design.GetDesignBriefCode(1);
        ///     System.Console.WriteLine(result.IsSuccessful);
        /// }</code></example>
        DesignQueryResult<DesignBriefCodeObservation> GetDesignBriefCode(int designBriefId);

        /// <summary>Eagerly observes an ordered bounded selection of design-brief codes.</summary>
        /// <param name="designBriefIds">Non-null bounded selection of nonnegative brief references; order, duplicates and zero references are retained.</param>
        /// <param name="maximumResults">Bound from 1 through 100,000 on supplied batch items, not the largest native ID.</param>
        /// <returns>Identified status and owned data after success; inspect IsSuccessful before Data. Ordinary native absence/failure is retained. Batch queries retain all ordinary outcomes.</returns>
        /// <remarks><para>When to use: Calls the same single-brief native query once per requested reference, retaining order, duplicates, brief identity and individual NotFound results. Enumerates input once and validates every ID before native calls. Empty input returns an owned empty list after binding validation. Transport/malformed-response errors abort rather than silently dropping items; this is not an atomic model snapshot.</para><para>Uses the parent main return contracts; the separate API-usability experiment is not merged. Runs synchronously on the caller's established COM apartment, with a per-adapter gate and binding checks between calls and after completion. No automatic analysis/design, save, retry, rollback, unit conversion or new STAAD root acquisition occurs. External edits/raw calls are not locked out. Reacquire the adapter after wrapper refresh. A native mutation can precede an exception. Common contracts share one internal service; only the documented GROUP grammar boundary is gated by native StaadVersion.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, malformed, outside its documented range or exceeds a configured bound; no native mutation is invoked for preflight errors.</exception>
        /// <exception cref="InvalidOperationException">Binding changed/disposed or native output/type/status is undocumented; holder activation requires an STA caller. A post-call error may follow native changes.</exception>
        /// <exception cref="NotSupportedException">The requested GROUP grammar is unresolved for the acquired native version.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native/activation error propagates; an earlier mutation may have completed. No retry occurs.</exception>
        /// <exception cref="MissingMemberException">A required indexed native holder property is unavailable.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">A compatible native operation or holder member is unavailable.</exception>
        /// <example><code>bool readExample = false;
        /// if (readExample)
        /// {
        ///     var result = design.GetDesignBriefCodes(new[] { 1, 3, 1 });
        ///     System.Console.WriteLine(result.Count);
        /// }</code></example>
        IReadOnlyList<DesignQueryResult<DesignBriefCodeObservation>> GetDesignBriefCodes(IEnumerable<int> designBriefIds, int maximumResults = 100000);

        /// <summary>Eagerly copies parameter snapshots for an ordered bounded member selection.</summary>
        /// <param name="designBriefId">Nonnegative native design-brief reference. Zero is forwarded because the creation contract reserves only -1 for failure; it is not a member ID or design-code number.</param>
        /// <param name="memberIds">Non-null bounded member-ID selection; positive IDs, input order and duplicates are retained.</param>
        /// <param name="maximumResults">Bound from 1 through 100,000 on supplied batch items, not the largest native ID.</param>
        /// <param name="maximumParameters">Per-member native holder row limit from 1 through 100,000; a malformed, negative, changing or oversized count is rejected.</param>
        /// <returns>Identified status and owned data after success; inspect IsSuccessful before Data. Ordinary native absence/failure is retained. Batch queries retain all ordinary outcomes.</returns>
        /// <remarks><para>When to use: Shares one brief while obtaining a separate owned native-holder snapshot per member. Retains duplicates, order and identified native failures, continuing after ordinary -1 query failures. Enumerates and validates all member IDs before querying. Empty input creates no holders. Transport, malformed-output, binding or holder errors abort the batch; completed earlier reads are not an atomic model snapshot.</para><para>Uses the parent main return contracts; the separate API-usability experiment is not merged. Runs synchronously on the caller's established COM apartment, with a per-adapter gate and binding checks between calls and after completion. No automatic analysis/design, save, retry, rollback, unit conversion or new STAAD root acquisition occurs. External edits/raw calls are not locked out. Reacquire the adapter after wrapper refresh. A native mutation can precede an exception. Common contracts share one internal service; only the documented GROUP grammar boundary is gated by native StaadVersion.</para><para>Requires registered Bentley OSMemberSteelDgnParams of compatible process bitness and an STA caller for private holder activation. The exclusively created temporary holder is released on success, failure and extraction exceptions. Shared Design/root objects are never released. Managed fixtures and metadata do not establish live compatibility on every installed build.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, malformed, outside its documented range or exceeds a configured bound; no native mutation is invoked for preflight errors.</exception>
        /// <exception cref="InvalidOperationException">Binding changed/disposed or native output/type/status is undocumented; holder activation requires an STA caller. A post-call error may follow native changes.</exception>
        /// <exception cref="NotSupportedException">The requested GROUP grammar is unresolved for the acquired native version.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native/activation error propagates; an earlier mutation may have completed. No retry occurs.</exception>
        /// <exception cref="MissingMemberException">A required indexed native holder property is unavailable.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">A compatible native operation or holder member is unavailable.</exception>
        /// <example><code>bool readExample = false;
        /// if (readExample)
        /// {
        ///     var result = design.GetMemberDesignParameters(1, new[] { 17, 41, 17 });
        ///     System.Console.WriteLine(result.Count);
        /// }</code></example>
        IReadOnlyList<DesignQueryResult<MemberDesignParametersResult>> GetMemberDesignParameters(int designBriefId, IEnumerable<int> memberIds, int maximumResults = 100000, int maximumParameters = 100000);
    }
}
