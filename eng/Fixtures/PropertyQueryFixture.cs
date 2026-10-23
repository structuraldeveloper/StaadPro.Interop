using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PackageVerification
{
    // Managed documentation fixture only; never instantiates STAAD or modifies a real model.
    public static class PropertyQueryFixture
    {
        public sealed class Root
        {
            public object Property { get; set; }
            public object Geometry => null;
            public int Major = 26;
            public object GetApplicationVersion(ref object major, ref object update, ref object revision, ref object build)
            { major = Major; update = 0; revision = 0; build = 340; return Major + ".00.00.340"; }
        }
        public sealed class Native
        {
            public readonly List<string> Calls = new List<string>();
            public readonly List<object[]> Arguments = new List<object[]>();
            public readonly List<int> Threads = new List<int>();
            public readonly Dictionary<string, Queue<object>> Responses = new Dictionary<string, Queue<object>>();
            public readonly Dictionary<string, Exception> Errors = new Dictionary<string, Exception>();
            public readonly Dictionary<string, object> Buffers = new Dictionary<string, object>();
            public Action<string> OnCall;
            public Func<string, object, object> ReplaceBuffer, ReplaceScalar;
            public int Count = 2;
            public int[] ReleaseModes = { 0, 1, -1, -3, -2, -2 };
            public double[] ReleaseValues = { double.NaN, double.NaN, 12.5, double.NaN, 0.25, 0.5 };
            public double[] DirectionalFactors = { double.NaN, 0.8, 0.9 };
            public object CommonFactor = 0.75, Assignment = 2.0, OffsetAxis = 2;
            public object R(string name, object fallback, params object[] args)
            {
                Calls.Add(name); Arguments.Add(args); Threads.Add(Thread.CurrentThread.ManagedThreadId);
                OnCall?.Invoke(name); if (Errors.TryGetValue(name, out var error)) throw error;
                return Responses.TryGetValue(name, out var queue) && queue.Count > 0 ? queue.Dequeue() : fallback;
            }
            private object S(string role, object value) => ReplaceScalar == null ? value : ReplaceScalar(role, value);
            private void Fill(string role, ref object output, Array values)
            {
                var a = (Array)output; if (a.GetLowerBound(0) != 0 || a.Rank != 1) throw new InvalidOperationException("Expected a zero-based one-dimensional input buffer.");
                if (a.GetType().GetElementType() == typeof(double)) { if (!a.Cast<double>().All(double.IsNaN)) throw new InvalidOperationException("Floating outputs must start unwritten."); }
                else { if (!a.Cast<int>().All(x => x == 0 || x == int.MinValue)) throw new InvalidOperationException("Integer output sentinel mismatch."); }
                if (ReplaceBuffer != null) output = ReplaceBuffer(role, output);
                else for (int i = 0; i < a.Length; i++) a.SetValue(values.GetValue(i), i);
                Buffers[role] = output;
            }
            public object GetBetaAngle(int id) => R(nameof(GetBetaAngle), -12.5, id);
            public object GetElementOffsetSpecCount() => R(nameof(GetElementOffsetSpecCount), Count);
            public object GetMemberReleaseSpec(int id, int end, ref object modes, ref object springs)
            { var result = R(nameof(GetMemberReleaseSpec), 1, id, end); Fill("modes", ref modes, ReleaseModes); Fill("springs", ref springs, ReleaseValues); return result; }
            public object GetMemberReleaseSpecEx(int id, int end, ref object modes, ref object springs, ref object common, ref object directional)
            { var result = R(nameof(GetMemberReleaseSpecEx), 1, id, end); Fill("modes", ref modes, ReleaseModes); Fill("springs", ref springs, ReleaseValues); common = S("common", CommonFactor); Fill("directional", ref directional, DirectionalFactors); return result; }
            public object GetMemberLocalOffSet(int id, int end, ref object x, ref object y, ref object z)
            { var result = R(nameof(GetMemberLocalOffSet), 0, id, end); Offset(ref x, ref y, ref z); return result; }
            public object GetMemberGlobalOffSet(int id, int end, ref object x, ref object y, ref object z)
            { var result = R(nameof(GetMemberGlobalOffSet), 0, id, end); Offset(ref x, ref y, ref z); return result; }
            private void Offset(ref object x, ref object y, ref object z) { x = S("x", 1.0); y = S("y", -2.0); z = S("z", 3.0); }
            public object GetElementLocalOffset(int id, int corner, ref object x, ref object y, ref object z)
            { var result = R(nameof(GetElementLocalOffset), 0, id, corner); Offset(ref x, ref y, ref z); return result; }
            public object GetElementGlobalOffset(int id, int corner, ref object x, ref object y, ref object z)
            { var result = R(nameof(GetElementGlobalOffset), 0, id, corner); Offset(ref x, ref y, ref z); return result; }
            public object GetElementOffsetSpec(int id, int corner, ref object axis, ref object x, ref object y, ref object z)
            { var result = R(nameof(GetElementOffsetSpec), 1, id, corner); axis = S("axis", OffsetAxis); Offset(ref x, ref y, ref z); return result; }
            public object GetPlateSectionPropertyRefNo(int id) => R(nameof(GetPlateSectionPropertyRefNo), 37, id);
            public object GetBeamProperty(int id, ref object w, ref object d, ref object ax, ref object ay, ref object az, ref object ix, ref object iy, ref object iz)
            { var result = R(nameof(GetBeamProperty), 1, id); Section(ref w, ref d, ref ax, ref ay, ref az, ref ix, ref iy, ref iz); return result; }
            public object GetBeamPropertyAll(int id, ref object w, ref object d, ref object ax, ref object ay, ref object az, ref object ix, ref object iy, ref object iz, ref object flange, ref object web)
            { var result = R(nameof(GetBeamPropertyAll), 1, id); Section(ref w, ref d, ref ax, ref ay, ref az, ref ix, ref iy, ref iz); flange = S("flange", 9.0); web = S("web", 10.0); return result; }
            private void Section(ref object w, ref object d, ref object ax, ref object ay, ref object az, ref object ix, ref object iy, ref object iz)
            { w = S("width", 1.0); d = S("depth", 2.0); ax = S("area", 3.0); ay = S("shearY", 4.0); az = S("shearZ", 5.0); ix = S("ix", 6.0); iy = S("iy", 7.0); iz = S("iz", 8.0); }
            private void Basic(ref object e, ref object poisson, ref object density, ref object alpha, ref object damping)
            { e = S("e", 1.0); poisson = S("poisson", 2.0); density = S("density", 3.0); alpha = S("alpha", 4.0); damping = S("damping", 5.0); }
            private void Strength(ref object fy, ref object fu, ref object ry, ref object rt, ref object fcu)
            { fy = S("fy", 7.0); fu = S("fu", 8.0); ry = S("ry", 9.0); rt = S("rt", 10.0); fcu = S("fcu", 11.0); }
            public object GetBeamConstants(int id, ref object e, ref object poisson, ref object density, ref object alpha, ref object damping)
            { var result = R(nameof(GetBeamConstants), true, id); Basic(ref e, ref poisson, ref density, ref alpha, ref damping); return result; }
            public object GetBeamMaterialName(int id) => R(nameof(GetBeamMaterialName), " STEEL ", id);
            public object GetElementMaterialName(int id) => R(nameof(GetElementMaterialName), " CONCRETE ", id);
            public object GetIsotropicMaterialCount() => R(nameof(GetIsotropicMaterialCount), Count);
            public object GetOrthotropic2DMaterialCount() => R(nameof(GetOrthotropic2DMaterialCount), Count);
            public object GetIsotropicMaterialAssignedBeamCount(string name) => R(nameof(GetIsotropicMaterialAssignedBeamCount), Count, name);
            public object GetIsotropicMaterialAssignedBeamList(string name, ref object ids)
            { var result = R(nameof(GetIsotropicMaterialAssignedBeamList), 0, name); Fill("ids", ref ids, Enumerable.Range(0,Count).Select(i=>i==0?41:17).ToArray()); return result; }
            public object GetMaterialProperty(string name, ref object e, ref object poisson, ref object density, ref object alpha, ref object damping)
            { var result = R(nameof(GetMaterialProperty), 0, name); Basic(ref e, ref poisson, ref density, ref alpha, ref damping); return result; }
            public object GetMaterialPropertyEx(string name, ref object e, ref object poisson, ref object density, ref object alpha, ref object damping, ref object fy, ref object fu, ref object ry, ref object rt, ref object fcu)
            { var result = R(nameof(GetMaterialPropertyEx), true, name); Basic(ref e, ref poisson, ref density, ref alpha, ref damping); Strength(ref fy, ref fu, ref ry, ref rt, ref fcu); return result; }
            public object GetIsotropicMaterialProperties(int index, ref object e, ref object poisson, ref object g, ref object density, ref object alpha, ref object damping)
            { var result = R(nameof(GetIsotropicMaterialProperties), "NATIVE STEEL", index); Basic(ref e, ref poisson, ref density, ref alpha, ref damping); g=S("g",6.0); return result; }
            public object GetIsotropicMaterialPropertiesEx(int index, ref object e, ref object poisson, ref object g, ref object density, ref object alpha, ref object damping, ref object fy, ref object fu, ref object ry, ref object rt, ref object fcu)
            { var result = R(nameof(GetIsotropicMaterialPropertiesEx), "NATIVE STEEL", index); Basic(ref e, ref poisson, ref density, ref alpha, ref damping); g=S("g",6.0); Strength(ref fy, ref fu, ref ry, ref rt, ref fcu); return result; }
            public object GetIsotropicMaterialPropertiesAssigned(int number, ref object e, ref object poisson, ref object g, ref object density, ref object alpha, ref object damping, ref object assigned)
            { var result = R(nameof(GetIsotropicMaterialPropertiesAssigned), "NATIVE STEEL", number); Basic(ref e, ref poisson, ref density, ref alpha, ref damping); g=S("g",6.0); assigned=S("assigned",Assignment); return result; }
            public object GetOrthotropic2DMaterialProperties(int number, ref object e, ref object poisson, ref object g, ref object density, ref object alpha, ref object damping)
            { var result = R(nameof(GetOrthotropic2DMaterialProperties), "NATIVE ORTHO", number); Fill("e",ref e,new[]{1.0,2.0});Fill("poisson",ref poisson,new[]{3.0,4.0});Fill("g",ref g,new[]{5.0,6.0,7.0});Fill("density",ref density,new[]{8.0,9.0});Fill("alpha",ref alpha,new[]{10.0,11.0});Fill("damping",ref damping,new[]{12.0,13.0});return result; }
        }
    }
}
