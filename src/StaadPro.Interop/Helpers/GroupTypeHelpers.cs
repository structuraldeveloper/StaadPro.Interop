using System;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Helpers
{
    public static class GroupTypeHelpers
    {
        public static GroupType GetGroupType<T>() => GetGroupType(typeof(T));

        public static GroupType GetGroupType(Type type)
        {
            if (type == typeof(Node)) return GroupType.Node;
            if (type == typeof(Beam)) return GroupType.Beam;
            if (type == typeof(Plate)) return GroupType.Plate;

            throw new NotSupportedException($"Group type for '{type.Name}' is not supported.");
        }

        public static GroupType GetGroupType(EntityType entityType)
        {
            switch (entityType)
            {
                case EntityType.Node: return GroupType.Node;
                case EntityType.Beam: return GroupType.Beam;
                case EntityType.Plate: return GroupType.Plate;
                default: throw new NotSupportedException($"Cannot convert EntityType '{entityType}' to GroupType.");
            }
        }
    }
}
