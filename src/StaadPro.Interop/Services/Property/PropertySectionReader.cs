using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Common;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Interfaces;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Sections;
using static StaadPro.Interop.Services.Property.PropertyValueReader;

namespace StaadPro.Interop.Services.Property
{
    internal sealed class PropertySectionReader
    {
        private readonly PropertyQueryReader queries;
        private readonly PropertyPortReader ports;
        private readonly OpenStaadWrapper wrapper;
        internal PropertySectionReader(OpenStaadWrapper wrapper, PropertyQueryReader queries, PropertyPortReader ports)
        { this.queries = queries; this.ports = ports; this.wrapper = wrapper; }
        private T Run<T>(Func<dynamic, T> body) => queries.WithBinding(p => body(p));
        private object Call(Func<dynamic, object> body) => queries.WithBinding(p => body(p));
        private void Write(Action<dynamic> body) => Run(p => { body(p); return true; });
        private static void Degree(int nThreads) { if (nThreads < 1 || nThreads > 512) throw new ArgumentOutOfRangeException(nameof(nThreads)); }
        private static T[] Entities<T>(IEnumerable<T> values, string name) where T : class, IEntity
        {
            if (values == null) throw new ArgumentNullException(name); var copy = values.ToArray();
            if (copy.Any(v => v == null || v.Id <= 0)) throw new ArgumentException("Candidates must be non-null with positive analytical IDs.", name); return copy;
        }
        private static T[] Packets<T>(HashSet<T> values, string name, bool required) where T : class
        {
            if (values == null || required && values.Count == 0) { if (required) throw new InvalidOperationException(name + " must contain definitions."); return new T[0]; }
            var copy = values.ToArray(); if (copy.Any(p => p == null)) throw new ArgumentException("Definitions must not contain null entries.", name); return copy;
        }
        private static int Capacity(object raw)
        { int count = Count(raw, "GetCountofSectionPropertyValuesEx"); if (count == 0 || count > 100000) throw new InvalidOperationException("Section buffer capacity must be 1 through 100000."); return count; }
        private static void Zero(object raw, string op)
        { int code = Integer(raw, op); if (code != 0) throw new InvalidOperationException(op + " failed with native code " + code + "."); }
        private static int Created(object raw, string op)
        { int id = Integer(raw, op); if (id <= 0) throw new InvalidOperationException(op + " returned invalid creation reference " + id + "."); return id; }
        internal int GetSectionPropertyCount() => Run(p => Count((object)p.GetSectionPropertyCount(), "GetSectionPropertyCount"));
        internal int GetThicknessPropertyCount() => Run(p => Count((object)p.GetThicknessPropertyCount(), "GetThicknessPropertyCount"));
        internal int GetCountofSectionPropertyValuesEx() => Run(p => Count((object)p.GetCountofSectionPropertyValuesEx(), "GetCountofSectionPropertyValuesEx"));
        internal int GetSectionPropertyCountry(int varSecRefNo)
        {
            Positive(varSecRefNo, nameof(varSecRefNo));
            return Run(p => { int code = Integer((object)p.GetSectionPropertyCountry(varSecRefNo), "GetSectionPropertyCountry"); if (code < 0) throw new InvalidOperationException("GetSectionPropertyCountry failed with native code " + code + "."); return code; });
        }
        internal int GetSectionPropertyType(int varSecRefNo)
        {
            Positive(varSecRefNo, nameof(varSecRefNo));
            return Run(p => { int code = Integer((object)p.GetSectionPropertyType(varSecRefNo), "GetSectionPropertyType"); if (code <= 0) throw new InvalidOperationException("GetSectionPropertyType failed with native code " + code + "."); return code; });
        }
        internal int GetRecordForSection(int Country, string SectionName)
        {
            Positive(Country, nameof(Country));
            if (string.IsNullOrWhiteSpace(SectionName) || SectionName.IndexOf('\0') >= 0) throw new ArgumentException("Provide an exact nonblank native table name without NUL characters.", nameof(SectionName));
            return Run(p => { int record = Integer((object)p.GetRecordForSection(Country, SectionName), "GetRecordForSection"); if (record < -1) throw new InvalidOperationException("GetRecordForSection returned an undocumented negative code."); return record; });
        }
        internal IGenericSection GetSectionBy(int propId)
        {
            Positive(propId, nameof(propId));
            return Run(p =>
            {
                // Adapters are created before RefreshComObjects populates the wrapper's metadata cache.
                int nativeMajor = wrapper.StaadVersion?.Major ?? 0;
                int typeBefore = GetSectionPropertyType(propId); string[] fields = SectionLayouts.Fields(nativeMajor, typeBefore);
                int capacity = Capacity(Call(q => (object)q.GetCountofSectionPropertyValuesEx()));
                if (fields.Length > capacity) throw new InvalidOperationException("Native capacity is smaller than the applicable section layout.");
                object values = Buffer(capacity), type = int.MinValue;
                object raw = Call(q => (object)q.GetSectionPropertyValuesEx(propId, ref type, ref values));
                if (!Flag(raw, "GetSectionPropertyValuesEx")) throw new InvalidOperationException("GetSectionPropertyValuesEx reported failure.");
                if (Integer(type, "GetSectionPropertyValuesEx") != typeBefore) throw new InvalidOperationException("Section type changed during value retrieval.");
                var copy = FloatingArray(values, capacity, "GetSectionPropertyValuesEx", false);
                for (int i = 0; i < copy.Length; i++)
                {
                    if (i < fields.Length) Number(copy[i], "GetSectionPropertyValuesEx " + fields[i]);
                    else if (double.IsNaN(copy[i])) copy[i] = 0;
                    else Number(copy[i], "GetSectionPropertyValuesEx padding");
                }
                string name = ports.GetSectionPropertyName(propId); int country = GetSectionPropertyCountry(propId);
                int record = country > 0 && typeBefore < 670 && !string.IsNullOrWhiteSpace(name) ? GetRecordForSection(country, name) : -1;
                if (GetSectionPropertyType(propId) != typeBefore || GetSectionPropertyCountry(propId) != country || Capacity(Call(q => (object)q.GetCountofSectionPropertyValuesEx())) != capacity || wrapper.StaadVersion?.Major != nativeMajor)
                    throw new InvalidOperationException("Section metadata/capacity changed during retrieval.");
                double? area = null;
                if (fields[0] == "Ax") area = copy[0];
                else if (typeBefore == 680)
                {
                    // Source tapered-I mean of start/end plate areas, without a catalog lookup.
                    double flanges = copy[3] * copy[4] + copy[5] * copy[6];
                    area = flanges + ((copy[0] + copy[2]) / 2 - copy[4] - copy[6]) * copy[1];
                }
                if (area.HasValue && (double.IsNaN(area.Value) || double.IsInfinity(area.Value) || area.Value < 0)) throw new InvalidOperationException("Section area is negative or nonfinite.");
                return (IGenericSection)new GenericSection(propId, typeBefore, country, record, name, nativeMajor, copy, fields, area);
            });
        }
        internal double? GetSectionalArea(int propId)
        { var section = (GenericSection)GetSectionBy(propId); return section.HasSectionalArea ? section.Ax : (double?)null; }
        internal HashSet<IGenericSection> GetAllSections() => Run(p =>
        {
            int[] ids = ports.GetSectionPropertyList(); var result = new HashSet<IGenericSection>(ids.Select(GetSectionBy));
            if (!ids.SequenceEqual(ports.GetSectionPropertyList())) throw new InvalidOperationException("Section inventory changed during retrieval."); return result;
        });
        internal IGenericSection GetSectionByPropertyIdWithAssignedBeamsList(int propId, IEnumerable<Beam> allBeams)
        {
            var candidates = new HashSet<Beam>(Entities(allBeams, nameof(allBeams))); Positive(propId, nameof(propId));
            return Run(p => { var section = GetSectionBy(propId); section.AssignedBeams.UnionWith(ports.GetSectionPropertyAssignedBeams(candidates, propId)); return section; });
        }
        internal IGenericSection GetSectionByPropertyIdWithAssignedBeamsList(int propId, IOSGeometry geometry)
        {
            if (geometry == null) throw new ArgumentNullException(nameof(geometry)); Positive(propId, nameof(propId));
            return Run(p => GetSectionByPropertyIdWithAssignedBeamsList(propId, geometry.GetAllEntities<Beam>(1)));
        }
        internal HashSet<IGenericSection> GetAllSectionsWithAssignedBeamsList(IEnumerable<Beam> allBeams)
        {
            var candidates = new HashSet<Beam>(Entities(allBeams, nameof(allBeams)));
            return Run(p => { var sections = GetAllSections(); foreach (var section in sections) section.AssignedBeams.UnionWith(ports.GetSectionPropertyAssignedBeams(candidates, section.Id)); return sections; });
        }
        internal HashSet<IGenericSection> GetAllSectionsWithAssignedBeamsList(IOSGeometry geometry)
        {
            if (geometry == null) throw new ArgumentNullException(nameof(geometry));
            return Run(p => GetAllSectionsWithAssignedBeamsList(geometry.GetAllEntities<Beam>(1)));
        }
        private static Tuple<int, int[]> Assignment(int id, IEnumerable<Beam> candidates)
        { var ids = Entities(candidates, "Beams").Select(b => b.Id).ToArray(); if (ids.Length == 0) return null; Positive(id, "Id"); return Tuple.Create(id, ids); }
        private void Assign(Tuple<int, int[]> input, bool scalar)
        {
            if (input == null) return;
            if (scalar) foreach (int id in input.Item2) Zero(Call(q => (object)q.AssignBeamProperty(id, input.Item1)), "AssignBeamProperty");
            else Zero(Call(q => (object)q.AssignBeamProperty(input.Item2, input.Item1)), "AssignBeamProperty");
        }
        internal void AssignSectionProperties(HashSet<IGenericSection> gsSet)
        { var inputs = Packets(gsSet, nameof(gsSet), false).Select(s => Assignment(s.Id, s.AssignedBeams)).ToArray(); Write(p => { foreach (var input in inputs) Assign(input, false); }); }
        internal void AssignSectionProperties(HashSet<SectionProperty> collection, int nThreads, bool scalar)
        { Degree(nThreads); var inputs = Packets(collection, nameof(collection), false).Select(s => Assignment(s.Id, s.Beams)).ToArray(); Write(p => { foreach (var input in inputs) Assign(input, scalar); }); }
        internal void AssignSectionProperty(SectionProperty sectionProperty, int nThreads, bool scalar)
        { Degree(nThreads); var input = sectionProperty == null ? null : Assignment(sectionProperty.Id, sectionProperty.Beams); Write(p => Assign(input, scalar)); }
        private int Create(SectionCreationInput input)
        { int id = Created(Call(q => input.Invoke(q)), "CreateSectionProperty"); input.Packet.Id = id; return id; }
        internal void CreateSectionProperty(SectionProperty sp)
        { var input = SectionCreationInput.From(sp); Write(p => Create(input)); }
        internal void CreateSectionProperties(HashSet<SectionProperty> spSet)
        { var inputs = Packets(spSet, nameof(spSet), true).Select(SectionCreationInput.From).ToArray(); Write(p => { foreach (var input in inputs) Create(input); }); }
        internal void CreateAndAssignSectionProperties(HashSet<Beam> beams, int nThreads, bool scalar)
        {
            Degree(nThreads); var candidates = Entities(beams, nameof(beams));
            var uses = candidates.Where(b => b.SectionProperty != null).Select(b => new { Beam = b, Packet = b.SectionProperty }).ToArray();
            foreach (var use in uses) SectionCreationInput.From(use.Packet);
            var groups = uses.GroupBy(u => u.Packet).Select(g => new {
                Input = SectionCreationInput.From(g.Key), Canonical = g.Key,
                Packets = g.Select(u => u.Packet).GroupBy(s => s, ReferenceComparer.Instance).Select(x => x.Key).ToArray(),
                Targets = Entities(g.Key.Beams, "Beams").Concat(g.Select(u => u.Beam)).ToArray()
            }).ToArray();
            int[][] targets = groups.Select(g => g.Targets.Select(b => b.Id).Distinct().ToArray()).ToArray();
            Write(p =>
            {
                var created = new List<Tuple<int, int[]>>();
                for (int i = 0; i < groups.Length; i++)
                { int id = Create(groups[i].Input); foreach (var packet in groups[i].Packets) packet.Id = id; groups[i].Canonical.Beams.UnionWith(groups[i].Targets); created.Add(Tuple.Create(id, targets[i])); }
                foreach (var input in created) Assign(input, scalar);
            });
        }
        internal void CreatePlateThicknessProperty(object property, PlateThickness pt)
        {
            if (property == null) throw new ArgumentNullException(nameof(property)); if (pt == null) throw new InvalidOperationException("pt must not be null.");
            double value = pt.Thickness; if (double.IsNaN(value) || double.IsInfinity(value) || value <= 0) throw new ArgumentOutOfRangeException(nameof(pt));
            Write(p =>
            {
                if (!ReferenceEquals(property, (object)p)) throw new ArgumentException("Use this adapter's captured Property receiver.", nameof(property));
                pt.Id = Created(Call(q => (object)q.CreatePlateThicknessProperty(value)), "CreatePlateThicknessProperty");
            });
        }
        private sealed class ReferenceComparer : IEqualityComparer<SectionProperty>
        {
            internal static readonly ReferenceComparer Instance = new ReferenceComparer();
            public bool Equals(SectionProperty a, SectionProperty b) => ReferenceEquals(a, b);
            public int GetHashCode(SectionProperty value) => System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(value);
        }
    }
}
