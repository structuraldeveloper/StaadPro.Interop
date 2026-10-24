using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Property;
using static StaadPro.Interop.Services.Property.PropertyValueReader;

namespace StaadPro.Interop.Services.Property
{
    internal sealed class PropertyUptReader
    {
        private readonly OpenStaadWrapper wrapper;
        private readonly PropertyQueryReader queries;
        internal PropertyUptReader(OpenStaadWrapper wrapper, PropertyQueryReader queries) { this.wrapper = wrapper; this.queries = queries; }
        private T Run<T>(Func<dynamic, T> body) => queries.WithBinding(p => body(p));
        private object Call(Func<dynamic, object> body) => queries.WithBinding(p => body(p));
        private static PropertyQueryResult<T> Result<T>(string op, PropertyQueryStatus status, T data = default, object raw = null, string diagnostic = null)
            => new PropertyQueryResult<T>(op, status, data, raw == null || raw is bool ? (int?)null : Integer(raw, op), raw is bool flag ? flag : (bool?)null, diagnostic);
        private static void Name(string sectionName)
        { if (sectionName == null) throw new ArgumentNullException(nameof(sectionName)); if (string.IsNullOrWhiteSpace(sectionName) || sectionName.IndexOf('\0') >= 0) throw new ArgumentException("Supply an exact nonblank native section name without NUL.", nameof(sectionName)); }
        private static void Input(int tableId, string sectionName, object definition)
        { Positive(tableId, nameof(tableId)); Name(sectionName); if (definition == null) throw new ArgumentNullException(nameof(definition)); }
        private static void TableType(UserProvidedTableType tableType)
        { if ((int)tableType < 1 || (int)tableType > 10) throw new ArgumentOutOfRangeException(nameof(tableType)); }
        private Version Snapshot()
        {
            var v = wrapper.StaadVersion;
            return v == null || v.Major < 0 || v.Update < 0 || v.Revision < 0 || v.Build < 0 ? null : new Version(v.Major, v.Update, v.Revision, v.Build);
        }
        private Version Reviewed(string op)
        {
            var version = Snapshot();
            if (!UptContractProfiles.IsReviewed(version)) throw new NotSupportedException(op + " requires a reviewed acquired native contract profile: 24.00.02.354, 25.00.01.424 or 26.00.00.340. Host ApplicationVersion does not select this policy.");
            return version;
        }
        private static PropertyMutationResult Insert(string op, object raw, int tableId, string sectionName)
        {
            int code = Integer(raw, op);
            if (code != 0 && code != -6032 && code != -6045) throw new InvalidOperationException(op + " returned an undocumented insertion status.");
            return new PropertyMutationResult(op, code == 0 ? PropertyMutationStatus.Succeeded : code == -6045 ? PropertyMutationStatus.AlreadyExists : PropertyMutationStatus.NativeFailure,
                raw, PropertyMutationTargetKind.UserProvidedSection, tableId: tableId, sectionName: sectionName);
        }
        internal PropertyMutationResult AddUPTPropertyANGLE(int tableId, string sectionName, UptAngleDefinition definition)
        {
            Input(tableId, sectionName, definition);
            return Run(p => Insert("AddUPTPropertyANGLE", (object)p.AddUPTPropertyANGLE(tableId, sectionName, definition.Depth, definition.FlangeWidth, definition.FlangeThickness, definition.PrincipalRadiusOfGyration, definition.ShearAreaY, definition.ShearAreaZ), tableId, sectionName));
        }
        internal PropertyMutationResult AddUPTPropertyDOUBLEANGLE(int tableId, string sectionName, UptDoubleAngleDefinition definition)
        {
            Input(tableId, sectionName, definition);
            return Run(p => Insert("AddUPTPropertyDOUBLEANGLE", (object)p.AddUPTPropertyDOUBLEANGLE(tableId, sectionName, definition.Depth, definition.FlangeWidth, definition.FlangeThickness, definition.Spacing, definition.NativeIz, definition.NativeIy, definition.NativeIx, definition.NativeCy, definition.ShearAreaY, definition.ShearAreaZ), tableId, sectionName));
        }
        internal PropertyMutationResult AddUPTPropertyPIPE(int tableId, string sectionName, UptPipeDefinition definition)
        {
            Input(tableId, sectionName, definition);
            return Run(p => Insert("AddUPTPropertyPIPE", (object)p.AddUPTPropertyPIPE(tableId, sectionName, definition.OuterDiameter, definition.InnerDiameter, definition.ShearAreaY, definition.ShearAreaZ), tableId, sectionName));
        }
        internal PropertyMutationResult AddUPTPropertyTUBE(int tableId, string sectionName, UptTubeDefinition definition)
        {
            Input(tableId, sectionName, definition);
            return Run(p => Insert("AddUPTPropertyTUBE", (object)p.AddUPTPropertyTUBE(tableId, sectionName, definition.AxialArea, definition.Depth, definition.FlangeWidth, definition.FlangeThickness, definition.NativeIz, definition.NativeIy, definition.NativeIx, definition.ShearAreaY, definition.ShearAreaZ), tableId, sectionName));
        }
        internal PropertyMutationResult AddUPTPropertyGENERAL(int tableId, string sectionName, UptGeneralDefinition definition)
        {
            Input(tableId, sectionName, definition);
            return Run(p => Insert("AddUPTPropertyGENERAL", (object)p.AddUPTPropertyGENERAL(tableId, sectionName, definition.AxialArea, definition.Depth, definition.DepthElementThickness, definition.Width, definition.FlangeElementThickness, definition.NativeIz, definition.NativeIy, definition.NativeIx, definition.SectionModulusZ, definition.SectionModulusY, definition.ShearAreaY, definition.ShearAreaZ, definition.PlasticModulusZ, definition.PlasticModulusY, definition.WarpingConstant, definition.WebDepth), tableId, sectionName));
        }
        internal PropertyMutationResult AddUPTPropertyISECTION(int tableId, string sectionName, UptISectionDefinition definition)
        {
            Input(tableId, sectionName, definition);
            return Run(p => Insert("AddUPTPropertyISECTION", (object)p.AddUPTPropertyISECTION(tableId, sectionName, definition.StartDepth, definition.WebThickness, definition.EndDepth, definition.TopFlangeWidth, definition.TopFlangeThickness, definition.BottomFlangeWidth, definition.BottomFlangeThickness, definition.ShearAreaY, definition.ShearAreaZ, definition.TorsionalConstant), tableId, sectionName));
        }
        internal PropertyMutationResult AddUPTPropertyPRISMATIC(int tableId, string sectionName, UptPrismaticDefinition definition)
        {
            Input(tableId, sectionName, definition);
            return Run(p => Insert("AddUPTPropertyPRISMATIC", (object)p.AddUPTPropertyPRISMATIC(tableId, sectionName, definition.AxialArea, definition.NativeIz, definition.NativeIy, definition.NativeIx, definition.ShearAreaY, definition.ShearAreaZ, definition.DepthY, definition.DepthZ), tableId, sectionName));
        }
        internal PropertyMutationResult AddUPTPropertyCHANNEL(int tableId, string sectionName, UptChannelDefinition definition)
        {
            Input(tableId, sectionName, definition); var d = definition.PrimaryValues;
            return Run(p => Insert("AddUPTPropertyCHANNEL", (object)p.AddUPTPropertyCHANNEL(tableId, sectionName, d.AxialArea, d.Depth, d.WebThickness, d.TopFlangeWidth, d.TopFlangeThickness, d.NativeIz, d.NativeIy, d.NativeIx, definition.NativeCz, d.ShearAreaY, d.ShearAreaZ), tableId, sectionName));
        }
        internal PropertyMutationResult AddUPTPropertyTEE(int tableId, string sectionName, UptTeeDefinition definition)
        {
            Input(tableId, sectionName, definition); var d = definition.PrimaryValues;
            return Run(p => Insert("AddUPTPropertyTEE", (object)p.AddUPTPropertyTEE(tableId, sectionName, d.AxialArea, d.Depth, d.TopFlangeWidth, d.TopFlangeThickness, d.WebThickness, d.NativeIz, d.NativeIy, d.NativeIx, definition.NativeCy, d.ShearAreaY, d.ShearAreaZ), tableId, sectionName));
        }
        internal PropertyMutationResult AddUPTPropertyWIDEFLANGECOMPOSITE(int tableId, string sectionName, UptCompositeWideFlangeDefinition definition)
        {
            Input(tableId, sectionName, definition); var values = definition.NativeValues();
            return Run(p => { const string op = "AddUPTPropertyWIDEFLANGECOMPOSITE"; object raw = p.AddUPTPropertyWIDEFLANGECOMPOSITE(tableId, sectionName, values);
                return new PropertyMutationResult(op, Flag(raw, op) ? PropertyMutationStatus.Succeeded : PropertyMutationStatus.NativeFailure, raw, PropertyMutationTargetKind.UserProvidedSection, tableId: tableId, sectionName: sectionName); });
        }
        private static PropertyMutationResult CreatedProperty(string op, object raw, int tableId, string sectionName, bool minusOneFailure)
        {
            int code = Integer(raw, op);
            if (minusOneFailure && code == -1) return new PropertyMutationResult(op, PropertyMutationStatus.NativeFailure, raw, PropertyMutationTargetKind.SectionDefinition, tableId: tableId, sectionName: sectionName);
            if (code <= 0) throw new InvalidOperationException(op + " returned a nonpositive or undocumented property reference " + code + ".");
            return new PropertyMutationResult(op, PropertyMutationStatus.Created, raw, PropertyMutationTargetKind.SectionDefinition, createdPropertyId: code, tableId: tableId, sectionName: sectionName);
        }
        internal PropertyMutationResult CreatePropertyFromUPTTable(int tableId, string sectionName)
        {
            Positive(tableId, nameof(tableId)); Name(sectionName);
            return Run(p => CreatedProperty("CreatePropertyFromUPTTable", (object)p.CreatePropertyFromUPTTable(tableId, sectionName), tableId, sectionName, true));
        }
        internal PropertyMutationResult CreatePropertyFromUserTable(string sectionName, int tableId)
        {
            Positive(tableId, nameof(tableId)); Name(sectionName);
            return Run(p => CreatedProperty("CreatePropertyFromUserTable", (object)p.CreatePropertyFromUserTable(sectionName, tableId), tableId, sectionName, false));
        }
        internal PropertyMutationResult CreateUPTTableEx(int requestedTableId, UserProvidedTableType tableType)
        {
            Positive(requestedTableId, nameof(requestedTableId)); TableType(tableType);
            return Run(p =>
            {
                const string op = "CreateUPTTableEx"; object raw = p.CreateUPTTableEx(requestedTableId, (int)tableType); int code = Integer(raw, op);
                if (code < 0) throw new InvalidOperationException(op + " returned an undocumented negative result.");
                return new PropertyMutationResult(op, code == 0 ? PropertyMutationStatus.NativeFailure : PropertyMutationStatus.Created, raw, PropertyMutationTargetKind.UserProvidedTable,
                    tableId: requestedTableId, createdTableId: code > 0 ? (int?)code : null, tableType: tableType);
            });
        }
        internal PropertyMutationResult RemoveUPTTable(int tableId)
        {
            Positive(tableId, nameof(tableId));
            return Run(p => { const string op = "RemoveUPTTable"; object raw = p.RemoveUPTTable(tableId);
                return new PropertyMutationResult(op, Flag(raw, op) ? PropertyMutationStatus.Succeeded : PropertyMutationStatus.NativeFailure, raw, PropertyMutationTargetKind.UserProvidedTable, tableId: tableId); });
        }
        internal PropertyMutationResult RemovePropertyFromUPTTable(int tableId, string sectionName)
        {
            Positive(tableId, nameof(tableId)); Name(sectionName);
            return Run(p =>
            {
                const string op = "RemovePropertyFromUPTTable"; object raw = p.RemovePropertyFromUPTTable(tableId, sectionName); int code = Integer(raw, op);
                if (code != 0 && code != 1 && code != -100) throw new InvalidOperationException(op + " returned an undocumented removal status.");
                return new PropertyMutationResult(op, code == 1 ? PropertyMutationStatus.Succeeded : code == -100 ? PropertyMutationStatus.InvalidInput : PropertyMutationStatus.NativeFailure,
                    raw, PropertyMutationTargetKind.UserProvidedSection, tableId: tableId, sectionName: sectionName);
            });
        }
        internal PropertyQueryResult<int> GetUserProvidedTableCount() => Run(p =>
        { const string op = "GetUserProvidedTableCount"; object raw = p.GetUserProvidedTableCount(); return Result(op, PropertyQueryStatus.Success, Count(raw, op), raw); });
        internal PropertyQueryResult<IReadOnlyList<int>> GetUserProvidedTableList(int maximumResults)
        {
            if (maximumResults < 1 || maximumResults > 100000) throw new ArgumentOutOfRangeException(nameof(maximumResults));
            return Run(p =>
            {
                const string op = "GetUserProvidedTableList", countOp = "GetUserProvidedTableCount";
                int count = Count(Call(q => (object)q.GetUserProvidedTableCount()), countOp);
                if (count > maximumResults) return Result<IReadOnlyList<int>>(countOp, PropertyQueryStatus.LimitExceeded, raw: count);
                var copy = new int[count]; object raw = null;
                if (count > 0)
                {
                    object buffer = copy; raw = Call(q => (object)q.GetUserProvidedTableList(ref buffer));
                    if (!Flag(raw, op)) return Result<IReadOnlyList<int>>(op, PropertyQueryStatus.NativeFailure, raw: raw);
                    copy = IntegerArray(buffer, count, op); if (copy.Any(id => id <= 0)) throw new InvalidOperationException(op + " returned invalid/unwritten table IDs.");
                }
                int after = Count(Call(q => (object)q.GetUserProvidedTableCount()), countOp);
                if (after != count) return Result<IReadOnlyList<int>>(countOp, PropertyQueryStatus.StateChanged, raw: after);
                return Result<IReadOnlyList<int>>(count == 0 ? countOp : op, PropertyQueryStatus.Success, Array.AsReadOnly(copy), count == 0 ? (object)count : raw);
            });
        }
        private int SectionCount(int tableId) => Count(Call(q => (object)q.GetUserProvidedTableSectionCount(tableId)), "GetUserProvidedTableSectionCount");
        internal PropertyQueryResult<UptTableSectionCount> GetUserProvidedTableSectionCount(int tableId)
        {
            Positive(tableId, nameof(tableId));
            return Run(p => { const string op = "GetUserProvidedTableSectionCount"; object raw = p.GetUserProvidedTableSectionCount(tableId);
                return Result(op, PropertyQueryStatus.Success, new UptTableSectionCount(tableId, Count(raw, op)), raw); });
        }
        internal PropertyQueryResult<UptTableSectionType> GetUserProvidedTableSectionType(int tableId)
        {
            Positive(tableId, nameof(tableId));
            return Run(p =>
            {
                const string op = "GetUserProvidedTableSectionType"; int count = SectionCount(tableId); object type = int.MinValue;
                object raw = Call(q => (object)q.GetUserProvidedTableSectionType(tableId, ref type)); int returned = Count(raw, op);
                if (returned != count) return Result<UptTableSectionType>(op, PropertyQueryStatus.StateChanged, raw: raw);
                int after = SectionCount(tableId);
                if (after != count) return Result<UptTableSectionType>("GetUserProvidedTableSectionCount", PropertyQueryStatus.StateChanged, raw: after);
                int code = Integer(type, op);
                if (count == 0 && (code == int.MinValue || code == 0)) return Result<UptTableSectionType>(op, PropertyQueryStatus.EmptyOrUnavailable, raw: raw, diagnostic: "No written type is available for an empty or unavailable native table.");
                if (code <= 0) throw new InvalidOperationException(op + " returned an unwritten or invalid scalar type code.");
                return Result(op, PropertyQueryStatus.Success, new UptTableSectionType(tableId, code, count), raw);
            });
        }
        internal PropertyQueryResult<UptNamedSectionCount> GetUserProvidedTableSectionPropertyCount(int tableId, string sectionName)
        {
            Positive(tableId, nameof(tableId)); Name(sectionName);
            return Run(p => { const string op = "GetUserProvidedTableSectionPropertyCount"; object raw = p.GetUserProvidedTableSectionPropertyCount(tableId, sectionName);
                return Result(op, PropertyQueryStatus.Success, new UptNamedSectionCount(tableId, sectionName, Count(raw, op)), raw,
                    "The native help describes this return as a section count despite the method name; it is retained as an observation and never used as property-buffer capacity."); });
        }
        private static int Capacity(object raw)
        { int count = Count(raw, "GetCountofSectionPropertyValuesEx"); if (count == 0 || count > 100000) throw new InvalidOperationException("UPT buffer capacity must be 1..100000."); return count; }
        internal PropertyQueryResult<UptSectionProperties> GetUserProvidedTableSectionProperties(int tableId, string sectionName)
        {
            Positive(tableId, nameof(tableId)); Name(sectionName);
            return Run(p =>
            {
                const string op = "GetUserProvidedTableSectionProperties"; var version = Reviewed(op);
                int capacity = Capacity(Call(q => (object)q.GetCountofSectionPropertyValuesEx())); object type = int.MinValue, values = Buffer(capacity);
                object raw = Call(q => (object)q.GetUserProvidedTableSectionProperties(tableId, sectionName, ref type, ref values));
                if (!Flag(raw, op)) return Result<UptSectionProperties>(op, PropertyQueryStatus.NativeFailure, raw: raw);
                int code = Integer(type, op); var fields = UptContractProfiles.Fields(code);
                if (fields.Length > capacity) throw new InvalidOperationException(op + " capacity is smaller than its applicable layout.");
                var copy = FloatingArray(values, capacity, op, false); for (int i = 0; i < fields.Length; i++) Number(copy[i], op + " " + fields[i]);
                int after = Capacity(Call(q => (object)q.GetCountofSectionPropertyValuesEx()));
                if (after != capacity || !Equals(version, Snapshot())) return Result<UptSectionProperties>(op, PropertyQueryStatus.StateChanged, raw: raw);
                return Result(op, PropertyQueryStatus.Success, new UptSectionProperties(tableId, sectionName, code, version, capacity, fields, copy), raw);
            });
        }
        internal PropertyQueryResult<UptTableReference> GetUserProvidedTableNo(int nativeTableIndex)
        {
            Nonnegative(nativeTableIndex, nameof(nativeTableIndex));
            return Run(p =>
            {
                const string op = "GetUserProvidedTableNo"; object raw = p.GetUserProvidedTableNo(nativeTableIndex); int code = Integer(raw, op);
                if (code == -1) return Result<UptTableReference>(op, PropertyQueryStatus.NativeFailure, raw: raw);
                if (code <= 0) throw new InvalidOperationException(op + " returned an undocumented nonpositive table ID.");
                return Result(op, PropertyQueryStatus.Success, new UptTableReference(nativeTableIndex, code), raw);
            });
        }
        internal PropertyQueryResult<SectionShapeCode> GetShapeCode(Country country, string sectionName)
        {
            if ((int)country < 1 || (int)country > 37) throw new ArgumentOutOfRangeException(nameof(country)); Name(sectionName);
            return Run(p =>
            {
                const string op = "GetShapeCode"; object raw = p.GetShapeCode((int)country, sectionName); int code = Integer(raw, op);
                if (code == -1) return Result<SectionShapeCode>(op, PropertyQueryStatus.NativeFailure, raw: raw);
                if (code < 0) throw new InvalidOperationException(op + " returned an undocumented negative shape code.");
                return Result(op, PropertyQueryStatus.Success, new SectionShapeCode(country, sectionName, code), raw);
            });
        }
        internal PropertyMutationResult SetStandardProfileDBFolder(string folderPath)
        {
            if (folderPath == null) throw new ArgumentNullException(nameof(folderPath)); if (folderPath.IndexOf('\0') >= 0) throw new ArgumentException("Folder text must not contain NUL.", nameof(folderPath));
            return Run(p =>
            {
                const string op = "SetStandardProfileDBFolder"; var version = Reviewed(op); object raw = p.SetStandardProfileDBFolder(folderPath); int code = Integer(raw, op);
                if (!Equals(version, Snapshot())) throw new InvalidOperationException("Native contract version changed during the folder mutation; effects may remain.");
                int success = version.Major == 24 ? 1 : 0, failure = version.Major == 24 ? 0 : -1;
                if (code != success && code != failure) throw new InvalidOperationException(op + " returned an undocumented status for the acquired native profile.");
                return new PropertyMutationResult(op, code == success ? PropertyMutationStatus.Succeeded : PropertyMutationStatus.NativeFailure, raw, PropertyMutationTargetKind.ProfileDatabaseFolder, folderPath: folderPath, nativeContractVersion: version);
            });
        }
        internal PropertyQueryResult<string> GetStandardProfileDBFolder() => Run(p =>
        {
            const string op = "GetStandardProfileDBFolder"; object raw = p.GetStandardProfileDBFolder();
            if (raw == null || raw == DBNull.Value || raw is string empty && empty.Length == 0) return Result<string>(op, PropertyQueryStatus.EmptyOrUnavailable, diagnostic: "An empty native folder does not distinguish unset from unavailable state.");
            if (!(raw is string path) || path.IndexOf('\0') >= 0) throw new InvalidOperationException(op + " returned malformed path text.");
            return Result(op, PropertyQueryStatus.Success, path);
        });
    }
}
