using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PackageVerification
{
    // Shared by tests and a PackageReference-only consumer; never activates or mutates native STAAD.
    public static class PropertyRoadmapFixture
    {
        public sealed class Root
        {
            public object Property { get; set; }
            public object Geometry => null;
            public int NativeMajor = 26;
            public object GetApplicationVersion(ref object major, ref object update, ref object revision, ref object build)
            { major = NativeMajor; update = 0; revision = 0; build = 100; return NativeMajor + ".00.00.100"; }
        }
        public sealed class Native
        {
            public readonly List<string> Calls = new List<string>();
            public readonly List<object[]> Arguments = new List<object[]>();
            public readonly List<int> Threads = new List<int>();
            public readonly Dictionary<string, Queue<object>> Responses = new Dictionary<string, Queue<object>>();
            public readonly Dictionary<string, object> Buffers = new Dictionary<string, object>();
            public Func<string, object, object> Output;
            public Action<string> OnCall;
            public int SolidCount = 3, SectionType = 610, MaterialType = 1, WrittenSectionSlots = 8;
            public object R(string op, object fallback, params object[] args)
            {
                Calls.Add(op); Arguments.Add(args); Threads.Add(Thread.CurrentThread.ManagedThreadId); OnCall?.Invoke(op);
                return Responses.TryGetValue(op, out var q) && q.Count > 0 ? q.Dequeue() : fallback;
            }
            private object O(string op, object value)
            { var result = Output == null ? value : Output(op, value); Buffers[op] = result; return result; }
            public object GetOrthotropic3DMaterialCount() => R(nameof(GetOrthotropic3DMaterialCount), 2);
            public object GetIsotropicMaterialCount() => R(nameof(GetIsotropicMaterialCount), 2);
            public object GetOrthotropic3DMaterialProperties(int id, ref object e, ref object poisson, ref object g, ref object density, ref object alpha, ref object damping)
            {
                var raw = R(nameof(GetOrthotropic3DMaterialProperties), "ORTHO3D", id);
                e = O("E", new[] { 101.0, 102.0, 103.0 }); poisson = O("Poisson", new[] { .1, .2, .3 });
                g = O("G", new[] { 31.0, 32.0, 33.0 }); density = O("Density", new[] { 71.0, 72.0, 73.0 });
                alpha = O("Alpha", new[] { .001, .002, .003 }); damping = O("Damping", new[] { .01, .02, .03 }); return raw;
            }
            public object RemoveMaterialFromBeam(int id) => R(nameof(RemoveMaterialFromBeam), 0, id);
            public object GetSolidMaterialName(int id) => R(nameof(GetSolidMaterialName), "STEEL", id);
            public object GetIsotropicMaterialAssignedSolidCount(string name) => R(nameof(GetIsotropicMaterialAssignedSolidCount), SolidCount, name);
            public object GetIsotropicMaterialAssignedSolidList(string name, ref object ids)
            { var raw = R(nameof(GetIsotropicMaterialAssignedSolidList), true, name); ids = O("SolidIds", new[] { 89, 71, 89 }); return raw; }
            public object SetTypeToIsotropicMaterial(string name, int type) => R(nameof(SetTypeToIsotropicMaterial), true, name, type);
            public object GetTypeForIsotropicMaterial(string name, ref object type)
            { var raw = R(nameof(GetTypeForIsotropicMaterial), true, name); type = O("MaterialType", MaterialType); return raw; }
            public object GetSectionPropertyValues(int profile, ref object w, ref object d, ref object ax, ref object ay, ref object az, ref object ix, ref object iy, ref object iz, ref object tf, ref object tw)
            {
                var raw = R(nameof(GetSectionPropertyValues), 0, profile); w = O("Width", .2); d = O("Depth", .3); ax = O("Ax", .01);
                ay = O("Ay", .002); az = O("Az", .003); ix = O("Ix", .000001); iy = O("Iy", .00001); iz = O("Iz", .0001);
                tf = O("Tf", .012); tw = O("Tw", .006); return raw;
            }
            public object GetBeamSectionPropertyRefNo(int id) => R(nameof(GetBeamSectionPropertyRefNo), 37, id);
            public object GetBeamSectionPropertyValuesEx(int id, ref object type, ref object values)
            {
                var raw = R(nameof(GetBeamSectionPropertyValuesEx), true, id); type = O("SectionType", SectionType);
                var array = (Array)values; for (int i = 0; i < Math.Min(array.Length, WrittenSectionSlots); i++) array.SetValue(i + .5, array.GetLowerBound(0) + i);
                values = O("SectionValues", values); return raw;
            }
            public object GetSectionTableNo(int id) => R(nameof(GetSectionTableNo), 7, id);
            public object GetBeamSectionPropertyTypeNo(int id) => R(nameof(GetBeamSectionPropertyTypeNo), SectionType, id);
            public object GetBeamSectionName(int id) => R(nameof(GetBeamSectionName), "ISMB300", id);
            public object GetBeamSectionDisplayName(int id) => R(nameof(GetBeamSectionDisplayName), "ISMB300 D SP 0.10", id);
            public object GetCountryTableNo(int id) => R(nameof(GetCountryTableNo), 10, id);
            public object CreateAssignProfileProperty(int type) => R(nameof(CreateAssignProfileProperty), 901, type);
            public object AssignElementSpecToPlate(int[] ids, int spec) => R(nameof(AssignElementSpecToPlate), 0, ids, spec);
            public object UpdatePropertiesToDesignSection() => R(nameof(UpdatePropertiesToDesignSection), 1);
            public object CreateIsotropicMaterialPropertiesEx(string name, double e, double p, double g, double density, double alpha, double damping, double fy, double fu, double ry, double rt, double fcu)
                => R(nameof(CreateIsotropicMaterialPropertiesEx), 0, name, e, p, g, density, alpha, damping, fy, fu, ry, rt, fcu);
            public object CreateIsotropicMaterialSteel(string name, double e, double p, double g, double density, double alpha, double damping, double fu, double fy, double rt, double ry, bool physical)
                => R(nameof(CreateIsotropicMaterialSteel), 0, name, e, p, g, density, alpha, damping, fu, fy, rt, ry, physical);
            public object CreateIsotropicMaterialConcrete(string name, double e, double p, double g, double density, double alpha, double damping, double fcu, bool physical)
                => R(nameof(CreateIsotropicMaterialConcrete), 0, name, e, p, g, density, alpha, damping, fcu, physical);
            public object CreateIsotropicMaterialAluminum(string name, double e, double p, double g, double density, double alpha, double damping, bool physical)
                => R(nameof(CreateIsotropicMaterialAluminum), 0, name, e, p, g, density, alpha, damping, physical);
            public object CreateIsotropicMaterialTimber(string name, double e, double p, double g, double density, double alpha, double damping, bool physical)
                => R(nameof(CreateIsotropicMaterialTimber), 0, name, e, p, g, density, alpha, damping, physical);
            public object AssignMaterialToSolid(string name, int[] ids) => R(nameof(AssignMaterialToSolid), 1, name, ids);
        }
        // These checks execute mutations only against this managed receiver in the installed consumer.
        public static void VerifyMutationCalls(StaadPro.Interop.Adapters.Interfaces.IOSProperty property, Native native)
        {
            var m = new StaadPro.Interop.Models.Property.IsotropicMaterialDefinition("EXPLICIT", 101, .2, 37, 71, -.001, .02);
            var extended = new StaadPro.Interop.Models.Property.IsotropicMaterialStrengths(11, 22, 1.1, 1.2, 33);
            var steel = new StaadPro.Interop.Models.Property.SteelMaterialStrengths(22, 11, 1.2, 1.1);
            if (!property.RemoveMaterialFromBeam(17).IsSuccessful || !property.SetTypeToIsotropicMaterial("EXPLICIT", StaadPro.Interop.Enums.Property.IsotropicMaterialType.Steel).IsSuccessful ||
                property.CreateAssignProfileProperty(StaadPro.Interop.Enums.Property.AssignedSectionProfile.Beam).CreatedPropertyId != 901 ||
                !property.AssignElementSpecToPlate(new[] { 31, 47 }, 9).IsSuccessful || !property.UpdatePropertiesToDesignSection().IsSuccessful ||
                !property.CreateIsotropicMaterialPropertiesEx(m, extended).IsSuccessful || !property.CreateIsotropicMaterialSteel(m, steel, true).IsSuccessful ||
                !property.CreateIsotropicMaterialConcrete(m, 33, false).IsSuccessful || !property.CreateIsotropicMaterialAluminum(m, false).IsSuccessful ||
                !property.CreateIsotropicMaterialTimber(m, false).IsSuccessful || !property.AssignMaterialToSolid("EXPLICIT", new[] { 71, 89 }).IsSuccessful)
                throw new InvalidOperationException("Installed Property mutation acknowledgement failed.");
            var args = native.Arguments[native.Calls.IndexOf("CreateIsotropicMaterialSteel")];
            if (!args.Skip(7).Take(4).Cast<double>().SequenceEqual(new[] { 22.0, 11.0, 1.2, 1.1 }) || !(bool)args[11])
                throw new InvalidOperationException("Installed Steel Fu/Fy/Rt/Ry order or physical flag failed.");
        }
    }
}
