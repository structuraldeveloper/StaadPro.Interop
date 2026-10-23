using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;
using StaadPro.Interop.Common;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using static StaadPro.Interop.Services.Property.PropertyValueReader;

namespace StaadPro.Interop.Services.Property
{
    // Main-interface compatibility ports. Keep signatures in IOSProperty/OSPropertyAdapter;
    // share serialization/lifetime checks with the existing typed observations.
    internal sealed class PropertyPortReader
    {
        private const int MaximumBufferLength = 100000;
        private readonly PropertyQueryReader shared;
        internal PropertyPortReader(PropertyQueryReader shared) { this.shared = shared; }
        private T Run<T>(Func<object, T> body) => shared.WithBinding(body);
        private static int BoundedCount(object raw, string op)
        {
            int count = Count(raw, op);
            if (count > MaximumBufferLength) throw new InvalidOperationException(op + " exceeds the 100000-entry allocation limit.");
            return count;
        }
        private static void Zero(object raw, string op)
        {
            int code = Integer(raw, op);
            if (code != 0) throw new InvalidOperationException(op + " failed with native code " + code + ".");
        }
        private static void Succeeded(object raw, string op)
        { if (!Flag(raw, op)) throw new InvalidOperationException(op + " reported failure."); }
        private static void AssignedListStatus(object raw, string op)
        {
            // The section-assignment help repeats a section-table return contract.
            // Accept its nonnegative integral evidence or TRUE; never mistake a negative error for success.
            if (raw is bool) Succeeded(raw, op);
            else if (Integer(raw, op) < 0) throw new InvalidOperationException(op + " reported a negative native error.");
        }
        private static int[] Ids(object raw, int count, string op)
        {
            int[] ids = IntegerArray(raw, count, op);
            if (ids.Any(id => id <= 0)) throw new InvalidOperationException(op + " returned an unwritten or nonpositive ID.");
            return ids;
        }
        private static int[] PropertyIds(dynamic native, bool section)
        {
            string op = section ? "GetSectionPropertyList" : "GetThicknessPropertyList";
            int count = BoundedCount(section ? (object)native.GetSectionPropertyCount() : (object)native.GetThicknessPropertyCount(), op);
            if (count == 0) return new int[0];
            object buffer = new int[count];
            Succeeded(section ? (object)native.GetSectionPropertyList(ref buffer) : (object)native.GetThicknessPropertyList(ref buffer), op);
            int[] result = Ids(buffer, count, op);
            int after = BoundedCount(section ? (object)native.GetSectionPropertyCount() : (object)native.GetThicknessPropertyCount(), op);
            if (after != count) throw new InvalidOperationException(op + " count changed during retrieval.");
            return result;
        }
        private static int AssignedCount(dynamic native, int propId, bool section) => BoundedCount(section
            ? (object)native.GetSectionPropertyAssignedBeamCount(propId)
            : (object)native.GetThicknessPropertyAssignedPlateCount(propId), section ? "GetSectionPropertyAssignedBeamCount" : "GetThicknessPropertyAssignedPlateCount");
        private static int[] AssignedIds(dynamic native, int propId, bool section)
        {
            int count = AssignedCount(native, propId, section);
            if (count == 0) return new int[0];
            object buffer = new int[count];
            string op = section ? "GetSectionPropertyAssignedBeamList" : "GetThicknessPropertyAssignedPlateList";
            object raw = section ? (object)native.GetSectionPropertyAssignedBeamList(propId, ref buffer)
                : (object)native.GetThicknessPropertyAssignedPlateList(propId, ref buffer);
            if (section) AssignedListStatus(raw, op); else Succeeded(raw, op);
            int[] result = Ids(buffer, count, op);
            if (AssignedCount(native, propId, section) != count) throw new InvalidOperationException(op + " count changed during retrieval.");
            return result;
        }
        internal int[] GetSectionPropertyList() => Run(p => PropertyIds(p, true));
        internal int[] GetThicknessPropertyList() => Run(p => PropertyIds(p, false));
        internal HashSet<int> GetUnusedSectionsIds() => Run(p =>
        {
            dynamic native = p;
            return new HashSet<int>(PropertyIds(p, true).Where(id => AssignedCount(native, id, true) == 0));
        });
        internal int GetUnusedSectionsCount() => GetUnusedSectionsIds().Count;
        internal int GetBeamProperty(int memberId)
        {
            Positive(memberId, nameof(memberId));
            return Run(p =>
            {
                foreach (int id in PropertyIds(p, true))
                    if (AssignedIds(p, id, true).Contains(memberId)) return id;
                return 0;
            });
        }
        internal string GetPlateMaterialName(int pId)
        {
            Positive(pId, nameof(pId));
            return Run(p =>
            {
                object name = ((dynamic)p).GetPlateMaterialName(pId);
                if (name == null || name is string) return (string)name;
                throw new InvalidOperationException("GetPlateMaterialName returned a non-string value.");
            });
        }
        internal string GetSectionPropertyName(int propId)
        {
            Positive(propId, nameof(propId));
            return Run(p =>
            {
                string sentinel = "<unwritten:" + Guid.NewGuid().ToString("N") + ">";
                object name = sentinel;
                Zero((object)((dynamic)p).GetSectionPropertyName(propId, ref name), "GetSectionPropertyName");
                if (!(name is string text) || text == sentinel) throw new InvalidOperationException("GetSectionPropertyName left its string output unwritten or malformed.");
                return text;
            });
        }
        internal double[] GetSectionPropertyValues(int propId)
        {
            Positive(propId, nameof(propId));
            return Run(p =>
            {
                dynamic native = p;
                const string op = "GetSectionPropertyValuesEx";
                int count = BoundedCount((object)native.GetCountofSectionPropertyValuesEx(), op);
                if (count == 0) throw new InvalidOperationException(op + " reported no output capacity.");
                // Preserve the main project's zero-padded, unparsed native section buffer.
                object buffer = new double[count], sectionType = int.MinValue;
                Succeeded((object)native.GetSectionPropertyValuesEx(propId, ref sectionType, ref buffer), op);
                if (Integer(sectionType, op) < 0) throw new InvalidOperationException(op + " returned an invalid or unwritten section type.");
                var values = FloatingArray(buffer, count, op);
                if (BoundedCount((object)native.GetCountofSectionPropertyValuesEx(), op) != count)
                    throw new InvalidOperationException(op + " output capacity changed during retrieval.");
                return values;
            });
        }
        private static T[] Entities<T>(HashSet<T> entities, string name) where T : class, IEntity
        {
            if (entities == null) throw new ArgumentNullException(name);
            var copy = entities.ToArray();
            if (copy.Any(item => item == null || item.Id <= 0)) throw new ArgumentException("Entities must be non-null and have positive analytical IDs.", name);
            return copy;
        }
        private HashSet<T> Assigned<T>(HashSet<T> entities, int propId, bool section, string name) where T : class, IEntity
        {
            var copy = Entities(entities, name); Positive(propId, nameof(propId));
            return Run(p =>
            {
                var ids = new HashSet<int>(AssignedIds(p, propId, section));
                return new HashSet<T>(copy.Where(item => ids.Contains(item.Id)));
            });
        }
        internal HashSet<Beam> GetSectionPropertyAssignedBeams(HashSet<Beam> allBeams, int propId) => Assigned(allBeams, propId, true, nameof(allBeams));
        internal HashSet<Plate> GetThicknessPropertyAssignedPlates(HashSet<Plate> allPlates, int propId) => Assigned(allPlates, propId, false, nameof(allPlates));
        private HashSet<T> Missing<T>(HashSet<T> entities, IEnumerable<int> propIds, bool section, string name) where T : class, IEntity
        {
            var copy = Entities(entities, name);
            if (propIds == null) throw new ArgumentNullException(nameof(propIds));
            int[] properties = propIds.ToArray();
            if (properties.Any(id => id <= 0)) throw new ArgumentException("Property IDs must be positive.", nameof(propIds));
            return Run(p =>
            {
                var assigned = new HashSet<int>();
                foreach (int id in properties) assigned.UnionWith(AssignedIds(p, id, section));
                return new HashSet<T>(copy.Where(item => !assigned.Contains(item.Id)));
            });
        }
        internal HashSet<Beam> GetBeamsWithMissingSectionProperty(HashSet<Beam> allBeams, IEnumerable<int> propIds) => Missing(allBeams, propIds, true, nameof(allBeams));
        internal HashSet<Plate> GetPlatesWithMissingThicknessProperty(HashSet<Plate> allPlates, IEnumerable<int> propIds) => Missing(allPlates, propIds, false, nameof(allPlates));
        internal double GetAveragePlateThickness(int plateId)
        {
            Positive(plateId, nameof(plateId));
            return Run(p =>
            {
                object buffer = Buffer(4);
                Zero((object)((dynamic)p).GetPlateThickness(plateId, ref buffer), "GetPlateThickness");
                double[] values = FloatingArray(buffer, 4, "GetPlateThickness");
                // Retain source arithmetic and .NET midpoint-to-even rounding to five decimal places.
                double mean = (values[0] + values[1] + values[2] + values[3]) / 4;
                if (double.IsInfinity(mean)) throw new InvalidOperationException("The thickness mean overflowed.");
                return Math.Round(mean, 5);
            });
        }
        internal MemberSpecificationType GetMemberSpecification(int msId)
        {
            Positive(msId, nameof(msId));
            return Run(p =>
            {
                object code = int.MinValue;
                Succeeded((object)((dynamic)p).GetMemberSpecCode(msId, ref code), "GetMemberSpecCode");
                int value = Integer(code, "GetMemberSpecCode");
                if (!Enum.IsDefined(typeof(MemberSpecificationType), value)) throw new InvalidOperationException("GetMemberSpecCode returned an unknown or unwritten code.");
                return (MemberSpecificationType)value;
            });
        }
        private static int[] MemberIds(int[] memberIds)
        {
            if (memberIds == null || memberIds.Length == 0) return null;
            var copy = (int[])memberIds.Clone();
            if (copy.Any(id => id <= 0)) throw new ArgumentException("Member IDs must be positive.", nameof(memberIds));
            return copy;
        }
        internal void AssignBeamProperty(int memberId, int propertyId)
        { Positive(memberId, nameof(memberId)); Positive(propertyId, nameof(propertyId)); AssignBeam(memberId, propertyId); }
        internal void AssignBeamProperty(int[] memberIds, int propertyId)
        {
            var ids = MemberIds(memberIds); if (ids == null) return;
            Positive(propertyId, nameof(propertyId)); AssignBeam(ids, propertyId);
        }
        private void AssignBeam(object ids, int propertyId) => Run(p => { Zero((object)((dynamic)p).AssignBeamProperty(ids, propertyId), "AssignBeamProperty"); return true; });
        private static void Name(string materialName)
        { if (materialName.IndexOf('\0') >= 0) throw new ArgumentException("Material names cannot contain NUL.", nameof(materialName)); }
        internal void AssignMaterialToMember(string materialName, int memberId)
        {
            if (string.IsNullOrWhiteSpace(materialName)) return;
            Name(materialName); Positive(memberId, nameof(memberId)); AssignMaterial(materialName, memberId);
        }
        internal void AssignMaterialToMember(string materialName, int[] memberIds)
        {
            if (string.IsNullOrWhiteSpace(materialName)) return;
            var ids = MemberIds(memberIds); if (ids == null) return;
            Name(materialName); AssignMaterial(materialName, ids);
        }
        private void AssignMaterial(string name, object ids) => Run(p => { Succeeded((object)((dynamic)p).AssignMaterialToMember(name, ids), "AssignMaterialToMember"); return true; });
        internal void SetMaterialName(string materialName)
        {
            if (string.IsNullOrWhiteSpace(materialName)) return;
            Name(materialName);
            Run(p => { Succeeded((object)((dynamic)p).SetMaterialName(materialName), "SetMaterialName"); return true; });
        }
        private static void Location(int location)
        { if (location != 0 && location != 1) throw new ArgumentOutOfRangeException(nameof(location)); }
        private static void Finite(double value, string name)
        { if (double.IsNaN(value) || double.IsInfinity(value)) throw new ArgumentOutOfRangeException(name, "An offset must be finite."); }
        internal bool AssignMemberEndOffsets(int memberId, double startY, double endY)
        { Positive(memberId, nameof(memberId)); Finite(startY, nameof(startY)); Finite(endY, nameof(endY)); return Offset(memberId, 0, startY, endY); }
        internal bool AssignMemberEndOffsets(int[] memberIds, double startY, double endY)
        {
            var ids = MemberIds(memberIds); if (ids == null) return false;
            Finite(startY, nameof(startY)); Finite(endY, nameof(endY)); return Offset(ids, 0, startY, endY);
        }
        internal bool AssignMemberEndOffset(int memberId, int location, double offsetY)
        { Positive(memberId, nameof(memberId)); Location(location); Finite(offsetY, nameof(offsetY)); return Offset(memberId, location, offsetY, null); }
        internal bool AssignMemberEndOffset(int[] memberIds, int location, double offsetY)
        {
            var ids = MemberIds(memberIds); if (ids == null) return false;
            Location(location); Finite(offsetY, nameof(offsetY)); return Offset(ids, location, offsetY, null);
        }
        private bool Offset(object ids, int location, double y, double? endY) => Run(p =>
        {
            dynamic native = p;
            try
            {
                int first = Integer((object)native.CreateMemberOffsetSpec(location, 0, 0.0, y, 0.0), "CreateMemberOffsetSpec");
                if (first <= 0) return false;
                int second = 0;
                if (endY.HasValue)
                {
                    second = Integer((object)native.CreateMemberOffsetSpec(1, 0, 0.0, endY.Value, 0.0), "CreateMemberOffsetSpec");
                    if (second <= 0) return false;
                }
                if (Integer((object)native.AssignMemberSpecToBeam(ids, first), "AssignMemberSpecToBeam") != 0) return false;
                return !endY.HasValue || Integer((object)native.AssignMemberSpecToBeam(ids, second), "AssignMemberSpecToBeam") == 0;
            }
            catch (COMException) { return false; }
            catch (RuntimeBinderException) { return false; }
        });
    }
}
