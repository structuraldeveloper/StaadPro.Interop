using System;
using System.Collections.Generic;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>
    /// Contract for performing Load operations and load case management via OpenSTAAD.
    /// </summary>
    public interface IOSLoad : IOSBase
    {
        #region Load Assignment Queries

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
        List<NodalLoad> GetNodalLoads(ILoadCase lc, int nId);

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
        List<MemberUniformlyDistributedLoad> GetMemberUniformlyDistributedLoads(ILoadCase lc, int mId);

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
        List<MemberConcentratedLoad> GetMemberConcentratedLoads(ILoadCase lc, int mId);

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
        List<MemberConcentratedMoment> GetMemberConcentratedMoments(ILoadCase lc, int mId);

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
        List<MemberLinearVaryingLoad> GetMemberLinearVaryingLoads(ILoadCase lc, int mId);

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
        List<MemberTrapezoidalLoad> GetMemberTrapezoidalLoads(ILoadCase lc, int mId);

        #endregion

        #region Load Case Clearing

        /// <summary>
        /// Clears a primary load case from the active model.
        /// </summary>
        /// <param name="loadCase">The primary load case to clear.</param>
        /// <param name="isReferenceLoad">Flag indicating if the load case is reference load.</param>
        /// <returns>True if the load case was cleared successfully; otherwise false.</returns>
        bool ClearPrimaryLoadCase(ILoadCase loadCase, bool isReferenceLoad = false);

        /// <summary>
        /// Clears multiple primary load cases from the active model.
        /// </summary>
        /// <param name="loadCases">Collection of primary load cases to clear.</param>
        /// <param name="isReferenceLoad">Flag indicating if the load cases are reference loads.</param>
        /// <returns>True if the load cases were cleared successfully; otherwise false.</returns>
        bool ClearPrimaryLoadCases(IEnumerable<ILoadCase> loadCases, bool isReferenceLoad = false);

        /// <summary>
        /// Clears primary load cases by their integer identifiers.
        /// </summary>
        /// <param name="lcIds">Collection of primary load case numbers.</param>
        /// <param name="isReferenceLoad">Flag indicating if the load cases are reference loads.</param>
        /// <returns>True if the load cases were cleared successfully; otherwise false.</returns>
        bool ClearPrimaryLoadCases(IEnumerable<int> lcIds, bool isReferenceLoad = false);

        /// <summary>
        /// Clears a primary load case from the active model.
        /// </summary>
        /// <param name="loadCase">The primary load case to clear.</param>
        /// <returns>True if the primary load case was cleared successfully; otherwise false.</returns>
        bool ClearPrimaryLoadCase(ILoadCase loadCase);

        /// <summary>
        /// Clears multiple primary load cases from the active model.
        /// </summary>
        /// <param name="loadCases">Collection of primary load cases to clear.</param>
        /// <returns>True if the primary load cases were cleared successfully; otherwise false.</returns>
        bool ClearPrimaryLoadCases(IEnumerable<ILoadCase> loadCases);

        /// <summary>
        /// Clears primary load cases by their integer identifiers.
        /// </summary>
        /// <param name="lcIds">Collection of primary load case numbers.</param>
        /// <returns>True if the primary load cases were cleared successfully; otherwise false.</returns>
        bool ClearPrimaryLoadCases(IEnumerable<int> lcIds);

        /// <summary>
        /// Clears a reference load case from the active model.
        /// </summary>
        /// <param name="loadCase">The reference load case to clear.</param>
        /// <returns>True if the reference load case was cleared successfully; otherwise false.</returns>
        bool ClearReferenceLoadCase(ILoadCase loadCase);

        /// <summary>
        /// Clears multiple reference load cases from the active model.
        /// </summary>
        /// <param name="loadCases">Collection of reference load cases to clear.</param>
        /// <returns>True if the reference load cases were cleared successfully; otherwise false.</returns>
        bool ClearReferenceLoadCases(IEnumerable<ILoadCase> loadCases);

        /// <summary>
        /// Clears reference load cases by their integer identifiers.
        /// </summary>
        /// <param name="lcIds">Collection of reference load case numbers.</param>
        /// <returns>True if the reference load cases were cleared successfully; otherwise false.</returns>
        bool ClearReferenceLoadCases(IEnumerable<int> lcIds);

        #endregion

        #region Load Case Creation & Titles

        /// <summary>
        /// Creates a new primary load case in the STAAD model with automatic ID assignment.
        /// </summary>
        /// <param name="lcTitle">Descriptive title of the primary load case.</param>
        /// <param name="loadType">Engineering load type category (Dead, Live, Wind, etc.).</param>
        /// <returns>The assigned primary load case ID number.</returns>
        int CreateNewPrimaryLoad(string lcTitle, LoadType loadType);

        /// <summary>
        /// Creates a new primary load case in the STAAD model with explicit ID assignment.
        /// </summary>
        /// <param name="lcId">Explicit load case identifier number.</param>
        /// <param name="lcTitle">Descriptive title of the primary load case.</param>
        /// <param name="loadType">Engineering load type category (Dead, Live, Wind, etc.).</param>
        /// <returns>The created primary load case ID number.</returns>
        int CreateNewPrimaryLoad(int lcId, string lcTitle, LoadType loadType);

        /// <summary>
        /// Creates a new primary load case from a strongly-typed <see cref="LoadCase"/> definition.
        /// </summary>
        /// <param name="lc">Load case definition containing title and load type.</param>
        /// <returns>The assigned primary load case ID number.</returns>
        int CreateNewPrimaryLoadEx(LoadCase lc);

        /// <summary>
        /// Creates a new primary load case with explicit ID from a strongly-typed <see cref="LoadCase"/> definition.
        /// </summary>
        /// <param name="lc">Load case definition containing explicit ID, title, and load type.</param>
        /// <returns>The created primary load case ID number.</returns>
        int CreateNewPrimaryLoadEx2(LoadCase lc);

        /// <summary>
        /// Creates a new reference load case in the STAAD model.
        /// </summary>
        /// <param name="lcId">Explicit reference load case identifier number.</param>
        /// <param name="lcTitle">Descriptive title of the reference load case.</param>
        /// <param name="loadType">Engineering load type category.</param>
        /// <returns>The created reference load case ID number.</returns>
        int CreateNewReferenceLoad(int lcId, string lcTitle, LoadType loadType);

        /// <summary>
        /// Creates a new reference load case from a strongly-typed <see cref="LoadCase"/> definition.
        /// </summary>
        /// <param name="lc">Load case definition containing ID, title, and load type.</param>
        /// <returns>The created reference load case ID number.</returns>
        int CreateNewReferenceLoad(LoadCase lc);

        /// <summary>
        /// Retrieves the title or description of a load case by its integer identifier.
        /// </summary>
        /// <param name="id">Load case ID.</param>
        /// <returns>The string title of the load case.</returns>
        string GetLoadCaseTitle(int id);

        /// <summary>
        /// Activates the specified load case in the STAAD model for subsequent load application or querying.
        /// </summary>
        /// <param name="lc">The load case to activate.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad SetLoadCaseActive(ILoadCase lc);

        /// <summary>
        /// Creates a new load case (primary or reference) based on the case type of the provided <see cref="ILoadCase"/>.
        /// </summary>
        /// <param name="lc">The load case to create.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad CreateNewLoadCase(ILoadCase lc);

        #endregion

        #region Support Settlement

        /// <summary>
        /// Applies support settlement/displacement to a single node by node ID.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="nodeId">Target node ID.</param>
        /// <param name="direction">Settlement direction (Fx, Fy, Fz, Mx, My, Mz).</param>
        /// <param name="mmSettlement">Settlement displacement in millimeters.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddSupportSettlement(ILoadCase lc, int nodeId, SettlementDirection direction, double mmSettlement);

        /// <summary>
        /// Applies support settlement/displacement to a single node.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="node">Target node entity.</param>
        /// <param name="direction">Settlement direction (Fx, Fy, Fz, Mx, My, Mz).</param>
        /// <param name="mmSettlement">Settlement displacement in millimeters.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddSupportSettlement(ILoadCase lc, Node node, SettlementDirection direction, double mmSettlement);

        /// <summary>
        /// Applies support settlement/displacement to multiple nodes.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="nodes">Target node entities.</param>
        /// <param name="direction">Settlement direction (Fx, Fy, Fz, Mx, My, Mz).</param>
        /// <param name="mmSettlement">Settlement displacement in millimeters.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddSupportSettlement(ILoadCase lc, IEnumerable<Node> nodes, SettlementDirection direction, double mmSettlement);

        /// <summary>
        /// Applies support settlement/displacement to multiple nodes by node IDs.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="nodeIds">Target node IDs.</param>
        /// <param name="direction">Settlement direction (Fx, Fy, Fz, Mx, My, Mz).</param>
        /// <param name="mmSettlement">Settlement displacement in millimeters.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddSupportSettlement(ILoadCase lc, IEnumerable<int> nodeIds, SettlementDirection direction, double mmSettlement);

        #endregion

        #region Batch Load Creation

        /// <summary>
        /// Creates a batch of primary load cases in the active STAAD model.
        /// </summary>
        /// <param name="primaryLoadCases">Collection of primary load cases to create.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad CreatePrimaryLoadCases(HashSet<ILoadCase> primaryLoadCases);

        /// <summary>
        /// Creates a batch of primary load cases in the active STAAD model.
        /// </summary>
        /// <param name="primaryLoadCases">Sequence of primary load cases to create.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad CreatePrimaryLoadCases(IEnumerable<ILoadCase> primaryLoadCases);

        /// <summary>
        /// Creates a batch of reference load cases in the active STAAD model.
        /// </summary>
        /// <param name="referenceLoads">Collection of reference load cases to create.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad CreateReferenceLoadCases(HashSet<ILoadCase> referenceLoads);

        /// <summary>
        /// Creates a batch of reference load cases in the active STAAD model.
        /// </summary>
        /// <param name="referenceLoads">Sequence of reference load cases to create.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad CreateReferenceLoadCases(IEnumerable<ILoadCase> referenceLoads);

        #endregion

        #region Nodal Loading

        /// <summary>
        /// Applies a concentrated force and/or moment to a single node.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="node">Target node entity.</param>
        /// <param name="nl">Nodal load definition containing 6-DOF forces and moments.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddNodalLoad(ILoadCase lc, Node node, NodalLoad nl);

        /// <summary>
        /// Applies a concentrated force and/or moment to multiple nodes.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="nodes">Target node entities.</param>
        /// <param name="nl">Nodal load definition containing 6-DOF forces and moments.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddNodalLoad(ILoadCase lc, IEnumerable<Node> nodes, NodalLoad nl);

        #endregion

        #region Member Loading

        /// <summary>
        /// Applies a member load (uniformly distributed load or concentrated point load) to a single beam element.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="beam">Target beam entity.</param>
        /// <param name="ml">Member load definition.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddMemberLoad(ILoadCase lc, Beam beam, MemberLoad ml);

        /// <summary>
        /// Applies a member load (uniformly distributed load or concentrated point load) to multiple beam elements.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="beams">Collection of target beam entities.</param>
        /// <param name="ml">Member load definition.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddMemberLoad(ILoadCase lc, IEnumerable<Beam> beams, MemberLoad ml);

        #endregion

        #region Plate Pressure Loading

        /// <summary>
        /// Applies a uniform surface pressure load to a single plate element by plate ID.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="plateId">Target plate element ID.</param>
        /// <param name="pressure">Pressure magnitude (force per unit area).</param>
        /// <param name="direction">Direction of pressure application (LocalZ, GlobalX, GlobalY, GlobalZ).</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddPlateUniformPressure(ILoadCase lc, int plateId, double pressure, LoadDirection direction = LoadDirection.LocalZ);

        /// <summary>
        /// Applies a uniform surface pressure load to multiple plate elements by their integer IDs.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="plateIds">Collection of target plate IDs.</param>
        /// <param name="pressure">Pressure magnitude (force per unit area).</param>
        /// <param name="direction">Direction of pressure application (LocalZ, GlobalX, GlobalY, GlobalZ).</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddPlateUniformPressure(ILoadCase lc, IEnumerable<int> plateIds, double pressure, LoadDirection direction = LoadDirection.LocalZ);

        /// <summary>
        /// Applies a uniform surface pressure load to multiple plate entities.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="plates">Collection of target Plate entities.</param>
        /// <param name="pressure">Pressure magnitude (force per unit area).</param>
        /// <param name="direction">Direction of pressure application (LocalZ, GlobalX, GlobalY, GlobalZ).</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddPlateUniformPressure(ILoadCase lc, IEnumerable<Plate> plates, double pressure, LoadDirection direction = LoadDirection.LocalZ);

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
        List<MemberUniformMoment> GetMemberUniformMoments(ILoadCase lc, int mId);

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
        HashSet<ILoadCase> GetAllPrimaryLoadCases();

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
        HashSet<ILoadCase> GetAllReferenceLoadCases();

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
        ILoadCase GetPrimaryLoadCaseFromId(int lcId);

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
        ILoadCase GetReferenceLoadCaseFromId(int lcId);

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
        List<ILoadCase> GetPrimaryLoadCasesFromIds(IEnumerable<int> loadCasesIds);

    }
}
