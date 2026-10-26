using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Output;
using StaadPro.Interop.Models.Output;
using StaadPro.Interop.Services.Interop;
using static StaadPro.Interop.Services.Output.OutputQueryReader;

namespace StaadPro.Interop.Services.Output
{
    // Shared documented 2024-2026 contracts only. No version-sensitive direction/layout guess is made here.
    internal sealed class OutputAdvancedReader
    {
        private readonly OutputQueryReader queries;
        internal OutputAdvancedReader(OutputQueryReader queries) { this.queries = queries; }
        private T Run<T>(Func<dynamic, T> query) => queries.WithBinding(query);
        private static int Integer(object value, string operation) => NativeValues.ReadInt32(value, operation);
        private static int Count(object value, string operation)
        { int count = Integer(value, operation); if (count < 0) throw new InvalidOperationException(operation + " returned a negative count."); return count; }
        private static double NativeNumber(object value, string operation) => value is double || value is float ? ReadNumber(value, operation) : NativeValues.ReadInt64(value, operation);
        private static void NativeIndex(int value, string name) { if (value < 0) throw new ArgumentOutOfRangeException(name); }
        private static void Station(double value) { if (double.IsNaN(value) || double.IsInfinity(value) || value < 0) throw new ArgumentOutOfRangeException("distanceFromStart"); }
        private static void EntityCase(int entityId, int loadCaseId, string name) { Positive(entityId, name); Positive(loadCaseId, nameof(loadCaseId)); }
        private static bool IsIntegral(object value) => value is sbyte || value is byte || value is short || value is ushort || value is int || value is uint || value is long || value is ulong;
        private static OutputQueryResult<T> Result<T>(string op, T data = default, OutputQueryStatus status = OutputQueryStatus.Success,
            object response = null, int? entity = null, int? loadCase = null, MemberEnd? end = null, string diagnostic = null, string parameterBlockName = null) =>
            new OutputQueryResult<T>(op, status, data, entity, loadCase, end,
                IsIntegral(response) ? Integer(response, op) : (int?)null, response is bool b ? (bool?)b : null, diagnostic, parameterBlockName);
        private static string Text(object value, string sentinel, string operation)
        { if (!(value is string text) || text == sentinel || text.IndexOf('\0') >= 0) throw new InvalidOperationException(operation + " returned unwritten or malformed text."); return text; }
        private static Vector3D Vector(double[] values) => new Vector3D(values[0], values[1], values[2]);

        internal OutputQueryResult<MemberSteelDesignResults> GetMemberSteelDesignResults(int memberId)
        {
            Positive(memberId, nameof(memberId));
            return Run(p =>
            {
                const string op = "GetMemberSteelDesignResults"; string sentinel = "<unwritten:" + Guid.NewGuid().ToString("N") + ">";
                object code = sentinel, designStatus = sentinel, ratio = double.NaN, allowable = double.NaN, load = int.MinValue,
                    position = double.NaN, clause = sentinel, section = sentinel, actions = Buffer(3), slenderness = double.NaN;
                object response = p.GetMemberSteelDesignResults(memberId, ref code, ref designStatus, ref ratio, ref allowable, ref load,
                    ref position, ref clause, ref section, ref actions, ref slenderness);
                if (!ReadFlag(response, op)) return Result<MemberSteelDesignResults>(op, status: OutputQueryStatus.NativeFailure, response: response, entity: memberId);
                string statusText = null; double? statusNumber = null; bool? statusBoolean = null; var decision = SteelDesignStatus.Unknown;
                if (designStatus is string)
                {
                    statusText = Text(designStatus, sentinel, op);
                    if (string.Equals(statusText.Trim(), "PASS", StringComparison.OrdinalIgnoreCase)) decision = SteelDesignStatus.Passed;
                    else if (string.Equals(statusText.Trim(), "FAIL", StringComparison.OrdinalIgnoreCase)) decision = SteelDesignStatus.Failed;
                }
                else if (designStatus is bool flag) statusBoolean = flag;
                else statusNumber = NativeNumber(designStatus, op + " design status");
                double actualRatio = ReadNumber(ratio, op), actualAllowable = ReadNumber(allowable, op), actualPosition = ReadNumber(position, op);
                int criticalCase = Integer(load, op);
                if (actualRatio < 0 || actualAllowable < 0 || actualPosition < 0 || criticalCase <= 0)
                    throw new InvalidOperationException(op + " returned invalid governing result data.");
                double[] values = ReadArrayValues(actions, 3, op);
                var data = new MemberSteelDesignResults(memberId, Text(code, sentinel, op), decision, statusText, statusNumber, statusBoolean,
                    actualRatio, actualAllowable, criticalCase, actualPosition, Text(clause, sentinel, op), Text(section, sentinel, op),
                    new SteelDesignForces(values[0], values[1], values[2]), ReadNumber(slenderness, op));
                return Result(op, data, response: response, entity: memberId);
            });
        }
        internal OutputQueryResult<int> GetBucklingFactorCount() => Run(p => { const string op = "GetNoOfBucklingFactors"; object raw = p.GetNoOfBucklingFactors(); return Result(op, Count(raw, op), response: raw); });
        internal OutputQueryResult<int> GetExtractedModeCount() => Run(p => { const string op = "GetNoOfModesExtracted"; object raw = p.GetNoOfModesExtracted(); return Result(op, Count(raw, op), response: raw); });
        internal OutputQueryResult<int> GetSteelDesignParameterBlockCount() => Run(p => { const string op = "GetSteelDesignParameterBlockCount"; object raw = p.GetSteelDesignParameterBlockCount(); return Result(op, Count(raw, op), response: raw); });
        internal OutputQueryResult<bool> GetMultipleSteelDesignResultAvailability() => Run(p => { const string op = "IsMultipleMemberSteelDesignResultsAvailable"; object raw = p.IsMultipleMemberSteelDesignResultsAvailable(); return Result(op, ReadFlag(raw, op, false), response: raw); });

        internal OutputQueryResult<ModeDisplacements> GetBucklingModeDisplacements(int nativeBucklingModeNumber, int nodeId) => Mode(nativeBucklingModeNumber, nodeId, true);
        internal OutputQueryResult<ModeDisplacements> GetModalNodeDisplacements(int nativeModeNumber, int nodeId) => Mode(nativeModeNumber, nodeId, false);
        private OutputQueryResult<ModeDisplacements> Mode(int mode, int nodeId, bool buckling)
        {
            NativeIndex(mode, buckling ? "nativeBucklingModeNumber" : "nativeModeNumber"); Positive(nodeId, nameof(nodeId));
            return Run(p =>
            {
                string op = buckling ? "GetBucklingModeDisplacementAtNode" : "GetModalDisplacementAtNode"; object buffer = Buffer(6);
                // Modal metadata takes the fixed-size array by value. Buckling metadata exposes a reference output.
                object raw = buckling ? (object)p.GetBucklingModeDisplacementAtNode(mode, nodeId, ref buffer) : (object)p.GetModalDisplacementAtNode(mode, nodeId, buffer);
                if (!ReadFlag(raw, op, !buckling)) return Result<ModeDisplacements>(op, status: OutputQueryStatus.NativeFailure, response: raw, entity: nodeId);
                double[] v = ReadArrayValues(buffer, 6, op);
                return Result(op, new ModeDisplacements(mode, nodeId, new Vector3D(v[0], v[1], v[2]), new Vector3D(v[3], v[4], v[5])), response: raw, entity: nodeId);
            });
        }
        internal OutputQueryResult<double> GetModeFrequency(int nativeModeNumber)
        {
            NativeIndex(nativeModeNumber, nameof(nativeModeNumber));
            return Run(p => { const string op = "GetModeFrequency"; object frequency = double.NaN; object raw = p.GetModeFrequency(nativeModeNumber, ref frequency);
                if (!ReadFlag(raw, op)) return Result<double>(op, status: OutputQueryStatus.NativeFailure, response: raw);
                double value = ReadNumber(frequency, op); if (value < 0) throw new InvalidOperationException(op + " returned a negative frequency.");
                return Result(op, value, response: raw); });
        }
        internal OutputQueryResult<ModalMassParticipation> GetModalMassParticipationFactors(int nativeModeNumber)
        {
            NativeIndex(nativeModeNumber, nameof(nativeModeNumber));
            return Run(p => { const string op = "GetModalMassParticipationFactors"; object x = double.NaN, y = double.NaN, z = double.NaN;
                object raw = p.GetModalMassParticipationFactors(nativeModeNumber, ref x, ref y, ref z);
                return ReadFlag(raw, op) ? Result(op, new ModalMassParticipation(nativeModeNumber, new Vector3D(ReadNumber(x, op), ReadNumber(y, op), ReadNumber(z, op))), response: raw)
                    : Result<ModalMassParticipation>(op, status: OutputQueryStatus.NativeFailure, response: raw); });
        }
        internal OutputQueryResult<double> GetNonlinearLoadStep(int loadCaseId)
        {
            Positive(loadCaseId, nameof(loadCaseId));
            return Run(p => { const string op = "GetNLLoadStep"; object raw = p.GetNLLoadStep(loadCaseId); double value = NativeNumber(raw, op);
                if (value >= 0) return Result(op, value, response: raw, loadCase: loadCaseId);
                var status = value == -1 ? OutputQueryStatus.NativeFailure : value == -8002 ? OutputQueryStatus.NotFound : value == -9911 ? OutputQueryStatus.ResultsUnavailable
                    : throw new InvalidOperationException(op + " returned an undocumented negative value.");
                return Result<double>(op, status: status, response: raw, loadCase: loadCaseId, diagnostic: "Native nonlinear return: " + value.ToString(System.Globalization.CultureInfo.InvariantCulture)); });
        }
        internal OutputQueryResult<Vector3D> GetRelativeMemberDisplacement(int memberId, double distanceFromStart, int loadCaseId) => Translation(memberId, distanceFromStart, loadCaseId, false);
        internal OutputQueryResult<Vector3D> GetAbsoluteMemberDisplacement(int memberId, double distanceFromStart, int loadCaseId) => Translation(memberId, distanceFromStart, loadCaseId, true);
        private OutputQueryResult<Vector3D> Translation(int memberId, double station, int loadCaseId, bool absolute)
        {
            EntityCase(memberId, loadCaseId, nameof(memberId)); Station(station);
            return Run(p => { string op = absolute ? "GetIntermediateMemberAbsTransDisplacements" : "GetIntermediateMemberTransDisplacements"; object values = Buffer(3);
                object raw = absolute ? (object)p.GetIntermediateMemberAbsTransDisplacements(memberId, station, loadCaseId, ref values) : (object)p.GetIntermediateMemberTransDisplacements(memberId, station, loadCaseId, ref values);
                return ReadFlag(raw, op) ? Result(op, Vector(ReadArrayValues(values, 3, op)), response: raw, entity: memberId, loadCase: loadCaseId)
                    : Result<Vector3D>(op, status: OutputQueryStatus.NativeFailure, response: raw, entity: memberId, loadCase: loadCaseId); });
        }
        internal OutputQueryResult<ForceComponents> GetPhysicalMemberEndForces(int physicalMemberId, MemberEnd memberEnd, int loadCaseId, ResultCoordinateSystem coordinateSystem)
        {
            EntityCase(physicalMemberId, loadCaseId, nameof(physicalMemberId));
            if (memberEnd != MemberEnd.Start && memberEnd != MemberEnd.End) throw new ArgumentOutOfRangeException(nameof(memberEnd));
            if (coordinateSystem != ResultCoordinateSystem.Local && coordinateSystem != ResultCoordinateSystem.Global) throw new ArgumentOutOfRangeException(nameof(coordinateSystem));
            return Run(p => { const string op = "GetPMemberEndForces"; object values = Buffer(6);
                object raw = p.GetPMemberEndForces(physicalMemberId, (int)memberEnd, loadCaseId, ref values, (int)coordinateSystem);
                return ReadFlag(raw, op) ? Result(op, new ForceComponents(ReadArrayValues(values, 6, op)), response: raw, entity: physicalMemberId, loadCase: loadCaseId, end: memberEnd)
                    : Result<ForceComponents>(op, status: OutputQueryStatus.NativeFailure, response: raw, entity: physicalMemberId, loadCase: loadCaseId, end: memberEnd); });
        }
        internal OutputQueryResult<ForceComponents> GetMemberForcesAtDistance(int memberId, double distanceFromStart, int loadCaseId) => Forces(memberId, distanceFromStart, loadCaseId, false);
        internal OutputQueryResult<ForceComponents> GetPhysicalMemberForcesAtDistance(int physicalMemberId, double distanceFromStart, int loadCaseId) => Forces(physicalMemberId, distanceFromStart, loadCaseId, true);
        private OutputQueryResult<ForceComponents> Forces(int id, double station, int loadCaseId, bool physical)
        {
            EntityCase(id, loadCaseId, physical ? "physicalMemberId" : "memberId"); Station(station);
            return Run(p => { string op = physical ? "GetPMemberIntermediateForcesAtDistance" : "GetIntermediateMemberForcesAtDistance"; object values = Buffer(6);
                object raw = physical ? (object)p.GetPMemberIntermediateForcesAtDistance(id, station, loadCaseId, ref values) : (object)p.GetIntermediateMemberForcesAtDistance(id, station, loadCaseId, ref values);
                return ReadFlag(raw, op) ? Result(op, new ForceComponents(ReadArrayValues(values, 6, op)), response: raw, entity: id, loadCase: loadCaseId)
                    : Result<ForceComponents>(op, status: OutputQueryStatus.NativeFailure, response: raw, entity: id, loadCase: loadCaseId); });
        }
        internal OutputQueryResult<MemberForceExtrema> GetMemberAxialForceExtrema(int memberId, int loadCaseId)
        {
            EntityCase(memberId, loadCaseId, nameof(memberId));
            return Run(p => { const string op = "GetMinMaxAxialForce"; object min = double.NaN, minPos = double.NaN, max = double.NaN, maxPos = double.NaN;
                object raw = p.GetMinMaxAxialForce(memberId, loadCaseId, ref min, ref minPos, ref max, ref maxPos);
                if (!ReadFlag(raw, op)) return Result<MemberForceExtrema>(op, status: OutputQueryStatus.NativeFailure, response: raw, entity: memberId, loadCase: loadCaseId);
                double a = ReadNumber(min, op), b = ReadNumber(minPos, op), c = ReadNumber(max, op), d = ReadNumber(maxPos, op);
                if (a > c || b < 0 || d < 0) throw new InvalidOperationException(op + " returned inconsistent extrema/stations.");
                return Result(op, new MemberForceExtrema(a, b, c, d), response: raw, entity: memberId, loadCase: loadCaseId); });
        }
        internal OutputQueryResult<MemberDeflection> GetMemberDeflectionAtDistance(int memberId, double distanceFromStart, int loadCaseId)
        {
            EntityCase(memberId, loadCaseId, nameof(memberId)); Station(distanceFromStart);
            return Run(p => { const string op = "GetIntermediateDeflectionAtDistance"; object y = double.NaN, z = double.NaN;
                object raw = p.GetIntermediateDeflectionAtDistance(memberId, distanceFromStart, loadCaseId, ref y, ref z);
                return ReadFlag(raw, op) ? Result(op, new MemberDeflection(ReadNumber(y, op), ReadNumber(z, op)), response: raw, entity: memberId, loadCase: loadCaseId)
                    : Result<MemberDeflection>(op, status: OutputQueryStatus.NativeFailure, response: raw, entity: memberId, loadCase: loadCaseId); });
        }
        internal OutputQueryResult<SolidNormalStresses> GetSolidNormalStresses(int solidId, int nativeCornerIndex, int loadCaseId) =>
            Solid(solidId, nativeCornerIndex, loadCaseId, "GetAllSolidNormalStresses", v => new SolidNormalStresses(v[0], v[1], v[2]));
        internal OutputQueryResult<SolidShearStresses> GetSolidShearStresses(int solidId, int nativeCornerIndex, int loadCaseId) =>
            Solid(solidId, nativeCornerIndex, loadCaseId, "GetAllSolidShearStresses", v => new SolidShearStresses(v[0], v[1], v[2]));
        internal OutputQueryResult<SolidPrincipalStresses> GetSolidPrincipalStresses(int solidId, int nativeCornerIndex, int loadCaseId) =>
            Solid(solidId, nativeCornerIndex, loadCaseId, "GetAllSolidPrincipalStresses", v => new SolidPrincipalStresses(v[0], v[1], v[2]));
        private OutputQueryResult<T> Solid<T>(int solidId, int corner, int loadCaseId, string op, Func<double[], T> decode)
        {
            EntityCase(solidId, loadCaseId, nameof(solidId)); NativeIndex(corner, "nativeCornerIndex");
            return Run(p => { object values = Buffer(3); object raw;
                switch (op) { case "GetAllSolidNormalStresses": raw = p.GetAllSolidNormalStresses(solidId, corner, loadCaseId, ref values); break;
                    case "GetAllSolidShearStresses": raw = p.GetAllSolidShearStresses(solidId, corner, loadCaseId, ref values); break;
                    default: raw = p.GetAllSolidPrincipalStresses(solidId, corner, loadCaseId, ref values); break; }
                return ReadFlag(raw, op) ? Result(op, decode(ReadArrayValues(values, 3, op)), response: raw, entity: solidId, loadCase: loadCaseId)
                    : Result<T>(op, status: OutputQueryStatus.NativeFailure, response: raw, entity: solidId, loadCase: loadCaseId); });
        }
        private static int[] Snapshot(IEnumerable<int> ids, int maximumResults)
        {
            if (ids == null) throw new ArgumentNullException("supportNodeIds");
            if (maximumResults <= 0 || maximumResults > 100000) throw new ArgumentOutOfRangeException(nameof(maximumResults));
            var copy = ids.Take(maximumResults + 1).ToArray();
            if (copy.Length > maximumResults) throw new ArgumentException("The support-node selection exceeds maximumResults.", "supportNodeIds");
            if (copy.Any(id => id <= 0)) throw new ArgumentOutOfRangeException("supportNodeIds");
            return copy;
        }
        internal OutputQueryResult<IReadOnlyList<MatInfluenceArea>> GetMatInfluenceAreas(IEnumerable<int> supportNodeIds, int maximumResults)
        {
            int[] ids = Snapshot(supportNodeIds, maximumResults);
            return Run(p => { const string op = "GetMatInfluenceAreas";
                if (ids.Length == 0) return Result<IReadOnlyList<MatInfluenceArea>>(op, Array.AsReadOnly(new MatInfluenceArea[0]));
                object nativeIds = (int[])ids.Clone(), yz = Buffer(ids.Length), zx = Buffer(ids.Length), xy = Buffer(ids.Length);
                // Exported metadata takes initialized arrays by value; successful retrieval must overwrite every applicable slot in place.
                object raw = p.GetMatInfluenceAreas(nativeIds, yz, zx, xy);
                bool ok; if (raw is bool flag) ok = flag; else { int code = Integer(raw, op); if (code != 0 && code != -107 && code != -108) throw new InvalidOperationException(op + " returned an undocumented status."); ok = code == 0; }
                if (!ok) return Result<IReadOnlyList<MatInfluenceArea>>(op, status: OutputQueryStatus.NativeFailure, response: raw);
                if (!ids.SequenceEqual(ReadIntIds(nativeIds, ids.Length, op))) return Result<IReadOnlyList<MatInfluenceArea>>(op, status: OutputQueryStatus.StateChanged, response: raw);
                double[] a = ReadArrayValues(yz, ids.Length, op), b = ReadArrayValues(zx, ids.Length, op), c = ReadArrayValues(xy, ids.Length, op);
                var data = ids.Select((id, i) => new MatInfluenceArea(id, a[i], b[i], c[i])).ToArray();
                return Result<IReadOnlyList<MatInfluenceArea>>(op, Array.AsReadOnly(data), response: raw); });
        }
        internal OutputQueryResult<IReadOnlyList<NodeBasePressure>> GetBasePressures(int loadCaseId, IEnumerable<int> supportNodeIds, int maximumResults)
        {
            Positive(loadCaseId, nameof(loadCaseId)); int[] ids = Snapshot(supportNodeIds, maximumResults);
            return Run(p => { const string op = "GetBasePressures";
                if (ids.Length == 0) return Result<IReadOnlyList<NodeBasePressure>>(op, Array.AsReadOnly(new NodeBasePressure[0]), loadCase: loadCaseId);
                object nativeIds = (int[])ids.Clone(), yz = Buffer(ids.Length), zx = Buffer(ids.Length), xy = Buffer(ids.Length);
                object raw = p.GetBasePressures(loadCaseId, nativeIds, yz, zx, xy);
                if (!ReadFlag(raw, op)) return Result<IReadOnlyList<NodeBasePressure>>(op, status: OutputQueryStatus.NativeFailure, response: raw, loadCase: loadCaseId);
                if (!ids.SequenceEqual(ReadIntIds(nativeIds, ids.Length, op))) return Result<IReadOnlyList<NodeBasePressure>>(op, status: OutputQueryStatus.StateChanged, response: raw, loadCase: loadCaseId);
                double[] a = ReadArrayValues(yz, ids.Length, op), b = ReadArrayValues(zx, ids.Length, op), c = ReadArrayValues(xy, ids.Length, op);
                var data = ids.Select((id, i) => new NodeBasePressure(id, a[i], b[i], c[i])).ToArray();
                return Result<IReadOnlyList<NodeBasePressure>>(op, Array.AsReadOnly(data), response: raw, loadCase: loadCaseId); });
        }
        private static int[] ReadIntIds(object values, int count, string op)
        { if (!(values is int[] ids) || ids.Length != count || ids.Any(id => id <= 0)) throw new InvalidOperationException(op + " returned malformed node correspondence."); return ids; }

        private static OutputQueryResult<TimeHistoryIntegrationInfo> Integration(dynamic p)
        {
            const string op = "GetTimeHistoryIntegrationStepInfo"; double interval = double.NaN;
            object raw = p.GetTimeHistoryIntegrationStepInfo(ref interval); int count = Integer(raw, op);
            if (count == 0 || count == -1) return Result<TimeHistoryIntegrationInfo>(op, status: count == 0 ? OutputQueryStatus.ResultsUnavailable : OutputQueryStatus.NativeFailure, response: raw);
            if (count < 0 || double.IsNaN(interval) || double.IsInfinity(interval) || interval <= 0 || double.IsInfinity((count - 1) * interval))
                throw new InvalidOperationException(op + " returned invalid integration metadata.");
            return Result(op, new TimeHistoryIntegrationInfo(count, interval), response: raw);
        }
        internal OutputQueryResult<TimeHistoryIntegrationInfo> GetTimeHistoryIntegrationInfo() => Run(p => (OutputQueryResult<TimeHistoryIntegrationInfo>)Integration(p));
        private static void ResponseArgs(int nodeId, ResponseDegreeOfFreedom dof, TimeHistoryResponseKind kind)
        { Positive(nodeId, nameof(nodeId)); if (!Enum.IsDefined(typeof(ResponseDegreeOfFreedom), dof)) throw new ArgumentOutOfRangeException("degreeOfFreedom"); if (!Enum.IsDefined(typeof(TimeHistoryResponseKind), kind)) throw new ArgumentOutOfRangeException("responseKind"); }
        private static OutputQueryStatus ResponseStatus(object raw, string op)
        {
            if (raw is bool flag) return flag ? OutputQueryStatus.Success : OutputQueryStatus.ResultsUnavailable;
            int code = Integer(raw, op);
            return code == 1 ? OutputQueryStatus.Success : code == 0 ? OutputQueryStatus.ResultsUnavailable : code == -1 ? OutputQueryStatus.NativeFailure
                : throw new InvalidOperationException(op + " returned an undocumented time-history status.");
        }
        private static bool Same(TimeHistoryIntegrationInfo a, TimeHistoryIntegrationInfo b) => a.SampleCount == b.SampleCount && a.TimeStepSeconds == b.TimeStepSeconds;
        internal OutputQueryResult<TimeHistorySample> GetTimeHistoryResponseAtTime(int nodeId, ResponseDegreeOfFreedom degreeOfFreedom, TimeHistoryResponseKind responseKind, double timeSeconds)
        {
            ResponseArgs(nodeId, degreeOfFreedom, responseKind);
            if (double.IsNaN(timeSeconds) || double.IsInfinity(timeSeconds) || timeSeconds < 0) throw new ArgumentOutOfRangeException(nameof(timeSeconds));
            return Run(p => { const string op = "GetTimeHistoryResponseAtTime"; var before = (OutputQueryResult<TimeHistoryIntegrationInfo>)Integration(p);
                if (!before.IsSuccessful) return Result<TimeHistorySample>(op, status: before.Status, entity: nodeId, diagnostic: before.NativeFunction + " prevented sample retrieval.");
                if (timeSeconds > before.Data.LastSampleTimeSeconds) throw new ArgumentOutOfRangeException(nameof(timeSeconds), "The requested time is outside the observed integration samples.");
                double value = double.NaN; object raw = p.GetTimeHistoryResponseAtTime(0, nodeId, (int)degreeOfFreedom, (int)responseKind, timeSeconds, ref value);
                var status = ResponseStatus(raw, op); if (status != OutputQueryStatus.Success) return Result<TimeHistorySample>(op, status: status, response: raw, entity: nodeId);
                double valid = ReadNumber(value, op); var after = (OutputQueryResult<TimeHistoryIntegrationInfo>)Integration(p);
                if (!after.IsSuccessful || !Same(before.Data, after.Data)) return Result<TimeHistorySample>(op, status: OutputQueryStatus.StateChanged, response: raw, entity: nodeId);
                return Result(op, new TimeHistorySample(timeSeconds, valid), response: raw, entity: nodeId); });
        }
        internal OutputQueryResult<TimeHistorySeries> GetTimeHistoryResponse(int nodeId, ResponseDegreeOfFreedom degreeOfFreedom, TimeHistoryResponseKind responseKind, int maximumSamples)
        {
            ResponseArgs(nodeId, degreeOfFreedom, responseKind);
            if (maximumSamples <= 0 || maximumSamples > 100000) throw new ArgumentOutOfRangeException(nameof(maximumSamples));
            return Run(p => { const string op = "GetTimeHistoryResponse"; var before = (OutputQueryResult<TimeHistoryIntegrationInfo>)Integration(p);
                if (!before.IsSuccessful) return Result<TimeHistorySeries>(op, status: before.Status, entity: nodeId, diagnostic: before.NativeFunction + " prevented series retrieval.");
                if (before.Data.SampleCount > maximumSamples) return Result<TimeHistorySeries>(op, status: OutputQueryStatus.LimitExceeded, entity: nodeId);
                object values = Buffer(before.Data.SampleCount); object raw = p.GetTimeHistoryResponse(0, nodeId, (int)degreeOfFreedom, (int)responseKind, ref values);
                var status = ResponseStatus(raw, op); if (status != OutputQueryStatus.Success) return Result<TimeHistorySeries>(op, status: status, response: raw, entity: nodeId);
                double[] observed = ReadArrayValues(values, before.Data.SampleCount, op); var after = (OutputQueryResult<TimeHistoryIntegrationInfo>)Integration(p);
                if (!after.IsSuccessful || !Same(before.Data, after.Data)) return Result<TimeHistorySeries>(op, status: OutputQueryStatus.StateChanged, response: raw, entity: nodeId);
                var samples = observed.Select((v, i) => new TimeHistorySample(i * before.Data.TimeStepSeconds, v)).ToArray();
                return Result(op, new TimeHistorySeries(nodeId, degreeOfFreedom, responseKind, before.Data, samples), response: raw, entity: nodeId); });
        }
        private static void BlockName(string parameterBlockName)
        {
            if (parameterBlockName == null) throw new ArgumentNullException(nameof(parameterBlockName));
            if (string.IsNullOrWhiteSpace(parameterBlockName) || parameterBlockName.IndexOf('\0') >= 0)
                throw new ArgumentException("Supply an exact nonblank parameter-block name without NUL characters.", nameof(parameterBlockName));
        }
        internal OutputQueryResult<string> GetSteelDesignParameterBlockName(int parameterBlockIndex)
        {
            NativeIndex(parameterBlockIndex, nameof(parameterBlockIndex));
            return Run(p => { const string op = "GetSteelDesignParameterBlockNameByIndex"; string sentinel = "<unwritten:" + Guid.NewGuid().ToString("N") + ">"; object name = sentinel;
                object raw = p.GetSteelDesignParameterBlockNameByIndex(parameterBlockIndex, ref name);
                if (!ReadFlag(raw, op)) return Result<string>(op, status: OutputQueryStatus.NativeFailure, response: raw);
                string value = Text(name, sentinel, op);
                if (string.IsNullOrWhiteSpace(value)) throw new InvalidOperationException(op + " returned an empty block name on success.");
                return Result(op, value, response: raw); });
        }
        internal OutputQueryResult<double> GetMemberSteelUtilizationRatio(int memberId, string parameterBlockName)
        { Positive(memberId, nameof(memberId)); BlockName(parameterBlockName);
            return Run(p => { const string op = "GetMultipleMemberSteelDesignRatio"; object value = double.NaN;
                object raw = p.GetMultipleMemberSteelDesignRatio(parameterBlockName, memberId, ref value);
                return SteelRatio(op, raw, value, memberId, parameterBlockName); }); }
        internal OutputQueryResult<double> GetMaximumMemberSteelUtilizationRatio(int memberId)
        { Positive(memberId, nameof(memberId));
            return Run(p => { const string op = "GetMultipleMemberSteelDesignMaxRatio"; object value = double.NaN;
                object raw = p.GetMultipleMemberSteelDesignMaxRatio(memberId, ref value); return SteelRatio(op, raw, value, memberId); }); }
        internal OutputQueryResult<double> GetMinimumSteelDesignFailureRatio() => Run(p => { const string op = "GetMemberSteelDesignMinFailureRatio"; object value = double.NaN;
            object raw = p.GetMemberSteelDesignMinFailureRatio(ref value); return SteelRatio(op, raw, value); });
        internal OutputQueryResult<double> GetMaximumSteelDesignFailureRatio() => Run(p => { const string op = "GetMemberSteelDesignMaxFailureRatio"; object value = double.NaN;
            object raw = p.GetMemberSteelDesignMaxFailureRatio(ref value); return SteelRatio(op, raw, value); });
        private static OutputQueryResult<double> SteelRatio(string op, object raw, object value, int? memberId = null, string blockName = null)
        {
            if (!ReadFlag(raw, op)) return Result<double>(op, status: OutputQueryStatus.NativeFailure, response: raw, entity: memberId, parameterBlockName: blockName);
            double ratio = ReadNumber(value, op); if (ratio < 0) throw new InvalidOperationException(op + " returned an undocumented negative ratio.");
            return Result(op, ratio, response: raw, entity: memberId, parameterBlockName: blockName);
        }
        internal OutputQueryResult<ParameterBlockSteelDesignResults> GetMemberSteelDesignResults(int memberId, string parameterBlockName)
        {
            Positive(memberId, nameof(memberId)); BlockName(parameterBlockName);
            return Run(p => { const string op = "GetMultipleMemberSteelDesignResults"; string sentinel = "<unwritten:" + Guid.NewGuid().ToString("N") + ">";
                object code = sentinel, status = sentinel, ratio = double.NaN, allowable = double.NaN, loadCase = int.MinValue, clause = sentinel, section = sentinel;
                object raw = p.GetMultipleMemberSteelDesignResults(parameterBlockName, memberId, ref code, ref status, ref ratio, ref allowable, ref loadCase, ref clause, ref section);
                if (!ReadFlag(raw, op)) return Result<ParameterBlockSteelDesignResults>(op, status: OutputQueryStatus.NativeFailure, response: raw, entity: memberId, parameterBlockName: parameterBlockName);
                string decisionText = Text(status, sentinel, op); SteelDesignStatus decision = string.Equals(decisionText.Trim(), "PASS", StringComparison.OrdinalIgnoreCase) ? SteelDesignStatus.Passed
                    : string.Equals(decisionText.Trim(), "FAIL", StringComparison.OrdinalIgnoreCase) ? SteelDesignStatus.Failed : SteelDesignStatus.Unknown;
                double actual = ReadNumber(ratio, op), permitted = ReadNumber(allowable, op); int criticalCase = Integer(loadCase, op);
                if (actual < 0 || permitted < 0 || criticalCase <= 0) throw new InvalidOperationException(op + " returned invalid design data.");
                var data = new ParameterBlockSteelDesignResults(memberId, parameterBlockName, Text(code, sentinel, op), decision, decisionText, actual, permitted, criticalCase, Text(clause, sentinel, op), Text(section, sentinel, op));
                return Result(op, data, response: raw, entity: memberId, parameterBlockName: parameterBlockName); });
        }
    }
}
