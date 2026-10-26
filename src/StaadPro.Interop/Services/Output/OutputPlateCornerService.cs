using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Output;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Output;
using StaadPro.Interop.Services.Interop;
using StaadPro.Interop.Services.Output.Coordinates;
using static StaadPro.Interop.Services.Output.OutputQueryReader;
namespace StaadPro.Interop.Services.Output
{
    internal sealed class OutputPlateCornerService
    {
        private const int Limit = 100000;
        private const string NativeName = "GetPlateCornerForces";
        private readonly OpenStaadWrapper wrapper;
        private readonly OutputQueryReader queries;
        internal OutputPlateCornerService(OpenStaadWrapper wrapper, OutputQueryReader queries)
        {
            this.wrapper = wrapper;
            this.queries = queries;
        }
        private sealed class Corner
        {
            internal int Id; internal Point3D Point;
        }
        private sealed class PlateSnapshot
        {
            internal int Id; internal Corner[] Corners;
        }
        private sealed class Request
        {
            internal PlateCornerForcesKey Key; internal OutputLoadCaseIdentity Case; internal PlateSnapshot Plate;
        }
        private sealed class Scope
        {
            private readonly OpenStaadWrapper wrapper;
            private readonly OutputQueryReader queries;
            private readonly object geometry, load;
            internal readonly List<OutputObservation> Evidence = new List<OutputObservation>();
            internal readonly Dictionary<int, PlateSnapshot> Plates = new Dictionary<int, PlateSnapshot>();
            private readonly Dictionary<LoadCaseType, OutputLoadCaseIdentity[]> cases = new Dictionary<LoadCaseType, OutputLoadCaseIdentity[]>();
            internal readonly IOSLoad Load;
            internal string MetadataDiagnostic;
            internal Scope(OpenStaadWrapper wrapper, OutputQueryReader queries, bool needGeometry, bool needLoad)
            {
                this.wrapper = wrapper;
                this.queries = queries;
                if (needGeometry)
                    geometry = (object)wrapper.RawGeometry ?? throw new InvalidOperationException("The current Geometry binding is required.");
                if (needLoad)
                {
                    Load = wrapper.Load ?? throw new InvalidOperationException("The current Load binding is required.");
                    load = (object)wrapper.RawLoad;
                }
                Check();
            }
            internal dynamic Geometry => geometry;
            internal void Check()
            {
                queries.WithBinding(p => 0);
                if (geometry != null && !ReferenceEquals(geometry, (object)wrapper.RawGeometry) || load != null && !ReferenceEquals(load, (object)wrapper.RawLoad))
                    throw new InvalidOperationException("A participating plate-corner binding was replaced; reacquire the adapters.");
            }
            private OutputLoadCaseIdentity[] Cases(LoadCaseType family)
            {
                if (cases.TryGetValue(family, out var found))
                    return found;
                Check();
                dynamic receiver = load;
                string function = family == LoadCaseType.PrimaryLoad ? "GetPrimaryLoadCaseCount" : family == LoadCaseType.ReferenceLoad ? "GetReferenceLoadCaseCount" : "GetLoadCombinationCaseCount";
                object raw = family == LoadCaseType.PrimaryLoad ? receiver.GetPrimaryLoadCaseCount() : family == LoadCaseType.ReferenceLoad ? receiver.GetReferenceLoadCaseCount() : receiver.GetLoadCombinationCaseCount();
                Check();
                int count = NativeValues.ReadInt32(raw, function);
                if (count < 0)
                    throw new InvalidOperationException("Failed Load metadata count: " + function);
                Evidence.Add(new OutputObservation(function, "Success", true, count, null));
                if (count > Limit)
                    throw new InvalidOperationException("Load metadata exceeds the 100000-item discovery budget.");
                found = Snapshot(Load.GetAllLoadCasesOfType(family), "caseMetadata").Select(Case).ToArray();
                Check();
                if (found.Any(c => c.CaseType != family))
                    throw new InvalidOperationException("Load metadata returned the wrong case family.");
                cases.Add(family, found);
                return found;
            }
            internal OutputLoadCaseIdentity ResolveCase(int id, LoadCaseType? family)
            {
                MetadataDiagnostic = null;
                var matches = (family.HasValue ? Cases(family.Value) : Cases(LoadCaseType.PrimaryLoad).Concat(Cases(LoadCaseType.LoadCombination)).Concat(Cases(LoadCaseType.ReferenceLoad))).Where(c => c.Id == id).ToArray();
                if (matches.Length != 1)
                {
                    MetadataDiagnostic = matches.Length == 0 ? "Case metadata was not found; no Output query was invoked." : "The case number is ambiguous across metadata families; specify LoadCaseType. No Output query was invoked.";
                    return null;
                }
                return matches[0];
            }
        }
        private T Run<T>(Func<Scope, T> query, bool geometry = false, bool load = false) => queries.WithBinding(p => { var s = new Scope(wrapper, queries, geometry, load); var r = query(s); s.Check(); return r; });
        private void Adapters(IOSGeometry geometry, IOSLoad load)
        {
            if (geometry == null)
                throw new ArgumentNullException(nameof(geometry));
            if (load == null)
                throw new ArgumentNullException(nameof(load));
            queries.WithBinding(p => 0);
            if (!ReferenceEquals(geometry.Wrapper, wrapper) || !ReferenceEquals(geometry, wrapper.Geometry) || !ReferenceEquals(load.Wrapper, wrapper) || !ReferenceEquals(load, wrapper.Load))
                throw new ArgumentException("Use this wrapper's current Geometry and Load adapters.");
        }
        private static T[] Snapshot<T>(IEnumerable<T> values, string parameter)
        {
            if (values == null)
                throw new ArgumentNullException(parameter);
            var a = values.Take(Limit + 1).ToArray();
            if (a.Length > Limit)
                throw new ArgumentException("Selection exceeds 100000 items.", parameter);
            return a;
        }
        private static void Family(LoadCaseType family)
        {
            if (family != LoadCaseType.PrimaryLoad && family != LoadCaseType.ReferenceLoad && family != LoadCaseType.LoadCombination)
                throw new ArgumentOutOfRangeException(nameof(family), "Use primary, combination or reference metadata; repeat metadata is not independently resolved.");
        }
        private static OutputLoadCaseIdentity Case(ILoadCase loadCase)
        {
            if (loadCase == null)
                throw new ArgumentNullException(nameof(loadCase));
            Positive(loadCase.Id, nameof(loadCase));
            Family(loadCase.CaseType);
            if (!Enum.IsDefined(typeof(LoadType), loadCase.Type) || loadCase.Title?.IndexOf('\0') >= 0)
                throw new ArgumentException("Case metadata is invalid.", nameof(loadCase));
            return new OutputLoadCaseIdentity(loadCase.Id, loadCase.Title, loadCase.CaseType, loadCase.Type);
        }
        private static Corner Node(Node node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            Positive(node.Id, nameof(node));
            var point = new Point3D(node.X, node.Y, node.Z);
            MemberResultFrame.Finite(point);
            return new Corner { Id = node.Id, Point = point };
        }
        private static PlateSnapshot Plate(Plate plate, bool allowPartial = true)
        {
            if (plate == null)
                throw new ArgumentNullException(nameof(plate));
            Positive(plate.Id, nameof(plate));
            if (plate.A == null || plate.B == null || plate.C == null)
            {
                if (!allowPartial)
                    throw new ArgumentException("All-corner selection requires populated A/B/C incidence.", nameof(plate));
                return new PlateSnapshot { Id = plate.Id };
            }
            var input = new[] { plate.A, plate.B, plate.C, plate.D }.Where(n => n != null).Select(Node).ToArray();
            if (input.Take(3).Select(n => n.Id).Distinct().Count() != 3)
                throw new ArgumentException("A/B/C incidence IDs must be distinct.", nameof(plate));
            var unique = new List<Corner>();
            foreach (var n in input)
            {
                var old = unique.FirstOrDefault(c => c.Id == n.Id);
                if (old != null && old.Point != n.Point)
                    throw new ArgumentException("One node ID has conflicting coordinates.", nameof(plate));
                if (old == null)
                    unique.Add(n);
            }
            return new PlateSnapshot { Id = plate.Id, Corners = unique.ToArray() };
        }
        private static bool SamePlate(PlateSnapshot a, PlateSnapshot b) => a.Id == b.Id && (a.Corners == null && b.Corners == null || a.Corners != null && b.Corners != null && a.Corners.Length == b.Corners.Length && a.Corners.Zip(b.Corners, (x, y) => x.Id == y.Id && x.Point == y.Point).All(x => x));
        private static bool SameCase(OutputLoadCaseIdentity a, OutputLoadCaseIdentity b) => a.Id == b.Id && a.CaseType == b.CaseType && a.Title == b.Title && a.Type == b.Type;
        private static Request Capture(ILoadCase loadCase, Plate plate, Node cornerNode)
        {
            if (cornerNode == null)
                throw new ArgumentNullException(nameof(cornerNode));
            Positive(cornerNode.Id, nameof(cornerNode));
            var c = Case(loadCase);
            var p = Plate(plate);
            if (p.Corners != null && !p.Corners.Any(n => n.Id == cornerNode.Id))
                throw new ArgumentException("The selected node is not in the supplied plate incidence.", nameof(cornerNode));
            return new Request { Key = new PlateCornerForcesKey(c.Id, p.Id, cornerNode.Id, c.CaseType), Case = c, Plate = p };
        }
        private static Request[] Unique(IEnumerable<Request> source)
        {
            var a = Snapshot(source, "requests");
            var ordered = new List<Request>();
            var keys = new Dictionary<PlateCornerForcesKey, Request>();
            var plates = new Dictionary<int, PlateSnapshot>();
            var cases = new Dictionary<string, OutputLoadCaseIdentity>();
            foreach (var r in a)
            {
                if (plates.TryGetValue(r.Plate.Id, out var oldPlate) && !SamePlate(oldPlate, r.Plate))
                    throw new ArgumentException("A plate ID has conflicting supplied geometry.");
                plates[r.Plate.Id] = r.Plate;
                string caseKey = (int)r.Case.CaseType + ":" + r.Case.Id;
                if (cases.TryGetValue(caseKey, out var oldCase) && !SameCase(oldCase, r.Case))
                    throw new ArgumentException("A case identity has conflicting metadata.");
                cases[caseKey] = r.Case;
                if (!keys.ContainsKey(r.Key))
                {
                    keys.Add(r.Key, r);
                    ordered.Add(r);
                }
            }
            return ordered.ToArray();
        }
        private static Request[] Requests(IEnumerable<PlateCornerForcesRequest> requests) => Unique(Snapshot(requests, nameof(requests)).Select(r => r == null ? throw new ArgumentException("A request is null.", nameof(requests)) : Capture(r.LoadCase, r.Plate, r.CornerNode)));
        private static PlateSnapshot ResolvePlate(Scope scope, int id, out OutputQueryStatus failure, out int code)
        {
            failure = OutputQueryStatus.Success;
            code = 0;
            if (scope.Plates.TryGetValue(id, out var cached))
                return cached;
            scope.Check();
            object a = int.MinValue, b = int.MinValue, c = int.MinValue, d = int.MinValue;
            object raw = scope.Geometry.GetPlateIncidence(id, ref a, ref b, ref c, ref d);
            scope.Check();
            code = NativeValues.ReadInt32(raw, "GetPlateIncidence");
            if (code != 0)
            {
                failure = code == -4001 ? OutputQueryStatus.NotFound : OutputQueryStatus.NativeFailure;
                if (code != -4001 && code != -1)
                    throw new InvalidOperationException("Unknown plate-incidence status.");
            }
            scope.Evidence.Add(new OutputObservation("GetPlateIncidence", failure.ToString(), code == 0, code, null));
            if (code != 0)
                return null;
            int ai = NativeValues.ReadInt32(a, "plate A"), bi = NativeValues.ReadInt32(b, "plate B"), ci = NativeValues.ReadInt32(c, "plate C"), di = NativeValues.ReadInt32(d, "plate D");
            if (ai <= 0 || bi <= 0 || ci <= 0 || di < 0 || new[] { ai, bi, ci }.Distinct().Count() != 3)
                throw new InvalidOperationException("Plate incidence is unwritten or invalid.");
            var nodes = new List<Corner>();
            foreach (int node in new[] { ai, bi, ci, di }.Where(n => n > 0).Distinct())
            {
                scope.Check();
                object x = double.NaN, y = double.NaN, z = double.NaN;
                scope.Geometry.GetNodeCoordinates(node, ref x, ref y, ref z);
                scope.Check();
                nodes.Add(new Corner { Id = node, Point = new Point3D(ReadNumber(x, "GetNodeCoordinates"), ReadNumber(y, "GetNodeCoordinates"), ReadNumber(z, "GetNodeCoordinates")) });
                scope.Evidence.Add(new OutputObservation("GetNodeCoordinates", "WrittenOutputs", true, null, null));
            }
            cached = new PlateSnapshot { Id = id, Corners = nodes.ToArray() };
            scope.Plates.Add(id, cached);
            return cached;
        }
        private static OutputQueryResult<T> Result<T>(PlateCornerForcesKey key, OutputQueryStatus status, T data, string operation, IEnumerable<OutputObservation> evidence, int? code = null, bool? flag = null, string diagnostic = null, string native = NativeName) =>
            new OutputQueryResult<T>(native, status, data, key.PlateId, key.LoadCaseId, nativeReturnCode: code, nativeBooleanReturn: flag, diagnostic: diagnostic, observations: evidence, managedOperation: operation, cornerNodeId: key.CornerNodeId, loadCaseType: key.LoadCaseType);
        private static OutputQueryResult<U> Map<T, U>(OutputQueryResult<T> source, U data, string operation) =>
            new OutputQueryResult<U>(source.NativeFunction, source.Status, data, source.EntityId, source.LoadCaseId, source.MemberEnd, source.NativeReturnCode, source.NativeBooleanReturn, source.Diagnostic, source.ParameterBlockName, source.Observations, operation, source.CornerNodeId, source.LoadCaseType);
        private OutputQueryResult<PlateCornerForces> Read(Scope scope, Request request, Action<Corner> validateCorner = null)
        {
            int start = scope.Evidence.Count;
            var p = request.Plate;
            if (p.Corners == null)
            {
                p = ResolvePlate(scope, p.Id, out var status, out int code);
                if (p == null)
                    return Result<PlateCornerForces>(request.Key, status, null, NativeName, scope.Evidence.Skip(start), code: code, native: "GetPlateIncidence");
            }
            var corner = p.Corners.SingleOrDefault(n => n.Id == request.Key.CornerNodeId);
            if (corner == null)
                return Result<PlateCornerForces>(request.Key, OutputQueryStatus.NotFound, null, NativeName, scope.Evidence.Skip(start), diagnostic: "The selected node is not in resolved plate incidence; no Output call was invoked.");
            validateCorner?.Invoke(corner);
            object values = Buffer(6);
            scope.Check();
            object raw = queries.WithBinding(o => { object returned = o.GetPlateCornerForces(request.Key.PlateId, request.Key.CornerNodeId, request.Key.LoadCaseId, ref values); return returned; });
            bool success = ReadFlag(raw, NativeName);
            int? nativeCode = raw is bool ? (int?)null : NativeValues.ReadInt32(raw, NativeName);
            bool? nativeFlag = raw is bool flag ? (bool?)flag : null;
            scope.Evidence.Add(new OutputObservation(NativeName, success ? "Success" : "NativeFailure", success, nativeCode, nativeFlag));
            scope.Check();
            var payload = success ? new PlateCornerForces(request.Key, request.Case, corner.Point, new ForceComponents(ReadArrayValues(values, 6, NativeName))) : null;
            return Result(request.Key, success ? OutputQueryStatus.Success : OutputQueryStatus.NativeFailure, payload, NativeName, scope.Evidence.Skip(start), nativeCode, nativeFlag);
        }
        private PlateCornerForcesResultSet ReadMany(Request[] requests) => Run(s => new PlateCornerForcesResultSet(requests.Select(r => Read(s, r)).ToArray()), geometry: requests.Any(r => r.Plate.Corners == null));
        internal OutputQueryResult<PlateCornerForces> GetPlateCornerForces(ILoadCase loadCase, Plate plate, Node cornerNode)
        {
            var r = Capture(loadCase, plate, cornerNode);
            return Run(s => Read(s, r), geometry: r.Plate.Corners == null);
        }
        internal OutputQueryResult<PlateCornerForces> GetPlateCornerForces(PlateCornerForcesRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            return GetPlateCornerForces(request.LoadCase, request.Plate, request.CornerNode);
        }
        private OutputQueryResult<PlateCornerForces> Numeric(Scope s, int caseId, int plateId, int nodeId, LoadCaseType? family)
        {
            var c = s.ResolveCase(caseId, family);
            if (c == null)
                return Result<PlateCornerForces>(new PlateCornerForcesKey(caseId, plateId, nodeId, family), OutputQueryStatus.MetadataUnavailable, null, NativeName, s.Evidence, diagnostic: s.MetadataDiagnostic);
            return Read(s, new Request { Key = new PlateCornerForcesKey(caseId, plateId, nodeId, c.CaseType), Case = c, Plate = new PlateSnapshot { Id = plateId } });
        }
        internal OutputQueryResult<PlateCornerForces> GetPlateCornerForces(int loadCaseId, int plateId, int cornerNodeId) => queries.WithBinding(p => GetPlateCornerForces(loadCaseId, plateId, cornerNodeId, wrapper.Geometry, wrapper.Load));
        internal OutputQueryResult<PlateCornerForces> GetPlateCornerForces(int loadCaseId, int plateId, int cornerNodeId, IOSGeometry geometry, IOSLoad load)
        {
            Positive(loadCaseId, nameof(loadCaseId));
            Positive(plateId, nameof(plateId));
            Positive(cornerNodeId, nameof(cornerNodeId));
            Adapters(geometry, load);
            return Run(s => Numeric(s, loadCaseId, plateId, cornerNodeId, null), geometry: true, load: true);
        }
        internal OutputQueryResult<PlateCornerForces> GetPlateCornerForces(int loadCaseId, int plateId, int cornerNodeId, LoadCaseType loadCaseType, IOSGeometry geometry, IOSLoad load)
        {
            Family(loadCaseType);
            Positive(loadCaseId, nameof(loadCaseId));
            Positive(plateId, nameof(plateId));
            Positive(cornerNodeId, nameof(cornerNodeId));
            Adapters(geometry, load);
            return Run(s => Numeric(s, loadCaseId, plateId, cornerNodeId, loadCaseType), geometry: true, load: true);
        }
        internal PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<PlateCornerForcesRequest> requests) => ReadMany(Requests(requests));
        internal PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<ILoadCase> loadCases, Plate plate, Node cornerNode)
        {
            var cases = Snapshot(loadCases, nameof(loadCases));
            return ReadMany(Unique(cases.Select(c => Capture(c, plate, cornerNode))));
        }
        private static Request[] Expand(IEnumerable<ILoadCase> loadCases, IEnumerable<Plate> plates)
        {
            var cases = Snapshot(loadCases, nameof(loadCases)).Select(Case).ToArray();
            var copy = Snapshot(plates, nameof(plates)).Select(p => Plate(p, false)).ToArray();
            long count = (long)cases.Length * copy.Sum(p => (long)p.Corners.Length);
            if (count > Limit)
                throw new ArgumentException("Corner Cartesian selection exceeds 100000 outcomes.");
            return Unique(cases.SelectMany(c => copy.SelectMany(p => p.Corners.Select(n => new Request { Key = new PlateCornerForcesKey(c.Id, p.Id, n.Id, c.CaseType), Case = c, Plate = p }))));
        }
        internal PlateCornerForcesResultSet GetPlateCornerForces(ILoadCase loadCase, Plate plate) => ReadMany(Expand(new[] { loadCase }, new[] { plate }));
        internal PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<ILoadCase> loadCases, Plate plate) => ReadMany(Expand(loadCases, new[] { plate }));
        internal PlateCornerForcesResultSet GetPlateCornerForces(ILoadCase loadCase, IEnumerable<Plate> plates) => ReadMany(Expand(new[] { loadCase }, plates));
        internal PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<ILoadCase> loadCases, IEnumerable<Plate> plates) => ReadMany(Expand(loadCases, plates));
        private PlateCornerForcesResultSet NumericMany(IEnumerable<int> caseIds, int plateId, int nodeId, LoadCaseType? family, IOSGeometry geometry, IOSLoad load)
        {
            var ids = Snapshot(caseIds, nameof(caseIds));
            foreach (int id in ids)
                Positive(id, nameof(caseIds));
            Positive(plateId, nameof(plateId));
            Positive(nodeId, nameof(nodeId));
            Adapters(geometry, load);
            return Run(s => new PlateCornerForcesResultSet(ids.Distinct().Select(id => Numeric(s, id, plateId, nodeId, family)).ToArray()), geometry: true, load: true);
        }
        internal PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<int> loadCaseIds, int plateId, int cornerNodeId, IOSGeometry geometry, IOSLoad load) => NumericMany(loadCaseIds, plateId, cornerNodeId, null, geometry, load);
        internal PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<int> loadCaseIds, int plateId, int cornerNodeId, LoadCaseType loadCaseType, IOSGeometry geometry, IOSLoad load)
        {
            Family(loadCaseType);
            return NumericMany(loadCaseIds, plateId, cornerNodeId, loadCaseType, geometry, load);
        }
        private PlateCornerForcesResultSet Keys(IEnumerable<PlateCornerForcesKey> keys, LoadCaseType? family, IOSGeometry geometry, IOSLoad load)
        {
            var copy = Snapshot(keys, nameof(keys));
            foreach (var key in copy)
            {
                new PlateCornerForcesKey(key.LoadCaseId, key.PlateId, key.CornerNodeId, key.LoadCaseType);
                if (family.HasValue && key.LoadCaseType.HasValue && key.LoadCaseType != family.Value)
                    throw new ArgumentException("Every key must match the explicit case family.", nameof(keys));
            }
            Adapters(geometry, load);
            return Run(s =>
            {
                var results = new List<OutputQueryResult<PlateCornerForces>>();
                var seen = new HashSet<PlateCornerForcesKey>();
                foreach (var key in copy)
                {
                    var selected = family ?? key.LoadCaseType;
                    var c = s.ResolveCase(key.LoadCaseId, selected);
                    var resolved = new PlateCornerForcesKey(key.LoadCaseId, key.PlateId, key.CornerNodeId, c?.CaseType ?? selected);
                    if (!seen.Add(resolved))
                        continue;
                    results.Add(c == null ? Result<PlateCornerForces>(resolved, OutputQueryStatus.MetadataUnavailable, null, NativeName, s.Evidence, diagnostic: s.MetadataDiagnostic) :
                        Read(s, new Request { Key = resolved, Case = c, Plate = new PlateSnapshot { Id = key.PlateId } }));
                }
                return new PlateCornerForcesResultSet(results);
            }, geometry: true, load: true);
        }
        internal PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<PlateCornerForcesKey> keys, IOSGeometry geometry, IOSLoad load) => Keys(keys, null, geometry, load);
        internal PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<PlateCornerForcesKey> keys, LoadCaseType loadCaseType, IOSGeometry geometry, IOSLoad load)
        {
            Family(loadCaseType);
            return Keys(keys, loadCaseType, geometry, load);
        }
        internal OutputQueryResult<PlateCornerForces> GetPlateCornerForcesInGlobalCoordinates(ILoadCase loadCase, Plate plate, Node cornerNode) => GetPlateCornerForces(loadCase, plate, cornerNode);
        internal OutputQueryResult<PlateCornerForces> GetPlateCornerForcesGlobal(Plate plate, Node cornerNode, int loadCaseId)
        {
            Positive(loadCaseId, nameof(loadCaseId));
            return GetPlateCornerForces(new LoadCase(loadCaseId), plate, cornerNode);
        }
        private sealed class Target
        {
            internal int? MemberId; internal int StartId, EndId; internal Point3D Start, End; internal MemberResultFrame Frame;
        }
        private static Target Member(Beam member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));
            Positive(member.Id, nameof(member));
            var a = Node(member.StartNode);
            var b = Node(member.EndNode);
            if (a.Id == b.Id)
                throw new ArgumentException("Member incidence IDs must differ.", nameof(member));
            return new Target { MemberId = member.Id, StartId = a.Id, EndId = b.Id, Start = a.Point, End = b.Point, Frame = MemberResultFrame.Create(a.Point, b.Point, member.BetaAngle) };
        }
        private static MemberEnd? ValidateTarget(Target target, int cornerId, Point3D point)
        {
            MemberEnd? end = null;
            if (target.MemberId.HasValue)
            {
                Point3D expected;
                if (cornerId == target.StartId)
                {
                    end = MemberEnd.Start;
                    expected = target.Start;
                }
                else if (cornerId == target.EndId)
                {
                    end = MemberEnd.End;
                    expected = target.End;
                }
                else
                    throw new ArgumentException("The corner node is not an end of the supplied member.");
                if ((point - expected).Length > 1e-9)
                    throw new ArgumentException("Corner and member-end coordinates differ; no moment transfer is implicit.");
            }
            return end;
        }
        private static PlateCornerMemberForces Transform(PlateCornerForces source, Target target)
        {
            MemberEnd? end = ValidateTarget(target, source.CornerNodeId, source.CornerPosition);
            var frame = target.Frame;
            var f = frame.ToLocal(source.Components.ForceVector);
            var m = frame.ToLocal(source.Components.MomentVector);
            return new PlateCornerMemberForces(source, target.MemberId, end, new ForceComponents(new[] { f.X, f.Y, f.Z, m.X, m.Y, m.Z }), frame.X, frame.Y, frame.Z, frame.Beta);
        }
        private static Target Direction(PlateCornerForces source, Corner other)
        {
            if (source.CornerNodeId == other.Id)
                throw new ArgumentException("A distinct direction node is required.");
            return new Target { Start = source.CornerPosition, End = other.Point, Frame = MemberResultFrame.Create(source.CornerPosition, other.Point, 0) };
        }
        private OutputQueryResult<PlateCornerMemberForces> FetchTransform(Request request, Target member, Corner other)
        {
            if (member != null && request.Key.CornerNodeId != member.StartId && request.Key.CornerNodeId != member.EndId)
                throw new ArgumentException("The corner node is not a supplied member end.");
            return Run(s =>
            {
                Target target = member;
                var r = Read(s, request, n =>
                {
                    if (target != null)
                        ValidateTarget(target, n.Id, n.Point);
                    else
                        target = new Target { Start = n.Point, End = other.Point, Frame = MemberResultFrame.Create(n.Point, other.Point, 0) };
                });
                return Map(r, r.IsSuccessful ? Transform(r.Data, target) : null, "GetPlateCornerForcesInMemberLocalCoordinates");
            }, geometry: request.Plate.Corners == null);
        }
        internal OutputQueryResult<PlateCornerMemberForces> GetPlateCornerForces(ILoadCase loadCase, Plate plate, Node cornerNode, Node otherMemberNode)
        {
            var request = Capture(loadCase, plate, cornerNode);
            var other = Node(otherMemberNode);
            if (request.Key.CornerNodeId == other.Id)
                throw new ArgumentException("Direction node must differ from the corner.");
            return FetchTransform(request, null, other);
        }
        internal OutputQueryResult<PlateCornerMemberForces> GetPlateCornerForces(ILoadCase loadCase, Plate plate, Node cornerNode, Beam member) => FetchTransform(Capture(loadCase, plate, cornerNode), Member(member), null);
        internal OutputQueryResult<PlateCornerMemberForces> TransformPlateCornerForcesToMemberLocal(PlateCornerForces plateCornerForces, Node otherMemberNode)
        {
            if (plateCornerForces == null)
                throw new ArgumentNullException(nameof(plateCornerForces));
            var target = Direction(plateCornerForces, Node(otherMemberNode));
            return Run(s => Result(plateCornerForces.Key, OutputQueryStatus.Success, Transform(plateCornerForces, target), "TransformPlateCornerForcesToMemberLocal", s.Evidence));
        }
        internal OutputQueryResult<PlateCornerMemberForces> TransformPlateCornerForcesToMemberLocal(PlateCornerForces plateCornerForces, Beam member)
        {
            if (plateCornerForces == null)
                throw new ArgumentNullException(nameof(plateCornerForces));
            var target = Member(member);
            return Run(s => Result(plateCornerForces.Key, OutputQueryStatus.Success, Transform(plateCornerForces, target), "TransformPlateCornerForcesToMemberLocal", s.Evidence));
        }
        internal OutputQueryResult<PlateCornerMemberForces> GetPlateCornerForcesInMemberLocalCoordinates(ILoadCase loadCase, Plate plate, Node cornerNode, Node otherMemberNode) => GetPlateCornerForces(loadCase, plate, cornerNode, otherMemberNode);
        internal OutputQueryResult<PlateCornerMemberForces> GetPlateCornerForcesInMemberLocalCoordinates(ILoadCase loadCase, Plate plate, Node cornerNode, Beam member) => GetPlateCornerForces(loadCase, plate, cornerNode, member);
        internal OutputBatchResult<PlateCornerMemberForces> GetPlateCornerForcesInMemberLocalCoordinates(IEnumerable<PlateCornerForcesRequest> requests, Beam member)
        {
            var copy = Requests(requests);
            var target = Member(member);
            foreach (var r in copy)
            {
                if (r.Key.CornerNodeId != target.StartId && r.Key.CornerNodeId != target.EndId)
                    throw new ArgumentException("Every selected corner must be a supplied member end.");
                if (r.Plate.Corners != null)
                    ValidateTarget(target, r.Key.CornerNodeId, r.Plate.Corners.Single(n => n.Id == r.Key.CornerNodeId).Point);
            }
            return Run(s => new OutputBatchResult<PlateCornerMemberForces>(copy.Select(r => { var raw = Read(s, r, n => ValidateTarget(target, n.Id, n.Point)); return Map(raw, raw.IsSuccessful ? Transform(raw.Data, target) : null, "GetPlateCornerForcesInMemberLocalCoordinates"); }).ToArray()), geometry: copy.Any(r => r.Plate.Corners == null));
        }
        internal OutputBatchResult<PlateCornerMemberForces> TransformPlateCornerForcesToMemberLocal(IEnumerable<PlateCornerForces> plateCornerForces, Beam member)
        {
            var copy = Snapshot(plateCornerForces, nameof(plateCornerForces));
            if (copy.Any(r => r == null))
                throw new ArgumentException("A source packet is null.", nameof(plateCornerForces));
            var target = Member(member);
            // Validate every pure transformation before returning any payload.
            var transformed = copy.Select(r => Transform(r, target)).ToArray();
            return Run(s => new OutputBatchResult<PlateCornerMemberForces>(transformed.Select(r => Result(r.Key, OutputQueryStatus.Success, r, "TransformPlateCornerForcesToMemberLocal", s.Evidence)).ToArray()));
        }
    }
}
