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

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.ClearPrimaryLoadCase`0(ILoadCase,bool)']/*" />
        bool ClearPrimaryLoadCase(ILoadCase loadCase, bool isReferenceLoad = false);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.ClearPrimaryLoadCases`0(IEnumerable&lt;ILoadCase&gt;,bool)']/*" />
        bool ClearPrimaryLoadCases(IEnumerable<ILoadCase> loadCases, bool isReferenceLoad = false);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.ClearPrimaryLoadCases`0(IEnumerable&lt;int&gt;,bool)']/*" />
        bool ClearPrimaryLoadCases(IEnumerable<int> lcIds, bool isReferenceLoad = false);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.ClearPrimaryLoadCase`0(ILoadCase)']/*" />
        bool ClearPrimaryLoadCase(ILoadCase loadCase);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.ClearPrimaryLoadCases`0(IEnumerable&lt;ILoadCase&gt;)']/*" />
        bool ClearPrimaryLoadCases(IEnumerable<ILoadCase> loadCases);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.ClearPrimaryLoadCases`0(IEnumerable&lt;int&gt;)']/*" />
        bool ClearPrimaryLoadCases(IEnumerable<int> lcIds);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.ClearReferenceLoadCase`0(ILoadCase)']/*" />
        bool ClearReferenceLoadCase(ILoadCase loadCase);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.ClearReferenceLoadCases`0(IEnumerable&lt;ILoadCase&gt;)']/*" />
        bool ClearReferenceLoadCases(IEnumerable<ILoadCase> loadCases);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.ClearReferenceLoadCases`0(IEnumerable&lt;int&gt;)']/*" />
        bool ClearReferenceLoadCases(IEnumerable<int> lcIds);

        #endregion

        #region Load Case Creation & Titles

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreateNewPrimaryLoad`0(string,LoadType)']/*" />
        int CreateNewPrimaryLoad(string lcTitle, LoadType loadType);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreateNewPrimaryLoad`0(int,string,LoadType)']/*" />
        int CreateNewPrimaryLoad(int lcId, string lcTitle, LoadType loadType);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreateNewPrimaryLoadEx`0(LoadCase)']/*" />
        int CreateNewPrimaryLoadEx(LoadCase lc);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreateNewPrimaryLoadEx2`0(LoadCase)']/*" />
        int CreateNewPrimaryLoadEx2(LoadCase lc);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreateNewReferenceLoad`0(int,string,LoadType)']/*" />
        int CreateNewReferenceLoad(int lcId, string lcTitle, LoadType loadType);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreateNewReferenceLoad`0(LoadCase)']/*" />
        int CreateNewReferenceLoad(LoadCase lc);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.GetLoadCaseTitle`0(int)']/*" />
        string GetLoadCaseTitle(int id);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.SetLoadCaseActive`0(ILoadCase)']/*" />
        IOSLoad SetLoadCaseActive(ILoadCase lc);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreateNewLoadCase`0(ILoadCase)']/*" />
        IOSLoad CreateNewLoadCase(ILoadCase lc);

        #endregion

        #region Support Settlement

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddSupportSettlement`0(ILoadCase,int,SettlementDirection,double)']/*" />
        IOSLoad AddSupportSettlement(ILoadCase lc, int nodeId, SettlementDirection direction, double mmSettlement);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddSupportSettlement`0(ILoadCase,Node,SettlementDirection,double)']/*" />
        IOSLoad AddSupportSettlement(ILoadCase lc, Node node, SettlementDirection direction, double mmSettlement);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddSupportSettlement`0(ILoadCase,IEnumerable&lt;Node&gt;,SettlementDirection,double)']/*" />
        IOSLoad AddSupportSettlement(ILoadCase lc, IEnumerable<Node> nodes, SettlementDirection direction, double mmSettlement);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddSupportSettlement`0(ILoadCase,IEnumerable&lt;int&gt;,SettlementDirection,double)']/*" />
        IOSLoad AddSupportSettlement(ILoadCase lc, IEnumerable<int> nodeIds, SettlementDirection direction, double mmSettlement);

        #endregion

        #region Batch Load Creation

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreatePrimaryLoadCases`0(HashSet&lt;ILoadCase&gt;)']/*" />
        IOSLoad CreatePrimaryLoadCases(HashSet<ILoadCase> primaryLoadCases);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreatePrimaryLoadCases`0(IEnumerable&lt;ILoadCase&gt;)']/*" />
        IOSLoad CreatePrimaryLoadCases(IEnumerable<ILoadCase> primaryLoadCases);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreateReferenceLoadCases`0(HashSet&lt;ILoadCase&gt;)']/*" />
        IOSLoad CreateReferenceLoadCases(HashSet<ILoadCase> referenceLoads);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.CreateReferenceLoadCases`0(IEnumerable&lt;ILoadCase&gt;)']/*" />
        IOSLoad CreateReferenceLoadCases(IEnumerable<ILoadCase> referenceLoads);

        #endregion

        #region Nodal Loading

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddNodalLoad`0(ILoadCase,Node,NodalLoad)']/*" />
        IOSLoad AddNodalLoad(ILoadCase lc, Node node, NodalLoad nl);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddNodalLoad`0(ILoadCase,IEnumerable&lt;Node&gt;,NodalLoad)']/*" />
        IOSLoad AddNodalLoad(ILoadCase lc, IEnumerable<Node> nodes, NodalLoad nl);

        #endregion

        #region Member Loading

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddMemberLoad`0(ILoadCase,Beam,MemberLoad)']/*" />
        IOSLoad AddMemberLoad(ILoadCase lc, Beam beam, MemberLoad ml);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddMemberLoad`0(ILoadCase,IEnumerable&lt;Beam&gt;,MemberLoad)']/*" />
        IOSLoad AddMemberLoad(ILoadCase lc, IEnumerable<Beam> beams, MemberLoad ml);

        #endregion

        #region Plate Pressure Loading

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddPlateUniformPressure`0(ILoadCase,int,double,LoadDirection)']/*" />
        IOSLoad AddPlateUniformPressure(ILoadCase lc, int plateId, double pressure, LoadDirection direction = LoadDirection.LocalZ);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddPlateUniformPressure`0(ILoadCase,IEnumerable&lt;int&gt;,double,LoadDirection)']/*" />
        IOSLoad AddPlateUniformPressure(ILoadCase lc, IEnumerable<int> plateIds, double pressure, LoadDirection direction = LoadDirection.LocalZ);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSLoad.AddPlateUniformPressure`0(ILoadCase,IEnumerable&lt;Plate&gt;,double,LoadDirection)']/*" />
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
        List<ILoadCase> GetReferenceLoadCasesFromIds(IEnumerable<int> loadCasesIds);

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
        HashSet<ILoadCase> GetAllLoadCombinationCases();

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
        ILoadCase GetLoadCombinationCaseFromId(int lcId);

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
        IEnumerable<ILoadCase> GetLoadCombinationCasesFromIds(IEnumerable<int> lcIds);

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
        IEnumerable<ILoadCase> GetAllLoadCasesOfType(LoadCaseType loadCaseType = LoadCaseType.PrimaryLoad);

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
        ILoadCase GetLoadCase(int lcId, LoadCaseType loadCaseType);

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
        IEnumerable<ILoadCase> GetLoadCases(int fromId, int toId, LoadCaseType loadCaseType);

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
        IEnumerable<ILoadCase> GetLoadCasesFromIds(IEnumerable<int> loadCasesIds, LoadCaseType loadCaseType);

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
        List<ILoadCase> GetLoadCasesOfType(LoadCaseType loadCaseType);

    }
}
