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
        public EntityGroup()
        {
            Entities = new HashSet<int>();
        }

        public string GroupName { get; set; }

        public EntityType EntityType { get; set; }

        public HashSet<int> Entities { get; set; }

        public static EntityGroup CreateSubGroupFrom<T>(string subGroupName, string lookupGroupName, IEnumerable<EntityGroup> fromEntityGroups)
        {
            return CreateSubGroupFrom<T>(subGroupName, new List<string> { lookupGroupName }, fromEntityGroups);
        }

        public static EntityGroup CreateSubGroupFromGroupContains<T>(string subGroupName, string lookupGroupName, IEnumerable<EntityGroup> fromEntityGroups)
        {
            return CreateSubGroupFromGroupContains<T>(subGroupName, new List<string> { lookupGroupName }, fromEntityGroups);
        }

        public static EntityGroup CreateSubGroupFrom<T>(string subGroupName, IEnumerable<string> lookupGroupNames, IEnumerable<EntityGroup> fromEntityGroups)
        {
            return CreateGroup<T>(subGroupName, fromEntityGroups.Where(g => lookupGroupNames.Any(gn => gn == g.GroupName)).SelectMany(g => g.Entities));
        }

        public static EntityGroup CreateSubGroupFromGroupContains<T>(string subGroupName, IEnumerable<string> lookupGroupNames, IEnumerable<EntityGroup> fromEntityGroups)
        {
            return CreateGroup<T>(subGroupName, fromEntityGroups.Where(g => lookupGroupNames.Any(gn => g.GroupName.Contains(gn))).SelectMany(g => g.Entities));
        }

        public static EntityGroup CreateGroup<T>(string groupName, IEnumerable<int> entities)
        {
            return new EntityGroup
            {
                EntityType = EntityTypeHelpers.GetEntityType<T>(),
                GroupName = groupName,
                Entities = entities.ToHashSet()
            };
        }

        public static EntityGroup CreateGroup<T>(string groupName, IEnumerable<T> entities) where T : IEntity
        {
            return new EntityGroup
            {
                GroupName = groupName,
                EntityType = EntityTypeHelpers.GetEntityType<T>(),
                Entities = entities.Select(b => b.Id).ToHashSet()
            };
        }

        public override int GetHashCode() => GroupName != null ? GroupName.GetHashCode() : 0;

        public bool Equals(EntityGroup group)
        {
            return group != null && Equality(this, group);
        }

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
