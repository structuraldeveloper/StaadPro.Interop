using System;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Helpers
{
    public static class GroupTypeHelpers
    {
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='GroupTypeHelpers.GetGroupType`1()']/*" />
        public static GroupType GetGroupType<T>() => GetGroupType(typeof(T));

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='GroupTypeHelpers.GetGroupType`0(Type)']/*" />
        public static GroupType GetGroupType(Type type)
        {
            if (type == typeof(Node)) return GroupType.Node;
            if (type == typeof(Beam)) return GroupType.Beam;
            if (type == typeof(Plate)) return GroupType.Plate;

            throw new NotSupportedException($"Group type for '{type.Name}' is not supported.");
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='GroupTypeHelpers.GetGroupType`0(EntityType)']/*" />
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
