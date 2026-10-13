using StaadPro.Interop.Attributes;
using StaadPro.Interop.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StaadPro.Interop.Comparers
{
    /// <summary>Compares load definitions using the reference case identity and field predicate.</summary>
    public class MemberLoadComparer : IEqualityComparer<MemberLoad>
    {
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<Type, PropertyInfo[]> CachedProperties = new System.Collections.Concurrent.ConcurrentDictionary<Type, PropertyInfo[]>();

        /// <summary>Tests structural definition equality without native access.</summary>
        /// <param name="x">First load definition.</param>
        /// <param name="y">Second load definition.</param>
        /// <returns>The reference comparison result.</returns>
        /// <remarks>Requires the same LoadCase object. Member definitions also require the same runtime type and equality of LoadDefiningProperty fields; nodal definitions compare all six force/moment components. Load definitions must remain stable during grouping. Initialized case/force data is required.</remarks>
        public bool Equals(MemberLoad x, MemberLoad y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;
            if (x.LoadCase != y.LoadCase) return false;

            var type = x.GetType();
            if (type != y.GetType()) return false;

            var props = GetLoadDefiningProperties(type);

            foreach (var p in props)
            {
                var vx = p.GetValue(x);
                var vy = p.GetValue(y);

                if (!Equals(vx, vy))
                    return false;
            }

            return true;
        }

        /// <summary>Computes the reference hash for a stable load definition.</summary>
        /// <param name="obj">Initialized load definition to hash.</param>
        /// <returns>A hash using case metadata and load-defining values.</returns>
        /// <remarks>No native access. Non-null definitions require a non-null LoadCase; nodal definitions also require initialized Forces. Hash/equality fields must remain stable in collections.</remarks>
        public int GetHashCode(MemberLoad obj)
        {
            if (obj == null) return 0;

            unchecked
            {
                int hash = obj.LoadCase.GetHashCode();
                hash = (hash * 397) ^ obj.GetType().GetHashCode();

                foreach (var p in GetLoadDefiningProperties(obj.GetType()))
                {
                    var v = p.GetValue(obj);
                    if (v != null)
                        hash = (hash * 397) ^ v.GetHashCode();
                }

                return hash;
            }
        }

        private static PropertyInfo[] GetLoadDefiningProperties(Type type)
        {
            if (!CachedProperties.TryGetValue(type, out var props))
            {
                props = type
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => Attribute.IsDefined(p, typeof(LoadDefiningPropertyAttribute)))
                    .ToArray();

                CachedProperties[type] = props;
            }

            return props;
        }
    }

}
