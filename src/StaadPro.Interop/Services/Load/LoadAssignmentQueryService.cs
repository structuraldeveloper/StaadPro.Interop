using System;
using System.Collections.Generic;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Loads;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Loads.Contracts;
using StaadPro.Interop.Models.Loads.Queries;

namespace StaadPro.Interop.Services.Load
{
    internal sealed class LoadAssignmentQueryService
    {
        private readonly LoadServiceContext context;
        internal LoadAssignmentQueryService(LoadServiceContext context) { this.context = context; }
        private static LoadQueryResult<T> Result<T>(string op, LoadQueryStatus status, T data = default, int? code = null, string observed = null) =>
            LoadServiceContext.Query(op, status, data, code, observed);
        private LoadQueryResult<T> Item<T>(ILoadCase targetCase, int index, string op, Func<LoadItemKind, bool> supports, Func<LoadItemKind, LoadQueryResult<T>> read)
        {
            var target = LoadServiceContext.Target(targetCase); LoadServiceContext.Index(index, nameof(index));
            return context.ReadActive(target, op, () =>
            {
                int count = LoadServiceContext.Count((object)context.Binding().GetLoadItemsCount(target.Id), "GetLoadItemsCount");
                if (count == -1) return Result<T>(op, LoadQueryStatus.NativeFailure, code: count, observed: "GetLoadItemsCount");
                if (index >= count) return Result<T>(op, LoadQueryStatus.NotFound, code: count, observed: "GetLoadItemsCount");
                int kind = LoadServiceContext.Integer((object)context.Binding().GetLoadItemType(target.Id, index), "GetLoadItemType");
                if (kind == 0) return Result<T>(op, LoadQueryStatus.NotFound, code: kind, observed: "GetLoadItemType");
                if (kind < 0) throw new InvalidOperationException("GetLoadItemType returned an undocumented negative item code.");
                if (!supports((LoadItemKind)kind)) return Result<T>(op, LoadQueryStatus.Unsupported, code: kind, observed: "GetLoadItemType");
                var result = read((LoadItemKind)kind);
                int afterCount = LoadServiceContext.Count((object)context.Binding().GetLoadItemsCount(target.Id), "GetLoadItemsCount");
                if (afterCount != count) return Result<T>(op, LoadQueryStatus.StateChanged, code: afterCount, observed: "GetLoadItemsCount");
                int afterKind = LoadServiceContext.Integer((object)context.Binding().GetLoadItemType(target.Id, index), "GetLoadItemType");
                return afterKind == kind ? result : Result<T>(op, LoadQueryStatus.StateChanged, code: afterKind, observed: "GetLoadItemType");
            });
        }
        internal LoadQueryResult<NodalLoadCommandInfo> GetNodalLoadCommand(ILoadCase targetCase, int loadItemIndex) =>
            Item(targetCase, loadItemIndex, "GetNodalLoadInfo", kind => kind == LoadItemKind.Nodal, kind =>
            {
                const string op = "GetNodalLoadInfo"; object force = LoadServiceContext.Doubles(6);
                var step = LoadServiceContext.Step(op, (object)context.Binding().GetNodalLoadInfo(loadItemIndex, ref force), 1, true);
                if (step.Status != LoadOperationStatus.Success) return Result<NodalLoadCommandInfo>(op, LoadQueryStatus.NativeFailure, code: step.NativeReturnCode);
                var f = LoadServiceContext.ArrayValues<double>(force, 6, op);
                return Result(op, LoadQueryStatus.Success, new NodalLoadCommandInfo(loadItemIndex, new Forces(f[0], f[1], f[2], f[3], f[4], f[5])), step.NativeReturnCode);
            });
        internal LoadQueryResult<MemberLoadCommandInfo> GetMemberLoadCommand(ILoadCase targetCase, int loadItemIndex) =>
            Item(targetCase, loadItemIndex, "GetMemberLoadInfo", kind => kind == LoadItemKind.MemberUniformForce || kind == LoadItemKind.MemberUniformMoment ||
                kind == LoadItemKind.MemberConcentratedForce || kind == LoadItemKind.MemberConcentratedMoment || kind == LoadItemKind.MemberLinear || kind == LoadItemKind.MemberTrapezoidal, kind =>
            {
                const string op = "GetMemberLoadInfo"; object direction = int.MinValue, force = LoadServiceContext.Doubles(3), distance = LoadServiceContext.Doubles(3);
                var step = LoadServiceContext.Step(op, (object)context.Binding().GetMemberLoadInfo(loadItemIndex, ref direction, ref force, ref distance), 1, true);
                if (step.Status != LoadOperationStatus.Success) return Result<MemberLoadCommandInfo>(op, LoadQueryStatus.NativeFailure, code: step.NativeReturnCode);
                var f = LoadServiceContext.ArrayValues<double>(force, 3, op); var d = LoadServiceContext.ArrayValues<double>(distance, 3, op);
                var axis = LoadServiceContext.Direction(LoadServiceContext.Integer(direction, op), op);
                return Result(op, LoadQueryStatus.Success, new MemberLoadCommandInfo(loadItemIndex, kind, axis, f[0], f[1], f[2], d[0], d[1], d[2]), step.NativeReturnCode);
            });
        internal LoadQueryResult<PlateLoadCommandInfo> GetPlateLoadCommand(ILoadCase targetCase, int loadItemIndex) =>
            Item(targetCase, loadItemIndex, "GetElementLoadInfo", kind => kind == LoadItemKind.PlateFullPressure, kind =>
            {
                const string op = "GetElementLoadInfo"; object direction = int.MinValue, force = LoadServiceContext.Doubles(4), distance = LoadServiceContext.Doubles(4);
                int code = LoadServiceContext.Integer((object)context.Binding().GetElementLoadInfo(loadItemIndex, ref direction, ref force, ref distance), op);
                if (code == -1) return Result<PlateLoadCommandInfo>(op, LoadQueryStatus.Unsupported, code: code);
                if (code == 0) return Result<PlateLoadCommandInfo>(op, LoadQueryStatus.NativeFailure, code: code);
                if (code != 1) throw new InvalidOperationException(op + " returned an undocumented status.");
                int axis = LoadServiceContext.Integer(direction, op); if (axis < 0 || axis > 8) throw new InvalidOperationException(op + " returned an invalid or unwritten direction.");
                var f = LoadServiceContext.ArrayValues<double>(force, 4, op); var d = LoadServiceContext.ArrayValues<double>(distance, 4, op);
                return Result(op, LoadQueryStatus.Success, new PlateLoadCommandInfo(loadItemIndex, (LoadDirection)(axis + 1), f[0], f[1], f[2], f[3], d[0], d[1], d[2], d[3]), code);
            });
        private int PlateCount(bool pressure, int plateId) => pressure
            ? LoadServiceContext.Count((object)context.Binding().GetElementPressureLoadCount(plateId), "GetElementPressureLoadCount")
            : LoadServiceContext.Count((object)context.Binding().GetElementConcLoadCount(plateId), "GetElementConcLoadCount");
        private LoadQueryResult<int> CountPlate(ILoadCase targetCase, int plateId, bool pressure)
        {
            var target = LoadServiceContext.Target(targetCase); LoadContractArguments.Positive(plateId, nameof(plateId));
            string op = pressure ? "GetElementPressureLoadCount" : "GetElementConcLoadCount";
            return context.ReadActive(target, op, () =>
            {
                int count = PlateCount(pressure, plateId);
                return Result(op, count == -1 ? LoadQueryStatus.NativeFailure : LoadQueryStatus.Success, count, count);
            });
        }
        internal LoadQueryResult<int> GetPlatePressureLoadCount(ILoadCase targetCase, int plateId) => CountPlate(targetCase, plateId, true);
        internal LoadQueryResult<int> GetPlateConcentratedLoadCount(ILoadCase targetCase, int plateId) => CountPlate(targetCase, plateId, false);
        internal LoadQueryResult<IEnumerable<PlatePressureLoad>> GetPlatePressureLoads(ILoadCase targetCase, int plateId, int maximumResults) =>
            ReadPlate(targetCase, plateId, maximumResults, true, (direction, values, i) => new PlatePressureLoad(direction, values[0][i], values[1][i], values[2][i], values[3][i], values[4][i]));
        internal LoadQueryResult<IEnumerable<PlateConcentratedLoad>> GetPlateConcentratedLoads(ILoadCase targetCase, int plateId, int maximumResults) =>
            ReadPlate(targetCase, plateId, maximumResults, false, (direction, values, i) => new PlateConcentratedLoad(direction, values[0][i], values[1][i], values[2][i]));
        private LoadQueryResult<IEnumerable<T>> ReadPlate<T>(ILoadCase targetCase, int plateId, int maximumResults, bool pressure, Func<LoadDirection, double[][], int, T> make)
        {
            var target = LoadServiceContext.Target(targetCase); LoadContractArguments.Positive(plateId, nameof(plateId)); LoadContractArguments.Positive(maximumResults, nameof(maximumResults));
            string op = pressure ? "GetElementPressureLoads" : "GetElementConcLoads", countOp = pressure ? "GetElementPressureLoadCount" : "GetElementConcLoadCount";
            return context.ReadActive(target, op, () =>
            {
                int count = PlateCount(pressure, plateId);
                if (count == -1) return Result<IEnumerable<T>>(op, LoadQueryStatus.NativeFailure, code: count, observed: countOp);
                if (count > maximumResults) return Result<IEnumerable<T>>(op, LoadQueryStatus.LimitExceeded, code: count, observed: countOp);
                var data = new T[count]; int code = 0;
                if (count > 0)
                {
                    object direction = new int[count], w = LoadServiceContext.Doubles(count), x1 = LoadServiceContext.Doubles(count), y1 = LoadServiceContext.Doubles(count), x2 = LoadServiceContext.Doubles(count), y2 = LoadServiceContext.Doubles(count);
                    code = LoadServiceContext.Integer(pressure ? (object)context.Binding().GetElementPressureLoads(plateId, ref direction, ref w, ref x1, ref y1, ref x2, ref y2)
                        : (object)context.Binding().GetElementConcLoads(plateId, ref direction, ref w, ref x1, ref y1), op);
                    if (code == -1) return Result<IEnumerable<T>>(op, LoadQueryStatus.NativeFailure, code: code);
                    if (code != 0) throw new InvalidOperationException(op + " returned an undocumented status.");
                    int[] directions = LoadServiceContext.ArrayValues<int>(direction, count, op);
                    var values = pressure ? new[] { LoadServiceContext.ArrayValues<double>(w, count, op), LoadServiceContext.ArrayValues<double>(x1, count, op), LoadServiceContext.ArrayValues<double>(y1, count, op), LoadServiceContext.ArrayValues<double>(x2, count, op), LoadServiceContext.ArrayValues<double>(y2, count, op) }
                        : new[] { LoadServiceContext.ArrayValues<double>(w, count, op), LoadServiceContext.ArrayValues<double>(x1, count, op), LoadServiceContext.ArrayValues<double>(y1, count, op) };
                    for (int i = 0; i < count; i++) data[i] = make(LoadServiceContext.Direction(directions[i], op), values, i);
                }
                int after = PlateCount(pressure, plateId);
                return after == count ? Result<IEnumerable<T>>(op, LoadQueryStatus.Success, Array.AsReadOnly(data), code, count == 0 ? countOp : op)
                    : Result<IEnumerable<T>>(op, LoadQueryStatus.StateChanged, code: after, observed: countOp);
            });
        }
        private static float Coordinate(double value)
        {
            if (value < -float.MaxValue || value > float.MaxValue) throw new ArgumentOutOfRangeException(nameof(value), "Floor observations require coordinates representable as native Single values.");
            return (float)value;
        }
        private static float[] Coordinates(FloorRegion region)
        {
            if (region == null) throw new ArgumentNullException(nameof(region)); var b = region.Bounds;
            return new[] { Coordinate(b.MinimumX), Coordinate(b.MaximumX), Coordinate(b.MinimumY), Coordinate(b.MaximumY), Coordinate(b.MinimumZ), Coordinate(b.MaximumZ) };
        }
        private int FloorCount(float[] b, int axis) => LoadServiceContext.Count((object)context.Binding().GetBeamCountAtFloor(b[0], b[1], b[2], b[3], b[4], b[5], axis), "GetBeamCountAtFloor");
        internal LoadQueryResult<int> GetFloorBeamCount(FloorRegion region)
        {
            var b = Coordinates(region); int axis = (int)region.RangeAxis + 1;
            return context.Run(() => { int count = FloorCount(b, axis); return Result("GetBeamCountAtFloor", count == -1 ? LoadQueryStatus.NativeFailure : LoadQueryStatus.Success, count, count); });
        }
        internal LoadQueryResult<IEnumerable<BeamInfluenceArea>> GetFloorInfluenceAreas(FloorRegion region, int maximumResults)
        {
            var b = Coordinates(region); int axis = (int)region.RangeAxis + 1; LoadContractArguments.Positive(maximumResults, nameof(maximumResults));
            return context.Run(() =>
            {
                const string op = "GetInfluenceArea"; int count = FloorCount(b, axis);
                if (count == -1) return Result<IEnumerable<BeamInfluenceArea>>(op, LoadQueryStatus.NativeFailure, code: count, observed: "GetBeamCountAtFloor");
                if (count > maximumResults) return Result<IEnumerable<BeamInfluenceArea>>(op, LoadQueryStatus.LimitExceeded, code: count, observed: "GetBeamCountAtFloor");
                var data = new BeamInfluenceArea[count]; int? code = 0;
                if (count > 0)
                {
                    object ids = new int[count], areas = LoadServiceContext.Doubles(count);
                    var step = LoadServiceContext.Step(op, (object)context.Binding().GetInfluenceArea(b[0], b[1], b[2], b[3], b[4], b[5], axis, ref ids, ref areas), 1, true); code = step.NativeReturnCode;
                    if (step.Status != LoadOperationStatus.Success) return Result<IEnumerable<BeamInfluenceArea>>(op, LoadQueryStatus.NativeFailure, code: code);
                    var numbers = LoadServiceContext.ArrayValues<int>(ids, count, op); var values = LoadServiceContext.ArrayValues<double>(areas, count, op);
                    for (int i = 0; i < count; i++)
                    {
                        if (values[i] < 0) throw new InvalidOperationException(op + " returned a negative influence area.");
                        data[i] = new BeamInfluenceArea(LoadServiceContext.NativeId(numbers[i], op), values[i]);
                    }
                }
                int after = FloorCount(b, axis);
                return after == count ? Result<IEnumerable<BeamInfluenceArea>>(op, LoadQueryStatus.Success, Array.AsReadOnly(data), code, count == 0 ? "GetBeamCountAtFloor" : op)
                    : Result<IEnumerable<BeamInfluenceArea>>(op, LoadQueryStatus.StateChanged, code: after, observed: "GetBeamCountAtFloor");
            });
        }
    }
}
