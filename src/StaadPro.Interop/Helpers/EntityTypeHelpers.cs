using System;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Helpers
{
    public static class EntityTypeHelpers
    {
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='EntityTypeHelpers.GetEntityType`1()']/*" />
        public static EntityType GetEntityType<T>()
        {
            return GetEntityType(typeof(T));
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='EntityTypeHelpers.GetEntityType`0(Type)']/*" />
        public static EntityType GetEntityType(Type type)
        {
            if (type == typeof(Node)) return EntityType.Node;
            if (type == typeof(Beam)) return EntityType.Beam;
            if (type == typeof(Plate)) return EntityType.Plate;
            if (type == typeof(Member)) return EntityType.PhysicalMember;

            throw new NotSupportedException($"Entity type '{type.Name}' is not supported.");
        }
    }
}
