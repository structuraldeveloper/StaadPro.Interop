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
        /// <summary>Initializes a typed group with an empty entity set.</summary>
        public EntityGroup()
        {
            Entities = new HashSet<T>();
        }

        public string GroupName { get => Get<string>(); set => Set(value); }

        public HashSet<T> Entities { get => Get<HashSet<T>>(); set => Set(value); }

        public EntityType EntityType => EntityTypeHelpers.GetEntityType<T>();

        /// <summary>Copies a typed group into a group of entity identifiers.</summary>
        /// <param name="entityGroup">The non-null typed group whose name, family and entity IDs are copied.</param>
        /// <returns>A new group with a new identifier set; managed entity objects are not copied.</returns>
        /// <remarks>Reads each entity's Id and collapses repeated identifiers. The input group is unchanged;
        /// no native model access occurs. Unsupported entity CLR types fail family classification.</remarks>
        public static EntityGroup ToNonGeneric(EntityGroup<T> entityGroup)
        {
            return new EntityGroup
            {
                GroupName = entityGroup.GroupName,
                EntityType = entityGroup.EntityType,
                Entities = entityGroup.Entities.Select(e => e.Id).ToHashSet()
            };
        }

        /// <summary>Copies typed groups into non-generic groups of entity identifiers.</summary>
        /// <param name="entityGroups">The non-null group set to convert; entries must be non-null.</param>
        /// <returns>A new set of new non-generic groups, with fresh identifier sets. Equal groups collapse.</returns>
        /// <remarks>The conversion is eager, leaves the source groups unchanged, and makes no STAAD calls.</remarks>
        public static HashSet<EntityGroup> ToNonGeneric(HashSet<EntityGroup<T>> entityGroups)
        {
            return entityGroups.Select(ToNonGeneric).ToHashSet();
        }

        /// <summary>Hashes the current group name.</summary>
        /// <returns>The name hash, or zero when the group name is null.</returns>
        /// <remarks>Keep GroupName stable while the group is stored in a hash collection.</remarks>
        public override int GetHashCode() => GroupName != null ? GroupName.GetHashCode() : 0;

        /// <summary>Compares group names, entity families and entity-set membership.</summary>
        /// <param name="group">The typed group to compare; null compares unequal.</param>
        /// <returns>True when names/families match and both sets contain the same entities; otherwise false.</returns>
        /// <remarks>Entity equality determines set membership. GroupName and membership must remain stable
        /// during comparison; this operation does not access STAAD.</remarks>
        public bool Equals(EntityGroup<T> group)
        {
            return group != null && Equality(this, group);
        }

        /// <summary>Compares this group with an object of the same closed generic group type.</summary>
        /// <param name="obj">The object to compare; null or a different type compares unequal.</param>
        /// <returns>True for equal names, families and entity-set membership; otherwise false.</returns>
        /// <remarks>Uses the same managed equality contract as the typed Equals overload.</remarks>
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
