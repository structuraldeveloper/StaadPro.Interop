using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Support;
using System.Collections.Generic;
using StaadPro.Interop.Models.Support;
using StaadPro.Interop.Services.Support;
using System;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Adapters.Models
{
    /// <summary>Provides typed Support operations through the captured native binding.</summary>
    /// <remarks>Common reviewed 2024–2026 operations use an internal shared service; no partial adapter or artificial per-year duplicate is used.</remarks>
    public sealed class OSSupportAdapter : OSBaseAdapter, IOSSupport
    {
        private readonly SupportService service;
        /// <summary>Captures the supplied wrapper’s Support interface without calling its native functions.</summary>
        /// <param name="wrapper">The connected wrapper whose RawSupport must be non-null.</param>
        /// <exception cref="ArgumentNullException">The wrapper or its RawSupport binding is null.</exception>
        public OSSupportAdapter(OpenStaadWrapper wrapper) : base(wrapper)
        {
            ComObject = wrapper.RawSupport ?? throw new ArgumentNullException(nameof(wrapper.RawSupport));
            service = new SupportService(wrapper, (object)ComObject);
        }

        /// <summary>Gets the captured native Support automation object.</summary>
        /// <value>The same object that RawSupport exposed at construction; refresh the wrapper after reopening a model.</value>
        public dynamic ComObject { get; }
        #region Support discovery, foundation observations and explicit mutations

        /// <summary>Counts analytical nodes with assigned supports.</summary>
        /// <returns>A qualified status/data result with owned payload and requested identity. Inspect IsSuccessful before Data; unsuccessful access throws. Counts, native type codes and Boolean flags are decoded independently.</returns>
        /// <remarks><para>When to use: This is the supported-node count used by GetSupportedNodeIds, not the number of distinct support definitions or any contiguous ID range. Zero is valid data. Exact nonnegative integral returns are required; Boolean success is never treated as count one. No node geometry is hydrated.</para><para>Reviewed declarations and non-example contracts match across STAAD.Pro 2024–2026; a shared internal service supplies these common routes. Calls run synchronously on the caller’s established COM apartment under the Support gate. Captured binding identity is checked before and after work; reacquire wrapper.Support after refresh. Other adapters, raw access and external edits are not excluded by this gate. Native units and signs are retained without conversion. No automatic analysis, save, rollback or retry occurs; partial native effects can precede a mutation failure or post-call lifetime exception. Managed tests do not certify live marshaling, result freshness or engineering behavior.</para></remarks>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, or a native response has an undocumented code/type, malformed text, invalid shape, unwritten/nonfinite fields or invalid identifiers.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; original exception identity is preserved and no retry occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>var result = support.GetSupportedNodeCount();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);</code></example>
        public SupportQueryResult<int> GetSupportedNodeCount() =>
            service.GetSupportedNodeCount();

        /// <summary>Reads an owned bounded snapshot of supported analytical node IDs.</summary>
        /// <param name="maximumResults">Managed selection/allocation limit from 1 through 100000. Counts beyond the read limit produce LimitExceeded before allocation; oversized input selections are rejected before native invocation.</param>
        /// <returns>A qualified status/data result with owned payload and requested identity. Inspect IsSuccessful before Data; unsuccessful access throws. Counts, native type codes and Boolean flags are decoded independently.</returns>
        /// <remarks><para>When to use: Use for node discovery instead of enumerating support-definition IDs from a count. The native return is a count, not a Boolean flag; -1 is failure. Count/list/count must agree; a changed count returns StateChanged with no payload and no retry. Zero is rechecked and returns an owned empty list without native list invocation. Initialized Int32 arrays must be completely written; native order and duplicates remain unchanged. Equal counts cannot prove an atomic model snapshot.</para><para>Reviewed declarations and non-example contracts match across STAAD.Pro 2024–2026; a shared internal service supplies these common routes. Calls run synchronously on the caller’s established COM apartment under the Support gate. Captured binding identity is checked before and after work; reacquire wrapper.Support after refresh. Other adapters, raw access and external edits are not excluded by this gate. Native units and signs are retained without conversion. No automatic analysis, save, rollback or retry occurs; partial native effects can precede a mutation failure or post-call lifetime exception. Managed tests do not certify live marshaling, result freshness or engineering behavior.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, limit, request, GUID or selection is invalid; no native mutation/retrieval is attempted for invalid input.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, or a native response has an undocumented code/type, malformed text, invalid shape, unwritten/nonfinite fields or invalid identifiers.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; original exception identity is preserved and no retry occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>var result = support.GetSupportedNodeIds(1000);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);</code></example>
        public SupportQueryResult<IReadOnlyList<int>> GetSupportedNodeIds(int maximumResults = 100000) =>
            service.GetSupportedNodeIds(maximumResults);

        /// <summary>Reads the native support type assigned to one analytical node.</summary>
        /// <param name="nodeId">Positive native analytical node ID. It is not a support-definition ID or list index.</param>
        /// <returns>A qualified status/data result with owned payload and requested identity. Inspect IsSuccessful before Data; unsuccessful access throws. Counts, native type codes and Boolean flags are decoded independently.</returns>
        /// <remarks><para>When to use: Prefer this inexpensive type-only observation when DOF states and stiffnesses are not needed. Native 0 is successful NoSupport data; -1 is NativeFailure. Unknown nonnegative codes retain NativeTypeCode and KnownType=null, without being cast to a fabricated known enum. GetSupportInformation instead requires a reviewed type and written six-DOF buffers. The metadata input is passed by value despite a mutable-reference help declaration.</para><para>Reviewed declarations and non-example contracts match across STAAD.Pro 2024–2026; a shared internal service supplies these common routes. Calls run synchronously on the caller’s established COM apartment under the Support gate. Captured binding identity is checked before and after work; reacquire wrapper.Support after refresh. Other adapters, raw access and external edits are not excluded by this gate. Native units and signs are retained without conversion. No automatic analysis, save, rollback or retry occurs; partial native effects can precede a mutation failure or post-call lifetime exception. Managed tests do not certify live marshaling, result freshness or engineering behavior.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, limit, request, GUID or selection is invalid; no native mutation/retrieval is attempted for invalid input.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, or a native response has an undocumented code/type, malformed text, invalid shape, unwritten/nonfinite fields or invalid identifiers.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; original exception identity is preserved and no retry occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>var result = support.GetSupportType(11);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.NativeTypeCode);</code></example>
        public SupportQueryResult<SupportTypeObservation> GetSupportType(int nodeId) =>
            service.GetSupportType(nodeId);

        /// <summary>Reads six native support DOF states and spring constants at one node.</summary>
        /// <param name="nodeId">Positive native analytical node ID. It is not a support-definition ID or list index.</param>
        /// <returns>A qualified status/data result with owned payload and requested identity. Inspect IsSuccessful before Data; unsuccessful access throws. Counts, native type codes and Boolean flags are decoded independently.</returns>
        /// <remarks><para>When to use: Unlike GetSupportType, this retrieves all FX/FY/FZ/MX/MY/MZ restraint observations. The native return is a support-type code, never a Boolean success flag. Type 0 yields NoSupport without consuming untouched buffers; -1 yields NativeFailure; an unknown positive type yields UnsupportedNativeType without guessing its layout. Known positive types require six written Int32 states (-1 spring, 0 fixed, 1 released) and six finite nonnegative floating-point stiffnesses. Springs are not collapsed to released=true. Native local support axes, force/length translational stiffness and moment/angle rotational stiffness are retained without conversion.</para><para>Reviewed declarations and non-example contracts match across STAAD.Pro 2024–2026; a shared internal service supplies these common routes. Calls run synchronously on the caller’s established COM apartment under the Support gate. Captured binding identity is checked before and after work; reacquire wrapper.Support after refresh. Other adapters, raw access and external edits are not excluded by this gate. Native units and signs are retained without conversion. No automatic analysis, save, rollback or retry occurs; partial native effects can precede a mutation failure or post-call lifetime exception. Managed tests do not certify live marshaling, result freshness or engineering behavior.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, limit, request, GUID or selection is invalid; no native mutation/retrieval is attempted for invalid input.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, or a native response has an undocumented code/type, malformed text, invalid shape, unwritten/nonfinite fields or invalid identifiers.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; original exception identity is preserved and no retry occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>var result = support.GetSupportInformation(11);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Fx.State);</code></example>
        public SupportQueryResult<SupportInformation> GetSupportInformation(int nodeId) =>
            service.GetSupportInformation(nodeId);

        /// <summary>Reads a support item’s GUID as parsed and exact native text.</summary>
        /// <param name="supportId">Known positive native support item/definition reference, not a node number or zero-based index. Creation returns a reference but does not assign it.</param>
        /// <returns>A qualified status/data result with owned payload and requested identity. Inspect IsSuccessful before Data; unsuccessful access throws. Counts, native type codes and Boolean flags are decoded independently.</returns>
        /// <remarks><para>When to use: This takes a support item/definition ID, not a node number, support-list index or user-facing name. Null, DBNull or empty native text gives EmptyOrUnavailable; malformed GUID text throws. Exact native text and parsed Guid are both retained. Guid.Empty, if returned, remains an observed value, not a generated replacement. SetSupportUniqueId writes a new identity and has only void invocation evidence.</para><para>Reviewed declarations and non-example contracts match across STAAD.Pro 2024–2026; a shared internal service supplies these common routes. Calls run synchronously on the caller’s established COM apartment under the Support gate. Captured binding identity is checked before and after work; reacquire wrapper.Support after refresh. Other adapters, raw access and external edits are not excluded by this gate. Native units and signs are retained without conversion. No automatic analysis, save, rollback or retry occurs; partial native effects can precede a mutation failure or post-call lifetime exception. Managed tests do not certify live marshaling, result freshness or engineering behavior.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, limit, request, GUID or selection is invalid; no native mutation/retrieval is attempted for invalid input.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, or a native response has an undocumented code/type, malformed text, invalid shape, unwritten/nonfinite fields or invalid identifiers.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; original exception identity is preserved and no retry occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>var result = support.GetSupportUniqueId(4);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Value);</code></example>
        public SupportQueryResult<SupportUniqueId> GetSupportUniqueId(int supportId) =>
            service.GetSupportUniqueId(supportId);

        /// <summary>Counts existing elastic-mat definitions.</summary>
        /// <returns>A qualified status/data result with owned payload and requested identity. Inspect IsSuccessful before Data; unsuccessful access throws. Counts, native type codes and Boolean flags are decoded independently.</returns>
        /// <remarks><para>When to use: This is a definition count, not supported-node count, assignment count or maximum support ID. Use GetElasticMat with a known positive definition ID; there is no inferred 1..count discovery. Zero is valid integral data. Plate-mat and footing definition counts are separate queries.</para><para>Reviewed declarations and non-example contracts match across STAAD.Pro 2024–2026; a shared internal service supplies these common routes. Calls run synchronously on the caller’s established COM apartment under the Support gate. Captured binding identity is checked before and after work; reacquire wrapper.Support after refresh. Other adapters, raw access and external edits are not excluded by this gate. Native units and signs are retained without conversion. No automatic analysis, save, rollback or retry occurs; partial native effects can precede a mutation failure or post-call lifetime exception. Managed tests do not certify live marshaling, result freshness or engineering behavior.</para></remarks>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, or a native response has an undocumented code/type, malformed text, invalid shape, unwritten/nonfinite fields or invalid identifiers.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; original exception identity is preserved and no retry occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>var result = support.GetElasticMatDefinitionCount();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);</code></example>
        public SupportQueryResult<int> GetElasticMatDefinitionCount() =>
            service.GetElasticMatDefinitionCount();

        /// <summary>Reads one elastic-mat definition’s native parameters and assigned-node count.</summary>
        /// <param name="supportId">Known positive native support item/definition reference, not a node number or zero-based index. Creation returns a reference but does not assign it.</param>
        /// <returns>A qualified status/data result with owned payload and requested identity. Inspect IsSuccessful before Data; unsuccessful access throws. Counts, native type codes and Boolean flags are decoded independently.</returns>
        /// <remarks><para>When to use: Use a known support definition ID, not a node ID or zero-based index. Native true qualifies direction 0..5, scalar subgrade, print flag, spring-generation type and nonnegative assignment count; false exposes no default packet. This query does not read assigned nodes; GetElasticMatAssignedNodeIds performs a bounded detail/list/detail observation. Native subgrade is force/length cubed, not force/length nodal stiffness. Only direction variants and compression/multilinear generation remain distinct. No model or geometry objects are hydrated.</para><para>Reviewed declarations and non-example contracts match across STAAD.Pro 2024–2026; a shared internal service supplies these common routes. Calls run synchronously on the caller’s established COM apartment under the Support gate. Captured binding identity is checked before and after work; reacquire wrapper.Support after refresh. Other adapters, raw access and external edits are not excluded by this gate. Native units and signs are retained without conversion. No automatic analysis, save, rollback or retry occurs; partial native effects can precede a mutation failure or post-call lifetime exception. Managed tests do not certify live marshaling, result freshness or engineering behavior.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, limit, request, GUID or selection is invalid; no native mutation/retrieval is attempted for invalid input.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, or a native response has an undocumented code/type, malformed text, invalid shape, unwritten/nonfinite fields or invalid identifiers.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; original exception identity is preserved and no retry occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>var result = support.GetElasticMat(4);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.SubgradeModulus);</code></example>
        public SupportQueryResult<ElasticMatObservation> GetElasticMat(int supportId) =>
            service.GetElasticMat(supportId);

        /// <summary>Reads owned node assignments for one explicit elastic-mat definition.</summary>
        /// <param name="supportId">Known positive native support item/definition reference, not a node number or zero-based index. Creation returns a reference but does not assign it.</param>
        /// <param name="maximumResults">Managed selection/allocation limit from 1 through 100000. Counts beyond the read limit produce LimitExceeded before allocation; oversized input selections are rejected before native invocation.</param>
        /// <returns>A qualified status/data result with owned payload and requested identity. Inspect IsSuccessful before Data; unsuccessful access throws. Counts, native type codes and Boolean flags are decoded independently.</returns>
        /// <remarks><para>When to use: The returned IDs are analytical nodes, never plate IDs or support-definition IDs. Allocation uses qualified GetElasticMat detail, then Boolean list retrieval and a complete detail recheck. Exceeding maximumResults gives LimitExceeded before list allocation; zero assignments return an owned empty list after recheck. Changed count/direction/subgrade/flags gives StateChanged without payload or retry. Native order/duplicates are preserved; equal metadata does not detect every same-count edit.</para><para>Reviewed declarations and non-example contracts match across STAAD.Pro 2024–2026; a shared internal service supplies these common routes. Calls run synchronously on the caller’s established COM apartment under the Support gate. Captured binding identity is checked before and after work; reacquire wrapper.Support after refresh. Other adapters, raw access and external edits are not excluded by this gate. Native units and signs are retained without conversion. No automatic analysis, save, rollback or retry occurs; partial native effects can precede a mutation failure or post-call lifetime exception. Managed tests do not certify live marshaling, result freshness or engineering behavior.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, limit, request, GUID or selection is invalid; no native mutation/retrieval is attempted for invalid input.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, or a native response has an undocumented code/type, malformed text, invalid shape, unwritten/nonfinite fields or invalid identifiers.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; original exception identity is preserved and no retry occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>var result = support.GetElasticMatAssignedNodeIds(4, 1000);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);</code></example>
        public SupportQueryResult<IReadOnlyList<int>> GetElasticMatAssignedNodeIds(int supportId, int maximumResults = 100000) =>
            service.GetElasticMatAssignedNodeIds(supportId, maximumResults);

        /// <summary>Counts existing plate-mat support definitions.</summary>
        /// <returns>A qualified status/data result with owned payload and requested identity. Inspect IsSuccessful before Data; unsuccessful access throws. Counts, native type codes and Boolean flags are decoded independently.</returns>
        /// <remarks><para>When to use: This count concerns plate-mat definitions, not plates, support nodes, assigned entities or maximum support reference. Zero is valid integral data. It does not resolve plate-mat ID/index or scalar/array contract gates; no plate assignments or subgrades are synthesized from this count.</para><para>Reviewed declarations and non-example contracts match across STAAD.Pro 2024–2026; a shared internal service supplies these common routes. Calls run synchronously on the caller’s established COM apartment under the Support gate. Captured binding identity is checked before and after work; reacquire wrapper.Support after refresh. Other adapters, raw access and external edits are not excluded by this gate. Native units and signs are retained without conversion. No automatic analysis, save, rollback or retry occurs; partial native effects can precede a mutation failure or post-call lifetime exception. Managed tests do not certify live marshaling, result freshness or engineering behavior.</para></remarks>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, or a native response has an undocumented code/type, malformed text, invalid shape, unwritten/nonfinite fields or invalid identifiers.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; original exception identity is preserved and no retry occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>var result = support.GetPlateMatDefinitionCount();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);</code></example>
        public SupportQueryResult<int> GetPlateMatDefinitionCount() =>
            service.GetPlateMatDefinitionCount();

        /// <summary>Counts existing elastic-footing support definitions.</summary>
        /// <returns>A qualified status/data result with owned payload and requested identity. Inspect IsSuccessful before Data; unsuccessful access throws. Counts, native type codes and Boolean flags are decoded independently.</returns>
        /// <remarks><para>When to use: This is independent of the supported-node and elastic-mat counts. No documented footing index-to-ID enumeration is assumed; supply known support IDs to GetElasticFooting. Zero is valid, and a count is never decoded as a success flag.</para><para>Reviewed declarations and non-example contracts match across STAAD.Pro 2024–2026; a shared internal service supplies these common routes. Calls run synchronously on the caller’s established COM apartment under the Support gate. Captured binding identity is checked before and after work; reacquire wrapper.Support after refresh. Other adapters, raw access and external edits are not excluded by this gate. Native units and signs are retained without conversion. No automatic analysis, save, rollback or retry occurs; partial native effects can precede a mutation failure or post-call lifetime exception. Managed tests do not certify live marshaling, result freshness or engineering behavior.</para></remarks>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, or a native response has an undocumented code/type, malformed text, invalid shape, unwritten/nonfinite fields or invalid identifiers.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; original exception identity is preserved and no retry occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>var result = support.GetElasticFootingDefinitionCount();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);</code></example>
        public SupportQueryResult<int> GetElasticFootingDefinitionCount() =>
            service.GetElasticFootingDefinitionCount();

        /// <summary>Reads an existing elastic-footing definition’s geometry, direction, soil modulus and assignment count.</summary>
        /// <param name="supportId">Known positive native support item/definition reference, not a node number or zero-based index. Creation returns a reference but does not assign it.</param>
        /// <returns>A qualified status/data result with owned payload and requested identity. Inspect IsSuccessful before Data; unsuccessful access throws. Counts, native type codes and Boolean flags are decoded independently.</returns>
        /// <remarks><para>When to use: Unlike GetElasticMat, the native footing packet includes finite positive length and width as well as native direction 0..5, scalar subgrade and assigned-node count. Known support definition ID is required. Native 1/true succeeds, 0/false fails; integral -1 is not VARIANT_TRUE for this narrower contract. Lengths remain native length units and subgrade remains force/length cubed; no area, spring stiffness or reaction is derived.</para><para>Reviewed declarations and non-example contracts match across STAAD.Pro 2024–2026; a shared internal service supplies these common routes. Calls run synchronously on the caller’s established COM apartment under the Support gate. Captured binding identity is checked before and after work; reacquire wrapper.Support after refresh. Other adapters, raw access and external edits are not excluded by this gate. Native units and signs are retained without conversion. No automatic analysis, save, rollback or retry occurs; partial native effects can precede a mutation failure or post-call lifetime exception. Managed tests do not certify live marshaling, result freshness or engineering behavior.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, limit, request, GUID or selection is invalid; no native mutation/retrieval is attempted for invalid input.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, or a native response has an undocumented code/type, malformed text, invalid shape, unwritten/nonfinite fields or invalid identifiers.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; original exception identity is preserved and no retry occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>var result = support.GetElasticFooting(4);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Length);</code></example>
        public SupportQueryResult<ElasticFootingObservation> GetElasticFooting(int supportId) =>
            service.GetElasticFooting(supportId);

        /// <summary>Reads owned node assignments of one explicit elastic-footing definition.</summary>
        /// <param name="supportId">Known positive native support item/definition reference, not a node number or zero-based index. Creation returns a reference but does not assign it.</param>
        /// <param name="maximumResults">Managed selection/allocation limit from 1 through 100000. Counts beyond the read limit produce LimitExceeded before allocation; oversized input selections are rejected before native invocation.</param>
        /// <returns>A qualified status/data result with owned payload and requested identity. Inspect IsSuccessful before Data; unsuccessful access throws. Counts, native type codes and Boolean flags are decoded independently.</returns>
        /// <remarks><para>When to use: Uses qualified footing detail/list/detail, retaining node identity and every observed length/width/direction/modulus/count in its state check. Native 1/true is success; 0/false is failure and numeric -1 is rejected rather than treated as true. Empty assignment is a rechecked owned empty list. LimitExceeded occurs before list allocation. StateChanged has no payload; same-count edits can remain undetected, so coordinate session access. No node geometry is hydrated or reordered.</para><para>Reviewed declarations and non-example contracts match across STAAD.Pro 2024–2026; a shared internal service supplies these common routes. Calls run synchronously on the caller’s established COM apartment under the Support gate. Captured binding identity is checked before and after work; reacquire wrapper.Support after refresh. Other adapters, raw access and external edits are not excluded by this gate. Native units and signs are retained without conversion. No automatic analysis, save, rollback or retry occurs; partial native effects can precede a mutation failure or post-call lifetime exception. Managed tests do not certify live marshaling, result freshness or engineering behavior.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, limit, request, GUID or selection is invalid; no native mutation/retrieval is attempted for invalid input.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, or a native response has an undocumented code/type, malformed text, invalid shape, unwritten/nonfinite fields or invalid identifiers.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; original exception identity is preserved and no retry occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>var result = support.GetElasticFootingAssignedNodeIds(4, 1000);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);</code></example>
        public SupportQueryResult<IReadOnlyList<int>> GetElasticFootingAssignedNodeIds(int supportId, int maximumResults = 100000) =>
            service.GetElasticFootingAssignedNodeIds(supportId, maximumResults);

        /// <summary>Creates a native fixed-support definition without assigning it.</summary>
        /// <returns>An operation-specific acknowledgement retaining requested/created identity and actual native evidence. Only Succeeded makes IsSuccessful true; void completion and no-op do not.</returns>
        /// <remarks><para>When to use: This creates a support definition and returns its qualified positive reference; it does not attach it to nodes. AssignSupportToNodes is a separate mutation. Native -1 is failure; Boolean success or native zero is not accepted as a created reference. No mutable managed support ID is overwritten. Read back assignments separately before drawing restraint/engineering conclusions.</para><para>Reviewed declarations and non-example contracts match across STAAD.Pro 2024–2026; a shared internal service supplies these common routes. Calls run synchronously on the caller’s established COM apartment under the Support gate. Captured binding identity is checked before and after work; reacquire wrapper.Support after refresh. Other adapters, raw access and external edits are not excluded by this gate. Native units and signs are retained without conversion. No automatic analysis, save, rollback or retry occurs; partial native effects can precede a mutation failure or post-call lifetime exception. Managed tests do not certify live marshaling, result freshness or engineering behavior.</para></remarks>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, or a native response has an undocumented code/type, malformed text, invalid shape, unwritten/nonfinite fields or invalid identifiers.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; original exception identity is preserved and no retry occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>bool allowNativeMutation = false;
        /// if (allowNativeMutation)
        /// {
        ///     var result = support.CreateFixedSupport();
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        public SupportMutationResult CreateFixedSupport() =>
            service.CreateFixedSupport();

        /// <summary>Creates the native pinned-support definition without inventing its restraint packet.</summary>
        /// <returns>An operation-specific acknowledgement retaining requested/created identity and actual native evidence. Only Succeeded makes IsSuccessful true; void completion and no-op do not.</returns>
        /// <remarks><para>When to use: The native pinned operation is separate from synthesizing fixed-but release flags. Help describes local Y/Z rotations while the private source convenience releases all rotations; this method deliberately returns only the created ID and does not claim torsional restraint. Assign it separately, then use GetSupportInformation at a node to observe actual states. Native -1 is failure; success requires a positive integral reference.</para><para>Reviewed declarations and non-example contracts match across STAAD.Pro 2024–2026; a shared internal service supplies these common routes. Calls run synchronously on the caller’s established COM apartment under the Support gate. Captured binding identity is checked before and after work; reacquire wrapper.Support after refresh. Other adapters, raw access and external edits are not excluded by this gate. Native units and signs are retained without conversion. No automatic analysis, save, rollback or retry occurs; partial native effects can precede a mutation failure or post-call lifetime exception. Managed tests do not certify live marshaling, result freshness or engineering behavior.</para></remarks>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, or a native response has an undocumented code/type, malformed text, invalid shape, unwritten/nonfinite fields or invalid identifiers.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; original exception identity is preserved and no retry occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>bool allowNativeMutation = false;
        /// if (allowNativeMutation)
        /// {
        ///     var result = support.CreatePinnedSupport();
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        public SupportMutationResult CreatePinnedSupport() =>
            service.CreatePinnedSupport();

        /// <summary>Creates a native fixed-but support from an owned explicit release/stiffness definition.</summary>
        /// <param name="definition">Non-null immutable snapshot of the existing managed Releases and Springs packets, with explicit finite nonnegative stiffnesses.</param>
        /// <returns>An operation-specific acknowledgement retaining requested/created identity and actual native evidence. Only Succeeded makes IsSuccessful true; void completion and no-op do not.</returns>
        /// <remarks><para>When to use: Use when creation flags and springs must be specified explicitly rather than using the dedicated fixed/pinned calls. FixedButSupportDefinition snapshots the existing Releases/Springs packets. Native input flags are six Double 0/1 values, not the read-back Spring=-1 state or member partial-release modes. Native arrays are fresh copies; the caller’s packets are not changed. Stiffness units and release/spring interaction remain native semantics without scaling or inferred modes. Native -1 fails; a positive integral support ID is required.</para><para>Reviewed declarations and non-example contracts match across STAAD.Pro 2024–2026; a shared internal service supplies these common routes. Calls run synchronously on the caller’s established COM apartment under the Support gate. Captured binding identity is checked before and after work; reacquire wrapper.Support after refresh. Other adapters, raw access and external edits are not excluded by this gate. Native units and signs are retained without conversion. No automatic analysis, save, rollback or retry occurs; partial native effects can precede a mutation failure or post-call lifetime exception. Managed tests do not certify live marshaling, result freshness or engineering behavior.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, limit, request, GUID or selection is invalid; no native mutation/retrieval is attempted for invalid input.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, or a native response has an undocumented code/type, malformed text, invalid shape, unwritten/nonfinite fields or invalid identifiers.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; original exception identity is preserved and no retry occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>bool allowNativeMutation = false;
        /// if (allowNativeMutation)
        /// {
        ///     var definition = new FixedButSupportDefinition(new StaadPro.Interop.Entities.Releases { My = true, Mz = true }, new StaadPro.Interop.Entities.Springs());
        ///     var result = support.CreateFixedButSupport(definition);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        public SupportMutationResult CreateFixedButSupport(FixedButSupportDefinition definition) =>
            service.CreateFixedButSupport(definition);

        /// <summary>Assigns an existing support definition to an owned explicit analytical-node selection.</summary>
        /// <param name="supportId">Known positive native support item/definition reference, not a node number or zero-based index. Creation returns a reference but does not assign it.</param>
        /// <param name="nodeIds">Non-null one-pass selection of positive analytical node IDs. Order and duplicates are preserved; empty input causes no mutation.</param>
        /// <param name="maximumResults">Managed selection/allocation limit from 1 through 100000. Counts beyond the read limit produce LimitExceeded before allocation; oversized input selections are rejected before native invocation.</param>
        /// <returns>An operation-specific acknowledgement retaining requested/created identity and actual native evidence. Only Succeeded makes IsSuccessful true; void completion and no-op do not.</returns>
        /// <remarks><para>When to use: Creation and assignment are separate; this function sends one Int32 node-ID array to native AssignSupportToNode in node-array-first/support-ID-second order. The support ID must be appropriate for node assignment; this is not the plate-mat entity-list route. Input is enumerated once and bounded; order and duplicates remain unchanged. Integral 0 is success and -1 is failure; actual Boolean false is not numeric zero-success. Empty input produces NoOperation after binding checks and no native invocation. No per-node acknowledgement is invented; partial native effects on failure are possible.</para><para>Reviewed declarations and non-example contracts match across STAAD.Pro 2024–2026; a shared internal service supplies these common routes. Calls run synchronously on the caller’s established COM apartment under the Support gate. Captured binding identity is checked before and after work; reacquire wrapper.Support after refresh. Other adapters, raw access and external edits are not excluded by this gate. Native units and signs are retained without conversion. No automatic analysis, save, rollback or retry occurs; partial native effects can precede a mutation failure or post-call lifetime exception. Managed tests do not certify live marshaling, result freshness or engineering behavior.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, limit, request, GUID or selection is invalid; no native mutation/retrieval is attempted for invalid input.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, or a native response has an undocumented code/type, malformed text, invalid shape, unwritten/nonfinite fields or invalid identifiers.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; original exception identity is preserved and no retry occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>bool allowNativeMutation = false;
        /// if (allowNativeMutation)
        /// {
        ///     var result = support.AssignSupportToNodes(4, new[] { 11, 12 }, 1000);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        public SupportMutationResult AssignSupportToNodes(int supportId, IEnumerable<int> nodeIds, int maximumResults = 100000) =>
            service.AssignSupportToNodes(supportId, nodeIds, maximumResults);

        /// <summary>Removes a support assignment from one analytical node.</summary>
        /// <param name="nodeId">Positive native analytical node ID. It is not a support-definition ID or list index.</param>
        /// <returns>An operation-specific acknowledgement retaining requested/created identity and actual native evidence. Only Succeeded makes IsSuccessful true; void completion and no-op do not.</returns>
        /// <remarks><para>When to use: This unassigns one node; DeleteSupport removes a shared definition and its assignments. Native 1/true is success, 0/false is failure. Failure does not prove that no support existed. No shared definition deletion, family-specific fallback, retry, save or managed node update is performed.</para><para>Reviewed declarations and non-example contracts match across STAAD.Pro 2024–2026; a shared internal service supplies these common routes. Calls run synchronously on the caller’s established COM apartment under the Support gate. Captured binding identity is checked before and after work; reacquire wrapper.Support after refresh. Other adapters, raw access and external edits are not excluded by this gate. Native units and signs are retained without conversion. No automatic analysis, save, rollback or retry occurs; partial native effects can precede a mutation failure or post-call lifetime exception. Managed tests do not certify live marshaling, result freshness or engineering behavior.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, limit, request, GUID or selection is invalid; no native mutation/retrieval is attempted for invalid input.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, or a native response has an undocumented code/type, malformed text, invalid shape, unwritten/nonfinite fields or invalid identifiers.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; original exception identity is preserved and no retry occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>bool allowNativeMutation = false;
        /// if (allowNativeMutation)
        /// {
        ///     var result = support.RemoveSupportFromNode(11);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        public SupportMutationResult RemoveSupportFromNode(int nodeId) =>
            service.RemoveSupportFromNode(nodeId);

        /// <summary>Deletes a support definition and its native assignments.</summary>
        /// <param name="supportId">Known positive native support item/definition reference, not a node number or zero-based index. Creation returns a reference but does not assign it.</param>
        /// <returns>An operation-specific acknowledgement retaining requested/created identity and actual native evidence. Only Succeeded makes IsSuccessful true; void completion and no-op do not.</returns>
        /// <remarks><para>When to use: This is broader than RemoveSupportFromNode, which targets only one assignment. The documented operation removes the support item and its assignments; the request retains the support ID without pretending to know every affected node. Integral 0 is success and -1 is failure; Boolean false is not decoded as numeric zero. Native partial changes may remain on failure and cannot be rolled back by this result.</para><para>Reviewed declarations and non-example contracts match across STAAD.Pro 2024–2026; a shared internal service supplies these common routes. Calls run synchronously on the caller’s established COM apartment under the Support gate. Captured binding identity is checked before and after work; reacquire wrapper.Support after refresh. Other adapters, raw access and external edits are not excluded by this gate. Native units and signs are retained without conversion. No automatic analysis, save, rollback or retry occurs; partial native effects can precede a mutation failure or post-call lifetime exception. Managed tests do not certify live marshaling, result freshness or engineering behavior.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, limit, request, GUID or selection is invalid; no native mutation/retrieval is attempted for invalid input.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, or a native response has an undocumented code/type, malformed text, invalid shape, unwritten/nonfinite fields or invalid identifiers.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; original exception identity is preserved and no retry occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>bool allowNativeMutation = false;
        /// if (allowNativeMutation)
        /// {
        ///     var result = support.DeleteSupport(4);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        public SupportMutationResult DeleteSupport(int supportId) =>
            service.DeleteSupport(supportId);

        /// <summary>Invokes the native void setter for one support item’s GUID.</summary>
        /// <param name="supportId">Known positive native support item/definition reference, not a node number or zero-based index. Creation returns a reference but does not assign it.</param>
        /// <param name="uniqueId">Explicit nonempty GUID to send as canonical D-format text. No GUID is generated implicitly.</param>
        /// <returns>An operation-specific acknowledgement retaining requested/created identity and actual native evidence. Only Succeeded makes IsSuccessful true; void completion and no-op do not.</returns>
        /// <remarks><para>When to use: This writes a nonempty explicitly supplied Guid to a support definition/item, not a node. GetSupportUniqueId reads its actual current value. The native setter returns void, so completed dispatch produces InvokedWithoutAcknowledgement and IsSuccessful=false, with the requested ID/GUID retained and no fabricated native flag. No automatic read-back or persistence claim occurs; callers may request a separate read-back if needed. The Guid is marshaled as canonical D-format text.</para><para>Reviewed declarations and non-example contracts match across STAAD.Pro 2024–2026; a shared internal service supplies these common routes. Calls run synchronously on the caller’s established COM apartment under the Support gate. Captured binding identity is checked before and after work; reacquire wrapper.Support after refresh. Other adapters, raw access and external edits are not excluded by this gate. Native units and signs are retained without conversion. No automatic analysis, save, rollback or retry occurs; partial native effects can precede a mutation failure or post-call lifetime exception. Managed tests do not certify live marshaling, result freshness or engineering behavior.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, limit, request, GUID or selection is invalid; no native mutation/retrieval is attempted for invalid input.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, or a native response has an undocumented code/type, malformed text, invalid shape, unwritten/nonfinite fields or invalid identifiers.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; original exception identity is preserved and no retry occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>bool allowNativeMutation = false;
        /// if (allowNativeMutation)
        /// {
        ///     var result = support.SetSupportUniqueId(4, System.Guid.Parse("dfe2cb8e-1b42-487f-a02c-953ac006d64a"));
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        public SupportMutationResult SetSupportUniqueId(int supportId, Guid uniqueId) =>
            service.SetSupportUniqueId(supportId, uniqueId);

        #endregion

        /// <summary>Creates an elastic-mat support definition without assigning it.</summary>
        /// <param name="direction">FoundationSupportDirection 0..5. X/Y/Z and XOnly/YOnly/ZOnly retain distinct native meanings; plate-mat All is not accepted.</param>
        /// <param name="subgradeModulus">Finite nonnegative scalar subgrade modulus in current native force/length cubed units. Zero is passed explicitly and native acceptance is reported.</param>
        /// <param name="printInfluenceAreas">Whether to include the influence print option, marshaled as native Long 0/1.</param>
        /// <param name="springType">None (0), CompressionOnly (1) or MultiLinear (2), without generating or inferring a stiffness curve.</param>
        /// <returns>A named SupportMutationResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: Uses one scalar Double subgrade in force/length cubed, direction 0..5 and separate Long print/spring flags. This differs from plate-mat scalar/array creation, which remains gated. A positive return is a support definition ID; zero is native failure, unlike fixed-support creation whose failure is -1.</para><para>Runs synchronously on the caller's established COM apartment under the existing Support gate, with captured binding checks before and after work. No save, retry, rollback, automatic analysis, unit conversion, geometry hydration or effect verification occurs. Native changes, including partial assignments, may precede a failure or post-call exception. Other adapters, raw access and external edits are not excluded. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = support.CreateElasticMat(FoundationSupportDirection.Y, 25000.0);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        public SupportMutationResult CreateElasticMat(FoundationSupportDirection direction, double subgradeModulus, bool printInfluenceAreas = false, FoundationSpringType springType = FoundationSpringType.None) => service.CreateElasticMat(direction, subgradeModulus, printInfluenceAreas, springType);

        /// <summary>Creates an elastic-footing definition without assigning it.</summary>
        /// <param name="length">Finite positive footing length in current native length units.</param>
        /// <param name="width">Finite positive footing width in current native length units.</param>
        /// <param name="direction">FoundationSupportDirection 0..5. X/Y/Z and XOnly/YOnly/ZOnly retain distinct native meanings; plate-mat All is not accepted.</param>
        /// <param name="subgradeModulus">Finite nonnegative scalar subgrade modulus in current native force/length cubed units. Zero is passed explicitly and native acceptance is reported.</param>
        /// <returns>A named SupportMutationResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: Length and width must be finite and positive in current native length units. Subgrade is finite and nonnegative in force/length cubed, not a nodal spring stiffness. Direction 0..5 preserves X/Y/Z versus XOnly/YOnly/ZOnly. Positive return is a definition ID; zero is native failure.</para><para>Runs synchronously on the caller's established COM apartment under the existing Support gate, with captured binding checks before and after work. No save, retry, rollback, automatic analysis, unit conversion, geometry hydration or effect verification occurs. Native changes, including partial assignments, may precede a failure or post-call exception. Other adapters, raw access and external edits are not excluded. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = support.CreateElasticFooting(2.0, 3.0, FoundationSupportDirection.Y, 25000.0);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        public SupportMutationResult CreateElasticFooting(double length, double width, FoundationSupportDirection direction, double subgradeModulus) => service.CreateElasticFooting(length, width, direction, subgradeModulus);

        /// <summary>Removes an elastic-mat definition by its support reference ID.</summary>
        /// <param name="supportId">Positive support definition/reference ID; not an analytical node, plate ID or zero-based list index.</param>
        /// <returns>A named SupportMutationResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: This definition-level call differs from RemoveElasticMatFromNode. Shared-assignment consequences are left to STAAD and are not enumerated or certified. Actual Boolean or integral 1/0 acknowledgements are supported; undocumented negative values are rejected.</para><para>Runs synchronously on the caller's established COM apartment under the existing Support gate, with captured binding checks before and after work. No save, retry, rollback, automatic analysis, unit conversion, geometry hydration or effect verification occurs. Native changes, including partial assignments, may precede a failure or post-call exception. Other adapters, raw access and external edits are not excluded. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = support.RemoveElasticMat(7);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        public SupportMutationResult RemoveElasticMat(int supportId) => service.RemoveElasticMat(supportId);

        /// <summary>Removes the elastic-mat assignment from one analytical node.</summary>
        /// <param name="nodeId">Positive analytical node ID; not a support definition/reference ID.</param>
        /// <returns>A named SupportMutationResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: The argument is a node ID, not a support definition ID. No shared-definition deletion is inferred. TRUE/FALSE is decoded independently of the zero-success legacy AssignSupportToNodes route. Actual Boolean or integral 1/0 acknowledgements are supported; undocumented negative values are rejected.</para><para>Runs synchronously on the caller's established COM apartment under the existing Support gate, with captured binding checks before and after work. No save, retry, rollback, automatic analysis, unit conversion, geometry hydration or effect verification occurs. Native changes, including partial assignments, may precede a failure or post-call exception. Other adapters, raw access and external edits are not excluded. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = support.RemoveElasticMatFromNode(17);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        public SupportMutationResult RemoveElasticMatFromNode(int nodeId) => service.RemoveElasticMatFromNode(nodeId);

        /// <summary>Removes a plate-mat definition by its support reference ID.</summary>
        /// <param name="supportId">Positive support definition/reference ID; not an analytical node, plate ID or zero-based list index.</param>
        /// <returns>A named SupportMutationResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: Accepts an existing definition ID even though plate-mat creation/readback remain gated. This is not the unresolved two-argument RemovePlateMatFromPlate operation. Shared-assignment consequences are left to STAAD. Actual Boolean or integral 1/0 values implement the documented TRUE/FALSE return; negative values are rejected.</para><para>Runs synchronously on the caller's established COM apartment under the existing Support gate, with captured binding checks before and after work. No save, retry, rollback, automatic analysis, unit conversion, geometry hydration or effect verification occurs. Native changes, including partial assignments, may precede a failure or post-call exception. Other adapters, raw access and external edits are not excluded. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = support.RemovePlateMat(7);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        public SupportMutationResult RemovePlateMat(int supportId) => service.RemovePlateMat(supportId);

        /// <summary>Removes an elastic-footing definition by its support reference ID.</summary>
        /// <param name="supportId">Positive support definition/reference ID; not an analytical node, plate ID or zero-based list index.</param>
        /// <returns>A named SupportMutationResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: This definition-level operation differs from node-specific removal. Its documented numeric 1/0 convention is decoded narrowly; integral -1 is rejected, not treated as true. No assignment enumeration, effect verification or automatic deletion of other foundations is promised.</para><para>Runs synchronously on the caller's established COM apartment under the existing Support gate, with captured binding checks before and after work. No save, retry, rollback, automatic analysis, unit conversion, geometry hydration or effect verification occurs. Native changes, including partial assignments, may precede a failure or post-call exception. Other adapters, raw access and external edits are not excluded. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = support.RemoveElasticFooting(7);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        public SupportMutationResult RemoveElasticFooting(int supportId) => service.RemoveElasticFooting(supportId);

        /// <summary>Removes the elastic-footing assignment from one analytical node.</summary>
        /// <param name="nodeId">Positive analytical node ID; not a support definition/reference ID.</param>
        /// <returns>A named SupportMutationResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: The argument is an analytical node ID, not a support definition ID. Native 1/true acknowledges and 0/false fails. Integral -1 is not a success flag for this operation. Shared definition deletion is not inferred.</para><para>Runs synchronously on the caller's established COM apartment under the existing Support gate, with captured binding checks before and after work. No save, retry, rollback, automatic analysis, unit conversion, geometry hydration or effect verification occurs. Native changes, including partial assignments, may precede a failure or post-call exception. Other adapters, raw access and external edits are not excluded. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = support.RemoveElasticFootingFromNode(17);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        public SupportMutationResult RemoveElasticFootingFromNode(int nodeId) => service.RemoveElasticFootingFromNode(nodeId);

        /// <summary>Assigns a support through the native entity-list route with an explicit requested entity family.</summary>
        /// <param name="supportId">Positive support definition/reference ID; not an analytical node, plate ID or zero-based list index.</param>
        /// <param name="entityKind">Managed Nodes or Plates request family. Must match the actual native definition; it is not sent as a native type selector.</param>
        /// <param name="entityIds">Non-null sequence of positive analytical node or plate IDs matching entityKind. Enumerated once, bounded and copied; input order and duplicates remain.</param>
        /// <param name="maximumResults">Managed input limit from 1 through 100000. Oversized inputs are rejected before native invocation.</param>
        /// <returns>A named SupportMutationResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: Calls AssignSupportToEntityList(supportId, Int32[]), preserving order/duplicates in an owned packet. Plates are required for plate-mat definitions; nodes for other types. entityKind records caller intent and is not a native selector or proof of the definition type. The caller must choose the matching family. Unlike AssignSupportToNodes, native TRUE/FALSE uses actual Boolean or integral 1/0 rather than integral zero-success. Empty input is NoOperation without native invocation. Receipts preserve EntityIds/EntityKind; NodeIds is empty for plates.</para><para>Runs synchronously on the caller's established COM apartment under the existing Support gate, with captured binding checks before and after work. No save, retry, rollback, automatic analysis, unit conversion, geometry hydration or effect verification occurs. Native changes, including partial assignments, may precede a failure or post-call exception. Other adapters, raw access and external edits are not excluded. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = support.AssignSupportToEntities(7, StaadPro.Interop.Enums.Support.SupportEntityKind.Nodes, new[] { 17, 41 });
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        public SupportMutationResult AssignSupportToEntities(int supportId, SupportEntityKind entityKind, IEnumerable<int> entityIds, int maximumResults = 100000) => service.AssignSupportToEntities(supportId, entityKind, entityIds, maximumResults);
    }
}
