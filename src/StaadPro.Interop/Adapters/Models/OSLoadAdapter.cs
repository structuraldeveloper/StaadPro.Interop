using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Adapters.Models
{
    /// <summary>
    /// Managed adapter for STAAD.Pro OpenSTAAD Load operations wrapping the underlying COM object.
    /// </summary>
    public class OSLoadAdapter : OSBaseAdapter, IOSLoad
    {
        public OSLoadAdapter(OpenStaadWrapper wrapper) : base(wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            ComObject = wrapper.RawLoad ?? throw new ArgumentNullException(nameof(wrapper.RawLoad), "STAAD Load COM object is not initialized.");
        }

        /// <summary>
        /// Gets the underlying COM load object.
        /// </summary>
        public dynamic ComObject { get; }

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

        #region Load Case Clearing

        public bool ClearPrimaryLoadCase(ILoadCase loadCase, bool isReferenceLoad) =>
            ClearPrimaryLoadCases(new List<ILoadCase> { loadCase }, isReferenceLoad);

        public bool ClearPrimaryLoadCases(IEnumerable<ILoadCase> loadCases, bool isReferenceLoad)
        {
            if (loadCases == null) throw new ArgumentNullException(nameof(loadCases));
            IEnumerable<ILoadCase> primaryLcs = loadCases.Where(lc => lc != null && lc.CaseType == LoadCaseType.PrimaryLoad);
            return ClearPrimaryLoadCases(primaryLcs.Select(lc => lc.Id), isReferenceLoad);
        }

        public bool ClearPrimaryLoadCases(IEnumerable<int> lcIds, bool isReferenceLoad)
        {
            if (lcIds == null) throw new ArgumentNullException(nameof(lcIds));
            int[] ids = lcIds.ToArray();
            if (ids.Length == 0) return true;
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");
            dynamic result = ComObject.ClearPrimaryLoadCase(ids, isReferenceLoad);
            return result != null && (int)result == 1;
        }

        public bool ClearPrimaryLoadCase(ILoadCase loadCase) =>
            ClearPrimaryLoadCases(new List<ILoadCase> { loadCase });

        public bool ClearPrimaryLoadCases(IEnumerable<ILoadCase> loadCases)
        {
            if (loadCases == null) throw new ArgumentNullException(nameof(loadCases));
            IEnumerable<ILoadCase> primaryLcs = loadCases.Where(lc => lc != null && lc.CaseType == LoadCaseType.PrimaryLoad);
            return ClearPrimaryLoadCases(primaryLcs.Select(lc => lc.Id));
        }

        public bool ClearPrimaryLoadCases(IEnumerable<int> lcIds)
        {
            if (lcIds == null) throw new ArgumentNullException(nameof(lcIds));
            int[] ids = lcIds.ToArray();
            if (ids.Length == 0) return true;
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");
            dynamic result = ComObject.ClearPrimaryLoadCase(ids, false);
            return result != null && (int)result == 1;
        }

        public bool ClearReferenceLoadCase(ILoadCase loadCase) =>
            ClearReferenceLoadCases(new List<ILoadCase> { loadCase });

        public bool ClearReferenceLoadCases(IEnumerable<ILoadCase> loadCases)
        {
            if (loadCases == null) throw new ArgumentNullException(nameof(loadCases));
            IEnumerable<ILoadCase> referenceLcs = loadCases.Where(lc => lc != null && lc.CaseType == LoadCaseType.ReferenceLoad);
            return ClearReferenceLoadCases(referenceLcs.Select(lc => lc.Id));
        }

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

        public int CreateNewPrimaryLoad(string lcTitle, LoadType loadType)
        {
            if (string.IsNullOrEmpty(lcTitle)) throw new ArgumentNullException(nameof(lcTitle));
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");
            dynamic result = ComObject.CreateNewPrimaryLoadEx(lcTitle, (int)loadType);
            return Convert.ToInt32(result);
        }

        public int CreateNewPrimaryLoad(int lcId, string lcTitle, LoadType loadType)
        {
            if (string.IsNullOrEmpty(lcTitle)) throw new ArgumentNullException(nameof(lcTitle));
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");
            dynamic result = ComObject.CreateNewPrimaryLoadEx2(lcTitle, (int)loadType, lcId);
            return Convert.ToInt32(result);
        }

        public int CreateNewPrimaryLoadEx(LoadCase lc)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            return CreateNewPrimaryLoad(lc.Title, lc.Type);
        }

        public int CreateNewPrimaryLoadEx2(LoadCase lc)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            return CreateNewPrimaryLoad(lc.Id, lc.Title, lc.Type);
        }

        public int CreateNewReferenceLoad(int lcId, string lcTitle, LoadType loadType)
        {
            if (string.IsNullOrEmpty(lcTitle)) throw new ArgumentNullException(nameof(lcTitle));
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");
            dynamic result = ComObject.CreateNewReferenceLoad(lcId, lcTitle, (int)loadType);
            return Convert.ToInt32(result);
        }

        public int CreateNewReferenceLoad(LoadCase lc)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            return CreateNewReferenceLoad(lc.Id, lc.Title, lc.Type);
        }

        public string GetLoadCaseTitle(int id)
        {
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");
            dynamic result = ComObject.GetLoadCaseTitle(id);
            return result != null ? result.ToString() : string.Empty;
        }

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

        public IOSLoad AddSupportSettlement(ILoadCase lc, int nodeId, SettlementDirection direction, double mmSettlement)
        {
            return AddSupportSettlement(lc, new List<int> { nodeId }, direction, mmSettlement);
        }

        public IOSLoad AddSupportSettlement(ILoadCase lc, Node node, SettlementDirection direction, double mmSettlement)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            return AddSupportSettlement(lc, new List<int> { node.Id }, direction, mmSettlement);
        }

        public IOSLoad AddSupportSettlement(ILoadCase lc, IEnumerable<Node> nodes, SettlementDirection direction, double mmSettlement)
        {
            if (nodes == null) throw new ArgumentNullException(nameof(nodes));
            return AddSupportSettlement(lc, nodes.Select(n => n.Id), direction, mmSettlement);
        }

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

        public IOSLoad CreatePrimaryLoadCases(HashSet<ILoadCase> primaryLoadCases) =>
            CreatePrimaryLoadCases((IEnumerable<ILoadCase>)primaryLoadCases);

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

        public IOSLoad CreateReferenceLoadCases(HashSet<ILoadCase> referenceLoads) =>
            CreateReferenceLoadCases((IEnumerable<ILoadCase>)referenceLoads);

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

        public IOSLoad AddNodalLoad(ILoadCase lc, Node node, NodalLoad nl)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            return AddNodalLoad(lc, new[] { node }, nl);
        }

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

        public IOSLoad AddMemberLoad(ILoadCase lc, Beam beam, MemberLoad ml)
        {
            if (beam == null) throw new ArgumentNullException(nameof(beam));
            return AddMemberLoad(lc, new[] { beam }, ml);
        }

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

        public IOSLoad AddPlateUniformPressure(ILoadCase lc, int plateId, double pressure, LoadDirection direction = LoadDirection.LocalZ)
        {
            return AddPlateUniformPressure(lc, new[] { plateId }, pressure, direction);
        }

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

        public IOSLoad AddPlateUniformPressure(ILoadCase lc, IEnumerable<Plate> plates, double pressure, LoadDirection direction = LoadDirection.LocalZ)
        {
            if (plates == null) throw new ArgumentNullException(nameof(plates));
            return AddPlateUniformPressure(lc, plates.Where(p => p != null).Select(p => p.Id), pressure, direction);
        }

        #endregion

        #region Load Query Helpers

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

            foreach (int id in ids)
            {
                object titleValue = primary ? ComObject.GetLoadCaseTitle(id) : ComObject.GetReferenceLoadCaseTitle(id);
                if (!(titleValue is string title))
                    throw new InvalidOperationException("OpenSTAAD returned a non-string load-case title for ID " + id + ".");
                object typeValue = primary ? ComObject.GetLoadType(id) : ComObject.GetReferenceLoadType(id);
                long typeCode = ReadQueryInteger(typeValue, primary ? "GetLoadType" : "GetReferenceLoadType");
                if (typeCode < 0 || typeCode > int.MaxValue || !Enum.IsDefined(typeof(LoadType), (int)typeCode))
                    throw new InvalidOperationException("OpenSTAAD returned an invalid load type for ID " + id + ".");
                result.Add(new LoadCase(id, title, caseType, (LoadType)typeCode));
            }
            return result;
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
