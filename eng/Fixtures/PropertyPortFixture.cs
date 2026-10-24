using System;
using System.Collections.Generic;
using System.Linq;

namespace PackageVerification
{
    // Shared by unit tests and the isolated NuGet consumer. No native STAAD activation.
    public static class PropertyPortFixture
    {
        public sealed class Root
        {
            public object Property { get; set; }
            public object Geometry { get; } = new GeometryNative();
            public object GetApplicationVersion(ref object major, ref object update, ref object revision, ref object build)
            { major = 26; update = 0; revision = 0; build = 340; return "26.00.00.340"; }
        }
        public sealed class GeometryNative
        {
            public int GetMemberCount() => 2;
            public int GetPlateCount() => 1;
            public void GetBeamList(ref object ids) { ids = new[] { 17, 41 }; }
            public void GetPlateList(ref object ids) { ids = new[] { 17 }; }
            public void GetMemberIncidence(int id, ref object start, ref object end) { start = 1; end = 2; }
            public void GetPlateIncidence(int id, ref object a, ref object b, ref object c, ref object d) { a = 1; b = 2; c = 3; d = 4; }
            public void GetNodeCoordinates(int id, ref object x, ref object y, ref object z) { x = (double)id; y = 0.0; z = id == 3 ? 1.0 : 0.0; }
        }
        public sealed class Native
        {
            public readonly List<string> Calls = new List<string>();
            public readonly List<object[]> Arguments = new List<object[]>();
            public readonly Dictionary<string, Queue<object>> Responses = new Dictionary<string, Queue<object>>();
            public readonly Dictionary<string, Exception> Errors = new Dictionary<string, Exception>();
            public readonly Dictionary<string, object> Buffers = new Dictionary<string, object>();
            public int[] Sections = { 37, 38 }, Thicknesses = { 45, 46 };
            public readonly Dictionary<int, int[]> Beams = new Dictionary<int, int[]> { [37] = new[] { 17, 41 }, [38] = new int[0] };
            public readonly Dictionary<int, int[]> Plates = new Dictionary<int, int[]> { [45] = new[] { 17 }, [46] = new int[0] };
            public Action<string> OnCall;
            public Func<string, object, object> Output;
            public object R(string op, object fallback, params object[] args)
            {
                Calls.Add(op); Arguments.Add(args); OnCall?.Invoke(op);
                if (Errors.TryGetValue(op, out var error)) throw error;
                return Responses.TryGetValue(op, out var queue) && queue.Count > 0 ? queue.Dequeue() : fallback;
            }
            private object O(string op, object value) => Output == null ? value : Output(op, value);
            private void Fill(string op, ref object buffer, Array values)
            {
                var a = (Array)buffer;
                for (int i = 0; i < Math.Min(a.Length, values.Length); i++) a.SetValue(values.GetValue(i), i);
                buffer = O(op, buffer); Buffers[op] = buffer;
            }
            public object GetSectionPropertyCount() => R(nameof(GetSectionPropertyCount), Sections.Length);
            public object GetSectionPropertyList(ref object ids)
            { object status = R(nameof(GetSectionPropertyList), true); Fill(nameof(GetSectionPropertyList), ref ids, Sections); return status; }
            public object GetThicknessPropertyCount() => R(nameof(GetThicknessPropertyCount), Thicknesses.Length);
            public object GetThicknessPropertyList(ref object ids)
            { object status = R(nameof(GetThicknessPropertyList), true); Fill(nameof(GetThicknessPropertyList), ref ids, Thicknesses); return status; }
            public object GetSectionPropertyAssignedBeamCount(int id) => R(nameof(GetSectionPropertyAssignedBeamCount), Beams[id].Length, id);
            public object GetSectionPropertyAssignedBeamList(int id, ref object ids)
            { object status = R(nameof(GetSectionPropertyAssignedBeamList), 0, id); Fill(nameof(GetSectionPropertyAssignedBeamList), ref ids, Beams[id]); return status; }
            public object GetThicknessPropertyAssignedPlateCount(int id) => R(nameof(GetThicknessPropertyAssignedPlateCount), Plates[id].Length, id);
            public object GetThicknessPropertyAssignedPlateList(int id, ref object ids)
            { object status = R(nameof(GetThicknessPropertyAssignedPlateList), true, id); Fill(nameof(GetThicknessPropertyAssignedPlateList), ref ids, Plates[id]); return status; }
            public object GetSectionPropertyName(int id, ref object name)
            { object status = R(nameof(GetSectionPropertyName), 0, id); name = O(nameof(GetSectionPropertyName), " Section Ω " ); return status; }
            public object GetPlateMaterialName(int id) => R(nameof(GetPlateMaterialName), " CONCRETE Ω ", id);
            public object GetCountofSectionPropertyValuesEx() => R(nameof(GetCountofSectionPropertyValuesEx), 12);
            public object GetSectionPropertyValuesEx(int id, ref object type, ref object values)
            { object status = R(nameof(GetSectionPropertyValuesEx), 1, id); type = O("sectionType", 612); Fill(nameof(GetSectionPropertyValuesEx), ref values, Enumerable.Range(1, 11).Select(i => (double)i).ToArray()); return status; }
            public object GetPlateThickness(int id, ref object values)
            { object status = R(nameof(GetPlateThickness), 0, id); Fill(nameof(GetPlateThickness), ref values, new[] { 0.1, 0.2, 0.3, 0.4 }); return status; }
            public object GetMemberSpecCode(int id, ref object code)
            { object status = R(nameof(GetMemberSpecCode), true, id); code = O(nameof(GetMemberSpecCode), 2); return status; }
            public object AssignBeamProperty(object ids, int propertyId) => R(nameof(AssignBeamProperty), 0, ids, propertyId);
            public object AssignMaterialToMember(string name, object ids) => R(nameof(AssignMaterialToMember), true, name, ids);
            public object SetMaterialName(string name) => R(nameof(SetMaterialName), 1, name);
            public object CreateMemberOffsetSpec(int location, int axis, double x, double y, double z) => R(nameof(CreateMemberOffsetSpec), location == 0 ? 71 : 72, location, axis, x, y, z);
            public object AssignMemberSpecToBeam(object ids, int spec) => R(nameof(AssignMemberSpecToBeam), 0, ids, spec);
            public object GetBetaAngle(int id) => R(nameof(GetBetaAngle), -12.5, id);
            public object DeleteMaterial(string name) => R(nameof(DeleteMaterial), true, name);
            public object CreateBeamPropertyFromTable(int country, string name, int type, double first, double second)
                => R(nameof(CreateBeamPropertyFromTable), 91, country, name, type, first, second);
            public object CreateBeamPropertyFromTableWithCoverPlates(int country, string name, int type, double[] values)
                => R(nameof(CreateBeamPropertyFromTableWithCoverPlates), 92, country, name, type, values);
            public object CreateTaperedIProperty(double[] values) => R(nameof(CreateTaperedIProperty), 93, values);
            public object AssignBetaAngle(int id, double angle) => R(nameof(AssignBetaAngle), 1, id, angle);
            public object GetIsotropicMaterialAssignedBeamCount(string name) => R(nameof(GetIsotropicMaterialAssignedBeamCount), 2, name);
            public object GetIsotropicMaterialAssignedPlateCount(string name) => R(nameof(GetIsotropicMaterialAssignedPlateCount), 1, name);
            public object GetIsotropicMaterialAssignedBeamList(string name, ref object ids)
            { var status = R(nameof(GetIsotropicMaterialAssignedBeamList), 0, name); Fill(nameof(GetIsotropicMaterialAssignedBeamList), ref ids, new[] { 41, 17 }); return status; }
            public object GetIsotropicMaterialAssignedPlateList(string name, ref object ids)
            { var status = R(nameof(GetIsotropicMaterialAssignedPlateList), true, name); Fill(nameof(GetIsotropicMaterialAssignedPlateList), ref ids, new[] { 17 }); return status; }
            public object CreatePlateThicknessProperty(double[] values) => R(nameof(CreatePlateThicknessProperty), 94, values);
            public object AssignPlateThickness(int[] ids, int propertyId) => R(nameof(AssignPlateThickness), 0, ids, propertyId);
            public object GetThicknessPropertyValues(int id, ref object values)
            { var status = R(nameof(GetThicknessPropertyValues), 4, id); Fill(nameof(GetThicknessPropertyValues), ref values, new[] { 0.1, 0.2, 0.3, 0.0 }); return status; }
            public object GetMemberGlobalOffSet(int id, int location, ref object x, ref object y, ref object z)
            {
                var status = R(nameof(GetMemberGlobalOffSet), 0, id, location);
                x = O("offsetX", -0.1); y = O("offsetY", 0.0); z = O("offsetZ", 0.3); return status;
            }
            public object GetMemberLocalOffSet(int id, int location, ref object x, ref object y, ref object z)
            {
                var status = R(nameof(GetMemberLocalOffSet), 0, id, location);
                x = O("offsetX", -0.2); y = O("offsetY", 0.0); z = O("offsetZ", 0.4); return status;
            }
        }
    }
}
