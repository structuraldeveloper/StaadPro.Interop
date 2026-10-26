using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Output;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Output;
using StaadPro.Interop.Services.Interop;
using static StaadPro.Interop.Services.Output.OutputQueryReader;
namespace StaadPro.Interop.Services.Output
{
    // Managed composition only; no additional native name or uncertain native contract is enabled.
    internal sealed class OutputWorkflowService
    {
        private const int MaximumItems = 100000;
        private readonly OpenStaadWrapper wrapper;
        private readonly OutputQueryReader queries;
        private readonly OutputAdvancedReader advanced;
        internal OutputWorkflowService(OpenStaadWrapper wrapper, OutputQueryReader queries, OutputAdvancedReader advanced)
        {
            this.wrapper = wrapper;
            this.queries = queries;
            this.advanced = advanced;
        }
        private sealed class Scope
        {
            private readonly OpenStaadWrapper wrapper;
            private readonly OutputQueryReader queries;
            private readonly object geometry, property, load;
            internal readonly IOSProperty Property;
            internal readonly IOSLoad Load;
            internal readonly List<OutputObservation> Evidence = new List<OutputObservation>();
            internal Scope(OpenStaadWrapper wrapper, OutputQueryReader queries, bool needsProperty, bool needsGeometry, IOSLoad load)
            {
                this.wrapper = wrapper;
                this.queries = queries;
                if (needsProperty)
                {
                    Property = wrapper.Property ?? throw new InvalidOperationException("Property binding is required for offsets.");
                    property = (object)wrapper.RawProperty;
                }
                if (needsGeometry)
                    geometry = (object)wrapper.RawGeometry ?? throw new InvalidOperationException("Geometry binding is required to resolve member coordinates.");
                if (load != null)
                {
                    if (!ReferenceEquals(load.Wrapper, wrapper) || !ReferenceEquals(load, wrapper.Load))
                        throw new ArgumentException("Use the acquired wrapper's current Load adapter.", nameof(load));
                    Load = load;
                    this.load = (object)wrapper.RawLoad;
                }
                Check();
            }
            internal dynamic Geometry => geometry;
            internal void Check()
            {
                queries.WithBinding(p => 0);
                if (property != null && !ReferenceEquals(property, (object)wrapper.RawProperty) || geometry != null && !ReferenceEquals(geometry, (object)wrapper.RawGeometry) || load != null && !ReferenceEquals(load, (object)wrapper.RawLoad))
                    throw new InvalidOperationException("A participating workflow binding was replaced. Reacquire the adapters.");
            }
            internal OutputQueryResult<T> Read<T>(Func<OutputQueryResult<T>> query)
            {
                Check();
                var r = query();
                Evidence.Add(new OutputObservation(r.NativeFunction, r.Status.ToString(), r.IsSuccessful, r.NativeReturnCode, r.NativeBooleanReturn));
                Check();
                return r;
            }
        }
        private T Run<T>(Func<Scope, T> work, bool property = false, bool geometry = false, IOSLoad load = null) => queries.WithBinding(p =>
        {
            var scope = new Scope(wrapper, queries, property, geometry, load);
            T result = work(scope);
            scope.Check();
            return result;
        });
        private static OutputQueryResult<T> Wrap<T, U>(OutputQueryResult<U> decisive, T data, string operation, IEnumerable<OutputObservation> evidence,
            OutputQueryStatus? status = null, string diagnostic = null) => new OutputQueryResult<T>(decisive.NativeFunction, status ?? decisive.Status, data, decisive.EntityId, decisive.LoadCaseId, decisive.MemberEnd,
                decisive.NativeReturnCode, decisive.NativeBooleanReturn, diagnostic ?? decisive.Diagnostic, decisive.ParameterBlockName, evidence, operation);
        private static OutputQueryResult<T> Failure<T>(string native, OutputQueryStatus status, string operation, Scope scope, int? member = null, int? loadCase = null, string diagnostic = null, int? code = null) =>
            new OutputQueryResult<T>(native, status, default, member, loadCase, nativeReturnCode: code, diagnostic: diagnostic, observations: scope.Evidence, managedOperation: operation);
        private static T[] Snapshot<T>(IEnumerable<T> items, string name)
        {
            if (items == null)
                throw new ArgumentNullException(name);
            var copy = items.Take(MaximumItems + 1).ToArray();
            if (copy.Length > MaximumItems)
                throw new ArgumentException("Selection exceeds 100000 items.", name);
            return copy;
        }
        private static int Member(Beam beam, string name = "beam")
        {
            if (beam == null)
                throw new ArgumentNullException(name);
            Positive(beam.Id, name);
            return beam.Id;
        }
        private static Beam[] Members(IEnumerable<Beam> beams)
        {
            var copy = Snapshot(beams, nameof(beams));
            var ids = new HashSet<int>();
            foreach (var beam in copy)
            {
                int id = Member(beam, nameof(beams));
                if (!ids.Add(id))
                    throw new ArgumentException("Supply one beam object per member ID.", nameof(beams));
            }
            return copy;
        }
        private static void SameId(Beam beam, int id)
        {
            if (beam.Id != id)
                throw new InvalidOperationException("A caller beam identity changed during result extraction.");
        }
        private static void Title(string title)
        {
            if (title == null)
                throw new ArgumentNullException("loadCaseTitle");
            if (title.IndexOf('\0') >= 0)
                throw new ArgumentException("Title cannot contain NUL.", "loadCaseTitle");
        }
        private static OutputLoadCaseIdentity Case(ILoadCase loadCase)
        {
            if (loadCase == null)
                throw new ArgumentNullException(nameof(loadCase));
            Positive(loadCase.Id, nameof(loadCase));
            if (!Enum.IsDefined(typeof(LoadCaseType), loadCase.CaseType) || !Enum.IsDefined(typeof(LoadType), loadCase.Type) || loadCase.Title?.IndexOf('\0') >= 0)
                throw new ArgumentException("Case metadata is invalid.", nameof(loadCase));
            return new OutputLoadCaseIdentity(loadCase.Id, loadCase.Title, loadCase.CaseType, loadCase.Type);
        }
        private static OutputLoadCaseIdentity[] Cases(IEnumerable<ILoadCase> loadCases)
        {
            var supplied = Snapshot(loadCases, nameof(loadCases));
            var result = new Dictionary<int, OutputLoadCaseIdentity>();
            foreach (var item in supplied)
            {
                var c = Case(item);
                if (result.TryGetValue(c.Id, out var old))
                {
                    if (old.CaseType != c.CaseType || old.Title != c.Title || old.Type != c.Type)
                        throw new ArgumentException("A case number has ambiguous metadata/family.", nameof(loadCases));
                }
                else
                    result.Add(c.Id, c);
            }
            return result.Values.ToArray();
        }
        private static void Degree(int degreeOfParallelism)
        {
            if (degreeOfParallelism < 1 || degreeOfParallelism > 64)
                throw new ArgumentOutOfRangeException(nameof(degreeOfParallelism));
        }
        private OutputLoadCaseIdentity[] DiscoverCases(Scope scope)
        {
            scope.Check();
            // Reuse the established Load API; the returned sequences are materialized exactly once.
            var primary = Snapshot(scope.Load.GetAllLoadCasesOfType(LoadCaseType.PrimaryLoad), "primaryCases");
            scope.Check();
            var combinations = Snapshot(scope.Load.GetAllLoadCasesOfType(LoadCaseType.LoadCombination), "combinationCases");
            scope.Check();
            return Cases(primary.Concat(combinations));
        }
        private OutputQueryResult<BeamSteelDesignResults> Design(Scope scope, Beam beam, int id, OutputLoadCaseIdentity[] cases)
        {
            int start = scope.Evidence.Count;
            var raw = scope.Read(() => advanced.GetMemberSteelDesignResults(id));
            SameId(beam, id);
            if (!raw.IsSuccessful)
                return Wrap<BeamSteelDesignResults, MemberSteelDesignResults>(raw, null, "GetSteelDesignResultsForBeam", scope.Evidence.Skip(start));
            var critical = cases.SingleOrDefault(x => x.Id == raw.Data.CriticalLoadCaseId);
            return critical == null ? Wrap<BeamSteelDesignResults, MemberSteelDesignResults>(raw, null, "GetSteelDesignResultsForBeam", scope.Evidence.Skip(start), OutputQueryStatus.MetadataUnavailable, "Governing native case " + raw.Data.CriticalLoadCaseId + " is absent from the supplied/discovered case metadata.")
                : Wrap(raw, new BeamSteelDesignResults(beam, id, raw.Data, critical), "GetSteelDesignResultsForBeam", scope.Evidence.Skip(start));
        }
        internal OutputQueryResult<BeamSteelDesignResults> GetSteelDesignResultsForBeam(Beam beam, IEnumerable<ILoadCase> loadCases)
        {
            int id = Member(beam);
            var cases = Cases(loadCases);
            return Run(s => Design(s, beam, id, cases));
        }
        internal OutputQueryResult<BeamSteelDesignResults> GetSteelDesignResultsForBeam(IOSLoad load, Beam beam)
        {
            if (load == null)
                throw new ArgumentNullException(nameof(load));
            int id = Member(beam);
            return Run(s => Design(s, beam, id, DiscoverCases(s)), load: load);
        }
        internal OutputBatchResult<BeamSteelDesignResults> GetSteelDesignResultsForBeams(IEnumerable<Beam> beams, IEnumerable<ILoadCase> loadCases)
        {
            var copy = Members(beams);
            var ids = copy.Select(x => x.Id).ToArray();
            var cases = Cases(loadCases);
            return Run(s => new OutputBatchResult<BeamSteelDesignResults>(copy.Select((b, i) => Design(s, b, ids[i], cases)).ToArray()));
        }
        internal OutputBatchResult<BeamSteelDesignResults> GetSteelDesignResultsForBeams(IOSLoad load, IEnumerable<Beam> beams)
        {
            if (load == null)
                throw new ArgumentNullException(nameof(load));
            var copy = Members(beams);
            var ids = copy.Select(x => x.Id).ToArray();
            return Run(s => { if (copy.Length == 0) return new OutputBatchResult<BeamSteelDesignResults>(new OutputQueryResult<BeamSteelDesignResults>[0]); var cases = DiscoverCases(s); return new OutputBatchResult<BeamSteelDesignResults>(copy.Select((b, i) => Design(s, b, ids[i], cases)).ToArray()); }, load: load);
        }
        private OutputBatchResult<double> Ratios(Scope scope, Beam[] beams, int[] ids)
        {
            var results = new List<OutputQueryResult<double>>();
            for (int i = 0; i < beams.Length; i++)
            {
                var r = scope.Read(() => queries.GetMemberSteelUtilizationRatio(ids[i]));
                SameId(beams[i], ids[i]);
                results.Add(r);
            }
            return new OutputBatchResult<double>(results);
        }
        internal OutputBatchResult<double> GetMemberSteelUtilizationRatios(HashSet<Beam> beams)
        {
            var copy = Members(beams);
            var ids = copy.Select(x => x.Id).ToArray();
            return Run(s => Ratios(s, copy, ids));
        }
        internal OutputQueryResult<double> GetMemberSteelUtilizationRatio(Beam beam)
        {
            int id = Member(beam);
            return Run(s => { var r = s.Read(() => queries.GetMemberSteelUtilizationRatio(id)); SameId(beam, id); return r; });
        }
        internal OutputPopulationResult PopulateMemberSteelUtilizationRatios(HashSet<Beam> beams)
        {
            var copy = Members(beams);
            var ids = copy.Select(x => x.Id).ToArray();
            return Run(s =>
            {
                var staged = Ratios(s, copy, ids);
                for (int i = 0; i < copy.Length; i++)
                    SameId(copy[i], ids[i]);
                if (!staged.IsSuccessful)
                    return new OutputPopulationResult(staged, false);
                for (int i = 0; i < copy.Length; i++)
                {
                    s.Check();
                    SameId(copy[i], ids[i]);
                    copy[i].UtilizationRatio = staged.Items[i].Data;
                    s.Check();
                }
                return new OutputPopulationResult(staged, copy.Length > 0);
            });
        }
        internal OutputQueryResult<ForceComponents> GetMemberEndForces(int memberId, MemberEnd memberEnd, int loadCaseId, ResultCoordinateSystem coordinateSystem)
        {
            Positive(memberId, nameof(memberId));
            Positive(loadCaseId, nameof(loadCaseId));
            End(memberEnd);
            Axes(coordinateSystem);
            return queries.WithBinding(p =>
            {
                const string op = "GetMemberEndForces";
                object values = Buffer(6);
                object raw = p.GetMemberEndForces(memberId, (int)memberEnd, loadCaseId, ref values, (int)coordinateSystem);
                bool success = ReadFlag(raw, op);
                return new OutputQueryResult<ForceComponents>(op, success ? OutputQueryStatus.Success : OutputQueryStatus.NativeFailure,
                    success ? new ForceComponents(ReadArrayValues(values, 6, op)) : null, memberId, loadCaseId, memberEnd,
                    nativeReturnCode: raw is bool ? (int?)null : NativeValues.ReadInt32(raw, op), nativeBooleanReturn: raw is bool b ? (bool?)b : null);
            });
        }
        private static void End(MemberEnd end)
        {
            if (end != MemberEnd.Start && end != MemberEnd.End)
                throw new ArgumentOutOfRangeException(nameof(end));
        }
        private static void Axes(ResultCoordinateSystem axes)
        {
            if (axes != ResultCoordinateSystem.Local && axes != ResultCoordinateSystem.Global)
                throw new ArgumentOutOfRangeException(nameof(axes));
        }
        private OutputQueryResult<IdentifiedMemberForces> NamedForces(Scope scope, string title, int member, int loadCase, MemberEnd end, ResultCoordinateSystem axes)
        {
            int start = scope.Evidence.Count;
            var raw = scope.Read(() => GetMemberEndForces(member, end, loadCase, axes));
            return Wrap(raw, raw.IsSuccessful ? new IdentifiedMemberForces(member, title, loadCase, end, axes, raw.Data) : null, "GetMemberEndForces", scope.Evidence.Skip(start));
        }
        internal OutputQueryResult<IdentifiedMemberForces> GetMemberEndForces(string loadCaseTitle, int memberId, int loadCaseId, MemberEnd memberEnd) =>
            GetMemberEndForces(loadCaseTitle, memberId, loadCaseId, memberEnd, ResultCoordinateSystem.Local);
        internal OutputQueryResult<IdentifiedMemberForces> GetMemberEndForces(string loadCaseTitle, int memberId, int loadCaseId, MemberEnd memberEnd, ResultCoordinateSystem coordinateSystem)
        {
            Title(loadCaseTitle);
            Positive(memberId, nameof(memberId));
            Positive(loadCaseId, nameof(loadCaseId));
            End(memberEnd);
            Axes(coordinateSystem);
            return Run(s => NamedForces(s, loadCaseTitle, memberId, loadCaseId, memberEnd, coordinateSystem));
        }
        private static Point3D Point(Node node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            Positive(node.Id, nameof(node));
            var p = new Point3D(node.X, node.Y, node.Z);
            Finite(p.X);
            Finite(p.Y);
            Finite(p.Z);
            return p;
        }
        private static void Finite(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Geometry or derived result must be finite.");
        }
        private static void Finite(Vector3D v)
        {
            Finite(v.X);
            Finite(v.Y);
            Finite(v.Z);
        }
        private static Point3D[] MemberPoints(Beam beam)
        {
            Member(beam);
            var a = Point(beam.StartNode);
            var b = Point(beam.EndNode);
            Vector3D delta = b - a;
            Finite(delta);
            if (beam.StartNode.Id == beam.EndNode.Id || double.IsInfinity(delta.Length) || delta.Length <= 1e-9)
                throw new ArgumentException("Member incidence must contain distinct noncoincident finite nodes.", nameof(beam));
            return new[] { a, b };
        }
        private static void SameGeometry(Beam beam, int id, Point3D[] points, int startId, int endId)
        {
            SameId(beam, id);
            if (beam.StartNode?.Id != startId || beam.EndNode?.Id != endId || Point(beam.StartNode) != points[0] || Point(beam.EndNode) != points[1])
                throw new InvalidOperationException("Caller member geometry changed during offset extraction.");
        }
        private static Beam ResolveMember(Scope scope, int id)
        {
            object a = int.MinValue, b = int.MinValue;
            object raw = scope.Geometry.GetMemberIncidence(id, ref a, ref b);
            scope.Check();
            int code = NativeValues.ReadInt32(raw, "GetMemberIncidence");
            scope.Evidence.Add(new OutputObservation("GetMemberIncidence", code == 0 ? "Success" : code == -3001 ? "NotFound" : "NativeFailure", code == 0, code, null));
            if (code != 0)
            {
                if (code != -1 && code != -3001)
                    throw new InvalidOperationException("Unknown member-incidence code.");
                return null;
            }
            int ai = NativeValues.ReadInt32(a, "member start ID"), bi = NativeValues.ReadInt32(b, "member end ID");
            if (ai <= 0 || bi <= 0)
                throw new InvalidOperationException("Unwritten member incidence.");
            return new Beam(id, ResolveNode(scope, ai), ResolveNode(scope, bi));
        }
        private static Node ResolveNode(Scope scope, int id)
        {
            object x = double.NaN, y = double.NaN, z = double.NaN;
            scope.Geometry.GetNodeCoordinates(id, ref x, ref y, ref z);
            scope.Check();
            var node = new Node(id, ReadNumber(x, "GetNodeCoordinates"), ReadNumber(y, "GetNodeCoordinates"), ReadNumber(z, "GetNodeCoordinates"));
            scope.Evidence.Add(new OutputObservation("GetNodeCoordinates", "WrittenOutputs", true, null, null));
            return node;
        }
        private OutputQueryResult<OffsetAwareMemberEndForces> Offsets(Scope scope, string title, Beam beam, int id, int loadCase, MemberEnd end)
        {
            var points = MemberPoints(beam);
            int startId = beam.StartNode.Id, endId = beam.EndNode.Id;
            scope.Check();
            var offset = scope.Property.GetMemberGlobalOffset(id, end);
            scope.Check();
            scope.Evidence.Add(new OutputObservation(offset.NativeFunction, offset.Status.ToString(), offset.IsSuccessful, offset.NativeReturnCode, offset.NativeBooleanReturn));
            if (!offset.IsSuccessful && offset.Status != PropertyQueryStatus.NoSpecification)
                return Failure<OffsetAwareMemberEndForces>(offset.NativeFunction, offset.Status == PropertyQueryStatus.NotFound ? OutputQueryStatus.NotFound : OutputQueryStatus.NativeFailure, "GetMemberEndForcesConsideringOffsets", scope, id, loadCase, offset.Status.ToString(), offset.NativeReturnCode);
            var vector = offset.IsSuccessful ? offset.Data : new Vector3D();
            Finite(vector);
            var forces = NamedForces(scope, title, id, loadCase, end, ResultCoordinateSystem.Global);
            SameGeometry(beam, id, points, startId, endId);
            if (!forces.IsSuccessful)
                return Wrap<OffsetAwareMemberEndForces, IdentifiedMemberForces>(forces, null, "GetMemberEndForcesConsideringOffsets", scope.Evidence);
            Point3D joint = end == MemberEnd.Start ? points[0] : points[1];
            Point3D face = joint + vector;
            Finite(face.X);
            Finite(face.Y);
            Finite(face.Z);
            Vector3D shift = Vector3D.CrossProduct(vector, forces.Data.Components.ForceVector);
            Vector3D moment = forces.Data.Components.MomentVector + shift;
            Finite(moment);
            var c = forces.Data.Components;
            var transferred = new ForceComponents(new[] { c.Fx, c.Fy, c.Fz, moment.X, moment.Y, moment.Z });
            var identified = new IdentifiedMemberForces(id, title, loadCase, end, ResultCoordinateSystem.Global, transferred);
            return Wrap(forces, new OffsetAwareMemberEndForces(forces.Data, identified, joint, face, vector, offset.Status), "GetMemberEndForcesConsideringOffsets", scope.Evidence);
        }
        internal OutputQueryResult<OffsetAwareMemberEndForces> GetMemberEndForcesConsideringOffsets(string loadCaseTitle, Beam beam, int loadCaseId, MemberEnd memberEnd)
        {
            Title(loadCaseTitle);
            int id = Member(beam);
            MemberPoints(beam);
            Positive(loadCaseId, nameof(loadCaseId));
            End(memberEnd);
            return Run(s => Offsets(s, loadCaseTitle, beam, id, loadCaseId, memberEnd), property: true);
        }
        internal OutputQueryResult<OffsetAwareMemberEndForces> GetMemberEndForcesConsideringOffsets(string loadCaseTitle, int memberId, int loadCaseId, MemberEnd memberEnd)
        {
            Title(loadCaseTitle);
            Positive(memberId, nameof(memberId));
            Positive(loadCaseId, nameof(loadCaseId));
            End(memberEnd);
            return Run(s => { var beam = ResolveMember(s, memberId); return beam == null ? Failure<OffsetAwareMemberEndForces>("GetMemberIncidence", s.Evidence.Last().NativeReturnCode == -3001 ? OutputQueryStatus.NotFound : OutputQueryStatus.NativeFailure, "GetMemberEndForcesConsideringOffsets", s, memberId, loadCaseId, code: s.Evidence.Last().NativeReturnCode) : Offsets(s, loadCaseTitle, beam, memberId, loadCaseId, memberEnd); }, property: true, geometry: true);
        }
        internal OutputQueryResult<IdentifiedMemberForces> GetMemberEndForcesAtPhysicalJoint(string loadCaseTitle, Beam beam, int loadCaseId, MemberEnd memberEnd)
        {
            var r = GetMemberEndForcesConsideringOffsets(loadCaseTitle, beam, loadCaseId, memberEnd);
            return Wrap(r, r.IsSuccessful ? r.Data.PhysicalJointForces : null, "GetMemberEndForcesAtPhysicalJoint", r.Observations);
        }
        internal OutputQueryResult<IdentifiedMemberForces> GetMemberEndForcesAtPhysicalJoint(string loadCaseTitle, int memberId, int loadCaseId, MemberEnd memberEnd)
        {
            var r = GetMemberEndForcesConsideringOffsets(loadCaseTitle, memberId, loadCaseId, memberEnd);
            return Wrap(r, r.IsSuccessful ? r.Data.PhysicalJointForces : null, "GetMemberEndForcesAtPhysicalJoint", r.Observations);
        }
        internal MemberCoordinateSystem GetMemberLocalCoordinateSystem(Beam member)
        {
            int id = Member(member, nameof(member));
            var points = MemberPoints(member);
            double beta = member.BetaAngle;
            Finite(beta);
            return Run(s =>
            {
                Vector3D x = points[1] - points[0];
                x.Normalize();
                Vector3D y, z;
                if (Math.Abs(x.Y) >= 1 - 1e-9)
                {
                    var gz = new Vector3D(0, 0, 1);
                    z = gz - Vector3D.DotProduct(gz, x) * x;
                    z.Normalize();
                    y = Vector3D.CrossProduct(z, x);
                    y.Normalize();
                }
                else
                {
                    var gy = new Vector3D(0, 1, 0);
                    y = gy - Vector3D.DotProduct(gy, x) * x;
                    y.Normalize();
                    z = Vector3D.CrossProduct(x, y);
                    z.Normalize();
                }
                var rotation = Matrix3D.Identity;
                rotation.Rotate(new Quaternion(x, beta % 360));
                y = rotation.Transform(y);
                z = rotation.Transform(z);
                Finite(y);
                Finite(z);
                return new MemberCoordinateSystem(id, points[0], x, y, z, beta);
            });
        }
        private static int PlateId(Plate plate)
        {
            if (plate == null)
                throw new ArgumentNullException(nameof(plate));
            Positive(plate.Id, nameof(plate));
            return plate.Id;
        }
        internal OutputQueryResult<PlateCenterForces> GetPlateCenterForces(ILoadCase loadCase, Plate plate)
        {
            var c = Case(loadCase);
            int p = PlateId(plate);
            return Run(s => s.Read(() => queries.GetPlateCenterForces(p, c.Id)));
        }
        internal OutputQueryResult<PlateCenterMoments> GetPlateCenterMoments(ILoadCase loadCase, Plate plate)
        {
            var c = Case(loadCase);
            int p = PlateId(plate);
            return Run(s => s.Read(() => queries.GetPlateCenterMoments(p, c.Id)));
        }
        internal OutputQueryResult<PlateCenterVonMises> GetPlateCenterVonMises(ILoadCase loadCase, Plate plate)
        {
            var c = Case(loadCase);
            int p = PlateId(plate);
            return Run(s => s.Read(() => queries.GetPlateCenterVonMisesStresses(p, c.Id)));
        }
        internal OutputQueryResult<PlatePrincipalStresses> GetPlatePrincipalStresses(ILoadCase loadCase, Plate plate)
        {
            var c = Case(loadCase);
            int p = PlateId(plate);
            return Run(s => s.Read(() => queries.GetPlateCenterNormalPrincipalStresses(p, c.Id)));
        }
        private OutputQueryResult<PlateCenterResults> PlateResults(Scope scope, OutputLoadCaseIdentity c, int p)
        {
            int start = scope.Evidence.Count;
            var f = scope.Read(() => queries.GetPlateCenterForces(p, c.Id));
            var m = scope.Read(() => queries.GetPlateCenterMoments(p, c.Id));
            var v = scope.Read(() => queries.GetPlateCenterVonMisesStresses(p, c.Id));
            var n = scope.Read(() => queries.GetPlateCenterNormalPrincipalStresses(p, c.Id));
            if (!f.IsSuccessful)
                return Wrap<PlateCenterResults, PlateCenterForces>(f, null, "GetPlateCenterResults", scope.Evidence.Skip(start));
            if (!m.IsSuccessful)
                return Wrap<PlateCenterResults, PlateCenterMoments>(m, null, "GetPlateCenterResults", scope.Evidence.Skip(start));
            if (!v.IsSuccessful)
                return Wrap<PlateCenterResults, PlateCenterVonMises>(v, null, "GetPlateCenterResults", scope.Evidence.Skip(start));
            if (!n.IsSuccessful)
                return Wrap<PlateCenterResults, PlatePrincipalStresses>(n, null, "GetPlateCenterResults", scope.Evidence.Skip(start));
            return Wrap(n, new PlateCenterResults(p, c, f.Data, m.Data, v.Data, n.Data), "GetPlateCenterResults", scope.Evidence.Skip(start));
        }
        internal OutputQueryResult<PlateCenterResults> GetPlateCenterResults(ILoadCase loadCase, Plate plate)
        {
            var c = Case(loadCase);
            int p = PlateId(plate);
            return Run(s => PlateResults(s, c, p));
        }
        internal OutputBatchResult<PlateCenterResults> GetPlateCenterResults(IEnumerable<ILoadCase> loadCases, IEnumerable<Plate> plates) =>
            GetPlateCenterResults(loadCases, plates, 1);
        internal OutputBatchResult<PlateCenterResults> GetPlateCenterResults(IEnumerable<ILoadCase> loadCases, IEnumerable<Plate> plates, int degreeOfParallelism)
        {
            Degree(degreeOfParallelism);
            var supplied = Snapshot(loadCases, nameof(loadCases));
            var cases = supplied.Select(Case).ToArray();
            Cases(supplied);
            var p = Snapshot(plates, nameof(plates)).Select(PlateId).ToArray();
            long count = (long)cases.Length * p.Length;
            if (count > MaximumItems)
                throw new ArgumentException("Cartesian result count exceeds 100000.");
            return Run(s => { var result = new List<OutputQueryResult<PlateCenterResults>>((int)count); foreach (var c in cases) foreach (int id in p) result.Add(PlateResults(s, c, id)); return new OutputBatchResult<PlateCenterResults>(result); });
        }
        internal OutputBatchResult<StaticCheckResult> GetStaticCheckResults(IEnumerable<int> loadCaseIds) => GetStaticCheckResults(loadCaseIds, 1);
        internal OutputBatchResult<StaticCheckResult> GetStaticCheckResults(IEnumerable<int> loadCaseIds, int degreeOfParallelism)
        {
            Degree(degreeOfParallelism);
            var ids = Snapshot(loadCaseIds, nameof(loadCaseIds));
            foreach (int id in ids)
                Positive(id, nameof(loadCaseIds));
            return Run(s => new OutputBatchResult<StaticCheckResult>(ids.Select(id => s.Read(() => queries.GetStaticCheckResult(id))).ToArray()));
        }
        internal OutputQueryResult<ForceComponents> GetSupportForces(int nodeId, int loadCaseId)
        {
            Positive(nodeId, nameof(nodeId));
            Positive(loadCaseId, nameof(loadCaseId));
            return Run(s =>
            {
                var r = s.Read(() => queries.GetSupportReactions(nodeId, loadCaseId));
                var applied = r.IsSuccessful ? r.Data.ToForces().ScaledBy(-1) : null;
                return Wrap(r, r.IsSuccessful ? new ForceComponents(new[] { applied.Fx, applied.Fy, applied.Fz, applied.Mx, applied.My, applied.Mz }) : null, "GetSupportForces", s.Evidence);
            });
        }
    }
}
