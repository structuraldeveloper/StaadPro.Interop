using System.Collections.Generic;

using StaadPro.Interop.Entities;

namespace StaadPro.Interop.Comparers
{
    /// <summary>Compares load definitions using the reference case identity and field predicate.</summary>
    public class NodalLoadComparer : IEqualityComparer<NodalLoad>
    {
        /// <summary>Tests structural definition equality without native access.</summary>
        /// <param name="x">First load definition.</param>
        /// <param name="y">Second load definition.</param>
        /// <returns>The reference comparison result.</returns>
        /// <remarks>Requires the same LoadCase object. Member definitions also require the same runtime type and equality of LoadDefiningProperty fields; nodal definitions compare all six force/moment components. Load definitions must remain stable during grouping. Initialized case/force data is required.</remarks>
        public bool Equals(NodalLoad x, NodalLoad y)
        {
            if (x == null || y == null) return false;

            return x.LoadCase == y.LoadCase &&
                   x.Forces.Fx == y.Forces.Fx &&
                   x.Forces.Fy == y.Forces.Fy &&
                   x.Forces.Fz == y.Forces.Fz &&
                   x.Forces.Mx == y.Forces.Mx &&
                   x.Forces.My == y.Forces.My &&
                   x.Forces.Mz == y.Forces.Mz;
        }

        /// <summary>Computes the reference hash for a stable load definition.</summary>
        /// <param name="obj">Initialized load definition to hash.</param>
        /// <returns>A hash using case metadata and load-defining values.</returns>
        /// <remarks>No native access. Non-null definitions require a non-null LoadCase; nodal definitions also require initialized Forces. Hash/equality fields must remain stable in collections.</remarks>
        public int GetHashCode(NodalLoad obj)
        {
            unchecked
            {
                int hash = obj.LoadCase.GetHashCode();
                hash = (hash * 397) ^ obj.Forces.Fx.GetHashCode();
                hash = (hash * 397) ^ obj.Forces.Fy.GetHashCode();
                hash = (hash * 397) ^ obj.Forces.Fz.GetHashCode();
                hash = (hash * 397) ^ obj.Forces.Mx.GetHashCode();
                hash = (hash * 397) ^ obj.Forces.My.GetHashCode();
                hash = (hash * 397) ^ obj.Forces.Mz.GetHashCode();
                return hash;
            }
        }
    }
}
