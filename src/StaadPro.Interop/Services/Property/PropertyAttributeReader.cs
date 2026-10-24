using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Models.Property;
using static StaadPro.Interop.Services.Property.PropertyValueReader;

namespace StaadPro.Interop.Services.Property
{
    internal sealed class PropertyAttributeReader
    {
        private readonly PropertyQueryReader queries;
        internal PropertyAttributeReader(PropertyQueryReader queries) { this.queries = queries; }
        private T Run<T>(Func<dynamic, T> body) => queries.WithBinding(p => body(p));
        private object Call(Func<dynamic, object> body) => queries.WithBinding(p => body(p));
        private static PropertyQueryResult<T> Result<T>(string op, PropertyQueryStatus status, T data = default, object raw = null, string diagnostic = null)
        { return new PropertyQueryResult<T>(op, status, data, raw == null || raw is bool ? (int?)null : Integer(raw, op), raw is bool flag ? flag : (bool?)null, diagnostic); }
        private static void Required(object value, string name) { if (value == null) throw new ArgumentNullException(name); }
        private static void Limit(int maximumResults) { if (maximumResults < 1 || maximumResults > 100000) throw new ArgumentOutOfRangeException(nameof(maximumResults)); }
        private PropertyQueryResult<int> DirectCount(string op, Func<dynamic, object> read) => Run(p =>
        { object raw = read(p); int count = Count(raw, op); return Result(op, PropertyQueryStatus.Success, count, raw); });
        internal PropertyQueryResult<int> GetInactiveMemberCount() => DirectCount("GetInactiveMemberCount", p => (object)p.GetInactiveMemberCount());
        internal PropertyQueryResult<int> GetFireProofedBeamCount() => DirectCount("GetFireProofedBeamCount", p => (object)p.GetFireProofedBeamCount());
        internal PropertyQueryResult<int> GetFireProofingSpecCount() => DirectCount("GetFireProofingSpecCount", p => (object)p.GetFireProofingSpecCount());
        internal PropertyQueryResult<int> GetMemberAttributeCount() => DirectCount("GetMemberAttributeCount", p => (object)p.GetMemberAttributeCount());
        internal PropertyQueryResult<int> GetFireProofingSpecAssignedBeamCount(int specificationIndex)
        {
            Positive(specificationIndex, nameof(specificationIndex));
            return DirectCount("GetFireProofingSpecAssignedBeamCount", p => (object)p.GetFireProofingSpecAssignedBeamCount(specificationIndex));
        }
        private delegate object ReadIdsCall(dynamic p, ref object ids);
        private PropertyQueryResult<IReadOnlyList<int>> ReadIds(string op, string countOp, Func<dynamic, object> readCount, ReadIdsCall readList, int maximumResults)
        {
            Limit(maximumResults);
            return Run(p =>
            {
                int count = Count(Call(readCount), countOp);
                if (count > maximumResults) return Result<IReadOnlyList<int>>(countOp, PropertyQueryStatus.LimitExceeded, raw: count);
                var copy = new int[count]; object raw = null;
                if (count > 0)
                {
                    object buffer = copy; raw = queries.WithBinding(q => readList(q, ref buffer));
                    if (!Flag(raw, op)) return Result<IReadOnlyList<int>>(op, PropertyQueryStatus.NativeFailure, raw: raw);
                    copy = IntegerArray(buffer, count, op);
                    if (copy.Any(id => id <= 0)) throw new InvalidOperationException(op + " returned invalid/unwritten IDs.");
                }
                int after = Count(Call(readCount), countOp);
                if (after != count) return Result<IReadOnlyList<int>>(countOp, PropertyQueryStatus.StateChanged, raw: after);
                return Result<IReadOnlyList<int>>(count == 0 ? countOp : op, PropertyQueryStatus.Success, Array.AsReadOnly(copy), count == 0 ? (object)count : raw);
            });
        }
        internal PropertyQueryResult<IReadOnlyList<int>> GetFireProofedBeamList(int maximumResults) => ReadIds("GetFireProofedBeamList", "GetFireProofedBeamCount",
            p => (object)p.GetFireProofedBeamCount(), (dynamic p, ref object ids) => (object)p.GetFireProofedBeamList(ref ids), maximumResults);
        internal PropertyQueryResult<IReadOnlyList<int>> GetFireProofingSpecAssignedBeamList(int specificationIndex, int maximumResults)
        {
            Positive(specificationIndex, nameof(specificationIndex));
            return ReadIds("GetFireProofingSpecAssignedBeamList", "GetFireProofingSpecAssignedBeamCount", p => (object)p.GetFireProofingSpecAssignedBeamCount(specificationIndex),
                (dynamic p, ref object ids) => (object)p.GetFireProofingSpecAssignedBeamList(specificationIndex, ref ids), maximumResults);
        }
        private PropertyQueryResult<FireProofingData> FireProof(int identity, bool member)
        {
            Positive(identity, member ? "memberId" : "specificationIndex");
            return Run(p =>
            {
                string op = member ? "GetFireProofDataForBeam" : "GetFireProofingSpecDetails";
                object type = int.MinValue, thickness = double.NaN, density = double.NaN, assignedCount = int.MinValue;
                object raw = member ? (object)p.GetFireProofDataForBeam(identity, ref type, ref thickness, ref density)
                    : (object)p.GetFireProofingSpecDetails(identity, ref type, ref thickness, ref density, ref assignedCount);
                if (!Flag(raw, op)) return Result<FireProofingData>(op, PropertyQueryStatus.NativeFailure, raw: raw);
                int typeCode = Integer(type, op); if (typeCode != 1 && typeCode != 2) throw new InvalidOperationException(op + " returned an unwritten or undocumented fireproofing type.");
                double t = Number(thickness, op), d = Number(density, op);
                if (t < 0 || d < 0) throw new InvalidOperationException(op + " returned negative fireproofing data.");
                int? count = member ? (int?)null : Count(assignedCount, op + " assignment count");
                return Result(op, PropertyQueryStatus.Success, new FireProofingData(member ? (int?)identity : null, member ? null : (int?)identity, (FireProofingType)typeCode, t, d, count), raw);
            });
        }
        internal PropertyQueryResult<FireProofingData> GetFireProofDataForBeam(int memberId) => FireProof(memberId, true);
        internal PropertyQueryResult<FireProofingData> GetFireProofingSpecDetails(int specificationIndex) => FireProof(specificationIndex, false);
        private PropertyQueryResult<PropertyAttributeMatchCount> AttributeCount(PropertyAttribute attribute, bool member)
        {
            Required(attribute, nameof(attribute));
            return Run(p =>
            {
                string op = member ? "GetMemberCountByAttribute" : "GetElementCountByAttribute"; string value = attribute.Value;
                object raw = member ? (object)p.GetMemberCountByAttribute(attribute.Name, ref value) : (object)p.GetElementCountByAttribute(attribute.Name, ref value);
                int count = Integer(raw, op);
                if (!string.Equals(value, attribute.Value, StringComparison.Ordinal)) return Result<PropertyAttributeMatchCount>(op, PropertyQueryStatus.StateChanged, raw: raw, diagnostic: "Native changed the input attribute value passed by reference; no data is attributed to the original pair.");
                if (count == -1) return Result<PropertyAttributeMatchCount>(op, PropertyQueryStatus.NativeFailure, raw: raw);
                if (count < 0) throw new InvalidOperationException(op + " returned an undocumented negative count.");
                return Result(op, PropertyQueryStatus.Success, new PropertyAttributeMatchCount(attribute, member ? PropertyAttributeScope.Member : PropertyAttributeScope.Element, count), raw);
            });
        }
        internal PropertyQueryResult<PropertyAttributeMatchCount> GetMemberCountByAttribute(PropertyAttribute attribute) => AttributeCount(attribute, true);
        internal PropertyQueryResult<PropertyAttributeMatchCount> GetElementCountByAttribute(PropertyAttribute attribute) => AttributeCount(attribute, false);
        private static PropertyQueryResult<PropertyAttributeEntityIds> NoIds(PropertyQueryResult<PropertyAttributeMatchCount> source)
            => new PropertyQueryResult<PropertyAttributeEntityIds>(source.NativeFunction, source.Status, nativeReturnCode: source.NativeReturnCode, nativeBooleanReturn: source.NativeBooleanReturn, diagnostic: source.Diagnostic);
        private PropertyQueryResult<PropertyAttributeEntityIds> AttributeIds(PropertyAttribute attribute, int maximumResults, bool member)
        {
            Required(attribute, nameof(attribute)); Limit(maximumResults);
            return Run(p =>
            {
                string op = member ? "GetMemberListByAttribute" : "GetElementListByAttribute";
                var before = AttributeCount(attribute, member); if (!before.IsSuccessful) return NoIds(before);
                int count = before.Data.Count;
                if (count > maximumResults) return Result<PropertyAttributeEntityIds>(before.NativeFunction, PropertyQueryStatus.LimitExceeded, raw: before.NativeReturnCode);
                var copy = new int[count]; object raw = null;
                if (count > 0)
                {
                    object buffer = copy; string value = attribute.Value;
                    raw = Call(q => member ? (object)q.GetMemberListByAttribute(attribute.Name, ref value, ref buffer) : (object)q.GetElementListByAttribute(attribute.Name, ref value, ref buffer));
                    int code = Integer(raw, op);
                    if (!string.Equals(value, attribute.Value, StringComparison.Ordinal)) return Result<PropertyAttributeEntityIds>(op, PropertyQueryStatus.StateChanged, raw: raw, diagnostic: "Native rewrote the requested attribute value during list retrieval.");
                    if (code == -1) return Result<PropertyAttributeEntityIds>(op, PropertyQueryStatus.NativeFailure, raw: raw);
                    if (code != 0) throw new InvalidOperationException(op + " returned an undocumented list status.");
                    copy = IntegerArray(buffer, count, op);
                    if (copy.Any(id => id <= 0)) throw new InvalidOperationException(op + " returned invalid/unwritten entity IDs.");
                }
                var after = AttributeCount(attribute, member); if (!after.IsSuccessful) return NoIds(after);
                if (after.Data.Count != count) return Result<PropertyAttributeEntityIds>(after.NativeFunction, PropertyQueryStatus.StateChanged, raw: after.NativeReturnCode);
                return Result(count == 0 ? before.NativeFunction : op, PropertyQueryStatus.Success, new PropertyAttributeEntityIds(attribute, before.Data.Scope, copy), count == 0 ? (object)count : raw);
            });
        }
        internal PropertyQueryResult<PropertyAttributeEntityIds> GetMemberListByAttribute(PropertyAttribute attribute, int maximumResults) => AttributeIds(attribute, maximumResults, true);
        internal PropertyQueryResult<PropertyAttributeEntityIds> GetElementListByAttribute(PropertyAttribute attribute, int maximumResults) => AttributeIds(attribute, maximumResults, false);
        private static string[] Strings(object value, int count, string op)
        {
            if (!(value is Array array) || array.Rank != 1 || array.Length != count || array.GetType().GetElementType() != typeof(string)) throw new InvalidOperationException(op + " returned a malformed String array.");
            var copy = new string[count]; int lower = array.GetLowerBound(0);
            for (int i = 0; i < count; i++)
            { copy[i] = (string)array.GetValue(lower + i); if (copy[i] == null || copy[i].IndexOf('\0') >= 0) throw new InvalidOperationException(op + " returned unwritten/NUL attribute text."); }
            return copy;
        }
        internal PropertyQueryResult<IReadOnlyList<PropertyAttribute>> GetMemberAttributeList(int maximumResults)
        {
            Limit(maximumResults);
            return Run(p =>
            {
                const string op = "GetMemberAttributeList", countOp = "GetMemberAttributeCount";
                int count = Count(Call(q => (object)q.GetMemberAttributeCount()), countOp);
                if (count > maximumResults) return Result<IReadOnlyList<PropertyAttribute>>(countOp, PropertyQueryStatus.LimitExceeded, raw: count);
                var pairs = new PropertyAttribute[count]; object raw = null;
                if (count > 0)
                {
                    object names = new string[count], values = new string[count]; raw = Call(q => (object)q.GetMemberAttributeList(ref names, ref values));
                    int returned = Count(raw, op);
                    if (returned != count) return Result<IReadOnlyList<PropertyAttribute>>(op, PropertyQueryStatus.StateChanged, raw: raw);
                    var n = Strings(names, count, op); var v = Strings(values, count, op);
                    for (int i = 0; i < count; i++) pairs[i] = new PropertyAttribute(n[i], v[i]);
                }
                int after = Count(Call(q => (object)q.GetMemberAttributeCount()), countOp);
                if (after != count) return Result<IReadOnlyList<PropertyAttribute>>(countOp, PropertyQueryStatus.StateChanged, raw: after);
                return Result<IReadOnlyList<PropertyAttribute>>(count == 0 ? countOp : op, PropertyQueryStatus.Success, Array.AsReadOnly(pairs), count == 0 ? (object)count : raw);
            });
        }
        internal PropertyQueryResult<AssignedAttributeSummary> GetAssignedAttributeCount(int memberOrPlateId)
        {
            Positive(memberOrPlateId, nameof(memberOrPlateId));
            return Run(p => { const string op = "GetAssignedAttributeCount"; object raw = p.GetAssignedAttributeCount(memberOrPlateId); int count = Count(raw, op);
                return Result(op, PropertyQueryStatus.Success, new AssignedAttributeSummary(memberOrPlateId, count), raw, "Native resolves a member first and a plate only if that member ID does not exist; no assigned-index context is certified."); });
        }
        private static PropertyMutationResult AttributeStatus(string op, object raw, PropertyAttribute attribute, PropertyAttributeScope scope, int[] ids = null)
        {
            int code = Integer(raw, op); if (code != 0 && code != -1) throw new InvalidOperationException(op + " returned an undocumented attribute status.");
            var target = ids == null ? PropertyMutationTargetKind.AttributeDefinition : scope == PropertyAttributeScope.Member ? PropertyMutationTargetKind.Member : scope == PropertyAttributeScope.Element ? PropertyMutationTargetKind.Element : PropertyMutationTargetKind.MemberOrPlate;
            return new PropertyMutationResult(op, code == 0 ? PropertyMutationStatus.Succeeded : PropertyMutationStatus.NativeFailure, raw, target, ids, attribute: attribute, attributeScope: scope);
        }
        internal PropertyMutationResult CreateMemberAttribute(PropertyAttribute attribute)
        { Required(attribute, nameof(attribute)); return Run(p => AttributeStatus("CreateMemberAttribute", (object)p.CreateMemberAttribute(attribute.Name, attribute.Value), attribute, PropertyAttributeScope.Member)); }
        internal PropertyMutationResult CreateElementAttribute(PropertyAttribute attribute)
        { Required(attribute, nameof(attribute)); return Run(p => AttributeStatus("CreateElementAttribute", (object)p.CreateElementAttribute(attribute.Name, attribute.Value), attribute, PropertyAttributeScope.Element)); }
        internal PropertyMutationResult DeleteMemberAttribute(PropertyAttribute attribute)
        { Required(attribute, nameof(attribute)); return Run(p => AttributeStatus("DeleteMemberAttribute", (object)p.DeleteMemberAttribute(attribute.Name, attribute.Value), attribute, PropertyAttributeScope.Member)); }
        internal PropertyMutationResult DeleteElementAttribute(PropertyAttribute attribute)
        { Required(attribute, nameof(attribute)); return Run(p => AttributeStatus("DeleteElementAttribute", (object)p.DeleteElementAttribute(attribute.Name, attribute.Value), attribute, PropertyAttributeScope.Element)); }
        private static int[] Ids(IEnumerable<int> ids, string name)
        {
            Required(ids, name); var copy = ids.Take(100001).ToArray();
            if (copy.Length < 1 || copy.Length > 100000 || copy.Any(id => id <= 0)) throw new ArgumentException("Supply 1..100000 positive native IDs.", name);
            return copy;
        }
        internal PropertyMutationResult AssignMemberAttribute(PropertyAttribute attribute, IEnumerable<int> memberIds)
        {
            Required(attribute, nameof(attribute)); var copy = Ids(memberIds, nameof(memberIds)); var identity = (int[])copy.Clone();
            return Run(p => AttributeStatus("AssignMemberAttribute", (object)p.AssignMemberAttribute(attribute.Name, attribute.Value, copy), attribute, PropertyAttributeScope.Member, identity));
        }
        internal PropertyMutationResult AssignElementAttribute(PropertyAttribute attribute, IEnumerable<int> elementIds)
        {
            Required(attribute, nameof(attribute)); var copy = Ids(elementIds, nameof(elementIds)); var identity = (int[])copy.Clone();
            return Run(p => AttributeStatus("AssignElementAttribute", (object)p.AssignElementAttribute(attribute.Name, attribute.Value, copy), attribute, PropertyAttributeScope.Element, identity));
        }
        internal PropertyMutationResult RemoveAttribute(PropertyAttribute attribute, IEnumerable<int> memberOrPlateIds)
        {
            Required(attribute, nameof(attribute)); var copy = Ids(memberOrPlateIds, nameof(memberOrPlateIds)); var identity = (int[])copy.Clone();
            return Run(p => AttributeStatus("RemoveAttribute", (object)p.RemoveAttribute(attribute.Name, attribute.Value, copy), attribute, PropertyAttributeScope.MemberOrPlate, identity));
        }
        private static void SectionName(string sectionName)
        { if (sectionName == null) throw new ArgumentNullException(nameof(sectionName)); if (string.IsNullOrWhiteSpace(sectionName) || sectionName.IndexOf('\0') >= 0) throw new ArgumentException("Use an exact nonblank UPT section name without NUL.", nameof(sectionName)); }
        internal PropertyMutationResult CreateUPTTable(UserProvidedTableType tableType)
        {
            if ((int)tableType < 1 || (int)tableType > 10) throw new ArgumentOutOfRangeException(nameof(tableType));
            return Run(p =>
            {
                const string op = "CreateUPTTable"; object raw = p.CreateUPTTable((int)tableType); int code = Integer(raw, op);
                if (code <= 0 && code != -6031) throw new InvalidOperationException(op + " returned an undocumented table creation result.");
                return new PropertyMutationResult(op, code > 0 ? PropertyMutationStatus.Created : PropertyMutationStatus.InvalidInput, raw, PropertyMutationTargetKind.UserProvidedTable, createdTableId: code > 0 ? (int?)code : null, tableType: tableType);
            });
        }
        internal PropertyMutationResult AddUPTPropertyWIDEFLANGE(int tableId, string sectionName, UptWideFlangeDefinition definition)
        {
            Positive(tableId, nameof(tableId)); SectionName(sectionName); Required(definition, nameof(definition));
            return Run(p =>
            {
                const string op = "AddUPTPropertyWIDEFLANGE";
                object raw = p.AddUPTPropertyWIDEFLANGE(tableId, sectionName, definition.AxialArea, definition.Depth, definition.WebThickness, definition.TopFlangeWidth, definition.TopFlangeThickness,
                    definition.NativeIz, definition.NativeIy, definition.NativeIx, definition.ShearAreaY, definition.ShearAreaZ); int code = Integer(raw, op);
                if (code != 0 && code != -6032 && code != -6045) throw new InvalidOperationException(op + " returned an undocumented insertion status.");
                return new PropertyMutationResult(op, code == 0 ? PropertyMutationStatus.Succeeded : code == -6045 ? PropertyMutationStatus.AlreadyExists : PropertyMutationStatus.NativeFailure, raw,
                    PropertyMutationTargetKind.UserProvidedSection, tableId: tableId, sectionName: sectionName);
            });
        }
        internal PropertyMutationResult AddUPTPropertyWIDEFLANGEUNEQUAL(int tableId, string sectionName, UptUnequalWideFlangeDefinition definition)
        {
            Positive(tableId, nameof(tableId)); SectionName(sectionName); Required(definition, nameof(definition)); var values = definition.NativeValues();
            return Run(p => { const string op = "AddUPTPropertyWIDEFLANGEUNEQUAL"; object raw = p.AddUPTPropertyWIDEFLANGEUNEQUAL(tableId, sectionName, values);
                return new PropertyMutationResult(op, Flag(raw, op) ? PropertyMutationStatus.Succeeded : PropertyMutationStatus.NativeFailure, raw, PropertyMutationTargetKind.UserProvidedSection, tableId: tableId, sectionName: sectionName); });
        }
    }
}
