using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Comparers;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Extensions;
using StaadPro.Interop.Models;
using StaadPro.Interop.Enums.Loads;
using StaadPro.Interop.Models.Loads.Queries;
using StaadPro.Interop.Models.Loads.Contracts;
using StaadPro.Interop.Models.Loads.Wind;
using StaadPro.Interop.Models.Loads.Seismic;
using StaadPro.Interop.Services.Load;
using StaadPro.Interop.Services.Interop;

namespace StaadPro.Interop.Adapters.Models
{
    /// <summary>
    /// Managed adapter for STAAD.Pro OpenSTAAD Load operations wrapping the underlying COM object.
    /// </summary>
    public class OSLoadAdapter : OSBaseAdapter, IOSLoad
    {
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='OSLoadAdapter..ctor(OpenStaadWrapper)']/*" />
        public OSLoadAdapter(OpenStaadWrapper wrapper) : base(wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            ComObject = wrapper.RawLoad ?? throw new ArgumentNullException(nameof(wrapper.RawLoad), "STAAD Load COM object is not initialized.");
            var context = new LoadServiceContext(wrapper, (object)ComObject);
            inspection = new LoadInspectionService(wrapper, (object)ComObject, context.Gate);
            caseManagement = new LoadCaseManagementService(context);
            automaticGeneration = new LoadAutomaticGenerationService(context);
            assignment = new LoadAssignmentService(context);
            assignmentQueries = new LoadAssignmentQueryService(context);
            beamTransfer = new LoadBeamTransferService(context);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.P:ComObject']/*" />
        public dynamic ComObject { get; }

        private readonly LoadInspectionService inspection;
        private readonly LoadCaseManagementService caseManagement;
        private readonly LoadAutomaticGenerationService automaticGeneration;
        private readonly LoadAssignmentService assignment;
        private readonly LoadAssignmentQueryService assignmentQueries;
        private readonly LoadBeamTransferService beamTransfer;

        #region Load expansion operations

        /// <summary>Splits the original beam load definitions onto the newly created beam.</summary>
        /// <param name="originalBeamId">Positive original beam number; it must differ from newBeamId.</param>
        /// <param name="newBeamId">Positive newly created beam number; geometry creation is a separate operation.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use after splitting beam geometry when the existing load definitions must be transferred to the two beam IDs. Unlike AddMemberLoad, this edits existing load definitions and does not create beam geometry or assign a new load template. Unlike MergeBeamLoads, it splits rather than combines definitions. Native scope across cases is controlled by STAAD; no managed case filter is implied. The IDs follow the native declaration and VBA example; its parameter-table description is erroneous. Native TRUE/1 means success.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.SplitLoadsOnBeam(17, 41);
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult SplitLoadsOnBeam(int originalBeamId, int newBeamId) =>
            beamTransfer.SplitLoadsOnBeam(originalBeamId, newBeamId);

        /// <summary>Adds explicit primary case IDs to an existing envelope.</summary>
        /// <param name="envelopeId">Positive existing envelope number.</param>
        /// <param name="primaryLoadCaseIds">Non-null, nonempty positive primary-case IDs; snapshotted once, preserving order and duplicates.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use to change membership of an existing envelope. CreateLoadEnvelopExt creates an envelope; CreateLoadCombinations creates combination cases. This method does neither and never changes load factors. Pass primary case IDs, not an envelope index or LOAD LIST record index. Native 1 means success.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.AddCasesToEnvelope(1, new[] { 17, 41 });
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult AddCasesToEnvelope(int envelopeId, IEnumerable<int> primaryLoadCaseIds) =>
            caseManagement.AddCasesToEnvelope(envelopeId, primaryLoadCaseIds);

        /// <summary>Removes explicit primary cases from an existing envelope.</summary>
        /// <param name="envelopeId">Positive existing envelope number.</param>
        /// <param name="primaryLoadCaseIds">Non-null, nonempty positive primary-case IDs; snapshotted once, preserving order and duplicates.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use to remove selected memberships while retaining the envelope and the load-case definitions. DeleteEnvelope removes the envelope itself; DeleteLoadCases removes case definitions. An empty list is rejected and never means remove all. Native 1 means success.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.RemoveCasesFromEnvelope(1, new[] { 17 });
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult RemoveCasesFromEnvelope(int envelopeId, IEnumerable<int> primaryLoadCaseIds) =>
            caseManagement.RemoveCasesFromEnvelope(envelopeId, primaryLoadCaseIds);

        /// <summary>Deletes one existing envelope explicitly.</summary>
        /// <param name="envelopeId">Positive existing envelope number.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use to delete an envelope definition. The cases previously listed in it remain case definitions; use DeleteLoadCases only when those cases should also be deleted. This method does not delete a LOAD LIST. Native 1 means success.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.DeleteEnvelope(1);
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult DeleteEnvelope(int envelopeId) =>
            caseManagement.DeleteEnvelope(envelopeId);

        /// <summary>Generates combinations using the installed code/category rules.</summary>
        /// <param name="request">Non-null typed generation request with an explicit case selection and valid parameters.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use installed code/category rule files to generate combination cases. CreateLoadCombinations instead accepts explicit caller-defined factors; CreateAutomaticRepeatLoads generates repeat cases, not result-combination definitions. Code and category strings must match the acquired installation, not a hard-coded engineering-code enum. AllCases is an explicit opt-in to the native empty-array all-case behavior. STAAD may advance the requested start ID; read ActualStartCaseId. GeneratedCount is null because this native API supplies no count, and IDs are never inferred as contiguous. Native 0 means success; the 2024 help lists -1 failure, while 2025/2026 list -8002/-8040/-8041. Error descriptions use wrapper.StaadVersion, never the host ApplicationVersion.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.CreateAutomaticCombinations(new AutomaticCombinationRequest("AISC 9th Ed", "2.3 LRFD General", LoadCaseSelection.SpecifiedCases(new[] { 17, 41 }), 100));
        ///     Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// }</code></example>
        public LoadOperationResult<AutomaticLoadGenerationData> CreateAutomaticCombinations(AutomaticCombinationRequest request) =>
            automaticGeneration.CreateAutomaticCombinations(request);

        /// <summary>Generates repeat cases with explicit reference/notional options.</summary>
        /// <param name="request">Non-null typed generation request with an explicit case selection and valid parameters.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use code/category rules to generate repeat load cases whose loads participate in a subsequent analysis. CreateAutomaticCombinations creates combination definitions instead; AddRepeatLoadsTo assigns explicit factors to one existing case. Optional reference and notional participation is explicit; null NotionalOptions disables all notional flags. The returned ActualStartCaseId and GeneratedCount come from native outputs, not a guessed contiguous ID range. Native 0 means success; -1 means failure.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.CreateAutomaticRepeatLoads(new AutomaticRepeatLoadRequest(new AutomaticCombinationRequest("AISC 9th Ed", "2.3 LRFD General", LoadCaseSelection.SpecifiedCases(new[] { 17, 41 }), 100)));
        ///     Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// }</code></example>
        public LoadOperationResult<AutomaticLoadGenerationData> CreateAutomaticRepeatLoads(AutomaticRepeatLoadRequest request) =>
            automaticGeneration.CreateAutomaticRepeatLoads(request);

        /// <summary>Changes the engineering load classification of existing native cases.</summary>
        /// <param name="loadCaseIds">Non-null, nonempty positive case IDs; empty input never means all.</param>
        /// <param name="loadType">Defined existing LoadType engineering classification, not case family.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use to reclassify existing cases as Dead, Live, Wind and similar engineering categories. This LoadType is different from LoadCaseType (primary/reference/combination family), LoadCommandKind (command table) and LoadItemKind (original item code). This changes native cases directly; setting a detached LoadCase.Type alone does not invoke this operation. Native 0 means success.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.SetLoadType(new[] { 17, 41 }, LoadType.Dead);
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult SetLoadType(IEnumerable<int> loadCaseIds, LoadType loadType) =>
            caseManagement.SetLoadType(loadCaseIds, loadType);

        /// <summary>Deletes one native LOAD LIST record.</summary>
        /// <param name="loadListIndex">Positive one-based native LOAD LIST index.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use to remove one LOAD LIST record created through CreateLoadList. The argument is a one-based list record index from GetLoadListCount, not a case number or envelope ID. Cases and envelopes remain defined. Use DeleteLoadCases or DeleteEnvelope for those scopes. Native 0 means success.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.DeleteLoadList(1);
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult DeleteLoadList(int loadListIndex) =>
            caseManagement.DeleteLoadList(loadListIndex);

        /// <summary>Sets an ASD strength-of-forces attribute.</summary>
        /// <param name="loadCaseId">Positive native case number.</param>
        /// <param name="strengthType">Defined native ASD strength-of-forces choice.</param>
        /// <param name="increaseAllowableStress">Explicitly allows the native one-third ASD increase when true.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use for an ASD case attribute with an explicit strength-of-forces choice and optional one-third allowable-stress increase. SetLimitStateAttribute instead sets LSD; SetLoadType changes engineering classification rather than design attributes. This does not select a model design code. GetLoadAttribute cannot reconstruct these settings: Bentley documents only a status observation for that getter. Native 0 means success.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.SetAllowableStressAttribute(17, LoadStrengthType.WorkingStressWithoutPDelta, false);
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult SetAllowableStressAttribute(int loadCaseId, LoadStrengthType strengthType, bool increaseAllowableStress) =>
            caseManagement.SetAllowableStressAttribute(loadCaseId, strengthType, increaseAllowableStress);

        /// <summary>Sets the LSD attribute of one case.</summary>
        /// <param name="loadCaseId">Positive native case number.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use to set the LSD case attribute. SetAllowableStressAttribute instead exposes ASD strength and allowable-stress choices; SetLoadType changes engineering classification. This does not select a design code or run a design. GetLoadAttribute cannot reconstruct LSD/ASD settings from its documented status-only return. Native 0 means success.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.SetLimitStateAttribute(17);
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult SetLimitStateAttribute(int loadCaseId) =>
            caseManagement.SetLimitStateAttribute(loadCaseId);

        /// <summary>Removes the native attribute of one case.</summary>
        /// <param name="loadCaseId">Positive native case number.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use to remove a case design attribute while retaining its loads and definition. ClearPrimaryLoadCase clears items, and DeleteLoadCases removes case definitions; neither is a substitute for this attribute operation. Native 0 means success.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.RemoveLoadAttribute(17);
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult RemoveLoadAttribute(int loadCaseId) =>
            caseManagement.RemoveLoadAttribute(loadCaseId);

        /// <summary>Observes the documented native attribute status without decoding attribute settings.</summary>
        /// <param name="loadCaseId">Positive native case number.</param>
        /// <returns>A status/data result; inspect IsSuccessful before reading Data. Failed, unsupported or changed observations expose no payload.</returns>
        /// <remarks><para>When to use: Use only to observe the documented native GetAttribute status. A successful Data.NativeValue is 0; it does not identify ASD, LSD, LoadStrengthType, stress-increase settings, or the absence of an attribute. Unlike the attribute setters, this API cannot return their settings because Bentley exposes no documented output payload. -1 produces NativeFailure; any other integral value is rejected as an undocumented schema rather than decoded speculatively.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.GetLoadAttribute(17);
        ///     Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// }</code></example>
        public LoadQueryResult<LoadAttributeObservation> GetLoadAttribute(int loadCaseId) =>
            caseManagement.GetLoadAttribute(loadCaseId);

        /// <summary>Reads the original entity assignments of a native command.</summary>
        /// <param name="primaryLoadCaseId">Positive primary-case number, activated internally with before/after identity checks.</param>
        /// <param name="commandKind">Defined LoadCommandKind, distinct from engineering LoadType and LoadItemKind.</param>
        /// <param name="commandIndex">Nonnegative zero-based index within the native command kind.</param>
        /// <param name="maximumResults">Positive retrieval limit; defaults to 100000. No silent truncation or unbounded allocation.</param>
        /// <returns>A status/data result; inspect IsSuccessful before reading Data. Failed, unsupported or changed observations expose no payload.</returns>
        /// <remarks><para>When to use: Use to preserve the entity assignment list of one original command, including native order and duplicates. GetAssignedEntityCount returns only its size. GetNodalLoads/GetMemberUniformlyDistributedLoads read loads for an entity, while grouped population methods reorganize loads across entities and can lose original command identity. The zero-based commandIndex is within commandKind, not the loadItemIndex used by GetLoadItemKind. Native zero is an error/unavailable observation, not a proven empty assignment. Sizing, returned size and observed command/entity counts must agree.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs. The selected primary/reference case remains active, including after failure. Activation and before/after numeric checks detect observed switches but cannot prove an atomic snapshot or distinguish an external switch to the other family with the same number.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.GetAssignedEntityIds(17, LoadCommandKind.Nodal, 0);
        ///     Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// }</code></example>
        public LoadQueryResult<IEnumerable<int>> GetAssignedEntityIds(int primaryLoadCaseId, LoadCommandKind commandKind, int commandIndex, int maximumResults = 100000) =>
            inspection.GetAssignedEntityIds(primaryLoadCaseId, commandKind, commandIndex, maximumResults);

        /// <summary>Deletes explicitly selected primary or reference case definitions.</summary>
        /// <param name="caseType">PrimaryLoad or ReferenceLoad only; combination/repeat classifications are rejected.</param>
        /// <param name="loadCaseIds">Non-null, nonempty positive case IDs; empty input never means all.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use to remove primary or reference case definitions. ClearPrimaryLoadCases/ClearReferenceLoadCases clear their contents while retaining the cases. Combination and RepeatLoad enum choices are rejected; repeat commands live inside primary cases and require primary-family selection. Primary requests dispatch DeletePrimaryLoadCases with the reference flag false; reference requests use DeleteReferenceLoadCases. Native 1 means success. Dependency failures are reported without deletion/recreation fallback.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.DeleteLoadCases(LoadCaseType.PrimaryLoad, new[] { 17, 41 });
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult DeleteLoadCases(LoadCaseType caseType, IEnumerable<int> loadCaseIds) =>
            caseManagement.DeleteLoadCases(caseType, loadCaseIds);

        /// <summary>Adds whole-model self-weight to an explicitly selected case.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="direction">Defined semantic axis/direction; internal mapping uses the operation-specific numeric domain.</param>
        /// <param name="factor">Finite signed dimensionless multiplier; zero is preserved.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use for native whole-model self-weight covering beams, plates and solids. AddSelfweightInXYZTo instead assigns to an explicit geometry list; these scopes are different even when a caller currently lists every entity. The factor is dimensionless, with its sign preserved, and the global direction uses SelfWeightDirection. Native Boolean TRUE means success.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs. The selected primary/reference case remains active, including after failure. Activation and before/after numeric checks detect observed switches but cannot prove an atomic snapshot or distinguish an external switch to the other family with the same number.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.AddSelfWeight(new LoadCase(17), SelfWeightDirection.GlobalY, -1.0);
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult AddSelfWeight(ILoadCase targetCase, SelfWeightDirection direction, double factor) =>
            assignment.AddSelfWeight(targetCase, direction, factor);

        /// <summary>Reads six nodal load components by original command index.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="loadItemIndex">Nonnegative zero-based original item index from GetLoadItemKind; distinct from a per-kind commandIndex.</param>
        /// <returns>A status/data result; inspect IsSuccessful before reading Data. Failed, unsupported or changed observations expose no payload.</returns>
        /// <remarks><para>When to use: Use to inspect the six components of one original ordinary nodal command. GetNodalLoads reads loads assigned to a particular node; GetNodalLoadsGroupedByLoadCase reorganizes entity data. This packet contains no node list: use GetAssignedEntityIds with the appropriate command index for assignments. Only native Nodal items are accepted; inclined loads and support displacement are not interchangeable. Pass the original zero-based GetLoadItemKind index unchanged. The nodal help omits an explicit base; confirm this command-index convention against the acquired build before production use. Native Boolean TRUE means success.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs. The selected primary/reference case remains active, including after failure. Activation and before/after numeric checks detect observed switches but cannot prove an atomic snapshot or distinguish an external switch to the other family with the same number.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.GetNodalLoadCommand(new LoadCase(17), 0);
        ///     Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// }</code></example>
        public LoadQueryResult<NodalLoadCommandInfo> GetNodalLoadCommand(ILoadCase targetCase, int loadItemIndex) =>
            assignmentQueries.GetNodalLoadCommand(targetCase, loadItemIndex);

        /// <summary>Adds a signed area-load value to explicit members.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="memberIds">Non-null, nonempty positive member IDs; enumerated once.</param>
        /// <param name="areaLoad">Finite signed native area-load quantity, not assumed uniform force per length.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use to assign the native member AREA load parameter to selected beam IDs. AddFloorLoad instead searches enclosed floor panels and distributes floor pressure; AddMemberLoad accepts supported line-load templates. These operations can produce different distributions on the same geometry. The signed area-load value uses current native units. Native 0 means success.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs. The selected primary/reference case remains active, including after failure. Activation and before/after numeric checks detect observed switches but cannot prove an atomic snapshot or distinguish an external switch to the other family with the same number.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.AddMemberAreaLoad(new LoadCase(17), new[] { 1, 2 }, -2.0);
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult AddMemberAreaLoad(ILoadCase targetCase, IEnumerable<int> memberIds, double areaLoad) =>
            assignment.AddMemberAreaLoad(targetCase, memberIds, areaLoad);

        /// <summary>Adds a fixed-end force/moment definition at both member ends.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="memberIds">Non-null, nonempty positive member IDs; enumerated once.</param>
        /// <param name="definition">Non-null typed definition with validated finite values and explicit mode.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use to supply six force/moment components at each member end as a fixed-end load definition. This is different from assigning concentrated member forces at a distance, support settlements, or reading Output reactions. StartForces and EndForces follow Fx/Fy/Fz/Mx/My/Mz order; signs and current force/moment units are preserved without support-reaction sign reversal. Native 0 means success.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs. The selected primary/reference case remains active, including after failure. Activation and before/after numeric checks detect observed switches but cannot prove an atomic snapshot or distinguish an external switch to the other family with the same number.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.AddMemberFixedEndLoad(new LoadCase(17), new[] { 1 }, new FixedEndLoadDefinition(new Forces(0, -1, 0, 0, 0, 0), new Forces(0, -1, 0, 0, 0, 0)));
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult AddMemberFixedEndLoad(ILoadCase targetCase, IEnumerable<int> memberIds, FixedEndLoadDefinition definition) =>
            assignment.AddMemberFixedEndLoad(targetCase, memberIds, definition);

        /// <summary>Reads a member command packet at its original zero-based index.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="loadItemIndex">Nonnegative zero-based original item index from GetLoadItemKind; distinct from a per-kind commandIndex.</param>
        /// <returns>A status/data result; inspect IsSuccessful before reading Data. Failed, unsupported or changed observations expose no payload.</returns>
        /// <remarks><para>When to use: Use to retain one original member-command packet and its native item kind. GetMemberUniformlyDistributedLoads and the concentrated/linear/trapezoidal readers instead select per-member load definitions. Force parameter dimensions depend on Kind; do not interpret moment slots as forces. Distances retain their native meanings and length units. Only the six ordinary uniform-force, uniform-moment, concentrated-force, concentrated-moment, linear and trapezoidal kinds documented for this reader are accepted. Physical-member and fixed-end packets are not inferred. The item index is zero-based. Native Boolean TRUE means success.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs. The selected primary/reference case remains active, including after failure. Activation and before/after numeric checks detect observed switches but cannot prove an atomic snapshot or distinguish an external switch to the other family with the same number.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.GetMemberLoadCommand(new LoadCase(17), 0);
        ///     Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// }</code></example>
        public LoadQueryResult<MemberLoadCommandInfo> GetMemberLoadCommand(ILoadCase targetCase, int loadItemIndex) =>
            assignmentQueries.GetMemberLoadCommand(targetCase, loadItemIndex);

        /// <summary>Adds hydrostatic pressures to explicit plates.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="plateIds">Non-null, nonempty positive plate IDs; enumerated once.</param>
        /// <param name="definition">Non-null typed definition with validated finite values and explicit mode.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use for pressures interpolated along a global coordinate axis. AddTrapezoidalPressure varies along plate-local X/Y or by joint; AddPlateUniformPressure assigns a uniform value. Pressure direction and interpolation axis are independent: the latter maps GlobalAxis X/Y/Z to native 1/2/3. Signed minimum/maximum pressure parameters are passed unchanged, not sorted or recalculated from fluid density. Native Boolean TRUE/1 means success.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs. The selected primary/reference case remains active, including after failure. Activation and before/after numeric checks detect observed switches but cannot prove an atomic snapshot or distinguish an external switch to the other family with the same number.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.AddHydrostaticPressure(new LoadCase(17), new[] { 1 }, new HydrostaticPressureDefinition(LoadDirection.LocalZ, GlobalAxis.Y, 0.0, -10.0));
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult AddHydrostaticPressure(ILoadCase targetCase, IEnumerable<int> plateIds, HydrostaticPressureDefinition definition) =>
            assignment.AddHydrostaticPressure(targetCase, plateIds, definition);

        /// <summary>Adds two-value or joint-ordered trapezoidal plate pressures.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="plateIds">Non-null, nonempty positive plate IDs; enumerated once.</param>
        /// <param name="definition">Non-null typed definition with validated finite values and explicit mode.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use for two-value variation along plate-local X/Y or four joint-ordered pressures. AddHydrostaticPressure interpolates along a global axis; AddPlateUniformPressure supplies one uniform pressure. AlongLocalAxis supplies zero only for the two native ignored corner slots; AtCorners retains all four joint values. No joint sorting or geometry conversion occurs. Confirm the fourth-value convention separately for triangular plates. Native Boolean TRUE/1 means success.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs. The selected primary/reference case remains active, including after failure. Activation and before/after numeric checks detect observed switches but cannot prove an atomic snapshot or distinguish an external switch to the other family with the same number.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.AddTrapezoidalPressure(new LoadCase(17), new[] { 1 }, TrapezoidalPressureDefinition.AlongLocalAxis(LoadDirection.LocalZ, PlatePressureVariation.LocalY, 0.0, -10.0));
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult AddTrapezoidalPressure(ILoadCase targetCase, IEnumerable<int> plateIds, TrapezoidalPressureDefinition definition) =>
            assignment.AddTrapezoidalPressure(targetCase, plateIds, definition);

        /// <summary>Counts pressure definitions applied to a plate in the selected case.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="plateId">Positive native plate number.</param>
        /// <returns>A status/data result; inspect IsSuccessful before reading Data. Failed, unsupported or changed observations expose no payload.</returns>
        /// <remarks><para>When to use: Use to count applied pressure entries for one plate in the selected case. GetLoadItemCount counts original case command items, which need not equal this per-plate count; GetPlateConcentratedLoadCount covers concentrated loads. GetPlatePressureLoads returns the entries with values and local extents. Zero is a valid empty count, -1 is failure.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs. The selected primary/reference case remains active, including after failure. Activation and before/after numeric checks detect observed switches but cannot prove an atomic snapshot or distinguish an external switch to the other family with the same number.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.GetPlatePressureLoadCount(new LoadCase(17), 1);
        ///     Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// }</code></example>
        public LoadQueryResult<int> GetPlatePressureLoadCount(ILoadCase targetCase, int plateId) =>
            assignmentQueries.GetPlatePressureLoadCount(targetCase, plateId);

        /// <summary>Reads ordered plate pressure definitions with local rectangles.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="plateId">Positive native plate number.</param>
        /// <param name="maximumResults">Positive retrieval limit; defaults to 100000. No silent truncation or unbounded allocation.</param>
        /// <returns>A status/data result; inspect IsSuccessful before reading Data. Failed, unsupported or changed observations expose no payload.</returns>
        /// <remarks><para>When to use: Use to read applied pressure entries and local rectangular extents for one plate. GetPlateLoadCommand reads an original full-pressure command packet by item index; GetPlateConcentratedLoads reads force entries at local points. These are load definitions, not Output plate stresses. Native top-left/bottom-right order is retained without min/max sorting. The bounded owned snapshot validates all directions and finite parameters and rechecks the count. Native 0 means success; -1 means failure. Zero count returns a read-only empty snapshot without fetching.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs. The selected primary/reference case remains active, including after failure. Activation and before/after numeric checks detect observed switches but cannot prove an atomic snapshot or distinguish an external switch to the other family with the same number.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.GetPlatePressureLoads(new LoadCase(17), 1);
        ///     Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// }</code></example>
        public LoadQueryResult<IEnumerable<PlatePressureLoad>> GetPlatePressureLoads(ILoadCase targetCase, int plateId, int maximumResults = 100000) =>
            assignmentQueries.GetPlatePressureLoads(targetCase, plateId, maximumResults);

        /// <summary>Counts applied concentrated plate loads.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="plateId">Positive native plate number.</param>
        /// <returns>A status/data result; inspect IsSuccessful before reading Data. Failed, unsupported or changed observations expose no payload.</returns>
        /// <remarks><para>When to use: Use to count concentrated applied-load entries for one plate. GetPlatePressureLoadCount counts pressure entries; GetLoadItemCount counts original commands for the case. GetPlateConcentratedLoads returns magnitude and local position. Zero is a valid empty count, -1 is failure.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs. The selected primary/reference case remains active, including after failure. Activation and before/after numeric checks detect observed switches but cannot prove an atomic snapshot or distinguish an external switch to the other family with the same number.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.GetPlateConcentratedLoadCount(new LoadCase(17), 1);
        ///     Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// }</code></example>
        public LoadQueryResult<int> GetPlateConcentratedLoadCount(ILoadCase targetCase, int plateId) =>
            assignmentQueries.GetPlateConcentratedLoadCount(targetCase, plateId);

        /// <summary>Reads applied concentrated plate loads and their local positions.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="plateId">Positive native plate number.</param>
        /// <param name="maximumResults">Positive retrieval limit; defaults to 100000. No silent truncation or unbounded allocation.</param>
        /// <returns>A status/data result; inspect IsSuccessful before reading Data. Failed, unsupported or changed observations expose no payload.</returns>
        /// <remarks><para>When to use: Use for concentrated applied loads at local plate positions. GetPlatePressureLoads instead returns pressure entries and rectangular extents; GetPlateLoadCommand returns an original pressure command. Magnitudes remain in current native units and are not divided by area. Native 0 means success, -1 means failure; bounded paired buffers and before/after counts protect the owned snapshot. A zero count returns an empty snapshot without fetching.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs. The selected primary/reference case remains active, including after failure. Activation and before/after numeric checks detect observed switches but cannot prove an atomic snapshot or distinguish an external switch to the other family with the same number.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.GetPlateConcentratedLoads(new LoadCase(17), 1);
        ///     Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// }</code></example>
        public LoadQueryResult<IEnumerable<PlateConcentratedLoad>> GetPlateConcentratedLoads(ILoadCase targetCase, int plateId, int maximumResults = 100000) =>
            assignmentQueries.GetPlateConcentratedLoads(targetCase, plateId, maximumResults);

        /// <summary>Reads an original plate pressure-command packet.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="loadItemIndex">Nonnegative zero-based original item index from GetLoadItemKind; distinct from a per-kind commandIndex.</param>
        /// <returns>A status/data result; inspect IsSuccessful before reading Data. Failed, unsupported or changed observations expose no payload.</returns>
        /// <remarks><para>When to use: Use only for the original ElementLoadPressure (PlateFullPressure, 3310) item packet. GetPlatePressureLoads reads per-plate pressure assignments; it is the alternative for those values and extents. Concentrated, trapezoidal, hydrostatic and partial-pressure item codes are not treated as full-pressure packets. The item index is zero-based. This native reader alone reports directions 0-8, translated to LoadDirection 1-9; member/plate-list readers already use 1-9. Native 1 means success, 0 failure, -1 Unsupported. Bentley explicitly requires selecting the load case in the STAAD UI: managed activation and numeric checks do not certify UI selection. Ensure that UI prerequisite before calling.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs. The selected primary/reference case remains active, including after failure. Activation and before/after numeric checks detect observed switches but cannot prove an atomic snapshot or distinguish an external switch to the other family with the same number.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.GetPlateLoadCommand(new LoadCase(17), 0);
        ///     Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// }</code></example>
        public LoadQueryResult<PlateLoadCommandInfo> GetPlateLoadCommand(ILoadCase targetCase, int loadItemIndex) =>
            assignmentQueries.GetPlateLoadCommand(targetCase, loadItemIndex);

        /// <summary>Adds a range/group/one-way floor-load request through one managed operation.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="definition">Non-null typed definition with validated finite values and explicit mode.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use to let STAAD find enclosed floor panels and distribute pressure using a coordinate range or an existing FLOOR group. AddMemberAreaLoad applies the separate member AREA command; GetFloorInfluenceAreas observes geometry and assigns nothing. The extended native floor function handles all modes, so the legacy fixed-direction call is not duplicated. Range axis and loading axis are independent. Standard range mode passes the single-space marker shown in Bentley's VBA example; one-way mode passes "0" from its parameter table. Group mode passes the exact group name and zero ignored bounds; it does not create the group. Native 1 means success. Verify generated distributions, particularly one-way floors, against the target model.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs. The selected primary/reference case remains active, including after failure. Activation and before/after numeric checks detect observed switches but cannot prove an atomic snapshot or distinguish an external switch to the other family with the same number.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.AddFloorLoad(new LoadCase(17), FloorLoadDefinition.ForRange(-2.0, GlobalAxis.Y, new FloorRegion(new GlobalLoadRegion(0, 10, 0, 0, 0, 10), GlobalAxis.Y)));
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult AddFloorLoad(ILoadCase targetCase, FloorLoadDefinition definition) =>
            assignment.AddFloorLoad(targetCase, definition);

        /// <summary>Counts beams in a bounded floor region.</summary>
        /// <param name="region">Non-null bounded region with finite coordinates and an explicit range axis.</param>
        /// <returns>A status/data result; inspect IsSuccessful before reading Data. Failed, unsupported or changed observations expose no payload.</returns>
        /// <remarks><para>When to use: Use for the native beam count in a bounded floor region, without assigning loads or selecting a load case. This is not GetAssignedEntityCount, which counts entities attached to an existing command. GetFloorInfluenceAreas returns beam/area pairs for the same region. Coordinates are converted to native Single precision (with range checks); RangeAxis maps X/Y/Z to native 1/2/3. Zero is an empty count; malformed negatives are rejected except -1, returned as NativeFailure.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.GetFloorBeamCount(new FloorRegion(new GlobalLoadRegion(0, 10, 0, 0, 0, 10), GlobalAxis.Y));
        ///     Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// }</code></example>
        public LoadQueryResult<int> GetFloorBeamCount(FloorRegion region) =>
            assignmentQueries.GetFloorBeamCount(region);

        /// <summary>Reads paired beam IDs and native floor influence areas.</summary>
        /// <param name="region">Non-null bounded region with finite coordinates and an explicit range axis.</param>
        /// <param name="maximumResults">Positive retrieval limit; defaults to 100000. No silent truncation or unbounded allocation.</param>
        /// <returns>A status/data result; inspect IsSuccessful before reading Data. Failed, unsupported or changed observations expose no payload.</returns>
        /// <remarks><para>When to use: Use to observe beam/tributary influence-area pairs calculated by STAAD for a floor region. GetFloorBeamCount provides only sizing; AddFloorLoad actually assigns pressure. This method does not multiply areas by pressure or calculate managed tributary areas. It uses the same converted Single-precision bounds and range axis for sizing and retrieval (correcting the differing axes in Bentley's example). Validates positive beam IDs, finite nonnegative areas, retrieval limit and stable count. Native Boolean TRUE means success; areas use squared native length units.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.GetFloorInfluenceAreas(new FloorRegion(new GlobalLoadRegion(0, 10, 0, 0, 0, 10), GlobalAxis.Y));
        ///     Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// }</code></example>
        public LoadQueryResult<IEnumerable<BeamInfluenceArea>> GetFloorInfluenceAreas(FloorRegion region, int maximumResults = 100000) =>
            assignmentQueries.GetFloorInfluenceAreas(region, maximumResults);

        /// <summary>Adds initial axial elongation or shrinkage to explicit beam/plate elements.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="elementIds">Non-null, nonempty positive beam/plate IDs for the native strain operation.</param>
        /// <param name="axialElongation">Finite signed initial elongation/shrinkage in the qualified native engineering convention.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use to assign the native initial axial elongation parameter to explicit element IDs. Despite the native Strain name, axialElongation is a length in current model units, not dimensionless strain; no division or multiplication by member length occurs. AddAxialThermalGradientTo instead supplies a thermal gradient, and fixed-end loading instead supplies force/moment components. Native 0 means success.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs. The selected primary/reference case remains active, including after failure. Activation and before/after numeric checks detect observed switches but cannot prove an atomic snapshot or distinguish an external switch to the other family with the same number.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.AddStrainLoad(new LoadCase(17), new[] { 1 }, 0.001);
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult AddStrainLoad(ILoadCase targetCase, IEnumerable<int> elementIds, double axialElongation) =>
            assignment.AddStrainLoad(targetCase, elementIds, axialElongation);

        /// <summary>Applies a seismic load command in a semantic global direction.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="direction">Defined semantic axis/direction; internal mapping uses the operation-specific numeric domain.</param>
        /// <param name="factor">Finite signed dimensionless multiplier; zero is preserved.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use to apply the existing seismic definition to a case with a global axis and multiplier. AddSeismicDefinition creates definition settings; AddSeismicDefSelfWeight and joint/member weight methods contribute seismic weights. AddRSLoad/AddResponseSpectrumLoadEx create response-spectrum commands instead of this generated static seismic command. GlobalAxis maps directly to native 0/1/2, unlike self-weight or hydrostatic interpolation. Native 0 means success; -1/-8001 are failures.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs. The selected primary/reference case remains active, including after failure. Activation and before/after numeric checks detect observed switches but cannot prove an atomic snapshot or distinguish an external switch to the other family with the same number.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.AddSeismicLoad(new LoadCase(17), GlobalAxis.X, 1.0);
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult AddSeismicLoad(ILoadCase targetCase, GlobalAxis direction, double factor) =>
            assignment.AddSeismicLoad(targetCase, direction, factor);

        /// <summary>Applies an existing wind definition within an explicit region.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="definition">Non-null typed definition with validated finite values and explicit mode.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use to apply an existing wind definition to a case over an explicit global region. AddWindDefinition/AddWindIntensity/AddWindExposure prepare that definition; ComputeWallWindPressureProfile computes a profile and does not assign this command. This native contract assumes Y vertical: a Z-up or unavailable geometry binding returns Unsupported before activation/assignment. No automatic coordinate remapping occurs. Uses documented X/Z signed codes 1/3/4/6 rather than the inconsistent example code 2. Native 0 means success. Signed factors are retained, so a negative factor can reverse the selected direction. Bentley’s all-zero-bounds example denotes whole-range walls; an all-zero region must not be assumed to select only a geometric point. Supply appropriate nondegenerate bounds when restricting scope.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs. The selected primary/reference case remains active, including after failure. Activation and before/after numeric checks detect observed switches but cannot prove an atomic snapshot or distinguish an external switch to the other family with the same number.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.AddWindLoad(new LoadCase(17), new WindLoadDefinition(1, WindLoadDirection.PositiveX, 1.0, true, new GlobalLoadRegion(0, 10, 0, 10, 0, 10)));
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult AddWindLoad(ILoadCase targetCase, WindLoadDefinition definition) =>
            assignment.AddWindLoad(targetCase, definition);

        /// <summary>Adds explicit ordered primary/reference notional contributions.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="contributions">Non-null, nonempty contribution records. Order and duplicates preserved; null entries rejected.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use to assign explicitly chosen primary and reference contributions to an existing case. AddRepeatLoadsTo/AddReferenceLoadsTo do not specify notional directions; CreateAutomaticRepeatLoads can generate notional participation through code rules rather than this explicit list. Contributions are partitioned into six aligned native buffers, preserving order and duplicates within each family. Assignment uses positive IDs in both families; GetNotionalContributions uses signed native IDs on readback. Factors retain signs, including zero, and directions use global X/Y/Z 1/2/3. Native 0 means success.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs. The selected primary/reference case remains active, including after failure. Activation and before/after numeric checks detect observed switches but cannot prove an atomic snapshot or distinguish an external switch to the other family with the same number.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.AddNotionalLoad(new LoadCase(17), new[] { new NotionalLoadContribution(41, LoadCaseType.PrimaryLoad, 0.002, NotionalLoadDirection.GlobalX) });
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult AddNotionalLoad(ILoadCase targetCase, IEnumerable<NotionalLoadContribution> contributions) =>
            assignment.AddNotionalLoad(targetCase, contributions);

        /// <summary>Counts contribution slots inside a reference-load command.</summary>
        /// <param name="primaryLoadCaseId">Positive primary-case number, activated internally with before/after identity checks.</param>
        /// <param name="referenceItemIndex">Nonnegative native reference-command index, passed unchanged. The installed help does not establish its base; qualify it on the acquired build.</param>
        /// <returns>A status/data result; inspect IsSuccessful before reading Data. Failed, unsupported or changed observations expose no payload.</returns>
        /// <remarks><para>When to use: Use to size the case/factor pairs inside one REFERENCE command of the activated primary case. GetReferenceItemCount counts REFERENCE commands; GetRepeatContributionCount counts primary repeat contributions. referenceItemIndex is passed unchanged to the native API: the installed reference help does not establish its index base and has an erroneous ID description in its count table. Confirm the native index on the acquired build; no speculative rebasing or ordinal-to-ID conversion is performed. -1 is NativeFailure.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs. The selected primary/reference case remains active, including after failure. Activation and before/after numeric checks detect observed switches but cannot prove an atomic snapshot or distinguish an external switch to the other family with the same number.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.GetReferenceContributionCount(17, 1);
        ///     Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// }</code></example>
        public LoadQueryResult<int> GetReferenceContributionCount(int primaryLoadCaseId, int referenceItemIndex) =>
            inspection.GetReferenceContributionCount(primaryLoadCaseId, referenceItemIndex);

        /// <summary>Reads ordered reference-case/factor pairs by reference-command index.</summary>
        /// <param name="primaryLoadCaseId">Positive primary-case number, activated internally with before/after identity checks.</param>
        /// <param name="referenceItemIndex">Nonnegative native reference-command index, passed unchanged. The installed help does not establish its base; qualify it on the acquired build.</param>
        /// <param name="maximumResults">Positive retrieval limit; defaults to 100000. No silent truncation or unbounded allocation.</param>
        /// <returns>A status/data result; inspect IsSuccessful before reading Data. Failed, unsupported or changed observations expose no payload.</returns>
        /// <remarks><para>When to use: Use to retain ordered reference-case/factor pairs in one REFERENCE command. GetReferenceItemCount counts commands, and GetRepeatContributions returns primary repeat pairs. Duplicate reference IDs and signed/zero factors are preserved. Unlike the repeat reader, the reference API's scalar is described as an ID, not a returned count; it is retained as NativeReturnCode and is not required to equal the buffer size. Data is validated against the sizing call and rechecked count. The native index is passed unchanged; Bentley does not specify its base. Confirm it on the acquired build. -1 is NativeFailure.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs. The selected primary/reference case remains active, including after failure. Activation and before/after numeric checks detect observed switches but cannot prove an atomic snapshot or distinguish an external switch to the other family with the same number.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.GetReferenceContributions(17, 1);
        ///     Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// }</code></example>
        public LoadQueryResult<IEnumerable<ReferenceLoadContribution>> GetReferenceContributions(int primaryLoadCaseId, int referenceItemIndex, int maximumResults = 100000) =>
            inspection.GetReferenceContributions(primaryLoadCaseId, referenceItemIndex, maximumResults);

        /// <summary>Invokes a balanced native beam-load merge lifecycle and returns completion evidence.</summary>
        /// <param name="requests">Non-null, nonempty retained/source-beam requests, snapshotted once before entering the native merge lifecycle.</param>
        /// <returns>Operation-specific status and ordered native evidence; inspect IsSuccessful and MayHaveChangedModel. Native failure does not guarantee rollback.</returns>
        /// <remarks><para>When to use: Use after a geometry merge when source-beam load definitions must be merged into retained beams. SplitLoadsOnBeam performs the opposite load transfer; neither method creates or merges geometry. Requests run in supplied order, with one BeginLoadMerging and an EndLoadMerging attempt in finally, even if begin/body throws. The body signature is corroborated by the Bentley class listing and installed CLR metadata, but its return meanings and affected-case scope are not documented. Consequently normal return is InvocationCompleted, IsSuccessful is false, and actual Boolean/integral responses remain in Steps; inspect model results rather than interpreting this as certified success. The merge is not transactional, may affect loads across cases, and is never retried. Both body and cleanup exceptions are preserved in AggregateException; a single exception retains its original stack. MayHaveChangedModel is true after invocation; no rollback is promised.</para><para>Runs synchronously on the caller's COM apartment. These expansion and inspection calls on one adapter share a lock; legacy/raw calls, other adapters and external STAAD edits are not excluded. The captured binding must remain valid. Collections are snapshotted once and results are materialized. No implicit save, solver run, retry, rollback or unit conversion occurs.</para></remarks>
        /// <exception cref="ArgumentException">An ID, index, enum, request, finite value or nonempty selection is invalid.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or a native response is malformed or undocumented. A mutation may already have changed the model before an output-validation exception.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native interface lacks a compatible member.</exception>
        /// <exception cref="AggregateException">Both merge execution and cleanup failed; both original exceptions are retained.</exception>
        /// <example><code>bool submitLoadChanges = false; // Review IDs, units and model state before enabling.
        /// if (submitLoadChanges)
        /// {
        ///     var result = load.MergeBeamLoads(new[] { new BeamLoadMergeRequest(17, 41) });
        ///     Console.WriteLine(result.Status);
        /// }</code></example>
        public LoadOperationResult MergeBeamLoads(IEnumerable<BeamLoadMergeRequest> requests) =>
            beamTransfer.MergeBeamLoads(requests);

        #endregion

        #region Native Load inspection and contribution queries

        /// <summary>Reads the native active load-case number without inferring its family.</summary>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; failed, ambiguous or changed observations expose no payload. NativeFunction and NativeReturnCode identify the decisive native response, including preflight calls.</returns>
        /// <remarks><para>Returns the positive native number or NativeFailure for -1. Primary/reference numbers can overlap; this result is not a LoadCase identity and performs no activation.</para><para>Uses the shared installed 2024–2026 contract; no year-specific differences were found for this operation. Runs synchronously on the caller's COM apartment. Calls through these new methods on one adapter are serialized; legacy/raw calls, other adapters and external edits are not locked out. Count/activation checks detect observed changes but cannot prove an atomic snapshot or detect same-count replacements. Dispose/rebind invalidates the captured binding. No load assignment, save, solver invocation, retry, rollback or units conversion is performed; any factors are dimensionless.</para></remarks>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed or a native response has an unsupported type, undocumented code, invalid array shape or unwritten/nonfinite data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The native interface lacks a compatible reviewed member.</exception>
        /// <example><code>var result = load.GetActiveLoadCaseNumber();
        /// if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// else Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public LoadQueryResult<int> GetActiveLoadCaseNumber() =>
            inspection.GetActiveLoadCaseNumber();

        /// <summary>Counts native LOAD LIST records.</summary>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; failed, ambiguous or changed observations expose no payload. NativeFunction and NativeReturnCode identify the decisive native response, including preflight calls.</returns>
        /// <remarks><para>Zero is a valid count; -1 is NativeFailure. This is not an envelope count or a count of cases in an individual list.</para><para>Uses the shared installed 2024–2026 contract; no year-specific differences were found for this operation. Runs synchronously on the caller's COM apartment. Calls through these new methods on one adapter are serialized; legacy/raw calls, other adapters and external edits are not locked out. Count/activation checks detect observed changes but cannot prove an atomic snapshot or detect same-count replacements. Dispose/rebind invalidates the captured binding. No load assignment, save, solver invocation, retry, rollback or units conversion is performed; any factors are dimensionless.</para></remarks>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed or a native response has an unsupported type, undocumented code, invalid array shape or unwritten/nonfinite data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The native interface lacks a compatible reviewed member.</exception>
        /// <example><code>var result = load.GetLoadListCount();
        /// if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// else Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public LoadQueryResult<int> GetLoadListCount() =>
            inspection.GetLoadListCount();

        /// <summary>Counts the case numbers in one native LOAD LIST record.</summary>
        /// <param name="loadListIndex">One-based native LOAD LIST record index; must be positive.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; failed, ambiguous or changed observations expose no payload. NativeFunction and NativeReturnCode identify the decisive native response, including preflight calls.</returns>
        /// <remarks><para>Validates the one-based list index against GetLoadListCount and rechecks that total after reading. Out-of-range lists are NotFound. The native count function has no documented negative error code; negative counts are rejected as malformed.</para><para>Uses the shared installed 2024–2026 contract; no year-specific differences were found for this operation. Runs synchronously on the caller's COM apartment. Calls through these new methods on one adapter are serialized; legacy/raw calls, other adapters and external edits are not locked out. Count/activation checks detect observed changes but cannot prove an atomic snapshot or detect same-count replacements. Dispose/rebind invalidates the captured binding. No load assignment, save, solver invocation, retry, rollback or units conversion is performed; any factors are dimensionless.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A number/index/limit or command-kind request is outside its documented domain.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed or a native response has an unsupported type, undocumented code, invalid array shape or unwritten/nonfinite data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The native interface lacks a compatible reviewed member.</exception>
        /// <example><code>var result = load.GetLoadListCaseCount(1);
        /// if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// else Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public LoadQueryResult<int> GetLoadListCaseCount(int loadListIndex) =>
            inspection.GetLoadListCaseCount(loadListIndex);

        /// <summary>Reads an owned, ordered snapshot of case numbers from a native LOAD LIST.</summary>
        /// <param name="loadListIndex">One-based native LOAD LIST record index; must be positive.</param>
        /// <param name="maximumResults">Positive retrieval limit (default 100000). A larger native count returns LimitExceeded before allocating output buffers; no silent truncation occurs.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; failed, ambiguous or changed observations expose no payload. NativeFunction and NativeReturnCode identify the decisive native response, including preflight calls.</returns>
        /// <remarks><para>Uses an Int32 SAFEARRAY output passed internally by reference following installed CLR metadata, despite const wording in the C++ prototype. Preserves order/duplicates; does not infer case families. Rechecks list and member counts; detected changes return StateChanged. Native false is NativeFailure. Zero cases returns a read-only empty list without fetching.</para><para>Uses the shared installed 2024–2026 contract; no year-specific differences were found for this operation. Runs synchronously on the caller's COM apartment. Calls through these new methods on one adapter are serialized; legacy/raw calls, other adapters and external edits are not locked out. Count/activation checks detect observed changes but cannot prove an atomic snapshot or detect same-count replacements. Dispose/rebind invalidates the captured binding. No load assignment, save, solver invocation, retry, rollback or units conversion is performed; any factors are dimensionless.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A number/index/limit or command-kind request is outside its documented domain.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed or a native response has an unsupported type, undocumented code, invalid array shape or unwritten/nonfinite data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The native interface lacks a compatible reviewed member.</exception>
        /// <example><code>var result = load.GetLoadListCases(1);
        /// if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// else Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public LoadQueryResult<IEnumerable<int>> GetLoadListCases(int loadListIndex, int maximumResults = 100000) =>
            inspection.GetLoadListCases(loadListIndex, maximumResults);

        /// <summary>Counts commands of a native kind in an explicitly activated primary case.</summary>
        /// <param name="primaryLoadCaseId">Positive primary load-case number. Activates this primary case with SetLoadActive; reference-family activation is not supported by this overload.</param>
        /// <param name="commandKind">Defined LoadCommandKind command-table value, not the engineering classification of a case.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; failed, ambiguous or changed observations expose no payload. NativeFunction and NativeReturnCode identify the decisive native response, including preflight calls.</returns>
        /// <remarks><para>Command kind is distinct from engineering LoadType and LoadItemKind. Native zero is documented as case not found and is returned as EmptyOrUnavailable, with no Data, rather than a proven empty inventory.</para><para>Uses the shared installed 2024–2026 contract; no year-specific differences were found for this operation. Runs synchronously on the caller's COM apartment. Calls through these new methods on one adapter are serialized; legacy/raw calls, other adapters and external edits are not locked out. Count/activation checks detect observed changes but cannot prove an atomic snapshot or detect same-count replacements. Dispose/rebind invalidates the captured binding. No load assignment, save, solver invocation, retry, rollback or units conversion is performed; any factors are dimensionless. Activates the requested primary case and checks GetActiveLoad before and after retrieval. Activation failure stops the query. Observed activation mismatch returns StateChanged; -1 from the active-number check is NativeFailure. The target remains active; previous activation is not restored, including after failure. A numeric active-case check cannot prove the family against an external switch to a reference case with the same number.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A number/index/limit or command-kind request is outside its documented domain.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed or a native response has an unsupported type, undocumented code, invalid array shape or unwritten/nonfinite data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The native interface lacks a compatible reviewed member.</exception>
        /// <example><code>var result = load.GetLoadCommandCount(17, LoadCommandKind.Nodal);
        /// if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// else Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public LoadQueryResult<int> GetLoadCommandCount(int primaryLoadCaseId, LoadCommandKind commandKind) =>
            inspection.GetLoadCommandCount(primaryLoadCaseId, commandKind);

        /// <summary>Counts entities assigned to one command of a specified native kind.</summary>
        /// <param name="primaryLoadCaseId">Positive primary load-case number. Activates this primary case with SetLoadActive; reference-family activation is not supported by this overload.</param>
        /// <param name="commandKind">Defined LoadCommandKind command-table value, not the engineering classification of a case.</param>
        /// <param name="commandIndex">Zero-based index within the specified native command kind; must be nonnegative.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; failed, ambiguous or changed observations expose no payload. NativeFunction and NativeReturnCode identify the decisive native response, including preflight calls.</returns>
        /// <remarks><para>Validates the zero-based command index against GetLoadTypeCount and rechecks that count afterwards. A zero command count is EmptyOrUnavailable; an index beyond a positive count is NotFound. A valid entity count can be zero. This returns a count, not the assignment-ID list.</para><para>Uses the shared installed 2024–2026 contract; no year-specific differences were found for this operation. Runs synchronously on the caller's COM apartment. Calls through these new methods on one adapter are serialized; legacy/raw calls, other adapters and external edits are not locked out. Count/activation checks detect observed changes but cannot prove an atomic snapshot or detect same-count replacements. Dispose/rebind invalidates the captured binding. No load assignment, save, solver invocation, retry, rollback or units conversion is performed; any factors are dimensionless. Activates the requested primary case and checks GetActiveLoad before and after retrieval. Activation failure stops the query. Observed activation mismatch returns StateChanged; -1 from the active-number check is NativeFailure. The target remains active; previous activation is not restored, including after failure. A numeric active-case check cannot prove the family against an external switch to a reference case with the same number.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A number/index/limit or command-kind request is outside its documented domain.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed or a native response has an unsupported type, undocumented code, invalid array shape or unwritten/nonfinite data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The native interface lacks a compatible reviewed member.</exception>
        /// <example><code>var result = load.GetAssignedEntityCount(17, LoadCommandKind.Nodal, 0);
        /// if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// else Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public LoadQueryResult<int> GetAssignedEntityCount(int primaryLoadCaseId, LoadCommandKind commandKind, int commandIndex) =>
            inspection.GetAssignedEntityCount(primaryLoadCaseId, commandKind, commandIndex);

        /// <summary>Counts original native load items in a specified load case.</summary>
        /// <param name="loadCaseId">Positive native case number passed directly to STAAD; its accepted family/suitability is determined by the native API.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; failed, ambiguous or changed observations expose no payload. NativeFunction and NativeReturnCode identify the decisive native response, including preflight calls.</returns>
        /// <remarks><para>Zero is valid and -1 is NativeFailure. Passes the case number directly without activation or family inference. Counts native items rather than per-entity duplicates or grouped managed definitions.</para><para>Uses the shared installed 2024–2026 contract; no year-specific differences were found for this operation. Runs synchronously on the caller's COM apartment. Calls through these new methods on one adapter are serialized; legacy/raw calls, other adapters and external edits are not locked out. Count/activation checks detect observed changes but cannot prove an atomic snapshot or detect same-count replacements. Dispose/rebind invalidates the captured binding. No load assignment, save, solver invocation, retry, rollback or units conversion is performed; any factors are dimensionless.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A number/index/limit or command-kind request is outside its documented domain.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed or a native response has an unsupported type, undocumented code, invalid array shape or unwritten/nonfinite data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The native interface lacks a compatible reviewed member.</exception>
        /// <example><code>var result = load.GetLoadItemCount(17);
        /// if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// else Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public LoadQueryResult<int> GetLoadItemCount(int loadCaseId) =>
            inspection.GetLoadItemCount(loadCaseId);

        /// <summary>Reads the original native kind at a zero-based load-item index.</summary>
        /// <param name="loadCaseId">Positive native case number passed directly to STAAD; its accepted family/suitability is determined by the native API.</param>
        /// <param name="loadItemIndex">Zero-based original native item index within the specified case; must be nonnegative.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; failed, ambiguous or changed observations expose no payload. NativeFunction and NativeReturnCode identify the decisive native response, including preflight calls.</returns>
        /// <remarks><para>Native zero is NotFound. Unknown positive codes are retained in the enum value; use Enum.IsDefined to test recognition. Code 3312 remains explicitly ambiguous between region nodal and partial plate pressure. Negative codes are malformed. No activation or family inference occurs.</para><para>Uses the shared installed 2024–2026 contract; no year-specific differences were found for this operation. Runs synchronously on the caller's COM apartment. Calls through these new methods on one adapter are serialized; legacy/raw calls, other adapters and external edits are not locked out. Count/activation checks detect observed changes but cannot prove an atomic snapshot or detect same-count replacements. Dispose/rebind invalidates the captured binding. No load assignment, save, solver invocation, retry, rollback or units conversion is performed; any factors are dimensionless.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A number/index/limit or command-kind request is outside its documented domain.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed or a native response has an unsupported type, undocumented code, invalid array shape or unwritten/nonfinite data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The native interface lacks a compatible reviewed member.</exception>
        /// <example><code>var result = load.GetLoadItemKind(17, 0);
        /// if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// else Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public LoadQueryResult<LoadItemKind> GetLoadItemKind(int loadCaseId, int loadItemIndex) =>
            inspection.GetLoadItemKind(loadCaseId, loadItemIndex);

        /// <summary>Observes whether the specified case includes dynamic loading.</summary>
        /// <param name="loadCaseId">Positive native case number passed directly to STAAD; its accepted family/suitability is determined by the native API.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; failed, ambiguous or changed observations expose no payload. NativeFunction and NativeReturnCode identify the decisive native response, including preflight calls.</returns>
        /// <remarks><para>Native 1/0 means true/false successful data; -1 is NativeFailure, never Boolean true. Actual Boolean native returns are also accepted. Does not classify a case from its title or engineering type, activate it, or start analysis.</para><para>Uses the shared installed 2024–2026 contract; no year-specific differences were found for this operation. Runs synchronously on the caller's COM apartment. Calls through these new methods on one adapter are serialized; legacy/raw calls, other adapters and external edits are not locked out. Count/activation checks detect observed changes but cannot prove an atomic snapshot or detect same-count replacements. Dispose/rebind invalidates the captured binding. No load assignment, save, solver invocation, retry, rollback or units conversion is performed; any factors are dimensionless.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A number/index/limit or command-kind request is outside its documented domain.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed or a native response has an unsupported type, undocumented code, invalid array shape or unwritten/nonfinite data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The native interface lacks a compatible reviewed member.</exception>
        /// <example><code>var result = load.ContainsDynamicLoad(17);
        /// if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// else Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public LoadQueryResult<bool> ContainsDynamicLoad(int loadCaseId) =>
            inspection.ContainsDynamicLoad(loadCaseId);

        /// <summary>Counts case/factor pairs in one repeat command.</summary>
        /// <param name="primaryLoadCaseId">Positive primary load-case number. Activates this primary case with SetLoadActive; reference-family activation is not supported by this overload.</param>
        /// <param name="repeatItemIndex">One-based repeat-command index in the selected primary case; must be positive.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; failed, ambiguous or changed observations expose no payload. NativeFunction and NativeReturnCode identify the decisive native response, including preflight calls.</returns>
        /// <remarks><para>Uses the documented one-based repeat index. Zero is a successful empty count and -1 is NotFound for an invalid repeat index. This is a command contribution count, not the number of cases containing repeat loads.</para><para>Uses the shared installed 2024–2026 contract; no year-specific differences were found for this operation. Runs synchronously on the caller's COM apartment. Calls through these new methods on one adapter are serialized; legacy/raw calls, other adapters and external edits are not locked out. Count/activation checks detect observed changes but cannot prove an atomic snapshot or detect same-count replacements. Dispose/rebind invalidates the captured binding. No load assignment, save, solver invocation, retry, rollback or units conversion is performed; any factors are dimensionless. Activates the requested primary case and checks GetActiveLoad before and after retrieval. Activation failure stops the query. Observed activation mismatch returns StateChanged; -1 from the active-number check is NativeFailure. The target remains active; previous activation is not restored, including after failure. A numeric active-case check cannot prove the family against an external switch to a reference case with the same number.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A number/index/limit or command-kind request is outside its documented domain.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed or a native response has an unsupported type, undocumented code, invalid array shape or unwritten/nonfinite data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The native interface lacks a compatible reviewed member.</exception>
        /// <example><code>var result = load.GetRepeatContributionCount(17, 1);
        /// if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// else Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public LoadQueryResult<int> GetRepeatContributionCount(int primaryLoadCaseId, int repeatItemIndex) =>
            inspection.GetRepeatContributionCount(primaryLoadCaseId, repeatItemIndex);

        /// <summary>Reads ordered case/factor pairs for a repeat command.</summary>
        /// <param name="primaryLoadCaseId">Positive primary load-case number. Activates this primary case with SetLoadActive; reference-family activation is not supported by this overload.</param>
        /// <param name="repeatItemIndex">One-based repeat-command index in the selected primary case; must be positive.</param>
        /// <param name="maximumResults">Positive retrieval limit (default 100000). A larger native count returns LimitExceeded before allocating output buffers; no silent truncation occurs.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; failed, ambiguous or changed observations expose no payload. NativeFunction and NativeReturnCode identify the decisive native response, including preflight calls.</returns>
        /// <remarks><para>Sizes Int32 ID and Double factor buffers with GetNoLoadFactorInRepeatLoad; checks native returned size and a post-fetch count. Preserves duplicate positive primary IDs and finite signed/zero factors in an immutable materialized sequence. Zero pairs returns an empty sequence without fetching; -1 is NotFound. No dictionary deduplication or summation occurs.</para><para>Uses the shared installed 2024–2026 contract; no year-specific differences were found for this operation. Runs synchronously on the caller's COM apartment. Calls through these new methods on one adapter are serialized; legacy/raw calls, other adapters and external edits are not locked out. Count/activation checks detect observed changes but cannot prove an atomic snapshot or detect same-count replacements. Dispose/rebind invalidates the captured binding. No load assignment, save, solver invocation, retry, rollback or units conversion is performed; any factors are dimensionless. Activates the requested primary case and checks GetActiveLoad before and after retrieval. Activation failure stops the query. Observed activation mismatch returns StateChanged; -1 from the active-number check is NativeFailure. The target remains active; previous activation is not restored, including after failure. A numeric active-case check cannot prove the family against an external switch to a reference case with the same number.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A number/index/limit or command-kind request is outside its documented domain.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed or a native response has an unsupported type, undocumented code, invalid array shape or unwritten/nonfinite data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The native interface lacks a compatible reviewed member.</exception>
        /// <example><code>var result = load.GetRepeatContributions(17, 1);
        /// if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// else Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public LoadQueryResult<IEnumerable<LoadFactorContribution>> GetRepeatContributions(int primaryLoadCaseId, int repeatItemIndex, int maximumResults = 100000) =>
            inspection.GetRepeatContributions(primaryLoadCaseId, repeatItemIndex, maximumResults);

        /// <summary>Counts notional-load commands in the selected primary case.</summary>
        /// <param name="primaryLoadCaseId">Positive primary load-case number. Activates this primary case with SetLoadActive; reference-family activation is not supported by this overload.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; failed, ambiguous or changed observations expose no payload. NativeFunction and NativeReturnCode identify the decisive native response, including preflight calls.</returns>
        /// <remarks><para>Zero is valid and -1 is NativeFailure. Counts command items, not their constituent case/factor/direction contributions.</para><para>Uses the shared installed 2024–2026 contract; no year-specific differences were found for this operation. Runs synchronously on the caller's COM apartment. Calls through these new methods on one adapter are serialized; legacy/raw calls, other adapters and external edits are not locked out. Count/activation checks detect observed changes but cannot prove an atomic snapshot or detect same-count replacements. Dispose/rebind invalidates the captured binding. No load assignment, save, solver invocation, retry, rollback or units conversion is performed; any factors are dimensionless. Activates the requested primary case and checks GetActiveLoad before and after retrieval. Activation failure stops the query. Observed activation mismatch returns StateChanged; -1 from the active-number check is NativeFailure. The target remains active; previous activation is not restored, including after failure. A numeric active-case check cannot prove the family against an external switch to a reference case with the same number.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A number/index/limit or command-kind request is outside its documented domain.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed or a native response has an unsupported type, undocumented code, invalid array shape or unwritten/nonfinite data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The native interface lacks a compatible reviewed member.</exception>
        /// <example><code>var result = load.GetNotionalItemCount(17);
        /// if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// else Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public LoadQueryResult<int> GetNotionalItemCount(int primaryLoadCaseId) =>
            inspection.GetNotionalItemCount(primaryLoadCaseId);

        /// <summary>Counts contribution slots in a notional-load command.</summary>
        /// <param name="primaryLoadCaseId">Positive primary load-case number. Activates this primary case with SetLoadActive; reference-family activation is not supported by this overload.</param>
        /// <param name="notionalItemIndex">Positive one-based notional-command index following the installed examples; verify behavior on the acquired native build.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; failed, ambiguous or changed observations expose no payload. NativeFunction and NativeReturnCode identify the decisive native response, including preflight calls.</returns>
        /// <remarks><para>Forwards a positive, one-based index following the installed examples that address command 1. Zero is valid and -1 is NativeFailure. Interprets the result as the paired reader buffer size; the manual also calls it a factor without documenting a numeric factor payload. Live index/count semantics require qualification.</para><para>Uses the shared installed 2024–2026 contract; no year-specific differences were found for this operation. Runs synchronously on the caller's COM apartment. Calls through these new methods on one adapter are serialized; legacy/raw calls, other adapters and external edits are not locked out. Count/activation checks detect observed changes but cannot prove an atomic snapshot or detect same-count replacements. Dispose/rebind invalidates the captured binding. No load assignment, save, solver invocation, retry, rollback or units conversion is performed; any factors are dimensionless. Activates the requested primary case and checks GetActiveLoad before and after retrieval. Activation failure stops the query. Observed activation mismatch returns StateChanged; -1 from the active-number check is NativeFailure. The target remains active; previous activation is not restored, including after failure. A numeric active-case check cannot prove the family against an external switch to a reference case with the same number.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A number/index/limit or command-kind request is outside its documented domain.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed or a native response has an unsupported type, undocumented code, invalid array shape or unwritten/nonfinite data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The native interface lacks a compatible reviewed member.</exception>
        /// <example><code>var result = load.GetNotionalContributionCount(17, 1);
        /// if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// else Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public LoadQueryResult<int> GetNotionalContributionCount(int primaryLoadCaseId, int notionalItemIndex) =>
            inspection.GetNotionalContributionCount(primaryLoadCaseId, notionalItemIndex);

        /// <summary>Reads ordered notional case/factor/global-direction contributions.</summary>
        /// <param name="primaryLoadCaseId">Positive primary load-case number. Activates this primary case with SetLoadActive; reference-family activation is not supported by this overload.</param>
        /// <param name="notionalItemIndex">Positive one-based notional-command index following the installed examples; verify behavior on the acquired native build.</param>
        /// <param name="maximumResults">Positive retrieval limit (default 100000). A larger native count returns LimitExceeded before allocating output buffers; no silent truncation occurs.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; failed, ambiguous or changed observations expose no payload. NativeFunction and NativeReturnCode identify the decisive native response, including preflight calls.</returns>
        /// <remarks><para>Sizes paired Int32 ID/direction and Double factor buffers using GetNoLoadFactorDirectionInNotionalLoad. Positive native IDs mean primary cases and negative IDs reference cases; returns their family, positive ID and original signed ID. Global direction codes 1/2/3 are X/Y/Z, not LoadDirection local axes. Preserves order, duplicates and signed/zero factors. Zero contributions skips fetch; -1 is NativeFailure. One-based indexing follows installed examples and remains a live qualification requirement.</para><para>Uses the shared installed 2024–2026 contract; no year-specific differences were found for this operation. Runs synchronously on the caller's COM apartment. Calls through these new methods on one adapter are serialized; legacy/raw calls, other adapters and external edits are not locked out. Count/activation checks detect observed changes but cannot prove an atomic snapshot or detect same-count replacements. Dispose/rebind invalidates the captured binding. No load assignment, save, solver invocation, retry, rollback or units conversion is performed; any factors are dimensionless. Activates the requested primary case and checks GetActiveLoad before and after retrieval. Activation failure stops the query. Observed activation mismatch returns StateChanged; -1 from the active-number check is NativeFailure. The target remains active; previous activation is not restored, including after failure. A numeric active-case check cannot prove the family against an external switch to a reference case with the same number.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A number/index/limit or command-kind request is outside its documented domain.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed or a native response has an unsupported type, undocumented code, invalid array shape or unwritten/nonfinite data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The native interface lacks a compatible reviewed member.</exception>
        /// <example><code>var result = load.GetNotionalContributions(17, 1);
        /// if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// else Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public LoadQueryResult<IEnumerable<NotionalLoadContribution>> GetNotionalContributions(int primaryLoadCaseId, int notionalItemIndex, int maximumResults = 100000) =>
            inspection.GetNotionalContributions(primaryLoadCaseId, notionalItemIndex, maximumResults);

        /// <summary>Counts reference-load command items inside the selected primary case.</summary>
        /// <param name="primaryLoadCaseId">Positive primary load-case number. Activates this primary case with SetLoadActive; reference-family activation is not supported by this overload.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; failed, ambiguous or changed observations expose no payload. NativeFunction and NativeReturnCode identify the decisive native response, including preflight calls.</returns>
        /// <remarks><para>Zero is valid and -1 is NativeFailure. Does not count reference-case definitions; use existing GetAllReferenceLoadCases for that separate inventory.</para><para>Uses the shared installed 2024–2026 contract; no year-specific differences were found for this operation. Runs synchronously on the caller's COM apartment. Calls through these new methods on one adapter are serialized; legacy/raw calls, other adapters and external edits are not locked out. Count/activation checks detect observed changes but cannot prove an atomic snapshot or detect same-count replacements. Dispose/rebind invalidates the captured binding. No load assignment, save, solver invocation, retry, rollback or units conversion is performed; any factors are dimensionless. Activates the requested primary case and checks GetActiveLoad before and after retrieval. Activation failure stops the query. Observed activation mismatch returns StateChanged; -1 from the active-number check is NativeFailure. The target remains active; previous activation is not restored, including after failure. A numeric active-case check cannot prove the family against an external switch to a reference case with the same number.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A number/index/limit or command-kind request is outside its documented domain.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed or a native response has an unsupported type, undocumented code, invalid array shape or unwritten/nonfinite data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The native interface lacks a compatible reviewed member.</exception>
        /// <example><code>var result = load.GetReferenceItemCount(17);
        /// if (result.IsSuccessful) Console.WriteLine(result.Data);
        /// else Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public LoadQueryResult<int> GetReferenceItemCount(int primaryLoadCaseId) =>
            inspection.GetReferenceItemCount(primaryLoadCaseId);

        #endregion

        #region Remaining Seismic, Spectrum and Direct Analysis Reference Operations

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCompletion15.1']/*" />
        public bool AddSeismicDefMemberWeight(int seismicCode, SeismicMemberLoadType loadType, double weight,
            double startDistance, double endDistance, IEnumerable<Beam> members)
        {
            if (members == null) throw new ArgumentNullException(nameof(members));
            var snapshot = members.ToArray();
            if (snapshot.Any(member => member == null)) throw new ArgumentException("Members cannot contain null.", nameof(members));
            return AddSeismicDefMemberWeight(seismicCode, loadType, weight, startDistance, endDistance, snapshot.Select(member => member.Id));
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCompletion15.2']/*" />
        public bool AddSeismicDefMemberUniformWeight(double weight, IEnumerable<int> memberIds,
            double startDistance = 0, double endDistance = 0, int seismicCode = 0) =>
            AddSeismicDefMemberWeight(seismicCode, SeismicMemberLoadType.Uniform, weight, startDistance, endDistance, memberIds);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCompletion15.3']/*" />
        public bool AddSeismicDefMemberConcentratedWeight(double weight, double distance, IEnumerable<int> memberIds, int seismicCode = 0) =>
            AddSeismicDefMemberWeight(seismicCode, SeismicMemberLoadType.Concentrated, weight, distance, 0, memberIds);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCompletion15.4']/*" />
        public int AddSeismicDefWallArea(int seismicCode, string direction, IEnumerable<double> dimensions)
        {
            if (seismicCode != 15) throw new ArgumentOutOfRangeException(nameof(seismicCode), "The reviewed wall-area table uses IS1893-2016 wall code 15, not definition code 12.");
            if (direction == null) throw new ArgumentNullException(nameof(direction));
            string nativeDirection = direction.Trim().ToUpperInvariant();
            if (nativeDirection != "X" && nativeDirection != "Z") throw new ArgumentException("Choose global X or Z.", nameof(direction));
            if (dimensions == null) throw new ArgumentNullException(nameof(dimensions));
            var values = dimensions.ToArray();
            if (values.Length % 2 != 0) throw new ArgumentException("Supply length/thickness pairs.", nameof(dimensions));
            foreach (double value in values)
            {
                LoadArguments.Finite(value, nameof(dimensions));
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(dimensions), "Wall lengths and thicknesses must be positive.");
            }
            if (values.Length == 0) return 0;
            return NativeValues.ReadInt32((object)ComObject.AddSeismicDefWallArea(seismicCode, nativeDirection, values), "AddSeismicDefWallArea status");
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCompletion15.5']/*" />
        public int AddSeismicDefWallArea(int seismicCode, SeismicWallDirection direction, IEnumerable<SeismicWallDimension> walls)
        {
            LoadArguments.Defined(direction, nameof(direction));
            if (walls == null) throw new ArgumentNullException(nameof(walls));
            var snapshot = walls.ToArray();
            var dimensions = new double[checked(snapshot.Length * 2)];
            for (int i = 0; i < snapshot.Length; i++)
            {
                if (snapshot[i] == null) throw new ArgumentException("Walls cannot contain null.", nameof(walls));
                dimensions[2 * i] = snapshot[i].Length;
                dimensions[2 * i + 1] = snapshot[i].Thickness;
            }
            return AddSeismicDefWallArea(seismicCode, direction == SeismicWallDirection.X ? "X" : "Z", dimensions);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCompletion15.6']/*" />
        public bool AddResponseSpectrumLoadEx(ResponseSpectrumLoadExParameters parameters)
        {
            var packet = SpectrumNativePacket.Extended(parameters);
            return SubmitExtendedSpectrum(packet);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCompletion15.7']/*" />
        public bool AddResponseSpectrumLoadEx(int spectrumCode, int combinationMethod, string[] primaryParameterNames,
            double[] primaryParameterValues, string[] generationParameterNames = null, double[] generationParameterValues = null,
            double[] customDataPairs = null)
        {
            var packet = SpectrumNativePacket.Extended(spectrumCode, combinationMethod, primaryParameterNames,
                primaryParameterValues, generationParameterNames, generationParameterValues, customDataPairs);
            return SubmitExtendedSpectrum(packet);
        }

        private bool SubmitExtendedSpectrum(SpectrumNativePacket packet) =>
            LoadArguments.ReadBoolean((object)ComObject.AddResponseSpectrumLoadEx(packet.Code, packet.Combination,
                packet.PrimaryNames, packet.PrimaryValues, packet.GenerationNames, packet.GenerationValues, packet.CustomPairs),
                "AddResponseSpectrumLoadEx");

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCompletion15.8']/*" />
        public bool AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType parameterType, IEnumerable<int> memberIds, double parameterValue = 0.0)
        {
            LoadArguments.Defined(parameterType, nameof(parameterType));
            LoadArguments.Finite(parameterValue, nameof(parameterValue));
            if (parameterType == DirectAnalysisParameterType.Axial && parameterValue != 0)
                throw new ArgumentException("AXIAL requires a zero unused scalar.", nameof(parameterValue));
            int[] ids = SnapshotPositiveLoadIds(memberIds, nameof(memberIds));
            if (ids.Length == 0) return true;
            return LoadArguments.ReadBoolean((object)ComObject.AddDirectAnalysisDefinitionParameter((int)parameterType, ids, parameterValue),
                "AddDirectAnalysisDefinitionParameter");
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCompletion15.9']/*" />
        public bool AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType parameterType, IEnumerable<Beam> members, double parameterValue = 0.0)
        {
            if (members == null) throw new ArgumentNullException(nameof(members));
            var snapshot = members.ToArray();
            if (snapshot.Any(member => member == null)) throw new ArgumentException("Members cannot contain null.", nameof(members));
            return AddDirectAnalysisDefinitionParameter(parameterType, snapshot.Select(member => member.Id), parameterValue);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCompletion15.10']/*" />
        public bool AddDirectAnalysisFlexParameter(double flexFactor, IEnumerable<int> memberIds) =>
            AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType.Flex, memberIds, flexFactor);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCompletion15.11']/*" />
        public bool AddDirectAnalysisAxialParameter(IEnumerable<int> memberIds) =>
            AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType.Axial, memberIds, 0.0);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCompletion15.12']/*" />
        public bool AddDirectAnalysisDefinitionParameter(DirectAnalysisParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (parameters.MemberIds == null) throw new ArgumentNullException(nameof(parameters.MemberIds));
            if (parameters.MemberIds.Count == 0) throw new ArgumentException("A model request requires at least one member ID.", nameof(parameters));
            return AddDirectAnalysisDefinitionParameter(parameters.ParameterType, parameters.MemberIds, parameters.ParameterValue);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCompletion15.13']/*" />
        public bool DeleteDirectAnalysisDefinitionParameter(DirectAnalysisParameterType parameterType)
        {
            LoadArguments.Defined(parameterType, nameof(parameterType));
            return LoadArguments.ReadBoolean((object)ComObject.DeleteDirectAnalysisDefinitionParameter((int)parameterType),
                "DeleteDirectAnalysisDefinitionParameter");
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCompletion15.14']/*" />
        public bool DeleteDirectAnalysisDefinition() =>
            LoadArguments.ReadBoolean((object)ComObject.DeleteDirectAnalysisDefinition(), "DeleteDirectAnalysisDefinition");

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadCompletion15.15']/*" />
        public bool AddRSLoad(ResponseSpectrumLoadParameters parameters)
        {
            var packet = SpectrumNativePacket.Legacy(parameters);
            // All nineteen slots are by value in both inspected 2025/2026 vendor interfaces.
            return LoadArguments.ReadBoolean((object)ComObject.AddRSLoad(packet.Code, packet.DirectionFactors, packet.Quantity,
                packet.Scale, packet.DampingType, packet.Damping, packet.Interpolation, packet.MissingMass,
                packet.MissingMassFactor, packet.Zpa, packet.ZpaFactor, packet.Ff1, packet.Ff1Factor, packet.Ff2,
                packet.Ff2Factor, packet.Save, packet.Periods.Length, packet.Periods, packet.Ordinates), "AddRSLoad");
        }

        #endregion

        #region ASCE Wind and Seismic Definitions

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadDefinitions15.1']/*" />
        public int AddWindDefinitionASCE7Parameters(ASCE7WindDefinitionParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            parameters.Validate();
            var packet = WindNativePacket.Capture(parameters.StructureData, parameters.Units, parameters.EscarpmentData, parameters.Publication, false);
            int[] flags = parameters.Factors.ToUserInputFlagsArray();
            double[] factors = parameters.Factors.ToFactorValuesArray();
            return NativeValues.ReadInt32((object)ComObject.AddWindDefinitionASCE7Parameters(parameters.TypeNumber,
                (int)parameters.Publication, parameters.WindSpeed, parameters.HeightAboveSeaLevel,
                (int)parameters.BuildingClassification, packet.StructureType, (int)parameters.ExposureCategory,
                parameters.ConsiderEscarpment ? 1 : 0, (int)parameters.WallType, parameters.IsFlexible ? 1 : 0,
                packet.Escarpment, packet.Structure, packet.Units, flags, factors), "AddWindDefinitionASCE7Parameters status");
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadDefinitions15.2']/*" />
        public int ComputeWallWindPressureProfile(WallWindPressureProfileParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            parameters.Validate();
            var packet = WindNativePacket.Capture(parameters.StructureData, parameters.Units, parameters.EscarpmentData,
                ASCE7Publication.ASCE7_05_10, true);
            return NativeValues.ReadInt32((object)ComObject.ComputeWallWindPressureProfile((int)parameters.LoadingCode,
                parameters.WindSpeed, (int)parameters.BuildingClassification, packet.StructureType,
                (int)parameters.ExposureCategory, parameters.ConsiderEscarpment ? 1 : 0, packet.Units, packet.Escarpment,
                packet.Structure, (int)parameters.WallType), "ComputeWallWindPressureProfile result");
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadDefinitions15.3']/*" />
        public int ComputeWallWindPressureProfileASCE72016(ASCE72016WallWindPressureProfileParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            parameters.Validate();
            var packet = WindNativePacket.Capture(parameters.StructureData, parameters.Units, parameters.EscarpmentData,
                ASCE7Publication.ASCE7_16, true);
            return NativeValues.ReadInt32((object)ComObject.ComputeWallWindPressureProfileASCE72016(parameters.WindSpeed,
                parameters.HeightAboveSeaLevel, (int)parameters.BuildingClassification, packet.StructureType,
                (int)parameters.ExposureCategory, parameters.ConsiderEscarpment ? 1 : 0, packet.Units, packet.Escarpment,
                packet.Structure, (int)parameters.WallType), "ComputeWallWindPressureProfileASCE72016 result");
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadDefinitions15.4']/*" />
        public bool AddSeismicDefinition(SeismicCode code, bool considerAccidentalTorsion = false) =>
            AddSeismicDefinition((int)code, considerAccidentalTorsion ? 1 : 0);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadDefinitions15.5']/*" />
        public bool AddSeismicDefinition(int code, int accidentalTorsion = 0)
        {
            if (!Enum.IsDefined(typeof(SeismicCode), code)) throw new ArgumentOutOfRangeException(nameof(code));
            if (accidentalTorsion != 0 && accidentalTorsion != 1) throw new ArgumentOutOfRangeException(nameof(accidentalTorsion));
            if (code == (int)SeismicCode.NRC2020)
            {
                var version = Wrapper.StaadVersion;
                LoadArguments.CoveredSeismicProfile(version);
                if (version.Major == 24) throw new NotSupportedException("NRC 2020 is not listed in the reviewed STAAD.Pro 2024 seismic definition contract.");
            }
            return LoadArguments.ReadBoolean((object)ComObject.AddSeismicDefinition(code, accidentalTorsion), "AddSeismicDefinition");
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadDefinitions15.6']/*" />
        public int ModifySeismicDefinitionParam(string parameterName, double value)
        {
            LoadArguments.Keyword(parameterName, nameof(parameterName));
            LoadArguments.Finite(value, nameof(value));
            return NativeValues.ReadInt32((object)ComObject.ModifySeismicDefinitionParams(parameterName, value), "ModifySeismicDefinitionParams status");
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadDefinitions15.7']/*" />
        public int ModifySeismicDefinitionParams(IDictionary<string, double> parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            var snapshot = parameters.ToArray();
            foreach (var parameter in snapshot)
            {
                LoadArguments.Keyword(parameter.Key, nameof(parameters));
                LoadArguments.Finite(parameter.Value, nameof(parameters));
            }
            foreach (var parameter in snapshot)
            {
                int status = ModifySeismicDefinitionParam(parameter.Key, parameter.Value);
                if (status != 0) return status;
            }
            return 0;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadDefinitions15.8']/*" />
        public int ModifySeismicDefinitionParams(IS1893_2016SeismicParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            return ModifySeismicDefinitionParams(parameters.ToParameterDictionary());
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadDefinitions15.9']/*" />
        public int ModifySeismicDefinitionParams(IS1893_2002SeismicParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            return ModifySeismicDefinitionParams(parameters.ToParameterDictionary());
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadDefinitions15.10']/*" />
        public int ModifySeismicDefinitionParams(IBCSeismicParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            return ModifySeismicDefinitionParams(parameters.ToParameterDictionary());
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadDefinitions15.11']/*" />
        public bool AddSeismicDefSelfWeight(double weightFactor = 1.0)
        {
            LoadArguments.Finite(weightFactor, nameof(weightFactor));
            return LoadArguments.ReadBoolean((object)ComObject.AddSeismicDefSelfWeight(weightFactor), "AddSeismicDefSelfWeight");
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadDefinitions15.12']/*" />
        public int AddSeismicDefJointWeight(double weight, IEnumerable<int> nodeIds)
        {
            LoadArguments.Finite(weight, nameof(weight));
            int[] ids = SnapshotPositiveLoadIds(nodeIds, nameof(nodeIds));
            if (ids.Length == 0) return 0;
            return NativeValues.ReadInt32((object)ComObject.AddSeismicDefJointWeight(weight, ids), "AddSeismicDefJointWeight status");
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadDefinitions15.13']/*" />
        public int AddSeismicDefJointWeight(double weight, IEnumerable<Node> nodes)
        {
            if (nodes == null) throw new ArgumentNullException(nameof(nodes));
            var snapshot = nodes.ToArray();
            if (snapshot.Any(node => node == null)) throw new ArgumentException("Nodes cannot contain null.", nameof(nodes));
            return AddSeismicDefJointWeight(weight, snapshot.Select(node => node.Id));
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadDefinitions15.14']/*" />
        public int AddSeismicDefJointWeight(double weight, int nodeId) => AddSeismicDefJointWeight(weight, new[] { nodeId });

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadDefinitions15.15']/*" />
        public bool AddSeismicDefMemberWeight(int seismicCode, SeismicMemberLoadType loadType, double weight,
            double startDistance, double endDistance, IEnumerable<int> memberIds)
        {
            LoadArguments.SeismicMemberCode(seismicCode, Wrapper.StaadVersion);
            LoadArguments.Defined(loadType, nameof(loadType));
            LoadArguments.Finite(weight, nameof(weight));
            LoadArguments.Finite(startDistance, nameof(startDistance));
            LoadArguments.Finite(endDistance, nameof(endDistance));
            if (startDistance < 0) throw new ArgumentOutOfRangeException(nameof(startDistance));
            if (endDistance < 0 || (endDistance != 0 && endDistance < startDistance))
                throw new ArgumentOutOfRangeException(nameof(endDistance));
            int[] ids = SnapshotPositiveLoadIds(memberIds, nameof(memberIds));
            if (ids.Length == 0) return true;
            return LoadArguments.ReadBoolean((object)ComObject.AddSeismicDefMemberWeight(seismicCode, (int)loadType,
                weight, startDistance, endDistance, ids), "AddSeismicDefMemberWeight");
        }

        #endregion

        #region Load Population and Wind Definitions

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadNext12.PopulateMemberUniformlyDistributedLoads']/*" />
        public void PopulateMemberUniformlyDistributedLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)
        {
            PopulateMemberLoadsCore(SnapshotPopulationEntities(forBeams, beam => beam.Id, nameof(forBeams)),
                SnapshotPopulationCases(fromLoadCases), GetMemberUniformlyDistributedLoads);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadNext12.PopulateMemberUniformMoments']/*" />
        public void PopulateMemberUniformMoments(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)
        {
            PopulateMemberLoadsCore(SnapshotPopulationEntities(forBeams, beam => beam.Id, nameof(forBeams)),
                SnapshotPopulationCases(fromLoadCases), GetMemberUniformMoments);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadNext12.PopulateNodalLoads']/*" />
        public IEnumerable<Node> PopulateNodalLoads(IEnumerable<Node> forNodes, IEnumerable<ILoadCase> fromLoadCases)
        {
            var nodes = SnapshotPopulationEntities(forNodes, node => node.Id, nameof(forNodes));
            var cases = SnapshotPopulationCases(fromLoadCases);
            PopulateNodalLoadsCore(nodes, cases);
            return nodes;
        }

        private void PopulateNodalLoadsCore(List<Node> nodes, List<ILoadCase> cases)
        {
            foreach (var node in nodes) node.NodalLoadItems.Clear();
            foreach (var loadCase in cases)
                foreach (var node in nodes) node.NodalLoadItems.AddRange(GetNodalLoads(loadCase, node.Id));
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadNext12.AddWindDefinition']/*" />
        public int AddWindDefinition(int windDefinitionId, string definitionName)
        {
            ValidateWindDefinitionId(windDefinitionId);
            if (definitionName == null) throw new ArgumentNullException(nameof(definitionName));
            if (string.IsNullOrWhiteSpace(definitionName) || definitionName.IndexOf('\0') >= 0)
                throw new ArgumentException("A wind definition requires nonblank text without a NUL character.", nameof(definitionName));
            return NativeValues.ReadInt32((object)ComObject.AddWindDefinition(windDefinitionId, definitionName), "AddWindDefinition status");
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadNext12.AddWindExposureIds']/*" />
        public int AddWindExposure(int windDefinitionId, double exposureFactor, IEnumerable<int> nodeIds)
        {
            ValidateWindDefinitionId(windDefinitionId);
            ValidateWindNonnegative(exposureFactor, nameof(exposureFactor));
            if (nodeIds == null) throw new ArgumentNullException(nameof(nodeIds));
            var ids = nodeIds.ToArray();
            if (ids.Length == 0) throw new ArgumentException("At least one node ID is required.", nameof(nodeIds));
            if (ids.Any(id => id <= 0)) throw new ArgumentOutOfRangeException(nameof(nodeIds), "Node IDs must be positive.");
            return NativeValues.ReadInt32((object)ComObject.AddWindExposure(windDefinitionId, exposureFactor, ids), "AddWindExposure status");
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadNext12.AddWindExposureNodes']/*" />
        public int AddWindExposure(int windDefinitionId, double exposureFactor, IEnumerable<Node> nodes)
        {
            ValidateWindDefinitionId(windDefinitionId);
            ValidateWindNonnegative(exposureFactor, nameof(exposureFactor));
            if (nodes == null) throw new ArgumentNullException(nameof(nodes));
            var snapshot = nodes.ToList();
            if (snapshot.Any(node => node == null || node.Id <= 0))
                throw new ArgumentException("Nodes must be initialized with positive IDs.", nameof(nodes));
            if (snapshot.Count == 0) throw new ArgumentException("At least one node is required.", nameof(nodes));
            return AddWindExposure(windDefinitionId, exposureFactor, snapshot.Select(node => node.Id));
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadNext12.AddWindExposureNodeId']/*" />
        public int AddWindExposure(int windDefinitionId, double exposureFactor, int nodeId)
        {
            ValidateWindDefinitionId(windDefinitionId);
            ValidateWindNonnegative(exposureFactor, nameof(exposureFactor));
            if (nodeId <= 0) throw new ArgumentOutOfRangeException(nameof(nodeId));
            return AddWindExposure(windDefinitionId, exposureFactor, new[] { nodeId });
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadNext12.AddWindIntensityScalar']/*" />
        public int AddWindIntensity(int windDefinitionId, double intensity, double height)
        {
            ValidateWindDefinitionId(windDefinitionId);
            ValidateWindFinite(intensity, nameof(intensity));
            ValidateWindNonnegative(height, nameof(height));
            return NativeValues.ReadInt32((object)ComObject.AddWindIntensity(windDefinitionId, intensity, height), "AddWindIntensity status");
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadNext12.AddWindIntensityArrays']/*" />
        public int AddWindIntensity(int windDefinitionId, IEnumerable<double> intensities, IEnumerable<double> heights)
        {
            ValidateWindDefinitionId(windDefinitionId);
            if (intensities == null) throw new ArgumentNullException(nameof(intensities));
            if (heights == null) throw new ArgumentNullException(nameof(heights));
            var pressureValues = intensities.ToArray();
            var heightValues = heights.ToArray();
            if (pressureValues.Length == 0 || pressureValues.Length != heightValues.Length)
                throw new ArgumentException("Intensity and height sequences must have the same nonzero length.", nameof(heights));
            foreach (double value in pressureValues) ValidateWindFinite(value, nameof(intensities));
            foreach (double value in heightValues) ValidateWindNonnegative(value, nameof(heights));
            return NativeValues.ReadInt32((object)ComObject.AddWindIntensity(windDefinitionId, pressureValues, heightValues), "AddWindIntensity status");
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadNext12.AddWindIntensityPairs']/*" />
        public int AddWindIntensity(int windDefinitionId, IEnumerable<WindIntensityPair> pairs)
        {
            ValidateWindDefinitionId(windDefinitionId);
            if (pairs == null) throw new ArgumentNullException(nameof(pairs));
            var snapshot = pairs.ToList();
            return AddWindIntensity(windDefinitionId, snapshot.Select(pair => pair.Intensity), snapshot.Select(pair => pair.Height));
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadNext12.DeleteWindDefinition']/*" />
        public int DeleteWindDefinition(int windDefinitionId = 0)
        {
            if (windDefinitionId < 0) throw new ArgumentOutOfRangeException(nameof(windDefinitionId));
            return NativeValues.ReadInt32((object)ComObject.DeleteWindDefinition(windDefinitionId), "DeleteWindDefinition status");
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='LoadNext12.DeleteAllWindDefinitions']/*" />
        public int DeleteAllWindDefinitions() => DeleteWindDefinition(0);

        private static void ValidateWindDefinitionId(int windDefinitionId)
        {
            if (windDefinitionId <= 0) throw new ArgumentOutOfRangeException(nameof(windDefinitionId), "Wind definition IDs must be positive.");
        }

        private static void ValidateWindFinite(double value, string parameterName)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentOutOfRangeException(parameterName, "The value must be finite.");
        }

        private static void ValidateWindNonnegative(double value, string parameterName)
        {
            ValidateWindFinite(value, parameterName);
            if (value < 0) throw new ArgumentOutOfRangeException(parameterName, "The value must not be negative.");
        }

        #endregion

        #region Reference, Repeat and Temperature Assignments

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddReferenceLoadsTo']/*" />
        public IOSLoad AddReferenceLoadsTo(int loadCaseId, Dictionary<int, double> loadCaseNumberFactorPairs)
        {
            var pairs = ReadLoadCaseFactors(loadCaseId, loadCaseNumberFactorPairs);
            if (pairs.Length == 0) return this;
            ActivateQueryCase(new LoadCase(loadCaseId, string.Empty, LoadCaseType.PrimaryLoad));
            ComObject.AddReferenceLoad(pairs.Select(p => p.Key).ToArray(), pairs.Select(p => p.Value).ToArray());
            return this;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddRepeatLoadsTo']/*" />
        public IOSLoad AddRepeatLoadsTo(int loadCaseId, IDictionary<int, double> loadCaseNumberFactorPairs)
        {
            var pairs = ReadLoadCaseFactors(loadCaseId, loadCaseNumberFactorPairs);
            if (pairs.Length == 0) return this;
            ActivateQueryCase(new LoadCase(loadCaseId, string.Empty, LoadCaseType.PrimaryLoad));
            ComObject.AddRepeatLoad(pairs.Select(p => p.Key).ToArray(), pairs.Select(p => p.Value).ToArray());
            return this;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddAxialThermalGradientTo']/*" />
        public IOSLoad AddAxialThermalGradientTo(int loadCaseId, IEnumerable<int> entities, double axialThermalGradient)
        {
            if (loadCaseId <= 0) throw new ArgumentOutOfRangeException(nameof(loadCaseId), "The target case ID must be positive.");
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            if (double.IsNaN(axialThermalGradient) || double.IsInfinity(axialThermalGradient))
                throw new ArgumentOutOfRangeException(nameof(axialThermalGradient), "The thermal input must be finite.");
            int[] ids = entities.ToArray();
            if (ids.Any(id => id <= 0)) throw new ArgumentOutOfRangeException(nameof(entities), "Entity IDs must be positive.");
            if (ids.Length == 0) return this;
            ActivateQueryCase(new LoadCase(loadCaseId, string.Empty, LoadCaseType.PrimaryLoad));
            ComObject.AddTemperatureLoad(ids, axialThermalGradient, 0.0, 0.0);
            return this;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddAxialThermalGradientTo.ThreadCount']/*" />
        public IOSLoad AddAxialThermalGradientTo(int loadCaseId, IEnumerable<int> entities, double axialThermalGradient, int nThreads)
        {
            if (nThreads <= 0) throw new ArgumentOutOfRangeException(nameof(nThreads), "The thread count must be positive.");
            if (loadCaseId <= 0) throw new ArgumentOutOfRangeException(nameof(loadCaseId), "The target case ID must be positive.");
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            if (double.IsNaN(axialThermalGradient) || double.IsInfinity(axialThermalGradient))
                throw new ArgumentOutOfRangeException(nameof(axialThermalGradient), "The thermal input must be finite.");
            int[] ids = entities.ToArray();
            if (ids.Any(id => id <= 0)) throw new ArgumentOutOfRangeException(nameof(entities), "Entity IDs must be positive.");
            if (ids.Length == 0) return this;
            ActivateQueryCase(new LoadCase(loadCaseId, string.Empty, LoadCaseType.PrimaryLoad));
            // Keep shared-session COM access on the caller's apartment instead of worker threads.
            // Preserve the reference overload's one native scalar assignment per entity.
            foreach (int id in ids) ComObject.AddTemperatureLoad(id, axialThermalGradient, 0.0, 0.0);
            return this;
        }

        private static KeyValuePair<int, double>[] ReadLoadCaseFactors(int loadCaseId,
            IEnumerable<KeyValuePair<int, double>> loadCaseNumberFactorPairs)
        {
            if (loadCaseId <= 0) throw new ArgumentOutOfRangeException(nameof(loadCaseId), "The target case ID must be positive.");
            if (loadCaseNumberFactorPairs == null) throw new ArgumentNullException(nameof(loadCaseNumberFactorPairs));
            var pairs = loadCaseNumberFactorPairs.ToArray();
            if (pairs.Any(p => p.Key <= 0)) throw new ArgumentOutOfRangeException(nameof(loadCaseNumberFactorPairs), "Source case IDs must be positive.");
            if (pairs.Any(p => double.IsNaN(p.Value) || double.IsInfinity(p.Value)))
                throw new ArgumentException("Load factors must be finite.", nameof(loadCaseNumberFactorPairs));
            return pairs;
        }

        #endregion

        #region Grouped Load Assignment

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddMemberLoadsFromGroupsByLoadCase']/*" />
        public IOSLoad AddMemberLoadsFromGroupsByLoadCase(Dictionary<ILoadCase, List<(MemberLoad Load, List<Beam> Beams)>> grouped)
        {
            if (grouped == null) throw new ArgumentNullException(nameof(grouped));
            foreach (var kvp in grouped)
            {
                if (kvp.Value == null) throw new ArgumentException("A load-case group list cannot be null.", nameof(grouped));
                foreach (var (ml, members) in kvp.Value)
                {
                    if (members == null || members.Count == 0 || ml == null) continue;
                    AddMemberLoad(kvp.Key, members, ml);
                }
            }
            return this;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddNodalLoadsFromGroupsByLoadCase']/*" />
        public IOSLoad AddNodalLoadsFromGroupsByLoadCase(Dictionary<ILoadCase, List<(NodalLoad Load, List<Node> Nodes)>> grouped)
        {
            if (grouped == null) throw new ArgumentNullException(nameof(grouped));
            foreach (var kvp in grouped)
            {
                if (kvp.Value == null) throw new ArgumentException("A load-case group list cannot be null.", nameof(grouped));
                foreach (var (nl, nodes) in kvp.Value) AddNodalLoad(kvp.Key, nodes, nl);
            }
            return this;
        }

        #endregion

        #region Load Assignment Queries

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.PopulateMemberLinearVaryingLoads']/*" />
        public void PopulateMemberLinearVaryingLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)
        {
            PopulateMemberLoadsCore(SnapshotPopulationEntities(forBeams, b => b.Id, nameof(forBeams)), SnapshotPopulationCases(fromLoadCases), GetMemberLinearVaryingLoads);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.PopulateMemberLoads']/*" />
        public IEnumerable<Beam> PopulateMemberLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)
        {
            var beams = SnapshotPopulationEntities(forBeams, b => b.Id, nameof(forBeams));
            var cases = SnapshotPopulationCases(fromLoadCases);
            PopulateAllMemberLoadsCore(beams, cases);
            return beams;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.PopulateMemberTrapezoidalLoads']/*" />
        public void PopulateMemberTrapezoidalLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)
        {
            PopulateMemberLoadsCore(SnapshotPopulationEntities(forBeams, b => b.Id, nameof(forBeams)), SnapshotPopulationCases(fromLoadCases), GetMemberTrapezoidalLoads);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.GetRepeatLoadCases']/*" />
        public HashSet<ILoadCase> GetRepeatLoadCases()
        {
            var result = new HashSet<ILoadCase>();
            foreach (var primary in ReadAllLoadCases(LoadCaseType.PrimaryLoad))
            {
                ActivateQueryCase(primary);
                int count = ReadQueryCount((object)ComObject.GetRepeatLoadCount(), "GetRepeatLoadCount");
                if (count > 0) result.Add(new LoadCase(primary.Id, primary.Title, LoadCaseType.RepeatLoad, primary.Type));
            }
            return result;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.PopulateMemberConcentratedLoads']/*" />
        public void PopulateMemberConcentratedLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)
        {
            PopulateMemberLoadsCore(SnapshotPopulationEntities(forBeams, b => b.Id, nameof(forBeams)), SnapshotPopulationCases(fromLoadCases), GetMemberConcentratedLoads);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.PopulateMemberConcentratedMoments']/*" />
        public void PopulateMemberConcentratedMoments(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)
        {
            PopulateMemberLoadsCore(SnapshotPopulationEntities(forBeams, b => b.Id, nameof(forBeams)), SnapshotPopulationCases(fromLoadCases), GetMemberConcentratedMoments);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.GetLoadCombination']/*" />
        public StaadLoadCombination GetLoadCombination(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            int count = ReadQueryCount((object)ComObject.GetNoOfLoadAndFactorPairsForCombination(id), "GetNoOfLoadAndFactorPairsForCombination");
            if (count == 0) return null;
            if (count == int.MaxValue) throw new InvalidOperationException("Combination factor count is too large.");
            object title = ComObject.GetLoadCaseTitle(id);
            if (!(title is string text)) throw new InvalidOperationException("Native combination title is not a string.");
            object ids = new int[count], factors = CreateQueryBuffer(count + 1);
            RequireLoadMutationSuccess((object)ComObject.GetLoadAndFactorForCombination(id, ref ids, ref factors), "GetLoadAndFactorForCombination");
            int[] sourceIds = ReadQueryArray<int>(ids, count, "combination source IDs");
            if (!(factors is Array array) || array.Rank != 1 || array.GetType().GetElementType() != typeof(double) || (array.Length != count && array.Length != count + 1))
                throw new InvalidOperationException("Native combination factor array is malformed.");
            int lower = array.GetLowerBound(0);
            if (array.Length == count + 1 && !double.IsNaN((double)array.GetValue(lower + count)))
                throw new NotSupportedException("SRSS overall factors cannot be represented by StaadLoadCombination.");
            var values = new double[count];
            for (int i = 0; i < count; i++) values[i] = (double)array.GetValue(lower + i);
            values = ReadQueryValues(values, count, "combination factors");
            if (sourceIds.Any(n => n <= 0) || sourceIds.Distinct().Count() != count)
                throw new InvalidOperationException("Native source IDs must be positive and unique.");
            var primary = ReadAllLoadCases(LoadCaseType.PrimaryLoad).ToDictionary(c => c.Id);
            var result = new StaadLoadCombination(id, text);
            for (int i = 0; i < count; i++)
            {
                if (!primary.TryGetValue(sourceIds[i], out var source)) throw new InvalidOperationException("Combination source case was not found in the primary family.");
                result.Factors.Add(source, values[i]);
            }
            return result;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.GetMemberLoadsGroupedByLoadCase']/*" />
        public Dictionary<ILoadCase, List<(MemberLoad Load, List<Beam> Beams)>> GetMemberLoadsGroupedByLoadCase(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)
        {
            var beams = SnapshotPopulationEntities(forBeams, b => b.Id, nameof(forBeams));
            var cases = SnapshotPopulationCases(fromLoadCases);
            PopulateAllMemberLoadsCore(beams, cases);
            return beams.SelectMany(b => b.MemberLoadItems.Select(load => (Beam: b, Load: load)))
                .GroupBy(x => x.Load.LoadCase)
                .ToDictionary(g => g.Key, g => g.GroupBy(x => x.Load, new MemberLoadComparer())
                    .Select(sub => (Load: sub.Key, Beams: sub.Select(x => x.Beam).ToList())).SplitDuplicateBeams().ToList());
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.GetNodalLoadsGroupedByLoadCase']/*" />
        public Dictionary<ILoadCase, List<(NodalLoad Load, List<Node> Nodes)>> GetNodalLoadsGroupedByLoadCase(IEnumerable<Node> forNodes, IEnumerable<ILoadCase> fromLoadCases)
        {
            var nodes = SnapshotPopulationEntities(forNodes, n => n.Id, nameof(forNodes));
            var cases = SnapshotPopulationCases(fromLoadCases);
            PopulateNodalLoadsCore(nodes, cases);
            return nodes.SelectMany(n => n.NodalLoadItems.Select(load => (Node: n, Load: load)))
                .GroupBy(x => x.Load.LoadCase)
                .ToDictionary(g => g.Key, g => g.GroupBy(x => x.Load, new NodalLoadComparer())
                    .Select(sub => (Load: sub.Key, Nodes: sub.Select(x => x.Node).ToList())).SplitDuplicateNodes().ToList());
        }

        private static List<T> SnapshotPopulationEntities<T>(IEnumerable<T> entities, Func<T, int> id, string parameter) where T : class
        {
            if (entities == null) throw new ArgumentNullException(parameter);
            var result = entities.ToList();
            if (result.Any(e => e == null || id(e) <= 0)) throw new ArgumentException("Entities must be initialized with positive IDs.", parameter);
            return result;
        }

        private static List<ILoadCase> SnapshotPopulationCases(IEnumerable<ILoadCase> fromLoadCases)
        {
            if (fromLoadCases == null) throw new ArgumentNullException(nameof(fromLoadCases));
            var result = fromLoadCases.ToList();
            foreach (var lc in result) ValidateLoadQuery(lc, 1, nameof(fromLoadCases));
            return result;
        }

        private void PopulateMemberLoadsCore<T>(List<Beam> beams, List<ILoadCase> cases, Func<ILoadCase, int, List<T>> read) where T : MemberLoad
        {
            foreach (var lc in cases)
                foreach (var beam in beams) beam.MemberLoadItems.AddRange(read(lc, beam.Id));
        }

        private void PopulateAllMemberLoadsCore(List<Beam> beams, List<ILoadCase> cases)
        {
            foreach (var beam in beams) beam.MemberLoadItems.Clear();
            PopulateMemberLoadsCore(beams, cases, GetMemberConcentratedLoads);
            PopulateMemberLoadsCore(beams, cases, GetMemberConcentratedMoments);
            PopulateMemberLoadsCore(beams, cases, GetMemberUniformlyDistributedLoads);
            PopulateMemberLoadsCore(beams, cases, GetMemberUniformMoments);
            PopulateMemberLoadsCore(beams, cases, GetMemberLinearVaryingLoads);
            PopulateMemberLoadsCore(beams, cases, GetMemberTrapezoidalLoads);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.SelfWeightThreads']/*" />
        public IOSLoad AddSelfweightInXYZTo(int loadCaseId, IEnumerable<int> entities, int nThreads, SelfWeightDirection loadDirection = SelfWeightDirection.GlobalY, double factor = -1)
        {
            if (nThreads <= 0) throw new ArgumentOutOfRangeException(nameof(nThreads));
            if (entities == null) return this;
            return AddSelfweightInXYZTo(loadCaseId, LoadCaseType.PrimaryLoad, entities, loadDirection, factor);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreateEnvelopeCombinations']/*" />
        public IOSLoad CreateLoadEnvelopExt(int envelopId, LoadEnvelopType envelopType, IEnumerable<EditorLoadCombination> loadCombinations)
        {
            if (loadCombinations == null) throw new ArgumentNullException(nameof(loadCombinations));
            int[] ids = loadCombinations.Where(EditorLoadCombination.IsValid).Select(c => c.Id).ToArray();
            return CreateLoadEnvelopExt(envelopId, envelopType, ids);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreateLoadCombinations']/*" />
        public IOSLoad CreateLoadCombinations(IEnumerable<EditorLoadCombination> loadCombinations)
        {
            if (loadCombinations == null) return this;
            var definitions = new List<(EditorLoadCombination Definition, KeyValuePair<int, double>[] Factors)>();
            foreach (var combination in loadCombinations)
            {
                if (!EditorLoadCombination.IsValid(combination)) continue;
                var factors = combination.Factors.ToArray();
                if (combination.Id <= 0 || factors.Any(p => p.Key <= 0 || double.IsNaN(p.Value) || double.IsInfinity(p.Value)))
                    throw new ArgumentException("Combination and source IDs must be positive and factors finite.", nameof(loadCombinations));
                definitions.Add((combination, factors));
            }
            foreach (var definition in definitions)
            {
                ComObject.CreateNewLoadCombination(definition.Definition.Title, definition.Definition.Id);
                foreach (var pair in definition.Factors)
                    ComObject.AddLoadAndFactorToCombination(definition.Definition.Id, pair.Key, pair.Value);
            }
            return this;
        }

        /// <summary>
        /// Reads assigned nodal forces and moments for one node in an existing primary or reference case.
        /// </summary>
        /// <param name="lc">The existing case to activate, with a positive ID and CaseType of
        /// PrimaryLoad or ReferenceLoad. Title and Type are not sent to OpenSTAAD.</param>
        /// <param name="nId">The positive STAAD node number in the currently open model.
        /// This is an entity ID, not a zero-based index. Existence is checked by OpenSTAAD.</param>
        /// <returns>A newly allocated, non-null list in OpenSTAAD record order, with one new
        /// <see cref="NodalLoad"/> per assignment. Every LoadCase is the same object as
        /// <paramref name="lc"/>. A successful zero count returns an empty list.</returns>
        /// <remarks>
        /// <para>Each record maps the six arrays to Forces.Fx, Fy, Fz, Mx, My and Mz. Forces act along the reported X/Y/Z axes and moments act about them. Every Forces object is newly allocated. These are JOINT LOAD assignments, not reactions or analysis results.</para>
        /// <para>Signs and raw numeric values are preserved without summation, coordinate
        /// transformation, unit conversion or scaling. Confirm the installed API's read-back
        /// unit convention before combining values with input loads; input-unit metadata alone
        /// does not guarantee the units of returned values.</para>
        /// <para>The case is activated before counting and fetching and remains active afterward,
        /// including when a subsequent read fails. The previous active case is not restored.
        /// No load assignments are added, removed or changed. Use a connected, undisposed wrapper.
        /// Serialize all operations sharing the STAAD session on its appropriate COM thread;
        /// these calls do not provide thread safety or COM apartment marshaling.</para>
        /// <para>Failed activation, negative counts, nonzero fetch status, unexpected array
        /// shape/type/length, invalid directions, and non-finite or unwritten values throw
        /// instead of returning partial data. COM and dynamic binding failures propagate.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="lc"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The case ID or <paramref name="nId"/> is not positive.</exception>
        /// <exception cref="NotSupportedException">The case is a combination, repeat load, or unknown case type.</exception>
        /// <exception cref="InvalidOperationException">OpenSTAAD reports failure or returns malformed data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface does not expose a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load, and existing case/entity IDs:
        /// <code>
        /// var lc = new LoadCase(1, "DEAD", LoadCaseType.PrimaryLoad);
        /// var loads = load.GetNodalLoads(lc, nId: 10);
        /// foreach (var item in loads) Console.WriteLine(item.Forces.Fy);
        /// </code>
        /// </example>
        public List<NodalLoad> GetNodalLoads(ILoadCase lc, int nId)
        {
            ValidateLoadQuery(lc, nId, nameof(nId));
            ActivateQueryCase(lc);
            int count = ReadQueryCount((object)ComObject.GetNodalLoadCount(nId), "GetNodalLoadCount");
            var result = new List<NodalLoad>(count);
            if (count == 0) return result;

            object fx = CreateQueryBuffer(count), fy = CreateQueryBuffer(count), fz = CreateQueryBuffer(count);
            object mx = CreateQueryBuffer(count), my = CreateQueryBuffer(count), mz = CreateQueryBuffer(count);
            object status = ComObject.GetNodalLoads(nId, ref fx, ref fy, ref fz, ref mx, ref my, ref mz);
            RequireQuerySuccess(status, "GetNodalLoads");
            double[] xs = ReadQueryValues(fx, count, "Fx"), ys = ReadQueryValues(fy, count, "Fy"), zs = ReadQueryValues(fz, count, "Fz");
            double[] xms = ReadQueryValues(mx, count, "Mx"), yms = ReadQueryValues(my, count, "My"), zms = ReadQueryValues(mz, count, "Mz");
            for (int i = 0; i < count; i++)
                result.Add(new NodalLoad(xs[i], ys[i], zs[i], xms[i], yms[i], zms[i]) { LoadCase = lc });
            return result;
        }

        /// <summary>
        /// Reads assigned uniform member forces for one member in an existing primary or reference case.
        /// </summary>
        /// <param name="lc">The existing case to activate, with a positive ID and CaseType of
        /// PrimaryLoad or ReferenceLoad. Title and Type are not sent to OpenSTAAD.</param>
        /// <param name="mId">The positive STAAD member number in the currently open model.
        /// This is an entity ID, not a zero-based index. Existence is checked by OpenSTAAD.</param>
        /// <returns>A newly allocated, non-null list in OpenSTAAD record order, with one new
        /// <see cref="MemberUniformlyDistributedLoad"/> per assignment. Every LoadCase is the same object as
        /// <paramref name="lc"/>. A successful zero count returns an empty list.</returns>
        /// <remarks>
        /// <para>Direction preserves codes 1 through 9 (LocalX/Y/Z, GlobalX/Y/Z, ProjectedX/Y/Z). Magnitude is force per length. SPosition and EPosition are distances from the member start to the load start and end. Eccentricity is the perpendicular offset from the member shear center to the local loading plane. Zero start/end sentinels are preserved. This reads uniform forces, not uniform moments, trapezoidal loads or analysis results. Members is empty; no Beam entities are fetched.</para>
        /// <para>Signs and raw numeric values are preserved without summation, coordinate
        /// transformation, unit conversion or scaling. Confirm the installed API's read-back
        /// unit convention before combining values with input loads; input-unit metadata alone
        /// does not guarantee the units of returned values.</para>
        /// <para>The case is activated before counting and fetching and remains active afterward,
        /// including when a subsequent read fails. The previous active case is not restored.
        /// No load assignments are added, removed or changed. Use a connected, undisposed wrapper.
        /// Serialize all operations sharing the STAAD session on its appropriate COM thread;
        /// these calls do not provide thread safety or COM apartment marshaling.</para>
        /// <para>Failed activation, negative counts, nonzero fetch status, unexpected array
        /// shape/type/length, invalid directions, and non-finite or unwritten values throw
        /// instead of returning partial data. COM and dynamic binding failures propagate.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="lc"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The case ID or <paramref name="mId"/> is not positive.</exception>
        /// <exception cref="NotSupportedException">The case is a combination, repeat load, or unknown case type.</exception>
        /// <exception cref="InvalidOperationException">OpenSTAAD reports failure or returns malformed data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface does not expose a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load, and existing case/entity IDs:
        /// <code>
        /// var lc = new LoadCase(1, "DEAD", LoadCaseType.PrimaryLoad);
        /// var loads = load.GetMemberUniformlyDistributedLoads(lc, mId: 101);
        /// foreach (var item in loads) Console.WriteLine($"{item.Direction}: {item.Magnitude}, {item.SPosition}, {item.EPosition}");
        /// </code>
        /// </example>
        public List<MemberUniformlyDistributedLoad> GetMemberUniformlyDistributedLoads(ILoadCase lc, int mId)
        {
            ValidateLoadQuery(lc, mId, nameof(mId));
            ActivateQueryCase(lc);
            int count = ReadQueryCount((object)ComObject.GetUDLLoadCount(mId), "GetUDLLoadCount");
            var result = new List<MemberUniformlyDistributedLoad>(count);
            if (count == 0) return result;

            object direction = new int[count], force = CreateQueryBuffer(count), start = CreateQueryBuffer(count);
            object end = CreateQueryBuffer(count), eccentricity = CreateQueryBuffer(count);
            object status = ComObject.GetUDLLoads(mId, ref direction, ref force, ref start, ref end, ref eccentricity);
            RequireQuerySuccess(status, "GetUDLLoads");
            int[] directions = ReadQueryArray<int>(direction, count, "Direction");
            double[] forces = ReadQueryValues(force, count, "Magnitude"), starts = ReadQueryValues(start, count, "SPosition");
            double[] ends = ReadQueryValues(end, count, "EPosition"), offsets = ReadQueryValues(eccentricity, count, "Eccentricity");
            for (int i = 0; i < count; i++)
                result.Add(new MemberUniformlyDistributedLoad(ReadQueryDirection(directions[i], 9), forces[i], starts[i], ends[i], offsets[i]) { LoadCase = lc });
            return result;
        }

        /// <summary>
        /// Reads assigned concentrated member forces for one member in an existing primary or reference case.
        /// </summary>
        /// <param name="lc">The existing case to activate, with a positive ID and CaseType of
        /// PrimaryLoad or ReferenceLoad. Title and Type are not sent to OpenSTAAD.</param>
        /// <param name="mId">The positive STAAD member number in the currently open model.
        /// This is an entity ID, not a zero-based index. Existence is checked by OpenSTAAD.</param>
        /// <returns>A newly allocated, non-null list in OpenSTAAD record order, with one new
        /// <see cref="MemberConcentratedLoad"/> per assignment. Every LoadCase is the same object as
        /// <paramref name="lc"/>. A successful zero count returns an empty list.</returns>
        /// <remarks>
        /// <para>Direction preserves codes 1 through 6 (LocalX/Y/Z, GlobalX/Y/Z); projected directions are invalid. Magnitude is force. Position is the distance from the member start to the force. Eccentricity is the perpendicular offset from the member shear center to the local loading plane. This reads concentrated forces, not concentrated moments or analysis results. Members is empty; no Beam entities are fetched.</para>
        /// <para>Signs and raw numeric values are preserved without summation, coordinate
        /// transformation, unit conversion or scaling. Confirm the installed API's read-back
        /// unit convention before combining values with input loads; input-unit metadata alone
        /// does not guarantee the units of returned values.</para>
        /// <para>The case is activated before counting and fetching and remains active afterward,
        /// including when a subsequent read fails. The previous active case is not restored.
        /// No load assignments are added, removed or changed. Use a connected, undisposed wrapper.
        /// Serialize all operations sharing the STAAD session on its appropriate COM thread;
        /// these calls do not provide thread safety or COM apartment marshaling.</para>
        /// <para>Failed activation, negative counts, nonzero fetch status, unexpected array
        /// shape/type/length, invalid directions, and non-finite or unwritten values throw
        /// instead of returning partial data. COM and dynamic binding failures propagate.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="lc"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The case ID or <paramref name="mId"/> is not positive.</exception>
        /// <exception cref="NotSupportedException">The case is a combination, repeat load, or unknown case type.</exception>
        /// <exception cref="InvalidOperationException">OpenSTAAD reports failure or returns malformed data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface does not expose a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load, and existing case/entity IDs:
        /// <code>
        /// var lc = new LoadCase(101, "REFERENCE", LoadCaseType.ReferenceLoad);
        /// var loads = load.GetMemberConcentratedLoads(lc, mId: 101);
        /// foreach (var item in loads) Console.WriteLine($"{item.Direction}: {item.Magnitude} at {item.Position}");
        /// </code>
        /// </example>
        public List<MemberConcentratedLoad> GetMemberConcentratedLoads(ILoadCase lc, int mId)
        {
            ValidateLoadQuery(lc, mId, nameof(mId));
            ActivateQueryCase(lc);
            int count = ReadQueryCount((object)ComObject.GetConcForceCount(mId), "GetConcForceCount");
            var result = new List<MemberConcentratedLoad>(count);
            if (count == 0) return result;

            object direction = new int[count], force = CreateQueryBuffer(count);
            object position = CreateQueryBuffer(count), eccentricity = CreateQueryBuffer(count);
            object status = ComObject.GetConcForces(mId, ref direction, ref force, ref position, ref eccentricity);
            RequireQuerySuccess(status, "GetConcForces");
            int[] directions = ReadQueryArray<int>(direction, count, "Direction");
            double[] forces = ReadQueryValues(force, count, "Magnitude"), positions = ReadQueryValues(position, count, "Position");
            double[] offsets = ReadQueryValues(eccentricity, count, "Eccentricity");
            for (int i = 0; i < count; i++)
                result.Add(new MemberConcentratedLoad(ReadQueryDirection(directions[i], 6), forces[i], positions[i], offsets[i]) { LoadCase = lc });
            return result;
        }

        #endregion

        #region Additional Member Load Queries

        /// <summary>
        /// Reads assigned concentrated member moments for one member in an existing primary or reference case.
        /// </summary>
        /// <param name="lc">The existing case to activate, with a positive ID and CaseType of
        /// PrimaryLoad or ReferenceLoad. Title and Type are not sent to OpenSTAAD.</param>
        /// <param name="mId">The positive STAAD member number in the currently open model.
        /// This is an entity ID, not a zero-based index. Existence is checked by OpenSTAAD.</param>
        /// <returns>A newly allocated, non-null list in OpenSTAAD record order, with one new
        /// <see cref="MemberConcentratedMoment"/> per assignment. Every LoadCase is the same object as
        /// <paramref name="lc"/>. A successful zero count returns an empty list.</returns>
        /// <remarks>
        /// <para>Direction preserves codes 1 through 6 (LocalX/Y/Z, GlobalX/Y/Z), identifying the moment axis. Magnitude is moment (force times length). Position is the distance from the member start to the moment. Eccentricity is the perpendicular offset from the member shear center to the local loading plane. Projected directions are invalid. These are assigned moments, not concentrated forces or internal analysis moments. Members is empty; no Beam entities are fetched.</para>
        /// <para>Signs and raw numeric values are preserved without summation, coordinate
        /// transformation, unit conversion or scaling. Confirm the installed API's read-back
        /// unit convention before combining values with input loads; input-unit metadata alone
        /// does not guarantee the units of returned values.</para>
        /// <para>The case is activated before counting and fetching and remains active afterward,
        /// including when a subsequent read fails. The previous active case is not restored.
        /// No load assignments are added, removed or changed. Use a connected, undisposed wrapper.
        /// Serialize all operations sharing the STAAD session on its appropriate COM thread;
        /// these calls do not provide thread safety or COM apartment marshaling.</para>
        /// <para>Failed activation, negative counts, nonzero fetch status, unexpected array
        /// shape/type/length, invalid directions, and non-finite or unwritten values throw
        /// instead of returning partial data. COM and dynamic binding failures propagate.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="lc"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The case ID or <paramref name="mId"/> is not positive.</exception>
        /// <exception cref="NotSupportedException">The case is a combination, repeat load, or unknown case type.</exception>
        /// <exception cref="InvalidOperationException">OpenSTAAD reports failure or returns malformed data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface does not expose a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load, and existing case/entity IDs:
        /// <code>
        /// var lc = new LoadCase(101, "REFERENCE", LoadCaseType.ReferenceLoad);
        /// var loads = load.GetMemberConcentratedMoments(lc, mId: 101);
        /// foreach (var item in loads) Console.WriteLine($"{item.Direction}: moment={item.Magnitude}, position={item.Position}, offset={item.Eccentricity}");
        /// </code>
        /// </example>
        public List<MemberConcentratedMoment> GetMemberConcentratedMoments(ILoadCase lc, int mId)
        {
            ValidateLoadQuery(lc, mId, nameof(mId));
            ActivateQueryCase(lc);
            int count = ReadQueryCount((object)ComObject.GetConcMomentCount(mId), "GetConcMomentCount");
            var result = new List<MemberConcentratedMoment>(count);
            if (count == 0) return result;

            object direction = new int[count], moment = CreateQueryBuffer(count);
            object position = CreateQueryBuffer(count), eccentricity = CreateQueryBuffer(count);
            object status = ComObject.GetConcMoments(mId, ref direction, ref moment, ref position, ref eccentricity);
            RequireQuerySuccess(status, "GetConcMoments");
            int[] directions = ReadQueryArray<int>(direction, count, "Direction");
            double[] moments = ReadQueryValues(moment, count, "Magnitude"), positions = ReadQueryValues(position, count, "Position");
            double[] offsets = ReadQueryValues(eccentricity, count, "Eccentricity");
            for (int i = 0; i < count; i++)
                result.Add(new MemberConcentratedMoment(ReadQueryDirection(directions[i], 6), moments[i], positions[i], offsets[i]) { LoadCase = lc });
            return result;
        }

        /// <summary>
        /// Reads assigned linearly varying member forces for one member in an existing primary or reference case.
        /// </summary>
        /// <param name="lc">The existing case to activate, with a positive ID and CaseType of
        /// PrimaryLoad or ReferenceLoad. Title and Type are not sent to OpenSTAAD.</param>
        /// <param name="mId">The positive STAAD member number in the currently open model.
        /// This is an entity ID, not a zero-based index. Existence is checked by OpenSTAAD.</param>
        /// <returns>A newly allocated, non-null list in OpenSTAAD record order, with one new
        /// <see cref="MemberLinearVaryingLoad"/> per assignment. Every LoadCase is the same object as
        /// <paramref name="lc"/>. A successful zero count returns an empty list.</returns>
        /// <remarks>
        /// <para>Direction permits only codes 1 through 3 (LocalX/Y/Z). WStart, WEnd and WMiddle are signed force-per-length intensities; WMiddle is the middle intensity for triangular loading. Native output arrays are start/end/middle, while the model constructor accepts start/middle/end. All three are mapped to their named properties without interpolation. These are full-member varying force assignments, not partial-span trapezoidal loads or analysis results. Members is empty; no Beam entities are fetched.</para>
        /// <para>Signs and raw numeric values are preserved without summation, coordinate
        /// transformation, unit conversion or scaling. Confirm the installed API's read-back
        /// unit convention before combining values with input loads; input-unit metadata alone
        /// does not guarantee the units of returned values.</para>
        /// <para>The case is activated before counting and fetching and remains active afterward,
        /// including when a subsequent read fails. The previous active case is not restored.
        /// No load assignments are added, removed or changed. Use a connected, undisposed wrapper.
        /// Serialize all operations sharing the STAAD session on its appropriate COM thread;
        /// these calls do not provide thread safety or COM apartment marshaling.</para>
        /// <para>Failed activation, negative counts, nonzero fetch status, unexpected array
        /// shape/type/length, invalid directions, and non-finite or unwritten values throw
        /// instead of returning partial data. COM and dynamic binding failures propagate.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="lc"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The case ID or <paramref name="mId"/> is not positive.</exception>
        /// <exception cref="NotSupportedException">The case is a combination, repeat load, or unknown case type.</exception>
        /// <exception cref="InvalidOperationException">OpenSTAAD reports failure or returns malformed data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface does not expose a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load, and existing case/entity IDs:
        /// <code>
        /// var lc = new LoadCase(101, "REFERENCE", LoadCaseType.ReferenceLoad);
        /// var loads = load.GetMemberLinearVaryingLoads(lc, mId: 101);
        /// foreach (var item in loads) Console.WriteLine($"{item.Direction}: start={item.WStart}, middle={item.WMiddle}, end={item.WEnd}");
        /// </code>
        /// </example>
        public List<MemberLinearVaryingLoad> GetMemberLinearVaryingLoads(ILoadCase lc, int mId)
        {
            ValidateLoadQuery(lc, mId, nameof(mId));
            ActivateQueryCase(lc);
            int count = ReadQueryCount((object)ComObject.GetLinearVaryingLoadCount(mId), "GetLinearVaryingLoadCount");
            var result = new List<MemberLinearVaryingLoad>(count);
            if (count == 0) return result;

            object direction = new int[count], start = CreateQueryBuffer(count);
            object end = CreateQueryBuffer(count), middle = CreateQueryBuffer(count);
            // Native output order is start/end/middle; constructor order is start/middle/end.
            object status = ComObject.GetLinearVaryingLoads(mId, ref direction, ref start, ref end, ref middle);
            RequireQuerySuccess(status, "GetLinearVaryingLoads");
            int[] directions = ReadQueryArray<int>(direction, count, "Direction");
            double[] starts = ReadQueryValues(start, count, "WStart"), ends = ReadQueryValues(end, count, "WEnd");
            double[] middles = ReadQueryValues(middle, count, "WMiddle");
            for (int i = 0; i < count; i++)
                result.Add(new MemberLinearVaryingLoad(ReadQueryDirection(directions[i], 3), starts[i], middles[i], ends[i]) { LoadCase = lc });
            return result;
        }

        /// <summary>
        /// Reads assigned trapezoidal member forces for one member in an existing primary or reference case.
        /// </summary>
        /// <param name="lc">The existing case to activate, with a positive ID and CaseType of
        /// PrimaryLoad or ReferenceLoad. Title and Type are not sent to OpenSTAAD.</param>
        /// <param name="mId">The positive STAAD member number in the currently open model.
        /// This is an entity ID, not a zero-based index. Existence is checked by OpenSTAAD.</param>
        /// <returns>A newly allocated, non-null list in OpenSTAAD record order, with one new
        /// <see cref="MemberTrapezoidalLoad"/> per assignment. Every LoadCase is the same object as
        /// <paramref name="lc"/>. A successful zero count returns an empty list.</returns>
        /// <remarks>
        /// <para>Direction preserves codes 1 through 9 (LocalX/Y/Z, GlobalX/Y/Z, ProjectedX/Y/Z). WStart and WEnd are signed force-per-length intensities at the loading endpoints. SPosition and EPosition are distances from the member start to the loading start and end. Zero endpoint sentinels are preserved; no full-span expansion or interpolation is performed. This reads trapezoidal force assignments, not uniform forces, full-member linear/triangular definitions or analysis results. Members is empty; no Beam entities are fetched.</para>
        /// <para>Signs and raw numeric values are preserved without summation, coordinate
        /// transformation, unit conversion or scaling. Confirm the installed API's read-back
        /// unit convention before combining values with input loads; input-unit metadata alone
        /// does not guarantee the units of returned values.</para>
        /// <para>The case is activated before counting and fetching and remains active afterward,
        /// including when a subsequent read fails. The previous active case is not restored.
        /// No load assignments are added, removed or changed. Use a connected, undisposed wrapper.
        /// Serialize all operations sharing the STAAD session on its appropriate COM thread;
        /// these calls do not provide thread safety or COM apartment marshaling.</para>
        /// <para>Failed activation, negative counts, nonzero fetch status, unexpected array
        /// shape/type/length, invalid directions, and non-finite or unwritten values throw
        /// instead of returning partial data. COM and dynamic binding failures propagate.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="lc"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The case ID or <paramref name="mId"/> is not positive.</exception>
        /// <exception cref="NotSupportedException">The case is a combination, repeat load, or unknown case type.</exception>
        /// <exception cref="InvalidOperationException">OpenSTAAD reports failure or returns malformed data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface does not expose a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load, and existing case/entity IDs:
        /// <code>
        /// var lc = new LoadCase(101, "REFERENCE", LoadCaseType.ReferenceLoad);
        /// var loads = load.GetMemberTrapezoidalLoads(lc, mId: 101);
        /// foreach (var item in loads) Console.WriteLine($"{item.Direction}: {item.WStart} to {item.WEnd}, from {item.SPosition} to {item.EPosition}");
        /// </code>
        /// </example>
        public List<MemberTrapezoidalLoad> GetMemberTrapezoidalLoads(ILoadCase lc, int mId)
        {
            ValidateLoadQuery(lc, mId, nameof(mId));
            ActivateQueryCase(lc);
            int count = ReadQueryCount((object)ComObject.GetTrapLoadCount(mId), "GetTrapLoadCount");
            var result = new List<MemberTrapezoidalLoad>(count);
            if (count == 0) return result;

            object direction = new int[count], startForce = CreateQueryBuffer(count), endForce = CreateQueryBuffer(count);
            object startPosition = CreateQueryBuffer(count), endPosition = CreateQueryBuffer(count);
            object status = ComObject.GetTrapLoads(mId, ref direction, ref startForce, ref endForce, ref startPosition, ref endPosition);
            RequireQuerySuccess(status, "GetTrapLoads");
            int[] directions = ReadQueryArray<int>(direction, count, "Direction");
            double[] starts = ReadQueryValues(startForce, count, "WStart"), ends = ReadQueryValues(endForce, count, "WEnd");
            double[] startPositions = ReadQueryValues(startPosition, count, "SPosition"), endPositions = ReadQueryValues(endPosition, count, "EPosition");
            for (int i = 0; i < count; i++)
                result.Add(new MemberTrapezoidalLoad(ReadQueryDirection(directions[i], 9), starts[i], ends[i], startPositions[i], endPositions[i]) { LoadCase = lc });
            return result;
        }

        #endregion

        /// <summary>
        /// Reads assigned uniform member moments for one member in an existing primary or reference case.
        /// </summary>
        /// <param name="lc">The existing case to activate, with a positive ID and CaseType of
        /// PrimaryLoad or ReferenceLoad. Title and Type are not sent to OpenSTAAD.</param>
        /// <param name="mId">The positive STAAD member number in the currently open model.
        /// This is an entity ID, not a zero-based index. Existence is checked by OpenSTAAD.</param>
        /// <returns>A newly allocated, non-null list in OpenSTAAD record order, with one new
        /// <see cref="MemberUniformMoment"/> per assignment. Every LoadCase is the same object as
        /// <paramref name="lc"/>. A successful zero count returns an empty list.</returns>
        /// <remarks>
        /// <para>Direction preserves codes 1 through 9 (LocalX/Y/Z, GlobalX/Y/Z, ProjectedX/Y/Z). Magnitude is distributed moment per length, for example kN-m/m. SPosition and EPosition are distances from the member start to the load start and end. Eccentricity is the perpendicular offset from the member shear center to the local loading plane. Zero start/end sentinels are preserved; zero end denotes the member end. These are UMOM assignments. Members is empty; no Beam entities are fetched.</para>
        /// <para>Signs and raw numeric values are preserved without summation, coordinate
        /// transformation, unit conversion or scaling. Confirm the installed API's read-back
        /// unit convention before combining values with input loads; input-unit metadata alone
        /// does not guarantee the units of returned values.</para>
        /// <para>The case is activated before counting and fetching and remains active afterward,
        /// including when a subsequent read fails. The previous active case is not restored.
        /// No load assignments are added, removed or changed. Use a connected, undisposed wrapper.
        /// Serialize all operations sharing the STAAD session on its appropriate COM thread;
        /// these calls do not provide thread safety or COM apartment marshaling.</para>
        /// <para>Failed activation, negative counts, nonzero fetch status, unexpected array
        /// shape/type/length, invalid directions, and non-finite or unwritten values throw
        /// instead of returning partial data. COM and dynamic binding failures propagate.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="lc"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The case ID or <paramref name="mId"/> is not positive.</exception>
        /// <exception cref="NotSupportedException">The case is a combination, repeat load, or unknown case type.</exception>
        /// <exception cref="InvalidOperationException">OpenSTAAD reports failure or returns malformed data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface does not expose a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load, and existing case/entity IDs:
        /// <code>
        /// var lc = new LoadCase(1, "DEAD", LoadCaseType.PrimaryLoad);
        /// var loads = load.GetMemberUniformMoments(lc, mId: 101);
        /// foreach (var item in loads) Console.WriteLine($"{item.Direction}: {item.Magnitude}, {item.SPosition}, {item.EPosition}");
        /// </code>
        /// </example>
        public List<MemberUniformMoment> GetMemberUniformMoments(ILoadCase lc, int mId)
        {
            ValidateLoadQuery(lc, mId, nameof(mId));
            ActivateQueryCase(lc);
            int count = ReadQueryCount((object)ComObject.GetUNIMomentCount(mId), "GetUNIMomentCount");
            var result = new List<MemberUniformMoment>(count);
            if (count == 0) return result;

            object direction = new int[count], force = CreateQueryBuffer(count), start = CreateQueryBuffer(count);
            object end = CreateQueryBuffer(count), eccentricity = CreateQueryBuffer(count);
            object status = ComObject.GetUNIMoments(mId, ref direction, ref force, ref start, ref end, ref eccentricity);
            RequireQuerySuccess(status, "GetUNIMoments");
            int[] directions = ReadQueryArray<int>(direction, count, "Direction");
            double[] moments = ReadQueryValues(force, count, "Magnitude"), starts = ReadQueryValues(start, count, "SPosition");
            double[] ends = ReadQueryValues(end, count, "EPosition"), offsets = ReadQueryValues(eccentricity, count, "Eccentricity");
            for (int i = 0; i < count; i++)
                result.Add(new MemberUniformMoment(ReadQueryDirection(directions[i], 9), moments[i], starts[i], ends[i], offsets[i]) { LoadCase = lc });
            return result;
        }

        /// <summary>Reads all existing primary load cases and their titles and engineering load types.</summary>
        /// <returns>A newly allocated, non-null set of new <see cref="LoadCase"/> objects with positive
        /// IDs, preserved titles, CaseType of PrimaryLoad, and validated Type values.
        /// A successful zero count returns a fresh empty set. Set iteration order is unspecified.</returns>
        /// <remarks>
        /// <para>Reads GetPrimaryLoadCaseCount, GetPrimaryLoadCaseNumbers, GetLoadCaseTitle
        /// and GetLoadType on the currently open model. The case-number method returns a count,
        /// which must match the preceding count; it does not use zero as a success status.
        /// Titles (including empty strings, whitespace and Unicode) are preserved without trimming.
        /// Engineering types include None (23); unknown codes are rejected.</para>
        /// <para>No case is activated and no active-case state or assignments are changed.
        /// Each call creates independent case objects; they contain metadata, not load assignments.
        /// Primary and reference IDs can overlap; metadata is read from the corresponding family.</para>
        /// <para>Negative/malformed counts, changed counts, invalid ID arrays, nonpositive or duplicate
        /// IDs, non-string titles and invalid type results throw instead of returning a partial set.
        /// The read is not an atomic model snapshot: serialize model edits and shared-session operations
        /// on the appropriate COM thread. Use a connected, undisposed wrapper.</para>
        /// <para>LoadCase equality and hashing use Id, CaseType and Type. Do not mutate those fields
        /// while a case belongs to a HashSet; remove it first or rebuild the set afterward.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">OpenSTAAD reports failure or returns inconsistent or malformed metadata.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface does not expose a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load:
        /// <code>
        /// var cases = load.GetAllPrimaryLoadCases();
        /// foreach (var lc in cases) Console.WriteLine(lc.Id + ": " + lc.Title + " (" + lc.Type + ")");
        /// </code>
        /// </example>
        public HashSet<ILoadCase> GetAllPrimaryLoadCases() => ReadAllLoadCases(LoadCaseType.PrimaryLoad);

        /// <summary>Reads all existing reference load cases and their titles and engineering load types.</summary>
        /// <returns>A newly allocated, non-null set of new <see cref="LoadCase"/> objects with positive
        /// IDs, preserved titles, CaseType of ReferenceLoad, and validated Type values.
        /// A successful zero count returns a fresh empty set. Set iteration order is unspecified.</returns>
        /// <remarks>
        /// <para>Reads GetReferenceLoadCaseCount, GetReferenceLoadCaseNumbers, GetReferenceLoadCaseTitle
        /// and GetReferenceLoadType on the currently open model. The case-number method returns a count,
        /// which must match the preceding count; it does not use zero as a success status.
        /// Titles (including empty strings, whitespace and Unicode) are preserved without trimming.
        /// Engineering types include None (23); unknown codes are rejected.</para>
        /// <para>No case is activated and no active-case state or assignments are changed.
        /// Each call creates independent case objects; they contain metadata, not load assignments.
        /// Primary and reference IDs can overlap; metadata is read from the corresponding family.</para>
        /// <para>Negative/malformed counts, changed counts, invalid ID arrays, nonpositive or duplicate
        /// IDs, non-string titles and invalid type results throw instead of returning a partial set.
        /// The read is not an atomic model snapshot: serialize model edits and shared-session operations
        /// on the appropriate COM thread. Use a connected, undisposed wrapper.</para>
        /// <para>LoadCase equality and hashing use Id, CaseType and Type. Do not mutate those fields
        /// while a case belongs to a HashSet; remove it first or rebuild the set afterward.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">OpenSTAAD reports failure or returns inconsistent or malformed metadata.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface does not expose a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load:
        /// <code>
        /// var cases = load.GetAllReferenceLoadCases();
        /// foreach (var lc in cases) Console.WriteLine(lc.Id + ": " + lc.Title + " (" + lc.Type + ")");
        /// </code>
        /// </example>
        public HashSet<ILoadCase> GetAllReferenceLoadCases() => ReadAllLoadCases(LoadCaseType.ReferenceLoad);

        /// <summary>Reads the title and engineering type of one existing primary load case by ID.</summary>
        /// <param name="lcId">Positive STAAD primary case number in the connected model, not a
        /// zero-based index. Zero (the native active-case shortcut) and negative numbers are rejected.</param>
        /// <returns>A newly allocated, non-null <see cref="LoadCase"/> containing the requested Id,
        /// preserved Title, CaseType of PrimaryLoad, and validated engineering Type.
        /// Each call creates an independent metadata object; no assignments are loaded.</returns>
        /// <remarks>
        /// <para>Calls GetLoadCaseTitle(lcId), then GetLoadType(lcId), without activating a case
        /// or enumerating case numbers. Reference and primary IDs may overlap; metadata is read
        /// exclusively from the requested family. The active case and model assignments are unchanged.</para>
        /// <para>Titles retain whitespace, Unicode and empty text. A literal title "NONE" is preserved;
        /// it is not interpreted as absence. Non-string titles, native type errors and undefined
        /// LoadType codes throw. Valid type codes are 0 through 23, including None.
        /// A missing ID is reported through the native metadata error contract and is not returned as null.</para>
        /// <para>This is a metadata read, not an atomic model snapshot. Use a connected, undisposed
        /// wrapper and serialize shared-session operations/model edits on the appropriate COM thread.
        /// Changing the returned object does not write to STAAD. Before changing Id, CaseType or Type
        /// on an object stored in a HashSet, remove it or rebuild the set to preserve hash consistency.</para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="lcId"/> is not positive.</exception>
        /// <exception cref="InvalidOperationException">OpenSTAAD returns malformed metadata or reports a type lookup failure.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface lacks a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load and an existing primary case:
        /// <code>
        /// var lc = load.GetPrimaryLoadCaseFromId(lcId: 17);
        /// Console.WriteLine(lc.Id + ": " + lc.Title + " (" + lc.Type + ")");
        /// </code>
        /// </example>
        public ILoadCase GetPrimaryLoadCaseFromId(int lcId)
        {
            if (lcId <= 0) throw new ArgumentOutOfRangeException(nameof(lcId), "The load case ID must be positive.");
            return ReadLoadCaseMetadata(lcId, LoadCaseType.PrimaryLoad);
        }
        /// <summary>Reads the title and engineering type of one existing reference load case by ID.</summary>
        /// <param name="lcId">Positive STAAD reference case number in the connected model, not a
        /// zero-based index. Zero (the native active-case shortcut) and negative numbers are rejected.</param>
        /// <returns>A newly allocated, non-null <see cref="LoadCase"/> containing the requested Id,
        /// preserved Title, CaseType of ReferenceLoad, and validated engineering Type.
        /// Each call creates an independent metadata object; no assignments are loaded.</returns>
        /// <remarks>
        /// <para>Calls GetReferenceLoadCaseTitle(lcId), then GetReferenceLoadType(lcId), without activating a case
        /// or enumerating case numbers. Reference and primary IDs may overlap; metadata is read
        /// exclusively from the requested family. The active case and model assignments are unchanged.</para>
        /// <para>Titles retain whitespace, Unicode and empty text. A literal title "NONE" is preserved;
        /// it is not interpreted as absence. Non-string titles, native type errors and undefined
        /// LoadType codes throw. Valid type codes are 0 through 23, including None.
        /// A missing ID is reported through the native metadata error contract and is not returned as null.</para>
        /// <para>This is a metadata read, not an atomic model snapshot. Use a connected, undisposed
        /// wrapper and serialize shared-session operations/model edits on the appropriate COM thread.
        /// Changing the returned object does not write to STAAD. Before changing Id, CaseType or Type
        /// on an object stored in a HashSet, remove it or rebuild the set to preserve hash consistency.</para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="lcId"/> is not positive.</exception>
        /// <exception cref="InvalidOperationException">OpenSTAAD returns malformed metadata or reports a type lookup failure.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface lacks a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load and an existing reference case:
        /// <code>
        /// var lc = load.GetReferenceLoadCaseFromId(lcId: 17);
        /// Console.WriteLine(lc.Id + ": " + lc.Title + " (" + lc.Type + ")");
        /// </code>
        /// </example>
        public ILoadCase GetReferenceLoadCaseFromId(int lcId)
        {
            if (lcId <= 0) throw new ArgumentOutOfRangeException(nameof(lcId), "The load case ID must be positive.");
            return ReadLoadCaseMetadata(lcId, LoadCaseType.ReferenceLoad);
        }
        /// <summary>Reads existing primary load case metadata for an ordered sequence of case IDs.</summary>
        /// <param name="loadCasesIds">Non-null sequence of positive STAAD primary case numbers.
        /// The sequence is enumerated once and all IDs are validated before any COM metadata access.</param>
        /// <returns>A newly allocated, non-null list of new <see cref="LoadCase"/> objects, one per
        /// supplied ID, in input order. Duplicate IDs are preserved as distinct objects. Every CaseType
        /// is PrimaryLoad. An empty input returns a fresh empty list without contacting OpenSTAAD.</returns>
        /// <remarks>
        /// <para>The operation is eager: input enumeration, validation and all metadata reads complete
        /// before returning. Each ID uses GetLoadCaseTitle followed by GetLoadType; no case is activated
        /// and no case-number enumeration is performed. The active case and assignments are unchanged.</para>
        /// <para>Titles preserve whitespace, Unicode, empty strings and literal "NONE". Types must be
        /// defined LoadType codes (0 through 23, including None). Malformed/native-error metadata throws
        /// and no partial list is returned. Exceptions from the input enumerator propagate unchanged;
        /// enumeration and argument failures occur before any COM metadata call.</para>
        /// <para>List entries are independent metadata objects, including repeated IDs. Editing them
        /// does not write to STAAD. Input IDs are snapshotted, but metadata reads are not an atomic model
        /// snapshot. Use a connected, undisposed wrapper and serialize shared-session operations and
        /// model edits on the appropriate COM thread. No assignments are loaded.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="loadCasesIds"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Any element of <paramref name="loadCasesIds"/> is not positive.</exception>
        /// <exception cref="InvalidOperationException">OpenSTAAD returns malformed metadata or reports a type lookup failure.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface lacks a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load and existing primary cases:
        /// <code>
        /// var cases = load.GetPrimaryLoadCasesFromIds(new[] { 17, 41, 17 });
        /// foreach (var lc in cases) Console.WriteLine(lc.Id + ": " + lc.Title);
        /// </code>
        /// </example>
        public List<ILoadCase> GetPrimaryLoadCasesFromIds(IEnumerable<int> loadCasesIds)
        {
            if (loadCasesIds == null) throw new ArgumentNullException(nameof(loadCasesIds));
            var ids = loadCasesIds.ToList();
            if (ids.Any(id => id <= 0))
                throw new ArgumentOutOfRangeException(nameof(loadCasesIds), "Every load case ID must be positive.");
            var result = new List<ILoadCase>(ids.Count);
            foreach (int id in ids) result.Add(ReadLoadCaseMetadata(id, LoadCaseType.PrimaryLoad));
            return result;
        }

        /// <summary>Reads existing reference load case metadata for an ordered sequence of case IDs.</summary>
        /// <param name="loadCasesIds">Non-null sequence of positive STAAD reference case numbers.
        /// The sequence is enumerated once and all IDs are validated before any COM metadata access.</param>
        /// <returns>A newly allocated, non-null list of new <see cref="LoadCase"/> objects, one per
        /// supplied ID, in input order. Duplicate IDs are preserved as distinct objects. Every CaseType
        /// is ReferenceLoad. An empty input returns a fresh empty list without contacting OpenSTAAD.</returns>
        /// <remarks>
        /// <para>The operation is eager: input enumeration, validation and all metadata reads complete
        /// before returning. Each ID uses GetReferenceLoadCaseTitle followed by GetReferenceLoadType; no case is activated
        /// and no case-number enumeration is performed. The active case and assignments are unchanged.</para>
        /// <para>Titles preserve whitespace, Unicode, empty strings and literal "NONE". Types must be
        /// defined LoadType codes (0 through 23, including None). Malformed/native-error metadata throws
        /// and no partial list is returned. Exceptions from the input enumerator propagate unchanged;
        /// enumeration and argument failures occur before any COM metadata call.</para>
        /// <para>List entries are independent metadata objects, including repeated IDs. Editing them
        /// does not write to STAAD. Input IDs are snapshotted, but metadata reads are not an atomic model
        /// snapshot. Use a connected, undisposed wrapper and serialize shared-session operations and
        /// model edits on the appropriate COM thread. No assignments are loaded.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="loadCasesIds"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Any element of <paramref name="loadCasesIds"/> is not positive.</exception>
        /// <exception cref="InvalidOperationException">OpenSTAAD returns malformed metadata or reports a type lookup failure.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface lacks a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load and existing reference cases:
        /// <code>
        /// var cases = load.GetReferenceLoadCasesFromIds(new[] { 17, 41, 17 });
        /// foreach (var lc in cases) Console.WriteLine(lc.Id + ": " + lc.Title);
        /// </code>
        /// </example>
        public List<ILoadCase> GetReferenceLoadCasesFromIds(IEnumerable<int> loadCasesIds)
        {
            if (loadCasesIds == null) throw new ArgumentNullException(nameof(loadCasesIds));
            var ids = loadCasesIds.ToList();
            if (ids.Any(id => id <= 0))
                throw new ArgumentOutOfRangeException(nameof(loadCasesIds), "Every load case ID must be positive.");
            var result = new List<ILoadCase>(ids.Count);
            foreach (int id in ids) result.Add(ReadLoadCaseMetadata(id, LoadCaseType.ReferenceLoad));
            return result;
        }

        /// <summary>Reads metadata for every existing load combination in the connected model.</summary>
        /// <returns>A fresh, non-null HashSet of new <see cref="LoadCase"/> metadata objects, with
        /// positive Id, preserved Title, CaseType of LoadCombination and Type of None.
        /// A successful zero count returns a fresh empty set. Iteration order is unspecified.</returns>
        /// <remarks>
        /// <para>Reads GetLoadCombinationCaseCount and GetLoadCombinationCaseNumbers, then
        /// GetLoadCaseTitle for each validated ID. The number-retrieval result must match the count;
        /// zero is not treated as success when combinations exist. IDs must be positive and unique
        /// in a one-dimensional Int32 array of exactly the expected length. Nonzero bounds are supported.</para>
        /// <para>Combinations have no engineering LoadType classification, so Type is always None (23);
        /// GetLoadType is not called. Titles preserve whitespace, Unicode, empty text and literal "NONE".
        /// No combination factors, constituent cases, assignments or analysis results are loaded.</para>
        /// <para>No case is activated and no model data is changed. Negative/malformed counts, native
        /// errors, inconsistent counts, invalid ID arrays and non-string titles throw instead of returning
        /// partial results. Returned objects are independent of native arrays and previous calls.</para>
        /// <para>Use a connected, undisposed wrapper and serialize shared-session operations/model edits
        /// on the appropriate COM thread. This is not an atomic snapshot. LoadCase hashing uses Id,
        /// CaseType and Type; remove objects from a HashSet before changing those fields or rebuild it.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">OpenSTAAD reports failure or returns inconsistent or malformed metadata.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface lacks a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load:
        /// <code>
        /// var combinations = load.GetAllLoadCombinationCases();
        /// foreach (var lc in combinations) Console.WriteLine(lc.Id + ": " + lc.Title);
        /// </code>
        /// </example>
        public HashSet<ILoadCase> GetAllLoadCombinationCases()
        {
            var result = new HashSet<ILoadCase>();
            foreach (int id in ReadCombinationCaseIds()) result.Add(ReadCombinationCaseMetadata(id));
            return result;
        }
        /// <summary>Reads metadata for one existing load combination, after verifying its membership.</summary>
        /// <param name="lcId">Positive STAAD load combination number in the connected model.
        /// Zero (the native active-case shortcut) and negative IDs are rejected before COM access.</param>
        /// <returns>A new, non-null <see cref="LoadCase"/> with the requested Id, preserved Title,
        /// CaseType of LoadCombination and Type of None. No factors or assignments are loaded.</returns>
        /// <remarks>
        /// <para>Reads and validates GetLoadCombinationCaseCount and GetLoadCombinationCaseNumbers
        /// before calling GetLoadCaseTitle only for the requested combination. An ID absent from the
        /// combination list throws, including an ID belonging only to a primary or reference case.
        /// An empty combination list throws without reading a title. This method enumerates combination
        /// IDs on each call; use GetAllLoadCombinationCases when reading many combinations.</para>
        /// <para>Combinations have no engineering LoadType category: Type is always None (23) and
        /// GetLoadType is not called. The title must be a string; whitespace, Unicode, empty strings
        /// and literal "NONE" are preserved. Nonpositive/duplicate IDs, malformed arrays or counts,
        /// native errors and non-string titles throw. Missing combinations are not returned as null.</para>
        /// <para>No case is activated and the active case/model assignments are unchanged. Each call
        /// returns an independent metadata object. Use a connected, undisposed wrapper and serialize
        /// shared-session access/model edits on the appropriate COM thread. Membership and title reads
        /// are not an atomic snapshot. Editing the result does not write to STAAD; remove it from any
        /// HashSet before changing Id, CaseType or Type, or rebuild the set afterward.</para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="lcId"/> is not positive.</exception>
        /// <exception cref="InvalidOperationException">The ID is absent from the combination list, or OpenSTAAD reports failure or malformed metadata.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface lacks a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load and an existing combination:
        /// <code>
        /// var lc = load.GetLoadCombinationCaseFromId(lcId: 201);
        /// Console.WriteLine(lc.Id + ": " + lc.Title);
        /// </code>
        /// </example>
        public ILoadCase GetLoadCombinationCaseFromId(int lcId)
        {
            if (lcId <= 0) throw new ArgumentOutOfRangeException(nameof(lcId), "The combination ID must be positive.");
            if (!ReadCombinationCaseIds().Contains(lcId))
                throw new InvalidOperationException("OpenSTAAD has no load combination with ID " + lcId + ".");
            return ReadCombinationCaseMetadata(lcId);
        }

        /// <summary>Reads metadata for a distinct set of existing load combination IDs.</summary>
        /// <param name="lcIds">Non-null sequence of positive STAAD combination numbers.
        /// Input is enumerated once and every ID is validated before any COM access. Duplicate IDs
        /// are collapsed; output order is unspecified.</param>
        /// <returns>A fresh, non-null HashSet exposed as IEnumerable, containing one new
        /// <see cref="LoadCase"/> per distinct requested ID. CaseType is LoadCombination and Type
        /// is None. An empty input returns a fresh empty set without contacting OpenSTAAD.</returns>
        /// <remarks>
        /// <para>This operation is eager. It snapshots input, reads and validates the combination
        /// ID list once, and checks every requested ID for membership before reading any title.
        /// A missing or primary-only ID throws; no entries are silently skipped and no partial
        /// set is returned. Titles are read once per distinct requested ID. No factors,
        /// constituent cases, assignments or engineering type categories are loaded.</para>
        /// <para>Native counts, number returns and arrays use the same strict validation as
        /// GetAllLoadCombinationCases. Titles retain whitespace, Unicode, empty text and literal
        /// "NONE"; non-string results throw. Input-enumerator, COM and binding failures propagate.
        /// Re-enumerating the returned set performs no COM reads; call again to refresh metadata.</para>
        /// <para>No case is activated or model data changed. Use a connected, undisposed wrapper
        /// and serialize shared-session operations/model edits on the appropriate COM thread.
        /// Membership and titles are not an atomic snapshot. Results are independent of input,
        /// native buffers and previous calls. Remove an object from any HashSet before changing
        /// its Id, CaseType or Type, or rebuild the set afterward to preserve hash consistency.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="lcIds"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Any element of <paramref name="lcIds"/> is not positive.</exception>
        /// <exception cref="InvalidOperationException">A requested ID is absent or native data is malformed/inconsistent.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface lacks a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load and existing combinations:
        /// <code>
        /// var combinations = load.GetLoadCombinationCasesFromIds(new[] { 201, 301, 201 });
        /// foreach (var lc in combinations) Console.WriteLine(lc.Id + ": " + lc.Title);
        /// </code>
        /// </example>
        public IEnumerable<ILoadCase> GetLoadCombinationCasesFromIds(IEnumerable<int> lcIds)
        {
            if (lcIds == null) throw new ArgumentNullException(nameof(lcIds));
            var ids = lcIds.ToList();
            if (ids.Any(id => id <= 0))
                throw new ArgumentOutOfRangeException(nameof(lcIds), "Every combination ID must be positive.");
            var result = new HashSet<ILoadCase>();
            if (ids.Count == 0) return result;
            var requested = new HashSet<int>(ids);
            var available = new HashSet<int>(ReadCombinationCaseIds());
            foreach (int id in requested)
                if (!available.Contains(id))
                    throw new InvalidOperationException("OpenSTAAD has no load combination with ID " + id + ".");
            foreach (int id in requested) result.Add(ReadCombinationCaseMetadata(id));
            return result;
        }
        /// <summary>Reads all case metadata for one supported load case family.</summary>
        /// <param name="loadCaseType">PrimaryLoad (the default), ReferenceLoad or LoadCombination.
        /// RepeatLoad and undefined enum values are rejected before COM access.</param>
        /// <returns>A fresh, non-null HashSet exposed as IEnumerable, with new <see cref="LoadCase"/>
        /// metadata objects for the selected family. Zero native count returns a fresh empty set.
        /// Iteration order is unspecified; combination Type is None and other types are validated.</returns>
        /// <remarks>
        /// <para>Delegates to GetAllPrimaryLoadCases, GetAllReferenceLoadCases or
        /// GetAllLoadCombinationCases. Each family retains its strict count, number, array and
        /// title validation; primary/reference engineering types use their own native APIs.
        /// No families are merged. Repeat-load discovery is not implemented.</para>
        /// <para>The read is eager: all native metadata calls finish before returning, and
        /// re-enumerating the result makes no COM calls. Failures throw instead of returning a
        /// partial set. No case is activated and no assignments or model data are changed.
        /// Factors, constituent cases and analysis results are not loaded.</para>
        /// <para>Use a connected, undisposed wrapper and serialize shared-session operations
        /// and model edits on the appropriate COM thread. This is not an atomic snapshot.
        /// Returned objects are independent of earlier calls; editing them does not write to
        /// STAAD. Remove objects from HashSets before changing Id, CaseType or Type, or rebuild
        /// the set afterward because those fields determine LoadCase equality and hashing.</para>
        /// </remarks>
        /// <exception cref="NotSupportedException"><paramref name="loadCaseType"/> is RepeatLoad or undefined.</exception>
        /// <exception cref="InvalidOperationException">OpenSTAAD reports failure or returns malformed/inconsistent metadata.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface lacks a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load:
        /// <code>
        /// var primaryCases = load.GetAllLoadCasesOfType();
        /// var referenceCases = load.GetAllLoadCasesOfType(LoadCaseType.ReferenceLoad);
        /// foreach (var lc in referenceCases) Console.WriteLine(lc.Id + ": " + lc.Title);
        /// </code>
        /// </example>
        public IEnumerable<ILoadCase> GetAllLoadCasesOfType(LoadCaseType loadCaseType = LoadCaseType.PrimaryLoad)
        {
            switch (loadCaseType)
            {
                case LoadCaseType.PrimaryLoad: return GetAllPrimaryLoadCases();
                case LoadCaseType.ReferenceLoad: return GetAllReferenceLoadCases();
                case LoadCaseType.LoadCombination: return GetAllLoadCombinationCases();
                default: throw new NotSupportedException("Case discovery supports only PrimaryLoad, ReferenceLoad and LoadCombination.");
            }
        }
        /// <summary>Finds a case by ID within a supported family, returning null when it is absent.</summary>
        /// <param name="lcId">Positive STAAD case number. Zero and negative numbers are rejected
        /// before COM access; a positive number need not exist in the selected family.</param>
        /// <param name="loadCaseType">PrimaryLoad, ReferenceLoad or LoadCombination.
        /// RepeatLoad and undefined enum values are rejected before COM access.</param>
        /// <returns>A new <see cref="LoadCase"/> exposed as ILoadCase when the requested ID belongs
        /// to the selected family; otherwise null, including when that family is empty.
        /// The object has the selected CaseType and validated metadata; combination Type is None.</returns>
        /// <remarks>
        /// <para>Preserves the main project's lookup behavior by eagerly reading the entire family
        /// through GetAllLoadCasesOfType, then finding the requested ID. A primary/reference number
        /// does not match another family. Missing IDs return null; native or malformed metadata
        /// failures throw, even if the failure concerns a different case in that family.</para>
        /// <para>Use GetPrimaryLoadCaseFromId, GetReferenceLoadCaseFromId or
        /// GetLoadCombinationCaseFromId when their focused lookup and throwing-on-error contracts
        /// fit your operation. This generic lookup reads metadata for every case, does not load
        /// assignments, factors or constituent cases, and performs no case activation/model changes.</para>
        /// <para>The result is independent of earlier queries; editing it does not write to STAAD.
        /// Use a connected, undisposed wrapper and serialize shared-session operations/model edits
        /// on the appropriate COM thread. This is not an atomic snapshot. Remove a returned object
        /// from any HashSet before changing Id, CaseType or Type, or rebuild the set afterward.</para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="lcId"/> is not positive.</exception>
        /// <exception cref="NotSupportedException"><paramref name="loadCaseType"/> is RepeatLoad or undefined.</exception>
        /// <exception cref="InvalidOperationException">OpenSTAAD reports failure or returns malformed/inconsistent family metadata.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface lacks a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load:
        /// <code>
        /// var lc = load.GetLoadCase(lcId: 201, loadCaseType: LoadCaseType.LoadCombination);
        /// if (lc != null) Console.WriteLine(lc.Id + ": " + lc.Title);
        /// </code>
        /// </example>
        public ILoadCase GetLoadCase(int lcId, LoadCaseType loadCaseType)
        {
            if (lcId <= 0) throw new ArgumentOutOfRangeException(nameof(lcId), "The load case ID must be positive.");
            return GetAllLoadCasesOfType(loadCaseType).FirstOrDefault(lc => lc.Id == lcId);
        }

        /// <summary>Reads case metadata whose IDs lie within an inclusive range in one supported family.</summary>
        /// <param name="fromId">Positive lower STAAD case number, included in the range.</param>
        /// <param name="toId">Positive upper STAAD case number, included in the range.
        /// Must be greater than or equal to fromId; reversed bounds are not silently swapped.</param>
        /// <param name="loadCaseType">PrimaryLoad, ReferenceLoad or LoadCombination.
        /// RepeatLoad and undefined values are unsupported.</param>
        /// <returns>A fresh, non-null List exposed as IEnumerable, containing new <see cref="LoadCase"/>
        /// objects for existing cases in the selected family and inclusive range. No matches returns
        /// an empty list. Result order is unspecified and missing IDs are not synthesized.</returns>
        /// <remarks>
        /// <para>Validates the bounds before COM access, then eagerly reads the complete family
        /// through GetAllLoadCasesOfType and filters its metadata. It does not iterate every integer
        /// in the range, sort cases or interpret IDs as array indices. Equal bounds select at most one
        /// case. Native or malformed metadata errors throw even when outside the requested range.</para>
        /// <para>All reads finish before return; re-enumeration makes no COM calls. No case is activated,
        /// and no model data, assignments or factors are changed or loaded. The result and its objects
        /// are independent of earlier calls; editing them does not write to STAAD.</para>
        /// <para>Use a connected, undisposed wrapper and serialize shared-session operations/model
        /// edits on the appropriate COM thread. This is a managed metadata result, not an atomic model
        /// snapshot. Combination Type is None; primary/reference engineering types are validated.</para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">fromId or toId is not positive.</exception>
        /// <exception cref="ArgumentException">toId is less than fromId.</exception>
        /// <exception cref="NotSupportedException">loadCaseType is RepeatLoad or undefined.</exception>
        /// <exception cref="InvalidOperationException">OpenSTAAD reports failure or returns malformed/inconsistent family metadata.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface lacks a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load:
        /// <code>
        /// var cases = load.GetLoadCases(fromId: 1, toId: 50, loadCaseType: LoadCaseType.PrimaryLoad);
        /// foreach (var lc in cases) Console.WriteLine(lc.Id + ": " + lc.Title);
        /// </code>
        /// </example>
        public IEnumerable<ILoadCase> GetLoadCases(int fromId, int toId, LoadCaseType loadCaseType)
        {
            if (fromId <= 0) throw new ArgumentOutOfRangeException(nameof(fromId), "The lower case ID must be positive.");
            if (toId <= 0) throw new ArgumentOutOfRangeException(nameof(toId), "The upper case ID must be positive.");
            if (toId < fromId) throw new ArgumentException("The upper case ID must not be less than the lower case ID.", nameof(toId));
            return GetAllLoadCasesOfType(loadCaseType).Where(lc => lc.Id >= fromId && lc.Id <= toId).ToList();
        }
        /// <summary>Filters one supported family's metadata by a sequence of requested case IDs.</summary>
        /// <param name="loadCasesIds">Non-null sequence of positive STAAD case numbers.
        /// Enumerated once and fully validated before COM access. Duplicate IDs collapse and
        /// missing IDs are omitted; input order is not the result order.</param>
        /// <param name="loadCaseType">PrimaryLoad, ReferenceLoad or LoadCombination.
        /// RepeatLoad and undefined values are rejected even when the ID sequence is empty.</param>
        /// <returns>A fresh, non-null List exposed as IEnumerable with one new <see cref="LoadCase"/>
        /// per existing matching case in the selected family. Empty input or no matches returns an
        /// empty list. Results contain distinct IDs in unspecified family enumeration order.</returns>
        /// <remarks>
        /// <para>This is a family filter, not the ordered primary/reference FromIds batch contract
        /// and not combination batch membership validation. Input is snapshotted once; every ID and
        /// the family are validated before COM access. Empty input makes no native calls.
        /// Otherwise GetAllLoadCasesOfType eagerly reads the entire family before matching IDs.</para>
        /// <para>Missing requested IDs are silently omitted after a successful family read; native
        /// or malformed metadata failures still throw, including failures for unrequested cases.
        /// Input-enumerator failures propagate unchanged before COM, and no partial result is returned.
        /// Re-enumeration performs no native reads, and later input changes do not affect the result.</para>
        /// <para>No case is activated or model data changed. Assignments, factors and constituent
        /// cases are not loaded. Result objects are independent of earlier calls; editing them does
        /// not write to STAAD. Use a connected, undisposed wrapper and serialize shared-session
        /// operations/model edits on the appropriate COM thread; reads are not an atomic snapshot.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">loadCasesIds is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">An element of loadCasesIds is not positive.</exception>
        /// <exception cref="NotSupportedException">loadCaseType is RepeatLoad or undefined.</exception>
        /// <exception cref="InvalidOperationException">OpenSTAAD reports failure or returns malformed/inconsistent family metadata.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface lacks a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load:
        /// <code>
        /// var cases = load.GetLoadCasesFromIds(new[] { 17, 41, 17 }, LoadCaseType.PrimaryLoad);
        /// foreach (var lc in cases) Console.WriteLine(lc.Id + ": " + lc.Title);
        /// </code>
        /// </example>
        public IEnumerable<ILoadCase> GetLoadCasesFromIds(IEnumerable<int> loadCasesIds, LoadCaseType loadCaseType)
        {
            if (loadCasesIds == null) throw new ArgumentNullException(nameof(loadCasesIds));
            var ids = loadCasesIds.ToList();
            if (ids.Any(id => id <= 0))
                throw new ArgumentOutOfRangeException(nameof(loadCasesIds), "Every requested case ID must be positive.");
            ValidateCaseFilterFamily(loadCaseType);
            var requested = new HashSet<int>(ids);
            if (requested.Count == 0) return new List<ILoadCase>();
            return GetAllLoadCasesOfType(loadCaseType).Where(lc => requested.Contains(lc.Id)).ToList();
        }
        /// <summary>Reads all metadata in one supported family into a concrete, mutable list.</summary>
        /// <param name="loadCaseType">PrimaryLoad, ReferenceLoad or LoadCombination.
        /// RepeatLoad and undefined values are unsupported and are never returned as null.</param>
        /// <returns>A fresh, non-null List of new <see cref="LoadCase"/> objects exposed as ILoadCase,
        /// with one entry per existing case in the selected family. A zero native count returns a
        /// fresh empty list. Result order is unspecified; combination engineering Type is None.</returns>
        /// <remarks>
        /// <para>Eagerly delegates to GetAllLoadCasesOfType and copies the family set into a new list.
        /// All native count, number, array, title and applicable engineering type checks are preserved.
        /// No families are merged or sorted. Failure throws instead of returning a partial list.</para>
        /// <para>List and records are independent of earlier calls. Editing them does not write to
        /// STAAD; re-enumeration makes no COM calls. No case is activated and no model data changed.
        /// Assignments, combination factors, constituent cases and analysis results are not loaded.</para>
        /// <para>Use a connected, undisposed wrapper and serialize shared-session operations/model
        /// edits on the appropriate COM thread. This is not an atomic model snapshot. Unlike the
        /// main project's repeat-load stub, unsupported families throw rather than returning null.</para>
        /// </remarks>
        /// <exception cref="NotSupportedException">loadCaseType is RepeatLoad or undefined.</exception>
        /// <exception cref="InvalidOperationException">OpenSTAAD reports failure or returns malformed/inconsistent family metadata.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The underlying COM operation fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface lacks a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load:
        /// <code>
        /// var cases = load.GetLoadCasesOfType(LoadCaseType.LoadCombination);
        /// foreach (var lc in cases) Console.WriteLine(lc.Id + ": " + lc.Title);
        /// </code>
        /// </example>
        public List<ILoadCase> GetLoadCasesOfType(LoadCaseType loadCaseType)
            => GetAllLoadCasesOfType(loadCaseType).ToList();

        #region Load Case Clearing

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='OSLoadAdapter.ClearPrimaryLoadCase`0(ILoadCase,bool)']/*" />
        public bool ClearPrimaryLoadCase(ILoadCase loadCase, bool isReferenceLoad) =>
            ClearPrimaryLoadCases(new List<ILoadCase> { loadCase }, isReferenceLoad);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='OSLoadAdapter.ClearPrimaryLoadCases`0(IEnumerable&lt;ILoadCase&gt;,bool)']/*" />
        public bool ClearPrimaryLoadCases(IEnumerable<ILoadCase> loadCases, bool isReferenceLoad)
        {
            if (loadCases == null) throw new ArgumentNullException(nameof(loadCases));
            IEnumerable<ILoadCase> primaryLcs = loadCases.Where(lc => lc != null && lc.CaseType == LoadCaseType.PrimaryLoad);
            return ClearPrimaryLoadCases(primaryLcs.Select(lc => lc.Id), isReferenceLoad);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='OSLoadAdapter.ClearPrimaryLoadCases`0(IEnumerable&lt;int&gt;,bool)']/*" />
        public bool ClearPrimaryLoadCases(IEnumerable<int> lcIds, bool isReferenceLoad)
        {
            if (lcIds == null) throw new ArgumentNullException(nameof(lcIds));
            int[] ids = lcIds.ToArray();
            if (ids.Length == 0) return true;
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");
            dynamic result = ComObject.ClearPrimaryLoadCase(ids, isReferenceLoad);
            return result != null && (int)result == 1;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.ClearPrimaryLoadCase`0(ILoadCase)']/*" />
        public bool ClearPrimaryLoadCase(ILoadCase loadCase) =>
            ClearPrimaryLoadCases(new List<ILoadCase> { loadCase });

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.ClearPrimaryLoadCases`0(IEnumerable&lt;ILoadCase&gt;)']/*" />
        public bool ClearPrimaryLoadCases(IEnumerable<ILoadCase> loadCases)
        {
            if (loadCases == null) throw new ArgumentNullException(nameof(loadCases));
            IEnumerable<ILoadCase> primaryLcs = loadCases.Where(lc => lc != null && lc.CaseType == LoadCaseType.PrimaryLoad);
            return ClearPrimaryLoadCases(primaryLcs.Select(lc => lc.Id));
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.ClearPrimaryLoadCases`0(IEnumerable&lt;int&gt;)']/*" />
        public bool ClearPrimaryLoadCases(IEnumerable<int> lcIds)
        {
            if (lcIds == null) throw new ArgumentNullException(nameof(lcIds));
            int[] ids = lcIds.ToArray();
            if (ids.Length == 0) return true;
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");
            dynamic result = ComObject.ClearPrimaryLoadCase(ids, false);
            return result != null && (int)result == 1;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.ClearReferenceLoadCase`0(ILoadCase)']/*" />
        public bool ClearReferenceLoadCase(ILoadCase loadCase) =>
            ClearReferenceLoadCases(new List<ILoadCase> { loadCase });

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.ClearReferenceLoadCases`0(IEnumerable&lt;ILoadCase&gt;)']/*" />
        public bool ClearReferenceLoadCases(IEnumerable<ILoadCase> loadCases)
        {
            if (loadCases == null) throw new ArgumentNullException(nameof(loadCases));
            IEnumerable<ILoadCase> referenceLcs = loadCases.Where(lc => lc != null && lc.CaseType == LoadCaseType.ReferenceLoad);
            return ClearReferenceLoadCases(referenceLcs.Select(lc => lc.Id));
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.ClearReferenceLoadCases`0(IEnumerable&lt;int&gt;)']/*" />
        public bool ClearReferenceLoadCases(IEnumerable<int> lcIds)
        {
            if (lcIds == null) throw new ArgumentNullException(nameof(lcIds));
            int[] ids = lcIds.ToArray();
            if (ids.Length == 0) return true;
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");
            dynamic result = ComObject.ClearReferenceLoadCase(ids);
            return result != null && (int)result == 1;
        }

        #endregion

        #region Load Case Creation & Titles

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreateNewPrimaryLoad`0(string,LoadType)']/*" />
        public int CreateNewPrimaryLoad(string lcTitle, LoadType loadType)
        {
            if (string.IsNullOrEmpty(lcTitle)) throw new ArgumentNullException(nameof(lcTitle));
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");
            dynamic result = ComObject.CreateNewPrimaryLoadEx(lcTitle, (int)loadType);
            return Convert.ToInt32(result);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreateNewPrimaryLoad`0(int,string,LoadType)']/*" />
        public int CreateNewPrimaryLoad(int lcId, string lcTitle, LoadType loadType)
        {
            if (string.IsNullOrEmpty(lcTitle)) throw new ArgumentNullException(nameof(lcTitle));
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");
            dynamic result = ComObject.CreateNewPrimaryLoadEx2(lcTitle, (int)loadType, lcId);
            return Convert.ToInt32(result);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreateNewPrimaryLoadEx`0(LoadCase)']/*" />
        public int CreateNewPrimaryLoadEx(LoadCase lc)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            return CreateNewPrimaryLoad(lc.Title, lc.Type);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreateNewPrimaryLoadEx2`0(LoadCase)']/*" />
        public int CreateNewPrimaryLoadEx2(LoadCase lc)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            return CreateNewPrimaryLoad(lc.Id, lc.Title, lc.Type);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreateNewReferenceLoad`0(int,string,LoadType)']/*" />
        public int CreateNewReferenceLoad(int lcId, string lcTitle, LoadType loadType)
        {
            if (string.IsNullOrEmpty(lcTitle)) throw new ArgumentNullException(nameof(lcTitle));
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");
            dynamic result = ComObject.CreateNewReferenceLoad(lcId, lcTitle, (int)loadType);
            return Convert.ToInt32(result);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreateNewReferenceLoad`0(LoadCase)']/*" />
        public int CreateNewReferenceLoad(LoadCase lc)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            return CreateNewReferenceLoad(lc.Id, lc.Title, lc.Type);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.GetLoadCaseTitle`0(int)']/*" />
        public string GetLoadCaseTitle(int id)
        {
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");
            dynamic result = ComObject.GetLoadCaseTitle(id);
            return result != null ? result.ToString() : string.Empty;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.SetLoadCaseActive`0(ILoadCase)']/*" />
        public IOSLoad SetLoadCaseActive(ILoadCase lc)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");

            if (lc.CaseType == LoadCaseType.ReferenceLoad)
            {
                ComObject.SetReferenceLoadActive(lc.Id);
            }
            else if (lc.CaseType == LoadCaseType.PrimaryLoad)
            {
                ComObject.SetLoadActive(lc.Id);
            }

            return this;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreateNewLoadCase`0(ILoadCase)']/*" />
        public IOSLoad CreateNewLoadCase(ILoadCase lc)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");

            if (lc.CaseType == LoadCaseType.ReferenceLoad)
            {
                CreateNewReferenceLoad(lc.Id, lc.Title, lc.Type);
            }
            else if (lc.CaseType == LoadCaseType.PrimaryLoad)
            {
                if (lc.Id > 0)
                {
                    CreateNewPrimaryLoad(lc.Id, lc.Title, lc.Type);
                }
                else
                {
                    CreateNewPrimaryLoad(lc.Title, lc.Type);
                }
            }
            else
            {
                throw new NotSupportedException($"Load case type '{lc.CaseType}' is not supported for creation via OpenSTAAD.");
            }

            return this;
        }

        #endregion

        #region Support Settlement

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddSupportSettlement`0(ILoadCase,int,SettlementDirection,double)']/*" />
        public IOSLoad AddSupportSettlement(ILoadCase lc, int nodeId, SettlementDirection direction, double mmSettlement)
        {
            return AddSupportSettlement(lc, new List<int> { nodeId }, direction, mmSettlement);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddSupportSettlement`0(ILoadCase,Node,SettlementDirection,double)']/*" />
        public IOSLoad AddSupportSettlement(ILoadCase lc, Node node, SettlementDirection direction, double mmSettlement)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            return AddSupportSettlement(lc, new List<int> { node.Id }, direction, mmSettlement);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddSupportSettlement`0(ILoadCase,IEnumerable&lt;Node&gt;,SettlementDirection,double)']/*" />
        public IOSLoad AddSupportSettlement(ILoadCase lc, IEnumerable<Node> nodes, SettlementDirection direction, double mmSettlement)
        {
            if (nodes == null) throw new ArgumentNullException(nameof(nodes));
            return AddSupportSettlement(lc, nodes.Select(n => n.Id), direction, mmSettlement);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddSupportSettlement`0(ILoadCase,IEnumerable&lt;int&gt;,SettlementDirection,double)']/*" />
        public IOSLoad AddSupportSettlement(ILoadCase lc, IEnumerable<int> nodeIds, SettlementDirection direction, double mmSettlement)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            if (nodeIds == null) throw new ArgumentNullException(nameof(nodeIds));

            SetLoadCaseActive(lc);
            int[] ids = nodeIds.ToArray();
            if (ids.Length > 0 && ComObject != null)
            {
                ComObject.AddSupportDisplacement(ids, (int)direction, mmSettlement / 1000.0);
            }

            return this;
        }

        #endregion

        #region Batch Load Creation

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreatePrimaryLoadCases`0(HashSet&lt;ILoadCase&gt;)']/*" />
        public IOSLoad CreatePrimaryLoadCases(HashSet<ILoadCase> primaryLoadCases) =>
            CreatePrimaryLoadCases((IEnumerable<ILoadCase>)primaryLoadCases);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreatePrimaryLoadCases`0(IEnumerable&lt;ILoadCase&gt;)']/*" />
        public IOSLoad CreatePrimaryLoadCases(IEnumerable<ILoadCase> primaryLoadCases)
        {
            if (primaryLoadCases != null)
            {
                foreach (ILoadCase lc in primaryLoadCases)
                {
                    if (lc is LoadCase pl)
                    {
                        CreateNewPrimaryLoad(pl.Title, pl.Type);
                    }
                    else if (lc != null)
                    {
                        CreateNewPrimaryLoad(lc.Title, LoadType.Dead);
                    }
                }
            }

            return this;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreateReferenceLoadCases`0(HashSet&lt;ILoadCase&gt;)']/*" />
        public IOSLoad CreateReferenceLoadCases(HashSet<ILoadCase> referenceLoads) =>
            CreateReferenceLoadCases((IEnumerable<ILoadCase>)referenceLoads);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreateReferenceLoadCases`0(IEnumerable&lt;ILoadCase&gt;)']/*" />
        public IOSLoad CreateReferenceLoadCases(IEnumerable<ILoadCase> referenceLoads)
        {
            if (referenceLoads != null)
            {
                foreach (ILoadCase lc in referenceLoads)
                {
                    if (lc is LoadCase rl)
                    {
                        CreateNewReferenceLoad(rl.Id, rl.Title, rl.Type);
                    }
                    else if (lc != null)
                    {
                        CreateNewReferenceLoad(lc.Id, lc.Title, LoadType.Dead);
                    }
                }
            }

            return this;
        }

        #endregion

        #region Nodal Loading

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddNodalLoad`0(ILoadCase,Node,NodalLoad)']/*" />
        public IOSLoad AddNodalLoad(ILoadCase lc, Node node, NodalLoad nl)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            return AddNodalLoad(lc, new[] { node }, nl);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddNodalLoad`0(ILoadCase,IEnumerable&lt;Node&gt;,NodalLoad)']/*" />
        public IOSLoad AddNodalLoad(ILoadCase lc, IEnumerable<Node> nodes, NodalLoad nl)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            if (nodes == null) throw new ArgumentNullException(nameof(nodes));
            if (nl == null) throw new ArgumentNullException(nameof(nl));
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");

            SetLoadCaseActive(lc);
            int[] nodeIds = nodes.Where(n => n != null).Select(n => n.Id).ToArray();
            if (nodeIds.Length > 0 && nl.Forces != null)
            {
                ComObject.AddNodalLoad(nodeIds, nl.Forces.Fx, nl.Forces.Fy, nl.Forces.Fz, nl.Forces.Mx, nl.Forces.My, nl.Forces.Mz);
            }

            return this;
        }

        #endregion

        #region Member Loading

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddMemberLoad`0(ILoadCase,Beam,MemberLoad)']/*" />
        public IOSLoad AddMemberLoad(ILoadCase lc, Beam beam, MemberLoad ml)
        {
            if (beam == null) throw new ArgumentNullException(nameof(beam));
            return AddMemberLoad(lc, new[] { beam }, ml);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddMemberLoad`0(ILoadCase,IEnumerable&lt;Beam&gt;,MemberLoad)']/*" />
        public IOSLoad AddMemberLoad(ILoadCase lc, IEnumerable<Beam> beams, MemberLoad ml)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            if (beams == null) throw new ArgumentNullException(nameof(beams));
            if (ml == null) throw new ArgumentNullException(nameof(ml));
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");

            SetLoadCaseActive(lc);
            int[] beamIds = beams.Where(b => b != null).Select(b => b.Id).ToArray();
            if (beamIds.Length > 0)
            {
                ml.ApplyTo(ComObject, beamIds);
            }

            return this;
        }

        #endregion

        #region Plate Pressure Loading

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddPlateUniformPressure`0(ILoadCase,int,double,LoadDirection)']/*" />
        public IOSLoad AddPlateUniformPressure(ILoadCase lc, int plateId, double pressure, LoadDirection direction = LoadDirection.LocalZ)
        {
            return AddPlateUniformPressure(lc, new[] { plateId }, pressure, direction);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddPlateUniformPressure`0(ILoadCase,IEnumerable&lt;int&gt;,double,LoadDirection)']/*" />
        public IOSLoad AddPlateUniformPressure(ILoadCase lc, IEnumerable<int> plateIds, double pressure, LoadDirection direction = LoadDirection.LocalZ)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            if (plateIds == null) throw new ArgumentNullException(nameof(plateIds));
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");

            SetLoadCaseActive(lc);
            int[] ids = plateIds.ToArray();
            if (ids.Length > 0)
            {
                ComObject.AddElementPressure(ids, (int)direction, pressure);
            }

            return this;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddPlateUniformPressure`0(ILoadCase,IEnumerable&lt;Plate&gt;,double,LoadDirection)']/*" />
        public IOSLoad AddPlateUniformPressure(ILoadCase lc, IEnumerable<Plate> plates, double pressure, LoadDirection direction = LoadDirection.LocalZ)
        {
            if (plates == null) throw new ArgumentNullException(nameof(plates));
            return AddPlateUniformPressure(lc, plates.Where(p => p != null).Select(p => p.Id), pressure, direction);
        }

        #endregion

        /// <summary>Reads the identifiers of all load envelopes defined in the connected STAAD model.</summary>
        /// <returns>A fresh, non-null HashSet of positive, distinct envelope IDs; empty when the native count is zero. Ordering is unspecified.</returns>
        /// <remarks>
        /// <para>Reads GetEnvelopeCount and, for a positive count, GetEnvelopeIDs. The fetch return must be
        /// integer 1 (success), not the number of IDs or assignment-query zero success. Native integer counts
        /// must be nonnegative; IDs must be distinct, positive Int32 values in a one-dimensional array
        /// of exactly the reported length. Nonzero SAFEARRAY lower bounds are supported.</para>
        /// <para>The operation is eager, returns independent managed data and never activates a case,
        /// changes the active load list, computes results or modifies the model. Zero count skips fetching.
        /// Coordinate COM access on the appropriate session thread; model changes between native reads
        /// may cause validation failures. Native failures throw; they are not treated as an empty model.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">A native status, count, classification or ID array is invalid or inconsistent.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">OpenSTAAD reports a COM failure; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface lacks a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load:
        /// <code>
        /// foreach (int envelopeId in load.GetEnvelopeIds())
        ///     Console.WriteLine("Envelope " + envelopeId);
        /// </code>
        /// </example>
        public HashSet<int> GetEnvelopeIds()
        {
            int count = ReadQueryCount((object)ComObject.GetEnvelopeCount(), "GetEnvelopeCount");
            var result = new HashSet<int>();
            if (count == 0) return result;
            object buffer = new int[count];
            RequireEnvelopeQuerySuccess((object)ComObject.GetEnvelopeIDs(ref buffer), "GetEnvelopeIDs");
            foreach (int id in ReadEnvelopeQueryIds(buffer, count, "Envelope IDs")) result.Add(id);
            return result;
        }

        /// <summary>Reads one load envelope's classification, reported case count and member load-case IDs.</summary>
        /// <param name="envId">Positive native envelope ID to read directly. This is not a load-case ID; no envelope enumeration is performed.</param>
        /// <returns>A fresh LoadEnvelopeDetail with the requested ID, a defined LoadEnvelopType and an independent, non-null membership set. A valid zero-member envelope has count zero and an empty set.</returns>
        /// <remarks>
        /// <para>Calls GetLoadEnvelopeDetails with Int32 reference outputs for type and count.
        /// Its return must be integer 1. Defined native types 0 through 6 are mapped without conversion.
        /// For a positive count, calls GetLoadListfromLoadEnvelope with a preallocated Int32 array;
        /// its return must also be integer 1, and the array must have the exact reported size,
        /// distinct positive IDs and one dimension. Nonzero SAFEARRAY lower bounds are supported.
        /// A zero count skips the membership fetch.</para>
        /// <para>This eager metadata read neither activates a case nor changes the active load list.
        /// It does not resolve member cases, read their titles or factors, or compute envelope extrema.
        /// A missing positive ID is delegated to OpenSTAAD and a native failure throws rather than
        /// returning null. Type/count outputs must be written even for a valid empty envelope.</para>
        /// <para>Editing the returned model does not write to STAAD. Membership ordering is unspecified;
        /// editing its set does not synchronize LoadCasesCount or raise property notifications.
        /// Serialize native session access; concurrent model changes can produce inconsistent reads.</para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">envId is zero or negative; rejected before native access.</exception>
        /// <exception cref="InvalidOperationException">A native status, count, classification or ID array is invalid or inconsistent.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">OpenSTAAD reports a COM failure; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface lacks a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load and an existing envelope 7:
        /// <code>
        /// var envelope = load.GetLoadEnvelopeDetail(7);
        /// Console.WriteLine(envelope.Id + ": " + envelope.EnvelopType);
        /// foreach (int caseId in envelope.LoadCaseIds) Console.WriteLine(caseId);
        /// </code>
        /// </example>
        public ILoadEnvelopeDetail GetLoadEnvelopeDetail(int envId)
        {
            if (envId <= 0) throw new ArgumentOutOfRangeException(nameof(envId), "The envelope ID must be positive.");
            int type = -1;
            int count = -1;
            RequireEnvelopeQuerySuccess((object)ComObject.GetLoadEnvelopeDetails(envId, ref type, ref count), "GetLoadEnvelopeDetails");
            if (!Enum.IsDefined(typeof(LoadEnvelopType), type))
                throw new InvalidOperationException("OpenSTAAD returned an invalid envelope classification.");
            count = ReadQueryCount(count, "GetLoadEnvelopeDetails");
            var ids = new HashSet<int>();
            if (count > 0)
            {
                object buffer = new int[count];
                RequireEnvelopeQuerySuccess((object)ComObject.GetLoadListfromLoadEnvelope(envId, ref buffer), "GetLoadListfromLoadEnvelope");
                foreach (int id in ReadEnvelopeQueryIds(buffer, count, "Envelope member load-case IDs")) ids.Add(id);
            }
            return new LoadEnvelopeDetail(envId, (LoadEnvelopType)type, count, ids);
        }

        /// <summary>Eagerly reads metadata and membership for every defined load envelope.</summary>
        /// <returns>A fresh, non-null HashSet of independent LoadEnvelopeDetail objects. An envelope-free model returns an empty set; ordering is unspecified.</returns>
        /// <remarks>
        /// <para>Calls GetEnvelopeIds once, then GetLoadEnvelopeDetail for each discovered ID.
        /// All count, success-status, classification, array and membership checks from those readers
        /// apply. Each object and its membership set belongs to this result and is not a native buffer.</para>
        /// <para>Reads all envelopes before returning and throws if any envelope fails; no partial result
        /// escapes. Zero envelope count skips all detail reads. A valid zero-member envelope is retained.
        /// The native model may change between calls; this is not an atomic model snapshot.</para>
        /// <para>Leaves the active case and active load list unchanged, performs no native writes and
        /// does not calculate envelope extrema or resolve member cases. Coordinate session COM calls
        /// on the appropriate thread. Managed edits do not write to STAAD or synchronize case counts.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">A native status, count, classification or ID array is invalid or inconsistent.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">OpenSTAAD reports a COM failure; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface lacks a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load:
        /// <code>
        /// foreach (var envelope in load.GetAllLoadEnvelopeDetails())
        ///     Console.WriteLine(envelope.Id + ": " + envelope.LoadCasesCount + " cases");
        /// </code>
        /// </example>
        public HashSet<ILoadEnvelopeDetail> GetAllLoadEnvelopeDetails()
        {
            var result = new HashSet<ILoadEnvelopeDetail>();
            foreach (int id in GetEnvelopeIds()) result.Add(GetLoadEnvelopeDetail(id));
            return result;
        }

        #region Self Weight, Envelope Creation and Load List Selection

        /// <summary>Assigns directional self-weight to specified geometry in an existing primary or reference load case.</summary>
        /// <param name="loadCaseId">Positive existing target case ID; native existence is checked by OpenSTAAD.</param>
        /// <param name="lcType">PrimaryLoad or ReferenceLoad. Repeat, combination and undefined families are unsupported.</param>
        /// <param name="entities">Non-null geometry IDs for beams, plates or solids. Every ID must be positive. Input is snapshotted once; order and duplicates are preserved. Empty input makes no native calls.</param>
        /// <param name="loadDirection">Defined global self-weight direction: X=1, Y=2 or Z=3; defaults to GlobalY.</param>
        /// <param name="factor">Finite, dimensionless self-weight multiplier, default -1. Zero and positive or negative values are passed unchanged.</param>
        /// <returns>This adapter after successful validation and assignment, including the empty-input no-op.</returns>
        /// <remarks>
        /// <para>Validates all arguments and the entire input before COM access. For nonempty input,
        /// activates the requested family using checked SetLoadActive or SetReferenceLoadActive,
        /// then sends one Int32 ID array, integer direction and double factor to AddSelfWeightInXYZToGeometry.
        /// The mutation must report Boolean true or integer 1/-1 (VARIANT_TRUE); false, zero and other statuses throw.</para>
        /// <para>Calls stay on the caller's COM thread. Self-weight uses native model geometry/materials;
        /// no unit conversion, geometry lookup or managed load collection update is performed.
        /// The target case remains active after assignment, including a later native assignment failure.
        /// No save, rollback or restoration of prior activation is performed. Serialize shared-session access.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">entities is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">loadCaseId or an entity ID is nonpositive, loadDirection is undefined, or factor is non-finite.</exception>
        /// <exception cref="NotSupportedException">lcType is not PrimaryLoad or ReferenceLoad.</exception>
        /// <exception cref="InvalidOperationException">Native activation or mutation reports failure or an invalid status.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">A native COM call fails; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface lacks a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load and existing target/geometry IDs:
        /// <code>
        /// load.AddSelfweightInXYZTo(17, LoadCaseType.PrimaryLoad, new[] { 101, 102 },
        ///     SelfWeightDirection.GlobalY, factor: -1.0);
        /// </code>
        /// </example>
        public IOSLoad AddSelfweightInXYZTo(int loadCaseId, LoadCaseType lcType, IEnumerable<int> entities, SelfWeightDirection loadDirection = SelfWeightDirection.GlobalY, double factor = -1)
        {
            if (loadCaseId <= 0) throw new ArgumentOutOfRangeException(nameof(loadCaseId), "The target case ID must be positive.");
            if (lcType != LoadCaseType.PrimaryLoad && lcType != LoadCaseType.ReferenceLoad)
                throw new NotSupportedException("Self-weight assignment supports primary and reference cases only.");
            if (!Enum.IsDefined(typeof(SelfWeightDirection), loadDirection)) throw new ArgumentOutOfRangeException(nameof(loadDirection));
            if (double.IsNaN(factor) || double.IsInfinity(factor)) throw new ArgumentOutOfRangeException(nameof(factor), "The factor must be finite.");
            int[] ids = SnapshotPositiveLoadIds(entities, nameof(entities));
            if (ids.Length == 0) return this;
            ActivateQueryCase(new LoadCase(loadCaseId, string.Empty, lcType));
            RequireLoadMutationSuccess((object)ComObject.AddSelfWeightInXYZToGeometry(ids, (int)loadDirection, factor), "AddSelfWeightInXYZToGeometry");
            return this;
        }

        /// <summary>Creates a native load envelope from an explicit list of member load-case IDs.</summary>
        /// <param name="envelopId">Positive native identifier for the envelope to create.</param>
        /// <param name="envelopType">Defined LoadEnvelopType code 0 through 6, including None.</param>
        /// <param name="loadCaseList">Non-null case-ID sequence, snapshotted once. IDs must be positive; order and duplicates are preserved. An empty Int32 array is forwarded for native acceptance or rejection.</param>
        /// <returns>This adapter after OpenSTAAD reports successful envelope creation.</returns>
        /// <remarks>
        /// <para>Validates all input before one CreateLoadEnvelop call with integer envelope ID/type
        /// and an independent Int32 array. Boolean true or integer 1/-1 (VARIANT_TRUE) means success;
        /// false, zero or malformed statuses throw. Native case suitability, envelope-ID conflicts,
        /// duplicate membership and empty-envelope support are delegated to OpenSTAAD.</para>
        /// <para>No case activation, preliminary case lookup, native read-back, save or rollback occurs.
        /// This mutates the native model; query it through GetLoadEnvelopeDetail when read-back is required.
        /// Caller collections remain unchanged and returned metadata is not populated automatically.
        /// Calls run on the caller's COM thread. Failed native operations can have native side effects.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">loadCaseList is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">envelopId or a member ID is nonpositive, or envelopType is undefined.</exception>
        /// <exception cref="InvalidOperationException">Native activation or mutation reports failure or an invalid status.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">A native COM call fails; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface lacks a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load and suitable existing load cases:
        /// <code>
        /// load.CreateLoadEnvelopExt(7, LoadEnvelopType.Strength, new[] { 17, 41 });
        /// </code>
        /// </example>
        public IOSLoad CreateLoadEnvelopExt(int envelopId, LoadEnvelopType envelopType, IEnumerable<int> loadCaseList)
        {
            if (envelopId <= 0) throw new ArgumentOutOfRangeException(nameof(envelopId));
            if (!Enum.IsDefined(typeof(LoadEnvelopType), envelopType)) throw new ArgumentOutOfRangeException(nameof(envelopType));
            int[] ids = SnapshotPositiveLoadIds(loadCaseList, nameof(loadCaseList));
            RequireLoadMutationSuccess((object)ComObject.CreateLoadEnvelop(envelopId, (int)envelopType, ids), "CreateLoadEnvelop");
            return this;
        }

        /// <summary>Creates the native load-case or envelope load list from explicit IDs.</summary>
        /// <param name="loadListType">LoadList (native 0) for case IDs or EnvelopList (native 1) for envelope IDs.</param>
        /// <param name="loadCaseIds">Non-null ID sequence for the selected list type. All IDs must be positive; input is snapshotted once with order and duplicates preserved. Empty input is forwarded for native handling.</param>
        /// <returns>The original integer zero success result from CreateLoadList, preserving its boxed integer representation and the reference dynamic return signature.</returns>
        /// <remarks>
        /// <para>Validates all input before calling CreateLoadList once with an integer list type
        /// and an independent Int32 array. Bentley's native contract specifies integer 0 success
        /// and -1 failure. Boolean values, nonzero integers and malformed results throw.
        /// Native integer short, Int32 and Int64 success representations are accepted unchanged.</para>
        /// <para>This changes native load-list state and may affect subsequent commands using that list.
        /// No case activation, ID existence check, automatic analysis, save or rollback is performed.
        /// Envelope-list IDs identify envelopes rather than load cases despite the parameter's reference name.
        /// Native handling of empty input and duplicate/suitable IDs is not inferred by this wrapper.
        /// Coordinate session access on the appropriate COM thread; native failures propagate.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">loadCaseIds is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">loadListType is undefined or a supplied ID is nonpositive.</exception>
        /// <exception cref="InvalidOperationException">Native activation or mutation reports failure or an invalid status.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">A native COM call fails; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed Load interface lacks a required signature.</exception>
        /// <example>
        /// With a connected IOSLoad named load and existing envelope 7:
        /// <code>
        /// object status = load.CreateLoadList(LoadListType.EnvelopList, new[] { 7 });
        /// Console.WriteLine(status);
        /// </code>
        /// </example>
        public dynamic CreateLoadList(LoadListType loadListType, IEnumerable<int> loadCaseIds)
        {
            if (!Enum.IsDefined(typeof(LoadListType), loadListType)) throw new ArgumentOutOfRangeException(nameof(loadListType));
            int[] ids = SnapshotPositiveLoadIds(loadCaseIds, nameof(loadCaseIds));
            object status = ComObject.CreateLoadList((int)loadListType, ids);
            RequireQuerySuccess(status, "CreateLoadList");
            return status;
        }

        #endregion

        #region Load Query and Mutation Helpers

        private static int[] SnapshotPositiveLoadIds(IEnumerable<int> ids, string parameterName)
        {
            if (ids == null) throw new ArgumentNullException(parameterName);
            int[] snapshot = ids.ToArray();
            if (snapshot.Any(id => id <= 0)) throw new ArgumentOutOfRangeException(parameterName, "Every ID must be positive.");
            return snapshot;
        }

        private static void RequireLoadMutationSuccess(object status, string operation)
        {
            bool success;
            if (status is bool flag) success = flag;
            else
            {
                long code = ReadQueryInteger(status, operation);
                success = code == 1 || code == -1;
            }
            if (!success) throw new InvalidOperationException("OpenSTAAD " + operation + " reported failure.");
        }

        private static void RequireEnvelopeQuerySuccess(object status, string operation)
        {
            if (ReadQueryInteger(status, operation) != 1)
                throw new InvalidOperationException("OpenSTAAD " + operation + " failed; expected success status 1.");
        }

        private static int[] ReadEnvelopeQueryIds(object buffer, int count, string component)
        {
            int[] ids = ReadQueryArray<int>(buffer, count, component);
            var seen = new HashSet<int>();
            foreach (int id in ids)
                if (id <= 0 || !seen.Add(id))
                    throw new InvalidOperationException("OpenSTAAD returned nonpositive, duplicate or unwritten " + component + ".");
            return ids;
        }

        private HashSet<ILoadCase> ReadAllLoadCases(LoadCaseType caseType)
        {
            bool primary = caseType == LoadCaseType.PrimaryLoad;
            string family = primary ? "Primary" : "Reference";
            object countValue = primary ? ComObject.GetPrimaryLoadCaseCount() : ComObject.GetReferenceLoadCaseCount();
            int count = ReadQueryCount(countValue, "Get" + family + "LoadCaseCount");
            var result = new HashSet<ILoadCase>();
            if (count == 0) return result;

            object buffer = new int[count];
            object numberCount = primary
                ? ComObject.GetPrimaryLoadCaseNumbers(ref buffer)
                : ComObject.GetReferenceLoadCaseNumbers(ref buffer);
            if (ReadQueryInteger(numberCount, "Get" + family + "LoadCaseNumbers") != count)
                throw new InvalidOperationException("OpenSTAAD returned an inconsistent " + family.ToLowerInvariant() + " load-case count.");
            int[] ids = ReadQueryArray<int>(buffer, count, family + " load-case IDs");
            var seen = new HashSet<int>();
            foreach (int id in ids)
                if (id <= 0 || !seen.Add(id))
                    throw new InvalidOperationException("OpenSTAAD returned nonpositive or duplicate " + family.ToLowerInvariant() + " load-case IDs.");

            foreach (int id in ids) result.Add(ReadLoadCaseMetadata(id, caseType));
            return result;
        }

        private LoadCase ReadLoadCaseMetadata(int id, LoadCaseType caseType)
        {
            bool primary = caseType == LoadCaseType.PrimaryLoad;
            object titleValue = primary ? ComObject.GetLoadCaseTitle(id) : ComObject.GetReferenceLoadCaseTitle(id);
            if (!(titleValue is string title))
                throw new InvalidOperationException("OpenSTAAD returned a non-string load-case title for ID " + id + ".");
            object typeValue = primary ? ComObject.GetLoadType(id) : ComObject.GetReferenceLoadType(id);
            long typeCode = ReadQueryInteger(typeValue, primary ? "GetLoadType" : "GetReferenceLoadType");
            if (typeCode < 0 || typeCode > int.MaxValue || !Enum.IsDefined(typeof(LoadType), (int)typeCode))
                throw new InvalidOperationException("OpenSTAAD returned an invalid load type for ID " + id + ".");
            return new LoadCase(id, title, caseType, (LoadType)typeCode);
        }

        private int[] ReadCombinationCaseIds()
        {
            int count = ReadQueryCount((object)ComObject.GetLoadCombinationCaseCount(), "GetLoadCombinationCaseCount");
            if (count == 0) return new int[0];
            object buffer = new int[count];
            object numberCount = ComObject.GetLoadCombinationCaseNumbers(ref buffer);
            if (ReadQueryInteger(numberCount, "GetLoadCombinationCaseNumbers") != count)
                throw new InvalidOperationException("OpenSTAAD returned an inconsistent load-combination count.");
            int[] ids = ReadQueryArray<int>(buffer, count, "Load combination IDs");
            var seen = new HashSet<int>();
            foreach (int id in ids)
                if (id <= 0 || !seen.Add(id))
                    throw new InvalidOperationException("OpenSTAAD returned nonpositive or duplicate load-combination IDs.");
            return ids;
        }

        private LoadCase ReadCombinationCaseMetadata(int id)
        {
            object titleValue = ComObject.GetLoadCaseTitle(id);
            if (!(titleValue is string title))
                throw new InvalidOperationException("OpenSTAAD returned a non-string combination title for ID " + id + ".");
            return new LoadCase(id, title, LoadCaseType.LoadCombination, LoadType.None);
        }

        private static void ValidateCaseFilterFamily(LoadCaseType type)
        {
            if (type != LoadCaseType.PrimaryLoad && type != LoadCaseType.ReferenceLoad && type != LoadCaseType.LoadCombination)
                throw new NotSupportedException("Case filtering supports only PrimaryLoad, ReferenceLoad and LoadCombination.");
        }

        private static void ValidateLoadQuery(ILoadCase lc, int entityId, string idParameter)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            if (lc.Id <= 0) throw new ArgumentOutOfRangeException(nameof(lc), "The load case ID must be positive.");
            if (entityId <= 0) throw new ArgumentOutOfRangeException(idParameter, "The node or member ID must be positive.");
            if (lc.CaseType != LoadCaseType.PrimaryLoad && lc.CaseType != LoadCaseType.ReferenceLoad)
                throw new NotSupportedException("Load assignment queries support only primary and reference load cases.");
        }

        private void ActivateQueryCase(ILoadCase lc)
        {
            // Do not use SetLoadCaseActive here: its legacy implementation discards failure statuses.
            if (lc.CaseType == LoadCaseType.ReferenceLoad)
            {
                object status = ComObject.SetReferenceLoadActive(lc.Id);
                if (ReadQueryInteger(status, "SetReferenceLoadActive") != lc.Id)
                    throw new InvalidOperationException("OpenSTAAD could not activate the requested reference load case.");
            }
            else
            {
                object status = ComObject.SetLoadActive(lc.Id);
                bool success = status is bool flag ? flag : IsQueryTrue(status);
                if (!success) throw new InvalidOperationException("OpenSTAAD could not activate the requested primary load case.");
            }
        }

        private static bool IsQueryTrue(object status)
        {
            long value = ReadQueryInteger(status, "SetLoadActive");
            return value == 1 || value == -1; // VARIANT_TRUE may be represented as -1.
        }

        private static long ReadQueryInteger(object value, string operation)
        {
            if (value is int integer) return integer;
            if (value is short small) return small;
            if (value is long large) return large;
            throw new InvalidOperationException($"OpenSTAAD {operation} returned an invalid integer result.");
        }

        private static int ReadQueryCount(object value, string operation)
        {
            long count = ReadQueryInteger(value, operation);
            if (count < 0 || count > int.MaxValue)
                throw new InvalidOperationException($"OpenSTAAD {operation} returned an invalid load count ({count}).");
            return (int)count;
        }

        private static void RequireQuerySuccess(object status, string operation)
        {
            long code = ReadQueryInteger(status, operation);
            if (code != 0) throw new InvalidOperationException($"OpenSTAAD {operation} failed with status {code}.");
        }

        private static double[] CreateQueryBuffer(int count)
        {
            var buffer = new double[count];
            // An unwritten component must not look like a legitimate zero load.
            for (int i = 0; i < count; i++) buffer[i] = double.NaN;
            return buffer;
        }

        private static T[] ReadQueryArray<T>(object value, int count, string component)
        {
            if (!(value is Array array) || array.Rank != 1 || array.Length != count || array.GetType().GetElementType() != typeof(T))
                throw new InvalidOperationException($"OpenSTAAD returned an invalid {component} array; expected {count} {typeof(T).Name} values.");
            var result = new T[count];
            int lowerBound = array.GetLowerBound(0);
            for (int i = 0; i < count; i++) result[i] = (T)array.GetValue(lowerBound + i);
            return result;
        }

        private static double[] ReadQueryValues(object value, int count, string component)
        {
            double[] result = ReadQueryArray<double>(value, count, component);
            foreach (double item in result)
                if (double.IsNaN(item) || double.IsInfinity(item))
                    throw new InvalidOperationException($"OpenSTAAD returned a non-finite or unwritten {component} value.");
            return result;
        }

        private static LoadDirection ReadQueryDirection(int value, int maximum)
        {
            if (value < 1 || value > maximum)
                throw new InvalidOperationException($"OpenSTAAD returned an invalid load direction ({value}); expected 1 through {maximum}.");
            return (LoadDirection)value;
        }

        #endregion
    }
}
