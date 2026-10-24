using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Models.Property;
using static StaadPro.Interop.Services.Property.PropertyValueReader;

namespace StaadPro.Interop.Services.Property
{
    // Common reviewed 2024/2025/2026 contracts. Never infer a created ID from a zero-success status.
    internal sealed class PropertyCreationReader
    {
        private readonly PropertyQueryReader queries;
        internal PropertyCreationReader(PropertyQueryReader queries) { this.queries = queries; }
        private T Run<T>(Func<dynamic, T> body) => queries.WithBinding(p => body(p));
        private static void Required(object value, string name) { if (value == null) throw new ArgumentNullException(name); }
        private static void Finite(double value, string name, bool positive = false, bool nonnegative = false)
            => IsotropicMaterialDefinition.Finite(value, name, positive, nonnegative);
        private static void Selector(int value, int minimum, int maximum, string name)
        { if (value < minimum || value > maximum) throw new ArgumentOutOfRangeException(name); }
        private static int[] Flags(DegreeOfFreedomSelection selection, string name)
        {
            Required(selection, name);
            return new[] { selection.Fx, selection.Fy, selection.Fz, selection.Mx, selection.My, selection.Mz }.Select(v => v ? 1 : 0).ToArray();
        }
        private static double[] Factors(PartialReleaseFactors factors)
        { Required(factors, nameof(factors)); return new[] { factors.Fx, factors.Fy, factors.Fz, factors.Mx, factors.My, factors.Mz }; }
        private static PropertyMutationResult Section(string op, object raw, params int[] failures)
        {
            int code = Integer(raw, op);
            if (code > 0) return new PropertyMutationResult(op, PropertyMutationStatus.Created, raw, PropertyMutationTargetKind.SectionDefinition, createdPropertyId: code);
            if (!failures.Contains(code)) throw new InvalidOperationException(op + " returned an undocumented section creation response.");
            return new PropertyMutationResult(op, code == -106 || code == -108 ? PropertyMutationStatus.InvalidInput : PropertyMutationStatus.NativeFailure, raw, PropertyMutationTargetKind.SectionDefinition);
        }
        private static PropertyMutationResult Specification(string op, object raw, int failure, int? plateNodeIndex = null)
        {
            int code = Integer(raw, op);
            if (code > 0) return new PropertyMutationResult(op, PropertyMutationStatus.Created, raw, PropertyMutationTargetKind.SpecificationDefinition, createdSpecificationId: code, plateNodeIndex: plateNodeIndex);
            if (code != failure) throw new InvalidOperationException(op + " returned an undocumented specification creation response; zero is not a created ID.");
            return new PropertyMutationResult(op, PropertyMutationStatus.NativeFailure, raw, PropertyMutationTargetKind.SpecificationDefinition, plateNodeIndex: plateNodeIndex);
        }
        private static PropertyMutationResult Status(string op, object raw, int failure, int[] nodes = null, int? controlNodeId = null)
        {
            int code = Integer(raw, op);
            if (code != 0 && code != -106 && code != failure && !(code == -108 && (op == "CreateMemberPartialReleaseSpec" || op == "CreateElementNodeReleaseSpec")))
                throw new InvalidOperationException(op + " returned an undocumented status; a positive value cannot be assumed to be a specification ID.");
            return new PropertyMutationResult(op, code == 0 ? PropertyMutationStatus.Succeeded : code == -106 || code == -108 ? PropertyMutationStatus.InvalidInput : PropertyMutationStatus.NativeFailure,
                raw, nodes == null ? PropertyMutationTargetKind.SpecificationDefinition : PropertyMutationTargetKind.Node, nodes, controlNodeId: controlNodeId);
        }
        internal PropertyMutationResult CreateWideFlangePropertyFromTable(WideFlangeTableDefinition definition)
        {
            Required(definition, nameof(definition)); var values = definition.NativeValues();
            return Run(p => Section("CreateWideFlangePropertyFromTable", (object)p.CreateWideFlangePropertyFromTable((int)definition.Country, definition.SectionName, (int)definition.Specification, values), -1));
        }
        internal PropertyMutationResult CreateBeamPropertyFromTableEx(Country country, string sectionName, TableSolidShape shape)
        {
            Selector((int)country, 1, 37, nameof(country)); Selector((int)shape, 1, 5, nameof(shape));
            if (sectionName == null) throw new ArgumentNullException(nameof(sectionName));
            if (string.IsNullOrWhiteSpace(sectionName) || sectionName.IndexOf('\0') >= 0)
                throw new ArgumentException("Provide an exact nonblank native table name without NUL characters.", nameof(sectionName));
            return Run(p => Section("CreateBeamPropertyFromTableEx", (object)p.CreateBeamPropertyFromTableEx((int)country, sectionName, (int)shape), 0));
        }
        internal PropertyMutationResult CreatePrismaticRectangleProperty(double depthY, double widthZ)
        {
            Finite(depthY, nameof(depthY), positive: true); Finite(widthZ, nameof(widthZ), positive: true);
            return Run(p => Section("CreatePrismaticRectangleProperty", (object)p.CreatePrismaticRectangleProperty(depthY, widthZ), 0));
        }
        internal PropertyMutationResult CreatePrismaticCircleProperty(double diameter)
        {
            Finite(diameter, nameof(diameter), positive: true);
            return Run(p => Section("CreatePrismaticCircleProperty", (object)p.CreatePrismaticCircleProperty(diameter), 0));
        }
        internal PropertyMutationResult CreatePrismaticTeeProperty(double totalDepth, double flangeWidth, double stemDepth, double stemWidth)
        {
            Finite(totalDepth, nameof(totalDepth), positive: true); Finite(flangeWidth, nameof(flangeWidth), positive: true);
            Finite(stemDepth, nameof(stemDepth), positive: true); Finite(stemWidth, nameof(stemWidth), positive: true);
            if (stemDepth >= totalDepth || stemWidth > flangeWidth) throw new ArgumentException("Stem depth must be less than total depth and stem width must not exceed flange width.");
            return Run(p => Section("CreatePrismaticTeeProperty", (object)p.CreatePrismaticTeeProperty(totalDepth, flangeWidth, stemDepth, stemWidth), 0));
        }
        internal PropertyMutationResult CreatePrismaticTrapezoidalProperty(double depth, double topWidth, double bottomWidth)
        {
            Finite(depth, nameof(depth), positive: true); Finite(topWidth, nameof(topWidth), positive: true); Finite(bottomWidth, nameof(bottomWidth), positive: true);
            return Run(p => Section("CreatePrismaticTrapezoidalProperty", (object)p.CreatePrismaticTrapezoidalProperty(depth, topWidth, bottomWidth), 0));
        }
        internal PropertyMutationResult CreatePrismaticGeneralProperty(GeneralPrismaticSectionDefinition definition)
        {
            Required(definition, nameof(definition));
            var values = new[] { definition.AxialArea, definition.ShearAreaY, definition.ShearAreaZ, definition.TorsionalConstant, definition.InertiaY, definition.InertiaZ,
                definition.DepthY, definition.DepthZ, definition.StemDepthOrTopWidth, definition.StemWidthOrBottomWidth };
            return Run(p => Section("CreatePrismaticGeneralProperty", (object)p.CreatePrismaticGeneralProperty(values), 0, -106, -108));
        }
        internal PropertyMutationResult CreateTaperedTubeProperty(TaperedTubeShape shape, double startDepth, double endDepth, double thickness)
        {
            Selector((int)shape, 0, 5, nameof(shape)); Finite(startDepth, nameof(startDepth), positive: true); Finite(endDepth, nameof(endDepth), positive: true); Finite(thickness, nameof(thickness), positive: true);
            if (thickness >= Math.Min(startDepth, endDepth) / 2) throw new ArgumentOutOfRangeException(nameof(thickness), "Twice the thickness must be smaller than both depths.");
            return Run(p => Section("CreateTaperedTubeProperty", (object)p.CreateTaperedTubeProperty((int)shape, startDepth, endDepth, thickness), 0));
        }
        internal PropertyMutationResult CreateMemberPartialReleaseSpec(MemberEnd memberEnd, DegreeOfFreedomSelection releasedDofs, PartialReleaseFactors factors)
        {
            Selector((int)memberEnd, 0, 1, nameof(memberEnd)); var flags = Flags(releasedDofs, nameof(releasedDofs)); var values = Factors(factors);
            return Run(p => Status("CreateMemberPartialReleaseSpec", (object)p.CreateMemberPartialReleaseSpec((int)memberEnd, flags, values), -6020));
        }
        internal PropertyMutationResult CreateMemberTrussSpec() => Run(p => Specification("CreateMemberTrussSpec", (object)p.CreateMemberTrussSpec(), -6010));
        internal PropertyMutationResult CreateMemberInactiveSpec() => Run(p => Specification("CreateMemberInactiveSpec", (object)p.CreateMemberInactiveSpec(), -6011));
        internal PropertyMutationResult CreateMemberTensionSpec() => Run(p => Specification("CreateMemberTensionSpec", (object)p.CreateMemberTensionSpec(), -6012));
        internal PropertyMutationResult CreateMemberCompressionSpec() => Run(p => Specification("CreateMemberCompressionSpec", (object)p.CreateMemberCompressionSpec(), -6013));
        internal PropertyMutationResult CreateMemberIgnoreStiffSpec() => Run(p => Specification("CreateMemberIgnoreStiffSpec", (object)p.CreateMemberIgnoreStiffSpec(), -6014));
        private static void Cable(CableDefinitionKind definitionKind, double value)
        { Selector((int)definitionKind, 0, 1, nameof(definitionKind)); Finite(value, nameof(value), positive: definitionKind == CableDefinitionKind.UnstressedLength, nonnegative: true); }
        internal PropertyMutationResult CreateMemberCableSpec(CableDefinitionKind definitionKind, double value)
        {
            Cable(definitionKind, value);
            return Run(p => Specification("CreateMemberCableSpec", (object)p.CreateMemberCableSpec((int)definitionKind, value), -6015));
        }
        internal PropertyMutationResult CreateMemberCableSpecEx(CableDefinitionKind definitionKind, double value, CableTensionEnd tensionEnd, double selfWeightFactorX, double selfWeightFactorY, double selfWeightFactorZ)
        {
            Cable(definitionKind, value); Selector((int)tensionEnd, 0, 2, nameof(tensionEnd));
            if (definitionKind == CableDefinitionKind.UnstressedLength && tensionEnd != CableTensionEnd.Unspecified)
                throw new ArgumentException("A tension end applies only to initial tension; use Unspecified for unstressed length.", nameof(tensionEnd));
            Finite(selfWeightFactorX, nameof(selfWeightFactorX)); Finite(selfWeightFactorY, nameof(selfWeightFactorY)); Finite(selfWeightFactorZ, nameof(selfWeightFactorZ));
            // Despite the declaration's varbTension spelling, all three parameter tables specify integral 0=TENSION, 1=LENGTH.
            return Run(p => Specification("CreateMemberCableSpecEx", (object)p.CreateMemberCableSpecEx((int)definitionKind, value, (int)tensionEnd, selfWeightFactorX, selfWeightFactorY, selfWeightFactorZ), -6015));
        }
        internal PropertyMutationResult CreateElementNodeReleaseSpec(int nodeId, DegreeOfFreedomSelection releasedDofs)
        {
            Positive(nodeId, nameof(nodeId)); var flags = Flags(releasedDofs, nameof(releasedDofs));
            return Run(p => Status("CreateElementNodeReleaseSpec", (object)p.CreateElementNodeReleaseSpec(nodeId, flags), -6021, new[] { nodeId }));
        }
        internal PropertyMutationResult CreateElementPlaneStressSpec() => Run(p => Specification("CreateElementPlaneStressSpec", (object)p.CreateElementPlaneStressSpec(), -6018));
        internal PropertyMutationResult CreateElementIgnoreInplaneRotnSpec() => Run(p => Specification("CreateElementIgnoreInplaneRotnSpec", (object)p.CreateElementIgnoreInplaneRotnSpec(), -6019));
        internal PropertyMutationResult AddControlDependentRelation(int controlNodeId, ControlDependencyMode mode, DegreeOfFreedomSelection rigidDofs, IEnumerable<int> dependentNodeIds)
        {
            Positive(controlNodeId, nameof(controlNodeId)); Selector((int)mode, 0, 4, nameof(mode)); var flags = Flags(rigidDofs, nameof(rigidDofs)); Required(dependentNodeIds, nameof(dependentNodeIds));
            var ids = dependentNodeIds.Take(100001).ToArray();
            if (ids.Length == 0 || ids.Length > 100000 || ids.Any(id => id <= 0 || id == controlNodeId))
                throw new ArgumentException("Supply 1..100000 positive dependent node IDs excluding the control node.", nameof(dependentNodeIds));
            var identity = (int[])ids.Clone();
            return Run(p => Status("AddControlDependentRelation", (object)p.AddControlDependentRelation(controlNodeId, (int)mode, flags[0], flags[1], flags[2], flags[3], flags[4], flags[5], ids), -6029, identity, controlNodeId));
        }
        internal PropertyMutationResult CreateElementOffsetSpec(ElementNodeOffsetAxis coordinateSystem, int plateNodeIndex, double offsetX, double offsetY, double offsetZ)
        {
            Selector((int)coordinateSystem, 0, 1, nameof(coordinateSystem)); Selector(plateNodeIndex, 1, 4, nameof(plateNodeIndex));
            Finite(offsetX, nameof(offsetX)); Finite(offsetY, nameof(offsetY)); Finite(offsetZ, nameof(offsetZ));
            return Run(p => Specification("CreateElementOffsetSpec", (object)p.CreateElementOffsetSpec((int)coordinateSystem, plateNodeIndex, offsetX, offsetY, offsetZ), -6016, plateNodeIndex));
        }
        internal PropertyMutationResult CreateElementLocalZOffsetSpec(double node1Offset, double node2Offset, double node3Offset, double node4Offset)
        {
            Finite(node1Offset, nameof(node1Offset)); Finite(node2Offset, nameof(node2Offset)); Finite(node3Offset, nameof(node3Offset)); Finite(node4Offset, nameof(node4Offset));
            return Run(p => Specification("CreateElementLocalZOffsetSpec", (object)p.CreateElementLocalZOffsetSpec(node1Offset, node2Offset, node3Offset, node4Offset), -6016));
        }
        internal PropertyMutationResult CreateMemberFireProofingSpec(FireProofingType fireProofingType, double thickness, double density)
        {
            Selector((int)fireProofingType, 1, 2, nameof(fireProofingType)); Finite(thickness, nameof(thickness), positive: true); Finite(density, nameof(density), nonnegative: true);
            return Run(p =>
            {
                const string op = "CreateMemberFireProofingSpec"; object raw = p.CreateMemberFireProofingSpec((int)fireProofingType, thickness, density); int index = Integer(raw, op);
                if (index < -1) throw new InvalidOperationException(op + " returned an undocumented negative index.");
                return new PropertyMutationResult(op, index == -1 ? PropertyMutationStatus.NativeFailure : PropertyMutationStatus.Created, raw,
                    PropertyMutationTargetKind.SpecificationDefinition, createdSpecificationIndex: index >= 0 ? (int?)index : null);
            });
        }
        internal PropertyQueryResult<PropertyUniqueIdentifier> GetPropertyUniqueID(int propertyId)
        {
            Positive(propertyId, nameof(propertyId));
            return Run(p =>
            {
                const string op = "GetPropertyUniqueID"; object raw = p.GetPropertyUniqueID(propertyId);
                if (raw == null || raw == DBNull.Value || raw is string empty && empty.Length == 0)
                    return new PropertyQueryResult<PropertyUniqueIdentifier>(op, PropertyQueryStatus.EmptyOrUnavailable, diagnostic: "An empty/null unique ID does not establish whether the property or metadata is absent.");
                if (!(raw is string value)) throw new InvalidOperationException(op + " returned a non-string unique ID.");
                return new PropertyQueryResult<PropertyUniqueIdentifier>(op, PropertyQueryStatus.Success, new PropertyUniqueIdentifier(propertyId, value));
            });
        }
        internal PropertyMutationResult SetPropertyUniqueID(int propertyId, string uniqueId)
        {
            Positive(propertyId, nameof(propertyId));
            if (uniqueId == null) throw new ArgumentNullException(nameof(uniqueId));
            if (uniqueId.IndexOf('\0') >= 0) throw new ArgumentException("Unique-ID metadata cannot contain a NUL character.", nameof(uniqueId));
            return Run(p =>
            {
                // The reviewed native setter is void. Do not read or manufacture a status, ID or read-back.
                p.SetPropertyUniqueID(propertyId, uniqueId);
                return new PropertyMutationResult("SetPropertyUniqueID", PropertyMutationStatus.InvokedWithoutAcknowledgement, null, PropertyMutationTargetKind.PropertyDefinition, propertyId: propertyId, uniqueId: uniqueId);
            });
        }
    }
}
