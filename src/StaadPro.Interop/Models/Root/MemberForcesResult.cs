using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using StaadPro.Interop.Entities;
namespace StaadPro.Interop.Models.Root
{
    /// <summary>Contains result availability and ordered per-end outcomes for one member-force batch.</summary>
    /// <remarks>Collections own their storage. Successful force models remain editable source-compatible
    /// managed records. Failed native flags retain identity without exposing unqualified buffer values.</remarks>
    public sealed class MemberForcesResult
    {
        internal MemberForcesResult(bool? areResultsAvailable, MemberForceQueryResult[] queries)
        {
            AreResultsAvailable = areResultsAvailable;
            Queries = new ReadOnlyCollection<MemberForceQueryResult>((MemberForceQueryResult[])queries.Clone());
            Forces = new ReadOnlyCollection<MemberForces>(queries.Where(q => q.IsSuccessful).Select(q => q.Forces).ToArray());
        }
        /// <summary>Gets the native availability observation, when performed.</summary>
        /// <value>True/false from AreResultsAvailable, or null for an empty request that made no native calls.</value>
        public bool? AreResultsAvailable { get; }
        /// <summary>Gets whether every requested query succeeded.</summary>
        /// <value>False when results were unavailable or any native end query failed; true for an empty request without asserting native availability.</value>
        public bool IsSuccessful => AreResultsAvailable != false && Queries.All(q => q.IsSuccessful);
        /// <summary>Gets ordered per-end query outcomes.</summary>
        /// <value>A read-only list in first-seen load-case/member order, start then end; empty if unavailable or no work was requested.</value>
        public IReadOnlyList<MemberForceQueryResult> Queries { get; }
        /// <summary>Gets only the successfully qualified force records.</summary>
        /// <value>A read-only collection preserving distinct identities even when their six component values are equal.
        /// It may be partial when IsSuccessful is false; inspect Queries for failures.</value>
        public IReadOnlyList<MemberForces> Forces { get; }
    }
}
