using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Models.Property;
using static StaadPro.Interop.Services.Property.PropertyValueReader;
namespace StaadPro.Interop.Services.Property
{
    internal sealed class PropertyCleanupReader
    {
        private readonly PropertyQueryReader queries;
        internal PropertyCleanupReader(PropertyQueryReader queries) { this.queries = queries; }
        private T Run<T>(Func<dynamic,T> body) => queries.WithBinding(p => body(p));
        private static int[] Ids(IEnumerable<int> ids, string name)
        { if (ids == null) throw new ArgumentNullException(name); var copy = ids.Take(100001).ToArray(); if (copy.Length == 0 || copy.Length > 100000 || copy.Any(id => id <= 0)) throw new ArgumentException("Supply 1..100000 positive analytical IDs.", name); return copy; }
        private static PropertyMutationStatus Zero(object raw, string op)
        { int code = Integer(raw, op); if (code != 0 && code != -1) throw new InvalidOperationException(op + " returned an undocumented status."); return code == 0 ? PropertyMutationStatus.Succeeded : PropertyMutationStatus.NativeFailure; }
        internal PropertyMutationResult RemovePropertyFromPlate(int plateId)
        {
            Positive(plateId, nameof(plateId));
            return Run(p => { const string op = "RemovePropertyFromPlate"; object raw = p.RemovePropertyFromPlate(plateId);
                return new PropertyMutationResult(op, (Flag(raw, op) ? PropertyMutationStatus.Succeeded : PropertyMutationStatus.NativeFailure), raw, PropertyMutationTargetKind.Plate, targetIds: new[] { plateId }); });
        }
        internal PropertyMutationResult RemoveMaterialFromPlate(IEnumerable<int> plateIds)
        {
            var copy = Ids(plateIds, nameof(plateIds));
            return Run(p => { const string op = "RemoveMaterialFromPlate"; object raw = p.RemoveMaterialFromPlate((int[])copy.Clone());
                return new PropertyMutationResult(op, (Flag(raw, op) ? PropertyMutationStatus.Succeeded : PropertyMutationStatus.NativeFailure), raw, PropertyMutationTargetKind.Plate, targetIds: copy); });
        }
        internal PropertyMutationResult RemoveMaterialFromSolid(IEnumerable<int> solidIds)
        {
            var copy = Ids(solidIds, nameof(solidIds));
            return Run(p => { const string op = "RemoveMaterialFromSolid"; object raw = p.RemoveMaterialFromSolid((int[])copy.Clone());
                return new PropertyMutationResult(op, (Flag(raw, op) ? PropertyMutationStatus.Succeeded : PropertyMutationStatus.NativeFailure), raw, PropertyMutationTargetKind.Solid, targetIds: copy); });
        }
        internal PropertyMutationResult DeleteMemberReleaseSpec(int memberId, MemberEnd memberEnd)
        {
            Positive(memberId, nameof(memberId)); if (memberEnd != MemberEnd.Start && memberEnd != MemberEnd.End) throw new ArgumentOutOfRangeException(nameof(memberEnd));
            return Run(p => { const string op = "DeleteMemberReleaseSpec"; object raw = p.DeleteMemberReleaseSpec(memberId, (int)memberEnd);
                return new PropertyMutationResult(op, (Flag(raw, op) ? PropertyMutationStatus.Succeeded : PropertyMutationStatus.NativeFailure), raw, PropertyMutationTargetKind.Member, targetIds: new[] { memberId }, memberEnd: memberEnd); });
        }
        internal PropertyMutationResult DeleteAllControlDependentRelations()
        {
            return Run(p => { const string op = "DeleteAllControlDependentRelations"; object raw = p.DeleteAllControlDependentRelations();
                return new PropertyMutationResult(op, Zero(raw, op), raw, PropertyMutationTargetKind.Model, targetIds: null); });
        }
        internal PropertyMutationResult RemoveAllElementOffsetSpec()
        {
            return Run(p => { const string op = "RemoveAllElementOffsetSpec"; object raw = p.RemoveAllElementOffsetSpec();
                return new PropertyMutationResult(op, (Integer(raw, op) == 1 ? PropertyMutationStatus.Succeeded : Integer(raw, op) == 0 ? PropertyMutationStatus.NoSpecification : throw new InvalidOperationException(op + " returned an undocumented removal status.")), raw, PropertyMutationTargetKind.Model, targetIds: null); });
        }
        internal PropertyMutationResult RemovePropertyFromBeam(int memberId)
        {
            Positive(memberId, nameof(memberId));
            return Run(p => { const string op = "RemovePropertyFromBeam"; object raw = p.RemovePropertyFromBeam(memberId);
                return new PropertyMutationResult(op, Zero(raw, op), raw, PropertyMutationTargetKind.Member, targetIds: new[] { memberId }); });
        }
        internal PropertyMutationResult DeleteProperty(int propertyId)
        {
            Positive(propertyId, nameof(propertyId));
            return Run(p => { const string op = "DeleteProperty"; object raw = p.DeleteProperty(propertyId);
                return new PropertyMutationResult(op, (Flag(raw, op) ? PropertyMutationStatus.Succeeded : PropertyMutationStatus.NativeFailure), raw, PropertyMutationTargetKind.PropertyDefinition, targetIds: null, propertyId: propertyId); });
        }
        internal PropertyMutationResult DeleteMemberSpec(int specificationId)
        {
            Positive(specificationId, nameof(specificationId));
            return Run(p => { const string op = "DeleteMemberSpec"; object raw = p.DeleteMemberSpec(specificationId);
                return new PropertyMutationResult(op, (Flag(raw, op) ? PropertyMutationStatus.Succeeded : PropertyMutationStatus.NativeFailure), raw, PropertyMutationTargetKind.SpecificationDefinition, targetIds: null, specificationId: specificationId); });
        }
        internal PropertyMutationResult RemoveMemberReleaseSpecFromBeam(int memberId, MemberEnd memberEnd)
        {
            Positive(memberId, nameof(memberId)); if (memberEnd != MemberEnd.Start && memberEnd != MemberEnd.End) throw new ArgumentOutOfRangeException(nameof(memberEnd));
            return Run(p => { const string op = "RemoveMemberReleaseSpecFromBeam"; object raw = p.RemoveMemberReleaseSpecFromBeam(memberId, (int)memberEnd);
                return new PropertyMutationResult(op, (Flag(raw, op) ? PropertyMutationStatus.Succeeded : PropertyMutationStatus.NativeFailure), raw, PropertyMutationTargetKind.Member, targetIds: new[] { memberId }, memberEnd: memberEnd); });
        }
        internal PropertyMutationResult RemoveMemberOffsetSpecFromBeam(int memberId, MemberEnd memberEnd)
        {
            Positive(memberId, nameof(memberId)); if (memberEnd != MemberEnd.Start && memberEnd != MemberEnd.End) throw new ArgumentOutOfRangeException(nameof(memberEnd));
            return Run(p => { const string op = "RemoveMemberOffsetSpecFromBeam"; object raw = p.RemoveMemberOffsetSpecFromBeam(memberId, (int)memberEnd);
                return new PropertyMutationResult(op, (Flag(raw, op) ? PropertyMutationStatus.Succeeded : PropertyMutationStatus.NativeFailure), raw, PropertyMutationTargetKind.Member, targetIds: new[] { memberId }, memberEnd: memberEnd); });
        }
        internal PropertyMutationResult RemoveMemberTrussSpecFromBeam(int memberId)
        {
            Positive(memberId, nameof(memberId));
            return Run(p => { const string op = "RemoveMemberTrussSpecFromBeam"; object raw = p.RemoveMemberTrussSpecFromBeam(memberId);
                return new PropertyMutationResult(op, Zero(raw, op), raw, PropertyMutationTargetKind.Member, targetIds: new[] { memberId }); });
        }
        internal PropertyMutationResult RemoveMemberInactiveSpecFromBeam(int memberId)
        {
            Positive(memberId, nameof(memberId));
            return Run(p => { const string op = "RemoveMemberInactiveSpecFromBeam"; object raw = p.RemoveMemberInactiveSpecFromBeam(memberId);
                return new PropertyMutationResult(op, Zero(raw, op), raw, PropertyMutationTargetKind.Member, targetIds: new[] { memberId }); });
        }
        internal PropertyMutationResult RemoveMemberTensionSpecFromBeam(int memberId)
        {
            Positive(memberId, nameof(memberId));
            return Run(p => { const string op = "RemoveMemberTensionSpecFromBeam"; object raw = p.RemoveMemberTensionSpecFromBeam(memberId);
                return new PropertyMutationResult(op, Zero(raw, op), raw, PropertyMutationTargetKind.Member, targetIds: new[] { memberId }); });
        }
        internal PropertyMutationResult RemoveMemberCompressionSpecFromBeam(int memberId)
        {
            Positive(memberId, nameof(memberId));
            return Run(p => { const string op = "RemoveMemberCompressionSpecFromBeam"; object raw = p.RemoveMemberCompressionSpecFromBeam(memberId);
                return new PropertyMutationResult(op, Zero(raw, op), raw, PropertyMutationTargetKind.Member, targetIds: new[] { memberId }); });
        }
        internal PropertyMutationResult RemoveMemberIgnoreStiffSpecFromBeam(int memberId)
        {
            Positive(memberId, nameof(memberId));
            return Run(p => { const string op = "RemoveMemberIgnoreStiffSpecFromBeam"; object raw = p.RemoveMemberIgnoreStiffSpecFromBeam(memberId);
                return new PropertyMutationResult(op, Zero(raw, op), raw, PropertyMutationTargetKind.Member, targetIds: new[] { memberId }); });
        }
        internal PropertyMutationResult RemoveMemberCableSpecFromBeam(int memberId, CableDefinitionKind definitionKind)
        {
            Positive(memberId, nameof(memberId)); if (definitionKind != CableDefinitionKind.InitialTension && definitionKind != CableDefinitionKind.UnstressedLength) throw new ArgumentOutOfRangeException(nameof(definitionKind));
            return Run(p => { const string op = "RemoveMemberCableSpecFromBeam"; object raw = p.RemoveMemberCableSpecFromBeam(memberId, (int)definitionKind);
                return new PropertyMutationResult(op, (Flag(raw, op) ? PropertyMutationStatus.Succeeded : PropertyMutationStatus.NativeFailure), raw, PropertyMutationTargetKind.Member, targetIds: new[] { memberId }, cableDefinitionKind: definitionKind); });
        }
        internal PropertyMutationResult RemoveElementPlaneStressSpecFromPlate(int plateId)
        {
            Positive(plateId, nameof(plateId));
            return Run(p => { const string op = "RemoveElementPlaneStressSpecFromPlate"; object raw = p.RemoveElementPlaneStressSpecFromPlate(plateId);
                return new PropertyMutationResult(op, Zero(raw, op), raw, PropertyMutationTargetKind.Plate, targetIds: new[] { plateId }); });
        }
        internal PropertyMutationResult RemoveElementIgnoreInplaneRotnSpecFromPlate(int plateId)
        {
            Positive(plateId, nameof(plateId));
            return Run(p => { const string op = "RemoveElementIgnoreInplaneRotnSpecFromPlate"; object raw = p.RemoveElementIgnoreInplaneRotnSpecFromPlate(plateId);
                return new PropertyMutationResult(op, Zero(raw, op), raw, PropertyMutationTargetKind.Plate, targetIds: new[] { plateId }); });
        }
        internal PropertyMutationResult RemoveElementNodeReleaseSpecFromPlate(int plateId, int nodeId)
        {
            Positive(plateId, nameof(plateId)); Positive(nodeId, nameof(nodeId));
            return Run(p => { const string op = "RemoveElementNodeReleaseSpecFromPlate"; object raw = p.RemoveElementNodeReleaseSpecFromPlate(plateId, nodeId);
                return new PropertyMutationResult(op, Zero(raw, op), raw, PropertyMutationTargetKind.Plate, targetIds: new[] { plateId }, nodeId: nodeId); });
        }
    }
}
