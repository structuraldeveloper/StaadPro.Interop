using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Media.Media3D;
using Microsoft.CSharp.RuntimeBinder;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Property;
using StaadPro.Interop.Services.Wrapper;
using static StaadPro.Interop.Services.Property.PropertyValueReader;

namespace StaadPro.Interop.Services.Property
{
    // Shared installed 2024-2026 contracts. Type clarifications do not justify duplicate year services.
    // Future changed layouts/capabilities must select policies from wrapper.StaadVersion, never the host version.
    internal sealed class PropertyQueryReader
    {
        private readonly OpenStaadWrapper wrapper;
        private readonly object native;
        private readonly object gate = new object();
        internal PropertyQueryReader(OpenStaadWrapper wrapper, object native) { this.wrapper = wrapper; this.native = native; }
        private dynamic Binding()
        {
            if (wrapper.RawOpenStaad == null || !ReferenceEquals((object)wrapper.RawProperty, native))
                throw new InvalidOperationException("The captured Property binding is disposed or replaced. Reacquire wrapper.Property.");
            return native;
        }
        private T Run<T>(Func<T> body) { lock (gate) { Binding(); var data = body(); Binding(); return data; } }
        // Compatibility ports share the observation reader's binding lifetime checks and lock.
        internal T WithBinding<T>(Func<object, T> body) => Run(() => body(native));
        private static PropertyQueryResult<T> Result<T>(string op, PropertyQueryStatus status, T data = default, object response = null, string diagnostic = null)
        {
            bool? flag = response is bool b ? b : (bool?)null;
            int? code = response is sbyte || response is byte || response is short || response is ushort || response is int || response is uint || response is long || response is ulong ? Integer(response, op) : (int?)null;
            return new PropertyQueryResult<T>(op, status, data, code, flag, diagnostic);
        }
        private static void End(MemberEnd memberEnd)
        { if (memberEnd != MemberEnd.Start && memberEnd != MemberEnd.End) throw new ArgumentOutOfRangeException(nameof(memberEnd)); }
        private static void Corner(int cornerIndex)
        { if (cornerIndex < 1 || cornerIndex > 4) throw new ArgumentOutOfRangeException(nameof(cornerIndex)); }
        internal PropertyQueryResult<double> GetBetaAngle(int memberId)
        {
            Positive(memberId, nameof(memberId));
            return Run(() =>
            {
                const string op = "GetBetaAngle"; object raw = Binding().GetBetaAngle(memberId); double angle = Number(raw, op, true);
                return angle == -3001 ? Result<double>(op, PropertyQueryStatus.NotFound, response: raw)
                    : Result(op, PropertyQueryStatus.Success, angle, raw);
            });
        }
        internal PropertyQueryResult<int> GetElementOffsetSpecificationCount() => Run(() =>
        {
            const string op = "GetElementOffsetSpecCount"; int count = Count((object)Binding().GetElementOffsetSpecCount(), op);
            return Result(op, PropertyQueryStatus.Success, count, count);
        });
        private static PropertyQueryResult<T> OffsetFailure<T>(string op, int code)
        {
            PropertyQueryStatus status;
            switch (code)
            {
                case -3001 when op.StartsWith("GetMember", StringComparison.Ordinal):
                case -4001 when op.StartsWith("GetElement", StringComparison.Ordinal):
                    status = PropertyQueryStatus.NotFound; break;
                case -6026 when op.StartsWith("GetMember", StringComparison.Ordinal):
                    status = PropertyQueryStatus.InvalidReference; break;
                case -6027 when op != "GetElementOffsetSpec":
                    status = PropertyQueryStatus.NoSpecification; break;
                default: throw new InvalidOperationException(op + " returned an undocumented offset status: " + code);
            }
            return Result<T>(op, status, response: code);
        }
        internal PropertyQueryResult<Vector3D> GetMemberLocalOffset(int memberId, MemberEnd memberEnd) => MemberOffset(memberId, memberEnd, true);
        internal PropertyQueryResult<Vector3D> GetMemberGlobalOffset(int memberId, MemberEnd memberEnd) => MemberOffset(memberId, memberEnd, false);
        private PropertyQueryResult<Vector3D> MemberOffset(int memberId, MemberEnd memberEnd, bool local)
        {
            Positive(memberId, nameof(memberId)); End(memberEnd);
            return Run(() =>
            {
                string op = local ? "GetMemberLocalOffSet" : "GetMemberGlobalOffSet"; object x = double.NaN, y = double.NaN, z = double.NaN;
                int code = Integer(local ? (object)Binding().GetMemberLocalOffSet(memberId, (int)memberEnd, ref x, ref y, ref z)
                    : (object)Binding().GetMemberGlobalOffSet(memberId, (int)memberEnd, ref x, ref y, ref z), op);
                return code != 0 ? OffsetFailure<Vector3D>(op, code) : Result(op, PropertyQueryStatus.Success, new Vector3D(Number(x, op), Number(y, op), Number(z, op)), code);
            });
        }
        internal PropertyQueryResult<Vector3D> GetPlateLocalOffset(int plateId, int cornerIndex) => PlateOffset(plateId, cornerIndex, true);
        internal PropertyQueryResult<Vector3D> GetPlateGlobalOffset(int plateId, int cornerIndex) => PlateOffset(plateId, cornerIndex, false);
        private PropertyQueryResult<Vector3D> PlateOffset(int plateId, int cornerIndex, bool local)
        {
            Positive(plateId, nameof(plateId)); Corner(cornerIndex);
            return Run(() =>
            {
                string op = local ? "GetElementLocalOffset" : "GetElementGlobalOffset"; object x = double.NaN, y = double.NaN, z = double.NaN;
                int code = Integer(local ? (object)Binding().GetElementLocalOffset(plateId, cornerIndex, ref x, ref y, ref z)
                    : (object)Binding().GetElementGlobalOffset(plateId, cornerIndex, ref x, ref y, ref z), op);
                return code != 0 ? OffsetFailure<Vector3D>(op, code) : Result(op, PropertyQueryStatus.Success, new Vector3D(Number(x, op), Number(y, op), Number(z, op)), code);
            });
        }
        internal PropertyQueryResult<ElementOffsetSpecification> GetPlateOffsetSpecification(int plateId, int cornerIndex)
        {
            Positive(plateId, nameof(plateId)); Corner(cornerIndex);
            return Run(() =>
            {
                const string op = "GetElementOffsetSpec"; object axis = int.MinValue, x = double.NaN, y = double.NaN, z = double.NaN;
                int code = Integer((object)Binding().GetElementOffsetSpec(plateId, cornerIndex, ref axis, ref x, ref y, ref z), op);
                if (code == 0) return Result<ElementOffsetSpecification>(op, PropertyQueryStatus.NoSpecification, response: code);
                if (code != 1) return OffsetFailure<ElementOffsetSpecification>(op, code);
                int selector = Integer(axis, op + " coordinate selector");
                if (!Enum.IsDefined(typeof(ElementNodeOffsetAxis), selector)) throw new InvalidOperationException(op + " returned an unknown/unwritten coordinate selector.");
                return Result(op, PropertyQueryStatus.Success, new ElementOffsetSpecification((ElementNodeOffsetAxis)selector, new Vector3D(Number(x, op), Number(y, op), Number(z, op))), code);
            });
        }
        internal PropertyQueryResult<int> GetPlateSectionPropertyId(int plateId)
        {
            Positive(plateId, nameof(plateId));
            return Run(() =>
            {
                const string op = "GetPlateSectionPropertyRefNo";
                object raw;
                // Documented in all three help sets but absent from the inspected 2025/2026 CLR
                // interfaces. Try only this named member, and never substitute a different API.
                try { raw = Binding().GetPlateSectionPropertyRefNo(plateId); }
                catch (RuntimeBinderException) { return Result<int>(op, PropertyQueryStatus.Unsupported, diagnostic: "The acquired Property receiver does not expose this documented member."); }
                catch (COMException error) when (BoundStaadModelServices.IsMissingMember(error))
                { return Result<int>(op, PropertyQueryStatus.Unsupported, diagnostic: "The acquired Property receiver does not expose this documented member."); }
                int code = Integer(raw, op);
                if (code == -4001) return Result<int>(op, PropertyQueryStatus.NotFound, response: raw);
                if (code == -6022) return Result<int>(op, PropertyQueryStatus.NotAssigned, response: raw);
                if (code <= 0) throw new InvalidOperationException(op + " returned an undocumented property reference.");
                return Result(op, PropertyQueryStatus.Success, code, raw);
            });
        }
        internal PropertyQueryResult<MemberReleaseSpecification> GetMemberReleaseSpecification(int memberId, MemberEnd memberEnd) => Release(memberId, memberEnd, false);
        internal PropertyQueryResult<MemberReleaseSpecification> GetMemberReleaseSpecificationExtended(int memberId, MemberEnd memberEnd) => Release(memberId, memberEnd, true);
        private PropertyQueryResult<MemberReleaseSpecification> Release(int memberId, MemberEnd memberEnd, bool extended)
        {
            Positive(memberId, nameof(memberId)); End(memberEnd);
            return Run(() =>
            {
                string op = extended ? "GetMemberReleaseSpecEx" : "GetMemberReleaseSpec";
                object modes = Enumerable.Repeat(int.MinValue, 6).ToArray(), parameters = Buffer(6), common = double.NaN, factors = Buffer(3);
                object raw = extended ? (object)Binding().GetMemberReleaseSpecEx(memberId, (int)memberEnd, ref modes, ref parameters, ref common, ref factors)
                    : (object)Binding().GetMemberReleaseSpec(memberId, (int)memberEnd, ref modes, ref parameters);
                if (!Flag(raw, op)) return Result<MemberReleaseSpecification>(op, PropertyQueryStatus.NativeFailure, response: raw);
                int[] codes = IntegerArray(modes, 6, op); double[] slots = FloatingArray(parameters, 6, op, false);
                var data = new DegreeOfFreedomRelease[6];
                for (int i = 0; i < 6; i++)
                {
                    if (!Enum.IsDefined(typeof(MemberDofReleaseMode), codes[i])) throw new InvalidOperationException(op + " returned an unknown/unwritten release mode.");
                    var mode = (MemberDofReleaseMode)codes[i]; double? parameter = null;
                    if (mode == MemberDofReleaseMode.Spring || (!extended && mode == MemberDofReleaseMode.DirectionalPartialMoment)) parameter = Number(slots[i], op);
                    else if (mode == MemberDofReleaseMode.DirectionalPartialMoment && !double.IsNaN(slots[i])) parameter = Number(slots[i], op);
                    data[i] = new DegreeOfFreedomRelease(mode, parameter);
                }
                double? commonFactor = extended && codes.Contains(-3) ? Number(common, op + " common MP factor") : (double?)null;
                PartialMomentReleaseFactors directional = null;
                if (extended && codes.Skip(3).Contains(-2))
                {
                    double[] values = FloatingArray(factors, 3, op, false);
                    directional = new PartialMomentReleaseFactors(codes[3] == -2 ? Number(values[0], op + " MPX") : (double?)null,
                        codes[4] == -2 ? Number(values[1], op + " MPY") : (double?)null,
                        codes[5] == -2 ? Number(values[2], op + " MPZ") : (double?)null);
                }
                return Result(op, PropertyQueryStatus.Success, new MemberReleaseSpecification(data, extended, commonFactor, directional), raw);
            });
        }
        internal PropertyQueryResult<BeamSectionProperties> GetMemberSectionProperties(int memberId) => Section(memberId, false);
        internal PropertyQueryResult<BeamSectionProperties> GetMemberSectionPropertiesExtended(int memberId) => Section(memberId, true);
        private PropertyQueryResult<BeamSectionProperties> Section(int memberId, bool extended)
        {
            Positive(memberId, nameof(memberId));
            return Run(() =>
            {
                string op = extended ? "GetBeamPropertyAll" : "GetBeamProperty";
                object width = double.NaN, depth = double.NaN, ax = double.NaN, ay = double.NaN, az = double.NaN,
                    ix = double.NaN, iy = double.NaN, iz = double.NaN, flange = double.NaN, web = double.NaN;
                object raw = extended ? (object)Binding().GetBeamPropertyAll(memberId, ref width, ref depth, ref ax, ref ay, ref az, ref ix, ref iy, ref iz, ref flange, ref web)
                    : (object)Binding().GetBeamProperty(memberId, ref width, ref depth, ref ax, ref ay, ref az, ref ix, ref iy, ref iz);
                if (!Flag(raw, op)) return Result<BeamSectionProperties>(op, PropertyQueryStatus.NativeFailure, response: raw,
                    diagnostic: "Native zero does not distinguish a missing member, an unassigned property and general failure.");
                var values = new[] { Number(width, op), Number(depth, op), Number(ax, op), Number(ay, op), Number(az, op), Number(ix, op), Number(iy, op), Number(iz, op),
                    extended ? Number(flange, op) : 0.0, extended ? Number(web, op) : 0.0 };
                return Result(op, PropertyQueryStatus.Success, new BeamSectionProperties(values, extended), raw);
            });
        }
        internal PropertyQueryResult<MaterialConstants> GetMemberMaterialConstants(int memberId)
        {
            Positive(memberId, nameof(memberId));
            return Run(() =>
            {
                const string op = "GetBeamConstants"; object e = double.NaN, poisson = double.NaN, density = double.NaN, alpha = double.NaN, damping = double.NaN;
                object raw = Binding().GetBeamConstants(memberId, ref e, ref poisson, ref density, ref alpha, ref damping);
                return Flag(raw, op, true) ? Result(op, PropertyQueryStatus.Success, Constants(op, e, poisson, density, alpha, damping), raw)
                    : Result<MaterialConstants>(op, PropertyQueryStatus.NativeFailure, response: raw);
            });
        }
        private static MaterialConstants Constants(string op, object e, object poisson, object density, object alpha, object damping, object shear = null) =>
            new MaterialConstants(new[] { Number(e, op), Number(poisson, op), Number(density, op), Number(alpha, op), Number(damping, op) }, shear == null ? (double?)null : Number(shear, op));
        private static MaterialStrengthProperties Strengths(string op, object yield, object tensile, object yieldRatio, object tensileRatio, object compressive) =>
            new MaterialStrengthProperties(new[] { Number(yield, op), Number(tensile, op), Number(yieldRatio, op), Number(tensileRatio, op), Number(compressive, op) });
        internal PropertyQueryResult<string> GetBeamMaterialName(int memberId) => EntityMaterialName(memberId, false);
        internal PropertyQueryResult<string> GetElementMaterialName(int plateId) => EntityMaterialName(plateId, true);
        private PropertyQueryResult<string> EntityMaterialName(int entityId, bool plate)
        {
            Positive(entityId, plate ? "plateId" : "memberId");
            return Run(() =>
            {
                string op = plate ? "GetElementMaterialName" : "GetBeamMaterialName";
                object raw = plate ? Binding().GetElementMaterialName(entityId) : Binding().GetBeamMaterialName(entityId);
                if (raw == null || raw == DBNull.Value || raw is string empty && empty.Length == 0)
                    return Result<string>(op, PropertyQueryStatus.EmptyOrUnavailable, diagnostic: "Native empty/null text cannot distinguish no assignment from an unavailable entity.");
                if (!(raw is string name)) throw new InvalidOperationException(op + " returned non-string material data.");
                return Result(op, PropertyQueryStatus.Success, name);
            });
        }
        internal PropertyQueryResult<int> GetIsotropicMaterialCount() => Run(() =>
        {
            const string op = "GetIsotropicMaterialCount"; int count = Count((object)Binding().GetIsotropicMaterialCount(), op);
            return Result(op, PropertyQueryStatus.Success, count, count);
        });
        internal PropertyQueryResult<int> GetOrthotropic2DMaterialCount() => Run(() =>
        {
            const string op = "GetOrthotropic2DMaterialCount"; int count = Count((object)Binding().GetOrthotropic2DMaterialCount(), op);
            return Result(op, PropertyQueryStatus.Success, count, count);
        });
        private int AssignedCount(string materialName) => Integer((object)Binding().GetIsotropicMaterialAssignedBeamCount(materialName), "GetIsotropicMaterialAssignedBeamCount");
        internal PropertyQueryResult<int> GetIsotropicMaterialAssignedMemberCount(string materialName)
        {
            MaterialName(materialName);
            return Run(() =>
            {
                const string op = "GetIsotropicMaterialAssignedBeamCount"; int count = AssignedCount(materialName);
                if (count == -6023) return Result<int>(op, PropertyQueryStatus.NotFound, response: count);
                if (count < 0) throw new InvalidOperationException(op + " returned an undocumented count.");
                return Result(op, PropertyQueryStatus.Success, count, count,
                    diagnostic: "Interpreted as a count following the native function purpose and main-source usage; the help return table labels only zero as OK.");
            });
        }
        internal PropertyQueryResult<IEnumerable<int>> GetIsotropicMaterialAssignedMemberIds(string materialName, int maximumResults)
        {
            MaterialName(materialName); Positive(maximumResults, nameof(maximumResults));
            return Run(() =>
            {
                const string op = "GetIsotropicMaterialAssignedBeamList", countOp = "GetIsotropicMaterialAssignedBeamCount";
                int count = AssignedCount(materialName);
                if (count == -6023) return Result<IEnumerable<int>>(countOp, PropertyQueryStatus.NotFound, response: count);
                if (count < 0) throw new InvalidOperationException(countOp + " returned an undocumented count.");
                if (count > maximumResults) return Result<IEnumerable<int>>(countOp, PropertyQueryStatus.LimitExceeded, response: count);
                int[] values = new int[count]; int code = 0;
                if (count > 0)
                {
                    object ids = values; code = Integer((object)Binding().GetIsotropicMaterialAssignedBeamList(materialName, ref ids), op);
                    if (code == -6023) return Result<IEnumerable<int>>(op, PropertyQueryStatus.NotFound, response: code);
                    if (code != 0) throw new InvalidOperationException(op + " returned an undocumented status.");
                    values = IntegerArray(ids, count, op);
                    if (values.Any(id => id <= 0)) throw new InvalidOperationException(op + " returned invalid/unwritten member IDs.");
                }
                int after = AssignedCount(materialName);
                if (after != count) return Result<IEnumerable<int>>(countOp, PropertyQueryStatus.StateChanged, response: after);
                return Result<IEnumerable<int>>(count == 0 ? countOp : op, PropertyQueryStatus.Success, Array.AsReadOnly(values), code);
            });
        }
        internal PropertyQueryResult<MaterialProperties> GetMaterialProperty(string materialName) => NamedMaterial(materialName, false);
        internal PropertyQueryResult<MaterialProperties> GetExtendedMaterialProperty(string materialName) => NamedMaterial(materialName, true);
        private PropertyQueryResult<MaterialProperties> NamedMaterial(string materialName, bool extended)
        {
            MaterialName(materialName);
            return Run(() =>
            {
                string op = extended ? "GetMaterialPropertyEx" : "GetMaterialProperty";
                object e = double.NaN, poisson = double.NaN, density = double.NaN, alpha = double.NaN, damping = double.NaN,
                    fy = double.NaN, fu = double.NaN, ry = double.NaN, rt = double.NaN, fcu = double.NaN;
                object raw = extended ? (object)Binding().GetMaterialPropertyEx(materialName, ref e, ref poisson, ref density, ref alpha, ref damping, ref fy, ref fu, ref ry, ref rt, ref fcu)
                    : (object)Binding().GetMaterialProperty(materialName, ref e, ref poisson, ref density, ref alpha, ref damping);
                if (extended && !Flag(raw, op, true)) return Result<MaterialProperties>(op, PropertyQueryStatus.NativeFailure, response: raw);
                if (!extended)
                {
                    int code = Integer(raw, op);
                    if (code == -6023) return Result<MaterialProperties>(op, PropertyQueryStatus.NotFound, response: raw);
                    if (code != 0) throw new InvalidOperationException(op + " returned an undocumented status.");
                }
                return Result(op, PropertyQueryStatus.Success, new MaterialProperties(materialName, Constants(op, e, poisson, density, alpha, damping),
                    extended ? Strengths(op, fy, fu, ry, rt, fcu) : null), raw);
            });
        }
        internal PropertyQueryResult<MaterialProperties> GetIsotropicMaterialProperty(int materialIndex) => IndexedMaterial(materialIndex, false);
        internal PropertyQueryResult<MaterialProperties> GetExtendedIsotropicMaterialProperty(int materialIndex) => IndexedMaterial(materialIndex, true);
        private PropertyQueryResult<MaterialProperties> IndexedMaterial(int materialIndex, bool extended)
        {
            Nonnegative(materialIndex, nameof(materialIndex));
            return Run(() =>
            {
                string op = extended ? "GetIsotropicMaterialPropertiesEx" : "GetIsotropicMaterialProperties";
                int count = Count((object)Binding().GetIsotropicMaterialCount(), "GetIsotropicMaterialCount");
                if (materialIndex >= count) return Result<MaterialProperties>("GetIsotropicMaterialCount", PropertyQueryStatus.NotFound, response: count);
                object e = double.NaN, poisson = double.NaN, g = double.NaN, density = double.NaN, alpha = double.NaN, damping = double.NaN,
                    fy = double.NaN, fu = double.NaN, ry = double.NaN, rt = double.NaN, fcu = double.NaN;
                object raw = extended ? (object)Binding().GetIsotropicMaterialPropertiesEx(materialIndex, ref e, ref poisson, ref g, ref density, ref alpha, ref damping, ref fy, ref fu, ref ry, ref rt, ref fcu)
                    : (object)Binding().GetIsotropicMaterialProperties(materialIndex, ref e, ref poisson, ref g, ref density, ref alpha, ref damping);
                if (raw == null || raw == DBNull.Value || raw is string empty && empty.Length == 0)
                    return Result<MaterialProperties>(op, PropertyQueryStatus.NotFound, diagnostic: "No material name was returned for the requested native index.");
                if (!(raw is string name)) throw new InvalidOperationException(op + " returned non-string material data.");
                var material = new MaterialProperties(name, Constants(op, e, poisson, density, alpha, damping, g), extended ? Strengths(op, fy, fu, ry, rt, fcu) : null);
                int after = Count((object)Binding().GetIsotropicMaterialCount(), "GetIsotropicMaterialCount");
                return after == count ? Result(op, PropertyQueryStatus.Success, material)
                    : Result<MaterialProperties>("GetIsotropicMaterialCount", PropertyQueryStatus.StateChanged, response: after);
            });
        }
        internal PropertyQueryResult<MaterialProperties> GetIsotropicMaterialPropertiesAssigned(int nativeMaterialNumber)
        {
            Nonnegative(nativeMaterialNumber, nameof(nativeMaterialNumber));
            return Run(() =>
            {
                const string op = "GetIsotropicMaterialPropertiesAssigned";
                object e = double.NaN, poisson = double.NaN, g = double.NaN, density = double.NaN, alpha = double.NaN, damping = double.NaN, assigned = double.NaN;
                object raw = Binding().GetIsotropicMaterialPropertiesAssigned(nativeMaterialNumber, ref e, ref poisson, ref g, ref density, ref alpha, ref damping, ref assigned);
                if (raw == null || raw == DBNull.Value || raw is string empty && empty.Length == 0) return Result<MaterialProperties>(op, PropertyQueryStatus.NotFound);
                if (!(raw is string name)) throw new InvalidOperationException(op + " returned non-string material data.");
                double observed = assigned is double || assigned is float ? Number(assigned, op) : Integer(assigned, op);
                if (observed != 1 && observed != 2) throw new InvalidOperationException(op + " returned an invalid/unwritten assignment flag.");
                return Result(op, PropertyQueryStatus.Success, new MaterialProperties(name, Constants(op, e, poisson, density, alpha, damping, g),
                    assignment: (MaterialAssignmentState)(int)observed));
            });
        }
        internal PropertyQueryResult<Orthotropic2DMaterialProperties> GetOrthotropic2DMaterialProperties(int nativeMaterialNumber)
        {
            Nonnegative(nativeMaterialNumber, nameof(nativeMaterialNumber));
            return Run(() =>
            {
                const string op = "GetOrthotropic2DMaterialProperties";
                object e = Buffer(2), poisson = Buffer(2), g = Buffer(3), density = Buffer(2), alpha = Buffer(2), damping = Buffer(2);
                object raw = Binding().GetOrthotropic2DMaterialProperties(nativeMaterialNumber, ref e, ref poisson, ref g, ref density, ref alpha, ref damping);
                if (raw == null || raw == DBNull.Value || raw is string empty && empty.Length == 0) return Result<Orthotropic2DMaterialProperties>(op, PropertyQueryStatus.NotFound);
                if (!(raw is string name)) throw new InvalidOperationException(op + " returned non-string material data.");
                return Result(op, PropertyQueryStatus.Success, new Orthotropic2DMaterialProperties(name, new[] { FloatingArray(e, 2, op), FloatingArray(poisson, 2, op), FloatingArray(g, 3, op),
                    FloatingArray(density, 2, op), FloatingArray(alpha, 2, op), FloatingArray(damping, 2, op) }));
            });
        }
    }
}
