// REVIEW CONTRACT ONLY: compiled into the test assembly, never the production/NuGet assembly.
// Approved signatures move into IOSLoad only with a real OSLoadAdapter implementation.
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
    /// <summary>Compile-checked proposed additions to IOSLoad; not an implemented runtime API.</summary>
    /// <remarks>All 15 existing inspection operations are inherited without signature changes. Covered
    /// creation and derived combination membership are mapped in the 56-row index instead of duplicated.</remarks>
    internal interface IOSLoadExpansionContract : IOSLoad
    {
        // Native: SplitLoadsOnBeam; internal route: LoadBeamTransferService.cs
        /// <summary>Splits the original beam load definitions onto the newly created beam.</summary>
        /// <param name="originalBeamId">Positive original beam number; it must differ from newBeamId.</param>
        /// <param name="newBeamId">Positive newly created beam number; geometry creation is a separate operation.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>Topology, affected-case scope and native parameter-table inconsistency must be verified before dispatch. Review state: Qualification required. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult SplitLoadsOnBeam(int originalBeamId, int newBeamId);

        // Native: AddLoadCasesToEnvelop; internal route: LoadCaseManagementService.cs
        /// <summary>Adds explicit primary case IDs to an existing envelope.</summary>
        /// <param name="envelopeId">Positive existing envelope number.</param>
        /// <param name="primaryLoadCaseIds">Non-null, nonempty positive primary-case IDs; snapshotted once, preserving order and duplicates.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>Positive IDs; snapshot once, preserve order/duplicates; reject empty selection rather than inferring all. Native 1 means success. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult AddCasesToEnvelope(int envelopeId, IEnumerable<int> primaryLoadCaseIds);

        // Native: RemoveLoadCasesFromEnvelop; internal route: LoadCaseManagementService.cs
        /// <summary>Removes explicit primary cases from an existing envelope.</summary>
        /// <param name="envelopeId">Positive existing envelope number.</param>
        /// <param name="primaryLoadCaseIds">Non-null, nonempty positive primary-case IDs; snapshotted once, preserving order and duplicates.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>No implicit remove-all; no deletion/recreation fallback. Native 1 means success. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult RemoveCasesFromEnvelope(int envelopeId, IEnumerable<int> primaryLoadCaseIds);

        // Native: DeleteLoadEnvelop; internal route: LoadCaseManagementService.cs
        /// <summary>Deletes one existing envelope explicitly.</summary>
        /// <param name="envelopeId">Positive existing envelope number.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>Destructive scope is the envelope, not its case definitions. Native 1 success; no implicit save. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult DeleteEnvelope(int envelopeId);

        // Native: AddAutoLoadCombinations; internal route: LoadAutomaticGenerationService.cs
        /// <summary>Generates combinations using the installed code/category rules.</summary>
        /// <param name="request">Non-null typed generation request with an explicit case selection and valid parameters.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>Explicit all-cases selection. Returns requested and actual start IDs; generated count is absent because this native API does not return one. Select 2024 versus 2025/2026 error interpretation internally. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult<AutomaticLoadGenerationData> CreateAutomaticCombinations(AutomaticCombinationRequest request);

        // Native: AddAutoCombinationRepeat; internal route: LoadAutomaticGenerationService.cs
        /// <summary>Generates repeat cases with explicit reference/notional options.</summary>
        /// <param name="request">Non-null typed generation request with an explicit case selection and valid parameters.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>Returns actual start and generated count from internal reference outputs. No implicit direction flags; no inference of contiguous case IDs. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult<AutomaticLoadGenerationData> CreateAutomaticRepeatLoads(AutomaticRepeatLoadRequest request);

        // Native: SetLoadType; internal route: LoadCaseManagementService.cs
        /// <summary>Changes the engineering load classification of existing native cases.</summary>
        /// <param name="loadCaseIds">Non-null, nonempty positive case IDs; empty input never means all.</param>
        /// <param name="loadType">Defined existing LoadType engineering classification, not case family.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>Reuses LoadType 0-23; does not change primary/reference family. Native 0 success, -1 failure. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult SetLoadType(IEnumerable<int> loadCaseIds, LoadType loadType);

        // Native: DeleteLoadList; internal route: LoadCaseManagementService.cs
        /// <summary>Deletes one native LOAD LIST record.</summary>
        /// <param name="loadListIndex">Positive one-based native LOAD LIST index.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>One-based list index, not a case/envelope ID. Native 0 success and -1 failure. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult DeleteLoadList(int loadListIndex);

        // Native: SetASDLoadAttribute; internal route: LoadCaseManagementService.cs
        /// <summary>Sets an ASD strength-of-forces attribute.</summary>
        /// <param name="loadCaseId">Positive native case number.</param>
        /// <param name="strengthType">Defined native ASD strength-of-forces choice.</param>
        /// <param name="increaseAllowableStress">Explicitly allows the native one-third ASD increase when true.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>Defined LoadStrengthType 0-6; explicitly selects the native one-third stress increase. Native 0 success. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult SetAllowableStressAttribute(int loadCaseId, LoadStrengthType strengthType, bool increaseAllowableStress);

        // Native: SetLSDLoadAttribute; internal route: LoadCaseManagementService.cs
        /// <summary>Sets the LSD attribute of one case.</summary>
        /// <param name="loadCaseId">Positive native case number.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>Native 0 success; does not choose a model design code. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult SetLimitStateAttribute(int loadCaseId);

        // Native: RemoveAttribute; internal route: LoadCaseManagementService.cs
        /// <summary>Removes the native attribute of one case.</summary>
        /// <param name="loadCaseId">Positive native case number.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>Native 0 success; no implicit clearing/deletion of case content. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult RemoveLoadAttribute(int loadCaseId);

        // Native: GetAttribute; internal route: LoadCaseManagementService.cs
        /// <summary>Preserves a native attribute observation only after its result meaning is established.</summary>
        /// <param name="loadCaseId">Positive native case number.</param>
        /// <returns>Status and typed data; inspect IsSuccessful before Data. Failed or unverified responses expose no successful payload.</returns>
        /// <remarks>WITHHELD: the help defines only 0/-1 status and no attribute payload. No LoadStrengthType decoding or fabricated attribute fields; signature remains review-only. Review state: Blocked: native payload undefined. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadQueryResult<LoadAttributeObservation> GetLoadAttribute(int loadCaseId);

        // Native: GetAssignmentListForLoadType; internal route: LoadInspectionService.cs
        /// <summary>Reads the original entity assignments of a native command.</summary>
        /// <param name="primaryLoadCaseId">Positive primary-case number, activated internally with before/after identity checks.</param>
        /// <param name="commandKind">Defined LoadCommandKind, distinct from engineering LoadType and LoadItemKind.</param>
        /// <param name="commandIndex">Nonnegative zero-based index within the native command kind.</param>
        /// <param name="maximumResults">Positive retrieval limit; defaults to 100000. No silent truncation or unbounded allocation.</param>
        /// <returns>Status and typed data; inspect IsSuccessful before Data. Failed or unverified responses expose no successful payload.</returns>
        /// <remarks>Primary activation, zero-based command index, paired sizing/retrieval counts, valid IDs, bounds and state checks. Native 0 means error; do not infer empty success from it. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadQueryResult<IEnumerable<int>> GetAssignedEntityIds(int primaryLoadCaseId, LoadCommandKind commandKind, int commandIndex, int maximumResults = 100000);

        // Native: DeletePrimaryLoadCases, DeleteReferenceLoadCases; internal route: LoadCaseManagementService.cs
        /// <summary>Deletes explicitly selected primary or reference case definitions.</summary>
        /// <param name="caseType">PrimaryLoad or ReferenceLoad only; combination/repeat classifications are rejected.</param>
        /// <param name="loadCaseIds">Non-null, nonempty positive case IDs; empty input never means all.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>Only PrimaryLoad/ReferenceLoad accepted; reuse LoadCaseType instead of a new family enum. Snapshot a nonempty selection; dependency errors and possible effects are reported. This differs from clearing items. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult DeleteLoadCases(LoadCaseType caseType, IEnumerable<int> loadCaseIds);

        // Native: AddSelfWeightInXYZ; internal route: LoadAssignmentService.cs
        /// <summary>Adds whole-model self-weight to an explicitly selected case.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="direction">Defined semantic axis/direction; internal mapping uses the operation-specific numeric domain.</param>
        /// <param name="factor">Finite signed dimensionless multiplier; zero is preserved.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>Reuses existing ILoadCase and SelfWeightDirection; factor is dimensionless. Explicit geometry-scoped API remains separate; empty selection is not relevant. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult AddSelfWeight(ILoadCase targetCase, SelfWeightDirection direction, double factor);

        // Native: GetNodalLoadInfo; internal route: LoadAssignmentQueryService.cs
        /// <summary>Reads six nodal load components by original command index.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="loadItemIndex">Nonnegative original command index; native base and supported command families must be qualified.</param>
        /// <returns>Status and typed data; inspect IsSuccessful before Data. Failed or unverified responses expose no successful payload.</returns>
        /// <remarks>Native text omits the index base; proposed zero-based convention requires qualification. Uses existing Forces through a defensive copy rather than new six-number arrays. Review state: Qualification required. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadQueryResult<NodalLoadCommandInfo> GetNodalLoadCommand(ILoadCase targetCase, int loadItemIndex);

        // Native: AddMemberAreaLoad; internal route: LoadAssignmentService.cs
        /// <summary>Adds a signed area-load value to explicit members.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="memberIds">Non-null, nonempty positive member IDs; enumerated once.</param>
        /// <param name="areaLoad">Finite signed native area-load quantity, not assumed uniform force per length.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>Native 0 success; quantity follows native area-load units, not uniform force-per-length. No automatic conversion. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult AddMemberAreaLoad(ILoadCase targetCase, IEnumerable<int> memberIds, double areaLoad);

        // Native: AddMemberFixedEnd; internal route: LoadAssignmentService.cs
        /// <summary>Adds a fixed-end force/moment definition at both member ends.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="memberIds">Non-null, nonempty positive member IDs; enumerated once.</param>
        /// <param name="definition">Non-null typed definition with validated finite values and explicit mode.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>Reuses Forces; internal buffers contain Fx/Fy/Fz/Mx/My/Mz in documented order. Native 0 success; qualify axes/sign conventions. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult AddMemberFixedEndLoad(ILoadCase targetCase, IEnumerable<int> memberIds, FixedEndLoadDefinition definition);

        // Native: GetMemberLoadInfo; internal route: LoadAssignmentQueryService.cs
        /// <summary>Reads a member command packet at its original zero-based index.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="loadItemIndex">Nonnegative original command index; native base and supported command families must be qualified.</param>
        /// <returns>Status and typed data; inspect IsSuccessful before Data. Failed or unverified responses expose no successful payload.</returns>
        /// <remarks>Retains three force parameters and three distances as named fields with original item kind. Does not collapse different command families or infer dimension-independent force slots. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadQueryResult<MemberLoadCommandInfo> GetMemberLoadCommand(ILoadCase targetCase, int loadItemIndex);

        // Native: AddElementHydrostaticPressure; internal route: LoadAssignmentService.cs
        /// <summary>Adds hydrostatic pressures to explicit plates.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="plateIds">Non-null, nonempty positive plate IDs; enumerated once.</param>
        /// <param name="definition">Non-null typed definition with validated finite values and explicit mode.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>Pressure direction LocalZ/GlobalX/Y/Z; interpolation GlobalAxis maps internally to 1/2/3. Native true/1 success; distinct axes stay distinct. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult AddHydrostaticPressure(ILoadCase targetCase, IEnumerable<int> plateIds, HydrostaticPressureDefinition definition);

        // Native: AddElementTrapPressureEx; internal route: LoadAssignmentService.cs
        /// <summary>Adds two-value or joint-ordered trapezoidal plate pressures.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="plateIds">Non-null, nonempty positive plate IDs; enumerated once.</param>
        /// <param name="definition">Non-null typed definition with validated finite values and explicit mode.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>Named factories separate local X/Y variation from four joint pressures. Qualify triangular joint conventions before enabling that shape. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult AddTrapezoidalPressure(ILoadCase targetCase, IEnumerable<int> plateIds, TrapezoidalPressureDefinition definition);

        // Native: GetElementPressureLoadCount; internal route: LoadAssignmentQueryService.cs
        /// <summary>Counts pressure definitions applied to a plate in the selected case.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="plateId">Positive native plate number.</param>
        /// <returns>Status and typed data; inspect IsSuccessful before Data. Failed or unverified responses expose no successful payload.</returns>
        /// <remarks>Zero valid, -1 failure. Count belongs to selected case, not Output results. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadQueryResult<int> GetPlatePressureLoadCount(ILoadCase targetCase, int plateId);

        // Native: GetElementPressureLoads; internal route: LoadAssignmentQueryService.cs
        /// <summary>Reads ordered plate pressure definitions with local rectangles.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="plateId">Positive native plate number.</param>
        /// <param name="maximumResults">Positive retrieval limit; defaults to 100000. No silent truncation or unbounded allocation.</param>
        /// <returns>Status and typed data; inspect IsSuccessful before Data. Failed or unverified responses expose no successful payload.</returns>
        /// <remarks>Bounded count/fetch; materialized immutable results; native 0 success, -1 failure; no min/max reordering of native corner coordinates. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadQueryResult<IEnumerable<PlatePressureLoad>> GetPlatePressureLoads(ILoadCase targetCase, int plateId, int maximumResults = 100000);

        // Native: GetElementConcLoadCount; internal route: LoadAssignmentQueryService.cs
        /// <summary>Counts applied concentrated plate loads.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="plateId">Positive native plate number.</param>
        /// <returns>Status and typed data; inspect IsSuccessful before Data. Failed or unverified responses expose no successful payload.</returns>
        /// <remarks>Zero valid and -1 failure; element family/active case remains explicit. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadQueryResult<int> GetPlateConcentratedLoadCount(ILoadCase targetCase, int plateId);

        // Native: GetElementConcLoads; internal route: LoadAssignmentQueryService.cs
        /// <summary>Reads applied concentrated plate loads and their local positions.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="plateId">Positive native plate number.</param>
        /// <param name="maximumResults">Positive retrieval limit; defaults to 100000. No silent truncation or unbounded allocation.</param>
        /// <returns>Status and typed data; inspect IsSuccessful before Data. Failed or unverified responses expose no successful payload.</returns>
        /// <remarks>Reuses main ElementConcentratedLoad field semantics without its unimplemented ApplyTo or proprietary base/dependencies. Verify direction/quantity conventions. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadQueryResult<IEnumerable<PlateConcentratedLoad>> GetPlateConcentratedLoads(ILoadCase targetCase, int plateId, int maximumResults = 100000);

        // Native: GetElementLoadInfo; internal route: LoadAssignmentQueryService.cs
        /// <summary>Reads an original plate pressure-command packet.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="loadItemIndex">Nonnegative original command index; native base and supported command families must be qualified.</param>
        /// <returns>Status and typed data; inspect IsSuccessful before Data. Failed or unverified responses expose no successful payload.</returns>
        /// <remarks>Only ElementLoadPressure is documented. Translate this API’s native 0-8 directions to LoadDirection 1-9. Native -1 means not implemented, not generic success. Review state: Qualification required. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadQueryResult<PlateLoadCommandInfo> GetPlateLoadCommand(ILoadCase targetCase, int loadItemIndex);

        // Native: AddMemberFloorLoad, AddMemberFloorLoadEx; internal route: LoadAssignmentService.cs
        /// <summary>Adds a range/group/one-way floor-load request through one managed operation.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="definition">Non-null typed definition with validated finite values and explicit mode.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>Shared typed request can use Ex for general modes; legacy GlobalX/Y-range subset is conditional on qualified equivalence. No duplicate public overload solely for native-name coverage. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult AddFloorLoad(ILoadCase targetCase, FloorLoadDefinition definition);

        // Native: GetBeamCountAtFloor; internal route: LoadAssignmentQueryService.cs
        /// <summary>Counts beams in a bounded floor region.</summary>
        /// <param name="region">Non-null bounded region with finite coordinates and an explicit range axis.</param>
        /// <returns>Status and typed data; inspect IsSuccessful before Data. Failed or unverified responses expose no successful payload.</returns>
        /// <remarks>GlobalAxis range is explicitly translated to native 1/2/3. Finite coordinates may require checked Single marshaling. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadQueryResult<int> GetFloorBeamCount(FloorRegion region);

        // Native: GetInfluenceArea; internal route: LoadAssignmentQueryService.cs
        /// <summary>Reads paired beam IDs and native floor influence areas.</summary>
        /// <param name="region">Non-null bounded region with finite coordinates and an explicit range axis.</param>
        /// <param name="maximumResults">Positive retrieval limit; defaults to 100000. No silent truncation or unbounded allocation.</param>
        /// <returns>Status and typed data; inspect IsSuccessful before Data. Failed or unverified responses expose no successful payload.</returns>
        /// <remarks>Bounded count/fetch with identical region; named associations replace parallel arrays. Native true/1 success; no managed engineering-area calculation. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadQueryResult<IEnumerable<BeamInfluenceArea>> GetFloorInfluenceAreas(FloorRegion region, int maximumResults = 100000);

        // Native: AddStrainLoad; internal route: LoadAssignmentService.cs
        /// <summary>Adds initial axial elongation or shrinkage to explicit beam/plate elements.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="elementIds">Non-null, nonempty positive beam/plate IDs for the native strain operation.</param>
        /// <param name="axialElongation">Finite signed initial elongation/shrinkage in the qualified native engineering convention.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>Finite signed value; engineering dimensional conventions require native technical-reference qualification. Temperature loading is not substituted. Review state: Qualification required. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult AddStrainLoad(ILoadCase targetCase, IEnumerable<int> elementIds, double axialElongation);

        // Native: AddSeismicLoad; internal route: LoadAssignmentService.cs
        /// <summary>Applies a seismic load command in a semantic global direction.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="direction">Defined semantic axis/direction; internal mapping uses the operation-specific numeric domain.</param>
        /// <param name="factor">Finite signed dimensionless multiplier; zero is preserved.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>Reuses main GlobalAxis 0/1/2; separate from definition and member-weight domains. Native 0 success and -8001 invalid direction. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult AddSeismicLoad(ILoadCase targetCase, GlobalAxis direction, double factor);

        // Native: AddWindLoad; internal route: LoadAssignmentService.cs
        /// <summary>Applies an existing wind definition within an explicit region.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="definition">Non-null typed definition with validated finite values and explicit mode.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>Direction codes 1/3/4/6; factor signed and dimensionless. Native range assumes Y vertical; verify model compatibility. No definition creation fallback. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult AddWindLoad(ILoadCase targetCase, WindLoadDefinition definition);

        // Native: AddNotionalLoad; internal route: LoadAssignmentService.cs
        /// <summary>Adds explicit ordered primary/reference notional contributions.</summary>
        /// <param name="targetCase">Existing ILoadCase with positive ID and supported PrimaryLoad/ReferenceLoad family; metadata is snapshotted before activation.</param>
        /// <param name="contributions">Non-null, nonempty contribution records. Order and duplicates preserved; null entries rejected.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>Reuses the query contribution model with a validated public constructor. Internal packets partition families with positive IDs and global 1/2/3 axes; do not forward signed read-back IDs directly to separate family arrays. Review state: Defined; implementation pending. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult AddNotionalLoad(ILoadCase targetCase, IEnumerable<NotionalLoadContribution> contributions);

        // Native: GetNoOfSetsInReferenceLoad; internal route: LoadInspectionService.cs
        /// <summary>Counts contribution slots inside a reference-load command.</summary>
        /// <param name="primaryLoadCaseId">Positive primary-case number, activated internally with before/after identity checks.</param>
        /// <param name="referenceItemIndex">Positive one-based proposed reference-command index; exact native semantics require qualification.</param>
        /// <returns>Status and typed data; inspect IsSuccessful before Data. Failed or unverified responses expose no successful payload.</returns>
        /// <remarks>Proposed positive one-based index requires qualification; native return narrative incorrectly describes an ID. Keep contract review-only until count/index semantics are corroborated. Review state: Qualification required. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadQueryResult<int> GetReferenceContributionCount(int primaryLoadCaseId, int referenceItemIndex);

        // Native: GetReferenceLoadByIndex; internal route: LoadInspectionService.cs
        /// <summary>Reads ordered reference-case/factor pairs by reference-command index.</summary>
        /// <param name="primaryLoadCaseId">Positive primary-case number, activated internally with before/after identity checks.</param>
        /// <param name="referenceItemIndex">Positive one-based proposed reference-command index; exact native semantics require qualification.</param>
        /// <param name="maximumResults">Positive retrieval limit; defaults to 100000. No silent truncation or unbounded allocation.</param>
        /// <returns>Status and typed data; inspect IsSuccessful before Data. Failed or unverified responses expose no successful payload.</returns>
        /// <remarks>Uses a separate reference payload because existing LoadFactorContribution explicitly denotes primary repeat cases. Output size and index semantics require live corroboration. Review state: Qualification required. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadQueryResult<IEnumerable<ReferenceLoadContribution>> GetReferenceContributions(int primaryLoadCaseId, int referenceItemIndex, int maximumResults = 100000);

        // Native: BeginLoadMerging, EndLoadMerging; internal route: LoadBeamTransferService.cs
        /// <summary>Owns a bounded beam-load merge lifecycle including guaranteed cleanup attempts.</summary>
        /// <param name="requests">Non-null, nonempty retained/source-beam requests; snapshotted before begin. This contract is withheld until the native body is verified.</param>
        /// <returns>Operation-specific status and ordered native evidence, with successful typed data where present. No universal zero/one success conversion or rollback guarantee.</returns>
        /// <remarks>WITHHELD: the merge body depends on metadata-only MergeLoadsOnBeam. Void Begin/End imply invocation evidence only. Native body, topology, side effects and errors must be qualified; preserve body and cleanup failures without a user-maintained begin/end pair. Review state: Blocked: merge-body contract unverified. Calls stay on the owning COM apartment. Explicit activation remains observable; no implicit save, solver run, retry or unit conversion. Empty mutations are rejected rather than interpreted as all-model. These are signatures for review, not callable pending stubs.</remarks>
        /// <exception cref="ArgumentException">A request, ID, enum, selection or finite value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale or a native response is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; original evidence is preserved.</exception>
        LoadOperationResult MergeBeamLoads(IEnumerable<BeamLoadMergeRequest> requests);
    }
}
