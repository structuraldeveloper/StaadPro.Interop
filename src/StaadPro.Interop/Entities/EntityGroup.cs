using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Common;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Helpers;

namespace StaadPro.Interop.Entities
{
    /// <summary>
    /// Represents a non-generic collection of entity identifiers grouped under a STAAD group name.
    /// </summary>
    public sealed class EntityGroup
    {
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='EntityGroup..ctor()']/*" />
        public EntityGroup()
        {
            Entities = new HashSet<int>();
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='EntityGroup.P:GroupName']/*" />
        public string GroupName { get; set; }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='EntityGroup.P:EntityType']/*" />
        public EntityType EntityType { get; set; }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='EntityGroup.P:Entities']/*" />
        public HashSet<int> Entities { get; set; }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='EntityGroup.CreateSubGroupFrom`1(string,string,IEnumerable&lt;EntityGroup&gt;)']/*" />
        public static EntityGroup CreateSubGroupFrom<T>(string subGroupName, string lookupGroupName, IEnumerable<EntityGroup> fromEntityGroups)
        {
            return CreateSubGroupFrom<T>(subGroupName, new List<string> { lookupGroupName }, fromEntityGroups);
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='EntityGroup.CreateSubGroupFromGroupContains`1(string,string,IEnumerable&lt;EntityGroup&gt;)']/*" />
        public static EntityGroup CreateSubGroupFromGroupContains<T>(string subGroupName, string lookupGroupName, IEnumerable<EntityGroup> fromEntityGroups)
        {
            return CreateSubGroupFromGroupContains<T>(subGroupName, new List<string> { lookupGroupName }, fromEntityGroups);
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='EntityGroup.CreateSubGroupFrom`1(string,IEnumerable&lt;string&gt;,IEnumerable&lt;EntityGroup&gt;)']/*" />
        public static EntityGroup CreateSubGroupFrom<T>(string subGroupName, IEnumerable<string> lookupGroupNames, IEnumerable<EntityGroup> fromEntityGroups)
        {
            return CreateGroup<T>(subGroupName, fromEntityGroups.Where(g => lookupGroupNames.Any(gn => gn == g.GroupName)).SelectMany(g => g.Entities));
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='EntityGroup.CreateSubGroupFromGroupContains`1(string,IEnumerable&lt;string&gt;,IEnumerable&lt;EntityGroup&gt;)']/*" />
        public static EntityGroup CreateSubGroupFromGroupContains<T>(string subGroupName, IEnumerable<string> lookupGroupNames, IEnumerable<EntityGroup> fromEntityGroups)
        {
            return CreateGroup<T>(subGroupName, fromEntityGroups.Where(g => lookupGroupNames.Any(gn => g.GroupName.Contains(gn))).SelectMany(g => g.Entities));
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='EntityGroup.CreateGroup`1(string,IEnumerable&lt;int&gt;)']/*" />
        public static EntityGroup CreateGroup<T>(string groupName, IEnumerable<int> entities)
        {
            return new EntityGroup
            {
                EntityType = EntityTypeHelpers.GetEntityType<T>(),
                GroupName = groupName,
                Entities = entities.ToHashSet()
            };
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='EntityGroup.CreateGroup`1(string,IEnumerable&lt;T&gt;)']/*" />
        public static EntityGroup CreateGroup<T>(string groupName, IEnumerable<T> entities) where T : IEntity
        {
            return new EntityGroup
            {
                GroupName = groupName,
                EntityType = EntityTypeHelpers.GetEntityType<T>(),
                Entities = entities.Select(b => b.Id).ToHashSet()
            };
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='EntityGroup.GetHashCode`0()']/*" />
        public override int GetHashCode() => GroupName != null ? GroupName.GetHashCode() : 0;

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='EntityGroup.Equals`0(EntityGroup)']/*" />
        public bool Equals(EntityGroup group)
        {
            return group != null && Equality(this, group);
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='EntityGroup.Equals`0(object)']/*" />
        public override bool Equals(object obj)
        {
            return obj is EntityGroup group && Equality(this, group);
        }

        public static bool operator ==(EntityGroup left, EntityGroup right)
        {
            return ReferenceEquals(left, right) || (!(left is null) && !(right is null) && Equality(left, right));
        }

        public static bool operator !=(EntityGroup left, EntityGroup right) => !(left == right);

        private static bool Equality(EntityGroup left, EntityGroup right)
        {
            return left.GroupName == right.GroupName &&
                   left.EntityType == right.EntityType &&
                   left.Entities.Count == right.Entities.Count &&
                   left.Entities.All(e => right.Entities.Contains(e));
        }
    }
}
