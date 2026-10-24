using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Common;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Models.Property;
using static StaadPro.Interop.Services.Property.PropertyValueReader;

namespace StaadPro.Interop.Services.Property
{
    // Source-compatible compound workflows share the existing adapter's lock and captured receiver.
    internal sealed class PropertyWorkflowReader
    {
        private readonly PropertyQueryReader queries;
        private readonly PropertyManagementReader management;
        private readonly PropertyPortReader ports;
        internal PropertyWorkflowReader(PropertyQueryReader queries, PropertyManagementReader management, PropertyPortReader ports) { this.queries = queries; this.management = management; this.ports = ports; }
        private T Run<T>(Func<dynamic, T> body) => queries.WithBinding(p => body(p));
        private void Write(Action<dynamic> body) => Run(p => { body(p); return true; });
        private object Call(Func<dynamic, object> operation) => queries.WithBinding(p => operation(p));
        private static void Degree(int nThreads) { if (nThreads < 1 || nThreads > 512) throw new ArgumentOutOfRangeException(nameof(nThreads)); }
        private static void Text(string name)
        { if (name == null) throw new ArgumentNullException(nameof(name)); if (string.IsNullOrWhiteSpace(name) || name.IndexOf('\0') >= 0) throw new ArgumentException("Provide an exact nonblank native material name without NUL characters.", nameof(name)); }
        private static void Finite(double value, string name)
        { if (double.IsNaN(value) || double.IsInfinity(value)) throw new ArgumentOutOfRangeException(name, "Provide a finite value."); }
        private static T[] Entities<T>(IEnumerable<T> source, string name, bool optional = false) where T : class, IEntity
        {
            if (source == null) { if (optional) return new T[0]; throw new ArgumentNullException(name); }
            var copy = source.ToArray();
            if (copy.Any(e => e == null || e.Id <= 0)) throw new ArgumentException("Candidates must be non-null with positive analytical IDs.", name);
            return copy;
        }
        private static T[] Packets<T>(HashSet<T> source, string name, bool required) where T : class
        {
            if (source == null || required && source.Count == 0)
            { if (required) throw new InvalidOperationException(name + " must contain at least one definition."); return new T[0]; }
            var copy = source.ToArray(); if (copy.Any(p => p == null)) throw new ArgumentException("Definitions cannot contain null entries.", name); return copy;
        }
        private static void Zero(object raw, string op)
        { int code = Integer(raw, op); if (code != 0) throw new InvalidOperationException(op + " failed with native code " + code + "."); }
        private static void Success(object raw, string op)
        { if (!Flag(raw, op)) throw new InvalidOperationException(op + " reported failure."); }
        private static int Created(object raw, string op)
        { int id = Integer(raw, op); if (id <= 0) throw new InvalidOperationException(op + " returned invalid creation reference " + id + "."); return id; }
        private static int Bound(object raw, string op)
        { int count = Count(raw, op); if (count > 100000) throw new InvalidOperationException(op + " exceeds the 100000-definition allocation limit."); return count; }
        private static MaterialProperty Material(PropertyQueryResult<MaterialProperties> result, int index = 0)
        {
            if (!result.IsSuccessful) throw new InvalidOperationException(result.NativeFunction + " could not provide a material: " + result.Status + ".");
            var value = result.Data; Text(value.Name); var c = value.Constants; var s = value.Strengths;
            return new MaterialProperty(value.Name) { Id = index, E = c.ElasticModulus, Poisson = c.PoissonRatio, Density = c.WeightDensity,
                Alpha = c.ThermalExpansionCoefficient, Damping = c.DampingRatio, Fy = s?.YieldStress ?? 0, Fu = s?.UltimateTensileStrength ?? 0,
                Ry = s?.YieldStrengthRatio ?? 0, Rt = s?.TensileStrengthRatio ?? 0, Fcu = s?.CompressiveStrength ?? 0 };
        }
        private HashSet<MaterialProperty> AllMaterials(bool extended) => Run(p =>
        {
            int count = Bound((object)p.GetIsotropicMaterialCount(), "GetIsotropicMaterialCount");
            var result = new HashSet<MaterialProperty>();
            for (int i = 0; i < count; i++) result.Add(Material(extended ? queries.GetExtendedIsotropicMaterialProperty(i) : queries.GetIsotropicMaterialProperty(i), i));
            if (Bound((object)p.GetIsotropicMaterialCount(), "GetIsotropicMaterialCount") != count) throw new InvalidOperationException("Material count changed during retrieval.");
            return result;
        });
        internal HashSet<MaterialProperty> GetAllIsotropicMaterialProperties() => AllMaterials(false);
        internal HashSet<MaterialProperty> GetAllExtendedIsotropicMaterialProperties() => AllMaterials(true);
        private HashSet<MaterialProperty> Assignments(HashSet<MaterialProperty> materials, Beam[] beams, Plate[] plates)
        {
            // Stage every native read before mutating caller/resolver-owned assignment sets.
            var staged = materials.Select(m => new {
                Packet = m,
                Beams = beams.Length == 0 ? null : management.GetIsotropicMaterialAssignedBeams(m.Name, beams),
                Plates = plates.Length == 0 ? null : management.GetIsotropicMaterialAssignedPlates(m.Name, plates)
            }).ToArray();
            foreach (var item in staged)
            {
                if (item.Beams != null) { if (item.Packet.Beams == null) item.Packet.Beams = new HashSet<Beam>(); item.Packet.Beams.Clear(); item.Packet.Beams.UnionWith(item.Beams); }
                if (item.Plates != null) { if (item.Packet.Plates == null) item.Packet.Plates = new HashSet<Plate>(); item.Packet.Plates.Clear(); item.Packet.Plates.UnionWith(item.Plates); }
            }
            return materials;
        }
        private HashSet<MaterialProperty> IndexedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates, bool extended)
        {
            var beams = Entities(forBeams, nameof(forBeams), true); var plates = Entities(forPlates, nameof(forPlates), true);
            return Run(p => Assignments(AllMaterials(extended), beams, plates));
        }
        internal HashSet<MaterialProperty> GetAllExtendedIsotropicMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates) => IndexedAssignments(forBeams, forPlates, true);
        internal HashSet<MaterialProperty> GetAllIsotropicMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates) => IndexedAssignments(forBeams, forPlates, false);
        internal HashSet<MaterialProperty> GetAllMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates, Func<string, MaterialProperty> materialResolver)
        {
            if (materialResolver == null) throw new ArgumentNullException(nameof(materialResolver));
            var beams = Entities(forBeams, nameof(forBeams), true); var plates = Entities(forPlates, nameof(forPlates), true);
            return Run(p =>
            {
                var names = new HashSet<string>(StringComparer.Ordinal);
                foreach (var beam in beams)
                {
                    var result = queries.GetBeamMaterialName(beam.Id);
                    if (!result.IsSuccessful) throw new InvalidOperationException("Cannot resolve the beam's material name.");
                    Text(result.Data); names.Add(result.Data);
                }
                foreach (var plate in plates) { string name = ports.GetPlateMaterialName(plate.Id); Text(name); names.Add(name); }
                var materials = new HashSet<MaterialProperty>();
                foreach (string name in names)
                {
                    var packet = materialResolver(name);
                    if (packet == null) throw new InvalidOperationException("The material resolver returned null.");
                    Text(packet.Name); materials.Add(packet);
                }
                return Assignments(materials, beams, plates);
            });
        }
        internal HashSet<MaterialProperty> GetAllMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates)
            => GetAllMaterialPropertiesWithPopulatedAssignments(forBeams, forPlates, name => Material(queries.GetMaterialProperty(name)));
        internal HashSet<MaterialProperty> GetAllExtendedMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates)
            => GetAllMaterialPropertiesWithPopulatedAssignments(forBeams, forPlates, name => Material(queries.GetExtendedMaterialProperty(name)));
        private sealed class MaterialInput
        {
            internal MaterialProperty Packet;
            internal string Name;
            internal double E, Poisson, G, Density, Alpha, Damping;
        }
        private static MaterialInput MaterialRequest(MaterialProperty mp, bool legacy)
        {
            if (mp == null) throw new InvalidOperationException("mp must not be null.");
            var input = new MaterialInput { Packet = mp, Name = mp.Name, E = mp.E * (legacy ? 1000 : 1), G = mp.G * (legacy ? 1000 : 1),
                Poisson = mp.Poisson, Density = mp.Density, Alpha = mp.Alpha, Damping = mp.Damping };
            Text(input.Name);
            foreach (double value in new[] { input.E, input.G, input.Poisson, input.Density, input.Alpha, input.Damping }) Finite(value, nameof(mp));
            if (input.E <= 0 || input.G <= 0 || input.Poisson <= -1 || input.Poisson >= .5 || input.Density < 0 || input.Damping < 0)
                throw new ArgumentOutOfRangeException(nameof(mp), "Use positive E/G, -1 < Poisson < 0.5 and nonnegative weight density/damping.");
            return input;
        }
        private void CreateMaterial(dynamic p, MaterialInput input)
        {
            int code = Integer(Call(q => (object)q.CreateIsotropicMaterialProperties(input.Name, input.E, input.Poisson, input.G, input.Density, input.Alpha, input.Damping)), "CreateIsotropicMaterialProperties");
            if (code != 0 && code != 1) throw new InvalidOperationException("CreateIsotropicMaterialProperties failed with native code " + code + ".");
            input.Packet.LastCreationStatus = (MaterialCreationStatus)code; // It is not a material ID/index.
        }
        internal void CreateMaterialProperty(MaterialProperty mp, bool legacy)
        { var input = MaterialRequest(mp, legacy); Write(p => CreateMaterial(p, input)); }
        internal void CreateMaterialProperties(HashSet<MaterialProperty> mpSet, bool legacy)
        {
            var inputs = Packets(mpSet, nameof(mpSet), true).Select(mp => MaterialRequest(mp, legacy)).ToArray();
            Write(p => { foreach (var input in inputs) CreateMaterial(p, input); });
        }
        private sealed class MaterialAssignment
        { internal string Name; internal int[] Beams, Plates; }
        private static MaterialAssignment MaterialAssignmentRequest(MaterialProperty mp)
        {
            if (mp == null) return null;
            var beams = Entities(mp.Beams, "mp.Beams", true).Select(b => b.Id).ToArray();
            var plates = Entities(mp.Plates, "mp.Plates", true).Select(b => b.Id).ToArray();
            if (beams.Length == 0 && plates.Length == 0) return null;
            Text(mp.Name); return new MaterialAssignment { Name = mp.Name, Beams = beams, Plates = plates };
        }
        private void AssignMaterial(dynamic p, MaterialAssignment input)
        {
            if (input == null) return;
            foreach (int id in input.Beams) Success(Call(q => (object)q.AssignMaterialToMember(input.Name, id)), "AssignMaterialToMember");
            foreach (int id in input.Plates) Zero(Call(q => (object)q.AssignMaterialToPlate(input.Name, id)), "AssignMaterialToPlate");
        }
        internal void AssignMaterialProperty(MaterialProperty mp, int nThreads)
        { Degree(nThreads); var input = MaterialAssignmentRequest(mp); Write(p => AssignMaterial(p, input)); }
        internal void AssignMaterialProperties(HashSet<MaterialProperty> collection, int nThreads)
        {
            Degree(nThreads); var inputs = Packets(collection, nameof(collection), false).Select(MaterialAssignmentRequest).ToArray();
            Write(p => { foreach (var input in inputs) AssignMaterial(p, input); });
        }
        internal void CreatePlateThicknessProperties(HashSet<PlateThickness> ptSet)
        {
            var inputs = Packets(ptSet, nameof(ptSet), true).Select(pt => new { Packet = pt, Value = pt.Thickness }).ToArray();
            foreach (var input in inputs) { Finite(input.Value, nameof(ptSet)); if (input.Value <= 0) throw new ArgumentOutOfRangeException(nameof(ptSet)); }
            Write(p => { foreach (var input in inputs) input.Packet.Id = Created(Call(q => (object)q.CreatePlateThicknessProperty(input.Value)), "CreatePlateThicknessProperty"); });
        }
        private static Tuple<int, int[]> ThicknessRequest(PlateThickness packet)
        {
            if (packet == null) return null;
            var ids = Entities(packet.Plates, "Plates", true).Select(e => e.Id).ToArray();
            if (ids.Length == 0) return null;
            Positive(packet.Id, "Id"); return Tuple.Create(packet.Id, ids);
        }
        private void AssignThickness(dynamic p, Tuple<int, int[]> input, bool scalar)
        {
            if (input == null) return;
            if (scalar) foreach (int id in input.Item2) Zero(Call(q => (object)q.AssignPlateThickness(id, input.Item1)), "AssignPlateThickness");
            else Zero(Call(q => (object)q.AssignPlateThickness(input.Item2, input.Item1)), "AssignPlateThickness");
        }
        internal void AssignThicknessProperties(HashSet<PlateThickness> collection, int nThreads, bool scalar)
        {
            Degree(nThreads); var inputs = Packets(collection, nameof(collection), false).Select(ThicknessRequest).ToArray();
            Write(p => { foreach (var input in inputs) AssignThickness(p, input, scalar); });
        }
        internal void AssignThicknessProperty(PlateThickness thicknessProperty, int nThreads)
        { Degree(nThreads); var input = ThicknessRequest(thicknessProperty); Write(p => AssignThickness(p, input, true)); }
        private static Tuple<int, int[]> OffsetAssignment(OffsetSpecification os)
        {
            if (os == null) return null;
            var ids = Entities(os.Beams, "Beams", true).Select(e => e.Id).ToArray(); if (ids.Length == 0) return null;
            Positive(os.Id, "Id"); return Tuple.Create(os.Id, ids);
        }
        private void AssignOffset(dynamic p, Tuple<int, int[]> input)
        { if (input != null) foreach (int id in input.Item2) Zero(Call(q => (object)q.AssignMemberSpecToBeam(id, input.Item1)), "AssignMemberSpecToBeam"); }
        internal void AssignOffsetSpecification(OffsetSpecification os, int nThreads)
        { Degree(nThreads); var input = OffsetAssignment(os); Write(p => AssignOffset(p, input)); }
        internal void AssignOffsetSpecifications(HashSet<OffsetSpecification> offsetSpecificationCollection, int nThreads)
        {
            Degree(nThreads); var inputs = Packets(offsetSpecificationCollection, nameof(offsetSpecificationCollection), false).Select(OffsetAssignment).ToArray();
            Write(p => { foreach (var input in inputs) AssignOffset(p, input); });
        }
        internal void CreateAndAssignOffsetSpecifications(HashSet<Beam> beams, int nThreads, bool scalar)
        {
            Degree(nThreads); var candidates = Entities(beams, nameof(beams));
            var uses = candidates.SelectMany(b => new[] { new { Beam = b, Spec = b.StartOffset }, new { Beam = b, Spec = b.EndOffset } }).Where(item => item.Spec != null).ToArray();
            foreach (var item in uses)
            {
                var s = item.Spec;
                if (!Enum.IsDefined(typeof(OffsetAxis), s.Axis) || !Enum.IsDefined(typeof(OffsetLocation), s.Location)) throw new ArgumentOutOfRangeException(nameof(beams));
                Finite(s.X, nameof(beams)); Finite(s.Y, nameof(beams)); Finite(s.Z, nameof(beams));
            }
            var groups = uses.GroupBy(item => item.Spec).Select(g => new { Packet = g.Key, Copies = g.Select(item => item.Spec).Distinct(ReferenceEqualityComparer<OffsetSpecification>.Instance).ToArray(),
                Targets = Entities(g.Key.Beams, "Beams", true).Concat(g.Select(item => item.Beam)).ToArray(),
                Location = g.Key.Location, Axis = g.Key.Axis, X = g.Key.X, Y = g.Key.Y, Z = g.Key.Z, Zero = g.Key.IsZero }).Where(g => !g.Zero).ToArray();
            var targets = groups.Select(g => g.Targets.Select(b => b.Id).Distinct().ToArray()).ToArray();
            Write(p =>
            {
                // Finish creation before assignment, matching the source pipeline's ordering.
                var created = new List<Tuple<int, int[]>>();
                int groupIndex = 0;
                foreach (var group in groups)
                {
                    int id = Created(Call(q => (object)q.CreateMemberOffsetSpec((int)group.Location, (int)group.Axis, group.X, group.Y, group.Z)), "CreateMemberOffsetSpec");
                    foreach (var packet in group.Copies) packet.Id = id;
                    group.Packet.Beams.UnionWith(group.Targets);
                    created.Add(Tuple.Create(id, targets[groupIndex++]));
                }
                foreach (var item in created)
                {
                    if (scalar) foreach (int id in item.Item2) Zero(Call(q => (object)q.AssignMemberSpecToBeam(id, item.Item1)), "AssignMemberSpecToBeam");
                    else Zero(Call(q => (object)q.AssignMemberSpecToBeam(item.Item2, item.Item1)), "AssignMemberSpecToBeam");
                }
            });
        }
        private sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T : class
        {
            internal static readonly ReferenceEqualityComparer<T> Instance = new ReferenceEqualityComparer<T>();
            public bool Equals(T a, T b) => ReferenceEquals(a, b);
            public int GetHashCode(T obj) => System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
        }
        internal void PopulateMemberReleases(IEnumerable<Beam> forBeams)
        {
            var beams = Entities(forBeams, nameof(forBeams)).Distinct().ToArray();
            Write(p =>
            {
                var packets = beams.Select(b => new[] { Release(queries.GetMemberReleaseSpecification(b.Id, MemberEnd.Start)), Release(queries.GetMemberReleaseSpecification(b.Id, MemberEnd.End)) }).ToArray();
                for (int i = 0; i < beams.Length; i++) { beams[i].StartRelease = packets[i][0]; beams[i].EndRelease = packets[i][1]; }
            });
        }
        private static MemberReleases Release(PropertyQueryResult<MemberReleaseSpecification> result)
        { if (!result.IsSuccessful) throw new InvalidOperationException("Cannot populate member releases: " + result.Status + "."); return MemberReleases.FromNative(result.Data); }
        private sealed class ReleaseInput
        { internal int Member, End; internal MemberReleases Packet; internal int[] Flags; internal double[] Springs; }
        private static ReleaseInput ReleaseRequest(Beam beam, MemberReleases packet, int end)
        {
            if (packet == null) return null;
            if (packet.ReleaseType == ReleaseType.MpAll || packet.ReleaseType == ReleaseType.MpOnly)
                throw new NotSupportedException("Basic member-release creation cannot represent partial-moment modes; use an explicitly supported partial-release workflow.");
            int[] flags = packet.Releases.ToIntArray(); double[] springs = packet.Springs.ToArray();
            if (springs.Any(v => double.IsNaN(v) || double.IsInfinity(v) || v < 0)) throw new ArgumentException("Spring creation inputs must be finite and nonnegative.", nameof(beam));
            if (packet.ReleaseType == ReleaseType.NoRelease) return null;
            return new ReleaseInput { Member = beam.Id, End = end, Packet = packet, Flags = flags, Springs = springs };
        }
        internal void CreateAndAssignMemberEndReleases(HashSet<Beam> beams, int nThreads)
        {
            Degree(nThreads); var inputs = Entities(beams, nameof(beams)).SelectMany(b => new[] { ReleaseRequest(b, b.StartRelease, 0), ReleaseRequest(b, b.EndRelease, 1) }).Where(p => p != null).ToArray();
            // One mutable packet cannot safely carry IDs for different native end definitions.
            if (inputs.GroupBy(i => i.Packet, ReferenceEqualityComparer<MemberReleases>.Instance).Any(g => g.Select(i => i.End).Distinct().Count() > 1))
                throw new ArgumentException("Use independent release packets for the two member ends.", nameof(beams));
            Write(p =>
            {
                foreach (var input in inputs)
                {
                    int id = Created(Call(q => (object)q.CreateMemberReleaseSpec(input.End, input.Flags, input.Springs)), "CreateMemberReleaseSpec");
                    input.Packet.Id = id;
                    Zero(Call(q => (object)q.AssignMemberSpecToBeam(input.Member, id)), "AssignMemberSpecToBeam");
                }
            });
        }
    }
}
