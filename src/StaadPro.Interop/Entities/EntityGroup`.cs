using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using StaadPro.Interop.Common;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Helpers;

namespace StaadPro.Interop.Entities
{
    /// <summary>
    /// Represents a strongly-typed collection of entities grouped under a STAAD group name.
    /// </summary>
    public class EntityGroup<T> : ValidatablePropertyStore, INotifyPropertyChanged where T : IEntity
    {
        public EntityGroup()
        {
            Entities = new HashSet<T>();
        }

        public string GroupName { get => Get<string>(); set => Set(value); }

        public HashSet<T> Entities { get => Get<HashSet<T>>(); set => Set(value); }

        public EntityType EntityType => EntityTypeHelpers.GetEntityType<T>();

        public static EntityGroup ToNonGeneric(EntityGroup<T> entityGroup)
        {
            return new EntityGroup
            {
                GroupName = entityGroup.GroupName,
                EntityType = entityGroup.EntityType,
                Entities = entityGroup.Entities.Select(e => e.Id).ToHashSet()
            };
        }

        public static HashSet<EntityGroup> ToNonGeneric(HashSet<EntityGroup<T>> entityGroups)
        {
            return entityGroups.Select(ToNonGeneric).ToHashSet();
        }

        public override int GetHashCode() => GroupName != null ? GroupName.GetHashCode() : 0;

        public bool Equals(EntityGroup<T> group)
        {
            return group != null && Equality(this, group);
        }

        public override bool Equals(object obj)
        {
            return obj is EntityGroup<T> group && Equality(this, group);
        }

        public static bool operator ==(EntityGroup<T> left, EntityGroup<T> right)
        {
            return ReferenceEquals(left, right) || (!(left is null) && !(right is null) && Equality(left, right));
        }

        public static bool operator !=(EntityGroup<T> left, EntityGroup<T> right) => !(left == right);

        private static bool Equality(EntityGroup<T> left, EntityGroup<T> right)
        {
            return left.GroupName == right.GroupName &&
                   left.EntityType == right.EntityType &&
                   left.Entities.Count == right.Entities.Count &&
                   left.Entities.All(e => right.Entities.Contains(e));
        }
    }
}
