using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Common;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Property;
using static StaadPro.Interop.Services.Property.PropertyValueReader;

namespace StaadPro.Interop.Services.Property
{
    // Shares the adapter's query lock and binding checks; never dispatches COM on worker threads.
    internal sealed class PropertyManagementReader
    {
        private readonly PropertyQueryReader queries;
        private readonly PropertyPortReader ports;
        internal PropertyManagementReader(PropertyQueryReader queries, PropertyPortReader ports) { this.queries = queries; this.ports = ports; }
        private T Run<T>(Func<dynamic, T> body) => queries.WithBinding(p => body(p));
        private void Write(Action<dynamic> body) => Run(p => { body(p); return true; });
        private static T[] Candidates<T>(IEnumerable<T> items, string name) where T : class, IEntity
        {
            if (items == null) throw new ArgumentNullException(name);
            var copy = items.ToArray();
            if (copy.Any(item => item == null || item.Id <= 0)) throw new ArgumentException("Candidates must be non-null with positive analytical IDs.", name);
            return copy;
        }
        private static T[] Definitions<T>(HashSet<T> items, string name, bool requireItems) where T : class
        {
            if (items == null || requireItems && items.Count == 0)
            { if (requireItems) throw new InvalidOperationException(name + " must contain at least one definition."); return new T[0]; }
            var copy = items.ToArray();
            if (copy.Any(item => item == null)) throw new ArgumentException("Definitions cannot contain null entries.", name);
            return copy;
        }
        private static void Finite(double value, string name)
        { if (double.IsNaN(value) || double.IsInfinity(value)) throw new ArgumentOutOfRangeException(name, "Provide a finite value."); }
        private static void Zero(object value, string op)
        { int code = Integer(value, op); if (code != 0) throw new InvalidOperationException(op + " failed with native code " + code + "."); }
        private static void Success(object value, string op)
        { if (!Flag(value, op)) throw new InvalidOperationException(op + " reported failure."); }
        private static int Created(object value, string op)
        { int id = Integer(value, op); if (id <= 0) throw new InvalidOperationException(op + " failed with native code " + id + "."); return id; }
        private static int Bound(object value, string op)
        { int count = Count(value, op); if (count > 100000) throw new InvalidOperationException(op + " exceeds the 100000-entry allocation limit."); return count; }
        private static void Text(string text, string name)
        {
            if (text == null) throw new ArgumentNullException(name);
            if (string.IsNullOrWhiteSpace(text) || text.IndexOf('\0') >= 0) throw new ArgumentException("Provide a nonblank name without NUL characters.", name);
        }
        private static double[] Dimensions(double[] values, int count, string name)
        {
            if (values == null) throw new ArgumentNullException(name);
            if (values.Length != count || values.Any(v => double.IsNaN(v) || double.IsInfinity(v) || v <= 0))
                throw new ArgumentException("Provide exactly " + count + " finite positive dimensions.", name);
            return (double[])values.Clone();
        }
        internal bool DeleteMaterial(string mName)
        { Text(mName, nameof(mName)); return Run(p => Flag((object)p.DeleteMaterial(mName), "DeleteMaterial")); }
        private static void CountryCode(Country country)
        { if (!Enum.IsDefined(typeof(Country), country)) throw new ArgumentOutOfRangeException(nameof(country)); }
        internal int CreateBeamPropertyFromTable(Country country, string sectionName, TypeSpecification typeSpec, double additionalSpec1, double additionalSpec2)
        {
            CountryCode(country); Text(sectionName, nameof(sectionName));
            if (!Enum.IsDefined(typeof(TypeSpecification), typeSpec)) throw new ArgumentOutOfRangeException(nameof(typeSpec));
            Finite(additionalSpec1, nameof(additionalSpec1)); Finite(additionalSpec2, nameof(additionalSpec2));
            return Run(p => Integer((object)p.CreateBeamPropertyFromTable((int)country, sectionName, (int)typeSpec, additionalSpec1, additionalSpec2), "CreateBeamPropertyFromTable"));
        }
        internal int CreateCompositeBeamPropertyFromTable(Country country, string baseStaadName, TypeSpecification typeSpecification, double[] plateValuesMetres)
        {
            CountryCode(country); Text(baseStaadName, nameof(baseStaadName));
            if (typeSpecification != TypeSpecification.TC && typeSpecification != TypeSpecification.BC && typeSpecification != TypeSpecification.TB)
                throw new ArgumentOutOfRangeException(nameof(typeSpecification));
            var copy = Dimensions(plateValuesMetres, typeSpecification == TypeSpecification.TB ? 4 : 2, nameof(plateValuesMetres));
            return Run(p => Integer((object)p.CreateBeamPropertyFromTableWithCoverPlates((int)country, baseStaadName, (int)typeSpecification, copy), "CreateBeamPropertyFromTableWithCoverPlates"));
        }
        internal int CreateBuiltUpBeamProperty(string designation, double[] propertyValuesMetres)
        {
            // The main TaperISection path derives its display designation; the argument is not a native label.
            var copy = Dimensions(propertyValuesMetres, 7, nameof(propertyValuesMetres));
            return Run(p => Integer((object)p.CreateTaperedIProperty(copy), "CreateTaperedIProperty"));
        }
        internal void AssignBetaAnglesToBeams(HashSet<Beam> beams, int nThreads)
        {
            if (nThreads < 1 || nThreads > 512) throw new ArgumentOutOfRangeException(nameof(nThreads));
            var copy = Candidates(beams, nameof(beams)).Select(b => new { b.Id, b.BetaAngle }).ToArray();
            foreach (var item in copy) Finite(item.BetaAngle, nameof(beams));
            Write(p => { foreach (var b in copy) Success((object)p.AssignBetaAngle(b.Id, b.BetaAngle), "AssignBetaAngle"); });
        }
        internal void PopulateBetaAngles(IEnumerable<Beam> forBeams)
        {
            var copy = Candidates(forBeams, nameof(forBeams)).Distinct().ToArray();
            Write(p =>
            {
                // Stage reads before updating caller-owned Beam objects.
                var values = copy.Select(b => queries.GetBetaAngle(b.Id)).ToArray();
                if (values.Any(v => !v.IsSuccessful)) throw new InvalidOperationException("Cannot populate a beta angle without a successful native observation.");
                for (int i = 0; i < copy.Length; i++) copy[i].BetaAngle = values[i].Data;
            });
        }
        private static int[] MaterialIds(dynamic p, string name, bool beams)
        {
            string countOp = beams ? "GetIsotropicMaterialAssignedBeamCount" : "GetIsotropicMaterialAssignedPlateCount";
            string listOp = beams ? "GetIsotropicMaterialAssignedBeamList" : "GetIsotropicMaterialAssignedPlateList";
            int count = Bound(beams ? (object)p.GetIsotropicMaterialAssignedBeamCount(name) : (object)p.GetIsotropicMaterialAssignedPlateCount(name), countOp);
            if (count == 0) return new int[0];
            object buffer = new int[count];
            object response = beams ? (object)p.GetIsotropicMaterialAssignedBeamList(name, ref buffer) : (object)p.GetIsotropicMaterialAssignedPlateList(name, ref buffer);
            if (beams) Zero(response, listOp); else Success(response, listOp);
            int[] result = IntegerArray(buffer, count, listOp);
            if (result.Any(id => id <= 0)) throw new InvalidOperationException(listOp + " returned an unwritten or nonpositive ID.");
            int after = Bound(beams ? (object)p.GetIsotropicMaterialAssignedBeamCount(name) : (object)p.GetIsotropicMaterialAssignedPlateCount(name), countOp);
            if (count != after) throw new InvalidOperationException(listOp + " count changed during retrieval.");
            return result;
        }
        private HashSet<T> MaterialAssigned<T>(string name, IEnumerable<T> candidates, string parameter, bool beams) where T : class, IEntity
        {
            Text(name, "mName"); var copy = Candidates(candidates, parameter);
            return Run(p =>
            {
                int[] ids = MaterialIds(p, name, beams);
                // Main selects the first supplied entity for each ID, in native list order.
                return new HashSet<T>(ids.Select(id => copy.FirstOrDefault(item => item.Id == id)).Where(item => item != null));
            });
        }
        internal HashSet<Beam> GetIsotropicMaterialAssignedBeams(string mName, IEnumerable<Beam> allBeams) => MaterialAssigned(mName, allBeams, nameof(allBeams), true);
        internal HashSet<Plate> GetIsotropicMaterialAssignedPlates(string mName, IEnumerable<Plate> allPlates) => MaterialAssigned(mName, allPlates, nameof(allPlates), false);
        internal HashSet<Beam> GetIsotropicMaterialAssignedBeams(string mName, IOSGeometry geometry)
        {
            Text(mName, nameof(mName)); if (geometry == null) throw new ArgumentNullException(nameof(geometry));
            return Run(p => GetIsotropicMaterialAssignedBeams(mName, geometry.GetAllEntities<Beam>(1)));
        }
        internal HashSet<Plate> GetIsotropicMaterialAssignedPlates(string mName, IOSGeometry geometry)
        {
            Text(mName, nameof(mName)); if (geometry == null) throw new ArgumentNullException(nameof(geometry));
            return Run(p => GetIsotropicMaterialAssignedPlates(mName, geometry.GetAllEntities<Plate>(1)));
        }
        private static double[] Thickness(PlateElementThickness pet)
        {
            if (pet == null) throw new InvalidOperationException("pet must not be null.");
            var values = pet.ToArray();
            if (values.Any(v => double.IsNaN(v) || double.IsInfinity(v) || v < 0) || values.Take(3).Any(v => v == 0))
                throw new ArgumentException("Thicknesses A/B/C must be positive; D may be zero for the native triangular slot.", nameof(pet));
            return values;
        }
        internal void CreatePlateElementThickness(PlateElementThickness pet)
        { var values = Thickness(pet); Write(p => pet.Id = Created((object)p.CreatePlateThicknessProperty(values), "CreatePlateThicknessProperty")); }
        internal void CreatePlateElementThicknesses(HashSet<PlateElementThickness> petSet)
        {
            var copy = Definitions(petSet, nameof(petSet), true);
            var values = copy.Select(Thickness).ToArray();
            Write(p => { for (int i = 0; i < copy.Length; i++) copy[i].Id = Created((object)p.CreatePlateThicknessProperty(values[i]), "CreatePlateThicknessProperty"); });
        }
        private static Tuple<int[], int> ThicknessAssignment(PlateElementThickness pet)
        {
            if (pet == null || pet.AssignedPlates.Count == 0) return null;
            Positive(pet.Id, "pet.Id"); return Tuple.Create(Candidates(pet.AssignedPlates, "pet.AssignedPlates").Select(item => item.Id).ToArray(), pet.Id);
        }
        internal void AssignPlateElementThickness(PlateElementThickness pet)
        { var packet = ThicknessAssignment(pet); Write(p => { if (packet != null) Zero((object)p.AssignPlateThickness(packet.Item1, packet.Item2), "AssignPlateThickness"); }); }
        internal void AssignPlateElementThicknesses(HashSet<PlateElementThickness> petSet)
        {
            // Preserve the main single-definition null no-op within this assignment-only batch.
            var copy = petSet == null ? new PlateElementThickness[0] : petSet.ToArray();
            var packets = copy.Select(ThicknessAssignment).Where(item => item != null).ToArray();
            Write(p => { foreach (var packet in packets) Zero((object)p.AssignPlateThickness(packet.Item1, packet.Item2), "AssignPlateThickness"); });
        }
        private static PlateElementThickness ReadThickness(dynamic p, int id, bool property)
        {
            string op = property ? "GetThicknessPropertyValues" : "GetPlateThickness";
            object values = Buffer(4);
            object response = property ? (object)p.GetThicknessPropertyValues(id, ref values) : (object)p.GetPlateThickness(id, ref values);
            if (property) { if (Integer(response, op) != 4) throw new InvalidOperationException(op + " did not return four values."); }
            else Zero(response, op);
            var result = FloatingArray(values, 4, op);
            if (result.Any(v => v < 0)) throw new InvalidOperationException(op + " returned negative thickness.");
            return PlateElementThickness.Create(property ? id : 0, result);
        }
        internal PlateElementThickness GetPlateThicknessByPlateId(int plateId)
        { Positive(plateId, nameof(plateId)); return Run(p => ReadThickness(p, plateId, false)); }
        internal PlateElementThickness GetPlateThicknessByThicknessPropertyId(int petId)
        { Positive(petId, nameof(petId)); return Run(p => ReadThickness(p, petId, true)); }
        internal HashSet<PlateElementThickness> GetAllPlateElementThicknesses() => Run(p =>
        {
            var ids = ports.GetThicknessPropertyList();
            var result = new HashSet<PlateElementThickness>(ids.Select(id => (PlateElementThickness)ReadThickness(p, id, true)));
            if (!ids.SequenceEqual(ports.GetThicknessPropertyList())) throw new InvalidOperationException("Thickness inventory changed during value retrieval.");
            return result;
        });
        internal HashSet<PlateElementThickness> GetAllPlateElementThicknessesWithPopulatedAssignments(IEnumerable<Plate> allPlates)
        {
            var copy = new HashSet<Plate>(Candidates(allPlates, nameof(allPlates)));
            return Run(p =>
            {
                var result = GetAllPlateElementThicknesses();
                foreach (var pet in result) pet.AssignedPlates.UnionWith(ports.GetThicknessPropertyAssignedPlates(copy, pet.Id));
                return result;
            });
        }
        internal HashSet<PlateElementThickness> GetAllPlateElementThicknessesWithPopulatedAssignments(IOSGeometry geometry)
        {
            if (geometry == null) throw new ArgumentNullException(nameof(geometry));
            return Run(p => GetAllPlateElementThicknessesWithPopulatedAssignments(geometry.GetAllEntities<Plate>(1)));
        }
        private static void Selectors(OffsetLocation location, OffsetAxis axis)
        {
            if (location != OffsetLocation.Start && location != OffsetLocation.End) throw new ArgumentOutOfRangeException(nameof(location));
            if (axis != OffsetAxis.Global && axis != OffsetAxis.Local) throw new ArgumentOutOfRangeException(nameof(axis));
        }
        internal OffsetSpecification GetBeamOffsetSpecification(int bId, OffsetLocation location, OffsetAxis offsetAxis)
        {
            Positive(bId, nameof(bId)); Selectors(location, offsetAxis);
            return Run(p =>
            {
                var result = offsetAxis == OffsetAxis.Global ? queries.GetMemberGlobalOffset(bId, (MemberEnd)location) : queries.GetMemberLocalOffset(bId, (MemberEnd)location);
                if (!result.IsSuccessful && result.Status != PropertyQueryStatus.NoSpecification) throw new InvalidOperationException("Cannot resolve member offset: " + result.Status + ".");
                var vector = result.IsSuccessful ? result.Data : default;
                return new OffsetSpecification { Location = location, Axis = offsetAxis, X = vector.X, Y = vector.Y, Z = vector.Z };
            });
        }
        internal void PopulateMemberOffsets(IEnumerable<Beam> forBeams, OffsetAxis offsetAxis)
        {
            Selectors(OffsetLocation.Start, offsetAxis); var copy = Candidates(forBeams, nameof(forBeams)).Distinct().ToArray();
            Write(p =>
            {
                var values = copy.Select(b => new[] { GetBeamOffsetSpecification(b.Id, OffsetLocation.Start, offsetAxis), GetBeamOffsetSpecification(b.Id, OffsetLocation.End, offsetAxis) }).ToArray();
                for (int i = 0; i < copy.Length; i++) { copy[i].StartOffset = values[i][0]; copy[i].EndOffset = values[i][1]; }
            });
        }
        private static void Offset(OffsetSpecification spec)
        {
            if (spec == null) throw new InvalidOperationException("Offset definition must not be null.");
            Selectors(spec.Location, spec.Axis); Finite(spec.X, "X"); Finite(spec.Y, "Y"); Finite(spec.Z, "Z");
        }
        private static void CreateOffset(dynamic p, OffsetSpecification spec)
        { if (!spec.IsZero) spec.Id = Created((object)p.CreateMemberOffsetSpec((int)spec.Location, (int)spec.Axis, spec.X, spec.Y, spec.Z), "CreateMemberOffsetSpec"); }
        internal void CreateOffsetSpecification(OffsetSpecification oSpec)
        { Offset(oSpec); Write(p => CreateOffset(p, oSpec)); }
        internal void CreateOffsetSpecifications(HashSet<OffsetSpecification> osSet)
        {
            var copy = Definitions(osSet, nameof(osSet), true); foreach (var spec in copy) Offset(spec);
            Write(p => { foreach (var spec in copy) CreateOffset(p, spec); });
        }
        internal void AssignOffsetSpecifications(HashSet<OffsetSpecification> osSet)
        {
            var copy = Definitions(osSet, nameof(osSet), false);
            var packets = new List<Tuple<int[], int>>();
            foreach (var spec in copy)
            {
                Offset(spec); if (spec.IsZero) continue;
                var ids = Candidates(spec.Beams, "Beams").Select(b => b.Id).ToArray();
                if (ids.Length == 0) continue;
                Positive(spec.Id, "Id"); packets.Add(Tuple.Create(ids, spec.Id));
            }
            Write(p => { foreach (var packet in packets) Zero((object)p.AssignMemberSpecToBeam(packet.Item1, packet.Item2), "AssignMemberSpecToBeam"); });
        }
    }
}
