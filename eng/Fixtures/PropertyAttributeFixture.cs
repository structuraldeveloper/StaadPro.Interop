using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Models.Property;

namespace PackageVerification
{
    // Pure managed receiver for tests and installed-package verification; never activates native STAAD.
    public static class PropertyAttributeFixture
    {
        public sealed class Root
        {
            public object Property { get; set; }
            public object Geometry => null;
            public object GetApplicationVersion(ref object major, ref object update, ref object revision, ref object build)
            { major = 26; update = 0; revision = 0; build = 100; return "26.00.00.100"; }
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
            public int EntityCount = 3, AttributeCount = 2;
            public object R(string op, object fallback, params object[] args)
            {
                Calls.Add(op); Arguments.Add(args); Threads.Add(Thread.CurrentThread.ManagedThreadId); OnCall?.Invoke(op);
                return Responses.TryGetValue(op, out var q) && q.Count > 0 ? q.Dequeue() : fallback;
            }
            private object O(string key, object value) { var output = Output == null ? value : Output(key, value); Buffers[key] = output; return output; }
            public object GetIsotropicMaterialCount() => R(nameof(GetIsotropicMaterialCount), 2);
            public object GetInactiveMemberCount() => R(nameof(GetInactiveMemberCount), 2);
            public object GetFireProofedBeamCount() => R(nameof(GetFireProofedBeamCount), EntityCount);
            public object GetFireProofingSpecCount() => R(nameof(GetFireProofingSpecCount), 2);
            public object GetFireProofedBeamList(ref object ids) { var raw = R(nameof(GetFireProofedBeamList), true); ids = O("FireIds", new[] { 41, 17, 41 }); return raw; }
            public object GetFireProofDataForBeam(int id, ref object type, ref object thickness, ref object density)
            { var raw = R(nameof(GetFireProofDataForBeam), 1, id); type = O("FireType", 1); thickness = O("Thickness", .03); density = O("Density", 12.0); return raw; }
            public object GetFireProofingSpecDetails(int index, ref object type, ref object thickness, ref object density, ref object count)
            { var raw = R(nameof(GetFireProofingSpecDetails), true, index); type = O("FireType", 2); thickness = O("Thickness", .02); density = O("Density", 13.0); count = O("AssignedCount", EntityCount); return raw; }
            public object GetFireProofingSpecAssignedBeamCount(int index) => R(nameof(GetFireProofingSpecAssignedBeamCount), EntityCount, index);
            public object GetFireProofingSpecAssignedBeamList(int index, ref object ids) { var raw = R(nameof(GetFireProofingSpecAssignedBeamList), 1, index); ids = O("FireIds", new[] { 41, 17, 41 }); return raw; }
            public object GetMemberCountByAttribute(string name, ref string value)
            { var raw = R(nameof(GetMemberCountByAttribute), EntityCount, name, value); value = (string)O("AttributeInput", value); return raw; }
            public object GetElementCountByAttribute(string name, ref string value)
            { var raw = R(nameof(GetElementCountByAttribute), EntityCount, name, value); value = (string)O("AttributeInput", value); return raw; }
            public object GetMemberListByAttribute(string name, ref string value, ref object ids)
            { var raw = R(nameof(GetMemberListByAttribute), 0, name, value); value = (string)O("ListAttributeInput", value); ids = O("AttributeIds", new[] { 41, 17, 41 }); return raw; }
            public object GetElementListByAttribute(string name, ref string value, ref object ids)
            { var raw = R(nameof(GetElementListByAttribute), 0, name, value); value = (string)O("ListAttributeInput", value); ids = O("AttributeIds", new[] { 31, 47, 31 }); return raw; }
            public object GetMemberAttributeCount() => R(nameof(GetMemberAttributeCount), AttributeCount);
            public object GetMemberAttributeList(ref object names, ref object values)
            { var raw = R(nameof(GetMemberAttributeList), AttributeCount); names = O("AttributeNames", new[] { "Purpose", " Purpose " }); values = O("AttributeValues", new[] { " REVIEWED ", "" }); return raw; }
            public object GetAssignedAttributeCount(int id) => R(nameof(GetAssignedAttributeCount), 2, id);
            public object CreateMemberAttribute(string name, string value) => R(nameof(CreateMemberAttribute), 0, name, value);
            public object CreateElementAttribute(string name, string value) => R(nameof(CreateElementAttribute), 0, name, value);
            public object DeleteMemberAttribute(string name, string value) => R(nameof(DeleteMemberAttribute), 0, name, value);
            public object DeleteElementAttribute(string name, string value) => R(nameof(DeleteElementAttribute), 0, name, value);
            public object RemoveAttribute(string name, string value, int[] ids) => R(nameof(RemoveAttribute), 0, name, value, ids);
            public object AssignMemberAttribute(string name, string value, int[] ids) => R(nameof(AssignMemberAttribute), 0, name, value, ids);
            public object AssignElementAttribute(string name, string value, int[] ids) => R(nameof(AssignElementAttribute), 0, name, value, ids);
            public object CreateUPTTable(int type) => R(nameof(CreateUPTTable), 7, type);
            public object AddUPTPropertyWIDEFLANGE(int id, string name, double ax, double d, double tw, double wf, double tf, double iz, double iy, double ix, double ay, double az)
                => R(nameof(AddUPTPropertyWIDEFLANGE), 0, id, name, ax, d, tw, wf, tf, iz, iy, ix, ay, az);
            public object AddUPTPropertyWIDEFLANGEUNEQUAL(int id, string name, double[] values) => R(nameof(AddUPTPropertyWIDEFLANGEUNEQUAL), true, id, name, values);
        }
        // Executes all 25 new routes against this managed receiver, independently of guarded XML examples.
        public static void VerifyCalls(IOSProperty property, Native native)
        {
            var attribute = new PropertyAttribute("Purpose", " REVIEWED ");
            var definition = new UptWideFlangeDefinition(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            var unequal = new UptUnequalWideFlangeDefinition(definition, 11, 12);
            if (!property.GetInactiveMemberCount().IsSuccessful || property.GetFireProofedBeamCount().Data != 3 || property.GetFireProofingSpecCount().Data != 2 ||
                !property.GetFireProofedBeamList().Data.SequenceEqual(new[] { 41, 17, 41 }) || property.GetFireProofDataForBeam(17).Data.MemberId != 17 ||
                property.GetFireProofingSpecDetails(1).Data.AssignedMemberCount != 3 || property.GetFireProofingSpecAssignedBeamCount(1).Data != 3 ||
                !property.GetFireProofingSpecAssignedBeamList(1).IsSuccessful || property.GetMemberCountByAttribute(attribute).Data.Count != 3 ||
                property.GetElementCountByAttribute(attribute).Data.Count != 3 || !property.GetMemberListByAttribute(attribute).IsSuccessful ||
                !property.GetElementListByAttribute(attribute).IsSuccessful || property.GetMemberAttributeCount().Data != 2 ||
                property.GetMemberAttributeList().Data[1].Value != "" || property.GetAssignedAttributeCount(17).Data.MemberOrPlateId != 17)
                throw new InvalidOperationException("Installed attribute/fireproofing query verification failed.");
            var writes = new[] { property.CreateMemberAttribute(attribute), property.CreateElementAttribute(attribute), property.DeleteMemberAttribute(attribute),
                property.DeleteElementAttribute(attribute), property.RemoveAttribute(attribute, new[] { 17, 41 }), property.AssignMemberAttribute(attribute, new[] { 17, 41 }),
                property.AssignElementAttribute(attribute, new[] { 31, 47 }), property.CreateUptTable(UserProvidedTableType.WideFlange),
                property.AddUptPropertyWideFlange(7, " EXACT_WF ", definition), property.AddUptPropertyWideFlangeUnequal(7, " EXACT_UNEQUAL ", unequal) };
            if (writes.Any(r => !r.IsSuccessful) || writes[7].CreatedTableId != 7 || writes[8].CreatedPropertyId.HasValue || writes[9].CreatedPropertyId.HasValue)
                throw new InvalidOperationException("Installed attribute/UPT mutation evidence failed.");
            var args = native.Arguments[native.Calls.IndexOf("AddUPTPropertyWIDEFLANGE")];
            if (!args.Skip(2).Cast<double>().SequenceEqual(Enumerable.Range(1, 10).Select(i => (double)i))) throw new InvalidOperationException("Installed UPT scalar order failed.");
            args = native.Arguments[native.Calls.IndexOf("AddUPTPropertyWIDEFLANGEUNEQUAL")];
            if (!((double[])args[2]).SequenceEqual(Enumerable.Range(1, 12).Select(i => (double)i))) throw new InvalidOperationException("Installed UPT twelve-slot order failed.");
        }
    }
}
