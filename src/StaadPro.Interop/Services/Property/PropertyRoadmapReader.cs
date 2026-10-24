using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Property;
using static StaadPro.Interop.Services.Property.PropertyValueReader;

namespace StaadPro.Interop.Services.Property
{
    // All calls, including dependent observations, share the adapter's captured receiver and gate.
    internal sealed class PropertyRoadmapReader
    {
        private readonly PropertyQueryReader queries;
        private readonly OpenStaadWrapper wrapper;
        internal PropertyRoadmapReader(OpenStaadWrapper wrapper, PropertyQueryReader queries) { this.wrapper = wrapper; this.queries = queries; }
        private T Run<T>(Func<dynamic, T> body) => queries.WithBinding(p => body(p));
        private object Call(Func<dynamic, object> body) => queries.WithBinding(p => body(p));
        private static PropertyQueryResult<T> Result<T>(string op, PropertyQueryStatus status, T data = default, object response = null, string diagnostic = null)
        {
            int? code = response == null || response is bool ? (int?)null : Integer(response, op);
            bool? flag = response is bool b ? b : (bool?)null;
            return new PropertyQueryResult<T>(op, status, data, code, flag, diagnostic);
        }
        private static void Profile(AssignedSectionProfile profileType)
        { if ((int)profileType < 0 || (int)profileType > 4) throw new ArgumentOutOfRangeException(nameof(profileType), "Use a defined ASSIGN profile selector, not a property ID."); }
        private static void MaterialType(IsotropicMaterialType materialType)
        { if ((int)materialType < 0 || (int)materialType > 4) throw new ArgumentOutOfRangeException(nameof(materialType)); }
        private static int[] Ids(IEnumerable<int> ids, string name)
        {
            if (ids == null) throw new ArgumentNullException(name);
            // Bounded enumeration also rejects infinite sequences before a native write.
            var copy = ids.Take(100001).ToArray();
            if (copy.Length == 0 || copy.Length > 100000 || copy.Any(id => id <= 0))
                throw new ArgumentException("Supply between 1 and 100000 positive analytical IDs.", name);
            return copy;
        }
        private static void Definition(IsotropicMaterialDefinition material)
        { if (material == null) throw new ArgumentNullException(nameof(material)); }
        private static PropertyMutationStatus CreationStatus(object raw, string op)
        {
            switch (Integer(raw, op))
            {
                case 0: return PropertyMutationStatus.Created;
                case 1: return PropertyMutationStatus.Updated;
                case -1: return PropertyMutationStatus.NativeFailure;
                default: throw new InvalidOperationException(op + " returned an undocumented material creation status.");
            }
        }
        internal PropertyQueryResult<int> GetOrthotropic3DMaterialCount() => Run(p =>
        {
            const string op = "GetOrthotropic3DMaterialCount"; int count = Count((object)p.GetOrthotropic3DMaterialCount(), op);
            return Result(op, PropertyQueryStatus.Success, count, count);
        });
        internal PropertyQueryResult<Orthotropic3DMaterialProperties> GetOrthotropic3DMaterialProperties(int nativeMaterialNumber)
        {
            Nonnegative(nativeMaterialNumber, nameof(nativeMaterialNumber));
            return Run(p =>
            {
                const string op = "GetOrthotropic3DMaterialProperties";
                object e = Buffer(3), poisson = Buffer(3), g = Buffer(3), density = Buffer(3), alpha = Buffer(3), damping = Buffer(3);
                object raw = p.GetOrthotropic3DMaterialProperties(nativeMaterialNumber, ref e, ref poisson, ref g, ref density, ref alpha, ref damping);
                if (raw == null || raw == DBNull.Value || raw is string empty && empty.Length == 0) return Result<Orthotropic3DMaterialProperties>(op, PropertyQueryStatus.NotFound);
                if (!(raw is string name)) throw new InvalidOperationException(op + " returned a non-string material name.");
                return Result(op, PropertyQueryStatus.Success, new Orthotropic3DMaterialProperties(nativeMaterialNumber, name,
                    new[] { FloatingArray(e, 3, op), FloatingArray(poisson, 3, op), FloatingArray(g, 3, op), FloatingArray(density, 3, op), FloatingArray(alpha, 3, op), FloatingArray(damping, 3, op) }));
            });
        }
        internal PropertyMutationResult RemoveMaterialFromBeam(int memberId)
        {
            Positive(memberId, nameof(memberId));
            return Run(p =>
            {
                const string op = "RemoveMaterialFromBeam"; object raw = p.RemoveMaterialFromBeam(memberId); int code = Integer(raw, op);
                if (code != 0 && code != -1) throw new InvalidOperationException(op + " returned an undocumented status.");
                return new PropertyMutationResult(op, code == 0 ? PropertyMutationStatus.Succeeded : PropertyMutationStatus.NativeFailure, raw, PropertyMutationTargetKind.Member, new[] { memberId });
            });
        }
        private PropertyQueryResult<string> Name(int id, string parameter, string op, Func<dynamic, object> read)
        {
            Positive(id, parameter);
            return Run(p =>
            {
                object raw = read(p);
                if (raw == null || raw == DBNull.Value || raw is string empty && empty.Length == 0)
                    return Result<string>(op, PropertyQueryStatus.EmptyOrUnavailable, diagnostic: "The native empty/null name does not distinguish a missing entity from an unavailable assignment.");
                if (!(raw is string name)) throw new InvalidOperationException(op + " returned a non-string name.");
                return Result(op, PropertyQueryStatus.Success, name);
            });
        }
        internal PropertyQueryResult<string> GetSolidMaterialName(int solidId) => Name(solidId, nameof(solidId), "GetSolidMaterialName", p => (object)p.GetSolidMaterialName(solidId));
        internal PropertyQueryResult<int> GetIsotropicMaterialAssignedSolidCount(string materialName)
        {
            MaterialName(materialName);
            return Run(p => { const string op = "GetIsotropicMaterialAssignedSolidCount"; int n = Count((object)p.GetIsotropicMaterialAssignedSolidCount(materialName), op); return Result(op, PropertyQueryStatus.Success, n, n); });
        }
        internal PropertyQueryResult<IReadOnlyList<int>> GetIsotropicMaterialAssignedSolidIds(string materialName, int maximumResults)
        {
            MaterialName(materialName); if (maximumResults < 1 || maximumResults > 100000) throw new ArgumentOutOfRangeException(nameof(maximumResults));
            return Run(p =>
            {
                const string op = "GetIsotropicMaterialAssignedSolidList", countOp = "GetIsotropicMaterialAssignedSolidCount";
                int count = Count(Call(q => (object)q.GetIsotropicMaterialAssignedSolidCount(materialName)), countOp);
                if (count > maximumResults) return Result<IReadOnlyList<int>>(countOp, PropertyQueryStatus.LimitExceeded, response: count);
                var copy = new int[count]; object raw = null;
                if (count > 0)
                {
                    object buffer = copy;
                    raw = Call(q => (object)q.GetIsotropicMaterialAssignedSolidList(materialName, ref buffer));
                    if (!Flag(raw, op)) return Result<IReadOnlyList<int>>(op, PropertyQueryStatus.NativeFailure, response: raw);
                    copy = IntegerArray(buffer, count, op);
                    if (copy.Any(id => id <= 0)) throw new InvalidOperationException(op + " returned invalid or unwritten solid IDs.");
                }
                int after = Count(Call(q => (object)q.GetIsotropicMaterialAssignedSolidCount(materialName)), countOp);
                if (count != after) return Result<IReadOnlyList<int>>(countOp, PropertyQueryStatus.StateChanged, response: after);
                return Result<IReadOnlyList<int>>(count == 0 ? countOp : op, PropertyQueryStatus.Success, Array.AsReadOnly(copy), count == 0 ? (object)count : raw);
            });
        }
        internal PropertyMutationResult SetTypeToIsotropicMaterial(string materialName, IsotropicMaterialType materialType)
        {
            MaterialName(materialName); MaterialType(materialType);
            return Run(p => { const string op = "SetTypeToIsotropicMaterial"; object raw = p.SetTypeToIsotropicMaterial(materialName, (int)materialType);
                return new PropertyMutationResult(op, Flag(raw, op) ? PropertyMutationStatus.Succeeded : PropertyMutationStatus.NativeFailure, raw, PropertyMutationTargetKind.Material, materialName: materialName); });
        }
        internal PropertyQueryResult<IsotropicMaterialType> GetTypeForIsotropicMaterial(string materialName)
        {
            MaterialName(materialName);
            return Run(p =>
            {
                const string op = "GetTypeForIsotropicMaterial"; object type = int.MinValue, raw = p.GetTypeForIsotropicMaterial(materialName, ref type);
                if (!Flag(raw, op)) return Result<IsotropicMaterialType>(op, PropertyQueryStatus.NotFound, response: raw);
                int code = Integer(type, op); if (code < 0 || code > 4) throw new InvalidOperationException(op + " returned an unwritten or undocumented material type.");
                return Result(op, PropertyQueryStatus.Success, (IsotropicMaterialType)code, raw);
            });
        }
        internal PropertyQueryResult<BeamSectionProperties> GetSectionPropertiesForProfile(AssignedSectionProfile profileType)
        {
            Profile(profileType);
            return Run(p =>
            {
                const string op = "GetSectionPropertyValues";
                object width = double.NaN, depth = double.NaN, ax = double.NaN, ay = double.NaN, az = double.NaN,
                    ix = double.NaN, iy = double.NaN, iz = double.NaN, tf = double.NaN, tw = double.NaN;
                object raw = p.GetSectionPropertyValues((int)profileType, ref width, ref depth, ref ax, ref ay, ref az, ref ix, ref iy, ref iz, ref tf, ref tw);
                int code = Integer(raw, op);
                if (code == -3001) return Result<BeamSectionProperties>(op, PropertyQueryStatus.NotFound, response: raw);
                if (code == -6022) return Result<BeamSectionProperties>(op, PropertyQueryStatus.NotAssigned, response: raw);
                if (code != 0) throw new InvalidOperationException(op + " returned an undocumented status.");
                return Result(op, PropertyQueryStatus.Success, new BeamSectionProperties(new[] { width, depth, ax, ay, az, ix, iy, iz, tf, tw }.Select(v => Number(v, op)).ToArray(), true), raw);
            });
        }
        private PropertyQueryResult<int> MemberNumber(int memberId, string op, Func<dynamic, object> read, bool reference = false, bool type = false, bool table = false)
        {
            Positive(memberId, nameof(memberId));
            return Run(p =>
            {
                object raw = read(p); int value = Integer(raw, op);
                if (reference && value == 0) return Result<int>(op, PropertyQueryStatus.EmptyOrUnavailable, response: raw, diagnostic: "Zero means the section reference was not found; missing member and no assignment are not distinguished.");
                if (type && value == 0) return Result<int>(op, PropertyQueryStatus.NativeFailure, response: raw);
                if (!reference && !type)
                {
                    if (value == -3001) return Result<int>(op, PropertyQueryStatus.NotFound, response: raw);
                    if (value == -6022) return Result<int>(op, PropertyQueryStatus.NotAssigned, response: raw);
                    if (table && value == -6004) return Result<int>(op, PropertyQueryStatus.InvalidReference, response: raw, diagnostic: "The section was not found in the profile database.");
                }
                if (value < 0) throw new InvalidOperationException(op + " returned an undocumented negative code.");
                return Result(op, PropertyQueryStatus.Success, value, raw);
            });
        }
        internal PropertyQueryResult<int> GetBeamSectionPropertyRefNo(int memberId) => MemberNumber(memberId, "GetBeamSectionPropertyRefNo", p => (object)p.GetBeamSectionPropertyRefNo(memberId), reference: true);
        internal PropertyQueryResult<int> GetSectionTableNo(int memberId) => MemberNumber(memberId, "GetSectionTableNo", p => (object)p.GetSectionTableNo(memberId), table: true);
        internal PropertyQueryResult<int> GetBeamSectionPropertyTypeNo(int memberId) => MemberNumber(memberId, "GetBeamSectionPropertyTypeNo", p => (object)p.GetBeamSectionPropertyTypeNo(memberId), type: true);
        internal PropertyQueryResult<int> GetCountryTableNo(int memberId) => MemberNumber(memberId, "GetCountryTableNo", p => (object)p.GetCountryTableNo(memberId));
        internal PropertyQueryResult<string> GetBeamSectionName(int memberId) => Name(memberId, nameof(memberId), "GetBeamSectionName", p => (object)p.GetBeamSectionName(memberId));
        internal PropertyQueryResult<string> GetBeamSectionDisplayName(int memberId) => Name(memberId, nameof(memberId), "GetBeamSectionDisplayName", p => (object)p.GetBeamSectionDisplayName(memberId));
        internal PropertyQueryResult<MemberSectionValues> GetBeamSectionPropertyValuesEx(int memberId)
        {
            Positive(memberId, nameof(memberId));
            return Run(p =>
            {
                const string op = "GetBeamSectionPropertyValuesEx";
                int major = wrapper.StaadVersion?.Major ?? 0;
                if (major < 24 || major > 26) throw new NotSupportedException("Member section layouts are reviewed only for native STAAD 24/25/26.");
                object type = int.MinValue, values = Buffer(24);
                object raw = p.GetBeamSectionPropertyValuesEx(memberId, ref type, ref values);
                if (!Flag(raw, op)) return Result<MemberSectionValues>(op, PropertyQueryStatus.NativeFailure, response: raw);
                int typeCode = Integer(type, op); string[] fields = SectionLayouts.Fields(major, typeCode);
                if (fields.Length > 24) throw new InvalidOperationException(op + " layout exceeds its documented buffer.");
                var copy = FloatingArray(values, 24, op, false);
                for (int i = 0; i < fields.Length; i++) Number(copy[i], op + " " + fields[i]);
                if (wrapper.StaadVersion?.Major != major) return Result<MemberSectionValues>(op, PropertyQueryStatus.StateChanged, response: raw);
                return Result(op, PropertyQueryStatus.Success, new MemberSectionValues(memberId, major, typeCode, fields, copy), raw);
            });
        }
        internal PropertyMutationResult CreateAssignProfileProperty(AssignedSectionProfile profileType)
        {
            Profile(profileType);
            return Run(p =>
            {
                const string op = "CreateAssignProfileProperty"; object raw = p.CreateAssignProfileProperty((int)profileType); int code = Integer(raw, op);
                if (code < 0 && code != -6008) throw new InvalidOperationException(op + " returned an undocumented status.");
                return new PropertyMutationResult(op, code > 0 ? PropertyMutationStatus.Created : code == 0 ? PropertyMutationStatus.NativeFailure : PropertyMutationStatus.InvalidInput,
                    raw, PropertyMutationTargetKind.SectionDefinition, profileType: profileType, createdPropertyId: code > 0 ? (int?)code : null);
            });
        }
        internal PropertyMutationResult AssignElementSpecToPlate(IEnumerable<int> plateIds, int specificationId)
        {
            var copy = Ids(plateIds, nameof(plateIds)); Positive(specificationId, nameof(specificationId));
            return Run(p =>
            {
                const string op = "AssignElementSpecToPlate"; var identity = (int[])copy.Clone();
                object raw = p.AssignElementSpecToPlate(copy, specificationId); int code = Integer(raw, op);
                if (code != 0 && code != -106 && code != -6017) throw new InvalidOperationException(op + " returned an undocumented status.");
                return new PropertyMutationResult(op, code == 0 ? PropertyMutationStatus.Succeeded : code == -106 ? PropertyMutationStatus.InvalidInput : PropertyMutationStatus.NativeFailure,
                    raw, PropertyMutationTargetKind.Plate, identity, specificationId: specificationId);
            });
        }
        internal PropertyMutationResult UpdatePropertiesToDesignSection() => Run(p =>
        {
            const string op = "UpdatePropertiesToDesignSection"; object raw = p.UpdatePropertiesToDesignSection();
            return new PropertyMutationResult(op, Flag(raw, op) ? PropertyMutationStatus.Succeeded : PropertyMutationStatus.NativeFailure, raw, PropertyMutationTargetKind.Model);
        });
        internal PropertyMutationResult CreateIsotropicMaterialPropertiesEx(IsotropicMaterialDefinition material, IsotropicMaterialStrengths strengths)
        {
            Definition(material); if (strengths == null) throw new ArgumentNullException(nameof(strengths));
            return Run(p =>
            {
                const string op = "CreateIsotropicMaterialPropertiesEx";
                object raw = p.CreateIsotropicMaterialPropertiesEx(material.Name, material.ElasticModulus, material.PoissonRatio, material.ShearModulus,
                    material.WeightDensity, material.ThermalExpansion, material.DampingRatio, strengths.YieldStrength, strengths.UltimateStrength, strengths.YieldRatio, strengths.UltimateRatio, strengths.CompressiveStrength);
                return new PropertyMutationResult(op, CreationStatus(raw, op), raw, PropertyMutationTargetKind.Material, materialName: material.Name);
            });
        }
        internal PropertyMutationResult CreateIsotropicMaterialSteel(IsotropicMaterialDefinition material, SteelMaterialStrengths strengths, bool isPhysical)
        {
            Definition(material); if (strengths == null) throw new ArgumentNullException(nameof(strengths));
            return Run(p =>
            {
                const string op = "CreateIsotropicMaterialSteel";
                // The native Steel order differs from PropertiesEx: Fu, Fy, Rt, Ry.
                object raw = p.CreateIsotropicMaterialSteel(material.Name, material.ElasticModulus, material.PoissonRatio, material.ShearModulus,
                    material.WeightDensity, material.ThermalExpansion, material.DampingRatio, strengths.UltimateStrength, strengths.YieldStrength, strengths.UltimateRatio, strengths.YieldRatio, isPhysical);
                return new PropertyMutationResult(op, CreationStatus(raw, op), raw, PropertyMutationTargetKind.Material, materialName: material.Name);
            });
        }
        internal PropertyMutationResult CreateIsotropicMaterialConcrete(IsotropicMaterialDefinition material, double compressiveStrength, bool isPhysical)
        {
            Definition(material); IsotropicMaterialDefinition.Finite(compressiveStrength, nameof(compressiveStrength));
            return Run(p =>
            {
                const string op = "CreateIsotropicMaterialConcrete";
                object raw = p.CreateIsotropicMaterialConcrete(material.Name, material.ElasticModulus, material.PoissonRatio, material.ShearModulus,
                    material.WeightDensity, material.ThermalExpansion, material.DampingRatio, compressiveStrength, isPhysical);
                return new PropertyMutationResult(op, CreationStatus(raw, op), raw, PropertyMutationTargetKind.Material, materialName: material.Name);
            });
        }
        internal PropertyMutationResult CreateIsotropicMaterialAluminum(IsotropicMaterialDefinition material, bool isPhysical)
        {
            Definition(material);
            return Run(p => { const string op = "CreateIsotropicMaterialAluminum";
                object raw = p.CreateIsotropicMaterialAluminum(material.Name, material.ElasticModulus, material.PoissonRatio, material.ShearModulus, material.WeightDensity, material.ThermalExpansion, material.DampingRatio, isPhysical);
                return new PropertyMutationResult(op, CreationStatus(raw, op), raw, PropertyMutationTargetKind.Material, materialName: material.Name); });
        }
        internal PropertyMutationResult CreateIsotropicMaterialTimber(IsotropicMaterialDefinition material, bool isPhysical)
        {
            Definition(material);
            return Run(p => { const string op = "CreateIsotropicMaterialTimber";
                object raw = p.CreateIsotropicMaterialTimber(material.Name, material.ElasticModulus, material.PoissonRatio, material.ShearModulus, material.WeightDensity, material.ThermalExpansion, material.DampingRatio, isPhysical);
                return new PropertyMutationResult(op, CreationStatus(raw, op), raw, PropertyMutationTargetKind.Material, materialName: material.Name); });
        }
        internal PropertyMutationResult AssignMaterialToSolid(string materialName, IEnumerable<int> solidIds)
        {
            MaterialName(materialName); var copy = Ids(solidIds, nameof(solidIds));
            return Run(p => { const string op = "AssignMaterialToSolid"; var identity = (int[])copy.Clone(); object raw = p.AssignMaterialToSolid(materialName, copy);
                return new PropertyMutationResult(op, Flag(raw, op) ? PropertyMutationStatus.Succeeded : PropertyMutationStatus.NativeFailure, raw, PropertyMutationTargetKind.Solid, identity, materialName); });
        }
    }
}
