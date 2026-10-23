// APPROVED CONTRACT SNAPSHOT: compiled into the test assembly, never the production/NuGet assembly.
using System;
using System.Collections.Generic;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Loads;
using StaadPro.Interop.Models.Loads.Contracts;
using StaadPro.Interop.Models.Loads.Queries;

namespace StaadPro.Interop.ContractReview
{
    /// <summary>Compile-checked approved signature snapshot; implementations are exposed by IOSLoad.</summary>
    /// <remarks>All 15 existing inspection operations are inherited without signature changes. Covered
    /// creation and derived combination membership are mapped in the 56-row index instead of duplicated.</remarks>
    internal interface IOSLoadExpansionContract : IOSLoad
    {
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
        new LoadOperationResult SplitLoadsOnBeam(int originalBeamId, int newBeamId);

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
        new LoadOperationResult AddCasesToEnvelope(int envelopeId, IEnumerable<int> primaryLoadCaseIds);

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
        new LoadOperationResult RemoveCasesFromEnvelope(int envelopeId, IEnumerable<int> primaryLoadCaseIds);

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
        new LoadOperationResult DeleteEnvelope(int envelopeId);

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
        new LoadOperationResult<AutomaticLoadGenerationData> CreateAutomaticCombinations(AutomaticCombinationRequest request);

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
        new LoadOperationResult<AutomaticLoadGenerationData> CreateAutomaticRepeatLoads(AutomaticRepeatLoadRequest request);

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
        new LoadOperationResult SetLoadType(IEnumerable<int> loadCaseIds, LoadType loadType);

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
        new LoadOperationResult DeleteLoadList(int loadListIndex);

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
        new LoadOperationResult SetAllowableStressAttribute(int loadCaseId, LoadStrengthType strengthType, bool increaseAllowableStress);

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
        new LoadOperationResult SetLimitStateAttribute(int loadCaseId);

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
        new LoadOperationResult RemoveLoadAttribute(int loadCaseId);

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
        new LoadQueryResult<LoadAttributeObservation> GetLoadAttribute(int loadCaseId);

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
        new LoadQueryResult<IEnumerable<int>> GetAssignedEntityIds(int primaryLoadCaseId, LoadCommandKind commandKind, int commandIndex, int maximumResults = 100000);

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
        new LoadOperationResult DeleteLoadCases(LoadCaseType caseType, IEnumerable<int> loadCaseIds);

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
        new LoadOperationResult AddSelfWeight(ILoadCase targetCase, SelfWeightDirection direction, double factor);

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
        new LoadQueryResult<NodalLoadCommandInfo> GetNodalLoadCommand(ILoadCase targetCase, int loadItemIndex);

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
        new LoadOperationResult AddMemberAreaLoad(ILoadCase targetCase, IEnumerable<int> memberIds, double areaLoad);

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
        new LoadOperationResult AddMemberFixedEndLoad(ILoadCase targetCase, IEnumerable<int> memberIds, FixedEndLoadDefinition definition);

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
        new LoadQueryResult<MemberLoadCommandInfo> GetMemberLoadCommand(ILoadCase targetCase, int loadItemIndex);

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
        new LoadOperationResult AddHydrostaticPressure(ILoadCase targetCase, IEnumerable<int> plateIds, HydrostaticPressureDefinition definition);

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
        new LoadOperationResult AddTrapezoidalPressure(ILoadCase targetCase, IEnumerable<int> plateIds, TrapezoidalPressureDefinition definition);

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
        new LoadQueryResult<int> GetPlatePressureLoadCount(ILoadCase targetCase, int plateId);

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
        new LoadQueryResult<IEnumerable<PlatePressureLoad>> GetPlatePressureLoads(ILoadCase targetCase, int plateId, int maximumResults = 100000);

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
        new LoadQueryResult<int> GetPlateConcentratedLoadCount(ILoadCase targetCase, int plateId);

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
        new LoadQueryResult<IEnumerable<PlateConcentratedLoad>> GetPlateConcentratedLoads(ILoadCase targetCase, int plateId, int maximumResults = 100000);

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
        new LoadQueryResult<PlateLoadCommandInfo> GetPlateLoadCommand(ILoadCase targetCase, int loadItemIndex);

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
        new LoadOperationResult AddFloorLoad(ILoadCase targetCase, FloorLoadDefinition definition);

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
        new LoadQueryResult<int> GetFloorBeamCount(FloorRegion region);

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
        new LoadQueryResult<IEnumerable<BeamInfluenceArea>> GetFloorInfluenceAreas(FloorRegion region, int maximumResults = 100000);

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
        new LoadOperationResult AddStrainLoad(ILoadCase targetCase, IEnumerable<int> elementIds, double axialElongation);

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
        new LoadOperationResult AddSeismicLoad(ILoadCase targetCase, GlobalAxis direction, double factor);

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
        new LoadOperationResult AddWindLoad(ILoadCase targetCase, WindLoadDefinition definition);

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
        new LoadOperationResult AddNotionalLoad(ILoadCase targetCase, IEnumerable<NotionalLoadContribution> contributions);

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
        new LoadQueryResult<int> GetReferenceContributionCount(int primaryLoadCaseId, int referenceItemIndex);

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
        new LoadQueryResult<IEnumerable<ReferenceLoadContribution>> GetReferenceContributions(int primaryLoadCaseId, int referenceItemIndex, int maximumResults = 100000);

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
        new LoadOperationResult MergeBeamLoads(IEnumerable<BeamLoadMergeRequest> requests);

    }
}
