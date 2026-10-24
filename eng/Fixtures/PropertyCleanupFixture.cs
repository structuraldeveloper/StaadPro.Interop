using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Property;
namespace PackageVerification
{
    public static class PropertyCleanupFixture
    {
        public sealed class Root
        {
            public object Property { get; set; }
            public object Geometry => null;
            public Version NativeVersion = new Version(26,0,0,340);
            public object GetApplicationVersion(ref object major, ref object update, ref object revision, ref object build)
            { if (NativeVersion == null) { major = update = revision = build = null; return null; } major=NativeVersion.Major;update=NativeVersion.Minor;revision=NativeVersion.Build;build=NativeVersion.Revision;return NativeVersion.ToString(); }
        }
        public class Native
        {
            public readonly List<string> Calls = new List<string>();
            public readonly List<object[]> Arguments = new List<object[]>();
            public readonly List<int> Threads = new List<int>();
            public readonly Dictionary<string,Queue<object>> Responses = new Dictionary<string,Queue<object>>();
            public Action<string> OnCall;
            protected object R(string op, object fallback, params object[] args)
            { Calls.Add(op); Arguments.Add(args); Threads.Add(Thread.CurrentThread.ManagedThreadId);OnCall?.Invoke(op);return Responses.TryGetValue(op,out var q)&&q.Count>0?q.Dequeue():fallback; }
            public object RemovePropertyFromPlate(int plateId) => R("RemovePropertyFromPlate", true, plateId);
            public object RemoveMaterialFromPlate(int[] plateIds) => R("RemoveMaterialFromPlate", true, plateIds);
            public object RemoveMaterialFromSolid(int[] solidIds) => R("RemoveMaterialFromSolid", true, solidIds);
            public object DeleteMemberReleaseSpec(int memberId, int memberEnd) => R("DeleteMemberReleaseSpec", true, memberId, memberEnd);
            public object DeleteAllControlDependentRelations() => R("DeleteAllControlDependentRelations", 0);
            public object RemoveAllElementOffsetSpec() => R("RemoveAllElementOffsetSpec", 1);
            public object RemovePropertyFromBeam(int memberId) => R("RemovePropertyFromBeam", 0, memberId);
            public object DeleteProperty(int propertyId) => R("DeleteProperty", true, propertyId);
            public object DeleteMemberSpec(int specificationId) => R("DeleteMemberSpec", true, specificationId);
            public object RemoveMemberReleaseSpecFromBeam(int memberId, int memberEnd) => R("RemoveMemberReleaseSpecFromBeam", true, memberId, memberEnd);
            public object RemoveMemberOffsetSpecFromBeam(int memberId, int memberEnd) => R("RemoveMemberOffsetSpecFromBeam", true, memberId, memberEnd);
            public object RemoveMemberTrussSpecFromBeam(int memberId) => R("RemoveMemberTrussSpecFromBeam", 0, memberId);
            public object RemoveMemberInactiveSpecFromBeam(int memberId) => R("RemoveMemberInactiveSpecFromBeam", 0, memberId);
            public object RemoveMemberTensionSpecFromBeam(int memberId) => R("RemoveMemberTensionSpecFromBeam", 0, memberId);
            public object RemoveMemberCompressionSpecFromBeam(int memberId) => R("RemoveMemberCompressionSpecFromBeam", 0, memberId);
            public object RemoveMemberIgnoreStiffSpecFromBeam(int memberId) => R("RemoveMemberIgnoreStiffSpecFromBeam", 0, memberId);
            public object RemoveMemberCableSpecFromBeam(int memberId, int definitionKind) => R("RemoveMemberCableSpecFromBeam", true, memberId, definitionKind);
            public object RemoveElementPlaneStressSpecFromPlate(int plateId) => R("RemoveElementPlaneStressSpecFromPlate", 0, plateId);
            public object RemoveElementIgnoreInplaneRotnSpecFromPlate(int plateId) => R("RemoveElementIgnoreInplaneRotnSpecFromPlate", 0, plateId);
            public object RemoveElementNodeReleaseSpecFromPlate(int plateId, int nodeId) => R("RemoveElementNodeReleaseSpecFromPlate", 0, plateId, nodeId);
            public object GetDefaultStandardProfileDBFolder() => R("GetDefaultStandardProfileDBFolder", @"C:\STAAD\DefaultProfiles");
            public object IsStandardDatabaseSection(object propertyId) => R("IsStandardDatabaseSection",true,propertyId);
            public object GetStandardSectionDatabaseName(object propertyId) => R("GetStandardSectionDatabaseName","EXACT_DB",propertyId);
            public object GetStandardSectionTableName(object propertyId) => R("GetStandardSectionTableName","EXACT_TABLE",propertyId);
            public object GetStandardSectionName(object propertyId) => R("GetStandardSectionName","EXACT_SECTION",propertyId);
        }
        public static void VerifyCalls(IOSProperty property, Native native)
        {
            var mutations = new[] {
                property.RemovePropertyFromPlate(101),
                property.RemoveMaterialFromPlate(new[] { 101, 102, 101 }),
                property.RemoveMaterialFromSolid(new[] { 101, 102, 101 }),
                property.DeleteMemberReleaseSpec(101, MemberEnd.End),
                property.DeleteAllControlDependentRelations(),
                property.RemoveAllElementOffsetSpec(),
                property.RemovePropertyFromBeam(101),
                property.DeleteProperty(7),
                property.DeleteMemberSpec(7),
                property.RemoveMemberReleaseSpecFromBeam(101, MemberEnd.End),
                property.RemoveMemberOffsetSpecFromBeam(101, MemberEnd.End),
                property.RemoveMemberTrussSpecFromBeam(101),
                property.RemoveMemberInactiveSpecFromBeam(101),
                property.RemoveMemberTensionSpecFromBeam(101),
                property.RemoveMemberCompressionSpecFromBeam(101),
                property.RemoveMemberIgnoreStiffSpecFromBeam(101),
                property.RemoveMemberCableSpecFromBeam(101, CableDefinitionKind.UnstressedLength),
                property.RemoveElementPlaneStressSpecFromPlate(101),
                property.RemoveElementIgnoreInplaneRotnSpecFromPlate(101),
                property.RemoveElementNodeReleaseSpecFromPlate(101, 11) };
            if (mutations.Length != 20 || mutations.Any(r => !r.IsSuccessful)) throw new InvalidOperationException("Installed cleanup acknowledgements failed.");
            if (mutations[7].PropertyId != 7 || mutations[8].SpecificationId != 7 || mutations[3].MemberEnd != MemberEnd.End || mutations[16].CableDefinitionKind != CableDefinitionKind.UnstressedLength || mutations[19].NodeId != 11 || mutations[19].PlateNodeIndex.HasValue) throw new InvalidOperationException("Installed requested removal identities failed.");
            if (!mutations[1].TargetIds.SequenceEqual(new[] {101,102,101})) throw new InvalidOperationException("Installed bulk removal ownership failed.");
            var grant=property.Capabilities.GetStandardCatalog(); if (!grant.IsAvailable) throw new InvalidOperationException(grant.Reason);
            var c=grant.Capability;
            if (c.GetDefaultStandardProfileDBFolder().Data!=@"C:\STAAD\DefaultProfiles" || !c.IsStandardDatabaseSection(7).Data.IsStandardDatabase || c.GetStandardSectionDatabaseName(7).Data.Text!="EXACT_DB" || c.GetStandardSectionTableName(7).Data.Text!="EXACT_TABLE" || c.GetStandardSectionName(7).Data.PropertyId!=7) throw new InvalidOperationException("Installed catalog observations failed.");
        }
    }
}
