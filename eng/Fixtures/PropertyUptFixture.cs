using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Models.Property;

namespace PackageVerification
{
    // Managed stand-in only. Shared by unit tests and an isolated installed-package consumer.
    public static class PropertyUptFixture
    {
        public sealed class Root
        {
            public object Property { get; set; }
            public object Geometry => null;
            public Version NativeVersion = new Version(26, 0, 0, 340);
            public object GetApplicationVersion(ref object major, ref object update, ref object revision, ref object build)
            { major = NativeVersion.Major; update = NativeVersion.Minor; revision = NativeVersion.Build; build = NativeVersion.Revision; return NativeVersion.ToString(); }
        }
        public sealed class Native
        {
            public readonly List<string> Calls = new List<string>();
            public readonly List<object[]> Arguments = new List<object[]>();
            public readonly List<int> Threads = new List<int>();
            public readonly Dictionary<string, Queue<object>> Responses = new Dictionary<string, Queue<object>>();
            public readonly Dictionary<string, object> Buffers = new Dictionary<string, object>();
            public Action<string> OnCall;
            public Func<string, object, object> Output;
            public int TableCount = 3, SectionCount = 2, TypeCode = 412, Capacity = 24, WrittenFields = 12;
            public string Folder = @"C:\STAAD\Profiles";
            public object R(string op, object fallback, params object[] args)
            { Calls.Add(op); Arguments.Add(args); Threads.Add(Thread.CurrentThread.ManagedThreadId); OnCall?.Invoke(op); return Responses.TryGetValue(op, out var q) && q.Count > 0 ? q.Dequeue() : fallback; }
            private object O(string key, object value) { var output = Output == null ? value : Output(key, value); Buffers[key] = output; return output; }
            public object AddUPTPropertyCHANNEL(int id, string name, double a1, double a2, double a3, double a4, double a5, double a6, double a7, double a8, double a9, double a10, double a11) => R(nameof(AddUPTPropertyCHANNEL), 0, id, name, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11);
            public object AddUPTPropertyANGLE(int id, string name, double a1, double a2, double a3, double a4, double a5, double a6) => R(nameof(AddUPTPropertyANGLE), 0, id, name, a1, a2, a3, a4, a5, a6);
            public object AddUPTPropertyDOUBLEANGLE(int id, string name, double a1, double a2, double a3, double a4, double a5, double a6, double a7, double a8, double a9, double a10) => R(nameof(AddUPTPropertyDOUBLEANGLE), 0, id, name, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10);
            public object AddUPTPropertyTEE(int id, string name, double a1, double a2, double a3, double a4, double a5, double a6, double a7, double a8, double a9, double a10, double a11) => R(nameof(AddUPTPropertyTEE), 0, id, name, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11);
            public object AddUPTPropertyPIPE(int id, string name, double a1, double a2, double a3, double a4) => R(nameof(AddUPTPropertyPIPE), 0, id, name, a1, a2, a3, a4);
            public object AddUPTPropertyTUBE(int id, string name, double a1, double a2, double a3, double a4, double a5, double a6, double a7, double a8, double a9) => R(nameof(AddUPTPropertyTUBE), 0, id, name, a1, a2, a3, a4, a5, a6, a7, a8, a9);
            public object AddUPTPropertyGENERAL(int id, string name, double a1, double a2, double a3, double a4, double a5, double a6, double a7, double a8, double a9, double a10, double a11, double a12, double a13, double a14, double a15, double a16) => R(nameof(AddUPTPropertyGENERAL), 0, id, name, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16);
            public object AddUPTPropertyISECTION(int id, string name, double a1, double a2, double a3, double a4, double a5, double a6, double a7, double a8, double a9, double a10) => R(nameof(AddUPTPropertyISECTION), 0, id, name, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10);
            public object AddUPTPropertyPRISMATIC(int id, string name, double a1, double a2, double a3, double a4, double a5, double a6, double a7, double a8) => R(nameof(AddUPTPropertyPRISMATIC), 0, id, name, a1, a2, a3, a4, a5, a6, a7, a8);
            public object AddUPTPropertyWIDEFLANGECOMPOSITE(int id, string name, double[] values) => R(nameof(AddUPTPropertyWIDEFLANGECOMPOSITE), true, id, name, values);
            public object CreatePropertyFromUPTTable(int id, string name) => R(nameof(CreatePropertyFromUPTTable), 801, id, name);
            public object CreatePropertyFromUserTable(string name, int id) => R(nameof(CreatePropertyFromUserTable), 802, name, id);
            public object CreateUPTTableEx(int id, int type) => R(nameof(CreateUPTTableEx), 13, id, type);
            public object RemoveUPTTable(int id) => R(nameof(RemoveUPTTable), true, id);
            public object RemovePropertyFromUPTTable(int id, string name) => R(nameof(RemovePropertyFromUPTTable), 1, id, name);
            public object GetUserProvidedTableCount() => R(nameof(GetUserProvidedTableCount), TableCount);
            public object GetUserProvidedTableList(ref object ids) { var raw = R(nameof(GetUserProvidedTableList), true); ids = O("TableIds", new[] { 7, 9, 7 }); return raw; }
            public object GetUserProvidedTableSectionCount(int id) => R(nameof(GetUserProvidedTableSectionCount), SectionCount, id);
            public object GetUserProvidedTableSectionType(int id, ref object type)
            { var raw = R(nameof(GetUserProvidedTableSectionType), SectionCount, id); type = O("TableType", TypeCode); return raw; }
            public object GetUserProvidedTableSectionPropertyCount(int id, string name) => R(nameof(GetUserProvidedTableSectionPropertyCount), 19, id, name);
            public object GetCountofSectionPropertyValuesEx() => R(nameof(GetCountofSectionPropertyValuesEx), Capacity);
            public object GetUserProvidedTableSectionProperties(int id, string name, ref object type, ref object values)
            {
                var raw = R(nameof(GetUserProvidedTableSectionProperties), true, id, name); type = O("PropertyType", TypeCode);
                var array = (Array)values; for (int i = 0; i < Math.Min(array.Length, WrittenFields); i++) array.SetValue(i + .5, array.GetLowerBound(0) + i);
                values = O("PropertyValues", values); return raw;
            }
            public object GetUserProvidedTableNo(int index) => R(nameof(GetUserProvidedTableNo), 7, index);
            public object GetShapeCode(int country, string name) => R(nameof(GetShapeCode), 2, country, name);
            public object SetStandardProfileDBFolder(string path) { var raw = R(nameof(SetStandardProfileDBFolder), 0, path); Folder = path; return raw; }
            public object GetStandardProfileDBFolder() => R(nameof(GetStandardProfileDBFolder), Folder);
            public object GetIsotropicMaterialCount() => R(nameof(GetIsotropicMaterialCount), 2);
        }
        public static UptWideFlangeDefinition Primary() => new UptWideFlangeDefinition(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        public static UptCompositeWideFlangeDefinition Composite(int length = 19) => new UptCompositeWideFlangeDefinition(
            new UptUnequalWideFlangeDefinition(Primary(), 11, 12), length >= 16 ? new UptCompositeSlab(13, 14, 15, 16) : null,
            length >= 19 ? new UptCompositeBottomPlate(17, 18, 19) : null);
        // Exercise every new route against this managed receiver; shipped mutation examples remain guarded.
        public static void VerifyCalls(IOSProperty property, Native native)
        {
            var primary = Primary();
            var mutations = new[] {
                property.AddUPTPropertyWIDEFLANGECOMPOSITE(7, "WF_COMPOSITE", Composite()),
                property.AddUPTPropertyCHANNEL(7, "CHANNEL", new UptChannelDefinition(primary, 11)),
                property.AddUPTPropertyANGLE(7, "ANGLE", new UptAngleDefinition(1, 2, 3, 4, 5, 6)),
                property.AddUPTPropertyDOUBLEANGLE(7, "DOUBLE", new UptDoubleAngleDefinition(1, 2, 3, 4, 5, 6, 7, 8, 9, 10)),
                property.AddUPTPropertyTEE(7, "TEE", new UptTeeDefinition(primary, 11)),
                property.AddUPTPropertyPIPE(7, "PIPE", new UptPipeDefinition(2, 1, 3, 4)),
                property.AddUPTPropertyTUBE(7, "TUBE", new UptTubeDefinition(1, 2, 3, 4, 5, 6, 7, 8, 9)),
                property.AddUPTPropertyGENERAL(7, "GENERAL", new UptGeneralDefinition(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16)),
                property.AddUPTPropertyISECTION(7, "ISECTION", new UptISectionDefinition(1, 2, 3, 4, 5, 6, 7, 8, 9, 10)),
                property.AddUPTPropertyPRISMATIC(7, "PRISMATIC", new UptPrismaticDefinition(1, 2, 3, 4, 5, 6, 7, 8)),
                property.CreatePropertyFromUPTTable(7, "WF_COMPOSITE"), property.CreatePropertyFromUserTable("WF_COMPOSITE", 7),
                property.CreateUPTTableEx(7, UserProvidedTableType.WideFlange), property.RemoveUPTTable(13),
                property.RemovePropertyFromUPTTable(7, "WF_COMPOSITE"), property.SetStandardProfileDBFolder(@"C:\STAAD\Profiles") };
            if (mutations.Length != 16 || mutations.Any(r => !r.IsSuccessful)) throw new InvalidOperationException("Installed UPT mutation acknowledgement failed.");
            if (mutations[10].CreatedPropertyId != 801 || mutations[11].CreatedPropertyId != 802 || mutations[12].CreatedTableId != 13 || mutations[12].TableId != 7 || mutations[0].CreatedPropertyId.HasValue)
                throw new InvalidOperationException("Installed UPT requested/created identity failed.");
            if (property.GetUserProvidedTableCount().Data != 3 || !property.GetUserProvidedTableList().Data.SequenceEqual(new[] { 7, 9, 7 }) ||
                property.GetUserProvidedTableSectionCount(7).Data.Count != 2 || property.GetUserProvidedTableSectionType(7).Data.NativeTypeCode != 412 ||
                property.GetUserProvidedTableSectionPropertyCount(7, "WF").Data.NativeCount != 19 || property.GetUserProvidedTableSectionProperties(7, "WF").Data.Values.Count != 12 ||
                property.GetUserProvidedTableNo(1).Data.TableId != 7 || property.GetShapeCode(Country.Indian, "ISMB300").Data.NativeCode != 2 || property.GetStandardProfileDBFolder().Data != @"C:\STAAD\Profiles")
                throw new InvalidOperationException("Installed UPT/catalog query checks failed.");
            var args = native.Arguments[native.Calls.IndexOf("AddUPTPropertyTEE")];
            if (!args.Skip(2).Cast<double>().SequenceEqual(new[] { 1.0, 2, 4, 5, 3, 6, 7, 8, 11, 9, 10 })) throw new InvalidOperationException("Installed tee parameter order failed.");
            args = native.Arguments[native.Calls.IndexOf("AddUPTPropertyWIDEFLANGECOMPOSITE")];
            if (!((double[])args[2]).SequenceEqual(Enumerable.Range(1, 19).Select(i => (double)i))) throw new InvalidOperationException("Installed composite packet order failed.");
        }
    }
}
